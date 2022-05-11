using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{
    class DBYearEnd
    {
        public DBYearEnd()
        {
        }

        public bool CreateNewBase(string currentdatabase, string newdatabase)
        {
            bool bRetValue = false;
           
                //SELECT IF('db3' IN(SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA), 1, 0) AS found
                string strSql = "CREATE DATABASE IF NOT EXISTS " + newdatabase + " DEFAULT CHARACTER SET latin1 COLLATE latin1_german1_ci";// +newdatabase;
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            
            return bRetValue;
        }
        public bool CreateTable(string currentdatabase, string newdatabase, string tablename)
        {
            bool bRetValue = false;
            //mysql> CREATE TABLE new_table LIKE old_table;

            //and then copying the data in:

            //mysql> INSERT INTO new_table SELECT * FROM old_table;
            string strSql = "CREATE TABLE " + newdatabase + "." + tablename + " LIKE " + currentdatabase + "." + tablename;
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            DBInterface.ExecuteQuery(strSql); 
                strSql = "INSERT INTO " + newdatabase + "." + tablename + " SELECT * FROM " + currentdatabase + "." + tablename;
                DBInterface.ExecuteQuery(strSql);               
                bRetValue = true;
            //}

           // string strSql = "Create TABLE " + newdatabase + "." + tablename + " Select * from " + currentdatabase + "." + tablename;
            // //CREATE TABLE pharmasysretailplus1516.changeddetailcashbankexpenses SELECT * FROM pharmasysretailplus1415.changeddetailcashbankexpenses;
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}

            return bRetValue;
        }

        public bool RemovePreviousYear(string voucherseries)
        {
            bool bRetValue = false;

            string strSql = "Delete from tblaccountingyear where voucherseries = '"+voucherseries +"'";
            // //CREATE TABLE pharmasysretailplus1516.changeddetailcashbankexpenses SELECT * FROM pharmasysretailplus1415.changeddetailcashbankexpenses;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool AddRowForCurrentAccountingYear(string newvoucherseries, string newsyear, string neweyear)
        {
            bool bRetValue = false;

            string strSql = "Insert into tblaccountingyear set voucherseries = '" + newvoucherseries + "' , FromDate = '" + newsyear + "' , Todate = '" + neweyear + "' , Yearend = 'N', CurrentYear = 'Y' ";
            // //CREATE TABLE pharmasysretailplus1516.changeddetailcashbankexpenses SELECT * FROM pharmasysretailplus1415.changeddetailcashbankexpenses;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool IfCurrentYearExists(string newvoucherseries)
        {
            bool bRetValue = false;
            DataRow dt;
            string strSql = "Select * from tblaccountingyear where voucherseries = '" + newvoucherseries + "'";
            // //CREATE TABLE pharmasysretailplus1516.changeddetailcashbankexpenses SELECT * FROM pharmasysretailplus1415.changeddetailcashbankexpenses;
            dt = DBInterface.SelectFirstRow(strSql);
            if (dt != null)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool DefinePrimaryKeys(string tablename, string primarykey)
        {
            bool bRetValue = false;

            string strSql = "alter table " + tablename + " ADD PRIMARY KEY (" + primarykey + ")";
                
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }
        public bool DefineUniqueKeys(string tablename, string primarykey)
        {
            bool bRetValue = false;

            string strSql = "alter table " + tablename + " ADD UNIQUE KEY (" + primarykey + ")";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }
    }
}
