using confocal_core.Common;
using confocal_core.Model;
using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.ViewModel
{
    public class SysSettingsViewModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private GalvoPropertyModel mGalvoPrpperty;
        private ScanAreaModel mFullScanArea;
        private DetectorModel mDetector;

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

        private string[] mXGalvoChannels;
        private string[] mYGalvoChannels;
        private string[] mY2GalvoChannels;

        public string[] XGalvoAoChannels
        {
            get { return mXGalvoChannels; }
            set { mXGalvoChannels = value; RaisePropertyChanged(() => XGalvoAoChannels); }
        }
        public string[] YGalvoAoChannels
        {
            get { return mYGalvoChannels; }
            set { mYGalvoChannels = value; RaisePropertyChanged(() => YGalvoAoChannels); }
        }
        public string[] Y2GalvoAoChannels
        {
            get { return mY2GalvoChannels; }
            set { mY2GalvoChannels = value; RaisePropertyChanged(() => Y2GalvoAoChannels); }
        }

        private string[][] mAiChannels;
        private string[][] mCiSources;
        private string[][] mCiChannels;
        private string[] mTriggerSignals;
        private string[] mTriggerReceivers;
        private string[] mStartTriggers;

        public string[][] AiChannels
        {
            get { return mAiChannels; }
            set { mAiChannels = value; RaisePropertyChanged(() => AiChannels); }
        }

        public string[][] CiSources
        {
            get { return mCiSources; }
            set { mCiSources = value; RaisePropertyChanged(() => CiSources); }
        }

        public string[][] CiChannels
        {
            get { return mCiChannels; }
            set { mCiChannels = value; RaisePropertyChanged(() => CiChannels); }
        }

        public string[] TriggerSignals
        {
            get { return mTriggerSignals; }
            set { mTriggerSignals = value; RaisePropertyChanged(() => TriggerSignals); }
        }

        public string[] TriggerReceivers
        {
            get { return mTriggerReceivers; }
            set { mTriggerReceivers = value; RaisePropertyChanged(() => TriggerReceivers); }
        }

        public string[] StartTriggers
        {
            get { return mStartTriggers; }
            set { mStartTriggers = value; RaisePropertyChanged(() => StartTriggers); }
        }

        public SysSettingsViewModel()
        {
            GalvoProperty = new GalvoPropertyModel();
            FullScanArea = ScanAreaModel.CreateFullScanArea();
            Detector = new DetectorModel();

            XGalvoAoChannels = NiDaq.GetAoChannels();
            YGalvoAoChannels = NiDaq.GetAoChannels();
            Y2GalvoAoChannels = NiDaq.GetAoChannels();

            AiChannels = new string[4][] 
            {
                NiDaq.GetAiChannels(),
                NiDaq.GetAiChannels(),
                NiDaq.GetAiChannels(),
                NiDaq.GetAiChannels()
            };
            CiSources = new string[4][]
            {
                NiDaq.GetCiChannels(),
                NiDaq.GetCiChannels(),
                NiDaq.GetCiChannels(),
                NiDaq.GetCiChannels()
            };
            CiChannels = new string[4][] 
            {
                NiDaq.GetPFIs(),
                NiDaq.GetPFIs(),
                NiDaq.GetPFIs(),
                NiDaq.GetPFIs()
            };
            StartTriggers = NiDaq.GetStartSyncSignals();
            TriggerSignals = NiDaq.GetDoLines();
            TriggerReceivers = NiDaq.GetPFIs();
        }

        public PmtChannelModel FindPmtChannel(int id)
        {
            switch (id)
            {
                case 0:
                    return Detector.PmtChannel405;
                case 1:
                    return Detector.PmtChannel488;
                case 2:
                    return Detector.PmtChannel561;
                case 3:
                    return Detector.PmtChannel640;
                default:
                    throw new ArgumentOutOfRangeException("ID Exception.");
            }
        }

        public ApdChannelModel FindApdChannel(int id)
        {
            switch (id)
            {
                case 0:
                    return Detector.ApdChannel405;
                case 1:
                    return Detector.ApdChannel488;
                case 2:
                    return Detector.ApdChannel561;
                case 3:
                    return Detector.ApdChannel640;
                default:
                    throw new ArgumentOutOfRangeException("ID Exception.");
            }
        }

        public API_RETURN_CODE XGalvoChannelChangeCommand(string xGalvoChannel)
        {
            GalvoProperty.XGalvoAoChannel = xGalvoChannel;
            Logger.Info(string.Format("X Galvo Ao Channel [{0}].", GalvoProperty.XGalvoAoChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE YGalvoChannelChangeCommand(string yGalvoChannel)
        {
            GalvoProperty.YGalvoAoChannel = yGalvoChannel;
            Logger.Info(string.Format("Y Galvo Ao Channel [{0}].", GalvoProperty.YGalvoAoChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE Y2GalvoChannelChangeCommand(string y2GalvoChannel)
        {
            GalvoProperty.Y2GalvoAoChannel = y2GalvoChannel;
            Logger.Info(string.Format("Y2 Galvo Ao Channel [{0}].", GalvoProperty.Y2GalvoAoChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE XGalvoOffsetVoltageChangeCommand(double offsetVoltage)
        {
            GalvoProperty.XGalvoOffsetVoltage = offsetVoltage;
            Logger.Info(string.Format("X Galvo Offset Voltage [{0}].", GalvoProperty.XGalvoOffsetVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE XGalvoCalibrationVoltageChangeCommand(double calibrationVoltage)
        {
            GalvoProperty.XGalvoCalibrationVoltage = calibrationVoltage;
            Logger.Info(string.Format("X Galvo Calibration Voltage [{0}].", GalvoProperty.XGalvoCalibrationVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE YGalvoOffsetVoltageChangeCommand(double offsetVoltage)
        {
            GalvoProperty.YGalvoOffsetVoltage = offsetVoltage;
            Logger.Info(string.Format("Y Galvo Offset Voltage [{0}].", GalvoProperty.YGalvoOffsetVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE YGalvoCalibrationVoltageChangeCommand(double calibrationVoltage)
        {
            GalvoProperty.YGalvoCalibrationVoltage = calibrationVoltage;
            Logger.Info(string.Format("Y Galvo Calibration Voltage [{0}].", GalvoProperty.YGalvoCalibrationVoltage));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE GalvoResponseTimeChangeCommand(double responseTime)
        {
            GalvoProperty.GalvoResponseTime = responseTime;
            Logger.Info(string.Format("Galvo Response Time [{0}].", GalvoProperty.GalvoResponseTime));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE FullScanAreaChangeCommand(float scanRange)
        {
            FullScanArea.Update(new System.Drawing.RectangleF(scanRange / 2, scanRange / 2, scanRange, scanRange));
            Logger.Info(string.Format("Full Scan Area [{0}].", FullScanArea.ScanRange));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE DetectorModeChangeCommand(bool pmtModeEnabled)
        {
            Detector.DetectorPmt.IsEnabled = pmtModeEnabled;
            Detector.DetectorApd.IsEnabled = !pmtModeEnabled;
            Logger.Info(string.Format("Detector Mode [{0}].", Detector.CurrentDetecor.Text));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE PmtChannelChangeCommand(int id, string pmtChannel)
        {
            PmtChannelModel channel = FindPmtChannel(id);
            channel.AiChannel = pmtChannel;
            Logger.Info(string.Format("Pmt [{0}] Ao Channel [{1}].", channel.ID, channel.AiChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ApdSourceChangeCommand(int id, string apdSource)
        {
            ApdChannelModel channel = FindApdChannel(id);
            channel.CiSource = apdSource;
            Logger.Info(string.Format("Apd [{0}] Ci Channel [{1}:{2}].", channel.ID, channel.CiSource, channel.CiChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE ApdChannelChangeCommand(int id, string apdChannel)
        {
            ApdChannelModel channel = FindApdChannel(id);
            channel.CiChannel = apdChannel;
            Logger.Info(string.Format("Apd [{0}] Ci Channel [{1}:{2}].", channel.ID, channel.CiSource, channel.CiChannel));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE StartTriggerChangeCommand(string startTrigger)
        {
            Detector.StartTrigger = startTrigger;
            Logger.Info(string.Format("Start Trigger [{0}].", Detector.StartTrigger));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE TriggerSignalChangeCommand(string triggerSignal)
        {
            Detector.TriggerSignal = triggerSignal;
            Logger.Info(string.Format("Trigger Signal [{0}].", Detector.TriggerSignal));
            return API_RETURN_CODE.API_SUCCESS;
        }

        public API_RETURN_CODE TriggerReceiverChangeCommand(string triggerReceive)
        {
            Detector.TriggerReceive = triggerReceive;
            Logger.Info(string.Format("Trigger Receiver [{0}].", Detector.TriggerReceive));
            return API_RETURN_CODE.API_SUCCESS;
        }

    }
}
