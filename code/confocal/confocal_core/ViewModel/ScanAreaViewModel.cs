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
        private List<ScanAreaTypeModel> scanAreaTypeList;

        /// <summary>
        /// 扫描区域类型列表
        /// </summary>
        public List<ScanAreaTypeModel> ScaAreaTypeList
        {
            get { return scanAreaTypeList; }
            set { scanAreaTypeList = value; RaisePropertyChanged(() => scanAreaTypeList); }
        }
        /// <summary>
        /// 选择的扫描区域类型
        /// </summary>
        public ScanAreaTypeModel SelectedScanAreaType
        {
            get { return ScaAreaTypeList.Where(p => p.IsEnabled).First(); }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanPixelModel> scanPixelList;
        private ScanPixelModel selectedScanPixel;

        /// <summary>
        /// 扫描像素列表
        /// </summary>
        public List<ScanPixelModel> ScanPixelList
        {
            get { return scanPixelList; }
            set { scanPixelList = value; RaisePropertyChanged(() => ScanPixelList); }
        }
        /// <summary>
        /// 选择的扫描像素 
        /// </summary>
        public ScanPixelModel SelectedScanPixel
        {
            get { return selectedScanPixel; }
            set { selectedScanPixel = value; RaisePropertyChanged(() => SelectedScanPixel); }
        }

        /// <summary>
        /// 扫描像素切换事件
        /// </summary>
        /// <param name="selectedScanPixel"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelChangeCommand(ScanPixelModel selectedScanPixel)
        {
            foreach (ScanPixelModel scanPixel in ScanPixelList)
            {
                if (scanPixel.ID != selectedScanPixel.ID)
                {
                    scanPixel.IsEnabled = false;
                }
                else
                {
                    SelectedScanPixel = scanPixel;
                    SelectedScanPixel.IsEnabled = true;
                }
            }
            ScanWidth = SelectedScanPixel.Data;
            ScanHeight = SelectedScanPixel.Data;
            ScanPixelSize = SelectedScanArea.ScanRange.Width / ScanWidth;
            Logger.Info(string.Format("Scan Pixel [{0}], Pixel Size [{1}].", SelectedScanPixel.Text, ScanPixelSize));
            return API_RETURN_CODE.API_SUCCESS;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScanAreaModel selectedScanArea;
        private ScanAreaModel fullScanArea;
        private Mat scanImage;
        private float scanPixelSize;
        private int scanPixelDwell;
        private int scanWidth;
        private int scanHeight;

        private int zoomFactor;

        /// <summary>
        /// 当前选择的扫描区域
        /// </summary>
        public ScanAreaModel SelectedScanArea
        {
            get { return selectedScanArea; }
            set { selectedScanArea = value; RaisePropertyChanged(() => SelectedScanArea); }
        }
        /// <summary>
        /// 全视场
        /// </summary>
        public ScanAreaModel FullScanArea
        {
            get { return fullScanArea; }
            set { fullScanArea = value; RaisePropertyChanged(() => FullScanArea); }
        }
        /// <summary>
        /// 扫描像素尺寸
        /// </summary>
        public float ScanPixelSize
        {
            get { return scanPixelSize; }
            set { scanPixelSize = value; RaisePropertyChanged(() => ScanPixelSize); }
        }
        /// <summary>
        /// 扫描像素时间
        /// </summary>
        public int ScanPixelDwell
        {
            get { return scanPixelDwell; }
            set { scanPixelDwell = value; RaisePropertyChanged(() => ScanPixelDwell); }
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
        /// 扫描图像
        /// </summary>
        public Mat ScanImage
        {
            get { return scanImage; }
            set { scanImage = value; RaisePropertyChanged(() => ScanImage); }
        }
        /// <summary>
        /// 扫描区域缩放因子
        /// </summary>
        public int ZoomFactor
        {
            get { return zoomFactor; }
            set { zoomFactor = value; RaisePropertyChanged(() => ZoomFactor); }
        }

        public API_RETURN_CODE ScanPixelDwellChangeCommand(ScanPixelDwellModel scanPixelDwell)
        {
            ScanPixelDwell = scanPixelDwell.Data;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ScanRangeChangeCommand(ScanAreaModel scanRange)
        {
            SelectedScanArea.Update(scanRange.ScanRange);
            ScanPixelSize = SelectedScanArea.ScanRange.Width / ScanWidth;
            Logger.Info(string.Format("Selected Scan Range [{0}].", SelectedScanArea.ScanRange));
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        
        public ScanAreaViewModel()
        {
            // 初始化扫描区域类型
            ScaAreaTypeList = new List<ScanAreaTypeModel>()
            {
                ScanAreaTypeModel.Initialize(0),
                ScanAreaTypeModel.Initialize(1)
            };
            // 初始化扫描范围
            FullScanArea = ScanAreaModel.CreateFullScanArea();
            SelectedScanArea = ScanAreaModel.CreateFullScanArea();
            // 初始化扫描像素
            ScanPixelList = ScanPixelModel.Initialize();
            SelectedScanPixel = ScanPixelList.Where(p => p.IsEnabled).First();
            // 初始化扫描宽度[X方向像素数]和高度[Y方向像素数]
            ScanWidth = SelectedScanPixel.Data;
            ScanHeight = SelectedScanPixel.Data;
            // 初始化像素停留时间、扫描图像、像素尺寸
            ScanPixelDwell = 8;
            ScanImage = new Mat(ScanWidth, ScanHeight, DepthType.Cv8U, 3);
            ScanPixelSize = SelectedScanArea.ScanRange.Width / ScanWidth;

            ZoomFactor = 5;
        }

        /// <summary>
        /// 扫描像素范围转换成扫描范围
        /// </summary>
        /// <param name="scanPixelRange"></param>
        /// <returns></returns>
        public RectangleF ScanPixelRangeToScanRange(Rectangle scanPixelRange)
        {
            float x = SelectedScanArea.ScanRange.X + ScanPixelSize * scanPixelRange.X;
            float y = SelectedScanArea.ScanRange.Y + ScanPixelSize * scanPixelRange.Y;
            float width = ScanPixelSize * scanPixelRange.Width;
            float height = ScanPixelSize * scanPixelRange.Height;
            return new RectangleF(x, y, width, height);
        }

        /// <summary>
        /// 扫描范围转换成扫描像素范围
        /// </summary>
        /// <param name="scanRange"></param>
        /// <returns></returns>
        public Rectangle ScanRangeToScanPixelRange(RectangleF scanRange)
        {
            int x = (int)((scanRange.X - SelectedScanArea.ScanRange.X) / ScanPixelSize);
            int y = (int)((scanRange.Y - SelectedScanArea.ScanRange.Y) / ScanPixelSize);
            int width = (int)(scanRange.Width / ScanPixelSize);
            int height = (int)(scanRange.Height / ScanPixelSize);
            return new Rectangle(x, y, width, height);
        }

    }
}
