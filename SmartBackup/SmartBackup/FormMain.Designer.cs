namespace SmartBackup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.m_tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_pnlTools = new System.Windows.Forms.Panel();
            this.m_btnEdit = new System.Windows.Forms.Button();
            this.m_btnRemove = new System.Windows.Forms.Button();
            this.m_btnAdd = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.m_tabMain.SuspendLayout();
            this.m_pnlTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabMain
            // 
            this.m_tabMain.Controls.Add(this.tabPage1);
            this.m_tabMain.Controls.Add(this.tabPage2);
            this.m_tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabMain.ImageList = this.imageList1;
            this.m_tabMain.Location = new System.Drawing.Point(0, 38);
            this.m_tabMain.Name = "m_tabMain";
            this.m_tabMain.SelectedIndex = 0;
            this.m_tabMain.Size = new System.Drawing.Size(800, 403);
            this.m_tabMain.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.ImageIndex = 5;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 376);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.ImageIndex = 3;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 376);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_pnlTools
            // 
            this.m_pnlTools.BackColor = System.Drawing.SystemColors.ControlDark;
            this.m_pnlTools.Controls.Add(this.m_btnEdit);
            this.m_pnlTools.Controls.Add(this.m_btnRemove);
            this.m_pnlTools.Controls.Add(this.m_btnAdd);
            this.m_pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTools.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTools.Name = "m_pnlTools";
            this.m_pnlTools.Size = new System.Drawing.Size(800, 38);
            this.m_pnlTools.TabIndex = 0;
            // 
            // m_btnEdit
            // 
            this.m_btnEdit.Location = new System.Drawing.Point(172, 7);
            this.m_btnEdit.Name = "m_btnEdit";
            this.m_btnEdit.Size = new System.Drawing.Size(75, 23);
            this.m_btnEdit.TabIndex = 2;
            this.m_btnEdit.Text = "Edit Group...";
            this.m_btnEdit.UseVisualStyleBackColor = true;
            this.m_btnEdit.Click += new System.EventHandler(this.m_btnEdit_Click);
            // 
            // m_btnRemove
            // 
            this.m_btnRemove.Location = new System.Drawing.Point(91, 7);
            this.m_btnRemove.Name = "m_btnRemove";
            this.m_btnRemove.Size = new System.Drawing.Size(75, 23);
            this.m_btnRemove.TabIndex = 1;
            this.m_btnRemove.Text = "Remove...";
            this.m_btnRemove.UseVisualStyleBackColor = true;
            this.m_btnRemove.Click += new System.EventHandler(this.m_btnRemove_Click);
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Location = new System.Drawing.Point(10, 7);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(75, 23);
            this.m_btnAdd.TabIndex = 0;
            this.m_btnAdd.Text = "Add Group...";
            this.m_btnAdd.UseVisualStyleBackColor = true;
            this.m_btnAdd.Click += new System.EventHandler(this.m_btnAdd_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Blacklisted.ico");
            this.imageList1.Images.SetKeyName(1, "Shared.ico");
            this.imageList1.Images.SetKeyName(2, "Syncing.ico");
            this.imageList1.Images.SetKeyName(3, "Provider.ico");
            this.imageList1.Images.SetKeyName(4, "propertysheets.ico");
            this.imageList1.Images.SetKeyName(5, "computer.ico");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(800, 441);
            this.Controls.Add(this.m_tabMain);
            this.Controls.Add(this.m_pnlTools);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Backup";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.m_tabMain.ResumeLayout(false);
            this.m_pnlTools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl m_tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private BackupListUserControl backupListUserControl1;
        private System.Windows.Forms.Panel m_pnlTools;
        private System.Windows.Forms.Button m_btnAdd;
        private System.Windows.Forms.Button m_btnEdit;
        private System.Windows.Forms.Button m_btnRemove;
        private System.Windows.Forms.ImageList imageList1;
    }
}

