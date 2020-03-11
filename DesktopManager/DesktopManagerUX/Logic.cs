using DesktopManagerUX.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace DesktopManagerUX
{
    public class Logic
    {
        public static List<AppInfo> GetProcessesWithUI()
        {
            Process[] processes = Process.GetProcesses();
            List<Process> pp = Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).ToList();

            List<AppInfo> apps = new List<AppInfo>();
            apps.Add(AppInfo.GetEmptyAppInfo());

            foreach (Process p in pp)
                apps.Add(new AppInfo(p));

            return apps;
        }

        public static List<DisplayInfo> GetDisplays()
        {
            List<DisplayInfo> list = new List<DisplayInfo>();
            List<WpfScreen> screens = WpfScreen.AllScreens();
            for (int i=0; i<screens.Count; i++)
            {
                list.Add(new DisplayInfo(screens[i], i));
            }
            return list;
        }

        public static void MoveWindow(Process p, double X, double Y, double nWidth, double nHeight)
        {
            if (p != null && !p.HasExited)
            {
                User32.ShowWindow(p.MainWindowHandle, User32.SWP_SHOWWINDOW);
                User32.SetForegroundWindow(p.MainWindowHandle);
                User32.MoveWindow(p.MainWindowHandle, (int)X, (int)Y, (int)nWidth, (int)nHeight, false);
            }
        }

        public static string SettingGet(int row, int col)
        {
            if (row == 0 && col == 0)
                return Properties.Settings.Default.AppTitle0x0;
            if (row == 0 && col == 1)
                return Properties.Settings.Default.AppTitle0x1;
            if (row == 0 && col == 2)
                return Properties.Settings.Default.AppTitle0x2;
            if (row == 1 && col == 0)
                return Properties.Settings.Default.AppTitle1x0;
            if (row == 1 && col == 1)
                return Properties.Settings.Default.AppTitle1x1;
            if (row == 1 && col == 2)
                return Properties.Settings.Default.AppTitle1x2;
            return null;
        }

        public static void SettingSave(string appTitle, int row, int col)
        {
            if (row == 0 && col == 0)
                Properties.Settings.Default.AppTitle0x0 = appTitle;
            if (row == 0 && col == 1)
                Properties.Settings.Default.AppTitle0x1 = appTitle;
            if (row == 0 && col == 2)
                Properties.Settings.Default.AppTitle0x2 = appTitle;
            if (row == 1 && col == 0)
                Properties.Settings.Default.AppTitle1x0 = appTitle;
            if (row == 1 && col == 1)
                Properties.Settings.Default.AppTitle1x1 = appTitle;
            if (row == 1 && col == 2)
                Properties.Settings.Default.AppTitle1x2 = appTitle;

            Properties.Settings.Default.Save();
        }

        public static BitmapSource CaptureApplication(Process process, System.Windows.Point DPI)
        {
            User32.RestoreWindow(process.MainWindowHandle); //to get snapshot

            User32.PROCESS_DPI_AWARENESS pda = User32.GetProcessDpiAwareness(process.Handle);

            User32.WINDOWINFO wi = User32.GetWindowInfo(process.MainWindowHandle);
            int width = (int)((wi.rcWindow.right - wi.rcWindow.left));// / DPI.X);
            int height = (int)((wi.rcWindow.bottom - wi.rcWindow.top));// / DPI.Y);

            using (Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.Clear(Color.Wheat);

                    IntPtr hdc = graphics.GetHdc();
                    if (!User32.PrintWindow(process.MainWindowHandle, hdc, User32.PW_RENDERFULLCONTENT))// User32.PW_CLIENTONLY))
                    {
                        int error = Marshal.GetLastWin32Error();
                        var exception = new System.ComponentModel.Win32Exception(error);
                        Debug.WriteLine("ERROR: " + error + ": " + exception.Message);
                    }

                    graphics.ReleaseHdc(hdc);
                    graphics.Dispose();
                }

                //bmp.Save("c:\\temp\\test.png", ImageFormat.Png);
                BitmapSource b = GetImageSourceFromBitmap(bmp);
                return b;
            }
        }

        public static BitmapSource GetImageSourceFromIcon(Icon ico)
        {
            using (Bitmap bmp = ico.ToBitmap())
            {
                return GetImageSourceFromBitmap(bmp);
            }
        }

        public static BitmapSource GetImageSourceFromBitmap(Bitmap bmp)
        {
            BitmapSource src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                  bmp.GetHbitmap(),
                                  IntPtr.Zero,
                                  System.Windows.Int32Rect.Empty,
                                  BitmapSizeOptions.FromWidthAndHeight(bmp.Width, bmp.Height));
            return src;
        }
    }
}
