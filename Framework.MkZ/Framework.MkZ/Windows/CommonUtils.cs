using MkZ.WinForms;
using MkZ.WPF.MessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MkZ.Windows
{
    public static class CommonUtils
    {
        public static void BrowseForFolder(this Form owner, ref string folderName, string selectedPath = "", string prompt = "Select Folder")
        {
            selectedPath = string.IsNullOrWhiteSpace(selectedPath) ? folderName : selectedPath;

            try
            {
                FormBrowseForFolder frm = new FormBrowseForFolder();
                frm.SelectedFolder = selectedPath;
                frm.Description = prompt;

                if (frm.ShowDialog(owner) == DialogResult.OK)
                {
                    folderName = frm.SelectedFolder;
                }
            }
            catch (Exception err)
            {
                PopUp.Error(err.Message, prompt);
            }        
        }

        public static void ExecuteOnUIThread(Action action, Control owner)
        {
            if (owner.Visible)
                owner.BeginInvoke(action);
            else
                TopmostForm().BeginInvoke(action);
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

        public static string Description<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            if (fi != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0) return attributes[0].Description;
                else return source.ToString();
            }
            else return source.ToString();
        }

        public static Dictionary<string, FieldData> GetFields<T>(this T source) where T : class
        {
            return GetFields(source.GetType());
        }

        public class FieldData
        {
            public string Name = "";
            public string Desc = "";
            public object Value = null;

            public FieldData(FieldInfo info)
            {
                Name = info.Name;
                if (Attribute.IsDefined(info, typeof(DescriptionAttribute)))
                {
                    Desc = (Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute)) as DescriptionAttribute).Description;
                    Value = info.GetValue(null);
                }
            }

            public override string ToString()
            {
                return string.Format("FieldData - Name: {0} Value: {1} Desc: {2}", Name, Value, Desc);
            }
        }

        public static Dictionary<string, FieldData> GetFields(this Type source)
        {
            List<FieldInfo> properties = source.GetRuntimeFields().ToList();
            return properties.Select(p => new FieldData(p)).ToDictionary(f => f.Name, f => f);

            //PropertyInfo [] properties = source.GetType().GetProperties();
            //return properties.Select(p => new {
            //    FiledName = p.Name,
            //    FieldValue = p.GetValue().ToString(),
            //    Desc = Attribute.IsDefined(p, typeof(DescriptionAttribute)) ? (Attribute.GetCustomAttribute(p, typeof(DescriptionAttribute)) as DescriptionAttribute).Description : p.Name).ToArray();
        }

        public static List<T> EnumToList<T>(T enumType) 
        {
            Array values = Enum.GetValues(typeof(T));
            return values.Cast<T>().ToList();
        }
       
        public static void ErrorMessage(string message, string title = "ERROR")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }
    }
}
