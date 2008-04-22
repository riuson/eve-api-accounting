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
    public partial class EveRefTypes : UserControl
    {
        public EveRefTypes(IMainControl mainControl, IHostWindow hostWindow)
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

            mData.GetRefTypesList();
            dgvEveRefTypes.DataSource = mData.DataSet;
            dgvEveRefTypes.DataMember = "aRefTypes";
            SetupDataGridView(dgvEveRefTypes);

            dgvEveRefTypes.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
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
                case ApiFunction.EveRefTypes:
                    dgvEveRefTypes.DataSource = null;
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
                #region ApiFunction.EveRefTypes
                case ApiFunction.EveRefTypes:
                    {
                        mHostWindow.ShowStatus(0, "Обновление...");
                        XmlNode nodeRefTypes = nodeRoot.SelectSingleNode("descendant::rowset[@name='refTypes']");
                        DataTable dtRefTypes = DataClass.ParseRowset(nodeRefTypes);
                        
                        if (dtRefTypes.Rows.Count > 0)
                        {
                            mData.ClearRefTypesList();
                            //data.TableRefTypes.Clear();
                            int totalRows = dtRefTypes.Rows.Count;
                            foreach (DataRow refType in dtRefTypes.Rows)
                            {
                                DataRow newRefType = mData.TableRefTypes.NewRow();
                                mHostWindow.ShowStatus(
                                    mData.TableRefTypes.Rows.Count, totalRows,
                                    String.Format("{2}, Добавление: {0}/{1}...", mData.TableRefTypes.Rows.Count, totalRows, args.Function));
                                for (int i = 0; i < dtRefTypes.Columns.Count; i++)
                                {
                                    newRefType[dtRefTypes.Columns[i].ColumnName] = refType[dtRefTypes.Columns[i].ColumnName];
                                }
                                mData.TableRefTypes.Rows.Add(newRefType);
                            }
                        }
                        mData.CommitRefTypes();
                        mData.GetRefTypesList();

                        mHostWindow.ShowStatus("Завершено");
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
                    #region ApiFunction.EveRefTypes
                    case ApiFunction.EveRefTypes:
                        {
                            dgvEveRefTypes.DataSource = mData.DataSet;
                            dgvEveRefTypes.DataMember = "aRefTypes";
                            SetupDataGridView(dgvEveRefTypes);
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
            mData.CommitRefTypes();
            dgvEveRefTypes.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
        }

        private void mSession_FunctionCompleted(object sender, FunctionCompeletedEventArgs e)
        {
            RunFunctionAsync(e);
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == bUpdate)
            {
                //RunFunctionAsync(ApiFunction.EveRefTypes);
                mSession.GetFunctionAsync(ApiFunction.EveRefTypes);
            }
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            if (dgv == dgvEveRefTypes)
            {
                dgv.Columns["refTypeID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //dgv.Columns["refTypeID"].ReadOnly = true;
                dgv.Columns["refTypeName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dgv.Columns["refTypeName"].ReadOnly = true;
            }
        }
    }
}
