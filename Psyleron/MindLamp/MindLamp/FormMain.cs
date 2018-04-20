using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace MindLamp
{
	public partial class FormMain : Form
	{

        Algorithms _alg = new Algorithms();

        private int m_total, m_total1 = 0, m_maxCurrent = 0;
		private ColorFromValues m_WhiteToColor = new ColorFromValues();
	    private bool _isHW_Avalable = false;
        private Random _random = new Random(DateTime.Now.Second);

        public FormMain()
		{
			InitializeComponent();

            m_numRange.Minimum = m_trackRange.Minimum;
            m_numRange.Maximum = m_trackRange.Maximum;
            m_numRange.Value = m_trackRange.Value;
        }

        private void FormMain_Load(object sender, EventArgs e)
		{
			try
			{
                m_btnStart.Enabled = false;
			    this.Visible = true;

                int ver = PsyleronApi.PsyREGAPIVersion();
				ulong build = PsyleronApi.PsyREGAPIBuild();

				uint sources = PsyleronApi.PsyREGEnumerateSources();
				uint count = PsyleronApi.PsyREGGetSourceCount();

                for (int i = 0; i < 1; i++)
                {
                    int source = PsyleronApi.PsyREGGetSource(i);

                    //string id = PsyleronApi.PsyREGGetDeviceIdBSTR(source);
                    //string type = PsyleronApi.PsyREGGetDeviceTypeBSTR(source);

                    //m_cmbDevice.Items.Add(id);
                }

                //m_cmbDevice.SelectedIndex = 0;

                int ok = PsyleronApi.PsyREGOpen(0);
			    if (ok == 1)
			    {
			        m_btnStart.Enabled = true; // m_cmbDevice.Text == "RGZD715";
			        _isHW_Avalable = true;
			    }
			    else
			    {
                    MessageBox.Show(this, "Cannot connect to device!");
			    }
            }
			catch (Exception err)
			{
				MessageBox.Show(this, err.Message);
			}
		}

		private void m_timer_Tick(object sender, EventArgs e)
		{
            try
            {
                if (m_chkSW_RG.Checked)
                {
                    byte[] pucBuf = GetRandomBytes(10);
                    CalculateDeviation(pucBuf);
                }
                else
                {
                    int ok = PsyleronApi.PsyREGOpened(0);
                    if (ok != 1)
                        return;

                    byte[] pucBuf = PsyleronApi.PsyREGGetBytes(0, 10);
                    CalculateDeviation(pucBuf);
                }

                m_lblBuffer.Text = "Buffer: (" + m_trackBuffer.Value + "/" + _alg.m_queue.Count + ")";
            }
            catch (Exception err)
            {
                Debug.Write("Error in timer: "+err.ToString());
            }
        }

	    private byte[] GetRandomBytes(int count)
	    {
	        byte [] data = new byte[count];
            _random.NextBytes(data);
            return data;
	    }

	    private void CalculateDeviation(byte[] data)
		{
			int countFromEnd = m_trackBuffer.Value;
            _alg.AddRange(data);
            _alg.CalculateDeviation(m_trackBuffer.Value);

            m_chartValues.Series[0].Points.Add(_alg.m_relative);
            m_chartValues.Series[1].Points.Add(0.0);
            while (m_chartValues.Series[0].Points.Count > countFromEnd)
            {
                m_chartValues.Series[0].Points.RemoveAt(0);
                m_chartValues.Series[1].Points.RemoveAt(0);
            }
            m_chartValues.ResetAutoValues();

            m_picCurrent.BackColor = _alg.m_relative > 0 ? Color.Red : Color.Blue;

			//sb.Insert(0, "\t");
			//sb.Insert(0, _alg.m_result);

			m_total = 0;
			foreach (var item in _alg.m_queue)
			{
				if (item % 2 == 0)
					m_total++;
				else
					m_total--;
			}
			m_total1 += _alg.m_absolute;

			//double index = (1000 / m_trackBuffer.Value);
			//double min = -0.1 * index;
			//double max = 0.1 * index;
			//m_trackRange.Value = WavelengthColors.GetWaveLengthFromRange(result, min, max);

			int colorIdx = 0;
			if (m_tabMode.SelectedIndex == 0)
			{
				m_pic.BackColor = ColorFromValues.GetWhiteToColor(_alg.m_relative, out colorIdx);
			}
			else
			{
				m_pic.BackColor = ColorFromValues.GetRainbowColor(_alg.m_relative, out colorIdx);
			}

			if (colorIdx > 0)
			{
				switch (colorIdx)
				{
					case 1: TextPlusPlus(ref label1); break;
					case 2: TextPlusPlus(ref label2); break;
					case 3: TextPlusPlus(ref label3); break;
					case 4: TextPlusPlus(ref label4); break;
					case 5: TextPlusPlus(ref label5); break;
					case 6: TextPlusPlus(ref label6); break;
					case 7: TextPlusPlus(ref label7); break;
					case 8: TextPlusPlus(ref label8); break;
				}
				m_lblCurrenColor.BackColor = ColorFromValues.m_Colors[colorIdx];
			}

			if (Math.Abs(_alg.m_absolute) > m_maxCurrent)
				m_maxCurrent = Math.Abs(_alg.m_absolute);

			//System.Diagnostics.Debug.WriteLine("BUFF: " + sb.ToString());
			string status = string.Format("Value: {0:0.00}({1}), Current: {2}/Max: {3}, Total: {4}/{5}",
                _alg.m_relative, m_trackRange.Value, (_alg.m_absolute), m_maxCurrent, m_total, m_total1);
			
			System.Diagnostics.Debug.WriteLine(status);
			m_lblStatus.Text = status;
		}

		private void TextPlusPlus(ref Label l)
		{
			int i=0;
			if (int.TryParse(l.Text, out i))
			{
				l.Text = "" + ++i;
			}
			else
			{
				l.Text = "1";
			}
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
            try
            {
    			PsyleronApi.PsyREGClose(0);
            }
            catch (Exception)
            {
            }
		}

		private void m_btnStart_Click(object sender, EventArgs e)
		{
			if (m_timer.Enabled)
			{
				m_timer.Stop();
				m_btnStart.Text = ("Start >");
			}
			else
			{
				m_timer.Interval = m_trackInterval.Value;
				m_timer.Start();
				m_btnStart.Text = ("[] Stop");
			}
		}

		private void m_trackRange_ValueChanged(object sender, EventArgs e)
		{
			m_pic.BackColor = WavelengthColors.GetColorFromWaveLength(m_trackRange.Value);
            m_numRange.Value = m_trackRange.Value;
        }

		private void m_trackInterval_ValueChanged(object sender, EventArgs e)
		{
			m_lblTInterval.Text = string.Format("Interval: ({0:##,###} ms), {1:0.00} Hz", 
                m_trackInterval.Value, (1000.0 / (double)m_trackInterval.Value));

		    if (m_btnStart.Enabled)
		    {
		        m_timer.Stop();
		        m_timer.Interval = m_trackInterval.Value;
		        m_timer.Start();
		    }
		}

        private void m_trackRange_Scroll(object sender, EventArgs e)
        {

        }

        private void m_numLuminosity_ValueChanged(object sender, EventArgs e)
        {
            ColorHSL hsv = new ColorHSL(Color.Red);
            hsv.Luminosity = (double)m_numLuminosity.Value;
            m_picCurrent.BackColor = hsv;
        }

        private void m_trackInterval_Scroll(object sender, EventArgs e)
        {

        }

        private void m_chkSW_RG_CheckedChanged(object sender, EventArgs e)
        {
            m_btnStart.Enabled = _isHW_Avalable || m_chkSW_RG.Checked;
        }

        private void m_trackBuffer_ValueChanged(object sender, EventArgs e)
		{
			m_lblBuffer.Text = "Buffer: (" + m_trackBuffer.Value + ")";
		}
	}
}
