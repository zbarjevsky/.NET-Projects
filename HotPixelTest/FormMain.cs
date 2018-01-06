using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Drawing.Drawing2D;
using System.Collections;

namespace HotPixelTest
{
	public partial class FormMain : Form
	{
		private Bitmap m_bmpFile;
		private bool m_bStop = false;

		public FormMain()
		{
			InitializeComponent();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			LoadFromRegistry();
		}

		private void Clear()
		{
			m_pictureControl.Clear();
			m_toolStripProgressBar1.Value = 0;
			m_listDeadPixels.Items.Clear();
			m_toolStripStatusLabel1.Text = "Ready";
		}

		private void EnableControls(bool bEnable)
		{
			m_btnStart.Enabled = bEnable;
			m_btnTest.Enabled = bEnable;
			m_btnOpen.Enabled = bEnable;
			m_btnSave.Enabled = bEnable;
			m_numMinimum.Enabled = bEnable;
			
			m_btnStop.Enabled = !bEnable;
			m_bStop = !m_btnStop.Enabled;
		}

		const string REG_PATH = @"Software\MarkZ\HotPixelTest";
		const string REG_KEY_FILE = "File";
		const string REG_KEY_THRESOLD = "Threshold";
		const string REG_KEY_ZOOM = "Zoom";
		const string REG_KEY_PIXEL = "Pixel";

		private void PersistValues()
		{
			try
			{
				PersistValue(REG_KEY_FILE, m_txtFileName.Text);
				PersistValue(REG_KEY_THRESOLD, m_numMinimum.Value.ToString());
				PersistValue(REG_KEY_ZOOM, m_numZoom.Value.ToString());
				PersistValue(REG_KEY_PIXEL, m_chkShowPixel.Checked.ToString());
			}
			catch (Exception err)
			{
				MessageBox.Show(this, err.Message, this.Text + " - Save to Registry Error",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void LoadFromRegistry()
		{
			try
			{
				m_txtFileName.Text = LoadFromRegistry(REG_KEY_FILE, string.Empty);
				m_numMinimum.Value = Convert.ToInt32(LoadFromRegistry(REG_KEY_THRESOLD, "25"));
				m_numZoom.Value = Convert.ToInt32(LoadFromRegistry(REG_KEY_ZOOM, "15"));
				m_chkShowPixel.Checked = Convert.ToBoolean(LoadFromRegistry(REG_KEY_PIXEL, "true"));
			}
			catch (Exception err)
			{
				MessageBox.Show(this, err.Message, this.Text + " - Load from Registry Error",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void PersistValue(string key, string value)
		{
			RegistryKey reg = Registry.CurrentUser.OpenSubKey(REG_PATH, true);
			if (reg == null)
				reg = Registry.CurrentUser.CreateSubKey(REG_PATH);

			reg.SetValue(key, value);
		}

		private string LoadFromRegistry(string key, string sDefault)
		{
			RegistryKey reg = Registry.CurrentUser.OpenSubKey(REG_PATH);
			if (reg == null)
				return sDefault;
			return reg.GetValue(key, sDefault).ToString();
		}

		private void Browse()
		{
			m_openFileDialog.FileName = m_txtFileName.Text;
			if (DialogResult.OK != m_openFileDialog.ShowDialog(this))
				return;
			
			Clear();
			m_txtFileName.Text = m_openFileDialog.FileName;
		}

		private void Test()
		{
			Clear();
			if (!File.Exists(m_txtFileName.Text))
				return;

			EnableControls(false);
			PersistValues();

			int count = 0, max = 0;
			int threshold = (int)m_numMinimum.Value;

			if (m_bmpFile != null)
				m_bmpFile.Dispose();

			m_bmpFile = new Bitmap(m_txtFileName.Text, false);
			LoadExifInfo(m_bmpFile);

			m_toolStripProgressBar1.Maximum = m_bmpFile.Width;
			m_toolStripProgressBar1.Minimum = 0;
			int width = m_bmpFile.Width;
			int height = m_bmpFile.Height;
			for (int x = 0; x < width; x++)
			{
				if (x % 20 == 0)
				{
					m_toolStripProgressBar1.Value = x;
					m_toolStripStatusLabel1.Text = (1 + 100 * x / width) + "%";
					Application.DoEvents();
				}

				for (int y = 0; y < height; y++)
				{
					Color c = m_bmpFile.GetPixel(x, y);
					int b = (c.R + c.G + c.B) / 3;
					if (b > threshold)
					{
						ListViewItem itm = m_listDeadPixels.Items.Add(count.ToString());
						itm.SubItems.Add(x.ToString());
						itm.SubItems.Add(y.ToString());
						itm.SubItems.Add(b.ToString());
						itm.SubItems.Add(c.R.ToString());
						itm.SubItems.Add(c.G.ToString());
						itm.SubItems.Add(c.B.ToString());

						count++;
					}
					if (b > max) max = b;
				}

				if (count > 1000)
				{
					MessageBox.Show(this, 
						string.Format("More than 1000 hot pixels,\nShowing first {0}...", count), 
						this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					break;
				}
				if (m_bStop) break;
			}

			string sStatus = string.Format("Hot pixels found: {0}, Max Luminance: {1}", count, max);
			System.Diagnostics.Trace.WriteLine(sStatus);
			MessageBox.Show(this, sStatus, this.Text, 
				MessageBoxButtons.OK, MessageBoxIcon.Information);

			if (!m_bStop) //if was stopped - do not reset progress
			{
				m_toolStripProgressBar1.Value = 0;
				m_toolStripStatusLabel1.Text = sStatus;
			}
			EnableControls(true);
		}

		private void LoadExifInfo(Bitmap m_bmpFile)
		{
			m_listExif.Items.Clear();
			Exif.ExifExtractor exif = new Exif.ExifExtractor(ref m_bmpFile, "");
			AddExifInfo(exif, "Equip Make");
			AddExifInfo(exif, "Equip Model");
			AddExifInfo(exif, "Date Time");
			AddExifInfo(exif, "Exposure Time");
			AddExifInfo(exif, "Shutter Speed");
			AddExifInfo(exif, "F-Number");
			AddExifInfo(exif, "Aperture");
			AddExifInfo(exif, "ISO Speed");
			m_listExif.Items.Add("---------------------------------------------");

			foreach (var item in exif)
			{
				m_listExif.Items.Add(item.ToString());
			}
		}

		private void AddExifInfo(Exif.ExifExtractor exif, string tag)
		{
			ListViewItem i = m_listExif.Items.Add(tag);
			i.SubItems.Add(exif[tag]);
		}

		private void SaveAs()
		{
			string sFileName = Path.GetFileNameWithoutExtension(m_txtFileName.Text);
			m_saveFileDialog.FileName = sFileName;
			if (DialogResult.OK != m_saveFileDialog.ShowDialog(this))
				return;

			sFileName = m_saveFileDialog.FileName;

			StringBuilder sb = new StringBuilder(512);
			sb.AppendLine("idx, x, y, Light, R, G, B");
			foreach (ListViewItem item in m_listDeadPixels.Items)
			{
				foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
				{
					sb.Append(subitem.Text + ", ");
				}
				sb.AppendLine();
			}

			File.WriteAllText(sFileName, sb.ToString());
		}

		private void CreateSumbnail()
		{
			if (m_listDeadPixels.SelectedItems == null || m_listDeadPixels.SelectedItems.Count == 0)
			{
				m_pictureControl.Clear();
				return;
			}

			ListViewItem itm = m_listDeadPixels.SelectedItems[0];
			int x = Convert.ToInt32(itm.SubItems[1].Text);
			int y = Convert.ToInt32(itm.SubItems[2].Text);

			m_pictureControl.SetPicture(m_bmpFile, x, y, (int)m_numZoom.Value, m_chkShowPixel.Checked);
		}

		private void m_listDeadPixels_SelectedIndexChanged(object sender, EventArgs e)
		{
			CreateSumbnail();
		}

		private void FormMain_SizeChanged(object sender, EventArgs e)
		{
			CreateSumbnail();
		}

		private void m_numZoom_ValueChanged(object sender, EventArgs e)
		{
			CreateSumbnail();
		}

		private void m_chkShowPixel_CheckedChanged(object sender, EventArgs e)
		{
			CreateSumbnail();
		}

		private void m_btnAbout_Click(object sender, EventArgs e)
		{
			FormAbout frm = new FormAbout();
			frm.ShowDialog(this);
		}

		private void m_numMinimum_ValueChanged(object sender, EventArgs e)
		{
			Clear();
		}

		private void m_btnOpen_Click(object sender, EventArgs e)
		{
			Browse();
		}

		private void m_btnStart_Click(object sender, EventArgs e)
		{
			Test();
		}

		private void m_btnSave_Click(object sender, EventArgs e)
		{
			SaveAs();
		}

		private void m_btnTest_Click(object sender, EventArgs e)
		{
			Test();
		}

		private void m_btnStop_Click(object sender, EventArgs e)
		{
			m_bStop = true;
		}
	}
}
