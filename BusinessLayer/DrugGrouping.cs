using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class DrugGrouping : BaseObject
    {
        # region Declaration
        private Int32  _DrugId;
        private string _DrugName;
        private string _ProductName;
        private Int32  _ProductID;
        private Int32  _CurrentDrugId;
        # endregion

        #region Constructors, Destructors
        public DrugGrouping()
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

        #region Properties
        public Int32  DrugId
        {
            get { return _DrugId; }
            set { _DrugId = value; }
        }
        public string DrugName
        {
            get { return _DrugName; }
            set { _DrugName = value; }
        }
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        public Int32  ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public Int32  CurrentDrugId
        {
            get { return _CurrentDrugId; }
            set { _CurrentDrugId = value; }
        }
        
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _DrugId = 0;
                _DrugName = "";
                _ProductName = "";
                _ProductID = 0;
                _CurrentDrugId = 0;
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
                DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
                DataTable dt = new DataTable();
                if (Id == null || Id == "")
                    ValidationMessages.Add("Please select the Drug");
                int productCount;
                int.TryParse(_ProductID.ToString(), out productCount);
                if (productCount == 0)
                    ValidationMessages.Add("Please add atleast one Product.");            
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }
        


        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }
        #endregion      

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.GetOverviewData();
        }

        public DataTable GetOverviewDataY()
        {
            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.GetOverviewDataY();
        }


        public DataTable GetDrug()
        {
            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.GetDrug();
        }

        public DataTable GetOverviewProductData(Int32 drugID)
        {
            
            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.GetOverviewProductData(drugID);
        }

        public DataTable GetOverviewProductDataY(Int32  prodID)
        {

            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.GetOverviewProductDataY(prodID);
        }

        public DataTable GetOverviewDrugData(int prodID)
        {

            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.GetOverviewDrugData(prodID.ToString());
        }
    
        public DataTable IsDrugAlreadyLinked(string Id)
        {
            DBDrugGrouping dbdg = new DBDrugGrouping();
            return dbdg.IsDrugAlreadyLinked(Id);
        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
                drow = dbDrugGrouping.ReadDetailsByIDDrug(Id);

                if (drow != null)
                {
                    Id = drow["GenericCategoryId"].ToString();
                    DrugName = Convert.ToString(drow["GenericCategoryName"]);
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
            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.AddDetails(DrugId ,DetailIntID ,  ProductID, CreatedBy, CreatedDate, CreatedTime, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.DeleteDetails(Id);
        }




        public bool ClearDrugIDInProductMaster()
        {
            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.ClearDrugIDInProductMaster(Id);
        }



        public bool UpdateProductMaster()
        {
            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.UpdateProductMaster(DrugId ,ProductID);
            
        }
        public Int32 GetIntID()
        {
            int maxid;
            maxid = 1;
            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            DataRow idrow = dbDrugGrouping.GetMaxID();
            if (idrow != null)
            {
                if (idrow["maxid"] != null && idrow["maxid"].ToString() != string.Empty)
                {
                    maxid = Convert.ToInt32(idrow["maxid"]) + 1;
                }
            }
            return maxid;

        }
        #endregion
    }
}
