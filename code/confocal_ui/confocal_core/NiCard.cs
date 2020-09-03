using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public delegate void AiSamplesReceivedEventHandler(object sender, short[][] samples);
    public delegate void CiSamplesReceivedEventHandler(object sender, int channelIndex, int[] samples);
    /// <summary>
    /// NI板卡接口层
    /// </summary>
    public class NiCard
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static NiCard m_card = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        public AiSamplesReceivedEventHandler AiSamplesReceived;
        public CiSamplesReceivedEventHandler CiSamplesReceived;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private SysConfig m_sysConfig;
        private Config m_config;
        private Params m_params;
        private Waver m_waver;

        private Task m_aoTask;
        private Task m_doTask;
        private Task m_aiTask;
        private Task[] m_ciTasks;
        private AnalogUnscaledReader m_aiUnscaledReader;
        private CounterSingleChannelReader[] m_ciChannelReaders;
        ///////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////
        public static NiCard CreateInstance()
        {
            if (m_card == null)
            {
                lock (locker)
                {
                    if (m_card == null)
                    {
                        m_card = new NiCard();
                    }
                }
            }
            return m_card;
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE Start()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            code = ConfigAoTask();
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

            if (m_sysConfig.GetAcqDevice() == ACQ_DEVICE.PMT)
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
            if (m_aoTask != null)
            {
                try
                {
                    m_aoTask.Stop();
                    m_aoTask.Dispose();
                }
                catch (DaqException e)
                {
                    Logger.Error(string.Format("stop ao task exception: [{0}].", e));
                }
                m_aoTask = null;
            }

            if (m_doTask != null)
            {
                try
                {
                    m_doTask.Stop();
                    m_doTask.Dispose();
                }
                catch (DaqException e)
                {
                    Logger.Error(string.Format("stop do task exception: [{0}].", e));
                }
                m_doTask = null;
            }

            if (m_aiTask != null)
            {
                try
                {
                    m_aiTask.Stop();
                    m_aiTask.Dispose();
                }
                catch (DaqException e)
                {
                    Logger.Error(string.Format("stop ai task exception: [{0}].", e));
                }
                m_aiTask = null;
                m_aiUnscaledReader = null;
            }

            if (m_ciTasks != null)
            {
                for (int i = 0; i < m_ciTasks.Length; i++)
                {
                    try
                    {
                        if (m_ciTasks[i] != null)
                        {
                            m_ciTasks[i].Stop();
                            m_ciTasks[i].Dispose();
                            m_ciTasks[i] = null;
                        }
                    }
                    catch (DaqException e)
                    {
                        Logger.Error(string.Format("stop ci task[{0}] exception: [{1}].", i, e));
                    }
                }

                for (int i = 0; i < m_ciChannelReaders.Length; i++)
                {
                    m_ciChannelReaders[i] = null;
                }

                m_ciTasks = null;
                m_ciChannelReaders = null;
            }

        }

        private NiCard()
        {
            m_sysConfig = SysConfig.GetSysConfig();
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_waver = Waver.GetWaver();

            m_aoTask = null;
            m_doTask = null;
            m_aiTask = null;
            m_ciTasks = null;
            m_aiUnscaledReader = null;
            m_ciChannelReaders = null;
        }

        /// <summary>
        /// 配置Ao输出任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigAoTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            try
            {
                m_aoTask = new Task();

                m_aoTask.AOChannels.CreateVoltageChannel(GetAoPhysicalChannelName(), "", -10.0, 10.0, AOVoltageUnits.Volts);
                m_aoTask.Control(TaskAction.Verify);

                m_aoTask.Timing.SampleClockRate = m_params.AoSampleRate;
                m_aoTask.Timing.ConfigureSampleClock("",
                    m_aoTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    m_params.SampleCountPerFrame);

                // 路由Ao Sample Clcok到PFI0
                //if (m_config.Debugging)
                //{
                //    Logger.Info(string.Format("route ao sample clock to PFI0."));
                //    m_aoTask.ExportSignals.SampleClockOutputTerminal = string.Concat("/", NI_CARD_NAME_DEFAULT, "/", "PFI0");
                //}

                //string source = m_sysConfig.GetStartSyncSignal();
                //m_aoTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);

                // 写入波形
                AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(m_aoTask.Stream);
                AnalogWaveform<double>[] waves;
                if (m_config.GetScanMirrorNum() == SCAN_MIRROR_NUM.THREEE)
                {
                    waves = new AnalogWaveform<double>[3];
                    waves[2] = AnalogWaveform<double>.FromArray1D(m_waver.Y2Wave);
                }
                else
                {
                    waves = new AnalogWaveform<double>[2];
                }
                waves[0] = AnalogWaveform<double>.FromArray1D(m_waver.XWave);
                waves[1] = AnalogWaveform<double>.FromArray1D(m_waver.Y1Wave);
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
        /// 配置Do输出任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigDoTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            try
            {
                m_doTask = new Task();

                m_doTask.DOChannels.CreateChannel(GetDoPhysicalChannelName(), "", ChannelLineGrouping.OneChannelForEachLine);
                m_doTask.Control(TaskAction.Verify);

                m_doTask.Timing.SampleClockRate = m_params.DoSampleRate;
                m_doTask.Timing.ConfigureSampleClock("",
                    m_doTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    m_params.SampleCountPerFrame);

                // 设置Do Start Trigger源为Ao Start Trigger[默认]，实现启动同步
                string source = m_sysConfig.GetStartSyncSignal();
                m_doTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);

                // 路由Do Sample Clcok到PFI1
                //if (m_config.Debugging)
                //{
                //    Logger.Info(string.Format("route do sample clock to PFI1."));
                //    m_doTask.ExportSignals.SampleClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI1");
                //}

                m_doTask.Stream.WriteRegenerationMode = WriteRegenerationMode.AllowRegeneration;

                DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(m_doTask.Stream);
                DigitalWaveform wave = DigitalWaveform.FromPort(m_params.DigitalTriggerSamplesPerLine, 0x01);
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
        /// 配置Ai采集任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigAiTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            try
            {
                m_aiTask = new Task();

                m_aiTask.AIChannels.CreateVoltageChannel(GetAiPhysicalChannelName(), "", AITerminalConfiguration.Differential, -5.0, 5.0, AIVoltageUnits.Volts);
                m_aiTask.Control(TaskAction.Verify);

                m_aiTask.Timing.SampleClockRate = m_params.AiSampleRate;
                m_aiTask.Timing.ConfigureSampleClock("",
                    m_aiTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.FiniteSamples,
                    m_params.ValidSampleCountPerLine);

                // 设置Ai Start Trigger源为PFIx，PFIx与Acq Trigger[一般是Do]物理直连，接收Do的输出信号，作为触发
                string source = m_sysConfig.GetPmtTriggerInPfi();
                m_aiTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);
                m_aiTask.Triggers.StartTrigger.Retriggerable = true;        // 设置为允许重触发

                // 路由AI Sample Clcok到PFI2， AI Convert Clock到PFI3
                //if (m_config.Debugging)
                //{
                //    Logger.Info(string.Format("route ai sample clock to FFI2, ai convert clock to PFI3."));
                //    m_aiTask.ExportSignals.SampleClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI2"); ;
                //    m_aiTask.ExportSignals.AIConvertClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI3"); ;
                //}

                m_aiTask.EveryNSamplesReadEventInterval = m_params.ValidSampleCountPerLine;
                m_aiTask.EveryNSamplesRead += new EveryNSamplesReadEventHandler(AiEveryNSamplesRead);

                m_aiUnscaledReader = new AnalogUnscaledReader(m_aiTask.Stream);
                m_aiUnscaledReader.SynchronizeCallbacks = false;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config ai task exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_AI_TASK_EXCEPTION;
            }

            return code;
        }

        /// <summary>
        /// 配置CI采集任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigCiTasks()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            int channelNum = m_config.GetChannelNum();
            m_ciTasks = new Task[channelNum];
            m_ciChannelReaders = new CounterSingleChannelReader[channelNum];
            for (int i = 0; i < channelNum; i++)
            {
                CHAN_ID id = (CHAN_ID)i;
                if (m_config.GetLaserSwitch(id) == LASER_CHAN_SWITCH.ON)
                {
                    code = ConfigCiTask(m_sysConfig.GetApdCiChannel(id), m_sysConfig.GetApdCiSrcPfi(id), m_sysConfig.GetApdCiGatePfi(id), ref m_ciTasks[i], ref m_ciChannelReaders[i]);
                    if (code != API_RETURN_CODE.API_SUCCESS)
                    {
                        return code;
                    }
                }
                else
                {
                    m_ciTasks[i] = null;
                    m_ciChannelReaders[i] = null;
                }
            }
            return code;
        }

        private API_RETURN_CODE ConfigCiTask(string ciChannel, string ciSrc, string ciGate, ref Task ciTask, ref CounterSingleChannelReader ciMultiChannelReader)
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            try
            {
                ciTask = new Task();

                ciTask.CIChannels.CreateCountEdgesChannel(ciChannel, "", CICountEdgesActiveEdge.Rising, 0, CICountEdgesCountDirection.Up);
                ciTask.Control(TaskAction.Verify);

                ciTask.Timing.SampleClockRate = m_params.AoSampleRate;
                ciTask.Timing.ConfigureSampleClock(ciGate,
                    ciTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    m_params.ValidSampleCountPerFrame);

                // 指定CI Channel使用的物理输入终端[APD的脉冲接收端]
                ciTask.CIChannels[0].CountEdgesTerminal = ciSrc;

                // 设置Ci Pause Trigger源为PFIx，PFIx与Acq Trigger[一般是Do]物理直连，接收Do的输出信号，作为触发
                // 低电平使能Pause Trigger,高电平禁能
                // ciTask.Triggers.PauseTrigger.ConfigureDigitalLevelTrigger(ciGate, DigitalLevelPauseTriggerCondition.Low);

                // 设置Arm Start Trigger，使CI与AO、DO同时启动工作
                string source = m_sysConfig.GetStartSyncSignal();
                ciTask.Triggers.ArmStartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeArmStartTriggerEdge.Rising);

                ciTask.EveryNSamplesReadEventInterval = m_params.ValidSampleCountPerLine;
                ciTask.EveryNSamplesRead += new EveryNSamplesReadEventHandler(CiEveryNSamplesRead);

                ciMultiChannelReader = new CounterSingleChannelReader(ciTask.Stream);
                ciMultiChannelReader.SynchronizeCallbacks = false;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config ci task[{0}] exception: [{1}].", ciChannel, e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_CI_TASK_EXCEPTION;
            }

            return code;
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
                if (m_sysConfig.GetAcqDevice() == ACQ_DEVICE.PMT)
                {
                    m_aiTask.Start();
                }
                else
                {
                    for (int i = 0; i < m_ciTasks.Length; i++)
                    {
                        if (m_ciTasks[i] != null)
                        {
                            m_ciTasks[i].Start();
                        }
                    }
                }
                
                m_doTask.Start();
                m_aoTask.Start();
            }
            catch (DaqException e)
            {
                Logger.Error(string.Format("start ni tasks exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_START_TASK_EXCEPTION;
            }

            return code;
        }

        /// <summary>
        /// 采集到N个样本的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AiEveryNSamplesRead(object sender, EveryNSamplesReadEventArgs e)
        {
            try
            {
                // 读取16位原始数据，每一行读取一次
                int channelNum = m_config.GetChannelNum();
                short[,] originSamples = m_aiUnscaledReader.ReadInt16(m_params.ValidSampleCountPerLine);
                AnalogWaveform<short>[] waves = AnalogWaveform<short>.FromArray2D(originSamples);

                short[][] samples = new short[channelNum][];
                for (int i = 0; i < channelNum; i++)
                {
                    samples[i] = m_params.GetLaserAiChannelIndex(i) >= 0 ? waves[m_params.GetLaserAiChannelIndex(i)].GetRawData() : null;
                }

                if (AiSamplesReceived != null)
                {
                    AiSamplesReceived.Invoke(this, samples);
                }
            }
            catch (Exception err)
            {
                Logger.Error(string.Format("every n samples read exception: [{0}].", err));
            }
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

                int[] originSamples = m_ciChannelReaders[index].ReadMultiSampleInt32(m_params.ValidSampleCountPerLine);

                if (CiSamplesReceived != null)
                {
                    CiSamplesReceived.Invoke(this, index, originSamples);
                }
            }
            catch (Exception err)
            {
                Logger.Error(string.Format("every n samples read exception: [{0}].", err));
            }
        }

        /// <summary>
        /// 生成AI物理通道名
        /// </summary>
        /// <returns></returns>
        private string GetAiPhysicalChannelName()
        {
            List<string> activatedChannelNames = new List<string>();
            int channelNum = m_config.GetChannelNum();
            for (int i = 0; i < channelNum; i++)
            {
                CHAN_ID id = (CHAN_ID)i;
                if (m_config.GetLaserSwitch(id) == LASER_CHAN_SWITCH.ON)
                {
                    activatedChannelNames.Add(m_sysConfig.GetPmtAiChannel(id));
                }
            }
            string physicalChannelName = string.Join(",", activatedChannelNames.ToArray());
            Logger.Info(string.Format("ai physical channel name: [{0}].", physicalChannelName));
            return physicalChannelName;
        }

        private string GetAoPhysicalChannelName()
        {
            string physicalChannelName = string.Empty;
            if (m_config.GetScanMirrorNum() == SCAN_MIRROR_NUM.THREEE)
            {
                physicalChannelName = string.Concat(m_sysConfig.GetXGalvoAoChannel(), ",",
                    m_sysConfig.GetYGalvoAoChannel(), ",", m_sysConfig.GetY2GalvoAoChannel());
            }
            else
            {
                physicalChannelName = string.Concat(m_sysConfig.GetXGalvoAoChannel(), ",", m_sysConfig.GetYGalvoAoChannel());
            }
            Logger.Info(string.Format("ao physical channel name: [{0}].", physicalChannelName));
            return physicalChannelName;
        }

        private string GetDoPhysicalChannelName()
        {
            return m_sysConfig.GetAcqTriggerDoLine();
        }

        private string GetCiSampleClockSourceName()
        {
            string aoDeviceName = m_sysConfig.GetXGalvoAoChannel().Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0];
            return string.Concat("/", aoDeviceName, "/ao/SampleClock");
        }

        private int FindCiMultiChannelReaderIndex(object sender)
        {
            for (int i = 0; i < m_ciTasks.Length; i++)
            {
                if (m_ciTasks[i] != null && m_ciTasks[i].Equals(sender))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
