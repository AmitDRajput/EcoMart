using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBBank
    {
        public DBBank()
        {
        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select BankId, BankName from MasterBank order by BankName";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from MasterBank where BankId='{0}' for update ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string Id, string Name,string createdby,string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, Name,createdby,createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string Name,string modifyby,string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Name,modifyby,modifydate,modifytime);

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

        public bool IsNameUniqueForAdd(string Name, string Id)
        {
            string strSql = GetDataForUniqueForAdd(Name, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsNameUniqueForEdit(string Name, string Id)
        {
            string strSql = GetDataForUniqueForEdit(Name, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        #region Query Building Functions
      
        private string GetDataForUniqueForAdd(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select BankId from MasterBank where BankName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND BankId in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select BankId from MasterBank where BankName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND BankId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }   

        private string GetInsertQuery(string Id, string Name,string createdby, string createddate,string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterBank";
            objQuery.AddToQuery("BankId", Id);
            objQuery.AddToQuery("BankName", Name);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            objQuery.AddToQuery("CreatedUserID", createdby);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Name,string modifyby,string modifydate,string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterBank";
            objQuery.AddToQuery("BankId", Id,true );
            objQuery.AddToQuery("BankName", Name);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            objQuery.AddToQuery("ModifiedUserID", modifyby);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterScheme";
            objQuery.AddToQuery("ID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 

    }
}
