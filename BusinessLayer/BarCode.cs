using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;
using System.Data;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class BarCode : BaseObject
    {
        #region Declaration
        private string _StockId;
        private int _ProductID;
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

        private int _LastProductNumberForBarCode;
        private int _LastBatchNumberForBarCode;


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

        public int LastProductNumberForBarCode
        {
            get { return _LastProductNumberForBarCode; }
            set { _LastProductNumberForBarCode = value; }
        }
        public int LastBatchNumberForBarCode
        {
            get { return _LastBatchNumberForBarCode; }
            set { _LastBatchNumberForBarCode = value; }
        }
        public string StockId
        {
            get { return _StockId; }
            set { _StockId = value; }

        }

        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }

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
            _ProductID = 0;
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
            _LastBatchNumberForBarCode = 0;
            _LastProductNumberForBarCode = 0;
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
        public DataTable GetBatchData(int ProductID)
        {
            DBBarCode dbBarCode = new DBBarCode();
            return dbBarCode.GetBatchData(ProductID.ToString());
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
                    if (drow["ProductID"] != DBNull.Value)
                        ProductID = Convert.ToInt32(drow["ProductID"]);
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

        public bool CheckIfProcedureIsAlreadyDone()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBBarCode dbData = new DBBarCode();
                drow = dbData.GettblVoucherNumberRow();
                if (drow == null)
                {

                    retValue = true;
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public void FillBarCodeNumbers(int prodno)
        {
            bool retValue = false;
            int mProductID = 0;
            string mbarcode = "";
            string mstockid = "";
            LastBatchNumberForBarCode = 1001;
            LastProductNumberForBarCode = prodno;
            DBBarCode dbData = new DBBarCode();
            DataTable dt = new DataTable();
            DataTable dtstock = new DataTable();
            dt = dbData.GetProductsFromMasterProduct();
            int dd = dt.Rows.Count;
            try
            {
                if (dd > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["ProductNumberForBarcode"] == DBNull.Value || dr["ProductNumberForBarcode"].ToString() == string.Empty)
                        {
                            if (dr["ProductID"] != DBNull.Value)
                            {
                                LastBatchNumberForBarCode = 1001;
                                mProductID = Convert.ToInt32(dr["ProductID"].ToString());
                                retValue = dbData.FillBarCodeNumbers(mProductID, LastProductNumberForBarCode.ToString());
                                dtstock = dbData.GetRowsFromtblStock(mProductID);
                                if (dtstock != null && dtstock.Rows.Count > 0)
                                {
                                    if (dtstock.Rows[0]["ScanCode"] != DBNull.Value && dtstock.Rows[0]["ScanCode"].ToString() != string.Empty)
                                        LastBatchNumberForBarCode = Convert.ToInt32(dtstock.Rows[0]["ScanCode"].ToString().Substring(6, 4)) + 1;
                                }
                                if (dtstock != null && dtstock.Rows.Count > 0)
                                {
                                    foreach (DataRow drstock in dtstock.Rows)
                                    {
                                        if (drstock["StockID"] != DBNull.Value && drstock["StockID"].ToString() != string.Empty)
                                        {
                                            mstockid = drstock["StockID"].ToString();
                                            mbarcode = string.Concat(LastProductNumberForBarCode, LastBatchNumberForBarCode.ToString());
                                            retValue = dbData.FillBarcodeIntblStock(mstockid, mbarcode);
                                            LastBatchNumberForBarCode += 1;
                                        }
                                    }
                                }

                                LastProductNumberForBarCode += 1;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }


        }

        public void GetProductAndBatchNumber()
        {
            int mLastProductNumberForBarCode = 0;
            DataRow drow = null;
            try
            {
                DBBarCode dbData = new DBBarCode();
                drow = dbData.GetLastProductNumberForBarCode();
                if (drow != null)
                {
                    if (drow["ProductNumberForBarcode"] != DBNull.Value && drow["ProductNumberForBarcode"].ToString() != string.Empty)
                        mLastProductNumberForBarCode = Convert.ToInt32(drow["ProductNumberForBarcode"].ToString());
                }

                LastProductNumberForBarCode = mLastProductNumberForBarCode;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }

        public bool DeletedRowsFromtblBarCode()
        {
            bool retValue = true;
            DBBarCode dbData = new DBBarCode();
            retValue = dbData.DeletedRowsFromtblBarCode();
            return retValue;
        }

        public DataTable GetPurchaseData(string mvoutype, int mvouno)
        {
            DataTable dt = new DataTable();
            DBBarCode dbData = new DBBarCode();
            dt = dbData.GetPurchaseData(mvoutype, mvouno);
            return dt;
        }
        public DataTable GetStockByProductID(int ProductID)
        {
            DBBarCode dbData = new DBBarCode();
            return dbData.GetStockByProductID(ProductID);
        }
        public DataTable GetGivenProductData(string mstockID)
        {
            DataTable dt = new DataTable();
            DBBarCode dbData = new DBBarCode();
            dt = dbData.GetGivenProductData(mstockID);
            return dt;
        }

        public DataTable GetShelfwiseData(string mshelf)
        {
            DataTable dt = new DataTable();
            DBBarCode dbData = new DBBarCode();
            dt = dbData.GetShelfwiseData(mshelf);
            return dt;
        }
    }
}
