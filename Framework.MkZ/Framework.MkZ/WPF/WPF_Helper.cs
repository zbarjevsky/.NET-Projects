using MZ.Tools;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MZ.WPF
{
    public static class WPF_Helper
    {
        private static Window _window;
        private static readonly Application _app;
        private static readonly string _assemblyName = Assembly.GetExecutingAssembly().GetName().Name; //"sD.WPF.MessageBox";

        private static Matrix _TransformToDevice;

        public static double ScaleWPF { get { return _TransformToDevice.M11; } }

        static WPF_Helper()
        {
            //to use in WinForms Application - when no WPF Application
            if (Application.Current == null)
            {
                _app = new Application(); //hidden app to access WPF resources
                _app.ShutdownMode = ShutdownMode.OnExplicitShutdown; 
            }
            else
            {
                _app = Application.Current;
            }
        }

        public static Window GetMainWindow()
        {
            Process.GetCurrentProcess().Refresh();
            IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
            if (mainWindowHandle != IntPtr.Zero) //Call is from WinForm application
            {
                //create WPF window to use as Owner
                _window = WrapWindow(_window, mainWindowHandle);
                _window.Show();

                return _window;
            }

            if (_app != null && _app.MainWindow != null && _app.MainWindow.ActualWidth > 10)
            {
                return _app.MainWindow;
            }

            return null;
        }

        private static Window WrapWindow(Window wnd, IntPtr handle)
        {
            if (wnd != null)
            {
                WindowInteropHelper helperExisting = new WindowInteropHelper(wnd);
                if (helperExisting.Owner == handle)
                    return wnd;

                wnd.Close();
            }

            wnd = new Window();
            WindowInteropHelper helper = new WindowInteropHelper(wnd);
            helper.Owner = handle;

            wnd.Closed += _window_Closed;

            wnd.Title = "Owner" + handle;
            wnd.ShowInTaskbar = false;
            wnd.WindowState = WindowState.Normal;

            wnd.WindowStyle = WindowStyle.None;
            wnd.AllowsTransparency = true;
            wnd.Background = Brushes.Transparent;

            //out of screen
            wnd.Left = -100;
            wnd.Top = -100;
            wnd.Width = 10;
            wnd.Height = 10;

            return wnd;
        }

        private static void _window_Closed(object sender, EventArgs e)
        {
            Window wnd = sender as Window;
            if (wnd != null)
            {
                wnd.Closed -= _window_Closed;
                wnd = null;
            }
        }

        public static Point CenterToRectangle(Size sizeToCenter, Rect rOwner)
        {
            double left = rOwner.Left + (rOwner.Width - sizeToCenter.Width) / 2;
            double top = rOwner.Top + (rOwner.Height - sizeToCenter.Height) / 2;

            //check bounds - stay within desktop
            Rect screenBounds = VirtualScreenBounds();

            if (left + sizeToCenter.Width > screenBounds.Left + screenBounds.Width)
                left = screenBounds.Left + screenBounds.Width - sizeToCenter.Width;

            if (top + sizeToCenter.Height > screenBounds.Top + screenBounds.Height)
                top = screenBounds.Top + screenBounds.Height - sizeToCenter.Height;

            if (left < screenBounds.Left) left = screenBounds.Left;
            if (top < screenBounds.Top) top = screenBounds.Top;

            Point location = new Point(left, top);
            return location;
        }

        public static Rect VirtualScreenBounds()
        {
            //System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point((int)rectToCenter.Left, (int)rectToCenter.Top)).Bounds;
            Rect screenBounds = new Rect(
                SystemParameters.VirtualScreenLeft, SystemParameters.VirtualScreenTop,
                SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight);
            return screenBounds;
        }

        public static Rect GetMainWindowRect()
        {
            System.Windows.Forms.Form topmost = TopmostForm();
            if (topmost != null)
            {
                double scale = WPF_Helper.ScaleWPF;// * WPF_Helper.SystemScale;
                return new Rect(topmost.Location.X / scale, topmost.Location.Y / scale, topmost.Width / scale, topmost.Height / scale);
            }
            else if (_app != null && _app.MainWindow != null && _app.MainWindow.ActualWidth > 10)
            {
                Window main = _app.MainWindow;
                return new Rect(main.Left, main.Top, main.ActualWidth, main.ActualHeight);
            }

            //no main window - use current screen
            return GetMainWindowRectFromHandle();
        }

        public static Rect GetMainWindowRectFromHandle()
        {
            Process p = Process.GetCurrentProcess();
            p.Refresh();
            if (p.MainWindowHandle != IntPtr.Zero)
            {
                User32.RECT r;
                User32.GetWindowRect(p.MainWindowHandle, out r);
                return new System.Windows.Rect(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);
            }
            else
            {
                return new Rect(
                    new Point(SystemParameters.VirtualScreenLeft, SystemParameters.VirtualScreenTop),
                    new Size(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight));
            }
        }

        //get scale from visual and store it in static variable
        public static void UpdateScaleWPF(UIElement element)
        {
            var source = PresentationSource.FromVisual(element);
            if (source != null)
                _TransformToDevice = source.CompositionTarget.TransformToDevice;
            else
                using (var source1 = new HwndSource(new HwndSourceParameters()))
                    _TransformToDevice = source1.CompositionTarget.TransformToDevice;
        }

        public static double SystemScale
        {
            get { return SystemParameters.VirtualScreenHeight / SystemParameters.PrimaryScreenHeight; }
        }

        public static T ExecuteOnUIThread<T>(Func<T> action)
        {
            System.Windows.Forms.Form topmostForm = TopmostForm();
            if (topmostForm != null) //Call is from WinForm application
            {
                return ExecuteOnUIThreadForm(action);
            }
            else //Call from WPF app
            {
                return ExecuteOnUIThreadWPF(action);
            }
        }

        public static T ExecuteOnUIThreadWPF<T>(Func<T> action)
        {
            if (!_app.Dispatcher.CheckAccess())
            {
                return _app.Dispatcher.Invoke(() =>
                {
                    return action.Invoke();
                });
            }
            else
            {
                return action.Invoke();
            }
        }

        public static System.Windows.Forms.Form TopmostForm()
        {
            int formsCount = System.Windows.Forms.Application.OpenForms.Count;
            if (formsCount > 0)
            {
                //find first visible Form
                for (int i = formsCount - 1; i >= 0; i--)
                {
                    if (System.Windows.Forms.Application.OpenForms[i].Visible)
                        return System.Windows.Forms.Application.OpenForms[i];
                }
            }

            return null;
        }

        public static T ExecuteOnUIThreadForm<T>(Func<T> action)
        {
            System.Windows.Forms.Form topmost = TopmostForm();
            if (topmost == null || !topmost.InvokeRequired)
            {
                return action.Invoke();
            }

            return (T)topmost.Invoke(action);
        }

        public static ImageSource GetResourceImage(string imageResourcePath)
        {
            try
            {
                //string pack = System.IO.Packaging.PackUriHelper.UriSchemePack;
                //Uri u = _app.StartupUri;
                //if (!UriParser.IsKnownScheme("pack"))
                //    new System.Windows.Application();
                //UriParser.Register(
                //    new GenericUriParser(GenericUriParserOptions.GenericAuthority), "pack", -1
                //);

                Uri oUri = new Uri("pack://application:,,,/"
                    + _assemblyName
                    + ";component/" 
                    + imageResourcePath, UriKind.Absolute);

                return new BitmapImage(oUri);
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show("Cannot load image: " + imageResourcePath + "\nError: " + err.Message,
                    "MessageWindow.GetResourceImage()");
                return null;
            }
        }

        /// <summary>
        /// Get Localized message box button text from User32.dll
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        //public static string GetMessageBoxButtonText(PopUp.PopUpResult btn)
        //{
        //    string btnText = "?";
        //    switch (btn)
        //    {
        //        case PopUp.PopUpResult.OK:
        //            btnText = WIN32.MB_GetString(WIN32.SystemLocStrings.OK);
        //            break;
        //        case PopUp.PopUpResult.Cancel:
        //            btnText = WIN32.MB_GetString(WIN32.SystemLocStrings.Cancel);
        //            break;
        //        //case PopUp.PopUpResult.Btn3:
        //        //    btnText = WIN32.MB_GetString(WIN32.SystemLocStrings.Yes);
        //        //    break;
        //        case PopUp.PopUpResult.No:
        //            btnText = WIN32.MB_GetString(WIN32.SystemLocStrings.No);
        //            break;
        //        case PopUp.PopUpResult.None:
        //        default:
        //            break;
        //    }
        //    return btnText.TryAddKeyboardAccellerator();
        //}

        public static ImageSource ToImageSource(this System.Drawing.Icon icon)
        {
            //System.Drawing.Bitmap bitmap = icon.ToBitmap();
            //IntPtr hBitmap = bitmap.GetHbitmap();
            //ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            //if (!WIN32.DeleteObject(hBitmap))
            //{
            //    throw new Win32Exception();
            //}
            //return wpfBitmap;
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                   icon.Handle,
                   Int32Rect.Empty,
                   BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }

        /// <summary>
        /// Keyboard Accellerators are used in Windows to allow easy shortcuts to controls like Buttons and 
        /// MenuItems. These allow users to press the Alt key, and a shortcut key will be highlighted on the 
        /// control. If the user presses that key, that control will be activated.
        /// This method checks a string if it contains a keyboard accellerator. If it doesn't, it adds one to the
        /// beginning of the string. If there are two strings with the same accellerator, Windows handles it.
        /// The keyboard accellerator character for WPF is underscore (_). It will not be visible.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static string TryAddKeyboardAccellerator(this string input)
        {
            const string accellerator = "_";  //This is the default WPF accellerator symbol - used to be & in WinForms

            // If it already contains an accellerator, do nothing
            if (input.Contains(accellerator))
                return input;

            return accellerator + input;
        }

        internal static string TryRemoveKeyboardAccellerator(this string input)
        {
            const string accellerator = "_";  //This is the default WPF accellerator symbol - used to be & in WinForms

            if (string.IsNullOrWhiteSpace(input))
                return input;

            // If it already contains an accellerator, do nothing
            if (!input.Contains(accellerator))
                return input;

            return input.Replace(accellerator, "");
        }
    }
    internal static class WIN32
    {
        private static IntPtr _user32 = IntPtr.Zero;

        public enum SystemLocStrings
        {
            OK = 800,
            Cancel = 801,
            Abort = 802,
            Retry = 803,
            Ignore = 804,
            Yes = 805,
            No = 806,
            Close = 807,
            Help = 808,
            TryAgain = 809,
            Continue = 810
        }

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int LoadString(IntPtr hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

        [DllImport("kernel32")]
        static extern IntPtr LoadLibrary(string lpFileName);

        public static string MB_GetString(SystemLocStrings btnId)
        {
            StringBuilder sb = new StringBuilder(256);

            if(_user32 == IntPtr.Zero)
                _user32 = LoadLibrary(Path.Combine(Environment.SystemDirectory, "User32.dll"));

            int length = LoadString(_user32, (uint)btnId, sb, sb.Capacity);
            if (length == 0 || sb.Length == 0)
                return btnId.ToString();

            return sb.ToString().Replace('&', '_');
        }
    }
}
