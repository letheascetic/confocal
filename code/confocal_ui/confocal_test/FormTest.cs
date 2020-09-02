using confocal_core;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
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
        private Bitmap m_bmp;
        private byte m_bmpValue;

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
            m_bmp = new Bitmap(100, 100, PixelFormat.Format24bppRgb);
            pictureBox.Image = m_bmp;
            m_bmpValue = 0;
            timer.Start();
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

        private void btnArrayTest_Click(object sender, EventArgs e)
        {
            int[] source = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            int[] seg = source.TakeWhile(i => i > 3).ToArray();

            seg[0] = 90;
            seg[1] = 91;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            m_bmpValue = (byte)(m_bmpValue + 50 > byte.MaxValue ? 0 : m_bmpValue + 50);
            byte[] data = Enumerable.Repeat<byte>(m_bmpValue, 100 * 100 * 3).ToArray<byte>();
            Bitmap canvas = new Bitmap(100, 100, PixelFormat.Format24bppRgb);
            Rectangle lockBitsZoom = new Rectangle(0, 0, m_bmp.Width, m_bmp.Height);
            BitmapData canvasData = canvas.LockBits(lockBitsZoom, ImageLockMode.WriteOnly, canvas.PixelFormat);
            Marshal.Copy(data, 0, canvasData.Scan0, data.Length);
            canvas.UnlockBits(canvasData);

            //Graphics x = Graphics.FromImage(pictureBox.Image);
            //x.DrawImage(canvas, 0, 0);
            //pictureBox.Update();
            pictureBox.Image = canvas;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            short[] x = new short[] { short.MaxValue, short.MinValue, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] y = new int[10];
            Array.Copy(x, y, 10);
            y[0] = 5;
        }
    }
}
