using log4net;
using NationalInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace confocal_core
{
    public class ScanInfo
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int LASER_SWITCH_OFF_DATA_INDEX = -1;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private DateTime m_startTime;           // 扫描开始时间
        private double m_timespan;              // 扫描经过时间
        private double m_fps;                   // 扫描帧率
        private long m_line;                    // 扫描当前所在行
        private long m_frame;                   // 扫描当前所在帧
        private ushort[][] m_nSamples;          // 当前获取到的有效样本
        private int m_405Index;                 // 405nm激光对应的采集通道
        private int m_488Index;                 // 488nm激光对应的采集通道
        private int m_561Index;                 // 561nm激光对应的采集通道
        private int m_640Index;                 // 640nm激光对应的采集通道

        public DateTime StartTime { get { return m_startTime; } set { m_startTime = value; } }
        public Double TimeSpan { get { return m_timespan; } set { m_timespan = value; } }
        public double Fps { get { return m_fps; } set { m_fps = value; } }
        public long CurrentLine { get { return m_line; } set { m_line = value; } }
        public long CurrentFrame { get { return m_frame; } set { m_frame = value; } }
        public ushort[][] NSamples { get { return m_nSamples; } set { m_nSamples = value; } }

        public ScanInfo()
        {
            m_startTime = DateTime.Now;
            m_timespan = 0;
            m_fps = 0;
            m_line = 0;
            m_frame = 0;
            m_nSamples = null;
            m_405Index = LASER_SWITCH_OFF_DATA_INDEX;
            m_488Index = LASER_SWITCH_OFF_DATA_INDEX;
            m_561Index = LASER_SWITCH_OFF_DATA_INDEX;
            m_640Index = LASER_SWITCH_OFF_DATA_INDEX;
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

        public int GetDataIndex(CHAN_ID id)
        {
            switch (id)
            {
                case CHAN_ID.WAVELENGTH_405_NM:
                    return m_405Index;
                case CHAN_ID.WAVELENGTH_488_NM:
                    return m_488Index;
                case CHAN_ID.WAVELENGTH_561_NM:
                    return m_561Index;
                case CHAN_ID.WAVELENGTH_640_NM:
                    return m_640Index;
                default:
                    return LASER_SWITCH_OFF_DATA_INDEX;
            }
        }

    }

    /// <summary>
    /// 扫描任务
    /// </summary>
    public class ScanTask
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private static bool m_scanning = false;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Config m_config;
        private Params m_params;
        private ScanInfo m_scanInfo;
        private DataPool m_scanData;
        private Thread m_convertThread;

        public static bool Scannning { get { return m_scanning; } }

        public ScanTask()
        {
            m_config = confocal_core.Config.GetConfig();
            m_params = Params.GetParams();
            m_scanInfo = new ScanInfo();
            m_scanData = new DataPool();
        }

        public void Config()
        {
            NiCard.CreateInstance().SamplesReceived += new SamplesReceivedEventHandler(ReceiveSamples);
            m_scanInfo.Config();
        }

        public void Start()
        {
            m_scanInfo.StartTime = DateTime.Now;
            m_scanning = true;
        }

        public void Stop()
        {
            NiCard.CreateInstance().SamplesReceived -= ReceiveSamples;
            m_scanning = false;
        }

        public ScanInfo GetScanInfo()
        {
            return m_scanInfo;
        }

        private void ReceiveSamples(object sender, ushort[][] samples)
        {
            Config m_config = confocal_core.Config.GetConfig();
            m_scanInfo.NSamples = samples;

            //SampleData sampleData = new SampleData(m_scanInfo.NSamples, m_scanInfo.CurrentFrame, m_scanInfo.CurrentLine);
            //m_scanData.EnqueueSample(sampleData);

            if (m_config.Debugging)
            {
                double timePerLine = m_scanInfo.TimeSpan / (m_config.GetScanYPoints() * m_scanInfo.CurrentFrame + m_scanInfo.CurrentLine);
                Logger.Info(string.Format("scan info: frame[{0}], line[{1}], number of samples[{2}], time per line:[{3}].", m_scanInfo.CurrentFrame, m_scanInfo.CurrentLine, m_scanInfo.NSamples[0].Length, timePerLine));
            }

            if (++m_scanInfo.CurrentLine % m_config.GetScanYPoints() == 0)
            {
                m_scanInfo.CurrentLine = 0;
                m_scanInfo.CurrentFrame++;

                m_scanInfo.TimeSpan = (DateTime.Now - m_scanInfo.StartTime).TotalSeconds;
                m_scanInfo.Fps = m_scanInfo.CurrentFrame / m_scanInfo.TimeSpan;
                Logger.Info(string.Format("scan info: frame[{0}], timespan[{1}], fps[{2}].", m_scanInfo.CurrentFrame, m_scanInfo.TimeSpan, m_scanInfo.Fps));
            }
        }

        private void ConvertSamples()
        {
            int activatedChannelNum = m_config.GetActivatedChannelNum();
            int sampleCountPerFrame = m_params.SampleCountPerFrame;
            long currentFrame, currentLine;
            int i;
            ushort[,] data = new ushort[activatedChannelNum, sampleCountPerFrame];

            //Marshal.Copy()

            while (m_scanning)
            {
                if (m_scanData.SampleQueueSize() == 0)
                {
                    continue;
                }

                SampleData sample;
                if (!m_scanData.DequeueSample(out sample))
                {
                    Logger.Info(string.Format("dequeue sample data failed."));
                    continue;
                }

                currentFrame = sample.Frame;
                for (i = 0; i < activatedChannelNum; i++)
                {
                    
                }
                                
            }
        }

    }
}
