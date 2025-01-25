using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MultiPlayer
{
    internal class SelectedIndexToIconTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int selectedIndex)
            {
                if (int.TryParse(parameter as string, out int currentIndex))
                    return selectedIndex == currentIndex ? "⚫" : ""; // ⁕ ₿ ⏺ Ო ● ☯ ⚫
            }
            return "--"; // Fallback for non-int values
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Optional: Implement if you need two-way binding
            throw new NotImplementedException();
        }
    }
}
