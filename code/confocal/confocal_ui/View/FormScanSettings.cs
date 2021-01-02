using C1.Win.C1InputPanel;
using C1.Win.C1Ribbon;
using confocal_core.Model;
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

        private InputButton[] mPixelDwellButtons;
        private InputButton[] mScanPixelButtons;

        public FormScanSettings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化成员变量
        /// </summary>
        private void Initialize()
        {
            mScanSettingsVM = new ScanSettingsViewModel();

            mPixelDwellButtons = new InputButton[] 
            {
                btnPixelDwell2,
                btnPixelDwell4,
                btnPixelDwell6,
                btnPixelDwell8,
                btnPixelDwell10,
                btnPixelDwell20,
                btnPixelDwell50,
                btnPixelDwell100
            };

            mScanPixelButtons = new InputButton[]
            {
                btnScanPixel64,
                btnScanPixel128,
                btnScanPixel256,
                btnScanPixel512,
                btnScanPixel1024,
                btnScanPixel2048,
                btnScanPixel4096
            };

            for (int i = 0; i < mPixelDwellButtons.Length; i++)
            {
                mPixelDwellButtons[i].Tag = mScanSettingsVM.ScanPixelDwellList[i];
                mPixelDwellButtons[i].Click += ScanPixelDwellChanged;
            }

        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void ScanSettingsRegisterEvents()
        {
            this.rbtnTwoScanners.CheckedChanged += ScannerHeadChanged;
            this.rbtnGalvano.CheckedChanged += ScannerHeadChanged;

            this.rbtnGalvano.CheckedChanged += ScanModeChanged;
            this.rbtnResonant.CheckedChanged += ScanModeChanged;
        }

        /// <summary>
        /// 设置DataBindings
        /// </summary>
        private void SetDataBindings()
        {
            // 扫描头
            this.rbtnTwoScanners.DataBindings.Add("Text", mScanSettingsVM.ScannerHeadTwoGalv, "Text");
            this.rbtnTwoScanners.DataBindings.Add("Checked", mScanSettingsVM.ScannerHeadTwoGalv, "IsEnabled");
            this.rbtnThreeScanners.DataBindings.Add("Text", mScanSettingsVM.ScannerHeadThreeGalv, "Text");
            this.rbtnThreeScanners.DataBindings.Add("Checked", mScanSettingsVM.ScannerHeadThreeGalv, "IsEnabled");
            // 扫描模式
            this.rbtnGalvano.DataBindings.Add("Text", mScanSettingsVM.ScanModeGalavano, "Text");
            this.rbtnGalvano.DataBindings.Add("Checked", mScanSettingsVM.ScanModeGalavano, "IsEnabled");
            this.rbtnResonant.DataBindings.Add("Text", mScanSettingsVM.ScanModeResonant, "Text");
            this.rbtnResonant.DataBindings.Add("Checked", mScanSettingsVM.ScanModeResonant, "IsEnabled");
            // 扫描方向
            this.btnUniDirection.DataBindings.Add("Text", mScanSettingsVM.ScanUniDirection, "Text");
            this.btnUniDirection.DataBindings.Add("Pressed", mScanSettingsVM.ScanUniDirection, "IsEnabled");
            this.btnBiDirection.DataBindings.Add("Text", mScanSettingsVM.ScanBiDirection, "Text");
            this.btnBiDirection.DataBindings.Add("Pressed", mScanSettingsVM.ScanBiDirection, "IsEnabled");
            // 像素时间
            foreach (InputButton button in mPixelDwellButtons)
            {
                ScanPixelDwellModel model = (ScanPixelDwellModel)button.Tag;
                button.DataBindings.Add("Text", model, "Text");
                button.DataBindings.Add("Pressed", model, "IsEnabled");
            }

            // 扫描像素

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

            Logger.Info(string.Format("Scan Mode: [{0}:{1}].", mScanSettingsVM.ScanModeGalavano.Text, mScanSettingsVM.ScanModeGalavano.IsEnabled));
            Logger.Info(string.Format("Scan Mode: [{0}:{1}].", mScanSettingsVM.ScanModeResonant.Text, mScanSettingsVM.ScanModeResonant.IsEnabled));

            Logger.Info(string.Format("Scan Direction: [{0}:{1}].", mScanSettingsVM.ScanUniDirection.Text, mScanSettingsVM.ScanUniDirection.IsEnabled));
            Logger.Info(string.Format("Scan Direction: [{0}:{1}].", mScanSettingsVM.ScanBiDirection.Text, mScanSettingsVM.ScanBiDirection.IsEnabled));
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

        /// <summary>
        /// 单向按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUniDirection_Click(object sender, EventArgs e)
        {
            if (mScanSettingsVM.ScanUniDirection.IsEnabled)
            {
                btnUniDirection.Pressed = true;
                return;
            }
            btnBiDirection.Pressed = !btnUniDirection.Pressed;
            mScanSettingsVM.ScanBiDirection.IsEnabled = btnBiDirection.Pressed;
            mScanSettingsVM.ScanUniDirection.IsEnabled = btnUniDirection.Pressed;
        }

        /// <summary>
        /// 双向按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBiDirection_Click(object sender, EventArgs e)
        {
            if (mScanSettingsVM.ScanBiDirection.IsEnabled)
            {
                btnBiDirection.Pressed = true;
                return;
            }
            btnUniDirection.Pressed = !btnBiDirection.Pressed;
            mScanSettingsVM.ScanBiDirection.IsEnabled = btnBiDirection.Pressed;
            mScanSettingsVM.ScanUniDirection.IsEnabled = btnUniDirection.Pressed;
        }

        /// <summary>
        /// 扫描像素按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanPixelDwellChanged(object sender, EventArgs e)
        {
            InputButton button = (InputButton)sender;
            ScanPixelDwellModel model = (ScanPixelDwellModel)button.Tag;
            if (model.IsEnabled)
            {
                button.Pressed = true;
                return;
            }
            foreach (InputButton otherButton in mPixelDwellButtons)
            {
                if (!otherButton.Equals(button))
                {
                    ScanPixelDwellModel otherModel = (ScanPixelDwellModel)otherButton.Tag;
                    otherButton.Pressed = false;
                    otherModel.IsEnabled = false;
                }
            }
            button.Pressed = true;
            model.IsEnabled = true;
        }
    }
}
