using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace synthetizer
{
    public class WAVBuilder
    {
        public string Path { get; set; }

        public WAVBuilder(string path) { Path = path; }

        public void Write(Signal signal)
        {
            if(null == signal || null == signal.Samples)
            {
                throw new ArgumentNullException("Signal or it's samples may not be null!");
            }

            FileStream file = File.OpenWrite(Path);
            BinaryWriter writer = new BinaryWriter(file, Encoding.UTF8, false);

            // header
            writer.Write(Encoding.UTF8.GetBytes("RIFF")); // RIFF ID
            writer.Write(4 + 26 + 12 + (8 + sizeof(float) * 1 * signal.Samples.Length + 0)); // chunk size
            writer.Write(Encoding.UTF8.GetBytes("WAVE"), 0, 4); // WAVE ID
            writer.Write(Encoding.UTF8.GetBytes("fmt "), 0, 4); // fmt chunk ID
            writer.Write(18); // chunk size
            writer.Write((short)3); // format code
            writer.Write((short)1); // channel number
            writer.Write(signal.SamplingRate * 1000); // samples/sec
            writer.Write(signal.SamplingRate * 1000 * sizeof(float) * 1); // avg bytes/sec
            writer.Write((short)(sizeof(float) * signal.Samples.Length)); // block align
            writer.Write((short)(8 * sizeof(float))); // bits/sample
            writer.Write((short)0); // extension size = 0
            writer.Write(Encoding.UTF8.GetBytes("fact"), 0, 4); // fact chunk ID
            writer.Write(4); // chunk size
            writer.Write(1 * signal.Samples.Length); // sample length
            writer.Write(Encoding.UTF8.GetBytes("data"), 0, 4); // data chunk ID
            writer.Write(sizeof(float) * 1 * signal.Samples.Length); // chunk size

            // sampled data
            foreach(float sample in signal.Samples)
            {
                writer.Write(sample);
            }

            file.Close();
        }
    }
}
