using confocal_core.Model;
using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
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
            Logger.Info(string.Format("Scan Pixel [{0}].", SelectedScanPixel.Text));
            return API_RETURN_CODE.API_SUCCESS;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////

        private float scanPixelSize;
        private int scanPixelDwell;
        private int scanWidth;
        private int scanHeight;

        /// <summary>
        /// 扫描像素尺寸
        /// </summary>
        public float ScanPixelSize
        {
            get { return scanPixelSize; }
            set { scanPixelSize = value; RaisePropertyChanged(() => ScanPixelSize); }
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
            set { scanHeight = value; RaisePropertyChanged(() => scanHeight); }
        }
        /// <summary>
        /// 扫描像素时间
        /// </summary>
        public int ScanPixelDwell
        {
            get { return scanPixelDwell; }
            set { scanPixelDwell = value; RaisePropertyChanged(() => ScanPixelDwell); }
        }

        public API_RETURN_CODE ScanPixelDwellChangeCommand(ScanPixelDwellModel scanPixelDwell)
        {
            ScanPixelDwell = scanPixelDwell.Data;
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        
        public ScanAreaViewModel()
        {
            // 扫描像素
            ScanPixelList = ScanPixelModel.Initialize();
            SelectedScanPixel = ScanPixelList.Where(p => p.IsEnabled).First();
            ScanWidth = SelectedScanPixel.Data;
            ScanHeight = SelectedScanPixel.Data;
            ScanPixelDwell = 8;
        }

    }
}
