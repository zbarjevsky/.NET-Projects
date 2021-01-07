using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO;
using MkZ.Tools;

namespace MkZ.WinForms
{
    public partial class FoldersTreeUserControl : UserControl
    {
		private const string NODE_PLACEHOLDER = "!!should not see this!!";

		public Action<string> AfterSelectAction = (fullPath) => { };
		public string SelectedFolder { get; private set; }

        public FoldersTreeUserControl()
        {
            InitializeComponent();
        }

        private void FoldersTreeUserControl_Load(object sender, EventArgs e)
        {
			PopulateDriveList();
		}

		//This procedure populate the TreeView with the Drive list
		public void SelectFolder(string fullPath, bool expand)
		{
			fullPath = fullPath.Trim().Trim('"'); //clean path

			TreeNode node = FindTreeNode(fullPath);
			if (node != null)
			{
				SelectedFolder = fullPath;
				tvFolders.SelectedNode = node;
				node.EnsureVisible();
				if (expand)
					node.Expand();
			}
		}

		public bool IsFolderSelected(string fullPath)
        {
			if (string.IsNullOrWhiteSpace(fullPath) || !Directory.Exists(fullPath))
				return false;

			if (string.IsNullOrWhiteSpace(SelectedFolder))
				return false;

			string f1 = SelectedFolder.ToLower().TrimEnd(Path.DirectorySeparatorChar);
			string f2 = fullPath.ToLower().TrimEnd(Path.DirectorySeparatorChar);
			return StringComparer.OrdinalIgnoreCase.Equals(f1, f2);
		}

		public void RefreshFolder(string fullPath)
		{
			TreeNode node = FindTreeNode(fullPath.Trim().Trim('"'));
			if (node != null)
			{
				PopulateDirectory(node);
			}
		}

		public void EditFolder(string fullPath)
		{
			TreeNode node = FindTreeNode(fullPath.Trim().Trim('"'));
			if (node != null)
			{
				node.BeginEdit();
			}
		}

		private TreeNode FindTreeNode(string fullPath)
		{
			if (tvFolders.Nodes.Count == 0)
				return null;
			
			if (string.IsNullOrWhiteSpace(fullPath))
				return tvFolders.Nodes[0]; //root

			for (int i = 0; i < tvFolders.Nodes[0].Nodes.Count; i++) //drives
			{
				if (tvFolders.Nodes[0].Nodes[i].Name == fullPath)
					return tvFolders.Nodes[0].Nodes[i];
			}

			List<string> folders = new List<string>();

			string name = Path.GetFileName(fullPath);
			string path = Path.GetDirectoryName(fullPath);
			folders.Add(name);
			
			while (!string.IsNullOrWhiteSpace(path))
			{
				name = Path.GetFileName(path);
				if (string.IsNullOrWhiteSpace(name))
				{
					folders.Add(path);
					break;
				}

				folders.Add(name);
				path = Path.GetDirectoryName(path);
			}

			folders.Reverse();

			TreeNode node = FindSubNode(tvFolders.Nodes[0], folders);

			return node;
		}

		private TreeNode FindSubNode(TreeNode node, List<string> folders, int index = 0)
		{
			if(node.Nodes.Count == 0 || node.Nodes[0].Name == NODE_PLACEHOLDER)
				PopulateDirectory(node);

			foreach (TreeNode n in node.Nodes)
			{
				if(StringComparer.OrdinalIgnoreCase.Equals(n.Name, folders[index]))
				{
					if (index == folders.Count - 1)
						return n;

					TreeNode found = FindSubNode(n, folders, index + 1);
					if (found == null)
						found = n; //if not found - return closest parent
					return found;
				}
			}
			return null;
		}

		private TreeNode PopulateDriveList()
		{
			int imageIndex = 0;
			int selectIndex = 0;

			this.Cursor = Cursors.WaitCursor;
			
			//clear TreeView
			tvFolders.Nodes.Clear();
			TreeNode root = new TreeNode("My Computer", 0, 0);
			tvFolders.Nodes.Add(root);

			//Get Drive list
			DriveInfo[] drives = System.IO.DriveInfo.GetDrives();

			foreach (DriveInfo drive in drives)
			{

				switch (drive.DriveType)
				{
					case DriveType.Removable:         //removable drives
						imageIndex = 5;
						selectIndex = 5;
						break;
					case DriveType.Fixed:         //Local drives
						imageIndex = 6;
						selectIndex = 6;
						break;
					case DriveType.CDRom:                //CD rom drives
						imageIndex = 7;
						selectIndex = 7;
						break;
					case DriveType.Network:           //Network drives
						imageIndex = 8;
						selectIndex = 8;
						break;
					default:                //defalut to folder
						imageIndex = 2;
						selectIndex = 3;
						break;
				}

				//create new drive node
				string desc = GetDriveDescription(drive);
				TreeNode driveNode = new TreeNode(desc, imageIndex, selectIndex);
				driveNode.Name = drive.Name;
				driveNode.Tag = drive;
				driveNode.Nodes.Add(NODE_PLACEHOLDER, ""); //to add + sign to expand the node

				//add new node
				root.Nodes.Add(driveNode);
			}

			root.Expand();
			this.Cursor = Cursors.Default;

			return root;
		}

		public string GetDriveDescription(DriveInfo drive)
		{
			try
			{
				if (drive.DriveType != DriveType.Network)
				{
					return drive.Name + " [" + drive.VolumeLabel + "] - " + drive.TotalSize.ToString("###,###,##0");
				}
				else
				{
					return drive.Name + " [Network Drive]";
				}
			}
			catch (Exception err)
			{
				System.Diagnostics.Debug.WriteLine("GetDriveList error: (" + drive.Name + ") - " + err.Message);
				return (drive.Name + "<" + err.Message + ">");
			}
		}

		//protected ManagementObjectCollection getDrives()
		//{
		//	//get drive collection
		//	ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * From Win32_LogicalDisk ");
		//	ManagementObjectCollection queryCollection = query.Get();
		//	query.Dispose();

		//	return queryCollection;
		//}

		private void tvFolders_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			//get current selected drive or folder
			try
			{
				if (!string.IsNullOrWhiteSpace(e.Node.Name) && !string.IsNullOrWhiteSpace(e.Label))
				{
					string path = getFullPath(e.Node);
					//string parent = Path.GetDirectoryName(path);
					//string newPath = Path.Combine(parent, e.Label);
					//Directory.Move(path, newPath);
					FileUtils.RenameDirectoryWithSystemProgressDialog(path, e.Label);
					e.Node.Name = e.Label;
					SelectedFolder = getFullPath(e.Node);
					AfterSelectAction(SelectedFolder);
				}
			}
			catch (Exception err)
			{
				e.CancelEdit = true;
				MessageBox.Show(this, err.Message, "Rename Folder");
			}			
		}

		private void tvFolders_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			//notify
			SelectedFolder = getFullPath(e.Node);
			AfterSelectAction(SelectedFolder);
		}

		private void tvFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
			if(e.Node.Nodes.Count == 0 || e.Node.Nodes[0].Name == NODE_PLACEHOLDER)
            {
                //Populate folders and files when a folder is expanded
                this.Cursor = Cursors.WaitCursor;
                
				PopulateDirectory(e.Node);

                this.Cursor = Cursors.Default;
            }
		}

		private void tvFolders_MouseDown(object sender, MouseEventArgs e)
		{
			TreeNode node = tvFolders.GetNodeAt(e.X, e.Y);
			if (node != tvFolders.SelectedNode && e.Button == MouseButtons.Right)
				tvFolders.SelectedNode = node;
		}

		private void tvFolders_MouseDoubleClick(object sender, MouseEventArgs e)
		{

		}

		private bool IsRootNoode(TreeNode node)
		{
			string path = getFullPath(node);
			return string.IsNullOrWhiteSpace(path);
		}

		protected void PopulateDirectory(TreeNode node)
		{
			int imageIndex = 2;     //unselected image index
			int selectIndex = 3;    //selected image index

			//populate treeview with folders
			try
			{
				node.Nodes.Clear();

				if (IsRootNoode(node))
				{
					//Selected My Computer - repopulate drive list
					PopulateDriveList();
				}
				else
				{
					//check path
					if (!Directory.Exists(getFullPath(node)))
					{
						MessageBox.Show("Directory or path " + node.ToString() + " does not exist.");
						return;
					}

					List<string> directories = new List<string>(Directory.GetDirectories(getFullPath(node)));
					directories.Sort();

					//loop throught all directories
					foreach (string stringDir in directories)
					{
						string stringPathName = Path.GetFileName(stringDir);

						//create node for directories
						TreeNode subDirNode = new TreeNode(stringPathName, imageIndex, selectIndex);
						subDirNode.Name = stringPathName;
						subDirNode.Nodes.Add(NODE_PLACEHOLDER, ""); //to add + sign to expand the node
						node.Nodes.Add(subDirNode);
					}
				}
			}
			catch (IOException e)
			{
				MessageBox.Show("Error: Drive not ready or directory does not exist.\n" + e.Message);
			}
			catch (UnauthorizedAccessException e)
			{
				MessageBox.Show("Error: Drive or directory access denided.\n" + e.Message);
			}
			catch (Exception e)
			{
				MessageBox.Show("Error: " + e.Message);
			}
		}

		protected string GetPathName(string stringPath)
		{
			//Get Name of folder
			string[] stringSplit = stringPath.Split('\\');
			int _maxIndex = stringSplit.Length;
			return stringSplit[_maxIndex - 1];
		}

		protected string getFullPath(TreeNode node)
		{
			string stringPath = node.Name;
			while (node.Parent != null)
			{
				node = node.Parent;
				if (string.IsNullOrWhiteSpace(node.Name))
					break;

				string name = node.Name.Replace("\\", "");
				stringPath = name + "\\" + stringPath;
			}

			return stringPath;
		}
    }
}
