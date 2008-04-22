using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Accounting
{
    public partial class StartPage : UserControl, IStartPage
    {
        public StartPage()
        {
            InitializeComponent();
            //поиск плагинов
            //pbStartImage1.Image = InternalResources.HeaderBackCenter;
            pbStartImage1.BackgroundImage = InternalResources.HeaderBackCenter;
            pbStartImage1.BackgroundImageLayout = ImageLayout.Tile;
        }
        #region IStartPage Members

        public event StartCommandHandler OnCommand;

        public void ClearHelps()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void ClearLastChecks()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void ClearCommands()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void ClearAll()
        {
            flpStartMenuItems.Controls.Clear();
        }

        public void AddItem(Image icon, string title, string description, string command, StartPageMenuItemType type, bool flowBreak)
        {
            switch (type)
            {
                case StartPageMenuItemType.Command:
                    {
                        StartPageMenuCommandItem item = new StartPageMenuCommandItem(
                                icon,
                                title,
                                description,
                                command);
                        item.Click += new EventHandler(item_Click);
                        flpStartMenuItems.Controls.Add(item);
                        flpStartMenuItems.SetFlowBreak(item, flowBreak);
                    }
                    break;
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            IStartPageMenuItem item = (IStartPageMenuItem)sender;
            if (OnCommand != null)
                OnCommand(this, new StartCommandEventArgs(item.Command, item.Type));
        }

        #endregion
    }
}
