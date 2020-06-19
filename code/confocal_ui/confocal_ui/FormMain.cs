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

        private void FormMain_Load(object sender, EventArgs e)
        {
            Init();
            InitLoadControls();
        }

        private void Init()
        {
            m_config = Config.GetConfig();
            m_params = Params.GetParams();
            m_scheduler = Scheduler.CreateInstance();

            m_scheduler.ScanTaskCreated += new ScanTaskEventHandler(ScanTaskCreatedHandler);

        }

        private void InitLoadControls()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text += version;
            Logger.Info(string.Format("get software version: [{0}]", version));

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

    }
}
