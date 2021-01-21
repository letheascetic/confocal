using confocal_core.Common;
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
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int SQUARE = 0;
        public static readonly int BANK = 1;
        public static readonly int LINE = 2;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static ScanAreaTypeModel Initialize(int id)
        {
            if (id == SQUARE)
            {
                return new ScanAreaTypeModel() { ID = SQUARE, Text = "方形", IsEnabled = true };
            }
            else if (id == BANK)
            {
                return new ScanAreaTypeModel() { ID = BANK, Text = "矩形", IsEnabled = false };
            }
            else if (id == LINE)
            {
                return new ScanAreaTypeModel() { ID = LINE, Text = "线条", IsEnabled = false };
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
        /// <summary>
        /// 行[X方向]扩展时间[单位：us]
        /// </summary>
        public static int ExtendLineTime { get; set; }
        /// <summary>
        /// [Y方向]扩展的行数
        /// </summary>
        public static int ExtendRowCount { get; set; }
        /// <summary>
        /// 行偏置时间[单位：us]
        /// </summary>
        public static int ExtendLineOffset { get; set; }
        /// <summary>
        /// [Y方向]的偏置
        /// </summary>
        public static int ExtendRowOffset { get; set; }
        /// <summary>
        /// 双向扫描中奇数偶数行错位的像素数
        /// </summary>
        public static int ScanPixelCalibration { get; set; }
        /// <summary>
        /// 行边缘区域除数因子
        /// </summary>
        public static double ExtendLineMarginDiv { get; set; }
        /// <summary>
        /// 扫描行起始时间[单双向有效]
        /// </summary>
        public static double ScanLineStartTime { get { return Settings.Default.GalvoResponseTime * 2; } }
        /// <summary>
        /// 扫描行保持时间[只对单向扫描有效]
        /// </summary>
        public static double ScanLineHoldTime { get { return Settings.Default.GalvoResponseTime; } }
        /// <summary>
        /// 扫描行结束时间[只对单向扫描有效]
        /// </summary>
        public static double ScanLineEndTime { get { return Settings.Default.GalvoResponseTime; } }

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

        ///////////////////////////////////////////////////////////////////////////////////////////
        static ScanAreaModel()
        {
            ExtendLineTime = EXTEND_LINE_TIME_DEFAULT;
            ExtendRowCount = EXTEND_ROW_COUNT_DEFAULT;
            ExtendLineOffset = 0;
            ExtendRowOffset = 0;
            ScanPixelCalibration = 0;
            ExtendLineMarginDiv = 50;
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
