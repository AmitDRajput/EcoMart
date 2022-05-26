using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBCreditNoteStock
    {
        #region Constructor
        public DBCreditNoteStock()
        {
        }
        #endregion

        #region Get Data
        public DataTable GetOverviewData(string DbntType)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a inner join  masteraccount b " +
                            "on a.AccountId = b.AccountId where a.VoucherType = " + "'" + DbntType + "' order by voucherdate desc ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForDebtorSale(string accID, string ClearInID)
        {
            // here 14/11/2015
            DataTable dtable = new DataTable();
            //string strSql = "Select CRDBID,AccountId,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,AmountNet,AmountClear,Narration " +
            //                "from vouchercreditdebitnote  where  (AccountID = '" + accID + "' AND  (ClearedInID is null OR ClearedInID = 0 OR ClearedInID = '' )   OR  (AccountID = '50004' AND  ClearedInID = '" + ClearInID + "')  ) AND (voucherType != '" + FixAccounts.VoucherTypeForStockIN + "' AND voucherType != '" + FixAccounts.VoucherTypeForStockOut + "' ) order by vouchertype, vouchernumber";
            string strSql = "Select ID,AccountId,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,AmountNet,AmountClear,Narration,ClearedInID " +
                            "from vouchercreditdebitnote  where  (AccountID = '" + accID + "' AND AmountClear = 0 AND  ( ClearedInID is null OR ClearedInID = 0 OR ClearedInID = '' )  AND (voucherType != '" + FixAccounts.VoucherTypeForStockIN + "' AND voucherType != '" + FixAccounts.VoucherTypeForStockOut + "' )) OR (ClearedInID != '' AND  ClearedInID = '" + ClearInID + "')  order by vouchertype, vouchernumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPatientSale(string ClearInID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID,AccountId,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,AmountNet,AmountClear,Narration " +
                            "from vouchercreditdebitnote  where  AccountID = '' AND  (AmountClear = 0   OR ClearedInID = '" + ClearInID + "')";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForParty(string accID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.ID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a, masteraccount b " +
                            "where a.AccountId = b.AccountId AND ( a.VoucherType = '"+FixAccounts.VoucherTypeForCreditNoteStock +"' OR a.VoucherType = '"+FixAccounts.VoucherTypeForCreditNoteAmount+"' order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPurchase(string accID, string ClearInID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select ID,AccountId,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,AmountNet,AmountClear,Narration " +
                            "from vouchercreditdebitnote  where  AccountID = '" + accID + "' AND (AmountClear = 0   OR ClearedInID = '" + ClearInID + "') order by voucherdate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForTempPurchase()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct a.ID,a.ProductID,a.StockID,a.Batchnumber,a.MRP,(a.Quantity-a.ClearedQuantity) as quantity,a.Expiry,a.PurchaseRate,a.CreatedDate, a.ClearedInVoucherID,b.ProdName,b.ProdLoosePack,b.ProdPack,b.ProdCompShortName,b.ProdBoxQuantity,b.ProdClosingStock,b.ProdVATPercent from tbltemppurchase a inner join masterproduct b  on a.productID = b.ProductID  where a.quantity > a.clearedQuantity ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
      
        public DataTable GetOverviewDataCreditNotes(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.ID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a  " +
                            "inner join masterAccount b on a.AccountID = b.AccountID  where  (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' OR a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteAmount + "') AND a.VoucherDate >= '" + fromDate + "' AND a.VoucherDate <= '" + toDate + "'" +
                            " order by voucherdate , VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from vouchercreditdebitnote where ID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByVouNumber(string voutype, int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select * from vouchercreditdebitnote where VoucherNumber ='{0}' AND voucherType = '" +  voutype  + "' ";
                strSql = string.Format(strSql, vouno);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByVouNumberForDistributor(int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select * from vouchercreditdebitnote where VoucherNumber ='{0}' AND voucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' ";
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
                string strsql = "Select distinct a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdCompShortName,b.StockID,b.BatchNumber,b.Quantity,b.Quantity as oldQuantity," +
                             "b.PurchaseRate,b.MRP,b.SaleRate,b.Expiry,b.ReasonCode,b.ExpiryDate,b.VATPer,b.Amount,b.ReturnRate,b.DiscountPercent,b.DiscountAmount,b.SerialNumber,b.GSTAmountZero,b.GSTSAmount,b.GSTCAmount,b.GSTIAmount,b.GSTS,b.GSTC,b.GSTI " +
                                "from detailcreditdebitnotestock b ,masterproduct a  where b.ProductId = a.ProductId  and " +
                                  " b. MasterID = '{0}' order by serialnumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }

        //public DataTable ReadDetailsByAccountID(string Id)
        //{
        //    DataTable dt = null;
        //    if (Id != "")
        //    {
        //        string strSql = "Select * from vouchercreditdebitnote where AccountID='{0}' AND ClearedInVoucherNumber = 0 ";
        //        strSql = string.Format(strSql, Id);
        //        dt = DBInterface.SelectDataTable(strSql);
        //    }
        //    return dt;
        //}
        //public DataTable ReadDetailsByAccountIDforEditPurchase(string Id, int vouno, string voutype, string vouseries)
        //{
        //    DataTable dt = null;
        //    if (Id != "")
        //    {
        //        string strSql = "Select * from vouchercreditdebitnote where AccountID='{0}'AND ClearedInVoucherNumber = {1} AND ClearedInVoucherType = '{2}' AND ClearedInVoucherSeries = '{3}'";
        //        strSql = string.Format(strSql, Id,vouno,voutype,vouseries);
        //        dt = DBInterface.SelectDataTable(strSql);
        //    }
        //    return dt;
        //}
        #endregion

        #region write Data
        public int AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double rnd, double clearAmount, string ClearVouType,
              double gstAmt0, double gstAmtS5, double gstAmtS12, double gstAmtS18, double gstAmtS28,
            double gstAmtC5, double gstAmtC12, double gstAmtC18, double gstAmtC28, double gsts5, double gsts12, double gsts18, double gsts28,
            double gstc5, double gstc12, double gstc18, double gstc28, double gstAmtI5, double gstAmtI12, double gstAmtI18, double gstAmtI28, double gstI5, double gstI12, double gstI18, double gstI28, string createdby, string createddate, string createdtime)
        {
            
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, rnd, clearAmount, ClearVouType,
            gstAmt0, gstAmtS5, gstAmtS12, gstAmtS18, gstAmtS28, gstAmtC5, gstAmtC12, gstAmtC18, gstAmtC28, gsts5,
                gsts12, gsts18, gsts28, gstc5, gstc12, gstc18, gstc28, gstAmtI5, gstAmtI12, gstAmtI18, gstAmtI28, gstI5, gstI12, gstI18, gstI28, createdby, createddate, createdtime);
            strSql += ";select last_insert_ID()";
            int ii = Convert.ToInt32(DBInterface.ExecuteScalar(strSql));
            return ii;
        }

        public bool AddDetailsProducts(int Id, string StockID, string ProductId, string Batchno, int quantity, int SchemeQuantity,
              double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, 
              double Amount, string VouType, int VouNo, string VouDate , double discountpercent, double discountamount,double TradeRate, 
              double ReturnRate, string MydbcrID, int serialNumber, double gstAmountZero, double gstSAmount, double gstCAmount,double gstIAmount,
               double gstS, double gstC, double gstI)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryProducts(Id, StockID, ProductId, Batchno, quantity, SchemeQuantity, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, reasoncode, VatPer, Amount, VouType, VouNo, VouDate, discountpercent, discountamount, TradeRate,
                ReturnRate, MydbcrID, serialNumber, gstAmountZero, gstSAmount, gstCAmount, gstIAmount, gstS, gstC, gstI); 

                if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
    //
        public bool AddVoucherIntblTrnac(int Id, int debitAccount, int creditAccount, string Narration, string VouType, int VouNo,
       string VouDate, double debitamount, double creditamount, string detailID, string createdby, string createddate, string createdtime)
        {
            bool retValue = false;
            string strSql = GetInsertQueryTrnacForVoucher(Id, debitAccount, creditAccount, Narration, VouType, VouNo,
                VouDate, debitamount, creditamount, detailID, createdby, createddate, createdtime);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retValue = true;
            }
            return retValue;
        }
        private string GetInsertQueryTrnacForVoucher(int Id, int debitaccount, int creditaccount, string Narration, string VouType, int VouNo,
        string VouDate, double debitamount, double creditamount, string DetailId, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
          //  objQuery.AddToQuery("tblTrnacID", DetailId);
            objQuery.AddToQuery("VoucherID", Id);
            objQuery.AddToQuery("AccountId", debitaccount);
            objQuery.AddToQuery("Debit", debitamount);
            objQuery.AddToQuery("Credit", creditamount);
            objQuery.AddToQuery("AccAccountID", creditaccount);
            objQuery.AddToQuery("TransactionDate", VouDate);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("ReferenceVoucherId", "");
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherNumber", VouNo);           
            //objQuery.AddToQuery("VoucherDate", VouDate);
            //objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            //objQuery.AddToQuery("AmountNet", Amt);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);

            return objQuery.InsertQuery();
        }
        //public bool AddProductDetailsInStockTable(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
        //      string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double PurchaseVATPercent,
        //      double ProductVATPercent, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount,
        //      string accountId, string billnumber, string voutype, int vounumber, string voudate, int ProdLoosePack, string StockId, double productMargin)
        public bool AddProductDetailsInStockTable(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
              string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity,  double Amount,
              string accountId,int ProdLoosePack, string scanCode, string StockId)
        {
            bool bRetValue = false;          
            string strSql = GetInsertQueryDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity,  Amount,
                    accountId,  ProdLoosePack,scanCode, StockId);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public int AddProductDetailsInStockTableForDistributor(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
            string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, double Amount,
            string accountId, int ProdLoosePack, string scanCode, string StockId)
        {           
            string strSql = GetInsertQueryDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity*ProdLoosePack, SchemeQuantity*ProdLoosePack, Amount,
                    accountId, ProdLoosePack, scanCode, StockId);
            strSql += ";select last_insert_ID()";
            int ii = Convert.ToInt32(DBInterface.ExecuteScalar(strSql));
            return ii;           
        }

        //
        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, double clearAmount, string ClearVouType, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd, clearAmount, ClearVouType, modifiedby,modifieddate,modifiedtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
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

         public bool DeleteProductsByMasterID(string Id)
        {
            bool bRetValue = false;
            string strSql = GetDeleteQueryProducts(Id);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
         public bool DeleteFromtblTrnac(string Id)
         {
             bool bRetValue = false;
             string strSql = "Delete from tblTrnac where voucherID = '"+ Id +"'";
             if (DBInterface.ExecuteQuery(strSql) > 0)
             {
                 bRetValue = true;
             }
             return bRetValue;
         }
        #endregion

        #region validations

        public bool CheckStock()
        {
            return true;
        }
        #endregion

        #region Query Building Functions

        private string GetInsertQuery(string id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double rnd, double clearAmount, string ClearVouType,
              double gstAmt0, double gstAmtS5, double gstAmtS12, double gstAmtS18, double gstAmtS28,
            double gstAmtC5, double gstAmtC12, double gstAmtC18, double gstAmtC28, double gsts5, double gsts12, double gsts18, double gsts28,
            double gstc5, double gstc12, double gstc18, double gstc28, double gstAmtI5, double gstAmtI12, double gstAmtI18, double gstAmtI28, double gstI5, double gstI12, double gstI18, double gstI28, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
          //  objQuery.AddToQuery("CRDBID", Id);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("DiscountPer", DiscPer);
            objQuery.AddToQuery("DiscountAmount", DiscAmt);
            objQuery.AddToQuery("Amount", Amt);
           // objQuery.AddToQuery("VAT5", Vat5);
          //  objQuery.AddToQuery("VAT12point5", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountClear", clearAmount);
            objQuery.AddToQuery("ClearedInVoucherType", ClearVouType);
            objQuery.AddToQuery("ClearedInVoucherNumber", 0);

            objQuery.AddToQuery("AmountGST0", gstAmt0);
            objQuery.AddToQuery("AmountGSTS5", gstAmtS5);
            objQuery.AddToQuery("AmountGSTS12", gstAmtS12);
            objQuery.AddToQuery("AmountGSTS18", gstAmtS18);
            objQuery.AddToQuery("AmountGSTS28", gstAmtS28);
            objQuery.AddToQuery("AmountGSTC5", gstAmtC5);
            objQuery.AddToQuery("AmountGSTC12", gstAmtC12);
            objQuery.AddToQuery("AmountGSTC18", gstAmtC18);
            objQuery.AddToQuery("AmountGSTC28", gstAmtC28);

            objQuery.AddToQuery("GSTS5", gsts5);
            objQuery.AddToQuery("GSTS12", gsts12);
            objQuery.AddToQuery("GSTS18", gsts18);
            objQuery.AddToQuery("GSTS28", gsts28);
            objQuery.AddToQuery("GSTC5", gstc5);
            objQuery.AddToQuery("GSTC12", gstc12);
            objQuery.AddToQuery("GSTC18", gstc18);
            objQuery.AddToQuery("GSTC28", gstc28);

            objQuery.AddToQuery("AmountGSTI5", gstAmtI5);
            objQuery.AddToQuery("AmountGSTI12", gstAmtI12);
            objQuery.AddToQuery("AmountGSTI18", gstAmtI18);
            objQuery.AddToQuery("AmountGSTI28", gstAmtI28);
            objQuery.AddToQuery("GSTI5", gstI5);
            objQuery.AddToQuery("GSTI12", gstI12);
            objQuery.AddToQuery("GSTI18", gstI18);
            objQuery.AddToQuery("GSTI28", gstI28);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryProducts(int Id,string stockid, string ProductId, string Batchno, int quantity, int SchemeQuantity,
              double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount,
              string VouType, int VouNo, string VouDate, double discountpercent, double discountamount, double TradeRate, double ReturnRate, string MydbcrID,
              int serialNumber, double gstAmountZero, double gstSAmount, double gstCAmount, double gstIAmount,
               double gstS, double gstC, double gstI)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcreditdebitnotestock";
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("ProductID", ProductId);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("Quantity", quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
         //   objQuery.AddToQuery("SaleRate",SaleRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("ReasonCode", reasoncode);
            objQuery.AddToQuery("VATPer", VatPer);
            objQuery.AddToQuery("Amount", Amount);          
            objQuery.AddToQuery("DiscountPercent", discountpercent);
            objQuery.AddToQuery("DiscountAmount", discountamount);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("ReturnRate", ReturnRate);
         //   objQuery.AddToQuery("DetailCreditDebitNoteStockID", MydbcrID);
            objQuery.AddToQuery("SerialNumber", serialNumber);

            objQuery.AddToQuery("GSTAmountZero", gstAmountZero);
            objQuery.AddToQuery("GSTSAmount", gstSAmount);
            objQuery.AddToQuery("GSTCAmount", gstCAmount);
            objQuery.AddToQuery("GSTIAmount", gstIAmount);
            objQuery.AddToQuery("GSTS", gstS);
            objQuery.AddToQuery("GSTC", gstC);
            objQuery.AddToQuery("GSTI", gstI);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, double clearAmount, string ClearVouType, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("ID", Id,true);
            objQuery.AddToQuery("AccountId", CreditorId);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("VoucherType", VouType);
            objQuery.AddToQuery("VoucherNumber", VouNo);
            objQuery.AddToQuery("VoucherDate", VouDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("DiscountPer", DiscPer);
            objQuery.AddToQuery("DiscountAmount", DiscAmt);
            objQuery.AddToQuery("Amount", Amt);
            objQuery.AddToQuery("VAT5", Vat5);
            objQuery.AddToQuery("VAT12point5", Vat12);
            objQuery.AddToQuery("RoundingAmount", rnd);
            objQuery.AddToQuery("AmountClear", clearAmount);
            objQuery.AddToQuery("ClearedInVoucherType", ClearVouType);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }

        private string GetDeleteQuery(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("ID", Id, true);
            strSql = objQuery.DeleteQuery();
            return strSql;
        }

        private string GetDeleteQueryProducts(string Id)
        {
            string strSql = "";
            Query objQuery = new Query();
            objQuery.Table = "detailcreditdebitnotestock";
            objQuery.AddToQuery("MasterID", Id, true);          
            strSql = objQuery.DeleteQuery();
            return strSql;
        }

        private string GetInsertQueryDetailsInStockTable(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
             string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity,  double Amount,
             string accountId, int ProdLoosePack, string scanCode, string StockId)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblstock";
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
       //     objQuery.AddToQuery("DistributorSaleRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("ClosingStock", (Quantity + SchemeQuantity) );
            objQuery.AddToQuery("CreditNoteStock", (Quantity + SchemeQuantity));
          //  objQuery.AddToQuery("PurchaseSchemeStock", (SchemeQuantity));
            //objQuery.AddToQuery("PurchaseReplacementStock", (ReplacementQuantity) * ProdLoosePack);
            //objQuery.AddToQuery("PurchaseVATPercent", PurchaseVATPercent);
            //objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("LastPurchaseAccountId", accountId);
            //objQuery.AddToQuery("LastPurchaseBillNumber", billnumber);
            //objQuery.AddToQuery("LastPurchaseVoucherType", voutype);
            //objQuery.AddToQuery("LastPurchaseVoucherNumber", vounumber);
            //objQuery.AddToQuery("LastPurchaseDate", voudate);
            objQuery.AddToQuery("OpeningStock", 0);
            objQuery.AddToQuery("BeginningStock", 0);
            objQuery.AddToQuery("IfRateCorrection", "");
            objQuery.AddToQuery("ScanCode", scanCode);
            objQuery.AddToQuery("TransferInStock", 0);
            objQuery.AddToQuery("PurchaseStock", 0);
            objQuery.AddToQuery("SaleStock", 0);
            objQuery.AddToQuery("TransferOutStock", 0);
            objQuery.AddToQuery("DebitNoteStock", 0);
            objQuery.AddToQuery("SaleSchemeStock", 0);
         //   objQuery.AddToQuery("StockID", StockId);
            //objQuery.AddToQuery("Margin", productMargin);

            return objQuery.InsertQuery();
        }       
        #endregion 
    
        public DataRow GetLastRecord(string CrdbVouType, string CrdbVouSeries)
        {
            DataRow dRow = null;

            string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' AND voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber desc";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }

        public DataRow GetLastVoucherNumber(string vouType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select Vouchernumber from vouchercreditdebitnote where  VoucherType =  '" + vouType + "'  AND  VoucherSeries = '" + vouSeries + "' order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetFirstRecord(string CrdbVouType, string CrdbVouSeries)
        {
            DataRow dRow = null;

            string strSql = "Select * from vouchercreditdebitnote where voucherType = '" + CrdbVouType + "' AND voucherSeries = '" + CrdbVouSeries + "' order by vouchernumber ";

            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }
        public DataTable GetOverviewDataForLastPurchase(string productID, string CreditAcID)   //Amar
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.DetailSaleID, a.ProductID, a.CashDiscountAmount, a.BatchNumber,a.SaleRate, a.Quantity ,a.SchemeQuantity,a.MRP,b.VoucherNumber,b.VoucherDate,b.AccountID from detailsale a  inner join vouchersale b on a.MasterSaleID = b.ID  where a.ProductID = '" + productID + "' and b.PatientID  ='" + CreditAcID + "' OR b.AccountID='" + CreditAcID + "'   order by b.VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
    }
}
