using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBCompany
    {
        public DBCompany()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select CompId, CompName, CompShortName, CompContactPerson,tag,PartyID_1,PartyID_2 from MasterCompany order by CompName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForMultiSelection()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select CompId as ID, CompName as Name from MasterCompany order by CompName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetCompany()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select CompId, CompName,CompShortName from MasterCompany order by CompName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public  DataTable  CreateTempComp()
        {
            DataTable dtable = new DataTable();
            string strSql = "Create Temporary table if not exists tempcomp select compID,compname,Tag from mastercompany";
            DBInterface.ExecuteQuery(strSql);
          
            strSql = "Select CompId, CompName,tag from tempcomp order by CompName";
            dtable = DBInterface.SelectDataTable(strSql);
            if (dtable.Rows.Count == 0)
            {
                strSql = "insert into tempcomp select compID,compName,Tag from mastercompany";
            }
            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from MasterCompany where CompId='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string Id, string ShortName, string Name, string Telephone, string MailId, 
            string ContactPerson, string Address,string partyID1,string partyID2, string createdby, string  createddate,string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, ShortName, Name, Telephone, MailId, ContactPerson, Address,partyID1,partyID2, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateProductMasterWithPartyID1(string Id, string partyID1)
        {
            bool bRetValue = false;
            string strSql = "Update masterproduct set prodPartyID_1 = '"+ partyID1 +"' where ProdCompID = '"+Id+"'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateProductMasterWithPartyID2(string Id, string partyID2)
        {
            bool bRetValue = false;
            string strSql = "Update masterproduct set prodPartyID_2 = '" + partyID2 + "' where ProdCompID = '" + Id + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateDetails(string Id, string ShortName, string Name, string Telephone, string MailId,
            string ContactPerson, string Address,string modifyby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, ShortName, Name, Telephone, MailId, ContactPerson, Address, modifyby,modifydate, modifytime);

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
            sQuery.AppendFormat("Select CompId from MasterCompany where CompName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND CompId in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select CompId from MasterCompany where CompName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND CompId not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetInsertQuery(string Id, string ShortName, string Name, string Telephone, string MailId,
            string ContactPerson, string Address,string partyID1,string partyID2, string createdby, string createddate,string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterCompany";
            objQuery.AddToQuery("CompId", Id);
            objQuery.AddToQuery("CompShortName", ShortName);
            objQuery.AddToQuery("CompName", Name);
            objQuery.AddToQuery("CompTelephone", Telephone);
            objQuery.AddToQuery("CompMailId", MailId);
            objQuery.AddToQuery("CompContactPerson", ContactPerson);
            objQuery.AddToQuery("CompAddress", Address);
            objQuery.AddToQuery("PartyID_1", partyID1);
            objQuery.AddToQuery("PartyID_2", partyID2);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string ShortName, string Name, string Telephone, string MailId,
            string ContactPerson, string Address,string modifyby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterCompany";
            objQuery.AddToQuery("CompId", Id, true);
            objQuery.AddToQuery("CompShortName", ShortName);
            objQuery.AddToQuery("CompName", Name);
            objQuery.AddToQuery("CompTelephone", Telephone);
            objQuery.AddToQuery("CompMailId", MailId);
            objQuery.AddToQuery("CompContactPerson", ContactPerson);
            objQuery.AddToQuery("CompAddress", Address);
            objQuery.AddToQuery("ModifiedUserID", modifyby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterCompany";
            objQuery.AddToQuery("CompId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion private methods


        
    }
}
