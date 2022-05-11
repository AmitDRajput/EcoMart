using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    class DBGeneral
    {
        public DataTable GetTableListByCode(string IDname, string OrderField, string tableName)
        {
            string strSql = string.Format("Select " + IDname + "," + OrderField + " from " + tableName + " order by " + OrderField);
            DataTable dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetTableListVouchers(string IDname, string voutype, int vouno, string vousubtype, string vouSeries, string tableName)
        {
            string strSql = string.Format("Select " + IDname + " from " + tableName + " where voucherSubType = '" + vousubtype + "' && VoucherSeries = '" + vouSeries + "'  order by vouchertype,voucherNumber,vouchersubtype");
            DataTable dtable = DBInterface.SelectDataTable(strSql);
            return dtable;


        }
    }
}
