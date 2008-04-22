using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Accounting
{
    public interface IStartPage
    {
        event StartCommandHandler OnCommand;
        void ClearHelps();
        void ClearLastChecks();
        void ClearCommands();
        void ClearAll();
        void AddItem(Image icon, string title, string description, string command, StartPageMenuItemType type, bool flowBreak);
    }
    #region event StartCommand
    public class StartCommandEventArgs : EventArgs
    {
        public StartCommandEventArgs(string command, StartPageMenuItemType type)
        {
            mCommand = command;
            mType = type;
        }
        private string mCommand;
        private StartPageMenuItemType mType;
        public string Command
        {
            get { return mCommand; }
        }
        public StartPageMenuItemType Type
        {
            get { return mType; }
        }
    }
    public delegate void StartCommandHandler(object sender, StartCommandEventArgs ea);
    #endregion
}
