using MeditationStopWatch.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeditationStopWatch
{
    public partial class FormFullScreenImage : Form
    {
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
        }

        private void FormFullScreenImage_Load(object sender, EventArgs e)
        {
            //pictureBox1.Dock = DockStyle.None;
            //pictureBox1.Draggable(true);
            //this.Draggable(true);
            //this.WindowState = FormWindowState.Normal;
        }
    }
}
