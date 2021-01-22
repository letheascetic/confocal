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

        private readonly ConfigViewModel mConfig;

        public ConfigViewModel Config
        {
            get { return mConfig; }
        }

        public ScanSettingsViewModel()
        {
            mConfig = ConfigViewModel.GetConfig();
        }

    }
}
