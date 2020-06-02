using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CircularGauge
{
    public class HighlightBrush
    {
        private static BrushConverter _converter = new BrushConverter();

        public enum HighlightColor : long
        {
            Black = 0xFF000000,
            MintCream = 0xFFF5FFFA,
            Honewdew = 0xFFF0FFF0,
            Azure = 0xFFF0FFFF,
            Aqua = 0xFF00FFFF,
            Cyan = 0xFF00FFFF,
            DodgerBlue = 0xFF1E90FF,
            Blue = 0xFF0000FF,
            Yellow = 0xFFFFFF00,
            Gold = 0xFFFFD700,
            Goldenrod = 0xFFDAA520,
            GreenYellow = 0xFFADFF2F,
            LawnGreen = 0xFF7CFC00,
            Lime = 0xFF00FF00,
            MediumSpringGreen = 0xFF00FA9A,
            Magenta = 0xFFFF00FF,
            Fuchsia = 0xFFFF00FF,
            Red = 0xFFFF0000
        }

        public HighlightColor eColor { get; private set; } = HighlightColor.Lime;

        public SolidColorBrush Brush { get; private set; } = Brushes.Lime;
        public Color Color { get; private set; } = Colors.Lime;

        public HighlightBrush() : this(HighlightColor.Lime)
        {

        }

        public HighlightBrush(HighlightColor color)
        {
            eColor = color;
            Color = (Color)ColorConverter.ConvertFromString(color.ToString());
            Brush = new SolidColorBrush(Color);
        }

        public HighlightBrush(SolidColorBrush brush)
        {
            Brush = brush;
            eColor = ConvertBack(brush);
            Color = brush.Color; // System.Drawing.Color.FromName(nameof(eColor));
        }

        public static Brush ConvertFrom(HighlightColor color)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(color.ToString()));
        }

        public static HighlightColor ConvertBack(SolidColorBrush brush)
        {
            string sColor = GetColorName(brush);
            if (string.IsNullOrWhiteSpace(sColor))
                return HighlightColor.Black;

            HighlightColor c;
            if (Enum.TryParse<HighlightColor>(sColor, out c))
                return c;

            return HighlightColor.Black;
        }

        private static string GetColorName(SolidColorBrush brush)
        {
            var results = typeof(Colors).GetProperties().Where(
             p => (Color)p.GetValue(null, null) == brush.Color).Select(p => p.Name);
            return results.Count() > 0 ? results.First() : String.Empty;
        }
    }

    public class HighlightColorConverter : TypeConverter
    {
        //should return true if sourcetype is string
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        //should return true when destinationtype if GeopointItem
        
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        //Actual convertion from string to HighlightBrush.HighlightColor
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    return (HighlightBrush.HighlightColor)Enum.Parse(typeof(HighlightBrush.HighlightColor), value as string);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Cannot convert '{0}' ({1}) because {2}", value, value.GetType(), ex.Message), ex);
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        //Actual convertion from HighlightBrush.HighlightColor to string
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
                throw new ArgumentNullException("destinationType");

            //HighlightBrush.HighlightColor gpoint = value as HighlightBrush.HighlightColor;

            //if (gpoint != null)
            //    if (this.CanConvertTo(context, destinationType))
            //        return gpoint.ToString();

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
