using confocal_core.Model;
using confocal_core.ViewModel;
using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Common
{
    /// <summary>
    /// 模拟输入采集事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="samples"></param>
    public delegate void AiSamplesReceivedEventHandler(object sender, ushort[][] samples, long acquisitionCount);
    /// <summary>
    /// 计数器计数事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="channelIndex"></param>
    /// <param name="samples"></param>
    public delegate void CiSamplesReceivedEventHandler(object sender, int channelIndex, uint[] samples, long acquisitionCount);

    /// <summary>
    /// NI板卡接口类
    /// </summary>
    public class NiDaq
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        public AiSamplesReceivedEventHandler AiSamplesReceived;
        public CiSamplesReceivedEventHandler CiSamplesReceived;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private readonly ConfigViewModel mConfig;
        private readonly SequenceModel mSequence;
        private Task mAoTask;
        private Task mDoTask;
        private Task mAiTask;
        private Task[] mCiTasks;
        private AnalogUnscaledReader mAiUnscaledReader;
        private CounterSingleChannelReader[] mCiChannelReaders;
        private readonly int[] mAiChannelIndex;
        private long[] mAcquisitionCount;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public NiDaq()
        {
            mConfig = ConfigViewModel.GetConfig();
            mSequence = SequenceModel.CreateInstance();
            mAoTask = null;
            mDoTask = null;
            mAiTask = null;
            mCiTasks = null;
            mAiUnscaledReader = null;
            mCiChannelReaders = null;
            int channelNum = mConfig.GetChannelNum();
            mAiChannelIndex = Enumerable.Repeat<int>(-1, channelNum).ToArray();
            mAcquisitionCount = Enumerable.Repeat<long>(-1, channelNum).ToArray();
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE Start()
        {
            mAcquisitionCount = Enumerable.Repeat<long>(0, mConfig.GetChannelNum()).ToArray();

            API_RETURN_CODE code = ConfigAoTask();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Stop();
                return code;
            }

            code = ConfigDoTask();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Stop();
                return code;
            }

            if (mConfig.Detector.CurrentDetecor.ID == DetectorTypeModel.PMT)
            {
                code = ConfigAiTask();
            }
            else
            {
                code = ConfigCiTasks();
            }
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Stop();
                return code;
            }

            code = StartAllTasks();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Stop();
                return code;
            }

            return code;
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            if (mAoTask != null)
            {
                try
                {
                    mAoTask.Stop();
                    mAoTask.Dispose();
                }
                catch (DaqException e)
                {
                    Logger.Error(string.Format("stop ao task exception: [{0}].", e));
                }
                mAoTask = null;
            }

            if (mDoTask != null)
            {
                try
                {
                    mDoTask.Stop();
                    mDoTask.Dispose();
                }
                catch (DaqException e)
                {
                    Logger.Error(string.Format("stop do task exception: [{0}].", e));
                }
                mDoTask = null;
            }

            if (mAiTask != null)
            {
                try
                {
                    mAiTask.Stop();
                    mAiTask.Dispose();
                }
                catch (DaqException e)
                {
                    Logger.Error(string.Format("stop ai task exception: [{0}].", e));
                }
                mAiTask = null;
                mAiUnscaledReader = null;
            }

            if (mCiTasks != null)
            {
                for (int i = 0; i < mCiTasks.Length; i++)
                {
                    try
                    {
                        if (mCiTasks[i] != null)
                        {
                            mCiTasks[i].Stop();
                            mCiTasks[i].Dispose();
                            mCiTasks[i] = null;
                        }
                    }
                    catch (DaqException e)
                    {
                        Logger.Error(string.Format("stop ci task[{0}] exception: [{1}].", i, e));
                    }
                }

                for (int i = 0; i < mCiChannelReaders.Length; i++)
                {
                    mCiChannelReaders[i] = null;
                }

                mCiTasks = null;
                mCiChannelReaders = null;
            }

        }

        /// <summary>
        /// 启动所有任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE StartAllTasks()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            try
            {
                if (mConfig.Detector.CurrentDetecor.ID == DetectorTypeModel.PMT)
                {
                    mAiTask.Start();
                }
                else
                {
                    for (int i = 0; i < mCiTasks.Length; i++)
                    {
                        if (mCiTasks[i] != null)
                        {
                            mCiTasks[i].Start();
                        }
                    }
                }

                mDoTask.Start();
                mAoTask.Start();
            }
            catch (DaqException e)
            {
                Logger.Error(string.Format("start ni tasks exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_START_TASK_EXCEPTION;
            }

            return code;
        }

        /// <summary>
        /// 配置AO任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigAoTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            try
            {
                mAoTask = new Task();

                mAoTask.AOChannels.CreateVoltageChannel(GetAoPhysicalChannelName(), "", -10.0, 10.0, AOVoltageUnits.Volts);
                mAoTask.Control(TaskAction.Verify);

                mAoTask.Timing.SampleClockRate = mSequence.OutputSampleRate;
                mAoTask.Timing.ConfigureSampleClock("",
                    mAoTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    mSequence.OutputSampleCountPerFrame);

                //string source = mSettings.GetStartSyncSignal();
                //m_aoTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);

                // 写入波形
                AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(mAoTask.Stream);
                AnalogWaveform<double>[] waves;
                if (mConfig.SelectedScannerHead.ID == ScannerHeadModel.THREE_SCANNERS)
                {
                    waves = new AnalogWaveform<double>[3];
                    waves[2] = AnalogWaveform<double>.FromArray1D(mSequence.Y2Wave);
                }
                else
                {
                    waves = new AnalogWaveform<double>[2];
                }
                waves[0] = AnalogWaveform<double>.FromArray1D(mSequence.XWave);
                waves[1] = AnalogWaveform<double>.FromArray1D(mSequence.Y1Wave);
                writer.WriteWaveform(false, waves);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config ao task exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_AO_TASK_EXCEPTION;
            }
            return code;
        }

        /// <summary>
        /// 配置Do任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigDoTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            try
            {
                mDoTask = new Task();

                mDoTask.DOChannels.CreateChannel(GetDoPhysicalChannelName(), "", ChannelLineGrouping.OneChannelForEachLine);
                mDoTask.Control(TaskAction.Verify);

                mDoTask.Timing.SampleClockRate = mSequence.OutputSampleRate;
                mDoTask.Timing.ConfigureSampleClock("",
                    mDoTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    mSequence.OutputSampleCountPerFrame);

                // 设置Do Start Trigger源为Ao Start Trigger[默认]，实现启动同步
                string source = mConfig.Detector.StartTrigger;
                mDoTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);

                mDoTask.Stream.WriteRegenerationMode = WriteRegenerationMode.AllowRegeneration;

                DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(mDoTask.Stream);
                DigitalWaveform wave = DigitalWaveform.FromPort(mSequence.TriggerWave, 0x01);
                writer.WriteWaveform(false, wave);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config do task exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_DO_TASK_EXCEPTION;
            }
            return code;
        }

        /// <summary>
        /// 配置CI任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigCiTasks()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            int channelNum = mConfig.GetChannelNum();
            mCiTasks = new Task[channelNum];
            mCiChannelReaders = new CounterSingleChannelReader[channelNum];
            for (int i = 0; i < channelNum; i++)
            {
                if (mConfig.ScanChannels[i].Activated)
                {
                    ApdChannelModel apdChannel = mConfig.Detector.FindApdChannel(i);
                    code = ConfigCiTask(apdChannel.CiSource, apdChannel.CiChannel, mConfig.Detector.TriggerReceive, ref mCiTasks[i], ref mCiChannelReaders[i]);
                    if (code != API_RETURN_CODE.API_SUCCESS)
                    {
                        return code;
                    }
                }
                else
                {
                    mCiTasks[i] = null;
                    mCiChannelReaders[i] = null;
                }
            }
            return code;
        }

        /// <summary>
        /// 配置CI任务
        /// </summary>
        /// <param name="ciChannel"></param>
        /// <param name="ciSrc"></param>
        /// <param name="ciClock"></param>
        /// <param name="ciTask"></param>
        /// <param name="ciMultiChannelReader"></param>
        /// <returns></returns>
        private API_RETURN_CODE ConfigCiTask(string ciChannel, string ciSrc, string ciClock, ref Task ciTask, ref CounterSingleChannelReader ciMultiChannelReader)
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            try
            {
                ciTask = new Task();

                ciTask.CIChannels.CreateCountEdgesChannel(ciChannel, "", CICountEdgesActiveEdge.Rising, 0, CICountEdgesCountDirection.Up);
                ciTask.Control(TaskAction.Verify);

                ciTask.Timing.SampleClockRate = mSequence.InputSampleRate;
                ciTask.Timing.ConfigureSampleClock(ciClock,
                    ciTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    mSequence.InputSampleCountPerFrame);

                ciTask.Stream.ConfigureInputBuffer(mSequence.InputSampleCountPerFrame);

                // CIDataTransferMechanism x = ciTask.CIChannels.All.DataTransferMechanism;

                // 指定CI Channel使用的物理输入终端[APD的脉冲接收端]
                ciTask.CIChannels[0].CountEdgesTerminal = ciSrc;

                // 设置Ci Pause Trigger源为PFIx，PFIx与Acq Trigger[一般是Do]物理直连，接收Do的输出信号，作为触发
                // 低电平使能Pause Trigger,高电平禁能
                // ciTask.Triggers.PauseTrigger.ConfigureDigitalLevelTrigger(ciGate, DigitalLevelPauseTriggerCondition.Low);

                // 设置Arm Start Trigger，使CI与AO、DO同时启动工作
                string source = mConfig.Detector.StartTrigger;
                ciTask.Triggers.ArmStartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeArmStartTriggerEdge.Rising);

                ciTask.EveryNSamplesReadEventInterval = mSequence.InputSampleCountPerAcquisition;
                ciTask.EveryNSamplesRead += new EveryNSamplesReadEventHandler(CiEveryNSamplesRead);

                ciMultiChannelReader = new CounterSingleChannelReader(ciTask.Stream)
                {
                    SynchronizeCallbacks = false
                };
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config ci task[{0}] exception: [{1}].", ciChannel, e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_CI_TASK_EXCEPTION;
            }

            return code;
        }

        /// <summary>
        /// 配置AI任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigAiTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            GenerateAiChannelIndex();

            try
            {
                mAiTask = new Task();

                mAiTask.AIChannels.CreateVoltageChannel(GetAiPhysicalChannelName(), "", AITerminalConfiguration.Differential, -5.0, 5.0, AIVoltageUnits.Volts);

                mAiTask.Control(TaskAction.Verify);

                mAiTask.Timing.SampleClockRate = mSequence.InputSampleRate;
                mAiTask.Timing.ConfigureSampleClock("",
                    mAiTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.FiniteSamples,
                    mSequence.InputSampleCountPerAcquisition);

                // 设置Ai Start Trigger源为PFIx，PFIx与Acq Trigger[一般是Do]物理直连，接收Do的输出信号，作为触发
                string source = mConfig.Detector.TriggerReceive;
                mAiTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);
                mAiTask.Triggers.StartTrigger.Retriggerable = true;        // 设置为允许重触发

                // mAiTask.AIChannels.All.DataTransferMechanism = AIDataTransferMechanism.Dma;

                // 路由AI Sample Clcok到PFI2， AI Convert Clock到PFI3
                //if (m_config.Debugging)
                //{
                //    Logger.Info(string.Format("route ai sample clock to FFI2, ai convert clock to PFI3."));
                //    mAiTask.ExportSignals.SampleClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI2"); ;
                //    mAiTask.ExportSignals.AIConvertClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI3"); ;
                //}

                mAiTask.EveryNSamplesReadEventInterval = mSequence.InputSampleCountPerAcquisition;
                mAiTask.EveryNSamplesRead += new EveryNSamplesReadEventHandler(AiEveryNSamplesRead);

                mAiUnscaledReader = new AnalogUnscaledReader(mAiTask.Stream)
                {
                    SynchronizeCallbacks = false
                };
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config ai task exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_AI_TASK_EXCEPTION;
            }

            return code;
        }

        private void CiEveryNSamplesRead(object sender, EveryNSamplesReadEventArgs e)
        {
            try
            {
                int index = FindCiMultiChannelReaderIndex(sender);
                if (index < 0)
                {
                    Logger.Warn(string.Format("no matching ci task found: [{0}].", sender));
                    return;
                }

                uint[] originSamples = mCiChannelReaders[index].ReadMultiSampleUInt32(mSequence.InputSampleCountPerAcquisition);

                if (CiSamplesReceived != null)
                {
                    CiSamplesReceived.Invoke(this, index, originSamples, ++mAcquisitionCount[index]);
                }
            }
            catch (Exception err)
            {
                Logger.Error(string.Format("every n samples read exception: [{0}].", err));
            }
        }

        private void AiEveryNSamplesRead(object sender, EveryNSamplesReadEventArgs e)
        {
            try
            {
                // 读取16位原始数据，每次读取单次采集的像素数
                int channelNum = mConfig.GetChannelNum();
                ushort[,] originSamples = mAiUnscaledReader.ReadUInt16(mSequence.InputSampleCountPerAcquisition);
                AnalogWaveform<ushort>[] waves = AnalogWaveform<ushort>.FromArray2D(originSamples);

                ushort[][] samples = new ushort[channelNum][];
                for (int i = 0; i < channelNum; i++)
                {
                    if (GetLaserAiChannelIndex(i) >= 0)
                    {
                        samples[i] = waves[GetLaserAiChannelIndex(i)].GetRawData();
                        mAcquisitionCount[i]++;
                    }
                    else
                    {
                        samples[i] = null;
                    }
                }

                if (AiSamplesReceived != null)
                {
                    AiSamplesReceived.Invoke(this, samples, mAcquisitionCount.Where(p => p>=0).First());
                }
            }
            catch (Exception err)
            {
                Logger.Error(string.Format("every n samples read exception: [{0}].", err));
            }
        }

        /// <summary>
        /// 获取AO物理通道名称
        /// </summary>
        /// <returns></returns>
        private string GetAoPhysicalChannelName()
        {
            string physicalChannelName;
            if (mConfig.SelectedScannerHead.ID == ScannerHeadModel.THREE_SCANNERS)
            {
                physicalChannelName = string.Concat(mConfig.GalvoProperty.XGalvoAoChannel, ",", mConfig.GalvoProperty.YGalvoAoChannel, ",", mConfig.GalvoProperty.Y2GalvoAoChannel);
            }
            else
            {
                physicalChannelName = string.Concat(mConfig.GalvoProperty.XGalvoAoChannel, ",", mConfig.GalvoProperty.YGalvoAoChannel);
            }
            Logger.Info(string.Format("ao physical channel name: [{0}].", physicalChannelName));
            return physicalChannelName;
        }

        /// <summary>
        /// 获取DO物理通道名称
        /// </summary>
        /// <returns></returns>
        private string GetDoPhysicalChannelName()
        {
            return mConfig.Detector.TriggerSignal;
        }

        /// <summary>
        /// 获取CI物理通道名称
        /// </summary>
        /// <returns></returns>
        private string GetCiSampleClockSourceName()
        {
            string aoDeviceName = mConfig.GalvoProperty.XGalvoAoChannel.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0];
            return string.Concat("/", aoDeviceName, "/ao/SampleClock");
        }

        /// <summary>
        /// 获取AI物理通道名
        /// </summary>
        /// <returns></returns>
        private string GetAiPhysicalChannelName()
        {
            List<string> activatedChannelNames = new List<string>();
            int channelNum = mConfig.GetChannelNum();
            for (int i = 0; i < channelNum; i++)
            {
                if (mConfig.FindScanChannel(i).Activated)
                {
                    activatedChannelNames.Add(mConfig.Detector.FindPmtChannel(i).AiChannel);
                }
            }
            string physicalChannelName = string.Join(",", activatedChannelNames.ToArray());
            Logger.Info(string.Format("ai physical channel name: [{0}].", physicalChannelName));
            return physicalChannelName;
        }

        /// <summary>
        /// 使用PMT时，生成激活的通道对应的模拟通道序列号
        /// </summary>
        public void GenerateAiChannelIndex()
        {
            int index = -1;
            int channelNum = mConfig.GetChannelNum();
            for (int i = 0; i < channelNum; i++)
            {
                mAiChannelIndex[i] = mConfig.ScanChannels[i].Activated ? ++index : -1;
            }
        }

        public int GetLaserAiChannelIndex(int channelId)
        {
            return mAiChannelIndex[channelId];
        }

        private int FindCiMultiChannelReaderIndex(object sender)
        {
            for (int i = 0; i < mCiTasks.Length; i++)
            {
                if (mCiTasks[i] != null && mCiTasks[i].Equals(sender))
                {
                    return i;
                }
            }
            return -1;
        }

        public static string[] GetDeviceNames()
        {
            return DaqSystem.Local.Devices;
        }

        public static string GetDeviceType(string device)
        {
            return DaqSystem.Local.LoadDevice(device).ProductType;
        }

        public static string[] GetAoChannels()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AO, PhysicalChannelAccess.External);
        }

        public static string[] GetAiChannels()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.AI, PhysicalChannelAccess.External);
        }

        public static string[] GetDoLines()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.DOLine, PhysicalChannelAccess.External);
        }

        public static string[] GetCiChannels()
        {
            return DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.CI, PhysicalChannelAccess.External);
        }

        public static string[] GetPFIs()
        {
            string[] terminals = DaqSystem.Local.GetTerminals(TerminalTypes.Basic);
            List<string> pfis = new List<string>();

            foreach (string terminal in terminals)
            {
                if (terminal.Contains("/PFI"))
                {
                    pfis.Add(terminal);
                }
            }
            return pfis.ToArray();
        }

        public static string[] GetStartSyncSignals()
        {
            string[] terminals = DaqSystem.Local.GetTerminals(TerminalTypes.Basic);
            List<string> pfis = new List<string>();

            foreach (string terminal in terminals)
            {
                if (terminal.Contains("/StartTrigger"))
                {
                    pfis.Add(terminal);
                }
            }
            return pfis.ToArray();
        }

    }
}
