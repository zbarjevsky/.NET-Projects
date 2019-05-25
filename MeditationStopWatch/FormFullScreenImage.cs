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
            get { return pictureBox1.PictureBox.Image; }
            set { pictureBox1.PictureBox.Image = value; }
        }

        public FormFullScreenImage(FormStopWatch parent)
        {
            _stopWatch = parent;

            InitializeComponent();

            m_analogClock.Settings = parent.m_Options.AnalogClockSettings.Clone();
            m_analogClock.Settings.HandOpacity = 210;
            m_analogClock.Settings.ClockBackground = Color.Transparent;
            m_analogClock.Settings.SuspendScreenSaver = true;
            m_analogClock.BackColor = Color.Transparent; 

            m_analogClock.Draggable(true);
            m_analogClock.Parent = pictureBox1.PictureBox;

            m_btnCancel.BackColor = Color.Transparent;
            m_btnCancel.Parent = pictureBox1.PictureBox;
        }

        private void FormFullScreenImage_Load(object sender, EventArgs e)
        {
            m_lblVolume.Parent = pictureBox1.PictureBox;
            m_lblVolume.Draggable(true);
            if (!DesignMode)
                m_lblVolume.Visible = false;

            m_analogClock.Bounds = _stopWatch.m_Options.ClockFullScreenBounds;
            pictureBox1.PictureBox.Focus();
            pictureBox1.PictureBox.Refresh();
            m_btnCancel.BringToFront();

            pictureBox1.OnSizeChangedAction = (bounds) =>
            {
                pictureBox1.EnsureVisible(m_lblVolume, AnchorStyles.Top | AnchorStyles.Right, 50);
                pictureBox1.EnsureVisible(m_btnCancel, AnchorStyles.Top | AnchorStyles.Right, 4, true);
                pictureBox1.EnsureVisible(m_analogClock, AnchorStyles.None, 50);
            };

            pictureBox1.ShowControlsAction = (show) => { m_btnCancel.Visible = show; };
        }

        private void FormFullScreenImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            _stopWatch.m_Options.ClockFullScreenBounds = m_analogClock.Bounds;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Alt))
                AdjustClockSize(e.Delta);
            else if (!ModifierKeys.HasFlag(Keys.Control))
                m_lblVolume.Show(_stopWatch.AdjustVolume(e.Delta), 4000);

            base.OnMouseWheel(e);
        }

        private void AdjustClockSize(int delta)
        {
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.PictureBox)).BeginInit();
            m_analogClock.AdjustClockSize(delta, pictureBox1.Bounds);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.PictureBox)).EndInit();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                case Keys.F11:
                    Close();
                    return true;
                case Keys.Space:
                    _stopWatch.PauseResume();
                    return true;
                case Keys.Up:
                    m_lblVolume.Show(_stopWatch.AdjustVolume(1), 4000);
                    return true;
                case Keys.Down:
                    m_lblVolume.Show(_stopWatch.AdjustVolume(-1), 4000);
                    return true;
                case Keys.Left:
                    pictureBox1.LoadImage(_stopWatch.ShowPrevImage().FullName);
                    return true;
                case Keys.Right:
                    pictureBox1.LoadImage(_stopWatch.ShowNextImage().FullName);
                    return true;
                default:
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
