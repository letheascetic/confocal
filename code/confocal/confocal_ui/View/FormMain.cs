using C1.Win.C1InputPanel;
using C1.Win.C1Ribbon;
using C1.Win.C1Themes;
using confocal_core.Model;
using confocal_ui.View;
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
    public partial class FormMain : C1RibbonForm
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private FormScanSettings mFormScanSetting;
        private FormScanArea mFormScanArea;

        public FormMain()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            WindowState = FormWindowState.Maximized;

            mFormScanSetting = new FormScanSettings();
            mFormScanSetting.MdiParent = this;
            mFormScanSetting.Location = new Point(this.ClientRectangle.Right - mFormScanSetting.Width, 0);
            mFormScanSetting.Visible = true;

            mFormScanArea = new FormScanArea();
            mFormScanArea.MdiParent = this;
            mFormScanArea.Location = new Point(mFormScanSetting.Location.X - mFormScanArea.Width, 0);
            mFormScanArea.Visible = true;

        }

        /// <summary>
        /// 应用主题
        /// </summary>
        private void ApplyTheme(string themeName)
        {
            this.SuspendPainting();
            confocal_ui.Properties.Settings.Default.ThemeName = themeName;
            C1ThemeController.ApplyThemeToControlTree(this, C1ThemeController.GetThemeByName(themeName, false));
            this.ResumePainting();
        }

        /// <summary>
        /// 注册ScanSettings的Events
        /// </summary>
        private void ScanSettingsRegisterEvents()
        {
            //this.rbtnTwoScanners.CheckedChanged += ScannerHeadChanged;
            //this.rbtnGalvano.CheckedChanged += ScanModeChanged;

        }

        /// <summary>
        /// 设置ScanSettings的DataBindings
        /// </summary>
        private void ScanSettingsDataBindings()
        {
            // Scan Mode Controlers
            //this.rbtnGalvano.DataBindings.Add("Text", mScanSettingsViewModel.ScanModeGalavano, "Text");
            //this.rbtnGalvano.DataBindings.Add("Checked", mScanSettingsViewModel.ScanModeGalavano, "IsEnabled");
            //this.rbtnResonant.DataBindings.Add("Text", mScanSettingsViewModel.ScanModeResonant, "Text");
            //this.rbtnResonant.DataBindings.Add("Checked", mScanSettingsViewModel.ScanModeResonant, "IsEnabled");

            //// Scanners
            //this.rbtnTwoScanners.DataBindings.Add("Text", mScanSettingsViewModel.ScannerHeadTwoGalv, "Text");
            //this.rbtnTwoScanners.DataBindings.Add("Checked", mScanSettingsViewModel.ScannerHeadTwoGalv, "IsEnabled");
            //this.rbtnThreeScanners.DataBindings.Add("Text", mScanSettingsViewModel.ScannerHeadThreeGalv, "Text");
            //this.rbtnThreeScanners.DataBindings.Add("Checked", mScanSettingsViewModel.ScannerHeadThreeGalv, "IsEnabled");

            //// Scan Direction
            //this.btnUniDirection.DataBindings.Add("Pressed", mScanSettingsViewModel.ScanUniDirection, "IsEnabled");
            //this.btnBiDirection.DataBindings.Add("Pressed", mScanSettingsViewModel.ScanBiDirection, "IsEnabled");

            //// Scan Pixel Dwell
            //// this.btnPixelDwell2.DataBindings.Add("Text", mScanSettingsViewModel.scan)

            //this.inputTextBox1.DataBindings.Add("Text", mScanSettingsViewModel.ScanModeResonant, "Text");
            
        }

        /// <summary>
        /// 选择主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTheme_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            string themeName = confocal_ui.Properties.Settings.Default.ThemeName;
            FormTheme themeManager = new FormTheme();
            if (themeManager.ShowDialog() == DialogResult.OK)
            {
                if (themeManager.ThemeName != confocal_ui.Properties.Settings.Default.ThemeName)
                {
                    ApplyTheme(themeManager.ThemeName);
                }
            }
        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // init variables & controlers
            // Initialize();

            // register events
            ScanSettingsRegisterEvents();

            // set data bindings
            ScanSettingsDataBindings();

            // apply theme
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            ApplyTheme(confocal_ui.Properties.Settings.Default.ThemeName);
        }

        private void inputButton1_Click(object sender, EventArgs e)
        {
            //Logger.Info(string.Format("ScannerHead [{0}][{1}]", mScanSettingsViewModel.ScannerHeadTwoGalv.IsEnabled, mScanSettingsViewModel.ScannerHeadThreeGalv.IsEnabled));
            //Logger.Info(string.Format("SelectedScannerHead [{0}]", mScanSettingsViewModel.SelectedScannerHead.Text));
            //Logger.Info(string.Format("SelectedScanMode [{0}]", mScanSettingsViewModel.SelectedScanMode.Text));
            //Logger.Info(string.Format("SelectedScanDirection [{0}]", mScanSettingsViewModel.SelectedScanDirection.Text));
        }

        private void btnUniDirection_Click(object sender, EventArgs e)
        {
            //if (mScanSettingsViewModel.ScanUniDirection.IsEnabled)
            //{
            //    btnUniDirection.Pressed = true;
            //    return;
            //}
            //btnBiDirection.Pressed = !btnUniDirection.Pressed;
            //mScanSettingsViewModel.ScanBiDirection.IsEnabled = btnBiDirection.Pressed;
            //mScanSettingsViewModel.ScanUniDirection.IsEnabled = btnUniDirection.Pressed;
            //mScanSettingsViewModel.SelectScanDirectionCommand();
        }

        private void btnBiDirection_Click(object sender, EventArgs e)
        {
            //if (mScanSettingsViewModel.ScanBiDirection.IsEnabled)
            //{
            //    btnBiDirection.Pressed = true;
            //    return;
            //}
            //btnUniDirection.Pressed = !btnBiDirection.Pressed;
            //mScanSettingsViewModel.ScanBiDirection.IsEnabled = btnBiDirection.Pressed;
            //mScanSettingsViewModel.ScanUniDirection.IsEnabled = btnUniDirection.Pressed;
            //mScanSettingsViewModel.SelectScanDirectionCommand();
        }

        private void cmdScanArea_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            FormScanArea scanArea = new FormScanArea();
            scanArea.MdiParent = this;
            scanArea.Show();
        }

        private void cmdScanSettings_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            FormScanSettings mFormScanSettings = new FormScanSettings();
            mFormScanSettings.MdiParent = this;
            mFormScanSettings.Show();
        }

        private void cmdScanImage_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            FormImage mFormImage = new FormImage();
            mFormImage.MdiParent = this;
            mFormImage.Show();
        }
    }
}
