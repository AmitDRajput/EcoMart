using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    class DBSsStock
    {
        # region Get
        //public DataTable GetStockByProductID(string prodID)
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = string.Format("Select ProductId,BatchNumber,Expiry,ExpiryDate,TradeRate,PurchaseRate,MRP,SaleRate,OpeningStock,ClosingStock,PurchaseStock,TransferInStock,CreditNoteStock,SaleStock,TransferOutStock,DebitNoteStock,PurchaseSchemeStock,PurchaseReplacementStock,SaleSchemeStock,IfRateCorrection,ProductVATPercent,PurchaseVATPercent,ProdCST,CompanyId,LastPurchaseAccountId,LastPurchasePartyShortName,LastPurchaseBillNumber,LastPurchaseDate,LastPurchaseVoucherNumber,ScanCode,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId from tblstock where ProductID = '{0}'", prodID);
        //    dtable = DBInterface.SelectDataTable(strSql);
        //    return dtable;
        //}
        public DataTable GetStockByProductIDForPurchase(string prodID, int mclstock)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select a.StockID,a.ProductId,a.BatchNumber,a.Expiry,a.TradeRate,a.PurchaseRate,a.MRP,a.SaleRate,a.ClosingStock,a.PurchaseVATPercent,a.ScanCode,b.AccountId,b.AccName from tblstock a left outer join  masteraccount b on a.LastPurchaseAccountId = b.AccountID  where a.ProductID = '{0}' AND closingstock >= "+ mclstock +" order by closingstock desc", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetStockByProductIDForDistributorSale(string prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select a.StockID,a.ProductId,a.BatchNumber,a.Expiry,a.ExpiryDate,a.TradeRate,a.PurchaseRate,a.MRP ,a.Salerate,c.ProdLoosePack, floor(a.ClosingStock/c.Prodloosepack) as ClosingStock,a.PurchaseVATPercent,a.ScanCode,b.AccountId,b.AccName from tblstock a left outer join  masteraccount b on a.LastPurchaseAccountId = b.AccountID  inner join masterproduct c on a.ProductID = c.ProductID where a.ProductID = '{0}'", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetStockByProductIDForSale(string prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select a.StockID,a.ProductId,a.BatchNumber,a.Expiry,a.ExpiryDate,a.TradeRate,a.PurchaseRate,a.MRP ,a.DistributorSalerate as SaleRate,c.ProdLoosePack, a.ClosingStock,a.PurchaseVATPercent,a.ScanCode,b.AccountId,b.AccName from tblstock a left outer join  masteraccount b on a.LastPurchaseAccountId = b.AccountID  inner join masterproduct c on a.ProductID = c.ProductID where a.ProductID = '{0}'", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        // ss 12/6/2015
        //public DataTable GetStockByProductIDForDBCRNote(string prodID)
        //{
        //    DataTable dtable = new DataTable();
        //     // string strSql = string.Format("Select  BatchNumber,Expiry,MRP,PurchaseRate,SaleRate,ClosingStock,ProductVATPercent,ExpiryDate,TradeRate,StockID from tblstock where ProductID = '{0}'", prodID);
        //    string strSql = string.Format("Select  a.BatchNumber,a.Expiry,a.MRP,a.PurchaseRate,a.SaleRate,a.DistributorSaleRate,a.ClosingStock,floor(a.ClosingStock/c.ProdLoosePack) as ClosingStockPack,a.ProductVATPercent,a.ExpiryDate,a.TradeRate,a.StockID,a.LastPurchaseDate,b.AccName from tblstock a left outer join masteraccount b on a.LastPurchaseAccountId = b.AccountID inner join masterproduct c on a.ProductID = c.ProductID where a.ProductID = '{0}'", prodID);
        //   //   string strSql = string.Format("Select  a.BatchNumber,a.Expiry,a.MRP,a.PurchaseRate,a.SaleRate,a.ClosingStock,a.ProductVATPercent,a.ExpiryDate,a.TradeRate,a.StockID,b.AccShortName from tblstock a left outer join masteraccount b on a.LastPurchaseAccountId = b.AccountID where a.ProductID = '{0}'", prodID);
        //    dtable = DBInterface.SelectDataTable(strSql);
        //    return dtable;
        //}

        public DataTable GetStockByProductIDForDBCRNote(string prodID)
        {
            DataTable dtable = new DataTable();
            // string strSql = string.Format("Select  BatchNumber,Expiry,MRP,PurchaseRate,SaleRate,ClosingStock,ProductVATPercent,ExpiryDate,TradeRate,StockID from tblstock where ProductID = '{0}'", prodID);
          // removed by ss  string strSql = string.Format("Select  a.BatchNumber,a.Expiry,a.MRP,a.PurchaseRate,a.SaleRate,a.ClosingStock,a.ClosingStock,a.ProductVATPercent,a.ExpiryDate,a.TradeRate,a.StockID,a.LastPurchaseDate,b.AccName As PartyName,a.LastPurchaseBillNumber As BillNo from tblstock a left outer join masteraccount b on a.LastPurchaseAccountId = b.AccountID inner join masterproduct c on a.ProductID = c.ProductID where a.ProductID = '{0}'order by  voucherdate)", prodID);

            string strSql = string.Format("Select  a.BatchNumber,a.Expiry,a.MRP,a.PurchaseRate,a.SaleRate,a.ClosingStock,a.ProductVATPercent,a.ExpiryDate,a.TradeRate,a.StockID,a.LastPurchaseDate,'' as PartyName,a.LastPurchaseBillNumber As BillNo from tblstock a inner join masterproduct c on a.ProductID = c.ProductID where a.ProductID = '{0}'", prodID);
            //   string strSql = string.Format("Select  a.BatchNumber,a.Expiry,a.MRP,a.PurchaseRate,a.SaleRate,a.ClosingStock,a.ProductVATPercent,a.ExpiryDate,a.TradeRate,a.StockID,b.AccShortName from tblstock a left outer join masteraccount b on a.LastPurchaseAccountId = b.AccountID where a.ProductID = '{0}'", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetStockByStockIDForDBCRNote(string stockID)
        {
            DataTable dtable = new DataTable();
            // string strSql = string.Format("Select  BatchNumber,Expiry,MRP,PurchaseRate,SaleRate,ClosingStock,ProductVATPercent,ExpiryDate,TradeRate,StockID from tblstock where ProductID = '{0}'", prodID);
            string strSql = string.Format("Select  a.BatchNumber,a.Expiry,a.MRP,a.PurchaseRate,a.SaleRate,a.DistributorSaleRate,a.ClosingStock,floor(a.ClosingStock/c.ProdLoosePack) as ClosingStockPack,a.ProductVATPercent,a.ExpiryDate,a.TradeRate,a.StockID,a.LastPurchaseDate,b.AccName from tblstock a left outer join masteraccount b on a.LastPurchaseAccountId = b.AccountID inner join masterproduct c on a.ProductID = c.ProductID where a.StockID = '{0}'", stockID);
            //   string strSql = string.Format("Select  a.BatchNumber,a.Expiry,a.MRP,a.PurchaseRate,a.SaleRate,a.ClosingStock,a.ProductVATPercent,a.ExpiryDate,a.TradeRate,a.StockID,b.AccShortName from tblstock a left outer join masteraccount b on a.LastPurchaseAccountId = b.AccountID where a.ProductID = '{0}'", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        // ss 12/6/2015


        //public DataTable GetStockByProductIDForSale(string prodID)
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = string.Format("Select  BatchNumber,Expiry,MRP,PurchaseRate,SaleRate,ClosingStock,ProductVATPercent,ExpiryDate,TradeRate,StockID from tblstock where ProductID = '{0}' && ExpiryDate > '" + DateTime.Today.Date.ToString("yyyyMMdd") + "'", prodID);
        //    dtable = DBInterface.SelectDataTable(strSql);
        //    return dtable;
        //}
        public DataTable GetStockByProductIDForFill(string prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select * from tblstock a  inner join  masterproduct b on a.ProductID = b.ProductID left outer join mastershelf c on b.ProdShelfID = c.ShelfID  where a.ProductID = '{0}' && ( ExpiryDate > '" + DateTime.Today.Date.ToString("yyyyMMdd") + "' || ExpiryDate = '' )", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        //public DataTable GetValidBatchesByProductID(string prodID)
        //{
        //    DataTable dtable = new DataTable();
        //    string strSql = string.Format("Select StockID,ProductId,BatchNumber,Expiry,ExpiryDate,TradeRate,PurchaseRate,MRP,SaleRate,OpeningStock,ClosingStock,PurchaseStock,TransferInStock,CreditNoteStock,SaleStock,TransferOutStock,DebitNoteStock,PurchaseSchemeStock,PurchaseReplacementStock,SaleSchemeStock,IfRateCorrection,ProductVATPercent,PurchaseVATPercent,ProdCST,CompanyId,LastPurchaseAccountId,LastPurchasePartyShortName,LastPurchaseBillNumber,LastPurchaseDate,LastPurchaseVoucherNumber,ScanCode,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId from tblstock where ProductID = '{0}' AND ClosingStock > 0 AND STR_TO_DATE(ExpiryDate,'%Y%c%d') >= CURDATE() ORDER BY STR_TO_DATE(ExpiryDate,'%Y%c%d')", prodID);
        //    dtable = DBInterface.SelectDataTable(strSql);
        //    return dtable;
        //}
        //public DataRow GetStockByProductIDAndBatchNumber(string productID, string batchID)
        //{
        //    DataRow dataRow = null;
        //    string strSql = string.Format("Select StockID,ProductId,BatchNumber,Expiry,ExpiryDate,TradeRate,PurchaseRate,MRP,SaleRate,OpeningStock,ClosingStock,PurchaseStock,TransferInStock,CreditNoteStock,SaleStock,TransferOutStock,DebitNoteStock,PurchaseSchemeStock,PurchaseReplacementStock,SaleSchemeStock,IfRateCorrection,ProductVATPercent,PurchaseVATPercent,ProdCST,CompanyId,LastPurchaseAccountId,LastPurchasePartyShortName,LastPurchaseBillNumber,LastPurchaseDate,LastPurchaseVoucherNumber,ScanCode,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId from tblstock where ProductID = '{0}' And BatchNumber = '{1}'", productID, batchID);
        //    dataRow = DBInterface.SelectFirstRow(strSql);
        //    return dataRow;
        //}

        public DataRow GetRecordByProductIDAndBatchNumberAndMRP(string stockID, int clstock)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select * from tblstock where StockID = '{0}' && ClosingStock >= {1} ", stockID, clstock);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }
        public DataRow GetRecordByProductIDAndBatchNumberAndMRP(string productID, string batchID, double mrp)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select * from tblstock where ProductID = '{0}' && BatchNumber = '{1}' && MRP = {2} ", productID, batchID, mrp);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }
        public DataRow GetRecordByProductIDAndBatchNumberAndMRPAndStockID(string productID, string batchID, double mrp, string stockID)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select * from tblstock where ProductID = '{0}' && BatchNumber = '{1}' && MRP = {2} && StockID = '{3}'", productID, batchID, mrp, stockID);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }

        public DataRow GetRecordByProductIDAndBatchNumberAndMRPAndStockID_stock(string productID, string batchID, double mrp, string accountId) //Amar
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select * from tblstock where ProductID = '{0}' && BatchNumber = '{1}' && MRP = {2} && LastPurchaseAccountId = '{3}'", productID, batchID, mrp, accountId);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }
        public DataRow GetRecord_oldaccountID(string stockID) //Amar
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select * from tblstock where StockID = '{0}'", stockID);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }
        public DataRow GetRecordByStockID(string stockID)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select * from tblstock where StockID = '{0}'", stockID);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }
        public DataRow GetCurrentClosingStockByThisStockID(String thisStockID)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select closingstock from tblstock where StockID = '{0}'", thisStockID);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }
        public DataRow GetRecordByProductBatchMRP(string productID, string batchID, double mrp)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select * from tblstock where ProductID = '{0}' && BatchNumber = '{1}' && MRP = '{2}'  ", productID, batchID, mrp);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }
        //public DataRow GetRecordByStockID(string StockID)
        //{
        //    DataRow dataRow = null;
        //    string strSql = string.Format("Select * from tblstock where StockID = '{0}' ", StockID);
        //    dataRow = DBInterface.SelectFirstRow(strSql);
        //    return dataRow;
        //}

        public int GetStockByStockID(string StockID)
        {
            int stockquantity = 0;
            DataRow dataRow = null;
            string strSql = string.Format("Select closingstock from tblstock where StockID = '{0}' ", StockID);
            dataRow = DBInterface.SelectFirstRow(strSql);
            if (dataRow != null && dataRow["ClosingStock"] != null)
                stockquantity = Convert.ToInt32(dataRow["ClosingStock"].ToString());
            return stockquantity;
        }
        public DataTable GetStockforCheck()
        {
            DataTable dt = null;
            string strSql = "Select * from tblstock";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataRow GetDataForAddToStock(string productID)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select ProductID,ProdIfShortListed,ProdClosingStock from masterproduct where ProductID = '{0}'", productID);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }

        public DataTable GetStockForAll(string compID)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,sum(a.OpeningStock) as OpeningStock,sum(a.ClosingStock) as ClosingStock,b.ProdCompID, b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,c.ShelfID,c.ShelfCode from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  left outer join  mastershelf c on b.ProdShelfID = c.ShelfID where b.ProdCompID = '" + compID + "' Group by a.ProductID order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetStockForAllWithOutZeroOpening(string compID)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,sum(a.OpeningStock) as OpeningStock,sum(a.ClosingStock) as ClosingStock,b.ProdCompID,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,c.ShelfID,c.ShelfCode from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastershelf c on b.ProdShelfID = c.ShelfID  where openingStock > 0  &&  b.ProdCompID = '" + compID + "'  Group by a.ProductID order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetStockForAllWithOutZeroClosing(string compID)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,sum(a.OpeningStock) as OpeningStock,sum(a.ClosingStock) as ClosingStock,b.ProdCompID,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,c.ShelfID,c.ShelfCode from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  left outer join  mastershelf c on b.ProdShelfID = c.ShelfID  where  ClosingStock > 0 && b.ProdCompID = '" + compID + "' Group by a.ProductID order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetStockForAllBatchOpening(string compID)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,a.OpeningStock,a.ClosingStock,a.BatchNumber,a.MRP,b.ProdCompID,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,c.ShelfID,c.ShelfCode from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastershelf c on b.ProdShelfID = c.ShelfID  where openingStock > 0 && b.ProdCompID = '" + compID + "'  order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetStockForAllBatchClosing(string compID)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,a.OpeningStock,a.ClosingStock,a.BatchNumber,a.MRP,b.ProdCompID, b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,c.ShelfID,c.ShelfCode from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastershelf c on b.ProdShelfID = c.ShelfID  where  ClosingStock > 0 && b.ProdCompID = '" + compID + "' order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetStockByCompanyID(string compid)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,sum(a.OpeningStock) as OpeningStock,sum(a.ClosingStock) as ClosingStock,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,b.ProdCompID,c.ShelfID,c.ShelfCode from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastershelf c on b.ProdShelfID = c.ShelfID  where b.ProdCompID = '" + compid + "' Group by a.ProductID order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetStockByCompanyIDWithOutZeroOpening(string compid)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,sum(a.OpeningStock) as OpeningStock,sum(a.ClosingStock) as ClosingStock,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,b.ProdCompID,c.ShelfID,c.ShelfCode from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastershelf c on b.ProdShelfID = c.ShelfID  where b.ProdCompID = '" + compid + "' && OpeningStock > 0  Group by a.ProductID order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetStockByCompanyIDWithOutZeroClosing(string compid)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,sum(a.OpeningStock) as OpeningStock,sum(a.ClosingStock) as ClosingStock,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,b.ProdCompID,c.ShelfID,c.ShelfCode from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastershelf c on b.ProdShelfID = c.ShelfID  where b.ProdCompID = '" + compid + "' && ClosingStock > 0  Group by a.ProductID order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetStockByCompanyIDBatchOpening(string compid)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,a.BatchNumber,a.MRP,a.OpeningStock,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,b.ProdCompID,c.ShelfID,c.ShelfCode from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastershelf c on b.ProdShelfID = c.ShelfID  where b.ProdCompID = '" + compid + "' && OpeningStock > 0   order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetStockByCompanyIDBatchClosing(string compid)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,a.BatchNumber,a.MRP,a.ClosingStock,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,b.ProdCompID,c.ShelfID,c.ShelfCode from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastershelf c on b.ProdShelfID = c.ShelfID  where b.ProdCompID = '" + compid + "' && ClosingStock > 0   order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetOverviewDataShelfWise(string shelfid)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,a.BatchNumber,a.Expiry,a.ExpiryDate,a.MRP,a.ClosingStock,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,b.ProdCompID,c.ShelfID,c.ShelfCode,(a.ClosingStock*a.MRP)/b.ProdLoosePack as Amount from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastershelf c on b.ProdShelfID = c.ShelfID  where b.ProdShelfID = '" + shelfid + "' && ClosingStock > 0   order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetOverviewDataNonMoving(string mdate)
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,a.BatchNumber,a.MRP,sum(a.ClosingStock) as ClosingStock,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdShelfID,b.ProdCompShortName,b.ProdCompID,b.ProdLastPurchasePartyId,c.ShelfID,c.ShelfCode,(ClosingStock*a.MRP)/b.ProdLoosePack as Amount,d.AccountID,d.AccName from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastershelf c on b.ProdShelfID = c.ShelfID inner join  masteraccount d on b.ProdLastPurchasePartyId = d.AccountID " + " where ClosingStock > 0 && ( b.ProdLastPurchaseDate < '" + mdate + "' && b.ProdLastSaleDate < '" + mdate + "') Group by a.ProductID  order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetOverViewStocknSale(string mcompID)
        {
            DataTable dt = null;
            string strSql = "";
            //   bool bRetValue = false;    
            //  strSql = "update masterproduct set SSOpeningStock = 0,SSPurchaseStock = 0, SSSaleStock = 0, SSCRStock = 0, SSDRStock = 0";
            //   bRetValue = (DBInterface.ExecuteQuery(strSql) > 0);
            //  strSql = "Select distinct ProductID,ProdName,ProdLoosePack,ProdPack,ProdCompID,ProdOpeningStock,SSOpeningStock,SSPurchaseStock,SSSaleStock,SSCRStock,SSDRStock from masterproduct  where prodCompID = '"+ mcompID + "' order by ProdName " ;
            strSql = "Select a.ProductID,a.ProdName,a.ProdLoosePack,a.ProdPack,a.ProdCompID,a.ProdCompShortName,sum(b.openingstock) as ProdOpeningStock,b.MRP from masterproduct a inner join tblstock b on a.ProductID = b.ProductID  where a.prodCompID = '" + mcompID + "' group by b.ProductID order by ProdName ";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetOpeningStockForStocknSale(string mcompID)
        {
            
            DataTable dt = null;
            string strSql = "";
            strSql = "Select b.ProductID, sum(b.OpeningStock) as OpeningStock,sum(b.closingstock) as closingstock, c.productID,c.prodcompID,sum(b.openingstock * (b.mrp/c.ProdLoosePack)) as OpStkValue,sum(b.closingstock * (b.mrp/c.ProdLoosePack)) as CLStkValue,(b.mrp) as mrp   from tblstock b  inner join masterproduct c  on b.productID = c.ProductID  where  c.ProdCompID = '" + mcompID + "' group by b.ProductId";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataRow GetOpendingStockByProductID(string prodID)
        {
            DataRow dt = null;
            string strSql = "";
            strSql = "Select ProdOpeningStock from masterproduct where  ProductID = '" + prodID + "'";
            dt = DBInterface.SelectFirstRow(strSql);
            return dt;
        }
        public DataTable GetPurchaseStockForStocknSale(string mtodate, string mcompID)
        {
            DataTable dt = null;
            string strSql = "";
            //  strSql = "Select a.PurchaseID,a.VoucherDate,b.PurchaseID,b.ProductID ,sum(b.Quantity*c.ProdLoosePack) as Quantity,sum(b.Schemequantity*c.ProdLoosePack) as Schemequantity,sum(b.ReplacementQuantity) as ReplacementQuantity,c.productID,c.prodcompID  from voucherpurchase a inner join detailpurchase b on a.purchaseid = b.purchaseid inner join masterproduct c  on b.productID = c.ProductID  where a.voucherdate <='" + mtodate + "' && c.ProdCompID = '" + mcompID + "' group by b.ProductID";

            strSql = "Select a.PurchaseID,a.VoucherDate,b.PurchaseID,b.ProductID ,(b.Quantity*c.ProdLoosePack) as Quantity,(b.Schemequantity*c.ProdLoosePack) as Schemequantity,(b.ReplacementQuantity) as ReplacementQuantity,(b.Quantity*b.mrp) as purstkValue , c.productID,c.prodcompID  from voucherpurchase a inner join detailpurchase b on a.purchaseid = b.purchaseid inner join masterproduct c  on b.productID = c.ProductID  where a.voucherdate <='" + mtodate + "' && c.ProdCompID = '" + mcompID + "'";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetSaleStockForStocknSale(string mtodate, string mcompID)
        {
            DataTable dt = null;
            string strSql = "";
            //strSql = "Select a.ID,a.VoucherDate,b.MasterSaleID,b.ProductID,sum(b.Quantity) as Quantity,sum(b.SchemeQuantity) as SchemeQuantity,c.productID, c.ProdCompID  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID inner join masterproduct c on b.ProductID = c.ProductID  where a.voucherdate <='" + mtodate + "' &&  c.ProdCompID = '" + mcompID + "' group by b.ProductID";

            strSql = "Select a.ID,a.VoucherDate,b.MasterSaleID,b.ProductID,(b.Quantity) as Quantity,(b.SchemeQuantity) as SchemeQuantity,(b.Quantity * (b.SaleRate/c.Prodloosepack)) as Amount,c.productID, c.ProdCompID  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID inner join masterproduct c on b.ProductID = c.ProductID  where a.voucherdate <='" + mtodate + "' &&  c.ProdCompID = '" + mcompID + "'" ;
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }


        public DataTable GetCRSTIStockForStocknSale(string mtodate, string mcompID)
        {
            DataTable dt = null;
            string strSql = "";
            //  strSql = "Select a.CRDBID,a.VoucherDate,b.MasterID,b.ProductID,sum(b.Quantity) as Quantity,sum(b.SchemeQuantity) as SchemeQuantity,c.productID, c.ProdCompID  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate <='" + mtodate + "' &&  c.ProdCompID = '" + mcompID + "' && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') group by b.ProductID";
            strSql = "Select a.CRDBID,a.VoucherDate,b.MasterID,b.ProductID,(b.Quantity),(b.SchemeQuantity),c.productID, c.ProdCompID  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate <='" + mtodate + "' &&  c.ProdCompID = '" + mcompID + "' && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "')";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetDBSTOStockForStocknSale(string mtodate, string mcompID)
        {
            DataTable dt = null;
            string strSql = "";
            //   strSql = "Select a.CRDBID,a.VoucherDate,b.MasterID,b.ProductID,sum(b.Quantity*c.ProdLoosePack) as Quantity,sum(b.Schemequantity*c.ProdLoosePack) as Schemequantity  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID inner join masterproduct c on b.productId = c.ProductId where a.voucherdate <='" + mtodate + "'  &&  (a.VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteStock + "')  group by b.ProductID"+
            //            " union Select a.CRDBID,a.VoucherDate,b.MasterID,b.ProductID,sum(b.Quantity) as Quantity,sum(b.Schemequantity) as Schemequantity  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID where a.voucherdate <='" + mtodate + "'  &&  (a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "') group by b.ProductID";

            strSql = "Select a.CRDBID,a.VoucherDate,b.MasterID,b.ProductID,(b.Quantity*c.ProdLoosePack) as Quantity,(b.Schemequantity*c.ProdLoosePack) as Schemequantity  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID inner join masterproduct c on b.productId = c.ProductId where a.voucherdate <='" + mtodate + "'  &&  (a.VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteStock + "')" +
                    " union Select a.CRDBID,a.VoucherDate,b.MasterID,b.ProductID,(b.Quantity) as Quantity,(b.Schemequantity) as Schemequantity  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID where a.voucherdate <='" + mtodate + "'  &&  (a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "')";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetDataForProductLedger(string mproductid, string mtodate)
        {
            DataTable dt = null;
            string strSql = "";
            if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                //strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' union " +
                //         "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,(b.Quantity*d.ProdLoosePack) as QuantityOUT,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' union " +
                //         "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' union " +
                //         "Select a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
                //         "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
                //         "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (b.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
                //         "Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' order by VoucherDate";


                  strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' union " +
                   "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,(b.Quantity*d.ProdLoosePack) as QuantityOUT,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' union " +
                   "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' union " +
                   "Select distinct a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
                 //  "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,(b.SchemeQuantity*c.ProdLoosePack) as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
                   "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
                 //  "Select all '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' union " +
                  "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber, newBatch, newquantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate where  voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'  union " +
                  "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber, oldBatch, ' ' as QuantityIN, '' as SchemeQuantityIN, newQuantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate  where voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'  union " +
                  "Select ' ' as ID,' ' as VoucherDate, 'OPS' VoucherType,'' as VoucherSubtype, ' ' as VoucherNumber, BatchNumber, openingStock as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from tblstock  where  ProductID = '" + mproductid + "'  && openingStock > 0   "+" order by voucherdate ";
                 // "Select a.MasterID as ID,a.VoucherDate,a.VoucherType,' ' as VoucherSubtype,a.VoucherNumber,b.BatchNumber,quantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT,' ' as AccountID, ' ' as AccName  from voucheropstock a inner join detailopstock b on a.MasterID = b.MasterID  inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' order by voucherdate ";
            else
                strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' union " +
                   "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' union " +
                   "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' union " +
                   "Select distinct a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
                //   "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
                   "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
                   //"Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' union " +
                  "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber, newBatch, newquantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate where  voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'  union " +
                  "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber, oldBatch, ' ' as QuantityIN, '' as SchemeQuantityIN, newQuantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate  where voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'  union " +
                  "Select ' ' as ID,' ' as VoucherDate, 'Beginning' VoucherType,'' as VoucherSubtype, ' ' as VoucherNumber, BatchNumber, openingStock as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from tblstock  where  ProductID = '" + mproductid + "'  && openingStock > 0   union " +
                  "Select a.MasterID as ID,a.VoucherDate,a.VoucherType,' ' as VoucherSubtype,a.VoucherNumber,b.BatchNumber,quantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT,' ' as AccountID, ' ' as AccName  from voucheropstock a inner join detailopstock b on a.MasterID = b.MasterID  inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' order by voucherdate ";

            //strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "' && a.VoucherDate >='20160331' && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' union " +
            //  "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "' && a.VoucherDate >='20160331' && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' union " +
            //  "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "' && a.VoucherDate >='20160331' &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' union " +
            //  "Select distinct a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "' && a.VoucherDate >='20160331' && b.ProductID = '" + mproductid + "' union " +
            //  "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'&& a.VoucherDate >='20160331'  && b.ProductID = '" + mproductid + "' union " +
            //  "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
            // //"Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' union " +
            // "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber, newBatch, newquantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate where  voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'  union " +
            // "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber, oldBatch, ' ' as QuantityIN, '' as SchemeQuantityIN, newQuantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate  where voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'  union " +
            // "Select ' ' as ID,' ' as VoucherDate, 'Beginning' VoucherType,'' as VoucherSubtype, ' ' as VoucherNumber, BatchNumber, openingStock as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from tblstock  where  ProductID = '" + mproductid + "'  && openingStock > 0   union " +
            // "Select a.MasterID as ID,a.VoucherDate,a.VoucherType,' ' as VoucherSubtype,a.VoucherNumber,b.BatchNumber,quantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT,' ' as AccountID, ' ' as AccName  from voucheropstock a inner join detailopstock b on a.MasterID = b.MasterID  inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate <='" + mtodate + "'&& a.VoucherDate >='20160331'  && b.ProductID = '" + mproductid + "' order by voucherdate ";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetSaleDataForProductLeder(string mproductid, string mtodate)
        {
            DataTable dt = null;
            string strSql = "";

            if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                strSql = "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "'";
            else
                strSql = "Select  '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }


     public DataTable GetDataForClosingStockASOn(string mtodate) // [ansuman]
        {
            DataTable dt = null;
            string strSql = "";
            if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                strSql = "Select b.ProductID, b.StockID, '' as voucherDate, 'OPB' as voucherType, (b.OpeningStock) as QuantityIN, '' as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack,d.ProdLastPurchaseRate, d.ProdLastPurchaseTradeRate, d.ProdVATPercent  from tblstock b inner join masterproduct d on b.productID = d.ProductId  union all " +
           "Select  b.ProductID,b.StockID,a.VoucherDate, a.voucherType, (b.Quantity) as QuantityIN,(b.SchemeQuantity) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack,d.ProdLastPurchaseRate, d.ProdLastPurchaseTradeRate, d.ProdVATPercent  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID inner join masterproduct d on b.ProductID = d.ProductID  where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') union all " +
          "Select b.ProductID,b.StockID,a.VoucherDate, a.voucherType, '' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*d.ProdLoosePack) as QuantityOUT, (b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack,d.ProdLastPurchaseRate, d.ProdLastPurchaseTradeRate, d.ProdVATPercent from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) union all " +
          "Select b.ProductID,b.StockID,a.VoucherDate,a.voucherType, '' as QuantityIN, '' as SchemeQuantityIN,(b.Quantity) as QuantityOUT,(b.SchemeQuantity) as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack,d.ProdLastPurchaseRate, d.ProdLastPurchaseTradeRate, d.ProdVATPercent  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' union all " +
          "Select b.ProductID,b.StockID,a.VoucherDate,a.voucherType, ((b.Quantity*d.ProdLoosePack)) as QuantityIN,((b.SchemeQuantity*d.ProdLoosePack)) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack,d.ProdLastPurchaseRate, d.ProdLastPurchaseTradeRate, d.ProdVATPercent  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "' union all " +
        // "Select b.ProductID,b.StockID,a.VoucherDate,a.voucherType, '' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*d.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack,d.ProdLastPurchaseRate, d.ProdLastPurchaseTradeRate, d.ProdVATPercent  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct d on b.ProductID = d.ProductID where (a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "' union all " +
         "Select b.ProductID,b.StockID,a.VoucherDate,a.voucherType, '' as QuantityIN, '' as SchemeQuantityIN,( b.Quantity) as QuantityOUT,'' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack,d.ProdLastPurchaseRate, d.ProdLastPurchaseTradeRate, d.ProdVATPercent from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID inner join masterproduct d on b.ProductID = d.ProductID where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "' union all " +
         "Select b.ProductID,b.StockID,voucherDate,'SVU'as vouchertype, '' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity) as QuantityOUT,'' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack, d.ProdLastPurchaseRate, d.ProdLastPurchaseTradeRate, d.ProdVATPercent   from detailsale b inner join masterproduct d on b.ProductID = d.ProductID  where (b.VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && b.voucherdate <='" + mtodate + "'" +
         " order by prodname ";

            //strSql = "Select b.ProductID, b.StockID, '' as voucherDate, 'OPB' as voucherType, (b.OpeningStock) as QuantityIN, '' as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack  from tblstock b inner join masterproduct d on b.productID = d.ProductId  union " +
            //  "Select  b.ProductID,b.StockID,a.VoucherDate, a.voucherType, (b.Quantity) as QuantityIN,(b.SchemeQuantity) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack   from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID inner join masterproduct d on b.ProductID = d.ProductID  where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') union   " +
            // "Select b.ProductID,b.StockID,a.VoucherDate, a.voucherType, '' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*d.ProdLoosePack) as QuantityOUT, (b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) union " +
            // "Select b.ProductID,b.StockID,a.VoucherDate,a.voucherType, '' as QuantityIN, '' as SchemeQuantityIN,(b.Quantity) as QuantityOUT,(b.SchemeQuantity) as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' union " +
            // "Select b.ProductID,b.StockID,a.VoucherDate,a.voucherType, ((b.Quantity*d.ProdLoosePack)) as QuantityIN,((b.SchemeQuantity*d.ProdLoosePack)) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "' union " +
            //"Select b.ProductID,b.StockID,a.VoucherDate,a.voucherType, '' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*d.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct d on b.ProductID = d.ProductID where (a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "' union " +
            //"Select b.ProductID,b.StockID,a.VoucherDate,a.voucherType, '' as QuantityIN, '' as SchemeQuantityIN,( b.Quantity) as QuantityOUT,'' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID inner join masterproduct d on b.ProductID = d.ProductID where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "' union " +
            //"Select b.ProductID,b.StockID,voucherDate,'SVU'as vouchertype, '' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity) as QuantityOUT,'' as SchemeQuantityOUT,d.Prodname,d.Prodpack,d.Prodloosepack   from detailsale b inner join masterproduct d on b.ProductID = d.ProductID  where (b.VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && b.voucherdate <='" + mtodate + "'" +
            //" order by prodname ";

            //strSql = "Select b.ProductID, a.VoucherType,sum(b.Quantity) as QuantityIN, sum(b.SchemeQuantity) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') group by b.ProductID  union " +
            //         "Select b.ProductID,a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,(b.Quantity*d.ProdLoosePack) as QuantityOUT,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' )  union " +
            //         "Select  b.ProductID,a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' union " +
            //         "Select  b.ProductID,a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "' union " +
            //         "Select b.ProductID, a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  union " +
            //         "Select b.ProductID, a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (b.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "' union " +
            //         "Select ProductID, '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  order by VoucherDate";
            else
                strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "')  union all" +
                   "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) union all" +
                   "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' union all" +
                   "Select a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "' union all" +
                //   "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "' union all" +
                   "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "' union all" +
                   "Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  order by VoucherDate";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        //public DataTable GetDataForProductLedgerBatchWise(string mproductid, string mtodate, string mbatchnumber)
        //{
        //    DataTable dt = null;
        //    string strSql = "";
        //    if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
        //        strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //                 "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,(b.Quantity*d.ProdLoosePack) as QuantityOUT,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //                 "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //                 "Select a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //                 "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //                 "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (b.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //                 "Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && BatchNumber = '" + mbatchnumber + "' order by VoucherDate";
        //    else
        //        strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //           "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //           "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //           "Select a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //           "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //           "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (b.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.BatchNumber = '" + mbatchnumber + "' union " +
        //          "Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && BatchNumber = '" + mbatchnumber + "' order by VoucherDate";
        //    dt = DBInterface.SelectDataTable(strSql);
        //    return dt;
        //}
        public DataTable GetDataForProductLedgerStockIDWise(string mproductid, string mtodate, string mstockID)
        {
            DataTable dt = null;
            string strSql = "";
            if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
            ////  strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' union " +
            ////       "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,(b.Quantity*d.ProdLoosePack) as QuantityOUT,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' union " +
            ////       "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' union " +
            ////       "Select a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
            ////       "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,(b.SchemeQuantity*c.ProdLoosePack) as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
            ////       "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
            ////     //  "Select all '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' union " +
            ////      "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber, newBatch, newquantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate where  voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'  union " +
            ////      "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber, oldBatch, ' ' as QuantityIN, '' as SchemeQuantityIN, newQuantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate  where voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'  union " +
            ////      "Select ' ' as ID,' ' as VoucherDate, 'OPS' VoucherType,'' as VoucherSubtype, ' ' as VoucherNumber, BatchNumber, openingStock as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from tblstock  where  ProductID = '" + mproductid + "'  && openingStock > 0   "+" order by voucherdate ";
            ////     // "Select a.MasterID as ID,a.VoucherDate,a.VoucherType,' ' as VoucherSubtype,a.VoucherNumber,b.BatchNumber,quantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT,' ' as AccountID, ' ' as AccName  from voucheropstock a inner join detailopstock b on a.MasterID = b.MasterID  inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' order by voucherdate ";
            ////else
            ////    strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' union " +
            ////       "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' union " +
            ////       "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' union " +
            ////       "Select a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
            ////       "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || b.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
            ////       "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' union " +
            ////       //"Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' union " +
            ////      "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber, newBatch, newquantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate where  voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'  union " +
            ////      "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber, oldBatch, ' ' as QuantityIN, '' as SchemeQuantityIN, newQuantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate  where voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "'  union " +
            ////      "Select ' ' as ID,' ' as VoucherDate, 'Beginning' VoucherType,'' as VoucherSubtype, ' ' as VoucherNumber, BatchNumber, openingStock as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from tblstock  where  ProductID = '" + mproductid + "'  && openingStock > 0   union " +
            ////      "Select a.MasterID as ID,a.VoucherDate,a.VoucherType,' ' as VoucherSubtype,a.VoucherNumber,b.BatchNumber,quantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT,' ' as AccountID, ' ' as AccName  from voucheropstock a inner join detailopstock b on a.MasterID = b.MasterID  inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' order by voucherdate ";
            ////dt = DBInterface.SelectDataTable(strSql);

                strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID, b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                         "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,(b.Quantity*d.ProdLoosePack) as QuantityOUT,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                         "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                         "Select a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                       //  "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                         "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                    // "Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,StockID,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && StockID = '" + mstockID + "' union " +
                          "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,newStockID as StockID, newBatch, newquantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate where  voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && NewStockID = '" + mstockID + "' union " +
                          "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,oldStockID as StockID, oldBatch, ' ' as QuantityIN, '' as SchemeQuantityIN, newQuantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate  where voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && oldStockID = '" + mstockID + "' union " +
                           "Select ' ' as ID,' ' as VoucherDate, 'OPS' VoucherType,'' as VoucherSubtype, ' ' as VoucherNumber,stockID, BatchNumber, openingStock as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from tblstock  where  ProductID = '" + mproductid + "' && StockID = '" + mstockID + "'  && openingStock > 0   " + " order by voucherdate ";
            else
                strSql = "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                   "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                   "Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                   "Select a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                 //  "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where ( "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                   "Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                    //  "Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,StockID,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && StockID = '" + mstockID + "' union " +
                   "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,newStockID as StockID, newBatch, newquantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate where  voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && NewStockID = '" + mstockID + "' union " +
                   "Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,oldStockID as StockID, oldBatch, ' ' as QuantityIN, '' as SchemeQuantityIN, newQuantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate  where voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && oldStockID = '" + mstockID + "' union " +
                    "Select ' ' as ID,' ' as VoucherDate, 'OPS' VoucherType,'' as VoucherSubtype, ' ' as VoucherNumber,StockID, BatchNumber, openingStock as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from tblstock  where  ProductID = '" + mproductid + "' && StockID = '" + mstockID + "' && openingStock > 0   " + " order by voucherdate ";
                  // "Select a.MasterID as ID,a.VoucherDate,a.VoucherType,' ' as VoucherSubtype,a.VoucherNumber,b.StockID,b.BatchNumber,quantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT,' ' as AccountID, ' ' as AccName  from voucheropstock a inner join detailopstock b on a.MasterID = b.MasterID  inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' order by voucherdate ";
            dt = DBInterface.SelectDataTable(strSql);


               

            return dt;
        }


        public DataTable GetSaleDataForProductLedgerBatchWise(string mproductid, string mtodate, string mstockID)
        {
            DataTable dt = null;
            string strSql = "";
            if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                strSql = //"Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID, b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                         //"Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,(b.Quantity*d.ProdLoosePack) as QuantityOUT,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                         //"Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                         //"Select a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                         //"Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                         //"Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                         "Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,StockID,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && StockID = '" + mstockID + "' " ;
                          //"Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,newStockID as StockID, newBatch, newquantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate where  voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && NewStockID = '" + mstockID + "' union " +
                          //"Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,oldStockID as StockID, oldBatch, ' ' as QuantityIN, '' as SchemeQuantityIN, newQuantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate  where voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && oldStockID = '" + mstockID + "' order by VoucherDate";
            else
                strSql = //"Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,b.Quantity as QuantityIN,b.SchemeQuantity as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID, c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "') && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                   //"Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && (a.VoucherType  = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ) && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                   //"Select a.CRDBID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN,b.Quantity as QuantityOUT,b.SchemeQuantity as SchemeQuantityOUT,a.AccountID,c.AccName  from vouchercreditdebitnote a inner join detailcreditdebitnotestock b on a.CRDBID = b.MasterID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockOut + "' && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                   //"Select a.PurchaseID as ID,a.VoucherDate,a.VoucherType,'' as VoucherSubType,a.VoucherNumber,b.StockID,b.BatchNumber,(b.Quantity*d.ProdLoosePack) as QuantityIN,(b.SchemeQuantity*d.ProdLoosePack) as SchemeQuantityIN, '' as QuantityOUT, '' as SchemeQuantityOUT,a.AccountID,c.AccName  from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID  inner join  masteraccount c on a.AccountID = c.AccountID inner join masterproduct d on b.ProductID = d.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                   //"Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, (b.Quantity*c.ProdLoosePack) as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  inner join masterproduct c on b.ProductID = c.ProductID where (a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCash + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCredit + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForDistributorSaleCreditStatement + "') &&  a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                   //"Select a.ID,a.VoucherDate,a.VoucherType,a.VoucherSubtype,a.VoucherNumber,b.StockID,b.BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, b.Quantity as QuantityOUT,'' as SchemeQuantityOUT,a.AccountID,a.PatientName as AccName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCashSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditSale + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "') && a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' union " +
                   "Select '' as ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,StockID,BatchNumber,'' as QuantityIN, '' as SchemeQuantityIN, Quantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from detailsale  where (VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "') && voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && StockID = '" + mstockID + "' " ;
                   //"Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,newStockID as StockID, newBatch, newquantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate where  voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && NewStockID = '" + mstockID + "' union " +
                   //"Select ID, VoucherDate,VoucherType,'' as VoucherSubtype,VoucherNumber,oldStockID as StockID, oldBatch, ' ' as QuantityIN, '' as SchemeQuantityIN, newQuantity as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from vouchercorrectioninrate  where voucherdate <='" + mtodate + "'  && ProductID = '" + mproductid + "' && oldStockID = '" + mstockID + "' union " +
                   //"Select ' ' as ID,' ' as VoucherDate, 'Beginning' VoucherType,'' as VoucherSubtype, ' ' as VoucherNumber, StockID, BatchNumber, beginningstock as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT, '' as AccountID,  '' as AccName  from tblstock  where  ProductID = '" + mproductid + "' && StockID = '" + mstockID + "' union " +
                   //"Select a.MasterID as ID,a.VoucherDate,a.VoucherType,' ' as VoucherSubtype,a.VoucherNumber,b.StockID,b.BatchNumber,quantity as QuantityIN, '' as SchemeQuantityIN, ' ' as QuantityOUT,'' as SchemeQuantityOUT,' ' as AccountID, ' ' as AccName  from voucheropstock a inner join detailopstock b on a.MasterID = b.MasterID  inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate <='" + mtodate + "'  && b.ProductID = '" + mproductid + "' && b.StockID = '" + mstockID + "' order by voucherdate ";
            dt = DBInterface.SelectDataTable(strSql);
            return dt; 
        }
        public DataTable GetProductCategorywise()
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,sum(a.ClosingStock* a.mrp / b.ProdLoosePack) as Amount,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCategoryID,c.ProductCategoryID,c.ProductCategoryName from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  masterproductCategory c on b.ProdCategoryID = c.ProductCategoryID   Group by c.ProductCategoryID order by c.ProductCategoryName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetProductCompanywise()
        {
            DataTable dt = null;
            string strSql = "Select a.ProductID,sum(a.ClosingStock* a.mrp / b.ProdLoosePack) as Amount,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompID,c.CompID,c.CompName from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastercompany c on b.ProdCompID = c.CompID   Group by c.CompID order by c.CompName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        //public DataTable GetOverviewPatientShortList(string cday)
        //{
        //    DataTable dt = null;
        //    int mday = 0;
        //    int.TryParse(cday, out mday);
        //    string strSql = "Select a.patientID,a.ProductID,sum(a.quantity) as RequiredQty,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName,b.ProdCompID,b.ProdClosingStock,b.ProdLastPurchaseMRP,b.ProdLastPurchasePartyId,c.PatientID,c.VisitDay1,c.VisitDay2,c.VisitDay3,b.ProdClosingStock- sum(a.Quantity) as Difference,d.AccountID,d.AccName,((sum(a.Quantity))/b.ProdLoosePack)*b.ProdLastPurchaseMRP as Amount   from linkpatientproduct a inner join masterproduct b on a.ProductID = b.ProductId  inner join  masterpatient c on a.patientID = c.PatientID   left outer join masteraccount d on b.ProdLastPurchasePartyId  = d.AccountId where  c.VisitDay1 = " + mday + " || c.VisitDay2 = " + mday + " || c.VisitDay3 = " + mday + "  Group by a.ProductID " +
        //         " union Select a.AccountID as PatientID,a.ProductID,sum(a.quantity) as RequiredQty,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName,b.ProdCompID,b.ProdClosingStock,b.ProdLastPurchaseMRP,b.ProdLastPurchasePartyId,c.AccountID as patientID,c.accdbVisitDay1 as visitday1,c.accdbVisitDay2 as visitday2,c.accdbVisitDay3 as visitday3,b.ProdClosingStock- sum(a.Quantity) as Difference,d.AccountID as patientID,d.AccName as patientName,((sum(a.Quantity))/b.ProdLoosePack)*b.ProdLastPurchaseMRP as Amount   from linkdebtorproduct a inner join masterproduct b on a.ProductID = b.ProductId  inner join  masteraccount c on a.AccountID = c.AccountID   left join masteraccount d on b.ProdLastPurchasePartyId  = d.AccountId where  c.accdbVisitDay1 = " + mday + " || c.accdbVisitDay2 = " + mday + " ||  c.accdbVisitDay3 = " + mday + "  Group by a.ProductID order by ProdName";
        //    dt = DBInterface.SelectDataTable(strSql);
        //    return dt;
        //}
        public DataTable GetOverviewDebtorShortList(string cday)
        {
            DataTable dt = null;
            int mday = 0;
            int.TryParse(cday, out mday);
            string strSql = "Select a.AccountID,a.ProductID,sum(a.quantity) as RequiredQty,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName,b.ProdCompID,b.ProdClosingStock,b.ProdLastPurchaseMRP,b.ProdLastPurchasePartyId,c.AccountID,c.AccDbVisitDay1,c.AccDbVisitDay2,c.AccDbVisitDay3,b.ProdClosingStock- sum(a.Quantity) as Difference,d.AccountID,d.AccName,((sum(a.Quantity))/b.ProdLoosePack)*b.ProdLastPurchaseMRP as Amount from linkdebtorproduct a inner join masterproduct b on a.ProductID = b.ProductId  inner join  masteraccount c on a.AccountID = c.AccountID   left join masteraccount d on b.ProdLastPurchasePartyId  = d.AccountId where  c.AccDbVisitDay1 = " + mday + " || c.AccDbVisitDay2 = " + mday + " || c.AccDbVisitDay3 = " + mday + "  Group by a.ProductID order by b.ProdName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        #endregion

        #region Read

        public DataTable ReadExpiredStock(string mdate) // [ansuman]
        {
            DataTable dtable = new DataTable();
            string strSql = "select c.ProductID, c.ProdName,c.ProdloosePack,c.ProdPack,c.ProdShelfID,a.StockID,a.BatchNumber,a.Expiry,a.MRP,a.ClosingStock,(a.ClosingStock*a.mrp/c.ProdLoosePack) as Amount,a.LastPurchaseAccountId as AccountID,a.ExpiryDate,a.PurchaseRate,a.SaleRate,a.TradeRate,a.LastPurchaseBillNumber,a.LastPurchaseDate,a.LastPurchaseVoucherType,a.LastPurchaseVoucherNumber,c.ProdVATPercent,d.ShelfCode, " +
                "b.accName,b.AccAddress1,b.AccAddress2,b.AccLessPercentInDebitNote from tblstock a left outer join masteraccount b  on a.LastPurchaseAccountId = b.AccountID  inner join masterproduct c on a.ProductID=c.ProductID left outer join mastershelf d  on c.ProdShelfId = d.ShelfID where (a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate <= '" + mdate + "' &&  a.ClosingStock > 0)  order by c.ProdName"; // c.ProdLoosePack
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable ReadExpiredProductData(string FromDate, string ToDate) // [ansuman] [17.4.2017]
        {
            DataTable dtable = new DataTable();
            string strSql = "select c.ProductID, c.ProdName,c.ProdloosePack,c.ProdPack,c.ProdShelfID,a.StockID,a.BatchNumber,a.Expiry,a.MRP,a.ClosingStock,(a.ClosingStock*a.mrp/c.ProdLoosePack) as Amount,a.LastPurchaseAccountId as AccountID,a.ExpiryDate,a.PurchaseRate,a.SaleRate,a.TradeRate,a.LastPurchaseBillNumber,a.LastPurchaseDate,a.LastPurchaseVoucherType,a.LastPurchaseVoucherNumber,c.ProdVATPercent,d.ShelfCode, " +
                "b.accName,b.AccAddress1,b.AccAddress2,b.AccLessPercentInDebitNote from tblstock a left outer join masteraccount b  on a.LastPurchaseAccountId = b.AccountID  inner join masterproduct c on a.ProductID=c.ProductID left outer join mastershelf d  on c.ProdShelfId = d.ShelfID where (a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate >= '" + FromDate + "' &&  a.expirydate <= '" + ToDate + "' &&  a.ClosingStock > 0)  order by c.ProdName"; // c.ProdLoosePack
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable ReadExpiredStockbtnDate(string FromDate, string ToDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "select c.ProductID, c.ProdName,c.ProdloosePack,c.ProdPack,c.ProdShelfID,a.StockID,a.BatchNumber,a.Expiry,a.MRP,a.ClosingStock,(a.ClosingStock*a.mrp/c.ProdLoosePack) as Amount,a.LastPurchaseAccountId as AccountID,a.ExpiryDate,a.PurchaseRate,a.SaleRate,a.TradeRate,a.LastPurchaseBillNumber,a.LastPurchaseDate,a.LastPurchaseVoucherType,a.LastPurchaseVoucherNumber,c.ProdVATPercent,d.ShelfCode, "+ 
                "b.accName,b.AccAddress1,b.AccAddress2 from tblstock a left outer join masteraccount b  on a.LastPurchaseAccountId = b.AccountID  inner join masterproduct c on a.ProductID=c.ProductID left outer join mastershelf d  on c.ProdShelfId = d.ShelfID where  a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate >= '" + FromDate + "' &&  a.expirydate <= '" + ToDate + "' &&  a.ClosingStock > 0  order by c.ProdName"; // c.ProdLoosePack
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadExpiredStockForStockOutExpiredProducts(string mdate)
        {
            DataTable dtable = new DataTable();
            string strSql = "select c.ProductID, c.ProdName,c.ProdloosePack,c.ProdPack,c.ProdShelfID,a.StockID,a.BatchNumber,a.Expiry,a.MRP,a.ClosingStock,a.LastPurchaseAccountId as AccountID,(a.ClosingStock*a.MRP)/c.ProdLoosePack  as Amount,a.ExpiryDate,a.PurchaseRate,a.SaleRate,a.TradeRate,a.LastPurchaseBillNumber,a.LastPurchaseDate,a.LastPurchaseVoucherType,a.LastPurchaseVoucherNumber,c.ProdVATPercent,d.ShelfCode from tblstock a   inner join masterproduct c on a.ProductID=c.ProductID left outer join mastershelf d  on c.ProdShelfId = d.ShelfID where  a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate <= '" + mdate + "' && a.ClosingStock > 0 order by c.ProdName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadExpiredStockForShelf(string FromDate, string ToDate, string mshelfID) // [ansuman]
        {
            DataTable dtable = new DataTable();
            string strSql = "select c.ProductID, c.ProdName,c.ProdloosePack,c.ProdPack,c.ProdShelfID,a.StockID,a.BatchNumber,a.Expiry,a.MRP,a.ClosingStock,a.LastPurchaseAccountId as AccountID,(a.ClosingStock*a.MRP)/c.ProdLoosePack  as Amount,a.ExpiryDate,a.PurchaseRate,a.SaleRate,a.TradeRate,a.LastPurchaseBillNumber,a.LastPurchaseDate,a.LastPurchaseVoucherType,a.LastPurchaseVoucherNumber,c.ProdVATPercent,d.ShelfCode, b.accName,b.AccAddress1,b.AccAddress2,b.AccLessPercentInDebitNote from tblstock a left outer join masteraccount b  on a.LastPurchaseAccountId = b.AccountID  inner join masterproduct c on a.ProductID=c.ProductID left outer join mastershelf d  on c.ProdShelfId = d.ShelfID where  a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate >= '" + FromDate + "' &&  a.expirydate <= '" + ToDate + "' &&  a.ClosingStock >= c.ProdLoosePack && c.ProdShelfID = '" + mshelfID + "' order by b.AccName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadExpiredStock(string mdate, string acID)
        {
            DataTable dtable = new DataTable();
            string strSql = "select c.ProductID, c.ProdName,c.ProdloosePack,c.ProdPack,c.ProdShelfID,c.ProdVatPercent as VatPer,c.ProdMaxLevel,c.ProdMinLevel,a.StockID,a.BatchNumber,a.Expiry,a.MRP,a.ClosingStock,a.LastPurchaseAccountId as AccountID,a.ExpiryDate,a.PurchaseRate,a.SaleRate,a.TradeRate,a.LastPurchaseBillNumber,a.LastPurchaseDate,a.LastPurchaseVoucherType,a.LastPurchaseVoucherNumber,c.ProdVATPercent,d.ShelfCode, b.accName,b.AccountID as AccID,b.AccAddress1,b.AccAddress2,b.AccLessPercentInDebitNote from tblstock a  inner join masteraccount b  on a.LastPurchaseAccountId = b.AccountID  inner join masterproduct c on a.ProductID=c.ProductID left outer join mastershelf d  on c.ProdShelfId = d.ShelfID where  a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate <= '" + mdate + "' &&  a.ClosingStock >= c.ProdLoosePack  && a.LastPurchaseAccountId = '" + acID + "'" +
                     " union select c.ProductID, c.ProdName,c.ProdloosePack,c.ProdPack,c.ProdShelfID,c.ProdVatPercent as VatPer,c.ProdMaxLevel,c.ProdMinLevel,a.StockID,a.BatchNumber,a.Expiry,a.MRP,a.ClosingStock,c.ProdPartyid_1 as AccountID,        a.ExpiryDate,a.PurchaseRate,a.SaleRate,a.TradeRate,a.LastPurchaseBillNumber,a.LastPurchaseDate,a.LastPurchaseVoucherType,a.LastPurchaseVoucherNumber,c.ProdVATPercent,d.ShelfCode, b.accName,b.AccountID as AccID,b.AccAddress1,b.AccAddress2,b.AccLessPercentInDebitNote from tblstock a  inner join masterproduct c on a.ProductID=c.ProductID left outer join mastershelf d  on c.ProdShelfId = d.ShelfID inner join masteraccount b on c.ProdPartyID_1 = b.AccountID where  a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate <= '" + mdate + "' &&  a.ClosingStock >= c.ProdLoosePack  && a.LastPurchaseAccountId = '' && c.ProdPartyID_1 = '" + acID + "'";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadExpiredStockForMessage(string mdate)
        {
            DataTable dtable = new DataTable();
            string strSql = "select c.ProductID, c.ProdName,c.ProdloosePack,c.ProdPack,a.StockID,a.BatchNumber,a.Expiry,a.MRP,a.ClosingStock,a.ExpiryDate from tblstock a inner join masterproduct c on a.ProductID=c.ProductID where  a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate <= '" + mdate + "' &&  a.ClosingStock > 0 order by c.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        #endregion

        # region Add
        //public bool AddDetails(string ProductId, string BatchNumber, string Expiry, string ExpiryDate, double TradeRate, double PurchaseRate, double MRP, double SaleRate, long OpeningStock, long ClosingStock, long PurchaseStock, long TransferInStock, long CreditNoteStock, long SaleStock, long TransferOutStock, long DebitNoteStock, long PurchaseSchemeStock, long PurchaseReplacementStock, long SaleSchemeStock, string IfRateCorrection, double ProductVATPercent, double PurchaseVATPercent, double ProdCST, string CompanyId, string LastPurchaseAccountId, string LastPurchasePartyShortName, string LastPurchaseBillNumber, string LastPurchaseDate, long LastPurchaseVoucherNumber, string LastPurchaseVoucherType, string ScanCode, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetInsertQuery(ProductId, BatchNumber, Expiry, ExpiryDate, TradeRate, PurchaseRate, MRP, SaleRate, OpeningStock, ClosingStock, PurchaseStock, TransferInStock, CreditNoteStock, SaleStock, TransferOutStock, DebitNoteStock, PurchaseSchemeStock, PurchaseReplacementStock, SaleSchemeStock, IfRateCorrection, ProductVATPercent, PurchaseVATPercent, ProdCST, CompanyId, LastPurchaseAccountId, LastPurchasePartyShortName, LastPurchaseBillNumber, LastPurchaseDate, LastPurchaseVoucherNumber, LastPurchaseVoucherType, ScanCode, CreatedDate, CreatedUserId, ModifyDate, ModifyUserId);
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }

        //    return bRetValue;
        //}
        #endregion

        #region Update
        //public bool UpdateDetails(string ProductId, string BatchNumber, string Expiry, string ExpiryDate, double TradeRate, double PurchaseRate, double MRP, double SaleRate, long OpeningStock, long ClosingStock, long PurchaseStock, long TransferInStock, long CreditNoteStock, long SaleStock, long TransferOutStock, long DebitNoteStock, long PurchaseSchemeStock, long PurchaseReplacementStock, long SaleSchemeStock, string IfRateCorrection, double ProductVATPercent, double PurchaseVATPercent, double ProdCST, string CompanyId, string LastPurchaseAccountId, string LastPurchasePartyShortName, string LastPurchaseBillNumber, string LastPurchaseDate, long LastPurchaseVoucherNumber, string LastPurchaseVoucherType, string ScanCode, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        //{
        //    bool returnVal = false;
        //    string strSql = GetUpdateQuery(ProductId, BatchNumber, Expiry, ExpiryDate, TradeRate, PurchaseRate, MRP, SaleRate, OpeningStock, ClosingStock, PurchaseStock, TransferInStock, CreditNoteStock, SaleStock, TransferOutStock, DebitNoteStock, PurchaseSchemeStock, PurchaseReplacementStock, SaleSchemeStock, IfRateCorrection, ProductVATPercent, PurchaseVATPercent, ProdCST, CompanyId, LastPurchaseAccountId, LastPurchasePartyShortName, LastPurchaseBillNumber, LastPurchaseDate, LastPurchaseVoucherNumber, LastPurchaseVoucherType, ScanCode, CreatedDate, CreatedUserId, ModifyDate, ModifyUserId);
        //    strSql = string.Format("{0} ProductID = '{1}' And BatchNumber = '{2}' And MRP = {3}", strSql, ProductId, BatchNumber, MRP);
        //    try
        //    {
        //        DBInterface.ExecuteQuery(strSql);
        //        returnVal = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteError(ex.ToString());
        //        returnVal = false;
        //    }
        //    return returnVal;

        //}
        public bool UpdateCreditNoteStock(string StockId, int Quantity)
        {
            bool returnVal = false;
            int mqty = Quantity;
            int mtotqty = mqty;



            string strSql = "";
            int closingstk = 0;
            int crstock = 0;

            DataRow dRow = null;
            if (StockId != "")
            {
                strSql = "select ClosingStock,CreditNoteStock from tblstock where StockId = '" + StockId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
            {
                if (dRow["ClosingStock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["ClosingStock"].ToString());
                if (dRow["CreditNoteStock"] != DBNull.Value)
                    crstock = Convert.ToInt32(dRow["CreditNoteStock"].ToString());


                closingstk += mtotqty;
                crstock += mqty;



                strSql = "Update tblstock set closingstock = " + closingstk + ", CreditNoteStock = " + crstock + " where StockID = '" + StockId + "'";
                try
                {
                    if (DBInterface.ExecuteQuery(strSql) > 0)
                        returnVal = true;
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());
                    returnVal = false;
                }
            }
            return returnVal;
        }
        public bool UpdateCreditNoteStockReduceFromTemp(string StockId, int Quantity)
        {
            bool returnVal = false;
            int mqty = Quantity;
            int mtotqty = mqty;



            string strSql = "";
            int closingstk = 0;
            int crstock = 0;

            DataRow dRow = null;
            if (StockId != "")
            {
                strSql = "select ClosingStock,CreditNoteStock from tblstock where StockId = '" + StockId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
            {
                if (dRow["ClosingStock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["ClosingStock"].ToString());
                if (dRow["CreditNoteStock"] != DBNull.Value)
                    crstock = Convert.ToInt32(dRow["CreditNoteStock"].ToString());


                closingstk -= mtotqty;
                crstock -= mqty;




                if (closingstk < 0)
                    returnVal = false;
                else
                {

                    strSql = "Update tblstock set closingstock = " + closingstk + ", CreditNoteStock =  " + crstock + " where StockID = '" + StockId + "'";
                    try
                    {
                        DBInterface.ExecuteQuery(strSql);
                        returnVal = true;
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex.ToString());
                        returnVal = false;
                    }
                }
            }
            return returnVal;
        }
        public bool UpdateDebitNoteStockAddFromTemp(string StockId, int Quantity)
        {
            bool returnVal = false;
            string strSql = "Update tblstock set closingstock = closingstock + " + Quantity + ", DebitNoteStock = DebitNoteStock  - " + Quantity + " where StockID = '" + StockId + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool UpdateDebitNoteStock(string StockId, int Quantity, int scmquantity)
        {
            bool bRetValue = false;

            int mqty = Quantity;
            int mscmqty = scmquantity;
            int mtotqty = mqty + mscmqty;

            string strSql = "";
            int closingstk = 0;
            int dbstock = 0;

            DataRow dRow = null;
            try
            {
                if (StockId != "")
                {
                    strSql = "select ClosingStock,DebitNoteStock from tblstock where StockId = '" + StockId + "'";
                    dRow = DBInterface.SelectFirstRow(strSql);
                }
                if (dRow != null)
                {
                    if (dRow["ClosingStock"] != DBNull.Value)
                        closingstk = Convert.ToInt32(dRow["ClosingStock"].ToString());
                    if (dRow["DebitNoteStock"] != DBNull.Value)
                        dbstock = Convert.ToInt32(dRow["DebitNoteStock"].ToString());


                    closingstk -= mtotqty;
                    dbstock += mqty;


                    strSql = "Update tblstock set closingstock = " + closingstk + ", DebitNoteStock = " + dbstock + " where StockID = '" + StockId + "'";

                    DBInterface.ExecuteQuery(strSql);
                    bRetValue = true;
                }
            }

            catch { bRetValue = false; }

            return bRetValue;
        }
        public DataRow IfStockIDFoundInStockTable(string stockID)
        {
            string strSql = "";
            //if (reasonCode == "N")
            //    strSql = "Select  * from tblStock where stockID = '" + stockID + "' && ifBreakageStock = 1";
            //else
            strSql = "Select  * from tblStock where stockID = '" + stockID + "'";
            return DBInterface.SelectFirstRow(strSql);

        }
        public bool UpdateStockOut(string StockId, int Quantity)
        {
            bool bRetValue = false;
            string strSql = "Update tblstock set closingstock = closingstock -  " + Quantity + ", DebitNoteStock = DebitNoteStock + " + Quantity + " where StockID = '" + StockId + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                bRetValue = true;
            }
            catch { bRetValue = false; }
            return bRetValue;
        }
        public bool UpdateStockOutAddFromTemp(string StockId, int Quantity)
        {
            bool bRetValue = false;
            string strSql = "Update tblstock set closingstock = closingstock + " + Quantity + ", DebitNoteStock = DebitNoteStock - " + Quantity + " where StockID = '" + StockId + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                bRetValue = true;
            }
            catch { bRetValue = false; }
            return bRetValue;
        }
        public bool UpdateDebtorSaleStockAddFromTemp(string LastStockId, int Quantity)
        {
            bool returnVal = false;
            int mqty = Quantity;
            int mtotqty = mqty;


            string strSql = "";
            int closingstk = 0;
            int salestock = 0;

            DataRow dRow = null;
            if (LastStockId != "")
            {
                strSql = "select ClosingStock,SaleStock from tblstock where StockId = '" + LastStockId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
            {
                if (dRow["ClosingStock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["ClosingStock"].ToString());
                if (dRow["SaleStock"] != DBNull.Value)
                    salestock = Convert.ToInt32(dRow["SaleStock"].ToString());


                closingstk += mtotqty;
                salestock -= mqty;



                strSql = "Update tblstock set closingstock = " + closingstk + ", SaleStock = " + salestock + " where StockID = '" + LastStockId + "'";
                // string strSql = "Update tblstock set closingstock = closingstock + " + Quantity + ", SaleStock = SaleStock + " + Quantity + " where StockID = " + "'" +
                // LastStockId + "'";
                try
                {
                    if (DBInterface.ExecuteQuery(strSql) > 0)
                        returnVal = true;
                }
                catch { returnVal = false; }
            }
            return returnVal;
        }
        public bool UpdateDebtorSaleStock(string StockId, int Quantity)
        {
            bool returnVal = false;
            int mqty = Quantity;
            int mtotqty = mqty;


            string strSql = "";
            int closingstk = 0;
            int salestock = 0;

            DataRow dRow = null;
            if (StockId != "")
            {
                strSql = "select ClosingStock,SaleStock from tblstock where StockId = '" + StockId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
            {
                if (dRow["ClosingStock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["ClosingStock"].ToString());
                if (dRow["SaleStock"] != DBNull.Value)
                    salestock = Convert.ToInt32(dRow["SaleStock"].ToString());


                closingstk -= mtotqty;
                salestock += mqty;
                if (closingstk < 0 && General.CurrentSetting.MsetSaleAllowNegativeStock != "Y")
                    closingstk = 0;



                strSql = "Update tblstock set closingstock = " + closingstk + ", SaleStock = " + salestock + " where StockID = '" + StockId + "'";
                //  string strSql = "Update tblstock set closingstock = closingstock -  " + Quantity + ", SaleStock = SaleStock + " + Quantity + " where StockID = '" + stockId + "'";
                try
                {
                    if (DBInterface.ExecuteQuery(strSql) > 0)
                        returnVal = true;
                }
                catch { returnVal = false; }
            }
            return returnVal;
        }

        public bool AddDebtorSaleStockForNegativeStock(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, Int32 SchemeQuanity, double VATPer, int ProdPakn, string StockID, double DistributorSaleRate, double DistributorSaleRatePercent)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryAddDebtorSaleStockForNegativeStock(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, VATPer, ProdPakn, StockID, DistributorSaleRate, DistributorSaleRatePercent);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQueryAddDebtorSaleStockForNegativeStock(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, Int32 SchemeQuanity, double VATPer, int ProdPakn, string StockID, double DistributorSaleRate, double DistributorSaleRatePercent)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblstock";
            objQuery.AddToQuery("StockID", StockID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("ClosingStock", (Quantity) * -1);
            objQuery.AddToQuery("SaleStock", Quantity);
            objQuery.AddToQuery("PurchaseSchemeStock", 0);
            objQuery.AddToQuery("PurchaseReplacementStock", 0);
            objQuery.AddToQuery("PurchaseVATPercent", VATPer);
            objQuery.AddToQuery("ProductVATPercent", VATPer);
            objQuery.AddToQuery("OpeningStock", 0);
            objQuery.AddToQuery("BeginningStock", 0);
            objQuery.AddToQuery("IfRateCorrection", "");
            objQuery.AddToQuery("ScanCode", "");
            objQuery.AddToQuery("TransferInStock", 0);
            objQuery.AddToQuery("CreditNoteStock", 0);
            objQuery.AddToQuery("TransferOutStock", 0);
            objQuery.AddToQuery("DebitNoteStock", 0);
            objQuery.AddToQuery("SaleSchemeStock", 0);
            objQuery.AddToQuery("DistributorSaleRate", 0);
            objQuery.AddToQuery("DistributorSaleRatePer", 0);
            return objQuery.InsertQuery();
        }

        public bool UpdatePurchaseStock(string StockId, int Quantity, int Scheme, int Replacement, int LoosePack, double distSaleRate, double distSaleRatePer)
        {
            bool bRetValue = false;
            int mqty = Quantity;
            int mscm = Scheme;
            int mrepl = Replacement;
            int mtotqty = mqty + mscm + mrepl;



            string strSql = "";
            int closingstk = 0;
            int purstock = 0;
            int scmstock = 0;
            int replstock = 0;
            DataRow dRow = null;
            if (StockId != "")
            {
                strSql = "select ClosingStock,PurchaseStock,PurchaseSchemeStock,PurchaseReplacementStock from tblstock where StockId = '" + StockId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
            {
                if (dRow["ClosingStock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["ClosingStock"].ToString());
                if (dRow["PurchaseStock"] != DBNull.Value)
                    purstock = Convert.ToInt32(dRow["PurchaseStock"].ToString());
                if (dRow["PurchaseSchemeStock"] != DBNull.Value)
                    scmstock = Convert.ToInt32(dRow["PurchaseSchemeStock"].ToString());
                if (dRow["PurchaseReplacementStock"] != DBNull.Value)
                    replstock = Convert.ToInt32(dRow["PurchaseReplacementStock"].ToString());

                closingstk += mtotqty;
                purstock += mqty;
                scmstock += mscm;
                replstock += mrepl;

                strSql = "";
                strSql = "Update tblstock set ClosingStock = " + closingstk +
                       ", PurchaseStock = " + purstock + ", PurchaseSchemeStock = " + scmstock +
                       ", PurchaseReplacementStock = " + replstock + " where StockID = '" + StockId + "'";
                try
                {
                    if (DBInterface.ExecuteQuery(strSql) > 0)
                    {
                        bRetValue = true;
                    }

                }
                catch { bRetValue = false; }
            }
            return bRetValue;
        }


        public bool UpdateProductDetailsInStockTable(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
              string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double PurchaseVATPercent,
              double ProductVATPercent, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount,
              string accountId, string billnumber, string voutype, int vounumber, string voudate, int ProdLoosePack, string StockId, double productMargin, int LoosePack, double pricetoretailer, double profitpercent)
        {

            bool bRetValue = false;
            int mqty = Quantity * LoosePack;
            int mscm = SchemeQuantity * LoosePack;
            int mrepl = ReplacementQuantity * LoosePack;
            int mtotqty = mqty + mscm + mrepl;



            string strSql = "";
            int closingstk = 0;
            int purstock = 0;
            int scmstock = 0;
            int replstock = 0;
            DataRow dRow = null;
            if (StockId != "")
            {
                strSql = "select ClosingStock,PurchaseStock,PurchaseSchemeStock,PurchaseReplacementStock from tblstock where StockId = '" + StockId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
            {
                if (dRow["ClosingStock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["ClosingStock"].ToString());
                if (dRow["PurchaseStock"] != DBNull.Value)
                    purstock = Convert.ToInt32(dRow["PurchaseStock"].ToString());
                if (dRow["PurchaseSchemeStock"] != DBNull.Value)
                    scmstock = Convert.ToInt32(dRow["PurchaseSchemeStock"].ToString());
                if (dRow["PurchaseReplacementStock"] != DBNull.Value)
                    replstock = Convert.ToInt32(dRow["PurchaseReplacementStock"].ToString());

                closingstk += mtotqty;
                purstock += mqty;
                scmstock += mscm;
                replstock += mrepl;

                //strSql = "Update tblstock set ClosingStock = " + closingstk +
                //       ", PurchaseStock = " + purstock + ", PurchaseSchemeStock = " + scmstock +
                //       ", PurchaseReplacementStock = " + replstock + " where StockID = '" + StockId + "'";
                //try
                //{
                //    if (DBInterface.ExecuteQuery(strSql) > 0)
                //    {
                //        bRetValue = true;
                //    }

                //}
                //catch { bRetValue = false; }
                strSql = "";
                strSql = GetUpdateQueryDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, PurchaseVATPercent,
                    ProductVATPercent, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount,
                    accountId, billnumber, voutype, vounumber, voudate, ProdLoosePack, StockId, productMargin,pricetoretailer,profitpercent);
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            }
            return bRetValue;

        }

        private string GetUpdateQueryDetailsInStockTable(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double PurchaseVATPercent,
               double ProductVATPercent, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount,
               string accountId, string billnumber, string voutype, int vounumber, string voudate, int ProdLoosePack, string stockid, double productMargin, double pricetoretailer, double profitpercent)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblstock";
            objQuery.AddToQuery("StockID", stockid, true);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("ClosingStock", (Quantity + SchemeQuantity + ReplacementQuantity) * ProdLoosePack);
            objQuery.AddToQuery("PurchaseStock", (Quantity) * ProdLoosePack);
            objQuery.AddToQuery("PurchaseSchemeStock", (SchemeQuantity) * ProdLoosePack);
            objQuery.AddToQuery("PurchaseReplacementStock", (ReplacementQuantity) * ProdLoosePack);
            objQuery.AddToQuery("PurchaseVATPercent", PurchaseVATPercent);
            objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("LastPurchaseAccountId", accountId);
            objQuery.AddToQuery("LastPurchaseBillNumber", billnumber);
            objQuery.AddToQuery("LastPurchaseVoucherType", voutype);
            objQuery.AddToQuery("LastPurchaseVoucherNumber", vounumber);
            objQuery.AddToQuery("LastPurchaseDate", voudate);
            objQuery.AddToQuery("OpeningStock", 0);
            objQuery.AddToQuery("BeginningStock", 0);
            objQuery.AddToQuery("IfRateCorrection", "");
            objQuery.AddToQuery("ScanCode", "");
            objQuery.AddToQuery("TransferInStock", 0);
            objQuery.AddToQuery("CreditNoteStock", 0);
            objQuery.AddToQuery("SaleStock", 0);
            objQuery.AddToQuery("TransferOutStock", 0);
            objQuery.AddToQuery("DebitNoteStock", 0);
            objQuery.AddToQuery("SaleSchemeStock", 0);
            objQuery.AddToQuery("Margin", productMargin);
            objQuery.AddToQuery("PriceToRetailer", pricetoretailer);
            objQuery.AddToQuery("ProfitPercent", profitpercent);
            return objQuery.UpdateQuery();
        }


        public bool UpdateOPStockStock(string StockId, int Quantity, string createdby, string createddate, string createdtime)
        {
            bool returnVal = false;
            int mqty = Quantity;
            int mtotqty = mqty;


            string strSql = "";
            int closingstk = 0;
            int openingtock = 0;

            DataRow dRow = null;
            if (StockId != "")
            {
                strSql = "select ClosingStock,OpeningStock from tblstock where StockId = '" + StockId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
            {
                if (dRow["ClosingStock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["ClosingStock"].ToString());
                if (dRow["OpeningStock"] != DBNull.Value)
                    openingtock = Convert.ToInt32(dRow["OpeningStock"].ToString());


                closingstk += mtotqty;
                openingtock += mqty;



                strSql = "Update tblstock set closingstock = " + closingstk + ", OpeningStock = " + openingtock + " where StockID = '" + StockId + "'";
                //string strSql = "Update tblstock set ClosingStock = ClosingStock +  " + Quantity + ", OpeningStock =  OpeningStock + " + Quantity + ", CreatedUserID = '" + createdby + "', CreatedDate = '" + createddate + "', CreatedTime = '" + createdtime + "' where StockID = '" + StockId + "'";
                try
                {
                    if (DBInterface.ExecuteQuery(strSql) > 0)
                    {
                        returnVal = true;
                    }

                }
                catch { returnVal = false; }
            }
            return returnVal;
        }

        public bool UpdatePurchaseIntblStockReduceFromTemp(string StockId, int Quantity, int Scheme, int Replacement, int LoosePack)
        {
            bool bRetValue = false;
            int mqty = Quantity * LoosePack;
            int mscm = Scheme * LoosePack;
            int mrepl = Replacement * LoosePack;
            int mtotqty = mqty + mscm + mrepl;
            string strSql = "Update tblstock set ClosingStock = ClosingStock - " + mtotqty +
                            ", PurchaseStock = PurchaseStock - " + mqty +
                              ", PurchaseSchemeStock = PurchaseSchemeStock - " + mscm +
                   ", PurchaseReplacementStock = PurchaseReplacementStock - " + mrepl +
                   " where StockID = " + "'" + StockId + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }

            }
            catch { bRetValue = false; }
            return bRetValue;
        }
        #endregion

        # region Insert
        public bool InsertCreditNoteStock(string stockid, string ProductId, string BatchNumber, double mrp, int Quantity, int CNQuantity, string expiry, double vatper, double purchaserate, double salerate, double traderate, string ExpiryDate)
        {

            bool bRetValue = false;

            string strSql = GetInsertQueryCreditNote(stockid, ProductId, BatchNumber, mrp, Quantity, CNQuantity, expiry, vatper, purchaserate, salerate, traderate, ExpiryDate);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }
        # endregion

        #region Delete
        public bool DeleteDetails(string Id)
        {
            bool bRetValue = false;
            string strSql = null;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }
        # endregion

        #region Query Building Functions
        private string GetDataForUnique(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("");
            return sQuery.ToString();
        }
      

        private string GetInsertQueryCreditNote(string stockid, string ProductId, string BatchNumber, double MRP, int Quantity, int CNQuantity, string Expiry, double VatPer, double PurchaseRate, double SaleRate, double TradeRate, string expirydate)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblStock";
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("ProductId", ProductId);
            objQuery.AddToQuery("BatchNumber", BatchNumber);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", expirydate);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("ClosingStock", Quantity);
            objQuery.AddToQuery("CreditNoteStock", CNQuantity);
            objQuery.AddToQuery("ProductVATPercent", VatPer);
            objQuery.AddToQuery("OpeningStock", 0);
            objQuery.AddToQuery("BeginningStock", 0);
            objQuery.AddToQuery("PurchaseStock", 0);
            objQuery.AddToQuery("TransferInStock", 0);
            objQuery.AddToQuery("SaleStock", 0);
            objQuery.AddToQuery("TransferOutStock", 0);
            objQuery.AddToQuery("DebitNoteStock", 0);
            objQuery.AddToQuery("PurchaseSchemeStock", 0);
            objQuery.AddToQuery("PurchaseReplacementStock", 0);
            objQuery.AddToQuery("SaleSchemeStock", 0);
            objQuery.AddToQuery("PurchaseVATPercent", VatPer);
            objQuery.AddToQuery("LastPurchaseAccountId", "");
            objQuery.AddToQuery("LastPurchaseBillNumber", "");
            objQuery.AddToQuery("LastPurchaseVoucherType", "");
            objQuery.AddToQuery("LastPurchaseVoucherNumber", 0);
            objQuery.AddToQuery("ScanCode", "");
            objQuery.AddToQuery("IfRateCorrection", "");
            objQuery.AddToQuery("LastPurchaseDate", "");

            return objQuery.InsertQuery();
        }
        private string GetUpdateQuery(string ProductId, string BatchNumber, string Expiry, string ExpiryDate, double TradeRate, double PurchaseRate, double MRP, double SaleRate, long OpeningStock, long ClosingStock, long PurchaseStock, long TransferInStock, long CreditNoteStock, long SaleStock, long TransferOutStock, long DebitNoteStock, long PurchaseSchemeStock, long PurchaseReplacementStock, long SaleSchemeStock, string IfRateCorrection, double ProductVATPercent, double PurchaseVATPercent, double ProdCST, string CompanyId, string LastPurchaseAccountId, string LastPurchasePartyShortName, string LastPurchaseBillNumber, string LastPurchaseDate, long LastPurchaseVoucherNumber, string LastPurchaseVoucherType, string ScanCode, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblStock ";
            objQuery.AddToQuery("ProductId", ProductId);
            objQuery.AddToQuery("BatchNumber", BatchNumber);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("OpeningStock", OpeningStock);
            objQuery.AddToQuery("ClosingStock", ClosingStock);
            objQuery.AddToQuery("PurchaseStock", PurchaseStock);
            objQuery.AddToQuery("TransferInStock", TransferInStock);
            objQuery.AddToQuery("CreditNoteStock", CreditNoteStock);
            objQuery.AddToQuery("SaleStock", SaleStock);
            objQuery.AddToQuery("TransferOutStock", TransferOutStock);
            objQuery.AddToQuery("DebitNoteStock", DebitNoteStock);
            objQuery.AddToQuery("PurchaseSchemeStock", PurchaseSchemeStock);
            objQuery.AddToQuery("PurchaseReplacementStock", PurchaseReplacementStock);
            objQuery.AddToQuery("SaleSchemeStock", SaleSchemeStock);
            objQuery.AddToQuery("IfRateCorrection", IfRateCorrection);
            objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("PurchaseVATPercent", PurchaseVATPercent);
            objQuery.AddToQuery("ProdCST", ProdCST);
            objQuery.AddToQuery("CompanyId", CompanyId);
            objQuery.AddToQuery("LastPurchaseAccountId", LastPurchaseAccountId);
            objQuery.AddToQuery("LastPurchasePartyShortName", LastPurchasePartyShortName);
            objQuery.AddToQuery("LastPurchaseBillNumber", LastPurchaseBillNumber);
            objQuery.AddToQuery("LastPurchaseDate", LastPurchaseDate);
            objQuery.AddToQuery("LastPurchaseVoucherNumber", LastPurchaseVoucherNumber);
            objQuery.AddToQuery("LastPurchaseVoucherType", LastPurchaseVoucherType);

            objQuery.AddToQuery("ScanCode", ScanCode);
            objQuery.AddToQuery("CreatedUserId", CreatedUserId);
            objQuery.AddToQuery("ModifiedUserId", ModifyUserId);
            return objQuery.UpdateQuery();
        }
        private string GetDeleteQuery(string Id)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblStock ";
            objQuery.AddToQuery("ProductId", Id, true);
            return objQuery.DeleteQuery();
        }
        #endregion

        internal DataRow GetDetailsForProduct(string prodID)
        {
            throw new NotImplementedException();
        }

        public DataRow GetRowForBatchMRPIntblTempPurchase(string ProductID, string Batchno, double MRP)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select * from tblTempPurchase where ProductID = '{0}' && BatchNumber = '{1}' && MRP = {2} ", ProductID, Batchno, MRP);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }

        public DataRow CheckForProductBatchMRPInStocktable(string ProductID, string Batchno, double MRP)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select * from tblStock where ProductID = '{0}' && BatchNumber = '{1}' && MRP = {2} ", ProductID, Batchno, MRP);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }

        public DataTable GetBatchListForReport(string productID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select BatchNumber,mrp,stockID from tblstock   where ProductID = '{0}'", productID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow GetOpeningStockForBatch(string productID, string batchNumber)
        {
            DataRow dr;
            string strSql = string.Format("Select OpeningStock from tblstock   where ProductID = '{0}' && stockID = '{1}'", productID, batchNumber);
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }

        public bool UpdateClosingStockForYearEnd(string mproductid, string mstockid, int mclosingstock)
        {
            bool returnVal = false;
          
            string strSql = "";
            int openingstk = 0;           
            DataRow dRow = null;
            if (mstockid != "")
            {
                strSql = "select OpeningStock from tblstock where StockId = '" + mstockid + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
            {
                if (dRow["OpeningStock"] != DBNull.Value)
                    openingstk = Convert.ToInt32(dRow["OpeningStock"].ToString());



                openingstk += mclosingstock;


                strSql = "Update tblstock set OpeningStock = "+ openingstk + " where StockID = '" + mstockid + "'";
                try
                {
                    if (DBInterface.ExecuteQuery(strSql) > 0)
                        returnVal = true;
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());
                    returnVal = false;
                }
            }
            return returnVal;
        }       

        public DataTable GetOpeningStockForCurrentYear(string _MToDate)
        {
            DataTable dtable = null;
            string strSql = "";

            strSql = "Select b.ProductID, b.StockID, a.voucherDate,  b.Quantity , b.SchemeQuantity,d.Prodname,d.Prodpack,d.Prodloosepack  from detailopstock b inner join voucheropStock a on b.MasterID = a.MasterID inner join masterproduct d on b.productID = d.ProductId where a.voucherDate > '" + _MToDate + "'";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable ReadPurchaseDetailsExpiredStock(string FromDate, string ToDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "select a.StockID,a.ProductID,b.PurchaseID,b.StockID,b.Quantity,b.SchemeQuantity,c.PurchaseID,c.VoucherType,c.voucherNumber,c.VoucherDate,c.AccountID,c.PurchaseBillNumber,d.AccName,d.AccAddress1,e.ProductID, e.ProdName,e.ProdLoosePack,e.ProdShelfID from tblstock a inner join masterproduct e on a.ProductID = e.ProductID inner join detailpurchase b on a.stockID = b.StockID inner join voucherpurchase c on b.purchaseID = c.purchaseID inner join masteraccount d on c.AccountID = d.AccountID where  a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate >= '" + FromDate + "' &&  a.expirydate <= '" + ToDate + "' &&  a.ClosingStock >= e.ProdLoosePack  order by b.StockID";
           // string strSql = "select a.StockID,a.BatchNumber,a.Expiry,a.MRP,a.ClosingStock,(a.ClosingStock*a.mrp/e.ProdLoosePack) as Amount,a.PurchaseRate,a.SaleRate,a.TradeRate,a.Expiry,a.ProductID,b.PurchaseID,b.StockID,c.PurchaseID,c.VoucherType,c.voucherNumber,c.VoucherDate,c.AccountID,e.ProductID, e.ProdName,e.ProdloosePack,e.ProdPack,e.ProdShelfID from tblstock a inner join masterproduct e on a.ProductID = e.ProductID inner join detailpurchase b on a.stockID = b.StockID inner join voucherpurchase c on b.purchaseID = c.purchaseID inner join masteraccount d on c.AccountID = d.AccountID where  a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate <= '" + mdate + "' &&  a.ClosingStock >= c.ProdLoosePack  order by b.AccName";
          //  string strSql = "select c.ProductID, c.ProdName,c.ProdloosePack,c.ProdPack,c.ProdShelfID,a.StockID,a.BatchNumber,a.Expiry,a.MRP,a.ClosingStock,(a.ClosingStock*a.mrp/c.ProdLoosePack) as Amount,a.LastPurchaseAccountId as AccountID,a.ExpiryDate,a.PurchaseRate,a.SaleRate,a.TradeRate,a.LastPurchaseBillNumber,a.LastPurchaseDate,a.LastPurchaseVoucherType,a.LastPurchaseVoucherNumber,c.ProdVATPercent,d.ShelfCode, b.accName,b.AccAddress1,b.AccAddress2,b.AccLessPercentInDebitNote from tblstock a left outer join masteraccount b  on a.LastPurchaseAccountId = b.AccountID  inner join masterproduct c on a.ProductID=c.ProductID left outer join mastershelf d  on c.ProdShelfId = d.ShelfID where  a.expiry != '" + "00/00" + "' && a.expiry != '" + "     " + "' &&  a.expirydate <= '" + mdate + "' &&  a.ClosingStock >= c.ProdLoosePack  order by b.AccName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetSaleForCompanywiseSummary(string mfromdate, string mtodate)
        {
            DataTable dt = null;
            string strSql = "";
            //strSql = "Select a.ID,a.VoucherDate,b.MasterSaleID,b.ProductID,sum(b.Quantity) as Quantity,sum(b.SchemeQuantity) as SchemeQuantity,c.productID, c.ProdCompID  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID inner join masterproduct c on b.ProductID = c.ProductID  where a.voucherdate <='" + mtodate + "' &&  c.ProdCompID = '" + mcompID + "' group by b.ProductID";

            strSql = "Select a.ID,a.VoucherDate,b.MasterSaleID,b.ProductID,(b.Quantity) as Quantity,(b.SchemeQuantity) as SchemeQuantity,sum(b.Quantity * (b.SaleRate/c.Prodloosepack)) as Amount,c.productID, c.ProdCompID, d.CompID,d.CompName  from vouchersale a inner join detailsale b on a.ID = b.MasterSaleID inner join masterproduct c on b.ProductID = c.ProductID inner join mastercompany d on c.ProdCompID = d.CompID  where a.voucherDate >= '"+ mfromdate +"' && a.voucherdate <='" + mtodate + "'  Group by c.ProdCompID order by Amount desc";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetPurchaseDetailsForPurchaseOrder(string productID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select a.StockID,a.ProductId,a.BatchNumber,a.Expiry,a.TradeRate,a.PurchaseRate,a.MRP,a.SaleRate,b.AccountId,b.AccName,c.AccountID,c.PurchaseID,c.VoucherDate from detailpurchase a inner join voucherpurchase c on a.PurchaseID = c.PurchaseID  left outer join  masteraccount b on c.AccountID = b.AccountID  where a.ProductID = '{0}'  order by c.Voucherdate desc", productID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        internal int GetStockToCheckNegetive(string LastStockID)
        {           
            string strSql = "";
            int closingstk = 0;  
            DataRow dRow = null;
            if (LastStockID != "")
            {
                strSql = "select ClosingStock from tblstock where StockId = '" + LastStockID + "'";
                dRow = DBInterface.SelectFirstRow(strSql);

                if (dRow != null)
                {
                    if (dRow["ClosingStock"] != DBNull.Value)
                        closingstk = Convert.ToInt32(dRow["ClosingStock"].ToString());
                }
                else
                    closingstk = -1;
            }
            return closingstk;
        }

        public bool setDistributionPercentage(int _max)  //Amar
        {
            try
            {
                bool returnVal = false;
                string strSql = "";
                string stockId = "";
                decimal TreadRate = 0;
                decimal DistsaleRate = 0;
                decimal NewDistsaleRate = 0;
                decimal percentage = 0;
                decimal NewPercentage = 0;
                DataTable dt = null;
                strSql = "select *from tblstock where StockId!=''";
                dt = DBInterface.SelectDataTable(strSql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    stockId = "";
                    TreadRate = 0;
                    DistsaleRate = 0;
                    NewDistsaleRate = 0;
                    percentage = 0;
                    NewPercentage = 0;
                    stockId = dt.Rows[i]["StockID"].ToString();
                    if (dt.Rows[i]["TradeRate"] != DBNull.Value && dt.Rows[i]["TradeRate"].ToString() != string.Empty)
                    {
                        TreadRate = Convert.ToDecimal(dt.Rows[i]["TradeRate"].ToString());
                    }
                   
                    if (dt.Rows[i]["DistributorSaleRate"] != DBNull.Value && dt.Rows[i]["DistributorSaleRate"].ToString() != string.Empty)
                    {
                        DistsaleRate = Convert.ToDecimal(dt.Rows[i]["DistributorSaleRate"].ToString());
                    }
                    percentage = (DistsaleRate * _max) / 100;
                    NewDistsaleRate = DistsaleRate + percentage;
                    if (NewDistsaleRate > 0 && TreadRate > 0)
                    {
                        NewPercentage = Convert.ToDecimal((NewDistsaleRate - TreadRate) * 100) / TreadRate;
                    }
                    strSql = "Update tblstock set DistributorSaleRate = " + NewDistsaleRate + ", DistributorSaleRatePer =" + NewPercentage + " where StockID = '" + stockId + "'";
                    if (DBInterface.ExecuteQuery(strSql) > 0)
                        returnVal = true;
                }
                return returnVal;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                return false;
            }

        }
    }
}

