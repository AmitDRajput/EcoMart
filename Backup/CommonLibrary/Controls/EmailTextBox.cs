using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PharmaSYSPlus.CommonLibrary
{
    public partial class EmailTextBox : TextBox
    {
        public EmailTextBox()
        {
            InitializeComponent();
        }
               
        private void EmailTextBox_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (this.Text.Trim().Length > 0)
            {
                if (!rEMail.IsMatch(this.Text.Trim()))
                {
                    MessageBox.Show("E-Mail expected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.SelectAll();
                    e.Cancel = true;
                }
            }
        }
    }
}
