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

namespace MZ.WinForms
{
    public partial class FoldersTreeUserControl : UserControl
    {
		public Action<string> AfterSelectAction = (fullPath) => { };

        public FoldersTreeUserControl()
        {
            InitializeComponent();
        }

        private void FoldersTreeUserControl_Load(object sender, EventArgs e)
        {
			PopulateDriveList();
		}

		//This procedure populate the TreeView with the Drive list
		public void SelectFolder(string fullPath)
		{
			TreeNode node = FindTreeNode(fullPath);
			if (node != null)
			{
				tvFolders.SelectedNode = node;
				node.EnsureVisible();
			}
		}

		public void RefreshFolder(string fullPath)
		{
			TreeNode node = FindTreeNode(fullPath);
			if (node != null)
			{
				PopulateDirectory(node);
			}
		}

		public void EditFolder(string fullPath)
		{
			TreeNode node = FindTreeNode(fullPath);
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
				if (tvFolders.Nodes[0].Nodes[i].Text == fullPath)
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
			if(node.Nodes.Count == 0)
				PopulateDirectory(node);

			foreach (TreeNode n in node.Nodes)
			{
				if(n.Text == folders[index])
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

			const int Removable = 2;
			const int LocalDisk = 3;
			const int Network = 4;
			const int CD = 5;
			//const int RAMDrive = 6;

			this.Cursor = Cursors.WaitCursor;
			//clear TreeView
			tvFolders.Nodes.Clear();
			TreeNode root = new TreeNode("My Computer", 0, 0);
			tvFolders.Nodes.Add(root);

			//Get Drive list
			ManagementObjectCollection queryCollection = getDrives();
			foreach (ManagementObject drive in queryCollection)
			{

				switch (int.Parse(drive["DriveType"].ToString()))
				{
					case Removable:         //removable drives
						imageIndex = 5;
						selectIndex = 5;
						break;
					case LocalDisk:         //Local drives
						imageIndex = 6;
						selectIndex = 6;
						break;
					case CD:                //CD rom drives
						imageIndex = 7;
						selectIndex = 7;
						break;
					case Network:           //Network drives
						imageIndex = 8;
						selectIndex = 8;
						break;
					default:                //defalut to folder
						imageIndex = 2;
						selectIndex = 3;
						break;
				}
				//create new drive node
				TreeNode nodeTreeNode = new TreeNode(drive["Name"].ToString() + "\\", imageIndex, selectIndex);

				//add new node
				root.Nodes.Add(nodeTreeNode);
			}

			root.Expand();
			this.Cursor = Cursors.Default;

			return root;
		}

		protected ManagementObjectCollection getDrives()
		{
			//get drive collection
			ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * From Win32_LogicalDisk ");
			ManagementObjectCollection queryCollection = query.Get();

			return queryCollection;
		}

		private void tvFolders_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			//get current selected drive or folder
			try
			{
				if (!string.IsNullOrWhiteSpace(e.Label))
				{
					string path = getFullPath(e.Node.FullPath);
					string parent = Path.GetDirectoryName(path);
					string newPath = Path.Combine(parent, e.Label);
					Directory.Move(path, newPath);
					AfterSelectAction(newPath);
				}
			}
			catch (Exception err)
			{
				e.CancelEdit = true;
				MessageBox.Show(err.Message, "Rename Folder");
			}			
		}

		private void tvFolders_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (e.Node.Nodes.Count == 0)
			{

				//Populate folders and files when a folder is selected
				this.Cursor = Cursors.WaitCursor;

				System.Diagnostics.Debug.WriteLine("tvFolders_AfterSelect: " + e.Node.Text);

				//populate sub-folders and folder files
				PopulateDirectory(e.Node);

				this.Cursor = Cursors.Default;
			}

			//notify
			AfterSelectAction(getFullPath(e.Node.FullPath));
		}

		private bool IsRootNoode(TreeNode node)
		{
			string path = getFullPath(node.FullPath);
			return string.IsNullOrWhiteSpace(path);
		}

		protected void PopulateDirectory(TreeNode node)
		{
			TreeNode nodeDir;
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
					if (!Directory.Exists(getFullPath(node.FullPath)))
					{
						MessageBox.Show("Directory or path " + node.ToString() + " does not exist.");
						return;
					}

					string[] stringDirectories = Directory.GetDirectories(getFullPath(node.FullPath));

					//loop throught all directories
					foreach (string stringDir in stringDirectories)
					{
						string stringPathName = Path.GetFileName(stringDir);

						//create node for directories
						nodeDir = new TreeNode(stringPathName, imageIndex, selectIndex);
						node.Nodes.Add(nodeDir);
					}
				}
			}
			catch (IOException e)
			{
				MessageBox.Show("Error: Drive not ready or directory does not exist.");
			}
			catch (UnauthorizedAccessException e)
			{
				MessageBox.Show("Error: Drive or directory access denided.");
			}
			catch (Exception e)
			{
				MessageBox.Show("Error: " + e);
			}
		}

		protected string GetPathName(string stringPath)
		{
			//Get Name of folder
			string[] stringSplit = stringPath.Split('\\');
			int _maxIndex = stringSplit.Length;
			return stringSplit[_maxIndex - 1];
		}

		protected string getFullPath(string stringPath)
		{
			//Get Full path
			//remove My Computer from path.
			return stringPath.Replace("My Computer\\", "").Replace("\\\\", "\\");
		}
	}
}
