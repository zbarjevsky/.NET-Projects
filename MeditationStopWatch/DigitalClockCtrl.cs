using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MeditationStopWatch
{
    public partial class DigitalClockCtrl : PictureBox
    {
        public DigitalClockCtrl()
        {
            InitializeComponent();
        }

        private void DigitalClockCtrl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            e.Graphics.Clear(BackColor);

            string time = DateTime.Now.ToString("HH:mm:ss");

            SizeF size = e.Graphics.MeasureString(time, Font);
            PointF location = new PointF((Width-size.Width)/2, (Height-size.Height)/2);

            e.Graphics.DrawString(time, Font, new SolidBrush(ForeColor), location);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
