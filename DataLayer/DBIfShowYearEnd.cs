using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace EcoMart.DataLayer
{
    class DBIfShowYearEnd
    {

        public DBIfShowYearEnd()
        {
 
        }

        public DataTable GetTblAccountingYearData()            
        {
            DataTable dtable = new DataTable();
            string strsql = "Select * from tblAccountingyear order by VoucherSeries";
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        public DataRow GetTblAccountingYearData(string voucherSeries)
        {
            DataRow dr = null;
            string strsql = "Select * from tblAccountingyear where voucherSeries = '"+voucherSeries +"'";
            dr = DBInterface.SelectFirstRow(strsql);
            return dr;
        }

    }
}
