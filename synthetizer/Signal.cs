using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synthetizer
{
    public class Signal
    {
        public int SamplingRate { get; private set; }
        public float[]? Samples { get; private set; }

        /// <summary>
        /// Create a Signal with a sine function
        /// </summary>
        /// <param name="frequency">Frequency in Hz</param>
        /// <param name="amplitude"></param>
        /// <param name="samplingRate">Sampling rate in kHz</param>
        /// <param name="sampleLength">Sample length in seconds</param>
        /// <returns></returns>
        public static Signal SineSignal(int frequency, float amplitude, int samplingRate, float sampleLength)
        {
            Signal signal = new Signal(samplingRate);
            signal.Samples = new float[(int)sampleLength * samplingRate * 1000];
            for(int i = 0; i < sampleLength * samplingRate * 1000; i++)
            {
                float timestamp = (float)i / (samplingRate * 1000);
                signal.Samples[i] = amplitude * MathF.Cos(frequency * MathF.Tau * timestamp);
            }
            return signal;
        }

        public static Signal SquareSignal(int frequency, float amplitude, int samplingRate, float sampleLength)
        {
            Signal signal = new Signal(samplingRate);
            signal.Samples = new float[(int)sampleLength * samplingRate * 1000];
            for (int i = 0; i < sampleLength * samplingRate * 1000; i++)
            {
                float timestamp = (float)i / (samplingRate * 1000);
                signal.Samples[i] = MathF.Cos(frequency * MathF.Tau * timestamp) > 0 ? amplitude : -amplitude;
            }
            return signal;
        }

        public static Signal AddSignals(Signal signal1, float coeff1, Signal signal2, float coeff2)
        {
            if(signal1.SamplingRate != signal2.SamplingRate)
            {
                throw new ArgumentException("Sampling rate of signals must be equal!");
            }
            if(signal1.Samples == null || signal2.Samples == null)
            {
                throw new ArgumentNullException("Signals' Sample fields must be non-null!");
            }
            if(signal1.Samples.Length != signal2.Samples.Length)
            {
                throw new ArgumentException("Signals must be of equal length!");
            }

            Signal combine = new Signal(signal1.SamplingRate);
            combine.Samples = new float[signal1.Samples.Length];

            for(int i = 0; i < combine.Samples.Length; i++)
            {
                combine.Samples[i] = coeff1 * signal1.Samples[i] + coeff2 * signal2.Samples[i];
            }

            return combine;
        }

        /// <param name="samplingRate">Sampling rate in kHz</param>
        private Signal(int samplingRate) { SamplingRate = samplingRate; }
    }
}
