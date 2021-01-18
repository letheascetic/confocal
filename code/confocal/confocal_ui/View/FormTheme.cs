using C1.Win.C1Themes;
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
    public partial class FormTheme : Form
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");

        public FormTheme()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 主题名
        /// </summary>
        public string ThemeName
        {
            get { return C1ThemeController.ApplicationTheme; }
        }

        /// <summary>
        /// 设置应用程序主题
        /// </summary>
        /// <param name="themeName"></param>
        private void SetApplicationTheme(string themeName)
        {
            tbApplicationTheme.Value = themeName;
            // confocal_ui.Properties.Settings.Default.ThemeName = themeName;
            C1ThemeController.ApplicationTheme = themeName;
            // C1ThemeController.ApplyThemeToControlTree(this, C1ThemeController.GetThemeByName(themeName, false));
        }
        
        /// <summary>
        /// 应用主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemesDoubleClick(object sender, EventArgs e)
        {
            if (ctdbgThemes.Row >= 0 && ctdbgThemes.Row < ctdbgThemes.Rows.Count)
            {
                string themeName = (string)ctdbgThemes.Rows[ctdbgThemes.Row][0];
                SetApplicationTheme(themeName);
            }
        }

        private void ThemesResize(object sender, EventArgs e)
        {
            ctdbgThemes.Splits[0].DisplayColumns[0].Width = ctdbgThemes.ClientSize.Width - ctdbgThemes.Splits[0].RecordSelectorWidth - 1;
        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormThemeLoad(object sender, EventArgs e)
        {
            ThemesResize(null, EventArgs.Empty);
            ctdbgThemes.SetDataBinding();
            string[] themes = C1ThemeController.GetThemes();
            foreach (string s in themes)
                ctdbgThemes.AddRow(s);
            string themeName = confocal_ui.Properties.Settings.Default.ThemeName;
            SetApplicationTheme(themeName);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 应用主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyClick(object sender, EventArgs e)
        {
            if (ctdbgThemes.Row >= 0 && ctdbgThemes.Row < ctdbgThemes.Rows.Count)
            {
                string themeName = (string)ctdbgThemes.Rows[ctdbgThemes.Row][0];
                SetApplicationTheme(themeName);
            }
        }
    }
}
