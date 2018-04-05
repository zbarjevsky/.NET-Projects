namespace VCFEdit
{
	partial class FormVCFEdit
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVCFEdit));
            this.m_toolStripContainer_Main = new System.Windows.Forms.ToolStripContainer();
            this.m_splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.m_treeContacts = new System.Windows.Forms.TreeView();
            this.m_imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.m_tbbtnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnDeleteChecked = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnAddContact = new System.Windows.Forms.ToolStripButton();
            this.m_splitContainerRight = new System.Windows.Forms.SplitContainer();
            this.m_txtContactData = new System.Windows.Forms.TextBox();
            this.m_Picture = new System.Windows.Forms.PictureBox();
            this.m_mnuMain = new System.Windows.Forms.MenuStrip();
            this.m_mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.m_tbbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnAppend = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnSave = new System.Windows.Forms.ToolStripButton();
            this.m_statusStripMain = new System.Windows.Forms.StatusStrip();
            this.m_toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.m_saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.m_errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.m_toolStripContainer_Main.ContentPanel.SuspendLayout();
            this.m_toolStripContainer_Main.TopToolStripPanel.SuspendLayout();
            this.m_toolStripContainer_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerMain)).BeginInit();
            this.m_splitContainerMain.Panel1.SuspendLayout();
            this.m_splitContainerMain.Panel2.SuspendLayout();
            this.m_splitContainerMain.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerRight)).BeginInit();
            this.m_splitContainerRight.Panel1.SuspendLayout();
            this.m_splitContainerRight.Panel2.SuspendLayout();
            this.m_splitContainerRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_Picture)).BeginInit();
            this.m_mnuMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.m_statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_toolStripContainer_Main
            // 
            // 
            // m_toolStripContainer_Main.ContentPanel
            // 
            this.m_toolStripContainer_Main.ContentPanel.Controls.Add(this.m_splitContainerMain);
            this.m_toolStripContainer_Main.ContentPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_toolStripContainer_Main.ContentPanel.Size = new System.Drawing.Size(1023, 479);
            this.m_toolStripContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_toolStripContainer_Main.Location = new System.Drawing.Point(0, 0);
            this.m_toolStripContainer_Main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_toolStripContainer_Main.Name = "m_toolStripContainer_Main";
            this.m_toolStripContainer_Main.Size = new System.Drawing.Size(1023, 534);
            this.m_toolStripContainer_Main.TabIndex = 0;
            this.m_toolStripContainer_Main.Text = "toolStripContainer1";
            // 
            // m_toolStripContainer_Main.TopToolStripPanel
            // 
            this.m_toolStripContainer_Main.TopToolStripPanel.Controls.Add(this.m_mnuMain);
            this.m_toolStripContainer_Main.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // m_splitContainerMain
            // 
            this.m_splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainerMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_splitContainerMain.Name = "m_splitContainerMain";
            // 
            // m_splitContainerMain.Panel1
            // 
            this.m_splitContainerMain.Panel1.Controls.Add(this.m_treeContacts);
            this.m_splitContainerMain.Panel1.Controls.Add(this.toolStrip2);
            // 
            // m_splitContainerMain.Panel2
            // 
            this.m_splitContainerMain.Panel2.Controls.Add(this.m_splitContainerRight);
            this.m_splitContainerMain.Size = new System.Drawing.Size(1023, 479);
            this.m_splitContainerMain.SplitterDistance = 398;
            this.m_splitContainerMain.SplitterWidth = 5;
            this.m_splitContainerMain.TabIndex = 0;
            // 
            // m_treeContacts
            // 
            this.m_treeContacts.CheckBoxes = true;
            this.m_treeContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_treeContacts.HideSelection = false;
            this.m_treeContacts.ImageIndex = 0;
            this.m_treeContacts.ImageList = this.m_imageListTree;
            this.m_treeContacts.Location = new System.Drawing.Point(0, 27);
            this.m_treeContacts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_treeContacts.Name = "m_treeContacts";
            this.m_treeContacts.SelectedImageIndex = 0;
            this.m_treeContacts.Size = new System.Drawing.Size(398, 452);
            this.m_treeContacts.TabIndex = 0;
            this.m_treeContacts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_treeContacts_AfterSelect);
            // 
            // m_imageListTree
            // 
            this.m_imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageListTree.ImageStream")));
            this.m_imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageListTree.Images.SetKeyName(0, "Web.ico");
            this.m_imageListTree.Images.SetKeyName(1, "Web1.ico");
            this.m_imageListTree.Images.SetKeyName(2, "Remove.ico");
            this.m_imageListTree.Images.SetKeyName(3, "Favorites.ico");
            this.m_imageListTree.Images.SetKeyName(4, "Mail.ico");
            this.m_imageListTree.Images.SetKeyName(5, "Tools.ico");
            this.m_imageListTree.Images.SetKeyName(6, "Precize.ico");
            this.m_imageListTree.Images.SetKeyName(7, "Synchronize.ico");
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tbbtnSelectAll,
            this.m_tbbtnDeleteChecked,
            this.m_tbbtnAddContact});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(398, 27);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "m_toolStripTreeContacts";
            // 
            // m_tbbtnSelectAll
            // 
            this.m_tbbtnSelectAll.CheckOnClick = true;
            this.m_tbbtnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnSelectAll.Image")));
            this.m_tbbtnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tbbtnSelectAll.Name = "m_tbbtnSelectAll";
            this.m_tbbtnSelectAll.Size = new System.Drawing.Size(91, 24);
            this.m_tbbtnSelectAll.Text = "Select All";
            this.m_tbbtnSelectAll.Click += new System.EventHandler(this.m_tbbtnSelectAll_Click);
            // 
            // m_tbbtnDeleteChecked
            // 
            this.m_tbbtnDeleteChecked.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnDeleteChecked.Image")));
            this.m_tbbtnDeleteChecked.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tbbtnDeleteChecked.Name = "m_tbbtnDeleteChecked";
            this.m_tbbtnDeleteChecked.Size = new System.Drawing.Size(134, 24);
            this.m_tbbtnDeleteChecked.Text = "Delete Selected";
            this.m_tbbtnDeleteChecked.Click += new System.EventHandler(this.m_tbbtnDeleteChecked_Click);
            // 
            // m_tbbtnAddContact
            // 
            this.m_tbbtnAddContact.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnAddContact.Image")));
            this.m_tbbtnAddContact.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tbbtnAddContact.Name = "m_tbbtnAddContact";
            this.m_tbbtnAddContact.Size = new System.Drawing.Size(112, 24);
            this.m_tbbtnAddContact.Text = "Add Contact";
            this.m_tbbtnAddContact.Click += new System.EventHandler(this.m_tbbtnAddContact_Click);
            // 
            // m_splitContainerRight
            // 
            this.m_splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainerRight.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainerRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_splitContainerRight.Name = "m_splitContainerRight";
            this.m_splitContainerRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainerRight.Panel1
            // 
            this.m_splitContainerRight.Panel1.Controls.Add(this.m_txtContactData);
            // 
            // m_splitContainerRight.Panel2
            // 
            this.m_splitContainerRight.Panel2.Controls.Add(this.m_Picture);
            this.m_splitContainerRight.Size = new System.Drawing.Size(620, 479);
            this.m_splitContainerRight.SplitterDistance = 202;
            this.m_splitContainerRight.SplitterWidth = 5;
            this.m_splitContainerRight.TabIndex = 0;
            // 
            // m_txtContactData
            // 
            this.m_txtContactData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtContactData.Location = new System.Drawing.Point(0, 0);
            this.m_txtContactData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_txtContactData.Multiline = true;
            this.m_txtContactData.Name = "m_txtContactData";
            this.m_txtContactData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtContactData.Size = new System.Drawing.Size(620, 202);
            this.m_txtContactData.TabIndex = 0;
            this.m_txtContactData.WordWrap = false;
            this.m_txtContactData.TextChanged += new System.EventHandler(this.m_txtContactData_TextChanged);
            // 
            // m_Picture
            // 
            this.m_Picture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_Picture.Location = new System.Drawing.Point(0, 0);
            this.m_Picture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_Picture.Name = "m_Picture";
            this.m_Picture.Size = new System.Drawing.Size(620, 272);
            this.m_Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_Picture.TabIndex = 0;
            this.m_Picture.TabStop = false;
            // 
            // m_mnuMain
            // 
            this.m_mnuMain.Dock = System.Windows.Forms.DockStyle.None;
            this.m_mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFile,
            this.m_mnuEdit,
            this.m_mnuView,
            this.m_mnuTools,
            this.m_mnuHelp});
            this.m_mnuMain.Location = new System.Drawing.Point(0, 27);
            this.m_mnuMain.Name = "m_mnuMain";
            this.m_mnuMain.Size = new System.Drawing.Size(1023, 28);
            this.m_mnuMain.TabIndex = 0;
            this.m_mnuMain.Text = "Main Menu";
            // 
            // m_mnuFile
            // 
            this.m_mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFileOpen,
            this.m_mnuFileSave,
            this.m_mnuFileClose,
            this.m_mnuFileSep1,
            this.m_mnuFileExit});
            this.m_mnuFile.Name = "m_mnuFile";
            this.m_mnuFile.Size = new System.Drawing.Size(44, 24);
            this.m_mnuFile.Text = "&File";
            // 
            // m_mnuFileOpen
            // 
            this.m_mnuFileOpen.Name = "m_mnuFileOpen";
            this.m_mnuFileOpen.Size = new System.Drawing.Size(114, 24);
            this.m_mnuFileOpen.Text = "&Open";
            this.m_mnuFileOpen.Click += new System.EventHandler(this.m_mnuFileOpen_Click);
            // 
            // m_mnuFileSave
            // 
            this.m_mnuFileSave.Name = "m_mnuFileSave";
            this.m_mnuFileSave.Size = new System.Drawing.Size(114, 24);
            this.m_mnuFileSave.Text = "&Save";
            this.m_mnuFileSave.Click += new System.EventHandler(this.m_mnuFileSave_Click);
            // 
            // m_mnuFileClose
            // 
            this.m_mnuFileClose.Name = "m_mnuFileClose";
            this.m_mnuFileClose.Size = new System.Drawing.Size(114, 24);
            this.m_mnuFileClose.Text = "&Close";
            this.m_mnuFileClose.Click += new System.EventHandler(this.m_mnuFileClose_Click);
            // 
            // m_mnuFileSep1
            // 
            this.m_mnuFileSep1.Name = "m_mnuFileSep1";
            this.m_mnuFileSep1.Size = new System.Drawing.Size(111, 6);
            // 
            // m_mnuFileExit
            // 
            this.m_mnuFileExit.Name = "m_mnuFileExit";
            this.m_mnuFileExit.Size = new System.Drawing.Size(114, 24);
            this.m_mnuFileExit.Text = "E&xit";
            this.m_mnuFileExit.Click += new System.EventHandler(this.m_mnuFileExit_Click);
            // 
            // m_mnuEdit
            // 
            this.m_mnuEdit.Name = "m_mnuEdit";
            this.m_mnuEdit.Size = new System.Drawing.Size(47, 24);
            this.m_mnuEdit.Text = "&Edit";
            // 
            // m_mnuView
            // 
            this.m_mnuView.Name = "m_mnuView";
            this.m_mnuView.Size = new System.Drawing.Size(53, 24);
            this.m_mnuView.Text = "&View";
            // 
            // m_mnuTools
            // 
            this.m_mnuTools.Name = "m_mnuTools";
            this.m_mnuTools.Size = new System.Drawing.Size(57, 24);
            this.m_mnuTools.Text = "&Tools";
            // 
            // m_mnuHelp
            // 
            this.m_mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.m_mnuHelp.Name = "m_mnuHelp";
            this.m_mnuHelp.Size = new System.Drawing.Size(53, 24);
            this.m_mnuHelp.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tbbtnOpen,
            this.m_tbbtnAppend,
            this.m_tbbtnSave});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(282, 27);
            this.toolStrip1.TabIndex = 1;
            // 
            // m_tbbtnOpen
            // 
            this.m_tbbtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnOpen.Image")));
            this.m_tbbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tbbtnOpen.Name = "m_tbbtnOpen";
            this.m_tbbtnOpen.Size = new System.Drawing.Size(74, 24);
            this.m_tbbtnOpen.Text = "Open...";
            this.m_tbbtnOpen.Click += new System.EventHandler(this.m_mnuFileOpen_Click);
            // 
            // m_tbbtnAppend
            // 
            this.m_tbbtnAppend.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnAppend.Image")));
            this.m_tbbtnAppend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tbbtnAppend.Name = "m_tbbtnAppend";
            this.m_tbbtnAppend.Size = new System.Drawing.Size(127, 24);
            this.m_tbbtnAppend.Text = "Append VCF(s)";
            this.m_tbbtnAppend.Click += new System.EventHandler(this.m_tbbtnAppend_Click);
            // 
            // m_tbbtnSave
            // 
            this.m_tbbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnSave.Image")));
            this.m_tbbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tbbtnSave.Name = "m_tbbtnSave";
            this.m_tbbtnSave.Size = new System.Drawing.Size(69, 24);
            this.m_tbbtnSave.Text = "Save...";
            this.m_tbbtnSave.Click += new System.EventHandler(this.m_mnuFileSave_Click);
            // 
            // m_statusStripMain
            // 
            this.m_errorProvider1.SetError(this.m_statusStripMain, "No Error");
            this.m_statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripStatusLabel1,
            this.m_toolStripStatusLabel2});
            this.m_statusStripMain.Location = new System.Drawing.Point(0, 534);
            this.m_statusStripMain.Name = "m_statusStripMain";
            this.m_statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.m_statusStripMain.Size = new System.Drawing.Size(1023, 25);
            this.m_statusStripMain.TabIndex = 0;
            this.m_statusStripMain.Text = "Status";
            // 
            // m_toolStripStatusLabel1
            // 
            this.m_toolStripStatusLabel1.Name = "m_toolStripStatusLabel1";
            this.m_toolStripStatusLabel1.Size = new System.Drawing.Size(50, 20);
            this.m_toolStripStatusLabel1.Text = "Ready";
            // 
            // m_toolStripStatusLabel2
            // 
            this.m_toolStripStatusLabel2.Name = "m_toolStripStatusLabel2";
            this.m_toolStripStatusLabel2.Size = new System.Drawing.Size(0, 20);
            // 
            // m_openFileDialog1
            // 
            this.m_openFileDialog1.FileName = "openFileDialog1";
            this.m_openFileDialog1.Filter = "VCF Files(*.vcf)|*.vcf|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.m_openFileDialog1.Multiselect = true;
            // 
            // m_errorProvider1
            // 
            this.m_errorProvider1.ContainerControl = this;
            // 
            // m_imageList1
            // 
            this.m_imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageList1.ImageStream")));
            this.m_imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageList1.Images.SetKeyName(0, "SmileYellow.ico");
            this.m_imageList1.Images.SetKeyName(1, "SmileGray.ico");
            this.m_imageList1.Images.SetKeyName(2, "SmileGreen.ico");
            this.m_imageList1.Images.SetKeyName(3, "SmileRed.ico");
            // 
            // FormVCFEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 559);
            this.Controls.Add(this.m_toolStripContainer_Main);
            this.Controls.Add(this.m_statusStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.m_mnuMain;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormVCFEdit";
            this.Text = "VCF";
            this.Load += new System.EventHandler(this.FormVCFEdit_Load);
            this.m_toolStripContainer_Main.ContentPanel.ResumeLayout(false);
            this.m_toolStripContainer_Main.TopToolStripPanel.ResumeLayout(false);
            this.m_toolStripContainer_Main.TopToolStripPanel.PerformLayout();
            this.m_toolStripContainer_Main.ResumeLayout(false);
            this.m_toolStripContainer_Main.PerformLayout();
            this.m_splitContainerMain.Panel1.ResumeLayout(false);
            this.m_splitContainerMain.Panel1.PerformLayout();
            this.m_splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerMain)).EndInit();
            this.m_splitContainerMain.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.m_splitContainerRight.Panel1.ResumeLayout(false);
            this.m_splitContainerRight.Panel1.PerformLayout();
            this.m_splitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerRight)).EndInit();
            this.m_splitContainerRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_Picture)).EndInit();
            this.m_mnuMain.ResumeLayout(false);
            this.m_mnuMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.m_statusStripMain.ResumeLayout(false);
            this.m_statusStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer m_toolStripContainer_Main;
		private System.Windows.Forms.StatusStrip m_statusStripMain;
		private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel1;
		private System.Windows.Forms.SplitContainer m_splitContainerMain;
		private System.Windows.Forms.TreeView m_treeContacts;
		private System.Windows.Forms.MenuStrip m_mnuMain;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFile;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFileOpen;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFileSave;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFileClose;
		private System.Windows.Forms.ToolStripSeparator m_mnuFileSep1;
		private System.Windows.Forms.ToolStripMenuItem m_mnuFileExit;
		private System.Windows.Forms.ToolStripMenuItem m_mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem m_mnuView;
		private System.Windows.Forms.ToolStripMenuItem m_mnuTools;
		private System.Windows.Forms.ToolStripMenuItem m_mnuHelp;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton m_tbbtnOpen;
		private System.Windows.Forms.OpenFileDialog m_openFileDialog1;
		private System.Windows.Forms.SaveFileDialog m_saveFileDialog1;
		private System.Windows.Forms.ToolStripButton m_tbbtnSave;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton m_tbbtnSelectAll;
		private System.Windows.Forms.SplitContainer m_splitContainerRight;
		private System.Windows.Forms.TextBox m_txtContactData;
		private System.Windows.Forms.ErrorProvider m_errorProvider1;
		private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel2;
		private System.Windows.Forms.ImageList m_imageList1;
		private System.Windows.Forms.ToolStripButton m_tbbtnDeleteChecked;
		private System.Windows.Forms.ImageList m_imageListTree;
		private System.Windows.Forms.ToolStripButton m_tbbtnAddContact;
		private System.Windows.Forms.PictureBox m_Picture;
		private System.Windows.Forms.ToolStripButton m_tbbtnAppend;
	}
}

