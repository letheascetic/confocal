﻿using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public class ScanInfo
    {
        private DateTime m_startTime;           // 扫描开始时间
        private double m_timespan;              // 扫描经过时间
        private double m_fps;                   // 扫描帧率
        private long m_line;                    // 扫描当前所在行
        private long m_frame;                   // 扫描当前所在帧
        private double[,] m_nSamples;           // 当前获取到的有效样本

        public DateTime StartTime { get { return m_startTime; } }
        public Double TimeSpan { get { return m_timespan; } set { m_timespan = value; } }
        public double Fps { get { return m_fps; } set { m_fps = value; } }
        public long CurrentLine { get { return m_line; } set { m_line = value; } }
        public long CurrentFrame { get { return m_frame; } set { m_frame = value; } }
        public double[,] NSamples { get { return m_nSamples; } set { m_nSamples = value; } }

        public ScanInfo()
        {
            m_startTime = DateTime.Now;
            m_timespan = 0;
            m_fps = 0;
            m_line = 0;
            m_frame = 0;
            m_nSamples = null;
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
        private static readonly string NI_CARD_NAME_DEFAULT = "Dev1";
        private static readonly string TWO_MIRROR_AO_CHANNELS = "ai0:1";
        private static readonly string THREE_MIRROR_AO_CHANNELS = "ai0:2";
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Config m_config;
        private Params m_params;
        private Waver m_waver;

        private Task m_aoTask;
        private Task m_doTask;
        private Task m_aiTask;
        private AsyncCallback m_aiTaskCallback;
        private AnalogMultiChannelReader m_aiReader;
        private ScanInfo m_scanInfo;

        private double[,] tempData;
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
            m_scanInfo = new ScanInfo();

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

                string physicalChannelName = string.Concat("/", NI_CARD_NAME_DEFAULT);
                if (m_config.GetScanMirrorNum() == SCAN_MIRROR_NUM.THREEE)
                {
                    physicalChannelName = string.Concat(physicalChannelName, "/", THREE_MIRROR_AO_CHANNELS);
                }
                else
                {
                    physicalChannelName = string.Concat(physicalChannelName, "/", TWO_MIRROR_AO_CHANNELS);
                }

                m_aoTask.AOChannels.CreateVoltageChannel(physicalChannelName, "", -10.0, 10.0, AOVoltageUnits.Volts);
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

                string pyhsicalChannelName = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/port0/line0");
                m_doTask.DOChannels.CreateChannel(pyhsicalChannelName, "", ChannelLineGrouping.OneChannelForEachLine);
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
                if (DebugFlag)
                {
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
            try
            {
                m_aiTask = new Task();

                m_aiTask.AIChannels.CreateVoltageChannel(GetAiPhysicalChannelName(), "", AITerminalConfiguration.Differential, -10.0, 10.0, AIVoltageUnits.Volts);
                m_aiTask.Control(TaskAction.Verify);

                m_aiTask.Timing.SampleClockRate = m_params.AoSampleRate;
                m_aiTask.Timing.ConfigureSampleClock("",
                    m_aiTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.FiniteSamples,
                    m_params.ValidSampleCountPerLine);

                // for test
                //double x = m_aiTask.Timing.AIConvertMaximumRate;
                //double delay = m_aiTask.Timing.DelayFromSampleClock;
                //DelayFromSampleClockUnits delayunits = m_aiTask.Timing.DelayFromSampleClockUnits;
                //double y = m_aiTask.Timing.AIConvertRate;

                // 设置Ai Start Trigger源为PFI4，PFI4与P0.0物理直连，接收Do的输出信号，作为触发
                string source = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI4");
                m_aiTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(source, DigitalEdgeStartTriggerEdge.Rising);
                m_aiTask.Triggers.StartTrigger.Retriggerable = true;        // 设置为允许重触发

                // 路由Do Sample Clcok到PFI1
                if (DebugFlag)
                {
                    m_aiTask.ExportSignals.SampleClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI2"); ;
                    m_aiTask.ExportSignals.AIConvertClockOutputTerminal = string.Concat("/" + NI_CARD_NAME_DEFAULT + "/PFI3"); ;
                }

                m_aiTask.EveryNSamplesReadEventInterval = m_params.ValidSampleCountPerLine;
                m_aiTask.EveryNSamplesRead += new EveryNSamplesReadEventHandler(EveryNSamplesRead);

                //m_aiTaskCallback = new AsyncCallback(AnalogInCallback);
                m_aiReader = new AnalogMultiChannelReader(m_aiTask.Stream);
                m_aiReader.SynchronizeCallbacks = false;

                int activatedChannelNum = m_config.GetActivatedChannelNum();
                tempData = new double[activatedChannelNum, m_params.ValidSampleCountPerLine];
                m_scanInfo = new ScanInfo();

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

        private void AnalogInCallback(IAsyncResult ar)
        {
            try
            {
                m_scanInfo.NSamples = m_aiReader.EndMemoryOptimizedReadMultiSample(ar, out int readSampleCount);
                ScanInfo dataInfo = ar.AsyncState as ScanInfo;
                if (dataInfo != null)
                {
                    if (++m_scanInfo.CurrentLine % m_config.GetScanYPoints() == 0)
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
                    m_scanInfo,
                    m_scanInfo.NSamples);
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("analog in callback exception: [{0}].", e));
            }
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
                m_aiReader.MemoryOptimizedReadMultiSample(m_params.ValidSampleCountPerLine, ref tempData, out int readSampleCount);
                m_scanInfo.NSamples = tempData;

                if (m_scanInfo != null)
                {
                    if (++m_scanInfo.CurrentLine % m_config.GetScanYPoints() == 0)
                    {
                        m_scanInfo.CurrentLine = 0;
                        m_scanInfo.CurrentFrame++;

                        m_scanInfo.TimeSpan = (DateTime.Now - m_scanInfo.StartTime).TotalSeconds;
                        m_scanInfo.Fps = m_scanInfo.CurrentFrame / m_scanInfo.TimeSpan;

                        Logger.Info(string.Format("scan info: frame[{0}], timespan[{1}], fps[{2}].",
                            m_scanInfo.CurrentFrame, m_scanInfo.TimeSpan, m_scanInfo.Fps));
                    }

                    double timePerLine = m_scanInfo.TimeSpan / (m_config.GetScanYPoints() * m_scanInfo.CurrentFrame + m_scanInfo.CurrentLine);
                    Logger.Info(string.Format("scan info: frame[{0}], line[{1}], number of samples[{2}], time per line:[{3}].",
                        m_scanInfo.CurrentFrame, m_scanInfo.CurrentLine, readSampleCount, timePerLine));
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
            List<string> activatedChannels = new List<string>();
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM) == LASER_CHAN_SWITCH.ON)
            {
                activatedChannels.Add(string.Concat("/", NI_CARD_NAME_DEFAULT, "/ai0"));
            }
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM) == LASER_CHAN_SWITCH.ON)
            {
                activatedChannels.Add(string.Concat("/", NI_CARD_NAME_DEFAULT, "/ai1"));
            }
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM) == LASER_CHAN_SWITCH.ON)
            {
                activatedChannels.Add(string.Concat("/", NI_CARD_NAME_DEFAULT, "/ai2"));
            }
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM) == LASER_CHAN_SWITCH.ON)
            {
                activatedChannels.Add(string.Concat("/", NI_CARD_NAME_DEFAULT, "/ai3"));
            }
            return string.Join(",", activatedChannels);
        }

    }
}
