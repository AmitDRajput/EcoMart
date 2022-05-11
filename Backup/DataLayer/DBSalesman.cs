using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBSalesman
    {
        public DBSalesman()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select SalesmanId, SalesmanName from mastersalesman order by SalesmanName";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from mastersalesman where SalesmanId='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string Id, string Name, string createdby, string createddate, string createtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, Name, createdby, createddate, createtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string Name, string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Name, modifiedby, modifydate, modifytime);

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
            sQuery.AppendFormat("Select SalesmanId from mastersalesman where SalesmanName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND SalesmanId in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select SalesmanId from mastersalesman where SalesmanName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND SalesmanId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }

        private string GetInsertQuery(string Id, string Name, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "mastersalesman";
            objQuery.AddToQuery("SalesmanId", Id);
            objQuery.AddToQuery("SalesmanName",Name);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Name, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "mastersalesman";
            objQuery.AddToQuery("SalesmanId", Id, true);
            objQuery.AddToQuery("SalesmanName", Name);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "mastersalesman";
            objQuery.AddToQuery("SalesmanId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 
    }
}
