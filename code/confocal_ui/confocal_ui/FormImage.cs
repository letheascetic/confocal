﻿using confocal_core;
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
        private Bitmap[] m_bitmapArr;
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
            UpdateVariables();
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

        public void ActivatedChannelChanged()
        {
            Logger.Info(string.Format("FormImage scan task[{0}|{1}] activated channel channged.", m_scanTask.TaskId, m_scanTask.TaskName));
            UpdateTabPages();
        }

        private void InitVariables()
        {
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
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

            int channelNum = m_config.GetChannelNum();
            m_bitmapArr = new Bitmap[channelNum];
            
            for (int i = 0; i < channelNum; i++)
            {
                m_bitmapArr[i] = new Bitmap(m_config.GetScanXPoints(), m_config.GetScanYPoints(), PixelFormat.Format24bppRgb);
            }
        }

        private void InitControlers()
        {
            pbxAllu.Parent = pbxAll;
            pbxAllu.Location = pbxAll.Location;
            pbxAllu.Size = pbxAll.Size;

            pbx405u.Parent = pbx405;
            pbx405u.Location = pbx405.Location;
            pbx405u.Size = pbx405.Size;

            pbx488u.Parent = pbx488;
            pbx488u.Location = pbx488.Location;
            pbx488u.Size = pbx488.Size;

            pbx561u.Parent = pbx561;
            pbx561u.Location = pbx561.Location;
            pbx561u.Size = pbx561.Size;

            pbx640u.Parent = pbx640;
            pbx640u.Location = pbx640.Location;
            pbx640u.Size = pbx640.Size;
        }

        private void UpdateVariables()
        {
            for (int i = 0; i < m_config.GetChannelNum(); i++)
            {
                m_bitmapArr[i] = new Bitmap(m_config.GetScanXPoints(), m_config.GetScanYPoints(), PixelFormat.Format24bppRgb);
            }
        }

        private void UpdateControlers()
        {
            this.Text = m_scanTask.TaskName;
            this.lbPixelSize.Text = string.Format("{0} um/px", m_params.PixelSize.ToString("F2"));
            this.lbScanPixels.Text = string.Format("{0} x {1} pixels", m_config.GetScanXPoints(), m_config.GetScanYPoints());
            this.lbBitDepth.Text = "16 bits";
            this.lbFps.Text = string.Format("{0} fps", m_params.Fps.ToString("F2"));
            
            UpdateRTControlers();
            UpdateTabPages();
        }

        private void UpdateRTControlers()
        {
            this.lbFrame.Text = string.Format("NO. {0} frame", m_scanTask.GetScanInfo().CurrentFrame);
            this.lbTimeSpan.Text = string.Format("{0} seconds", m_scanTask.GetScanInfo().TimeSpan.ToString("F1"));
        }

        private void UpdateTabPages()
        {
            this.tpgAll.Parent = null;
            this.tpg405.Parent = null;
            this.tpg488.Parent = null;
            this.tpg561.Parent = null;
            this.tpg640.Parent = null;

            this.tpgAll.Parent = m_config.GetActivatedChannelNum() <= 1 ? null : this.tabControl;
            this.tpg405.Parent = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM) == LASER_CHAN_SWITCH.OFF ? null : this.tabControl;
            this.tpg488.Parent = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM) == LASER_CHAN_SWITCH.OFF ? null : this.tabControl;
            this.tpg561.Parent = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM) == LASER_CHAN_SWITCH.OFF ? null : this.tabControl;
            this.tpg640.Parent = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM) == LASER_CHAN_SWITCH.OFF ? null : this.tabControl;
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
                PictureBox pbx = GetMappingPictureBox(tabControl.SelectedTab);
                pbx.Image = m_scanTask.GetScanData().ScanImage.GetDisplayImage(index, ref m_bitmapArr[index]);
            }
        }

        private void DisplayImage()
        {
            ImageData imageData = m_scanTask.GetScanData().ScanImage;
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                DisplayImage(tabPage);
            }
        }

        private void FormImage_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //DisplayImage();
            UpdateRTControlers();
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            DisplayImage();
        }

        private void btnDisplayCenter_Click(object sender, EventArgs e)
        {
            PictureBox pbx = GetMappingPictureBox(tabControl.SelectedTab);
            if (pbx != null)
            {
                pbx.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        private void btnDisplaytNormal_Click(object sender, EventArgs e)
        {
            PictureBox pbx = GetMappingPictureBox(tabControl.SelectedTab);
            if (pbx != null)
            {
                pbx.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void btnDisplayStretch_Click(object sender, EventArgs e)
        {
            PictureBox pbx = GetMappingPictureBox(tabControl.SelectedTab);
            if (pbx != null)
            {
                pbx.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnDisplayZoom_Click(object sender, EventArgs e)
        {
            PictureBox pbx = GetMappingPictureBox(tabControl.SelectedTab);
            if (pbx != null)
            {
                pbx.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PictureBox pbx = GetMappingPictureBox(tabControl.SelectedTab);
            if (pbx != null)
            {
                Bitmap bmp = (Bitmap)pbx.Image.Clone();
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Jpeg Files (*.)|*.Png";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!dialog.FileName.EndsWith(".Png"))
                    {
                        MessageBox.Show("文件名格式有误!");
                        return;
                    }
                    string filename = dialog.FileName;
                    bmp.Save(filename, ImageFormat.Png);
                }
            }
        }
    }
}
