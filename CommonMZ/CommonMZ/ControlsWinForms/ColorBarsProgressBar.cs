using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Controls
{
    public class ColorBarsProgressBar : ProgressBar
    {
        //what color to show when inactive - more than value
        public enum InactiveColorType
        {
            Pale, //pale multicolor
            Mono  //single color
        }

        public enum ColorsThemeType
        {
            Regular,
            Speaker,
            Microphone
        }

        public class ColorSet
        {
            private int _changeableColorIndex { get; }
            private SolidBrush[] brushes;

            public int Threshold1 { get; }
            public int Threshold2 { get; }

            public SolidBrush ChangeableColor { get { return brushes[_changeableColorIndex]; } set { brushes[_changeableColorIndex] = value; } }

            public ColorSet(Color c1) 
            {
                _changeableColorIndex = 0; ;
                Threshold1 = 0;
                Threshold2 = 0;
                brushes = new SolidBrush[] { new SolidBrush(c1) };
            }
            public ColorSet(Color c1, Color c2, Color c3, int threshold1 = 0, int threshold2 = 100, int activeColorIndex = 0)
            {
                _changeableColorIndex = activeColorIndex;
                Threshold1 = threshold1;
                Threshold2 = threshold2;
                brushes = new SolidBrush[] { new SolidBrush(c1), new SolidBrush(c2), new SolidBrush(c3) };
            }
            public ColorSet(Color cActive, ColorSet baseSet)
            {
                _changeableColorIndex = baseSet._changeableColorIndex;
                Threshold1 = baseSet.Threshold1;
                Threshold2 = baseSet.Threshold2;
                brushes = new SolidBrush[] { baseSet.brushes[0], baseSet.brushes[1], baseSet.brushes[2] };
                ChangeableColor = new SolidBrush(cActive);
            }

            public Brush GetColor(int percent)
            {
                if (brushes.Length == 1)
                    return brushes[0];

                return brushes[GetColorIndex(percent)];
            }

            private int GetColorIndex(int percent)
            {
                if (percent < Threshold1)
                    return 0;
                else if (percent >= Threshold1 && percent <= Threshold2)
                    return 1;
                else
                    return 2;
            }

            public static ColorSet Pale(ColorsThemeType theme)
            {
                switch (theme)
                {
                    case ColorsThemeType.Speaker:
                        return SpkPale();
                    case ColorsThemeType.Microphone:
                        return MicPale();
                    case ColorsThemeType.Regular:
                    default:
                        return new ColorSet(Color.Gainsboro);
                }
            }

            public static ColorSet MicActive() { return new ColorSet(Color.Yellow, Color.LimeGreen, Color.Red, 10, 75, 1); }
            public static ColorSet MicPale() { return new ColorSet(Color.PaleGoldenrod, Color.Honeydew, Color.MistyRose, 10, 75); }

            public static ColorSet SpkActive() { return new ColorSet(Color.LimeGreen, Color.Goldenrod, Color.Red, 50, 85, 0); }
            public static ColorSet SpkPale() { return new ColorSet(Color.Honeydew, Color.PaleGoldenrod, Color.MistyRose, 50, 85); }
            public static ColorSet SpkDark() { return new ColorSet(Color.DarkGreen, Color.DarkGoldenrod, Color.DarkRed, 50, 85); }
        }

        public class ColorTheme
        {
            public ColorSet Active, Inactive;
 
            public ColorsThemeType Theme { get; }

            private InactiveColorType _inactiveBarsType = InactiveColorType.Mono; 
            public InactiveColorType InactiveBarsType { get { return _inactiveBarsType; } set { SetInactiveType(value); } }

            private void SetInactiveType(InactiveColorType inactiveBarsType)
            {
                if (_inactiveBarsType == inactiveBarsType)
                    return;

                _inactiveBarsType = inactiveBarsType;
                switch (_inactiveBarsType)
                {
                    case InactiveColorType.Pale:
                        Inactive = ColorSet.Pale(Theme);
                        break;
                    case InactiveColorType.Mono:
                    default:
                        Inactive = new ColorSet(InactiveColor);
                        break;
                }
            }

            public Color ActiveColor
            {
                get { return Active.ChangeableColor.Color; }
                set { Active.ChangeableColor = new SolidBrush(value); }
            }

            public Color InactiveColor
            {
                get { return Inactive.ChangeableColor.Color; }
                set { Inactive.ChangeableColor = new SolidBrush(value); }
            }

            public ColorTheme(Color activeColor, ColorsThemeType theme)
            {
                Theme = theme;

                switch (theme)
                {
                    case ColorsThemeType.Speaker:
                        Active = ColorSet.SpkActive();
                        Inactive = ColorSet.SpkPale();
                        break;
                    case ColorsThemeType.Microphone:
                        Active = ColorSet.MicActive();
                        Inactive = ColorSet.MicPale();
                        break;
                    case ColorsThemeType.Regular:
                    default:
                        Active = new ColorSet(activeColor);
                        Inactive = new ColorSet(Color.Gainsboro);
                        break;
                }
                Active.ChangeableColor = new SolidBrush(activeColor);
            }

            public Brush GetColor(int percent, bool isActive)
            {
                return isActive ? Active.GetColor(percent) : Inactive.GetColor(percent);
            }
        }

        private const string CAT = "Color Bars";

        private ColorTheme _colorTheme = new ColorTheme(Color.LimeGreen, ColorsThemeType.Microphone);

        public int ValuePercent { get { return (100 * (Value - Minimum)) / (Maximum - Minimum); } }

        [Category(CAT)]
        public ColorsThemeType ColorThemeType 
        { 
            get { return _colorTheme.Theme; } 
            set { _colorTheme = new ColorTheme(ActiveColor, value); Refresh(); } 
        }

        [Category(CAT)]
        public InactiveColorType InactiveBarsColorType
        { 
            get { return _colorTheme.InactiveBarsType; } 
            set { _colorTheme.InactiveBarsType = value; Refresh(); } 
        }

        [Category(CAT)]
        public Color ActiveColor
        {
            get { return _colorTheme.ActiveColor; }
            set { _colorTheme.ActiveColor = value; Refresh(); }
        }

        [Category(CAT)]
        public Color InactiveColor
        {
            get { return _colorTheme.InactiveColor; }
            set { _colorTheme.InactiveColor = value; Refresh(); }
        }

        private Orientation _orientation = Orientation.Vertical;
        [Category(CAT)]
        public Orientation Orientation { get { return _orientation; } set { _orientation = value; Refresh(); } }

        public ColorBarsProgressBar()
        {
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
        }

        const int margin = 2;
        const int barSize = 6;

        protected override void OnPaint(PaintEventArgs pe)
        {
            //clear graphics
            pe.Graphics.Clear(this.BackColor);

            int barsCount = CalcBarsCount();
            for (int i = 0; i < barsCount; i++)
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

            int barPercent = (int)Math.Ceiling(100.0 * (barIndex) / (double)barsCount);

            if (_orientation == Orientation.Vertical)
            {
                rBar.X = rClnt.X + margin;
                rBar.Y = rClnt.Y + margin + barIndex * barSize;
                rBar.Width = rClnt.Width - 2 * margin;
                rBar.Height = barSize - margin;

                //if out of bounds
                if (rBar.Y + rBar.Height + margin > this.Height)
                    rBar.Height = rClnt.Height - rBar.Y - margin;

                barPercent = (int)Math.Ceiling(100.0 * (barsCount - barIndex - 1) / (double)barsCount);
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

            }

            bool isBarActive = ValuePercent > 0 && (barPercent) <= ValuePercent;
            Brush brush = _colorTheme.GetColor(barPercent, isBarActive);
            g.FillRectangle(brush, rBar);
        }
    }
}
