using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MkZ.WPF.Converters
{
    public class BooleanToTextConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool isChecked)
            {
                if (parameter is string s)
                {
                    string[] p = s.Split('|');
                    if(p.Length != 2)
                        throw new NotSupportedException("Should be 2 strings separated by '|'");

                    return isChecked ? p[0] : p[1];
                }
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