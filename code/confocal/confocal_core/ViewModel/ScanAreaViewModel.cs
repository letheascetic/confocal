﻿using confocal_core.Common;
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
        private readonly ConfigViewModel mConfig;

        public ConfigViewModel Config
        {
            get { return mConfig; }
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
            mConfig = ConfigViewModel.GetConfig();

            // 初始化扫描宽度[X方向像素数]和高度[Y方向像素数]
            PixelDwell = mConfig.SelectedScanPixelDwell.Data;
            ScanWidth = mConfig.SelectedScanPixel.Data;
            ScanHeight = mConfig.SelectedScanPixel.Data;
 
            ZoomFactor = 5;
            ScanImage = new Mat(ScanWidth, ScanHeight, DepthType.Cv8U, 3);

            // 绑定事件
            mConfig.ScanPixelChangedEvent += ScanPixelChangedHandler;
            mConfig.ScanPixelDwellChangedEvent += ScanPixelDwellChangedHandler;
        }

        /// <summary>
        /// 扫描像素范围转换成扫描范围
        /// </summary>
        /// <param name="scanPixelRange"></param>
        /// <returns></returns>
        public RectangleF ScanPixelRangeToScanRange(Rectangle scanPixelRange)
        {
            float x = Config.SelectedScanArea.ScanRange.X + Config.ScanPixelSize * scanPixelRange.X;
            float y = Config.SelectedScanArea.ScanRange.Y + Config.ScanPixelSize * scanPixelRange.Y;
            float width = Config.ScanPixelSize * scanPixelRange.Width;
            float height = Config.ScanPixelSize * scanPixelRange.Height;
            return new RectangleF(x, y, width, height);
        }

        /// <summary>
        /// 扫描范围转换成扫描像素范围
        /// </summary>
        /// <param name="scanRange"></param>
        /// <returns></returns>
        public Rectangle ScanRangeToScanPixelRange(RectangleF scanRange)
        {
            int x = (int)((scanRange.X - Config.SelectedScanArea.ScanRange.X) / Config.ScanPixelSize);
            int y = (int)((scanRange.Y - Config.SelectedScanArea.ScanRange.Y) / Config.ScanPixelSize);
            int width = (int)(scanRange.Width / Config.ScanPixelSize);
            int height = (int)(scanRange.Height / Config.ScanPixelSize);
            return new Rectangle(x, y, width, height);
        }

        public API_RETURN_CODE ScanPixelChangedHandler(ScanPixelModel scanPixel)
        {
            ScanWidth = scanPixel.Data;
            ScanHeight = scanPixel.Data;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ScanPixelDwellChangedHandler(ScanPixelDwellModel scanPixelDwell)
        {
            PixelDwell = scanPixelDwell.Data;
            return API_RETURN_CODE.API_SUCCESS;
        }

    }
}
