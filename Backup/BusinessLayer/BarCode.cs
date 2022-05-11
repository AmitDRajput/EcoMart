using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.DataLayer;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public  class BarCode : BaseObject
    {
        #region Declaration
        private string _StockId;
        private string _ProductId;
        private string _ProdName;
        private string _BatchNo;
        private string _Expiry;
        private double _Mrp;
        private double _PurchaseRate;
        private double _SaleRate;
        private double _TradeRate;
        private string _ExpiryDate;          
        private int _ClosingStock;
        private int _Qty;
        private string _BarCodeNumber;

        #endregion
        #region Constructors, Destructors
        public BarCode()
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
          public string StockId
        {
            get { return _StockId; }
            set { _StockId = value; }

        }

        public string ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }

        }
        public string ProdName
        {
            get { return _ProdName; }
            set { _ProdName = value; }
        }
        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
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

        public int ClosingStock
        {
            get { return _ClosingStock; }
            set { _ClosingStock = value; }
        }
        public double Mrp
        {
            get { return _Mrp; }
            set { _Mrp = value; }
        }
        public double PurchaseRate
        {
            get { return _PurchaseRate; }
            set { _PurchaseRate = value; }
        }

        public double TradeRate
        {
            get { return _TradeRate; }
            set { _TradeRate = value; }
        }
        public double SaleRate
        {
            get { return _SaleRate; }
            set { _SaleRate = value; }
        }
        public int Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }
        public string BarCodeNumber
        {
            get { return _BarCodeNumber; }
            set { _BarCodeNumber = value; }
        }
        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _StockId = "";            
            _ProductId = "";
            _ProdName = "";
            _BatchNo = "";
            _Expiry = "";
            _Mrp = 0;
            _PurchaseRate = 0;
            _SaleRate = 0;
            _Qty = 0;          
            _ClosingStock = 0;
            _TradeRate = 0;           
            _ExpiryDate = "";
            _BarCodeNumber = "";
        }       
        public override void DoValidate()
        {
            try
            {
                if (Name == null || Name == "")
                    ValidationMessages.Add("Please enter the Area Name.");

                DBArea dbval = new DBArea();
                if (IFEdit == "Y")
                {
                    if (dbval.IsNameUniqueForEdit(Name, Id))
                    {
                        ValidationMessages.Add("Name Already Exists.");
                    }
                }
                else
                {
                    if (dbval.IsNameUniqueForAdd(Name, Id))
                    {
                        ValidationMessages.Add("Name Already Exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion 
        public DataTable GetOverviewProductData()
        {
            DBBarCode dbBarCode = new DBBarCode();
            return dbBarCode.GetProductData();
        }
        public DataTable GetBatchData(string productID )
        {
            DBBarCode dbBarCode = new DBBarCode();
            return dbBarCode.GetBatchData(productID);
        }


        public bool ReadDetailsByStockID(string StockID)
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCorrection dbData = new DBCorrection();
                drow = dbData.GetStockByStockID(StockID);
                if (drow != null)
                {
                    if (drow["StockID"] != DBNull.Value)
                        StockId = drow["StockID"].ToString();
                    if (drow["ProductId"] != DBNull.Value)
                        ProductId = Convert.ToString(drow["ProductId"]);
                    if (drow["BatchNumber"] != DBNull.Value)
                        BatchNo = Convert.ToString(drow["BatchNumber"]);
                    if (drow["Expiry"] != DBNull.Value)
                        Expiry = Convert.ToString(drow["Expiry"]);
                    if (drow["PurchaseRate"] != DBNull.Value)
                        PurchaseRate = Convert.ToDouble(drow["PurchaseRate"]);
                    if (drow["MRP"] != DBNull.Value)
                        Mrp = Convert.ToDouble(drow["MRP"]);
                    if (drow["SaleRate"] != DBNull.Value)
                        SaleRate = Convert.ToDouble(drow["SaleRate"]);                   
                    if (drow["TradeRate"] != DBNull.Value)
                        TradeRate = Convert.ToDouble(drow["TradeRate"]);
                    if (drow["ClosingStock"] != DBNull.Value)
                        ClosingStock = Convert.ToInt32(drow["ClosingStock"]);
                    if (drow["ScanCode"] != DBNull.Value)
                        BarCodeNumber = Convert.ToString(drow["ScanCode"]);
                    Expiry = General.GetValidExpiry(Expiry);
                    ExpiryDate = General.GetValidExpiryDate(Expiry);
                    ExpiryDate = General.GetExpiryInyyyymmddForm(ExpiryDate);                   

                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
    }
}
