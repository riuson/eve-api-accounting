using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Xml;

namespace Accounting
{
    public partial class WalletJournalTransactions : UserControl
    {
        public WalletJournalTransactions(IMainControl mainControl, IHostWindow hostWindow, object[] data)
        {
            InitializeComponent();
            //data: title, accountKey, "char_view_journal"
            mMainControl = mainControl;
            mOptions = mainControl.Options;
            mData = mainControl.Data;
            mSession = mainControl.Session;
            mSession.FunctionCompleted += new FunctionCompletedHandler(mSession_FunctionCompleted);
            mHostWindow = hostWindow;

            timerAsyncDebugRun.Interval = 500;
            timerAsyncDebugRun.Tick += new EventHandler(timerAsyncDebugRun_Tick);

            dtpWalletFilterTop.Value = DateTime.UtcNow;
            dtpWalletFilterLow.Value = DateTime.UtcNow.AddDays(-1 * mOptions.DefaultDaysInterval);

            mAccountKey = Convert.ToString(data[1]);
            mMode = Convert.ToString(data[2]);

            mData.GetRefTypesList();

            dtpWalletFilterTop.Value = DateTime.UtcNow.AddMinutes(2);
            dtpWalletFilterLow.Value = DateTime.UtcNow.AddDays(-1 * mOptions.DefaultDaysInterval);

            dgvWalletJournal.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
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
        private string mAccountKey;
        private string mMode;
        private DateTime mdtHigh;
        private DateTime mdtLow;
        #endregion
        #endregion

        void RunFunctionAsync(FunctionCompeletedEventArgs args)
        {
            //подготовка, если требуется
            //switch (args.Function)
            //{
            //    case ApiFunction.None:
            //        break;
            //}
            dgvWalletJournal.DataSource = null;
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
            //XmlNode nodeRoot = args.XmlResponse.DocumentElement;
            //XmlNode nodeCurrentTime = nodeRoot.SelectSingleNode("descendant::currentTime");
            //DateTime currentTime = DateTime.Parse(nodeCurrentTime.InnerText);
            //bool continueParsing = mOptions.ContinueParsing;
            //int accountKey = Convert.ToInt32(mSession.AccountKey);
            //switch (args.Function)
            //{
            //    #region ApiFunction.None
            //    case ApiFunction.None:
            //        {
            //            mHostWindow.ShowStatus(0, "base...");
            //            if (bres)
            //                result = args.Function;
            //            else
            //                result = ApiFunction.None;
            //            break;
            //        }
            //    #endregion
            //}
            //if (result == ApiFunction.None)
            //    mHostWindow.ShowStatus("Произошла ошибка");
            //else
            //    mHostWindow.ShowStatus("Завершено");
            mHostWindow.ShowStatus(50, "Загрузка данных из базы, пожалуйста подождите...");
            LoadData();
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
                    #region ApiFunction.None
                    case ApiFunction.None:
                        {
                            ShowData();
                            break;
                        }
                    #endregion
                }
            }
        }
        void timerAsyncDebugRun_Tick(object sender, EventArgs e)
        {
            timerAsyncDebugRun.Stop();
            FunctionCompeletedEventArgs args = (FunctionCompeletedEventArgs)timerAsyncDebugRun.Tag;
            FunctionAsync(args);
            FunctionCompleted(ApiFunction.None);
        }

        delegate ApiFunction FunctionAsyncDelegate(FunctionCompeletedEventArgs args);

        private void BeforeDestroy()
        {
            mSession.FunctionCompleted -= new FunctionCompletedHandler(mSession_FunctionCompleted);
            dgvWalletJournal.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
        }

        private void mSession_FunctionCompleted(object sender, FunctionCompeletedEventArgs e)
        {
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == bWalletFilterApply)
            {
                mdtHigh = dtpWalletFilterTop.Value;
                mdtLow = dtpWalletFilterLow.Value;
                RunFunctionAsync(null);
            }
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            if (mMode.Contains("view_journal"))
            {
                //if (!dgvWalletJournal.Columns.Contains("refTypeName"))
                //{
                //    DataGridViewComboBoxColumn dgvc = new DataGridViewComboBoxColumn();
                //    dgvc.DataPropertyName = "refTypeId";
                //    dgvc.DataSource = mData.DataSet;
                //    dgvc.DisplayMember = "aRefTypes.refTypeName";
                //    dgvc.HeaderText = "refTypeName";
                //    dgvc.Name = "refTypeName";
                //    dgvc.ValueMember = "aRefTypes.refTypeId";
                //    dgvc.ReadOnly = true;
                //    dgvc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                //    dgvWalletJournal.Columns.Add(dgvc);
                //    //dgvc.DisplayIndex = view.Columns["refTypeId"].DisplayIndex + 1;
                //}
                //dgvWalletJournal.Columns["refTypeID"].Visible = false;//вылетает тут после обновления данных с сервера, почему-то отсутствует колонка...
                //dgvWalletJournal.Columns["recordId"].Visible = false;
                //dgvWalletJournal.Columns["ownerID1"].Visible = false;
                //dgvWalletJournal.Columns["ownerID2"].Visible = false;
                //dgvWalletJournal.Columns["argID1"].Visible = false;
                //dgvWalletJournal.Columns["userId"].Visible = false;
                //dgvWalletJournal.Columns["accountKey"].Visible = false;
                dgvWalletJournal.Columns["amount"].DefaultCellStyle.Format = "#,##0.00";
                dgvWalletJournal.Columns["amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvWalletJournal.Columns["balance"].DefaultCellStyle.Format = "#,##0.00";
                dgvWalletJournal.Columns["balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mData.FormatDataGridView(dgvWalletJournal);
                //dgvWalletJournal.Columns["refTypeName"].DisplayIndex = 4;
                dgvWalletJournal.Sort(dgvWalletJournal.Columns["date"], ListSortDirection.Descending);
            }
            if (mMode.Contains("view_trans"))
            {
                dgvWalletJournal.Columns["price"].DefaultCellStyle.Format = "#,##0.00";
                dgvWalletJournal.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvWalletJournal.Columns["typeId"].Visible = false;
                //dgvWalletJournal.Columns["userId"].Visible = false;
                //dgvWalletJournal.Columns["recordId"].Visible = false;
                //dgvWalletJournal.Columns["accountKey"].Visible = false;
                //dgvWalletJournal.Columns["clientId"].Visible = false;
                //dgvWalletJournal.Columns["userId"].Visible = false;
                //dgvWalletJournal.Columns["stationId"].Visible = false;
                dgvWalletJournal.Columns["stationName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //dgvWalletJournal.Columns["price_total"].DisplayIndex = dgvWalletJournal.Columns["price"].DisplayIndex + 1;
                dgvWalletJournal.Columns["price_total"].DefaultCellStyle.Format = "#,##0.00";
                dgvWalletJournal.Columns["price_total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mData.FormatDataGridView(dgvWalletJournal);
                dgvWalletJournal.Sort(dgvWalletJournal.Columns["transactionDateTime"], ListSortDirection.Descending);
            }
        }

        private void LoadData()
        {
            if (mMode == "char_view_journal")
                mData.GetCharWalletJournalForView(mOptions.CharacterId, mAccountKey, mdtHigh, mdtLow);
            if (mMode == "char_view_trans")
                mData.GetCharWalletTransactionsForView(mOptions.CharacterId, mAccountKey, mdtHigh, mdtLow);
            if (mMode == "corp_view_journal")
                mData.GetCorpWalletJournalForView(mOptions.CharacterId, mAccountKey, mdtHigh, mdtLow);
            if (mMode == "corp_view_trans")
                mData.GetCorpWalletTransactionsForView(mOptions.CharacterId, mAccountKey, mdtHigh, mdtLow);
        }
        private void ShowData()
        {
            if (mMode == "char_view_journal")
                dgvWalletJournal.DataSource = mData.TableCharWalletJournalForView;
            if (mMode == "char_view_trans")
                dgvWalletJournal.DataSource = mData.TableCharWalletTransactionsForView;
            if (mMode == "corp_view_journal")
                dgvWalletJournal.DataSource = mData.TableCorpWalletJournalForView;
            if (mMode == "corp_view_trans")
                dgvWalletJournal.DataSource = mData.TableCorpWalletTransactionsForView;
            mData.FormatDataGridView(dgvWalletJournal);
            SetupDataGridView(dgvWalletJournal);
        }
    }
}
