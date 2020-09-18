using confocal_core;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace confocal_ui
{
    public partial class FormParas : DockContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Scheduler m_scheduler;
        private Config m_config;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public FormParas()
        {
            InitializeComponent();
        }

        public void ScanTaskStarted()
        {
            UpdateControlers();
        }

        public void ScanTaskConfigured()
        {
            UpdateControlers();
        }

        private void InitVariables()
        {
            m_config = Config.GetConfig();
            m_scheduler = Scheduler.CreateInstance();
        }

        private void InitControlers()
        {
            UpdateControlers();
        }

        private void UpdateControlers()
        {
            tbxResponseTime.Text = m_config.GetGalvResponseTime().ToString();
            tbxFieldSize.Text = m_config.GetScanFieldSize().ToString();
            tbxCalibrationV.Text = m_config.GetScanCalibrationVoltage().ToString();
            tbxCurveCoff.Text = m_config.GetScanCurveCoff().ToString();

            Params m_params = Params.GetParams();
            tbxAORate.Text = (m_params.AoSampleRate / 1e6).ToString();
            tbxPixelSize.Text = m_params.PixelSize.ToString();
            tbxVoltagePerPixel.Text = m_params.AoVoltagePerPixel.ToString();
            tbxAoSpCtPerLn.Text = string.Format("{0}+{1}*{2}+{3}={4}", 
                m_params.AoPreviousSampleCountPerLine, m_params.AoValidSampleCountPerLine, m_config.GetScanStrategy() == SCAN_STRATEGY.Z_BIDIRECTION ? 2:1,
                m_params.AoPostSampleCountPerLine, m_params.AoSampleCountPerLine);
            tbxSpCtPerFm.Text = m_params.AoSampleCountPerFrame.ToString();
            tbxScanTimePerLine.Text = m_params.ScanTimePerLine.ToString();
            tbxScanTimePerFm.Text = (1e3 / m_params.Fps).ToString();
            tbxFPS.Text = m_params.Fps.ToString();
            tbxPixelRate.Text = (m_params.PixelSampleRate / 1e6).ToString();
            tbxValidSpCtPerLn.Text = m_params.ValidScanPixelsPerLine.ToString();

            UpdateChart();
        }

        private void UpdateChart()
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Series[2].Points.Clear();
            chart.Series[3].Points.Clear();

            Params m_params = Params.GetParams();
            SCAN_STRATEGY strategy = Config.GetConfig().GetScanStrategy();
            SCAN_MIRROR_NUM mirror = Config.GetConfig().GetScanMirrorNum();

            int aoPointCount = m_params.AoSampleCountPerLine * 2;
            double aoSampleTime = 1e3 / m_params.AoSampleRate;

            double[] aoXValues = new double[aoPointCount];
            double[] xGalvSamples = new double[aoPointCount];
            double[] y1GalvSamples, y2GalvSamples;

            if (m_config.GetScanStrategy() == SCAN_STRATEGY.Z_UNIDIRECTION)
            {
                y1GalvSamples = Enumerable.Concat(
                    Enumerable.Repeat<double>(m_params.AoY1SamplesPerRow[0], m_params.AoSampleCountPerLine),
                    Enumerable.Repeat<double>(m_params.AoY1SamplesPerRow[1], m_params.AoSampleCountPerLine)).ToArray();
                y2GalvSamples = Enumerable.Concat(
                    Enumerable.Repeat<double>(m_params.AoY2SamplesPerRow[0], m_params.AoSampleCountPerLine),
                    Enumerable.Repeat<double>(m_params.AoY2SamplesPerRow[1], m_params.AoSampleCountPerLine)).ToArray();
            }
            else
            {
                y1GalvSamples = Enumerable.Concat(
                    Enumerable.Concat(
                        Enumerable.Repeat<double>(m_params.AoY1SamplesPerRow[0], m_params.AoSampleCountPerLine / 2),
                        Enumerable.Repeat<double>(m_params.AoY1SamplesPerRow[1], m_params.AoSampleCountPerLine / 2)),
                    Enumerable.Concat(
                        Enumerable.Repeat<double>(m_params.AoY1SamplesPerRow[2], m_params.AoSampleCountPerLine / 2),
                        Enumerable.Repeat<double>(m_params.AoY1SamplesPerRow[3], m_params.AoSampleCountPerLine / 2))
                    ).ToArray();

                y2GalvSamples = Enumerable.Concat(
                    Enumerable.Concat(
                        Enumerable.Repeat<double>(m_params.AoY2SamplesPerRow[0], m_params.AoSampleCountPerLine / 2),
                        Enumerable.Repeat<double>(m_params.AoY2SamplesPerRow[1], m_params.AoSampleCountPerLine / 2)),
                    Enumerable.Concat(
                        Enumerable.Repeat<double>(m_params.AoY2SamplesPerRow[2], m_params.AoSampleCountPerLine / 2),
                        Enumerable.Repeat<double>(m_params.AoY2SamplesPerRow[3], m_params.AoSampleCountPerLine / 2))
                    ).ToArray();
            }
            
            aoXValues[0] = 0;
            for (int i = 1; i < aoPointCount; i++)
            {
                aoXValues[i] = aoXValues[i - 1] + aoSampleTime;
            }

            Array.Copy(m_params.AoXSamplesPerLine, 0, xGalvSamples, 0, m_params.AoSampleCountPerLine);
            Array.Copy(m_params.AoXSamplesPerLine, 0, xGalvSamples, m_params.AoSampleCountPerLine, m_params.AoSampleCountPerLine);

            chart.Series[0].Points.DataBindXY(aoXValues, xGalvSamples);
            chart.Series[1].Points.DataBindXY(aoXValues, y1GalvSamples);
            chart.Series[2].Points.DataBindXY(aoXValues, y2GalvSamples);

            chart.Series[2].IsVisibleInLegend = Config.GetConfig().GetScanMirrorNum() == SCAN_MIRROR_NUM.THREEE ? true : false;

            int doPointCount = m_params.DoSampleCountPerLine * 2;
            double doSampleTime = 1e3 / m_params.DoSampleRate;

            double[] doXValues = new double[doPointCount];
            double[] doSamples = new double[doPointCount];

            doXValues[0] = 0;
            for (int i = 1; i < doPointCount; i++)
            {
                doXValues[i] = doXValues[i - 1] + doSampleTime;
            }

            Array.Copy(m_params.DigitalTriggerSamplesPerLine, 0, doSamples, 0, m_params.DoSampleCountPerLine);
            Array.Copy(m_params.DigitalTriggerSamplesPerLine, 0, doSamples, m_params.DoSampleCountPerLine, m_params.DoSampleCountPerLine);

            chart.Series[3].Points.DataBindXY(doXValues, doSamples);

            //double xValue;
            //int index, line;

            //for (int i = 0; i < pointCount; i++)
            //{
            //    line = i / m_params.AoSampleCountPerLine;
            //    index = i % m_params.AoSampleCountPerLine;
            //    xValue = aoSampleTime * i;

            //    chart.Series[0].Points.AddXY(xValue, m_params.AoXSamplesPerLine[index]);

            //    if (strategy == SCAN_STRATEGY.Z_UNIDIRECTION)
            //    {
            //        chart.Series[1].Points.AddXY(xValue, m_params.AoY1SamplesPerRow[line]);
            //        chart.Series[2].Points.AddXY(xValue, m_params.AoY2SamplesPerRow[line]);
            //    }
            //    else
            //    {
            //        if (index < m_params.AoPreviousSampleCountPerLine + m_params.AoValidSampleCountPerLine)
            //        {
            //            chart.Series[1].Points.AddXY(xValue, m_params.AoY1SamplesPerRow[line]);
            //            chart.Series[2].Points.AddXY(xValue, m_params.AoY2SamplesPerRow[line]);
            //        }
            //        else
            //        {
            //            chart.Series[1].Points.AddXY(xValue, m_params.AoY1SamplesPerRow[line] + m_params.AoVoltagePerPixel);
            //            chart.Series[2].Points.AddXY(xValue, m_params.AoY2SamplesPerRow[line] + m_params.AoVoltagePerPixel * 2);
            //        }
            //    }
            //    chart.Series[3].Points.AddXY(xValue, m_params.DigitalTriggerSamplesPerLine[index*2]);
            //    chart.Series[3].Points.AddXY(xValue + aoSampleTime, m_params.DigitalTriggerSamplesPerLine[index*2+1]);
            //}

            //chart.Series[2].IsVisibleInLegend = Config.GetConfig().GetScanMirrorNum() == SCAN_MIRROR_NUM.THREEE ? true : false;

        }

        private void FormShowBox_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
        }
    }
}
