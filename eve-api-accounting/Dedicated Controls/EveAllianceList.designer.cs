namespace Accounting
{
    partial class EveAllianceList
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
            this.dgvAllianceList = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllianceList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAllianceList
            // 
            this.dgvAllianceList.AllowUserToAddRows = false;
            this.dgvAllianceList.AllowUserToDeleteRows = false;
            this.dgvAllianceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAllianceList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAllianceList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAllianceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllianceList.Location = new System.Drawing.Point(3, 32);
            this.dgvAllianceList.Name = "dgvAllianceList";
            this.dgvAllianceList.ReadOnly = true;
            this.dgvAllianceList.Size = new System.Drawing.Size(633, 388);
            this.dgvAllianceList.TabIndex = 7;
            // 
            // bUpdate
            // 
            this.bUpdate.AutoSize = true;
            this.bUpdate.Location = new System.Drawing.Point(3, 3);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(107, 23);
            this.bUpdate.TabIndex = 8;
            this.bUpdate.Text = "Обновить данные";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // EveAllianceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.dgvAllianceList);
            this.Name = "EveAllianceList";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllianceList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAllianceList;
        private System.Windows.Forms.Button bUpdate;
    }
}
