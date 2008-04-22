using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Accounting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            catch (Exception exc)
            {
                ExceptionsViewer.Show(System.Reflection.MethodInfo.GetCurrentMethod(), exc);
            }
        }
    }
}