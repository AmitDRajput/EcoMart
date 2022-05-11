using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;


namespace EcoMart.DataLayer
{
    public class DBCashExpenses
    {
        public DBCashExpenses()
        {
        }

        public DataTable GetOverviewData(string CEType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID,a.Narration, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankexpenses a, masteraccount b " +
                            "where a.AccountId = b.AccountId && a.VoucherType = " + "'" + CEType + "' && a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' && a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.voucherdate desc ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetOverviewDataForReport(string CEType ,string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
                            "a.AccountID,a.Narration, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankexpenses a, masteraccount b " +
                            "where a.AccountId = b.AccountId && a.VoucherType = " + "'" + CEType + "' && a.VoucherDate >= '"+ fromDate + "' && a.VoucherDate <= '"+ toDate+"'  order by a.vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankexpenses a inner join masteraccount b on a.AccountID = b.AccountID where CBID = '" + Id + "' && VoucherType = '" + FixAccounts.VoucherTypeForCashExpenses + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow ReadDetailsByVouNumber(int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID,a.AmountNet,a.Narration,b.AccName,b.AccAddress1,b.AccAddress2 from vouchercashbankexpenses a inner join masteraccount b on a.AccountID = b.AccountID where VoucherNumber= " + vouno + " && VoucherType = '" + FixAccounts.VoucherTypeForCashExpenses + "'";
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

        public int AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt,  string createdby, string createddate, string createdtime)
        {
            
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt,  createdby, createddate, createdtime);

            bool ii = (DBInterface.ExecuteQuery(strSql) > 0);
            strSql = "select last_insert_ID()";
            int iid = DBInterface.ExecuteScalar(strSql);

            return iid;
        }
        public int AddDetailsInmaterJV(string detailId,string AccountID, string VouType, int VouNo, string VouSeries, string VouDate, double mdebit, double mcredit, string Refno,string Narration, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            // bool bRetValue = false;
            int ii = 0;
            string strSql = GetInsertQueryformasterJV(detailId,AccountID, VouType, VouNo, VouSeries, VouDate, mdebit, mcredit, Refno,Narration, CreatedBy, CreatedDate, CreatedTime);
            strSql += ";select last_insert_ID()";
            ii = Convert.ToInt32(DBInterface.ExecuteScalar(strSql));
            return ii;
           
        }

        public bool AddVoucherIntblTrnac(int Id, int CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string detailID, string createdby, string createddate, string createdtime)
        {
            bool retValue = false;
            string strSql = GetInsertQueryTrnacForVoucher(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, detailID, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retValue = true;
            }
            return retValue;
        }

        public bool AddJVDetailsIntblTrnac(string ID, string Vseries, string VID, string Accid, double debit, double credit, string AccAccountID, string Transdate, string RefVouID, string Vtype,
            string VsubType, int Vno, string Vdate, string Narration, string ShortName, string CheckNo, string ChkDate, string ClrDate, string BankID, string BankName, string BranchID, string BranchName, string createdDt,string createdtime,string createdUserId,string modifieddt, string modtime,string moduserid)
        {
            bool retValue = false;
            string strSql = GetInsertQueryTrnacForJournalVoucher(ID, Vseries, VID, Accid, debit, credit, AccAccountID, Transdate, RefVouID, Vtype,
                VsubType, Vno, Vdate, Narration, ShortName, CheckNo, ChkDate, ClrDate, BankID, BankName, BranchID, BranchName, createdDt, createdtime,
                createdUserId, modifieddt, modtime, moduserid);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retValue = true;
            }
            return retValue;
        }

        public bool UpdateJVDetailsIntblTrnac(string ID, string Vseries, string VID, string Accid, double debit, double credit, string AccAccountID, string Transdate, string RefVouID, string Vtype,
            string VsubType, int Vno, string Vdate, string Narration, string ShortName, string CheckNo, string ChkDate, string ClrDate, string BankID, string BankName, string BranchID, string BranchName, string createdDt, string createdtime, string createdUserId, string modifieddt, string modtime, string moduserid)
        {
            bool retValue = false;
            string strSql = GetUpdateQueryTrnacForJournalVoucher(ID, Vseries, VID, Accid, debit, credit, AccAccountID, Transdate, RefVouID, Vtype,
                VsubType, Vno, Vdate, Narration, ShortName, CheckNo, ChkDate, ClrDate, BankID, BankName, BranchID, BranchName, createdDt, createdtime,
                createdUserId, modifieddt, modtime, moduserid);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retValue = true;
            }
            return retValue;
        }

        public bool AddVoucherIntblTrnacReverse(int Id, int CreditorId, string Narration, string VouType, int VouNo,
        string VouDate, double Amt, string detailID, string createdby, string createddate, string createdtime)
        {
            bool retValue = false;
            string strSql = GetInsertQueryTrnacForVoucherReverse(Id,CreditorId, Narration, VouType, VouNo,
                VouDate, Amt, detailID, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retValue = true;
            }
            return retValue;
        }
        public bool AddDetailsP(int Id, int acId, double mdebit, double mcredit, string detailid)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryP(Id, acId, mdebit, mcredit, detailid);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddJVIntblTrnac(string Id, string acId, double mdebit, double mcredit, string detailid, string accountID, string VouDate, string VouType, string Refno, string Narration, int Vouno, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForJVIntblTrnac(Id, acId, mdebit, mcredit, detailid, accountID, VouDate, VouType, Refno, Narration, Vouno, CreatedBy, CreatedDate, CreatedTime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddJVIntblTrnacReverse(string Id, string acId, double mdebit, double mcredit, string detailid, string accountID, string VouDate, string VouType, string Refno, string Narration, int Vouno, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForJVIntblTrnacReverse(Id, acId, mdebit, mcredit, detailid, accountID, VouDate, VouType, Refno, Narration, Vouno, CreatedBy, CreatedDate, CreatedTime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo, VouDate, Amt, modifiedby, modifieddate, modifiedtime);

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
             string VouDate, double Amt, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankexpenses";
            //objQuery.AddToQuery("CBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", Amt); 
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }

        private string GetInsertQueryformasterJV(string detailId,string AccountID, string VouType, int VouNo, string VouSeries, string VouDate, double mdebit, double mcredit, string Refno,string Narration, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherjv";
            //objQuery.AddToQuery("ID", detailId);
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

      

        private string GetInsertQueryTrnacForVoucher(int Id, int CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string DetailId, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            //objQuery.AddToQuery("tblTrnacID", DetailId);
            objQuery.AddToQuery("VoucherID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Debit", Amt);
            objQuery.AddToQuery("Credit", 0);
            objQuery.AddToQuery("AccAccountID", FixAccounts.AccountCash);
            objQuery.AddToQuery("TransactionDate", VouDate);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("ReferenceVoucherId", 0);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            //objQuery.AddToQuery("VoucherDate", VouDate);
            //objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            //objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }

        private string GetInsertQueryTrnacForJournalVoucher(string ID, string Vseries, string VID, string Accid, double debit, double credit, string AccAccountID, string Transdate,
            string RefVouID, string Vtype, string VsubType, int Vno, string Vdate, string Narration, string ShortName, string CheckNo, string ChkDate, string ClrDate, string BankID,
            string BankName, string BranchID, string BranchName, string createdDt, string createdtime, string createdUserId, string modifieddt, string modtime, string moduserid)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("tblTrnacID", ID);
            objQuery.AddToQuery("Voucherseries", Vseries);
            objQuery.AddToQuery("VoucherID", VID);
            objQuery.AddToQuery("AccountId", Accid);
            objQuery.AddToQuery("Debit", debit);
            objQuery.AddToQuery("Credit", credit);
            objQuery.AddToQuery("AccAccountID", AccAccountID);
            objQuery.AddToQuery("TransactionDate", Transdate);
            objQuery.AddToQuery("ReferenceVoucherId", RefVouID);
            objQuery.AddToQuery("VoucherType", Vtype);
            objQuery.AddToQuery("VoucherSubType", VsubType);
            objQuery.AddToQuery("VoucherNumber", Vno);
            objQuery.AddToQuery("VoucherDate", Vdate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("ShortName", ShortName);
            objQuery.AddToQuery("ChequeNumber", CheckNo);
            objQuery.AddToQuery("ChequeDate", ChkDate);
            objQuery.AddToQuery("ClearedDate", ClrDate);
            objQuery.AddToQuery("BankID", BankID);
            objQuery.AddToQuery("BankName", BankName);
            objQuery.AddToQuery("BranchID", BranchID);
            objQuery.AddToQuery("BranchName", BranchName);
            objQuery.AddToQuery("CreatedDate", createdDt);
            objQuery.AddToQuery("CreatedTime", createdtime);
            objQuery.AddToQuery("CreatedUserID", createdUserId);
            objQuery.AddToQuery("ModifiedDate", modifieddt);
            objQuery.AddToQuery("ModifiedTime", modtime);
            objQuery.AddToQuery("ModifiedUserID", moduserid);

            return objQuery.InsertQuery();
        }

        private string GetUpdateQueryTrnacForJournalVoucher(string ID, string Vseries, string VID, string Accid, double debit, double credit, string AccAccountID, string Transdate,
           string RefVouID, string Vtype, string VsubType, int Vno, string Vdate, string Narration, string ShortName, string CheckNo, string ChkDate, string ClrDate, string BankID,
           string BankName, string BranchID, string BranchName, string createdDt, string createdtime, string createdUserId, string modifieddt, string modtime, string moduserid)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("tblTrnacID", ID);
            objQuery.AddToQuery("Voucherseries", Vseries);
            objQuery.AddToQuery("VoucherID", VID);
            objQuery.AddToQuery("AccountId", Accid);
            objQuery.AddToQuery("Debit", debit);
            objQuery.AddToQuery("Credit", credit);
            objQuery.AddToQuery("AccAccountID", AccAccountID);
            objQuery.AddToQuery("TransactionDate", Transdate);
            objQuery.AddToQuery("ReferenceVoucherId", RefVouID);
            objQuery.AddToQuery("VoucherType", Vtype);
            objQuery.AddToQuery("VoucherSubType", VsubType);
            objQuery.AddToQuery("VoucherNumber", Vno);
            objQuery.AddToQuery("VoucherDate", Vdate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("ShortName", ShortName);
            objQuery.AddToQuery("ChequeNumber", CheckNo);
            objQuery.AddToQuery("ChequeDate", ChkDate);
            objQuery.AddToQuery("ClearedDate", ClrDate);
            objQuery.AddToQuery("BankID", BankID);
            objQuery.AddToQuery("BankName", BankName);
            objQuery.AddToQuery("BranchID", BranchID);
            objQuery.AddToQuery("BranchName", BranchName);
            objQuery.AddToQuery("CreatedDate", createdDt);
            objQuery.AddToQuery("CreatedTime", createdtime);
            objQuery.AddToQuery("CreatedUserID", createdUserId);
            objQuery.AddToQuery("ModifiedDate", modifieddt);
            objQuery.AddToQuery("ModifiedTime", modtime);
            objQuery.AddToQuery("ModifiedUserID", moduserid);

            return objQuery.UpdateQuery();
        }

        private string GetInsertQueryTrnacForVoucherReverse(int Id, int CreditorId, string Narration, string VouType, int VouNo,
          string VouDate, double Amt, string DetailId, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            //objQuery.AddToQuery("tblTrnacID", DetailId);
            objQuery.AddToQuery("VoucherID", Id);
            objQuery.AddToQuery("AccountId", FixAccounts.AccountCash);
            objQuery.AddToQuery("Debit", 0);
            objQuery.AddToQuery("Credit", Amt);
            objQuery.AddToQuery("AccAccountID", CreditorId);
            objQuery.AddToQuery("TransactionDate", VouDate);
            objQuery.AddToQuery("ReferenceVoucherId", 0);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            //objQuery.AddToQuery("VoucherDate", VouDate);
            //objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            //objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }


        private string GetInsertQueryP(int Id, int acId, double mdebit, double mcredit, string detailId)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcashbankexpenses";
            objQuery.AddToQuery("MasterID", Id);
            //objQuery.AddToQuery("DetailCashBankExpensesID", detailId);
            objQuery.AddToQuery("AccountID", acId);
            objQuery.AddToQuery("Debit", mdebit);
            objQuery.AddToQuery("Credit", mcredit);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryForJVIntblTrnac(string Id, string acId, double mdebit, double mcredit, string detailId, string accountID, string VouDate, string VouType, string Refno, string Narration, int VouNo, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            //objQuery.AddToQuery("tblTrnacID", detailId);
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
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryForJVIntblTrnacReverse(string Id, string acId, double mdebit, double mcredit, string detailId, string accountID, string VouDate, string VouType, string Refno, string Narration, int VouNo, string CreatedBy, string CreatedDate, string CreatedTime)
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
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            return objQuery.InsertQuery();
        }


        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
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


        public int AddToMasterJV(int Id, int CBJVIDpay, int CBVouNo, string CBAccountID, string CBNarration, string CBVouDate, double CBTotalDiscount, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            //bool bRetValue = false;
            string strSql = GetInsertQueryJV(Id, CBJVIDpay, CBVouNo, CBAccountID, CBNarration, CBVouDate, CBTotalDiscount, CreatedBy, CreatedDate, CreatedTime);


            bool ii = (DBInterface.ExecuteQuery(strSql) > 0);
            strSql = "select last_insert_ID()";
            int iid = DBInterface.ExecuteScalar(strSql);

            return iid;
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}
            //return bRetValue;
        }
        private string GetInsertQueryJV(int Id, int CBJVIDpay, int CBVouNo, string CBAccountID, string CBNarration, string CBVouDate, double CBTotalDiscount, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherjv";
            //objQuery.AddToQuery("ID", CBJVID);
            objQuery.AddToQuery("AccountId", CBAccountID);
            objQuery.AddToQuery("Narration", CBNarration);
            objQuery.AddToQuery("VoucherType", "JV ");
            objQuery.AddToQuery("VoucherNumber", CBVouNo);
            objQuery.AddToQuery("VoucherDate", CBVouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("Debit", CBTotalDiscount);
            objQuery.AddToQuery("Credit", 0);
            objQuery.AddToQuery("ReferenceVoucherID", Id);
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);

            return objQuery.InsertQuery();
        }



    }
}
