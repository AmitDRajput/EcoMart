using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;
using EcoMart.DataLayer;


namespace EcoMart.DataLayer
{

public class DBPurchase
    {
        public DBPurchase()
        {

        }

        public DataTable GetPurchase()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select purchaseID,EntryDate,VoucherSeries,VoucherType,VoucherSubType,VoucherNumber,VoucherDate,PurchaseBillNumber,AccountID,AmountNet,AmountClear,AmountGross,AmountItemDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountAddOn,CashDiscountPercentage,AmountCreditNote,AmountDebitNote,StatementNumber,RoundUpAmount,OctroiPercentage,AmountOctroi,DueDate,Narration,AmountPurchase4PercentVAT,AmountVAT4Percent,PurchaseAmount12.5PercentVAT,AmountVAT12.5Percent, AmountPurchaseZeroVAT,NumberofChallans,CreatedDate,CreatedUserId,ModifiedDate,ModifiedUserId";
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
            string strSql = "Select sum(AmountNet) as AmountNet from vouchercreditdebitnote where AmountClear = 0 AND (VoucherType = '"+ FixAccounts.VoucherTypeForDebitNoteAmount +"' OR VoucherType = '"+ FixAccounts.VoucherTypeForDebitNoteStock +"')  AND AccountID = '" + accountid + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }
        public int AddDetails(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
            string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
            double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
            double AmountAddOn, double AmountFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
            double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
            double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string voucherSubType,
             double gstAmt0, double gstAmtS5, double gstAmtS12, double gstAmtS18, double gstAmtS28,
            double gstAmtC5, double gstAmtC12, double gstAmtC18, double gstAmtC28, double gsts5, double gsts12, double gsts18, double gsts28,
            double gstc5, double gstc12, double gstc18, double gstc28, double gstAmtI5, double gstAmtI12, double gstAmtI18, double gstAmtI28, double gstI5, double gstI12, double gstI18, double gstI28, string createdby, string createddate, string createdtime)
        {
            //bool bRetValue = false;
            string strSql = GetInsertQuery(purchaseID, accountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
                SpecialDiscPer , AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
                AmountAddOn,AmountFreight, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
                OctroiPercentage, AmountOctroi, PurchaseAmount5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
                PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, voucherSubType,
                 gstAmt0, gstAmtS5, gstAmtS12, gstAmtS18, gstAmtS28, gstAmtC5, gstAmtC12, gstAmtC18, gstAmtC28, gsts5,
                gsts12, gsts18, gsts28, gstc5, gstc12, gstc18, gstc28, gstAmtI5, gstAmtI12, gstAmtI18, gstAmtI28, gstI5, gstI12, gstI18, gstI28, createdby, createddate, createdtime);
            //strSql += ";select last_insert_ID()";
            int ii = Convert.ToInt32(DBInterface.ExecuteScalar(strSql)) ;                     
            return ii;
        }

        public int AddChangedDetails(string purchaseID, string changedID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
           string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
           double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
           double AmountAddOn, double AmountFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
           double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
           double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string modifiedby, string modifieddate, string modifiedtime)
        {
            int iRetValue = 0;
            string strSql = GetInsertQueryChanged(purchaseID,changedID,accountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
                SpecialDiscPer, AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
                AmountAddOn, AmountFreight, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
                OctroiPercentage, AmountOctroi, PurchaseAmount5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
                PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, modifiedby, modifieddate, modifiedtime);
            iRetValue = DBInterface.ExecuteScalar(strSql);
            //if (DBInterface.ExecuteQuery(strSql) > 0)
            //{
            //    bRetValue = true;
            //}

            return iRetValue;
        }

        public bool AddDeletedDetails(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
           string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
           double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
           double AmountAddOn,double AmountFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
           double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
           double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDeleted(purchaseID, accountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
                SpecialDiscPer, AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
                AmountAddOn, AmountFreight, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
                OctroiPercentage, AmountOctroi, PurchaseAmount5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
                PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        public bool AddDetailsProductsSS(int Id, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
               double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
               string IfTradeRateInclusiveOfVAT, double Amount, double spldiscamt, double spldiscper, double AmountZeroVAT, double AmountCashDiscountperunit, int stockid, string mydetailpurchaseid,
               double productMargin,double productMargin2,int serialNumber,string scancode, double gstPurchaseAmountZero, double gstSPurchaseAmount, double gstCPurchaseAmount,
               double gstSAmount, double gstCAmount,double pricetoretailer,double profitpercent)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetails(Id, ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercent,
                    AmountSchemeDiscount, PurchaseVATPercent, ProductVATPercent, AmountPurchaseVAT, AmountProductVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT,
                    IfTradeRateInclusiveOfVAT, Amount, spldiscamt, spldiscper, AmountZeroVAT, AmountCashDiscountperunit,stockid,mydetailpurchaseid,productMargin,productMargin2,
                    serialNumber,scancode, gstPurchaseAmountZero,gstSPurchaseAmount, gstCPurchaseAmount, gstSAmount, gstCAmount,pricetoretailer,profitpercent);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }


        public bool AddChangedDetailsProductsSS(string Id,string changedMasterID, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
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

        public bool AddDeletedDetailsProductsSS(string Id, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
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



        public int AddProductDetailsInStockTable(int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
            string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double PurchaseVATPercent,
            double ProductVATPercent, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount,
            string accountId, string billnumber, string voutype, int vounumber, string voudate, int ProdLoosePack,string StockId,double productMargin,string purScanCode,double pricetoretailer,double profitpercent)
        {
            //bool bRetValue = false;
            string strSql = GetInsertQueryDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, PurchaseVATPercent,
                    ProductVATPercent, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount,
                    accountId, billnumber, voutype, vounumber, voudate, ProdLoosePack,StockId, productMargin,purScanCode,pricetoretailer,profitpercent);
            //strSql += "; select last_insert_ID()";
            int iid = Convert.ToInt32(DBInterface.ExecuteScalar(strSql));
            //if (iid > 0)
            //{
            //    bRetValue = true;
            //}
            return iid;
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
            double AmountAddOn, double AmountFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
            double OctroiPercentage, double AmountOctroi, double AmountPurchase5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
            double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVAT, string DueDate, int NumberofChallans, int statementNumber, string voucherSubType,
            double gstAmt0, double gstAmtS5, double gstAmtS12, double gstAmtS18, double gstAmtS28,
            double gstAmtC5, double gstAmtC12, double gstAmtC18, double gstAmtC28, double gsts5, double gsts12, double gsts18, double gsts28,
            double gstc5, double gstc12, double gstc18, double gstc28, double gstAmtI5, double gstAmtI12, double gstAmtI18, double gstAmtI28, double gstI5, double gstI12, double gstI18, double gstI28, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(purchaseID, accountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
                SpecialDiscPer, AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
                AmountAddOn,AmountFreight, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
                OctroiPercentage, AmountOctroi, AmountPurchase5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
                PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVAT, DueDate, NumberofChallans,statementNumber,voucherSubType,
                gstAmt0, gstAmtS5, gstAmtS12, gstAmtS18, gstAmtS28, gstAmtC5, gstAmtC12, gstAmtC18, gstAmtC28, gsts5,
                gsts12, gsts18, gsts28, gstc5, gstc12, gstc18, gstc28, gstAmtI5, gstAmtI12, gstAmtI18, gstAmtI28, gstI5, gstI12, gstI18, gstI28, modifiedby, modifieddate, modifiedtime);
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

        public DataRow CheckProductInShortList(int ProductID)
        {
            string strSql = string.Format("Select *  from tbldailyshortlist where ProductID = '{0}' AND  OrderNumber =  0", ProductID);
            return DBInterface.SelectFirstRow(strSql);
        }
        public DataRow GetFirstAndSecondCreditor(int ProductID)
        {
            string strSql = string.Format("Select *  from masterproduct where ProductID = '{0}'", ProductID);
            return DBInterface.SelectFirstRow(strSql);
        }
        public bool FillFirstCreditorInMasterProduct(int ProductID, string accountID)
        {
            bool returnVal = false;
            string strSql = "Update masterproduct set prodpartyID_1 = '" + accountID + "' where  ProductID = '" + ProductID + "'";
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
        public bool FillSecondCreditorInMasterProduct(int ProductID, string accountID)
        {
            bool returnVal = false;
            string strSql = "Update masterproduct set prodpartyID_2 = '" + accountID + "' where  ProductID = '" + ProductID + "'";
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
        public bool RemoveFromShortList(int ProductID)
        {
            bool returnVal = false;
            string strSql = "Delete from tbldailyshortlist where ProductID = '" + ProductID + "' AND OrderNumber = 0";
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

        public DataRow GetMaxID()
        {
            DataRow dRow = null;

            string strSql = "Select max(purchaseID) as maxid from voucherpurchase ";
            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }
        public DataRow GetChangedMaxID()
        {
            DataRow dRow = null;

            string strSql = "Select max(ChangedID) as maxid from changedvoucherpurchase ";
            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }
        #region Query Building Functions       

        private string GetInsertQuery(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
            string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
            double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
            double AmountAddOn, double AmountFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
            double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
            double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans,  int StatementNumber, string voucherSubType,
            double gstAmt0, double gstAmtS5, double gstAmtS12, double gstAmtS18, double gstAmtS28,
            double gstAmtC5, double gstAmtC12, double gstAmtC18, double gstAmtC28, double gsts5, double gsts12, double gsts18, double gsts28,
            double gstc5, double gstc12, double gstc18, double gstc28, double gstAmtI5, double gstAmtI12, double gstAmtI18, double gstAmtI28, double gstI5, double gstI12, double gstI18, double gstI28, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherpurchase";
          //  objQuery.AddToQuery("purchaseID", purchaseID);
          //  objQuery.AddToQuery("EntryDate", EntryDate);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries );
            objQuery.AddToQuery("VoucherType", VoucherType);
            //objQuery.AddToQuery("VoucherSubType", voucherSubType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherDate", VoucherDate);
            objQuery.AddToQuery("PurchaseBillNumber", PurchaseBillNumber);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("AmountNet", AmountNet);
            objQuery.AddToQuery("AmountBalance", AmountNet-AmountClear);
            objQuery.AddToQuery("AmountClear", AmountClear);
            objQuery.AddToQuery("AmountGross", AmountGross);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            //objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
            objQuery.AddToQuery("AmountAddOn", AmountAddOn);
            objQuery.AddToQuery("AmountFreight", AmountFreight);
            objQuery.AddToQuery("CashDiscountPercentage", CashDiscountPercentage);
            //objQuery.AddToQuery("SpecialDiscountPercentage", SpecialDiscPer);
            objQuery.AddToQuery("AmountCreditNote", AmountCreditNote);
            objQuery.AddToQuery("AmountDebitNote", AmountDebitNote);

            objQuery.AddToQuery("RoundUpAmount", RoundUpAmount);
         //   objQuery.AddToQuery("OctroiPercentage", OctroiPercentage);
        //    objQuery.AddToQuery("AmountOctroi", AmountOctroi);
          //  objQuery.AddToQuery("DueDate", DueDate);
            objQuery.AddToQuery("Narration", Narration);
          //  objQuery.AddToQuery("AmountVAT5Percent", AmountVAT5Percent);
         //   objQuery.AddToQuery("AmountVAT12point5Percent", AmountVAT12Point5Percent);
          //  objQuery.AddToQuery("AmountVATOPercent", AmtOtherPercentVAT);


          //  objQuery.AddToQuery("AmountPurchaseZeroVAT", PurchaseAmountZeroVATS);
          //  objQuery.AddToQuery("AmountPurchase5PercentVAT", PurchaseAmount5PercentVAT);
         //   objQuery.AddToQuery("AmountPurchase12point5PercentVAT", PurchaseAmount12Point5PercentVAT);
          //  objQuery.AddToQuery("AmountPurchaseOPercentVAT", PurchaseAmount0PercentVAT);
            //objQuery.AddToQuery("StatementNumber", StatementNumber);
            //objQuery.AddToQuery("NumberofChallans", NumberofChallans);

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

        private string GetInsertQueryChanged(string purchaseID, string changedID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
           string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
           double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
           double AmountAddOn, double AmountFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
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
            //objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
            objQuery.AddToQuery("AmountAddOn", AmountAddOn);
            objQuery.AddToQuery("AmountFreight", AmountFreight);
            objQuery.AddToQuery("CashDiscountPercentage", CashDiscountPercentage);
            //objQuery.AddToQuery("SpecialDiscountPercentage", SpecialDiscPer);
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
                   double AmountAddOn, double AmountFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
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
            //objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
            objQuery.AddToQuery("AmountAddOn", AmountAddOn);
            objQuery.AddToQuery("AmountFreight", AmountFreight);
            objQuery.AddToQuery("CashDiscountPercentage", CashDiscountPercentage);
            //objQuery.AddToQuery("SpecialDiscountPercentage", SpecialDiscPer);
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

        private string GetInsertQueryDetails(int Id, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
               double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
               string IfTradeRateInclusiveOfVAT, double Amount, double amtspldisc, double spldiscper, double AmountZeroVAT, double Amountcashdiscperunit,int stockid,string mydetailpurchaseid, double productMargin,
               double productMargin2, int serialNumber,string scancode, double gstPurchaseAmountZero, double gstSPurchaseAmount, double gstCPurchaseAmount,
               double gstSAmount, double gstCAmount,double pricetoretailer,double profitpercent)
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
            //objQuery.AddToQuery("CSTPercent", CSTPercent);
            //objQuery.AddToQuery("AmountCST", AmountCST);
            //objQuery.AddToQuery("IfMRPInclusiveOfVAT", IfMRPInclusiveOfVAT);
            //objQuery.AddToQuery("IfTradeRateInclusiveOfVAT", IfTradeRateInclusiveOfVAT);          
            //objQuery.AddToQuery("AmountSpecialDiscount", amtspldisc);
            //objQuery.AddToQuery("SpecialDiscountPercent", spldiscper);      
            objQuery.AddToQuery("AmountCashDiscount", Amountcashdiscperunit);
            objQuery.AddToQuery("StockID", stockid);
         //   objQuery.AddToQuery("DetailPurchaseID", mydetailpurchaseid);
            objQuery.AddToQuery("Margin", productMargin);
            objQuery.AddToQuery("MarginAfterDiscount", productMargin2);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            objQuery.AddToQuery("ScanCode", scancode);

            objQuery.AddToQuery("GSTAmountZero", gstPurchaseAmountZero);
            objQuery.AddToQuery("GSTSAmount", gstSPurchaseAmount);
            objQuery.AddToQuery("GSTCAmount", gstCPurchaseAmount);
            objQuery.AddToQuery("GSTS", gstSAmount);
            objQuery.AddToQuery("GSTC", gstCAmount);
            objQuery.AddToQuery("PriceToRetailer", pricetoretailer);
            objQuery.AddToQuery("ProfitPercent", profitpercent);
            return objQuery.InsertQuery();
        }


        private string GetInsertQueryChangedDetails(string Id, string changedMasterID, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
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
            //objQuery.AddToQuery("CSTPercent", CSTPercent);
            //objQuery.AddToQuery("AmountCST", AmountCST);
            //objQuery.AddToQuery("IfMRPInclusiveOfVAT", IfMRPInclusiveOfVAT);
            //objQuery.AddToQuery("IfTradeRateInclusiveOfVAT", IfTradeRateInclusiveOfVAT);
            //objQuery.AddToQuery("AmountSpecialDiscount", amtspldisc);
            //objQuery.AddToQuery("SpecialDiscountPercent", spldiscper);
            objQuery.AddToQuery("AmountCashDiscount", Amountcashdiscperunit);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("DetailPurchaseID", mydetailpurchaseid);
            objQuery.AddToQuery("Margin", productMargin);
            objQuery.AddToQuery("MarginAfterDiscount", productMargin2);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryDeletedDetails(string Id, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
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
            //objQuery.AddToQuery("CSTPercent", CSTPercent);
            //objQuery.AddToQuery("AmountCST", AmountCST);
            //objQuery.AddToQuery("IfMRPInclusiveOfVAT", IfMRPInclusiveOfVAT);
            //objQuery.AddToQuery("IfTradeRateInclusiveOfVAT", IfTradeRateInclusiveOfVAT);
            //objQuery.AddToQuery("AmountSpecialDiscount", amtspldisc);
            //objQuery.AddToQuery("SpecialDiscountPercent", spldiscper);
            objQuery.AddToQuery("AmountCashDiscount", Amountcashdiscperunit);
            objQuery.AddToQuery("StockID", stockid);
            objQuery.AddToQuery("DetailPurchaseID", mydetailpurchaseid);
            objQuery.AddToQuery("Margin", productMargin);
            objQuery.AddToQuery("MarginAfterDiscount", productMargin2);
            objQuery.AddToQuery("SerialNumber", serialNumber);
            return objQuery.InsertQuery();
        }



        private string GetInsertQueryDetailsInStockTable(int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
            string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double PurchaseVATPercent,
            double ProductVATPercent, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount,
            string accountId, string billnumber, string voutype, int vounumber, string voudate, int ProdLoosePack,string stockid,double productMargin,string purScanCode,double pricetoretailer,double profitpercent)
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
            objQuery.AddToQuery("ClosingStock", (Quantity + SchemeQuantity + ReplacementQuantity) * ProdLoosePack);
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
            //objQuery.AddToQuery("BeginningStock", 0);
            objQuery.AddToQuery("IfRateCorrection", "");
            objQuery.AddToQuery("ScanCode", purScanCode);
            objQuery.AddToQuery("TransferInStock", 0);
            objQuery.AddToQuery("CreditNoteStock", 0);
            objQuery.AddToQuery("SaleStock", 0);
            objQuery.AddToQuery("TransferOutStock", 0);
            objQuery.AddToQuery("DebitNoteStock", 0);
            objQuery.AddToQuery("SaleSchemeStock", 0);
            objQuery.AddToQuery("PriceToRetailer", pricetoretailer);
            objQuery.AddToQuery("ProfitPercent", profitpercent);               
         //   objQuery.AddToQuery("StockID",stockid);
            objQuery.AddToQuery("Margin", productMargin);

            return objQuery.InsertQuery();
        }       


        private string GetUpdateQuery(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
          string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
          double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
          double AmountAddOn, double AmountFreight, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
          double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
          double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double purchaseAmountZeroVat, string DueDate, int NumberofChallans, int statementNumber,string voucherSubType,
           double gstAmt0, double gstAmtS5, double gstAmtS12, double gstAmtS18, double gstAmtS28,
            double gstAmtC5, double gstAmtC12, double gstAmtC18, double gstAmtC28, double gsts5, double gsts12, double gsts18, double gsts28,
            double gstc5, double gstc12, double gstc18, double gstc28, double gstAmtI5, double gstAmtI12, double gstAmtI18, double gstAmtI28, double gstI5, double gstI12, double gstI18, double gstI28, string modifiedby, string modifieddate, string modifiedtime)
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
            //objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
            objQuery.AddToQuery("AmountAddOn", AmountAddOn);
            objQuery.AddToQuery("AmountFreight", AmountFreight);
            objQuery.AddToQuery("CashDiscountPercentage", CashDiscountPercentage);
            //objQuery.AddToQuery("SpecialDiscountPercentage", SpecialDiscPer);
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
            objQuery.AddToQuery("GSTI5", gstc5);
            objQuery.AddToQuery("GSTI12", gstc12);
            objQuery.AddToQuery("GSTI18", gstc18);
            objQuery.AddToQuery("GSTI28", gstc28);

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
        public DataTable GetOverviewDataForGSTReport(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "";
            strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,b.GSTnumber,(AmountCreditNote) as TotalLess,(AmountFreight+AmountDebitNote) as TotalAdd,AmountGST0,AmountGSTS5,GSTS5,AmountGSTS12,GSTS12,AmountGSTS18,GSTS18,AmountGSTS28,GSTS28,AmountGSTC5,GSTC5,AmountGSTC12,GSTC12,AmountGSTC18,GSTC18,AmountGSTC28,GSTC28, AmountCreditNote,AmountFreight,AmountDebitNote,RoundUpAmount,AmountCashDiscount from voucherpurchase a inner join masteraccount b on a.AccountID = b.AccountID AND a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForGSTReportHSN(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "";
            strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.VoucherSubType,a.PurchaseBillNumber,a.VoucherDate,c.ProductID,c.ProdName,c.HSNNumber,b.ProductID,b.ProductVATPercent,b.GSTAmountZero,b.GSTSAmount,b.GSTCAmount,b.GSTIAmount,b.GSTS,b.GSTC,b.GSTI from voucherpurchase a inner join detailpurchase b on a.PurchaseID = b.PurchaseID inner join masterproduct c on b.ProductID = c.ProductID where a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,b.AccName,b.AccAddress1,b.AccAddress2,a.AmountClear,0 as AmountVAT from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID order by a.VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForWithoutStockSearch()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.VoucherSubType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,b.AccName,b.AccAddress1,b.AccAddress2,a.AmountClear,(a.AmountVAT5Percent+a.AmountVAT12Point5Percent) as AmountVAT from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND a.VoucherSubType = '2'  order by a.VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPurchaseRegister(string fromdate, string todate, string voutype)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear,(AmountVAT5Percent+AmountVAT12Point5Percent) as AmountVAT from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "' AND a.vouchertype = '" + voutype + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPurchaseRegister(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select distinct purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForVATReport(string fromdate, string todate, string voutype)
        {
            DataTable dtable = new DataTable();
            string strSql = "";
            if (voutype == string.Empty)
            {
                strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountCreditNote) as TotalLess,(AmountAddOn+AmountDebitNote) as TotalAdd,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,AmountItemDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountCreditNote,AmountAddOn,AmountDebitNote,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "'order by VoucherNumber";
            }
            else
            {
                strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountCreditNote) as TotalLess,(AmountAddOn+AmountDebitNote) as TotalAdd,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,AmountItemDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountCreditNote,AmountAddOn,AmountDebitNote,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "' AND a.voucherType = '" + voutype + "' order by VoucherNumber";
            }
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForVATReport(string fromdate, string todate, string voutype, string accountID)
        {
            DataTable dtable = new DataTable();
            string strSql = "";

            strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountCreditNote) as TotalLess,(AmountAddOn+AmountDebitNote) as TotalAdd,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,AmountItemDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountCreditNote,AmountAddOn,AmountDebitNote,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND a.AccountID = '" + accountID + "' AND a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "'order by VoucherNumber";
            //}
            //else
            //{
            //    strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountCreditNote) as TotalLess,(AmountAddOn+AmountDebitNote) as TotalAdd,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,AmountItemDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountCreditNote,AmountAddOn,AmountDebitNote,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "' AND a.voucherType = '" + voutype + "' order by VoucherNumber";
            //}
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForVATReportOtherDetails(string fromdate, string todate, string voutype)
        {
            DataTable dtable = new DataTable();
            string strSql = "";
            if (voutype == string.Empty)
            {
                strSql = "Select purchaseID,VoucherNumber,VoucherType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount) as AmountDiscount,AmountAddOn,AmountFreight,AmountExcise,AmountCreditNote,AmountDebitNote,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent,AmountItemDiscount,AmountSchemeDiscount,AmountCashDiscount from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND AmountCashDiscount+AmountSchemeDiscount+AmountItemDiscount+AmountAddOn+AmountCreditNote+AmountDebitNote > 0 AND a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "'order by VoucherNumber";
            }
            else
            {
                strSql = "Select purchaseID,VoucherNumber,VoucherType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount) as AmountDiscount,AmountAddOn,AmountFreight,AmountExcise,AmountCreditNote,AmountDebitNote,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent,AmountItemDiscount,AmountSchemeDiscount,AmountCashDiscount from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND AmountCashDiscount+AmountSchemeDiscount+AmountItemDiscount+AmountAddOn+AmountCreditNote+AmountDebitNote > 0 AND a.voucherdate >= '" + fromdate + "' AND a.voucherdate <= '" + todate + "' AND a.voucherType = '" + voutype + "' order by VoucherNumber";
            }
            //  string strSql = "Select purchaseID,VoucherNumber,VoucherType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount) as AmountDiscount,AmountAddOn,AmountCreditNote,AmountDebitNote,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent,AmountItemDiscount,AmountSchemeDiscount,AmountCashDiscount from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND AmountCashDiscount+AmountSchemeDiscount+AmountItemDiscount+AmountAddOn+AmountCreditNote+AmountDebitNote > 0 order by VoucherType,VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForVATReportDATE(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select voucherType,VoucherDate,sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOn+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase where VoucherDate >= '" + mfromdate + "' AND VoucherDate <= '" + mtodate + "'  group by vouchertype,VoucherDate order by VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATReportDATEALL()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select voucherType,VoucherDate,sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOn+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase   group by VoucherDate order by VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForVATReport()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountCreditNote) as TotalLess,(AmountAddOn+AmountDebitNote) as TotalAdd,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,AmountItemDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountCreditNote,AmountAddOn,AmountDebitNote,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATReportOtherDetails()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select purchaseID,VoucherNumber,VoucherType,PurchaseBillNumber,VoucherDate,AmountNet,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount) as AmountDiscount,AmountAddOn,AmountCreditNote,AmountDebitNote,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent,AmountItemDiscount,AmountSchemeDiscount,AmountCashDiscount from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND AmountCashDiscount+AmountSchemeDiscount+AmountItemDiscount+AmountAddOn+AmountCreditNote+AmountDebitNote > 0 order by VoucherType,VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForVATReportDATE()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select voucherType,VoucherDate,sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOn+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase   group by vouchertype,VoucherDate order by VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
      

        public DataTable GetOverviewDataForVATReportMONTH(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select voucherType,VoucherDate,sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOn+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase where VoucherDate >= '" + mfromdate + "' AND VoucherDate <= '" + mtodate + "'  group by vouchertype, substring(VoucherDate,5,2) order by VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATReportMONTHALL(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select voucherType,VoucherDate, sum(AmountNet) as AmountNet,sum(AmountItemDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountDebitNote) as TotalLess,sum(AmountAddOn+AmountCreditNote) as TotalAdd,sum(AmountCreditNote) as AmountCreditNote,sum(AmountDebitNote) as AmountDebitNote,sum(AmountPurchase5PercentVAT) as AmountPurchase5PercentVAT,sum(AmountPurchase12point5PercentVAT) as AmountPurchase12point5PercentVAT,sum(AmountPurchaseZeroVAT) as AmountPurchaseZeroVAT,sum(RoundUpAmount) as RoundUpAmount,sum(AmountVAT5Percent) as AmountVAT5Percent, sum(AmountVAT12Point5Percent) as AmountVAT12Point5Percent from voucherpurchase where VoucherDate >= '" + mfromdate + "' AND VoucherDate <= '" + mtodate + "'  group by substr(VoucherDate,5,2) order by VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATReportTIN(string mfromdate, string mtodate )
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.VoucherDate,sum(a.AmountPurchase5PercentVAT + a.AmountPurchase12point5PercentVAT) as TotalAmount,sum(a.AmountVAT5Percent + a.AmountVAT12Point5Percent) as TotalVAT,b.AccountID,b.AccName,b.AccAddress1,b.AccVATTIN  from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID AND a.VoucherDate >= '"+ mfromdate  +"' AND a.VoucherDate <= '"+ mtodate  +"'  group by b.AccountID  order by TotalVAT desc";
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
        public DataTable GetPurchaseDataProductWise(int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate,a.TradeRate, (a.TradeRate * a.Quantity) as TAmount, (a.PurchaseRate * a.Quantity) as PAmount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where  a.ProductID = '" + ProductID + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetPurchaseDataProductWiseWithScheme(int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where a.SchemeQuantity > 0 AND  a.ProductID = '" +ProductID +"' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForProductBatchList(int ProductID, string mbatchno, double mrp)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.MRP,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where a.ProductID = '" + ProductID + "' AND a.BatchNumber = '" + mbatchno + "' AND a.MRP = "+ mrp +"  order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPartyProductList(string partyid, int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where c.AccountID = '" + partyid + "' AND a.ProductID = '" + ProductID + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPartywiseBills(string partyid, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,a.AccountID,c.AccountID,c.AccName " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where a.AccountID = '" + partyid + "' AND a.voucherdate >= '" + fromDate + "' AND  a.voucherdate <= '" + toDate + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPartywiseBillsForStatements(string partyid, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate ,a.AmountNet , a.AmountVAT5Percent,a.AmountVAT12point5Percent " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where a.AccountID = '" + partyid + "' AND a.voucherdate >= '" + fromDate + "' AND  a.voucherdate <= '" + toDate + "' AND a.StatementNumber = 0  AND AmountClear = 0 order by VoucherType, VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPartywiseStatementsView(int statementNumber, string voucherSeries)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate ,a.AmountNet , a.AmountVAT5Percent,a.AmountVAT12point5Percent " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where a.statementnumber = "+ statementNumber +" AND voucherseries = '"+ voucherSeries + "' order by VoucherType, VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
      
        public DataTable GetOverviewDataForDiscount()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,a.AmountItemDiscount,a.AmountSchemeDiscount,a.AmountCashDiscount,a.AccountID,c.AccountID,c.AccName " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where a.AmountItemDiscount > 0 or a.amountSchemeDiscount > 0 or a.AmountCashDiscount > 0  order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForAllPartySummary(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.voucherdate, sum(a.AmountNet) as AmountNet,a.AccountID,c.AccountID,c.AccName " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID  where a.voucherdate >= '" + fromdate + "' AND  a.voucherdate <= '" + todate + "'  group by a.AccountID  order by c.AccName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataCategory(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select sum(a.Quantity *a.TradeRate) as AmountNet,a.ProductID, sum(a.AmountItemDiscount) as AmountItemDiscount, " +
                 "sum(a.AmountSchemeDiscount) as AmountSchemeDiscount, " +
                 "sum(a.AmountCashDiscount) as AmountCashDiscount, b.ProductID,b.ProdCategoryID,c.ProductCategoryID,c.ProductCategoryName,d.voucherdate from detailpurchase a inner join masterproduct b on a.ProductID = b.ProductID inner join masterproductcategory c on b.ProdCategoryID = c.ProductCategoryID inner join voucherpurchase d on a.PurchaseID = d.PurchaseID  where d.voucherdate >= '" + mfromdate + "' AND d.voucherdate <= '"+ mtodate +"' group by c.ProductCategoryID order by c.ProductCategoryName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataCompany(string companyid, string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.ProductID,a.BatchNumber,a.Expiry,a.MRP,a.Quantity,(a.SchemeQuantity + a.ReplacementQuantity) as SchemeQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount,b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName, " +
                 " d.ProductID,d.ProdName,d.ProdLoosePack,d.ProdPack " +
                 "from detailpurchase a  inner join voucherpurchase b on a.purchaseID = b.purchaseID inner join masteraccount c on b.AccountID = c.AccountID inner join masterproduct d on a.ProductID = d.ProductID where d.ProdCompID = '" + companyid + "' AND b.Voucherdate >= '"+ mfromdate +"' AND b.Voucherdate <='"+ mtodate  + "' order by d.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataNewProducts(string mfromdate, string mtodate )
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.ProductID,a.BatchNumber,a.Expiry,a.MRP,a.Quantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount,b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName, " +
                 " d.ProductID,d.ProdName,d.ProdLoosePack,d.ProdPack " +
                 "from detailpurchase a  inner join voucherpurchase b on a.purchaseID = b.purchaseID inner join masteraccount c on b.AccountID = c.AccountID inner join masterproduct d on a.ProductID = d.ProductID where  d.CreatedDate >= '"+ General.ShopDetail.Shopsy + "' AND  b.Voucherdate >= '"+ mfromdate +"' AND b.Voucherdate <='"+ mtodate  + "'  order by d.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForLastPurchase(int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate,a.MRP,a.Margin ,a.MarginAfterDiscount," +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where a.ProductID = '"+ ProductID+"' order by b.VoucherDate";
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

        public DataTable GetOverviewDataForPartywiseOutstandingPurchaseReportforParty(string accID, string fromdate, string todate)
        {
            DataTable dt = null;
            try
            {
                {


                    string strSql = "select  b.PurchaseID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,a.AccName,a.AccAddress1,b.AmountNet as Amount,b.AmountBalance, a.AccAddress2 as AccAddress2  from  voucherpurchase b inner join masteraccount a on b.AccountID = a.AccountID  " +
                                    "where b.AccountID = '" + accID + "' AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' AND b.AmountBalance > 0 order by b.VoucherDate,b.VoucherNumber";


                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }
        public DataTable GetOverviewDataForPartywiseOutstandingPurchaseReportAll(string fromdate, string todate)
        {
            DataTable dt = null;
            try
            {
                {


                    string strSql = "select  b.PurchaseID  , b.VoucherType, " +
                                    "b.VoucherNumber,b.VoucherSubType,b.VoucherDate,b.AccountID,a.AccName,a.AccAddress1,b.AmountNet as Amount, b.AmountBalance, a.AccAddress2,a.accopeningdebit,a.accopeningCredit,a.accclearedamount  from  voucherpurchase b " +
                                    "inner join masteraccount a on b.AccountID = a.AccountID where b.VoucherType = 'PCR' AND  b.AmountBalance > 0   AND  b.voucherdate >= '" + fromdate + "'  AND b.VoucherDate <= '" + todate + "' order by a.AccName, a.AccAddress1,b.VoucherDate";


                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }


        public void SavePurchaseBillFormat(string purchaseBillFormat, string accountID)
        {
            bool retValue = false;
            string strSql = "Update masteraccount set PurchaseBillFormat = '" + purchaseBillFormat + "' where AccountID = '" + accountID + "'";
            retValue = (DBInterface.ExecuteQuery(strSql) > 0);
        }

        public DataRow ReadDetailsByVouNumber(int vouno, string voutype)
        {
            DataRow dRow = null;
            
           
                string strSql = "Select * from voucherpurchase where VoucherNumber = " + vouno + " AND VoucherType = '" + voutype + "'";
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
                "b.PurchaseVATPercent,b.SchemeQuantity,b.ReplacementQuantity,b.AmountItemDiscount,b.ItemDiscountPercent,b.ProfitPercent," +
                "b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.AmountPurchaseVAT,b.AmountCashDiscount,b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount, " +
                "d.ClosingStock,d.ScanCode,e.ShelfCode from masterproduct a inner join  detailpurchase b  on a.ProductID = b.ProductID left outer join tblstock d on b.StockID = d.StockID  left outer join mastershelf e on  a.ProdShelfID = e.shelfID where b.PurchaseID = '{0}' order by b.SerialNumber";
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
                "b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.AmountPurchaseVAT,b.AmountCreditNote,b.AmountCashDiscount,b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount, b.scancode," +
                "d.ClosingStock,e.ShelfCode from masterproduct a inner join  changeddetailpurchase b  on a.ProductID = b.ProductID left outer join tblstock d on b.StockID = d.StockID  left outer join mastershelf e on  a.ProdShelfID = e.shelfID where b.ChangedMasterID = '{0}' order by b.SerialNumber";
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
                "b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.AmountPurchaseVAT,b.AmountCreditNote,b.AmountCashDiscount,b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount, " +
                "d.ClosingStock,e.ShelfCode from masterproduct a inner join  deleteddetailpurchase b  on a.ProductID = b.ProductID left outer join tblstock d on b.StockID = d.StockID  left outer join mastershelf e on  a.ProdShelfID = e.shelfID where b.PurchaseID = '{0}' order by b.SerialNumber";
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
            strSql = "select * from voucherpurchase where accountID = '" + AccID + "' AND PurchaseBillNumber = '" + PurBillNumber + "' AND PurchaseID != '" + Id + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retVal = false;
            }

            return retVal;
        }
        public bool CheckforUniqueBillNumberforNew(string PurBillNumber, string AccID)
        {
            bool retVal = true;
            string strSql = "";
            DataTable dt = new DataTable();
            strSql = "select * from voucherpurchase where accountID = '" + AccID + "' AND PurchaseBillNumber = '" + PurBillNumber + "'";
            if (DBInterface.ExecuteQuery(strSql) > 0)
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
            strSql = "select * from voucherpurchase where voucherDate >= '"+ VoucherDate + "' AND voucherDate <= '" + VoucherDate + "' AND statementnumber > 0";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retVal = false;
            }

            return retVal;
        }


        public DataTable GetOverviewDataForDiscount(string mfromDate, string mtoDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,a.AmountItemDiscount,a.AmountSchemeDiscount,a.AmountCashDiscount,a.AccountID,c.AccountID,c.AccName " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where (a.AmountItemDiscount > 0 OR a.amountSchemeDiscount > 0 OR a.AmountCashDiscount > 0 ) AND (a.VoucherDate >= '" + mfromDate + "' AND a.VoucherDate <= '" + mtoDate + "' ) order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }



        public DataTable GetAllBatchNumbersForScanCode(int ProductID)
        {
            DataTable dt = null;
            try
            {
                {

                    string strSql = "select  scancode from  tblstock where ProductID = '{0}' order by scancode";
                    strSql = string.Format(strSql, ProductID);
                    dt = DBInterface.SelectDataTable(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dt;
        }

        public DataRow GetProductScancode(int ProductID)
        {
            DataRow dr = null;
            try
            {
                {

                    string strSql = "select  ScannedBarcode from masterproduct where ProductID = '{0}'";
                    strSql = string.Format(strSql, ProductID);
                    dr = DBInterface.SelectFirstRow(strSql);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("DBSaleList.GetOverviewDataForDailySaleReport>>" + Ex.Message);

            }

            return dr;
        }


        public DataRow GetFirstRecord(string voutype, string vouSubType, string VouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select * from voucherpurchase where VoucherType = '" + voutype + "'  AND VoucherSubType = '" + vouSubType + "' AND  VoucherSeries = '" + VouSeries + "'  order by Vouchernumber ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetLastRecordForPurchase(string vouType, string vouSubType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select * from voucherpurchase where VoucherType = '" + vouType + "'  AND VoucherSubType = '" + vouSubType + "' AND VoucherSeries = '" + vouSeries + "'  order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }
        public DataRow GetDuplicateBarcode(string Barcode) 
        {
            DataRow dr;
            string strsql = "select PurchaseID,StockID,ProductID,scancode from detailpurchase where scancode = '" + Barcode + "'";
            dr = DBInterface.SelectFirstRow(strsql);
            return dr;
        }
      


        public DataRow GetLastVoucherNumber(string vouType, string vouSubType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select Vouchernumber from voucherpurchase where VoucherType =  '" + vouType + "'  AND  VoucherSubType = '" + vouSubType + "' AND  VoucherSeries = '" + vouSeries + "' order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }


        public DataRow ReadDetailsByVouNumber(int vouno, string voutype, string vouSeries, string vousubtype)
        {
            DataRow dRow = null;


            string strSql = "Select * from voucherpurchase where VoucherNumber = " + vouno + " AND VoucherType = '" + voutype + "'  AND Voucherseries = '" + vouSeries + "' AND VoucherSubType = '" + vousubtype + "'";
            dRow = DBInterface.SelectFirstRow(strSql);

            return dRow;
        }


        public bool CheckforProductforDistributorID(string distributorID, string distributorProductID)
        {
            bool retVal = true;
            string strSql = "";
            DataRow dr = null;
            strSql = "select * from tblbillimportlink where DistributorID = '" + distributorID + "' AND DistributorProductID = '" + distributorProductID + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr == null)
            {
                retVal = false;
            }

            return retVal;
        }

        public bool AddLinkIntblbillimportlink(string guid, string DistributorID, string distributorProductID, string retailerProductID)
        {
            bool retValue = false;
            string strSql = GetInsertQueryForAddLinkIntblbillimportlink(guid, DistributorID, distributorProductID, retailerProductID);
            if (DBInterface.ExecuteQuery(strSql) > 0)
                retValue = true;
            return retValue;
        }


        private string GetInsertQueryForAddLinkIntblbillimportlink(string guid, string DistributorID, string distributorProductID, string retailerProductID)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblbillimportlink";
            objQuery.AddToQuery("ID", guid);
            objQuery.AddToQuery("DistributorID", DistributorID);
            objQuery.AddToQuery("distributorProductID", distributorProductID);
            objQuery.AddToQuery("retailerProductID", retailerProductID);

            return objQuery.InsertQuery();
        }


        public DataTable ReadPaymentDetailsStatementsByID(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select a.MasterID, a.MasterPurchaseID,a.ClearAmount,b.VoucherType, " +
                "b.VoucherNumber,b.VoucherDate,b.CBID, " +
                "c.ID from detailcashbankpayment a inner join  vouchercashbankpayment b  on a.MasterID = b.CBID  inner join voucherstatement c on a.MasterPurchaseID = c.ID where a.MasterPurchaseID = '{0}'";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }

       
    }
}
