using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MkZ.MediaPlayer.Controls
{
    [ValueConversion(typeof(TimeSpan), typeof(double))]
    public class TimeSpanToSecondsConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double dur)
                return dur;

            if (!(value is TimeSpan ts))
                return 0;

            return ts.TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is double pos))
                return TimeSpan.FromSeconds(0);
            return TimeSpan.FromSeconds(pos);
        }

        #endregion
    }

    [ValueConversion(typeof(double), typeof(string))]
    public class SecondsToStringConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double dur)
                return TimeSpan.FromSeconds(dur).ToString("mm':'ss'.'f");

            return "--/--";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
