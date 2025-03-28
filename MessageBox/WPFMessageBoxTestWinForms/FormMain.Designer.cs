﻿namespace WPFMessageBoxTestWinForms
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
            this.m_btnInput = new System.Windows.Forms.Button();
            this.m_cmbIconStyle = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_btnTimeout = new System.Windows.Forms.Button();
            this.m_numTimeout = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_numTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnInfo
            // 
            this.m_btnInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnInfo.Location = new System.Drawing.Point(67, 97);
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
            this.m_btnWarn.Location = new System.Drawing.Point(67, 154);
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
            this.m_btnError.Location = new System.Drawing.Point(67, 211);
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
            this.m_btnQuestion.Location = new System.Drawing.Point(67, 268);
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
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.LargeImageList = this.m_imageListIcons;
            this.listView1.Location = new System.Drawing.Point(274, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(546, 162);
            this.listView1.SmallImageList = this.m_imageListIcons;
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // m_btnQuestionYNC
            // 
            this.m_btnQuestionYNC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnQuestionYNC.Location = new System.Drawing.Point(67, 329);
            this.m_btnQuestionYNC.Name = "m_btnQuestionYNC";
            this.m_btnQuestionYNC.Size = new System.Drawing.Size(167, 45);
            this.m_btnQuestionYNC.TabIndex = 5;
            this.m_btnQuestionYNC.Text = "Question YNC";
            this.m_btnQuestionYNC.UseVisualStyleBackColor = true;
            this.m_btnQuestionYNC.Click += new System.EventHandler(this.m_btnQuestionYNC_Click);
            // 
            // m_btnSpecial
            // 
            this.m_btnSpecial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnSpecial.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnSpecial.Location = new System.Drawing.Point(274, 330);
            this.m_btnSpecial.Name = "m_btnSpecial";
            this.m_btnSpecial.Size = new System.Drawing.Size(167, 45);
            this.m_btnSpecial.TabIndex = 6;
            this.m_btnSpecial.Text = "Question YNC - long text";
            this.m_btnSpecial.UseVisualStyleBackColor = true;
            this.m_btnSpecial.Click += new System.EventHandler(this.m_btnSpecial_Click);
            // 
            // m_btnInput
            // 
            this.m_btnInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnInput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnInput.Location = new System.Drawing.Point(649, 330);
            this.m_btnInput.Name = "m_btnInput";
            this.m_btnInput.Size = new System.Drawing.Size(167, 45);
            this.m_btnInput.TabIndex = 7;
            this.m_btnInput.Text = "Input...";
            this.m_btnInput.UseVisualStyleBackColor = true;
            this.m_btnInput.Click += new System.EventHandler(this.m_btnInput_Click);
            // 
            // m_cmbIconStyle
            // 
            this.m_cmbIconStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbIconStyle.FormattingEnabled = true;
            this.m_cmbIconStyle.Location = new System.Drawing.Point(67, 59);
            this.m_cmbIconStyle.Name = "m_cmbIconStyle";
            this.m_cmbIconStyle.Size = new System.Drawing.Size(167, 21);
            this.m_cmbIconStyle.TabIndex = 8;
            this.m_cmbIconStyle.SelectionChangeCommitted += new System.EventHandler(this.m_cmbIconStyle_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Buttons Icon Style";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(274, 211);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(546, 103);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "\"The quick brown fox jumps over the lazy dog\"\n\"The quick brown fox jumps over the" +
    " lazy dog\"\n\"The quick brown fox jumps over the lazy dog\"\n\"The quick brown fox ju" +
    "mps over the lazy dog\"";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Message Text:";
            // 
            // m_btnTimeout
            // 
            this.m_btnTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnTimeout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnTimeout.Location = new System.Drawing.Point(465, 364);
            this.m_btnTimeout.Name = "m_btnTimeout";
            this.m_btnTimeout.Size = new System.Drawing.Size(167, 45);
            this.m_btnTimeout.TabIndex = 12;
            this.m_btnTimeout.Text = "Timeout 6 sec...";
            this.m_btnTimeout.UseVisualStyleBackColor = true;
            this.m_btnTimeout.Click += new System.EventHandler(this.m_btnTimeout_Click);
            // 
            // m_numTimeout
            // 
            this.m_numTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_numTimeout.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numTimeout.Location = new System.Drawing.Point(467, 334);
            this.m_numTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_numTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numTimeout.Name = "m_numTimeout";
            this.m_numTimeout.Size = new System.Drawing.Size(106, 20);
            this.m_numTimeout.TabIndex = 13;
            this.m_numTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numTimeout.Value = new decimal(new int[] {
            6100,
            0,
            0,
            0});
            this.m_numTimeout.ValueChanged += new System.EventHandler(this.m_numTimeout_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(598, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "mSec";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 421);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_numTimeout);
            this.Controls.Add(this.m_btnTimeout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_cmbIconStyle);
            this.Controls.Add(this.m_btnInput);
            this.Controls.Add(this.m_btnSpecial);
            this.Controls.Add(this.m_btnQuestionYNC);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.m_btnQuestion);
            this.Controls.Add(this.m_btnError);
            this.Controls.Add(this.m_btnWarn);
            this.Controls.Add(this.m_btnInfo);
            this.MinimumSize = new System.Drawing.Size(850, 454);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Message Box";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_numTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button m_btnInput;
        private System.Windows.Forms.ComboBox m_cmbIconStyle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_btnTimeout;
        private System.Windows.Forms.NumericUpDown m_numTimeout;
        private System.Windows.Forms.Label label3;
    }
}

