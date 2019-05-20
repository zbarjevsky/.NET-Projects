using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MeditationStopWatch.Tools
{
    public partial class LabelWithTimeout : Label
    {
        private Stopwatch _stopwatch = new Stopwatch();

        private int _timeout  = 3000;

        public LabelWithTimeout()
        {
            InitializeComponent();

            m_Timer.Start();
        }

        public void Show(string text, int timeout = 3000)
        {
            this.BackColor = Color.FromArgb(25, ForeColor); //semi transparent

            this.Text = text;
            _timeout = timeout;
            this.Visible = true;
            _stopwatch.Restart();
        }

        private void LabelWithTimeout_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible)
                _stopwatch.Restart();
        }

        private void m_Timer_Tick(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            if (_stopwatch.Elapsed.TotalMilliseconds > _timeout)
                this.Visible = false;
        }

        private void LabelWithTimeout_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Visible)
                _stopwatch.Restart();
        }
    }
}
