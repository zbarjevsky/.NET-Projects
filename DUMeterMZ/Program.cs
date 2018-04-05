using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
                Application.Run(new FormLoadGraph());
            }//end try
            catch (Exception err)
            {
                FormLoadGraph.MessageBox("Main error: " + err.Message);
                Environment.Exit(-1);
            }//end catch
        }//end Main
    }
}
