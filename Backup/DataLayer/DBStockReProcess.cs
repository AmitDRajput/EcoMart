using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;


namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBStockReProcess
    {
        public DBStockReProcess()
        {

        }

        public bool InitializeMasterProduct()
        {
            bool bRetValue = false;
            string strSql = "update masterproduct  set prodclosingstock = 0, prodopeningstock = 0";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateMasterProduct()
        {
            bool bRetValue = false;
            string strSql = "update masterproduct  set prodclosingstock = (select COALESCE(sum(tblstock.closingstock),0) from tblstock where masterproduct.productid = tblstock.productid group by tblstock.productid)";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "update masterproduct  set prodopeningstock = (select COALESCE(sum(tblstock.openingstock),0) from tblstock where masterproduct.productid = tblstock.productid group by tblstock.productid)";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool RemoveNegetiveStockFromtblStock()
        {
            bool bRetValue = false;
            string strSql = "update tblstock  set closingstock = 0  where closingstock < 0";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            strSql = "update tblstock  set openingstock = 0  where openingstock < 0";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public DataTable GetCompanyData()
        {
            DataTable dtable = new DataTable();
            string strSql = "select * from mastercompany";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public  void  UpdateProductMaster(string compID, string compShortName)
        {
            
           
            string strSql = "update masterproduct  set ProdCompShortName = '" + compShortName + "'  where ProdCompID = '"+compID +"'";

            DBInterface.ExecuteQuery(strSql);
           
        }
    }
}
