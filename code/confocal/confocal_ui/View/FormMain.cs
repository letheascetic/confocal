using C1.Win.C1Ribbon;
using C1.Win.C1Themes;
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
    public partial class FormMain : C1RibbonForm
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        
        private ScanSettingsViewModel mScanSettingsViewModel = new ScanSettingsViewModel();

        public FormMain()
        {
            InitializeComponent();
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
        /// 设置ScanSettings的DataBindings
        /// </summary>
        private void ScanSettingsDataBindings()
        {
            // Scan Mode Controlers
            this.rbtnGalvano.DataBindings.Add("Text", mScanSettingsViewModel.ScanModeGalavano, "Text");
            this.rbtnGalvano.DataBindings.Add("Checked", mScanSettingsViewModel.ScanModeGalavano, "IsEnabled");
            this.rbtnResonant.DataBindings.Add("Text", mScanSettingsViewModel.ScanModeResonant, "Text");
            this.rbtnResonant.DataBindings.Add("Checked", mScanSettingsViewModel.ScanModeResonant, "IsEnabled");

            // Scanners
            


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
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            ScanSettingsDataBindings();
            ApplyTheme(confocal_ui.Properties.Settings.Default.ThemeName);
        }

    }
}
