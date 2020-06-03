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
    public partial class FormShowBox : DockContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private Config m_config;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public FormShowBox()
        {
            InitializeComponent();
        }

        private void FormShowBox_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
        }

        private void InitVariables()
        {
            m_config = Config.GetConfig();
        }

        private void InitControlers()
        {
            tbxResponseTime.Text = m_config.GetGalvResponseTime().ToString();
            tbxFieldSize.Text = m_config.GetScanFieldSize().ToString();
            tbxCalibrationV.Text = m_config.GetScanCalibrationVoltage().ToString();
            tbxCurveCoff.Text = m_config.GetScanCurveCoff().ToString();


        }

    }
}
