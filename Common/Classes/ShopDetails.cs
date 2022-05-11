using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;
using EcoMart.DataLayer;

namespace EcoMart.BusinessLayer
{
    public class ShopDetails : BaseObject
    {
        #region Declaration 
        public static string _ShopOwnersName;
        public static string _ShopName;
        public static string _ShopAddress1;
        public static string _ShopAddress2;
        public static string _ShopTelephone;
        public static string _ShopMobileNumber;
        public static string _ShopEmailID;
        public static string _ShopVATTINV;
        public static string _ShopVATTINC;
        public static string _ShopDLN;
        public static string _ShopDLNDist;
        public static string _ShopJurisdiction;
        public static string _ShopLicenses;
        public static string _ShopVoucherSeries;
        public static string _Shopsy;
        public static string _Shopey;
        public static string _ShopYearEndOver;
        public static string _ShopLBT;
        public static string _ShopDistributorSale;
        public static string _ShopChangeCounterSaleType;
        public static string _ShopDebitNoteWithLooseQuantity;
        public static string _ShopScorgCode;
        public static string _ShopBarCode;
        public static string _ShopTransferToTally;
        #endregion

        #region Constructor Destructor
        public ShopDetails()
        {
            Initialise();
        }
        #endregion

        # region Properties
        public string ShopOwnersName
        {
            get { return _ShopOwnersName; }
            set { _ShopOwnersName = value; }
        }
        public string ShopMobileNumber
        {
            get { return _ShopMobileNumber; }
            set { _ShopMobileNumber = value; }
        }
        public string ShopBarCode
        {
            get { return _ShopBarCode; }
            set { _ShopBarCode = value; }
        }
        public string ShopTransferToTally
        {
            get { return _ShopTransferToTally; }
            set { _ShopTransferToTally = value; }
        }
        public string ShopScorgCode
        {
            get { return _ShopScorgCode; }
            set { _ShopScorgCode = value; }
        }
        public string ShopName
        {
            get { return _ShopName; }
            set { _ShopName = value; }
        }

        public string ShopAddress1
        {
            get { return _ShopAddress1; }
            set { _ShopAddress1 = value; }
        }

        public string ShopAddress2
        {
            get { return _ShopAddress2; }
            set { _ShopAddress2 = value; }
        }

        public string ShopTelephone
        {
            get { return _ShopTelephone; }
            set { _ShopTelephone = value; }
        }
        public string ShopEmailID
        {
            get { return _ShopEmailID; }
            set { _ShopEmailID = value; }
        }
        public string ShopVATTINV
        {
            get { return _ShopVATTINV; }
            set { _ShopVATTINV = value; }
        }
        public string ShopVATTINC
        {
            get { return _ShopVATTINC; }
            set { _ShopVATTINC = value; }
        }
        public string ShopLBT
        {
            get { return _ShopLBT; }
            set { _ShopLBT = value; }
        }
        public string ShopDistributorSale
        {
            get { return _ShopDistributorSale; }
            set { _ShopDistributorSale = value; }
        }
        public string ShopChangeCounterSaleType
        {
            get { return _ShopChangeCounterSaleType; }
            set { _ShopChangeCounterSaleType = value; }
        }
        public string ShopDebitNoteWithLooseQuantity
        {
            get { return _ShopDebitNoteWithLooseQuantity; }
            set { _ShopDebitNoteWithLooseQuantity = value; }
        }
        public string ShopDLN
        {
            get { return _ShopDLN; }
            set { _ShopDLN = value; }
        }
        public string ShopDLNDist
        {
            get { return _ShopDLNDist; }
            set { _ShopDLNDist = value; }
        }
        public string ShopJurisdiction
        {
            get { return _ShopJurisdiction; }
            set { _ShopJurisdiction = value; }
        }

        public string ShopLicenses
        {
            get { return _ShopLicenses; }
            set { _ShopLicenses = value; }
        }
        public string ShopVoucherSeries
        {
            get { return _ShopVoucherSeries; }
            set { _ShopVoucherSeries = value; }
        }
        public string Shopsy
        {
            get { return _Shopsy; }
            set { _Shopsy = value; }
        }
        public string Shopey
        {
            get { return _Shopey; }
            set { _Shopey = value; }
        }
        public string ShopYearEndOver
        {
            get { return _ShopYearEndOver; }
            set { _ShopYearEndOver = value; }
        }
        # endregion

        #region Internal Methods
      
        #endregion

        #region  Validations


        public override void DoValidate()
        {
          
            
        }
        #endregion

        #region Public Method  
        
        public void FillShopDetails(EcoMartLicenseLib.Licence license)
        {
            try
            {
                //ShopOwnersName = license.ShopOwnersName;
                ShopName = license.ShopName;
                ShopAddress1 = license.Address1;
                //ShopAddress2 = license.Address2;
                //ShopTelephone = license.Telephone;
                ShopMobileNumber = license.MobileNo;
                ShopEmailID = license.EmailID;
                ShopDLN = license.DLN;
                //ShopDLNDist = license.DLNDist;
                ShopJurisdiction = license.Jurisdiction;
                ShopVATTINV = license.GSTN;
                //ShopVATTINC = license.VATTINC;
                //ShopLBT = license.LBT;
                //ShopDistributorSale = license.DistributorSale;
                //ShopChangeCounterSaleType = license.ChangeCounterSaleType;
                //ShopDebitNoteWithLooseQuantity = license.DebitNoteInLooseQuantity;
                ShopScorgCode = string.Empty;
                ShopTransferToTally = license.TransferToTally;
                //ShopBarCode = license.BarCode;
                ShopDetailsForForm sdf = new ShopDetailsForForm();
                sdf.GetDataFromtblShopDetails();
                    
           
               
                
                //ShopVoucherSeries = dr["ShopVoucherSeries"].ToString();
                //Shopsy = "20120401";
                //Shopey = "20130331";
                //ShopYearEndOver = dr["ShopYearEndOver"].ToString();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
        #endregion

        public string ReadFromtblShopDetails()
        {
            string scorgcd = string.Empty;
            DBShopDetails dbs = new DBShopDetails();
            DataRow dr;
            dr = dbs.ReadShopDetails();
            if (dr != null )
            {
                if (dr["SCORGCode"] != DBNull.Value)
                {
                    scorgcd = dr["SCORGCode"].ToString();
                }
            }
            return scorgcd;
        }
    }
}
