using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace EcoMart.DataLayer
{
    public class DBDebtorProduct
    {
        # region Constructor
        public DBDebtorProduct()
        {
        }
        # endregion

        # region Other Private Methods

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();    
            string strSql = "Select distinct a.AccName, a.AccountID,a.AccAddress1,a.AccAddress2 from Masteraccount  a  INNER Join linkdebtorproduct b where a.AccountID = b.AccountID order by a.AccName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetPrescription()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select AccountID,AccCode,AccName,AccAddress1,AccAddress2 " +
                            "from masteraccount order by AccName";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDebtorData(string DD)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.ProductID, a.ProdName ,a.ProdLoosePack,a.ProdPack,a.ProdCompShortName, b.ProductID, b.AccountID,b.Quantity from  masterproduct a ,linkdebtorproduct b   where   b.ProductID = a.ProductID  and b.AccountID = " + '"' + DD + '"' + " order by a.prodName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewProductDataY()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.ProductID, a.ProdName ,a.ProdLoosePack,a.ProdPack,a.ProdCompShortName, b.ProductID from  masterproduct a inner join  linkdebtorproduct b   on   b.ProductID = a.ProductID order by a.prodName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDebtorDataY(int prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.AccName, a.AccountID,a.AccAddress1,a.AccAddress2 from Masteraccount a INNER Join linkdebtorproduct b  on b.AccountID = a.AccountID where b.ProductID = '"+ prodID +"' order by a.AccName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

      
        public DataTable GetAllDebtors()
        {
            string strSql = "Select AccountID,AccCode,AccName,AccAddress1,AccAddress2 " +
                            "from masteraccount order by AccName";
            return DBInterface.SelectDataTable(strSql);
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strsql = "Select AccountID,AccCode,AccName,AccAddress1,AccAddress2 " +
                                "from masteraccount  where AccountID= '{0}'";

                strsql = string.Format(strsql, Id);
                dRow = DBInterface.SelectFirstRow(strsql);
            }
            return dRow;
        }

        public bool DeleteProductsByID(string Id)
        {
            bool bRetValue = true;

            if (Id != "")
            {

                string strsql = "Delete from linkdebtorproduct where AccountID= '{0}'";

                strsql = string.Format(strsql, Id);
                if (DBInterface.ExecuteQuery(strsql) > 0)
                {
                    bRetValue = true;
                }


            }
            return bRetValue;

        }
        public DataTable IsPartyAlreadyLinked(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strSql = "Select * from linkdebtorproduct where AccountId='{0}' ";
                strSql = string.Format(strSql, Id);
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataTable ReadProdDetailsById(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select distinct a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdCompShortName,b.Quantity,b.AccountID,b.CreatedDate,b.CreatedTime,b.CreatedUserID,b.ModifiedDate,b.ModifiedTime,b.ModifiedUserID " +
                                "from linkdebtorproduct b ,masterproduct a  where b.ProductID = a.ProductID  and " +
                                  " b.AccountID= '{0}'";

                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);


            }
            return dt;
        }

      

        public DataTable ReadProdDetailsByIdForDebtorSale(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select distinct a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdCompShortName,a.ProdClosingStock,a.ProdVATPercent,a.ProdShelfID,b.Quantity,b.Quantity as SQuantity,b.AccountID " +
                                "from linkdebtorproduct b ,masterproduct a  where b.ProductID = a.ProductID  and " +
                                  " b.AccountID= '{0}'";             
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);


            }
            return dt;
        }

      
        public bool DeleteDetails(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(Id);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
     
        public bool AddProductDetails(string accId, string detailID, int ProductID, int Quantity, string createdby, string createddate, string createdtime, string modifiedby, string modifieddate, string modifiedtime)
        {

            bool bRetValue = false;
            string strSql = GetInsertProductQuery(accId, detailID, ProductID, Quantity,createdby,createddate,createdtime,modifiedby,modifieddate,modifiedtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
      
        private string GetInsertProductQuery(string accId, string detailID, int ProductID, int Quantity, string createdby, string createddate, string createdtime, string modifyby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "linkdebtorproduct";
            objQuery.AddToQuery("LinkDebtorProductID", detailID);
            objQuery.AddToQuery("AccountID", accId);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            objQuery.AddToQuery("ModifiedUserID", modifyby);

            return objQuery.InsertQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "linkdebtorproduct";
            objQuery.AddToQuery("AccountID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        # endregion 
    
    
    
      
    }
}
