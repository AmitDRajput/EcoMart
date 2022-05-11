using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBToolRewrite
    {
        public DBToolRewrite()
        {
        }

        public DataTable ReadFixAccounts()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select * from tblFixAccounts";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }


        public bool CheckmasterAccountForID(string maccid)
        {
            bool bRetValue = false;
            DataRow dr = null;
            string strSql = "Select * from masterAccount where accountID = '"+ maccid +"'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
            {
                bRetValue = true;
            }
            return bRetValue;
        }



        public string GetInsertQueryFixAccounts(string maccid, string macccode, string maccname, string mgroupid)
        {
            Query objQuery = new Query();
            objQuery.Table = "masteraccount";
            objQuery.AddToQuery("AccountID", maccid);
            objQuery.AddToQuery("AccCode", macccode);
            objQuery.AddToQuery("AccName", maccname);
            objQuery.AddToQuery("AccGroupID", mgroupid);           
            return objQuery.InsertQuery();
        }


        public bool UpdatemasterAccount(string maccid, string macccode, string maccname, string mgroupid)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryFixAccounts(maccid, macccode, maccname, mgroupid);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            else
                bRetValue = false;
            return bRetValue;
        }

        public DataTable ReadDataFromtblTrnacForBlankTransactionDate()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select voucherID from tbltrnac where TransactionDate is null || TransactionDate = ''";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDateFromvouchersale(string voucherID)
        {
            DataRow dr = null;
            string strSql = "Select voucherDate from voucherSale where ID = '"+ voucherID +"'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }

        public bool UpdateDateIntbltrnac(string mvoucherID, string mdate)
        {
            bool bRetValue = false;
            string strSql = "update tbltrnac set TransactionDate = '"+ mdate +"' where VoucherID = '"+ mvoucherID +"'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            else
                bRetValue = false;
            return bRetValue;
        }

        public bool DeletePurchaseRecordsFromtblTrnac()
        {
            bool bRetValue = false;
            string strSql = "delete from tbltrnac  where vouchertype = '"+ FixAccounts.VoucherTypeForCashPurchase +"' || voucherType = '"+ FixAccounts.VoucherTypeForCreditPurchase + "'  || voucherType = '"+ FixAccounts.VoucherTypeForCreditStatementPurchase +"'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            else
                bRetValue = false;
            return bRetValue;
        }

        public DataTable ReadPurchaseVouchers()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select purchaseID  from voucherPurchase";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}
