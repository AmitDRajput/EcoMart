using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBDrugGrouping
    {
        public DBDrugGrouping()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.GenericCategoryName, a.GenericCategoryID from MastergenericCategory  a  INNER Join linkdruggrouping b where a.GenericCategoryID = b.GenericCategoryID order by a.GenericCategoryName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataY()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.ProdName, a.ProductID from Masterproduct  a  INNER Join linkdruggrouping b where a.ProductID = b.ProductID order by a.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewProductData(string DrugID)
        {
            DataTable dtable = new DataTable();
//string strSql = "Select distinct  a.ProdName , a.ProductID, a.ProddrugID from  masterproduct a ,linkdruggrouping b   where   b.ProductID = a.ProductID  and b.GenericCategoryID = " + '"' + DrugID + '"' + " order by a.ProdName";
            string strSql = "Select ProdName , ProductID, ProddrugID from  masterproduct  where   ProddrugID = " + '"' + DrugID + '"' + " order by ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewProductDataY(string prodID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.GenericCategoryName , b.ProductID, b.GenericCategoryID,b.CreatedDate,b.CreatedTime,b.CreatedUserID from  mastergenericcategory a ,linkdruggrouping b   where   b.GenericCategoryID = a.GenericCategoryID  and b.ProductID = " + '"' + prodID + '"' + " order by a.GenericCategoryName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDrugData(string ProdID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.GenericCategoryName , b.ProductID, b.GenericCategoryID,b.CreatedDate,b.CreatedTime,b.CreatedUserID from  mastergenericcategory a ,linkdruggrouping b   where   b.GenericCategoryID = a.GenericCategoryID  and b.ProductID = " + '"' + ProdID + '"' + " order by a.GenericCategoryName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    
        public DataTable GetDrug()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select GenericCategoryName, GenericCategoryId  from mastergenericcategory order by GenericCategoryName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetProduct()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ProdName, ProductId,ProdLoosePack,ProdPack,ProdCompShortName  from masterproduct order by ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
      

        public DataRow ReadDetailsByIDDrug(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from mastergenericcategory where GenericCategoryId='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataTable IsDrugAlreadyLinked(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strSql = "Select * from linkdruggrouping where GenericcategoryId='{0}' ";
                strSql = string.Format(strSql, Id);
                dt = DBInterface.SelectDataTable(strSql);
            }
            return dt;
        }

        public DataRow ReadDetailsByIDProduct(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from masterproduct where ProductId='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public bool ClearDrugIDInProductMaster(string Id)
        {
            bool bRetValue = false;
            if (Id != "")
            {
                string strSql = "update masterproduct set proddrugID = '' where proddrugid = '" + Id + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            }
            return bRetValue;
        }
        public bool UpdateProductMaster(string Id,string productID)
        {
            bool bRetValue = false;
            if (Id != "")
            {
                string strSql = "update masterproduct set proddrugID = '" + Id + "' where productid = '" + productID + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            }
            return bRetValue;
           
        }
        public bool AddDetails(string Id,string detailID,  string ProductId, string createdby, string createddate, string createdtime, string modifyby, string modifydate, string modifytime)
        {        
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id,detailID, ProductId ,createdby,createddate,createdtime,modifyby,modifydate,modifytime);

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

        public bool IsNameUnique(string ProductId, string Id)
        {
            string strSql = GetDataForUnique(ProductId, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }


        #region Query Building Functions
     
        private string GetDataForUnique(string ProductId, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ProductId from linkdruggrouping where GenericCategoryID= " + '"' + Id +'"' + "and ProductID = "+ '"' +ProductId +'"', Id);
            return sQuery.ToString();
        }
        private string GetInsertQuery(string Id, string detailID, string ProductId,string createdby, string createddate, string createdtime, string modifyby, string modifydate, string modifytime)
       {
            Query objQuery = new Query();
            objQuery.Table = "linkdruggrouping";
            objQuery.AddToQuery("LinkDrugGroupingID", detailID);
            objQuery.AddToQuery("GenericCategoryID", Id);
            objQuery.AddToQuery("ProductID", ProductId);
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
            strSql = "delete from linkdruggrouping where GenericCategoryID = " + "'" + Id + "'";
            return strSql;
        }
       
        #endregion 

    
      
    
      
    }

}


