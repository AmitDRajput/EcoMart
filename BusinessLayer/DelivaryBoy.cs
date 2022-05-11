using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;


namespace EcoMart.BusinessLayer
{
    class DelivaryBoy : BaseObject
    {
        #region Constructors, Destructors
        public DelivaryBoy()
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
                    ValidationMessages.Add("Please enter the Delivary boy Name.");

                DBDelivaryBoy dbval = new DBDelivaryBoy();
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
            DBDelivaryBoy dbDelivaryBoy = new DBDelivaryBoy();
            return dbDelivaryBoy.GetOverviewData();
        }

        public bool ReadDetailsByID(int ID)
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBDelivaryBoy dbDelivaryBoy = new DBDelivaryBoy();
                drow = dbDelivaryBoy.ReadDetailsByID(ID);

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
            DBDelivaryBoy dbDelivaryBoy = new DBDelivaryBoy();
            return dbDelivaryBoy.AddDetails(Name, Address1, Address2, Telephone, MobileNumberForSMS, MailID, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBDelivaryBoy dbDelivaryBoy = new DBDelivaryBoy();
            return dbDelivaryBoy.UpdateDetails(Convert.ToInt32(Id), Name, Address1, Address2, Telephone, MobileNumberForSMS, MailID, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBDelivaryBoy dbDelivaryBoy = new DBDelivaryBoy();
            return dbDelivaryBoy.DeleteDetails(Id);
        }


        #endregion

    }
}
