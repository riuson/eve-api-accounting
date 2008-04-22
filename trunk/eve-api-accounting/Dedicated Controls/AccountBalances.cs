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
    public partial class AccountBalances : UserControl
    {
        public AccountBalances(IMainControl mainControl, IHostWindow hostWindow)
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

            mData.GetCorporationSheet(mOptions.CharacterId);

            dgvCharAccountBalance.DataSource = mData.TableCharAccountBalance;
            mData.GetCharAccountBalance(mOptions.CharacterId);
            SetupDataGridView(dgvCharAccountBalance);

            dgvCorpAccountBalance.DataSource = mData.TableCorpAccountBalance;
            mData.GetCorpAccountBalance(mOptions.CharacterId);
            SetupDataGridView(dgvCorpAccountBalance);

            DateTime dtMin = mData.CacheGetLastWalletUpdateTime(mOptions.CharacterId);
            if (dtMin != DateTime.MinValue)
            {
                lWalletsStatus.Text = String.Format("Обновление доступно после {0}", dtMin);
            }
            else
            {
                lWalletsStatus.Text = "Данные отсутствуют";
                if (MessageBox.Show(this, "Записей в кеше не обнаружено. Загрузить данные?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    OnButtonClick(bUpdate, new EventArgs());
                }
            }

            dgvCharAccountBalance.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
            dgvCorpAccountBalance.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
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
        #endregion
        #endregion

        void RunFunctionAsync(FunctionCompeletedEventArgs args)
        {
            //подготовка, если требуется
            switch (args.Function)
            {
                case ApiFunction.CharAccountBalance:
                    dgvCharAccountBalance.DataSource = null;
                    break;
                case ApiFunction.CorpAccountBalance:
                    dgvCorpAccountBalance.DataSource = null;
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
            bool addingFinish = false;
            bool continueOnExists = mOptions.ContinueParsing;
            int accountKey = Convert.ToInt32(mSession.AccountKey);
            switch (args.Function)
            {
                #region ApiFunction.CharAccountBalance
                case ApiFunction.CharAccountBalance:
                    {
                        XmlNode nodeCharWalletData = nodeRoot.SelectSingleNode("descendant::rowset[@name='accounts']");
                        DataTable dtCharWalletData = DataClass.ParseRowset(nodeCharWalletData);

                        int totalRows = dtCharWalletData.Rows.Count;
                        foreach (DataRow row in dtCharWalletData.Rows)
                        {
                            DataRow newRow;// = mData.TableCharAccountBalance.NewRow();

                            DataRow[] exists_rows = mData.TableCharAccountBalance.Select(String.Format("accountKey = {0}", row["accountKey"]));
                            if (exists_rows.Length == 1)
                                newRow = exists_rows[0];
                            else
                                newRow = mData.TableCharAccountBalance.NewRow();

                            mHostWindow.ShowStatus(
                                mData.TableCharAccountBalance.Rows.Count, totalRows,
                                String.Format("{2}, Добавление: {0}/{1}...", mData.TableCharAccountBalance.Rows.Count, totalRows, args.Function));

                            for (int i = 0; i < dtCharWalletData.Columns.Count; i++)
                            {
                                string val = Convert.ToString(row[dtCharWalletData.Columns[i].ColumnName]);
                                if (dtCharWalletData.Columns[i].ColumnName == "balance")
                                    val = DataClass.ReplaceDecimalSeparator(val);
                                newRow["balance_updated"] = currentTime;
                                newRow[dtCharWalletData.Columns[i].ColumnName] = val;
                            }
                            if (exists_rows.Length == 0)
                                mData.TableCharAccountBalance.Rows.Add(newRow);
                        }
                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitCharAccountBalance();

                        mHostWindow.ShowStatus(100, "Завершено");
                        //автоматически, после загрузки частного кошелька, запускаем загрузку корпоративного кошелька
                        //mSession.GetFunction(ApiFunction.CorpAccountBalance);
                        result = args.Function;
                        break;
                    }
                #endregion
                #region ApiFunction.CorpAccountBalance
                case ApiFunction.CorpAccountBalance:
                    {
                        XmlNode nodeCorpWalletData = nodeRoot.SelectSingleNode("descendant::rowset[@name='accounts']");
                        DataTable dtCorpWalletData = DataClass.ParseRowset(nodeCorpWalletData);

                        int totalRows = dtCorpWalletData.Rows.Count;
                        foreach (DataRow row in dtCorpWalletData.Rows)
                        {
                            DataRow newRow;// = mData.TableCorpAccountBalance.NewRow();

                            DataRow[] exists_rows = mData.TableCorpAccountBalance.Select(String.Format("accountKey = {0}", row["accountKey"]));
                            if (exists_rows.Length == 1)
                                newRow = exists_rows[0];
                            else
                                newRow = mData.TableCorpAccountBalance.NewRow();

                            mHostWindow.ShowStatus(mData.TableCorpAccountBalance.Rows.Count, totalRows,
                                String.Format("{2}, Добавление: {0}/{1}...", mData.TableCorpAccountBalance.Rows.Count, totalRows, args.Function));

                            for (int i = 0; i < dtCorpWalletData.Columns.Count; i++)
                            {
                                string val = Convert.ToString(row[dtCorpWalletData.Columns[i].ColumnName]);
                                if (dtCorpWalletData.Columns[i].ColumnName == "balance")
                                    val = DataClass.ReplaceDecimalSeparator(val);
                                newRow["balance_updated"] = currentTime;
                                newRow[dtCorpWalletData.Columns[i].ColumnName] = val;
                            }
                            if (exists_rows.Length == 0)
                                mData.TableCorpAccountBalance.Rows.Add(newRow);
                        }
                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitCorpAccountBalance();

                        mHostWindow.ShowStatus(100, "Завершено");
                        result = args.Function;
                        break;
                    }
                #endregion
                #region ApiFunction.CharWalletJournal
                case ApiFunction.CharWalletJournal:
                    {
                        XmlNode nodeCharWalletJournal = nodeRoot.SelectSingleNode("descendant::rowset[@name='entries']");
                        DataTable dtCharWalletJournal = DataClass.ParseRowset(nodeCharWalletJournal);
                        int totalRows = dtCharWalletJournal.Rows.Count;
                        addingFinish = false;
                        string beforeRefId = "0";
                        int counter = 0;

                        DataRow[] dr = mData.TableCharAccountBalance.Select(String.Format("accountKey = {0}", mSession.AccountKey));
                        //dr[0]["balance"] = row["balance"];
                        dr[0]["journal_updated"] = currentTime;
                        mData.CommitCharAccountBalance();

                        foreach (DataRow row in dtCharWalletJournal.Rows)
                        {
                            beforeRefId = Convert.ToString(row["refId"]);
                            DataRow newRow = mData.TableCharWalletJournal.NewRow();
                            newRow["accountKey"] = accountKey;
                            for (int i = 0; i < dtCharWalletJournal.Columns.Count; i++)
                            {
                                string val = Convert.ToString(row[dtCharWalletJournal.Columns[i].ColumnName]);
                                if (dtCharWalletJournal.Columns[i].ColumnName == "balance" ||
                                    dtCharWalletJournal.Columns[i].ColumnName == "amount")
                                    val = DataClass.ReplaceDecimalSeparator(val);
                                newRow[dtCharWalletJournal.Columns[i].ColumnName] = val;
                            }
                            bool exists = mData.IsCharWalletJournalRecordExists(mSession.CharacterId, mSession.AccountKey, newRow);
                            if (!exists)
                            {
                                mHostWindow.ShowStatus(
                                    counter, totalRows,
                                    String.Format("{2}, Добавление: {0}/{1}...", counter, totalRows, args.Function));
                                mData.TableCharWalletJournal.Rows.Add(newRow);
                            }
                            else
                            {
                                mHostWindow.ShowStatus(
                                    counter, totalRows,
                                    String.Format("Пропуск: {0}/{1}...", counter, totalRows));
                                if (!continueOnExists)
                                {
                                    addingFinish = true;
                                    break;
                                }

                            }
                            counter++;
                            Thread.Sleep(10);
                        }
                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitCharWalletJournal();
                        //SetupDataGridView(dgvWalletJournal);
                        mHostWindow.ShowStatus(100, "Завершено");
                        if (!addingFinish && totalRows == 1000)
                        {
                            //если ошибки не было, можно запросить следующую страницу
                            //mSession.BeforeRefId = beforeRefId;
                            //mSession.GetFunction(ApiFunction.CharWalletJournal);
                            mSession.CommandQueue.Enqueue(new Command(ApiFunction.CharWalletJournal, mSession.AccountKey, beforeRefId));
                        }
                        result = args.Function;
                        break;
                    }
                #endregion
                #region ApiFunction.CorpWalletJournal
                case ApiFunction.CorpWalletJournal:
                    {
                        XmlNode nodeCorpWalletJournal = nodeRoot.SelectSingleNode("descendant::rowset[@name='entries']");
                        DataTable dtCorpWalletJournal = DataClass.ParseRowset(nodeCorpWalletJournal);
                        int totalRows = dtCorpWalletJournal.Rows.Count;
                        addingFinish = false;
                        string beforeRefId = "0";
                        int counter = 0;

                        DataRow[] dr = mData.TableCorpAccountBalance.Select(String.Format("accountKey = {0}", mSession.AccountKey));
                        //dr[0]["balance"] = row["balance"];
                        dr[0]["journal_updated"] = currentTime;
                        mData.CommitCorpAccountBalance();

                        foreach (DataRow row in dtCorpWalletJournal.Rows)
                        {
                            beforeRefId = Convert.ToString(row["refId"]);
                            DataRow newRow = mData.TableCorpWalletJournal.NewRow();
                            newRow["accountKey"] = accountKey;
                            for (int i = 0; i < dtCorpWalletJournal.Columns.Count; i++)
                            {
                                string val = Convert.ToString(row[dtCorpWalletJournal.Columns[i].ColumnName]);
                                if (dtCorpWalletJournal.Columns[i].ColumnName == "balance" ||
                                    dtCorpWalletJournal.Columns[i].ColumnName == "amount")
                                    val = DataClass.ReplaceDecimalSeparator(val);
                                newRow[dtCorpWalletJournal.Columns[i].ColumnName] = val;
                            }
                            bool exists = mData.IsCorpWalletJournalRecordExists(mSession.CharacterId, mSession.AccountKey, newRow);
                            if (!exists)
                            {
                                mHostWindow.ShowStatus(
                                    counter, totalRows,
                                    String.Format("{2}, Добавление: {0}/{1}...", counter, totalRows, args.Function));
                                mData.TableCorpWalletJournal.Rows.Add(newRow);
                            }
                            else
                            {
                                mHostWindow.ShowStatus(
                                    counter, totalRows,
                                    String.Format("Пропуск: {0}/{1}...", counter, totalRows));
                                if (!continueOnExists)
                                {
                                    addingFinish = true;
                                    break;
                                }
                            }
                            counter++;
                            Thread.Sleep(10);
                        }
                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitCorpWalletJournal();
                        //SetupDataGridView(dgvWalletJournal);
                        mHostWindow.ShowStatus(100, "Завершено");
                        if (!addingFinish && totalRows == 1000)
                        {
                            //если ошибки не было, можно запросить следующую страницу
                            //mSession.BeforeRefId = beforeRefId;
                            //mSession.GetFunction(ApiFunction.CorpWalletJournal);
                            mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletJournal, mSession.AccountKey, beforeRefId));
                        }
                        result = args.Function;
                        break;
                    }
                #endregion
                #region ApiFunction.CharWalletTransactions
                case ApiFunction.CharWalletTransactions:
                    {
                        XmlNode nodeCharWalletTransactions = nodeRoot.SelectSingleNode("descendant::rowset[@name='transactions']");
                        DataTable dtCharWalletTransactions = DataClass.ParseRowset(nodeCharWalletTransactions);
                        int totalRows = dtCharWalletTransactions.Rows.Count;
                        addingFinish = false;
                        string beforeTransId = "0";
                        int counter = 0;

                        DataRow[] dr = mData.TableCharAccountBalance.Select(String.Format("accountKey = {0}", mSession.AccountKey));
                        //dr[0]["balance"] = row["balance"];
                        dr[0]["trans_updated"] = currentTime;
                        mData.CommitCharAccountBalance();

                        foreach (DataRow row in dtCharWalletTransactions.Rows)
                        {
                            beforeTransId = Convert.ToString(row["transactionId"]);
                            DataRow newRow = mData.TableCharWalletTransactions.NewRow();
                            newRow["accountKey"] = accountKey;//нет такой колонки при обновлении данных с сервера, надо загружать таблицу хотя бы 1 раз
                            for (int i = 0; i < dtCharWalletTransactions.Columns.Count; i++)
                            {
                                if (mData.TableCharWalletTransactions.Columns.Contains(dtCharWalletTransactions.Columns[i].ColumnName))
                                {
                                    string val = Convert.ToString(row[dtCharWalletTransactions.Columns[i].ColumnName]);
                                    if (dtCharWalletTransactions.Columns[i].ColumnName == "price")
                                        val = DataClass.ReplaceDecimalSeparator(val);
                                    newRow[dtCharWalletTransactions.Columns[i].ColumnName] = val;
                                }
                            }
                            bool exists = mData.IsCharWalletTransactionsRecordExists(mSession.CharacterId, mSession.AccountKey, newRow);
                            if (!exists)
                            {
                                mHostWindow.ShowStatus(
                                    counter, totalRows,
                                    String.Format("{2}, Добавление: {0}/{1}...", counter, totalRows, args.Function));
                                mData.TableCharWalletTransactions.Rows.Add(newRow);
                            }
                            else
                            {
                                mHostWindow.ShowStatus(
                                    counter, totalRows,
                                    String.Format("Пропуск: {0}/{1}...", counter, totalRows));
                                if (!continueOnExists)
                                {
                                    addingFinish = true;
                                    break;
                                }

                            }
                            counter++;
                            Thread.Sleep(10);
                        }
                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitCharWalletTransactions();
                        //SetupDataGridView(dgvWalletTransactions);
                        mHostWindow.ShowStatus(100, "Завершено");
                        if (!addingFinish && totalRows == 1000)
                        {
                            //если ошибки не было, можно запросить следующую страницу
                            //mSession.BeforeTransId = beforeTransId;
                            //mSession.GetFunction(ApiFunction.CharWalletTransactions);
                            mSession.CommandQueue.Enqueue(new Command(ApiFunction.CharWalletTransactions, mSession.AccountKey, beforeTransId));
                        }
                        result = args.Function;
                        break;
                    }
                #endregion
                #region ApiFunction.CorpWalletTransactions
                case ApiFunction.CorpWalletTransactions:
                    {
                        XmlNode nodeCorpWalletTransactions = nodeRoot.SelectSingleNode("descendant::rowset[@name='transactions']");
                        DataTable dtCorpWalletTransactions = DataClass.ParseRowset(nodeCorpWalletTransactions);
                        int totalRows = dtCorpWalletTransactions.Rows.Count;
                        addingFinish = false;
                        string beforeTransId = "0";
                        int counter = 0;

                        DataRow[] dr = mData.TableCorpAccountBalance.Select(String.Format("accountKey = {0}", mSession.AccountKey));
                        //dr[0]["balance"] = row["balance"];
                        dr[0]["trans_updated"] = currentTime;
                        mData.CommitCorpAccountBalance();

                        foreach (DataRow row in dtCorpWalletTransactions.Rows)
                        {
                            beforeTransId = Convert.ToString(row["transactionId"]);
                            DataRow newRow = mData.TableCorpWalletTransactions.NewRow();
                            newRow["accountKey"] = accountKey;
                            for (int i = 0; i < dtCorpWalletTransactions.Columns.Count; i++)
                            {
                                string val = Convert.ToString(row[dtCorpWalletTransactions.Columns[i].ColumnName]);
                                if (dtCorpWalletTransactions.Columns[i].ColumnName == "price")
                                    val = DataClass.ReplaceDecimalSeparator(val);
                                newRow[dtCorpWalletTransactions.Columns[i].ColumnName] = val;
                            }
                            bool exists = mData.IsCorpWalletTransactionsRecordExists(mSession.CharacterId, mSession.AccountKey, newRow);
                            if (!exists)
                            {
                                mHostWindow.ShowStatus(
                                    counter, totalRows,
                                    String.Format("{2}, Добавление: {0}/{1}...", counter, totalRows, args.Function));
                                mData.TableCorpWalletTransactions.Rows.Add(newRow);
                            }
                            else
                            {
                                mHostWindow.ShowStatus(
                                    counter, totalRows,
                                    String.Format("Пропуск: {0}/{1}...", counter, totalRows));
                                if (!continueOnExists)
                                {
                                    addingFinish = true;
                                    break;
                                }

                            }
                            counter++;
                            Thread.Sleep(10);
                        }
                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitCorpWalletTransactions();
                        //SetupDataGridView(dgvWalletTransactions);
                        mHostWindow.ShowStatus(100, "Завершено");
                        if (!addingFinish && totalRows == 1000)
                        {
                            //если ошибки не было, можно запросить следующую страницу
                            //mSession.BeforeTransId = beforeTransId;
                            //mSession.GetFunction(ApiFunction.CorpWalletTransactions);
                            mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletTransactions, mSession.AccountKey, beforeTransId));
                        }
                        result = args.Function;
                        break;
                    }
                #endregion
            }
            if (result == ApiFunction.None)
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
                    #region ApiFunction.CharAccountBalance
                    case ApiFunction.CharAccountBalance:
                        {
                            dgvCharAccountBalance.DataSource = mData.DataSet;
                            dgvCharAccountBalance.DataMember = "aCharAccountBalance";
                            SetupDataGridView(dgvCharAccountBalance);
                            break;
                        }
                    #endregion
                    #region ApiFunction.CorpAccountBalance
                    case ApiFunction.CorpAccountBalance:
                        {
                            dgvCorpAccountBalance.DataSource = mData.DataSet;
                            dgvCorpAccountBalance.DataMember = "aCorpAccountBalance";
                            SetupDataGridView(dgvCorpAccountBalance);
                            break;
                        }
                    #endregion
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
            dgvCharAccountBalance.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
            dgvCorpAccountBalance.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
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
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CharAccountBalance, null));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpAccountBalance, null));

                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CharWalletJournal, 1000, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CharWalletTransactions, 1000, 0));

                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletJournal, 1000, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletTransactions, 1000, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletJournal, 1001, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletTransactions, 1001, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletJournal, 1002, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletTransactions, 1002, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletJournal, 1003, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletTransactions, 1003, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletJournal, 1004, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletTransactions, 1004, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletJournal, 1005, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletTransactions, 1005, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletJournal, 1006, 0));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpWalletTransactions, 1006, 0));

                mData.GetCharWalletJournal(mOptions.CharacterId, "1000", DateTime.Now, DateTime.Now.AddSeconds(-1));
                mData.GetCorpWalletJournal(mOptions.CharacterId, "1000", DateTime.Now, DateTime.Now.AddSeconds(-1));
                mData.GetCharWalletTransactions(mOptions.CharacterId, "1000", DateTime.Now, DateTime.Now.AddSeconds(-1));
                mData.GetCorpWalletTransactions(mOptions.CharacterId, "1000", DateTime.Now, DateTime.Now.AddSeconds(-1));

                mSession.ExecuteCommandFromQueue();
            }
        }

        private void SetupDataGridView(DataGridView view)
        {
            if (view == dgvCorpAccountBalance || view == dgvCharAccountBalance)
            {
                if (!view.Columns.Contains("divisionName"))
                {
                    DataGridViewComboBoxColumn dgvc = new DataGridViewComboBoxColumn();
                    dgvc.DataPropertyName = "accountKey";
                    dgvc.DataSource = mData.DataSet;
                    dgvc.DisplayMember = "aWalletDivisions.description";
                    dgvc.HeaderText = "divisionName";
                    dgvc.Name = "divisionName";
                    dgvc.ValueMember = "aWalletDivisions.accountKey";
                    dgvc.ReadOnly = true;
                    dgvc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    view.Columns.Add(dgvc);
                    //dgvc.DisplayIndex = view.Columns["accountKey"].DisplayIndex + 1;
                }
                if (!view.Columns.Contains("view_journal"))
                {
                    DataGridViewLinkColumn dgvlc = new DataGridViewLinkColumn();
                    dgvlc.Name = "view_journal";
                    dgvlc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvlc.DataPropertyName = "journal_updated";
                    dgvlc.DefaultCellStyle.Format = "Журнал (dd/MM/yyyy HH:mm)";
                    //dgvlc.UseColumnTextForLinkValue = true;
                    dgvlc.Text = "Журнал";
                    dgvlc.ValueType = typeof(string);
                    view.Columns.Add(dgvlc);
                }
                if (!view.Columns.Contains("view_trans"))
                {
                    DataGridViewLinkColumn dgvlc = new DataGridViewLinkColumn();
                    dgvlc.Name = "view_trans";
                    dgvlc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvlc.DataPropertyName = "trans_updated";
                    dgvlc.DefaultCellStyle.Format = "Транзакции (dd/MM/yyyy HH:mm)";
                    //dgvlc.UseColumnTextForLinkValue = true;
                    dgvlc.Text = "Транзакции";
                    dgvlc.ValueType = typeof(string);
                    view.Columns.Add(dgvlc);
                }
                view.Columns["accountId"].DisplayIndex = 0;
                view.Columns["accountId"].Visible = false;
                view.Columns["accountKey"].DisplayIndex = 1;
                view.Columns["divisionName"].DisplayIndex = 2;
                view.Columns["balance"].DisplayIndex = 3;
                view.Columns["balance"].DefaultCellStyle.Format = "#,##0.00";
                view.Columns["balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                view.Columns["balance_updated"].DisplayIndex = 4;
                view.Columns["view_journal"].DisplayIndex = 5;
                view.Columns["view_journal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                view.Columns["view_journal"].Width = 150;

                view.Columns["view_trans"].DisplayIndex = 6;
                view.Columns["view_trans"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                view.Columns["view_trans"].Width = 170;

                //view.Columns["update_journal"].DisplayIndex = 7;
                //view.Columns["update_journal"].Width = 200;
                //view.Columns["update_journal"].Visible = allowSeparateWalletUpdate;
                //view.Columns["update_journal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //view.Columns["update_trans"].DisplayIndex = 8;
                //view.Columns["update_trans"].Width = 230;
                //view.Columns["update_trans"].Visible = allowSeparateWalletUpdate;
                //view.Columns["update_trans"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                view.Columns["userId"].Visible = false;
                view.Columns["journal_updated"].Visible = false;
                view.Columns["trans_updated"].Visible = false;
            }
            if (view.Columns.Contains("recordId"))
            {
                view.Columns["recordId"].Visible = false;
            }

        }
        
        private void OnDataGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    DataGridView dgv = (DataGridView)sender;
                    int accountKey = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["accountKey"].Value);
                    //mSession.AccountKey = Convert.ToString(accountKey);
                    //mSession.BeforeRefId = "0";
                    //mSession.BeforeTransId = "0";
                    if (dgv == dgvCharAccountBalance)
                    {
                        if (dgv.Columns[e.ColumnIndex].Name == "view_journal")
                        {
                            string divisionName = Convert.ToString(dgvCharAccountBalance.CurrentRow.Cells["divisionName"].FormattedValue);
                            string title = String.Format("Просмотр журнала частного кошелька {0} [{1}]", divisionName, accountKey);
                            mMainControl.SwitchToInterfaceState(InterfaceState.WalletJournalAndTransactions, true,
                                title, accountKey, "char_view_journal");
                        }
                        else
                            if (dgv.Columns[e.ColumnIndex].Name == "view_trans")
                            {
                                string divisionName = Convert.ToString(dgvCharAccountBalance.CurrentRow.Cells["divisionName"].FormattedValue);
                                string title = String.Format("Просмотр транзакций  частного кошелька {0} [{1}]", divisionName, accountKey);
                                mMainControl.SwitchToInterfaceState(InterfaceState.WalletJournalAndTransactions, true,
                                    title, accountKey, "char_view_trans");
                            }
                    }
                    else
                        if (dgv == dgvCorpAccountBalance)
                        {
                            if (dgv.Columns[e.ColumnIndex].Name == "view_journal")
                            {
                                string divisionName = Convert.ToString(dgvCorpAccountBalance.CurrentRow.Cells["divisionName"].FormattedValue);
                                string title = String.Format("Просмотр журнала корпоративного кошелька {0} [{1}]", divisionName, accountKey);
                                mMainControl.SwitchToInterfaceState(InterfaceState.WalletJournalAndTransactions, true,
                                    title, accountKey, "corp_view_journal");
                            }
                            else
                                if (dgv.Columns[e.ColumnIndex].Name == "view_trans")
                                {
                                    string divisionName = Convert.ToString(dgvCorpAccountBalance.CurrentRow.Cells["divisionName"].FormattedValue);
                                    string title = String.Format("Просмотр транзакций корпоративного кошелька {0} [{1}]", divisionName, accountKey);
                                    mMainControl.SwitchToInterfaceState(InterfaceState.WalletJournalAndTransactions, true,
                                        title, accountKey, "corp_view_trans");
                                }
                        }
                }
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
    }
}
