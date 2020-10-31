using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;


using MZ.Tools;

namespace MZ.WPF.Utils
{
    public class WpfScreen
    {
        public static List<WpfScreen> AllScreens()
        {
            List<WpfScreen> screens = new List<WpfScreen>();
            for (int i = 0; i < System.Windows.Forms.Screen.AllScreens.Length; i++)
            {
                System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.AllScreens[i];
                screens.Add(new WpfScreen(i, screen));
            }
            return screens;
        }

        public static WpfScreen GetScreenFrom(Window window)
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(window);
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.FromHandle(windowInteropHelper.Handle);
            WpfScreen wpfScreen = new WpfScreen(-1, screen);
            return wpfScreen;
        }

        public static WpfScreen GetScreenFrom(int x, int y)
        {
            //int x = (int)Math.Round(X);
            //int y = (int)Math.Round(Y);

            // are x,y device-independent-pixels ??
            System.Drawing.Point drawingPoint = new System.Drawing.Point(x, y);
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.FromPoint(drawingPoint);
            WpfScreen wpfScreen = new WpfScreen(-1, screen);

            return wpfScreen;
        }

        public static int ScreenIndexFromPoint(double x, double y)
        {
            return ScreenIndexFromPoint((int)Math.Round(x), (int)Math.Round(y));
        }

        public static int ScreenIndexFromPoint(int x, int y)
        {
            List<WpfScreen> screens = AllScreens();
            for (int i = 0; i < screens.Count; i++)
            {
                if (screens[i].DeviceBounds.Contains(x, y))
                    return i;
            };
            return -1;
        }

        public static WpfScreen Primary
        {
            get { return new WpfScreen(-1, System.Windows.Forms.Screen.PrimaryScreen); }
        }

        private readonly System.Windows.Forms.Screen screen;
        public int Index { get; }

        public WpfScreen(int idx, System.Windows.Forms.Screen screen)
        {
            Index = idx;
            this.screen = screen;
        }

        public Rect DeviceBounds
        {
            get { return this.GetRect(this.screen.Bounds); }
        }

        public Rect WorkingArea
        {
            get { return this.GetRect(this.screen.WorkingArea); }
        }

        private Rect GetRect(System.Drawing.Rectangle value)
        {
            // should x, y, width, height be device-independent-pixels ??
            return new Rect
            {
                X = value.X,
                Y = value.Y,
                Width = value.Width,
                Height = value.Height
            };
        }

        public bool IsPrimary
        {
            get { return this.screen.Primary; }
        }

        public string DeviceName
        {
            get { return this.screen.DeviceName; }
        }

        public override string ToString()
        {
            return DeviceBounds + (IsPrimary ? " (Primary)" : "");
        }
    }
}
