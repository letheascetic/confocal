using confocal_core.Model;
using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.ViewModel
{
    public class MainViemModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

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





    }
}
