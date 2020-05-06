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

        }

        private void Init()
        {

        }

        private void InitControlers()
        {
            cbxPort.SelectedIndex = 0;
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
