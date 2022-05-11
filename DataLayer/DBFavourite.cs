using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    public class DBFavourite
    {
        public DBFavourite()
        {
        }  
  
        public DataTable  GetFavouriteList()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select FavouriteId,FavName,FavControlName,FavOperationMode,FavIndex from tblfavourite order by FavIndex";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;  
        }

        public bool AddDetails(string Id, string Name, string ControlName, int OperationMode, int Index)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, Name, ControlName,OperationMode,Index);

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
     
        private string GetInsertQuery(string Id, string Name, string ControlName, int OperationMode, int Index)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblfavourite";
            objQuery.AddToQuery("FavouriteId", Id);
            objQuery.AddToQuery("FavName", Name);
            objQuery.AddToQuery("FavControlName", ControlName);
            objQuery.AddToQuery("FavOperationMode", OperationMode);
            objQuery.AddToQuery("FavIndex", Index);
            return objQuery.InsertQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tblfavourite";
            objQuery.AddToQuery("FavouriteId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
    }
}
