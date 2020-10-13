using MZ.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Media
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (AssemblyTools.UpdateAssemblyInfoVersion(args))
                    return;

                if (MZ.Tools.SingleInstanceHelper.GlobalShowWindow(FormMain.TITLE))
                    return; //already running

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, FormMain.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
