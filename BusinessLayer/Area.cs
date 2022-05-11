using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class Area : BaseObject
    {
        #region Constructors, Destructors
        public Area()
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
                if (Name == null || Name == "")
                    ValidationMessages.Add("Please enter the Area Name.");

                DBArea dbval = new DBArea();
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
                _rowcount = dbdelete.GetOverviewDataSelect("masteraccount", "AccAreaID", Id);
                if (_rowcount == 0)
                {
                    bRetValue = true;
                    _rowcount = dbdelete.GetOverviewDataSelect("vouchersale", "AreaID", Id);
                    if (_rowcount == 0)
                        bRetValue = true;
                    else
                        bRetValue = false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return bRetValue;
        }
        #endregion Internal Methods

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBArea dbArea = new DBArea();
            return dbArea.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBArea dbArea = new DBArea();
                drow = dbArea.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["AreaId"].ToString();
                    Name = Convert.ToString(drow["AreaName"]);
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
            DBArea dbArea = new DBArea();
            return dbArea.AddDetails(IntID , Name,CreatedBy,CreatedDate,CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBArea dbArea = new DBArea();
            return dbArea.UpdateDetails(Id, Name,ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBArea dbArea = new DBArea();
            return dbArea.DeleteDetails(Id);
        }

        public DataTable GetAreaList()
        {
            DBArea dbData = new DBArea();
            return dbData.GetOverviewData();
        }

        public Int32 GetIntID()
        {
            int maxid;
            maxid = 1;
            DBArea dbarea = new DBArea();
            DataRow idrow = dbarea.GetMaxID();
            if (idrow != null)
            {
                if (idrow["maxid"] != null && idrow["maxid"].ToString() != string.Empty)
                {
                    maxid = Convert.ToInt32(idrow["maxid"]) + 1;
                }
            }
            return maxid;

        }

        #endregion Public Methods


        public DataTable GetOverViewDataForAddress()
        {
            DBArea dbArea = new DBArea();
            return dbArea.GetOverViewDataForAddress();
        }
    }
}
