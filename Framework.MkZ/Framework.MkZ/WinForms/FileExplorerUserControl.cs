using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;


using MkZ.WPF.MessageBox;
using MkZ.Tools;
using MkZ.Windows;

namespace MkZ.WinForms
{
    public partial class FileExplorerUserControl : UserControl
    {
		public const string PARENT_FOLDER_TEXT = "[..]";

		public class FileData : ListViewItem
		{
			public string FullPath { get; private set; }
			public string FileName { get; private set; }
			public bool IsDirectory { get; private set; }
			public FileInfo FileInfo { get; private set; }
			private DateTime _createTime = DateTime.MinValue;
			public DateTime CreatedTime { get { return GetCreateTime(); } }

            private DateTime _modifiedTime = DateTime.MinValue;
			public DateTime ModifiedTime { get { return GetModifiedTime(); } }

			private long _fileLength = -1;
            public long FileSize { get { return GetFileLength(); } }

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

				//init listViewItem
				this.SubItems.AddRange(new string[] { "", "", "", ""});
			}

			//for special folder name 'up'
			public void SetName(string name)
			{
				FileName = name;
			}

			private long GetFileLength()
			{
				if (!IsDirectory && _fileLength < 0)
					_fileLength = FileInfo.Length;
				return _fileLength;
			}

			private DateTime GetCreateTime()
			{
				if (_createTime == DateTime.MinValue)
				{
					//check if file is in local current day light saving time
					if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(FileInfo.CreationTime) == false)
					{
						//not in day light saving time adjust time
						_createTime = FileInfo.CreationTime.AddHours(1);
					}
					else
					{
						//is in day light saving time adjust time
						_createTime = FileInfo.CreationTime;
					}
				}
				return _createTime;
			}

			private DateTime GetModifiedTime()
			{
				if (_modifiedTime == DateTime.MinValue)
				{
					//check if file is in local current day light saving time
					if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(FileInfo.LastWriteTime) == false)
					{
						//not in day light saving time adjust time
						_modifiedTime = FileInfo.LastWriteTime.AddHours(1);
					}
					else
					{
						//not in day light saving time adjust time
						_modifiedTime = FileInfo.LastWriteTime;
					}
				}
				return _modifiedTime;
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
					if (this.SubItems[0].Text == PARENT_FOLDER_TEXT)
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
				if (file == null)
					return;

				Control parent = file.ListView;
				CommonUtils.ExecuteOnUIThread(() =>
				{
					file.Init(e.FullPath, file.Checked);
					SortList();
					OnChangeAction(e.FullPath);
				}, parent);
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

		public class SelectedFoldersAndFilesList
		{
			public CheckState State { get; set; } = CheckState.Indeterminate;
			public string FolderSrc { get; set; }
			public List<string> Names { get; set; } = new List<string>();

			//for serialization
			public SelectedFoldersAndFilesList() { }

			public SelectedFoldersAndFilesList(string folderSrc) { FolderSrc = folderSrc; }
			public SelectedFoldersAndFilesList(SelectedFoldersAndFilesList selection) 
			{ 
				FolderSrc = selection.FolderSrc;
				State = selection.State;
				Names = new List<string>(selection.Names);
			}

			public bool IsSelected(string name)
            {
				if (State != CheckState.Indeterminate)
					return State.IsChecked();

				return Names.FirstOrDefault((s) => s == name) != null;
            }

			public override string ToString()
			{
				string selItems = State == CheckState.Indeterminate ? Names.Count + " Item(s)" : "All Items " + State;
				return selItems;
			}

			public override bool Equals(object obj)
			{
				return obj is SelectedFoldersAndFilesList list &&
					   FolderSrc == list.FolderSrc &&
					   Names.SequenceEqual(list.Names);
			}

			public override int GetHashCode()
			{
				int hashCode = FolderSrc.GetHashCode();
				hashCode = hashCode * Names.GetHashCode();
				return hashCode;
			}

			public static bool operator ==(SelectedFoldersAndFilesList o1, SelectedFoldersAndFilesList o2)
			{
				if (object.ReferenceEquals(o1, null))
				{
					return object.ReferenceEquals(o2, null);
				}

				return o1.Equals(o2);
			}

			public static bool operator !=(SelectedFoldersAndFilesList o1, SelectedFoldersAndFilesList o2)
			{
				if (object.ReferenceEquals(o1, null))
				{
					return !object.ReferenceEquals(o2, null);
				}

				return !o1.Equals(o2);
			}

			public SelectedFoldersAndFilesList Clone()
			{
				return new SelectedFoldersAndFilesList(FolderSrc)
				{
					Names = new List<string>(this.Names)
				};
			}
		}

		private readonly List<FileData> _list = new List<FileData>();
		private readonly SelectedFoldersAndFilesList _checkedItems = new SelectedFoldersAndFilesList("");
		private readonly FileSystemWatcherHelper _fileSystemWatcherHelper;

		public Action<string> OpenFolderAction = (fullPath) => { };
		public Action<CheckState> CheckedChangedAction = (checkAllState) => { };

		public bool CheckBoxes { get { return m_listFiles.CheckBoxes; } set { m_listFiles.CheckBoxes = value; } }

		public CheckState GetCheckState() 
		{
			if (!m_listFiles.CheckBoxes)
				return CheckState.Indeterminate;

			bool areAllChecked = true;
			bool areAllUnchecked = true;
			for (int i = 1; i < _list.Count; i++)
			{
				areAllChecked &= _list[i].Checked;
				areAllUnchecked &= !_list[i].Checked;
			}

			if (areAllChecked)
				return CheckState.Checked;
			
			if (areAllUnchecked)
				return CheckState.Unchecked;

			return CheckState.Indeterminate;	
		}

		public SelectedFoldersAndFilesList GetCheckedFiles() 
		{
			SelectedFoldersAndFilesList selection = new SelectedFoldersAndFilesList(m_txtPath.Text);
			if (!CheckBoxes)
				return selection;

			selection.State = GetCheckState();
			if (selection.State != CheckState.Indeterminate)
				return selection;

			foreach (FileData data in _list)
			{
				if(data.SubItems[0].Text != PARENT_FOLDER_TEXT && data.Checked)
					selection.Names.Add(data.FileName);
			}

			return selection;
		}

		public void SetCheckedFiles(SelectedFoldersAndFilesList selection)
		{
			if (!CheckBoxes)
				return;

			if(selection.State != CheckState.Indeterminate)
			{
				SetCheckedForAll(selection.State.IsChecked());
			}
			else //set checks individually
			{
				foreach (string file in selection.Names)
				{
					SetCheckedFile(file, true);
				}
			}
			m_listFiles.Refresh();
		}

		public void SetCheckedForAll(bool isChecked)
        {
			if (_list.Count > 512)
				this.Cursor = Cursors.WaitCursor;

			_list.ForEach((item) => item.Checked = isChecked);
			_checkedItems.Names.Clear();

			this.Cursor = Cursors.Default;
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
				CommonUtils.ExecuteOnUIThread(() => 
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
			_checkedItems.FolderSrc = m_txtPath.Text;

			m_listFiles.VirtualListSize = 0;
			_list.Clear();

			errorProvider1.SetError(m_txtPath, "");

			EnableMenu(false);
		}

		private void m_listFiles_ItemCheck(object sender, ItemCheckEventArgs e)
		{

		}

		private void m_listFiles_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			//check/uncheck all - if first was checked/unchecked
			if (e.Item.Text == PARENT_FOLDER_TEXT)
			{
				SetCheckedForAll(e.Item.Checked);
				m_listFiles.Invalidate();
				CheckedChangedAction(e.Item.Checked.CheckState());
			}
			else
			{
				_list[0].Checked = GetCheckState().IsChecked();
				m_listFiles.Invalidate(_list[0].Bounds);
				CheckedChangedAction(CheckState.Indeterminate);
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
			this.MessageInfo("Not Implemented");
		}

		public void PopulateFiles(string fullPath, SelectedFoldersAndFilesList selection = null)
		{
			if (m_txtPath.Text == fullPath)
				return; //no change

			//clear list
			InitListView();

			if (selection == null)
				selection = new SelectedFoldersAndFilesList(fullPath) { State = CheckState.Unchecked };

			m_txtPath.Text = fullPath;
			_checkedItems.FolderSrc = m_txtPath.Text;

			//check path
			if (!string.IsNullOrEmpty(fullPath) && Directory.Exists(fullPath))
			{
				try
				{
					string parentFolder = Path.GetDirectoryName(fullPath);
					if (Directory.Exists(parentFolder))
					{
						FileData parent = new FileData(parentFolder, selection.State.IsChecked());
						parent.SetName(PARENT_FOLDER_TEXT);
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
						_list.Add(new FileData(folder, true, selection.IsSelected(folder)));
					}

					string[] stringFiles = Directory.GetFiles(fullPath);
					List<string> files = stringFiles.OrderBy(s => s).ToList();

					int pageSize = VisibleItemsCount();

					//loop throught all files
					for (int i = 0; i < files.Count; i++)
					{
						_list.Add(new FileData(files[i], false, selection.IsSelected(files[i])));

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
					_checkedItems.FolderSrc = m_txtPath.Text;
					OpenFolderAction(fullPath);
				}
				catch (IOException e)
				{
					this.MessageError("Error: Drive not ready or directory does not exist."+e.Message, "FileExplorerUserControl");
					errorProvider1.SetError(m_txtPath, e.Message);
				}
				catch (UnauthorizedAccessException e)
				{
					this.MessageError("Error: Drive or directory access denided." + e.Message, "FileExplorerUserControl");
					errorProvider1.SetError(m_txtPath, e.Message);
				}
				catch (Exception e)
				{
					this.MessageError("Error: " + e, "FileExplorerUserControl");
					errorProvider1.SetError(m_txtPath, e.Message);
				}
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

        private void m_mnuSelect_Click(object sender, EventArgs e)
        {
			if (m_listFiles.SelectedIndices.Count == 0)
				return;

			FileData data = _list[m_listFiles.SelectedIndices[0]];
			data.Checked = true;
			m_listFiles.Invalidate(data.Bounds);
		}

		private void m_mnuRefresh_Click(object sender, EventArgs e)
        {
			string path = m_txtPath.Text;
			m_txtPath.Text = "";
			PopulateFiles(path);
		}

        private void m_mnuRename_Click(object sender, EventArgs e)
        {
			this.MessageInfo("Not Implemented");
        }

        private void m_mnuDelete_Click(object sender, EventArgs e)
        {
			if (m_listFiles.SelectedIndices.Count == 0)
				return;

			FileData data = _list[m_listFiles.SelectedIndices[0]];
			data.Checked = false;
			if(data.IsDirectory)
            {
				string dir = data.FileInfo.FullName;
				Task.Factory.StartNew(() => { FileUtils.DeleteDirectoryWithSystemProgressDialog(dir); });
            }
            else
            {
				var res = this.MessageQuestion("Delete: " + data.FileName + "?", "Delete");
				if (res == PopUp.PopUpResult.OK)
				{
					data.FileInfo.Delete();
				}
			}
		}

        private void m_listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
			EnableMenu(m_listFiles.SelectedIndices.Count > 0);
		}

		private void EnableMenu(bool bEnable)
        {
			m_mnuSelect.Visible = CheckBoxes;
			m_mnuSeparator1.Visible = CheckBoxes;

			m_mnuSelect.Enabled = bEnable;
			m_mnuRename.Enabled = bEnable;
			m_mnuDelete.Enabled = bEnable;
			m_mnuRefresh.Enabled = true;
		}
	}

	public static class CheckStateExtension
    {
		public static bool IsChecked(this CheckState state)
        {
			return state == System.Windows.Forms.CheckState.Checked;
        }

		public static CheckState CheckState(this bool check)
        {
			return check ? System.Windows.Forms.CheckState.Checked : System.Windows.Forms.CheckState.Unchecked;
        }
    }
}
