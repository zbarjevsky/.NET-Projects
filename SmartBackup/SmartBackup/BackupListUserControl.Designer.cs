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
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Priority", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Other", System.Windows.Forms.HorizontalAlignment.Left);
            this.m_btnBackupAll = new System.Windows.Forms.Button();
            this.m_btnBackupImportant = new System.Windows.Forms.Button();
            this.m_btnEdit = new System.Windows.Forms.Button();
            this.m_btnRemove = new System.Windows.Forms.Button();
            this.m_btnAdd = new System.Windows.Forms.Button();
            this.m_listBackup = new ListViewExtensions.ListViewCollapsibleGroups();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // m_btnBackupAll
            // 
            this.m_btnBackupAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBackupAll.Location = new System.Drawing.Point(427, 4);
            this.m_btnBackupAll.Name = "m_btnBackupAll";
            this.m_btnBackupAll.Size = new System.Drawing.Size(142, 23);
            this.m_btnBackupAll.TabIndex = 11;
            this.m_btnBackupAll.Text = "Full Backup...";
            this.m_btnBackupAll.UseVisualStyleBackColor = true;
            this.m_btnBackupAll.Click += new System.EventHandler(this.m_btnBackupAll_Click);
            // 
            // m_btnBackupImportant
            // 
            this.m_btnBackupImportant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBackupImportant.Location = new System.Drawing.Point(261, 4);
            this.m_btnBackupImportant.Name = "m_btnBackupImportant";
            this.m_btnBackupImportant.Size = new System.Drawing.Size(160, 23);
            this.m_btnBackupImportant.TabIndex = 10;
            this.m_btnBackupImportant.Text = "Backup Important Only...";
            this.m_btnBackupImportant.UseVisualStyleBackColor = true;
            // 
            // m_btnEdit
            // 
            this.m_btnEdit.Location = new System.Drawing.Point(165, 4);
            this.m_btnEdit.Name = "m_btnEdit";
            this.m_btnEdit.Size = new System.Drawing.Size(75, 23);
            this.m_btnEdit.TabIndex = 9;
            this.m_btnEdit.Text = "Modify...";
            this.m_btnEdit.UseVisualStyleBackColor = true;
            this.m_btnEdit.Click += new System.EventHandler(this.m_btnEdit_Click);
            // 
            // m_btnRemove
            // 
            this.m_btnRemove.Location = new System.Drawing.Point(84, 4);
            this.m_btnRemove.Name = "m_btnRemove";
            this.m_btnRemove.Size = new System.Drawing.Size(75, 23);
            this.m_btnRemove.TabIndex = 8;
            this.m_btnRemove.Text = "Remove...";
            this.m_btnRemove.UseVisualStyleBackColor = true;
            this.m_btnRemove.Click += new System.EventHandler(this.m_btnRemove_Click);
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Location = new System.Drawing.Point(3, 4);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(75, 23);
            this.m_btnAdd.TabIndex = 7;
            this.m_btnAdd.Text = "Add...";
            this.m_btnAdd.UseVisualStyleBackColor = true;
            this.m_btnAdd.Click += new System.EventHandler(this.m_btnAdd_Click);
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
            this.m_listBackup.FullRowSelect = true;
            this.m_listBackup.GridLines = true;
            listViewGroup3.Header = "Priority";
            listViewGroup3.Name = "listViewGroup1";
            listViewGroup4.Header = "Other";
            listViewGroup4.Name = "listViewGroup2";
            this.m_listBackup.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
            this.m_listBackup.HideSelection = false;
            this.m_listBackup.Location = new System.Drawing.Point(3, 32);
            this.m_listBackup.Name = "m_listBackup";
            this.m_listBackup.Size = new System.Drawing.Size(567, 265);
            this.m_listBackup.TabIndex = 6;
            this.m_listBackup.UseCompatibleStateImageBehavior = false;
            this.m_listBackup.View = System.Windows.Forms.View.Details;
            this.m_listBackup.SelectedIndexChanged += new System.EventHandler(this.m_listBackup_SelectedIndexChanged);
            this.m_listBackup.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_listBackup_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Op";
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
            this.MinimumSize = new System.Drawing.Size(700, 300);
            this.Name = "BackupListUserControl";
            this.Size = new System.Drawing.Size(700, 300);
            this.Load += new System.EventHandler(this.BackupListUserControl_Load);
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
    }
}
