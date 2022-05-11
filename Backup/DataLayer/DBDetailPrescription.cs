using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
{
    class DBDetailPrescription
    {
        public bool AddDetail(string PrescriptionID, string ProductID, int Quantity, string CreatedDate, string CreatedUserID, string ModifyDate, string ModifyUserID)
        {
            Query objQuery = new Query();
            objQuery.Table = "linkprescription";
            objQuery.AddToQuery("PrescriptionID", PrescriptionID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedUserID", CreatedUserID);
            objQuery.AddToQuery("ModifiedDate", ModifyDate);
            objQuery.AddToQuery("ModifiedUserID", ModifyUserID);

            string strSql = objQuery.InsertQuery();
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                return true;
            }

            return false;
        }

        public bool UpdateDetail(string PrescriptionID, string ProductID, int Quantity, string CreatedDate, string CreatedUserID, string ModifyDate, string ModifyUserID)
        {
            Query objQuery = new Query();
            objQuery.Table = "linkprescription";
            objQuery.AddToQuery("PrescriptionID", PrescriptionID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedUserID", CreatedUserID);
            objQuery.AddToQuery("ModifiedDate", ModifyDate);
            objQuery.AddToQuery("ModifiedUserID", ModifyUserID);

            string strSql = objQuery.UpdateQuery();
            strSql += " PrescriptionID='" + PrescriptionID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteDetail(string PrescriptionID)
        {
            Query objQuery = new Query();
            objQuery.Table = "linkprescription";
            string strSql = objQuery.DeleteQuery();

            strSql += " PrescriptionID='" + PrescriptionID + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                return true;
            }

            return false;
        }

        public DataTable GetAllPrescriptions()
        {
            string strSql = "SELECT `PrescriptionID`,`ProductID`,`Quantity`,`CreatedDate`,`CreatedUserID`,`ModifyDate`,`ModifyUserID`, mp.ProdName,mp.ProdPack,mp.ProdLoosePack,mp.ProdClosingStock, mp.ProdShelfID,mp.ProdIfSaleDisc" +
                            "FROm linkPrescription dp, masterProduct mp" +
                            "WHERE dp.ProductID = mp.ProductID";
            return DBInterface.SelectDataTable(strSql);
        }

        public DataTable GetAllPrescriptionsByID(string PrescriptionID)
        {
            string strSql = "SELECT dp.PrescriptionID,dp.ProductID,dp.Quantity,dp.CreatedDate,dp.CreatedUserID,dp.ModifyDate,dp.ModifyUserID,mp.ProdName,mp.ProdPack,mp.ProdLoosePack,mp.ProdClosingStock,mp.ProdShelfID,mp.ProdIfSaleDisc" +
                            " FROm linkPrescription dp, masterProduct mp" +
                            " WHERE dp.ProductID = mp.ProductID AND PrescriptionID='" + PrescriptionID + "'";
            return DBInterface.SelectDataTable(strSql);
        }
    }
}
