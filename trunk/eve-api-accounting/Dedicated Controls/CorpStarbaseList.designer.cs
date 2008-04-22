namespace Accounting
{
    partial class CorpStarbaseList
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
            this.label1 = new System.Windows.Forms.Label();
            this.bStarbaseConfig = new System.Windows.Forms.Button();
            this.flpStarbases = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRegion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbConstellation = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSolarSystem = new System.Windows.Forms.ComboBox();
            this.bSelectFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Требуется роль Director или CEO";
            // 
            // bStarbaseConfig
            // 
            this.bStarbaseConfig.AutoSize = true;
            this.bStarbaseConfig.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bStarbaseConfig.Location = new System.Drawing.Point(3, 32);
            this.bStarbaseConfig.Name = "bStarbaseConfig";
            this.bStarbaseConfig.Size = new System.Drawing.Size(94, 23);
            this.bStarbaseConfig.TabIndex = 13;
            this.bStarbaseConfig.Text = "Конфигурация";
            this.bStarbaseConfig.UseVisualStyleBackColor = true;
            this.bStarbaseConfig.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // flpStarbases
            // 
            this.flpStarbases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flpStarbases.Location = new System.Drawing.Point(3, 101);
            this.flpStarbases.Name = "flpStarbases";
            this.flpStarbases.Size = new System.Drawing.Size(633, 316);
            this.flpStarbases.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Регион:";
            // 
            // cbRegion
            // 
            this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegion.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbRegion.FormattingEnabled = true;
            this.cbRegion.Location = new System.Drawing.Point(6, 74);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(189, 21);
            this.cbRegion.TabIndex = 16;
            this.cbRegion.SelectedIndexChanged += new System.EventHandler(this.OnButtonClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Созвездие:";
            // 
            // cbConstellation
            // 
            this.cbConstellation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConstellation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbConstellation.FormattingEnabled = true;
            this.cbConstellation.Location = new System.Drawing.Point(201, 74);
            this.cbConstellation.Name = "cbConstellation";
            this.cbConstellation.Size = new System.Drawing.Size(189, 21);
            this.cbConstellation.TabIndex = 16;
            this.cbConstellation.SelectedIndexChanged += new System.EventHandler(this.OnButtonClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Система:";
            // 
            // cbSolarSystem
            // 
            this.cbSolarSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSolarSystem.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbSolarSystem.FormattingEnabled = true;
            this.cbSolarSystem.Location = new System.Drawing.Point(396, 74);
            this.cbSolarSystem.Name = "cbSolarSystem";
            this.cbSolarSystem.Size = new System.Drawing.Size(189, 21);
            this.cbSolarSystem.TabIndex = 16;
            // 
            // bSelectFilter
            // 
            this.bSelectFilter.AutoSize = true;
            this.bSelectFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bSelectFilter.Location = new System.Drawing.Point(591, 74);
            this.bSelectFilter.Name = "bSelectFilter";
            this.bSelectFilter.Size = new System.Drawing.Size(75, 22);
            this.bSelectFilter.TabIndex = 17;
            this.bSelectFilter.Text = "Выбрать";
            this.bSelectFilter.UseVisualStyleBackColor = true;
            this.bSelectFilter.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // CorpStarbaseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bSelectFilter);
            this.Controls.Add(this.cbSolarSystem);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbConstellation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbRegion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flpStarbases);
            this.Controls.Add(this.bStarbaseConfig);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bUpdate);
            this.Name = "CorpStarbaseList";
            this.Size = new System.Drawing.Size(639, 420);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bStarbaseConfig;
        private System.Windows.Forms.FlowLayoutPanel flpStarbases;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbRegion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbConstellation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSolarSystem;
        private System.Windows.Forms.Button bSelectFilter;
    }
}
