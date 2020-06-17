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
        public long Line { get; }

        public SampleData(ushort[][] samples, long frame, long line)
        {
            NSamples = samples;
            Frame = frame;
            Line = line;
        }
    }

    public struct FrameData
    {
        public long Frame { get; }
        public ushort[] Data;

        public FrameData(long frame, ushort[] data)
        {
            Frame = frame;
            Data = data;
        }
    }

    public class DataPool
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private static readonly int MAXIMUM_FRAME_QUEUE_SIZE_MBYTES = 512;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private ConcurrentQueue<SampleData> m_sampleQueue;
        private ConcurrentQueue<FrameData[]> m_frameQueue;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public DataPool()
        {
            m_sampleQueue = new ConcurrentQueue<SampleData>();
            m_frameQueue = new ConcurrentQueue<FrameData[]>();
        }

        public int SampleQueueSize()
        {
            return m_sampleQueue.Count;
        }

        public void EnqueueSample(SampleData sampleData)
        {
            m_sampleQueue.Enqueue(sampleData);
        }

        public void EnqueueSample(ushort[][] samples, long frame, long line)
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

        public void EnqueueFrames(FrameData[] frames)
        {
            m_frameQueue.Enqueue(frames);
        }

        public bool DequeueFrames(out FrameData[] frames)
        {
            return m_frameQueue.TryDequeue(out frames);
        }

    }
}
