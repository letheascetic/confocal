using confocal_core.Common;
using confocal_core.Model;
using confocal_core.Properties;
using Emgu.CV;
using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace confocal_core.ViewModel
{
    public class ConfigViewModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private volatile static ConfigViewModel pConfig = null;
        private static readonly object locker = new object();
        private static readonly int CHAN_NUM = 4;
        ///////////////////////////////////////////////////////////////////////////////////////////
        public bool Debugging { get; set; }
        public double InputSampleRate { get; set; }                         // 像素速率[采样速率]
        ///////////////////////////////////////////////////////////////////////////////////////////
        public event ScanAreaChangedEventHandler ScanAreaChangedEvent;
        public event ScanAreaChangedEventHandler FullScanAreaChangedEvent;
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
        private ScanAcquisitionModel scanLiveMode;
        private ScanAcquisitionModel scanCaptureMode;

        /// <summary>
        /// 实时模式
        /// </summary>
        public ScanAcquisitionModel ScanLiveMode
        {
            get { return scanLiveMode; }
            set { scanLiveMode = value; RaisePropertyChanged(() => ScanLiveMode); }
        }
        /// <summary>
        /// 捕捉模式
        /// </summary>
        public ScanAcquisitionModel ScanCaptureMode
        {
            get { return scanCaptureMode; }
            set { scanCaptureMode = value; RaisePropertyChanged(() => ScanCaptureMode); }
        }

        /// <summary>
        /// 扫描状态
        /// </summary>
        public bool IsScanning
        {
            get { return ScanLiveMode.IsEnabled || ScanCaptureMode.IsEnabled; }
        }
        /// <summary>
        /// 当前的采集模式
        /// </summary>
        public ScanAcquisitionModel SelectedScanAcquisition
        {
            get
            {
                if (!IsScanning)
                {
                    return null;
                }
                return ScanLiveMode.IsEnabled ? ScanLiveMode : ScanCaptureMode;
            }
        }

        /// <summary>
        /// 采集模式状态[启动、切换、停止]变化事件处理
        /// </summary>
        /// <param name="liveModeEnabled"></param>
        /// <param name="captureModeEnabled"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanAcquisitionChangeCommand(bool liveModeEnabled, bool captureModeEnabled)
        {
            BeforePropertyChanged();

            // 更新状态
            ScanLiveMode.IsEnabled = liveModeEnabled;
            ScanCaptureMode.IsEnabled = captureModeEnabled;
            Logger.Info(string.Format("Scan Acquisition Mode [{0}:{1}].", IsScanning, IsScanning ? SelectedScanAcquisition.Text : "None"));

            AfterPropertyChanged();

            if (ScanAcquisitionChangedEvent != null)
            {
                return ScanAcquisitionChangedEvent.Invoke(SelectedScanAcquisition);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScannerHeadModel scannerHeadTwoGalv;
        private ScannerHeadModel scannerHeadThreeGalv;

        /// <summary>
        /// 双镜
        /// </summary>
        public ScannerHeadModel ScannerHeadTwoGalv
        {
            get { return scannerHeadTwoGalv; }
            set { scannerHeadTwoGalv = value; RaisePropertyChanged(() => ScannerHeadTwoGalv); }
        }
        /// <summary>
        /// 三镜
        /// </summary>
        public ScannerHeadModel ScannerHeadThreeGalv
        {
            get { return scannerHeadThreeGalv; }
            set { scannerHeadThreeGalv = value; RaisePropertyChanged(() => ScannerHeadThreeGalv); }
        }
        /// <summary>
        /// 选择的扫描头
        /// </summary>
        public ScannerHeadModel SelectedScannerHead
        {
            get { return ScannerHeadTwoGalv.IsEnabled ? ScannerHeadTwoGalv : ScannerHeadThreeGalv; }
        }

        /// <summary>
        /// 扫描头切换事件处理
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE ScannerHeadChangeCommand(bool twoGalvEnabled)
        {
            BeforePropertyChanged();

            // 更新扫描头
            ScannerHeadTwoGalv.IsEnabled = twoGalvEnabled;
            ScannerHeadThreeGalv.IsEnabled = !twoGalvEnabled;
            Logger.Info(string.Format("Scan Header [{0}].", SelectedScannerHead.Text));

            AfterPropertyChanged();

            if (ScannerHeadModelChangedEvent != null)
            {
                return ScannerHeadModelChangedEvent.Invoke(SelectedScannerHead);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScanDirectionModel scanUniDirection;
        private ScanDirectionModel scanBiDirection;

        /// <summary>
        /// 选择的扫描方向
        /// </summary>
        public ScanDirectionModel SelectedScanDirection
        {
            get { return ScanUniDirection.IsEnabled ? ScanUniDirection : ScanBiDirection; }
        }
        /// <summary>
        /// 单向
        /// </summary>
        public ScanDirectionModel ScanUniDirection
        {
            get { return scanUniDirection; }
            set { scanUniDirection = value; RaisePropertyChanged(() => ScanUniDirection); }
        }
        /// <summary>
        /// 双向
        /// </summary>
        public ScanDirectionModel ScanBiDirection
        {
            get { return scanBiDirection; }
            set { scanBiDirection = value; RaisePropertyChanged(() => ScanBiDirection); }
        }

        /// <summary>
        /// 扫描方向切换事件
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE ScanDirectionChangeCommand(bool uniDirectionEnabled)
        {
            BeforePropertyChanged();

            // 更新扫描方向
            ScanUniDirection.IsEnabled = uniDirectionEnabled;
            ScanBiDirection.IsEnabled = !uniDirectionEnabled;
            Logger.Info(string.Format("Scan Direction [{0}].", SelectedScanDirection.Text));

            AfterPropertyChanged();

            if (ScanDirectionChangedEvent != null)
            {
                return ScanDirectionChangedEvent.Invoke(SelectedScanDirection);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScanModeModel scanModeGalvano;
        private ScanModeModel scanModeResonant;

        /// <summary>
        /// 选择的扫描模式
        /// </summary>
        public ScanModeModel SelectedScanMode
        {
            get { return ScanModeGalavano.IsEnabled ? ScanModeGalavano : ScanModeResonant; }
        }
        /// <summary>
        /// Galvano扫描模式
        /// </summary>
        public ScanModeModel ScanModeGalavano
        {
            get { return scanModeGalvano; }
            set { scanModeGalvano = value; RaisePropertyChanged(() => ScanModeGalavano); }
        }
        /// <summary>
        /// Resonant扫描模式
        /// </summary>
        public ScanModeModel ScanModeResonant
        {
            get { return scanModeResonant; }
            set { scanModeResonant = value; RaisePropertyChanged(() => ScanModeResonant); }
        }

        /// <summary>
        /// 扫描模式切换事件
        /// </summary>
        /// <param name="galvEnabled"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanModeChangeCommand(bool galvEnabled)
        {
            BeforePropertyChanged();

            // 更新扫描模式
            ScanModeGalavano.IsEnabled = galvEnabled;
            ScanModeResonant.IsEnabled = !galvEnabled;
            Logger.Info(string.Format("Scan Mode [{0}].", SelectedScanMode.Text));

            AfterPropertyChanged();

            if (ScanModeChangedEvent != null)
            {
                return ScanModeChangedEvent.Invoke(SelectedScanMode);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanPixelModel> scanPixelList;
        private ScanPixelModel selectedScanPixel;

        /// <summary>
        /// 扫描像素列表
        /// </summary>
        public List<ScanPixelModel> ScanPixelList
        {
            get { return scanPixelList; }
            set { scanPixelList = value; RaisePropertyChanged(() => ScanPixelList); }
        }
        /// <summary>
        /// 选择的扫描像素 
        /// </summary>
        public ScanPixelModel SelectedScanPixel
        {
            get { return selectedScanPixel; }
            set { selectedScanPixel = value; RaisePropertyChanged(() => SelectedScanPixel); }
        }

        /// <summary>
        /// 扫描像素切换事件
        /// </summary>
        /// <param name="selectedScanPixel"></param>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelChangeCommand(ScanPixelModel selectedScanPixel)
        {
            BeforePropertyChanged();

            foreach (ScanPixelModel scanPixel in ScanPixelList)
            {
                if (scanPixel.ID != selectedScanPixel.ID)
                {
                    scanPixel.IsEnabled = false;
                }
                else
                {
                    scanPixel.IsEnabled = true;
                }
            }
            SelectedScanPixel = selectedScanPixel;
            ScanPixelSize = SelectedScanArea.ScanRange.Width / SelectedScanPixel.Data;
            Logger.Info(string.Format("Scan Pixel [{0}].", SelectedScanPixel.Text));

            AfterPropertyChanged();

            if (ScanPixelChangedEvent != null)
            {
                return ScanPixelChangedEvent.Invoke(SelectedScanPixel);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private bool fastModeEnabled;
        private List<ScanPixelDwellModel> scanPixelDwellList;
        private ScanPixelDwellModel selectedScanPixelDwell;

        /// <summary>
        /// 快速模式使能
        /// </summary>
        public bool FastModeEnabled
        {
            get { return fastModeEnabled; }
            set { fastModeEnabled = value; RaisePropertyChanged(() => FastModeEnabled); }
        }
        /// <summary>
        /// 像素停留时间列表
        /// </summary>
        public List<ScanPixelDwellModel> ScanPixelDwellList
        {
            get { return scanPixelDwellList; }
            set { scanPixelDwellList = value; RaisePropertyChanged(() => ScanPixelDwellList); }
        }
        /// <summary>
        /// 选择的像素停留时间
        /// </summary>
        public ScanPixelDwellModel SelectedScanPixelDwell
        {
            get { return selectedScanPixelDwell; }
            set { selectedScanPixelDwell = value; RaisePropertyChanged(() => SelectedScanPixelDwell); }
        }

        /// <summary>
        /// 像素时间变更事件
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE ScanPixelDwellChangeCommand(ScanPixelDwellModel selectedPixelDwell)
        {
            BeforePropertyChanged();

            foreach (ScanPixelDwellModel pixelDwell in ScanPixelDwellList)
            {
                if (pixelDwell.ID != selectedPixelDwell.ID)
                {
                    pixelDwell.IsEnabled = false;
                }
            }
            selectedPixelDwell.IsEnabled = true;
            SelectedScanPixelDwell = selectedPixelDwell;
            Logger.Info(string.Format("Scan Pixel Dwell [{0}].", SelectedScanPixelDwell.Text));

            AfterPropertyChanged();

            if (ScanPixelDwellChangedEvent != null)
            {
                ScanPixelDwellChangedEvent.Invoke(SelectedScanPixelDwell);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private bool scanLineSkipEnabled;
        private ScanLineSkipModel selectedScanLineSkip;
        private List<ScanLineSkipModel> scanLineSkipList;

        /// <summary>
        /// 跳行扫描使能
        /// </summary>
        public bool ScanLineSkipEnabled
        {
            get { return scanLineSkipEnabled; }
            set { scanLineSkipEnabled = value; RaisePropertyChanged(() => ScanLineSkipEnabled); }
        }
        /// <summary>
        /// 选择的跳行扫描
        /// </summary>
        public ScanLineSkipModel SelectedScanLineSkip
        {
            get { return selectedScanLineSkip; }
            set { selectedScanLineSkip = value; RaisePropertyChanged(() => SelectedScanLineSkip); }
        }
        /// <summary>
        /// 跳行扫描列表
        /// </summary>
        public List<ScanLineSkipModel> ScanLineSkipList
        {
            get { return scanLineSkipList; }
            set { scanLineSkipList = value; RaisePropertyChanged(() => SelectedScanLineSkip); }
        }

        /// <summary>
        /// 跳行扫描使能变更事件
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE LineSkipEnableChangeCommand(bool lineSkipEnabled)
        {
            BeforePropertyChanged();

            ScanLineSkipEnabled = lineSkipEnabled;
            Logger.Info(string.Format("Scan Line Skip [{0}:{1}].", ScanLineSkipEnabled, SelectedScanLineSkip.Text));

            AfterPropertyChanged();

            if (LineSkipEnableChangedEvent != null)
            {
                return LineSkipEnableChangedEvent.Invoke(ScanLineSkipEnabled);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 跳行扫描值变更事件
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE LineSkipValueChangeCommand(ScanLineSkipModel lineSkip)
        {
            BeforePropertyChanged();

            SelectedScanLineSkip = lineSkip;
            Logger.Info(string.Format("Scan Line Skip [{0}:{1}].", ScanLineSkipEnabled, SelectedScanLineSkip.Text));

            AfterPropertyChanged();

            if (LineSkipChangedEvent != null)
            {
                LineSkipChangedEvent.Invoke(SelectedScanLineSkip);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScanChannelModel scanChannel405;
        private ScanChannelModel scanChannel488;
        private ScanChannelModel scanChannel561;
        private ScanChannelModel scanChannel640;
        private ScanChannelModel[] scanChannels;

        /// <summary>
        /// 405nm通道
        /// </summary>
        public ScanChannelModel ScanChannel405
        {
            get { return scanChannel405; }
            set { scanChannel405 = value; RaisePropertyChanged(() => ScanChannel405); }
        }
        /// <summary>
        /// 488nm通道
        /// </summary>
        public ScanChannelModel ScanChannel488
        {
            get { return scanChannel488; }
            set { scanChannel488 = value; RaisePropertyChanged(() => ScanChannel488); }
        }
        /// <summary>
        /// 561nm通道
        /// </summary>
        public ScanChannelModel ScanChannel561
        {
            get { return scanChannel561; }
            set { scanChannel561 = value; RaisePropertyChanged(() => ScanChannel561); }
        }
        /// <summary>
        /// 640nm通道
        /// </summary>
        public ScanChannelModel ScanChannel640
        {
            get { return scanChannel640; }
            set { scanChannel640 = value; RaisePropertyChanged(() => ScanChannel640); }
        }
        public ScanChannelModel[] ScanChannels
        {
            get { return scanChannels; }
            set { scanChannels = value; }
        }

        /// <summary>
        /// 通道增益更新事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gain"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelGainChangeCommand(int id, int gain)
        {
            ScanChannelModel channel = FindScanChannel(id);
            channel.Gain = gain;
            UsbDac.SetDacOut((uint)id, UsbDac.ConfigValueToVout(gain));
            Logger.Info(string.Format("Channel Gain [{0}:{1}].", id, gain));        // 设置

            if (ChannelGainChangedEvent != null)
            {
                return ChannelGainChangedEvent.Invoke(channel);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 通道偏置更新事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelOffsetChangeCommand(int id, int offset)
        {
            ScanChannelModel channel = FindScanChannel(id);
            channel.Offset = offset;
            Logger.Info(string.Format("Channel Offset [{0}:{1}].", id, offset));
            if (ChannelOffsetChangedEvent != null)
            {
                return ChannelOffsetChangedEvent.Invoke(channel);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 通道功率更新事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelPowerChangeCommand(int id, int power)
        {
            ScanChannelModel channel = FindScanChannel(id);
            channel.LaserPower = power;
            Logger.Info(string.Format("Channel Power [{0}:{1}].", id, power));
            if (ChannelPowerChangedEvent != null)
            {
                ChannelPowerChangedEvent.Invoke(channel);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 通道激光开关事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activated"></param>
        /// <returns></returns>
        public API_RETURN_CODE ChannelActivateChangeCommand(int id, bool activated)
        {
            BeforePropertyChanged();

            ScanChannelModel channel = FindScanChannel(id);
            channel.Activated = activated;
            Logger.Info(string.Format("Channel Status [{0}:{1}].", id, activated));

            AfterPropertyChanged();
            if (ChannelActivateChangedEvent != null)
            {
                ChannelActivateChangedEvent.Invoke(channel);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanPinHoleModel> scanPinHoleList;
        private ScanPinHoleModel selectedPinHole;

        /// <summary>
        /// 小孔列表
        /// </summary>
        public List<ScanPinHoleModel> ScanPinHoleList
        {
            get { return scanPinHoleList; }
            set { scanPinHoleList = value; RaisePropertyChanged(() => ScanPinHoleList); }
        }
        /// <summary>
        /// 选择的小孔
        /// </summary>
        public ScanPinHoleModel SelectedPinHole
        {
            get { return selectedPinHole; }
            set { selectedPinHole = value; RaisePropertyChanged(() => SelectedPinHole); }
        }

        /// <summary>
        /// 小孔切换事件
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE PinHoleSelectChangeCommand(ScanPinHoleModel selected)
        {
            // BeforePropertyChanged();

            SelectedPinHole = selected;
            Logger.Info(string.Format("Scan Pin Hole [{0}:{1}].", SelectedPinHole.Name, SelectedPinHole.Size));

            // AfterPropertyChanged();
            return API_RETURN_CODE.API_SUCCESS;
        }
        /// <summary>
        /// 小孔孔径变化事件
        /// </summary>
        /// <returns></returns>
        public API_RETURN_CODE PinHoleValueChangeCommand(int value)
        {
            // TO DO: 操作小孔孔径变化

            SelectedPinHole.Size = value;
            Logger.Info(string.Format("Scan Pin Hole [{0}:{1}].", SelectedPinHole.Name, SelectedPinHole.Size));

            if (PinHoleChangedEvent != null)
            {
                PinHoleChangedEvent.Invoke(SelectedPinHole);
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanAreaTypeModel> scanAreaTypeList;

        /// <summary>
        /// 扫描区域类型列表
        /// </summary>
        public List<ScanAreaTypeModel> ScaAreaTypeList
        {
            get { return scanAreaTypeList; }
            set { scanAreaTypeList = value; RaisePropertyChanged(() => scanAreaTypeList); }
        }
        /// <summary>
        /// 选择的扫描区域类型
        /// </summary>
        public ScanAreaTypeModel SelectedScanAreaType
        {
            get { return ScaAreaTypeList.Where(p => p.IsEnabled).First(); }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScanAreaModel selectedScanArea;
        private ScanAreaModel fullScanArea;
        private float scanPixelSize;

        /// <summary>
        /// 当前选择的扫描区域
        /// </summary>
        public ScanAreaModel SelectedScanArea
        {
            get { return selectedScanArea; }
            set { selectedScanArea = value; RaisePropertyChanged(() => SelectedScanArea); }
        }
        /// <summary>
        /// 全视场
        /// </summary>
        public ScanAreaModel FullScanArea
        {
            get { return fullScanArea; }
            set { fullScanArea = value; RaisePropertyChanged(() => FullScanArea); }
        }
        /// <summary>
        /// 扫描像素尺寸
        /// </summary>
        public float ScanPixelSize
        {
            get { return scanPixelSize; }
            set { scanPixelSize = value; RaisePropertyChanged(() => ScanPixelSize); }
        }

        public API_RETURN_CODE ScanAreaChangeCommand(ScanAreaModel scanArea)
        {
            SelectedScanArea.Update(scanArea.ScanRange);
            ScanPixelSize = SelectedScanArea.ScanRange.Width / SelectedScanPixel.Data;
            Logger.Info(string.Format("Selected Scan Area [{0}].", SelectedScanArea.ScanRange));

            if (ScanAreaChangedEvent != null)
            {
                ScanAreaChangedEvent.Invoke(SelectedScanArea);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE FullScanAreaChangeCommand(ScanAreaModel fullScanArea)
        {
            FullScanArea.Update(fullScanArea.ScanRange);
            Logger.Info(string.Format("Full Scan Area [{0}].", FullScanArea.ScanRange));

            if (FullScanAreaChangedEvent != null)
            {
                FullScanAreaChangedEvent.Invoke(FullScanArea);
            }

            return API_RETURN_CODE.API_SUCCESS;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        private GalvoPropertyModel mGalvoPrpperty;
        private DetectorModel mDetector;

        /// <summary>
        /// 振镜属性
        /// </summary>
        public GalvoPropertyModel GalvoProperty
        {
            get { return mGalvoPrpperty; }
            set { mGalvoPrpperty = value; RaisePropertyChanged(() => GalvoProperty); }
        }
        /// <summary>
        /// 探测器属性
        /// </summary>
        public DetectorModel Detector
        {
            get { return mDetector; }
            set { mDetector = value; RaisePropertyChanged(() => Detector); }
        }

        public API_RETURN_CODE XGalvoChannelChangeCommand(string xGalvoChannel)
        {
            GalvoProperty.XGalvoAoChannel = xGalvoChannel;
            Logger.Info(string.Format("X Galvo Ao Channel [{0}].", GalvoProperty.XGalvoAoChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE YGalvoChannelChangeCommand(string yGalvoChannel)
        {
            GalvoProperty.YGalvoAoChannel = yGalvoChannel;
            Logger.Info(string.Format("Y Galvo Ao Channel [{0}].", GalvoProperty.YGalvoAoChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE Y2GalvoChannelChangeCommand(string y2GalvoChannel)
        {
            GalvoProperty.Y2GalvoAoChannel = y2GalvoChannel;
            Logger.Info(string.Format("Y2 Galvo Ao Channel [{0}].", GalvoProperty.Y2GalvoAoChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE XGalvoOffsetVoltageChangeCommand(double offsetVoltage)
        {
            GalvoProperty.XGalvoOffsetVoltage = offsetVoltage;
            Logger.Info(string.Format("X Galvo Offset Voltage [{0}].", GalvoProperty.XGalvoOffsetVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE XGalvoCalibrationVoltageChangeCommand(double calibrationVoltage)
        {
            GalvoProperty.XGalvoCalibrationVoltage = calibrationVoltage;
            Logger.Info(string.Format("X Galvo Calibration Voltage [{0}].", GalvoProperty.XGalvoCalibrationVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE YGalvoOffsetVoltageChangeCommand(double offsetVoltage)
        {
            GalvoProperty.YGalvoOffsetVoltage = offsetVoltage;
            Logger.Info(string.Format("Y Galvo Offset Voltage [{0}].", GalvoProperty.YGalvoOffsetVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE YGalvoCalibrationVoltageChangeCommand(double calibrationVoltage)
        {
            GalvoProperty.YGalvoCalibrationVoltage = calibrationVoltage;
            Logger.Info(string.Format("Y Galvo Calibration Voltage [{0}].", GalvoProperty.YGalvoCalibrationVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE GalvoResponseTimeChangeCommand(double responseTime)
        {
            GalvoProperty.GalvoResponseTime = responseTime;
            Logger.Info(string.Format("Galvo Response Time [{0}].", GalvoProperty.GalvoResponseTime));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE DetectorModeChangeCommand(bool pmtModeEnabled)
        {
            Detector.DetectorPmt.IsEnabled = pmtModeEnabled;
            Detector.DetectorApd.IsEnabled = !pmtModeEnabled;
            Logger.Info(string.Format("Detector Mode [{0}].", Detector.CurrentDetecor.Text));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE PmtChannelChangeCommand(int id, string pmtChannel)
        {
            PmtChannelModel channel = FindPmtChannel(id);
            channel.AiChannel = pmtChannel;
            Logger.Info(string.Format("Pmt [{0}] Ao Channel [{1}].", channel.ID, channel.AiChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ApdSourceChangeCommand(int id, string apdSource)
        {
            ApdChannelModel channel = FindApdChannel(id);
            channel.CiSource = apdSource;
            Logger.Info(string.Format("Apd [{0}] Ci Channel [{1}:{2}].", channel.ID, channel.CiSource, channel.CiChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ApdChannelChangeCommand(int id, string apdChannel)
        {
            ApdChannelModel channel = FindApdChannel(id);
            channel.CiChannel = apdChannel;
            Logger.Info(string.Format("Apd [{0}] Ci Channel [{1}:{2}].", channel.ID, channel.CiSource, channel.CiChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE StartTriggerChangeCommand(string startTrigger)
        {
            Detector.StartTrigger = startTrigger;
            Logger.Info(string.Format("Start Trigger [{0}].", Detector.StartTrigger));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE TriggerSignalChangeCommand(string triggerSignal)
        {
            Detector.TriggerSignal = triggerSignal;
            Logger.Info(string.Format("Trigger Signal [{0}].", Detector.TriggerSignal));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE TriggerReceiverChangeCommand(string triggerReceive)
        {
            Detector.TriggerReceive = triggerReceive;
            Logger.Info(string.Format("Trigger Receiver [{0}].", Detector.TriggerReceive));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public PmtChannelModel FindPmtChannel(int id)
        {
            switch (id)
            {
                case 0:
                    return Detector.PmtChannel405;
                case 1:
                    return Detector.PmtChannel488;
                case 2:
                    return Detector.PmtChannel561;
                case 3:
                    return Detector.PmtChannel640;
                default:
                    throw new ArgumentOutOfRangeException("ID Exception.");
            }
        }

        public ApdChannelModel FindApdChannel(int id)
        {
            switch (id)
            {
                case 0:
                    return Detector.ApdChannel405;
                case 1:
                    return Detector.ApdChannel488;
                case 2:
                    return Detector.ApdChannel561;
                case 3:
                    return Detector.ApdChannel640;
                default:
                    throw new ArgumentOutOfRangeException("ID Exception.");
            }
        }

        public API_RETURN_CODE FullScanAreaChangeCommand(float scanRange)
        {
            FullScanArea.Update(new System.Drawing.RectangleF(-scanRange / 2, -scanRange / 2, scanRange, scanRange));
            Logger.Info(string.Format("Full Scan Area [{0}].", FullScanArea.ScanRange));
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private string mLaserPort;

        public string LaserPort
        {
            get { return mLaserPort; }
            set { mLaserPort = value; RaisePropertyChanged(() => LaserPort); }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        public static ConfigViewModel GetConfig()
        {
            if (pConfig == null)
            {
                lock (locker)
                {
                    if (pConfig == null)
                    {
                        pConfig = new ConfigViewModel();
                    }
                }
            }
            return pConfig;
        }

        /// <summary>
        /// 通道数量
        /// </summary>
        /// <returns></returns>
        public int GetChannelNum()
        {
            return CHAN_NUM;
        }

        /// <summary>
        /// 当前激活的通道数
        /// </summary>
        /// <returns></returns>
        public int GetActivatedChannelNum()
        {
            int activatedChannelNum = 0;
            activatedChannelNum += ScanChannel405.Activated ? 1 : 0;
            activatedChannelNum += ScanChannel488.Activated ? 1 : 0;
            activatedChannelNum += ScanChannel561.Activated ? 1 : 0;
            activatedChannelNum += ScanChannel640.Activated ? 1 : 0;
            return activatedChannelNum;
        }

        public ScanChannelModel FindScanChannel(int id)
        {
            switch (id)
            {
                case 0:
                    return ScanChannel405;
                case 1:
                    return ScanChannel488;
                case 2:
                    return ScanChannel561;
                case 3:
                    return ScanChannel640;
                default:
                    throw new ArgumentOutOfRangeException("ID Exception.");
            }
        }

        /// <summary>
        /// 扩展后的扫描范围
        /// </summary>
        /// <returns></returns>
        public ScanAreaModel GetExtendScanArea()
        {
            RectangleF scanRange = SelectedScanArea.ScanRange;
            float xExtendRange = ScanAreaModel.ExtendLineTime * scanRange.Width / (SelectedScanPixelDwell.Data * SelectedScanPixel.Data);
            float yExtendRange = ScanAreaModel.ExtendRowCount * ScanPixelSize;
            return new ScanAreaModel(new RectangleF(scanRange.X - xExtendRange / 2, scanRange.Y - yExtendRange / 2, scanRange.Width + xExtendRange, scanRange.Height + yExtendRange));
        }

        /// <summary>
        /// 扩展后的X像素数
        /// </summary>
        /// <returns></returns>
        public int GetExtendScanXPixels()
        {
            return SelectedScanPixel.Data + (ScanAreaModel.ExtendLineTime >> 1) / SelectedScanPixelDwell.Data * 2;
        }

        /// <summary>
        /// 扩展后的Y像素数
        /// </summary>
        /// <returns></returns>
        public int GetExtendScanYPixels()
        {
            return SelectedScanPixel.Data + ScanAreaModel.ExtendRowCount;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private ConfigViewModel()
        {
            // 采集模式
            ScanLiveMode = ScanAcquisitionModel.Initialize(ScanAcquisitionModel.LIVE);
            ScanCaptureMode = ScanAcquisitionModel.Initialize(ScanAcquisitionModel.CAPTURE);
            // 扫描头
            ScannerHeadTwoGalv = ScannerHeadModel.Initialize(ScannerHeadModel.TWO_SCANNERS);
            ScannerHeadThreeGalv = ScannerHeadModel.Initialize(ScannerHeadModel.THREE_SCANNERS);
            // 扫描模式
            ScanModeResonant = ScanModeModel.Initialize(ScanModeModel.RESONANT);
            ScanModeGalavano = ScanModeModel.Initialize(ScanModeModel.GALVANO);
            // 扫描方向
            ScanUniDirection = ScanDirectionModel.Initialize(ScanDirectionModel.UNIDIRECTION);
            ScanBiDirection = ScanDirectionModel.Initialize(ScanDirectionModel.BIDIRECTION);
            // 像素时间
            FastModeEnabled = false;
            ScanPixelDwellList = ScanPixelDwellModel.Initialize();
            SelectedScanPixelDwell = ScanPixelDwellList.Where(p => p.IsEnabled).First();
            // 扫描像素
            ScanPixelList = ScanPixelModel.Initialize();
            SelectedScanPixel = ScanPixelList.Where(p => p.IsEnabled).First();
            // 跳行扫描
            ScanLineSkipEnabled = Settings.Default.ScanLineSkipEnabled;
            ScanLineSkipList = ScanLineSkipModel.Initialize();
            SelectedScanLineSkip = ScanLineSkipList.Where(p => p.ID == Settings.Default.ScanLineSkip).First();
            // 扫描通道
            ScanChannel405 = ScanChannelModel.Initialize(ScanChannelModel.CHANNEL405);
            ScanChannel488 = ScanChannelModel.Initialize(ScanChannelModel.CHANNEL488);
            ScanChannel561 = ScanChannelModel.Initialize(ScanChannelModel.CHANNEL561);
            ScanChannel640 = ScanChannelModel.Initialize(ScanChannelModel.CHANNEL640);
            ScanChannels = new ScanChannelModel[] { ScanChannel405, ScanChannel488, ScanChannel561, ScanChannel640 };
            // 小孔
            ScanPinHoleList = ScanPinHoleModel.Initialize();
            SelectedPinHole = ScanPinHoleList[0];
            // 扫描类型 & 范围
            ScaAreaTypeList = new List<ScanAreaTypeModel>()
            {
                ScanAreaTypeModel.Initialize(ScanAreaTypeModel.SQUARE),
                ScanAreaTypeModel.Initialize(ScanAreaTypeModel.BANK)
            };
            FullScanArea = ScanAreaModel.CreateFullScanArea();
            SelectedScanArea = ScanAreaModel.CreateFullScanArea();
            // 像素尺寸
            ScanPixelSize = SelectedScanArea.ScanRange.Width / SelectedScanPixel.Data;
            // 振镜参数和探测器参数
            GalvoProperty = new GalvoPropertyModel();
            Detector = new DetectorModel();
            // 激光端口
            LaserPort = "COM2";
        }

        /// <summary>
        /// 在属性变化前执行
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE BeforePropertyChanged()
        {
            // 如果当前正在采集(有任一采集模式使能)，则先停止采集
            if (IsScanning)
            {
                // TO DO：停止采集
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

        /// <summary>
        /// 在属性变化后执行
        /// </summary>
        /// <returns></returns>
        private API_RETURN_CODE AfterPropertyChanged()
        {
            // 如果有任一采集模式使能，则重新开启采集
            if (IsScanning)
            {
                // TO DO：开启指定的采集模式
            }
            return API_RETURN_CODE.API_SUCCESS;
        }

    }
}
