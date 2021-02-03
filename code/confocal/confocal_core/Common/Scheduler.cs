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
        private static readonly int PMT_TASK_COUNT = 1;
        private static readonly int APD_TASK_COUNT = 2;
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
        /// 启动扫描任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskName"></param>
        /// <returns></returns>
        public API_RETURN_CODE StartScanTask(int taskId, string taskName)
        {
            API_RETURN_CODE code;
            if (mConfig.GetActivatedChannelNum() == 0)
            {
                code = API_RETURN_CODE.API_FAILED_NI_NO_AI_CHANNEL_ACTIVATED;
                Logger.Info(string.Format("start scan task[{0}|{1}] failed: [{2}].", taskId, taskName, code));
                return code;
            }

            ScanTask scanTask = FindScanTask(taskId);

            if (scanTask == null)
            {
                scanTask = new ScanTask(taskId, taskName);
                ScanTasks.Add(scanTask);
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

            if (FindScanTask(ScanningTask.TaskId) == null)
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

        /// <summary>
        /// 查找任务
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ScanTask FindScanTask(int taskId)
        {
            return ScanTasks.Where(p => p.TaskId == taskId).FirstOrDefault();
        }

        /// <summary>
        /// 在属性变化前执行
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE BeforePropertyChanged()
        {
            // 如果当前正在采集(有任一采集模式使能)，则先停止采集
            if (ConfigViewModel.GetConfig().IsScanning)
            {
                return StopScanTask();
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 改变通道激光功率
        /// </summary>
        /// <param name="id"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelPowerChangeCommand(ScanChannelModel channel)
        {
            if (Laser.IsConnected())
            {
                return Laser.SetChannelPower(channel.ID, Laser.ConfigValueToPower(channel.LaserPower));
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 改变通道增益
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelGainChangeCommand(ScanChannelModel channel)
        {
            return UsbDac.SetDacOut((uint)channel.ID, UsbDac.ConfigValueToVout(channel.Gain));
        }

        /// <summary>
        /// 打开或关闭通道激光
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activated"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelActivateChangeCommand(ScanChannelModel channel)
        {
            // 当前没有扫描，则直接返回
            if (!mConfig.IsScanning)
            {
                return API_RETURN_CODE.API_SUCCESS;
            }

            // 当前正在扫描，这分两种情况：
            // 1 原先没打开的激光器被打开了：打开该激光器，设置功率，启动扫描
            // 2 原先已经打开的激光器被关闭了：
            // 2.1 该激光器是唯一打开的激光器，则不应该被关闭，设置Activated为真，打开激光器，启动扫描
            // 2.2 该激光器不是唯一打开的激光器，则可以被关闭，关闭该激光器，

            if (channel.Activated)
            {
                return StartScanTask(0, "实时扫描");
            }

            if (!channel.Activated && mConfig.GetActivatedChannelNum() == 0)
            {
                channel.Activated = true;
            }

            return StartScanTask(0, "实时扫描");
        }

        /// <summary>
        /// 切换扫描头[双镜or三镜]
        /// </summary>
        /// <param name="scannerHead"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScannerHeadChangeCommand(ScannerHeadModel scannerHead)
        {
            return AfterPropertyChanged();
        }

        /// <summary>
        /// 切换扫描方向
        /// </summary>
        /// <param name="scanDirection"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanDirectionChangeCommand(ScanDirectionModel scanDirection)
        {
            return AfterPropertyChanged();
        }

        /// <summary>
        /// 切换扫描模式
        /// </summary>
        /// <param name="scanMode"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanModeChangeCommand(ScanModeModel scanMode)
        {
            return AfterPropertyChanged();
        }

        /// <summary>
        /// 切换扫描像素
        /// </summary>
        /// <param name="selectedScanPixel"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelChangeCommand(ScanPixelModel selectedScanPixel)
        {
            return AfterPropertyChanged();
        }

        /// <summary>
        /// 切换像素时间
        /// </summary>
        /// <param name="selectedPixelDwell"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelDwellChangeCommand(ScanPixelDwellModel selectedPixelDwell)
        {
            return AfterPropertyChanged();
        }

        /// <summary>
        /// 更新双向像素补偿
        /// </summary>
        /// <param name="scanPixelCalibration"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelCalibrationChangeCommand(int scanPixelCalibration)
        {
            mConfig.SelectedScanPixelDwell.ScanPixelCalibration = scanPixelCalibration;
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 更新扫描像素缩放系数
        /// </summary>
        /// <param name="scanPixelScale"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelScaleChangeCommand(int scanPixelScale)
        {
            mConfig.SelectedScanPixelDwell.ScanPixelScale = scanPixelScale;
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 切换扫描范围
        /// </summary>
        /// <param name="scanArea"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanAreaChangeCommand(ScanAreaModel scanArea)
        {
            return AfterPropertyChanged();
        }

        /// <summary>
        /// 切换跳行扫描使能
        /// </summary>
        /// <param name="lineSkipEnabled"></param>
        /// <returns></returns>
        public API_RETURN_CODE LineSkipEnableChangeCommand(bool lineSkipEnabled)
        {
            return AfterPropertyChanged();
        }

        /// <summary>
        /// 切换跳行扫描
        /// </summary>
        /// <param name="lineSkip"></param>
        /// <returns></returns>
        public API_RETURN_CODE LineSkipValueChangeCommand(ScanLineSkipModel lineSkip)
        {
            return AfterPropertyChanged();
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

        private void PmtReceiveSamples(object sender, short[][] samples, long acquisitionCount)
        {
            try
            {
                PmtSampleData sampleData = new PmtSampleData(samples, acquisitionCount);
                mPmtSampleQueue.TryAdd(sampleData, 50, mCancelToken.Token);
                // Logger.Info(string.Format("Enqueue Pmt Samples [{0}].", acquisitionCount));
            }
            catch (OperationCanceledException)
            {
                Logger.Info(string.Format("Enqueue Pmt Smaples [{0}] Canceled.", acquisitionCount));
                mPmtSampleQueue.CompleteAdding();
            }
        }

        private void ApdReceiveSamples(object sender, int channelIndex, uint[] samples, long acquisitionCount)
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
                return StartScanTask(0, "实时扫描");
            }
            else
            {
                mSequence.GenerateScanCoordinates();
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
                        ScanningTask.ScanInfo.UpdateScanInfo(sampleData.AcquisitionCount);
                        ScanningTask.ConvertPmtSamples(sampleData);
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
                    mApdSampleQueue.TryTake(out ApdSampleData sampleData, 20, mCancelToken.Token);
                }
                catch (OperationCanceledException)
                {
                    Logger.Info(string.Format("Apd Sample Worker Canceled."));
                    break;
                }
                Logger.Info(string.Format("Apd Sample Worker Finished."));
            }
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
