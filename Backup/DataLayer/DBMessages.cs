using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;


namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBMessages
    {
         public DBMessages()
        {
        }
    
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID,CreatedDate,Message from tblMessage order by Createddate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from tblMessage where ID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string Id, string Name, string createdby, string createddate, string createtime)
        {
            bool bRetValue = false;
            string curdate = DateTime.Now.ToString();
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

        public DataTable GetMessageList()
        {
            DataTable dtable = new DataTable();
            string strSql = "SELECT ID, Message,Date FROM tblmessage";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
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
            sQuery.AppendFormat("Select ID from tblmessage where message='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ID in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ID from tblmessage where message='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ID not in ('{0}')", Id);
            }
            return sQuery.ToString();
        } 
       
        private string GetInsertQuery(string Id, string Name, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblmessage";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("Message", Name);      
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Name, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblmessage";
            objQuery.AddToQuery("ID", Id, true);
            objQuery.AddToQuery("Message", Name);        
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
           
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tblmessage";
            objQuery.AddToQuery("ID", Id, true);

            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 
    }
}
