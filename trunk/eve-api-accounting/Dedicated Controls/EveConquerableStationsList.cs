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
    public partial class EveConquerableStationsList : UserControl
    {
        public EveConquerableStationsList(IMainControl mainControl, IHostWindow hostWindow)
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

            mData.GetConquerableStationsList();
            SetupDataGridView(dgvEveConquerableStationsList);
            dgvEveConquerableStationsList.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
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
                case ApiFunction.EveConquerableStationList:
                    dgvEveConquerableStationsList.DataSource = null;
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
                #region ApiFunction.EveConquerableStationList
                case ApiFunction.EveConquerableStationList:
                    {
                        mHostWindow.ShowStatus(0, "Обновление списка станций...");
                        XmlNode nodeConquerableStationList = nodeRoot.SelectSingleNode("descendant::rowset[@name='outposts']");
                        DataTable dtConquerableStationList = DataClass.ParseRowset(nodeConquerableStationList);

                        mData.ClearConquerableStationsList();
                        //data.TableAlliances.Clear();
                        int totalRows = dtConquerableStationList.Rows.Count;
                        foreach (DataRow station in dtConquerableStationList.Rows)
                        {
                            DataRow newStation = mData.TableConquerableStationsList.NewRow();
                            mHostWindow.ShowStatus(
                                mData.TableConquerableStationsList.Rows.Count,
                                totalRows,
                                String.Format("{2}, Добавление: {0}/{1}...", mData.TableConquerableStationsList.Rows.Count, totalRows, args.Function));
                            for (int i = 0; i < dtConquerableStationList.Columns.Count; i++)
                            {
                                newStation[dtConquerableStationList.Columns[i].ColumnName] = station[dtConquerableStationList.Columns[i].ColumnName];
                            }
                            mData.TableConquerableStationsList.Rows.Add(newStation);
                            Thread.Sleep(10);
                        }
                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitConquerableStationsList();
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
                    #region ApiFunction.EveConquerableStationList
                    case ApiFunction.EveConquerableStationList:
                        {
                            SetupDataGridView(dgvEveConquerableStationsList);
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
            dgvEveConquerableStationsList.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
        }

        private void mSession_FunctionCompleted(object sender, FunctionCompeletedEventArgs e)
        {
            if (e.ErrorMessage == "")
            {
                RunFunctionAsync(e);
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == bUpdate)
            {
                mSession.GetFunctionAsync(ApiFunction.EveConquerableStationList);
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load(@"G:\Documents\Visual Studio 2005\Projects\eve api\Accounting_v1.1\XmlSamples\ConquerableStationsList.xml");
                //RunFunctionAsync(new FunctionCompeletedEventArgs(ApiFunction.EveConquerableStationList, xmlDoc, "")); ;
            }
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            if (dgv == dgvEveConquerableStationsList)
            {
                mData.GetConquerableStationsListForView();
                dgv.DataSource = mData.DataSet;
                dgv.DataMember = "aConquerableStationsListForView";
                mData.FormatDataGridView(dgv);
                dgv.Columns["solarSystemId"].Visible = false;
                dgv.Columns["stationTypeId"].Visible = false;
                dgv.Columns["corporationId"].Visible = false;
                dgv.Columns["stationId"].Visible = false;
            }
        }
    }
}
