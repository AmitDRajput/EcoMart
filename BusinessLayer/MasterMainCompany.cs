using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class MasterMainCompany : BaseObject
    {
        #region Constructors, Destructors
        public MasterMainCompany()
        {
            Initialise();
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override void DoValidate()
        {
            try
            {
                if (Name == "")
                    ValidationMessages.Add("Please enter the Company Name.");

                if (Address1 == "" || Address2 =="")
                    ValidationMessages.Add("Please enter Address for Company.");

                DBMasterMainCompany dbval = new DBMasterMainCompany();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("Name Already Exists.");
                    }
                }
                else
                {
                    if (dbval.IsNameUniqueForAdd(Name, Id))
                    {
                        ValidationMessages.Add("Name Already Exists.");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBMasterMainCompany dbMasterMainCompany = new DBMasterMainCompany();
            return dbMasterMainCompany.GetOverviewData();
        }

        public bool ReadDetailsByID(int ID)
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBMasterMainCompany dbMasterMainCompany = new DBMasterMainCompany();
                drow = dbMasterMainCompany.ReadDetailsByID(ID);

                if (drow != null)
                {
                    //IntID = drow["SalesmanId"].ToString();
                    Id = drow["ID"].ToString();
                    Name = Convert.ToString(drow["Name"]);
                    Address1 = Convert.ToString(drow["Address1"]);
                    Address2 = Convert.ToString(drow["Address2"]);
                    Telephone = Convert.ToString(drow["TelephoneNumber"]);
                    MobileNumberForSMS = Convert.ToString(drow["MobileNumberForSMS"]);
                    MailID = Convert.ToString(drow["Email"]);
                    AIODA = Convert.ToString(drow["AIOCDA"]);
                    GlobalNumber = Convert.ToString(drow["GlobalNumber"]);
                    GalliNumber = Convert.ToInt32(drow["GalliNumber"]);

                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public bool AddDetails()
        {
            DBMasterMainCompany dbMasterMainCompany = new DBMasterMainCompany();
            return dbMasterMainCompany.AddDetails(Name, Address1, Address2, Telephone, MobileNumberForSMS, MailID,AIODA,GlobalNumber,GalliNumber, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBMasterMainCompany dbMasterMainCompany = new DBMasterMainCompany();
            return dbMasterMainCompany.UpdateDetails(Convert.ToInt32(Id), Name, Address1, Address2, Telephone, MobileNumberForSMS, MailID,AIODA,GlobalNumber,GalliNumber, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBMasterMainCompany dbMasterMainCompany = new DBMasterMainCompany();
            return dbMasterMainCompany.DeleteDetails(Id);
        }


        #endregion


    }
}
