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
        private readonly Scheduler mScheduler;

        private int scanPixelCalibration;
        private int scanPixelOffset;
        private int scanPixelCalibrationMaximum;
        private int scanPixelScale;

        public Scheduler Engine
        {
            get { return mScheduler; }
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
        /// <summary>
        /// 扫描像素缩放系数
        /// </summary>
        public int ScanPixelScale
        {
            get { return scanPixelScale; }
            set { scanPixelScale = value; RaisePropertyChanged(() => ScanPixelScale); }
        }

        public ScanSettingsViewModel()
        {
            mScheduler = Scheduler.CreateInstance();
            ScanPixelCalibrationMaximum = mScheduler.Config.SelectedScanPixelDwell.ScanPixelCalibrationMaximum;
            ScanPixelCalibration = mScheduler.Config.SelectedScanPixelDwell.ScanPixelCalibration;
            ScanPixelOffset = mScheduler.Config.SelectedScanPixelDwell.ScanPixelOffset;
            ScanPixelScale = mScheduler.Config.SelectedScanPixelDwell.ScanPixelScale;
            // 绑定事件
            mScheduler.ScanPixelDwellChangedEvent += ScanPixelDwellChangedHandler;
        }

        public API_RETURN_CODE ScanPixelDwellChangedHandler(ScanPixelDwellModel scanPixelDwell)
        {
            ScanPixelCalibrationMaximum = scanPixelDwell.ScanPixelCalibrationMaximum;
            ScanPixelCalibration = scanPixelDwell.ScanPixelCalibration;
            ScanPixelOffset = scanPixelDwell.ScanPixelOffset;
            ScanPixelScale = scanPixelDwell.ScanPixelScale;
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ScanPixelCalibrationChangeCommand(int scanPixelCalibration)
        {
            ScanPixelCalibration = scanPixelCalibration;
            return mScheduler.ScanPixelCalibrationChangeCommand(ScanPixelCalibration);
        }

        public API_RETURN_CODE ScanPixelScaleChangeCommand(int scanPixelScale)
        {
            ScanPixelScale = scanPixelScale;
            return mScheduler.ScanPixelScaleChangeCommand(scanPixelScale);
        }

    }
}
