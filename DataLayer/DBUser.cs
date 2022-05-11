using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    class DBUser
    {
        public DBUser()
        {
           
        }

        public DataTable GetUserByUserNameAndPassword(string userName, string password)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select * from tbluser where UserName = '{0}' and Password='{1}'", userName, password);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;          
        }

        public bool AddDetails(string ID, string username, string mpassword,int IfInUse, string MakeItDefault, int mlevel, string details, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(ID, username, mpassword, IfInUse, MakeItDefault, mlevel, details, createdby, createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQuery(string ID, string username, string mpassword, int IfInUse, string MakeItDefault, int mlevel, string details, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbluser";
            objQuery.AddToQuery("UserID", ID);
            objQuery.AddToQuery("UserName", username);
            objQuery.AddToQuery("Password", mpassword);
            objQuery.AddToQuery("IfInUse", IfInUse);
            objQuery.AddToQuery("MakeItDefault", MakeItDefault);
            objQuery.AddToQuery("Level", mlevel);
            objQuery.AddToQuery("UserDetails", details);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            
            return objQuery.InsertQuery();
        }
        
        public DataTable GetOverviewData()
        {
           
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select UserID, UserName, Password, Level,IfInUse, MakeItDefault,UserDetails from tbluser where level > 0  order by UserName");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetLevelData()
        {
            DataTable dtable = new DataTable();
            string strSql = "select ID,Type from tbluserlevel  where ID > 0 order by ID";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public bool UpdateDetails(string id, string UserName, string Password, int Level, int ifinuse, string MakeItDefault, string Details, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(id,UserName, Password, Level,ifinuse, MakeItDefault, Details,modifiedby,modifieddate,modifiedtime);
            
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetUpdateQuery(string id, string UserName, string Password, int Level, int ifinuse, string MakeItDefault, string Details, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbluser";
            objQuery.AddToQuery("UserID", id, true);
            objQuery.AddToQuery("UserName", UserName);
            objQuery.AddToQuery("Password", Password);
            objQuery.AddToQuery("Level", Level);
            objQuery.AddToQuery("IfInUse", ifinuse);
            objQuery.AddToQuery("MakeItDefault", MakeItDefault);
            objQuery.AddToQuery("UserDetails", Details);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
           
            return objQuery.UpdateQuery();
        }
        
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.UserID,a.UserName,a.Password,a.Level,a.IfInUse,a.MakeItDefault,a.UserDetails,b.Type from tbluser a inner join tbluserlevel b on a.level = b.ID  where UserID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool DeleteDetails(string Id,int inuse)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(Id,inuse);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        
        private string GetDeleteQuery(string Id,int inuse)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tbluser";
            objQuery.AddToQuery("UserID", Id, true);        
            strSql = objQuery.DeleteQuery();

            return strSql;
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

        private string GetDataForUniqueForAdd(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select UserId from tbluser where UserName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND UserId  in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select UserId from tbluser where UserName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND UserId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }

        public void MakeAllMakeItDefaultNo()
        {
            string strSql = "update  tbluser set makeitDefault = 'N'";
            DBInterface.ExecuteQuery(strSql);
        }

        public DataRow GetDefaultUser()
        {
            DataRow dRow = null;
            string strSql = "Select UserID,UserName,Password,Level,IfInUse,MakeItDefault,UserDetails from tbluser  where makeitDefault = 'Y'";
            dRow = DBInterface.SelectFirstRow(strSql);           
            return dRow;
        }
    }
}
