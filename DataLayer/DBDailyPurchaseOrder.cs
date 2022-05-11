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
            string strsql = "Select a.OrderNumber,a.OrderDate,a.OrderQuantity,a.ProductID,(a.OrderQuantity*a.PurchaseRate) as Amount ,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName from tbldailyshortlist a inner join masterproduct b where a.ProductId = b.ProductID && a.OrderNumber = " + ordernumber;
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }

        internal DataTable GetOverviewSummaryData(int FirstOrderNumber)
        {
            DataTable dtable = new DataTable();
            string strsql = "Select a.OrderNumber,a.OrderDate,a.AccountID,sum(a.OrderQuantity*a.PurchaseRate) as Amount ,b.AccountID,b.AccName,b.AccTelephone,b.MobileNumberForSMS from tbldailyshortlist a inner join masteraccount b where a.AccountId = b.AccountID && a.OrderNumber >= " + FirstOrderNumber + " Group by a.OrderNumber";
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable ReadShotListByDate(string fromday, string today)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.SchemeQuantity,a.IfSave,a.AccountID,e.AccName,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel - b.ProdClosingStock as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName,c.AccountID as AccountID1, c.AccName as AccName1,c.AccAddress1,c.AccAddress2, d.AccountID as AccountID2,d.AccName as AccName2  from tbldailyshortlist a " +
         " inner join masterproduct b on a.ProductId = b.ProductID  left outer join masteraccount e on a.AccountID = e.AccountID  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID  left outer join masteraccount d on b.ProdPartyId_2 = d.AccountID where a.ShortListDate >= " + "'" + fromday + "'  && a.ShortListDate <= '" + today + "' && a.OrderNumber = 0 && b.ProdMaxLevel >= b.ProdClosingStock";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadShotListByDateForToday(string fromday, string today)
        {

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSql = "Select distinct a.ProductID, sum(a.Quantity) as Quantity ,a.Voucherdate from  detailsale a " +
   "  where  a.voucherdate >= '" + fromday + "' && a.voucherdate <= '" + today + "'  Group by a.ProductID ";

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
                                  on p.Field<string>("ProductID") equals t.Field<string>("ProductID")
                                  select new
                                  {
                                      AccountID = p.Field<string>("AccountID"),
                                      AccName = p.Field<string>("AccName"),
                                      ProductID = p.Field<string>("ProductID"),
                                      ProdName = p.Field<string>("ProdName"),
                                      ProdPack = p.Field<string>("ProdPack"),
                                      //  ProdPackType = p.Field<string>("ProdPackType"),
                                      ProdLoosePack = p.Field<UInt32?>("ProdLoosePack"),
                                      //   ProdClosingStockPack = p.Field<Int64>("ProdClosingStockPack"),
                                      ProdCompShortName = p.Field<string>("ProdCompShortName"),
                                      ProdBoxQuantity = p.Field<UInt32?>("ProdBoxQuantity"),
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

                                      ProdMaxLevel = p.Field<UInt32?>("ProdMaxLevel"),
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
                                      ProdClosingStock = p.Field<Int32?>("ProdClosingStock"),
                                      Quantity = t.Field<decimal>("Quantity"),
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

        public DataTable ReadShotListByDateALLTypes(string fromDay, string endDay)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();            
            string strSql = "Select a.DSLID,a.ProductID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity as Quantity ,a.IfSave  from" +
        " tbldailyshortlist a  where a.ShortListDate >= " + "'" + fromDay + "'  && a.ShortListDate <= '" + endDay + "' && a.OrderNumber = 0 ";
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
            //" tbldailyshortlist a  where a.ShortListDate >= " + "'" + fromDay + "'  && a.ShortListDate <= '" + endDay + "' && a.OrderNumber = 0 ";
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
   " inner join vouchersale e on a.MasterSaleID = e.ID inner join masterproduct b on a.ProductId = b.ProductID  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID   where  e.voucherdate >= '" + fromday + "' && e.voucherdate <= '" + today + "' && c.AccountID = '" + accountID + "'  Group by a.ProductID  order by b.ProdClosingStock asc";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadListForTodayALLTypes(string fromday, string today)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string strSql = "Select distinct a.ProductID, sum(a.Quantity) as Quantity ,a.Voucherdate from  detailsale a " +
   "  where  a.voucherdate >= '" + fromday + "' && a.voucherdate <= '" + today + "'  Group by a.ProductID ";

            dt1 = DBInterface.SelectDataTable(strSql);


            strSql = "Select b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
     "COALESCE(b.ProdPartyId_1),COALESCE(b.ProdPartyId_2),b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName as AccName,c.AccCrVisitDays as AccCrVisitDays,c.AccAddress1,c.AccAddress2 " +
    " from masterproduct b  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID   order by ProdClosingStock asc";

            dt2 = DBInterface.SelectDataTable(strSql);

            DataTable dtable = new DataTable();
            dtable = GetMergedProductStockDecimalQuantity(dt2, dt1);


            return dtable;
        }
        public DataTable ReadOrderByNumber(int firstnumber) // added schemequantity in query [ansuman] // added AccTelephone[01.06.2017SS]
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,a.SchemeQuantity,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock, b.ProdLastPurchaseRate,0 as OrderQuantity1, c.AccountID , c.AccName ,c.AccAddress1,c.AccAddress2,c.AccountID as AccountID1, c.AccName as AccName1,c.AccAddress1,c.AccountID as AccountID2,c.AccName as AccName2, c.AccTelephone  from tbldailyshortlist a " +
         " inner join masterproduct b on a.ProductId = b.ProductID  left outer join masteraccount c on a.AccountID = c.AccountID  where a.OrderNumber >= " + firstnumber + "  Order by a.OrderNumber ";

            //       string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,e.AccName,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
            // "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock, b.ProdLastPurchaseRate,0 as OrderQuantity1, c.AccountId,c.AccName,c.AccAddress1,c.AccAddress2,c.AccountID as AccountID1, c.AccName as AccName1,c.AccAddress1,c.AccAddress2,d.AccountID as AccountID2,d.AccName as AccName2  from tbldailyshortlist a " +
            //" inner join masterproduct b on a.ProductId = b.ProductID  left outer join masteraccount e on a.AccountID = e.AccountID  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID  left outer join masteraccount d on b.ProdPartyId_2 = d.AccountID where a.OrderNumber >= " + firstnumber + "  Order by a.OrderNumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public bool SaveOrder(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, string DSLdailyshortlist, int dslSchemeQuantity)
        {

            bool bRetValue = false;
            string strSql = GetUpdateQuerySaveOrder(DSLID, DSLAccountID, DSLQty, DSLIfSave, DSLdailyshortlist,dslSchemeQuantity);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetUpdateQuerySaveOrder(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, string DSLdailyshortlist, int dslSchemeQuantity)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbldailyshortlist";
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
            objQuery.Table = "tbldailyshortlist";
            objQuery.AddToQuery("DSLID", DSLID);
            objQuery.AddToQuery("AccountID", DSLAccountID);
            objQuery.AddToQuery(" OrderQuantity", DSLQty);
            objQuery.AddToQuery("IfSave", DSLIfSave);
            objQuery.AddToQuery("IfDailyShortList", DSLdailyshortlist);
            return objQuery.InsertQuery();
        }



        public bool CreateOrder(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, int DSLOrderNumber, string TodayS, string DSLDailyshortlist, double DSLPurchaseRate, string DSLMasterID, int dslSchemeQuantity, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(DSLID, DSLAccountID, DSLQty, DSLIfSave, DSLOrderNumber, TodayS, DSLDailyshortlist, DSLPurchaseRate, DSLMasterID, dslSchemeQuantity, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool CreateOrderForToday(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, int DSLOrderNumber, string TodayS, string DSLDailyshortlist, double DSLPurchaseRate, string DSLMasterID, string dslProductID, int dslSchemeQuantity, string createdby, string createddate, string createdtime, string netrate)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(DSLID, DSLAccountID, DSLQty, DSLIfSave, DSLOrderNumber, TodayS, DSLDailyshortlist, DSLPurchaseRate, DSLMasterID, dslProductID, dslSchemeQuantity, createdby, createddate, createdtime, netrate);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool AddDetails(string DSLMasterID, string VoucherSeries, string DSLVoucherType, int DSLOrderNumber, string ToDays, string DSLAccountID, double DSLAmount, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(DSLMasterID, VoucherSeries, DSLVoucherType, DSLOrderNumber, ToDays, DSLAccountID, DSLAmount, CreatedBy, CreatedDate, CreatedTime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetUpdateQuery(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, int DSLOrderNumber, string TodayS, string DSLDailyshortlist, double DslPurchaseRate, string DSLMasterID,int dslSchemeQuantity, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbldailyshortlist";
            objQuery.AddToQuery("DSLID", DSLID, true);
            objQuery.AddToQuery("AccountID", DSLAccountID);
            objQuery.AddToQuery("OrderQuantity", DSLQty);
            objQuery.AddToQuery("SchemeQuantity", dslSchemeQuantity);
            objQuery.AddToQuery("IfSave", DSLIfSave);
            objQuery.AddToQuery("OrderNumber", DSLOrderNumber);
            objQuery.AddToQuery("OrderDate", TodayS);
            objQuery.AddToQuery("IfDailyShortList", DSLDailyshortlist);
            objQuery.AddToQuery("PurchaseRate", DslPurchaseRate);
            objQuery.AddToQuery("MasterID", DSLMasterID);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.UpdateQuery();
        }

        private string GetInsertQuery(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, int DSLOrderNumber, string TodayS, string DSLDailyshortlist, double DslPurchaseRate, string DSLMasterID, string dslProductID, int dslSchemeQuantity, string createdby, string createddate, string createdtime, string netrate)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbldailyshortlist";
            objQuery.AddToQuery("DSLID", DSLID);
            objQuery.AddToQuery("ProductID", dslProductID);
            objQuery.AddToQuery("AccountID", DSLAccountID);
            objQuery.AddToQuery("OrderQuantity", DSLQty);
            objQuery.AddToQuery("SchemeQuantity", dslSchemeQuantity);
            objQuery.AddToQuery("IfSave", DSLIfSave);
            objQuery.AddToQuery("OrderNumber", DSLOrderNumber);
            objQuery.AddToQuery("OrderDate", TodayS);
            objQuery.AddToQuery("IfDailyShortList", DSLDailyshortlist);
            objQuery.AddToQuery("PurchaseRate", DslPurchaseRate);
            objQuery.AddToQuery("MasterID", DSLMasterID);
            objQuery.AddToQuery("ShortListDate", createddate);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            objQuery.AddToQuery("NetRate", netrate);
            return objQuery.InsertQuery();
        }

        private string GetInsertQuery(string DSLMasterID, string VoucherSeries, string DSLVoucherType, int DSLOrderNumber, string TodayS, string DSLAccountID, double DSLAmount, string CreatedBy, string CreatedDate, string CreatedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "masterorder";
            objQuery.AddToQuery("ID", DSLMasterID);
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

            string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,a.PurchaseRate as ProdLastPurchaseRate,0 as Quantity,c.AccountId,c.AccName,c.AccountID from tbldailyshortlist a " +
         " inner join masterproduct b on a.ProductId = b.ProductID  inner join masteraccount c on a.AccountID = c.AccountID where a.AccountID = '" + accountID + "' order by a.OrderNumber desc";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable ReadLastOrderRemainingProducts(string accountID)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,(a.OrderQuantity - a.PurchaseQuantity) as OrderQuantity,a.IfSave,a.AccountID,a.PurchaseQuantity,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,a.PurchaseRate as ProdLastPurchaseRate,0 as Quantity,c.AccountId,c.AccName,c.AccountID from tbldailyshortlist a " +
         " inner join masterproduct b on a.ProductId = b.ProductID  inner join masteraccount c on a.AccountID = c.AccountID where a.AccountID = '" + accountID + "' && a.purchaseQuantity < a.OrderQuantity order by a.OrderNumber desc";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadLastOrderRemainingProductsAllTypes()
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,(a.OrderQuantity - a.PurchaseQuantity) as OrderQuantity,a.IfSave,a.AccountID,a.PurchaseQuantity,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,a.PurchaseRate as ProdLastPurchaseRate,ifnull(((a.OrderQuantity - a.PurchaseQuantity) * b.ProdLoosePack),0) as Quantity,c.AccountId,c.AccName,c.AccountID from tbldailyshortlist a " +
         " inner join masterproduct b on a.ProductId = b.ProductID  left outer join masteraccount c on a.AccountID = c.AccountID where (purchaseQuantity is null || a.purchaseQuantity < a.OrderQuantity ) && a.OrderQuantity > 0 order by a.OrderNumber desc";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}
