using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Common
{
    /// <summary>
    /// 振镜属性
    /// </summary>
    public class GalvanoProperty
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly double GALV_RESPONSE_TIME_DEFAULT = 200.0;              // 振镜响应时间, us
        private static readonly double CALIBRATION_VOLTAGE_DEFAULT = 5.848e-5 * 1000;   // 校准[标定]电压,V/um
        private static readonly double CALIBRATION_FACTOR_DEFAULT = 1.0;                // 校准系数
        private static readonly double XOFFSET_VOLTAGE_DEFAULT = 0;                     // X=0位置对应的偏置电压
        private static readonly double YOFFSET_VOLTAGE_DEFAULT = 0;                     // Y=0位置对应的偏置电压
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// X轴原点坐标对应的振镜电压偏置
        /// </summary>
        public static double XOffsetVoltage { get; set; }
        /// <summary>
        /// Y轴原点坐标对应的振镜电压偏置
        /// </summary>
        public static double YOffsetVoltage { get; set; }
        /// <summary>
        /// 振镜响应时间
        /// </summary>
        public static double GalvanoResponseTime { get; set; }
        /// <summary>
        /// 振镜校准电压，单位：V/um
        /// </summary>
        public static double GalvanoCalibrationVoltage { get; set; }
        /// <summary>
        /// 振镜校准系数
        /// </summary>
        public static double GalvanoCalibrationFactor { get; set; }

        static GalvanoProperty()
        {
            XOffsetVoltage = XOFFSET_VOLTAGE_DEFAULT;
            YOffsetVoltage = YOFFSET_VOLTAGE_DEFAULT;
            GalvanoResponseTime = GALV_RESPONSE_TIME_DEFAULT;
            GalvanoCalibrationVoltage = CALIBRATION_VOLTAGE_DEFAULT;
            GalvanoCalibrationFactor = CALIBRATION_FACTOR_DEFAULT;
        }

        /// <summary>
        /// X坐标->X振镜电压
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <returns></returns>
        public static double XCoordinateToVoltage(double xCoordinate)
        {
            return xCoordinate * GalvanoCalibrationVoltage * GalvanoCalibrationFactor + XOffsetVoltage;
        }

        /// <summary>
        /// X坐标序列->X振镜电压序列
        /// </summary>
        /// <param name="xCoordinates"></param>
        /// <returns></returns>
        public static double[] XCoordinateToVoltage(double[] xCoordinates)
        {
            double coff = GalvanoCalibrationVoltage * GalvanoCalibrationFactor;
            double[] xVoltages = new double[xCoordinates.Length];
            for (int i = 0; i < xCoordinates.Length; i++)
            {
                xVoltages[i] = xCoordinates[i] * coff + XOffsetVoltage;
            }
            return xVoltages;
        }

        /// <summary>
        /// Y坐标->Y振镜电压
        /// </summary>
        /// <param name="yCoordinate"></param>
        /// <returns></returns>
        public static double YCoordinateToVoltage(double yCoordinate)
        {
            return yCoordinate * GalvanoCalibrationVoltage * GalvanoCalibrationFactor + YOffsetVoltage;
        }

        /// <summary>
        /// Y坐标序列->Y振镜电压序列
        /// </summary>
        /// <param name="yCoordinates"></param>
        /// <returns></returns>
        public static double[] YCoordinateToVoltage(double[] yCoordinates)
        {
            double coff = GalvanoCalibrationVoltage * GalvanoCalibrationFactor;
            double[] yVoltages = new double[yCoordinates.Length];
            for (int i = 0; i < yCoordinates.Length; i++)
            {
                yVoltages[i] = yCoordinates[i] * coff + YOffsetVoltage;
            }
            return yVoltages;
        }

        /// <summary>
        /// X振镜电压->X坐标
        /// </summary>
        /// <param name="xVoltage"></param>
        /// <returns></returns>
        public static double XVoltageToCoordinate(double xVoltage)
        {
            return (xVoltage - XOffsetVoltage) / GalvanoCalibrationFactor / GalvanoCalibrationVoltage;
        }

        /// <summary>
        /// X振镜电压序列->X坐标序列
        /// </summary>
        /// <param name="xVoltages"></param>
        /// <returns></returns>
        public static double[] XVoltageToCoordinate(double[] xVoltages)
        {
            double coff = GalvanoCalibrationFactor * GalvanoCalibrationVoltage;
            double[] xCoordinates = new double[xVoltages.Length];
            for (int i = 0; i < xVoltages.Length; i++)
            {
                xCoordinates[i] = (xVoltages[i] - XOffsetVoltage) / coff;
            }
            return xCoordinates;
        }

        /// <summary>
        /// Y振镜电压->Y坐标
        /// </summary>
        /// <param name="yVoltage"></param>
        /// <returns></returns>
        public static double YVoltageToCoordinate(double yVoltage)
        {
            return (yVoltage - YOffsetVoltage) / GalvanoCalibrationFactor / GalvanoCalibrationVoltage;
        }

        /// <summary>
        /// Y振镜电压序列->Y坐标序列
        /// </summary>
        /// <param name="yVoltages"></param>
        /// <returns></returns>
        public static double[] YVoltageToCoordinate(double[] yVoltages)
        {
            double coff = GalvanoCalibrationFactor * GalvanoCalibrationVoltage;
            double[] yCoordinates = new double[yVoltages.Length];
            for (int i = 0; i < yVoltages.Length; i++)
            {
                yCoordinates[i] = (yVoltages[i] - YOffsetVoltage) / coff;
            }
            return yCoordinates;
        }

    }
}
