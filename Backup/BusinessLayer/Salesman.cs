using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
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
            DBSalesman dbSalesman = new DBSalesman();
            return dbSalesman.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBSalesman dbSalesman = new DBSalesman();
                drow = dbSalesman.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["SalesmanId"].ToString();
                    Name = Convert.ToString(drow["SalesmanName"]);
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
            DBSalesman dbSalesman = new DBSalesman();
            return dbSalesman.AddDetails(Id, Name, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBSalesman dbSalesman = new DBSalesman();
            return dbSalesman.UpdateDetails(Id, Name, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBSalesman dbSalesman = new DBSalesman();
            return dbSalesman.DeleteDetails(Id);
        }

       
        #endregion      

    }
}
