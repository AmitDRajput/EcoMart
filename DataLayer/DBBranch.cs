using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace EcoMart.DataLayer
{
    public class DBBranch
    {
        public DBBranch()
        { 

        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select BranchId,BranchName from MasterBranch order by BranchName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from MasterBranch where BranchId='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public int AddDetails(int  Id, string Name,string createdby,string createddate,string createdtime)
        {
             int iRetValue = 0;
            string strSql = GetInsertQuery(Id, Name,createdby,createddate,createdtime);
            iRetValue = DBInterface.ExecuteScalar(strSql);
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    iRetValue = true;
            //}
            return iRetValue;
        }

        public bool UpdateDetails(string Id, string Name,string modifyby,string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Name,modifyby,modifydate,modifytime);

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
            string strSql = "Select BranchId from masterbranch where BranchName= '" + Name + "'";
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
       
        public DataRow GetMaxID()
        {
            DataRow dRow = null;

            string strSql = "Select max(branchid) as maxbranchid from MasterBranch ";
            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }
        #region Query Building Functions

       
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select BranchId from masterbranch where BranchName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND BranchId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        } 

        private string  GetInsertQuery(int Id, string Name,string createdby,string createddate,string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterBranch";
            //objQuery.AddToQuery("BranchId", Id);
            objQuery.AddToQuery("BranchName", Name);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            objQuery.AddToQuery("CreatedUserID", createdby);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Name,string modifyby,string modifydate,string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterBranch";
            objQuery.AddToQuery("BranchId", Id, true);
            objQuery.AddToQuery("BranchName", Name);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            objQuery.AddToQuery("ModifiedUserID", modifyby);

            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterBranch";
            objQuery.AddToQuery("BranchId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 
    }
}
