using C1.Win.C1Ribbon;
using confocal_core.Common;
using confocal_core.Model;
using confocal_core.ViewModel;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using log4net;
using NumSharp;
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
    public partial class FormScanImage : C1RibbonForm
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private ScanImageViewModel mScanImageVM;
        private TabPage[] mTabPages;
        private ImageBox[] mImages;

        public FormScanImage(ScanTask scanTask)
        {
            InitializeComponent();
            mScanImageVM = new ScanImageViewModel(scanTask);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            mTabPages = new TabPage[] { pageAll, page405, page488, page561, page640 };
            mImages = new ImageBox[] { imageAll, image405, image488, image561, image640 };
            InitializeTabPages();

            lbPixelSize.Text = string.Format("{0} um/px", mScanImageVM.Engine.Config.ScanPixelSize.ToString("F3"));
            lbScanPixel.Text = string.Format("{0} x {1} pixels", mScanImageVM.Engine.Config.SelectedScanPixel.Data, mScanImageVM.Engine.Config.SelectedScanPixel.Data);
            lbFps.Text = string.Format("{0} fps", mScanImageVM.Engine.Sequence.FPS.ToString("F3"));
            if (mScanImageVM.Engine.Config.IsScanning)
            {
                lbFrame.Text = string.Format("NO. {0} frame", mScanImageVM.Engine.ScanningTask.ScanInfo.CurrentFrame);
                lbTimeSpan.Text = string.Format("{0} secs", mScanImageVM.Engine.ScanningTask.ScanInfo.TimeSpan.ToString("F1"));
            }
            else
            {
                lbFrame.Text = string.Format("NO. 0 frame");
                lbTimeSpan.Text = string.Format("0.0 secs");
            }
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            mScanImageVM.Engine.ChannelActivateChangedEvent += ChannelActivateChangedEventHandler;
            mScanImageVM.Engine.ScanAcquisitionChangedEvent += ScanAcquisitionChangedEventHandler;
            mScanImageVM.Engine.ScanPixelChangedEvent += ScanPixelChangedEventHandler;
        }

        /// <summary>
        /// 设置数据绑定
        /// </summary>
        private void SetDataBindings()
        {
            
        }

        private void InitializeTabPages()
        {
            tabControl.TabPages.Clear();
            foreach (TabPage page in mTabPages)
            {
                int id = int.Parse(page.Tag.ToString());
                if (id < 0 && mScanImageVM.Engine.Config.GetActivatedChannelNum() > 1)
                {
                    tabControl.TabPages.Add(page);
                }
                else if (id >= 0 && mScanImageVM.Engine.Config.ScanChannels[id].Activated)
                {
                    tabControl.TabPages.Add(page);
                }
            }
        }

        private void ScanImageLoad(object sender, EventArgs e)
        {
            Initialize();
            SetDataBindings();
            RegisterEvents();
        }

        private API_RETURN_CODE ChannelActivateChangedEventHandler(ScanChannelModel channel)
        {
            InitializeTabPages();
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE ScanAcquisitionChangedEventHandler(ScanAcquisitionModel scanAcquisition)
        {
            if (scanAcquisition != null)
            {
                mImageTimer.Start();
            }
            else
            {
                mImageTimer.Stop();
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        private API_RETURN_CODE ScanPixelChangedEventHandler(ScanPixelModel scanPixel)
        {
            lbPixelSize.Text = string.Format("{0} um/px", mScanImageVM.Engine.Config.ScanPixelSize.ToString("F3"));
            lbFps.Text = string.Format("{0} fps", mScanImageVM.Engine.Sequence.FPS.ToString("F3"));
            lbScanPixel.Text = string.Format("{0} x {1} pixels", mScanImageVM.Engine.Config.SelectedScanPixel.Data, mScanImageVM.Engine.Config.SelectedScanPixel.Data);
            return API_RETURN_CODE.API_SUCCESS;
        }

        private void ImageToUpdate(object sender, EventArgs e)
        {
            int id = mScanImageVM.Engine.Config.ScanChannels.Where(p => p.Activated).First().ID;
            lbFrame.Text = string.Format("NO. {0} frame", mScanImageVM.Engine.ScanningTask.ScanInfo.CurrentFrame[id]);
            lbTimeSpan.Text = string.Format("{0} secs", mScanImageVM.Engine.ScanningTask.ScanInfo.TimeSpan.ToString("F1"));
            foreach (TabPage page in tabControl.TabPages)
            {
                id = int.Parse(page.Tag.ToString());
                if (id >= 0)
                {
                    ImageBox imageBox = mImages.Where(p => int.Parse(p.Tag.ToString()) == id).First();
                    imageBox.Image = mScanImageVM.Engine.ScanningTask.ScanData.GrayImages[0].Image;
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            short[] data = new short[2 * 20 * 10];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (short)i;
            }
            NDArray matrix = NMatrix.ToMatrix(data, 2, 20, 10, 1, 4, 2, 16);
            Mat image = new Mat(10, 16, DepthType.Cv32S, 1);
            NMatrix.ToBankImage(matrix, ref image);
        }

    }
}
