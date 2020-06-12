using MZ.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.WinForms
{
    public static class DraggableExtension
    {
        private static Size _dragOffset = new Size();
        // TKey is control to drag, TValue is a flag used while dragging
        private static Dictionary<Control, bool> _draggables = new Dictionary<Control, bool>();
        private static System.Drawing.Size _dragStartLocation;

        /// <summary>
        /// Enabling/disabling dragging for control
        /// </summary>
        public static void Draggable(this Control control, bool enable)
        {
            if (control is Form)
            {
                (control as Form).Draggable(enable);
                return;
            }

            Debug.Assert(control.Parent != null);

            if (enable)
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

        public static bool WasDragging(this Control control)
        {
            if (!_draggables.ContainsKey(control))
                return false;
            return (Math.Abs(_dragOffset.Width) > 0 || Math.Abs(_dragOffset.Height) > 0);
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

    public static class DraggableForm
    {
        private static Dictionary<Form, bool> _draggables = new Dictionary<Form, bool>();

        public static void Draggable(this Form form, bool enable)
        {
            if (enable)
            {
                if (_draggables.ContainsKey(form))
                    return;
                _draggables.Add(form, false);

                form.MouseDown += Form_MouseDown;
            }
            else 
            {
                if (_draggables.ContainsKey(form))
                {
                    form.MouseDown -= Form_MouseDown;
                    _draggables.Remove(form);
                }
            }
        }

        private static void Form_MouseDown(object sender, MouseEventArgs e)
        {
            Debug.Assert(sender is Form);

            if (e.Button == MouseButtons.Left)
            {
                const int HT_CAPTION = 0x2;

                User32.ReleaseCapture();
                User32.SendMessage((sender as Form).Handle, (uint)WM_Message.WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
