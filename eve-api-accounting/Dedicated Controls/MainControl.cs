using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Accounting;

namespace Accounting
{
    public partial class MainControl : UserControl, IMainControl
    {
        public MainControl(IHostWindow progressShower)
        {
            mHostWindow = progressShower;
            mHostWindow.ShowStatus(10, "Создание компонентов...");
            InitializeComponent();
            mOptions = Options.Load();

            #region создание управляющих классов
            //сбор списка библиотек калибраторов
            mHostWindow.ShowStatus(20, "Создание управляющих классов...");
            mData = new DataClass();
            mHostWindow.ShowStatus(30, "Лог запросов...");
            mData.GetRequestsLog(mOptions.CharacterId, DateTime.UtcNow.AddMinutes(2), DateTime.UtcNow.AddDays(-1 * mOptions.DefaultDaysInterval));
            mHostWindow.ShowStatus(40, "Данные корпорации...");
            mData.GetCorporationSheet(mOptions.CharacterId);
            //mHostWindow.ShowStatus(50, "Перечень предметов...");
            //mData.GetInvTypes();

            mSession = new Session(mOptions, mData);
            mSession.DownloadProgress += new System.Net.DownloadProgressChangedEventHandler(mSession_DownloadProgress);
            mSession.FunctionCompleted += new FunctionCompletedHandler(mSession_FunctionCompleted);
            mSession.SendRequest += new EventHandler(mSession_SendRequest);
            #endregion

            #region создание стартовой страницы
            //создание стартовой страницы
            mHostWindow.ShowStatus(30, "Создание стартовой страницы");
            mStartPage = new StartPage();
            mStartPage.Parent = this;
            mStartPage.Dock = DockStyle.Fill;

            IStartPage startPage = (IStartPage)mStartPage;
            startPage.OnCommand += new StartCommandHandler(startPage_OnCommand);
            //заполнение списка основных команд стартовой страницы
            startPage.ClearAll();
            startPage.AddItem(InternalResources.IconSetup,
                "Настройка",
                "Настройки параметров программы.",
                "appsetup",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconCorporation,
                "Корпорация",
                "Просмотр информации о корпорации.",
                "CorpCorporationSheet",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconAlliance,
                "Альянсы",
                "Список альянсов в EVE.",
                "EveAllianceList",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconMembers,
                "Member Tracking",
                "Список членов корпорации и данные о них.",
                "CorpMemberTracking",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconWallet,
                "Баланс кошельков",
                "Просмотр средств на кошельках, журналов и логов транзакций.",
                "AccountBalances",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconTable,
                "RefTypes",
                "Типы переводов в EVE (eve ref types).",
                "EveRefTypes",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconTable,
                "MapSovereignty",
                "Список солнечных систем.",
                "MapSovereignty",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconStats,
                "Статистика",
                "Статистика ввода/вывода средств в кошельках.",
                "Statistics",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconControlTower,
                "ПОСы",
                "Информация о ПОСах корпорации.",
                "CorpStarbaseList",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconStation,
                "Outposts List",
                "Список частных станций (Conquerable Stations List)",
                "EveConquerableStationsList",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconAssets,
                "Assets",
                "Активы",
                "Assets",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconTable,
                "IndustryJobs",
                "IndustryJobs",
                "IndustryJobs",
                StartPageMenuItemType.Command,
                false);
            startPage.AddItem(InternalResources.IconKillMail,
                "Kill Logs",
                "Логи киллов чара и корпы",
                "KillLogs",
                StartPageMenuItemType.Command,
                false);
            #endregion
            mHostWindow.ShowStatus(100, "Запуск");
            NavigatePanelVisible = false;
            //SetStyle(ControlStyles.ResizeRedraw, false);
            mStatesStack = new Stack<StateStackItem>();
            mConfig = new Dictionary<string, object>();
            //mInterfaceState = InterfaceState.StartPage;
            SwitchToInterfaceState(InterfaceState.StartPage, true);
        }

        void mSession_SendRequest(object sender, EventArgs e)
        {
            mHostWindow.ShowStatus(0, "Отправка запроса...");
        }

        void mSession_FunctionCompleted(object sender, FunctionCompeletedEventArgs e)
        {
            if (e.ErrorMessage != "")
                mHostWindow.ShowStatus("Ошибка: " + e.ErrorMessage);
            else
                mHostWindow.ShowStatus("Загрузка завершена");
        }

        void mSession_DownloadProgress(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            mHostWindow.ShowStatus(String.Format("Загружено: {0} байт", e.BytesReceived));
        }

        private void startPage_OnCommand(object sender, StartCommandEventArgs ea)
        {
            //MessageBox.Show(ea.Command, ea.Type.ToString());
            if (ea.Type == StartPageMenuItemType.Command)
            {
                if (ea.Command == "appsetup")
                    SwitchToInterfaceState(InterfaceState.ApplicationSetup, true);
                if (ea.Command == "CorpCorporationSheet")
                {
                    SwitchToInterfaceState(InterfaceState.CorporationSheet, true);
                }
                if (ea.Command == "EveAllianceList")
                {
                    SwitchToInterfaceState(InterfaceState.EveAllianceList, true);
                }
                if (ea.Command == "CorpMemberTracking")
                {
                    SwitchToInterfaceState(InterfaceState.CorpMemberTracking, true);
                }
                if (ea.Command == "AccountBalances")
                {
                    SwitchToInterfaceState(InterfaceState.AccountBalances, true);
                }
                if (ea.Command == "EveRefTypes")
                {
                    SwitchToInterfaceState(InterfaceState.EveRefTypes, true);
                }
                if (ea.Command == "MapSovereignty")
                {
                    SwitchToInterfaceState(InterfaceState.MapSovereignty, true);
                }
                if (ea.Command == "Statistics")
                {
                    SwitchToInterfaceState(InterfaceState.Statistics, true);
                }
                if (ea.Command == "CorpStarbaseList")
                {
                    SwitchToInterfaceState(InterfaceState.CorpStarbaseList, true);
                }
                if (ea.Command == "EveConquerableStationsList")
                {
                    SwitchToInterfaceState(InterfaceState.EveConquerableStationsList, true);
                }
                if (ea.Command == "Assets")
                {
                    SwitchToInterfaceState(InterfaceState.Assets, true);
                }
                if (ea.Command == "IndustryJobs")
                {
                    SwitchToInterfaceState(InterfaceState.IndustryJobs, true);
                }
                if (ea.Command == "KillLogs")
                {
                    SwitchToInterfaceState(InterfaceState.KillLogs, true);
                }
            }
        }
        #region переменные

        private IHostWindow mHostWindow;
        public IHostWindow HostWindow
        {
            get { return mHostWindow; }
            set { mHostWindow = value; }
        }

        private StartPage mStartPage;
        private Control mChildControl;
        private Options mOptions;
        private Stack<StateStackItem> mStatesStack;
        private InterfaceState mInterfaceState;
        private Session mSession;
        private DataClass mData;
        private Dictionary<string, object> mConfig;
        #endregion

        #region IMainControl Members

        public void SwitchToInterfaceState(InterfaceState state, bool saveInStack)
        {
            SwitchToInterfaceState(state, saveInStack, null);
        }
        public void SwitchToInterfaceState(InterfaceState state, bool saveInStack, params object[] data)
        {
            if (mChildControl != null)
            {
                mChildControl.Dispose();
                mChildControl = null;
            }
            switch (state)
            {
                case InterfaceState.StartPage:
                    mHostWindow.SetTitle("");
                    NavigatePanelVisible = false;
                    mStartPage.Parent = this;
                    mStartPage.Dock = DockStyle.Fill;
                    mStartPage.Show();
                    mStartPage.BringToFront();
                    //mHostWindow.ShowProgress(100, "Начало работы");
                    if (saveInStack)
                    {
                        SaveInterfaceStateInStack(state, data);
                    }
                    mInterfaceState = state;
                    break;
                case InterfaceState.ApplicationSetup:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Настройка параметров");
                        NavigatePanelVisible = true;
                        mChildControl = new AppSetup((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.CorporationSheet:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Corporation Sheet");
                        NavigatePanelVisible = true;
                        mChildControl = new CorpCorporationSheet((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.EveAllianceList:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Список альянсов");
                        NavigatePanelVisible = true;
                        mChildControl = new EveAllianceList((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.CorpMemberTracking:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Member Tracking");
                        NavigatePanelVisible = true;
                        mChildControl = new CorpMemberTracking((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.AccountBalances:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Баланс кошельков");
                        NavigatePanelVisible = true;
                        mChildControl = new AccountBalances((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.WalletJournalAndTransactions:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle(Convert.ToString(data[0]));
                        NavigatePanelVisible = true;
                        mChildControl = new WalletJournalTransactions((IMainControl)this, mHostWindow, data);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.EveRefTypes:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("RefTypes");
                        NavigatePanelVisible = true;
                        mChildControl = new EveRefTypes((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.MapSovereignty:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Map Sovereignty");
                        NavigatePanelVisible = true;
                        mChildControl = new MapSovereignty((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.Statistics:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Статистика");
                        NavigatePanelVisible = true;
                        mChildControl = new ControlStatistics((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.CorpStarbaseList:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("ПОСы");
                        NavigatePanelVisible = true;
                        mChildControl = new CorpStarbaseList((IMainControl)this, mHostWindow);
                        //mChildControl = new CorpStarbaseDetails((IMainControl)this, mHostWindow, new object[] { "1719099701" });
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.CorpStarbaseListConfig:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Конфигурация ПОСов");
                        NavigatePanelVisible = true;
                        mChildControl = new CorpStarbaseListConfig((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.CorpStarbaseListDetails:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Информация по ПОСу");
                        NavigatePanelVisible = true;
                        mChildControl = new CorpStarbaseDetails((IMainControl)this, mHostWindow, data);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.EveConquerableStationsList:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Conquerable Stations List");
                        NavigatePanelVisible = true;
                        mChildControl = new EveConquerableStationsList((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.Assets:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Активы");
                        NavigatePanelVisible = true;
                        mChildControl = new ControlAssets((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.IndustryJobs:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Industry Jobs");
                        NavigatePanelVisible = true;
                        mChildControl = new ControlIndustryJobs((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.KillLogs:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Kill Logs");
                        NavigatePanelVisible = true;
                        mChildControl = new ControlKillLogs((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.KillLogView:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Kill Log View");
                        NavigatePanelVisible = true;
                        mChildControl = new ControlKillLogView((IMainControl)this, mHostWindow, data);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
                case InterfaceState.ControlTowerFuelPrices:
                    if (mChildControl == null)
                    {
                        HideChildControls();
                        mHostWindow.SetTitle("Стоимость топлива");
                        NavigatePanelVisible = true;
                        mChildControl = new ControlTowerFuelPrices((IMainControl)this, mHostWindow);
                        mChildControl.Parent = this;
                        mChildControl.Dock = DockStyle.Fill;
                        mChildControl.Show();
                        mChildControl.BringToFront();
                        if (saveInStack)
                        {
                            SaveInterfaceStateInStack(state, data);
                        }
                        mInterfaceState = state;
                    }
                    break;
            }
            ShowNavigationPath();

        }
        public void SaveInterfaceState(InterfaceState state, params object[] data)
        {
            SaveInterfaceStateInStack(state, data);
            ShowNavigationPath();
        }
        public void RestoreInterfaceState()
        {
            lock (mStatesStack)
            {
                if (mStatesStack.Count > 0)
                {
                    mStatesStack.Pop();
                    StateStackItem state = mStatesStack.Peek();
                    SwitchToInterfaceState(state.InterfaceState, false, state.Data);
                }
            }
            ShowNavigationPath();
        }

        private void HideChildControls()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl != flpNavigation)
                {
                    ctrl.Parent = null;
                    ctrl.Hide();
                }
            }
        }
        public bool NavigatePanelVisible
        {
            get
            {
                return flpNavigation.Visible;
            }
            set
            {
                flpNavigation.Visible = value;
                if (value)
                {
                    flpNavigation.BringToFront();
                    flpNavigation.Dock = DockStyle.Top;
                }
            }
        }

        public void NavigateControlAdd(Control control)
        {
            control.Parent = flpNavigation;
            //flpNavigation.Controls.Add(control);
        }

        public void NavigateControlRemove(Control control)
        {
            flpNavigation.Controls.Remove(control);
        }

        //public event EventHandler NavigationControlClick;

        public void ProcessException(System.Reflection.MethodBase method, Exception exc)
        {
            ExceptionsViewer.Show(method, exc);
        }

        public Options Options
        {
            get
            {
                return mOptions;
            }
        }

        public DataClass Data
        {
            get
            {
                return mData;
            }
        }

        public Session Session
        {
            get
            {
                return mSession;
            }
        }

        public void SetConfigValue(string key, object value)
        {
            if (mConfig.ContainsKey(key))
                mConfig[key] = value;
            else
                mConfig.Add(key, value);
        }
        public object GetConfigValue(string key)
        {
            if (mConfig.ContainsKey(key))
                return mConfig[key];
            else
                return null;
        }
        #endregion

        private void OnNavigationClick(object sender, EventArgs e)
        {
            if (sender == bBack)
            {
                RestoreInterfaceState();
            }
            //else
            //{
            //    if (NavigationControlClick != null)
            //    {
            //        NavigationControlClick(sender, e);
            //    }
            //}
        }

        private void ShowNavigationPath()
        {
            string str = "";
            foreach (StateStackItem item in mStatesStack)
            {
                str += item.ToString() + ", ";
            }
            str = str.TrimEnd(',', ' ');
            lNavigationPath.Text = str;
        }
        private void SaveInterfaceStateInStack(InterfaceState state, params object[] data)
        {
            lock (mStatesStack)
            {
                mStatesStack.Push(new StateStackItem(state, data));
            }
        }

    }
    public class StateStackItem
    {
        public StateStackItem(InterfaceState state, object[] data)
        {
            mInterfaceState = state;
            mData = data;
        }
        private InterfaceState mInterfaceState;
        private object[] mData;

        public InterfaceState InterfaceState
        {
            get { return mInterfaceState; }
            set { mInterfaceState = value; }
        }
        public object[] Data
        {
            get { return mData; }
            set { mData = value; }
        }

        public override string ToString()
        {
            string str = String.Format("{0}", mInterfaceState.ToString());
            if (mData != null && mData.Length > 0)
            {
                str += " (";
                foreach (object obj in mData)
                {
                    str += String.Format("{0}, ", obj);
                }
                str = str.TrimEnd(' ', ',');
                str += ")";
            }
            return str;
        }
    }
}
