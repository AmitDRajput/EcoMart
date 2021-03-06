using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class ProductSchedulH1 : BaseObject
    {
        #region Declaration
        private int _ProductID;
        private bool _DuplicateProduct;
        #endregion
        #region Constructors, Destructors
        public ProductSchedulH1()
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
        # endregion

        #region  Validations
        public override void Initialise()
        {
            base.Initialise();          
            _ProductID = 0;           
            _DuplicateProduct = false;
        }

         public DataTable ReadProductDetailsById()
        {

            DataTable dt = null;
            try
            {
                DBProduct dbprod = new DBProduct();
                dt = dbprod.GetOverviewDataForScheduleH1();
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
             //DBDebtorProduct dbdbprod = new DBDebtorProduct();
             //return dbdbprod.AddProductDetails(Id, DetailId, ProductID, Quantity, CreatedBy, CreatedDate, CreatedTime, ModifiedBy, ModifiedDate, ModifiedTime);
             return true;
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
        #endregion

         public void RemoveH1TagInProductMaster()
         {
             DBProductScheduleH1 dbp = new DBProductScheduleH1();
             dbp.RemoveH1TagInProductMaster();
         }

         public void SetProdScheduleCode(int ProductID)
         {
             DBProductScheduleH1 dbp = new DBProductScheduleH1();
             dbp.SetProdScheduleCode(ProductID);
         }
    }
}
