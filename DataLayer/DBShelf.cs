using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    public class DBShelf
    {
        public DBShelf()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ShelfId,ShelfCode, ShelfDescription from mastershelf order by ShelfCode";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
        public DataTable GetOverviewDataForMultiSelection()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ShelfId as ID,ShelfCode as Name from mastershelf order by ShelfCode";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }
       
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow drow = null;
            if (Id != "")
            {
                string strSql = "Select *  from  mastershelf  where ShelfId='{0}' ";
                strSql = string.Format(strSql, Id);
                drow = DBInterface.SelectFirstRow(strSql);
            }
            return drow;
        }
        public int AddDetails(int Id, string Code, string Description,string createdby, string createddate, string createdtime)
        {
            int iRetValue = 0;
            string strSql = GetInsertQuery(Id, Code, Description, createdby, createddate, createdtime);
            iRetValue = DBInterface.ExecuteScalar(strSql);
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}
            return iRetValue;
        }

        public bool UpdateDetails(string Id, string Code, string Description,string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Code, Description,modifiedby,modifieddate,modifiedtime);

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

        //public bool IsNameUniqueForAdd(string Name, string Id)
        //{
        //    string strSql = GetDataForUniqueForAdd(Name, Id);
        //    bool bRetValue = false;
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}

        //public bool IsNameUniqueForEdit(string Name, string Id)
        //{
        //    string strSql = GetDataForUniqueForEdit(Name, Id);
        //    bool bRetValue = false;
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}
        public bool IsNameUniqueForAdd(string Name, string Id)
        {
            int ifdup = GetDataForUniqueForAdd(Name, Id);
            bool bRetValue = false;
            if (ifdup > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsNameUniqueForEdit(string Name, string Id)
        {
            int ifdup = GetDataForUniqueForAdd(Name, Id);
            bool bRetValue = false;
            if (ifdup > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private int GetDataForUniqueForAdd(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            DataRow dRow = null;
            string strSql = "Select ShelfId from MasterShelf where ShelfCode = '" + Name + "'";
            dRow = DBInterface.SelectFirstRow(strSql);
            if (dRow == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }
        public bool ShelfTransfer(string fromShelf, string toShelf)
        {
            string strSql = "Update masterproduct set prodshelfID = '"+ toShelf +"' where prodshelfID = '"+ fromShelf +"'";
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public DataRow GetMaxID()
        {
            DataRow dRow = null;

            string strSql = "Select max(shelfid) as maxid from mastershelf ";
            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        #region Query Building Functions      

        //private string GetDataForUniqueForAdd(string Name, string Id)
        //{
        //    StringBuilder sQuery = new StringBuilder();
        //    sQuery.AppendFormat("Select ShelfId from MasterShelf where ShelfCode='{0}'", Name);
        //    if (Id != "")
        //    {
        //        sQuery.AppendFormat(" AND ShelfId in ('{0}')", Id);
        //    }
        //    return sQuery.ToString();
        //}
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ShelfId from MasterShelf where ShelfCode='{0}'", Name );
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ShelfId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }     

        private string GetInsertQuery(int Id, string Code, string Description, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterShelf";
            //objQuery.AddToQuery("ShelfId", Id);
            objQuery.AddToQuery("ShelfCode", Code);
            objQuery.AddToQuery("ShelfDescription", Description);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Code, string Description, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterShelf";
            objQuery.AddToQuery("ShelfId", Id, true);
            objQuery.AddToQuery("ShelfCode", Code);
            objQuery.AddToQuery("ShelfDescription", Description);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
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
