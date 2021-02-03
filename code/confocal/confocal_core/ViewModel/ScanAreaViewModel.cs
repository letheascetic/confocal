using confocal_core.Common;
using confocal_core.Model;
using Emgu.CV;
using Emgu.CV.CvEnum;
using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace confocal_core.ViewModel
{
    
    public class ScanAreaViewModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        // private readonly ConfigViewModel mConfig;
        private readonly Scheduler mScheduler;

        public Scheduler Engine
        {
            get { return mScheduler; }
        }

        private int pixelDwell;
        private int scanWidth;
        private int scanHeight;
        private int zoomFactor;
        private Mat scanImage;

        public int PixelDwell
        {
            get { return pixelDwell; }
            set { pixelDwell = value; RaisePropertyChanged(() => PixelDwell); }
        }
        /// <summary>
        /// 扫描宽度
        /// </summary>
        public int ScanWidth
        {
            get { return scanWidth; }
            set { scanWidth = value; RaisePropertyChanged(() => ScanWidth); }
        }
        /// <summary>
        /// 扫描高度
        /// </summary>
        public int ScanHeight
        {
            get { return scanHeight; }
            set { scanHeight = value; RaisePropertyChanged(() => ScanHeight); }
        }
        /// <summary>
        /// 扫描区域缩放因子
        /// </summary>
        public int ZoomFactor
        {
            get { return zoomFactor; }
            set { zoomFactor = value; RaisePropertyChanged(() => ZoomFactor); }
        }
        /// <summary>
        /// 扫描图像
        /// </summary>
        public Mat ScanImage
        {
            get { return scanImage; }
            set { scanImage = value; RaisePropertyChanged(() => ScanImage); }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        public ScanAreaViewModel()
        {
            mScheduler = Scheduler.CreateInstance();

            // 初始化扫描宽度[X方向像素数]和高度[Y方向像素数]
            PixelDwell = mScheduler.Config.SelectedScanPixelDwell.Data;
            ScanWidth = mScheduler.Config.SelectedScanPixel.Data;
            ScanHeight = mScheduler.Config.SelectedScanPixel.Data;
 
            ZoomFactor = 5;
            ScanImage = new Mat(ScanWidth, ScanHeight, DepthType.Cv8U, 3);

            // 绑定事件
            mScheduler.ScanPixelChangedEvent += ScanPixelChangedHandler;
            mScheduler.ScanPixelDwellChangedEvent += ScanPixelDwellChangedHandler;
        }

        /// <summary>
        /// 扫描像素范围转换成扫描范围
        /// </summary>
        /// <param name="scanPixelRange"></param>
        /// <returns></returns>
        public RectangleF ScanPixelRangeToScanRange(Rectangle scanPixelRange)
        {
            float x = mScheduler.Config.SelectedScanArea.ScanRange.X + mScheduler.Config.ScanPixelSize * scanPixelRange.X;
            float y = mScheduler.Config.SelectedScanArea.ScanRange.Y + mScheduler.Config.ScanPixelSize * scanPixelRange.Y;
            float width = mScheduler.Config.ScanPixelSize * scanPixelRange.Width;
            float height = mScheduler.Config.ScanPixelSize * scanPixelRange.Height;
            return new RectangleF(x, y, width, height);
        }

        /// <summary>
        /// 扫描范围转换成扫描像素范围
        /// </summary>
        /// <param name="scanRange"></param>
        /// <returns></returns>
        public Rectangle ScanRangeToScanPixelRange(RectangleF scanRange)
        {
            int x = (int)((scanRange.X - mScheduler.Config.SelectedScanArea.ScanRange.X) / mScheduler.Config.ScanPixelSize);
            int y = (int)((scanRange.Y - mScheduler.Config.SelectedScanArea.ScanRange.Y) / mScheduler.Config.ScanPixelSize);
            int width = (int)(scanRange.Width / mScheduler.Config.ScanPixelSize);
            int height = (int)(scanRange.Height / mScheduler.Config.ScanPixelSize);
            return new Rectangle(x, y, width, height);
        }

        public API_RETURN_CODE ScanPixelChangedHandler(ScanPixelModel scanPixel)
        {
            ScanWidth = scanPixel.Data;
            ScanHeight = scanPixel.Data;
            ScanImage = new Mat(ScanWidth, ScanHeight, DepthType.Cv8U, 3);
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ScanPixelDwellChangedHandler(ScanPixelDwellModel scanPixelDwell)
        {
            PixelDwell = scanPixelDwell.Data;
            return API_RETURN_CODE.API_SUCCESS;
        }

    }
}
