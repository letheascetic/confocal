using confocal_base;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace confocal_core
{
    /// <summary>
    /// 原始的行数据
    /// </summary>
    public struct PmtSampleData
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        public short[][] NSamples { get; set; }
        public long Frame { get; }
        public int Line { get; }

        public PmtSampleData(short[][] samples, long frame, int line)
        {
            NSamples = samples;
            Frame = frame;
            Line = line;
        }

        public void Convert()
        {
            for (int i = 0; i < NSamples.GetLength(0); i++)
            {
                short[] channelSample = NSamples[i];
                if (channelSample != null)
                {
                    CHAN_ID id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i);
                    short noiseLevel = Config.GetConfig().GetChannelBackgroundNoiseLevel(id);
                    int len = channelSample.Length;
                    for (int j = 0; j < len; j++)
                    {
                        if (channelSample[j] < 0)
                        {
                            channelSample[j] = (short)-channelSample[j];
                        }
                        if (channelSample[j] <= noiseLevel)
                        {
                            channelSample[j] = 0;
                        }
                    }
                }
            }
        }
    }

    public struct ApdSampleData
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        public int[] NSamples { get; set; }
        public long Frame { get; }
        public int Line { get; }
        public int ChannelIndex { get; set; }

        public ApdSampleData(int[] samples, long frame, int line, int channelIndex)
        {
            NSamples = samples;
            Frame = frame;
            Line = line;
            ChannelIndex = channelIndex;
        }

        public void Convert()
        {
            for (int i = NSamples.Length - 1; i > 0; i--)
            {
                NSamples[i] = NSamples[i] - NSamples[i - 1];
                if (NSamples[i] < 0)
                {
                    NSamples[i] = NSamples[i] + int.MaxValue;
                }
            }
            NSamples[0] = NSamples[1];
            Logger.Info(string.Format("max count: [{0}].", NSamples.Max()));
        }
    }
    
    public class ImageData
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private readonly object m_locker = new object();        // 锁
        private long m_frame;
        private int m_line;
        private double[][] m_linePixelValue;
        private int[][] m_data;
        private byte[][] m_bgrData;
        private Bitmap[] m_displayImages;
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 当前帧
        /// </summary>
        public long Frame { get { return m_frame; } set { m_frame = value; } }
        /// <summary>
        /// 当前行
        /// </summary>
        public int Line { get { return m_line; } set { m_line = value; } }
        /// <summary>
        /// 原始图像数据
        /// </summary>
        public int[][] Data { get { return m_data; } set { m_data = value; } }
        /// <summary>
        /// 伪彩色图像数据
        /// </summary>
        public byte[][] BGRData { get { return m_bgrData; } set { m_bgrData = value; } }

        /// <summary>
        /// 每个通道每行的平均像素
        /// </summary>
        public double[][] PixelValuePerLine { get { return m_linePixelValue; } set { m_linePixelValue = value; } }

        public ImageData(int channelNum, int scanXPoints, int scanYPoints)
        {
            int samplesPerFrame = scanXPoints * scanYPoints;
            m_frame = -1;
            m_line = -1;
            Data = new int[channelNum][];
            BGRData = new byte[channelNum][];
            m_displayImages = new Bitmap[channelNum];
            m_linePixelValue = new double[channelNum][];
            for (int i = 0; i < channelNum; i++)
            {
                Data[i] = new int[samplesPerFrame];
                BGRData[i] = new byte[samplesPerFrame * 3];
                m_displayImages[i] = new Bitmap(scanXPoints, scanYPoints, PixelFormat.Format24bppRgb);
                m_linePixelValue[i] = Enumerable.Repeat<double>(0, scanYPoints).ToArray();
            }
        }

        public Bitmap GetDisplayImage(int index, ref Bitmap destnation)
        {
            lock (m_locker)
            {
                destnation = (Bitmap)m_displayImages[index].Clone();
                return destnation;
            }
        }

        public void UpdateDisplayImage(int index, byte[,] mapping, Rectangle lockBitsZoom)
        {
            lock (m_locker)
            {
                byte[] bgrData = m_bgrData[index];
                Bitmap canvas = m_displayImages[index];
                BitmapData canvasData = canvas.LockBits(lockBitsZoom, ImageLockMode.WriteOnly, canvas.PixelFormat);
                Marshal.Copy(bgrData, 0, canvasData.Scan0, bgrData.Length);
                canvas.UnlockBits(canvasData);
            }
        }
        
    }

    public class DataPool
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private BlockingQueue<PmtSampleData> m_pmtSampleQueue;       // 原始行数据队列
        private BlockingQueue<PmtSampleData> m_pmtConvertQueue;      // 截断、反转、去底噪后的行数据队列
        private BlockingQueue<ApdSampleData> m_apdSampleQueue;
        private BlockingQueue<ApdSampleData> m_apdConvertQueue;
        private ImageData m_imageData;                              // 帧数据
        ///////////////////////////////////////////////////////////////////////////////////////////
        public ImageData ScanImage
        { get { return m_imageData; } set { m_imageData = value; } }
        ///////////////////////////////////////////////////////////////////////////////////////////

        public DataPool()
        {
            m_pmtSampleQueue = new BlockingQueue<PmtSampleData>();
            m_pmtConvertQueue = new BlockingQueue<PmtSampleData>();
            m_apdSampleQueue = new BlockingQueue<ApdSampleData>();
            m_apdConvertQueue = new BlockingQueue<ApdSampleData>();
            confocal_core.Config config = confocal_core.Config.GetConfig();
            m_imageData = new ImageData(config.GetChannelNum(), config.GetScanXPoints(), config.GetScanYPoints());
        }

        public void Config()
        {
            Release();
            confocal_core.Config config = confocal_core.Config.GetConfig();
            m_imageData = new ImageData(config.GetChannelNum(), config.GetScanXPoints(), config.GetScanYPoints());
        }

        public long GetImageFrame()
        {
            return m_imageData.Frame;
        }

        public void SetImageFrame(long frame)
        {
            m_imageData.Frame = frame;
        }

        public int GetImageLine()
        {
            return m_imageData.Line;
        }

        public void SetImageLine(int line)
        {
            m_imageData.Line = line;
        }

        public int PmtSampleQueueSize()
        {
            return m_pmtSampleQueue.Count;
        }

        public void EnqueuePmtSample(PmtSampleData sampleData)
        {
            m_pmtSampleQueue.Enqueue(sampleData);
        }

        public void EnqueuePmtSample(short[][] samples, long frame, int line)
        {
            PmtSampleData sampleData = new PmtSampleData(samples, frame, line);
            EnqueuePmtSample(sampleData);
        }

        public bool DequeuePmtSample(out PmtSampleData sampleData)
        {
            return m_pmtSampleQueue.Dequeue(out sampleData);
        }

        public int PmtConvertQueueSize()
        {
            return m_pmtConvertQueue.Count;
        }

        public void EnqueuePmtConvertData(PmtSampleData convertData)
        {
            m_pmtConvertQueue.Enqueue(convertData);
        }

        public bool DequeuePmtConvertData(out PmtSampleData convertData)
        {
            return m_pmtConvertQueue.Dequeue(out convertData);
        }

        public int ApdSampleQueueSize()
        {
            return m_apdSampleQueue.Count;
        }

        public void EnqueueApdSample(ApdSampleData sampleData)
        {
            m_apdSampleQueue.Enqueue(sampleData);
        }

        public void EnqueueApdSample(short[][] samples, long frame, int line)
        {
            PmtSampleData sampleData = new PmtSampleData(samples, frame, line);
            EnqueuePmtSample(sampleData);
        }

        public bool DequeueApdSample(out ApdSampleData sampleData)
        {
            return m_apdSampleQueue.Dequeue(out sampleData);
        }

        public int ApdConvertQueueSize()
        {
            return m_apdConvertQueue.Count;
        }

        public void EnqueueApdConvertData(ApdSampleData convertData)
        {
            m_apdConvertQueue.Enqueue(convertData);
        }

        public bool DequeueApdConvertData(out ApdSampleData convertData)
        {
            return m_apdConvertQueue.Dequeue(out convertData);
        }

        public void Release()
        {
            while (!m_pmtSampleQueue.IsEmpty)
            {
                DequeuePmtSample(out PmtSampleData sampleData);
            }
            while (!m_pmtConvertQueue.IsEmpty)
            {
                DequeuePmtConvertData(out PmtSampleData convertData);
            }
            while (!m_apdSampleQueue.IsEmpty)
            {
                DequeueApdSample(out ApdSampleData sampleData);
            }
            while (!m_apdConvertQueue.IsEmpty)
            {
                DequeueApdConvertData(out ApdSampleData convertData);
            }
        }
    }
}
