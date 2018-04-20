using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MindLamp
{
	public class ColorFromValues
	{
		public static Color[] m_Colors = new Color[10] 
        {
			Color.White,
			Color.Red, Color.Orange, Color.Yellow, Color.Green, 
			Color.Cyan, Color.Blue, Color.Violet, Color.Magenta,
			Color.Black
		};

        public static double[] m_ColorRange = new double[]
        {
            -0.4, -0.3, -0.2, -0.1, -0.01, 0.01, 0.1, 0.2, 0.3, 0.4
        };

		public static Color GetWhiteToColor(double relative, out int colorIdx)
		{
			return GetColorRelativeForRange(relative, out colorIdx);
			//ColorHSL hsl = new ColorHSL(m_Colors[colorIdx]);
			//hsl.Luminosity = 240 - (140 * percent);
		}

		public static Color GetRainbowColor(double relative, out int colorIdx)
		{
            return GetColorRelativeForRange(relative, out colorIdx);
            //ColorHSL hsl = new ColorHSL(m_Colors[colorIdx]);
            //hsl.Luminosity = 240 - (140 * percent);
			//return hsl;
		}

		private static Color GetColorRelativeForRange(double relative, out int colorIdx)
		{
		    colorIdx = 0;
            if (relative < m_ColorRange[0])
                return Color.White;

            if (relative >= m_ColorRange.Last())
                return Color.Black;

            for (int i = 1; i < m_ColorRange.Length; i++)
            {
                if (relative >= m_ColorRange[i - 1] && relative < m_ColorRange[i])
                {
                    int percent = (int)(100 * (relative - m_ColorRange[i - 1]) / (m_ColorRange[i] - m_ColorRange[i - 1]));
                    return MixColors(m_Colors[i - 1], m_Colors[i], percent);
                }
            }

            return Color.White;
		}

        public static Color MixColors(Color c1, Color c2, int percent)
        {
            double f1 = (100 - percent) / 100.0;
            double f2 = percent / 100.0;

            double r = (c1.R * f1 + c2.R * f2);
            double g = (c1.G * f1 + c2.G * f2);
            double b = (c1.B * f1 + c2.B * f2);

            Color c3 = Color.FromArgb((int)r, (int)g, (int)b);
            return c3;
        }

    }
}
