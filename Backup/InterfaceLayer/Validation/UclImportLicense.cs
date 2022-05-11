using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PharmaSYSRetailPlus.Common;
using LicenseLib;
using PharmaSYSRetailPlus.BusinessLayer;

namespace PharmaSYSRetailPlus.InterfaceLayer.Validation
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclImportLicense : UserControl, IValidationControl
    {
        public UclImportLicense()
        {
            InitializeComponent();
        }

        #region IValidationControl Members

        public void Initialize()
        {
            this.Location = new Point(10, 10);
            if (OnStateError != null)
            {
            }
        }

        public event EventHandler OnStateOk;

        public event EventHandler OnStateError;

        #endregion

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void btnImportLicense_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                StreamReader sr = new StreamReader(textBox1.Text);
                string licData = sr.ReadToEnd();
                sr.Close();

                Licence lic = new Licence();
                lic.LoadLicense(licData);

                if (General.PharmaSysRetailPlusLicense != null && General.PharmaSysRetailPlusLicense.LicenseType == LicenseTypes.Trial)
                {
                    if (lic.LicenseType == LicenseTypes.Trial)
                    {
                        MessageBox.Show("Invalid License. Please contact your system administrator...!", General.ApplicationTitle);
                        return;
                    }
                }

                lic.ActivationDate = Licence.GetDateTimeString(DateTime.Now);
                if (lic.LicenseType == LicenseTypes.Trial)
                {
                    lic.DeactivationDate = Licence.GetDateTimeString(DateTime.Now.AddDays(LicenseLib.Licence.TRIAL_DAYS));
                }
                lic.LastRunDate = lic.ActivationDate;

                //Save License data
                PharmaSysRetailPlusLic mpLic = new PharmaSysRetailPlusLic();
                mpLic.DeleteLicense();

                mpLic.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                mpLic.Data = lic.GetLicenseData(lic);
                if (mpLic.AddDetails())
                {
                    General.PharmaSysRetailPlusLicense = new LicenseLib.Licence();
                    General.PharmaSysRetailPlusLicense.LoadLicense(mpLic.Data);
                    MessageBox.Show("License imported successfully...!", General.ApplicationTitle);
                    //this.DialogResult = DialogResult.OK;
                    if (OnStateOk != null)
                        OnStateOk(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Please select or enter valid license file name");
            }
        }
    }
}
