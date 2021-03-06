using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using System.Globalization;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class OPStock : BaseObject
    {
        #region Declaration
        private string _EntryDate;
        private string _VoucherSeries;
        private string _VoucherType;
        private int _VoucherNumber;
        private string _VoucherDate;
        private string _PurchaseBillNumber;
        private string _AccountID;

        private string _DueDate;
        private string _Narration;
        private double _VAT5Percent;
        private double _VATOPercent;
        private double _VAT12point5Percent;
        private string _ProductID;
        private string _Batchno;
        private double _Mrp;
        private double _PurchaseRate;
        private double _SaleRate;
        private double _TradeRate;
        private int _Quantity;
        private int _SchemeQuantity;
        private int _ReplacementQuantity;
        private int _BeginningStock;
        private string _Expiry;
        private string _ExpiryDate;
        private double _ItemDiscountPercent;
        private double _AmountItemDiscount;
        private double _SchemeDiscountPercent;
        private double _AmountSchemeDiscount;
        private double _SplDiscountPercent;
        private double _AmountSplDiscount;
        private double _PurchaseVATPercent;
        private double _ProductVATPercent;
        private double _AmountPurchseVAT;
        private double _CSTPercent;
        private double _AmountCST;
        private double _Amount;
        private string _IfMRPInclusiveOfVAT;
        private string _IfTradeRateInclusiveOfVAT;
        private string _ShelfCode;
        private string _ShelfID;
        private string _StockID;


        private double _AmountS;
        private double _AmountNetS;    
        private double _AmountVAT5PercentS;
        private double _AmountVAT0PercentS;    
        private double _AmountVAT12point5PercentS;
       
       
       
   
        #endregion
        #region Properties
      
        public string StockID
        {
            get { return _StockID ; }
            set { _StockID = value; }
        }

        public string ShelfCode
        {
            get { return _ShelfCode; }
            set { _ShelfCode = value; }
        }
        public string ShelfID
        {
            get { return _ShelfID; }
            set { _ShelfID = value; }
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
        public int ReplacementQuantity
        {
            get { return _ReplacementQuantity; }
            set { _ReplacementQuantity = value; }
        }
        public int BeginningStock
        {
            get { return _BeginningStock; }
            set { _BeginningStock = value; }
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

        public string EntryDate
        {
            get { return _EntryDate; }
            set { _EntryDate = value; }
        }
        public string VoucherSeries
        {
            get { return _VoucherSeries; }
            set { _VoucherSeries = value; }
        }
        public string VoucherType
        {
            get { return _VoucherType; }
            set { _VoucherType = value; }
        }
        public int VoucherNumber
        {
            get { return _VoucherNumber; }
            set { _VoucherNumber = value; }
        }
        public string VoucherDate
        {
            get { return _VoucherDate; }
            set { _VoucherDate = value; }
        }
        public string PurchaseBillNumber
        {
            get { return _PurchaseBillNumber; }
            set { _PurchaseBillNumber = value; }
        }
        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }
        public string DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        public string Narration
        {
            get { return _Narration; }
            set { _Narration = value; }
        }

        public double VATOPercent
        {
            get { return _VATOPercent; }
            set { _VATOPercent = value; }
        }
        public double VAT5Percent
        {
            get { return _VAT5Percent; }
            set { _VAT5Percent = value; }
        }
        public double VAT12point5Percent
        {
            get { return _VAT12point5Percent; }
            set { _VAT12point5Percent = value; }
        }

        public double ItemDiscountPercent
        {
            get { return _ItemDiscountPercent; }
            set { _ItemDiscountPercent = value; }
        }

        public double AmountItemDiscount
        {
            get { return _AmountItemDiscount; }
            set { _AmountItemDiscount = value; }
        }
        public double SplDiscountPercent
        {
            get { return _SplDiscountPercent; }
            set { _SplDiscountPercent = value; }
        }

        public double AmountSplDiscount
        {
            get { return _AmountSplDiscount; }
            set { _AmountSplDiscount = value; }
        }
        public double SchemeDiscountPercent
        {
            get { return _SchemeDiscountPercent; }
            set { _SchemeDiscountPercent = value; }
        }
        public double AmountSchemeDiscount
        {
            get { return _AmountSchemeDiscount; }
            set { _AmountSchemeDiscount = value; }
        }
        public double PurchaseVATPercent
        {
            get { return _PurchaseVATPercent; }
            set { _PurchaseVATPercent = value; }
        }
        public double ProductVATPercent
        {
            get { return _ProductVATPercent; }
            set { _ProductVATPercent = value; }
        }
        public double AmountPurchaseVAT
        {
            get { return _AmountPurchseVAT; }
            set { _AmountPurchseVAT = value; }
        }
        public double CSTPercent
        {
            get { return _CSTPercent; }
            set { _CSTPercent = value; }
        }
        public double AmountCST
        {
            get { return _AmountCST; }
            set { _AmountCST = value; }
        }
        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string IfMRPInclusiveOfVAT
        {
            get { return _IfMRPInclusiveOfVAT; }
            set { _IfMRPInclusiveOfVAT = value; }
        }
        public string IfTradeRateInclusiveOfVAT
        {
            get { return _IfTradeRateInclusiveOfVAT; }
            set { _IfTradeRateInclusiveOfVAT = value; }
        }

        public double AmountS
        {
            get { return _AmountS; }
            set { _AmountS = value; }
        }
        public double AmountNetS
        {
            get { return _AmountNetS; }
            set { _AmountNetS = value; }
        }     
   
        public double AmountVAT0PercentS
        {
            get { return _AmountVAT0PercentS; }
            set { _AmountVAT0PercentS = value; }
        }
        public double AmountVAT5PercentS
        {
            get { return _AmountVAT5PercentS; }
            set { _AmountVAT5PercentS = value; }
        }
    
        public double AmountVAT12point5PercentS
        {
            get { return _AmountVAT12point5PercentS; }
            set { _AmountVAT12point5PercentS = value; }
        }    
      
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _EntryDate = "";
                _VoucherSeries = "";
                _VoucherType = FixAccounts.VoucherTypeForOpeningStock;
                _VoucherNumber = 0;
                _VoucherDate = "";
                _PurchaseBillNumber = "";
                _AccountID = "";

                _Batchno = "";
                _Expiry = "";
                _ExpiryDate = "";
                _Mrp = 0;
                _ProductID = "";
                _PurchaseRate = 0;
                _Quantity = 0;
                _SaleRate = 0;
                _SchemeQuantity = 0;
                _ReplacementQuantity = 0;
                _BeginningStock = 0;
                _TradeRate = 0;
                _ShelfCode = "";
                _ShelfID = "";
                _StockID = "";

                _ItemDiscountPercent = 0;
                _AmountItemDiscount = 0;
                _SchemeDiscountPercent = 0;
                _AmountSchemeDiscount = 0;
                _PurchaseVATPercent = 0; ;
                _ProductVATPercent = 0; ;
                _AmountPurchseVAT = 0; ;
                _CSTPercent = 0;
                _AmountCST = 0;
                _Amount = 0;
                _IfMRPInclusiveOfVAT = "";
                _IfTradeRateInclusiveOfVAT = "";


                _AmountNetS = 0;            
                _AmountVAT0PercentS = 0;
                _AmountVAT5PercentS = 0;            
                _AmountVAT12point5PercentS = 0;
             

              
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
                if (Amount == 0)
                    ValidationMessages.Add("Can Not Save");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        

        }

        #endregion

        #region Public Methods
        public  void ReadDetailsByID()
        {

            DataRow drow = null;
            try
            {
                DBOPStock dbData = new DBOPStock();
                drow = dbData.ReadDetailsByID(Id);
                if (drow != null)
                {


                    if (drow["VoucherSeries"] != DBNull.Value)
                        VoucherSeries = Convert.ToString(drow["VoucherSeries"]);
                    if (drow["VoucherType"] != DBNull.Value)
                        VoucherType = Convert.ToString(drow["VoucherType"]);
                    if (drow["VoucherNumber"] != DBNull.Value)
                        VoucherNumber = Convert.ToInt32(drow["VoucherNumber"]);
                    if (drow["VoucherDate"] != DBNull.Value)
                        VoucherDate = Convert.ToString(drow["VoucherDate"]);
                    if (drow["AmountNet"] != DBNull.Value)
                        AmountNetS = Convert.ToDouble(drow["AmountNet"]);
                 //   if (drow["AmountVAT5Percent"] != DBNull.Value)
                 //       VAT5Percent = Convert.ToDouble(drow["AmountVAT5Percent"]);
                 //   if (drow["AmountVAT12point5Percent"] != DBNull.Value)
                //        VAT12point5Percent = Convert.ToDouble(drow["AmountVAT12point5Percent"]);
                 //   if (drow["AmountVATOPercent"] != DBNull.Value)
                  //      VATOPercent = Convert.ToDouble(drow["AmountVATOPercent"]);
                  
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }       
            
        }

        public void ReadDetailsByVoucherNumber()
        {

            DataRow drow = null;
            try
            {
                DBOPStock dbData = new DBOPStock();
                drow = dbData.ReadDetailsByVoucherNumber(VoucherNumber);
                if (drow != null)
                {

                    Id = drow["MasterID"].ToString();
                    if (drow["VoucherSeries"] != DBNull.Value)
                        VoucherSeries = Convert.ToString(drow["VoucherSeries"]);
                    if (drow["VoucherType"] != DBNull.Value)
                        VoucherType = Convert.ToString(drow["VoucherType"]);
                    if (drow["VoucherNumber"] != DBNull.Value)
                        VoucherNumber = Convert.ToInt32(drow["VoucherNumber"]);
                    if (drow["VoucherDate"] != DBNull.Value)
                        VoucherDate = Convert.ToString(drow["VoucherDate"]);
                    if (drow["AmountNet"] != DBNull.Value)
                        AmountNetS = Convert.ToDouble(drow["AmountNet"]);
                    if (drow["AmountVAT5Percent"] != DBNull.Value)
                        VAT5Percent = Convert.ToDouble(drow["AmountVAT5Percent"]);
                    if (drow["AmountVAT12point5Percent"] != DBNull.Value)
                        VAT12point5Percent = Convert.ToDouble(drow["AmountVAT12point5Percent"]);
                    if (drow["AmountVATOPercent"] != DBNull.Value)
                        VATOPercent = Convert.ToDouble(drow["AmountVATOPercent"]);
                
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public DataTable ReadProductDetailsByID()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = null;
                DBOPStock dbp = new DBOPStock();
                dt = dbp.ReadProductDetailsByID(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        

            return dt;
        }

        public int GetAndUpdateOpeningStockNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetOpeningStock(voucherseries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return vouno;
        }

        public bool AddDetails()
        {
           DBOPStock dbcrdb = new DBOPStock();
            return dbcrdb.AddDetails(Id, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
           Amount, AmountVAT5PercentS,AmountVAT12point5PercentS,AmountVAT0PercentS,CreatedBy,CreatedDate,CreatedTime);
        }
        public bool AddProductDetailsSS()
        {
            DBOPStock dbpur = new DBOPStock();
            return dbpur.AddDetailsProductsSS(Id, ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercent,
                 PurchaseVATPercent, ProductVATPercent, AmountPurchaseVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT,
                IfTradeRateInclusiveOfVAT, Amount, AmountSplDiscount, SplDiscountPercent, StockID, DetailId, SerialNumber);
        }
        public bool AddProductDetailsInStockTable()
        {
            DBOPStock dbpur = new DBOPStock();
            return dbpur.AddProductDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, ReplacementQuantity, PurchaseVATPercent,
                ProductVATPercent, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount, AccountID, PurchaseBillNumber,
                VoucherType, VoucherNumber, VoucherDate,StockID,BeginningStock, CreatedBy, CreatedDate,CreatedTime);
        }

        public bool CheckForBatchMRPInStockTable()
        {
            DBSsStock sstk = new DBSsStock();
            DataRow drow = null;
            drow = sstk.GetRecordByProductBatchMRP(ProductID, Batchno, MRP);
            if (drow == null)
                return false;
            else
            {
                if (drow["StockID"] != DBNull.Value)
                    StockID = drow["StockID"].ToString();
                return true;
            }
        }

        public DataTable GetStockForCheck()
        {
            DBSsStock cstk = new DBSsStock();
            DataTable stktbl = new DataTable();
            stktbl = cstk.GetStockforCheck();
            return stktbl;
        }
       
        public bool UpdateOPStockIntblStock()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateOPStockStock(StockID, Quantity,CreatedBy ,CreatedDate,CreatedTime);

        }   

        public bool UpdateOpeningStockInMasterProduct()
        {
            int Closingstock = GetClosingStock();
            int OpeningStock = GetOpeningStock();
            Closingstock += Quantity;
            OpeningStock += Quantity;
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateOpeningStockInmasterProduct(ProductID, Batchno, MRP, Closingstock,OpeningStock);
        }
        public int GetClosingStock()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetClosingStock(ProductID);
        }
        public int GetOpeningStock()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetOpeningStock(ProductID);
        }
        public bool UpdateLastPurhcaseDataInMasterProduct()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdatePurchaseDataInmasterProduct(ProductID, PurchaseBillNumber, VoucherDate, AccountID, VoucherType,
                VoucherNumber, PurchaseRate, TradeRate, SaleRate, MRP, PurchaseVATPercent, CSTPercent, AmountCST, SchemeDiscountPercent,
                AmountSchemeDiscount, ItemDiscountPercent, Expiry, ExpiryDate, Batchno,ShelfID, StockID,0);


        }

        public bool UpdatePurchaseStockInmasterProductReduceFromTemp()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdatePurchaseStockInmasterProductReduceFromTemp(ProductID, Quantity + SchemeQuanity + ReplacementQuantity);
        }
       

        #endregion

        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBOPStock dbPur = new DBOPStock();
            return dbPur.GetOverviewData();
        }

        public DataTable GetPurchase()
        {
            DBOPStock dbPur = new DBOPStock();
            return dbPur.GetOpstock();
        }

        public DataTable GetProductPurchased(string purchaseID)
        {
            DBOPStock dbPur = new DBOPStock();
            return dbPur.GetProductPurchased(purchaseID);
        }
        public DataTable GetOverviewDataForProductList(string ProductID)
        {
            DBOPStock dbOPS = new DBOPStock();
            return dbOPS.GetOverviewDataForProductList(ProductID);
        }
        #endregion

    }
}
