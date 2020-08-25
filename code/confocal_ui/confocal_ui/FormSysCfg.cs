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
        private SysConfig m_sysConfig;
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
            m_sysConfig = SysConfig.GetSysConfig();
        }

        private void InitControlers()
        {
            string[] aoChannels = SysConfig.GetAoChannels();

            cbxXGalvo.Items.AddRange(aoChannels);
            cbxYGalvo.Items.AddRange(aoChannels);
            cbxY2Galvo.Items.AddRange(aoChannels);

            nudResponseTime.Value = (Decimal)m_config.GetGalvResponseTime();
            nudFieldSize.Value = (Decimal)m_config.GetScanFieldSize();
            tbxCalibrationV.Text = m_config.GetScanCalibrationVoltage().ToString();
            tbxCurveCoff.Text = m_config.GetScanCurveCoff().ToString();

            rbtnApd.Checked = m_sysConfig.GetAcqDevice() == ACQ_DEVICE.APD ? true : false;
            tbcAcq.SelectedIndex = rbtnApd.Checked ? 0 : 1;

            string[] ciChannels = SysConfig.GetCiChannels();
            cbxApd405Ci.Items.AddRange(ciChannels);
            cbxApd488Ci.Items.AddRange(ciChannels);
            cbxApd561Ci.Items.AddRange(ciChannels);
            cbxApd640Ci.Items.AddRange(ciChannels);

            string[] aiChannels = SysConfig.GetAiChannels();
            cbxPmt405Ai.Items.AddRange(aiChannels);
            cbxPmt488Ai.Items.AddRange(aiChannels);
            cbxPmt561Ai.Items.AddRange(aiChannels);
            cbxPmt640Ai.Items.AddRange(aiChannels);
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
