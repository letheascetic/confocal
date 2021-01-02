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
            // 扫描头
            ScannerHeadTwoGalv = ScannerHeadModel.Initialize(ScannerHeadModel.TWO_SCANNERS);
            ScannerHeadThreeGalv = ScannerHeadModel.Initialize(ScannerHeadModel.THREE_SCANNERS);
            // 扫描模式
            ScanModeResonant = ScanModeModel.Initialize(ScanModeModel.RESONANT);
            ScanModeGalavano = ScanModeModel.Initialize(ScanModeModel.GALVANO);

            ScanDirectionList = ScanDirectionModel.Initialize();
            ScanPixelDwellList = ScanPixelDwellModel.Initialize();
            ScanPixelsList = ScanPixelsModel.Initialize();
        }

    }
}
