using confocal_core.Model;
using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_ui.ViewModel
{
    public class ScanAreaViewModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private List<ScanPixelModel> scanPixelList;
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
            get { return ScanPixelList.Where(p => p.IsEnabled).First(); }
        }

        public ScanAreaViewModel()
        {
            // 扫描像素
            ScanPixelList = ScanPixelModel.Initialize();
        }
    }
}
