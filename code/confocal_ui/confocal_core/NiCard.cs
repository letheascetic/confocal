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
    public delegate void SamplesReceivedEventHandler(object sender, short[][] samples);

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
        private static readonly string NI_CARD_NAME_DEFAULT = "Dev1";
        private static readonly string TWO_MIRROR_AO_CHANNELS = "ao0:1";
        private static readonly string THREE_MIRROR_AO_CHANNELS = "ao0:2";
        ///////////////////////////////////////////////////////////////////////////////////////////
        public SamplesReceivedEventHandler SamplesReceived;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Config m_config;
        private Params m_params;
        private Waver m_waver;

        private Task m_aoTask;
        private Task m_doTask;
        private Task m_aiTask;
        private AnalogUnscaledReader m_aiUnscaledReader;
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

            code = ConfigAiTask();
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
                    Logger.Error("stop ao task exception: [{0}].", e);
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
                    Logger.Error("stop do task exception: [{0}].", e);
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
                    Logger.Error("stop ai task exception: [{0}].", e);
                }
                m_aiTask = null;
                m_aiUnscaledReader = null;
            }
        }

        private NiCard()
        {
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_waver = Waver.GetWaver();

            m_aoTask = null;
            m_doTask = null;
            m_aiTask = null;
            m_aiUnscaledReader = null;
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
                if (m_config.Debugging)
                {
                    Logger.Info(string.Format("route ao sample clock to PFI0."));
                    m_aoTask.ExportSignals.SampleClockOutputTerminal = string.Concat("/", NI_CARD_NAME_DEFAULT, "/", "PFI0");
                }
                
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

                m_doTask.Timing.SampleClockRate = m_params.AoSampleRate;
                m_doTask.Timing.ConfigureSampleClock("",
                    m_doTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    m_params.SampleCountPerFrame);

                // 设置Do Start Trigger源为Ao Start Trigger，实现启动同步
                string source = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/ao/StartTrigger");
                m_doTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);

                // 路由Do Sample Clcok到PFI1
                if (m_config.Debugging)
                {
                    Logger.Info(string.Format("route do sample clock to PFI1."));
                    m_doTask.ExportSignals.SampleClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI1");
                }

                m_doTask.Stream.WriteRegenerationMode = WriteRegenerationMode.AllowRegeneration;

                DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(m_doTask.Stream);
                DigitalWaveform wave = DigitalWaveform.FromPort(m_params.DigitalTriggerSamplesPerLine, 0x01);
                writer.WriteWaveform(false, wave);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config do task exception: [{0}].", e));
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_AO_TASK_EXCEPTION;
            }
            return code;
        }

        /// <summary>
        /// 配置Ai输出任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigAiTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            //if (m_config.GetActivatedChannelNum() == 0)
            //{
            //    Logger.Info(string.Format("no channel activated."));
            //    return API_RETURN_CODE.API_FAILED_NI_NO_AI_CHANNEL_ACTIVATED;
            //}

            try
            {
                m_aiTask = new Task();

                m_aiTask.AIChannels.CreateVoltageChannel(GetAiPhysicalChannelName(), "", AITerminalConfiguration.Differential, -5.0, 5.0, AIVoltageUnits.Volts);
                m_aiTask.Control(TaskAction.Verify);

                m_aiTask.Timing.SampleClockRate = m_params.AoSampleRate;
                m_aiTask.Timing.ConfigureSampleClock("",
                    m_aiTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.FiniteSamples,
                    m_params.ValidSampleCountPerLine);

                // 设置Ai Start Trigger源为PFI4，PFI4与P0.0物理直连，接收Do的输出信号，作为触发
                string source = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI4");
                m_aiTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);
                m_aiTask.Triggers.StartTrigger.Retriggerable = true;        // 设置为允许重触发

                // 路由AI Sample Clcok到PFI2， AI Convert Clock到PFI3
                if (m_config.Debugging)
                {
                    Logger.Info(string.Format("route ai sample clock to FFI2, ai convert clock to PFI3."));
                    m_aiTask.ExportSignals.SampleClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI2"); ;
                    m_aiTask.ExportSignals.AIConvertClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI3"); ;
                }

                m_aiTask.EveryNSamplesReadEventInterval = m_params.ValidSampleCountPerLine;
                m_aiTask.EveryNSamplesRead += new EveryNSamplesReadEventHandler(EveryNSamplesRead);

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
        /// 启动所有任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE StartAllTasks()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;
            try
            {
                m_aiTask.Start();
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
        private void EveryNSamplesRead(object sender, EveryNSamplesReadEventArgs e)
        {
            try
            {
                // 读取16位原始数据，每一行读取一次
                short[,] originSamples = m_aiUnscaledReader.ReadInt16(m_params.ValidSampleCountPerLine);
                AnalogWaveform<short>[] waves = AnalogWaveform<short>.FromArray2D(originSamples);

                short[][] samples = new short[waves.Length][];
                for (int i = 0; i < waves.Length; i++)
                {
                    samples[i] = waves[i].GetRawData();
                }

                if (SamplesReceived != null)
                {
                    SamplesReceived.Invoke(this, samples);
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
            //List<string> activatedChannels = new List<string>();
            //if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM) == LASER_CHAN_SWITCH.ON)
            //{
            //    activatedChannels.Add(string.Concat("/", NI_CARD_NAME_DEFAULT, "/ai0"));
            //}
            //if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM) == LASER_CHAN_SWITCH.ON)
            //{
            //    activatedChannels.Add(string.Concat("/", NI_CARD_NAME_DEFAULT, "/ai1"));
            //}
            //if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM) == LASER_CHAN_SWITCH.ON)
            //{
            //    activatedChannels.Add(string.Concat("/", NI_CARD_NAME_DEFAULT, "/ai2"));
            //}
            //if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM) == LASER_CHAN_SWITCH.ON)
            //{
            //    activatedChannels.Add(string.Concat("/", NI_CARD_NAME_DEFAULT, "/ai3"));
            //}
            //string physicalChannelName = string.Join(",", activatedChannels);
            string physicalChannelName = string.Concat("/", NI_CARD_NAME_DEFAULT, "/ai0:3");
            Logger.Info(string.Format("ai physical channel name: [{0}].", physicalChannelName));
            return physicalChannelName;
        }

        private string GetAoPhysicalChannelName()
        {
            string physicalChannelName = string.Concat("/", NI_CARD_NAME_DEFAULT);
            if (m_config.GetScanMirrorNum() == SCAN_MIRROR_NUM.THREEE)
            {

                physicalChannelName = string.Concat(physicalChannelName, "/", THREE_MIRROR_AO_CHANNELS);
            }
            else
            {
                physicalChannelName = string.Concat(physicalChannelName, "/", TWO_MIRROR_AO_CHANNELS);
            }
            Logger.Info(string.Format("ao physical channel name: [{0}].", physicalChannelName));
            return physicalChannelName;
        }

        private string GetDoPhysicalChannelName()
        {
            return string.Concat("/" + NI_CARD_NAME_DEFAULT + "/port0/line0");
        }

    }
}
