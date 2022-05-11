using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class PharmaSysRetailPlusLic : BaseObject
    {

        private string _Data;
        public string Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        public PharmaSysRetailPlusLic()
        {
        }

        public bool Read()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBPharmaSysRetailPlusLic dbPharmaSysRetailPlus = new DBPharmaSysRetailPlusLic();
                drow = dbPharmaSysRetailPlus.Read();
                if (drow != null)
                {
                    Id = drow["PharmaSYSRetailPlusID"].ToString();
                    Data = drow["Data"].ToString();
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public bool IsLicenseFound()
        {
            DBPharmaSysRetailPlusLic dbLic = new DBPharmaSysRetailPlusLic();
            return dbLic.IsLicenseFound();
        }

        public bool DeleteLicense()
        {
            DBPharmaSysRetailPlusLic dbLic = new DBPharmaSysRetailPlusLic();
            return dbLic.DeleteLicense();
        }

        public bool AddDetails()
        {
            DBPharmaSysRetailPlusLic dbLic = new DBPharmaSysRetailPlusLic();
            return dbLic.AddDetails(Id, Data);
        }

        public bool UpdateDetails()
        {
            DBPharmaSysRetailPlusLic dbLic = new DBPharmaSysRetailPlusLic();
            return dbLic.UpdateDetails(Id, Data);
        }

        public bool DeleteDetails()
        {
            DBPharmaSysRetailPlusLic dbLic = new DBPharmaSysRetailPlusLic();
            return dbLic.DeleteDetails(Id);
        }
    }
}
