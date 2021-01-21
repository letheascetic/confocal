using confocal_core.Model;
using confocal_core.Properties;
using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace confocal_core.Common
{
    /// <summary>
    /// 代码执行结果
    /// </summary>
    public enum API_RETURN_CODE : int
    {
        API_SUCCESS = 0x00000000,
        API_FAILED = 0x00000001,
        /* error for config */
        API_FAILED_CONFIG_HANDLE_INVALID = 0x00000100,
        API_FAILED_CONFIG_SET_LASER_SWITCH_FAILED,
        API_FAILED_CONFIG_SET_LASER_POWER_FAILED,
        /* error for ats9440 */
        API_FAILED_ATS_HANDLE_INVALID = 0x00000200,
        API_FAILED_ATS_GET_BOARD_VERSION_FAILED,
        API_FAILED_ATS_GET_SDK_VERSION_FAILED,
        API_FIALED_ATS_GET_DRIVER_VERSION_FAILED,
        API_FIALED_ATS_GET_CPLD_VERSION_FAILED,
        API_FAILED_ATS_INFO_INSTANCE_INVALID,
        API_FAILED_ATS_SET_CLOCK_FAILED,
        API_FAILED_ATS_SET_INPUT_CHANNEL_FAILED,
        API_FAILED_ATS_SET_TRIGGER_OPERATION_FAILED,
        API_FAILED_ATS_SET_EXTERNAL_TRIGGER_FAILED,
        API_FAILED_ATS_SET_TRIGGER_TIMEOUT_FAILED,
        API_FAILED_ATS_SET_TRIGGER_DELAY_FAILED,
        API_FAILED_ATS_SET_AUX_IO_FAILED,
        API_FAILED_ATS_GET_CHANNEL_INFO_FAILED,
        API_FAILED_ATS_MALLOC_BUFFER_FAILED,
        API_FAILED_ATS_SET_RECORD_SIZE_FAILED,
        API_FAILED_ATS_CONFIG_ASYNC_READ_FAILED,
        API_FAILED_ATS_START_CAPTURE_FAILED,
        API_FAILED_ATS_ENABLE_CRS_FAILED,
        API_FAILED_ATS_ABORT_ASYNC_READ_FAILED,
        API_FAILED_ATS_FORCE_TRIGGER_ENABLE_FAILED,
        /* error for laser */
        API_FAILED_LASER_LOAD_DLL_FAILED = 0x00000400,
        API_FAILED_LASER_CONNECT_FAILED,
        API_FAILED_LASER_RELEASE_FAILED,
        API_FAILED_LASER_OPEN_CHANNEL_FAILED,
        API_FAILED_LASER_CLOSE_CHANNEL_FAILED,
        API_FAILED_LASER_SET_POWER_FAILED,
        API_FAILED_LASER_UP_POWER_FAILED,
        API_FAILED_LASER_DOWN_POWER_FAILED,
        /* error for ni card */
        API_FAILED_NI_CONFIG_AO_TASK_EXCEPTION = 0x00000800,
        API_FAILED_NI_CONFIG_DO_TASK_EXCEPTION,
        API_FAILED_NI_CONFIG_AI_TASK_EXCEPTION,
        API_FAILED_NI_CONFIG_CI_TASK_EXCEPTION,
        API_FAILED_NI_NO_AI_CHANNEL_ACTIVATED,
        API_FAILED_NI_START_TASK_EXCEPTION,
        /* error for scan task */
        API_FAILED_SCAN_TASK_INVALID = 0x00001000,
        API_FAILED_SCAN_TASK_NOT_FOUND,
        API_FAILED_SCAN_TASK_START_FAILED,
        API_FAILED_SCAN_TASK_STOP_FAILED,
        /* error for usb dac */
        API_FAILED_USB_DAC_OPEN_FAILED = 0x00002000,
        API_FAILED_USB_DAC_RELEASE_FAILED,
        API_FAILED_USB_DAC_SET_ALL_OUT_FAILED,
        API_FAILED_USB_DAC_SET_CHANNEL_OUT_FAILED,
        API_FAILED_USB_DAC_SET_ZERO_CALIBRATION_FAILED,
        API_FAILED_USB_DAC_SET_GAIN_CALIBRATION_FAILED,
        API_FAILED_USB_DAC_WRITE_CONFIG_FAILED,
        API_FAILED_USB_DAC_READ_CONFIG_FAILED,
        API_FAILED_USB_DAC_SET_REGISTER_FAILED
    }

    /// <summary>
    /// 配置项
    /// </summary>
    public class Config : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly int CHAN_NUM = 4;
        private static readonly double INPUT_SAMPLE_RATE_DEFAULT = 1e6;     // 像素采样速率
        private volatile static Config pConfig = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        public bool Debugging { get; set; }
        public double InputSampleRate { get; set; }                         // 像素速率[采样速率]
        ///////////////////////////////////////////////////////////////////////////////////////////
        private string laserPortName;
        /// <summary>
        /// 激光端口
        /// </summary>
        public string LaserPortName
        {
            get { return laserPortName; }
            set { laserPortName = value; RaisePropertyChanged(() => LaserPortName); }
        }

        private ScanAcquisitionModel selectedAcquisitionMode;
        /// <summary>
        /// 当前的采集模式
        /// </summary>
        public ScanAcquisitionModel SelectedAcquisitioMode
        {
            get { return selectedAcquisitionMode; }
            set { selectedAcquisitionMode = value; RaisePropertyChanged(() => SelectedAcquisitioMode); }
        }
        /// <summary>
        /// 扫描状态
        /// </summary>
        public bool IsScanning
        {
            get { return SelectedAcquisitioMode != null && SelectedAcquisitioMode.IsEnabled; }
        }

        private ScannerHeadModel selectedScannerHead;
        /// <summary>
        /// 当前的扫描头
        /// </summary>
        public ScannerHeadModel SelectedScannerHead
        {
            get { return selectedScannerHead; }
            set { selectedScannerHead = value; RaisePropertyChanged(() => SelectedScannerHead); }
        }

        private ScanDirectionModel selectedScanDirection;
        /// <summary>
        /// 当前的扫描方向
        /// </summary>
        public ScanDirectionModel SelectedScanDirection
        {
            get { return selectedScanDirection; }
            set { selectedScanDirection = value; RaisePropertyChanged(() => SelectedScanDirection); }
        }

        private ScanModeModel selectedScanMode;
        /// <summary>
        /// 当前的扫描模式
        /// </summary>
        public ScanModeModel SelectedScanMode
        {
            get { return selectedScanMode; }
            set { selectedScanMode = value; RaisePropertyChanged(() => SelectedScanMode); }
        }

        private ScanPixelModel selectedScanPixel;
        /// <summary>
        /// 当前的扫描像素
        /// </summary>
        public ScanPixelModel SelectedScanPixel
        {
            get { return selectedScanPixel; }
            set { selectedScanPixel = value; RaisePropertyChanged(() => SelectedScanPixel); }
        }

        private bool fastModeEnabled;
        private ScanPixelDwellModel selectedScanPixelDwell;
        /// <summary>
        /// 当前的像素时间
        /// </summary>
        public ScanPixelDwellModel SelectedScanPixelDwell
        {
            get { return selectedScanPixelDwell; }
            set { selectedScanPixelDwell = value; RaisePropertyChanged(() => SelectedScanPixelDwell); }
        }
        /// <summary>
        /// 快速模式使能
        /// </summary>
        public bool FastModeEnabled
        {
            get { return fastModeEnabled; }
            set { fastModeEnabled = value; RaisePropertyChanged(() => FastModeEnabled); }
        }

        private bool scanLineSkipEnabled;
        private ScanLineSkipModel selectedScanLineSkip;
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

        private List<ScanPinHoleModel> scanPinHoleList;
        /// <summary>
        /// 小孔列表
        /// </summary>
        public List<ScanPinHoleModel> ScanPinHoleList
        {
            get { return scanPinHoleList; }
            set { scanPinHoleList = value; RaisePropertyChanged(() => ScanPinHoleList); }
        }

        private GalvoPropertyModel mGalvoPrpperty;
        private ScanAreaModel mFullScanArea;
        private DetectorModel mDetector;
        private ScanAreaModel selectedScanArea;
        /// <summary>
        /// 振镜属性
        /// </summary>
        public GalvoPropertyModel GalvoProperty
        {
            get { return mGalvoPrpperty; }
            set { mGalvoPrpperty = value; RaisePropertyChanged(() => GalvoProperty); }
        }
        /// <summary>
        /// 最大扫描视场范围
        /// </summary>
        public ScanAreaModel FullScanArea
        {
            get { return mFullScanArea; }
            set { mFullScanArea = value; RaisePropertyChanged(() => FullScanArea); }
        }
        /// <summary>
        /// 探测器属性
        /// </summary>
        public DetectorModel Detector
        {
            get { return mDetector; }
            set { mDetector = value; RaisePropertyChanged(() => Detector); }
        }
        /// <summary>
        /// 当前选择的扫描区域
        /// </summary>
        public ScanAreaModel SelectedScanArea
        {
            get { return selectedScanArea; }
            set { selectedScanArea = value; RaisePropertyChanged(() => SelectedScanArea); }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        public static Config GetConfig()
        {
            if (pConfig == null)
            {
                lock (locker)
                {
                    if (pConfig == null)
                    {
                        pConfig = new Config();
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

        /// <summary>
        /// 像素尺寸
        /// </summary>
        /// <returns></returns>
        public float GetPixelSize()
        {
            return SelectedScanArea.ScanRange.Width / SelectedScanPixel.Data;
        }

        /// <summary>
        /// 像素电压[单位：V/pixel]
        /// </summary>
        /// <returns></returns>
        public double GetPixelVoltage()
        {
            return GetPixelSize() * GalvoProperty.XGalvoCalibrationVoltage / 1000;
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

        /// <summary>
        /// 查询对应的扫描通道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            float yExtendRange = ScanAreaModel.ExtendRowCount * GetPixelSize();
            return new ScanAreaModel(new RectangleF(scanRange.X - xExtendRange / 2, scanRange.Y - yExtendRange / 2, scanRange.Width + xExtendRange, scanRange.Height + yExtendRange));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private Config()
        {
            Debugging = true;
            LaserPortName = Settings.Default.LaserPortName;

            SelectedAcquisitioMode = null;
            SelectedScannerHead = ScannerHeadModel.Initialize(Settings.Default.ScannerHead);
            SelectedScanDirection = ScanDirectionModel.Initialize(Settings.Default.ScanDirection);
            SelectedScanMode = ScanModeModel.Initialize(Settings.Default.ScanMode);
            SelectedScanPixel = ScanPixelModel.Initialize(Settings.Default.ScanPixel);
            FastModeEnabled = Settings.Default.FastModeEnabled;
            SelectedScanPixelDwell = ScanPixelDwellModel.Initialize(Settings.Default.ScanPixelDwell);
            ScanLineSkipEnabled = Settings.Default.ScanLineSkipEnabled;
            SelectedScanLineSkip = ScanLineSkipModel.Initialize(Settings.Default.ScanLineSkip);
            ScanChannel405 = ScanChannelModel.Initialize(ScanChannelModel.CHANNEL405);
            ScanChannel488 = ScanChannelModel.Initialize(ScanChannelModel.CHANNEL488);
            ScanChannel561 = ScanChannelModel.Initialize(ScanChannelModel.CHANNEL561);
            ScanChannel640 = ScanChannelModel.Initialize(ScanChannelModel.CHANNEL640);
            ScanChannels = new ScanChannelModel[] { ScanChannel405, ScanChannel488, ScanChannel561, ScanChannel640 };
            ScanPinHoleList = ScanPinHoleModel.Initialize();

            GalvoProperty = new GalvoPropertyModel();
            FullScanArea = ScanAreaModel.CreateFullScanArea();
            SelectedScanArea = ScanAreaModel.CreateFullScanArea();
            Detector = new DetectorModel();

            InputSampleRate = INPUT_SAMPLE_RATE_DEFAULT;
        }

    }
}
