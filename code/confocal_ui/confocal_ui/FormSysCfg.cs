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

namespace confocal_ui
{
    public partial class FormSysCfg : Form
    {
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
        /************************************************************************************/
        private Config m_config;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public FormSysCfg()
        {
            InitializeComponent();
        }

        private void FormSysCfg_Load(object sender, EventArgs e)
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
            nudResponseTime.Value = (Decimal)m_config.GetGalvResponseTime();
            nudFieldSize.Value = (Decimal)m_config.GetScanFieldSize();
            tbxCalibrationV.Text = m_config.GetScanCalibrationVoltage().ToString();
            tbxCurveCoff.Text = m_config.GetScanCurveCoff().ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_config.SetGalvResponseTime((double)nudResponseTime.Value);
            m_config.SetScanFieldSize((double)nudFieldSize.Value);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
