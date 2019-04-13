namespace DiskCryptorHelper
{
    partial class HideDriveLetterControl
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
            this.m_listDriveLetters = new System.Windows.Forms.ListView();
            this.m_clmnDriveLetter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_clmnDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_listDriveLetters
            // 
            this.m_listDriveLetters.CheckBoxes = true;
            this.m_listDriveLetters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_clmnDriveLetter,
            this.m_clmnDescription});
            this.m_listDriveLetters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listDriveLetters.FullRowSelect = true;
            this.m_listDriveLetters.GridLines = true;
            this.m_listDriveLetters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listDriveLetters.Location = new System.Drawing.Point(0, 18);
            this.m_listDriveLetters.Name = "m_listDriveLetters";
            this.m_listDriveLetters.Size = new System.Drawing.Size(98, 384);
            this.m_listDriveLetters.TabIndex = 8;
            this.m_listDriveLetters.UseCompatibleStateImageBehavior = false;
            this.m_listDriveLetters.View = System.Windows.Forms.View.Details;
            this.m_listDriveLetters.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.m_listDriveLetters_ItemChecked);
            // 
            // m_clmnDriveLetter
            // 
            this.m_clmnDriveLetter.Text = "Drive:";
            this.m_clmnDriveLetter.Width = 40;
            // 
            // m_clmnDescription
            // 
            this.m_clmnDescription.Text = "Description";
            this.m_clmnDescription.Width = 200;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hide Drives:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HideDriveLetterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_listDriveLetters);
            this.Controls.Add(this.label2);
            this.Name = "HideDriveLetterControl";
            this.Size = new System.Drawing.Size(98, 402);
            this.Load += new System.EventHandler(this.HideDriveLetterControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView m_listDriveLetters;
        private System.Windows.Forms.ColumnHeader m_clmnDriveLetter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader m_clmnDescription;
    }
}
