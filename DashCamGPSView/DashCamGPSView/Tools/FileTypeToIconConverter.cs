using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DashCamGPSView.Tools
{
    public class FileTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || Enum.IsDefined(value.GetType(), value) == false)
                return new Uri("/Images/Movie48.png", UriKind.RelativeOrAbsolute);

            var parameterValue = (FileType)value;

            switch (parameterValue)
            {
                case FileType.Parking:
                    return new Uri("/Images/Parking.png", UriKind.RelativeOrAbsolute);
                case FileType.ReadOnly:
                    return new Uri("/Images/Warning.png", UriKind.RelativeOrAbsolute);
                case FileType.Unknown:
                case FileType.Recording:
                default:
                    return new Uri("/Images/Movie48.png", UriKind.RelativeOrAbsolute);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
