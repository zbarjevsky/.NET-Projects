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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                UpdateDependencies();
                Application.Run(new FormMain());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "YouTube Downloader UI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }

        private static void UpdateDependencies()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string youtube_dl = Path.Combine(dir, "Dependencies", "youtube-dl.exe");
            if (File.Exists(youtube_dl))
                return; //already extracted

            string fileName = Path.Combine(dir, "Dependencies.sfx.exe");
            if (!File.Exists(fileName))
            {
                File.WriteAllBytes(fileName, Properties.Resources.Dependencies_sfx);
                if (File.Exists(fileName))
                {
                    Process  p = Process.Start(fileName);
                    p.WaitForExit();
                    File.Delete(fileName);
                }
            }
        }
    }
}
