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
        private Dictionary<CHAN_ID, string> m_activatedChannelDict;

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
            UpdateVariables();
            UpdateControlers();
            m_timer.Start();
        }

        public void ScanTaskStopped()
        {
            Logger.Info(string.Format("FormHistogram scan task[{0}|{1}] stopped.", m_scanTask.TaskId, m_scanTask.TaskName));
            m_timer.Stop();
        }

        public void SelectedChannelChanged(CHAN_ID id)
        {
            m_activatedChannelDict.TryGetValue(id, out string value);
            cbxChannel.SelectedIndex = cbxChannel.FindString(value);
            m_selectedChannelIndex = (int)id;
            UpdateHistogram();
        }

        private void InitVariables()
        {
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_scheduler = Scheduler.CreateInstance();
            m_activatedChannelDict = new Dictionary<CHAN_ID, string>();
            m_selectedChannelIndex = -1;
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

        public void ChannelGammaChanged(CHAN_ID id, double gamma)
        {
            UpdateHistogram();
        }

        private void m_timer_Tick(object sender, EventArgs e)
        {
            UpdateHistogram();
        }

        private void UpdateControlers()
        {
            this.cbxChannel.BeginUpdate();
            this.cbxChannel.ComboBox.DataSource = null;
            this.cbxChannel.Items.Clear();
            this.cbxChannel.ComboBox.DataSource = m_activatedChannelDict.ToList<KeyValuePair<CHAN_ID, string>>();
            this.cbxChannel.ComboBox.DisplayMember = "Value";
            this.cbxChannel.ComboBox.ValueMember = "Key";
            this.cbxChannel.SelectedIndex = 0;
            this.cbxChannel.EndUpdate();
        }

        private void UpdateVariables()
        {
            m_activatedChannelDict.Clear();
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_activatedChannelDict.Add(CHAN_ID.WAVELENGTH_405_NM, "405nm");
            }
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_activatedChannelDict.Add( CHAN_ID.WAVELENGTH_488_NM, "488nm");
            }
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_activatedChannelDict.Add(CHAN_ID.WAVELENGTH_561_NM, "561nm");
            }
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_activatedChannelDict.Add(CHAN_ID.WAVELENGTH_640_NM, "640nm");
            }
        }

        private void UpdateHistogram()
        {
            histogramBox.ClearHistogram();
            histogramBox.GenerateHistograms(m_scanTask.GetScanData().ScanImage.GrayMat[m_selectedChannelIndex], 256);
            histogramBox.Refresh();
        }

        private void cbxChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxChannel.SelectedIndex < 0)
            {
                return;
            }

            CHAN_ID id = ((KeyValuePair<CHAN_ID, string>)cbxChannel.SelectedItem).Key;
            
            m_selectedChannelIndex = (int)id;
            gtbGamma.Value = (int)(100 * Math.Log(m_config.GetChannelGamma(id)) / Math.Log(2));
            UpdateHistogram();
        }

        private void gtbGamma_ValueChanged(object sender, EventArgs e)
        {
            if (m_selectedChannelIndex < 0)
            {
                return;
            }

            double gamma = Math.Pow(2, gtbGamma.Value / 100.0);
            m_scheduler.ChangeChannelGamma(m_scanTask, (CHAN_ID)m_selectedChannelIndex, gamma);
        }
    }
}
