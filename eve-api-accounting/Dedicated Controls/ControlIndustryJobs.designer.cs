namespace Accounting
{
    partial class ControlIndustryJobs
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
            BeforeDestroy();
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
            this.dgvIndustryJobs = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            this.rbCharIndustryJobs = new System.Windows.Forms.RadioButton();
            this.rbCorpIndustryJobs = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndustryJobs)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvIndustryJobs
            // 
            this.dgvIndustryJobs.AllowUserToAddRows = false;
            this.dgvIndustryJobs.AllowUserToDeleteRows = false;
            this.dgvIndustryJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIndustryJobs.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvIndustryJobs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIndustryJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIndustryJobs.Location = new System.Drawing.Point(3, 32);
            this.dgvIndustryJobs.Name = "dgvIndustryJobs";
            this.dgvIndustryJobs.Size = new System.Drawing.Size(633, 385);
            this.dgvIndustryJobs.TabIndex = 8;
            // 
            // bUpdate
            // 
            this.bUpdate.AutoSize = true;
            this.bUpdate.Location = new System.Drawing.Point(3, 3);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(107, 23);
            this.bUpdate.TabIndex = 9;
            this.bUpdate.Text = "Обновить данные";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // rbCharIndustryJobs
            // 
            this.rbCharIndustryJobs.AutoSize = true;
            this.rbCharIndustryJobs.Checked = true;
            this.rbCharIndustryJobs.Location = new System.Drawing.Point(174, 6);
            this.rbCharIndustryJobs.Name = "rbCharIndustryJobs";
            this.rbCharIndustryJobs.Size = new System.Drawing.Size(112, 17);
            this.rbCharIndustryJobs.TabIndex = 11;
            this.rbCharIndustryJobs.TabStop = true;
            this.rbCharIndustryJobs.Text = "Char Industry Jobs";
            this.rbCharIndustryJobs.UseVisualStyleBackColor = true;
            this.rbCharIndustryJobs.CheckedChanged += new System.EventHandler(this.rbIndustryJobsClick);
            // 
            // rbCorpIndustryJobs
            // 
            this.rbCorpIndustryJobs.AutoSize = true;
            this.rbCorpIndustryJobs.Location = new System.Drawing.Point(292, 6);
            this.rbCorpIndustryJobs.Name = "rbCorpIndustryJobs";
            this.rbCorpIndustryJobs.Size = new System.Drawing.Size(282, 17);
            this.rbCorpIndustryJobs.TabIndex = 12;
            this.rbCorpIndustryJobs.Text = "Corp Industry Jobs (требуется роль Factory Manager)";
            this.rbCorpIndustryJobs.UseVisualStyleBackColor = true;
            this.rbCorpIndustryJobs.CheckedChanged += new System.EventHandler(this.rbIndustryJobsClick);
            // 
            // ControlIndustryJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvIndustryJobs);
            this.Controls.Add(this.rbCorpIndustryJobs);
            this.Controls.Add(this.rbCharIndustryJobs);
            this.Controls.Add(this.bUpdate);
            this.Name = "ControlIndustryJobs";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndustryJobs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvIndustryJobs;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.RadioButton rbCharIndustryJobs;
        private System.Windows.Forms.RadioButton rbCorpIndustryJobs;
    }
}
