using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.DataLayer;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class ShopDetails : BaseObject
    {
        #region Declaration 
        public static string _ShopName;
        public static string _ShopAddress1;
        public static string _ShopAddress2;
        public static string _ShopTelephone;
        public static string _ShopEmailID;
        public static string _ShopVATTINV;
        public static string _ShopVATTINC;
        public static string _ShopDLN;
        public static string _ShopJurisdiction;
        public static string _ShopLicenses;
        public static string _ShopVoucherSeries;
        public static string _Shopsy;
        public static string _Shopey;
        public static string _ShopYearEndOver;
        public static string _ShopLBT;
        #endregion

        #region Constructor Destructor
        public ShopDetails()
        {
            Initialise();
        }
        #endregion

        # region Properties
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
        public string ShopDLN
        {
            get { return _ShopDLN; }
            set { _ShopDLN = value; }
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
        
        public void FillShopDetails(LicenseLib.Licence license)
        {
            try
            {
                ShopName = license.ShopName;
                ShopAddress1 = license.Address1;
                ShopAddress2 = license.Address2;
                ShopTelephone = license.Telephone;
                ShopEmailID = license.EmailID;
                ShopDLN = license.DLN;
                ShopJurisdiction = license.Jurisdiction;
                ShopVATTINV = license.VATTINV;
                ShopVATTINC = license.VATTINC;
                ShopLBT = license.LBT;
                //ShopVoucherSeries = dr["ShopVoucherSeries"].ToString();
                //Shopsy = "20120401";
                //Shopey = "20130331";
                //ShopYearEndOver = dr["ShopYearEndOver"].ToString();
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
        #endregion
    }
}
