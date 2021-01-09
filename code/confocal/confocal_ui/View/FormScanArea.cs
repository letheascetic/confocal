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
        ///////////////////////////////////////////////////////////////////////////////////////////

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
            mScanAreaViewModel = new ScanAreaViewModel();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            cbxScanPixel.ChangeCommitted += ScanPixelChanged;
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
        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormScanArea_Load(object sender, EventArgs e)
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

    }
}
