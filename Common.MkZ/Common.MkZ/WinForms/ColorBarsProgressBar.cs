using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.WinForms
{
    public class ColorBarsProgressBar : ProgressBar
    {
        public enum ColorsThemeType
        {
            Custom,
            Regular,
            Speaker,
            Microphone
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ThemeColorSet
        {
            private SolidBrush[] brushes = new SolidBrush[6];

            private ColorsThemeType _theme = ColorsThemeType.Regular;
            [DisplayName("Color Theme")]
            [Description("Color Theme by ProgressBar Designation")]
            public ColorsThemeType Theme { get { return _theme; } set { UpdateTheme(value); } }

            public Color Part1_ActiveColor { get { return brushes[0].Color; } set { UpdateColor(0, value); } }
            public Color Part2_ActiveColor { get { return brushes[1].Color; } set { UpdateColor(1, value); } }
            public Color Part3_ActiveColor { get { return brushes[2].Color; } set { UpdateColor(2, value); } }
            public Color Part1_InactiveColor { get { return brushes[3].Color; } set { UpdateColor(3, value); } }
            public Color Part2_InactiveColor { get { return brushes[4].Color; } set { UpdateColor(4, value); } }
            public Color Part3_InactiveColor { get { return brushes[5].Color; } set { UpdateColor(5, value); } }

            public int Threshold1 { get; set; } = 0;
            public int Threshold2 { get; set; } = 100;

            public ThemeColorSet()
            {
                UpdateTheme(ColorsThemeType.Regular);
            }

            private void UpdateTheme(ColorsThemeType theme)
            {
                if (theme == _theme)
                    return;
                _theme = theme;

                switch (_theme)
                {
                    case ColorsThemeType.Speaker:
                        brushes[0] = (SolidBrush)Brushes.LimeGreen; 
                        brushes[1] = (SolidBrush)Brushes.Orange; 
                        brushes[2] = (SolidBrush)Brushes.Red;
                        brushes[3] = (SolidBrush)Brushes.Honeydew; 
                        brushes[4] = (SolidBrush)Brushes.LightGoldenrodYellow; 
                        brushes[5] = (SolidBrush)Brushes.MistyRose;
                        Threshold1 = 60; 
                        Threshold2 = 85;
                        break;
                    case ColorsThemeType.Microphone:
                        brushes[0] = (SolidBrush)Brushes.Orange; 
                        brushes[1] = (SolidBrush)Brushes.LimeGreen;
                        brushes[2] = (SolidBrush)Brushes.Red;
                        brushes[3] = (SolidBrush)Brushes.LightGoldenrodYellow; 
                        brushes[4] = (SolidBrush)Brushes.Honeydew;
                        brushes[5] = (SolidBrush)Brushes.MistyRose;
                        Threshold1 = 10; 
                        Threshold2 = 85;
                        break;
                    case ColorsThemeType.Regular:
                        brushes[0] = (SolidBrush)Brushes.LimeGreen; 
                        brushes[1] = (SolidBrush)Brushes.LimeGreen;
                        brushes[2] = (SolidBrush)Brushes.LimeGreen;
                        brushes[3] = (SolidBrush)Brushes.Gainsboro; 
                        brushes[4] = (SolidBrush)Brushes.Gainsboro;
                        brushes[5] = (SolidBrush)Brushes.Gainsboro;
                        Threshold1 = 100; 
                        Threshold2 = 100;
                        break;
                    case ColorsThemeType.Custom:
                    default:
                        break;
                }
            }

            private void UpdateColor(int i, Color c)
            {
                if (brushes[i] != null && brushes[i].Color == c)
                    return;

                brushes[i] = new SolidBrush(c);
                _theme = ColorsThemeType.Custom;
            }

            public Brush GetColor(int percent, bool isActive)
            {
                return brushes[GetColorIndex(percent, isActive)];
            }

            private int GetColorIndex(int percent, bool active)
            {
                int offset = active ? 0 : 3;
                if (percent < Threshold1)
                    return offset + 0;
                else if (percent >= Threshold1 && percent < Threshold2)
                    return offset + 1;
                else
                    return offset + 2;
            }

            public override string ToString()
            {
                return "Theme: " + Theme + ", " + Threshold1 + "-|--|-" + Threshold2;
            }
        }

        private const string CAT = "Color Bars";

        [Category("Behavior")]
        public int ValuePercent { get { return (100 * (Value - Minimum)) / (Maximum - Minimum); } }

        #region Colors

        [Category(CAT)]
        public ThemeColorSet ColorTheme { get; set; } = new ThemeColorSet();

        #endregion        
        
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
            Brush brush = ColorTheme.GetColor(barPercent, isBarActive);
            g.FillRectangle(brush, rBar);
        }
    }
}
