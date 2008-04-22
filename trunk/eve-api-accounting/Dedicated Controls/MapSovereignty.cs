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
    public partial class MapSovereignty : UserControl
    {
        public MapSovereignty(IMainControl mainControl, IHostWindow hostWindow)
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

            mData.GetSovereigntyListForView();
            dgvMapSovereignty.DataSource = mData.TableSovereigntyListForView;
            SetupDataGridView(dgvMapSovereignty);

            dgvMapSovereignty.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
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
        //ArchiveCheck mCalArchive = new ArchiveCheck();
        #endregion
        #endregion

        void RunFunctionAsync(FunctionCompeletedEventArgs args)
        {
            //подготовка, если требуется
            switch (args.Function)
            {
                case ApiFunction.MapSovereignty:
                    dgvMapSovereignty.DataSource = null;
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
                #region ApiFunction.MapSovereignty
                case ApiFunction.MapSovereignty:
                    {
                        mHostWindow.ShowStatus(0, "Обновление карты систем...");
                        XmlNode nodeSovereignty = nodeRoot.SelectSingleNode("descendant::rowset[@name='solarSystems']");
                        DataTable dtSovereignty = DataClass.ParseRowset(nodeSovereignty);
                        
                        mData.ClearSovereigntyList();
                        //data.TableAlliances.Clear();
                        int totalRows = dtSovereignty.Rows.Count;
                        foreach (DataRow sovereignty in dtSovereignty.Rows)
                        {
                            DataRow newSovereignty = mData.TableSovereignty.NewRow();
                            mHostWindow.ShowStatus(
                                mData.TableSovereignty.Rows.Count, totalRows,
                                String.Format("{2}, Добавление: {0}/{1}...", mData.TableSovereignty.Rows.Count, totalRows, args.Function));
                            for (int i = 0; i < dtSovereignty.Columns.Count; i++)
                            {
                                newSovereignty[dtSovereignty.Columns[i].ColumnName] = sovereignty[dtSovereignty.Columns[i].ColumnName];
                            }
                            mData.TableSovereignty.Rows.Add(newSovereignty);
                            Thread.Sleep(10);
                        }
                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitSovereigntyList();
                        mData.GetSovereigntyListForView();
                        mHostWindow.ShowStatus(100, "Завершено");
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
                    #region ApiFunction.MapSovereignty
                    case ApiFunction.MapSovereignty:
                        {
                            dgvMapSovereignty.DataSource = mData.TableSovereigntyListForView;
                            SetupDataGridView(dgvMapSovereignty);
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
            FunctionCompleted(args.Function);
        }

        delegate ApiFunction FunctionAsyncDelegate(FunctionCompeletedEventArgs args);

        private void BeforeDestroy()
        {
            mSession.FunctionCompleted -= new FunctionCompletedHandler(mSession_FunctionCompleted);
            dgvMapSovereignty.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
        }

        private void mSession_FunctionCompleted(object sender, FunctionCompeletedEventArgs e)
        {
            if (e.ErrorMessage == "")
                RunFunctionAsync(e);
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == bUpdate)
            {
                mSession.GetFunctionAsync(ApiFunction.MapSovereignty);
            }
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            if (dgv == dgvMapSovereignty)
            {
                mData.FormatDataGridView(dgv);
            }
        }

        private void dgvMapSovereignty_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (sender == dgvMapSovereignty)
            {
                e.ThrowException = false;
                if (e.Exception is System.ArgumentException)
                {
                    if (Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) != 0)
                        dgv[e.ColumnIndex, e.RowIndex].ErrorText = "Альянс отсутствует в базе данных";
                }
            }
        }
    }
}
