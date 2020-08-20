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
        private Config m_config;                
        private Params m_params;
        private ScanInfo m_scanInfo;
        private DataPool m_scanData;            // 扫描任务数据池
        private Thread m_convertThread;         // 扫描任务数据转换子线程
        private Thread m_displayImageThread;    // 扫描任务图像显示子线程
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
            m_config = confocal_core.Config.GetConfig();
            m_params = Params.GetParams();
            m_scanInfo = new ScanInfo();
            m_scanData = new DataPool();
            m_convertThread = null;
            m_displayImageThread = null;
        }

        public void Config()
        {
            NiCard.CreateInstance().SamplesReceived += new SamplesReceivedEventHandler(ReceiveSamples);
            m_scanInfo.Config();
            m_scanData.Config();
        }

        public void Start()
        {
            m_scanning = true;
            m_convertThread = new Thread(ConvertSamplesHandler2);
            m_convertThread.Start();
            m_displayImageThread = new Thread(UpdateDisplayImageHandler2);
            m_displayImageThread.Start();
            m_scanInfo.StartTime = DateTime.Now;
        }

        public void Stop()
        {
            NiCard.CreateInstance().SamplesReceived -= ReceiveSamples;
            m_scanning = false;
            if (m_convertThread != null)
            {
                m_convertThread.Join();
                m_convertThread.Abort();
                m_convertThread = null;
            }
            if (m_displayImageThread != null)
            {
                m_displayImageThread.Join();
                m_displayImageThread.Abort();
                m_displayImageThread = null;
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

        private void ReceiveSamples(object sender, short[][] samples)
        {
            Config m_config = confocal_core.Config.GetConfig();

            m_scanInfo.NSamples = samples;

            SampleData sampleData = new SampleData(m_scanInfo.NSamples, m_scanInfo.CurrentFrame, m_scanInfo.CurrentLine);
            m_scanData.EnqueueSample(sampleData);

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

        /// <summary>
        /// 将行数据转换成帧数据存储
        /// 对于单向扫描，将行数据逐行存储，直到完成一帧数据的存储后，加入到帧数据存储队列
        /// 对于双向扫描：
        /// (a)对偶数行数据截取中间段数据存储到帧数据中
        /// (b)奇数行数据先做反转操作，再根据数据错位值截取相应的中间段数据，并存储到帧数据中
        /// (c)一帧数据存储完成后，加入到帧数据存储队列
        /// </summary>
        //private void ConvertSamplesHandler()
        //{
        //    //int activatedChannelNum = m_config.GetActivatedChannelNum();
        //    int activatedChannelNum = m_config.GetChannelNum();
        //    int sampleCountPerLine = m_params.SampleCountPerLine;
        //    int xSampleCountPerLine = m_config.GetScanXPoints();
        //    int imageSampleCountPerFrame = m_config.GetScanXPoints() * m_config.GetScanYPoints();
        //    int sacnRows = m_config.GetScanStrategy() == SCAN_STRATEGY.Z_BIDIRECTION ? m_params.ScanRows * 2 : m_params.ScanRows;
        //    SCAN_STRATEGY scanStrategy = m_config.GetScanStrategy();

        //    int i, offset, sourceIndex;

        //    short[][] frame = new short[activatedChannelNum][];
        //    for (i = 0; i < activatedChannelNum; i++)
        //    {
        //        frame[i] = new short[imageSampleCountPerFrame];
        //    }

        //    while (m_scanning)
        //    {
        //        if (m_scanData.SampleQueueSize() == 0)
        //        {
        //            continue;
        //        }

        //        SampleData sample;
        //        if (!m_scanData.DequeueSample(out sample))
        //        {
        //            Logger.Info(string.Format("dequeue sample data failed."));
        //            continue;
        //        }

        //        //double average = 0;
        //        //for (i = 0; i < sample.NSamples.GetLength(0); i++)
        //        //{
        //        //    average = 0;
        //        //    for (int j = 0; j < sample.NSamples[i].Length; j++)
        //        //    {
        //        //        average += sample.NSamples[i][j];
        //        //    }
        //        //    average = average / sample.NSamples[0].Length;
        //        //    Logger.Info(string.Format("sample average value: [{0}][{1}].", i, average));
        //        //}

        //        sample.Convert();

        //        offset = sample.Line * xSampleCountPerLine;
        //        // 如果是双向扫描，且当前是奇数行，则该行的数据需要反转
        //        // 根据错位补偿参数，完成相应的截断
        //        if (scanStrategy == SCAN_STRATEGY.Z_BIDIRECTION)
        //        {
        //            if ((sample.Line & 0x01) == 0x01)
        //            {
        //                // sourceIndex = m_config.GetScanPixelCompensation() / 2 + m_config.GetScanPixelOffset();
        //                sourceIndex = m_config.GetScanPixelCompensation() / 2 + m_config.GetScanPixelCalibration(); 
        //                for (i = 0; i < activatedChannelNum; i++)
        //                {
        //                    Array.Reverse(sample.NSamples[i]);
        //                    Array.Copy(sample.NSamples[i], sourceIndex, frame[i], offset, xSampleCountPerLine);
        //                }
        //            }
        //            else
        //            {
        //                sourceIndex = m_config.GetScanPixelCompensation() / 2 + m_config.GetScanPixelOffset();
        //                for (i = 0; i < activatedChannelNum; i++)
        //                {
        //                    Array.Copy(sample.NSamples[i], sourceIndex, frame[i], offset, xSampleCountPerLine);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            sourceIndex = m_config.GetScanPixelCompensation() / 2 + m_config.GetScanPixelOffset();
        //            for (i = 0; i < activatedChannelNum; i++)
        //            {
        //                Array.Copy(sample.NSamples[i], sourceIndex, frame[i], offset, xSampleCountPerLine);
        //            }
        //        }

        //        // 完成一帧的转换
        //        if (sample.Line == sacnRows - 1)
        //        {
        //            FrameData frameData = new FrameData(sample.Frame, frame);
        //            m_scanData.EnqueueFrame(frameData);
        //            Logger.Info(string.Format("convert samples to frame: [{0}].", sample.Frame));

        //            //for (i = 0; i < activatedChannelNum; i++)
        //            //{
        //            //    average = 0;
        //            //    for (int j = 0; j < imageSampleCountPerFrame; j++)
        //            //    {
        //            //        average += frame[i][j];
        //            //    }
        //            //    average = average / imageSampleCountPerFrame;
        //            //    Logger.Info(string.Format("frame average value: [{0}][{1}].", i, average));
        //            //}

        //            frame = new short[activatedChannelNum][];
        //            for (i = 0; i < activatedChannelNum; i++)
        //            {
        //                frame[i] = new short[imageSampleCountPerFrame];
        //            }
        //        }
        //    }
        //    Logger.Info(string.Format("scan task[{0}|{1}] stop, finish convert samples.", m_taskId, m_taskName));
        //}

        private void ConvertSamplesHandler2()
        {
            int activatedChannelNum = m_config.GetChannelNum();
            int sampleCountPerLine = m_params.SampleCountPerLine;
            int xSampleCountPerLine = m_config.GetScanXPoints();
            int imageSampleCountPerFrame = m_config.GetScanXPoints() * m_config.GetScanYPoints();
            int sacnRows = m_config.GetScanStrategy() == SCAN_STRATEGY.Z_BIDIRECTION ? m_params.ScanRows * 2 : m_params.ScanRows;
            SCAN_STRATEGY scanStrategy = m_config.GetScanStrategy();

            int i, sourceIndex;

            short[][] data = new short[activatedChannelNum][];
            for (i = 0; i < activatedChannelNum; i++)
            {
                data[i] = new short[xSampleCountPerLine];
            }

            while (m_scanning)
            {
                if (m_scanData.SampleQueueSize() == 0)
                {
                    continue;
                }

                if (!m_scanData.DequeueSample(out SampleData sample))
                {
                    Logger.Info(string.Format("dequeue sample data failed."));
                    continue;
                }

                sample.Convert();

                // 如果是双向扫描，且当前是奇数行，则该行的数据需要反转
                // 根据错位补偿参数，完成相应的截断
                if (scanStrategy == SCAN_STRATEGY.Z_BIDIRECTION)
                {
                    if ((sample.Line & 0x01) == 0x01)
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

                ConvertData convertData = new ConvertData(data, sample.Frame, sample.Line);
                m_scanData.EnqueueConvertData(convertData);
                if (convertData.Line + 1 == sacnRows)
                {
                    // Logger.Info(string.Format("convert info: frame[{0}], line[{1}].", convertData.Frame, convertData.Line));
                }

                data = new short[activatedChannelNum][];
                for (i = 0; i < activatedChannelNum; i++)
                {
                    data[i] = new short[xSampleCountPerLine];
                }
            }
            Logger.Info(string.Format("scan task[{0}|{1}] stop, finish convert samples.", m_taskId, m_taskName));
        }
        
        //private void UpdateDisplayImageHandler()
        //{
        //    // int activatedChannelNum = m_config.GetActivatedChannelNum();
        //    int activatedChannelNum = m_config.GetChannelNum();
        //    int i;

        //    while (m_scanning)
        //    {
        //        if (m_scanData.FrameQueueSize() == 0)
        //        {
        //            continue;
        //        }

        //        FrameData frame = m_scanData.GetNewFrameData();
        //        if (frame.Frame == m_scanData.ImageData.Frame)
        //        {
        //            continue;
        //        }

        //        byte[][] displayData = new byte[activatedChannelNum][];
        //        for (i = 0; i < activatedChannelNum; i++)
        //        {
        //            CHAN_ID id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i);
        //            Color colorReference = m_config.GetChannelColorReference(id);
        //            // short noiseLevel = m_config.GetChannelBackgroundNoiseLevel(id);
        //            CImage.Gray16ToBGR24(colorReference, frame.Data[i], out displayData[i]);
        //            // CImage.Gray16ToGray24(frame.Data[i], out displayData[i]);
        //            // CImage.Gray16ToBGR24(frame.Data[i], out displayData[i]);
        //        }
        //        DisplayData display = new DisplayData(frame.Frame, displayData);
        //        m_scanData.ImageData = display;

        //        Logger.Info(string.Format("get new display data: [{0}].", m_scanData.ImageData.Frame));
        //    }

        //    Logger.Info(string.Format("scan task[{0}|{1}] stop, finish update display images.", m_taskId, m_taskName));
        //}

        private void UpdateDisplayImageHandler2()
        {
            int activatedChannelNum = m_config.GetChannelNum();
            int xSampleCountPerLine = m_config.GetScanXPoints();
            int sacnRows = m_config.GetScanStrategy() == SCAN_STRATEGY.Z_BIDIRECTION ? m_params.ScanRows * 2 : m_params.ScanRows;

            int i, index;
            List<byte[,]> colorMappingArr = new List<byte[,]>();

            // 生成伪彩色转换的映射序列
            for (i = 0; i < activatedChannelNum; i++)
            {
                CHAN_ID id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i);
                Color colorReference = m_config.GetChannelColorReference(id);
                byte[,] mapping = CImage.CreateColorMapping(colorReference);
                colorMappingArr.Add(mapping);
            }

            while (m_scanning)
            {
                if (m_scanData.ConvertQueueSize() == 0)
                {
                    continue;
                }

                if (!m_scanData.DequeueConvertData(out ConvertData convertData))
                {
                    Logger.Info(string.Format("dequeue convert data failed."));
                    continue;
                }

                m_scanData.SetImageFrame(convertData.Frame);
                // m_scanData.GetScanImage().SetCurrentLine(convertData.Line);
                for (i = 0; i < activatedChannelNum; i++)
                {
                    byte[] bgrData = m_scanData.ScanImage.BGRData[i];
                    byte[,] mapping = colorMappingArr.ElementAt(i);

                    index = xSampleCountPerLine * convertData.Line;
                    Array.Copy(convertData.NSamples[i], 0, m_scanData.ScanImage.Data[i], index, xSampleCountPerLine);

                    index = index * 3;
                    CImage.Gray16ToBGR24(convertData.NSamples[i], ref bgrData, index, mapping);

                    //Bitmap Canvas = m_scanData.ScanImage.DisplayImage;
                    //BitmapData CanvasData = Canvas.LockBits(new System.Drawing.Rectangle(0, convertData.Line, xSampleCountPerLine, 1), ImageLockMode.WriteOnly, Canvas.PixelFormat);
                    //Marshal.Copy(bgrData, index, CanvasData.Scan0, xSampleCountPerLine);
                    //Canvas.UnlockBits(CanvasData);
                }

                if (convertData.Line + 1 == sacnRows)
                {
                    
                    Logger.Info(string.Format("update image info: frame[{0}], line[{1}].", m_scanData.ScanImage.Frame, m_scanData.ScanImage.Line));
                }
                
            }

            Logger.Info(string.Format("scan task[{0}|{1}] stop, finish update display images.", m_taskId, m_taskName));
        }

        #endregion
    }
}
