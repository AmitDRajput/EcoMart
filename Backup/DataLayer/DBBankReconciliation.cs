using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
    class DBBankReconciliation
    {
        public DBBankReconciliation()
        {
        }

        public DataTable GetOverviewData(string FromDate, string ToDate, string BankID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  tblTrnacID,VoucherID,a.VoucherType,a.VoucherNumber,a.TransactionDate," +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ClearedDate,a.debit,a.credit, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from tbltrnac a inner join masteraccount b " +
                            "on a.AccAccountId = b.AccountId && a.AccountID = '" + BankID + "' && TransactionDate >= '" + FromDate + "' && TransactionDate <= '" + ToDate + "' order by a.TransactionDate ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetOverviewDataDBNew(string fromDate, string toDate, string bankID,double debitAmount)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  tblTrnacID,VoucherID,a.VoucherType,a.VoucherNumber,a.TransactionDate," +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ClearedDate,a.debit,a.credit, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from tbltrnac a inner join masteraccount b " +
                            "on a.AccAccountId = b.AccountId && a.AccountID = '" + bankID + "' && TransactionDate >= '" + fromDate + "' && TransactionDate <= '" + toDate + "' && Debit = " + debitAmount + "  && ( a.ClearedDate == "+ DBNull.Value +" ||a.ClearedDate == '')  order by a.TransactionDate ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetOverviewDataDBAll(string fromDate, string toDate, string bankID, double debitAmount)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  tblTrnacID,VoucherID,a.VoucherType,a.VoucherNumber,a.TransactionDate," +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ClearedDate,a.debit,a.credit, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from tbltrnac a inner join masteraccount b " +
                            "on a.AccAccountId = b.AccountId && a.AccountID = '" + bankID + "' && TransactionDate >= '" + fromDate + "' && TransactionDate <= '" + toDate + "' && Debit = " + debitAmount + "  order by a.TransactionDate ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }


        public DataTable GetOverviewDataCRNew(string fromDate, string toDate, string bankID, double creditAmount)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  tblTrnacID,VoucherID,a.VoucherType,a.VoucherNumber,a.TransactionDate," +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ClearedDate,a.debit,a.credit, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from tbltrnac a inner join masteraccount b " +
                            "on a.AccAccountId = b.AccountId && a.AccountID = '" + bankID + "' && TransactionDate >= '" + fromDate + "' && TransactionDate <= '" + toDate + "' && Credit = " + creditAmount + "  && ( a.ClearedDate == " + DBNull.Value + " ||a.ClearedDate == '')  order by a.TransactionDate ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetOverviewDataCRAll(string fromDate, string toDate, string bankID, double creditAmount)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  tblTrnacID,VoucherID,a.VoucherType,a.VoucherNumber,a.TransactionDate," +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ClearedDate,a.debit,a.credit, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from tbltrnac a inner join masteraccount b " +
                            "on a.AccAccountId = b.AccountId && a.AccountID = '" + bankID + "' && TransactionDate >= '" + fromDate + "' && TransactionDate <= '" + toDate + "' && Credit = " + creditAmount + "  order by a.TransactionDate ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }


        public DataTable GetOverviewDataNew(string fromDate, string toDate, string bankID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  tblTrnacID,VoucherID,a.VoucherType,a.VoucherNumber,a.TransactionDate," +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ClearedDate,a.debit,a.credit, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from tbltrnac a inner join masteraccount b " +
                            "on a.AccAccountId = b.AccountId && a.AccountID = '" + bankID + "' && TransactionDate >= '" + fromDate + "' && TransactionDate <= '" + toDate + "' && ( a.ClearedDate == " + DBNull.Value + " ||a.ClearedDate == '')  order by a.TransactionDate ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetOverviewDataAll(string fromDate, string toDate, string bankID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  tblTrnacID,VoucherID,a.VoucherType,a.VoucherNumber,a.TransactionDate," +
                            "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ClearedDate,a.debit,a.credit, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from tbltrnac a inner join masteraccount b " +
                            "on a.AccAccountId = b.AccountId && a.AccountID = '" + bankID + "' && TransactionDate >= '" + fromDate + "' && TransactionDate <= '" + toDate + "'   order by a.TransactionDate ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public bool UpdatetblTrnac(string voucherID, string clearedDate)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryFortblTrnac(voucherID,clearedDate);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetUpdateQueryFortblTrnac(string voucherID, string clearedDate)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("VoucherID", voucherID , true);
            objQuery.AddToQuery("clearedDate", clearedDate);           
            return objQuery.UpdateQuery();
        }


        public bool UpdatetblForPayment(string voucherID, string clearedDate)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryForPayment(voucherID, clearedDate);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetUpdateQueryForPayment(string voucherID, string clearedDate)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankpayment";
            objQuery.AddToQuery("CBID", voucherID, true);
            objQuery.AddToQuery("clearedDate", clearedDate);
            return objQuery.UpdateQuery();
        }

        public bool UpdatetblForReceipt(string voucherID, string clearedDate)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryForReceipt(voucherID, clearedDate);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetUpdateQueryForReceipt(string voucherID, string clearedDate)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankreceipt";
            objQuery.AddToQuery("CBID", voucherID, true);
            objQuery.AddToQuery("clearedDate", clearedDate);
            return objQuery.UpdateQuery();
        }
    }
}
