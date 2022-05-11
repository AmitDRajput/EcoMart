using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBProduct
    {
        # region Contructor
        public DBProduct()
        {
        }
        #endregion

        #region Get
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                            "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId order by a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        public DataTable GetOverviewDataForCache()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdClosingStock as ProdClosingStockDatabase, a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                            "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId order by a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        public DataTable GetOverviewDataForScheduleH1()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdClosingStock as ProdClosingStockDatabase, a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                            "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId where a.ProdScheduleDrugCode = 'H1' order by a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public bool GetVatPercent(double vatper)
        {
            bool retValue = false;
            DataRow dr;
            string strsql = "Select * from mastervatpercent where vatpercent = " + vatper;
            dr = DBInterface.SelectFirstRow(strsql);
            if (dr == null)
                retValue = false;
            else
                retValue = true;
            return retValue;
        }

        public DataRow GetProductName(string productID)
        {
            DataRow dr = null;
            string strsql = "select prodName from masterproduct where productid = '" + productID + "'";
            dr = DBInterface.SelectFirstRow(strsql);
            return dr;
        }
        public DataRow GetOverviewDataForProductIDForCache(string productID)
        {
            DataRow drow = null;
            string strsql = string.Format("Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock, a.ProdClosingStock as ProdClosingStockDatabase ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                            "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId where a.ProductID='{0}'", productID );

            drow = DBInterface.SelectFirstRow(strsql);
            return drow;
        }
 
        public DataTable GetOverviewDataForClosingStockNotZero()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdMaxLevel,a.ProdLastSaleStockID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId where a.ProdClosingStock > 0 order by a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetOverviewDataForClosingStockNotZero(string ProductID)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdMaxLevel,a.ProdLastSaleStockID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId where a.ProdClosingStock > 0 && a.ProductID = '" + ProductID + "' order by a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetOverviewDataForList()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select ProductID,ProdName,ProdPack,ProdLoosePack," +
                            "ProdCompShortName from masterproduct order by ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetOverviewDataWithOutZeroAllProducts()
        {
            DataTable dtable = new DataTable();           
            string strSql = "Select a.ProductID,b.ProductID,b.ProdName,b.ProdLoosePack,"+
            "b.ProdPack,b.ProdCompShortName,b.ProdBoxQuantity,b.ProdVATPercent,b.ProdCST,b.ProdGrade,b.ProdCompID,b.ProdLastPurchaseMRP," +
            "b.ProdShelfID,b.ProdScheduleDrugCode,b.ProdIfSchedule,b.ProdClosingStock ,b.ProdOpeningStock,b.ProdIfSaleDisc,"+
            "b.ProdLastPurchaseRate,b.ProdIfShortListed,b.ProdMaxLevel,b.ProdRequireColdStorage,sum(a.ClosingStock* a.mrp / b.ProdLoosePack) as CLValueByMRP, sum(a.ClosingStock* a.PurchaseRate / b.ProdLoosePack) as CLValueByPurchaseRate,sum(a.OpeningStock* a.mrp / b.ProdLoosePack) as OPValueByMRP, sum(a.OpeningStock* a.PurchaseRate / b.ProdLoosePack) as OPValueByPurchaseRate,c.CompID,c.CompName from tblstock a inner join masterproduct b on a.ProductID = b.ProductId  inner join  mastercompany c on b.ProdCompID = c.CompID   Group by a.ProductID order by b.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetForClosingStockNotZero()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select ProductID,ProdName,ProdPack,ProdLoosePack," +
                            "ProdCompShortName,ProdClosingStock from masterproduct where ProdClosingStock > 0  order by ProdName ";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }       
        public DataTable GetOverviewDataForCompany(string compcd)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdMaxLevel,a.ProdRequireColdStorage,b.ShelfCode  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID   where a.ProdCompID = '" + compcd + "' order by  a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetOverviewDataForShelf(string shelfID)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdMaxLevel,a.ProdRequireColdStorage,c.CompID,c.CompName,b.ShelfCode  from masterproduct a  left outer join mastercompany c on a.ProdCompId = c.CompID left outer join mastershelf b on a.ProdShelfID = b.ShelfID  where a.ProdShelfID = '" + shelfID + "' order by c.CompName , a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATPercent(double vatper)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdMaxLevel,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID    left outer join mastercompany c on a.ProdCompId = c.CompID  where a.ProdVATPercent = " + vatper + " order by c.CompName , a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }      
       
        public int GetClosingStock(string Id)
        {
            DataRow drow = null;
            int clstk = 0;

            string strSql = "Select ProdClosingStock from masterproduct where ProductID = " + "'" + Id + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["ProdClosingStock"] != DBNull.Value)
                    clstk = Convert.ToInt32(drow["ProdClosingStock"]);
            }
            return clstk;
        }

        public int GetOpeningStock(string Id)
        {
            DataRow drow = null;
            int opstk = 0;

            string strSql = "Select ProdOpeningStock from masterproduct where ProductID = " + "'" + Id + "'";
            drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
            {
                if (drow["ProdOpeningStock"] != DBNull.Value)
                    opstk = Convert.ToInt32(drow["ProdOpeningStock"]);
            }
            return opstk;
        }

        #endregion

        #region Read
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strsql = "Select a.ProductID,a.ProdName,a.ProdLoosePack,a.ProdPack,a.ProdPackType," +
                            "a.ProdCompShortName,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdDrugID,a.ProdIfOctroi,a.ProdIfSchedule," +
                            "a.ProdCategoryID,a.ProdPartyId_1,a.ProdPartyId_2,a.ProdMinLevel,a.ProdMaxLevel,a.ProdBoxQuantity," +
                            "a.ProdIfMRPInclusive,a.ProdIfSaleDisc,a.ProdIfPurchaseRateInclusive,a.ProdIfShortListed,a.ProdRequireColdStorage,b.ShelfID,b.ShelfCode," +
                             "a.ProdScheduleDrugCode  from masterproduct a left outer join mastershelf b on a.ProdShelfID = b.ShelfID where ProductID= '{0}'";

                strsql = string.Format(strsql, Id);
                dRow = DBInterface.SelectFirstRow(strsql);
            }
            return dRow;
        }
        public DataRow ReadLastSaleByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strsql = "select * from masterproduct A left outer join mastershelf B  on A.ProdShelfID = B.ShelfID   where A.ProductID= '{0}' ";
                strsql = string.Format(strsql, Id);
                dRow = DBInterface.SelectFirstRow(strsql);
            }
            return dRow;
        }
        public DataRow ReadLastPurchaseByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strsql = "select * from masterproduct A left outer join mastershelf B  on A.ProdShelfID = B.ShelfID   where A.ProductID= '{0}' ";
                strsql = string.Format(strsql, Id);
                dRow = DBInterface.SelectFirstRow(strsql);
            }
            return dRow;
        }

        public DataRow GetDetailsForProduct(string prodID)
        {
             DataRow dRow = null;
             if (prodID != "")
            {
                string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack," +
                           "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                           "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdIfOctroi,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                           "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId where a.ProductID = '" + prodID + "'";
            dRow = DBInterface.SelectFirstRow(strsql);
            }
            return dRow;
        }
        public DataTable ReadPatientProdDetailsByID(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select distinct a.ProductId,a.ProdName,a.ProdLoosePack,a.Prodpack , b.PatientId,b.ProductId,b.Quantity from masterproduct a,linkpatientproduct b where a.ProductId = b.ProductId and  b.patientId = '" + Id + "' order by a.prodname";
                dt = DBInterface.SelectDataTable(strsql);

            }
            return dt;

        }
        #endregion

        #region Update
        public bool UpdateClosingStock(string Id)
        {
            bool returnVal = false;
            string strSql = "UPDATE masterProduct SET masterProduct.ProdClosingStock = (SELECT SUM(ClosingStock) FROM tblStock WHERE ProductID = '{0}') WHERE masterproduct.ProductID = '{0}'";
            strSql = string.Format(strSql, Id);

            try
            {
                if( DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool UpdateCreditNoteStockInmasterProduct(string Id, int Quantity)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock = " + Quantity + " where ProductID = " + "'" + Id + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool UpdateCreditNoteStockInmasterProductForNULLClosingStock(string Id, int Quantity)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock =  " + Quantity + " where ProductID = " + "'" + Id + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool UpdateCreditNoteStockInmasterProductReduceFromTemp(string Id, int Quantity)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock = "+ Quantity + " where ProductID = " + "'" + Id + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool UpdateDebitNoteStockInmasterProduct(string Id, int Quantity)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock =  "+ Quantity + " where ProductID = '" +  Id + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool UpdateDebitNoteStockInmasterProductAddFromTemp(string Id, int Quantity)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock = " + Quantity + " where ProductID = " + "'" + Id + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool UpdateSaleStockInmasterProduct(string Id,  int Quantity,string stockid, string voutype, int vouno, string voudate, string accountid, string scancode)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock = " + Quantity + ", ProdLastSaleStockID = '" + stockid + "', ProdLastSaleBillType = '" + voutype + "', ProdLastSaleBillNumber = " + vouno + ", ProdLastSaleDate = '" + voudate + "',ProdLastSalePartyId = '"+ accountid+"', ProdLastSaleScanID = '"+ scancode+"'  where ProductID = '" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;    
            }
         
            return returnVal;
        }
        public bool UpdateDebtorSaleStockInmasterProductAddFromTemp(string Id, int Quantity)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock = " + Quantity + " where ProductID = " + "'" + Id + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool UpdateStockOutInmasterProduct(string ProdId, int Quantity)
        {
            bool returnVal = false;
            string strSql = "";
            int closingstk = 0;
            DataRow dRow = null;
            if (ProdId != "")
            {
                strSql = "select ProdClosingStock from masterproduct where ProductID = '" + ProdId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
                if (dRow["prodclosingstock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["prodclosingstock"].ToString());
            closingstk -= Quantity;
            strSql = "Update masterProduct SET ProdClosingStock = " + closingstk + "  where ProductID = '" + ProdId + "'";
          //  string strSql = "Update masterProduct SET ProdClosingStock = " + Quantity + " where ProductID = " + "'" + Id + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool UpdateStockOutInmasterProductAddFromTemp(string ProdId, int Quantity)
        {
            bool returnVal = false;
            string strSql = "";
            int closingstk = 0;
            DataRow dRow = null;
            if (ProdId != "")
            {
                strSql = "select ProdClosingStock from masterproduct where ProductID = '" + ProdId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
                if (dRow["prodclosingstock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["prodclosingstock"].ToString());
            closingstk += Quantity;
            strSql = "Update masterProduct SET ProdClosingStock = " + closingstk + "  where ProductID = '" + ProdId + "'";
         //   string strSql = "Update masterProduct SET ProdClosingStock = ProdClosingStock + " + Quantity + " where ProductID = " + "'" + Id + "'";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }

        public bool UpdatePurchaseStockInmasterProduct(string ProdId, int Quantity,double margin, double distSaleRatePer)
        {
            bool returnVal = false;                       
            string strSql = "";
            int closingstk = 0;
            DataRow dRow = null;
            if (ProdId != "")
            {
                strSql = "select ProdClosingStock from masterproduct where ProductID = '" + ProdId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
                if (dRow["prodclosingstock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["prodclosingstock"].ToString());
            closingstk += Quantity;
            strSql = "Update masterProduct SET ProdClosingStock = " + closingstk + " , prodmargin = " + margin + " , ProdLastPurchaseDistributorSaleRatePer = "+ distSaleRatePer +" where ProductID = '" + ProdId + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }

        public bool UpdateOpeningStockInmasterProduct(string Id, string Batchno, double mrp, int closingStock, int openingStock)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock = " + closingStock + ", ProdOpeningStock = " + openingStock + " where ProductID = " + "'" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }

        public bool UpdatePurchaseDataInmasterProduct(string ProductID, string PurchaseBillNumber, string VoucherDate, string AccountID, string VoucherType,
                int VoucherNumber, double PurchaseRate, double TradeRate, double SaleRate, double MRP, double PurchaseVATPercent, double CSTPercent, double AmountCST, double SchemeDiscountPercent,
                double AmountSchemeDiscount, double ItemDiscountPercent, string Expiry, string ExpiryDate, string Batchno,string shelfID, string stockid , double distSaleRatePer)
        {
            bool returnVal = false;
            string strSql = GetUpdateQueryforlastpurchase(ProductID, PurchaseBillNumber, VoucherDate, AccountID, VoucherType,
                   VoucherNumber, PurchaseRate, TradeRate, SaleRate, MRP, PurchaseVATPercent, CSTPercent, AmountCST, SchemeDiscountPercent,
                   AmountSchemeDiscount, ItemDiscountPercent, Expiry, ExpiryDate, Batchno,shelfID, stockid,distSaleRatePer);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }
            return returnVal;

        }

        public bool UpdatePurchaseStockInmasterProductReduceFromTemp(string ProdId, int Quantity)
        {
            bool returnVal = false;
            string strSql = "";
            int closingstk = 0;
            DataRow dRow = null;
            if (ProdId != "")
            {
                strSql = "select ProdClosingStock from masterproduct where ProductID = '" + ProdId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            if (dRow != null)
                if (dRow["prodclosingstock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["prodclosingstock"].ToString());
            closingstk -= Quantity;
            strSql = "Update masterProduct SET ProdClosingStock = " + closingstk + " where ProductID = '" + ProdId + "'";
            
          //  string strSql = "Update masterProduct SET ProdClosingStock = ProdClosingStock - " + Quantity + " where ProductID = " + "'" + Id + "'; ";
            try
            {
                DBInterface.ExecuteQuery(strSql);
                returnVal = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        #endregion

        #region For Product

        public bool AddDetails(string Id, string ProdName,
           int ProdLoosePack, string ProdPack, string ProdPackType, string ProdCompShortName,
            string ProdCompID, double ProdVATPercent, double ProdCST, string ProdGrade,
            int ProdMinLevel, int ProdMaxLevel, int ProdBoxQty,
            string ProdGenericID, string ProdShelfID, string ProdScheduleDrugCode, string ProdCategoryID,
            string ProdPartyId_1, string ProdPartyId_2,
            string ProdIfShortListed, string ProdIfSaleDisc, string ProdIfOctroi,
           string ProdIfMRPInclusive, string ProdIfPurchaseRateInclusive, int prodopeningstock,int prodclosingstock,string prodifscheduleddrug,string prodrequirecoldstorage, string prodIfBarCodeRequired, string createdby, string createddate,string createdtime)
        {
            bool returnVal = false;
            string strSql = GetInsertQuery(Id, ProdName,
           ProdLoosePack, ProdPack,ProdPackType, ProdCompShortName,
             ProdCompID, ProdVATPercent, ProdCST, ProdGrade,
             ProdMinLevel, ProdMaxLevel, ProdBoxQty,
             ProdGenericID, ProdShelfID, ProdScheduleDrugCode,
              ProdCategoryID, ProdPartyId_1, ProdPartyId_2,
              ProdIfShortListed,
            ProdIfOctroi, ProdIfMRPInclusive, ProdIfSaleDisc, ProdIfPurchaseRateInclusive, prodopeningstock,prodclosingstock,prodifscheduleddrug,prodrequirecoldstorage,prodIfBarCodeRequired, createdby, createddate,createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }
           
            return returnVal;
        }
        public bool AddPack(string Id, string Pack)
        {
            bool returnVal = false;
            string strSql = GetInsertQueryPack(Id, Pack);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            return returnVal;
        }
        public bool AddPackType(string Id, string Packtype)
        {
            bool returnVal = false;
            string strSql = GetInsertQueryPackType(Id, Packtype);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            return returnVal;
        }
        public bool SaveProductDrugLink(string linkID, string productID, string drugID, string createdBy, string createdDate, string createdTime)
        {
            bool returnVal = false;
            string strSql = GetInsertQueryAddInDrugGrouping(linkID, productID, drugID, createdBy, createdDate, createdTime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }
            return returnVal;            
        }
        public bool UpdateDetails(string Id, string ProdName,
           int ProdLoosePack, string ProdPack, string ProdPackType, string ProdCompShortName,
            string ProdCompID, double ProdVATPercent, double ProdCST, string ProdGrade,
            int ProdMinLevel, int ProdMaxLevel, int ProdBoxQty,
            string ProdGenericID, string ProdShelfID, string ProdScheduleDrugCode, string ProdCategoryID,
            string ProdPartyId_1, string ProdPartyId_2,
            string ProdIfShortListed, string ProdIfSaleDisc, string ProdIfOctroi,
           string ProdIfMRPInclusive, string ProdIfPurchaseRateInclusive, string prodifscheduleddrug,string prodrequirecoldstorage, string prodIfBarCodeRequired, string modifiedby, string modifydate,string modifytime)
        {
            bool returnVal = false;
            string strSql = GetUpdateQuery(Id, ProdName,
            ProdLoosePack, ProdPack,ProdPackType, ProdCompShortName,
             ProdCompID, ProdVATPercent, ProdCST, ProdGrade, ProdMinLevel, ProdMaxLevel, ProdBoxQty,
             ProdGenericID, ProdShelfID, ProdCategoryID, ProdPartyId_1, ProdPartyId_2,
             ProdScheduleDrugCode, ProdIfShortListed,
             ProdIfOctroi, ProdIfMRPInclusive, ProdIfSaleDisc, ProdIfPurchaseRateInclusive,prodifscheduleddrug,prodrequirecoldstorage, prodIfBarCodeRequired, modifiedby, modifydate,modifytime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }
            return returnVal;
        }

        private string GetInsertQuery(string Id, string ProdName,
            int ProdLoosePack, string ProdPack, string ProdPackType, string ProdCompShortName,
            string ProdCompID, double ProdVATPercent, double ProdCST, string ProdGrade,
            int ProdMinLevel, int ProdMaxLevel, int ProdBoxQty,
           string ProdGenericID, string ProdShelfID, string ProdScheduleDrugCode,
            string ProdCategoryID, string ProdPartyId_1, string ProdPartyId_2,
            string ProdIfShortListed, string ProdIfOctroi,
           string ProdIfMRPInclusive, string ProdIfSaleDisc, string ProdIfPurchaseRateInclusive, int prodopeningstock, int prodclosingstock,string prodifscheduleddrug,string prodrequirecoldstorage, string prodIfBarCodeRequired, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterproduct";
            objQuery.AddToQuery("ProductID", Id);
            objQuery.AddToQuery("ProdName", ProdName);
            objQuery.AddToQuery("ProdLoosePack", ProdLoosePack);
            objQuery.AddToQuery("ProdPack", ProdPack);
            objQuery.AddToQuery("ProdPackType", ProdPackType);
            objQuery.AddToQuery("ProdCompShortName", ProdCompShortName);
            objQuery.AddToQuery("ProdCompID", ProdCompID);
            objQuery.AddToQuery("ProdVATPercent", ProdVATPercent);
            objQuery.AddToQuery("ProdCST", ProdCST);
            objQuery.AddToQuery("ProdCSTPercent", 0);
            objQuery.AddToQuery("ProdGrade", ProdGrade);
            objQuery.AddToQuery("ProdMinLevel", ProdMinLevel);
            objQuery.AddToQuery("ProdMaxLevel", ProdMaxLevel);
            objQuery.AddToQuery("ProdBoxQuantity", ProdBoxQty);
            objQuery.AddToQuery("ProdScheduleDrugCode", ProdScheduleDrugCode);
            objQuery.AddToQuery("ProdShelfID", ProdShelfID);
            objQuery.AddToQuery("ProdDrugID", ProdGenericID);
            objQuery.AddToQuery("ProdCategoryID", ProdCategoryID);
            objQuery.AddToQuery("ProdPartyId_1", ProdPartyId_1);
            objQuery.AddToQuery("ProdPartyId_2", ProdPartyId_2);
            objQuery.AddToQuery("ProdIfShortListed", ProdIfShortListed);
            objQuery.AddToQuery("ProdIfOctroi", ProdIfOctroi);
            objQuery.AddToQuery("ProdIfMRPInclusive", ProdIfMRPInclusive);
            objQuery.AddToQuery("ProdIfSaleDisc", ProdIfSaleDisc);
            objQuery.AddToQuery("ProdIfPurchaseRateInclusive", ProdIfPurchaseRateInclusive);         
            objQuery.AddToQuery("ProdOpeningStock",prodopeningstock);
            objQuery.AddToQuery("ProdClosingStock", prodclosingstock);
            objQuery.AddToQuery("ProdIfSchedule", prodifscheduleddrug);
            objQuery.AddToQuery("ProdRequireColdStorage", prodrequirecoldstorage);
            objQuery.AddToQuery("ProdIfBarCodeRequired", prodIfBarCodeRequired);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryPack(string Id, string Pack)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterpack";
            objQuery.AddToQuery("PackID", Id);
            objQuery.AddToQuery("PackName", Pack);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryPackType(string Id, string Packtype)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterpacktype";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("PackTypeName", Packtype);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryAddInDrugGrouping(string linkID, string productID, string drugID, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "linkdruggrouping";
            objQuery.AddToQuery("LinkDrugGroupingID", linkID);
            objQuery.AddToQuery("GenericCategoryID", drugID);
            objQuery.AddToQuery("ProductID", productID);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
        private string GetUpdateQuery(string Id, string ProdName,
           int ProdLoosePack, string ProdPack, string ProdPackType,  string ProdCompShortName,
            string ProdCompID, double ProdVATPercent, double ProdCST, string ProdGrade,
            int ProdMinLevel, int ProdMaxLevel, int ProdBoxQty,
           string ProdGenericID, string ProdShelfID, string ProdCategoryID, string ProdPartyId_1, string ProdPartyId_2,
            string ProdScheduleDrugCode,
             string ProdIfShortListed, string ProdIfOctroi,
           string ProdIfMRPInclusive, string ProdIfSaleDisc, string ProdIfPurchaseRateInclusive, string prodifscheduleddrug,string prodrequirecoldstorage, string prodIfBarCodeRequired, string modifiedby, string modifydate,string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterproduct";
            objQuery.AddToQuery("ProductID", Id, true);
            objQuery.AddToQuery("ProdName", ProdName);
            objQuery.AddToQuery("ProdLoosePack", ProdLoosePack);
            objQuery.AddToQuery("ProdPack", ProdPack);
            objQuery.AddToQuery("ProdPackType", ProdPackType);
            objQuery.AddToQuery("ProdCompShortName", ProdCompShortName);
            objQuery.AddToQuery("ProdCompID", ProdCompID);
            objQuery.AddToQuery("ProdVATPercent", ProdVATPercent);
            objQuery.AddToQuery("ProdCST", ProdCST);
            objQuery.AddToQuery("ProdGrade", ProdGrade);
            objQuery.AddToQuery("ProdMinLevel", ProdMinLevel);
            objQuery.AddToQuery("ProdMaxLevel", ProdMaxLevel);
            objQuery.AddToQuery("ProdBoxQuantity", ProdBoxQty);
            objQuery.AddToQuery("ProdScheduleDrugCode", ProdScheduleDrugCode);
            objQuery.AddToQuery("ProdShelfID", ProdShelfID);
            objQuery.AddToQuery("ProdDrugID", ProdGenericID);
            objQuery.AddToQuery("ProdCategoryID", ProdCategoryID);
            objQuery.AddToQuery("ProdPartyId_1", ProdPartyId_1);
            objQuery.AddToQuery("ProdPartyId_2", ProdPartyId_2);
            objQuery.AddToQuery("ProdIfShortListed", ProdIfShortListed);
            objQuery.AddToQuery("ProdIfOctroi", ProdIfOctroi);
            objQuery.AddToQuery("ProdIfMRPInclusive", ProdIfMRPInclusive);
            objQuery.AddToQuery("ProdIfSaleDisc", ProdIfSaleDisc);
            objQuery.AddToQuery("ProdIfPurchaseRateInclusive", ProdIfPurchaseRateInclusive);         
            objQuery.AddToQuery("ProdIfSchedule", prodifscheduleddrug);
            objQuery.AddToQuery("ProdRequireColdStorage", prodrequirecoldstorage);
            objQuery.AddToQuery("ProdIfBarCodeRequired", prodIfBarCodeRequired);
            objQuery.AddToQuery("ModifiedUserID",modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);

            return objQuery.UpdateQuery();
        }

        private string GetUpdateQueryforlastpurchase(string ProductID, string PurchaseBillNumber, string VoucherDate, string AccountID, string VoucherType,
                int VoucherNumber, double PurchaseRate, double TradeRate, double SaleRate, double MRP, double PurchaseVATPercent,
               double CSTPercent, double AmountCST, double SchemeDiscountPercent,
                double AmountSchemeDiscount, double ItemDiscountPercent, string Expiry, string ExpiryDate, string Batchno,string shelfID, string stockid, double distSaleRatePer )
        {
            Query objQuery = new Query();
            objQuery.Table = "masterproduct";
            objQuery.AddToQuery("ProductID", ProductID, true);
            objQuery.AddToQuery("ProdLastPurchaseBillNumber", PurchaseBillNumber);
            objQuery.AddToQuery("ProdLastPurchaseDate", VoucherDate);
            objQuery.AddToQuery("ProdLastPurchasePartyId", AccountID);
            objQuery.AddToQuery("ProdLastPurchaseVoucherType", VoucherType);
            objQuery.AddToQuery("ProdLastPurchaseVoucherNumber", VoucherNumber);
            objQuery.AddToQuery("ProdLastPurchaseRate", PurchaseRate);
            objQuery.AddToQuery("ProdLastPurchaseTradeRate", TradeRate);
            objQuery.AddToQuery("ProdLastPurchaseSaleRate", SaleRate);
            objQuery.AddToQuery("ProdLastPurchaseMRP", MRP);
            objQuery.AddToQuery("ProdLastPurchaseVATPer", PurchaseVATPercent);
            objQuery.AddToQuery("ProdLastPurchaseCSTPer", CSTPercent);
            objQuery.AddToQuery("ProdLastPurchaseCST", AmountCST);
            objQuery.AddToQuery("ProdLastPurchaseSCMPer", SchemeDiscountPercent);
            objQuery.AddToQuery("ProdLastPurchaseSCM", AmountSchemeDiscount);
            objQuery.AddToQuery("ProdLastPurchaseItemDiscPer", ItemDiscountPercent);
            objQuery.AddToQuery("ProdLastPurchaseExpiry", Expiry);
            objQuery.AddToQuery("ProdLastPurchaseExpiryDate", ExpiryDate);
            objQuery.AddToQuery("ProdLastPurchaseBatchNumber", Batchno);
            objQuery.AddToQuery("ProdShelfID", shelfID);
            objQuery.AddToQuery("ProdLastPurchaseStockID", stockid);
            objQuery.AddToQuery("ProdLastPurchaseDistributorSaleRatePer", distSaleRatePer);
            return objQuery.UpdateQuery();
        }      
        #endregion

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
       
        public bool IsNameUniqueForAdd(string ProdName,int loosepack, string pack, string compcode,  string Id)
        {
            bool bRetValue = false;
            string strSql = GetQueryUniqueForAdd(ProdName,loosepack,pack,compcode, Id);
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
        public string SearchForProdPack(string pack)
        {
            string mpackID = "";
            string strSql = "Select PackID from masterpack where packName = '"+ pack+"'";
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
                mpackID = drow["PackID"].ToString();
            return mpackID;
        }
        public string SearchForProdPackType(string packtype)
        {
            string mpackID = "";
            string strSql = "Select ID from masterpacktype where packTypeName = '" + packtype + "'";
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
                mpackID = drow["ID"].ToString();
            return mpackID;
        }
        public bool RemoveProductDrugLink(string Id, string ProdGenericID)
        {
            bool bRetValue = false;
            string strSql = "Delete from linkdruggrouping where ProductID = '"+ Id +"' && GenericCategoryID = '"+ ProdGenericID +"'" ;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        #region Query Building Functions
      
        private string GetQueryUniqueForAdd(string ProdName, int loosepack, string pack, string compcode, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ProductID from masterproduct where ProdName='{0}'", ProdName);            
                sQuery.AppendFormat(" AND ProductID in ('{0}')", Id);
                sQuery.AppendFormat(" AND ProdLoosePack =  ('{0}')", loosepack);
                sQuery.AppendFormat(" AND ProdPack =  ('{0}')", pack);
                sQuery.AppendFormat(" AND ProdCompID =  ('{0}')", compcode);
           
            return sQuery.ToString();
        }

        private string GetQueryUniqueForEdit(string ProdName, int loosepack, string pack, string compcode, string Id)
        {
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ProductID from masterproduct where ProdName='{0}'", ProdName);
            sQuery.AppendFormat(" AND ProductID not in ('{0}')", Id);
            sQuery.AppendFormat(" AND ProdLoosePack =  ('{0}')", loosepack);
            sQuery.AppendFormat(" AND ProdPack =  ('{0}')", pack);
            sQuery.AppendFormat(" AND ProdCompID =  ('{0}')", compcode);

            return sQuery.ToString();
        }
        private string GetDeleteQuery(string Id)
        {
            string strSql = "";

            Query objQuery = new Query();
            objQuery.Table = "masterproduct";
            objQuery.AddToQuery("ProductID", Id, true);
            strSql = objQuery.DeleteQuery();

            return strSql;
        }
        #endregion 
        #endregion

    }
}
