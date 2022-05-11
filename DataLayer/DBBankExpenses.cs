using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;


namespace EcoMart.DataLayer
{
    public  class DBBankExpenses
    {


        public DBBankExpenses()
        {
        }

        public DataTable GetOverviewData(string CEType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID,a.Narration, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankexpenses a, masteraccount b " +
                            "where a.AccountId = b.AccountId && a.VoucherType = " + "'" + CEType + "' && a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' && a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetOverviewData(string bankID, string VouType, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            try
            {
                string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                                "a.AccountID,a.ChequeNumber, a.ChequeDate,a.ChequeDepositedBankID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankpayment a, masteraccount b " +
                                "where a.AccountId = b.AccountId && a.VoucherType = '" + VouType + "' &&  a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "' && ChequeDepositedBankID = '" + bankID + "' order by a.vouchernumber ";

                dtable = DBInterface.SelectDataTable(strSql);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }

            return dtable;
        }
        public DataTable GetOverviewDataForSearch(string CEType ,string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID,a.Narration, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankexpenses a, masteraccount b " +
                            "where a.AccountId = b.AccountId && a.VoucherType = " + "'" + CEType + "' && a.VoucherDate >= '"+ fromDate + "' && a.VoucherDate <= '"+ toDate+"'  order by a.voucherdate desc ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.ChequeNumber,a.ChequeDate,a.ChequeDepositedBankID,a.AmountNet,a.Narration,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankexpenses a inner join masteraccount b on a.AccountID = b.AccountID where CBID = '" + Id + "' && VoucherType = '" + FixAccounts.VoucherTypeForBankExpenses + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow ReadDetailsByVouNumber(int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankexpenses a inner join masteraccount b on a.AccountID = b.AccountID where VoucherNumber= " + vouno + " && VoucherType = '" + FixAccounts.VoucherTypeForBankExpenses + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataTable GetExpensesDetailsByID(string Id)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select  distinct a.MasterID,a.AccountID,a.debit,a.credit,b.AccName,b.AccAddress1,b.AccAddress2 from detailcashbankexpenses a , masteraccount b where a.AccountID = b.AccountID  &&  a.MasterID =  " + "'" + Id + "'";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public bool AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankAccountID, string chequeNumber, string chequeDate, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, bankAccountID, chequeNumber, chequeDate, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDetailsInmaterJV(string detailId,string AccountID, string VouType, int VouNo, string VouSeries, string VouDate, double mdebit, double mcredit, string Refno,string Narration, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryformasterJV(detailId,AccountID, VouType, VouNo, VouSeries, VouDate, mdebit, mcredit, Refno,Narration, CreatedBy, CreatedDate, CreatedTime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddVoucherIntblTrnac(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string detailID, string bankAccountID, string chequeNumber, string chequeDate, string createdby, string createddate, string createdtime)
        {
            bool retValue = false;
            string strSql = GetInsertQueryTrnacForVoucher(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, detailID, bankAccountID, chequeNumber, chequeDate, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retValue = true;
            }
            return retValue;
        }

        public bool AddVoucherIntblTrnacReverse(string Id, string CreditorId, string Narration, string VouType, int VouNo,
        string VouDate, double Amt, string detailID,string bankAccountID,string chequeNumber, string chequeDate, string createdby, string createddate, string createdtime)
        {
            bool retValue = false;
            string strSql = GetInsertQueryTrnacForVoucherReverse(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, detailID, bankAccountID, chequeNumber, chequeDate, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retValue = true;
            }
            return retValue;
        }
        public bool AddDetailsP(string Id, string acId, double mdebit, double mcredit, string detailid)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryP(Id, acId, mdebit, mcredit, detailid);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddJVIntblTrnac(string Id, string acId, double mdebit, double mcredit, string detailid, string accountID, string VouDate, string VouType, string Refno, string Narration, int Vouno, string chequeNumber, string chequeDate, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForJVIntblTrnac(Id, acId, mdebit, mcredit, detailid, accountID, VouDate, VouType, Refno, Narration, Vouno, chequeNumber, chequeDate, CreatedBy, CreatedDate, CreatedTime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddJVIntblTrnacReverse(string Id, string acId, double mdebit, double mcredit, string detailid, string accountID, string VouDate, string VouType, string Refno, string Narration, int Vouno, string chequeNumber, string chequeDate, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForJVIntblTrnacReverse(Id, acId, mdebit, mcredit, detailid, accountID, VouDate, VouType, Refno, Narration, Vouno, chequeNumber,chequeDate, CreatedBy, CreatedDate, CreatedTime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string BankAccountID, string ChequeNumber, string ChequeDate, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo, VouDate, Amt, BankAccountID, ChequeNumber, ChequeDate, modifiedby, modifieddate, modifiedtime);

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

        public bool DeletePreviosRowsByID(string Id)
        {
            bool bRetValue = false;
            string strSql = "Delete from detailcashbankexpenses where MasterID = " + "'" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            strSql = "Delete from tbltrnac where VoucherID = " + "'" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            strSql = "Delete from tbltrnac where ReferenceVoucherID = " + "'" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            strSql = "Delete from voucherjv where ReferenceVoucherID = " + "'" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsNameUnique(string Name, string Id)
        {
            string strSql = GetDataForUnique(Name, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool CheckStock()
        {
            return true;

        }


        #region Query Building Functions

        private string GetDataForUnique(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select CompId from MasterCompany where CompName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND CompId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetInsertQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankAccountID, string chequeNumber, string chequeDate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankexpenses";
            objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ChequeDepositedBankID", bankAccountID);
            objQuery.AddToQuery("ChequeNumber", chequeNumber);
            objQuery.AddToQuery("ChequeDate", chequeDate);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }

        private string GetInsertQueryformasterJV(string detailId,string AccountID, string VouType, int VouNo, string VouSeries, string VouDate, double mdebit, double mcredit, string Refno,string Narration, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherjv";
            objQuery.AddToQuery("ID", detailId);
            objQuery.AddToQuery("AccountId",AccountID );
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("Debit", mdebit);
            objQuery.AddToQuery("Credit",mcredit);
            objQuery.AddToQuery("ReferenceVoucherID", Refno);
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);

            return objQuery.InsertQuery();
        }


        private string GetInsertQueryTrnacForVoucher(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string DetailId,string bankAccountID,string chequeNumber, string chequeDate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("tblTrnacID", DetailId);
            objQuery.AddToQuery("VoucherID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Debit", Amt);
            objQuery.AddToQuery("Credit", 0);
            objQuery.AddToQuery("AccAccountID", bankAccountID);
            objQuery.AddToQuery("TransactionDate", VouDate);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("ReferenceVoucherId", "");
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("ChequeNumber", chequeNumber);
            objQuery.AddToQuery("ChequeDate", chequeDate);
            //objQuery.AddToQuery("VoucherDate", VouDate);
            //objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            //objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }
        private string GetInsertQueryTrnacForVoucherReverse(string Id, string CreditorId, string Narration, string VouType, int VouNo,
          string VouDate, double Amt, string DetailId, string bankAccountID, string chequeNumber, string chequeDate, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("tblTrnacID", DetailId);
            objQuery.AddToQuery("VoucherID", Id);
            objQuery.AddToQuery("AccountId", bankAccountID);
            objQuery.AddToQuery("Debit", 0);
            objQuery.AddToQuery("Credit", Amt);
            objQuery.AddToQuery("AccAccountID", CreditorId);
            objQuery.AddToQuery("TransactionDate", VouDate);
            objQuery.AddToQuery("ReferenceVoucherId", "");
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("ChequeNumber", chequeNumber);
            objQuery.AddToQuery("ChequeDate", chequeDate);
            //objQuery.AddToQuery("VoucherDate", VouDate);
            //objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            //objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }


        private string GetInsertQueryP(string Id, string acId, double mdebit, double mcredit, string detailId)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcashbankexpenses";
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("DetailCashBankExpensesID", detailId);
            objQuery.AddToQuery("AccountID", acId);
            objQuery.AddToQuery("Debit", mdebit);
            objQuery.AddToQuery("Credit", mcredit);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryForJVIntblTrnac(string Id, string acId, double mdebit, double mcredit, string detailId, string accountID, string VouDate, string VouType, string Refno, string Narration, int VouNo, string chequeNumber, string chequeDate, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("tblTrnacID", detailId);
            objQuery.AddToQuery("VoucherID", Id);
            objQuery.AddToQuery("AccountId", acId);
            objQuery.AddToQuery("Debit", mdebit);
            objQuery.AddToQuery("Credit", mcredit);
            objQuery.AddToQuery("AccAccountID", accountID);
            objQuery.AddToQuery("TransactionDate", VouDate);
            objQuery.AddToQuery("ReferenceVoucherId", Refno);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("ChequeNumber", chequeNumber);
            objQuery.AddToQuery("ChequeDate", chequeDate);
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryForJVIntblTrnacReverse(string Id, string acId, double mdebit, double mcredit, string detailId, string accountID, string VouDate, string VouType, string Refno, string Narration, int VouNo, string chequeNumber, string chequeDate, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("tblTrnacID", detailId);
            objQuery.AddToQuery("VoucherID", Id);
            objQuery.AddToQuery("AccountId", accountID);
            objQuery.AddToQuery("Debit", mcredit);
            objQuery.AddToQuery("Credit", mdebit);
            objQuery.AddToQuery("AccAccountID", acId);
            objQuery.AddToQuery("TransactionDate", VouDate);
            objQuery.AddToQuery("ReferenceVoucherId", Refno);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("ChequeNumber", chequeNumber);
            objQuery.AddToQuery("ChequeDate", chequeDate);
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            return objQuery.InsertQuery();
        }



        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string bankAccountID, string chequeNumber, string chequeDate, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankexpenses";
            objQuery.AddToQuery("CBID", Id, true);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("ChequeDepositedBankID", bankAccountID);
            objQuery.AddToQuery("ChequeNumber", chequeNumber);
            objQuery.AddToQuery("ChequeDate", chequeDate);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankexpenses";
            objQuery.AddToQuery("CBID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
        #endregion

        public DataRow GetLastRecord(string vouType, string vouSeries)
        {
            DataRow dRow = null;

            string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID  where voucherType = '" + vouType + "' && voucherSeries = '" + vouSeries + "' order by vouchernumber desc";

            //string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' && voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber desc";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        public DataRow GetLastVoucherNumber(string vouType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select VoucherNumber from vouchercashbankreceipt  where voucherType = '" + vouType + "' && voucherSeries = '" + vouSeries + "' order by vouchernumber desc";

                // string strSql = "Select Vouchernumber from vouchercreditdebitnote where  VoucherType =  '" + vouType + "'  &&  VoucherSeries = '" + vouSeries + "' order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetFirstRecord(string vouType, string vouSeries)
        {
            DataRow dRow = null;
            string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,a.OnaccountAmount,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankreceipt a inner join masteraccount b on a.AccountID = b.AccountID where VoucherType= " + vouType + " && VoucherSeries = '" + vouSeries + "' ";
            // string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' && voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber ";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }
    }
}
