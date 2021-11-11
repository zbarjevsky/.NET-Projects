using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MkZ.Tools
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MainWindowState
    {
        public System.Windows.Rect Bounds { get; set; }

        public WindowState WindowState { get; set; } = WindowState.Normal;

        public WindowStyle WindowStyle { get; set; } = WindowStyle.ThreeDBorderWindow;

        public MainWindowState()
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
            Bounds = new System.Windows.Rect(x, y, width, height);
        }

        public void CopyFrom(Window wnd)
        {
            bool bFullScreen = wnd.WindowStyle == WindowStyle.None;

            if (!wnd.RestoreBounds.IsEmpty)
                Bounds = wnd.RestoreBounds;
            else if (wnd.WindowState == WindowState.Normal)
                Bounds = new Rect(wnd.Left, wnd.Top, wnd.ActualWidth, wnd.ActualHeight);

            WindowStyle = wnd.WindowStyle;
            if (!bFullScreen)
            {
                WindowState = wnd.WindowState;
            }
        }

        public void CopyFrom(MainWindowState state)
        {
            Bounds = state.Bounds;

            WindowState = state.WindowState;
            WindowStyle = state.WindowStyle;
        }

        public void RestoreTo(Window wnd)
        {
            Rect virtScreen = new Rect(
                SystemParameters.VirtualScreenLeft,
                SystemParameters.VirtualScreenTop,
                SystemParameters.VirtualScreenWidth,
                SystemParameters.VirtualScreenHeight);

            if (virtScreen.Contains(Bounds))
            {
                wnd.WindowStartupLocation = WindowStartupLocation.Manual;

                wnd.Left = Bounds.Left;
                wnd.Top = Bounds.Top;
                wnd.Width = Bounds.Width;
                wnd.Height = Bounds.Height;
            }
            else
            {
                wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            wnd.WindowState = WindowState;
            wnd.WindowStyle = WindowStyle;
        }

        public override string ToString()
        {
            return string.Format("MainWindowState: {0}, Bounds: {1}", WindowState, Bounds);
        }
    }
}
