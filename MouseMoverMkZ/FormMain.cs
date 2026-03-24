using MkZ.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseMoverMkZ
{
    public partial class FormMain : Form
    {
        private System.Windows.Forms.Timer m_Timer = new System.Windows.Forms.Timer();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_Timer.Tick += new EventHandler(TimerTick);
            m_Timer.Interval = 1000;
            m_Timer.Start();
        }

        private void m_chkEnableMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_chkEnableMouseMove.Checked)
                m_lblTime.Text = "Inactive";
        }

        private void TimerTick(object sender, EventArgs e)
        {
            MoveMouseIfInactive();
        }

        private void MoveMouseIfInactive()
        {
            if (!m_chkEnableMouseMove.Checked)
                return;

            try
            {
                TimeSpan idleTime = UserInactivityDetector.GetIdleTime();
                if (idleTime.TotalMinutes > 5)
                {
                    MouseInput.SimulateMouseMove(3);
                    MouseInput.SimulateMouseMove(-3);
                }
                m_lblTime.Text = "Idle time: " + idleTime.ToString(@"mm\:ss");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }        
        }
    }
}
