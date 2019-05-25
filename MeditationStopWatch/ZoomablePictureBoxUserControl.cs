using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MeditationStopWatch.Tools;
using System.Diagnostics;

namespace MeditationStopWatch
{
    public partial class ZoomablePictureBoxUserControl : UserControl
    {
        Stopwatch _stopwatch = new Stopwatch();

        public Action OnClickAction = () => { };
        public Action<Rectangle> OnSizeChangedAction = (rect) => { };
        public Action OnMouseMoveAction = () => { };
        public Action<bool> ShowControlsAction = (show) => { };
        public Action<int> OnMouseWheelAction = (delta) => { };

        public ZoomablePictureBoxUserControl()
        {
            InitializeComponent();
        }

        public PictureBox PictureBox {  get { return pictureBox1; } }
        public void LoadImage(string fullPath)
        {
            pictureBox1.Load(fullPath);
            toolTip1.SetToolTip(m_btnOrigSize, string.Format("Original Size ({0})", pictureBox1.Image.Size));
            m_btnFitWindow_Click(this, null);
        }

        public void EnsureVisible(Control ctrl, AnchorStyles ancors, int margin = 20, bool bAlways = false)
        {
            //visible bounds in picture box coordinates
            int left = pictureBox1.Left > 0 ? 0 : -pictureBox1.Left;
            int top = pictureBox1.Top > 0 ? 0 : -pictureBox1.Top;
            int right = pictureBox1.Right > this.Width ? this.Width - pictureBox1.Left : pictureBox1.Width;
            int bottom = pictureBox1.Bottom > this.Height ? this.Height - pictureBox1.Top : pictureBox1.Height;

            //if (ctrl.Left < left)
            //    ctrl.Left = left;
            //if (ctrl.Top < top)
            //    ctrl.Top = top;
            //if (ctrl.Right > right)
            //    ctrl.Left = right - ctrl.Width;
            //if (ctrl.Bottom > bottom)
            //    ctrl.Top = bottom - ctrl.Height;

            if (bAlways || ctrl.Left < left || ctrl.Top < top || ctrl.Right > right || ctrl.Bottom > bottom)
            {
                if (ancors == AnchorStyles.None) //center if out of view
                {
                    ctrl.Left = left + (right - left - ctrl.Width) / 2;
                    ctrl.Top = top + (bottom - top - ctrl.Height) / 2;
                }
                if (ancors.HasFlag(AnchorStyles.Left))
                {
                    ctrl.Left = left + margin;
                }
                if (ancors.HasFlag(AnchorStyles.Right))
                {
                    ctrl.Left = right - ctrl.Width - margin;
                }
                if (ancors.HasFlag(AnchorStyles.Top))
                {
                    ctrl.Top = top + margin;
                }
                if (ancors.HasFlag(AnchorStyles.Bottom))
                {
                    ctrl.Top = bottom - ctrl.Height - margin;
                }
            }
        }

        private void ZoomablePictureBoxUserControl_Load(object sender, EventArgs e)
        {
            _stopwatch.Restart();
        }

        private void m_btnZoomIn_Click(object sender, EventArgs e)
        {
            Zoom(1.1);
        }

        private void m_btnZoomOut_Click(object sender, EventArgs e)
        {
            Zoom(0.9);
        }

        private void Zoom(double scale)
        {
            if (pictureBox1.Dock == DockStyle.Fill)
            {
                pictureBox1.Dock = DockStyle.None;
                pictureBox1.Size = this.Size;
                pictureBox1.Draggable(true);
            }

            pictureBox1.Width = (int)(pictureBox1.Width * scale);
            pictureBox1.Height = (int)(pictureBox1.Height * scale);

            CenterImage();
        }

        private void m_btnFitWindow_Click(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Draggable(false);
        }

        private void m_btnOrigSize_Click(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.None;
            pictureBox1.Size = pictureBox1.Image.Size;
            pictureBox1.Draggable(true);
            CenterImage();
        }

        private void CenterImage()
        {
            int left = (this.Width - pictureBox1.Width) / 2;
            int top = (this.Height - pictureBox1.Height) / 2;
            pictureBox1.Location = new Point(left, top);
            OnSizeChangedAction(pictureBox1.Bounds);
        }

        private Point _mousePos = new Point();
        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (Math.Abs(e.X - _mousePos.X) < 2 && Math.Abs(e.Y - _mousePos.Y) < 2)
                return;
            _mousePos = e.Location;
            ShowControls(true);
            OnMouseMoveAction();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_stopwatch.Elapsed.TotalMilliseconds > 10000)
                ShowControls(false);
        }

        private void ShowControls(bool show)
        {
            if(show)
                _stopwatch.Restart();

            if (m_btnFitWindow.Visible == show)
                return;

            if (pictureBox1.Dock == DockStyle.Fill)
                pictureBox1.Cursor = Cursors.Arrow;
            else
                pictureBox1.Cursor = Cursors.Hand;

            ShowCursor(show);

            m_btnZoomIn.Visible = show;
            m_btnZoomOut.Visible = show;
            m_btnOrigSize.Visible = show;
            m_btnFitWindow.Visible = show;

            ShowControlsAction(show);
        }

        private void ShowCursor(bool show)
        {
            if (!show && ClientRectangle.Contains(PointToClient(Control.MousePosition)))
                CursorHandler.IsCursorVisible = false;
            else
                CursorHandler.IsCursorVisible = true;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            ShowControls(true);

            if (pictureBox1.WasDragging())
            {
                OnSizeChangedAction(pictureBox1.Bounds);
                return;
            }

            if(e.Button == MouseButtons.Left)
                OnClickAction();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if(ModifierKeys.HasFlag(Keys.Control))
                Zoom(e.Delta > 0 ? 1.1 : 0.9);

            OnMouseWheelAction(e.Delta);
        }
    }
}
