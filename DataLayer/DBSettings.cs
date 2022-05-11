using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;

namespace EcoMart.DataLayer
{
    public class DBSettings
    {
        public DBSettings()
        {
        }
        public DataRow GetOverviewData(string voucherseries)
        {
            DataRow drow = null;
            try
            {
                string strSql = "Select * from tblsettings where ID = '"+voucherseries+"'";
                drow = DBInterface.SelectFirstRow(strSql);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return drow;
        }

        public bool AddDetails(string MsetPurchaseCopyPurchseOrder, string MsetPurchaseRounding,string MsetPurchaseHold, string MsetPurchaseIfPTR,  string MsetPurchaseReadPurchaseOrder,
                    string MsetPurchaseChangeSaleRate, string MsetPurchaseAllowExpriedItems, string MsetSaleCreditSale,  string MsetSaleAskDiscountinCounterSale, string MsetAskRoundingInSale,
                    string MsetSaleShowProfitInSaleBill, string MsetSaleIPDOPD, string MsetSaleDiscountWithoutVAT, string MsetGeneralProfitPercentageByPurchaseRate, string MsetGeneralExpiryLast, string MsetGeneralBatchNumberRequired, string MsetGeneralExpiryDateRequired, string MsetAskOperatorVoucherSale,
                    string MsetAskOperatorOtherThanVoucherSale, string MsetAskOperatorPurchase, string MsetAskOperatorCRDB, string MsetAskOperatorOpeningStock, string MsetAskOperatorCorrectioninRate, string MsetAskOperatorJV,
                    string MsetAskOperatorCashBankReceipt, string MsetAskOperatorCashBankPayment, string MsetScanBarcode, string MsetPurchaseIncludeCreditPurchase, string MsetPurchaseUpdateVATInMaster, string MsetSaleIncludeCreditSale, string MsetSaleSaveCustomerInMaster, string MsetSaleShowOnlyMRPInCounterSale,
                    string MsetSaleAllowDistributorSale, string MsetSaleOnlyCashSaleInCounterSale, string MsetSaleRoundingTo10Paise, string MsetSaleRoundingToPreviousRupee, string MsetSaleEditRateInCounterSale, string MsetSaleAllowNegativeStock, string MsetRemoveCodeFromCreditNote, string MsetAllowPendingCashMemo, string MsetFixedNarration,
                    string MsetCreditNoteDefaultTransferToAccount, string MsetCreditNoteReturnRateDisable, string MsetSaleAllowBackDate, string MsetSaleCounterSaleSingleGrid, string MsetCreditNoteDoNotShowPurchaseRate, string MsetSaleSelectVATPercent, 
                    string MsetGeneralAskDatesInSearch, string MsetGeneralAlphabetical, string MsetSaleProductRefreshButton, double MsetSaleMaxDiscount, double MsetOpeningStockGetPercent, string MsetSaleTenderAmount, string MsetCashBankShowDiscount, int MsetProductNameWidthInSaleInvoice, 
                    string SmsSetPatientSale, string SmsSetDebtorSale, string SmsSetCreditCardSale, string SmsSetInstitutionalSale, string SmsSetBankPaymentSale, string SmsSetBankReceiptSale, string SmsSetCashPaymentSale, string SmsSetCashReceiptSale, string MsetAllowPrintMessage)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(MsetPurchaseCopyPurchseOrder, MsetPurchaseRounding,MsetPurchaseHold, MsetPurchaseIfPTR, MsetPurchaseReadPurchaseOrder,
                    MsetPurchaseChangeSaleRate,MsetPurchaseAllowExpriedItems, MsetSaleCreditSale, MsetSaleAskDiscountinCounterSale, MsetAskRoundingInSale,
                    MsetSaleShowProfitInSaleBill, MsetSaleIPDOPD, MsetSaleDiscountWithoutVAT, MsetGeneralProfitPercentageByPurchaseRate, MsetGeneralExpiryLast, MsetGeneralBatchNumberRequired, MsetGeneralExpiryDateRequired, MsetAskOperatorVoucherSale,
                    MsetAskOperatorOtherThanVoucherSale, MsetAskOperatorPurchase, MsetAskOperatorCRDB, MsetAskOperatorOpeningStock, MsetAskOperatorCorrectioninRate, MsetAskOperatorJV,
                    MsetAskOperatorCashBankReceipt, MsetAskOperatorCashBankPayment, MsetScanBarcode, MsetPurchaseIncludeCreditPurchase, MsetPurchaseUpdateVATInMaster, MsetSaleIncludeCreditSale, MsetSaleSaveCustomerInMaster, MsetSaleShowOnlyMRPInCounterSale,
                    MsetSaleAllowDistributorSale, MsetSaleOnlyCashSaleInCounterSale, MsetSaleRoundingTo10Paise, MsetSaleRoundingToPreviousRupee, MsetSaleEditRateInCounterSale, MsetSaleAllowNegativeStock, MsetRemoveCodeFromCreditNote, MsetAllowPendingCashMemo, MsetFixedNarration,
                    MsetCreditNoteDefaultTransferToAccount, MsetCreditNoteReturnRateDisable, MsetSaleAllowBackDate, MsetSaleCounterSaleSingleGrid, MsetCreditNoteDoNotShowPurchaseRate, MsetSaleSelectVATPercent, MsetGeneralAskDatesInSearch, MsetGeneralAlphabetical,
                    MsetSaleProductRefreshButton, MsetSaleMaxDiscount, MsetOpeningStockGetPercent, MsetSaleTenderAmount, MsetCashBankShowDiscount, MsetProductNameWidthInSaleInvoice,
                    SmsSetPatientSale, SmsSetDebtorSale, SmsSetCreditCardSale, SmsSetInstitutionalSale, SmsSetBankPaymentSale, SmsSetBankReceiptSale, SmsSetCashPaymentSale, SmsSetCashReceiptSale, MsetAllowPrintMessage);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }






        public bool UpdateDetails(string MsetPurchaseCopyPurchaseOrder, string MsetPurchaseRounding, string MsetPurchaseHold, string MsetPurchaseIfPTR, string MsetPurchaseReadPurchaseOrder,string MsetPurchaseChangeSaleRate,string MsetPurchaseAllowExpriedItems,
            string MsetSaleCreditSale, string MsetSaleAskDiscountinCounterSale, string MsetAskRoundingInSale, string MsetSaleShowProfitInSaleBill, string MsetSaleIPDOPD,
            string MsetSaleDiscountWithoutVAT, string MsetGeneralProfitPercentageByPurchaseRate, string MsetGeneralExpiryLast, string MsetGeneralBatchNumberRequired, string MsetGeneralExpiryDateRequired,
            string MsetAskOperatorVoucherSale,string MsetAskOperatorOtherThanVoucherSale, string MsetAskOperatorPurchase, string MsetAskOperatorCRDB, string MsetAskOperatorOpeningStock, 
            string MsetAskOperatorCorrectioninRate, string MsetAskOperatorJV,string MsetAskOperatorCashBankReceipt, string MsetAskOperatorCashBankPayment, string MsetScanBarCode,
            string MsetSaleIncludeCreditSale, string MsetSaleSaveCustomerInMaster, string MsetSaleShowOnlyMRPInCounterSale,
            string MsetSaleAllowDistributorSale, string MsetSaleOnlyCashSaleInCounterSale, string MsetSaleRoundingTo10Paise, string MsetSaleRoundingToPreviousRupee, string MsetSaleEditRateInCounterSale,
            string MsetSaleAllowNegativeStock, string MsetRemoveCodeFromCreditNote, string MsetAllowPendingCashMemo, string MsetFixedNarration,string MsetCreditNoteDefaultTransferToAccount,
            string MsetCreditNoteReturnRateDisable, string MsetSaleAllowBackDate, string MsetSaleCounterSaleSingleGrid, string MsetCreditNoteDoNotShowPurchaseRate, string MsetSaleSelectVATPercent,
            string MsetGeneralAskDatesInSearch, string MsetGeneralAlphabetical, string MsetSaleProductRefreshButton,double MsetSaleMaxDiscount, double MsetOpeningStockGetPercent, 
            string MsetSaleTenderAmount, string MsetCashBankShowDiscount, int MsetProductNameWidthInSaleInvoice,string SmsSetPatientSale,
            string SmsSetDebtorSale, string SmsSetCreditCardSale, string SmsSetInstitutionalSale, string SmsSetBankPaymentSale, string SmsSetBankReceiptSale, string SmsSetCashPaymentSale, string SmsSetCashReceiptSale)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(MsetPurchaseCopyPurchaseOrder, MsetPurchaseRounding,MsetPurchaseHold,MsetPurchaseIfPTR, MsetPurchaseReadPurchaseOrder,MsetPurchaseChangeSaleRate, MsetPurchaseAllowExpriedItems,
                MsetSaleCreditSale, MsetSaleAskDiscountinCounterSale, MsetAskRoundingInSale, MsetSaleShowProfitInSaleBill, MsetSaleIPDOPD,
                MsetSaleDiscountWithoutVAT, MsetGeneralProfitPercentageByPurchaseRate, MsetGeneralExpiryLast, MsetGeneralBatchNumberRequired, MsetGeneralExpiryDateRequired,
                MsetAskOperatorVoucherSale, MsetAskOperatorOtherThanVoucherSale, MsetAskOperatorPurchase, MsetAskOperatorCRDB, MsetAskOperatorOpeningStock,
                MsetAskOperatorCorrectioninRate, MsetAskOperatorJV, MsetAskOperatorCashBankReceipt, MsetAskOperatorCashBankPayment, MsetScanBarCode,
                MsetSaleIncludeCreditSale, MsetSaleSaveCustomerInMaster, MsetSaleShowOnlyMRPInCounterSale,MsetSaleAllowDistributorSale, MsetSaleOnlyCashSaleInCounterSale,
                MsetSaleRoundingTo10Paise, MsetSaleRoundingToPreviousRupee, MsetSaleEditRateInCounterSale, MsetSaleAllowNegativeStock, MsetRemoveCodeFromCreditNote,
                MsetAllowPendingCashMemo, MsetFixedNarration, MsetCreditNoteDefaultTransferToAccount, MsetCreditNoteReturnRateDisable, MsetSaleAllowBackDate,
                MsetSaleCounterSaleSingleGrid, MsetCreditNoteDoNotShowPurchaseRate, MsetSaleSelectVATPercent, MsetGeneralAskDatesInSearch, MsetGeneralAlphabetical,
                MsetSaleProductRefreshButton, MsetSaleMaxDiscount, MsetOpeningStockGetPercent, MsetSaleTenderAmount,
                MsetCashBankShowDiscount, MsetProductNameWidthInSaleInvoice,SmsSetPatientSale, SmsSetDebtorSale, SmsSetCreditCardSale,
                SmsSetInstitutionalSale, SmsSetBankPaymentSale, SmsSetBankReceiptSale, SmsSetCashPaymentSale, SmsSetCashReceiptSale);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
      






        private string GetInsertQuery(string MsetPurchaseCopyPurchseOrder, string MsetPurchaseRounding, string MsetPurchaseHold, string MsetPurchaseIfPTR,string MsetPurchaseReadPurchaseOrder,
                    string MsetPurchaseChangeSaleRate,string MsetPurchaseAllowExpriedItems, string MsetSaleCreditSale, string MsetSaleAskDiscountinCounterSale, string MsetAskRoundingInSale,
                    string MsetSaleShowProfitInSaleBill, string MsetSaleIPDOPD, string MsetSaleDiscountWithoutVAT, string MsetGeneralProfitPercentageByPurchaseRate, string MsetGeneralExpiryLast, string MsetGeneralBatchNumberRequired, string MsetGeneralExpiryDateRequired, string MsetAskOperatorVoucherSale,
                    string MsetAskOperatorOtherThanVoucherSale, string MsetAskOperatorPurchase, string MsetAskOperatorCRDB, string MsetAskOperatorOpeningStock, string MsetAskOperatorCorrectioninRate, string MsetAskOperatorJV,
                    string MsetAskOperatorCashBankReceipt, string MsetAskOperatorCashBankPayment, string MsetScanBarCode, string MsetPurchaseIncludeCreditPurchase, string MsetPurchaseUpdateVATInMaster, string MsetSaleIncludeCreditSale, string MsetSaleSaveCustomerInMaster, string MsetSaleShowOnlyMRPInCounterSale,
                    string MsetSaleAllowDistributorSale, string MsetSaleOnlyCashSaleInCounterSale, string MsetSaleRoundingTo10Paise, string MsetSaleRoundingToPreviousRupee, string MsetSaleEditRateInCounterSale, string MsetSaleAllowNegativeStock, string MsetRemoveCodeFromCreditNote, string MsetAllowPendingCashMemo, string MsetFixedNarration,
                    string MsetCreditNoteDefaultTransferToAccount, string MsetCreditNoteReturnRateDisable, string MsetSaleAllowBackDate, string MsetSaleCounterSaleSingleGrid, string MsetCreditNoteDoNotShowPurchaseRate, string MsetSaleSelectVATPercent, string MsetGeneralAskDatesInSearch, string MsetGeneralAlphabetical, 
                    string MsetSaleProductRefreshButton, double MsetSaleMaxDiscount, double MsetOpeningStockGetPercent, string MsetSaleTenderAmount, string MsetCashBankShowDiscount, int MsetProductNameWidthInSaleInvoice,
                     string SmsSetPatientSale, string SmsSetDebtorSale, string SmsSetCreditCardSale, string SmsSetInstitutionalSale, string SmsSetBankPaymentSale, string SmsSetBankReceiptSale, string SmsSetCashPaymentSale, string SmsSetCashReceiptSale, string MsetAllowPrintMessage)
      
        {
            Query objQuery = new Query();
            objQuery.Table = "tblsettings";
            objQuery.AddToQuery("ID", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("setPurchaseCopyPurchaseOrder", MsetPurchaseCopyPurchseOrder);
            objQuery.AddToQuery("setPurchaseRounding", MsetPurchaseRounding);
            objQuery.AddToQuery("setPurchaseHold", MsetPurchaseHold);
            objQuery.AddToQuery("setPurchaseIfPTR", MsetPurchaseIfPTR);
            //objQuery.AddToQuery("setPurchaseIfCreditPurchase", MsetPurchaseIfCreditPurchase);
            objQuery.AddToQuery("setPurchaseAddVATInPurchaseRate", "N");
            objQuery.AddToQuery("setPurchaseAddVATInSaleRate", "N");
            objQuery.AddToQuery("setPurchaseReadPurchaseOrder", MsetPurchaseReadPurchaseOrder);
            //objQuery.AddToQuery("setPurchaseGetPendingScheme", MsetPurchaseGetPendingScheme);
            //objQuery.AddToQuery("setPurchaseIfProductWithOctroi", MsetPurchaseIfProductWithOctroi);
            //objQuery.AddToQuery("setPurchaseOctroionZeroVAT", MsetPurchaseOctroionZeroVAT);
            objQuery.AddToQuery("setPurchaseChangeSaleRate", MsetPurchaseChangeSaleRate);
            objQuery.AddToQuery("setPurchaseAllowExpiredItems", MsetPurchaseAllowExpriedItems);
            //objQuery.AddToQuery("setPurchaseMarginbyPurchaseRate", MsetPurchaseMarginByPurchaseRate);
          //  objQuery.AddToQuery("setSaleRoundOff", MsetSaleRoundOff);
            objQuery.AddToQuery("setSaleCreditStatement", MsetSaleCreditSale);
            objQuery.AddToQuery("setSaleAskDiscountinCounterSale", MsetSaleAskDiscountinCounterSale);
            objQuery.AddToQuery("setSaleAskRoundinginSale", MsetAskRoundingInSale);
            objQuery.AddToQuery("setSaleShowProfitInSaleBill", MsetSaleShowProfitInSaleBill);
            objQuery.AddToQuery("setSaleIPDOPD", MsetSaleIPDOPD);
            objQuery.AddToQuery("setSaleDiscountWithoutVAT", MsetSaleDiscountWithoutVAT);
            objQuery.AddToQuery("setSaleSelectVATPercent", MsetSaleSelectVATPercent);
          
            objQuery.AddToQuery("setSaleTenderAmount", MsetSaleTenderAmount);
          //  objQuery.AddToQuery("setSaleDoNotShowNegetiveStock", MsetSaleDoNotShowNegetiveStock);
         
            objQuery.AddToQuery("setGeneralProfitPercentageByPurchaseRate", MsetGeneralProfitPercentageByPurchaseRate);
            objQuery.AddToQuery("setGeneralExpiryLast", MsetGeneralExpiryLast);
            objQuery.AddToQuery("setGeneralBatchNumberRequired", MsetGeneralBatchNumberRequired);
            objQuery.AddToQuery("setGeneralExpiryDateRequired", MsetGeneralExpiryDateRequired);
            objQuery.AddToQuery("setGeneralAskDatesInSearch", MsetGeneralAskDatesInSearch);
            objQuery.AddToQuery("MsetGeneralAlphabetical", MsetGeneralAlphabetical);
            objQuery.AddToQuery("setAskOperatorVoucherSale", MsetAskOperatorVoucherSale);
            objQuery.AddToQuery("setAskOperatorOtherThanVoucherSale", MsetAskOperatorOtherThanVoucherSale);
            objQuery.AddToQuery("setAskOperatorPurchase", MsetAskOperatorPurchase);
            objQuery.AddToQuery("setAskOperatorCRDB", MsetAskOperatorCRDB);
            objQuery.AddToQuery("setAskOperatorOpeningStock", MsetAskOperatorOpeningStock);
            objQuery.AddToQuery("setAskOperatorCorrectionInRate", MsetAskOperatorCorrectioninRate);
            objQuery.AddToQuery("setAskOperatorJV", MsetAskOperatorJV);
            objQuery.AddToQuery("setAskOperatorCashBankReceipt", MsetAskOperatorCashBankReceipt);
            objQuery.AddToQuery("setAskOperatorCashBankPayment", MsetAskOperatorCashBankPayment);
            objQuery.AddToQuery("setScanBarCode", MsetScanBarCode);
            objQuery.AddToQuery("setPurchaseIncludeCreditPurchaseInStatements", MsetPurchaseIncludeCreditPurchase);
            objQuery.AddToQuery("setPurchaseUpdateVATInMaster", MsetPurchaseUpdateVATInMaster);
            objQuery.AddToQuery("setSaleIncludeCreditsaleInStatements", MsetSaleIncludeCreditSale);
            objQuery.AddToQuery("setSaleSaveCustomerInMaster", MsetSaleSaveCustomerInMaster);
            objQuery.AddToQuery("setSaleShowOnlyMRPInCounterSale", MsetSaleShowOnlyMRPInCounterSale);
            objQuery.AddToQuery("setSaleAllowDistributorSale", MsetSaleAllowDistributorSale);
            objQuery.AddToQuery("setSaleOnlyCashSaleInCounterSale", MsetSaleOnlyCashSaleInCounterSale);
            objQuery.AddToQuery("setSaleRoundingTo10Paise", MsetSaleRoundingTo10Paise);
            objQuery.AddToQuery("setSaleRoundingToPreviousRupee", MsetSaleRoundingToPreviousRupee);
            objQuery.AddToQuery("setSaleEditRateInCounterSale", MsetSaleEditRateInCounterSale);
            objQuery.AddToQuery("setSaleAllowNegativeStock", MsetSaleAllowNegativeStock);
            objQuery.AddToQuery("setCreditNoteRemoveCode", MsetRemoveCodeFromCreditNote);
            objQuery.AddToQuery("setSaleAllowPendingCashMemo", MsetAllowPendingCashMemo);
            objQuery.AddToQuery("setFixedNarration", MsetFixedNarration);
            objQuery.AddToQuery("setCreditNoteDefaultTransferToAccount", MsetCreditNoteDefaultTransferToAccount);
            objQuery.AddToQuery("setCreditNoteReturnRateDisable", MsetCreditNoteReturnRateDisable);
            objQuery.AddToQuery("setSaleAllowBackDate", MsetSaleAllowBackDate);
            objQuery.AddToQuery("setSaleCounterSaleSingleGrid", MsetSaleCounterSaleSingleGrid);
            objQuery.AddToQuery("setSaleProductRefreshInCounterSale", MsetSaleProductRefreshButton);
            objQuery.AddToQuery("setSaleMaxDiscount", MsetSaleMaxDiscount);
            objQuery.AddToQuery("setOpeningStockGetPercent", MsetOpeningStockGetPercent);
            objQuery.AddToQuery("setCreditNoteDoNotShowPurchaseRate", MsetCreditNoteDoNotShowPurchaseRate);
            objQuery.AddToQuery("setCashBankShowDiscount", MsetCashBankShowDiscount); //Amar
            objQuery.AddToQuery("setProductNameWidthInSaleInvoice", MsetProductNameWidthInSaleInvoice);

            objQuery.AddToQuery("SmsPatientSaleSet", SmsSetPatientSale); //Amar
            objQuery.AddToQuery("SmsDebtorSaleSet", SmsSetDebtorSale);
            objQuery.AddToQuery("SmsCreditCardSaleSet", SmsSetCreditCardSale);
            objQuery.AddToQuery("SmsInstitutionalSaleSet", SmsSetInstitutionalSale);
            objQuery.AddToQuery("SmsBankPaymentSet", SmsSetBankPaymentSale);
            objQuery.AddToQuery("SmsBankReceiptSet", SmsSetBankReceiptSale);
            objQuery.AddToQuery("SmsCashReceiptSet", SmsSetCashReceiptSale);
            objQuery.AddToQuery("SmsCashPaymentSet", SmsSetCashPaymentSale);
            objQuery.AddToQuery("setPrintSettingYesNo", MsetAllowPrintMessage);//end

            return objQuery.InsertQuery();
        }
       

        private string GetUpdateQuery(string MsetPurchaseCopyPurchseOrder, string MsetPurchaseRounding, string MsetPurchaseHold, string MsetPurchaseIfPTR, string MsetPurchaseReadPurchaseOrder,string MsetPurchaseChangeSaleRate, string MsetPurchaseAllowExpriedItems,
            string MsetSaleCreditSale, string MsetSaleAskDiscountinCounterSale, string MsetAskRoundingInSale,string MsetSaleShowProfitInSaleBill, string MsetSaleIPDOPD,
            string MsetSaleDiscountWithoutVAT, string MsetGeneralProfitPercentageByPurchaseRate, string MsetGeneralExpiryLast, string MsetGeneralBatchNumberRequired, string MsetGeneralExpiryDateRequired,
            string MsetAskOperatorVoucherSale,string MsetAskOperatorOtherThanVoucherSale, string MsetAskOperatorPurchase, string MsetAskOperatorCRDB, string MsetAskOperatorOpeningStock,
            string MsetAskOperatorCorrectioninRate, string MsetAskOperatorJV, string MsetAskOperatorCashBankReceipt, string MsetAskOperatorCashBankPayment, string MsetScanBarCode,
            string MsetSaleIncludeCreditSale, string MsetSaleSaveCustomerInMaster, string MsetSaleShowOnlyMRPInCounterSale,
            string MsetSaleAllowDistributorSale,string MsetSaleOnlyCashSaleInCounterSale, string MsetSaleRoundingTo10Paise, string MsetSaleRoundingToPreviousRupee, string MsetSaleEditRateInCounterSale,
            string MsetSaleAllowNegativeStock, string MsetRemoveCodeFromCreditNote, string MsetAllowPendingCashMemo, string MsetFixedNarration,string MsetCreditNoteDefaultTransferToAccount,
            string MsetCreditNoteReturnRateDisable, string MsetSaleAllowBackDate, string MsetSaleCounterSaleSingleGrid, string MsetCreditNoteDoNotShowPurchaseRate, string MsetSaleSelectVATPercent,
            string MsetGeneralAskDatesInSearch, string MsetGeneralAlphabetical,string MsetSaleProductRefreshButton, double MsetSaleMaxDiscount, double MsetOpeningStockGetPercent,
            string MsetSaleTenderAmount, string MsetCashBankShowDiscount, int MsetProductNameWidthInSaleInvoice,string SmsSetPatientSale,
            string SmsSetDebtorSale, string SmsSetCreditCardSale, string SmsSetInstitutionalSale, string SmsSetBankPaymentSale, string SmsSetBankReceiptSale, string SmsSetCashPaymentSale, string SmsSetCashReceiptSale)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblsettings";
            objQuery.AddToQuery("ID", General.ShopDetail.ShopVoucherSeries, true);
            objQuery.AddToQuery("setPurchaseCopyPurchaseOrder", MsetPurchaseCopyPurchseOrder);
            objQuery.AddToQuery("setPurchaseRounding", MsetPurchaseRounding);
            objQuery.AddToQuery("setPurchaseHold", MsetPurchaseHold);
            objQuery.AddToQuery("setPurchaseIfPTR", MsetPurchaseIfPTR);
//objQuery.AddToQuery("setPurchaseIfCreditPurchase", MsetPurchaseIfCreditPurchase);
       //     objQuery.AddToQuery("setPurchaseAddVATInPurchaseRate", "N");
         //   objQuery.AddToQuery("setPurchaseAddVATInSaleRate", "N");
            objQuery.AddToQuery("setPurchaseReadPurchaseOrder", MsetPurchaseReadPurchaseOrder);
         //   objQuery.AddToQuery("setPurchaseGetPendingScheme", MsetPurchaseGetPendingScheme);
        //    objQuery.AddToQuery("setPurchaseIfProductWithOctroi", MsetPurchaseIfProductWithOctroi);
         //   objQuery.AddToQuery("setPurchaseOctroionZeroVAT", MsetPurchaseOctroionZeroVAT);
            objQuery.AddToQuery("setPurchaseChangeSaleRate", MsetPurchaseChangeSaleRate);
            objQuery.AddToQuery("setPurchaseAllowExpiredItems", MsetPurchaseAllowExpriedItems);
         //   objQuery.AddToQuery("setPurchaseMarginbyPurchaseRate", MsetPurchaseMarginByPurchaseRate);
        //    objQuery.AddToQuery("setSaleRoundOff", MsetSaleRoundOff);
            objQuery.AddToQuery("setSaleCreditStatement", MsetSaleCreditSale);
          //  objQuery.AddToQuery("setSaleAskDiscountinCounterSale", MsetSaleAskDiscountinCounterSale);
          //  objQuery.AddToQuery("setSaleAskRoundinginSale", MsetAskRoundingInSale);
            objQuery.AddToQuery("setSaleShowProfitInSaleBill", MsetSaleShowProfitInSaleBill);
         //   objQuery.AddToQuery("setSaleIPDOPD", MsetSaleIPDOPD);
         //   objQuery.AddToQuery("setSaleDiscountWithoutVAT", MsetSaleDiscountWithoutVAT);
         //   objQuery.AddToQuery("setSaleSelectVATPercent", MsetSaleSelectVATPercent);
            // ss 5/11
            objQuery.AddToQuery("setSaleTenderAmount", MsetSaleTenderAmount);
         //   objQuery.AddToQuery("setSaleDoNotShowNegetiveStock", MsetSaleDoNotShowNegetiveStock);
            // ss 5/11
         //   objQuery.AddToQuery("setGeneralProfitPercentageByPurchaseRate", MsetGeneralProfitPercentageByPurchaseRate);
            objQuery.AddToQuery("setGeneralExpiryLast", MsetGeneralExpiryLast);
            objQuery.AddToQuery("setGeneralBatchNumberRequired", MsetGeneralBatchNumberRequired);
            objQuery.AddToQuery("setGeneralExpiryDateRequired", MsetGeneralExpiryDateRequired);
            objQuery.AddToQuery("setGeneralAskDatesInSearch", MsetGeneralAskDatesInSearch);
            objQuery.AddToQuery("setGeneralAlphabetical", MsetGeneralAlphabetical);
            objQuery.AddToQuery("setAskOperatorVoucherSale", MsetAskOperatorVoucherSale);
            objQuery.AddToQuery("setAskOperatorOtherThanVoucherSale", MsetAskOperatorOtherThanVoucherSale);
            objQuery.AddToQuery("setAskOperatorPurchase", MsetAskOperatorPurchase);
            objQuery.AddToQuery("setAskOperatorCRDB", MsetAskOperatorCRDB);
            objQuery.AddToQuery("setAskOperatorOpeningStock", MsetAskOperatorOpeningStock);
            objQuery.AddToQuery("setAskOperatorCorrectionInRate", MsetAskOperatorCorrectioninRate);
            objQuery.AddToQuery("setAskOperatorJV", MsetAskOperatorJV);
            objQuery.AddToQuery("setAskOperatorCashBankReceipt", MsetAskOperatorCashBankReceipt);
            objQuery.AddToQuery("setAskOperatorCashBankPayment", MsetAskOperatorCashBankPayment);
            objQuery.AddToQuery("setScanBarCode", MsetScanBarCode);
            //objQuery.AddToQuery("setPurchaseIncludeCreditPurchaseInStatements", MsetPurchaseIncludeCreditPurchase);
            //objQuery.AddToQuery("setPurchaseUpdateVATInMaster", MsetPurchaseUpdateVATInMaster);
         //   objQuery.AddToQuery("setSaleIncludeCreditsaleInStatements", MsetSaleIncludeCreditSale);
          //  objQuery.AddToQuery("setSaleSaveCustomerInMaster", MsetSaleSaveCustomerInMaster);
        //    objQuery.AddToQuery("setSaleShowOnlyMRPInCounterSale", MsetSaleShowOnlyMRPInCounterSale);
         //   objQuery.AddToQuery("setSaleAllowDistributorSale", MsetSaleAllowDistributorSale);
        //    objQuery.AddToQuery("setSaleOnlyCashSaleInCounterSale", MsetSaleOnlyCashSaleInCounterSale);
        //    objQuery.AddToQuery("setSaleRoundingTo10Paise", MsetSaleRoundingTo10Paise);
        //    objQuery.AddToQuery("setSaleRoundingToPreviousRupee", MsetSaleRoundingToPreviousRupee);
        //    objQuery.AddToQuery("setSaleEditRateInCounterSale", MsetSaleEditRateInCounterSale);
        //    objQuery.AddToQuery("setSaleAllowNegativeStock", MsetSaleAllowNegativeStock);
        //    objQuery.AddToQuery("setCreditNoteRemoveCode", MsetRemoveCodeFromCreditNote);
         //   objQuery.AddToQuery("setSaleAllowPendingCashMemo", MsetAllowPendingCashMemo);
            objQuery.AddToQuery("setFixedNarration", MsetFixedNarration);
            objQuery.AddToQuery("setCreditNoteDefaultTransferToAccount", MsetCreditNoteDefaultTransferToAccount);
            objQuery.AddToQuery("setCreditNoteReturnRateDisable", MsetCreditNoteReturnRateDisable);
            objQuery.AddToQuery("setSaleAllowBackDate", MsetSaleAllowBackDate);
         //   objQuery.AddToQuery("setSaleCounterSaleSingleGrid", MsetSaleCounterSaleSingleGrid);
         //   objQuery.AddToQuery("setSaleProductRefreshInCounterSale", MsetSaleProductRefreshButton);
            objQuery.AddToQuery("setSaleMaxDiscount", MsetSaleMaxDiscount);
            objQuery.AddToQuery("setOpeningStockGetPercent", MsetOpeningStockGetPercent);
            objQuery.AddToQuery("setCreditNoteDoNotShowPurchaseRate", MsetCreditNoteDoNotShowPurchaseRate);
            objQuery.AddToQuery("setCashBankShowDiscount", MsetCashBankShowDiscount);
            objQuery.AddToQuery("setProductNameWidthInSaleInvoice", MsetProductNameWidthInSaleInvoice);

            objQuery.AddToQuery("SmsPatientSaleSet", SmsSetPatientSale); 
            objQuery.AddToQuery("SmsDebtorSaleSet", SmsSetDebtorSale);
            objQuery.AddToQuery("SmsCreditCardSaleSet", SmsSetCreditCardSale);
            objQuery.AddToQuery("SmsInstitutionalSaleSet", SmsSetInstitutionalSale);
            objQuery.AddToQuery("SmsBankPaymentSet", SmsSetBankPaymentSale);
            objQuery.AddToQuery("SmsBankReceiptSet", SmsSetBankReceiptSale);
            objQuery.AddToQuery("SmsCashReceiptSet", SmsSetCashReceiptSale);
            objQuery.AddToQuery("SmsCashPaymentSet", SmsSetCashPaymentSale);
         //   objQuery.AddToQuery("setPrintSettingYesNo", MsetAllowPrintMessage);
//objQuery.AddToQuery("setGSTSPercent", MsetGSTSPercent);   //end
            return objQuery.UpdateQuery();
        }

        public bool UpdateDetailsPrint(string msetBill, string msetCRDB, string msetCashBank, string msetPO, int msetNumberOfLinesSaleBill , int msetNumberofBillsAtaTime, string msetSortOrder , string msetprintFixNumberOfLines)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryPrint(msetBill, msetCRDB, msetCashBank, msetPO, msetNumberOfLinesSaleBill,msetNumberofBillsAtaTime,msetSortOrder,msetprintFixNumberOfLines);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        public bool UpdateDetailsReports(string msetdailysaledonotshowtotal, string msetReportSaleDailyProductsDoNotShowTotal, string msetPrintFontName, string msetPrintFontSize)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryReports(msetdailysaledonotshowtotal, msetReportSaleDailyProductsDoNotShowTotal, msetPrintFontName, msetPrintFontSize);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }
        private string GetUpdateQueryReports(string msetdailysaledonotshowtotal, string msetReportSaleDailyProductsDoNotShowTotal, string msetPrintFontName, string msetPrintFontSize)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblsettings";           
            objQuery.AddToQuery("ID", General.ShopDetail.ShopVoucherSeries, true);
            objQuery.AddToQuery("setReportSaleDailySaleDoNotShowTotal", msetdailysaledonotshowtotal);
            objQuery.AddToQuery("setReportSaleDailyProductsDoNotShowTotal", msetReportSaleDailyProductsDoNotShowTotal);
            objQuery.AddToQuery("setPrintFontName", msetPrintFontName);
            objQuery.AddToQuery("setPrintFontSize", msetPrintFontSize);
            return objQuery.UpdateQuery();
        }
        private string GetUpdateQueryPrint(string msetBill, string msetCRDB, string msetCashBank, string msetPO, int msetNumberOfLinesSaleBill, int msetNumberofBillsAtaTime, string msetSortOrder, string msetprintFixNumberOfLines)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblsettings";
            objQuery.AddToQuery("ID", General.ShopDetail.ShopVoucherSeries, true);
            objQuery.AddToQuery("setPrintSaleBillPrintedPaper", msetBill);
            objQuery.AddToQuery("setPrintCRDBNotePrintedPaper", msetCRDB);
            objQuery.AddToQuery("setPrintCashBankVoucherPrintedPaper", msetCashBank);
            objQuery.AddToQuery("setPrintPurchaseOrderPrintedPaper", msetPO);
            objQuery.AddToQuery("setNumberOfLinesSaleBill", msetNumberOfLinesSaleBill);
            objQuery.AddToQuery("setNumberOfBillsAtaTime", msetNumberofBillsAtaTime);
            objQuery.AddToQuery("setSortOrder", msetSortOrder);
            objQuery.AddToQuery("setPrintFixNumberOfLines", msetprintFixNumberOfLines);
            return objQuery.UpdateQuery();
        }
        public bool UpdateDetailsEmail(string msetEmailID, string msetEmailPassword, string msetEmailType)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryEmail(msetEmailID, msetEmailPassword, msetEmailType);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetUpdateQueryEmail(string msetEmailID, string msetEmailPassword, string msetEmailType)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblsettings";
            objQuery.AddToQuery("ID", General.ShopDetail.ShopVoucherSeries, true);
            objQuery.AddToQuery("setEmailID", msetEmailID);
            objQuery.AddToQuery("setEmailPassword", msetEmailPassword);
            objQuery.AddToQuery("setEmailType", msetEmailType);           
            return objQuery.UpdateQuery();
        }
    }
}
