using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{

    public class City : BaseObject
    {
        #region Constructors, Destructors
        public City()
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
                    ValidationMessages.Add("Please enter the City Name.");

                DBCity dbval = new DBCity();
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
            //try
            //{
            //    int _rowcount = 0;
            //    DBDelete dbdelete = new DBDelete();
            //    _rowcount = dbdelete.GetOverviewDataSelect("masteraccount", "AccCityID", Id);
            //    if (_rowcount == 0)
            //    {
            //        bRetValue = true;
            //        _rowcount = dbdelete.GetOverviewDataSelect("vouchersale", "CityID", Id);
            //        if (_rowcount == 0)
            //            bRetValue = true;
            //        else
            //            bRetValue = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteException(ex);
            //}
            return bRetValue;
        }
        #endregion Internal Methods

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBCity dbCity = new DBCity();
            return dbCity.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCity dbCity = new DBCity();
                drow = dbCity.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["CityId"].ToString();
                    Name = Convert.ToString(drow["CityName"]);
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
            DBCity dbCity = new DBCity();
            return dbCity.AddDetails(IntID, Name, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBCity dbCity = new DBCity();
            return dbCity.UpdateDetails(Id, Name, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBCity dbCity = new DBCity();
            return dbCity.DeleteDetails(Id);
        }

        public DataTable GetCityList()
        {
            DBCity dbData = new DBCity();
            return dbData.GetOverviewData();
        }

        public Int32 GetIntID()
        {
            int maxid;
            maxid = 1;
            DBCity dbCity = new DBCity();
            DataRow idrow = dbCity.GetMaxID();
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
            DBCity dbCity = new DBCity();
            return dbCity.GetOverViewDataForAddress();
        }
    }
}
