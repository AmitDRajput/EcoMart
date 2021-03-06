using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class Salesman : BaseObject
    {
       
        #region Constructors, Destructors
        public Salesman()
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
                    ValidationMessages.Add("Please enter the Salesman Name.");

                DBSalesman dbval = new DBSalesman();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("Name Already Exists.");
                    }
                }
                else
                {
                    if (dbval.IsNameUniqueForAdd(Name, Id ))
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
            DBSalesman dbSalesman = new DBSalesman();
            return dbSalesman.GetOverviewData();
        }

        public bool ReadDetailsByID(int ID)
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBSalesman dbSalesman = new DBSalesman();
                drow = dbSalesman.ReadDetailsByID(ID);

                if (drow != null)
                {
                    //IntID = drow["SalesmanId"].ToString();
                    Id = drow["SalesmanID"].ToString();
                    Name = Convert.ToString(drow["SalesmanName"]);
                    Address1= Convert.ToString(drow["Address1"]);
                    Address2 = Convert.ToString(drow["Address2"]);
                    Telephone = Convert.ToString(drow["TelephoneNumber"]);
                    MobileNumberForSMS = Convert.ToString(drow["MobileNumberForSMS"]);
                    MailID = Convert.ToString(drow["Email"]);
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }

        public int AddDetails()
        {
            DBSalesman dbSalesman = new DBSalesman();
            return dbSalesman.AddDetails(IntID , Name,Address1,Address2,Telephone,MobileNumberForSMS,MailID, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBSalesman dbSalesman = new DBSalesman();
            return dbSalesman.UpdateDetails(Convert.ToInt32(Id), Name, Address1, Address2, Telephone, MobileNumberForSMS, MailID, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBSalesman dbSalesman = new DBSalesman();
            return dbSalesman.DeleteDetails(Id);
        }
       

        #endregion

    }
}
