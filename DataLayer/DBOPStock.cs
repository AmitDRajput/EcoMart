using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBOPStock
    {
        public DBOPStock()
        {

        }

        public DataTable GetOpstock()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select masterID,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,AmountNet,AmountVAT5Percent,AmountVAT12point5Percent,AmountVATOPercent,CreatedDate,CreatedUserId,ModifyDate,ModifyUserId";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public bool AddDetails(string masterID, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
            double AmountNet, double AmountVAT5Percent,double AmountVAT12Point5Percent, double PurchaseAmountZeroVAT,string createdby, string createddate, string createdtime )
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(masterID, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                AmountNet, AmountVAT5Percent, AmountVAT12Point5Percent, PurchaseAmountZeroVAT, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool AddDetailsProductsSS(string Id, string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
               double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
               string IfTradeRateInclusiveOfVAT, double Amount, double spldiscamt, double spldiscper,string stockid,string MyDetailOPStockID, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetails(Id, ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercent,
                    PurchaseVATPercent, ProductVATPercent, AmountPurchaseVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT,
                    IfTradeRateInclusiveOfVAT, Amount, spldiscamt, spldiscper,stockid,MyDetailOPStockID,serialNumber);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool AddProductDetailsInStockTable(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
            string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double PurchaseVATPercent,
            double ProductVATPercent, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount,
            string accountId, string billnumber, string voutype, int vounumber, string voudate,string stockid,string ScanBarCode, int BeginningStock, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, PurchaseVATPercent,
                    ProductVATPercent, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount,
                    accountId, billnumber, voutype, vounumber, voudate,stockid, ScanBarCode,BeginningStock, createdby, createddate,createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        #region Query Building Functions      

        private string GetInsertQuery(string masterID,string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
            double AmountNet, double AmountVAT5Percent, double AmountVAT12Point5Percent, double AmtOtherPercentVAT,string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucheropstock";
            objQuery.AddToQuery("masterID", masterID);        
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherDate", VoucherDate);          
            objQuery.AddToQuery("AmountNet", AmountNet); 
            //objQuery.AddToQuery("AmountVAT5Percent", AmountVAT5Percent);
            //objQuery.AddToQuery("AmountVAT12point5Percent", AmountVAT12Point5Percent);
            //objQuery.AddToQuery("AmountVATOPercent", AmtOtherPercentVAT);
            //objQuery.AddToQuery("AmountZeroPercent", 0);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }


        private string GetInsertQueryDetails(string Id, string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
               double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
               string IfTradeRateInclusiveOfVAT, double Amount, double amtspldisc, double spldiscper, string stockid, string MyDetailOPStockID, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailopstock";
            objQuery.AddToQuery("masterID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
            objQuery.AddToQuery("ReplacementQuantity", ReplacementQuantity);      
            objQuery.AddToQuery("PurchaseVATPercent", PurchaseVATPercent);
            objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("AmountPurchaseVAT", AmountPurchaseVAT);
            objQuery.AddToQuery("CSTPercent", CSTPercent);
            objQuery.AddToQuery("AmountCST", AmountCST);
            objQuery.AddToQuery("IfMRPInclusiveOfVAT", IfMRPInclusiveOfVAT);
            objQuery.AddToQuery("IfTradeRateInclusiveOfVAT", IfTradeRateInclusiveOfVAT);    
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("DetailOpStockID",MyDetailOPStockID);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryDetailsInStockTable(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
            string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double PurchaseVATPercent,
            double ProductVATPercent, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount,
            string accountId, string billnumber, string voutype, int vounumber, string voudate,string stockid,string ScanBarCode,int BeginningStock, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblstock";         
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("BeginningStock", BeginningStock);
            objQuery.AddToQuery("ClosingStock", Quantity + SchemeQuantity + ReplacementQuantity);
            objQuery.AddToQuery("OpeningStock", Quantity);
            objQuery.AddToQuery("PurchaseSchemeStock", SchemeQuantity);
            objQuery.AddToQuery("PurchaseReplacementStock", ReplacementQuantity);
            objQuery.AddToQuery("PurchaseVATPercent", PurchaseVATPercent);
            objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("LastPurchaseAccountId", accountId);
            objQuery.AddToQuery("LastPurchaseBillNumber", billnumber);
            objQuery.AddToQuery("LastPurchaseVoucherType", voutype);
            objQuery.AddToQuery("LastPurchaseVoucherNumber", vounumber);
            objQuery.AddToQuery("LastPurchaseDate", voudate);
            objQuery.AddToQuery("PurchaseStock", 0);
            objQuery.AddToQuery("TransferInStock", 0);
            objQuery.AddToQuery("CreditNoteStock", 0);
            objQuery.AddToQuery("SaleStock", 0);
            objQuery.AddToQuery("TransferOutStock", 0);
            objQuery.AddToQuery("DebitNoteStock", 0);                 
            objQuery.AddToQuery("SaleSchemeStock", 0);       
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("ScanCode", ScanBarCode);
            objQuery.AddToQuery("IfRateCorrection", "");
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        #endregion

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select MasterID,VoucherNumber,VoucherType,VoucherDate,AmountNet from voucheropstock where VoucherDate >= '" + General.ShopDetail.Shopsy + "' && VoucherDate <= '" + General.ShopDetail.Shopey + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForProductList(string productid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate, a.MRP,(a.PurchaseRate * a.Quantity) as Amount, " +
                 "b.masterID,b.VoucherNumber,b.VoucherType,b.VoucherDate " +
                 "from detailopstock a  inner join voucheropstock b on a.MasterID = b.MasterID  where a.ProductId = '" + productid + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetProductPurchased(string masterID)
        {
            DataTable dtable = null;
            string strSql = string.Format("Select *, '0' as 'Amount' from masterproduct mp, detailopstock dp where mp.ProductID = dp.ProductID and masterID = '{0}' order by dp.SerialNumber", masterID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dr = null;
            if (Id != "")
            {
                string strSql = "Select * from  voucheropstock where masterID='{0}' ";
                strSql = string.Format(strSql, Id);
                dr = DBInterface.SelectFirstRow(strSql);
               
            }
            return dr;
        }

        public DataRow ReadDetailsByVoucherNumber(int vouno)
        {
            DataRow dr = null;
            if (vouno != 0)
            {
                string strSql = "Select * from  voucheropstock where vouchernumber = {0} ";
                strSql = string.Format(strSql, vouno);
                dr = DBInterface.SelectFirstRow(strSql);

            }
            return dr;
        }


        public DataTable ReadProductDetailsByID(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdCompID,a.ProdIfOctroi,a.ProdShelfID,b.ProductID, " +
                "b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.AmountProdVAT," +
                "b.PurchaseVATPercent,b.SchemeQuantity,b.ReplacementQuantity," +
                "b.AmountPurchaseVAT,b.CSTPercent,b.AmountCST,b.Quantity*b.TradeRate as Amount," +
                "b.StockID,d.stockID,d.ClosingStock,e.ShelfCode,e.ShelfID from masterproduct A inner join  detailopstock B  on A.ProductId = B.ProductID    left outer join tblstock D on B.StockID = D.StockID  left outer join mastershelf e on a.ProdShelfID = e.ShelfId  where B.MasterID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataRow GetLastRecord(string vouType, string vouSeries)
        {
            DataRow dRow = null;

            string strSql = "Select MasterID,VoucherNumber,VoucherType,VoucherDate,AmountNet from voucheropstock order by VoucherNumber desc";

            //string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' && voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber desc";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        public DataRow GetLastVoucherNumber(string vouType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select VoucherNumber from voucheropstock order by VoucherNumber desc";

                // string strSql = "Select Vouchernumber from vouchercreditdebitnote where  VoucherType =  '" + vouType + "'  &&  VoucherSeries = '" + vouSeries + "' order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetFirstRecord(string vouType, string vouSeries)
        {
            DataRow dRow = null;
            string strSql = "Select MasterID,VoucherNumber,VoucherType,VoucherDate,AmountNet from voucheropstock order by VoucherNumber";

            //string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' && voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber desc";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }
        public DataRow GetLastSerialNumber(string masterID)
        {
            DataRow dRow = null;
            {
                string strSql = "Select a.ProductID,a.SerialNumber,b.ProdName from detailopstock a inner join masterproduct b on a.ProductID = b.ProductID  where a.masterID = '" + masterID + "' order by a.SerialNumber desc";
                // string strSql = "Select Vouchernumber from vouchercreditdebitnote where  VoucherType =  '" + vouType + "'  &&  VoucherSeries = '" + vouSeries + "' order by Vouchernumber desc ";		
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow GetDuplicateBarcode(string Barcode) // [08.02.2017]
        {
            DataRow dr;
            string strsql = "select StockID,ProductID,ScanCode from tblstock where ScanCode = '" + Barcode + "'";
            dr = DBInterface.SelectFirstRow(strsql);
            return dr;
        }
    }
}
