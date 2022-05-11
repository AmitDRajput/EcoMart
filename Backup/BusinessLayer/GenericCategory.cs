using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;


namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class GenericCategory : BaseObject
    {

        #region Constructors, Destructors
        public GenericCategory()
        {
            try
            {
                Initialise();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
                    ValidationMessages.Add("Please enter the Generic Category Name.");
                DBGenericCategory dbGenericCategory = new DBGenericCategory();
                if (dbGenericCategory.IsNameUnique(Name, Id))
                {
                    ValidationMessages.Add("Drug Category Already Exist.");
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }


        public override bool CanBeDeleted()
        {
            bool bRetValue = false;
            try
            {
                int _rowcount = 0;
                DBDelete dbdelete = new DBDelete();
                _rowcount = dbdelete.GetOverviewDataSelect("linkdruggrouping", "GenericCategoryID", Id);
                if (_rowcount == 0)
                    bRetValue = true;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return bRetValue;
        }
        #endregion

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBGenericCategory dbGenericCategory = new DBGenericCategory();
            return dbGenericCategory.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBGenericCategory dbGenericCategory = new DBGenericCategory();
                drow = dbGenericCategory.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["GenericCategoryId"].ToString();
                    Name = Convert.ToString(drow["GenericCategoryName"]);
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
            DBGenericCategory dbGenericCategory = new DBGenericCategory();
            return dbGenericCategory.AddDetails(Id, Name, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBGenericCategory dbGenericCategory = new DBGenericCategory();
            return dbGenericCategory.UpdateDetails(Id, Name, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBGenericCategory dbGenericCategory = new DBGenericCategory();
            return dbGenericCategory.DeleteDetails(Id);
        }

        #endregion

    }
}
