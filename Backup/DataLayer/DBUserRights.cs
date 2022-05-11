using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{
    class DBUserRights
    {
        public DBUserRights()
        { 
        }


        public bool IsNameUnique(string Name, string Id)
        {
            string strSql = GetDataForUnique(Name, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDetails(string ID, string fname, int add, int edit, int del, int view, int print, string crby,string crdate,string crtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(ID,fname, add, edit, del, view, print,crby,crdate,crtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateDetails(string ID, string fname, int add, int edit, int del, int view, int print, string moby,string modate,string motime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(ID, fname, add, edit, del, view, print,moby,modate,motime);

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
        #region Query Building Functions
        private string GetDeleteQuery(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "tbluserrights";
            objQuery.AddToQuery("ID",Id,true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        private string GetDataForUnique(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ID from tbluserrights where FormName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND ID not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        public DataTable GetLevel()
        {
            DataTable dtable=new DataTable();
            string strSql = string.Format("select FormName,AddModule,DeleteModule,EditModule,ViewModule,PrintModule from tbluserrights ");
            dtable =DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetLevel(string fname)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("select FormName,AddModule,DeleteModule,EditModule,ViewModule,PrintModule from tbluserrights where FormName'{0}'", fname);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetLevelData()
        {
            DataTable dtable = new DataTable();
           
            string strSql = string.Format("select ID,Type from tbluserlevel  order by ID");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetComboLevelData()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("select ID,Type from tbluserlevel where ID != '0' ");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("select ID,FormName,AddModule,EditModule,DeleteModule,ViewModule,PrintModule from tbluserrights");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable ReadDetails()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("select ID,FormName,AddModule,EditModule,DeleteModule,ViewModule,PrintModule from tbluserrights");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByID(string  formname)
        {
            DataRow dtrow=null;
            if (formname != null)
            {
                string strSql = string.Format("select ID,FormName,AddModule,DeleteModule,EditModule,ViewModule,PrintModule from tbluserrights where FormName='{0}'", formname);
                dtrow=DBInterface.SelectFirstRow(strSql);
            }
            return dtrow;
        }
        private string GetInsertQuery(string ID,string fname, int add, int edit, int del, int view, int print, string crby, string crdate, string crtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbluserrights";
            objQuery.AddToQuery("ID", ID);
            objQuery.AddToQuery("FormName", fname);
            objQuery.AddToQuery("AddModule",add);
            objQuery.AddToQuery("EditModule", edit);
            objQuery.AddToQuery("DeleteModule", del);
            objQuery.AddToQuery("ViewModule", view);
            objQuery.AddToQuery("PrintModule", print);
            objQuery.AddToQuery("CreatedUserID", crby);
            objQuery.AddToQuery("CreatedDate", crdate);
            objQuery.AddToQuery("CreatedTime", crtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string ID, string fname, int add, int edit, int del, int view, int print , string moby, string modate, string motime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbluserrights";
            objQuery.AddToQuery("ID", ID,true);
            objQuery.AddToQuery("FormName", fname);
            objQuery.AddToQuery("AddModule", add);
            objQuery.AddToQuery("EditModule", edit);
            objQuery.AddToQuery("DeleteModule", del);
            objQuery.AddToQuery("ViewModule", view);
            objQuery.AddToQuery("PrintModule", print);
            objQuery.AddToQuery("ModifiedUserID", moby);
            objQuery.AddToQuery("ModifiedDate", modate);
            objQuery.AddToQuery("ModifiedTime", motime);
            return objQuery.UpdateQuery();
        }
        #endregion
    }

}
