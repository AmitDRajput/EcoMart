using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;
using EcoMart.Common;
using EcoMart.BusinessLayer;

namespace EcoMart.DataLayer
{
    public class DBJournalVoucher
    {

        public DBJournalVoucher()
        {

        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select AreaId,AreaName from MasterArea order by AreaName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewJVData(string VouType, string fromDate, string toDate)
        {

            DataTable dtable = new DataTable();
            try
            {
                string strSql = "Select distinct a.ID,a.SerialNumber,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AccountID, " +
                    "a.Debit,a.Credit, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from voucherjv a, masteraccount b " +
                    "where a.AccountID = b.AccountID && a.VoucherType = '" + VouType + "' && a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "'  order by a.VoucherNumber, a.SerialNumber";

                dtable = DBInterface.SelectDataTable(strSql);
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }

            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from MasterArea where AreaId='{0}'  "; //for update
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByJVID(string ID)
        {
            DataRow dRow = null;
            if (ID != "")
            {
                string strSql = "Select * from voucherjv where ID = '{0}'";
                strSql = string.Format(strSql, ID);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataTable ReadDetailsByVoucherID(string VoucherID)
        {
            DataTable dt = null;
            if (VoucherID != "")
            {
                string strSql = "Select a.ID, a.AccountID, a.VoucherID, a.VoucherType, a.VoucherSeries,a.VoucherNumber,a.VoucherDate,a.SerialNumber,a.Debit,a.Credit,a.Narration,b.AccName,b.AccAddress1 from voucherjv a inner join masteraccount b on a.AccountID = b.AccountID where VoucherID = '{0}' order by a.SerialNumber";
                strSql = string.Format(strSql, VoucherID);
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public bool AddDetails(string ID, string AccountID, string VoucherID, string VoucherType, string VoucherSeries, int VoucherNumber, string VoucherDate, int serialno, double Debit, double Credit, double AmountClear,
            double AmountBalance, string Narration, string ReferenceVoucherID, string OperatorID, string CreatedUserID, string CreatedDate, string CreatedTime, string ModifiedUserID, string ModifiedDate,
            string ModifiedTime, string ModifiedOperatorID)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(ID, AccountID, VoucherID, VoucherType, VoucherSeries, VoucherNumber, VoucherDate, serialno, Debit, Credit, AmountClear, AmountBalance, Narration,
                ReferenceVoucherID, OperatorID, CreatedUserID, CreatedDate, CreatedTime, ModifiedUserID, ModifiedDate, ModifiedTime, ModifiedOperatorID);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        //public bool UpdateDetails(string ID, string AccountID, string VoucherID, string VoucherType, string VoucherSeries, int VoucherNumber, string VoucherDate, int serialno, double Debit, double Credit, double AmountClear,
        //   double AmountBalance, string Narration, string ReferenceVoucherID, string OperatorID, string CreatedUserID, string CreatedDate, string CreatedTime, string ModifiedUserID, string ModifiedDate,
        //   string ModifiedTime, string ModifiedOperatorID)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetUpdateQuery(ID, AccountID, VoucherID, VoucherType, VoucherSeries, VoucherNumber, VoucherDate, serialno, Debit, Credit, AmountClear, AmountBalance, Narration,
        //         ReferenceVoucherID, OperatorID, CreatedUserID, CreatedDate, CreatedTime, ModifiedUserID, ModifiedDate, ModifiedTime, ModifiedOperatorID);


        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}

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

        public bool DeleteJVDetails(string JVVouID)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQueryForJV(JVVouID); // GetDeleteQueryForJVtblTrans

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool DeleteJVFormtblTrans(string JVVouID)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQueryForJVtblTrans(JVVouID); // GetDeleteQueryForJVtblTrans

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public DataTable GetJVList()
        {
            DataTable dtable = new DataTable();
            string strSql = "SELECT AreaId, AreaName FROM MasterArea";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
       

        #region Query Building Functions
        private string GetInsertQuery(string ID, string AccountID, string VoucherID, string VoucherType, string VoucherSeries, int VoucherNumber, string VoucherDate,
            int serialno, double Debit, double Credit, double AmountClear, double AmountBalance, string Narration, string ReferenceVoucherID, string OperatorID,
            string CreatedUserID, string CreatedDate, string CreatedTime, string ModifiedUserID, string ModifiedDate, string ModifiedTime,
            string ModifiedOperatorID)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherjv";
            objQuery.AddToQuery("ID", ID);
            objQuery.AddToQuery("AccountID", AccountID);
            objQuery.AddToQuery("VoucherID", VoucherID);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherSeries", VoucherSeries);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherDate", VoucherDate);
            objQuery.AddToQuery("SerialNumber", serialno);
            objQuery.AddToQuery("Debit", Debit);
            objQuery.AddToQuery("Credit", Credit);
            objQuery.AddToQuery("AmountClear", AmountClear);
            objQuery.AddToQuery("AmountBalance", AmountBalance);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("ReferenceVoucherID", ReferenceVoucherID);
            objQuery.AddToQuery("OperatorID", OperatorID);
            objQuery.AddToQuery("CreatedUserID", CreatedUserID);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            objQuery.AddToQuery("ModifiedUserID", ModifiedUserID);
            objQuery.AddToQuery("ModifiedDate", ModifiedDate);
            objQuery.AddToQuery("ModifiedTime", ModifiedTime);
            objQuery.AddToQuery("ModifiedOperatorID", ModifiedOperatorID);
            return objQuery.InsertQuery();
        }

        //private string GetUpdateQuery(string ID, string AccountID, string VoucherID, string VoucherType, string VoucherSeries, int VoucherNumber, string VoucherDate,
        //    int serialno, double Debit, double Credit, double AmountClear, double AmountBalance, string Narration, string ReferenceVoucherID, string OperatorID,
        //    string CreatedUserID, string CreatedDate, string CreatedTime, string ModifiedUserID, string ModifiedDate, string ModifiedTime,
        //    string ModifiedOperatorID)
        //{
        //    Query objQuery = new Query();
        //    objQuery.Table = "voucherjv";
        //    objQuery.AddToQuery("ID", ID);
        //    objQuery.AddToQuery("AccountID", AccountID);
        //    objQuery.AddToQuery("VoucherID", VoucherID, true);
        //    objQuery.AddToQuery("VoucherType", VoucherType);
        //    objQuery.AddToQuery("VoucherSeries", VoucherSeries);
        //    objQuery.AddToQuery("VoucherNumber", VoucherNumber);
        //    objQuery.AddToQuery("VoucherDate", VoucherDate);
        //    objQuery.AddToQuery("SerialNumber", serialno);
        //    objQuery.AddToQuery("Debit", Debit);
        //    objQuery.AddToQuery("Credit", Credit);
        //    objQuery.AddToQuery("AmountClear", AmountClear);
        //    objQuery.AddToQuery("AmountBalance", AmountBalance);
        //    objQuery.AddToQuery("Narration", Narration);
        //    objQuery.AddToQuery("ReferenceVoucherID", ReferenceVoucherID);
        //    objQuery.AddToQuery("OperatorID", OperatorID);
        //    objQuery.AddToQuery("CreatedUserID", CreatedUserID);
        //    objQuery.AddToQuery("CreatedDate", CreatedDate);
        //    objQuery.AddToQuery("CreatedTime", CreatedTime);
        //    objQuery.AddToQuery("ModifiedUserID", ModifiedUserID);
        //    objQuery.AddToQuery("ModifiedDate", ModifiedDate);
        //    objQuery.AddToQuery("ModifiedTime", ModifiedTime);
        //    objQuery.AddToQuery("ModifiedOperatorID", ModifiedOperatorID);
        //    return objQuery.UpdateQuery();
        //}

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterArea";
            objQuery.AddToQuery("AreaId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }

        private string GetDeleteQueryForJV(string JVVouID)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "voucherjv";
            objQuery.AddToQuery("VoucherID", JVVouID, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }

        private string GetDeleteQueryForJVtblTrans(string JVVouID)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("VoucherID", JVVouID, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }

        public DataTable ReadJVDetailsByID(string VouID)
        {
            DataTable dtable = new DataTable();
            string strSql = "select distinct a.AccountID, a.AccName, a.AccAddress1, b.VoucherNumber, b.SerialNumber, b.Debit, b.Credit from masteraccount a inner join voucherjv b on a.AccountID = b.AccountID where b.VoucherID = '{0}' order by b.VoucherNumber, b.SerialNumber";
            strSql = string.Format(strSql, VouID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;

        }
        #endregion Query Building Functions
    }
}
