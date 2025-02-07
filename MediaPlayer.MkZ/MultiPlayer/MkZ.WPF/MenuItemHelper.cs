using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MultiPlayer.MkZ.WPF
{
    public static class MenuItemHelper
    {
        public static readonly DependencyProperty Parameter1Property =
           DependencyProperty.RegisterAttached("Parameter1", typeof(object), typeof(MenuItemHelper), new PropertyMetadata(null));

        public static void SetParameter1(DependencyObject element, object value)
        {
            element.SetValue(Parameter1Property, value);
        }

        public static object GetParameter1(DependencyObject element)
        {
            return element.GetValue(Parameter1Property);
        }

        public static readonly DependencyProperty Parameter2Property =
           DependencyProperty.RegisterAttached("Parameter2", typeof(object), typeof(MenuItemHelper), new PropertyMetadata(null));

        public static void SetParameter2(DependencyObject element, object value)
        {
            element.SetValue(Parameter2Property, value);
        }

        public static object GetParameter2(DependencyObject element)
        {
            return element.GetValue(Parameter2Property);
        }
    }
}
