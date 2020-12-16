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
        private static readonly double ACQUISITION_INTERVAL_DEFAULT = 50;
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 触发电压序列[一帧]
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
        public static int OutputSampleCountPerFrame { get; set; }
        /// <summary>
        /// 单帧的往返次数
        /// </summary>
        public static int OutputRoundTripCountPerFrame { get; set; }
        /// <summary>
        /// 单行电压序列数
        /// </summary>
        public static int OutputSampleCountPerRoundTrip { get; set; }
        /// <summary>
        /// 电压序列输出速率
        /// </summary>
        public static double OutputSampleRate { get; set; }
        /// <summary>
        /// 样本采样速率
        /// </summary>
        public static double InputSampleRate { get; set; }
        /// <summary>
        /// 单次往返采集的样本数
        /// </summary>
        public static int InputSampleCountPerRoundTrip { get; set; }
        /// <summary>
        /// 单帧采集的往返次数
        /// </summary>
        public static int InputRoundTripCountPerFrame { get; set; }
        /// <summary>
        /// 单帧采集的样本数
        /// </summary>
        public static int InputSampleCountPerFrame { get; set; }
        /// <summary>
        /// 单像素采集的样本数
        /// </summary>
        public static int InputSampleCountPerPixel { get; set; }
        /// <summary>
        /// 单次采集的样本数
        /// </summary>
        public static int InputSampleCountPerAcquisition { get; set; }
        /// <summary>
        /// 单次采集的像素数
        /// </summary>
        public static int InputPixelCountPerAcquisition { get; set; }
        /// <summary>
        /// 单次采集的往返次数
        /// </summary>
        public static int InputRoundTripCountPerAcquisition { get; set; }
        /// <summary>
        /// 单帧包含的采集次数
        /// </summary>
        public static int InputAcquisitionCountPerFrame { get; set; }

        /// <summary>
        /// 帧率
        /// </summary>
        public static double FPS { get; set; }
        /// <summary>
        /// 帧时间[单位：seconds/frame]
        /// </summary>
        public static double FrameTime { get; set; }
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

            // 计算输出相关参数
            OutputSampleRate = 1e6 / (int)scanProperty.ScanPixelDwell;
            OutputSampleCountPerRoundTrip = XVoltages.Length;
            OutputRoundTripCountPerFrame = scanProperty.ScanDirection == SCAN_DIRECTION.UNIDIRECTION ? YVoltaegs.Length : YVoltaegs.Length / 2;
            OutputSampleCountPerFrame = OutputSampleCountPerRoundTrip * OutputRoundTripCountPerFrame;

            // 计算采集相关参数
            InputSampleRate = scanProperty.InputSampleRate;
            InputSampleCountPerPixel = (int)(InputSampleRate / OutputSampleRate);
            InputRoundTripCountPerFrame = OutputRoundTripCountPerFrame;
            if (scanProperty.ScanDirection == SCAN_DIRECTION.UNIDIRECTION)
            {
                InputSampleCountPerRoundTrip = (int)(extendScanRange.Width / scanProperty.GetPixelSize()) * InputSampleCountPerPixel;
            }
            else
            {
                InputSampleCountPerRoundTrip = (int)(extendScanRange.Width / scanProperty.GetPixelSize()) * InputSampleCountPerPixel * 2;
            }
            InputSampleCountPerFrame = InputRoundTripCountPerFrame * InputSampleCountPerRoundTrip;

            double roundTripTime = OutputSampleCountPerRoundTrip * (int)scanProperty.ScanPixelDwell / 1e6;
            int nRoundTrips = (int)Math.Ceiling(ACQUISITION_INTERVAL_DEFAULT / roundTripTime);
            while (nRoundTrips > 1)
            {
                if (OutputRoundTripCountPerFrame % nRoundTrips == 0)
                {
                    break;
                }
                nRoundTrips--;
            }
            InputRoundTripCountPerAcquisition = nRoundTrips;                                // 单次采集的往返次数
            InputSampleCountPerAcquisition = InputSampleCountPerRoundTrip * nRoundTrips;    // 单次采集的样本数
            InputPixelCountPerAcquisition = InputSampleCountPerAcquisition / InputSampleCountPerPixel;  // 单次采集的像素数
            InputAcquisitionCountPerFrame = InputRoundTripCountPerFrame / nRoundTrips;      // 单帧包含的采集次数

            // 帧率和帧时间
            FrameTime = OutputSampleCountPerFrame * (int)scanProperty.ScanPixelDwell / 1e6;
            FPS = 1.0 / FrameTime;
        }

        /// <summary>
        /// 生成帧电压序列 -- [暂未考虑LineAverage和LineIntegrate]
        /// </summary>
        /// <param name="scanProperty"></param>
        public static void GenerateFrameScanWaves(Z1ScanProperty scanProperty)
        {
            WaveInitialize(scanProperty);

            if (scanProperty.ScanDirection == SCAN_DIRECTION.UNIDIRECTION)
            {
                int index = -OutputSampleCountPerRoundTrip;
                for (int n = 0; n < OutputRoundTripCountPerFrame; n++)
                {
                    index += OutputSampleCountPerRoundTrip;
                    Array.Copy(XVoltages, 0, XWave, index, OutputSampleCountPerRoundTrip);
                    Array.Copy(Enumerable.Repeat<double>(YVoltaegs[n], OutputSampleCountPerRoundTrip).ToArray(), 0, Y1Wave, index, OutputSampleCountPerRoundTrip);
                    if (scanProperty.Scanners == SCANNER_SYSTEM.THREE_SCANNERS)
                    {
                        Array.Copy(Enumerable.Repeat<double>(YVoltaegs[n] * 2, OutputSampleCountPerRoundTrip).ToArray(), 0, Y2Wave, index, OutputSampleCountPerRoundTrip);
                    }
                    Array.Copy(TriggerVoltages, 0, TriggerWave, index, OutputSampleCountPerRoundTrip);
                }
            }
            else
            {
                int index = -OutputSampleCountPerRoundTrip;
                for (int n = 0; n < OutputRoundTripCountPerFrame; n++)
                {
                    index += OutputSampleCountPerRoundTrip;
                    Array.Copy(XVoltages, 0, XWave, index, OutputSampleCountPerRoundTrip);
                    Array.Copy(Enumerable.Repeat<double>(YVoltaegs[2 * n], OutputSampleCountPerRoundTrip >> 1).ToArray(), 0, Y1Wave, index, OutputSampleCountPerRoundTrip >> 1);
                    Array.Copy(Enumerable.Repeat<double>(YVoltaegs[2 * n + 1], OutputSampleCountPerRoundTrip >> 1).ToArray(), 0, Y1Wave, index + (OutputSampleCountPerRoundTrip >> 1), OutputSampleCountPerRoundTrip >> 1);
                    if (scanProperty.Scanners == SCANNER_SYSTEM.THREE_SCANNERS)
                    {
                        Array.Copy(Enumerable.Repeat<double>(YVoltaegs[2 * n] * 2, OutputSampleCountPerRoundTrip >> 1).ToArray(), 0, Y2Wave, index, OutputSampleCountPerRoundTrip >> 1);
                        Array.Copy(Enumerable.Repeat<double>(YVoltaegs[2 * n + 1] * 2, OutputSampleCountPerRoundTrip >> 1).ToArray(), 0, Y2Wave, index + (OutputSampleCountPerRoundTrip >> 1), OutputSampleCountPerRoundTrip >> 1);
                    }
                    Array.Copy(TriggerVoltages, 0, TriggerWave, index, OutputSampleCountPerRoundTrip);
                }
            }

            int resetSampleCount = (int)(Z1ScanField.ScanLineStartTime / (int)scanProperty.ScanPixelDwell);
            double[] y1ResetVoltages = CreateLinearArray(YVoltaegs.Last(), YVoltaegs[0], resetSampleCount);
            Array.Copy(y1ResetVoltages, 0, Y1Wave, 0, resetSampleCount);
            if (scanProperty.Scanners == SCANNER_SYSTEM.THREE_SCANNERS)
            {
                double[] y2ResetVoltages = CreateLinearArray(YVoltaegs.Last() * 2, YVoltaegs[0] * 2, resetSampleCount);
                Array.Copy(y2ResetVoltages, 0, Y2Wave, 0, resetSampleCount);
            }
        }

        private static void WaveInitialize(Z1ScanProperty scanProperty)
        {
            if (TriggerWave == null || TriggerWave.Length != OutputSampleCountPerRoundTrip)
            {
                TriggerWave = new byte[OutputSampleCountPerRoundTrip];
            }

            if (XWave == null || XWave.Length != OutputSampleCountPerFrame)
            {
                XWave = new double[OutputSampleCountPerFrame];
            }

            if (Y1Wave == null || Y1Wave.Length != OutputSampleCountPerFrame)
            {
                Y1Wave = new double[OutputSampleCountPerFrame];
            }

            if (scanProperty.Scanners == SCANNER_SYSTEM.THREE_SCANNERS)
            {
                if (Y2Wave == null || Y2Wave.Length != OutputSampleCountPerFrame)
                {
                    Y2Wave = new double[OutputSampleCountPerFrame];
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

    }
}
