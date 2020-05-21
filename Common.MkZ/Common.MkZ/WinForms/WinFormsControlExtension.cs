using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Tools.WinForms
{
    public static class ControlExtension
    {
        public static void SetDoubleBuffered(this Control c, bool value)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, value, null);
        }

        public static void ExpandGridItem(this PropertyGrid propertyGrid, string name)
        {
            GridItem root = propertyGrid.SelectedGridItem;
            //Get the parent
            while (root.Parent != null)
                root = root.Parent;

            ExpandGridItem(root, name);
        }

        private static void ExpandGridItem(GridItem root, string name)
        {
            if (root != null)
            {
                foreach (GridItem g in root.GridItems)
                {
                    ExpandGridItem(g, name);
                    if (g.Label == name)
                        g.Expanded = true;
                }
            }
        }

        public static void ShowOnDisabled(this ToolTip control, bool bEnable)
        {

        }

        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions)
        {
            if (extensions == null)
                throw new ArgumentNullException("extensions");
            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            return files.Where(f => extensions.Contains(f.Extension));
        }

        //ensure child is visible in the container using parent coordinates
        //where child.Parent == parent and parent.Parent == container
        public static void EnsureVisibleChild(this Control container, Control innerCtrl, Control innerCtrlChild, AnchorStyles ancors, int margin = 20, bool bAlways = false)
        {
            //visible bounds in innerCtrl coordinates
            int left = -innerCtrl.Left;
            int top = -innerCtrl.Top;
            int right = container.Width - innerCtrl.Left;
            int bottom = container.Height - innerCtrl.Top;

            if (bAlways || innerCtrlChild.Left < left || innerCtrlChild.Top < top || innerCtrlChild.Right > right || innerCtrlChild.Bottom > bottom)
            {
                if (ancors == AnchorStyles.None) //center child if out of view
                {
                    innerCtrlChild.Left = left + (right - left - innerCtrlChild.Width) / 2;
                    innerCtrlChild.Top = top + (bottom - top - innerCtrlChild.Height) / 2;
                }
                if (ancors.HasFlag(AnchorStyles.Left))
                {
                    innerCtrlChild.Left = left + margin;
                }
                if (ancors.HasFlag(AnchorStyles.Right))
                {
                    innerCtrlChild.Left = right - innerCtrlChild.Width - margin;
                }
                if (ancors.HasFlag(AnchorStyles.Top))
                {
                    innerCtrlChild.Top = top + margin;
                }
                if (ancors.HasFlag(AnchorStyles.Bottom))
                {
                    innerCtrlChild.Top = bottom - innerCtrlChild.Height - margin;
                }
            }
        }
    }
}
