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
        private FormROI m_pFormROI = new FormROI();
        private FormMeas m_pFormMeas = new FormMeas();
        private FormShowBox m_pFormShowBox = new FormShowBox();
        private List<FormImage> m_formImages = new List<FormImage>();

        private Config m_config;
        private Params m_params;
        private Scheduler m_scheduler;
        /************************************************************************************/

        public FormMain()
        {
            InitializeComponent();
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
        }

        private void ConfigDevice()
        {
            API_RETURN_CODE code = ConfigLaserDevice();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("连接激光器端口[{0}]失败，请检查接线是否正常，端口号是否正确。", cbxSelectLaser.SelectedItem.ToString()));
            }

            code = ConfigUsbDac();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("配置激光器增益失败，请联系厂家。"));
            }

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
            m_pFormScan.Show(this.dockPanel, DockState.DockRight);
            m_pFormShowBox.Show(this.dockPanel, DockState.DockLeft);

            //m_formImages = new List<FormImage>();
            //m_formImages.Add(new FormImage());
            //m_formImages.Add(new FormImage());
            //m_formImages[0].Show(this.dockPanel, DockState.Document);
            //m_formImages[1].Show(this.dockPanel, DockState.Document);
            //m_pFormROI.Show(m_pFormSC.Pane, DockAlignment.Left, 0.5);
            //m_pFormROI.Show(m_pFormSC.Pane, DockAlignment.Bottom, 0.5);
            //m_pFormMeas.Show(this.dockPanel, DockState.DockRight);
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
            FormImage formImage = FindFormImage(scanTask);
            if (formImage == null)
            {
                Logger.Info(string.Format("new scan task[{0}|{1}], create form image.", scanTask.TaskId, scanTask.TaskName));
                formImage = new FormImage(scanTask);
                formImage.Show(this.dockPanel, DockState.Document);
                m_formImages.Add(formImage);
            }
            else
            {
                Logger.Info(string.Format("scan task[{0}|{1} alreay created.", scanTask.TaskId, scanTask.TaskName));
            }
            formImage.ScanTaskCreated();
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE ScanTaskStartedHandler(ScanTask scanTask, object paras)
        {
            FormImage formImage = FindFormImage(scanTask);
            if (formImage == null)
            {
                Logger.Info(string.Format("scan task matching form image not found."));
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_START_FAILED;
            }
            formImage.ScanTaskStrated();
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE ScanTaskStoppedHandler(ScanTask scanTask, object paras)
        {
            FormImage formImage = FindFormImage(scanTask);
            if (formImage == null)
            {
                Logger.Info(string.Format("scan task matching form image not found."));
                return API_RETURN_CODE.API_FAILED_SCAN_TASK_START_FAILED;
            }
            formImage.ScanTaskStopped();
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE ScanTaskReleasedHandler(ScanTask scanTask, object paras)
        {
            return API_RETURN_CODE.API_SUCCESS;
        }

        private FormImage FindFormImage(ScanTask scanTask)
        {
            foreach (FormImage formImage in m_formImages)
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

        private API_RETURN_CODE ConfigUsbDac()
        {
            return UsbDac.Connect();
        }

        private API_RETURN_CODE ConfigLaserDevice()
        {
            string portName = cbxSelectLaser.SelectedItem.ToString();
            API_RETURN_CODE code = LaserDevice.Connect(portName);
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                return code;
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
            return API_RETURN_CODE.API_SUCCESS;
        }

        #region controlers event

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitVariables();
            ConfigDevice();
            InitLoadControlers();
            UpdateControlers();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // release scantask
            ReleaseScanTask();

            // close all activated laser channels
            ReleaseLaserDevice();

            // close  usb dac device
            ReleaseUsbDac();
        }

        private void btnLaserConnect_Click(object sender, EventArgs e)
        {
            API_RETURN_CODE code = ConfigLaserDevice();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("连接激光器失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
            }
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

    }
}
