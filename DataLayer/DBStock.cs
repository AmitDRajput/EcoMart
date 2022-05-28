using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcoMart.DataLayer
{
    class DBStock
    {
        public DataTable GetStockByProductID(int prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select ProductID,BatchNumber,Expiry,ExpiryDate,TradeRate,PurchaseRate,MRP,SaleRate,OpeningStock,ClosingStock,PurchaseStock,TransferInStock,CreditNoteStock,SaleStock,TransferOutStock,DebitNoteStock,PurchaseSchemeStock,PurchaseReplacementStock,SaleSchemeStock,IfRateCorrection,ProductVATPercent,PurchaseVATPercent,LastPurchaseBillNumber,LastPurchaseDate,LastPurchaseVoucherNumber,ScanCode,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId from tblstock where ProductID = '{0}'", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetStockByProductIDForSale(int prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select d.StockID,d.ProductID,d.BatchNumber,d.Expiry,d.ExpiryDate,d.TradeRate,d.PurchaseRate,d.MRP,d.SaleRate,d.DistributorSaleRate,d.OpeningStock,d.ClosingStock,d.ScanCode,a.ProductID,a.ProdName,a.ProdPack,a.ProdLoosePack,floor(d.ClosingStock/a.ProdLoosePack) as ClosingStockPack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdMaxLevel,a.ProdLastSaleStockID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from tblstock d inner join  masterproduct a  on d.ProductID = a.ProductID  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId where a.ProdClosingStock > 0  &&  d.ProductID = '{0}' && (d.expirydate >= '"+ DateTime.Today.Date.ToString("yyyyMMdd") + "' || d.expirydate = '') order by ExpiryDate", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow GetProductIDExists(int prodID) // [ansuman][testing]
        {
            DataRow drow = null;
            string strSql = string.Format("select ProductID from tblstock where ProductID = '{0}'", prodID);
            drow = DBInterface.SelectFirstRow(strSql);
            return drow;
        }
        public DataTable GetBatchListByProductID(int prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select ProductID,BatchNumber,Expiry,MRP,SaleRate,PurchaseStock from tblstock where ProductID = '{0}'", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetBatchListByProductIDForPurchaseBatchWise(int prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select a.ProductID,a.BatchNumber,a.MRP,a.PurchaseRate from tblstock a where ProductID = '{0}' group by a.ProductID,a.BatchNumber,a.mrp", prodID );
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetStockByProductIDForDBCRNote(int prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select BatchNumber,Expiry,MRP,PurchaseRate,SaleRate,ClosingStock,ProductVATPercent,ExpiryDate from tblstock where ProductID = '{0}'", prodID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from tblStock where BatchNumber='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(int ProductID, string BatchNumber, string Expiry, string ExpiryDate, double TradeRate, double PurchaseRate, double MRP, double SaleRate, long OpeningStock, long ClosingStock, long PurchaseStock, long TransferInStock, long CreditNoteStock, long SaleStock, long TransferOutStock, long DebitNoteStock, long PurchaseSchemeStock, long PurchaseReplacementStock, long SaleSchemeStock, string IfRateCorrection, double ProductVATPercent, double PurchaseVATPercent, double ProdCST, string CompanyId, string LastPurchaseAccountId, string LastPurchasePartyShortName, string LastPurchaseBillNumber, string LastPurchaseDate, long LastPurchaseVoucherNumber, string LastPurchaseVoucherType, string ScanCode, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(ProductID, BatchNumber, Expiry, ExpiryDate, TradeRate, PurchaseRate, MRP, SaleRate, OpeningStock, ClosingStock, PurchaseStock, TransferInStock, CreditNoteStock, SaleStock, TransferOutStock, DebitNoteStock, PurchaseSchemeStock, PurchaseReplacementStock, SaleSchemeStock, IfRateCorrection, ProductVATPercent, PurchaseVATPercent, ProdCST, CompanyId, LastPurchaseAccountId, LastPurchasePartyShortName, LastPurchaseBillNumber, LastPurchaseDate, LastPurchaseVoucherNumber, LastPurchaseVoucherType, ScanCode, CreatedDate, CreatedUserId, ModifyDate, ModifyUserId);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool UpdateDetails(int ProductID, string BatchNumber, string Expiry, string ExpiryDate, double TradeRate, double PurchaseRate, double MRP, double SaleRate, long OpeningStock, long ClosingStock, long PurchaseStock, long TransferInStock, long CreditNoteStock, long SaleStock, long TransferOutStock, long DebitNoteStock, long PurchaseSchemeStock, long PurchaseReplacementStock, long SaleSchemeStock, string IfRateCorrection, double ProductVATPercent, double PurchaseVATPercent, double ProdCST, string CompanyId, string LastPurchaseAccountId, string LastPurchasePartyShortName, string LastPurchaseBillNumber, string LastPurchaseDate, long LastPurchaseVoucherNumber, string LastPurchaseVoucherType, string ScanCode, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(ProductID, BatchNumber, Expiry, ExpiryDate, TradeRate, PurchaseRate, MRP, SaleRate, OpeningStock, ClosingStock, PurchaseStock, TransferInStock, CreditNoteStock, SaleStock, TransferOutStock, DebitNoteStock, PurchaseSchemeStock, PurchaseReplacementStock, SaleSchemeStock, IfRateCorrection, ProductVATPercent, PurchaseVATPercent, ProdCST, CompanyId, LastPurchaseAccountId, LastPurchasePartyShortName, LastPurchaseBillNumber, LastPurchaseDate, LastPurchaseVoucherNumber, LastPurchaseVoucherType, ScanCode, CreatedDate, CreatedUserId, ModifyDate, ModifyUserId);
            strSql = string.Format("{0} ProductID = '{1}' And BatchNumber = '{2}' And MRP = {3}", strSql, ProductID, BatchNumber, MRP);
            try
            {
                DBInterface.ExecuteQuery(strSql);
                bRetValue = true;
            }
            catch { bRetValue = false; }

            return bRetValue;
        }

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

        public DataTable GetValidBatchesByProductID(int prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select * from tblstock where ProductID = '{0}' AND ClosingStock > 0 AND STR_TO_DATE(ExpiryDate,'%Y%c%d') >= CURDATE() ORDER BY STR_TO_DATE(ExpiryDate,'%Y%c%d')", prodID);
            dtable = DBInterface.SelectDataTable(strSql);          
            return dtable;
        }

        public DataRow GetStockByProductIDAndBatchNumber(int ProductID, string batchID)
        {
            DataRow dataRow = null;
            string strSql = string.Format("Select ProductID,BatchNumber,Expiry,ExpiryDate,TradeRate,PurchaseRate,MRP,SaleRate,OpeningStock,ClosingStock,PurchaseStock,TransferInStock,CreditNoteStock,SaleStock,TransferOutStock,DebitNoteStock,PurchaseSchemeStock,PurchaseReplacementStock,SaleSchemeStock,IfRateCorrection,ProductVATPercent,PurchaseVATPercent,ProdCST,CompanyId,LastPurchaseAccountId,LastPurchasePartyShortName,LastPurchaseBillNumber,LastPurchaseDate,LastPurchaseVoucherNumber,ScanCode,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId from tblstock where ProductID = '{0}' And BatchNumber = '{1}'", ProductID, batchID);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }

        public DataRow GetStockByProductIDAndBatchNumberAndMRP(int ProductID, string batchID, string mrp)
        {
            DataRow dataRow = null;
            string strSql = ("Select StockID,ProductID,BatchNumber,Expiry,ExpiryDate,TradeRate,PurchaseRate,MRP,SaleRate,OpeningStock,ClosingStock,PurchaseStock,TransferInStock,CreditNoteStock,SaleStock,TransferOutStock,DebitNoteStock,PurchaseSchemeStock,PurchaseReplacementStock,SaleSchemeStock,IfRateCorrection,ProductVATPercent,PurchaseVATPercent,LastPurchaseAccountId,LastPurchaseBillNumber,LastPurchaseDate,LastPurchaseVoucherNumber,ScanCode,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId from tblstock where ProductID = '{0}' And BatchNumber = '{1}' AND MRP = '{2}'");
            strSql = string.Format(strSql, ProductID, batchID, mrp);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }

        public DataRow GetStockByStockID(string stockID)
        {
            DataRow dataRow = null;
            string strSql = ("Select ProductID,BatchNumber,Expiry,ExpiryDate,TradeRate,PurchaseRate,MRP,SaleRate,OpeningStock,ClosingStock,PurchaseStock,TransferInStock,CreditNoteStock,SaleStock,TransferOutStock,DebitNoteStock,PurchaseSchemeStock,PurchaseReplacementStock,SaleSchemeStock,IfRateCorrection,ProductVATPercent,PurchaseVATPercent,LastPurchaseAccountId,LastPurchaseBillNumber,LastPurchaseDate,LastPurchaseVoucherNumber,ScanCode,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId from tblstock where StockID = '{0}'");
            strSql = string.Format(strSql, stockID);
            dataRow = DBInterface.SelectFirstRow(strSql);
            return dataRow;
        }

        #region Query Building Functions
        private string GetDataForUnique(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("");
            return sQuery.ToString();
        }


        private string GetInsertQuery(int ProductID, string BatchNumber, string Expiry, string ExpiryDate, double TradeRate, double PurchaseRate, double MRP, double SaleRate, long OpeningStock, long ClosingStock, long PurchaseStock, long TransferInStock, long CreditNoteStock, long SaleStock, long TransferOutStock, long DebitNoteStock, long PurchaseSchemeStock, long PurchaseReplacementStock, long SaleSchemeStock, string IfRateCorrection, double ProductVATPercent, double PurchaseVATPercent, double ProdCST, string CompanyId, string LastPurchaseAccountId, string LastPurchasePartyShortName, string LastPurchaseBillNumber, string LastPurchaseDate, long LastPurchaseVoucherNumber, string LastPurchaseVoucherType, string ScanCode, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblStock";
            objQuery.AddToQuery("ProductID", ProductID);
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
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(int ProductID, string BatchNumber, string Expiry, string ExpiryDate, double TradeRate, double PurchaseRate, double MRP, double SaleRate, long OpeningStock, long ClosingStock, long PurchaseStock, long TransferInStock, long CreditNoteStock, long SaleStock, long TransferOutStock, long DebitNoteStock, long PurchaseSchemeStock, long PurchaseReplacementStock, long SaleSchemeStock, string IfRateCorrection, double ProductVATPercent, double PurchaseVATPercent, double ProdCST, string CompanyId, string LastPurchaseAccountId, string LastPurchasePartyShortName, string LastPurchaseBillNumber, string LastPurchaseDate, long LastPurchaseVoucherNumber, string LastPurchaseVoucherType, string ScanCode, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblStock ";
            objQuery.AddToQuery("ProductID", ProductID);
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
            objQuery.AddToQuery("ProductID", Id, true);
            return objQuery.DeleteQuery();
        }

        #endregion

    }
}

