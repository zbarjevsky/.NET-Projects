using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartBackup.Tools
{
    public static class Utils
    {
        public static void Browse(this Form owner, TextBox txt, string prompt = "Select Folder")
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog()
            {
                RootFolder = Environment.SpecialFolder.MyComputer,
                SelectedPath = txt.Text,
                Description = prompt
            };

            //select current folder
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                owner.Invoke(new MethodInvoker(() => SendKeys.Send("{TAB}{TAB}{DOWN}{DOWN}{UP}{UP}")));
            });

            if (dlg.ShowDialog(owner) == DialogResult.OK)
            {
                txt.Text = dlg.SelectedPath;
            }
        }
    }
}
