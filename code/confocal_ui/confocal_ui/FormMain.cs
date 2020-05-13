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

namespace confocal
{
    public partial class FormMain : Form
    {
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
        /************************************************************************************/
        FormScanControl m_pFormSC = new FormScanControl();
        FormROI m_pFormROI = new FormROI();
        FormMeasurement m_pFormMeas = new FormMeasurement();
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

            m_pFormSC.Show(this.dockPanel, DockState.DockRight);
            //m_pFormROI.Show(m_pFormSC.Pane, DockAlignment.Left, 0.5);
            //m_pFormROI.Show(m_pFormSC.Pane, DockAlignment.Bottom, 0.5);
            //m_pFormMeas.Show(this.dockPanel, DockState.DockRight);
        }
    }
}
