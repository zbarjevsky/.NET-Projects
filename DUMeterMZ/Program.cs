using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DUMeterMZ.Common;

namespace DUMeterMZ
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                bool result;
                using (var mutex = new System.Threading.Mutex(true, Utils.AppName, out result))
                {
                    if (!result)
                    {
                        Utils.MessageBox("Another instance is already running.");
                        return;
                    }

                    Application.Run(new FormLoadGraph());
                }
            }//end try
            catch (Exception err)
            {
                Utils.MessageBox("Main error: " + err.Message);
                Environment.Exit(-1);
            }//end catch
        }//end Main
    }
}
