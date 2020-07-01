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
        private bool m_firstDisplay;
        private long m_currentFrame;
        private KeyValuePair<TabPage, PictureBox>[] m_pagePicPairs;
        private KeyValuePair<TabPage, int>[] m_pageIndexPairs;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public int FormId { get { return m_scanTask.TaskId; } }
        public string FormName { get { return m_scanTask.TaskName; } }
        ///////////////////////////////////////////////////////////////////////////////////////////
        public FormImage(ScanTask scanTask)
        {
            InitializeComponent();
            LoadScanTask(scanTask);
        }

        public void LoadScanTask(ScanTask scanTask)
        {
            m_scanTask = scanTask;
        }

        public void ScanTaskCreated()
        {
            Logger.Info(string.Format("FormImage scan task[{0}|{1}] created.", m_scanTask.TaskId, m_scanTask.TaskName));
            UpdateControlers();
            this.Activate();
        }

        public void ScanTaskStrated()
        {
            Logger.Info(string.Format("FormImage scan task[{0}|{1}] started.", m_scanTask.TaskId, m_scanTask.TaskName));
            string status = m_scanTask.Scannning ? "进行中" : "暂停";
            this.Text = string.Format(m_scanTask.TaskName, " ", status);
            timer.Start();
        }

        public void ScanTaskStopped()
        {
            Logger.Info(string.Format("FormImage scan task[{0}|{1}] stopped.", m_scanTask.TaskId, m_scanTask.TaskName));
            string status = m_scanTask.Scannning ? "进行中" : "暂停";
            this.Text = string.Format(m_scanTask.TaskName, " ", status);
            timer.Stop();
        }

        private void InitVariables()
        {
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_currentFrame = -1;
            m_firstDisplay = true;
            m_pagePicPairs = new KeyValuePair<TabPage, PictureBox>[] {
                new KeyValuePair<TabPage, PictureBox>(tpgAll, pbxAll),
                new KeyValuePair<TabPage, PictureBox>(tpg405, pbx405),
                new KeyValuePair<TabPage, PictureBox>(tpg488, pbx488),
                new KeyValuePair<TabPage, PictureBox>(tpg561, pbx561),
                new KeyValuePair<TabPage, PictureBox>(tpg640, pbx640)
            };
            m_pageIndexPairs = new KeyValuePair<TabPage, int>[] {
                new KeyValuePair<TabPage, int>(tpgAll, -1),
                new KeyValuePair<TabPage, int>(tpg405, (int)CHAN_ID.WAVELENGTH_405_NM),
                new KeyValuePair<TabPage, int>(tpg488, (int)CHAN_ID.WAVELENGTH_488_NM),
                new KeyValuePair<TabPage, int>(tpg561, (int)CHAN_ID.WAVELENGTH_561_NM),
                new KeyValuePair<TabPage, int>(tpg640, (int)CHAN_ID.WAVELENGTH_640_NM)
            }; 
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

            this.m_currentFrame = -1;
            this.m_firstDisplay = true;

            this.tpgAll.Parent = m_config.GetActivatedChannelNum() <= 1 ? null : this.tabControl;
            this.tpg405.Parent = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM) == LASER_CHAN_SWITCH.OFF ? null : this.tabControl;
            this.tpg488.Parent = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM) == LASER_CHAN_SWITCH.OFF ? null : this.tabControl;
            this.tpg561.Parent = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM) == LASER_CHAN_SWITCH.OFF ? null : this.tabControl;
            this.tpg640.Parent = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM) == LASER_CHAN_SWITCH.OFF ? null : this.tabControl;

            byte[] data = new byte[m_config.GetScanXPoints() * m_config.GetScanYPoints()];
            Bitmap bmp = CImage.CreateBitmap(data, m_config.GetScanXPoints(), m_config.GetScanYPoints());
            this.pbx405.Image = bmp;
            this.pbx488.Image = bmp;
            this.pbx561.Image = bmp;
            this.pbx640.Image = bmp;
        }

        private PictureBox GetMappingPictureBox(TabPage tabPage)
        {
            foreach (KeyValuePair<TabPage, PictureBox> keyValue in m_pagePicPairs)
            {
                if (object.ReferenceEquals(keyValue.Key, tabPage))
                {
                    return keyValue.Value;
                }
            }
            return null;
        }

        private int GetMappingIndex(TabPage tabPage)
        {
            foreach (KeyValuePair<TabPage, int> keyValue in m_pageIndexPairs)
            {
                if (object.ReferenceEquals(keyValue.Key, tabPage))
                {
                    return keyValue.Value;
                }
            }
            return -1;
        }

        private void DisplayImage(TabPage tabPage)
        {
            int index = GetMappingIndex(tabControl.SelectedTab);
            if (index >= 0)
            {
                Logger.Info(string.Format("convert frame[{0}] to image.", m_currentFrame));
                PictureBox pbx = GetMappingPictureBox(tabControl.SelectedTab);
                DisplayData displayData = m_scanTask.GetScanData().ImageData;
                pbx.Image = CImage.CreateBitmap(displayData.Data[index], m_config.GetScanXPoints(), m_config.GetScanYPoints());
            }
        }

        private void DisplayImage()
        {
            DisplayData displayData = m_scanTask.GetScanData().ImageData;
            if (m_currentFrame != displayData.Frame)
            {
                m_currentFrame = displayData.Frame;
                if (m_firstDisplay)
                {
                    m_firstDisplay = false;
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        DisplayImage(tabPage);
                    }
                }
                else
                {
                    DisplayImage(tabControl.SelectedTab);
                }
            }
        }

        private void FormImage_Load(object sender, EventArgs e)
        {
            InitVariables();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DisplayImage();
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            DisplayImage();
        }
    }
}
