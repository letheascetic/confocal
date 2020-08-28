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
                CHAN_ID id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i);
                short noiseLevel = Config.GetConfig().GetChannelBackgroundNoiseLevel(id);
                short[] channelSample = NSamples[i];
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

    public struct PmtConvertData
    {
        public short[][] NSamples { get; set; }
        public long Frame { get; }
        public int Line { get; }

        public PmtConvertData(short[][] samples, long frame, int line)
        {
            NSamples = samples;
            Frame = frame;
            Line = line;
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
        private short[][] m_data;
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
        public short[][] Data { get { return m_data; } set { m_data = value; } }
        /// <summary>
        /// 伪彩色图像数据
        /// </summary>
        public byte[][] BGRData { get { return m_bgrData; } set { m_bgrData = value; } }

        public ImageData(int activatedChannelNum, int scanXPoints, int scanYPoints)
        {
            int samplesPerFrame = scanXPoints * scanYPoints;
            m_frame = -1;
            m_line = -1;
            Data = new short[activatedChannelNum][];
            BGRData = new byte[activatedChannelNum][];
            m_displayImages = new Bitmap[activatedChannelNum];
            for (int i = 0; i < activatedChannelNum; i++)
            {
                Data[i] = new short[samplesPerFrame];
                BGRData[i] = new byte[samplesPerFrame * 3];
                m_displayImages[i] = new Bitmap(scanXPoints, scanYPoints, PixelFormat.Format24bppRgb);
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
        private ConcurrentQueue<PmtSampleData> m_sampleQueue;      // 原始行数据队列
        private ConcurrentQueue<PmtConvertData> m_convertQueue;    // 截断、反转、去底噪后的行数据队列
        private ImageData m_imageData;                          // 帧数据
        ///////////////////////////////////////////////////////////////////////////////////////////
        public ImageData ScanImage
        { get { return m_imageData; } set { m_imageData = value; } }
        ///////////////////////////////////////////////////////////////////////////////////////////

        public DataPool()
        {
            m_sampleQueue = new ConcurrentQueue<PmtSampleData>();
            m_convertQueue = new ConcurrentQueue<PmtConvertData>();
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

        public int SampleQueueSize()
        {
            return m_sampleQueue.Count;
        }

        public void EnqueueSample(PmtSampleData sampleData)
        {
            m_sampleQueue.Enqueue(sampleData);
        }

        public void EnqueueSample(short[][] samples, long frame, int line)
        {
            PmtSampleData sampleData = new PmtSampleData(samples, frame, line);
            EnqueueSample(sampleData);
        }

        public bool DequeueSample(out PmtSampleData sampleData)
        {
            return m_sampleQueue.TryDequeue(out sampleData);
        }

        public int ConvertQueueSize()
        {
            return m_convertQueue.Count;
        }

        public void EnqueueConvertData(PmtConvertData convertData)
        {
            m_convertQueue.Enqueue(convertData);
        }

        public bool DequeueConvertData(out PmtConvertData convertData)
        {
            return m_convertQueue.TryDequeue(out convertData);
        }

        public void Release()
        {
            while (!m_sampleQueue.IsEmpty)
            {
                DequeueSample(out PmtSampleData sampleData);
            }
            while (!m_convertQueue.IsEmpty)
            {
                DequeueConvertData(out PmtConvertData convertData);
            }
        }
    }
}
