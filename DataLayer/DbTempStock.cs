using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.Common;


namespace EcoMart.DataLayer
{
    class DbTempStock
    {
        public DataTable GetAllTempStockRows()
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select TempStockID,StockID,ProductID,SoldQuantity,ModuleNumber,CompName,Mode,CustomerNumber from tbltempstock ");
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetStockByProductID(int prodID, int mode)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select TempStockID,StockID,ProductID,SoldQuantity,ModuleNumber,CompName,Mode,CustomerNumber from tbltempstock where ProductID = '{0}' AND Mode={1}", prodID, mode);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetStockByStockID(string stockID, int moduleNumber, string compName, int mode)
        {
            DataTable dtable = new DataTable();
            string strSql = string.Format("Select TempStockID,StockID,ProductID,SoldQuantity,ModuleNumber,CompName,Mode,CustomerNumber from tbltempstock where StockID = '{0}' AND ModuleNumber = {1} AND CompName = '{2}' AND Mode={3}", stockID, moduleNumber, compName, mode);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public string GetStockByStockIDAndProductID(string stockID, int ProductID, int moduleNumber, string compName, int mode, int customerNumber)
        {
            string retValue = string.Empty;
            try
            {
                DataTable dtable = new DataTable();
                DataRow dRow = null;
                string strSql = string.Format("Select TempStockID, StockID,ProductID,ModuleNumber,CompName,Mode,CustomerNumber from tbltempstock where StockID = '{0}' AND ProductID = '{1}' AND ModuleNumber = {2} AND CompName = '{3}' AND Mode={4} AND CustomerNumber={5}", stockID, ProductID, moduleNumber, compName, mode, customerNumber);
                dRow = DBInterface.SelectFirstRow(strSql);
                if (dRow != null)
                {
                    retValue = dRow["TempStockID"].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public bool AddDetails(string Id, string stockID, int ProductID, int soldQuantity, int moduleNumber, string compName, int mode, int customerNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, stockID, ProductID, soldQuantity, moduleNumber, compName, mode, customerNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string stockID, int ProductID, int soldQuantity, int moduleNumber, string compName, int mode, int customerNumber)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, stockID, ProductID, soldQuantity, moduleNumber, compName, mode, customerNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeleteDetails(string stockID, int ProductID, int soldQuantity, int moduleNumber, string compName, int customerNumber)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(stockID, ProductID, soldQuantity, moduleNumber, compName, customerNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeleteDetailsByModuleNumber(int moduleNumber, string compName)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQueryByModuleNumber(moduleNumber, compName);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeleteDetailsByModuleNumberAndCustomerNumber(int moduleNumber, string compName, int customerNumber)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQueryByModuleNumberAndCustomerNumber(moduleNumber, compName, customerNumber);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool DeleteDetailsByComputerName(string compName)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQueryByComputerName(compName);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQuery(string Id, string stockID, int ProductID, int soldQuantity, int moduleNumber, string compName, int mode, int customerNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltempstock";
            objQuery.AddToQuery("TempStockID", Id);
            objQuery.AddToQuery("StockID", stockID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("SoldQuantity", soldQuantity);
            objQuery.AddToQuery("ModuleNumber", moduleNumber);
            objQuery.AddToQuery("CompName", compName);
            objQuery.AddToQuery("Mode", mode);
            objQuery.AddToQuery("CustomerNumber", customerNumber);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string stockID, int ProductID, int soldQuantity, int moduleNumber, string compName, int mode, int customerNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltempstock";
            objQuery.AddToQuery("TempStockID", Id, true);
            objQuery.AddToQuery("StockID", stockID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("SoldQuantity", soldQuantity);
            objQuery.AddToQuery("ModuleNumber", moduleNumber);
            objQuery.AddToQuery("CompName", compName);
            objQuery.AddToQuery("Mode", mode);
            objQuery.AddToQuery("CustomerNumber", customerNumber);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string stockID, int ProductID, int soldQuantity, int moduleNumber, string compName, int customerNumber)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tbltempstock";
            objQuery.AddToQuery("StockID", stockID, true);
            objQuery.AddToQuery("ProductID", ProductID, true);
            objQuery.AddToQuery("SoldQuantity", soldQuantity, true);
            objQuery.AddToQuery("ModuleNumber", moduleNumber, true);
            objQuery.AddToQuery("CompName", compName, true);
            objQuery.AddToQuery("CustomerNumber", customerNumber, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }

        private string GetDeleteQueryByModuleNumber(int moduleNumber, string compName)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tbltempstock";            
            objQuery.AddToQuery("ModuleNumber", moduleNumber, true);
            objQuery.AddToQuery("CompName", compName, true);          
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        private string GetDeleteQueryByModuleNumberAndCustomerNumber(int moduleNumber, string compName, int customerNumber)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tbltempstock";
            objQuery.AddToQuery("ModuleNumber", moduleNumber, true);
            objQuery.AddToQuery("CompName", compName, true);
            objQuery.AddToQuery("CustomerNumber", customerNumber, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        private string GetDeleteQueryByComputerName(string compName)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "tbltempstock";           
            objQuery.AddToQuery("CompName", compName, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
    }
}
