using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBDoctor
    {
        public DBDoctor()
        { 

        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select DocId,DocName,DocAddress,DocTelephone,DocEmailID,DocShortNameAddress,DocRegistrationNumber,DocDegree from MasterDoctor order by DocName";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {   
                string strSql = "Select * from MasterDoctor where DocId='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string Id, string Name, string Address,string Telephone,string MailId,string ShortNameAddress,string registrationNumber, string degree, string createdby, string createddate,string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, Name, Address,Telephone,MailId,ShortNameAddress,registrationNumber,degree, createdby, createddate,createdtime );

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string Name, string Address, string Telephone, string MailId, string ShortNameAddress, string registrationNumber, string degree, string modifiedby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Name, Address, Telephone, MailId, ShortNameAddress,registrationNumber,degree, modifiedby, modifydate,modifytime);

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
            sQuery.AppendFormat("Select DocId from MasterDoctor where DocName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND DocId in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select DocId from MasterDoctor where DocName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND DocId != ('{0}')", Id);
            }
            return sQuery.ToString();
        }


        private string GetInsertQuery(string Id, string Name, string Address, string Telephone, string MailId, string ShortNameAddress, string registrationNumber, string degree, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterDoctor";
            objQuery.AddToQuery("DocId", Id);
            objQuery.AddToQuery("DocName", Name);
            objQuery.AddToQuery("DocAddress", Address);
            objQuery.AddToQuery("DocTelephone", Telephone);
            objQuery.AddToQuery("DocEmailID", MailId);
            objQuery.AddToQuery("DocShortNameAddress", ShortNameAddress);
            objQuery.AddToQuery("DocRegistrationNumber", registrationNumber);
            objQuery.AddToQuery("DocDegree", degree);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Name, string Address, string Telephone, string MailId, string ShortNameAddress,string registrationNumber, string degree,string modifiedby, string modifydate,string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterDoctor";
            objQuery.AddToQuery("DocId", Id,true );
            objQuery.AddToQuery("DocName", Name);
            objQuery.AddToQuery("DocAddress", Address);
            objQuery.AddToQuery("DocTelephone", Telephone);
            objQuery.AddToQuery("DocEmailID", MailId);
            objQuery.AddToQuery("DocShortNameAddress", ShortNameAddress);
            objQuery.AddToQuery("DocRegistrationNumber", registrationNumber);
            objQuery.AddToQuery("DocDegree", degree);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterDoctor";
            objQuery.AddToQuery("DocId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 

        public DataTable GetDoctorsList()
        {
            DataTable dtable = new DataTable();
            string strSql = "SELECT DocId, DocName, DocAddress, CONCAT(`DocName`, ', ', `DocAddress`) AS NameAddress FROM masterdoctor";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetSSDoctorsList()
        {
            DataTable dtable = new DataTable();         
            string strSql = "SELECT DocId, DocName, DocAddress, DocShortNameAddress FROM masterdoctor order by DocName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }

}
