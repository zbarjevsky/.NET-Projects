using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPUD
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
        Application.Run(new FormMain());
      }//end try
      catch (Exception err)
      {
        MessageBox.Show(err.Message, "Unexpected error", 
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }//end catch
    }
  }
}