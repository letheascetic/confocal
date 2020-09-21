using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private static readonly int DIGITAL_TRIGGER_PULSE_WIDTH = 20;        // 数字行触发脉冲宽度，20个DO Sample Clock 
        private static readonly double MAXIMUM_VOLTAGE_DIFF_PER_PIXEL = 0.1; // 像素间的最大电压差
        private static readonly double DEFAULT_AO_SAMPLE_RATE = 250000;      // 默认AO输出速率
        private static readonly double DEFAULT_ACQUISITION_INTERVAL = 20.0;  // 默认采集时间间隔，单位：ms，WINDOWS时间片轮询时间为20ms左右
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Config m_config;
        private SysConfig m_sysConfig;
        private byte[][,] m_colorMappingArr;
        private int[] m_aiChannelIndex;
        ///////////////////////////////////////////////////////////////////////////////////////////
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
        /// 单行采集的有效像素数
        /// </summary>
        public int ValidScanPixelsPerLine { get; set; }
        /// <summary>
        /// 单帧采集的有效像素数
        /// </summary>
        public int ValidScanPixelsPerFrame { get; set; }
        /// <summary>
        /// 每行输出触发信号样本数
        /// </summary>
        public int DoSampleCountPerLine { get; set; }
        /// <summary>
        /// 相邻像素间电压差，单位：V
        /// </summary>
        public double AoVoltagePerPixel { get; set; }
        /// <summary>
        /// 每行前置输出样本数量
        /// </summary>
        public int AoPreviousSampleCountPerLine { get; set; }
        /// <summary>
        /// 每行有效输出样本数量
        /// </summary>
        public int AoValidSampleCountPerLine { get; set; }
        /// <summary>
        /// 每行后置输出样本数量
        /// </summary>
        public int AoPostSampleCountPerLine { get; set; }
        /// <summary>
        /// 单行输出样本数量，包括前置样本、有效样本、后置样本
        /// </summary>
        public int AoSampleCountPerLine { get; set; }
        /// <summary>
        /// 扫描行数
        /// (1) 对于Z形单向扫描，等于config.ScanYPoints
        /// (2) 对于Z行双向扫描，等于config.ScanYPoints / 2
        /// </summary>
        public int ScanRows { get; set; }
        /// <summary>
        /// 每次采集扫描的行数
        /// </summary>
        public int ScanRowsPerAcquisition{ get; set; }
        /// <summary>
        /// 每次采集的像素数
        /// </summary>
        public int ScanPixelsPerAcquisition { get; set; }
        /// <summary>
        /// 每次采集的时间间隔，单位：ms
        /// </summary>
        public double IntervalPerAcquisition { get; set; }
        /// <summary>
        /// 单帧输出样本数量
        /// </summary>
        public int AoSampleCountPerFrame { get; set; }
        /// <summary>
        /// 单帧的有效样本数量
        /// </summary>
        public int AoValidSampleCountPerFrame { get; set; }
        /// <summary>
        /// 单行扫描时间，单位：ms
        /// </summary>
        public double ScanTimePerLine { get; set; }
        /// <summary>
        /// 扫描单行的有效样本，X振镜的电压变化幅值，单位：V
        /// </summary>
        public double AoValidSampleVoltagePerLine { get; set; }
        /// <summary>
        /// 扫描一帧Y[Y1]振镜的电压变化幅值，单位：V
        /// </summary>
        public double AoVoltagePerRow { get; set; }
        /// <summary>
        /// 每行输出样本数据（X振镜的电压变化曲线），单元：V
        /// </summary>
        public double[] AoXSamplesPerLine { get; set; }
        /// <summary>
        /// 每列输出样本数据（Y1振镜的电压变化曲线），每个数据代表Y1振镜在扫描到该行时的电压，单位：V
        /// </summary>
        public double[] AoY1SamplesPerRow { get; set; }
        /// <summary>
        /// 每列输出样本数据（Y2振镜的电压变化曲线），每个数据代表Y2振镜在扫描到该行时的电压，单位：V
        /// </summary>
        public double[] AoY2SamplesPerRow { get; set; }
        /// <summary>
        /// 每行DO输出数据（用于每次在AO输出有效样本的开始时刻，产生数字触发脉冲，触发AI采集数据）
        /// </summary>
        public byte[] DigitalTriggerSamplesPerLine { get; set; }
        /// <summary>
        /// 伪彩色转换映射数组
        /// </summary>
        public byte[][,] ColorMappingArr
        { get { return m_colorMappingArr; } set { m_colorMappingArr = value; } }
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
                    GenerateSingleScanWave();
                    GenerateSingleTriggerWave();
                    break;
                case SCAN_STRATEGY.Z_BIDIRECTION:
                    GenerateDoubleScanWave();
                    GenerateDoubleTriggerWave();
                    break;
                default:
                    GenerateSingleScanWave();
                    GenerateSingleTriggerWave();
                    break;
            }
        }

        public void GenerateColorMapping()
        {
            int channelNum = m_config.GetChannelNum();
            for (int i = 0; i < channelNum; i++)
            {
                CHAN_ID id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i);
                Color colorReference = m_config.GetChannelColorReference(id);
                CImage.CreateColorMapping(colorReference, ref m_colorMappingArr[i]);
            }
        }

        public void GenerateAiChannelIndex()
        {
            int index = -1;
            int channelNum = m_config.GetChannelNum();
            for (int i = 0; i < channelNum; i++)
            {
                m_aiChannelIndex[i] = m_config.GetLaserSwitch((CHAN_ID)i) == LASER_CHAN_SWITCH.ON ? ++index : -1;
            }
        }

        public int GetLaserAiChannelIndex(int channelId)
        {
            return m_aiChannelIndex[channelId];
        }

        private void GenerateSingleScanWave()
        {
            double maximumVolatgeStep = MAXIMUM_VOLTAGE_DIFF_PER_PIXEL;

            double pixelSampleRate = 1e6 / m_config.GetScanDwellTime();     
            double pixelSamplePeriod = m_config.GetScanDwellTime();               // 像素采样周期，单位us

            double aoSampleRate = pixelSampleRate > DEFAULT_AO_SAMPLE_RATE ? DEFAULT_AO_SAMPLE_RATE : pixelSampleRate;
            double aoSamplePeriod = 1e6 / aoSampleRate;                 // AO输出周期，单位us

            double pixelSize = m_config.GetScanFieldSize() / m_config.GetScanXPoints();             // 像素尺寸，单位：um/pixel            
            double voltagePerPixel = m_config.GetScanCalibrationVoltage() / 5.0 * 1000 * pixelSize; // 像素电压，单位：V/pixel

            int realScanXPixels = m_config.GetScanXPoints() + m_config.GetScanPixelCompensation();
            int realScanYPixels = m_config.GetScanYPoints();

            double w = m_config.GetScanDwellTime() * realScanXPixels / 1000;        // 行有效样本区间的时间范围，单位：ms
            double h = voltagePerPixel * realScanXPixels;                           // 行有效样本区间的电压范围，单位：V
            double h2 = voltagePerPixel * m_config.GetScanYPoints();                // 列样本区间的电压范围，单位：V
            double cosx = w / Math.Sqrt(w * w + h * h);                             // 倾斜角度计算
            double sinx = Math.Sqrt(1 - cosx * cosx);                               
            double r = m_config.GetScanCurveCoff() / 100 * h / (1 - cosx);          // 弧度半径计算

            RectangleF scanRange = m_config.GetScanFieldRange();
            double xCenter = (scanRange.Left + scanRange.Right) / 2;
            double yCenter = (scanRange.Top + scanRange.Bottom) / 2;
            double xVoltageOffset = xCenter / pixelSize * voltagePerPixel;
            double yVoltageOffset = yCenter / pixelSize * voltagePerPixel;

            // 从prev_x0到prev_xn总共的samples数量
            int previousTotalSampleCount = (int)(2 * r * sinx / aoSamplePeriod * 1000);
            // 计算每行前置输出样本数量
            int previousSampleCountPerLine = (int)Math.Ceiling(m_config.GetGalvResponseTime() / aoSamplePeriod);

            // 计算每行有效输出样本数量
            int validSampleCountPerLine = (int)(w * 1000 / aoSamplePeriod);

            int postFirstTotalSampleCount = (int)(2 * r * sinx / aoSamplePeriod * 1000);
            // 计算每行后置输出样本数量
            int postFirstSampleCount = (int)Math.Ceiling(m_config.GetGalvResponseTime() / aoSamplePeriod);
            int postSecondSampleCount = (int)Math.Ceiling(m_config.GetGalvResponseTime() / aoSamplePeriod / 2);
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
                previousSamplesPerLine[i] = -Math.Sqrt(r * r - (xn - prev_xc) * (xn - prev_xc)) + prev_yc + xVoltageOffset;
                previousSamplesPerLine[previousSampleCountPerLine - 1 - i] = previousSamplesPerLine[i];
            }

            // 计算逐行有效输出样本
            double[] validSamplesPerLine = new double[validSampleCountPerLine];
            double half_h = h / 2;
            double step_h = h / validSampleCountPerLine;
            for (int i = 0; i < validSampleCountPerLine; i++)
            {
                validSamplesPerLine[i] = -half_h + i * step_h + xVoltageOffset;
            }

            // 计算逐行后置输出样本
            double[] postSamplesPerLine = new double[postSampleCountPerLine];
            double post_xc = w / 2 + r * sinx;
            double post_yc = h / 2 - r * cosx;

            for (int i = 0; i < (postFirstSampleCount + 1) / 2; i++)
            {
                xn = w / 2 + 2 * r * sinx / postFirstTotalSampleCount * i;
                postSamplesPerLine[i] = Math.Sqrt(r * r - (xn - post_xc) * (xn - post_xc)) + post_yc + xVoltageOffset;
                postSamplesPerLine[postFirstSampleCount - 1 - i] = postSamplesPerLine[i];
            }

            for (int i = 0; i < postSecondSampleCount; i++)
            {
                postSamplesPerLine[i + postFirstSampleCount] = h / 2 - h / postSecondSampleCount * i + xVoltageOffset;
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
                y1SamplesPerRow[i] = yn + yVoltageOffset;
                y2SamplesPerRow[i] = y1SamplesPerRow[i] * 2;
            }

            m_params.Fps = fps;                         // 帧率
            m_params.PixelSampleRate = pixelSampleRate; // 像素生成速率
            m_params.AoSampleRate = aoSampleRate;       // 模拟输出率
            m_params.AiSampleRate = pixelSampleRate;    // 模拟输入采样率
            m_params.CtrSampleRate = pixelSampleRate;   // 计数器计数速率

            m_params.PixelSize = pixelSize;             // 像素尺寸
            m_params.AoVoltagePerPixel = voltagePerPixel;
            m_params.AoPreviousSampleCountPerLine = previousSampleCountPerLine;
            m_params.AoValidSampleCountPerLine = validSampleCountPerLine;
            m_params.AoPostSampleCountPerLine = postSampleCountPerLine;
            m_params.AoSampleCountPerLine = sampleCountPerLine;
            m_params.ValidScanPixelsPerLine = realScanXPixels;
            m_params.ScanRows = realScanYPixels;
            m_params.ValidScanPixelsPerFrame = realScanXPixels * realScanYPixels;

            m_params.ScanTimePerLine = sampleCountPerLine * aoSamplePeriod / 1000;
            m_params.AoValidSampleVoltagePerLine = h;
            m_params.AoVoltagePerRow = h2;

            m_params.AoSampleCountPerFrame = sampleCountPerFrame;
            m_params.AoValidSampleCountPerFrame = m_params.AoValidSampleCountPerLine * m_params.ScanRows;
            m_params.AoXSamplesPerLine = xSamplesPerLine;
            m_params.AoY1SamplesPerRow = y1SamplesPerRow;
            m_params.AoY2SamplesPerRow = y2SamplesPerRow;

            m_params.ScanRowsPerAcquisition = 1;
            // m_params.ScanRowsPerAcquisition = (int)Math.Ceiling(DEFAULT_ACQUISITION_INTERVAL / m_params.ScanTimePerLine);
            m_params.ScanPixelsPerAcquisition = m_params.ScanRowsPerAcquisition * m_params.ValidScanPixelsPerLine;
            m_params.IntervalPerAcquisition = m_params.ScanRowsPerAcquisition * m_params.ScanTimePerLine;
        }

        private void GenerateSingleTriggerWave()
        {
            if (m_sysConfig.GetAcqDevice() == ACQ_DEVICE.PMT)
            {
                // 如果是PMT采集，则DoSampleRate=AoSampleRate，DoSampleCountPerLine=AoSampleCountPerLine
                m_params.DoSampleRate = m_params.AoSampleRate;                  
                m_params.DoSampleCountPerLine = m_params.AoSampleCountPerLine;
                m_params.DigitalTriggerSamplesPerLine = Enumerable.Repeat<byte>(0, m_params.DoSampleCountPerLine).ToArray();

                int segStart = m_params.AoPreviousSampleCountPerLine;
                int segEnd = segStart + Params.DIGITAL_TRIGGER_PULSE_WIDTH;
                for (int i = segStart; i < segEnd; i++)
                {
                    m_params.DigitalTriggerSamplesPerLine[i] = 0x01;
                }
            }
            else
            {
                // 如果是APD采集，则DoSampleRate=PixelSampleRate*2，DoSampleCountPerLine按输出比例相应放大
                m_params.DoSampleRate = m_params.PixelSampleRate * 2;
                m_params.DoSampleCountPerLine = (int)(m_params.DoSampleRate / m_params.AoSampleRate * m_params.AoSampleCountPerLine);
                m_params.DigitalTriggerSamplesPerLine = Enumerable.Repeat<byte>(0, m_params.DoSampleCountPerLine).ToArray();

                int segStart = (int)(m_params.DoSampleRate / m_params.AoSampleRate * m_params.AoPreviousSampleCountPerLine);
                int segEnd = segStart + m_params.ValidScanPixelsPerLine * 2;
                for (int i = segStart; i < segEnd; i = i + 2)
                {
                    m_params.DigitalTriggerSamplesPerLine[i] = 0x01;
                }
            }
        }

        private void GenerateDoubleScanWave()
        {
            double maximumVolatgeStep = MAXIMUM_VOLTAGE_DIFF_PER_PIXEL;

            double pixelSampleRate = 1e6 / m_config.GetScanDwellTime();
            double pixelSamplePeriod = m_config.GetScanDwellTime();               // 像素采样周期，单位us

            double aoSampleRate = pixelSampleRate > DEFAULT_AO_SAMPLE_RATE ? DEFAULT_AO_SAMPLE_RATE : pixelSampleRate;
            double aoSamplePeriod = 1e6 / aoSampleRate;                 // AO输出周期，单位us

            double pixelSize = m_config.GetScanFieldSize() / m_config.GetScanXPoints();             // 像素尺寸，单位：um/pixel            
            double voltagePerPixel = m_config.GetScanCalibrationVoltage() / 5.0 * 1000 * pixelSize; // 像素电压，单位：V/pixel

            int realScanXPixels = m_config.GetScanXPoints() + m_config.GetScanPixelCompensation();
            int realScanYPixels = m_config.GetScanYPoints() / 2;

            double w = m_config.GetScanDwellTime() * realScanXPixels / 1000;    // ms
            double h = voltagePerPixel * realScanXPixels;
            double h2 = voltagePerPixel * m_config.GetScanYPoints();
            double cosx = w / Math.Sqrt(w * w + h * h);
            double sinx = Math.Sqrt(1 - cosx * cosx);
            double r = m_config.GetScanCurveCoff() / 100 * h / (1 - cosx);

            RectangleF scanRange = m_config.GetScanFieldRange();
            double xCenter = (scanRange.Left + scanRange.Right) / 2;
            double yCenter = (scanRange.Top + scanRange.Bottom) / 2;
            double xVoltageOffset = xCenter / pixelSize * voltagePerPixel;
            double yVoltageOffset = yCenter / pixelSize * voltagePerPixel;

            // 从prev_x0到prev_xn总共的samples数量
            int previousTotalSampleCount = (int)(2 * r * sinx / aoSamplePeriod * 1000);
            // 计算每行前置输出样本数量
            int previousSampleCountPerLine = (int)Math.Ceiling(m_config.GetGalvResponseTime() / aoSamplePeriod);

            // 计算每行有效输出样本数量
            int validSampleCountPerLine = (int)(w * 1000 / aoSamplePeriod);

            int postTotalSampleCount = (int)(2 * r * sinx / aoSamplePeriod * 1000);
            // 计算每行后置输出样本数量
            int postSampleCountPerLine = (int)Math.Ceiling(m_config.GetGalvResponseTime() / aoSamplePeriod);

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
                previousSamplesPerLine[i] = -Math.Sqrt(r * r - (xn - prev_xc) * (xn - prev_xc)) + prev_yc + xVoltageOffset;
                previousSamplesPerLine[previousSampleCountPerLine - 1 - i] = previousSamplesPerLine[i];
            }

            // 计算逐行有效输出样本
            double[] validFirstSamplesPerLine = new double[validSampleCountPerLine];
            double[] validSecondSamplesPerLine = new double[validSampleCountPerLine];
            double half_h = h / 2;
            double step_h = h / validSampleCountPerLine;
            for (int i = 0; i < validSampleCountPerLine; i++)
            {
                validFirstSamplesPerLine[i] = -half_h + i * step_h + xVoltageOffset;
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
                postSamplesPerLine[i] = Math.Sqrt(r * r - (xn - post_xc) * (xn - post_xc)) + post_yc + xVoltageOffset;
                postSamplesPerLine[postSampleCountPerLine - 1 - i] = postSamplesPerLine[i];
            }

            // 生成单行X振镜的电压变化曲线
            double[] xSamplesPerLine = new double[sampleCountPerLine];
            Array.Copy(previousSamplesPerLine, xSamplesPerLine, previousSampleCountPerLine);
            Array.Copy(validFirstSamplesPerLine, 0, xSamplesPerLine, previousSampleCountPerLine, validSampleCountPerLine);
            Array.Copy(postSamplesPerLine, 0, xSamplesPerLine, previousSampleCountPerLine + validSampleCountPerLine, postSampleCountPerLine);
            Array.Copy(validSecondSamplesPerLine, 0, xSamplesPerLine, previousSampleCountPerLine + validSampleCountPerLine + postSampleCountPerLine, validSampleCountPerLine);

            int sampleCountPerFrame = sampleCountPerLine * realScanYPixels;

            double[] y1SamplesPerRow = new double[m_config.GetScanYPoints()];
            double[] y2SamplesPerRow = new double[m_config.GetScanYPoints()];
            double yn;
            for (int i = 0; i < m_config.GetScanYPoints(); i++)
            {
                yn = voltagePerPixel * i - h2 / 2;
                y1SamplesPerRow[i] = yn + yVoltageOffset;
                y2SamplesPerRow[i] = y1SamplesPerRow[i] * 2;
            }

            m_params.Fps = fps;                         // 帧率
            m_params.PixelSampleRate = pixelSampleRate; // 像素生成速率
            m_params.AoSampleRate = aoSampleRate;       // 模拟输出率
            m_params.AiSampleRate = pixelSampleRate;    // 模拟输入采样率
            m_params.CtrSampleRate = pixelSampleRate;   // 计数器计数速率

            m_params.PixelSize = pixelSize;
            m_params.AoVoltagePerPixel = voltagePerPixel;
            m_params.AoPreviousSampleCountPerLine = previousSampleCountPerLine;
            m_params.AoValidSampleCountPerLine = validSampleCountPerLine;
            m_params.AoPostSampleCountPerLine = postSampleCountPerLine;
            m_params.AoSampleCountPerLine = sampleCountPerLine;
            m_params.ValidScanPixelsPerLine = realScanXPixels;
            m_params.ScanRows = realScanYPixels;
            m_params.ValidScanPixelsPerFrame = realScanXPixels * realScanYPixels * 2;

            m_params.ScanTimePerLine = sampleCountPerLine * aoSamplePeriod / 1000;
            m_params.AoValidSampleVoltagePerLine = h;
            m_params.AoVoltagePerRow = h2;

            m_params.AoSampleCountPerFrame = sampleCountPerFrame;
            m_params.AoValidSampleCountPerFrame = m_params.AoValidSampleCountPerLine * m_params.ScanRows * 2;
            m_params.AoXSamplesPerLine = xSamplesPerLine;
            m_params.AoY1SamplesPerRow = y1SamplesPerRow;
            m_params.AoY2SamplesPerRow = y2SamplesPerRow;

            m_params.ScanRowsPerAcquisition = 1;
            // m_params.ScanRowsPerAcquisition = (int)Math.Ceiling(DEFAULT_ACQUISITION_INTERVAL / m_params.ScanTimePerLine) * 2;
            m_params.ScanPixelsPerAcquisition = m_params.ScanRowsPerAcquisition * m_params.ValidScanPixelsPerLine;
            m_params.IntervalPerAcquisition = m_params.ScanRowsPerAcquisition * m_params.ScanTimePerLine / 2;
        }

        private void GenerateDoubleTriggerWave()
        {
            if (m_sysConfig.GetAcqDevice() == ACQ_DEVICE.PMT)
            {
                // 如果是PMT采集，则DoSampleRate=AoSampleRate，DoSampleCountPerLine=AoSampleCountPerLine
                m_params.DoSampleRate = m_params.AoSampleRate;
                m_params.DoSampleCountPerLine = m_params.AoSampleCountPerLine;
                m_params.DigitalTriggerSamplesPerLine = Enumerable.Repeat<byte>(0, m_params.DoSampleCountPerLine).ToArray();

                int segStart = m_params.AoPreviousSampleCountPerLine;
                int segEnd = segStart + Params.DIGITAL_TRIGGER_PULSE_WIDTH;
                for (int i = segStart; i < segEnd; i++)
                {
                    m_params.DigitalTriggerSamplesPerLine[i] = 0x01;
                }

                segStart = m_params.AoPreviousSampleCountPerLine + m_params.AoValidSampleCountPerLine + m_params.AoPostSampleCountPerLine;
                segEnd = segStart + Params.DIGITAL_TRIGGER_PULSE_WIDTH;
                for (int i = segStart; i < segEnd; i++)
                {
                    m_params.DigitalTriggerSamplesPerLine[i] = 0x01;
                }
            }
            else
            {
                // 如果是APD采集，则DoSampleRate=PixelSampleRate*2，DoSampleCountPerLine按输出比例相应放大
                m_params.DoSampleRate = m_params.PixelSampleRate * 2;
                m_params.DoSampleCountPerLine = (int)(m_params.DoSampleRate / m_params.AoSampleRate * m_params.AoSampleCountPerLine);
                m_params.DigitalTriggerSamplesPerLine = Enumerable.Repeat<byte>(0, m_params.DoSampleCountPerLine).ToArray();

                int segStart = (int)(m_params.DoSampleRate / m_params.AoSampleRate * m_params.AoPreviousSampleCountPerLine);
                int segEnd = segStart + m_params.ValidScanPixelsPerLine * 2;
                for (int i = segStart; i < segEnd; i = i + 2)
                {
                    m_params.DigitalTriggerSamplesPerLine[i] = 0x01;
                }

                segStart = (int)(m_params.DoSampleRate / m_params.AoSampleRate * (
                    m_params.AoPreviousSampleCountPerLine + m_params.AoValidSampleCountPerLine + m_params.AoPostSampleCountPerLine));
                segEnd = segStart + m_params.ValidScanPixelsPerLine * 2;
                for (int i = segStart; i < segEnd; i = i + 2)
                {
                    m_params.DigitalTriggerSamplesPerLine[i] = 0x01;
                }
            }
        }

        private Params()
        {
            m_sysConfig = SysConfig.GetSysConfig();
            m_config = Config.GetConfig();
            int channelNum = m_config.GetChannelNum();
            m_colorMappingArr = new byte[channelNum][,];
            m_aiChannelIndex = new int[channelNum];
            for (int i = 0; i < channelNum; i++)
            {
                m_colorMappingArr[i] = new byte[256, 3];
                m_aiChannelIndex[i] = -1;
            }
        }

        #endregion

    }

}
