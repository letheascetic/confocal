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
            string[] devices = SysConfig.GetDeviceNames();
            lbInfo.Visible = devices.Length > 0 ? false : true;

            string[] aoChannels = SysConfig.GetAoChannels();

            cbxXGalvo.Items.AddRange(aoChannels);
            cbxXGalvo.SelectedIndex = cbxXGalvo.FindString(m_sysConfig.GetXGalvoAoChannel());
            cbxYGalvo.Items.AddRange(aoChannels);
            cbxYGalvo.SelectedIndex = cbxYGalvo.FindString(m_sysConfig.GetYGalvoAoChannel());
            cbxY2Galvo.Items.AddRange(aoChannels);
            cbxY2Galvo.SelectedIndex = cbxY2Galvo.FindString(m_sysConfig.GetY2GalvoAoChannel());
            
            nudResponseTime.Value = (Decimal)m_config.GetGalvResponseTime();
            nudFieldSize.Value = (Decimal)m_config.GetScanFieldSize();
            tbxCalibrationV.Text = m_config.GetScanCalibrationVoltage().ToString();
            tbxCurveCoff.Text = m_config.GetScanCurveCoff().ToString();

            rbtnApd.Checked = m_sysConfig.GetAcqDevice() == ACQ_DEVICE.APD ? true : false;
            tbcAcq.SelectedIndex = rbtnApd.Checked ? 0 : 1;

            string[] doLines = SysConfig.GetDoLines();
            cbxAcqTrigger.Items.AddRange(doLines);
            cbxAcqTrigger.SelectedIndex = cbxAcqTrigger.FindString(m_sysConfig.GetAcqTriggerDoLine());

            string[] startSyncSignals = SysConfig.GetStartSyncSignals();
            cbxStartSync.Items.AddRange(startSyncSignals);
            cbxStartSync.SelectedIndex = cbxStartSync.FindString(m_sysConfig.GetStartSyncSignal());

            string[] ciChannels = SysConfig.GetCiChannels();
            cbxApd405Ci.Items.AddRange(ciChannels);
            cbxApd488Ci.Items.AddRange(ciChannels);
            cbxApd561Ci.Items.AddRange(ciChannels);
            cbxApd640Ci.Items.AddRange(ciChannels);

            cbxApd405Ci.SelectedIndex = cbxApd405Ci.FindString(m_sysConfig.GetApdCiChannel(CHAN_ID.WAVELENGTH_405_NM));
            cbxApd488Ci.SelectedIndex = cbxApd488Ci.FindString(m_sysConfig.GetApdCiChannel(CHAN_ID.WAVELENGTH_488_NM));
            cbxApd561Ci.SelectedIndex = cbxApd561Ci.FindString(m_sysConfig.GetApdCiChannel(CHAN_ID.WAVELENGTH_561_NM));
            cbxApd640Ci.SelectedIndex = cbxApd640Ci.FindString(m_sysConfig.GetApdCiChannel(CHAN_ID.WAVELENGTH_640_NM));

            string[] pfis = SysConfig.GetPFIs();
            cbxApd405CiSrc.Items.AddRange(pfis);
            cbxApd488CiSrc.Items.AddRange(pfis);
            cbxApd561CiSrc.Items.AddRange(pfis);
            cbxApd640CiSrc.Items.AddRange(pfis);

            cbxApd405CiSrc.SelectedIndex = cbxApd405CiSrc.FindString(m_sysConfig.GetApdCiSrcPfi(CHAN_ID.WAVELENGTH_405_NM));
            cbxApd488CiSrc.SelectedIndex = cbxApd488CiSrc.FindString(m_sysConfig.GetApdCiSrcPfi(CHAN_ID.WAVELENGTH_488_NM));
            cbxApd561CiSrc.SelectedIndex = cbxApd561CiSrc.FindString(m_sysConfig.GetApdCiSrcPfi(CHAN_ID.WAVELENGTH_561_NM));
            cbxApd640CiSrc.SelectedIndex = cbxApd640CiSrc.FindString(m_sysConfig.GetApdCiSrcPfi(CHAN_ID.WAVELENGTH_640_NM));

            cbxApd405CiGate.Items.AddRange(pfis);
            cbxApd488CiGate.Items.AddRange(pfis);
            cbxApd561CiGate.Items.AddRange(pfis);
            cbxApd640CiGate.Items.AddRange(pfis);

            cbxApd405CiGate.SelectedIndex = cbxApd405CiGate.FindString(m_sysConfig.GetApdCiGatePfi(CHAN_ID.WAVELENGTH_405_NM));
            cbxApd488CiGate.SelectedIndex = cbxApd488CiGate.FindString(m_sysConfig.GetApdCiGatePfi(CHAN_ID.WAVELENGTH_488_NM));
            cbxApd561CiGate.SelectedIndex = cbxApd561CiGate.FindString(m_sysConfig.GetApdCiGatePfi(CHAN_ID.WAVELENGTH_561_NM));
            cbxApd640CiGate.SelectedIndex = cbxApd640CiGate.FindString(m_sysConfig.GetApdCiGatePfi(CHAN_ID.WAVELENGTH_640_NM));

            string[] aiChannels = SysConfig.GetAiChannels();
            cbxPmt405Ai.Items.AddRange(aiChannels);
            cbxPmt488Ai.Items.AddRange(aiChannels);
            cbxPmt561Ai.Items.AddRange(aiChannels);
            cbxPmt640Ai.Items.AddRange(aiChannels);

            cbxPmt405Ai.SelectedIndex = cbxPmt405Ai.FindString(m_sysConfig.GetPmtAiChannel(CHAN_ID.WAVELENGTH_405_NM));
            cbxPmt488Ai.SelectedIndex = cbxPmt488Ai.FindString(m_sysConfig.GetPmtAiChannel(CHAN_ID.WAVELENGTH_488_NM));
            cbxPmt561Ai.SelectedIndex = cbxPmt561Ai.FindString(m_sysConfig.GetPmtAiChannel(CHAN_ID.WAVELENGTH_561_NM));
            cbxPmt640Ai.SelectedIndex = cbxPmt640Ai.FindString(m_sysConfig.GetPmtAiChannel(CHAN_ID.WAVELENGTH_640_NM));

            cbxPmtTriggerIn.Items.AddRange(pfis);
            cbxPmtTriggerIn.SelectedIndex = cbxPmtTriggerIn.FindString(m_sysConfig.GetPmtTriggerInPfi());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UpdateSysConfig();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateSysConfig()
        {
            m_config.SetGalvResponseTime((double)nudResponseTime.Value);
            m_config.SetScanFieldSize((double)nudFieldSize.Value);

            m_sysConfig.SetXGalvoAoChannel(cbxXGalvo.SelectedItem.ToString());
            m_sysConfig.SetYGalvoAoChannel(cbxYGalvo.SelectedItem.ToString());
            m_sysConfig.SetY2GalvoAoChannel(cbxY2Galvo.SelectedItem.ToString());

            ACQ_DEVICE acqDevice = rbtnApd.Checked ? ACQ_DEVICE.APD : ACQ_DEVICE.PMT;
            m_sysConfig.SetAcqDevice(acqDevice);

            m_sysConfig.SetAcqTriggerDoLine(cbxAcqTrigger.SelectedItem.ToString());
            m_sysConfig.SetStartSyncSignal(cbxStartSync.SelectedItem.ToString());

            m_sysConfig.SetPmtAiChannel(CHAN_ID.WAVELENGTH_405_NM, cbxPmt405Ai.SelectedItem.ToString());
            m_sysConfig.SetPmtAiChannel(CHAN_ID.WAVELENGTH_488_NM, cbxPmt488Ai.SelectedItem.ToString());
            m_sysConfig.SetPmtAiChannel(CHAN_ID.WAVELENGTH_561_NM, cbxPmt561Ai.SelectedItem.ToString());
            m_sysConfig.SetPmtAiChannel(CHAN_ID.WAVELENGTH_640_NM, cbxPmt640Ai.SelectedItem.ToString());

            m_sysConfig.SetPmtTriggerInPfi(cbxPmtTriggerIn.SelectedItem.ToString());

            m_sysConfig.SetApdCiChannel(CHAN_ID.WAVELENGTH_405_NM, cbxApd405Ci.SelectedItem.ToString());
            m_sysConfig.SetApdCiChannel(CHAN_ID.WAVELENGTH_488_NM, cbxApd488Ci.SelectedItem.ToString());
            m_sysConfig.SetApdCiChannel(CHAN_ID.WAVELENGTH_561_NM, cbxApd561Ci.SelectedItem.ToString());
            m_sysConfig.SetApdCiChannel(CHAN_ID.WAVELENGTH_640_NM, cbxApd640Ci.SelectedItem.ToString());

            m_sysConfig.SetApdCiSrcPfi(CHAN_ID.WAVELENGTH_405_NM, cbxApd405CiSrc.SelectedItem.ToString());
            m_sysConfig.SetApdCiSrcPfi(CHAN_ID.WAVELENGTH_488_NM, cbxApd488CiSrc.SelectedItem.ToString());
            m_sysConfig.SetApdCiSrcPfi(CHAN_ID.WAVELENGTH_561_NM, cbxApd561CiSrc.SelectedItem.ToString());
            m_sysConfig.SetApdCiSrcPfi(CHAN_ID.WAVELENGTH_640_NM, cbxApd640CiSrc.SelectedItem.ToString());

            m_sysConfig.SetApdCiGatePfi(CHAN_ID.WAVELENGTH_405_NM, cbxApd405CiGate.SelectedItem.ToString());
            m_sysConfig.SetApdCiGatePfi(CHAN_ID.WAVELENGTH_488_NM, cbxApd488CiGate.SelectedItem.ToString());
            m_sysConfig.SetApdCiGatePfi(CHAN_ID.WAVELENGTH_561_NM, cbxApd561CiGate.SelectedItem.ToString());
            m_sysConfig.SetApdCiGatePfi(CHAN_ID.WAVELENGTH_640_NM, cbxApd640CiGate.SelectedItem.ToString());
        }

        private void ReleaseControlers()
        {
            cbxXGalvo.Items.Clear();
            cbxYGalvo.Items.Clear();
            cbxY2Galvo.Items.Clear();

            cbxAcqTrigger.Items.Clear();
            cbxStartSync.Items.Clear();

            cbxPmt405Ai.Items.Clear();
            cbxPmt488Ai.Items.Clear();
            cbxPmt561Ai.Items.Clear();
            cbxPmt640Ai.Items.Clear();
            cbxPmtTriggerIn.Items.Clear();

            cbxApd405Ci.Items.Clear();
            cbxApd488Ci.Items.Clear();
            cbxApd561Ci.Items.Clear();
            cbxApd640Ci.Items.Clear();

            cbxApd405CiSrc.Items.Clear();
            cbxApd488CiSrc.Items.Clear();
            cbxApd561CiSrc.Items.Clear();
            cbxApd640CiSrc.Items.Clear();

            cbxApd405CiGate.Items.Clear();
            cbxApd488CiGate.Items.Clear();
            cbxApd561CiGate.Items.Clear();
            cbxApd640CiGate.Items.Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            ReleaseControlers();
            InitControlers();
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
    }
}
