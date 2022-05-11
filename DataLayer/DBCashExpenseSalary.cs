using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBCashExpenseSalary
    {
        public DBCashExpenseSalary()
        {
        }
        public DataTable GetOverviewData(string CEType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select CBID,VoucherType,VoucherNumber,VoucherDate,AmountNet,Narration from vouchercashbankexpenses  where "+
                            " VoucherType = " + "'" + CEType + "'" + "  order by vouchernumber ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
   
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from vouchercashbankexpenses where CBID ='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataTable GetExpensesDetailsByID(string Id)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID,a.AccountID,a.debit,a.credit,b.AccName,b.AccAddress1,b.AccAddress2 from detailcashbankexpenses a inner join masteraccount b on a.AccountID = b.AccountID where  a.MasterID =  " + "'" + Id + "'";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public bool AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, Amt,createdby,createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsP(string Id, string acId, double mdebit, double mcredit)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryP(Id, acId, mdebit, mcredit);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string Narration, string VouType, int VouNo,
           string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Narration, VouType, VouNo, VouDate, Amt,modifiedby,modifieddate,modifiedtime);

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
            objQuery.AddToQuery("CBID", Id);
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


        private string GetInsertQueryP(string Id, string acId, double mdebit, double mcredit)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcashbankexpenses";
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("AccountID", acId);
            objQuery.AddToQuery("Debit", mdebit);
            objQuery.AddToQuery("Credit", mcredit);
            return objQuery.InsertQuery();
        }


        private string GetUpdateQuery(string Id, string Narration, string VouType, int VouNo,
             string VouDate, double Amt, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankexpenses";
            objQuery.AddToQuery("CBID", Id,true);       
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
            objQuery.AddToQuery("CRDBID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
        #endregion 

    }
}
