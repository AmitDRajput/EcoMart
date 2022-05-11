using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class Ward : BaseObject
    {
       
        #region Constructors, Destructors
        public Ward()
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
                    ValidationMessages.Add("Please enter the Ward Name.");

                DBWard dbval = new DBWard();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("Ward Already Exists.");
                    }
                }
                else
                {
                    if (dbval.IsNameUniqueForAdd(Name, Id))
                    {
                        ValidationMessages.Add("Ward Already Exists.");
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
            DBWard dbWard = new DBWard();
            return dbWard.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBWard dbWard = new DBWard();
                drow = dbWard.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["WardId"].ToString();
                    Name = Convert.ToString(drow["WardName"]);
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
            DBWard dbWard = new DBWard();
            return dbWard.AddDetails(Id, Name, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBWard dbWard = new DBWard();
            return dbWard.UpdateDetails(Id, Name, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBWard dbWard = new DBWard();
            return dbWard.DeleteDetails(Id);
        }

       
        #endregion      

    }
}
