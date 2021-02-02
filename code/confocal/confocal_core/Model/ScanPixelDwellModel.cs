using confocal_core.Common;
using confocal_core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{    
    /// <summary>
     /// 扫描像素时间更新事件委托
     /// </summary>
     /// <param name="scanPixelDwell"></param>
     /// <returns></returns>
    public delegate API_RETURN_CODE ScanPixelDwellChangedEventHandler(ScanPixelDwellModel scanPixelDwell);

    /// <summary>
    /// 扫描像素停留时间
    /// </summary>
    public class ScanPixelDwellModel : ScanPropertyWithValueBaseModel<int>
    {
        private int scanPixelCalibration;
        private int scanPixelOffset;
        private int scanPixelCalibrationMaximum;

        public int ScanPixelCalibration
        {
            get { return scanPixelCalibration; }
            set { scanPixelCalibration = value; RaisePropertyChanged(() => ScanPixelCalibration); }
        }
        /// <summary>
        /// 扫描像素偏置
        /// </summary>
        public int ScanPixelOffset
        {
            get { return scanPixelOffset; }
            set { scanPixelOffset = value; RaisePropertyChanged(() => ScanPixelOffset); }
        }
        /// <summary>
        /// 扫描像素补偿最大值
        /// </summary>
        public int ScanPixelCalibrationMaximum
        {
            get { return scanPixelCalibrationMaximum; }
            set { scanPixelCalibrationMaximum = value; RaisePropertyChanged(() => ScanPixelCalibrationMaximum); }
        }

        public static List<ScanPixelDwellModel> Initialize()
        {
            return new List<ScanPixelDwellModel>()
            {
                new ScanPixelDwellModel(){ ID = 0, IsEnabled = Settings.Default.ScanPixelDwell == 0, Text = "2", Data = 2,
                    ScanPixelCalibrationMaximum = 50, ScanPixelOffset = 25, ScanPixelCalibration = 25},
                new ScanPixelDwellModel(){ ID = 1, IsEnabled = Settings.Default.ScanPixelDwell == 1, Text = "4", Data = 4,
                    ScanPixelCalibrationMaximum = 24, ScanPixelOffset = 12, ScanPixelCalibration = 12},
                new ScanPixelDwellModel(){ ID = 2, IsEnabled = Settings.Default.ScanPixelDwell == 2, Text = "6", Data = 6,
                    ScanPixelCalibrationMaximum = 16, ScanPixelOffset = 8, ScanPixelCalibration = 8},
                new ScanPixelDwellModel(){ ID = 3, IsEnabled = Settings.Default.ScanPixelDwell == 3, Text = "8", Data = 8,
                    ScanPixelCalibrationMaximum = 12, ScanPixelOffset = 6, ScanPixelCalibration = 6},
                new ScanPixelDwellModel(){ ID = 4, IsEnabled = Settings.Default.ScanPixelDwell == 4, Text = "10", Data = 10,
                    ScanPixelCalibrationMaximum = 10, ScanPixelOffset = 5, ScanPixelCalibration = 5},
                new ScanPixelDwellModel(){ ID = 5, IsEnabled = Settings.Default.ScanPixelDwell == 5, Text = "20", Data = 20,
                    ScanPixelCalibrationMaximum = 4, ScanPixelOffset = 2, ScanPixelCalibration = 2},
                new ScanPixelDwellModel(){ ID = 6, IsEnabled = Settings.Default.ScanPixelDwell == 6, Text = "50", Data = 50,
                    ScanPixelCalibrationMaximum = 2, ScanPixelOffset = 1, ScanPixelCalibration = 1},
                new ScanPixelDwellModel(){ ID = 7, IsEnabled = Settings.Default.ScanPixelDwell == 7, Text = "100", Data = 100,
                    ScanPixelCalibrationMaximum = 0, ScanPixelOffset = 0, ScanPixelCalibration = 0}
            };
        }

    }

}
