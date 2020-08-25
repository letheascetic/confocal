using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public class NiApd
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static NiApd m_card = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly string NI_CARD_NAME_DEFAULT = "Dev1";
        private static readonly string TWO_MIRROR_AO_CHANNELS = "ao0:1";
        private static readonly string THREE_MIRROR_AO_CHANNELS = "ao0:2";
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Config m_config;
        private Params m_params;
        private Waver m_waver;

        private Task m_aoTask;
        private Task m_doTask;
        private Task m_ciTask;
        private CounterMultiChannelReader m_ciReader;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static NiApd CreateInstance()
        {
            if (m_card == null)
            {
                lock (locker)
                {
                    if (m_card == null)
                    {
                        m_card = new NiApd();
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

            code = ConfigCiTask();
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

            if (m_ciTask != null)
            {
                try
                {
                    m_ciTask.Stop();
                    m_ciTask.Dispose();
                }
                catch (DaqException e)
                {
                    Logger.Error("stop ci task exception: [{0}].", e);
                }
                m_ciTask = null;
                m_ciReader = null;
            }
        }

        private NiApd()
        {
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_waver = Waver.GetWaver();

            m_aoTask = null;
            m_doTask = null;
            m_ciTask = null;
            m_ciReader = null;
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
                code = API_RETURN_CODE.API_FAILED_NI_CONFIG_DO_TASK_EXCEPTION;
            }
            return code;
        }

        /// <summary>
        /// 配置CI采集任务
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE ConfigCiTask()
        {
            API_RETURN_CODE code = API_RETURN_CODE.API_SUCCESS;

            try
            {
                m_ciTask = new Task();

                m_ciTask.CIChannels.CreateCountEdgesChannel("/Dev1/ctr0", "", CICountEdgesActiveEdge.Rising, 0, CICountEdgesCountDirection.Up);
                m_ciTask.Control(TaskAction.Verify);

                m_ciTask.Timing.SampleClockRate = m_params.AoSampleRate;
                m_ciTask.Timing.ConfigureSampleClock("/Dev1/ao/SampleClock",
                    m_ciTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    m_params.SampleCountPerFrame);

                m_ciTask.EveryNSamplesReadEventInterval = m_params.ValidSampleCountPerLine;
                m_ciTask.EveryNSamplesRead += new EveryNSamplesReadEventHandler(EveryNSamplesRead);

                m_ciReader = new CounterMultiChannelReader(m_ciTask.Stream);
                m_ciReader.SynchronizeCallbacks = false;
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config ci task exception: [{0}].", e));
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
                m_ciTask.Start();
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
                int[,] originSamples = m_ciReader.ReadMultiSampleInt32(m_params.ValidSampleCountPerLine);
                Logger.Info(string.Format("every n samples read: [{0}].", originSamples.Length));
            }
            catch (Exception err)
            {
                Logger.Error(string.Format("every n samples read exception: [{0}].", err));
            }
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
