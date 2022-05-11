using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBAccountingYear
    {
        public DBAccountingYear()
        {
        }
        #region Private methods
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select VoucherSeries,FromDate,ToDate,YearEndOver,CurrentYear from tblAccountingyear order by FromDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow ReadDetailsByVoucherSeries(string voucherSeries)
        {
            DataRow dRow = null;
            if (voucherSeries != "")
            {
                string strSql = "Select * from tblAccountingyear where VoucherSeries='{0}' for update ";
                strSql = string.Format(strSql, voucherSeries);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public bool AddDetails(string VoucherSeries, string FromDate, string ToDate, string YearEndOver)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(VoucherSeries, FromDate, ToDate, YearEndOver);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateCurrentYearColumn()
        {
            bool bRetValue = false;
            string strSql = "Update tblAccountingyear set CurrentYear = 'N'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddRowIntblVoucherNumber(string VoucherSeries)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryFortblVoucherNumbers(VoucherSeries);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        //public bool UpdateDetails(string Id, string Name, string modifiedby, string modifydate, string modifytime)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetUpdateQuery(Id, Name, modifiedby, modifydate, modifytime);

        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}

        public bool DeleteDetails(string voucherSeries)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(voucherSeries);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQuery(string VoucherSeries, string FromDate, string ToDate, string YearEndOver)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblAccountingyear";
            objQuery.AddToQuery("VoucherSeries", VoucherSeries);
            objQuery.AddToQuery("FromDate", FromDate);
            objQuery.AddToQuery("ToDate", ToDate);
            objQuery.AddToQuery("YearEndOver", YearEndOver);
            objQuery.AddToQuery("CurrentYear", "Y");
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryFortblVoucherNumbers(string VoucherSeries)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblvouchernumbers";
            objQuery.AddToQuery("ID", VoucherSeries);           
            return objQuery.InsertQuery();
        }
        private string GetDeleteQuery(string voucherSeries)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tblAccountingyear";
            objQuery.AddToQuery("VoucherSeries", voucherSeries, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion private methods

    }
}
