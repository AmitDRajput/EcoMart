using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBGroup
    {
        public DBGroup()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GroupName, GroupId,GroupCode,UnderGroupId,IFMainGroup from MasterGroup  where levelNumber != 28 && levelNumber != 31 && IfMainGroup != 'Y' order by GroupName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
      
        public DataTable GetOverviewDataALL()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GroupName,GroupId,GroupCode,UnderGroupId,IFMainGroup,IFFIX,UnderGroupIdParentID from MasterGroup where  UnderGroupIdParentID < 20  && levelNumber != 28 && levelNumber != 31 order by GroupName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForFixedCode(string fixcode)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GroupName, GroupId,GroupCode,UnderGroupId,IFMainGroup,IFFIX from MasterGroup where GroupID = '" +  fixcode + "'  order by GroupName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForCreditor()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GroupName, GroupId,GroupCode,UnderGroupId,IFMainGroup,IFFIX from MasterGroup where GroupID = '31'  order by GroupName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForBank()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GroupName, GroupId,GroupCode,UnderGroupId,IFMainGroup,IFFIX from MasterGroup where GroupID = '23'  order by GroupName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForOtherCreditor()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GroupName, GroupId,GroupCode,UnderGroupId,IFMainGroup,IFFIX from MasterGroup where GroupID = '101'  order by GroupName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForOtherDebtor()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GroupName, GroupId,GroupCode,UnderGroupId,IFMainGroup,IFFIX from MasterGroup where GroupID = '102'  order by GroupName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForGeneral()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GroupName, GroupId,GroupCode,UnderGroupId,IFMainGroup,IFFIX from MasterGroup where  IfMainGroup != 'Y' && IfMainGroup != 'X' order by GroupName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetGroup()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GroupName, GroupId,UnderGroupId,IFMainGroup,IFFIX from MasterGroup order by GroupName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetGroupIDInteger()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GroupId from MasterGroup order by LevelNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select GroupName, GroupId,UnderGroupId,IFMainGroup,IFFIX,UnderGroupIDParentID from MasterGroup where GroupId='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadUnerGroupByID(string UnderGroupId)
        {
            DataRow dRow = null;
            if (UnderGroupId != "")
            {
                string strSql = "Select GroupName, GroupId,UnderGroupId,IFMainGroup,IFFIX,UnderGroupIDParentID from MasterGroup where GroupId='{0}' ";
                strSql = string.Format(strSql, UnderGroupId);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(int groupIdInt, string GroupName, string UnderGroupId, string iffix, string ifmain, int UnderGroupIDParentID, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(groupIdInt, GroupName, UnderGroupId, iffix, ifmain, UnderGroupIDParentID, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(int groupIdInt, string GroupName, string UnderGroupId, string iffix, string ifmain, int UnderGroupIDParentID, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(groupIdInt, GroupName, UnderGroupId, iffix, ifmain, UnderGroupIDParentID, modifiedby, modifieddate, modifiedtime);

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
            sQuery.AppendFormat("Select GroupID from MasterGroup where GroupName = '{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND GroupId = ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select GroupID from MasterGroup where GroupName = '{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND GroupId != ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetInsertQuery(int groupIdInt, string GroupName, string UnderGroupId, string iffix, string ifmain, int UnderGroupIDParentID, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterGroup";
            objQuery.AddToQuery("GroupId", groupIdInt);
            objQuery.AddToQuery("GroupName", GroupName);
            objQuery.AddToQuery("UnderGroupId", UnderGroupId);
            objQuery.AddToQuery("IFFIX", "N");
            objQuery.AddToQuery("IFMainGroup", "N");
            objQuery.AddToQuery("GroupCode", "G");
            objQuery.AddToQuery("LevelNumber", groupIdInt);
            objQuery.AddToQuery("UnderGroupIDParentID", UnderGroupIDParentID);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
           
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(int groupIdInt, string GroupName, string UnderGroupId, string iffix, string ifmain, int UnderGroupIDParentID, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "MasterGroup";
            objQuery.AddToQuery("GroupId", groupIdInt, true);
            objQuery.AddToQuery("GroupName",GroupName);
            objQuery.AddToQuery("UnderGroupId", UnderGroupId);
            objQuery.AddToQuery("IFFIX", "N");
            objQuery.AddToQuery("IFMainGroup", "N");
            objQuery.AddToQuery("GroupCode", "G");
            objQuery.AddToQuery("LevelNumber", groupIdInt);
            objQuery.AddToQuery("UnderGroupIDParentID", UnderGroupIDParentID);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterGroup";
            objQuery.AddToQuery("GroupId", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 

    
      
    }
    
}
