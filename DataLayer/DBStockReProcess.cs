using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;


namespace EcoMart.DataLayer
{
    public class DBStockReProcess
    {
        public DBStockReProcess()
        {

        }

        public bool InitializeMasterProduct()
        {
            bool bRetValue = true;
            string strSql = "update masterproduct  set prodclosingstock = 0, prodopeningstock = 0";
            DBInterface.ExecuteQuery(strSql);
           
            return bRetValue;
        }

        //public bool UpdateMasterProduct()
        //{
        //    bool bRetValue = false;
        //    string strSql = "update masterproduct  set prodclosingstock = (select COALESCE(sum(tblstock.closingstock),0) from tblstock where masterproduct.productid = tblstock.productid   group by tblstock.productid)";

        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }

        //    strSql = "update masterproduct  set prodopeningstock = (select COALESCE(sum(tblstock.openingtock),0) from tblstock where masterproduct.productid = tblstock.productid   group by tblstock.productid)";

        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}

        public bool RemoveNegetiveStockFromtblStock()
        {
            bool bRetValue = true;
            string strSql = "update tblstock  set closingstock = 0  where (closingstock is null ||  closingstock < 0)"; 

            int mstrsql = DBInterface.ExecuteQuery(strSql);
           

            strSql = "update tblstock  set openingstock = 0  where (openingstock is null || openingstock < 0)";

            mstrsql = DBInterface.ExecuteQuery(strSql);
           
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

        public DataTable GetStockFromtblStock()
        {
            DataTable dt;
            string strSql = "Select productID, COALESCE(sum(openingStock),0) as opstk,COALESCE(sum(closingstock),0) as clstk from tblstock  group by productid";

            dt = DBInterface.SelectDataTable(strSql);
            return dt;
           
        }

        public bool UpdateStockInMasterProduct(string mprodID, int mopstk, int mclstk)
        {
            bool retValue = false;
            string strSql = "update masterproduct  set ProdClosingStock = " + mclstk + ", ProdOpeningStock = " + mopstk + "  where ProductID = '" + mprodID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            else
                retValue = false;
            return retValue;
        }
    }
}
