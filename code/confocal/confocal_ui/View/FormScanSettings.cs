using C1.Win.C1Ribbon;
using confocal_ui.ViewModel;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace confocal_ui
{
    public partial class FormScanSettings : C1RibbonForm
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        
        private ScanSettingsViewModel mScanSettingsVM;


        public FormScanSettings()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            mScanSettingsVM = new ScanSettingsViewModel();
        }

        /// <summary>
        /// 注册ScanSettings的Events
        /// </summary>
        private void ScanSettingsRegisterEvents()
        {
            this.rbtnTwoScanners.CheckedChanged += ScannerHeadChanged;
            this.rbtnGalvano.CheckedChanged += ScannerHeadChanged;


        }

        /// <summary>
        /// 设置DataBindings
        /// </summary>
        private void SetDataBindings()
        {
            this.rbtnTwoScanners.DataBindings.Add("Text", mScanSettingsVM.ScannerHeadTwoGalv, "Text");
            this.rbtnTwoScanners.DataBindings.Add("Checked", mScanSettingsVM.ScannerHeadTwoGalv, "IsEnabled");
            this.rbtnThreeScanners.DataBindings.Add("Text", mScanSettingsVM.ScannerHeadThreeGalv, "Text");
            this.rbtnThreeScanners.DataBindings.Add("Checked", mScanSettingsVM.ScannerHeadThreeGalv, "IsEnabled");

            this.rbtnGalvano.DataBindings.Add("Text", mScanSettingsVM.ScanModeGalavano, "Text");
            this.rbtnGalvano.DataBindings.Add("Checked", mScanSettingsVM.ScanModeGalavano, "IsEnabled");
            this.rbtnResonant.DataBindings.Add("Text", mScanSettingsVM.ScanModeResonant, "Text");
            this.rbtnResonant.DataBindings.Add("Checked", mScanSettingsVM.ScanModeResonant, "IsEnabled");

            this.inputTextBox1.DataBindings.Add("Text", mScanSettingsVM.ScannerHeadTwoGalv, "Text");
        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormScanSettings_Load(object sender, EventArgs e)
        {
            Initialize();
            SetDataBindings();
            ScanSettingsRegisterEvents();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Logger.Info(string.Format("Scanner Header: [{0}:{1}].", mScanSettingsVM.ScannerHeadTwoGalv.Text, mScanSettingsVM.ScannerHeadTwoGalv.IsEnabled));
            Logger.Info(string.Format("Scanner Header: [{0}:{1}].", mScanSettingsVM.ScannerHeadThreeGalv.Text, mScanSettingsVM.ScannerHeadThreeGalv.IsEnabled));
        }

        /// <summary>
        /// 扫描头切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScannerHeadChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.ScannerHeadTwoGalv.IsEnabled = rbtnTwoScanners.Checked;
            mScanSettingsVM.ScannerHeadThreeGalv.IsEnabled = rbtnThreeScanners.Checked;
        }

        /// <summary>
        /// 切换扫描模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanModeChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.ScanModeGalavano.IsEnabled = rbtnGalvano.Checked;
            mScanSettingsVM.ScanModeResonant.IsEnabled = rbtnResonant.Checked;
        }

    }
}
