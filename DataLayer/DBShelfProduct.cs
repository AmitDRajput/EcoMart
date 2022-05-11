using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace EcoMart.DataLayer
{
    public class DBShelfProduct
    {
        # region Constructor
        public DBShelfProduct()
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
            string strSql = "Select distinct a.productID, a.ProdName ,a.ProdLoosePack,a.ProdPack,a.ProdCompShortName, b.ProductID, b.AccountID,b.Quantity from  masterproduct a ,linkdebtorproduct b   where   b.ProductID = a.ProductID  and b.AccountID = " + '"' + DD + '"' + " order by a.prodName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
      

        
        public bool UpdateMasterProductClearShelfId(string Id)
        {
            bool bRetValue = false;
            string strSql = "Update masterproduct set prodshelfID = ''  where prodshelfID = '" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateMasterProductByProductId(string Id, string productID)
        {
            bool bRetValue = false;
            string strSql = "Update masterproduct set prodshelfID = '"+  Id  + "' where ProductID = '" + productID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public DataTable ReadProdDetailsByShelfId(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select distinct a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdCompShortName,a.ProdShelfID,b.ShelfID,b.ShelfCode " +
                                "from masterproduct a inner join mastershelf b on a.ProdShelfId = b.ShelfId  and " +
                                  " a.ProdShelfID = '{0}'";

                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);


            }
            return dt;
        }
        # endregion  
    }
}
