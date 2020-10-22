using confocal_base;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.OCR;
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
                    // CHAN_ID id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i);
                    // short noiseLevel = Config.GetConfig().GetChannelBackgroundNoiseLevel(id);
                    int len = channelSample.Length;
                    for (int j = 0; j < len; j++)
                    {
                        if (channelSample[j] < 0)
                        {
                            channelSample[j] = (short)-channelSample[j];
                        }
                        // channelSample[j] = (short)(channelSample[j] >> 8);
                        //if (channelSample[j] <= noiseLevel)
                        //{
                        //    channelSample[j] = 0;
                        //}
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

        private Mat[] m_originMat;      // 原始图像数据
        private Mat[] m_grayMat;        // 灰度图像数据
        private Mat[] m_gray3Mat;       // 
        private Mat[] m_bgrMat;         // 伪彩色图像数据
        
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
        /// 灰度图像
        /// </summary>
        public Mat[] GrayMat { get { return m_grayMat; } set { m_grayMat = value; } }

        public Mat[] Gray3Mat { get { return m_gray3Mat; } set { m_gray3Mat = value; } }
        /// <summary>
        /// 原始图像
        /// </summary>
        public Mat[] OriginMat { get { return m_originMat; } set { m_originMat = value; } }
        /// <summary>
        /// 伪彩色图像
        /// </summary>
        public Mat[] BGRMat { get { return m_bgrMat; } set { m_bgrMat = value; } }

        public ImageData(int channelNum, int scanXPoints, int scanYPoints)
        {
            int samplesPerFrame = scanXPoints * scanYPoints;
            m_frame = -1;
            m_line = -1;

            m_originMat = new Mat[channelNum];
            m_grayMat = new Mat[channelNum];
            m_bgrMat = new Mat[channelNum];
            m_gray3Mat = new Mat[channelNum];

            for (int i = 0; i < channelNum; i++)
            {
                OriginMat[i] = new Mat(scanXPoints, scanYPoints, DepthType.Cv16S, 1);
                GrayMat[i] = new Mat(scanYPoints, scanXPoints, DepthType.Cv8U, 1);
                Gray3Mat[i] = new Mat(scanYPoints, scanXPoints, DepthType.Cv8U, 3);
                BGRMat[i] = new Mat(scanYPoints, scanXPoints, DepthType.Cv8U, 3);
            }
        }

        public void UpdateDisplayImage(int index, Mat mapping)
        {
            try
            {
                lock (m_locker)
                {
                    OriginMat[index].ConvertTo(GrayMat[index], DepthType.Cv8U, 1.0 / 128, 0);
                    CvInvoke.CvtColor(GrayMat[index], Gray3Mat[index], ColorConversion.Gray2Bgr);
                    CvInvoke.LUT(Gray3Mat[index], mapping, BGRMat[index]);
                    // CvInvoke.ApplyColorMap(GrayMat[index], BGRMat[index], Emgu.CV.CvEnum.ColorMapType.Autumn);
                }
            }
            catch (Exception e)
            {
                Logger.Info(string.Format("[{0}].", e));
            }
        }

        //public void UpdateDisplayImage(int index, byte[,] mapping, Rectangle lockBitsZoom)
        //{
        //    lock (m_locker)
        //    {
        //        byte[] bgrData = m_bgrData[index];
        //        Bitmap canvas = m_displayImages[index];
        //        BitmapData canvasData = canvas.LockBits(lockBitsZoom, ImageLockMode.WriteOnly, canvas.PixelFormat);
        //        Marshal.Copy(bgrData, 0, canvasData.Scan0, bgrData.Length);
        //        canvas.UnlockBits(canvasData);
        //    }
        //}
        
    }

    public class DataPool
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private BlockingQueue<PmtSampleData> m_pmtSampleQueue;          // 原始行数据队列
        // private BlockingQueue<PmtSampleData> m_pmtConvertQueue;      // 截断、反转、去底噪后的行数据队列
        private BlockingQueue<ApdSampleData> m_apdSampleQueue;
        // private BlockingQueue<ApdSampleData> m_apdConvertQueue;
        private ImageData m_imageData;                              // 帧数据
        ///////////////////////////////////////////////////////////////////////////////////////////
        public ImageData ScanImage
        { get { return m_imageData; } set { m_imageData = value; } }
        ///////////////////////////////////////////////////////////////////////////////////////////

        public DataPool()
        {
            m_pmtSampleQueue = new BlockingQueue<PmtSampleData>();
            // m_pmtConvertQueue = new BlockingQueue<PmtSampleData>();
            m_apdSampleQueue = new BlockingQueue<ApdSampleData>();
            // m_apdConvertQueue = new BlockingQueue<ApdSampleData>();
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

        //public int PmtConvertQueueSize()
        //{
        //    return m_pmtConvertQueue.Count;
        //}

        //public void EnqueuePmtConvertData(PmtSampleData convertData)
        //{
        //    m_pmtConvertQueue.Enqueue(convertData);
        //}

        //public bool DequeuePmtConvertData(out PmtSampleData convertData)
        //{
        //    return m_pmtConvertQueue.Dequeue(out convertData);
        //}

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

        //public int ApdConvertQueueSize()
        //{
        //    return m_apdConvertQueue.Count;
        //}

        //public void EnqueueApdConvertData(ApdSampleData convertData)
        //{
        //    m_apdConvertQueue.Enqueue(convertData);
        //}

        //public bool DequeueApdConvertData(out ApdSampleData convertData)
        //{
        //    return m_apdConvertQueue.Dequeue(out convertData);
        //}

        public void Release()
        {
            while (!m_pmtSampleQueue.IsEmpty)
            {
                DequeuePmtSample(out PmtSampleData sampleData);
            }
            //while (!m_pmtConvertQueue.IsEmpty)
            //{
            //    DequeuePmtConvertData(out PmtSampleData convertData);
            //}
            while (!m_apdSampleQueue.IsEmpty)
            {
                DequeueApdSample(out ApdSampleData sampleData);
            }
            //while (!m_apdConvertQueue.IsEmpty)
            //{
            //    DequeueApdConvertData(out ApdSampleData convertData);
            //}
        }
    }
}
