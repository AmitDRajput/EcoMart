using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.DataLayer
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

        public bool AddDetails(string MsetPurchaseRounding,string MsetPurchaseIfCreditPurchase,string MsetPurchaseAddVATInPurchaseRate,string MsetPurchaseAddVATInSaleRate,string MsetPurchaseReadPurchaseOrder,
                    string MsetPurchaseIfProductWithOctroi, string MsetPurchaseOctroionZeroVAT, string MsetPurchaseChangeSaleRate, string MsetPurchaseMarginByPurchaseRate,string MsetSaleRoundOff,  string MsetSaleCreditSale, string MsetPurchaseAllowExpriedItems, string MsetSaleAskDiscountinCounterSale, string MsetAskRoundingInSale,
                    string MsetSaleShowProfitInSaleBill, string MsetSaleIPDOPD,string MsetSaleDiscountWithoutVAT, string MsetGeneralProfitPercentageByPurchaseRate,string MsetGeneralExpiryLast,string MsetGeneralBatchNumberRequired, string MsetGeneralExpiryDateRequired, string MsetAskOperatorVoucherSale,
                    string MsetAskOperatorOtherThanVoucherSale, string MsetAskOperatorPurchase, string MsetAskOperatorCRDB, string MsetAskOperatorOpeningStock, string MsetAskOperatorCorrectioninRate, string MsetAskOperatorJV,
                    string MsetAskOperatorCashBankReceipt, string MsetAskOperatorCashBankPayment, string MsetScanBarcode, string MsetPurchaseIncludeCreditPurchase, string MsetSaleIncludeCreditSale, string MsetSaleSaveCustomerInMaster, string MsetSaleShowOnlyMRPInCounterSale, string MsetSaleAllowDistributorSale, string MsetSaleF3KeyForPatientSaleEdit, string MsetSaleOnlyCashSaleInCounterSale, string MsetSaleRoundingTo10Paise)
        {
            bool bRetValue = false;
            string strSql = GetInsertQuery(MsetPurchaseRounding,MsetPurchaseIfCreditPurchase,MsetPurchaseAddVATInPurchaseRate,MsetPurchaseAddVATInSaleRate,MsetPurchaseReadPurchaseOrder,
                    MsetPurchaseIfProductWithOctroi, MsetPurchaseOctroionZeroVAT, MsetPurchaseChangeSaleRate, MsetPurchaseMarginByPurchaseRate, MsetPurchaseAllowExpriedItems,MsetSaleRoundOff, MsetSaleCreditSale, MsetSaleAskDiscountinCounterSale, MsetAskRoundingInSale,
                    MsetSaleShowProfitInSaleBill, MsetSaleIPDOPD,MsetSaleDiscountWithoutVAT,MsetGeneralProfitPercentageByPurchaseRate,MsetGeneralExpiryLast,MsetGeneralBatchNumberRequired,MsetGeneralExpiryDateRequired, MsetAskOperatorVoucherSale,
                    MsetAskOperatorOtherThanVoucherSale, MsetAskOperatorPurchase, MsetAskOperatorCRDB, MsetAskOperatorOpeningStock, MsetAskOperatorCorrectioninRate, MsetAskOperatorJV,
                    MsetAskOperatorCashBankReceipt, MsetAskOperatorCashBankPayment,MsetScanBarcode,MsetPurchaseIncludeCreditPurchase,MsetSaleIncludeCreditSale,MsetSaleSaveCustomerInMaster,MsetSaleShowOnlyMRPInCounterSale,MsetSaleAllowDistributorSale,MsetSaleF3KeyForPatientSaleEdit,MsetSaleOnlyCashSaleInCounterSale,MsetSaleRoundingTo10Paise);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        public bool UpdateDetails(string MsetPurchaseRounding, string MsetPurchaseIfCreditPurchase, string MsetPurchaseAddVATInPurchaseRate, string MsetPurchaseAddVATInSaleRate, string MsetPurchaseReadPurchaseOrder,
                    string MsetPurchaseIfProductWithOctroi, string MsetPurchaseOctroionZeroVAT, string MsetPurchaseChangeSaleRate, string MsetPurchaseMarginByPurchaseRate, string MsetPurchaseAllowExpriedItems, string MsetSaleRoundOff, string MsetSaleCreditSale, string MsetSaleAskDiscountinCounterSale, string MsetAskRoundingInSale,
                    string MsetSaleShowProfitInSaleBill, string MsetSaleIPDOPD, string MsetSaleDiscountWithoutVAT, string MsetGeneralProfitPercentageByPurchaseRate, string MsetGeneralExpiryLast, string MsetGeneralBatchNumberRequired, string MsetGeneralExpiryDateRequired, string MsetAskOperatorVoucherSale,
                    string MsetAskOperatorOtherThanVoucherSale, string MsetAskOperatorPurchase, string MsetAskOperatorCRDB, string MsetAskOperatorOpeningStock, string MsetAskOperatorCorrectioninRate, string MsetAskOperatorJV,
                    string MsetAskOperatorCashBankReceipt, string MsetAskOperatorCashBankPayment, string MsetScanBarCode, string MsetPurchaseIncludeCreditPurchase, string MsetSaleIncludeCreditSale, string MsetSaleSaveCustomerInMaster, string MsetSaleShowOnlyMRPInCounterSale, string MsetSaleAllowDistributorSale, string MsetSaleF3KeyForPatientSaleEdit, string MsetSaleOnlyCashSaleInCounterSale, string MsetSaleRoundingTo10Paise)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQuery(MsetPurchaseRounding, MsetPurchaseIfCreditPurchase, MsetPurchaseAddVATInPurchaseRate, MsetPurchaseAddVATInSaleRate, MsetPurchaseReadPurchaseOrder,
                    MsetPurchaseIfProductWithOctroi, MsetPurchaseOctroionZeroVAT, MsetPurchaseChangeSaleRate, MsetPurchaseMarginByPurchaseRate, MsetPurchaseAllowExpriedItems,MsetSaleRoundOff, MsetSaleCreditSale, MsetSaleAskDiscountinCounterSale, MsetAskRoundingInSale,
                    MsetSaleShowProfitInSaleBill, MsetSaleIPDOPD,MsetSaleDiscountWithoutVAT, MsetGeneralProfitPercentageByPurchaseRate, MsetGeneralExpiryLast, MsetGeneralBatchNumberRequired,MsetGeneralExpiryDateRequired, MsetAskOperatorVoucherSale,
                    MsetAskOperatorOtherThanVoucherSale, MsetAskOperatorPurchase, MsetAskOperatorCRDB, MsetAskOperatorOpeningStock, MsetAskOperatorCorrectioninRate, MsetAskOperatorJV,
                    MsetAskOperatorCashBankReceipt, MsetAskOperatorCashBankPayment, MsetScanBarCode, MsetPurchaseIncludeCreditPurchase, MsetSaleIncludeCreditSale, MsetSaleSaveCustomerInMaster, MsetSaleShowOnlyMRPInCounterSale,MsetSaleAllowDistributorSale,MsetSaleF3KeyForPatientSaleEdit,MsetSaleOnlyCashSaleInCounterSale,MsetSaleRoundingTo10Paise);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }


        private string GetInsertQuery(string MsetPurchaseRounding, string MsetPurchaseIfCreditPurchase, string MsetPurchaseAddVATInPurchaseRate, string MsetPurchaseAddVATInSaleRate, string MsetPurchaseReadPurchaseOrder,
                    string MsetPurchaseIfProductWithOctroi, string MsetPurchaseOctroionZeroVAT, string MsetPurchaseChangeSaleRate, string MsetPurchaseMarginByPurchaseRate, string MsetPurchaseAllowExpriedItems,string MsetSaleRoundOff, string MsetSaleCreditSale, string MsetSaleAskDiscountinCounterSale, string MsetAskRoundingInSale,
                    string MsetSaleShowProfitInSaleBill, string MsetSaleIPDOPD, string MsetSaleDiscountWithoutVAT, string MsetGeneralProfitPercentageByPurchaseRate, string MsetGeneralExpiryLast, string MsetGeneralBatchNumberRequired, string MsetGeneralExpiryDateRequired, string MsetAskOperatorVoucherSale,
                    string MsetAskOperatorOtherThanVoucherSale, string MsetAskOperatorPurchase, string MsetAskOperatorCRDB, string MsetAskOperatorOpeningStock, string MsetAskOperatorCorrectioninRate, string MsetAskOperatorJV,
                    string MsetAskOperatorCashBankReceipt, string MsetAskOperatorCashBankPayment, string MsetScanBarCode, string MsetPurchaseIncludeCreditPurchase, string MsetSaleIncludeCreditSale, string MsetSaleSaveCustomerInMaster, string MsetSaleShowOnlyMRPInCounterSale , string MsetSaleAllowDistributorSale, string MsetSaleF3KeyForPatientSaleEdit, string MsetSaleOnlyCashSaleInCounterSale, string MsetSaleRoundingTo10Paise)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblsettings";
            objQuery.AddToQuery("ID", General.ShopDetail.ShopVoucherSeries);
            objQuery.AddToQuery("setPurchaseRounding", MsetPurchaseRounding);
            objQuery.AddToQuery("setPurchaseIfCreditPurchase", MsetPurchaseIfCreditPurchase);        
            objQuery.AddToQuery("setPurchaseAddVATInPurchaseRate", "N");
            objQuery.AddToQuery("setPurchaseAddVATInSaleRate", "N");
            objQuery.AddToQuery("setPurchaseReadPurchaseOrder", MsetPurchaseReadPurchaseOrder);
            objQuery.AddToQuery("setPurchaseIfProductWithOctroi", MsetPurchaseIfProductWithOctroi);
            objQuery.AddToQuery("setPurchaseOctroionZeroVAT", MsetPurchaseOctroionZeroVAT);
            objQuery.AddToQuery("setPurchaseChangeSaleRate", MsetPurchaseChangeSaleRate);
            objQuery.AddToQuery("setPurchaseAllowExpiredItems", MsetPurchaseAllowExpriedItems);
            objQuery.AddToQuery("setPurchaseMarginbyPurchaseRate", MsetPurchaseMarginByPurchaseRate);
            objQuery.AddToQuery("setSaleRoundOff",MsetSaleRoundOff);
            objQuery.AddToQuery("setSaleCreditStatement", MsetSaleCreditSale);
            objQuery.AddToQuery("setSaleAskDiscountinCounterSale", MsetSaleAskDiscountinCounterSale);
            objQuery.AddToQuery("setSaleAskRoundinginSale", MsetAskRoundingInSale);
            objQuery.AddToQuery("setSaleShowProfitInSaleBill", MsetSaleShowProfitInSaleBill);
            objQuery.AddToQuery("setSaleIPDOPD", MsetSaleIPDOPD);
            objQuery.AddToQuery("setSaleDiscountWithoutVAT", MsetSaleDiscountWithoutVAT);
            objQuery.AddToQuery("setGeneralProfitPercentageByPurchaseRate", MsetGeneralProfitPercentageByPurchaseRate);
            objQuery.AddToQuery("setGeneralExpiryLast", MsetGeneralExpiryLast);
            objQuery.AddToQuery("setGeneralBatchNumberRequired", MsetGeneralBatchNumberRequired);
            objQuery.AddToQuery("setGeneralExpiryDateRequired", MsetGeneralExpiryDateRequired);
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
            objQuery.AddToQuery("setPurchaseIncludeCreditPurchaseInStatements",MsetPurchaseIncludeCreditPurchase);
            objQuery.AddToQuery("setSaleIncludeCreditsaleInStatements",MsetSaleIncludeCreditSale);
            objQuery.AddToQuery("setSaleSaveCustomerInMaster", MsetSaleSaveCustomerInMaster);
            objQuery.AddToQuery("setSaleShowOnlyMRPInCounterSale",MsetSaleShowOnlyMRPInCounterSale);
            objQuery.AddToQuery("setSaleAllowDistributorSale", MsetSaleAllowDistributorSale);
            objQuery.AddToQuery("setSaleF3KeyForPatientSaleEdit", MsetSaleF3KeyForPatientSaleEdit);
            objQuery.AddToQuery("setSaleOnlyCashSaleInCounterSale", MsetSaleOnlyCashSaleInCounterSale);
            objQuery.AddToQuery("setSaleRoundingTo10Paise", MsetSaleRoundingTo10Paise);
            return objQuery.InsertQuery();
        }

        private string GetUpdateQuery(string MsetPurchaseRounding, string MsetPurchaseIfCreditPurchase, string MsetPurchaseAddVATInPurchaseRate, string MsetPurchaseAddVATInSaleRate, string MsetPurchaseReadPurchaseOrder,
                    string MsetPurchaseIfProductWithOctroi, string MsetPurchaseOctroionZeroVAT, string MsetPurchaseChangeSaleRate, string MsetPurchaseAllowExpriedItems, string MsetPurchaseMarginByPurchaseRate, string MsetSaleRoundOff, string MsetSaleCreditSale, string MsetSaleAskDiscountinCounterSale, string MsetAskRoundingInSale,
                    string MsetSaleShowProfitInSaleBill, string MsetSaleIPDOPD, string MsetSaleDiscountWithoutVAT, string MsetGeneralProfitPercentageByPurchaseRate, string MsetGeneralExpiryLast, string MsetGeneralBatchNumberRequired, string MsetGeneralExpiryDateRequired, string MsetAskOperatorVoucherSale,
                    string MsetAskOperatorOtherThanVoucherSale, string MsetAskOperatorPurchase, string MsetAskOperatorCRDB, string MsetAskOperatorOpeningStock, string MsetAskOperatorCorrectioninRate, string MsetAskOperatorJV,
                    string MsetAskOperatorCashBankReceipt, string MsetAskOperatorCashBankPayment, string MsetScanBarCode, string MsetPurchaseIncludeCreditPurchase, string MsetSaleIncludeCreditSale, string MsetSaleSaveCustomerInMaster, string MsetSaleShowOnlyMRPInCounterSale, string MsetSaleAllowDistributorSale, string MsetSaleF3KeyForPatientSaleEdit, string MsetSaleOnlyCashSaleInCounterSale, string MsetSaleRoundingTo10Paise)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblsettings";
            objQuery.AddToQuery("ID", General.ShopDetail.ShopVoucherSeries, true);
            objQuery.AddToQuery("setPurchaseRounding", MsetPurchaseRounding);
            objQuery.AddToQuery("setPurchaseIfCreditPurchase", MsetPurchaseIfCreditPurchase);
            objQuery.AddToQuery("setPurchaseAddVATInPurchaseRate", "N");
            objQuery.AddToQuery("setPurchaseAddVATInSaleRate", "N");
            objQuery.AddToQuery("setPurchaseReadPurchaseOrder", MsetPurchaseReadPurchaseOrder);
            objQuery.AddToQuery("setPurchaseIfProductWithOctroi", MsetPurchaseIfProductWithOctroi);
            objQuery.AddToQuery("setPurchaseOctroionZeroVAT", MsetPurchaseOctroionZeroVAT);
            objQuery.AddToQuery("setPurchaseChangeSaleRate", MsetPurchaseChangeSaleRate);
            objQuery.AddToQuery("setPurchaseAllowExpiredItems", MsetPurchaseAllowExpriedItems);
            objQuery.AddToQuery("setPurchaseMarginbyPurchaseRate", MsetPurchaseMarginByPurchaseRate);
            objQuery.AddToQuery("setSaleRoundOff", MsetSaleRoundOff);
            objQuery.AddToQuery("setSaleCreditStatement", MsetSaleCreditSale);
            objQuery.AddToQuery("setSaleAskDiscountinCounterSale", MsetSaleAskDiscountinCounterSale);
            objQuery.AddToQuery("setSaleAskRoundinginSale", MsetAskRoundingInSale);
            objQuery.AddToQuery("setSaleShowProfitInSaleBill", MsetSaleShowProfitInSaleBill);
            objQuery.AddToQuery("setSaleIPDOPD", MsetSaleIPDOPD);
            objQuery.AddToQuery("setSaleDiscountWithoutVAT", MsetSaleDiscountWithoutVAT);
            objQuery.AddToQuery("setGeneralProfitPercentageByPurchaseRate", MsetGeneralProfitPercentageByPurchaseRate);
            objQuery.AddToQuery("setGeneralExpiryLast", MsetGeneralExpiryLast);
            objQuery.AddToQuery("setGeneralBatchNumberRequired", MsetGeneralBatchNumberRequired);
            objQuery.AddToQuery("setGeneralExpiryDateRequired", MsetGeneralExpiryDateRequired);
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
            objQuery.AddToQuery("setSaleIncludeCreditsaleInStatements",MsetSaleIncludeCreditSale);
            objQuery.AddToQuery("setSaleSaveCustomerInMaster", MsetSaleSaveCustomerInMaster);
            objQuery.AddToQuery("setSaleShowOnlyMRPInCounterSale", MsetSaleShowOnlyMRPInCounterSale);
            objQuery.AddToQuery("setSaleAllowDistributorSale", MsetSaleAllowDistributorSale);
            objQuery.AddToQuery("setSaleF3KeyForPatientSaleEdit", MsetSaleF3KeyForPatientSaleEdit);
            objQuery.AddToQuery("setSaleOnlyCashSaleInCounterSale", MsetSaleOnlyCashSaleInCounterSale);
            objQuery.AddToQuery("setSaleRoundingTo10Paise", MsetSaleRoundingTo10Paise);
            return objQuery.UpdateQuery();
        }
       

        public bool UpdateDetailsPrint(string msetBill, string msetCRDB, string msetCashBank, string msetPO, int msetNumberOfLinesSaleBill)
        {
            bool bRetValue = false;
            string strSql = GetUpdateQueryPrint(msetBill, msetCRDB, msetCashBank, msetPO, msetNumberOfLinesSaleBill);

            if (DBInterface.ExecuteQuery(strSql) > 0)
            {
                bRetValue = true;
            }
            return bRetValue;
        }

        private string GetUpdateQueryPrint(string msetBill, string msetCRDB, string msetCashBank, string msetPO, int msetNumberOfLinesSaleBill)
        {
            Query objQuery = new Query();
            objQuery.Table = "tblsettings";           
            objQuery.AddToQuery("ID", General.ShopDetail.ShopVoucherSeries, true);
            objQuery.AddToQuery("setPrintSaleBillPrintedPaper", msetBill);
            objQuery.AddToQuery("setPrintCRDBNotePrintedPaper", msetCRDB);
            objQuery.AddToQuery("setPrintCashBankVoucherPrintedPaper", msetCashBank);
            objQuery.AddToQuery("setPrintPurchaseOrderPrintedPaper", msetPO);
            objQuery.AddToQuery("setNumberOfLinesSaleBill", msetNumberOfLinesSaleBill);
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
