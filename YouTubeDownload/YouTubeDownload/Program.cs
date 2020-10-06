using MZ.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeDownload
{
    static class Program
    {
        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((x, y) =>
            {
                var exception = y.ExceptionObject as Exception;

                if (exception is System.IO.FileNotFoundException)
                    Console.WriteLine("Please make sure the DLL is in the same folder.");
                Console.WriteLine(exception);
            });
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (YouTubeDownload.Utils.Utils.UpdateAssemblyInfoVersion(args))
                return;

            UpdateDependencies();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                Application.Run(new FormMain());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "YouTube Downloader UI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }

        private static void UpdateDependencies()
        {
            //DependenciesTool.LoadFrameworkMkZ_Dependency(Properties.Resources.Framework_MkZ);
            DependenciesTool.UpdateDependenciesSfx("", "youtube-dl.exe", Properties.Resources.Dependencies_sfx);
        }
    }
}
