using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    public class DBCompany
    {
        public DBCompany()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select CompId, CompName, CompShortName, CompContactPerson,tag,PartyID_1,PartyID_2, PartyID_3, PartyID_4 from MasterCompany order by CompName";
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
        public DataRow ReadDetailsByID(int Id)
        {
            DataRow dRow = null;
            if (Id != 0)
            {
                string strSql = "Select * from MasterCompany where CompId='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(int Id, string ShortName, string Name, string Telephone, string MailId, 
            string ContactPerson, string Address,string partyID1,string partyID2, string partyID3, string partyID4, string createdby, string  createddate,string createdtime)
        {
            bool iRetValue = false;
            string strSql = GetInsertQuery(Id, ShortName, Name, Telephone, MailId, ContactPerson, Address,partyID1,partyID2, partyID3, partyID4, createdby, createddate, createdtime);
           if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                iRetValue = true;
            }
            return iRetValue;
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
        public bool UpdateProductMasterWithPartyID3(string Id, string partyID3) //Amar
        {
            bool bRetValue = false;
            string strSql = "Update masterproduct set prodPartyID_3 = '" + partyID3 + "' where ProdCompID = '" + Id + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateProductMasterWithPartyID4(string Id, string partyID4)  //Amar
        {
            bool bRetValue = false;
            string strSql = "Update masterproduct set prodPartyID_4 = '" + partyID4 + "' where ProdCompID = '" + Id + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateDetails(string Id, string ShortName, string Name, string Telephone, string MailId,
            string ContactPerson, string Address, string FirstCreditor, string SecondCreditor, string ThirdCreditor, string ForthCreditor, string modifyby, string modifydate, string modifytime)  // [ansuman][15.11.2016]
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, ShortName, Name, Telephone, MailId, ContactPerson, Address, FirstCreditor, SecondCreditor, ThirdCreditor, ForthCreditor, modifyby, modifydate, modifytime);

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

        //public bool IsNameUniqueForAdd(string Name, string Id)
        //{
        //    string strSql = GetDataForUniqueForAdd(Name, Id);
        //    bool bRetValue = false;
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}

        //public bool IsNameUniqueForEdit(string Name, string Id)
        //{
        //    string strSql = GetDataForUniqueForEdit(Name, Id);
        //    bool bRetValue = false;
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}  
        public bool IsNameUniqueForAdd(string Name, string Shortname, int id)
        {
            int ifdup = GetDataForUniqueForAdd(Name, Shortname, id);
            bool bRetValue = false;
            if (ifdup > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsNameUniqueForEdit(string Name, string Shortname, int id)
        {
            int ifdup = GetDataForUniqueForEdit(Name, Shortname,id);
            bool bRetValue = false;
            if (ifdup > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private int GetDataForUniqueForAdd(string Name, string Shortname, int id)
        {
            StringBuilder sQuery = new StringBuilder();
            DataRow dRow = null;
            string strSql = "Select CompId from MasterCompany where CompName = '" + Name + "' and CompShortName = '" + Shortname + "'";
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
       
        private int GetDataForUniqueForEdit(string Name, string Shortname, int id)
        {

            StringBuilder sQuery = new StringBuilder();
            DataRow dRow = null;
            string strSql = "Select CompId from MasterCompany where CompName = '" + Name + "' and CompShortName = '" + Shortname + "' and compID != " + id ;
            dRow = DBInterface.SelectFirstRow(strSql);
            if (dRow == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }

            //StringBuilder sQuery = new StringBuilder();
            //sQuery.AppendFormat("Select CompId from MasterCompany where CompName='{0}'", Name);
            //if (Id != "")
            //{
            //    sQuery.AppendFormat(" AND CompId not in ('{0}')", Id);
            //}
            //return sQuery.ToString();
        }
        private string GetInsertQuery(int Id, string ShortName, string Name, string Telephone, string MailId,
            string ContactPerson, string Address,string partyID1,string partyID2, string partyID3, string partyID4, string createdby, string createddate,string createdtime)
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
            objQuery.AddToQuery("PartyID_3", partyID3);
            objQuery.AddToQuery("PartyID_4", partyID4);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string ShortName, string Name, string Telephone, string MailId,
            string ContactPerson, string Address,string FirstCreditor, string SecondCreditor, string ThirdCreditor, string ForthCreditor, string modifyby, string modifydate, string modifytime) // [ansuman] [15.11.2016]
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
            objQuery.AddToQuery("PartyID_1", FirstCreditor);   
            objQuery.AddToQuery("PartyID_2", SecondCreditor);   // [ansuman] [15.11.2016]
            objQuery.AddToQuery("PartyID_3", ThirdCreditor);    // Amar
            objQuery.AddToQuery("PartyID_4", ForthCreditor);   // Amar
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

              
        public DataTable GetTableListByCode(string IDname)
        {
            string strSql = string.Format("Select "+ IDname +",CompName from masterCompany order by compName");
            DataTable dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}
