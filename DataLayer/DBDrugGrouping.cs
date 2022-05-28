using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace EcoMart.DataLayer
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

        public DataTable GetOverviewProductData(Int32 DrugID)  // [25.01.2017]
        {
            DataTable dtable = new DataTable();
//string strSql = "Select distinct  a.ProdName , a.ProductID, a.ProddrugID from  masterproduct a ,linkdruggrouping b   where   b.ProductID = a.ProductID  and b.GenericCategoryID = " + '"' + DrugID + '"' + " order by a.ProdName";
            string strSql = "Select ProdName, ProductID, ProdPack, ProddrugID from  masterproduct  where   ProddrugID = " + '"' + DrugID + '"' + " order by ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewProductDataY(Int32  prodID)
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
            string strSql = "Select ProdName, ProductID,ProdLoosePack,ProdPack,ProdCompShortName  from masterproduct order by ProdName";
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
                string strSql = "Select * from masterproduct where ProductID='{0}' ";
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
                string strSql = "update masterproduct set proddrugID = '' where proddrugid like '" + Id + "%'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            }
            return bRetValue;
        }
        public bool UpdateProductMaster(int  Id, int ProductID)
        {
            bool bRetValue = false;
            if (Id != 0)
            {
                string strSql = "update masterproduct set proddrugID = '" + Id + "' where ProductID = '" + ProductID + "'";
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
            }
            return bRetValue;
           
        }
        public bool AddDetails(int Id,int detailID, int  ProductID, string createdby, string createddate, string createdtime, string modifyby, string modifydate, string modifytime)
        {        
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id,detailID, ProductID ,createdby,createddate,createdtime,modifyby,modifydate,modifytime);

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

        public bool IsNameUnique(int ProductID, string Id)
        {
            string strSql = GetDataForUnique(ProductID, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public DataRow GetMaxID()
        {
            DataRow dRow = null;

            string strSql = "Select max(LinkDrugGroupingID) as maxid from linkdruggrouping ";
            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        #region Query Building Functions

        private string GetDataForUnique(int ProductID, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ProductID from linkdruggrouping where GenericCategoryID= " + '"' + Id +'"' + "and ProductID = "+ '"' +ProductID +'"', Id);
            return sQuery.ToString();
        }
        private string GetInsertQuery(int Id, int detailID, int ProductID,string createdby, string createddate, string createdtime, string modifyby, string modifydate, string modifytime)
       {
            Query objQuery = new Query();
            objQuery.Table = "linkdruggrouping";
            //objQuery.AddToQuery("LinkDrugGroupingID", detailID);
            objQuery.AddToQuery("GenericCategoryID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
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


