using Emgu.CV;
using Emgu.CV.CvEnum;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    /// <summary>
    /// 扫描区域类型
    /// </summary>
    public class ScanAreaTypeModel : ScanPropertyBaseModel
    {
        public static ScanAreaTypeModel Initialize(int id)
        {
            if (id == 0)
            {
                return new ScanAreaTypeModel() { ID = 0, Text = "方形", IsEnabled = true };
            }
            else if (id == 1)
            {
                return new ScanAreaTypeModel() { ID = 1, Text = "矩形", IsEnabled = false };
            }
            else
            {
                throw new ArgumentOutOfRangeException("ID Exception");
            }
        }
    }

    /// <summary>
    /// 扫描区域
    /// </summary>
    public class ScanAreaModel : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly float FULL_FIELD_DEFAULT = 200.0F;
        private static readonly int EXTEND_LINE_TIME_DEFAULT = 100;
        private static readonly int EXTEND_ROW_COUNT_DEFAULT = 0;
        ///////////////////////////////////////////////////////////////////////////////////////////

        private RectangleF fullScanRange;
        private ScanAreaTypeModel scanAreaType;
        private RectangleF selectedScanRange;

        /// <summary>
        /// 扫描范围类型
        /// </summary>
        public ScanAreaTypeModel ScanAreaType
        {
            get { return scanAreaType; }
            set { scanAreaType = value; RaisePropertyChanged(() => ScanAreaType); }
        }
        /// <summary>
        /// 全扫描范围
        /// </summary>
        public RectangleF FullScanRange
        {
            get { return fullScanRange; }
            set { fullScanRange = value; RaisePropertyChanged(() => FullScanRange); }
        }
        /// <summary>
        /// 当前选择的扫描范围
        /// </summary>
        public RectangleF SelectedScanRange
        {
            get { return selectedScanRange; }
            set { selectedScanRange = value; RaisePropertyChanged(() => SelectedScanRange); }
        }

        public ScanAreaModel()
        {
            ScanAreaType = ScanAreaTypeModel.Initialize(0);
            FullScanRange = new RectangleF(-FULL_FIELD_DEFAULT / 2, -FULL_FIELD_DEFAULT / 2, FULL_FIELD_DEFAULT, FULL_FIELD_DEFAULT);
            SelectedScanRange = new RectangleF(-FULL_FIELD_DEFAULT / 2, -FULL_FIELD_DEFAULT / 2, FULL_FIELD_DEFAULT, FULL_FIELD_DEFAULT);
        }

    }
}
