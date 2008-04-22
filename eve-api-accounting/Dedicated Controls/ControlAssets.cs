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
    public partial class ControlAssets : UserControl
    {
        public ControlAssets(IMainControl mainControl, IHostWindow hostWindow)
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

            dgvAssetList.AutoGenerateColumns = false;

            if (mMainControl.GetConfigValue("assets view corp") == null)
                mViewForCorp = false;
            else
                mViewForCorp = (bool)mMainControl.GetConfigValue("assets view corp");
            
            if (mViewForCorp)
                rbCorpAssets.Checked = true;
            else
                rbCharAssets.Checked = true;
            mData.GetAssetListForView(mOptions.CharacterId, mViewForCorp);
            mMode = ViewMode.LocationsList;
            ConnectDataSource(true);

            dgvAssetList.SelectionChanged += new EventHandler(mHostWindow.ShowGridPosition);
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
        ViewMode mMode;
        DataView mDataView;
        bool mViewForCorp;
        #endregion
        #endregion

        void RunFunctionAsync(FunctionCompeletedEventArgs args)
        {
            //подготовка, если требуется
            switch (args.Function)
            {
                case ApiFunction.CharAssetList:
                case ApiFunction.CorpAssetList:
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
                #region ApiFunction.CharAssetList
                case ApiFunction.CharAssetList:
                    {
                        XmlNode nodeAssets = nodeRoot.SelectSingleNode("descendant::rowset[@name='assets']");
                        mHostWindow.ShowStatus(20, "Обработка...");

                        mData.ClearAssetList(mOptions.CharacterId, false);

                        //DataRow newRow = mData.TableAssetList.NewRow();
                        //Guid rowId = Guid.NewGuid();
                        //newRow["recordId"] = rowId;
                        //newRow["parentRecordId"] = Guid.Empty;
                        //newRow["isCorp"] = false;
                        //newRow["itemId"] = 0;
                        //newRow["locationId"] = 0;
                        //newRow["typeId"] = 0;
                        //newRow["quantity"] = 0;
                        //newRow["flag"] = 0;
                        //newRow["singleton"] = 0;
                        //newRow["hasChild"] = true;
                        //mData.TableAssetList.Rows.Add(newRow);

                        //ProcessRowset(nodeAssets, 0, false, rowId);
                        ProcessRowset(nodeAssets, 0, false, Guid.Empty);

                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitAssetList();
                        mHostWindow.ShowStatus(100, "Завершено");
                        result = args.Function;
                        break;
                    }
                #endregion
                #region ApiFunction.CorpAssetList
                case ApiFunction.CorpAssetList:
                    {
                        XmlNode nodeAssets = nodeRoot.SelectSingleNode("descendant::rowset[@name='assets']");
                        mHostWindow.ShowStatus(20, "Обработка...");

                        mData.ClearAssetList(mOptions.CharacterId, true);

                        //DataRow newRow = mData.TableAssetList.NewRow();
                        //Guid rowId = Guid.NewGuid();
                        //newRow["recordId"] = rowId;
                        //newRow["parentRecordId"] = Guid.Empty;
                        //newRow["isCorp"] = true;
                        //newRow["itemId"] = 0;
                        //newRow["locationId"] = 0;
                        //newRow["typeId"] = 0;
                        //newRow["quantity"] = 0;
                        //newRow["flag"] = 0;
                        //newRow["singleton"] = 0;
                        //newRow["hasChild"] = true;
                        //mData.TableAssetList.Rows.Add(newRow);

                        ProcessRowset(nodeAssets, 0, true, Guid.Empty);

                        mHostWindow.ShowStatus(90, "Подтверждение данных...");
                        mData.CommitAssetList();
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
                    #region ApiFunction.CharAssetList
                    case ApiFunction.CharAssetList:
                        {
                            mViewForCorp = false;
                            mData.GetAssetListForView(mOptions.CharacterId, mViewForCorp);
                            mMode = ViewMode.LocationsList;
                            ConnectDataSource(true);
                            break;
                        }
                    case ApiFunction.CorpAssetList:
                        {
                            mViewForCorp = true;
                            mData.GetAssetListForView(mOptions.CharacterId, mViewForCorp);
                            mMode = ViewMode.LocationsList;
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
            dgvAssetList.SelectionChanged -= new EventHandler(mHostWindow.ShowGridPosition);
            mMainControl.SetConfigValue("assets view corp", mViewForCorp);
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
                //mSession.GetFunctionAsync(ApiFunction.CharAssetList);
                //mData.ClearAssetList();
                //mData.GetAssetList(mOptions.CharacterId);
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load(@"G:\Documents\Visual Studio 2005\Projects\eve api\Accounting_v1.1\XmlSamples\CharAssetList.xml");
                //RunFunctionAsync(new FunctionCompeletedEventArgs(ApiFunction.CharAssetList, xmlDoc, "")); ;
                //xmlDoc.Load(@"G:\Documents\Visual Studio 2005\Projects\eve api\Accounting_v1.1\XmlSamples\CorpAssetList.xml");
                //RunFunctionAsync(new FunctionCompeletedEventArgs(ApiFunction.CorpAssetList, xmlDoc, "")); ;
                if (!mViewForCorp)
                    mSession.CommandQueue.Enqueue(new Command(ApiFunction.CharAssetList, mOptions.UserId, mOptions.ApiKey, mOptions.CharacterId));
                else
                    mSession.CommandQueue.Enqueue(new Command(ApiFunction.CorpAssetList, mOptions.UserId, mOptions.ApiKey, mOptions.CharacterId));
                mSession.ExecuteCommandFromQueue();
            }
            if (sender == rbCharAssets)
            {
                mViewForCorp = false;
                mData.GetAssetListForView(mOptions.CharacterId, mViewForCorp);
            }
            if (sender == rbCorpAssets)
            {
                mViewForCorp = true;
                mData.GetAssetListForView(mOptions.CharacterId, mViewForCorp);
            }
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            if (dgv == dgvAssetList)
            {
                SetupDataGridViewColumns(mData.TableAssetListForView, dgv);
                if (dgv.Columns.Contains("recordId"))
                    dgv.Columns["recordId"].Visible = false;
                if (dgv.Columns.Contains("parentRecordId"))
                    dgv.Columns["parentRecordId"].Visible = false;
                //if (dgv.Columns.Contains("hasChild"))
                //    dgv.Columns["hasChild"].Visible = false;
                //if (dgv.Columns.Contains("isCorp"))
                //    dgv.Columns["isCorp"].Visible = false;
                //if (!dgv.Columns.Contains("isCorpImage") && dgv.Columns.Contains("isCorp"))
                //{
                //    DataGridViewImageColumn col = new DataGridViewImageColumn();
                //    col.DataPropertyName = "aAssetListForView.isCorp";
                //    col.Name = "isCorpImage";
                //    //col.Image = InternalResources.Blank;
                //    dgv.Columns.Add(col);
                //}
                //if (!dgv.Columns.Contains("hasChildImage") && dgv.Columns.Contains("hasChild"))
                //{
                //    DataGridViewImageColumn col = new DataGridViewImageColumn();
                //    col.DataPropertyName = "aAssetListForView.hasChild";
                //    col.Name = "hasChildImage";
                //    //col.Image = InternalResources.Blank;
                //    dgv.Columns.Add(col);
                //}
                mData.FormatDataGridView(dgv);
                //if (dgv.Columns.Contains("isCorpImage"))
                //    dgv.Columns["isCorpImage"].DisplayIndex = 0;
                //if (dgv.Columns.Contains("hasChildImage"))
                //    dgv.Columns["hasChildImage"].DisplayIndex = 1;

                //foreach (DataGridViewRow dgvr in dgv.Rows)
                //{
                //    DataRowView drv = (DataRowView)dgvr.DataBoundItem;
                //    if (dgv.Columns.Contains("isCorpImage"))
                //    {
                //        //if (dgvr.Cells["isCorpImage"].Value == null)
                //        {
                //            if (Convert.ToBoolean(drv["isCorp"]) == true)
                //            {
                //                dgvr.Cells["isCorpImage"].Value = InternalResources.IsCorp;
                //            }
                //            else
                //            {
                //                dgvr.Cells["isCorpImage"].Value = InternalResources.Blank;
                //            }
                //        }
                //    }
                //    if (dgv.Columns.Contains("hasChildImage"))
                //    {
                //        //if (dgvr.Cells["hasChildImage"].Value == null)
                //        {
                //            if (Convert.ToBoolean(dgvr.Cells["hasChild"].Value) == true)
                //            {
                //                dgvr.Cells["hasChildImage"].Value = InternalResources.HasChild;
                //            }
                //            else
                //            {
                //                dgvr.Cells["hasChildImage"].Value = InternalResources.Blank;
                //            }
                //        }
                //    }
                //}
            }
        }

        private void ProcessRowset(XmlNode rowset, int locationId, bool isCorp, Guid parentId)
        {
            foreach (XmlNode subNode in rowset.ChildNodes)
            {
                //разбор данных
                if (subNode.Attributes["locationID"] != null)
                {
                    locationId = Convert.ToInt32(subNode.Attributes["locationID"].Value);
                }
                int itemId = Convert.ToInt32(subNode.Attributes["itemID"].Value);
                int typeId = Convert.ToInt32(subNode.Attributes["typeID"].Value);
                int quantity = Convert.ToInt32(subNode.Attributes["quantity"].Value);
                int flag = Convert.ToInt32(subNode.Attributes["flag"].Value);
                bool singleton = (Convert.ToInt32(subNode.Attributes["singleton"].Value) > 0 ? true : false);
                Guid rowId = Guid.NewGuid();
                //добавление в базу
                DataRow newRow = mData.TableAssetList.NewRow();
                newRow["recordId"] = rowId;
                newRow["parentRecordId"] = parentId;
                newRow["isCorp"] = isCorp;
                newRow["itemId"] = itemId;
                newRow["locationId"] = locationId;
                newRow["typeId"] = typeId;
                newRow["quantity"] = quantity;
                newRow["flag"] = flag;
                newRow["singleton"] = singleton;
                newRow["hasChild"] = subNode.HasChildNodes;
                mData.TableAssetList.Rows.Add(newRow);
                //продолжение обработки
                if (subNode.HasChildNodes)
                {
                    XmlNode subRowSet = subNode.FirstChild;
                    ProcessRowset(subRowSet, locationId, isCorp, rowId);
                }
            }
        }

        private void dgvAssetList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && dgv.CurrentRow != null)
            {
                OpenAssetRow();
            }
        }

        private void dgvAssetList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenAssetRow();
            }
            if (e.KeyCode == Keys.Back)
            {
                CloseAssetRow();
            }
        }

        private void OpenAssetRow()
        {
            DataGridView dgv = dgvAssetList;
            if (dgv.CurrentRow != null)
            {
                DataRowView drv = (DataRowView)dgv.CurrentRow.DataBoundItem;
                if (mMode == ViewMode.ByItem)
                {
                    Guid rowId = new Guid(Convert.ToString(drv["recordId"]));
                    bool hasChild = Convert.ToBoolean(drv["hasChild"]);
                    if (hasChild)
                    {
                        ConnectDataSource(false);
                        mData.GetAssetListForView(mOptions.CharacterId, rowId);
                        mMode = ViewMode.ByItem;
                        ConnectDataSource(true);
                    }
                }
                if (mMode == ViewMode.LocationsList)
                {
                    int locationId = Convert.ToInt32(drv["locationId"]);
                    bool isCorp = Convert.ToBoolean(drv["isCorp"]);

                    ConnectDataSource(false);
                    mData.GetAssetListForView(mOptions.CharacterId, locationId, isCorp);
                    mMode = ViewMode.ByItem;
                    ConnectDataSource(true);
                }
            }
        }
        private void CloseAssetRow()
        {
            DataGridView dgv = dgvAssetList;
            if (dgv.CurrentRow != null)
            {
                if (mMode == ViewMode.ByItem)
                {
                    DataRowView drv = (DataRowView)dgv.CurrentRow.DataBoundItem;
                    //ID родительского элемента для этого выбранного
                    Guid rowId = new Guid(Convert.ToString(drv["parentRecordId"]));
                    int locationId = Convert.ToInt32(drv["locationId"]);
                    bool isCorp = Convert.ToBoolean(drv["isCorp"]);
                    if (rowId != Guid.Empty)
                    {
                        rowId = mData.GetParentIdForRecordId(mOptions.CharacterId, rowId);
                        if (rowId != Guid.Empty)
                        {
                            ConnectDataSource(false);
                            mData.GetAssetListForView(mOptions.CharacterId, rowId);
                            mMode = ViewMode.ByItem;
                            ConnectDataSource(true);
                        }
                        else
                        {
                            rowId = mData.GetParentIdForRecordId(mOptions.CharacterId, rowId);

                            ConnectDataSource(false);
                            mData.GetAssetListForView(mOptions.CharacterId, locationId, isCorp);
                            mMode = ViewMode.ByItem;
                            ConnectDataSource(true);
                        }
                    }
                    else
                    {

                        ConnectDataSource(false);
                        mData.GetAssetListForView(mOptions.CharacterId, mViewForCorp);
                        mMode = ViewMode.LocationsList;
                        ConnectDataSource(true);
                    }
                }
            }
        }

        private void SetupDataGridViewColumns(DataTable table, DataGridView view)
        {
            dgvAssetList.Columns.Clear();
            /*
             * isCorp bool
             * locationId int
             * locationName string
             * 
             * isCorp bool
             * hasChild bool
             * locationId int
             * locationName string
             * itemId int
             * typeId int
             * typeName string
             * quantity int
             * flag int
             * singleton bool
             */
            if (table.Columns.Count == 4)
            {
                if (table.Columns.Contains("isCorp"))
                {
                    DataGridViewImageColumn col = new DataGridViewImageColumn();
                    col.DataPropertyName = "isCorpImage";
                    col.Name = "isCorpImage";
                    //col.Image = InternalResources.Blank;
                    view.Columns.Add(col);
                }
                if (table.Columns.Contains("locationId"))
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "locationId";
                    col.HeaderText = "locationId";
                    col.Name = "locationId";
                    dgvAssetList.Columns.Add(col);
                }
                if (table.Columns.Contains("locationName"))
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "locationName";
                    col.HeaderText = "locationName";
                    col.Name = "locationName";
                    dgvAssetList.Columns.Add(col);
                }
            }
            else
            {
                if (table.Columns.Contains("isCorp"))
                {
                    DataGridViewImageColumn col = new DataGridViewImageColumn();
                    col.DataPropertyName = "isCorpImage";
                    col.Name = "isCorpImage";
                    //col.Image = InternalResources.Blank;
                    view.Columns.Add(col);
                }
                if (table.Columns.Contains("hasChild"))
                {
                    DataGridViewImageColumn col = new DataGridViewImageColumn();
                    col.DataPropertyName = "hasChildImage";
                    col.Name = "hasChildImage";
                    //col.Image = InternalResources.Blank;
                    view.Columns.Add(col);
                }
                if (table.Columns.Contains("locationId"))
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "locationId";
                    col.HeaderText = "locationId";
                    col.Name = "locationId";
                    dgvAssetList.Columns.Add(col);
                }
                if (table.Columns.Contains("locationName"))
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "locationName";
                    col.HeaderText = "locationName";
                    col.Name = "locationName";
                    dgvAssetList.Columns.Add(col);
                }
                if (table.Columns.Contains("itemId"))
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "itemId";
                    col.HeaderText = "itemId";
                    col.Name = "itemId";
                    dgvAssetList.Columns.Add(col);
                }
                if (table.Columns.Contains("typeId"))
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "typeId";
                    col.HeaderText = "typeId";
                    col.Name = "typeId";
                    dgvAssetList.Columns.Add(col);
                }
                if (table.Columns.Contains("typeName"))
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "typeName";
                    col.HeaderText = "typeName";
                    col.Name = "typeName";
                    dgvAssetList.Columns.Add(col);
                }
                if (table.Columns.Contains("quantity"))
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "quantity";
                    col.HeaderText = "quantity";
                    col.Name = "quantity";
                    dgvAssetList.Columns.Add(col);
                }
                if (table.Columns.Contains("flagText"))
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.DataPropertyName = "flagText";
                    col.HeaderText = "flagText";
                    col.Name = "flagText";
                    dgvAssetList.Columns.Add(col);
                }
                if (table.Columns.Contains("singleton"))
                {
                    DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
                    col.DataPropertyName = "singleton";
                    col.HeaderText = "singleton";
                    col.Name = "singleton";
                    dgvAssetList.Columns.Add(col);
                }
            }
        }

        private void ConnectDataSource(bool connect)
        {
            if (connect)
            {
                mDataView = new DataView(mData.TableAssetListForView);
                dgvAssetList.DataSource = mDataView;
                SetupDataGridView(dgvAssetList);
            }
            else
            {
                dgvAssetList.DataSource = null;
                mDataView.Dispose();
                mDataView = null;
            }
        }
    }
    internal enum ViewMode
    {
        LocationsList,
        ByItem
    }
}
