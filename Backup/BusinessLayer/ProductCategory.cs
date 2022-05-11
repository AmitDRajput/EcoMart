using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class ProductCategory : BaseObject
    {
        #region Declaration
        private string _IFSaleDiscount;
        #endregion Declaration

        #region Constructors, Destructors
        public ProductCategory()
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

        public string IFSaleDiscount
        {
            get { return _IFSaleDiscount; }
            set { _IFSaleDiscount = value; }
        }

        #endregion Properties
        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();           
        }

        public override void DoValidate()
        {
            try
            {
                if (IFSaleDiscount != "Y" && IFSaleDiscount != "N")
                    ValidationMessages.Add("Please Check SaleDiscount Y/N ");
                if (Name == "")
                    ValidationMessages.Add("Please enter the Product Category Name.");

                DBProductCategory dbval = new DBProductCategory();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("Category Already Exists.");
                    }
                }
                else
                {
                    if (dbval.IsNameUniqueForAdd(Name, Id))
                    {
                        ValidationMessages.Add("Category Already Exists.");
                    }
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
                _rowcount = dbdelete.GetOverviewDataSelect("masterproduct", "ProdCategoryID", Id);
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
            DBProductCategory dbProductCategory = new DBProductCategory();
            return dbProductCategory.GetOverviewData();
        }

        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBProductCategory dbProductCategory = new DBProductCategory();
                drow = dbProductCategory.ReadDetailsByID(Id);

                if (drow != null)
                {
                    Id = drow["ProductCategoryId"].ToString();
                    Name = Convert.ToString(drow["ProductCategoryName"]);
                    if (drow["SaleDiscount"] != DBNull.Value)
                        IFSaleDiscount = drow["SaleDiscount"].ToString();
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
            DBProductCategory dbProductCategory = new DBProductCategory();        
            return dbProductCategory.AddDetails(Id, Name, IFSaleDiscount, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBProductCategory dbProductCategory = new DBProductCategory();        
            return dbProductCategory.UpdateDetails(Id, Name,IFSaleDiscount, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool UpdateProductMaster()
        {
            DBProductCategory dbProductCategory = new DBProductCategory();
            return dbProductCategory.UpdateProductMaster(Id,IFSaleDiscount);
        }
        public bool DeleteDetails()
        {
            DBProductCategory dbProductCategory = new DBProductCategory();
            return dbProductCategory.DeleteDetails(Id);
        }

       
        #endregion      

    }
}
