using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    public class DBPrescription
    {
        # region Constructor
        public DBPrescription()
        {
        }
        # endregion

        # region Other Private Methods

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select PrescriptionID,PrescriptionName " +
                            "from masterprescription order by PrescriptionName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        public DataTable GetPrescription()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select PrescriptionID,PrescriptionName " +
                            "from masterprescription order by PrescriptionName";


            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetAllPrescriptions()
        {
            string strSql = "SELECT `prescriptionID`,`PrescriptionName`,`CreatedDate`,`CreatedUserID`,`ModifyDate`,`ModifiyUserID` FROM MasterPrescription";
            return DBInterface.SelectDataTable(strSql);
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strsql = "Select PrescriptionID,PrescriptionName " +
                                "from masterprescription  where PrescriptionID= '{0}'";

                strsql = string.Format(strsql, Id);
                dRow = DBInterface.SelectFirstRow(strsql);
            }
            return dRow;
        }

        public bool DeleteProductsByID(string Id)
        {
            bool bRetValue = false;

            if (Id != "")
            {
              
                string strsql = "Delete from linkprescription where PrescriptionID= '{0}'";

                strsql = string.Format(strsql, Id);
                if (DBInterface.ExecuteQuery(strsql) > 0)
                {
                    bRetValue = true;
                }
              
               
            }
            return bRetValue;
           
        }

        public DataTable ReadProdDetailsById(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select distinct a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdCompShortName,a.ProdClosingStock,b.Quantity,b.prescriptionID " +
                                "from linkprescription b ,masterproduct a  where b.ProductID = a.ProductID  and " +
                                  " b.PrescriptionID= '{0}'";
 
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
                
                
            }
            return dt;
        }         

        public bool AddDetails(string Id, string PrescriptionName, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, PrescriptionName,createdby,createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddProductDetails(string Id, string detailID, int ProductID, int Quantity)
        {

            bool bRetValue = false;
            string strSql = GetInsertProductQuery(Id,detailID, ProductID, Quantity);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string Id, string Name, string modifiedby, string modifieddate, string modifiedtime)
             
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, Name,modifiedby,modifieddate,modifiedtime);
               
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQuery(string Id, string Name, string createdby, string createddate, string createdtime)
             
        {
            Query objQuery = new Query();
            objQuery.Table = "masterprescription";
            objQuery.AddToQuery("PrescriptionID", Id);
            objQuery.AddToQuery("PrescriptionName", Name);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
        private string GetInsertProductQuery(string Id, string detailID, int ProductID, int Quantity)
        {
            Query objQuery = new Query();
            objQuery.Table = "linkprescription";
            objQuery.AddToQuery("LinkPrescriptionID", detailID);
            objQuery.AddToQuery("PrescriptionID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("Quantity", Quantity);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string Name,string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterprescription";
            objQuery.AddToQuery("PrescriptionID", Id,true);
            objQuery.AddToQuery("PrescriptionName", Name);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
          
            return objQuery.UpdateQuery();
        }

       
        # endregion


        #region Other Comman Functions
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

        #region Query Building Functions
        public bool IsNameUniqueForAdd(string Name, string Id)
        {
            string strSql = GetDataForUniqueForAdd(Name, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool IsNameUniqueForEdit(string Name, string Id)
        {
            string strSql = GetDataForUniqueForEdit(Name, Id);
            bool bRetValue = false;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }     

       
        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "MasterPrescription";
            objQuery.AddToQuery("PrescriptionID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }

        private string GetDataForUniqueForAdd(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select PrescriptionId from MasterPrescription where PrescriptionName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND PrescriptionID in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        private string GetDataForUniqueForEdit(string Name, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select PrescriptionId from MasterPrescription where PrescriptionName='{0}'", Name);
            if (Id != "")
            {
                sQuery.AppendFormat(" AND PrescriptionID not in ('{0}')", Id);
            }
            return sQuery.ToString();
        }
        #endregion
        #endregion

    }


}
