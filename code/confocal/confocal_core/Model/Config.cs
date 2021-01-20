﻿using confocal_core.Properties;
using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{

    public class Config : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly int CHAN_NUM = 4;
        private volatile static Config pConfig = null;
        private static readonly object locker = new object();
        ///////////////////////////////////////////////////////////////////////////////////////////
        public bool Debugging { get; set; }

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

        private List<ScanPinHoleModel> scanPinHoleList;
        /// <summary>
        /// 小孔列表
        /// </summary>
        public List<ScanPinHoleModel> ScanPinHoleList
        {
            get { return scanPinHoleList; }
            set { scanPinHoleList = value; RaisePropertyChanged(() => ScanPinHoleList); }
        }

        private GalvoPrppertyModel mGalvoPrpperty;
        private ScanAreaModel mFullScanArea;
        private DetectorModel mDetector;
        private ScanAreaModel selectedScanArea;
        /// <summary>
        /// 振镜属性
        /// </summary>
        public GalvoPrppertyModel GalvoProperty
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
            ScanPinHoleList = ScanPinHoleModel.Initialize();

            GalvoProperty = new GalvoPrppertyModel();
            FullScanArea = ScanAreaModel.CreateFullScanArea();
            SelectedScanArea = ScanAreaModel.CreateFullScanArea();
            Detector = new DetectorModel();
        }

    }
}
