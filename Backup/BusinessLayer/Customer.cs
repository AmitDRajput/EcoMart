using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;


namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class Customer : BaseObject
    {
       

        #region Constructors, Destructors
            public Customer()
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
            #endregion

       

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
                        ValidationMessages.Add("Please enter the Customer Name.");

                    DBCustomer dbval = new DBCustomer();
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
                    _rowcount = dbdelete.GetOverviewDataSelect("vouchersale", "CustomerID", Id);
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
            DBCustomer dbCustomer = new DBCustomer();
            return dbCustomer.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCustomer dbCustomer = new DBCustomer();
                drow = dbCustomer.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["CustomerId"].ToString();
                    Name = Convert.ToString(drow["CustomerNameAddress"]);
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public bool AddDetails()
        {
            DBCustomer dbCustomer = new DBCustomer();
            return dbCustomer.AddDetails(Id, Name,CreatedBy,CreatedDate,CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBCustomer dbCustomer = new DBCustomer();
            return dbCustomer.UpdateDetails(Id, Name,ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBCustomer dbCustomer = new DBCustomer();
            return dbCustomer.DeleteDetails(Id);
        }

        public DataTable GetAvailableCustomerList()
        {
            DBCustomer dbCustomer = new DBCustomer();
            return dbCustomer.GetAvailableCustomerList();
        }

        #endregion      

    }
}
