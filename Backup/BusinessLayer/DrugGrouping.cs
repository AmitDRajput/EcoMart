using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class DrugGrouping : BaseObject
    {
        # region Declaration
        private string _DrugId;
        private string _DrugName;
        private string _ProductName;
        private string _ProductId;
        private string _CurrentDrugId;
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
        public string DrugId
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
        public string ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }
        public string CurrentDrugId
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
                _DrugId = "";
                _DrugName = "";
                _ProductName = "";
                _ProductId = "";
                _CurrentDrugId = "";
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
                int.TryParse(_ProductId, out productCount);
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

        public DataTable GetOverviewProductData(string drugID)
        {
            
            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.GetOverviewProductData(drugID);
        }

        public DataTable GetOverviewProductDataY(string prodID)
        {

            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.GetOverviewProductDataY(prodID);
        }

        public DataTable GetOverviewDrugData(string prodID)
        {

            DBDrugGrouping dbDrugGrouping = new DBDrugGrouping();
            return dbDrugGrouping.GetOverviewDrugData(prodID);
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
            return dbDrugGrouping.AddDetails(Id,DetailId,   ProductId, CreatedBy, CreatedDate, CreatedTime, ModifiedBy, ModifiedDate, ModifiedTime);
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
            return dbDrugGrouping.UpdateProductMaster(Id,ProductId);
            
        }

        #endregion
    }
}
