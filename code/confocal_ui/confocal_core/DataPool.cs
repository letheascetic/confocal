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
        public ushort[,] NSamples { get; set; }
        public long Frame { get; }
        public long Line { get; }

        public SampleData(ushort[,] samples, long frame, long line)
        {
            NSamples = samples;
            Frame = frame;
            Line = line;
        }
    }

    public struct ImageData
    {
        public long Frame { get; }
        public ushort[] Data;
    }

    public class DataPool
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private ConcurrentQueue<SampleData> m_sampleQueue;

        
        ///////////////////////////////////////////////////////////////////////////////////////////


        public DataPool()
        {
            m_sampleQueue = new ConcurrentQueue<SampleData>();

        }

        public int SampleQueueSize()
        {
            return m_sampleQueue.Count;
        }

        public void EnqueueSample(SampleData sampleData)
        {
            m_sampleQueue.Enqueue(sampleData);
        }

        public bool DequeueSample(out SampleData sampleData)
        {
            return m_sampleQueue.TryDequeue(out sampleData);
        }



    }
}
