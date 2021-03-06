using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using System.Globalization;
using PharmaSYSRetailPlus.Common;
using System.Windows.Forms;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class ImportBill : BaseObject
    {
        #region Declaration

        private string _Version;
        private string _BillNumber;
        private string _BillDate;
        private string _TransactionType;        
        private string _BillNetAmount;
        private string _mscdaCode;
        private string _DistributorCode;
        private string _DistributorName;
        private string _DistributorID;

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

        private string _MscdaProductCode;
        //private string _AllidedProductCode;
        //private string _GlobalProductCode;

        private string _BillNumberAleadyEntered;

        private DataGridView dgsaleBillData;
        #endregion Declaration

        #region Properties

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
        public string mscdaCode
        {
            get { return _mscdaCode; }
            set { _mscdaCode = value; }
        }
        public string DistributorCode
        {
            get { return _DistributorCode; }
            set { _DistributorCode = value; }
        }
        public string DistributorID
        {
            get { return _DistributorID; }
            set { _DistributorID = value; }
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
        public string MscdaProductCode
        {
            get { return _MscdaProductCode; }
            set { _MscdaProductCode = value; }
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
             _mscdaCode = string.Empty;
             _DistributorCode = string.Empty;
             _DistributorName = string.Empty;
             _DistributorID = string.Empty;
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

       
    }
}
