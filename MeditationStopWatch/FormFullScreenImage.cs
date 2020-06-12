using MeditationStopWatch.Tools;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MZ.WinForms;

namespace MeditationStopWatch
{
    public partial class FormFullScreenImage : Form
    {
        FormStopWatch _stopWatchForm;
        ZoomablePictureBoxUserControl.MarginRect _clockMargins = new ZoomablePictureBoxUserControl.MarginRect(100);
        static double _zoomScale = 1;

        public Image Picture
        {
            get { return pictureBox1.PictureBox.Image; }
            set { pictureBox1.PictureBox.Image = value; }
        }

        public FormFullScreenImage(FormStopWatch parent)
        {
            _stopWatchForm = parent;

            InitializeComponent();

            m_analogClock.Settings = parent.m_Options.AnalogClockSettings.Clone();
            m_analogClock.Settings.ClockBackground = Color.Transparent;
            m_analogClock.Settings.SuspendScreenSaver = true;
            m_analogClock.BackColor = Color.Transparent;

            m_analogClock.Draggable(true, () => { UpdateClockMargins(); SaveClockRectangle(); });
            m_analogClock.Parent = pictureBox1.PictureBox; //to show picture as background

            m_btnCancel.BackColor = Color.Transparent;
            m_btnCancel.Parent = pictureBox1.PictureBox;
        }

        private void FormFullScreenImage_Load(object sender, EventArgs e)
        {
            m_lblVolume.Parent = pictureBox1.PictureBox;
            m_lblVolume.Draggable(true);
            if (!DesignMode)
                m_lblVolume.Visible = false;

            pictureBox1.OnSizeChangedAction = (bounds) => EnsureVisibleControls();
            pictureBox1.OnMouseWheelAction = (delta) => EnsureVisibleControls();
            pictureBox1.ShowControlsAction = (show) => { m_btnCancel.Visible = show; m_lblUsage.Visible = show; };
            pictureBox1.OnClickAction = () => { _stopWatchForm.PauseResume(); };

            this.WindowState = FormWindowState.Maximized;

            EnsureVisibleControls();
            pictureBox1.Zoom(_zoomScale);
            pictureBox1.PictureBox.Focus();
            pictureBox1.PictureBox.Refresh();
            m_btnCancel.BringToFront();
            
            m_analogClock.Bounds = _stopWatchForm.m_Options.ClockFullScreenBounds;
            UpdateClockMargins();
            //m_analogClock.LocationChanged += (s, o) => { UpdateClockMargins(); SaveClockRectangle(); };

            this.Activate();
        }

        private void FormFullScreenImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveClockRectangle();
        }

        private void FormFullScreenImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            _zoomScale = (double)pictureBox1.PictureBox.Width / (double)pictureBox1.Width;
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            //Close();
            this.Visible = false;
        }

        private void EnsureVisibleControls()
        {
            pictureBox1.EnsureVisible(m_lblVolume, AnchorStyles.Top | AnchorStyles.Right, 50, true);
            pictureBox1.EnsureVisible(m_btnCancel, AnchorStyles.Top | AnchorStyles.Right, 4, true);
            pictureBox1.EnsureVisible(m_analogClock, AnchorStyles.Top | AnchorStyles.Right, _clockMargins, true);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Alt))
                AdjustClockSize(e.Delta);
            else if (!ModifierKeys.HasFlag(Keys.Control))
                m_lblVolume.Show(_stopWatchForm.AdjustVolume(e.Delta), 4000);

            //enforce clock margins - if image resized
            EnsureVisibleControls();

            base.OnMouseWheel(e);
        }

        private void AdjustClockSize(int delta)
        {
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.PictureBox)).BeginInit();
            m_analogClock.AdjustClockSize(delta, pictureBox1.Bounds);
            pictureBox1.EnsureVisible(m_analogClock, AnchorStyles.Top | AnchorStyles.Right, _clockMargins, true);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.PictureBox)).EndInit();
            SaveClockRectangle();
        }

        private void UpdateClockMargins()
        {
            if (m_analogClock.Anchor.HasFlag(AnchorStyles.Left))
            {
                _clockMargins.Left = pictureBox1.PictureBox.Left + m_analogClock.Left;
            }
            if (m_analogClock.Anchor.HasFlag(AnchorStyles.Top))
            {
                _clockMargins.Top = pictureBox1.PictureBox.Top + m_analogClock.Top;
            }
            if (m_analogClock.Anchor.HasFlag(AnchorStyles.Right))
            {
                _clockMargins.Right = pictureBox1.Width - (pictureBox1.PictureBox.Left + m_analogClock.Right);
            }
            if (m_analogClock.Anchor.HasFlag(AnchorStyles.Bottom))
            {
                _clockMargins.Bottom = pictureBox1.Height - (pictureBox1.PictureBox.Top + m_analogClock.Bottom);
            }
        }

        private void SaveClockRectangle()
        {
            //save bounds
            _stopWatchForm.m_Options.ClockFullScreenBounds = new Rectangle()
            {
                X = pictureBox1.PictureBox.Left + m_analogClock.Left,
                Y = pictureBox1.PictureBox.Top + m_analogClock.Top,
                Width = m_analogClock.Width,
                Height = m_analogClock.Height
            };
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                case Keys.F11:
                    //Close();
                    this.Visible = false;
                    return true;
                case Keys.Space:
                    _stopWatchForm.PauseResume();
                    return true;
                case Keys.Up:
                    m_lblVolume.Show(_stopWatchForm.AdjustVolume(1), 4000);
                    return true;
                case Keys.Down:
                    m_lblVolume.Show(_stopWatchForm.AdjustVolume(-1), 4000);
                    return true;
                case Keys.Left:
                    pictureBox1.LoadImage(_stopWatchForm.ShowPrevImage().FullName);
                    EnsureVisibleControls();
                    return true;
                case Keys.Right:
                    pictureBox1.LoadImage(_stopWatchForm.ShowNextImage().FullName);
                    EnsureVisibleControls();
                    return true;
                default:
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
