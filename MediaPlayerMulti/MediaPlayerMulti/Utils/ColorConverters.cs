﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MkZ.MediaPlayer.Utils
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
}