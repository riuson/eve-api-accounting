namespace Accounting
{
    partial class ControlKillLogs
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvKillLog = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            this.rbCharKillLog = new System.Windows.Forms.RadioButton();
            this.rbCorpKillLog = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKillLog)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvKillLog
            // 
            this.dgvKillLog.AllowUserToAddRows = false;
            this.dgvKillLog.AllowUserToDeleteRows = false;
            this.dgvKillLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKillLog.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvKillLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKillLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvKillLog.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKillLog.Location = new System.Drawing.Point(3, 32);
            this.dgvKillLog.Name = "dgvKillLog";
            this.dgvKillLog.ReadOnly = true;
            this.dgvKillLog.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKillLog.Size = new System.Drawing.Size(633, 385);
            this.dgvKillLog.TabIndex = 8;
            this.dgvKillLog.DoubleClick += new System.EventHandler(this.dgvKillLog_DoubleClick);
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
            // rbCharKillLog
            // 
            this.rbCharKillLog.AutoSize = true;
            this.rbCharKillLog.Checked = true;
            this.rbCharKillLog.Location = new System.Drawing.Point(175, 6);
            this.rbCharKillLog.Name = "rbCharKillLog";
            this.rbCharKillLog.Size = new System.Drawing.Size(84, 17);
            this.rbCharKillLog.TabIndex = 10;
            this.rbCharKillLog.TabStop = true;
            this.rbCharKillLog.Text = "Char Kill Log";
            this.rbCharKillLog.UseVisualStyleBackColor = true;
            this.rbCharKillLog.Click += new System.EventHandler(this.rbCharKillLog_Click);
            // 
            // rbCorpKillLog
            // 
            this.rbCorpKillLog.AutoSize = true;
            this.rbCorpKillLog.Location = new System.Drawing.Point(265, 6);
            this.rbCorpKillLog.Name = "rbCorpKillLog";
            this.rbCorpKillLog.Size = new System.Drawing.Size(257, 17);
            this.rbCorpKillLog.TabIndex = 10;
            this.rbCorpKillLog.Text = "Corp Kill Log (требуется роль Director или CEO)";
            this.rbCorpKillLog.UseVisualStyleBackColor = true;
            this.rbCorpKillLog.Click += new System.EventHandler(this.rbCharKillLog_Click);
            this.rbCorpKillLog.CheckedChanged += new System.EventHandler(this.rbCharKillLog_Click);
            // 
            // ControlKillLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbCorpKillLog);
            this.Controls.Add(this.rbCharKillLog);
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.dgvKillLog);
            this.Name = "ControlKillLogs";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKillLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvKillLog;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.RadioButton rbCharKillLog;
        private System.Windows.Forms.RadioButton rbCorpKillLog;
    }
}
