namespace Accounting
{
    partial class ControlKillLogView
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
            this.dgvKillLogAttackers = new System.Windows.Forms.DataGridView();
            this.lInfoVictim = new System.Windows.Forms.Label();
            this.pbVictim = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvKillLogItems = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKillLogAttackers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVictim)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKillLogItems)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvKillLogAttackers
            // 
            this.dgvKillLogAttackers.AllowUserToAddRows = false;
            this.dgvKillLogAttackers.AllowUserToDeleteRows = false;
            this.dgvKillLogAttackers.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvKillLogAttackers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKillLogAttackers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKillLogAttackers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKillLogAttackers.Location = new System.Drawing.Point(3, 16);
            this.dgvKillLogAttackers.Name = "dgvKillLogAttackers";
            this.dgvKillLogAttackers.ReadOnly = true;
            this.dgvKillLogAttackers.Size = new System.Drawing.Size(332, 264);
            this.dgvKillLogAttackers.TabIndex = 8;
            // 
            // lInfoVictim
            // 
            this.lInfoVictim.AutoSize = true;
            this.lInfoVictim.Location = new System.Drawing.Point(76, 19);
            this.lInfoVictim.Name = "lInfoVictim";
            this.lInfoVictim.Size = new System.Drawing.Size(167, 91);
            this.lInfoVictim.TabIndex = 10;
            this.lInfoVictim.Text = "Date: 22.12.2007 17:12:00\r\nVictim:MarzenBier\r\nCorporation: 3B Legio IX\r\nAlliance:" +
                " <GetName(284278305)>\r\nShip: Drake (Battlecruiser)\r\nLocation: Nalnifan, Security" +
                ": 0,3\r\nDamage taken: 82435";
            // 
            // pbVictim
            // 
            this.pbVictim.Location = new System.Drawing.Point(6, 19);
            this.pbVictim.Name = "pbVictim";
            this.pbVictim.Size = new System.Drawing.Size(64, 64);
            this.pbVictim.TabIndex = 11;
            this.pbVictim.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbVictim);
            this.groupBox1.Controls.Add(this.lInfoVictim);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 125);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Victim";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.dgvKillLogAttackers);
            this.groupBox2.Location = new System.Drawing.Point(3, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 283);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Involved parties";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvKillLogItems);
            this.groupBox3.Location = new System.Drawing.Point(347, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(338, 414);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Items";
            // 
            // dgvKillLogItems
            // 
            this.dgvKillLogItems.AllowUserToAddRows = false;
            this.dgvKillLogItems.AllowUserToDeleteRows = false;
            this.dgvKillLogItems.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvKillLogItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKillLogItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKillLogItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKillLogItems.Location = new System.Drawing.Point(3, 16);
            this.dgvKillLogItems.Name = "dgvKillLogItems";
            this.dgvKillLogItems.ReadOnly = true;
            this.dgvKillLogItems.Size = new System.Drawing.Size(332, 395);
            this.dgvKillLogItems.TabIndex = 8;
            this.dgvKillLogItems.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvKillLogItems_CellPainting);
            // 
            // ControlKillLogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ControlKillLogView";
            this.Size = new System.Drawing.Size(695, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKillLogAttackers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVictim)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKillLogItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvKillLogAttackers;
        private System.Windows.Forms.Label lInfoVictim;
        private System.Windows.Forms.PictureBox pbVictim;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvKillLogItems;
    }
}
