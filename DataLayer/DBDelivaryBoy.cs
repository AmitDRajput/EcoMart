using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    class DBDelivaryBoy
    {

        public DBDelivaryBoy()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID, Name from masterdelivaryboy order by Name";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataRow ReadDetailsByID(int Id)
        {
            DataRow dRow = null;
            if (Id != 0)
            {
                string strSql = "Select * from masterdelivaryboy where ID= '{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string Name, string Address1, string Address2, string Telephone, string MobileNumberForSMS, string MailID, string createdby, string createddate, string createtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Name, Address1, Address2, Telephone, MobileNumberForSMS, MailID, createdby, createddate, createtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
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
            sQuery.AppendFormat("Select ID from masterdelivaryboy where SalesmanName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ID in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ID from masterdelivaryboy where SalesmanName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ID not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }

        private string GetInsertQuery(string Name, string Address1, string Address2, string Telephone, string MobileNumberForSMS, string MailID, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterdelivaryboy";
            objQuery.AddToQuery("Name", Name);
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
            objQuery.Table = "masterdelivaryboy";
            objQuery.AddToQuery("ID", Id, true);
            objQuery.AddToQuery("Name", Name);
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
            objQuery.Table = "masterdelivaryboy";
            objQuery.AddToQuery("ID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }

        #endregion

    }
}
