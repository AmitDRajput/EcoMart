using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{
    public  class DBSubstitute
    {
        public DBSubstitute()
        {
        }

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            //string strSql = "Select distinct a.AccName, a.AccountID from Masteraccount  a  INNER Join linkpartyCompany b where a.AccountID = b.AccountID order by a.AccName";
            //dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }



        public DataTable GetOverviewDataByDrugCode(string drugCode)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.ProdName , a.ProductID, a.ProdLoosePack,a.ProdPack,a.ProdCompShortName,a.ProdClosingStock,b.StockID,b.BatchNumber,b.Expiry,b.ClosingStock,b.Margin from  masterproduct a inner join tblstock b on a.productID = b.ProductId  where   a.prodDrugID = '"+ drugCode +"' order by a.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }


        public DataTable GetProduct()
        {
            DataTable dtable = new DataTable();
            //string strSql = "Select AccName, AccountID  from masteraccount where acc_code = 'C' order by AccName ";
            //dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetSubstitute()
        {
            DataTable dtable = new DataTable();
            //string strSql = "Select CompName, CompId  from mastercompany order by CompName";
            //dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByDrugID(string Id)
        {
            DataRow dRow = null;
            //if (Id != "")
            //{
            //    string strSql = "Select AccName, AccountID  from masteraccount where AccountID='{0}' ";
            //    strSql = string.Format(strSql, Id);
            //    dRow = DBInterface.SelectFirstRow(strSql);
            //}
            return dRow;
        }

        public DataTable IsProductAlreadyLinked(string Id)
        {
            DataTable dt = null;
            //if (Id != "")
            //{
            //    string strSql = "Select * from linkpartycompany where AccountId='{0}' ";
            //    strSql = string.Format(strSql, Id);
            //    dt = DBInterface.SelectDataTable(strSql);
            //}
            return dt;
        }
        public DataRow ReadDetailsBySubstitute(string Id)
        {
            DataRow dRow = null;
            //if (Id != "")
            //{
            //    string strSql = "Select CompName, CompId from mastercompany where CompID='{0}' ";
            //    strSql = string.Format(strSql, Id);
            //    dRow = DBInterface.SelectFirstRow(strSql);
            //}
            return dRow;
        }

        public bool AddDetails(string Id, string detailID, string CompanyId, string createdby, string createddate, string createdtime, string modifyby, string modifydate, string modifytime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, detailID, CompanyId,createdby,createddate,createdtime,modifyby,modifydate,modifytime);

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

        public bool IsNameUnique(string CompanyId, string Id)
        {
            string strSql = GetDataForUnique(CompanyId, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }


        #region Query Building Functions
      
        private string GetDataForUnique(string CompanyId, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select CompID from linkpartycompany where AccountID= " + '"' + Id + '"' + "and CompID = " + '"' + CompanyId + '"', Id);
            return sQuery.ToString();
        }
        private string GetInsertQuery(string Id, string detailID, string CompanyId, string createdby, string createddate, string createdtime, string modifyby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "linkpartycompany";
            objQuery.AddToQuery("LinkPartyCompanyID", detailID);
            objQuery.AddToQuery("AccountID", Id);
            objQuery.AddToQuery("CompID", CompanyId);
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
            strSql = "delete from linkpartycompany where accountId = " + "'" + Id + "'";
            return strSql;
        }
        #endregion

    }
}
