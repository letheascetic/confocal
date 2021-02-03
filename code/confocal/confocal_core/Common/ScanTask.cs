using confocal_core.Model;
using confocal_core.ViewModel;
using Emgu.CV;
using log4net;
using NumSharp;
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
        private readonly ConfigViewModel mConfig;
        private readonly SequenceModel mSequence;
        private int mTaskId;
        private string mTaskName;
        private ScanInfoModel mScanInfo;
        private ScanDataModel mScanData;
        
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

        }

        /// <summary>
        /// 停止扫描
        /// </summary>
        public void Stop()
        {

        }

        /// <summary>
        /// 更新Bank图像
        /// </summary>
        /// <param name="sampleData"></param>
        public void ConvertPmtSamples(PmtSampleData sampleData)
        {
            for (int i = 0; i < mConfig.GetChannelNum(); i++)
            {
                if (sampleData.NSamples[i] != null)
                {
                    // 负电压转换
                    Matrix.ToPositive(ref sampleData.NSamples[i]);
                    // 生成Bank数据矩阵[截断/双向扫描偶数行翻转和错位补偿]
                    NDArray matrix = Matrix.ToMatrix(sampleData.NSamples[i], mSequence.InputSampleCountPerPixel, mSequence.InputPixelCountPerRow,
                        mSequence.InputPixelCountPerAcquisition / mSequence.InputPixelCountPerRow, mConfig.SelectedScanDirection.ID,
                        mConfig.SelectedScanPixelDwell.ScanPixelOffset, mConfig.SelectedScanPixelDwell.ScanPixelCalibration,
                        mConfig.SelectedScanPixel.Data);
                    // Bank数据矩阵更新到OriginImages对应的BankImage
                    Mat originImage = ScanData.OriginImages[i].Banks[ScanInfo.CurrentBank].Bank;
                    Matrix.ToBankImage(matrix, ref originImage);
                    // Origin的BankImage更新到Gray
                    Mat grayImage = ScanData.GrayImages[i].Banks[ScanInfo.CurrentBank].Bank;
                    double scale = 1.0 / Math.Pow(2, mConfig.SelectedScanPixelDwell.ScanPixelScale);
                    Matrix.ToGrayImage(originImage, ref grayImage, scale, mConfig.ScanChannels[i].Offset);
                }
            }
        }

    }
}
