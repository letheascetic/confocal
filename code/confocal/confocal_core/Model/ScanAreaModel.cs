using confocal_core.Properties;
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
    /// 扫描范围变化事件委托
    /// </summary>
    /// <param name="scanRange"></param>
    /// <returns></returns>
    public delegate API_RETURN_CODE ScanRangeChangedEventHandler(ScanAreaModel scanRange);

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
        private static readonly int EXTEND_LINE_TIME_DEFAULT = 100;
        private static readonly int EXTEND_ROW_COUNT_DEFAULT = 0;
        ///////////////////////////////////////////////////////////////////////////////////////////

        private RectangleF scanRange;
        private string text;

        /// <summary>
        /// 扫描范围
        /// </summary>
        public RectangleF ScanRange
        {
            get { return scanRange; }
            set { scanRange = value; RaisePropertyChanged(() => ScanRange); }
        }

        public string Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(() => Text); }
        }

        public ScanAreaModel(RectangleF scanRange)
        {
            ScanRange = scanRange;
            Text = string.Format("[{0}, {1}][{2}, {3}]", ScanRange.X.ToString("0.0"), ScanRange.Y.ToString("0.0"),
                ScanRange.Width.ToString("0.0"), ScanRange.Height.ToString("0.0"));
        }

        public void Update(RectangleF scanRange)
        {
            ScanRange = scanRange;
            Text = string.Format("[{0}, {1}][{2}, {3}]", ScanRange.X.ToString("0.0"), ScanRange.Y.ToString("0.0"), 
                ScanRange.Width.ToString("0.0"), ScanRange.Height.ToString("0.0"));
        }

        public static ScanAreaModel CreateFullScanArea()
        {
            float fullScanRange = Settings.Default.FullScanRange;
            return new ScanAreaModel(new RectangleF(-fullScanRange / 2, -fullScanRange / 2, fullScanRange, fullScanRange));
        }
    }
}
