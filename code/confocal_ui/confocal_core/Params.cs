using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core
{
    /// <summary>
    /// 根据扫描参数配置，计算得到的相应参数，如AO输出速率、AO前置/后置样本数量、fps等
    /// </summary>
    public class Params
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static Params m_params = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int DIGITAL_TRIGGER_PULSE_WIDTH = 10;        // 数字行触发脉冲宽度，10个DO Sample Clock 
        public static readonly double MAXIMUM_VOLTAGE_DIFF_PER_PIXEL = 0.2; // 像素间的最大电压差
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Config m_config;
        ///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 扫描帧率
        /// </summary>
        public double Fps { get; set; }
        /// <summary>
        /// AO输出速率，单位：xx Sample/s
        /// </summary>
        public double AoSampleRate { get; set; }
        /// <summary>
        /// 像素尺寸，单位：um
        /// </summary>
        public double PixelSize { get; set; }
        /// <summary>
        /// 相邻像素间电压差，单位：V
        /// </summary>
        public double VoltagePerPixel { get; set; }
        /// <summary>
        /// 每行前置输出样本数量
        /// </summary>
        public int PreviousSampleCountPerLine { get; set; }
        /// <summary>
        /// 每行有效输出样本数量
        /// </summary>
        public int ValidSampleCountPerLine { get; set; }
        /// <summary>
        /// 每行后置输出样本数量
        /// </summary>
        public int PostSampleCountPerLine { get; set; }
        /// <summary>
        /// 单行输出样本数量，包括前置样本、有效样本、后置样本
        /// </summary>
        public int SampleCountPerLine { get; set; }
        /// <summary>
        /// 扫描行数
        /// (1) 对于Z形单向扫描，等于config.ScanYPoints
        /// (2) 对于Z行双向扫描，等于config.ScanYPoints / 2
        /// </summary>
        public int ScanRows { get; set; }
        /// <summary>
        /// 单帧输出样本数量
        /// </summary>
        public int SampleCountPerFrame { get; set; }
        /// <summary>
        /// 单帧的有效样本数量
        /// </summary>
        public int ValidSampleCountPerFrame { get; set; }
        /// <summary>
        /// 单行有效样本的扫描时间，单位：ms
        /// </summary>
        public double ValidSampleScanTimePerLine { get; set; }
        /// <summary>
        /// 扫描单行的有效样本，X振镜的电压变化幅值，单位：V
        /// </summary>
        public double ValidSampleVoltagePerLine { get; set; }
        /// <summary>
        /// 扫描一帧Y[Y1]振镜的电压变化幅值，单位：V
        /// </summary>
        public double VoltagePerRow { get; set; }
        /// <summary>
        /// 每行输出样本数据（X振镜的电压变化曲线），单元：V
        /// </summary>
        public double[] XSamplesPerLine { get; set; }
        /// <summary>
        /// 每列输出样本数据（Y1振镜的电压变化曲线），每个数据代表Y1振镜在扫描到该行时的电压，单位：V
        /// </summary>
        public double[] Y1SamplesPerRow { get; set; }
        /// <summary>
        /// 每列输出样本数据（Y2振镜的电压变化曲线），每个数据代表Y2振镜在扫描到该行时的电压，单位：V
        /// </summary>
        public double[] Y2SamplesPerRow { get; set; }
        /// <summary>
        /// 每行DO输出数据（用于每次在AO输出有效样本的开始时刻，产生数字触发脉冲，触发AI采集数据）
        /// </summary>
        public byte[] DigitalTriggerSamplesPerLine { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////////////

        #region apis

        public static Params GetParams()
        {
            if (m_params == null)
            {
                lock (locker)
                {
                    if (m_params == null)
                    {
                        m_params = new Params();
                    }
                }
            }
            return m_params;
        }

        /// <summary>
        /// 根据Config配置参数，计算相应的结果，包括：像素尺寸、帧率、模拟输出速率等
        /// </summary>
        public void Calculate()
        {
            switch (m_config.GetScanStrategy())
            {
                case SCAN_STRATEGY.Z_UNIDIRECTION:
                    CalculateSScan();
                    break;
                case SCAN_STRATEGY.Z_BIDIRECTION:
                    CalculateBScan();
                    break;
                default:
                    CalculateSScan();
                    break;
            }
        }

        private void CalculateSScan()
        {
            double maximumVolatgeStep = MAXIMUM_VOLTAGE_DIFF_PER_PIXEL;

            double aoSampleRate = 1e6 / m_config.GetScanDwellTime();
            double pixelSize = m_config.GetScanFieldSize() / m_config.GetScanXPoints();
            double voltagePerPixel = m_config.GetScanCalibrationVoltage() / 5.0 * 1000 * pixelSize;

            int realScanXPixels = m_config.GetScanXPoints() + m_config.GetScanPixelCompensation();
            int realScanYPixels = m_config.GetScanYPoints() / 2;

            double w = m_config.GetScanDwellTime() * realScanXPixels / 1000;
            double h = voltagePerPixel * realScanXPixels;
            double h2 = voltagePerPixel * m_config.GetScanYPoints();
            double cosx = w / Math.Sqrt(w * w + h * h);
            double sinx = Math.Sqrt(1 - cosx * cosx);
            double r = m_config.GetScanCurveCoff() / 100 * h / (1 - cosx);

            // 从prev_x0到prev_xn总共的samples数量
            int previousTotalSampleCount = (int)(2 * r * sinx / w * realScanXPixels);
            // 计算每行前置输出样本数量
            int previousSampleCountPerLine = (int)Math.Ceiling(m_config.GetGalvResponseTime() / m_config.GetScanDwellTime());

            // 计算每行有效输出样本数量
            int validSampleCountPerLine = realScanXPixels;

            int postFirstTotalSampleCount = (int)(2 * r * sinx / w * realScanXPixels);
            // 计算每行后置输出样本数量
            int postFirstSampleCount = (int)Math.Ceiling(m_config.GetGalvResponseTime() / m_config.GetScanDwellTime());
            int postSecondSampleCount = (int)Math.Ceiling(m_config.GetGalvResponseTime() / m_config.GetScanDwellTime() / 2);
            int postSecondMinimumSampleCount = (int)(h / maximumVolatgeStep);
            if (postSecondSampleCount < postSecondMinimumSampleCount)
            {
                postSecondSampleCount = postSecondMinimumSampleCount;
            }
            int postSampleCountPerLine = postFirstSampleCount + postSecondSampleCount;

            // 每行输出样本数量
            int sampleCountPerLine = previousSampleCountPerLine + validSampleCountPerLine + postSampleCountPerLine;

            // 计算帧率
            double fps = aoSampleRate / (sampleCountPerLine * realScanYPixels);

            double xn;
            // 计算逐行前置输出样本
            double[] previousSamplesPerLine = new double[previousSampleCountPerLine];
            double prev_xc = -(w / 2 + r * sinx);       // 前置扫描曲线圆心的x坐标值
            double prev_yc = -(h / 2 - r * cosx);       // 前置扫描曲线圆心的y坐标值
            double prev_x0 = -(w / 2 + 2 * r * sinx);
            double prev_y0 = -h / 2;

            for (int i = 0; i < (previousSampleCountPerLine + 1) / 2; i++)
            {
                xn = prev_x0 + 2 * r * sinx / previousTotalSampleCount * i;
                previousSamplesPerLine[i] = -Math.Sqrt(r * r - (xn - prev_xc) * (xn - prev_xc)) + prev_yc;
                previousSamplesPerLine[previousSampleCountPerLine - 1 - i] = previousSamplesPerLine[i];
            }

            // 计算逐行有效输出样本
            double[] validSamplesPerLine = new double[validSampleCountPerLine];
            for (int i = 0; i < validSampleCountPerLine; i++)
            {
                validSamplesPerLine[i] = (i - (validSampleCountPerLine / 2)) * voltagePerPixel;
            }

            // 计算逐行后置输出样本
            double[] postSamplesPerLine = new double[postSampleCountPerLine];
            double post_xc = w / 2 + r * sinx;
            double post_yc = h / 2 - r * cosx;

            for (int i = 0; i < (postFirstSampleCount + 1) / 2; i++)
            {
                xn = w / 2 + 2 * r * sinx / postFirstTotalSampleCount * i;
                postSamplesPerLine[i] = Math.Sqrt(r * r - (xn - post_xc) * (xn - post_xc)) + post_yc;
                postSamplesPerLine[postFirstSampleCount - 1 - i] = postSamplesPerLine[i];
            }

            for (int i = 0; i < postSecondSampleCount; i++)
            {
                postSamplesPerLine[i + postFirstSampleCount] = h / 2 - h / postSecondSampleCount * i;
            }

            // 生成单行X振镜的电压变化曲线
            double[] xSamplesPerLine = new double[sampleCountPerLine];
            Array.Copy(previousSamplesPerLine, xSamplesPerLine, previousSampleCountPerLine);
            Array.Copy(validSamplesPerLine, 0, xSamplesPerLine, previousSampleCountPerLine, validSampleCountPerLine);
            Array.Copy(postSamplesPerLine, 0, xSamplesPerLine, previousSampleCountPerLine + validSampleCountPerLine, postSampleCountPerLine);

            int sampleCountPerFrame = sampleCountPerLine * realScanYPixels;

            double[] y1SamplesPerRow = new double[realScanYPixels];
            double[] y2SamplesPerRow = new double[realScanYPixels];
            double yn;
            for (int i = 0; i < realScanYPixels; i++)
            {
                yn = voltagePerPixel * i - h2 / 2;
                y1SamplesPerRow[i] = yn;
                y2SamplesPerRow[i] = yn * 2;
            }

            // 生成单行数字触发波形
            byte[] digitalTriggerSamplesPerLine = new byte[sampleCountPerLine];
            for (int i = 0; i < sampleCountPerLine; i++)
            {
                digitalTriggerSamplesPerLine[i] = 0x00;
                if (i >= previousSampleCountPerLine && i < previousSampleCountPerLine + Params.DIGITAL_TRIGGER_PULSE_WIDTH)
                {
                    digitalTriggerSamplesPerLine[i] = 0x01;
                }
            }

            m_params.Fps = fps;                         // 帧率
            m_params.AoSampleRate = aoSampleRate;       // 模拟输出率
            m_params.PixelSize = pixelSize;             // 像素尺寸
            m_params.VoltagePerPixel = voltagePerPixel;
            m_params.PreviousSampleCountPerLine = previousSampleCountPerLine;
            m_params.ValidSampleCountPerLine = validSampleCountPerLine;
            m_params.PostSampleCountPerLine = postSampleCountPerLine;
            m_params.SampleCountPerLine = sampleCountPerLine;
            m_params.ScanRows = realScanYPixels;

            m_params.ValidSampleScanTimePerLine = w;
            m_params.ValidSampleVoltagePerLine = h;
            m_params.VoltagePerRow = h2;

            m_params.SampleCountPerFrame = sampleCountPerFrame;
            m_params.ValidSampleCountPerFrame = m_params.ValidSampleCountPerLine * m_params.ScanRows;
            m_params.XSamplesPerLine = xSamplesPerLine;
            m_params.Y1SamplesPerRow = y1SamplesPerRow;
            m_params.Y2SamplesPerRow = y2SamplesPerRow;
            m_params.DigitalTriggerSamplesPerLine = digitalTriggerSamplesPerLine;
        }

        private void CalculateBScan()
        {
            double maximumVolatgeStep = MAXIMUM_VOLTAGE_DIFF_PER_PIXEL;

            double aoSampleRate = 1e6 / m_config.GetScanDwellTime();
            double pixelSize = m_config.GetScanFieldSize() / m_config.GetScanXPoints();
            double voltagePerPixel = m_config.GetScanCalibrationVoltage() / 5.0 * 1000 * pixelSize;

            int realScanXPixels = m_config.GetScanXPoints() + m_config.GetScanPixelCompensation();
            int realScanYPixels = m_config.GetScanYPoints() / 2;

            double w = m_config.GetScanDwellTime() * realScanXPixels / 1000;    // ms
            double h = voltagePerPixel * realScanXPixels;                       
            double h2 = voltagePerPixel * m_config.GetScanYPoints();
            double cosx = w / Math.Sqrt(w * w + h * h);
            double sinx = Math.Sqrt(1 - cosx * cosx);
            double r = m_config.GetScanCurveCoff() / 100 * h / (1 - cosx);

            // 从prev_x0到prev_xn总共的samples数量
            int previousTotalSampleCount = (int)(2 * r * sinx / w * realScanXPixels);
            // 计算每行前置输出样本数量
            int previousSampleCountPerLine = (int)Math.Ceiling(m_config.GetGalvResponseTime() / m_config.GetScanDwellTime());

            // 计算每行有效输出样本数量
            int validSampleCountPerLine = realScanXPixels;

            int postTotalSampleCount = (int)(2 * r * sinx / w * realScanXPixels);
            // 计算每行后置输出样本数量
            int postSampleCountPerLine = (int)Math.Ceiling(m_config.GetGalvResponseTime() / m_config.GetScanDwellTime());

            // 每行输出样本数量
            int sampleCountPerLine = previousSampleCountPerLine + validSampleCountPerLine * 2 + postSampleCountPerLine;

            // 计算帧率
            double fps = aoSampleRate / (sampleCountPerLine * realScanYPixels);

            double xn;
            // 计算逐行前置输出样本
            double[] previousSamplesPerLine = new double[previousSampleCountPerLine];
            double prev_xc = -(w / 2 + r * sinx);       // 前置扫描曲线圆心的x坐标值
            double prev_yc = -(h / 2 - r * cosx);       // 前置扫描曲线圆心的y坐标值
            double prev_x0 = -(w / 2 + 2 * r * sinx);
            double prev_y0 = -h / 2;

            for (int i = 0; i < (previousSampleCountPerLine + 1) / 2; i++)
            {
                xn = prev_x0 + 2 * r * sinx / previousTotalSampleCount * i;
                previousSamplesPerLine[i] = -Math.Sqrt(r * r - (xn - prev_xc) * (xn - prev_xc)) + prev_yc;
                previousSamplesPerLine[previousSampleCountPerLine - 1 - i] = previousSamplesPerLine[i];
            }

            // 计算逐行有效输出样本
            double[] validFirstSamplesPerLine = new double[validSampleCountPerLine];
            double[] validSecondSamplesPerLine = new double[validSampleCountPerLine];
            for (int i = 0; i < validSampleCountPerLine; i++)
            {
                validFirstSamplesPerLine[i] = (i - (validSampleCountPerLine / 2)) * voltagePerPixel;
            }
            Array.Copy(validFirstSamplesPerLine, validSecondSamplesPerLine, validSampleCountPerLine);
            Array.Reverse(validSecondSamplesPerLine);

            // 计算逐行后置输出样本
            double[] postSamplesPerLine = new double[postSampleCountPerLine];
            double post_xc = w / 2 + r * sinx;
            double post_yc = h / 2 - r * cosx;

            for (int i = 0; i < (postSampleCountPerLine + 1) / 2; i++)
            {
                xn = w / 2 + 2 * r * sinx / postTotalSampleCount * i;
                postSamplesPerLine[i] = Math.Sqrt(r * r - (xn - post_xc) * (xn - post_xc)) + post_yc;
                postSamplesPerLine[postSampleCountPerLine - 1 - i] = postSamplesPerLine[i];
            }

            // 生成单行X振镜的电压变化曲线
            double[] xSamplesPerLine = new double[sampleCountPerLine];
            Array.Copy(previousSamplesPerLine, xSamplesPerLine, previousSampleCountPerLine);
            Array.Copy(validFirstSamplesPerLine, 0, xSamplesPerLine, previousSampleCountPerLine, validSampleCountPerLine);
            Array.Copy(postSamplesPerLine, 0, xSamplesPerLine, previousSampleCountPerLine + validSampleCountPerLine, postSampleCountPerLine);
            Array.Copy(validSecondSamplesPerLine, 0, xSamplesPerLine, previousSampleCountPerLine + validSampleCountPerLine + postSampleCountPerLine, validSampleCountPerLine);

            int sampleCountPerFrame = sampleCountPerLine * realScanYPixels;

            double[] y1SamplesPerRow = new double[realScanYPixels];
            double[] y2SamplesPerRow = new double[realScanYPixels];
            double yn;
            for (int i = 0; i < realScanYPixels; i++)
            {
                yn = voltagePerPixel * i * 2 - h2 / 2;
                y1SamplesPerRow[i] = yn;
                y2SamplesPerRow[i] = yn * 2;
            }

            // 生成单行数字触发波形
            byte[] digitalTriggerSamplesPerLine = new byte[sampleCountPerLine];
            int firstValidSamplesIndex = previousSampleCountPerLine;
            int secondValidSamplesIndex = previousSampleCountPerLine + validSampleCountPerLine + postSampleCountPerLine;
            for (int i = 0; i < sampleCountPerLine; i++)
            {
                digitalTriggerSamplesPerLine[i] = 0x00;
                if (i >= firstValidSamplesIndex && i < firstValidSamplesIndex + Params.DIGITAL_TRIGGER_PULSE_WIDTH)
                {
                    digitalTriggerSamplesPerLine[i] = 0x01;
                }
                else if (i >= secondValidSamplesIndex && i < secondValidSamplesIndex + Params.DIGITAL_TRIGGER_PULSE_WIDTH)
                {
                    digitalTriggerSamplesPerLine[i] = 0x01;
                }
            }

            m_params.Fps = fps;
            m_params.AoSampleRate = aoSampleRate;
            m_params.PixelSize = pixelSize;
            m_params.VoltagePerPixel = voltagePerPixel;
            m_params.PreviousSampleCountPerLine = previousSampleCountPerLine;
            m_params.ValidSampleCountPerLine = validSampleCountPerLine;
            m_params.PostSampleCountPerLine = postSampleCountPerLine;
            m_params.SampleCountPerLine = sampleCountPerLine;
            m_params.ScanRows = realScanYPixels;

            m_params.ValidSampleScanTimePerLine = w;
            m_params.ValidSampleVoltagePerLine = h;
            m_params.VoltagePerRow = h2;

            m_params.SampleCountPerFrame = sampleCountPerFrame;
            m_params.ValidSampleCountPerFrame = m_params.ValidSampleCountPerLine * m_params.ScanRows * 2;
            m_params.XSamplesPerLine = xSamplesPerLine;
            m_params.Y1SamplesPerRow = y1SamplesPerRow;
            m_params.Y2SamplesPerRow = y2SamplesPerRow;
            m_params.DigitalTriggerSamplesPerLine = digitalTriggerSamplesPerLine;
        }

        private Params()
        {
            m_config = Config.GetConfig();
        }

        #endregion

    }

}
