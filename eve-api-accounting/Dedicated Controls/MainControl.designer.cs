namespace Accounting
{
    partial class MainControl
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
            this.flpNavigation = new System.Windows.Forms.FlowLayoutPanel();
            this.bBack = new System.Windows.Forms.Button();
            this.lNavigationPath = new System.Windows.Forms.Label();
            this.flpNavigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpNavigation
            // 
            this.flpNavigation.AutoSize = true;
            this.flpNavigation.Controls.Add(this.bBack);
            this.flpNavigation.Controls.Add(this.lNavigationPath);
            this.flpNavigation.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpNavigation.Location = new System.Drawing.Point(0, 0);
            this.flpNavigation.Name = "flpNavigation";
            this.flpNavigation.Size = new System.Drawing.Size(868, 29);
            this.flpNavigation.TabIndex = 0;
            // 
            // bBack
            // 
            this.bBack.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bBack.Location = new System.Drawing.Point(3, 3);
            this.bBack.Name = "bBack";
            this.bBack.Size = new System.Drawing.Size(75, 23);
            this.bBack.TabIndex = 0;
            this.bBack.Text = "Назад";
            this.bBack.UseVisualStyleBackColor = true;
            this.bBack.Click += new System.EventHandler(this.OnNavigationClick);
            // 
            // lNavigationPath
            // 
            this.lNavigationPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lNavigationPath.AutoSize = true;
            this.lNavigationPath.Location = new System.Drawing.Point(84, 8);
            this.lNavigationPath.Name = "lNavigationPath";
            this.lNavigationPath.Size = new System.Drawing.Size(35, 13);
            this.lNavigationPath.TabIndex = 1;
            this.lNavigationPath.Text = "label1";
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.flpNavigation);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(868, 501);
            this.flpNavigation.ResumeLayout(false);
            this.flpNavigation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpNavigation;
        private System.Windows.Forms.Button bBack;
        private System.Windows.Forms.Label lNavigationPath;




    }
}
