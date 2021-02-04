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

        public FormScanImage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            mScanImageVM = new ScanImageViewModel();
            mTabPages = new TabPage[] { pageAll, page405, page488, page561, page640 };
            mImages = new ImageBox[] { imageAll, image405, image488, image561, image640 };
            InitializeTabPages();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            mScanImageVM.Engine.ChannelActivateChangedEvent += ChannelActivateChangedEventHandler;
            mScanImageVM.Engine.ScanAcquisitionChangedEvent += ScanAcquisitionChangedEventHandler;
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

        private void ImageToUpdate(object sender, EventArgs e)
        {
            foreach (TabPage page in tabControl.TabPages)
            {
                int id = int.Parse(page.Tag.ToString());
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
            NDArray matrix = Matrix.ToMatrix(data, 2, 20, 10, 1, 4, 2, 16);
            Mat image = new Mat(10, 16, DepthType.Cv32S, 1);
            Matrix.ToBankImage(matrix, ref image);
        }

    }
}
