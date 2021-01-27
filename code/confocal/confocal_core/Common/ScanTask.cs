using confocal_core.Model;
using confocal_core.ViewModel;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace confocal_core.Common
{
    public class ScanTask
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly int PMT_TASK_COUNT = 1;
        private static readonly int APD_TASK_COUNT = 2;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private readonly ConfigViewModel mConfig;
        private int mTaskId;
        private string mTaskName;
        private ScanInfoModel mScanInfo;
        private ScanDataModel mScanData;

        private CancellationTokenSource mCancelToken;
        private BlockingCollection<PmtSampleData> mPmtSampleQueue;
        private BlockingCollection<ApdSampleData> mApdSampleQueue;
        private Task[] mConsumers;

        /// <summary>
        /// 扫描任务ID
        /// </summary>
        public int TaskId
        {
            get { return mTaskId; }
            set { mTaskId = value; }
        }
        /// <summary>
        /// 扫描任务名
        /// </summary>
        public string TaskName
        {
            get { return mTaskName; }
            set { mTaskName = value; }
        }
        /// <summary>
        /// 扫描信息
        /// </summary>
        public ScanInfoModel ScanInfo
        {
            get { return mScanInfo; }
            set { mScanInfo = value; }
        }

        public ScanDataModel ScanData
        {
            get { return mScanData; }
            set { mScanData = value; }
        }

        public ScanTask(int taskId, string taskName)
        {
            mConfig = ConfigViewModel.GetConfig();
            TaskId = taskId;
            TaskName = taskName;
        }

        /// <summary>
        /// 启动扫描
        /// </summary>
        public void Start()
        {
            mCancelToken = new CancellationTokenSource();
            if (mConfig.Detector.CurrentDetecor.ID == DetectorTypeModel.PMT)
            {
                mPmtSampleQueue = new BlockingCollection<PmtSampleData>(new ConcurrentQueue<PmtSampleData>());
                mConsumers = new Task[PMT_TASK_COUNT];
                for (int i = 0; i < mConsumers.Length; i++)
                {
                    mConsumers[i] = Task.Run(() => PmtSampleConsumer(mPmtSampleQueue, mCancelToken.Token));
                }
            }
            else
            {
                mApdSampleQueue = new BlockingCollection<ApdSampleData>(new ConcurrentQueue<ApdSampleData>());
                mConsumers = new Task[APD_TASK_COUNT];
                for (int i = 0; i < mConsumers.Length; i++)
                {
                    mConsumers[i] = Task.Run(() => ApdSampleConsumer(mApdSampleQueue, mCancelToken.Token));
                }
            }
        }

        /// <summary>
        /// 停止扫描
        /// </summary>
        public void Stop()
        {
            mCancelToken.Cancel();
            if (mConfig.Detector.CurrentDetecor.ID == DetectorTypeModel.PMT)
            {
                mPmtSampleQueue.CompleteAdding();
            }
            else
            {
                mApdSampleQueue.CompleteAdding();
            }
            Task.WaitAll(mConsumers);
            Dispose();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        private void Dispose()
        {
            if (mCancelToken != null)
            {
                mCancelToken.Dispose();
                mCancelToken = null;
            }

            if (mApdSampleQueue != null)
            {
                mApdSampleQueue.Dispose();
                mApdSampleQueue = null;
            }

            if (mPmtSampleQueue != null)
            {
                mPmtSampleQueue.Dispose();
                mPmtSampleQueue = null;
            }

            foreach (Task consumer in mConsumers)
            {
                if (consumer != null)
                {
                    consumer.Dispose();
                }
            }
            mConsumers = null;
        }

        private void PmtSampleConsumer(BlockingCollection<PmtSampleData> queue, CancellationToken token)
        {
            while (!queue.IsCompleted)
            {

            }
        }

        private void ApdSampleConsumer(BlockingCollection<ApdSampleData> queue, CancellationToken token)
        {
            while (!queue.IsCompleted)
            {

            }
        }

    }
}
