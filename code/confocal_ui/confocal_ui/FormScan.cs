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
    public partial class FormScan : DockContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Dictionary<int, string> scanPixelsDict;
        private Dictionary<SCAN_MIRROR_NUM, string> mirrorNumDict;
        private Dictionary<SCAN_STRATEGY, string> scanStrategyDict;
        private Config m_config;
        private Params m_params;
        private Scheduler m_scheduler;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public FormScan()
        {
            InitializeComponent();
        }

        private void InitVariables()
        {
            scanPixelsDict = new Dictionary<int, string>();
            scanPixelsDict.Add(256, "256x256");
            scanPixelsDict.Add(512, "512x512");
            scanPixelsDict.Add(1024, "1024x1024");
            scanPixelsDict.Add(2048, "2048x2048");
            scanPixelsDict.Add(4096, "4096x4096");

            mirrorNumDict = new Dictionary<SCAN_MIRROR_NUM, string>();
            mirrorNumDict.Add(SCAN_MIRROR_NUM.TWO, "双振镜");
            mirrorNumDict.Add(SCAN_MIRROR_NUM.THREEE, "三振镜");

            scanStrategyDict = new Dictionary<SCAN_STRATEGY, string>();
            scanStrategyDict.Add(SCAN_STRATEGY.Z_UNIDIRECTION, "单向");
            scanStrategyDict.Add(SCAN_STRATEGY.Z_BIDIRECTION, "双向");

            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_scheduler = Scheduler.CreateInstance();
        }

        private void InitControlers()
        {
            cbxScanPixels.DataSource = scanPixelsDict.ToList<KeyValuePair<int, string>>();
            cbxScanPixels.DisplayMember = "Value";
            cbxScanPixels.ValueMember = "Key";

            cbxScanStrategy.DataSource = scanStrategyDict.ToList<KeyValuePair<SCAN_STRATEGY, string>>();
            cbxScanStrategy.DisplayMember = "Value";
            cbxScanStrategy.ValueMember = "Key";

            switch (m_config.GetScanMode())
            {
                case SCAN_MODE.GALVANOMETER:
                    rbtnGalv.Checked = true;
                    break;
                case SCAN_MODE.RESONANT:
                    rbtnRes.Checked = true;
                    break;
                default:
                    rbtnGalv.Checked = true;
                    break;
            }

            tbxPixelTime.Text = m_config.GetScanPixelTime().ToString();
            cbxScanStrategy.SelectedIndex = cbxScanStrategy.FindString(scanStrategyDict[m_config.GetScanStrategy()]);

            chbx405.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM) == LASER_CHAN_SWITCH.ON ? true : false;
            chbx488.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM) == LASER_CHAN_SWITCH.ON ? true : false;
            chbx561.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM) == LASER_CHAN_SWITCH.ON ? true : false;
            chbx640.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM) == LASER_CHAN_SWITCH.ON ? true : false;
        }

        private void UpdateVariables()
        {
            m_config.SetScanPixelTime(double.Parse(tbxPixelTime.Text));
            int scanPixels = ((KeyValuePair<int, string>)cbxScanPixels.SelectedItem).Key;
            m_config.SetScanXPoints(scanPixels);
            m_config.SetScanYPoints(scanPixels);

            m_params.Calculate();
        }

        private void FormScan_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            UpdateVariables();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (m_scheduler.Scanning == false)
            {
                m_scheduler.StartToScan();
            }
            else
            {
                m_scheduler.Stop();
            }
        }
    }
}
