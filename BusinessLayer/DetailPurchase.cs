using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class DetailPurchase : BaseObject
    {
        #region Declaration
        private string _PurchaseId;
        private string _ProductID;
        private string _BatchNumber;
        private double _TradeRate;
        private double _PurchaseRate;
        private double _MRP;
        private double _SaleRate;
        private string _Expiry;
        private string _ExpiryDate;
        private long    _Quantity;
        private long    _SchemeQuantity;
        private double _ItemDiscountPercent;
        private double _AmountItemDiscount;
        private double _SchemeDiscountPercentage;
        private double _AmountSchemeDiscount;
        private double _PurchaseVATtPercent;
        private double _ProductVATPercent;
        private double _AmountPurchseVAT;
        private double _CSTPercent;
        private double _AmountCST;
        private string _IfMRPInclusiveOfVAT;
        private string _IfTradeRateInclusiveOfVAT;
        private long   _ReplacementQuentity;
        private double _Amount;
        #endregion

        private Product _Product = new Product();

        public Product Product
        {
            get
            {
                return _Product;
            }
            set
            {
                _Product = value;
            }
        }

        #region Properties
        public string PurchaseId
        {
            get { return _PurchaseId; }
            set { _PurchaseId = value; }
        }
        
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        
        public string BatchNumber
        {
            get { return _BatchNumber; }
            set { _BatchNumber = value; }
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
        
        public long Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        
        public long SchemeQuantity
        {
            get { return _SchemeQuantity; }
            set { _SchemeQuantity = value; }
        }

        public long ReplacementQuantity
        {
            get 
            {
                return this._ReplacementQuentity;
            }
            set
            {
                this._ReplacementQuentity = value;
            }
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
        
        public double SchemeDiscountPercentage
        {
            get { return _SchemeDiscountPercentage; }
            set { _SchemeDiscountPercentage = value; }
        }
        
        public double AmountSchemeDiscount
        {
            get { return _AmountSchemeDiscount; }
            set { _AmountSchemeDiscount = value; }
        }
        
        public double PurchaseVATtPercent
        {
            get { return _PurchaseVATtPercent; }
            set { _PurchaseVATtPercent = value; }
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

        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _PurchaseId = "";
                _ProductID = "";
                _BatchNumber = "";
                _TradeRate = 0;
                _PurchaseRate = 0;
                _MRP = 0;
                _SaleRate = 0;
                _Expiry = "";
                _ExpiryDate = "";
                _Quantity = 0;
                _SchemeQuantity = 0;
                _ItemDiscountPercent = 0;
                _AmountItemDiscount = 0;
                _SchemeDiscountPercentage = 0;
                _AmountSchemeDiscount = 0;
                _PurchaseVATtPercent = 0;
                _ProductVATPercent = 0;
                _AmountPurchseVAT = 0;
                _CSTPercent = 0;
                _AmountCST = 0;
                _IfMRPInclusiveOfVAT = "";
                _IfTradeRateInclusiveOfVAT = "";
                _Amount = 0;
                _ReplacementQuentity = 0;
                _SchemeDiscountPercentage = 0;

                _Product = new Product();
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
                ValidationMessages.Clear();
                if (BatchNumber == "")
                    ValidationMessages.Add("Please enter the  BatchNumber");
                if (TradeRate == 0)
                    ValidationMessages.Add("Please enter the  TradeRate");
                if (PurchaseRate == 0)
                    ValidationMessages.Add("Please enter the  PurchaseRate");
                if (MRP == 0)
                    ValidationMessages.Add("Please enter the  MRP");
                if (SaleRate == 0)
                    ValidationMessages.Add("Please enter the  SaleRate");
                if (Expiry != "00/00" &&
                     !System.Text.RegularExpressions.Regex.IsMatch(ExpiryDate, @"^([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]([1-9]|0[1-9]|1[012])[- /.][0-9]{4}$"))
            
                    ValidationMessages.Add("Please enter the valid ExpiryDate");

                if (Quantity == 0)
                    ValidationMessages.Add("Please enter the  Quantity");
                if (PurchaseVATtPercent != 0)
                {
                    // vat 5.5
                    if (PurchaseVATtPercent != 6)
                    {
                        if (PurchaseVATtPercent != 13.5)
                        {
                            ValidationMessages.Add("Please enter the correct PurchaseVATPercent");
                        }
                    }
                }
                if (ProductVATPercent == 0)
                    ValidationMessages.Add("Please enter the  ProductVATPercent");
                if (AmountCST < 0)
                    ValidationMessages.Add("Please enter the  AmountCST");
                if (IfMRPInclusiveOfVAT == "")
                    ValidationMessages.Add("Please enter the  IfMRPInclusiveOfVAT");
                if (IfTradeRateInclusiveOfVAT == "")
                    ValidationMessages.Add("Please enter the  IfTradeRateInclusiveOfVAT");

                //bool retValue = General.CheckDates(date CrdbVouDate, CrdbVouDate);
                //if (retValue == false)
                //{
                //    ValidationMessages.Add("Please Check Date...");
                //}
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
        }

        #endregion

        #region Public Methods
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBDetailPurchase dbData = new DBDetailPurchase();
                drow = dbData.ReadDetailsByID(Id);
                if (drow != null)
                {
                    if (drow["PurchaseId"] != DBNull.Value)
                        PurchaseId = Convert.ToString(drow["PurchaseId"]);
                    if (drow["ProductID"] != DBNull.Value)
                        ProductID = Convert.ToString(drow["ProductID"]);
                    if (drow["BatchNumber"] != DBNull.Value)
                        BatchNumber = Convert.ToString(drow["BatchNumber"]);
                    if (drow["TradeRate"] != DBNull.Value)
                        TradeRate = Convert.ToDouble(drow["TradeRate"]);
                    if (drow["PurchaseRate"] != DBNull.Value)
                        PurchaseRate = Convert.ToDouble(drow["PurchaseRate"]);
                    if (drow["MRP"] != DBNull.Value)
                        MRP = Convert.ToDouble(drow["MRP"]);
                    if (drow["SaleRate"] != DBNull.Value)
                        SaleRate = Convert.ToDouble(drow["SaleRate"]);
                    if (drow["Expiry"] != DBNull.Value)
                        Expiry = Convert.ToString(drow["Expiry"]);
                    if (drow["ExpiryDate"] != DBNull.Value)
                        ExpiryDate = Convert.ToString(drow["ExpiryDate"]);
                    if (drow["Quantity"] != DBNull.Value)
                        Quantity = Convert.ToInt64(drow["Quantity"]);
                    if (drow["SchemeQuantity"] != DBNull.Value)
                        SchemeQuantity = Convert.ToInt64(drow["SchemeQuantity"]);
                    if (drow["ReplacementQuantity"] != DBNull.Value)
                        ReplacementQuantity = Convert.ToInt64(drow["ReplacementQuantity"]);
                    if (drow["ItemDiscountPercent"] != DBNull.Value)
                        ItemDiscountPercent = Convert.ToDouble(drow["ItemDiscountPercent"]);
                    if (drow["AmountItemDiscount"] != DBNull.Value)
                        AmountItemDiscount = Convert.ToDouble(drow["AmountItemDiscount"]);
                    if (drow["SchemeDiscountPercentage"] != DBNull.Value)
                        SchemeDiscountPercentage = Convert.ToDouble(drow["SchemeDiscountPercentage"]);
                    if (drow["AmountSchemeDiscount"] != DBNull.Value)
                        AmountSchemeDiscount = Convert.ToDouble(drow["AmountSchemeDiscount"]);
                    if (drow["PurchaseVATtPercent"] != DBNull.Value)
                        PurchaseVATtPercent = Convert.ToDouble(drow["PurchaseVATtPercent"]);
                    if (drow["ProductVATPercent"] != DBNull.Value)
                        ProductVATPercent = Convert.ToByte(drow["ProductVATPercent"]);
                    if (drow["AmountPurchaseVAT"] != DBNull.Value)
                        AmountPurchaseVAT = Convert.ToDouble(drow["AmountPurchaseVAT"]);
                    if (drow["CSTPercent"] != DBNull.Value)
                        CSTPercent = Convert.ToDouble(drow["CSTPercent"]);
                    if (drow["AmountCST"] != DBNull.Value)
                        AmountCST = Convert.ToDouble(drow["AmountCST"]);
                    if (drow["IfMRPInclusiveOfVAT"] != DBNull.Value)
                        IfMRPInclusiveOfVAT = Convert.ToString(drow["IfMRPInclusiveOfVAT"]);
                    if (drow["IfTradeRateInclusiveOfVAT"] != DBNull.Value)
                        IfTradeRateInclusiveOfVAT = Convert.ToString(drow["IfTradeRateInclusiveOfVAT"]);

                    _Product.Id = ProductID;
                    _Product.ReadDetailsByID();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }

        public bool GetPurchaseByProductIdAndBatchId(string productID, string batchID, string BatchMRP)
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBDetailPurchase dbData = new DBDetailPurchase();
                drow = dbData.GetPurchaseByProductIdAndBatchId(productID, batchID, BatchMRP);
                if (drow != null)
                {
                    if (drow["PurchaseId"] != DBNull.Value)
                        PurchaseId = Convert.ToString(drow["PurchaseId"]);
                    if (drow["ProductID"] != DBNull.Value)
                        ProductID = Convert.ToString(drow["ProductID"]);
                    if (drow["BatchNumber"] != DBNull.Value)
                        BatchNumber = Convert.ToString(drow["BatchNumber"]);
                    if (drow["TradeRate"] != DBNull.Value)
                        TradeRate = Convert.ToDouble(drow["TradeRate"]);
                    if (drow["PurchaseRate"] != DBNull.Value)
                        PurchaseRate = Convert.ToDouble(drow["PurchaseRate"]);
                    if (drow["MRP"] != DBNull.Value)
                        MRP = Convert.ToDouble(drow["MRP"]);
                    if (drow["SaleRate"] != DBNull.Value)
                        SaleRate = Convert.ToDouble(drow["SaleRate"]);
                    if (drow["Expiry"] != DBNull.Value)
                        Expiry = Convert.ToString(drow["Expiry"]);
                    if (drow["ExpiryDate"] != DBNull.Value)
                        ExpiryDate = Convert.ToString(drow["ExpiryDate"]);
                    if (drow["Quantity"] != DBNull.Value)
                        Quantity = Convert.ToInt64(drow["Quantity"]);
                    if (drow["SchemeQuantity"] != DBNull.Value)
                        SchemeQuantity = Convert.ToInt64(drow["SchemeQuantity"]);
                    if (drow["ReplacementQuantity"] != DBNull.Value)
                        ReplacementQuantity = Convert.ToInt64(drow["ReplacementQuantity"]);
                    if (drow["ItemDiscountPercent"] != DBNull.Value)
                        ItemDiscountPercent = Convert.ToDouble(drow["ItemDiscountPercent"]);
                    if (drow["AmountItemDiscount"] != DBNull.Value)
                        AmountItemDiscount = Convert.ToDouble(drow["AmountItemDiscount"]);               
                    if (drow["AmountSchemeDiscount"] != DBNull.Value)
                        AmountSchemeDiscount = Convert.ToDouble(drow["AmountSchemeDiscount"]);
                    if (drow["PurchaseVATtPercent"] != DBNull.Value)
                        PurchaseVATtPercent = Convert.ToDouble(drow["PurchaseVATtPercent"]);
                    if (drow["ProductVATPercent"] != DBNull.Value)
                        ProductVATPercent = Convert.ToByte(drow["ProductVATPercent"]);
                    if (drow["AmountPurchaseVAT"] != DBNull.Value)
                        AmountPurchaseVAT = Convert.ToDouble(drow["AmountPurchaseVAT"]);
                    if (drow["CSTPercent"] != DBNull.Value)
                        CSTPercent = Convert.ToDouble(drow["CSTPercent"]);
                    if (drow["AmountCST"] != DBNull.Value)
                        AmountCST = Convert.ToDouble(drow["AmountCST"]);
                    if (drow["IfMRPInclusiveOfVAT"] != DBNull.Value)
                        IfMRPInclusiveOfVAT = Convert.ToString(drow["IfMRPInclusiveOfVAT"]);
                    if (drow["IfTradeRateInclusiveOfVAT"] != DBNull.Value)
                        IfTradeRateInclusiveOfVAT = Convert.ToString(drow["IfTradeRateInclusiveOfVAT"]);
                    if (drow["Amount"] != DBNull.Value)
                        Amount = Convert.ToDouble(drow["Amount"]);
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }        
            return retValue;
        }

        public bool AddDetails()
        {
            DBDetailPurchase dbData = new DBDetailPurchase();
            DateTime expDt;
            if (DateTime.TryParse(ExpiryDate, out expDt))
            {
                ExpiryDate = expDt.ToString("yyyy/dd/MM").Replace("/", "");
            }
           return dbData.AddDetails(PurchaseId, ProductID, BatchNumber, TradeRate, PurchaseRate, MRP, SaleRate, Expiry, ExpiryDate, Quantity, SchemeQuantity,ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercentage, AmountSchemeDiscount, PurchaseVATtPercent, ProductVATPercent, AmountPurchaseVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount);
           
        }

        public bool UpdateDetails()
        {
            DBDetailPurchase dbData = new DBDetailPurchase();
            return dbData.UpdateDetails(PurchaseId, ProductID, BatchNumber, TradeRate, PurchaseRate, MRP, SaleRate, Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercentage, AmountSchemeDiscount, PurchaseVATtPercent, ProductVATPercent, AmountPurchaseVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount);
        }

        public bool DeleteDetails()
        {
            DBDetailPurchase dbData = new DBDetailPurchase();
            return dbData.DeleteDetails(Id);
        }

        #endregion

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBDetailPurchase dbPur = new DBDetailPurchase();
            return dbPur.GetOverviewData();
        }        
        #endregion

    }
}
