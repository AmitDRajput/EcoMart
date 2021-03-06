using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
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
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from MasterArea where AreaId='{0}' for update ";
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

        public DataTable GetJVList()
        {
            DataTable dtable = new DataTable();
            string strSql = "SELECT AreaId, AreaName FROM MasterArea";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
       

        #region Query Building Functions
        private string GetInsertQuery(string Id, string Name,string createdby,string createddate,string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterArea";
            objQuery.AddToQuery("AreaId", Id);
            objQuery.AddToQuery("AreaName", Name);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Name,string modifiedby,string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterArea";
            objQuery.AddToQuery("AreaId", Id, true);
            objQuery.AddToQuery("AreaName", Name);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterArea";
            objQuery.AddToQuery("AreaId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion Query Building Functions
    }
}
