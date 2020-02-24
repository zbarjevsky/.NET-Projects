using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeditationStopWatch.Tools
{
    public static class ControlExtension
    {
        // TKey is control to drag, TValue is a flag used while dragging
        private static Dictionary<Control, bool> _draggables = new Dictionary<Control, bool>();
        private static System.Drawing.Size _dragStartLocation;

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

        private static Size _dragOffset = new Size();
        public static bool WasDragging(this Control control)
        {
            if (!_draggables.ContainsKey(control))
                return false;
            return (Math.Abs(_dragOffset.Width) > 0 || Math.Abs(_dragOffset.Height) > 0);
        }

        /// <summary>
        /// Enabling/disabling dragging for control
        /// </summary>
        public static void Draggable(this Control control, bool Enable)
        {
            if (Enable)
            {
                // enable drag feature
                if (_draggables.ContainsKey(control))
                {   // return if control is already draggable
                    return;
                }
                // 'false' - initial state is 'not dragging'
                _draggables.Add(control, false);

                // assign required event handlersnnn
                control.MouseDown += new MouseEventHandler(control_MouseDown);
                control.MouseUp += new MouseEventHandler(control_MouseUp);
                control.MouseMove += new MouseEventHandler(control_MouseMove);
            }
            else
            {
                // disable drag feature
                if (!_draggables.ContainsKey(control))
                {  // return if control is not draggable
                    return;
                }
                // remove event handlers
                control.MouseDown -= control_MouseDown;
                control.MouseUp -= control_MouseUp;
                control.MouseMove -= control_MouseMove;
                _draggables.Remove(control);
            }
        }
        static void control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragOffset = new Size();
                _dragStartLocation = new Size(e.Location);
                
                // turning on dragging
                _draggables[(Control)sender] = true;
            }
        }
        static void control_MouseUp(object sender, MouseEventArgs e)
        {
            // turning off dragging
            _draggables[(Control)sender] = false;
        }

        private static void control_MouseMove(object sender, MouseEventArgs e)
        {
            Control ctrl = (Control)sender;

            // only if dragging is turned on
            if (!_draggables[ctrl])
                return;

            //accumulated move
            _dragOffset += new Size(e.Location - _dragStartLocation);

            // calculations of control's new position
            Point offset = UpdateOffset(ctrl, e.Location - _dragStartLocation);

            ctrl.Left += offset.X;
            ctrl.Top += offset.Y;
        }

        private static Point UpdateOffset(Control ctrl, Point newLocationOffset)
        {
            Size parentSize = ctrl.Parent.Size;

            if (ctrl.Left + newLocationOffset.X < -ctrl.Width / 2)
                newLocationOffset.X = 0;

            if (ctrl.Top + newLocationOffset.Y < -ctrl.Height / 2)
                newLocationOffset.Y = 0;

            if (ctrl.Left + ctrl.Width + newLocationOffset.X > parentSize.Width + ctrl.Width / 2)
                newLocationOffset.X = 0;

            if (ctrl.Top + ctrl.Height + newLocationOffset.Y > parentSize.Height + ctrl.Height / 2)
                newLocationOffset.Y = 0;

            return newLocationOffset;
        }
    }
}
