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
            delta /= Math.Abs(delta);
            if (m_analogClock.Size.Width > Screen.FromControl(m_analogClock).Bounds.Height && delta > 0)
                return;

            if (m_analogClock.Size.Width < 20 && delta < 0)
                return;

            double factor = delta > 0 ? 1.1 : 0.9;
            int width = (int)(factor * m_analogClock.Size.Width);
            int deltaWidth = (m_analogClock.Size.Width - width) / 2;

            Point location = m_analogClock.Location;
            location.Offset(deltaWidth, deltaWidth);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_analogClock)).BeginInit();
            m_analogClock.Bounds = new Rectangle(location, new Size(width, width));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_analogClock)).EndInit();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space 
                || keyData == Keys.Left || keyData == Keys.Right)
            {
                //if (CanUseArrorws())
                {
                    if (keyData == Keys.Space)
                        _stopWatch.PauseResume();

                    if (keyData == Keys.Up)
                        _stopWatch.AdjustVolume(1);

                    if (keyData == Keys.Down)
                        _stopWatch.AdjustVolume(-1);

                    if (keyData == Keys.Left)
                        pictureBox1.Load(_stopWatch.ShowPrevImage().FullName);

                    if (keyData == Keys.Right)
                        pictureBox1.Load(_stopWatch.ShowNextImage().FullName);

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
