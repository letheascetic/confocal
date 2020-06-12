using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace confocal_core
{
    public class ScanInfo
    {
        private DateTime m_startTime;           // 扫描开始时间
        private double m_timespan;              // 扫描经过时间
        private double m_fps;                   // 扫描帧率
        private long m_line;                    // 扫描当前所在行
        private long m_frame;                   // 扫描当前所在帧
        private ushort[,] m_nSamples;           // 当前获取到的有效样本
        private int m_405Index;                 // 405nm激光对应的采集通道
        private int m_488Index;                 // 488nm激光对应的采集通道
        private int m_561Index;                 // 561nm激光对应的采集通道
        private int m_640Index;                 // 640nm激光对应的采集通道

        public DateTime StartTime { get { return m_startTime; } set { m_startTime = value; } }
        public Double TimeSpan { get { return m_timespan; } set { m_timespan = value; } }
        public double Fps { get { return m_fps; } set { m_fps = value; } }
        public long CurrentLine { get { return m_line; } set { m_line = value; } }
        public long CurrentFrame { get { return m_frame; } set { m_frame = value; } }
        public ushort[,] NSamples { get { return m_nSamples; } set { m_nSamples = value; } }

        public ScanInfo()
        {
            m_startTime = DateTime.Now;
            m_timespan = 0;
            m_fps = 0;
            m_line = 0;
            m_frame = 0;
            m_nSamples = null;
            m_405Index = -1;
            m_488Index = -1;
            m_561Index = -1;
            m_640Index = -1;
        }

        public void Config()
        {
            Config config = confocal_core.Config.GetConfig();
            int alreadyUsedIndex = -1;
            if (config.GetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_405Index = ++alreadyUsedIndex;
            }
            if (config.GetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_488Index = ++alreadyUsedIndex;
            }
            if (config.GetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_561Index = ++alreadyUsedIndex;
            }
            if (config.GetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_640Index = ++alreadyUsedIndex;
            }
        }
    }

    /// <summary>
    /// 调度器，单例模式
    /// </summary>
    public class Scheduler
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static Scheduler pScheduler = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        private bool m_scanning;
        private Params m_params;
        private Config m_config;
        private Waver m_waver;
        private NiCard m_card;
        private ScanInfo m_scanInfo;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public bool Scanning { get { return m_scanning; } }
        ///////////////////////////////////////////////////////////////////////////////////////////
        #region public apis

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

        public API_RETURN_CODE StartToScan()
        {
            m_params.Calculate();
            m_waver.Generate();
            API_RETURN_CODE code = m_card.Start();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                Logger.Info(string.Format("start to scan failed: [{0}].", code));
                return code;
            }
            m_scanInfo.Config();
            m_scanning = true;
            Logger.Info(string.Format("start to scan success."));
            return code;
        }

        public void Stop()
        {
            m_card.Stop();
            m_scanning = false;
            Logger.Info(string.Format("stop scanning."));
        }

        public ScanInfo GetScanInfo()
        {
            return m_scanInfo;
        }

        #endregion

        #region private apis    

        private Scheduler()
        {
            m_params = Params.GetParams();
            m_config = Config.GetConfig();
            m_waver = Waver.GetWaver();
            m_card = NiCard.CreateInstance();
            m_scanInfo = new ScanInfo(); ;
            m_scanning = false;

            Init();
        }

        private void ProcessReceivedSamples(object sender, ushort[,] samples)
        {
            m_scanInfo.NSamples = samples;
            if (++m_scanInfo.CurrentLine % m_config.GetScanYPoints() == 0)
            {
                m_scanInfo.CurrentLine = 0;
                m_scanInfo.CurrentFrame++;

                m_scanInfo.TimeSpan = (DateTime.Now - m_scanInfo.StartTime).TotalSeconds;
                m_scanInfo.Fps = m_scanInfo.CurrentFrame / m_scanInfo.TimeSpan;
                Logger.Info(string.Format("scan info: frame[{0}], timespan[{1}], fps[{2}].", m_scanInfo.CurrentFrame, m_scanInfo.TimeSpan, m_scanInfo.Fps));
            }

            if (m_config.Debugging)
            {
                double timePerLine = m_scanInfo.TimeSpan / (m_config.GetScanYPoints() * m_scanInfo.CurrentFrame + m_scanInfo.CurrentLine);
                Logger.Info(string.Format("scan info: frame[{0}], line[{1}], number of samples[{2}], time per line:[{3}].", m_scanInfo.CurrentFrame, m_scanInfo.CurrentLine, m_scanInfo.NSamples.GetLength(1), timePerLine));
            }

            // SampleData sampleData = new SampleData(m_scanInfo.NSamples, scanInfo.CurrentFrame, scanInfo.CurrentLine);
        }

        private void Init()
        {
            m_card.SamplesReceived += new SamplesReceivedEventHandler(ProcessReceivedSamples);
            m_params.Calculate();
        }

        #endregion
    }
}
