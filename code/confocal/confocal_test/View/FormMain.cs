using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace confocal_test.View
{
    public partial class FormMain : Form
    {
        private Mat scanImage;

        public FormMain()
        {
            InitializeComponent();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormArea a = new FormArea();
            a.MdiParent = this;
            a.Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            scanImage = new Mat(512, 512, DepthType.Cv8U, 3);
            imageBox1.Image = scanImage;
        }
    }
}
