using System;
using System.Collections.Concurrent;
using System.Threading;

namespace confocal_base
{
    public class BlockingQueue<T>: ConcurrentQueue<T>
    {
        private int m_queueSize;
        private Semaphore m_semaphore;

        public BlockingQueue(int queueSize=Int32.MaxValue)
        {
            m_queueSize = queueSize;
            m_semaphore = new Semaphore(0, Int32.MaxValue);
        }

        public bool Dequeue(out T t)
        {
            m_semaphore.WaitOne();
            return base.TryDequeue(out t);
        }

        public new bool TryDequeue(out T t)
        {
            return Dequeue(out t);
        }

        public new void Enqueue(T t)
        {
            base.Enqueue(t);
            m_semaphore.Release();
        }

    }
}
