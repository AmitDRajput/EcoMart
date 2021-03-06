using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    class SsStock : BaseObject
    {
        #region Declaration
        private string _ProductId;
        private string _BatchNumber;
        private string _Expiry;
        private string _ExpiryDate;
        private double _TradeRate;
        private double _PurchaseRate;
        private double _MRP;
        private double _SaleRate;
        private int _OpeningStock;
        private int _ClosingStock;
        private int _PurchaseStock;
        private int _TransferInStock;
        private int _CreditNoteStock;
        private int _SaleStock;
        private int _TransferOutStock;
        private int _DebitNoteStock;
        private int _PurchaseSchemeStock;
        private int _PurchaseReplacementStock;
        private int _SaleSchemeStock;
        private string _IfRateCorrection;
        private double _ProductVATPercent;
        private double _PurchaseVATPercent;
        private double _ProdCST;
        private string _CompanyId;
        private string _LastPurchaseAccountId;
        private string _LastPurchasePartyShortName;
        private string _LastPurchaseBillNumber;
        private string _LastPurchaseDate;
        private string _LastPurchaseVoucherType;
        private int _LastPurchaseVoucherNumber;
        private string _ScanCode;   
        private string _CreatedUserId;
        private string _ModifyDate;
        private string _ModifyUserId;
        private DateTime _Today;
        private string _MDate;    
        #endregion

        #region Properties


        public string ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }


        public string BatchNumber
        {
            get { return _BatchNumber; }
            set { _BatchNumber = value; }
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


        public double TradeRate
        {
            get { return _TradeRate; }
            set { _TradeRate = value; }
        }


        public double PurchaseRate
        {
            get { return _PurchaseRate; }
            set { _PurchaseRate = value; }
        }


        public double MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }


        public double SaleRate
        {
            get { return _SaleRate; }
            set { _SaleRate = value; }
        }


        public int OpeningStock
        {
            get { return _OpeningStock; }
            set { _OpeningStock = value; }
        }


        public int ClosingStock
        {
            get { return _ClosingStock; }
            set { _ClosingStock = value; }
        }


        public int PurchaseStock
        {
            get { return _PurchaseStock; }
            set { _PurchaseStock = value; }
        }


        public int TransferInStock
        {
            get { return _TransferInStock; }
            set { _TransferInStock = value; }
        }


        public int CreditNoteStock
        {
            get { return _CreditNoteStock; }
            set { _CreditNoteStock = value; }
        }


        public int SaleStock
        {
            get { return _SaleStock; }
            set { _SaleStock = value; }
        }


        public int TransferOutStock
        {
            get { return _TransferOutStock; }
            set { _TransferOutStock = value; }
        }


        public int DebitNoteStock
        {
            get { return _DebitNoteStock; }
            set { _DebitNoteStock = value; }
        }


        public int PurchaseSchemeStock
        {
            get { return _PurchaseSchemeStock; }
            set { _PurchaseSchemeStock = value; }
        }


        public int SaleSchemeStock
        {
            get { return _SaleSchemeStock; }
            set { _SaleSchemeStock = value; }
        }


        public string IfRateCorrection
        {
            get { return _IfRateCorrection; }
            set { _IfRateCorrection = value; }
        }


        public double ProductVATPercent
        {
            get { return _ProductVATPercent; }
            set { _ProductVATPercent = value; }
        }


        public double PurchaseVATPercent
        {
            get { return _PurchaseVATPercent; }
            set { _PurchaseVATPercent = value; }
        }


        public double ProdCST
        {
            get { return _ProdCST; }
            set { _ProdCST = value; }
        }


        public string CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }


        public string LastPurchaseAccountId
        {
            get { return _LastPurchaseAccountId; }
            set { _LastPurchaseAccountId = value; }
        }

        public string LastPurchasePartyShortName
        {
            get { return this._LastPurchasePartyShortName; }
            set { this._LastPurchasePartyShortName = value; }
        }


        public string LastPurchaseBillNumber
        {
            get { return _LastPurchaseBillNumber; }
            set { _LastPurchaseBillNumber = value; }
        }


        public string LastPurchaseDate
        {
            get { return _LastPurchaseDate; }
            set { _LastPurchaseDate = value; }
        }

        public string LastPurchaseVoucherType
        {
            get { return _LastPurchaseVoucherType; }
            set { _LastPurchaseVoucherType = value; }
        }


        public int LastPurchaseVoucherNumber
        {
            get { return _LastPurchaseVoucherNumber; }
            set { _LastPurchaseVoucherNumber = value; }
        }

        public string ScanCode
        {
            get { return _ScanCode; }
            set { _ScanCode = value; }
        }

        public string CreatedUserId
        {
            get { return _CreatedUserId; }
            set { _CreatedUserId = value; }
        }


        public string ModifyDate
        {
            get { return _ModifyDate; }
            set { _ModifyDate = value; }
        }


        public string ModifyUserId
        {
            get { return _ModifyUserId; }
            set { _ModifyUserId = value; }
        }

        public int PurchaseReplacementStock
        {
            get { return this._PurchaseReplacementStock; }
            set { this._PurchaseReplacementStock = value; }
        }

        public DateTime Today
        {
            get { return _Today; }
            set { _Today = value; }
        }

        public string MDate
        {
            get { return _MDate; }
            set { _MDate = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _ProductId = "";
                _BatchNumber = "";
                _Expiry = "";
                _ExpiryDate = "";
                _TradeRate = 0;
                _PurchaseRate = 0;
                _MRP = 0;
                _SaleRate = 0;
                _OpeningStock = 0;
                _ClosingStock = 0;
                _PurchaseStock = 0;
                _TransferInStock = 0;
                _CreditNoteStock = 0;
                _SaleStock = 0;
                _TransferOutStock = 0;
                _DebitNoteStock = 0;
                _PurchaseSchemeStock = 0;
                _SaleSchemeStock = 0;
                _IfRateCorrection = "";
                _ProductVATPercent = 0;
                _PurchaseVATPercent = 0;
                _ProdCST = 0;
                _CompanyId = "";
                _LastPurchaseAccountId = "";
                _LastPurchaseBillNumber = "";
                _LastPurchaseDate = "";
                _LastPurchaseVoucherNumber = 0;
                _LastPurchaseVoucherType = "";
                _LastPurchasePartyShortName = "";
                _ScanCode = "";       
                _CreatedUserId = "";
                _ModifyDate = "";
                _ModifyUserId = "";
                Today = DateTime.Now;
                _MDate = "";
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
                if (ProductId == "")
                    ValidationMessages.Add("Please enter the  ProductId");
                if (BatchNumber == "")
                    ValidationMessages.Add("Please enter the  BatchNumber");
                if (Expiry == "")
                    ValidationMessages.Add("Please enter the  Expiry");
                if (ExpiryDate == "")
                    ValidationMessages.Add("Please enter the  ExpiryDate");
                if (TradeRate == 0)
                    ValidationMessages.Add("Please enter the  TradeRate");
                if (PurchaseRate == 0)
                    ValidationMessages.Add("Please enter the  PurchaseRate");
                if (MRP == 0)
                    ValidationMessages.Add("Please enter the  MRP");
                if (SaleRate == 0)
                    ValidationMessages.Add("Please enter the  SaleRate");
                if (OpeningStock == 0)
                    ValidationMessages.Add("Please enter the  OpeningStock");
                if (ClosingStock == 0)
                    ValidationMessages.Add("Please enter the  ClosingStock");
                if (PurchaseStock == 0)
                    ValidationMessages.Add("Please enter the  PurchaseStock");
                if (TransferInStock == 0)
                    ValidationMessages.Add("Please enter the  TransferInStock");
                if (CreditNoteStock == 0)
                    ValidationMessages.Add("Please enter the  CreditNoteStock");
                if (SaleStock == 0)
                    ValidationMessages.Add("Please enter the  SaleStock");
                if (TransferOutStock == 0)
                    ValidationMessages.Add("Please enter the  TransferOutStock");
                if (DebitNoteStock == 0)
                    ValidationMessages.Add("Please enter the  DebitNoteStock");
                if (PurchaseSchemeStock == 0)
                    ValidationMessages.Add("Please enter the  PurchaseSchemeStock");
                if (SaleSchemeStock == 0)
                    ValidationMessages.Add("Please enter the  SaleSchemeStock");
                if (IfRateCorrection == "")
                    ValidationMessages.Add("Please enter the  IfRateCorrection");
                if (ProductVATPercent == 0)
                    ValidationMessages.Add("Please enter the  ProductVATPercent");
                if (PurchaseVATPercent == 0)
                    ValidationMessages.Add("Please enter the  PurchaseVATPercent");
                if (ProdCST == 0)
                    ValidationMessages.Add("Please enter the  ProdCST");
                if (CompanyId == "")
                    ValidationMessages.Add("Please enter the  CompanyId");
                if (LastPurchaseAccountId == "")
                    ValidationMessages.Add("Please enter the  LastPurchaseAccountId");
                if (LastPurchaseBillNumber == "")
                    ValidationMessages.Add("Please enter the  LastPurchaseBillNumber");
                if (LastPurchaseDate == "")
                    ValidationMessages.Add("Please enter the  LastPurchaseDate");
                if (LastPurchaseVoucherNumber == 0)
                    ValidationMessages.Add("Please enter the  LastPurchaseVoucherNumber");
                if (ScanCode == "")
                    ValidationMessages.Add("Please enter the  ScanCode");           
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }

        #endregion

        #region Public Methods

        //public DataTable GetStockByProductID(string productID)
        //{
        //    DBSsStock dbData = new DBSsStock();
        //    return dbData.GetStockByProductID(productID);
        //}
        public DataTable GetStockByProductIDForPurchase(string productID)
        {
            DBSsStock dbData = new DBSsStock();
            return dbData.GetStockByProductIDForPurchase(productID);
        }
        public DataTable GetStockByProductIDForDistributorSale(string productID)
        {
            DBSsStock dbData = new DBSsStock();
            return dbData.GetStockByProductIDForDistributorSale(productID);
        }
        public DataTable GetStockByProductIDForDBCRNote(string productID)
        {
            DBSsStock dbData = new DBSsStock();
            return dbData.GetStockByProductIDForDBCRNote(productID);
        }

        //public DataTable GetStockByProductIDForSale(string productID)
        //{
        //    DBSsStock dbData = new DBSsStock();
        //    return dbData.GetStockByProductIDForSale(productID);
        //}

        public DataTable  GetStockByProductIDForFill(string productID )
        {
            DBSsStock dbData = new DBSsStock();
            return dbData.GetStockByProductIDForFill(productID);
        }

        //public DataTable GetValidBatchesByProductID(string productID)
        //{
        //    DBSsStock dbData = new DBSsStock();
        //    return dbData.GetValidBatchesByProductID(productID);
        //}

        //public bool ReadDetailsByID()
        //{
        //    bool retValue = false;
        //    try
        //    {
        //        DataRow drow = null;
        //        DBSsStock dbData = new DBSsStock();
        //        drow = dbData.ReadDetailsByID(Id);
        //        if (drow != null)
        //        {
        //            if (drow["ProductId"] != DBNull.Value)
        //                ProductId = Convert.ToString(drow["ProductId"]);
        //            if (drow["BatchNumber"] != DBNull.Value)
        //                BatchNumber = Convert.ToString(drow["BatchNumber"]);
        //            if (drow["Expiry"] != DBNull.Value)
        //                Expiry = Convert.ToString(drow["Expiry"]);
        //            if (drow["ExpiryDate"] != DBNull.Value)
        //                ExpiryDate = Convert.ToString(drow["ExpiryDate"]);
        //            if (drow["TradeRate"] != DBNull.Value)
        //                TradeRate = Convert.ToDouble(drow["TradeRate"]);
        //            if (drow["PurchaseRate"] != DBNull.Value)
        //                PurchaseRate = Convert.ToDouble(drow["PurchaseRate"]);
        //            if (drow["MRP"] != DBNull.Value)
        //                MRP = Convert.ToDouble(drow["MRP"]);
        //            if (drow["SaleRate"] != DBNull.Value)
        //                SaleRate = Convert.ToDouble(drow["SaleRate"]);
        //            if (drow["OpeningStock"] != DBNull.Value)
        //                OpeningStock = Convert.ToInt32(drow["OpeningStock"]);
        //            if (drow["ClosingStock"] != DBNull.Value)
        //                ClosingStock = Convert.ToInt32(drow["ClosingStock"]);
        //            if (drow["PurchaseStock"] != DBNull.Value)
        //                PurchaseStock = Convert.ToInt32(drow["PurchaseStock"]);
        //            if (drow["TransferInStock"] != DBNull.Value)
        //                TransferInStock = Convert.ToInt32(drow["TransferInStock"]);
        //            if (drow["CreditNoteStock"] != DBNull.Value)
        //                CreditNoteStock = Convert.ToInt32(drow["CreditNoteStock"]);
        //            if (drow["SaleStock"] != DBNull.Value)
        //                SaleStock = Convert.ToInt32(drow["SaleStock"]);
        //            if (drow["TransferOutStock"] != DBNull.Value)
        //                TransferOutStock = Convert.ToInt32(drow["TransferOutStock"]);
        //            if (drow["DebitNoteStock"] != DBNull.Value)
        //                DebitNoteStock = Convert.ToInt32(drow["DebitNoteStock"]);
        //            if (drow["PurchaseSchemeStock"] != DBNull.Value)
        //                PurchaseSchemeStock = Convert.ToInt32(drow["PurchaseSchemeStock"]);
        //            if (drow["PurchaseReplacementStock"] != DBNull.Value)
        //                PurchaseReplacementStock = Convert.ToInt32(drow["PurchaseReplacementStock"]);
        //            if (drow["SaleSchemeStock"] != DBNull.Value)
        //                SaleSchemeStock = Convert.ToInt32(drow["SaleSchemeStock"]);
        //            if (drow["IfRateCorrection"] != DBNull.Value)
        //                IfRateCorrection = Convert.ToString(drow["IfRateCorrection"]);
        //            if (drow["ProductVATPercent"] != DBNull.Value)
        //                ProductVATPercent = Convert.ToDouble(drow["ProductVATPercent"]);
        //            if (drow["PurchaseVATPercent"] != DBNull.Value)
        //                PurchaseVATPercent = Convert.ToDouble(drow["PurchaseVATPercent"]);
        //            if (drow["ProdCST"] != DBNull.Value)
        //                ProdCST = Convert.ToDouble(drow["ProdCST"]);
        //            if (drow["CompanyId"] != DBNull.Value)
        //                CompanyId = Convert.ToString(drow["CompanyId"]);
        //            if (drow["LastPurchaseAccountId"] != DBNull.Value)
        //                LastPurchaseAccountId = Convert.ToString(drow["LastPurchaseAccountId"]);
        //            if (drow["LastPurchaseBillNumber"] != DBNull.Value)
        //                LastPurchaseBillNumber = Convert.ToString(drow["LastPurchaseBillNumber"]);
        //            if (drow["LastPurchaseDate"] != DBNull.Value)
        //                LastPurchaseDate = Convert.ToString(drow["LastPurchaseDate"]);
        //            if (drow["LastPurchaseVoucherNumber"] != DBNull.Value)
        //                LastPurchaseVoucherNumber = Convert.ToInt32(drow["LastPurchaseVoucherNumber"]);
        //            if (drow["ScanCode"] != DBNull.Value)
        //                ScanCode = Convert.ToString(drow["ScanCode"]);
        //            if (drow["CreatedDate"] != DBNull.Value)
        //                CreatedDate = Convert.ToString(drow["CreatedDate"]);
        //            if (drow["CreatedUserId"] != DBNull.Value)
        //                CreatedUserId = Convert.ToString(drow["CreatedUserId"]);
        //            if (drow["ModifiedDate"] != DBNull.Value)
        //                ModifyDate = Convert.ToString(drow["ModifiedDate"]);
        //            if (drow["ModifiedUserId"] != DBNull.Value)
        //                ModifyUserId = Convert.ToString(drow["ModifiedUserId"]);
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }        
        //    return retValue;
        //}

        //public bool AddDetails()
        //{
        //    DBStock dbData = new DBStock();
        //    try
        //    {
        //        DateTime expDt;
        //        if (DateTime.TryParse(ExpiryDate, out expDt))
        //        {
        //            ExpiryDate = expDt.ToString("yyyy/dd/MM").Replace("/", "");
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }        
        //    return dbData.AddDetails(ProductId, BatchNumber, Expiry, ExpiryDate, TradeRate, PurchaseRate, MRP, SaleRate, OpeningStock, ClosingStock, PurchaseStock, TransferInStock, CreditNoteStock, SaleStock, TransferOutStock, DebitNoteStock, PurchaseSchemeStock, PurchaseReplacementStock, SaleSchemeStock, IfRateCorrection, ProductVATPercent, PurchaseVATPercent, ProdCST, CompanyId, LastPurchaseAccountId, LastPurchasePartyShortName, LastPurchaseBillNumber, LastPurchaseDate, LastPurchaseVoucherNumber, LastPurchaseVoucherType, ScanCode, CreatedDate, CreatedUserId, ModifyDate, ModifyUserId);
        //}

        //public bool UpdateDetails()
        //{
        //    DBStock dbData = new DBStock();
        //    return dbData.UpdateDetails(ProductId, BatchNumber, Expiry, ExpiryDate, TradeRate, PurchaseRate, MRP, SaleRate, OpeningStock, ClosingStock, PurchaseStock, TransferInStock, CreditNoteStock, SaleStock, TransferOutStock, DebitNoteStock, PurchaseSchemeStock, PurchaseReplacementStock, SaleSchemeStock, IfRateCorrection, ProductVATPercent, PurchaseVATPercent, ProdCST, CompanyId, LastPurchaseAccountId, LastPurchasePartyShortName, LastPurchaseBillNumber, LastPurchaseDate, LastPurchaseVoucherNumber, LastPurchaseVoucherType, ScanCode, CreatedDate, CreatedUserId, ModifyDate, ModifyUserId);
        //}

        //public bool DeleteDetails()
        //{
        //    DBStock dbData = new DBStock();
        //    return dbData.DeleteDetails(Id);
        //}

        //public bool GetStockByProductIDAndBatchID(string productID, string batchID)
        //{
        //    bool retValue = false;
        //    try
        //    {
        //        DataRow drow = null;
        //        DBStock dbData = new DBStock();
        //        drow = dbData.GetStockByProductIDAndBatchNumber(productID, batchID);
        //        if (drow != null)
        //        {
        //            if (drow["ProductId"] != DBNull.Value)
        //                ProductId = Convert.ToString(drow["ProductId"]);
        //            if (drow["BatchNumber"] != DBNull.Value)
        //                BatchNumber = Convert.ToString(drow["BatchNumber"]);
        //            if (drow["Expiry"] != DBNull.Value)
        //                Expiry = Convert.ToString(drow["Expiry"]);
        //            if (drow["ExpiryDate"] != DBNull.Value)
        //                ExpiryDate = Convert.ToString(drow["ExpiryDate"]);
        //            if (drow["TradeRate"] != DBNull.Value)
        //                TradeRate = Convert.ToDouble(drow["TradeRate"]);
        //            if (drow["PurchaseRate"] != DBNull.Value)
        //                PurchaseRate = Convert.ToDouble(drow["PurchaseRate"]);
        //            if (drow["MRP"] != DBNull.Value)
        //                MRP = Convert.ToDouble(drow["MRP"]);
        //            if (drow["SaleRate"] != DBNull.Value)
        //                SaleRate = Convert.ToDouble(drow["SaleRate"]);
        //            if (drow["OpeningStock"] != DBNull.Value)
        //                OpeningStock = Convert.ToInt32(drow["OpeningStock"]);
        //            if (drow["ClosingStock"] != DBNull.Value)
        //                ClosingStock = Convert.ToInt32(drow["ClosingStock"]);
        //            if (drow["PurchaseStock"] != DBNull.Value)
        //                PurchaseStock = Convert.ToInt32(drow["PurchaseStock"]);
        //            if (drow["TransferInStock"] != DBNull.Value)
        //                TransferInStock = Convert.ToInt32(drow["TransferInStock"]);
        //            if (drow["CreditNoteStock"] != DBNull.Value)
        //                CreditNoteStock = Convert.ToInt32(drow["CreditNoteStock"]);
        //            if (drow["SaleStock"] != DBNull.Value)
        //                SaleStock = Convert.ToInt32(drow["SaleStock"]);
        //            if (drow["TransferOutStock"] != DBNull.Value)
        //                TransferOutStock = Convert.ToInt32(drow["TransferOutStock"]);
        //            if (drow["DebitNoteStock"] != DBNull.Value)
        //                DebitNoteStock = Convert.ToInt32(drow["DebitNoteStock"]);
        //            if (drow["PurchaseSchemeStock"] != DBNull.Value)
        //                PurchaseSchemeStock = Convert.ToInt32(drow["PurchaseSchemeStock"]);
        //            if (drow["PurchaseReplacementStock"] != DBNull.Value)
        //                PurchaseReplacementStock = Convert.ToInt32(drow["PurchaseReplacementStock"]);
        //            if (drow["SaleSchemeStock"] != DBNull.Value)
        //                SaleSchemeStock = Convert.ToInt32(drow["SaleSchemeStock"]);
        //            if (drow["IfRateCorrection"] != DBNull.Value)
        //                IfRateCorrection = Convert.ToString(drow["IfRateCorrection"]);
        //            if (drow["ProductVATPercent"] != DBNull.Value)
        //                ProductVATPercent = Convert.ToDouble(drow["ProductVATPercent"]);
        //            if (drow["PurchaseVATPercent"] != DBNull.Value)
        //                PurchaseVATPercent = Convert.ToDouble(drow["PurchaseVATPercent"]);
        //            if (drow["ProdCST"] != DBNull.Value)
        //                ProdCST = Convert.ToDouble(drow["ProdCST"]);
        //            if (drow["CompanyId"] != DBNull.Value)
        //                CompanyId = Convert.ToString(drow["CompanyId"]);
        //            if (drow["LastPurchaseAccountId"] != DBNull.Value)
        //                LastPurchaseAccountId = Convert.ToString(drow["LastPurchaseAccountId"]);
        //            if (drow["LastPurchasePartyShortName"] != DBNull.Value)
        //                LastPurchasePartyShortName = Convert.ToString("LastPurchasePartyShortName");
        //            if (drow["LastPurchaseBillNumber"] != DBNull.Value)
        //                LastPurchaseBillNumber = Convert.ToString(drow["LastPurchaseBillNumber"]);
        //            if (drow["LastPurchaseDate"] != DBNull.Value)
        //                LastPurchaseDate = Convert.ToString(drow["LastPurchaseDate"]);
        //            if (drow["LastPurchaseVoucherNumber"] != DBNull.Value)
        //                LastPurchaseVoucherNumber = Convert.ToInt32(drow["LastPurchaseVoucherNumber"]);
        //            if (drow["ScanCode"] != DBNull.Value)
        //                ScanCode = Convert.ToString(drow["ScanCode"]);
        //            if (drow["CreatedDate"] != DBNull.Value)
        //                CreatedDate = Convert.ToString(drow["CreatedDate"]);
        //            if (drow["CreatedUserId"] != DBNull.Value)
        //                CreatedUserId = Convert.ToString(drow["CreatedUserId"]);
        //            if (drow["ModifiedDate"] != DBNull.Value)
        //                ModifyDate = Convert.ToString(drow["ModifiedDate"]);
        //            if (drow["ModifiedUserId"] != DBNull.Value)
        //                ModifyUserId = Convert.ToString(drow["ModifiedUserId"]);

        //            retValue = true;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }        

        //    return retValue;
        //}

        public DataRow GetStockByProductIDAndBatchNumberAndMRP(string productID, string batchID, string mrp)
        {
            DataRow drow = null;
            try
            {
                DBStock dbData = new DBStock();
                drow = dbData.GetStockByProductIDAndBatchNumberAndMRP(productID, batchID, mrp);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return drow;
        }

        public DataRow GetStockByStockID(string StockID)
        {
            DataRow drow = null;
            try
            {
                DBStock dbData = new DBStock();
                drow = dbData.GetStockByStockID(StockID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return drow;
        }
        public DataTable GetStockForAll(string compID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetStockForAll(compID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;

        }

        public DataTable GetStockForAllWithOutZeroOpening(string compID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetStockForAllWithOutZeroOpening(compID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetStockForAllWithOutZeroClosing(string compID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetStockForAllWithOutZeroClosing(compID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetStockForAllBatchOpening(string compID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetStockForAllBatchOpening(compID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetStockForAllBatchClosing(string compID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetStockForAllBatchClosing(compID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetStockByCompanyID(string compid)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetStockByCompanyID(compid);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;

        }

        //public DataTable GetStockByCompanyIDWithOutZeroOpening(string compid)
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        DBSsStock dbss = new DBSsStock();
        //        dt = dbss.GetStockByCompanyIDWithOutZeroOpening(compid);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }        
        //    return dt;
        //}

        //public DataTable GetStockByCompanyIDWithOutZeroClosing(string compid)
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        DBSsStock dbss = new DBSsStock();
        //        dt = dbss.GetStockByCompanyIDWithOutZeroClosing(compid);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }        
        //    return dt;
        //}

        //public DataTable GetStockByCompanyIDBatchOpening(string compid)
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        DBSsStock dbss = new DBSsStock();
        //        dt = dbss.GetStockByCompanyIDBatchOpening(compid);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }        
        //    return dt;
        //}

        //public DataTable GetStockByCompanyIDBatchClosing(string compid)
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        DBSsStock dbss = new DBSsStock();
        //        dt = dbss.GetStockByCompanyIDBatchClosing(compid);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }        
        //    return dt;
        //}

        public DataTable GetOverviewDataShelfWise(string shelfid)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetOverviewDataShelfWise(shelfid);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetOverviewDataNonMoving(string mdate)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetOverviewDataNonMoving(mdate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetOverViewStocknSale(string mcompID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetOverViewStocknSale(mcompID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetOpeningStockForStocknSale(string mcompID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetOpeningStockForStocknSale(mcompID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }
        public int GetOpendingStockByProductID(string prodID)
        {
            int opstk = 0;
            try
            {
                DataRow dr;
                DBSsStock dbss = new DBSsStock();
                dr = dbss.GetOpendingStockByProductID(prodID);
                if (dr != null)
                {
                    if (dr["ProdOpeningStock"] != DBNull.Value)
                        opstk = Convert.ToInt32(dr["ProdOpeningStock"].ToString());
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return opstk;
        }
        public DataTable GetPurchaseStockForStocknSale(string mtodate, string mcompID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetPurchaseStockForStocknSale(mtodate,mcompID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetSaleStockForStocknSale(string mtodate, string mcompID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetSaleStockForStocknSale(mtodate,mcompID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetCRSTIStockForStocknSale(string mtodate,string mcompID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetCRSTIStockForStocknSale(mtodate,mcompID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetDBSTOStockForStocknSale(string mtodate, string mcompID)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetDBSTOStockForStocknSale(mtodate,mcompID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetDataForProductLedger(string mproductid, string mtodate)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetDataForProductLedger(mproductid, mtodate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetProductCategorywise()
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetProductCategorywise();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }
        public DataTable GetProductCompanywise()
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetProductCompanywise();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }

        public DataTable GetOverviewPatientShortList(string cday)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetOverviewPatientShortList(cday);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return dt;
        }
        public DataTable GetOverviewDebtorShortList(string cday)
        {
            DataTable dt = null;
            try
            {
                DBSsStock dbss = new DBSsStock();
                dt = dbss.GetOverviewDebtorShortList(cday);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        #endregion
    }
}
