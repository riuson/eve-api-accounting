namespace Accounting
{
    partial class CorpMemberTracking
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
            this.bUpdate = new System.Windows.Forms.Button();
            this.dgvCorpMemberTracking = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpMemberTracking)).BeginInit();
            this.SuspendLayout();
            // 
            // bUpdate
            // 
            this.bUpdate.AutoSize = true;
            this.bUpdate.Location = new System.Drawing.Point(3, 3);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(110, 23);
            this.bUpdate.TabIndex = 8;
            this.bUpdate.Text = "Загрузить данные";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // dgvCorpMemberTracking
            // 
            this.dgvCorpMemberTracking.AllowUserToAddRows = false;
            this.dgvCorpMemberTracking.AllowUserToDeleteRows = false;
            this.dgvCorpMemberTracking.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCorpMemberTracking.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCorpMemberTracking.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCorpMemberTracking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCorpMemberTracking.Location = new System.Drawing.Point(3, 32);
            this.dgvCorpMemberTracking.Name = "dgvCorpMemberTracking";
            this.dgvCorpMemberTracking.ReadOnly = true;
            this.dgvCorpMemberTracking.Size = new System.Drawing.Size(633, 385);
            this.dgvCorpMemberTracking.TabIndex = 9;
            // 
            // CorpMemberTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvCorpMemberTracking);
            this.Controls.Add(this.bUpdate);
            this.Name = "CorpMemberTracking";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpMemberTracking)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.DataGridView dgvCorpMemberTracking;

    }
}
