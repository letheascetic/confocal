using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    /// <summary>
    /// 扫描信息
    /// </summary>
    public class ScanInfoModel : ObservableObject
    {
        private DateTime startTime;
        private double timeSpan;
        private double frameTime;
        private double fps;
        private long currentFrame;
        private int currentBank;
        private int numOfBank;

        /// <summary>
        /// 扫描开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        /// <summary>
        /// 扫描时长
        /// </summary>
        public double TimeSpan
        {
            get { return timeSpan; }
            set { timeSpan = value; RaisePropertyChanged(() => TimeSpan); }
        }
        /// <summary>
        /// 扫描帧率
        /// </summary>
        public double FPS
        {
            get { return fps; }
            set { fps = value; RaisePropertyChanged(() => FPS); }
        }
        /// <summary>
        /// 扫描帧时间
        /// </summary>
        public double FrameTime
        {
            get { return frameTime; }
            set { frameTime = value; RaisePropertyChanged(() => FrameTime); }
        }
        /// <summary>
        /// 当前帧
        /// </summary>
        public long CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; RaisePropertyChanged(() => CurrentFrame); }
        }
        /// <summary>
        /// 当前Bank
        /// </summary>
        public int CurrentBank
        {
            get { return currentBank; }
            set { currentBank = value; }
        }
        /// <summary>
        /// 单帧中包含的Bank数量
        /// </summary>
        public int NumOfBank
        {
            get { return numOfBank; }
            set { numOfBank = value; RaisePropertyChanged(() => NumOfBank); }
        }

        public ScanInfoModel(int numOfBank)
        {
            TimeSpan = 0.0;
            FPS = 0.0;
            FrameTime = 0.0;
            CurrentFrame = 0;
            CurrentBank = 0;
            NumOfBank = numOfBank;
        }
        
    }
}
