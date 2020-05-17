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
using MZ.Tools.WinForms;

namespace MZ.ControlsWinForms
{
    public partial class FoldersTreeUserControl : UserControl
    {
		public Action<string> OpenFolder = (fullPath) => { };

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
			if (tvFolders.Nodes.Count == 0)
				return;

			List<string> folders = new List<string>();
			
			string name = Path.GetFileName(fullPath);
			string path = Path.GetDirectoryName(fullPath);
			folders.Add(name);
			while (!string.IsNullOrWhiteSpace(path))
			{
				name = Path.GetFileName(path);
				if(string.IsNullOrWhiteSpace(name))
				{
					folders.Add(path);
					break;
				}

				folders.Add(name);
				path = Path.GetDirectoryName(path);
			}
			folders.Reverse();

			TreeNode node = FindSubNode(tvFolders.Nodes[0], folders);
			if (node != null)
			{
				tvFolders.SelectedNode = node;
				node.EnsureVisible();
			}
		}

		private TreeNode FindSubNode(TreeNode node, List<string> folders, int index = 0)
		{
			if(node.Nodes.Count == 0)
				PopulateDirectory(node, node.Nodes);

			foreach (TreeNode n in node.Nodes)
			{
				if(n.Text == folders[index])
				{
					if (index == folders.Count - 1)
						return n;

					return FindSubNode(n, folders, index + 1);
				}
			}
			return null;
		}

		private void PopulateDriveList()
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

			//set node collection
			TreeNodeCollection nodeCollection = root.Nodes;

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
				nodeCollection.Add(nodeTreeNode);
			}

			root.Expand();
			this.Cursor = Cursors.Default;

		}

		protected ManagementObjectCollection getDrives()
		{
			//get drive collection
			ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * From Win32_LogicalDisk ");
			ManagementObjectCollection queryCollection = query.Get();

			return queryCollection;
		}

		private void tvFolders_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			//Populate folders and files when a folder is selected
			this.Cursor = Cursors.WaitCursor;

			//get current selected drive or folder
			TreeNode nodeCurrent = e.Node;

			//clear all sub-folders
			nodeCurrent.Nodes.Clear();

			if (nodeCurrent.SelectedImageIndex == 0)
			{
				//Selected My Computer - repopulate drive list
				PopulateDriveList();
			}
			else
			{
				//populate sub-folders and folder files
				PopulateDirectory(nodeCurrent, nodeCurrent.Nodes);
				OpenFolder(getFullPath(nodeCurrent.FullPath));
			}

			this.Cursor = Cursors.Default;
		}

		protected void PopulateDirectory(TreeNode nodeCurrent, TreeNodeCollection nodeCurrentCollection)
		{
			TreeNode nodeDir;
			int imageIndex = 2;     //unselected image index
			int selectIndex = 3;    //selected image index

			if (nodeCurrent.SelectedImageIndex != 0)
			{
				//populate treeview with folders
				try
				{
					//check path
					if (Directory.Exists(getFullPath(nodeCurrent.FullPath)) == false)
					{
						MessageBox.Show("Directory or path " + nodeCurrent.ToString() + " does not exist.");
					}
					else
					{
						//populate files
						//PopulateFiles(nodeCurrent);

						string[] stringDirectories = Directory.GetDirectories(getFullPath(nodeCurrent.FullPath));

						//loop throught all directories
						foreach (string stringDir in stringDirectories)
						{
							string stringPathName = Path.GetFileName(stringDir);

							//create node for directories
							nodeDir = new TreeNode(stringPathName, imageIndex, selectIndex);
							nodeCurrentCollection.Add(nodeDir);
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
