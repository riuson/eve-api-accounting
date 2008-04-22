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
        /// ����� �������
        /// </summary>
        Help,
        /// <summary>
        /// �������� ��������� �������
        /// </summary>
        LastCheck,
        /// <summary>
        /// �������� ������� (�������, ���������, �����������)
        /// </summary>
        Command
    }
}
