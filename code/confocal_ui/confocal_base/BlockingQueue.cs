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

        public T Dequeue()
        {
            T t = default(T);
            m_semaphore.WaitOne();
            base.TryDequeue(out T);
            return t;
        }

        public new T TryDequeue(out T t)
        {
            throw new Exception("Unsupport Method Exception");
        }

        public new void Enqueue(T t)
        {
            base.Enqueue(t);
            m_semaphore.Release();
        }

    }
}
