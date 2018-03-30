using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscillator_DETA
{
    public class Oscillator1
    {

        public static double [] _buffer;

        public static System.Media.SoundPlayer PlayBeep(double frequency1, double frequency2, int msDuration, bool sine, UInt16 volume = 16383)
        {
            var mStrm = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(mStrm);

            int formatChunkSize = 16;
            int headerSize = 8;
            short formatType = 1;
            short tracks = 1;
            int samplesPerSecond = 240000;// 44100;
            short bitsPerSample = 16;
            short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            int bytesPerSecond = samplesPerSecond * frameSize;
            int waveSize = 4;
            int samples = (int)((decimal)samplesPerSecond * msDuration / 1000);
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
            // var encoding = new System.Text.UTF8Encoding();
            writer.Write(0x46464952); // = encoding.GetBytes("RIFF")
            writer.Write(fileSize);
            writer.Write(0x45564157); // = encoding.GetBytes("WAVE")
            writer.Write(0x20746D66); // = encoding.GetBytes("fmt ")
            writer.Write(formatChunkSize);
            writer.Write(formatType);
            writer.Write(tracks);
            writer.Write(samplesPerSecond);
            writer.Write(bytesPerSecond);
            writer.Write(frameSize);
            writer.Write(bitsPerSample);
            writer.Write(0x61746164); // = encoding.GetBytes("data")
            writer.Write(dataChunkSize);
            {
                if (sine)
                {
                    _buffer = SineWave(frequency1, volume, samplesPerSecond, samples);
                    _buffer = SineWaveModulation(_buffer, volume/10.0, frequency2, samplesPerSecond, samples);
                }
                else
                {
                    _buffer = SineWave(frequency1, volume, samplesPerSecond, samples);
                    _buffer = SquareWave(_buffer, frequency2, samplesPerSecond, samples);
                }

                for (int step = 0; step < samples; step++)
                    writer.Write((short)_buffer[step]);
            }

            mStrm.Seek(0, SeekOrigin.Begin);
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(mStrm);
            player.PlaySync();
            writer.Close();
            mStrm.Close();

            return player;
        }

        private static double[] SineWave(double frequency, ushort volume, int samplesPerSecond, int samples)
        {
            // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
            // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
            double amp = volume >> 2; // so we simply set amp = volume / 2

            double[] buffer = new double[samples];
            for (int step = 0; step < samples; step++)
                buffer[step] = amp;

            return SineWave(buffer, frequency, samplesPerSecond, samples);
        }

        private static double[] SineWave(double[] buffer, double frequency, int samplesPerSecond, int samples)
        {
            const double TAU = 2 * Math.PI;

            double theta = frequency * TAU / (double)samplesPerSecond;

            for (int step = 0; step < samples; step++)
            {
                buffer[step] = (buffer[step] * Math.Sin(theta * step));
            }

            return buffer;
        }

        private static double[] SineWaveModulation(double[] buffer, double volume, double frequency, int samplesPerSecond, int samples)
        {
            const double TAU = 2 * Math.PI;

            double theta = frequency * TAU / (double)samplesPerSecond;

            for (int step = 0; step < samples; step++)
            {
                buffer[step] += volume * Math.Sin(theta * step);
            }

            return buffer;
        }

        private static double[] SquareWave(double frequency, ushort volume, int samplesPerSecond, int samples)
        {
            // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
            // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
            double amp = (volume >> 2); // so we simply set amp = volume / 2

            double[] buffer = new double[samples];
            for (int step = 0; step < samples; step++)
                buffer[step] = amp;

            return SquareWave(buffer, frequency, samplesPerSecond, samples);
        }

        private static double[] SquareWave(double[] buffer, double frequency, int samplesPerSecond, int samples)
        {
            short stepsPerWaveLen = (short)(samplesPerSecond / frequency);

            short vol = 1;
            for (int step = 0; step < samples; step++)
            {
                if (step % stepsPerWaveLen == 0)
                {
                    vol = (short)((vol == 1) ? 0 : 1);
                }
                buffer[step] *= vol;
            }
            return buffer;
        }
    }
}
