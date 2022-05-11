using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBAccountDetails
    {
        # region save in tbltrnac
        public DBAccountDetails()
        {
        }

        # region Purchase

        public bool AddDetailsForAccountsPurchase(string VoucherId, string DetailID, string accountid, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string voucherSubType, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForAccountsPurchase(VoucherId, DetailID, accountid, debit, credit, accaccountid, VoucherType, VoucherNumber, transactiondate, Narration, voucherSubType, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        public bool DeleteAccountDetailsPurchase(string VoucherId)
        {
            bool returnVal = false;
            string strSql = "Delete from tbltrnac where VoucherID = '" + VoucherId + "'";
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


        private string GetInsertQueryForAccountsPurchase(string purchaseID, string DetailID, string accountID, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string voucherSubType, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
         //   objQuery.AddToQuery("tblTrnacID", DetailID);
            objQuery.AddToQuery("VoucherID", purchaseID);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("Debit", debit);
            objQuery.AddToQuery("Credit", credit);
            objQuery.AddToQuery("AccAccountID", accaccountid);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("TransactionDate", transactiondate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("ReferenceVoucherID","");
            objQuery.AddToQuery("VoucherSubType", voucherSubType);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
        #endregion

        # region BankPayment

        public bool AddDetailsForAccountsBankPayment(int VoucherId, string DetailID, string accountid, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string chqno, string chqdate, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForAccountsBankPayment(VoucherId, DetailID, accountid, debit, credit, accaccountid, VoucherType, VoucherNumber, transactiondate, Narration, chqno, chqdate, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        private string GetInsertQueryForAccountsBankPayment(int purchaseID, string DetailID, string accountID, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string chqno, string chqdate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
           // objQuery.AddToQuery("tblTrnacID", DetailID);
            objQuery.AddToQuery("VoucherID", purchaseID);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("Debit", debit);
            objQuery.AddToQuery("Credit", credit);
            objQuery.AddToQuery("AccAccountID", accaccountid);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("TransactionDate", transactiondate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("ChequeNumber", chqno);
            objQuery.AddToQuery("ChequeDate", chqdate);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }



        public bool UpdateDetailsForAccountsBankPaymentForFifth(string VoucherId, string DetailID, string accountid, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string chqno, string chqdate, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryForAccountsBankPaymentForFifth(VoucherId, DetailID, accountid, debit, credit, accaccountid, VoucherType, VoucherNumber, transactiondate, Narration, chqno, chqdate, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        private string GetUpdateQueryForAccountsBankPaymentForFifth(string purchaseID, string DetailID, string accountID, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string chqno, string chqdate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("VoucherID", purchaseID, true);
            objQuery.AddToQuery("TransactionDate", transactiondate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("ChequeNumber", chqno);
            objQuery.AddToQuery("ChequeDate", chqdate);
            return objQuery.UpdateQuery();
        }


        #endregion

        # region cashpayment

        public bool AddDetailsForAccountsCashPayment(int VoucherId, string DetailID, string accountid, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForAccountsCashPayment(VoucherId, DetailID, accountid, debit, credit, accaccountid, VoucherType, VoucherNumber, transactiondate, Narration, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }



        private string GetInsertQueryForAccountsCashPayment(int purchaseID, string DetailID, string accountID, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
           // objQuery.AddToQuery("tblTrnacID", DetailID);
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

        #endregion

        # region cashReceipt

        public bool AddDetailsForAccountsCashReceipt(int VoucherId, string DetailID, string accountid, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForAccountsCashReceipt(VoucherId, DetailID, accountid, debit, credit, accaccountid, VoucherType, VoucherNumber, transactiondate, Narration, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        private string GetInsertQueryForAccountsCashReceipt(int voucherID, string DetailID, string accountID, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            //objQuery.AddToQuery("tblTrnacID", DetailID);
            objQuery.AddToQuery("VoucherID", voucherID);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("Debit", debit);
            objQuery.AddToQuery("Credit", credit);
            objQuery.AddToQuery("AccAccountID", accaccountid);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("TransactionDate", transactiondate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        #endregion

        # region BankReceipt

        public bool AddDetailsForAccountsBankReceipt(int VoucherId, string DetailID, string accountid, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string BankID, string BranchID, string ChequeNumber, string ChequeDate, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForAccountsBankReceipt(VoucherId, DetailID, accountid, debit, credit, accaccountid, VoucherType, VoucherNumber, transactiondate, Narration, BankID, BranchID, ChequeNumber, ChequeDate, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        private string GetInsertQueryForAccountsBankReceipt(int purchaseID, string DetailID, string accountID, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string BankID, string BranchID, string ChequeNumber, string ChequeDate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            //objQuery.AddToQuery("tblTrnacID", DetailID);
            objQuery.AddToQuery("VoucherID", purchaseID);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("Debit", debit);
            objQuery.AddToQuery("Credit", credit);
            objQuery.AddToQuery("AccAccountID", accaccountid);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("TransactionDate", transactiondate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("BankID", BankID);
            objQuery.AddToQuery("BranchID", BranchID);
            objQuery.AddToQuery("ChequeNumber", ChequeNumber);
            objQuery.AddToQuery("ChequeDate", ChequeDate);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }


        public bool UpdateAccountDetailsIntbltrnacForFifth(string VoucherId, string DetailID, string accountid, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string BankID, string BranchID, string ChequeNumber, string ChequeDate, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryForFifth(VoucherId, DetailID, accountid, debit, credit, accaccountid, VoucherType, VoucherNumber, transactiondate, Narration, BankID, BranchID, ChequeNumber, ChequeDate, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        private string GetUpdateQueryForFifth(string purchaseID, string DetailID, string accountID, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string BankID, string BranchID, string ChequeNumber, string ChequeDate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("VoucherID", purchaseID, true);
            objQuery.AddToQuery("AccAccountID", accaccountid);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("TransactionDate", transactiondate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("BankID", BankID);
            objQuery.AddToQuery("BranchID", BranchID);
            objQuery.AddToQuery("ChequeNumber", ChequeNumber);
            objQuery.AddToQuery("ChequeDate", ChequeDate);
            return objQuery.UpdateQuery();
        }

        public bool AddDetailsForAccountsDebitNote(string VoucherId, string DetailID, string accountid, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForAccountsDebitNote(VoucherId, DetailID, accountid, debit, credit, accaccountid, VoucherType, VoucherNumber, transactiondate, Narration, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }
        private string GetInsertQueryForAccountsDebitNote(string voucherID, string DetailID, string accountID, double debit, double credit, string accaccountid, string VoucherType, int VoucherNumber, string transactiondate, string Narration, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("tblTrnacID", DetailID);
            objQuery.AddToQuery("VoucherID", voucherID);
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
        #endregion

        # region delete
        public bool DeleteAccountDetailsFromtbltrnac(string VoucherId)
        {
            bool returnVal = false;
            string strSql = "Delete from tbltrnac where VoucherID = '" + VoucherId + "'";
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
            strSql = "Delete from tbltrnac where ReferenceVoucherID = '" + VoucherId + "'";
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
        #endregion

        # endregion

        # region Reports


        public DataTable GetGeneralLedger(string fromDate, string toDate)
        {
            DataTable dt = new DataTable();
            string strSql = "Select a.VoucherID, a.AccountID,a.Debit,a.Credit,a.AccAccountID,a.TransactionDate, " +
                 "a.VoucherType,a.VoucherSubType,a.VoucherNumber,a.VoucherDate,a.Narration,a.ChequeNumber,a.ChequeDate,a.BankID,a.BranchID,a.CreatedDate,a.CreatedTime, b.AccountID,b.AccName,b.AccCode,c.BankName,d.BranchName from tbltrnac a inner join masteraccount b on a.AccAccountID = b.AccountID left outer join masterbank c on a.BankID = c.BankID left outer join masterbranch d on a.BranchID = d.BranchID  where a.TransactionDate >= '" + fromDate + "' && a.TransactionDate <= '" + toDate + "'order by a.TransactionDate,a.CreatedTime";

            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetTrialBalanceOPDBCRFromMaster()
        {
            DataTable dt = new DataTable();
            string strSql = "Select AccountID,AccName,AccAddress1,AccCode,AccOpeningDebit,AccOpeningCredit,AccGroupID from masteraccount order by accName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetTrialBalanceOPDBCRFromTransaction(string fromDate)
        {
            DataTable dt = new DataTable();
            string strSql = "Select AccountID,sum(Debit) as debit,sum(Credit) as credit from tbltrnac where TransactionDate < '" + fromDate + "' group by AccountID order by AccountID";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetTrialBalanceTRDBCRFromTransaction(string fromDate, string toDate)
        {
            DataTable dt = new DataTable();
            string strSql = "Select AccountID,sum(Debit) as debit,sum(Credit) as credit,TransactionDate from tbltrnac where TransactionDate >= '" + fromDate + "' && TransactionDate <= '" + toDate + "' group by AccountID order by AccountID";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetTrialBalanceLevel4()
        {
            DataTable dt = new DataTable();
            //string strSql = "Select GroupID,GroupName,GroupCode,UnderGroupID,LevelNumber,BalanceSheetCode,ShowInBalanceSheet,BalanceSheetSrNumber from mastergroup order by UnderGroupID";
            string strSql = "Select AccGroupId,sum(AccClosingDebit) as ClosingDebit, sum(AccClosingCredit) as ClosingCredit from  masteraccount group by AccGroupId";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        //public DataTable GetUnion(DataTable op, DataTable optr, DataTable trtr)
        //{
        //    DataTable dt = new DataTable();
        //    string strSql = "Select * from  op ";
        //    dt = DBInterface.SelectDataTable(strSql);
        //    return dt;
        //}
        // public DataTable GetTrialBalanceTransactions(string fromDate, string toDate)
        //{
        //    DataTable dt = new DataTable();
        //    //string strSql = "Select a.AccountID,sum(a.Debit) as debit,sum(a.Credit) as credit,a.TransactionDate, " +
        //    //     "b.AccountID,b.AccName,b.AccCode,b.AccOpeningDebit,b.AccOpeningCredit from tbltrnac a inner join masteraccount b on a.AccAccountID = b.AccountID left outer join masterbank c on a.BankID = c.BankID left outer join masterbranch d on a.BranchID = d.BranchID  where a.TransactionDate >= '" + fromDate + "' && a.TransactionDate <= '" + toDate + "' group by b.AccountID order by b.AccName";
        //    string strSql = "Select a.AccountID,sum(a.Debit) as debit,sum(a.Credit) as credit,a.TransactionDate, " +
        //         "b.AccountID,b.AccName,b.AccCode,b.AccOpeningDebit,b.AccOpeningCredit from tbltrnac a inner join masteraccount b on a.AccAccountID = b.AccountID  where a.TransactionDate >= '" + fromDate + "' && a.TransactionDate <= '" + toDate + "' group by b.AccountID order by b.AccName";
        //    dt = DBInterface.SelectDataTable(strSql);
        //    return dt;
        //}
        public DataTable GetGeneralLedgerByClearedDate(string fromDate, string toDate, string accountID)
        {
            DataTable dt = new DataTable();
            string strSql = "Select a.VoucherID, a.AccountID,a.Debit,a.Credit,a.AccAccountID,a.TransactionDate, ClearedDate," +
                 "a.VoucherType,a.VoucherSubType,a.VoucherNumber,a.VoucherDate,a.Narration,a.ChequeNumber,a.ChequeDate,a.BankID,a.BranchID,a.CreatedDate,a.CreatedTime, b.AccountID,b.AccName,b.AccCode,c.BankName,d.BranchName from tbltrnac a inner join masteraccount b on a.AccAccountID = b.AccountID left outer join masterbank c on a.BankID = c.BankID left outer join masterbranch d on a.BranchID = d.BranchID  where  a.AccountID = '" + accountID + "' &&  a.ClearedDate != ''  &&  a.ClearedDate <= '" + toDate + "'order by a.TransactionDate,a.CreatedTime";

            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetDayTotalForDailyClosing(string fromDate, string toDate, string accountID)
        {
            DataTable dt = new DataTable();
            string strSql = "Select sum(Debit) as debit ,sum(Credit) as credit,TransactionDate from tbltrnac where  AccountID = '" + accountID + "' && TransactionDate <= '" + toDate + "' group by TransactionDate";

            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }


        public DataTable GetGeneralLedger(string fromDate, string toDate, string accountID)
        {
            DataTable dt = new DataTable();
            string strSql = "Select a.VoucherID, a.AccountID,a.Debit,a.Credit,a.AccAccountID,a.TransactionDate, " +
                            "a.VoucherType,a.VoucherSubType,a.VoucherNumber,a.VoucherDate,a.Narration,a.ChequeNumber,a.ChequeDate,a.BankID,a.BranchID,a.CreatedDate,a.CreatedTime, b.AccountID,b.AccName,b.AccCode,c.BankName,d.BranchName from tbltrnac a inner join masteraccount b on a.AccAccountID = b.AccountID left outer join masterbank c on a.BankID = c.BankID left outer join masterbranch d on a.BranchID = d.BranchID  where  a.AccountID = '" + accountID + "' &&  a.TransactionDate <= '" + toDate + "'order by a.TransactionDate,a.CreatedTime";
        
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetSundryDebtors(string mdate)
        {
            DataTable dt = new DataTable();
            string strSql = "Select  a.AccountID,a.AccCode,b.AccountID,sum(b.Debit) as debit ,sum(b.Credit) as credit ,b.TransactionDate " + "   from masteraccount a left outer join  tbltrnac b  on a.AccountID = b.AccountID   where a.AccCode = 'D' && ((b.vouchertype != '" + FixAccounts.VoucherTypeForCashSale + "' ) || (b.VoucherType = '"+ FixAccounts.VoucherTypeForCreditSale +"'  &&  b.VoucherSubtype != 'C' ) ) Group by a.AccountID  having (b.TransactionDate <= '" + mdate + "' || b.transactiondate = '' )  order by a.AccName";

            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetSundryCreditors(string mdate)
        {
            DataTable dt = new DataTable();
            string strSql = "Select  a.AccountID,a.AccCode, b.AccountID,sum(b.Debit) as debit ,sum(b.Credit) as credit ,b.TransactionDate " +
                  "   from masteraccount a  left outer join  tbltrnac b  on a.AccountID = b.AccountID   where  a.AccCode = 'C'  Group by a.AccountID having b.TransactionDate <= '" + mdate + "' || b.transactiondate = '' order by a.AccName";

            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public bool InitializeDBCRFieldsInMasterAccount()
        {
            bool bRetValue = false;
            string strSql = "update masteraccount set acctransactionopeningDb = 0, acctransactionopeningCR = 0," +
                " AccTransactionDebit = 0 , AccTransactionCredit = 0, AccClosingDebit = 0, AccClosingCredit = 0";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;

            strSql = "update mastergroup set OpeningDebit = 0, OpeningCredit = 0," +
                " TransactionDebit = 0 , TransactionCredit = 0, ClosingDebit = 0, ClosingCredit = 0";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;

        }
        #endregion



        public bool UpdateTransactionOP(string accountID, double debitsum, double creditsum)
        {
            bool bRetValue = false;
            string strSql = "update masteraccount set acctransactionopeningDb = acctransactionopeningDb + " + debitsum + ", acctransactionopeningCR = acctransactionopeningCR + " + creditsum + "  where AccountID = '" + accountID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        public bool UpdateTransactionTR(string accountID, double debitsum, double creditsum)
        {
            bool bRetValue = false;
            string strSql = "update masteraccount set AccTransactionDebit = AccTransactionDebit + " + debitsum + ", AccTransactionCredit = AccTransactionCredit + " + creditsum + "  where AccountID = '" + accountID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            return bRetValue;
        }

        public bool CalculateClosingBalanceInmasterAccount()
        {
            bool bRetValue = false;
            string strSql = "update masteraccount set AccClosingDebit = (AccopeningDebit+acctransactionopeningDb+AccTransactionDebit) - ( AccopeningCredit +acctransactionopeningCR+AccTransactionCredit)";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;
            //
            //strSql = "update masteraccount set AccClosingDebit = AccClosingDebit - AccClosingCredit";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //    bRetValue = true;
            //
            //strSql = "update masteraccount set AccClosingCredit = 0 where  AccClosingDebit > 0";

            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //    bRetValue = true;

            strSql = "update masteraccount set AccClosingCredit = AccClosingDebit  * (-1) where  AccClosingDebit < 0";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;

            strSql = "update masteraccount set AccClosingDebit = 0 where  AccClosingDebit < 0";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true; 
            

            return bRetValue;
        }

        public DataTable GetDetailsFromMasterAccount()
        {
            DataTable dt = new DataTable();
            string strSql = "Select  AccountID,AccName,AccCode,AccAddress1,Accopeningdebit ,Accopeningcredit,AccTransactionOpeningDB,AccTransactionOpeningCR,AccTransactionDebit,AccTransactionCredit, AccClosingDebit, AccClosingCredit,AccGroupID from masterAccount order by AccName";

            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetDetailsFromMasterAccount(string accCode)
        {
            DataTable dt = new DataTable();
            string strSql = "Select  AccountID,AccName,AccCode,AccAddress1,Accopeningdebit ,Accopeningcredit,AccTransactionOpeningDB,AccTransactionOpeningCR,AccTransactionDebit,AccTransactionCredit, AccClosingDebit, AccClosingCredit,AccGroupID from masterAccount where accCode = '"+ accCode +"' order by AccName";

            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetTotalsinmasterAccount()        {
           
            DataTable dt = new DataTable();
            string strSql = "select sum(AccOpeningDebit) as opdebit, sum(AccOpeningCredit) as opcredit, sum(AcctransactionopeningDb) as optrdebit, sum(acctransactionopeningCR) as optrcredit, sum(AccTransactionDebit) as trdebit, sum(AccTransactioncredit) as trcredit, sum(AccClosingDebit) as cldebit, sum(AccClosingCredit) as clcredit from masteraccount ";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;

        }

        public DataTable UpdatebalancesInMasterGroup()
        {           
            DataTable dt = new DataTable();
            string strSql = "select AccgroupID, sum(AccOpeningDebit) as opdebit, sum(AccOpeningCredit) as opcredit, sum(AcctransactionopeningDb) as optrdebit, sum(acctransactionopeningCR) as optrcredit, sum(AccTransactionDebit) as trdebit, sum(AccTransactioncredit) as trcredit, sum(AccClosingDebit) as cldebit, sum(AccClosingCredit) as clcredit from masteraccount group by AccgroupID";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;

        }

        public bool UpdateMasterGroup(string mgroupID, double mopeningdb, double mopeningcr, double mtransactiondb, double mtransactioncr, double mclosingdb, double mclosingcr)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(mgroupID, mopeningdb, mopeningcr, mtransactiondb, mtransactioncr, mclosingdb, mclosingcr);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public string GetUpdateQuery(string mgroupID, double mopeningdb, double mopeningcr, double mtransactiondb, double mtransactioncr, double mclosingdb, double mclosingcr)
        {
            Query objQuery = new Query();
            objQuery.Table = "mastergroup";
            objQuery.AddToQuery("GroupID", mgroupID, true);
            objQuery.AddToQuery("OpeningDebit", mopeningdb);
            objQuery.AddToQuery("OpeningCredit", mopeningcr);
            objQuery.AddToQuery("TransactionDebit", mtransactiondb);
            objQuery.AddToQuery("TransactionCredit", mtransactioncr);
            objQuery.AddToQuery("ClosingDebit", mclosingdb);
            objQuery.AddToQuery("ClosingCredit", mclosingcr);            
            return objQuery.UpdateQuery();
        }

      

        public bool UpdateMasterGrouplevel2(string mgroupID, double mopeningdb, double mopeningcr, double mtransactiondb, double mtransactioncr, double mclosingdb, double mclosingcr)
        {
            bool bRetValue = false;
            string strSql = "Update mastergroup set openingdebit = openingDebit + "+ mopeningdb + ", openingcredit = openingcredit + " + mopeningcr + " , transactiondebit = transactiondebit + " + mtransactiondb + ", transactioncredit = transactioncredit + " + mtransactioncr + ", closingdebit = closingdebit + "+ mclosingdb + ",closingcredit = closingcredit + " + mclosingcr + " where groupId = '"+ mgroupID +"'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public DataTable Gettotalsinmastergroup()
        {
           
            DataTable dt = new DataTable();
            string strSql = "select sum(OpeningDebit) as opdebit, sum(OpeningCredit) as opcredit, sum(TransactionDebit) as trdebit, sum(Transactioncredit) as trcredit, sum(ClosingDebit) as cldebit, sum(ClosingCredit) as clcredit from mastergroup ";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetTotalsForLevel3()
        {           
            DataTable dt = new DataTable();
            string strSql = "select UndergroupID,UnderGroupIDParentID, sum(OpeningDebit) as opdebit, sum(OpeningCredit) as opcredit, sum(TransactionDebit) as trdebit, sum(Transactioncredit) as trcredit, sum(ClosingDebit) as cldebit, sum(ClosingCredit) as clcredit from mastergroup group by UnderGroupID ";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetTotalsForLevel2()
        {           
            DataTable dt = new DataTable();
            string strSql = "select UndergroupID,UnderGroupIDParentID, sum(OpeningDebit) as opdebit, sum(OpeningCredit) as opcredit, sum(TransactionDebit) as trdebit, sum(Transactioncredit) as trcredit, sum(ClosingDebit) as cldebit, sum(ClosingCredit) as clcredit from mastergroup group by UnderGroupID ";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetOverviewGroupMaster()
        {          
            DataTable dt = new DataTable();
            string strSql = "select * from masterGroup group by GroupName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetGroupsUnderLevel2(int GrpID2)
        {
            DataTable dt = new DataTable();
            string strSql = "select groupID,GroupName,UnderGroupID,OpeningDebit,OpeningCredit,TransactionDebit,TransactionCredit,ClosingDebit,ClosingCredit,'' as AccountID from masterGroup where UnderGroupID = '"+ GrpID2 + "'" +
                " union select AccgroupID as groupID ,AccName as GroupName ,'' as UnderGroupID,AccOpeningDebit as OpeningDebit,AccOpeningCredit as OpeningCredit,AccTransactionDebit as TransactionDebit,AccTransactionCredit as TransactionCredit,AccClosingDebit as ClosingDebit,AccClosingCredit as ClosingCredit,AccountID from masterAccount where AccGroupID = '" + GrpID2 + "' && (AccOpeningDebit > 0 || AccOpeningCredit > 0 || AccTransactionDebit > 0 || AccTransactionCredit > 0 || AccClosingDebit > 0 || AccClosingCredit > 0) order by groupName ";
            dt = DBInterface.SelectDataTable(strSql);
            return dt; 
        }

        public DataTable GetGroupsUnderLevel3(int GrpID3)
        {
            DataTable dt = new DataTable();
            string strSql = "select groupID,GroupName,UnderGroupID,OpeningDebit,OpeningCredit,TransactionDebit,TransactionCredit,ClosingDebit,ClosingCredit from masterGroup where UnderGroupID = '" + GrpID3 + "'" +
                " union select AccgroupID as groupID ,AccName as GroupName ,'' as UnderGroupID,AccOpeningDebit as OpeningDebit,AccOpeningCredit as OpeningCredit,AccTransactionDebit as TransactionDebit,AccTransactionCredit as TransactionCredit,AccClosingDebit as ClosingDebit,AccClosingCredit as ClosingCredit from masterAccount where AccGroupID = '" + GrpID3 + "' order by groupName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public DataTable GetTrnacTotalsByVoucherTypeNo( string fromDate, string toDate )
        {
            DataTable dt = new DataTable();
            string strSql = "select voucherID, voucherType,VoucherNumber,TransactionDate, sum(Debit) as debit,sum(Credit) as credit from tbltrnac where transactiondate >= '"+ fromDate +"' && transactiondate <= '"+ toDate +"'  group by voucherID order by transactiondate";
            dt = DBInterface.SelectDataTable(strSql);
            return dt; 
        }
        public DataTable OldyearNotFound(string voucherSeries)
        {
            DataTable dt = new DataTable();
            string strSql = "select TransactionDate from tbltrnac where TransactionDate < '" + voucherSeries + "'";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }
        public DataTable GetAccountList(string acccode)
        {
            DataTable dt = new DataTable();
            string strSql = "select accountID,Accname,Accaddress1,Acccode,AccOpeningDebit, AccOpeningCredit from masteraccount where Acccode = '"+acccode+"' order by AccName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }



        public bool CopyClosingBalanceAsOpening()
        {
            bool bRetValue = false;
            string strSql = "update masteraccount set AccOpeningDebit =  AccClosingDebit, AccOpeningCredit = AccClosingCredit,AccClearedAmount = 0";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                bRetValue = true;           
            return bRetValue;
        }

        public DataTable GetDataForEntryOfScheduledNumbers()
        {
            DataTable dt = new DataTable();
            string strSql = "select GroupID,GroupName,ScheduleNumber from mastergroup order by GroupName";
            dt = DBInterface.SelectDataTable(strSql);
            return dt;
        }

        public void SaveScheduleNumber(string mgroupid, int mschedulenumber)
        {           
            string strSql = "update mastergroup set ScheduleNumber = " + mschedulenumber + "  where GroupID = '" + mgroupid + "'";
            DBInterface.ExecuteQuery(strSql);            
        }

        public DataRow GetTotalsFromMasterAccount()
        {
            DataRow dr;
            string strSql = "select sum(accopeningdebit+AccTransactionOpeningDB) as opdb ,sum(accopeningcredit+AccTransactionOpeningCR) as opcr,sum(AccTransactionDebit) as trdb,sum(AccTransactionCredit) as trcr,sum(AccClosingDebit) as cldb,sum(AccClosingCredit) as clcr from masterAccount";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }
    }
}
