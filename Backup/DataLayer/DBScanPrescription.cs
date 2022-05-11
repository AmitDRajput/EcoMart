using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace PharmaSYSRetailPlus.DataLayer
{
     public class DBScanPrescription
    {

         public DBScanPrescription()
        {
        }
         public DataTable GetOverviewData()
         {
             DataTable dtable = new DataTable();
             string strSql = "Select a.ID, a.ProductID,a.ProductQuantity1,a.SchemeQuantity1,a.ProductQuantity2,a.SchemeQuantity2," +
                             "a.ProductQuantity3,a.SchemeQuantity3,a.StartingDate,a.ClosingDate,a.IfSchemeClosed,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName,b.ProdCompID from masterscheme a inner join masterproduct b on a.ProductID = b.ProductID order by b.ProdName";

             dtable = DBInterface.SelectDataTable(strSql);

             return dtable;
         }

       

     

        

         public DataRow ReadDetailsBySaleBillID(string Id)
         {
             DataRow dRow = null;
             if (Id != "")
             {
                 string strSql = "Select * from tblscanprescriptions where SaleBillID = '{0}'" ;
                 strSql = string.Format(strSql, Id);
                 dRow = DBInterface.SelectFirstRow(strSql);
             }
             return dRow;
         }

       


         //public bool AddDetails(string scanPrescriptionId, byte[] scandata, string fileExtention, string saleBillID)
         //{
         //    bool bRetValue = false;
         //    int count = PharmaSYSRetailPlus.DataLayer.DBInterface.AddPrescriptionData(scanPrescriptionId, scandata, fileExtention, saleBillID);

         //    if (count > 0)
         //    {
         //        bRetValue = true;
         //    }
         //    return bRetValue;
         //}

         //public bool UpdateDetails(string scanPrescriptionId, byte[] scandata, string fileExtention, string saleBillID)
         //{
         //    bool bRetValue = false;
         //    int count = PharmaSYSRetailPlus.DataLayer.DBInterface.AddPrescriptionData(scanPrescriptionId, scandata, fileExtention, saleBillID);

         //    if (count > 0)
         //    {
         //        bRetValue = true;
         //    }
         //    return bRetValue;
         //}

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

         
         private string GetDeleteQuery(string Id)
         {
             string strSql = "";
             Query objQuery = new Query();
             objQuery.Table = "tblscanprescriptions";
             objQuery.AddToQuery("ScanPrescriptionID", Id, true);
             strSql = objQuery.DeleteQuery();

             return strSql;
         }
         #endregion 
    }
}
