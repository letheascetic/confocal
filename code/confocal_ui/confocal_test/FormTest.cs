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

namespace confocal_test
{
    public partial class FormTest : Form
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private Config m_config;

        public FormTest()
        {
            InitializeComponent();
        }

        private void btnLaserConnect_Click(object sender, EventArgs e)
        {
            string portName = cbxLaser.SelectedItem.ToString();
            API_RETURN_CODE code = LaserDevice.Connect(portName);
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("连接激光器失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
            }
        }

        private void btnLaserRelease_Click(object sender, EventArgs e)
        {
            API_RETURN_CODE code = LaserDevice.Release();
            if (code != API_RETURN_CODE.API_SUCCESS)
            {
                MessageBox.Show(string.Format("断开激光器连接失败，失败码: [0x{0}][{1}].", ((int)code).ToString("X"), code));
            }
        }

        private void FormTest_Load(object sender, EventArgs e)
        {

        }

        private void chbx405_CheckedChanged(object sender, EventArgs e)
        {
            if (LaserDevice.IsConnected())
            {
                if (chbx405.Checked)
                {
                    LaserDevice.OpenChannel(CHAN_ID.WAVELENGTH_405_NM);
                }
                else
                {
                    LaserDevice.CloseChannel(CHAN_ID.WAVELENGTH_405_NM);
                }
            }
        }

        private void chbx488_CheckedChanged(object sender, EventArgs e)
        {
            if (LaserDevice.IsConnected())
            {
                LaserDevice.OpenChannel(CHAN_ID.WAVELENGTH_488_NM);
            }
        }



    }
}
