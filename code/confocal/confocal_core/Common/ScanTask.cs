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
        private readonly SequenceModel mSequence;
        private int mTaskId;
        private string mTaskName;
        private ScanInfoModel mScanInfo;
        private ScanDataModel mScanData;

        private CancellationTokenSource mCancelToken;
        private BlockingCollection<PmtSampleData> mPmtSampleQueue;
        private BlockingCollection<ApdSampleData> mApdSampleQueue;
        private Task[] mSampleWorkers;

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
            mSequence = SequenceModel.CreateInstance();
            TaskId = taskId;
            TaskName = taskName;
        }

        /// <summary>
        /// 启动扫描
        /// </summary>
        public void Start()
        {
            bool[] statusOfChannels = mConfig.ScanChannels.Select(p => p.Activated).ToArray();

            ScanInfo = new ScanInfoModel(mSequence.InputAcquisitionCountPerFrame);
            ScanData = new ScanDataModel(mConfig.SelectedScanPixel.Data, mConfig.SelectedScanPixel.Data, mSequence.InputAcquisitionCountPerFrame,
                mConfig.GetChannelNum(), statusOfChannels);

            mCancelToken = new CancellationTokenSource();
            if (mConfig.Detector.CurrentDetecor.ID == DetectorTypeModel.PMT)
            {
                mPmtSampleQueue = new BlockingCollection<PmtSampleData>(new ConcurrentQueue<PmtSampleData>());
                mSampleWorkers = new Task[PMT_TASK_COUNT];
                for (int i = 0; i < mSampleWorkers.Length; i++)
                {
                    mSampleWorkers[i] = Task.Run(() => PmtSampleWorker());
                }
            }
            else
            {
                mApdSampleQueue = new BlockingCollection<ApdSampleData>(new ConcurrentQueue<ApdSampleData>());
                mSampleWorkers = new Task[APD_TASK_COUNT];
                for (int i = 0; i < mSampleWorkers.Length; i++)
                {
                    mSampleWorkers[i] = Task.Run(() => ApdSampleWorker());
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
            Task.WaitAll(mSampleWorkers);
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

            foreach (Task consumer in mSampleWorkers)
            {
                if (consumer != null)
                {
                    consumer.Dispose();
                }
            }
            mSampleWorkers = null;
        }

        public void EnquenePmtSamples(ushort[][] samples, long acquisitionCount)
        {
            try
            {
                PmtSampleData sampleData = new PmtSampleData(samples, acquisitionCount);
                mPmtSampleQueue.TryAdd(sampleData, 50, mCancelToken.Token);
                ConvertPmtSamples(sampleData);
                Logger.Info(string.Format("Enqueue Pmt Samples [{0}].", acquisitionCount));
            }
            catch (OperationCanceledException)
            {
                Logger.Info(string.Format("Enqueue Pmt Smaples [{0}] Canceled.", acquisitionCount));
                mPmtSampleQueue.CompleteAdding();
            }
        }

        public void EnqueneApdSamples(int channelIndex, uint[] samples, long acquisitionCount)
        {
            try
            {
                ApdSampleData sampleData = new ApdSampleData(samples, channelIndex, acquisitionCount);
                mApdSampleQueue.TryAdd(sampleData, 50, mCancelToken.Token);
                Logger.Info(string.Format("Enqueue Apd Samples [{0}][{1}].", channelIndex, acquisitionCount));
            }
            catch (OperationCanceledException)
            {
                Logger.Info(string.Format("Enqueue Apd Smaples [{0}][{1}] Canceled.", channelIndex, acquisitionCount));
                mPmtSampleQueue.CompleteAdding();
            }
        }

        private void PmtSampleWorker()
        {
            while (!mPmtSampleQueue.IsCompleted)
            {
                try
                {
                    if (mPmtSampleQueue.TryTake(out PmtSampleData sampleData, 20, mCancelToken.Token))
                    {
                        ScanInfo.UpdateScanInfo(sampleData.AcquisitionCount);
                        Logger.Info(string.Format("Scan Info [{0}].", ScanInfo));
                    }
                }
                catch (OperationCanceledException)
                {
                    Logger.Info(string.Format("Pmt Sample Worker Canceled."));
                    break;
                }
            }
            Logger.Info(string.Format("Pmt Sample Worker Finished."));
        }

        private void ApdSampleWorker()
        {
            while (!mApdSampleQueue.IsCompleted)
            {
                try
                {
                    mApdSampleQueue.TryTake(out ApdSampleData sampleData, 20, mCancelToken.Token);
                }
                catch (OperationCanceledException)
                {
                    Logger.Info(string.Format("Apd Sample Worker Canceled."));
                    break;
                }
                Logger.Info(string.Format("Apd Sample Worker Finished."));
            }
        }

        private void ConvertPmtSamples(PmtSampleData sampleData)
        {
            int samplesPerPixel = mSequence.InputSampleCountPerPixel;
            int pixelsPerRow = mSequence.InputSampleCountPerRow / mSequence.InputSampleCountPerPixel;
            int pixelsPerCol = mSequence.InputPixelCountPerAcquisition / pixelsPerRow;
            // Matrix.ToMatrix(sampleData.NSamples[0], samplesPerPixel, pixelsPerRow, pixelsPerCol, mConfig.SelectedScanDirection.ID);
        }

    }
}
