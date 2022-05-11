using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBStockIn
    {
        #region Constructor
        public DBStockIn()
        {
        }
        #endregion

        #region Get Data
        public DataTable GetOverviewData(string DbntType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a, masteraccount b " +
                            "where a.AccountId = b.AccountId and a.VoucherType = " + "'" + DbntType + "'" + "  order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataStockIN(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a left outer join masteraccount b " +
                            " on a.AccountID = b.AccountID where  a.VoucherType = '"+ FixAccounts.VoucherTypeForStockIN +"' && a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "' order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForParty(string accID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a, masteraccount b " +
                            "where a.AccountId = b.AccountId &&  a.VoucherType = '" + FixAccounts.VoucherTypeForStockIN + "' order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from vouchercreditdebitnote where CRDBID='{0}' ";
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
                string strsql = "Select distinct a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdCompShortName,a.ProdClosingStock,b.StockID,b.BatchNumber,b.Quantity,b.Quantity as oldquantity, " +
                             "b.PurchaseRate,b.MRP,b.SaleRate,b.TradeRate,b.Expiry,b.ReasonCode,b.ExpiryDate,b.VATPer,b.Amount " +
                                "from detailcreditdebitnotestock b ,masterproduct a  where b.ProductId = a.ProductId  && " +
                                  " b.MasterID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }

        public DataRow ReadDetailsByVouNumber(int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select * from vouchercreditdebitnote where VoucherNumber ='{0}' && voucherType = '" + FixAccounts.VoucherTypeForStockIN + "'";
                strSql = string.Format(strSql, vouno);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        #endregion

        #region write Data
        public bool AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd,createdby,createddate,createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsProducts(string Id, string stockid, string ProductId, string Batchno, int quantity, int SchemeQuantity,
              double PurchaseRate, double MRP, double SaleRate,double TradeRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate,string MyDetailCRDBID, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryProducts(Id,stockid, ProductId, Batchno, quantity, SchemeQuantity, PurchaseRate, MRP, SaleRate,TradeRate, Expiry, ExpiryDate, reasoncode, VatPer, Amount, VouType, VouNo, VouDate,MyDetailCRDBID, serialNumber);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd,string modfiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd,modfiedby,modifieddate,modifiedtime);
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
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, string createdby, string createddate, string createdtime)
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
            objQuery.AddToQuery("VAT5", Vat5);
            objQuery.AddToQuery("VAT12point5", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountClear", 0);
            objQuery.AddToQuery("ClearedInVoucherNumber", 0);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryProducts(string Id, string stockid, string ProductId, string Batchno, int quantity, int SchemeQuantity,
              double PurchaseRate, double MRP, double SaleRate, double TradeRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate, string MyDetailCRDBID, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcreditdebitnotestock";
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("ProductID", ProductId);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("Quantity", quantity);           
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("ReasonCode", reasoncode);
            objQuery.AddToQuery("VATPer", VatPer);
            objQuery.AddToQuery("Amount", Amount);
            objQuery.AddToQuery("SchemeQuantity", 0);
            objQuery.AddToQuery("DiscountPercent",0.00);
            objQuery.AddToQuery("DiscountAmount",0.00);
            objQuery.AddToQuery("DetailCreditDebitNoteStockID", MyDetailCRDBID);
            objQuery.AddToQuery("SerialNumber", serialNumber);

            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("CRDBID", Id, true);
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
