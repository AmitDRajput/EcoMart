using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBPack
    {
        public DBPack()
        {
        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select PackID,PackName from masterpack order by PackName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPackType()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID,PackTypeName from masterpacktype order by PacktypeName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from MasterPack where ID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string Id, string Name)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, Name);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string Name)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Name);

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

        public DataTable GetPackList()
        {
            DataTable dtable = new DataTable();
            string strSql = "SELECT ID, PackName FROM MasterPack";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        #region Query Building Functions
      
        private string GetDataForUnique(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ID from masterPack where PackName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ID not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }

        private string GetInsertQuery(string Id, string Name)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterPack";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("PackName", Name);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Name)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterPack";
            objQuery.AddToQuery("ID", Id, true);
            objQuery.AddToQuery("PackName", Name);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterPack";
            objQuery.AddToQuery("ID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion
       
    }
}