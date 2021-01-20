using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
                UpdateDependencies();
                UpdateMessageIconType();

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

        private static void UpdateDependencies()
        {
            //string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string fileName = Path.Combine(dir, "MZ.WPF.MessageBox.dll");
            //if (!File.Exists(fileName))
            //    File.WriteAllBytes(fileName, Properties.Resources.MZ_WPF_MessageBox);
        }

        private static void UpdateMessageIconType()
        {
            MkZ.WPF.MessageBox.PopUp.IconType = MkZ.WPF.MessageBox.PopUp.IconStyle.RegularImages;
        }
    }
}
