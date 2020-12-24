using confocal_wpf.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace confocal_wpf.ViewModel
{
    /// <summary>
    /// 扫描窗口VM
    /// </summary>
    public class ScanWindowViewModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanDirectionModel> scanDirectionList;
        private ScanDirectionModel selectedScanDirection;
        private RelayCommand selectScanDirectionCommand;

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
        /// 选择扫描方向命令
        /// </summary>
        public RelayCommand SelectScanDirectionCommand
        {
            get
            {
                if (selectScanDirectionCommand == null)
                {
                    selectScanDirectionCommand = new RelayCommand(() => { SelectedScanDirection = ScanDirectionList.Where(p => p.IsChecked).First(); });
                }
                return selectScanDirectionCommand;
            }
            set { selectScanDirectionCommand = value; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanModeModel> scanModeList;
        private ScanModeModel selectedScanMode;
        private RelayCommand selectScanModeCommand;

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
        /// 选择扫描模式命令
        /// </summary>
        public RelayCommand SelectScanModeCommand
        {
            get
            {
                if (selectScanModeCommand == null)
                {
                    selectScanModeCommand = new RelayCommand(() => { SelectedScanMode = ScanModeList.Where(p => p.IsChecked).First(); });
                }
                return selectScanModeCommand;
            }
            set { selectScanModeCommand = value; }
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
                    selectScanPixelsCommand = new RelayCommand(() => { SelectedScanPixels = ScanPixelsList.Where(p => p.IsChecked).First(); });
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
                    selectScanPixelDwellCommand = new RelayCommand(() => { SelectedScanPixelDwell = ScanPixelDwellList.Where(p => p.IsChecked).First(); });
                }
                return selectScanPixelDwellCommand;
            }
            set { selectScanPixelDwellCommand = value; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        private List<ScanChannelModel> scanChannels;
         
        /// <summary>
        /// 扫描通道
        /// </summary>
        public List<ScanChannelModel> ScanChannels
        {
            get { return scanChannels; }
            set { scanChannels = value; RaisePropertyChanged(() => ScanChannels); }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////

        public ScanWindowViewModel()
        {
            ScanDirectionList = ScanDirectionModel.Initialize();
            ScanModeList = ScanModeModel.Initialize();
            ScanPixelDwellList = ScanPixelDwellModel.Initialize();
            ScanPixelsList = ScanPixelsModel.Initialize();
            ScanChannels = ScanChannelModel.Initialize();
        }

    }
}
