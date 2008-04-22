namespace Accounting
{
    partial class ControlTowerFuelPrices
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
            this.dgvFuelPrices = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFuelPrices)).BeginInit();
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
            // dgvFuelPrices
            // 
            this.dgvFuelPrices.AllowUserToAddRows = false;
            this.dgvFuelPrices.AllowUserToDeleteRows = false;
            this.dgvFuelPrices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFuelPrices.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvFuelPrices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFuelPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFuelPrices.Location = new System.Drawing.Point(3, 32);
            this.dgvFuelPrices.Name = "dgvFuelPrices";
            this.dgvFuelPrices.Size = new System.Drawing.Size(633, 385);
            this.dgvFuelPrices.TabIndex = 8;
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
            // ControlTowerFuelPrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.dgvFuelPrices);
            this.Controls.Add(this.label1);
            this.Name = "ControlTowerFuelPrices";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFuelPrices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvFuelPrices;
        private System.Windows.Forms.Button bUpdate;
    }
}
