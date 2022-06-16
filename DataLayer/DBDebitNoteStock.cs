using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBDebitNoteStock
    {
        #region Constructor
        public DBDebitNoteStock()
        {
        }
        #endregion

        #region Get Data
        public DataTable GetOverviewData(string DbntType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct A.ID, a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountId,a.AmountNet,a.AmountClear,a.DiscountPer,a.DiscountAmount," +
                "a.RoundingAmount,a.Amount,a.Narration,a.ClearedInID,a.ClearedInVoucherSeries,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate," +
                "a.ClearedInPurchaseBillNumber,a.OperatorID,a.Uploaded,a.CreatedDate,a.CreatedTime,a.CreatedUserID,a.ModifiedDate,a.ModifiedTime,a.ModifiedUserID,a.ModifiedOperatorID," +
                "a.IsHold,a.TransferToAcc, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a INNER JOIN masteraccount b ON a.AccountId = b.AccountId " +
                 "where a.VoucherType = " + "'" + DbntType + "' AND a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' AND a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.voucherdate ,a.vouchernumber desc ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForAllYears(string DbntType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.VoucherSeries,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountId,a.AmountNet,a.AmountClear,a.DiscountPer,a.DiscountAmount," +
                            "a.RoundingAmount,a.Amount,a.Narration,a.ClearedInID,a.ClearedInVoucherSeries,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate," +
                            "a.ClearedInPurchaseBillNumber,a.OperatorID,a.Uploaded,a.CreatedDate,a.CreatedTime,a.CreatedUserID,a.ModifiedDate,a.ModifiedTime,a.ModifiedUserID,a.ModifiedOperatorID," +
                            "a.IsHold,a.TransferToAcc, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a INNER JOIN masteraccount b ON a.AccountId = b.AccountId " +
                            "where a.VoucherType = " + "'" + DbntType + "' AND a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.voucherdate desc,a.vouchernumber desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForParty(string accID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a, masteraccount b " +
                            "where a.AccountId = b.AccountId AND ( a.VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' OR a.VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteAmount + "' order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataDebitNotes(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate,a.ClearedInPurchaseBillNumber, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a left outer join masteraccount b " +
                            " on a.AccountID = b.AccountID where  (a.VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' OR a.VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteAmount + "') AND a.VoucherDate >= '" + fromDate + "' AND a.VoucherDate <= '" + toDate + "'  order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataDebitNotesPending(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate,a.ClearedInPurchaseBillNumber, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a left outer join masteraccount b " +
                            " on a.AccountID = b.AccountID where  (a.VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' OR a.VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteAmount + "') AND a.VoucherDate >= '" + fromDate + "' AND a.VoucherDate <= '" + toDate + "' AND (a.AmountClear = 0)order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataDebitNotesOnlyPending(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate,a.ClearedInPurchaseBillNumber, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a left outer join masteraccount b " +
                            " on a.AccountID = b.AccountID where  (a.VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' OR a.VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteAmount + "') AND ClearedInID is null AND a.VoucherDate >= '" + fromDate + "' AND a.VoucherDate <= '" + toDate + "'  order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow ReadDetailsByVouNumber(int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select * from vouchercreditdebitnote where VoucherNumber ='{0}' AND voucherType = '" + FixAccounts.VoucherTypeForDebitNoteStock + "' ";
                strSql = string.Format(strSql, vouno);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }


        public DataTable ReadProductDetailsByID(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdCompShortName,a.ProdClosingStock,b.StockID,b.StockID as LastStockID,b.BatchNumber,b.Quantity,b.Quantity+b.SchemeQuantity as oldQuantity, " +
                             "b.PurchaseRate,b.MRP,b.SaleRate,b.Expiry,b.SchemeQuantity,b.TradeRate,b.ReasonCode,b.ExpiryDate,b.VATPer,b.Amount,b.discountpercent,b.discountamount,b.TradeRate,b.AddVatInTradeRate,b.VatAmount,c.ClosingStock " +
                               "from detailcreditdebitnotestock b  inner join masterproduct a  on b.ProductID = a.ProductID  inner join tblstock c on b.StockID = c.StockID " +
                                  " where b. MasterID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        #endregion

        #region write Data
        public int AddDetails(string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, double amountClear, string voucherseries, string ClearVouType, string createdby, string createddate, string createdtime)
        {
            string strSql = GetInsertQuery(CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, amountClear, voucherseries, ClearVouType, createdby, createddate, createdtime);
            return DBInterface.ExecuteScalar(strSql);
        }

        public bool AddDetailsProducts(string Id, string StockID, int ProductID, string Batchno, int quantity, int SchemeQuantity,
             double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate, double discountpercent, double discountamount, double TradeRate, double VatAmount, string IfAddVATInTradeRate, string MydbcrID, int serialNumber, string voucherseries)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryProducts(Id, StockID, ProductID, Batchno, quantity, SchemeQuantity, PurchaseRate, MRP, SaleRate, Expiry, ExpiryDate, reasoncode, VatPer, Amount, VouType, VouNo, VouDate, discountpercent, discountamount, TradeRate, VatAmount, IfAddVATInTradeRate, MydbcrID, serialNumber, voucherseries);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double rnd, double amountClear, string voucherseries, string ClearVouType, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, rnd, amountClear, voucherseries, ClearVouType, modifiedby, modifieddate, modifiedtime);
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

        private string GetInsertQuery(string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, double amountClear, string voucherseries, string ClearVouType, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            //objQuery.AddToQuery("CRDBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", voucherseries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("DiscountPer", DiscPer);
            objQuery.AddToQuery("DiscountAmount", DiscAmt);
            objQuery.AddToQuery("Amount", Amt);
            //objQuery.AddToQuery("VAT5", Vat5);
            //objQuery.AddToQuery("VAT12point5", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountClear", amountClear);
            objQuery.AddToQuery("ClearedInVoucherType", ClearVouType);
            objQuery.AddToQuery("ClearedInVoucherNumber", 0);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryProducts(string Id, string stockid, int ProductID, string Batchno, int quantity, int SchemeQuantity,
             double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate, double discountpercent, double discountamount, double TradeRate, double VatAmount, string IfAddVATInTradeRate, string MydbcrID, int serialNumber, string voucherseries)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcreditdebitnotestock";
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("ProductID", ProductID);
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
            objQuery.AddToQuery("Amount", Amount);
            objQuery.AddToQuery("DiscountPercent", discountpercent);
            objQuery.AddToQuery("DiscountAmount", discountamount);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("VatAmount", VatAmount);
            objQuery.AddToQuery("AddVatInTradeRate", IfAddVATInTradeRate);
            //objQuery.AddToQuery("DetailCreditDebitNoteStockID", MydbcrID);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", voucherseries);
            objQuery.AddToQuery("SerialNumber", serialNumber);

            //objQuery.AddToQuery("GSTAmountZero", gstAmountZero);
            //objQuery.AddToQuery("GSTSAmount", gstSAmount);
            //objQuery.AddToQuery("GSTCAmount", gstCAmount);
            //objQuery.AddToQuery("GSTIAmount", gstIAmount);
            //objQuery.AddToQuery("GSTS", gstS);
            //objQuery.AddToQuery("GSTC", gstC);
            //objQuery.AddToQuery("GSTI", gstI);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double rnd, double amountClear, string voucherseries, string ClearVouType, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("ID", Id, true);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", voucherseries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("DiscountPer", DiscPer);
            objQuery.AddToQuery("DiscountAmount", DiscAmt);
            objQuery.AddToQuery("Amount", Amt);
            //objQuery.AddToQuery("VAT5", Vat5);
            //objQuery.AddToQuery("VAT12point5", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountClear", amountClear);
            objQuery.AddToQuery("ClearedInVoucherType", ClearVouType);
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
            objQuery.AddToQuery("Id", Id, true);
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

        public DataRow GetLastRecord(string CrdbVouType, string CrdbVouSeries)
        {
            DataRow dRow = null;

            string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' AND voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber desc";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        public DataRow GetLastVoucherNumber(string vouType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select Vouchernumber from vouchercreditdebitnote where  VoucherType =  '" + vouType + "'  AND  VoucherSeries = '" + vouSeries + "' order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetFirstRecord(string CrdbVouType, string CrdbVouSeries)
        {
            DataRow dRow = null;

            string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' AND voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber ";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }


    }

}
