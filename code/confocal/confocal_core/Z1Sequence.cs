using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace confocal_core
{
    public class Z1Sequence
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private static readonly int TRIGGER_WIDTH_DEFAULT = 4;
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 触发电压序列[一次往返]
        /// </summary>
        public static byte[] TriggerWave { get; set; }
        /// <summary>
        /// X振镜电压序列[一帧]
        /// </summary>
        public static double[] XWave { get; set; }         
        /// <summary>
        /// Y1振镜电压序列[一帧]
        /// </summary>
        public static double[] Y1Wave { get; set; }
        /// <summary>
        /// Y2振镜电压序列[一帧]
        /// </summary>
        public static double[] Y2Wave { get; set; }
        /// <summary>
        /// 单帧电压序列数
        /// </summary>
        public static long FrameCount { get; set; }
        /// <summary>
        /// 单帧的往返次数
        /// </summary>
        public static long RoundTripCount { get; set; }
        /// <summary>
        /// 单行电压序列数
        /// </summary>
        public static long LineCount { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static double[] XCoordinates { get; set; }
        public static double[] YCoordinates { get; set; }
        public static double[] XVoltages { get; set; }
        public static double[] YVoltaegs { get; set; }
        public static byte[] TriggerVoltages { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 生成扫描范围序列和电压序列
        /// </summary>
        /// <param name="scanProperty"></param>
        public static void GenerateScanCoordinates(Z1ScanProperty scanProperty)
        {
            Z1ScanField extendScanField = scanProperty.GetExtendScanField();
            RectangleF extendScanRange = extendScanField.ScanRange;

            if (scanProperty.ScanDirection == SCAN_DIRECTION.UNIDIRECTION)
            {
                int lineStartSampleCount = (int)(Z1ScanField.ScanLineStartTime / (int)scanProperty.ScanPixelDwell);
                double scale = extendScanRange.Width / Z1ScanField.ExtendLineMarginDiv;
                double step0 = -Math.PI / Z1ScanField.ScanLineStartTime * (int)scanProperty.ScanPixelDwell;
                double[] lineSatrtSamples = CreateSinArray(scale, step0, lineStartSampleCount, extendScanRange.X);

                int lineHoldSampleCount = (int)(Z1ScanField.ScanLineHoldTime / (int)scanProperty.ScanPixelDwell);
                double step1 = Math.PI / Z1ScanField.ScanLineHoldTime * (int)scanProperty.ScanPixelDwell;
                double[] lineHoldSamples = CreateSinArray(scale, step1, lineHoldSampleCount, extendScanRange.Right);

                int lineEndSampleCount = (int)(Z1ScanField.ScanLineEndTime / (int)scanProperty.ScanPixelDwell);
                double[] lineEndSamples = CreateLinearArray(extendScanRange.Right, extendScanRange.X, lineEndSampleCount);

                int lineScanSampleCount = (int)scanProperty.ScanPixels;
                double[] lineScanSamples = CreateLinearArray(extendScanRange.X, extendScanRange.Right, lineScanSampleCount);

                int lineSamples = lineStartSampleCount + lineHoldSampleCount + lineEndSampleCount + lineScanSampleCount;
                XCoordinates = new double[lineSamples];
                Array.Copy(lineSatrtSamples, 0, XCoordinates, 0, lineStartSampleCount);
                Array.Copy(lineScanSamples, 0, XCoordinates, lineStartSampleCount, lineScanSampleCount);
                Array.Copy(lineHoldSamples, 0, XCoordinates, lineStartSampleCount + lineScanSampleCount, lineHoldSampleCount);
                Array.Copy(lineEndSamples, 0, XCoordinates, lineSamples - lineEndSampleCount, lineEndSampleCount);

                int ySamplesCount = scanProperty.GetExtendScanYPixels();
                YCoordinates = CreateLinearArray(extendScanRange.Y, extendScanRange.Bottom, ySamplesCount);

                TriggerVoltages = Enumerable.Repeat<byte>(0x00, lineSamples).ToArray();
                for (int n = 0; n < TRIGGER_WIDTH_DEFAULT; n++)
                {
                    TriggerVoltages[lineStartSampleCount + n] = 0x01;
                }
            }
            else
            {
                int lineStartSampleCount = (int)(Z1ScanField.ScanLineStartTime / (int)scanProperty.ScanPixelDwell);
                double scale = extendScanRange.Width / Z1ScanField.ExtendLineMarginDiv;
                double step0 = -Math.PI / Z1ScanField.ScanLineStartTime * (int)scanProperty.ScanPixelDwell;
                double[] lineSatrtSamples = CreateSinArray(scale, step0, lineStartSampleCount, extendScanRange.X);

                // 双向扫描中，ScanLineHoldTime应默认为ScanLineStartTime
                int lineHoldSampleCount = (int)(Z1ScanField.ScanLineStartTime / (int)scanProperty.ScanPixelDwell);
                double step1 = Math.PI / Z1ScanField.ScanLineStartTime * (int)scanProperty.ScanPixelDwell;
                double[] lineHoldSamples = CreateSinArray(scale, step1, lineHoldSampleCount, extendScanRange.Right);

                int lineScanSampleCount = (int)scanProperty.ScanPixels;
                double[] lineScanSamples = CreateLinearArray(extendScanRange.X, extendScanRange.Right, lineScanSampleCount);

                // 双向扫描中，lineEndSamples=回程的ScanSamples
                int lineEndSampleCount = (int)scanProperty.ScanPixels;
                double[] lineEndSamples = CreateLinearArray(extendScanRange.Right, extendScanRange.X, lineEndSampleCount);

                int lineSamples = lineStartSampleCount + lineHoldSampleCount + lineEndSampleCount + lineScanSampleCount;
                XCoordinates = new double[lineSamples];
                Array.Copy(lineSatrtSamples, 0, XCoordinates, 0, lineStartSampleCount);
                Array.Copy(lineScanSamples, 0, XCoordinates, lineStartSampleCount, lineScanSampleCount);
                Array.Copy(lineHoldSamples, 0, XCoordinates, lineStartSampleCount + lineScanSampleCount, lineHoldSampleCount);
                Array.Copy(lineEndSamples, 0, XCoordinates, lineSamples - lineEndSampleCount, lineEndSampleCount);

                int ySamplesCount = scanProperty.GetExtendScanYPixels();
                YCoordinates = CreateLinearArray(extendScanRange.Y, extendScanRange.Bottom, ySamplesCount);

                TriggerVoltages = Enumerable.Repeat<byte>(0x00, lineSamples).ToArray();
                for (int n = 0; n < TRIGGER_WIDTH_DEFAULT; n++)
                {
                    TriggerVoltages[lineStartSampleCount + n] = 0x01;
                    TriggerVoltages[lineSamples - lineEndSampleCount + n] = 0x01;
                }
            }

            XVoltages = Z1GalvanoProperty.XCoordinateToVoltage(XCoordinates);
            YVoltaegs = Z1GalvanoProperty.YCoordinateToVoltage(YCoordinates);

            LineCount = XVoltages.Length;
            RoundTripCount = scanProperty.ScanDirection == SCAN_DIRECTION.UNIDIRECTION ? YVoltaegs.Length : YVoltaegs.Length / 2;
            FrameCount = LineCount * RoundTripCount;
        }

        

        private static void WaveInitialize(Z1ScanProperty scanProperty)
        {
            if (TriggerWave == null || TriggerWave.Length != LineCount)
            {
                TriggerWave = new byte[LineCount];
            }

            if (XWave == null || XWave.Length != FrameCount)
            {
                XWave = new double[FrameCount];
            }

            if (Y1Wave == null || Y1Wave.Length != FrameCount)
            {
                Y1Wave = new double[FrameCount];
            }

            if (scanProperty.Scanners == SCANNER_SYSTEM.THREE_SCANNERS)
            {
                if (Y2Wave == null || Y2Wave.Length != FrameCount)
                {
                    Y2Wave = new double[FrameCount];
                }
            }
        }

        /// <summary>
        /// 生成线性序列
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static double[] CreateLinearArray(double start, double end, int count)
        {
            double[] array = new double[count];
            double step = (end - start) / count;
            for (int n = 0; n < count; n++)
            {
                array[n] = start + step * n;
            }
            return array;
        }
        /// <summary>
        /// 生成正弦函数序列
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static double[] CreateSinArray(double scale, double step, int count, double offset)
        {
            double[] array = new double[count];
            for (int n = 0; n < count; n++)
            {
                array[n] = scale * Math.Sin(step * n) + offset;
            }
            return array;
        }

        public static void GenerateFrameScanWaves(Z1ScanProperty scanProperty)
        {
            WaveInitialize(scanProperty);
            
        }
    }
}
