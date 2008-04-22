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
    public partial class ControlTowerFuelPrices : UserControl
    {
        public ControlTowerFuelPrices(IMainControl mainControl, IHostWindow hostWindow)
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

            mData.GetControlTowerFuelPrices(mOptions.CharacterId);
            dgvFuelPrices.DataSource = mData.TableControlTowerFuelPrices;
            SetupDataGridView(dgvFuelPrices);
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
                case ApiFunction.None:
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
            bool bres = false;
            ApiFunction result = ApiFunction.None;
            XmlNode nodeRoot = args.XmlResponse.DocumentElement;
            XmlNode nodeCurrentTime = nodeRoot.SelectSingleNode("descendant::currentTime");
            DateTime currentTime = DateTime.Parse(nodeCurrentTime.InnerText);
            bool continueParsing = mOptions.ContinueParsing;
            int accountKey = Convert.ToInt32(mSession.AccountKey);
            switch (args.Function)
            {
                #region ApiFunction.None
                case ApiFunction.None:
                    {
                        mHostWindow.ShowStatus(0, "base...");
                        if (bres)
                            result = args.Function;
                        else
                            result = ApiFunction.None;
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
                    #region ApiFunction.None
                    case ApiFunction.None:
                        {
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
            mData.CommitControlTowerFuelPrices();
            mSession.FunctionCompleted -= new FunctionCompletedHandler(mSession_FunctionCompleted);
        }

        private void mSession_FunctionCompleted(object sender, FunctionCompeletedEventArgs e)
        {
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            //if (sender == button)
            //{
            //    RunFunctionAsync(FunctionType.Error);
            //}
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            if (dgv == dgvFuelPrices)
            {
                dgv.Columns["typeId"].Visible = false;
                dgv.Columns["recordId"].Visible = false;
                dgv.Columns["userId"].Visible = false;
                
                dgv.Columns["typeName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns["typeName"].ReadOnly = true;
                dgv.Columns["typeName"].HeaderText = "Вид топлива";
                
                dgv.Columns["price"].DefaultCellStyle.Format = "#,##0.00";
                dgv.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns["price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns["price"].HeaderText = "Цена";
            }
        }
    }
}
