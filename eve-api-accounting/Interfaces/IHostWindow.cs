using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting
{
    public interface IHostWindow
    {
        /// <summary>
        /// отображение статуса процесса в статусбаре
        /// </summary>
        /// <param name="progress">прогресс 0-100</param>
        /// <param name="message">текстовое сообщение</param>
        void ShowStatus(int progress, string message);
        /// <summary>
        /// отображение статуса процесса в статусбаре
        /// </summary>
        /// <param name="step">шаг прогресса</param>
        /// <param name="count">общее число шагов</param>
        /// <param name="message">текстовое сообщение</param>
        void ShowStatus(int step, int count, string message);
        /// <summary>
        /// отображение статуса процесса в статусбаре
        /// </summary>
        /// <param name="progress">прогресс 0-100</param>
        void ShowStatus(int progress);
        /// <summary>
        /// отображение статуса процесса в статусбаре
        /// </summary>
        /// <param name="step">шаг прогресса</param>
        /// <param name="count">общее число шагов</param>
        void ShowStatus(int step, int count);
        /// <summary>
        /// отображение статуса процесса в статусбаре
        /// </summary>
        /// <param name="message">текстовое сообщение</param>
        void ShowStatus(string message);
        /// <summary>
        /// установка заголовка окна
        /// </summary>
        /// <param name="title">дополнительная строка к заголовку</param>
        void SetTitle(string title);
        /// <summary>
        /// отображение позиции курсора в DataGridView
        /// </summary>
        /// <param name="dataGridView">Объект DataGridView</param>
        /// <param name="e">Данные события</param>
        void ShowGridPosition(object dataGridView, EventArgs e);
    }
}
