using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using MkZ.Framework.Tools;

namespace MkZ.Tools
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (AssemblyTools.UpdateAssemblyInfoVersion(args))
				return; //version updated

            if (!SingleInstance())
                return; //already running

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			try
			{
				Form frm = new FormMainTest();
				Application.Run(frm);
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message, "Framework");
			}
		}

        private static bool SingleInstance()
        {
            return !(MkZ.Tools.SingleInstanceHelper.GlobalShowWindow(FormMainTest.TITLE));
        }//end SingleInstance
    }
}
