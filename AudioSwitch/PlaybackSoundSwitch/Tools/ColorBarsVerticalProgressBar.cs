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
        private int _value = 0;
        public int Value { get { return _value; } set { _value = value; Refresh(); } }

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

                Brush brush = i > index ? GetColor((barsCount - i) / (double)barsCount) : Brushes.Gainsboro;
                pe.Graphics.FillRectangle(brush, new Rectangle(x, y, width, height));
            }
        }

        private Brush GetColor(double percent)
        {
            if (percent < 0.7)
                return Brushes.LimeGreen;
            else if (percent >= 0.7 && percent <= 0.85)
                return Brushes.Goldenrod;
            else
                return Brushes.Red;
        }
    }
}
