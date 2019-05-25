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

        public Action OnClick = () => { };
        public Action<Rectangle> OnSizeChanged = (rect) => { };

        public ZoomablePictureBoxUserControl()
        {
            InitializeComponent();
        }

        public PictureBox PictureBox {  get { return pictureBox1; } }

        public void EnsureVisible(Control ctrl)
        {
            //visible bounds in picture box coordinates
            int left = pictureBox1.Left > 0 ? 0 : -pictureBox1.Left;
            int top = pictureBox1.Top > 0 ? 0 : -pictureBox1.Top;
            int right = pictureBox1.Right > this.Width ? this.Width - pictureBox1.Left : pictureBox1.Width;
            int bottom = pictureBox1.Bottom > this.Height ? this.Height - pictureBox1.Top : pictureBox1.Height;

            if (ctrl.Left < left)
                ctrl.Left = left;
            if (ctrl.Top < top)
                ctrl.Top = top;
            if (ctrl.Right > right)
                ctrl.Left = right - ctrl.Width;
            if (ctrl.Bottom > bottom)
                ctrl.Top = bottom - ctrl.Height;
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
            OnSizeChanged(pictureBox1.Bounds);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            ShowControls(true);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            ShowControls(true);
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

            m_btnZoomIn.Visible = show;
            m_btnZoomOut.Visible = show;
            m_btnOrigSize.Visible = show;
            m_btnFitWindow.Visible = show;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OnClick();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            Zoom(e.Delta > 0 ? 1.1 : 0.9);
        }
    }
}
