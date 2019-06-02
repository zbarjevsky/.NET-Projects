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

namespace MZ.WPF.MessageBox
{
    public static class WPF_Helper
    {
        private static Window _window;
        private static readonly Application _app;
        private static readonly string _assemblyName = Assembly.GetExecutingAssembly().GetName().Name; //"sD.WPF.MessageBox";

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
            System.Windows.Forms.Form topmostForm = TopmostForm();
            if (topmostForm != null) //Call is from WinForm application
            {
                //create WPF window to use as Owner
                if (_window == null)
                {
                    _window = new Window();
                    WindowInteropHelper helper = new WindowInteropHelper(_window);
                    helper.Owner = topmostForm.Handle;

                    _window.Closed += _window_Closed;

                    _window.ShowInTaskbar = false;
                    _window.WindowState = WindowState.Normal;

                    _window.WindowStyle = WindowStyle.None;
                    _window.AllowsTransparency = true;
                    _window.Background = Brushes.Transparent;

                    //out of screen
                    _window.Left = -100;
                    _window.Top = -100;    
                    _window.Width = 10;
                    _window.Height = 10;
                }

                _window.Show();

                return _window;
            }

            if (_app != null && _app.MainWindow != null && _app.MainWindow.ActualWidth > 10)
            {
                return _app.MainWindow;
            }

            return null;
        }

        private static void _window_Closed(object sender, EventArgs e)
        {
            _window.Closed -= _window_Closed;
            _window = null;
        }

        public static Point CenterToRectangle(Window window, Rect r)
        {
            double left = r.Left + (r.Width - window.Width) / 2;
            double top = r.Top + (r.Height - window.Height) / 2;

            //check bounds - stay within desktop
            System.Drawing.Rectangle bounds = System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point((int)r.Left, (int)r.Top)).Bounds;
            if (left + window.Width > bounds.Left + bounds.Width)
                left = bounds.Left + bounds.Width - window.Width;

            if (top + window.Height > bounds.Top + bounds.Height)
                top = bounds.Top + bounds.Height - window.Height;

            if (left < bounds.Left) left = bounds.Left;
            if (top < bounds.Top) top = bounds.Top;

            Point location = new Point(left, top);
            return location;
        }

        public static Rect GetMainWindowRect()
        {
            System.Windows.Forms.Form topmost = TopmostForm();
            if (topmost != null)
            {
                return new Rect(topmost.Location.X, topmost.Location.Y, topmost.Width, topmost.Height);
            }
            else if (_app != null && _app.MainWindow != null && _app.MainWindow.ActualWidth > 10)
            {
                Window main = _app.MainWindow;
                return new Rect(main.PointToScreen(new Point()), new Size(main.Width, main.Height));
            }

            //no main window - use current screen
            System.Drawing.Rectangle bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            return new Rect(bounds.Left, bounds.Top, bounds.Width, bounds.Height);
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

        public static ImageSource GetResourceImage(string imageFileName)
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
                    + ";component/Images/" 
                    + imageFileName, UriKind.Absolute);

                return new BitmapImage(oUri);
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show("Cannot load image: " + imageFileName + "\nError: " + err.Message,
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
