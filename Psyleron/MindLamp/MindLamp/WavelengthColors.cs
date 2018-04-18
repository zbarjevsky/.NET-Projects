using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MindLamp
{
	public class WavelengthColors
	{
		public static Color GetColorForRange(double val, double min, double max)
		{
			int waveLength = GetWaveLengthFromRange(val, min, max);
			return GetColorFromWaveLength(waveLength);
		}

		//"Approximate RGB values for Visible Wavelengths"
		// Range 380 - 780
		public static Color GetColorFromWaveLength(int waveLength)
		{
			int w = waveLength;
			double R, G, B;

			if (w >= 380 && w < 440)
			{
				R = -(w - 440.0) / (440.0 - 380.0);
				G = 0.0;
				B = 1.0;
			}
			else if (w >= 440 && w < 490)
			{
				R = 0.0;
				G = (w - 440.0) / (490.0 - 440.0);
				B = 1.0;
			}
			else if (w >= 490 && w < 510)
			{
				R = 0.0;
				G = 1.0;
				B = -(w - 510.0) / (510.0 - 490.0);
			}
			else if (w >= 510 && w < 580)
			{
				R = (w - 510.0) / (580.0 - 510.0);
				G = 1.0;
				B = 0.0;
			}
			else if (w >= 580 && w < 645)
			{
				R = 1.0;
				G = -(w - 645.0) / (645.0 - 580.0);
				B = 0.0;
			}
			else if (w >= 645 && w <= 780)
			{
				R = 1.0;
				G = 0.0;
				B = 0.0;
			}
			else
			{
				R = 0.0;
				G = 0.0;
				B = 0.0;
			}

			return Color.FromArgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
		}

		//WaveLength Range 380 - 780
		public static int GetWaveLengthFromRange(double val, double min, double max)
		{
			int waveRange = 400; //780 - 380
			double range = max - min;

			int result = (int)(380.0 + ((val - min) * waveRange) / range);

			if (result < 380) return 380;
			if (result > 780) return 780;
			return result;
		}
	}
}
