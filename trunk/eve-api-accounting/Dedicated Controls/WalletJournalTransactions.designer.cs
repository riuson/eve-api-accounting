namespace Accounting
{
    partial class WalletJournalTransactions
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvWalletJournal = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bWalletFilterApply = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpWalletFilterLow = new System.Windows.Forms.DateTimePicker();
            this.dtpWalletFilterTop = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWalletJournal)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Базовый контрол";
            // 
            // dgvWalletJournal
            // 
            this.dgvWalletJournal.AllowUserToAddRows = false;
            this.dgvWalletJournal.AllowUserToDeleteRows = false;
            this.dgvWalletJournal.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvWalletJournal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWalletJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWalletJournal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWalletJournal.Location = new System.Drawing.Point(0, 33);
            this.dgvWalletJournal.Name = "dgvWalletJournal";
            this.dgvWalletJournal.ReadOnly = true;
            this.dgvWalletJournal.Size = new System.Drawing.Size(639, 387);
            this.dgvWalletJournal.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bWalletFilterApply);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtpWalletFilterLow);
            this.panel1.Controls.Add(this.dtpWalletFilterTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 33);
            this.panel1.TabIndex = 10;
            // 
            // bWalletFilterApply
            // 
            this.bWalletFilterApply.AutoSize = true;
            this.bWalletFilterApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bWalletFilterApply.Location = new System.Drawing.Point(519, 4);
            this.bWalletFilterApply.Name = "bWalletFilterApply";
            this.bWalletFilterApply.Size = new System.Drawing.Size(109, 23);
            this.bWalletFilterApply.TabIndex = 6;
            this.bWalletFilterApply.Text = "Filter";
            this.bWalletFilterApply.UseVisualStyleBackColor = true;
            this.bWalletFilterApply.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "From:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(277, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "To:";
            // 
            // dtpWalletFilterLow
            // 
            this.dtpWalletFilterLow.CustomFormat = "dd MMMM yyyy г. - HH:mm";
            this.dtpWalletFilterLow.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWalletFilterLow.Location = new System.Drawing.Point(48, 5);
            this.dtpWalletFilterLow.Name = "dtpWalletFilterLow";
            this.dtpWalletFilterLow.Size = new System.Drawing.Size(188, 20);
            this.dtpWalletFilterLow.TabIndex = 5;
            // 
            // dtpWalletFilterTop
            // 
            this.dtpWalletFilterTop.CustomFormat = "dd MMMM yyyy г. - HH:mm";
            this.dtpWalletFilterTop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWalletFilterTop.Location = new System.Drawing.Point(306, 5);
            this.dtpWalletFilterTop.Name = "dtpWalletFilterTop";
            this.dtpWalletFilterTop.Size = new System.Drawing.Size(188, 20);
            this.dtpWalletFilterTop.TabIndex = 5;
            // 
            // WalletJournalTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvWalletJournal);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "WalletJournalTransactions";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWalletJournal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvWalletJournal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bWalletFilterApply;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpWalletFilterLow;
        private System.Windows.Forms.DateTimePicker dtpWalletFilterTop;
    }
}
