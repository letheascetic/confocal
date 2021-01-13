using C1.Win.C1Ribbon;
using confocal_core;
using confocal_core.Model;
using confocal_core.ViewModel;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace confocal_ui.View
{
    public partial class FormScanArea : C1RibbonForm
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        public event ScanPixelChangedEventHandler ScanPixelChangedEvent;
        public event ScanRangeChangedEventHandler ScanRangeChangedEvent;
        ///////////////////////////////////////////////////////////////////////////////////////////

        private Point mMouseDownPosition;    // 鼠标在图像区域按下时的坐标

        private Point mMousePosition;        // 鼠标在图像区域的坐标
        private Point mPixelPosition;        // 鼠标在图像区域的像素坐标

        private Rectangle mCoordinate;       // 控件中的扫描范围
        private Rectangle mScanPixelRange;   // 图像中的扫描范围
        private RectangleF mScanRange;       // 视场中的扫描范围（包含未确定状态）

        private Bitmap mScanAreaBmp;         // 扫描范围图层的Bitmap
        private Graphics mScanAreaGra;       // 扫描范围图层的画图句柄[在Bitmap上的画图操作不会丢失，直接在pbxScanArea上画图会丢失]

        private ScanAreaViewModel mScanAreaViewModel;

        public ScanAreaViewModel ScanAreaVM
        {
            get { return mScanAreaViewModel; }
        }

        public FormScanArea()
        {
            InitializeComponent();
        }

        public API_RETURN_CODE ScanPixelChangedHandler(ScanPixelModel scanPixel)
        {
            API_RETURN_CODE code = mScanAreaViewModel.ScanPixelChangeCommand(scanPixel);
            cbxScanPixel.SelectedItem = mScanAreaViewModel.SelectedScanPixel;
            return code;
        }

        public API_RETURN_CODE ScanPixelDwellChangedHandler(ScanPixelDwellModel scanPixelDwell)
        {
            return mScanAreaViewModel.ScanPixelDwellChangeCommand(scanPixelDwell);
        }

        private void Initialize()
        {
            pbxScanArea.Parent = pictureBox;
            pbxScanArea.Location = new Point(0, 0);
            pbxScanArea.Size = pictureBox.Size;

            contextMenu.CommandLinks.Add(cmdLinkFullRange);
            contextMenu.CommandLinks.Add(cmdLinkLastScanRange);
            contextMenu.CommandLinks.Add(cmdLinkConfirm);

            mScanAreaViewModel = new ScanAreaViewModel();
            mScanRange = mScanAreaViewModel.SelectedScanArea.ScanRange;
            mScanPixelRange = mScanAreaViewModel.ScanRangeToScanPixelRange(mScanRange);
            mCoordinate = ScanPixelRangeToCoordinate(mScanPixelRange);

            mMousePosition = new Point();
            mPixelPosition = new Point();

            pictureBox.Image = mScanAreaViewModel.ScanImage;
            mScanAreaBmp = new Bitmap(pbxScanArea.ClientSize.Width, pbxScanArea.ClientSize.Height);
            mScanAreaGra = Graphics.FromImage(mScanAreaBmp);
            mScanAreaGra.Clear(Color.Transparent);
            pbxScanArea.Image = mScanAreaBmp;
            
            DrawScanArea(mCoordinate, mScanAreaGra, pbxScanArea);
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            cbxScanPixel.ChangeCommitted += ScanPixelChanged;
            pbxScanArea.MouseEnter += MouseEnterImage;
            pbxScanArea.MouseLeave += MouseLeaveImage;
            pbxScanArea.MouseWheel += ScanRangeZoomed;
        }

        /// <summary>
        /// 设置数据绑定
        /// </summary>
        private void SetDataBindings()
        {
            // 扫描像素
            cbxScanPixel.DataSource = mScanAreaViewModel.ScanPixelList;
            cbxScanPixel.DisplayMember = "Text";
            cbxScanPixel.ValueMember = "Data";
            cbxScanPixel.SelectedItem = mScanAreaViewModel.SelectedScanPixel;

            // 显示的数据
            lbScanWidth.DataBindings.Add("Text", mScanAreaViewModel, "ScanWidth");
            lbScanHeight.DataBindings.Add("Text", mScanAreaViewModel, "ScanHeight");
            lbPixelDwellValue.DataBindings.Add("Text", mScanAreaViewModel, "ScanPixelDwell", true, DataSourceUpdateMode.OnPropertyChanged, null, "0.0 us");
            lbPixelSizeValue.DataBindings.Add("Text", mScanAreaViewModel, "ScanPixelSize", true, DataSourceUpdateMode.OnPropertyChanged, null, "0.00 um/pxl");
        }

        private void DrawScanArea(Rectangle rectangle, Graphics graphics, Control control)
        {
            control.Invalidate();
            control.Update();
            graphics.Clear(Color.Transparent);
            graphics.DrawRectangle(new Pen(Color.Red, 5), rectangle);
        }

        /// <summary>
        /// 坐标转换成像素范围
        /// </summary>
        /// <param name="scanCoordinateRange"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        private Rectangle CoordinateToScanPixelRange(Rectangle scanCoordinate)
        {
            float ratio = (float)mScanAreaViewModel.ScanWidth / pictureBox.ClientSize.Width;
            int x = (int)(scanCoordinate.X * ratio);
            int y = (int)(scanCoordinate.Y * ratio);
            int width = (int)(scanCoordinate.Width * ratio);
            int height = (int)(scanCoordinate.Height * ratio);
            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// 像素范围转换成坐标
        /// </summary>
        /// <param name="scanPixelRange"></param>
        /// <returns></returns>
        private Rectangle ScanPixelRangeToCoordinate(Rectangle scanPixelRange)
        {
            float ratio = (float)mScanAreaViewModel.ScanWidth / pictureBox.ClientSize.Width;
            int x = (int)(scanPixelRange.X / ratio);
            int y = (int)(scanPixelRange.Y / ratio);
            int width = (int)(scanPixelRange.Width / ratio);
            int height = (int)(scanPixelRange.Height / ratio);
            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormScanAreaLoad(object sender, EventArgs e)
        {
            Initialize();
            SetDataBindings();
            RegisterEvents();
        }

        private void ScanPixelChanged(object sender, EventArgs e)
        {
            ScanPixelModel scanPixel = (ScanPixelModel)cbxScanPixel.SelectedItem;
            if (scanPixel.IsEnabled)
            {
                return;
            }
            mScanAreaViewModel.ScanPixelChangeCommand(scanPixel);
            if (ScanPixelChangedEvent != null)
            {
                ScanPixelChangedEvent.Invoke(scanPixel);
            }
        }

        private void MouseEnterImage(object sender, EventArgs e)
        {
            timer.Start();
            this.Cursor = Cursors.Hand;
        }

        private void MouseLeaveImage(object sender, EventArgs e)
        {
            timer.Stop();
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 计算鼠标所在像素点在图像中的坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            //mMousePosition = pictureBox.PointToClient(MousePosition);
            //mPixelPosition.X = (int)((float)mScanAreaViewModel.ScanWidth / pictureBox.Width * mMousePosition.X);
            //mPixelPosition.Y = (int)((float)mScanAreaViewModel.ScanHeight / pictureBox.Height * mMousePosition.Y);
            //lbPixelPosition.Text = string.Format("[{0}:{1}]", mPixelPosition.X, mPixelPosition.Y);
            //Logger.Info(string.Format("Mouse Position [{0}:{1}], Pixel Position [{2}:{3}].",
            //    mMousePosition.X, mMousePosition.Y, mPixelPosition.X, mPixelPosition.Y));
        }

        private void ScanRangeZoomed(object sender, MouseEventArgs e)
        {
            Rectangle coordinate = mCoordinate;
            if (e.Delta > 0)
            {
                coordinate.Width += 5;
                coordinate.Height += 5;
            }
            else
            {
                coordinate.Width -= 5;
                coordinate.Height -= 5;
            }
            if (coordinate.Right > pbxScanArea.Right || coordinate.Bottom > pbxScanArea.Bottom)
            {
                return;
            }
            else if (coordinate.Width < 20 || coordinate.Height < 20)
            {
                return;
            }
            mCoordinate = coordinate;
            Logger.Info(string.Format("Coordinate [{0}].", mCoordinate.ToString()));
            DrawScanArea(mCoordinate, mScanAreaGra, pbxScanArea);
        }

        private void ScanRangeMoved(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Logger.Info(string.Format("Coordinate [{0}].", mCoordinate.ToString()));
                if (mCoordinate.Contains(mMouseDownPosition))
                {
                    Logger.Info(string.Format("Mouse Down Position [{0}].", mMouseDownPosition));
                    Rectangle coordinate = mCoordinate;
                    coordinate.X += e.X - mMouseDownPosition.X;
                    coordinate.Y += e.Y - mMouseDownPosition.Y;
                    if (coordinate.Left < pbxScanArea.Left || coordinate.Right > pbxScanArea.Right || coordinate.Top < pbxScanArea.Top || coordinate.Bottom > pbxScanArea.Bottom)
                    {
                        return;
                    }
                    mMouseDownPosition = e.Location;
                    mCoordinate = coordinate;
                    DrawScanArea(mCoordinate, mScanAreaGra, pbxScanArea);
                }
            }
        }

        private void MouseDownChanged(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mMouseDownPosition = e.Location;
                Logger.Info(string.Format("Mouse Down Position [{0}].", mMouseDownPosition));
            }
            else if (e.Button == MouseButtons.Right)
            {
                contextMenu.ShowContextMenu(pbxScanArea, e.Location);
            }
        }

        private void FullRangeClick(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            mScanRange = mScanAreaViewModel.SelectedScanArea.ScanRange;
            mScanPixelRange = mScanAreaViewModel.ScanRangeToScanPixelRange(mScanRange);
            mCoordinate = ScanPixelRangeToCoordinate(mScanPixelRange);
            DrawScanArea(mCoordinate, mScanAreaGra, pbxScanArea);
            DrawScanArea(mCoordinate, mScanAreaGra, pbxScanArea);
        }

        private void LastScanRangeClick(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            mScanRange = mScanAreaViewModel.SelectedScanArea.ScanRange;
            mScanPixelRange = mScanAreaViewModel.ScanRangeToScanPixelRange(mScanRange);
            mCoordinate = ScanPixelRangeToCoordinate(mScanPixelRange);
            DrawScanArea(mCoordinate, mScanAreaGra, pbxScanArea);
            DrawScanArea(mCoordinate, mScanAreaGra, pbxScanArea);
        }

        private void ScanRangeConfirmClick(object sender, C1.Win.C1Command.ClickEventArgs e)
        {

        }
    }
}
