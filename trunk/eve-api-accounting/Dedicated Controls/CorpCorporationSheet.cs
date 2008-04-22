using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace Accounting
{
    public partial class CorpCorporationSheet : UserControl
    {
        public CorpCorporationSheet(IMainControl mainControl, IHostWindow hostWindow)
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
            ShowInfo();
            dgvWalletDivisions.DataSource = mData.TableWalletDivisions;
            dgvDivisions.DataSource = mData.TableDivisions;

            dgvDivisions.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
            dgvWalletDivisions.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
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
            ApiFunction result = args.Function;
            switch (result)
            {
                #region ApiFunction.CorpCorporationSheet
                case ApiFunction.CorpCorporationSheet:
                    {
                        mHostWindow.ShowStatus(0, "Обновление информации о корпорации...");
                        string str = args.XmlResponse.InnerXml;
                        mData.SaveCorporationSheet(mOptions.CharacterId, str);
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
                    #region ApiFunction.CorpCorporationSheet
                    case ApiFunction.CorpCorporationSheet:
                        {
                            mData.GetCorporationSheet(mOptions.CharacterId);
                            ShowInfo();
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
            dgvDivisions.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
            dgvWalletDivisions.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
        }

        private void mSession_FunctionCompleted(object sender, FunctionCompeletedEventArgs e)
        {
            if (e.ErrorMessage == "" && e.Function == ApiFunction.CorpCorporationSheet)
            {
                RunFunctionAsync(e);
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == bUpdate)
            {
                mSession.GetFunctionAsync(ApiFunction.CorpCorporationSheet);
            }
        }

        private void ShowInfo()
        {
            lCorporationInfo.Text = String.Format(
                "Корпорация: {0} [{1}]\nЦео: {2}\nАльянс: {3}\nОписание: {4}\nСтанция: {5}\nURL: {6}\n" +
                "TaxRate: {7}%\nShares: {8}\nMember Count: {9}\nMember Limit: {10}",
                mData.CorporationSheetInfo.CorporationName,
                mData.CorporationSheetInfo.Ticker,
                mData.CorporationSheetInfo.CeoName,
                mData.CorporationSheetInfo.AllianceName,
                mData.CorporationSheetInfo.Description,
                mData.CorporationSheetInfo.StationName,
                mData.CorporationSheetInfo.Url,
                mData.CorporationSheetInfo.TaxRate,
                mData.CorporationSheetInfo.Shares,
                mData.CorporationSheetInfo.MemberCount,
                mData.CorporationSheetInfo.MemberLimit);
        }
    }
}
