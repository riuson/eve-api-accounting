using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Accounting
{
    internal partial class FExceptionMessage : Form
    {
        public FExceptionMessage()
        {
            InitializeComponent();
            mShortDisplayType = true;
            Icon icon = SystemIcons.Error;
            Bitmap bmpIcon = new Bitmap(icon.Width, icon.Height);
            Graphics gr = Graphics.FromImage(bmpIcon);
            gr.DrawIcon(icon, 0, 0);
            pbIcon.Image = bmpIcon;
            pbIcon.Width = icon.Width;
            pbIcon.Height = icon.Height;
            System.Media.SystemSounds.Beep.Play();
        }
        private string mExcMsg;
        private string mExcModule;
        private string mExcTracing;
        private bool mShortDisplayType;
        public string ExceptionMessage
        {
            get
            {
                return mExcMsg;
            }
            set
            {
                mExcMsg = value;
                ShowComments();
            }
        }
        public string Module
        {
            get
            {
                return mExcModule;
            }
            set
            {
                mExcModule = value;
                ShowComments();
            }
        }
        public string StackTracing
        {
            get
            {
                return mExcTracing;
            }
            set
            {
                mExcTracing = value;
                ShowComments();
            }
        }
        private void ShowComments()
        {
            //string stacktrace = String.Format("Исключение: {0}{1}Модуль: {2}{1}Стек:{1}{3}",
            //    _excMsg,
            //    Environment.NewLine,
            //    _excModule,
            //    _excTracing);
            //string msg = String.Format("При работе программы в модуле{2}\"{0}\"{2}возникла ошибка{2}\"{1}\"",
            //    _excModule,
            //    _excMsg,
            //    Environment.NewLine);
            //lExceptionMessage.Text = msg;
            ////richTextBox1.Clear();
            ////richTextBox1.AppendText(stacktrace);
            //rtbDetails.Text = stacktrace;
            SetDisplayType(mShortDisplayType);
        }

        private void llChangeDisplayType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mShortDisplayType = !mShortDisplayType;
            SetDisplayType(mShortDisplayType);
        }
        private void SetDisplayType(bool shortmessage)
        {
            if (shortmessage)
            {
                lMessage.Text = String.Format("При работе программы в модуле {0}{2}возникла ошибка:\"{1}\"",
                    mExcModule,
                    mExcMsg,
                    Environment.NewLine);
                tbMessage.Hide();
                lMessage.Show();
                llChangeDisplayType.Text = "Подробно >>";
                this.Size = new Size(484, 148);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                //this.MaximizeBox = false;
            }
            else
            {
                tbMessage.Text = String.Format("Исключение: {0}{1}Модуль: {2}{1}Стек:{1}{3}",
                    mExcMsg,
                    Environment.NewLine,
                    mExcModule,
                    mExcTracing);
                lMessage.Hide();
                tbMessage.Show();
                Graphics gr = tbMessage.CreateGraphics();
                SizeF szf = gr.MeasureString(tbMessage.Text, tbMessage.Font);
                llChangeDisplayType.Text = "Кратко <<";
                this.FormBorderStyle = FormBorderStyle.Sizable;
                //this.MaximizeBox = true;
            }
        }

    }
    public class ExceptionsViewer
    {
        /// <summary>
        /// Перехват и отображение исключения в программе. Пример:
        /// <example>
        /// <c>
        /// ExceptionHandler.HandleException(System.Reflection.MethodInfo.GetCurrentMethod().Name, exc);
        /// </c>
        /// </example>
        /// </summary>
        /// <param name="method">Функция</param>
        /// <param name="exc">информация исключения</param>
        static public void Show(System.Reflection.MethodBase method, Exception exc)
        {
            string message, messageSimple;
            try
            {
                message = String.Format("Exception: {0}{1}Module: {2}{1}{3}",
                    exc.Message,
                    Environment.NewLine,
                    method.Name,
                    exc.StackTrace);
                messageSimple = String.Format("Exception: \"{0}\" in module \"{1}\"", exc.Message, method.Name);
                //NetLog.PushErrorLogStatic(MessageType.Error, messageSimple, message);
                //message = "Exception: " + exc.Message +
                //    Environment.NewLine +
                //    "Module: " + exc.TargetSite.Module +
                //    Environment.NewLine +
                //    "Method: " + exc.StackTrace;//exc.TargetSite.Name;
                //MessageBox.Show(message, caption, MessageBoxButtons.OK);
                FExceptionMessage form = new FExceptionMessage();
                form.Module = method.Name;
                form.ExceptionMessage = exc.Message;
                form.StackTracing = exc.StackTrace;
                form.ShowDialog();
                System.Diagnostics.Debug.Write(message);
            }
            finally { }
        }
    }
}