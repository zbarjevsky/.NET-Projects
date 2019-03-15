namespace WPFMessageBoxTestWinForms
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
            this.m_btnInfo = new System.Windows.Forms.Button();
            this.m_btnWarn = new System.Windows.Forms.Button();
            this.m_btnError = new System.Windows.Forms.Button();
            this.m_btnQuestion = new System.Windows.Forms.Button();
            this.m_imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_btnQuestionYNC = new System.Windows.Forms.Button();
            this.m_btnSpecial = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_btnInfo
            // 
            this.m_btnInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnInfo.Location = new System.Drawing.Point(67, 37);
            this.m_btnInfo.Name = "m_btnInfo";
            this.m_btnInfo.Size = new System.Drawing.Size(167, 45);
            this.m_btnInfo.TabIndex = 0;
            this.m_btnInfo.Text = "Info";
            this.m_btnInfo.UseVisualStyleBackColor = true;
            this.m_btnInfo.Click += new System.EventHandler(this.m_btnInfo_Click);
            // 
            // m_btnWarn
            // 
            this.m_btnWarn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnWarn.Location = new System.Drawing.Point(67, 94);
            this.m_btnWarn.Name = "m_btnWarn";
            this.m_btnWarn.Size = new System.Drawing.Size(167, 45);
            this.m_btnWarn.TabIndex = 1;
            this.m_btnWarn.Text = "Exclamation";
            this.m_btnWarn.UseVisualStyleBackColor = true;
            this.m_btnWarn.Click += new System.EventHandler(this.m_btnWarn_Click);
            // 
            // m_btnError
            // 
            this.m_btnError.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnError.Location = new System.Drawing.Point(67, 151);
            this.m_btnError.Name = "m_btnError";
            this.m_btnError.Size = new System.Drawing.Size(167, 45);
            this.m_btnError.TabIndex = 2;
            this.m_btnError.Text = "Error";
            this.m_btnError.UseVisualStyleBackColor = true;
            this.m_btnError.Click += new System.EventHandler(this.m_btnError_Click);
            // 
            // m_btnQuestion
            // 
            this.m_btnQuestion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnQuestion.Location = new System.Drawing.Point(67, 208);
            this.m_btnQuestion.Name = "m_btnQuestion";
            this.m_btnQuestion.Size = new System.Drawing.Size(167, 45);
            this.m_btnQuestion.TabIndex = 3;
            this.m_btnQuestion.Text = "Question";
            this.m_btnQuestion.UseVisualStyleBackColor = true;
            this.m_btnQuestion.Click += new System.EventHandler(this.m_btnQuestion_Click);
            // 
            // m_imageListIcons
            // 
            this.m_imageListIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.m_imageListIcons.ImageSize = new System.Drawing.Size(32, 32);
            this.m_imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.LargeImageList = this.m_imageListIcons;
            this.listView1.Location = new System.Drawing.Point(274, 37);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(346, 216);
            this.listView1.SmallImageList = this.m_imageListIcons;
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // m_btnQuestionYNC
            // 
            this.m_btnQuestionYNC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnQuestionYNC.Location = new System.Drawing.Point(67, 269);
            this.m_btnQuestionYNC.Name = "m_btnQuestionYNC";
            this.m_btnQuestionYNC.Size = new System.Drawing.Size(167, 45);
            this.m_btnQuestionYNC.TabIndex = 5;
            this.m_btnQuestionYNC.Text = "Question YNC";
            this.m_btnQuestionYNC.UseVisualStyleBackColor = true;
            this.m_btnQuestionYNC.Click += new System.EventHandler(this.m_btnQuestionYNC_Click);
            // 
            // m_btnSpecial
            // 
            this.m_btnSpecial.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnSpecial.Location = new System.Drawing.Point(274, 269);
            this.m_btnSpecial.Name = "m_btnSpecial";
            this.m_btnSpecial.Size = new System.Drawing.Size(167, 45);
            this.m_btnSpecial.TabIndex = 6;
            this.m_btnSpecial.Text = "Question YNC - long text";
            this.m_btnSpecial.UseVisualStyleBackColor = true;
            this.m_btnSpecial.Click += new System.EventHandler(this.m_btnSpecial_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 354);
            this.Controls.Add(this.m_btnSpecial);
            this.Controls.Add(this.m_btnQuestionYNC);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.m_btnQuestion);
            this.Controls.Add(this.m_btnError);
            this.Controls.Add(this.m_btnWarn);
            this.Controls.Add(this.m_btnInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Message Box";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_btnInfo;
        private System.Windows.Forms.Button m_btnWarn;
        private System.Windows.Forms.Button m_btnError;
        private System.Windows.Forms.Button m_btnQuestion;
        private System.Windows.Forms.ImageList m_imageListIcons;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button m_btnQuestionYNC;
        private System.Windows.Forms.Button m_btnSpecial;
    }
}

