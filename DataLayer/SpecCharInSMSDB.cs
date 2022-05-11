
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using EcoMart.Common;
using System;

namespace EcoMart.DataLayer
{

    public class SpecCharInSMSDB
    {
        //nandini 13th dec
        public DataTable getSpecChar()
        {
            DataTable ds = new DataTable();
            string query = "Select * from SpecCharInSMS";
            ds = DBInterface.SelectDataTable(query);
            return ds;
        }
        //nandini 13th dec
        public bool GetSpecCharByName(string Name, string Id)
        {

            bool flag = false;
            string exename = null;

            if (Id == string.Empty)
            {
                exename = DBInterface.GetSingleFieldData("Select count(*) From SpecCharInSMS Where Upper(Find)='" + Name.ToUpper() + "'");
            }
            else
            {
                exename = DBInterface.GetSingleFieldData("Select count(*) From SpecCharInSMS Where Upper(Find) = '" + "' and Id not in (" + Id + ")");
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
        //nandini 13 dec 2010

        //public int InsertSpecChar(SpecCharInSMSDS objDS)
        //{

        //    int res = 0;


        //   // res = DataAccess.InsertDSAndGetIdentity(objDS, objDS.SpecCharInSMS.TableName);

        //    return res;


        //}
        //nandini 13th dec 2010

        public DataTable GetSpecCharById(string Id)
        {

            string query = null;
            DataTable dt = new DataTable();

            query = "Select * from SpecCharInSMS where Id= " + Id;
            DBInterface.ExecuteQuery(query);

            return dt;

        }
        //nandini 4 oct 2010

       
        //nandini 4 oct 2010

        public int DeleteSpecCharById(string Id)
        {
            int res = 0;
            //string Dquery = "delete from SpecCharInSMS where Id=" + Id;
            //res = DBInterface.DeleteRecord(Dquery);

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