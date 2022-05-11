using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBDebitNoteExpiry
    {
        #region Constructor
        public DBDebitNoteExpiry()
        {
        }
        #endregion

        #region Get Data
        public DataTable GetOverviewData(string DbntType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a, masteraccount b " +
                            "where a.AccountId = b.AccountId and a.VoucherType = " + "'" + DbntType + "'"  + "  order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from MasterCompany where CompId='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataTable ReadProductDetailsByID(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select distinct a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,b.BatchNumber,b.Quantity, " +
                             "b.PurchaseRate,b.MRP,b.SaleRate,b.Expiry,b.SchemeQuantity,b.ReasonCode,b.ExpiryDate,b.VATPer,b.Amount " +
                                "from detailcreditdebitnotestock b ,masterproduct a  where b.ProductId = a.ProductId  and " +
                                  " b. MasterID = '{0}'";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        #endregion

        #region write Data
        public bool AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double rnd, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, rnd, createdby,createddate,createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public DataRow GetCurrentClosingStockFromMaster(string productID)
        {
            DataRow dr;
            string strSql = "Select Prodclosingstock from masterproduct where productID = '" + productID + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}
            //return closingstock;
        }
        public bool AddDetailsProducts(string Id, string ProductId, string Batchno, int quantity, int SchemeQuantity,
                double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount,  string VouType, int VouNo, string VouDate,double TradeRate,  string stockID, string MyCRDBID, double discPercent, double discAmount)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryProducts(Id, ProductId, Batchno, quantity, SchemeQuantity, PurchaseRate, MRP, SaleRate, Expiry, ExpiryDate, reasoncode, VatPer, Amount, VouType, VouNo, VouDate,TradeRate, stockID,MyCRDBID, discPercent,discAmount);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool SaveCreditorsNameID(string Id, string stockID)
        {
            bool bRetValue = false;
            string strSql =  "Update tblstock set  LastPurchaseAccountId = '" + Id +"' where StockID = '" + stockID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd,modifiedby,modifieddate,modifiedtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeleteDetails(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(Id);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeleteProductsByMasterID(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQueryProducts(Id);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        #endregion

        #region validations

        public bool CheckStock()
        {
            return true;
        }
        #endregion

        #region Query Building Functions

        private string GetInsertQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double rnd, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("CRDBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("DiscountPer", DiscPer);
            objQuery.AddToQuery("DiscountAmount", DiscAmt);
            objQuery.AddToQuery("Amount", Amt);
            objQuery.AddToQuery("VAT5", 0);
            objQuery.AddToQuery("VAT12point5", 0);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountClear", 0);
            objQuery.AddToQuery("ClearedInVoucherNumber", 0);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryProducts(string Id, string ProductId, string Batchno, int quantity, int SchemeQuantity,
          double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate,double TradeRate, string stockID, string MyCRDBID, double discPercent, double discAmount)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcreditdebitnotestock";
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("ProductID", ProductId);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("Quantity", quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("ReasonCode", reasoncode);
            objQuery.AddToQuery("VATPer", VatPer);
            objQuery.AddToQuery("VATAmount", 0);
            objQuery.AddToQuery("DiscountPercent", discPercent);
            objQuery.AddToQuery("DiscountAmount", discAmount);
            objQuery.AddToQuery("Amount", Amount);          
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("StockID", stockID);
            objQuery.AddToQuery("DetailCreditDebitNoteStockID", MyCRDBID);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("CRDBID", Id,true);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("DiscountPer", DiscPer);
            objQuery.AddToQuery("DiscountAmount", DiscAmt);
            objQuery.AddToQuery("Amount", Amt);
            objQuery.AddToQuery("VAT5", Vat5);
            objQuery.AddToQuery("VAT12point5", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("CRDBID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }

        private string GetDeleteQueryProducts(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "detailcreditdebitnotestock";
            objQuery.AddToQuery("MasterID", Id, true);       
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
        #endregion 
    }

}
