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
        private ScanAreaModel mFullScanArea;

        public GalvoPrppertyModel GalvoProperty
        {
            get { return mGalvoPrpperty; }
            set { mGalvoPrpperty = value; RaisePropertyChanged(() => GalvoProperty); }
        }
        public ScanAreaModel FullScanArea
        {
            get { return mFullScanArea; }
            set { mFullScanArea = value; RaisePropertyChanged(() => FullScanArea); }
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


        public SysSettingsViewModel()
        {
            GalvoProperty = new GalvoPrppertyModel();
            FullScanArea = ScanAreaModel.CreateFullScanArea();

            XGalvoAoChannels = NiDaq.GetAoChannels();
            YGalvoAoChannels = NiDaq.GetAoChannels();
            Y2GalvoAoChannels = NiDaq.GetAoChannels();

            AiChannels = NiDaq.GetAiChannels();
            CiChannels = NiDaq.GetCiChannels();
            DoLines = NiDaq.GetDoLines();
            PFIs = NiDaq.GetPFIs();
            StartTriggers = NiDaq.GetStartSyncSignals();
        }

    }
}
