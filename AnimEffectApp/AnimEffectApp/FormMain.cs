using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnimEffectApp
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();

			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			AnimEffect eff = new AnimEffect(10, 10);
			eff.ReportProgressAction = (progr) =>
			{
				m_statusProgress.Value = (int)(100.0 * progr);
				Application.DoEvents();
			};

			eff.SetEffect((AnimEffect.EffectType)comboBox1.SelectedItem);
			eff.Play(this.Bounds, m_Color, true);
			eff.Play(this.Bounds, m_Color, false);
		}

		private Color m_Color = Color.DarkBlue;
		private void FormMain_Load(object sender, EventArgs e)
		{
			comboBox1.Items.Add(AnimEffect.EffectType.Random);
			comboBox1.Items.Add(AnimEffect.EffectType.Spin);
			comboBox1.Items.Add(AnimEffect.EffectType.Vortex);
			comboBox1.Items.Add(AnimEffect.EffectType.ScatterGather);
			comboBox1.Items.Add(AnimEffect.EffectType.Spike);
			comboBox1.Items.Add(AnimEffect.EffectType.Fireworks);

			comboBox1.SelectedIndex = 1;

			m_txtColor.Text = m_Color.ToString();
		}

		private void m_btnColor_Click(object sender, EventArgs e)
		{
			colorDialog1.Color = m_Color;
			if (colorDialog1.ShowDialog() != DialogResult.OK)
				return;
			m_Color = colorDialog1.Color;
			m_txtColor.Text = m_Color.ToString();
		}
	}
}