using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBStatements
    {

        public DBStatements()
        {
        }

        public DataTable GetOverviewDataPurchase15Days(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.VoucherNumber,a.VoucherDate,a.PurchaseBillNumber,a.AccountID,a.AmountNet,a.StatementNumber,a.VoucherType,a.AmountVAT5Percent,a.AmountVAT12point5Percent,b.AccName,b.AccAddress1 from voucherpurchase a inner join masteraccount b on a.AccountID = b.AccountID where ( a.VoucherType =  '" + FixAccounts.VoucherTypeForCreditStatementPurchase + "' && a.StatementNumber = 0 && a.Voucherdate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "') Order By b.AccName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataSale(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.StatementNumber,a.VoucherType,b.AccName,b.AccAddress1 from vouchersale a inner join masteraccount b on a.AccountID = b.AccountID where ( a.VoucherType =  '" + FixAccounts.VoucherTypeForCreditStatementSale + "' && a.StatementNumber = 0 && a.Voucherdate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "') Order By b.AccName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataPurchase15DaysForView(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.VoucherNumber,a.PurchaseBillNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.StatementNumber,a.VoucherType,a.AmountVAT5Percent,a.AmountVAT12point5Percent,b.AccName,b.AccAddress1 from voucherpurchase a inner join masteraccount b on a.AccountID = b.AccountID where ( a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementPurchase + "'  && a.StatementNumber != 0 && a.Voucherdate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "' ) Order By b.AccName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataSaleForView(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.StatementNumber,a.VoucherType,b.AccName,b.AccAddress1,b.AccAddress2,b.AccTelephone from vouchersale a inner join masteraccount b on a.AccountID = b.AccountID where ( a.VoucherType = '" + FixAccounts.VoucherTypeForCreditStatementSale + "'  && a.StatementNumber != 0 && a.Voucherdate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "' ) Order By a.StatementNumber ,a.VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataBothStatementForView(string voutype, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherDate,a.Vat5Per,a.VAt12point5per,a.FromDate,a.ToDate,a.AccountID,a.AmountNet,a.NumberofBills,b.AccName,b.AccAddress1 from voucherstatement a inner join masteraccount b on a.AccountID = b.AccountID where ( a.VoucherType =  '" + voutype + "' && a.FromDate >= '" + fromDate + "' && a.ToDate <= '" + toDate + "') Order By a.VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataByType(string vouType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherType,a.VoucherDate,a.FromDate,a.ToDate,a.AccountID,a.AmountNet,a.NumberofBills,b.AccName,b.AccAddress1 from voucherstatement a inner join masteraccount b on a.AccountID = b.AccountID where ( a.VoucherType =  '" + vouType + "' && a.FromDate >= '" + General.ShopDetail.Shopsy + "' && a.ToDate <= '" + General.ShopDetail.Shopey  + "') Order By a.VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByID(string id)
        {
            DataRow dr;
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherDate,a.FromDate,a.ToDate,a.AccountID,a.AmountNet,a.AmountClear,a.NumberofBills,b.AccName,b.AccAddress1 from voucherstatement a inner join masteraccount b on a.AccountID = b.AccountID where  a.ID =  '" + id  + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }

        public DataTable GetOverviewDataSaleStatementForView(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.NumberofBills,b.AccName,b.AccAddress1 from voucherstatement a inner join masteraccount b on a.AccountID = b.AccountID where ( a.VoucherType =  '" + FixAccounts.VoucherTypeForStatementSale + "' && a.FromDate = '" + fromDate + "' && a.ToDate = '" + toDate + "') Order By a.VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow GetDetailsFromMaster(int statementNumber,string voucherType)
        {
            DataRow drow = null;          
            string strSql = "Select * from voucherstatement  where vouchernumber = " + statementNumber + " && vouchertype = '"+ voucherType + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            return drow;
        }

        public DataRow GetDetailsByID(string ID)
        {
            DataRow drow = null;
            string strSql = "Select * from voucherstatement  where ID = '" + ID + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            return drow;
        }

        public bool CheckForLastNumberPurchase(int laststatementnumber)
        {
            bool bRetValue = true;
            string strSql = "Select * from voucherstatement where vouchernumber > " + laststatementnumber;
            DataTable dtable = new DataTable();
            dtable = DBInterface.SelectDataTable(strSql);
            if (dtable != null && dtable.Rows.Count > 0)
                bRetValue = false;
            return bRetValue;
        }

        public bool CheckForLastNumberSale(int laststatementnumber)
        {
            bool bRetValue = true;
            string strSql = "Select * from voucherstatement where vouchernumber > " + laststatementnumber;
            DataTable dtable = new DataTable();
            dtable = DBInterface.SelectDataTable(strSql);
            if (dtable != null && dtable.Rows.Count > 0)
                bRetValue = false;
            return bRetValue;
        }
        public bool AddDetails(string Id, int StatementNumber, string AccountID, double StatementAmount,int noofBills, string FromDate, string ToDate, double Vat5Percent, double Vat12point5Percent,string VoucherSeries,string VoucherType,string CreatedBy, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, StatementNumber,AccountID, StatementAmount,noofBills, FromDate, ToDate, Vat5Percent, Vat12point5Percent,VoucherSeries,VoucherType, CreatedBy, CreatedDate, CreatedTime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddStatementNumberInPurchaseVoucher(string purchaseID, int statementnumber, string statementID)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryStatementPurchase(purchaseID, statementnumber, statementID);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddStatementNumberInSaleVoucher(string SaleID, int statementnumber)
        {
            bool bRetValue = false;
            string strSql = "update vouchersale set statementnumber = " + statementnumber + "  where ID = '" + SaleID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddStatementNumberInSaleVoucher(string SaleID, int statementnumber, string statementID)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryStatementSale(SaleID, statementnumber, statementID);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateLastStatementNumberInTblVoucherNumbersPurchase(int laststatementnumber,string VoucherSeries)
        {
            bool bRetValue = false;
            string strSql = "update tblvouchernumbers set StatementPurchase = " + laststatementnumber + " Where Id = '"+ VoucherSeries +"'" ;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateLastStatementNumberInTblVoucherNumbersSale(int laststatementnumber, string VoucherSeries)
        {
            bool bRetValue = false;
            string strSql = "update tblvouchernumbers set StatementSale = " + laststatementnumber + " Where Id = '" + VoucherSeries + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetInsertQuery(string Id, int StatementNumber,string AccountID, double StatementAmount, int noofBills, string FromDate, string ToDate, double Vat5Percent, double Vat12point5Percent,string VoucherSeries,string VoucherType, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "VoucherStatement";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("VoucherSeries",VoucherSeries);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", StatementNumber);
            objQuery.AddToQuery("VoucherDate", General.TodayString);
            objQuery.AddToQuery("FromDate", FromDate);
            objQuery.AddToQuery("ToDate", ToDate);
            objQuery.AddToQuery("AccountID", AccountID);
            objQuery.AddToQuery("AmountNet", StatementAmount);
            objQuery.AddToQuery("NumberofBills", noofBills);
            objQuery.AddToQuery("AmountClear", 0);
            objQuery.AddToQuery("AmountBalance", StatementAmount);
            objQuery.AddToQuery("VAT5Per", Vat5Percent);
            //objQuery.AddToQuery("AmountVAT5Per",);
            objQuery.AddToQuery("VAT12Point5Per", Vat12point5Percent);
           // objQuery.AddToQuery("AmountVAT12Point5Per",);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            return objQuery.InsertQuery();
        }

        public bool DeleteStatementsPurchase(int fromno, int tono, string voutype,string vouseries)
        {
            bool bRetValue = false;
            string strSql = "Delete from voucherstatement Where vouchernumber >= " +fromno + " && vouchernumber <= "+ tono + " && VoucherType = '" + voutype +"' && voucherseries = '"+ vouseries +"'" ;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool DeleteStatementsPurchase(string statementID)
        {
            bool bRetValue = false;
            string strSql = "Delete from voucherstatement Where ID = '" + statementID  + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeleteStatementsSale(int fromno, int tono, string voutype, string vouseries)
        {
            bool bRetValue = false;
            string strSql = "Delete from voucherstatement Where vouchernumber >= " + fromno + " && vouchernumber <= " + tono + " && VoucherType = '" + voutype + "' && voucherseries = '" + vouseries + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool DeleteStatementsSale(string statementID)
        {
            bool bRetValue = false;
            string strSql = "Delete from voucherstatement Where ID = '" + statementID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool RemoveStatementNumbersFrommasterPurchase(int fromno, int tono, string vouseries)
        {
            bool bRetValue = false;
            string strSql = "update voucherpurchase set statementnumber = 0 Where statementnumber >= " + fromno + " && statementnumber <= " + tono + " && voucherseries = '"+ vouseries +"'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool RemoveStatementNumbersFrommasterPurchase(int statementNumber, string voucherSeries)
        {
            bool bRetValue = false;
            string strSql = "update voucherpurchase set statementnumber = 0 Where statementnumber = " + statementNumber + " &&  voucherseries = '" + voucherSeries + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool RemoveStatementNumbersFrommasterSale(int fromno, int tono, string vouseries)
        {
            bool bRetValue = false;
            string strSql = "update vouchersale set statementnumber = 0 Where statementnumber >= " + fromno + " && statementnumber <= " + tono + " && voucherseries = '" + vouseries + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool RemoveStatementNumbersFrommasterSale(int statementNumber, string voucherSeries)
        {
            bool bRetValue = false;
            string strSql = "update vouchersale set statementnumber = 0 Where statementnumber = " + statementNumber + " &&  voucherseries = '" + voucherSeries + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetUpdateQueryStatementPurchase(string purchaseID, int statementNumber, string statementID)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherpurchase";
            objQuery.AddToQuery("PurchaseID", purchaseID,true);
            objQuery.AddToQuery("StatementNumber", statementNumber);
            objQuery.AddToQuery("StatementID", statementID);          
            return objQuery.UpdateQuery();
        }

        private string GetUpdateQueryStatementSale(string saleID, int statementNumber, string statementID)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchersale";
            objQuery.AddToQuery("ID", saleID, true);
            objQuery.AddToQuery("StatementNumber", statementNumber);
            objQuery.AddToQuery("StatementID", statementID);
            return objQuery.UpdateQuery();
        }

        public bool AddAccountDetails(string VoucherId, string DetailID, string accountid, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForAccountDetails(VoucherId, DetailID, accountid, debit, credit, accaccountid, VoucherType, VoucherNumber, transactiondate, Narration, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }
        private string GetInsertQueryForAccountDetails(string purchaseID, string DetailID, string accountID, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("tblTrnacID", DetailID);
            objQuery.AddToQuery("VoucherID", purchaseID);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("Debit", debit);
            objQuery.AddToQuery("Credit", credit);
            objQuery.AddToQuery("AccAccountID", accaccountid);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("TransactionDate", transactiondate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
    }
}
