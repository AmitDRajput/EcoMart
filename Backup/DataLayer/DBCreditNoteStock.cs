using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
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
            string strSql = "Select a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a inner join  masteraccount b " +
                            "on a.AccountId = b.AccountId where a.VoucherType = " + "'" + DbntType + "'" +
                            " union Select a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID, b.patientID as AccountID, b.PatientNAme as AccName, b.PatientAddress1 as AccAddress1,b.patientAddress2 as AccAddress2 from vouchercreditdebitnote a inner join  masterpatient b " +
                            "on a.AccountId = b.PatientID where a.VoucherType = " + "'" + DbntType + "'" + "  order by vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForDebtorSale(string accID, string ClearInID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select CRDBID,AccountId,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,AmountNet,AmountClear,Narration " +
                            "from vouchercreditdebitnote  where  AccountID = '" + accID + "' && (AmountClear = 0   || ClearedInID = '" + ClearInID + "') order by vouchertype, vouchernumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPatientSale(string ClearInID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select CRDBID,AccountId,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,AmountNet,AmountClear,Narration " +
                            "from vouchercreditdebitnote  where  AccountID = '' &&  (AmountClear = 0   || ClearedInID = '" + ClearInID + "')";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForParty(string accID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a, masteraccount b " +
                            "where a.AccountId = b.AccountId && ( a.VoucherType = '"+FixAccounts.VoucherTypeForCreditNoteStock +"' || a.VoucherType = '"+FixAccounts.VoucherTypeForCreditNoteAmount+"' order by a.vouchernumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPurchase(string accID, string ClearInID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select CRDBID,AccountId,VoucherSeries,VoucherType,VoucherNumber,VoucherDate,AmountNet,AmountClear,Narration " +
                            "from vouchercreditdebitnote  where  AccountID = '" + accID + "' && (AmountClear = 0   || ClearedInID = '" + ClearInID + "')";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
      
        public DataTable GetOverviewDataCreditNotes(string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct  a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercreditdebitnote a  " +
                            "inner join masterAccount b on a.AccountID = b.AccountID  where (a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteAmount + "' && a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "') " +
                            " union select distinct a.CRDBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet,a.Narration, " +
                            "a.AccountID,a.ClearedInVoucherType,a.ClearedInVoucherNumber,a.ClearedInVoucherDate, c.PatientID as AccountId,c.PatientName as AccName,c.PatientAddress1 as AccAddress1,c.PatientAddress2 as AccAddress2 from vouchercreditdebitnote a  " +
                            " inner join masterPatient c on a.AccountID = c.PatientID where  a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' || a.VoucherType = '" + FixAccounts.VoucherTypeForCreditNoteAmount + "' && a.VoucherDate >= '" + fromDate + "' && a.VoucherDate <= '" + toDate + "'";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from vouchercreditdebitnote where CRDBID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByVouNumber(int vouno)
        {
            DataRow dRow = null;
            if (vouno != 0)
            {
                string strSql = "Select * from vouchercreditdebitnote where VoucherNumber ='{0}' && voucherType = '" + FixAccounts.VoucherTypeForCreditNoteStock + "' ";
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
                             "b.PurchaseRate,b.MRP,b.SaleRate,b.Expiry,b.ReasonCode,b.ExpiryDate,b.VATPer,b.Amount,b.ReturnRate " +
                                "from detailcreditdebitnotestock b ,masterproduct a  where b.ProductId = a.ProductId  and " +
                                  " b. MasterID = '{0}'";
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
        //        string strSql = "Select * from vouchercreditdebitnote where AccountID='{0}' && ClearedInVoucherNumber = 0 ";
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
        //        string strSql = "Select * from vouchercreditdebitnote where AccountID='{0}'&& ClearedInVoucherNumber = {1} && ClearedInVoucherType = '{2}' && ClearedInVoucherSeries = '{3}'";
        //        strSql = string.Format(strSql, Id,vouno,voutype,vouseries);
        //        dt = DBInterface.SelectDataTable(strSql);
        //    }
        //    return dt;
        //}
        #endregion

        #region write Data
        public bool AddDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, double clearAmount, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd,clearAmount, createdby,createddate,createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool AddDetailsProducts(string Id, string StockID, string ProductId, string Batchno, int quantity, int SchemeQuantity,
              double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate , double discountpercent, double discountamount,double TradeRate, double ReturnRate, string MydbcrID)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryProducts(Id,StockID, ProductId, Batchno, quantity, SchemeQuantity, PurchaseRate, MRP, SaleRate, Expiry, ExpiryDate, reasoncode, VatPer, Amount, VouType, VouNo, VouDate,discountpercent,discountamount,TradeRate,ReturnRate, MydbcrID);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
    //
        public bool AddVoucherIntblTrnac(string Id, string debitAccount, string creditAccount, string Narration, string VouType, int VouNo,
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
        private string GetInsertQueryTrnacForVoucher(string Id, string debitaccount, string creditaccount, string Narration, string VouType, int VouNo,
        string VouDate, double debitamount, double creditamount, string DetailId, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "tbltrnac";
            objQuery.AddToQuery("tblTrnacID", DetailId);
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
              string accountId,int ProdLoosePack, string StockId)
        {
            bool bRetValue = false;          
            string strSql = GetInsertQueryDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity,  Amount,
                    accountId,  ProdLoosePack, StockId);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }
        //
        public bool UpdateDetails(string Id, string CreditorId, string Narration, string VouType, int VouNo,
           string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, double clearAmount, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(Id, CreditorId, Narration, VouType, VouNo,
                VouDate, AmountNet, DiscPer, DiscAmt, Amt, Vat5, Vat12, rnd,clearAmount, modifiedby,modifieddate,modifiedtime);
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

        private string GetInsertQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, double clearAmount, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("CRDBID", Id);
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
            objQuery.AddToQuery("ClearedInVoucherNumber", 0);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryProducts(string Id,string stockid, string ProductId, string Batchno, int quantity, int SchemeQuantity,
              double PurchaseRate, double MRP, double SaleRate, string Expiry, string ExpiryDate, string reasoncode, double VatPer, double Amount, string VouType, int VouNo, string VouDate, double discountpercent, double discountamount, double TradeRate, double ReturnRate, string MydbcrID)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcreditdebitnotestock";
            objQuery.AddToQuery("MasterID", Id);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("ProductID", ProductId);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("Quantity", quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
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
            objQuery.AddToQuery("DetailCreditDebitNoteStockID", MydbcrID);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string Id, string CreditorId, string Narration, string VouType, int VouNo,
             string VouDate, double AmountNet, double DiscPer, double DiscAmt, double Amt, double Vat5, double Vat12, double rnd, double clearAmount, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercreditdebitnote";
            objQuery.AddToQuery("CRDBID", Id,true);
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
            objQuery.AddToQuery("CRDBID", Id, true);
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
             string accountId, int ProdLoosePack, string StockId)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblstock";
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("ClosingStock", (Quantity + SchemeQuantity) * ProdLoosePack);
            objQuery.AddToQuery("PurchaseStock", (Quantity) * ProdLoosePack);
            objQuery.AddToQuery("PurchaseSchemeStock", (SchemeQuantity) * ProdLoosePack);
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
            //objQuery.AddToQuery("ScanCode", purScanCode);
            objQuery.AddToQuery("TransferInStock", 0);
            objQuery.AddToQuery("CreditNoteStock", 0);
            objQuery.AddToQuery("SaleStock", 0);
            objQuery.AddToQuery("TransferOutStock", 0);
            objQuery.AddToQuery("DebitNoteStock", 0);
            objQuery.AddToQuery("SaleSchemeStock", 0);
            objQuery.AddToQuery("StockID", StockId);
            //objQuery.AddToQuery("Margin", productMargin);

            return objQuery.InsertQuery();
        }       
        #endregion 
    
      
    }
}
