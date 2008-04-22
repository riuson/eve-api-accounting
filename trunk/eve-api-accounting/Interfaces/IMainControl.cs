using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Accounting
{
    public interface IMainControl
    {
        //void SetInterfaceState(InterfaceState state, bool needPush);
        //void SetInterfaceState(InterfaceState state, bool needPush, params object[] data);
        void SwitchToInterfaceState(InterfaceState state, bool saveInStack);
        void SwitchToInterfaceState(InterfaceState state, bool saveInStack, params object[] data);
        void SaveInterfaceState(InterfaceState state, params object[] data);

        void RestoreInterfaceState();

        bool NavigatePanelVisible { get; set;}
        void NavigateControlAdd(Control control);
        void NavigateControlRemove(Control control);
        //event EventHandler NavigationControlClick;

        void ProcessException(System.Reflection.MethodBase method, Exception exc);

        Options Options { get;}
        DataClass Data { get;}
        Session Session { get;}
        void SetConfigValue(string key, object value);
        object GetConfigValue(string key);
    }
    public enum InterfaceState
    {
        /// <summary>
        /// на шаг назад
        /// </summary>
        Back,
        /// <summary>
        /// на стартовую страницу
        /// </summary>
        StartPage,
        /// <summary>
        /// на страницу настройки программы
        /// </summary>
        ApplicationSetup,
        /// <summary>
        /// /corp/CorporationSheet.xml.aspx
        /// </summary>
        CorporationSheet,
        /// <summary>
        /// /eve/AllianceList.xml.aspx
        /// </summary>
        EveAllianceList,
        /// <summary>
        /// /corp/MemberTracking.xml.aspx
        /// </summary>
        CorpMemberTracking,
        /// <summary>
        /// /char/AccountBalance.xml.aspx
        /// /corp/AccountBalance.xml.aspx
        /// </summary>
        AccountBalances,
        /// <summary>
        /// /char/WalletJournal.xml.aspx
        /// /corp/WalletJournal.xml.aspx
        /// /char/WalletTransactions.xml.aspx
        /// /corp/WalletTransactions.xml.aspx
        /// </summary>
        WalletJournalAndTransactions,
        /// <summary>
        /// /eve/RefTypes.xml.aspx
        /// </summary>
        EveRefTypes,
        /// <summary>
        /// /map/Sovereignty.xml.aspx
        /// </summary>
        MapSovereignty,
        /// <summary>
        /// Статистика
        /// </summary>
        Statistics,
        /// <summary>
        /// /corp/StarbaseList.xml.aspx
        /// /corp/StarbaseDetail.xml.aspx
        /// </summary>
        CorpStarbaseList,
        /// <summary>
        /// /eve/ConquerableStationList.xml.aspx
        /// </summary>
        EveConquerableStationsList,
        /// <summary>
        /// /char/AssetList.xml.aspx 
        /// /corp/AssetList.xml.aspx
        /// </summary>
        Assets,
        /// <summary>
        /// /char/IndustryJobs.xml.aspx 
        /// /corp/IndustryJobs.xml.aspx 
        /// </summary>
        IndustryJobs,
        /// <summary>
        /// /char/KillLog.xml.aspx
        /// /corp/KillLog.xml.aspx
        /// </summary>
        KillLogs,
        KillLogView,
        /// <summary>
        /// Конфигурация посов
        /// </summary>
        CorpStarbaseListConfig,
        /// <summary>
        /// Детальная информация по ПОСу
        /// </summary>
        CorpStarbaseListDetails,
        /// <summary>
        /// Список цен на топливо
        /// </summary>
        ControlTowerFuelPrices
    }
}
