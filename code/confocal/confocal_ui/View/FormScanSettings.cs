using C1.Win.C1InputPanel;
using C1.Win.C1Ribbon;
using confocal_core;
using confocal_core.Common;
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
        public event ScanAcquisitionChangedEventHandler ScanAcquisitionChangedEvent;
        public event ScannerHeadModelChangedEventHandler ScannerHeadModelChangedEvent;
        public event ScanDirectionChangedEventHandler ScanDirectionChangedEvent;
        public event ScanModeChangedEventHandler ScanModeChangedEvent;
        public event LineSkipEnableChangedEventHandler LineSkipEnableChangedEvent;
        public event LineSkipChangedEventHandler LineSkipChangedEvent;
        public event ScanPixelChangedEventHandler ScanPixelChangedEvent;
        public event ScanPixelDwellChangedEventHandler ScanPixelDwellChangedEvent;
        public event PinHoleChangedEventHandler PinHoleChangedEvent;
        public event ChannelGainChangedEventHandler ChannelGainChangedEvent;
        public event ChannelOffsetChangedEventHandler ChannelOffsetChangedEvent;
        public event ChannelPowerChangedEventHandler ChannelPowerChangedEvent;
        public event ChannelActivateChangedEventHandler ChannelActivateChangedEvent;
        ///////////////////////////////////////////////////////////////////////////////////////////

        private ScanSettingsViewModel mScanSettingsVM;

        public ScanSettingsViewModel ScanSettingsVM
        {
            get { return mScanSettingsVM; }
        }

        private InputButton[] mPixelDwellButtons;
        private InputButton[] mScanPixelButtons;
        private InputTrackBar[] mChannelGainBars;
        private InputTrackBar[] mChannelOffsetBars;
        private InputTrackBar[] mChannelPowerBars;
        private InputButton[] mChannelActivateButtons;

        public FormScanSettings()
        {
            InitializeComponent();
        }

        public API_RETURN_CODE ScanPixelChangedHandler(ScanPixelModel scanPixel)
        {
            return mScanSettingsVM.Config.ScanPixelChangeCommand(scanPixel);
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

            mChannelActivateButtons = new InputButton[]
            {
                btn405Power,
                btn488Power,
                btn561Power,
                btn640Power
            };

        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            this.rbtnTwoScanners.CheckedChanged += ScannerHeadChanged;
            this.rbtnGalvano.CheckedChanged += ScanModeChanged;

            for (int i = 0; i < mPixelDwellButtons.Length; i++)
            {
                mPixelDwellButtons[i].Tag = mScanSettingsVM.Config.ScanPixelDwellList[i];
                mPixelDwellButtons[i].Click += ScanPixelDwellChanged;
            }

            for (int i = 0; i < mScanPixelButtons.Length; i++)
            {
                mScanPixelButtons[i].Tag = mScanSettingsVM.Config.ScanPixelList[i];
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
                mChannelActivateButtons[i].Tag = i;
                mChannelActivateButtons[i].Click += ChannelActivateChanged;
            }
        }

        /// <summary>
        /// 设置DataBindings
        /// </summary>
        private void SetDataBindings()
        {
            // 采集模式
            this.btnLive.DataBindings.Add("Text", mScanSettingsVM.Config.ScanLiveMode, "Text");
            this.btnLive.DataBindings.Add("Pressed", mScanSettingsVM.Config.ScanLiveMode, "IsEnabled");
            this.btnCapture.DataBindings.Add("Text", mScanSettingsVM.Config.ScanCaptureMode, "Text");
            this.btnCapture.DataBindings.Add("Pressed", mScanSettingsVM.Config.ScanCaptureMode, "IsEnabled");
            // 扫描头
            this.rbtnTwoScanners.DataBindings.Add("Text", mScanSettingsVM.Config.ScannerHeadTwoGalv, "Text");
            this.rbtnTwoScanners.DataBindings.Add("Checked", mScanSettingsVM.Config.ScannerHeadTwoGalv, "IsEnabled");
            this.rbtnThreeScanners.DataBindings.Add("Text", mScanSettingsVM.Config.ScannerHeadThreeGalv, "Text");
            this.rbtnThreeScanners.DataBindings.Add("Checked", mScanSettingsVM.Config.ScannerHeadThreeGalv, "IsEnabled");
            // 扫描模式
            this.rbtnGalvano.DataBindings.Add("Text", mScanSettingsVM.Config.ScanModeGalavano, "Text");
            this.rbtnGalvano.DataBindings.Add("Checked", mScanSettingsVM.Config.ScanModeGalavano, "IsEnabled");
            this.rbtnResonant.DataBindings.Add("Text", mScanSettingsVM.Config.ScanModeResonant, "Text");
            this.rbtnResonant.DataBindings.Add("Checked", mScanSettingsVM.Config.ScanModeResonant, "IsEnabled");
            // 扫描方向
            this.btnUniDirection.DataBindings.Add("Text", mScanSettingsVM.Config.ScanUniDirection, "Text");
            this.btnUniDirection.DataBindings.Add("Pressed", mScanSettingsVM.Config.ScanUniDirection, "IsEnabled");
            this.btnBiDirection.DataBindings.Add("Text", mScanSettingsVM.Config.ScanBiDirection, "Text");
            this.btnBiDirection.DataBindings.Add("Pressed", mScanSettingsVM.Config.ScanBiDirection, "IsEnabled");
            // 像素时间
            // 快速模式使能
            this.rbtnFastMode.DataBindings.Add("Pressed", mScanSettingsVM.Config, "FastModeEnabled");
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
            this.chbxLineSkip.DataBindings.Add("Checked", mScanSettingsVM.Config, "ScanLineSkipEnabled");
            this.cbxLineSkip.DataSource = mScanSettingsVM.Config.ScanLineSkipList;
            this.cbxLineSkip.DisplayMember = "Text";
            this.cbxLineSkip.ValueMember = "Data";
            this.cbxLineSkip.SelectedItem = mScanSettingsVM.Config.SelectedScanLineSkip;
            // 扫描通道1 - 405nm
            this.gh405.DataBindings.Add("BackColor", mScanSettingsVM.Config.ScanChannel405, "PseudoColor");
            this.gh405.DataBindings.Add("Collapsed", mScanSettingsVM.Config.ScanChannel405, "Collapsed");
            this.tbar405HV.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel405, "Gain");
            this.tbx405HV.DataBindings.Add("Text", tbar405HV, "Value");
            this.tbar405Offset.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel405, "Offset");
            this.tbx405Offset.DataBindings.Add("Text", tbar405Offset, "Value");
            this.tbar405Power.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel405, "LaserPower");
            this.tbx405Power.DataBindings.Add("Text", tbar405Power, "Value");
            this.btn405Power.DataBindings.Add("Pressed", mScanSettingsVM.Config.ScanChannel405, "Activated");
            // 扫描通道2 - 488nm
            this.gh488.DataBindings.Add("BackColor", mScanSettingsVM.Config.ScanChannel488, "PseudoColor");
            this.gh488.DataBindings.Add("Collapsed", mScanSettingsVM.Config.ScanChannel488, "Collapsed");
            this.tbar488HV.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel488, "Gain");
            this.tbx488HV.DataBindings.Add("Text", mScanSettingsVM.Config.ScanChannel488, "Gain");
            this.tbar488Offset.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel488, "Offset");
            this.tbx488Offset.DataBindings.Add("Text", mScanSettingsVM.Config.ScanChannel488, "Offset");
            this.tbar488Power.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel488, "LaserPower");
            this.tbx488Power.DataBindings.Add("Text", mScanSettingsVM.Config.ScanChannel488, "LaserPower");
            this.btn488Power.DataBindings.Add("Pressed", mScanSettingsVM.Config.ScanChannel488, "Activated");
            // 扫描通道3 - 561nm
            this.gh561.DataBindings.Add("BackColor", mScanSettingsVM.Config.ScanChannel561, "PseudoColor");
            this.gh561.DataBindings.Add("Collapsed", mScanSettingsVM.Config.ScanChannel561, "Collapsed");
            this.tbar561HV.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel561, "Gain");
            this.tbx561HV.DataBindings.Add("Text", mScanSettingsVM.Config.ScanChannel561, "Gain");
            this.tbar561Offset.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel561, "Offset");
            this.tbx561Offset.DataBindings.Add("Text", mScanSettingsVM.Config.ScanChannel561, "Offset");
            this.tbar561Power.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel561, "LaserPower");
            this.tbx561Power.DataBindings.Add("Text", mScanSettingsVM.Config.ScanChannel561, "LaserPower");
            this.btn561Power.DataBindings.Add("Pressed", mScanSettingsVM.Config.ScanChannel561, "Activated");
            // 扫描通道4 - 640nm
            this.gh640.DataBindings.Add("BackColor", mScanSettingsVM.Config.ScanChannel640, "PseudoColor");
            this.gh640.DataBindings.Add("Collapsed", mScanSettingsVM.Config.ScanChannel640, "Collapsed");
            this.tbar640HV.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel640, "Gain");
            this.tbx640HV.DataBindings.Add("Text", mScanSettingsVM.Config.ScanChannel640, "Gain");
            this.tbar640Offset.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel640, "Offset");
            this.tbx640Offset.DataBindings.Add("Text", mScanSettingsVM.Config.ScanChannel640, "Offset");
            this.tbar640Power.DataBindings.Add("Value", mScanSettingsVM.Config.ScanChannel640, "LaserPower");
            this.tbx640Power.DataBindings.Add("Text", mScanSettingsVM.Config.ScanChannel640, "LaserPower");
            this.btn640Power.DataBindings.Add("Pressed", mScanSettingsVM.Config.ScanChannel640, "Activated");
            // 小孔
            this.cbxPinHoleSelect.DataSource = mScanSettingsVM.Config.ScanPinHoleList;
            this.cbxPinHoleSelect.DisplayMember = "Name";
            this.cbxPinHoleSelect.ValueMember = "Size";
            this.cbxPinHoleSelect.SelectedItem = mScanSettingsVM.Config.SelectedPinHole;
            this.tbxPinHole.DataBindings.Add("Text", tbarPinHole, "Value");

            // 其他
        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormScanSettingsLoad(object sender, EventArgs e)
        {
            Initialize();
            RegisterEvents();
            SetDataBindings();
        }

        /// <summary>
        /// 扫描头切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScannerHeadChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.Config.ScannerHeadChangeCommand(rbtnTwoScanners.Checked);
            if (ScannerHeadModelChangedEvent != null)
            {
                ScannerHeadModelChangedEvent.Invoke(mScanSettingsVM.Config.SelectedScannerHead);
            }
        }

        /// <summary>
        /// 切换扫描模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanModeChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.Config.ScanModeChangeCommand(rbtnGalvano.Checked);
            if (ScanModeChangedEvent != null)
            {
                ScanModeChangedEvent.Invoke(mScanSettingsVM.Config.SelectedScanMode);
            }
        }

        /// <summary>
        /// 单向按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UniDirectionClick(object sender, EventArgs e)
        {
            if (mScanSettingsVM.Config.ScanUniDirection.IsEnabled)
            {
                btnUniDirection.Pressed = true;
                return;
            }
            btnBiDirection.Pressed = !btnUniDirection.Pressed;
            mScanSettingsVM.Config.ScanDirectionChangeCommand(btnUniDirection.Pressed);
            if (ScanDirectionChangedEvent != null)
            {
                ScanDirectionChangedEvent.Invoke(mScanSettingsVM.Config.SelectedScanDirection);
            }
        }

        /// <summary>
        /// 双向按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BiDirectionClick(object sender, EventArgs e)
        {
            if (mScanSettingsVM.Config.ScanBiDirection.IsEnabled)
            {
                btnBiDirection.Pressed = true;
                return;
            }
            btnUniDirection.Pressed = !btnBiDirection.Pressed;
            mScanSettingsVM.Config.ScanDirectionChangeCommand(btnUniDirection.Pressed);
            if (ScanDirectionChangedEvent != null)
            {
                ScanDirectionChangedEvent.Invoke(mScanSettingsVM.Config.SelectedScanDirection);
            }
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

            mScanSettingsVM.Config.ScanPixelDwellChangeCommand(model);

            if (ScanPixelDwellChangedEvent != null)
            {
                ScanPixelDwellChangedEvent.Invoke(model);
            }
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

            mScanSettingsVM.Config.ScanPixelChangeCommand(model);

            if (ScanPixelChangedEvent != null)
            {
                ScanPixelChangedEvent.Invoke(model);
            }
        }

        /// <summary>
        /// 切换跳行扫描事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanLineSkipChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.Config.LineSkipValueChangeCommand((ScanLineSkipModel)cbxLineSkip.SelectedItem);
            if (LineSkipChangedEvent != null)
            {
                LineSkipChangedEvent.Invoke(mScanSettingsVM.Config.SelectedScanLineSkip);
            }
        }

        /// <summary>
        /// 切换小孔事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanPinHoleChanged(object sender, EventArgs e)
        {
            this.tbarPinHole.DataBindings.Clear();
            mScanSettingsVM.Config.PinHoleSelectChangeCommand((ScanPinHoleModel)cbxPinHoleSelect.SelectedItem);
            this.tbarPinHole.DataBindings.Add("Value", mScanSettingsVM.Config.SelectedPinHole, "Size");
        }

        /// <summary>
        /// 实时采集模式点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LiveClick(object sender, EventArgs e)
        {
            if (btnLive.Pressed && btnCapture.Pressed)
            {
                btnCapture.Pressed = false;
            }
            mScanSettingsVM.Config.ScanAcquisitionChangeCommand(btnLive.Pressed, btnCapture.Pressed);
            if (ScanAcquisitionChangedEvent != null)
            {
                ScanAcquisitionChangedEvent.Invoke(mScanSettingsVM.Config.SelectedScanAcquisition);
            }
        }

        /// <summary>
        /// 捕捉模式点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CaptureClick(object sender, EventArgs e)
        {
            if (btnLive.Pressed && btnCapture.Pressed)
            {
                btnLive.Pressed = false;
            }
            mScanSettingsVM.Config.ScanAcquisitionChangeCommand(btnLive.Pressed, btnCapture.Pressed);
            if (ScanAcquisitionChangedEvent != null)
            {
                ScanAcquisitionChangedEvent.Invoke(mScanSettingsVM.Config.SelectedScanAcquisition);
            }
        }

        /// <summary>
        /// 快速模式使能点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FastModeClick(object sender, EventArgs e)
        {
            mScanSettingsVM.Config.FastModeEnabled = rbtnFastMode.Pressed;
            Logger.Info(string.Format("Fast Mode Enabled [{0}].", mScanSettingsVM.Config.FastModeEnabled));
        }

        /// <summary>
        /// 跳行扫描使能变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineSkipCheckedChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.Config.LineSkipEnableChangeCommand(chbxLineSkip.Checked);
            if (LineSkipEnableChangedEvent != null)
            {
                LineSkipEnableChangedEvent.Invoke(mScanSettingsVM.Config.ScanLineSkipEnabled);
            }
        }

        /// <summary>
        /// 小孔孔径变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PinHoleValueChanged(object sender, EventArgs e)
        {
            mScanSettingsVM.Config.PinHoleValueChangeCommand(tbarPinHole.Value);
            if (PinHoleChangedEvent != null)
            {
                PinHoleChangedEvent.Invoke(mScanSettingsVM.Config.SelectedPinHole.ID, mScanSettingsVM.Config.SelectedPinHole.Size);
            }
        }

        /// <summary>
        /// 通道增益更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelGainChanged(object sender, EventArgs e)
        {
            InputTrackBar bar = (InputTrackBar)sender;
            mScanSettingsVM.Config.ChannelGainChangeCommand((int)bar.Tag, bar.Value);
            if (ChannelGainChangedEvent != null)
            {
                ChannelGainChangedEvent.Invoke((int)bar.Tag, bar.Value);
            }
        }

        /// <summary>
        /// 通道偏置更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelOffsetChanged(object sender, EventArgs e)
        {
            InputTrackBar bar = (InputTrackBar)sender;
            mScanSettingsVM.Config.ChannelOffsetChangeCommand((int)bar.Tag, bar.Value);
            if (ChannelOffsetChangedEvent != null)
            {
                ChannelOffsetChangedEvent.Invoke((int)bar.Tag, bar.Value);
            }
        }

        /// <summary>
        /// 通道功率更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelPowerChanged(object sender, EventArgs e)
        {
            InputTrackBar bar = (InputTrackBar)sender;
            mScanSettingsVM.Config.ChannelPowerChangeCommand((int)bar.Tag, bar.Value);
            if (ChannelPowerChangedEvent != null)
            {
                ChannelPowerChangedEvent.Invoke((int)bar.Tag, bar.Value);
            }
        }

        /// <summary>
        /// 通道激光开关事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelActivateChanged(object sender, EventArgs e)
        {
            InputButton button = (InputButton)sender;
            mScanSettingsVM.Config.ChannelActivateChangeCommand((int)button.Tag, button.Pressed);
            if (ChannelActivateChangedEvent != null)
            {
                ChannelActivateChangedEvent.Invoke((int)button.Tag, button.Pressed);
            }
        }
    }
}
