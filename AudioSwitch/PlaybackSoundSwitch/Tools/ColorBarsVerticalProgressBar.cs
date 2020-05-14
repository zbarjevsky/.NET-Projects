using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Controls
{
    public class ColorBarsVerticalProgressBar : PictureBox
    {
        //what color to show when inactive - more than value
        public enum InactiveColorsTheme
        {
            Dark,
            Pale,
            Gray
        }

        private int _value = 0;
        public int Value { get { return _value; } set { _value = value; Refresh(); } }

        private InactiveColorsTheme _inactiveColorTheme = InactiveColorsTheme.Pale;
        public InactiveColorsTheme InactiveColorTheme { get { return _inactiveColorTheme; } set { _inactiveColorTheme = value; Refresh(); } }

        public ColorBarsVerticalProgressBar()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            
            Rectangle r = this.ClientRectangle;

            int barHeight = 6;
            int barsCount = r.Height / barHeight;
            int index = barsCount - (int)(_value * barsCount / 100.0);

            //clear graphics
            pe.Graphics.Clear(this.BackColor);

            int margin = 2;
            for (int i = 0; i <= barsCount; i++)
            {
                int x = r.Left + margin;
                int y = r.Top + margin + i * barHeight;
                int width = r.Width - 2 * margin;
                int height = barHeight - margin;

                //if out of bounds
                if (y + height + margin > this.Height)
                    height = r.Height - y - margin;

                //Brush brush = i > index ? GetColor((barsCount - i) / (double)barsCount) : Brushes.Gainsboro;
                Brush brush = GetColor((barsCount - i) / (double)barsCount, i > index);// Brushes.Gainsboro;
                pe.Graphics.FillRectangle(brush, new Rectangle(x, y, width, height));
            }
        }

        private Brush GetColor(double percent, bool bActive)
        {
            int colorIdx = 0;
            if (percent < 0.7)
                colorIdx = 0;
            else if (percent >= 0.7 && percent <= 0.85)
                colorIdx = 1;
            else
                colorIdx = 2;

            return bActive ? _activeColors[colorIdx] : GetInactiveThemeColor(colorIdx);
        }

        private Brush[] _activeColors = new Brush[] { Brushes.LimeGreen, Brushes.Goldenrod, Brushes.Red };
        private Brush[] _paleColors = new Brush[] { Brushes.Honeydew, Brushes.PaleGoldenrod, Brushes.MistyRose };
        private Brush[] _darkColors = new Brush[] { Brushes.DarkGreen, Brushes.DarkGoldenrod, Brushes.DarkRed };

        private Brush GetInactiveThemeColor(int idx)
        {
            switch (InactiveColorTheme)
            {
                case InactiveColorsTheme.Dark: return _darkColors[idx];
                case InactiveColorsTheme.Pale: return _paleColors[idx];
                case InactiveColorsTheme.Gray: return Brushes.Gainsboro;
                default: return Brushes.Gainsboro;
            }
        }
    }
}
