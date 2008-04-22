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
    public partial class ControlKillLogView : UserControl
    {
        public ControlKillLogView(IMainControl mainControl, IHostWindow hostWindow, object[] data)
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
            timerStringAsyncDebugRun.Interval = 500;
            timerStringAsyncDebugRun.Tick += new EventHandler(timerStringAsyncDebugRun_Tick);

            if (data[0] is Guid)
            {
                mRecordId = (Guid)data[0];

                mData.GetKillLogsVictimsForView(mRecordId);
                mData.GetKillLogsAttackersForView(mRecordId);
                mData.GetKillLogsItemsForView(mRecordId);
                foreach (DataRow row in mData.TableKillLogsItemsForView.Rows)
                {
                    if (row["typeId"] is DBNull)
                        row.Delete();
                }
                mData.TableKillLogsItemsForView.AcceptChanges();
                {
                    DataRow row = mData.TableKillLogsVictimsForView.Rows[0];
                    lInfoVictim.Text = String.Format(
                        "Date: {0}\nVictim:{1}\nCorporation: {2}\nAlliance: {3}\nShip: {4} ({5})\nLocation: {6}, Security: {7}\nDamage taken: {8}",
                        row["killTime"],
                        row["characterName"],
                        row["CorporationName"],
                        row["allianceName"],
                        row["shipTypeName"],
                        row["shipGroupName"],
                        row["solarSystemName"],
                        row["security"],
                        row["damageTaken"]);
                    pbVictim.Image = null;
                }
                {

                    dgvKillLogAttackers.AutoGenerateColumns = false;
                    dgvKillLogAttackers.DataSource = mData.TableKillLogsAttackersForView;

                    DataGridViewImageColumn colImage = new DataGridViewImageColumn();
                    colImage.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    colImage.DataPropertyName = "charImage";
                    colImage.Name = "charImage";
                    colImage.HeaderText = "charImage";
                    dgvKillLogAttackers.Columns.Add(colImage);

                    DataGridViewTextBoxColumn colText = new DataGridViewTextBoxColumn();
                    colText.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    colText.DataPropertyName = "characterName";
                    colText.Name = "characterData";
                    colText.HeaderText = "characterData";
                    dgvKillLogAttackers.Columns.Add(colText);

                    dgvKillLogAttackers.RowTemplate.Height = 100;
                    dgvKillLogAttackers.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }
                {

                    dgvKillLogItems.DataSource = mData.TableKillLogsItemsForView.DefaultView;
                    foreach (DataRow row in mData.TableKillLogsItemsForView.Rows)
                    {
                        int typeId = Convert.ToInt32(row["typeId"]);
                        int flag = Convert.ToInt32(row["flag"]);
                        if (flag == 0)
                        {
                            SlotType slotTtype = mData.GetSlotType(typeId);
                            row["slotType"] = slotTtype;
                            row["slotTypeImage"] = InternalResources.Blank;
                            switch (slotTtype)
                            {
                                case SlotType.None:
                                    row["slotTypeImage"] = InternalResources.Blank;
                                    break;
                                case SlotType.High:
                                    row["slotTypeImage"] = InternalResources.HighSlot;
                                    break;
                                case SlotType.Middle:
                                    row["slotTypeImage"] = InternalResources.MidSlot;
                                    break;
                                case SlotType.Low:
                                    row["slotTypeImage"] = InternalResources.LowSlot;
                                    break;
                                case SlotType.Rig:
                                    row["slotTypeImage"] = InternalResources.RigSlot;
                                    break;
                            }
                        }
                        else
                        {
                            if(flag == 5)
                                row["slotTypeImage"] = InternalResources.CargoBay;
                            else
                                if (flag == 87)
                                    row["slotTypeImage"] = InternalResources.DroneBay;
                        }
                    }
                    mData.TableKillLogsItemsForView.DefaultView.Sort = "slotType DESC, flag DESC";
                    dgvKillLogItems.Columns["itemImage"].DisplayIndex = 0;
                    dgvKillLogItems.Columns["itemImage"].Width = 64;
                    dgvKillLogItems.Columns["slotTypeImage"].DisplayIndex = 1;
                    dgvKillLogItems.Columns["slotTypeImage"].Width = 64;
                    dgvKillLogItems.Columns["typeId"].Visible = false;
                    dgvKillLogItems.Columns["groupName"].Visible = false;
                    dgvKillLogItems.Columns["flag"].Visible = false;
                    dgvKillLogItems.Columns["flagText"].Visible = false;
                    dgvKillLogItems.Columns["qtyDropped"].Visible = false;
                    dgvKillLogItems.Columns["slotType"].Visible = false;
                    dgvKillLogItems.Columns["qtyDestroyed"].Visible = false;
                    dgvKillLogItems.Columns["typeName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvKillLogItems.RowTemplate.Height = 64;
                    dgvKillLogItems.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }
                StartDownloadImages();
                dgvKillLogAttackers.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvKillLogAttackers_CellFormatting);
                dgvKillLogItems.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvKillLogAttackers_CellFormatting);
            }

            dgvKillLogAttackers.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
            dgvKillLogItems.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
        }
        #region variables
        #region простые переменные
        private IMainControl mMainControl;
        private IHostWindow mHostWindow;
        private Options mOptions;
        private Session mSession;
        private DataClass mData;
        System.Windows.Forms.Timer timerAsyncDebugRun = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timerStringAsyncDebugRun = new System.Windows.Forms.Timer();
        #endregion

        #region private
        Guid mRecordId;
        Image mDownloadedImage;
        int mDownloadedImageId;
        string mDownloadedColumnName;
        DataRow mDownloadedRow;
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
            mSession.FunctionCompleted -= new FunctionCompletedHandler(mSession_FunctionCompleted);
            dgvKillLogAttackers.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
            dgvKillLogItems.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
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
            if (dgv == dgvKillLogAttackers)
            {
            }
        }

        private void dgvKillLogAttackers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (sender == dgvKillLogAttackers)
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && !e.FormattingApplied && e.DesiredType == typeof(string))
                {
                    try
                    {
                        DataRowView drv = (DataRowView)dgv.Rows[e.RowIndex].DataBoundItem;
                        e.Value = String.Format(
                            "Name: {0}\nCorporation: {1}\nAlliance: {2}\nSecurity Status: {3}\nShip: {4} ({5})\nWeapon: {6}\nDamage done: {7}",
                            drv["characterName"],
                            drv["corporationName"],
                            drv["allianceName"],
                            drv["securityStatus"],
                            drv["shipTypeName"],
                            drv["shipGroupName"],
                            drv["weaponTypeName"],
                            drv["damageDone"]
                            );
                        e.FormattingApplied = true;
                    }
                    catch
                    {
                        e.FormattingApplied = false;
                    }
                }
            }
            if (sender == dgvKillLogItems)
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && !e.FormattingApplied && dgv.Columns[e.ColumnIndex].Name == "typeName")
                {
                    try
                    {
                        DataRowView drv = (DataRowView)dgv.Rows[e.RowIndex].DataBoundItem;
                        int dropped = Convert.ToInt32(drv["qtyDropped"]);
                        int destroyed = Convert.ToInt32(drv["qtyDestroyed"]);
                        int flag = Convert.ToInt32(drv["flag"]);
                        string val = String.Format("{0} ({1})", drv["typeName"], drv["groupName"]);
                        if (flag > 0)
                        {
                            val += String.Format("\nfrom: {0}", drv["flagText"]);
                        }
                        if (dropped > 0 && destroyed > 0)
                        {
                            val += String.Format("\nDropped: {0}, Destroyed: {1}", dropped, destroyed);
                        }
                        if (destroyed == 0)
                        {
                            val += String.Format("\nDropped: {0}", dropped);
                        }
                        if (dropped == 0)
                        {
                            val += String.Format("\nDestroyed: {0}", destroyed);
                        }
                        e.Value = val;
                        e.FormattingApplied = true;
                    }
                    catch
                    {
                        e.FormattingApplied = false;
                    }
                }
            }
        }

        private void dgvKillLogItems_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (sender == dgvKillLogItems)
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    DataRowView drv = (DataRowView)dgv.Rows[e.RowIndex].DataBoundItem;
                    if (!(drv["qtyDropped"] is DBNull))
                    {
                        int dropped = Convert.ToInt32(drv["qtyDropped"]);
                        if (dropped > 0)
                            e.CellStyle.BackColor = Color.Green;
                    }
                }
            }
        }


        private void StartDownloadImages()
        {
            DataRow row1 = mData.TableKillLogsVictimsForView.Rows[0];
            if (pbVictim.Image == null)
            {
                mDownloadedImageId = Convert.ToInt32(row1["characterId"]);
                RunStringFunctionAsync("download:victimImage");
            }
            else
            {
                bool flag = false;
                foreach (DataRow row in mData.TableKillLogsAttackersForView.Rows)
                {
                    string colName = "charImage";
                    if (row[colName] == null || row[colName] is DBNull)
                    {
                        mDownloadedColumnName = colName;
                        mDownloadedRow = row;
                        mDownloadedImageId = Convert.ToInt32(row["characterId"]);
                        RunStringFunctionAsync("download:charImage");
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    foreach (DataRow row in mData.TableKillLogsItemsForView.Rows)
                    {
                        string colName = "itemImage";
                        if (row[colName] == null || row[colName] is DBNull)
                        {
                            //if (!(row["typeId"] is DBNull))
                            {
                                mDownloadedColumnName = colName;
                                mDownloadedRow = row;
                                mDownloadedImageId = Convert.ToInt32(row["typeId"]);
                                RunStringFunctionAsync("download:typeImage");
                            }
                            break;
                        }
                    }
                }
            }
        }

        void RunStringFunctionAsync(string value)
        {
            //подготовка, если требуется
            switch (value)
            {
                case "":
                    break;
            }
#if DEBUG
            timerStringAsyncDebugRun.Tag = value;
            timerStringAsyncDebugRun.Start();
#else
            StringFunctionAsyncDelegate f = this.StringFunctionAsync;
            f.BeginInvoke(value, new AsyncCallback(StringFunctionCompletedCallBack), this);
#endif
        }

        string StringFunctionAsync(string value)
        {
            string result = "";
            if (value.Contains("download"))
            {
                string fileName;
                if (value == "download:victimImage")
                {
                    if (!mSession.GetCharImage(ImageSize.x64, mDownloadedImageId, out mDownloadedImage, out fileName))
                        mDownloadedImage = InternalResources.NoPortrait;
                    result = value;
                }
                if (value == "download:charImage")
                {
                    if (!mSession.GetCharImage(ImageSize.x64, mDownloadedImageId, out mDownloadedImage, out fileName))
                        mDownloadedImage = InternalResources.NoPortrait;
                    result = value;
                }
                if (value == "download:typeImage")
                {
                    if (!mSession.GetTypeImage(ImageSize.x64, mDownloadedImageId, out mDownloadedImage))
                        mDownloadedImage = InternalResources.NoPortrait;
                    result = value;
                }
            }
            if (result == "")
                mHostWindow.ShowStatus("Произошла ошибка");
            else
                mHostWindow.ShowStatus("Завершено");
            return result;
        }
        void StringFunctionCompletedCallBack(IAsyncResult result)
        {
            try
            {
                AsyncResult r = (AsyncResult)result;
                StringFunctionAsyncDelegate command = (StringFunctionAsyncDelegate)r.AsyncDelegate;
                string val = command.EndInvoke(result);
                StringFunctionCompleted(val);
            }
            catch (Exception exc)
            {
                mMainControl.ProcessException(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
        void StringFunctionCompleted(string value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((EventHandler)delegate
                {
                    StringFunctionCompleted(value);
                });
            }
            else
            {
                if (value.Contains("download"))
                {
                    if (value == "download:victimImage")
                    {
                        pbVictim.Image = (Image)mDownloadedImage.Clone();
                    }
                    else
                    {
                        if (mDownloadedRow.RowState == DataRowState.Detached || mDownloadedRow.RowState == DataRowState.Deleted)
                        {
                        }
                        else
                        {
                            mDownloadedRow[mDownloadedColumnName] = mDownloadedImage.Clone();
                        }
                    }
                }
                Thread.Sleep(10);
                StartDownloadImages();
            }
        }
        void timerStringAsyncDebugRun_Tick(object sender, EventArgs e)
        {
            timerStringAsyncDebugRun.Stop();
            string value = (string)timerStringAsyncDebugRun.Tag;
            value = StringFunctionAsync(value);
            StringFunctionCompleted(value);
        }
        delegate string StringFunctionAsyncDelegate(string value);
    }
}
