using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class CreditNoteStock : BaseObject
    {
        #region Declaration
        private string _ProductID;
        private string _Batchno;
        private double _Mrp;
        private double _PurchaseRate;
        private double _SaleRate;
        private double _TradeRate;
        private int _Quantity;
        private int _SchemeQuantity;
        private string _Expiry;
        private string _ExpiryDate;
        private string _ReasonCode;
        private double _VATPer;
        private int _ClosingStock;
        private int _ProdLoosePack;
        private double _DiscountPercent;
        private double _DiscountAmount;
        private double _ReturnRate;
        private string _ScanCode;

        private string _CrdbId;
        private string _CrdbAccountId;
        private string _CrdbName;
        private string _CrdbAddress;
        private string _CrdbNarration;
        private string _CrdbVouType;
        private int _CrdbVouNo;
        private string _CrdbVouDate;
        private string _CrdbVouSeries;
        private int _CrdbNoOfRows;
        private double _CrdbVat5;
        private double _CrdbVat12point5;
        private double _CrdbAmount;
        private double _CrdbDiscPer;
        private double _CrdbDiscAmt;
        private double _CrdbAmountNet;
        private double _CrdbRoundAmount;
        private double _CrdbTotalAmount;
        private string _Particulars;
        private double _Amount;
        private int _ClearVouNo;
        private string _ClearVouType;
        private string _StockID;
        private string _TransferToAccount;
        private double _CrdbAmountClear;
      //  private string _DetailID;

        private string _SetExpiryFirst;

        private string _CrdbVouTypeDistributor;

        private string _ClearedIn;
        private string _CreditAcID;

        private double _GSTAmt0;
        private double _GSTAmtS5 = 0;
        private double _GSTAmtC5 = 0;
        private double _GSTAmtI5 = 0;
        private double _GSTS5 = 0;
        private double _GSTC5 = 0;
        private double _GSTI5 = 0;
        private double _GSTAmtS12 = 0;
        private double _GSTAmtC12 = 0;
        private double _GSTAmtI12 = 0;
        private double _GSTS12 = 0;
        private double _GSTC12 = 0;
        private double _GSTI12 = 0;
        private double _GSTAmtS18 = 0;
        private double _GSTAmtC18 = 0;
        private double _GSTAmtI18 = 0;
        private double _GSTS18 = 0;
        private double _GSTC18 = 0;
        private double _GSTI18 = 0;
        private double _GSTAmtS28 = 0;
        private double _GSTAmtC28 = 0;
        private double _GSTAmtI28 = 0;
        private double _GSTS28 = 0;
        private double _GSTC28 = 0;
        private double _GSTI28 = 0;

        private double _GSTSAmount = 0;
        private double _GSTCAmount = 0;
        private double _GSTIAmount = 0;
        private double _GSTSPurchaseAmount = 0;
        private double _GSTCPurchaseAmount = 0;
        private double _GSTIPurchaseAmount = 0;
        private double _GSTPurchaseAmountZero = 0;

        private string _IfOMS = "N";
        private string _IfExemptedScheme = "N";


        #endregion

        #region Constructors
        public CreditNoteStock()
        {
            Initialise();
        }
        #endregion

        #region properties
        public string IFOMS
        {
            get { return _IfOMS; }
            set { _IfOMS = value; }
        }

        public string IfExemptedScheme
        {
            get { return _IfExemptedScheme; }
            set { _IfExemptedScheme = value; }
        }

        public double GSTPurchaseAmountZero
        {
            get { return _GSTPurchaseAmountZero; }
            set { _GSTPurchaseAmountZero = value; }
        }
        public double GSTSAmount
        {
            get { return _GSTSAmount; }
            set { _GSTSAmount = value; }
        }
        public double GSTCAmount
        {
            get { return _GSTCAmount; }
            set { _GSTCAmount = value; }
        }
        public double GSTIAmount
        {
            get { return _GSTIAmount; }
            set { _GSTIAmount = value;  }
        }
        public double GSTIPurchaseAmount
        {
            get { return _GSTIPurchaseAmount; }
            set { _GSTIPurchaseAmount = value; }
        }
        public double GSTSPurchaseAmount
        {
            get { return _GSTSPurchaseAmount; }
            set { _GSTSPurchaseAmount = value; }
        }


        public double GSTCPurchaseAmount
        {
            get { return _GSTCPurchaseAmount; }
            set { _GSTCPurchaseAmount = value; }
        }
        public double GSTAmt0
        {
            get { return _GSTAmt0; }
            set { _GSTAmt0 = value; }
        }
        public double GSTAmtS5
        {
            get { return _GSTAmtS5; }
            set { _GSTAmtS5 = value; }
        }
        public double GSTAmtC5
        {
            get { return _GSTAmtC5; }
            set { _GSTAmtC5 = value; }
        }
        public double GSTAmtI5
        {
            get { return _GSTAmtI5; }
            set { _GSTAmtI5 = value; }
        }
        public double GSTAmtS12
        {
            get { return _GSTAmtS12; }
            set { _GSTAmtS12 = value; }
        }
        public double GSTAmtC12
        {
            get { return _GSTAmtC12; }
            set { _GSTAmtC12 = value; }
        }
        public double GSTAmtI12
        {
            get { return _GSTAmtI12; }
            set { _GSTAmtI12 = value; }
        }
        public double GSTAmtS18
        {
            get { return _GSTAmtS18; }
            set { _GSTAmtS18 = value; }
        }
        public double GSTAmtC18
        {
            get { return _GSTAmtC18; }
            set { _GSTAmtC18 = value; }
        }
        public double GSTAmtI18
        {
            get { return _GSTAmtI18; }
            set { _GSTAmtI18 = value; }
        }
        public double GSTAmtS28
        {
            get { return _GSTAmtS28; }
            set { _GSTAmtS28 = value; }
        }
        public double GSTAmtC28
        {
            get { return _GSTAmtC28; }
            set { _GSTAmtC28 = value; }
        }
        public double GSTAmtI28
        {
            get { return _GSTAmtI28; }
            set { _GSTAmtI28 = value; }
        }
        public double GSTS5
        {
            get { return _GSTS5; }
            set { _GSTS5 = value; }
        }
        public double GSTC5
        {
            get { return _GSTC5; }
            set { _GSTC5 = value; }
        }
        public double GSTI5
        {
            get { return _GSTI5; }
            set { _GSTI5 = value; }
        }
        public double GSTS12
        {
            get { return _GSTS12; }
            set { _GSTS12 = value; }
        }
        public double GSTC12
        {
            get { return _GSTC12; }
            set { _GSTC12 = value; }
        }
        public double GSTI12
        {
            get { return _GSTI12; }
            set { _GSTI12 = value; }
        }
        public double GSTS18
        {
            get { return _GSTS18; }
            set { _GSTS18 = value; }
        }
        public double GSTC18
        {
            get { return _GSTC18; }
            set { _GSTC18 = value; }
        }
        public double GSTI18
        {
            get { return _GSTI18; }
            set { _GSTI18 = value; }
        }
        public double GSTS28
        {
            get { return _GSTS28; }
            set { _GSTS28 = value; }
        }
        public double GSTC28
        {
            get { return _GSTC28; }
            set { _GSTC28 = value; }
        }
        public double GSTI28
        {
            get { return _GSTI28; }
            set { _GSTI28 = value; }
        }
        public string ScanCode
        {
            get { return _ScanCode; }
            set { _ScanCode = value; }
        }

        public string ClearedIn
        {
            get { return _ClearedIn; }
            set { _ClearedIn = value; }
        }

        public string StockID
        {
            get { return _StockID; }
            set { _StockID = value; }
        }
        
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }

        public string CreditAcID
        {
            get { return _CreditAcID; }
            set { _CreditAcID = value; }
        }
        public string Batchno
        {
            get { return _Batchno; }
            set { _Batchno = value; }
        }
        public double MRP
        {
            get { return _Mrp; }
            set { _Mrp = value; }
        }
        public double PurchaseRate
        {
            get { return _PurchaseRate; }
            set { _PurchaseRate = value; }
        }
        public double SaleRate
        {
            get { return _SaleRate; }
            set { _SaleRate = value; }
        }
        public double TradeRate
        {
            get { return _TradeRate; }
            set { _TradeRate = value; }
        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public int SchemeQuanity
        {
            get { return _SchemeQuantity; }
            set { _SchemeQuantity = value; }
        }
        public string Expiry
        {
            get { return _Expiry; }
            set { _Expiry = value; }
        }
        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        public string ReasonCode
        {
            get { return _ReasonCode; }
            set { _ReasonCode = value; }
        }

        public double VATPer
        {
            get { return _VATPer; }
            set { _VATPer = value; }
        }

        public int Closingstock
        {
            get { return _ClosingStock; }
            set { _ClosingStock = value; }
        }

        public int ClearVouNo
        {
            get { return _ClearVouNo; }
            set { _ClearVouNo = value; }
        }
        public string ClearVouType
        {
            get { return _ClearVouType; }
            set { _ClearVouType = value; }
        }
        public string AccountID
        {
            get { return _CrdbAccountId; }
            set { _CrdbAccountId = value; }
        }


        public string Particulars
        {
            get { return _Particulars; }
            set { _Particulars = value; }
        }

        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }


        public string CrdbVouDate
        {
            get { return _CrdbVouDate; }
            set { _CrdbVouDate = value; }
        }

        public string CrdbId
        {
            get { return _CrdbId; }
            set { _CrdbId = value; }
        }

        public string CrdbName
        {
            get { return _CrdbName; }
            set { _CrdbName = value; }
        }
        public string CrdbAddress
        {
            get { return _CrdbAddress; }
            set { _CrdbAddress = value; }
        }
        public string CrdbNarration
        {
            get { return _CrdbNarration; }
            set { _CrdbNarration = value; }
        }
        public string CrdbVouType
        {
            get { return _CrdbVouType; }
            set { _CrdbVouType = value; }
        }
        public string CrdbVouSeries
        {
            get { return _CrdbVouSeries; }
            set { _CrdbVouSeries = value; }
        }
        public string CrdbVouTypeDistributor
        {
            get { return _CrdbVouTypeDistributor; }
            set { _CrdbVouTypeDistributor = value; }
        }
        public int CrdbVouNo
        {
            get { return _CrdbVouNo; }
            set { _CrdbVouNo = value; }
        }
        public int CrdbNoOFRows
        {
            get { return _CrdbNoOfRows; }
            set { _CrdbNoOfRows = value; }
        }
        public double CrdbVat5
        {
            get { return _CrdbVat5; }
            set { _CrdbVat5 = value; }
        }
        public double CrdbVat12point5
        {
            get { return _CrdbVat12point5; }
            set { _CrdbVat12point5 = value; }
        }
        public double CrdbAmount
        {
            get { return _CrdbAmount; }
            set { _CrdbAmount = value; }
        }
        public double CrdbAmountClear
        {
            get { return _CrdbAmountClear; }
            set { _CrdbAmountClear = value; }
        }
        public double CrdbDiscPer
        {
            get { return _CrdbDiscPer; }
            set { _CrdbDiscPer = value; }
        }
        public double CrdbDiscAmt
        {
            get { return _CrdbDiscAmt; }
            set { _CrdbDiscAmt = value; }
        }
        public double CrdbAmountNet
        {
            get { return _CrdbAmountNet; }
            set { _CrdbAmountNet = value; }
        }

        public double CrdbTotalAmount
        {
            get { return _CrdbTotalAmount; }
            set { _CrdbTotalAmount = value; }
        }
        public double CrdbRoundAmount
        {
            get { return _CrdbRoundAmount; }
            set { _CrdbRoundAmount = value; }
        }

        public string SetExpiryFirst
        {
            get { return _SetExpiryFirst; }
            set { _SetExpiryFirst = value; }
        }

        public int ProdLoosePack
        {
            get { return _ProdLoosePack; }
            set { _ProdLoosePack = value; }
        }

        public double DiscountPercent
        {
            get { return _DiscountPercent; }
            set { _DiscountPercent = value; }
        }

        public double DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        public double ReturnRate
        {
            get { return _ReturnRate; }
            set { _ReturnRate = value; }
        }
        public string TrasferToAccount
        {
            get { return _TransferToAccount; }
            set { _TransferToAccount = value; }
        }
        //private string DetailID
        //{
        //    get { return _DetailID; }
        //    set { _DetailID = value; }
        //}
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();

            _Batchno = "";
            _Expiry = "";
            _ExpiryDate = "";
            _Mrp = 0;
            _ProductID = "";
            _PurchaseRate = 0;
            _Quantity = 0;
            _ReasonCode = "";
            _SaleRate = 0;
            _SchemeQuantity = 0;
            _TradeRate = 0;
            _ScanCode = "";

            _ProdLoosePack = 0;
            _DiscountPercent = 0;
            _DiscountAmount = 0;
            _ReturnRate = 0;
            _ClearedIn = "";

            _CrdbId = "";
            _CrdbName = "";
            _CrdbAccountId = "";
            _CrdbAddress = "";
            _CrdbNarration = "";
            _CrdbVouType = FixAccounts.VoucherTypeForCreditNoteStock;
          //  _CrdbVouTypeDistributor = FixAccounts.VoucherTypeForDistributorCreditNoteStock;
            _CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            _CrdbVouNo = 0;
            _CrdbVat5 = 0;
            _CrdbVat12point5 = 0;
            _CrdbNoOfRows = 0;
            _CrdbAmount = 0;
            _CrdbDiscPer = 0;
            _CrdbDiscAmt = 0;
            _CrdbAmountNet = 0;
            _CrdbRoundAmount = 0;
            _CrdbVouDate = "";
            _Particulars = "";
            _Amount = 0;
            _ClearVouNo = 0;
            _ClearVouType = "";
            _StockID = "";
            _CrdbAmountClear = 0;

            _SetExpiryFirst = "Y";
            _TransferToAccount = "Y";
            //_DetailID = "";
            _GSTAmt0 = 0;
            _GSTAmtS5 = 0;
            _GSTAmtC5 = 0;
            _GSTAmtI5 = 0;
            _GSTS5 = 0;
            _GSTC5 = 0;
            _GSTI5 = 0;
            _GSTAmtS12 = 0;
            _GSTAmtC12 = 0;
            _GSTAmtI12 = 0;
            _GSTS12 = 0;
            _GSTC12 = 0;
            _GSTI12 = 0;
            _GSTAmtS18 = 0;
            _GSTAmtC18 = 0;
            _GSTAmtI18 = 0;
            _GSTS18 = 0;
            _GSTC18 = 0;
            _GSTI18 = 0;
            _GSTAmtS28 = 0;
            _GSTAmtC28 = 0;
            _GSTAmtI28 = 0;
            _GSTS28 = 0;
            _GSTC28 = 0;
            _GSTI28 = 0;

            _GSTSAmount = 0;
            _GSTCAmount = 0;
            _GSTIAmount = 0;
            _GSTSPurchaseAmount = 0;
            _GSTCPurchaseAmount = 0;
            _GSTIPurchaseAmount = 0;
            _GSTPurchaseAmountZero = 0;



            _IfOMS = "N";
            _IfExemptedScheme = "N";

        }
        public override void DoValidate()
        {
            try
            {
                if (CrdbId == "")
                    ValidationMessages.Add("Please enter the Account Name.");
                if (CrdbAmount == 0)
                    ValidationMessages.Add("Invalid Amount");
                bool retValue = General.CheckDates(CrdbVouDate, CrdbVouDate);
                if (retValue == false)
                {
                    ValidationMessages.Add("Please Check Date...");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }
  
        #endregion

        #region Public Methods

        public DataTable GetOverviewDataCreditNotes(string fromDate, string toDate)
        {
            DBCreditNoteStock dbStock = new DBCreditNoteStock();
            return dbStock.GetOverviewDataCreditNotes(fromDate,toDate);
        }

        public DataTable GetOverviewData(string DbntType)
        {
            DBCreditNoteStock dbStock = new DBCreditNoteStock();
            return dbStock.GetOverviewData(DbntType);
        }
        public DataTable GetOverviewDataForParty(string AccID)
        {
            DBCreditNoteStock dbStock = new DBCreditNoteStock();
            return dbStock.GetOverviewDataForParty(AccID);
        }
        public DataTable GetOverviewDataForDebtorSale(string AccID, string ClearedInID)
        {
            DBCreditNoteStock dbStock = new DBCreditNoteStock();
            return dbStock.GetOverviewDataForDebtorSale(AccID,ClearedInID);
        }
        public DataTable GetOverviewDataForPatientSale(string AccID, string ClearedInID)
        {
            DBCreditNoteStock dbStock = new DBCreditNoteStock();
            return dbStock.GetOverviewDataForDebtorSale(AccID, ClearedInID);
        }

        public DataTable GetOverviewDataForLastPurchase(string productID, string CreditAcID)  //Amar
        {
            DBCreditNoteStock dbp = new DBCreditNoteStock();
            return dbp.GetOverviewDataForLastPurchase(productID, CreditAcID);
        }

        public int GetAndUpdateCNNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetCreditNote(voucherseries);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vouno;
        }


        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCreditNoteStock dbStock = new DBCreditNoteStock();
                drow = dbStock.ReadDetailsByID(Id);

                if (drow != null)
                {
                    CrdbId = drow["AccountId"].ToString();
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CrdbNarration = Convert.ToString(drow["Narration"]);
                    CrdbAmount = Convert.ToDouble(drow["Amount"].ToString());
                    CrdbDiscAmt = Convert.ToDouble(drow["DiscountAmount"].ToString());
                    CrdbDiscPer = Convert.ToDouble(drow["DiscountPer"].ToString());
                    CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                  //  CrdbVat12point5 = Convert.ToDouble(drow["VAT12point5"].ToString());
                 //   CrdbVat5 = Convert.ToDouble(drow["VAT5"].ToString());
                    CrdbTotalAmount = CrdbTotalAmount - CrdbDiscAmt;
                    CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                    CrdbAmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    if (drow["ClearedInVoucherType"] != DBNull.Value && drow["ClearedInVoucherType"].ToString().Trim() != "")
                    {
                        ClearedIn = drow["ClearedInVoucherType"].ToString() + " - " + drow["ClearedInVoucherNumber"].ToString() + " - " + General.GetDateInShortDateFormat(drow["ClearedInVoucherDate"].ToString());
                    }
                    else ClearedIn = "";

                    if (CrdbAmountClear > 0 && ClearVouNo == 0)
                        TrasferToAccount = "Y";
                    else
                        TrasferToAccount = "N";
                    if (drow["amountgst0"] != DBNull.Value)
                        GSTAmt0 = Convert.ToDouble(drow["amountgst0"].ToString());
                    if (drow["amountgsts5"] != DBNull.Value)
                        GSTAmtS5 = Convert.ToDouble(drow["amountgsts5"].ToString());
                    if (drow["amountgstc5"] != DBNull.Value)
                        GSTAmtC5 = Convert.ToDouble(drow["amountgstc5"].ToString());
                    if (drow["amountgsti5"] != DBNull.Value)
                        GSTAmtI5 = Convert.ToDouble(drow["amountgsti5"].ToString());
                    if (drow["amountgsts12"] != DBNull.Value)
                        GSTAmtS12 = Convert.ToDouble(drow["amountgsts12"].ToString());
                    if (drow["amountgstc12"] != DBNull.Value)
                        GSTAmtC12 = Convert.ToDouble(drow["amountgstc12"].ToString());
                    if (drow["amountgsti12"] != DBNull.Value)
                        GSTAmtI12 = Convert.ToDouble(drow["amountgsti12"].ToString());

                    if (drow["amountgsts18"] != DBNull.Value)
                        GSTAmtS18 = Convert.ToDouble(drow["amountgsts18"].ToString());
                    if (drow["amountgstc18"] != DBNull.Value)
                        GSTAmtC18 = Convert.ToDouble(drow["amountgstc18"].ToString());
                    if (drow["amountgsti18"] != DBNull.Value)
                        GSTAmtI18 = Convert.ToDouble(drow["amountgsti18"].ToString());

                    if (drow["amountgsts28"] != DBNull.Value)
                        GSTAmtS28 = Convert.ToDouble(drow["amountgsts28"].ToString());
                    if (drow["amountgstc28"] != DBNull.Value)
                        GSTAmtC28 = Convert.ToDouble(drow["amountgstc28"].ToString());
                    if (drow["amountgsti28"] != DBNull.Value)
                        GSTAmtI28 = Convert.ToDouble(drow["amountgsti28"].ToString());

                    if (drow["gsts5"] != DBNull.Value)
                        GSTS5 = Convert.ToDouble(drow["gsts5"].ToString());
                    if (drow["gstc5"] != DBNull.Value)
                        GSTC5 = Convert.ToDouble(drow["gstc5"].ToString());
                    if (drow["gsti5"] != DBNull.Value)
                        GSTI5 = Convert.ToDouble(drow["gsti5"].ToString());

                    if (drow["gsts12"] != DBNull.Value)
                        GSTS12 = Convert.ToDouble(drow["gsts12"].ToString());
                    if (drow["gstc12"] != DBNull.Value)
                        GSTC12 = Convert.ToDouble(drow["gstc12"].ToString());
                    if (drow["gsti12"] != DBNull.Value)
                        GSTI12 = Convert.ToDouble(drow["gsti12"].ToString());

                    if (drow["gsts18"] != DBNull.Value)
                        GSTS18 = Convert.ToDouble(drow["gsts18"].ToString());
                    if (drow["gstc18"] != DBNull.Value)
                        GSTC18 = Convert.ToDouble(drow["gstc18"].ToString());
                    if (drow["gsti18"] != DBNull.Value)
                        GSTI18 = Convert.ToDouble(drow["gsti18"].ToString());

                    if (drow["gsts28"] != DBNull.Value)
                        GSTS28 = Convert.ToDouble(drow["gsts28"].ToString());
                    if (drow["gstc28"] != DBNull.Value)
                        GSTC28 = Convert.ToDouble(drow["gstc28"].ToString());
                    if (drow["gsti28"] != DBNull.Value)
                        GSTI28 = Convert.ToDouble(drow["gsti28"].ToString());
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public DataRow ReadDetailsByVouNumber(string voutype, int vouno)
        {
           // bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCreditNoteStock dbStock = new DBCreditNoteStock();
                drow = dbStock.ReadDetailsByVouNumber(voutype,vouno);

                if (drow != null)
                {
                    CrdbId = drow["AccountId"].ToString();
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    Id = drow["CRDBID"].ToString();
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CrdbNarration = Convert.ToString(drow["Narration"]);
                    CrdbAmount = Convert.ToDouble(drow["Amount"].ToString());
                    CrdbDiscAmt = Convert.ToDouble(drow["DiscountAmount"].ToString());
                    CrdbDiscPer = Convert.ToDouble(drow["DiscountPer"].ToString());
                    CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    CrdbVat12point5 = Convert.ToDouble(drow["VAT12point5"].ToString());
                    CrdbVat5 = Convert.ToDouble(drow["VAT5"].ToString());
                    CrdbTotalAmount = CrdbAmount - CrdbDiscAmt;
                    CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                   // retValue = true;
                }
                else
                {
                    Id = "";
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return drow;
        }
        public bool ReadDetailsByVouNumberForDistributor(int vouno)
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCreditNoteStock dbStock = new DBCreditNoteStock();
                drow = dbStock.ReadDetailsByVouNumberForDistributor(vouno);

                if (drow != null)
                {
                    CrdbId = drow["AccountId"].ToString();
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    Id = drow["CRDBID"].ToString();
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CrdbNarration = Convert.ToString(drow["Narration"]);
                    CrdbAmount = Convert.ToDouble(drow["Amount"].ToString());
                    CrdbDiscAmt = Convert.ToDouble(drow["DiscountAmount"].ToString());
                    CrdbDiscPer = Convert.ToDouble(drow["DiscountPer"].ToString());
                    CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    CrdbVat12point5 = Convert.ToDouble(drow["VAT12point5"].ToString());
                    CrdbVat5 = Convert.ToDouble(drow["VAT5"].ToString());
                    CrdbTotalAmount = CrdbAmount - CrdbDiscAmt;
                    CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                    retValue = true;
                }
                else
                {
                    Id = "";
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public DataTable  ReadProductDetailsByID()
        {
           
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                if (Id != null && Id != "")
                {
                    DBCreditNoteStock dbStock = new DBCreditNoteStock();
                    dt = dbStock.ReadProductDetailsByID(Id);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
          
            return dt;
        }

        public DataTable ReadProductDetails()
        {

            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBProduct dbStock = new DBProduct();
                dt = dbStock.GetOverviewData();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }

        //public DataTable ReadDetailsByAccountID(string AccId)
        //{
        //    DataTable dt = new DataTable();
        //    dt = null;
        //    try
        //    {
        //        DBCreditNoteStock dbStock = new DBCreditNoteStock();
        //        dt = dbStock.ReadDetailsByAccountID(AccId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    return dt;  
        //}
        //public DataTable ReadDetailsByAccountIDforEditPurchase(string AccId, int vouno, string voutype, string vouseries)
        //{
        //    DataTable dt = new DataTable();
        //    dt = null;
        //    try
        //    {
        //        DBCreditNoteStock dbStock = new DBCreditNoteStock();
        //        dt = dbStock.ReadDetailsByAccountIDforEditPurchase(AccId,vouno,voutype,vouseries);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //    return dt;
        //}

        public int AddDetails()
        {
            DBCreditNoteStock dbcrdb = new DBCreditNoteStock();
            return dbcrdb.AddDetails(Id, CrdbId, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbAmountNet,
               CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbRoundAmount,  CrdbAmountClear, ClearVouType,
              GSTAmt0, GSTAmtS5, GSTAmtS12, GSTAmtS18, GSTAmtS28, GSTAmtC5, GSTAmtC12, GSTAmtC18, GSTAmtC28,
           GSTS5, GSTS12, GSTS18, GSTS28, GSTC5, GSTC12, GSTC18, GSTC28, GSTAmtI5, GSTAmtI12, GSTAmtI18, GSTAmtI28, GSTI5, GSTI12, GSTI18, GSTI28, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddProductDetails()
        {
            DBCreditNoteStock dbcrdbp = new DBCreditNoteStock();
            return dbcrdbp.AddDetailsProducts(IntID, StockID, ProductID, Batchno, Quantity, SchemeQuanity, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, ReasonCode, VATPer, Amount, CrdbVouType, CrdbVouNo, CrdbVouDate, DiscountPercent, DiscountAmount, TradeRate,
                ReturnRate, DetailId,SerialNumber, GSTPurchaseAmountZero, GSTSPurchaseAmount, GSTCPurchaseAmount, GSTIPurchaseAmount,  GSTSAmount, GSTCAmount, GSTIAmount);
        }

        public bool UpdateDetails()
        {
            DBCreditNoteStock dbcrdb = new DBCreditNoteStock();
            return dbcrdb.UpdateDetails(Id, CrdbId, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbAmountNet,
                CrdbDiscPer, CrdbDiscAmt, CrdbTotalAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,  CrdbAmountClear, ClearVouType, ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBCreditNoteStock dbcrdb = new DBCreditNoteStock();
            return dbcrdb.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBCreditNoteStock dbcrdb = new DBCreditNoteStock();
            return dbcrdb.DeleteProductsByMasterID(Id);
        }


        public string CheckForBatchMRPInStockTable()
        {
            DBSsStock sstk = new DBSsStock();
            DataRow drow = null;
            string ifrowfound = "";
            try
            {
                drow = sstk.GetRecordByProductBatchMRP(ProductID, Batchno, MRP);
                if (drow != null)
                    ifrowfound = (drow["stockID"].ToString());
               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return ifrowfound;

        }

        public string CheckStockForBatchMRPInStockTable()
        {
            DBSsStock sstk = new DBSsStock();
            DataRow drow = null;
            string ifrowfound = "N";
            try
            {               
                drow = sstk.GetRecordByProductIDAndBatchNumberAndMRP(StockID, Quantity);
                if (drow != null)
                    ifrowfound = "Y";
                else
                    ifrowfound = "N";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return ifrowfound;
        }

        public bool UpdateIntblStock()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateCreditNoteStock(StockID, Quantity);
            
        }
        public bool UpdateIntblStockForDistributor()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateCreditNoteStock(StockID, Quantity);

        }
        public bool InsertNewBatchIntblStock()
        {
            DBSsStock sstk = new DBSsStock();   
            return sstk.InsertCreditNoteStock( StockID,ProductID, Batchno, MRP, Quantity, Quantity, Expiry, VATPer, PurchaseRate, SaleRate, TradeRate,ExpiryDate);
        }

        public bool UpdateCreditNoteStockInMasterProduct()
        {
            Closingstock = GetClosingStock();
            if (Closingstock == 0)
            {
                DBProduct dbprod = new DBProduct();
                return dbprod.UpdateCreditNoteStockInmasterProductForNULLClosingStock(ProductID, Quantity);
            }
            else
            {
                DBProduct dbprod = new DBProduct();
                Closingstock += Quantity;
                return dbprod.UpdateCreditNoteStockInmasterProduct(ProductID, Closingstock);
            }
        }
        public bool UpdateCreditNoteStockInMasterProductForDistributor()
        {
            Closingstock = GetClosingStock();
            if (Closingstock == 0)
            {
                DBProduct dbprod = new DBProduct();
                return dbprod.UpdateCreditNoteStockInmasterProductForNULLClosingStock(ProductID, Quantity*ProdLoosePack);
            }
            else
            {
                DBProduct dbprod = new DBProduct();
                Closingstock += (Quantity*ProdLoosePack);
                return dbprod.UpdateCreditNoteStockInmasterProduct(ProductID, Closingstock);
            }
        }
        public int GetClosingStock()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetClosingStock(ProductID);
        }
        public DataRow IfStockIDFoundInStockTable(string stockID)
        {
            DBSsStock dbssstk = new DBSsStock();
            return dbssstk.IfStockIDFoundInStockTable(stockID);

        }
        public bool AddProductDetailsInStockTable()
        {
            DBCreditNoteStock dbpur = new DBCreditNoteStock();
            return dbpur.AddProductDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, Amount, AccountID, ProdLoosePack,ScanCode, StockID);
            
        }
        public string AddProductDetailsInStockTableForDistributor()
        {
            string stockid = "";
            int mstockid = 0;

            DBCreditNoteStock dbpur = new DBCreditNoteStock();
            mstockid = dbpur.AddProductDetailsInStockTableForDistributor(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, Amount, AccountID, ProdLoosePack, ScanCode, StockID);
            stockid = Convert.ToString(mstockid);
            return stockid;

        }

        //}
        public bool UpdateProductDetailsInStockTable()
        {
            DBSsStock sstk = new DBSsStock();
            //return sstk.UpdateProductDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
            //    Expiry, ExpiryDate, Quantity, SchemeQuanity, ReplacementQuantity, PurchaseVATPercent,
            //    ProductVATPercent, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount, AccountID, PurchaseBillNumber,
            //    VoucherType, VoucherNumber, VoucherDate, ProdLoosePack, StockID, ProductMargin, ProdLoosePack);
            return true;
        }
        public int GetCurrentClosingStock(string thisStockID)
        {
            int thisclosingstock = 0;
            try
            {
                DBSsStock sstk = new DBSsStock();
                DataRow drow = null;
                drow = sstk.GetCurrentClosingStockByThisStockID(thisStockID);
                if (drow != null)
                {
                    if (drow["ClosingStock"] != DBNull.Value)
                        thisclosingstock = Convert.ToInt32(drow["ClosingStock"].ToString());
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return thisclosingstock;
        }
        public bool UpdateIntblStockReduce()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateCreditNoteStockReduceFromTemp(StockID, Quantity);

        }
        public bool UpdateIntblStockReduceForDistributor()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateCreditNoteStockReduceFromTemp(StockID, Quantity * ProdLoosePack);

        }
        public bool UpdateCreditNoteStockInMasterProductReduce()
        {

             int Closingstock = GetClosingStock();
           
                DBProduct dbprod = new DBProduct();
                Closingstock -= Quantity;
                return dbprod.UpdateCreditNoteStockInmasterProductReduceFromTemp(ProductID, Closingstock); 
        }
        public bool UpdateCreditNoteStockInMasterProductReduceForDistributor()
        {

            int Closingstock = GetClosingStock();

            DBProduct dbprod = new DBProduct();
            Closingstock -= Quantity * ProdLoosePack;
            return dbprod.UpdateCreditNoteStockInmasterProductReduceFromTemp(ProductID, Closingstock);
        }
        #endregion

        public bool AddVoucherIntblTrnac()
        {
            DBCreditNoteStock dbc = new DBCreditNoteStock();
            return dbc.AddVoucherIntblTrnac(IntID, DebitAccount, CreditAccount, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, DebitAmount, CreditAmount, DetailId, CreatedBy, CreatedDate, CreatedTime);
        }

        public void DeleteFromtblTrnac()
        {
            DBCreditNoteStock dbc = new DBCreditNoteStock();
            dbc.DeleteFromtblTrnac(Id);
        }

        public bool AddAccountDetailsIntbltrnacDebit()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (CrdbAmountNet > 0)
            {
                bRetValue = dbpur.AddDetailsForAccountsCashReceipt(IntID, DetailId, FixAccounts.AccountDebitNotePurchase.ToString(), 0, CrdbAmountNet, CrdbId, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbNarration, CreatedBy, CreatedDate, CreatedTime);
            }
            return bRetValue;
        }
        public bool AddAccountDetailsIntbltrnacCredit()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (CrdbAmountNet > 0)
            {
                bRetValue = dbpur.AddDetailsForAccountsCashReceipt(IntID, DetailId, CrdbId, CrdbAmountNet, 0, FixAccounts.AccountDebitNotePurchase.ToString(), CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbNarration, CreatedBy, CreatedDate, CreatedTime);
            }
            return bRetValue;
        }

        public bool RemoveAccountDetails()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (CrdbAmountNet > 0)
            {
                bRetValue = dbpur.DeleteAccountDetailsFromtbltrnac(Id);
            }
            return bRetValue;
        }
        public void GetLastRecord()
        {
            DataRow dr;
            try
            {
                DBCreditNoteStock dbc = new DBCreditNoteStock();
                dr = dbc.GetLastRecord(CrdbVouType, CrdbVouSeries);
                if (dr != null && dr["CRDBID"] != null)
                {

                    Id = dr["CRDBID"].ToString();

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        public int GetLastVoucherNumber(string vouType, string vouSeries)
        {
            DataRow dr;
            int lastvouno = 0;
            try
            {
                DBCreditNoteStock dbc = new DBCreditNoteStock();
                dr = dbc.GetLastVoucherNumber(vouType, vouSeries);
                if (dr != null)
                {

                    lastvouno = Convert.ToInt32(dr["VoucherNumber"].ToString());

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return lastvouno;
        }

        public DataRow GetFirstRecord()
        {
            DataRow dr = null;
            try
            {
                DBCreditNoteStock dbc = new DBCreditNoteStock();
                dr = dbc.GetFirstRecord(CrdbVouType, CrdbVouSeries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }
    }
}
