using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.WPF.WpfAnalogClock.Tools
{
    public static class ColorConverter
    {
        public static System.Drawing.Color ToWinformsColor(this System.Windows.Media.Brush brush)
        {
            System.Windows.Media.SolidColorBrush b = brush as System.Windows.Media.SolidColorBrush;
            if(b != null)
                return System.Drawing.Color.FromArgb(b.Color.A, b.Color.R, b.Color.G, b.Color.B);

            return System.Drawing.Color.BlueViolet;
        }

        public static System.Windows.Media.Brush ToWpfBrush(this System.Drawing.Color color)
        {
            return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(
                color.A, color.R, color.G, color.B));
        }
    }
}
