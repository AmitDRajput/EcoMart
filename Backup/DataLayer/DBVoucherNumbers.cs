using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class DBVoucherNumbers 
    {
        public DataRow GetVouchernumbers()
        {
            string strSql = "Select PurchaseCredit,PurchaseCashCredit,PuchaseCash,SaleChitNumber, SaleCash, SaleCredit, SaleCashCredit,CreatedDate,CreatedUserId,ModifyDate,ModifyUserId from tblvouchernumbers";
            return DBInterface.SelectFirstRow(strSql);
        }

        public bool AddDetails(long PurchaseCredit, long PurchaseCashCredit, long PuchaseCash, long SaleChitNumber, long SaleCash, long SaleCredit, long SaleCashCredit, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(PurchaseCredit, PurchaseCashCredit, PuchaseCash, SaleChitNumber, SaleCash, SaleCredit, SaleCashCredit, CreatedDate, CreatedUserId, ModifyDate, ModifyUserId);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool UpdateDetails(long PurchaseCredit, long PurchaseCashCredit, long PuchaseCash, long SaleChitNumber, long SaleCash, long SaleCredit, long SaleCashCredit, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(PurchaseCredit, PurchaseCashCredit, PuchaseCash, SaleChitNumber, SaleCash, SaleCredit, SaleCashCredit, CreatedDate, CreatedUserId, ModifyDate, ModifyUserId);
            strSql += " 1=1";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        #region Query Building Functions
        private string GetDataForUnique(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("");
            return sQuery.ToString();
        }

        private string GetInsertQuery(long PurchaseCredit, long PurchaseCashCredit, long PuchaseCash, long SaleChitNumber, long SaleCash, long SaleCredit, long SaleCashCredit, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblvouchernumbers ";
            objQuery.AddToQuery("PurchaseCredit", PurchaseCredit);
            objQuery.AddToQuery("PurchaseCashCredit", PurchaseCashCredit);
            objQuery.AddToQuery("PuchaseCash", PuchaseCash);
            objQuery.AddToQuery("SaleChitNumber", SaleChitNumber);
            objQuery.AddToQuery("SaleCash", SaleCash);
            objQuery.AddToQuery("SaleCredit", SaleCredit);
            objQuery.AddToQuery("SaleCashCredit", SaleCashCredit);          
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(long PurchaseCredit, long PurchaseCashCredit, long PuchaseCash, long SaleChitNumber, long SaleCash, long SaleCredit, long SaleCashCredit, string CreatedDate, string CreatedUserId, string ModifyDate, string ModifyUserId)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblvouchernumbers ";
            objQuery.AddToQuery("PurchaseCredit", PurchaseCredit);
            objQuery.AddToQuery("PurchaseCashCredit", PurchaseCashCredit);
            objQuery.AddToQuery("PuchaseCash", PuchaseCash);
            objQuery.AddToQuery("SaleChitNumber", SaleChitNumber);
            objQuery.AddToQuery("SaleCash", SaleCash);
            objQuery.AddToQuery("SaleCredit", SaleCredit);
            objQuery.AddToQuery("SaleCashCredit", SaleCashCredit);           
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            Query objQuery = new Query();
            objQuery.Table = "Vouchernumbers ";
            objQuery.AddToQuery("PurchaseCredit", Id, true);
            return objQuery.DeleteQuery();
        }
       
        #endregion



       
    }
}
