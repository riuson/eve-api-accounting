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
    public partial class EveAllianceList : UserControl
    {
        public EveAllianceList(IMainControl mainControl, IHostWindow hostWindow)
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

            mData.GetAlliancesList();
            dgvAllianceList.DataSource = mData.TableAllianceList;

            dgvAllianceList.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
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
                case ApiFunction.EveAllianceList:
                    dgvAllianceList.DataSource = null;
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
            ApiFunction result = ApiFunction.EveAllianceList;
            XmlNode nodeRoot = args.XmlResponse.DocumentElement;
            XmlNode nodeCurrentTime = nodeRoot.SelectSingleNode("descendant::currentTime");
            DateTime currentTime = DateTime.Parse(nodeCurrentTime.InnerText);
            bool continueParsing = mOptions.ContinueParsing;
            int accountKey = Convert.ToInt32(mSession.AccountKey);
            switch (result)
            {
                #region ApiFunction.EveAllianceList
                case ApiFunction.EveAllianceList:
                    {
                        mHostWindow.ShowStatus(0, "Обновление списка альянсов...");
                        XmlNode nodeAllianceList = nodeRoot.SelectSingleNode("descendant::rowset[@name='alliances']");
                        DataTable dtAllianceList = DataClass.ParseRowset(nodeAllianceList);

                        mData.ClearAllianceList();
                        //data.TableAlliances.Clear();
                        int totalRows = dtAllianceList.Rows.Count;
                        foreach (DataRow alliance in dtAllianceList.Rows)
                        {
                            DataRow newAlliance = mData.TableAllianceList.NewRow();
                            mHostWindow.ShowStatus(
                                mData.TableAllianceList.Rows.Count,
                                totalRows,
                                String.Format("{2}, Добавление: {0}/{1}...", mData.TableAllianceList.Rows.Count, totalRows, args.Function));
                            for (int i = 0; i < dtAllianceList.Columns.Count; i++)
                            {
                                newAlliance[dtAllianceList.Columns[i].ColumnName] = alliance[dtAllianceList.Columns[i].ColumnName];
                            }
                            mData.TableAllianceList.Rows.Add(newAlliance);
                            Thread.Sleep(10);
                        }
                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitAllianceList();
                        //SetupDataGridView(dgvAlliances);
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
                    #region ApiFunction.EveAllianceList
                    case ApiFunction.EveAllianceList:
                        {
                            dgvAllianceList.DataSource = mData.TableAllianceList;
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
            dgvAllianceList.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
        }

        private void mSession_FunctionCompleted(object sender, FunctionCompeletedEventArgs e)
        {
            if (e.ErrorMessage == "" && e.Function == ApiFunction.EveAllianceList)
            {
                RunFunctionAsync(e);
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == bUpdate)
            {
                mSession.GetFunctionAsync(ApiFunction.EveAllianceList);
            }
        }
    }
}
