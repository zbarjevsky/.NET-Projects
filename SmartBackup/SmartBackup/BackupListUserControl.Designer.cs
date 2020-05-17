namespace SmartBackup
{
    partial class BackupListUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupListUserControl));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("High Priority", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Normal Priority", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Low Priority", System.Windows.Forms.HorizontalAlignment.Left);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.m_ContextMenuList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_listBackup = new ListViewExtensions.ListViewCollapsibleGroups();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_btnBackupAll = new System.Windows.Forms.Button();
            this.m_btnBackupImportant = new System.Windows.Forms.Button();
            this.m_btnEdit = new System.Windows.Forms.Button();
            this.m_btnRemove = new System.Windows.Forms.Button();
            this.m_btnAdd = new System.Windows.Forms.Button();
            this.m_mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuBackupSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuBackupPriority = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuBackupAll = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ContextMenuList.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Shared.ico");
            this.imageList1.Images.SetKeyName(1, "Blacklisted.ico");
            this.imageList1.Images.SetKeyName(2, "propertysheets.ico");
            this.imageList1.Images.SetKeyName(3, "folder.ico");
            this.imageList1.Images.SetKeyName(4, "FileCut.ico");
            this.imageList1.Images.SetKeyName(5, "FilesCopy.ico");
            this.imageList1.Images.SetKeyName(6, "Paste.ico");
            this.imageList1.Images.SetKeyName(7, "Favorites.ico");
            // 
            // m_ContextMenuList
            // 
            this.m_ContextMenuList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuEdit,
            this.toolStripMenuItem2,
            this.m_mnuBackupSelected,
            this.toolStripMenuItem1,
            this.m_mnuBackupPriority,
            this.m_mnuBackupAll});
            this.m_ContextMenuList.Name = "m_ContextMenuList";
            this.m_ContextMenuList.Size = new System.Drawing.Size(193, 104);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 6);
            // 
            // m_listBackup
            // 
            this.m_listBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listBackup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_listBackup.ContextMenuStrip = this.m_ContextMenuList;
            this.m_listBackup.FullRowSelect = true;
            this.m_listBackup.GridLines = true;
            listViewGroup1.Header = "High Priority";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "Normal Priority";
            listViewGroup2.Name = "listViewGroup2";
            listViewGroup3.Header = "Low Priority";
            listViewGroup3.Name = "listViewGroup3";
            this.m_listBackup.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.m_listBackup.HideSelection = false;
            this.m_listBackup.Location = new System.Drawing.Point(3, 32);
            this.m_listBackup.Name = "m_listBackup";
            this.m_listBackup.Size = new System.Drawing.Size(594, 165);
            this.m_listBackup.TabIndex = 6;
            this.m_listBackup.UseCompatibleStateImageBehavior = false;
            this.m_listBackup.View = System.Windows.Forms.View.Details;
            this.m_listBackup.SelectedIndexChanged += new System.EventHandler(this.m_listBackup_SelectedIndexChanged);
            this.m_listBackup.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_listBackup_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Priority";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "From";
            this.columnHeader2.Width = 300;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "To";
            this.columnHeader3.Width = 300;
            // 
            // m_btnBackupAll
            // 
            this.m_btnBackupAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBackupAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnBackupAll.ImageIndex = 5;
            this.m_btnBackupAll.ImageList = this.imageList1;
            this.m_btnBackupAll.Location = new System.Drawing.Point(455, 4);
            this.m_btnBackupAll.Name = "m_btnBackupAll";
            this.m_btnBackupAll.Size = new System.Drawing.Size(142, 23);
            this.m_btnBackupAll.TabIndex = 11;
            this.m_btnBackupAll.Text = "Backup All...";
            this.m_btnBackupAll.UseVisualStyleBackColor = true;
            this.m_btnBackupAll.Click += new System.EventHandler(this.m_btnBackupAll_Click);
            // 
            // m_btnBackupImportant
            // 
            this.m_btnBackupImportant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBackupImportant.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnBackupImportant.ImageIndex = 7;
            this.m_btnBackupImportant.ImageList = this.imageList1;
            this.m_btnBackupImportant.Location = new System.Drawing.Point(278, 4);
            this.m_btnBackupImportant.Name = "m_btnBackupImportant";
            this.m_btnBackupImportant.Size = new System.Drawing.Size(171, 23);
            this.m_btnBackupImportant.TabIndex = 10;
            this.m_btnBackupImportant.Text = "Backup High Priority Only...";
            this.m_btnBackupImportant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnBackupImportant.UseVisualStyleBackColor = true;
            this.m_btnBackupImportant.Click += new System.EventHandler(this.m_btnBackupImportant_Click);
            // 
            // m_btnEdit
            // 
            this.m_btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnEdit.ImageIndex = 2;
            this.m_btnEdit.ImageList = this.imageList1;
            this.m_btnEdit.Location = new System.Drawing.Point(160, 4);
            this.m_btnEdit.Name = "m_btnEdit";
            this.m_btnEdit.Size = new System.Drawing.Size(78, 23);
            this.m_btnEdit.TabIndex = 9;
            this.m_btnEdit.Text = "Edit...";
            this.m_btnEdit.UseVisualStyleBackColor = true;
            this.m_btnEdit.Click += new System.EventHandler(this.m_btnEdit_Click);
            // 
            // m_btnRemove
            // 
            this.m_btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnRemove.ImageIndex = 4;
            this.m_btnRemove.ImageList = this.imageList1;
            this.m_btnRemove.Location = new System.Drawing.Point(84, 4);
            this.m_btnRemove.Name = "m_btnRemove";
            this.m_btnRemove.Size = new System.Drawing.Size(70, 23);
            this.m_btnRemove.TabIndex = 8;
            this.m_btnRemove.Text = "Delete";
            this.m_btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnRemove.UseVisualStyleBackColor = true;
            this.m_btnRemove.Click += new System.EventHandler(this.m_btnRemove_Click);
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnAdd.ImageIndex = 3;
            this.m_btnAdd.ImageList = this.imageList1;
            this.m_btnAdd.Location = new System.Drawing.Point(3, 4);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(75, 23);
            this.m_btnAdd.TabIndex = 7;
            this.m_btnAdd.Text = "Add...";
            this.m_btnAdd.UseVisualStyleBackColor = true;
            this.m_btnAdd.Click += new System.EventHandler(this.m_btnAdd_Click);
            // 
            // m_mnuEdit
            // 
            this.m_mnuEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_mnuEdit.Image = global::SmartBackup.Properties.Resources.Properties;
            this.m_mnuEdit.Name = "m_mnuEdit";
            this.m_mnuEdit.Size = new System.Drawing.Size(192, 22);
            this.m_mnuEdit.Text = "Edit...";
            this.m_mnuEdit.Click += new System.EventHandler(this.m_btnEdit_Click);
            // 
            // m_mnuBackupSelected
            // 
            this.m_mnuBackupSelected.Image = global::SmartBackup.Properties.Resources.FileCopy;
            this.m_mnuBackupSelected.Name = "m_mnuBackupSelected";
            this.m_mnuBackupSelected.Size = new System.Drawing.Size(192, 22);
            this.m_mnuBackupSelected.Text = "Backup Selected...";
            this.m_mnuBackupSelected.Click += new System.EventHandler(this.m_mnuBackupSelected_Click);
            // 
            // m_mnuBackupPriority
            // 
            this.m_mnuBackupPriority.Image = global::SmartBackup.Properties.Resources.Favorites;
            this.m_mnuBackupPriority.Name = "m_mnuBackupPriority";
            this.m_mnuBackupPriority.Size = new System.Drawing.Size(192, 22);
            this.m_mnuBackupPriority.Text = "Backup High Priority...";
            this.m_mnuBackupPriority.Click += new System.EventHandler(this.m_btnBackupImportant_Click);
            // 
            // m_mnuBackupAll
            // 
            this.m_mnuBackupAll.Image = global::SmartBackup.Properties.Resources.FilesCopy;
            this.m_mnuBackupAll.Name = "m_mnuBackupAll";
            this.m_mnuBackupAll.Size = new System.Drawing.Size(192, 22);
            this.m_mnuBackupAll.Text = "Backup All..";
            this.m_mnuBackupAll.Click += new System.EventHandler(this.m_btnBackupAll_Click);
            // 
            // BackupListUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.m_btnBackupAll);
            this.Controls.Add(this.m_btnBackupImportant);
            this.Controls.Add(this.m_btnEdit);
            this.Controls.Add(this.m_btnRemove);
            this.Controls.Add(this.m_btnAdd);
            this.Controls.Add(this.m_listBackup);
            this.MinimumSize = new System.Drawing.Size(600, 200);
            this.Name = "BackupListUserControl";
            this.Size = new System.Drawing.Size(600, 200);
            this.Load += new System.EventHandler(this.BackupListUserControl_Load);
            this.m_ContextMenuList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_btnBackupAll;
        private System.Windows.Forms.Button m_btnBackupImportant;
        private System.Windows.Forms.Button m_btnEdit;
        private System.Windows.Forms.Button m_btnRemove;
        private System.Windows.Forms.Button m_btnAdd;
        private ListViewExtensions.ListViewCollapsibleGroups m_listBackup;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip m_ContextMenuList;
        private System.Windows.Forms.ToolStripMenuItem m_mnuBackupSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem m_mnuBackupPriority;
        private System.Windows.Forms.ToolStripMenuItem m_mnuBackupAll;
        private System.Windows.Forms.ToolStripMenuItem m_mnuEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}
