using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace MZ.ControlsWinForms
{
    public partial class FileExplorerUserControl : UserControl
    {
		private class FileData
		{
			public string FullPath { get; }
			public string FileName { get; }
			public bool IsDirectory { get; }
			public FileInfo FileInfo { get; }
			public DateTime CreatedTime { get; }
			public DateTime ModifiedTime { get; }
			public long FileSize { get; }

			public FileData(string fullPath, bool isDirectory)
			{
				FullPath = fullPath;
				IsDirectory = isDirectory;
				FileName = Path.GetFileName(fullPath);

				FileInfo = new FileInfo(fullPath);
				if(!IsDirectory)
					FileSize = FileInfo.Length;

				//check if file is in local current day light saving time
				if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(FileInfo.CreationTime) == false)
				{
					//not in day light saving time adjust time
					CreatedTime = FileInfo.CreationTime.AddHours(1);
				}
				else
				{
					//is in day light saving time adjust time
					CreatedTime = FileInfo.CreationTime;
				}

				//check if file is in local current day light saving time
				if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(FileInfo.LastWriteTime) == false)
				{
					//not in day light saving time adjust time
					ModifiedTime = FileInfo.LastWriteTime.AddHours(1);
				}
				else
				{
					//not in day light saving time adjust time
					ModifiedTime = FileInfo.LastWriteTime;
				}
			}
		}

		private List<FileData> _list = new List<FileData>();

        public FileExplorerUserControl()
        {
            InitializeComponent();
        }

        private void FileExplorerUserControl_Load(object sender, EventArgs e)
        {
            InitListView();
        }

        protected void InitListView()
        {
            //init ListView control
            m_listFiles.Clear();    //clear control
			_list.Clear();
			m_listFiles.VirtualListSize = 0;

									//create column header for ListView
			m_listFiles.Columns.Add("Name", 150, System.Windows.Forms.HorizontalAlignment.Left);
            m_listFiles.Columns.Add("Size", 75, System.Windows.Forms.HorizontalAlignment.Right);
            m_listFiles.Columns.Add("Created", 140, System.Windows.Forms.HorizontalAlignment.Left);
            m_listFiles.Columns.Add("Modified", 140, System.Windows.Forms.HorizontalAlignment.Left);
        }

		public void ShowFolder(string fullPath)
		{
			PopulateFiles(fullPath);
		}

		private void m_btnRoot_Click(object sender, EventArgs e)
		{
			string root = Path.GetPathRoot(m_txtPath.Text);
			if (Directory.Exists(root))
				PopulateFiles(root);
		}

		private void m_btnBrowse_Click(object sender, EventArgs e)
		{
			string parentFolder = Path.GetDirectoryName(m_txtPath.Text);
			if(Directory.Exists(parentFolder))
				PopulateFiles(parentFolder);
		}

		protected void PopulateFiles(string fullPath)
		{
			m_txtPath.Text = fullPath;

			//Populate listview with files
			string[] lvData = new string[4];

			//clear list
			InitListView();

			//check path
			if (Directory.Exists(fullPath) == false)
			{
				MessageBox.Show("Directory or path " + fullPath.ToString() + " does not exist.");
			}
			else
			{
				try
				{
					string parentFolder = Path.GetDirectoryName(fullPath);
					if (Directory.Exists(parentFolder))
					{
						_list.Add(new FileData(parentFolder, true));
						m_btnUp.Enabled = true;
					}
					else
					{
						m_btnUp.Enabled = false;
					}

					string[] folders = Directory.GetDirectories(fullPath);
					foreach (string folder in folders)
					{
						_list.Add(new FileData(folder, true));
					}

					string[] stringFiles = Directory.GetFiles(fullPath);

					//loop throught all files
					foreach (string stringFile in stringFiles)
					{
						_list.Add(new FileData(stringFile, false));

						//Icon ico = Icon.ExtractAssociatedIcon(stringFile);
						//int iconIndex = m_imageListTreeView.Images.Count;
						//m_imageListTreeView.Images.Add(ico);
					}

					m_listFiles.VirtualListSize = _list.Count;
				}
				catch (IOException e)
				{
					MessageBox.Show("Error: Drive not ready or directory does not exist."+e.Message);
				}
				catch (UnauthorizedAccessException e)
				{
					MessageBox.Show("Error: Drive or directory access denided." + e.Message);
				}
				catch (Exception e)
				{
					MessageBox.Show("Error: " + e);
				}
			}
		}

		protected string formatDate(DateTime dtDate)
		{
			//Get date and time in short format
			string stringDate = "";

			stringDate = dtDate.ToShortDateString().ToString() + " " + dtDate.ToShortTimeString().ToString();

			return stringDate;
		}

		protected string formatSize(Int64 lSize)
		{
			//Format number to KB
			string stringSize = "";
			NumberFormatInfo myNfi = new NumberFormatInfo();

			Int64 lKBSize = 0;

			if (lSize < 1024)
			{
				if (lSize == 0)
				{
					//zero byte
					stringSize = "0";
				}
				else
				{
					//less than 1K but not zero byte
					stringSize = "1";
				}
			}
			else
			{
				//convert to KB
				lKBSize = lSize / 1024;
				//format number with default format
				stringSize = lKBSize.ToString("n", myNfi);
				//remove decimal
				stringSize = stringSize.Replace(".00", "");
			}

			return stringSize + " KB";

		}

		private void m_listFiles_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (m_listFiles.SelectedIndices.Count == 0)
				return;

			FileData data = _list[m_listFiles.SelectedIndices[0]];
			if (!data.IsDirectory)
				return;

			if(Directory.Exists(data.FullPath))
				PopulateFiles(data.FullPath);
		}

		private void m_listFiles_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			string[] lvData = new string[4];
			FileData data = _list[e.ItemIndex];

			lvData[0] = data.FileName;
			if(!data.IsDirectory)
				lvData[1] = formatSize(data.FileSize);
			lvData[2] = formatDate(data.CreatedTime);
			lvData[3] = formatDate(data.ModifiedTime);

			int iconIndex = data.IsDirectory ? 2 : 4;
			ListViewItem lvItem = new ListViewItem(lvData, iconIndex);
			e.Item = lvItem;
		}
	}
}
