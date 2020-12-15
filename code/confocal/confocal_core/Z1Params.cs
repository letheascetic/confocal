using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public class Z1ScanParams
    {
        /// <summary>
        /// 扫描帧率
        /// </summary>
        public double Fps { get; set; }
        /// <summary>
        /// 像素采样速率，单位：xx Sample/s
        /// </summary>
        public double PixelSampleRate { get; set; }
        /// <summary>
        /// AO输出速率，单位：xx Sample/s
        /// </summary>
        public double AoSampleRate { get; set; }
        /// <summary>
        /// AO输出时间，单位：us
        /// </summary>
        public double AoSampleTime { get; set; }
        /// <summary>
        /// DO输出速率，单位：xx Sample/s
        /// </summary>
        public double DoSampleRate { get; set; }
        /// <summary>
        /// AI采样速率，单位：xx Sample/s
        /// </summary>
        public double AiSampleRate { get; set; }
        /// <summary>
        /// Counter输出速率，单位：xx Sample/s
        /// </summary>
        public double CtrSampleRate { get; set; }
        /// <summary>
        /// 像素尺寸，单位：um
        /// </summary>
        public double PixelSize { get; set; }
        /// <summary>
        /// 相邻像素间电压差，单位：V
        /// </summary>
        public double AoVoltagePerPixel { get; set; }
    }

    public class Z1Generator
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");

        ///////////////////////////////////////////////////////////////////////////////////////////

        public static void CalculateScanParams(Z1Config config, ref Z1ScanParams scanParams)
        {
            Z1ScanProperty scanProperty = config.ScanProperty;
            RectangleF scanField = scanProperty.ScanFields[(int)scanProperty.ScanArea].ScanRange;

            scanParams.PixelSampleRate = scanProperty.PixelSampleRate;
            scanParams.AiSampleRate = scanParams.PixelSampleRate;
            scanParams.CtrSampleRate = scanParams.PixelSampleRate;
            scanParams.AoSampleRate = 1e6 / (int)scanProperty.ScanPixelDwell;       // 扫描

            double pixelSize = scanField.Width / (int)scanProperty.ScanPixels;       // 像素尺寸 = 扫描宽度(um) / 行成像像素数, 单位：um/pixel
            int xScanPixels = scanProperty.GetExtendScanXPixels();                   // 行扫描像素数 = 行成像像素数 + 补偿像素数
            double voltagePerPixel = scanProperty.GalvanoProperty.GalvanoCalibrationVoltage * scanProperty.GalvanoProperty.GalvanoCalibrationFactor * pixelSize;  // 像素电压, 单位：V/pixel

            double w = (int)scanProperty.ScanPixelDwell * xScanPixels / 1000;        // 行有效样本区间的时间范围，单位：ms
            double h = voltagePerPixel * xScanPixels;                                // 行有效样本区间的电压范围，单位：V
            double r = scanProperty.CurveCalibrationFactor * h;                      // 圆弧半径

        }

    }
}
