using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class DebitNoteExpiry : BaseObject
    {

        #region Declaration
        private string _ProductID;
        private int _UOM;
        private string _Batchno;
        private double _Mrp;
        private double _PurchaseRate;
        private double _SaleRate;
        private double _TradeRate;
        private int _Quantity;
        private double _DiscountPercentProduct;
        private double _DiscountAmountProduct;
        private int _SchemeQuantity;
        private string _Expiry;
        private string _ExpiryDate;
        private string _ReasonCode;
        private double _VATPer;
        private int _ClosingStock;

        private string _CrdbId;
        private string _CrdbAccountId;
        private string _CrdbName;
        private string _CrdbAddress;
        private string _CrdbNarration;
        private string _CrdbVouType;
        private int _CrdbVouNo;
        private string _CrdbVouDate;
        private int _CrdbNoOfRows;
        private double _CrdbAmount;
        private double _CrdbDiscPer;
        private double _CrdbDiscAmt;
        private double _CrdbAmountNet;
        private double _CrdbRoundAmount;
        private double _CrdbTotalAmount;     
        private string _Particulars;
        private double _Amount;
        private int _ClearVouNo;
        private int _CrdbMonth;
        private int _CrdbYear;
        private string _StockID;
        private string _SetExpiryFirst;      

        # endregion

        #region Constructors, Destructors
        public DebitNoteExpiry()
        {
            try
            {
                Initialise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
        public int UOM
        {
            get { return _UOM; }
            set { _UOM = value; }

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
        public double DiscountAmountProduct
        {
            get { return _DiscountAmountProduct; }
            set { _DiscountAmountProduct = value; }
        }
        public double DiscountPercentProduct
        {
            get { return _DiscountPercentProduct; }
            set { _DiscountPercentProduct = value; }
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
        public int CrdbMonth
        {
            get { return _CrdbMonth; }
            set { _CrdbMonth = value; }
        }
        public int CrdbYear
        {
            get { return _CrdbYear; }
            set { _CrdbYear = value; }
        }

        #endregion

        #region Public Methods
        //public DataTable GetOverviewData()
        //{
        //    DBPatient dbPatient = new DBPatient();
        //    return dbPatient.GetOverviewData();
        //}

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();

            _Batchno = "";
            _Expiry = "";
            _ExpiryDate = "";
            _Mrp = 0;
            _ProductID = "";
            _UOM = 0;
            _PurchaseRate = 0;
            _Quantity = 0;
            _ReasonCode = "";
            _SaleRate = 0;
            _SchemeQuantity = 0;
            _TradeRate = 0;

            _CrdbId = "";
            _CrdbName = "";
            _CrdbAccountId = "";
            _CrdbAddress = "";
            _CrdbNarration = "";
            _CrdbVouType = FixAccounts.VoucherTypeForDebitNoteStock;
            _CrdbVouNo = 0;
            _CrdbNoOfRows = 0;
            _CrdbAmount = 0;
            _CrdbDiscPer = 0;
            _CrdbDiscAmt = 0;
            _CrdbAmountNet = 0;
            _CrdbTotalAmount = 0;
            _DiscountPercentProduct = 0;
            _DiscountAmountProduct = 0;
            _CrdbRoundAmount = 0;
            _CrdbVouDate = "";
            _Particulars = "";
            _Amount = 0;
            _ClearVouNo = 0;
            _CrdbMonth = 0;
            _CrdbYear = 0;
            _StockID = "";

            _SetExpiryFirst = "Y";
            
        }

        public override void DoValidate()
        {
            try
            {
                if (CrdbMonth == 0)
                    ValidationMessages.Add("Please enter Month.");
                if (CrdbYear == 0)
                    ValidationMessages.Add("Please enter Year.");
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
        #endregion

        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }

        public int GetAndUpdateDNNumber(string voucherseries)
        {
            int vouno = 0;
            DBGetVouNumbers dbno = new DBGetVouNumbers();
            vouno = dbno.GetDebitNote(voucherseries);
            return vouno;
        }
        public bool AddDetails()
        {
             DBDebitNoteExpiry dbcrdb = new DBDebitNoteExpiry();
             return dbcrdb.AddDetails(Id, AccountID , CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbAmountNet,
               CrdbDiscPer, CrdbDiscAmt, CrdbTotalAmount, CrdbRoundAmount, CreatedBy, CreatedDate, CreatedTime);
        }
        public int GetCurrentClosingStockFromMaster(string productID)
        {
            DataRow dr;
            int clstk = 0;
             DBDebitNoteExpiry dbcrdb = new DBDebitNoteExpiry();
             dr =  dbcrdb.GetCurrentClosingStockFromMaster(productID);
             if (dr != null && dr["ProdClosingStock"] != DBNull.Value)
                 clstk = Convert.ToInt32(dr["ProdClosingStock"].ToString());
            return clstk;
        }
        public bool AddProductDetails()
        {
            DBDebitNoteExpiry dbcrdbp = new DBDebitNoteExpiry();
            return dbcrdbp.AddDetailsProducts(Id, ProductID, Batchno, Quantity, SchemeQuanity, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, ReasonCode, VATPer, Amount, CrdbVouType, CrdbVouNo, CrdbVouDate, TradeRate, StockID, DetailId,DiscountPercentProduct,DiscountAmountProduct);
        }


        public bool SaveCreditorsNameID()
        {
            DBDebitNoteExpiry dbcrdbp = new DBDebitNoteExpiry();
            return dbcrdbp.SaveCreditorsNameID(CrdbId, StockID);
        }
        public string CheckForBatchMRPInStockTable()
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
            return sstk.UpdateDebitNoteStock(StockID,  Quantity*UOM, SchemeQuanity*UOM);

        }

        public bool UpdateDebitNoteStockInmasterProduct( int curClosingStock)
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateDebitNoteStockInmasterProduct(ProductID, Math.Max(0,curClosingStock-((Quantity+SchemeQuanity)*UOM)));
        }
        public DataTable ReadProductDetailsById(string Id)
        {

            DataTable dt = null;
            DBPatient _dbPatient = new DBPatient();
            dt = _dbPatient.ReadProductDetailsById(Id);
            return dt;


        }
        public DataTable ReadExpiredProductData(string FromDate, string ToDate) // [ansuman]
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBSsStock _dbStock = new DBSsStock();
                dt = _dbStock.ReadExpiredProductData(FromDate, ToDate);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }
        public DataTable ReadExpiredStockbtnDate(string FromDate, string Todate)
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBSsStock _dbStock = new DBSsStock();
                dt = _dbStock.ReadExpiredStockbtnDate(FromDate, Todate);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }
        public DataTable ReadExpiredStockForShelf(string FromDate, string ToDate, string mshelfID) // [ansuman]
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBSsStock _dbStock = new DBSsStock();
                dt = _dbStock.ReadExpiredStockForShelf(FromDate, ToDate, mshelfID);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }
        public DataTable ReadExpiredStockForMessage(string mdate)
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBSsStock _dbStock = new DBSsStock();
                dt = _dbStock.ReadExpiredStockForMessage(mdate);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }
        #endregion

        public DataTable ReadPurchaseDetailsExpiredStock(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBSsStock _dbStock = new DBSsStock();
                dt = _dbStock.ReadPurchaseDetailsExpiredStock(FromDate, ToDate);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }
    }
}
