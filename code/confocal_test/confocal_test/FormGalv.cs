using log4net;
using NationalInstruments;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace confocal_test
{
    public enum GalvSystem
    {
        TwoGalv = 0,
        ThreeGalv
    };

    public enum ScanDirection
    {
        Single = 0,
        Double
    };

    public partial class FormGalv : Form
    {
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
        /************************************************************************************/
        private const double DEFAULT_GALV_RESPONSE_TIME = 200.0;    // us
        private const double DEFAULT_FIELD_SIZE = 200.0;            // um
        private const int DEFAULT_SCAN_PIXELS = 512;                // 
        private const double DEFAULT_PIXEL_TIME = 2.0;              // us
        private const double DEFAULT_CALIBRATION_VOLTAGE = 4.09855e-5;              // V
        private const double DEFAULT_CURVE_COFF = 10.0;             // %
        private const GalvSystem DEFAULT_GALV_SYSTEM = GalvSystem.TwoGalv;
        private const ScanDirection DEFAULT_SCAN_DIRECTION = ScanDirection.Single;
        /************************************************************************************/
        private Dictionary<int, string> scanPixelsDict;
        private Dictionary<GalvSystem, string> galvNumDict;
        private Dictionary<ScanDirection, string> scanDirectionDict;

        private double galvResponseTime;        // 振镜响应时间,us
        private double fieldSize;               // 视场大小,um
        private int scanPixels;                 // 扫描像素个数
        private double pixelTime;               // 像素时间,us
        private double calibrationVoltage;      // 校准[标定]电压,V
        private double curveCoff;               // 曲线系数,%
        private GalvSystem galvNum;             // 双镜 or 三镜
        private ScanDirection scanDirection;    // 扫描方向,单向 or 双向

        private double fps;                     // 帧率
        private double aoSampleRate;            // DAQ设备模拟输出频率（AO_SAMPLE_CLOCK）,Hz
        private double pixelSize;               // 像素大小,um
        private double voltagePerPixel;         // 相邻像素间的电压差,V
        private int previousSampleCountPerLine;     // 逐行前置样本数量
        private int validSampleCountPerLine;        // 逐行有效样本数量
        private int postSampleCountPerLine;         // 逐行后置样本数量
        private int sampleCountPerLine;        // 逐行所有样本数量
        private double[] samplesPerLine;       // 逐行所有样本

        private int aoNum = 3;
        private int sampleCountPerAO;               // 单AO通道单帧的样本数量
        //private int sampleCountPerFrame;          // 单帧所有AO通道的样本数量
        private double[] xWaves;                    // 单帧X振镜的所有样本
        private double[] y1Waves;                   // 单帧X振镜的所有样本
        private double[] y2Waves;                   // 单帧X振镜的所有样本

        private Task galvTask;

        /************************************************************************************/
        public FormGalv()
        {
            InitializeComponent();
        }

        private void FormGalv_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
            CalculateParams();
            UpdateControlers();
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender.Equals(cbxPixels) && cbxPixels.SelectedItem != null)
            {
                int past = scanPixels;
                scanPixels = ((KeyValuePair<int, string>)cbxPixels.SelectedItem).Key;
                if (past != scanPixels)
                {
                    Logger.Info(string.Format("change scan pixels from [{0}] to [{1}].", past, scanPixels));
                }
            }
            else if (sender.Equals(cbxGalvNum) && cbxGalvNum.SelectedItem != null)
            {
                GalvSystem past = galvNum;
                galvNum = ((KeyValuePair<GalvSystem, string>)cbxGalvNum.SelectedItem).Key;
                if (past != galvNum)
                {
                    Logger.Info(string.Format("change galv num from [{0}] to [{1}].", past, galvNum));
                }
            }
            else if (sender.Equals(cbxDirection) && cbxDirection.SelectedItem != null)
            {
                ScanDirection past = scanDirection;
                scanDirection = ((KeyValuePair<ScanDirection, string>)cbxDirection.SelectedItem).Key;
                if (scanDirection != past)
                {
                    Logger.Info(string.Format("change scan direction from [{0}] to [{1}].", past, scanDirection));
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            UpdateVariables();
            CalculateParams();
            UpdateControlers();
            Start();
            UpdateChart();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void InitVariables()
        {
            scanPixelsDict = new Dictionary<int, string>();
            scanPixelsDict.Add(256, "256x256");
            scanPixelsDict.Add(512, "512x512");
            scanPixelsDict.Add(1024, "1024x1024");
            scanPixelsDict.Add(2048, "2048x2048");
            scanPixelsDict.Add(4096, "4096x4096");

            galvNumDict = new Dictionary<GalvSystem, string>();
            galvNumDict.Add(GalvSystem.TwoGalv, "双振镜");
            galvNumDict.Add(GalvSystem.ThreeGalv, "三振镜");

            scanDirectionDict = new Dictionary<ScanDirection, string>();
            scanDirectionDict.Add(ScanDirection.Single, "单向");
            scanDirectionDict.Add(ScanDirection.Double, "双向");

            galvResponseTime = DEFAULT_GALV_RESPONSE_TIME;
            fieldSize = DEFAULT_FIELD_SIZE;
            scanPixels = DEFAULT_SCAN_PIXELS;
            pixelTime = DEFAULT_PIXEL_TIME;
            calibrationVoltage = DEFAULT_CALIBRATION_VOLTAGE;
            curveCoff = DEFAULT_CURVE_COFF;
            galvNum = DEFAULT_GALV_SYSTEM;
            scanDirection = DEFAULT_SCAN_DIRECTION;
        }

        private void InitControlers()
        {
            cbxPixels.DataSource = scanPixelsDict.ToList<KeyValuePair<int, string>>();
            cbxPixels.DisplayMember = "Value";
            cbxPixels.ValueMember = "Key";

            cbxGalvNum.DataSource = galvNumDict.ToList<KeyValuePair<GalvSystem, string>>();
            cbxGalvNum.DisplayMember = "Value";
            cbxGalvNum.ValueMember = "Key";

            cbxDirection.DataSource = scanDirectionDict.ToList<KeyValuePair<ScanDirection, string>>();
            cbxDirection.DisplayMember = "Value";
            cbxDirection.ValueMember = "Key";

            tbxResponseTime.Text = galvResponseTime.ToString();
            tbxFieldSize.Text = fieldSize.ToString();
            cbxPixels.SelectedIndex = cbxPixels.FindString(scanPixelsDict[scanPixels]);
            tbxPixelTime.Text = pixelTime.ToString();
            tbxCalibrationV.Text = calibrationVoltage.ToString();
            tbxCurveCoff.Text = curveCoff.ToString();
            cbxGalvNum.SelectedIndex = cbxGalvNum.FindString(galvNumDict[galvNum]);
            cbxDirection.SelectedIndex = cbxDirection.FindString(scanDirectionDict[scanDirection]);

            //cbxPixels.SelectedIndexChanged += SelectedIndexChanged;
            //cbxGalvNum.SelectedIndexChanged += SelectedIndexChanged;
            //cbxDirection.SelectedIndexChanged += SelectedIndexChanged;
            //cbxGalvNum.SelectedIndexChanged += SelectedIndexChanged;

        }

        private void UpdateVariables()
        {
            galvResponseTime = double.Parse(tbxResponseTime.Text);
            fieldSize = double.Parse(tbxFieldSize.Text);
            scanPixels = ((KeyValuePair<int, string>)cbxPixels.SelectedItem).Key;
            pixelTime = double.Parse(tbxPixelTime.Text);
            calibrationVoltage = double.Parse(tbxCalibrationV.Text);
            curveCoff = double.Parse(tbxCurveCoff.Text);
            galvNum = ((KeyValuePair<GalvSystem, string>)cbxGalvNum.SelectedItem).Key;
            scanDirection = ((KeyValuePair<ScanDirection, string>)cbxDirection.SelectedItem).Key;
        }

        private void UpdateControlers()
        {
            tbxAORate.Text = (aoSampleRate / 1000000).ToString();
            tbxPixelSize.Text = pixelSize.ToString();
            tbxVoltagePerPixel.Text = voltagePerPixel.ToString();
            tbxPrevSpCtPerLn.Text = previousSampleCountPerLine.ToString();
            tbxVaildSpCtPerLn.Text = validSampleCountPerLine.ToString();
            tbxPostSpCtPerLn.Text = postSampleCountPerLine.ToString();
            tbxTotalSpCtPerLn.Text = sampleCountPerLine.ToString();
            tbxFPS.Text = fps.ToString();
        }

        private void CalculateParams()
        {
            aoSampleRate = 1000000 / pixelTime;
            pixelSize = fieldSize / scanPixels;
            voltagePerPixel = calibrationVoltage / 5.0 * 1000 * pixelSize;

            double maximumVolatgeStep = 0.2;				    // 相邻像素间最大电压差,0.2V
            double w = pixelTime * scanPixels / 1000;			// 扫描一行有效样本需要的时间，单位ms
            double h = scanPixels * voltagePerPixel;            // 扫描一行有效样本振镜的电压变化幅值，单位V
            double h2 = scanPixels * voltagePerPixel;           // 扫描一帧Y1振镜的电压变化幅值，单位V
            double cosx = w / Math.Sqrt(w * w + h * h);         // 锯齿波余弦角度值
            double sinx = Math.Sqrt(1 - cosx * cosx);		    // 锯齿波正弦角度值
            double r = curveCoff / 100 * h / (1 - cosx);        // 前置扫描曲线的半径，曲线系数默认0.1

            int previousTotalSampleCount = (int)(2 * r * sinx / w * scanPixels);             // 从prev_x0到prev_xn总共的samples数量
            previousSampleCountPerLine = (int)Math.Ceiling(galvResponseTime / pixelTime) * 2;       // 前置扫描曲线的样本数量

            validSampleCountPerLine = scanPixels;

            int postFirstTotalSampleCount = (int)(2 * r * sinx / w * scanPixels);
            int postFirstSampleCount = (int)Math.Ceiling(galvResponseTime / pixelTime) * 2;
            int postSecondSampleCount = (int)Math.Ceiling(galvResponseTime / pixelTime);
            int postSecondMinimumSampleCount = (int)(h / maximumVolatgeStep);
            if (postSecondSampleCount < postSecondMinimumSampleCount)
            {
                postSecondSampleCount = postSecondMinimumSampleCount;
            }
            postSampleCountPerLine = postFirstSampleCount + postSecondSampleCount;

            sampleCountPerLine = previousSampleCountPerLine + validSampleCountPerLine + postSampleCountPerLine;

            fps = aoSampleRate / (sampleCountPerLine * scanPixels);

            double xn;

            // 计算逐行前置样本
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

            // 计算逐行有效样本
            double[] validSamplesPerLine = new double[validSampleCountPerLine];
            for (int i = 0; i < validSampleCountPerLine; i++)
            {
                validSamplesPerLine[i] = (i - (validSampleCountPerLine / 2)) * voltagePerPixel;
            }

            // 计算逐行后置样本
            double[] postSamplesPerLine = new double[postSampleCountPerLine];
            double post_xc = w / 2 + r * sinx;
            double post_yc = h / 2 - r * cosx;
            int postIndex;

            for (int i = 0; i < postFirstSampleCount / 2; i++)
            {
                xn = w / 2 + 2 * r * sinx / postFirstTotalSampleCount * i;
                postSamplesPerLine[i] = Math.Sqrt(r * r - (xn - post_xc) * (xn - post_xc)) + post_yc;
                postSamplesPerLine[postFirstSampleCount - 1 - i] = postSamplesPerLine[i];
            }
            postIndex = postFirstSampleCount;

            for (int i = 0; i < postSecondSampleCount; i++)
            {
                postSamplesPerLine[i + postIndex] = h / 2 - h / postSecondSampleCount * i;
            }
            //postIndex = postFirstSampleCount + postSecondSampleCount;

            samplesPerLine = new double[sampleCountPerLine];

            Array.Copy(previousSamplesPerLine, samplesPerLine, previousSampleCountPerLine);
            Array.Copy(validSamplesPerLine, 0, samplesPerLine, previousSampleCountPerLine, validSampleCountPerLine);
            Array.Copy(postSamplesPerLine, 0, samplesPerLine, previousSampleCountPerLine + validSampleCountPerLine, postSampleCountPerLine);

            sampleCountPerAO = sampleCountPerLine * scanPixels;
            //sampleCountPerFrame = sampleCountPerAO * aoNum;

            //waves = new AnalogWaveform<double>[3];
            xWaves = new double[sampleCountPerAO];
            y1Waves = new double[sampleCountPerAO];
            y2Waves = new double[sampleCountPerAO];

            int offset = -sampleCountPerLine;
            double y1Voltage, y2Voltage;
            //double[] y1Data = new double[sampleCountPerLine];
            //double[] y2Data = new double[sampleCountPerLine];

            for (int i = 0; i < scanPixels; i++)
            {
                offset += sampleCountPerLine;
                Array.Copy(samplesPerLine, 0, xWaves, offset, sampleCountPerLine);

                y1Voltage = voltagePerPixel * i - h2 / 2;
                y2Voltage = y1Voltage * 2;

                for (int j = 0; j < sampleCountPerLine; j++)
                {
                    y1Waves[offset + j] = y1Voltage;
                    y2Waves[offset + j] = y2Voltage;
                }

                //waves[0].Append(samplesPerLine);
                //waves[1].Append(y1Data);
                //waves[2].Append(y2Data);
            }
        }

        private void Start()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                // 创建模拟输出任务
                galvTask = new Task();
                // 创建模拟电压输出通道
                galvTask.AOChannels.CreateVoltageChannel("Dev1/ao0:2", "", -10.0, 10.0, AOVoltageUnits.Volts);
                // 验证任务
                galvTask.Control(TaskAction.Verify);

                double desiredFrequency = aoSampleRate / sampleCountPerAO;
                double samplesPerBuffer = sampleCountPerAO;
                double cyclesPerBuffer = 1.0;
                WaveGenerator wave = new WaveGenerator(
                    galvTask.Timing, desiredFrequency, samplesPerBuffer, cyclesPerBuffer, WaveformType.GalvWave, 2.0);

                // configure the sample clock with the calculated rate
                galvTask.Timing.ConfigureSampleClock("",
                    wave.ResultingSampleClockRate,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.ContinuousSamples, sampleCountPerAO);

                AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(galvTask.Stream);
                AnalogWaveform<double>[] waves = new AnalogWaveform<double>[3];
                waves[0] = AnalogWaveform<double>.FromArray1D(xWaves);
                waves[1] = AnalogWaveform<double>.FromArray1D(y1Waves);
                waves[2] = AnalogWaveform<double>.FromArray1D(y2Waves);

                //write data to buffer
                writer.WriteWaveform(false, waves);

                //start writing out data
                galvTask.Start();
            }
            catch (DaqException e)
            {
                Logger.Error(string.Format("daq exception [{0}].", e));
                MessageBox.Show(e.Message);
                galvTask.Dispose();
            }
            Cursor.Current = Cursors.Default;
        }

        private void Stop()
        {
            if (galvTask != null)
            {
                try
                {
                    galvTask.Stop();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    Logger.Error("stop galv task error [{0}].", e);
                }
                galvTask.Dispose();
                galvTask = null;
            }
        }

        private void UpdateChart()
        {
            ctGalv.Series[0].Points.Clear();
            ctGalv.Series[1].Points.Clear();
            ctGalv.Series[2].Points.Clear();

            int pointCount = sampleCountPerLine * 3;
            double aoSampleTime = 1.0 / aoSampleRate;
            double xValue;

            for (int i = 0; i < pointCount; i++)
            {
                xValue = aoSampleTime * i;
                ctGalv.Series[0].Points.AddXY(xValue, xWaves[i]);
                ctGalv.Series[1].Points.AddXY(xValue, y1Waves[i]);
                ctGalv.Series[2].Points.AddXY(xValue, y2Waves[i]);
            }
        }

    }
}
