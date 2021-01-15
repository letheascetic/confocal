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
        private GalvoPrppertyModel mGalvoPrpperty;

        public GalvoPrppertyModel GalvoProperty
        {
            get { return mGalvoPrpperty; }
            set { mGalvoPrpperty = value; RaisePropertyChanged(() => GalvoProperty); }
        }

        private string[] mXGalvoChannels;
        private string[] mYGalvoChannels;
        private string[] mY2GalvoChannels;

        private string xGalvoChannel;       // X振镜控制电压 - AO输出
        private string yGalvoAoChannel;     // Y振镜控制电压 - AO输出
        private string y2GalvoAoChannel;    // Y补偿镜控制电压 - AO输出

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

        private string[] mAiChannels;
        private string[] mCiChannels;
        private string[] mDoLines;
        private string[] mPFIs;
        private string[] mStartTriggers;

        public string[] AiChannels
        {
            get { return mAiChannels; }
            set { mAiChannels = value; RaisePropertyChanged(() => AiChannels); }
        }

        public string[] CiChannels
        {
            get { return mCiChannels; }
            set { mCiChannels = value; RaisePropertyChanged(() => CiChannels); }
        }

        public string[] DoLines
        {
            get { return mDoLines; }
            set { mDoLines = value; RaisePropertyChanged(() => DoLines); }
        }

        public string[] PFIs
        {
            get { return mPFIs; }
            set { mPFIs = value; RaisePropertyChanged(() => PFIs); }
        }

        public string[] StartTriggers
        {
            get { return mStartTriggers; }
            set { mStartTriggers = value; RaisePropertyChanged(() => StartTriggers); }
        }

        /// <summary>
        /// X振镜模拟输出通道
        /// </summary>
        public string XGalvoAoChannel
        {
            get { return xGalvoChannel; }
            set { xGalvoChannel = value; RaisePropertyChanged(() => XGalvoAoChannel); }
        }
        /// <summary>
        /// Y振镜模拟输出通道
        /// </summary>
        public string YGalvoAoChannel
        {
            get { return yGalvoAoChannel; }
            set { yGalvoAoChannel = value; RaisePropertyChanged(() => YGalvoAoChannel); }
        }
        /// <summary>
        /// Y2补偿镜模拟输出通道
        /// </summary>
        public string Y2GalvoAoChannel
        {
            get { return y2GalvoAoChannel; }
            set { y2GalvoAoChannel = value; RaisePropertyChanged(() => Y2GalvoAoChannel); }
        }

        public SysSettingsViewModel()
        {
            GalvoProperty = new GalvoPrppertyModel();

            string[] devices = NiDaq.GetDeviceNames();
            string deviceName = devices.Length > 0 ? devices[0] : "Dev1";

            XGalvoAoChannels = NiDaq.GetAoChannels();
            YGalvoAoChannels = NiDaq.GetAoChannels();
            Y2GalvoAoChannels = NiDaq.GetAoChannels();

            XGalvoAoChannel = string.Concat(deviceName, "/ao0");
            YGalvoAoChannel = string.Concat(deviceName, "/ao1");
            Y2GalvoAoChannel = string.Concat(deviceName, "/ao2");

            AiChannels = NiDaq.GetAiChannels();
            CiChannels = NiDaq.GetCiChannels();
            DoLines = NiDaq.GetDoLines();
            PFIs = NiDaq.GetPFIs();
            StartTriggers = NiDaq.GetStartSyncSignals();
        }

    }
}
