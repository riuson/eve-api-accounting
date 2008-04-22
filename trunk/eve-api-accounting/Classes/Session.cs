using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Net;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Drawing;

namespace Accounting
{
    public class Session
    {
        public Session(Options options, DataClass dataClass)//, FormHtmlMessage formHtmlMessage)
        {
            mOptions = options;
            mDataClass = dataClass;
            //mFormHtmlMessage = formHtmlMessage;
            mXmlResponse = new XmlDocument();

            #region соответствие функций и адресов
            mFunctionUrls = new Dictionary<ApiFunction, string>();
            mFunctionUrls.Add(ApiFunction.EveAllianceList, murlEveAllianceList);
            mFunctionUrls.Add(ApiFunction.EveRefTypes, murlEveRefTypes);
            mFunctionUrls.Add(ApiFunction.EveSkillTree, murlEveSkillTree);
            mFunctionUrls.Add(ApiFunction.EveConquerableStationList, murlEveConquerableStationList);
            mFunctionUrls.Add(ApiFunction.MapSovereignty, murlMapSovereignty);

            mFunctionUrls.Add(ApiFunction.AccountCharacters, murlAccountCharacters);
            mFunctionUrls.Add(ApiFunction.AccountCharacterSheet, murlAccountCharacterSheet);
            mFunctionUrls.Add(ApiFunction.AccountSkillInTraining, murlAccountSkillInTraining);

            mFunctionUrls.Add(ApiFunction.CharAccountBalance, murlCharAccountBalance);
            mFunctionUrls.Add(ApiFunction.CharWalletJournal, murlCharWalletJournal);
            mFunctionUrls.Add(ApiFunction.CharWalletTransactions, murlCharWalletTransactions);
            mFunctionUrls.Add(ApiFunction.CharAssetList, murlCharAssetList);
            mFunctionUrls.Add(ApiFunction.CharIndustryJobs, murlCharIndustryJobs);
            mFunctionUrls.Add(ApiFunction.CharKillLog, murlCharKillLog);

            mFunctionUrls.Add(ApiFunction.CorpAccountBalance, murlCorpAccountBalance);
            mFunctionUrls.Add(ApiFunction.CorpMemberTracking, murlCorpMemberTracking);
            mFunctionUrls.Add(ApiFunction.CorpWalletJournal, murlCorpWalletJournal);
            mFunctionUrls.Add(ApiFunction.CorpWalletTransactions, murlCorpWalletTransactions);
            mFunctionUrls.Add(ApiFunction.CorpStarbaseList, murlCorpStarbaseList);
            mFunctionUrls.Add(ApiFunction.CorpStarbaseDetail, murlCorpStarbaseDetail);
            mFunctionUrls.Add(ApiFunction.CorpCorporationSheet, murlCorpSheet);
            mFunctionUrls.Add(ApiFunction.CorpAssetList, murlCorpAssetList);
            mFunctionUrls.Add(ApiFunction.CorpIndustryJobs, murlCorpIndustryJobs);
            mFunctionUrls.Add(ApiFunction.CorpKillLog, murlCorpKillLog);
            #endregion
        }
        #region константы - адреса
        // Базовый адрес
        private const string murlApiBase = "http://api.eve-online.com";
        //EVE Information
        private const string murlEveAllianceList = "/eve/AllianceList.xml.aspx";
        private const string murlEveRefTypes = "/eve/RefTypes.xml.aspx";
        private const string murlEveConquerableStationList = "/eve/ConquerableStationList.xml.aspx";
        private const string murlEveSkillTree = "/eve/SkillTree.xml.aspx";
        private const string murlMapSovereignty = "/map/Sovereignty.xml.aspx";
        //Character Information
        private const string murlAccountCharacters = "/account/Characters.xml.aspx";
        private const string murlAccountCharacterSheet = "/char/CharacterSheet.xml.aspx";
        private const string murlAccountSkillInTraining = "/char/SkillInTraining.xml.aspx";
        //Character Sensitive Information
        private const string murlCharAccountBalance = "/char/AccountBalance.xml.aspx";
        private const string murlCharWalletJournal = "/char/WalletJournal.xml.aspx";
        private const string murlCharWalletTransactions = "/char/WalletTransactions.xml.aspx";
        private const string murlCharAssetList = "/char/AssetList.xml.aspx";
        private const string murlCharIndustryJobs = "/char/IndustryJobs.xml.aspx";
        private const string murlCharKillLog = "/char/KillLog.xml.aspx";
        //Corporate Sensitive Information
        private const string murlCorpAccountBalance = "/corp/AccountBalance.xml.aspx";
        private const string murlCorpMemberTracking = "/corp/MemberTracking.xml.aspx";
        private const string murlCorpWalletJournal = "/corp/WalletJournal.xml.aspx";
        private const string murlCorpWalletTransactions = "/corp/WalletTransactions.xml.aspx";
        private const string murlCorpStarbaseList = "/corp/StarbaseList.xml.aspx";
        private const string murlCorpStarbaseDetail = "/corp/StarbaseDetail.xml.aspx";
        private const string murlCorpSheet = "/corp/CorporationSheet.xml.aspx";
        private const string murlCorpAssetList = "/corp/AssetList.xml.aspx";
        private const string murlCorpIndustryJobs = "/corp/IndustryJobs.xml.aspx";
        private const string murlCorpKillLog = "/corp/KillLog.xml.aspx";
        #endregion
        #region переменные
        private int mUserId;
        private string mApiKey;
        private int mCharacterId;
        private string mBeforeRefId;
        private string mBeforeTransId;
        private int mStarbaseId;
        private string mAccountKey;
        private int mBeforeKillId;

        private XmlDocument mXmlResponse;

        public int UserId
        {
            get { return mUserId; }
            //set { mUserId = value; }
        }
        public string ApiKey
        {
            get { return mApiKey; }
            //set { mApiKey = value; }
        }
        public int CharacterId
        {
            get { return mCharacterId; }
            //set
            //{
            //    mCharacterId = value;
            //    mCacheFolder = Path.Combine(Options.ApplicationDataPath, mCharacterId);
            //}
        }
        public string BeforeRefId
        {
            get { return mBeforeRefId; }
            set { mBeforeRefId = value; }
        }
        public string BeforeTransId
        {
            get { return mBeforeTransId; }
            set { mBeforeTransId = value; }
        }
        public int StarbaseId
        {
            get { return mStarbaseId; }
            set { mStarbaseId = value; }
        }
        public string AccountKey
        {
            get { return mAccountKey; }
            set { mAccountKey = value; }
        }
        public int BeforeKillId
        {
            get { return mBeforeKillId; }
            set { mBeforeKillId = value; }
        }

        public XmlDocument XmlResponse
        {
            get { return mXmlResponse; }
        }

        private Dictionary<ApiFunction, String> mFunctionUrls;
        //private string mCacheFolder;
        private DataClass mDataClass;
        //private WebClient mClient;

        private Options mOptions;
        private Queue<Command> mCommandQueue = new Queue<Command>();
        private object lockObj = new object();
        //private Mutex mMutex = new Mutex(false, "eve api accounting");
        //private Semaphore mSemaphore = new Semaphore(1, 1);
        AutoResetEvent mProcessing = new AutoResetEvent(true);

        //private FormHtmlMessage mFormHtmlMessage;
        #endregion
        public void GetFunctionAsync(ApiFunction function)
        {
            //lock (lockObj)
            {
                try
                {
                    //Monitor.Enter(lockObj);
                    //mSemaphore.WaitOne();
                    if (mProcessing.WaitOne(100, false))
                    {
                        mUserId = mOptions.UserId;
                        mApiKey = mOptions.ApiKey;
                        mCharacterId = mOptions.CharacterId;

                        WebClient client = new WebClient();
                        client.BaseAddress = murlApiBase;
                        client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(client_DownloadDataCompleted);
                        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);

                        Uri url = new Uri(murlApiBase + mFunctionUrls[function]);
                        client.QueryString.Clear();
                        switch (function)
                        {
                            case ApiFunction.EveAllianceList:
                            case ApiFunction.EveRefTypes:
                            case ApiFunction.EveSkillTree:
                            case ApiFunction.EveConquerableStationList:
                            case ApiFunction.MapSovereignty:
                                break;
                            case ApiFunction.AccountCharacters:
                                client.QueryString.Add("userId", mUserId.ToString());
                                client.QueryString.Add("apiKey", mApiKey);
                                break;
                            case ApiFunction.AccountCharacterSheet:
                            case ApiFunction.AccountSkillInTraining:
                            case ApiFunction.CharAccountBalance:
                            case ApiFunction.CorpAccountBalance:
                            case ApiFunction.CorpMemberTracking:
                                client.QueryString.Add("userId", mUserId.ToString());
                                client.QueryString.Add("apiKey", mApiKey);
                                client.QueryString.Add("characterId", mCharacterId.ToString());
                                break;
                            case ApiFunction.CharWalletJournal:
                            case ApiFunction.CorpWalletJournal:
                                client.QueryString.Add("userId", mUserId.ToString());
                                client.QueryString.Add("apiKey", mApiKey);
                                client.QueryString.Add("characterId", mCharacterId.ToString());
                                client.QueryString.Add("beforeRefId", mBeforeRefId);
                                client.QueryString.Add("accountKey", mAccountKey);
                                break;
                            case ApiFunction.CharWalletTransactions:
                            case ApiFunction.CorpWalletTransactions:
                                client.QueryString.Add("userId", mUserId.ToString());
                                client.QueryString.Add("apiKey", mApiKey);
                                client.QueryString.Add("characterId", mCharacterId.ToString());
                                client.QueryString.Add("beforeTransId", mBeforeTransId);
                                client.QueryString.Add("accountKey", mAccountKey);
                                break;
                            case ApiFunction.CorpStarbaseList:
                                //функция отключена на сервере
                                client.QueryString.Add("userId", mUserId.ToString());
                                client.QueryString.Add("apiKey", mApiKey);
                                client.QueryString.Add("characterId", mCharacterId.ToString());
                                client.QueryString.Add("version", "2");
                                break;
                            case ApiFunction.CorpStarbaseDetail:
                                //функция отключена на сервере
                                client.QueryString.Add("userId", mUserId.ToString());
                                client.QueryString.Add("apiKey", mApiKey);
                                client.QueryString.Add("characterId", mCharacterId.ToString());
                                client.QueryString.Add("itemId", mStarbaseId.ToString());
                                client.QueryString.Add("version", "2");
                                break;
                            case ApiFunction.CorpCorporationSheet:
                            case ApiFunction.CharAssetList:
                            case ApiFunction.CorpAssetList:
                            case ApiFunction.CharIndustryJobs:
                            case ApiFunction.CorpIndustryJobs:
                                client.QueryString.Add("userId", mUserId.ToString());
                                client.QueryString.Add("apiKey", mApiKey);
                                client.QueryString.Add("characterId", mCharacterId.ToString());
                                client.QueryString.Add("version", "2");
                                break;
                            case ApiFunction.CharKillLog:
                            case ApiFunction.CorpKillLog:
                                client.QueryString.Add("userId", mUserId.ToString());
                                client.QueryString.Add("apiKey", mApiKey);
                                client.QueryString.Add("characterId", mCharacterId.ToString());
                                client.QueryString.Add("beforeKillId", mBeforeKillId.ToString());
                                client.QueryString.Add("version", "2");
                                break;
                        }
                        if (SendRequest != null)
                            SendRequest(this, new EventArgs());
                        DateTime cachedUntil;
                        string testUrl = url.AbsoluteUri;
                        if (client.QueryString.Count > 0)
                        {
                            testUrl += "?";
                            foreach (string str in client.QueryString)
                            {
                                testUrl += String.Format("{0}={1}&", str, client.QueryString.Get(str));
                            }
                            testUrl = testUrl.Remove(testUrl.Length - 1);
                        }
                        //добавление в лог записи о запросе
                        DataRow rowLog = mDataClass.TableRequestsLog.NewRow();
                        rowLog["userId"] = mCharacterId;
                        rowLog["date"] = DateTime.UtcNow;
                        rowLog["uri"] = testUrl;
                        mDataClass.TableRequestsLog.Rows.Add(rowLog);

                        Ping ping = new Ping();
                        PingReply reply = null;
                        try
                        {
                            //reply = ping.Send(murlApiBase, 5000);
                            reply = ping.Send("api.eve-online.com", 2000);
                            //if (reply.Status == IPStatus.Success)
                            if (true)
                            {
                                if (mDataClass.CacheIsUpdateAllowed(mCharacterId, testUrl, out cachedUntil) ||
                                    function == ApiFunction.AccountCharacters ||
                                    function == ApiFunction.AccountCharacterSheet ||
                                    function == ApiFunction.EveRefTypes ||
                                    function == ApiFunction.CorpMemberTracking ||
                                    function == ApiFunction.CorpCorporationSheet)
                                //if(true)
                                {
                                    client.BaseAddress = testUrl;
                                    client.DownloadDataAsync(url, function);

                                    //отладка
                                    //mProcessing.Set();
                                    //XmlDocument doc = new XmlDocument();
                                    //string testFile = @"G:\Documents\Visual Studio 2005\Projects\Eve Api Resources\*\00.xml";
                                    //doc.Load(testFile);
                                    //FunctionCompleted(client, new FunctionCompeletedEventArgs(function, doc, ""));
                                }
                                else
                                {
                                    mProcessing.Set();
                                    string message = String.Format("Cache: данные можно загрузить после {0}", cachedUntil); ;
                                    if (FunctionCompleted != null)
                                    {
                                        FunctionCompleted(client, new FunctionCompeletedEventArgs(function, null, message));
                                    }
                                }
                            }
                            else
                            {
                                mProcessing.Set();
                                string message = String.Format("Хост недоступен: {0}", reply); ;
                                if (FunctionCompleted != null)
                                {
                                    FunctionCompleted(client, new FunctionCompeletedEventArgs(function, null, message));
                                }
                            }
                        }
                        catch (Exception exc)
                        {
                            mProcessing.Set();
                            string message = String.Format("{0}", exc.Message); ;
                            if (FunctionCompleted != null)
                            {
                                FunctionCompleted(client, new FunctionCompeletedEventArgs(function, null, message));
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
                }
                finally
                {
                }
            }
        }
        public bool GetCharImage(ImageSize size, int characterId, out Image image, out string fileName)
        {
            bool result = false;
            fileName = Path.Combine(Options.SavedApiFiles, "portraits");
            image = null;
            try
            {
                string url = null;
                Directory.CreateDirectory(fileName);
                switch (size)
                {
                    case ImageSize.x64:
                        fileName = Path.Combine(fileName, String.Format("{0}_64.jpg", characterId));
                        url = String.Format("http://img.eve.is/serv.asp?s=64&c={0}", characterId);
                        break;
                    case ImageSize.x256:
                        fileName = Path.Combine(fileName, String.Format("{0}_256.jpg", characterId));
                        url = String.Format("http://img.eve.is/serv.asp?s=256&c={0}", characterId);
                        break;
                }
                if (File.Exists(fileName))
                {
                    //if (image != null)
                    image = Image.FromFile(fileName);
                    result = true;
                }
                else
                {
                    if (mOptions.LoadImagesFromWeb)
                    {
                        Ping ping = new Ping();
                        PingReply reply = ping.Send("img.eve.is", 1000);
                        if (reply.Status == IPStatus.Success)
                        {
                            WebClient client = new WebClient();
                            client.DownloadFile(url, fileName);
                            //if (image != null)
                            image = Image.FromFile(fileName);
                            result = true;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
                result = false;
            }
            return result;
        }
        public bool GetTypeImage(ImageSize size, int typeId, out Image image)
        {
            bool result = false;
            image = mDataClass.GetInvTypeImage(typeId, size, mOptions.LoadImagesFromWeb);
            if (image != null)
                result = true;
            return result;
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (DownloadProgress != null)
            {
                DownloadProgress(sender, e);
            }
        }
        public event DownloadProgressChangedEventHandler DownloadProgress;
        public event FunctionCompletedHandler FunctionCompleted;
        public event EventHandler SendRequest;
        private void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            string strResponse = "";
            try
            {
                //mSemaphore.Release();
                mProcessing.Set();
                WebClient client = (WebClient)sender;
                string testUrl = client.BaseAddress;
                ApiFunction function = (ApiFunction)e.UserState;
                strResponse = Encoding.UTF8.GetString(e.Result);
                mXmlResponse.LoadXml(strResponse);
                //"<html><body><h1>Error Encountered</h1><p>Sorry, you encountered a fatal error.  Please contact the administrators about this if it continues.</p></body></html>"
                string path = Path.Combine(Options.SavedApiFiles, function.ToString());
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, String.Format("{0:yyyy.MM.dd - HH.mm.ss}.xml", DateTime.Now));
                mXmlResponse.Save(path);
                XmlNode nodeRoot = mXmlResponse.DocumentElement;//.SelectSingleNode("/eveapi");
                XmlNode nodeError = nodeRoot.SelectSingleNode("descendant::error");
                XmlNode nodeCurrentTime = nodeRoot.SelectSingleNode("descendant::currentTime");
                DateTime currentTime = DateTime.Parse(nodeCurrentTime.InnerText);
                string msg = "";
                if (nodeError == null)
                {
                    XmlNode nodeCachedUntil = nodeRoot.SelectSingleNode("descendant::cachedUntil");
                    DateTime cachedUntil = DateTime.Parse(nodeCachedUntil.InnerText);
                    mDataClass.CacheAdd(mCharacterId, testUrl, currentTime, cachedUntil);
                }
                else
                {
                    int errorCode = Convert.ToInt32(nodeError.Attributes["code"].InnerText);
                    string errorMessage = nodeError.InnerText;
                    msg = String.Format("Ошибка {0}: {1}", errorCode, errorMessage);
                }
                if (FunctionCompleted != null)
                {
                    FunctionCompleted(sender, new FunctionCompeletedEventArgs(function, mXmlResponse, msg));
                }
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
                //MessageBox.Show(strResponse, "Отладка: принятый текст", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //if (mFormHtmlMessage.InvokeRequired)
                //{
                //    mFormHtmlMessage.Invoke((EventHandler)delegate
                //    {
                //        mFormHtmlMessage.ShowDialog(strResponse);
                //    });
                //}
                //else
                //{
                //    mFormHtmlMessage.ShowDialog(strResponse);
                //}
            }
        }

        public void ExecuteCommandFromQueue()
        {
            try
            {
                if (mCommandQueue.Count > 0)
                {
                    Command cmd = mCommandQueue.Dequeue();
                    ExecuteCommand(cmd);
                }
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
        public void ExecuteCommand(Command command)
        {
            try
            {
                switch (command.Function)
                {
                    case ApiFunction.EveAllianceList:
                        GetFunctionAsync(ApiFunction.EveAllianceList);
                        break;
                    case ApiFunction.EveConquerableStationList:
                        GetFunctionAsync(ApiFunction.EveConquerableStationList);
                        break;
                    case ApiFunction.AccountCharacterSheet:
                        mOptions.UserId = Convert.ToInt32(command.Data[0]);//tbUserId.Text;
                        mOptions.ApiKey = Convert.ToString(command.Data[1]);//tbApiKey.Text;
                        mOptions.CharacterName = Convert.ToString(command.Data[2]);//formSelectCharacter.SelectedCharacterName;
                        mOptions.CharacterId = Convert.ToInt32(command.Data[3]);//formSelectCharacter.SelectedCharacterId;
                        ///tbCharName.Text = mOptions.CharacterName;
                        GetFunctionAsync(ApiFunction.AccountCharacterSheet);
                        break;
                    case ApiFunction.CharAccountBalance:
                        GetFunctionAsync(ApiFunction.CharAccountBalance);
                        break;
                    case ApiFunction.CorpAccountBalance:
                        GetFunctionAsync(ApiFunction.CorpAccountBalance);
                        break;
                    case ApiFunction.CharWalletJournal:
                        mAccountKey = Convert.ToString(command.Data[0]);//Convert.ToString(accountKey);
                        mBeforeRefId = Convert.ToString(command.Data[1]);//"0";
                        GetFunctionAsync(ApiFunction.CharWalletJournal);
                        break;
                    case ApiFunction.CorpWalletJournal:
                        mAccountKey = Convert.ToString(command.Data[0]);//Convert.ToString(accountKey);
                        mBeforeRefId = Convert.ToString(command.Data[1]);//"0";
                        GetFunctionAsync(ApiFunction.CorpWalletJournal);
                        break;
                    case ApiFunction.CharWalletTransactions:
                        mAccountKey = Convert.ToString(command.Data[0]);//Convert.ToString(accountKey);
                        mBeforeTransId = Convert.ToString(command.Data[1]);//"0";
                        GetFunctionAsync(ApiFunction.CharWalletTransactions);
                        break;
                    case ApiFunction.CorpWalletTransactions://419105951
                        mAccountKey = Convert.ToString(command.Data[0]);//Convert.ToString(accountKey);
                        mBeforeTransId = Convert.ToString(command.Data[1]);//"0";
                        GetFunctionAsync(ApiFunction.CorpWalletTransactions);
                        break;
                    case ApiFunction.EveRefTypes:
                        GetFunctionAsync(ApiFunction.EveRefTypes);
                        break;
                    case ApiFunction.MapSovereignty:
                        GetFunctionAsync(ApiFunction.MapSovereignty);
                        break;
                    case ApiFunction.CorpMemberTracking:
                        GetFunctionAsync(ApiFunction.CorpMemberTracking);
                        break;
                    case ApiFunction.AccountCharacters:
                        GetFunctionAsync(ApiFunction.AccountCharacters);
                        break;
                    case ApiFunction.CorpStarbaseDetail:
                        mOptions.UserId = Convert.ToInt32(command.Data[0]);//tbUserId.Text;
                        mOptions.ApiKey = Convert.ToString(command.Data[1]);//tbApiKey.Text;
                        mOptions.CharacterId = Convert.ToInt32(command.Data[2]);//formSelectCharacter.SelectedCharacterId;
                        ///tbCharName.Text = mOptions.CharacterName;
                        mStarbaseId = Convert.ToInt32(command.Data[3]);
                        GetFunctionAsync(ApiFunction.CorpStarbaseDetail);
                        break;
                    case ApiFunction.CharAssetList:
                    case ApiFunction.CorpAssetList:
                    case ApiFunction.CharIndustryJobs:
                    case ApiFunction.CorpIndustryJobs:
                        mOptions.UserId = Convert.ToInt32(command.Data[0]);//tbUserId.Text;
                        mOptions.ApiKey = Convert.ToString(command.Data[1]);//tbApiKey.Text;
                        mOptions.CharacterId = Convert.ToInt32(command.Data[2]);//formSelectCharacter.SelectedCharacterId;
                        GetFunctionAsync(command.Function);
                        break;
                    case ApiFunction.CharKillLog:
                    case ApiFunction.CorpKillLog:
                        mOptions.UserId = Convert.ToInt32(command.Data[0]);
                        mOptions.ApiKey = Convert.ToString(command.Data[1]);
                        mOptions.CharacterId = Convert.ToInt32(command.Data[2]);
                        mBeforeKillId = Convert.ToInt32(command.Data[3]);
                        GetFunctionAsync(command.Function);
                        break;
                }
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
        public Queue<Command> CommandQueue
        {
            get { return mCommandQueue; }
        }
        //public void ClearCommandQueue()
        //{
        //    while (mCommandQueue.Count > 0)
        //    {
        //        mCommandQueue.Dequeue();
        //    }
        //}
    }
    public enum ApiFunction
    {
        EveAllianceList, EveRefTypes, EveSkillTree, EveConquerableStationList, MapSovereignty,
        AccountCharacters, AccountCharacterSheet, AccountSkillInTraining,
        CharAccountBalance, CharWalletJournal, CharWalletTransactions, CharAssetList,
        CharIndustryJobs, CharKillLog,
        CorpAccountBalance, CorpMemberTracking, CorpWalletJournal, CorpWalletTransactions,
        CorpAssetList, CorpStarbaseList, CorpStarbaseDetail, CorpCorporationSheet,
        CorpIndustryJobs, CorpKillLog,
        None
    }
    public enum ImageSize
    {
        x64,
        x256
    }
    /// <summary>
    /// Класс данных для события завершения загрузки
    /// </summary>
    public class FunctionCompeletedEventArgs : EventArgs
    {
        /// <summary>
        /// Создание аргументов для передачи в обработчик события
        /// </summary>
        /// <param name="newIsConnected">Подключено ли устройство</param>
        /// <param name="newDeviceName">Название устройства</param>
        public FunctionCompeletedEventArgs(ApiFunction function, XmlDocument xmlResponse, string errorMessage)
        {
            mFunction = function;
            mXmlResponse = xmlResponse;
            mErrorMessage = errorMessage;
        }

        /// <summary>сообщение, read-only</summary>
        public ApiFunction Function
        {
            get
            {
                return mFunction;
            }
        }
        private ApiFunction mFunction;
        /// <summary>время создания, read-only</summary>
        public XmlDocument XmlResponse
        {
            get
            {
                return mXmlResponse;
            }
        }
        private XmlDocument mXmlResponse;
        private string mErrorMessage;

        public string ErrorMessage
        {
            get { return mErrorMessage; }
        }

    }
    /// <summary>
    /// Обработчик события для завершения загрузки
    /// </summary>
    public delegate void FunctionCompletedHandler(object sender, FunctionCompeletedEventArgs e);
    public class Command
    {
        public Command(ApiFunction function, params object[] data)
        {
            mFunction = function;
            if (data != null)
            {
                mData = new List<object>();
                foreach (object obj in data)
                {
                    mData.Add(obj);
                }
            }
        }
        private ApiFunction mFunction;
        private List<object> mData;
        public ApiFunction Function
        {
            get { return mFunction; }
            set { mFunction = value; }
        }
        public List<object> Data
        {
            get { return mData; }
        }
    }

}
