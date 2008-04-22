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
    public partial class ControlStatistics : UserControl
    {
        public ControlStatistics(IMainControl mainControl, IHostWindow hostWindow)
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

            HideStatsPages();

            cbWalletStatisticRefTypeId.DataSource = null;
            mData.GetRefTypesList();
            cbWalletStatisticRefTypeId.DataSource = mData.TableRefTypes;
            cbWalletStatisticRefTypeId.ValueMember = "refTypeId";
            cbWalletStatisticRefTypeId.DisplayMember = "refTypeName";
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
        private List<Control> mWithdrawalsButtons = new List<Control>();
        #endregion
        #endregion

        void RunFunctionAsync(string value)
        {
            //подготовка, если требуется
            switch (value)
            {
                case "getstat":
                    break;
            }
#if DEBUG
            timerAsyncDebugRun.Tag = value;
            timerAsyncDebugRun.Start();
#else
            FunctionAsyncDelegate f = this.FunctionAsync;
            f.BeginInvoke(value, new AsyncCallback(FunctionCompletedCallBack), this);
#endif
        }

        string FunctionAsync(string value)
        {
            switch (value)
            {
                case "getstat":
                    {
                        mHostWindow.ShowStatus(20, "Расчёт...");
                        mData.GetWithdrawalsBetweenAccounts(mOptions.CharacterId, dtpStatisticFilterTop.Value, dtpStatisticFilterLow.Value);
                        mHostWindow.ShowStatus(100, "Завершено...");
                        break;
                    }
            }
            return value;
        }
        void FunctionCompletedCallBack(IAsyncResult result)
        {
            try
            {
                AsyncResult r = (AsyncResult)result;
                FunctionAsyncDelegate command = (FunctionAsyncDelegate)r.AsyncDelegate;
                string value = command.EndInvoke(result);
                FunctionCompleted(value);
            }
            catch (Exception exc)
            {
                mMainControl.ProcessException(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
        void FunctionCompleted(string value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((EventHandler)delegate
                {
                    FunctionCompleted(value);
                });
            }
            else
            {
                switch (value)
                {
                    case "getstat":
                        {
                            if (mWithdrawalsButtons.Count == 0)
                            {
                                mWithdrawalsButtons.Add(bWD1000);
                                mWithdrawalsButtons.Add(bWD1001);
                                mWithdrawalsButtons.Add(bWD1002);
                                mWithdrawalsButtons.Add(bWD1003);
                                mWithdrawalsButtons.Add(bWD1004);
                                mWithdrawalsButtons.Add(bWD1005);
                                mWithdrawalsButtons.Add(bWD1006);
                            }
                            break;
                        }
                }
            }
        }
        void timerAsyncDebugRun_Tick(object sender, EventArgs e)
        {
            timerAsyncDebugRun.Stop();
            FunctionCompeletedEventArgs args = (FunctionCompeletedEventArgs)timerAsyncDebugRun.Tag;
            FunctionAsync("");
            FunctionCompleted("");
        }

        delegate string FunctionAsyncDelegate(string value);

        private void BeforeDestroy()
        {
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
            if (dgv == dgvBountyByMember)
            {
                dgv.Columns["sumAmount"].DefaultCellStyle.Format = "#,##0.00";
                dgv.Columns["sumAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns["sumAmount"].DisplayIndex = 1;
                dgv.Columns["ownerName2"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns["ownerName2"].DisplayIndex = 0;
                dgv.Columns["sumAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            if (dgv == dgvCorpWalletsInOutWithWithdrawals || dgv == dgvCorpWalletsInOutWithoutWithdrawals)
            {
                //dgv.Columns["accountKey"].DefaultCellStyle.Format = "#,##0.00";
                dgv.Columns["accountKey"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["divisionName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["moneyIn"].DefaultCellStyle.Format = "#,##0.00";
                dgv.Columns["moneyIn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns["moneyOut"].DefaultCellStyle.Format = "#,##0.00";
                dgv.Columns["moneyOut"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns["balance"].DefaultCellStyle.Format = "#,##0.00";
                dgv.Columns["balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgv == dgvWalletStatisticRefTypeId2)
            {
                dgv.Columns["refTypeId"].Visible = false;
                dgv.Columns["moneyIn"].DefaultCellStyle.Format = "#,##0.00";
                dgv.Columns["moneyIn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns["moneyOut"].DefaultCellStyle.Format = "#,##0.00";
                dgv.Columns["moneyOut"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns["balance"].DefaultCellStyle.Format = "#,##0.00";
                dgv.Columns["balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void OnStatisticDateFilterChanged(object sender, EventArgs e)
        {
            HideStatsPages();
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Focus();
        }

        private void onStatsTypeSelect(object sender, EventArgs e)
        {
            HideStatsPages();

            if (sender == llCorpWalletsInOutWithWithdrawals)
            {
                llCorpWalletsInOutWithWithdrawals.Font = new Font(llCorpWalletsInOutWithWithdrawals.Font, FontStyle.Italic);
                tcStats.TabPages.Add(tpCorpWalletsInOutWithWithdrawals);
                if (tcStats.SelectedTab == tpCorpWalletsInOutWithWithdrawals)
                {
                    dgvCorpWalletsInOutWithWithdrawals.DataSource = null;
                    mData.GetCorpWalletsInOutStatisticWithWithdrawal(mOptions.CharacterId, dtpStatisticFilterTop.Value, dtpStatisticFilterLow.Value);
                    dgvCorpWalletsInOutWithWithdrawals.DataSource = mData.TableCorpWalletsInOutWithWithdrawals;
                    SetupDataGridView(dgvCorpWalletsInOutWithWithdrawals);
                }
            }

            if (sender == llCorpWalletsInOutWithoutWithdrawals)
            {
                llCorpWalletsInOutWithoutWithdrawals.Font = new Font(llCorpWalletsInOutWithoutWithdrawals.Font, FontStyle.Italic);
                tcStats.TabPages.Add(tpCorpWalletsInOutWithoutWithdrawals);
                if (tcStats.SelectedTab == tpCorpWalletsInOutWithoutWithdrawals)
                {
                    //ShowProgress("Получение баланса кошельков...", 0.5);
                    dgvCorpWalletsInOutWithoutWithdrawals.DataSource = null;
                    mData.GetCorpWalletsInOutStatisticWithoutWithdrawal(mOptions.CharacterId, dtpStatisticFilterTop.Value, dtpStatisticFilterLow.Value);
                    dgvCorpWalletsInOutWithoutWithdrawals.DataSource = mData.TableCorpWalletsInOutWithoutWithdrawals;
                    SetupDataGridView(dgvCorpWalletsInOutWithoutWithdrawals);
                }
            }
            if (sender == llTransBetweenWallets)
            {
                llTransBetweenWallets.Font = new Font(llTransBetweenWallets.Font, FontStyle.Italic);
                tcStats.TabPages.Add(tpTransBetweenWallets);
            }
            if (sender == llWalletStatisticRefTypeId)
            {
                llWalletStatisticRefTypeId.Font = new Font(llWalletStatisticRefTypeId.Font, FontStyle.Italic);
                tcStats.TabPages.Add(tpWalletStatisticRefTypeId);
            }
            if (sender == llBountyPrizeByMember)
            {
                llBountyPrizeByMember.Font = new Font(llBountyPrizeByMember.Font, FontStyle.Italic);
                tcStats.TabPages.Add(tpBountyPrizeByMember);

                dgvBountyByMember.DataSource = null;
                mData.GetBountyByMember(mOptions.CharacterId, dtpStatisticFilterTop.Value, dtpStatisticFilterLow.Value);
                dgvBountyByMember.DataSource = mData.TableBountyByMember;
                SetupDataGridView(dgvBountyByMember);
            }
            if (sender == llWalletStatisticRefTypeId2)
            {
                llWalletStatisticRefTypeId2.Font = new Font(llWalletStatisticRefTypeId.Font, FontStyle.Italic);
                tcStats.TabPages.Add(tpWalletStatisticRefTypeId2);

                try
                {
                    dgvWalletStatisticRefTypeId2.DataSource = null;
                    mData.GetCorpWalletsInOutStatisticGroupByType(mOptions.CharacterId, dtpStatisticFilterTop.Value, dtpStatisticFilterLow.Value);
                    dgvWalletStatisticRefTypeId2.DataSource = mData.TableCorpWalletsInOutGroupByType;
                    SetupDataGridView(dgvWalletStatisticRefTypeId2);
                }
                catch (Exception exc)
                {
                    ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
                }
            }
        }

        private void cbWalletStatisticRefTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                object refId = cbWalletStatisticRefTypeId.SelectedValue;
                if (cbWalletStatisticRefTypeId.SelectedValue != null && refId.GetType() == typeof(int))
                {
                    dgvWalletStatisticRefTypeId.DataSource = null;
                    mData.GetCorpWalletsInOutStatisticByType(mOptions.CharacterId, dtpStatisticFilterTop.Value, dtpStatisticFilterLow.Value, (int)refId);
                    dgvWalletStatisticRefTypeId.DataSource = mData.TableCorpWalletsInOutByType;
                    SetupDataGridView(dgvWalletStatisticRefTypeId);
                }
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }

        private void HideStatsPages()
        {
            for (int i = tcStats.TabPages.Count - 1; i >= 0; i--)
            {
                tcStats.TabPages.Remove(tcStats.TabPages[i]);
            }
            llCorpWalletsInOutWithWithdrawals.Font = new Font(llCorpWalletsInOutWithWithdrawals.Font, FontStyle.Regular);
            llCorpWalletsInOutWithoutWithdrawals.Font = new Font(llCorpWalletsInOutWithWithdrawals.Font, FontStyle.Regular);
            llTransBetweenWallets.Font = new Font(llCorpWalletsInOutWithWithdrawals.Font, FontStyle.Regular);
            llWalletStatisticRefTypeId.Font = new Font(llCorpWalletsInOutWithWithdrawals.Font, FontStyle.Regular);
            llWalletStatisticRefTypeId2.Font = new Font(llCorpWalletsInOutWithWithdrawals.Font, FontStyle.Regular);
            llBountyPrizeByMember.Font = new Font(llBountyPrizeByMember.Font, FontStyle.Regular);
        }

        private void bGetStatistic_Click(object sender, EventArgs e)
        {
            RunFunctionAsync("getstat");
        }

        private void OnWithdrawalsButtonMouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (mWithdrawalsButtons.Count > 0)
                {
                    int accountKey = GetAccountKeyFromControl((Control)sender);
                    Bitmap bmp = new Bitmap(pbWithdrawalsBackground.Width, pbWithdrawalsBackground.Height);
                    Graphics gr = Graphics.FromImage(bmp);
                    gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    gr.FillRectangle(Brushes.Transparent, 0, 0, bmp.Width, bmp.Height);
                    Pen pen = new Pen(Color.Navy, 5.0f);
                    int selIndex = accountKey - 1000;
                    Control center = mWithdrawalsButtons[selIndex];
                    DataRow[] rows;
                    DataRow[] divnames;
                    for (int i = 0; i < 7; i++)
                    {
                        if (i != selIndex)
                        {
                            gr.DrawLine(pen, GetControlCenter(center), GetControlCenter(mWithdrawalsButtons[i]));
                            rows = mData.TableCorpWalletWithdrawals.Select(
                                String.Format("accountKeyFrom = {1} and accountKeyTo = {0}", GetAccountKeyFromControl(center), GetAccountKeyFromControl(mWithdrawalsButtons[i])));
                            if (rows.Length > 0)
                            {
                                double val = Convert.ToDouble(rows[0]["amount"]);
                                divnames = mData.TableWalletDivisions.Select(String.Format("accountKey = {0}", GetAccountKeyFromControl(mWithdrawalsButtons[i])));
                                mWithdrawalsButtons[i].Text = String.Format("{0}\n-{1:#,##0.00}", divnames[0]["description"], val);
                            }
                        }
                    }
                    rows = mData.TableCorpWalletWithdrawals.Select(String.Format("accountKeyTo = {0} and accountKeyFrom <> {0}", GetAccountKeyFromControl(center)));
                    double summTo = 0;
                    foreach (DataRow row in rows)
                    {
                        summTo += Convert.ToDouble(row["amount"]);
                    }
                    divnames = mData.TableWalletDivisions.Select(String.Format("accountKey = {0}", accountKey));
                    center.Text = String.Format("{0}\n{1:#,##0.00}", divnames[0]["description"], summTo);
                    pbWithdrawalsBackground.Image = bmp;
                }
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
        private int GetAccountKeyFromControl(Control control)
        {
            int accountKey = Convert.ToInt32(control.Name.Replace("bWD", ""));
            return accountKey;
        }

        private PointF GetControlCenter(Control control)
        {
            PointF p = new PointF(
                control.Width / 2 + control.Left - pbWithdrawalsBackground.Left,
                control.Height / 2 + control.Top - pbWithdrawalsBackground.Top);
            return p;
        }
    }
}
