using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MitraPlus.BusinessLayer;
using System.Windows.Forms;
using MitraPlus.InterfaceLayer;

namespace MitraPlus.Common.Classes
{
    public class LicenseHelper
    {
        MitraPlusLic licData;
        public LicenseHelper()
        {
            licData = new MitraPlusLic();
        }

        public bool CheckLicense()
        {
            bool retValue = false;
            try
            {
                bool isLicFound = licData.IsLicenseFound();
                if (isLicFound)
                {
                    if (licData.Read())
                    {
                        General.MitraPlusLicense = new LicenseLib.Licence();
                        if (General.MitraPlusLicense.LoadLicense(licData.Data))
                        {                           
                            retValue = CheckTrialLicense();
                        }
                        else
                        {
                            MessageBox.Show("License is invalid. Please contact your administrator");
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
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private bool CheckTrialLicense()
        {
            bool retValue = false;
            try
            {
                if (General.MitraPlusLicense.LicenseType == LicenseLib.LicenseTypes.Trial)
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
    }
}
