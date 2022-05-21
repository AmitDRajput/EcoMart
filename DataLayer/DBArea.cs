using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;


namespace EcoMart.DataLayer
{
    public class DBArea
    {        
        public DBArea()
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
                string strSql = "Select * from MasterArea where AreaId='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public int AddDetails(int Id, string Name,string createdby, string createddate, string createtime)
        {
            int iRetValue = 0;
            string strSql = GetInsertQuery(Id, Name,createdby,createddate,createtime);

            iRetValue = DBInterface.ExecuteScalar(strSql);
            
            return iRetValue;
        }

        public bool UpdateDetails(string Id, string Name,string modifiedby,string modifydate,string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Name,modifiedby,modifydate,modifytime);

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

        public DataTable GetAreaList()
        {
            DataTable dtable = new DataTable();
            string strSql = "SELECT AreaId, AreaName FROM MasterArea";
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

        public DataRow GetMaxID()
        {
            DataRow dRow = null;

            string strSql = "Select max(areaid) as maxid from MasterArea ";
            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        #region Query Building Functions

        private int GetDataForUniqueForAdd(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            DataRow dRow = null;           
            string strSql = "Select AreaId from masterarea where AreaName= '"+ Name + "'" ;
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
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select AreaId from masterarea where AreaName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND AreaId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
   
        private string GetInsertQuery(int Id, string Name,string createdby,string createddate,string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterArea";           
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
        #endregion private methods



        public DataTable GetOverViewDataForAddress()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select AreaId,AreaName from MasterArea union ( select '' as AreaId ,PatientAddress1 as AreaName from vouchersale where PatientAddress1 is not null && PatientAddress1 != '') union ( select '' as AreaId ,DoctorAddress as AreaName from vouchersale where DoctorAddress is not null && DoctorAddress != '') order by AreaName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}


