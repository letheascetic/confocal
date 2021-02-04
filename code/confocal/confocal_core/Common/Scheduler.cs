using confocal_core.Model;
using confocal_core.ViewModel;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace confocal_core.Common
{
    public class Scheduler
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static Scheduler pScheduler = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        public event ScanImageUpdatedEventHandler ScanImageUpdatedEvent;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly int PMT_TASK_COUNT = 4;
        private static readonly int APD_TASK_COUNT = 4;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public event ScanAreaChangedEventHandler ScanAreaChangedEvent;
        public event ScanAreaChangedEventHandler FullScanAreaChangedEvent;
        public event ScanAcquisitionChangedEventHandler ScanAcquisitionChangedEvent;
        public event ScannerHeadModelChangedEventHandler ScannerHeadModelChangedEvent;
        public event ScanDirectionChangedEventHandler ScanDirectionChangedEvent;
        public event ScanModeChangedEventHandler ScanModeChangedEvent;
        public event LineSkipEnableChangedEventHandler LineSkipEnableChangedEvent;
        public event LineSkipChangedEventHandler LineSkipChangedEvent;
        public event ScanPixelChangedEventHandler ScanPixelChangedEvent;
        public event ScanPixelDwellChangedEventHandler ScanPixelDwellChangedEvent;
        public event PinHoleChangedEventHandler PinHoleChangedEvent;
        public event ChannelGainChangedEventHandler ChannelGainChangedEvent;
        public event ChannelOffsetChangedEventHandler ChannelOffsetChangedEvent;
        public event ChannelPowerChangedEventHandler ChannelPowerChangedEvent;
        public event ChannelActivateChangedEventHandler ChannelActivateChangedEvent;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private NiDaq mNiDaq;
        private SequenceModel mSequence;
        private ConfigViewModel mConfig;

        private List<ScanTask> mScanTasks;
        private ScanTask mScanningTask;

        private CancellationTokenSource mCancelToken;
        private BlockingCollection<PmtSampleData> mPmtSampleQueue;
        private BlockingCollection<ApdSampleData> mApdSampleQueue;
        private Task[] mSampleWorkers;

        public List<ScanTask> ScanTasks
        {
            get { return mScanTasks; }
            set { mScanTasks = value; }
        }

        public ScanTask ScanningTask
        {
            get { return mScanningTask; }
            set { mScanningTask = value; }
        }

        public SequenceModel Sequence
        {
            get { return mSequence; }
        }

        public ConfigViewModel Config
        {
            get { return mConfig; }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        public static Scheduler CreateInstance()
        {
            if (pScheduler == null)
            {
                lock (locker)
                {
                    if (pScheduler == null)
                    {
                        pScheduler = new Scheduler();
                    }
                }
            }
            return pScheduler;
        }

        public void Initialize()
        {
            mConfig = ConfigViewModel.GetConfig();
            mNiDaq = new NiDaq();
            mSequence = SequenceModel.CreateInstance();
            ScanTasks = new List<ScanTask>();
            ScanningTask = null;
            ConfigUsbDac();
            ConfigLaser();
            mNiDaq.AiSamplesReceived += PmtReceiveSamples;
            mNiDaq.CiSamplesReceived += ApdReceiveSamples;
        }

        public void Release()
        {
            ReleaseLaser();
            ReleaseUsbDac();
        }

        /// <summary>
        /// 创建指定TaskID的扫描任务，若已经存在，则返回已经存在的
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskName"></param>
        /// <param name="scanTask"></param>
        /// <returns></returns>
        public API_RETURN_CODE CreateScanTask(int taskId, string taskName, out ScanTask scanTask)
        {
            scanTask = GetScanTask(taskId);
            if(scanTask == null)
            {
                scanTask = new ScanTask(taskId, taskName);
                ScanTasks.Add(scanTask);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 获取指定TaskId的扫描任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ScanTask GetScanTask(int taskId)
        {
            return ScanTasks.Where(p => p.TaskId == taskId).FirstOrDefault();
        }

        /// <summary>
        /// 启动扫描任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskName"></param>
        /// <returns></returns>
        public API_RETURN_CODE StartScanTask(ScanTask scanTask)
        {
            API_RETURN_CODE code;
            if (mConfig.GetActivatedChannelNum() == 0)
            {
                code = API_RETURN_CODE.API_FAILED_NI_NO_AI_CHANNEL_ACTIVATED;
                Logger.Info(string.Format("start scan task[{0}|{1}] failed: [{2}].", scanTask.TaskId, scanTask.TaskName, code));
                return code;
            }

            ScanningTask = scanTask;

            mSequence.GenerateScanCoordinates();         // 生成扫描范围序列和电压序列
            mSequence.GenerateFrameScanWaves();          // 生成帧电压序列
            OpenLaserChannels();                         // 打开激光器
            ScanningTask.Start();

            mCancelToken = new CancellationTokenSource();
            if (mConfig.Detector.CurrentDetecor.ID == DetectorTypeModel.PMT)
            {
                mPmtSampleQueue = new BlockingCollection<PmtSampleData>(new ConcurrentQueue<PmtSampleData>());
                mSampleWorkers = new Task[PMT_TASK_COUNT];
                for (int i = 0; i < mSampleWorkers.Length; i++)
                {
                    mSampleWorkers[i] = Task.Run(() => PmtSampleWorker());
                }
            }
            else
            {
                mApdSampleQueue = new BlockingCollection<ApdSampleData>(new ConcurrentQueue<ApdSampleData>());
                mSampleWorkers = new Task[APD_TASK_COUNT];
                for (int i = 0; i < mSampleWorkers.Length; i++)
                {
                    mSampleWorkers[i] = Task.Run(() => ApdSampleWorker());
                }
            }

            code = mNiDaq.Start();                       // 启动板卡

            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Logger.Info(string.Format("start scan task[{0}|{1}] failed: [{2}].", scanTask.TaskId, scanTask.TaskName, code));
                StopScanTask();
                return code;
            }

            Logger.Info(string.Format("start scan task[{0}|{1}] success.", scanTask.TaskId, scanTask.TaskName));
            return code;
        }

        /// <summary>
        /// 停止扫描任务
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE StopScanTask()
        {
            if (ScanningTask == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_INVALID;
            }

            if (GetScanTask(ScanningTask.TaskId) == null)
            {
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_NOT_FOUND;
            }

            Logger.Info(string.Format("stop scan task[{0}|{1}].", ScanningTask.TaskId, ScanningTask.TaskName));


            mNiDaq.Stop();
            ScanningTask.Stop();

            mCancelToken.Cancel();
            if (mConfig.Detector.CurrentDetecor.ID == DetectorTypeModel.PMT)
            {
                mPmtSampleQueue.CompleteAdding();
            }
            else
            {
                mApdSampleQueue.CompleteAdding();
            }
            Task.WaitAll(mSampleWorkers);
            Dispose();

            ScanningTask = null;
            CloseLaserChannels();

            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 采集模式状态[启动、切换、停止]变化事件处理
        /// </summary>
        /// <param name="liveModeEnabled"></param>
        /// <param name="captureModeEnabled"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanAcquisitionChangeCommand(bool liveModeEnabled, bool captureModeEnabled)
        {
            BeforePropertyChanged();

            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            if (liveModeEnabled || captureModeEnabled)
            {
                CreateScanTask(0, "实时扫描", out ScanTask scanTask);
                code = StartScanTask(scanTask);
            }

            if (code == API_RETURN_CODE.API_SUCCESS)
            {
                mConfig.ScanAcquisitionChangeCommand(liveModeEnabled, captureModeEnabled);
                if (ScanAcquisitionChangedEvent != null)
                {
                    return ScanAcquisitionChangedEvent.Invoke(mConfig.SelectedScanAcquisition);
                }
            }

            return code;
        }

        /// <summary>
        /// 切换扫描头[双镜or三镜]
        /// </summary>
        /// <param name="twoGalvEnabled"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScannerHeadChangeCommand(bool twoGalvEnabled)
        {
            BeforePropertyChanged();
            mConfig.ScannerHeadChangeCommand(twoGalvEnabled);
            AfterPropertyChanged();

            if (ScannerHeadModelChangedEvent != null)
            {
                return ScannerHeadModelChangedEvent.Invoke(mConfig.SelectedScannerHead);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 扫描方向切换事件
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE ScanDirectionChangeCommand(bool uniDirectionEnabled)
        {
            BeforePropertyChanged();
            mConfig.ScanDirectionChangeCommand(uniDirectionEnabled);
            AfterPropertyChanged();

            if (ScanDirectionChangedEvent != null)
            {
                return ScanDirectionChangedEvent.Invoke(mConfig.SelectedScanDirection);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 扫描模式切换事件
        /// </summary>
        /// <param name="galvEnabled"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanModeChangeCommand(bool galvEnabled)
        {
            BeforePropertyChanged();
            mConfig.ScanModeChangeCommand(galvEnabled);
            AfterPropertyChanged();

            if (ScanModeChangedEvent != null)
            {
                return ScanModeChangedEvent.Invoke(mConfig.SelectedScanMode);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 扫描像素切换事件
        /// </summary>
        /// <param name="selectedScanPixel"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelChangeCommand(ScanPixelModel selectedScanPixel)
        {
            BeforePropertyChanged();
            mConfig.ScanPixelChangeCommand(selectedScanPixel);
            AfterPropertyChanged();

            if (ScanPixelChangedEvent != null)
            {
                return ScanPixelChangedEvent.Invoke(mConfig.SelectedScanPixel);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 像素时间变更事件
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelDwellChangeCommand(ScanPixelDwellModel selectedPixelDwell)
        {
            BeforePropertyChanged();
            mConfig.ScanPixelDwellChangeCommand(selectedPixelDwell);
            AfterPropertyChanged();

            if (ScanPixelDwellChangedEvent != null)
            {
                ScanPixelDwellChangedEvent.Invoke(mConfig.SelectedScanPixelDwell);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 更新双向像素补偿
        /// </summary>
        /// <param name="scanPixelCalibration"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelCalibrationChangeCommand(int scanPixelCalibration)
        {
            return mConfig.ScanPixelCalibrationChangeCommand(scanPixelCalibration);
        }

        /// <summary>
        /// 扫描像素缩放系数更新事件
        /// </summary>
        /// <param name="scanPixelScale"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelScaleChangeCommand(int scanPixelScale)
        {
            return mConfig.ScanPixelScaleChangeCommand(scanPixelScale);
        }

        /// <summary>
        /// 跳行扫描使能变更事件
        /// </summary>
        /// <param name="lineSkipEnabled"></param>
        /// <returns></returns>
        public API_RETURN_CODE LineSkipEnableChangeCommand(bool lineSkipEnabled)
        {
            BeforePropertyChanged();
            mConfig.LineSkipEnableChangeCommand(lineSkipEnabled);
            AfterPropertyChanged();

            if (LineSkipEnableChangedEvent != null)
            {
                return LineSkipEnableChangedEvent.Invoke(mConfig.ScanLineSkipEnabled);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 跳行扫描值变更事件
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE LineSkipValueChangeCommand(ScanLineSkipModel lineSkip)
        {
            BeforePropertyChanged();
            mConfig.LineSkipValueChangeCommand(lineSkip);
            AfterPropertyChanged();

            if (LineSkipChangedEvent != null)
            {
                return LineSkipChangedEvent.Invoke(mConfig.SelectedScanLineSkip);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 通道增益更新事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gain"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelGainChangeCommand(int id, int gain)
        {
            mConfig.ChannelGainChangeCommand(id, gain);
            ScanChannelModel channel = mConfig.FindScanChannel(id);
            API_RETURN_CODE code = UsbDac.SetDacOut((uint)channel.ID, UsbDac.ConfigValueToVout(channel.Gain));

            if (ChannelGainChangedEvent != null)
            {
                return ChannelGainChangedEvent.Invoke(channel);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 通道偏置更新事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelOffsetChangeCommand(int id, int offset)
        {
            mConfig.ChannelOffsetChangeCommand(id, offset);
            if (ChannelOffsetChangedEvent != null)
            {
                return ChannelOffsetChangedEvent.Invoke(mConfig.FindScanChannel(id));
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 通道功率更新事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelPowerChangeCommand(int id, int power)
        {
            mConfig.ChannelPowerChangeCommand(id, power);
            ScanChannelModel channel = mConfig.FindScanChannel(id);
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            if (Laser.IsConnected())
            {
                code = Laser.SetChannelPower(channel.ID, Laser.ConfigValueToPower(channel.LaserPower));
            }

            if (ChannelPowerChangedEvent != null)
            {
                return ChannelPowerChangedEvent.Invoke(channel);
            }
            return code;
        }

        /// <summary>
        /// 通道激光开关事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activated"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelActivateChangeCommand(int id, bool activated)
        {
            BeforePropertyChanged();
            mConfig.ChannelActivateChangeCommand(id, activated);
            ScanChannelModel channel = mConfig.FindScanChannel(id);

            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            if (mConfig.IsScanning)
            {
                // 当前正在扫描，这分两种情况：
                // 1 原先没打开的激光器被打开了：打开该激光器，设置功率，启动扫描
                // 2 原先已经打开的激光器被关闭了：
                // 2.1 该激光器是唯一打开的激光器，则不应该被关闭，设置Activated为真，打开激光器，启动扫描
                // 2.2 该激光器不是唯一打开的激光器，则可以被关闭，关闭该激光器，

                if (!channel.Activated && mConfig.GetActivatedChannelNum() == 0)
                {
                    channel.Activated = true;
                }

                CreateScanTask(0, "实时扫描", out ScanTask scanTask);
                code = StartScanTask(scanTask);
            }

            if (ChannelActivateChangedEvent != null)
            {
                return ChannelActivateChangedEvent.Invoke(channel);
            }
            return code;
        }

        /// <summary>
        /// 小孔切换事件
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE PinHoleSelectChangeCommand(ScanPinHoleModel scanPinHole)
        {
            return mConfig.PinHoleSelectChangeCommand(scanPinHole);
        }

        /// <summary>
        /// 小孔孔径变化事件
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public API_RETURN_CODE PinHoleValueChangeCommand(int value)
        {
            mConfig.PinHoleValueChangeCommand(value);
            if (PinHoleChangedEvent != null)
            {
                return PinHoleChangedEvent.Invoke(mConfig.SelectedPinHole);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 切换扫描范围
        /// </summary>
        /// <param name="scanArea"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanAreaChangeCommand(ScanAreaModel scanArea)
        {
            BeforePropertyChanged();
            mConfig.ScanAreaChangeCommand(scanArea);
            AfterPropertyChanged();

            if (ScanAreaChangedEvent != null)
            {
                return ScanAreaChangedEvent.Invoke(mConfig.SelectedScanArea);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 切换全扫描范围
        /// </summary>
        /// <param name="fullScanArea"></param>
        /// <returns></returns>
        public API_RETURN_CODE FullScanAreaChangeCommand(ScanAreaModel fullScanArea)
        {
            mConfig.FullScanAreaChangeCommand(fullScanArea);

            if (FullScanAreaChangedEvent != null)
            {
                return FullScanAreaChangedEvent.Invoke(mConfig.FullScanArea);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 切换全扫描范围
        /// </summary>
        /// <param name="scanRange"></param>
        /// <returns></returns>
        public API_RETURN_CODE FullScanAreaChangeCommand(float scanRange)
        {
            mConfig.FullScanAreaChangeCommand(scanRange);
            if (FullScanAreaChangedEvent != null)
            {
                return FullScanAreaChangedEvent.Invoke(mConfig.FullScanArea);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        public API_RETURN_CODE XGalvoChannelChangeCommand(string xGalvoChannel)
        {
            mConfig.GalvoProperty.XGalvoAoChannel = xGalvoChannel;
            Logger.Info(string.Format("X Galvo Ao Channel [{0}].", mConfig.GalvoProperty.XGalvoAoChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE YGalvoChannelChangeCommand(string yGalvoChannel)
        {
            mConfig.GalvoProperty.YGalvoAoChannel = yGalvoChannel;
            Logger.Info(string.Format("Y Galvo Ao Channel [{0}].", mConfig.GalvoProperty.YGalvoAoChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE Y2GalvoChannelChangeCommand(string y2GalvoChannel)
        {
            mConfig.GalvoProperty.Y2GalvoAoChannel = y2GalvoChannel;
            Logger.Info(string.Format("Y2 Galvo Ao Channel [{0}].", mConfig.GalvoProperty.Y2GalvoAoChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE XGalvoOffsetVoltageChangeCommand(double offsetVoltage)
        {
            mConfig.GalvoProperty.XGalvoOffsetVoltage = offsetVoltage;
            Logger.Info(string.Format("X Galvo Offset Voltage [{0}].", mConfig.GalvoProperty.XGalvoOffsetVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE XGalvoCalibrationVoltageChangeCommand(double calibrationVoltage)
        {
            mConfig.GalvoProperty.XGalvoCalibrationVoltage = calibrationVoltage;
            Logger.Info(string.Format("X Galvo Calibration Voltage [{0}].", mConfig.GalvoProperty.XGalvoCalibrationVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE YGalvoOffsetVoltageChangeCommand(double offsetVoltage)
        {
            mConfig.GalvoProperty.YGalvoOffsetVoltage = offsetVoltage;
            Logger.Info(string.Format("Y Galvo Offset Voltage [{0}].", mConfig.GalvoProperty.YGalvoOffsetVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE YGalvoCalibrationVoltageChangeCommand(double calibrationVoltage)
        {
            mConfig.GalvoProperty.YGalvoCalibrationVoltage = calibrationVoltage;
            Logger.Info(string.Format("Y Galvo Calibration Voltage [{0}].", mConfig.GalvoProperty.YGalvoCalibrationVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE GalvoResponseTimeChangeCommand(double responseTime)
        {
            mConfig.GalvoProperty.GalvoResponseTime = responseTime;
            Logger.Info(string.Format("Galvo Response Time [{0}].", mConfig.GalvoProperty.GalvoResponseTime));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE DetectorModeChangeCommand(bool pmtModeEnabled)
        {
            mConfig.Detector.DetectorPmt.IsEnabled = pmtModeEnabled;
            mConfig.Detector.DetectorApd.IsEnabled = !pmtModeEnabled;
            Logger.Info(string.Format("Detector Mode [{0}].", mConfig.Detector.CurrentDetecor.Text));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE PmtChannelChangeCommand(int id, string pmtChannel)
        {
            PmtChannelModel channel = mConfig.FindPmtChannel(id);
            channel.AiChannel = pmtChannel;
            Logger.Info(string.Format("Pmt [{0}] Ao Channel [{1}].", channel.ID, channel.AiChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ApdSourceChangeCommand(int id, string apdSource)
        {
            ApdChannelModel channel = mConfig.FindApdChannel(id);
            channel.CiSource = apdSource;
            Logger.Info(string.Format("Apd [{0}] Ci Channel [{1}:{2}].", channel.ID, channel.CiSource, channel.CiChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ApdChannelChangeCommand(int id, string apdChannel)
        {
            ApdChannelModel channel = mConfig.FindApdChannel(id);
            channel.CiChannel = apdChannel;
            Logger.Info(string.Format("Apd [{0}] Ci Channel [{1}:{2}].", channel.ID, channel.CiSource, channel.CiChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE StartTriggerChangeCommand(string startTrigger)
        {
            mConfig.Detector.StartTrigger = startTrigger;
            Logger.Info(string.Format("Start Trigger [{0}].", mConfig.Detector.StartTrigger));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE TriggerSignalChangeCommand(string triggerSignal)
        {
            mConfig.Detector.TriggerSignal = triggerSignal;
            Logger.Info(string.Format("Trigger Signal [{0}].", mConfig.Detector.TriggerSignal));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE TriggerReceiverChangeCommand(string triggerReceive)
        {
            mConfig.Detector.TriggerReceive = triggerReceive;
            Logger.Info(string.Format("Trigger Receiver [{0}].", mConfig.Detector.TriggerReceive));
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 控制振镜偏转到其偏置电压对应的角度
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE GalvoCalibrationCommand()
        {
            API_RETURN_CODE code = mNiDaq.GalvoCalibration(Config.GalvoProperty.XGalvoAoChannel, Config.GalvoProperty.XGalvoOffsetVoltage);
            mNiDaq.GalvoCalibration(Config.GalvoProperty.YGalvoAoChannel, Config.GalvoProperty.YGalvoOffsetVoltage);
            return code;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        private Scheduler()
        {
            Initialize();
        }

        /// <summary>
        /// 初始化增益控制板卡
        /// </summary>
        private void ConfigUsbDac()
        {
            UsbDac.Connect();
        }

        /// <summary>
        /// 初始化激光器
        /// </summary>
        private void ConfigLaser()
        {
            ConfigViewModel mConfig = ConfigViewModel.GetConfig();
            string portName = mConfig.LaserPort;
            Laser.Connect(portName);

            for (int i = 0; i < mConfig.GetChannelNum(); i++)
            {
                if (!mConfig.ScanChannels[i].Activated)
                {
                    continue;
                }
                if (Laser.OpenChannel(i) != API_RETURN_CODE.API_SUCCESS)
                {
                    mConfig.ScanChannels[i].Activated = false;
                }
                Laser.SetChannelPower(i, mConfig.ScanChannels[i].LaserPower);
            }
        }

        private void ReleaseLaser()
        {
            ConfigViewModel mConfig = ConfigViewModel.GetConfig();
            if (Laser.IsConnected())
            {
                for (int i = 0; i < mConfig.GetChannelNum(); i++)
                {
                    if (mConfig.ScanChannels[i].Activated)
                    {
                        Laser.CloseChannel(i);
                        mConfig.ScanChannels[i].Activated = false;
                    }
                }
            }
            Laser.Release();
        }

        private void ReleaseUsbDac()
        {
            if (UsbDac.IsConnected())
            {
                UsbDac.Release();
            }
        }

        private void PmtReceiveSamples(object sender, short[][] samples, long[] acquisitionCount)
        {
            try
            {
                PmtSampleData sampleData = new PmtSampleData(samples, acquisitionCount);
                mPmtSampleQueue.TryAdd(sampleData, 50, mCancelToken.Token);
                // Logger.Info(string.Format("Enqueue Pmt Samples [{0}][{1}].", acquisitionCount, samples[0].Length));
            }
            catch (OperationCanceledException)
            {
                Logger.Info(string.Format("Enqueue Pmt Smaples [{0}] Canceled.", acquisitionCount));
                mPmtSampleQueue.CompleteAdding();
            }
        }

        private void ApdReceiveSamples(object sender, int channelIndex, int[] samples, long acquisitionCount)
        {
            try
            {
                ApdSampleData sampleData = new ApdSampleData(samples, channelIndex, acquisitionCount);
                mApdSampleQueue.TryAdd(sampleData, 50, mCancelToken.Token);
                Logger.Info(string.Format("Enqueue Apd Samples [{0}][{1}].", channelIndex, acquisitionCount));
            }
            catch (OperationCanceledException)
            {
                Logger.Info(string.Format("Enqueue Apd Smaples [{0}][{1}] Canceled.", channelIndex, acquisitionCount));
                mPmtSampleQueue.CompleteAdding();
            }
        }

        private API_RETURN_CODE AfterPropertyChanged()
        {
            if (mConfig.IsScanning)
            {
                CreateScanTask(0, "实时扫描", out ScanTask scanTask);
                return StartScanTask(scanTask);
            }
            else
            {
                mSequence.GenerateScanCoordinates();
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 在属性变化前执行
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE BeforePropertyChanged()
        {
            // 如果当前正在采集(有任一采集模式使能)，则先停止采集
            if (ConfigViewModel.GetConfig().IsScanning)
            {
                return StopScanTask();
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 关闭所有的激光
        /// </summary>
        private void CloseLaserChannels()
        {
            for (int i = 0; i < mConfig.GetChannelNum(); i++)
            {
                Laser.CloseChannel(i);
            }
        }

        /// <summary>
        /// 打开对应的激光
        /// </summary>
        private void OpenLaserChannels()
        {
            if (!Laser.IsConnected())
            {
                Laser.Connect(mConfig.LaserPort);
            }

            for (int i = 0; i < mConfig.GetChannelNum(); i++)
            {
                if (mConfig.ScanChannels[i].Activated)
                {
                    Laser.OpenChannel(i);
                    Laser.SetChannelPower(mConfig.ScanChannels[i].ID, Laser.ConfigValueToPower(mConfig.ScanChannels[i].LaserPower));
                }
            }
        }

        private void PmtSampleWorker()
        {
            while (!mPmtSampleQueue.IsCompleted)
            {
                try
                {
                    if (mPmtSampleQueue.TryTake(out PmtSampleData sampleData, 20, mCancelToken.Token))
                    {
                        ScanningTask.ScanInfo.UpdateScanInfo(sampleData);
                        ScanningTask.ConvertSamples(sampleData);
                        if (ScanImageUpdatedEvent != null)
                        {
                            ScanImageUpdatedEvent.Invoke(ScanningTask.ScanData.GrayImages);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.Info(string.Format("Pmt Sample Worker Canceled."));
                    break;
                }
            }
            Logger.Info(string.Format("Pmt Sample Worker Finished."));
        }

        private void ApdSampleWorker()
        {
            while (!mApdSampleQueue.IsCompleted)
            {
                try
                {
                    if (mApdSampleQueue.TryTake(out ApdSampleData sampleData, 20, mCancelToken.Token))
                    {
                        ScanningTask.ScanInfo.UpdateScanInfo(sampleData);
                        ScanningTask.ConvertSamples(sampleData);
                        if (ScanImageUpdatedEvent != null)
                        {
                            ScanImageUpdatedEvent.Invoke(ScanningTask.ScanData.GrayImages);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.Info(string.Format("Apd Sample Worker Canceled."));
                    break;
                }
            }
            Logger.Info(string.Format("Apd Sample Worker Finished."));
        }

        private void Dispose()
        {
            if (mCancelToken != null)
            {
                mCancelToken.Dispose();
                mCancelToken = null;
            }

            if (mApdSampleQueue != null)
            {
                mApdSampleQueue.Dispose();
                mApdSampleQueue = null;
            }

            if (mPmtSampleQueue != null)
            {
                mPmtSampleQueue.Dispose();
                mPmtSampleQueue = null;
            }

            foreach (Task consumer in mSampleWorkers)
            {
                if (consumer != null)
                {
                    consumer.Dispose();
                }
            }
            mSampleWorkers = null;
        }
    }
}
