using confocal_core;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            // 扫描模式
            rbtnGalv.Checked = m_config.GetScanMode() == SCAN_MODE.GALVANOMETER ? true : false;
            // 扫描策略
            cbxScanStrategy.DataSource = scanStrategyDict.ToList<KeyValuePair<SCAN_STRATEGY, string>>();
            cbxScanStrategy.DisplayMember = "Value";
            cbxScanStrategy.ValueMember = "Key";
            cbxScanStrategy.SelectedIndex = cbxScanStrategy.FindString(scanStrategyDict[m_config.GetScanStrategy()]);
            // 扫描像素
            cbxScanPixels.DataSource = scanPixelsDict.ToList<KeyValuePair<int, string>>();
            cbxScanPixels.DisplayMember = "Value";
            cbxScanPixels.ValueMember = "Key";
            cbxScanPixels.SelectedIndex = cbxScanPixels.FindString(scanPixelsDict[m_config.GetScanXPoints()]);
            // 振镜系统
            rbtnThree.Checked = m_config.GetScanMirrorNum() == SCAN_MIRROR_NUM.THREEE ? true : false;
            // 停留时间
            tbxDwellTime.Text = m_config.GetScanDwellTime().ToString();
            // 激光[通道]开关状态
            chbx405.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM) == LASER_CHAN_SWITCH.ON ? true : false;
            chbx488.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM) == LASER_CHAN_SWITCH.ON ? true : false;
            chbx561.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM) == LASER_CHAN_SWITCH.ON ? true : false;
            chbx640.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM) == LASER_CHAN_SWITCH.ON ? true : false;
        }

        private void UpdateVariables()
        {
            // 扫描模式
            SCAN_MODE mode = rbtnGalv.Checked ? SCAN_MODE.GALVANOMETER : SCAN_MODE.RESONANT;
            m_config.SetScanMode(mode);
            // 扫描策略
            SCAN_STRATEGY strategy = ((KeyValuePair<SCAN_STRATEGY, string>)cbxScanStrategy.SelectedItem).Key;
            m_config.SetScanStartegy(strategy);
            // 振镜系统
            SCAN_MIRROR_NUM mirrorNum = rbtnThree.Checked ? SCAN_MIRROR_NUM.THREEE : SCAN_MIRROR_NUM.TWO;
            m_config.SetScanMirrorNum(mirrorNum);
            // Dwell Time
            m_config.SetScanDwellTime(double.Parse(tbxDwellTime.Text));
            // 扫描像素
            int scanPixels = ((KeyValuePair<int, string>)cbxScanPixels.SelectedItem).Key;
            m_config.SetScanXPoints(scanPixels);
            m_config.SetScanYPoints(scanPixels);
            // 激光[通道]开关状态
            LASER_CHAN_SWITCH status = chbx405.Checked ? LASER_CHAN_SWITCH.ON : LASER_CHAN_SWITCH.OFF;
            m_config.SetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM, status);
            status = chbx488.Checked ? LASER_CHAN_SWITCH.ON : LASER_CHAN_SWITCH.OFF;
            m_config.SetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM, status);
            status = chbx561.Checked ? LASER_CHAN_SWITCH.ON : LASER_CHAN_SWITCH.OFF;
            m_config.SetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM, status);
            status = chbx640.Checked ? LASER_CHAN_SWITCH.ON : LASER_CHAN_SWITCH.OFF;
            m_config.SetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM, status);

            m_params.Calculate();
        }

        private void FormScan_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            byte[,] source = new byte[3, 5] { { 1,2,3,4,5},{ 6,7,8,9,10},{11,12,13,14,15 } };
            byte[] dst = new byte[10];
            int x = source.GetLowerBound(0);
            int y = source.GetLowerBound(1);
            int a = source.GetUpperBound(0);
            int b = source.GetUpperBound(1);

            //IntPtr ptr = Marshal.AllocHGlobal(10);
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(source, 4);
            Marshal.Copy(ptr, dst, 0, 4);

            UpdateVariables();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (ScanTask.Scannning == false)
            {
                UpdateVariables();
                m_scheduler.StartScanTask();
            }
            else
            {
                m_scheduler.StopScanTask();
            }
        }
    }
}
