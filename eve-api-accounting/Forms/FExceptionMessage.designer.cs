namespace Accounting
{
    partial class FExceptionMessage
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
            this.bOk = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.lMessage = new System.Windows.Forms.Label();
            this.llChangeDisplayType = new System.Windows.Forms.LinkLabel();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bOk.Location = new System.Drawing.Point(390, 80);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 2;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            // 
            // tbMessage
            // 
            this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMessage.Location = new System.Drawing.Point(81, 10);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMessage.Size = new System.Drawing.Size(384, 59);
            this.tbMessage.TabIndex = 1;
            this.tbMessage.Text = "tbMessage";
            // 
            // lMessage
            // 
            this.lMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lMessage.Location = new System.Drawing.Point(81, 10);
            this.lMessage.Name = "lMessage";
            this.lMessage.Size = new System.Drawing.Size(384, 59);
            this.lMessage.TabIndex = 0;
            this.lMessage.Text = "lMessage";
            // 
            // llChangeDisplayType
            // 
            this.llChangeDisplayType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llChangeDisplayType.AutoSize = true;
            this.llChangeDisplayType.Location = new System.Drawing.Point(17, 83);
            this.llChangeDisplayType.Margin = new System.Windows.Forms.Padding(5);
            this.llChangeDisplayType.Name = "llChangeDisplayType";
            this.llChangeDisplayType.Size = new System.Drawing.Size(72, 13);
            this.llChangeDisplayType.TabIndex = 6;
            this.llChangeDisplayType.TabStop = true;
            this.llChangeDisplayType.Text = "Подробно >>";
            this.llChangeDisplayType.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llChangeDisplayType_LinkClicked);
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(20, 20);
            this.pbIcon.Margin = new System.Windows.Forms.Padding(10);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(48, 48);
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            // 
            // FExceptionMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(478, 116);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.llChangeDisplayType);
            this.Controls.Add(this.pbIcon);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.lMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FExceptionMessage";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Внимание";
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label lMessage;
        private System.Windows.Forms.LinkLabel llChangeDisplayType;
        private System.Windows.Forms.PictureBox pbIcon;
    }
}