namespace Accounting
{
    partial class EveConquerableStationsList
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
            this.dgvEveConquerableStationsList = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEveConquerableStationsList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEveConquerableStationsList
            // 
            this.dgvEveConquerableStationsList.AllowUserToAddRows = false;
            this.dgvEveConquerableStationsList.AllowUserToDeleteRows = false;
            this.dgvEveConquerableStationsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEveConquerableStationsList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvEveConquerableStationsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEveConquerableStationsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEveConquerableStationsList.Location = new System.Drawing.Point(3, 32);
            this.dgvEveConquerableStationsList.Name = "dgvEveConquerableStationsList";
            this.dgvEveConquerableStationsList.ReadOnly = true;
            this.dgvEveConquerableStationsList.Size = new System.Drawing.Size(633, 385);
            this.dgvEveConquerableStationsList.TabIndex = 8;
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
            // EveConquerableStationsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.dgvEveConquerableStationsList);
            this.Name = "EveConquerableStationsList";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEveConquerableStationsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEveConquerableStationsList;
        private System.Windows.Forms.Button bUpdate;
    }
}
