using confocal_core.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.ViewModel
{
    public class ScanSettingsViewModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
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
        /// 当前采集的模式
        /// </summary>
        public ScanAcquisitionModel CurrentAcquisitionMode
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
            Logger.Info(string.Format("Scan Acquisition Mode [{0}:{1}].", IsScanning, IsScanning ? CurrentAcquisitionMode.Text : "None"));

            AfterPropertyChanged();
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScannerHeadModel scannerHeadTwoGalv;
        private ScannerHeadModel scannerHeadThreeGalv;

        /// <summary>
        /// 选择的扫描头
        /// </summary>
        public ScannerHeadModel SelectedScannerHead
        {
            get { return ScannerHeadTwoGalv.IsEnabled ? ScannerHeadTwoGalv : ScannerHeadThreeGalv; }
        }
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
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanPixelModel> scanPixelList;

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
            get { return ScanPixelList.Where(p => p.IsEnabled).First(); }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private bool fastModeEnabled;
        private List<ScanPixelDwellModel> scanPixelDwellList;

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
            get { return scanPixelDwellList.Where(p => p.IsEnabled).First(); }
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
            Logger.Info(string.Format("Scan Pixel Dwell [{0}].", SelectedScanPixelDwell.Text));

            AfterPropertyChanged();
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
            return API_RETURN_CODE.API_SUCCESS;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private ScanChannelModel scanChannel405;
        private ScanChannelModel scanChannel488;
        private ScanChannelModel scanChannel561;
        private ScanChannelModel scanChannel640;

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

        ///////////////////////////////////////////////////////////////////////////////////////////
        public ScanSettingsViewModel()
        {
            // 采集模式
            ScanLiveMode = ScanAcquisitionModel.Initialize(0);
            ScanCaptureMode = ScanAcquisitionModel.Initialize(1);

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
            // 扫描像素
            ScanPixelList = ScanPixelModel.Initialize();
            // 跳行扫描
            ScanLineSkipEnabled = false;
            ScanLineSkipList = ScanLineSkipModel.Initialize();
            SelectedScanLineSkip = ScanLineSkipList[0];
            // 扫描通道
            ScanChannel405 = ScanChannelModel.Initialize(0);
            ScanChannel488 = ScanChannelModel.Initialize(1);
            ScanChannel561 = ScanChannelModel.Initialize(2);
            ScanChannel640 = ScanChannelModel.Initialize(3);
            // 小孔
            ScanPinHoleList = ScanPinHoleModel.Initialize();
            SelectedPinHole = ScanPinHoleList[0];
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
