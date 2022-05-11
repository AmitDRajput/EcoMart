using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MitraPlus.DataLayer
{
    class DBDetailSale
    {
        #region "Query Builder"
        private string GetInsertQuery(string SaleId, string ProductId, string BatchNumber, double PuchaseRate, double MRP,
            double SellingRate, string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity,
            string AccountID, string PatientID, string CompanyID, string DoctorID, double OctroiPer,
            double OctroiAmount, double CSTPer, double CSTAmount, double VATPer, double VATAmount, string InwardNumber,
            char IPDOPDCode, int IndentNumber, string PatientName, string PatientAddress1, string PatientAddress2, string DoctorNameAddress, 
            double PMTDiscount, double PMTAmount, char IfProductDiscount)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailSale";
            objQuery.AddToQuery("MasterSaleID", SaleId);
            objQuery.AddToQuery("ProductId", ProductId);
            objQuery.AddToQuery("BatchNumber", BatchNumber);
            objQuery.AddToQuery("PuchaseRate", PuchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SellingRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
            objQuery.AddToQuery("AccountID", AccountID);
            objQuery.AddToQuery("PatientID", PatientID);
            objQuery.AddToQuery("CompanyID", CompanyID);
            objQuery.AddToQuery("DoctorID", DoctorID);
            objQuery.AddToQuery("OctroiPer", OctroiPer);
            objQuery.AddToQuery("OctroiAmount", OctroiAmount);
            objQuery.AddToQuery("CSTPer", CSTPer);
            objQuery.AddToQuery("CSTAmount", CSTAmount);
            objQuery.AddToQuery("VATPer", VATPer);
            objQuery.AddToQuery("VATAmount", VATAmount);
            objQuery.AddToQuery("InwardNumber", InwardNumber);
            objQuery.AddToQuery("IPDOPDCode", IPDOPDCode.ToString());
            objQuery.AddToQuery("PatientName", PatientName);
            objQuery.AddToQuery("PatientAddress1", PatientAddress1);
            objQuery.AddToQuery("PatientAddress2", PatientAddress2);
            objQuery.AddToQuery("DoctorNameAddress", DoctorNameAddress);
            objQuery.AddToQuery("PMTDiscount", PMTDiscount);
            objQuery.AddToQuery("PMTAmount", PMTAmount);
            objQuery.AddToQuery("IfProductDiscount", IfProductDiscount.ToString());

            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string SaleId, string ProductId, string BatchNumber, double PuchaseRate, double MRP,
            double SellingRate, string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity,
            string AccountID, string PatientID, string CompanyID, string DoctorID, double OctroiPer,
            double OctroiAmount, double CSTPer, double CSTAmount, double VATPer, double VATAmount, string InwardNumber,
            char IPDOPDCode, int IndentNumber, string PatientName, string PatientAddress1, string PatientAddress2, string DoctorNameAddress,
            double PMTDiscount, double PMTAmount, char IfProductDiscount)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailSale";
            objQuery.AddToQuery("MasterSaleID", SaleId);
            objQuery.AddToQuery("ProductId", ProductId);
            objQuery.AddToQuery("BatchNumber", BatchNumber);
            objQuery.AddToQuery("PuchaseRate", PuchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SellingRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
            objQuery.AddToQuery("AccountID", AccountID);
            objQuery.AddToQuery("PatientID", PatientID);
            objQuery.AddToQuery("CompanyID", CompanyID);
            objQuery.AddToQuery("DoctorID", DoctorID);
            objQuery.AddToQuery("OctroiPer", OctroiPer);
            objQuery.AddToQuery("OctroiAmount", OctroiAmount);
            objQuery.AddToQuery("CSTPer", CSTPer);
            objQuery.AddToQuery("CSTAmount", CSTAmount);
            objQuery.AddToQuery("VATPer", VATPer);
            objQuery.AddToQuery("VATAmount", VATAmount);
            objQuery.AddToQuery("InwardNumber", InwardNumber);
            objQuery.AddToQuery("IPDOPDCode", IPDOPDCode.ToString());
            objQuery.AddToQuery("PatientName", PatientName);
            objQuery.AddToQuery("PatientAddress1", PatientAddress1);
            objQuery.AddToQuery("PatientAddress2", PatientAddress2);
            objQuery.AddToQuery("DoctorNameAddress", DoctorNameAddress);
            objQuery.AddToQuery("PMTDiscount", PMTDiscount);
            objQuery.AddToQuery("PMTAmount", PMTAmount);
            objQuery.AddToQuery("IfProductDiscount", IfProductDiscount.ToString());

            return objQuery.UpdateQuery();
        }
        #endregion

        public bool Save(string MasterSaleId, string ProductId, string BatchNumber, double PuchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, string AccountID, string PatientID, string CompanyID, string DoctorID, double OctroiPer, double OctroiAmount, double CSTPer, double CSTAmount, double VATPer, double VATAmount, string InwardNumber, char IPDOPDCode, int IndentNumber, string PatientName, string PatientAddress1, string PatientAddress2, string DoctorNameAddress, double PMTDiscount, double PMTAmount, char IfProductDiscount)
        {
            string sql = GetInsertQuery(MasterSaleId, ProductId, BatchNumber, PuchaseRate, MRP, SaleRate, Expiry, ExpiryDate,
                            Quantity, SchemeQuantity, AccountID, PatientID, CompanyID, DoctorID, OctroiPer, OctroiAmount, CSTPer, CSTAmount, VATPer,
                            VATAmount, InwardNumber, IPDOPDCode, IndentNumber, PatientName, PatientAddress1,PatientAddress2,
                            DoctorNameAddress, PMTDiscount, PMTAmount, IfProductDiscount);
            if (DBInterface.ExecuteQuery(sql) > 0)
            {
                return true;
            }
            return false;
        }

        public bool Update(string MasterSaleId, string ProductId, string BatchNumber, double PuchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, string AccountID, string PatientID, string CompanyID, string DoctorID, double OctroiPer, double OctroiAmount, double CSTPer, double CSTAmount, double VATPer, double VATAmount, string InwardNumber, char IPDOPDCode, int IndentNumber, string PatientName, string PatientAddress1, string PatientAddress2, string DoctorNameAddress, double PMTDiscount, double PMTAmount, char IfProductDiscount)
        {
            string sql = GetUpdateQuery(MasterSaleId, ProductId, BatchNumber, PuchaseRate, MRP, SaleRate, Expiry, ExpiryDate,
                            Quantity, SchemeQuantity, AccountID, PatientID, CompanyID, DoctorID, OctroiPer, OctroiAmount, CSTPer, CSTAmount, VATPer,
                            VATAmount, InwardNumber, IPDOPDCode, IndentNumber, PatientName, PatientAddress1,PatientAddress2,
                            DoctorNameAddress, PMTDiscount, PMTAmount, IfProductDiscount);
            sql += " MasterSaleId = '" + MasterSaleId + "' AND ProductId='" + ProductId + "'";
            if (DBInterface.ExecuteQuery(sql) > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable ReadDetailById(string Id)
        {
            string strSql = string.Format("SELECT '1' AS CustID, mp.ProdName, mp.ProdLoosePack AS 'UOM', mp.ProdPack, ds.BatchNumber, STR_TO_DATE(ds.ExpiryDate,'%Y%m%d') AS 'ExpiryDate', ds.MRP, mp.ProdShelfID AS 'ShelfNo', ds.Quantity, ROUND(((ds.MRP / mp.ProdLoosePack) * ds.Quantity),2) AS Amount, mp.ProductID, mp.ProdIfSaleDisc FROM MasterProduct mp,DetailSale ds WHERE mp.ProductID = ds.ProductID AND MasterSaleID = '{0}'", Id);
            return DBInterface.SelectDataTable(strSql);
        }

        public bool DeleteAll(string Id)
        {
            string strSql = "DELETE FROM DetailSale WHERE MasterSaleId = '" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteById(string SaleId, string ProductId)
        {
            string strSql = "DELETE FROM DetailSale WHERE MasterSaleId = '" + SaleId + "' AND ProductId='" + ProductId + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
