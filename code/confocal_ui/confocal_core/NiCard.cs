using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public class DataInfo
    {
        public DateTime StartTime { get; set; }
        public double TimeSpan { get; set; }
        public double Fps { get; set; }
        public long CurrentLine { get; set; }
        public long CurrentFrame { get; set; } 
        public double[,] Data { get; set; }

        public DataInfo()
        {
            GetReady();
        }

        public void GetReady()
        {
            Fps = 0.0;
            CurrentLine = 0;
            CurrentFrame = 0;
            Data = null;
            TimeSpan = 0.0;
            StartTime = DateTime.Now;
        }

    }

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

        ///////////////////////////////////////////////////////////////////////////////////////////
        private Config m_config;
        private Params m_params;
        private Waver m_waver;

        private Task m_aoTask;
        private Task m_doTask;
        private Task m_aiTask;
        private AsyncCallback m_aiTaskCallback;
        private AnalogMultiChannelReader m_aiReader;
        private DataInfo m_dataInfo;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public bool DebugFlag { get; set; }
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
                m_aiReader = null;
                m_aiTaskCallback = null;
                m_dataInfo.Data = null;
                m_dataInfo.CurrentFrame = 0;
                m_dataInfo.CurrentLine = 0;
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
            m_aiReader = null;
            m_aiTaskCallback = null;
            m_dataInfo = new DataInfo();

            DebugFlag = true;
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
                m_aoTask.AOChannels.CreateVoltageChannel("/Dev1/ao0:2", "", -10.0, 10.0, AOVoltageUnits.Volts);
                m_aoTask.Control(TaskAction.Verify);

                m_aoTask.Timing.SampleClockRate = m_params.AoSampleRate;
                m_aoTask.Timing.ConfigureSampleClock("",
                    m_aoTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    m_params.SampleCountPerFrame);

                // 路由Ao Sample Clcok到PFI0
                if (DebugFlag)
                {
                    m_aoTask.ExportSignals.SampleClockOutputTerminal = "/Dev1/PFI0";
                }

                // 写入波形
                AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(m_aoTask.Stream);
                AnalogWaveform<double>[] waves = new AnalogWaveform<double>[3];
                waves[0] = AnalogWaveform<double>.FromArray1D(m_waver.XWave);
                waves[1] = AnalogWaveform<double>.FromArray1D(m_waver.Y1Wave);
                waves[2] = AnalogWaveform<double>.FromArray1D(m_waver.Y2Wave);
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
                m_doTask.DOChannels.CreateChannel("Dev1/port0/line0", "", ChannelLineGrouping.OneChannelForEachLine);

                m_doTask.Control(TaskAction.Verify);

                m_doTask.Timing.SampleClockRate = m_params.AoSampleRate;
                m_doTask.Timing.ConfigureSampleClock("",
                    m_doTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    m_params.SampleCountPerFrame);

                // 设置Do Start Trigger源为Ao Start Trigger，实现启动同步
                m_doTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("/Dev1/ao/StartTrigger", DigitalEdgeStartTriggerEdge.Rising);

                // 路由Do Sample Clcok到PFI1
                if (DebugFlag)
                {
                    m_doTask.ExportSignals.SampleClockOutputTerminal = "/Dev1/PFI1";
                }

                m_doTask.Stream.WriteRegenerationMode = WriteRegenerationMode.AllowRegeneration;

                DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(m_doTask.Stream);
                DigitalWaveform wave = DigitalWaveform.FromPort(m_params.DigitalTriggerSamplesPerLine, 0x01);
                writer.WriteWaveform(false, wave);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("config ao task exception: [{0}].", e));
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
            try
            {
                m_aiTask = new Task();
                m_aiTask.AIChannels.CreateVoltageChannel("/Dev1/ai0","", AITerminalConfiguration.Differential, -10.0, 10.0, AIVoltageUnits.Volts);
                m_aiTask.Control(TaskAction.Verify);

                m_aiTask.Timing.SampleClockRate = m_params.AoSampleRate;
                m_aiTask.Timing.ConfigureSampleClock("",
                    m_aiTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.FiniteSamples,
                    m_params.ValidSampleCountPerLine);

                double x = m_aiTask.Timing.AIConvertMaximumRate;
                double y = m_aiTask.Timing.AIConvertRate;

                // 设置Ai Start Trigger源为PFI4，PFI4与P0.0物理直连，接收Do的输出信号，作为触发
                m_aiTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("/Dev1/PFI4", DigitalEdgeStartTriggerEdge.Rising);
                m_aiTask.Triggers.StartTrigger.Retriggerable = true;

                m_aiTaskCallback = new AsyncCallback(AnalogInCallback);
                m_aiReader = new AnalogMultiChannelReader(m_aiTask.Stream);
                m_aiReader.SynchronizeCallbacks = false;

                m_dataInfo.GetReady();
                m_dataInfo.Data = new double[1, m_params.ValidSampleCountPerLine];
                m_aiReader.BeginMemoryOptimizedReadMultiSample(
                    m_params.ValidSampleCountPerLine, 
                    m_aiTaskCallback,
                    m_dataInfo,
                    m_dataInfo.Data);

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

        private void AnalogInCallback(IAsyncResult ar)
        {
            try
            {
                m_dataInfo.Data = m_aiReader.EndMemoryOptimizedReadMultiSample(ar, out int readSampleCount);
                DataInfo dataInfo = ar.AsyncState as DataInfo;
                if (dataInfo != null)
                {
                    if (++dataInfo.CurrentLine % m_config.GetScanYPoints() == 0)
                    {
                        dataInfo.CurrentLine = 0;
                        dataInfo.CurrentFrame++;

                        dataInfo.TimeSpan = (DateTime.Now - dataInfo.StartTime).TotalSeconds;
                        dataInfo.Fps = dataInfo.CurrentFrame / dataInfo.TimeSpan;

                        Logger.Info(string.Format("scan info: frame[{0}], timespan[{1}], fps[{2}].", 
                            dataInfo.CurrentFrame, dataInfo.TimeSpan, dataInfo.Fps));
                    }

                    double timePerLine = dataInfo.TimeSpan / (m_config.GetScanYPoints() * dataInfo.CurrentFrame + dataInfo.CurrentLine);
                    Logger.Info(string.Format("scan info: frame[{0}], line[{1}], number of samples[{2}], time per line:[{3}].",
                        dataInfo.CurrentFrame, dataInfo.CurrentLine, readSampleCount, timePerLine));
                }

                m_aiReader.BeginMemoryOptimizedReadMultiSample(
                    m_params.ValidSampleCountPerLine,
                    m_aiTaskCallback,
                    m_dataInfo,
                    m_dataInfo.Data);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("analog in callback exception: [{0}].", e));
            }
        }

    }
}
