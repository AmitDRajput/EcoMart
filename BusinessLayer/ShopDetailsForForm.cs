using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;
using EcoMart.DataLayer;

namespace EcoMart.BusinessLayer
{
    class ShopDetailsForForm : BaseObject
    {
        #region Declaration 
        public static string _FShopOwnersName;
        public static string _FShopName;
        public static string _FShopAddress1;
        public static string _FShopAddress2;
        public static string _FShopTelephone;
        public static string _FShopMobileNumber;
        public static string _FShopNumberOfUsers;
        public static string _FShopEmailID;
        public static string _FShopVATTINV;
        public static string _FShopVATTINC;
        public static string _FShopDLN;
        public static string _FShopDLNDist;
        public static string _FShopJurisdiction;
        public static string _FShopLicenses;
        public static string _FShopVoucherSeries;
        public static string _FShopsy;
        public static string _FShopey;
        public static string _FShopYearEndOver;
        public static string _FShopLBT;
        public static string _FShopDistributorSale;
        public static string _FShopChangeCounterSaleType;
        public static string _FShopDebitNoteWithLooseQuantity;
        public string _FAIOCDACode;
        public string _FSCORGCode;
        #endregion

        #region Constructor Destructor
        public ShopDetailsForForm()
        {
            Initialise();
        }
        #endregion

        # region Properties

        public string FAIOCDACode
        {
            get { return _FAIOCDACode; }
            set { _FAIOCDACode = value; }
        }
        public string FSCORGCode
        {
            get { return _FSCORGCode; }
            set { _FSCORGCode = value; }
        }
        public string FShopNumberOfUsers
        {
            get { return _FShopNumberOfUsers; }
            set { _FShopNumberOfUsers = value; }
        }
        public string FShopMobileNumber
        {
            get { return _FShopMobileNumber; }
            set { _FShopMobileNumber = value; }
        }
        public string FShopOwnersName
        {
            get { return _FShopOwnersName; }
            set { _FShopOwnersName = value; }
        }
        public string FShopName
        {
            get { return _FShopName; }
            set { _FShopName = value; }
        }

        public string FShopAddress1
        {
            get { return _FShopAddress1; }
            set { _FShopAddress1 = value; }
        }

        public string FShopAddress2
        {
            get { return _FShopAddress2; }
            set { _FShopAddress2 = value; }
        }

        public string FShopTelephone
        {
            get { return _FShopTelephone; }
            set { _FShopTelephone = value; }
        }
        public string FShopEmailID
        {
            get { return _FShopEmailID; }
            set { _FShopEmailID = value; }
        }
        public string FShopVATTINV
        {
            get { return _FShopVATTINV; }
            set { _FShopVATTINV = value; }
        }
        public string FShopVATTINC
        {
            get { return _FShopVATTINC; }
            set { _FShopVATTINC = value; }
        }
        public string FShopLBT
        {
            get { return _FShopLBT; }
            set { _FShopLBT = value; }
        }
        public string FShopDistributorSale
        {
            get { return _FShopDistributorSale; }
            set { _FShopDistributorSale = value; }
        }
        public string FShopChangeCounterSaleType
        {
            get { return _FShopChangeCounterSaleType; }
            set { _FShopChangeCounterSaleType = value; }
        }
        public string FShopDebitNoteWithLooseQuantity
        {
            get { return _FShopDebitNoteWithLooseQuantity; }
            set { _FShopDebitNoteWithLooseQuantity = value; }
        }
        public string FShopDLN
        {
            get { return _FShopDLN; }
            set { _FShopDLN = value; }
        }
        public string FShopDLNDist
        {
            get { return _FShopDLNDist; }
            set { _FShopDLNDist = value; }
        }
        public string FShopJurisdiction
        {
            get { return _FShopJurisdiction; }
            set { _FShopJurisdiction = value; }
        }

        public string FShopLicenses
        {
            get { return _FShopLicenses; }
            set { _FShopLicenses = value; }
        }
        public string FShopVoucherSeries
        {
            get { return _FShopVoucherSeries; }
            set { _FShopVoucherSeries = value; }
        }
        public string FShopsy
        {
            get { return _FShopsy; }
            set { _FShopsy = value; }
        }
        public string FShopey
        {
            get { return _FShopey; }
            set { _FShopey = value; }
        }
        public string FShopYearEndOver
        {
            get { return _FShopYearEndOver; }
            set { _FShopYearEndOver = value; }
        }
        #endregion

        #region Internal Methods

        #endregion

        #region  Validations
        //public override void DoValidate()
        //{


        //}
        #endregion

        #region Public Method  

        public void FillShopDetails()
        {
            try
            {
                FShopName = General.ShopDetail.ShopName;
                if (General.ShopDetail.ShopOwnersName != null)
                    FShopOwnersName = General.ShopDetail.ShopOwnersName;
                FShopAddress1 = General.ShopDetail.ShopAddress1;
                FShopAddress2 = General.ShopDetail.ShopAddress2;
                FShopTelephone = General.ShopDetail.ShopTelephone;
                FShopMobileNumber = General.ShopDetail.ShopMobileNumber;
                FShopEmailID = General.ShopDetail.ShopEmailID;
                FShopDLN = General.ShopDetail.ShopDLN;
                FShopDLNDist = General.ShopDetail.ShopDLNDist;
                FShopJurisdiction = General.ShopDetail.ShopJurisdiction;
                FShopVATTINV = General.ShopDetail.ShopVATTINV;
                FShopVATTINC = General.ShopDetail.ShopVATTINC;
                FShopLBT = General.ShopDetail.ShopLBT;
                FShopDistributorSale = General.ShopDetail.ShopDistributorSale;
                FShopChangeCounterSaleType = General.ShopDetail.ShopChangeCounterSaleType;
                FShopDebitNoteWithLooseQuantity = General.ShopDetail.ShopDebitNoteWithLooseQuantity;
                GetDataFromtblShopDetails();
                //  FShopNumberOfUsers = General.ShopDetail.s
                //     ShopChangeCounterSaleType = 
                //  ShopChangeCounterSaleType = license.


                //ShopVoucherSeries = dr["ShopVoucherSeries"].ToString();
                //Shopsy = "20120401";
                //Shopey = "20130331";
                //ShopYearEndOver = dr["ShopYearEndOver"].ToString();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        public void GetDataFromtblShopDetails()
        {
            DataRow dt;
            DBShopDetails dbs = new DBShopDetails();
            dt = dbs.GetOverviewData();
            if (dt != null)
            {
                if (dt["ShopOwnerName"] != DBNull.Value)
                {
                    FShopOwnersName = dt["ShopOwnerName"].ToString();
                    General.ShopDetail.ShopOwnersName = FShopOwnersName;
                }
                //if (dt["AIOCDACode"] != DBNull.Value)
                //{
                //    FAIOCDACode = dt["AIOCDACode"].ToString();
                //}
                //if (dt["SCORGCode"] != DBNull.Value)
                //{
                //    FSCORGCode = dt["SCORGCode"].ToString();
                //}
                if (dt["Address2"] != DBNull.Value)
                {
                    FShopAddress2 = dt["Address2"].ToString();
                    General.ShopDetail.ShopAddress2 = FShopAddress2;
                }
                if (dt["Telephone"] != DBNull.Value)
                {
                    FShopTelephone = dt["Telephone"].ToString();
                    General.ShopDetail.ShopTelephone = FShopTelephone;
                }
                if (dt["MobileNumber"] != DBNull.Value)
                {
                    FShopMobileNumber = dt["MobileNumber"].ToString();
                    General.ShopDetail.ShopMobileNumber = FShopMobileNumber;
                }
                if (dt["EmailID"] != DBNull.Value)
                {
                    FShopEmailID = dt["EmailID"].ToString();
                    General.ShopDetail.ShopEmailID = FShopEmailID;
                }
            }
        }
        #endregion

        public void SaveDetails(string fShopOwnersName, string fAIOCDACode, string fSCORGCode, string fShopAddress2, string fShopTelephone, string fShopMobileNumber, string fShopEmailID)
        {
            DBShopDetails dbs = new DBShopDetails();
            dbs.SaveDeails(fShopOwnersName, fAIOCDACode, fSCORGCode, fShopAddress2, fShopTelephone, fShopMobileNumber, fShopEmailID);
        }
    }
}
