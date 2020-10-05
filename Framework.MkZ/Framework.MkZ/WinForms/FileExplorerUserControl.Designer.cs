namespace MZ.WinForms
{
    partial class FileExplorerUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileExplorerUserControl));
            this.m_imageList = new System.Windows.Forms.ImageList(this.components);
            this.m_pnlCommands = new System.Windows.Forms.Panel();
            this.m_btnNewFolder = new System.Windows.Forms.Button();
            this.m_btnBrowse = new System.Windows.Forms.Button();
            this.m_btnRoot = new System.Windows.Forms.Button();
            this.m_btnUp = new System.Windows.Forms.Button();
            this.m_txtPath = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_ctxmnuList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_mnuSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.m_listFiles = new ListViewVirtualMode.ListViewVirtualWithCheckBoxes();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_pnlCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.m_ctxmnuList.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_imageList
            // 
            this.m_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageList.ImageStream")));
            this.m_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageList.Images.SetKeyName(0, "Icon_272_16.png");
            this.m_imageList.Images.SetKeyName(1, "");
            this.m_imageList.Images.SetKeyName(2, "Icon_003_16.png");
            this.m_imageList.Images.SetKeyName(3, "Icon_126_16.png");
            this.m_imageList.Images.SetKeyName(4, "Icon_000_16.png");
            this.m_imageList.Images.SetKeyName(5, "");
            this.m_imageList.Images.SetKeyName(6, "");
            this.m_imageList.Images.SetKeyName(7, "Icon_188_16.png");
            this.m_imageList.Images.SetKeyName(8, "");
            this.m_imageList.Images.SetKeyName(9, "Copy.ico");
            this.m_imageList.Images.SetKeyName(10, "Icon_146_16.png");
            // 
            // m_pnlCommands
            // 
            this.m_pnlCommands.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_pnlCommands.Controls.Add(this.m_btnNewFolder);
            this.m_pnlCommands.Controls.Add(this.m_btnBrowse);
            this.m_pnlCommands.Controls.Add(this.m_btnRoot);
            this.m_pnlCommands.Controls.Add(this.m_btnUp);
            this.m_pnlCommands.Controls.Add(this.m_txtPath);
            this.m_pnlCommands.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlCommands.Location = new System.Drawing.Point(0, 0);
            this.m_pnlCommands.Name = "m_pnlCommands";
            this.m_pnlCommands.Size = new System.Drawing.Size(538, 28);
            this.m_pnlCommands.TabIndex = 1;
            // 
            // m_btnNewFolder
            // 
            this.m_btnNewFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnNewFolder.Image = ((System.Drawing.Image)(resources.GetObject("m_btnNewFolder.Image")));
            this.m_btnNewFolder.Location = new System.Drawing.Point(59, -1);
            this.m_btnNewFolder.Name = "m_btnNewFolder";
            this.m_btnNewFolder.Size = new System.Drawing.Size(27, 25);
            this.m_btnNewFolder.TabIndex = 6;
            this.toolTip1.SetToolTip(this.m_btnNewFolder, "New Folder");
            this.m_btnNewFolder.UseVisualStyleBackColor = true;
            this.m_btnNewFolder.Click += new System.EventHandler(this.m_btnNewFolder_Click);
            // 
            // m_btnBrowse
            // 
            this.m_btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("m_btnBrowse.Image")));
            this.m_btnBrowse.Location = new System.Drawing.Point(512, -1);
            this.m_btnBrowse.Name = "m_btnBrowse";
            this.m_btnBrowse.Size = new System.Drawing.Size(27, 26);
            this.m_btnBrowse.TabIndex = 5;
            this.toolTip1.SetToolTip(this.m_btnBrowse, "Browse");
            this.m_btnBrowse.UseVisualStyleBackColor = true;
            this.m_btnBrowse.Click += new System.EventHandler(this.m_btnBrowse_Click);
            // 
            // m_btnRoot
            // 
            this.m_btnRoot.Image = ((System.Drawing.Image)(resources.GetObject("m_btnRoot.Image")));
            this.m_btnRoot.Location = new System.Drawing.Point(0, -1);
            this.m_btnRoot.Name = "m_btnRoot";
            this.m_btnRoot.Size = new System.Drawing.Size(27, 25);
            this.m_btnRoot.TabIndex = 4;
            this.toolTip1.SetToolTip(this.m_btnRoot, "Drive Root");
            this.m_btnRoot.UseVisualStyleBackColor = true;
            this.m_btnRoot.Click += new System.EventHandler(this.m_btnRoot_Click);
            // 
            // m_btnUp
            // 
            this.m_btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnUp.Image = ((System.Drawing.Image)(resources.GetObject("m_btnUp.Image")));
            this.m_btnUp.Location = new System.Drawing.Point(30, -1);
            this.m_btnUp.Name = "m_btnUp";
            this.m_btnUp.Size = new System.Drawing.Size(27, 25);
            this.m_btnUp.TabIndex = 3;
            this.toolTip1.SetToolTip(this.m_btnUp, "Folder UP");
            this.m_btnUp.UseVisualStyleBackColor = true;
            this.m_btnUp.Click += new System.EventHandler(this.m_btnUp_Click);
            // 
            // m_txtPath
            // 
            this.m_txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.m_txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.m_txtPath.BackColor = System.Drawing.SystemColors.ControlLight;
            this.m_txtPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.errorProvider1.SetIconAlignment(this.m_txtPath, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.errorProvider1.SetIconPadding(this.m_txtPath, 2);
            this.m_txtPath.Location = new System.Drawing.Point(110, 8);
            this.m_txtPath.Name = "m_txtPath";
            this.m_txtPath.ReadOnly = true;
            this.m_txtPath.Size = new System.Drawing.Size(397, 13);
            this.m_txtPath.TabIndex = 2;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // m_ctxmnuList
            // 
            this.m_ctxmnuList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuSelect,
            this.m_mnuSeparator1,
            this.m_mnuRefresh,
            this.m_mnuSeparator2,
            this.m_mnuRename,
            this.m_mnuDelete});
            this.m_ctxmnuList.Name = "m_ctxmnuTree";
            this.m_ctxmnuList.Size = new System.Drawing.Size(147, 104);
            // 
            // m_mnuSelect
            // 
            this.m_mnuSelect.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuSelect.Image")));
            this.m_mnuSelect.Name = "m_mnuSelect";
            this.m_mnuSelect.Size = new System.Drawing.Size(146, 22);
            this.m_mnuSelect.Text = "Toggle &Check";
            this.m_mnuSelect.Click += new System.EventHandler(this.m_mnuSelect_Click);
            // 
            // m_mnuSeparator1
            // 
            this.m_mnuSeparator1.Name = "m_mnuSeparator1";
            this.m_mnuSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // m_mnuRefresh
            // 
            this.m_mnuRefresh.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuRefresh.Image")));
            this.m_mnuRefresh.Name = "m_mnuRefresh";
            this.m_mnuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.m_mnuRefresh.Size = new System.Drawing.Size(146, 22);
            this.m_mnuRefresh.Text = "&Refresh";
            this.m_mnuRefresh.Click += new System.EventHandler(this.m_mnuRefresh_Click);
            // 
            // m_mnuSeparator2
            // 
            this.m_mnuSeparator2.Name = "m_mnuSeparator2";
            this.m_mnuSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // m_mnuRename
            // 
            this.m_mnuRename.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuRename.Image")));
            this.m_mnuRename.Name = "m_mnuRename";
            this.m_mnuRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.m_mnuRename.Size = new System.Drawing.Size(146, 22);
            this.m_mnuRename.Text = "Re&name";
            this.m_mnuRename.Click += new System.EventHandler(this.m_mnuRename_Click);
            // 
            // m_mnuDelete
            // 
            this.m_mnuDelete.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuDelete.Image")));
            this.m_mnuDelete.Name = "m_mnuDelete";
            this.m_mnuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.m_mnuDelete.Size = new System.Drawing.Size(146, 22);
            this.m_mnuDelete.Text = "&Delete";
            this.m_mnuDelete.Click += new System.EventHandler(this.m_mnuDelete_Click);
            // 
            // m_listFiles
            // 
            this.m_listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.m_listFiles.ContextMenuStrip = this.m_ctxmnuList;
            this.m_listFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listFiles.FullRowSelect = true;
            this.m_listFiles.GridLines = true;
            this.m_listFiles.HideSelection = false;
            this.m_listFiles.Location = new System.Drawing.Point(0, 28);
            this.m_listFiles.Name = "m_listFiles";
            this.m_listFiles.OwnerDraw = true;
            this.m_listFiles.Size = new System.Drawing.Size(538, 467);
            this.m_listFiles.SmallImageList = this.m_imageList;
            this.m_listFiles.TabIndex = 0;
            this.m_listFiles.UseCompatibleStateImageBehavior = false;
            this.m_listFiles.View = System.Windows.Forms.View.Details;
            this.m_listFiles.VirtualMode = true;
            this.m_listFiles.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.m_listFiles_ItemCheck);
            this.m_listFiles.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.m_listFiles_ItemChecked);
            this.m_listFiles.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.m_listFiles_RetrieveVirtualItem);
            this.m_listFiles.SelectedIndexChanged += new System.EventHandler(this.m_listFiles_SelectedIndexChanged);
            this.m_listFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_listFiles_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 180;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 75;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Created";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Modified";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 120;
            // 
            // FileExplorerUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_listFiles);
            this.Controls.Add(this.m_pnlCommands);
            this.Name = "FileExplorerUserControl";
            this.Size = new System.Drawing.Size(538, 495);
            this.Load += new System.EventHandler(this.FileExplorerUserControl_Load);
            this.m_pnlCommands.ResumeLayout(false);
            this.m_pnlCommands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.m_ctxmnuList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ListViewVirtualMode.ListViewVirtualWithCheckBoxes m_listFiles;
        private System.Windows.Forms.Panel m_pnlCommands;
        private System.Windows.Forms.Button m_btnRoot;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button m_btnUp;
        private System.Windows.Forms.TextBox m_txtPath;
        private System.Windows.Forms.Button m_btnBrowse;
        private System.Windows.Forms.ImageList m_imageList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button m_btnNewFolder;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ContextMenuStrip m_ctxmnuList;
        private System.Windows.Forms.ToolStripMenuItem m_mnuSelect;
        private System.Windows.Forms.ToolStripSeparator m_mnuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem m_mnuRefresh;
        private System.Windows.Forms.ToolStripSeparator m_mnuSeparator2;
        private System.Windows.Forms.ToolStripMenuItem m_mnuRename;
        private System.Windows.Forms.ToolStripMenuItem m_mnuDelete;
    }
}
