using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Common
{
    public class Sequence
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
