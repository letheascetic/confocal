using log4net;
using NationalInstruments.DAQmx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace confocal_test
{
    public enum WaveformType
    {
        SineWave = 0,
        GalvWave
    }

    public class WaveGenerator
    {
        /************************************************************************************/
        private static readonly ILog Logger = log4net.LogManager.GetLogger("info");
        /************************************************************************************/

        private double[] _data;
        private double _resultingSampleClockRate;
        private double _resultingFrequency;
        private double _desiredSampleClockRate;
        private double _samplesPerCycle;

        public double[] Data
        {
            get
            {
                return _data;
            }
        }

        public double ResultingSampleClockRate
        {
            get
            {
                return _resultingSampleClockRate;
            }
        }

        public WaveGenerator(
            Timing timingSubobject,
            double desiredFrequency,
            double samplesPerBuffer,
            double cyclesPerBuffer,
            WaveformType type,
            double amplitude)
        {
            Init(
                timingSubobject,
                desiredFrequency,
                samplesPerBuffer,
                cyclesPerBuffer,
                type,
                amplitude);
        }

        private void Init(Timing timingSubobject, double desiredFrequency, double samplesPerBuffer, 
            double cyclesPerBuffer, WaveformType type, double amplitude)
        {
            if (desiredFrequency <= 0)
                throw new ArgumentOutOfRangeException("desiredFrequency", desiredFrequency, "This parameter must be a positive number");
            if (samplesPerBuffer <= 0)
                throw new ArgumentOutOfRangeException("samplesPerBuffer", samplesPerBuffer, "This parameter must be a positive number");
            if (cyclesPerBuffer <= 0)
                throw new ArgumentOutOfRangeException("cyclesPerBuffer", cyclesPerBuffer, "This parameter must be a positive number");

            // First configure the Task timing parameters
            if (timingSubobject.SampleTimingType == SampleTimingType.OnDemand)
                timingSubobject.SampleTimingType = SampleTimingType.SampleClock;

            _desiredSampleClockRate = (desiredFrequency * samplesPerBuffer) / cyclesPerBuffer;
            _samplesPerCycle = samplesPerBuffer / cyclesPerBuffer;

            // Determine the actual sample clock rate
            timingSubobject.SampleClockRate = _desiredSampleClockRate;
            _resultingSampleClockRate = timingSubobject.SampleClockRate;

            _resultingFrequency = _resultingSampleClockRate / (samplesPerBuffer / cyclesPerBuffer);

            switch (type)
            {
                case WaveformType.SineWave:
                    _data = GenerateSineWave(_resultingFrequency, amplitude, _resultingSampleClockRate, samplesPerBuffer);
                    break;
                default:
                    // Invalid type value
                    // Debug.Assert(false);
                    break;
            }
        }

        public static double[] GenerateSineWave(double frequency, double amplitude, double sampleClockRate, double samplesPerBuffer)
        {
            double deltaT = 1 / sampleClockRate; // sec./samp
            int intSamplesPerBuffer = (int)samplesPerBuffer;

            double[] rVal = new double[intSamplesPerBuffer];

            for (int i = 0; i < intSamplesPerBuffer; i++)
                rVal[i] = amplitude * Math.Sin((2.0 * Math.PI) * frequency * (i * deltaT));

            return rVal;
        }

    }
}
