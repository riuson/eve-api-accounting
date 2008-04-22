namespace Accounting
{
    partial class ControlStatistics
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
            this.llBountyPrizeByMember = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.tcStats = new System.Windows.Forms.TabControl();
            this.tpCorpWalletsInOutWithWithdrawals = new System.Windows.Forms.TabPage();
            this.dgvCorpWalletsInOutWithWithdrawals = new System.Windows.Forms.DataGridView();
            this.tpCorpWalletsInOutWithoutWithdrawals = new System.Windows.Forms.TabPage();
            this.dgvCorpWalletsInOutWithoutWithdrawals = new System.Windows.Forms.DataGridView();
            this.tpTransBetweenWallets = new System.Windows.Forms.TabPage();
            this.bWD1004 = new System.Windows.Forms.Label();
            this.bWD1002 = new System.Windows.Forms.Label();
            this.bWD1006 = new System.Windows.Forms.Label();
            this.bWD1005 = new System.Windows.Forms.Label();
            this.bWD1000 = new System.Windows.Forms.Label();
            this.bWD1003 = new System.Windows.Forms.Label();
            this.bWD1001 = new System.Windows.Forms.Label();
            this.bGetStatistic = new System.Windows.Forms.Button();
            this.pbWithdrawalsBackground = new System.Windows.Forms.PictureBox();
            this.tpWalletStatisticRefTypeId = new System.Windows.Forms.TabPage();
            this.cbWalletStatisticRefTypeId = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dgvWalletStatisticRefTypeId = new System.Windows.Forms.DataGridView();
            this.tpBountyPrizeByMember = new System.Windows.Forms.TabPage();
            this.dgvBountyByMember = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.llWalletStatisticRefTypeId = new System.Windows.Forms.LinkLabel();
            this.dtpStatisticFilterLow = new System.Windows.Forms.DateTimePicker();
            this.llTransBetweenWallets = new System.Windows.Forms.LinkLabel();
            this.dtpStatisticFilterTop = new System.Windows.Forms.DateTimePicker();
            this.llCorpWalletsInOutWithoutWithdrawals = new System.Windows.Forms.LinkLabel();
            this.llCorpWalletsInOutWithWithdrawals = new System.Windows.Forms.LinkLabel();
            this.tpWalletStatisticRefTypeId2 = new System.Windows.Forms.TabPage();
            this.llWalletStatisticRefTypeId2 = new System.Windows.Forms.LinkLabel();
            this.dgvWalletStatisticRefTypeId2 = new System.Windows.Forms.DataGridView();
            this.tcStats.SuspendLayout();
            this.tpCorpWalletsInOutWithWithdrawals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpWalletsInOutWithWithdrawals)).BeginInit();
            this.tpCorpWalletsInOutWithoutWithdrawals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpWalletsInOutWithoutWithdrawals)).BeginInit();
            this.tpTransBetweenWallets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWithdrawalsBackground)).BeginInit();
            this.tpWalletStatisticRefTypeId.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWalletStatisticRefTypeId)).BeginInit();
            this.tpBountyPrizeByMember.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBountyByMember)).BeginInit();
            this.tpWalletStatisticRefTypeId2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWalletStatisticRefTypeId2)).BeginInit();
            this.SuspendLayout();
            // 
            // llBountyPrizeByMember
            // 
            this.llBountyPrizeByMember.AutoSize = true;
            this.llBountyPrizeByMember.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.llBountyPrizeByMember.Location = new System.Drawing.Point(11, 178);
            this.llBountyPrizeByMember.Name = "llBountyPrizeByMember";
            this.llBountyPrizeByMember.Size = new System.Drawing.Size(102, 13);
            this.llBountyPrizeByMember.TabIndex = 21;
            this.llBountyPrizeByMember.TabStop = true;
            this.llBountyPrizeByMember.Text = "Доходы от миссий";
            this.llBountyPrizeByMember.Click += new System.EventHandler(this.onStatsTypeSelect);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "From:";
            // 
            // tcStats
            // 
            this.tcStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcStats.Controls.Add(this.tpCorpWalletsInOutWithWithdrawals);
            this.tcStats.Controls.Add(this.tpCorpWalletsInOutWithoutWithdrawals);
            this.tcStats.Controls.Add(this.tpTransBetweenWallets);
            this.tcStats.Controls.Add(this.tpWalletStatisticRefTypeId);
            this.tcStats.Controls.Add(this.tpBountyPrizeByMember);
            this.tcStats.Controls.Add(this.tpWalletStatisticRefTypeId2);
            this.tcStats.ItemSize = new System.Drawing.Size(20, 18);
            this.tcStats.Location = new System.Drawing.Point(289, 5);
            this.tcStats.Multiline = true;
            this.tcStats.Name = "tcStats";
            this.tcStats.SelectedIndex = 0;
            this.tcStats.Size = new System.Drawing.Size(503, 440);
            this.tcStats.TabIndex = 20;
            // 
            // tpCorpWalletsInOutWithWithdrawals
            // 
            this.tpCorpWalletsInOutWithWithdrawals.Controls.Add(this.dgvCorpWalletsInOutWithWithdrawals);
            this.tpCorpWalletsInOutWithWithdrawals.Location = new System.Drawing.Point(4, 76);
            this.tpCorpWalletsInOutWithWithdrawals.Name = "tpCorpWalletsInOutWithWithdrawals";
            this.tpCorpWalletsInOutWithWithdrawals.Padding = new System.Windows.Forms.Padding(3);
            this.tpCorpWalletsInOutWithWithdrawals.Size = new System.Drawing.Size(495, 360);
            this.tpCorpWalletsInOutWithWithdrawals.TabIndex = 0;
            this.tpCorpWalletsInOutWithWithdrawals.Text = "Ввод/вывод средств с внутренними переводами";
            this.tpCorpWalletsInOutWithWithdrawals.UseVisualStyleBackColor = true;
            // 
            // dgvCorpWalletsInOutWithWithdrawals
            // 
            this.dgvCorpWalletsInOutWithWithdrawals.AllowUserToAddRows = false;
            this.dgvCorpWalletsInOutWithWithdrawals.AllowUserToDeleteRows = false;
            this.dgvCorpWalletsInOutWithWithdrawals.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCorpWalletsInOutWithWithdrawals.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCorpWalletsInOutWithWithdrawals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCorpWalletsInOutWithWithdrawals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCorpWalletsInOutWithWithdrawals.Location = new System.Drawing.Point(3, 3);
            this.dgvCorpWalletsInOutWithWithdrawals.Name = "dgvCorpWalletsInOutWithWithdrawals";
            this.dgvCorpWalletsInOutWithWithdrawals.Size = new System.Drawing.Size(489, 354);
            this.dgvCorpWalletsInOutWithWithdrawals.TabIndex = 6;
            // 
            // tpCorpWalletsInOutWithoutWithdrawals
            // 
            this.tpCorpWalletsInOutWithoutWithdrawals.Controls.Add(this.dgvCorpWalletsInOutWithoutWithdrawals);
            this.tpCorpWalletsInOutWithoutWithdrawals.Location = new System.Drawing.Point(4, 76);
            this.tpCorpWalletsInOutWithoutWithdrawals.Name = "tpCorpWalletsInOutWithoutWithdrawals";
            this.tpCorpWalletsInOutWithoutWithdrawals.Padding = new System.Windows.Forms.Padding(3);
            this.tpCorpWalletsInOutWithoutWithdrawals.Size = new System.Drawing.Size(495, 360);
            this.tpCorpWalletsInOutWithoutWithdrawals.TabIndex = 1;
            this.tpCorpWalletsInOutWithoutWithdrawals.Text = "Ввод/вывод средств без внутренних переводов";
            this.tpCorpWalletsInOutWithoutWithdrawals.UseVisualStyleBackColor = true;
            // 
            // dgvCorpWalletsInOutWithoutWithdrawals
            // 
            this.dgvCorpWalletsInOutWithoutWithdrawals.AllowUserToAddRows = false;
            this.dgvCorpWalletsInOutWithoutWithdrawals.AllowUserToDeleteRows = false;
            this.dgvCorpWalletsInOutWithoutWithdrawals.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCorpWalletsInOutWithoutWithdrawals.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCorpWalletsInOutWithoutWithdrawals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCorpWalletsInOutWithoutWithdrawals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCorpWalletsInOutWithoutWithdrawals.Location = new System.Drawing.Point(3, 3);
            this.dgvCorpWalletsInOutWithoutWithdrawals.Name = "dgvCorpWalletsInOutWithoutWithdrawals";
            this.dgvCorpWalletsInOutWithoutWithdrawals.Size = new System.Drawing.Size(489, 354);
            this.dgvCorpWalletsInOutWithoutWithdrawals.TabIndex = 6;
            // 
            // tpTransBetweenWallets
            // 
            this.tpTransBetweenWallets.Controls.Add(this.bWD1004);
            this.tpTransBetweenWallets.Controls.Add(this.bWD1002);
            this.tpTransBetweenWallets.Controls.Add(this.bWD1006);
            this.tpTransBetweenWallets.Controls.Add(this.bWD1005);
            this.tpTransBetweenWallets.Controls.Add(this.bWD1000);
            this.tpTransBetweenWallets.Controls.Add(this.bWD1003);
            this.tpTransBetweenWallets.Controls.Add(this.bWD1001);
            this.tpTransBetweenWallets.Controls.Add(this.bGetStatistic);
            this.tpTransBetweenWallets.Controls.Add(this.pbWithdrawalsBackground);
            this.tpTransBetweenWallets.Location = new System.Drawing.Point(4, 76);
            this.tpTransBetweenWallets.Name = "tpTransBetweenWallets";
            this.tpTransBetweenWallets.Padding = new System.Windows.Forms.Padding(3);
            this.tpTransBetweenWallets.Size = new System.Drawing.Size(495, 360);
            this.tpTransBetweenWallets.TabIndex = 2;
            this.tpTransBetweenWallets.Text = "Внутренние переводы между корп. кошельками";
            this.tpTransBetweenWallets.UseVisualStyleBackColor = true;
            // 
            // bWD1004
            // 
            this.bWD1004.BackColor = System.Drawing.Color.Gainsboro;
            this.bWD1004.Location = new System.Drawing.Point(312, 141);
            this.bWD1004.Name = "bWD1004";
            this.bWD1004.Size = new System.Drawing.Size(125, 38);
            this.bWD1004.TabIndex = 7;
            this.bWD1004.Text = "Fifth Division\r\n99 000 000 000.00";
            this.bWD1004.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bWD1004.MouseEnter += new System.EventHandler(this.OnWithdrawalsButtonMouseEnter);
            // 
            // bWD1002
            // 
            this.bWD1002.BackColor = System.Drawing.Color.Gainsboro;
            this.bWD1002.Location = new System.Drawing.Point(312, 75);
            this.bWD1002.Name = "bWD1002";
            this.bWD1002.Size = new System.Drawing.Size(125, 38);
            this.bWD1002.TabIndex = 7;
            this.bWD1002.Text = "Third Division\r\n99 000 000 000.00";
            this.bWD1002.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bWD1002.MouseEnter += new System.EventHandler(this.OnWithdrawalsButtonMouseEnter);
            // 
            // bWD1006
            // 
            this.bWD1006.BackColor = System.Drawing.Color.Gainsboro;
            this.bWD1006.Location = new System.Drawing.Point(224, 205);
            this.bWD1006.Name = "bWD1006";
            this.bWD1006.Size = new System.Drawing.Size(125, 38);
            this.bWD1006.TabIndex = 7;
            this.bWD1006.Text = "Seventh Division\r\n99 000 000 000.00";
            this.bWD1006.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bWD1006.MouseEnter += new System.EventHandler(this.OnWithdrawalsButtonMouseEnter);
            // 
            // bWD1005
            // 
            this.bWD1005.BackColor = System.Drawing.Color.Gainsboro;
            this.bWD1005.Location = new System.Drawing.Point(93, 205);
            this.bWD1005.Name = "bWD1005";
            this.bWD1005.Size = new System.Drawing.Size(125, 38);
            this.bWD1005.TabIndex = 7;
            this.bWD1005.Text = "Sixth Division\r\n99 000 000 000.00";
            this.bWD1005.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bWD1005.MouseEnter += new System.EventHandler(this.OnWithdrawalsButtonMouseEnter);
            // 
            // bWD1000
            // 
            this.bWD1000.BackColor = System.Drawing.Color.Gainsboro;
            this.bWD1000.Location = new System.Drawing.Point(160, 15);
            this.bWD1000.Name = "bWD1000";
            this.bWD1000.Size = new System.Drawing.Size(125, 38);
            this.bWD1000.TabIndex = 7;
            this.bWD1000.Text = "Master Wallet\r\n99 000 000 000.00";
            this.bWD1000.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bWD1000.MouseEnter += new System.EventHandler(this.OnWithdrawalsButtonMouseEnter);
            // 
            // bWD1003
            // 
            this.bWD1003.BackColor = System.Drawing.Color.Gainsboro;
            this.bWD1003.Location = new System.Drawing.Point(6, 141);
            this.bWD1003.Name = "bWD1003";
            this.bWD1003.Size = new System.Drawing.Size(125, 38);
            this.bWD1003.TabIndex = 7;
            this.bWD1003.Text = "Fourth Division\r\n99 000 000 000.00";
            this.bWD1003.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bWD1003.MouseEnter += new System.EventHandler(this.OnWithdrawalsButtonMouseEnter);
            // 
            // bWD1001
            // 
            this.bWD1001.BackColor = System.Drawing.Color.Gainsboro;
            this.bWD1001.Location = new System.Drawing.Point(6, 75);
            this.bWD1001.Name = "bWD1001";
            this.bWD1001.Size = new System.Drawing.Size(125, 38);
            this.bWD1001.TabIndex = 7;
            this.bWD1001.Text = "Second Division\r\n99 000 000 000.00";
            this.bWD1001.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bWD1001.MouseEnter += new System.EventHandler(this.OnWithdrawalsButtonMouseEnter);
            // 
            // bGetStatistic
            // 
            this.bGetStatistic.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bGetStatistic.Location = new System.Drawing.Point(6, 6);
            this.bGetStatistic.Name = "bGetStatistic";
            this.bGetStatistic.Size = new System.Drawing.Size(77, 23);
            this.bGetStatistic.TabIndex = 6;
            this.bGetStatistic.Text = "Рассчитать";
            this.bGetStatistic.UseVisualStyleBackColor = true;
            this.bGetStatistic.Click += new System.EventHandler(this.bGetStatistic_Click);
            // 
            // pbWithdrawalsBackground
            // 
            this.pbWithdrawalsBackground.Location = new System.Drawing.Point(6, 15);
            this.pbWithdrawalsBackground.Name = "pbWithdrawalsBackground";
            this.pbWithdrawalsBackground.Size = new System.Drawing.Size(431, 228);
            this.pbWithdrawalsBackground.TabIndex = 1;
            this.pbWithdrawalsBackground.TabStop = false;
            // 
            // tpWalletStatisticRefTypeId
            // 
            this.tpWalletStatisticRefTypeId.Controls.Add(this.cbWalletStatisticRefTypeId);
            this.tpWalletStatisticRefTypeId.Controls.Add(this.label9);
            this.tpWalletStatisticRefTypeId.Controls.Add(this.dgvWalletStatisticRefTypeId);
            this.tpWalletStatisticRefTypeId.Location = new System.Drawing.Point(4, 94);
            this.tpWalletStatisticRefTypeId.Name = "tpWalletStatisticRefTypeId";
            this.tpWalletStatisticRefTypeId.Padding = new System.Windows.Forms.Padding(3);
            this.tpWalletStatisticRefTypeId.Size = new System.Drawing.Size(495, 342);
            this.tpWalletStatisticRefTypeId.TabIndex = 3;
            this.tpWalletStatisticRefTypeId.Text = "Ввод/вывод средств кошельков, по типу переводов";
            this.tpWalletStatisticRefTypeId.UseVisualStyleBackColor = true;
            // 
            // cbWalletStatisticRefTypeId
            // 
            this.cbWalletStatisticRefTypeId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWalletStatisticRefTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWalletStatisticRefTypeId.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbWalletStatisticRefTypeId.FormattingEnabled = true;
            this.cbWalletStatisticRefTypeId.Location = new System.Drawing.Point(42, 6);
            this.cbWalletStatisticRefTypeId.Name = "cbWalletStatisticRefTypeId";
            this.cbWalletStatisticRefTypeId.Size = new System.Drawing.Size(322, 21);
            this.cbWalletStatisticRefTypeId.TabIndex = 8;
            this.cbWalletStatisticRefTypeId.SelectedIndexChanged += new System.EventHandler(this.cbWalletStatisticRefTypeId_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Тип:";
            // 
            // dgvWalletStatisticRefTypeId
            // 
            this.dgvWalletStatisticRefTypeId.AllowUserToAddRows = false;
            this.dgvWalletStatisticRefTypeId.AllowUserToDeleteRows = false;
            this.dgvWalletStatisticRefTypeId.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWalletStatisticRefTypeId.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvWalletStatisticRefTypeId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWalletStatisticRefTypeId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWalletStatisticRefTypeId.Location = new System.Drawing.Point(6, 33);
            this.dgvWalletStatisticRefTypeId.Name = "dgvWalletStatisticRefTypeId";
            this.dgvWalletStatisticRefTypeId.Size = new System.Drawing.Size(483, 303);
            this.dgvWalletStatisticRefTypeId.TabIndex = 6;
            // 
            // tpBountyPrizeByMember
            // 
            this.tpBountyPrizeByMember.Controls.Add(this.dgvBountyByMember);
            this.tpBountyPrizeByMember.Location = new System.Drawing.Point(4, 94);
            this.tpBountyPrizeByMember.Name = "tpBountyPrizeByMember";
            this.tpBountyPrizeByMember.Padding = new System.Windows.Forms.Padding(3);
            this.tpBountyPrizeByMember.Size = new System.Drawing.Size(495, 342);
            this.tpBountyPrizeByMember.TabIndex = 5;
            this.tpBountyPrizeByMember.Text = "Доходы от миссиий";
            this.tpBountyPrizeByMember.UseVisualStyleBackColor = true;
            // 
            // dgvBountyByMember
            // 
            this.dgvBountyByMember.AllowUserToAddRows = false;
            this.dgvBountyByMember.AllowUserToDeleteRows = false;
            this.dgvBountyByMember.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBountyByMember.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvBountyByMember.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBountyByMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBountyByMember.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBountyByMember.Location = new System.Drawing.Point(3, 3);
            this.dgvBountyByMember.Name = "dgvBountyByMember";
            this.dgvBountyByMember.Size = new System.Drawing.Size(489, 336);
            this.dgvBountyByMember.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "To:";
            // 
            // llWalletStatisticRefTypeId
            // 
            this.llWalletStatisticRefTypeId.AutoSize = true;
            this.llWalletStatisticRefTypeId.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.llWalletStatisticRefTypeId.Location = new System.Drawing.Point(11, 133);
            this.llWalletStatisticRefTypeId.Name = "llWalletStatisticRefTypeId";
            this.llWalletStatisticRefTypeId.Size = new System.Drawing.Size(272, 13);
            this.llWalletStatisticRefTypeId.TabIndex = 19;
            this.llWalletStatisticRefTypeId.TabStop = true;
            this.llWalletStatisticRefTypeId.Text = "Ввод/вывод средств кошельков, по типу переводов";
            this.llWalletStatisticRefTypeId.Click += new System.EventHandler(this.onStatsTypeSelect);
            // 
            // dtpStatisticFilterLow
            // 
            this.dtpStatisticFilterLow.CustomFormat = "dd MMMM yyyy г. - HH:mm";
            this.dtpStatisticFilterLow.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStatisticFilterLow.Location = new System.Drawing.Point(55, 14);
            this.dtpStatisticFilterLow.Name = "dtpStatisticFilterLow";
            this.dtpStatisticFilterLow.Size = new System.Drawing.Size(212, 20);
            this.dtpStatisticFilterLow.TabIndex = 14;
            this.dtpStatisticFilterLow.ValueChanged += new System.EventHandler(this.OnStatisticDateFilterChanged);
            // 
            // llTransBetweenWallets
            // 
            this.llTransBetweenWallets.AutoSize = true;
            this.llTransBetweenWallets.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.llTransBetweenWallets.Location = new System.Drawing.Point(11, 113);
            this.llTransBetweenWallets.Name = "llTransBetweenWallets";
            this.llTransBetweenWallets.Size = new System.Drawing.Size(252, 13);
            this.llTransBetweenWallets.TabIndex = 18;
            this.llTransBetweenWallets.TabStop = true;
            this.llTransBetweenWallets.Text = "Внутренние переводы между корп. кошельками";
            this.llTransBetweenWallets.Click += new System.EventHandler(this.onStatsTypeSelect);
            // 
            // dtpStatisticFilterTop
            // 
            this.dtpStatisticFilterTop.CustomFormat = "dd MMMM yyyy г. - HH:mm";
            this.dtpStatisticFilterTop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStatisticFilterTop.Location = new System.Drawing.Point(55, 40);
            this.dtpStatisticFilterTop.Name = "dtpStatisticFilterTop";
            this.dtpStatisticFilterTop.Size = new System.Drawing.Size(212, 20);
            this.dtpStatisticFilterTop.TabIndex = 15;
            this.dtpStatisticFilterTop.ValueChanged += new System.EventHandler(this.OnStatisticDateFilterChanged);
            // 
            // llCorpWalletsInOutWithoutWithdrawals
            // 
            this.llCorpWalletsInOutWithoutWithdrawals.AutoSize = true;
            this.llCorpWalletsInOutWithoutWithdrawals.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.llCorpWalletsInOutWithoutWithdrawals.Location = new System.Drawing.Point(11, 93);
            this.llCorpWalletsInOutWithoutWithdrawals.Name = "llCorpWalletsInOutWithoutWithdrawals";
            this.llCorpWalletsInOutWithoutWithdrawals.Size = new System.Drawing.Size(251, 13);
            this.llCorpWalletsInOutWithoutWithdrawals.TabIndex = 17;
            this.llCorpWalletsInOutWithoutWithdrawals.TabStop = true;
            this.llCorpWalletsInOutWithoutWithdrawals.Text = "Ввод/вывод средств без внутренних переводов";
            this.llCorpWalletsInOutWithoutWithdrawals.Click += new System.EventHandler(this.onStatsTypeSelect);
            // 
            // llCorpWalletsInOutWithWithdrawals
            // 
            this.llCorpWalletsInOutWithWithdrawals.AutoSize = true;
            this.llCorpWalletsInOutWithWithdrawals.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.llCorpWalletsInOutWithWithdrawals.Location = new System.Drawing.Point(11, 73);
            this.llCorpWalletsInOutWithWithdrawals.Name = "llCorpWalletsInOutWithWithdrawals";
            this.llCorpWalletsInOutWithWithdrawals.Size = new System.Drawing.Size(256, 13);
            this.llCorpWalletsInOutWithWithdrawals.TabIndex = 16;
            this.llCorpWalletsInOutWithWithdrawals.TabStop = true;
            this.llCorpWalletsInOutWithWithdrawals.Text = "Ввод/вывод средств с внутренними переводами";
            this.llCorpWalletsInOutWithWithdrawals.Click += new System.EventHandler(this.onStatsTypeSelect);
            // 
            // tpWalletStatisticRefTypeId2
            // 
            this.tpWalletStatisticRefTypeId2.Controls.Add(this.dgvWalletStatisticRefTypeId2);
            this.tpWalletStatisticRefTypeId2.Location = new System.Drawing.Point(4, 94);
            this.tpWalletStatisticRefTypeId2.Name = "tpWalletStatisticRefTypeId2";
            this.tpWalletStatisticRefTypeId2.Padding = new System.Windows.Forms.Padding(3);
            this.tpWalletStatisticRefTypeId2.Size = new System.Drawing.Size(495, 342);
            this.tpWalletStatisticRefTypeId2.TabIndex = 6;
            this.tpWalletStatisticRefTypeId2.Text = "Ввод/вывод средств общий по типу переводов";
            this.tpWalletStatisticRefTypeId2.UseVisualStyleBackColor = true;
            // 
            // llWalletStatisticRefTypeId2
            // 
            this.llWalletStatisticRefTypeId2.AutoSize = true;
            this.llWalletStatisticRefTypeId2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.llWalletStatisticRefTypeId2.Location = new System.Drawing.Point(11, 155);
            this.llWalletStatisticRefTypeId2.Name = "llWalletStatisticRefTypeId2";
            this.llWalletStatisticRefTypeId2.Size = new System.Drawing.Size(249, 13);
            this.llWalletStatisticRefTypeId2.TabIndex = 19;
            this.llWalletStatisticRefTypeId2.TabStop = true;
            this.llWalletStatisticRefTypeId2.Text = "Ввод/вывод средств общий, по типу переводов";
            this.llWalletStatisticRefTypeId2.Click += new System.EventHandler(this.onStatsTypeSelect);
            // 
            // dgvWalletStatisticRefTypeId2
            // 
            this.dgvWalletStatisticRefTypeId2.AllowUserToAddRows = false;
            this.dgvWalletStatisticRefTypeId2.AllowUserToDeleteRows = false;
            this.dgvWalletStatisticRefTypeId2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWalletStatisticRefTypeId2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvWalletStatisticRefTypeId2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWalletStatisticRefTypeId2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWalletStatisticRefTypeId2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWalletStatisticRefTypeId2.Location = new System.Drawing.Point(3, 3);
            this.dgvWalletStatisticRefTypeId2.Name = "dgvWalletStatisticRefTypeId2";
            this.dgvWalletStatisticRefTypeId2.ReadOnly = true;
            this.dgvWalletStatisticRefTypeId2.Size = new System.Drawing.Size(489, 336);
            this.dgvWalletStatisticRefTypeId2.TabIndex = 8;
            // 
            // ControlStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.llBountyPrizeByMember);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tcStats);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.llWalletStatisticRefTypeId2);
            this.Controls.Add(this.llWalletStatisticRefTypeId);
            this.Controls.Add(this.dtpStatisticFilterLow);
            this.Controls.Add(this.llTransBetweenWallets);
            this.Controls.Add(this.dtpStatisticFilterTop);
            this.Controls.Add(this.llCorpWalletsInOutWithoutWithdrawals);
            this.Controls.Add(this.llCorpWalletsInOutWithWithdrawals);
            this.Name = "ControlStatistics";
            this.Size = new System.Drawing.Size(795, 448);
            this.tcStats.ResumeLayout(false);
            this.tpCorpWalletsInOutWithWithdrawals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpWalletsInOutWithWithdrawals)).EndInit();
            this.tpCorpWalletsInOutWithoutWithdrawals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorpWalletsInOutWithoutWithdrawals)).EndInit();
            this.tpTransBetweenWallets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbWithdrawalsBackground)).EndInit();
            this.tpWalletStatisticRefTypeId.ResumeLayout(false);
            this.tpWalletStatisticRefTypeId.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWalletStatisticRefTypeId)).EndInit();
            this.tpBountyPrizeByMember.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBountyByMember)).EndInit();
            this.tpWalletStatisticRefTypeId2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWalletStatisticRefTypeId2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel llBountyPrizeByMember;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tcStats;
        private System.Windows.Forms.TabPage tpCorpWalletsInOutWithWithdrawals;
        private System.Windows.Forms.DataGridView dgvCorpWalletsInOutWithWithdrawals;
        private System.Windows.Forms.TabPage tpCorpWalletsInOutWithoutWithdrawals;
        private System.Windows.Forms.DataGridView dgvCorpWalletsInOutWithoutWithdrawals;
        private System.Windows.Forms.TabPage tpTransBetweenWallets;
        private System.Windows.Forms.Label bWD1004;
        private System.Windows.Forms.Label bWD1002;
        private System.Windows.Forms.Label bWD1006;
        private System.Windows.Forms.Label bWD1005;
        private System.Windows.Forms.Label bWD1000;
        private System.Windows.Forms.Label bWD1003;
        private System.Windows.Forms.Label bWD1001;
        private System.Windows.Forms.Button bGetStatistic;
        private System.Windows.Forms.PictureBox pbWithdrawalsBackground;
        private System.Windows.Forms.TabPage tpWalletStatisticRefTypeId;
        private System.Windows.Forms.ComboBox cbWalletStatisticRefTypeId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgvWalletStatisticRefTypeId;
        private System.Windows.Forms.TabPage tpBountyPrizeByMember;
        private System.Windows.Forms.DataGridView dgvBountyByMember;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel llWalletStatisticRefTypeId;
        private System.Windows.Forms.DateTimePicker dtpStatisticFilterLow;
        private System.Windows.Forms.LinkLabel llTransBetweenWallets;
        private System.Windows.Forms.DateTimePicker dtpStatisticFilterTop;
        private System.Windows.Forms.LinkLabel llCorpWalletsInOutWithoutWithdrawals;
        private System.Windows.Forms.LinkLabel llCorpWalletsInOutWithWithdrawals;
        private System.Windows.Forms.TabPage tpWalletStatisticRefTypeId2;
        private System.Windows.Forms.LinkLabel llWalletStatisticRefTypeId2;
        private System.Windows.Forms.DataGridView dgvWalletStatisticRefTypeId2;
    }
}
