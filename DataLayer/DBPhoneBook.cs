using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    class DBPhoneBook
    {
        public DBPhoneBook()
        { 

        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select * from tblPhoneBook order by Name";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from tblPhoneBook where Id='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string Id, string Name, string Address1,string Address2, string Telephone, string MobileNumberForSMS, string MailId, string bDay, string bMonth, string bYear,string remark, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, Name, Address1, Address2, Telephone, MobileNumberForSMS, MailId,bDay,bMonth,bYear,remark, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string Name, string Address1,string Address2, string Telephone, string MobileNumberForSMS, string MailId, string bDay, string bMonth, string bYear,string remark, string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Name, Address1, Address2, Telephone, MobileNumberForSMS, MailId, bDay, bMonth, bYear, remark, modifiedby, modifydate, modifytime);

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
            sQuery.AppendFormat("Select Id from tblPhoneBook where Name='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND Id in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select Id from tblPhoneBook where Name='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND Id != ('{0}')", Id);
            }
            return sQuery.ToString();
        }


        private string GetInsertQuery(string Id, string Name, string Address1, string Address2, string Telephone, string MobileNumberForSMS, string MailId, string bDay, string bMonth, string bYear, string remark, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblPhoneBook";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("Name", Name);
            objQuery.AddToQuery("Address1", Address1);
            objQuery.AddToQuery("Address2", Address2);
            objQuery.AddToQuery("Telephone", Telephone);
            objQuery.AddToQuery("MobileNumberForSMS", MobileNumberForSMS);
            objQuery.AddToQuery("EmailID", MailId);
            objQuery.AddToQuery("BirthDay", bDay);
            objQuery.AddToQuery("BirthMonth", bMonth);
            objQuery.AddToQuery("BirthYear", bYear);
            objQuery.AddToQuery("Remark", remark);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Name, string Address1,string Address2, string Telephone, string MobileNumberForSMS, string MailId, string bDay, string bMonth, string bYear,string remark, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblPhoneBook";
            objQuery.AddToQuery("ID", Id, true);
            objQuery.AddToQuery("Name", Name);
            objQuery.AddToQuery("Address1", Address1);
            objQuery.AddToQuery("Address2", Address2);
            objQuery.AddToQuery("Telephone", Telephone);
            objQuery.AddToQuery("MobileNumberForSMS", MobileNumberForSMS);
            objQuery.AddToQuery("EmailID", MailId);
            objQuery.AddToQuery("BirthDay", bDay);
            objQuery.AddToQuery("BirthMonth", bMonth);
            objQuery.AddToQuery("BirthYear", bYear);
            objQuery.AddToQuery("Remark", remark);      
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tblPhoneBook";
            objQuery.AddToQuery("DocId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion

        public DataTable GetDoctorsList()
        {
            DataTable dtable = new DataTable();
            string strSql = "SELECT DocId, DocName, DocAddress, CONCAT(`DocName`, ', ', `DocAddress`) AS NameAddress FROM tblPhoneBook";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetSSDoctorsList()
        {
            DataTable dtable = new DataTable();
            string strSql = "SELECT DocId, DocName, DocAddress, DocShortNameAddress,MobileNumberForSMS FROM tblPhoneBook order by DocName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}
