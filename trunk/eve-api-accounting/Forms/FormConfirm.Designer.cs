namespace Accounting
{
    partial class FormConfirm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lCode = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.bOk = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите указанный ниже код в текстовое поле:";
            // 
            // lCode
            // 
            this.lCode.AutoSize = true;
            this.lCode.BackColor = System.Drawing.Color.Transparent;
            this.lCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lCode.Location = new System.Drawing.Point(11, 22);
            this.lCode.Name = "lCode";
            this.lCode.Size = new System.Drawing.Size(399, 24);
            this.lCode.TabIndex = 1;
            this.lCode.Text = "{72B6CCC7-F3ED-44fe-8AFC-658531AF1BBB}";
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(15, 49);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(390, 20);
            this.tbCode.TabIndex = 2;
            this.tbCode.Text = "{72B6CCC7-F3ED-44fe-8AFC-658531AF1BBB}";
            this.tbCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbCode.TextChanged += new System.EventHandler(this.tbCode_TextChanged);
            // 
            // bOk
            // 
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOk.Enabled = false;
            this.bOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bOk.Location = new System.Drawing.Point(232, 75);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 3;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bCancel.Location = new System.Drawing.Point(313, 75);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // FormSubmit
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(417, 108);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.lCode);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSubmit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Подтверждение опасной операции";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lCode;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bCancel;
    }
}