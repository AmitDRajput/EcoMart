using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.InterfaceLayer.Validation
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclTrialLicense : UserControl, IValidationControl
    {
        public UclTrialLicense()
        {
            InitializeComponent();
        }

        #region IValidationControl Members

        public void Initialize()
        {
            this.Location = new Point(10, 10);
            DateTime todaysDate = DateTime.Now;
            DateTime deactivationDate = LicenseLib.Licence.GetDateTime(General.PharmaSysRetailPlusLicense.DeactivationDate);

            TimeSpan span = deactivationDate.Subtract(todaysDate);
            lblTrialDays.Text = lblTrialDays.Text + span.Days.ToString();

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
