using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting;
using System.Runtime.Remoting.Messaging;

namespace Accounting
{
    public partial class FormMain : Form, IHostWindow
    {
        public FormMain()
        {
            InitializeComponent();
            mBaseTitle = this.Text;
#if DEBUG
            mBaseTitle += " (debug)";
#endif
        }
        MainControl mMainControl;
        string mBaseTitle;

        #region асинхронный конструктор
        private void FormMain_Shown(object sender, EventArgs e)
        {
            ShowStatus(10, "Инициализация интерфейса");
#if DEBUG
            mMainControl = new MainControl((IHostWindow)this);
            mMainControl.Parent = this;
            mMainControl.Dock = DockStyle.Fill;
            mMainControl.BringToFront();
#else
            InterfaceConstructorAsync interfaceCtor = this.InterfaceCreate;
            IAsyncResult asycnCtor = interfaceCtor.BeginInvoke(ref mMainControl, new AsyncCallback(InterfaceCreateDoneCallback), mMainControl);
#endif
        }
        delegate void InterfaceConstructorAsync(ref MainControl mainControl);
        private void InterfaceCreate(ref MainControl mainControl)
        {
            //Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            mainControl = new MainControl((IHostWindow)this);
        }
        private void InterfaceCreateDoneCallback(IAsyncResult result)
        {
            //Text = "Конструктор завершён";
            try
            {
                AsyncResult r = (AsyncResult)result;
                InterfaceConstructorAsync command = (InterfaceConstructorAsync)r.AsyncDelegate;
                MainControl mainControl = null;
                command.EndInvoke(ref mainControl, result);
                mMainControl = mainControl;
                if (this.InvokeRequired)
                {
                    this.Invoke((EventHandler)delegate
                    {
                        labelLoading.Hide();
                        mMainControl.Parent = this;
                        mMainControl.Dock = DockStyle.Fill;
                        mMainControl.BringToFront();
                    });
                }
                else
                {
                    labelLoading.Hide();
                    mMainControl.Parent = this;
                    mMainControl.Dock = DockStyle.Fill;
                    mMainControl.BringToFront();
                }
            }
            catch (Exception exc)
            {
                mMainControl.ProcessException(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
        #endregion

        #region IHostWindow Members

        public void ShowStatus(int progress, string message)
        {
            if (ssStatusMain.InvokeRequired)
            {
                ssStatusMain.Invoke((EventHandler)delegate
                {
                    ShowStatus(progress, message);
                });
            }
            else
            {
                tsslStatusProgress.Maximum = 100;
                tsslStatusProgress.Value = progress;
                tsslMessage.Text = message;
                tsslStatusProgress.Invalidate();
                tsslMessage.Invalidate();
            }
        }

        public void SetTitle(string title)
        {
            if (title != "")
                this.Text = String.Format("{0} - {1}", mBaseTitle, title);
            else
                this.Text = mBaseTitle;
        }

        public void ShowStatus(int progress)
        {
            if (ssStatusMain.InvokeRequired)
            {
                ssStatusMain.Invoke((EventHandler)delegate
                {
                    ShowStatus(progress);
                });
            }
            else
            {
                tsslStatusProgress.Value = progress;
                tsslStatusProgress.Maximum = 100;
                tsslStatusProgress.Invalidate();
            }
        }

        public void ShowStatus(string message)
        {
            if (ssStatusMain.InvokeRequired)
            {
                ssStatusMain.Invoke((EventHandler)delegate
                {
                    ShowStatus(message);
                });
            }
            else
            {
                tsslMessage.Text = message;
                tsslMessage.Invalidate();
            }
        }

        public void ShowStatus(int step, int count, string message)
        {
            if (ssStatusMain.InvokeRequired)
            {
                ssStatusMain.Invoke((EventHandler)delegate
                {
                    ShowStatus(step, count, message);
                });
            }
            else
            {
                tsslStatusProgress.Maximum = count;
                tsslStatusProgress.Value = step;
                tsslMessage.Text = message;
                tsslStatusProgress.Invalidate();
                tsslMessage.Invalidate();
            }
        }

        public void ShowStatus(int step, int count)
        {
            if (ssStatusMain.InvokeRequired)
            {
                ssStatusMain.Invoke((EventHandler)delegate
                {
                    ShowStatus(step, count);
                });
            }
            else
            {
                tsslStatusProgress.Maximum = count;
                tsslStatusProgress.Value = step;
                tsslStatusProgress.Invalidate();
            }
        }

        public void ShowGridPosition(object dataGridView, EventArgs e)
        {
            DataGridView dgv = (DataGridView)dataGridView;
            if (dgv.CurrentRow != null)
            {
                if (ssStatusMain.IsHandleCreated)
                {
                    ssStatusMain.Invoke((EventHandler)delegate
                    {
                        tsslPosition.Text = String.Format("Строка: {0}/{1}", dgv.CurrentRow.Index + 1, dgv.Rows.Count);
                    });
                }
            }
        }
        #endregion

        private void timerGMT_Tick(object sender, EventArgs e)
        {
            tsslGmtTime.Text = String.Format("GMT: {0}", DateTime.UtcNow);
        }
    }
}