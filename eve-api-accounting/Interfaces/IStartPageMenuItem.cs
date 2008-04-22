using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting
{
    public interface IStartPageMenuItem
    {
        string Command { get;}
        StartPageMenuItemType Type { get;}
    }
    public enum StartPageMenuItemType
    {
        /// <summary>
        /// вызов справки
        /// </summary>
        Help,
        /// <summary>
        /// просмотр последней поверки
        /// </summary>
        LastCheck,
        /// <summary>
        /// основная команда (поверки, настройки, калибраторы)
        /// </summary>
        Command
    }
}
