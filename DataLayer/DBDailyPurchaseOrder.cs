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
    public class DBDailyPurchaseOrder
    {
        public DBDailyPurchaseOrder()
        {
        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            return dtable;
        }

        public DataTable GetOverviewDetailData(int ordernumber)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.stockistOrderNumber,a.stockistOrderDate,a.stockistOrderQuantity,a.ProductID,(a.stockistOrderQuantity*a.PurchaseRate) as Amount ,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName from detailpurchaseorderstockist a inner join masterproduct b ON a.ProductID = b.ProductID WHERE a.stockistOrderNumber = " + ordernumber;
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        internal DataTable GetOverviewSummaryData(int FirstOrderNumber)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.stockistOrderNumber, sum(a.stockistOrderQuantity*a.PurchaseRate) as Amount from detailpurchaseorderstockist a inner join masteraccount b ON a.stockistAccountId = b.AccountID WHERE a.stockistOrderNumber >= " + FirstOrderNumber + " Group by a.stockistOrderNumber";
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable ReadShotListByDate(string fromday, string today)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.stockistOrderNumber,a.stockistOrderDate,a.ShortListDate,a.stockistOrderQuantity,a.stockistSchemeQuantity,a.IfSave,a.stockistAccountID,e.AccName,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel - b.ProdClosingStock as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName,c.AccountID as AccountID1, c.AccName as AccName1,c.AccAddress1,c.AccAddress2, d.AccountID as AccountID2,d.AccName as AccName2  from detailpurchaseorderstockist a " +
         " inner join masterproduct b on a.ProductID = b.ProductID  left outer join masteraccount e on a.AccountID = e.AccountID  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID  left outer join masteraccount d on b.ProdPartyId_2 = d.AccountID where a.ShortListDate >= " + "'" + fromday + "'  AND a.ShortListDate <= '" + today + "' AND a.stockistOrderNumber = 0 AND b.ProdMaxLevel >= b.ProdClosingStock";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadShotListByDateForToday(string fromday, string today)
        {

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSql = "Select distinct a.ProductID, sum(a.Quantity) as Quantity ,a.Voucherdate from  detailsale a " +
   "  where  a.voucherdate >= '" + fromday + "' AND a.voucherdate <= '" + today + "'  Group by a.ProductID ";

            dt1 = DBInterface.SelectDataTable(strSql);


            strSql = "Select b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
     "COALESCE(b.ProdPartyId_1),COALESCE(b.ProdPartyId_2),b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName as AccName,c.AccCrVisitDays as AccCrVisitDays,c.AccAddress1,c.AccAddress2 " +
    " from masterproduct b  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID   order by ProdClosingStock asc";

            dt2 = DBInterface.SelectDataTable(strSql);

            DataTable dtable = new DataTable();
            dtable = GetMergedProductStockDecimalQuantity(dt2, dt1);


            return dtable;
        }
        public DataTable GetMergedProductStockIntegerQuantity(DataTable dtProduct, DataTable dtStock)
        {
            DataTable dtable = new DataTable();
            try
            {
                var JoinResult = (from p in dtProduct.AsEnumerable()
                                  join t in dtStock.AsEnumerable()
                                  on p.Field<string>("ProductID") equals t.Field<string>("ProductID")
                                  select new
                                  {
                                      AccountID = p.Field<string>("AccountID"),
                                      AccName = p.Field<string>("AccName"),
                                      ProductID = p.Field<string>("ProductID"),
                                      ProdName = p.Field<string>("ProdName"),
                                      ProdPack = p.Field<string>("ProdPack"),
                                      //  ProdPackType = p.Field<string>("ProdPackType"),
                                      ProdLoosePack = p.Field<UInt32>("ProdLoosePack"),
                                      //   ProdClosingStockPack = p.Field<Int64>("ProdClosingStockPack"),
                                      ProdCompShortName = p.Field<string>("ProdCompShortName"),
                                      ProdBoxQuantity = p.Field<UInt32>("ProdBoxQuantity"),
                                      //  ProdVATPercent = p.Field<double>("ProdVATPercent"),
                                      //   ProdCST = p.Field<double>("ProdCST"),
                                      //    ProdGrade = p.Field<string>("ProdGrade"),
                                      //    ProdCompID = p.Field<string>("ProdCompID"),
                                      //     ProdLastPurchaseMRP = p.Field<double?>("ProdLastPurchaseMRP"),
                                      //     ProdLastPurchaseSaleRate = p.Field<double?>("ProdLastPurchaseSaleRate"),
                                      //      ProdShelfID = p.Field<string>("ProdShelfID"),
                                      //     ProdScheduleDrugCode = p.Field<string>("ProdScheduleDrugCode"),
                                      //     ProdIfSchedule = p.Field<string>("ProdIfSchedule"),
                                      //     ProdOpeningStock = p.Field<UInt32>("ProdOpeningStock"),
                                      //     ProdIfSaleDisc = p.Field<string>("ProdIfSaleDisc"),
                                      ProdLastPurchaseRate = p.Field<double?>("ProdLastPurchaseRate"),
                                      //     ProdIfShortListed = p.Field<string>("ProdIfShortListed"),
                                      //      ProdRequireColdStorage = p.Field<string>("ProdRequireColdStorage"),
                                      //     ProdMRP = p.Field<double?>("ProdMRP"),

                                      ProdMaxLevel = p.Field<UInt32>("ProdMaxLevel"),
                                      //    ProdLastSaleStockID = p.Field<string>("ProdLastSaleStockID"),
                                      //     ProdLastPurchaseStockID = p.Field<string>("ProdLastPurchaseStockID"),
                                      //     tag = p.Field<string>("tag"),
                                      //       ProdDrugID = p.Field<string>("ProdDrugID"),
                                      //       ShelfID = p.Field<string>("ShelfID"),
                                      //       ShelfCode = p.Field<string>("ShelfCode"),
                                      //       CompID = p.Field<string>("CompID"),
                                      //       CompName = p.Field<string>("CompName"),
                                      //      GenericCategoryName = p.Field<string>("GenericCategoryName"),

                                      //      lastSaleMRP = p.Field<Int64>("lastSaleMRP"),
                                      //     Barcode = p.Field<string>("Barcode"),
                                      //       ProdCategoryID = p.Field<string>("ProdCategoryID"),
                                      ProdClosingStock = p.Field<Int32>("ProdClosingStock"),
                                      Quantity = t.Field<Int32>("Quantity"),
                                      SchemeQuantity = "",
                                      AccAddress1 = "",
                                      AccAddress2 = "",
                                      OrderQuantity = 0

                                  }).ToList();

                dtable = LINQResultToDataTable(JoinResult);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dtable;
        }
        public DataTable GetMergedProductStockDecimalQuantity(DataTable dtProduct, DataTable dtStock)
        {
            DataTable dtable = new DataTable();
            try
            {
                var JoinResult = (from p in dtProduct.AsEnumerable()
                                  join t in dtStock.AsEnumerable()
                                  on p.Field<int>("ProductID") equals t.Field<int>("ProductID")
                                  select new
                                  {
                                      AccountID = p.Field<int?>("AccountID"),
                                      AccName = p.Field<string>("AccName"),
                                      ProductID = p.Field<int>("ProductID"),
                                      ProdName = p.Field<string>("ProdName"),
                                      ProdPack = p.Field<string>("ProdPack"),
                                      ProdLoosePack = p.Field<int?>("ProdLoosePack"),
                                      ProdCompShortName = p.Field<string>("ProdCompShortName"),
                                      ProdBoxQuantity = p.Field<int?>("ProdBoxQuantity"),
                                      ProdLastPurchaseRate = p.Field<decimal?>("ProdLastPurchaseRate"),
                                      ProdMaxLevel = p.Field<int?>("ProdMaxLevel"),
                                      ProdClosingStock = p.Field<int?>("ProdClosingStock"),
                                      OrderQuantity = t.Field<int?>("Quantity"),
                                      SchemeQuantity = "",
                                      AccAddress1 = "",
                                      AccAddress2 = "",
                                      //OrderQuantity = 0

                                  }).ToList();

                dtable = LINQResultToDataTable(JoinResult);
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

                        if ((IcolType.IsGenericType) &&
                            (IcolType.GetGenericTypeDefinition() == typeof(Nullable<>)))
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

        public bool UpdateMasterIDinDetailPurchaseOrder(int dSLOrderNumber, int intID)
        {
            bool bRetValue = false;
            string strSql = "Update detailpurchaseorderstockist set masterID = " + intID + " where stockistorderNumber = " + dSLOrderNumber;

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool InsertRowinDailypurchaseorderfromstockist(int mshopid, int mcnfid, int mecomartid, int mprodid, int orderqty,int mschemeqty,int salequantity, int closingstock, int mordernumber, string morderdate)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryForDailypurchaseorderfromstockist(mshopid, mcnfid, mecomartid, mprodid, orderqty, mschemeqty, salequantity, closingstock,mordernumber, morderdate);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetInsertQueryForDailypurchaseorderfromstockist(int mshopid, int mcnfid, int mecomartid, int mprodid, int orderqty,int dslSchemeQuantity, int salequantity, int closingstock, int mordernumber, string morderdate)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailpurchaseorderfromstockist";
            //objQuery.AddToQuery("DSLID", DSLID);
            objQuery.AddToQuery("EcoMartID", mecomartid);
            objQuery.AddToQuery("CNFID", mcnfid);
            objQuery.AddToQuery("StockistID", mshopid);
            objQuery.AddToQuery("ProductID", mprodid);            
            objQuery.AddToQuery("stockistOrderQuantity", orderqty);
            objQuery.AddToQuery("stockistSchemeQuantity", dslSchemeQuantity);
            objQuery.AddToQuery("stockistOrderNumber", mordernumber);
            objQuery.AddToQuery("stockistOrderDate", morderdate);
            objQuery.AddToQuery("StockistSaleQuantity", salequantity);
            objQuery.AddToQuery("StockistClosingStock", closingstock);


            //objQuery.AddToQuery("IfDailyShortList", DSLDailyshortlist);
            //objQuery.AddToQuery("PurchaseRate", DslPurchaseRate);
            //objQuery.AddToQuery("MasterID", DSLMasterID);
            //objQuery.AddToQuery("ShortListDate", createddate);
            //objQuery.AddToQuery("IfSave", DSLIfSave);
            //objQuery.AddToQuery("CreatedUserID", createdby);
            //objQuery.AddToQuery("CreatedDate", createddate);
            //objQuery.AddToQuery("CreatedTime", createdtime);
            //objQuery.AddToQuery("NetRate", netrate);
            return objQuery.InsertQuery();
        }
        public bool UpdatePurchaseOrderNumberIndetailsale(int dSLOrderNumber, string fromDay, string endDay)
        {
            bool bRetValue = false;
            string strSql = "Update detailsale set ponumber = " + dSLOrderNumber  + " where voucherdate >= '" + fromDay  + "' and  voucherdate <= '" + endDay + "'";

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public DataTable ReadShotListByDateALLTypes(string fromDay, string endDay)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSql = "Select a.DSLID,a.ProductID,a.stockistOrderNumber,a.stockistOrderDate,a.ShortListDate,a.stockistOrderQuantity as Quantity ,a.IfSave  from" +
        " detailpurchaseorderstockist a  where a.ShortListDate >= " + "'" + fromDay + "'  AND a.ShortListDate <= '" + endDay + "' AND a.stockistOrderNumber = 0 ";
            dt1 = DBInterface.SelectDataTable(strSql);
            strSql = "Select b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
     "COALESCE(b.ProdPartyId_1),COALESCE(b.ProdPartyId_2),b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName as AccName,c.AccCrVisitDays as AccCrVisitDays,c.AccAddress1,c.AccAddress2 " +
    " from masterproduct b  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID where b.ProdMaxLevel >= b.ProdClosingStock  order by ProdClosingStock asc";
            dt2 = DBInterface.SelectDataTable(strSql);
            DataTable dtable = new DataTable();
            dtable = GetMergedProductStockIntegerQuantity(dt2, dt1);
            return dtable;
        }

        public DataTable ReadShotListByDateNextVisit(string fromDay, string endDay, string FromDayNextVisit, string EndDayNextVisit)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            //    string strSql = "Select a.DSLID,a.ProductID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity as Quantity ,a.IfSave  from" +
            //" detailpurchaseorder a  where a.ShortListDate >= " + "'" + fromDay + "'  AND a.ShortListDate <= '" + endDay + "' AND a.OrderNumber = 0 ";
            string strSql = "Select d.ProductID, Cast((d.Quantity) AS DECIMAL(2)) As Quantity from vouchersale v inner join detailsale d on v.ID = d.MasterSaleID where NextVisitDate between '" + FromDayNextVisit + "' and '" + EndDayNextVisit + "'";
            dt1 = DBInterface.SelectDataTable(strSql);

            strSql = "Select b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
   "COALESCE(b.ProdPartyId_1),COALESCE(b.ProdPartyId_2),b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName as AccName,c.AccCrVisitDays as AccCrVisitDays,c.AccAddress1,c.AccAddress2 " +
  " from masterproduct b  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID   order by ProdClosingStock asc";
            dt2 = DBInterface.SelectDataTable(strSql);


            //dt3 = DBInterface.SelectDataTable(strSql);

            DataTable dtable = new DataTable();
            dtable = GetMergedProductStockDecimalQuantity(dt2, dt1);


            return dtable;
        }

        public DataTable ReadShotListByDateForTodayByAccountID(string fromday, string today, string accountID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.ProductID, sum(a.Quantity) as Quantity , e.VoucherDate, b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
    "COALESCE(b.ProdPartyId_1),COALESCE(b.ProdPartyId_2),b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName as AccName,c.AccCrVisitDays as AccCrVisitDays,c.AccAddress1,c.AccAddress2, '' as DLSID from  detailsale a " +
   " inner join vouchersale e on a.MasterSaleID = e.ID inner join masterproduct b on a.ProductID = b.ProductID  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID   where  e.voucherdate >= '" + fromday + "' AND e.voucherdate <= '" + today + "' AND c.AccountID = '" + accountID + "'  Group by a.ProductID  order by b.ProdClosingStock asc";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadListForTodayALLTypes(string fromday, string today)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSql = "Select distinct a.ProductID, sum(a.Quantity) as Quantity from  detailsale a " +
   "  where  a.voucherdate >= '" + fromday + "' AND a.voucherdate <= '" + today + "'  Group by a.ProductID ";

            dt1 = DBInterface.SelectDataTable(strSql);


            strSql = "Select b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
     " b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName as AccName,c.AccCrVisitDays as AccCrVisitDays,c.AccAddress1,c.AccAddress2 " +
    " from masterproduct b  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID   order by ProdClosingStock asc";
            strSql = "Select b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
    "b.ProdPartyId_1, b.ProdPartyId_2, b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName as AccName,c.AccCrVisitDays as AccCrVisitDays,c.AccAddress1,c.AccAddress2 " +
   " from masterproduct b  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID   order by ProdClosingStock asc";
            dt2 = DBInterface.SelectDataTable(strSql);

            DataTable dtable = new DataTable();
            dtable = GetMergedProductStockDecimalQuantity(dt2, dt1);


            return dtable;
        }
        public DataTable ReadOrderByNumber(int firstnumber) // added schemequantity in query [ansuman] // added AccTelephone[01.06.2017SS]
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.stockistOrderNumber,a.stockistOrderDate,a.ShortListDate,a.stockistOrderQuantity,a.IfSave,a.stockistAccountID,a.stockistSchemeQuantity,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock, b.ProdLastPurchaseRate,0 as OrderQuantity1, c.stockistAccountID , c.AccName ,c.AccAddress1,c.AccAddress2,c.AccountID as AccountID1, c.AccName as AccName1,c.AccAddress1,c.AccountID as AccountID2,c.AccName as AccName2, c.AccTelephone  from detailpurchaseorderstockist a " +
         " inner join masterproduct b on a.ProductID = b.ProductID  left outer join masteraccount c on a.AccountID = c.AccountID  where a.stockistOrderNumber >= " + firstnumber + "  Order by a.stockistOrderNumber ";

            //       string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,e.AccName,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
            // "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock, b.ProdLastPurchaseRate,0 as OrderQuantity1, c.AccountId,c.AccName,c.AccAddress1,c.AccAddress2,c.AccountID as AccountID1, c.AccName as AccName1,c.AccAddress1,c.AccAddress2,d.AccountID as AccountID2,d.AccName as AccName2  from detailpurchaseorder a " +
            //" inner join masterproduct b on a.ProductID = b.ProductID  left outer join masteraccount e on a.AccountID = e.AccountID  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID  left outer join masteraccount d on b.ProdPartyId_2 = d.AccountID where a.OrderNumber >= " + firstnumber + "  Order by a.OrderNumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public bool SaveOrder(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, string DSLdailyshortlist, int dslSchemeQuantity)
        {

            bool bRetValue = false;
            string strSql = GetUpdateQuerySaveOrder(DSLID, DSLAccountID, DSLQty, DSLIfSave, DSLdailyshortlist, dslSchemeQuantity);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetUpdateQuerySaveOrder(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, string DSLdailyshortlist, int dslSchemeQuantity)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailpurchaseorderstockist";
            objQuery.AddToQuery("DSLID", DSLID, true);
            objQuery.AddToQuery("AccountID", DSLAccountID);
            objQuery.AddToQuery("OrderQuantity", DSLQty);
            objQuery.AddToQuery("SchemeQuantity", dslSchemeQuantity);
            objQuery.AddToQuery("IfSave", DSLIfSave);
            objQuery.AddToQuery("IfDailyShortList", DSLdailyshortlist);
            return objQuery.UpdateQuery();
        }


        public bool SaveOrderForToday(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, string DSLdailyshortlist)
        {

            bool bRetValue = false;
            string strSql = GetInsertQuerySaveOrderForToday(DSLID, DSLAccountID, DSLQty, DSLIfSave, DSLdailyshortlist);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetInsertQuerySaveOrderForToday(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, string DSLdailyshortlist)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailpurchaseorderstockist";
            objQuery.AddToQuery("DSLID", DSLID);
            objQuery.AddToQuery("AccountID", DSLAccountID);
            objQuery.AddToQuery(" OrderQuantity", DSLQty);
            objQuery.AddToQuery("IfSave", DSLIfSave);
            objQuery.AddToQuery("IfDailyShortList", DSLdailyshortlist);
            return objQuery.InsertQuery();
        }



        //public bool CreateOrder(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, int DSLOrderNumber, string TodayS, string DSLDailyshortlist, double DSLPurchaseRate, string DSLMasterID, int dslSchemeQuantity, string createdby, string createddate, string createdtime)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetUpdateQuery(DSLID, DSLAccountID, DSLQty, DSLIfSave, DSLOrderNumber, TodayS, DSLDailyshortlist, DSLPurchaseRate, DSLMasterID, dslSchemeQuantity, createdby, createddate, createdtime);

        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }
        //    return bRetValue;
        //}

        public int AddInDetailPurchaseOrder(string DSLAccountID, int DSLQty, string DSLIfSave, int DSLOrderNumber, string TodayS, string DSLDailyshortlist, double DSLPurchaseRate, string DSLMasterID, string dslProductID, int dslSchemeQuantity, string createdby, string createddate, string createdtime, string netrate, int masterid, int msaleqty, int mclosingstk)
        {
            string strSql = GetInsertQueryForDetail(DSLAccountID, DSLQty, DSLIfSave, DSLOrderNumber, TodayS, DSLDailyshortlist, DSLPurchaseRate, DSLMasterID, dslProductID, dslSchemeQuantity, createdby, createddate, createdtime, netrate,masterid,msaleqty,mclosingstk);
                        return DBInterface.ExecuteQuery(strSql);            
        }
        public int AddDetails(string VoucherSeries, string DSLVoucherType, int DSLOrderNumber, string ToDays, string DSLAccountID, double DSLAmount, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            int ii = 0;
            string strSql = GetInsertQuery(VoucherSeries, DSLVoucherType, DSLOrderNumber, ToDays, DSLAccountID, DSLAmount, CreatedBy, CreatedDate, CreatedTime);
            ii = Convert.ToInt32(DBInterface.ExecuteScalar(strSql));
            return ii;
        }
        private string GetUpdateQuery(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, int DSLOrderNumber, string TodayS, string DSLDailyshortlist, double DslPurchaseRate, string DSLMasterID, int dslSchemeQuantity, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailpurchaseorderstockist";
            objQuery.AddToQuery("DSLID", DSLID, true);
            objQuery.AddToQuery("stockistAccountID", DSLAccountID);
            objQuery.AddToQuery("stockistOrderQuantity", DSLQty);
            objQuery.AddToQuery("stockistSchemeQuantity", dslSchemeQuantity);
            objQuery.AddToQuery("IfSave", DSLIfSave);
            objQuery.AddToQuery("stockistOrderNumber", DSLOrderNumber);
            objQuery.AddToQuery("stockistOrderDate", TodayS);
            objQuery.AddToQuery("IfDailyShortList", DSLDailyshortlist);
            objQuery.AddToQuery("PurchaseRate", DslPurchaseRate);
            objQuery.AddToQuery("MasterID", DSLMasterID);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.UpdateQuery();
        }

        private string GetInsertQueryForDetail(string DSLAccountID, int DSLQty, string DSLIfSave, int DSLOrderNumber, string TodayS, string DSLDailyshortlist, double DslPurchaseRate, string DSLMasterID, string dslProductID, int dslSchemeQuantity, string createdby, string createddate, string createdtime, string netrate, int masterid, int msaleqty, int mclosingstk)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailpurchaseorderstockist";
            //objQuery.AddToQuery("DSLID", DSLID);
            objQuery.AddToQuery("EcoMartID", General.EcoMartLicense.EcoMartInfo.ShopID);
            objQuery.AddToQuery("CNFID", General.EcoMartLicense.CNFInfo.ShopID);
            objQuery.AddToQuery("StockistID", General.EcoMartLicense.ShopID);            
            objQuery.AddToQuery("ProductID", dslProductID);
            objQuery.AddToQuery("stockistAccountID", DSLAccountID);
            objQuery.AddToQuery("stockistOrderQuantity", DSLQty);
            objQuery.AddToQuery("stockistSchemeQuantity", dslSchemeQuantity);
            objQuery.AddToQuery("StockistSaleQuantity", msaleqty);
            objQuery.AddToQuery("StockistClosingStock", mclosingstk);
            objQuery.AddToQuery("IfSave", DSLIfSave);
            objQuery.AddToQuery("stockistOrderNumber", DSLOrderNumber);
            objQuery.AddToQuery("stockistOrderDate", TodayS);
            objQuery.AddToQuery("IfDailyShortList", DSLDailyshortlist);
            objQuery.AddToQuery("PurchaseRate", DslPurchaseRate);
            objQuery.AddToQuery("MasterID", DSLMasterID);
            objQuery.AddToQuery("ShortListDate", createddate);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            //objQuery.AddToQuery("NetRate", netrate);
            return objQuery.InsertQuery();
        }

        private string GetInsertQuery(string VoucherSeries, string DSLVoucherType, int DSLOrderNumber, string TodayS, string DSLAccountID, double DSLAmount, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterpurchaseorderstockist";
            //objQuery.AddToQuery("ID", DSLMasterID);
            objQuery.AddToQuery("VoucherSeries", VoucherSeries);
            objQuery.AddToQuery("VoucherType", DSLVoucherType);
            objQuery.AddToQuery("VoucherNumber", DSLOrderNumber);
            objQuery.AddToQuery("VoucherDate", TodayS);
            objQuery.AddToQuery("AccountID", DSLAccountID);
            objQuery.AddToQuery("Amount", DSLAmount);
            objQuery.AddToQuery("Narration", "");
            objQuery.AddToQuery("CreatedUserID", CreatedBy);
            objQuery.AddToQuery("CreatedDate", CreatedDate);
            objQuery.AddToQuery("CreatedTime", CreatedTime);
            return objQuery.InsertQuery();
        }

        public DataTable ReadShotListByDateForTodayByAccountID(string accountID)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.stockistOrderNumber,a.stockistOrderDate,a.ShortListDate,a.stockistOrderQuantity,a.IfSave,a.stockistAccountID,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,a.PurchaseRate as ProdLastPurchaseRate,0 as Quantity,c.AccountId,c.AccName,c.AccountID from detailpurchaseorderstockist a " +
         " inner join masterproduct b on a.ProductID = b.ProductID  inner join masteraccount c on a.AccountID = c.AccountID where a.AccountID = '" + accountID + "' order by a.stockistOrderNumber desc";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable ReadLastOrderRemainingProducts(string accountID)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.stockistOrderNumber,a.stockistOrderDate,a.ShortListDate,(a.stockistOrderQuantity - a.PurchaseQuantity) as OrderQuantity,a.IfSave,a.stockistAccountID,a.PurchaseQuantity,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,a.PurchaseRate as ProdLastPurchaseRate,0 as Quantity,c.AccountId,c.AccName,c.AccountID from detailpurchaseorderstockist a " +
         " inner join masterproduct b on a.ProductID = b.ProductID  inner join masteraccount c on a.AccountID = c.AccountID where a.AccountID = '" + accountID + "' AND a.purchaseQuantity < a.OrderQuantity order by a.stockistOrderNumber desc";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadLastOrderRemainingProductsAllTypes()
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.stockistOrderNumber,a.stockistOrderDate,a.ShortListDate,(a.stockistOrderQuantity - a.PurchaseQuantity) as OrderQuantity,a.IfSave,a.stockistAccountID,a.PurchaseQuantity,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,a.PurchaseRate as ProdLastPurchaseRate,ifnull(((a.OrderQuantity - a.PurchaseQuantity) * b.ProdLoosePack),0) as Quantity,c.AccountId,c.AccName,c.AccountID from detailpurchaseorderstockist a " +
         " inner join masterproduct b on a.ProductID = b.ProductID  left outer join masteraccount c on a.AccountID = c.AccountID where (purchaseQuantity is null OR a.purchaseQuantity < a.OrderQuantity ) AND a.OrderQuantity > 0 order by a.stockistOrderNumber desc";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}
