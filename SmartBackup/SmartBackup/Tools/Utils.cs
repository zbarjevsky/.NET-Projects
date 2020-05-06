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
        public static void BrowseForFolder(this Form owner, TextBox txt, string prompt = "Select Folder")
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

        public static void ExecuteOnUIThread(Action action, Form owner)
        {
            owner.BeginInvoke(action);
        }

        public static T ExecuteOnUIThread<T>(Func<T> action, Form owner)
        {

            Form topmost = owner;
            if (topmost == null)
                topmost = TopmostForm();

            if (topmost == null || !topmost.InvokeRequired)
            {
                return action.Invoke();
            }

            return (T)topmost.Invoke(action);
        }

        public static System.Windows.Forms.Form TopmostForm()
        {
            int formsCount = System.Windows.Forms.Application.OpenForms.Count;
            if (formsCount > 0)
            {
                //find first visible Form
                for (int i = formsCount - 1; i >= 0; i--)
                {
                    if (System.Windows.Forms.Application.OpenForms[i].Visible)
                        return System.Windows.Forms.Application.OpenForms[i];
                }
            }

            return null;
        }

    }
}
