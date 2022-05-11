using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class Bank : BaseObject
    {

        #region Constructors, Destructors
        public Bank()
        {
            try
            {
                Initialise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Constructors, Destructors

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override void DoValidate()
        {
            try
            {
                if (Name == "")
                    ValidationMessages.Add("Please enter the Bank Name.");
                DBBank dbval = new DBBank();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, IntID ))
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
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override bool CanBeDeleted()
        {
            bool bRetValue = false;
            try
            {
                int _rowcount = 0;
                DBDelete dbdelete = new DBDelete();
                _rowcount = dbdelete.GetOverviewDataSelect("masteraccount", "AccBankID", Id);
                if (_rowcount == 0)
                    bRetValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return bRetValue;
        }

        #endregion


        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBBank dbBank = new DBBank();
            return dbBank.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            DBBank dbBank = new DBBank();
            drow = dbBank.ReadDetailsByID(Id);

            if (drow != null)
            {
                Id = drow["BankId"].ToString();
                Name = Convert.ToString(drow["BankName"]);
                retValue = true;
            }
            return retValue;
        }

        public int AddDetails()
        {
            DBBank dbBank = new DBBank();
            return dbBank.AddDetails(IntID , Name, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBBank dbBank = new DBBank();
            return dbBank.UpdateDetails(Id, Name, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBBank dbBank = new DBBank();
            return dbBank.DeleteDetails(Id);
        }
        
        #endregion



    }
}
