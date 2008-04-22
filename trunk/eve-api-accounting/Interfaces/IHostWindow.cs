using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting
{
    public interface IHostWindow
    {
        /// <summary>
        /// ����������� ������� �������� � ����������
        /// </summary>
        /// <param name="progress">�������� 0-100</param>
        /// <param name="message">��������� ���������</param>
        void ShowStatus(int progress, string message);
        /// <summary>
        /// ����������� ������� �������� � ����������
        /// </summary>
        /// <param name="step">��� ���������</param>
        /// <param name="count">����� ����� �����</param>
        /// <param name="message">��������� ���������</param>
        void ShowStatus(int step, int count, string message);
        /// <summary>
        /// ����������� ������� �������� � ����������
        /// </summary>
        /// <param name="progress">�������� 0-100</param>
        void ShowStatus(int progress);
        /// <summary>
        /// ����������� ������� �������� � ����������
        /// </summary>
        /// <param name="step">��� ���������</param>
        /// <param name="count">����� ����� �����</param>
        void ShowStatus(int step, int count);
        /// <summary>
        /// ����������� ������� �������� � ����������
        /// </summary>
        /// <param name="message">��������� ���������</param>
        void ShowStatus(string message);
        /// <summary>
        /// ��������� ��������� ����
        /// </summary>
        /// <param name="title">�������������� ������ � ���������</param>
        void SetTitle(string title);
        /// <summary>
        /// ����������� ������� ������� � DataGridView
        /// </summary>
        /// <param name="dataGridView">������ DataGridView</param>
        /// <param name="e">������ �������</param>
        void ShowGridPosition(object dataGridView, EventArgs e);
    }
}
