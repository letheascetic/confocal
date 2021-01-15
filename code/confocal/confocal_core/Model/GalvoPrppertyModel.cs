using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    public class GalvoPrppertyModel : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly double GALV_RESPONSE_TIME_DEFAULT = 200.0;              // 振镜响应时间, us
        private static readonly double CALIBRATION_VOLTAGE_DEFAULT = 5.848e-5 * 1000;   // 校准[标定]电压,V/um
        private static readonly double XOFFSET_VOLTAGE_DEFAULT = 0;                     // X=0位置对应的偏置电压
        private static readonly double YOFFSET_VOLTAGE_DEFAULT = 0;                     // Y=0位置对应的偏置电压
        ///////////////////////////////////////////////////////////////////////////////////////////
        private double xGalvoOffsetVoltage;
        private double yGalvoOffsetVoltage;
        private double y2GalvoOffsetVoltage;

        private double galvoResponseTime;
        private double xGalvoCalibrationVoltage;
        private double yGalvoCalibrationVoltage;

        /// <summary>
        /// X振镜偏置电压
        /// </summary>
        public double XGalvoOffsetVoltage
        {
            get { return xGalvoOffsetVoltage; }
            set { xGalvoOffsetVoltage = value; RaisePropertyChanged(() => XGalvoOffsetVoltage); }
        }
        /// <summary>
        /// Y振镜偏置电压
        /// </summary>
        public double YGalvoOffsetVoltage
        {
            get { return yGalvoOffsetVoltage; }
            set { yGalvoOffsetVoltage = value; RaisePropertyChanged(() => YGalvoOffsetVoltage); }
        }
        /// <summary>
        /// Y2振镜偏置电压
        /// </summary>
        public double Y2GalvoOffsetVoltage
        {
            get { return y2GalvoOffsetVoltage; }
            set { y2GalvoOffsetVoltage = value; RaisePropertyChanged(() => Y2GalvoOffsetVoltage); }
        }
        /// <summary>
        /// 振镜响应时间
        /// </summary>
        public double GalvoResponseTime
        {
            get { return galvoResponseTime; }
            set { galvoResponseTime = value; RaisePropertyChanged(() => GalvoResponseTime); }
        }
        /// <summary>
        /// X振镜校准电压
        /// </summary>
        public double XGalvoCalibrationVoltage
        {
            get { return xGalvoCalibrationVoltage; }
            set { xGalvoCalibrationVoltage = value; RaisePropertyChanged(() => XGalvoCalibrationVoltage); }
        }
        /// <summary>
        /// Y振镜校准电压
        /// </summary>
        public double YGalvoCalibrationVoltage
        {
            get { return yGalvoCalibrationVoltage; }
            set { yGalvoCalibrationVoltage = value; RaisePropertyChanged(() => YGalvoCalibrationVoltage); }
        }

        public GalvoPrppertyModel()
        {
            XGalvoOffsetVoltage = XOFFSET_VOLTAGE_DEFAULT;
            YGalvoOffsetVoltage = YOFFSET_VOLTAGE_DEFAULT;
            Y2GalvoOffsetVoltage = YOFFSET_VOLTAGE_DEFAULT;
            GalvoResponseTime = GALV_RESPONSE_TIME_DEFAULT;
            XGalvoCalibrationVoltage = CALIBRATION_VOLTAGE_DEFAULT;
            YGalvoCalibrationVoltage = CALIBRATION_VOLTAGE_DEFAULT;
        }

    }
}
