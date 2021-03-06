using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EcoMart.DataLayer
{
    class DBDetailPatient
    {
        public bool AddDetails(string PatientID, int ProductID, int Quantity, string CreatedDate, string CreatedUserID, string ModifyDate, string ModifyUserID)
        {
            bool ReturnVal = false;
            
            Query objQuery = new Query();
            objQuery.Table = "linkpatientproduct";
            objQuery.AddToQuery("PatientID", PatientID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedUserID", CreatedUserID);
            objQuery.AddToQuery("ModifiedDate", ModifyDate);
            objQuery.AddToQuery("ModifiedUserID", ModifyUserID);

            string strSql = objQuery.InsertQuery();
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                ReturnVal = true;
            }
            return ReturnVal;
        }

        public bool UpdateDetails(string PatientID, int ProductID, int Quantity, string CreatedDate, string CreatedUserID, string ModifyDate, string ModifyUserID)
        {
            bool ReturnVal = false;
            Query objQuery = new Query();
            objQuery.Table = "linkpatientproduct";
            objQuery.AddToQuery("PatientID", PatientID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedUserID", CreatedUserID);
            objQuery.AddToQuery("ModifiedDate", ModifyDate);
            objQuery.AddToQuery("ModifiedUserID", ModifyUserID);

            string strSql = objQuery.UpdateQuery();
            strSql += " PatientID='" + PatientID + "' AND ProductID='" + ProductID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                ReturnVal = true;
            }
            return ReturnVal;
        }

        public bool DeleteDetail(string PatientID, int ProductID)
        {
            bool ReturnVal = false;

            Query objQuery = new Query();
            objQuery.Table = "linkpatientproduct";
            string strSql = objQuery.DeleteQuery();

            strSql += " PatientID='" + PatientID + "' AND ProductID='" + ProductID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                ReturnVal = true;
            }

            return ReturnVal;
        }

        public DataRow ReadDetailById(string PatientID, int ProductID)
        {
            string strSql = "SELECT dp.patientID,dp.ProductID,mp.ProdName,mp.ProdPack,mp.ProdLoosePack,dp.quantity,dp.CreatedDate,dp.CreatedUserID,dp.ModifyDate,dp.ModifyUserID,mp.ProdClosingStock, mp.ProdShelfID,mp.ProdIfSaleDisc" +
                            "FROM linkpatientproduct dp, masterProduct mp" +
                            "WHERE dp.ProductID = mp.ProductID" +
                            " patientID='" + PatientID + "' AND ProductID='" + ProductID + "'";
            return DBInterface.SelectFirstRow(strSql);
        }

        public DataTable ReadDetailsByPatientID(string PatientID)
        {
            string strSql = "SELECT dp.patientID,dp.ProductID,mp.ProdName,mp.ProdPack,mp.ProdLoosePack,dp.quantity,dp.CreatedDate,dp.CreatedUserID,dp.ModifyDate,dp.ModifyUserID,mp.ProdClosingStock, mp.ProdShelfID,mp.ProdIfSaleDisc" +
                            "FROM linkpatientproduct dp, masterProduct mp" +
                            "WHERE dp.ProductID = mp.ProductID AND PatientID='" + PatientID + "'";
            return DBInterface.SelectDataTable(strSql);
        }
    }
}
