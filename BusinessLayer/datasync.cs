using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class DataSync : BaseObject
    {
        #region Constructors, Destructors
        public DataSync()
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

        #endregion



        #region Public Methods
        public string GetDataToSync()
        {
            DBDataSync dbDataSync = new DBDataSync();
            string mstAccount = dbDataSync.GetMasterCompanyDataToSync();
            string mstProduct = dbDataSync.GetMasterProductDataToSync();
            string mstProductPack = dbDataSync.GetMasterProductPackDataToSync();
            string mstProductPackType = dbDataSync.GetMasterProductPackTypeDataToSync();
            string mstProductCategory = dbDataSync.GetMasterProductCategoryDataToSync();
            string mstGenericCategory = dbDataSync.GetMasterGenericCategoryDataToSync();
            string mstVAT = dbDataSync.GetMasterVATDataToSync();

            return "";
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
