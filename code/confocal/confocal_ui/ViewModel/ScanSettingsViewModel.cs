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
        private List<ScanPixelDwellModel> scanPixelDwellList;

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



        public ScanSettingsViewModel()
        {
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
            ScanPixelDwellList = ScanPixelDwellModel.Initialize();
            // 扫描像素
            ScanPixelList = ScanPixelModel.Initialize();
        }

    }
}
