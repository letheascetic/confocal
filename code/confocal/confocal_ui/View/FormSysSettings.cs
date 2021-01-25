using C1.Win.C1InputPanel;
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

        private InputComboBox[] mPmtChannelCbx;
        private InputComboBox[] mApdSourceCbx;
        private InputComboBox[] mApdChannelCbx;
        
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
            mPmtChannelCbx = new InputComboBox[]
            {
                cbx405PMT,
                cbx488PMT,
                cbx561PMT,
                cbx640PMT
            };
            mApdSourceCbx = new InputComboBox[]
            {
                cbx405Ctr,
                cbx488Ctr,
                cbx561Ctr,
                cbx640Ctr
            };
            mApdChannelCbx = new InputComboBox[]
            {
                cbx405Channel,
                cbx488Channel,
                cbx561Channel,
                cbx640Channel
            };
        }

        private void SetDataBindings()
        {
            // 振镜模拟输出通道
            cbxXGalvo.DataSource = mSysSettingsViewModel.XGalvoAoChannels;
            cbxYGalvo.DataSource = mSysSettingsViewModel.YGalvoAoChannels;
            cbxYGalvo2.DataSource = mSysSettingsViewModel.Y2GalvoAoChannels;

            
            cbxXGalvo.SelectedItem = mSysSettingsViewModel.Config.GalvoProperty.XGalvoAoChannel;
            cbxYGalvo.SelectedItem = mSysSettingsViewModel.Config.GalvoProperty.YGalvoAoChannel;
            cbxYGalvo2.SelectedItem = mSysSettingsViewModel.Config.GalvoProperty.Y2GalvoAoChannel;

            // 振镜偏置电压和校准电压
            nbXGalvoOffset.DataBindings.Add("Value", mSysSettingsViewModel.Config.GalvoProperty, "XGalvoOffsetVoltage");
            nbYGalvoOffset.DataBindings.Add("Value", mSysSettingsViewModel.Config.GalvoProperty, "YGalvoOffsetVoltage");

            nbXGalvoCalibration.DataBindings.Add("Value", mSysSettingsViewModel.Config.GalvoProperty, "XGalvoCalibrationVoltage");
            nbYGalvoCalibration.DataBindings.Add("Value", mSysSettingsViewModel.Config.GalvoProperty, "YGalvoCalibrationVoltage");

            // 振镜响应时间和视场大小
            nbGalvoResponseTime.DataBindings.Add("Value", mSysSettingsViewModel.Config.GalvoProperty, "GalvoResponseTime");
            nbScanRange.DataBindings.Add("Value", mSysSettingsViewModel.Config.FullScanArea.ScanRange, "Width");

            // 采集控制
            rbtnPMT.DataBindings.Add("Checked", mSysSettingsViewModel.Config.Detector.DetectorPmt, "IsEnabled");
            rbtnAPD.DataBindings.Add("Checked", mSysSettingsViewModel.Config.Detector.DetectorApd, "IsEnabled");
            cbxStartSync.DataSource = mSysSettingsViewModel.StartTriggers;
            cbxStartSync.SelectedItem = mSysSettingsViewModel.Config.Detector.StartTrigger;
            cbxTrigger.DataSource = mSysSettingsViewModel.TriggerSignals;
            cbxTrigger.SelectedItem = mSysSettingsViewModel.Config.Detector.TriggerSignal;
            cbxTriggerR.DataSource = mSysSettingsViewModel.TriggerReceivers;
            cbxTriggerR.SelectedItem = mSysSettingsViewModel.Config.Detector.TriggerReceive;

            // PMT
            for (int i = 0; i < mPmtChannelCbx.Length; i++)
            {
                mPmtChannelCbx[i].Tag = i;
                mPmtChannelCbx[i].DataSource = mSysSettingsViewModel.AiChannels[i];
                mPmtChannelCbx[i].SelectedItem = mSysSettingsViewModel.Config.FindPmtChannel(i).AiChannel;
            }

            // APD
            for (int i = 0; i < mApdSourceCbx.Length; i++)
            {
                mApdSourceCbx[i].Tag = i;
                mApdSourceCbx[i].DataSource = mSysSettingsViewModel.CiSources[i];
                mApdSourceCbx[i].SelectedItem = mSysSettingsViewModel.Config.FindApdChannel(i).CiSource;
            }

            for (int i = 0; i < mApdChannelCbx.Length; i++)
            {
                mApdChannelCbx[i].Tag = i;
                mApdChannelCbx[i].DataSource = mSysSettingsViewModel.CiChannels[i];
                mApdChannelCbx[i].SelectedItem = mSysSettingsViewModel.Config.FindApdChannel(i).CiChannel;
            }
        }

        private void RegisterEvents()
        {
            // 振镜控制端口
            cbxXGalvo.SelectedIndexChanged += XGalvoChannelChanged;
            cbxYGalvo.SelectedIndexChanged += YGalvoChannelChanged;
            cbxYGalvo2.SelectedIndexChanged += Y2GalvoChannelChanged;

            // 振镜偏置电压和校准电压
            nbXGalvoOffset.ChangeCommitted += XGalvoOffsetVoltageChaned;
            nbYGalvoOffset.ChangeCommitted += YGalvoOffsetVoltageChaned;
            nbXGalvoCalibration.ChangeCommitted += XGalvoCalibrationVoltageChaned;
            nbYGalvoCalibration.ChangeCommitted += YGalvoCalibrationVoltageChaned;

            // 振镜响应时间和视场
            nbGalvoResponseTime.ValueChanged += GalvoResponseTimeChanged;
            nbScanRange.ValueChanged += FullScanAreaChanged;

            // 探测器类型 启动同步 触发信号 触发接收端口
            rbtnPMT.CheckedChanged += DetectorModeChanged;
            cbxStartSync.SelectedIndexChanged += StartTriggerChanged;
            cbxTrigger.SelectedIndexChanged += TriggerSignalChanged;
            cbxTriggerR.SelectedIndexChanged += TriggerReceiveChanged;

            // PMT
            for (int i = 0; i < mPmtChannelCbx.Length; i++)
            {
                mPmtChannelCbx[i].SelectedIndexChanged += PmtChannelChanged;
            }

            // APD
            for (int i = 0; i < mApdSourceCbx.Length; i++)
            {
                mApdSourceCbx[i].SelectedIndexChanged += ApdSourceChanged;
            }

            for (int i = 0; i < mApdChannelCbx.Length; i++)
            {
                mApdChannelCbx[i].SelectedIndexChanged += ApdChannelChanged;
            }

        }

        private void FormSysSettingsLoad(object sender, EventArgs e)
        {
            Initialize();
            SetDataBindings();
            RegisterEvents();
        }

        /// <summary>
        /// PMT通道接口更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PmtChannelChanged(object sender, EventArgs e)
        {
            InputComboBox cbx = (InputComboBox)sender;
            mSysSettingsViewModel.Config.PmtChannelChangeCommand((int)cbx.Tag, cbx.SelectedItem.ToString());
        }

        /// <summary>
        /// APD使用的计数器更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApdSourceChanged(object sender, EventArgs e)
        {
            InputComboBox cbx = (InputComboBox)sender;
            mSysSettingsViewModel.Config.ApdSourceChangeCommand((int)cbx.Tag, cbx.SelectedItem.ToString());
        }

        /// <summary>
        /// APD使用的计数器接收端口更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApdChannelChanged(object sender, EventArgs e)
        {
            InputComboBox cbx = (InputComboBox)sender;
            mSysSettingsViewModel.Config.ApdChannelChangeCommand((int)cbx.Tag, cbx.SelectedItem.ToString());
        }

        /// <summary>
        /// X振镜端口更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XGalvoChannelChanged(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.XGalvoChannelChangeCommand(cbxXGalvo.SelectedItem.ToString());
        }

        /// <summary>
        /// Y振镜端口更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YGalvoChannelChanged(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.YGalvoChannelChangeCommand(cbxYGalvo.SelectedItem.ToString());
        }

        /// <summary>
        /// Y2振镜端口更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y2GalvoChannelChanged(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.Y2GalvoChannelChangeCommand(cbxYGalvo2.SelectedItem.ToString());
        }

        private void XGalvoOffsetVoltageChaned(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.XGalvoOffsetVoltageChangeCommand((double)nbXGalvoOffset.Value);
        }

        private void YGalvoOffsetVoltageChaned(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.YGalvoOffsetVoltageChangeCommand((double)nbYGalvoOffset.Value);
        }

        private void XGalvoCalibrationVoltageChaned(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.XGalvoCalibrationVoltageChangeCommand((double)nbXGalvoCalibration.Value);
        }

        private void YGalvoCalibrationVoltageChaned(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.YGalvoCalibrationVoltageChangeCommand((double)nbYGalvoCalibration.Value);
        }

        private void HidingForm(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void GalvoResponseTimeChanged(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.GalvoResponseTimeChangeCommand((double)nbGalvoResponseTime.Value);
        }

        private void FullScanAreaChanged(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.FullScanAreaChangeCommand((float)nbScanRange.Value);
        }

        private void DetectorModeChanged(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.DetectorModeChangeCommand(rbtnPMT.Checked);
        }

        private void StartTriggerChanged(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.StartTriggerChangeCommand(cbxStartSync.SelectedItem.ToString());
        }

        private void TriggerSignalChanged(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.TriggerSignalChangeCommand(cbxTrigger.SelectedItem.ToString());
        }

        private void TriggerReceiveChanged(object sender, EventArgs e)
        {
            mSysSettingsViewModel.Config.TriggerReceiverChangeCommand(cbxTriggerR.SelectedItem.ToString());
        }

    }
}
