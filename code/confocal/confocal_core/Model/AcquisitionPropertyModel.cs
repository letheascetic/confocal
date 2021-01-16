using confocal_core.Common;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    /// <summary>
    /// 探测器类型
    /// </summary>
    public class DetectorTypeModel : ScanPropertyBaseModel
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static readonly int PMT = 0;
        public static readonly int APD = 1;
        ///////////////////////////////////////////////////////////////////////////////////////////

        public static DetectorTypeModel Initialize(int id)
        {
            if (id == PMT)
            {
                return new DetectorTypeModel() { ID = id, Text = "PMT", IsEnabled = true };
            }
            else if (id == APD)
            {
                return new DetectorTypeModel() { ID = id, Text = "APD", IsEnabled = false };
            }
            else
            {
                throw new ArgumentOutOfRangeException("ID Exception");
            }
        }
    }

    /// <summary>
    /// 探测器模块
    /// </summary>
    public class DetectorModel : ObservableObject
    {
        private DetectorTypeModel detectorApd;
        private DetectorTypeModel detectorPmt;

        private string startTrigger;
        private string triggerSignal;
        private string triggerReceive;

        private PmtChannelModel pmtChannel405;
        private PmtChannelModel pmtChannel488;
        private PmtChannelModel pmtChannel561;
        private PmtChannelModel pmtChannel640;

        private ApdChannelModel apdChannel405;
        private ApdChannelModel apdChannel488;
        private ApdChannelModel apdChannel561;
        private ApdChannelModel apdChannel640;

        /// <summary>
        /// APD
        /// </summary>
        public DetectorTypeModel DetectorApd
        {
            get { return detectorApd; }
            set { detectorApd = value; RaisePropertyChanged(() => DetectorApd); }
        }
        /// <summary>
        /// PMT
        /// </summary>
        public DetectorTypeModel DetectorPmt
        {
            get { return detectorPmt; }
            set { detectorPmt = value; RaisePropertyChanged(() => DetectorPmt); }
        }
        /// <summary>
        /// 启动同步源
        /// </summary>
        public string StartTrigger
        {
            get { return startTrigger; }
            set { startTrigger = value; RaisePropertyChanged(() => StartTrigger); }
        }
        /// <summary>
        /// 触发信号[输出端]
        /// </summary>
        public string TriggerSignal
        {
            get { return triggerSignal; }
            set { triggerSignal = value; RaisePropertyChanged(() => TriggerSignal); }
        }
        /// <summary>
        /// 触发接收端
        /// </summary>
        public string TriggerReceive
        {
            get { return triggerReceive; }
            set { triggerReceive = value; RaisePropertyChanged(() => TriggerReceive); }
        }

        public PmtChannelModel PmtChannel405
        {
            get { return pmtChannel405; }
            set { pmtChannel405 = value; RaisePropertyChanged(() => PmtChannel405); }
        }
        public PmtChannelModel PmtChannel488
        {
            get { return pmtChannel488; }
            set { pmtChannel488 = value; RaisePropertyChanged(() => PmtChannel488); }
        }
        public PmtChannelModel PmtChannel561
        {
            get { return pmtChannel561; }
            set { pmtChannel561 = value; RaisePropertyChanged(() => PmtChannel561); }
        }
        public PmtChannelModel PmtChannel640
        {
            get { return pmtChannel640; }
            set { pmtChannel640 = value; RaisePropertyChanged(() => PmtChannel640); }
        }

        public ApdChannelModel ApdChannel405
        {
            get { return apdChannel405; }
            set { apdChannel405 = value; RaisePropertyChanged(() => ApdChannel405); }
        }

        public ApdChannelModel ApdChannel488
        {
            get { return apdChannel488; }
            set { apdChannel488 = value; RaisePropertyChanged(() => ApdChannel488); }
        }

        public ApdChannelModel ApdChannel561
        {
            get { return apdChannel561; }
            set { apdChannel561 = value; RaisePropertyChanged(() => ApdChannel561); }
        }

        public ApdChannelModel ApdChannel640
        {
            get { return apdChannel640; }
            set { apdChannel640 = value; RaisePropertyChanged(() => ApdChannel640); }
        }

        public DetectorModel()
        {
            DetectorApd = DetectorTypeModel.Initialize(DetectorTypeModel.APD);
            DetectorPmt = DetectorTypeModel.Initialize(DetectorTypeModel.PMT);

            string[] devices = NiDaq.GetDeviceNames();
            string deviceName = devices.Length > 0 ? devices[0] : "Dev1";

            StartTrigger = string.Concat("/", deviceName, "/ao/StartTrigger");
            TriggerSignal = string.Concat(deviceName, "/port0/line0");
            TriggerReceive = string.Concat("/", deviceName, "/PFI9");
        }

    }

    public class PmtChannelModel : ObservableObject
    {
        private int id;
        private string aiChannel;

        public int ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => ID); }
        }

        public string AiChannel
        {
            get { return aiChannel; }
            set { aiChannel = value; RaisePropertyChanged(() => AiChannel); }
        }

        public PmtChannelModel(int id)
        {
            string[] devices = NiDaq.GetDeviceNames();
            string deviceName = devices.Length > 0 ? devices[0] : "Dev1";

            if (id >= 0 && id <= 3)
            {
                ID = id;
                AiChannel = string.Concat(deviceName, string.Format("/ai{0}", id));
            }
            else
            {
                throw new ArgumentOutOfRangeException("ID Exception");
            }
        }

    }

    public class ApdChannelModel : ObservableObject
    {
        private int id;
        private string ciSource;
        private string ciChannel;

        public int ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged(() => ID); }
        }

        public string CiSource
        {
            get { return ciSource; }
            set { ciSource = value; RaisePropertyChanged(() => CiSource); }
        }

        public string CiChannel
        {
            get { return ciChannel; }
            set { ciChannel = value; RaisePropertyChanged(() => CiChannel); }
        }

        public ApdChannelModel(int id)
        {
            string[] devices = NiDaq.GetDeviceNames();
            string deviceName = devices.Length > 0 ? devices[0] : "Dev1";

            switch (id)
            {
                case 0:
                    ID = id;
                    CiSource = string.Concat(deviceName, "/ctr0");
                    CiChannel = string.Concat("/", deviceName, "/PFI8");
                    break;
                case 1:
                    ID = id;
                    CiSource = string.Concat(deviceName, "/ctr1");
                    CiChannel = string.Concat("/", deviceName, "/PFI3");
                    break;
                case 2:
                    ID = id;
                    CiSource = string.Concat(deviceName, "/ctr2");
                    CiChannel = string.Concat("/", deviceName, "/PFI0");
                    break;
                case 3:
                    ID = id;
                    CiSource = string.Concat(deviceName, "/ctr3");
                    CiChannel = string.Concat("/", deviceName, "/PFI5");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("ID Exception");
            }

        }

    }

}
