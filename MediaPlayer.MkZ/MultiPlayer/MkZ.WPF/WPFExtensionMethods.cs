using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

    public class ClickAndDoubleClickHandler
    {
        private System.Windows.Controls.UserControl _owner;

        private static readonly int DoubleClickTime = System.Windows.Forms.SystemInformation.DoubleClickTime;

        private static DispatcherTimer _clickWaitTimer;

        private Action<object, MouseButtonEventArgs> _mouseDoubleClick;
        private Action<object, MouseButtonEventArgs> _mouseClick;

        public ClickAndDoubleClickHandler(System.Windows.Controls.UserControl owner, 
            Action<object, MouseButtonEventArgs> mouseDoubleClick,
            Action<object, MouseButtonEventArgs> mouseClick)
        {
            _owner = owner;
            _owner.MouseDown += _owner_MouseDown;
            _owner.MouseUp += _owner_MouseUp;
            _owner.MouseDoubleClick += _owner_MouseDoubleClick;

            _mouseDoubleClick = mouseDoubleClick;
            _mouseClick = mouseClick;

            _clickWaitTimer = new DispatcherTimer(
                TimeSpan.FromMilliseconds(DoubleClickTime),
                DispatcherPriority.Background,
                mouseWaitTimer_Tick,
                Dispatcher.CurrentDispatcher);
            _clickWaitTimer.Stop();
        }

        private System.Windows.Point _ptMouse;
        private void _owner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _ptMouse = _owner.PointToScreen(Mouse.GetPosition(_owner));
        }

        private bool _isDoubleClick = false;
        private void _owner_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Stop the timer from ticking.
            _clickWaitTimer.Stop();

            Debug.WriteLine("ClickAndDoubleClickHandler::Double Click");
            _isDoubleClick = true;

            _mouseDoubleClick?.Invoke(sender, e);
        }

        private MouseButtonEventArgs _eClick;
        private void _owner_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point ptMouse = _owner.PointToScreen(Mouse.GetPosition(_owner));
            bool isMouseMove = (ptMouse != _ptMouse); //was dragged between 'down' and 'up'

            if (_isDoubleClick || isMouseMove)
            {
                Debug.WriteLine("ClickAndDoubleClickHandler::Mouse UP: dbl clk OR mouse move");
                _isDoubleClick = false;
                return;
            }

            _eClick = e;
            _clickWaitTimer.Start();
        }

        private void mouseWaitTimer_Tick(object sender, EventArgs e)
        {
            _clickWaitTimer.Stop();

            // Handle Single Click Actions
            Trace.WriteLine("ClickAndDoubleClickHandler::Single Click");

            _mouseClick?.Invoke(sender, _eClick);
        }
    }
}
