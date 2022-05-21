using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    public class DBSalesman
    {
        public DBSalesman()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select SalesmanID, SalesmanName from mastersalesman order by SalesmanName";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataRow ReadDetailsByID(int Id)
        {
            DataRow dRow = null;
            if (Id != 0)
            {
                string strSql = "Select * from mastersalesman where SalesmanID= '{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public int AddDetails(int Id, string Name, string Address1, string Address2, string Telephone, string MobileNumberForSMS, string MailID, string createdby, string createddate, string createtime)
        {
            int iRetValue = 0;
            string strSql = GetInsertQuery(Id, Name,Address1,Address2,Telephone,MobileNumberForSMS,MailID, createdby, createddate, createtime);
            iRetValue = DBInterface.ExecuteScalar(strSql);           
            return iRetValue;
        }

        public bool UpdateDetails(int Id, string Name, string Address1, string Address2, string Telephone, string MobileNumberForSMS, string MailID, string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Name, Address1, Address2, Telephone, MobileNumberForSMS, MailID, modifiedby, modifydate, modifytime);

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
            string strSql = "Select SalesmanID from mastersalesman where SalesmanName= '" + Name + "'";
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

        private string GetInsertQuery(int Id, string Name,string Address1, string Address2, string Telephone, string MobileNumberForSMS, string MailID, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "mastersalesman";           
            objQuery.AddToQuery("SalesmanName", Name);
            objQuery.AddToQuery("Address1", Address1);
            objQuery.AddToQuery("Address2", Address2);
            objQuery.AddToQuery("TelephoneNumber", Telephone);
            objQuery.AddToQuery("MobileNumberForSMS", MobileNumberForSMS);
            objQuery.AddToQuery("Email", MailID);

            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(int Id, string Name, string Address1, string Address2, string Telephone, string MobileNumberForSMS, string MailID, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "mastersalesman";
            objQuery.AddToQuery("SalesmanID", Id, true);
            objQuery.AddToQuery("SalesmanName", Name);
            objQuery.AddToQuery("Address1", Address1);
            objQuery.AddToQuery("Address2", Address2);
            objQuery.AddToQuery("TelephoneNumber", Telephone);
            objQuery.AddToQuery("MobileNumberForSMS", MobileNumberForSMS);
            objQuery.AddToQuery("Email", MailID);



            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "mastersalesman";
            objQuery.AddToQuery("SalesmanId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 
    }
}
