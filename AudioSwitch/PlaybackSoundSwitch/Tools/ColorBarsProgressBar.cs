using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Controls
{
    public class ColorBarsProgressBar : PictureBox
    {
        //what color to show when inactive - more than value
        public enum InactiveColorsTheme
        {
            Dark,
            Pale,
            Gray
        }

        public enum ActiveColorsTheme
        {
            Multicolor,
            SingleColor
        }

        private const string CAT = "Color Bars";

        private int _value = 0;
        [Category(CAT)]
        public int Value { get { return _value; } set { _value = value; Refresh(); } }

        private InactiveColorsTheme _inactiveColorTheme = InactiveColorsTheme.Pale;
        [Category(CAT)]
        public InactiveColorsTheme InactiveColorTheme { get { return _inactiveColorTheme; } set { _inactiveColorTheme = value; Refresh(); } }

        private ActiveColorsTheme _activeColorTheme = ActiveColorsTheme.Multicolor;
        [Category(CAT)]
        public ActiveColorsTheme ActiveColorTheme { get { return _activeColorTheme; } set { _activeColorTheme = value; Refresh(); } }

        private SolidBrush _activeColor = (SolidBrush)Brushes.LimeGreen;
        [Category(CAT)]
        public Color ActiveColor { get { return _activeColor.Color; } set { _activeColor = new SolidBrush(value); Refresh(); } }

        private Orientation _orientation = Orientation.Vertical;
        [Category(CAT)]
        public Orientation Orientation { get { return _orientation; } set { _orientation = value; Refresh(); } }

        public ColorBarsProgressBar()
        {
            this.DoubleBuffered = true;
        }

        const int margin = 2;
        const int barSize = 6;

        protected override void OnPaint(PaintEventArgs pe)
        {
            //clear graphics
            pe.Graphics.Clear(this.BackColor);

            int barsCount = CalcBarsCount();
            for (int i = 0; i <= barsCount; i++)
                DrawBarRectangle(i, barsCount, pe.Graphics);
         }

        private int CalcBarsCount()
        {
            if (_orientation == Orientation.Vertical)
                return this.ClientRectangle.Height / barSize;
            return this.ClientRectangle.Width / barSize;
        }

        private void DrawBarRectangle(int barIndex, int barsCount, Graphics g)
        {
            Rectangle rClnt = this.ClientRectangle;
            Rectangle rBar = new Rectangle();

            int valueIndex = barsCount - (int)(_value * barsCount / 100.0);
            double percent = (barsCount - barIndex) / (double)barsCount;
            bool isActive = valueIndex < barIndex;

            if (_orientation == Orientation.Vertical)
            {
                rBar.X = rClnt.X + margin;
                rBar.Y = rClnt.Y + margin + barIndex * barSize;
                rBar.Width = rClnt.Width - 2 * margin;
                rBar.Height = barSize - margin;

                //if out of bounds
                if (rBar.Y + rBar.Height + margin > this.Height)
                    rBar.Height = rClnt.Height - rBar.Y - margin;
            }
            else
            {
                rBar.X = rClnt.X + margin + barIndex * barSize;
                rBar.Y = rClnt.Y + margin;
                rBar.Width = barSize - margin;
                rBar.Height = rClnt.Height - 2 * margin;

                //if out of bounds
                if (rBar.X + rBar.Width + margin > this.Width)
                    rBar.Width = rClnt.Width - rBar.X - margin;

                percent = 1 - percent; //left to right
                isActive = valueIndex < (barsCount - barIndex);
            }

            Brush brush = GetBarColor(percent, isActive);
            g.FillRectangle(brush, rBar);
        }

        private Brush GetBarColor(double percent, bool bActive)
        {
            int colorIdx = 0;
            if (percent < 0.7)
                colorIdx = 0;
            else if (percent >= 0.7 && percent <= 0.85)
                colorIdx = 1;
            else
                colorIdx = 2;

            return bActive ? GetActiveColor(colorIdx) : GetInactiveThemeColor(colorIdx);
        }

        private Brush[] _activeColors = new Brush[] { Brushes.LimeGreen, Brushes.Goldenrod, Brushes.Red };
        private Brush[] _paleColors = new Brush[] { Brushes.Honeydew, Brushes.PaleGoldenrod, Brushes.MistyRose };
        private Brush[] _darkColors = new Brush[] { Brushes.DarkGreen, Brushes.DarkGoldenrod, Brushes.DarkRed };

        private SolidBrush GetActiveColor(int colorIdx)
        {
            if (ActiveColorTheme == ActiveColorsTheme.Multicolor)
                return (SolidBrush)_activeColors[colorIdx];
            return _activeColor;
        }

        private Brush GetInactiveThemeColor(int idx)
        {
            if (ActiveColorTheme == ActiveColorsTheme.SingleColor)
                return Brushes.Gainsboro;

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
