using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBPartyCompany
    {
        public DBPartyCompany()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.AccName, a.AccountID from Masteraccount  a  INNER Join linkpartyCompany b where a.AccountID = b.AccountID order by a.AccName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataY()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.CompName, a.CompID from MasterCompany  a  INNER Join linkpartyCompany b where a.CompID = b.CompID order by a.CompName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewCompanyData(string AccCode)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CompName , b.CompID, b.AccountID,b.CreatedDate,b.CreatedTime,b.CreatedUserID from  mastercompany a ,linkpartycompany b   where   b.CompID = a.CompID  and b.AccountID = " + '"' + AccCode + '"' + " order by a.CompName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewCompanyDataY(string compcode)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.AccName , b.AccountID, b.CompID,b.CreatedDate,b.CreatedTime,b.CreatedUserID from  masteraccount a ,linkpartycompany b   where   b.AccountID = a.AccountID  and b.CompID = " + '"' + compcode + '"' + " order by a.AccName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }    

        public DataTable GetParty()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select AccName, AccountID  from masteraccount where acc_code = 'C' order by AccName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetCompany()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select CompName, CompId  from mastercompany order by CompName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByIDParty(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select AccName, AccountID  from masteraccount where AccountID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataTable IsPartyAlreadyLinked(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strSql = "Select * from linkpartycompany where AccountId='{0}' ";
                strSql = string.Format(strSql, Id);
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }
        public DataRow ReadDetailsByIDCompany(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select CompName, CompId from mastercompany where CompID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string Id, string detailID, string CompanyId, string createdby, string createddate, string createdtime, string modifyby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, detailID, CompanyId,createdby,createddate,createdtime,modifyby,modifydate,modifytime);

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

        public bool IsNameUnique(string CompanyId, string Id)
        {
            string strSql = GetDataForUnique(CompanyId, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }


        #region Query Building Functions
      
        private string GetDataForUnique(string CompanyId, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select CompID from linkpartycompany where AccountID= " + '"' + Id + '"' + "and CompID = " + '"' + CompanyId + '"', Id);
            return sQuery.ToString();
        }
        private string GetInsertQuery(string Id, string detailID, string CompanyId, string createdby, string createddate, string createdtime, string modifyby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "linkpartycompany";
            objQuery.AddToQuery("LinkPartyCompanyID", detailID);
            objQuery.AddToQuery("AccountID", Id);
            objQuery.AddToQuery("CompID", CompanyId);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            objQuery.AddToQuery("ModifiedUserID", modifyby);
            return objQuery.InsertQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";
            strSql = "delete from linkpartycompany where accountId = " + "'" + Id + "'";
            return strSql;
        }
        #endregion

    }

}
