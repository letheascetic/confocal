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
        FormScan m_pFormScan = new FormScan();
        FormROI m_pFormROI = new FormROI();
        FormMeas m_pFormMeas = new FormMeas();
        FormShowBox m_pFormShowBox = new FormShowBox();
        FormImage m_pFormImage = new FormImage();
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

        }

        private void InitLoadControls()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text += version;
            Logger.Info(string.Format("get software version: [{0}]", version));

            m_pFormScan.Show(this.dockPanel, DockState.DockRight);
            //m_pFormROI.Show(this.dockPanel, DockState.DockRight);
            m_pFormShowBox.Show(this.dockPanel, DockState.DockLeft);
            m_pFormImage.Show(this.dockPanel, DockState.Document);
            //m_pFormROI.Show(m_pFormSC.Pane, DockAlignment.Left, 0.5);
            //m_pFormROI.Show(m_pFormSC.Pane, DockAlignment.Bottom, 0.5);
            //m_pFormMeas.Show(this.dockPanel, DockState.DockRight);
        }
    }
}
