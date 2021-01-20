using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    public class SysSettingsModel : ObservableObject
    {
        private GalvoPrppertyModel mGalvoPrpperty;
        private ScanAreaModel mFullScanArea;
        private DetectorModel mDetector;

        /// <summary>
        /// 振镜属性
        /// </summary>
        public GalvoPrppertyModel GalvoProperty
        {
            get { return mGalvoPrpperty; }
            set { mGalvoPrpperty = value; RaisePropertyChanged(() => GalvoProperty); }
        }
        /// <summary>
        /// 最大扫描视场范围
        /// </summary>
        public ScanAreaModel FullScanArea
        {
            get { return mFullScanArea; }
            set { mFullScanArea = value; RaisePropertyChanged(() => FullScanArea); }
        }
        /// <summary>
        /// 探测器属性
        /// </summary>
        public DetectorModel Detector
        {
            get { return mDetector; }
            set { mDetector = value; RaisePropertyChanged(() => Detector); }
        }

    }
}
