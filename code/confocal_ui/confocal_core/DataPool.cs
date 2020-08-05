﻿using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core
{
    /// <summary>
    /// 原始的行数据
    /// </summary>
    public struct SampleData
    {
        public short[][] NSamples { get; set; }
        public long Frame { get; }
        public int Line { get; }

        public SampleData(short[][] samples, long frame, int line)
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

    public struct ConvertData
    {
        public short[][] NSamples { get; set; }
        public long Frame { get; }
        public int Line { get; }

        public ConvertData(short[][] samples, long frame, int line)
        {
            NSamples = samples;
            Frame = frame;
            Line = line;
        }

        public void Convert()
        { }
    }

    public struct ImageData
    {
        /// <summary>
        /// 当前帧
        /// </summary>
        public long Frame { get; set; }
        /// <summary>
        /// 当前行
        /// </summary>
        public int Line { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public short[][] Data { get; set; }

        public byte[][] BGRData { get; set; }

        public ImageData(int activatedChannelNum, int scanXPoints, int scanYPoints)
        {
            int samplesPerFrame = scanXPoints * scanYPoints;
            Frame = -1;
            Line = -1;
            Data = new short[activatedChannelNum][];
            BGRData = new byte[activatedChannelNum][];
            for (int i = 0; i < activatedChannelNum; i++)
            {
                Data[i] = new short[samplesPerFrame];
                BGRData[i] = new byte[samplesPerFrame * 3];
            }
        }
    }

    /// <summary>
    /// 帧数据
    /// </summary>
    public struct FrameData
    {
        public long Frame { get; }
        public short[][] Data { get; }

        public FrameData(long frame, short[][] data)
        {
            Frame = frame;
            Data = data;
        }
    }

    //public struct DisplayData
    //{
    //    public long Frame { get; set; }
    //    public byte[][] Data { get; set;}

    //    public DisplayData(long frame, byte[][] data)
    //    {
    //        Frame = frame;
    //        Data = data;
    //    }
    //}
    
    public class DataPool
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        // private static readonly int MAXIMUM_FRAME_QUEUE_SIZE = 2;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private ConcurrentQueue<SampleData> m_sampleQueue;      // 原始行数据队列
        private ConcurrentQueue<ConvertData> m_convertQueue;    // 截断、反转、去底噪后的行数据队列
        private ImageData m_imageData;                          // 帧数据
        // private ConcurrentQueue<FrameData> m_frameQueue;
        // private DisplayData m_displayData;
        ///////////////////////////////////////////////////////////////////////////////////////////
        //public DisplayData ImageData
        //{ get { return m_displayData; } set { m_displayData = value; } }
        public ImageData ScanImage
        { get { return m_imageData; } set { m_imageData = value; } }
        ///////////////////////////////////////////////////////////////////////////////////////////

        public DataPool()
        {
            // m_displayData = new DisplayData(-1, null);
            m_sampleQueue = new ConcurrentQueue<SampleData>();
            m_convertQueue = new ConcurrentQueue<ConvertData>();
            m_imageData = new ImageData();
            // m_frameQueue = new ConcurrentQueue<FrameData>();
        } 

        public void Config()
        {
            Release();
            confocal_core.Config config = confocal_core.Config.GetConfig();
            m_imageData = new ImageData(config.GetChannelNum(), config.GetScanXPoints(), config.GetScanYPoints());
        }

        public int SampleQueueSize()
        {
            return m_sampleQueue.Count;
        }

        public void EnqueueSample(SampleData sampleData)
        {
            m_sampleQueue.Enqueue(sampleData);
        }

        public void EnqueueSample(short[][] samples, long frame, int line)
        {
            SampleData sampleData = new SampleData(samples, frame, line);
            EnqueueSample(sampleData);
        }

        public bool DequeueSample(out SampleData sampleData)
        {
            return m_sampleQueue.TryDequeue(out sampleData);
        }

        //public int FrameQueueSize()
        //{
        //    return m_frameQueue.Count;
        //}

        //public void EnqueueFrame(FrameData frame)
        //{
        //    FrameData frameData;
        //    while (m_frameQueue.Count >= MAXIMUM_FRAME_QUEUE_SIZE)
        //    {
        //        DequeueFrame(out frameData);
        //    }
        //    m_frameQueue.Enqueue(frame);
        //}

        //public bool DequeueFrame(out FrameData frame)
        //{
        //    return m_frameQueue.TryDequeue(out frame);
        //}

        //public FrameData GetNewFrameData()
        //{
        //    return m_frameQueue.Last();
        //}

        public int ConvertQueueSize()
        {
            return m_convertQueue.Count;
        }

        public void EnqueueConvertData(ConvertData convertData)
        {
            m_convertQueue.Enqueue(convertData);
        }

        public bool DequeueConvertData(out ConvertData convertData)
        {
            return m_convertQueue.TryDequeue(out convertData);
        }

        public void Release()
        {
            // m_displayData = new DisplayData(-1, null);
            while (!m_sampleQueue.IsEmpty)
            {
                DequeueSample(out SampleData sampleData);
            }
            while (!m_convertQueue.IsEmpty)
            {
                DequeueConvertData(out ConvertData convertData);
            }
            //while (!m_frameQueue.IsEmpty)
            //{
            //    DequeueFrame(out FrameData frame);
            //}
        }
    }
}
