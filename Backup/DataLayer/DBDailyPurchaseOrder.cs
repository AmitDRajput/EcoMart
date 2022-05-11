using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PharmaSYSRetailPlus.DataLayer
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
            string strsql = "Select a.OrderNumber,a.OrderDate,a.AccountID,sum(a.OrderQuantity*a.PurchaseRate) as Amount ,b.AccountID,b.AccName from tbldailyshortlist a inner join masteraccount b where a.AccountId = b.AccountID && a.OrderNumber >= " + FirstOrderNumber + " Group by a.OrderNumber";
            dtable = DBInterface.SelectDataTable(strsql);
            return dtable;
        }
        public DataTable ReadShotListByDate(string fromday, string today)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,e.AccName,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel - b.ProdClosingStock as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName,c.AccountID as AccountID1, c.AccName as AccName1,c.AccAddress1,c.AccAddress2, d.AccountID as AccountID2,d.AccName as AccName2  from tbldailyshortlist a " +
         " inner join masterproduct b on a.ProductId = b.ProductID  left outer join masteraccount e on a.AccountID = e.AccountID  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID  left outer join masteraccount d on b.ProdPartyId_2 = d.AccountID where a.ShortListDate >= " + "'" + fromday + "'  && a.ShortListDate <= '" + today + "' && a.OrderNumber = 0 && b.ProdMaxLevel >= b.ProdClosingStock";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadShotListByDateForToday(string fromday, string today, int dayofweek)
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
     "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel - b.ProdClosingStock as OrderQuantity ,b.ProdLastPurchaseRate,c.AccountId,c.AccName as AccName,c.AccCrVisitDays as AccCrVisitDays,c.AccAddress1,c.AccAddress2 from tbldailyshortlist a " +
    " inner join masterproduct b on a.ProductId = b.ProductID  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID  where a.ShortListDate >= " + "'" + fromday + "'  && a.ShortListDate <= '" + today + "' && a.OrderNumber = 0 && b.ProdMaxLevel >= b.ProdClosingStock && (Instr(c.AccCrVisitDays," + dayofweek + ") > 0 ) Order by c.AccName";


         //   string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
         // "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel - b.ProdClosingStock as OrderQuantity ,b.ProdLastPurchaseRate,c.AccName,c.AccCrVisitDays as AccCrVisitDays from tbldailyshortlist a " +
         //" inner join masterproduct b on a.ProductId = b.ProductID  inner join masteraccount c on a.AccountID = c.AccountID where a.ShortListDate >= " + "'" + fromday + "'  && a.ShortListDate <= '" + today + "' && a.OrderNumber = 0 && Instr(c.AccCrVisitDays,"+ dayofweek +") > 0 ";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable ReadOrderByNumber(int firstnumber )
        {
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock, b.ProdLastPurchaseRate,0 as OrderQuantity1, c.AccountID , c.AccName ,c.AccAddress1,c.AccAddress2,c.AccountID as AccountID1, c.AccName as AccName1,c.AccAddress1,c.AccountID as AccountID2,c.AccName as AccName2  from tbldailyshortlist a " +
         " inner join masterproduct b on a.ProductId = b.ProductID  left outer join masteraccount c on a.AccountID = c.AccountID  where a.OrderNumber >= " + firstnumber + "  Order by a.OrderNumber ";

     //       string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,e.AccName,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
     // "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock, b.ProdLastPurchaseRate,0 as OrderQuantity1, c.AccountId,c.AccName,c.AccAddress1,c.AccAddress2,c.AccountID as AccountID1, c.AccName as AccName1,c.AccAddress1,c.AccAddress2,d.AccountID as AccountID2,d.AccName as AccName2  from tbldailyshortlist a " +
     //" inner join masterproduct b on a.ProductId = b.ProductID  left outer join masteraccount e on a.AccountID = e.AccountID  left outer join masteraccount c on b.ProdPartyId_1 = c.AccountID  left outer join masteraccount d on b.ProdPartyId_2 = d.AccountID where a.OrderNumber >= " + firstnumber + "  Order by a.OrderNumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public bool SaveOrder(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, string DSLdailyshortlist)
        {

            bool bRetValue = false;
            string strSql = GetUpdateQuerySaveOrder(DSLID, DSLAccountID, DSLQty, DSLIfSave, DSLdailyshortlist);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetUpdateQuerySaveOrder(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, string DSLdailyshortlist)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbldailyshortlist";
            objQuery.AddToQuery("DSLID", DSLID, true);
            objQuery.AddToQuery("AccountID", DSLAccountID);
            objQuery.AddToQuery(" OrderQuantity", DSLQty);
            objQuery.AddToQuery("IfSave", DSLIfSave);
            objQuery.AddToQuery("IfDailyShortList", DSLdailyshortlist);
            return objQuery.UpdateQuery();
        }

        public bool CreateOrder(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, int DSLOrderNumber, string TodayS, string DSLDailyshortlist, double DSLPurchaseRate, string DSLMasterID, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(DSLID, DSLAccountID, DSLQty, DSLIfSave, DSLOrderNumber, TodayS, DSLDailyshortlist, DSLPurchaseRate, DSLMasterID, createdby, createddate, createdtime);

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
        private string GetUpdateQuery(string DSLID, string DSLAccountID, int DSLQty, string DSLIfSave, int DSLOrderNumber, string TodayS, string DSLDailyshortlist, double DslPurchaseRate, string DSLMasterID, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbldailyshortlist";
            objQuery.AddToQuery("DSLID", DSLID, true);
            objQuery.AddToQuery("AccountID", DSLAccountID);
            objQuery.AddToQuery("OrderQuantity", DSLQty);
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
    }
}
