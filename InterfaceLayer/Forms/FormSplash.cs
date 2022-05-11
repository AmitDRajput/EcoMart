using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;

namespace EcoMart.InterfaceLayer
{
    public partial class FormSplash : Form
    {
        public FormSplash()
        {
            this.Cursor = Cursors.WaitCursor;
            InitializeComponent();
            lblVersion.Text = General.PharmaSYSVersion;
            this.Icon = EcoMart.Properties.Resources.Icon;
        }

        public void SetProgress(string msg, int progress)
        {
            lblProgressMsg.Text = msg;
            pBar.Value = progress;
            this.Refresh();
        }
    }
}
