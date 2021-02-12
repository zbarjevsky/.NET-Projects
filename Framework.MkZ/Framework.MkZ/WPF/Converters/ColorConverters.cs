using MkZ.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MkZ.WPF.Converters
{
    [ValueConversion(typeof(Brush), typeof(Color))]
    public class MyBrushToColorConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
                return brush.Color;

            return Colors.Pink;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    [ValueConversion(typeof(Brush), typeof(Brush))]
    public class BrushOpacityConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                double opacity = 0.3;
                if (parameter is string p)
                    opacity = System.Convert.ToDouble(p);
                return new SolidColorBrush(brush.Color) { Opacity = opacity };
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BoolToBrushConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool isFirst)
            {
                if (parameter is string brushes)
                {
                    string[] colors = brushes.Split('|');
                    if (isFirst)
                        return StringToBrush(colors[0]);
                    return StringToBrush(colors[1]);
                }
                return Brushes.Black;
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion

        private Brush StringToBrush(string sColor)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(sColor));
        }
    }
}