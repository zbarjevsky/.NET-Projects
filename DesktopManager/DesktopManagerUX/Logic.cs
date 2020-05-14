using DesktopManagerUX.Utils;
using MZ.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ObservableCollection<AppInfo> GetAppsWithUI()
        {
            List<AppInfo> apps = new List<AppInfo>();
            apps.AddRange(EnumOpenWindows.GetOpenWindows().Select(w => new AppInfo(w)));
            apps.Sort((a1, a2) => string.Compare(a1.ProcessName, a2.ProcessName, true));
            apps.Insert(0, AppInfo.GetEmptyAppInfo());

            //Process[] processes = Process.GetProcesses();
            //List<Process> pp = processes.Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).ToList();
            //pp = processes.Where(p => p.MainWindowHandle != IntPtr.Zero).ToList();

            //foreach (Process p in pp)
            //    apps.Add(new AppInfo(p));

            return new ObservableCollection<AppInfo>(apps);
        }

        public static List<Process> FindProcess(string processName)
        {
            return FindProcess(p => p.ProcessName == processName);
        }

        public static List<Process> FindProcess(Func<Process, bool> predicate)
        {
            Process[] processes = Process.GetProcesses();
            return processes.Where(predicate).ToList();
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

        public static void MoveWindow(IntPtr hWnd, double X, double Y, double nWidth, double nHeight)
        {
            if (hWnd != IntPtr.Zero)
            {
                User32.ShowWindow(hWnd, User32.SWP_SHOWWINDOW);
                User32.SetForegroundWindow(hWnd);
                User32.MoveWindow(hWnd, (int)X, (int)Y, (int)nWidth, (int)nHeight, false);
            }
        }

        //public static string SettingGet(int row, int col)
        //{
        //    if (row == 0 && col == 0)
        //        return Properties.Settings.Default.AppTitle0x0;
        //    if (row == 0 && col == 1)
        //        return Properties.Settings.Default.AppTitle0x1;
        //    if (row == 0 && col == 2)
        //        return Properties.Settings.Default.AppTitle0x2;
        //    if (row == 1 && col == 0)
        //        return Properties.Settings.Default.AppTitle1x0;
        //    if (row == 1 && col == 1)
        //        return Properties.Settings.Default.AppTitle1x1;
        //    if (row == 1 && col == 2)
        //        return Properties.Settings.Default.AppTitle1x2;
        //    return null;
        //}

        //public static void SettingSave(string appTitle, int row, int col)
        //{
        //    if (row == 0 && col == 0)
        //        Properties.Settings.Default.AppTitle0x0 = appTitle;
        //    if (row == 0 && col == 1)
        //        Properties.Settings.Default.AppTitle0x1 = appTitle;
        //    if (row == 0 && col == 2)
        //        Properties.Settings.Default.AppTitle0x2 = appTitle;
        //    if (row == 1 && col == 0)
        //        Properties.Settings.Default.AppTitle1x0 = appTitle;
        //    if (row == 1 && col == 1)
        //        Properties.Settings.Default.AppTitle1x1 = appTitle;
        //    if (row == 1 && col == 2)
        //        Properties.Settings.Default.AppTitle1x2 = appTitle;

        //    Properties.Settings.Default.Save();
        //}

        public static BitmapSource CaptureApplication(IntPtr hWnd, bool bRestoreBeforeSnapshot)
        {
            if(bRestoreBeforeSnapshot)
                User32.RestoreWindow(hWnd); //to get snapshot

            //User32.PROCESS_DPI_AWARENESS pda = User32.GetProcessDpiAwareness(process.Handle);

            User32.WINDOWINFO wi = User32.GetWindowInfo(hWnd);
            int width = (int)((wi.rcWindow.right - wi.rcWindow.left));
            int height = (int)((wi.rcWindow.bottom - wi.rcWindow.top));

            if (width == 0 || height == 0)
                return null;

            using (Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.Clear(Color.Wheat);

                    IntPtr hdc = graphics.GetHdc();
                    if (!User32.PrintWindow(hWnd, hdc, User32.PW_RENDERFULLCONTENT))// User32.PW_CLIENTONLY))
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
            IntPtr hBmp = bmp.GetHbitmap();
            BitmapSource src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                  hBmp,
                                  IntPtr.Zero,
                                  System.Windows.Int32Rect.Empty,
                                  BitmapSizeOptions.FromWidthAndHeight(bmp.Width, bmp.Height));
            Gdi32.DeleteObject(hBmp);
            return src;
        }

        public void RunApp(AppInfo app)
        {
            //app.ProcessPath;
            MessageBox.Show("Not Implemented");
        }
    }
}
