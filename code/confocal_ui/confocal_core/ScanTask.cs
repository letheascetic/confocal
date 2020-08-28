using log4net;
using NationalInstruments;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        private int m_line;                     // 扫描当前所在行
        private long m_frame;                   // 扫描当前所在帧
        private short[][] m_nSamples;           // 当前获取到的有效样本
        private int m_405Index;                 // 405nm激光对应的采集通道
        private int m_488Index;                 // 488nm激光对应的采集通道
        private int m_561Index;                 // 561nm激光对应的采集通道
        private int m_640Index;                 // 640nm激光对应的采集通道

        public DateTime StartTime { get { return m_startTime; } set { m_startTime = value; } }
        public Double TimeSpan { get { return m_timespan; } set { m_timespan = value; } }
        public double Fps { get { return m_fps; } set { m_fps = value; } }
        public int CurrentLine { get { return m_line; } set { m_line = value; } }
        public long CurrentFrame { get { return m_frame; } set { m_frame = value; } }
        public short[][] NSamples { get { return m_nSamples; } set { m_nSamples = value; } }

        public ScanInfo()
        {
            Init();
        }

        public void Config()
        {
            Init();

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

        private void Init()
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
        private int m_taskId;                   // 扫描任务ID
        private string m_taskName;              // 扫描任务名字
        private SysConfig m_sysConfig;
        private Config m_config;                
        private Params m_params;
        private ScanInfo m_scanInfo;
        private DataPool m_scanData;            // 扫描任务数据池
        private Thread m_convertThread;         // 扫描任务数据转换子线程
        private Thread m_imageDataThread;       // 扫描任务图像数据生成子线程
        private Thread m_imageDisplayThread;    // 扫描任务图像[Bitmap]生成子线程
        ///////////////////////////////////////////////////////////////////////////////////////////
        public int TaskId { get { return m_taskId; } }
        public string TaskName { get { return m_taskName; } }
        public bool Scannning { get { return m_scanning; } }
        ///////////////////////////////////////////////////////////////////////////////////////////

        #region public apis

        public ScanTask(int taskId, string taskName)
        {
            m_taskId = taskId;
            m_taskName = taskName;
            m_sysConfig = confocal_core.SysConfig.GetSysConfig();
            m_config = confocal_core.Config.GetConfig();
            m_params = Params.GetParams();
            m_scanInfo = new ScanInfo();
            m_scanData = new DataPool();
            m_convertThread = null;
            m_imageDataThread = null;
            m_imageDisplayThread = null;
        }

        public void Config()
        {
            if (m_sysConfig.GetAcqDevice() == ACQ_DEVICE.PMT)
            {
                NiCard.CreateInstance().SamplesReceived += new SamplesReceivedEventHandler(ReceiveSamples);
            }
            else
            {
                NiCard.CreateInstance().CiSamplesReceived += new CiSamplesReceivedEventHandler(CiReceiveSamples);
            }
            m_scanInfo.Config();
            m_scanData.Config();
        }

        public void Start()
        {
            m_scanning = true;
            m_convertThread = new Thread(ConvertSamplesHandler);
            m_convertThread.Start();
            m_imageDataThread = new Thread(UpdateImageDataHandler);
            m_imageDataThread.Start();
            m_imageDisplayThread = new Thread(UpdateDisplayImageHandler);
            m_imageDisplayThread.Start();
            m_scanInfo.StartTime = DateTime.Now;
        }

        public void Stop()
        {
            if (m_sysConfig.GetAcqDevice() == ACQ_DEVICE.PMT)
            {
                NiCard.CreateInstance().SamplesReceived -= ReceiveSamples;
            }
            else
            {
                NiCard.CreateInstance().CiSamplesReceived -= CiReceiveSamples;
            }
            m_scanning = false;
            if (m_convertThread != null)
            {
                m_convertThread.Join();
                m_convertThread.Abort();
                m_convertThread = null;
            }
            if (m_imageDataThread != null)
            {
                m_imageDataThread.Join();
                m_imageDataThread.Abort();
                m_imageDataThread = null;
            }
            if(m_imageDisplayThread != null)
            {
                m_imageDisplayThread.Join();
                m_imageDisplayThread.Abort();
                m_imageDisplayThread = null;
            }
        }

        public ScanInfo GetScanInfo()
        {
            return m_scanInfo;
        }

        public DataPool GetScanData()
        {
            return m_scanData;
        }

        #endregion

        #region private apis

        /// <summary>
        /// 接收NICard AI中获取到的行数据，生成SampleData，加入队列中
        /// 计算并更新Frame Line Fps TimeSpan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="samples"></param>
        private void ReceiveSamples(object sender, short[][] samples)
        {
            Config m_config = confocal_core.Config.GetConfig();

            m_scanInfo.NSamples = samples;

            PmtSampleData sampleData = new PmtSampleData(m_scanInfo.NSamples, m_scanInfo.CurrentFrame, m_scanInfo.CurrentLine);
            m_scanData.EnqueuePmtSample(sampleData);

            m_scanInfo.TimeSpan = (DateTime.Now - m_scanInfo.StartTime).TotalSeconds;

            //if (m_config.Debugging)
            //{
            //    double timePerLine = m_scanInfo.TimeSpan / (m_config.GetScanYPoints() * m_scanInfo.CurrentFrame + m_scanInfo.CurrentLine + 1);
            //    Logger.Info(string.Format("scan info: frame[{0}], line[{1}], number of samples[{2}], time per line:[{3}].", m_scanInfo.CurrentFrame, m_scanInfo.CurrentLine, m_scanInfo.NSamples[0].Length, timePerLine));
            //}

            if (++m_scanInfo.CurrentLine % m_config.GetScanYPoints() == 0)
            {
                m_scanInfo.CurrentLine = 0;
                m_scanInfo.CurrentFrame++;

                m_scanInfo.Fps = m_scanInfo.CurrentFrame / m_scanInfo.TimeSpan;
                Logger.Info(string.Format("scan info: frame[{0}], timespan[{1}], fps[{2}].", m_scanInfo.CurrentFrame, m_scanInfo.TimeSpan, m_scanInfo.Fps));
            }
        }

        private void CiReceiveSamples(object sender, int channelIndex, int[] samples)
        {
            Config m_config = confocal_core.Config.GetConfig();

            m_scanInfo.TimeSpan = (DateTime.Now - m_scanInfo.StartTime).TotalSeconds;

            if (channelIndex == 0)
            {
                if (++m_scanInfo.CurrentLine % m_config.GetScanYPoints() == 0)
                {
                    m_scanInfo.CurrentLine = 0;
                    m_scanInfo.CurrentFrame++;

                    m_scanInfo.Fps = m_scanInfo.CurrentFrame / m_scanInfo.TimeSpan;
                    Logger.Info(string.Format("scan info: frame[{0}], timespan[{1}], fps[{2}].", m_scanInfo.CurrentFrame, m_scanInfo.TimeSpan, m_scanInfo.Fps));
                }
            }
        }

        /// <summary>
        /// 将行数据转换成帧数据存储
        /// 对于单向扫描，将行数据逐行存储，直到完成一帧数据的存储后，加入到帧数据存储队列
        /// 对于双向扫描：
        /// (a)对偶数行数据截取中间段数据存储到帧数据中
        /// (b)奇数行数据先做反转操作，再根据数据错位值截取相应的中间段数据，并存储到帧数据中
        /// (c)一帧数据存储完成后，加入到帧数据存储队列
        /// </summary>
        private void ConvertSamplesHandler()
        {
            int activatedChannelNum = m_config.GetChannelNum();
            int sampleCountPerLine = m_params.SampleCountPerLine;
            int xSampleCountPerLine = m_config.GetScanXPoints();
            int imageSampleCountPerFrame = m_config.GetScanXPoints() * m_config.GetScanYPoints();
            int scanRows = m_config.GetScanStrategy() == SCAN_STRATEGY.Z_BIDIRECTION ? m_params.ScanRows * 2 : m_params.ScanRows;
            SCAN_STRATEGY scanStrategy = m_config.GetScanStrategy();

            int i, sourceIndex;

            short[][] data = new short[activatedChannelNum][];
            for (i = 0; i < activatedChannelNum; i++)
            {
                data[i] = new short[xSampleCountPerLine];
            }

            while (m_scanning)
            {
                if (m_scanData.PmtSampleQueueSize() == 0)
                {
                    continue;
                }

                if (!m_scanData.DequeuePmtSample(out PmtSampleData sample))
                {
                    Logger.Info(string.Format("dequeue sample data failed."));
                    continue;
                }

                // 采集数据转换
                // 去背景噪声
                sample.Convert();

                // 如果是双向扫描，且当前是奇数行，则该行的数据需要反转
                // 根据错位补偿参数，完成相应的截断
                if (scanStrategy == SCAN_STRATEGY.Z_BIDIRECTION)
                {
                    if ((sample.Line & 0x01) == 0x01)   // 奇数行，需要反转
                    {
                        sourceIndex = m_config.GetScanPixelCompensation() / 2 + m_config.GetScanPixelCalibration();
                        for (i = 0; i < activatedChannelNum; i++)
                        {
                            Array.Reverse(sample.NSamples[i]);
                            Array.Copy(sample.NSamples[i], sourceIndex, data[i], 0, xSampleCountPerLine);
                        }
                    }
                    else
                    {
                        sourceIndex = m_config.GetScanPixelCompensation() / 2 + m_config.GetScanPixelOffset();
                        for (i = 0; i < activatedChannelNum; i++)
                        {
                            Array.Copy(sample.NSamples[i], sourceIndex, data[i], 0, xSampleCountPerLine);
                        }
                    }
                }
                else
                {
                    sourceIndex = m_config.GetScanPixelCompensation() / 2 + m_config.GetScanPixelOffset();
                    for (i = 0; i < activatedChannelNum; i++)
                    {
                        Array.Copy(sample.NSamples[i], sourceIndex, data[i], 0, xSampleCountPerLine);
                    }
                }

                PmtSampleData convertData = new PmtSampleData(data, sample.Frame, sample.Line);
                m_scanData.EnqueuePmtConvertData(convertData);

                // Logger.Info(string.Format("convert info: frame[{0}], line[{1}].", convertData.Frame, convertData.Line));
                if (convertData.Line + 1 == scanRows)
                {
                    Logger.Info(string.Format("convert info: frame[{0}], line[{1}].", convertData.Frame, convertData.Line));
                }

                data = new short[activatedChannelNum][];
                for (i = 0; i < activatedChannelNum; i++)
                {
                    data[i] = new short[xSampleCountPerLine];
                }
            }
            Logger.Info(string.Format("scan task[{0}|{1}] stop, finish convert samples.", m_taskId, m_taskName));
        }

        /// <summary>
        /// 从ConvertData队列中取出行数据，做伪彩色处理，生成图像的原始数据和BGR数据
        /// </summary>
        private void UpdateImageDataHandler()
        {
            int activatedChannelNum = m_config.GetChannelNum();
            int xSampleCountPerLine = m_config.GetScanXPoints();
            int scanRows = m_config.GetScanStrategy() == SCAN_STRATEGY.Z_BIDIRECTION ? m_params.ScanRows * 2 : m_params.ScanRows;

            int i, index;

            while (m_scanning)
            {
                if (m_scanData.PmtConvertQueueSize() == 0)
                {
                    continue;
                }

                if (!m_scanData.DequeuePmtConvertData(out PmtSampleData convertData))
                {
                    Logger.Info(string.Format("dequeue convert data failed."));
                    continue;
                }

                for (i = 0; i < activatedChannelNum; i++)
                {
                    byte[] bgrData = m_scanData.ScanImage.BGRData[i];
                    byte[,] mapping = m_params.ColorMappingArr[i];

                    index = xSampleCountPerLine * convertData.Line;
                    Array.Copy(convertData.NSamples[i], 0, m_scanData.ScanImage.Data[i], index, xSampleCountPerLine);

                    index = index * 3;
                    CImage.Gray16ToBGR24(convertData.NSamples[i], ref bgrData, index, mapping);
                }

                m_scanData.SetImageFrame(convertData.Frame);
                m_scanData.SetImageLine(convertData.Line);

                // Logger.Info(string.Format("update image data: frame[{0}], line[{1}].", convertData.Frame, convertData.Line));

                if (convertData.Line + 1 == scanRows)
                {
                    Logger.Info(string.Format("update image data: frame[{0}], line[{1}].", m_scanData.ScanImage.Frame, m_scanData.ScanImage.Line));
                }
            }
            Logger.Info(string.Format("scan task[{0}|{1}] stop, finish update image data.", m_taskId, m_taskName));
        }

        /// <summary>
        /// 更新bitmap
        /// </summary>
        private void UpdateDisplayImageHandler()
        {
            int activatedChannelNum = m_config.GetChannelNum();
            int scanRows = m_config.GetScanStrategy() == SCAN_STRATEGY.Z_BIDIRECTION ? m_params.ScanRows * 2 : m_params.ScanRows;
            
            Rectangle lockBitsZoom = new Rectangle(0, 0, m_config.GetScanXPoints(), m_config.GetScanYPoints());
            
            double timePerFrame = 1.0f / m_params.Fps;
            double updateNum = timePerFrame / 0.2;
            int updateLines = (int)Math.Ceiling(scanRows / updateNum);

            int i, line = -1;

            while (m_scanning)
            {
                if (line > m_scanData.ScanImage.Line || (m_scanData.ScanImage.Line - line) >= updateLines)
                {
                    line = m_scanData.ScanImage.Line;

                    for (i = 0; i < activatedChannelNum; i++)
                    {
                        byte[,] mapping = m_params.ColorMappingArr[i];
                        m_scanData.ScanImage.UpdateDisplayImage(i, mapping, lockBitsZoom);
                    }

                    Logger.Info(string.Format("update display images: frame[{0}], line[{1}].", m_scanData.ScanImage.Frame, m_scanData.ScanImage.Line));

                    //if (line + 1 == scanRows)
                    //{
                    //    Logger.Info(string.Format("update display images: frame[{0}], line[{1}].", m_scanData.ScanImage.Frame, m_scanData.ScanImage.Line));
                    //}
                }
            }

            Logger.Info(string.Format("scan task[{0}|{1}] stop, finish update display images.", m_taskId, m_taskName));
        }

        #endregion
    }
}
