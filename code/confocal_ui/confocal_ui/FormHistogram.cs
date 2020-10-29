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
    public partial class FormHistogram : DockContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScanTask m_scanTask;
        private Params m_params;
        private Config m_config;
        private Scheduler m_scheduler;
        private int m_selectedChannelIndex;

        public FormHistogram()
        {
            InitializeComponent();
        }

        public void ScanTaskCreated(ScanTask scanTask)
        {
            m_scanTask = scanTask;
            Logger.Info(string.Format("FormHistogram scan task[{0}|{1}] created.", m_scanTask.TaskId, m_scanTask.TaskName));
        }

        public void ScanTaskStrated()
        {
            Logger.Info(string.Format("FormHistogram scan task[{0}|{1}] started.", m_scanTask.TaskId, m_scanTask.TaskName));
            m_timer.Start();
        }

        public void ScanTaskStopped()
        {
            Logger.Info(string.Format("FormHistogram scan task[{0}|{1}] stopped.", m_scanTask.TaskId, m_scanTask.TaskName));
            m_timer.Stop();
        }

        public void SelectedChannelChanged(CHAN_ID id)
        {
            m_selectedChannelIndex = (int)id;
        }

        private void InitVariables()
        {
            m_selectedChannelIndex = -1;
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_scheduler = Scheduler.CreateInstance();
        }

        private void InitControlers()
        {

        }

        private void FormHistogram_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
        }

        public void ChannelOffsetChanged(CHAN_ID id, int offset)
        {
            UpdateHistogram();
        }

        private void m_timer_Tick(object sender, EventArgs e)
        {
            UpdateHistogram();
        }

        private void UpdateHistogram()
        {
            histogramBox.ClearHistogram();
            histogramBox.GenerateHistograms(m_scanTask.GetScanData().ScanImage.GrayMat[m_selectedChannelIndex], 256);
            histogramBox.Refresh();
        }

    }
}
