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
			colorIdx = GetPercentIdx(relative);
            return m_Colors[colorIdx];
			//ColorHSL hsl = new ColorHSL(m_Colors[colorIdx]);
			//hsl.Luminosity = 240 - (140 * percent);
		}

		public static Color GetRainbowColor(double relative, out int colorIdx)
		{
            colorIdx = GetPercentIdx(relative);
            return m_Colors[colorIdx];
            //ColorHSL hsl = new ColorHSL(m_Colors[colorIdx]);
            //hsl.Luminosity = 240 - (140 * percent);
			//return hsl;
		}

		private static int GetPercentIdx(double relative)
		{
            if (relative < m_ColorRange[0])
                return 0;

            if (relative >= m_ColorRange.Last())
                return m_ColorRange.Length -1;

            for (int i = 1; i < m_ColorRange.Length; i++)
            {
                if (relative >= m_ColorRange[i - 1] && relative < m_ColorRange[i])
                    return i;
            }

            return 0;
		}
	}
}
