using confocal_base;
using confocal_core;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace confocal_ui
{
    public partial class FormDisplay : DockContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScanTask m_scanTask;
        private Params m_params;
        private Config m_config;
        private Bitmap[] m_bitmapArr;
        private Dictionary<string, CHAN_ID> m_activatedChannelDict;
        private int m_selectedChannelIndex;         // 当前显示图像对应激光通道的索引
        private int m_xScanPoints;                  // X扫描像素点
        private int m_yScanPoints;                  // Y扫描像素点
        private Point m_mousePosition;              // 鼠标在图像区域的位置[当前显示图像中的位置]
        private Point m_imagePosition;              // 鼠标在图像区域的位置[扫描像素图像中的位置]
        private Rectangle m_imageRectangle;         // Image在PictureBox中的位置
        private SizeF m_imageScaleRatio;            // 显示图像与扫描图像的缩放比例
        ///////////////////////////////////////////////////////////////////////////////////////////
        public int FormId { get { return m_scanTask.TaskId; } }
        public string FormName { get { return m_scanTask.TaskName; } }
        ///////////////////////////////////////////////////////////////////////////////////////////
        public FormDisplay(ScanTask scanTask)
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景. 
            SetStyle(ControlStyles.DoubleBuffer, true);         // 双缓冲  
            LoadScanTask(scanTask);
        }

        public void LoadScanTask(ScanTask scanTask)
        {
            m_scanTask = scanTask;
        }

        public void ScanTaskCreated()
        {
            Logger.Info(string.Format("FormImage scan task[{0}|{1}] created.", m_scanTask.TaskId, m_scanTask.TaskName));
            UpdateVariables();
            UpdateControlers();
            this.Activate();
        }

        public void ScanTaskStrated()
        {
            Logger.Info(string.Format("FormImage scan task[{0}|{1}] started.", m_scanTask.TaskId, m_scanTask.TaskName));
            string status = m_scanTask.Scannning ? "扫描中" : "暂停";
            this.Text = string.Format(m_scanTask.TaskName, " ", status);
            m_timer.Start();
        }

        public void ScanTaskStopped()
        {
            Logger.Info(string.Format("FormImage scan task[{0}|{1}] stopped.", m_scanTask.TaskId, m_scanTask.TaskName));
            string status = m_scanTask.Scannning ? "扫描中" : "暂停";
            this.Text = string.Format(m_scanTask.TaskName, " ", status);
            m_timer.Stop();
        }

        public void ActivatedChannelChanged()
        {
            Logger.Info(string.Format("FormImage scan task[{0}|{1}] activated channel channged.", m_scanTask.TaskId, m_scanTask.TaskName));
        }

        private void InitVariables()
        {
            m_config = Config.GetConfig();
            m_params = Params.GetParams();

            int channelNum = m_config.GetChannelNum();
            m_bitmapArr = new Bitmap[channelNum];

            for (int i = 0; i < channelNum; i++)
            {
                m_bitmapArr[i] = new Bitmap(m_config.GetScanXPoints(), m_config.GetScanYPoints(), PixelFormat.Format24bppRgb);
            }

            m_activatedChannelDict = new Dictionary<string, CHAN_ID>();
            m_selectedChannelIndex = -1;

            m_cursorTimer.Start();
        }

        private void InitControlers()
        {
            // imageBox.Image = m_bitmapArr[0];
        }

        private void UpdateVariables()
        {
            for (int i = 0; i < m_config.GetChannelNum(); i++)
            {
                m_bitmapArr[i] = new Bitmap(m_config.GetScanXPoints(), m_config.GetScanYPoints(), PixelFormat.Format24bppRgb);
            }

            m_activatedChannelDict.Clear();
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_405_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_activatedChannelDict.Add("405nm", CHAN_ID.WAVELENGTH_405_NM);
            }
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_488_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_activatedChannelDict.Add("488nm", CHAN_ID.WAVELENGTH_488_NM);
            }
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_561_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_activatedChannelDict.Add("561nm", CHAN_ID.WAVELENGTH_561_NM);
            }
            if (m_config.GetLaserSwitch(CHAN_ID.WAVELENGTH_640_NM) == LASER_CHAN_SWITCH.ON)
            {
                m_activatedChannelDict.Add("640nm", CHAN_ID.WAVELENGTH_640_NM);
            }

            m_xScanPoints = m_config.GetScanXPoints();
            m_yScanPoints = m_config.GetScanYPoints();

            //PropertyInfo info = imageBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            //m_imageRectangle = (Rectangle)info.GetValue(imageBox, null);
            m_imageRectangle = imageBox.DisplayRectangle;
            m_imageScaleRatio = new SizeF((float)m_imageRectangle.Width / m_xScanPoints, (float)m_imageRectangle.Height / m_yScanPoints);
        }

        private void UpdateControlers()
        {
            this.Text = m_scanTask.TaskName;
            this.lbPixelSize.Text = string.Format("{0} um/px", m_params.PixelSize.ToString("F2"));
            this.lbScanPixels.Text = string.Format("{0} x {1} pixels", m_config.GetScanXPoints(), m_config.GetScanYPoints());
            this.lbBitDepth.Text = "16 bits";
            this.lbFps.Text = string.Format("{0} fps", m_params.Fps.ToString("F2"));

            this.cbxSelect.BeginUpdate();
            this.cbxSelect.Items.Clear();
            this.cbxSelect.Items.AddRange(m_activatedChannelDict.Keys.ToArray());
            this.cbxSelect.SelectedIndex = 0;
            this.cbxSelect.EndUpdate();

            UpdateRTControlers();
        }

        private void UpdateRTControlers()
        {
            this.lbFrame.Text = string.Format("NO. {0} frame", m_scanTask.GetScanInfo().CurrentFrame);
            this.lbTimeSpan.Text = string.Format("{0} seconds", m_scanTask.GetScanInfo().TimeSpan.ToString("F1"));
        }

        private void UpdateCurrentPosition()
        {
            this.m_mousePosition = imageBox.PointToClient(MousePosition);
            if (!imageBox.Bounds.Contains(m_mousePosition))
            {
                return;
            }

            if (imageBox.SizeMode == PictureBoxSizeMode.AutoSize)
            {
                this.m_imagePosition = this.m_mousePosition;
                // int pixelIndex = m_xScanPoints * m_imagePosition.Y + m_imagePosition.X;
                // m_scanTask.GetScanData().ScanImage.GrayMat[m_selectedChannelIndex].get
                // int pixelValue = m_scanTask.GetScanData().ScanImage.Data[m_selectedChannelIndex][pixelIndex];
                dynamic pixelValue = MatExtension.GetValue(m_scanTask.GetScanData().ScanImage.GrayMat[m_selectedChannelIndex], m_imagePosition.Y, m_imagePosition.X);
                this.lbCurrent.Text = string.Format("[{0}, ({1}, {2})]", pixelValue, this.m_imagePosition.X, this.m_imagePosition.Y);
            }
            else
            {
                if (m_imageRectangle.Contains(m_mousePosition))
                {
                    Point posInImage = new Point(m_mousePosition.X - m_imageRectangle.Left, m_mousePosition.Y - m_imageRectangle.Y);
                    m_imagePosition.X = (int)(posInImage.X / m_imageScaleRatio.Width);
                    m_imagePosition.Y = (int)(posInImage.Y / m_imageScaleRatio.Height);
                    // int pixelIndex = m_xScanPoints * m_imagePosition.Y + m_imagePosition.X;
                    // int pixelValue = m_scanTask.GetScanData().ScanImage.Data[m_selectedChannelIndex][pixelIndex];
                    dynamic pixelValue = MatExtension.GetValue(m_scanTask.GetScanData().ScanImage.GrayMat[m_selectedChannelIndex], m_imagePosition.Y, m_imagePosition.X);
                    this.lbCurrent.Text = string.Format("[{0}, ({1}, {2})]", pixelValue, this.m_imagePosition.X, this.m_imagePosition.Y);
                }
            }
        }

        private void FormDisplay_Load(object sender, EventArgs e)
        {
            InitVariables();
            InitControlers();
        }

        private void btnDisplayCenter_Click(object sender, EventArgs e)
        {
            imageBox.Dock = DockStyle.None;
            imageBox.SizeMode = PictureBoxSizeMode.AutoSize;
            // PropertyInfo info = imageBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            // m_imageRectangle = (Rectangle)info.GetValue(imageBox, null);
            m_imageRectangle = imageBox.DisplayRectangle;
            m_imageScaleRatio = new SizeF((float)m_imageRectangle.Width / m_xScanPoints, (float)m_imageRectangle.Height / m_yScanPoints);
        }

        private void btnDisplayZoom_Click(object sender, EventArgs e)
        {
            imageBox.Dock = DockStyle.Fill;
            imageBox.SizeMode = PictureBoxSizeMode.Zoom;
            // PropertyInfo info = imageBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            // m_imageRectangle = (Rectangle)info.GetValue(imageBox, null);
            m_imageRectangle = imageBox.DisplayRectangle;
            m_imageScaleRatio = new SizeF((float)m_imageRectangle.Width / m_xScanPoints, (float)m_imageRectangle.Height / m_yScanPoints);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Bitmap bmp = imageBox.Image.Bitmap;
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
                bmp.Save(filename, ImageFormat.Png);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (m_selectedChannelIndex < 0)
            {
                return;
            }

            imageBox.Image = m_scanTask.GetScanData().ScanImage.GrayMat[m_selectedChannelIndex];
            // pbxImage.Image = m_scanTask.GetScanData().ScanImage.GetDisplayImage(m_selectedChannelIndex, ref m_bitmapArr[m_selectedChannelIndex]);
            UpdateRTControlers();
        }

        private void cbxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSelect.SelectedIndex < 0)
            {
                return;
            }

            m_activatedChannelDict.TryGetValue(cbxSelect.SelectedItem.ToString(), out CHAN_ID id);
            m_selectedChannelIndex = (int)id;
            imageBox.Image = m_scanTask.GetScanData().ScanImage.GrayMat[m_selectedChannelIndex];
        }

        private void m_cursorTimer_Tick(object sender, EventArgs e)
        {
            UpdateCurrentPosition();
        }

    }
}
