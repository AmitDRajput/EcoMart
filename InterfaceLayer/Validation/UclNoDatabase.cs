using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EcoMart.InterfaceLayer.Validation
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclNoDatabase : UserControl, IValidationControl
    {
        private bool isConnectDatabase = true;
        public UclNoDatabase()
        {
            InitializeComponent();
        }

        public bool IsConnectDatabase
        {
            get { return isConnectDatabase; }
            set { isConnectDatabase = value; }
        }

        #region IValidationControl Members

        public void Initialize()
        {
            this.Location = new Point(10, 10);
            if (OnStateError != null)
            {
            }
            if (OnStateOk != null)
            {
            }
        }

        public event EventHandler OnStateOk;

        public event EventHandler OnStateError;

        #endregion

        private void rbtnConnectDatabase_CheckedChanged(object sender, EventArgs e)
        {
            IsConnectDatabase = rbtnConnectDatabase.Checked;
        }

        private void rbtnCreateDatbase_CheckedChanged(object sender, EventArgs e)
        {
            IsConnectDatabase = rbtnConnectDatabase.Checked;
        }
    }
}
