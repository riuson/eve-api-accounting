namespace Accounting
{
    partial class ControlAssets
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
            this.dgvAssetList = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            this.rbCorpAssets = new System.Windows.Forms.RadioButton();
            this.rbCharAssets = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssetList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAssetList
            // 
            this.dgvAssetList.AllowUserToAddRows = false;
            this.dgvAssetList.AllowUserToDeleteRows = false;
            this.dgvAssetList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAssetList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAssetList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAssetList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssetList.Location = new System.Drawing.Point(3, 32);
            this.dgvAssetList.Name = "dgvAssetList";
            this.dgvAssetList.ReadOnly = true;
            this.dgvAssetList.Size = new System.Drawing.Size(633, 385);
            this.dgvAssetList.TabIndex = 8;
            this.dgvAssetList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAssetList_CellDoubleClick);
            this.dgvAssetList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvAssetList_KeyUp);
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
            // rbCorpIndustryJobs
            // 
            this.rbCorpAssets.AutoSize = true;
            this.rbCorpAssets.Location = new System.Drawing.Point(260, 6);
            this.rbCorpAssets.Name = "rbCorpIndustryJobs";
            this.rbCorpAssets.Size = new System.Drawing.Size(254, 17);
            this.rbCorpAssets.TabIndex = 14;
            this.rbCorpAssets.Text = "Corp Assets (требуется роль Director или CEO)";
            this.rbCorpAssets.UseVisualStyleBackColor = true;
            this.rbCorpAssets.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // rbCharIndustryJobs
            // 
            this.rbCharAssets.AutoSize = true;
            this.rbCharAssets.Checked = true;
            this.rbCharAssets.Location = new System.Drawing.Point(173, 6);
            this.rbCharAssets.Name = "rbCharIndustryJobs";
            this.rbCharAssets.Size = new System.Drawing.Size(81, 17);
            this.rbCharAssets.TabIndex = 13;
            this.rbCharAssets.TabStop = true;
            this.rbCharAssets.Text = "Char Assets";
            this.rbCharAssets.UseVisualStyleBackColor = true;
            this.rbCharAssets.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // ControlAssets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbCorpAssets);
            this.Controls.Add(this.rbCharAssets);
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.dgvAssetList);
            this.Name = "ControlAssets";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssetList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAssetList;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.RadioButton rbCorpAssets;
        private System.Windows.Forms.RadioButton rbCharAssets;
    }
}
