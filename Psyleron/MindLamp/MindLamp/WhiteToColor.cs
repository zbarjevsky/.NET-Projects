using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MindLamp
{
	public class WhiteToColor
	{
		public Color[] m_Colors = new Color[10] {
			Color.White,
			Color.Red, Color.Orange, Color.Yellow, Color.Green, 
			Color.Cyan, Color.Blue, Color.Violet, Color.Magenta,
			Color.Black
		};

		public int m_current = 0, m_iColorIndex = 1;

		public Color GetWhiteToColor(int current, int buffSize, out int colorIdx)
		{
			colorIdx = -1;
			if (Math.Sign(current) != Math.Sign(m_current))//Color changes
			{
				byte val = PsyleronApi.PsyREGGetByte(0);
				m_iColorIndex = val%8+1;
				colorIdx = m_iColorIndex;
			}

			m_current = current;

			double percent = GetPercent(current, buffSize);
			if (percent < 0.1)
			{
				return Color.White;
			}
			else
			{
				ColorHSL hsl = new ColorHSL(m_Colors[m_iColorIndex]);
				hsl.Luminosity = 240 - (140 * percent);
				return hsl;
			}
		}

		public Color GetRainbowColor(int current, int buffSize, out int colorIdx)
		{
			colorIdx = -1;
			if (Math.Sign(current) != Math.Sign(m_current))//Color changes
			{
				m_iColorIndex += Math.Sign(current);
				if (m_iColorIndex < 1) m_iColorIndex = 8;
				if (m_iColorIndex > 8) m_iColorIndex = 1;
				colorIdx = m_iColorIndex;
			}

			m_current = current;

			double percent = GetPercent(current, buffSize);
			if (percent < 0.1)
			{
				return Color.White;
			}
			else
			{
				ColorHSL hsl = new ColorHSL(m_Colors[m_iColorIndex]);
				hsl.Luminosity = 240 - (140 * percent);
				return hsl;
			}
		}

		private double GetPercent(int current, int buffSize)
		{
			double index = 100;// 1000.0 / buffSize;
			current = Math.Abs(current);
			if (current >= index)
				return 1.0;

			return (current / index) ;
		}
	}
}
