using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.DataLayer;
using EcoMart.InterfaceLayer;


namespace EcoMart.BusinessLayer
{
    public class Correction : BaseObject
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
        private double _ProductVATPercent;
        private double _PurchaseVATPercent;
        private string _LastPurchaseDate;
        private string _LastPurchaseAccountID;
        private int _Qty;
        private double _DistributorRate;

        private string _NewStockId;
        private string _NewBatchNo;
        private string _NewExpiry;
        private string _NewExpiryDate;
        private double _NewMrp;
        private double _NewPurchRate;
        private double _NewSaleRate;
        private int _NewQty;
        private double _NewDistributorRate;

        private int _ProdLoosePack;
        private string _ProdPack;
        private string _ProdCompShortName;
        private int _ClosingStock;
        private int _OpeningStock;
        private int _TransferIn;
        private int _TransferOut;
        private int _VoucherNumber;
        private string _VoucherDate;
        private string _VoucherType;
        private string _VoucherSeries;

        #endregion

        #region Constructors, Destructors
        public Correction()
        {
            Initialise();
        }
        #endregion


        #region Properties

        public string StockId
        {
            get { return _StockId; }
            set { _StockId = value; }

        }

        public string NewStockId
        {
            get { return _NewStockId; }
            set { _NewStockId = value; }

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

        public string LastPurchaseDate
        {
            get { return _LastPurchaseDate; }
            set { _LastPurchaseDate = value; }
        }

        public string LastPurchaseAccountID
        {
            get { return _LastPurchaseAccountID; }
            set { _LastPurchaseAccountID = value; }
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
        public int ProdLoosePack
        {
            get { return _ProdLoosePack; }
            set { _ProdLoosePack = value; }
        }
        public string ProdPack
        {
            get { return _ProdPack; }
            set { _ProdPack = value; }
        }
        public string ProdCompShortName
        {
            get { return _ProdCompShortName; }
            set { _ProdCompShortName = value; }
        }
        public int ClosingStock
        {
            get { return _ClosingStock; }
            set { _ClosingStock = value; }
        }
        public int OpeningStock
        {
            get { return _OpeningStock; }
            set { _OpeningStock = value; }
        }
        public string NewBatchNo
        {
            get { return _NewBatchNo; }
            set { _NewBatchNo = value; }
        }
        public string NewExpiry
        {
            get { return _NewExpiry; }
            set { _NewExpiry = value; }
        }

        public string NewExpiryDate
        {
            get { return _NewExpiryDate; }
            set { _NewExpiryDate = value; }
        }
        public double NewMrp
        {
            get { return _NewMrp; }
            set { _NewMrp = value; }
        }
        public double NewPurchRate
        {
            get { return _NewPurchRate; }
            set { _NewPurchRate = value; }
        }
        public double NewSaleRate
        {
            get { return _NewSaleRate; }
            set { _NewSaleRate = value; }
        }
        public int NewQty
        {
            get { return _NewQty; }
            set { _NewQty = value; }
        }
        public int TransferIn
        {
            get { return _TransferIn; }
            set { _TransferIn = value; }
        }
        public int TransferOut
        {
            get { return _TransferOut; }
            set { _TransferOut = value; }
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

        public string VoucherType
        {
            get { return _VoucherType; }
            set { _VoucherType = value; }
        }

        public string VoucherSeries
        {
            get { return _VoucherSeries; }
            set { _VoucherSeries = value; }
        }

        public double DistributorRate
        {
            get { return _DistributorRate; }
            set { _DistributorRate = value; }
        }
        public double NewDistributorRate
        {
            get { return _NewDistributorRate; }
            set { _NewDistributorRate = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _StockId = "";
            _NewStockId = "";
            _ProductID = 0;
            _ProdName = "";
            _BatchNo = "";
            _Expiry = "";
            _Mrp = 0;
            _PurchaseRate = 0;
            _SaleRate = 0;
            _Qty = 0;       
            _ProdLoosePack = 0;
            _ProdPack = "";
            _ClosingStock = 0;
            _TradeRate = 0;
            _ProductVATPercent = 0;
            _PurchaseVATPercent = 0;
            _LastPurchaseDate = "";
            _LastPurchaseAccountID = "";
            _ExpiryDate = "";
            _NewExpiryDate = "";
            _NewDistributorRate = 0;
            _DistributorRate = 0;

            _VoucherNumber = 0;
            _VoucherDate = DateTime.Today.Date.ToString("yyyyMMdd");
            _VoucherType = FixAccounts.VoucherTypeForCorrectionInRate;
            _VoucherSeries = General.ShopDetail.ShopVoucherSeries;
        }
        public override void DoValidate()
        {
            try
            {
                if (_NewBatchNo == "")
                    ValidationMessages.Add("Please enter the New Batch No .");
                if (_NewExpiry == "")
                    ValidationMessages.Add("Please enter the New Expiry Date.");  
                if (_NewMrp == 0)
                    ValidationMessages.Add("Please enter the New MRP  .");
                if (_NewPurchRate == 0)
                    ValidationMessages.Add("Please enter the New Purchase Rate .");
                if (_NewSaleRate == 0)
                    ValidationMessages.Add("Please enter the New Sale Rate .");
                if (_NewQty == 0)
                    ValidationMessages.Add("Please enter the New Quantity.");
                if (_NewQty > _Qty)
                    ValidationMessages.Add("Please enter Correct Quantity .");

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

        public bool Cancel()
        {
            bool retValue = true;        
            return retValue;
        }

        #endregion

        #region Public Methods

        public  DataRow SearchForNewBatchAndMrpIntblStock(int ProductID, string NewBatch, double NewMrp)
        {
            DBCorrection dbCorrection = new DBCorrection();
            return dbCorrection.SearchForNewBatchAndMrpIntblStock(ProductID, NewBatch, NewMrp);
        }
        public bool AddDetails(string stockID, int ProductID, string batchno, string expiry, double mrp, double purrate, double salerate, int newQty, double traderate, string expirydate, double productvat, double purchasevat, string lastpurchasedate, string lastpurchaseaccountid, double distributorRate)
        {
            DBCorrection dbCorrection = new DBCorrection();
            return dbCorrection.AddDetails(stockID, ProductID, batchno, expiry, mrp, purrate, salerate, newQty,traderate,expirydate,productvat,purchasevat,lastpurchasedate,lastpurchaseaccountid,distributorRate);
        }

        public bool AddDetailsInVoucherCorrection()
        {
            DBCorrection dbCorrection = new DBCorrection();
            return dbCorrection.AddDetailsInVoucherCorrection(Id, StockId, NewStockId,VoucherNumber,VoucherDate,  ProductID,  BatchNo, Expiry, Mrp, PurchaseRate, SaleRate, Qty, NewBatchNo, NewExpiry, NewMrp, NewPurchRate, NewSaleRate, NewQty,DistributorRate,NewDistributorRate, CreatedBy,CreatedDate,CreatedTime);
            
        }

        public bool UpdateDetails()
        {
            DBCorrection dbCorrection = new DBCorrection();
            return dbCorrection.UpdateDetails(StockId, NewExpiry, NewExpiryDate,  NewPurchRate,NewSaleRate,NewDistributorRate);
        }
    
        public bool UpdateOldDetails(string stockID, int newqty)
        {
            DBCorrection dbCorrection = new DBCorrection();
            return dbCorrection.UpdateOldDetails(stockID, newqty);
        }

        public bool UpdateNewDetails(string stockID, int newqty)
        {
            DBCorrection dbCorrection = new DBCorrection();
            return dbCorrection.UpdateNewDetails(stockID, newqty);
        }
        public DataTable GetOverviewData()
        {
            DBCorrection dbCorrection = new DBCorrection();
            return dbCorrection.GetOverviewData();
        }

        public DataTable GetOverviewDataForSearch()
        {
            DBCorrection dbCorrection = new DBCorrection();
            return dbCorrection.GetOverviewDataForSearch();
        }

        public DataTable GetStockByProductID(int ProductID)
        {
            DBCorrection dbData = new DBCorrection();
            return dbData.GetStockByProductID(ProductID);

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
                    if (drow["ClosingStock"] != DBNull.Value)
                        ClosingStock = Convert.ToInt32(drow["ClosingStock"]);
                    if (drow["TradeRate"] != DBNull.Value)
                        TradeRate = Convert.ToDouble(drow["TradeRate"]);
                    Expiry = General.GetValidExpiry(Expiry);
                    ExpiryDate = General.GetValidExpiryDate(Expiry);
                    ExpiryDate = General.GetExpiryInyyyymmddForm(ExpiryDate);
                    if (drow["ProductVATPercent"] != DBNull.Value)
                        ProductVATPercent = Convert.ToDouble(drow["ProductVATPercent"]);
                    if (drow["PurchaseVATPercent"] != DBNull.Value)
                        PurchaseVATPercent = Convert.ToDouble(drow["PurchaseVATPercent"]);
                    if (drow["LastPurchaseDate"] != DBNull.Value)
                        LastPurchaseDate = drow["LastPurchaseDate"].ToString();
                    if (drow["DistributorSaleRate"] != DBNull.Value)
                        DistributorRate = Convert.ToDouble(drow["DistributorSaleRate"].ToString());

                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }


        public DataRow ReadDetailsByVoucherNumber()
        {
          
            DataRow drow = null;
            try
            {
                DBCorrection dbData = new DBCorrection();
                drow = dbData.ReadDetailsByVoucherNumber(VoucherNumber);
                if (drow != null)
                {
                    if (drow["ID"] != DBNull.Value)
                        Id = drow["ID"].ToString();
                    if (drow["ProductID"] != DBNull.Value)
                        ProductID = Convert.ToInt32(drow["ProductID"]);
                    if (drow["OldBatch"] != DBNull.Value)
                        BatchNo = Convert.ToString(drow["OldBatch"]);
                    if (drow["OldExpiry"] != DBNull.Value)
                        Expiry = Convert.ToString(drow["OldExpiry"]);
                    if (drow["oldPurchaseRate"] != DBNull.Value)
                        PurchaseRate = Convert.ToDouble(drow["OldPurchaseRate"]);
                    if (drow["OldMRP"] != DBNull.Value)
                        Mrp = Convert.ToDouble(drow["OldMRP"]);
                    if (drow["OldSaleRate"] != DBNull.Value)
                        SaleRate = Convert.ToDouble(drow["OldSaleRate"]);
                    if (drow["OldQuantity"] != DBNull.Value)
                        Qty = Convert.ToInt32(drow["OldQuantity"]);
                    if (drow["NewBatch"] != DBNull.Value)
                        NewBatchNo = drow["NewBatch"].ToString();
                    if (drow["NewExpiry"] != DBNull.Value)
                        NewExpiry = Convert.ToString(drow["NewExpiry"]);
                    if (drow["NewPurchaseRate"] != DBNull.Value)
                        NewPurchRate = Convert.ToDouble(drow["NewPurchaseRate"]);
                    if (drow["NewMRP"] != DBNull.Value)
                        NewMrp = Convert.ToDouble(drow["NewMRP"]);
                    if (drow["NewSaleRate"] != DBNull.Value)
                        NewSaleRate = Convert.ToDouble(drow["NewSaleRate"]);
                    if (drow["NewQuantity"] != DBNull.Value)
                        NewQty = Convert.ToInt32(drow["NewQuantity"]);
                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return drow;
        }




        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCorrection dbData = new DBCorrection();
                drow = dbData.ReadDetailsByID(Id);
                if (drow != null)
                {
                    if (drow["ID"] != DBNull.Value)
                        Id = drow["ID"].ToString();
                    if (drow["ProductID"] != DBNull.Value)
                        ProductID = Convert.ToInt32(drow["ProductID"]);
                    if (drow["OldBatch"] != DBNull.Value)
                        BatchNo = Convert.ToString(drow["OldBatch"]);
                    if (drow["OldExpiry"] != DBNull.Value)
                        Expiry = Convert.ToString(drow["OldExpiry"]);
                    if (drow["oldPurchaseRate"] != DBNull.Value)
                        PurchaseRate = Convert.ToDouble(drow["OldPurchaseRate"]);
                    if (drow["OldMRP"] != DBNull.Value)
                        Mrp = Convert.ToDouble(drow["OldMRP"]);
                    if (drow["OldSaleRate"] != DBNull.Value)
                        SaleRate = Convert.ToDouble(drow["OldSaleRate"]);
                    if (drow["OldQuantity"] != DBNull.Value)
                        Qty = Convert.ToInt32(drow["OldQuantity"]);
                    if (drow["NewBatch"] != DBNull.Value)
                        NewBatchNo = drow["NewBatch"].ToString();
                    if (drow["NewExpiry"] != DBNull.Value)
                        NewExpiry = Convert.ToString(drow["NewExpiry"]);
                    if (drow["NewPurchaseRate"] != DBNull.Value)
                        NewPurchRate = Convert.ToDouble(drow["NewPurchaseRate"]);
                    if (drow["NewMRP"] != DBNull.Value)
                        NewMrp = Convert.ToDouble(drow["NewMRP"]);
                    if (drow["NewSaleRate"] != DBNull.Value)
                        NewSaleRate = Convert.ToDouble(drow["NewSaleRate"]);
                    if (drow["NewQuantity"] != DBNull.Value)
                        NewQty = Convert.ToInt32(drow["NewQuantity"]);
                    if (drow["VoucherNumber"] != DBNull.Value)
                        VoucherNumber = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    if (drow["VoucherSeries"] != DBNull.Value)
                        VoucherSeries = drow["VoucherSeries"].ToString();

                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        #endregion

        public void SetQuantity(int prevqty, int newqty, string key)
        {
            try
            {
                if (key == "Insert")
                {
                    Qty = prevqty - newqty;
                    TransferIn = newqty;
                    TransferOut = 0;
                }
                else
                    if (key == "Update")
                    {
                        Qty = prevqty - newqty;
                        TransferIn = newqty;
                        TransferOut = 0;

                    }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
        public int GetAndUpdateCorrectionNumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetCorrectionInRate(voucherseries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return vouno;
        }
        public void GetLastRecord()
        {
            DataRow dr;
            try
            {
                DBCorrection dbs = new DBCorrection();
                dr = dbs.GetLastRecord(VoucherType, VoucherSeries);
                if (dr != null && dr["ID"] != null)
                {

                    Id = dr["ID"].ToString();

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
                DBCorrection dbs = new DBCorrection();
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
                DBCorrection dbs = new DBCorrection();
                dr = dbs.GetFirstRecord(VoucherType, VoucherSeries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }
    }
}