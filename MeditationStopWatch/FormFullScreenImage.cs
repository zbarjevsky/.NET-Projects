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
        FormStopWatch _stopWatch;

        public Image Picture
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        public FormFullScreenImage(FormStopWatch parent)
        {
            _stopWatch = parent;

            InitializeComponent();

            FormStopWatch.ApplyClockColors(m_analogClock, parent.m_Options);

            m_analogClock.Draggable(true);
            m_analogClock.BackColor = Color.Transparent;
            m_analogClock.Parent = pictureBox1;
            m_analogClock.SuspendScreenSaver = true;

            m_btnCancel.BackColor = Color.Transparent;
            m_btnCancel.Parent = pictureBox1;
        }

        private void FormFullScreenImage_Load(object sender, EventArgs e)
        {
            m_lblVolume.Parent = pictureBox1;
            m_lblVolume.Draggable(true);
            if (!DesignMode)
                m_lblVolume.Visible = false;

            m_analogClock.Bounds = _stopWatch.m_Options.ClockFullScreenBounds;
        }

        private void FormFullScreenImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            _stopWatch.m_Options.ClockFullScreenBounds = m_analogClock.Bounds;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            AdjustClockSize(e.Delta);
            base.OnMouseWheel(e);
        }

        private void AdjustClockSize(int delta)
        {
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            m_analogClock.AdjustClockSize(delta, pictureBox1.Bounds);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Close();
                    return true;
                case Keys.Space:
                    _stopWatch.PauseResume();
                    return true;
                case Keys.Up:
                    int vol1 = _stopWatch.AdjustVolume(1);
                    m_lblVolume.Show((vol1 / 10.0).ToString("Volume: 0.0") + "%", 4000);
                    return true;
                case Keys.Down:
                    int vol2 = _stopWatch.AdjustVolume(-1);
                    m_lblVolume.Show((vol2 / 10.0).ToString("Volume: 0.0") + "%", 4000);
                    return true;
                case Keys.Left:
                    pictureBox1.Load(_stopWatch.ShowPrevImage().FullName);
                    return true;
                case Keys.Right:
                    pictureBox1.Load(_stopWatch.ShowNextImage().FullName);
                    return true;
                default:
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
