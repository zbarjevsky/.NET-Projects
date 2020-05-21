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
using MZ.WinForms;

namespace MeditationStopWatch
{
    public partial class ZoomablePictureBoxUserControl : UserControl
    {
        public class MarginRect
        {
            public int Left, Top, Right, Bottom;

            public MarginRect(int margin, int max = 10000, int min = 0)
            {
                Left = Top = Right = Bottom = EnsureValidMargin(margin, max, min);
            }

            public MarginRect(Rectangle r, Rectangle bounds)
            {
                FromRectangle(r, bounds);
            }

            public MarginRect FromRectangle(Rectangle r, Rectangle bounds)
            {
                Left = EnsureValidMargin(r.Left, bounds.Width - r.Width);
                Top = EnsureValidMargin(r.Top, bounds.Height - r.Height);
                Right = EnsureValidMargin(bounds.Width - r.Right, bounds.Width);
                Bottom = EnsureValidMargin(bounds.Height - r.Bottom, bounds.Height);

                return this;
            }

            public static int EnsureValidMargin(int margin, int max, int min = 0)
            {
                if (margin < min) margin = min;
                if (margin > max) margin = max;
                return margin;
            }
        }

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

        public void EnsureVisible(Control ctrl, AnchorStyles ancors, int margin = 0, bool bAlways = false)
        {
            EnsureVisible(ctrl, ancors, new MarginRect(margin), bAlways);
        }

        public void EnsureVisible(Control ctrl, AnchorStyles ancors, MarginRect margin, bool bAlways = false)
        {
            if (margin == null)
                margin = new MarginRect(100);

            //visible bounds in picture box coordinates
            int left = -pictureBox1.Left;
            int top = -pictureBox1.Top;
            int right = this.Width - pictureBox1.Left;
            int bottom = this.Height - pictureBox1.Top;

            if (bAlways || ctrl.Left < left || ctrl.Top < top || ctrl.Right > right || ctrl.Bottom > bottom)
            {
                if (ancors == AnchorStyles.None) //center if out of view
                {
                    ctrl.Left = left + (right - left - ctrl.Width) / 2;
                    ctrl.Top = top + (bottom - top - ctrl.Height) / 2;
                }
                if (ancors.HasFlag(AnchorStyles.Left))
                {
                    ctrl.Left = left + margin.Left;
                }
                if (ancors.HasFlag(AnchorStyles.Right))
                {
                    ctrl.Left = right - ctrl.Width - margin.Right;
                }
                if (ancors.HasFlag(AnchorStyles.Top))
                {
                    ctrl.Top = top + margin.Top;
                }
                if (ancors.HasFlag(AnchorStyles.Bottom))
                {
                    ctrl.Top = bottom - ctrl.Height - margin.Bottom;
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

        public void Zoom(double scale)
        {
            if (scale == 1.0)
                return;

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
            if (this.DesignMode)
                return;

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
            if(show)
            {
                CursorHandler.IsCursorVisible = true;
            }
            else
            {
                Control parent = Parent;
                while (parent.Parent != null)
                    parent = parent.Parent;

                IntPtr hWnd = CursorHandler.WindowFromPoint(Cursor.Position);
                if (hWnd == pictureBox1.Handle) //mouse over picture box
                    CursorHandler.IsCursorVisible = false;
            }
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
