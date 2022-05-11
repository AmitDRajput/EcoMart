using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
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

        private string _CrdbId;
        private string _CrdbAccountId;
        private string _CrdbName;
        private string _CrdbAddress;
        private string _CrdbNarration;
        private string _CrdbVouType;
        private int _CrdbVouNo;
        private string _CrdbVouDate;
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
      
    
        # endregion

        #region Constructors
        public CreditNoteStock()
        {
            Initialise();
        }
        #endregion

        # region properties

       
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

            _ProdLoosePack = 0;
            _DiscountPercent = 0;
            _DiscountAmount = 0;
            _ReturnRate = 0;

            _CrdbId = "";
            _CrdbName = "";
            _CrdbAccountId = "";
            _CrdbAddress = "";
            _CrdbNarration = "";
            _CrdbVouType = FixAccounts.VoucherTypeForCreditNoteStock;
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
          

        }
        public override void DoValidate()
        {
            try
            {
                if (CrdbId == "")
                    ValidationMessages.Add("Please enter the Account Name.");
                if (CrdbAmount == 0)
                    ValidationMessages.Add("Invalid Amount");
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
                    CrdbVat12point5 = Convert.ToDouble(drow["VAT12point5"].ToString());
                    CrdbVat5 = Convert.ToDouble(drow["VAT5"].ToString());
                    CrdbTotalAmount = CrdbAmount - CrdbDiscAmt;
                    CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public bool ReadDetailsByVouNumber(int vouno)
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCreditNoteStock dbStock = new DBCreditNoteStock();
                drow = dbStock.ReadDetailsByVouNumber(vouno);

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

        public bool AddDetails()
        {
            DBCreditNoteStock dbcrdb = new DBCreditNoteStock();
            return dbcrdb.AddDetails(Id, CrdbId, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbAmountNet,
               CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,CrdbAmountClear, CreatedBy,CreatedDate,CreatedTime);
        }
        public bool AddProductDetails()
        {
            DBCreditNoteStock dbcrdbp = new DBCreditNoteStock();
            return dbcrdbp.AddDetailsProducts(Id, StockID, ProductID, Batchno, Quantity, SchemeQuanity, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, ReasonCode, VATPer, Amount, CrdbVouType, CrdbVouNo, CrdbVouDate, DiscountPercent, DiscountAmount, TradeRate,ReturnRate, DetailId);
        }

        public bool UpdateDetails()
        {
            DBCreditNoteStock dbcrdb = new DBCreditNoteStock();
            return dbcrdb.UpdateDetails(Id, CrdbId, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbAmountNet,
                CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,CrdbAmountClear, ModifiedBy,ModifiedDate,ModifiedTime);
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
            string ifrowfound = "N";
            try
            {
                drow = sstk.GetRecordByProductBatchMRP(ProductID, Batchno, MRP);
                if (drow != null)
                    ifrowfound = drow["stockID"].ToString();
               
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
                Expiry, ExpiryDate, Quantity, SchemeQuanity, Amount, AccountID, ProdLoosePack, StockID);
            
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
        public bool UpdateCreditNoteStockInMasterProductReduce()
        {

             int Closingstock = GetClosingStock();
           
                DBProduct dbprod = new DBProduct();
                Closingstock -= Quantity;
                return dbprod.UpdateCreditNoteStockInmasterProductReduceFromTemp(ProductID, Closingstock);
           





            //DBProduct dbprod = new DBProduct();
            //return dbprod.UpdateCreditNoteStockInmasterProductReduceFromTemp(ProductID, Quantity);
        }
    
        #endregion








        public bool AddVoucherIntblTrnac()
        {
            DBCreditNoteStock dbc = new DBCreditNoteStock();
            return dbc.AddVoucherIntblTrnac(Id, DebitAccount, CreditAccount, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, DebitAmount, CreditAmount, DetailId, CreatedBy, CreatedDate, CreatedTime);
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
                bRetValue = dbpur.AddDetailsForAccountsCashReceipt(Id, DetailId, FixAccounts.AccountDebitNotePurchase, 0, CrdbAmountNet, CrdbId, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbNarration, CreatedBy, CreatedDate, CreatedTime);
            }
            return bRetValue;
        }
        public bool AddAccountDetailsIntbltrnacCredit()
        {
            bool bRetValue = false;
            DBAccountDetails dbpur = new DBAccountDetails();

            if (CrdbAmountNet > 0)
            {
                bRetValue = dbpur.AddDetailsForAccountsCashReceipt(Id, DetailId, CrdbId, CrdbAmountNet, 0, FixAccounts.AccountDebitNotePurchase, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbNarration, CreatedBy, CreatedDate, CreatedTime);
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
    }
}
