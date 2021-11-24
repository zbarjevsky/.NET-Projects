using DashCamGPSView.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DashCamGPSView.Tools
{
    public class VideoFileToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is VideoFile v)
            {
                switch (v.FileType)
                {
                    case RecordingType.Parking:
                        if (v.IsProtected)
                            return new Uri("/Images/Warning.png", UriKind.RelativeOrAbsolute);
                        else
                            return new Uri("/Images/Parking.png", UriKind.RelativeOrAbsolute);
                    //case RecordingType.ReadOnly:
                    //    return new Uri("/Images/Warning.png", UriKind.RelativeOrAbsolute);
                    case RecordingType.Unknown:
                    case RecordingType.Driving:
                    default:
                        if (v.IsProtected)
                            return new Uri("/Images/Warning.png", UriKind.RelativeOrAbsolute);
                        else
                            return new Uri("/Images/Movie48.png", UriKind.RelativeOrAbsolute);
                }
            }
            else
            {
                return new Uri("/Images/Movie48.png", UriKind.RelativeOrAbsolute);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
