using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using System.IO;
using EcoMart.BusinessLayer;
using EcoMart.InterfaceLayer.Validation;
using EcoMartLicenseLib;

namespace EcoMart.InterfaceLayer
{
     [System.ComponentModel.ToolboxItem(false)]
    public partial class FormShopDetails : Form
    {
       
          #region Declaration
       //  UclImportLicense uclImportLic;
      //   public ShopDetailsForForm ShopDetailsForForm;
        ShopDetailsForForm ShopDetailsForForm = new ShopDetailsForForm();
        #endregion Declaration

        public FormShopDetails()
        {
            InitializeComponent();            
            FillDetails();
            txtShopOwnersName.Focus();
        }

        private void FillDetails()
        {
            txtNoOfUsers.Enabled = true;
            ShopDetailsForForm.FillShopDetails();
            txtShopOwnersName.Text = ShopDetailsForForm.FShopOwnersName;
            txtShopName.Text = ShopDetailsForForm.FShopName;
            txtAddress1.Text = ShopDetailsForForm.FShopAddress1;
            txtAddress2.Text = ShopDetailsForForm.FShopAddress2;
            txtTelephone.Text = ShopDetailsForForm.FShopTelephone;
            txtMobileNo.Text = ShopDetailsForForm.FShopMobileNumber;
            txtEmailID.Text = ShopDetailsForForm.FShopEmailID;
            txtDLN.Text = ShopDetailsForForm.FShopDLN;
            txtDistributorDrugLicNo.Text = ShopDetailsForForm.FShopDLNDist;
            txtJurisdiction.Text = ShopDetailsForForm.FShopJurisdiction;
            txtVATTINV.Text = ShopDetailsForForm.FShopVATTINV;
            txtVATTINC.Text = ShopDetailsForForm.FShopVATTINC;
            txtNoOfUsers.Text = ShopDetailsForForm.FShopNumberOfUsers;
            txtLBT.Text = ShopDetailsForForm.FShopLBT;
            txtAIOCDACode.Text = ShopDetailsForForm.FAIOCDACode;
            txtScorgCode.Text = ShopDetailsForForm.FSCORGCode;
           
            if (ShopDetailsForForm.FShopDistributorSale == null)
                ShopDetailsForForm.FShopDistributorSale = "N";
            if (ShopDetailsForForm.FShopDistributorSale == "Y")
                cbDistributorSale.Checked = true;
            else
                cbDistributorSale.Checked = false;
            if (ShopDetailsForForm.FShopChangeCounterSaleType == null)
                ShopDetailsForForm.FShopChangeCounterSaleType = "N";
            if (ShopDetailsForForm.FShopChangeCounterSaleType == "Y")
                cbChangeCounterSaleType.Checked = true;
            else
                cbChangeCounterSaleType.Checked = false;
            if (ShopDetailsForForm.FShopDebitNoteWithLooseQuantity == "Y")
                cbDebitNoteInLooseQuantity.Checked = true;
            else
                cbDebitNoteInLooseQuantity.Checked = false;
            
            
            //if (_ShopDetailsForForm. mBarCode == "Y")
            //    cbBarCode.Checked = true;
            //else
            //    cbBarCode.Checked = false;
            //if (mTransferToTally == "Y")
            //    cbTransferToTally.Checked = true;
            //else
            //    cbTransferToTally.Checked = false;
            //if (mLicenseType == "1")
            //    cmbLicenseType.SelectedIndex = 1;
            //else if (mLicenseType == "2")
            //    cmbLicenseType.SelectedIndex = 2;    
            txtAIOCDACode.Focus();
        }

       
        private void btnGenerateLicense_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
              //  GenerateLicense();
                ClearControls();
            }

            else
                MessageBox.Show("Unable to Generate..", "Licenses Portal", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

       

        private void ClearControls()
        {
            pnlImport.Visible = false;
            txtShopOwnersName.Text = "";
            txtShopName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtTelephone.Text = "";
            txtMobileNo.Text = "";
            txtEmailID.Text = "";
            txtDLN.Text = "";
            txtDistributorDrugLicNo.Text = "";
            txtJurisdiction.Text = "";
            txtVATTINV.Text = "";
            txtVATTINC.Text = "";
            txtNoOfUsers.Text = "1";
            txtLBT.Text = "";
            cbChangeCounterSaleType.Checked = false;
            cbDistributorSale.Checked = false;
            cbDebitNoteInLooseQuantity.Checked = false;
           
        }
        private bool ValidateData()
        {
            bool retValue = true;
            try
            {
                if (txtShopOwnersName.Text == null || txtShopOwnersName.Text == "")
                {
                    retValue = false;                    
                }
                else if (txtShopName.Text == null || txtShopName.Text == "")
                {
                    retValue = false;                   
                }
                else if (txtAddress1.Text == null || txtAddress1.Text == "")
                {
                    retValue = false;                   
                }
                else if (txtAddress2.Text == null || txtAddress2.Text == "")
                {
                    retValue = false;                   
                }
                else if (txtTelephone.Text == null || txtTelephone.Text == "")
                {
                    retValue = false;                   
                }
                else if (txtMobileNo.Text == null || txtMobileNo.Text == "")
                {
                    retValue = false;                   
                }
                else if (txtDLN.Text == null || txtDLN.Text == "")
                {
                    retValue = false;                    
                }
                else if (txtVATTINV.Text == null || txtVATTINV.Text == "")
                {
                    retValue = false;                   
                }
                //if (cmbLicenseType.Text == null || cmbLicenseType.Text == "")
                //{
                //    retValue = false;
                //}
                //else if (cmbLicenseType.SelectedIndex == 2)
                //{
                //    int _noofusers = 0;
                //    int.TryParse(txtNoOfUsers.Text.ToString(), out  _noofusers);                   
                //    if (_noofusers == 0)
                //    {
                //     //   retValue = false;                     
                //        _noofusers = 1;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private void txtShopOwnersName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtAIOCDACode.Focus();
                    break;
                case Keys.Down:
                    txtAIOCDACode.Focus();
                    break;
            }
        }
        private void txtShopName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtAddress1.Focus();
                    break;
                case Keys.Down:
                    txtAddress1.Focus();
                    break;
                case Keys.Up:
                    txtShopOwnersName.Focus();
                    break;
            }
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtAddress2.Focus();
                    break;
                case Keys.Down:
                    txtAddress2.Focus();
                    break;
                case Keys.Up:
                    txtShopName.Focus();
                    break;
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtTelephone.Focus();
                    break;
                case Keys.Down:
                    txtTelephone.Focus();
                    break;
                case Keys.Up:
                    txtAddress1.Focus();
                    break;
            }
        }

        private void txtTelephone_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtMobileNo.Focus();
                    break;
                case Keys.Down:
                    txtMobileNo.Focus();
                    break;
                case Keys.Up:
                    txtAddress2.Focus();
                    break;
            }
        }

        private void txtMobileNo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtEmailID.Focus();
                    break;
                case Keys.Down:
                    txtEmailID.Focus();
                    break;
                case Keys.Up:
                    txtTelephone.Focus();
                    break;
            }
        }

        private void txtEmailID_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtDLN.Focus();
                    break;
                case Keys.Down:
                    txtDLN.Focus();
                    break;
                case Keys.Up:
                    txtMobileNo.Focus();
                    break;
            }

        }

        private void txtDLN_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtJurisdiction.Focus();
                    break;
                case Keys.Down:
                    txtJurisdiction.Focus();
                    break;
                case Keys.Up:
                    txtEmailID.Focus();
                    break;
            }
        }

        private void txtJurisdiction_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtVATTINV.Focus();
                    break;
                case Keys.Down:
                    txtVATTINV.Focus();
                    break;
                case Keys.Up:
                    txtDLN.Focus();
                    break;
            }
        }

        private void txtVATTINV_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtVATTINC.Focus();
                    break;
                case Keys.Down:
                    txtVATTINC.Focus();
                    break;
                case Keys.Up:
                    txtJurisdiction.Focus();
                    break;
            }
        }

        private void txtVATTINC_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtVATTINC.Focus();
                    break;
                case Keys.Down:
                    txtVATTINC.Focus();
                    break;
                case Keys.Up:
                    txtVATTINV.Focus();
                    break;
            }
        }

      

        

        private void cmbLicenseType_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cmbLicenseType.SelectedItem != null)
            //{
            //    ComboBoxItem item = (ComboBoxItem)cmbLicenseType.SelectedItem;
            //    if (item.Value == LicenseCommon.LICENSEID_FULL)
            //    {
            //        txtNoOfUsers.Enabled = true;
            //    }
            //    else
            //    {
            //        txtNoOfUsers.Text = "";
            //        txtNoOfUsers.Enabled = false;
            //    }

            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BtnSaveClick();
        }

        private void BtnSaveClick()
        {
            ShopDetailsForForm SDF = new ShopDetailsForForm();
            if (txtShopOwnersName.Text != null)
            ShopDetailsForForm.FShopOwnersName = txtShopOwnersName.Text.ToString();
            if (txtAIOCDACode.Text != null)
            ShopDetailsForForm.FAIOCDACode = txtAIOCDACode.Text.ToString();
            if (txtScorgCode.Text != null)
            ShopDetailsForForm.FSCORGCode = txtScorgCode.Text.ToString();
            if (txtTelephone.Text != null)
                ShopDetailsForForm.FShopTelephone = txtTelephone.Text.ToString();
            if (txtMobileNo.Text != null)
                ShopDetailsForForm.FShopMobileNumber = txtMobileNo.Text.ToString();
            if (txtAddress2.Text != null)
                ShopDetailsForForm.FShopAddress2 = txtAddress2.Text.ToString();
            if (txtEmailID.Text != null)
                ShopDetailsForForm.FShopEmailID = txtEmailID.Text.ToString();
            SDF.SaveDetails(ShopDetailsForForm.FShopOwnersName, ShopDetailsForForm.FAIOCDACode, ShopDetailsForForm.FSCORGCode, ShopDetailsForForm.FShopAddress2, ShopDetailsForForm.FShopTelephone, ShopDetailsForForm.FShopMobileNumber, ShopDetailsForForm.FShopEmailID);

            MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            BtnExitClick();                 
        }

        private void BtnExitClick()
        {
            this.Close();
        }

        private void cbChangeCounterSaleType_Click(object sender, EventArgs e)
        {
            txtDistributorDrugLicNo.Focus();
        }

        private void txtAIOCDACode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtScorgCode.Focus();
            else if (e.KeyCode == Keys.Up)
                txtShopOwnersName.Focus();
        }

        private void txtScorgCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
            if (e.KeyCode == Keys.Up)
                txtAIOCDACode.Focus();
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                btnExit.Focus();
        }
     
   

        private void Import_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox2.Text))
            {
               
                StreamReader sr = new StreamReader(textBox2.Text);
                string licData = sr.ReadToEnd();
                sr.Close();

                Licence lic = new Licence();
                lic.LoadLicense(licData);

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
                    lic.DeactivationDate = Licence.GetDateTimeString(DateTime.Now.AddDays(Licence.TRIAL_DAYS));
                }
                lic.LastRunDate = lic.ActivationDate;

                //Save License data
                EcoMartLic mpLic = new EcoMartLic();
                mpLic.DeleteLicense();

                mpLic.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                mpLic.Data = lic.GetLicenseData(lic);
                if (mpLic.AddDetails())
                {
                    General.EcoMartLicense = new EcoMartLicenseLib.Licence();
                    General.EcoMartLicense.LoadLicense(mpLic.Data);
                    MessageBox.Show("License imported successfully...!", General.ApplicationTitle);
                    this.DialogResult = DialogResult.OK;
                    //if (OnStateOk != null)
                    //    OnStateOk(this, new EventArgs());
                    General.DisposeConnection();
                    Environment.Exit(0);
                }
            }
            else
            {
                MessageBox.Show("Please select or enter valid license file name");
            }

            pnlImport.Visible = false;
           

        }
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
            }
        }

        private void btnImportLicense_Click(object sender, EventArgs e)
        {
            pnlImport.Visible = true;
        }
    }
}
