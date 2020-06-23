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
            // 激光[通道]开关状态/功率/增益
            chbx405.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM) == LASER_CHAN_SWITCH.ON ? true : false;
            chbx488.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM) == LASER_CHAN_SWITCH.ON ? true : false;
            chbx561.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM) == LASER_CHAN_SWITCH.ON ? true : false;
            chbx640.Checked = m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM) == LASER_CHAN_SWITCH.ON ? true : false;

            tb405Power.Value = LaserDevice.PowerToConfigValue(m_config.GetLaserPower(CHAN_ID.WAVELENGTH_405_NM));
            tb488Power.Value = LaserDevice.PowerToConfigValue(m_config.GetLaserPower(CHAN_ID.WAVELENGTH_488_NM));
            tb561Power.Value = LaserDevice.PowerToConfigValue(m_config.GetLaserPower(CHAN_ID.WAVELENGTH_561_NM));
            tb640Power.Value = LaserDevice.PowerToConfigValue(m_config.GetLaserPower(CHAN_ID.WAVELENGTH_640_NM));

            tbx405Power.Text = m_config.GetLaserPower(CHAN_ID.WAVELENGTH_405_NM).ToString("F2");
            tbx488Power.Text = m_config.GetLaserPower(CHAN_ID.WAVELENGTH_488_NM).ToString("F2");
            tbx561Power.Text = m_config.GetLaserPower(CHAN_ID.WAVELENGTH_561_NM).ToString("F2");
            tbx640Power.Text = m_config.GetLaserPower(CHAN_ID.WAVELENGTH_640_NM).ToString("F2");
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
            UpdateVariables();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (!LaserDevice.IsConnected())
            {
                MessageBox.Show("激光器未连接，请先连接激光器.");
                return;
            }

            if (m_scheduler.TaskScanning() == false)
            {
                UpdateVariables();
                m_scheduler.CreateScanTask(0, "实时扫描", out ScanTask scanTask);
                API_RETURN_CODE code = m_scheduler.StartScanTask(scanTask);
                if (code != API_RETURN_CODE.API_SUCCESS)
                {
                    MessageBox.Show(string.Format("启动扫描任务失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
                }
            }
            else
            {
                ScanTask scanTask = m_scheduler.GetScanningTask();
                API_RETURN_CODE code = m_scheduler.StopScanTask(scanTask);
                if (code != API_RETURN_CODE.API_SUCCESS)
                {
                    MessageBox.Show(string.Format("暂停扫描任务失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
                }
            }
        }

        private void tb405Power_ValueChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_405_NM;
            double power = LaserDevice.ConfigValueToPower(tb405Power.Value);
            m_config.SetLaserPower(id, power);
            if (LaserDevice.IsConnected())
            {
                LaserDevice.SetChannelPower(id, power);
            }
            tbx405Power.Text = power.ToString("F2");
        }

        private void tb488Power_ValueChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_488_NM;
            double power = LaserDevice.ConfigValueToPower(tb488Power.Value);
            m_config.SetLaserPower(id, power);
            if (LaserDevice.IsConnected())
            {
                LaserDevice.SetChannelPower(id, power);
            }
            tbx488Power.Text = power.ToString("F2");
        }

        private void tb561Power_ValueChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_561_NM;
            double power = LaserDevice.ConfigValueToPower(tb561Power.Value);
            m_config.SetLaserPower(id, power);
            if (LaserDevice.IsConnected())
            {
                LaserDevice.SetChannelPower(id, power);
            }
            tbx561Power.Text = power.ToString("F2");
        }

        private void tb640Power_ValueChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_640_NM;
            double power = LaserDevice.ConfigValueToPower(tb640Power.Value);
            m_config.SetLaserPower(id, power);
            if (LaserDevice.IsConnected())
            {
                LaserDevice.SetChannelPower(id, power);
            }
            tbx640Power.Text = power.ToString("F2");
        }

        private void chbx405_CheckedChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_405_NM;
            LASER_CHAN_SWITCH status = chbx405.Checked ? LASER_CHAN_SWITCH.ON : LASER_CHAN_SWITCH.OFF;
            m_config.SetLaserSwitch(id, status);
            if (LaserDevice.IsConnected())
            {
                if (status == LASER_CHAN_SWITCH.ON)
                {
                    LaserDevice.OpenChannel(id);
                    LaserDevice.SetChannelPower(id, m_config.GetLaserPower(id));
                }
                else
                {
                    LaserDevice.CloseChannel(id);
                }
            }
        }

        private void chbx488_CheckedChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_488_NM;
            LASER_CHAN_SWITCH status = chbx488.Checked ? LASER_CHAN_SWITCH.ON : LASER_CHAN_SWITCH.OFF;
            m_config.SetLaserSwitch(id, status);
            if (LaserDevice.IsConnected())
            {
                if (status == LASER_CHAN_SWITCH.ON)
                {
                    LaserDevice.OpenChannel(id);
                    LaserDevice.SetChannelPower(id, m_config.GetLaserPower(id));
                }
                else
                {
                    LaserDevice.CloseChannel(id);
                }
            }
        }

        private void chbx561_CheckedChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_561_NM;
            LASER_CHAN_SWITCH status = chbx561.Checked ? LASER_CHAN_SWITCH.ON : LASER_CHAN_SWITCH.OFF;
            m_config.SetLaserSwitch(id, status);
            if (LaserDevice.IsConnected())
            {
                if (status == LASER_CHAN_SWITCH.ON)
                {
                    LaserDevice.OpenChannel(id);
                    LaserDevice.SetChannelPower(id, m_config.GetLaserPower(id));
                }
                else
                {
                    LaserDevice.CloseChannel(id);
                }
            }
        }

        private void chbx640_CheckedChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_640_NM;
            LASER_CHAN_SWITCH status = chbx640.Checked ? LASER_CHAN_SWITCH.ON : LASER_CHAN_SWITCH.OFF;
            m_config.SetLaserSwitch(id, status);
            if (LaserDevice.IsConnected())
            {
                if (status == LASER_CHAN_SWITCH.ON)
                {
                    LaserDevice.OpenChannel(id);
                    LaserDevice.SetChannelPower(id, m_config.GetLaserPower(id));
                }
                else
                {
                    LaserDevice.CloseChannel(id);
                }
            }
        }
    }
}
