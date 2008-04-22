namespace Accounting
{
    partial class CorpStarbaseDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lStarbaseDetailInfo = new System.Windows.Forms.Label();
            this.bUpdate = new System.Windows.Forms.Button();
            this.dtpCustomToDate = new System.Windows.Forms.DateTimePicker();
            this.lCustomVolume = new System.Windows.Forms.Label();
            this.lTimePeriod = new System.Windows.Forms.Label();
            this.dgvCorpStarbaseDetailFuel = new System.Windows.Forms.DataGridView();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lTowerInfo1 = new System.Windows.Forms.Label();
            this.lCPU = new System.Windows.Forms.Label();
            this.lPower = new System.Windows.Forms.Label();
            this.pbCpuLevel = new System.Windows.Forms.ProgressBar();
            this.pbPowerLevel = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbCalcFuelToTime = new System.Windows.Forms.RadioButton();
            this.rbCalcFuelToPeriod = new System.Windows.Forms.RadioButton();
            this.numCustomToPeriodHours = new System.Windows.Forms.NumericUpDown();
            this.numCustomToPeriodDays = new System.Windows.Forms.NumericUpDown();
            this.bStarbaseConfig = new System.Windows.Forms.Button();
            this.bFuelPrices = new System.Windows.Forms.Button();
            this.rbCalcFuelToCargoFull = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpStarbaseDetailFuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomToPeriodHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomToPeriodDays)).BeginInit();
            this.SuspendLayout();
            // 
            // lStarbaseDetailInfo
            // 
            this.lStarbaseDetailInfo.AutoSize = true;
            this.lStarbaseDetailInfo.Location = new System.Drawing.Point(11, 144);
            this.lStarbaseDetailInfo.Name = "lStarbaseDetailInfo";
            this.lStarbaseDetailInfo.Size = new System.Drawing.Size(24, 13);
            this.lStarbaseDetailInfo.TabIndex = 0;
            this.lStarbaseDetailInfo.Text = "info";
            // 
            // bUpdate
            // 
            this.bUpdate.AutoSize = true;
            this.bUpdate.Location = new System.Drawing.Point(229, 3);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(107, 23);
            this.bUpdate.TabIndex = 9;
            this.bUpdate.Text = "Пересчитать";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // dtpCustomToDate
            // 
            this.dtpCustomToDate.CustomFormat = "dd MMMM yyyy - HH:mm";
            this.dtpCustomToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCustomToDate.Location = new System.Drawing.Point(469, 28);
            this.dtpCustomToDate.Name = "dtpCustomToDate";
            this.dtpCustomToDate.Size = new System.Drawing.Size(169, 20);
            this.dtpCustomToDate.TabIndex = 11;
            this.dtpCustomToDate.ValueChanged += new System.EventHandler(this.OnButtonClick);
            // 
            // lCustomVolume
            // 
            this.lCustomVolume.AutoSize = true;
            this.lCustomVolume.Location = new System.Drawing.Point(364, 51);
            this.lCustomVolume.Name = "lCustomVolume";
            this.lCustomVolume.Size = new System.Drawing.Size(101, 13);
            this.lCustomVolume.TabIndex = 10;
            this.lCustomVolume.Text = "Расчётный объём:";
            // 
            // lTimePeriod
            // 
            this.lTimePeriod.AutoSize = true;
            this.lTimePeriod.Location = new System.Drawing.Point(364, 32);
            this.lTimePeriod.Name = "lTimePeriod";
            this.lTimePeriod.Size = new System.Drawing.Size(99, 13);
            this.lTimePeriod.TabIndex = 10;
            this.lTimePeriod.Text = "Дата дозаправки:";
            // 
            // dgvCorpStarbaseDetailFuel
            // 
            this.dgvCorpStarbaseDetailFuel.AllowUserToAddRows = false;
            this.dgvCorpStarbaseDetailFuel.AllowUserToDeleteRows = false;
            this.dgvCorpStarbaseDetailFuel.AllowUserToOrderColumns = true;
            this.dgvCorpStarbaseDetailFuel.AllowUserToResizeRows = false;
            this.dgvCorpStarbaseDetailFuel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCorpStarbaseDetailFuel.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCorpStarbaseDetailFuel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCorpStarbaseDetailFuel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCorpStarbaseDetailFuel.Location = new System.Drawing.Point(367, 67);
            this.dgvCorpStarbaseDetailFuel.Name = "dgvCorpStarbaseDetailFuel";
            this.dgvCorpStarbaseDetailFuel.ReadOnly = true;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCorpStarbaseDetailFuel.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCorpStarbaseDetailFuel.Size = new System.Drawing.Size(443, 350);
            this.dgvCorpStarbaseDetailFuel.TabIndex = 9;
            this.dgvCorpStarbaseDetailFuel.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCorpStarbaseDetailFuel_CellFormatting);
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(11, 32);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(64, 64);
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // lTowerInfo1
            // 
            this.lTowerInfo1.AutoSize = true;
            this.lTowerInfo1.Location = new System.Drawing.Point(81, 32);
            this.lTowerInfo1.Name = "lTowerInfo1";
            this.lTowerInfo1.Size = new System.Drawing.Size(24, 13);
            this.lTowerInfo1.TabIndex = 0;
            this.lTowerInfo1.Text = "info";
            // 
            // lCPU
            // 
            this.lCPU.AutoSize = true;
            this.lCPU.Location = new System.Drawing.Point(169, 102);
            this.lCPU.Name = "lCPU";
            this.lCPU.Size = new System.Drawing.Size(11, 13);
            this.lCPU.TabIndex = 1;
            this.lCPU.Text = "*";
            // 
            // lPower
            // 
            this.lPower.AutoSize = true;
            this.lPower.Location = new System.Drawing.Point(169, 121);
            this.lPower.Name = "lPower";
            this.lPower.Size = new System.Drawing.Size(11, 13);
            this.lPower.TabIndex = 1;
            this.lPower.Text = "*";
            // 
            // pbCpuLevel
            // 
            this.pbCpuLevel.Location = new System.Drawing.Point(54, 102);
            this.pbCpuLevel.Name = "pbCpuLevel";
            this.pbCpuLevel.Size = new System.Drawing.Size(109, 13);
            this.pbCpuLevel.TabIndex = 2;
            // 
            // pbPowerLevel
            // 
            this.pbPowerLevel.Location = new System.Drawing.Point(54, 121);
            this.pbPowerLevel.Name = "pbPowerLevel";
            this.pbPowerLevel.Size = new System.Drawing.Size(109, 13);
            this.pbPowerLevel.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "CPU:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Power:";
            // 
            // rbCalcFuelToTime
            // 
            this.rbCalcFuelToTime.AutoSize = true;
            this.rbCalcFuelToTime.Checked = true;
            this.rbCalcFuelToTime.Location = new System.Drawing.Point(367, 6);
            this.rbCalcFuelToTime.Name = "rbCalcFuelToTime";
            this.rbCalcFuelToTime.Size = new System.Drawing.Size(117, 17);
            this.rbCalcFuelToTime.TabIndex = 13;
            this.rbCalcFuelToTime.TabStop = true;
            this.rbCalcFuelToTime.Text = "Заправка до даты";
            this.rbCalcFuelToTime.UseVisualStyleBackColor = true;
            this.rbCalcFuelToTime.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // rbCalcFuelToPeriod
            // 
            this.rbCalcFuelToPeriod.AutoSize = true;
            this.rbCalcFuelToPeriod.Location = new System.Drawing.Point(490, 6);
            this.rbCalcFuelToPeriod.Name = "rbCalcFuelToPeriod";
            this.rbCalcFuelToPeriod.Size = new System.Drawing.Size(128, 17);
            this.rbCalcFuelToPeriod.TabIndex = 13;
            this.rbCalcFuelToPeriod.Text = "Заправка на период";
            this.rbCalcFuelToPeriod.UseVisualStyleBackColor = true;
            this.rbCalcFuelToPeriod.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // numCustomToPeriodHours
            // 
            this.numCustomToPeriodHours.Location = new System.Drawing.Point(535, 28);
            this.numCustomToPeriodHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numCustomToPeriodHours.Name = "numCustomToPeriodHours";
            this.numCustomToPeriodHours.Size = new System.Drawing.Size(39, 20);
            this.numCustomToPeriodHours.TabIndex = 14;
            this.numCustomToPeriodHours.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCustomToPeriodHours.Visible = false;
            this.numCustomToPeriodHours.ValueChanged += new System.EventHandler(this.OnButtonClick);
            // 
            // numCustomToPeriodDays
            // 
            this.numCustomToPeriodDays.Location = new System.Drawing.Point(490, 28);
            this.numCustomToPeriodDays.Name = "numCustomToPeriodDays";
            this.numCustomToPeriodDays.Size = new System.Drawing.Size(39, 20);
            this.numCustomToPeriodDays.TabIndex = 14;
            this.numCustomToPeriodDays.Visible = false;
            this.numCustomToPeriodDays.ValueChanged += new System.EventHandler(this.OnButtonClick);
            // 
            // bStarbaseConfig
            // 
            this.bStarbaseConfig.AutoSize = true;
            this.bStarbaseConfig.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bStarbaseConfig.Location = new System.Drawing.Point(3, 3);
            this.bStarbaseConfig.Name = "bStarbaseConfig";
            this.bStarbaseConfig.Size = new System.Drawing.Size(94, 23);
            this.bStarbaseConfig.TabIndex = 15;
            this.bStarbaseConfig.Text = "Конфигурация";
            this.bStarbaseConfig.UseVisualStyleBackColor = true;
            this.bStarbaseConfig.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bFuelPrices
            // 
            this.bFuelPrices.AutoSize = true;
            this.bFuelPrices.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bFuelPrices.Location = new System.Drawing.Point(103, 3);
            this.bFuelPrices.Name = "bFuelPrices";
            this.bFuelPrices.Size = new System.Drawing.Size(120, 23);
            this.bFuelPrices.TabIndex = 15;
            this.bFuelPrices.Text = "Стоимость топлива";
            this.bFuelPrices.UseVisualStyleBackColor = true;
            this.bFuelPrices.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // rbCalcFuelToCargoFull
            // 
            this.rbCalcFuelToCargoFull.AutoSize = true;
            this.rbCalcFuelToCargoFull.Location = new System.Drawing.Point(624, 6);
            this.rbCalcFuelToCargoFull.Name = "rbCalcFuelToCargoFull";
            this.rbCalcFuelToCargoFull.Size = new System.Drawing.Size(153, 17);
            this.rbCalcFuelToCargoFull.TabIndex = 13;
            this.rbCalcFuelToCargoFull.Text = "Заправка до упора карго";
            this.rbCalcFuelToCargoFull.UseVisualStyleBackColor = true;
            this.rbCalcFuelToCargoFull.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // CorpStarbaseDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bFuelPrices);
            this.Controls.Add(this.bStarbaseConfig);
            this.Controls.Add(this.numCustomToPeriodHours);
            this.Controls.Add(this.numCustomToPeriodDays);
            this.Controls.Add(this.rbCalcFuelToCargoFull);
            this.Controls.Add(this.rbCalcFuelToPeriod);
            this.Controls.Add(this.rbCalcFuelToTime);
            this.Controls.Add(this.dtpCustomToDate);
            this.Controls.Add(this.pbPowerLevel);
            this.Controls.Add(this.lCustomVolume);
            this.Controls.Add(this.lTimePeriod);
            this.Controls.Add(this.dgvCorpStarbaseDetailFuel);
            this.Controls.Add(this.lStarbaseDetailInfo);
            this.Controls.Add(this.pbCpuLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.lPower);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lTowerInfo1);
            this.Controls.Add(this.lCPU);
            this.Name = "CorpStarbaseDetails";
            this.Size = new System.Drawing.Size(813, 420);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpStarbaseDetailFuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomToPeriodHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomToPeriodDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lStarbaseDetailInfo;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.DataGridView dgvCorpStarbaseDetailFuel;
        private System.Windows.Forms.Label lTimePeriod;
        private System.Windows.Forms.DateTimePicker dtpCustomToDate;
        private System.Windows.Forms.Label lCustomVolume;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lTowerInfo1;
        private System.Windows.Forms.ProgressBar pbPowerLevel;
        private System.Windows.Forms.ProgressBar pbCpuLevel;
        private System.Windows.Forms.Label lPower;
        private System.Windows.Forms.Label lCPU;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbCalcFuelToTime;
        private System.Windows.Forms.RadioButton rbCalcFuelToPeriod;
        private System.Windows.Forms.NumericUpDown numCustomToPeriodHours;
        private System.Windows.Forms.NumericUpDown numCustomToPeriodDays;
        private System.Windows.Forms.Button bStarbaseConfig;
        private System.Windows.Forms.Button bFuelPrices;
        private System.Windows.Forms.RadioButton rbCalcFuelToCargoFull;
    }
}
