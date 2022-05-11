using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class DebitNoteStock : BaseObject
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
        private double _VATAmount;
        private int _ClosingStock;
        private int _ProdLoosePack;
        private double _DiscountPercent;
        private double _DiscountAmount;
        private string _IfAddVATInTradeRate;
        private int _CurrentProductStock;
        private int _CurrentBatchStock;

        private string _CrdbId;
        private string _CrdbAccountId;
        private string _CrdbAccountName;
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

       
        private double _CrdbPreSelectedAmount;
        private string _DetailIDForSelected;
        private string _IDForSelected;
        private int _CrdbVouNoForSelected;
        private double _CrdbBillAmountForSelected;
        private double _CrdbAmountForSelected;
        private double _CrdbVat5ForSelected;
        private double _CrdbVat12point5ForSelected;
        private double _crdbDiscAmtForSelected;

        private int _ClearVouNo;
        private string _ClearVouType;
        private string _SetExpiryFirst;
        private string _StockID;

        private string _TransferToAccount;
        private double _CrdbAmountClear;
        // private string _DetailID;

        private string _ClearedIn;
        # endregion

        #region Constructors
        public DebitNoteStock()
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

        # region properties

        public string ClearedIn
        {
            get { return _ClearedIn; }
            set { _ClearedIn = value; }
        }
        public int CurrentProductStock
        {
            get { return _CurrentProductStock; }
            set { _CurrentProductStock = value; }
        }
        public int CurrentBatchStock
        {
            get { return _CurrentBatchStock; }
            set { _CurrentBatchStock = value; }
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
        public double VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
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

        public string CrdbAccountName
        {
            get { return _CrdbAccountName; }
            set { _CrdbAccountName = value; }
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
        public double CrdbBillAmountForSelected
        {
            get { return _CrdbBillAmountForSelected; }
            set { _CrdbBillAmountForSelected = value; }
        }

        public double CrdbPreSelectedAmount
        {
            get { return _CrdbPreSelectedAmount; }
            set { _CrdbPreSelectedAmount = value; }
        }

        public string DetailIDForSelected
        {
            get { return _DetailIDForSelected; }
            set { _DetailIDForSelected = value; }
        }

        public string IDForSelected
        {
            get { return _IDForSelected; }
            set { _IDForSelected = value; }
        }
        public int CrdbVouNoForSelected
        {
            get { return _CrdbVouNoForSelected; }
            set { _CrdbVouNoForSelected = value; }
        }
        public double CrdbAmountForSelected
        {
            get { return _CrdbAmountForSelected; }
            set { _CrdbAmountForSelected = value; }
        }
        public double CrdbVat5ForSelected
        {
            get { return _CrdbVat5ForSelected; }
            set { _CrdbVat5ForSelected = value; }
        }
        public double CrdbVat12point5ForSelected
        {
            get { return _CrdbVat12point5ForSelected; }
            set { _CrdbVat12point5ForSelected = value; }
        }
        public double crdbDiscAmtForSelected
        {
            get { return _crdbDiscAmtForSelected; }
            set { _crdbDiscAmtForSelected = value; }
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

        public string IfAddVATInTradeRate
        {
            get { return _IfAddVATInTradeRate; }
            set { _IfAddVATInTradeRate = value; }
        }
        public string TrasferToAccount
        {
            get { return _TransferToAccount; }
            set { _TransferToAccount = value; }
        }
        public double CrdbAmountClear
        {
            get { return _CrdbAmountClear; }
            set { _CrdbAmountClear = value; }
        }
        #endregion


        #region Internal Methods

        public override void Initialise()
        {
            try
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
                _VATPer = 0;
                _VATAmount = 0;
                _TradeRate = 0;
                _ProdLoosePack = 0;
                _DiscountPercent = 0;
                _DiscountAmount = 0;
                _IfAddVATInTradeRate = "";

                _ClearedIn = "";

                _CrdbId = "";
                _CrdbAccountName = "";
                _CrdbAccountId = "";
                _CrdbAddress = "";
                _CrdbNarration = "";
                _CrdbVouType = FixAccounts.VoucherTypeForDebitNoteStock;
                _CrdbVouNo = 0;
                _CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
                _CrdbVat5 = 0;
                _CrdbVat12point5 = 0;
                _CrdbNoOfRows = 0;
                _CrdbAmount = 0;
                _CrdbDiscPer = 0;
                _CrdbDiscAmt = 0;
                _CrdbAmountNet = 0;
                _CrdbRoundAmount = 0;
               
                _CrdbPreSelectedAmount = 0;
                _CrdbVouNoForSelected = 0;
                _IDForSelected = "";
                _DetailIDForSelected = "";
                _CrdbBillAmountForSelected = 0;
                _CrdbAmountForSelected = 0;
                _CrdbVat5ForSelected = 0;
                _CrdbVat12point5ForSelected = 0;
                _crdbDiscAmtForSelected = 0;

                _CrdbVouDate = "";
                _Particulars = "";
                _Amount = 0;
                _ClearVouNo = 0;
                _ClearVouType = "";
                _StockID = "";
                _SetExpiryFirst = "Y";
                _CurrentBatchStock = 0;
                _CurrentProductStock = 0;

                _TransferToAccount = "";
                _CrdbAmountClear = 0;

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        public override void DoValidate()
        {
            try
            {
                if (CrdbId == "")
                    ValidationMessages.Add("Please Enter Account.");
                if ((CrdbAmountNet == 0) && TrasferToAccount != "Y")
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
        public DataTable GetOverviewData(string DbntType)
        {
            DBDebitNoteStock dbStock = new DBDebitNoteStock();
            return dbStock.GetOverviewData(DbntType);
        }
        public DataTable GetOverviewDataForAllYears(string DbntType)
        {
            DBDebitNoteStock dbStock = new DBDebitNoteStock();
            return dbStock.GetOverviewDataForAllYears(DbntType);
        }
        public DataTable GetOverviewDataDebitNotes(string fromDate, string toDate)
        {
            DBDebitNoteStock dbStock = new DBDebitNoteStock();
            return dbStock.GetOverviewDataDebitNotes(fromDate, toDate);
        }
        public DataTable GetOverviewDataDebitNotesPending(string fromDate, string toDate)
        {
            DBDebitNoteStock dbStock = new DBDebitNoteStock();
            return dbStock.GetOverviewDataDebitNotesPending(fromDate, toDate);
        }
        public DataTable GetOverviewDataDebitNotesOnlyPending(string fromDate, string toDate)
        {
            DBDebitNoteStock dbStock = new DBDebitNoteStock();
            return dbStock.GetOverviewDataDebitNotesOnlyPending(fromDate, toDate);
        }
        public DataTable GetOverviewDataForParty(string AccID)
        {
            DBDebitNoteStock dbStock = new DBDebitNoteStock();
            return dbStock.GetOverviewDataForParty(AccID);
        }

        public int GetAndUpdateDNNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetDebitNote(voucherseries);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                    if (drow["VoucherSeries"] != DBNull.Value)
                        CrdbVouSeries = drow["VoucherSeries"].ToString();
                    CrdbNarration = Convert.ToString(drow["Narration"]);
                    CrdbTotalAmount = Convert.ToDouble(drow["Amount"].ToString());
                    CrdbDiscAmt = Convert.ToDouble(drow["DiscountAmount"].ToString());
                    CrdbDiscPer = Convert.ToDouble(drow["DiscountPer"].ToString());
                    CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());

                    CrdbVat12point5 = Convert.ToDouble(drow["VAT12point5"].ToString());
                    CrdbVat5 = Convert.ToDouble(drow["VAT5"].ToString());
                  
                    CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                    CrdbAmount = CrdbAmountNet + CrdbRoundAmount;
                    CrdbAmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    if (drow["ClearedInVoucherNumber"] != DBNull.Value)
                    {
                        ClearVouNo = Convert.ToInt32(drow["ClearedInVoucherNumber"].ToString());
                        ClearVouType = drow["ClearedInVoucherType"].ToString();
                    }
                    if (drow["ClearedInVoucherType"] != DBNull.Value && drow["ClearedInVoucherType"].ToString().Trim() != "")
                    {
                        ClearedIn = drow["ClearedInVoucherType"].ToString() + " - " + drow["ClearedInVoucherNumber"].ToString() + " - " + General.GetDateInShortDateFormat(drow["ClearedInVoucherDate"].ToString());
                    }
                    else ClearedIn = "";
                    if (CrdbAmountClear > 0 && ClearVouNo == 0)
                        TrasferToAccount = "Y";
                    else
                        TrasferToAccount = "N";
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public DataRow ReadDetailsByVouNumber(int vouno)
        {
           // bool retValue = false;
            DataRow drow = null;
            try
            {
                DBDebitNoteStock dbStock = new DBDebitNoteStock();
                drow = dbStock.ReadDetailsByVouNumber(vouno);
                if (drow != null)
                {
                    Id = drow["CRDBID"].ToString();
                    CrdbId = drow["AccountId"].ToString();
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CrdbNarration = Convert.ToString(drow["Narration"]);
                    CrdbTotalAmount = Convert.ToDouble(drow["Amount"].ToString());
                    CrdbDiscAmt = Convert.ToDouble(drow["DiscountAmount"].ToString());
                    CrdbDiscPer = Convert.ToDouble(drow["DiscountPer"].ToString());
                    CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    CrdbVat12point5 = Convert.ToDouble(drow["VAT12point5"].ToString());
                    CrdbVat5 = Convert.ToDouble(drow["VAT5"].ToString());
                    CrdbAmount = CrdbTotalAmount - CrdbDiscAmt;
                    CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["ClearedInVoucherNumber"] != DBNull.Value)
                    {
                        ClearVouNo = Convert.ToInt32(drow["ClearedInVoucherNumber"].ToString());
                        ClearVouType = drow["ClearedInVoucherType"].ToString();
                    }
                   // retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return drow;
        }

        public DataTable ReadProductDetailsByID()
        {

            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBDebitNoteStock dbStock = new DBDebitNoteStock();
                dt = dbStock.ReadProductDetailsByID(Id);

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
        public DataTable ReadExpiredStock(string mdate)
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBSsStock _dbStock = new DBSsStock();
                dt = _dbStock.ReadExpiredStock(mdate);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }
        public DataTable ReadExpiredStock(string mdate, string acID)
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBSsStock _dbStock = new DBSsStock();
                dt = _dbStock.ReadExpiredStock(mdate, acID);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }
        public bool AddDetails()
        {
            DBDebitNoteStock dbcrdb = new DBDebitNoteStock();
            return dbcrdb.AddDetails(Id, CrdbId, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbAmountNet,
               CrdbDiscPer, CrdbDiscAmt, CrdbTotalAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount, CrdbAmountClear,CrdbVouSeries,ClearVouType, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddDetailsForSelected()
        {
            DBDebitNoteStock dbcrdb = new DBDebitNoteStock();
            return dbcrdb.AddDetails(IDForSelected, CrdbId, CrdbNarration, CrdbVouType, CrdbVouNoForSelected, CrdbVouDate, CrdbBillAmountForSelected,
               CrdbDiscPer, crdbDiscAmtForSelected, CrdbBillAmountForSelected, CrdbVat5ForSelected, CrdbVat12point5ForSelected, CrdbRoundAmount, 0, CrdbVouSeries, ClearVouType, CreatedBy, CreatedDate, CreatedTime);
        }
        public bool AddProductDetails()
        {
            DBDebitNoteStock dbcrdbp = new DBDebitNoteStock();
            return dbcrdbp.AddDetailsProducts(Id, StockID, ProductID, Batchno, Quantity, SchemeQuanity, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, ReasonCode, VATPer, Amount, CrdbVouType, CrdbVouNo, CrdbVouDate, DiscountPercent, DiscountAmount, TradeRate, VATAmount, IfAddVATInTradeRate, DetailId, SerialNumber);
        }
        public bool AddProductDetailsForSelected()
        {
            DBDebitNoteStock dbcrdbp = new DBDebitNoteStock();
            return dbcrdbp.AddDetailsProducts(IDForSelected, StockID, ProductID, Batchno, Quantity, SchemeQuanity, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, ReasonCode, VATPer, Amount, CrdbVouType, CrdbVouNo, CrdbVouDate, DiscountPercent, DiscountAmount, TradeRate, VATAmount, IfAddVATInTradeRate, DetailIDForSelected, SerialNumber);
        }
        public bool UpdateDetails()
        {
            DBDebitNoteStock dbcrdb = new DBDebitNoteStock();
            return dbcrdb.UpdateDetails(Id, CrdbId, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbAmountNet,
                CrdbDiscPer, CrdbDiscAmt, CrdbTotalAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount, CrdbAmountClear, CrdbVouSeries, ClearVouType, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBDebitNoteStock dbcrdb = new DBDebitNoteStock();
            return dbcrdb.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBDebitNoteStock dbcrdb = new DBDebitNoteStock();
            return dbcrdb.DeleteProductsByMasterID(Id);
        }

        public string CheckForStockIDInStockTable()
        {
            DBSsStock sstk = new DBSsStock();
            DataRow drow = null;
            string ifrowfound = "N";
            try
            {
                drow = sstk.GetRecordByStockID(StockID);
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
            if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                return sstk.UpdateDebitNoteStock(StockID, ProdLoosePack * Quantity, SchemeQuanity * ProdLoosePack);
            else
                return sstk.UpdateDebitNoteStock(StockID, Quantity, SchemeQuanity);

        }

        public bool UpdateDebitNoteStockInMasterProduct()
        {
            int Closingstock = GetClosingStock();
            DBProduct dbprod = new DBProduct();
            if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                Closingstock -= (Quantity + SchemeQuanity) * ProdLoosePack;
            else
                Closingstock -= (Quantity + SchemeQuanity);
            return dbprod.UpdateDebitNoteStockInmasterProduct(ProductID, Closingstock);
        }
        public int GetClosingStock()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetClosingStock(ProductID);
        }
        public bool UpdateIntblStockAdd()
        {
            DBSsStock sstk = new DBSsStock();
            if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                return sstk.UpdateDebitNoteStockAddFromTemp(StockID, (Quantity + SchemeQuanity) * ProdLoosePack);
            else
                return sstk.UpdateDebitNoteStockAddFromTemp(StockID, (Quantity + SchemeQuanity));

        }
        public bool UpdateDebitNoteStockInMasterProductAddFromTemp()
        {
            int Closingstock = GetClosingStock();
            DBProduct dbprod = new DBProduct();
            if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                Closingstock += (Quantity + SchemeQuanity) * ProdLoosePack;
            else
                Closingstock += (Quantity + SchemeQuanity);
            return dbprod.UpdateDebitNoteStockInmasterProductAddFromTemp(ProductID, Closingstock);
        }
        #endregion

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
                DBDebitNoteStock dbs = new DBDebitNoteStock();
                dr = dbs.GetLastRecord(CrdbVouType, CrdbVouSeries);
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
                DBDebitNoteStock dbs = new DBDebitNoteStock();
                dr = dbs.GetLastVoucherNumber(vouType, vouSeries);
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
                DBDebitNoteStock dbs = new DBDebitNoteStock();
                dr = dbs.GetFirstRecord(CrdbVouType, CrdbVouSeries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }
    }
}
