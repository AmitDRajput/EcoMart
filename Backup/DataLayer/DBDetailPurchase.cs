using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBDetailPurchase
    {
        public DBDetailPurchase()
        {

        }

        public DataTable GetTable()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select PurchaseId,ProductID,BatchNumber,TradeRate,PurchaseRate,MRP,SaleRate,Expiry,ExpiryDate,Quantity,SchemeQuantity,ReplacementQuantity,ItemDiscountPercent,AmountItemDiscount,SchemeDiscountPercentage,AmountSchemeDiscount,PurchaseVATtPercent,ProductVATPercent,AmountPurchaseVAT,CSTPercent,AmountCST,IfMRPInclusiveOfVAT,IfTradeRateInclusiveOfVAT";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public bool AddDetails(string PurchaseId, string ProductID, string BatchNumber, double TradeRate, double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, long Quantity, long SchemeQuantity, long ReplacementQuntity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercentage, double AmountSchemeDiscount, double PurchaseVATtPercent, double ProductVATPercent, double AmountPurchaseVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(PurchaseId, ProductID, BatchNumber, TradeRate, PurchaseRate, MRP, SaleRate, Expiry, ExpiryDate, Quantity, SchemeQuantity,ReplacementQuntity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercentage, AmountSchemeDiscount, PurchaseVATtPercent, ProductVATPercent, AmountPurchaseVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool UpdateDetails(string PurchaseId, string ProductID, string BatchNumber, double TradeRate, double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, long Quantity, long SchemeQuantity, long ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercentage, double AmountSchemeDiscount, double PurchaseVATtPercent, double ProductVATPercent, double AmountPurchaseVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount)
        {
            bool returnVal = false;
            string strSql = GetUpdateQuery(PurchaseId, ProductID, BatchNumber, TradeRate, PurchaseRate, MRP, SaleRate, Expiry, ExpiryDate, Quantity, SchemeQuantity,ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercentage, AmountSchemeDiscount, PurchaseVATtPercent, ProductVATPercent, AmountPurchaseVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount);
            strSql = string.Format("{0} ProductID = '{1}' And BatchNumber = '{2}' And MRP = {3}", strSql, ProductID, BatchNumber, MRP);
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

        #region Query Building Functions
        private string GetDataForUnique(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("");
            return sQuery.ToString();
        }

        private string GetInsertQuery(string PurchaseId, string ProductID, string BatchNumber, double TradeRate, double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, long Quantity, long SchemeQuantity, long ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercentage, double AmountSchemeDiscount, double PurchaseVATtPercent, double ProductVATPercent, double AmountPurchaseVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailpurchase";
            objQuery.AddToQuery("PurchaseId", PurchaseId);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", BatchNumber);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
            objQuery.AddToQuery("ReplacementQuantity", ReplacementQuantity);
            objQuery.AddToQuery("ItemDiscountPercent", ItemDiscountPercent);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("SchemeDiscountPercent", SchemeDiscountPercentage);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("PurchaseVATtPercent", PurchaseVATtPercent);
            objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("AmountPurchaseVAT", AmountPurchaseVAT);
            objQuery.AddToQuery("CSTPercent", CSTPercent);
            objQuery.AddToQuery("AmountCST", AmountCST);
            objQuery.AddToQuery("IfMRPInclusiveOfVAT", IfMRPInclusiveOfVAT);
            objQuery.AddToQuery("IfTradeRateInclusiveOfVAT", IfTradeRateInclusiveOfVAT);
            objQuery.AddToQuery("Amount", Amount);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string PurchaseId, string ProductID, string BatchNumber, double TradeRate, double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, double Quantity, double SchemeQuantity, double ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercentage, double AmountSchemeDiscount, double PurchaseVATtPercent, double ProductVATPercent, double AmountPurchaseVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailpurchase";
            objQuery.AddToQuery("PurchaseId", PurchaseId);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", BatchNumber);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
            objQuery.AddToQuery("ReplacementQuantity", ReplacementQuantity); 
            objQuery.AddToQuery("ItemDiscountPercent", ItemDiscountPercent);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("SchemeDiscountPercent", SchemeDiscountPercentage);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("PurchaseVATtPercent", PurchaseVATtPercent);
            objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("AmountPurchaseVAT", AmountPurchaseVAT);
            objQuery.AddToQuery("CSTPercent", CSTPercent);
            objQuery.AddToQuery("AmountCST", AmountCST);
            objQuery.AddToQuery("IfMRPInclusiveOfVAT", IfMRPInclusiveOfVAT);
            objQuery.AddToQuery("IfTradeRateInclusiveOfVAT", IfTradeRateInclusiveOfVAT);
            objQuery.AddToQuery("Amount", Amount);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailpurchase";
            objQuery.AddToQuery("PurchaseId", Id, true);
            return objQuery.DeleteQuery();
        }

        #endregion



        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();         
            string strSql = "select * from detailpurchase";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetPurchase()
        {
            DataTable dtable = new DataTable();           
            string strSql = "Select * from detailpurchase order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow GetPurchaseByProductIdAndBatchId(string productID, string batchID, string BatchMRP)
        {
            DataRow dRow = null;
            string strSql = "Select * from detailpurchase dp, voucherpurchase mp where mp.PurchaseId = dp.PurchaseId and ProductID='{0}' AND BatchNumber = '{1}' AND MRP='{2}'";
            strSql = string.Format(strSql, productID, batchID, BatchMRP);
            dRow = DBInterface.SelectFirstRow(strSql);
            return dRow;
        }

        public DataTable GetProductPurchased()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select *  from masterproduct mp, detailpurchase dp where mp.ProductID = dp.ProductID";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from voucherpurchase where purchaseID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

    }
}
