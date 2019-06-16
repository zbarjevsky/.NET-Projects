namespace FileSystemHelper
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.m_tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_brtnRenameMove = new System.Windows.Forms.Button();
            this.m_txtDestination = new System.Windows.Forms.TextBox();
            this.m_btnBrowse = new System.Windows.Forms.Button();
            this.m_txtOriginal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.m_btnDelete = new System.Windows.Forms.Button();
            this.m_tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabMain
            // 
            this.m_tabMain.Controls.Add(this.tabPage1);
            this.m_tabMain.Controls.Add(this.tabPage2);
            this.m_tabMain.Controls.Add(this.tabPage3);
            this.m_tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabMain.Location = new System.Drawing.Point(0, 0);
            this.m_tabMain.Name = "m_tabMain";
            this.m_tabMain.SelectedIndex = 0;
            this.m_tabMain.Size = new System.Drawing.Size(637, 253);
            this.m_tabMain.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_btnDelete);
            this.tabPage1.Controls.Add(this.m_brtnRenameMove);
            this.tabPage1.Controls.Add(this.m_txtDestination);
            this.tabPage1.Controls.Add(this.m_btnBrowse);
            this.tabPage1.Controls.Add(this.m_txtOriginal);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(629, 224);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Rename/Move";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_brtnRenameMove
            // 
            this.m_brtnRenameMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_brtnRenameMove.Location = new System.Drawing.Point(426, 118);
            this.m_brtnRenameMove.Name = "m_brtnRenameMove";
            this.m_brtnRenameMove.Size = new System.Drawing.Size(175, 33);
            this.m_brtnRenameMove.TabIndex = 4;
            this.m_brtnRenameMove.Text = "Rename/Move(1000)";
            this.m_brtnRenameMove.UseVisualStyleBackColor = true;
            this.m_brtnRenameMove.Click += new System.EventHandler(this.m_brtnRenameMove_Click);
            // 
            // m_txtDestination
            // 
            this.m_txtDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtDestination.Location = new System.Drawing.Point(109, 79);
            this.m_txtDestination.Name = "m_txtDestination";
            this.m_txtDestination.Size = new System.Drawing.Size(492, 22);
            this.m_txtDestination.TabIndex = 3;
            // 
            // m_btnBrowse
            // 
            this.m_btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowse.Location = new System.Drawing.Point(562, 33);
            this.m_btnBrowse.Name = "m_btnBrowse";
            this.m_btnBrowse.Size = new System.Drawing.Size(39, 25);
            this.m_btnBrowse.TabIndex = 2;
            this.m_btnBrowse.Text = "...";
            this.m_btnBrowse.UseVisualStyleBackColor = true;
            this.m_btnBrowse.Click += new System.EventHandler(this.m_btnBrowse_Click);
            // 
            // m_txtOriginal
            // 
            this.m_txtOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtOriginal.Location = new System.Drawing.Point(109, 34);
            this.m_txtOriginal.Name = "m_txtOriginal";
            this.m_txtOriginal.Size = new System.Drawing.Size(444, 22);
            this.m_txtOriginal.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Name:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(655, 295);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Delete";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(655, 295);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // m_btnDelete
            // 
            this.m_btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDelete.Location = new System.Drawing.Point(426, 167);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Size = new System.Drawing.Size(175, 33);
            this.m_btnDelete.TabIndex = 5;
            this.m_btnDelete.Text = "Delete(1000)";
            this.m_btnDelete.UseVisualStyleBackColor = true;
            this.m_btnDelete.Click += new System.EventHandler(this.m_btnDelete_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 253);
            this.Controls.Add(this.m_tabMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File System Tools";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.m_tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl m_tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button m_brtnRenameMove;
        private System.Windows.Forms.TextBox m_txtDestination;
        private System.Windows.Forms.Button m_btnBrowse;
        private System.Windows.Forms.TextBox m_txtOriginal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button m_btnDelete;
    }
}

