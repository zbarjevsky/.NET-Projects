using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscillator_DETA
{
    public class OscillatorUtils
    {
        //public short[] GenerateOscillatorSampleData(Oscillator osc)
        //{
        //    // Creates a looping buffer based on the params given
        //    // Fill the buffer with whatever waveform at the specified frequency            
        //    int numSamples = Convert.ToInt32(bufferDurationSeconds *  waveFormat.SamplesPerSecond);
        //    short[] sampleData = new short[numSamples];
        //    double frequency = osc.Frequency;
        //    int amplitude = osc.Amplitude;
        //    double angle = (Math.PI * 2 * frequency) / (waveFormat.SamplesPerSecond * waveFormat.Channels);

        //    switch (osc.WaveType)
        //    {
        //        case WaveType.Sine:
        //            {
        //                for (int i = 0; i < numSamples; i++)
        //                    // Generate a sine wave in both channels.
        //                    sampleData[i] = Convert.ToInt16(amplitude *  Math.Sin(angle * i));
        //            }
        //            break;
        //        case WaveType.Square:
        //            {
        //                for (int i = 0; i < numSamples; i++)
        //                {
        //                    // Generate a square wave in both channels.
        //                    if (Math.Sin(angle * i) > 0)
        //                        sampleData[i] = Convert.ToInt16(amplitude);
        //                    else
        //                        sampleData[i] = Convert.ToInt16(-amplitude);
        //                }
        //            }
        //            break;
        //        case WaveType.Sawtooth:
        //            {
        //                int samplesPerPeriod = Convert.ToInt32(waveFormat.SamplesPerSecond / (frequency / waveFormat.Channels));
        //                short sampleStep = Convert.ToInt16((amplitude * 2) / samplesPerPeriod);
        //                short tempSample = 0;

        //                int i = 0;
        //                int totalSamplesWritten = 0;
        //                while (totalSamplesWritten < numSamples)
        //                {
        //                    tempSample = (short)-amplitude;
        //                    for (i = 0; i < samplesPerPeriod && totalSamplesWritten < numSamples; i++)
        //                    {
        //                        tempSample += sampleStep;
        //                        sampleData[totalSamplesWritten] = tempSample;

        //                        totalSamplesWritten++;
        //                    }
        //                }
        //            }
        //            break;
        //        case WaveType.Noise:
        //            {
        //                Random rnd = new Random();
        //                for (int i = 0; i < numSamples; i++)
        //                {
        //                    sampleData[i] = Convert.ToInt16(rnd.Next(-amplitude, amplitude));
        //                }
        //            }
        //            break;
        //    }
        //    return sampleData;
        //}
    }
}
