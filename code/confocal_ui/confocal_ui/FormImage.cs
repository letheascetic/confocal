using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace confocal_ui
{
    public partial class FormImage : DockContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////


        public FormImage()
        {
            InitializeComponent();
        }

        private void FormImage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(400, 400, PixelFormat.Format24bppRgb);
                pictureBox.Image = bmp;
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Jpeg Files (*.)|*.Png";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!dialog.FileName.EndsWith(".Png"))
                    {
                        MessageBox.Show("文件名格式有误!");
                        return;
                    }
                    string filename = dialog.FileName;
                    bmp.Save(filename, ImageFormat.Bmp);
                }
            }
            catch (Exception err)
            {
                Logger.Error(string.Format("exception: [{0}]", err));
            }
        }
    }
}
