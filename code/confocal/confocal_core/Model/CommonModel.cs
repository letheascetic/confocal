using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    public class CommonModel : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private volatile static CommonModel pCommon = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////

        private string laserPortName;
        /// <summary>
        /// 激光端口
        /// </summary>
        public string LaserPortName
        {
            get { return laserPortName; }
            set { laserPortName = value; RaisePropertyChanged(() => LaserPortName); }
        }

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
        /// 选中的扫描像素
        /// </summary>
        public ScanPixelModel SelectedScanPixel
        {
            get { return selectedScanPixel; }
            set { selectedScanPixel = value; RaisePropertyChanged(() => SelectedScanPixel); }
        }

        public CommonModel GetCommonModel()
        {
            if (pCommon == null)
            {
                lock (locker)
                {
                    if (pCommon == null)
                    {
                        pCommon = new CommonModel();
                    }
                }
            }
            return pCommon;
        }

        private CommonModel()
        {
            ScanPixelList = ScanPixelModel.Initialize();
            SelectedScanPixel = ScanPixelList.Where(p => p.IsEnabled).First();
        }

    }
}
