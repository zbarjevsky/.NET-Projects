namespace MZ.WinForms
{
    partial class FormBrowseForFolder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBrowseForFolder));
            MZ.WinForms.ColorBarsProgressBar.ThemeColorSet themeColorSet6 = new MZ.WinForms.ColorBarsProgressBar.ThemeColorSet();
            this.m_txtSelectedFolder = new System.Windows.Forms.TextBox();
            this.m_btnNewFolder = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_txtDescription = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.m_btnGoToFolder = new System.Windows.Forms.Button();
            this.m_lblMessage = new System.Windows.Forms.Label();
            this.m_progressBar = new MZ.WinForms.ColorBarsProgressBar();
            this.m_btnRefresh = new System.Windows.Forms.Button();
            this.m_treeFolders = new MZ.WinForms.FoldersTreeUserControl();
            this.m_ctxmnuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_mnuSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.m_imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_pnlMain.SuspendLayout();
            this.m_ctxmnuTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtSelectedFolder
            // 
            this.m_txtSelectedFolder.AcceptsReturn = true;
            this.m_txtSelectedFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSelectedFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.m_txtSelectedFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.m_txtSelectedFolder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtSelectedFolder.Location = new System.Drawing.Point(5, 5);
            this.m_txtSelectedFolder.Name = "m_txtSelectedFolder";
            this.m_txtSelectedFolder.Size = new System.Drawing.Size(310, 13);
            this.m_txtSelectedFolder.TabIndex = 0;
            this.m_txtSelectedFolder.TextChanged += new System.EventHandler(this.m_txtSelectedFolder_TextChanged);
            // 
            // m_btnNewFolder
            // 
            this.m_btnNewFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnNewFolder.Image = ((System.Drawing.Image)(resources.GetObject("m_btnNewFolder.Image")));
            this.m_btnNewFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnNewFolder.Location = new System.Drawing.Point(4, 319);
            this.m_btnNewFolder.Name = "m_btnNewFolder";
            this.m_btnNewFolder.Size = new System.Drawing.Size(87, 23);
            this.m_btnNewFolder.TabIndex = 4;
            this.m_btnNewFolder.Text = "New &Folder";
            this.m_btnNewFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnNewFolder.UseVisualStyleBackColor = true;
            this.m_btnNewFolder.Click += new System.EventHandler(this.m_btnNewFolder_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnOk.Location = new System.Drawing.Point(246, 319);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 5;
            this.m_btnOk.Text = "&Ok";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
            this.m_btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnCancel.Location = new System.Drawing.Point(327, 319);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 0;
            this.m_btnCancel.Text = "&Cancel";
            this.m_btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_txtDescription
            // 
            this.m_txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtDescription.Location = new System.Drawing.Point(5, 5);
            this.m_txtDescription.Multiline = false;
            this.m_txtDescription.Name = "m_txtDescription";
            this.m_txtDescription.ReadOnly = true;
            this.m_txtDescription.Size = new System.Drawing.Size(385, 14);
            this.m_txtDescription.TabIndex = 0;
            this.m_txtDescription.Text = "";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.m_txtDescription);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 26);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.m_txtSelectedFolder);
            this.panel2.Location = new System.Drawing.Point(5, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(322, 26);
            this.panel2.TabIndex = 3;
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pnlMain.Controls.Add(this.m_btnGoToFolder);
            this.m_pnlMain.Controls.Add(this.m_lblMessage);
            this.m_pnlMain.Controls.Add(this.m_progressBar);
            this.m_pnlMain.Controls.Add(this.m_btnRefresh);
            this.m_pnlMain.Controls.Add(this.m_treeFolders);
            this.m_pnlMain.Controls.Add(this.m_btnNewFolder);
            this.m_pnlMain.Controls.Add(this.m_btnOk);
            this.m_pnlMain.Controls.Add(this.m_btnCancel);
            this.m_pnlMain.Controls.Add(this.panel2);
            this.m_pnlMain.Controls.Add(this.panel1);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(409, 361);
            this.m_pnlMain.TabIndex = 0;
            // 
            // m_btnGoToFolder
            // 
            this.m_btnGoToFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnGoToFolder.Image = ((System.Drawing.Image)(resources.GetObject("m_btnGoToFolder.Image")));
            this.m_btnGoToFolder.Location = new System.Drawing.Point(333, 36);
            this.m_btnGoToFolder.Name = "m_btnGoToFolder";
            this.m_btnGoToFolder.Size = new System.Drawing.Size(33, 27);
            this.m_btnGoToFolder.TabIndex = 8;
            this.toolTip1.SetToolTip(this.m_btnGoToFolder, "Refresh (F5)");
            this.m_btnGoToFolder.UseVisualStyleBackColor = true;
            this.m_btnGoToFolder.Click += new System.EventHandler(this.m_btnGoToFolder_Click);
            // 
            // m_lblMessage
            // 
            this.m_lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblMessage.Location = new System.Drawing.Point(25, 153);
            this.m_lblMessage.Name = "m_lblMessage";
            this.m_lblMessage.Size = new System.Drawing.Size(355, 71);
            this.m_lblMessage.TabIndex = 1;
            this.m_lblMessage.Text = "Wait...";
            this.m_lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_progressBar
            // 
            themeColorSet6.Part1_ActiveColor = System.Drawing.SystemColors.HotTrack;
            themeColorSet6.Part1_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet6.Part2_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet6.Part2_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet6.Part3_ActiveColor = System.Drawing.Color.LimeGreen;
            themeColorSet6.Part3_InactiveColor = System.Drawing.Color.Gainsboro;
            themeColorSet6.Theme = MZ.WinForms.ColorBarsProgressBar.ColorsThemeType.Custom;
            themeColorSet6.Threshold1 = 100;
            themeColorSet6.Threshold2 = 100;
            this.m_progressBar.ColorTheme = themeColorSet6;
            this.m_progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_progressBar.Location = new System.Drawing.Point(0, 347);
            this.m_progressBar.Name = "m_progressBar";
            this.m_progressBar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.m_progressBar.Size = new System.Drawing.Size(407, 12);
            this.m_progressBar.TabIndex = 7;
            // 
            // m_btnRefresh
            // 
            this.m_btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("m_btnRefresh.Image")));
            this.m_btnRefresh.Location = new System.Drawing.Point(369, 36);
            this.m_btnRefresh.Name = "m_btnRefresh";
            this.m_btnRefresh.Size = new System.Drawing.Size(33, 27);
            this.m_btnRefresh.TabIndex = 6;
            this.toolTip1.SetToolTip(this.m_btnRefresh, "Refresh (F5)");
            this.m_btnRefresh.UseVisualStyleBackColor = true;
            this.m_btnRefresh.Click += new System.EventHandler(this.m_mnuRefresh_Click);
            // 
            // m_treeFolders
            // 
            this.m_treeFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_treeFolders.ContextMenuStrip = this.m_ctxmnuTree;
            this.m_treeFolders.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_treeFolders.Location = new System.Drawing.Point(4, 69);
            this.m_treeFolders.Name = "m_treeFolders";
            this.m_treeFolders.Size = new System.Drawing.Size(397, 246);
            this.m_treeFolders.TabIndex = 0;
            // 
            // m_ctxmnuTree
            // 
            this.m_ctxmnuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuSelect,
            this.toolStripMenuItem2,
            this.m_mnuRefresh,
            this.toolStripMenuItem1,
            this.m_mnuNewFolder,
            this.m_mnuRename,
            this.m_mnuDelete});
            this.m_ctxmnuTree.Name = "m_ctxmnuTree";
            this.m_ctxmnuTree.Size = new System.Drawing.Size(137, 126);
            // 
            // m_mnuSelect
            // 
            this.m_mnuSelect.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuSelect.Image")));
            this.m_mnuSelect.Name = "m_mnuSelect";
            this.m_mnuSelect.Size = new System.Drawing.Size(136, 22);
            this.m_mnuSelect.Text = "Select";
            this.m_mnuSelect.Click += new System.EventHandler(this.m_mnuSelect_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 6);
            // 
            // m_mnuRefresh
            // 
            this.m_mnuRefresh.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuRefresh.Image")));
            this.m_mnuRefresh.Name = "m_mnuRefresh";
            this.m_mnuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.m_mnuRefresh.Size = new System.Drawing.Size(136, 22);
            this.m_mnuRefresh.Text = "&Refresh";
            this.m_mnuRefresh.Click += new System.EventHandler(this.m_mnuRefresh_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 6);
            // 
            // m_mnuNewFolder
            // 
            this.m_mnuNewFolder.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuNewFolder.Image")));
            this.m_mnuNewFolder.Name = "m_mnuNewFolder";
            this.m_mnuNewFolder.Size = new System.Drawing.Size(136, 22);
            this.m_mnuNewFolder.Text = "New Folder";
            this.m_mnuNewFolder.Click += new System.EventHandler(this.m_mnuNewFolder_Click);
            // 
            // m_mnuRename
            // 
            this.m_mnuRename.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuRename.Image")));
            this.m_mnuRename.Name = "m_mnuRename";
            this.m_mnuRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.m_mnuRename.Size = new System.Drawing.Size(136, 22);
            this.m_mnuRename.Text = "Re&name";
            this.m_mnuRename.Click += new System.EventHandler(this.m_mnuRename_Click);
            // 
            // m_mnuDelete
            // 
            this.m_mnuDelete.Image = ((System.Drawing.Image)(resources.GetObject("m_mnuDelete.Image")));
            this.m_mnuDelete.Name = "m_mnuDelete";
            this.m_mnuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.m_mnuDelete.Size = new System.Drawing.Size(136, 22);
            this.m_mnuDelete.Text = "&Delete";
            this.m_mnuDelete.Click += new System.EventHandler(this.m_mnuDelete_Click);
            // 
            // m_imageListIcons
            // 
            this.m_imageListIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.m_imageListIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.m_imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FormBrowseForFolder
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(409, 361);
            this.Controls.Add(this.m_pnlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 400);
            this.Name = "FormBrowseForFolder";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Browse For Folder";
            this.Load += new System.EventHandler(this.FormBrowseForFolder_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.m_pnlMain.ResumeLayout(false);
            this.m_ctxmnuTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FoldersTreeUserControl m_treeFolders;
        private System.Windows.Forms.TextBox m_txtSelectedFolder;
        private System.Windows.Forms.Button m_btnNewFolder;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.RichTextBox m_txtDescription;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel m_pnlMain;
        private System.Windows.Forms.Button m_btnRefresh;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip m_ctxmnuTree;
        private System.Windows.Forms.ToolStripMenuItem m_mnuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem m_mnuRename;
        private System.Windows.Forms.ToolStripMenuItem m_mnuDelete;
        private System.Windows.Forms.ImageList m_imageListIcons;
        private WinForms.ColorBarsProgressBar m_progressBar;
        private System.Windows.Forms.Label m_lblMessage;
        private System.Windows.Forms.ToolStripMenuItem m_mnuSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem m_mnuNewFolder;
        private System.Windows.Forms.Button m_btnGoToFolder;
    }
}