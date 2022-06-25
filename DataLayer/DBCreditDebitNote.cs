using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBCreditDebitNote
    {
        #region Constructor
        public DBCreditDebitNote()
        {
        }
        #endregion

        #region Get Data
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a, masteraccount b " +
                            "where a.AccountId = b.AccountId   order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

      

        public DataTable GetOverviewDataForParty(string accID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from vouchercreditdebitnote a, masteraccount b " +
                            "where a.AccountId = b.AccountId && ( a.VoucherType = '"+ FixAccounts.VoucherTypeForCreditNoteStock +"' || a.VoucherType = '"+FixAccounts.VoucherTypeForCreditNoteAmount +"' order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
      
        #endregion

        public  bool  UpdateCreditDebitNoteAdjustedDetails(string crdbid, double mamtnet , string voutype, int vounumber, string voudate, string billnumber , string purchaseid, string vouSeries)
        {
            bool bRetValue = false;
            DataTable dtable = new DataTable();
            string strSql = GetUpdateCreditDebitNoteAdjustedDetailsQuety(crdbid, mamtnet, voutype, vounumber,voudate, billnumber,purchaseid,vouSeries);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool ClearPreviousdebitcreditnotes(string purchaseid)
        {
            bool bRetValue = true;
            DataTable dtable = new DataTable();
            string strSql = "update vouchercreditdebitnote  set ClearedInID = '" + " ' , AmountClear =  0  , ClearedInVoucherNumber = 0 , ClearedInVoucherType = ' ', ClearedInPurchaseBillNumber = ' ', ClearedInVoucherDate = ' '  where ClearedInID = '" + purchaseid + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            else
                bRetValue = false;

            return bRetValue;
        }

        public DataTable GetOverviewDataForParty(string accID, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from vouchercreditdebitnote a, masteraccount b " +
                            "where a.AccountId = b.AccountId  && a.AccountID = '"+accID +"' && a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "' order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetDebitCreditStockListProductByVouType(string voutype,string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID,a.ProductID,a.Quantity,a.SchemeQuantity,b.CRDBID,b.VoucherType,b.VoucherNumber,b.VoucherDate,b.AccountId,b.Narration," +
                            "c.AccountID, c.AccName, c.AccAddress1, c.AccAddress2, d.ProdName, d.ProdLoosePack, d.ProdPack, d.ProdCompShortName from detailcreditdebitnotestock a inner join vouchercreditdebitnote b on a.MasterID = b.CRDBID " +
                            " inner join masteraccount c  on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where b.VoucherType = '" + voutype + "' && b.VoucherDate >= '"+ fromDate+"' && b.VoucherDate <= '"+toDate +"'" +
                            " union Select a.MasterID,a.ProductID,a.Quantity,a.SchemeQuantity,b.CRDBID,b.VoucherType,b.VoucherNumber,b.VoucherDate,b.AccountId,b.Narration, " +
            "d.ProdName, d.ProdLoosePack, d.ProdPack, d.ProdCompShortName from detailcreditdebitnotestock a inner join vouchercreditdebitnote b on a.MasterID = b.CRDBID " +
            " inner join masterproduct d on a.ProductID = d.ProductID  where b.VoucherType = '" + voutype + "' && b.VoucherDate >= '" + fromDate + "' && b.VoucherDate <= '" + toDate + "' order by ProdName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetDebitCreditStockListProductByVouType(string voutype, string fromDate, string toDate, int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID,a.ProductID,a.Quantity,a.SchemeQuantity,b.CRDBID,b.VoucherType,b.VoucherNumber,b.VoucherDate,b.AccountId,b.Narration, " +
                            "c.AccountID, c.AccName, c.AccAddress1, c.AccAddress2, d.ProdName, d.ProdLoosePack, d.ProdPack, d.ProdCompShortName from detailcreditdebitnotestock a inner join vouchercreditdebitnote b on a.MasterID = b.CRDBID " +
                            " inner join masteraccount c  on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where b.VoucherType = '" + voutype + "' && b.VoucherDate >= '" + fromDate + "' && b.VoucherDate <= '" + toDate + "' && a.ProductID = '"+ ProductID+"'" +
            "  union Select a.MasterID,a.ProductID,a.Quantity,a.SchemeQuantity,b.CRDBID,b.VoucherType,b.VoucherNumber,b.VoucherDate,b.AccountId,b.Narration, " +
            "d.ProdName, d.ProdLoosePack, d.ProdPack, d.ProdCompShortName from detailcreditdebitnotestock a inner join vouchercreditdebitnote b on a.MasterID = b.CRDBID " +
            " inner join masterproduct d on a.ProductID = d.ProductID  where b.VoucherType = '" + voutype + "' && b.VoucherDate >= '" + fromDate + "' && b.VoucherDate <= '" + toDate + "' && a.ProductID = '" + ProductID + "'";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetDebitCreditListProduct(int ProductID,string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID,a.ProductID,a.Quantity,a.SchemeQuantity,b.CRDBID,b.VoucherType,b.VoucherNumber,b.VoucherDate,b.AccountId,b.Narration, " +
                            "c.AccountID, c.AccName, c.AccAddress1, c.AccAddress2, d.ProdName, d.ProdLoosePack, d.ProdPack, d.ProdCompShortName from detailcreditdebitnotestock a inner join vouchercreditdebitnote b on a.MasterID = b.CRDBID " +
                            " inner join masteraccount c  on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where  b.VoucherDate >= '" + fromDate + "' && b.VoucherDate <= '" + toDate + "' && a.ProductID = '" + ProductID + "'" +
            "  union Select a.MasterID,a.ProductID,a.Quantity,a.SchemeQuantity,b.CRDBID,b.VoucherType,b.VoucherNumber,b.VoucherDate,b.AccountId,b.Narration, " +
            "d.ProdName, d.ProdLoosePack, d.ProdPack, d.ProdCompShortName from detailcreditdebitnotestock a inner join vouchercreditdebitnote b on a.MasterID = b.CRDBID " +
            "inner join masterproduct d on a.ProductID = d.ProductID  where  b.VoucherDate >= '" + fromDate + "' && b.VoucherDate <= '" + toDate + "' && a.ProductID = '" + ProductID + "'";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetStockoutListProduct(int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.MasterID,a.ProductID,a.Quantity,a.SchemeQuantity,b.CRDBID,b.VoucherType,b.VoucherNumber,b.VoucherDate,b.AccountId, " +
                            "c.AccountID, c.AccName, c.AccAddress1, c.AccAddress2, d.ProdName, d.ProdLoosePack, d.ProdPack, d.ProdCompShortName from detailcreditdebitnotestock a inner join vouchercreditdebitnote b on a.MasterID = b.CRDBID " +
                            " left outer join masteraccount c  on b.AccountID = c.AccountID  inner join masterproduct d on a.ProductID = d.ProductID  where  a.ProductID = '" + ProductID + "' &&  b.VoucherType = '"+ FixAccounts.VoucherTypeForStockOut +"' order by b.VoucherDate ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForVATReport(string voucherType, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            //// check
            //string strSql = "(Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
            //                "a.AccountID,a.VAT5,a.VAT12point5,((a.VAT5*100)/6) as Amount5, ((a.VAT12Point5*100)/13.5) as Amount12point5,a.RoundingAmount,a.ClearedInVoucherType, " +
            //                "a.ClearedInVoucherNumber, b.AccountID, b.AccName, b.AccAddress1, b.AccAddress2 from vouchercreditdebitnote a inner join masteraccount b " +
            //                "on a.AccountID = b.AccountID  where  VoucherType = '"+ voucherType +"' && a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "') " +
            //                " union all (Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
            //                "a.AccountID,a.VAT5,a.VAT12point5,((a.VAT5*100)/6) as Amount5, ((a.VAT12Point5*100)/13.5) as Amount12point5,a.RoundingAmount,a.ClearedInVoucherType, " +
            //                "a.ClearedInVoucherNumber,b.PatientID, b.PatientName, b.PatientAddress1, b.PatientAddress2 from vouchercreditdebitnote a " +
            //                "atientID  where  VoucherType = '"+ voucherType +"' && a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "') order by VoucherDate, VoucherNumber ";
            //dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
           // string strSql = "Select voucherType,VoucherDate, sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOnFreight+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase where VoucherDate >= '" + mfromdate + "' && VoucherDate <= '" + mtodate + "'  group by substr(VoucherDate,5,2) order by VoucherDate";
        }
        public DataTable GetOverviewDataForVATReportMonth(string voucherType, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            // vat 5.5
            string strSql = "Select  VoucherType,VoucherDate,sum(AmountNet) as AmountNet, " +
                            "sum(VAT5) as VAT5, sum(VAT12point5) as VAT12point5,sum((VAT5*100)/6) as Amount5, sum((VAT12Point5*100)/13.5) as Amount12point5 from vouchercreditdebitnote  " +
                            "where  VoucherType = '" + voucherType + "' && VoucherDate >= '" + fromDate + "' && VoucherDate <= '" + toDate + "' group by substr(VoucherDate,5,2) order by VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
            // string strSql = "Select voucherType,VoucherDate, sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOnFreight+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase where VoucherDate >= '" + mfromdate + "' && VoucherDate <= '" + mtodate + "'  group by substr(VoucherDate,5,2) order by VoucherDate";
        }
        public bool UpdateCreditDebitNoteforTypeChange(string crdbid, double mamtnet, string voutype, int vounumber, string voudate, string billnumber, string purchaseid)
        {
            bool bRetValue = false;
            DataTable dtable = new DataTable();
            string strSql = "Update vouchercreditdebitnote  set ClearedInVoucherType = '" + voutype + "' , ClearedInVoucherNumber = " + vounumber + "  where ClearedInID = '" + purchaseid + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        private string GetUpdateCreditDebitNoteAdjustedDetailsQuety(string crdbid, double mamtnet, string voutype, int vounumber, string voudate,string billnumber,string purchaseid, string vouSeries)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("Id", crdbid, true);
            objQuery.AddToQuery("AmountClear", mamtnet);
            objQuery.AddToQuery("ClearedInVoucherType", voutype);
            objQuery.AddToQuery("ClearedInVoucherNumber", vounumber);
            objQuery.AddToQuery("ClearedInVoucherDate", voudate);
            objQuery.AddToQuery("ClearedInVoucherSeries", vouSeries);
            objQuery.AddToQuery("ClearedInPurchaseBillNumber", billnumber);
            objQuery.AddToQuery("ClearedInID", purchaseid);
            return objQuery.UpdateQuery();
        }

        public bool clearPreviousdebitcreditnotes(string purchaseid)
        {
            bool bRetValue = true;
            DataTable dtable = new DataTable();
            string strSql = "update vouchercreditdebitnote  set ClearedInID = '" + " ' , AmountClear =  0  , ClearedInVoucherNumber = 0 , ClearedInVoucherType = ' ', ClearedInPurchaseBillNumber = ' ', ClearedInVoucherDate = ' '  where ClearedInID = '" + purchaseid + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            else
                bRetValue = false;

            return bRetValue;
        }



    }
}
