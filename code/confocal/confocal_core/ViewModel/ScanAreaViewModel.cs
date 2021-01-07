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

        public ScanAreaViewModel()
        {
            // 扫描像素
            ScanPixelList = ScanPixelModel.Initialize();
            SelectedScanPixel = ScanPixelList.Where(p => p.IsEnabled).First();
        }

    }
}
