namespace Accounting
{
    partial class AppSetup
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
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.cbUserIdApiKeyPair = new System.Windows.Forms.ComboBox();
            this.bUserIdApiKeyDelete = new System.Windows.Forms.Button();
            this.bUserIdApiKeySave = new System.Windows.Forms.Button();
            this.cbCharacterName = new System.Windows.Forms.ComboBox();
            this.llGetUserIdAndApiKey = new System.Windows.Forms.LinkLabel();
            this.bSelectCharacter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbApiKey = new System.Windows.Forms.TextBox();
            this.tbUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lCharacter = new System.Windows.Forms.Label();
            this.gbOtherParameters = new System.Windows.Forms.GroupBox();
            this.cbLoadImagesFromWeb = new System.Windows.Forms.CheckBox();
            this.bClearDatabase = new System.Windows.Forms.Button();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.lLanguage = new System.Windows.Forms.Label();
            this.cbContinueParsingOnExists = new System.Windows.Forms.CheckBox();
            this.numDefaultDaysInterval = new System.Windows.Forms.NumericUpDown();
            this.lDefaultDateInterval = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bImportInvCategories = new System.Windows.Forms.Button();
            this.bImportEveGraphics = new System.Windows.Forms.Button();
            this.bMapSolarSystems = new System.Windows.Forms.Button();
            this.bImportInvGroups = new System.Windows.Forms.Button();
            this.bImportInvFlags = new System.Windows.Forms.Button();
            this.bImportInvTypes = new System.Windows.Forms.Button();
            this.bImportDgmTypeAttributes = new System.Windows.Forms.Button();
            this.bImportDgmTypeEffects = new System.Windows.Forms.Button();
            this.bImportDgmEffects = new System.Windows.Forms.Button();
            this.bImportInvControlTowerResourcePurposes = new System.Windows.Forms.Button();
            this.bImportInvControlTowerResources = new System.Windows.Forms.Button();
            this.bImportDgmAttributeTypes = new System.Windows.Forms.Button();
            this.bImportEveNames = new System.Windows.Forms.Button();
            this.pbCharPortrait = new System.Windows.Forms.PictureBox();
            this.gbLogin.SuspendLayout();
            this.gbOtherParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDefaultDaysInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCharPortrait)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLogin
            // 
            this.gbLogin.Controls.Add(this.cbUserIdApiKeyPair);
            this.gbLogin.Controls.Add(this.bUserIdApiKeyDelete);
            this.gbLogin.Controls.Add(this.bUserIdApiKeySave);
            this.gbLogin.Controls.Add(this.cbCharacterName);
            this.gbLogin.Controls.Add(this.llGetUserIdAndApiKey);
            this.gbLogin.Controls.Add(this.bSelectCharacter);
            this.gbLogin.Controls.Add(this.label1);
            this.gbLogin.Controls.Add(this.tbApiKey);
            this.gbLogin.Controls.Add(this.tbUserId);
            this.gbLogin.Controls.Add(this.label2);
            this.gbLogin.Controls.Add(this.lCharacter);
            this.gbLogin.Location = new System.Drawing.Point(3, 3);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Size = new System.Drawing.Size(475, 129);
            this.gbLogin.TabIndex = 0;
            this.gbLogin.TabStop = false;
            this.gbLogin.Text = "Идентификация";
            // 
            // cbUserIdApiKeyPair
            // 
            this.cbUserIdApiKeyPair.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserIdApiKeyPair.FormattingEnabled = true;
            this.cbUserIdApiKeyPair.Location = new System.Drawing.Point(7, 95);
            this.cbUserIdApiKeyPair.Name = "cbUserIdApiKeyPair";
            this.cbUserIdApiKeyPair.Size = new System.Drawing.Size(395, 21);
            this.cbUserIdApiKeyPair.TabIndex = 5;
            this.cbUserIdApiKeyPair.SelectedIndexChanged += new System.EventHandler(this.OnButtonClick);
            // 
            // bUserIdApiKeyDelete
            // 
            this.bUserIdApiKeyDelete.Image = global::Accounting.Properties.Resources.delete;
            this.bUserIdApiKeyDelete.Location = new System.Drawing.Point(439, 93);
            this.bUserIdApiKeyDelete.Name = "bUserIdApiKeyDelete";
            this.bUserIdApiKeyDelete.Size = new System.Drawing.Size(25, 23);
            this.bUserIdApiKeyDelete.TabIndex = 7;
            this.bUserIdApiKeyDelete.UseVisualStyleBackColor = true;
            this.bUserIdApiKeyDelete.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bUserIdApiKeySave
            // 
            this.bUserIdApiKeySave.Image = global::Accounting.Properties.Resources.apply;
            this.bUserIdApiKeySave.Location = new System.Drawing.Point(408, 93);
            this.bUserIdApiKeySave.Name = "bUserIdApiKeySave";
            this.bUserIdApiKeySave.Size = new System.Drawing.Size(25, 23);
            this.bUserIdApiKeySave.TabIndex = 6;
            this.bUserIdApiKeySave.UseVisualStyleBackColor = true;
            this.bUserIdApiKeySave.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // cbCharacterName
            // 
            this.cbCharacterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCharacterName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbCharacterName.FormattingEnabled = true;
            this.cbCharacterName.Location = new System.Drawing.Point(214, 66);
            this.cbCharacterName.Name = "cbCharacterName";
            this.cbCharacterName.Size = new System.Drawing.Size(250, 21);
            this.cbCharacterName.TabIndex = 4;
            this.cbCharacterName.SelectedIndexChanged += new System.EventHandler(this.OnButtonClick);
            // 
            // llGetUserIdAndApiKey
            // 
            this.llGetUserIdAndApiKey.AutoSize = true;
            this.llGetUserIdAndApiKey.Location = new System.Drawing.Point(6, 16);
            this.llGetUserIdAndApiKey.Name = "llGetUserIdAndApiKey";
            this.llGetUserIdAndApiKey.Size = new System.Drawing.Size(133, 13);
            this.llGetUserIdAndApiKey.TabIndex = 0;
            this.llGetUserIdAndApiKey.TabStop = true;
            this.llGetUserIdAndApiKey.Text = "Получить UserId и ApiKey";
            this.llGetUserIdAndApiKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llGetUserIdAndApiKey_LinkClicked);
            // 
            // bSelectCharacter
            // 
            this.bSelectCharacter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bSelectCharacter.Location = new System.Drawing.Point(6, 64);
            this.bSelectCharacter.Name = "bSelectCharacter";
            this.bSelectCharacter.Size = new System.Drawing.Size(140, 23);
            this.bSelectCharacter.TabIndex = 3;
            this.bSelectCharacter.Text = "Выбрать чара";
            this.bSelectCharacter.UseVisualStyleBackColor = true;
            this.bSelectCharacter.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(318, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "UserId:";
            // 
            // tbApiKey
            // 
            this.tbApiKey.Location = new System.Drawing.Point(53, 41);
            this.tbApiKey.Name = "tbApiKey";
            this.tbApiKey.Size = new System.Drawing.Size(411, 20);
            this.tbApiKey.TabIndex = 2;
            this.tbApiKey.Text = "mu0M10FXqeGlkEj3FBK8vY0BbR2BDsEN84T05sdQduVw2SIFhfpo2zYiGEOMrYgp";
            // 
            // tbUserId
            // 
            this.tbUserId.Location = new System.Drawing.Point(367, 15);
            this.tbUserId.Name = "tbUserId";
            this.tbUserId.Size = new System.Drawing.Size(97, 20);
            this.tbUserId.TabIndex = 1;
            this.tbUserId.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ApiKey:";
            // 
            // lCharacter
            // 
            this.lCharacter.AutoSize = true;
            this.lCharacter.Location = new System.Drawing.Point(178, 69);
            this.lCharacter.Name = "lCharacter";
            this.lCharacter.Size = new System.Drawing.Size(30, 13);
            this.lCharacter.TabIndex = 1;
            this.lCharacter.Text = "Чар:";
            // 
            // gbOtherParameters
            // 
            this.gbOtherParameters.Controls.Add(this.cbLoadImagesFromWeb);
            this.gbOtherParameters.Controls.Add(this.bClearDatabase);
            this.gbOtherParameters.Controls.Add(this.cbLanguage);
            this.gbOtherParameters.Controls.Add(this.lLanguage);
            this.gbOtherParameters.Controls.Add(this.cbContinueParsingOnExists);
            this.gbOtherParameters.Controls.Add(this.numDefaultDaysInterval);
            this.gbOtherParameters.Controls.Add(this.lDefaultDateInterval);
            this.gbOtherParameters.Location = new System.Drawing.Point(3, 138);
            this.gbOtherParameters.Name = "gbOtherParameters";
            this.gbOtherParameters.Size = new System.Drawing.Size(475, 137);
            this.gbOtherParameters.TabIndex = 1;
            this.gbOtherParameters.TabStop = false;
            this.gbOtherParameters.Text = "Другое";
            // 
            // cbLoadImagesFromWeb
            // 
            this.cbLoadImagesFromWeb.AutoSize = true;
            this.cbLoadImagesFromWeb.Location = new System.Drawing.Point(6, 75);
            this.cbLoadImagesFromWeb.Name = "cbLoadImagesFromWeb";
            this.cbLoadImagesFromWeb.Size = new System.Drawing.Size(151, 17);
            this.cbLoadImagesFromWeb.TabIndex = 2;
            this.cbLoadImagesFromWeb.Text = "Загружать изображения";
            this.cbLoadImagesFromWeb.UseVisualStyleBackColor = true;
            // 
            // bClearDatabase
            // 
            this.bClearDatabase.AutoSize = true;
            this.bClearDatabase.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bClearDatabase.Location = new System.Drawing.Point(6, 96);
            this.bClearDatabase.Name = "bClearDatabase";
            this.bClearDatabase.Size = new System.Drawing.Size(134, 23);
            this.bClearDatabase.TabIndex = 3;
            this.bClearDatabase.Text = "Очистить базу данных";
            this.bClearDatabase.UseVisualStyleBackColor = true;
            this.bClearDatabase.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // cbLanguage
            // 
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(322, 18);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbLanguage.TabIndex = 4;
            this.cbLanguage.Visible = false;
            // 
            // lLanguage
            // 
            this.lLanguage.AutoSize = true;
            this.lLanguage.Location = new System.Drawing.Point(261, 21);
            this.lLanguage.Name = "lLanguage";
            this.lLanguage.Size = new System.Drawing.Size(35, 13);
            this.lLanguage.TabIndex = 3;
            this.lLanguage.Text = "Язык";
            this.lLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lLanguage.Visible = false;
            // 
            // cbContinueParsingOnExists
            // 
            this.cbContinueParsingOnExists.AutoSize = true;
            this.cbContinueParsingOnExists.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbContinueParsingOnExists.Location = new System.Drawing.Point(6, 51);
            this.cbContinueParsingOnExists.Name = "cbContinueParsingOnExists";
            this.cbContinueParsingOnExists.Size = new System.Drawing.Size(341, 18);
            this.cbContinueParsingOnExists.TabIndex = 1;
            this.cbContinueParsingOnExists.Text = "Продолжать добавлять строки даже если обнаружен повтор";
            this.cbContinueParsingOnExists.UseVisualStyleBackColor = true;
            // 
            // numDefaultDaysInterval
            // 
            this.numDefaultDaysInterval.Location = new System.Drawing.Point(192, 19);
            this.numDefaultDaysInterval.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numDefaultDaysInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDefaultDaysInterval.Name = "numDefaultDaysInterval";
            this.numDefaultDaysInterval.Size = new System.Drawing.Size(47, 20);
            this.numDefaultDaysInterval.TabIndex = 0;
            this.numDefaultDaysInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lDefaultDateInterval
            // 
            this.lDefaultDateInterval.AutoSize = true;
            this.lDefaultDateInterval.Location = new System.Drawing.Point(6, 21);
            this.lDefaultDateInterval.Name = "lDefaultDateInterval";
            this.lDefaultDateInterval.Size = new System.Drawing.Size(153, 13);
            this.lDefaultDateInterval.TabIndex = 0;
            this.lDefaultDateInterval.Text = "Интервал дат по умолчанию:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bImportInvCategories);
            this.groupBox1.Controls.Add(this.bImportEveGraphics);
            this.groupBox1.Controls.Add(this.bMapSolarSystems);
            this.groupBox1.Controls.Add(this.bImportInvGroups);
            this.groupBox1.Controls.Add(this.bImportInvFlags);
            this.groupBox1.Controls.Add(this.bImportInvTypes);
            this.groupBox1.Controls.Add(this.bImportDgmTypeAttributes);
            this.groupBox1.Controls.Add(this.bImportDgmTypeEffects);
            this.groupBox1.Controls.Add(this.bImportDgmEffects);
            this.groupBox1.Controls.Add(this.bImportInvControlTowerResourcePurposes);
            this.groupBox1.Controls.Add(this.bImportInvControlTowerResources);
            this.groupBox1.Controls.Add(this.bImportDgmAttributeTypes);
            this.groupBox1.Controls.Add(this.bImportEveNames);
            this.groupBox1.Location = new System.Drawing.Point(3, 281);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 147);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Импорт дополнительных таблиц";
            this.groupBox1.Visible = false;
            // 
            // bImportInvCategories
            // 
            this.bImportInvCategories.AutoSize = true;
            this.bImportInvCategories.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportInvCategories.Location = new System.Drawing.Point(189, 48);
            this.bImportInvCategories.Name = "bImportInvCategories";
            this.bImportInvCategories.Size = new System.Drawing.Size(98, 23);
            this.bImportInvCategories.TabIndex = 5;
            this.bImportInvCategories.Text = "Inv Categories...";
            this.bImportInvCategories.UseVisualStyleBackColor = true;
            this.bImportInvCategories.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportEveGraphics
            // 
            this.bImportEveGraphics.AutoSize = true;
            this.bImportEveGraphics.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportEveGraphics.Location = new System.Drawing.Point(97, 19);
            this.bImportEveGraphics.Name = "bImportEveGraphics";
            this.bImportEveGraphics.Size = new System.Drawing.Size(94, 23);
            this.bImportEveGraphics.TabIndex = 1;
            this.bImportEveGraphics.Text = "Eve Graphics...";
            this.bImportEveGraphics.UseVisualStyleBackColor = true;
            this.bImportEveGraphics.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bMapSolarSystems
            // 
            this.bMapSolarSystems.AutoSize = true;
            this.bMapSolarSystems.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bMapSolarSystems.Location = new System.Drawing.Point(197, 19);
            this.bMapSolarSystems.Name = "bMapSolarSystems";
            this.bMapSolarSystems.Size = new System.Drawing.Size(120, 23);
            this.bMapSolarSystems.TabIndex = 2;
            this.bMapSolarSystems.Text = "Map Solar Systems...";
            this.bMapSolarSystems.UseVisualStyleBackColor = true;
            this.bMapSolarSystems.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportInvGroups
            // 
            this.bImportInvGroups.AutoSize = true;
            this.bImportInvGroups.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportInvGroups.Location = new System.Drawing.Point(7, 48);
            this.bImportInvGroups.Name = "bImportInvGroups";
            this.bImportInvGroups.Size = new System.Drawing.Size(85, 23);
            this.bImportInvGroups.TabIndex = 3;
            this.bImportInvGroups.Text = "Inv Groups...";
            this.bImportInvGroups.UseVisualStyleBackColor = true;
            this.bImportInvGroups.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportInvFlags
            // 
            this.bImportInvFlags.AutoSize = true;
            this.bImportInvFlags.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportInvFlags.Location = new System.Drawing.Point(98, 48);
            this.bImportInvFlags.Name = "bImportInvFlags";
            this.bImportInvFlags.Size = new System.Drawing.Size(85, 23);
            this.bImportInvFlags.TabIndex = 4;
            this.bImportInvFlags.Text = "Inv Flags...";
            this.bImportInvFlags.UseVisualStyleBackColor = true;
            this.bImportInvFlags.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportInvTypes
            // 
            this.bImportInvTypes.AutoSize = true;
            this.bImportInvTypes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportInvTypes.Location = new System.Drawing.Point(293, 48);
            this.bImportInvTypes.Name = "bImportInvTypes";
            this.bImportInvTypes.Size = new System.Drawing.Size(85, 23);
            this.bImportInvTypes.TabIndex = 6;
            this.bImportInvTypes.Text = "Inv Types...";
            this.bImportInvTypes.UseVisualStyleBackColor = true;
            this.bImportInvTypes.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportDgmTypeAttributes
            // 
            this.bImportDgmTypeAttributes.AutoSize = true;
            this.bImportDgmTypeAttributes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportDgmTypeAttributes.Location = new System.Drawing.Point(232, 77);
            this.bImportDgmTypeAttributes.Name = "bImportDgmTypeAttributes";
            this.bImportDgmTypeAttributes.Size = new System.Drawing.Size(126, 23);
            this.bImportDgmTypeAttributes.TabIndex = 9;
            this.bImportDgmTypeAttributes.Text = "Dgm Type Attributes...";
            this.bImportDgmTypeAttributes.UseVisualStyleBackColor = true;
            this.bImportDgmTypeAttributes.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportDgmTypeEffects
            // 
            this.bImportDgmTypeEffects.AutoSize = true;
            this.bImportDgmTypeEffects.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportDgmTypeEffects.Location = new System.Drawing.Point(364, 77);
            this.bImportDgmTypeEffects.Name = "bImportDgmTypeEffects";
            this.bImportDgmTypeEffects.Size = new System.Drawing.Size(115, 23);
            this.bImportDgmTypeEffects.TabIndex = 10;
            this.bImportDgmTypeEffects.Text = "Dgm Type Effects...";
            this.bImportDgmTypeEffects.UseVisualStyleBackColor = true;
            this.bImportDgmTypeEffects.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportDgmEffects
            // 
            this.bImportDgmEffects.AutoSize = true;
            this.bImportDgmEffects.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportDgmEffects.Location = new System.Drawing.Point(138, 77);
            this.bImportDgmEffects.Name = "bImportDgmEffects";
            this.bImportDgmEffects.Size = new System.Drawing.Size(88, 23);
            this.bImportDgmEffects.TabIndex = 8;
            this.bImportDgmEffects.Text = "Dgm Effects...";
            this.bImportDgmEffects.UseVisualStyleBackColor = true;
            this.bImportDgmEffects.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportInvControlTowerResourcePurposes
            // 
            this.bImportInvControlTowerResourcePurposes.AutoSize = true;
            this.bImportInvControlTowerResourcePurposes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportInvControlTowerResourcePurposes.Location = new System.Drawing.Point(180, 106);
            this.bImportInvControlTowerResourcePurposes.Name = "bImportInvControlTowerResourcePurposes";
            this.bImportInvControlTowerResourcePurposes.Size = new System.Drawing.Size(210, 23);
            this.bImportInvControlTowerResourcePurposes.TabIndex = 12;
            this.bImportInvControlTowerResourcePurposes.Text = "Inv Control Tower Resource Purposes...";
            this.bImportInvControlTowerResourcePurposes.UseVisualStyleBackColor = true;
            this.bImportInvControlTowerResourcePurposes.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportInvControlTowerResources
            // 
            this.bImportInvControlTowerResources.AutoSize = true;
            this.bImportInvControlTowerResources.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportInvControlTowerResources.Location = new System.Drawing.Point(6, 106);
            this.bImportInvControlTowerResources.Name = "bImportInvControlTowerResources";
            this.bImportInvControlTowerResources.Size = new System.Drawing.Size(168, 23);
            this.bImportInvControlTowerResources.TabIndex = 11;
            this.bImportInvControlTowerResources.Text = "Inv Control Tower Resources...";
            this.bImportInvControlTowerResources.UseVisualStyleBackColor = true;
            this.bImportInvControlTowerResources.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportDgmAttributeTypes
            // 
            this.bImportDgmAttributeTypes.AutoSize = true;
            this.bImportDgmAttributeTypes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportDgmAttributeTypes.Location = new System.Drawing.Point(6, 77);
            this.bImportDgmAttributeTypes.Name = "bImportDgmAttributeTypes";
            this.bImportDgmAttributeTypes.Size = new System.Drawing.Size(126, 23);
            this.bImportDgmAttributeTypes.TabIndex = 7;
            this.bImportDgmAttributeTypes.Text = "Dgm Attribute Types...";
            this.bImportDgmAttributeTypes.UseVisualStyleBackColor = true;
            this.bImportDgmAttributeTypes.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // bImportEveNames
            // 
            this.bImportEveNames.AutoSize = true;
            this.bImportEveNames.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bImportEveNames.Location = new System.Drawing.Point(6, 19);
            this.bImportEveNames.Name = "bImportEveNames";
            this.bImportEveNames.Size = new System.Drawing.Size(85, 23);
            this.bImportEveNames.TabIndex = 0;
            this.bImportEveNames.Text = "Eve Names...";
            this.bImportEveNames.UseVisualStyleBackColor = true;
            this.bImportEveNames.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // pbCharPortrait
            // 
            this.pbCharPortrait.Location = new System.Drawing.Point(484, 3);
            this.pbCharPortrait.Name = "pbCharPortrait";
            this.pbCharPortrait.Size = new System.Drawing.Size(128, 128);
            this.pbCharPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCharPortrait.TabIndex = 9;
            this.pbCharPortrait.TabStop = false;
            // 
            // AppSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pbCharPortrait);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbOtherParameters);
            this.Controls.Add(this.gbLogin);
            this.Name = "AppSetup";
            this.Size = new System.Drawing.Size(673, 416);
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            this.gbOtherParameters.ResumeLayout(false);
            this.gbOtherParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDefaultDaysInterval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCharPortrait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.LinkLabel llGetUserIdAndApiKey;
        private System.Windows.Forms.Button bSelectCharacter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbApiKey;
        private System.Windows.Forms.TextBox tbUserId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lCharacter;
        private System.Windows.Forms.ComboBox cbCharacterName;
        private System.Windows.Forms.GroupBox gbOtherParameters;
        private System.Windows.Forms.Button bClearDatabase;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label lLanguage;
        private System.Windows.Forms.CheckBox cbContinueParsingOnExists;
        private System.Windows.Forms.NumericUpDown numDefaultDaysInterval;
        private System.Windows.Forms.Label lDefaultDateInterval;
        private System.Windows.Forms.Button bUserIdApiKeySave;
        private System.Windows.Forms.Button bUserIdApiKeyDelete;
        private System.Windows.Forms.ComboBox cbUserIdApiKeyPair;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bImportEveNames;
        private System.Windows.Forms.Button bImportInvTypes;
        private System.Windows.Forms.Button bImportInvFlags;
        private System.Windows.Forms.PictureBox pbCharPortrait;
        private System.Windows.Forms.Button bImportInvGroups;
        private System.Windows.Forms.Button bMapSolarSystems;
        private System.Windows.Forms.Button bImportEveGraphics;
        private System.Windows.Forms.Button bImportInvCategories;
        private System.Windows.Forms.CheckBox cbLoadImagesFromWeb;
        private System.Windows.Forms.Button bImportDgmAttributeTypes;
        private System.Windows.Forms.Button bImportDgmEffects;
        private System.Windows.Forms.Button bImportDgmTypeAttributes;
        private System.Windows.Forms.Button bImportDgmTypeEffects;
        private System.Windows.Forms.Button bImportInvControlTowerResourcePurposes;
        private System.Windows.Forms.Button bImportInvControlTowerResources;

    }
}
