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
        private Dictionary<SCAN_ACQUISITION_MODE, string> scanAcquisitionModeDict;
        private Config m_config;
        private Params m_params;
        private Scheduler m_scheduler;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public FormScan()
        {
            InitializeComponent();
        }

        public void ScanTaskStarted()
        {
            Logger.Info(string.Format("FormScan scan task started."));
            btnScan.Image = m_scheduler.TaskScanning() ? global::confocal_ui.Properties.Resources.Stop : global::confocal_ui.Properties.Resources.Scan;
            btnScan.BackColor = m_scheduler.TaskScanning() ? System.Drawing.SystemColors.GradientActiveCaption : System.Drawing.SystemColors.Control;
        }

        public void ScanTaskStopped()
        {
            Logger.Info(string.Format("FormScan scan task stopped."));
            btnScan.Image = m_scheduler.TaskScanning() ? global::confocal_ui.Properties.Resources.Stop : global::confocal_ui.Properties.Resources.Scan;
            btnScan.BackColor = m_scheduler.TaskScanning() ? System.Drawing.SystemColors.GradientActiveCaption : System.Drawing.SystemColors.Control;
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

            scanAcquisitionModeDict = new Dictionary<SCAN_ACQUISITION_MODE, string>();
            scanAcquisitionModeDict.Add(SCAN_ACQUISITION_MODE.STANDARD, "标准");
            scanAcquisitionModeDict.Add(SCAN_ACQUISITION_MODE.AVARAGE, "平均");
            scanAcquisitionModeDict.Add(SCAN_ACQUISITION_MODE.SUM, "积分");

            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_scheduler = Scheduler.CreateInstance();
        }

        private void InitControlers()
        {
            // 扫描模式
            rbtnGalv.Checked = m_config.GetScanMode() == SCAN_MODE.GALVANOMETER ? true : false;
            rbtnRes.Checked = m_config.GetScanMode() == SCAN_MODE.GALVANOMETER ? false : true;
            rbtnGalv.CheckedChanged += rbtnGalv_CheckedChanged;
            
            // 扫描策略
            cbxScanStrategy.DataSource = scanStrategyDict.ToList<KeyValuePair<SCAN_STRATEGY, string>>();
            cbxScanStrategy.DisplayMember = "Value";
            cbxScanStrategy.ValueMember = "Key";
            cbxScanStrategy.SelectedIndex = cbxScanStrategy.FindString(scanStrategyDict[m_config.GetScanStrategy()]);
            cbxScanStrategy.SelectedIndexChanged += cbxScanStrategy_SelectedIndexChanged;

            // 扫描采集模式
            cbxAcquisitionMode.DataSource = scanAcquisitionModeDict.ToList<KeyValuePair<SCAN_ACQUISITION_MODE, string>>();
            cbxAcquisitionMode.DisplayMember = "Value";
            cbxAcquisitionMode.ValueMember = "Key";
            cbxAcquisitionMode.SelectedIndex = cbxAcquisitionMode.FindString(scanAcquisitionModeDict[m_config.GetScanAcquisitionMode()]);
            cbxAcquisitionMode.SelectedIndexChanged += cbxAcquisitionMode_SelectedIndexChanged;

            // 扫描采集模式数量
            cbxAcquisitionModeNum.DataSource = new int[] { 2, 4, 8, 16 };
            cbxAcquisitionModeNum.SelectedIndex = cbxAcquisitionModeNum.FindString(m_config.GetScanAcquisitionModeNum().ToString());
            
            // 扫描像素
            cbxScanPixels.DataSource = scanPixelsDict.ToList<KeyValuePair<int, string>>();
            cbxScanPixels.DisplayMember = "Value";
            cbxScanPixels.ValueMember = "Key";
            cbxScanPixels.SelectedIndex = cbxScanPixels.FindString(scanPixelsDict[m_config.GetScanXPoints()]);
            cbxScanPixels.SelectedIndexChanged += cbxScanPixels_SelectedIndexChanged;

            // 振镜系统
            rbtnThree.Checked = m_config.GetScanMirrorNum() == SCAN_MIRROR_NUM.THREEE ? true : false;
            rbtnTwo.Checked = m_config.GetScanMirrorNum() == SCAN_MIRROR_NUM.THREEE ? false : true;
            rbtnTwo.CheckedChanged += rbtnTwo_CheckedChanged;

            // 停留时间
            cbxDwellTime.DataSource = new string[] { "2.0", "4.0", "6.0", "8.0", "10.0" };
            cbxDwellTime.SelectedIndex = cbxDwellTime.FindString(m_config.GetScanDwellTime().ToString("F1"));
            cbxDwellTime.SelectedIndexChanged += cbxDwellTime_SelectedIndexChanged;

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

            tb405Gain.Value = (int)m_config.GetPmtGain(CHAN_ID.WAVELENGTH_405_NM) * 10;
            tb488Gain.Value = (int)m_config.GetPmtGain(CHAN_ID.WAVELENGTH_488_NM) * 10;
            tb561Gain.Value = (int)m_config.GetPmtGain(CHAN_ID.WAVELENGTH_561_NM) * 10;
            tb640Gain.Value = (int)m_config.GetPmtGain(CHAN_ID.WAVELENGTH_640_NM) * 10;

            tbx405Gain.Text = string.Concat(m_config.GetPmtGain(CHAN_ID.WAVELENGTH_405_NM).ToString("F1"), "");
            tbx488Gain.Text = string.Concat(m_config.GetPmtGain(CHAN_ID.WAVELENGTH_488_NM).ToString("F1"), "");
            tbx561Gain.Text = string.Concat(m_config.GetPmtGain(CHAN_ID.WAVELENGTH_561_NM).ToString("F1"), "");
            tbx640Gain.Text = string.Concat(m_config.GetPmtGain(CHAN_ID.WAVELENGTH_640_NM).ToString("F1"), "");

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
            // 采集模式 & 采集数量
            SCAN_ACQUISITION_MODE acquisitionMode = ((KeyValuePair<SCAN_ACQUISITION_MODE, string>)cbxAcquisitionMode.SelectedItem).Key;
            m_config.SetScanAcquisitionMode(acquisitionMode);
            // 采集模式数量
            int acquisitionModeNum = (int)cbxAcquisitionModeNum.SelectedItem;
            m_config.SetScanAcquisitionModeNum(acquisitionModeNum);
            // Dwell Time
            m_config.SetScanDwellTime(double.Parse(cbxDwellTime.SelectedItem.ToString()));
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

        private void UpdateControlers()
        {
            cbxAcquisitionModeNum.Visible = m_config.GetScanAcquisitionMode() == SCAN_ACQUISITION_MODE.STANDARD ? false : true;
            lbFps.Text = string.Format("Fps: {0}", m_params.Fps.ToString("F2"));
            lbFrameTime.Text = string.Format("Frame Time: {0}s", (1 / m_params.Fps).ToString("F2"));
        }

        private void FormScan_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
            UpdateControlers();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            //if (!LaserDevice.IsConnected())
            //{
            //    MessageBox.Show("激光器未连接，请先连接激光器.");
            //    return;
            //}

            if (m_scheduler.TaskScanning() == false)
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                m_scheduler.CreateScanTask(0, "实时扫描", out ScanTask scanTask);
                API_RETURN_CODE code = m_scheduler.StartScanTask(scanTask);
                this.Cursor = System.Windows.Forms.Cursors.Default;

                if (code != API_RETURN_CODE.API_SUCCESS)
                {
                    MessageBox.Show(string.Format("启动扫描任务失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
                }
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                ScanTask scanTask = m_scheduler.GetScanningTask();
                API_RETURN_CODE code = m_scheduler.StopScanTask(scanTask);
                this.Cursor = System.Windows.Forms.Cursors.Default;

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
            ScanTask scanTask = m_scheduler.GetScanningTask();
            m_scheduler.ChangeActivatedChannels(scanTask);
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
            ScanTask scanTask = m_scheduler.GetScanningTask();
            m_scheduler.ChangeActivatedChannels(scanTask);
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
            ScanTask scanTask = m_scheduler.GetScanningTask();
            m_scheduler.ChangeActivatedChannels(scanTask);
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
            ScanTask scanTask = m_scheduler.GetScanningTask();
            m_scheduler.ChangeActivatedChannels(scanTask);
        }

        private void tb405Gain_ValueChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_405_NM;
            float configValue = tb405Gain.Value / 10.0f;
            m_config.SetPmtGain(id, configValue);
            UsbDac.SetDacOut((uint)id, UsbDac.ConfigValueToVout(configValue));
            // UsbDac.SetGainCalibration((uint)id, UsbDac.ConfigValueToGain(configValue));
            tbx405Gain.Text = string.Concat(configValue.ToString("F1"), "");
        }

        private void tb488Gain_ValueChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_488_NM;
            float configValue = tb488Gain.Value / 10.0f;
            m_config.SetPmtGain(id, configValue);
            UsbDac.SetDacOut((uint)id, UsbDac.ConfigValueToVout(configValue));
            // UsbDac.SetGainCalibration((uint)id, UsbDac.ConfigValueToGain(configValue));
            tbx488Gain.Text = string.Concat(configValue.ToString("F1"), "");
        }

        private void tb561Gain_ValueChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_561_NM;
            float configValue = tb561Gain.Value / 10.0f;
            m_config.SetPmtGain(id, configValue);
            UsbDac.SetDacOut((uint)id, UsbDac.ConfigValueToVout(configValue));
            // UsbDac.SetGainCalibration((uint)id, UsbDac.ConfigValueToGain(configValue));
            tbx561Gain.Text = string.Concat(configValue.ToString("F1"), "");
        }

        private void tb640Gain_ValueChanged(object sender, EventArgs e)
        {
            CHAN_ID id = CHAN_ID.WAVELENGTH_640_NM;
            float configValue = tb640Gain.Value / 10.0f;
            m_config.SetPmtGain(id, configValue);
            UsbDac.SetDacOut((uint)id, UsbDac.ConfigValueToVout(configValue));
            // UsbDac.SetGainCalibration((uint)id, UsbDac.ConfigValueToGain(configValue));
            tbx640Gain.Text = string.Concat(configValue.ToString("F1"), "");
        }

        private void cbxScanStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SCAN_STRATEGY strategy = ((KeyValuePair<SCAN_STRATEGY, string>)cbxScanStrategy.SelectedItem).Key;
            if (m_config.GetScanStrategy() == strategy)
            {
                return;
            }

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            // if task is not running, just update config
            if (m_scheduler.TaskScanning() == false)
            {
                m_config.SetScanStartegy(strategy);
                m_scheduler.ConfigScanTask(m_scheduler.GetScanningTask());
                UpdateControlers();

                this.Cursor = System.Windows.Forms.Cursors.Default;
                return;
            }

            // if task is already running, stop first
            m_scheduler.StopScanTask(m_scheduler.GetScanningTask());
            // update config
            m_config.SetScanStartegy(strategy);
            // create & start task
            m_scheduler.CreateScanTask(0, "实时扫描", out ScanTask scanTask);
            API_RETURN_CODE code = m_scheduler.StartScanTask(scanTask);

            UpdateControlers();
            this.Cursor = System.Windows.Forms.Cursors.Default;

            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("启动扫描任务失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
            }
        }

        private void cbxAcquisitionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SCAN_ACQUISITION_MODE acquisitionMode = ((KeyValuePair<SCAN_ACQUISITION_MODE, string>)cbxAcquisitionMode.SelectedItem).Key;
            if (m_config.GetScanAcquisitionMode() == acquisitionMode)
            {
                return;
            }

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            // if task is not running, just update config
            if (m_scheduler.TaskScanning() == false)
            {
                m_config.SetScanAcquisitionMode(acquisitionMode);
                m_scheduler.ConfigScanTask(m_scheduler.GetScanningTask());
                UpdateControlers();

                this.Cursor = System.Windows.Forms.Cursors.Default;
                return;
            }

            // if task is already running, stop first
            m_scheduler.StopScanTask(m_scheduler.GetScanningTask());
            // update config
            m_config.SetScanAcquisitionMode(acquisitionMode);
            // create & start task
            m_scheduler.CreateScanTask(0, "实时扫描", out ScanTask scanTask);
            API_RETURN_CODE code = m_scheduler.StartScanTask(scanTask);

            UpdateControlers();
            this.Cursor = System.Windows.Forms.Cursors.Default;

            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("启动扫描任务失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
            }
        }

        private void rbtnGalv_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rbtnTwo_CheckedChanged(object sender, EventArgs e)
        {
            SCAN_MIRROR_NUM mirrorNum = rbtnTwo.Checked ? SCAN_MIRROR_NUM.TWO : SCAN_MIRROR_NUM.THREEE;
            if (m_config.GetScanMirrorNum() == mirrorNum)
            {
                return;
            }

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            // if task is not running, just update config
            if (m_scheduler.TaskScanning() == false)
            {
                m_config.SetScanMirrorNum(mirrorNum);
                m_scheduler.ConfigScanTask(m_scheduler.GetScanningTask());

                UpdateControlers();
                this.Cursor = System.Windows.Forms.Cursors.Default;
                return;
            }

            // if task is already running, stop first
            m_scheduler.StopScanTask(m_scheduler.GetScanningTask());
            // update config
            m_config.SetScanMirrorNum(mirrorNum);
            // create & start task
            m_scheduler.CreateScanTask(0, "实时扫描", out ScanTask scanTask);
            API_RETURN_CODE code = m_scheduler.StartScanTask(scanTask);

            UpdateControlers();
            this.Cursor = System.Windows.Forms.Cursors.Default;

            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("启动扫描任务失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
            }
        }

        private void cbxScanPixels_SelectedIndexChanged(object sender, EventArgs e)
        {
            int scanPixels = ((KeyValuePair<int, string>)cbxScanPixels.SelectedItem).Key;
            if (m_config.GetScanXPoints() == scanPixels && m_config.GetScanYPoints() == scanPixels)
            {
                return;
            }

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            // if task is not running, just update config
            if (m_scheduler.TaskScanning() == false)
            {
                m_config.SetScanXPoints(scanPixels);
                m_config.SetScanYPoints(scanPixels);
                m_scheduler.ConfigScanTask(m_scheduler.GetScanningTask());
                UpdateControlers();

                this.Cursor = System.Windows.Forms.Cursors.Default;
                return;
            }

            // if task is already running, stop first
            m_scheduler.StopScanTask(m_scheduler.GetScanningTask());
            // update config
            m_config.SetScanXPoints(scanPixels);
            m_config.SetScanYPoints(scanPixels);
            // create & start task
            m_scheduler.CreateScanTask(0, "实时扫描", out ScanTask scanTask);
            API_RETURN_CODE code = m_scheduler.StartScanTask(scanTask);

            UpdateControlers();
            this.Cursor = System.Windows.Forms.Cursors.Default;

            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("启动扫描任务失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
            }
        }

        private void cbxDwellTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            double dwellTime = double.Parse(cbxDwellTime.SelectedItem.ToString());
            if (m_config.GetScanDwellTime() == dwellTime)
            {
                return;
            }

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            // if task is not running, just update config
            if (m_scheduler.TaskScanning() == false)
            {
                m_config.SetScanDwellTime(dwellTime);
                m_scheduler.ConfigScanTask(m_scheduler.GetScanningTask());
                UpdateControlers();

                this.Cursor = System.Windows.Forms.Cursors.Default;
                return;
            }

            // if task is already running, stop first
            m_scheduler.StopScanTask(m_scheduler.GetScanningTask());
            // update config
            m_config.SetScanDwellTime(dwellTime);
            // create & start task
            m_scheduler.CreateScanTask(0, "实时扫描", out ScanTask scanTask);
            API_RETURN_CODE code = m_scheduler.StartScanTask(scanTask);

            UpdateControlers();
            this.Cursor = System.Windows.Forms.Cursors.Default;

            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("启动扫描任务失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
            }
        }
    }
}
