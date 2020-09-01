﻿using log4net;
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

    public struct ApdSampleData
    {
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
            for (int i = 1; i < NSamples.Length; i++)
            {
                NSamples[i] = NSamples[i + 1] - NSamples[i];
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
        private ConcurrentQueue<PmtSampleData> m_pmtSampleQueue;       // 原始行数据队列
        private ConcurrentQueue<PmtSampleData> m_pmtConvertQueue;      // 截断、反转、去底噪后的行数据队列
        private ConcurrentQueue<ApdSampleData> m_apdSampleQueue;
        private ConcurrentQueue<ApdSampleData> m_apdConvertQueue;
        private ImageData m_imageData;                              // 帧数据
        ///////////////////////////////////////////////////////////////////////////////////////////
        public ImageData ScanImage
        { get { return m_imageData; } set { m_imageData = value; } }
        ///////////////////////////////////////////////////////////////////////////////////////////

        public DataPool()
        {
            m_pmtSampleQueue = new ConcurrentQueue<PmtSampleData>();
            m_pmtConvertQueue = new ConcurrentQueue<PmtSampleData>();
            m_apdSampleQueue = new ConcurrentQueue<ApdSampleData>();
            m_apdConvertQueue = new ConcurrentQueue<ApdSampleData>();
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
            return m_pmtSampleQueue.TryDequeue(out sampleData);
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
            return m_pmtConvertQueue.TryDequeue(out convertData);
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
            return m_apdSampleQueue.TryDequeue(out sampleData);
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
            return m_apdConvertQueue.TryDequeue(out convertData);
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
