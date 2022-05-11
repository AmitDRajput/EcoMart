using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    public  class DBScheme
    {
        public DBScheme()
        {
        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ProductID,a.ProductQuantity1,a.SchemeQuantity1,a.ProductQuantity2,a.SchemeQuantity2,"+
                            "a.ProductQuantity3,a.SchemeQuantity3,a.StartingDate,a.ClosingDate,a.IfSchemeClosed,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName,b.ProdCompID from masterscheme a inner join masterproduct b on a.ProductID = b.ProductID order by b.ProdName";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetOverviewDataForSchemeListAll(string today)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ProductID,a.ProductQuantity1,a.SchemeQuantity1,a.ProductQuantity2,a.SchemeQuantity2," +
                            "a.ProductQuantity3,a.SchemeQuantity3,a.StartingDate,a.ClosingDate,a.IfSchemeClosed,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName,b.ProdCompID from masterscheme a inner join masterproduct b on a.ProductID = b.ProductID where a.ClosingDate >= '"+ today + "' order by b.ProdName ";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetSchemeDataForSelectedCompany(string compid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ProductID,a.ProductQuantity1,a.SchemeQuantity1,a.ProductQuantity2,a.SchemeQuantity2," +
                            "a.ProductQuantity3,a.SchemeQuantity3,a.StartingDate,a.ClosingDate,a.IfSchemeClosed,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName,b.ProdCompID from masterscheme a inner join masterproduct b on a.ProductID = b.ProductID where  b.ProdCompID = '"+ compid +"' order by b.ProdName";

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetOverviewDataForSelectedCompany(string mcompcd)
        { 
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.PurchaseID, a.ProductID,a.Quantity,a.SchemeQuantity,b.AccountID,b.purchaseID,b.VoucherType,b.voucherDate,b.VoucherNumber," +
                            "c.AccountID,c.AccName,d.ProductID,d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName,d.ProdCompID from detailpurchase a inner join voucherpurchase b on a.PurchaseID = b.PurchaseID  inner join masteraccount c on b.AccountID = c.AccountID inner join masterproduct d on a.ProductId = d.ProductId where d.ProdCompId = '" + mcompcd + "' order by d.ProdName;";
                      

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataTable GetOverviewDataForSelectedProduct(string mproductid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID, a.ProductID,a.Quantity,a.SchemeQuantity,b.AccountID,b.purchaseID,"+
                            "c.AccountID,c.AccName,d.ProductID,d.ProdName,d.ProdLoosePack,d.ProdPack,d.ProdCompShortName from detailpurchase a inner join voucherpurchase b on a.PurchaseID = b.PurchaseID  inner join masteraccount c on b.AccountID = c.AccountID inner join masterproduct d on a.ProductId = d.ProductId where d.ProdCompId = '" + mproductid + "' order by c.AccName;";
                      

            dtable = DBInterface.SelectDataTable(strSql);

            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select a.ID, a.ProductID,a.ProductQuantity1, a.SchemeQuantity1,a.ProductQuantity2,a.SchemeQuantity2, a. ProductQuantity3,a.schemeQuantity3,a.StartingDate,a.ClosingDate,a.IfSchemeClosed,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName from Masterscheme a inner join masterproduct b on a.ProductID = b.ProductID where a.Id='{0}'  "; //for update
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByProductID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from Masterscheme where ProductId='{0}'  "; //for update
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }


        public bool AddDetails(string Id, string productid, string startdate, string closingdate, int qty1, int scm1, int qty2, int scm2, int qty3, int scm3, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id,productid ,startdate,closingdate,qty1,scm1, qty2,scm2,qty3,scm3, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string productid, string startdate, string closingdate, int qty1, int scm1, int qty2, int scm2, int qty3, int scm3, string modifyby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, productid, startdate, closingdate, qty1, scm1, qty2, scm2, qty3, scm3, modifyby, modifydate, modifytime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
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

        public bool IsNameUniqueForAdd(string ProdName, int loosepack, string pack, string compcode, string Id)
        {
            bool bRetValue = false;
            string strSql = GetQueryUniqueForAdd(ProdName, loosepack, pack, compcode, Id);
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsNameUniqueForEdit(string ProdName, int loosepack, string pack, string compcode, string Id)
        {
            bool bRetValue = false;
            string strSql = GetQueryUniqueForEdit(ProdName, loosepack, pack, compcode, Id);
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        #region Query Building Functions
       
        private string GetQueryUniqueForAdd(string ProdName, int loosepack, string pack, string compcode, string Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("Select ProductId from Masterscheme where ProductID='{0}'", Id);
            return strSql.ToString();
        }

        private string GetQueryUniqueForEdit(string ProdName, int loosepack, string pack, string compcode, string Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("Select ProductId from Masterscheme where ProductID !='{0}'", Id);
            return strSql.ToString();
        }

        private string GetInsertQuery(string Id, string productid, string startdate, string closingdate, int qty1, int scm1, int qty2, int scm2, int qty3, int scm3, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "Masterscheme";
            objQuery.AddToQuery("Id", Id);
            objQuery.AddToQuery("ProductID",productid );
            objQuery.AddToQuery("StartingDate", startdate);
            objQuery.AddToQuery("ClosingDate",closingdate );
            objQuery.AddToQuery("ProductQuantity1", qty1);
            objQuery.AddToQuery("SchemeQuantity1", scm1);
            objQuery.AddToQuery("ProductQuantity2", qty2);
            objQuery.AddToQuery("SchemeQuantity2", scm2);
            objQuery.AddToQuery("ProductQuantity3", qty3);
            objQuery.AddToQuery("SchemeQuantity3", scm3);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            objQuery.AddToQuery("CreatedUserID", createdby);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string productid, string startdate, string closingdate, int qty1, int scm1, int qty2, int scm2, int qty3, int scm3, string modifyby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "Masterscheme";
            objQuery.AddToQuery("Id", Id, true);
            objQuery.AddToQuery("ProductID", productid);
            objQuery.AddToQuery("StartingDate", startdate);
            objQuery.AddToQuery("ClosingDate", closingdate);
            objQuery.AddToQuery("ProductQuantity1", qty1);
            objQuery.AddToQuery("SchemeQuantity1", scm1);
            objQuery.AddToQuery("ProductQuantity2", qty2);
            objQuery.AddToQuery("SchemeQuantity2", scm2);
            objQuery.AddToQuery("ProductQuantity3", qty3);
            objQuery.AddToQuery("SchemeQuantity3", scm3);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);
            objQuery.AddToQuery("ModifiedUserID", modifyby);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "Masterscheme";
            objQuery.AddToQuery("ID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 

    }
}
