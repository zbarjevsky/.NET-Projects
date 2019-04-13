using DiskCryptorHelper.Properties;
using Microsoft.Win32;
using sD.WPF.MessageBox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskCryptorHelper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] cmd_line)
        {
            if(SingleInstanceHelper.GlobalShowWindow(FormMain.TITLE))
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new FormMain(cmd_line));
            }
            catch (Exception err)
            {
                Debug.WriteLine(err);
            }
            finally
            {
            }
        }
    }
}
