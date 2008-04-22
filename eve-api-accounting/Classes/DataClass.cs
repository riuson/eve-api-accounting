using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Drawing;
using System.Net;

namespace Accounting
{
    /// <summary>
    /// класс связи с базой данных
    /// </summary>
    public class DataClass : IDisposable
    {
        public DataClass()
        {
            CheckOpen();
            mDataSet = new DataSet("Accounting");
            mCorporationSheetInfo = new CorporationSheetInfo();
        }

        #region Переменные
        /// <summary>
        /// соединение с базой данных
        /// </summary>
        OleDbConnection connectDB;
        //Датасет
        private DataSet mDataSet;
        public DataSet DataSet
        {
            get { return mDataSet; }
        }
        //Комманд билдер
        private OleDbCommandBuilder mCommandBuilder;

        private Stack<Boolean> openStates = new Stack<bool>();
        private bool baseExists;
        //private Environment.SpecialFolder programDataFolder = Environment.SpecialFolder.ApplicationData;
        public static string DataBaseFile
        {
            get
            {
                return Path.Combine(Options.ExePath, "database.mdb");
            }
        }
        //public event ExceptionHandler OnException;
        //private DataView mDataViewRefTypesFiltered;
        #endregion

        public bool SureBaseExists()
        {
            bool result = true;
            if (!File.Exists(DataBaseFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DataBaseFile));

                try
                {
                    File.Copy(Path.Combine(Options.ExePath, "database.mdb"), DataBaseFile);
                }
                catch (Exception exc)
                {
                    CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
                    result = false;
                }
            }
            baseExists = result;
            return result;
        }
        private void CallExceptionHandler(Exception exc, string methodName)
        {
            //if (OnException != null)
            //{
            //    ExceptionHandlerEventArgs ea = new ExceptionHandlerEventArgs(exc, methodName);
            //    OnException(this, ea);
            //}
        }

        public bool connectionOpened
        {
            get
            {
                bool res = false;
                if (connectDB != null)
                    res = connectDB.State == System.Data.ConnectionState.Open;
                else
                    res = false;
                return res;
            }
        }
        public bool OpenDatabase()
        {
            bool res = false;
            string source = String.Format(
                @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = {0};", DataBaseFile);
            connectDB = new OleDbConnection(source);
            try
            {
                connectDB.Open();
                res = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return res;
        }
        public bool CloseDatabase()
        {
            bool res = false;
            try
            {
                connectDB.Close();
                if (connectDB.State == System.Data.ConnectionState.Closed)
                {
                    res = true;
                }
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return res;
        }


        #region CharAccountBalance
        public DataTable TableCharAccountBalance
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCharAccountBalance"))
                {
                    mDataSet.Tables.Add("aCharAccountBalance");
                }
                return mDataSet.Tables["aCharAccountBalance"];
            }
        }
        private OleDbDataAdapter moddaCharAccountBalance;
        public bool GetCharAccountBalance(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableCharAccountBalance.Clear();
            moddaCharAccountBalance = new OleDbDataAdapter(String.Format("select * from aCharAccountBalance where userId={0};", characterId), connectDB);
            moddaCharAccountBalance.RowUpdating += new OleDbRowUpdatingEventHandler(moddaCharAccountBalance_RowUpdating);
            mCommandBuilder = new OleDbCommandBuilder(moddaCharAccountBalance);

            moddaCharAccountBalance.Fill(mDataSet, "aCharAccountBalance");
            TableCharAccountBalance.Columns["userId"].DefaultValue = characterId;
            //CheckClose();
            return result;
        }

        private void moddaCharAccountBalance_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public void ClearCharAccountBalance()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCharAccountBalance", connectDB);
            cmd.ExecuteNonQuery();
            TableCharAccountBalance.Clear();
            //CheckClose();
        }
        public int CommitCharAccountBalance()
        {
            int result = -1;
            try
            {
                result = moddaCharAccountBalance.Update(TableCharAccountBalance);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        #endregion

        #region CorpAccountBalance
        public DataTable TableCorpAccountBalance
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpAccountBalance"))
                {
                    mDataSet.Tables.Add("aCorpAccountBalance");
                }
                return mDataSet.Tables["aCorpAccountBalance"];
            }
        }
        private OleDbDataAdapter moddaCorpAccountBalance;
        public bool GetCorpAccountBalance(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableCorpAccountBalance.Clear();
            moddaCorpAccountBalance = new OleDbDataAdapter(String.Format("select * from aCorpAccountBalance where userId = {0} order by accountKey ;", characterId), connectDB);
            moddaCorpAccountBalance.RowUpdating += new OleDbRowUpdatingEventHandler(moddaCorpAccountBalance_RowUpdating);
            mCommandBuilder = new OleDbCommandBuilder(moddaCorpAccountBalance);

            moddaCorpAccountBalance.Fill(mDataSet, "aCorpAccountBalance");
            TableCorpAccountBalance.Columns["userId"].DefaultValue = characterId;
            //CheckClose();
            return result;
        }

        private void moddaCorpAccountBalance_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public void ClearCorpAccountBalance()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCorpAccountBalance", connectDB);
            cmd.ExecuteNonQuery();
            TableCorpAccountBalance.Clear();
            //CheckClose();
        }
        public int CommitCorpAccountBalance()
        {
            int result = -1;
            try
            {
                result = moddaCorpAccountBalance.Update(TableCorpAccountBalance);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        #endregion

        #region CharWalletJournal
        public DataTable TableCharWalletJournal
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCharWalletJournal"))
                {
                    mDataSet.Tables.Add("aCharWalletJournal");
                }
                return mDataSet.Tables["aCharWalletJournal"];
            }
        }
        private OleDbDataAdapter moddaCharWalletJournal;
        public bool GetCharWalletJournal(int characterId, string accountKey, DateTime top, DateTime low)
        {
            //CheckOpen();
            bool result = false;
            TableCharWalletJournal.Clear();
            moddaCharWalletJournal = new OleDbDataAdapter(
                String.Format(
                    "select * from aCharWalletJournal where userId = {0} and accountKey = {1} and ([date] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#) order by [date];",
                    characterId, accountKey, low, top),
                connectDB);
            moddaCharWalletJournal.RowUpdating += new OleDbRowUpdatingEventHandler(moddaCharWalletJournal_RowUpdating);
            mCommandBuilder = new OleDbCommandBuilder(moddaCharWalletJournal);
            mCommandBuilder.QuotePrefix = "[";
            mCommandBuilder.QuoteSuffix = "]";
            //OleDbCommand cmd = mCommandBuilder.GetInsertCommand();

            moddaCharWalletJournal.Fill(mDataSet, "aCharWalletJournal");
            TableCharWalletJournal.Columns["userId"].DefaultValue = characterId;
            TableCharWalletJournal.Columns["accountKey"].DefaultValue = accountKey;
            //CheckClose();
            return result;
        }

        private void moddaCharWalletJournal_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public bool IsCharWalletJournalRecordExists(int characterId, string accountKey, DataRow row)
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand(
                String.Format(
                    "select count (*) from aCharWalletJournal where userId = {0} and accountKey = {1} and [date] = #{2:MM'/'dd'/'yyyy HH:mm}# and refId = {3}",
                    characterId, accountKey, row["date"], row["refId"]),
                connectDB);
            int count = (int)cmd.ExecuteScalar();
            //CheckClose();
            return (count > 0);
        }
        public int CommitCharWalletJournal()
        {
            int result = -1;
            try
            {
                result = moddaCharWalletJournal.Update(TableCharWalletJournal);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        public void ClearCharWalletJournal()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCharWalletJournal", connectDB);
            cmd.ExecuteNonQuery();
            TableCharWalletJournal.Clear();
            //CheckClose();
        }

        public DataTable TableCharWalletJournalForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCharWalletJournalForView"))
                {
                    mDataSet.Tables.Add("aCharWalletJournalForView");
                }
                return mDataSet.Tables["aCharWalletJournalForView"];
            }
        }
        public bool GetCharWalletJournalForView(int characterId, string accountKey, DateTime top, DateTime low)
        {
            //CheckOpen();
            bool result = false;
            TableCharWalletJournalForView.Clear();
            OleDbDataAdapter moddaCharWalletJournalForView = new OleDbDataAdapter(
                String.Format(
                    "select aCharWalletJournal.refId, [date], aCharWalletJournal.ownerName1, aRefTypes.refTypeName, aCharWalletJournal.ownerName2, aCharWalletJournal.argName1, aCharWalletJournal.amount, aCharWalletJournal.balance, aCharWalletJournal.reason from aCharWalletJournal left join aRefTypes on (aCharWalletJournal.refTypeId =aRefTypes.refTypeId) where aCharWalletJournal.userId = {0} and accountKey = {1} and ([date] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#) order by [date];",
                    characterId, accountKey, low, top),
                connectDB);

            moddaCharWalletJournalForView.Fill(mDataSet, "aCharWalletJournalForView");
            //CheckClose();
            return result;
        }
        #endregion

        #region CorpWalletJournal
        public DataTable TableCorpWalletJournal
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpWalletJournal"))
                {
                    mDataSet.Tables.Add("aCorpWalletJournal");
                }
                return mDataSet.Tables["aCorpWalletJournal"];
            }
        }
        private OleDbDataAdapter moddaCorpWalletJournal;
        public bool GetCorpWalletJournal(int characterId, string accountKey, DateTime top, DateTime low)
        {
            //CheckOpen();
            bool result = false;
            TableCorpWalletJournal.Clear();
            moddaCorpWalletJournal = new OleDbDataAdapter(
                String.Format(
                    "select * from aCorpWalletJournal where userId = {0} and accountKey = {1} and ([date] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#) order by [date];",
                    characterId, accountKey, low, top),
                connectDB);
            moddaCorpWalletJournal.RowUpdating += new OleDbRowUpdatingEventHandler(moddaCorpWalletJournal_RowUpdating);
            mCommandBuilder = new OleDbCommandBuilder(moddaCorpWalletJournal);
            mCommandBuilder.QuotePrefix = "[";
            mCommandBuilder.QuoteSuffix = "]";
            //OleDbCommand cmd = mCommandBuilder.GetInsertCommand();

            moddaCorpWalletJournal.Fill(mDataSet, "aCorpWalletJournal");
            TableCorpWalletJournal.Columns["userId"].DefaultValue = characterId;
            TableCorpWalletJournal.Columns["accountKey"].DefaultValue = accountKey;
            //CheckClose();
            return result;
        }

        private void moddaCorpWalletJournal_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public bool IsCorpWalletJournalRecordExists(int characterId, string accountKey, DataRow row)
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand(
                String.Format(
                    "select count (*) from aCorpWalletJournal where userId = {0} and accountKey = {1} and [date] = #{2:MM'/'dd'/'yyyy HH:mm}# and refId = {3}",
                    characterId, accountKey, row["date"], row["refId"]),
                connectDB);
            int count = (int)cmd.ExecuteScalar();
            //CheckClose();
            return (count > 0);
        }
        public int CommitCorpWalletJournal()
        {
            int result = -1;
            try
            {
                result = moddaCorpWalletJournal.Update(TableCorpWalletJournal);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        public void ClearCorpWalletJournal()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCorpWalletJournal", connectDB);
            cmd.ExecuteNonQuery();
            TableCorpWalletJournal.Clear();
            //CheckClose();
        }

        public DataTable TableCorpWalletJournalForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpWalletJournalForView"))
                {
                    mDataSet.Tables.Add("aCorpWalletJournalForView");
                }
                return mDataSet.Tables["aCorpWalletJournalForView"];
            }
        }
        public bool GetCorpWalletJournalForView(int characterId, string accountKey, DateTime top, DateTime low)
        {
            //CheckOpen();
            bool result = false;
            TableCorpWalletJournalForView.Clear();
            OleDbDataAdapter moddaCorpWalletJournalForView = new OleDbDataAdapter(
                String.Format(
                //"select aCorpWalletJournal.*, aRefTypes.refTypeName from aCorpWalletJournal left join aRefTypes on (aCorpWalletJournal.refTypeId = aRefTypes.refTypeId and aCorpWalletJournal.userId = aRefTypes.userId) where aCorpWalletJournal.userId = {0} and accountKey = {1} and ([date] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#) order by [date];",
                    "select aCorpWalletJournal.refId, [date], aCorpWalletJournal.ownerName1, aRefTypes.refTypeName, aCorpWalletJournal.ownerName2, aCorpWalletJournal.argName1, aCorpWalletJournal.amount, aCorpWalletJournal.balance, aCorpWalletJournal.reason from aCorpWalletJournal left join aRefTypes on (aCorpWalletJournal.refTypeId =aRefTypes.refTypeId) where aCorpWalletJournal.userId = {0} and accountKey = {1} and ([date] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#) order by [date];",
                    characterId, accountKey, low, top),
                connectDB);

            moddaCorpWalletJournalForView.Fill(mDataSet, "aCorpWalletJournalForView");
            //CheckClose();
            return result;
        }
        #endregion


        //#region RefTypesFiltered
        //private DataView TableRefTypesFiltered
        //{
        //    get
        //    {
        //        if (mDataViewRefTypesFiltered == null)
        //        {
        //            mDataViewRefTypesFiltered = new DataView(TableRefTypes);
        //            mDataViewRefTypesFiltered.RowFilter = "needed = true";
        //        }
        //        return mDataViewRefTypesFiltered;
        //    }
        //}
        //#endregion

        #region WalletDivisions & Divisions
        public DataTable TableWalletDivisions
        {
            get
            {
                //return mCorporationSheetInfo.WalletDivisions;
                if (!mDataSet.Tables.Contains("aWalletDivisions"))
                {
                    mDataSet.Tables.Add("aWalletDivisions");
                    mDataSet.Tables["aWalletDivisions"].Columns.Add("accountKey", typeof(Int32));
                    mDataSet.Tables["aWalletDivisions"].Columns.Add("description", typeof(String));
                }
                return mDataSet.Tables["aWalletDivisions"];
            }
        }
        public DataTable TableDivisions
        {
            get
            {
                //return mCorporationSheetInfo.Divisions;
                if (!mDataSet.Tables.Contains("aDivisions"))
                {
                    mDataSet.Tables.Add("aDivisions");
                    mDataSet.Tables["aDivisions"].Columns.Add("accountKey", typeof(Int32));
                    mDataSet.Tables["aDivisions"].Columns.Add("description", typeof(String));
                }
                return mDataSet.Tables["aDivisions"];
            }
        }
        #endregion

        #region Статистика прихода/расхода и сальдо (баланса) кошельков с участием внутрикорповых переводов
        public DataTable TableCorpWalletsInOutWithWithdrawals
        {
            get
            {
                if (!mDataSet.Tables.Contains("mCorpWalletsInOutWithWithdrawals"))
                {
                    mDataSet.Tables.Add("mCorpWalletsInOutWithWithdrawals");
                    TableCorpWalletsInOutWithWithdrawals.Columns.Add("accountKey", typeof(int));
                    TableCorpWalletsInOutWithWithdrawals.Columns.Add("divisionName", typeof(string));
                    TableCorpWalletsInOutWithWithdrawals.Columns.Add("moneyIn", typeof(double));
                    TableCorpWalletsInOutWithWithdrawals.Columns.Add("moneyOut", typeof(double));
                    TableCorpWalletsInOutWithWithdrawals.Columns.Add("balance", typeof(double));//сальдо по-русски
                }
                return mDataSet.Tables["mCorpWalletsInOutWithWithdrawals"];
            }
        }
        public void GetCorpWalletsInOutStatisticWithWithdrawal(int characterId, DateTime top, DateTime low)
        {
            //CheckOpen();
            GetCorporationSheet(characterId);
            TableCorpWalletsInOutWithWithdrawals.Rows.Clear();
            double totalIn = 0, totalOut = 0;
            foreach (DataRow divName in TableWalletDivisions.Rows)
            {
                TableCorpWalletsInOutWithWithdrawals.Rows.Add(divName["accountKey"], divName["description"], 0, 0, 0);
            }
            OleDbDataReader reader;
            OleDbCommand cmdSel;
            //получение прихода
            cmdSel = new OleDbCommand(
                String.Format(
                    "select accountKey, sum(amount) as moneyIn from aCorpWalletJournal where amount > 0 and userId = {0} and " +
                    "([date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) group by accountKey order by accountKey;",
                    characterId, low, top),
                connectDB);
            reader = cmdSel.ExecuteReader();
            while (reader.Read())
            {
                int accountKey = Convert.ToInt32(reader["accountKey"]);
                double moneyIn = Convert.ToDouble(reader["moneyIn"]);
                DataRow[] rows = TableCorpWalletsInOutWithWithdrawals.Select(String.Format("accountKey = {0}", accountKey));
                if (rows.Length == 1)
                {
                    rows[0]["moneyIn"] = moneyIn;
                    totalIn += moneyIn;
                }
            }
            reader.Close();
            //получение расхода
            cmdSel = new OleDbCommand(
                String.Format(
                    "select accountKey, sum(amount) as moneyOut from aCorpWalletJournal where amount < 0 and userId = {0} and " +
                    "([date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) group by accountKey order by accountKey;",
                    characterId, low, top),
                connectDB);
            reader = cmdSel.ExecuteReader();
            while (reader.Read())
            {
                int accountKey = Convert.ToInt32(reader["accountKey"]);
                double moneyOut = Convert.ToDouble(reader["moneyOut"]);
                DataRow[] rows = TableCorpWalletsInOutWithWithdrawals.Select(String.Format("accountKey = {0}", accountKey));
                if (rows.Length == 1)
                {
                    rows[0]["moneyOut"] = moneyOut;
                    totalOut += moneyOut;
                }
            }
            foreach (DataRow row in TableCorpWalletsInOutWithWithdrawals.Rows)
            {
                double moneyIn = Convert.ToDouble(row["moneyIn"]);
                double moneyOut = Convert.ToDouble(row["moneyOut"]);
                row["balance"] = moneyIn + moneyOut;
            }
            reader.Close();
            TableCorpWalletsInOutWithWithdrawals.Rows.Add(1, "*** Total ***", totalIn, totalOut, totalIn + totalOut);
            //CheckClose();
        }
        #endregion
        #region Статистика прихода/расхода и сальдо (баланса) кошельков без участия внутрикорповых переводов
        public DataTable TableCorpWalletsInOutWithoutWithdrawals
        {
            get
            {
                if (!mDataSet.Tables.Contains("mCorpWalletsInOutWithoutWithdrawals"))
                {
                    mDataSet.Tables.Add("mCorpWalletsInOutWithoutWithdrawals");
                    TableCorpWalletsInOutWithoutWithdrawals.Columns.Add("accountKey", typeof(int));
                    TableCorpWalletsInOutWithoutWithdrawals.Columns.Add("divisionName", typeof(string));
                    TableCorpWalletsInOutWithoutWithdrawals.Columns.Add("moneyIn", typeof(double));
                    TableCorpWalletsInOutWithoutWithdrawals.Columns.Add("moneyOut", typeof(double));
                    TableCorpWalletsInOutWithoutWithdrawals.Columns.Add("balance", typeof(double));//сальдо по-русски
                }
                return mDataSet.Tables["mCorpWalletsInOutWithoutWithdrawals"];
            }
        }
        public void GetCorpWalletsInOutStatisticWithoutWithdrawal(int characterId, DateTime top, DateTime low)
        {
            //CheckOpen();
            GetCorporationSheet(characterId);
            TableCorpWalletsInOutWithoutWithdrawals.Rows.Clear();
            double totalIn = 0, totalOut = 0;
            foreach (DataRow divName in TableWalletDivisions.Rows)
            {
                TableCorpWalletsInOutWithoutWithdrawals.Rows.Add(divName["accountKey"], divName["description"], 0, 0, 0);
            }
            OleDbDataReader reader;
            OleDbCommand cmdSel;
            //получение прихода
            cmdSel = new OleDbCommand(
                String.Format(
                    "select accountKey, sum(amount) as moneyIn from aCorpWalletJournal where amount > 0 and userId = {0} and " +
                    "([date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) and refTypeId <> 37 group by accountKey order by accountKey;",
                    characterId, low, top),
                connectDB);
            reader = cmdSel.ExecuteReader();
            while (reader.Read())
            {
                int accountKey = Convert.ToInt32(reader["accountKey"]);
                double moneyIn = Convert.ToDouble(reader["moneyIn"]);
                DataRow[] rows = TableCorpWalletsInOutWithoutWithdrawals.Select(String.Format("accountKey = {0}", accountKey));
                if (rows.Length == 1)
                {
                    rows[0]["moneyIn"] = moneyIn;
                    totalIn += moneyIn;
                }
            }
            reader.Close();
            //получение расхода
            cmdSel = new OleDbCommand(
                String.Format(
                    "select accountKey, sum(amount) as moneyOut from aCorpWalletJournal where amount < 0 and userId = {0} and " +
                    "([date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) and refTypeId <> 37 group by accountKey order by accountKey;",
                    characterId, low, top),
                connectDB);
            reader = cmdSel.ExecuteReader();
            while (reader.Read())
            {
                int accountKey = Convert.ToInt32(reader["accountKey"]);
                double moneyOut = Convert.ToDouble(reader["moneyOut"]);
                DataRow[] rows = TableCorpWalletsInOutWithoutWithdrawals.Select(String.Format("accountKey = {0}", accountKey));
                if (rows.Length == 1)
                {
                    rows[0]["moneyOut"] = moneyOut;
                    totalOut += moneyOut;
                }
            }
            foreach (DataRow row in TableCorpWalletsInOutWithoutWithdrawals.Rows)
            {
                double moneyIn = Convert.ToDouble(row["moneyIn"]);
                double moneyOut = Convert.ToDouble(row["moneyOut"]);
                row["balance"] = moneyIn + moneyOut;
            }
            reader.Close();
            TableCorpWalletsInOutWithoutWithdrawals.Rows.Add(1, "*** Total ***", totalIn, totalOut, totalIn + totalOut);
            //CheckClose();
        }
        #endregion
        #region Статистика прихода/расхода и сальдо (баланса) кошельков по выбранному типу перевода
        public DataTable TableCorpWalletsInOutByType
        {
            get
            {
                if (!mDataSet.Tables.Contains("mCorpWalletsInOutByType"))
                {
                    mDataSet.Tables.Add("mCorpWalletsInOutByType");
                    TableCorpWalletsInOutByType.Columns.Add("accountKey", typeof(int));
                    TableCorpWalletsInOutByType.Columns.Add("divisionName", typeof(string));
                    TableCorpWalletsInOutByType.Columns.Add("moneyIn", typeof(double));
                    TableCorpWalletsInOutByType.Columns.Add("moneyOut", typeof(double));
                    TableCorpWalletsInOutByType.Columns.Add("balance", typeof(double));//сальдо по-русски
                }
                return mDataSet.Tables["mCorpWalletsInOutByType"];
            }
        }
        public void GetCorpWalletsInOutStatisticByType(int characterId, DateTime top, DateTime low, int refTypeId)
        {
            //CheckOpen();
            GetCorporationSheet(characterId);
            TableCorpWalletsInOutByType.Rows.Clear();
            double totalIn = 0, totalOut = 0;
            foreach (DataRow divName in TableWalletDivisions.Rows)
            {
                TableCorpWalletsInOutByType.Rows.Add(divName["accountKey"], divName["description"], 0, 0, 0);
            }
            OleDbDataReader reader;
            OleDbCommand cmdSel;
            //получение прихода
            cmdSel = new OleDbCommand(
                String.Format(
                    "select accountKey, sum(amount) as moneyIn from aCorpWalletJournal where amount > 0 and userId = {0} and " +
                    "([date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) and refTypeId = {3} group by accountKey order by accountKey;",
                    characterId, low, top, refTypeId),
                connectDB);
            reader = cmdSel.ExecuteReader();
            while (reader.Read())
            {
                int accountKey = Convert.ToInt32(reader["accountKey"]);
                double moneyIn = Convert.ToDouble(reader["moneyIn"]);
                DataRow[] rows = TableCorpWalletsInOutByType.Select(String.Format("accountKey = {0}", accountKey));
                if (rows.Length == 1)
                {
                    rows[0]["moneyIn"] = moneyIn;
                    totalIn += moneyIn;
                }
            }
            reader.Close();
            //получение расхода
            cmdSel = new OleDbCommand(
                String.Format(
                    "select accountKey, sum(amount) as moneyOut from aCorpWalletJournal where amount < 0 and userId = {0} and " +
                    "([date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) and refTypeId = {3} group by accountKey order by accountKey;",
                    characterId, low, top, refTypeId),
                connectDB);
            reader = cmdSel.ExecuteReader();
            while (reader.Read())
            {
                int accountKey = Convert.ToInt32(reader["accountKey"]);
                double moneyOut = Convert.ToDouble(reader["moneyOut"]);
                DataRow[] rows = TableCorpWalletsInOutByType.Select(String.Format("accountKey = {0}", accountKey));
                if (rows.Length == 1)
                {
                    rows[0]["moneyOut"] = moneyOut;
                    totalOut += moneyOut;
                }
            }
            foreach (DataRow row in TableCorpWalletsInOutByType.Rows)
            {
                double moneyIn = Convert.ToDouble(row["moneyIn"]);
                double moneyOut = Convert.ToDouble(row["moneyOut"]);
                row["balance"] = moneyIn + moneyOut;
            }
            reader.Close();
            TableCorpWalletsInOutByType.Rows.Add(1, "*** Total ***", totalIn, totalOut, totalIn + totalOut);
            //CheckClose();
        }
        #endregion
        #region Статистика прихода/расхода и сальдо (баланса) всех кошельков сразу, сгруппировано по типам переводов
        public DataTable TableCorpWalletsInOutGroupByType
        {
            get
            {
                if (!mDataSet.Tables.Contains("mCorpWalletsInOutGroupByType"))
                {
                    mDataSet.Tables.Add("mCorpWalletsInOutGroupByType");
                    TableCorpWalletsInOutGroupByType.Columns.Add("refTypeId", typeof(int));
                    TableCorpWalletsInOutGroupByType.Columns.Add("refTypeName", typeof(string));
                    TableCorpWalletsInOutGroupByType.Columns.Add("moneyIn", typeof(double));
                    TableCorpWalletsInOutGroupByType.Columns.Add("moneyOut", typeof(double));
                    TableCorpWalletsInOutGroupByType.Columns.Add("balance", typeof(double));//сальдо по-русски
                    TableCorpWalletsInOutGroupByType.Columns["moneyIn"].DefaultValue = 0;
                    TableCorpWalletsInOutGroupByType.Columns["moneyOut"].DefaultValue = 0;
                    TableCorpWalletsInOutGroupByType.Columns["balance"].DefaultValue = 0;
                }
                return mDataSet.Tables["mCorpWalletsInOutGroupByType"];
            }
        }
        public void GetCorpWalletsInOutStatisticGroupByType(int characterId, DateTime top, DateTime low)
        {
            //CheckOpen();
            TableCorpWalletsInOutGroupByType.Rows.Clear();
            //GetRefTypesList();
            //foreach (DataRow refType in TableRefTypes.Rows)
            //{
            //    DataRow newRow = TableCorpWalletsInOutGroupByType.NewRow();
            //    newRow["refTypeId"] = refType["refTypeId"];
            //    newRow["refTypeName"] = refType["refTypeName"];
            //    newRow["moneyIn"] = 0;
            //    newRow["moneyOut"] = 0;
            //    newRow["balance"] = 0;
            //    TableCorpWalletsInOutGroupByType.Rows.Add(newRow);
            //}
            double totalIn = 0, totalOut = 0;
            OleDbDataReader reader;
            OleDbCommand cmdSel;
            //получение прихода
            cmdSel = new OleDbCommand(
                String.Format(
                /*
                    SELECT aCorpWalletJournal.refTypeId, aRefTypes.refTypeName, Sum(aCorpWalletJournal.[amount]) AS moneyIn
                    FROM aCorpWalletJournal LEFT JOIN aRefTypes ON aCorpWalletJournal.refTypeID = aRefTypes.refTypeID
                    WHERE (((aCorpWalletJournal.amount)>0))
                    GROUP BY aCorpWalletJournal.refTypeId, aRefTypes.refTypeName;
*/
                //"select accountKey, sum(amount) as moneyIn from aCorpWalletJournal where amount > 0 and userId = {0} and " +
                //"([date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) and refTypeId = {3} group by accountKey order by accountKey;",
                    "SELECT aCorpWalletJournal.refTypeId, aRefTypes.refTypeName, Sum(aCorpWalletJournal.[amount]) AS moneyIn " +
                    "from aCorpWalletJournal LEFT JOIN aRefTypes ON aCorpWalletJournal.refTypeID = aRefTypes.refTypeID " +
                    "where (((aCorpWalletJournal.amount)>0)) and aCorpWalletJournal.userId = {0} and " +
                    "(aCorpWalletJournal.[date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) " +
                    "group by aCorpWalletJournal.refTypeId, aRefTypes.refTypeName;",
                    characterId, low, top),
                connectDB);
            reader = cmdSel.ExecuteReader();
            while (reader.Read())
            {
                int refTypeId = Convert.ToInt32(reader["refTypeId"]);
                double moneyIn = Convert.ToDouble(reader["moneyIn"]);
                DataRow[] rows = TableCorpWalletsInOutGroupByType.Select(String.Format("refTypeId = {0}", refTypeId));
                totalIn += moneyIn;
                if (rows.Length == 1)
                {
                    rows[0]["moneyIn"] = moneyIn;
                }
                else
                {
                    DataRow newRow = TableCorpWalletsInOutGroupByType.NewRow();
                    newRow["refTypeId"] = reader["refTypeId"];
                    newRow["refTypeName"] = reader["refTypeName"];
                    newRow["moneyIn"] = reader["moneyIn"];
                    TableCorpWalletsInOutGroupByType.Rows.Add(newRow);
                }
            }
            reader.Close();
            //получение расхода
            cmdSel = new OleDbCommand(
                String.Format(
                    "SELECT aCorpWalletJournal.refTypeId, aRefTypes.refTypeName, Sum(aCorpWalletJournal.[amount]) AS moneyOut " +
                    "from aCorpWalletJournal LEFT JOIN aRefTypes ON aCorpWalletJournal.refTypeID = aRefTypes.refTypeID " +
                    "where (((aCorpWalletJournal.amount)<0)) and aCorpWalletJournal.userId = {0} and " +
                    "(aCorpWalletJournal.[date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) " +
                    "group by aCorpWalletJournal.refTypeId, aRefTypes.refTypeName;",
                    characterId, low, top),
                connectDB);
            reader = cmdSel.ExecuteReader();
            while (reader.Read())
            {
                int refTypeId = Convert.ToInt32(reader["refTypeId"]);
                double moneyOut = Convert.ToDouble(reader["moneyOut"]);
                DataRow[] rows = TableCorpWalletsInOutGroupByType.Select(String.Format("refTypeId = {0}", refTypeId));
                totalOut += moneyOut;
                if (rows.Length == 1)
                {
                    rows[0]["moneyOut"] = moneyOut;
                }
                else
                {
                    DataRow newRow = TableCorpWalletsInOutGroupByType.NewRow();
                    newRow["refTypeId"] = reader["refTypeId"];
                    newRow["refTypeName"] = reader["refTypeName"];
                    newRow["moneyOut"] = reader["moneyOut"];
                    TableCorpWalletsInOutGroupByType.Rows.Add(newRow);
                }
            }
            foreach (DataRow row in TableCorpWalletsInOutGroupByType.Rows)
            {
                double moneyIn = Convert.ToDouble(row["moneyIn"]);
                double moneyOut = Convert.ToDouble(row["moneyOut"]);
                row["balance"] = moneyIn + moneyOut;
            }
            reader.Close();
            TableCorpWalletsInOutGroupByType.Rows.Add(0, "*** Total ***", totalIn, totalOut, totalIn + totalOut);
            //CheckClose();
        }
        #endregion
        #region Статистика баунти по выбранному корпмемберу
        public DataTable TableBountyByMember
        {
            get
            {
                if (!mDataSet.Tables.Contains("mBountyByMember"))
                {
                    mDataSet.Tables.Add("mBountyByMember");
                }
                return mDataSet.Tables["mBountyByMember"];
            }
        }
        public void GetBountyByMember(int characterId, DateTime top, DateTime low)
        {
            //CheckOpen();
            TableBountyByMember.Rows.Clear();

            //получение доходов с миссий
            string selQuery =
                String.Format(
                    "select Sum(amount) as sumAmount, ownerName2 FROM aCorpWalletJournal WHERE (refTypeId = 17 or refTypeId = 33 or refTypeId = 34 or refTypeId = 85) and userId = {0} and ([date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) group by ownerName2 order by sum(amount) desc;",
                    characterId, low, top);
            OleDbDataAdapter moddaBountyByMember = new OleDbDataAdapter(selQuery, connectDB);
            moddaBountyByMember.Fill(TableBountyByMember);
            //CheckClose();
        }
        #endregion

        #region переводы между двумя кошельками
        public DataTable TableCorpWalletWithdrawals
        {
            get
            {
                if (!mDataSet.Tables.Contains("mCorpWalletWithdrawals"))
                {
                    mDataSet.Tables.Add("mCorpWalletWithdrawals");
                    TableCorpWalletWithdrawals.Columns.Add("accountKeyFrom", typeof(int));
                    TableCorpWalletWithdrawals.Columns.Add("divisionNameFrom", typeof(string));
                    TableCorpWalletWithdrawals.Columns.Add("accountKeyTo", typeof(int));
                    TableCorpWalletWithdrawals.Columns.Add("divisionNameTo", typeof(string));
                    TableCorpWalletWithdrawals.Columns.Add("amount", typeof(double));
                }
                return mDataSet.Tables["mCorpWalletWithdrawals"];
            }
        }
        private void GetWithdrawalsBetweenAccounts(int characterId, DateTime top, DateTime low, int accountKey1, int accountKey2, out double from1to2, out double from2to1)
        {
            List<string> refs1 = new List<string>();
            OleDbCommand cmdSel;
            OleDbDataReader reader;
            //CheckOpen();
            cmdSel = new OleDbCommand(
                String.Format(
                    "select refId from aCorpWalletJournal where accountKey = {0} and refTypeId = 37 and " +
                    "[date] >= #{1:MM'/'dd'/'yyyy HH:mm}# and [date] <= #{2:MM'/'dd'/'yyyy HH:mm}# and userId = {3}",
                    accountKey1, low, top, characterId),
                connectDB);
            reader = cmdSel.ExecuteReader();
            while (reader.Read())
            {
                refs1.Add(Convert.ToString(reader["refId"]));
            }
            reader.Close();
            from1to2 = from2to1 = 0;
            if (refs1.Count > 0)
            {
                cmdSel = new OleDbCommand(
                    String.Format(
                        "select sum(amount) from aCorpWalletJournal where accountKey = {0} and refTypeId = 37 and " +
                        "[date] >= #{1:MM'/'dd'/'yyyy HH:mm}# and [date] <= #{2:MM'/'dd'/'yyyy HH:mm}# and refId in ({3}) " +
                        "and amount > 0 and userId = {4}",
                        accountKey2, low, top, String.Join(", ", refs1.ToArray()), characterId),
                    connectDB);
                object result = cmdSel.ExecuteScalar();
                if (result.GetType() != typeof(DBNull))
                    from1to2 = Convert.ToDouble(result);

                cmdSel = new OleDbCommand(
                String.Format(
                    "select sum(amount) from aCorpWalletJournal where accountKey = {0} and refTypeId = 37 and " +
                    "[date] >= #{1:MM'/'dd'/'yyyy HH:mm}# and [date] <= #{2:MM'/'dd'/'yyyy HH:mm}# and refId in ({3}) " +
                    "and amount < 0 and userId = {4}",
                    accountKey2, low, top, String.Join(", ", refs1.ToArray()), characterId),
                connectDB);
                result = cmdSel.ExecuteScalar();
                if (result.GetType() != typeof(DBNull))
                    from2to1 = Convert.ToDouble(result);
            }
            //CheckClose();
        }
        public void GetWithdrawalsBetweenAccounts(int characterId, DateTime top, DateTime low)
        {
            TableCorpWalletWithdrawals.Rows.Clear();
            for (int i = 0; i < TableWalletDivisions.Rows.Count; i++)
            {
                DataRow row1 = TableWalletDivisions.Rows[i];
                for (int j = 0; j < TableWalletDivisions.Rows.Count; j++)
                {
                    if (i != j)
                    {
                        DataRow row2 = TableWalletDivisions.Rows[j];
                        double from1to2 = 0, from2to1 = 0;
                        GetWithdrawalsBetweenAccounts(
                            characterId, top, low,
                            Convert.ToInt32(row1["accountKey"]), Convert.ToInt32(row2["accountKey"]),
                            out from1to2, out from2to1);
                        TableCorpWalletWithdrawals.Rows.Add(
                            row1["accountKey"], row1["description"],
                            row2["accountKey"], row2["description"],
                            from1to2);
                        //TableCorpWalletWithdrawals.Rows.Add(
                        //    row2["accountKey"], row2["divisionName"],
                        //    row1["accountKey"], row1["divisionName"],
                        //    from2to1);
                    }
                }
            }
        }
        #endregion

        #region CharWalletTransactions
        public DataTable TableCharWalletTransactions
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCharWalletTransactions"))
                {
                    mDataSet.Tables.Add("aCharWalletTransactions");
                }
                return mDataSet.Tables["aCharWalletTransactions"];
            }
        }
        private OleDbDataAdapter moddaCharWalletTransactions;
        public bool GetCharWalletTransactions(int characterId, string accountKey, DateTime top, DateTime low)
        {
            //CheckOpen();
            bool result = false;
            TableCharWalletTransactions.Clear();
            moddaCharWalletTransactions = new OleDbDataAdapter(
                String.Format(
                    "select *, price*quantity as price_total from aCharWalletTransactions where userId = {0} and accountKey = {1} and ([transactionDateTime] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#);",
                    characterId, accountKey, low, top),
                connectDB);
            moddaCharWalletTransactions.RowUpdating += new OleDbRowUpdatingEventHandler(moddaCharWalletTransactions_RowUpdating);
            mCommandBuilder = new OleDbCommandBuilder(moddaCharWalletTransactions);
            mCommandBuilder.QuotePrefix = "[";
            mCommandBuilder.QuoteSuffix = "]";
            //OleDbCommand cmd = mCommandBuilder.GetInsertCommand();

            moddaCharWalletTransactions.Fill(mDataSet, "aCharWalletTransactions");
            TableCharWalletTransactions.Columns["userId"].DefaultValue = characterId;
            TableCharWalletTransactions.Columns["accountKey"].DefaultValue = accountKey;
            //CheckClose();
            return result;
        }

        private void moddaCharWalletTransactions_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public bool IsCharWalletTransactionsRecordExists(int characterId, string accountKey, DataRow row)
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand(
                String.Format(
                    "select count (*) from aCharWalletTransactions where userId = {0} and accountKey = {1} and transactionDateTime = #{2:MM'/'dd'/'yyyy HH:mm}# and transactionID = {3}",
                    characterId, accountKey, row["transactionDateTime"], row["transactionID"]),
                connectDB);
            int count = (int)cmd.ExecuteScalar();
            //CheckClose();
            return (count > 0);
        }
        public int CommitCharWalletTransactions()
        {
            int result = -1;
            try
            {
                result = moddaCharWalletTransactions.Update(TableCharWalletTransactions);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        public void ClearCharWalletTransactions()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCharWalletTransactions", connectDB);
            cmd.ExecuteNonQuery();
            TableCharWalletTransactions.Clear();
            //CheckClose();
        }

        public DataTable TableCharWalletTransactionsForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCharWalletTransactionsForView"))
                {
                    mDataSet.Tables.Add("aCharWalletTransactionsForView");
                }
                return mDataSet.Tables["aCharWalletTransactionsForView"];
            }
        }
        public bool GetCharWalletTransactionsForView(int characterId, string accountKey, DateTime top, DateTime low)
        {
            //CheckOpen();
            bool result = false;
            TableCharWalletTransactionsForView.Clear();
            OleDbDataAdapter moddaCharWalletTransactionsForView = new OleDbDataAdapter(
                String.Format(
                    "select transactionId, transactionDateTime, quantity, price, typeName, price*quantity as price_total, clientName, characterName, stationName, transactionType, transactionFor from aCharWalletTransactions where userId = {0} and accountKey = {1} and ([transactionDateTime] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#);",
                    characterId, accountKey, low, top),
                connectDB);

            moddaCharWalletTransactionsForView.Fill(mDataSet, "aCharWalletTransactionsForView");
            //CheckClose();
            return result;
        }
        #endregion

        #region CorpWalletTransactions
        public DataTable TableCorpWalletTransactions
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpWalletTransactions"))
                {
                    mDataSet.Tables.Add("aCorpWalletTransactions");
                }
                return mDataSet.Tables["aCorpWalletTransactions"];
            }
        }
        private OleDbDataAdapter moddaCorpWalletTransactions;
        public bool GetCorpWalletTransactions(int characterId, string accountKey, DateTime top, DateTime low)
        {
            //CheckOpen();
            bool result = false;
            TableCorpWalletTransactions.Clear();
            moddaCorpWalletTransactions = new OleDbDataAdapter(
                String.Format(
                    "select *, price*quantity as price_total from aCorpWalletTransactions where userId = {0} and accountKey = {1} and ([transactionDateTime] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#);",
                    characterId, accountKey, low, top),
                connectDB);
            moddaCorpWalletTransactions.RowUpdating += new OleDbRowUpdatingEventHandler(moddaCorpWalletTransactions_RowUpdating);
            mCommandBuilder = new OleDbCommandBuilder(moddaCorpWalletTransactions);
            mCommandBuilder.QuotePrefix = "[";
            mCommandBuilder.QuoteSuffix = "]";
            //OleDbCommand cmd = mCommandBuilder.GetInsertCommand();

            moddaCorpWalletTransactions.Fill(mDataSet, "aCorpWalletTransactions");
            TableCorpWalletTransactions.Columns["userId"].DefaultValue = characterId;
            TableCorpWalletTransactions.Columns["accountKey"].DefaultValue = accountKey;
            //CheckClose();
            return result;
        }

        private void moddaCorpWalletTransactions_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public bool IsCorpWalletTransactionsRecordExists(int characterId, string accountKey, DataRow row)
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand(
                String.Format(
                    "select count (*) from aCorpWalletTransactions where userId = {0} and accountKey = {1} and transactionDateTime = #{2:MM'/'dd'/'yyyy HH:mm}# and transactionId = {3}",
                    characterId, accountKey, row["transactionDateTime"], row["transactionId"]),
                connectDB);
            int count = (int)cmd.ExecuteScalar();
            //CheckClose();
            return (count > 0);
        }
        public int CommitCorpWalletTransactions()
        {
            int result = -1;
            try
            {
                result = moddaCorpWalletTransactions.Update(TableCorpWalletTransactions);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        public void ClearCorpWalletTransactions()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCorpWalletTransactions", connectDB);
            cmd.ExecuteNonQuery();
            TableCorpWalletTransactions.Clear();
            //CheckClose();
        }

        public DataTable TableCorpWalletTransactionsForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpWalletTransactionsForView"))
                {
                    mDataSet.Tables.Add("aCorpWalletTransactionsForView");
                }
                return mDataSet.Tables["aCorpWalletTransactionsForView"];
            }
        }
        public bool GetCorpWalletTransactionsForView(int characterId, string accountKey, DateTime top, DateTime low)
        {
            //CheckOpen();
            bool result = false;
            TableCorpWalletTransactionsForView.Clear();
            OleDbDataAdapter moddaCorpWalletTransactionsForView = new OleDbDataAdapter(
                String.Format(
                    "select transactionId, transactionDateTime, quantity, price, typeName, price*quantity as price_total, clientName, characterName, stationName, transactionType, transactionFor from aCorpWalletTransactions where userId = {0} and accountKey = {1} and ([transactionDateTime] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#);",
                    characterId, accountKey, low, top),
                connectDB);

            moddaCorpWalletTransactionsForView.Fill(mDataSet, "aCorpWalletTransactionsForView");
            //CheckClose();
            return result;
        }
        #endregion

        #region Cache
        public bool CacheIsUpdateAllowed(int characterId, string url, out DateTime cachedUntil)
        {
            bool result = false;
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand(
                String.Format("select count(*) from aCache where userId = {0} and uri = '{1}'",
                    characterId, url),
                connectDB);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            cachedUntil = DateTime.MinValue;
            if (count == 0)
            {
                result = true;
            }
            else
            {
                cmd = new OleDbCommand(
                    String.Format("select cachedUntil from aCache where userId = {0} and uri = '{1}'",
                        characterId, url),
                    connectDB);
                cachedUntil = Convert.ToDateTime(cmd.ExecuteScalar());
                if (cachedUntil < DateTime.UtcNow)
                    result = true;
            }
            //CheckClose();
            //return true;
            return result;
        }
        public void CacheAdd(int characterId, string url, DateTime currentTime, DateTime cachedUntil)
        {
            try
            {
                //CheckOpen();
                OleDbCommand cmd = new OleDbCommand(
                    String.Format("select count(*) from aCache where userId = {0} and uri = '{1}'",
                        characterId, url),
                    connectDB);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                {
                    cmd = new OleDbCommand(
                        String.Format("insert into aCache (userId, uri, cached, cachedUntil) values ({0}, '{1}', '{2}', '{3}');",
                            characterId, url, currentTime, cachedUntil),
                        connectDB);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new OleDbCommand(
                        String.Format("update aCache set cached = '{2}', cachedUntil = '{3}' where userId = {0} and uri = '{1}';",
                            characterId, url, currentTime, cachedUntil),
                        connectDB);
                    cmd.ExecuteNonQuery();
                }
                //CheckClose();
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
        public DateTime CacheGetLastWalletUpdateTime(int characterId)
        {
            DateTime result = DateTime.MinValue;
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand(
                String.Format(
                    "select max(cachedUntil) from aCache where uri like '%wallet%' and userId = {0}",
                    characterId),
                connectDB);
            object obj = cmd.ExecuteScalar();
            if (obj != null && obj.GetType() != typeof(DBNull))
            {
                result = Convert.ToDateTime(obj);
            }
            //CheckClose();
            return result;
        }
        public void ClearCache()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCache", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        #endregion

        #region RequestsLog
        public DataTable TableRequestsLog
        {
            get
            {
                if (!mDataSet.Tables.Contains("aRequestsLog"))
                {
                    mDataSet.Tables.Add("aRequestsLog");
                    TableRequestsLog.RowChanged += new DataRowChangeEventHandler(aRequestsLog_RowChanged);
                    TableRequestsLog.RowDeleted += new DataRowChangeEventHandler(aRequestsLog_RowChanged);
                }
                return mDataSet.Tables["aRequestsLog"];
            }
        }
        private OleDbDataAdapter moddaRequestsLog;
        private bool mRequestsLogUpdating;
        public bool GetRequestsLog(int characterId, DateTime top, DateTime low)
        {
            //CheckOpen();
            bool result = false;
            mRequestsLogUpdating = true;
            TableRequestsLog.Clear();
            moddaRequestsLog = new OleDbDataAdapter(
                String.Format(
                    "select * from aRequestsLog where userId = {0} and ([date] between #{1:MM'/'dd'/'yyyy HH:mm}# and #{2:MM'/'dd'/'yyyy HH:mm}#) order by [date] desc;",
                    characterId, low, top),
                connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaRequestsLog);
            mCommandBuilder.QuotePrefix = "[";
            mCommandBuilder.QuoteSuffix = "]";
            //OleDbCommand cmd = mCommandBuilder.GetInsertCommand();

            moddaRequestsLog.Fill(mDataSet, "aRequestsLog");
            TableRequestsLog.Columns["userId"].DefaultValue = characterId;
            //CheckClose();
            mRequestsLogUpdating = false;
            return result;
        }
        private void aRequestsLog_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (!mRequestsLogUpdating)
            {
                try
                {
                    switch (e.Action)
                    {
                        case DataRowAction.Add:
                        case DataRowAction.Change:
                        case DataRowAction.Delete:
                            moddaRequestsLog.Update(TableRequestsLog);
                            break;
                    }
                }
                catch (Exception exc)
                {
                    ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
                }
            }
        }
        public void ClearRequestsLog()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aRequestsLog", connectDB);
            cmd.ExecuteNonQuery();
            TableRequestsLog.Clear();
            //CheckClose();
        }
        #endregion

        #region CorporationSheet
        public void SaveCorporationSheet(int characterId, string sheet)
        {
            OleDbCommand cmd = new OleDbCommand(String.Format("select count(*) from aCorporationSheet where userId = {0};", characterId),
                connectDB);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count == 0)
            {
                sheet = CodeEscapes(sheet);
                string query = String.Format("insert into aCorporationSheet (userId, sheet) values ({0}, '{1}');", characterId, sheet);
                cmd = new OleDbCommand(String.Format("insert into aCorporationSheet (userId, sheet) values ({0}, '{1}');", characterId, sheet), connectDB);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new OleDbCommand(String.Format("update aCorporationSheet set sheet = '{1}' where userid = {0};", characterId, CodeEscapes(sheet)), connectDB);
                cmd.ExecuteNonQuery();
            }
        }
        public void GetCorporationSheet(int characterId)
        {
            OleDbCommand cmd = new OleDbCommand(String.Format(
                "select sheet from aCorporationSheet where userId = {0}", characterId),
                connectDB);
            object obj = cmd.ExecuteScalar();
            if (obj == null)
                mCorporationSheetInfo.Clear();
            else
            {
                string str = Convert.ToString(obj);
                XmlDocument xmlDoc = new XmlDocument();
                str = DecodeEscapes(str);
                xmlDoc.LoadXml(str);
                mCorporationSheetInfo.Init(xmlDoc);

                TableWalletDivisions.Rows.Clear();
                foreach (DataRow row in mCorporationSheetInfo.WalletDivisions.Rows)
                {
                    TableWalletDivisions.ImportRow(row);
                }

                TableDivisions.Rows.Clear();
                foreach (DataRow row in mCorporationSheetInfo.Divisions.Rows)
                {
                    TableDivisions.ImportRow(row);
                }
            }
        }
        private CorporationSheetInfo mCorporationSheetInfo;
        public CorporationSheetInfo CorporationSheetInfo
        {
            get
            {
                if (mCorporationSheetInfo == null)
                {
                    mCorporationSheetInfo = new CorporationSheetInfo();
                }
                return mCorporationSheetInfo;
            }
        }
        public void ClearCorporationSheet()
        {
            OleDbCommand cmd = new OleDbCommand("delete from aCorporationSheet;", connectDB);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region CorpStarbaseList
        public DataTable TableCorpStarbaseList
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpStarbaseList"))
                {
                    mDataSet.Tables.Add("aCorpStarbaseList");
                }
                return mDataSet.Tables["aCorpStarbaseList"];
            }
        }
        public DataTable TableCorpStarbaseDetailFuel
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpStarbaseDetailFuel"))
                {
                    mDataSet.Tables.Add("aCorpStarbaseDetailFuel");
                    mDataSet.Tables["aCorpStarbaseDetailFuel"].Columns.Add("typeID", typeof(Int32));
                    mDataSet.Tables["aCorpStarbaseDetailFuel"].Columns.Add("quantity", typeof(Int32));
                    mDataSet.Tables["aCorpStarbaseDetailFuel"].Columns.Add("typeName", typeof(String));
                }
                return mDataSet.Tables["aCorpStarbaseDetailFuel"];
            }
        }
        private OleDbDataAdapter moddaCorpStarbaseList;
        public bool GetCorpStarbaseList(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableCorpStarbaseList.Clear();
            moddaCorpStarbaseList = new OleDbDataAdapter(String.Format("select * from aCorpStarbaseList where userId = {0};", characterId), connectDB);
            moddaCorpStarbaseList.RowUpdating += new OleDbRowUpdatingEventHandler(moddaCorpStarbaseList_RowUpdating);
            mCommandBuilder = new OleDbCommandBuilder(moddaCorpStarbaseList);

            moddaCorpStarbaseList.Fill(mDataSet, "aCorpStarbaseList");
            TableCorpStarbaseList.Columns["userId"].DefaultValue = characterId;
            //CheckClose();
            return result;
        }

        public DataTable TableCorpStarbaseListForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpStarbaseListForView"))
                {
                    mDataSet.Tables.Add("aCorpStarbaseListForView");
                }
                return mDataSet.Tables["aCorpStarbaseListForView"];
            }
        }
        public bool GetCorpStarbaseListForView(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableCorpStarbaseListForView.Clear();
            OleDbDataAdapter moddaCorpStarbaseListForView = new OleDbDataAdapter(
                String.Format(
                    "SELECT aCorpStarbaseList.itemId, aEveNames4.itemName AS regionName, aEveNames5.itemName AS constellationName, aCorpStarbaseList.locationID, aEveNames2.itemName AS locationName, aCorpStarbaseList.moonID, aEveNames3.itemName AS moonName, aCorpStarbaseList.typeID, aInvTypes1.typeName, aCorpStarbaseList.state, aCorpStarbaseList.stateTimestamp, aCorpStarbaseList.onlineTimestamp, aCorpStarbaseList.starbaseDetail, aCorpStarbaseList.cpu, aCorpStarbaseList.power " +
                    "FROM (((((aCorpStarbaseList LEFT JOIN aEveNames AS aEveNames2 ON aCorpStarbaseList.locationID = aEveNames2.itemId) LEFT JOIN aEveNames AS aEveNames3 ON aCorpStarbaseList.moonID = aEveNames3.itemId) INNER JOIN aInvTypes AS aInvTypes1 ON aCorpStarbaseList.typeID = aInvTypes1.typeId) LEFT JOIN aMapSolarSystems ON aCorpStarbaseList.locationID = aMapSolarSystems.solarSystemID) LEFT JOIN aEveNames AS aEveNames4 ON aMapSolarSystems.regionId = aEveNames4.itemId) LEFT JOIN aEveNames AS aEveNames5 ON aMapSolarSystems.constellationID = aEveNames5.itemId " +
                    "WHERE (((aCorpStarbaseList.userId)={0})) " +
                    "ORDER BY aEveNames3.itemName;",
                //"select transactionId, transactionDateTime, quantity, price, typeName, price*quantity as price_total, clientName, characterName, stationName, transactionType, transactionFor from aCorpWalletTransactions where userId = {0} and accountKey = {1} and ([transactionDateTime] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#);",
                    characterId),
                connectDB);

            moddaCorpStarbaseListForView.Fill(mDataSet, "aCorpStarbaseListForView");
            //CheckClose();
            return result;
        }

        private void moddaCorpStarbaseList_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public void ClearCorpStarbaseList()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCorpStarbaseList;", connectDB);
            cmd.ExecuteNonQuery();
            TableCorpStarbaseList.Clear();
            //GetCorpStarbaseList();
            //CheckClose();
        }
        public int CommitCorpStarbaseList()
        {
            int result = -1;
            try
            {
                result = moddaCorpStarbaseList.Update(TableCorpStarbaseList);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        StarbaseDetail mStarbaseDetail = new StarbaseDetail();
        public StarbaseDetail StarbaseDetail
        {
            get { return mStarbaseDetail; }
        }
        public void GetCorpStarbaseDetail(XmlDocument doc)
        {
            mStarbaseDetail.Init(doc);
            TableCorpStarbaseDetailFuel.Rows.Clear();
            foreach (DataRow row in mStarbaseDetail.Fuel.Rows)
            {
                int typeId = Convert.ToInt32(row["typeId"]);
                {
                    OleDbCommand cmd = new OleDbCommand(
                        String.Format("select typeName from aInvTypes where typeId = {0};", typeId),
                        connectDB);
                    object obj = cmd.ExecuteScalar();
                    string str = "";
                    if (obj != null)
                    {
                        str = Convert.ToString(obj);
                    }
                    row["typeName"] = str;
                }
                TableCorpStarbaseDetailFuel.ImportRow(row);
            }
        }
        #endregion

        #region CorpStarbaseStructures
        public DataTable TableCorpStarbaseStructures
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpStarbaseStructures"))
                {
                    mDataSet.Tables.Add("aCorpStarbaseStructures");
                }
                return mDataSet.Tables["aCorpStarbaseStructures"];
            }
        }
        private OleDbDataAdapter moddaCorpStarbaseStructures;
        public bool GetCorpStarbaseStructures(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableCorpStarbaseStructures.Clear();
            moddaCorpStarbaseStructures = new OleDbDataAdapter(String.Format("select * from aCorpStarbaseStructures where userId = {0};", characterId), connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaCorpStarbaseStructures);

            moddaCorpStarbaseStructures.Fill(mDataSet, "aCorpStarbaseStructures");
            mDataSet.Tables["aCorpStarbaseStructures"].Columns["userId"].DefaultValue = characterId;
            moddaCorpStarbaseStructures.RowUpdating += new OleDbRowUpdatingEventHandler(moddaCorpStarbaseStructures_RowUpdating);
            //CheckClose();
            return result;
        }

        void moddaCorpStarbaseStructures_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                if (e.Row["recordId"] is DBNull)
                    e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public void ClearCorpStarbaseStructures()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCorpStarbaseStructures", connectDB);
            cmd.ExecuteNonQuery();
            TableCorpStarbaseStructures.Clear();
            //GetCorpStarbaseStructures();
            //CheckClose();
        }
        public void ClearCorpStarbaseStructures(int characterId)
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand(String.Format("delete from aCorpStarbaseStructures where userId = {0}", characterId), connectDB);
            cmd.ExecuteNonQuery();
            //TableCorpStarbaseStructures.Clear();
            GetCorpStarbaseStructures(characterId);
            //CheckClose();
        }
        public int CommitCorpStarbaseStructures()
        {
            int result = -1;
            try
            {
                result = moddaCorpStarbaseStructures.Update(TableCorpStarbaseStructures);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        #endregion

        #region AssetList
        public DataTable TableAssetList
        {
            get
            {
                if (!mDataSet.Tables.Contains("aAssetList"))
                {
                    mDataSet.Tables.Add("aAssetList");
                }
                return mDataSet.Tables["aAssetList"];
            }
        }
        private OleDbDataAdapter moddaAssetList;
        public bool GetAssetList(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableAssetList.Clear();
            moddaAssetList = new OleDbDataAdapter(String.Format("select * from aAssetList where userId = {0};", characterId), connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaAssetList);

            moddaAssetList.Fill(mDataSet, "aAssetList");
            mDataSet.Tables["aAssetList"].Columns["userId"].DefaultValue = characterId;

            //CheckClose();
            return result;
        }
        public void ClearAssetList()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aAssetList", connectDB);
            cmd.ExecuteNonQuery();
            TableAssetList.Clear();
            //GetAssetList();
            //CheckClose();
        }
        public void ClearAssetList(int characterId, bool isCorp)
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand(String.Format("delete from aAssetList where userId = {0} and isCorp = {1}", characterId, isCorp), connectDB);
            cmd.ExecuteNonQuery();
            //TableAssetList.Clear();
            GetAssetList(characterId);
            //CheckClose();
        }
        public int CommitAssetList()
        {
            int result = -1;
            try
            {
                result = moddaAssetList.Update(TableAssetList);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }

        public DataTable TableAssetListForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aAssetListForView"))
                {
                    mDataSet.Tables.Add("aAssetListForView");
                }
                return mDataSet.Tables["aAssetListForView"];
            }
        }
        /// <summary>
        /// Выборка списка разных мест наличия вещей и принадлежности к корпе/частному
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public bool GetAssetListForView(int characterId, bool isCorp)
        {
            //CheckOpen();
            bool result = false;
            TableAssetListForView.Clear();
            TableAssetListForView.Columns.Clear();
            //TableAssetListForView.Reset();
            OleDbDataAdapter moddaAssetListForView = new OleDbDataAdapter(
                //"select aSovereignty.solarSystemId, aSovereignty.solarSystemName, aSovereignty.allianceId, aAlliances.name from aSovereignty left join aAlliances on (aSovereignty.allianceId=aAlliances.allianceId)",
                String.Format(
                    "SELECT DISTINCT aAssetList.isCorp, aAssetList.locationId, aEveNames.itemName as locationName " +
                    "FROM aAssetList LEFT JOIN aEveNames ON aAssetList.locationId = aEveNames.itemId " +
                    "where userId = {0} and isCorp = {1};",
                    characterId, isCorp),
                connectDB);
            moddaAssetListForView.Fill(mDataSet, "aAssetListForView");
            TableAssetListForView.Columns.Add("isCorpImage", typeof(Image));

            foreach (DataRow row in TableAssetListForView.Rows)
            {
                if (Convert.ToBoolean(row["isCorp"]) == true)
                    row["isCorpImage"] = InternalResources.IsCorp;
                else
                    row["isCorpImage"] = InternalResources.Blank;
            }
            //CheckClose();
            return result;
        }
        public bool GetAssetListForView(int characterId, int locationId, bool isCorp)
        {
            //CheckOpen();
            bool result = false;
            TableAssetListForView.Clear();
            TableAssetListForView.Columns.Clear();
            //TableAssetListForView.Reset();
            OleDbDataAdapter moddaAssetListForView = new OleDbDataAdapter(
                //"select aSovereignty.solarSystemId, aSovereignty.solarSystemName, aSovereignty.allianceId, aAlliances.name from aSovereignty left join aAlliances on (aSovereignty.allianceId=aAlliances.allianceId)",
                String.Format(
                //"SELECT aAssetList.recordId, aAssetList.parentRecordId, aAssetList.hasChild, aAssetList.isCorp, aAssetList.itemId, aAssetList.locationId, aEveNames2.itemName as locationName, aAssetList.typeId, aInvTypes1.typeName as typeName, aAssetList.quantity, aAssetList.flag, aAssetList.singleton " +
                //"FROM (aAssetList LEFT JOIN aInvTypes AS aInvTypes1 ON aAssetList.typeId = aInvTypes1.typeId) LEFT JOIN aEveNames AS aEveNames2 ON aAssetList.locationId = aEveNames2.itemId " +
                    "SELECT aAssetList.recordId, aAssetList.parentRecordId, aAssetList.hasChild, aAssetList.isCorp, aAssetList.itemId, aAssetList.locationId, aEveNames2.itemName AS locationName, aAssetList.typeId, aInvTypes1.typeName AS typeName, aAssetList.quantity, aAssetList.flag, aAssetList.singleton, aInvFlags.flagText " +
                    "FROM ((aAssetList LEFT JOIN aInvTypes AS aInvTypes1 ON aAssetList.typeId = aInvTypes1.typeId) LEFT JOIN aEveNames AS aEveNames2 ON aAssetList.locationId = aEveNames2.itemId) LEFT JOIN aInvFlags ON aAssetList.flag = aInvFlags.flagID " +
                    "where userId = {0} and parentRecordId = '00000000-0000-0000-0000-000000000000' and locationId = {1} and isCorp = {2};",
                    characterId, locationId, isCorp),
                connectDB);

            moddaAssetListForView.Fill(mDataSet, "aAssetListForView");

            TableAssetListForView.Columns.Add("isCorpImage", typeof(Image));
            TableAssetListForView.Columns.Add("hasChildImage", typeof(Image));

            foreach (DataRow row in TableAssetListForView.Rows)
            {
                if (Convert.ToBoolean(row["isCorp"]) == true)
                    row["isCorpImage"] = InternalResources.IsCorp;
                else
                    row["isCorpImage"] = InternalResources.Blank;

                if (Convert.ToBoolean(row["hasChild"]) == true)
                    row["hasChildImage"] = InternalResources.HasChild;
                else
                    row["hasChildImage"] = InternalResources.Blank;
            }
            //CheckClose();
            return result;
        }
        public bool GetAssetListForView_StructuresInLocation(int characterId, int locationId)
        {
            //CheckOpen();
            bool result = false;
            TableAssetListForView.Clear();
            TableAssetListForView.Columns.Clear();
            //TableAssetListForView.Reset();
            OleDbDataAdapter moddaAssetListForView = new OleDbDataAdapter(
                //"select aSovereignty.solarSystemId, aSovereignty.solarSystemName, aSovereignty.allianceId, aAlliances.name from aSovereignty left join aAlliances on (aSovereignty.allianceId=aAlliances.allianceId)",
                String.Format(
                    "SELECT aAssetList.recordId, aAssetList.itemId, aAssetList.locationId, aEveNames2.itemName AS locationName, aAssetList.typeId, aInvTypes1.typeName AS typeName " +
                    "FROM (((aAssetList LEFT JOIN aInvTypes AS aInvTypes1 ON aAssetList.typeId = aInvTypes1.typeId) LEFT JOIN aEveNames AS aEveNames2 ON aAssetList.locationId = aEveNames2.itemId) LEFT JOIN aInvFlags ON aAssetList.flag = aInvFlags.flagID) LEFT JOIN aInvGroups ON aInvTypes1.groupID = aInvGroups.groupId " +
                    "WHERE (((aAssetList.locationId)={1}) AND ((aAssetList.userId)={0}) AND ((aAssetList.flag)=0) AND ((aInvGroups.categoryID)=23));",
                    characterId, locationId),
                connectDB);

            moddaAssetListForView.Fill(mDataSet, "aAssetListForView");

            //TableAssetListForView.Columns.Add("isCorpImage", typeof(Image));
            //TableAssetListForView.Columns.Add("hasChildImage", typeof(Image));

            //foreach (DataRow row in TableAssetListForView.Rows)
            //{
            //    if (Convert.ToBoolean(row["isCorp"]) == true)
            //        row["isCorpImage"] = InternalResources.IsCorp;
            //    else
            //        row["isCorpImage"] = InternalResources.Blank;

            //    if (Convert.ToBoolean(row["hasChild"]) == true)
            //        row["hasChildImage"] = InternalResources.HasChild;
            //    else
            //        row["hasChildImage"] = InternalResources.Blank;
            //}
            //CheckClose();
            return result;
        }
        public bool GetAssetListForView(int characterId, Guid parentRecordId)
        {
            //CheckOpen();
            bool result = false;
            TableAssetListForView.Clear();
            TableAssetListForView.Columns.Clear();
            //TableAssetListForView.Reset();
            OleDbDataAdapter moddaAssetListForView = new OleDbDataAdapter(
                //"select aSovereignty.solarSystemId, aSovereignty.solarSystemName, aSovereignty.allianceId, aAlliances.name from aSovereignty left join aAlliances on (aSovereignty.allianceId=aAlliances.allianceId)",
                String.Format(
                //"SELECT aAssetList.recordId, aAssetList.parentRecordId, aAssetList.hasChild, aAssetList.isCorp, aAssetList.itemId, aAssetList.locationId, aEveNames2.itemName as locationName, aAssetList.typeId, aInvTypes1.typeName as typeName, aAssetList.quantity, aAssetList.flag, aAssetList.singleton " +
                //"FROM (aAssetList LEFT JOIN aInvTypes AS aInvTypes1 ON aAssetList.typeId = aInvTypes1.typeId) LEFT JOIN aEveNames AS aEveNames2 ON aAssetList.locationId = aEveNames2.itemId " +
                    "SELECT aAssetList.recordId, aAssetList.parentRecordId, aAssetList.hasChild, aAssetList.isCorp, aAssetList.itemId, aAssetList.locationId, aEveNames2.itemName AS locationName, aAssetList.typeId, aInvTypes1.typeName AS typeName, aAssetList.quantity, aAssetList.flag, aAssetList.singleton, aInvFlags.flagText " +
                    "FROM ((aAssetList LEFT JOIN aInvTypes AS aInvTypes1 ON aAssetList.typeId = aInvTypes1.typeId) LEFT JOIN aEveNames AS aEveNames2 ON aAssetList.locationId = aEveNames2.itemId) LEFT JOIN aInvFlags ON aAssetList.flag = aInvFlags.flagID " +
                    "where userId = {0} and parentRecordId = '{1}';",
                    characterId, parentRecordId),
                connectDB);

            moddaAssetListForView.Fill(mDataSet, "aAssetListForView");

            TableAssetListForView.Columns.Add("isCorpImage", typeof(Image));
            TableAssetListForView.Columns.Add("hasChildImage", typeof(Image));

            foreach (DataRow row in TableAssetListForView.Rows)
            {
                if (Convert.ToBoolean(row["isCorp"]) == true)
                    row["isCorpImage"] = InternalResources.IsCorp;
                else
                    row["isCorpImage"] = InternalResources.Blank;

                if (Convert.ToBoolean(row["hasChild"]) == true)
                    row["hasChildImage"] = InternalResources.HasChild;
                else
                    row["hasChildImage"] = InternalResources.Blank;
            }
            //CheckClose();
            return result;
        }
        public Guid GetParentIdForRecordId(int characterId, Guid rowId)
        {
            OleDbCommand cmd = new OleDbCommand(
                String.Format(
                    "select first(parentRecordId) from aAssetList where recordId = '{1}' and userId = {0}",
                    characterId, rowId),
                connectDB);
            object obj = cmd.ExecuteScalar();
            Guid result = Guid.Empty;
            try
            {
                result = new Guid(Convert.ToString(obj));
            }
            catch
            {
                result = Guid.Empty;
            }
            return result;
        }
        #endregion

        #region CharIndustryJobs
        public DataTable TableCharIndustryJobs
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCharIndustryJobs"))
                {
                    mDataSet.Tables.Add("aCharIndustryJobs");
                }
                return mDataSet.Tables["aCharIndustryJobs"];
            }
        }
        private OleDbDataAdapter moddaCharIndustryJobs;
        public bool GetCharIndustryJobs(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableCharIndustryJobs.Clear();
            moddaCharIndustryJobs = new OleDbDataAdapter(String.Format("select * from aCharIndustryJobs where userId = {0}", characterId), connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaCharIndustryJobs);
            moddaCharIndustryJobs.RowUpdating += new OleDbRowUpdatingEventHandler(moddaCharIndustryJobs_RowUpdating);

            moddaCharIndustryJobs.Fill(mDataSet, "aCharIndustryJobs");
            TableCharIndustryJobs.Columns["userId"].DefaultValue = characterId;

            //CheckClose();
            return result;
        }

        void moddaCharIndustryJobs_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public void ClearCharIndustryJobs()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCharIndustryJobs", connectDB);
            cmd.ExecuteNonQuery();
            TableCharIndustryJobs.Clear();
            //GetAlliancesList();
            //CheckClose();
        }
        public int CommitCharIndustryJobs()
        {
            int result = -1;
            try
            {
                result = moddaCharIndustryJobs.Update(TableCharIndustryJobs);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        #endregion

        #region CorpIndustryJobs
        public DataTable TableCorpIndustryJobs
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpIndustryJobs"))
                {
                    mDataSet.Tables.Add("aCorpIndustryJobs");
                }
                return mDataSet.Tables["aCorpIndustryJobs"];
            }
        }
        private OleDbDataAdapter moddaCorpIndustryJobs;
        public bool GetCorpIndustryJobs(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableCorpIndustryJobs.Clear();
            moddaCorpIndustryJobs = new OleDbDataAdapter(String.Format("select * from aCorpIndustryJobs where userId = {0}", characterId), connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaCorpIndustryJobs);
            moddaCorpIndustryJobs.RowUpdating += new OleDbRowUpdatingEventHandler(moddaCorpIndustryJobs_RowUpdating);

            moddaCorpIndustryJobs.Fill(mDataSet, "aCorpIndustryJobs");
            TableCorpIndustryJobs.Columns["userId"].DefaultValue = characterId;

            //CheckClose();
            return result;
        }

        void moddaCorpIndustryJobs_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public void ClearCorpIndustryJobs()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aCorpIndustryJobs", connectDB);
            cmd.ExecuteNonQuery();
            TableCorpIndustryJobs.Clear();
            //GetAlliancesList();
            //CheckClose();
        }
        public int CommitCorpIndustryJobs()
        {
            int result = -1;
            try
            {
                result = moddaCorpIndustryJobs.Update(TableCorpIndustryJobs);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }

        public DataTable TableCorpIndustryJobsForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpIndustryJobsForView"))
                {
                    mDataSet.Tables.Add("aCorpIndustryJobsForView");
                }
                return mDataSet.Tables["aCorpIndustryJobsForView"];
            }
        }
        public bool GetCorpIndustryJobsForView(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableCorpIndustryJobsForView.Clear();
            OleDbDataAdapter moddaCorpIndustryJobsForView = new OleDbDataAdapter(
                String.Format(
                    "select transactionId, transactionDateTime, quantity, price, typeName, price*quantity as price_total, clientName, characterName, stationName, transactionType, transactionFor from aCorpWalletTransactions where userId = {0} and accountKey = {1} and ([transactionDateTime] between #{2:MM'/'dd'/'yyyy HH:mm}# and #{3:MM'/'dd'/'yyyy HH:mm}#);",
                    characterId),
                connectDB);

            moddaCorpIndustryJobsForView.Fill(mDataSet, "aCorpIndustryJobsForView");
            //CheckClose();
            return result;
        }
        #endregion

        #region Kill Log
        public DataTable TableKillLogs
        {
            get
            {
                if (!mDataSet.Tables.Contains("aKillLog"))
                {
                    mDataSet.Tables.Add("aKillLog");
                }
                return mDataSet.Tables["aKillLog"];
            }
        }
        private OleDbDataAdapter moddaKillLogs;
        public bool GetKillLogs(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableKillLogs.Clear();
            moddaKillLogs = new OleDbDataAdapter(String.Format("select * from aKillLog where userId = {0}", characterId), connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaKillLogs);
            //moddaKillLogs.RowUpdating += new OleDbRowUpdatingEventHandler(moddaKillLogs_RowUpdating);

            moddaKillLogs.Fill(mDataSet, "aKillLog");
            TableKillLogs.Columns["userId"].DefaultValue = characterId;

            //CheckClose();
            return result;
        }
        public bool IsKillLogRecordExists(int characterId, int killId, bool isCorp)
        {
            bool result = false;
            OleDbCommand cmd = new OleDbCommand(
                String.Format(
                    "select count(*) from aKillLog where userId = {0} and isCorp = {1} and killId = {2};",
                    characterId, isCorp, killId),
                connectDB);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0)
                result = true;
            return result;
        }
        public void ClearKillLogs()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aKillLog", connectDB);
            cmd.ExecuteNonQuery();
            TableKillLogs.Clear();
            //GetAlliancesList();
            //CheckClose();
        }
        public int CommitKillLogs()
        {
            int result = -1;
            try
            {
                result = moddaKillLogs.Update(TableKillLogs);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }

        public DataTable TableKillLogsForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aKillLogsForView"))
                {
                    mDataSet.Tables.Add("aKillLogsForView");
                }
                if (!mDataSet.Tables["aKillLogsForView"].Columns.Contains("victimImage"))
                {
                    mDataSet.Tables["aKillLogsForView"].Columns.Add("victimImage", typeof(Image));
                }
                if (!mDataSet.Tables["aKillLogsForView"].Columns.Contains("charFynalBlowImage"))
                {
                    mDataSet.Tables["aKillLogsForView"].Columns.Add("charFynalBlowImage", typeof(Image));
                }
                if (!mDataSet.Tables["aKillLogsForView"].Columns.Contains("victimShipImage"))
                {
                    mDataSet.Tables["aKillLogsForView"].Columns.Add("victimShipImage", typeof(Image));
                }
                return mDataSet.Tables["aKillLogsForView"];
            }
        }
        public bool GetKillLogsForView(int characterId, bool isCorp)
        {
            //CheckOpen();
            bool result = false;
            TableKillLogsForView.Clear();
            OleDbDataAdapter moddaKillLogsForView = new OleDbDataAdapter(
                String.Format(
                    "SELECT aKillLog.recordId, aKillLog.killId, aKillLog.killTime, aKillLogVictims.shipTypeId as victimShipId, aInvTypes.typeName+'\n'+aInvGroups.groupName AS shipType, aKillLogVictims.characterId as victimCharacterId, aKillLogVictims.characterName+'\n'+aKillLogVictims.corporationName+' ['+aKillLogVictims.allianceName+']' AS Victim, aKillLogAttackers.characterId as charFynalBlowId, aKillLogAttackers.characterName+'\n'+aKillLogAttackers.corporationName+' ['+aKillLogAttackers.allianceName+']' AS CharFynalBlow, aMapSolarSystems1.solarSystemName+'\n('+CStr(Round(aMapSolarSystems1.security,1))+')' AS SolarSystem " +
                    "FROM ((((aKillLog LEFT JOIN aKillLogVictims ON aKillLog.recordId = aKillLogVictims.parentRecordId) LEFT JOIN aInvTypes ON aKillLogVictims.shipTypeId = aInvTypes.typeId) LEFT JOIN aInvGroups ON aInvTypes.groupID = aInvGroups.groupId) LEFT JOIN aMapSolarSystems AS aMapSolarSystems1 ON aKillLog.solarSystemId = aMapSolarSystems1.solarSystemID) LEFT JOIN aKillLogAttackers ON aKillLog.recordId = aKillLogAttackers.parentRecordId " +
                //"SELECT aKillLog.recordId, aKillLog.killId, aKillLog.killTime, aInvTypes.typeName+' ('+aInvGroups.groupName+')' AS shipType, aKillLogVictims.characterName+' ('+aKillLogVictims.corporationName+') ['+aKillLogVictims.allianceName+']' AS Victim, aKillLogAttackers.characterName+'\n'+aKillLogAttackers.corporationName+' ['+aKillLogAttackers.allianceName+']' AS CharFynalBlow, aMapSolarSystems1.solarSystemName+'\n('+CStr(Round(aMapSolarSystems1.security,1))+')' AS SolarSystem " +
                //"FROM ((((aKillLog LEFT JOIN aKillLogVictims ON aKillLog.recordId = aKillLogVictims.parentRecordId) LEFT JOIN aInvTypes ON aKillLogVictims.shipTypeId = aInvTypes.typeId) LEFT JOIN aInvGroups ON aInvTypes.groupID = aInvGroups.groupId) LEFT JOIN aMapSolarSystems AS aMapSolarSystems1 ON aKillLog.solarSystemId = aMapSolarSystems1.solarSystemID) LEFT JOIN aKillLogAttackers ON aKillLog.recordId = aKillLogAttackers.parentRecordId " +
                    "WHERE (((aKillLogAttackers.finalBlow)=True)) and userId = {0} and isCorp = {1} " +
                    "ORDER BY aKillLog.killTime DESC;",
                    characterId, isCorp),
                connectDB);

            moddaKillLogsForView.Fill(mDataSet, "aKillLogsForView");
            //CheckClose();
            return result;
        }
        #endregion
        #region Kill Log Attackers
        public DataTable TableKillLogAttackers
        {
            get
            {
                if (!mDataSet.Tables.Contains("aKillLogAttackers"))
                {
                    mDataSet.Tables.Add("aKillLogAttackers");
                }
                return mDataSet.Tables["aKillLogAttackers"];
            }
        }
        private OleDbDataAdapter moddaKillLogAttackers;
        public bool GetKillLogAttackers(Guid parentRecordId)
        {
            //CheckOpen();
            bool result = false;
            TableKillLogAttackers.Clear();
            moddaKillLogAttackers = new OleDbDataAdapter(String.Format("select * from aKillLogAttackers where parentRecordId = '{0}'", parentRecordId), connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaKillLogAttackers);
            moddaKillLogAttackers.RowUpdating += new OleDbRowUpdatingEventHandler(moddaKillLogAttackers_RowUpdating);

            moddaKillLogAttackers.Fill(mDataSet, "aKillLogAttackers");
            TableKillLogAttackers.Columns["parentRecordId"].DefaultValue = parentRecordId;

            //CheckClose();
            return result;
        }

        private void moddaKillLogAttackers_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public void ClearKillLogAttackers()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aKillLogAttackers", connectDB);
            cmd.ExecuteNonQuery();
            TableKillLogAttackers.Clear();
            //GetAlliancesList();
            //CheckClose();
        }
        public int CommitKillLogAttackers()
        {
            int result = -1;
            try
            {
                result = moddaKillLogAttackers.Update(TableKillLogAttackers);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }

        public DataTable TableKillLogsAttackersForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aKillLogsAttackersForView"))
                {
                    mDataSet.Tables.Add("aKillLogsAttackersForView");
                }
                return mDataSet.Tables["aKillLogsAttackersForView"];
            }
        }
        public bool GetKillLogsAttackersForView(Guid parentRecordId)
        {
            //CheckOpen();
            bool result = false;
            TableKillLogsAttackersForView.Clear();
            OleDbDataAdapter moddaKillLogsAttackersForView = new OleDbDataAdapter(
                String.Format(
                    "SELECT aKillLogAttackers.characterId, aKillLogAttackers.characterName, aKillLogAttackers.corporationName, aKillLogAttackers.allianceName, aKillLogAttackers.securityStatus, aKillLogAttackers.damageDone, aKillLogAttackers.finalBlow, aInvTypes_1.typeName as weaponTypeName, aInvTypes.typeName as shipTypeName, aInvGroups.groupName as shipGroupName " +
                    "FROM (((aKillLog LEFT JOIN aKillLogAttackers ON aKillLog.recordId = aKillLogAttackers.parentRecordId) LEFT JOIN aInvTypes AS aInvTypes_1 ON aKillLogAttackers.weaponTypeId = aInvTypes_1.typeId) LEFT JOIN aInvTypes ON aKillLogAttackers.shipTypeId = aInvTypes.typeId) LEFT JOIN aInvGroups ON aInvTypes.groupID = aInvGroups.groupId " +
                    "WHERE (((aKillLog.recordId)='{0}')) " +
                    "ORDER BY aKillLogAttackers.finalBlow;",
                    parentRecordId),
                connectDB);

            moddaKillLogsAttackersForView.Fill(mDataSet, "aKillLogsAttackersForView");
            if (!TableKillLogsAttackersForView.Columns.Contains("charImage"))
            {
                TableKillLogsAttackersForView.Columns.Add("charImage", typeof(Image));
            }
            //CheckClose();
            return result;
        }
        #endregion
        #region Kill Log Items
        public DataTable TableKillLogItems
        {
            get
            {
                if (!mDataSet.Tables.Contains("aKillLogItems"))
                {
                    mDataSet.Tables.Add("aKillLogItems");
                }
                return mDataSet.Tables["aKillLogItems"];
            }
        }
        private OleDbDataAdapter moddaKillLogItems;
        public bool GetKillLogItems(Guid parentRecordId)
        {
            //CheckOpen();
            bool result = false;
            TableKillLogItems.Clear();
            moddaKillLogItems = new OleDbDataAdapter(String.Format("select * from aKillLogItems where parentRecordId = '{0}'", parentRecordId), connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaKillLogItems);
            moddaKillLogItems.RowUpdating += new OleDbRowUpdatingEventHandler(moddaKillLogItems_RowUpdating);

            moddaKillLogItems.Fill(mDataSet, "aKillLogItems");
            TableKillLogItems.Columns["parentRecordId"].DefaultValue = parentRecordId;

            //CheckClose();
            return result;
        }

        private void moddaKillLogItems_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public void ClearKillLogItems()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aKillLogItems", connectDB);
            cmd.ExecuteNonQuery();
            TableKillLogItems.Clear();
            //GetAlliancesList();
            //CheckClose();
        }
        public int CommitKillLogItems()
        {
            int result = -1;
            try
            {
                result = moddaKillLogItems.Update(TableKillLogItems);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }

        public DataTable TableKillLogsItemsForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aKillLogsItemsForView"))
                {
                    mDataSet.Tables.Add("aKillLogsItemsForView");
                }
                return mDataSet.Tables["aKillLogsItemsForView"];
            }
        }
        public bool GetKillLogsItemsForView(Guid parentRecordId)
        {
            //CheckOpen();
            bool result = false;
            TableKillLogsItemsForView.Clear();
            OleDbDataAdapter moddaKillLogsItemsForView = new OleDbDataAdapter(
                String.Format(
                    "SELECT aKillLogItems.typeId, aInvTypes_1.typeName, aKillLogItems.flag, aInvFlags.flagText, aInvGroups.groupName, aKillLogItems.qtyDropped, aKillLogItems.qtyDestroyed " +
                    "FROM (((aKillLog LEFT JOIN aKillLogItems ON aKillLog.recordId = aKillLogItems.parentRecordId) LEFT JOIN aInvTypes AS aInvTypes_1 ON aKillLogItems.typeId = aInvTypes_1.typeId) LEFT JOIN aInvFlags ON aKillLogItems.flag = aInvFlags.flagID) LEFT JOIN aInvGroups ON aInvTypes_1.groupID = aInvGroups.groupId " +
                    "WHERE (((aKillLog.recordId)='{0}')) " +
                    "ORDER BY aKillLogItems.flag;",
                    parentRecordId),
                connectDB);

            moddaKillLogsItemsForView.Fill(mDataSet, "aKillLogsItemsForView");
            if (!TableKillLogsItemsForView.Columns.Contains("itemImage"))
            {
                TableKillLogsItemsForView.Columns.Add("itemImage", typeof(Image));
            }
            if (!TableKillLogsItemsForView.Columns.Contains("slotTypeImage"))
            {
                TableKillLogsItemsForView.Columns.Add("slotTypeImage", typeof(Image));
            }
            if (!TableKillLogsItemsForView.Columns.Contains("slotType"))
            {
                TableKillLogsItemsForView.Columns.Add("slotType", typeof(SlotType));
            }
            //CheckClose();
            return result;
        }
        #endregion
        #region Kill Log Victims
        public DataTable TableKillLogVictims
        {
            get
            {
                if (!mDataSet.Tables.Contains("aKillLogVictims"))
                {
                    mDataSet.Tables.Add("aKillLogVictims");
                }
                return mDataSet.Tables["aKillLogVictims"];
            }
        }
        private OleDbDataAdapter moddaKillLogVictims;
        public bool GetKillLogVictims(Guid parentRecordId)
        {
            //CheckOpen();
            bool result = false;
            TableKillLogVictims.Clear();
            moddaKillLogVictims = new OleDbDataAdapter(String.Format("select * from aKillLogVictims where parentRecordId = '{0}'", parentRecordId), connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaKillLogVictims);
            moddaKillLogVictims.RowUpdating += new OleDbRowUpdatingEventHandler(moddaKillLogVictims_RowUpdating);

            moddaKillLogVictims.Fill(mDataSet, "aKillLogVictims");
            TableKillLogVictims.Columns["parentRecordId"].DefaultValue = parentRecordId;

            //CheckClose();
            return result;
        }

        private void moddaKillLogVictims_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public void ClearKillLogVictims()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aKillLogVictims", connectDB);
            cmd.ExecuteNonQuery();
            TableKillLogVictims.Clear();
            //GetAlliancesList();
            //CheckClose();
        }
        public int CommitKillLogVictims()
        {
            int result = -1;
            try
            {
                result = moddaKillLogVictims.Update(TableKillLogVictims);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }

        public DataTable TableKillLogsVictimsForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aKillLogsVictimsForView"))
                {
                    mDataSet.Tables.Add("aKillLogsVictimsForView");
                }
                return mDataSet.Tables["aKillLogsVictimsForView"];
            }
        }
        public bool GetKillLogsVictimsForView(Guid parentRecordId)
        {
            //CheckOpen();
            bool result = false;
            TableKillLogsVictimsForView.Clear();
            OleDbDataAdapter moddaKillLogsVictimsForView = new OleDbDataAdapter(
                String.Format(
                    "SELECT aKillLog.killTime, aKillLogVictims.characterId, aKillLogVictims.characterName, aKillLogVictims.corporationName, aKillLogVictims.allianceName, aKillLogVictims.damageTaken, aInvTypes.typeName as shipTypeName, aInvGroups.groupName as shipGroupName, aMapSolarSystems.solarSystemName, cstr(round(aMapSolarSystems.security, 1)) as security " +
                    "FROM (aKillLog LEFT JOIN ((aKillLogVictims LEFT JOIN aInvTypes ON aKillLogVictims.shipTypeId = aInvTypes.typeId) LEFT JOIN aInvGroups ON aInvTypes.groupID = aInvGroups.groupId) ON aKillLog.recordId = aKillLogVictims.parentRecordId) LEFT JOIN aMapSolarSystems ON aKillLog.solarSystemId = aMapSolarSystems.solarSystemID " +
                    "WHERE (((aKillLog.recordId)='{0}'));",
                    parentRecordId),
                connectDB);

            moddaKillLogsVictimsForView.Fill(mDataSet, "aKillLogsVictimsForView");
            if (!TableKillLogsVictimsForView.Columns.Contains("charImage"))
            {
                TableKillLogsVictimsForView.Columns.Add("charImage", typeof(Image));
            }
            //CheckClose();
            return result;
        }
        #endregion

        #region ItemAttributes
        public DataTable TableTypeAttributes
        {
            get
            {
                if (!mDataSet.Tables.Contains("aTypeAttributes"))
                {
                    mDataSet.Tables.Add("aTypeAttributes");
                }
                return mDataSet.Tables["aTypeAttributes"];
            }
        }
        public bool GetTypeAttributes(int typeId)
        {
            //CheckOpen();
            bool result = false;
            TableTypeAttributes.Clear();
            OleDbDataAdapter moddaTypeAttributes = new OleDbDataAdapter(
                String.Format(
                    "SELECT aDgmAttributeTypes.attributeId, aDgmAttributeTypes.attributeName, aDgmTypeAttributes.valueInt, aDgmTypeAttributes.valueFloat, aDgmAttributeTypes.description " +
                    "FROM (aInvTypes LEFT JOIN aDgmTypeAttributes ON aInvTypes.typeId=aDgmTypeAttributes.typeId) LEFT JOIN aDgmAttributeTypes ON aDgmTypeAttributes.attributeId=aDgmAttributeTypes.attributeId " +
                    "WHERE (((aInvTypes.typeId)={0})) " +
                    "ORDER BY aDgmAttributeTypes.attributeName;",
                    typeId),
                connectDB);

            moddaTypeAttributes.Fill(mDataSet, "aTypeAttributes");
            //CheckClose();
            return result;
        }
        public bool GetTypeCpuPower(int typeId, out int cpu, out int power)
        {
            bool result = false;
            cpu = 0;
            power = 0;
            try
            {
                OleDbCommand cmd = new OleDbCommand(
                    String.Format(
                    //"SELECT aDgmAttributeTypes.attributeName, aDgmTypeAttributes.valueInt " +
                    //"FROM (aInvTypes LEFT JOIN aDgmTypeAttributes ON aInvTypes.typeId = aDgmTypeAttributes.typeId) LEFT JOIN aDgmAttributeTypes ON aDgmTypeAttributes.attributeId = aDgmAttributeTypes.attributeId " +
                    //"WHERE (((aDgmAttributeTypes.attributeName) Like '%cpu%' Or (aDgmAttributeTypes.attributeName) Like '%power%') AND ((aInvTypes.typeId)={0})) " +
                    //"ORDER BY aDgmAttributeTypes.attributeName;",
                        "select aDgmTypeAttributes.valueInt, aDgmTypeAttributes.attributeId " +
                        "from aDgmTypeAttributes " +
                        "where aDgmTypeAttributes.typeId = {0} and aDgmTypeAttributes.attributeId in (11, 30, 48, 50);",
                        typeId),
                    connectDB);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int attributeId = Convert.ToInt32(reader["attributeId"]);
                    int attributeValue = Convert.ToInt32(reader["valueInt"]);
                    if (attributeId == 48 || attributeId == 50)
                        cpu = attributeValue;
                    if (attributeId == 11 || attributeId == 30)
                        power = attributeValue;
                }
                reader.Close();
                //OleDbDataAdapter adapter = new OleDbDataAdapter(
                //    String.Format(
                //        "SELECT aDgmAttributeTypes.attributeName, aDgmTypeAttributes.valueInt " +
                //        "FROM (aInvTypes LEFT JOIN aDgmTypeAttributes ON aInvTypes.typeId = aDgmTypeAttributes.typeId) LEFT JOIN aDgmAttributeTypes ON aDgmTypeAttributes.attributeId = aDgmAttributeTypes.attributeId " +
                //        "WHERE (((aDgmAttributeTypes.attributeName) Like '%cpu%' Or (aDgmAttributeTypes.attributeName) Like '%power%') AND ((aInvTypes.typeId)={0})) " +
                //        "ORDER BY aDgmAttributeTypes.attributeName;",
                //        typeId),
                //    connectDB);
                //DataTable dt = new DataTable();
                //adapter.Fill(dt);
                //foreach(DataRow row in dt.Rows)
                //{
                //    string attributeName = Convert.ToString(row["attributeName"]);
                //    int attributeValue = Convert.ToInt32(row["valueInt"]);
                //    if (attributeName.Contains("cpu"))
                //        cpu = attributeValue;
                //    if (attributeName.Contains("power"))
                //        power = attributeValue;
                //}
                result = true;
            }
            catch
            {
            }
            return result;
        }
        #endregion
        #region ControlTowerResources
        public DataTable TableControlTowerResources
        {
            get
            {
                if (!mDataSet.Tables.Contains("aControlTowerResources"))
                {
                    mDataSet.Tables.Add("aControlTowerResources");
                }
                return mDataSet.Tables["aControlTowerResources"];
            }
        }
        public bool GetControlTowerResources(int typeId)
        {
            //CheckOpen();
            bool result = false;
            TableControlTowerResources.Clear();
            OleDbDataAdapter moddaControlTowerResources = new OleDbDataAdapter(
                String.Format(
                    "SELECT aInvTypes.typeId, aInvControlTowerResourcePurposes.purposeText, aInvControlTowerResources.resourceTypeID, aInvTypes_1.typeName, aInvControlTowerResources.quantity, aInvTypes_1.volume " +
                    "FROM ((aInvTypes LEFT JOIN aInvControlTowerResources ON aInvTypes.typeId = aInvControlTowerResources.controlTowerTypeID) LEFT JOIN aInvTypes AS aInvTypes_1 ON aInvControlTowerResources.resourceTypeID = aInvTypes_1.typeId) LEFT JOIN aInvControlTowerResourcePurposes ON aInvControlTowerResources.purpose = aInvControlTowerResourcePurposes.purpose " +
                    "WHERE (((aInvTypes.typeId)={0})) " +
                    "ORDER BY aInvControlTowerResourcePurposes.purpose, aInvControlTowerResources.resourceTypeID;",
                    typeId),
                connectDB);

            moddaControlTowerResources.Fill(mDataSet, "aControlTowerResources");
            //CheckClose();
            return result;
        }
        #endregion
        #region ControlTowerFuelPrices
        public DataTable TableControlTowerFuelPrices
        {
            get
            {
                if (!mDataSet.Tables.Contains("aControlTowerFuelPrices"))
                {
                    mDataSet.Tables.Add("aControlTowerFuelPrices");
                }
                return mDataSet.Tables["aControlTowerFuelPrices"];
            }
        }
        private OleDbDataAdapter moddaControlTowerFuelPrices;
        public bool GetControlTowerFuelPrices(int characterId)
        {
            //CheckOpen();
            bool result = false;
            TableControlTowerFuelPrices.Clear();
            moddaControlTowerFuelPrices = new OleDbDataAdapter(String.Format("select * from aControlTowerFuelPrices where userId = {0}", characterId), connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaControlTowerFuelPrices);
            moddaControlTowerFuelPrices.RowUpdating += new OleDbRowUpdatingEventHandler(moddaControlTowerFuelPrices_RowUpdating);

            moddaControlTowerFuelPrices.Fill(mDataSet, "aControlTowerFuelPrices");
            TableControlTowerFuelPrices.Columns["userId"].DefaultValue = characterId;
            if (TableControlTowerFuelPrices.Rows.Count == 0)
            {
                //typeId    typeName
                //44	Enriched Uranium
                //3683  Oxygen
                //3689  Mechanical Parts
                //9832  Coolant
                //9848  Robotics
                //16274 Helium Isotopes
                //16272   Heavy Water
                //16273   Liquid Ozone
                //16275   Strontium Clathrates
                //17887 Oxygen Isotopes
                //17888 Nitrogen Isotopes
                //17889 Hydrogen Isotopes

                //24592 Amarr Empire Starbase Charter
                //24593 Caldari State Starbase Charter
                //24594 Gallente Federation Starbase Charter
                //24595 Minmatar Republic Starbase Charter
                //24596 Khanid Kingdom Starbase Charter
                //24597 Ammatar Mandate Starbase Charter
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 44, "Enriched Uranium", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 3683, "Oxygen", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 3689, "Mechanical Parts", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 9832, "Coolant", 0);

                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 9848, "Robotics", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 16272, "Heavy Water", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 16273, "Liquid Ozone", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 16274, "Helium Isotopes", 0);

                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 16275, "Strontium Clathrates", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 17887, "Oxygen Isotopes", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 17888, "Nitrogen Isotopes", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 17889, "Hydrogen Isotopes", 0);

                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 24592, "Amarr Empire Starbase Charter", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 24593, "Caldari State Starbase Charter", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 24594, "Gallente Federation Starbase Charter", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 24595, "Minmatar Republic Starbase Charter", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 24596, "Khanid Kingdom Starbase Charter", 0);
                TableControlTowerFuelPrices.Rows.Add(Guid.NewGuid(), characterId, 24597, "Ammatar Mandate Starbase Charter", 0);
                CommitControlTowerFuelPrices();
            }

            //CheckClose();
            return result;
        }

        void moddaControlTowerFuelPrices_RowUpdating(object sender, OleDbRowUpdatingEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                e.Row["recordId"] = Guid.NewGuid();
            }
        }
        public void ClearControlTowerFuelPrices()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aControlTowerFuelPrices", connectDB);
            cmd.ExecuteNonQuery();
            TableControlTowerFuelPrices.Clear();
            //GetAlliancesList();
            //CheckClose();
        }
        public int CommitControlTowerFuelPrices()
        {
            int result = -1;
            try
            {
                result = moddaControlTowerFuelPrices.Update(TableControlTowerFuelPrices);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        #endregion

        #region Общие данные
        #region CorpMemberTracking
        public DataTable TableCorpMemberTracking
        {
            get
            {
                if (!mDataSet.Tables.Contains("aCorpMemberTracking"))
                {
                    mDataSet.Tables.Add("aCorpMemberTracking");
                }
                return mDataSet.Tables["aCorpMemberTracking"];
            }
        }
        #endregion
        #region Alliances
        public DataTable TableAllianceList
        {
            get
            {
                if (!mDataSet.Tables.Contains("aAlliances"))
                {
                    mDataSet.Tables.Add("aAlliances");
                }
                return mDataSet.Tables["aAlliances"];
            }
        }
        private OleDbDataAdapter moddaAllianceList;
        public bool GetAlliancesList()
        {
            //CheckOpen();
            bool result = false;
            TableAllianceList.Clear();
            moddaAllianceList = new OleDbDataAdapter("select * from aAlliances", connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaAllianceList);

            moddaAllianceList.Fill(mDataSet, "aAlliances");

            //CheckClose();
            return result;
        }
        public void ClearAllianceList()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aAlliances", connectDB);
            cmd.ExecuteNonQuery();
            TableAllianceList.Clear();
            GetAlliancesList();
            //CheckClose();
        }
        public int CommitAllianceList()
        {
            int result = -1;
            try
            {
                result = moddaAllianceList.Update(TableAllianceList);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        #endregion
        #region Sovereignty
        public DataTable TableSovereignty
        {
            get
            {
                if (!mDataSet.Tables.Contains("aMapSovereignty"))
                {
                    mDataSet.Tables.Add("aMapSovereignty");
                }
                return mDataSet.Tables["aMapSovereignty"];
            }
        }
        private OleDbDataAdapter moddaSovereignty;
        public bool GetSovereigntyList()
        {
            //CheckOpen();
            bool result = false;
            TableSovereignty.Clear();
            moddaSovereignty = new OleDbDataAdapter("select * from aSovereignty", connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaSovereignty);

            moddaSovereignty.Fill(mDataSet, "aMapSovereignty");

            //CheckClose();
            return result;
        }
        public void ClearSovereigntyList()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aSovereignty", connectDB);
            cmd.ExecuteNonQuery();
            TableSovereignty.Clear();
            GetSovereigntyList();
            //CheckClose();
        }
        public int CommitSovereigntyList()
        {
            int result = -1;
            try
            {
                result = moddaSovereignty.Update(TableSovereignty);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }

        public DataTable TableSovereigntyListForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aMapSovereigntyForView"))
                {
                    mDataSet.Tables.Add("aMapSovereigntyForView");
                }
                return mDataSet.Tables["aMapSovereigntyForView"];
            }
        }
        public bool GetSovereigntyListForView()
        {
            //CheckOpen();
            bool result = false;
            TableSovereigntyListForView.Clear();
            OleDbDataAdapter moddaSovereigntyForView = new OleDbDataAdapter(
                //"select aSovereignty.solarSystemId, aSovereignty.solarSystemName, aSovereignty.allianceId, aAlliances.name from aSovereignty left join aAlliances on (aSovereignty.allianceId=aAlliances.allianceId)",

                //"SELECT aSovereignty.solarSystemID, aSovereignty.solarSystemName, all2.name as alliance, all3.name as constellationSovereignty, aSovereignty.sovereigntyLevel, aSovereignty.factionID, aEveNames1.itemName as factionName " +
                //"FROM ((aSovereignty LEFT JOIN aAlliances AS all3 ON aSovereignty.constellationSovereignty = all3.allianceID) LEFT JOIN aAlliances AS all2 ON aSovereignty.allianceID = all2.allianceID) LEFT JOIN aEveNames AS aEveNames1 ON aSovereignty.factionID = aEveNames1.itemId;",

                    "SELECT aSovereignty.solarSystemID, aSovereignty.solarSystemName, Round(aMapSolarSystems.security,1) AS security, all2.name AS alliance, all3.name AS constellationSovereignty, aSovereignty.sovereigntyLevel, aSovereignty.factionID, aEveNames1.itemName AS factionName " +
                    "FROM (((aSovereignty LEFT JOIN aAlliances AS all3 ON aSovereignty.constellationSovereignty = all3.allianceID) LEFT JOIN aAlliances AS all2 ON aSovereignty.allianceID = all2.allianceID) LEFT JOIN aEveNames AS aEveNames1 ON aSovereignty.factionID = aEveNames1.itemId) LEFT JOIN aMapSolarSystems ON aSovereignty.solarSystemID = aMapSolarSystems.solarSystemID;",
                connectDB);

            moddaSovereigntyForView.Fill(mDataSet, "aMapSovereigntyForView");
            //CheckClose();
            return result;
        }

        public bool GetSystemInfo(int solarSystemId, out string fractionName, out double security, out int sovereigntyLevel, out string allianceName)
        {
            bool result = false;
            fractionName = "none";
            security = 0;
            sovereigntyLevel = 0;
            allianceName = "none";
            try
            {
                OleDbCommand cmd = new OleDbCommand(
                    String.Format(
                        "SELECT Round(aMapSolarSystems.security,1) AS security, aEveNames1.itemName as fractionName, aSovereignty.sovereigntyLevel, aAlliances.name as allianceName  " +
                        "FROM ((aSovereignty LEFT JOIN aMapSolarSystems ON aSovereignty.solarSystemID = aMapSolarSystems.solarSystemID) LEFT JOIN aEveNames AS aEveNames1 ON aSovereignty.factionID = aEveNames1.itemId) LEFT JOIN aAlliances ON aSovereignty.allianceID = aAlliances.allianceID " +
                        "WHERE (((aSovereignty.solarSystemID)={0}));",
                        solarSystemId),
                    connectDB);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fractionName = Convert.ToString(reader["fractionName"]);
                    security = Convert.ToDouble(reader["security"]);
                    sovereigntyLevel = Convert.ToInt32(reader["sovereigntyLevel"]);
                    allianceName = Convert.ToString(reader["allianceName"]);
                }
                reader.Close();
                result = true;
            }
            catch
            {
            }
            return result;
        }
        #endregion
        #region RefTypes
        public DataTable TableRefTypes
        {
            get
            {
                if (!mDataSet.Tables.Contains("aRefTypes"))
                {
                    mDataSet.Tables.Add("aRefTypes");
                }
                return mDataSet.Tables["aRefTypes"];
            }
        }
        private OleDbDataAdapter moddaRefTypes;
        public bool GetRefTypesList()
        {
            //CheckOpen();
            bool result = false;
            TableRefTypes.Clear();
            if (moddaRefTypes != null)
            {
                moddaRefTypes.Dispose();
            }
            moddaRefTypes = new OleDbDataAdapter("select * from aRefTypes order by refTypeName;", connectDB);
            //moddaRefTypes.RowUpdated += new OleDbRowUpdatedEventHandler(moddaRefTypes_RowUpdated);
            mCommandBuilder = new OleDbCommandBuilder(moddaRefTypes);

            moddaRefTypes.Fill(mDataSet, "aRefTypes");

            //CheckClose();
            return result;
        }
        public int CommitRefTypes()
        {
            int result = -1;
            try
            {
                result = moddaRefTypes.Update(TableRefTypes);//вылетает при обновлении неизменной таблицы
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        public void ClearRefTypesList()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aRefTypes", connectDB);
            cmd.ExecuteNonQuery();
            TableRefTypes.Clear();
            //CheckClose();
            //GetRefTypesList();
        }
        #endregion
        #region InvTypes
        public DataTable TableInvTypes
        {
            get
            {
                if (!mDataSet.Tables.Contains("aInvTypes"))
                {
                    mDataSet.Tables.Add("aInvTypes");
                }
                return mDataSet.Tables["aInvTypes"];
            }
        }
        private OleDbDataAdapter moddaInvTypes;
        public bool GetInvTypes()
        {
            //CheckOpen();
            bool result = false;
            TableInvTypes.Clear();
            moddaInvTypes = new OleDbDataAdapter("select * from aInvTypes", connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaInvTypes);

            moddaInvTypes.Fill(mDataSet, "aInvTypes");

            //CheckClose();
            return result;
        }
        public void ClearInvTypes()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aInvTypes", connectDB);
            cmd.ExecuteNonQuery();
            TableInvTypes.Clear();
            //GetInvTypes();
            //CheckClose();
        }
        public int CommitInvTypes()
        {
            int result = -1;
            try
            {
                result = moddaInvTypes.Update(TableInvTypes);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }
        public bool ImportInvTypes(string fileName)
        {
            bool result = false;
            try
            {
                //путь к временному файлу
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=typeID Long");
                sw.WriteLine("Col2=groupID Long");
                sw.WriteLine("Col3=typeName Text Width 100");
                sw.WriteLine("Col4=description Memo");
                sw.WriteLine("Col5=graphicID Long");
                sw.WriteLine("Col6=radius Double");
                sw.WriteLine("Col7=mass Double");
                sw.WriteLine("Col8=volume Double");
                sw.WriteLine("Col9=capacity Double");
                sw.WriteLine("Col10=portionSize Long");
                sw.WriteLine("Col11=raceID Long");
                sw.WriteLine("Col12=basePrice Double");
                sw.WriteLine("Col13=published Long");
                sw.WriteLine("Col14=marketGroupID Long");
                sw.WriteLine("Col15=chanceOfDuplicating Double");
                sw.Close();

                //импортируем
                //insert into aInvTypes select * from tmpbase;
                string query = String.Format(
                    "insert into aInvTypes select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public Image GetInvTypeImage(int typeId, ImageSize size, bool allowLoadFromWeb)
        {
            Image result = null;
            string url = null;
            string str_size = "64_64";
            if (size == ImageSize.x256)
                str_size = "256_256";

            OleDbCommand cmd;
            object obj;
            ImageType type = ImageType.None;
            string icon = "";
            string fileName = Path.Combine(Options.SavedApiFiles, "images"); ;
            //получаем картинку
            {
                string categoryName = "";
                cmd = new OleDbCommand(
                    String.Format(
                        "select categoryName from aInvCategories where categoryId = (select categoryId from aInvGroups where groupId = (select groupId from aInvTypes where typeId = {0}));",
                        typeId),
                    connectDB);
                obj = cmd.ExecuteScalar();
                //if (obj != null && obj.GetType() != typeof(DBNull))
                if (obj != null && !(obj is DBNull))
                {
                    //категория этого итема
                    categoryName = Convert.ToString(obj);
                    //родительская группа маркета для этой группы
                    if (categoryName == "Ship")//"Ships"
                        type = ImageType.ShipType;
                    if (categoryName == "Structure")//"Starbase Structures"
                        type = ImageType.StructureType;
                    if (categoryName == "Drone")//"Drones" && != "Drone Upgrades"
                        type = ImageType.DroneType;
                }
            }
            if (type == ImageType.None)
            {
                cmd = new OleDbCommand(
                    String.Format(
                        "SELECT aEveGraphics.icon " +
                        "FROM aInvTypes LEFT JOIN aEveGraphics ON aInvTypes.graphicID = aEveGraphics.graphicId " +
                        "WHERE (((aInvTypes.typeId)={0}));",
                        typeId),
                    connectDB);
                obj = cmd.ExecuteScalar();
                //если имя иконки существует
                if (obj != null && obj.GetType() != typeof(DBNull))
                {
                    icon = Convert.ToString(obj);
                    type = ImageType.Item;
                }

            }
            switch (type)
            {
                case ImageType.Item:
                    url = String.Format("http://www.eve-online.com/bitmaps/icons/itemdb/black/{0}/icon{1}.png", str_size, icon);
                    fileName = Path.Combine(fileName, String.Format(@"bitmaps\icons\itemdb\black\{0}\icon{1}.png", str_size, icon));
                    break;
                case ImageType.DroneType:
                    url = String.Format("http://www.eve-online.com/bitmaps/icons/itemdb/dronetypes/{0}/{1}.png", str_size, typeId);
                    fileName = Path.Combine(fileName, String.Format(@"bitmaps\icons\itemdb\dronetypes\{0}\{1}.png", str_size, typeId));
                    break;
                case ImageType.ShipType:
                    url = String.Format("http://www.eve-online.com/bitmaps/icons/itemdb/shiptypes/{0}/{1}.png", str_size, typeId);
                    fileName = Path.Combine(fileName, String.Format(@"bitmaps\icons\itemdb\shiptypes\{0}\{1}.png", str_size, typeId));
                    break;
                case ImageType.StructureType:
                    url = String.Format("http://www.eve-online.com/bitmaps/icons/itemdb/structuretypes/{0}/{1}.png", str_size, typeId);
                    fileName = Path.Combine(fileName, String.Format(@"bitmaps\icons\itemdb\structuretypes\{0}\{1}.png", str_size, typeId));
                    break;
            }
            if (type != ImageType.None)
            {
                try
                {
                    if (File.Exists(fileName))
                    {
                        result = Image.FromFile(fileName);
                    }
                    else
                    {
                        if (allowLoadFromWeb)
                        {
                            WebClient client = new WebClient();
                            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                            client.DownloadFile(url, fileName);
                            result = Image.FromFile(fileName);
                        }
                    }
                }
                catch (Exception exc)
                {
                    CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
                    result = InternalResources.NoImage;
                    if (exc.Message.Contains("404"))
                    {
                        result.Save(fileName);
                    }
                }
            }
            return result;
        }
        internal enum ImageType
        {
            Item,
            StructureType,
            ShipType,
            DroneType,
            None
        }
        #endregion
        #region InvFlags
        public void ClearInvFlags()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aInvFlags", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportInvFlags(string fileName)
        {
            bool result = false;
            try
            {
                //путь к временному файлу
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=flagID Long");
                sw.WriteLine("Col2=flagName Text Width 100");
                sw.WriteLine("Col3=flagText Text Width 100");
                sw.WriteLine("Col4=flagType Text Width 20");
                sw.WriteLine("Col5=orderID Long");
                sw.Close();

                //импортируем
                //insert into aInvFlags select * from tmpbase;
                string query = String.Format(
                    "insert into aInvFlags select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region InvGroups
        public void ClearInvGroups()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aInvGroups", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportInvGroups(string fileName)
        {
            bool result = false;
            try
            {
                //путь к временному файлу
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=groupID Long");
                sw.WriteLine("Col2=categoryID Long");
                sw.WriteLine("Col3=groupName Text Width 100");
                sw.WriteLine("Col4=description Text Width 3000");
                sw.WriteLine("Col5=graphicID Long");
                sw.WriteLine("Col6=useBasePrice Long");
                sw.WriteLine("Col7=allowManufacture Long");
                sw.WriteLine("Col8=allowRecycler Long");
                sw.WriteLine("Col9=anchored Long");
                sw.WriteLine("Col10=anchorable Long");
                sw.WriteLine("Col11=fittableNonSingleton Long");
                sw.WriteLine("Col12=published Long");
                sw.Close();

                //импортируем
                //insert into aInvGroups select * from tmpbase;
                string query = String.Format(
                    "insert into aInvGroups select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region InvCategories
        public void ClearInvCategories()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aInvCategories", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportInvCategories(string fileName)
        {
            bool result = false;
            try
            {
                //путь к временному файлу
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=categoryID Long");
                sw.WriteLine("Col2=categoryName Text Width 100");
                sw.WriteLine("Col3=description Memo");
                sw.WriteLine("Col4=graphicID Long");
                sw.WriteLine("Col5=published Long");
                sw.Close();

                //импортируем
                //insert into aInvCategories select * from tmpbase;
                string query = String.Format(
                    "insert into aInvCategories select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public string GetCategoryNameById(int categoryId)
        {
            OleDbCommand cmd = new OleDbCommand(String.Format("select categoryName from aInvCategories where categoryId = {0}", categoryId), connectDB);
            object obj = cmd.ExecuteScalar();
            string result = "";
            if (obj != null && obj.GetType() != typeof(DBNull))
            {
                result = Convert.ToString(obj);
            }
            return result;
        }
        #endregion
        #region MapSolarSystems
        public void ClearMapSolarSystems()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aMapSolarSystems", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportMapSolarSystems(string fileName)
        {
            bool result = false;
            try
            {
                //путь к временному файлу
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=regionID Long");
                sw.WriteLine("Col2=constellationID Long");
                sw.WriteLine("Col3=solarSystemID Long");
                sw.WriteLine("Col4=solarSystemName Text Width 3000");
                sw.WriteLine("Col5=x Double");
                sw.WriteLine("Col6=y Double");
                sw.WriteLine("Col7=z Double");
                sw.WriteLine("Col8=xMin Double");
                sw.WriteLine("Col9=xMax Double");
                sw.WriteLine("Col10=yMin Double");
                sw.WriteLine("Col11=yMax Double");
                sw.WriteLine("Col12=zMin Double");
                sw.WriteLine("Col13=zMax Double");
                sw.WriteLine("Col14=luminosity Double");
                sw.WriteLine("Col15=border Long");
                sw.WriteLine("Col16=fringe Long");
                sw.WriteLine("Col17=corridor Long");
                sw.WriteLine("Col18=hub Long");
                sw.WriteLine("Col19=international Long");
                sw.WriteLine("Col20=regional Long");
                sw.WriteLine("Col21=constellation Long");
                sw.WriteLine("Col22=security Double");
                sw.WriteLine("Col23=factionID Long");
                sw.WriteLine("Col24=radius Double");
                sw.WriteLine("Col25=sunTypeID Long");
                sw.WriteLine("Col26=securityClass Text Width 10");
                sw.WriteLine("Col27=allianceID Long");
                sw.WriteLine("Col28=sovereigntyLevel Long");
                sw.WriteLine("Col29=sovereigntyDateTime DateTime");
                sw.Close();

                //импортируем
                //insert into aMapSolarSystems select * from tmpbase;
                string query = String.Format(
                    "insert into aMapSolarSystems select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region EveNames
        public void ClearEveNames()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aEveNames", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportEveNames(string fileName)
        {
            bool result = false;
            //путь к временному файлу
            try
            {
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=itemID Long");
                sw.WriteLine("Col2=itemName Text Width 100");
                sw.WriteLine("Col3=categoryID Memo");
                sw.WriteLine("Col4=groupID Long");
                sw.WriteLine("Col5=typeID Double");
                sw.Close();

                //импортируем
                //insert into aInvTypes select * from tmpbase;
                string query = String.Format(
                    "insert into aEveNames select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region EveGraphics
        public void ClearEveGraphics()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aEveGraphics", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportEveGraphics(string fileName)
        {
            bool result = false;
            //путь к временному файлу
            try
            {
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=graphicID Long");
                sw.WriteLine("Col2=url3D Text Width 100");
                sw.WriteLine("Col3=urlWeb Text Width 100");
                sw.WriteLine("Col4=description Memo");
                sw.WriteLine("Col5=published Long");
                sw.WriteLine("Col6=obsolete Long");
                sw.WriteLine("Col7=icon Text Width 100");
                sw.WriteLine("Col8=urlSound Text Width 100");
                sw.WriteLine("Col9=explosionId Long");
                sw.Close();

                //импортируем
                //insert into aInvTypes select * from tmpbase;
                string query = String.Format(
                    "insert into aEveGraphics select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region DgmAttributeTypes
        public void ClearDgmAttributeTypes()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aDgmAttributeTypes", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportDgmAttributeTypes(string fileName)
        {
            bool result = false;
            //путь к временному файлу
            try
            {
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=attributeId Long");
                sw.WriteLine("Col2=attributeName Text Width 100");
                sw.WriteLine("Col3=attributeCategory Long");
                sw.WriteLine("Col4=description Memo");
                sw.WriteLine("Col5=maxAttributeId Long");
                sw.WriteLine("Col6=attributeIdx Long");
                sw.WriteLine("Col7=graphicId Text Width 100");
                sw.WriteLine("Col8=chargeRechargeTimeID Text Width 100");
                sw.WriteLine("Col9=defaultValue Double");
                sw.WriteLine("Col10=published Long");
                sw.WriteLine("Col11=displayName Text Width 100");
                sw.WriteLine("Col12=unitId Long");
                sw.WriteLine("Col13=stackable Long");
                sw.WriteLine("Col14=highIsGood Long");
                sw.Close();

                //импортируем
                //insert into aInvTypes select * from tmpbase;
                string query = String.Format(
                    "insert into aDgmAttributeTypes select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region DgmEffects
        public void ClearDgmEffects()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aDgmEffects", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportDgmEffects(string fileName)
        {
            bool result = false;
            //путь к временному файлу
            try
            {
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=effectID Long");
                sw.WriteLine("Col2=effectName Memo");
                sw.WriteLine("Col3=effectCategory Long");
                sw.WriteLine("Col4=preExpression Long");
                sw.WriteLine("Col5=postExpression Long");
                sw.WriteLine("Col6=description Memo");
                sw.WriteLine("Col7=guid Text Width 60");
                sw.WriteLine("Col8=graphicID Long");
                sw.WriteLine("Col9=isOffensive Long");
                sw.WriteLine("Col10=isAssistance Long");
                sw.WriteLine("Col11=durationAttributeID Long");
                sw.WriteLine("Col12=trackingSpeedAttributeID Long");
                sw.WriteLine("Col13=dischargeAttributeID Long");
                sw.WriteLine("Col14=rangeAttributeID Long");
                sw.WriteLine("Col15=falloffAttributeID Long");
                sw.WriteLine("Col16=disallowAutoRepeat Long");
                sw.WriteLine("Col17=published Long");
                sw.WriteLine("Col18=displayName Text Width 100");
                sw.WriteLine("Col19=isWarpSafe Long");
                sw.WriteLine("Col20=rangeChance Long");
                sw.WriteLine("Col21=electronicChance Long");
                sw.WriteLine("Col22=propulsionChance Long");
                sw.WriteLine("Col23=distribution Long");
                sw.WriteLine("Col24=sfxName Text Width 20");
                sw.WriteLine("Col25=npcUsageChanceAttributeID Long");
                sw.WriteLine("Col26=npcActivationChanceAttributeID Long");
                sw.WriteLine("Col27=fittingUsageChanceAttributeID Long");
                sw.Close();

                //импортируем
                //insert into aInvTypes select * from tmpbase;
                string query = String.Format(
                    "insert into aDgmEffects select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region DgmTypeAttributes
        public void ClearDgmTypeAttributes()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aDgmTypeAttributes", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportDgmTypeAttributes(string fileName)
        {
            bool result = false;
            //путь к временному файлу
            try
            {
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=typeID Long");
                sw.WriteLine("Col2=attributeID Long");
                sw.WriteLine("Col3=valueInt Long");
                sw.WriteLine("Col4=valueFloat Double");
                sw.Close();

                //импортируем
                //insert into aInvTypes select * from tmpbase;
                string query = String.Format(
                    "insert into aDgmTypeAttributes select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region DgmTypeEffects
        public void ClearDgmTypeEffects()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aDgmTypeEffects", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportDgmTypeEffects(string fileName)
        {
            bool result = false;
            //путь к временному файлу
            try
            {
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=typeID Long");
                sw.WriteLine("Col2=effectID Long");
                sw.WriteLine("Col3=isDefault Long");
                sw.Close();

                //импортируем
                //insert into aInvTypes select * from tmpbase;
                string query = String.Format(
                    "insert into aDgmTypeEffects select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region InvControlTowerResourcePurposes
        public void ClearInvControlTowerResourcePurposes()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aInvControlTowerResourcePurposes", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportInvControlTowerResourcePurposes(string fileName)
        {
            bool result = false;
            //путь к временному файлу
            try
            {
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=purpose Long");
                sw.WriteLine("Col2=purposeText Text Width 100");
                sw.Close();

                //импортируем
                //insert into aInvTypes select * from tmpbase;
                string query = String.Format(
                    "insert into aInvControlTowerResourcePurposes select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region invControlTowerResources
        public void ClearinvControlTowerResources()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from ainvControlTowerResources", connectDB);
            cmd.ExecuteNonQuery();
            //CheckClose();
        }
        public bool ImportinvControlTowerResources(string fileName)
        {
            bool result = false;
            //путь к временному файлу
            try
            {
                string tempFileName = Path.Combine(Options.ApplicationDataPath, "tempfile.txt");
                //копируем заданный файл во временный (т.к. туда же надо поместить schema.ini)
                File.Copy(fileName, tempFileName, true);
                //пишет туда же schema.ini
                string schema = Path.Combine(Options.ApplicationDataPath, "schema.ini");
                if (File.Exists(schema))
                    File.Delete(schema);
                FileStream fs = File.OpenWrite(schema);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("[tempfile.txt]");
                sw.WriteLine("Format=Delimited(|)");
                sw.WriteLine("ColNameHeader=False");
                sw.WriteLine("Col1=controlTowerTypeID Long");
                sw.WriteLine("Col2=resourceTypeID Long");
                sw.WriteLine("Col3=purpose Long");
                sw.WriteLine("Col4=quantity Long");
                sw.WriteLine("Col5=minSecurityLevel Double");
                sw.WriteLine("Col6=factionID Long");
                sw.Close();

                //импортируем
                //insert into aInvTypes select * from tmpbase;
                string query = String.Format(
                    "insert into ainvControlTowerResources select * from [Text;DATABASE={1}\\;].[{0}]",
                    Path.GetFileName(tempFileName),
                    Path.GetDirectoryName(tempFileName));
                OleDbCommand cmd = new OleDbCommand(query, connectDB);
                cmd.ExecuteNonQuery();
                File.Delete(tempFileName);
                result = true;
            }
            catch (Exception exc)
            {
                CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
        #endregion
        #region ConquerableStationsList
        public DataTable TableConquerableStationsList
        {
            get
            {
                if (!mDataSet.Tables.Contains("aConquerableStationsList"))
                {
                    mDataSet.Tables.Add("aConquerableStationsList");
                }
                return mDataSet.Tables["aConquerableStationsList"];
            }
        }
        private OleDbDataAdapter moddaConquerableStationsList;
        public bool GetConquerableStationsList()
        {
            //CheckOpen();
            bool result = false;
            TableConquerableStationsList.Clear();
            moddaConquerableStationsList = new OleDbDataAdapter("select * from aConquerableStationsList", connectDB);
            mCommandBuilder = new OleDbCommandBuilder(moddaConquerableStationsList);

            moddaConquerableStationsList.Fill(mDataSet, "aConquerableStationsList");

            //CheckClose();
            return result;
        }
        public void ClearConquerableStationsList()
        {
            //CheckOpen();
            OleDbCommand cmd = new OleDbCommand("delete from aConquerableStationsList", connectDB);
            cmd.ExecuteNonQuery();
            TableConquerableStationsList.Clear();
            GetConquerableStationsList();
            //CheckClose();
        }
        public int CommitConquerableStationsList()
        {
            int result = -1;
            try
            {
                result = moddaConquerableStationsList.Update(TableConquerableStationsList);
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
            return result;
        }

        public DataTable TableConquerableStationsListForView
        {
            get
            {
                if (!mDataSet.Tables.Contains("aConquerableStationsListForView"))
                {
                    mDataSet.Tables.Add("aConquerableStationsListForView");
                }
                return mDataSet.Tables["aConquerableStationsListForView"];
            }
        }
        public bool GetConquerableStationsListForView()
        {
            //CheckOpen();
            bool result = false;
            TableConquerableStationsListForView.Clear();
            OleDbDataAdapter moddaConquerableStationsListForView = new OleDbDataAdapter(
                //"select aSovereignty.solarSystemId, aSovereignty.solarSystemName, aSovereignty.allianceId, aAlliances.name from aSovereignty left join aAlliances on (aSovereignty.allianceId=aAlliances.allianceId)",
                "SELECT aConquerableStationsList.solarSystemID, aEveNames.itemName AS solarSystemName, aConquerableStationsList.stationTypeID, aInvTypes.typeName AS stationTypeName, aConquerableStationsList.corporationID, aConquerableStationsList.corporationName, aConquerableStationsList.stationId, aConquerableStationsList.stationName " +
                "FROM (aConquerableStationsList LEFT JOIN aInvTypes ON aConquerableStationsList.stationTypeID = aInvTypes.typeId) LEFT JOIN aEveNames ON aConquerableStationsList.solarSystemID = aEveNames.itemId;",
                connectDB);

            moddaConquerableStationsListForView.Fill(mDataSet, "aConquerableStationsListForView");
            //CheckClose();
            return result;
        }
        #endregion
        #region Item Slot Type
        public SlotType GetSlotType(int typeId)
        {
            SlotType result = SlotType.None;
            OleDbCommand cmd = new OleDbCommand(
                String.Format(
                    "select effectName from aDgmEffects where effectId in (select effectId from aDgmTypeEffects where typeId = {0} and effectId in (11, 12, 13, 2663));",
                    typeId),
                connectDB);
            object obj = cmd.ExecuteScalar();
            if (obj != null && !(obj is DBNull))
            {
                string str = Convert.ToString(obj);
                switch (str)
                {
                    case "loPower":
                        result = SlotType.Low;
                        break;
                    case "hiPower":
                        result = SlotType.High;
                        break;
                    case "medPower":
                        result = SlotType.Middle;
                        break;
                    case "rigSlot":
                        result = SlotType.Rig;
                        break;
                }
            }
            return result;
        }
        #endregion
        #endregion
        public static bool SetDataGridRowToId(System.Windows.Forms.DataGridView dgv, Int32 id)
        {
            bool result = false;
            for (int i = 0; i < dgv.Rows.Count && id > 0; i++)
            {
                if (dgv.Rows[i].Cells["id"].Value != null && (Int32)dgv.Rows[i].Cells["id"].Value == id)
                {
                    for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                    {
                        if (dgv.Rows[i].Cells[j].Visible == true)
                        {
                            dgv.CurrentCell = dgv.Rows[i].Cells[j];
                            result = true;
                            break;
                        }
                    }
                    break;
                }
            }
            return result;
        }
        public static bool SetDataGridRowToId(System.Windows.Forms.DataGridView dgv, string columnName, object value)
        {
            bool result = false;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (Convert.ToString(dgv.Rows[i].Cells[columnName].Value) == Convert.ToString(value))
                {
                    for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                    {
                        if (dgv.Rows[i].Cells[j].Visible == true)
                        {
                            dgv.CurrentCell = dgv.Rows[i].Cells[j];
                            result = true;
                            break;
                        }
                    }
                    break;
                }
            }
            return result;
        }
        public static bool TableCaptionsToHeaders(DataTable dt, System.Windows.Forms.DataGridView dgv)
        {
            try
            {
                if (dt != null && dgv != null)
                {
                    for (int i = 0; i < dt.Columns.Count && i < dgv.Columns.Count; i++)
                    {
                        //try
                        //{
                        string s1;
                        s1 = dt.Columns[i].Caption;
                        //s2 = dgv.Columns[i].HeaderText;
                        //dt.Columns[i].Caption = s1;
                        dgv.Columns[i].HeaderText = s1;

                        s1 = dt.Columns[i].ColumnName;
                        dgv.Columns[i].Name = s1;

                        if (s1.ToUpper().Contains("ID")) dgv.Columns[i].Visible = false;
                        //dgv.Columns[i].HeaderText = dt.Columns[i].Caption;
                        //}
                        //catch
                        //{
                        //}
                    }
                    return true;
                }
                else
                    return false;

            }
            catch// (Exception exc)
            {
                //CallExceptionHandler(exc, System.Reflection.MethodInfo.GetCurrentMethod().Name);
                return false;
            }
        }
        private int GetIdentity(OleDbConnection connection)
        {
            OleDbCommand cmdGetIdentity = new OleDbCommand("select @@identity", connection);
            int identity = (int)cmdGetIdentity.ExecuteScalar();
            return identity;
        }

        private void CheckOpen()
        {
            openStates.Push(connectionOpened);
            if (connectionOpened == false) OpenDatabase();
        }
        private void CheckClose()
        {
            if (openStates.Count > 0)
            {
                bool prev = openStates.Pop();
                if (prev == false && connectionOpened == true) CloseDatabase();
            }
        }
        public static DataTable ParseRowset(XmlNode rowset)
        {
            DataTable table = new DataTable(rowset.Attributes["name"].InnerText);
            string key = null;
            if (rowset.Attributes["key"] != null)
            {
                key = rowset.Attributes["key"].InnerText;
            }
            string[] columns = rowset.Attributes["columns"].InnerText.Split(',');
            foreach (string str in columns)
            {
                table.Columns.Add(str, typeof(String));
            }
            foreach (XmlNode nodeRow in rowset.SelectNodes("child::*"))
            {
                DataRow row = table.NewRow();
                foreach (string str in columns)
                {
                    try
                    {
                        row[str] = nodeRow.Attributes[str].InnerText;
                    }
                    catch
                    {
                        row[str] = "unpublished";
                    }
                }
                table.Rows.Add(row);
            }
            if (key != null)
                table.PrimaryKey = new DataColumn[] { table.Columns[key] };
            return table;
        }
        public void FormatDataGridView(DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            //dgv.Columns[dgv.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        public static string ReplaceDecimalSeparator(string value)
        {
            string separator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (!value.Contains(separator))
                value = value.Replace(".", separator).Replace(",", separator);
            return value;
        }
        private string CodeEscapes(string value)
        {
            string result = value.Replace("\\", "\\\\");
            result = result.Replace("\"", "\\\"");
            result = result.Replace("'", "\\'");
            return result;
        }
        private string DecodeEscapes(string value)
        {
            string result = value.Replace("\\'", "'");
            result = result.Replace("\\\"", "\"");
            return result.Replace("\\\\", "\\");
        }
        #region IDisposable Members

        public void Dispose()
        {
            CheckClose();
        }

        #endregion
    }
    public class CorporationSheetInfo
    {
        public CorporationSheetInfo()
        {
            mDivisions = new DataTable("divisions");
            mDivisions.Columns.Add("accountKey", typeof(Int32));
            mDivisions.Columns.Add("description", typeof(String));

            mWalletDivisions = new DataTable("walletDivisions");
            mWalletDivisions.Columns.Add("accountKey", typeof(Int32));
            mWalletDivisions.Columns.Add("description", typeof(String));
            Clear();
        }
        public void Clear()
        {
            mCorporationID = 0;
            mCorporationName = "unknown";
            mTicker = "unknown";
            mCeoID = 0;
            mCeoName = "unknown";
            mStationID = 0;
            mStationName = "unknown";
            mDescription = "unknown";
            mUrl = "unknown";
            mAllianceID = 0;
            mAllianceName = "unknown";
            mTaxRate = 0;
            mMemberCount = 0;
            mMemberLimit = 0;
            mShares = 0;
            mDivisions.Rows.Clear();
            mWalletDivisions.Rows.Clear();
            mDivisions.Rows.Add(1000, "1");
            mDivisions.Rows.Add(1001, "2");
            mDivisions.Rows.Add(1002, "3");
            mDivisions.Rows.Add(1003, "4");
            mDivisions.Rows.Add(1004, "5");
            mDivisions.Rows.Add(1005, "6");
            mDivisions.Rows.Add(1006, "7");
            mWalletDivisions.Rows.Add(1000, "1");
            mWalletDivisions.Rows.Add(1001, "2");
            mWalletDivisions.Rows.Add(1002, "3");
            mWalletDivisions.Rows.Add(1003, "4");
            mWalletDivisions.Rows.Add(1004, "5");
            mWalletDivisions.Rows.Add(1005, "6");
            mWalletDivisions.Rows.Add(1006, "7");
        }
        public void Init(XmlDocument doc)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode node = root.SelectSingleNode("descendant::corporationID");
            mCorporationID = Convert.ToInt32(node.InnerText);

            node = root.SelectSingleNode("descendant::corporationName");
            if (node != null)
                mCorporationName = node.InnerText;

            node = root.SelectSingleNode("descendant::ticker");
            if (node != null)
                mTicker = node.InnerText;

            node = root.SelectSingleNode("descendant::ceoID");
            if (node != null)
                mCeoID = Convert.ToInt32(node.InnerText);

            node = root.SelectSingleNode("descendant::ceoName");
            if (node != null)
                mCeoName = node.InnerText;

            node = root.SelectSingleNode("descendant::stationID");
            if (node != null)
                mStationID = Convert.ToInt32(node.InnerText);

            node = root.SelectSingleNode("descendant::stationName");
            if (node != null)
                mStationName = node.InnerText;

            node = root.SelectSingleNode("descendant::description");
            if (node != null)
                mDescription = node.InnerText;

            node = root.SelectSingleNode("descendant::url");
            if (node != null)
                mUrl = node.InnerText;

            node = root.SelectSingleNode("descendant::allianceID");
            if (node != null)
                mAllianceID = Convert.ToInt32(node.InnerText);

            node = root.SelectSingleNode("descendant::allianceName");
            if (node != null)
                mAllianceName = node.InnerText;

            node = root.SelectSingleNode("descendant::taxRate");
            if (node != null)
                mTaxRate = Convert.ToInt32(node.InnerText);

            node = root.SelectSingleNode("descendant::memberCount");
            if (node != null)
                mMemberCount = Convert.ToInt32(node.InnerText);

            node = root.SelectSingleNode("descendant::memberLimit");
            if (node != null)
                mMemberLimit = Convert.ToInt32(node.InnerText);

            node = root.SelectSingleNode("descendant::shares");
            if (node != null)
                mShares = Convert.ToInt32(node.InnerText);

            DataTable dt;
            node = root.SelectSingleNode("descendant::rowset[@name='divisions']");
            if (node != null)
            {
                dt = DataClass.ParseRowset(node);
                mDivisions.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    mDivisions.ImportRow(row);
                }
            }

            node = root.SelectSingleNode("descendant::rowset[@name='walletDivisions']");
            if (node != null)
            {
                dt = DataClass.ParseRowset(node);
                mWalletDivisions.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    mWalletDivisions.ImportRow(row);
                }
            }
        }

        int mCorporationID;
        string mCorporationName;
        string mTicker;
        int mCeoID;
        string mCeoName;
        int mStationID;
        string mStationName;
        string mDescription;
        string mUrl;
        int mAllianceID;
        string mAllianceName;
        int mTaxRate;
        int mMemberCount;
        int mMemberLimit;
        int mShares;
        DataTable mDivisions;
        DataTable mWalletDivisions;

        public int CorporationID { get { return mCorporationID; } }
        public string CorporationName { get { return mCorporationName; } }
        public string Ticker { get { return mTicker; } }
        public int CeoID { get { return mCeoID; } }
        public string CeoName { get { return mCeoName; } }
        public int StationID { get { return mStationID; } }
        public string StationName { get { return mStationName; } }
        public string Description { get { return mDescription; } }
        public string Url { get { return mUrl; } }
        public int AllianceID { get { return mAllianceID; } }
        public string AllianceName { get { return mAllianceName; } }
        public int TaxRate { get { return mTaxRate; } }
        public int MemberCount { get { return mMemberCount; } }
        public int MemberLimit { get { return mMemberLimit; } }
        public int Shares { get { return mShares; } }
        public DataTable Divisions { get { return mDivisions; } }
        public DataTable WalletDivisions { get { return mWalletDivisions; } }
    }
    public class StarbaseDetail
    {
        public StarbaseDetail()
        {
            mFuel = new DataTable("Fuel");
            mFuel.Columns.Add("typeID", typeof(Int32));
            mFuel.Columns.Add("quantity", typeof(Int32));
            mFuel.Columns.Add("typeName", typeof(String));

            mGeneralSettings = new GeneralSettings();
            mCombatSettings = new CombatSettings();

            Clear();
        }
        public void Clear()
        {
            mFuel.Rows.Clear();
            mGeneralSettings.AllowAllianceMembers = false;
            mGeneralSettings.AllowCorporationMembers = false;
            CachedUntil = DateTime.MinValue;
            mGeneralSettings.ClaimSovereignty = 0;
            CurrentTime = DateTime.Now;
            mGeneralSettings.DeployFlags = 0;
            mGeneralSettings.UsageFlags = 0;

            mCombatSettings.OnAggressionEnabled = false;
            mCombatSettings.OnCorporationWarEnabled = false;
            mCombatSettings.OnStandingDropEnabled = false;
            mCombatSettings.OnStandingDropValue = 0;
            mCombatSettings.OnStatusDropEnabled = false;
            mCombatSettings.OnStatusDropValue = 0;
        }
        public void Init(XmlDocument doc)
        {
            XmlNode root = doc.DocumentElement;
            XmlNode node = root.SelectSingleNode("descendant::currentTime");
            mCurrentTime = DateTime.Parse(node.InnerText);

            node = root.SelectSingleNode("descendant::cachedUntil");
            mCachedUntil = DateTime.Parse(node.InnerText);

            node = root.SelectSingleNode("descendant::usageFlags");
            mGeneralSettings.UsageFlags = Convert.ToInt32(node.InnerText);

            node = root.SelectSingleNode("descendant::deployFlags");
            mGeneralSettings.DeployFlags = Convert.ToInt32(node.InnerText);

            node = root.SelectSingleNode("descendant::allowCorporationMembers");
            mGeneralSettings.AllowCorporationMembers = (Convert.ToInt32(node.InnerText) > 0 ? true : false);

            node = root.SelectSingleNode("descendant::allowAllianceMembers");
            mGeneralSettings.AllowAllianceMembers = (Convert.ToInt32(node.InnerText) > 0 ? true : false);

            node = root.SelectSingleNode("descendant::claimSovereignty");
            mGeneralSettings.ClaimSovereignty = Convert.ToInt32(node.InnerText);

            node = root.SelectSingleNode("descendant::onStandingDrop");
            if (node.Attributes["enabled"] != null)
                mCombatSettings.OnStandingDropEnabled = (Convert.ToInt32(node.Attributes["enabled"].InnerText) > 0 ? true : false);
            if (node.Attributes["standing"] != null)
                mCombatSettings.OnStandingDropValue = Convert.ToDouble(node.Attributes["standing"].InnerText) / 10;

            node = root.SelectSingleNode("descendant::onStatusDrop");
            if (node.Attributes["enabled"] != null)
                mCombatSettings.OnStatusDropEnabled = (Convert.ToInt32(node.Attributes["enabled"].InnerText) > 0 ? true : false);
            if (node.Attributes["standing"] != null)
                mCombatSettings.OnStatusDropValue = Convert.ToDouble(node.Attributes["standing"].InnerText) / 10;

            node = root.SelectSingleNode("descendant::onAggression");
            if (node.Attributes["enabled"] != null)
                mCombatSettings.OnAggressionEnabled = (Convert.ToInt32(node.Attributes["enabled"].InnerText) > 0 ? true : false);

            node = root.SelectSingleNode("descendant::onCorporationWar");
            if (node.Attributes["enabled"] != null)
                mCombatSettings.OnCorporationWarEnabled = (Convert.ToInt32(node.Attributes["enabled"].InnerText) > 0 ? true : false);

            node = root.SelectSingleNode("descendant::rowset[@name='fuel']");
            DataTable dtFuel = DataClass.ParseRowset(node);
            mFuel.Rows.Clear();
            foreach (DataRow row in dtFuel.Rows)
            {
                mFuel.ImportRow(row);
            }
        }

        DateTime mCurrentTime;
        DateTime mCachedUntil;
        GeneralSettings mGeneralSettings;
        CombatSettings mCombatSettings;
        DataTable mFuel;

        public DateTime CurrentTime
        {
            get { return mCurrentTime; }
            set { mCurrentTime = value; }
        }
        public DateTime CachedUntil
        {
            get { return mCachedUntil; }
            set { mCachedUntil = value; }
        }
        public GeneralSettings GeneralSettings
        {
            get { return mGeneralSettings; }
            //set { mGeneralSettings = value; }
        }
        public CombatSettings CombatSettings
        {
            get { return mCombatSettings; }
        }
        public DataTable Fuel
        {
            get { return mFuel; }
        }
    }
    public class GeneralSettings
    {
        int mUsageFlags;
        int mDeployFlags;
        bool mAllowCorporationMembers;
        bool mAllowAllianceMembers;
        int mClaimSovereignty;

        public int UsageFlags
        {
            get { return mUsageFlags; }
            set { mUsageFlags = value; }
        }
        public int DeployFlags
        {
            get { return mDeployFlags; }
            set { mDeployFlags = value; }
        }
        public bool AllowCorporationMembers
        {
            get { return mAllowCorporationMembers; }
            set { mAllowCorporationMembers = value; }
        }
        public bool AllowAllianceMembers
        {
            get { return mAllowAllianceMembers; }
            set { mAllowAllianceMembers = value; }
        }
        public int ClaimSovereignty
        {
            get { return mClaimSovereignty; }
            set { mClaimSovereignty = value; }
        }
    }
    public class CombatSettings
    {
        bool mOnStandingDropEnabled;
        double mOnStandingDropValue;
        bool mOnStatusDropEnabled;
        double mOnStatusDropValue;
        bool mOnAggressionEnabled;
        bool mOnCorporationWarEnabled;

        public bool OnStandingDropEnabled
        {
            get { return mOnStandingDropEnabled; }
            set { mOnStandingDropEnabled = value; }
        }
        public double OnStandingDropValue
        {
            get { return mOnStandingDropValue; }
            set { mOnStandingDropValue = value; }
        }
        public bool OnStatusDropEnabled
        {
            get { return mOnStatusDropEnabled; }
            set { mOnStatusDropEnabled = value; }
        }
        public double OnStatusDropValue
        {
            get { return mOnStatusDropValue; }
            set { mOnStatusDropValue = value; }
        }
        public bool OnAggressionEnabled
        {
            get { return mOnAggressionEnabled; }
            set { mOnAggressionEnabled = value; }
        }
        public bool OnCorporationWarEnabled
        {
            get { return mOnCorporationWarEnabled; }
            set { mOnCorporationWarEnabled = value; }
        }
    }
    public enum SlotType
    {
        None,
        Low,
        Middle,
        High,
        Rig
    }

    #region POS
    public class StarbaseFuel
    {
        int mTypeId;
        string mTypeName;
        int mConsumptionPerHour;
        double mVolume;
    }
    public class StarbaseStructure
    {
        int mCPU;
        int mPowerGrid;
    }
    #endregion
}
