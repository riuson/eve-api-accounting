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
    public partial class ControlIndustryJobs : UserControl
    {
        public ControlIndustryJobs(IMainControl mainControl, IHostWindow hostWindow)
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

            mData.GetCharIndustryJobs(mOptions.CharacterId);
            mData.GetCorpIndustryJobs(mOptions.CharacterId);
            mShowForCorp = false;
            ConnectDataSource(true);

            dgvIndustryJobs.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
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
        bool mShowForCorp;
        #endregion
        #endregion

        void RunFunctionAsync(FunctionCompeletedEventArgs args)
        {
            //подготовка, если требуется
            switch (args.Function)
            {
                case ApiFunction.CharIndustryJobs:
                case ApiFunction.CorpIndustryJobs:
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
                #region ApiFunction.CharIndustryJobs
                case ApiFunction.CharIndustryJobs:
                    {
                        mHostWindow.ShowStatus(0, "Обновление...");
                        XmlNode nodeIndustryJobs = nodeRoot.SelectSingleNode("descendant::rowset[@name='jobs']");
                        DataTable dtIndustryJobs = DataClass.ParseRowset(nodeIndustryJobs);

                        if (dtIndustryJobs.Rows.Count > 0)
                        {
                            foreach (DataRow row in mData.TableCharIndustryJobs.Rows)
                            {
                                row.Delete();
                            }
                            mData.CommitCharIndustryJobs();

                            int totalRows = dtIndustryJobs.Rows.Count;
                            foreach (DataRow job in dtIndustryJobs.Rows)
                            {
                                DataRow newJob = mData.TableCharIndustryJobs.NewRow();
                                mHostWindow.ShowStatus(
                                    mData.TableCharIndustryJobs.Rows.Count, totalRows,
                                    String.Format("{2}, Добавление: {0}/{1}...", mData.TableCharIndustryJobs.Rows.Count, totalRows, args.Function));
                                for (int i = 0; i < dtIndustryJobs.Columns.Count; i++)
                                {
                                    string val = Convert.ToString(job[dtIndustryJobs.Columns[i].ColumnName]);
                                    if (mData.TableCharIndustryJobs.Columns[dtIndustryJobs.Columns[i].ColumnName].DataType == typeof(Boolean))
                                    {
                                        if (val == "0") val = "false";
                                        else val = "true";
                                    }
                                    if (mData.TableCharIndustryJobs.Columns[dtIndustryJobs.Columns[i].ColumnName].DataType == typeof(Double))
                                    {
                                        val = DataClass.ReplaceDecimalSeparator(val);
                                    }
                                    newJob[dtIndustryJobs.Columns[i].ColumnName] = val;
                                }
                                mData.TableCharIndustryJobs.Rows.Add(newJob);
                            }
                        }
                        mData.CommitCharIndustryJobs();
                        mData.GetCharIndustryJobs(mOptions.CharacterId);

                        mHostWindow.ShowStatus("Завершено");
                        result = args.Function;
                        break;
                    }
                #endregion
                #region ApiFunction.CorpIndustryJobs
                case ApiFunction.CorpIndustryJobs:
                    {
                        mHostWindow.ShowStatus(0, "Обновление...");
                        XmlNode nodeIndustryJobs = nodeRoot.SelectSingleNode("descendant::rowset[@name='jobs']");
                        DataTable dtIndustryJobs = DataClass.ParseRowset(nodeIndustryJobs);

                        if (dtIndustryJobs.Rows.Count > 0)
                        {
                            foreach (DataRow row in mData.TableCorpIndustryJobs.Rows)
                            {
                                row.Delete();
                            }
                            mData.CommitCorpIndustryJobs();

                            int totalRows = dtIndustryJobs.Rows.Count;
                            foreach (DataRow job in dtIndustryJobs.Rows)
                            {
                                DataRow newJob = mData.TableCorpIndustryJobs.NewRow();
                                mHostWindow.ShowStatus(
                                    mData.TableCorpIndustryJobs.Rows.Count, totalRows,
                                    String.Format("{2}, Добавление: {0}/{1}...", mData.TableCorpIndustryJobs.Rows.Count, totalRows, args.Function));
                                for (int i = 0; i < dtIndustryJobs.Columns.Count; i++)
                                {
                                    string val = Convert.ToString(job[dtIndustryJobs.Columns[i].ColumnName]);
                                    if (mData.TableCorpIndustryJobs.Columns[dtIndustryJobs.Columns[i].ColumnName].DataType == typeof(Boolean))
                                    {
                                        if (val == "0") val = "false";
                                        else val = "true";
                                    }
                                    if (mData.TableCorpIndustryJobs.Columns[dtIndustryJobs.Columns[i].ColumnName].DataType == typeof(Double))
                                    {
                                        val = DataClass.ReplaceDecimalSeparator(val);
                                    }
                                    newJob[dtIndustryJobs.Columns[i].ColumnName] = val;
                                }
                                mData.TableCorpIndustryJobs.Rows.Add(newJob);
                            }
                        }
                        mData.CommitCorpIndustryJobs();
                        mData.GetCorpIndustryJobs(mOptions.CharacterId);

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
                    #region ApiFunction.CharIndustryJobs
                    case ApiFunction.CharIndustryJobs:
                        {
                            rbCharIndustryJobs.Checked = true;
                            ConnectDataSource(true);
                            break;
                        }
                    #endregion
                    #region ApiFunction.CorpIndustryJobs
                    case ApiFunction.CorpIndustryJobs:
                        {
                            rbCorpIndustryJobs.Checked = true;
                            ConnectDataSource(true);
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
            dgvIndustryJobs.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
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
                XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load(@"G:\Documents\Visual Studio 2005\Projects\eve api\Accounting_v1.1\XmlSamples\CorpIndustryJobs.xml");
                //RunFunctionAsync(new FunctionCompeletedEventArgs(ApiFunction.CharIndustryJobs, xmlDoc, ""));
                //xmlDoc.Load(@"G:\Documents\Visual Studio 2005\Projects\eve api\Accounting_v1.1\XmlSamples\CorpIndustryJobs.xml");
                //RunFunctionAsync(new FunctionCompeletedEventArgs(ApiFunction.CorpIndustryJobs, xmlDoc, ""));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CharIndustryJobs, mOptions.UserId, mOptions.ApiKey, mOptions.CharacterId));
                mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpIndustryJobs, mOptions.UserId, mOptions.ApiKey, mOptions.CharacterId));
                mSession.ExecuteCommandFromQueue();
            }
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            if (dgv == dgvIndustryJobs)
            {
            }
        }

        private void rbIndustryJobsClick(object sender, EventArgs e)
        {
            if (rbCorpIndustryJobs.Checked != mShowForCorp)
            {
                ConnectDataSource(false);
                mShowForCorp = rbCorpIndustryJobs.Checked;
                if (!mShowForCorp)
                    mData.GetCharIndustryJobs(mOptions.CharacterId);
                else
                    mData.GetCorpIndustryJobs(mOptions.CharacterId);
                ConnectDataSource(true);
            }
        }
        private void ConnectDataSource(bool connect)
        {
            if (!connect)
            {
                dgvIndustryJobs.DataSource = false;
            }
            else
            {
                //dgvCharIndustryJobs.AutoGenerateColumns = false;
                if (!mShowForCorp)
                    dgvIndustryJobs.DataSource = mData.TableCharIndustryJobs;
                else
                    dgvIndustryJobs.DataSource = mData.TableCorpIndustryJobs;
                //mData.FormatDataGridView(dgvKillLog);
                SetupDataGridView(dgvIndustryJobs);

                //dgvCharIndustryJobs.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //dgvCharIndustryJobs.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //dgvCharIndustryJobs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                //dgvCharIndustryJobs.RowTemplate.Height = 64;

                //StartDownloadImages();
            }
        }
    }
}
