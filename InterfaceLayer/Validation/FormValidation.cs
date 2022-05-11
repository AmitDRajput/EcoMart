using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using EcoMartLicenseLib;
using EcoMart.BusinessLayer;
using EcoMart.Common;
using EcoMart.InterfaceLayer.Validation;

namespace EcoMart.InterfaceLayer
{
    public partial class FormValidation : Form
    { 
        IValidationControl CurrentControl;
        UclNoDatabase uclNoDB;
        UclConnectDatabase uclConnectDB;
        UclCreateDatabase uclCreateDB;
        UclNoLicense uclNoLic;
        UclTrialLicense uclTrialLic;
        UclTrialLicenseExpired uclTrialLicExpired;
        UclImportLicense uclImportLic;
        UclAssociation uclAssociation;

        public enum WizardStates
        {
            State_None = 0,
            State_NoConnection = 1,
            State_CreateDatabase = 2,
            State_ConnnectDatabase = 3,
            State_TrialLicense = 4,
            State_NoLicense = 5,
            State_Import = 6,
            State_Association = 7,
        }

        WizardStates currentState;
        public FormValidation()
        {
            
            InitializeComponent();
            lblVersion.Text = General.PharmaSYSVersion;
         //   this.Icon = EcoMart.Properties.Resources.Icon;
            this.Text = General.ApplicationTitle;           
            CreateValidationControls();
        }

        private void CreateValidationControls()
        {
            try
            {
                uclNoDB = new UclNoDatabase();
                uclNoDB.OnStateOk += new EventHandler(uclNoDB_OnStateOk);
                uclNoDB.OnStateError += new EventHandler(uclNoDB_OnStateError);

                uclCreateDB = new UclCreateDatabase();
                uclCreateDB.OnStateOk += new EventHandler(uclCreateDB_OnStateOk);
                uclCreateDB.OnStateError += new EventHandler(uclCreateDB_OnStateError);

                uclConnectDB = new UclConnectDatabase();
                uclConnectDB.OnStateOk += new EventHandler(uclConnectDB_OnStateOk);
                uclConnectDB.OnStateError += new EventHandler(uclConnectDB_OnStateError);

                uclNoLic = new UclNoLicense();
                uclNoLic.OnStateOk += new EventHandler(uclNoLic_OnStateOk);
                uclNoLic.OnStateError += new EventHandler(uclNoLic_OnStateError);

                uclTrialLic = new UclTrialLicense();
                uclTrialLic.OnStateOk += new EventHandler(uclTrialLic_OnStateOk);
                uclTrialLic.OnStateError += new EventHandler(uclTrialLic_OnStateError);

                uclTrialLicExpired = new UclTrialLicenseExpired();
                uclTrialLicExpired.OnStateOk += new EventHandler(uclTrialLicExpired_OnStateOk);
                uclTrialLicExpired.OnStateError += new EventHandler(uclTrialLicExpired_OnStateError);

                uclImportLic = new UclImportLicense();
                uclImportLic.OnStateOk += new EventHandler(uclImportLic_OnStateOk);
                uclImportLic.OnStateError += new EventHandler(uclImportLic_OnStateError);

                uclAssociation = new UclAssociation();
                uclAssociation.OnStateOk += new EventHandler(uclAssociation_OnStateOk);
                uclAssociation.OnStateError += new EventHandler(uclAssociation_OnStateError);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        
        

        public void Initialize(WizardStates state)
        {
            try
            {
                currentState = state;
                btnContinue.Focus();
                if (currentState == WizardStates.State_NoConnection)
                {
                    ShowNoDBScreen();
                }
                else if (currentState == WizardStates.State_NoLicense)
                {
                    ShowNoLicenseScreen();
                }
                else if (currentState == WizardStates.State_TrialLicense)
                {
                    DateTime todaysDate = DateTime.Now;
                    DateTime deactivationDate = Licence.GetDateTime(General.EcoMartLicense.DeactivationDate);
                    DateTime lastRunDate = Licence.GetDateTime(General.EcoMartLicense.LastRunDate);
                    if (todaysDate > deactivationDate || lastRunDate > todaysDate)
                    {
                        //License is expired
                        ShowLicenseExpiredScreen();
                    }
                    else
                    {
                        ShowTrialDaysScreen();
                    }
                }
                else if (currentState == WizardStates.State_Association)
                {
                    ShowAssociationScreen();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ShowNoDBScreen()
        {
            grpMain.Controls.Clear();
            grpMain.Controls.Add(uclNoDB);
            CurrentControl = uclNoDB;
            CurrentControl.Initialize();

            btnBack.Visible = false;
            btnNext.Visible = true;
            btnCancel.Visible = true;
            btnContinue.Visible = false;
            btnFinish.Visible = false;
        }

        private void ShowDBCreateScreen()
        {
            grpMain.Controls.Clear();
            grpMain.Controls.Add(uclCreateDB);
            CurrentControl = uclCreateDB;
            CurrentControl.Initialize();

            btnBack.Visible = false;
            btnNext.Visible = false;
            btnCancel.Visible = true;
            btnContinue.Visible = false;
            btnFinish.Visible = false;
        }

        private void ShowDBConnectScreen()
        {
            grpMain.Controls.Clear();
            grpMain.Controls.Add(uclConnectDB);
            CurrentControl = uclConnectDB;
            CurrentControl.Initialize();

            btnBack.Visible = false;
            btnNext.Visible = false;
            btnCancel.Visible = true;
            btnContinue.Visible = false;
            btnFinish.Visible = false;
        }

        private void ShowLicenseExpiredScreen()
        {
            grpMain.Controls.Clear();
            grpMain.Controls.Add(uclTrialLicExpired);
            CurrentControl = uclTrialLicExpired;
            CurrentControl.Initialize();

            btnBack.Visible = false;           
            btnNext.Visible = true;
            btnCancel.Visible = true;
            btnContinue.Visible = false;
            btnFinish.Visible = false;
        }

        private void ShowTrialDaysScreen()
        {
            grpMain.Controls.Clear();
            grpMain.Controls.Add(uclTrialLic);
            CurrentControl = uclTrialLic;
            CurrentControl.Initialize();

            btnBack.Visible = false;          
            btnNext.Visible = true;
            btnCancel.Visible = false;
            btnContinue.Visible = true;
            btnFinish.Visible = false;
        }

        private void ShowNoLicenseScreen()
        {
            grpMain.Controls.Clear();
            grpMain.Controls.Add(uclNoLic);
            CurrentControl = uclNoLic;
            CurrentControl.Initialize();          

            btnBack.Visible = false;           
            btnNext.Visible = true;
            btnCancel.Visible = true;
            btnContinue.Visible = false;
            btnFinish.Visible = false;
        }

        private void ShowImportLicenseScreen()
        {
            grpMain.Controls.Clear();
            grpMain.Controls.Add(uclImportLic);
            CurrentControl = uclImportLic;
            CurrentControl.Initialize();

            btnNext.Enabled = false;
          
            btnBack.Visible = false;          
            btnNext.Visible = true;
            btnCancel.Visible = true;          
            btnContinue.Visible = false;
            btnFinish.Visible = false;
        }

        private void ShowAssociationScreen()
        {
            grpMain.Controls.Clear();
            grpMain.Controls.Add(uclAssociation);
            CurrentControl = uclAssociation;
            CurrentControl.Initialize();

            btnBack.Visible = false;
            btnNext.Visible = false;
            btnCancel.Visible = true;
            btnContinue.Visible = false;
            btnFinish.Visible = true;
        }

        private void SaveLicense()
        {
            try
            {
                EcoMartLic licData = new EcoMartLic();
                licData.Read();
                licData.Data = General.EcoMartLicense.GetLicenseData(General.EcoMartLicense);
                licData.UpdateDetails();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private bool IsLicenseAssociateToThisPC()
        {
            bool IsLicAssociated = false;
            try
            {
                string localMACID = EcoMart.InterfaceLayer.Classes.NetworkBrowser.GetLocalMacAddress();
                for (int index = 0; index < General.EcoMartLicense.UserCount; index++)
                {
                    if (General.EcoMartLicense.AssociationList.Item(index) == localMACID)
                    {
                        IsLicAssociated = true;
                        break;
                    }                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return IsLicAssociated;
        }

        #region Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentState == WizardStates.State_NoConnection)
            {
                if (uclNoDB.IsConnectDatabase)
                {
                    currentState = WizardStates.State_ConnnectDatabase;
                    ShowDBConnectScreen();
                }
                else
                {
                    currentState = WizardStates.State_CreateDatabase;
                    ShowDBCreateScreen();
                }
            }
            else if (currentState == WizardStates.State_NoLicense || currentState == WizardStates.State_TrialLicense)
            {
                ShowImportLicenseScreen();
            }
            else if (currentState == WizardStates.State_Association)
            {
                ShowAssociationScreen();
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            General.EcoMartLicense.LastRunDate = Licence.GetDateTimeString(DateTime.Now);
            SaveLicense();
            this.DialogResult = DialogResult.OK;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (currentState == WizardStates.State_Association)
            {
                List<string> associationList = uclAssociation.GetAssociationList();
                for (int index = 0; index < General.EcoMartLicense.UserCount; index++)
                {
                    General.EcoMartLicense.AssociationList.SetItem(index, associationList[index].ToString());
                }
                SaveLicense();
                if (IsLicenseAssociateToThisPC())
                    this.DialogResult = DialogResult.OK;
                else
                    this.DialogResult = DialogResult.Cancel;
            }
            else
                this.DialogResult = DialogResult.OK;
        }     

        private void uclNoDB_OnStateOk(object sender, EventArgs e)
        {

        }

        private void uclNoDB_OnStateError(object sender, EventArgs e)
        {

        }

        private void uclConnectDB_OnStateError(object sender, EventArgs e)
        {

        }

        private void uclConnectDB_OnStateOk(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void uclCreateDB_OnStateOk(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void uclCreateDB_OnStateError(object sender, EventArgs e)
        {

        }

        private void uclImportLic_OnStateError(object sender, EventArgs e)
        {

        }

        private void uclImportLic_OnStateOk(object sender, EventArgs e)
        {
            if (General.EcoMartLicense.LicenseType == LicenseTypes.Trial)
            {
                btnNext.Visible = false;
                btnFinish.Visible = true;
            }
            else
            {
                btnNext.Enabled = true;
                btnNext.Visible = true;
                btnFinish.Visible = false;
                currentState = WizardStates.State_Association;
            }
        }

        private void uclTrialLicExpired_OnStateError(object sender, EventArgs e)
        {

        }

        private void uclTrialLicExpired_OnStateOk(object sender, EventArgs e)
        {

        }

        private void uclTrialLic_OnStateError(object sender, EventArgs e)
        {

        }

        private void uclTrialLic_OnStateOk(object sender, EventArgs e)
        {

        }

        private void uclNoLic_OnStateOk(object sender, EventArgs e)
        {

        }

        private void uclNoLic_OnStateError(object sender, EventArgs e)
        {

        }

        
        private void uclAssociation_OnStateError(object sender, EventArgs e)
        {
            
        }

        private void uclAssociation_OnStateOk(object sender, EventArgs e)
        {
            
        }

        #endregion //Events

      
    }
}
