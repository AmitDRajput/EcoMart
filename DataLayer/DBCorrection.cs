using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBCorrection
    {

        public DBCorrection()
        {
        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ProductID,ProdName, ProdLoosePack, ProdPack, ProdCompShortName  from masterproduct order by ProductID";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForSearch()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSeries,a.ProductID, b.ProdName, b.ProdLoosePack, b.ProdPack, b.ProdCompShortName  from vouchercorrectioninrate a inner join masterproduct b on a.Productid = b.ProductId where  a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' && a.VoucherDate <= '" + General.ShopDetail.Shopey + "' order by a.VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public bool AddDetails(string stockid, string productId , string BatchNumber, string Expiry,
        double MRP, double PurchaseRate, double SellRate, int Qty, double traderate, string expirydate, double productvat, double purchasevat, string lastpurchasedate, string lastpurchaseaccountid, double distributorRate)
            {
            bool bRetValue = false;
            string strSql = GetInsertQuery(stockid,productId, BatchNumber, Expiry, MRP, PurchaseRate, SellRate, Qty,traderate,expirydate,productvat,purchasevat,lastpurchasedate,lastpurchaseaccountid,distributorRate) ;

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetInsertQuery(string stockid, string productID, string BatchNumber, string Expiry,
        double MRP, double PurchaseRate, double SaleRate, int Qty, double traderate, string expirydate, double productvat, double purchasevat, string lastpurchasedate, string lastpurchaseaccountid, double distributorRate)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblstock";
            objQuery.AddToQuery("StockID",stockid);
            objQuery.AddToQuery("ProductId",productID);
            objQuery.AddToQuery("BatchNumber", BatchNumber);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("ClosingStock", Qty);
            objQuery.AddToQuery("TransferInStock", Qty);
            objQuery.AddToQuery("TradeRate", traderate);
            objQuery.AddToQuery("ExpiryDate", expirydate);
            objQuery.AddToQuery("ProductVATPercent", productvat);
            objQuery.AddToQuery("PurchaseVATPercent", purchasevat);
            objQuery.AddToQuery("LastPurchaseDate", lastpurchasedate);
            objQuery.AddToQuery("OpeningStock", 0);
            objQuery.AddToQuery("PurchaseStock", 0);
            objQuery.AddToQuery("CreditNoteStock", 0);
            objQuery.AddToQuery("DebitNoteStock", 0);
            objQuery.AddToQuery("SaleStock", 0);
            objQuery.AddToQuery("TransferOutStock", 0);
            objQuery.AddToQuery("PurchaseSchemeStock", 0);
            objQuery.AddToQuery("PurchaseReplacementStock", 0);
            objQuery.AddToQuery("SaleSchemeStock", 0);      
            objQuery.AddToQuery("LastPurchaseAccountID", lastpurchaseaccountid);
            objQuery.AddToQuery("LastPurchaseBillNumber", "");
            objQuery.AddToQuery("LastPurchaseVoucherType", "");
            objQuery.AddToQuery("LastPurchaseVoucherNumber", 0);
            objQuery.AddToQuery("DistributorSaleRate", distributorRate);


            return objQuery.InsertQuery();
        }

        public bool AddDetailsInVoucherCorrection(string ID, string Oldstockid, string NewStockID, int Vouchernumber, string Voucherdate, string productId, string OldBatch, string OldExpiry,
                    double OldMRP, double OldPurchaseRate, double OldSellRate, int OldQty,
                    string NewBatch, string NewExpiry, double NewMRP, double NewPurchaseRate, double NewSaleRate, int NewQty, double distributorRate, double newDistributorRate, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryInVoucherCorrection(ID, Oldstockid, NewStockID, Vouchernumber, Voucherdate, productId, OldBatch, OldExpiry,
                               OldMRP, OldPurchaseRate, OldSellRate, OldQty, NewBatch, NewExpiry, NewMRP, NewPurchaseRate, NewSaleRate, NewQty,distributorRate,newDistributorRate, createdby,createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQueryInVoucherCorrection(string ID, string Oldstockid, string NewStockID, int Vouchernumber, string Voucherdate, string productId, string OldBatch, string OldExpiry,
                    double OldMRP, double OldPurchaseRate, double OldSellRate, int OldQty,
                    string NewBatch, string NewExpiry, double NewMRP, double NewPurchaseRate, double NewSaleRate, int NewQty, double olddistributorRate, double newDistributorRate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercorrectioninrate";
            objQuery.AddToQuery("ID", ID);
            objQuery.AddToQuery("OldStockID", Oldstockid);
            objQuery.AddToQuery("NewStockID", NewStockID);
            objQuery.AddToQuery("VoucherNumber", Vouchernumber);
            objQuery.AddToQuery("VoucherDate", Voucherdate);
            objQuery.AddToQuery("VoucherType", FixAccounts.VoucherTypeForCorrectionInRate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("ProductID", productId);
            objQuery.AddToQuery("OldBatch", OldBatch);
            objQuery.AddToQuery("OldExpiry", OldExpiry);
            objQuery.AddToQuery("OldMRP", OldMRP);
            objQuery.AddToQuery("OldPurchaseRate", OldPurchaseRate);
            objQuery.AddToQuery("OldSaleRate", OldSellRate);
            objQuery.AddToQuery("OldQuantity", OldQty);
            objQuery.AddToQuery("OldDistributorSaleRate", olddistributorRate);
            objQuery.AddToQuery("NewDistributorSaleRate", newDistributorRate);
            objQuery.AddToQuery("NewBatch", NewBatch);
            objQuery.AddToQuery("NewExpiry", NewExpiry);
            objQuery.AddToQuery("NewMRP", NewMRP);
            objQuery.AddToQuery("NewPurchaseRate", NewPurchaseRate);
            objQuery.AddToQuery("NewSaleRate", NewSaleRate);
            objQuery.AddToQuery("NewQuantity", NewQty);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            objQuery.AddToQuery("CreatedUserID", createdby);
            return objQuery.InsertQuery();
        }


        public DataTable SelectUniqueBatch(string Id, string Batch)
        {
            DataTable dtable = new DataTable();
            string strsql = string.Format("Select ProductId,BatchNumber from tblstock where ProductId = '{0}' and BatchNumber='{1}'", Id, Batch);
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        public DataRow SearchForNewBatchAndMrpIntblStock(string productidId, string NewBatch, double  NewMrp)
        {
            DataRow dr = null;
            string strsql = string.Format("Select stockid from tblstock where ProductId = '{0}' && BatchNumber = '{1}' && Mrp = {2}", productidId,NewBatch,NewMrp);
            dr = DBInterface.SelectFirstRow(strsql);
            return dr;
        }
        public DataTable GetStockByProductID(string prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select StockID,ProductId,BatchNumber,MRP,Expiry,ExpiryDate,TradeRate,PurchaseRate,TransferInStock,SaleRate,OpeningStock,ClosingStock,PurchaseStock ,CreditNoteStock,SaleStock,TransferOutStock,DebitNoteStock,PurchaseSchemeStock,PurchaseReplacementStock,SaleSchemeStock,IfRateCorrection,ProductVATPercent,PurchaseVATPercent,LastPurchaseAccountId,LastPurchaseBillNumber,LastPurchaseDate,LastPurchaseVoucherType,LastPurchaseVoucherNumber,ScanCode,DistributorSaleRate,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId from tblstock where ProductID  = '{0}' and ClosingStock <> 0", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }
        public DataRow GetStockByStockID(string stockID)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select StockID,ProductId,BatchNumber,MRP,Expiry,ExpiryDate,TradeRate,PurchaseRate,SaleRate,OpeningStock,ClosingStock,PurchaseStock ,TransferInStock,CreditNoteStock,SaleStock,TransferOutStock,DebitNoteStock,PurchaseSchemeStock,PurchaseReplacementStock,SaleSchemeStock,IfRateCorrection,ProductVATPercent,PurchaseVATPercent,LastPurchaseAccountId,LastPurchaseBillNumber,LastPurchaseDate,LastPurchaseVoucherType,LastPurchaseVoucherNumber,ScanCode,DistributorSaleRate,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId from tblstock where StockID = '{0}'", stockID);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }
        public bool UpdateDetails(string StockID, string Expiry,string expirydate, double PurchaseRate, double SaleRate, double distributorRate)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(StockID , Expiry, expirydate, PurchaseRate, SaleRate,distributorRate);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetUpdateQuery(string stockid, string Expiry, string ExpiryDate,double PurchaseRate,double SellRate, double distributorRate)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblstock";
            objQuery.AddToQuery("StockID",stockid ,true);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("PurchaseRate",PurchaseRate);
            objQuery.AddToQuery("SaleRate", SellRate);
            objQuery.AddToQuery("IfRateCorrection", "Y");
            objQuery.AddToQuery("DistributorSaleRate", distributorRate);         
            return objQuery.UpdateQuery();
        }

        public bool UpdateOldDetails(string stockid, int Qty)
        {
            bool bRetValue = false;
            string strSql = "Update tblstock set closingstock = closingstock - " + Qty + ", TransferOutStock = TransferOutStock + " + Qty + " where stockid = '" + stockid + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateNewDetails(string newstockid, int Qty)
        {
            bool bRetValue = false;
            string strSql = "Update tblstock set closingstock = closingstock + " + Qty + ", TransferInStock = TransferInStock + " + Qty + " where stockid = '" + newstockid + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetUpdateOldDetailsQuery(string stockid, int Qty)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblstock";
            objQuery.AddToQuery("StockId",stockid,  true);              
            objQuery.AddToQuery("ClosingStock",Qty);       
            return objQuery.UpdateQuery();
        }

        public DataRow  ReadDetailsByVoucherNumber(int vouno)
        {
            DataRow dr = null;
            string strSql = "Select a.ID,a.VoucherType,a.VoucherNumber,a.VoucherSeries,a.VoucherDate,a.ProductID, b.ProdName, b.ProdLoosePack, b.ProdPack, b.ProdCompShortName,a.oldBatch,a.oldExpiry,a.oldMRP,a.OldPurchaseRate,a.OldSaleRate,a.OldQuantity,a.NewBatch,a.NewExpiry,a.NewMRP,a.NewPurchaseRate,a.NewSalerate,a.NewQuantity  from vouchercorrectioninrate a inner join masterproduct b on a.Productid = b.ProductId  where a.vouchernumber = "+vouno +" order by a.VoucherNumber";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }

        public DataRow ReadDetailsByID(string ID)
        {
            DataRow dr = null;
            string strSql = "Select a.ID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSeries,a.ProductID, b.ProdName, b.ProdLoosePack, b.ProdPack, b.ProdCompShortName,a.oldBatch,a.oldExpiry,a.oldMRP,a.OldPurchaseRate,a.OldSaleRate,a.OldQuantity,a.NewBatch,a.NewExpiry,a.NewMRP,a.NewPurchaseRate,a.NewSalerate,a.NewQuantity  from vouchercorrectioninrate a inner join masterproduct b on a.Productid = b.ProductId  where a.ID = '" + ID + "' order by a.VoucherNumber";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }


        public DataRow GetLastRecord(string vouType, string vouSeries)
        {
            DataRow dRow = null;

            string strSql = "Select a.ID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSeries,a.ProductID, b.ProdName, b.ProdLoosePack, b.ProdPack, b.ProdCompShortName  from vouchercorrectioninrate a inner join masterproduct b on a.Productid = b.ProductId order by a.VoucherNumber desc";

            //string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' && voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber desc";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        public DataRow GetLastVoucherNumber(string vouType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select VoucherNumber from vouchercorrectioninrate order by VoucherNumber desc";

                // string strSql = "Select Vouchernumber from vouchercreditdebitnote where  VoucherType =  '" + vouType + "'  &&  VoucherSeries = '" + vouSeries + "' order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetFirstRecord(string vouType, string vouSeries)
        {
            DataRow dRow = null;
           string strSql = "Select a.ID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.VoucherSeries,a.ProductID, b.ProdName, b.ProdLoosePack, b.ProdPack, b.ProdCompShortName  from vouchercorrectioninrate a inner join masterproduct b on a.Productid = b.ProductId order by a.VoucherNumber";

            //string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' && voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber desc";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }
       }
    }
    


