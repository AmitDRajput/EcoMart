using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    public class DBEcoMartLic
    {
        public DataRow Read()
        {
            DataRow dRow = null;
            string strSql = "Select * from tblpharmasysdistpluslic ";            
            dRow = DBInterface.SelectFirstRow(strSql);
            return dRow;
        }

        public bool IsLicenseFound()
        {
            DataRow dRow = null;
            string strSql = "Select * from tblpharmasysdistpluslic ";
            dRow = DBInterface.SelectFirstRow(strSql);
            if (dRow == null)
                return false;
            else
                return true;
        }

        public bool DeleteLicense()
        {
            bool bRetValue = false;
            string strSql = "Delete from tblpharmasysdistpluslic";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetails(string Id, string data)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, data);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string data)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, data);

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


        private string GetInsertQuery(string Id, string data)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblpharmasysdistpluslic";
            objQuery.AddToQuery("EcoMartID", Id);
            objQuery.AddToQuery("Data", data);           
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string data)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblpharmasysdistpluslic";
            objQuery.AddToQuery("EcoMartID", Id, true);
            objQuery.AddToQuery("Data", data);            
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tblpharmasysdistpluslic";
            objQuery.AddToQuery("EcoMartID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
    }
}
