using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DUMeterMZ.Common
{
    public static class Utils
    {
        public const string AppName = "DUMeterMZ";

        public static void MessageBox(string message)
        {
            System.Windows.Forms.MessageBox.Show(message, AppName,
                MessageBoxButtons.OK, MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.ServiceNotification);
        }
    }
}
