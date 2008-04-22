namespace Accounting
{
    partial class CorpStarbaseListConfig
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
            this.dgvCorpStarbaseList = new System.Windows.Forms.DataGridView();
            this.bUpdate = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pbTowerPower = new System.Windows.Forms.ProgressBar();
            this.pbTowerCPU = new System.Windows.Forms.ProgressBar();
            this.lTowerPower = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lTowerCPU = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvUndefinedStructures = new System.Windows.Forms.DataGridView();
            this.dgvDefinedStructures = new System.Windows.Forms.DataGridView();
            this.bInitializeStructuresList = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpStarbaseList)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUndefinedStructures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefinedStructures)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCorpStarbaseList
            // 
            this.dgvCorpStarbaseList.AllowUserToAddRows = false;
            this.dgvCorpStarbaseList.AllowUserToDeleteRows = false;
            this.dgvCorpStarbaseList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCorpStarbaseList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCorpStarbaseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCorpStarbaseList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCorpStarbaseList.Location = new System.Drawing.Point(0, 0);
            this.dgvCorpStarbaseList.Name = "dgvCorpStarbaseList";
            this.dgvCorpStarbaseList.ReadOnly = true;
            this.dgvCorpStarbaseList.Size = new System.Drawing.Size(633, 116);
            this.dgvCorpStarbaseList.TabIndex = 8;
            this.dgvCorpStarbaseList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCorpStarbaseList_CellFormatting);
            this.dgvCorpStarbaseList.SelectionChanged += new System.EventHandler(this.dgvCorpStarbaseList_SelectionChanged);
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
            this.splitContainer1.Panel1.Controls.Add(this.dgvCorpStarbaseList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pbTowerPower);
            this.splitContainer1.Panel2.Controls.Add(this.pbTowerCPU);
            this.splitContainer1.Panel2.Controls.Add(this.lTowerPower);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.lTowerCPU);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.bInitializeStructuresList);
            this.splitContainer1.Size = new System.Drawing.Size(633, 385);
            this.splitContainer1.SplitterDistance = 116;
            this.splitContainer1.TabIndex = 10;
            // 
            // pbTowerPower
            // 
            this.pbTowerPower.Location = new System.Drawing.Point(50, 3);
            this.pbTowerPower.Name = "pbTowerPower";
            this.pbTowerPower.Size = new System.Drawing.Size(100, 13);
            this.pbTowerPower.TabIndex = 3;
            // 
            // pbTowerCPU
            // 
            this.pbTowerCPU.Location = new System.Drawing.Point(50, 22);
            this.pbTowerCPU.Name = "pbTowerCPU";
            this.pbTowerCPU.Size = new System.Drawing.Size(100, 13);
            this.pbTowerCPU.TabIndex = 3;
            // 
            // lTowerPower
            // 
            this.lTowerPower.AutoSize = true;
            this.lTowerPower.Location = new System.Drawing.Point(156, 3);
            this.lTowerPower.Name = "lTowerPower";
            this.lTowerPower.Size = new System.Drawing.Size(69, 13);
            this.lTowerPower.TabIndex = 2;
            this.lTowerPower.Text = "lTowerPower";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Power:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "CPU:";
            // 
            // lTowerCPU
            // 
            this.lTowerCPU.AutoSize = true;
            this.lTowerCPU.Location = new System.Drawing.Point(156, 22);
            this.lTowerCPU.Name = "lTowerCPU";
            this.lTowerCPU.Size = new System.Drawing.Size(61, 13);
            this.lTowerCPU.TabIndex = 2;
            this.lTowerCPU.Text = "lTowerCPU";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(3, 67);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvUndefinedStructures);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvDefinedStructures);
            this.splitContainer2.Size = new System.Drawing.Size(627, 195);
            this.splitContainer2.SplitterDistance = 312;
            this.splitContainer2.TabIndex = 1;
            // 
            // dgvUndefinedStructures
            // 
            this.dgvUndefinedStructures.AllowDrop = true;
            this.dgvUndefinedStructures.AllowUserToAddRows = false;
            this.dgvUndefinedStructures.AllowUserToDeleteRows = false;
            this.dgvUndefinedStructures.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvUndefinedStructures.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUndefinedStructures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUndefinedStructures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUndefinedStructures.Location = new System.Drawing.Point(0, 0);
            this.dgvUndefinedStructures.Name = "dgvUndefinedStructures";
            this.dgvUndefinedStructures.Size = new System.Drawing.Size(312, 195);
            this.dgvUndefinedStructures.TabIndex = 9;
            this.dgvUndefinedStructures.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvUndefinedStructures_MouseDown);
            this.dgvUndefinedStructures.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvDefinedStructures_DragOver);
            this.dgvUndefinedStructures.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUndefinedStructures_CellValueChanged);
            this.dgvUndefinedStructures.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvDefinedStructures_DragDrop);
            // 
            // dgvDefinedStructures
            // 
            this.dgvDefinedStructures.AllowDrop = true;
            this.dgvDefinedStructures.AllowUserToAddRows = false;
            this.dgvDefinedStructures.AllowUserToDeleteRows = false;
            this.dgvDefinedStructures.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvDefinedStructures.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDefinedStructures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDefinedStructures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDefinedStructures.Location = new System.Drawing.Point(0, 0);
            this.dgvDefinedStructures.Name = "dgvDefinedStructures";
            this.dgvDefinedStructures.Size = new System.Drawing.Size(311, 195);
            this.dgvDefinedStructures.TabIndex = 9;
            this.dgvDefinedStructures.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvUndefinedStructures_MouseDown);
            this.dgvDefinedStructures.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvDefinedStructures_DragOver);
            this.dgvDefinedStructures.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUndefinedStructures_CellValueChanged);
            this.dgvDefinedStructures.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvDefinedStructures_DragDrop);
            // 
            // bInitializeStructuresList
            // 
            this.bInitializeStructuresList.AutoSize = true;
            this.bInitializeStructuresList.Location = new System.Drawing.Point(3, 38);
            this.bInitializeStructuresList.Name = "bInitializeStructuresList";
            this.bInitializeStructuresList.Size = new System.Drawing.Size(200, 23);
            this.bInitializeStructuresList.TabIndex = 0;
            this.bInitializeStructuresList.Text = "Инициализировать список структур";
            this.bInitializeStructuresList.UseVisualStyleBackColor = true;
            this.bInitializeStructuresList.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Требуется роль Director или CEO";
            // 
            // CorpStarbaseListConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bUpdate);
            this.Name = "CorpStarbaseListConfig";
            this.Size = new System.Drawing.Size(639, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpStarbaseList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUndefinedStructures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefinedStructures)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCorpStarbaseList;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvUndefinedStructures;
        private System.Windows.Forms.DataGridView dgvDefinedStructures;
        private System.Windows.Forms.Button bInitializeStructuresList;
        private System.Windows.Forms.Label lTowerPower;
        private System.Windows.Forms.Label lTowerCPU;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pbTowerPower;
        private System.Windows.Forms.ProgressBar pbTowerCPU;
    }
}
