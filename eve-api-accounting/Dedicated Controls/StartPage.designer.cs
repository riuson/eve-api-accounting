namespace Accounting
{
    partial class StartPage
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
            this.flpStartMenuItems = new System.Windows.Forms.FlowLayoutPanel();
            this.pbStartImage1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbStartImage1)).BeginInit();
            this.SuspendLayout();
            // 
            // flpStartMenuItems
            // 
            this.flpStartMenuItems.AutoScroll = true;
            this.flpStartMenuItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpStartMenuItems.Location = new System.Drawing.Point(0, 100);
            this.flpStartMenuItems.Name = "flpStartMenuItems";
            this.flpStartMenuItems.Padding = new System.Windows.Forms.Padding(5);
            this.flpStartMenuItems.Size = new System.Drawing.Size(592, 231);
            this.flpStartMenuItems.TabIndex = 1;
            // 
            // pbStartImage1
            // 
            this.pbStartImage1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbStartImage1.Location = new System.Drawing.Point(0, 0);
            this.pbStartImage1.Name = "pbStartImage1";
            this.pbStartImage1.Size = new System.Drawing.Size(592, 100);
            this.pbStartImage1.TabIndex = 3;
            this.pbStartImage1.TabStop = false;
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flpStartMenuItems);
            this.Controls.Add(this.pbStartImage1);
            this.Name = "StartPage";
            this.Size = new System.Drawing.Size(592, 331);
            ((System.ComponentModel.ISupportInitialize)(this.pbStartImage1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpStartMenuItems;
        private System.Windows.Forms.PictureBox pbStartImage1;
    }
}
