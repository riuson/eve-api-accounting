namespace Accounting
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ssStatusMain = new System.Windows.Forms.StatusStrip();
            this.tsslStatusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslGmtTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerGMT = new System.Windows.Forms.Timer(this.components);
            this.labelLoading = new System.Windows.Forms.Label();
            this.ssStatusMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssStatusMain
            // 
            this.ssStatusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatusProgress,
            this.tsslMessage,
            this.tsslPosition,
            this.tsslGmtTime});
            this.ssStatusMain.Location = new System.Drawing.Point(0, 498);
            this.ssStatusMain.Name = "ssStatusMain";
            this.ssStatusMain.Size = new System.Drawing.Size(965, 24);
            this.ssStatusMain.TabIndex = 0;
            this.ssStatusMain.Text = "statusStrip1";
            // 
            // tsslStatusProgress
            // 
            this.tsslStatusProgress.Name = "tsslStatusProgress";
            this.tsslStatusProgress.Size = new System.Drawing.Size(100, 18);
            // 
            // tsslMessage
            // 
            this.tsslMessage.Name = "tsslMessage";
            this.tsslMessage.Size = new System.Drawing.Size(109, 19);
            this.tsslMessage.Text = "toolStripStatusLabel1";
            // 
            // tsslPosition
            // 
            this.tsslPosition.Name = "tsslPosition";
            this.tsslPosition.Size = new System.Drawing.Size(707, 19);
            this.tsslPosition.Spring = true;
            this.tsslPosition.Text = "0/0";
            this.tsslPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tsslGmtTime
            // 
            this.tsslGmtTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslGmtTime.Name = "tsslGmtTime";
            this.tsslGmtTime.Size = new System.Drawing.Size(32, 19);
            this.tsslGmtTime.Text = "GMT";
            // 
            // timerGMT
            // 
            this.timerGMT.Enabled = true;
            this.timerGMT.Interval = 250;
            this.timerGMT.Tick += new System.EventHandler(this.timerGMT_Tick);
            // 
            // labelLoading
            // 
            this.labelLoading.BackColor = System.Drawing.Color.Transparent;
            this.labelLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLoading.Font = new System.Drawing.Font("Times New Roman", 60F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLoading.ForeColor = System.Drawing.Color.Silver;
            this.labelLoading.Location = new System.Drawing.Point(0, 0);
            this.labelLoading.Name = "labelLoading";
            this.labelLoading.Size = new System.Drawing.Size(965, 498);
            this.labelLoading.TabIndex = 1;
            this.labelLoading.Text = "Загрузка...";
            this.labelLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(965, 522);
            this.Controls.Add(this.labelLoading);
            this.Controls.Add(this.ssStatusMain);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "FormMain";
            this.Text = "Accounting";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.ssStatusMain.ResumeLayout(false);
            this.ssStatusMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssStatusMain;
        private System.Windows.Forms.ToolStripStatusLabel tsslMessage;
        private System.Windows.Forms.ToolStripProgressBar tsslStatusProgress;
        private System.Windows.Forms.ToolStripStatusLabel tsslPosition;
        private System.Windows.Forms.ToolStripStatusLabel tsslGmtTime;
        private System.Windows.Forms.Timer timerGMT;
        private System.Windows.Forms.Label labelLoading;
    }
}

