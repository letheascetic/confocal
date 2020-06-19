using confocal_core;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace confocal_ui
{
    public partial class FormImage : DockContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScanTask m_scanTask;
        private Params m_params;
        private Config m_config;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public int FormId { get { return m_scanTask.TaskId; } }
        public string FormName { get { return m_scanTask.TaskName; } }

        ///////////////////////////////////////////////////////////////////////////////////////////
        public FormImage(ScanTask scanTask)
        {
            InitializeComponent();
            this.m_scanTask = scanTask;
        }

        public void ScanTaskCreated()
        {
            UpdateControlers();
            this.Activate();
        }

        public void ScanTaskStrated()
        {
            string status = m_scanTask.Scannning ? "进行中" : "暂停";
            this.Text = string.Format(m_scanTask.TaskName, " ", status);
            timer.Start();
        }

        public void ScanTaskStopped()
        {
            string status = m_scanTask.Scannning ? "进行中" : "暂停";
            this.Text = string.Format(m_scanTask.TaskName, " ", status);
            timer.Stop();
        }

        private void InitVariables()
        {
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
        }

        private void UpdateControlers()
        {
            this.Text = m_scanTask.TaskName;
            this.lbPixelSize.Text = string.Format("{0} um/px", m_params.PixelSize.ToString("F2"));
            this.lbScanPixels.Text = string.Format("{0} x {1} pixels", m_config.GetScanXPoints(), m_config.GetScanYPoints());
            this.lbBitDepth.Text = "16 bits";
            this.lbFps.Text = string.Format("{0} fps", m_params.Fps.ToString("F2"));
            this.lbFrame.Text = string.Format("NO. {0} frame", m_scanTask.GetScanInfo().CurrentFrame);
            this.lbTimeSpan.Text = string.Format("{0} seconds", m_scanTask.GetScanInfo().TimeSpan.ToString("F1"));
        }

        private void FormImage_Load(object sender, EventArgs e)
        {
            InitVariables();
        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }

    }
}
