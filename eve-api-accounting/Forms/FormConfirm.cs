using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Accounting
{
    public partial class FormConfirm : Form
    {
        public FormConfirm()
        {
            InitializeComponent();
            lCode.Text = Guid.NewGuid().ToString();
            tbCode.Text = "";
        }

        private void tbCode_TextChanged(object sender, EventArgs e)
        {
            bOk.Enabled = lCode.Text == tbCode.Text;
        }

        public string Promt
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        public string CancelText
        {
            get
            {
                return bCancel.Text;
            }
            set
            {
                bCancel.Text = value;
            }
        }
    }
}