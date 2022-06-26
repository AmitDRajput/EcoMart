using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;
using System.Reflection;
using PaperlessPharmaRetail.Common.Classes;
using System.Threading.Tasks;

namespace EcoMart.DataLayer
{
    public class DBProduct
    {
        # region Contructor
        public DBProduct()
        {
        }
        #endregion

        #region Get
        //public DataTable GetOverviewData()
        //{
        //    DataTable dtable = new DataTable();
        //    string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack,floor(a.ProdClosingStock/a.ProdLoosePack) as ProdClosingStockPack," +
        //                    "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
        //                    "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage,a.ProdMRP, " +
        //                    "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,b.ShelfID,b.ShelfCode,c.CompID,c.CompName,d.GenericCategoryName, 0 as  lastSaleMRP, a.ScannedBarcode as Barcode,ProdCategoryID from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId left outer join mastergenericcategory d on a.ProdDrugID = d.GenericCategoryID  order by a.ProdName";

        //    dtable = DBInterface.SelectDataTable(strsql);
        //    return dtable;
        //}


        public DataTable GetOverviewData(double vatPercent)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack,floor(a.ProdClosingStock/a.ProdLoosePack) as ProdClosingStockPack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage,a.ProdMRP, " +
                            "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,a.HSNNumber,b.ShelfID,b.ShelfCode,c.CompID,c.CompName,d.GenericCategoryName, 0 as  lastSaleMRP, a.ScannedBarcode as Barcode from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId left outer join mastergenericcategory d on a.ProdDrugID = d.GenericCategoryID  where a.ProdVatPercent = " + vatPercent + " order by a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetOverviewDataForCache()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack,floor(a.ProdClosingStock/a.ProdLoosePack) as ProdClosingStockPack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdClosingStock as ProdClosingStockDatabase, a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                            "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,a.HSNNumber,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId order by a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetOverviewDataForReports()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdLoosePack,a.ProdCompShortName from masterproduct a order by a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetOverviewDataForScheduleH1()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdClosingStock as ProdClosingStockDatabase, a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                            "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,a.HSNNumber,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId where a.ProdScheduleDrugCode = 'H1' order by a.ProdName";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public bool GetVatPercent(double vatper)
        {
            bool retValue = false;
            DataRow dr;
            string strsql = "Select * from tblvat where vatpercentage = " + vatper;
            dr = DBInterface.SelectFirstRow(strsql);
            if (dr == null)
                retValue = false;
            else
                retValue = true;
            return retValue;
        }

        public DataRow GetProductName(int ProductID)
        {
            DataRow dr = null;
            string strsql = "select prodName from masterproduct where ProductID = '" + ProductID + "'";
            dr = DBInterface.SelectFirstRow(strsql);
            return dr;
        }
        public DataTable GetForBulkMaintenance() // [Ansuman][For testing][5.12.2016]
        {
            DataTable dtable = null;
            string strsql = "select ProductID, ProdName, ProdCompID, ProdCompShortName, ProdLoosePack, ProdPack, prodpacktype, ProdVATPercent, ProdCST, ProdMinLevel, ProdMaxLevel, ProdRequireColdStorage, ProdShelfID, ProdDrugID, ProdCategoryID, ProdPartyId_1, ProdPartyId_2, ProdBoxQuantity, ProdIfShortListed, ProdIfSchedule, ProdScheduleDrugCode, ProdIfSaleDisc, ProdGrade, ProdIfBarCodeRequired, ScannedBarCode,HSNNumber from masterproduct order by ProdName";
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetNonTransactionProductsBulk() // [Ansuman][For testing][14.12.2016]
        {
            DataTable dtable = null;
            string strsql = "select m.ProductID, m.ProdName, m.ProdCompID, m.ProdCompShortName, m.ProdLoosePack, m.ProdPack, m.prodpacktype, m.ProdVATPercent, m.ProdCST, m.ProdMinLevel, m.ProdMaxLevel, m.ProdRequireColdStorage, m.ProdShelfID, m.ProdDrugID, m.ProdCategoryID, m.ProdPartyId_1, m.ProdPartyId_2, m.ProdBoxQuantity, m.ProdIfShortListed, m.ProdIfSchedule, m.ProdScheduleDrugCode, m.ProdIfSaleDisc, m.ProdGrade, m.ProdIfBarCodeRequired, m.ScannedBarCode from masterproduct m where m.ProductID NOT IN (select s.ProductID from tblstock s group by s.ProductID)";
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetTransactionProductsBulk() // [Ansuman][For testing][14.12.2016]
        {
            DataTable dtable = null;
            string strsql = "select m.ProductID, m.ProdName, m.ProdCompID, m.ProdCompShortName, m.ProdLoosePack, m.ProdPack, m.prodpacktype, m.ProdVATPercent, m.ProdCST, m.ProdMinLevel, m.ProdMaxLevel, m.ProdRequireColdStorage, m.ProdShelfID, m.ProdDrugID, m.ProdCategoryID, m.ProdPartyId_1, m.ProdPartyId_2, m.ProdBoxQuantity, m.ProdIfShortListed, m.ProdIfSchedule, m.ProdScheduleDrugCode, m.ProdIfSaleDisc, m.ProdGrade, m.ProdIfBarCodeRequired, m.ScannedBarCode from masterproduct m where m.ProductID IN (select s.ProductID from tblstock s group by s.ProductID)";
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetFilteredProdByProdWise(string ProdName) // [Ansuman][For testing][9.12.2016]
        {
            DataTable dtable = null;
            string strsql = "select ProductID, ProdName, ProdCompID, ProdCompShortName, ProdLoosePack, ProdPack, prodpacktype, ProdVATPercent, ProdCST, ProdMinLevel, ProdMaxLevel, ProdRequireColdStorage, ProdShelfID, ProdDrugID, ProdCategoryID, ProdPartyId_1, ProdPartyId_2, ProdBoxQuantity, ProdIfShortListed, ProdIfSchedule, ProdScheduleDrugCode, ProdIfSaleDisc, ProdGrade, ProdIfBarCodeRequired, ScannedBarCode from masterproduct where ProdName LIKE '" + ProdName + "%'";
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetFilteredProdByCompanyWise(string CompID) // [Ansuman][For testing][9.12.2016]
        {
            DataTable dtable = null;
            string strsql = "select ProductID, ProdName, ProdCompID, ProdCompShortName, ProdLoosePack, ProdPack, prodpacktype, ProdVATPercent, ProdCST, ProdMinLevel, ProdMaxLevel, ProdRequireColdStorage, ProdShelfID, ProdDrugID, ProdCategoryID, ProdPartyId_1, ProdPartyId_2, ProdBoxQuantity, ProdIfShortListed, ProdIfSchedule, ProdScheduleDrugCode, ProdIfSaleDisc, ProdGrade, ProdIfBarCodeRequired, ScannedBarCode from masterproduct where ProdCompID = '" + CompID + "' order by ProdName";
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable GetFilteredProdByContentWise(string DrugID) // [Ansuman][For testing][9.12.2016]
        {
            DataTable dtable = null;
            string strsql = "select ProductID, ProdName, ProdCompID, ProdCompShortName, ProdLoosePack, ProdPack, prodpacktype, ProdVATPercent, ProdCST, ProdMinLevel, ProdMaxLevel, ProdRequireColdStorage, ProdShelfID, ProdDrugID, ProdCategoryID, ProdPartyId_1, ProdPartyId_2, ProdBoxQuantity, ProdIfShortListed, ProdIfSchedule, ProdScheduleDrugCode, ProdIfSaleDisc, ProdGrade, ProdIfBarCodeRequired, ScannedBarCode from masterproduct where ProdDrugID = '" + DrugID + "' order by ProdName";
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataRow GetOverviewDataForProductIDForCache(int ProductID)
        {
            DataRow drow = null;
            string strsql = string.Format("Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdClosingStock, a.ProdClosingStock as ProdClosingStockDatabase ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                            "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,a.HSNNumber,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId where a.ProductID='{0}'", ProductID);

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
        public DataTable GetOverviewDataForClosingStockNotZero(int ProductID)
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
            //string strSql = "Select a.ProductID,b.ProductID,b.ProdName,b.ProdLoosePack," +
            //"b.ProdPack,b.ProdCompShortName,b.ProdBoxQuantity,b.ProdVATPercent,b.ProdCST,b.ProdGrade,b.ProdCompID,b.ProdLastPurchaseMRP," +
            //"b.ProdShelfID,b.ProdScheduleDrugCode,b.ProdIfSchedule,b.ProdClosingStock ,b.ProdOpeningStock,b.ProdIfSaleDisc," +
            //"b.ProdLastPurchaseRate,b.ProdIfShortListed,b.ProdMaxLevel,b.ProdRequireColdStorage,sum(b.ProdClosingStock* b.ProdLastPurchaseMRP / b.ProdLoosePack) as CLValueByMRP, sum(b.ProdClosingStock* b.ProdLastPurchaseRate / b.ProdLoosePack) as CLValueByPurchaseRate,sum(b.ProdOpeningStock* b.ProdLastPurchaseMRP / b.ProdLoosePack) as OPValueByMRP, sum(b.ProdOpeningStock* b.ProdLastPurchaseRate / b.ProdLoosePack) as OPValueByPurchaseRate,c.CompID,c.CompName from tblstock a inner join masterproduct b on a.ProductID = b.ProductID  inner join  mastercompany c on b.ProdCompID = c.CompID  where b.ProdOpeningstock != 0 || b.ProdClosingStock != 0  Group by a.ProductID order by b.ProdName";
            //dtable = DBInterface.SelectDataTable(strSql);

            string strSql = "Select a.ProductID,b.ProductID,b.ProdName,b.ProdLoosePack," +
           "b.ProdPack,b.ProdCompShortName,b.ProdBoxQuantity,b.ProdVATPercent,b.ProdCST,b.ProdGrade,b.ProdCompID,b.ProdLastPurchaseMRP," +
           "b.ProdShelfID,b.ProdScheduleDrugCode,b.ProdIfSchedule,b.ProdClosingStock ,b.ProdOpeningStock,b.ProdIfSaleDisc," +
           "b.ProdLastPurchaseRate,b.ProdIfShortListed,b.ProdMaxLevel,b.ProdRequireColdStorage,sum(a.ClosingStock* a.MRP / b.ProdLoosePack) as CLValueByMRP, sum(a.ClosingStock* a.PurchaseRate / b.ProdLoosePack) as CLValueByPurchaseRate,sum(a.Openingstock* a.MRP / b.ProdLoosePack) as OPValueByMRP, sum(a.Openingstock* a.PurchaseRate / b.ProdLoosePack) as OPValueByPurchaseRate,c.CompID,c.CompName " +
           "from tblstock a inner join masterproduct b on a.ProductID = b.ProductID  inner join  mastercompany c on b.ProdCompID = c.CompID  where " +
           "a.Openingstock >= 0 && a.ClosingStock >= 0  Group by a.ProductID order by b.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataWithZeroAllProducts()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ProductID,b.ProductID,b.ProdName,b.ProdLoosePack," +
            "b.ProdPack,b.ProdCompShortName,b.ProdBoxQuantity,b.ProdVATPercent,b.ProdCST,b.ProdGrade,b.ProdCompID,b.ProdLastPurchaseMRP," +
            "b.ProdShelfID,b.ProdScheduleDrugCode,b.ProdIfSchedule,b.ProdClosingStock ,b.ProdOpeningStock,b.ProdIfSaleDisc," +
            "b.ProdLastPurchaseRate,b.ProdIfShortListed,b.ProdMaxLevel,b.ProdRequireColdStorage,sum(b.ProdClosingStock* b.ProdLastPurchaseMRP / b.ProdLoosePack) as CLValueByMRP, sum(b.ProdClosingStock* b.ProdLastPurchaseRate / b.ProdLoosePack) as CLValueByPurchaseRate,sum(b.ProdOpeningStock* b.ProdLastPurchaseMRP / b.ProdLoosePack) as OPValueByMRP, sum(b.ProdOpeningStock* b.ProdLastPurchaseRate / b.ProdLoosePack) as OPValueByPurchaseRate,c.CompID,c.CompName from tblstock a inner join masterproduct b on a.ProductID = b.ProductID  inner join  mastercompany c on b.ProdCompID = c.CompID  Group by a.ProductID order by b.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataWithOutZeroAllProductsBatchWise()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ProductID,b.ProductID,b.ProdName,b.ProdLoosePack," +
            "b.ProdPack,b.ProdCompShortName,b.ProdBoxQuantity,b.ProdVATPercent,b.ProdCST,b.ProdGrade,b.ProdCompID," +
            "b.ProdShelfID,b.ProdScheduleDrugCode,b.ProdIfSchedule,a.ClosingStock ,a.OpeningStock,b.ProdIfSaleDisc,a.BatchNumber,a.MRP,a.TradeRate," +
            "b.ProdLastPurchaseRate,b.ProdIfShortListed,b.ProdMaxLevel,b.ProdRequireColdStorage,(a.ClosingStock* a.mrp / b.ProdLoosePack) as CLValueByMRP, (a.ClosingStock* a.PurchaseRate / b.ProdLoosePack) as CLValueByPurchaseRate,(a.OpeningStock* a.mrp / b.ProdLoosePack) as OPValueByMRP, (a.OpeningStock* a.PurchaseRate / b.ProdLoosePack) as OPValueByPurchaseRate,c.CompID,c.CompName " +
            "from tblstock a inner join masterproduct b on a.ProductID = b.ProductID  inner join  mastercompany c on b.ProdCompID = c.CompID Where a.ClosingStock >= 0 && a.Openingstock >= 0 order by b.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetForClosingStockNotZero()
        {
            DataTable dtable = new DataTable();
            string strsql = "Select ProductID,ProdName,ProdPack,ProdLoosePack," +
                            "ProdCompShortName,ProdClosingStock from masterproduct where ProdClosingStock <> 0  order by ProdName ";

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

        public DataTable GetOverviewDataForLBTPurchaseOUT(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select d.PurchaseID,d.ProductID,((d.Quantity*d.PurchaseRate) * f.LBTPercent/100) as LBTAmount ,e.VoucherType,e.VoucherNumber,e.VoucherDate,e.PurchaseBillNumber,e.AccountID,e.PurchaseID,e.AmountNet,a.ProductID,a.ProdName,a.ProdPack,a.ProdLoosePack,a.ProdCategoryID," +
                            "b.AccountID,b.AccName,b.IFLBT,f.LBTPercent,f.ProductCategoryName  from detailpurchase d  left outer join voucherpurchase e on d.PurchaseID = e.PurchaseID left outer join masterproduct a on d.ProductID = a.ProductID left outer join masteraccount b on e.AccountID = b.AccountID left outer join masterproductcategory f on a.ProdCategoryID = f.productcategoryID where (b.IFLBT is null || b.IFLBT != 'Y') &&  e.VoucherDate >= '" + fromDate + "' && e.Voucherdate <= '" + toDate + "' order by e.VoucherDate";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        public DataTable GetOverviewDataForLBTPurchaseWITHIN(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select d.PurchaseID,d.ProductID,((d.Quantity*d.PurchaseRate) * f.LBTPercent/100) as LBTAmount ,e.VoucherType,e.VoucherNumber,e.VoucherDate,e.PurchaseBillNumber,e.AccountID,e.PurchaseID,e.AmountNet,a.ProductID,a.ProdName,a.ProdPack,a.ProdLoosePack,a.ProdCategoryID," +
                            "b.AccountID,b.AccName,b.IFLBT,f.LBTPercent,f.ProductCategoryName  from detailpurchase d  left outer join voucherpurchase e on d.PurchaseID = e.PurchaseID left outer join masterproduct a on d.ProductID = a.ProductID left outer join masteraccount b on e.AccountID = b.AccountID left outer join masterproductcategory f on a.ProdCategoryID = f.productcategoryID where (b.IFLBT is null || b.IFLBT != 'Y')  &&  e.VoucherDate >= '" + fromDate + "' && e.Voucherdate <= '" + toDate + "' order by e.VoucherDate";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        public DataTable GetOverviewDataForLBTPurchasePartywise(string party, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select d.PurchaseID,d.ProductID,((d.Quantity*d.PurchaseRate) * f.LBTPercent/100) as LBTAmount ,e.VoucherType,e.VoucherNumber,e.VoucherDate,e.PurchaseBillNumber,e.AccountID,e.PurchaseID,e.AmountNet,a.ProductID,a.ProdName,a.ProdPack,a.ProdLoosePack,a.ProdCategoryID," +
                            "b.AccountID,b.AccName,b.IFLBT,f.LBTPercent,f.ProductCategoryName  from detailpurchase d  left outer join voucherpurchase e on d.PurchaseID = e.PurchaseID left outer join masterproduct a on d.ProductID = a.ProductID left outer join masteraccount b on e.AccountID = b.AccountID left outer join masterproductcategory f on a.ProdCategoryID = f.productcategoryID where e.AccountID = '" + party + "'  &&  e.VoucherDate >= '" + fromDate + "' && e.Voucherdate <= '" + toDate + "' order by e.VoucherDate";

            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataRow GetOverviewDataForLBTSummaryTotalPurchase(string fromDate, string toDate)
        {
            DataRow dr = null;
            string strsql = "Select  sum(AmountNet) as AmountNet   from voucherpurchase  where VoucherDate >= '" + fromDate + "' && Voucherdate <= '" + toDate + "'";

            dr = DBInterface.SelectFirstRow(strsql);
            return dr;
        }
        public DataRow GetOverviewDataForLBTPurchaseOUTSummary(string fromDate, string toDate)
        {
            DataRow dr = null;
            string strsql = "Select  sum(a.AmountNet) as AmountNet   from voucherpurchase a inner join masteraccount b  on a.AccountID = b.AccountID   where b.iflbt = 'Y' && VoucherDate >= '" + fromDate + "' && Voucherdate <= '" + toDate + "'";

            dr = DBInterface.SelectFirstRow(strsql);
            return dr;
        }
        public DataRow GetOverviewDataForLBTPurchaseINSummary(string fromDate, string toDate)
        {
            DataRow dr = null;
            string strsql = "Select  sum(a.AmountNet) as AmountNet   from voucherpurchase a inner join masteraccount b  on a.AccountID = b.AccountID   where (b.IFLBT is null || b.iflbt != 'Y') && VoucherDate >= '" + fromDate + "' && Voucherdate <= '" + toDate + "'";

            dr = DBInterface.SelectFirstRow(strsql);
            return dr;
        }
        public int GetClosingStock(int Id)
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

        public int GetOpeningStock(int Id)
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


        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            DataTable dtableClsStk = new DataTable();
            DataTable dtableMerged = new DataTable();

            string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack,a.ProdClosingStock as ProdClosingStockPack," +
                            "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdClosingStock,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdOpeningStock,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage,a.ProdMRP, " +
                            "a.ProdMinLevel,a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,a.HSNNumber,b.ShelfID,b.ShelfCode,c.CompID,c.CompName,d.GenericCategoryName, 0 as  lastSaleMRP, a.ScannedBarcode as Barcode,ProdCategoryID from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId left outer join mastergenericcategory d on a.ProdDrugID = d.GenericCategoryID  order by a.ProdName";

            // dtable = CacheObject.Get<DataTable>("cacheCounterSale");
            dtable = DBInterface.SelectDataTable(strsql);
            //if (dtable == null)
            //{
            //    dtable = DBInterface.SelectDataTable(strsql);
            //    CacheObject.Add(dtable, "cacheCounterSale");
            //}

            return dtable;


        }



        public static DataTable GetFilteredProductStock(string filterValue)
        {
            DataTable dtable = new DataTable();

            string strsql = "Select TOP 50 a.ProductID,a.ProdClosingStock,'' As BatchNumber, '' As Expiry,cast(0 as decimal(10,2)) AS MRP from masterproduct a  where " + filterValue + "order by prodname";
            dtable = DBInterface.SelectDataTable(strsql);

            return dtable;
        }

        public static DataTable GetFilteredProductFromtbStock(string filterValue)
        {
            DataTable dtable = new DataTable();

            string strsql = "Select TOP 50 a.ProductID,a.ClosingStock As ProdClosingStock, a.BatchNumber, a.Expiry, cast(a.MRP as decimal(10,2)) as MRP from tblstock a  where " + filterValue;
            dtable = DBInterface.SelectDataTable(strsql);

            return dtable;
        }

        public static DataTable GetMergedSimillarProductStock(DataTable dtProduct, DataTable dtStock) // [30.11.2016]
        {
            DataTable dtable = new DataTable();
            //dtable = LINQResultToDataTable(JoinResult);

            DataTable dtTempCounterSale = CacheObject.Get<DataTable>("TempCounterSale");


            if (dtTempCounterSale != null)
            {
                if (dtTempCounterSale.Rows.Count > 0)
                {
                    var JoinResult = (from p in dtProduct.AsEnumerable()
                                      join t in dtTempCounterSale.AsEnumerable()
                                      on new { PID = p.Field<string>("ProductID"), BID = p.Field<string>("BatchNumber") } equals new { PID = t.Field<string>("ProductID"), BID = t.Field<string>("BatchID") }
                                      into joinedtables
                                      from stuff in joinedtables.DefaultIfEmpty()
                                      select new
                                      {
                                          ProdName = p.Field<string>("ProdName"),
                                          ProductID = p.Field<string>("ProductID"),
                                          ProdLoosePack = p.Field<UInt32>("ProdLoosePack"),
                                          ProdPack = p.Field<string>("ProdPack"),
                                          ProdCompShortName = p.Field<string>("ProdCompShortName"),
                                          ProdClosingStock = p.Field<Int32?>("ProdClosingStock"),
                                          StockID = p.Field<string>("StockID"),
                                          BatchNumber = p.Field<string>("BatchNumber"),
                                          Expiry = p.Field<string>("Expiry"),
                                          ExpiryDate = p.Field<string>("ExpiryDate"),
                                          Margin = p.Field<double?>("Margin"),
                                          //ProdClosingStock = t.Field<Int32>("ProdClosingStock")
                                          //BatchID = t.Field<string>("BatchID"),
                                          ClosingStock = p.Field<Int32?>("ClosingStock") - (stuff?.Field<Int32>("QTY") ?? 0)

                                      }).ToList();

                    dtable = LINQResultToDataTable(JoinResult);
                }
                else dtable = dtProduct;
            }
            else dtable = dtProduct;

            return dtable;
        }

        public static DataTable GetMergedProductStock(DataTable dtProduct, DataTable dtStock, DataTable EditedTempDataList)
        {

            DataTable dtable = new DataTable();
            try
            {
                if (dtStock != null)
                {
                    var JoinResult = (from p in dtProduct.AsEnumerable()
                                      join t in dtStock.AsEnumerable()
                                      on p.Field<int>("ProductID") equals t.Field<int>("ProductID")
                                      select new
                                      {
                                          ProductID = p.Field<int>("ProductID"),
                                          ProdName = p.Field<string>("ProdName"),
                                          ProdPack = p.Field<string>("ProdPack"),
                                          ProdPackType = p.Field<string>("ProdPackType"),
                                          //   ProdLoosePack = p.Field<int>("ProdLoosePack"),
                                          ProdClosingStockPack = p.Field<int?>("ProdClosingStockPack"),
                                          ProdCompShortName = p.Field<string>("ProdCompShortName"),
                                          ProdBoxQuantity = p.Field<int?>("ProdBoxQuantity"),
                                          ProdVATPercent = p.Field<decimal?>("ProdVATPercent"),
                                          //////   ProdCST = p.Field<double>("ProdCST"),
                                          ProdGrade = p.Field<string>("ProdGrade"),
                                          ProdCompID = p.Field<int?>("ProdCompID"),
                                          ProdLastPurchaseMRP = p.Field<decimal?>("ProdLastPurchaseMRP"),
                                          ProdLastPurchaseSaleRate = p.Field<decimal?>("ProdLastPurchaseSaleRate"),
                                          ProdShelfID = p.Field<int?>("ProdShelfID"),
                                          ProdScheduleDrugCode = p.Field<string>("ProdScheduleDrugCode"),
                                          ProdIfSchedule = p.Field<string>("ProdIfSchedule"),
                                          ProdOpeningStock = p.Field<int?>("ProdOpeningStock"),
                                          ProdIfSaleDisc = p.Field<string>("ProdIfSaleDisc"),
                                          ProdLastPurchaseRate = p.Field<decimal?>("ProdLastPurchaseRate"),
                                          ProdIfShortListed = p.Field<string>("ProdIfShortListed"),
                                          ProdRequireColdStorage = p.Field<string>("ProdRequireColdStorage"),
                                          ProdMRP = p.Field<decimal?>("ProdMRP"),
                                          /////////
                                          ProdMinLevel = p.Field<int?>("ProdMinLevel"),
                                          ProdMaxLevel = p.Field<int?>("ProdMaxLevel"),
                                          ProdLastSaleStockID = p.Field<int?>("ProdLastSaleStockID"),
                                          ProdLastPurchaseStockID = p.Field<string>("ProdLastPurchaseStockID"),
                                          tag = p.Field<string>("tag"),
                                          ProdDrugID = p.Field<int?>("ProdDrugID"),
                                          ShelfID = p.Field<int?>("ShelfID"),
                                          ShelfCode = p.Field<string>("ShelfCode"),
                                          CompID = p.Field<int?>("CompID"),
                                          CompName = p.Field<string>("CompName"),
                                          GenericCategoryName = p.Field<string>("GenericCategoryName"),

                                          ////   ProdlastSaleMRP = p.Field<double?>("ProdlastSaleMRP"),
                                          Barcode = p.Field<decimal?>("Barcode"),
                                          ProdCategoryID = p.Field<int?>("ProdCategoryID"),
                                          ProdClosingStock = t.Field<int?>("ProdClosingStock"),
                                          BatchNumber = t.Field<string>("BatchNumber"),
                                          Expiry = t.Field<string>("Expiry"),
                                          MRP = t.Field<decimal?>("MRP"),

                                          HSNNumber = p.Field<decimal?>("HSNNumber")
                                      }).ToList();


                    dtable = LINQResultToDataTable(JoinResult);
                }
                else dtable = dtProduct;
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            DataTable dtTempCounterSale = CacheObject.Get<DataTable>("TempCounterSale");
            try
            {
                if (dtTempCounterSale != null)
                {
                    DataTable dtRsult = dtTempCounterSale.Clone();
                    dtRsult.Columns.Remove("BatchID");

                    var distinctRows = dtTempCounterSale.DefaultView.ToTable(true, "ProductID").Rows.OfType<DataRow>().Select(k => k[0] + "").ToArray();
                    foreach (string pID in distinctRows)
                    {
                        var rows = dtTempCounterSale.Select("ProductID = '" + pID + "'");
                        string value = "0";
                        foreach (DataRow row in rows)
                        {
                            value = Convert.ToString(Convert.ToInt32(value) + Convert.ToInt32(row["Qty"]));
                        }
                        value = value.Trim(',');

                        dtRsult.Rows.Add(Convert.ToInt32(pID), value);
                        value = "";
                    }
                    dtTempCounterSale = dtRsult;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            try
            {
                if (dtTempCounterSale != null && dtable != null)
                {
                    if (dtTempCounterSale.Rows.Count > 0 && dtable.Rows.Count > 0)
                    {
                        var JoinResultCounter = (from p in dtable.AsEnumerable() 
                                                 join t in dtTempCounterSale.AsEnumerable() 
                                                    on p.Field<int>("ProductID") equals t.Field<int>("ProductID") 
                                                    into joinedtables from stuff in joinedtables.DefaultIfEmpty()
                                                 select new
                                                 {
                                                     ProductID = p.Field<int>("ProductID"),
                                                     ProdName = p.Field<string>("ProdName"),
                                                     ProdPack = p.Field<string>("ProdPack"),
                                                     ProdPackType = p.Field<string>("ProdPackType"),
                                                     //ProdLoosePack = p.Field<int?>("ProdLoosePack"),
                                                     ProdClosingStockPack = p.Field<int?>("ProdClosingStockPack"),
                                                     ProdCompShortName = p.Field<string>("ProdCompShortName"),
                                                     ProdBoxQuantity = p.Field<int?>("ProdBoxQuantity"),
                                                     ProdVATPercent = p.Field<decimal?>("ProdVATPercent"),
                                                     //ProdCST = p.Field<double?>("ProdCST"),
                                                     ProdGrade = p.Field<string>("ProdGrade"),
                                                     ProdCompID = p.Field<int?>("ProdCompID"),
                                                     ProdLastPurchaseMRP = p.Field<decimal?>("ProdLastPurchaseMRP"),
                                                     ProdLastPurchaseSaleRate = p.Field<decimal?>("ProdLastPurchaseSaleRate"),
                                                     ProdShelfID = p.Field<int?>("ProdShelfID"),
                                                     ProdScheduleDrugCode = p.Field<string>("ProdScheduleDrugCode"),
                                                     ProdIfSchedule = p.Field<string>("ProdIfSchedule"),
                                                     ProdOpeningStock = p.Field<int?>("ProdOpeningStock"),
                                                     ProdIfSaleDisc = p.Field<string>("ProdIfSaleDisc"),
                                                     ProdLastPurchaseRate = p.Field<decimal?>("ProdLastPurchaseRate"),
                                                     ProdIfShortListed = p.Field<string>("ProdIfShortListed"),
                                                     ProdRequireColdStorage = p.Field<string>("ProdRequireColdStorage"),
                                                     ProdMRP = p.Field<decimal?>("ProdMRP"),

                                                     ProdMinLevel = p.Field<int?>("ProdMinLevel"),
                                                     ProdMaxLevel = p.Field<int?>("ProdMaxLevel"),
                                                     ProdLastSaleStockID = p.Field<int?>("ProdLastSaleStockID"),
                                                     ProdLastPurchaseStockID = p.Field<string>("ProdLastPurchaseStockID"),
                                                     tag = p.Field<string>("tag"),
                                                     ProdDrugID = p.Field<int?>("ProdDrugID"),
                                                     ShelfID = p.Field<int?>("ShelfID"),
                                                     ShelfCode = p.Field<string>("ShelfCode"),
                                                     CompID = p.Field<int?>("CompID"),
                                                     CompName = p.Field<string>("CompName"),
                                                     GenericCategoryName = p.Field<string>("GenericCategoryName"),

                                                     //lastSaleMRP = p.Field<int?>("lastSaleMRP"),
                                                     Barcode = p.Field<decimal?>("Barcode"),
                                                     ProdCategoryID = p.Field<int?>("ProdCategoryID"),
                                                     ProdClosingStock = p.Field<int?>("ProdClosingStock") - (stuff?.Field<int?>("QTY") ?? 0),
                                                     BatchNumber = p.Field<string>("BatchNumber"),
                                                     Expiry = p.Field<string>("Expiry"),
                                                     MRP = p.Field<decimal?>("MRP")

                                                     //ProdClosingStock = p.Field<Int32>("ProdClosingStock") - stuff.Field<Int32>("QTY")
                                                 }).ToList();

                        dtable = LINQResultToDataTable(JoinResultCounter);
                    }
                }

                if (EditedTempDataList != null && EditedTempDataList.Rows.Count > 0)
                {
                    foreach (DataRow drDTbl in dtable.Rows)
                    {
                        var rowsProdClsingStk = EditedTempDataList.Select("ProductID = '" + drDTbl["ProductID"].ToString() + "'");
                        foreach (DataRow row in rowsProdClsingStk)
                        {
                            drDTbl["ProdClosingStock"] = Convert.ToInt32(drDTbl["ProdClosingStock"].ToString()) + Convert.ToInt32(row["OldQuantity"]);
                        }
                    }


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dtable;
        }



        public static DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type IcolType = GetProperty.PropertyType;

                        if ((IcolType.IsGenericType) && (IcolType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            IcolType = IcolType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, IcolType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo p in columns)
                {
                    dr[p.Name] = p.GetValue(Record, null) == null ? DBNull.Value : p.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        #endregion

        #region Read
        public DataRow ReadDetailsByID(int Id)
        {
            DataRow dRow = null;
                string strsql = "Select a.ProductID,a.ProdName,a.ProdLoosePack,a.ProdPack,a.ProdPackType," +
                            "a.ProdCompShortName,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID," +
                            "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdDrugID,a.ProdIfOctroi,a.ProdIfSchedule," +
                            "a.ProdCategoryID,a.ProdPartyId_1,a.ProdPartyId_2,a.ProdMinLevel,a.ProdMaxLevel,a.ProdBoxQuantity," +
                            "a.ProdIfMRPInclusive,a.ProdIfSaleDisc,a.ProdIfPurchaseRateInclusive,a.ProdIfShortListed,a.ProdRequireColdStorage,b.ShelfID,b.ShelfCode," +
                             "a.ProdScheduleDrugCode, a.ScannedBarcode, a.ProdlastPurchaseRate,a.HSNNumber from masterproduct a left outer join mastershelf b on a.ProdShelfID = b.ShelfID where ProductID= '{0}'";

                strsql = string.Format(strsql, Id);
                dRow = DBInterface.SelectFirstRow(strsql);
            return dRow;
        }
        public DataRow ReadLastSaleByID(int Id)
        {
            DataRow dRow = null;
            
                string strsql = "select * from masterproduct A left outer join mastershelf B  on A.ProdShelfID = B.ShelfID   where A.ProductID= '{0}' ";
                strsql = string.Format(strsql, Id);
                dRow = DBInterface.SelectFirstRow(strsql);
            return dRow;
        }
        public DataRow ReadLastPurchaseByID(int Id)
        {
            DataRow dRow = null;
            
                string strsql = "select * from masterproduct A left outer join mastershelf B  on A.ProdShelfID = B.ShelfID   where A.ProductID= '{0}' ";
                strsql = string.Format(strsql, Id);
                dRow = DBInterface.SelectFirstRow(strsql);
            return dRow;
        }

        public DataRow GetDetailsForProduct(int prodID)
        {
            DataRow dRow = null;
            
                string strsql = "Select a.ProductID,a.ProdName,a.ProdPack,a.ProdPackType,a.ProdLoosePack," +
                           "a.ProdCompShortName,a.ProdBoxQuantity,a.ProdVATPercent,a.ProdCST,a.ProdGrade,a.ProdCompID,a.ProdLastPurchaseMRP,a.ProdLastPurchaseSaleRate," +
                           "a.ProdShelfID,a.ProdScheduleDrugCode,a.ProdIfSchedule,a.ProdIfOctroi,a.ProdOpeningStock,a.ProdClosingStock ,a.ProdIfSaleDisc,a.ProdLastPurchaseRate,a.ProdIfShortListed,a.ProdRequireColdStorage, " +
                           "a.ProdMaxLevel,a.ProdLastSaleStockID,a.ProdLastPurchaseStockID,a.tag,a.ProdDrugID,a.HSNNumber,b.ShelfID,b.ShelfCode,c.CompID,c.CompName  from masterproduct a  left outer join mastershelf b  on a.ProdShelfID = b.ShelfID  inner join mastercompany c on a.ProdCompId = c.CompId where a.ProductID = '" + prodID + "'";
                dRow = DBInterface.SelectFirstRow(strsql);
            return dRow;
        }
        public DataTable ReadPatientProdDetailsByID(int Id)
        {
            DataTable dt = null;
            
                string strsql = "Select distinct a.ProductID,a.ProdName,a.ProdLoosePack,a.Prodpack , b.PatientId,b.ProductID,b.Quantity from masterproduct a,linkpatientproduct b where a.ProductID = b.ProductID and  b.patientId = '" + Id + "' order by a.prodname";
                dt = DBInterface.SelectDataTable(strsql);
            return dt;

        }
        #endregion

        #region Update
        public bool UpdateClosingStock(int Id)
        {
            bool returnVal = false;
            string strSql = "UPDATE masterProduct SET masterProduct.ProdClosingStock = (SELECT SUM(ClosingStock) FROM tblStock WHERE ProductID = '{0}') WHERE masterproduct.ProductID = '{0}'";
            strSql = string.Format(strSql, Id);

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
        public bool UpdateCreditNoteStockInmasterProduct(int Id, int Quantity)
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
        public bool UpdateCreditNoteStockInmasterProductForNULLClosingStock(int Id, int Quantity)
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
        public bool UpdateCreditNoteStockInmasterProductReduceFromTemp(int Id, int Quantity)
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
        public bool UpdateDebitNoteStockInmasterProduct(int Id, int Quantity)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock =  " + Quantity + " where ProductID = '" + Id + "'";
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
        public bool UpdateDebitNoteStockInmasterProductAddFromTemp(int Id, int Quantity)
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
        public bool UpdateSaleStockInmasterProduct(int Id, int Quantity, string stockid, string voutype, int vouno, string voudate, string accountid, string scancode, double mrp)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock = " + Quantity + ", ProdLastSaleStockID = '" + stockid + "', ProdLastSaleBillType = '" + voutype + "', ProdLastSaleBillNumber = " + vouno + ", ProdLastSaleDate = '" + voudate + "',ProdLastSalePartyId = '" + accountid + "', ProdLastSaleScanID = '" + scancode + "', ProdMRP = " + mrp + "  where ProductID = '" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }
        public bool UpdateDebtorSaleStockInmasterProductAddFromTemp(int Id, int Quantity)
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
        public bool UpdateProdSelfID(Int32 id, Int32 prodShelfID)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdShelfID = '" + prodShelfID + "' where ProductID = " + "'" + id + "'";
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
        public bool UpdateProductScanCode(Int32 id, string ScannedBarCode)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ScannedBarCode = '" + ScannedBarCode + "' where ProductID = " + id;
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
        public bool UpdatePurchaseStockInmasterProduct(int ProdId, int Quantity, double margin, double distSaleRatePer)
        {
            bool returnVal = false;
            string strSql = "";
            int closingstk = 0;
            DataRow dRow = null;
            int ProductID = 0;
            ProductID = Convert.ToInt32(ProdId);
            strSql = "select ProdClosingStock from masterproduct where ProductID = " + ProductID;
            dRow = DBInterface.SelectFirstRow(strSql);
            if (dRow != null)
                if (dRow["prodclosingstock"] != DBNull.Value)
                    closingstk = Convert.ToInt32(dRow["prodclosingstock"].ToString());
            closingstk += Quantity;
            strSql = "Update masterProduct SET ProdClosingStock = " + closingstk + " , prodmargin = " + margin + " where ProductID = " + ProductID;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }

        public bool UpdateOpeningStockInmasterProduct(int Id, string Batchno, double mrp, int closingStock, int openingStock)
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdClosingStock = " + closingStock + ", ProdOpeningStock = " + openingStock + " where ProductID = " + "'" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }

        public bool UpdatePurchaseDataInmasterProduct(int ProductID, string PurchaseBillNumber, string VoucherDate, string AccountID, string VoucherType,
                int VoucherNumber, double PurchaseRate, double TradeRate, double SaleRate, double MRP, double PurchaseVATPercent, double CSTPercent, double AmountCST, double SchemeDiscountPercent,
                double AmountSchemeDiscount, double ItemDiscountPercent, string Expiry, string ExpiryDate, string Batchno, string shelfID, string stockid)
        {
            bool returnVal = false;
            string strSql = GetUpdateQueryforlastpurchase(ProductID, PurchaseBillNumber, VoucherDate, AccountID, VoucherType,
                   VoucherNumber, PurchaseRate, TradeRate, SaleRate, MRP, PurchaseVATPercent, CSTPercent, AmountCST, SchemeDiscountPercent,
                   AmountSchemeDiscount, ItemDiscountPercent, Expiry, ExpiryDate, Batchno, shelfID, stockid);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }
            return returnVal;

        }

        public bool UpdatePurchaseStockInmasterProductReduceFromTemp(int ProdId, int Quantity)
        {
            bool returnVal = false;
            string strSql = "";
            int closingstk = 0;
            DataRow dRow = null;
            
                strSql = "select ProdClosingStock from masterproduct where ProductID = '" + ProdId + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
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

        public bool UpdateStockProdInmasterProduct(int ID, string panme, string cname, string cshortname, string GenCatName, string ProdCatName, int UOM, string packingtype, string pack, string ShelfID, string pscheduledrug) // [ansuman]
        {
            bool returnVal = false;
            string strSql = "Update masterProduct SET ProdName = '" + panme + "', ProdCompID = '" + cname + "', ProdCompShortName = '" + cshortname + "', ProdDrugID = '" + GenCatName + "', ProdCategoryID ='" + ProdCatName + "', ProdLoosePack = " + UOM + ", prodpacktype = '" + packingtype + "', ProdPack = '" + pack + "', ProdShelfID = '" + ShelfID + "', ProdScheduleDrugCode = '" + pscheduledrug + "' where ProductID = " + "'" + ID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }

        #endregion

        #region For Product

        public int AddDetails(int Id, string ProdName,
           int ProdLoosePack, string ProdPack, string ProdPackType, string ProdCompShortName,
            Int32 ProdCompID, double ProdVATPercent, double ProdCST, string ProdGrade,
            int ProdMinLevel, int ProdMaxLevel, int ProdBoxQty,
            Int32 ProdGenericID, Int32 ProdShelfID, string ProdScheduleDrugCode, Int32 ProdCategoryID,
            Int32 ProdPartyId_1, Int32 ProdPartyId_2,
            string ProdIfShortListed, string ProdIfSaleDisc, string ProdIfOctroi,
           string ProdIfMRPInclusive, string ProdIfPurchaseRateInclusive, int prodopeningstock, int prodclosingstock,
           string prodifscheduleddrug, string prodrequirecoldstorage, string prodIfBarCodeRequired, string ScannedBarcode, int hsnnumber,
           string createdby, string createddate, string createdtime)
        {
            int iRetValue = 0;
            string strSql = GetInsertQuery(Id, ProdName,
           ProdLoosePack, ProdPack, ProdPackType, ProdCompShortName,
             ProdCompID, ProdVATPercent, ProdCST, ProdGrade,
             ProdMinLevel, ProdMaxLevel, ProdBoxQty,
             ProdGenericID, ProdShelfID, ProdScheduleDrugCode,
              ProdCategoryID, ProdPartyId_1, ProdPartyId_2,
              ProdIfShortListed,
            ProdIfOctroi, ProdIfMRPInclusive, ProdIfSaleDisc, ProdIfPurchaseRateInclusive, prodopeningstock,
            prodclosingstock, prodifscheduleddrug, prodrequirecoldstorage, prodIfBarCodeRequired, ScannedBarcode, hsnnumber,
            createdby, createddate, createdtime);

            iRetValue = DBInterface.ExecuteScalar(strSql);
            return iRetValue;
           
        }
        public bool AddPack(Int32 Id, string Pack)
        {
            bool returnVal = false;
            string strSql = GetInsertQueryPack(Id, Pack);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            return returnVal;
        }
        public bool AddPackType(Int32 Id, string Packtype)
        {
            bool returnVal = false;
            string strSql = GetInsertQueryPackType(Id, Packtype);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                returnVal = true;
            return returnVal;
        }
        public bool SaveProductDrugLink(Int32 linkID, Int32 ProductID, Int32 drugID, string createdBy, string createdDate, string createdTime)
        {
            bool returnVal = false;
            string strSql = GetInsertQueryAddInDrugGrouping(linkID, ProductID, drugID, createdBy, createdDate, createdTime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }
            return returnVal;
        }
        public bool UpdateDetails(int Id, string ProdName,
           int ProdLoosePack, string ProdPack, string ProdPackType, string ProdCompShortName,
            Int32 ProdCompID, double ProdVATPercent, double ProdCST, string ProdGrade,
            int ProdMinLevel, int ProdMaxLevel, int ProdBoxQty,
            Int32 ProdGenericID, Int32 ProdShelfID, string ProdScheduleDrugCode, Int32 ProdCategoryID,
            Int32 ProdPartyId_1, Int32 ProdPartyId_2,
            string ProdIfShortListed, string ProdIfSaleDisc, string ProdIfOctroi,
           string ProdIfMRPInclusive, string ProdIfPurchaseRateInclusive, string prodifscheduleddrug, string prodrequirecoldstorage,
           string prodIfBarCodeRequired, string ScannedBarcode, int hsnnumber, string modifiedby, string modifydate, string modifytime)
        {
            bool returnVal = false;
            string strSql = GetUpdateQuery(Id, ProdName,
            ProdLoosePack, ProdPack, ProdPackType, ProdCompShortName,
             ProdCompID, ProdVATPercent, ProdCST, ProdGrade, ProdMinLevel, ProdMaxLevel, ProdBoxQty,
             ProdGenericID, ProdShelfID, ProdCategoryID, ProdPartyId_1, ProdPartyId_2,
             ProdScheduleDrugCode, ProdIfShortListed,
             ProdIfOctroi, ProdIfMRPInclusive, ProdIfSaleDisc, ProdIfPurchaseRateInclusive, prodifscheduleddrug,
             prodrequirecoldstorage, prodIfBarCodeRequired, ScannedBarcode, hsnnumber, modifiedby, modifydate, modifytime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }
            return returnVal;
        }

        public bool UpdateDetailsBulk(int Id, string ProdName, Int32 ProdCompID, string ProdCompShortName, int ProdLoosePack,
            string ProdPack, string ProdPackType, double ProdVATPercent, double ProdCST, int ProdMinLevel, int ProdMaxLevel,
            string prodrequirecoldstorage, Int32 ProdShelfID, Int32 ProdGenericID, Int32 ProdCategoryID,
            Int32 ProdPartyId_1, Int32 ProdPartyId_2, int ProdBoxQty, string ProdIfShortListed, string prodifscheduleddrug,
            string ProdScheduleDrugCode, string ProdIfSaleDisc, string ProdGrade, string prodIfBarCodeRequired, string ScannedBarcode, int hsnnumber,
            string modifiedby, string modifydate, string modifytime)
        {
            bool returnVal = false;
            string strSql = GetUpdateQueryForBulk(Id, ProdName, ProdCompID, ProdCompShortName, ProdLoosePack, ProdPack, ProdPackType,
             ProdVATPercent, ProdCST, ProdMinLevel, ProdMaxLevel, prodrequirecoldstorage, ProdShelfID, ProdGenericID, ProdCategoryID,
             ProdPartyId_1, ProdPartyId_2, ProdBoxQty, ProdIfShortListed, prodifscheduleddrug, ProdScheduleDrugCode, ProdIfSaleDisc,
             ProdGrade, prodIfBarCodeRequired, ScannedBarcode, hsnnumber, modifiedby, modifydate, modifytime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }
            return returnVal;
        }

        private string GetInsertQuery(int Id, string ProdName,
            int ProdLoosePack, string ProdPack, string ProdPackType, string ProdCompShortName,
            int ProdCompID, double ProdVATPercent, double ProdCST, string ProdGrade,
            int ProdMinLevel, int ProdMaxLevel, int ProdBoxQty,
            int ProdGenericID, int ProdShelfID, string ProdScheduleDrugCode,
            int ProdCategoryID, int ProdPartyId_1, int ProdPartyId_2,
            string ProdIfShortListed, string ProdIfOctroi,
           string ProdIfMRPInclusive, string ProdIfSaleDisc, string ProdIfPurchaseRateInclusive, int prodopeningstock,
           int prodclosingstock, string prodifscheduleddrug, string prodrequirecoldstorage, string prodIfBarCodeRequired,
           string ScannedBarcode, int hsnnumber, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterproduct";
            //objQuery.AddToQuery("ProductID", Id);
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
            objQuery.AddToQuery("ProdOpeningStock", prodopeningstock);
            objQuery.AddToQuery("ProdClosingStock", prodclosingstock);
            objQuery.AddToQuery("ProdIfSchedule", prodifscheduleddrug);
            objQuery.AddToQuery("ProdRequireColdStorage", prodrequirecoldstorage);
            objQuery.AddToQuery("ProdIfBarCodeRequired", prodIfBarCodeRequired);
            //objQuery.AddToQuery("ScannedBarcode", ScannedBarcode);
            objQuery.AddToQuery("HSNNumber", hsnnumber);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryPack(Int32 Id, string Pack)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterpack";
            objQuery.AddToQuery("PackID", Id);
            objQuery.AddToQuery("PackName", Pack);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryPackType(Int32 Id, string Packtype)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterpacktype";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("PackTypeName", Packtype);
            return objQuery.InsertQuery();
        }
        private string GetInsertQueryAddInDrugGrouping(Int32 linkID, Int32 ProductID, Int32 drugID, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "linkdruggrouping";
            objQuery.AddToQuery("LinkDrugGroupingID", linkID);
            objQuery.AddToQuery("GenericCategoryID", drugID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }
        private string GetUpdateQuery(int Id, string ProdName,
           int ProdLoosePack, string ProdPack, string ProdPackType, string ProdCompShortName,
            Int32 ProdCompID, double ProdVATPercent, double ProdCST, string ProdGrade,
            int ProdMinLevel, int ProdMaxLevel, int ProdBoxQty,
           Int32 ProdGenericID, Int32 ProdShelfID, Int32 ProdCategoryID, Int32 ProdPartyId_1, Int32 ProdPartyId_2,
            string ProdScheduleDrugCode,
             string ProdIfShortListed, string ProdIfOctroi,
           string ProdIfMRPInclusive, string ProdIfSaleDisc, string ProdIfPurchaseRateInclusive, string prodifscheduleddrug,
           string prodrequirecoldstorage, string prodIfBarCodeRequired, string ScannedBarcode, int hsnnumber,
           string modifiedby, string modifydate, string modifytime)
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
            //objQuery.AddToQuery("ScannedBarcode", ScannedBarcode);
            objQuery.AddToQuery("HSNNumber", hsnnumber);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);

            return objQuery.UpdateQuery();
        }

        private string GetUpdateQueryForBulk(int Id, string ProdName, int ProdCompID, string ProdCompShortName,   // [Ansuman]
           int ProdLoosePack, string ProdPack, string ProdPackType, double ProdVATPercent, double ProdCST,
           int ProdMinLevel, int ProdMaxLevel, string prodrequirecoldstorage, int ProdShelfID, int ProdGenericID,
           int ProdCategoryID, int ProdPartyId_1, int ProdPartyId_2, int ProdBoxQty, string ProdIfShortListed,
           string prodifscheduleddrug, string ProdScheduleDrugCode, string ProdIfSaleDisc, string ProdGrade, string prodIfBarCodeRequired,
            string ScannedBarcode, int hsnnumber, string modifiedby, string modifydate, string modifytime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterproduct";
            objQuery.AddToQuery("ProductID", Id, true);
            objQuery.AddToQuery("ProdName", ProdName);
            objQuery.AddToQuery("ProdCompID", ProdCompID);
            objQuery.AddToQuery("ProdCompShortName", ProdCompShortName);
            objQuery.AddToQuery("ProdLoosePack", ProdLoosePack);
            objQuery.AddToQuery("ProdPack", ProdPack);
            objQuery.AddToQuery("ProdPackType", ProdPackType);
            objQuery.AddToQuery("ProdVATPercent", ProdVATPercent);
            objQuery.AddToQuery("ProdCST", ProdCST);
            objQuery.AddToQuery("ProdMinLevel", ProdMinLevel);
            objQuery.AddToQuery("ProdMaxLevel", ProdMaxLevel);
            objQuery.AddToQuery("ProdRequireColdStorage", prodrequirecoldstorage);
            objQuery.AddToQuery("ProdShelfID", ProdShelfID);
            objQuery.AddToQuery("ProdDrugID", ProdGenericID);
            objQuery.AddToQuery("ProdCategoryID", ProdCategoryID);
            objQuery.AddToQuery("ProdPartyId_1", ProdPartyId_1);
            objQuery.AddToQuery("ProdPartyId_2", ProdPartyId_2);
            objQuery.AddToQuery("ProdBoxQuantity", ProdBoxQty);
            objQuery.AddToQuery("ProdIfShortListed", ProdIfShortListed);
            objQuery.AddToQuery("ProdIfSchedule", prodifscheduleddrug);
            objQuery.AddToQuery("ProdScheduleDrugCode", ProdScheduleDrugCode);
            objQuery.AddToQuery("ProdIfSaleDisc", ProdIfSaleDisc);
            objQuery.AddToQuery("ProdGrade", ProdGrade);
            objQuery.AddToQuery("ProdIfBarCodeRequired", prodIfBarCodeRequired);
            objQuery.AddToQuery("ScannedBarcode", ScannedBarcode);
            objQuery.AddToQuery("HSNNumber", hsnnumber);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifydate);
            objQuery.AddToQuery("ModifiedTime", modifytime);

            return objQuery.UpdateQuery();
        }

        private string GetUpdateQueryforlastpurchase(int ProductID, string PurchaseBillNumber, string VoucherDate, string AccountID, string VoucherType,
                int VoucherNumber, double PurchaseRate, double TradeRate, double SaleRate, double MRP, double PurchaseVATPercent,
               double CSTPercent, double AmountCST, double SchemeDiscountPercent,
                double AmountSchemeDiscount, double ItemDiscountPercent, string Expiry, string ExpiryDate, string Batchno, string shelfID, string stockid)
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
            objQuery.AddToQuery("ProdMRP", MRP);
            objQuery.AddToQuery("ProdLastPurchaseVATPer", PurchaseVATPercent);
            objQuery.AddToQuery("ProdVATPercent", PurchaseVATPercent);
            objQuery.AddToQuery("ProdLastPurchaseCSTPer", CSTPercent);
            objQuery.AddToQuery("ProdLastPurchaseCST", AmountCST);
            objQuery.AddToQuery("ProdLastPurchaseSCMPer", SchemeDiscountPercent);
            objQuery.AddToQuery("ProdLastPurchaseSCM", AmountSchemeDiscount);
            objQuery.AddToQuery("ProdLastPurchaseItemDiscPer", ItemDiscountPercent);
            objQuery.AddToQuery("ProdLastPurchaseExpiry", Expiry);
            objQuery.AddToQuery("ProdLastPurchaseExpiryDate", ExpiryDate);
            objQuery.AddToQuery("ProdLastPurchaseBatchNumber", Batchno);
            //objQuery.AddToQuery("ProdShelfID", shelfID);
            objQuery.AddToQuery("ProdLastPurchaseStockID", stockid);
            //  objQuery.AddToQuery("ProdLastPurchaseDistributorSaleRatePer", distSaleRatePer);
            //   objQuery.AddToQuery("ProdLastPurchaseDistributorSaleRate", distributorSaleRate);
            return objQuery.UpdateQuery();
        }
        #endregion

        #region Other Comman Functions
        public bool DeleteDetails(int Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQuery(Id);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
       
       

       

       
        //public bool IsNameUniqueForEdit(string Name, string Id)
        //{
        //    int ifdup = GetDataForUniqueForAdd(Name, Id);
        //    bool bRetValue = false;
        //    if (ifdup > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}
        public int IsNameUniqueForEdit(string ProdName, int loosepack, string pack, int compcode, string Id)
        {
            //bool bRetValue = false;
            string strSql = GetQueryUniqueForAdd(ProdName, loosepack, pack, compcode, Id);
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow == null)
            {
                return 0;
            }
            else
                return 1;
        }
        public int IsNameUniqueForAdd(string ProdName, int loosepack, string pack, Int32 compcode, string Id)
        {
            //bool bRetValue = false;
            string strSql = GetQueryUniqueForAdd(ProdName, loosepack, pack, compcode, Id);
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow == null)
            {
                return 0;
            }
            else
                return 1;
           
        }
        private string GetQueryUniqueForAdd(string ProdName, int loosepack, string pack, Int32 compcode, string Id)
        {
            int iId = 0;
            if (Id == "")
                iId = 0;
            else
                iId = Convert.ToInt32(Id);
            StringBuilder sQuery = new StringBuilder();
            sQuery.AppendFormat("Select ProductID from masterproduct where ProdName='{0}'", ProdName);
            sQuery.AppendFormat(" AND ProductID = ({0})", iId);
            sQuery.AppendFormat(" AND ProdLoosePack =  ('{0}')", loosepack);
            sQuery.AppendFormat(" AND ProdPack =  ('{0}')", pack);
            sQuery.AppendFormat(" AND ProdCompID =  ({0})", compcode);

            return sQuery.ToString();
        }
        //private int GetDataForUniqueForAdd(string Name, string Id)
        //{
        //    StringBuilder sQuery = new StringBuilder();
        //    DataRow dRow = null;
        //    string strSql = "Select BankId from masterbank where BankName= '" + Name + "'";
        //    dRow = DBInterface.SelectFirstRow(strSql);
        //    if (dRow == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return 1;
        //    }

        //}
        public string SearchForProdPack(string pack)
        {
            string mpackID = "";
            string strSql = "Select PackID from masterpack where packName = '" + pack + "'";
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
                mpackID = drow["PackID"].ToString();
            return mpackID;
        }
        public Int32 SearchForProdPackType(string packtype)
        {
            Int32 mpackID = 0;
            string strSql = "Select ID from masterpacktype where packTypeName = '" + packtype + "'";
            DataRow drow = DBInterface.SelectFirstRow(strSql);
            if (drow != null)
                mpackID = Convert.ToInt32(drow["ID"].ToString());
            return mpackID;
        }
        public bool RemoveProductDrugLink(Int32 Id, Int32 ProdGenericID)
        {
            bool bRetValue = false;
            string strSql = "Delete from linkdruggrouping where ProductID = '" + Id + "' && GenericCategoryID = '" + ProdGenericID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        #region Query Building Functions

        

        //private string GetQueryUniqueForEdit(string ProdName, int loosepack, string pack, int compId, Int32 Id)
        //{
        //    StringBuilder sQuery = new StringBuilder();
        //    sQuery.AppendFormat("Select ProductID from masterproduct where ProdName='{0}'", ProdName);
        //    sQuery.AppendFormat(" AND ProductID != ({0})", Id);
        //    sQuery.AppendFormat(" AND ProdLoosePack =  ('{0}')", loosepack);
        //    sQuery.AppendFormat(" AND ProdPack =  ('{0}')", pack);
        //    sQuery.AppendFormat(" AND ProdCompID =  ({0})", compId);

        //    return sQuery.ToString();
        //}
        private string GetDeleteQuery(int Id)
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

        public bool UpdatePurchaseOrder(int ProductID, int Quantity)
        {
            bool returnVal = false;
            int prodid = Convert.ToInt32(ProductID);
            string strSql = "Update tbldailyshortlist SET PurchaseQuantity = " + Quantity + "  where ProductID = '" + ProductID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }

        public bool SetMinMax(int _min, int _max)
        {
            bool returnVal = false;
            string strSql = "Update masterproduct SET prodminLevel = " + _min + ", ProdmaxLevel = " + _max;
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }

        public bool RemoveEDEProductLink(int prodID, string partyID)
        {
            bool returnVal = false;
            string strSql = "delete from tblbillimportlink where DistributorID = '" + partyID + "' && RetailerProductID = '" + prodID + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                returnVal = true;
            }

            return returnVal;
        }
        public DataRow GetDuplicateBarcode(string Barcode) // [08.02.2017]
        {
            DataRow dr;
            string strsql = "select ProductID,ProdName,ScannedBarCode from masterproduct where ScannedBarCode = '" + Barcode + "'";
            dr = DBInterface.SelectFirstRow(strsql);
            return dr;
        }

        //public DataRow GetLastProductNoForBarCode() // [10.02.2017]
        //{
        //    DataRow dr = null;
        //    try
        //    {
        //        string strSql = "select ProductID,ProductNumberForBarcode from masterproduct where ProductNumberForBarcode is not null order by ProductID desc limit 1";
        //        strSql = string.Format(strSql);
        //        dr = DBInterface.SelectFirstRow(strSql);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);
        //    }
        //    return dr;
        //}
        public DataRow GetMaxID()
        {
            DataRow dRow = null;

            string strSql = "Select max(ProductID) as maxid from masterproduct ";
            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }
    }
}
