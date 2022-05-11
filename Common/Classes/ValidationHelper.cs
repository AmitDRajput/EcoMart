using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.BusinessLayer;
using System.Windows.Forms;
using EcoMart.InterfaceLayer;
using EcoMart.InterfaceLayer.Classes;

namespace EcoMart.Common.Classes
{
    public class ValidationHelper
    {
        EcoMartLic licData;
        private const string SUPPORTED_LICENSE_VERSION = "1.0";
        public ValidationHelper()
        {
            licData = new EcoMartLic();
        }

        public bool DoValidate()
        {
            bool retValue = false;
            try
            {
                bool isDBConnected = CheckDatabase();
                if (isDBConnected)
                {
                    retValue = true;
                    retValue = CheckLicense(retValue);
                }                
                if (retValue && IsFullLicense())
                   retValue = IsLicenseAssociated();
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
             //   retValue = false;
            }
            return retValue;
        }

        private bool IsLicenseAssociated()
        {
            bool IsLicAssociated = false;
            bool IsLicAssociationEmpty = false;
            try
            {
                string localMACID = NetworkBrowser.GetLocalMacAddress();                
                for (int index = 0; index < General.EcoMartLicense.UserCount; index++)
                {
                    if (General.EcoMartLicense.AssociationList.Item(index).ToLower() == localMACID.ToLower())
                    {
                        IsLicAssociated = true;
                        break;
                    }
                    else if (General.EcoMartLicense.AssociationList.Item(index) == "")
                    {
                        IsLicAssociationEmpty = true;
                    }
                }
                if (IsLicAssociated == false)
                {
                    if (IsLicAssociationEmpty)
                    {
                        IsLicAssociated = ShowLicenseAssociationDialog();
                    }
                    else
                    {
                        MessageBox.Show("License is not associated with this PC. Name: " + localMACID, General.ApplicationTitle);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }           
            return IsLicAssociated;
        }

        private bool ShowLicenseAssociationDialog()
        {
            bool IsLicAssociated = false;
            try
            {
                if (MessageBox.Show("License is not associated with this PC." + Environment.NewLine + "Do you want to Associate now?", General.ApplicationTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FormValidation importLic = new FormValidation();
                    importLic.Initialize(FormValidation.WizardStates.State_Association);
                    if (importLic.ShowDialog() == DialogResult.OK)
                    {
                        IsLicAssociated = true;
                    }
                    else
                    {
                        MessageBox.Show("License is not associated with this PC...!", General.ApplicationTitle);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return IsLicAssociated;
        }

        private bool CheckLicense(bool retValue)
        {
            bool isLicFound = licData.IsLicenseFound();
            if (isLicFound)
            {
                if (licData.Read())
                {
                    General.EcoMartLicense = new EcoMartLicenseLib.Licence();
                    if (General.EcoMartLicense.LoadLicense(licData.Data))
                    {
                        if (IsValidLicenseNumber())
                        {
                            if (IsTrialLicense())
                                retValue = ValidateTrialLicense();
                            else if (IsFullLicense())
                                retValue = ValidateFullLicense();
                        }
                        else
                        {
                            MessageBox.Show("Invalid License schema. Please contact your administrator", General.ApplicationTitle);
                            retValue = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("License is invalid. Please contact your administrator", General.ApplicationTitle);
                    }
                }
            }
            else
            {
                FormValidation importLic = new FormValidation();
                importLic.Initialize(FormValidation.WizardStates.State_NoLicense);
                if (importLic.ShowDialog() == DialogResult.OK)
                {
                    retValue = true;
                }
            }
            return retValue;
        }

        public bool IsFullLicense()
        {
            return General.EcoMartLicense.LicenseType != EcoMartLicenseLib.LicenseTypes.Trial;
        }

        public bool IsTrialLicense()
        {
            return General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Trial;
        }

        public bool IsValidLicenseNumber()
        {
            return General.EcoMartLicense.LicenseVersionNo == SUPPORTED_LICENSE_VERSION;
        }

        private bool ValidateFullLicense()
        {
            bool retValue = false;
            try
            {
                if (General.EcoMartLicense.LicenseType != EcoMartLicenseLib.LicenseTypes.Trial)
                {
                   //Validate Full License
                    retValue = true;
                }
                else
                {
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private bool ValidateTrialLicense()
        {
            bool retValue = false;
            try
            {
                if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Trial)
                {
                    FormValidation importLic = new FormValidation();
                    importLic.Initialize(FormValidation.WizardStates.State_TrialLicense);
                    if (importLic.ShowDialog() == DialogResult.OK)
                    {
                        retValue = true;
                    }
                }
                else
                {
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public bool CheckDatabase()
        {
            bool retValue = false;
            try
            {
                ConnectionInfo connInfo = new ConnectionInfo();
                connInfo.Initialize();
                if (connInfo.IsDBConnected)
                {
                    retValue = true; 
                                  
                }
                else
                {
                    FormValidation validateConnection = new FormValidation();
                    validateConnection.Initialize(FormValidation.WizardStates.State_NoConnection);
                    if (validateConnection.ShowDialog() == DialogResult.OK)
                    {
                        retValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }

            return retValue;
        }
    }
}
