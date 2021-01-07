using MkZ.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace MkZ.WPF.MessageBox
{
    public static class MessageWindowExtension
    {
        public static PopUp.PopUpResult MessageBox(UIElement owner,
            ref string message, string title,
            MessageBoxImage icon,
            TextAlignment textAlignment,
            PopUp.PopUpButtons buttons,
            int autoCloseTimeoutMs = Timeout.Infinite, //infinite
            bool bReadonly = true)
        {
            Action<MessageWindow> SetOwnerAndCenter = (wnd) =>
            {
                wnd.SetOwner(owner);
                if (owner != null)
                    wnd.CenterToUIElement(owner);
                else
                    wnd.CenterToMainWindow();
            };

            return MessageBoxCreateAndOpen(SetOwnerAndCenter, ref message, title, icon, textAlignment, buttons, autoCloseTimeoutMs, bReadonly);
        }

        public static PopUp.PopUpResult MessageBox(IntPtr ownerHwnd,
            ref string message, string title,
            MessageBoxImage icon,
            TextAlignment textAlignment,
            PopUp.PopUpButtons buttons,
            int autoCloseTimeoutMs = Timeout.Infinite, //infinite
            bool bReadonly = true)
        {
            Action<MessageWindow> SetOwnerAndCenter = (wnd) =>
            {
                wnd.SetOwner(ownerHwnd);
                if (ownerHwnd != IntPtr.Zero)
                    wnd.CenterToWindow(ownerHwnd);
                else
                    wnd.CenterToMainWindow();
            };

            return MessageBoxCreateAndOpen(SetOwnerAndCenter, ref message, title, icon, textAlignment, buttons, autoCloseTimeoutMs, bReadonly);
        }

        private static PopUp.PopUpResult MessageBoxCreateAndOpen(Action<MessageWindow> SetOwnerAndCenter,
            ref string message, string title, MessageBoxImage icon, TextAlignment textAlignment,
            PopUp.PopUpButtons buttons, int autoCloseTimeoutMs = Timeout.Infinite, //infinite
            bool bReadonly = true)
        {
            MessageWindow wnd = new MessageWindow(buttons);

            wnd.WindowStartupLocation = WindowStartupLocation.Manual; // sWindowStartupLocation;
            wnd.ConfigureAppearance(icon);
            wnd.Title = title; //for task bar visible tetx
            wnd.txtMessage.TextAlignment = textAlignment;
            wnd.txtMessage.Text = message;
            wnd.txtMessage.IsReadOnly = bReadonly;
            //wnd.txtMessage.ToolTip = message;
            wnd.txtTitle.Text = title;
            //wnd.txtTitle.ToolTip = title;

            wnd.btn1.Content = buttons.btn1.Text;
            wnd.btn2.Content = buttons.btn2.Text;
            wnd.btn3.Content = buttons.btn3.Text;

            wnd.AdjustSize(message, true);

            SetOwnerAndCenter(wnd);

            //if timeout is set and window is not closed after timeout - close it
            Thread t = wnd.CloseWindowOnTimeout(autoCloseTimeoutMs);

            wnd.ShowDialog();

            //if closed before timeout - abort thread
            if (t != null)
                t.Abort();

            //get text from input box - return value
            message = wnd.txtMessage.Text;

            return wnd._DialogResult;
        }

        private static void SetOwner(this Window window, UIElement owner)
        {
            window.Owner = GetWindowImpl(owner);
        }

        private static void SetOwner(this Window window, IntPtr ownerHwnd)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            helper.Owner = ownerHwnd;
        }

        private static Window GetWindowImpl(UIElement owner)
        {
            if (owner == null)
                return WPF_Helper.GetMainWindow();

            return Window.GetWindow(owner);
        }

        private static void CenterToUIElement(this Window window, UIElement owner)
        {
            Rect r = new Rect(owner.PointToScreen(new Point()), owner.RenderSize);
            window.CenterToRectangle(r);
        }

        private static void CenterToMainWindow(this Window window)
        {
            Rect r = WPF_Helper.GetMainWindowRect();
            window.CenterToRectangle(r);
        }

        private static void CenterToWindow(this Window window, IntPtr hWnd)
        {
            User32.GetWindowRect(hWnd, out User32.RECT r);
            Rect rect = new System.Windows.Rect(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);
            if (r.Left < 0 || r.Top < 0)
            {
                //center screen
                rect = new Rect(
                   new Point(SystemParameters.VirtualScreenLeft, SystemParameters.VirtualScreenTop),
                   new Size(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight));
            }

            window.CenterToRectangle(rect);
        }

        private static void CenterToRectangle(this Window window, Rect rOwner)
        {
            Point location = WPF_Helper.CenterToRectangle(new Size(window.Width, window.Height), rOwner);
            window.Left = location.X;
            window.Top = location.Y;
        }
    }
}
