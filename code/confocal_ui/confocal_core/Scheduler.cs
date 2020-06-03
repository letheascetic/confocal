using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace confocal_core
{
    /// <summary>
    /// 根据扫描参数配置，计算得到的相应参数，如AO输出速率、AO前置/后置样本数量、fps等
    /// </summary>
    public class Params
    {
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
        /// 单行输出样本数量
        /// </summary>
        public int SampleCountPerLine { get; set; }
        /// <summary>
        /// 单帧输出样本数量
        /// </summary>
        public int SampleCountPerFrame { get; set; }
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
    }

    /// <summary>
    /// 扫描波形生成器
    /// </summary>
    public class WaveGenerator
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        public double[] XWave { get; set; }
        public double[] Y1Wave { get; set; }
        public double[] Y2Wave { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////////////

        public WaveGenerator()
        {
            
        }

        /// <summary>
        /// 生成扫描控制波形
        /// </summary>
        /// <param name="m_params"></param>
        public void Generate(Params m_params)
        {
            if (XWave == null || XWave.Length != m_params.SampleCountPerFrame)
            {
                XWave = new double[m_params.SampleCountPerFrame];
            }
            if (Y1Wave == null || Y1Wave.Length != m_params.SampleCountPerFrame)
            {
                Y1Wave = new double[m_params.SampleCountPerFrame];
            }
            if (Y2Wave == null || Y2Wave.Length != m_params.SampleCountPerFrame)
            {
                Y2Wave = new double[m_params.SampleCountPerFrame];
            }

            int offset = -m_params.SampleCountPerLine;
            for (int i = 0; i < Config.GetConfig().GetScanYPoints(); i++)
            {
                offset += m_params.SampleCountPerLine;
                Array.Copy(m_params.XSamplesPerLine, 0, XWave, offset, m_params.SampleCountPerLine);

                for (int j = 0; j < m_params.SampleCountPerLine; j++)
                {
                    
                    Y1Wave[offset + j] = m_params.Y1SamplesPerRow[i];
                    Y2Wave[offset + j] = m_params.Y2SamplesPerRow[i];
                }
            }
        }

    }

    /// <summary>
    /// 调度器，单例模式
    /// </summary>
    public class Scheduler
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static Scheduler pScheduler = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Params m_params;
        private Config m_config;
        private WaveGenerator m_waveG;
        private Task m_aoTask;
        //private Task m_aiTask;
        //private Task m_doTask;
        ///////////////////////////////////////////////////////////////////////////////////////////

        #region public apis

        public static Scheduler CreateInstance()
        {
            if (pScheduler == null)
            {
                lock (locker)
                {
                    if (pScheduler == null)
                    {
                        pScheduler = new Scheduler();
                    }
                }
            }
            return pScheduler;
        }

        public void CalculateParams()
        {
            double maximumVolatgeStep = 0.2;

            double aoSampleRate = 1e6 / m_config.GetScanPixelTime();
            double pixelSize = m_config.GetScanFieldSize() / m_config.GetScanXPoints();
            double voltagePerPixel = m_config.GetScanCalibrationVoltage() / 5.0 * 1000 * pixelSize;

            double w = m_config.GetScanPixelTime() * m_config.GetScanXPoints();
            double h = voltagePerPixel * m_config.GetScanXPoints();
            double h2 = voltagePerPixel * m_config.GetScanYPoints();
            double cosx = w / Math.Sqrt(w * w + h * h);
            double sinx = Math.Sqrt(1 - cosx * cosx);
            double r = m_config.GetScanCurveCoff() / 100 * h / (1 - cosx);

            // 从prev_x0到prev_xn总共的samples数量
            int previousTotalSampleCount = (int)(2 * r * sinx / w * m_config.GetScanXPoints());             
            // 计算每行前置输出样本数量
            int previousSampleCountPerLine = (int)Math.Ceiling(m_config.GetGalvResponseTime() / m_config.GetScanPixelTime()) * 2;

            // 计算每行有效输出样本数量
            int validSampleCountPerLine = m_config.GetScanXPoints();

            int postFirstTotalSampleCount = (int)(2 * r * sinx / w * m_config.GetScanXPoints());
            // 计算每行后置输出样本数量
            int postFirstSampleCount = (int)Math.Ceiling(m_config.GetGalvResponseTime() / m_config.GetScanPixelTime()) * 2;
            int postSecondSampleCount = (int)Math.Ceiling(m_config.GetGalvResponseTime() / m_config.GetScanPixelTime());
            int postSecondMinimumSampleCount = (int)(h / maximumVolatgeStep);
            if (postSecondSampleCount < postSecondMinimumSampleCount)
            {
                postSecondSampleCount = postSecondMinimumSampleCount;
            }
            int postSampleCountPerLine = postFirstSampleCount + postSecondSampleCount;

            // 每行输出样本数量
            int sampleCountPerLine = previousSampleCountPerLine + validSampleCountPerLine + postSampleCountPerLine;

            // 计算帧率
            double fps = aoSampleRate / (sampleCountPerLine * m_config.GetScanYPoints());

            double xn;
            // 计算逐行前置输出样本
            double[] previousSamplesPerLine = new double[previousSampleCountPerLine];
            double prev_xc = -(w / 2 + r * sinx);       // 前置扫描曲线圆心的x坐标值
            double prev_yc = -(h / 2 - r * cosx);       // 前置扫描曲线圆心的y坐标值
            double prev_x0 = -(w / 2 + 2 * r * sinx);
            double prev_y0 = -h / 2;

            for (int i = 0; i < previousSampleCountPerLine / 2; i++)
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

            for (int i = 0; i < postFirstSampleCount / 2; i++)
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

            int sampleCountPerFrame = sampleCountPerLine * m_config.GetScanYPoints();

            double[] y1SamplesPerRow = new double[m_config.GetScanYPoints()];
            double[] y2SamplesPerRow = new double[m_config.GetScanYPoints()];
            double yn;
            for (int i = 0; i < m_config.GetScanYPoints(); i++)
            {
                yn = voltagePerPixel * i - h2 / 2;
                y1SamplesPerRow[i] = yn;
                y2SamplesPerRow[i] = yn * 2;
            }

            m_params.Fps = fps;
            m_params.AoSampleRate = aoSampleRate;
            m_params.PixelSize = pixelSize;
            m_params.VoltagePerPixel = voltagePerPixel;
            m_params.PreviousSampleCountPerLine = previousSampleCountPerLine;
            m_params.ValidSampleCountPerLine = validSampleCountPerLine;
            m_params.PostSampleCountPerLine = postSampleCountPerLine;
            m_params.SampleCountPerLine = sampleCountPerLine;

            m_params.ValidSampleScanTimePerLine = w;
            m_params.ValidSampleVoltagePerLine = h;
            m_params.VoltagePerRow = h2;

            m_params.SampleCountPerFrame = sampleCountPerFrame;
            m_params.XSamplesPerLine = xSamplesPerLine;
            m_params.Y1SamplesPerRow = y1SamplesPerRow;
            m_params.Y2SamplesPerRow = y2SamplesPerRow;

        }

        public void GenerateWaves()
        {
            m_waveG.Generate(m_params);
        }

        public void StartTask()
        {
            CalculateParams();
            GenerateWaves();
            ConfigAoTask();
            StartNiTask();
        }

        public void StopTask()
        {
            if (m_aoTask != null)
            {
                try
                {
                    m_aoTask.Stop();
                    m_aoTask.Dispose();
                    m_aoTask = null;
                }
                catch (DaqException e)
                {
                    MessageBox.Show(e.Message);
                    Logger.Error("stop galv task error [{0}].", e);
                }
            }
        }

        #endregion

        #region private apis    

        private Scheduler()
        {
            m_params = new Params();
            m_config = Config.GetConfig();
            m_waveG = new WaveGenerator();
        }

        private void ConfigAoTask()
        {
            try
            {
                // 创建模拟输出任务
                m_aoTask = new Task();
                // 创建模拟电压输出通道
                m_aoTask.AOChannels.CreateVoltageChannel("Dev1/ao0:2", "", -10.0, 10.0, AOVoltageUnits.Volts);
                // 验证任务
                m_aoTask.Control(TaskAction.Verify);

                // 配置时钟
                m_aoTask.Timing.SampleClockRate = m_params.AoSampleRate;
                m_aoTask.Timing.ConfigureSampleClock("",
                    m_aoTask.Timing.SampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples,
                    m_params.SampleCountPerFrame);

                // 写入波形
                AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(m_aoTask.Stream);
                AnalogWaveform<double>[] waves = new AnalogWaveform<double>[3];
                waves[0] = AnalogWaveform<double>.FromArray1D(m_waveG.XWave);
                waves[1] = AnalogWaveform<double>.FromArray1D(m_waveG.Y1Wave);
                waves[2] = AnalogWaveform<double>.FromArray1D(m_waveG.Y2Wave);
                writer.WriteWaveform(false, waves);
            }
            catch(Exception e)
            {
                Logger.Error(string.Format("daq exception [{0}].", e));
                MessageBox.Show(e.Message);
                m_aoTask.Dispose();
                m_aoTask = null;
            }
        }

        private void StartNiTask()
        {
            try
            {
                m_aoTask.Start();
            }
            catch (DaqException e)
            {
                Logger.Error(string.Format("daq exception [{0}].", e));
                MessageBox.Show(e.Message);
                m_aoTask.Dispose();
                m_aoTask = null;
            }
        }

        #endregion
    }
}
