﻿using C1.Win.C1InputPanel;
using C1.Win.C1Ribbon;
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
        private InputTrackBar[] mChannelGainBars;
        private InputTrackBar[] mChannelOffsetBars;
        private InputTrackBar[] mChannelPowerBars;

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

            mChannelGainBars = new InputTrackBar[]
            {
                tbar405HV,
                tbar488HV,
                tbar561HV,
                tbar640HV
            };

            mChannelOffsetBars = new InputTrackBar[]
            {
                tbar405Offset,
                tbar488Offset,
                tbar561Offset,
                tbar640Offset
            };

            mChannelPowerBars = new InputTrackBar[]
            {
                tbar405Power,
                tbar488Power,
                tbar561Power,
                tbar640Power
            };

        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            this.rbtnTwoScanners.CheckedChanged += ScannerHeadChanged;
            // this.rbtnThreeScanners.CheckedChanged += ScannerHeadChanged;

            this.rbtnGalvano.CheckedChanged += ScanModeChanged;
            // this.rbtnResonant.CheckedChanged += ScanModeChanged;

            for (int i = 0; i < mPixelDwellButtons.Length; i++)
            {
                mPixelDwellButtons[i].Tag = mScanSettingsVM.ScanPixelDwellList[i];
                mPixelDwellButtons[i].Click += ScanPixelDwellChanged;
            }

            for (int i = 0; i < mScanPixelButtons.Length; i++)
            {
                mScanPixelButtons[i].Tag = mScanSettingsVM.ScanPixelList[i];
                mScanPixelButtons[i].Click += ScanPixelChanged;
            }

            cbxLineSkip.SelectedIndexChanged += ScanLineSkipChanged;

            cbxPinHoleSelect.SelectedIndexChanged += ScanPinHoleChanged;

            for (int i = 0; i < mChannelGainBars.Length; i++)
            {
                mChannelGainBars[i].Tag = i;
                mChannelGainBars[i].ValueChanged += ChannelGainChanged;
                mChannelOffsetBars[i].Tag = i;
                mChannelOffsetBars[i].ValueChanged += ChannelOffsetChanged;
                mChannelPowerBars[i].Tag = i;
                mChannelPowerBars[i].ValueChanged += ChannelPowerChanged;
            }
        }

        /// <summary>
        /// 设置DataBindings
        /// </summary>
        private void SetDataBindings()
        {
            // 采集模式
            this.btnLive.DataBindings.Add("Text", mScanSettingsVM.ScanLiveMode, "Text");
            this.btnLive.DataBindings.Add("Pressed", mScanSettingsVM.ScanLiveMode, "IsEnabled");
            this.btnCapture.DataBindings.Add("Text", mScanSettingsVM.ScanCaptureMode, "Text");
            this.btnCapture.DataBindings.Add("Pressed", mScanSettingsVM.ScanCaptureMode, "IsEnabled");
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
            // 快速模式使能
            this.rbtnFastMode.DataBindings.Add("Pressed", mScanSettingsVM, "FastModeEnabled");
            foreach (InputButton button in mPixelDwellButtons)
            {
                ScanPixelDwellModel model = (ScanPixelDwellModel)button.Tag;
                button.DataBindings.Add("Text", model, "Text");
                button.DataBindings.Add("Pressed", model, "IsEnabled");
            }
            // 扫描像素
            foreach (InputButton button in mScanPixelButtons)
            {
                ScanPixelModel model = (ScanPixelModel)button.Tag;
                button.DataBindings.Add("Text", model, "Text");
                button.DataBindings.Add("Pressed", model, "IsEnabled");
            }
            // 跳行扫描
            this.chbxLineSkip.DataBindings.Add("Checked", mScanSettingsVM, "ScanLineSkipEnabled");
            this.cbxLineSkip.DataSource = mScanSettingsVM.ScanLineSkipList;
            this.cbxLineSkip.DisplayMember = "Text";
            this.cbxLineSkip.ValueMember = "Data";
            this.cbxLineSkip.SelectedItem = mScanSettingsVM.SelectedScanLineSkip;
            // 扫描通道1 - 405nm
            this.gh405.DataBindings.Add("BackColor", mScanSettingsVM.ScanChannel405, "PseudoColor");
            this.gh405.DataBindings.Add("Collapsed", mScanSettingsVM.ScanChannel405, "Collapsed");
            this.tbar405HV.DataBindings.Add("Value", mScanSettingsVM.ScanChannel405, "Gain");
            this.tbx405HV.DataBindings.Add("Text", tbar405HV, "Value");
            this.tbar405Offset.DataBindings.Add("Value", mScanSettingsVM.ScanChannel405, "Offset");
            this.tbx405Offset.DataBindings.Add("Text", tbar405Offset, "Value");
            this.tbar405Power.DataBindings.Add("Value", mScanSettingsVM.ScanChannel405, "LaserPower");
            this.tbx405Power.DataBindings.Add("Text", tbar405Power, "Value");
            this.btn405Power.DataBindings.Add("Pressed", mScanSettingsVM.ScanChannel405, "Activated");
            // 扫描通道2 - 488nm
            this.gh488.DataBindings.Add("BackColor", mScanSettingsVM.ScanChannel488, "PseudoColor");
            this.gh488.DataBindings.Add("Collapsed", mScanSettingsVM.ScanChannel488, "Collapsed");
            this.tbar488HV.DataBindings.Add("Value", mScanSettingsVM.ScanChannel488, "Gain");
            this.tbx488HV.DataBindings.Add("Text", mScanSettingsVM.ScanChannel488, "Gain");
            this.tbar488Offset.DataBindings.Add("Value", mScanSettingsVM.ScanChannel488, "Offset");
            this.tbx488Offset.DataBindings.Add("Text", mScanSettingsVM.ScanChannel488, "Offset");
            this.tbar488Power.DataBindings.Add("Value", mScanSettingsVM.ScanChannel488, "LaserPower");
            this.tbx488Power.DataBindings.Add("Text", mScanSettingsVM.ScanChannel488, "LaserPower");
            this.btn488Power.DataBindings.Add("Pressed", mScanSettingsVM.ScanChannel488, "Activated");
            // 扫描通道3 - 561nm
            this.gh561.DataBindings.Add("BackColor", mScanSettingsVM.ScanChannel561, "PseudoColor");
            this.gh561.DataBindings.Add("Collapsed", mScanSettingsVM.ScanChannel561, "Collapsed");
            this.tbar561HV.DataBindings.Add("Value", mScanSettingsVM.ScanChannel561, "Gain");
            this.tbx561HV.DataBindings.Add("Text", mScanSettingsVM.ScanChannel561, "Gain");
            this.tbar561Offset.DataBindings.Add("Value", mScanSettingsVM.ScanChannel561, "Offset");
            this.tbx561Offset.DataBindings.Add("Text", mScanSettingsVM.ScanChannel561, "Offset");
            this.tbar561Power.DataBindings.Add("Value", mScanSettingsVM.ScanChannel561, "LaserPower");
            this.tbx561Power.DataBindings.Add("Text", mScanSettingsVM.ScanChannel561, "LaserPower");
            this.btn561Power.DataBindings.Add("Pressed", mScanSettingsVM.ScanChannel561, "Activated");
            // 扫描通道4 - 640nm
            this.gh640.DataBindings.Add("BackColor", mScanSettingsVM.ScanChannel640, "PseudoColor");
            this.gh640.DataBindings.Add("Collapsed", mScanSettingsVM.ScanChannel640, "Collapsed");
            this.tbar640HV.DataBindings.Add("Value", mScanSettingsVM.ScanChannel640, "Gain");
            this.tbx640HV.DataBindings.Add("Text", mScanSettingsVM.ScanChannel640, "Gain");
            this.tbar640Offset.DataBindings.Add("Value", mScanSettingsVM.ScanChannel640, "Offset");
            this.tbx640Offset.DataBindings.Add("Text", mScanSettingsVM.ScanChannel640, "Offset");
            this.tbar640Power.DataBindings.Add("Value", mScanSettingsVM.ScanChannel640, "LaserPower");
            this.tbx640Power.DataBindings.Add("Text", mScanSettingsVM.ScanChannel640, "LaserPower");
            this.btn640Power.DataBindings.Add("Pressed", mScanSettingsVM.ScanChannel640, "Activated");
            // 小孔
            this.cbxPinHoleSelect.DataSource = mScanSettingsVM.ScanPinHoleList;
            this.cbxPinHoleSelect.DisplayMember = "Name";
            this.cbxPinHoleSelect.ValueMember = "Size";
            this.cbxPinHoleSelect.SelectedItem = mScanSettingsVM.SelectedPinHole;
            this.tbxPinHole.DataBindings.Add("Text", tbarPinHole, "Value");

            // 其他
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
            RegisterEvents();
            SetDataBindings();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Logger.Info(string.Format("Scanner Header: [{0}:{1}].", mScanSettingsVM.ScannerHeadTwoGalv.Text, mScanSettingsVM.ScannerHeadTwoGalv.IsEnabled));
            Logger.Info(string.Format("Scanner Header: [{0}:{1}].", mScanSettingsVM.ScannerHeadThreeGalv.Text, mScanSettingsVM.ScannerHeadThreeGalv.IsEnabled));

            Logger.Info(string.Format("Scan Mode: [{0}:{1}].", mScanSettingsVM.ScanModeGalavano.Text, mScanSettingsVM.ScanModeGalavano.IsEnabled));
            Logger.Info(string.Format("Scan Mode: [{0}:{1}].", mScanSettingsVM.ScanModeResonant.Text, mScanSettingsVM.ScanModeResonant.IsEnabled));

            Logger.Info(string.Format("Scan Direction: [{0}:{1}].", mScanSettingsVM.ScanUniDirection.Text, mScanSettingsVM.ScanUniDirection.IsEnabled));
            Logger.Info(string.Format("Scan Direction: [{0}:{1}].", mScanSettingsVM.ScanBiDirection.Text, mScanSettingsVM.ScanBiDirection.IsEnabled));

            Logger.Info(string.Format("Scan Line Skip: [{0}:{1}].", mScanSettingsVM.ScanLineSkipEnabled, mScanSettingsVM.SelectedScanLineSkip.Text));
        }

        /// <summary>
        /// 扫描头切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScannerHeadChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.ScannerHeadChangeCommand(rbtnTwoScanners.Checked);
        }

        /// <summary>
        /// 切换扫描模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanModeChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.ScanModeChangeCommand(rbtnGalvano.Checked);
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
            mScanSettingsVM.ScanDirectionChangeCommand(btnUniDirection.Pressed);
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
            mScanSettingsVM.ScanDirectionChangeCommand(btnUniDirection.Pressed);
        }

        /// <summary>
        /// 像素停留时间按钮点击事件
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

            mScanSettingsVM.ScanPixelDwellChangeCommand(model);
        }

        /// <summary>
        /// 扫描像素按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanPixelChanged(object sender, EventArgs e)
        {
            InputButton button = (InputButton)sender;
            ScanPixelModel model = (ScanPixelModel)button.Tag;
            if (model.IsEnabled)
            {
                button.Pressed = true;
                return;
            }

            mScanSettingsVM.ScanPixelChangeCommand(model);
        }

        /// <summary>
        /// 切换跳行扫描事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanLineSkipChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.LineSkipValueChangeCommand((ScanLineSkipModel)cbxLineSkip.SelectedItem);
        }

        /// <summary>
        /// 切换小孔事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanPinHoleChanged(object sender, EventArgs e)
        {
            this.tbarPinHole.DataBindings.Clear();
            mScanSettingsVM.PinHoleSelectChangeCommand((ScanPinHoleModel)cbxPinHoleSelect.SelectedItem);
            this.tbarPinHole.DataBindings.Add("Value", mScanSettingsVM.SelectedPinHole, "Size");
        }

        /// <summary>
        /// 实时采集模式点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLive_Click(object sender, EventArgs e)
        {
            if (btnLive.Pressed && btnCapture.Pressed)
            {
                btnCapture.Pressed = false;
            }
            mScanSettingsVM.ScanAcquisitionChangeCommand(btnLive.Pressed, btnCapture.Pressed);
        }

        /// <summary>
        /// 捕捉模式点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (btnLive.Pressed && btnCapture.Pressed)
            {
                btnLive.Pressed = false;
            }
            mScanSettingsVM.ScanAcquisitionChangeCommand(btnLive.Pressed, btnCapture.Pressed);
        }

        /// <summary>
        /// 快速模式使能点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnFastMode_Click(object sender, EventArgs e)
        {
            mScanSettingsVM.FastModeEnabled = rbtnFastMode.Pressed;
            Logger.Info(string.Format("Fast Mode Enabled [{0}].", mScanSettingsVM.FastModeEnabled));
        }

        /// <summary>
        /// 跳行扫描使能变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbxLineSkip_CheckedChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.LineSkipEnableChangeCommand(chbxLineSkip.Checked);
        }

        /// <summary>
        /// 小孔孔径变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbarPinHole_ValueChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.PinHoleValueChangeCommand(tbarPinHole.Value);
        }

        /// <summary>
        /// 通道增益更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelGainChanged(object sender, EventArgs e)
        {
            InputTrackBar bar = (InputTrackBar)sender;
            mScanSettingsVM.ChannelGainChangeCommand((int)bar.Tag, bar.Value);
        }

        /// <summary>
        /// 通道偏置更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelOffsetChanged(object sender, EventArgs e)
        {
            InputTrackBar bar = (InputTrackBar)sender;
            mScanSettingsVM.ChannelOffsetChangeCommand((int)bar.Tag, bar.Value);
        }

        /// <summary>
        /// 通道功率更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelPowerChanged(object sender, EventArgs e)
        {
            InputTrackBar bar = (InputTrackBar)sender;
            mScanSettingsVM.ChannelPowerChangeCommand((int)bar.Tag, bar.Value);
        }
    }
}
