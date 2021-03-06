using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MkZ.Framework.Tools
{
    class Utils
    {
        public static void ErrorMessage(string message, string title = "ERROR")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }
    }
}
