using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


using MkZ.Tools;

namespace MkZ.Media
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            try
            {
                Log.i("Starting Media Controller...");
                if(Properties.Settings.Default.IsUpgradeNeeded)
                {
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.IsUpgradeNeeded = false;
                    Properties.Settings.Default.Save();
                }

                if (MkZ.Tools.SingleInstanceHelper.GlobalShowWindow(FormMain.TITLE))
                    return; //already running

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            catch (Exception err)
            {
                Log.e("Main Exception in {0}\n{1}", FormMain.TITLE, err.ToString());
                MessageBox.Show(err.Message, FormMain.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.e("Unhandled Exception in {0}\n{1}", FormMain.TITLE, e.ExceptionObject.ToString());
            MessageBox.Show("UnhandledException", FormMain.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Log.e("Thread Exception in {0}\n{1}", FormMain.TITLE, e.Exception.ToString());
            MessageBox.Show(e.Exception.Message, FormMain.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
