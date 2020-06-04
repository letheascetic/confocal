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
    public partial class FormShowBox : DockContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Scheduler m_scheduler;
        private Config m_config;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public FormShowBox()
        {
            InitializeComponent();
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
            tbxAORate.Text = m_params.AoSampleRate.ToString();
            tbxPixelSize.Text = m_params.PixelSize.ToString();
            tbxVoltagePerPixel.Text = m_params.VoltagePerPixel.ToString();
            tbxPrevSpCtPerLn.Text = m_params.PreviousSampleCountPerLine.ToString();
            tbxVaildSpCtPerLn.Text = m_params.ValidSampleCountPerLine.ToString();
            tbxPostSpCtPerLn.Text = m_params.PostSampleCountPerLine.ToString();
            tbxTotalSpCtPerLn.Text = m_params.SampleCountPerLine.ToString();
            tbxFPS.Text = m_params.Fps.ToString();

            UpdateChart();
        }

        private void UpdateChart()
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Series[2].Points.Clear();
            chart.Series[3].Points.Clear();

            Params m_params = Params.GetParams();

            int pointCount = m_params.SampleCountPerLine * 3;
            double aoSampleTime = 1.0 / m_params.AoSampleRate;
            double xValue;
            int index, line;

            for (int i = 0; i < pointCount; i++)
            {
                line = i / m_params.SampleCountPerLine;
                index = i % m_params.SampleCountPerLine;
                xValue = aoSampleTime * i;
                chart.Series[0].Points.AddXY(xValue, m_params.XSamplesPerLine[index]);
                chart.Series[1].Points.AddXY(xValue, m_params.Y1SamplesPerRow[line]);
                chart.Series[2].Points.AddXY(xValue, m_params.Y2SamplesPerRow[line]);
                chart.Series[3].Points.AddXY(xValue, m_params.DigitalTriggerSamplesPerLine[index]);
            }

        }

        private void FormShowBox_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateControlers();
        }
    }
}
