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
using System.Windows.Documents;

namespace MZ.WinForms
{
    public partial class FileExplorerUserControl : UserControl
    {
		public const string ParentFolderText = "[..]";

		public class FileData : ListViewItem
		{
			public string FullPath { get; private set; }
			public string FileName { get; private set; }
			public bool IsDirectory { get; private set; }
			public FileInfo FileInfo { get; private set; }
			public DateTime CreatedTime { get; private set; }
			public DateTime ModifiedTime { get; private set; }
			public long FileSize { get; private set; }

			public FileData(string fullPath, bool isCheck)
			{
				Init(fullPath, isCheck);
			}

			public FileData(string fullPath, bool isDirectory, bool isCheck)
			{
				Init(fullPath, isDirectory, isCheck);
			}

			public void Init(string fullPath, bool isCheck)
			{
				Init(fullPath, Directory.Exists(fullPath), isCheck);
			}

			private void Init(string fullPath, bool isDirectory, bool isCheck)
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

				//init listViewItem
				this.SubItems.AddRange(new string[] { "", "", "", ""});
			}

			//for special folder name 'up'
			internal void SetName(string name)
			{
				FileName = name;
			}

			public void PrepareListViewItem()
			{
				try
				{
					this.SubItems[0].Text = this.FileName;
					if (!this.IsDirectory)
						this.SubItems[1].Text = formatSize(this.FileSize);
					this.SubItems[2].Text = formatDate(this.CreatedTime);
					this.SubItems[3].Text = formatDate(this.ModifiedTime);

					int iconIndex = this.IsDirectory ? 2 : 4;
					if (this.SubItems[0].Text == ParentFolderText)
						iconIndex = 10;
					this.ImageIndex = iconIndex;
				}
				catch (Exception err)
				{
					Text = err.Message;
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
		}

		private class FileSystemWatcherHelper : IDisposable
		{
			private FileSystemWatcher _fileSystemWatcher;
			private readonly List<FileData> _list;

			public Action<string> OnChangeAction = (fullPath) => { };

			public FileSystemWatcherHelper(List<FileData> list)
			{
				_list = list;
			}

			public void StartWatching(string fullPath)
			{
				Dispose();

				_fileSystemWatcher = new FileSystemWatcher(fullPath);

				_fileSystemWatcher.Created += OnFileCreated;
				_fileSystemWatcher.Renamed += OnFileRenamed;
				_fileSystemWatcher.Deleted += OnFileDeleted;

				_fileSystemWatcher.EnableRaisingEvents = true;
			}

			public void Dispose()
			{
				if (_fileSystemWatcher != null)
				{
					_fileSystemWatcher.Created -= OnFileCreated;
					_fileSystemWatcher.Renamed -= OnFileRenamed;
					_fileSystemWatcher.Deleted -= OnFileDeleted;

					_fileSystemWatcher.EnableRaisingEvents = false;
					_fileSystemWatcher = null;
				}
			}

			private void OnFileDeleted(object sender, FileSystemEventArgs e)
			{
				FileData file = _list.FirstOrDefault(f => f.FullPath == e.FullPath);
				if (file != null)
				{
					_list.Remove(file);
					OnChangeAction(e.FullPath);
				}
			}

			private void OnFileRenamed(object sender, RenamedEventArgs e)
			{
				FileData file = _list.FirstOrDefault(f => f.FullPath == e.OldFullPath);
				if (file != null)
				{
					file.Init(e.FullPath, file.Checked);
					SortList();
					OnChangeAction(e.FullPath);
				}
			}

			private void OnFileCreated(object sender, FileSystemEventArgs e)
			{
				_list.Add(new FileData(e.FullPath, true));
				SortList();
				OnChangeAction(e.FullPath);
			}

			private void SortList() //folders first
			{
				List<FileData> folders = _list.Where(f => f.IsDirectory).ToList();
				folders.Sort((f1, f2) => string.Compare(f1.FileName, f2.FileName, true));
				
				List<FileData> files = _list.Where(f => !f.IsDirectory).ToList();
				files.Sort((f1, f2) => string.Compare(f1.FileName, f2.FileName, true));

				_list.Clear();
				_list.AddRange(folders);
				_list.AddRange(files);
			}
		}

		private readonly List<FileData> _list = new List<FileData>();
		private readonly FileSystemWatcherHelper _fileSystemWatcherHelper;

		public Action<string> OpenFolderAction = (fullPath) => { };

		public bool CheckBoxes { get { return m_listFiles.CheckBoxes; } set { m_listFiles.CheckBoxes = value; } }

		public bool IsAllChecked() 
		{
			if (!m_listFiles.CheckBoxes)
				return false;

			for (int i = 1; i < _list.Count; i++)
			{
				if (!_list[i].Checked)
					return false;
			}
			return true;	
		}

		public List<string> GetCheckedFiles() 
		{
			List<string> list = new List<string>();
			if (!CheckBoxes)
				return list;

			foreach (FileData data in _list)
			{
				if(data.SubItems[0].Text != ParentFolderText && data.Checked)
					list.Add(data.FileName);
			}
			return list;
		}

		public void SetCheckedFiles(List<string> list, bool bCheckAll, bool isChecked = true)
		{
			if (!CheckBoxes)
				return;

			if(bCheckAll)
			{
				if(_list.Count > 512)
					this.Cursor = Cursors.WaitCursor;

				_list.ForEach((item) => item.Checked = isChecked);
				
				this.Cursor = Cursors.Default;
			}
			else
			{
				foreach (string file in list)
				{
					SetCheckedFile(file, isChecked);
				}
			}
			m_listFiles.Refresh();
		}

		public void SetCheckedFile(string file, bool isChecked = true)
		{
			if (!CheckBoxes)
				return;

			FileData data = _list.FirstOrDefault(d => d.FileName == file);
			if (data != null)
				data.Checked = isChecked;
		}

        public FileExplorerUserControl()
        {
            InitializeComponent();

			_fileSystemWatcherHelper = new FileSystemWatcherHelper(_list);
			_fileSystemWatcherHelper.OnChangeAction = (path) =>
			{
				MZ.Tools.CommonUtils.ExecuteOnUIThread(() => 
				{
					m_listFiles.VirtualListSize = _list.Count;
					m_listFiles.Invalidate();
				}, this);
			};
		}

		private void FileExplorerUserControl_Load(object sender, EventArgs e)
        {
            InitListView();
		}

		protected void InitListView()
        {
			m_txtPath.Text = "";

			m_listFiles.VirtualListSize = 0;
			_list.Clear();

			errorProvider1.SetError(m_txtPath, "");
        }

		private void m_listFiles_ItemCheck(object sender, ItemCheckEventArgs e)
		{

		}

		private void m_listFiles_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			//check/uncheck all - if first was checked/unchecked
			if (_list[e.Item.Index].Text == ParentFolderText)
			{
				SetCheckedFiles(null, true, e.Item.Checked);
			}
			else
			{
				_list[0].Checked = IsAllChecked();
				m_listFiles.Invalidate(_list[0].Bounds);
			}
		}

		private void m_btnRoot_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(m_txtPath.Text))
				return;

			string root = Path.GetPathRoot(m_txtPath.Text);
			if (Directory.Exists(root))
				PopulateFiles(root);
		}

		private void m_btnBrowse_Click(object sender, EventArgs e)
		{
			FormBrowseForFolder frm = new FormBrowseForFolder();
			frm.SelectedFolder = m_txtPath.Text;
			frm.Description = "Choose folder: ";

			if (frm.ShowDialog(this) == DialogResult.OK)
			{
				PopulateFiles(frm.SelectedFolder);
			}
		}

		private void m_btnUp_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(m_txtPath.Text))
				return;

			string parentFolder = Path.GetDirectoryName(m_txtPath.Text);
			if(Directory.Exists(parentFolder))
				PopulateFiles(parentFolder);
		}

        private void m_btnNewFolder_Click(object sender, EventArgs e)
        {

        }

		public void PopulateFiles(string fullPath, bool isCheck = false)
		{
			if (m_txtPath.Text == fullPath)
				return; //no change

			//clear list
			InitListView();

			m_txtPath.Text = fullPath;

			//check path
			if (!string.IsNullOrEmpty(fullPath) && Directory.Exists(fullPath))
			{
				try
				{
					string parentFolder = Path.GetDirectoryName(fullPath);
					if (Directory.Exists(parentFolder))
					{
						FileData parent = new FileData(parentFolder, true);
						parent.SetName(ParentFolderText);
						_list.Add(parent);
						m_btnUp.Enabled = true;
					}
					else
					{
						m_btnUp.Enabled = false;
					}

					string[] folders = Directory.GetDirectories(fullPath);
					List<string> dirs = folders.OrderBy(s => s).ToList();
					
					foreach (string folder in dirs)
					{
						_list.Add(new FileData(folder, isCheck));
					}

					string[] stringFiles = Directory.GetFiles(fullPath);
					List<string> files = stringFiles.OrderBy(s => s).ToList();

					int pageSize = VisibleItemsCount();

					//loop throught all files
					for (int i = 0; i < files.Count; i++)
					{
						_list.Add(new FileData(files[i], isCheck));

						if (i == pageSize) //after first page
						{
							//m_listFiles.SuspendLayout();
							m_listFiles.VirtualListSize = _list.Count;
							//m_listFiles.ResumeLayout();
							Application.DoEvents();
						}

						//Icon ico = Icon.ExtractAssociatedIcon(stringFile);
						//int iconIndex = m_imageListTreeView.Images.Count;
						//m_imageListTreeView.Images.Add(ico);
					}

					_fileSystemWatcherHelper.StartWatching(fullPath);

					m_listFiles.VirtualListSize = _list.Count;
					m_txtPath.Text = fullPath;
					OpenFolderAction(fullPath);
				}
				catch (IOException e)
				{
					MessageBox.Show("Error: Drive not ready or directory does not exist."+e.Message);
					errorProvider1.SetError(m_txtPath, e.Message);
				}
				catch (UnauthorizedAccessException e)
				{
					MessageBox.Show("Error: Drive or directory access denided." + e.Message);
					errorProvider1.SetError(m_txtPath, e.Message);
				}
				catch (Exception e)
				{
					MessageBox.Show("Error: " + e);
					errorProvider1.SetError(m_txtPath, e.Message);
				}
			}
            else
            {
				MessageBox.Show("Folder does not exists: \n" + fullPath);
				errorProvider1.SetError(m_txtPath, "Folder does not exists");
			}
		}

		private int VisibleItemsCount()
		{
			return m_listFiles.Height / 18;
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
			_list[e.ItemIndex].PrepareListViewItem();
			e.Item = _list[e.ItemIndex];
		}
    }
}
