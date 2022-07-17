using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using System.Globalization;
using EcoMart.Common;
using System.Windows.Forms;

namespace EcoMart.BusinessLayer
{
    public class ImportBill : BaseObject
    {
        #region Declaration

        private string _Version;
        private string _BillNumber;
        private string _BillDate;
        private string _TransactionType;        
        private string _BillNetAmount;
        private string _AIOCDACode;
        private string _DistributorCode;
        private string _DistributorName;
        private string _CompanyID;

        private string _CashDiscountPercent;
        private string _CashDiscountAmount;
        private string _TotalAmount;
        private string _Vat5PerAmount;
        private string _Vat12point5PerAmount;
        private string _RoundOFF;
        private string _NetAmount;
        private string _CRAmount;
        private string _DRAmount;

        private string _VoucherType;
        private string _VoucherSeries;
        private int _VoucherNumber;

        private string _AIOCDAProductCode;
        //private string _AllidedProductCode;
        //private string _GlobalProductCode;

        private string _BillNumberAleadyEntered;

        private DataGridView dgsaleBillData;

        private string _PurchaseBillFormat;
        #endregion Declaration

        #region Properties

        public string PurchaseBillFormat
        {
            get { return _PurchaseBillFormat; }
            set { _PurchaseBillFormat = value; }
        }

        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }
        public string BillNumber
        {
            get { return _BillNumber; }
            set { _BillNumber = value; }
        }
        public string BillDate
        {
            get { return _BillDate; }
            set { _BillDate = value; }
        }
        public string TransactionType
        {
            get { return _TransactionType; }
            set { _TransactionType = value; }
        }
        public string BillNetAmount
        {
            get { return _BillNetAmount; }
            set { _BillNetAmount = value; }
        }
        public string AIOCDACode
        {
            get { return _AIOCDACode; }
            set { _AIOCDACode = value; }
        }
        public string DistributorCode
        {
            get { return _DistributorCode; }
            set { _DistributorCode = value; }
        }
        public string CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        public string DistributorName
        {
            get { return _DistributorName; }
            set { _DistributorName = value; }
        }

        public string CashDiscountPercent
        {
            get { return _CashDiscountPercent; }
            set { _CashDiscountPercent = value; }
        }
        public string TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        public string Vat5PerAmount
        {
            get { return _Vat5PerAmount; }
            set { _Vat5PerAmount = value; }
        }
        public string Vat12point5PerAmount
        {
            get { return _Vat12point5PerAmount; }
            set { _Vat12point5PerAmount = value; }
        }
        public string CashDiscountAmount
        {
            get { return _CashDiscountAmount; }
            set { _CashDiscountAmount = value; }
        }
        public string RoundOFF
        {
            get { return _RoundOFF; }
            set { _RoundOFF = value; }
        }

        public string NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }

        public string CRAmount
        {
            get { return _CRAmount; }
            set { _CRAmount = value; }
        }
        public string DRAmount
        {
            get { return _DRAmount; }
            set { _DRAmount = value; }
        }
        public string BillNumberAlreadyEntered
        {
            get { return _BillNumberAleadyEntered; }
            set { _BillNumberAleadyEntered = value; }
        }

        public string VoucherType
        {
            get { return _VoucherType; }
            set { _VoucherType = value; }
        }

        public string VoucherSeries
        {
            get { return _VoucherSeries; }
            set { _VoucherSeries = value; }
        }
        public Int32 VoucherNumber
        {
            get { return _VoucherNumber; }
            set { _VoucherNumber = value; }
        }
        public string AIOCDAProductCode
        {
            get { return _AIOCDAProductCode; }
            set { _AIOCDAProductCode = value; }
        }
      


        public DataGridView SaleBillData
        {
            get
            {
                return dgsaleBillData;
            }
            set
            {
                dgsaleBillData = value;
            }
        }
        #endregion Properties

        #region Internal Methods

        public override void Initialise()
        {
            base.Initialise();

             _Version = string.Empty;
             _BillNumber = string.Empty;
             _BillDate = string.Empty;
             _TransactionType = string.Empty;
             _BillNetAmount = string.Empty;
             _AIOCDACode = string.Empty;
             _DistributorCode = string.Empty;
             _DistributorName = string.Empty;
            _CompanyID = string.Empty;
             _CashDiscountPercent = string.Empty;
             _TotalAmount = string.Empty;
             _Vat5PerAmount = string.Empty;
             _Vat12point5PerAmount = string.Empty;
             _CashDiscountAmount = string.Empty;
             _RoundOFF = string.Empty;
             _NetAmount = string.Empty;
             _CRAmount = string.Empty;
             _DRAmount = string.Empty;
             _VoucherType = string.Empty;
             _VoucherSeries = string.Empty;
             _VoucherNumber = 0;

             _BillNumberAleadyEntered = "N";
        }      

        public string  CheckParty()
        {          
            DataRow dr = null;
            string distributorID = "";
            DBImportBill dbimport = new DBImportBill();
            dr =  dbimport.ReadPartyDetailsByAlliedID(DistributorCode);
            if (dr != null)
            {
                if (dr["AccountID"] != DBNull.Value && dr["AccountID"].ToString() != string.Empty)
                {
                    distributorID = dr["AccountID"].ToString();                   
                   
                }
            }
            return distributorID;

        }

        public bool CheckForBillNumberInPurchase(string partyCode, string partyBillNumber, string partyTransactionType)
        {
            bool retValue = false;
            DataRow dr = null;           
            DBImportBill dbimport = new DBImportBill();
            dr = dbimport.ReadPurchaseFor(partyCode, partyBillNumber, partyTransactionType);
            if (dr != null)
            {
                if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() != string.Empty)
                {
                    VoucherType = dr["VoucherType"].ToString();                    

                }
                if (dr["VoucherNumber"] != DBNull.Value && dr["VoucherNumber"].ToString() != string.Empty)
                {
                    VoucherNumber = Convert.ToInt32(dr["VoucherNumber"].ToString());
                    
                }
                retValue = true;
            }
            return retValue;
                        
         }
        
        #endregion Internal Methods

        public string GetAIOCDACode(string DistCode)
        {
            string AIOCDAcode = string.Empty;
            //DataRow dr = null;
            //DBImportBill dbimport = new DBImportBill();
            //dr = dbimport.GetAIOCDACode(DistCode);
            //if (dr != null)
            //{
            //    if (dr["AIOCDAcode"] != DBNull.Value && dr["AIOCDAcode"].ToString() != string.Empty)
            //    {
            //        AIOCDAcode = dr["AIOCDAcode"].ToString();

            //    }               
            //}
            return AIOCDAcode;
        }

        public string GetDistributorIDFromAIOCDACode(string AIOCDACode)
        {
            string CreditorID = string.Empty;
            //DataRow dr = null;
            //DBImportBill dbimport = new DBImportBill();
            //dr = dbimport.GetDistributorIDFromAIOCDACode(AIOCDACode);
            //if (dr != null)
            //{
            //    if (dr["AccountID"] != DBNull.Value && dr["AccountID"].ToString() != string.Empty)
            //    {
            //        CreditorID = dr["AccountID"].ToString();

            //    }
            //}
            return CreditorID;
        }

        public string GetRetailerProductIDFromDistributorProductID(string distributorID, string distributorProductID)
        {
            string retailerProductID = string.Empty;            
            DataRow dr = null;
            DBImportBill dbimport = new DBImportBill();
            dr = dbimport.GetRetailerProductIDFromCompanyProductID(distributorID, distributorProductID);
            if (dr != null)
            {
                if (dr["RetailerProductID"] != DBNull.Value && dr["RetailerProductID"].ToString() != string.Empty)
                {
                    retailerProductID = dr["RetailerProductID"].ToString();

                }
            }
            return retailerProductID;
        }

        public bool CheckIFProductIDIsNOTLinked(string _CompanyID, string _DistProductID, string _ProductID)
        {
            bool retValue = true;
            DataRow dr = null;
            DBImportBill dbimport = new DBImportBill();
            dr = dbimport.CheckIFProductIDIsNOTLinked(_CompanyID,_DistProductID,_ProductID);
            if (dr != null)
            {
                
                    retValue = false;

               
            }
            return retValue;
        }

        public string GetDistributorIDFromDistributorCode(string p)
        {
            string CreditorID = string.Empty;
            DataRow dr = null;
            DBImportBill dbimport = new DBImportBill();
            dr = dbimport.GetCompanyIDFromCompanyCode(p);
            if (dr != null)
            {
                if (dr["AccountID"] != DBNull.Value && dr["AccountID"].ToString() != string.Empty)
                {
                    CreditorID = dr["AccountID"].ToString();

                }
            }
            return CreditorID;
        }
    }
}
