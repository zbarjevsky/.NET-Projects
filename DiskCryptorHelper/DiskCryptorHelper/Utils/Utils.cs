using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskCryptorHelper.Utils
{
    public static class CommonUtils
    {
        public static void ExecuteOnUIThread(Action action, Form owner)
        {
            owner.BeginInvoke(action);
        }

        public static T ExecuteOnUIThread<T>(Func<T> action, Form owner)
        {

            Form topmost = owner;
            if (topmost == null)
                topmost = TopmostForm();

            if (topmost == null || !topmost.InvokeRequired)
            {
                return action.Invoke();
            }

            return (T)topmost.Invoke(action);
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

        public static string Description<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            if (fi != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0) return attributes[0].Description;
                else return source.ToString();
            }
            else return source.ToString();
        }

        [DllImport("User32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("User32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0x00;
        private const int SW_SHOW = 0x05;

        private const int WS_EX_APPWINDOW = 0x40000;
        private const int GWL_EXSTYLE = -0x14;

        public static void ShowWindowInTaskbar(IntPtr pMainWindow)
        {
            SetWindowLong(pMainWindow, GWL_EXSTYLE, GetWindowLong(pMainWindow, GWL_EXSTYLE) | WS_EX_APPWINDOW);

            ShowWindow(pMainWindow, SW_HIDE);
            ShowWindow(pMainWindow, SW_SHOW);
        }

        public static void HideWindowFromTaskbar(IntPtr pMainWindow)
        {
            SetWindowLong(pMainWindow, GWL_EXSTYLE, GetWindowLong(pMainWindow, GWL_EXSTYLE) & ~WS_EX_APPWINDOW);

            ShowWindow(pMainWindow, SW_HIDE);
            ShowWindow(pMainWindow, SW_SHOW);
        }
    }
}
