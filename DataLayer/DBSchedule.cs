
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    class DBSchedule
    {
        public DBSchedule()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID,ScheduleCode from tblschedule";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select ScheduleID,ScheduleCode,ScheduleName from tblschedule where Id='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string Id, string Code, string Description)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, Code, Description);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string Code, string Description)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Code, Description);

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
      
        public bool IsNameUnique(string Code, string Id)
        {
            string strSql = GetDataForUnique(Code, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        #region Query Building Functions
      
        private string GetDataForUnique(string Code, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ShelfId from MasterShelf where ShelfCode='{0}'", Code);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ShelfId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }

        private string GetInsertQuery(string Id, string Code, string Description)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterShelf";
            objQuery.AddToQuery("ShelfId", Id);
            objQuery.AddToQuery("ShelfCode", Code);
            objQuery.AddToQuery("ShelfDescription", Description);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Code, string Description)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterShelf";
            objQuery.AddToQuery("ShelfId", Id, true);
            objQuery.AddToQuery("ShelfCode", Code);
            objQuery.AddToQuery("ShelfDescription", Description);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterShelf";
            objQuery.AddToQuery("ShelfId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 
    }
}
