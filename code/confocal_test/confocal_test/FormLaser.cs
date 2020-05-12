using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Confocal;

namespace confocal_test
{
    public partial class FormLaser : Form
    {
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
        /************************************************************************************/

        public FormLaser()
        {
            InitializeComponent();
        }

        private void FormLaser_Load(object sender, EventArgs e)
        {
            Init();
            InitControlers();
        }

        private void Init()
        {
            
        }

        private void InitControlers()
        {
            cbxPort.SelectedIndex = Laser.PortName() == null ? 0 : cbxPort.FindString(Laser.PortName());
            cbxPort.Enabled = Laser.IsConnected() ? false : true;

            chbx405.Checked = Laser.GetChannelStatus(Laser.LASER_CHAN_ID_405_NM) == Laser.LASER_CHAN_SWITCH_ON ? true : false;
            chbx488.Checked = Laser.GetChannelStatus(Laser.LASER_CHAN_ID_488_NM) == Laser.LASER_CHAN_SWITCH_ON ? true : false;
            chbx561.Checked = Laser.GetChannelStatus(Laser.LASER_CHAN_ID_561_NM) == Laser.LASER_CHAN_SWITCH_ON ? true : false;
            chbx640.Checked = Laser.GetChannelStatus(Laser.LASER_CHAN_ID_640_NM) == Laser.LASER_CHAN_SWITCH_ON ? true : false;

            tbP405.Value = (int)Laser.GetChannelPower(Laser.LASER_CHAN_ID_405_NM);
            tbP488.Value = (int)Laser.GetChannelPower(Laser.LASER_CHAN_ID_488_NM);
            tbP561.Value = (int)Laser.GetChannelPower(Laser.LASER_CHAN_ID_561_NM);
            tbP640.Value = (int)Laser.GetChannelPower(Laser.LASER_CHAN_ID_640_NM);
        }


        private void btnOpen_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void chbx405_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
