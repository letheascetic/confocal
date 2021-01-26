using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using confocal_core.Common;
using confocal_core.Properties;

namespace confocal_core.Model
{
    public class GalvoPropertyModel : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private double xGalvoOffsetVoltage;
        private double yGalvoOffsetVoltage;
        private double y2GalvoOffsetVoltage;

        private double galvoResponseTime;
        private double xGalvoCalibrationVoltage;
        private double yGalvoCalibrationVoltage;

        private string xGalvoChannel;       // X振镜控制电压 - AO输出
        private string yGalvoAoChannel;     // Y振镜控制电压 - AO输出
        private string y2GalvoAoChannel;    // Y补偿镜控制电压 - AO输出

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
        /// <summary>
        /// X振镜模拟输出通道
        /// </summary>
        public string XGalvoAoChannel
        {
            get { return xGalvoChannel; }
            set { xGalvoChannel = value; RaisePropertyChanged(() => XGalvoAoChannel); }
        }
        /// <summary>
        /// Y振镜模拟输出通道
        /// </summary>
        public string YGalvoAoChannel
        {
            get { return yGalvoAoChannel; }
            set { yGalvoAoChannel = value; RaisePropertyChanged(() => YGalvoAoChannel); }
        }
        /// <summary>
        /// Y2补偿镜模拟输出通道
        /// </summary>
        public string Y2GalvoAoChannel
        {
            get { return y2GalvoAoChannel; }
            set { y2GalvoAoChannel = value; RaisePropertyChanged(() => Y2GalvoAoChannel); }
        }

        public GalvoPropertyModel()
        {
            XGalvoOffsetVoltage = Settings.Default.XGalvoOffsetVoltage;
            YGalvoOffsetVoltage = Settings.Default.YGalvoOffsetVoltage;
            Y2GalvoOffsetVoltage = Settings.Default.YGalvoOffsetVoltage;
            GalvoResponseTime = Settings.Default.GalvoResponseTime;
            XGalvoCalibrationVoltage = Settings.Default.XGalvoCalibrationVoltage;
            YGalvoCalibrationVoltage = Settings.Default.YGalvoCalibrationVoltage;

            string[] devices = NiDaq.GetDeviceNames();
            string deviceName = devices.Length > 0 ? devices[0] : Settings.Default.NiDeviceName;

            XGalvoAoChannel = string.Concat(deviceName, Settings.Default.XGalvoAoChannel);
            YGalvoAoChannel = string.Concat(deviceName, Settings.Default.YGalvoAoChannel);
            Y2GalvoAoChannel = string.Concat(deviceName, Settings.Default.Y2GalvoAoChannel);
        }

        /// <summary>
        /// X坐标->X振镜电压
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <returns></returns>
        public double XCoordinateToVoltage(double xCoordinate)
        {
            return xCoordinate * XGalvoCalibrationVoltage / 1000 + XGalvoOffsetVoltage;
        }

        /// <summary>
        /// X坐标序列->X振镜电压序列
        /// </summary>
        /// <param name="xCoordinates"></param>
        /// <returns></returns>
        public double[] XCoordinateToVoltage(double[] xCoordinates)
        {
            double calibrationVoltage = XGalvoCalibrationVoltage / 1000;
            double[] xVoltages = new double[xCoordinates.Length];
            for (int i = 0; i < xCoordinates.Length; i++)
            {
                xVoltages[i] = xCoordinates[i] * calibrationVoltage + XGalvoOffsetVoltage;
            }
            return xVoltages;
        }

        /// <summary>
        /// Y坐标->Y振镜电压
        /// </summary>
        /// <param name="yCoordinate"></param>
        /// <returns></returns>
        public double YCoordinateToVoltage(double yCoordinate)
        {
            return yCoordinate * YGalvoCalibrationVoltage / 1000 + YGalvoOffsetVoltage;
        }

        /// <summary>
        /// Y坐标序列->Y振镜电压序列
        /// </summary>
        /// <param name="yCoordinates"></param>
        /// <returns></returns>
        public double[] YCoordinateToVoltage(double[] yCoordinates)
        {
            double calibrationVoltage = YGalvoCalibrationVoltage / 1000;
            double[] yVoltages = new double[yCoordinates.Length];
            for (int i = 0; i < yCoordinates.Length; i++)
            {
                yVoltages[i] = yCoordinates[i] * calibrationVoltage + YGalvoOffsetVoltage;
            }
            return yVoltages;
        }

        /// <summary>
        /// X振镜电压->X坐标
        /// </summary>
        /// <param name="xVoltage"></param>
        /// <returns></returns>
        public double XVoltageToCoordinate(double xVoltage)
        {
            return (xVoltage - XGalvoOffsetVoltage) / XGalvoCalibrationVoltage * 1000;
        }

        /// <summary>
        /// X振镜电压序列->X坐标序列
        /// </summary>
        /// <param name="xVoltages"></param>
        /// <returns></returns>
        public double[] XVoltageToCoordinate(double[] xVoltages)
        {
            double calibrationVoltage = XGalvoCalibrationVoltage / 1000;
            double[] xCoordinates = new double[xVoltages.Length];
            for (int i = 0; i < xVoltages.Length; i++)
            {
                xCoordinates[i] = (xVoltages[i] - XGalvoOffsetVoltage) / calibrationVoltage;
            }
            return xCoordinates;
        }

        /// <summary>
        /// Y振镜电压->Y坐标
        /// </summary>
        /// <param name="yVoltage"></param>
        /// <returns></returns>
        public double YVoltageToCoordinate(double yVoltage)
        {
            return (yVoltage - YGalvoOffsetVoltage) / YGalvoCalibrationVoltage * 1000;
        }

        /// <summary>
        /// Y振镜电压序列->Y坐标序列
        /// </summary>
        /// <param name="yVoltages"></param>
        /// <returns></returns>
        public double[] YVoltageToCoordinate(double[] yVoltages)
        {
            double calibrationVoltage = YGalvoCalibrationVoltage / 1000;
            double[] yCoordinates = new double[yVoltages.Length];
            for (int i = 0; i < yVoltages.Length; i++)
            {
                yCoordinates[i] = (yVoltages[i] - YGalvoOffsetVoltage) / calibrationVoltage;
            }
            return yCoordinates;
        }

        /// <summary>
        /// X坐标->X振镜电压
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="galvoPrpperty"></param>
        /// <returns></returns>
        public static double XCoordinateToVoltage(double xCoordinate, GalvoPropertyModel galvoPrpperty)
        {
            return xCoordinate * galvoPrpperty.XGalvoCalibrationVoltage / 1000 + galvoPrpperty.XGalvoOffsetVoltage;
        }

        /// <summary>
        /// X坐标序列->X振镜电压序列
        /// </summary>
        /// <param name="xCoordinates"></param>
        /// <returns></returns>
        public static double[] XCoordinateToVoltage(double[] xCoordinates, GalvoPropertyModel galvoPrpperty)
        {
            double calibrationVoltage = galvoPrpperty.XGalvoCalibrationVoltage / 1000;
            double[] xVoltages = new double[xCoordinates.Length];
            for (int i = 0; i < xCoordinates.Length; i++)
            {
                xVoltages[i] = xCoordinates[i] * calibrationVoltage + galvoPrpperty.XGalvoOffsetVoltage;
            }
            return xVoltages;
        }

        /// <summary>
        /// Y坐标->Y振镜电压
        /// </summary>
        /// <param name="yCoordinate"></param>
        /// <param name="galvoPrpperty"></param>
        /// <returns></returns>
        public static double YCoordinateToVoltage(double yCoordinate, GalvoPropertyModel galvoPrpperty)
        {
            return yCoordinate * galvoPrpperty.YGalvoCalibrationVoltage / 1000 + galvoPrpperty.YGalvoOffsetVoltage;
        }

        /// <summary>
        /// Y坐标序列->Y振镜电压序列
        /// </summary>
        /// <param name="yCoordinates"></param>
        /// <param name="galvoPrpperty"></param>
        /// <returns></returns>
        public static double[] YCoordinateToVoltage(double[] yCoordinates, GalvoPropertyModel galvoPrpperty)
        {
            double calibrationVoltage = galvoPrpperty.YGalvoCalibrationVoltage / 1000;
            double[] yVoltages = new double[yCoordinates.Length];
            for (int i = 0; i < yCoordinates.Length; i++)
            {
                yVoltages[i] = yCoordinates[i] * calibrationVoltage + galvoPrpperty.YGalvoOffsetVoltage;
            }
            return yVoltages;
        }

        /// <summary>
        /// X振镜电压->X坐标
        /// </summary>
        /// <param name="xVoltage"></param>
        /// <param name="galvoPrpperty"></param>
        /// <returns></returns>
        public static double XVoltageToCoordinate(double xVoltage, GalvoPropertyModel galvoPrpperty)
        {
            return (xVoltage - galvoPrpperty.XGalvoOffsetVoltage) / galvoPrpperty.XGalvoCalibrationVoltage * 1000;
        }

        /// <summary>
        /// X振镜电压序列->X坐标序列
        /// </summary>
        /// <param name="xVoltages"></param>
        /// <param name="galvoPrpperty"></param>
        /// <returns></returns>
        public static double[] XVoltageToCoordinate(double[] xVoltages, GalvoPropertyModel galvoPrpperty)
        {
            double calibrationVoltage = galvoPrpperty.XGalvoCalibrationVoltage / 1000;
            double[] xCoordinates = new double[xVoltages.Length];
            for (int i = 0; i < xVoltages.Length; i++)
            {
                xCoordinates[i] = (xVoltages[i] - galvoPrpperty.XGalvoOffsetVoltage) / calibrationVoltage;
            }
            return xCoordinates;
        }

        /// <summary>
        /// Y振镜电压->Y坐标
        /// </summary>
        /// <param name="yVoltage"></param>
        /// <param name="galvoPrpperty"></param>
        /// <returns></returns>
        public static double YVoltageToCoordinate(double yVoltage, GalvoPropertyModel galvoPrpperty)
        {
            return (yVoltage - galvoPrpperty.YGalvoOffsetVoltage) / galvoPrpperty.YGalvoCalibrationVoltage * 1000;
        }

        /// <summary>
        /// Y振镜电压序列->Y坐标序列
        /// </summary>
        /// <param name="yVoltages"></param>
        /// <param name="galvoPrpperty"></param>
        /// <returns></returns>
        public static double[] YVoltageToCoordinate(double[] yVoltages, GalvoPropertyModel galvoPrpperty)
        {
            double calibrationVoltage = galvoPrpperty.YGalvoCalibrationVoltage / 1000;
            double[] yCoordinates = new double[yVoltages.Length];
            for (int i = 0; i < yVoltages.Length; i++)
            {
                yCoordinates[i] = (yVoltages[i] - galvoPrpperty.YGalvoOffsetVoltage) / calibrationVoltage;
            }
            return yCoordinates;
        }

    }
}
