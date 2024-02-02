using MkZ.Windows.Win32API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static MkZ.Windows.Win32API.User32;
using static System.Windows.Forms.AxHost;

namespace MkZ.Windows.DwmApi
{
    public class WindowStatePosition
    {
        public User32.RECT Bounds { get; set; }

        public eShowWindowCmd WindowState { get; set; }

        public WindowStylesEx WindowStyle { get; set; }

        public string Title { get; set; }

        public IntPtr hWnd { get; set; }

        public WindowStatePosition(WindowInfo info)
        {
            double width = 1600;
            if (width > SystemParameters.PrimaryScreenWidth)
                width = SystemParameters.PrimaryScreenWidth - 80;

            double height = 900;
            if (height > SystemParameters.PrimaryScreenHeight)
                height = SystemParameters.PrimaryScreenHeight - 80;

            double x = (SystemParameters.PrimaryScreenWidth - width) / 2.0;
            double y = (SystemParameters.PrimaryScreenHeight - height) / 2.0;

            //center screen
            Bounds = new User32.RECT(x, y, width, height);

            CopyFrom(info);
        }

        public static void MoveWindow(IntPtr hWnd, WindowStatePosition pos)
        {
            if (hWnd != IntPtr.Zero)
            {
                User32.ShowWindow(hWnd, pos.WindowState);
                User32.SetForegroundWindow(hWnd);
                User32.MoveWindow(hWnd, pos.Bounds.Left, pos.Bounds.Top, pos.Bounds.Width, pos.Bounds.Height, false);
            }
        }

        public void CopyFrom(WindowInfo info)
        {
            User32.RECT rect = DesktopWindowManager.GetWindowRectangle(info.hWnd);
            Bounds = info.Bounds;

            User32.WINDOWINFO info1 = User32.GetWindowInfo(info.hWnd);
            User32.GetWindowPlacement(info.hWnd, out WINDOWPLACEMENT pos);
            
            WindowState = (eShowWindowCmd)pos.showCmd;
            WindowStyle = info.StyleEx;

            hWnd = info.hWnd;
            Title = info.Title;
        }

        public void CopyFrom(WindowStatePosition state)
        {
            Bounds = state.Bounds;

            WindowState = state.WindowState;
            WindowStyle = state.WindowStyle;
        }

        public override string ToString()
        {
            return string.Format("WindowStatePosition: {0}, Bounds: {1}", WindowState, Bounds);
        }
    }
}
