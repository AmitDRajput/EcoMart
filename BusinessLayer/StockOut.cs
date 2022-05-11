using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class StockOut : BaseObject
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

        private string _CrdbId;
        private string _CrdbAccountId;
        private string _CrdbName;
        private string _CrdbAddress;
        private string _CrdbNarration;
        private string _CrdbVouType;
        private string _CrdbVouSeries;

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
        private string _StockID;

        private string _SetExpiryFirst;
        # endregion

        #region Constructors
        public StockOut()
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

            _CrdbId = "";
            _CrdbName = "";
            _CrdbAccountId = "";
            _CrdbAddress = "";
            _CrdbNarration = "";
            _CrdbVouType = FixAccounts.VoucherTypeForStockOut;
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
            _CrdbVouDate = "";
            _Particulars = "";
            _Amount = 0;
            _ClearVouNo = 0;
            _StockID = "";

            _SetExpiryFirst = "Y";

        }
        public override void DoValidate()
        {
            try
            {
                if (CrdbId == "")
                    ValidationMessages.Add("Please enter the Account Name.");
                if (CrdbAmountNet == 0)
                    ValidationMessages.Add("Invalid Amount");
                bool retValue = General.CheckDates(CrdbVouDate, CrdbVouDate);
                if (retValue == false)
                {
                    ValidationMessages.Add("Please Check Date...");
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
            DBStockOut dbStock = new DBStockOut();
            return dbStock.GetOverviewData(DbntType);
        }

        public DataTable GetOverviewDataStockOut(string fromDate, string toDate )
        {
            DBStockOut dbStock = new DBStockOut();
            return dbStock.GetOverviewDataStockOut(fromDate,toDate);
        }       

        public int GetAndUpdateStockOutNumber(string voucherseries)
        {
            int vouno = 0;
            DBGetVouNumbers dbno = new DBGetVouNumbers();
            vouno = dbno.GetStockOut(voucherseries);
            return vouno;
        }

        public DataTable ReadExpiredStockForStockOutExpiredProducts(string mdate)
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBSsStock _dbStock = new DBSsStock();
                dt = _dbStock.ReadExpiredStockForStockOutExpiredProducts(mdate);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dt;
        }
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
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
                    CrdbTotalAmount = CrdbAmount + CrdbVat5 + CrdbVat12point5 - CrdbDiscAmt;
                    CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }
          public DataRow ReadDetailsByVouNumber(int vouno)
        {
            
            DataRow drow = null;
            try
            {
               
                DBStockOut dbStock = new DBStockOut();
                drow = dbStock.ReadDetailsByVouNumber(vouno);
                if (drow != null)
                {
                    Id = drow["CRDBID"].ToString();
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
                    CrdbTotalAmount = CrdbAmount + CrdbVat5 + CrdbVat12point5 - CrdbDiscAmt;
                    CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                    
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return drow;
        }
        public DataTable ReadProductDetailsByID()
        {

            DataTable dt = new DataTable();
            try
            {
                dt = null;
                DBStockOut dbStock = new DBStockOut();
                dt = dbStock.ReadProductDetailsByID(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable ReadProductDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = null;
                DBProduct dbStock = new DBProduct();
                dt = dbStock.GetOverviewData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public bool AddDetails()
        {
            DBStockOut dbcrdb = new DBStockOut();
            return dbcrdb.AddDetails(Id, CrdbId, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbAmountNet,
               CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,CreatedBy,CreatedDate,CreatedTime);
        }

        public bool AddProductDetails()
        {
            DBStockOut dbcrdbp = new DBStockOut();
            return dbcrdbp.AddDetailsProducts(Id, StockID, ProductID, Batchno, Quantity, SchemeQuanity, PurchaseRate, MRP, SaleRate,TradeRate,
                Expiry, ExpiryDate, ReasonCode, VATPer, Amount, CrdbVouType, CrdbVouNo, CrdbVouDate, DiscountPercent, DiscountAmount, DetailId, SerialNumber);
        }

        public bool UpdateDetails()
        {
            DBStockOut dbcrdb = new DBStockOut();
            return dbcrdb.UpdateDetails(Id, CrdbId, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbAmountNet,
                CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBStockOut dbcrdb = new DBStockOut();
            return dbcrdb.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBCreditNoteStock dbcrdb = new DBCreditNoteStock();
            return dbcrdb.DeleteProductsByMasterID(Id);
        }

        public string CheckForStockIDInStockTable()
        {
            DBSsStock sstk = new DBSsStock();
            string ifrowfound = "N";
            try
            {
                DataRow drow = null;
             
                drow = sstk.GetRecordByStockID(StockID);
                if (drow != null)
                    ifrowfound = "Y";
                else
                    ifrowfound = "N";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return ifrowfound;

        }
        public bool UpdateIntblStock()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateStockOut(StockID, Quantity+SchemeQuanity);

        }

        public bool UpdateStockOutInMasterProduct()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateStockOutInmasterProduct(ProductID, Quantity+SchemeQuanity);
        }

        public bool UpdateIntblStockAdd()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateStockOutAddFromTemp(StockID, Quantity+SchemeQuanity);

        }
        public bool UpdateStockOutInMasterProductAddFromTemp()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateStockOutInmasterProductAddFromTemp(ProductID, Quantity+SchemeQuanity);
        } 

        #endregion

        public void GetLastRecord()
        {
            DataRow dr;
            try
            {
                DBStockOut dbs = new DBStockOut();
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
                DBStockOut dbs = new DBStockOut();
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
                DBStockOut dbs = new DBStockOut();
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
