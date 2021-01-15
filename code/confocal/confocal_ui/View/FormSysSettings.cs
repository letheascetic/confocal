using C1.Win.C1Ribbon;
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
    public partial class FormSysSettings : C1RibbonForm
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private SysSettingsViewModel mSysSettingsViewModel;

        public SysSettingsViewModel SysSettingsVM
        {
            get { return mSysSettingsViewModel; }
        }

        public FormSysSettings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化成员变量
        /// </summary>
        private void Initialize()
        {
            mSysSettingsViewModel = new SysSettingsViewModel();

        }

        private void SetDataBindings()
        {
            // 振镜模拟输出通道
            cbxXGalvo.DataSource = mSysSettingsViewModel.XGalvoAoChannels;
            cbxYGalvo.DataSource = mSysSettingsViewModel.YGalvoAoChannels;
            cbxYGalvo2.DataSource = mSysSettingsViewModel.Y2GalvoAoChannels;

            cbxXGalvo.SelectedItem = mSysSettingsViewModel.XGalvoAoChannel;
            cbxYGalvo.SelectedItem = mSysSettingsViewModel.YGalvoAoChannel;
            cbxYGalvo2.SelectedItem = mSysSettingsViewModel.Y2GalvoAoChannel;

            // 振镜偏置电压和校准电压
            tbxXGalvoOffset.DataBindings.Add("Text", mSysSettingsViewModel.GalvoProperty, "XGalvoOffsetVoltage");
            tbxYGalvoOffset.DataBindings.Add("Text", mSysSettingsViewModel.GalvoProperty, "YGalvoOffsetVoltage");

            tbxXGalvoScaleFactor.DataBindings.Add("Text", mSysSettingsViewModel.GalvoProperty, "XGalvoCalibrationVoltage");
            tbxYGalvoScaleFactor.DataBindings.Add("Text", mSysSettingsViewModel.GalvoProperty, "YGalvoCalibrationVoltage");

            // 振镜响应时间和扫描视场


        }

        private void RegisterEvents()
        {
            
        }

        private void FormSysSettingsLoad(object sender, EventArgs e)
        {
            Initialize();
            RegisterEvents();
            SetDataBindings();
        }
    }
}
