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
        private static readonly int SAMPLE_COUNT_FACTOR = 2;
        ///////////////////////////////////////////////////////////////////////////////////////////
        private readonly ConfigViewModel mConfig;
        private readonly SequenceModel mSequence;

        private double[] timeValues;
        private double[] xGalvoValues;
        private double[] yGalvoValues;
        private double[] y2GalvoValues;
        private byte[] triggerVlaues;

        public double[] TimeValues
        {
            get { return timeValues; }
            set { timeValues = value; RaisePropertyChanged(() => TimeValues); }
        }

        public double[] XGalvoValues
        {
            get { return xGalvoValues; }
            set { xGalvoValues = value; RaisePropertyChanged(() => XGalvoValues); }
        }

        public double[] YGalvoValues
        {
            get { return yGalvoValues; }
            set { yGalvoValues = value; RaisePropertyChanged(() => YGalvoValues); }
        }

        public double[] Y2GalvoValues
        {
            get { return y2GalvoValues; }
            set { y2GalvoValues = value; RaisePropertyChanged(() => Y2GalvoValues); }
        }

        public byte[] TriggerValues
        {
            get { return triggerVlaues; }
            set { triggerVlaues = value; RaisePropertyChanged(() => TriggerValues); }
        }

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

        public void UpdateChartValues()
        {
            double sampleTime = 1e3 / Sequence.OutputSampleRate;
            int sampleCount = Sequence.OutputSampleCountPerRoundTrip * SAMPLE_COUNT_FACTOR;

            TimeValues = new double[sampleCount];
            TimeValues[0] = sampleTime;
            for (int i = 1; i < sampleCount; i++)
            {
                TimeValues[i] = TimeValues[i - 1] + sampleTime;
            }

            XGalvoValues = Enumerable.Concat(Sequence.XVoltages, Sequence.XVoltages).ToArray();
            TriggerValues = Enumerable.Concat(Sequence.TriggerVoltages, Sequence.TriggerVoltages).ToArray();
        }
    }
}
