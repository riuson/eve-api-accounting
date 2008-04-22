namespace Accounting
{
    partial class CorpCorporationSheet
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
            this.dgvWalletDivisions = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDivisions = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lCorporationInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWalletDivisions)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDivisions)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvWalletDivisions
            // 
            this.dgvWalletDivisions.AllowUserToAddRows = false;
            this.dgvWalletDivisions.AllowUserToDeleteRows = false;
            this.dgvWalletDivisions.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvWalletDivisions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWalletDivisions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWalletDivisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWalletDivisions.Location = new System.Drawing.Point(3, 16);
            this.dgvWalletDivisions.Name = "dgvWalletDivisions";
            this.dgvWalletDivisions.Size = new System.Drawing.Size(291, 195);
            this.dgvWalletDivisions.TabIndex = 6;
            // 
            // bUpdate
            // 
            this.bUpdate.AutoSize = true;
            this.bUpdate.Location = new System.Drawing.Point(3, 3);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(107, 23);
            this.bUpdate.TabIndex = 7;
            this.bUpdate.Text = "Обновить данные";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvDivisions);
            this.groupBox1.Location = new System.Drawing.Point(3, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 214);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Divisions";
            // 
            // dgvDivisions
            // 
            this.dgvDivisions.AllowUserToAddRows = false;
            this.dgvDivisions.AllowUserToDeleteRows = false;
            this.dgvDivisions.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDivisions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDivisions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDivisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDivisions.Location = new System.Drawing.Point(3, 16);
            this.dgvDivisions.Name = "dgvDivisions";
            this.dgvDivisions.Size = new System.Drawing.Size(285, 195);
            this.dgvDivisions.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvWalletDivisions);
            this.groupBox2.Location = new System.Drawing.Point(300, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 214);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wallet Divisions";
            // 
            // lCorporationInfo
            // 
            this.lCorporationInfo.AutoSize = true;
            this.lCorporationInfo.Location = new System.Drawing.Point(3, 29);
            this.lCorporationInfo.Name = "lCorporationInfo";
            this.lCorporationInfo.Size = new System.Drawing.Size(25, 13);
            this.lCorporationInfo.TabIndex = 9;
            this.lCorporationInfo.Text = "Info";
            // 
            // CorpCorporationSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lCorporationInfo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bUpdate);
            this.Name = "CorpCorporationSheet";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWalletDivisions)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDivisions)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWalletDivisions;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDivisions;
        private System.Windows.Forms.Label lCorporationInfo;
    }
}
