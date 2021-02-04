using confocal_core.Common;
using confocal_core.Model;
using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace confocal_core.ViewModel
{
    class HistogramViewModel : ViewModelBase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        ///////////////////////////////////////////////////////////////////////////////////////////

        private readonly Scheduler mScheduler;
        private ScanChannelModel[] mActivatedChannels;

        public Scheduler Engine
        {
            get { return mScheduler; }
        }

        public HistogramViewModel()
        {
            mScheduler = Scheduler.CreateInstance();
        }

    }
}
