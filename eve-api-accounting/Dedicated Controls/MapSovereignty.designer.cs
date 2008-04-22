namespace Accounting
{
    partial class MapSovereignty
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
            this.dgvMapSovereignty = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapSovereignty)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMapSovereignty
            // 
            this.dgvMapSovereignty.AllowUserToAddRows = false;
            this.dgvMapSovereignty.AllowUserToDeleteRows = false;
            this.dgvMapSovereignty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMapSovereignty.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMapSovereignty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMapSovereignty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMapSovereignty.Location = new System.Drawing.Point(3, 32);
            this.dgvMapSovereignty.Name = "dgvMapSovereignty";
            this.dgvMapSovereignty.ReadOnly = true;
            this.dgvMapSovereignty.Size = new System.Drawing.Size(633, 385);
            this.dgvMapSovereignty.TabIndex = 8;
            this.dgvMapSovereignty.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMapSovereignty_DataError);
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
            // MapSovereignty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.dgvMapSovereignty);
            this.Name = "MapSovereignty";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapSovereignty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMapSovereignty;
        private System.Windows.Forms.Button bUpdate;
    }
}
