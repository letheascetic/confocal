using confocal_core.Common;
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

        public ConfigViewModel Config
        {
            get { return mConfig; }
        }

        public ScanParasViewModel()
        {
            mConfig = ConfigViewModel.GetConfig();
        }

    }
}
