using confocal_core;
using confocal_ui;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace confocal_ui
{
    public partial class FormMain : Form
    {
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
        /************************************************************************************/
        private FormScan m_pFormScan = new FormScan();
        private FormZone m_pFormZone = new FormZone();
        private FormMeas m_pFormMeas = new FormMeas();
        private FormParas m_pFormShowBox = new FormParas();
        private List<FormDisplay> m_pFormImages = new List<FormDisplay>();

        private Config m_config;
        private Params m_params;
        private Scheduler m_scheduler;
        /************************************************************************************/

        public FormMain()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景. 
            SetStyle(ControlStyles.DoubleBuffer, true);         // 双缓冲  
        }

        private void InitVariables()
        {
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_scheduler = Scheduler.CreateInstance();

            m_scheduler.ScanTaskCreated += new ScanTaskEventHandler(ScanTaskCreatedHandler);
            m_scheduler.ScanTaskStarted += new ScanTaskEventHandler(ScanTaskStartedHandler);
            m_scheduler.ScanTaskStopped += new ScanTaskEventHandler(ScanTaskStoppedHandler);
            m_scheduler.ScanTaskReleased += new ScanTaskEventHandler(ScanTaskReleasedHandler);
            m_scheduler.ActivatedChannelChanged += new ScanTaskEventHandler(ActivatedChannelChangedHandler);
            m_scheduler.ScanTaskConfigured += new ScanTaskEventHandler(ConfigScanTaskHandler);
            m_scheduler.SelectedChannelChanged += new ChannelEventHandler(SelectedChannelChangedHandler);
            m_scheduler.ChannelColorReferenceChanged += new ChannelEventHandler(SelectedColorReferenceChangedHandler);
        }

        private void ConfigDevice()
        {
            ConfigLaserDevice();
            ConfigUsbDac();
        }

        private void InitLoadControlers()
        {
            // version
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text += version;
            Logger.Info(string.Format("get software version: [{0}]", version));

            // menu 
            string portName = m_config.GetLaserPortName();
            cbxSelectLaser.SelectedIndex = portName != null ? cbxSelectLaser.FindString(portName) : 0;

            // panel
            m_pFormShowBox.Show(this.dockPanel, DockState.DockLeft);
            // m_pFormMeas.Show(this.dockPanel, DockState.DockLeft);
            m_pFormScan.Show(this.dockPanel, DockState.DockRight);
            m_pFormZone.Show(m_pFormScan.Pane, DockAlignment.Bottom, 0.25);
        }

        private void UpdateControlers()
        {
            // menu 
            cbxSelectLaser.Enabled = LaserDevice.IsConnected() ? false : true;
            btnLaserConnect.Enabled = LaserDevice.IsConnected() ? false : true;
            btnLaserRelease.Enabled = LaserDevice.IsConnected() ? true : false;
        }

        private API_RETURN_CODE ScanTaskCreatedHandler(ScanTask scanTask, object paras)
        {
            FormDisplay formImage = FindFormImage(scanTask);
            if (formImage == null)
            {
                Logger.Info(string.Format("new scan task[{0}|{1}], create form image.", scanTask.TaskId, scanTask.TaskName));
                formImage = new FormDisplay(scanTask);
                formImage.Show(this.dockPanel, DockState.Document);
                m_pFormImages.Add(formImage);
            }
            else
            {

                Logger.Info(string.Format("scan task[{0}|{1} alreay created.", scanTask.TaskId, scanTask.TaskName));
            }
            formImage.ScanTaskCreated();
            m_pFormZone.ScanTaskCreated(scanTask);
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE ScanTaskStartedHandler(ScanTask scanTask, object paras)
        {
            FormDisplay formImage = FindFormImage(scanTask);
            if (formImage == null)
            {
                Logger.Info(string.Format("scan task matching form image not found."));
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_START_FAILED;
            }
            formImage.ScanTaskStrated();
            m_pFormScan.ScanTaskStarted();
            m_pFormShowBox.ScanTaskStarted();
            m_pFormZone.ScanTaskStrated();
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE ScanTaskStoppedHandler(ScanTask scanTask, object paras)
        {
            FormDisplay formImage = FindFormImage(scanTask);
            if (formImage == null)
            {
                Logger.Info(string.Format("scan task matching form image not found."));
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_START_FAILED;
            }
            formImage.ScanTaskStopped();
            m_pFormScan.ScanTaskStopped();
            m_pFormZone.ScanTaskStopped();
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE ScanTaskReleasedHandler(ScanTask scanTask, object paras)
        {
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE ActivatedChannelChangedHandler(ScanTask scanTask, object paras)
        {
            FormDisplay formImage = FindFormImage(scanTask);
            if (formImage == null)
            {
                Logger.Info(string.Format("scan task matching form image not found."));
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_START_FAILED;
            }
            formImage.ActivatedChannelChanged();
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE ConfigScanTaskHandler(ScanTask scanTask, object paras)
        {
            m_pFormShowBox.ScanTaskConfigured();
            m_pFormScan.ScanTaskConfigured();
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE SelectedChannelChangedHandler(ScanTask scanTask, CHAN_ID id, object paras)
        {
            FormDisplay formImage = FindFormImage(scanTask);
            if (formImage == null)
            {
                Logger.Info(string.Format("scan task matching form image not found."));
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_START_FAILED;
            }
            formImage.SelectedChannelChanged(id);
            m_pFormZone.SelectedChannelChanged(id);
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE SelectedColorReferenceChangedHandler(ScanTask scanTask, CHAN_ID id, object paras)
        {
            Color color = (Color)paras;
            FormDisplay formImage = FindFormImage(scanTask);
            if (formImage == null)
            {
                Logger.Info(string.Format("scan task matching form image not found."));
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_START_FAILED;
            }
            formImage.ChannelColorReferenceChanged(id, color);
            m_pFormZone.ChannelColorReferenceChanged(id, color);
            return API_RETURN_CODE.API_SUCCESS;
        }

        private FormDisplay FindFormImage(ScanTask scanTask)
        {
            foreach (FormDisplay formImage in m_pFormImages)
            {
                if (formImage.FormId == scanTask.TaskId)
                {
                    return formImage;
                }
            }
            return null;
        }

        private void ReleaseScanTask()
        {
            ScanTask scanTask = m_scheduler.GetScanningTask();
            if (scanTask != null && scanTask.Scannning)
            {
                m_scheduler.StopScanTask(scanTask);
            }
        }

        private void ReleaseLaserDevice()
        {
            if (LaserDevice.IsConnected())
            {
                for (int i = 0; i < m_config.GetChannelNum(); i++)
                {
                    CHAN_ID id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i);
                    if (m_config.GetLaserSwitch(id) == LASER_CHAN_SWITCH.ON)
                    {
                        LaserDevice.CloseChannel(id);
                        m_config.SetLaserSwitch(id, LASER_CHAN_SWITCH.OFF);
                    }
                }
            }
            LaserDevice.Release();
        }

        private void ReleaseUsbDac()
        {
            if (UsbDac.IsConnected())
            {
                UsbDac.Release();
            }
        }

        private void ConfigUsbDac()
        {
            API_RETURN_CODE code = UsbDac.Connect();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("配置激光器增益失败，请联系厂家。"));
            }
        }

        private void ConfigLaserDevice()
        {
            string portName = cbxSelectLaser.SelectedItem.ToString();
            API_RETURN_CODE code = LaserDevice.Connect(portName);
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("连接激光器端口[{0}]失败，请检查接线是否正常，端口号是否正确。", portName));
            }

            m_config.SetLaserPortName(portName);
            for (int i = 0; i < m_config.GetChannelNum(); i++)
            {
                CHAN_ID id = (CHAN_ID)i;
                if (m_config.GetLaserSwitch(id) != LASER_CHAN_SWITCH.ON)
                {
                    continue;
                }

                if (LaserDevice.OpenChannel(id) != API_RETURN_CODE.API_SUCCESS)
                {
                    m_config.SetLaserSwitch(id, LASER_CHAN_SWITCH.OFF);
                    continue;
                }
                
                LaserDevice.SetChannelPower(id, (float)m_config.GetLaserPower(id));
            }

        }

        #region controlers event

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitLoadControlers();
            ConfigDevice();
            UpdateControlers();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // release scantask
            ReleaseScanTask();

            // close all activated laser channels
            ReleaseLaserDevice();

            // close usb dac device
            ReleaseUsbDac();
        }

        private void btnLaserConnect_Click(object sender, EventArgs e)
        {
            ConfigLaserDevice();
            UpdateControlers();
        }

        private void btnLaserRelease_Click(object sender, EventArgs e)
        {
            if (LaserDevice.IsConnected())
            {
                for (int i = 0; i < m_config.GetChannelNum(); i++)
                {
                    CHAN_ID id = (CHAN_ID)Enum.ToObject(typeof(CHAN_ID), i);
                    if (m_config.GetLaserSwitch(id) == LASER_CHAN_SWITCH.ON)
                    {
                        LaserDevice.CloseChannel(id);
                        m_config.SetLaserSwitch(id, LASER_CHAN_SWITCH.OFF);
                    }
                }
            }

            API_RETURN_CODE code = LaserDevice.Release();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("断开激光器连接失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
            }
            else
            {
                m_config.SetLaserPortName(null);
            }
            UpdateControlers();
        }

        #endregion

        private void tsmiScan_Click(object sender, EventArgs e)
        {
            m_pFormScan.Show();
            this.Activate();
        }

        private void tsmiShow_Click(object sender, EventArgs e)
        {
            m_pFormShowBox.Show();
            this.Activate();
        }

        private void tsmiSysCfg_Click(object sender, EventArgs e)
        {
            if (m_scheduler.TaskScanning())
            {
                MessageBox.Show("正在扫描中，请先停止扫描再配置系统参数。");
                return;
            }

            FormSysCfg formSysCfg = new FormSysCfg();
            if (formSysCfg.ShowDialog() == DialogResult.OK)
            {
                ScanTask scanTask = m_scheduler.GetScanningTask();
                m_scheduler.ConfigScanTask(scanTask);
                formSysCfg.Close();
            }
        }
    }
}
