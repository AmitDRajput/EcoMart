using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

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

        public int GetNextIntID(string tableName, string fieldName)
        {
            int RetValue = 0;
            try
            {
                string strSql = string.Format("Select max({0}) as maxid from {1} ", fieldName, tableName);
                DataRow dRow = DBInterface.SelectFirstRow(strSql);
                if (dRow != null)
                {
                    if (dRow["maxid"] != null && dRow["maxid"].ToString() != string.Empty)
                    {
                        RetValue = Convert.ToInt32(dRow["maxid"]) + 1;
                    }
                }
                
                if (RetValue == 0)
                    RetValue = 1;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return RetValue;
        }
    }
}
