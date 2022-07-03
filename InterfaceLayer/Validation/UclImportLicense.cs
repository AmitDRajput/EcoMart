using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using EcoMart.Common;
using EcoMartLicenseLib;
using EcoMart.BusinessLayer;
using System.Configuration;

namespace EcoMart.InterfaceLayer.Validation
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
        private bool CheckDBConnectionStringAzure()
        {
            bool RetValue = true;
            try
            {
                string connStrDB = ConfigurationManager.ConnectionStrings["EcoMartConnectionString"].ConnectionString;
                string connStrDBAzure = ConfigurationManager.ConnectionStrings["EcoMartAzureConnectionString"].ConnectionString;

                if (connStrDB.ToLower() == connStrDBAzure.ToLower())
                {
                    RetValue = false;
                    MessageBox.Show("Error in license import. You can't use CNF/Stockist License/Database as Azure Database.", General.ApplicationTitle);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                //   retValue = false;
            }
            return RetValue;
        }

        private void btnImportLicense_Click(object sender, EventArgs e)
        {
            bool IsValidConnectionString = true;
            if (File.Exists(textBox1.Text))
            {
                StreamReader sr = new StreamReader(textBox1.Text);
                string licData = sr.ReadToEnd();
                sr.Close();

                Licence lic = new Licence();
                if (lic.LoadLicense(licData))
                {
                    if (General.EcoMartLicense != null && General.EcoMartLicense.LicenseType == LicenseTypes.Trial)
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
                        lic.DeactivationDate = Licence.GetDateTimeString(DateTime.Now.AddDays(EcoMartLicenseLib.Licence.TRIAL_DAYS));
                    }
                    lic.LastRunDate = lic.ActivationDate;

                    //Save License data
                    EcoMartLic mpLic = new EcoMartLic();
                    mpLic.DeleteLicense();

                    mpLic.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    mpLic.Data = lic.GetLicenseData(lic);
                    if (lic.ApplicationType != ApplicationTypes.EcoMart)
                    {
                        IsValidConnectionString = CheckDBConnectionStringAzure();
                    }
                    if (IsValidConnectionString && mpLic.AddDetails())
                    {
                        General.EcoMartLicense = new EcoMartLicenseLib.Licence();
                        General.EcoMartLicense.LoadLicense(mpLic.Data);
                        MessageBox.Show("License imported successfully...!" + Environment.NewLine + "Click 'Next' to continue ...", General.ApplicationTitle);
                        //this.DialogResult = DialogResult.OK;
                        if (OnStateOk != null)
                            OnStateOk(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Unable to read license file. Please select or enter valid license file...!!!");
                }
            }
            else
            {
                MessageBox.Show("Please select or enter valid license file name");
            }
        }
    }
}
