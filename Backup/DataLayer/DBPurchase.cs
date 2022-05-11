using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;


namespace PharmaSYSRetailPlus.DataLayer
{
    public class DBPurchase
    {
        public DBPurchase()
        {

        }

        public DataTable GetPurchase()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select purchaseID,EntryDate,VoucherSeries,VoucherType,VoucherSubType,VoucherNumber,VoucherDate,PurchaseBillNumber,AccountID,AmountNet,AmountClear,AmountGross,AmountItemDiscount,AmountSpecialDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountAddOnFreight,CashDiscountPercentage,AmountCreditNote,AmountDebitNote,StatementNumber,RoundUpAmount,OctroiPercentage,AmountOctroi,DueDate,Narration,AmountPurchase4PercentVAT,AmountVAT4Percent,PurchaseAmount12.5PercentVAT,AmountVAT12.5Percent, AmountPurchaseZeroVAT,NumberofChallans,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataRow GetPendingAmount(string accountid)
        {
            DataRow dr;
            string strSql = "Select sum(debit) as debit,sum(credit) as credit from tbltrnac where AccountID = '" + accountid + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }
       
        public DataRow GetDNAmount(string accountid)
        {
            DataRow dr;
            string strSql = "Select sum(AmountNet) as AmountNet from vouchercreditdebitnote where AmountClear = 0 && (VoucherType = '"+ FixAccounts.VoucherTypeForDebitNoteAmount +"' || VoucherType = '"+ FixAccounts.VoucherTypeForDebitNoteStock +"')  && AccountID = '" + accountid + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }
        public bool AddDetails(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
            string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
            double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
            double AmountAddOnFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
            double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
            double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string voucherSubType,string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(purchaseID, accountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
                SpecialDiscPer , AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
                AmountAddOnFreight, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
                OctroiPercentage, AmountOctroi, PurchaseAmount5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
                PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, voucherSubType, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool AddChangedDetails(string purchaseID, string changedID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
           string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
           double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
           double AmountAddOnFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
           double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
           double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryChanged(purchaseID,changedID,accountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
                SpecialDiscPer, AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
                AmountAddOnFreight, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
                OctroiPercentage, AmountOctroi, PurchaseAmount5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
                PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, modifiedby, modifieddate, modifiedtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool AddDeletedDetails(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
           string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
           double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
           double AmountAddOnFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
           double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
           double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDeleted(purchaseID, accountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
                SpecialDiscPer, AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
                AmountAddOnFreight, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
                OctroiPercentage, AmountOctroi, PurchaseAmount5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
                PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool AddDetailsProductsSS(string Id, string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
               double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
               string IfTradeRateInclusiveOfVAT, double Amount, double spldiscamt, double spldiscper, double AmountZeroVAT, double AmountCashDiscountperunit, string stockid, string mydetailpurchaseid,double productMargin,double productMargin2,int serialNumber,string scancode, double distributorRatePer)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetails(Id, ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercent,
                    AmountSchemeDiscount, PurchaseVATPercent, ProductVATPercent, AmountPurchaseVAT, AmountProductVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT,
                    IfTradeRateInclusiveOfVAT, Amount, spldiscamt, spldiscper, AmountZeroVAT, AmountCashDiscountperunit,stockid,mydetailpurchaseid,productMargin,productMargin2, serialNumber,scancode,distributorRatePer);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }


        public bool AddChangedDetailsProductsSS(string Id,string changedMasterID, string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
             string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
             double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
             string IfTradeRateInclusiveOfVAT, double Amount, double spldiscamt, double spldiscper, double AmountZeroVAT, double AmountCashDiscountperunit, string stockid, string mydetailpurchaseid, double productMargin, double productMargin2, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryChangedDetails(Id, changedMasterID, ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercent,
                    AmountSchemeDiscount, PurchaseVATPercent, ProductVATPercent, AmountPurchaseVAT, AmountProductVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT,
                    IfTradeRateInclusiveOfVAT, Amount, spldiscamt, spldiscper, AmountZeroVAT, AmountCashDiscountperunit, stockid, mydetailpurchaseid, productMargin, productMargin2, serialNumber);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool AddDeletedDetailsProductsSS(string Id, string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
             string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
             double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
             string IfTradeRateInclusiveOfVAT, double Amount, double spldiscamt, double spldiscper, double AmountZeroVAT, double AmountCashDiscountperunit, string stockid, string mydetailpurchaseid, double productMargin, double productMargin2, int serialNumber)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDeletedDetails(Id, ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercent,
                    AmountSchemeDiscount, PurchaseVATPercent, ProductVATPercent, AmountPurchaseVAT, AmountProductVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT,
                    IfTradeRateInclusiveOfVAT, Amount, spldiscamt, spldiscper, AmountZeroVAT, AmountCashDiscountperunit, stockid, mydetailpurchaseid, productMargin, productMargin2, serialNumber);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }



        public bool AddProductDetailsInStockTable(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
            string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double PurchaseVATPercent,
            double ProductVATPercent, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount,
            string accountId, string billnumber, string voutype, int vounumber, string voudate, int ProdLoosePack,string StockId,double productMargin,string purScanCode,double distSaleRate, double distSaleRatePer)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, PurchaseVATPercent,
                    ProductVATPercent, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount,
                    accountId, billnumber, voutype, vounumber, voudate, ProdLoosePack, StockId, productMargin, purScanCode,distSaleRate,distSaleRatePer);
            //string strSql = GetInsertQueryDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
            //        Expiry, ExpiryDate, Quantity, SchemeQuantity,  Amount,
            //        accountId,ProdLoosePack,StockId);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

      

        public bool AddCashEntry(string CBId, string CBVouType, int CBVouNo, string VoucherDate, string AccountID, string Narration, double Amount, string createdby, string createddate, string createdtime)
        {
            bool retValue = false;
            string strSql = GetInsertQueryForCashPayment(CBId, CBVouType, CBVouNo, VoucherDate, AccountID, Narration, Amount, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;
        }

        public bool AddCashEntryDetails(string CBId, string purchaseid, string purchasevoutype, int purchasevounumber, string puchaseVouDate, double Amount)
        {
            bool retValue = false;
            string strSql = GetInsertQueryForCashPaymentDetails(CBId, purchaseid, purchasevoutype, purchasevounumber, puchaseVouDate, Amount);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;
        }
        public bool AddBankEntry(string CBId, string CBVouType, int CBVouNo, string VoucherDate, string AccountID, string Narration, double Amount, string chequenumber, string chequedate, string bankid, string createdby, string createddate, string createdtime)
        {
            bool retValue = false;
            string strSql = GetInsertQueryForBankPayment(CBId, CBVouType, CBVouNo, VoucherDate, AccountID, Narration, Amount, chequenumber,chequedate,bankid, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;
        }
        public bool AddBankEntryDetails(string CBId, string purchaseid, string purchasevoutype, int purchasevounumber, string puchaseVouDate, double Amount)
        {
            bool retValue = false;
            string strSql = GetInsertQueryForCashPaymentDetails(CBId, purchaseid, purchasevoutype, purchasevounumber, puchaseVouDate, Amount);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;
        }
        public bool UpdateDetails(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
            string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
            double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
            double AmountAddOnFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
            double OctroiPercentage, double AmountOctroi, double AmountPurchase5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
            double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVAT, string DueDate, int NumberofChallans, int statementNumber, string voucherSubType, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(purchaseID, accountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
                SpecialDiscPer, AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
                AmountAddOnFreight, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
                OctroiPercentage, AmountOctroi, AmountPurchase5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
                PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVAT, DueDate, NumberofChallans,statementNumber,voucherSubType,modifiedby, modifieddate, modifiedtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool UpdateDetailsForTypeChange(string purchaseID, string VoucherType, int VoucherNumber, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryForTypeChange(purchaseID, VoucherType, VoucherNumber, modifiedby, modifieddate, modifiedtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public DataRow CheckProductInShortList(string productID)
        {
            string strSql = string.Format("Select *  from tbldailyshortlist where ProductID = '{0}' &&  OrderNumber =  0", productID);
            return DBInterface.SelectFirstRow(strSql);
        }
        public DataRow GetFirstAndSecondCreditor(string productID)
        {
            string strSql = string.Format("Select *  from masterproduct where ProductID = '{0}'", productID);
            return DBInterface.SelectFirstRow(strSql);
        }
        public bool FillFirstCreditorInMasterProduct(string productId, string accountID)
        {
            bool returnVal = false;
            string strSql = "Update masterproduct set prodpartyID_1 = '" + accountID + "' where  ProductID = '" + productId + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool FillSecondCreditorInMasterProduct(string productId, string accountID)
        {
            bool returnVal = false;
            string strSql = "Update masterproduct set prodpartyID_2 = '" + accountID + "' where  ProductID = '" + productId + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }
        public bool RemoveFromShortList(string productID)
        {
            bool returnVal = false;
            string strSql = "Delete from tbldailyshortlist where ProductID = '" + productID + "' && OrderNumber = 0";
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


        public bool DeleteDetails(string Id)
        {
            bool returnVal = false;
            string strSql = "Delete from voucherpurchase where PurchaseID = '" + Id + "'";
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

        public bool DeletePreviousRecordsFromtblBarCode()
        {
            bool returnVal = false;
            string strSql = "Delete from tblBarCode";
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


        public bool DeletePreviousRecords(string Id)
        {
            bool returnVal = false;
            string strSql = "Delete from detailpurchase where PurchaseID = '" + Id + "'";
            try
            {
                if (DBInterface.ExecuteQuery(strSql) > 0)
                    returnVal = true;
                else
                    returnVal = false;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }

        #region Query Building Functions       

        private string GetInsertQuery(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
            string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
            double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
            double AmountAddOnFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
            double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
            double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string voucherSubType,  string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherpurchase";
            objQuery.AddToQuery("purchaseID", purchaseID);
            objQuery.AddToQuery("EntryDate", EntryDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries );
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherSubType", voucherSubType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherDate", VoucherDate);
            objQuery.AddToQuery("PurchaseBillNumber", PurchaseBillNumber);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("AmountBalance", AmountNet-AmountClear);
            objQuery.AddToQuery("AmountClear", AmountClear);
            objQuery.AddToQuery("AmountGross", AmountGross);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
            objQuery.AddToQuery("AmountAddOnFreight", AmountAddOnFreight);
            objQuery.AddToQuery("CashDiscountPercentage", CashDiscountPercentage);
            objQuery.AddToQuery("SpecialDiscountPercentage", SpecialDiscPer);
            objQuery.AddToQuery("AmountCreditNote", AmountCreditNote);
            objQuery.AddToQuery("AmountDebitNote", AmountDebitNote);

            objQuery.AddToQuery("RoundUpAmount", RoundUpAmount);
            objQuery.AddToQuery("OctroiPercentage", OctroiPercentage);
            objQuery.AddToQuery("AmountOctroi", AmountOctroi);
            objQuery.AddToQuery("DueDate", DueDate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("AmountVAT5Percent", AmountVAT5Percent);
            objQuery.AddToQuery("AmountVAT12point5Percent", AmountVAT12Point5Percent);
            objQuery.AddToQuery("AmountVATOPercent", AmtOtherPercentVAT);


            objQuery.AddToQuery("AmountPurchaseZeroVAT", PurchaseAmountZeroVATS);
            objQuery.AddToQuery("AmountPurchase5PercentVAT", PurchaseAmount5PercentVAT);
            objQuery.AddToQuery("AmountPurchase12point5PercentVAT", PurchaseAmount12Point5PercentVAT);
          //  objQuery.AddToQuery("AmountPurchaseOPercentVAT", PurchaseAmount0PercentVAT);
            objQuery.AddToQuery("StatementNumber", StatementNumber);
            objQuery.AddToQuery("NumberofChallans", NumberofChallans);           

            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryChanged(string purchaseID, string changedID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
           string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
           double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
           double AmountAddOnFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
           double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
           double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "changedvoucherpurchase";
            objQuery.AddToQuery("purchaseID", purchaseID);
            objQuery.AddToQuery("ChangedID", changedID);
            objQuery.AddToQuery("EntryDate", EntryDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherDate", VoucherDate);
            objQuery.AddToQuery("PurchaseBillNumber", PurchaseBillNumber);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("AmountBalance", AmountNet - AmountClear);
            objQuery.AddToQuery("AmountClear", AmountClear);
            objQuery.AddToQuery("AmountGross", AmountGross);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
            objQuery.AddToQuery("AmountAddOnFreight", AmountAddOnFreight);
            objQuery.AddToQuery("CashDiscountPercentage", CashDiscountPercentage);
            objQuery.AddToQuery("SpecialDiscountPercentage", SpecialDiscPer);
            objQuery.AddToQuery("AmountCreditNote", AmountCreditNote);
            objQuery.AddToQuery("AmountDebitNote", AmountDebitNote);

            objQuery.AddToQuery("RoundUpAmount", RoundUpAmount);
            objQuery.AddToQuery("OctroiPercentage", OctroiPercentage);
            objQuery.AddToQuery("AmountOctroi", AmountOctroi);
            objQuery.AddToQuery("DueDate", DueDate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("AmountVAT5Percent", AmountVAT5Percent);
            objQuery.AddToQuery("AmountVAT12point5Percent", AmountVAT12Point5Percent);
            objQuery.AddToQuery("AmountVATOPercent", AmtOtherPercentVAT);


            objQuery.AddToQuery("AmountPurchaseZeroVAT", PurchaseAmountZeroVATS);
            objQuery.AddToQuery("AmountPurchase5PercentVAT", PurchaseAmount5PercentVAT);
            objQuery.AddToQuery("AmountPurchase12point5PercentVAT", PurchaseAmount12Point5PercentVAT);
          //  objQuery.AddToQuery("AmountPurchaseOPercentVAT", PurchaseAmount0PercentVAT);
            objQuery.AddToQuery("StatementNumber", StatementNumber);
            objQuery.AddToQuery("NumberofChallans", NumberofChallans);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryDeleted(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
                   string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
                   double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
                   double AmountAddOnFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
                   double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
                   double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans,  int StatementNumber, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "deletedvoucherpurchase";
            objQuery.AddToQuery("purchaseID", purchaseID);
            objQuery.AddToQuery("EntryDate", EntryDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherDate", VoucherDate);
            objQuery.AddToQuery("PurchaseBillNumber", PurchaseBillNumber);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("AmountBalance", AmountNet - AmountClear);
            objQuery.AddToQuery("AmountClear", AmountClear);
            objQuery.AddToQuery("AmountGross", AmountGross);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
            objQuery.AddToQuery("AmountAddOnFreight", AmountAddOnFreight);
            objQuery.AddToQuery("CashDiscountPercentage", CashDiscountPercentage);
            objQuery.AddToQuery("SpecialDiscountPercentage", SpecialDiscPer);
            objQuery.AddToQuery("AmountCreditNote", AmountCreditNote);
            objQuery.AddToQuery("AmountDebitNote", AmountDebitNote);

            objQuery.AddToQuery("RoundUpAmount", RoundUpAmount);
            objQuery.AddToQuery("OctroiPercentage", OctroiPercentage);
            objQuery.AddToQuery("AmountOctroi", AmountOctroi);
            objQuery.AddToQuery("DueDate", DueDate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("AmountVAT5Percent", AmountVAT5Percent);
            objQuery.AddToQuery("AmountVAT12point5Percent", AmountVAT12Point5Percent);
            objQuery.AddToQuery("AmountVATOPercent", AmtOtherPercentVAT);


            objQuery.AddToQuery("AmountPurchaseZeroVAT", PurchaseAmountZeroVATS);
            objQuery.AddToQuery("AmountPurchase5PercentVAT", PurchaseAmount5PercentVAT);
            objQuery.AddToQuery("AmountPurchase12point5PercentVAT", PurchaseAmount12Point5PercentVAT);
          //  objQuery.AddToQuery("AmountPurchaseOPercentVAT", PurchaseAmount0PercentVAT);
            objQuery.AddToQuery("StatementNumber", StatementNumber);
            objQuery.AddToQuery("NumberofChallans", NumberofChallans);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryForCashPayment(string CBId, string CBVouType, int CBVouNo, string VoucherDate, string AccountID, string Narration, double Amount, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankpayment";
            objQuery.AddToQuery("CBID", CBId);
            objQuery.AddToQuery("VoucherType", CBVouType);
            objQuery.AddToQuery("VoucherNumber", CBVouNo);
            objQuery.AddToQuery("VoucherDate", VoucherDate);
            objQuery.AddToQuery("AccountID", AccountID);
            objQuery.AddToQuery("AmountNet", Amount);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryForBankPayment(string CBId, string CBVouType, int CBVouNo, string VoucherDate, string AccountID, string Narration, double Amount, string chequenumber, string chequedate, string bankid, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "vouchercashbankpayment";
            objQuery.AddToQuery("CBID", CBId);
            objQuery.AddToQuery("VoucherType", CBVouType);
            objQuery.AddToQuery("VoucherNumber", CBVouNo);
            objQuery.AddToQuery("VoucherDate", VoucherDate);
            objQuery.AddToQuery("AccountID", AccountID);
            objQuery.AddToQuery("AmountNet", Amount);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("ChequeNumber", chequenumber);
            objQuery.AddToQuery("ChequeDate", chequedate);
            objQuery.AddToQuery("ChequeDepositedBankID", bankid);
            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryForCashPaymentDetails(string CBId, string purchaseid, string purchasevoutype, int purchasevounumber, string puchaseVouDate, double Amount)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailcashbankpayment";
            objQuery.AddToQuery("MasterID", CBId);
            objQuery.AddToQuery("MasterPurchaseID", purchaseid);
            objQuery.AddToQuery("BillType", purchasevoutype);
            objQuery.AddToQuery("BillNumber", purchasevounumber);
            objQuery.AddToQuery("BillDate", puchaseVouDate);
            objQuery.AddToQuery("BillAmount", Amount);
            objQuery.AddToQuery("BalanceAmount", Amount);
            objQuery.AddToQuery("ClearAmount", Amount);           
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryDetails(string Id, string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
               double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
               string IfTradeRateInclusiveOfVAT, double Amount, double amtspldisc, double spldiscper, double AmountZeroVAT, double Amountcashdiscperunit,string stockid,string mydetailpurchaseid, double productMargin,double productMargin2, int serialNumber,string scancode, double distributorRatePer)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailpurchase";
            objQuery.AddToQuery("PurchaseID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
            objQuery.AddToQuery("ReplacementQuantity", ReplacementQuantity);
            objQuery.AddToQuery("ItemDiscountPercent", ItemDiscountPercent);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("SchemeDiscountPercent", SchemeDiscountPercent);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("PurchaseVATPercent", PurchaseVATPercent);
            objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("AmountPurchaseVAT", AmountPurchaseVAT);
            objQuery.AddToQuery("AmountProdVAT", AmountProductVAT);
            objQuery.AddToQuery("CSTPercent", CSTPercent);
            objQuery.AddToQuery("AmountCST", AmountCST);
            objQuery.AddToQuery("IfMRPInclusiveOfVAT", IfMRPInclusiveOfVAT);
            objQuery.AddToQuery("IfTradeRateInclusiveOfVAT", IfTradeRateInclusiveOfVAT);          
            objQuery.AddToQuery("AmountSpecialDiscount", amtspldisc);
            objQuery.AddToQuery("SpecialDiscountPercent", spldiscper);      
            objQuery.AddToQuery("AmountCashDiscount", Amountcashdiscperunit);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("DetailPurchaseID", mydetailpurchaseid);
            objQuery.AddToQuery("Margin", productMargin);
            objQuery.AddToQuery("MarginAfterDiscount", productMargin2);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            objQuery.AddToQuery("ScanCode", scancode);
            objQuery.AddToQuery("DistributorSaleRatePer", distributorRatePer);
            return objQuery.InsertQuery();
        }


        private string GetInsertQueryChangedDetails(string Id, string changedMasterID, string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
              string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
              double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
              string IfTradeRateInclusiveOfVAT, double Amount, double amtspldisc, double spldiscper, double AmountZeroVAT, double Amountcashdiscperunit, string stockid, string mydetailpurchaseid, double productMargin, double productMargin2, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "changeddetailpurchase";
            objQuery.AddToQuery("PurchaseID", Id);
            objQuery.AddToQuery("ChangedMasterID", changedMasterID);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
            objQuery.AddToQuery("ReplacementQuantity", ReplacementQuantity);
            objQuery.AddToQuery("ItemDiscountPercent", ItemDiscountPercent);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("SchemeDiscountPercent", SchemeDiscountPercent);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("PurchaseVATPercent", PurchaseVATPercent);
            objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("AmountPurchaseVAT", AmountPurchaseVAT);
            objQuery.AddToQuery("AmountProdVAT", AmountProductVAT);
            objQuery.AddToQuery("CSTPercent", CSTPercent);
            objQuery.AddToQuery("AmountCST", AmountCST);
            objQuery.AddToQuery("IfMRPInclusiveOfVAT", IfMRPInclusiveOfVAT);
            objQuery.AddToQuery("IfTradeRateInclusiveOfVAT", IfTradeRateInclusiveOfVAT);
            objQuery.AddToQuery("AmountSpecialDiscount", amtspldisc);
            objQuery.AddToQuery("SpecialDiscountPercent", spldiscper);
            objQuery.AddToQuery("AmountCashDiscount", Amountcashdiscperunit);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("DetailPurchaseID", mydetailpurchaseid);
            objQuery.AddToQuery("Margin", productMargin);
            objQuery.AddToQuery("MarginAfterDiscount", productMargin2);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryDeletedDetails(string Id, string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
              string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
              double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
              string IfTradeRateInclusiveOfVAT, double Amount, double amtspldisc, double spldiscper, double AmountZeroVAT, double Amountcashdiscperunit, string stockid, string mydetailpurchaseid, double productMargin, double productMargin2, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "deleteddetailpurchase";
            objQuery.AddToQuery("PurchaseID", Id);
            objQuery.AddToQuery("ProductID", ProductID);
            objQuery.AddToQuery("BatchNumber", Batchno);
            objQuery.AddToQuery("TradeRate", TradeRate);
            objQuery.AddToQuery("PurchaseRate", PurchaseRate);
            objQuery.AddToQuery("MRP", MRP);
            objQuery.AddToQuery("SaleRate", SaleRate);
            objQuery.AddToQuery("Expiry", Expiry);
            objQuery.AddToQuery("ExpiryDate", ExpiryDate);
            objQuery.AddToQuery("Quantity", Quantity);
            objQuery.AddToQuery("SchemeQuantity", SchemeQuantity);
            objQuery.AddToQuery("ReplacementQuantity", ReplacementQuantity);
            objQuery.AddToQuery("ItemDiscountPercent", ItemDiscountPercent);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("SchemeDiscountPercent", SchemeDiscountPercent);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("PurchaseVATPercent", PurchaseVATPercent);
            objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("AmountPurchaseVAT", AmountPurchaseVAT);
            objQuery.AddToQuery("AmountProdVAT", AmountProductVAT);
            objQuery.AddToQuery("CSTPercent", CSTPercent);
            objQuery.AddToQuery("AmountCST", AmountCST);
            objQuery.AddToQuery("IfMRPInclusiveOfVAT", IfMRPInclusiveOfVAT);
            objQuery.AddToQuery("IfTradeRateInclusiveOfVAT", IfTradeRateInclusiveOfVAT);
            objQuery.AddToQuery("AmountSpecialDiscount", amtspldisc);
            objQuery.AddToQuery("SpecialDiscountPercent", spldiscper);
            objQuery.AddToQuery("AmountCashDiscount", Amountcashdiscperunit);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("DetailPurchaseID", mydetailpurchaseid);
            objQuery.AddToQuery("Margin", productMargin);
            objQuery.AddToQuery("MarginAfterDiscount", productMargin2);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            return objQuery.InsertQuery();
        }



        private string GetInsertQueryDetailsInStockTable(string ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
              string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double PurchaseVATPercent,
              double ProductVATPercent, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount,
              string accountId, string billnumber, string voutype, int vounumber, string voudate, int ProdLoosePack, string StockId, double productMargin, string purScanCode, double distSaleRate, double distSaleRatePer)
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
            objQuery.AddToQuery("ClosingStock", (Quantity + SchemeQuantity ) * ProdLoosePack);
            objQuery.AddToQuery("PurchaseStock", (Quantity) * ProdLoosePack);
            objQuery.AddToQuery("PurchaseSchemeStock", (SchemeQuantity) * ProdLoosePack);
            objQuery.AddToQuery("PurchaseReplacementStock", (ReplacementQuantity) * ProdLoosePack);
            objQuery.AddToQuery("PurchaseVATPercent", PurchaseVATPercent);
            objQuery.AddToQuery("ProductVATPercent", ProductVATPercent);
            objQuery.AddToQuery("LastPurchaseAccountId", accountId);
            objQuery.AddToQuery("LastPurchaseBillNumber", billnumber);
            objQuery.AddToQuery("LastPurchaseVoucherType", voutype);
            objQuery.AddToQuery("LastPurchaseVoucherNumber", vounumber);
            objQuery.AddToQuery("LastPurchaseDate", voudate);
            objQuery.AddToQuery("OpeningStock", 0);
            objQuery.AddToQuery("BeginningStock", 0);
            objQuery.AddToQuery("IfRateCorrection", "");
            objQuery.AddToQuery("ScanCode", purScanCode);
            objQuery.AddToQuery("TransferInStock", 0);
            objQuery.AddToQuery("CreditNoteStock", 0);
            objQuery.AddToQuery("SaleStock", 0);
            objQuery.AddToQuery("TransferOutStock", 0);
            objQuery.AddToQuery("DebitNoteStock", 0);
            objQuery.AddToQuery("SaleSchemeStock", 0);
            objQuery.AddToQuery("StockID", StockId);
            objQuery.AddToQuery("Margin", productMargin);

            objQuery.AddToQuery("DistributorSaleRate", distSaleRate);
            objQuery.AddToQuery("DistributorSaleRatePer", distSaleRatePer);

            return objQuery.InsertQuery();
        }       


        private string GetUpdateQuery(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
          string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
          double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
          double AmountAddOnFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
          double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
          double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double purchaseAmountZeroVat, string DueDate, int NumberofChallans, int statementNumber, string voucherSubType, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherpurchase";
            objQuery.AddToQuery("purchaseID", purchaseID, true);
        //    objQuery.AddToQuery("EntryDate", EntryDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherSubType", voucherSubType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherDate", VoucherDate);
            objQuery.AddToQuery("PurchaseBillNumber", PurchaseBillNumber);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("AmountClear", AmountClear);
            objQuery.AddToQuery("AmountBalance", AmountNet - AmountClear);
            objQuery.AddToQuery("AmountGross", AmountGross);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
            objQuery.AddToQuery("AmountAddOnFreight", AmountAddOnFreight);
            objQuery.AddToQuery("CashDiscountPercentage", CashDiscountPercentage);
            objQuery.AddToQuery("SpecialDiscountPercentage", SpecialDiscPer);
            objQuery.AddToQuery("AmountCreditNote", AmountCreditNote);
            objQuery.AddToQuery("AmountDebitNote", AmountDebitNote);
            objQuery.AddToQuery("RoundUpAmount", RoundUpAmount);
            objQuery.AddToQuery("OctroiPercentage", OctroiPercentage);
            objQuery.AddToQuery("AmountOctroi", AmountOctroi);          
            objQuery.AddToQuery("Narration", Narration);

            objQuery.AddToQuery("AmountVAT5Percent", AmountVAT5Percent);
            objQuery.AddToQuery("AmountVAT12point5Percent", AmountVAT12Point5Percent);
            objQuery.AddToQuery("AmountVATOPercent", AmtOtherPercentVAT);
            objQuery.AddToQuery("AmountPurchaseZeroVAT", purchaseAmountZeroVat);
            objQuery.AddToQuery("AmountPurchase12point5PercentVAT", PurchaseAmount12Point5PercentVAT);
            objQuery.AddToQuery("AmountPurchase5PercentVAT", PurchaseAmount5PercentVAT);
            objQuery.AddToQuery("NumberofChallans", NumberofChallans);
            objQuery.AddToQuery("StatementNumber", statementNumber);

            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }

          private string GetUpdateQueryForTypeChange(string purchaseID, string  VoucherType, int VoucherNumber, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherpurchase";
            objQuery.AddToQuery("purchaseID", purchaseID, true);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }



        private string GetDeleteQuery(string Id)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherpurchase";
            objQuery.AddToQuery("purchaseID", Id, true);
            return objQuery.DeleteQuery();
        }

        #endregion

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.VoucherSubType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,b.AccName,b.AccAddress1,b.AccAddress2,a.AmountClear,(a.AmountVAT5Percent+a.AmountVAT12Point5Percent) as AmountVAT from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.VoucherSubType = '1'  order by a.voucherdate desc , a.vouchernumber desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForWithoutStockSearch()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.VoucherSubType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,b.AccName,b.AccAddress1,b.AccAddress2,a.AmountClear,(a.AmountVAT5Percent+a.AmountVAT12Point5Percent) as AmountVAT from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.VoucherSubType = '2'  order by voucherdate desc , vouchernumber desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPurchaseRegister(string fromdate, string todate, string voutype)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear,(AmountVAT5Percent+AmountVAT12Point5Percent) as AmountVAT from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.voucherdate >= '" + fromdate + "' && a.voucherdate <= '" + todate + "' && a.vouchertype = '" + voutype + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPurchaseRegister(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear,(AmountVAT5Percent+AmountVAT12Point5Percent) as AmountVAT from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.voucherdate >= '" + fromdate + "' && a.voucherdate <= '" + todate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForVATReport(string fromdate, string todate, string voutype)
        {
            DataTable dtable = new DataTable();
            string strSql = "";
            if (voutype == string.Empty)
            {
                strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountCreditNote) as TotalLess,(AmountAddOnFreight+AmountDebitNote) as TotalAdd,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,AmountItemDiscount,AmountSpecialDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountCreditNote,AmountAddOnFreight,AmountDebitNote,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.voucherdate >= '" + fromdate + "' && a.voucherdate <= '" + todate + "'order by VoucherNumber";
            }
            else
            {
                strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountCreditNote) as TotalLess,(AmountAddOnFreight+AmountDebitNote) as TotalAdd,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,AmountItemDiscount,AmountSpecialDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountCreditNote,AmountAddOnFreight,AmountDebitNote,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.voucherdate >= '" + fromdate + "' && a.voucherdate <= '" + todate + "' && a.voucherType = '"+ voutype +"' order by VoucherNumber";
            }
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATReportOtherDetails(string fromdate, string todate, string voutype)
        {
            DataTable dtable = new DataTable();
            string strSql = "";
            if (voutype == string.Empty)
            {
                strSql = "Select purchaseID,VoucherNumber,VoucherType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount) as AmountDiscount,AmountAddOnFreight,AmountCreditNote,AmountDebitNote,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent,AmountItemDiscount,AmountSpecialDiscount,AmountSchemeDiscount,AmountCashDiscount from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && AmountCashDiscount+AmountSchemeDiscount+AmountSpecialDiscount+AmountItemDiscount+AmountAddOnFreight+AmountCreditNote+AmountDebitNote > 0 && a.voucherdate >= '" + fromdate + "' && a.voucherdate <= '" + todate + "'order by VoucherNumber";
            }
            else
            {
                strSql = "Select purchaseID,VoucherNumber,VoucherType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount) as AmountDiscount,AmountAddOnFreight,AmountCreditNote,AmountDebitNote,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent,AmountItemDiscount,AmountSpecialDiscount,AmountSchemeDiscount,AmountCashDiscount from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && AmountCashDiscount+AmountSchemeDiscount+AmountSpecialDiscount+AmountItemDiscount+AmountAddOnFreight+AmountCreditNote+AmountDebitNote > 0 && a.voucherdate >= '" + fromdate + "' && a.voucherdate <= '" + todate + "' && a.voucherType = '" + voutype + "' order by VoucherNumber";
            }
          //  string strSql = "Select purchaseID,VoucherNumber,VoucherType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount) as AmountDiscount,AmountAddOnFreight,AmountCreditNote,AmountDebitNote,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent,AmountItemDiscount,AmountSpecialDiscount,AmountSchemeDiscount,AmountCashDiscount from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && AmountCashDiscount+AmountSchemeDiscount+AmountSpecialDiscount+AmountItemDiscount+AmountAddOnFreight+AmountCreditNote+AmountDebitNote > 0 order by VoucherType,VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForVATReportDATE(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select voucherType,VoucherDate,sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOnFreight+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase where VoucherDate >= '" + mfromdate + "' && VoucherDate <= '" + mtodate + "'  group by vouchertype,VoucherDate order by VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATReportDATEALL()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select voucherType,VoucherDate,sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOnFreight+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase   group by VoucherDate order by VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATReportMONTH(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select voucherType,VoucherDate,sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOnFreight+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase where VoucherDate >= '" + mfromdate + "' && VoucherDate <= '" + mtodate + "'  group by vouchertype, substring(VoucherDate,5,2) order by VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATReportMONTHALL(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select voucherType,VoucherDate, sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOnFreight+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase where VoucherDate >= '" + mfromdate + "' && VoucherDate <= '" + mtodate + "'  group by substr(VoucherDate,5,2) order by VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATReportTIN(string mfromdate, string mtodate )
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.VoucherDate,sum(a.AmountPurchase5PercentVAT + a.AmountPurchase12point5PercentVAT) as TotalAmount,sum(a.AmountVAT5Percent + a.AmountVAT12Point5Percent) as TotalVAT,b.AccountID,b.AccName,b.AccAddress1,b.AccVATTINNumber  from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.VoucherDate >= '"+ mfromdate  +"' && a.VoucherDate <= '"+ mtodate  +"'  group by b.AccountID  order by TotalVAT desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetProductPurchased(string purchaseID)
        {
            DataTable dtable = null;
            string strSql = string.Format("Select *, '0' as 'Amount' from masterproduct mp, detailpurchase dp where mp.ProductID = dp.ProductID and purchaseID = '{0}'", purchaseID);
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForProductList()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID  order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetPurchaseDataProductWise(string productID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate,a.TradeRate, (a.TradeRate * a.Quantity) as TAmount, (a.PurchaseRate * a.Quantity) as PAmount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where  a.ProductID = '" + productID + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetPurchaseDataProductWiseWithScheme(string productID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where a.SchemeQuantity > 0 &&  a.ProductID = '" +productID +"' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForProductBatchList(string productid, string mbatchno, double mrp)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.MRP,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where a.ProductId = '" + productid + "' && a.BatchNumber = '" + mbatchno + "' && a.MRP = "+ mrp +"  order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPartyProductList(string partyid, string productid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where c.AccountID = '" + partyid + "' && a.ProductId = '" + productid + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPartywiseBills(string partyid, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,a.AccountID,c.AccountID,c.AccName " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where a.AccountID = '" + partyid + "' && a.voucherdate >= '" + fromDate + "' &&  a.voucherdate <= '" + toDate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPartywiseBillsForStatements(string partyid, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate ,a.AmountNet , a.AmountVAT5Percent,a.AmountVAT12point5Percent " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where a.AccountID = '" + partyid + "' && a.voucherdate >= '" + fromDate + "' &&  a.voucherdate <= '" + toDate + "' && a.StatementNumber = 0  && AmountClear = 0 order by VoucherType, VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPartywiseStatementsView(int statementNumber, string voucherSeries)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate ,a.AmountNet , a.AmountVAT5Percent,a.AmountVAT12point5Percent " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where a.statementnumber = "+ statementNumber +" && voucherseries = '"+ voucherSeries + "' order by VoucherType, VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
      
        public DataTable GetOverviewDataForDiscount(string mfromDate, string mtoDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,a.AmountItemDiscount,a.AmountSpecialDiscount,a.AmountSchemeDiscount,a.AmountCashDiscount,a.AccountID,c.AccountID,c.AccName " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where (a.AmountItemDiscount > 0 || a.AmountSpecialDiscount > 0 || a.amountSchemeDiscount > 0 || a.AmountCashDiscount > 0 ) && (a.VoucherDate >= '" + mfromDate + "' && a.VoucherDate <= '"+ mtoDate +"' ) order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForAllPartySummary(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.voucherdate, sum(a.AmountNet) as AmountNet,a.AccountID,c.AccountID,c.AccName " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID  where a.voucherdate >= '" + fromdate + "' &&  a.voucherdate <= '" + todate + "'  group by a.AccountID  order by c.AccName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataCategory(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select sum(a.Quantity *a.TradeRate) as AmountNet,a.ProductId, sum(a.AmountItemDiscount) as AmountItemDiscount, " +
                 "sum(a.AmountSchemeDiscount) as AmountSchemeDiscount, sum(a.AmountSpecialDiscount) as AmountSpecialDiscount, " +
                 "sum(a.AmountCashDiscount) as AmountCashDiscount, b.ProductID,b.ProdCategoryID,c.ProductCategoryID,c.ProductCategoryName,d.voucherdate from detailpurchase a inner join masterproduct b on a.ProductID = b.ProductID inner join masterproductcategory c on b.ProdCategoryID = c.ProductCategoryID inner join voucherpurchase d on a.PurchaseID = d.PurchaseID  where d.voucherdate >= '" + mfromdate + "' && d.voucherdate <= '"+ mtodate +"' group by c.ProductCategoryID order by c.ProductCategoryName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataCompany(string companyid, string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.ProductID,a.BatchNumber,a.Expiry,a.MRP,a.Quantity,(a.SchemeQuantity + a.ReplacementQuantity) as SchemeQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount,b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName, " +
                 " d.ProductID,d.ProdName,d.ProdLoosePack,d.ProdPack " +
                 "from detailpurchase a  inner join voucherpurchase b on a.purchaseID = b.purchaseID inner join masteraccount c on b.AccountID = c.AccountID inner join masterproduct d on a.productId = d.ProductID where d.ProdCompID = '" + companyid + "' && b.Voucherdate >= '"+ mfromdate +"' && b.Voucherdate <='"+ mtodate  + "' order by d.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataNewProducts(string mfromdate, string mtodate )
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.ProductID,a.BatchNumber,a.Expiry,a.MRP,a.Quantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount,b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName, " +
                 " d.ProductID,d.ProdName,d.ProdLoosePack,d.ProdPack " +
                 "from detailpurchase a  inner join voucherpurchase b on a.purchaseID = b.purchaseID inner join masteraccount c on b.AccountID = c.AccountID inner join masterproduct d on a.productId = d.ProductID where  d.CreatedDate >= '"+ General.ShopDetail.Shopsy + "' &&  b.Voucherdate >= '"+ mfromdate +"' && b.Voucherdate <='"+ mtodate  + "'  order by d.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForLastPurchase(string productID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate,a.MRP,a.Margin ,a.MarginAfterDiscount," +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where a.ProductID = '"+ productID+"' order by b.VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from voucherpurchase where purchaseID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByIDForChanged(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from changedvoucherpurchase where changedID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByIDForDeleted(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from deletedvoucherpurchase where purchaseID='{0}' ";
                strSql = string.Format(strSql, Id);
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow ReadDetailsByVouNumber(int vouno, string voutype)
        {
            DataRow dRow = null;
            
           
                string strSql = "Select * from voucherpurchase where VoucherNumber = " + vouno + " && VoucherType = '" + voutype + "'";
                dRow = DBInterface.SelectFirstRow(strSql);
           
            return dRow;
        }

        public DataTable ReadPaymentDetailsByID(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select a.MasterID, a.MasterPurchaseID,a.ClearAmount,b.VoucherType, " +
                "b.VoucherNumber,b.VoucherDate,b.CBID, " +
                "c.purchaseID from detailcashbankpayment a inner join  vouchercashbankpayment b  on a.MasterID = b.CBID  inner join voucherpurchase c on a.MasterPurchaseID = c.purchaseID where a.MasterPurchaseID = '{0}'";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataTable ReadProductDetailsByIDPurchase(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select  a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdCompID,a.ProdIfOctroi,a.ProdShelfID,a.ProdIfBarCodeRequired,b.ProductID, " +
                "b.StockID,b.PurchaseID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.AmountProdVAT," +
                "b.PurchaseVATPercent,b.SchemeQuantity,b.ReplacementQuantity,b.AmountItemDiscount,b.ItemDiscountPercent," +
                "b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.SpecialDiscountPercent,b.AmountPurchaseVAT,b.CSTPercent,b.AmountCST,b.AmountCreditNote,b.AmountCashDiscount,b.AmountSpecialDiscount,b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount,b.DistributorSaleRatePer, (b.TradeRate*b.DistributorSaleRatePer) as DistributorSaleRate, " +
                "d.ClosingStock,d.ScanCode,e.ShelfCode from masterproduct a inner join  detailpurchase b  on a.ProductId = b.ProductID left outer join tblstock d on b.StockID = d.StockID  left outer join mastershelf e on  a.ProdShelfID = e.shelfID where b.PurchaseID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }

        public DataTable ReadProductDetailsByIDPurchaseForChanged(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select  a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdCompID,a.ProdIfOctroi,a.ProdShelfID,b.ProductID, " +
                "b.StockID,b.PurchaseID,b.ChangedMasterID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.AmountProdVAT," +
                "b.PurchaseVATPercent,b.SchemeQuantity,b.ReplacementQuantity,b.AmountItemDiscount,b.ItemDiscountPercent," +
                "b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.SpecialDiscountPercent,b.AmountPurchaseVAT,b.CSTPercent,b.AmountCST,b.AmountCreditNote,b.AmountCashDiscount,b.AmountSpecialDiscount,b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount, b.scancode," +
                "d.ClosingStock,e.ShelfCode from masterproduct a inner join  changeddetailpurchase b  on a.ProductId = b.ProductID left outer join tblstock d on b.StockID = d.StockID  left outer join mastershelf e on  a.ProdShelfID = e.shelfID where b.ChangedMasterID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataTable ReadProductDetailsByIDPurchaseForDeleted(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select  a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdCompID,a.ProdIfOctroi,a.ProdShelfID,b.ProductID, " +
                "b.StockID,b.PurchaseID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.AmountProdVAT," +
                "b.PurchaseVATPercent,b.SchemeQuantity,b.ReplacementQuantity,b.AmountItemDiscount,b.ItemDiscountPercent," +
                "b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.SpecialDiscountPercent,b.AmountPurchaseVAT,b.CSTPercent,b.AmountCST,b.AmountCreditNote,b.AmountCashDiscount,b.AmountSpecialDiscount,b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount, " +
                "d.ClosingStock,e.ShelfCode from masterproduct a inner join  deleteddetailpurchase b  on a.ProductId = b.ProductID left outer join tblstock d on b.StockID = d.StockID  left outer join mastershelf e on  a.ProdShelfID = e.shelfID where b.PurchaseID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public bool CheckforUniqueBillNumberforEdit(string Id, string PurBillNumber, string AccID)
        {
            bool retVal = true;
            string strSql = "";
            DataTable dt = new DataTable();
            DataRow dr = null;
            strSql = "select * from voucherpurchase where accountID = '" + AccID + "' && PurchaseBillNumber = '" + PurBillNumber + "' && PurchaseID != '" + Id + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
            {
                retVal = false;
            }

            return retVal;
        }
        public bool CheckforUniqueBillNumberforNew(string PurBillNumber, string AccID)
        {
            bool retVal = true;
            string strSql = "";
            DataRow dr = null;
            DataTable dt = new DataTable();
            strSql = "select * from voucherpurchase where accountID = '" + AccID + "' && PurchaseBillNumber = '" + PurBillNumber + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
            {
                retVal = false;
            }

            return retVal;
        }

        public bool CheckForStatementOver(string VoucherDate)
        {
            bool retVal = true;
            string strSql = "";
            DataTable dt = new DataTable();
            strSql = "select * from voucherpurchase where voucherDate >= '"+ VoucherDate + "' && voucherDate <= '" + VoucherDate + "' && statementnumber > 0";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retVal = false;
            }

            return retVal;
        }
    }
}
