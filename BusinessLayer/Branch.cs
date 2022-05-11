using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;



namespace EcoMart.BusinessLayer
{
   public  class Branch: BaseObject 
    {     

       #region Constructors, Destructors
        public Branch()
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
                    ValidationMessages.Add("Please enter the Branch Name.");

                DBBranch dbval = new DBBranch();
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
            int _rowcount = 0;
            try
            {
                DBDelete dbdelete = new DBDelete();
                _rowcount = dbdelete.GetOverviewDataSelect("masteraccount", "AccBranchID", Id);
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
            DBBranch dbBranch = new DBBranch();
            return dbBranch.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBBranch dbBranch = new DBBranch();
                drow = dbBranch.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["BranchId"].ToString();
                    Name = Convert.ToString(drow["BranchName"]);
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public int AddDetails()
        {
            DBBranch dbBranch = new DBBranch();
            return dbBranch.AddDetails(IntID , Name,CreatedBy,CreatedDate,CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBBranch dbBranch = new DBBranch();
            return dbBranch.UpdateDetails(Id, Name,ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBBranch dbBranch = new DBBranch();
            return dbBranch.DeleteDetails(Id);
        }
        public Int32 GetIntID()
        {

            DBBranch dbcomp = new DBBranch();
            DataRow idrow = dbcomp.GetMaxID();
            int maxid = Convert.ToInt32(idrow["maxbranchid"]) + 1;
            return maxid;

        }
        #endregion      

    }
}
    