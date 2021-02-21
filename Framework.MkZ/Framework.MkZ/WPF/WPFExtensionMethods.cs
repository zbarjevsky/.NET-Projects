using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;


using MkZ.Tools;
using MkZ.Windows.Win32API;

namespace MkZ.WPF
{

    public static class WPFExtensionMethods
    {
        public static System.Windows.Point GetAbsolutePosition(this Window w)
        {
            if (w.WindowState != WindowState.Maximized)
                return new System.Windows.Point(w.Left, w.Top);

            Int32Rect r;
            bool multimonSupported = User32.GetSystemMetrics(User32.SM_CMONITORS) != 0;
            if (!multimonSupported)
            {
                User32.RECT rc = new User32.RECT();
                User32.SystemParametersInfo(48, 0, ref rc, 0);
                r = new Int32Rect(rc.Left, rc.Top, rc.Width, rc.Height);
            }
            else
            {
                WindowInteropHelper helper = new WindowInteropHelper(w);
                IntPtr hmonitor = User32.MonitorFromWindow(new HandleRef((object)null, helper.EnsureHandle()), 2);
                User32.MONITORINFOEX info = new User32.MONITORINFOEX();
                User32.GetMonitorInfo(new HandleRef((object)null, hmonitor), info);
                r = new Int32Rect(info.rcWork.Left, info.rcWork.Top, info.rcWork.Width, info.rcWork.Height);
            }
            return new System.Windows.Point(r.X, r.Y);
        }

        public static bool SetMousePosition(this UIElement element, System.Windows.Point point)
        {
            System.Windows.Point ptOnScreen = element.PointToScreen(point);
            bool res = User32.SetCursorPos((int)ptOnScreen.X, (int)ptOnScreen.Y);
            Mouse.UpdateCursor();
            return res;
        }

        public static void ForceRender(this UIElement element, 
            Action renderAction = null)
        {
            if (renderAction == null)
                renderAction = () => { };

            element.Dispatcher.Invoke(DispatcherPriority.Render, renderAction);
        }
    }
}
