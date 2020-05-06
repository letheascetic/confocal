using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Confocal;
using log4net;

namespace confocal_test
{
    using HCONFIG = System.IntPtr;
    using HATS = System.IntPtr;

    public partial class FormEntry : Form
    {
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
        /************************************************************************************/

        public FormEntry()
        {
            InitializeComponent();
        }

        private void btnCfg_Click(object sender, EventArgs e)
        {
            int val = Config.Test(6);
            HCONFIG pConfig = Config.GetConfig();
            val = Config.GetChannelNum();

            float amp = Config.GetCrsAmplitude(pConfig);
            Config.SetCrsAmplitude(pConfig, 1.2f);
            amp = Config.GetCrsAmplitude(pConfig);

            float power = Config.GetLaserPower(pConfig, 0);
            Config.SetLaserPower(pConfig, 0, 55.0f);
            power = Config.GetLaserPower(pConfig, 0);

            val = Ats.Test(0);
            int num = Ats.Find();
            
            HATS pAts = Ats.Open();
            Ats.Close();

            Board_Info board_Info = new Board_Info();
            Ats.AtsGetBoardInfo(pAts, ref board_Info);
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            FormMain form = new FormMain();
            form.ShowDialog();
        }

        private void btnLaser_Click(object sender, EventArgs e)
        {
            FormLaser form = new FormLaser();
            form.ShowDialog();
        }
    }
}
