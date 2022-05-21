using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
   public  class DBEmailID
    {
       public DBEmailID()
        {
        }        
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select EmailID,EmailName,Description from masterEmail order by EmailName";
            dtable = DBInterface.SelectDataTable(strSql);           
            return dtable;
        }
        public DataRow ReadDetailsByID(string  Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from masterEmail where EmailID ='{0}'  "; //for update
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        
        public int AddDetails(int Id, string Name, string Details, string createdby, string createddate, string createdtime)
        {
            int iRetValue = 0;
            string strSql = GetInsertQuery(Id, Name, Details, createdby, createddate, createdtime);
            iRetValue = DBInterface.ExecuteScalar(strSql);          
            return iRetValue;
        }


        public bool UpdateDetails(string Id, string EmailID, string Details)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, EmailID, Details);

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

        public DataTable GetEmailIDList()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select EmailID,EmailName,Description from masterEmail order by EmailID";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
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
            string strSql = "Select EmailId from masterEmail where EmailName= '" + Name + "'";
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
       
        #region Query Building Functions

       
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select EmailID from masterEmail where EmailName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND EmailID != ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        
        private string GetInsertQuery(int EmailID, string EmailName,string Details, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterEmail";
            //objQuery.AddToQuery("AreaId", Id);
            //objQuery.AddToQuery("EmailID", EmailID);
            objQuery.AddToQuery("EmailName",EmailName);
            objQuery.AddToQuery("Description", Details); 
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string EmailID, string Details)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterEmail";
            objQuery.AddToQuery("EmailID", Id, true);
            objQuery.AddToQuery("EmailName", EmailID);
            objQuery.AddToQuery("Description", Details);            
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "masterEmail";
            objQuery.AddToQuery("EmailID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion private methods
    }
}
