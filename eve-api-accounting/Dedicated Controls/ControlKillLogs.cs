using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Xml;
using System.Threading;

namespace Accounting
{
    public partial class ControlKillLogs : UserControl
    {
        public ControlKillLogs(IMainControl mainControl, IHostWindow hostWindow)
        {
            InitializeComponent();

            mMainControl = mainControl;
            mOptions = mainControl.Options;
            mData = mainControl.Data;
            mSession = mainControl.Session;
            mSession.FunctionCompleted += new FunctionCompletedHandler(mSession_FunctionCompleted);
            mHostWindow = hostWindow;

            timerAsyncDebugRun.Interval = 500;
            timerAsyncDebugRun.Tick += new EventHandler(timerAsyncDebugRun_Tick);
            timerStringAsyncDebugRun.Interval = 500;
            timerStringAsyncDebugRun.Tick += new EventHandler(timerStringAsyncDebugRun_Tick);

            if (mMainControl.GetConfigValue("kills view corp") == null)
                mViewForCorp = false;
            else
            {
                mViewForCorp = (bool)mMainControl.GetConfigValue("kills view corp");
                if (mViewForCorp)
                    rbCorpKillLog.Checked = mViewForCorp;
            }
            mData.GetKillLogsForView(mOptions.CharacterId, mViewForCorp);
            int killId = -1;
            if (mMainControl.GetConfigValue("kills killid") != null)
                killId = Convert.ToInt32(mMainControl.GetConfigValue("kills killid"));
            ConnectDataSource(true);
            dgvKillLog.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
            if (killId > 0)
                DataClass.SetDataGridRowToId(dgvKillLog, "killId", killId);
        }
        #region variables
        #region простые переменные
        private IMainControl mMainControl;
        private IHostWindow mHostWindow;
        private Options mOptions;
        private Session mSession;
        private DataClass mData;
        System.Windows.Forms.Timer timerAsyncDebugRun = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timerStringAsyncDebugRun = new System.Windows.Forms.Timer();
        #endregion

        #region private
        Image mDownloadedImage;
        int mDownloadedImageId;
        string mDownloadedColumnName;
        DataRow mDownloadedRow;
        bool mViewForCorp;
        #endregion
        #endregion

        void RunFunctionAsync(FunctionCompeletedEventArgs args)
        {
            //подготовка, если требуется
            switch (args.Function)
            {
                case ApiFunction.CorpKillLog:
                case ApiFunction.CharKillLog:
                    ConnectDataSource(false);
                    break;
            }
#if DEBUG
            timerAsyncDebugRun.Tag = args;
            timerAsyncDebugRun.Start();
#else
            FunctionAsyncDelegate f = this.FunctionAsync;
            f.BeginInvoke(args, new AsyncCallback(FunctionCompletedCallBack), this);
#endif
        }

        ApiFunction FunctionAsync(FunctionCompeletedEventArgs args)
        {
            ApiFunction result = ApiFunction.None;
            XmlNode nodeRoot = args.XmlResponse.DocumentElement;
            XmlNode nodeCurrentTime = nodeRoot.SelectSingleNode("descendant::currentTime");
            DateTime currentTime = DateTime.Parse(nodeCurrentTime.InnerText);
            bool continueParsing = mOptions.ContinueParsing;
            int accountKey = Convert.ToInt32(mSession.AccountKey);
            switch (args.Function)
            {
                #region ApiFunction.CharKillLog
                case ApiFunction.CharKillLog:
                    {
                        mHostWindow.ShowStatus(0, "Обновление...");
                        //нода спсика киллов
                        XmlNode nodeKills = nodeRoot.SelectSingleNode("descendant::rowset[@name='kills']");
                        mData.GetKillLogs(mOptions.CharacterId);
                        //выборка отдельного килла
                        bool killRecordExists = false;
                        int beforeKillId = 0;
                        foreach (XmlNode nodeKill in nodeKills.ChildNodes)
                        {
                            int killId = Convert.ToInt32(nodeKill.Attributes["killID"].Value);
                            beforeKillId = killId;
                            if (mData.IsKillLogRecordExists(mOptions.CharacterId, killId, false))
                            {
                                killRecordExists = true;
                            }
                            else
                            {
                                mHostWindow.ShowStatus(50, String.Format("Добавление: Kill Id {0}...", killId));
                                int solarSystemId = Convert.ToInt32(nodeKill.Attributes["solarSystemID"].Value);
                                DateTime killTime = DateTime.Parse(nodeKill.Attributes["killTime"].Value);
                                int moonId = Convert.ToInt32(nodeKill.Attributes["moonID"].Value);

                                Guid killRecordId = Guid.NewGuid();

                                DataRow rowKill = mData.TableKillLogs.NewRow();
                                rowKill["isCorp"] = false;
                                rowKill["killId"] = killId;
                                rowKill["solarSystemId"] = solarSystemId;
                                rowKill["killTime"] = killTime;
                                rowKill["moonId"] = moonId;
                                rowKill["recordId"] = killRecordId;
                                mData.TableKillLogs.Rows.Add(rowKill);
                                mData.CommitKillLogs();

                                //Victim
                                {
                                    mData.GetKillLogVictims(killRecordId);
                                    XmlNode nodeVictim = nodeKill["victim"];

                                    int characterId = Convert.ToInt32(nodeVictim.Attributes["characterID"].Value);
                                    string characterName = Convert.ToString(nodeVictim.Attributes["characterName"].Value);
                                    int corporationId = Convert.ToInt32(nodeVictim.Attributes["corporationID"].Value);
                                    string corporationName = Convert.ToString(nodeVictim.Attributes["corporationName"].Value);
                                    int allianceId = Convert.ToInt32(nodeVictim.Attributes["allianceID"].Value);
                                    string allianceName = Convert.ToString(nodeVictim.Attributes["allianceName"].Value);
                                    int damageTaken = Convert.ToInt32(nodeVictim.Attributes["damageTaken"].Value);
                                    int shipTypeId = Convert.ToInt32(nodeVictim.Attributes["shipTypeID"].Value);

                                    DataRow rowVictim = mData.TableKillLogVictims.NewRow();
                                    rowVictim["characterId"] = characterId;
                                    rowVictim["characterName"] = characterName;
                                    rowVictim["corporationId"] = corporationId;
                                    rowVictim["corporationName"] = corporationName;
                                    rowVictim["allianceId"] = allianceId;
                                    rowVictim["allianceName"] = allianceName;
                                    rowVictim["damageTaken"] = damageTaken;
                                    rowVictim["shipTypeId"] = shipTypeId;
                                    mData.TableKillLogVictims.Rows.Add(rowVictim);

                                    mData.CommitKillLogVictims();
                                }
                                //Attackers
                                {
                                    mData.GetKillLogAttackers(killRecordId);

                                    XmlNode nodeAttackers = nodeKill.SelectSingleNode("descendant::rowset[@name='attackers']");
                                    DataTable dtAttackers = DataClass.ParseRowset(nodeAttackers);

                                    int totalRows = dtAttackers.Rows.Count;
                                    foreach (DataRow attacker in dtAttackers.Rows)
                                    {
                                        DataRow newAttacker = mData.TableKillLogAttackers.NewRow();
                                        for (int i = 0; i < dtAttackers.Columns.Count; i++)
                                        {
                                            string val = Convert.ToString(attacker[dtAttackers.Columns[i].ColumnName]);
                                            if (mData.TableKillLogAttackers.Columns[dtAttackers.Columns[i].ColumnName].DataType == typeof(Boolean))
                                            {
                                                if (val == "0") val = "false";
                                                else val = "true";
                                            }
                                            if (mData.TableKillLogAttackers.Columns[dtAttackers.Columns[i].ColumnName].DataType == typeof(Double))
                                            {
                                                val = DataClass.ReplaceDecimalSeparator(val);
                                            }
                                            newAttacker[dtAttackers.Columns[i].ColumnName] = val;
                                        }
                                        mData.TableKillLogAttackers.Rows.Add(newAttacker);
                                    }
                                    mData.CommitKillLogAttackers();
                                }
                                //Items
                                {
                                    mData.GetKillLogItems(killRecordId);

                                    XmlNode nodeItems = nodeKill.SelectSingleNode("descendant::rowset[@name='items']");
                                    DataTable dtItems = DataClass.ParseRowset(nodeItems);

                                    int totalRows = dtItems.Rows.Count;
                                    foreach (DataRow item in dtItems.Rows)
                                    {
                                        DataRow newItem = mData.TableKillLogItems.NewRow();
                                        for (int i = 0; i < dtItems.Columns.Count; i++)
                                        {
                                            string val = Convert.ToString(item[dtItems.Columns[i].ColumnName]);
                                            if (mData.TableKillLogItems.Columns[dtItems.Columns[i].ColumnName].DataType == typeof(Boolean))
                                            {
                                                if (val == "0") val = "false";
                                                else val = "true";
                                            }
                                            if (mData.TableKillLogItems.Columns[dtItems.Columns[i].ColumnName].DataType == typeof(Double))
                                            {
                                                val = DataClass.ReplaceDecimalSeparator(val);
                                            }
                                            newItem[dtItems.Columns[i].ColumnName] = val;
                                        }
                                        mData.TableKillLogItems.Rows.Add(newItem);
                                    }
                                    mData.CommitKillLogItems();
                                }
                            }
                        }
                        if (nodeKills.ChildNodes.Count > 0 && (!killRecordExists || mOptions.ContinueParsing))
                        {
                            mSession.CommandQueue.Enqueue(new Command(ApiFunction.CharKillLog, mOptions.UserId, mOptions.ApiKey, mOptions.CharacterId, beforeKillId));
                        }
                        mViewForCorp = false;
                        mData.GetKillLogsForView(mOptions.CharacterId, mViewForCorp);
                        result = args.Function;
                        break;
                    }
                #endregion
                #region ApiFunction.CorpKillLog
                case ApiFunction.CorpKillLog:
                    {
                        mHostWindow.ShowStatus(0, "Обновление...");
                        //нода спсика киллов
                        XmlNode nodeKills = nodeRoot.SelectSingleNode("descendant::rowset[@name='kills']");
                        mData.GetKillLogs(mOptions.CharacterId);
                        //выборка отдельного килла
                        bool killRecordExists = false;
                        int beforeKillId = 0;
                        foreach (XmlNode nodeKill in nodeKills.ChildNodes)
                        {
                            int killId = Convert.ToInt32(nodeKill.Attributes["killID"].Value);
                            beforeKillId = killId;
                            if (mData.IsKillLogRecordExists(mOptions.CharacterId, killId, true))
                            {
                                killRecordExists = true;
                            }
                            else
                            {
                                mHostWindow.ShowStatus(50, String.Format("Добавление: Kill Id {0}...", killId));
                                int solarSystemId = Convert.ToInt32(nodeKill.Attributes["solarSystemID"].Value);
                                DateTime killTime = DateTime.Parse(nodeKill.Attributes["killTime"].Value);
                                int moonId = Convert.ToInt32(nodeKill.Attributes["moonID"].Value);

                                Guid killRecordId = Guid.NewGuid();

                                DataRow rowKill = mData.TableKillLogs.NewRow();
                                rowKill["isCorp"] = true;
                                rowKill["killId"] = killId;
                                rowKill["solarSystemId"] = solarSystemId;
                                rowKill["killTime"] = killTime;
                                rowKill["moonId"] = moonId;
                                rowKill["recordId"] = killRecordId;
                                mData.TableKillLogs.Rows.Add(rowKill);
                                mData.CommitKillLogs();

                                //Victim
                                {
                                    mData.GetKillLogVictims(killRecordId);
                                    XmlNode nodeVictim = nodeKill["victim"];

                                    int characterId = Convert.ToInt32(nodeVictim.Attributes["characterID"].Value);
                                    string characterName = Convert.ToString(nodeVictim.Attributes["characterName"].Value);
                                    int corporationId = Convert.ToInt32(nodeVictim.Attributes["corporationID"].Value);
                                    string corporationName = Convert.ToString(nodeVictim.Attributes["corporationName"].Value);
                                    int allianceId = Convert.ToInt32(nodeVictim.Attributes["allianceID"].Value);
                                    string allianceName = Convert.ToString(nodeVictim.Attributes["allianceName"].Value);
                                    int damageTaken = Convert.ToInt32(nodeVictim.Attributes["damageTaken"].Value);
                                    int shipTypeId = Convert.ToInt32(nodeVictim.Attributes["shipTypeID"].Value);

                                    DataRow rowVictim = mData.TableKillLogVictims.NewRow();
                                    rowVictim["characterId"] = characterId;
                                    rowVictim["characterName"] = characterName;
                                    rowVictim["corporationId"] = corporationId;
                                    rowVictim["corporationName"] = corporationName;
                                    rowVictim["allianceId"] = allianceId;
                                    rowVictim["allianceName"] = allianceName;
                                    rowVictim["damageTaken"] = damageTaken;
                                    rowVictim["shipTypeId"] = shipTypeId;
                                    mData.TableKillLogVictims.Rows.Add(rowVictim);

                                    mData.CommitKillLogVictims();
                                }
                                //Attackers
                                {
                                    mData.GetKillLogAttackers(killRecordId);

                                    XmlNode nodeAttackers = nodeKill.SelectSingleNode("descendant::rowset[@name='attackers']");
                                    DataTable dtAttackers = DataClass.ParseRowset(nodeAttackers);

                                    int totalRows = dtAttackers.Rows.Count;
                                    foreach (DataRow attacker in dtAttackers.Rows)
                                    {
                                        DataRow newAttacker = mData.TableKillLogAttackers.NewRow();
                                        for (int i = 0; i < dtAttackers.Columns.Count; i++)
                                        {
                                            string val = Convert.ToString(attacker[dtAttackers.Columns[i].ColumnName]);
                                            if (mData.TableKillLogAttackers.Columns[dtAttackers.Columns[i].ColumnName].DataType == typeof(Boolean))
                                            {
                                                if (val == "0") val = "false";
                                                else val = "true";
                                            }
                                            if (mData.TableKillLogAttackers.Columns[dtAttackers.Columns[i].ColumnName].DataType == typeof(Double))
                                            {
                                                val = DataClass.ReplaceDecimalSeparator(val);
                                            }
                                            newAttacker[dtAttackers.Columns[i].ColumnName] = val;
                                        }
                                        mData.TableKillLogAttackers.Rows.Add(newAttacker);
                                    }
                                    mData.CommitKillLogAttackers();
                                }
                                //Items
                                {
                                    mData.GetKillLogItems(killRecordId);

                                    XmlNode nodeItems = nodeKill.SelectSingleNode("descendant::rowset[@name='items']");
                                    DataTable dtItems = DataClass.ParseRowset(nodeItems);

                                    int totalRows = dtItems.Rows.Count;
                                    foreach (DataRow item in dtItems.Rows)
                                    {
                                        DataRow newItem = mData.TableKillLogItems.NewRow();
                                        for (int i = 0; i < dtItems.Columns.Count; i++)
                                        {
                                            string val = Convert.ToString(item[dtItems.Columns[i].ColumnName]);
                                            if (mData.TableKillLogItems.Columns[dtItems.Columns[i].ColumnName].DataType == typeof(Boolean))
                                            {
                                                if (val == "0") val = "false";
                                                else val = "true";
                                            }
                                            if (mData.TableKillLogItems.Columns[dtItems.Columns[i].ColumnName].DataType == typeof(Double))
                                            {
                                                val = DataClass.ReplaceDecimalSeparator(val);
                                            }
                                            newItem[dtItems.Columns[i].ColumnName] = val;
                                        }
                                        mData.TableKillLogItems.Rows.Add(newItem);
                                    }
                                    mData.CommitKillLogItems();
                                }
                            }
                        }
                        if (nodeKills.ChildNodes.Count > 0 && (!killRecordExists || mOptions.ContinueParsing))
                        {
                            mSession.CommandQueue.Enqueue(new Command(ApiFunction.CharKillLog, mOptions.UserId, mOptions.ApiKey, mOptions.CharacterId, beforeKillId));
                        }
                        mViewForCorp = true;
                        mData.GetKillLogsForView(mOptions.CharacterId, mViewForCorp);
                        result = args.Function;
                        break;
                    }
                #endregion
            }
            if (result == ApiFunction.None)
                mHostWindow.ShowStatus("Произошла ошибка");
            else
                mHostWindow.ShowStatus(100, "Завершено");
            return result;
        }
        void FunctionCompletedCallBack(IAsyncResult result)
        {
            try
            {
                AsyncResult r = (AsyncResult)result;
                FunctionAsyncDelegate command = (FunctionAsyncDelegate)r.AsyncDelegate;
                ApiFunction function = command.EndInvoke(result);
                FunctionCompleted(function);
            }
            catch (Exception exc)
            {
                mMainControl.ProcessException(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
        void FunctionCompleted(ApiFunction function)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((EventHandler)delegate
                {
                    FunctionCompleted(function);
                });
            }
            else
            {
                switch (function)
                {
                    case ApiFunction.CharKillLog:
                        rbCharKillLog.Checked = true;
                        ConnectDataSource(true);
                        break;
                    case ApiFunction.CorpKillLog:
                        rbCorpKillLog.Checked = true;
                        ConnectDataSource(true);
                        break;
                }
                mSession.ExecuteCommandFromQueue();
            }
        }
        void timerAsyncDebugRun_Tick(object sender, EventArgs e)
        {
            timerAsyncDebugRun.Stop();
            FunctionCompeletedEventArgs args = (FunctionCompeletedEventArgs)timerAsyncDebugRun.Tag;
            FunctionAsync(args);
            FunctionCompleted(args.Function);
        }

        delegate ApiFunction FunctionAsyncDelegate(FunctionCompeletedEventArgs args);

        private void BeforeDestroy()
        {
            mSession.FunctionCompleted -= new FunctionCompletedHandler(mSession_FunctionCompleted);
            dgvKillLog.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
            mMainControl.SetConfigValue("kills view corp", mViewForCorp);
        }

        private void mSession_FunctionCompleted(object sender, FunctionCompeletedEventArgs e)
        {
            if (e.ErrorMessage == "")
            {
                RunFunctionAsync(e);
            }
            else
            {
                mSession.ExecuteCommandFromQueue();
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == bUpdate)
            {
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load(@"G:\Documents\Visual Studio 2005\Projects\eve api\Accounting_v1.1\XmlSamples\CorpKillLog.xml.xml");
                //RunFunctionAsync(new FunctionCompeletedEventArgs(ApiFunction.CharIndustryJobs, xmlDoc, ""));
                //xmlDoc.Load(@"G:\Documents\Visual Studio 2005\Projects\eve api\Accounting_v1.1\XmlSamples\CorpKillLog.xml");
                //RunFunctionAsync(new FunctionCompeletedEventArgs(ApiFunction.CorpKillLog, xmlDoc, ""));
                if (rbCharKillLog.Checked)
                {
                    mSession.CommandQueue.Enqueue(new Command(ApiFunction.CharKillLog, mOptions.UserId, mOptions.ApiKey, mOptions.CharacterId, 0));
                }
                if (rbCorpKillLog.Checked)
                {
                    mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpKillLog, mOptions.UserId, mOptions.ApiKey, mOptions.CharacterId, 0));
                }
                mSession.ExecuteCommandFromQueue();
            }
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            if (dgv == dgvKillLog)
            {
                //mData.FormatDataGridView(dgv);
                //dgv.Columns["recordId"].Visible = false;
                //dgv.Columns["victimShipId"].Visible = false;
                //dgv.Columns["victimCharacterId"].Visible = false;
                //dgv.Columns["charFynalBlowId"].Visible = false;

                //dgv.Columns["killId"].DisplayIndex = 0;
                //dgv.Columns["killTime"].DisplayIndex = 1;
                //dgv.Columns["victimShipImage"].DisplayIndex = 2;
                //dgv.Columns["victimShipImage"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                //dgv.Columns["victimShipImage"].Width = 64;
                //dgv.Columns["shipType"].DisplayIndex = 3;
                //dgv.Columns["victimImage"].DisplayIndex = 4;
                //dgv.Columns["victimImage"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                //dgv.Columns["victimImage"].Width = 64;
                //dgv.Columns["Victim"].DisplayIndex = 5;
                //dgv.Columns["charFynalBlowImage"].DisplayIndex = 6;
                //dgv.Columns["charFynalBlowImage"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                //dgv.Columns["charFynalBlowImage"].Width = 64;
                //dgv.Columns["charFynalBlow"].DisplayIndex = 7;
                //dgv.Columns["SolarSystem"].DisplayIndex = 8;
                dgv.Columns.Clear();

                DataGridViewTextBoxColumn colText = new DataGridViewTextBoxColumn();
                colText.Name = "killId";
                colText.HeaderText = "Kill Id";
                colText.DataPropertyName = "killId";
                colText.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns.Add(colText);

                colText = new DataGridViewTextBoxColumn();
                colText.Name = "killTime";
                colText.HeaderText = "Kill Time";
                colText.DataPropertyName = "killTime";
                colText.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns.Add(colText);

                DataGridViewImageColumn colImage = new DataGridViewImageColumn();
                colImage.Name = "victimShipImage";
                colImage.HeaderText = " ";
                colImage.DataPropertyName = "victimShipImage";
                colImage.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                dgv.Columns.Add(colImage);

                colText = new DataGridViewTextBoxColumn();
                colText.Name = "shipType";
                colText.HeaderText = "Ship Type";
                colText.DataPropertyName = "shipType";
                colText.Width = 150;
                dgv.Columns.Add(colText);

                colImage = new DataGridViewImageColumn();
                colImage.Name = "victimImage";
                colImage.HeaderText = " ";
                colImage.DataPropertyName = "victimImage";
                colImage.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                dgv.Columns.Add(colImage);

                colText = new DataGridViewTextBoxColumn();
                colText.Name = "Victim";
                colText.HeaderText = "Victim";
                colText.DataPropertyName = "Victim";
                colText.Width = 200;
                //colText.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns.Add(colText);

                colImage = new DataGridViewImageColumn();
                colImage.Name = "charFynalBlowImage";
                colImage.HeaderText = " ";
                colImage.DataPropertyName = "charFynalBlowImage";
                colImage.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                dgv.Columns.Add(colImage);

                colText = new DataGridViewTextBoxColumn();
                colText.Name = "charFynalBlow";
                colText.HeaderText = "Fynal Blow";
                colText.DataPropertyName = "charFynalBlow";
                colText.Width = 200;
                //colText.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns.Add(colText);

                colText = new DataGridViewTextBoxColumn();
                colText.Name = "SolarSystem";
                colText.HeaderText = "Solar System";
                colText.DataPropertyName = "SolarSystem";
                colText.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns.Add(colText);
            }
        }

        private void rbCharKillLog_Click(object sender, EventArgs e)
        {
            if (rbCorpKillLog.Checked != mViewForCorp)
            {
                ConnectDataSource(false);
                mViewForCorp = rbCorpKillLog.Checked;
                mData.GetKillLogsForView(mOptions.CharacterId, mViewForCorp);
                ConnectDataSource(true);
            }
        }

        private void dgvKillLog_DoubleClick(object sender, EventArgs e)
        {
            if (dgvKillLog.CurrentRow != null)
            {
                DataRowView drv = (DataRowView)dgvKillLog.CurrentRow.DataBoundItem;
                Guid recordId = new Guid(Convert.ToString(drv["recordId"]));
                mMainControl.SetConfigValue("kills killid", Convert.ToInt32(drv["killId"]));
                mMainControl.SwitchToInterfaceState(InterfaceState.KillLogView, true, recordId);
            }
        }

        private void ConnectDataSource(bool connect)
        {
            if (!connect)
            {
                dgvKillLog.DataSource = false;
            }
            else
            {
                dgvKillLog.AutoGenerateColumns = false;
                dgvKillLog.DataSource = mData.TableKillLogsForView;
                //mData.FormatDataGridView(dgvKillLog);
                SetupDataGridView(dgvKillLog);

                dgvKillLog.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvKillLog.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvKillLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvKillLog.RowTemplate.Height = 64;

                StartDownloadImages();
            }
        }

        private void StartDownloadImages()
        {
            foreach (DataRow row in mData.TableKillLogsForView.Rows)
            {
                string colName = "victimImage";
                if (row[colName] == null || row[colName] is DBNull)
                {
                    mDownloadedColumnName = colName;
                    mDownloadedRow = row;
                    mDownloadedImageId = Convert.ToInt32(row["victimCharacterId"]);
                    RunStringFunctionAsync("download:charImage");
                    break;
                }
                colName = "charFynalBlowImage";
                if (row[colName] == null || row[colName] is DBNull)
                {
                    mDownloadedColumnName = colName;
                    mDownloadedRow = row;
                    mDownloadedImageId = Convert.ToInt32(row["charFynalBlowId"]);
                    RunStringFunctionAsync("download:charImage");
                    break;
                }
                colName = "victimShipImage";
                if (row[colName] == null || row[colName] is DBNull)
                {
                    mDownloadedColumnName = colName;
                    mDownloadedRow = row;
                    mDownloadedImageId = Convert.ToInt32(row["victimShipId"]);
                    RunStringFunctionAsync("download:typeImage");
                    break;
                }
            }
        }


        void RunStringFunctionAsync(string value)
        {
            //подготовка, если требуется
            switch (value)
            {
                case "":
                    break;
            }
#if DEBUG
            timerStringAsyncDebugRun.Tag = value;
            timerStringAsyncDebugRun.Start();
#else
            StringFunctionAsyncDelegate f = this.StringFunctionAsync;
            f.BeginInvoke(value, new AsyncCallback(StringFunctionCompletedCallBack), this);
#endif
        }

        string StringFunctionAsync(string value)
        {
            string result = "";
            if (value.Contains("download"))
            {
                string fileName;
                if (value == "download:charImage")
                {
                    if (!mSession.GetCharImage(ImageSize.x64, mDownloadedImageId, out mDownloadedImage, out fileName))
                        mDownloadedImage = InternalResources.NoPortrait;
                    result = value;
                }
                if (value == "download:typeImage")
                {
                    if (!mSession.GetTypeImage(ImageSize.x64, mDownloadedImageId, out mDownloadedImage))
                        mDownloadedImage = InternalResources.NoPortrait;
                    result = value;
                }
            }
            if (result == "")
                mHostWindow.ShowStatus("Произошла ошибка");
            else
                mHostWindow.ShowStatus("Завершено");
            return result;
        }
        void StringFunctionCompletedCallBack(IAsyncResult result)
        {
            try
            {
                AsyncResult r = (AsyncResult)result;
                StringFunctionAsyncDelegate command = (StringFunctionAsyncDelegate)r.AsyncDelegate;
                string val = command.EndInvoke(result);
                StringFunctionCompleted(val);
            }
            catch (Exception exc)
            {
                mMainControl.ProcessException(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
        void StringFunctionCompleted(string value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((EventHandler)delegate
                {
                    StringFunctionCompleted(value);
                });
            }
            else
            {
                if (value.Contains("download"))
                {
                    if (mDownloadedRow.RowState == DataRowState.Detached || mDownloadedRow.RowState == DataRowState.Deleted)
                    {
                    }
                    else
                    {
                        if (mDownloadedImage != null)
                        {
                            mDownloadedRow[mDownloadedColumnName] = mDownloadedImage.Clone();
                            Thread.Sleep(10);
                            StartDownloadImages();
                        }
                    }
                }
            }
        }
        void timerStringAsyncDebugRun_Tick(object sender, EventArgs e)
        {
            timerStringAsyncDebugRun.Stop();
            string value = (string)timerStringAsyncDebugRun.Tag;
            value = StringFunctionAsync(value);
            StringFunctionCompleted(value);
        }
        delegate string StringFunctionAsyncDelegate(string value);
    }
}
