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
    public class ScanParasViewModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////
        private readonly ConfigViewModel mConfig;
        private readonly SequenceModel mSequence;

        public ConfigViewModel Config
        {
            get { return mConfig; }
        }

        public SequenceModel Sequence
        {
            get { return mSequence; }
        }

        public ScanParasViewModel()
        {
            mConfig = ConfigViewModel.GetConfig();
            mSequence = SequenceModel.CreateInstance();
        }

    }
}
