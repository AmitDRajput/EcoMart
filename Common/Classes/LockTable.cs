using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.DataLayer;

namespace EcoMart.Common
{   
    
    public class LockTable
    {
        public static void LocktblVoucherNo()
        {
            //string strSql = "LOCK TABLES  tblvouchernumbers Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }

        public static void LockTablesForSale()
        {
            //string strSql = "LOCK TABLES tblStock Low_priority WRITE , tblvouchernumbers Low_priority WRITE, vouchersale Low_Priority WRITE , detailsale Low_Priority WRITE,  tbltrnac Low_Priority WRITE ,vouchercreditdebitnote LOW_PRIORITY WRITE, mastershelf READ, mastercompany READ , detailpurchaseorderstockist LOW_PRIORITY WRITE,masterproduct Low_Priority WRITE,  masteraccount Low_priority WRITE, tblscanprescriptions Low_priority WRITE,masterdoctor Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);                           
        }
        public static void LockTablesForSpecialSale()
        {
            //string strSql = "LOCK TABLES specialvouchersale Low_Priority WRITE , specialdetailsale Low_Priority WRITE,vouchercreditdebitnote LOW_PRIORITY WRITE, mastershelf READ, mastercompany READ ,masterproduct Low_Priority WRITE, masteraccount Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForStatementSale()
        {
            //string strSql = "LOCK TABLES tblvouchernumbers Low_priority WRITE, vouchersale Low_Priority WRITE , voucherstatement Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForPurchase()
        {
            //string strSql = "LOCK TABLES tblStock Low_Priority WRITE , tblvouchernumbers Low_priority WRITE, voucherpurchase Low_Priority WRITE , detailpurchase Low_Priority WRITE,tbltrnac Low_priority WRITE,vouchercreditdebitnote LOW_PRIORITY WRITE,masterproduct Low_priority WRITE,detailpurchaseorderstockist Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForStatementPurchase()
        {
            //string strSql = "LOCK TABLES tblvouchernumbers Low_priority WRITE, voucherpurchase Low_Priority WRITE , tbltrnac Low_priority WRITE, voucherstatement Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForOpeningStock()
        {
            //string strSql = "LOCK TABLES tblStock Low_Priority WRITE , tblvouchernumbers Low_priority WRITE, voucheropstock Low_Priority WRITE , detailopstock Low_Priority WRITE,masterproduct Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }

        public static void LockTablesForCashBankPayment()
        {
            //string strSql = "LOCK TABLES tblvouchernumbers Low_priority WRITE, vouchercashbankpayment Low_Priority WRITE , detailcashbankpayment Low_Priority WRITE,voucherpurchase Low_priority WRITE,tbltrnac Low_priority WRITE,voucherstatement Low_priority WRITE,masteraccount Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForCashBankExpenses()
        {
            //string strSql = "LOCK TABLES tblvouchernumbers Low_priority WRITE, vouchercashbankexpenses Low_Priority WRITE , detailcashbankexpenses Low_Priority WRITE,tbltrnac Low_priority WRITE,voucherJV Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForCashBankReceipts()
        {
            //string strSql = "LOCK TABLES tblvouchernumbers Low_priority WRITE, vouchercashbankreceipt Low_Priority WRITE , detailcashbankreceipt Low_Priority WRITE,vouchersale Low_priority WRITE,tbltrnac Low_priority WRITE,voucherstatement Low_priority WRITE,masteraccount Low_priority WRITE,voucherjv Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForChequeReturn()
        {
            //string strSql = "LOCK TABLES tblvouchernumbers Low_priority WRITE,voucherchequereturn Low_priority WRITE ,vouchercashbankreceipt Low_Priority WRITE , detailchequereturn Low_Priority WRITE,vouchersale Low_priority WRITE,tbltrnac Low_priority WRITE,voucherstatement Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForCreditDebitNoteStock()
        {
            //string strSql = "LOCK TABLES tblStock Low_Priority WRITE,tblvouchernumbers Low_priority WRITE, vouchercreditdebitnote Low_Priority WRITE , detailcreditdebitnotestock Low_Priority WRITE,tbltrnac Low_priority WRITE,masterproduct Low_priority WRITE,mastershelf Low_priority WRITE,mastercompany Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForCreditDebitNoteAmount()
        {
            //string strSql = "LOCK TABLES tblvouchernumbers Low_priority WRITE, vouchercreditdebitnote Low_Priority WRITE , detailcreditdebitnoteamount Low_Priority WRITE,tbltrnac Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForCorrectioninRate()
        {
            //string strSql = "LOCK TABLES tblStock Low_Priority WRITE,tblvouchernumbers Low_priority WRITE, vouchercorrectioninrate Low_Priority WRITE ";
            //DBInterface.ExecuteQuery(strSql);
        }

        public static void UnLockTables()
        {
            //string strSql = "UNLOCK TABLES";
            //DBInterface.ExecuteQuery(strSql);           
        }
        public static void LockTableForDoctor()
        {
            //string strSql = "LOCK TABLES masterdoctor Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTableForPhoneBook()
        {
            //string strSql = "LOCK TABLES tblPhoneBook Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTableForAccount()
        {
            //string strSql = "LOCK TABLES masteraccount Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }

        public static void LockTableProductForCache()
        {
            //string strSql = "LOCK TABLES masterproduct low_priority Write,mastershelf read, mastercompany read";
            //DBInterface.ExecuteQuery(strSql);
        }

        public static void LockTableForLinkDebtorProduct()
        {
            //string strSql = "LOCK TABLES linkdebtorproduct Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }

        public static void LockTableForLinkDrugGrouping()
        {
            //string strSql = "LOCK TABLES linkdruggrouping Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }

        public static void LockTableForLinkPartyCompany()
        {
            //string strSql = "LOCK TABLES linkpartycompany Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }

        public static void LockTableForLinkShelfProduct()
        {
            //string strSql = "LOCK TABLES masterproduct Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }

        public static void LockTableForSettings()
        {
            //string strSql = "LOCK TABLES tblsettings Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForStockReProcess()
        {
            //string strSql = "LOCK TABLES tblStock Low_Priority WRITE , masterproduct Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }

        public static void LockTablesForCompanyShortName()
        {
            //string strSql = "LOCK TABLES mastercompany Low_Priority WRITE , masterproduct Low_Priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForChallanPurchase()
        {
            //string strSql = "LOCK TABLES tblStock Low_Priority WRITE , tblvouchernumbers Low_priority WRITE, voucherChallanpurchase Low_Priority WRITE , detailpurchase Low_Priority WRITE,masterproduct Low_priority WRITE,detailpurchaseorderstockist Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
        public static void LockTablesForPurchaseOrder()
        {
            //string strSql = "LOCK TABLES masterpurchaseorderstockist Low_Priority WRITE , detailpurchaseorderstockist Low_priority WRITE";
            //DBInterface.ExecuteQuery(strSql);
        }
    }
}
