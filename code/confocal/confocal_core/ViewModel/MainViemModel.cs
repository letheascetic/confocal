using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.ViewModel
{
    public class MainViemModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private readonly ConfigViewModel mConfig;

        public ConfigViewModel Config
        {
            get { return mConfig; }
        }

        public MainViemModel()
        {
            mConfig = ConfigViewModel.GetConfig();
        }

    }
}
