using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class ProductCategory : BaseObject
    {
        //#region Declaration
        //private string _IFSaleDiscount;
        //private double _LBTPercent;
        //private string _IFDoctorRequired;
        //#endregion Declaration

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

        //public string IFSaleDiscount
        //{
        //    get { return _IFSaleDiscount; }
        //    set { _IFSaleDiscount = value; }
        //}

        //public double LBTPercent
        //{
        //    get { return _LBTPercent; }
        //    set { _LBTPercent = value; }
        //}
        //public string IFDoctorRequired
        //{
        //    get { return _IFDoctorRequired; }
        //    set { _IFDoctorRequired = value; }
        //}
        #endregion Properties
        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            //    _LBTPercent = 0;
            //    _IFSaleDiscount = "";
        }

        public override void DoValidate()
        {
            try
            {
                //if (IFSaleDiscount != "Y" && IFSaleDiscount != "N")
                //    ValidationMessages.Add("Please Check SaleDiscount Y/N ");
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
                    //if (drow["SaleDiscount"] != DBNull.Value)
                    //    IFSaleDiscount = drow["SaleDiscount"].ToString();
                    //if (drow["LBTPercent"] != DBNull.Value)
                    //    LBTPercent = Convert.ToDouble(drow["LBTPercent"].ToString());
                    //if (drow["IFDoctorRequired"] != DBNull.Value)
                    //    IFDoctorRequired = Convert.ToString(drow["IFDoctorRequired"]);
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }

        public int AddDetails()
        {
            DBProductCategory dbProductCategory = new DBProductCategory();        
            return dbProductCategory.AddDetails(IntID , Name,  CreatedBy, CreatedDate, CreatedTime);
        }

        public bool UpdateDetails()
        {
            DBProductCategory dbProductCategory = new DBProductCategory();        
            return dbProductCategory.UpdateDetails(Id, Name, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        //public bool UpdateProductMaster()
        //{
        //    DBProductCategory dbProductCategory = new DBProductCategory();
        //    return dbProductCategory.UpdateProductMaster(Id,IFSaleDiscount);
        //}
        public bool DeleteDetails()
        {
            DBProductCategory dbProductCategory = new DBProductCategory();
            return dbProductCategory.DeleteDetails(Id);
        }

       
        #endregion      

    }
}
