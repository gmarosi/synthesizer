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
                signal.Samples[i] = amplitude * MathF.Sin(-frequency * timestamp);
            }
            return signal;
        }

        /// <param name="samplingRate">Sampling rate in kHz</param>
        private Signal(int samplingRate) { SamplingRate = samplingRate; }
    }
}
