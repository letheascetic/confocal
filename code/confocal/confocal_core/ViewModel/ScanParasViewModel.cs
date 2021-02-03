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

        private readonly Scheduler mScheduler;

        private double[] triggerTimeValues;
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

        public double[] TriggerTimeValues
        {
            get { return triggerTimeValues; }
            set { triggerTimeValues = value; RaisePropertyChanged(() => TriggerTimeValues); }
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

        public Scheduler Engine
        {
            get { return mScheduler; }
        }

        public ScanParasViewModel()
        {
            mScheduler = Scheduler.CreateInstance();
        }

        public void UpdateChartValues()
        {
            double sampleTime = 1e3 / Engine.Sequence.OutputSampleRate;
            int sampleCount = Engine.Sequence.OutputSampleCountPerRoundTrip * SAMPLE_COUNT_FACTOR;

            TimeValues = new double[sampleCount];
            TimeValues[0] = sampleTime;
            for (int i = 1; i < sampleCount; i++)
            {
                TimeValues[i] = TimeValues[i - 1] + sampleTime;
            }

            if (Engine.Config.Detector.CurrentDetecor.ID == DetectorTypeModel.PMT)
            {
                TriggerTimeValues = TimeValues;
            }
            else
            {
                sampleTime /= 2;
                sampleCount *= 2;
                TriggerTimeValues = new double[sampleCount];
                for (int i = 1; i < sampleCount; i++)
                {
                    TriggerTimeValues[i] = TriggerTimeValues[i - 1] + sampleTime;
                }
            }

            XGalvoValues = Enumerable.Concat(Engine.Sequence.XVoltages, Engine.Sequence.XVoltages).ToArray();
            TriggerValues = Enumerable.Concat(Engine.Sequence.TriggerVoltages, Engine.Sequence.TriggerVoltages).ToArray();
        }
    }
}
