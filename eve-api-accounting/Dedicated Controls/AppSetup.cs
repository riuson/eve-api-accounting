using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.IO;
using System.Threading;
using System.Net;

namespace Accounting
{
    public partial class AppSetup : UserControl
    {
        public AppSetup(IMainControl mainControl, IHostWindow hostWindow)
        {
            InitializeComponent();
            mMainControl = mainControl;
            mHostWindow = hostWindow;
            mOptions = mainControl.Options;
            mData = mainControl.Data;
            mSession = mainControl.Session;
            mSession.FunctionCompleted += new FunctionCompletedHandler(mSession_FunctionCompleted);

            tbApiKey.Text = mOptions.ApiKey;
            tbUserId.Text = mOptions.UserId.ToString();

            string charName = mOptions.CharacterName;
            cbCharacterName.Items.Clear();
            cbCharacterName.Items.Add(charName);
            cbCharacterName.SelectedIndex = 0;
            if (mOptions.CharacterId != 0)
            {
                Image im;
                string fileName;
                if (mSession.GetCharImage(ImageSize.x256, mOptions.CharacterId, out im, out fileName))
                {
                    pbCharPortrait.Image = im;
                }
            }

            numDefaultDaysInterval.Value = mOptions.DefaultDaysInterval;
            cbContinueParsingOnExists.Checked = mOptions.ContinueParsing;

            mCharsTable = null;

            cbUserIdApiKeyPair.Items.AddRange(mOptions.UserIdApiKeyPairs);
            SetFullDropDownWidth(cbUserIdApiKeyPair);

            cbLoadImagesFromWeb.Checked = mOptions.LoadImagesFromWeb;

            timerAsyncDebugRun.Tick += new EventHandler(timerAsyncDebugRun_Tick);
        }

        private void mSession_FunctionCompleted(object sender, FunctionCompeletedEventArgs e)
        {
            if (e.ErrorMessage == "")
            {
                XmlNode nodeRoot = e.XmlResponse.DocumentElement;//.SelectSingleNode("/eveapi");
                XmlNode nodeCurrentTime = nodeRoot.SelectSingleNode("descendant::currentTime");
                DateTime currentTime = DateTime.Parse(nodeCurrentTime.InnerText);
                if (e.Function == ApiFunction.AccountCharacters)
                {
                    XmlNode nodeCharacters = nodeRoot.SelectSingleNode("descendant::rowset[@name='characters']");
                    mCharsTable = DataClass.ParseRowset(nodeCharacters);
                    cbCharacterName.Items.Clear();
                    foreach (DataRow row in mCharsTable.Rows)
                    {
                        string name = String.Format("{0} [{1}]", row["name"], row["corporationName"]);
                        cbCharacterName.Items.Add(name);
                    }
                    cbCharacterName.SelectedIndex = 0;
                    SetFullDropDownWidth(cbCharacterName);
                    cbCharacterName.DroppedDown = true;
                }
            }
        }
        #region variables
        #region простые переменные
        private IMainControl mMainControl;
        private IHostWindow mHostWindow;
        private Options mOptions;
        private Session mSession;
        private DataClass mData;
        System.Windows.Forms.Timer timerAsyncDebugRun = new System.Windows.Forms.Timer();
        #endregion

        #region private
        private DataTable mCharsTable;
        #endregion
        #endregion

        void RunFunctionAsync(string value)
        {
            //подготовка, если требуется
            switch (value)
            {
                case "":
                    break;
            }
#if DEBUG
            timerAsyncDebugRun.Tag = value;
            timerAsyncDebugRun.Start();
#else
            FunctionAsyncDelegate f = this.FunctionAsync;
            f.BeginInvoke(value, new AsyncCallback(FunctionCompletedCallBack), this);
#endif
        }

        string FunctionAsync(string value)
        {
            string result = "";
            if (value.Contains("import:invTypes"))
            {
                string fileName = value.Replace("import:invTypes:", "");
                mHostWindow.ShowStatus(30, "Импорт invTypes...");
                mData.ClearInvTypes();
                if (mData.ImportInvTypes(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
                //mData.GetInvTypes();
            }
            if (value.Contains("import:invFlags"))
            {
                string fileName = value.Replace("import:invFlags:", "");
                mHostWindow.ShowStatus(30, "Импорт invFlags...");
                mData.ClearInvFlags();
                if (mData.ImportInvFlags(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
                //mData.GetInvFlags();
            }
            if (value.Contains("import:invGroups"))
            {
                string fileName = value.Replace("import:invGroups:", "");
                mHostWindow.ShowStatus(30, "Импорт invGroups...");
                mData.ClearInvGroups();
                if (mData.ImportInvGroups(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
                //mData.GetInvGroups();
            }
            if (value.Contains("import:eveNames"))
            {
                string fileName = value.Replace("import:eveNames:", "");
                mHostWindow.ShowStatus(30, "Импорт eveNames...");
                mData.ClearEveNames();
                if (mData.ImportEveNames(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
            }
            if (value.Contains("import:mapSolarSystems"))
            {
                string fileName = value.Replace("import:mapSolarSystems:", "");
                mHostWindow.ShowStatus(30, "Импорт mapSolarSystems...");
                mData.ClearMapSolarSystems();
                if (mData.ImportMapSolarSystems(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
            }
            if (value.Contains("download_portrait"))
            {
                string str = value.Replace("download_portrait:", "");
                int characterId = Convert.ToInt32(str);
                string fileName = null;
                Image im;
                if (mSession.GetCharImage(ImageSize.x256, mOptions.CharacterId, out im, out fileName))
                {
                    result = "download_portrait:" + fileName;
                }
            }
            if (value.Contains("import:eveGraphics"))
            {
                string fileName = value.Replace("import:eveGraphics:", "");
                mHostWindow.ShowStatus(30, "Импорт eveGraphics...");
                mData.ClearEveGraphics();
                if (mData.ImportEveGraphics(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
            }
            if (value.Contains("import:invCategories"))
            {
                string fileName = value.Replace("import:invCategories:", "");
                mHostWindow.ShowStatus(30, "Импорт invCategories...");
                mData.ClearInvCategories();
                if (mData.ImportInvCategories(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
            }
            if (value.Contains("import:dgmAttributeTypes"))
            {
                string fileName = value.Replace("import:dgmAttributeTypes:", "");
                mHostWindow.ShowStatus(30, "Импорт dgmAttributeTypes...");
                mData.ClearDgmAttributeTypes();
                if (mData.ImportDgmAttributeTypes(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
            }
            if (value.Contains("import:dgmEffects"))
            {
                string fileName = value.Replace("import:dgmEffects:", "");
                mHostWindow.ShowStatus(30, "Импорт dgmEffects...");
                mData.ClearDgmEffects();
                if (mData.ImportDgmEffects(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
            }
            if (value.Contains("import:dgmTypeAttributes"))
            {
                string fileName = value.Replace("import:dgmTypeAttributes:", "");
                mHostWindow.ShowStatus(30, "Импорт dgmTypeAttributes...");
                mData.ClearDgmTypeAttributes();
                if (mData.ImportDgmTypeAttributes(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
            }
            if (value.Contains("import:dgmTypeEffects"))
            {
                string fileName = value.Replace("import:dgmTypeEffects:", "");
                mHostWindow.ShowStatus(30, "Импорт dgmTypeEffects...");
                mData.ClearDgmTypeEffects();
                if (mData.ImportDgmTypeEffects(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
            }
            if (value.Contains("import:invControlTowerResources"))
            {
                string fileName = value.Replace("import:invControlTowerResources:", "");
                mHostWindow.ShowStatus(30, "Импорт invControlTowerResources...");
                mData.ClearinvControlTowerResources();
                if (mData.ImportinvControlTowerResources(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
            }
            if (value.Contains("import:invControlTowerResourcePurposes"))
            {
                string fileName = value.Replace("import:invControlTowerResourcePurposes:", "");
                mHostWindow.ShowStatus(30, "Импорт invControlTowerResourcePurposes...");
                mData.ClearInvControlTowerResourcePurposes();
                if (mData.ImportInvControlTowerResourcePurposes(fileName))
                {
                    mHostWindow.ShowStatus(100, "Завершено");
                    result = value;
                }
            }
            if (result == "")
                mHostWindow.ShowStatus("Произошла ошибка");
            else
                mHostWindow.ShowStatus("Завершено");
            return result;
        }
        void FunctionCompletedCallBack(IAsyncResult result)
        {
            try
            {
                AsyncResult r = (AsyncResult)result;
                FunctionAsyncDelegate command = (FunctionAsyncDelegate)r.AsyncDelegate;
                string val = command.EndInvoke(result);
                FunctionCompleted(val);
            }
            catch (Exception exc)
            {
                mMainControl.ProcessException(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
        void FunctionCompleted(string value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((EventHandler)delegate
                {
                    FunctionCompleted(value);
                });
            }
            else
            {
                if (value.Contains("download_portrait"))
                {
                    value = value.Replace("download_portrait:", "");
                    pbCharPortrait.ImageLocation = value;
                }
            }
        }
        void timerAsyncDebugRun_Tick(object sender, EventArgs e)
        {
            timerAsyncDebugRun.Stop();
            string value = (string)timerAsyncDebugRun.Tag;
            value = FunctionAsync(value);
            FunctionCompleted(value);
        }

        delegate string FunctionAsyncDelegate(string value);

        private void BeforeDestroy()
        {
            try
            {
                mSession.FunctionCompleted -= new FunctionCompletedHandler(mSession_FunctionCompleted);
                mOptions.ApiKey = tbApiKey.Text;
                mOptions.UserId = Convert.ToInt32(tbUserId.Text);
                mOptions.DefaultDaysInterval = Convert.ToInt32(numDefaultDaysInterval.Value);
                mOptions.ContinueParsing = cbContinueParsingOnExists.Checked;

                string val = String.Format("{0},{1}", tbUserId.Text, tbApiKey.Text);
                if (!cbUserIdApiKeyPair.Items.Contains(val))
                    cbUserIdApiKeyPair.Items.Add(val);
                List<string> strs = new List<string>();
                foreach (object obj in cbUserIdApiKeyPair.Items)
                {
                    strs.Add(Convert.ToString(obj));
                }
                mOptions.UserIdApiKeyPairs = strs.ToArray();
                mOptions.LoadImagesFromWeb = cbLoadImagesFromWeb.Checked;
                mOptions.Save();
            }
            catch (Exception exc)
            {
                //mMainControl.ProcessException(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == bSelectCharacter)
            {
                mOptions.ApiKey = tbApiKey.Text;
                mOptions.UserId = Convert.ToInt32(tbUserId.Text);
                mSession.GetFunctionAsync(ApiFunction.AccountCharacters);
            }
            if (sender == cbCharacterName)
            {
                if (mCharsTable != null)
                {
                    mOptions.CharacterName = cbCharacterName.Text;
                    mOptions.CharacterId = Convert.ToInt32(mCharsTable.Rows[cbCharacterName.SelectedIndex]["characterID"]);
                    RunFunctionAsync(String.Format("download_portrait:{0}", mOptions.CharacterId));
                }
            }
            if (sender == bUserIdApiKeySave)
            {
                string val = String.Format("{0},{1}", tbUserId.Text, tbApiKey.Text);
                if (!cbUserIdApiKeyPair.Items.Contains(val))
                    cbUserIdApiKeyPair.Items.Add(val);
                SetFullDropDownWidth(cbUserIdApiKeyPair);
            }
            if (sender == bUserIdApiKeyDelete)
            {
                if (cbUserIdApiKeyPair.SelectedIndex >= 0)
                {
                    object obj = cbUserIdApiKeyPair.SelectedItem;
                    cbUserIdApiKeyPair.Items.Remove(obj);
                }
            }
            if (sender == cbUserIdApiKeyPair)
            {
                string[] strs = Convert.ToString(cbUserIdApiKeyPair.SelectedItem).Split(',');
                tbUserId.Text = strs[0];
                tbApiKey.Text = strs[1];
            }
            if (sender == bImportInvTypes)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа invTypes";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:invTypes:{0}", ofd.FileName));
                }
            }
            if (sender == bImportEveNames)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа eveNames";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:eveNames:{0}", ofd.FileName));
                }
            }
            if (sender == bImportInvFlags)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа invFlags";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:invFlags:{0}", ofd.FileName));
                }
            }
            if (sender == bImportInvGroups)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа invGroups";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:invGroups:{0}", ofd.FileName));
                }
            }
            if (sender == bMapSolarSystems)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа mapSolarSystems";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:mapSolarSystems:{0}", ofd.FileName));
                }
            }
            if (sender == bImportEveGraphics)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа eveGraphics";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:eveGraphics:{0}", ofd.FileName));
                }
            }
            if (sender == bImportInvCategories)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа invCategories";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:invCategories:{0}", ofd.FileName));
                }
            }
            if (sender == bImportDgmAttributeTypes)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа dgmAttributeTypes";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:dgmAttributeTypes:{0}", ofd.FileName));
                }
            }
            if (sender == bImportDgmEffects)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа dgmEffects";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:dgmEffects:{0}", ofd.FileName));
                }
            }
            if (sender == bImportDgmTypeAttributes)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа dgmTypeAttributes";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:dgmTypeAttributes:{0}", ofd.FileName));
                }
            }
            if (sender == bImportDgmTypeEffects)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа dgmTypeEffects";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:dgmTypeEffects:{0}", ofd.FileName));
                }
            }
            if (sender == bImportInvControlTowerResources)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа invControlTowerResources";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:invControlTowerResources:{0}", ofd.FileName));
                }
            }
            if (sender == bImportInvControlTowerResourcePurposes)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Выберите файл дампа invControlTowerResourcePurposes";
                ofd.Filter = "Файлы с разделителями (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    RunFunctionAsync(String.Format("import:invControlTowerResourcePurposes:{0}", ofd.FileName));
                }
            }
            if (sender == bClearDatabase)
            {
                FormConfirm form = new FormConfirm();
                form.Text = "Подтверждение опасной операции";
                form.Promt = "Введите указанный ниже код в текстовое поле:";
                form.CancelText = "Отмена";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    mData.ClearCache();
                    
                    mData.ClearCharAccountBalance();
                    mData.ClearCorpAccountBalance();

                    mData.ClearCharWalletJournal();
                    mData.ClearCorpWalletJournal();
                    
                    mData.ClearCharWalletTransactions();
                    mData.ClearCorpWalletTransactions();
                    
                    //mData.ClearRefTypesList();
                    mData.ClearRequestsLog();

                    mData.ClearCorporationSheet();
                    mData.ClearCorpStarbaseList();
                    mData.ClearConquerableStationsList();
                    mData.ClearAssetList();

                    mData.ClearCharIndustryJobs();
                    mData.ClearCorpIndustryJobs();

                    mData.ClearKillLogs();
                    mData.ClearKillLogVictims();
                    mData.ClearKillLogAttackers();
                    mData.ClearKillLogItems();

                    mData.ClearCorpStarbaseStructures();
                    mData.ClearControlTowerFuelPrices();
                }
            }
        }

        private void llGetUserIdAndApiKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://myeve.eve-online.com/api/default.asp");
        }

        private void SetFullDropDownWidth(ComboBox cb)
        {
            int width = 0;
            foreach (object obj in cb.Items)
            {
                string str = Convert.ToString(obj);
                if (width < TextRenderer.MeasureText(str, cb.Font).Width)
                    width = TextRenderer.MeasureText(str, cb.Font).Width;
            }
            if (width > cb.DropDownWidth)
                cb.DropDownWidth = width;
        }
    }
}
