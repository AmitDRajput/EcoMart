using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class DebtorProduct : BaseObject
    {
        #region Declaration
        private int _ProductID;
        private int _Quantity;
        private string _AccountID;
        private bool _DuplicateProduct;
        #endregion

        #region Constructors, Destructors
        public DebtorProduct()
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

        #region Properties

        public bool DuplicateProduct
        {
            get { return _DuplicateProduct; }
            set { _DuplicateProduct = value; }
        } 

        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }

        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        #endregion

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBDebtorProduct dbdbprod = new DBDebtorProduct();
            return dbdbprod.GetOverviewData();
        }

        public DataTable GetAllDebtors()
        {
            DBDebtorProduct _dbPresc = new DBDebtorProduct();
            return _dbPresc.GetOverviewData();
        }
        public DataTable GetOverviewProductDataY()
        {
            DBDebtorProduct dbdbprod = new DBDebtorProduct();
            return dbdbprod.GetOverviewProductDataY();
        }
        public DataTable GetOverviewDebtorDataY(int prodID)
        {
            DBDebtorProduct dbdbprod = new DBDebtorProduct();
            return dbdbprod.GetOverviewDebtorDataY(prodID);
        }
        public DataTable GetOverviewDebtorData(string acID)
        {

            DBDebtorProduct dbdbprod = new DBDebtorProduct();
            return dbdbprod.GetOverviewDebtorData(acID);
        }

        # endregion

        #region  Validations
        public override void Initialise()
        {
            base.Initialise();
            _Quantity = 0;
            _ProductID = 0;
            _AccountID = "";
            _DuplicateProduct = false;

        }

        public override void DoValidate()
        {
            if (Id == "")
                ValidationMessages.Add("Please enter Debtor.");            
        }
        #endregion

        # region Internal Methods

        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBDebtorProduct dbdbprod = new DBDebtorProduct();
                drow = dbdbprod.ReadDetailsByID(Id);

                if (drow != null)
                {
                    if (drow["AccountID"] != DBNull.Value)
                        Id = drow["AccountId"].ToString();
                    if (drow["AccName"] != DBNull.Value)
                        Name = Convert.ToString(drow["AccName"]);

                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public DataTable ReadProductDetailsById(string Id)
        {

            DataTable dt = null;
            try
            {
                DBDebtorProduct dbdbprod = new DBDebtorProduct();
                dt = dbdbprod.ReadProdDetailsById(Id);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;


        }
        public DataTable ReadProdDetailsById(string Id)
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBDebtorProduct dbdbprod = new DBDebtorProduct();
                dt = dbdbprod.ReadProdDetailsById(Id);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }

        public DataTable ReadProdDetailsByIdForDebtorSale(string Id)
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBDebtorProduct dbdbprod = new DBDebtorProduct();
                dt = dbdbprod.ReadProdDetailsByIdForDebtorSale(Id);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }


        public bool AddDetails()
        {
           return true;
        }

        public bool AddProductDetails()
        {
            DBDebtorProduct dbdbprod = new DBDebtorProduct();
            return dbdbprod.AddProductDetails(Id,DetailId,  ProductID, Quantity,CreatedBy,CreatedDate,CreatedTime,ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool UpdateDetails()
        {
          
            return true;
        }

        public bool DeleteDetails()
        { 
            DBDebtorProduct dbdbprod = new DBDebtorProduct();
            return dbdbprod.DeleteDetails(Id);           
        }
        public DataTable IsPartyAlreadyLinked(string Id)
        {
            DBDebtorProduct dbdg = new DBDebtorProduct();
            return dbdg.IsPartyAlreadyLinked(Id);
        }

        public bool DeleteProductsById()
        {
            DBDebtorProduct dbdbprod = new DBDebtorProduct();
            return dbdbprod.DeleteProductsByID(Id);
        }

        #endregion       

    }
}
