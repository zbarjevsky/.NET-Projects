using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MindLamp
{
	public partial class FormMain : Form
	{

		private List<byte> m_queue = new List<byte>(300000);
		private int m_total, m_total1 = 0, m_maxCurrent = 0;
		private WhiteToColor m_WhiteToColor = new WhiteToColor();

		public FormMain()
		{
			InitializeComponent();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			try
			{
				int ver = PsyleronApi.PsyREGAPIVersion();
				ulong build = PsyleronApi.PsyREGAPIBuild();

				uint sources = PsyleronApi.PsyREGEnumerateSources();
				uint count = PsyleronApi.PsyREGGetSourceCount();

				for (int i = 0; i < 1; i++)
				{
					int source = PsyleronApi.PsyREGGetSource(i);

					string id = PsyleronApi.PsyREGGetDeviceIdBSTR(source);
					string type = PsyleronApi.PsyREGGetDeviceTypeBSTR(source);

					m_cmbDevice.Items.Add(id);
				}

				m_cmbDevice.SelectedIndex = 0;

				m_btnStart.Enabled = m_cmbDevice.Text == "RGZD715";

				int ok = PsyleronApi.PsyREGOpen(0);

				
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}

		private void m_timer_Tick(object sender, EventArgs e)
		{
			int ok = PsyleronApi.PsyREGOpened(0);
			if (ok != 1)
				return;

			//restrict size
			if (m_queue.Count > 800000)
				m_queue.RemoveRange(0, 400000);

			byte[] pucBuf = PsyleronApi.PsyREGGetBytes(0);
			m_queue.AddRange(pucBuf);
			
			CalculateDeviation();

			m_lblBuffer.Text = "Buffer: (" + m_trackBuffer.Value + "/" + m_queue.Count + ")";
		}

		private void CalculateDeviation()
		{
			int countFromEnd = m_trackBuffer.Value;

			int odd = 0;
			int even = 0;
			StringBuilder sb = new StringBuilder(3 * countFromEnd);
			for (int i = m_queue.Count - 1; i >= 0 && i > m_queue.Count - countFromEnd; i--)
			{
				byte item = m_queue[i];
				sb.Append(item);
				sb.Append("\t");

				if (item % 2 == 0)
					even++;
				else
					odd++;
			}

			int current = (even - odd);
			double result = (double)current / (double)(even + odd);

			m_picCurrent.BackColor = result > 0 ? Color.Red : Color.Blue;

			sb.Insert(0, "\t");
			sb.Insert(0, result);

			m_total = 0;
			foreach (var item in m_queue)
			{
				if (item % 2 == 0)
					m_total++;
				else
					m_total--;
			}
			m_total1 += (even - odd);

			//double index = (1000 / m_trackBuffer.Value);
			//double min = -0.1 * index;
			//double max = 0.1 * index;
			//m_trackRange.Value = WavelengthColors.GetWaveLengthFromRange(result, min, max);

			int colorIdx = 0;
			if (m_tabMode.SelectedIndex == 0)
			{
				m_pic.BackColor = m_WhiteToColor.GetWhiteToColor(current, m_trackBuffer.Value, out colorIdx);
			}
			else
			{
				m_pic.BackColor = m_WhiteToColor.GetRainbowColor(current, m_trackBuffer.Value, out colorIdx);
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
				m_lblCurrenColor.BackColor = m_WhiteToColor.m_Colors[colorIdx];
			}

			if (Math.Abs(current) > m_maxCurrent)
				m_maxCurrent = Math.Abs(current);

			//System.Diagnostics.Debug.WriteLine("BUFF: " + sb.ToString());
			string status = string.Format("Value: {0:0.00}({1}), Current: {2}/Max: {3}, Total: {4}/{5}",
				result, m_trackRange.Value, (current), m_maxCurrent, m_total, m_total1);
			
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
			PsyleronApi.PsyREGClose(0);
		}

		private void m_btnStart_Click(object sender, EventArgs e)
		{
			if (m_timer.Enabled)
			{
				m_timer.Stop();
				m_btnStart.Text = ("> Play");
			}
			else
			{
				m_timer.Interval = 1000 / m_trackFrequency.Value;
				m_timer.Start();
				m_btnStart.Text = ("> Stop");
			}
		}

		private void m_trackRange_ValueChanged(object sender, EventArgs e)
		{
			m_pic.BackColor = WavelengthColors.GetColorFromWaveLength(m_trackRange.Value);
		}

		private void m_trackFrequency_ValueChanged(object sender, EventArgs e)
		{
			m_lblTFreq.Text = "Frequency: (" + m_trackFrequency.Value + " Hz)";

			m_timer.Stop();
			m_timer.Interval = 1000 / m_trackFrequency.Value;
			m_timer.Start();
		}

		private void m_trackBuffer_ValueChanged(object sender, EventArgs e)
		{
			m_lblBuffer.Text = "Buffer: (" + m_trackBuffer.Value + ")";
		}

		private void m_trackBar_ValueChanged(object sender, EventArgs e)
		{
			ColorHSL hsv = new ColorHSL(Color.Red);
			hsv.Luminosity = m_trackBar.Value;
			m_picCurrent.BackColor = hsv;
		}
	}
}
