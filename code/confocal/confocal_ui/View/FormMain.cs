using C1.Win.C1InputPanel;
using C1.Win.C1Ribbon;
using C1.Win.C1Themes;
using confocal_core;
using confocal_core.Common;
using confocal_core.Model;
using confocal_core.ViewModel;
using confocal_ui.View;
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
        private FormSysSettings mFormSysSettings;
        private FormScanParas mFormScanParas;
        private FormScanImage mFormScanImage;

        private MainViemModel mMainVM;

        public FormMain()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            mMainVM = new MainViemModel();

            mFormScanSetting = new FormScanSettings
            {
                MdiParent = this,
                Visible = true
            };

            mFormScanArea = new FormScanArea
            {
                MdiParent = this,
                Visible = true
            };

            mFormSysSettings = new FormSysSettings
            {
                MdiParent = this,
                Visible = false
            };

            mFormScanParas = new FormScanParas
            {
                MdiParent = this,
                Visible = false
            };

            mFormScanImage = new FormScanImage
            {
                MdiParent = this,
                Visible = false
            };

        }

        private void InitAppearance()
        {
            WindowState = FormWindowState.Maximized;
            mFormScanSetting.Location = new Point(this.ClientRectangle.Right - mFormScanSetting.Width, 0);
            mFormScanArea.Location = new Point(mFormScanSetting.Location.X - mFormScanArea.Width, 0);

            // menu strip
            cmdScanArea.Checked = mFormScanArea.Visible;
            cmdScanSettings.Checked = mFormScanSetting.Visible;
            cmdSysCfg.Checked = mFormSysSettings.Visible;
        }

        /// <summary>
        /// 设置数据绑定
        /// </summary>
        private void SetDataBindings()
        {
            // menu strip
            


            // window
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            mMainVM.Engine.ScanAcquisitionChangedEvent += ScanAcquisitionChangedHandler;
        }

        /// <summary>
        /// 采集模式更新处理函数
        /// </summary>
        /// <param name="scanAcquisition"></param>
        /// <returns></returns>
        private API_RETURN_CODE ScanAcquisitionChangedHandler(ScanAcquisitionModel scanAcquisition)
        {
            if (scanAcquisition != null)
            {
                mFormScanImage.Visible = true;
                cmdScanImage.Checked = mFormScanImage.Visible;
            }
            return API_RETURN_CODE.API_SUCCESS;
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
        /// 选择主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemeClick(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
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
        private void FormMainLoad(object sender, EventArgs e)
        {
            // init variables & controlers
            // Initialize();
            InitAppearance();

            // set data bindings

            // register events
            RegisterEvents();

            // apply theme
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            ApplyTheme(confocal_ui.Properties.Settings.Default.ThemeName);
        }

        private void ScanAreaClick(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            mFormScanArea.Visible = cmdScanArea.Checked;
        }

        private void ScanSettingsClick(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            mFormScanSetting.Visible = cmdScanSettings.Checked;
        }

        private void ScanImageClick(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            mFormScanImage.Visible = cmdScanImage.Checked;
        }

        private void SysSettingsClick(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            mFormSysSettings.Visible = cmdSysCfg.Checked;
        }

        private void ScanParasClick(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            mFormScanParas.Visible = cmdScanParas.Checked;
        }
    }
}
