using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    public partial class FormSplash : Form
    {
        public FormSplash()
        {
            this.Cursor = Cursors.WaitCursor;
            InitializeComponent();            
        }

        public void SetProgress(string msg, int progress)
        {
            lblProgressMsg.Text = msg;
            pBar.Value = progress;
            this.Refresh();
        }
    }
}
