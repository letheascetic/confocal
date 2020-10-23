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
        public void SelectedChannelChanged(CHAN_ID id)
        {
            m_selectedChannelIndex = (int)id;
            imageBox.Image = m_scanTask.GetScanData().ScanImage.BGRMat[m_selectedChannelIndex];
        }

        private void InitVariables()
        {
            m_selectedChannelIndex = -1;

            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_scheduler = Scheduler.CreateInstance();

            scanPixelsDict = new Dictionary<int, string>();
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

            pbxZone.Parent = imageBox;
            pbxZone.Size = imageBox.Size;
            pbxZone.Location = imageBox.Location;
            pbxZone.Dock = DockStyle.Fill;
        }

        private void FormZone_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (m_selectedChannelIndex < 0)
            {
                return;
            }
            imageBox.Image = m_scanTask.GetScanData().ScanImage.BGRMat[m_selectedChannelIndex];
        }

        private void m_cursorTimer_Tick(object sender, EventArgs e)
        {

        }

    }
}
