using confocal_core.Common;
using confocal_core.Model;
using confocal_core.Properties;
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

        private readonly SequenceModel mSequence;
        private readonly ConfigViewModel mConfig;

        private int scanPixelCalibration;
        private int scanPixelOffset;
        private int scanPixelCalibrationMaximum;

        public ConfigViewModel Config
        {
            get { return mConfig; }
        }

        public SequenceModel Sequence
        {
            get { return mSequence; }
        }

        /// <summary>
        /// 扫描像素补偿
        /// </summary>
        public int ScanPixelCalibration
        {
            get { return scanPixelCalibration; }
            set { scanPixelCalibration = value; RaisePropertyChanged(() => ScanPixelCalibration); }
        }
        /// <summary>
        /// 扫描像素偏置
        /// </summary>
        public int ScanPixelOffset
        {
            get { return scanPixelOffset; }
            set { scanPixelOffset = value; RaisePropertyChanged(() => ScanPixelOffset); }
        }
        /// <summary>
        /// 扫描像素补偿最大值
        /// </summary>
        public int ScanPixelCalibrationMaximum
        {
            get { return scanPixelCalibrationMaximum; }
            set { scanPixelCalibrationMaximum = value; RaisePropertyChanged(() => ScanPixelCalibrationMaximum); }
        }

        public ScanSettingsViewModel()
        {
            mConfig = ConfigViewModel.GetConfig();
            mSequence = SequenceModel.CreateInstance();
            ScanPixelCalibrationMaximum = mConfig.SelectedScanPixelDwell.ScanPixelCalibrationMaximum;
            ScanPixelCalibration = mConfig.SelectedScanPixelDwell.ScanPixelCalibration;
            ScanPixelOffset = mConfig.SelectedScanPixelDwell.ScanPixelOffset;
            // 绑定事件
            mConfig.ScanPixelDwellChangedEvent += ScanPixelDwellChangedHandler;
        }

        public API_RETURN_CODE ScanPixelDwellChangedHandler(ScanPixelDwellModel scanPixelDwell)
        {
            ScanPixelCalibrationMaximum = scanPixelDwell.ScanPixelCalibrationMaximum;
            ScanPixelCalibration = scanPixelDwell.ScanPixelCalibration;
            ScanPixelOffset = scanPixelDwell.ScanPixelOffset;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ScanPixelCalibrationChangeCommand(int scanPixelCalibration)
        {
            ScanPixelCalibration = scanPixelCalibration;
            mConfig.ScanPixelCalibrationChangeCommand(ScanPixelCalibration);
            return API_RETURN_CODE.API_SUCCESS;
        }

    }
}
