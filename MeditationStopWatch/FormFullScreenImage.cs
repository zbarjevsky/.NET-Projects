using MeditationStopWatch.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnalogClockControl;

namespace MeditationStopWatch
{
    public partial class FormFullScreenImage : Form
    {
        class Position
        {
            public Rectangle R { get; set; }

            internal void Apply(Control ctrl)
            {
                //ctrl.Location = R.Location;
                //ctrl.Size = R.Size;
                ctrl.Bounds = R;
            }

            internal void Read(Control ctrl)
            {
                R = ctrl.Bounds; //new Rectangle(ctrl.Location, ctrl.Size);
            }
        }
        private static Position _clockPosition = null;

        public Image Picture
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        public FormFullScreenImage(Options options)
        {
            InitializeComponent();

            FormStopWatch.ApplyClockColors(m_analogClock, options);

            m_analogClock.Draggable(true);
            m_analogClock.BackColor = Color.Transparent;
            m_analogClock.Parent = pictureBox1;

            m_btnCancel.BackColor = Color.Transparent;
            m_btnCancel.Parent = pictureBox1;
        }

        private void FormFullScreenImage_Load(object sender, EventArgs e)
        {
            if (_clockPosition != null)
                _clockPosition.Apply(m_analogClock);
        }

        private void FormFullScreenImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_clockPosition == null)
                _clockPosition = new Position();
            _clockPosition.Read(m_analogClock);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            AdjustClockSize(e.Delta);
            base.OnMouseWheel(e);
        }

        private void AdjustClockSize(int delta)
        {
            double factor = delta > 0 ? 1.1 : 0.9;

            m_analogClock.Size = new Size((int)(factor * m_analogClock.Size.Width), (int)(factor * m_analogClock.Size.Height));
        }
    }
}
