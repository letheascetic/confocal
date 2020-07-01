using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public struct SampleData
    {
        public ushort[][] NSamples { get; set; }
        public long Frame { get; }
        public int Line { get; }

        public SampleData(ushort[][] samples, long frame, int line)
        {
            NSamples = samples;
            Frame = frame;
            Line = line;
        }
    }

    public struct FrameData
    {
        public long Frame { get; }
        public ushort[][] Data { get; }

        public FrameData(long frame, ushort[][] data)
        {
            Frame = frame;
            Data = data;
        }
    }

    public struct DisplayData
    {
        public long Frame { get; set; }
        public byte[][] Data { get; set;}

        public DisplayData(long frame, byte[][] data)
        {
            Frame = frame;
            Data = data;
        }
    }
    
    public class DataPool
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private static readonly int MAXIMUM_FRAME_QUEUE_SIZE = 2;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private ConcurrentQueue<SampleData> m_sampleQueue;
        private ConcurrentQueue<FrameData> m_frameQueue;
        private DisplayData m_displayData;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public DisplayData ImageData
        { get { return m_displayData; } set { m_displayData = value; } }
        ///////////////////////////////////////////////////////////////////////////////////////////

        public DataPool()
        {
            m_displayData = new DisplayData(-1, null);
            m_sampleQueue = new ConcurrentQueue<SampleData>();
            m_frameQueue = new ConcurrentQueue<FrameData>();
        }

        public int SampleQueueSize()
        {
            return m_sampleQueue.Count;
        }

        public void EnqueueSample(SampleData sampleData)
        {
            m_sampleQueue.Enqueue(sampleData);
        }

        public void EnqueueSample(ushort[][] samples, long frame, int line)
        {
            SampleData sampleData = new SampleData(samples, frame, line);
            EnqueueSample(sampleData);
        }

        public bool DequeueSample(out SampleData sampleData)
        {
            return m_sampleQueue.TryDequeue(out sampleData);
        }

        public int FrameQueueSize()
        {
            return m_frameQueue.Count;
        }

        public void EnqueueFrame(FrameData frame)
        {
            FrameData frameData;
            while (m_frameQueue.Count >= MAXIMUM_FRAME_QUEUE_SIZE)
            {
                DequeueFrame(out frameData);
            }
            m_frameQueue.Enqueue(frame);
        }

        public bool DequeueFrame(out FrameData frame)
        {
            return m_frameQueue.TryDequeue(out frame);
        }

        public FrameData GetNewFrameData()
        {
            return m_frameQueue.Last();
        }
        
    }
}
