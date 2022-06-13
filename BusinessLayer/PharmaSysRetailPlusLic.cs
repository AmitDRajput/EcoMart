using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class EcoMartLic : BaseObject
    {

        private string _Data;
        public string Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        public EcoMartLic()
        {
        }

        public bool Read()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBEcoMartLic dbEcoMart = new DBEcoMartLic();
                drow = dbEcoMart.Read();
                if (drow != null)
                {
                    Id = drow["EcoMartID"].ToString();
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
            DBEcoMartLic dbLic = new DBEcoMartLic();
            return dbLic.IsLicenseFound();
        }

        public bool DeleteLicense()
        {
            DBEcoMartLic dbLic = new DBEcoMartLic();
            return dbLic.DeleteLicense();
        }

        public bool AddDetails()
        {
            DBEcoMartLic dbLic = new DBEcoMartLic();
            return dbLic.AddDetails(Id, Data);
        }

        public bool UpdateDetails()
        {
            DBEcoMartLic dbLic = new DBEcoMartLic();
            return dbLic.UpdateDetails(Id, Data);
        }

        public bool DeleteDetails()
        {
            DBEcoMartLic dbLic = new DBEcoMartLic();
            return dbLic.DeleteDetails(Id);
        }
    }
}
