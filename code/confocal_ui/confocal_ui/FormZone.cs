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
    public partial class FormZone : DockContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScanTask m_scanTask;
        private Params m_params;
        private Config m_config;
        private Scheduler m_scheduler;
        private Dictionary<int, string> scanPixelsDict;
        private Bitmap m_bitmap;
        private int m_selectedChannelIndex;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public FormZone()
        {
            InitializeComponent();
        }

        private void InitVariables()
        {
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_scheduler = Scheduler.CreateInstance();

            m_bitmap = new Bitmap(m_config.GetScanXPoints(), m_config.GetScanYPoints(), PixelFormat.Format24bppRgb);

            scanPixelsDict = new Dictionary<int, string>();
            scanPixelsDict.Add(64, "64x64");
            scanPixelsDict.Add(128, "128x128");
            scanPixelsDict.Add(256, "256x256");
            scanPixelsDict.Add(512, "512x512");
            scanPixelsDict.Add(1024, "1024x1024");
            scanPixelsDict.Add(2048, "2048x2048");
            scanPixelsDict.Add(4096, "4096x4096");
        }

        private void InitControlers()
        {
            cbxScanPixels.DataSource = scanPixelsDict.ToList<KeyValuePair<int, string>>();
            cbxScanPixels.DisplayMember = "Value";
            cbxScanPixels.ValueMember = "Key";
            cbxScanPixels.SelectedIndex = cbxScanPixels.FindString(scanPixelsDict[m_config.GetScanXPoints()]);
            // cbxScanPixels.SelectedIndexChanged += cbxScanPixels_SelectedIndexChanged;

            pbxImage.Image = m_bitmap;

            pbxZone.Parent = pbxImage;
            pbxZone.Size = pbxImage.Size;
            pbxZone.Location = pbxImage.Location;
            pbxZone.Dock = DockStyle.Fill;
        }

        private void FormZone_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pbxImage.Image = m_scanTask.GetScanData().ScanImage.GetDisplayImage(m_selectedChannelIndex, ref m_bitmap);
        }

        private void m_cursorTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
