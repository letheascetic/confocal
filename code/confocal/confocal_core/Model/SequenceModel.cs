using GalaSoft.MvvmLight;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace confocal_core.Model
{
    public class SequenceModel : ObservableObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        private static readonly ILog Logger = LogManager.GetLogger("info");
        private static readonly int TRIGGER_WIDTH_DEFAULT = 4;
        private static readonly double ACQUISITION_INTERVAL_DEFAULT = 50;
        ///////////////////////////////////////////////////////////////////////////////////////////

        private byte[] triggerWave;
        private double[] xWave;
        private double[] y1Wave;
        private double[] y2Wave;



        public byte[] TriggerWave
        {
            get { return triggerWave; }
            set { triggerWave = value; RaisePropertyChanged(() => TriggerWave); }
        }

        public double[] XWave
        {
            get { return xWave; }
            set { xWave = value; RaisePropertyChanged(() => XWave); }
        }

        public double[] Y1Wave
        {
            get { return y1Wave; }
            set { y1Wave = value; RaisePropertyChanged(() => Y1Wave); }
        }

        public double[] Y2Wave
        {
            get { return y2Wave; }
            set { y2Wave = value; RaisePropertyChanged(() => Y2Wave); }
        }


    }
}
