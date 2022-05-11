using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;


namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBBarCode
    {
        public DBBarCode()
        {
        }
        public DataTable GetProductData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select * from MasterProduct order by ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetBatchData(string iD)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select * from tblStock where productID = '"+ iD +"' order by Expiry";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}
