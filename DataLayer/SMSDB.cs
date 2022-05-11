
using EcoMart.Common;
using EcoMart.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EcoMart.DataLayer
{
    public class SMSDB
    {

        public DataTable GetSettings()
        {

            string query = "Select * from SMSSettings order by priority";
            DataTable dtable = new DataTable();
            dtable = DBInterface.SelectDataTable(query);
            return dtable;
        }

        //nandini 25th oct 2010
        public DataTable GetSMSsettings()
        {
            string query = null;
            DataTable dtable = new DataTable();
            query = "Select * from SMSSettings ";
            dtable = DBInterface.SelectDataTable(query);

            return dtable;

        }
        //nandini 25th oct 2010
        public DataTable GetSMSsettingsById(string Id)
        {

            string query = null;
            DataTable dtable = new DataTable();

            query = "Select * from SMSSettings where Id= " + Id;
            dtable = DBInterface.SelectDataTable(query);

            return dtable;

        }
        //nandini 25th oct 2010
        public bool GetSMSsettingsByURL(string Name, string Id)
        {

            bool flag = false;
            string exename = null;

            if (Id == string.Empty)
            {
                exename = DBInterface.GetSingleFieldData("Select count(*) From smssettings Where Upper(URL)='" + Name.ToUpper() + "'");
            }
            else
            {
                exename = DBInterface.GetSingleFieldData("Select count(*) From smssettings Where Upper(URL)='" + Name.ToUpper() + "' and Id not in (" + Id + ")");
            }

            int cnt = 0;
            if (exename != "No Rec Found")
            {
                try
                {
                    cnt = int.Parse(exename);
                    if (cnt > 0)
                    {
                        flag = true;
                    }

                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.Message);
                }
            }

            return flag;

        }
        //nandini 25th oct 2010
        public bool GetSMSsettingsByName(string Name, string Id)
        {

            bool flag = false;
            string exename = null;

            if (Id == string.Empty)
            {
                exename = DBInterface.GetSingleFieldData("Select count(*) From smssettings Where Upper(Name)='" + Name.ToUpper() + "'");
            }
            else
            {
                exename = DBInterface.GetSingleFieldData("Select count(*) From smssettings Where Upper(Name)='" + Name.ToUpper() + "' and Id not in (" + Id + ")");
            }

            int cnt = 0;
            if (exename != "No Rec Found")
            {
                try
                {
                    cnt = int.Parse(exename);
                    if (cnt > 0)
                    {
                        flag = true;
                    }

                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.Message);
                }
            }

            return flag;

        }
        //nandini 25th oct 2010
        public bool InsertSMSSettings(string uRL, string name, int priority)
        {
            bool res = false;
            string strSql = GetInsertQueryP(uRL, name, priority);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                res = true;
            }
            return res;
        }
        //nandini 25th oct 2010
        public bool UpdateSMSSettings(string id, string uRL, string name, int priority)
        {

            bool res = false;
            string strSql = GetUpdateQuery(Convert.ToInt32(id),uRL, name, priority);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                res = true;
            }
            return res;
        }

        private string GetInsertQueryP(string uRL, string name, int priority)
        {
            Query objQuery = new Query();
            objQuery.Table = "smssettings";
            //objQuery.AddToQuery("Id", id);
            objQuery.AddToQuery("URL", uRL);
            objQuery.AddToQuery("Name", name);
            objQuery.AddToQuery("Priority", priority);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(int id, string uRL, string name, int priority)
        {
            Query objQuery = new Query();
            objQuery.Table = "smssettings";
            objQuery.AddToQuery("Id", id, true);
            objQuery.AddToQuery("URL", uRL);
            objQuery.AddToQuery("Name", name);
            objQuery.AddToQuery("Priority", priority);
            return objQuery.UpdateQuery();
        }
        //nandini 25th oct 2010
        public int DeleteAlertById(string Id)
        {
            int res = 0;
            string Dquery = "delete from SMSSettings where Id=" + Id;
            res = DBInterface.ExecuteQuery(Dquery);
            return res;
        }

    }

}
//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
