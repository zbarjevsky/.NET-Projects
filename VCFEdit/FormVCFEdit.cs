using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VCFEdit
{
	public partial class FormVCFEdit : Form
	{
		public FormVCFEdit()
		{
			InitializeComponent();
		}

		private void FormVCFEdit_Load(object sender, EventArgs e)
		{

		}

		private void m_mnuFileOpen_Click(object sender, EventArgs e)
		{
			OpenFiles(true);
		}

		private void m_tbbtnAppend_Click(object sender, EventArgs e)
		{
			OpenFiles(false);
		}

		private void OpenFiles(bool bClear)
		{
			if (DialogResult.OK != m_openFileDialog1.ShowDialog(this))
				return;

			if (bClear)
			{
				m_treeContacts.Nodes.Clear();
			}

			int total = m_treeContacts.Nodes.Count;
			m_txtContactData.Text = string.Empty;

			string[] svFiles = m_openFileDialog1.FileNames;
			foreach (string sFileName in svFiles)
			{
				AppendFile(sFileName, ref total);
			}
			m_txtContactData.Text = string.Empty;
		}

		private void AppendFile(string sFileName, ref int total)
		{
			string[] lines = File.ReadAllLines(sFileName);
			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];
				if (line.StartsWith("BEGIN:VCARD"))
				{
					total++;
					Contact c = new Contact(ref i, lines);
					if (c.HasData() && !Exists(c))
						AddContact(c);
				}
			}
			m_toolStripStatusLabel1.Text = string.Format("Loaded {0} contacts, Total: {1}",
				m_treeContacts.Nodes.Count, total);
		}

		private TreeNode AddContact(Contact c)
		{
			if (c.HasData() && !Exists(c))
			{
				TreeNode n = m_treeContacts.Nodes.Add(c.ToString());
				n.Tag = c;
				n.ImageIndex = 1;
				n.SelectedImageIndex = 5;
				n.Nodes.Add("N: " + c.Name);
				n.Nodes.Add("FN: " + c.FullName);
				foreach (var item in c.Numbers)
				{
					n.ImageIndex = 3;
					n.Nodes.Add("Num: " + item.Number);
				}
				foreach (var item in c.Mails)
				{
					n.Nodes.Add("Num: " + item.Mail);
				}
				return n;
			}
			return null;
		}

		private void m_mnuFileSave_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK != m_saveFileDialog1.ShowDialog(this))
				return;

			int count = 0;
			string sFileName = m_saveFileDialog1.FileName;
			List<string> lines = new List<string>();
			foreach (TreeNode n in m_treeContacts.Nodes)
			{
				if (!n.Checked) continue;

				Contact c = n.Tag as Contact;
				if (c != null)
				{
					count++;
					lines.AddRange(c.OriginalText);
				}
			}
			File.WriteAllLines(sFileName, lines);
			m_toolStripStatusLabel1.Text = string.Format("Saved {0} contacts, Lines: {1}",
				count, lines.Count);
		}

		private void m_mnuFileClose_Click(object sender, EventArgs e)
		{

		}

		private void m_mnuFileExit_Click(object sender, EventArgs e)
		{

		}

		private bool Browse()
		{
			//m_openFileDialog1.FileName = m_txtFileName.Text;
			if (DialogResult.OK != m_openFileDialog1.ShowDialog(this))
				return false;
			//m_txtFileName.Text = m_openFileDialog1.FileName;
			return true;
		}

		private void m_tbbtnSelectAll_Click(object sender, EventArgs e)
		{
			foreach (TreeNode n in m_treeContacts.Nodes)
			{
				n.Checked = m_tbbtnSelectAll.Checked;
			}
		}

		private void m_treeContacts_AfterSelect(object sender, TreeViewEventArgs e)
		{
			m_txtContactData.Text = string.Empty;
			if (m_treeContacts.SelectedNode == null || m_treeContacts.SelectedNode.Tag == null)
				return;

			Contact c = m_treeContacts.SelectedNode.Tag as Contact;
			m_txtContactData.Text = c.TextExport();
			m_Picture.Image = c.Photo;
		}

		private bool Exists(Contact c1)
		{
			foreach (TreeNode n in m_treeContacts.Nodes)
			{
				Contact c2 = n.Tag as Contact;
				if (c2.Equals(c1))
					return true;
			}
			return false;
		}

		private void m_txtContactData_TextChanged(object sender, EventArgs e)
		{
			if (m_treeContacts.SelectedNode == null || m_treeContacts.SelectedNode.Tag == null 
				|| string.IsNullOrEmpty(m_txtContactData.Text.Trim()))
				return;

			Contact c = m_treeContacts.SelectedNode.Tag as Contact;
			bool bValid = c.TextLoad(m_txtContactData.Text);
			m_treeContacts.SelectedNode.Text = c.ToString();

			m_toolStripStatusLabel2.Text = bValid ? "Contact is Valid" : "Contact is not Valid";
			m_toolStripStatusLabel2.Image = bValid ? m_imageList1.Images[2] : m_imageList1.Images[3];
			//m_errorProvider1.SetError(m_statusStripMain, m_toolStripStatusLabel2.Text);
		}

		private void m_tbbtnDeleteChecked_Click(object sender, EventArgs e)
		{
			for (int i = m_treeContacts.Nodes.Count - 1; i >= 0; i--)
			{
				if (m_treeContacts.Nodes[i].Checked)
					m_treeContacts.Nodes[i].Remove();
			}
			m_toolStripStatusLabel1.Text = string.Format("{0} Contacts", m_treeContacts.Nodes.Count);
		}

		private void m_tbbtnAddContact_Click(object sender, EventArgs e)
		{
			Contact c = new Contact();
			TreeNode n = AddContact(c);
			if (n != null) m_treeContacts.SelectedNode = n;
		}
	}
}
