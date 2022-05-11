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
    public partial class UclNoLicense : UserControl, IValidationControl
    {
        public UclNoLicense()
        {
            InitializeComponent();
        }

        #region IValidationControl Members

        public void Initialize()
        {
            this.Location = new Point(10, 10);
            if (OnStateOk != null)
            {
            }
            if (OnStateError != null)
            {
            }
        }

        public event EventHandler OnStateOk;

        public event EventHandler OnStateError;

        #endregion
    }
}
