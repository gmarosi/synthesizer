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

            // header
            file.Write(Encoding.ASCII.GetBytes("RIFF"), 0, 4); // RIFF ID
            file.Write(BitConverter.GetBytes(4 + 26 + 12 + (8 + sizeof(float) * 1 * signal.Samples.Length + 0)), 0, 4); // chunk size
            file.Write(Encoding.ASCII.GetBytes("WAVE"), 0, 4); // WAVE chunk ID
            file.Write(Encoding.ASCII.GetBytes("fmt "), 0, 4); // fmt chunk ID
            file.Write(BitConverter.GetBytes((short)3), 0, 2); // format code
            file.Write(BitConverter.GetBytes((short)1), 0, 2); // channel number
            file.Write(BitConverter.GetBytes(signal.SamplingRate * 1000), 0, 4); // samples/sec
            file.Write(BitConverter.GetBytes(signal.SamplingRate * 1000 * sizeof(float) * 1), 0, 4); // avg bytes/sec
            file.Write(BitConverter.GetBytes((short)(sizeof(float) * signal.Samples.Length)), 0, 2); // block align
            file.Write(BitConverter.GetBytes((short)(8 * sizeof(float))), 0, 2); // bits/sample
            file.Write(BitConverter.GetBytes((short)0), 0, 2); // extension size = 0
            file.Write(Encoding.ASCII.GetBytes("fact"), 0, 4); // fact chunk ID
            file.Write(BitConverter.GetBytes(4), 0, 4); // chunk size
            file.Write(BitConverter.GetBytes(1 * signal.Samples.Length), 0, 4); // sample length
            file.Write(Encoding.ASCII.GetBytes("data"), 0, 4); // data chunk ID
            file.Write(BitConverter.GetBytes(sizeof(float) * 1 * signal.Samples.Length), 0, 4); // chunk size

            // sampled data
            foreach(float sample in signal.Samples)
            {
                file.Write(BitConverter.GetBytes(sample), 0, 4);
            }

            file.Close();
        }
    }
}
