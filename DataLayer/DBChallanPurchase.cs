using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;
using EcoMart.DataLayer;

namespace EcoMart.DataLayer
{
    public class DBChallanPurchase
    {
        public DBChallanPurchase()
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
            string strSql = "Select sum(AmountNet) as AmountNet from vouchercreditdebitnote where AmountClear = 0 && (VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteAmount + "' || VoucherType = '" + FixAccounts.VoucherTypeForDebitNoteStock + "')  && AccountID = '" + accountid + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            return dr;
        }
        public bool AddDetails(string ID, string accountID, string Narration, string EntryDate, string ChallanNumber, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
            string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
            double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
            double AmountAddOnFreight, double AmountLess, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
            double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
            double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string voucherSubType, string createdby, string createddate, string createdtime)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(ID, accountID, Narration, EntryDate, ChallanNumber, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
                SpecialDiscPer, AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
                AmountAddOnFreight, AmountLess, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
                OctroiPercentage, AmountOctroi, PurchaseAmount5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
                PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, voucherSubType, createdby, createddate, createdtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }

        //public bool AddChangedDetails(string purchaseID, string changedID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
        //   string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
        //   double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
        //   double AmountAddOnFreight, double AmountLess, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
        //   double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
        //   double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string modifiedby, string modifieddate, string modifiedtime)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetInsertQueryChanged(purchaseID, changedID, accountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
        //        PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
        //        SpecialDiscPer, AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
        //        AmountAddOnFreight, AmountLess, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
        //        OctroiPercentage, AmountOctroi, PurchaseAmount5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
        //        PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, modifiedby, modifieddate, modifiedtime);
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }

        //    return bRetValue;
        //}

        //public bool AddDeletedDetails(string purchaseID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
        //   string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
        //   double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
        //   double AmountAddOnFreight, double AmountLess, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
        //   double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
        //   double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string createdby, string createddate, string createdtime)
        //{
        //    bool bRetValue = false;
        //    string strSql = GetInsertQueryDeleted(purchaseID, accountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
        //        PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
        //        SpecialDiscPer, AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
        //        AmountAddOnFreight, AmountLess, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
        //        OctroiPercentage, AmountOctroi, PurchaseAmount5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
        //        PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, createdby, createddate, createdtime);
        //    if (DBInterface.ExecuteQuery(strSql) > 0)
        //    {
        //        bRetValue = true;
        //    }

        //    return bRetValue;
        //}

        public bool AddDetailsProductsSS(string Id, string ChallanNumber, string VoucherDate, int VoucherNumber, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
               double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
               string IfTradeRateInclusiveOfVAT, double Amount, double spldiscamt, double spldiscper, double AmountZeroVAT, double AmountCashDiscountperunit, string stockid, string mydetailpurchaseid, double productMargin, double productMargin2, int serialNumber, string scancode, double distributorRatePer, double distributorSaleRate)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetails(Id, ChallanNumber, VoucherDate, VoucherNumber, ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercent,
                    AmountSchemeDiscount, PurchaseVATPercent, ProductVATPercent, AmountPurchaseVAT, AmountProductVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT,
                    IfTradeRateInclusiveOfVAT, Amount, spldiscamt, spldiscper, AmountZeroVAT, AmountCashDiscountperunit, stockid, mydetailpurchaseid, productMargin, productMargin2, serialNumber, scancode, distributorRatePer, distributorSaleRate);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }


        public bool AddChangedDetailsProductsSS(string Id, string changedMasterID, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
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



        public bool AddProductDetailsInStockTable(int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
            string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double PurchaseVATPercent,
            double ProductVATPercent, string IfMRPInclusiveOfVAT, string IfTradeRateInclusiveOfVAT, double Amount,
            string accountId, string billnumber, string voutype, int vounumber, string voudate, int ProdLoosePack, string StockId, double productMargin, string purScanCode, double distSaleRate, double distSaleRatePer)
        {
            bool bRetValue = false;
            string strSql = GetInsertQueryDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                    Expiry, ExpiryDate, Quantity, SchemeQuantity, ReplacementQuantity, PurchaseVATPercent,
                    ProductVATPercent, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount,
                    accountId, billnumber, voutype, vounumber, voudate, ProdLoosePack, StockId, productMargin, purScanCode, distSaleRate, distSaleRatePer);
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
            string strSql = GetInsertQueryForBankPayment(CBId, CBVouType, CBVouNo, VoucherDate, AccountID, Narration, Amount, chequenumber, chequedate, bankid, createdby, createddate, createdtime);
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
        public bool UpdateDetails(string ID, string accountID, string Narration, string EntryDate, string ChallanNumber, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
            string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
            double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
            double AmountAddOnFreight, double AmountLess, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
            double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
            double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string voucherSubType, string modifiedby, string modifieddate, string modifiedtime)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(ID, accountID, Narration, EntryDate, ChallanNumber, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
                PurchaseBillNumber, AmountNet, AmountClear, AmountGross, AmountItemDiscount, AmountSpecialDiscount,
                SpecialDiscPer, AmountCashDiscount, CRNoteDiscPer, AmountSchemeDiscount,
                AmountAddOnFreight, AmountLess, CashDiscountPercentage, AmountCreditNote, AmountDebitNote, RoundUpAmount,
                OctroiPercentage, AmountOctroi, PurchaseAmount5PercentVAT, AmtOtherPercentVAT, AmountVAT5Percent,
                PurchaseAmount12Point5PercentVAT, AmountVAT12Point5Percent, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, voucherSubType, modifiedby, modifieddate, modifiedtime);
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }

            return bRetValue;
        }
        public bool UpdatePurchaseDetails(string Id, string Purchaseid, string purchaseBillNumber, string ModifiedBy, string ModifiedDate, string ModifiedTime)
        {
            bool bRetValue = false;
            string strSql = GetUpdatePurchaseDetailsQuery(Id, Purchaseid, purchaseBillNumber, ModifiedBy, ModifiedDate, ModifiedTime);
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
            string strSql = string.Format("Select *  from detailpurchaseorderstockist where ProductID = '{0}' &&  OrderNumber =  0", ProductID);
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
            string strSql = "Delete from detailpurchaseorderstockist where ProductID = '" + ProductID + "' && OrderNumber = 0";
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
            string strSql = "Delete from voucherchallanpurchase where ID = '" + Id + "'";
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
            string strSql = "Delete from detailpurchase where ChallanID = '" + Id + "'";
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

        private string GetInsertQuery(string ID, string accountID, string Narration, string EntryDate, string ChallanNumber, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
            string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
            double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
            double AmountAddOnFreight, double AmountLess, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
            double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
            double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string voucherSubType, string createdby, string createddate, string createdtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherChallanpurchase";
            objQuery.AddToQuery("ID", ID);
            objQuery.AddToQuery("EntryDate", EntryDate);
            objQuery.AddToQuery("ChallanNumber", ChallanNumber);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherSubType", voucherSubType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherDate", VoucherDate);
            objQuery.AddToQuery("PurchaseBillNumber", PurchaseBillNumber);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("AmountNet", AmountNet);
            //  objQuery.AddToQuery("AmountBalance", AmountNet - AmountClear);
            //  objQuery.AddToQuery("AmountClear", AmountClear);
            objQuery.AddToQuery("AmountGross", AmountGross);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
            objQuery.AddToQuery("AmountAddOnFreight", AmountAddOnFreight);
            objQuery.AddToQuery("AmountLess", AmountLess);
            objQuery.AddToQuery("CashDiscountPercentage", CashDiscountPercentage);
            objQuery.AddToQuery("SpecialDiscountPercentage", SpecialDiscPer);
            //   objQuery.AddToQuery("AmountCreditNote", AmountCreditNote);
            //  objQuery.AddToQuery("AmountDebitNote", AmountDebitNote);

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
            //   objQuery.AddToQuery("StatementNumber", StatementNumber);
            objQuery.AddToQuery("NumberofChallans", NumberofChallans);

            objQuery.AddToQuery("CreatedUserID", createdby);
            objQuery.AddToQuery("CreatedDate", createddate);
            objQuery.AddToQuery("CreatedTime", createdtime);
            return objQuery.InsertQuery();
        }

        private string GetInsertQueryChanged(string purchaseID, string changedID, string accountID, string Narration, string EntryDate, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
           string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
           double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
           double AmountAddOnFreight, double AmountLess, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
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
            objQuery.AddToQuery("AmountLess", AmountLess);
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
                   double AmountAddOnFreight, double AmountLess, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
                   double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
                   double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double PurchaseAmountZeroVATS, string DueDate, int NumberofChallans, int StatementNumber, string modifiedby, string modifieddate, string modifiedtime)
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
            objQuery.AddToQuery("AmountLess", AmountLess);
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

        private string GetInsertQueryDetails(string Id, string ChallanNumber, string VoucherDate, int ChallanVoucherNumber, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
               string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
               double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
               string IfTradeRateInclusiveOfVAT, double Amount, double amtspldisc, double spldiscper, double AmountZeroVAT, double Amountcashdiscperunit, string stockid, string mydetailpurchaseid, double productMargin, double productMargin2, int serialNumber, string scancode, double distributorRatePer, double distributorSaleRate)
        {
            Query objQuery = new Query();
            objQuery.Table = "detailpurchase";
            objQuery.AddToQuery("ChallanID", Id);
            objQuery.AddToQuery("ChallanNumber", ChallanNumber);
            objQuery.AddToQuery("ChallanDate", VoucherDate);
            objQuery.AddToQuery("ChallanVoucherNumber", ChallanVoucherNumber);
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
            objQuery.AddToQuery("DistributorSaleRate", distributorSaleRate);
            return objQuery.InsertQuery();
        }


        private string GetInsertQueryChangedDetails(string Id, string changedMasterID, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
              string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
              double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
              string IfTradeRateInclusiveOfVAT, double Amount, double amtspldisc, double spldiscper, double AmountZeroVAT, double Amountcashdiscperunit, string stockid, string mydetailpurchaseid, double productMargin, double productMargin2, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "changeddetailpurchase";
            objQuery.AddToQuery("ChallanID", Id);
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

        private string GetInsertQueryDeletedDetails(string Id, int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
              string Expiry, string ExpiryDate, int Quantity, int SchemeQuantity, int ReplacementQuantity, double ItemDiscountPercent, double AmountItemDiscount, double SchemeDiscountPercent,
              double AmountSchemeDiscount, double PurchaseVATPercent, double ProductVATPercent, double AmountPurchaseVAT, double AmountProductVAT, double CSTPercent, double AmountCST, string IfMRPInclusiveOfVAT,
              string IfTradeRateInclusiveOfVAT, double Amount, double amtspldisc, double spldiscper, double AmountZeroVAT, double Amountcashdiscperunit, string stockid, string mydetailpurchaseid, double productMargin, double productMargin2, int serialNumber)
        {
            Query objQuery = new Query();
            objQuery.Table = "deleteddetailpurchase";
            objQuery.AddToQuery("ChallanID", Id);
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



        private string GetInsertQueryDetailsInStockTable(int ProductID, string Batchno, double TradeRate, double PurchaseRate, double MRP, double SaleRate,
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
            objQuery.AddToQuery("ClosingStock", (Quantity + SchemeQuantity) * ProdLoosePack);
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


        private string GetUpdateQuery(string ID, string accountID, string Narration, string EntryDate, string ChallanNumber, string VoucherSeries, string VoucherType, int VoucherNumber, string VoucherDate,
          string PurchaseBillNumber, double AmountNet, double AmountClear, double AmountGross, double AmountItemDiscount, double AmountSpecialDiscount,
          double SpecialDiscPer, double AmountCashDiscount, double CRNoteDiscPer, double AmountSchemeDiscount,
          double AmountAddOnFreight, double AmountLess, double CashDiscountPercentage, double AmountCreditNote, double AmountDebitNote, double RoundUpAmount,
          double OctroiPercentage, double AmountOctroi, double PurchaseAmount5PercentVAT, double AmtOtherPercentVAT, double AmountVAT5Percent,
          double PurchaseAmount12Point5PercentVAT, double AmountVAT12Point5Percent, double purchaseAmountZeroVat, string DueDate, int NumberofChallans, int statementNumber, string voucherSubType, string modifiedby, string modifieddate, string modifiedtime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherChallanpurchase";
            objQuery.AddToQuery("ID", ID, true);
            //    objQuery.AddToQuery("EntryDate", EntryDate);          
            objQuery.AddToQuery("ChallanNumber", ChallanNumber);
            objQuery.AddToQuery("VoucherSeries", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("VoucherType", VoucherType);
            objQuery.AddToQuery("VoucherSubType", voucherSubType);
            objQuery.AddToQuery("VoucherNumber", VoucherNumber);
            objQuery.AddToQuery("VoucherDate", VoucherDate);
            objQuery.AddToQuery("PurchaseBillNumber", PurchaseBillNumber);
            objQuery.AddToQuery("AccountID", accountID);
            objQuery.AddToQuery("AmountNet", AmountNet);
            //  objQuery.AddToQuery("AmountBalance", AmountNet - AmountClear);
            //  objQuery.AddToQuery("AmountClear", AmountClear);
            objQuery.AddToQuery("AmountGross", AmountGross);
            objQuery.AddToQuery("AmountItemDiscount", AmountItemDiscount);
            objQuery.AddToQuery("AmountSpecialDiscount", AmountSpecialDiscount);
            objQuery.AddToQuery("AmountSchemeDiscount", AmountSchemeDiscount);
            objQuery.AddToQuery("AmountCashDiscount", AmountCashDiscount);
            objQuery.AddToQuery("AmountAddOnFreight", AmountAddOnFreight);
            objQuery.AddToQuery("AmountLess", AmountLess);
            objQuery.AddToQuery("CashDiscountPercentage", CashDiscountPercentage);
            objQuery.AddToQuery("SpecialDiscountPercentage", SpecialDiscPer);
            //   objQuery.AddToQuery("AmountCreditNote", AmountCreditNote);
            //  objQuery.AddToQuery("AmountDebitNote", AmountDebitNote);

            objQuery.AddToQuery("RoundUpAmount", RoundUpAmount);
            objQuery.AddToQuery("OctroiPercentage", OctroiPercentage);
            objQuery.AddToQuery("AmountOctroi", AmountOctroi);
            objQuery.AddToQuery("DueDate", DueDate);
            objQuery.AddToQuery("Narration", Narration);
            objQuery.AddToQuery("AmountVAT5Percent", AmountVAT5Percent);
            objQuery.AddToQuery("AmountVAT12point5Percent", AmountVAT12Point5Percent);
            objQuery.AddToQuery("AmountVATOPercent", AmtOtherPercentVAT);
            objQuery.AddToQuery("AmountPurchase5PercentVAT", PurchaseAmount5PercentVAT);
            objQuery.AddToQuery("AmountPurchase12point5PercentVAT", PurchaseAmount12Point5PercentVAT);
            //  objQuery.AddToQuery("AmountPurchaseOPercentVAT", PurchaseAmount0PercentVAT);
            //   objQuery.AddToQuery("StatementNumber", StatementNumber);
            objQuery.AddToQuery("NumberofChallans", NumberofChallans);

            objQuery.AddToQuery("ModifiedUserID", modifiedby);
            objQuery.AddToQuery("ModifiedDate", modifieddate);
            objQuery.AddToQuery("ModifiedTime", modifiedtime);
            return objQuery.UpdateQuery();
        }
        private string GetUpdatePurchaseDetailsQuery(string ID,string PurchaseID, string purchaseBillNumber, string ModifiedBy, string ModifiedDate, string ModifiedTime)
        {
            Query objQuery = new Query();
            objQuery.Table = "voucherChallanpurchase";
            objQuery.AddToQuery("ID",ID, true);       
            objQuery.AddToQuery("PurchaseID", PurchaseID);
            objQuery.AddToQuery("PurchaseBillNumber", purchaseBillNumber);
            objQuery.AddToQuery("ModifiedUserID", ModifiedBy);
            objQuery.AddToQuery("ModifiedDate", ModifiedDate);
            objQuery.AddToQuery("ModifiedTime", ModifiedTime);

            return objQuery.UpdateQuery();
        }
        private string GetUpdateQueryForTypeChange(string purchaseID, string VoucherType, int VoucherNumber, string modifiedby, string modifieddate, string modifiedtime)
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
            objQuery.AddToQuery("ChallanID", Id, true);
            return objQuery.DeleteQuery();
        }

        #endregion

        public DataTable GetOverviewData()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherType,a.VoucherSubType,a.challanNumber,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,b.AccName,b.AccAddress1,b.AccAddress2"/*,a.AmountClear*/+ ",(a.AmountVAT5Percent+a.AmountVAT12Point5Percent) as AmountVAT from voucherchallanpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.VoucherSubType = '1' && a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' && a.VoucherDate <= '" + General.ShopDetail.Shopey + "'  order by a.voucherdate desc , a.vouchernumber desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForWithoutStockSearch()
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.VoucherSubType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,b.AccName,b.AccAddress1,b.AccAddress2,a.AmountClear,(a.AmountVAT5Percent+a.AmountVAT12Point5Percent) as AmountVAT from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.VoucherSubType = '2'  && a.VoucherDate >= '" + General.ShopDetail.Shopsy + "' && a.VoucherDate <= '" + General.ShopDetail.Shopey + "' order by voucherdate desc , vouchernumber desc";
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
                strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountCreditNote) as TotalLess,(AmountAddOnFreight+AmountDebitNote) as TotalAdd,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,AmountItemDiscount,AmountSpecialDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountCreditNote,AmountAddOnFreight,AmountDebitNote,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.voucherdate >= '" + fromdate + "' && a.voucherdate <= '" + todate + "' && a.voucherType = '" + voutype + "' order by VoucherNumber";
            }
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForVATReport(string fromdate, string todate, string voutype, string accountID)
        {
            DataTable dtable = new DataTable();
            string strSql = "";

            strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountCreditNote) as TotalLess,(AmountAddOnFreight+AmountDebitNote) as TotalAdd,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,AmountItemDiscount,AmountSpecialDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountCreditNote,AmountAddOnFreight,AmountDebitNote,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.AccountID = '" + accountID + "' && a.voucherdate >= '" + fromdate + "' && a.voucherdate <= '" + todate + "'order by VoucherNumber";
            //}
            //else
            //{
            //    strSql = "Select purchaseID,VoucherNumber,VoucherType,VoucherSubType,PurchaseBillNumber,VoucherDate,AmountNet,b.AccountID,AccName,AccAddress1,AccAddress2,AmountClear,(AmountItemDiscount+AmountSpecialDiscount+AmountSchemeDiscount+AmountCashDiscount+AmountCreditNote) as TotalLess,(AmountAddOnFreight+AmountDebitNote) as TotalAdd,AmountPurchase5PercentVAT,AmountPurchase12point5PercentVAT,AmountPurchaseZeroVAT,AmountItemDiscount,AmountSpecialDiscount,AmountSchemeDiscount,AmountCashDiscount,AmountCreditNote,AmountAddOnFreight,AmountDebitNote,RoundUpAmount, AmountVAT5Percent,AmountVAT12Point5Percent from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.voucherdate >= '" + fromdate + "' && a.voucherdate <= '" + todate + "' && a.voucherType = '" + voutype + "' order by VoucherNumber";
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

        public DataTable GetOverviewDataForVATReportTIN(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.VoucherDate,sum(a.AmountPurchase5PercentVAT + a.AmountPurchase12point5PercentVAT) as TotalAmount,sum(a.AmountVAT5Percent + a.AmountVAT12Point5Percent) as TotalVAT,sum(a.AmountVAT5Percent) as TotalVAT5,sum(a.AmountVAT12Point5Percent) as TotalVAT12,b.AccountID,b.AccName,b.AccAddress1,b.AccVATTIN  from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.VoucherDate >= '" + mfromdate + "' && a.VoucherDate <= '" + mtodate + "'  group by b.AccountID  order by TotalVAT desc";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForVATReportTINDetail(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.VoucherType,a.VoucherNumber,a.VoucherDate,PurchaseBillNumber,(a.AmountPurchase5PercentVAT+a.AmountPurchase12point5PercentVAT) as TotalAmount,(a.AmountVAT5Percent + a.AmountVAT12Point5Percent) as TotalVAT,(a.AmountVAT5Percent) as TotalVAT5,(a.AmountVAT12Point5Percent) as TotalVAT12,a.AmountPurchaseZeroVAT,b.AccountID,b.AccName,b.AccAddress1,b.AccVATTIN  from voucherpurchase a, masteraccount b Where a.AccountID = b.AccountID && a.VoucherDate >= '" + mfromdate + "' && a.VoucherDate <= '" + mtodate + "' order by VoucherNumber";
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
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where a.SchemeQuantity > 0 &&  a.ProductID = '" + ProductID + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForProductBatchList(int ProductID, string mbatchno, double mrp)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.MRP,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where a.ProductID = '" + ProductID + "' && a.BatchNumber = '" + mbatchno + "' && a.MRP = " + mrp + "  order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPartyProductList(string partyid, int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount, " +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where c.AccountID = '" + partyid + "' && a.ProductID = '" + ProductID + "' order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPartywiseBills(string partyid)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.ChallanNumber,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,a.AccountID,"+
                   "c.AccountID,c.AccName from voucherchallanpurchase a inner join masteraccount c on a.Accountid = c.AccountID inner join detailpurchase d "+
                   "on a.ID = d.ChallanID where a.AccountID = '" + partyid + "' And TRIM(IFNULL(d.PurchaseID, '')) = '' GROUP BY a.ChallanNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForProductwiseBills(string partyid)
        {
            DataTable dtable = new DataTable();
            string strSql = "select b.ChallanID,b.ChallanNumber, b.DetailPurchaseID, a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock," +
                "a.ProdVATPercent,a.ProdCompShortName,a.ProdCompID,a.ProdIfOctroi,a.ProdShelfID,a.ProdIfBarCodeRequired,b.ProductID, b.StockID,b.PurchaseID," +
                "b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.AmountProdVAT,b.PurchaseVATPercent,b.SchemeQuantity," +
                "b.ReplacementQuantity,b.AmountItemDiscount,b.ItemDiscountPercent,b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.AmountSpecialDiscount," +
                "b.SpecialDiscountPercent,b.AmountPurchaseVAT,b.CSTPercent,b.AmountCST,b.AmountCreditNote,b.AmountCashDiscount,b.AmountSpecialDiscount," +
                "b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount,b.DistributorSaleRatePer,b.DistributorSaleRate from masterproduct " +
                "a inner join  detailpurchase b  on a.ProductID = b.ProductID inner join voucherchallanpurchase c on b.ChallanID = c.ID " +
                "inner join masteraccount d on  c.AccountID = d.AccountID Where C.AccountID = '"+ partyid + "' And TRIM(IFNULL(b.PurchaseID,'')) = ''order by b.ChallanNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPartywiseBills(string partyid, string fromDate, string toDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,a.AccountID,c.AccountID,c.AccName " +
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
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where a.statementnumber = " + statementNumber + " && voucherseries = '" + voucherSeries + "' order by VoucherType, VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForDiscount(string mfromDate, string mtoDate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.VoucherNumber,a.VoucherType,a.PurchaseBillNumber,a.VoucherDate,a.AmountNet,a.AmountItemDiscount,a.AmountSpecialDiscount,a.AmountSchemeDiscount,a.AmountCashDiscount,a.AccountID,c.AccountID,c.AccName " +
                 "from voucherpurchase a inner join masteraccount c on a.Accountid = c.AccountID where (a.AmountItemDiscount > 0 || a.AmountSpecialDiscount > 0 || a.amountSchemeDiscount > 0 || a.AmountCashDiscount > 0 ) && (a.VoucherDate >= '" + mfromDate + "' && a.VoucherDate <= '" + mtoDate + "' ) order by VoucherNumber";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        #region ChallanPurchaseSummary

        public DataTable GetOverviewDataForAllChallanSummary(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ChallanNumber, a.VoucherDate As ChallanDate, a.AmountNet As Amount, a.PurchaseID, a.AccountID," +
                            "b.AccName,c.VoucherNumber, c.VoucherDate ,c.PurchaseBillNumber From voucherchallanpurchase a inner join " +
                            " masteraccount b on a.AccountID = b.AccountID Left outer join voucherpurchase c on a.PurchaseID = c.purchaseID "+
                            "where a.VoucherDate >= '" + fromdate + "' &&  a.VoucherDate <= '" + todate + "'Order by a.challannumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPendingChallanSummary(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ChallanNumber, a.VoucherDate As ChallanDate, a.AmountNet As Amount, a.PurchaseID, a.AccountID," +
                            "b.AccName,c.VoucherNumber, c.VoucherDate ,c.PurchaseBillNumber From voucherchallanpurchase a inner join " +
                            " masteraccount b on a.AccountID = b.AccountID Left outer join voucherpurchase c on a.PurchaseID = c.purchaseID " +
                            "where a.VoucherDate >= '" + fromdate + "' &&  a.VoucherDate <= '" + todate + "' And a.PurchaseID IS NULL Order by a.challannumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPartyAllChallanSummary(string PartyID, string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ChallanNumber, a.VoucherDate As ChallanDate, a.AmountNet As Amount, a.PurchaseID, a.AccountID," +
                            "b.AccName,c.VoucherNumber, c.VoucherDate ,c.PurchaseBillNumber From voucherchallanpurchase a inner join " +
                            " masteraccount b on a.AccountID = b.AccountID Left outer join voucherpurchase c on a.PurchaseID = c.purchaseID " +
                            "where a.VoucherDate >= '" + fromdate + "' &&  a.VoucherDate <= '" + todate + "' And a.AccountID = '" + PartyID + "'Order by a.challannumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPartyPendingChallanSummary(string PartyID, string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ChallanNumber, a.VoucherDate As ChallanDate, a.AmountNet As Amount, a.PurchaseID, a.AccountID," +
                            "b.AccName,c.VoucherNumber, c.VoucherDate ,c.PurchaseBillNumber From voucherchallanpurchase a inner join " +
                            " masteraccount b on a.AccountID = b.AccountID Left outer join voucherpurchase c on a.PurchaseID = c.purchaseID " +
                            "where a.VoucherDate >= '" + fromdate + "' &&  a.VoucherDate <= '" + todate + "' And a.AccountID = '" + PartyID + "'And a.PurchaseID IS NULL Order by a.challannumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        #endregion ChallanPurchaseSummary

        #region ChallanPurchaseSummary

        public DataTable GetOverviewDataForAllChallanProductSummary(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ChallanNumber, a.VoucherDate As ChallanDate,d.Quantity * d.TradeRate As Amount, a.PurchaseID, a.AccountID," +
                            "e.ProdName, d.BatchNumber, d.ExpiryDate,d.Quantity ,d.TradeRate, b.AccName,c.VoucherNumber, c.VoucherDate ,c.PurchaseBillNumber From voucherchallanpurchase a inner join " +
                            " masteraccount b on a.AccountID = b.AccountID Left outer join voucherpurchase c on a.PurchaseID = c.purchaseID"+
                            " inner join  detailpurchase d on  a.ID  = d.ChallanID inner join masterproduct e on e.ProductID = d.ProductID" +
                            " where a.VoucherDate >= '" + fromdate + "' &&  a.VoucherDate <= '" + todate + "'Order by a.challannumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPendingChallanProductSummary(string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ChallanNumber, a.VoucherDate As ChallanDate, d.Quantity * d.TradeRate As Amount, a.PurchaseID, a.AccountID," +
                            "e.ProdName, d.BatchNumber, d.ExpiryDate,d.Quantity ,d.TradeRate, b.AccName,c.VoucherNumber, c.VoucherDate ,c.PurchaseBillNumber From voucherchallanpurchase a inner join " +
                            " masteraccount b on a.AccountID = b.AccountID Left outer join voucherpurchase c on a.PurchaseID = c.purchaseID"+ 
                            " inner join  detailpurchase d on  a.ID  = d.ChallanID inner join masterproduct e on e.ProductID = d.ProductID" +
                            " where a.VoucherDate >= '" + fromdate + "' &&  a.VoucherDate <= '" + todate + "' And a.PurchaseID IS NULL Order by a.challannumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForPartyAllChallanProductSummary(string PartyID, string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ChallanNumber, a.VoucherDate As ChallanDate, d.Quantity * d.TradeRate As Amount, a.PurchaseID, a.AccountID," +
                            "e.ProdName, d.BatchNumber, d.ExpiryDate,d.Quantity ,d.TradeRate, b.AccName,c.VoucherNumber, c.VoucherDate ,c.PurchaseBillNumber From voucherchallanpurchase a inner join " +
                            " masteraccount b on a.AccountID = b.AccountID Left outer join voucherpurchase c on a.PurchaseID = c.purchaseID "+
                            "inner join  detailpurchase d on  a.ID  = d.ChallanID inner join masterproduct e on e.ProductID = d.ProductID" +
                            " where a.VoucherDate >= '" + fromdate + "' &&  a.VoucherDate <= '" + todate + "' And a.AccountID = '" + PartyID + "'Order by a.challannumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataTable GetOverviewDataForPartyPendingChallanProductSummary(string PartyID, string fromdate, string todate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.ID, a.ChallanNumber, a.VoucherDate As ChallanDate, d.Quantity * d.TradeRate As Amount, a.PurchaseID, a.AccountID," +
                            "e.ProdName, d.BatchNumber, d.ExpiryDate,d.Quantity ,d.TradeRate, b.AccName,c.VoucherNumber, c.VoucherDate ,c.PurchaseBillNumber From voucherchallanpurchase a inner join " +
                            " masteraccount b on a.AccountID = b.AccountID Left outer join voucherpurchase c on a.PurchaseID = c.purchaseID "+
                            "inner join  detailpurchase d on  a.ID  = d.ChallanID inner join masterproduct e on e.ProductID = d.ProductID" +
                            " where a.VoucherDate >= '" + fromdate + "' &&  a.VoucherDate <= '" + todate + "' And a.AccountID = '" + PartyID + "'And a.PurchaseID IS NULL Order by a.challannumber ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        #endregion ChallanPurchaseSummary

        public DataTable GetOverviewDataCategory(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select sum(a.Quantity *a.TradeRate) as AmountNet,a.ProductID, sum(a.AmountItemDiscount) as AmountItemDiscount, " +
                 "sum(a.AmountSchemeDiscount) as AmountSchemeDiscount, sum(a.AmountSpecialDiscount) as AmountSpecialDiscount, " +
                 "sum(a.AmountCashDiscount) as AmountCashDiscount, b.ProductID,b.ProdCategoryID,c.ProductCategoryID,c.ProductCategoryName,d.voucherdate from detailpurchase a inner join masterproduct b on a.ProductID = b.ProductID inner join masterproductcategory c on b.ProdCategoryID = c.ProductCategoryID inner join voucherpurchase d on a.PurchaseID = d.PurchaseID  where d.voucherdate >= '" + mfromdate + "' && d.voucherdate <= '" + mtodate + "' group by c.ProductCategoryID order by c.ProductCategoryName ";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataCompany(string companyid, string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.ProductID,a.BatchNumber,a.Expiry,a.MRP,a.Quantity,(a.SchemeQuantity + a.ReplacementQuantity) as SchemeQuantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount,b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName, " +
                 " d.ProductID,d.ProdName,d.ProdLoosePack,d.ProdPack " +
                 "from detailpurchase a  inner join voucherpurchase b on a.purchaseID = b.purchaseID inner join masteraccount c on b.AccountID = c.AccountID inner join masterproduct d on a.ProductID = d.ProductID where d.ProdCompID = '" + companyid + "' && b.Voucherdate >= '" + mfromdate + "' && b.Voucherdate <='" + mtodate + "' order by d.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataNewProducts(string mfromdate, string mtodate)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.purchaseID,a.ProductID,a.BatchNumber,a.Expiry,a.MRP,a.Quantity,a.PurchaseRate, (a.PurchaseRate * a.Quantity) as Amount,b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName, " +
                 " d.ProductID,d.ProdName,d.ProdLoosePack,d.ProdPack " +
                 "from detailpurchase a  inner join voucherpurchase b on a.purchaseID = b.purchaseID inner join masteraccount c on b.AccountID = c.AccountID inner join masterproduct d on a.ProductID = d.ProductID where  d.CreatedDate >= '" + General.ShopDetail.Shopsy + "' &&  b.Voucherdate >= '" + mfromdate + "' && b.Voucherdate <='" + mtodate + "'  order by d.ProdName";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }

        public DataTable GetOverviewDataForLastPurchase(int ProductID)
        {
            DataTable dtable = new DataTable();
            string strSql = "Select a.PurchaseID,a.ProductID,a.BatchNumber,a.Quantity ,a.SchemeQuantity,a.ReplacementQuantity,a.PurchaseRate,a.MRP,a.Margin ,a.MarginAfterDiscount,a.DistributorSaleRatePer,a.DistributorSaleRate," +
                 "b.purchaseID,b.VoucherNumber,b.VoucherType,b.PurchaseBillNumber,b.VoucherDate,b.AccountID,c.AccountID,c.AccName " +
                 "from detailpurchase a  inner join voucherpurchase b on a.PurchaseID = b.purchaseID inner join masteraccount c on b.Accountid = c.AccountID where a.ProductID = '" + ProductID + "' order by b.VoucherDate";
            dtable = DBInterface.SelectDataTable(strSql);
            return dtable;
        }
        public DataRow ReadDetailsByID(string Id)
        {
            DataRow dRow = null;
            if (Id != "")
            {
                string strSql = "Select * from voucherchallanpurchase where ID='{0}' ";
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

        public DataRow ReadDetailsByVouNumber(int vouno, string voutype, string vouSeries, string vousubtype)
        {
            DataRow dRow = null;


            string strSql = "Select * from voucherpurchase where VoucherNumber = " + vouno + " && VoucherType = '" + voutype + "'  && Voucherseries = '" + vouSeries + "' && VoucherSubType = '" + vousubtype + "'";
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
        public DataTable ReadProductDetailsByIDPurchase(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select b.DetailPurchaseID, a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdCompID,a.ProdIfOctroi,a.ProdShelfID,a.ProdIfBarCodeRequired,b.ProductID, " +
                "b.StockID,b.PurchaseID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.AmountProdVAT," +
                "b.PurchaseVATPercent,b.SchemeQuantity,b.ReplacementQuantity,b.AmountItemDiscount,b.ItemDiscountPercent," +
                "b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.AmountSpecialDiscount,b.SpecialDiscountPercent,b.AmountPurchaseVAT,b.CSTPercent,b.AmountCST,b.AmountCreditNote,b.AmountCashDiscount,b.AmountSpecialDiscount,b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount,b.DistributorSaleRatePer,b.DistributorSaleRate, " +
                "d.ClosingStock,d.ScanCode,e.ShelfCode from masterproduct a inner join  detailpurchase b  on a.ProductID = b.ProductID left outer join tblstock d on b.StockID = d.StockID  left outer join mastershelf e on  a.ProdShelfID = e.shelfID where b.ChallanID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataTable ReadProductDetailWithoutPurchaseByID(string Id)
        {
            DataTable dt = null;
            if (Id != "")
            {
                string strsql = "select b.DetailPurchaseID, a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdCompID,a.ProdIfOctroi,a.ProdShelfID,a.ProdIfBarCodeRequired,b.ProductID, " +
                "b.StockID,b.PurchaseID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.AmountProdVAT," +
                "b.PurchaseVATPercent,b.SchemeQuantity,b.ReplacementQuantity,b.AmountItemDiscount,b.ItemDiscountPercent," +
                "b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.AmountSpecialDiscount,b.SpecialDiscountPercent,b.AmountPurchaseVAT,b.CSTPercent,b.AmountCST,b.AmountCreditNote,b.AmountCashDiscount,b.AmountSpecialDiscount,b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount,b.DistributorSaleRatePer,b.DistributorSaleRate, " +
                "d.ClosingStock,d.ScanCode,e.ShelfCode from masterproduct a inner join  detailpurchase b  on a.ProductID = b.ProductID left outer join tblstock d on b.StockID = d.StockID  left outer join mastershelf e on  a.ProdShelfID = e.shelfID where b.ChallanID = '{0}' And TRIM(IFNULL(b.PurchaseID, '')) = '' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public DataTable ReadProductDetailsByIDPurchaseAndDetailID(string Id, string DetailID)
        {
            DataTable dt = null;
            if (Id != "" && DetailID != null)
            {
                string strsql = "select b.DetailPurchaseID, a.ProductID, a.ProdName,a.Prodloosepack,a.prodpack,a.ProdClosingStock,a.ProdVATPercent,a.ProdCompShortName,a.ProdCompID,a.ProdIfOctroi,a.ProdShelfID,a.ProdIfBarCodeRequired,b.ProductID, " +
                "b.StockID,b.PurchaseID,b.BatchNumber,b.MRP,b.PurchaseRate,b.SaleRate,b.TradeRate,b.Expiry,b.ExpiryDate,b.Quantity,b.AmountProdVAT," +
                "b.PurchaseVATPercent,b.SchemeQuantity,b.ReplacementQuantity,b.AmountItemDiscount,b.ItemDiscountPercent," +
                "b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.AmountSpecialDiscount,b.SpecialDiscountPercent,b.AmountPurchaseVAT,b.CSTPercent,b.AmountCST,b.AmountCreditNote,b.AmountCashDiscount,b.AmountSpecialDiscount,b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount,b.DistributorSaleRatePer,b.DistributorSaleRate, " +
                "d.ClosingStock,d.ScanCode,e.ShelfCode from masterproduct a inner join  detailpurchase b  on a.ProductID = b.ProductID left outer join tblstock d on b.StockID = d.StockID  left outer join mastershelf e on  a.ProdShelfID = e.shelfID where b.ChallanID = '{0}' And b.DetailPurchaseID = '{1}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id, DetailID);
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
                "b.AmountSchemeDiscount,b.SchemeDiscountPercent,b.SpecialDiscountPercent,b.AmountPurchaseVAT,b.CSTPercent,b.AmountCST,b.AmountCreditNote,b.AmountCashDiscount,b.AmountSpecialDiscount,b.Quantity*b.TradeRate as Amount,b.Margin,b.MarginAfterDiscount, " +
                "d.ClosingStock,e.ShelfCode from masterproduct a inner join  deleteddetailpurchase b  on a.ProductID = b.ProductID left outer join tblstock d on b.StockID = d.StockID  left outer join mastershelf e on  a.ProdShelfID = e.shelfID where b.PurchaseID = '{0}' order by b.SerialNumber";
                strsql = string.Format(strsql, Id);
                dt = DBInterface.SelectDataTable(strsql);
            }
            return dt;
        }
        public bool CheckforUniqueBillNumberforEdit(string Id, string ChallanNumber, string AccID)
        {
            bool retVal = true;
            string strSql = "";
            DataTable dt = new DataTable();
            DataRow dr = null;
            strSql = "select * from voucherchallanpurchase where accountID = '" + AccID + "' && ChallanNumber = '" + ChallanNumber + "' && PurchaseID != '" + Id + "'";
            dr = DBInterface.SelectFirstRow(strSql);
            if (dr != null)
            {
                retVal = false;
            }

            return retVal;
        }
        public bool CheckforUniqueBillNumberforNew(string ChallanNumber, string AccID)
        {
            bool retVal = true;
            string strSql = "";
            DataRow dr = null;
            DataTable dt = new DataTable();
            strSql = "select * from voucherchallanpurchase where accountID = '" + AccID + "' && ChallanNumber = '" + ChallanNumber + "'";
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
            strSql = "select * from voucherpurchase where voucherDate >= '" + VoucherDate + "' && voucherDate <= '" + VoucherDate + "' && statementnumber > 0";
            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                retVal = false;
            }

            return retVal;
        }

        public void UpdateTempPurchasewithVoucherID(string iD, string tempID, int quantity)
        {
            string strSql = "Update tblTempPurchase set ClearedInVoucherID = '" + iD + "', ClearedQuantity =  clearedQuantity +  " + quantity + " where ID = '" + tempID + "'";
            DBInterface.ExecuteQuery(strSql);
        }

        public DataRow GetFirstRecord(string voutype, string vouSubType, string VouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select * from voucherpurchase where VoucherType = '" + voutype + "'  && VoucherSubType = '" + vouSubType + "' &&  VoucherSeries = '" + VouSeries + "'  order by Vouchernumber ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        public DataRow GetLastRecordForPurchase(string vouType, string vouSubType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select * from voucherpurchase where VoucherType = '" + vouType + "'  && VoucherSubType = '" + vouSubType + "' && VoucherSeries = '" + vouSeries + "'  order by Vouchernumber desc ";
                dRow = DBInterface.SelectFirstRow(strSql);
            }
            return dRow;
        }

        internal DataRow GetLastVoucherNumber(string vouType, string vouSubType, string vouSeries)
        {
            DataRow dRow = null;
            {
                string strSql = "Select Vouchernumber from voucherpurchase where VoucherType =  '" + vouType + "'  &&  VoucherSubType = '" + vouSubType + "' &&  VoucherSeries = '" + vouSeries + "' order by Vouchernumber desc ";
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
                                    "where b.AccountID = '" + accID + "' &&  b.voucherdate >= '" + fromdate + "'  && b.VoucherDate <= '" + todate + "' && b.AmountBalance > 0 order by b.VoucherDate,b.VoucherNumber";


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
                                    "inner join masteraccount a on b.AccountID = a.AccountID where b.VoucherType = 'PCR' &&  b.AmountBalance > 0   &&  b.voucherdate >= '" + fromdate + "'  && b.VoucherDate <= '" + todate + "' order by a.AccName, a.AccAddress1,b.VoucherDate";


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

        public bool CheckforProductforDistributorID(string distributorID, string distributorProductID)
        {
            bool retVal = true;
            string strSql = "";
            DataRow dr = null;
            strSql = "select * from tblbillimportlink where DistributorID = '" + distributorID + "' && DistributorProductID = '" + distributorProductID + "'";
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

                    string strSql = "select  ProductNumberForBarcode from masterproduct where ProductID = '{0}'";
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

    }
}