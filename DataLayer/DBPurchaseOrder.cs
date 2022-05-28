using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBPurchaseOrder
    {
        public DBPurchaseOrder()
        {
        }

        public bool AddProductDetails(string shortlistid, string Id, int ProductID, int Quantity, double schemeQuantity, string AccountID, double PurchaseRate, int OrderNumber, string OrderDate, string IFSAVE, string createdby, string createddate, string createdtime)
        {

            bool bRetValue = false;
            string strSql = GetInsertProductQuery(shortlistid, Id, ProductID, Quantity,schemeQuantity, AccountID, PurchaseRate, OrderNumber, OrderDate, IFSAVE, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddProductDetailsModified(string shortlistid, string Id, int ProductID, int Quantity, double schemeQuantity, string AccountID, double PurchaseRate, int OrderNumber, string OrderDate, string IFSAVE,string shortlistdate, string ifdailyshortlist, string DSLCreatedBy,string DSLCreatedDate,string DSLCreatedTime ,string ModifiedBy,string ModifiedDate,string ModifiedTime)                                                                                                                     
        {
            bool bRetValue = false;
            string strSql = GetInsertProductQueryModified(shortlistid, Id, ProductID, Quantity, schemeQuantity,  AccountID, PurchaseRate, OrderNumber, OrderDate, IFSAVE, shortlistdate,ifdailyshortlist, DSLCreatedBy,DSLCreatedDate,DSLCreatedTime,ModifiedBy,ModifiedDate,ModifiedTime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetails(string Id, string AccountID, string Narration, double Amount,string VoucherType, int VoucherNumber, string VoucherDate, string VoucherSeries, string createdby, string createddate, string createdtime)
        {
            {
                bool bRetValue = false;
                string strSql = GetInsertQuery(Id, AccountID, Narration, Amount,VoucherType, VoucherNumber, VoucherDate,VoucherSeries, createdby, createddate, createdtime);
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
                return bRetValue;
            }
        }
        public bool UpdateDetails(string Id, string AccountID, string Narration, double Amount, string VoucherDate, string modifiedby, string modifieddate, string modifiedtime)
        {
            {
                bool bRetValue = false;
                string strSql = GetUpdateQuery(Id, AccountID, Narration, Amount, VoucherDate, modifiedby, modifieddate, modifiedtime);
                if (DBInterface.ExecuteQuery(strSql) > 0)
                {
                    bRetValue = true;
                }
                return bRetValue;
            }
        }
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
        public bool DeletePreviousProducts(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQueryProducts(Id);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        #region Query Building Functions

        private string GetInsertQuery(string Id, string CreditorId, string Narration,double AmountNet, string VouType, int VouNo,
             string VouDate,string VoucherSeries, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "Masterorder";
            objQuery.AddToQuery("ID", Id);
            objQuery.AddToQuery("VoucherSeries", VoucherSeries);  
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("AccountID", CreditorId);
            objQuery.AddToQuery("Amount", AmountNet);
            objQuery.AddToQuery("Narration", Narration);          
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertProductQuery(string shortlistid, string Id, int ProductID, int Quantity, double schemeQuantity, string AccountID, double PurchaseRate, int OrderNumber, string OrderDate, string IFSAVE, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbldailyshortlist";
            objQuery.AddToQuery("DSLID", shortlistid);
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("OrderQuantity", Quantity);
            objQuery.AddToQuery("SchemeQuantity", schemeQuantity);
            objQuery.AddToQuery("AccountID", AccountID);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("OrderNumber", OrderNumber);
            objQuery.AddToQuery("OrderDate", OrderDate);
            objQuery.AddToQuery("IfSave", IFSAVE);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string CreditorId, string Narration, double AmountNet,
             string VouDate, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "Masterorder";
            objQuery.AddToQuery("ID", Id,true); 
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("AccountID", CreditorId);
            objQuery.AddToQuery("Amount", AmountNet);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("modifiedUserID",modifiedby);
            objQuery.AddToQuery("modifiedDate", modifieddate);
            objQuery.AddToQuery("modifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "Masterorder";
            objQuery.AddToQuery("ID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }
             

        private string GetDeleteQueryProducts(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "tbldailyshortlist";
            objQuery.AddToQuery("MasterID", Id, true);           
            strSql = objQuery.DeleteQuery();
            return strSql;
        }

        private string GetInsertProductQueryModified(string shortlistid, string Id, int ProductID, int Quantity, double schemeQuantity, string AccountID, double PurchaseRate, int OrderNumber, string OrderDate, string IFSAVE, string shortlistdate, string ifdailyshortlist, string DSLCreatedBy, string DSLCreatedDate, string DSLCreatedTime, string ModifiedBy, string ModifiedDate, string ModifiedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbldailyshortlist";
            objQuery.AddToQuery("DSLID", shortlistid);
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("OrderQuantity", Quantity);
            objQuery.AddToQuery("SchemeQuantity", schemeQuantity);
            objQuery.AddToQuery("AccountID", AccountID);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("OrderNumber", OrderNumber);
            objQuery.AddToQuery("OrderDate", OrderDate);
            objQuery.AddToQuery("IfSave", IFSAVE);
            objQuery.AddToQuery("ShortListDate", shortlistdate);
            objQuery.AddToQuery("IfDailyShortList",ifdailyshortlist);
            objQuery.AddToQuery("CreatedUserID", DSLCreatedBy);
            objQuery.AddToQuery("CreatedDate", DSLCreatedDate);
            objQuery.AddToQuery("CreatedTime", DSLCreatedTime);
            objQuery.AddToQuery("ModifiedUserID", ModifiedBy);
            objQuery.AddToQuery("ModifiedDate", ModifiedDate);
            objQuery.AddToQuery("ModifiedTime", ModifiedTime);
            return objQuery.InsertQuery();
        }
        #endregion private methods

        public  DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.ID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.Amount, " +
                            "a.AccountID,a.Narration, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from masterorder a, masteraccount b " +
                            "where a.AccountId = b.AccountId &&  a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' && a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from Masterorder where ID ='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByVoucherNumber(int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select * from Masterorder where VoucherNumber = {0} ";
                strSql = string.Format(strSql, vouno);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataTable ReadProductDetailsByID(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select distinct a.DSLID, a.MasterID, a.ProductID,0 as Quantity,a.OrderQuantity,a.SchemeQuantity,a.PurchaseRate as ProdLastPurchaseRate,a.ShortListDate,a.IfDailyShortList,a.CreateduserID,a.CreatedDate,a.CreatedTime, b.ProductID, b.ProdName,b.Prodloosepack,b.prodpack,b.ProdCompShortName,b.ProdClosingStock,b.ProdBoxQuantity, a.OrderQuantity*a.PurchaseRate as Amount, 0 as sale " +
                                    "from tbldailyshortlist a inner join masterproduct b  on a.ProductID = b.ProductID where  a.MasterID = '{0}' ";

                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataTable ReadProductDetailsByIDForPurchase(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "Select distinct a.DSLID, a.MasterID, a.ProductID,0 as Quantity,a.OrderQuantity,a.SchemeQuantity,a.ShortListDate,a.IfDailyShortList,a.CreateduserID,a.CreatedDate,a.CreatedTime, b.ProductID, b.ProdName,b.Prodloosepack,b.prodpack,b.ProdCompShortName,b.ProdClosingStock,b.ProdBoxQuantity, a.OrderQuantity*a.PurchaseRate as Amount, 0 as sale, " +
                                    "b.ProdLastPurchaseVATPer,b.ProdIFBarcodeRequired,b.ProdLastPurchaseRate,b.ProdLastPurchaseTradeRate,b.ProdLastPurchaseSaleRate,b.ProdLastPurchaseDistributorSaleRatePer,b.ProdLastPurchaseDistributorSaleRate,b.ProdLastPurchaseMRP,b.ProdLastPurchaseCSTPer,ProdLastPurchaseCST,ProdLastPurchaseExpiry,ProdLastPurchaseExpiryDate,ProdLastPurchaseBatchNumber,ProdLastPurchaseStockID   from tbldailyshortlist a inner join masterproduct b  on a.ProductID = b.ProductID where  a. MasterID = '{0}' ";

                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataTable ReadProductDetailsByAccountID(string AccountId , string fromDate, string toDate)
        {
            
            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,e.AccName,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel - b.ProdClosingStock as OrderQuantity ,b.ProdLastPurchaseRate,0 as quantity  from tbldailyshortlist a " +
         " inner join masterproduct b on a.ProductID = b.ProductID  left outer join masteraccount e on a.AccountID = e.AccountID   where a.AccountID = '"+ AccountId +"' &&  a.ShortListDate >= " + "'" + fromDate + "'  && a.ShortListDate <= '" + toDate + "' && a.OrderNumber = 0 && b.ProdMaxLevel >= b.ProdClosingStock";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;          
        }
        public DataTable ReadProductDetailsAllTypesAccountIDWise(string fromDate, string toDate)
        {

            DataTable dtable = new DataTable();

            string strSql = "Select a.DSLID,a.OrderNumber,a.OrderDate,a.ShortListDate,a.OrderQuantity,a.IfSave,a.AccountID,e.AccName,b.ProductID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdBoxQuantity,b.ProdCompShortName," +
          "b.ProdPartyId_1,b.ProdPartyId_2,b.ProdMinLevel,b.ProdMaxLevel,b.ProdClosingStock,b.ProdMaxLevel - b.ProdClosingStock as OrderQuantity ,b.ProdLastPurchaseRate,0 as quantity  from tbldailyshortlist a " +
         " inner join masterproduct b on a.ProductID = b.ProductID  left outer join masteraccount e on a.AccountID = e.AccountID   where  a.ShortListDate >= " + "'" + fromDate + "'  && a.ShortListDate <= '" + toDate + "' && a.OrderNumber = 0 && b.ProdMaxLevel >= b.ProdClosingStock";

            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow GetSaleDataForLastSoldDays(string mprod, string lastdate)
        {
            DataRow dr = null;
            if (mprod != "" && lastdate != "")
            {
                string strsql = "Select  sum(a.quantity) as quantity  from  detailsale a  inner join vouchersale b on a.mastersaleID = b.ID where ProductID = '"+ mprod +"' &&  b.VoucherDate >= '"+ lastdate +"'";
                dr = DBInterface.SelectFirstRow(strsql);
            }
            return dr;
        }
        public DataRow GetRecoverQty(string mprod, string FromDaySaleToday, string EndDaySaleToday)
        {
            DataRow dr = null;
            if (mprod != "")
            {
                //string strsql = "Select distinct  sum(a.Quantity) as Quantity  from  detailsale a   where  a.voucherdate >= '"+ FromDaySaleToday + "' && a.voucherdate <= '"+ EndDaySaleToday + "' && a.ProductID='"+ mprod + "'  Group by a.ProductID ";
                string strsql = "Select distinct  round((sum(a.Quantity))/b.ProdLoosePack) as Quantity  from  detailsale a inner join masterproduct b on a.ProductID =b.ProductID   where  a.voucherdate >= '" + FromDaySaleToday + "' && a.voucherdate <= '" + EndDaySaleToday + "' && a.ProductID='" + mprod + "'  Group by a.ProductID ";
                dr = DBInterface.SelectFirstRow(strsql);
            }
            return dr;
        }

        public void TransferProductTodaySale(string accountID, int ProductID, int mqty)
        {
            string strsql = "Update masterproduct set prodpartyID_1 = '"+ accountID  +"' where ProductID = '"+ ProductID +"'";
            DBInterface.ExecuteQuery(strsql);
        }

        public void TransferProductShortList(string accountID, string dslID, int mqty)
        {
            string strsql = "Update tbldailyshortlist set accountID = '" + accountID + "' where dslID = '" + dslID + "'";
            bool retValue = (DBInterface.ExecuteQuery(strsql) > 0);
        }

        public DataTable GetListforPurchase(string accountID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID,VoucherType,VoucherSeries,VoucherNumber,VoucherDate,AccountID,Amount from masterorder  where AccountId = '"+ accountID +"' order by vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}
