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

        //public int AddDetails(string Id, string EmailID, string Details)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetInsertQuery(Id,EmailID, Details);

        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}
        public int AddDetails(int Id, string Name, string Details, string createdby, string createddate, string createdtime)
        {
            int iRetValue = 0;
            string strSql = GetInsertQuery(Id, Name, Details, createdby, createddate, createdtime);
            iRetValue = DBInterface.ExecuteScalar(strSql);
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    iRetValue = true;
            //}
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
            string strSql = "Select EmailID from masterEmail where EmailName='" + Name + "'";
            bool bRetValue = true;
            DataRow dr = DBInterface.SelectFirstRow(strSql);
            if (dr == null)
            {
                bRetValue = false;
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
           
             string strSql = "Select EmailID from masterEmail where EmailID='" + Name +"'";
            //if (Id != "")
            //{
            //    sQuery.AppendFormat(" AND ID in ('{0}')", Id);
            //}
             return strSql;
        }
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

        //private string GetInsertQuery(string Id, string EmailID, string Details)
        //{
        //    Query objQuery = new Query();
        //    objQuery.Table = "tblEmailID";
        //    objQuery.AddToQuery("ID", Id, true);
        //    objQuery.AddToQuery("EmailID", EmailID);
        //    objQuery.AddToQuery("Details", Details);      
        //    return objQuery.InsertQuery();
        //}
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
