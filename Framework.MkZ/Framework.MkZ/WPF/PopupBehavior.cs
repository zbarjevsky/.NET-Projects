using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace MkZ.WPF
{
    //https://stackoverflow.com/questions/24570144/allow-mouse-events-to-pass-through-semitransparent-popup
    public static class PopupBehavior
    {
        public static readonly DependencyProperty IsPopupEventTransparentProperty =
            DependencyProperty.RegisterAttached("IsPopupEventTransparent",
                                                typeof(bool),
                                                typeof(PopupBehavior),
                                                new UIPropertyMetadata(false, OnIsPopupEventTransparentPropertyChanged));

        public static bool GetIsPopupEventTransparent(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPopupEventTransparentProperty);
        }
        
        public static void SetIsPopupEventTransparent(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPopupEventTransparentProperty, value);
        }

        private static void OnIsPopupEventTransparentPropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = target as FrameworkElement;
            if ((bool)e.NewValue == true)
            {
                FrameworkElement topParent = VisualTreeHelpers.GetTopParent(element) as FrameworkElement;
                topParent.Opacity = 0.4;
                HwndSource popupHwndSource = HwndSource.FromVisual(element as Window) as HwndSource;
                WindowHelper.SetWindowExTransparent(popupHwndSource.Handle);
            }
            else
            {
                FrameworkElement topParent = VisualTreeHelpers.GetTopParent(element) as FrameworkElement;
                topParent.Opacity = 1.0;
                HwndSource popupHwndSource = HwndSource.FromVisual(element) as HwndSource;
                WindowHelper.UndoWindowExTransparent(popupHwndSource.Handle, element);
            }
        }
    }

    public class VisualTreeHelpers
    {
        /// <summary>
        /// Returns the first ancester of specified type
        /// </summary>
        public static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            current = VisualTreeHelper.GetParent(current);

            while (current != null)
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            };
            return null;
        }

        public static DependencyObject GetTopParent(DependencyObject element)
        {
            DependencyObject current = element;
            while (current != null)
            {
                var parent = VisualTreeHelper.GetParent(current);
                if (parent == null)
                    return current;
                current = parent;
            };
            return null;
        }
    }

        public static class WindowHelper
    {
        private static Dictionary<IntPtr, int> _extendedStyleHwndDictionary = new Dictionary<IntPtr, int>();

        const int WS_EX_TRANSPARENT = 0x00000020;
        const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public static void SetWindowExTransparent(IntPtr hwnd)
        {
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            if (_extendedStyleHwndDictionary.Keys.Contains(hwnd) == false)
            {
                _extendedStyleHwndDictionary.Add(hwnd, extendedStyle);
            }
        }

        public static void UndoWindowExTransparent(IntPtr hwnd, FrameworkElement elementInPopup)
        {
            if (_extendedStyleHwndDictionary.Keys.Contains(hwnd) == true)
            {
                int extendedStyle = _extendedStyleHwndDictionary[hwnd];
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle);
                // Fix Focus problems that forces the users to click twice to
                // re-activate the window after the Popup loses event transparency
                elementInPopup.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Keyboard.Focus(elementInPopup);
                    Mouse.Capture(elementInPopup);
                    elementInPopup.ReleaseMouseCapture();
                }));
            }
        }
    }
}
