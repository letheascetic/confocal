using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace confocal_core.Common
{
    public class BlockingQueue<T> : ConcurrentQueue<T>
    {
        private int mQueueSize;
        private Semaphore mSemaphore;

        public BlockingQueue(int queueSize = Int32.MaxValue)
        {
            mQueueSize = queueSize;
            mSemaphore = new Semaphore(0, Int32.MaxValue);

            BlockingCollection<T> x = new BlockingCollection<T>();
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Dequeue(out T t)
        {
            mSemaphore.WaitOne(50);
            return base.TryDequeue(out t);
        }
        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public new bool TryDequeue(out T t)
        {
            return Dequeue(out t);
        }
        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="t"></param>
        public new void Enqueue(T t)
        {
            base.Enqueue(t);
            mSemaphore.Release();
        }

    }
}
