using System;
using System.Collections.Generic;
using System.Drawing;
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
        private static System.Drawing.Size _mouseOffset;

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

        public static void ShowOnDisabled(this ToolTip control, bool bEnable)
        {

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
            _mouseOffset = new System.Drawing.Size(e.Location);
            // turning on dragging
            _draggables[(Control)sender] = true;
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

            // calculations of control's new position
            Point newLocationOffset = UpdateOffset(ctrl, e.Location - _mouseOffset);

            ctrl.Left += newLocationOffset.X;
            ctrl.Top += newLocationOffset.Y;
        }

        private static Point UpdateOffset(Control ctrl, Point newLocationOffset)
        {
            const int HARD_MARGIN = 128;

            Size parentSize = ctrl.Parent.Size;

            if (ctrl.Left + newLocationOffset.X < -HARD_MARGIN)
                newLocationOffset.X = 0;

            if (ctrl.Top + newLocationOffset.Y < -HARD_MARGIN)
                newLocationOffset.Y = 0;

            if (ctrl.Left + ctrl.Width + newLocationOffset.X > parentSize.Width + HARD_MARGIN)
                newLocationOffset.X = 0;

            if (ctrl.Top + ctrl.Height + newLocationOffset.Y > parentSize.Height + HARD_MARGIN)
                newLocationOffset.Y = 0;

            return newLocationOffset;
        }
    }
}
