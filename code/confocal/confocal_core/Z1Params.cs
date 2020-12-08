using log4net;
using System;
using System.Collections.Generic;
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
            scanParams.PixelSampleRate = config.ScanProperty.PixelSampleRate;
            scanParams.AiSampleRate = scanParams.PixelSampleRate;
            scanParams.CtrSampleRate = scanParams.PixelSampleRate;
            scanParams.AoSampleRate = 1e6 / (int)config.ScanProperty.ScanPixelDwell;

            double pixelSampleTime = 1e6 / config.ScanProperty.PixelSampleRate;

            scanParams.PixelSize = config.ScanProperty.ScanField.ScanField.Width / (int)config.ScanProperty.ScanPixels;
            scanParams.AoVoltagePerPixel = config.ScanProperty.GalvanoCalibrationVoltage * 1000 * scanParams.PixelSize;





        }

    }
}
