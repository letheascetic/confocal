﻿using confocal_core.Common;
using confocal_core.ViewModel;
using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    public class SequenceModel : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static SequenceModel pSequence = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly int TRIGGER_WIDTH_DEFAULT = 4;
        private static readonly double ACQUISITION_INTERVAL_DEFAULT = 50;
        ///////////////////////////////////////////////////////////////////////////////////////////

        private byte[] triggerWave;
        private double[] xWave;
        private double[] y1Wave;
        private double[] y2Wave;
        private int outputSampleCountPerFrame;
        private int outputRoundTripCountPerFrame;
        private int outputSampleCountPerRoundTrip;
        private double outputSampleRate;
        private double inputSampleRate;
        private int inputSampleCountPerRoundTrip;
        private int inputRoundTripCountPerFrame;
        private int inputSampleCountPerFrame;
        private int inputSampleCountPerPixel;
        private int inputSampleCountPerAcquisition;
        private int inputPixelCountPerAcquisition;
        private int inputRoundTripCountPerAcquisition;
        private int inputAcquisitionCountPerFrame;
        private int inputSampleCountPerRow;
        private double fps;
        private double frameTime;

        private double[] yCoordinates;
        private double[] xCoordinates;
        private double[] xVoltages;
        private double[] yVoltaegs;
        private byte[] triggerVoltages;
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 触发波形
        /// </summary>
        public byte[] TriggerWave
        {
            get { return triggerWave; }
            set { triggerWave = value; RaisePropertyChanged(() => TriggerWave); }
        }
        /// <summary>
        /// X振镜波形
        /// </summary>
        public double[] XWave
        {
            get { return xWave; }
            set { xWave = value; RaisePropertyChanged(() => XWave); }
        }
        /// <summary>
        /// Y1振镜波形
        /// </summary>
        public double[] Y1Wave
        {
            get { return y1Wave; }
            set { y1Wave = value; RaisePropertyChanged(() => Y1Wave); }
        }
        /// <summary>
        /// Y2振镜波形
        /// </summary>
        public double[] Y2Wave
        {
            get { return y2Wave; }
            set { y2Wave = value; RaisePropertyChanged(() => Y2Wave); }
        }
        /// <summary>
        /// 单帧输出样本数
        /// </summary>
        public int OutputSampleCountPerFrame
        {
            get { return outputSampleCountPerFrame; }
            set { outputSampleCountPerFrame = value; RaisePropertyChanged(() => OutputSampleCountPerFrame); }
        }
        /// <summary>
        /// 单帧往返次数
        /// </summary>
        public int OutputRoundTripCountPerFrame
        {
            get { return outputRoundTripCountPerFrame; }
            set { outputRoundTripCountPerFrame = value; RaisePropertyChanged(() => OutputRoundTripCountPerFrame); }
        }
        /// <summary>
        /// 单次往返样本数
        /// </summary>
        public int OutputSampleCountPerRoundTrip
        {
            get { return outputSampleCountPerRoundTrip; }
            set { outputSampleCountPerRoundTrip = value; RaisePropertyChanged(() => OutputSampleCountPerRoundTrip); }
        }
        /// <summary>
        /// 样本输出速率
        /// </summary>
        public double OutputSampleRate
        {
            get { return outputSampleRate; }
            set { outputSampleRate = value; RaisePropertyChanged(() => OutputSampleRate); }
        }
        /// <summary>
        /// 样本采样速率
        /// </summary>
        public double InputSampleRate 
        {
            get { return inputSampleRate; }
            set { inputSampleRate = value; RaisePropertyChanged(() => InputSampleRate); }
        }
        /// <summary>
        /// 单次往返采集的样本数
        /// </summary>
        public int InputSampleCountPerRoundTrip 
        { 
            get { return inputSampleCountPerRoundTrip; }
            set { inputSampleCountPerRoundTrip = value; RaisePropertyChanged(() => InputSampleCountPerRoundTrip); }
        }
        /// <summary>
        /// 单帧的往返次数
        /// </summary>
        public int InputRoundTripCountPerFrame
        {
            get { return inputRoundTripCountPerFrame; }
            set { inputRoundTripCountPerFrame = value; RaisePropertyChanged(() => InputRoundTripCountPerFrame); }
        }
        /// <summary>
        /// 单帧采集的样本数
        /// </summary>
        public int InputSampleCountPerFrame
        {
            get { return inputSampleCountPerFrame; }
            set { inputSampleCountPerFrame = value; RaisePropertyChanged(() => InputSampleCountPerFrame); }
        }
        /// <summary>
        /// 单像素采集的样本数
        /// </summary>
        public int InputSampleCountPerPixel
        {
            get { return inputSampleCountPerPixel; }
            set { inputSampleCountPerPixel = value; RaisePropertyChanged(() => InputSampleCountPerPixel); }
        }
        /// <summary>
        /// 单次采集的样本数
        /// </summary>
        public int InputSampleCountPerAcquisition 
        {
            get { return inputSampleCountPerAcquisition; }
            set { inputSampleCountPerAcquisition = value; RaisePropertyChanged(() => InputSampleCountPerAcquisition); }
        }
        /// <summary>
        /// 单次采集的像素数
        /// </summary>
        public int InputPixelCountPerAcquisition 
        {
            get { return inputPixelCountPerAcquisition; }
            set { inputPixelCountPerAcquisition = value; RaisePropertyChanged(() => InputPixelCountPerAcquisition); }
        }
        /// <summary>
        /// 单次采集的往返次数
        /// </summary>
        public int InputRoundTripCountPerAcquisition 
        {
            get { return inputRoundTripCountPerAcquisition; }
            set { inputRoundTripCountPerAcquisition = value; RaisePropertyChanged(() => InputRoundTripCountPerAcquisition); }
        }
        /// <summary>
        /// 单帧包含的采集次数
        /// </summary>
        public int InputAcquisitionCountPerFrame 
        {
            get { return inputAcquisitionCountPerFrame; }
            set { inputAcquisitionCountPerFrame = value; RaisePropertyChanged(() => InputAcquisitionCountPerFrame); }
        }
        /// <summary>
        /// 每行采集的样本数
        /// </summary>
        public int InputSampleCountPerRow
        {
            get { return inputSampleCountPerRow; }
            set { inputSampleCountPerRow = value; RaisePropertyChanged(() => InputSampleCountPerRow); }
        }
        /// <summary>
        /// 帧率
        /// </summary>
        public double FPS
        {
            get { return fps; }
            set { fps = value; RaisePropertyChanged(() => FPS); }
        }
        /// <summary>
        /// 帧时间
        /// </summary>
        public double FrameTime
        {
            get { return frameTime; }
            set { frameTime = value; RaisePropertyChanged(() => FrameTime); }
        }
        /// <summary>
        /// X坐标序列[单次往返]
        /// </summary>
        public double[] XCoordinates
        {
            get { return xCoordinates; }
            set { xCoordinates = value; RaisePropertyChanged(() => XCoordinates); }
        }
        /// <summary>
        /// Y坐标序列
        /// </summary>
        public double[] YCoordinates
        {
            get { return yCoordinates; }
            set { yCoordinates = value; RaisePropertyChanged(() => YCoordinates); }
        }
        /// <summary>
        /// X电压序列[单次往返]
        /// </summary>
        public double[] XVoltages
        {
            get { return xVoltages; }
            set { xVoltages = value; RaisePropertyChanged(() => XVoltages); }
        }
        /// <summary>
        /// Y电压序列
        /// </summary>
        public double[] YVoltages
        {
            get { return yVoltaegs; }
            set { yVoltaegs = value; RaisePropertyChanged(() => YVoltages); }
        }
        /// <summary>
        /// 触发信号序列
        /// </summary>
        public byte[] TriggerVoltages
        {
            get { return triggerVoltages; }
            set { triggerVoltages = value; RaisePropertyChanged(() => TriggerVoltages); }
        }

        public static SequenceModel CreateInstance()
        {
            if (pSequence == null)
            {
                lock (locker)
                {
                    if (pSequence == null)
                    {
                        pSequence = new SequenceModel();
                    }
                }
            }
            return pSequence;
        }

        /// <summary>
        /// 生成扫描范围序列和电压序列
        /// </summary>
        public void GenerateScanCoordinates()
        {
            ConfigViewModel config = ConfigViewModel.GetConfig();
            ScanAreaModel extendScanArea = config.GetExtendScanArea();

            if (config.SelectedScanDirection.ID == ScanDirectionModel.UNIDIRECTION)
            {
                int lineStartSampleCount = (int)(ScanAreaModel.ScanLineStartTime / config.SelectedScanPixelDwell.Data);
                double scale = extendScanArea.ScanRange.Width / ScanAreaModel.ExtendLineMarginDiv;
                double step0 = -Math.PI / ScanAreaModel.ScanLineStartTime * config.SelectedScanPixelDwell.Data;
                double[] lineSatrtSamples = CreateSinArray(scale, step0, lineStartSampleCount, extendScanArea.ScanRange.X);

                int lineHoldSampleCount = (int)(ScanAreaModel.ScanLineHoldTime / config.SelectedScanPixelDwell.Data);
                double step1 = Math.PI / ScanAreaModel.ScanLineHoldTime * config.SelectedScanPixelDwell.Data;
                double[] lineHoldSamples = CreateSinArray(scale, step1, lineHoldSampleCount, extendScanArea.ScanRange.Right);

                int lineEndSampleCount = (int)(ScanAreaModel.ScanLineEndTime / config.SelectedScanPixelDwell.Data);
                double[] lineEndSamples = CreateLinearArray(extendScanArea.ScanRange.Right, extendScanArea.ScanRange.X, lineEndSampleCount);

                int lineScanSampleCount = config.GetExtendScanXPixels();
                double[] lineScanSamples = CreateLinearArray(extendScanArea.ScanRange.X, extendScanArea.ScanRange.Right, lineScanSampleCount);

                int lineSamples = lineStartSampleCount + lineHoldSampleCount + lineEndSampleCount + lineScanSampleCount;
                XCoordinates = new double[lineSamples];
                Array.Copy(lineSatrtSamples, 0, XCoordinates, 0, lineStartSampleCount);
                Array.Copy(lineScanSamples, 0, XCoordinates, lineStartSampleCount, lineScanSampleCount);
                Array.Copy(lineHoldSamples, 0, XCoordinates, lineStartSampleCount + lineScanSampleCount, lineHoldSampleCount);
                Array.Copy(lineEndSamples, 0, XCoordinates, lineSamples - lineEndSampleCount, lineEndSampleCount);

                int ySamplesCount = config.GetExtendScanYPixels();
                YCoordinates = CreateLinearArray(extendScanArea.ScanRange.Y, extendScanArea.ScanRange.Bottom, ySamplesCount);

                TriggerVoltages = Enumerable.Repeat<byte>(0x00, lineSamples).ToArray();
                for (int n = 0; n < TRIGGER_WIDTH_DEFAULT; n++)
                {
                    TriggerVoltages[lineStartSampleCount + n] = 0x01;
                }
            }
            else
            {
                int lineStartSampleCount = (int)(ScanAreaModel.ScanLineStartTime / config.SelectedScanPixelDwell.Data);
                double scale = extendScanArea.ScanRange.Width / ScanAreaModel.ExtendLineMarginDiv;
                double step0 = -Math.PI / ScanAreaModel.ScanLineStartTime * config.SelectedScanPixelDwell.Data;
                double[] lineSatrtSamples = CreateSinArray(scale, step0, lineStartSampleCount, extendScanArea.ScanRange.X);

                // 双向扫描中，ScanLineHoldTime应默认为ScanLineStartTime
                int lineHoldSampleCount = (int)(ScanAreaModel.ScanLineStartTime / config.SelectedScanPixelDwell.Data);
                double step1 = Math.PI / ScanAreaModel.ScanLineStartTime * config.SelectedScanPixelDwell.Data;
                double[] lineHoldSamples = CreateSinArray(scale, step1, lineHoldSampleCount, extendScanArea.ScanRange.Right);

                int lineScanSampleCount = config.GetExtendScanXPixels();
                double[] lineScanSamples = CreateLinearArray(extendScanArea.ScanRange.X, extendScanArea.ScanRange.Right, lineScanSampleCount);

                // 双向扫描中，lineEndSamples=回程的ScanSamples
                int lineEndSampleCount = lineScanSampleCount;
                double[] lineEndSamples = CreateLinearArray(extendScanArea.ScanRange.Right, extendScanArea.ScanRange.X, lineEndSampleCount);

                int lineSamples = lineStartSampleCount + lineHoldSampleCount + lineEndSampleCount + lineScanSampleCount;
                XCoordinates = new double[lineSamples];
                Array.Copy(lineSatrtSamples, 0, XCoordinates, 0, lineStartSampleCount);
                Array.Copy(lineScanSamples, 0, XCoordinates, lineStartSampleCount, lineScanSampleCount);
                Array.Copy(lineHoldSamples, 0, XCoordinates, lineStartSampleCount + lineScanSampleCount, lineHoldSampleCount);
                Array.Copy(lineEndSamples, 0, XCoordinates, lineSamples - lineEndSampleCount, lineEndSampleCount);

                int ySamplesCount = config.GetExtendScanYPixels();
                YCoordinates = CreateLinearArray(extendScanArea.ScanRange.Y, extendScanArea.ScanRange.Bottom, ySamplesCount);

                TriggerVoltages = Enumerable.Repeat<byte>(0x00, lineSamples).ToArray();
                for (int n = 0; n < TRIGGER_WIDTH_DEFAULT; n++)
                {
                    TriggerVoltages[lineStartSampleCount + n] = 0x01;
                    TriggerVoltages[lineSamples - lineEndSampleCount + n] = 0x01;
                }
            }

            XVoltages = config.GalvoProperty.XCoordinateToVoltage(XCoordinates);
            YVoltages = config.GalvoProperty.YCoordinateToVoltage(YCoordinates);

            // 计算输出相关参数
            OutputSampleRate = 1e6 / config.SelectedScanPixelDwell.Data;
            OutputSampleCountPerRoundTrip = XVoltages.Length;
            OutputRoundTripCountPerFrame = config.SelectedScanDirection.ID == ScanDirectionModel.UNIDIRECTION ? YVoltages.Length : YVoltages.Length / 2;
            OutputSampleCountPerFrame = OutputSampleCountPerRoundTrip * OutputRoundTripCountPerFrame;

            // 计算采集相关参数
            InputSampleRate = config.InputSampleRate;
            InputSampleCountPerPixel = (int)(InputSampleRate / OutputSampleRate);
            InputRoundTripCountPerFrame = OutputRoundTripCountPerFrame;
            if (config.SelectedScanDirection.ID == ScanDirectionModel.UNIDIRECTION)
            {
                InputSampleCountPerRoundTrip = (int)(extendScanArea.ScanRange.Width / config.ScanPixelSize) * InputSampleCountPerPixel;
                InputSampleCountPerRow = InputSampleCountPerRoundTrip;
            }
            else
            {
                InputSampleCountPerRoundTrip = (int)(extendScanArea.ScanRange.Width / config.ScanPixelSize) * InputSampleCountPerPixel * 2;
                InputSampleCountPerRow = InputSampleCountPerRoundTrip / 2;
            }
            InputSampleCountPerFrame = InputRoundTripCountPerFrame * InputSampleCountPerRoundTrip;

            double roundTripTime = OutputSampleCountPerRoundTrip * config.SelectedScanPixelDwell.Data / 1e3;
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
            FrameTime = OutputSampleCountPerFrame * config.SelectedScanPixelDwell.Data / 1e6;
            FPS = 1.0 / FrameTime;
        }

        /// <summary>
        /// 生成帧电压序列
        /// </summary>
        public void GenerateFrameScanWaves()
        {
            ConfigViewModel config = ConfigViewModel.GetConfig();
            WaveInitialize();

            if (config.SelectedScanDirection.ID == ScanDirectionModel.UNIDIRECTION)
            {
                int index = -OutputSampleCountPerRoundTrip;
                for (int n = 0; n < OutputRoundTripCountPerFrame; n++)
                {
                    index += OutputSampleCountPerRoundTrip;
                    Array.Copy(XVoltages, 0, XWave, index, OutputSampleCountPerRoundTrip);
                    Array.Copy(Enumerable.Repeat<double>(YVoltages[n], OutputSampleCountPerRoundTrip).ToArray(), 0, Y1Wave, index, OutputSampleCountPerRoundTrip);
                    if (config.SelectedScannerHead.ID == ScannerHeadModel.THREE_SCANNERS)
                    {
                        Array.Copy(Enumerable.Repeat<double>(YVoltages[n] * 2, OutputSampleCountPerRoundTrip).ToArray(), 0, Y2Wave, index, OutputSampleCountPerRoundTrip);
                    }
                }
            }
            else
            {
                int index = -OutputSampleCountPerRoundTrip;
                for (int n = 0; n < OutputRoundTripCountPerFrame; n++)
                {
                    index += OutputSampleCountPerRoundTrip;
                    Array.Copy(XVoltages, 0, XWave, index, OutputSampleCountPerRoundTrip);
                    Array.Copy(Enumerable.Repeat<double>(YVoltages[2 * n], OutputSampleCountPerRoundTrip >> 1).ToArray(), 0, Y1Wave, index, OutputSampleCountPerRoundTrip >> 1);
                    Array.Copy(Enumerable.Repeat<double>(YVoltages[2 * n + 1], OutputSampleCountPerRoundTrip >> 1).ToArray(), 0, Y1Wave, index + (OutputSampleCountPerRoundTrip >> 1), OutputSampleCountPerRoundTrip >> 1);
                    if (config.SelectedScannerHead.ID == ScannerHeadModel.THREE_SCANNERS)
                    {
                        Array.Copy(Enumerable.Repeat<double>(YVoltages[2 * n] * 2, OutputSampleCountPerRoundTrip >> 1).ToArray(), 0, Y2Wave, index, OutputSampleCountPerRoundTrip >> 1);
                        Array.Copy(Enumerable.Repeat<double>(YVoltages[2 * n + 1] * 2, OutputSampleCountPerRoundTrip >> 1).ToArray(), 0, Y2Wave, index + (OutputSampleCountPerRoundTrip >> 1), OutputSampleCountPerRoundTrip >> 1);
                    }
                }
            }

            Array.Copy(TriggerVoltages, TriggerWave, OutputSampleCountPerRoundTrip);

            int resetSampleCount = (int)(ScanAreaModel.ScanLineStartTime / config.SelectedScanPixelDwell.Data);
            double[] y1ResetVoltages = CreateLinearArray(YVoltages.Last(), YVoltages[0], resetSampleCount);
            Array.Copy(y1ResetVoltages, 0, Y1Wave, 0, resetSampleCount);
            if (config.SelectedScannerHead.ID == ScannerHeadModel.THREE_SCANNERS)
            {
                double[] y2ResetVoltages = CreateLinearArray(YVoltages.Last() * 2, YVoltages[0] * 2, resetSampleCount);
                Array.Copy(y2ResetVoltages, 0, Y2Wave, 0, resetSampleCount);
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

        /// <summary>
        /// 初始化
        /// </summary>
        private void WaveInitialize()
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

            if (ConfigViewModel.GetConfig().SelectedScannerHead.ID == ScannerHeadModel.THREE_SCANNERS)
            {
                if (Y2Wave == null || Y2Wave.Length != OutputSampleCountPerFrame)
                {
                    Y2Wave = new double[OutputSampleCountPerFrame];
                }
            }
        }

        private SequenceModel()
        {
            
        }

    }
}
