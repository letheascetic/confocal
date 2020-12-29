using confocal_core.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_ui.ViewModel
{
    public class ScanSettingsViewModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScannerHeadModel> scannerHeadList;
        private ScannerHeadModel selectedScannerHead;

        /// <summary>
        /// 扫描头列表
        /// </summary>
        public List<ScannerHeadModel> ScannerHeadList
        {
            get { return scannerHeadList; }
            set { scannerHeadList = value; RaisePropertyChanged(() => ScannerHeadList); }
        }
        /// <summary>
        /// 选择的扫描头
        /// </summary>
        public ScannerHeadModel SelectedScannerHead
        {
            get { return selectedScannerHead; }
            set { selectedScannerHead = value; RaisePropertyChanged(() => SelectedScannerHead); }
        }
        /// <summary>
        /// 双镜
        /// </summary>
        public ScannerHeadModel ScannerHeadTwoGalv
        {
            get { return scannerHeadList.Where(p => p.ID == ScannerHeadModel.TWO_SCANNERS).First(); }
        }
        /// <summary>
        /// 三镜
        /// </summary>
        public ScannerHeadModel ScannerHeadThreeGalv
        {
            get { return scannerHeadList.Where(p => p.ID == ScannerHeadModel.THREE_SCANNERS).First(); }
        }
        /// <summary>
        /// 选择扫描头
        /// </summary>
        public void SelectScannerHeadCommand()
        {
            SelectedScannerHead = ScannerHeadList.Where(p => p.IsEnabled).First();
            Logger.Info(string.Format("Select Scanner Head [{0}].", SelectedScannerHead.Text));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanDirectionModel> scanDirectionList;
        private ScanDirectionModel selectedScanDirection;

        /// <summary>
        /// 扫描方向列表
        /// </summary>
        public List<ScanDirectionModel> ScanDirectionList
        {
            get { return scanDirectionList; }
            set { scanDirectionList = value; RaisePropertyChanged(() => ScanDirectionList); }
        }
        /// <summary>
        /// 选择的扫描方向
        /// </summary>
        public ScanDirectionModel SelectedScanDirection
        {
            get { return selectedScanDirection; }
            set { selectedScanDirection = value; RaisePropertyChanged(() => SelectedScanDirection); }
        }
        /// <summary>
        /// 选择扫描方向
        /// </summary>
        public void SelectScanDirectionCommand()
        {
            SelectedScanDirection = ScanDirectionList.Where(p => p.IsEnabled).First();
            Logger.Info(string.Format("Select Scan Direction [{0}].", SelectedScanDirection.Text));
        }
        public ScanDirectionModel ScanUniDirection
        {
            get { return scanDirectionList.Where(p => p.ID == ScanDirectionModel.UNIDIRECTION).First(); }
        }
        public ScanDirectionModel ScanBiDirection
        {
            get { return scanDirectionList.Where(p => p.ID == ScanDirectionModel.BIDIRECTION).First(); }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanModeModel> scanModeList;
        private ScanModeModel selectedScanMode;

        /// <summary>
        /// 扫描模式列表
        /// </summary>
        public List<ScanModeModel> ScanModeList
        {
            get { return scanModeList; }
            set { scanModeList = value; RaisePropertyChanged(() => ScanModeList); }
        }
        /// <summary>
        /// 选择的扫描模式
        /// </summary>
        public ScanModeModel SelectedScanMode
        {
            get { return selectedScanMode; }
            set { selectedScanMode = value; RaisePropertyChanged(() => SelectedScanMode); }
        }
        /// <summary>
        /// 选择扫描模式
        /// </summary>
        public void SelectScanModeCommand()
        {
            SelectedScanMode = ScanModeList.Where(p => p.IsEnabled).First();
            Logger.Info(string.Format("Select Scan Mode [{0}].", SelectedScanMode.Text));
        }
        /// <summary>
        /// Galvano扫描模式
        /// </summary>
        public ScanModeModel ScanModeGalavano
        {
            get { return scanModeList.Where(p => p.ID == ScanModeModel.GALVANO).First(); }
        }
        /// <summary>
        /// Resonant扫描模式
        /// </summary>
        public ScanModeModel ScanModeResonant
        {
            get { return scanModeList.Where(p => p.ID == ScanModeModel.RESONANT).First(); }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanPixelsModel> scanPixelsList;
        private ScanPixelsModel selectedScanPixels;
        private RelayCommand selectScanPixelsCommand;

        /// <summary>
        /// 扫描像素列表
        /// </summary>
        public List<ScanPixelsModel> ScanPixelsList
        {
            get { return scanPixelsList; }
            set { scanPixelsList = value; RaisePropertyChanged(() => ScanPixelsList); }
        }
        /// <summary>
        /// 选择的扫描像素 
        /// </summary>
        public ScanPixelsModel SelectedScanPixels
        {
            get { return selectedScanPixels; }
            set { selectedScanPixels = value; RaisePropertyChanged(() => SelectedScanPixels); }
        }
        /// <summary>
        /// 选择扫描像素
        /// </summary>
        public RelayCommand SelectScanPixelsCommand
        {
            get
            {
                if (selectScanPixelsCommand == null)
                {
                    selectScanPixelsCommand = new RelayCommand(() => { SelectedScanPixels = ScanPixelsList.Where(p => p.IsEnabled).First(); });
                }
                return selectScanPixelsCommand;
            }
            set { selectScanPixelsCommand = value; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanPixelDwellModel> scanPixelDwellList;
        private ScanPixelDwellModel selectedScanPixelDwell;
        private RelayCommand selectScanPixelDwellCommand;

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
        /// 选择像素停留时间
        /// </summary>
        public RelayCommand SelectScanPixelDwellCommand
        {
            get
            {
                if (selectScanPixelDwellCommand == null)
                {
                    selectScanPixelDwellCommand = new RelayCommand(() => { SelectedScanPixelDwell = ScanPixelDwellList.Where(p => p.IsEnabled).First(); });
                }
                return selectScanPixelDwellCommand;
            }
            set { selectScanPixelDwellCommand = value; }
        }

        public ScanSettingsViewModel()
        {
            ScannerHeadList = ScannerHeadModel.Initialize();
            ScanDirectionList = ScanDirectionModel.Initialize();
            ScanModeList = ScanModeModel.Initialize();
            ScanPixelDwellList = ScanPixelDwellModel.Initialize();
            ScanPixelsList = ScanPixelsModel.Initialize();
        }

    }
}
