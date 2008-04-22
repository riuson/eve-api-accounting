namespace Accounting
{
    partial class AccountBalances
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
            this.lWalletsStatus = new System.Windows.Forms.Label();
            this.dgvCharAccountBalance = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            this.dgvCorpAccountBalance = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCharAccountBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpAccountBalance)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lWalletsStatus
            // 
            this.lWalletsStatus.AutoSize = true;
            this.lWalletsStatus.Location = new System.Drawing.Point(120, 8);
            this.lWalletsStatus.Name = "lWalletsStatus";
            this.lWalletsStatus.Size = new System.Drawing.Size(11, 13);
            this.lWalletsStatus.TabIndex = 0;
            this.lWalletsStatus.Text = "*";
            // 
            // dgvCharAccountBalance
            // 
            this.dgvCharAccountBalance.AllowUserToAddRows = false;
            this.dgvCharAccountBalance.AllowUserToDeleteRows = false;
            this.dgvCharAccountBalance.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCharAccountBalance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCharAccountBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCharAccountBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCharAccountBalance.Location = new System.Drawing.Point(0, 0);
            this.dgvCharAccountBalance.Name = "dgvCharAccountBalance";
            this.dgvCharAccountBalance.ReadOnly = true;
            this.dgvCharAccountBalance.Size = new System.Drawing.Size(633, 86);
            this.dgvCharAccountBalance.TabIndex = 8;
            this.dgvCharAccountBalance.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnDataGridViewCellContentClick);
            // 
            // bUpdate
            // 
            this.bUpdate.AutoSize = true;
            this.bUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bUpdate.Location = new System.Drawing.Point(3, 3);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(111, 23);
            this.bUpdate.TabIndex = 9;
            this.bUpdate.Text = "Обновить данные";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // dgvCorpAccountBalance
            // 
            this.dgvCorpAccountBalance.AllowUserToAddRows = false;
            this.dgvCorpAccountBalance.AllowUserToDeleteRows = false;
            this.dgvCorpAccountBalance.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCorpAccountBalance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCorpAccountBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCorpAccountBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCorpAccountBalance.Location = new System.Drawing.Point(0, 13);
            this.dgvCorpAccountBalance.Name = "dgvCorpAccountBalance";
            this.dgvCorpAccountBalance.ReadOnly = true;
            this.dgvCorpAccountBalance.Size = new System.Drawing.Size(633, 282);
            this.dgvCorpAccountBalance.TabIndex = 10;
            this.dgvCorpAccountBalance.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnDataGridViewCellContentClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 32);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvCharAccountBalance);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvCorpAccountBalance);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(633, 385);
            this.splitContainer1.SplitterDistance = 86;
            this.splitContainer1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Требуется роль Junior Accounting или Accounting";
            // 
            // AccountBalances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.lWalletsStatus);
            this.Name = "AccountBalances";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCharAccountBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpAccountBalance)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lWalletsStatus;
        private System.Windows.Forms.DataGridView dgvCharAccountBalance;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.DataGridView dgvCorpAccountBalance;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
    }
}
