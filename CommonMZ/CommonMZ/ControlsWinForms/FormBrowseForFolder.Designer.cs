namespace MZ.ControlsWinForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBrowseForFolder));
            this.m_treeFolders = new MZ.ControlsWinForms.FoldersTreeUserControl();
            this.m_txtSelectedFolder = new System.Windows.Forms.RichTextBox();
            this.m_btnNewFolder = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_txtDescription = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // m_treeFolders
            // 
            this.m_treeFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_treeFolders.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_treeFolders.Location = new System.Drawing.Point(5, 46);
            this.m_treeFolders.Name = "m_treeFolders";
            this.m_treeFolders.Size = new System.Drawing.Size(274, 256);
            this.m_treeFolders.TabIndex = 0;
            // 
            // m_txtSelectedFolder
            // 
            this.m_txtSelectedFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSelectedFolder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtSelectedFolder.Location = new System.Drawing.Point(5, 307);
            this.m_txtSelectedFolder.Multiline = false;
            this.m_txtSelectedFolder.Name = "m_txtSelectedFolder";
            this.m_txtSelectedFolder.ReadOnly = true;
            this.m_txtSelectedFolder.Size = new System.Drawing.Size(274, 21);
            this.m_txtSelectedFolder.TabIndex = 1;
            this.m_txtSelectedFolder.Text = "";
            // 
            // m_btnNewFolder
            // 
            this.m_btnNewFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnNewFolder.Location = new System.Drawing.Point(4, 329);
            this.m_btnNewFolder.Name = "m_btnNewFolder";
            this.m_btnNewFolder.Size = new System.Drawing.Size(75, 23);
            this.m_btnNewFolder.TabIndex = 2;
            this.m_btnNewFolder.Text = "New &Folder";
            this.m_btnNewFolder.UseVisualStyleBackColor = true;
            this.m_btnNewFolder.Click += new System.EventHandler(this.m_btnNewFolder_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOk.Location = new System.Drawing.Point(123, 329);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "&Ok";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(204, 329);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "&Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_txtDescription
            // 
            this.m_txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtDescription.Location = new System.Drawing.Point(5, 12);
            this.m_txtDescription.Multiline = false;
            this.m_txtDescription.Name = "m_txtDescription";
            this.m_txtDescription.ReadOnly = true;
            this.m_txtDescription.Size = new System.Drawing.Size(274, 28);
            this.m_txtDescription.TabIndex = 5;
            this.m_txtDescription.Text = "";
            // 
            // FormBrowseForFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.m_txtDescription);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_btnNewFolder);
            this.Controls.Add(this.m_txtSelectedFolder);
            this.Controls.Add(this.m_treeFolders);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 400);
            this.Name = "FormBrowseForFolder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Browse For Folder";
            this.Load += new System.EventHandler(this.FormBrowseForFolder_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FoldersTreeUserControl m_treeFolders;
        private System.Windows.Forms.RichTextBox m_txtSelectedFolder;
        private System.Windows.Forms.Button m_btnNewFolder;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.RichTextBox m_txtDescription;
    }
}