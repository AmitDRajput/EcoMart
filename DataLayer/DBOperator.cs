using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    class DBOperator
    {
        public DBOperator()
        {
        }

        public DataTable GetOperatorByOperatorNameAndPassword(string OperatorName, string password)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select * from tblOperator where OperatorName = '{0}' and Password='{1}'", OperatorName, password);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow GetOperatorIDByOperatorPassword(string password)
        {
            DataRow dr;
            string strSql = string.Format("Select OperatorID from tblOperator where Password='{0}' && IFinUse = 0",password);
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }

        public bool AddDetails(string ID, string Operatorname, string mpassword, int IfInUse, string details, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(ID, Operatorname, mpassword, IfInUse, details, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQuery(string ID, string username, string mpassword, int IfInUse, string details, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblOperator";
            objQuery.AddToQuery("OperatorID", ID);
            objQuery.AddToQuery("OperatorName", username);
            objQuery.AddToQuery("Password", mpassword);
            objQuery.AddToQuery("IfInUse", IfInUse);
            objQuery.AddToQuery("OperatorDetails", details);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }

        public DataTable GetOverviewData()
        {

            DataTable dtable = new DataTable();
            string strSql = string.Format("Select OperatorID, OperatorName, Password, IfInUse,OperatorDetails from tblOperator  order by OperatorName");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public bool UpdateDetails(string id, string OperatorName, string Password, int ifinuse, string Details, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(id, OperatorName, Password, ifinuse, Details, modifiedby, modifieddate, modifiedtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetUpdateQuery(string id, string OperatorName, string Password,  int ifinuse, string Details, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblOperator";
            objQuery.AddToQuery("OperatorID", id, true);
            objQuery.AddToQuery("OperatorName", OperatorName);
            objQuery.AddToQuery("Password", Password);          
            objQuery.AddToQuery("IfInUse", ifinuse);
            objQuery.AddToQuery("OperatorDetails", Details);
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
                string strSql = "Select a.OperatorID,a.OperatorName,a.Password,a.IfInUse,a.OperatorDetails from tblOperator a  where OperatorID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool DeleteDetails(string Id, int inuse)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(Id, inuse);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetDeleteQuery(string Id, int inuse)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tblOperator";
            objQuery.AddToQuery("OperatorID", Id, true);
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
            sQuery.AppendFormat("Select OperatorId from tblOperator where OperatorName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND OperatorId  in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select OperatorId from tblOperator where OperatorName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND OperatorId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }

    }
}
