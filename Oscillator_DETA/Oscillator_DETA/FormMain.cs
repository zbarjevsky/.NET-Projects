using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oscillator_DETA
{
    public partial class FormMain : Form
    {
        private const int _duration_ms = 1000;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void m_btnPlay_Click(object sender, EventArgs e)
        {
            m_btnPlay.Enabled = false;
            System.Media.SoundPlayer player = Oscillator1.PlayBeep((double)m_numFreq.Value, (double)m_numFreq2.Value, _duration_ms, m_rbSine.Checked);

            m_numBufferSize.Maximum = Oscillator1._buffer.Length;
            m_numOffset.Maximum = Oscillator1._buffer.Length;

            m_numBufferSize_ValueChanged(this, e);

            m_btnPlay.Enabled = true;
        }

        private void m_trackFreq_ValueChanged(object sender, EventArgs e)
        {
            m_numFreq.Value = m_trackFreq.Value;
        }

        private void m_numFreq_ValueChanged(object sender, EventArgs e)
        {
            m_trackFreq.Value = (int)m_numFreq.Value;
        }

        private void m_numBufferSize_ValueChanged(object sender, EventArgs e)
        {
            m_chart.Series[0].Points.Clear();
            if (Oscillator1._buffer == null || Oscillator1._buffer.Length < 10)
                return;

            int start = (int)m_numOffset.Value;
            int end = start + (int)(m_numBufferSize.Value);
            double ms_per_sample = _duration_ms / (double)Oscillator1._buffer.Length;

            double[] buff = Oscillator1._buffer;
            double[] buffTime;
            const int max = 10000;
            int len = end - start;
            if(len > max)
            {
                buff = new double[max+1];
                buffTime = new double[max+1];
                for (int i = 0; i < max; i++)
                {
                    int offset = len * i / max;
                    buff[i] = Oscillator1._buffer[start + offset];
                    buffTime[i] = ms_per_sample * (start + offset);
                }
            }
            else
            {
                buff = new double[len+1];
                buffTime = new double[len+1];
                for (int i = 0; i < len; i++)
                {
                    buff[i] = Oscillator1._buffer[start + i];
                    buffTime[i] = ms_per_sample * (start + i);
                }
            }

            for (int i = 0; i < buff.Length; i++)
            {
                m_chart.Series[0].Points.AddXY(Math.Round(buffTime[i], 4), buff[i]);
            }
        }
    }
}
