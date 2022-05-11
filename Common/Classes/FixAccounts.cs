using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using EcoMart.Common;
using EcoMart.DataLayer;

namespace EcoMart.Common
{
    public class FixAccounts
    {
        #region Declaration
        private static int _AccountStockInOut;
        private static int _AccountCash;
        private static int _AccountCashSale; 
        private static int _AccountCreditStatementSale;
        private static int _AccountCreditSale;
        private static int _AccountCreditCardSale;
        private static int _AccountVoucherSale;
        private static int _AccountPendingCashBills;
        private static int _AccountSalesReturn;
        private static int _AccountCashDiscountSale;
        private static int _AccountPMTDiscountSale;
        private static int _AccountItemDiscountSale;
        private static int _AccountDebitNoteSale;
        private static int _AccountCreditNoteSale;
        private static int _AccountAddonSale;
        private static int _AccountVatOutput5Sale;
        private static int _AccountVatOutput12point5Sale;
        private static int _AccountRoundoffSale;
        private static int _AccountVATZeroSale;
        private static int _AccountStatementPurchase;
        private static int _AccountStatementSale;
        private static int _AccountDiscountInCashBankEntry;
        private static int _AccountVAT5point5Sale;
        private static int _AccountVAT12point5Sale;

        private static int _AccountCounterSaleCreditNote;

        private static int _AccountCashPurchase;
        private static int _AccountCreditPurchase;
        private static int _AccountCashCreditPurchase;
        private static int _AccountPurchaseReturn;
        private static int _AccountCashDiscountPurchase;
        private static int _AccountSplDiscountPurchase;
        private static int _AccountSchemeDiscountPurchcase;
        private static int _AccountItemDiscountPurchase;
        private static int _AccountDebitNotePurchase;
        private static int _AccountCreditNotePurchase;
        private static int _AccountAddonHamaliPurchase;
        private static int _AccountLessPurchase;
        private static int _AccountVatInput5Purchase;
        private static int _AccountVatInput12point5Purchase;
        private static int _AccountRoundoffPurchase;
        private static int _AccountVATZeroPurchase;
        private static int _AccountVAT5Purchase;
        private static int _AccountVAT12point5Purchase;

        private static int _AccountAddonPurchase;
        private static int _AccountFreightPurchase;

        private static string _DashLine80Normal;
        private static string _DashLine80Condenced;
        private static string _DashLine60Normal;

        private static string _VoucherTypeForCashSale;
        private static string _VoucherTypeForCreditSale;
        private static string _VoucherTypeForCreditStatementSale;
        private static string _VoucherTypeForVoucherSale;
        private static string _VoucherTypeForBillReprint;
        private static string _VoucherTypeForChallanSale;

        private static string _VoucherTypeForCashPurchase;
        private static string _VoucherTypeForCreditPurchase;
        private static string _VoucherTypeForCreditStatementPurchase;
        private static string _VoucherTypeForChallanPurchase;

        private static string _VoucherTypeForOpeningStock;
        private static string _VoucherTypeForPurchaseOrder;
        private static string _VoucherTypeForCorrectionInRate;
        private static string _VoucherTypeForJournalEntry;
        private static string _VoucherTypeForChequeReturn;

        private static string _VoucherTypeForStatementPurchase;
        private static string _VoucherTypeForStatementSale;
        private static string _VoucherTypeForStatementHospital;

        private static string _VoucherTypeForCreditNoteStock;
        private static string _VoucherTypeForCreditNoteAmount;
        private static string _VoucherTypeForDebitNoteStock;
        private static string _VoucherTypeForDebitNoteAmount;
        private static string _VoucherTypeForStockIN;
        private static string _VoucherTypeForStockOut;
        private static string _VoucherTypeForSalesReturn;
        private static string _VoucherTypeForPurchaseReturn;
        private static string _VoucherTypeForTransferToAccount;  //Kiran 20/04/2017

        private static string _VoucherTypeForCashReceipt;
        private static string _VoucherTypeForCashPayment;
        private static string _VoucherTypeForBankReceipt;
        private static string _VoucherTypeForBankPayment;
        private static string _VoucherTypeForCashExpenses;
        private static string _VoucherTypeForBankExpenses;
        private static string _VoucherTypeForContraEntry;

        private static string _VoucherTypeForOpeningBalance;

        //private static string _VoucherTypeForDistributorSaleCash;   // 26
        //private static string _VoucherTypeForDistributorSaleCredit;  // 27
        //private static string _VoucherTypeForDistributorSaleCreditStatement; // 28
        //private static string _VoucherTypeForDistributorCreditNoteStock;
        private static string _VoucherTypeForJournalVoucher;    // [ansuman]

        private static int _GroupCodeForDebtor;
        private static int _GroupCodeForCreditor;
        private static int _GroupCodeForBank;
        private static int _GroupCodeForOtherCreditor;
        private static int _GroupCodeForOtherDebtor;
        private static int _GroupCodeClosingStock;
        private static int _GroupCodeForGrossProfitCD;
        private static int _GroupCodeForLOSSCD;
        private static int _GroupCodeForGrossProfitBD;
        private static int _GroupCodeForLOSSBD;
        private static int _GroupCodeForNETProfit;
        private static int _GroupCodeForNETLOSS;


        private static string _AccTypeForDebtor;
        private static string _AccTypeForCreditor;
        private static string _AccTypeForBank;
        private static string _AccTypeForGeneral;
        private static string _AccTypeForSale;
        private static string _AccTypeForPurchase;
        private static string _AccTypeForOtherCreditor;
        private static string _AccTypeForOtherDebtor;
        private static string _AccTypeForPatient;


        private static string _AccCodeForDebtor;
        private static string _AccCodeForCreditor;
        private static string _AccCodeForBank;
        private static string _AccCodeForGeneral;
        private static string _AccCodeForSale;
        private static string _AccCodeForPurchase;
        private static string _AccCodeForOtherCreditor;
        private static string _AccCodeForOtherDebtor;
        private static string _AccCodeForPatient;
        private static string _SubTypeForRegularSale;
        private static string _SubTypeForSpecialSale;
        private static string _SubTypeForPTSSale;
        private static string _SubTypeForRegularSale2;     
        private static string _SubTypeForExpiry;
        private static string _SubTypeForBreakage;
        private static string _SubTypeForStock;
        private static string _SubTypeForGoodsReturn;
        private static string _SubTypeForVoucherSale;
        private static string _SubTypeForSaleWithoutStock;
     // private static string _SubTypeForSaleWithProductDiscount;

        private static string _TransactionTypeForCash;
        private static string _TransactionTypeForCredit;
        private static string _TransactionTypeForCreditStatement;
        private static string _TransactionTypeForCreditCard;
        private static string _TransactionTypeForVoucher;

        private static string _PrinterTypeDotMatrix;
        private static string _PrinterTypeLaserJet;

        private static string _SortByShelf;
        private static string _SortByNone;
        // Printing
        //  private static double _NumberOfRowsPerSaleBill;
        private static double _NumberOfRowsPerInstitutionalSaleBill;
        private static double _NumberOfRowsPerReport;
        private static int _ReportPageHeight;
        private static int _ReportPageWidth;

        private static int _AccountVAT6Sale;
        private static int _AccountVAT13point5Sale;
        private static int _AccountVatOutput6Sale;
        private static int _AccountVatOutput13point5Sale;

         private static int _AccountVatInput6Purchase;
         private static int _AccountVatInput13point5Purchase;        
         private static int _AccountVAT6Purchase;
         private static int _AccountVAT13point5Purchase;

        private static int _AccountAmountGST0Sale;
        private static int _AccountAmountSGST5Sale;
        private static int _AccountAmountCGST5Sale;
        private static int _AccountAmountIGST5Sale;
        private static int _AccountAmountSGST12Sale;
        private static int _AccountAmountCGST12Sale;
        private static int _AccountAmountIGST12Sale;
        private static int _AccountAmountSGST18Sale;
        private static int _AccountAmountCGST18Sale;
        private static int _AccountAmountIGST18Sale;
        private static int _AccountAmountSGST28Sale;
        private static int _AccountAmountCGST28Sale;
        private static int _AccountAmountIGST28Sale;

        private static int _AccountSGST5Sale;
        private static int _AccountCGST5Sale;
        private static int _AccountIGST5Sale;
        private static int _AccountSGST12Sale;
        private static int _AccountCGST12Sale;
        private static int _AccountIGST12Sale;
        private static int _AccountSGST18Sale;
        private static int _AccountCGST18Sale;
        private static int _AccountIGST18Sale;
        private static int _AccountSGST28Sale;
        private static int _AccountCGST28Sale;
        private static int _AccountIGST28Sale;


        private static int _AccountAmountGST0Purchase;
        private static int _AccountAmountSGST5Purchase;
        private static int _AccountAmountCGST5Purchase;
        private static int _AccountAmountIGST5Purchase;
        private static int _AccountAmountSGST12Purchase;
        private static int _AccountAmountCGST12Purchase;
        private static int _AccountAmountIGST12Purchase;
        private static int _AccountAmountSGST18Purchase;
        private static int _AccountAmountCGST18Purchase;
        private static int _AccountAmountIGST18Purchase;
        private static int _AccountAmountSGST28Purchase;
        private static int _AccountAmountCGST28Purchase;
        private static int _AccountAmountIGST28Purchase;

        private static int _AccountSGST5Purchase;
        private static int _AccountCGST5Purchase;
        private static int _AccountIGST5Purchase;
        private static int _AccountSGST12Purchase;
        private static int _AccountCGST12Purchase;
        private static int _AccountIGST12Purchase;
        private static int _AccountSGST18Purchase;
        private static int _AccountCGST18Purchase;
        private static int _AccountIGST18Purchase;
        private static int _AccountSGST28Purchase;
        private static int _AccountCGST28Purchase;
        private static int _AccountIGST28Purchase;
        private static int _AccountGSTPurchaseUnderScheme;

        #endregion Declaration

        #region Constructor
        static FixAccounts()
        {
            Initialize();
        }
        #endregion Constructor

        #region Properties
        public static int AccountAmountGST0Sale
        {
            get { return _AccountAmountGST0Sale; }
            set { _AccountAmountGST0Sale = value; }
        }
        public static int AccountAmountSGST5Sale
        {
            get { return _AccountAmountSGST5Sale; }
            set { _AccountAmountSGST5Sale = value; }
        }
        public static int AccountAmountCGST5Sale
        {
            get { return _AccountAmountCGST5Sale; }
            set { _AccountAmountCGST5Sale = value; }
        }
        public static int AccountAmountIGST5Sale
        {
            get { return _AccountAmountIGST5Sale; }
            set { _AccountAmountIGST5Sale = value; }
        }
        public static int AccountAmountSGST12Sale
        {
            get { return _AccountAmountSGST12Sale; }
            set { _AccountAmountSGST12Sale = value; }
        }
        public static int AccountAmountCGST12Sale
        {
            get { return _AccountAmountCGST12Sale; }
            set { _AccountAmountCGST12Sale = value; }
        }
        public static int AccountAmountIGST12Sale
        {
            get { return _AccountAmountIGST12Sale; }
            set { _AccountAmountIGST12Sale = value; }
        }
        public static int AccountAmountSGST18Sale
        {
            get { return _AccountAmountSGST18Sale; }
            set { _AccountAmountSGST18Sale = value; }
        }
        public static int AccountAmountCGST18Sale
        {
            get { return _AccountAmountCGST18Sale; }
            set { _AccountAmountCGST18Sale = value; }
        }
        public static int AccountAmountIGST18Sale
        {
            get { return _AccountAmountIGST18Sale; }
            set { _AccountAmountIGST18Sale = value; }
        }
        public static int AccountAmountSGST28Sale
        {
            get { return _AccountAmountSGST28Sale; }
            set { _AccountAmountSGST18Sale = value; }
        }
        public static int AccountAmountCGST28Sale
        {
            get { return _AccountAmountCGST28Sale; }
            set { _AccountAmountCGST28Sale = value; }
        }
        public static int AccountAmountIGST28Sale
        {
            get { return _AccountAmountIGST28Sale; }
            set { _AccountAmountIGST28Sale = value; }
        }

        public static int AccountSGST5Sale
        {
            get { return _AccountSGST5Sale; }
            set { _AccountSGST5Sale = value; }
        }
        public static int AccountCGST5Sale
        {
            get { return _AccountCGST5Sale; }
            set { _AccountCGST5Sale = value; }
        }
        public static int AccountIGST5Sale
        {
            get { return _AccountIGST5Sale; }
            set { _AccountIGST5Sale = value; }
        }
        public static int AccountSGST12Sale
        {
            get { return _AccountSGST12Sale; }
            set { _AccountSGST12Sale = value; }
        }
        public static int AccountCGST12Sale
        {
            get { return _AccountCGST12Sale; }
            set { _AccountCGST12Sale = value; }
        }
        public static int AccountIGST12Sale
        {
            get { return _AccountIGST12Sale; }
            set { _AccountIGST12Sale = value; }
        }
        public static int AccountSGST18Sale
        {
            get { return _AccountSGST18Sale; }
            set { _AccountSGST18Sale = value; }
        }
        public static int AccountCGST18Sale
        {
            get { return _AccountCGST18Sale; }
            set { _AccountCGST18Sale = value; }
        }
        public static int AccountIGST18Sale
        {
            get { return _AccountIGST18Sale; }
            set { _AccountIGST18Sale = value; }
        }
        public static int AccountSGST28Sale
        {
            get { return _AccountSGST28Sale; }
            set { _AccountSGST18Sale = value; }
        }
        public static int AccountCGST28Sale
        {
            get { return _AccountCGST28Sale; }
            set { _AccountCGST28Sale = value; }
        }
        public static int AccountIGST28Sale
        {
            get { return _AccountIGST28Sale; }
            set { _AccountIGST28Sale = value; }
        }


        public static int AccountAmountGST0Purchase
        {
            get { return _AccountAmountGST0Purchase; }
            set { _AccountAmountGST0Purchase = value; }
        }
        public static int AccountAmountSGST5Purchase
        {
            get { return _AccountAmountSGST5Purchase; }
            set { _AccountAmountSGST5Purchase = value; }
        }
        public static int AccountAmountCGST5Purchase
        {
            get { return _AccountAmountCGST5Purchase; }
            set { _AccountAmountCGST5Purchase = value; }
        }
        public static int AccountAmountIGST5Purchase
        {
            get { return _AccountAmountIGST5Purchase; }
            set { _AccountAmountIGST5Purchase = value; }
        }
        public static int AccountAmountSGST12Purchase
        {
            get { return _AccountAmountSGST12Purchase; }
            set { _AccountAmountSGST12Purchase = value; }
        }
        public static int AccountAmountCGST12Purchase
        {
            get { return _AccountAmountCGST12Purchase; }
            set { _AccountAmountCGST12Purchase = value; }
        }
        public static int AccountAmountIGST12Purchase
        {
            get { return _AccountAmountIGST12Purchase; }
            set { _AccountAmountIGST12Purchase = value; }
        }
        public static int AccountAmountSGST18Purchase
        {
            get { return _AccountAmountSGST18Purchase; }
            set { _AccountAmountSGST18Purchase = value; }
        }
        public static int AccountAmountCGST18Purchase
        {
            get { return _AccountAmountCGST18Purchase; }
            set { _AccountAmountCGST18Purchase = value; }
        }
        public static int AccountAmountIGST18Purchase
        {
            get { return _AccountAmountIGST18Purchase; }
            set { _AccountAmountIGST18Purchase = value; }
        }
        public static int AccountAmountSGST28Purchase
        {
            get { return _AccountAmountSGST28Purchase; }
            set { _AccountAmountSGST18Purchase = value; }
        }
        public static int AccountAmountCGST28Purchase
        {
            get { return _AccountAmountCGST28Purchase; }
            set { _AccountAmountCGST28Purchase = value; }
        }
        public static int AccountAmountIGST28Purchase
        {
            get { return _AccountAmountIGST28Purchase; }
            set { _AccountAmountIGST28Purchase = value; }
        }

        public static int AccountSGST5Purchase
        {
            get { return _AccountSGST5Purchase; }
            set { _AccountSGST5Purchase = value; }
        }
        public static int AccountCGST5Purchase
        {
            get { return _AccountCGST5Purchase; }
            set { _AccountCGST5Purchase = value; }
        }
        public static int AccountIGST5Purchase
        {
            get { return _AccountIGST5Purchase; }
            set { _AccountIGST5Purchase = value; }
        }
        public static int AccountSGST12Purchase
        {
            get { return _AccountSGST12Purchase; }
            set { _AccountSGST12Purchase = value; }
        }
        public static int AccountCGST12Purchase
        {
            get { return _AccountCGST12Purchase; }
            set { _AccountCGST12Purchase = value; }
        }
        public static int AccountIGST12Purchase
        {
            get { return _AccountIGST12Purchase; }
            set { _AccountIGST12Purchase = value; }
        }
        public static int AccountSGST18Purchase
        {
            get { return _AccountSGST18Purchase; }
            set { _AccountSGST18Purchase = value; }
        }
        public static int AccountCGST18Purchase
        {
            get { return _AccountCGST18Purchase; }
            set { _AccountCGST18Purchase = value; }
        }
        public static int AccountIGST18Purchase
        {
            get { return _AccountIGST18Purchase; }
            set { _AccountIGST18Purchase = value; }
        }
        public static int AccountSGST28Purchase
        {
            get { return _AccountSGST28Purchase; }
            set { _AccountSGST18Purchase = value; }
        }
        public static int AccountCGST28Purchase
        {
            get { return _AccountCGST28Purchase; }
            set { _AccountCGST28Purchase = value; }
        }
        public static int AccountIGST28Purchase
        {
            get { return _AccountIGST28Purchase; }
            set { _AccountIGST28Purchase = value; }
        }

        public static int AccountGSTPurchaseUnderScheme
        {
            get { return _AccountGSTPurchaseUnderScheme; }
            set { _AccountGSTPurchaseUnderScheme = value; }
        }

        public static int AccountVatInput6Purchase
        {
            get { return _AccountVatInput6Purchase; }
            set { _AccountVatInput6Purchase = value; }
        }
        public static int AccountVatInput13point5Purchase
        {
            get { return _AccountVatInput13point5Purchase; }
            set { _AccountVatInput13point5Purchase = value; }
        }
        public static int AccountVAT6Purchase
        {
            get { return _AccountVAT6Purchase; }
            set { _AccountVAT6Purchase = value; }
        }
        public static int AccountVAT13point5Purchase
        {
            get { return _AccountVAT13point5Purchase; }
            set { _AccountVAT13point5Purchase = value; }
        }


        public static int AccountVAT6Sale
        {
            get { return _AccountVAT6Sale; }
            set { _AccountVAT6Sale = value; }
        }
        public static int AccountVAT13point5Sale
        {
            get { return _AccountVAT13point5Sale; }
            set { _AccountVAT13point5Sale = value; }
        }
        public static int AccountVatOutput6Sale
        {
            get { return _AccountVatOutput6Sale; }
            set { _AccountVatOutput6Sale = value; }
        }
        public static int AccountVatOutput13point5Sale
        {
            get { return _AccountVatOutput13point5Sale; }
            set { _AccountVatOutput13point5Sale = value; }
        }
        public static string SortByNone
        {
            get { return _SortByNone; }
            set { _SortByNone = value; }
        }

        public static string SortByShelf
        {
            get { return _SortByShelf; }
            set { _SortByShelf = value; }
        }
        public static int AccountCounterSaleCreditNote
        {
            get { return _AccountCounterSaleCreditNote; }
            set { _AccountCounterSaleCreditNote = value; }
        }
        public static int AccountStockInOut
        {
            get { return _AccountStockInOut; }
            set { _AccountStockInOut = value; }
        }
        public static int AccountCash
        {
            get { return _AccountCash; }
            set { _AccountCash = value; }
        }
        public static int AccountCashSale
        {
            get { return _AccountCashSale; }
            set { _AccountCashSale = value; }
        }
        public static int AccountCreditSale
        {
            get { return _AccountCreditSale; }
            set { _AccountCreditSale = value; }
        }

        public static int AccountCreditStatementSale
        {
            get { return _AccountCreditStatementSale; }
            set { _AccountCreditStatementSale = value; }
        }

        public static int AccountCreditCardSale
        {
            get { return _AccountCreditCardSale; }
            set { _AccountCreditCardSale = value; }
        }

        public static int AccountVoucherSale
        {
            get { return _AccountVoucherSale; }
            set { _AccountVoucherSale = value; }
        }
    
        public static int AccountPendingCashBills
        {
            get { return _AccountPendingCashBills; }
            set { _AccountPendingCashBills = value; }
        }
        public static int AccountSalesReturn
        {
            get { return _AccountSalesReturn; }
            set { _AccountSalesReturn = value; }
        }

        public static int AccountCashDiscountSale
        {
            get { return _AccountCashDiscountSale; }
            set { _AccountCashDiscountSale = value; }
        }
        public static int AccountDiscountInCashBankEntry
        {
            get { return _AccountDiscountInCashBankEntry; }
            set { _AccountDiscountInCashBankEntry = value; }
        }
        public static int AccountPMTDiscountSale
        {
            get { return _AccountPMTDiscountSale; }
            set { _AccountPMTDiscountSale = value; }
        }

        public static int AccountItemDiscountSale
        {
            get { return _AccountItemDiscountSale; }
            set { _AccountItemDiscountSale = value; }
        }

        public static int AccountDebitNoteSale
        {
            get { return _AccountDebitNoteSale; }
            set { _AccountDebitNoteSale = value; }
        }

        public static int AccountCreditNoteSale
        {
            get { return _AccountCreditNoteSale; }
            set { _AccountCreditNoteSale = value; }
        }
        public static int AccountAddonSale
        {
            get { return _AccountAddonSale; }
            set { _AccountAddonSale = value; }
        }

        public static int AccountVatOutput5Sale
        {
            get { return _AccountVatOutput5Sale; }
            set { _AccountVatOutput5Sale = value; }
        }

        public static int AccountVatOutput12point5Sale
        {
            get { return _AccountVatOutput12point5Sale; }
            set { _AccountVatOutput12point5Sale = value; }
        }

        public static int AccountRoundoffSale
        {
            get { return _AccountRoundoffSale; }
            set { _AccountRoundoffSale = value; }
        }
        public static int AccountVATZeroSale
        {
            get { return _AccountVATZeroSale; }
            set { _AccountVATZeroSale = value; }
        }
        //public static double NumberOfRowsPerSaleBill
        //{
        //    get { return _NumberOfRowsPerSaleBill; }
        //    set { _NumberOfRowsPerSaleBill = value; }
        //}
        public static double NumberOfRowsPerInstitutionalSaleBill
        {
            get { return _NumberOfRowsPerInstitutionalSaleBill; }
            set { _NumberOfRowsPerInstitutionalSaleBill = value; }
        }
        public static double NumberOfRowsPerReport
        {
            get { return _NumberOfRowsPerReport; }
            set { _NumberOfRowsPerReport = value; }
        }
        public static int ReportPageHeight
        {
            get { return _ReportPageHeight; }
            set { _ReportPageHeight = value; }
        }

        public static int ReportPageWidth
        {
            get { return _ReportPageWidth; }
            set { _ReportPageWidth = value; }
        }
        public static int AccountCashPurchase
        {
            get { return _AccountCashPurchase; }
            set { _AccountCashPurchase = value; }
        }

        public static int AccountCreditPurchase
        {
            get { return _AccountCreditPurchase; }
            set { _AccountCreditPurchase = value; }
        }

        public static int AccountCashCreditPurchase
        {
            get { return _AccountCashCreditPurchase; }
            set { _AccountCashCreditPurchase = value; }
        }

        public static int AccountPurchaseReturn
        {
            get { return _AccountPurchaseReturn; }
            set { _AccountPurchaseReturn = value; }
        }

        public static int AccountCashDiscountPurchase
        {
            get { return _AccountCashDiscountPurchase; }
            set { _AccountCashDiscountPurchase = value; }
        }
        public static int AccountSplDiscountPurchase
        {
            get { return _AccountSplDiscountPurchase; }
            set { _AccountSplDiscountPurchase = value; }
        }
        public static int AccountSchemeDiscountPurchcase
        {
            get { return _AccountSchemeDiscountPurchcase; }
            set { _AccountSchemeDiscountPurchcase = value; }
        }
        public static int AccountItemDiscountPurchase
        {
            get { return _AccountItemDiscountPurchase; }
            set { _AccountItemDiscountPurchase = value; }
        }
        public static int AccountDebitNotePurchase
        {
            get { return _AccountDebitNotePurchase; }
            set { _AccountDebitNotePurchase = value; }
        }
        public static int AccountCreditNotePurchase
        {
            get { return _AccountCreditNotePurchase; }
            set { _AccountCreditNotePurchase = value; }
        }
        public static int AccountAddonOctroiHamaliPurchase
        {
            get { return _AccountAddonHamaliPurchase; }
            set { _AccountAddonHamaliPurchase = value; }
        }

        public static int AccountLessPurchase
        {
            get { return _AccountLessPurchase; }
            set { _AccountLessPurchase = value; }
        }
        public static int AccountVatInput5Purchase
        {
            get { return _AccountVatInput5Purchase; }
            set { _AccountVatInput5Purchase = value; }
        }
        public static int AccountVatInput12point5Purchase
        {
            get { return _AccountVatInput12point5Purchase; }
            set { _AccountVatInput12point5Purchase = value; }
        }
        public static int AccountRoundoffPurchase
        {
            get { return _AccountRoundoffPurchase; }
            set { _AccountRoundoffPurchase = value; }
        }
        public static int AccountVATZeroPurchase
        {
            get { return _AccountVATZeroPurchase; }
            set { _AccountVATZeroPurchase = value; }
        }
        public static int AccountVAT5Purchase
        {
            get { return _AccountVAT5Purchase; }
            set { _AccountVAT5Purchase = value; }
        }
        public static int AccountVAT12point5Purchase
        {
            get { return _AccountVAT12point5Purchase; }
            set { _AccountVAT12point5Purchase = value; }
        }

        public static int AccountVAT5point5Sale
        {
            get { return _AccountVAT5point5Sale; }
            set { _AccountVAT5point5Sale = value; }
        }

        public static int AccountVAT12point5Sale
        {
            get { return  _AccountVAT12point5Sale; }
            set { _AccountVAT12point5Sale = value; }
        }
        public static string DashLine80Normal
        {
            get { return _DashLine80Normal; }
            set { _DashLine80Normal = value; }
        }
        public static string DashLine80Condenced
        {
            get { return _DashLine80Condenced; }
            set { _DashLine80Condenced = value; }
        }
        public static string DashLine60Normal
        {
            get { return _DashLine60Normal; }
            set { _DashLine60Normal = value; }
        }
        public static string VoucherTypeForCashSale
        {
            get { return _VoucherTypeForCashSale; }
            set { _VoucherTypeForCashSale = value; }
        }
        public static string VoucherTypeForCreditSale
        {
            get { return _VoucherTypeForCreditSale; }
            set { _VoucherTypeForCreditSale = value; }
        }
        public static string VoucherTypeForCreditStatementSale
        {
            get { return _VoucherTypeForCreditStatementSale; }
            set { _VoucherTypeForCreditStatementSale = value; }
        }
        public static string VoucherTypeForVoucherSale
        {
            get { return _VoucherTypeForVoucherSale; }
            set { _VoucherTypeForVoucherSale = value; }
        }
        public static string VoucherTypeForChallanSale
        {
            get { return _VoucherTypeForChallanSale; }
            set { _VoucherTypeForChallanSale = value; }
        }
        public static string VoucherTypeForBillReprint
        {
            get { return _VoucherTypeForBillReprint; }
            set { _VoucherTypeForBillReprint = value; }
        }
        public static string VoucherTypeForCashPurchase
        {
            get { return _VoucherTypeForCashPurchase; }
            set { _VoucherTypeForCashPurchase = value; }
        }
        public static string VoucherTypeForCreditPurchase
        {
            get { return _VoucherTypeForCreditPurchase; }
            set { _VoucherTypeForCreditPurchase = value; }
        }

        public static string VoucherTypeForCreditStatementPurchase
        {
            get { return _VoucherTypeForCreditStatementPurchase; }
            set { _VoucherTypeForCreditStatementPurchase = value; }
        }

        public static string VoucherTypeForChallanPurchase
        {
            get { return _VoucherTypeForChallanPurchase; }
            set { _VoucherTypeForChallanPurchase = value; }
        }
        public static string VoucherTypeForCashPayment
        {
            get { return _VoucherTypeForCashPayment; }
            set { _VoucherTypeForCashPayment = value; }
        }

        public static string VoucherTypeForCashReceipt
        {
            get { return _VoucherTypeForCashReceipt; }
            set { _VoucherTypeForCashReceipt = value; }
        }
        public static string VoucherTypeForCashExpenses
        {
            get { return _VoucherTypeForCashExpenses; }
            set { _VoucherTypeForCashExpenses = value; }
        }
        public static string VoucherTypeForBankPayment
        {
            get { return _VoucherTypeForBankPayment; }
            set { _VoucherTypeForBankPayment = value; }
        }
        public static string VoucherTypeForBankReceipt
        {
            get { return _VoucherTypeForBankReceipt; }
            set { _VoucherTypeForBankReceipt = value; }
        }

        public static string VoucherTypeForBankExpenses
        {
            get { return _VoucherTypeForBankExpenses; }
            set { _VoucherTypeForBankExpenses = value; }
        }
        public static string VoucherTypeForContraEntry
        {
            get { return _VoucherTypeForContraEntry; }
            set { _VoucherTypeForContraEntry = value; }
        }
        public static string VoucherTypeForOpeningBalance
        {
            get { return _VoucherTypeForOpeningBalance; }
            set { _VoucherTypeForOpeningBalance = value; }
        }
        public static string VoucherTypeForCreditNoteStock
        {
            get { return _VoucherTypeForCreditNoteStock; }
            set { _VoucherTypeForCreditNoteStock = value; }
        }
        public static string VoucherTypeForCreditNoteAmount
        {
            get { return _VoucherTypeForCreditNoteAmount; }
            set { _VoucherTypeForCreditNoteAmount = value; }
        }
        public static string VoucherTypeForDebitNoteStock
        {
            get { return _VoucherTypeForDebitNoteStock; }
            set { _VoucherTypeForDebitNoteStock = value; }
        }
        public static string VoucherTypeForDebitNoteAmount
        {
            get { return _VoucherTypeForDebitNoteAmount; }
            set { _VoucherTypeForDebitNoteAmount = value; }
        }
        public static string VoucherTypeForStockIN
        {
            get { return _VoucherTypeForStockIN; }
            set { _VoucherTypeForStockIN = value; }
        }

        public static string VoucherTypeForStockOut
        {
            get { return _VoucherTypeForStockOut; }
            set { _VoucherTypeForStockOut = value; }
        }
        public static string VoucherTypeForSalesReturn
        {
            get { return _VoucherTypeForSalesReturn; }
            set { _VoucherTypeForSalesReturn = value; }
        }
        public static string VoucherTypeForPurchaseReturn
        {
            get { return _VoucherTypeForPurchaseReturn; }
            set { _VoucherTypeForPurchaseReturn = value; }
        }
        public static string VoucherTypeForTransferToAccount
        {
            get { return _VoucherTypeForTransferToAccount; }
            set { _VoucherTypeForTransferToAccount = value; }
        }
        
        public static string VoucherTypeForOpeningStock
        {
            get { return _VoucherTypeForOpeningStock; }
            set { _VoucherTypeForOpeningStock = value; }
        }
        public static string VoucherTypeForPurchaseOrder
        {
            get { return _VoucherTypeForPurchaseOrder; }
            set { _VoucherTypeForPurchaseOrder = value; }
        }

        public static string VoucherTypeForCorrectionInRate
        {
            get { return _VoucherTypeForCorrectionInRate; }
            set { _VoucherTypeForCorrectionInRate = value; }
        }

        public static string VoucherTypeForJournalEntry
        {
            get { return _VoucherTypeForJournalEntry; }
            set { _VoucherTypeForJournalEntry = value; }
        }

        public static string VoucherTypeForChequeReturn
        {
            get { return _VoucherTypeForChequeReturn; }
            set { _VoucherTypeForChequeReturn = value; }
        }

        public static string VoucherTypeForStatementPurchase
        {
            get { return _VoucherTypeForStatementPurchase; }
            set { _VoucherTypeForStatementPurchase = value; }
        }
        public static string VoucherTypeForStatementSale
        {
            get { return _VoucherTypeForStatementSale; }
            set { _VoucherTypeForStatementSale = value; }
        }

        public static string VoucherTypeForStatementHospital
        {
            get { return _VoucherTypeForStatementHospital; }
            set { _VoucherTypeForStatementHospital = value; }
        }

        //public static string VoucherTypeForDistributorSaleCash
        //{
        //    get { return _VoucherTypeForDistributorSaleCash; }
        //    set { _VoucherTypeForDistributorSaleCash = value; }
        //}

        //public static string VoucherTypeForDistributorSaleCredit
        //{
        //    get { return _VoucherTypeForDistributorSaleCredit; }
        //    set { _VoucherTypeForDistributorSaleCredit = value; }
        //}

        //public static string VoucherTypeForDistributorSaleCreditStatement
        //{
        //    get { return _VoucherTypeForDistributorSaleCreditStatement; }
        //    set { _VoucherTypeForDistributorSaleCreditStatement = value; }
        //}

        //public static string VoucherTypeForDistributorCreditNoteStock
        //{
        //    get { return _VoucherTypeForDistributorCreditNoteStock; }
        //    set { _VoucherTypeForDistributorCreditNoteStock = value; }
        //}

        public static string VoucherTypeForJournalVoucher     // [ansuman]
        {
            get { return _VoucherTypeForJournalVoucher; }
            set { _VoucherTypeForJournalVoucher = value; }
        }

        public static int GroupCodeForDebtor
        {
            get { return _GroupCodeForDebtor; }
            set { _GroupCodeForDebtor = value; }
        }

        public static int GroupCodeForCreditor
        {
            get { return _GroupCodeForCreditor; }
            set { _GroupCodeForCreditor = value; }
        }
        public static int GroupCodeForBank
        {
            get { return _GroupCodeForBank; }
            set { _GroupCodeForBank = value; }
        }

        public static int GroupCodeForOtherCreditor
        {
            get { return _GroupCodeForOtherCreditor; }
            set { _GroupCodeForOtherCreditor = value; }
        }

        public static int GroupCodeForOtherDebtor
        {
            get { return _GroupCodeForOtherDebtor; }
            set { _GroupCodeForOtherDebtor = value; }
        }
        public static int GroupCodeClosingStock
        {
            get { return _GroupCodeClosingStock; }
            set { _GroupCodeClosingStock = value; }
        }

        public static int GroupCodeForGrossProfitCD
        {
            get { return _GroupCodeForGrossProfitCD; }
            set { _GroupCodeForGrossProfitCD = value; }
        }

        public static int GroupCodeForLOSSCD
        {
            get { return _GroupCodeForLOSSCD; }
            set { _GroupCodeForLOSSCD = value; }
        }
        public static int GroupCodeForGrossProfitBD
        {
            get { return  _GroupCodeForGrossProfitBD; }
            set { _GroupCodeForGrossProfitBD = value; }
        }
        public static int GroupCodeForLOSSBD
        {
            get { return  _GroupCodeForLOSSBD; }
            set { _GroupCodeForLOSSBD = value; }
        }
        public static int GroupCodeForNETLOSS
        {
            get { return _GroupCodeForNETLOSS; }
            set { _GroupCodeForNETLOSS = value; }
        }
        public static int GroupCodeForNETProfit
        {
            get { return _GroupCodeForNETProfit; }
            set { _GroupCodeForNETProfit = value; }
        }

        public static string AccTypeForDebtor
        {
            get { return _AccTypeForDebtor; }
            set { _AccTypeForDebtor = value; }
        }

        public static string AccTypeForBank
        {
            get { return _AccTypeForBank; }
            set { _AccTypeForBank = value; }
        }
        public static string AccTypeForCreditor
        {
            get { return _AccTypeForCreditor; }
            set { _AccTypeForCreditor = value; }
        }
        public static string AccTypeForGeneral
        {
            get { return _AccTypeForGeneral; }
            set { _AccTypeForGeneral = value; }
        }
        public static string AccTypeForOtherCreditor
        {
            get { return _AccTypeForOtherCreditor; }
            set { _AccTypeForOtherCreditor = value; }
        }
        public static string AccTypeForOtherDebtor
        {
            get { return _AccTypeForOtherDebtor; }
            set { _AccTypeForOtherDebtor = value; }
        }
        public static string AccTypeForPurchase
        {
            get { return _AccTypeForPurchase; }
            set { _AccTypeForPurchase = value; }
        }
        public static string AccTypeForSale
        {
            get { return _AccTypeForSale; }
            set { _AccTypeForSale = value; }
        }
        public static string AccTypeForPatient
        {
            get { return _AccTypeForPatient; }
            set { _AccTypeForPatient = value; }
        }

        public static string AccCodeForDebtor
        {
            get { return _AccCodeForDebtor; }
            set { _AccCodeForDebtor = value; }
        }
        public static string AccCodeForCreditor
        {
            get { return _AccCodeForCreditor; }
            set { _AccCodeForCreditor = value; }
        }

        public static string AccCodeForBank
        {
            get { return _AccCodeForBank; }
            set { _AccCodeForBank = value; }
        }

        public static string AccCodeForGeneral
        {
            get { return _AccCodeForGeneral; }
            set { _AccCodeForGeneral = value; }
        }
        public static string AccCodeForSale
        {
            get { return _AccCodeForSale; }
            set { _AccCodeForSale = value; }
        }

        public static string AccCodeForPurchase
        {
            get { return _AccCodeForPurchase; }
            set { _AccCodeForPurchase = value; }
        }

        public static string AccCodeForOtherCreditor
        {
            get { return _AccCodeForOtherCreditor; }
            set { _AccCodeForOtherCreditor = value; }
        }
        public static string AccCodeForOtherDebtor
        {
            get { return _AccCodeForOtherDebtor; }
            set { _AccCodeForOtherDebtor = value; }
        }

        public static string AccCodeForPatient
        {
            get { return _AccCodeForPatient; }
            set { _AccCodeForPatient = value; }
        }


        public static string SubTypeForBreakage
        {
            get { return _SubTypeForBreakage; }
            set { _SubTypeForBreakage = value; }
        }

        public static string SubTypeForRegularSale
        {
            get { return _SubTypeForRegularSale; }
            set { _SubTypeForRegularSale = value; }
        }

        public static string SubTypeForRegularSale2
        {
            get { return _SubTypeForRegularSale2; }
            set { _SubTypeForRegularSale2 = value; }
        }

        public static string SubTypeForSpecialSale
        {
            get { return _SubTypeForSpecialSale; }
            set { _SubTypeForSpecialSale = value; }
        }

        public static string SubTypeForPTSSale
        {
            get { return _SubTypeForPTSSale; }
            set { _SubTypeForPTSSale = value; }
        }
        public static string SubTypeForExpiry
        {
            get { return _SubTypeForExpiry; }
            set { _SubTypeForExpiry = value; }
        }
        public static string SubTypeForGoodsReturn
        {
            get { return _SubTypeForGoodsReturn; }
            set { _SubTypeForGoodsReturn = value; }
        }
       
        public static string SubTypeForStock
        {
            get { return _SubTypeForStock; }
            set { _SubTypeForStock = value; }
        }
        public static string SubTypeForVoucherSale
        {
            get { return _SubTypeForVoucherSale; }
            set { _SubTypeForVoucherSale = value; }
        }          
        public static string SubTypeForSaleWithoutStock
        {
            get { return _SubTypeForSaleWithoutStock; }
            set { _SubTypeForSaleWithoutStock = value; }
        }       
        public static int AccountStatementPurchase
        {
            get { return _AccountStatementPurchase; }
            set { _AccountStatementPurchase = value; }
        }

        public static int AccountStatementSale
        {
            get { return _AccountStatementSale; }
            set { _AccountStatementSale = value; }
        }

        public static string TransactionTypeForCash
        {
            get { return _TransactionTypeForCash; }
            set { _TransactionTypeForCash = value; }
        }

        public static string TransactionTypeForCredit
        {
            get { return _TransactionTypeForCredit; }
            set { _TransactionTypeForCredit = value; }
        }

        public static string TransactionTypeForCreditCard
        {
            get { return _TransactionTypeForCreditCard; }
            set { _TransactionTypeForCreditCard = value; }
        }

        public static string TransactionTypeForCreditStatement
        {
            get { return _TransactionTypeForCreditStatement; }
            set { _TransactionTypeForCreditStatement = value; }
        }

        public static string TransactionTypeForVoucher
        {
            get { return _TransactionTypeForVoucher; }
            set { _TransactionTypeForVoucher = value; }
        }

        public static string PrinterTypeDotMatrix
        {
            get { return _PrinterTypeDotMatrix; }
            set { _PrinterTypeDotMatrix = value; }
        }

        public static string PrinterTypeLaserJet
        {
            get { return _PrinterTypeLaserJet; }
            set { _PrinterTypeLaserJet = value; }
        }

        public static int AccountAddonPurchase
        {
            get { return _AccountAddonPurchase; }
            set { _AccountAddonPurchase = value; }
        }

        public static int AccountFreightPurchase
        {
            get { return _AccountFreightPurchase; }
            set { _AccountFreightPurchase = value; }
        }
        #endregion Properties

        private static void Initialize()
        {


            _AccountCash = 1;

            _AccountStockInOut = 11;
            _AccountStatementPurchase = 12;
            _AccountStatementSale = 13;

         //-   _AccountChequeReturnCharges = 51;

            _AccountDiscountInCashBankEntry = 52;
          

            _AccountCashSale = 101;
            _AccountCreditSale = 102;
            _AccountCreditStatementSale = 103;
        
            _AccountSalesReturn = 104;
            _AccountCashDiscountSale = 105;
            _AccountDebitNoteSale = 106;
            _AccountAddonSale = 107;            
            _AccountRoundoffSale = 110;
            _AccountItemDiscountSale = 112;
        //-    _AccountRateDifferenceCRNOTEAdjustedInSale = 121;
        //-    _AccountBreakageExpiryCRNOTEAdjustedInSale = 122;

            _AccountCashPurchase = 201;
            _AccountCreditPurchase = 202;
         //-   _AccountCreditStatementPurchase = 203;
            _AccountPurchaseReturn = 204;
            _AccountCashDiscountPurchase = 205;
          //-  _AccountSurchargePurchase = 206;
            _AccountSplDiscountPurchase = 210;           
            _AccountSchemeDiscountPurchcase = 211;
            _AccountItemDiscountPurchase = 212;
          //  _AccountDebitNotePurchase = 213;
            _AccountCreditNotePurchase = 214;
            _AccountAddonHamaliPurchase = 215;           
            _AccountRoundoffPurchase = 216;           
            _AccountLessPurchase = 218;
            //   _AccountAddonPurchase = "40019";
            // ?   _AccountBreakageExpiryCRNOTEAdjustedInPurchase = 227;
           // ?  _AccountRateDifferenceCRNOTEAdjustedInPurchase = 228;
           // ? _AccountDBNOTEInterestAdjustedinPurchase = 229;
           // ? _AccountCRNOTEOtherAdjustedinPurchase = 230;
           // ? _AccountDBNOTELatePaymentAdjustedinPurchase = 232;
           // ? _AccountDBNOTEOtherAdjustedInPurchase = 233;
            _AccountFreightPurchase = 231;



            _AccountAmountGST0Sale = 301;
            _AccountAmountSGST5Sale = 302;
            _AccountAmountCGST5Sale = 303;
            _AccountAmountIGST5Sale = 304;
            _AccountAmountSGST12Sale = 305;
            _AccountAmountCGST12Sale = 306;
            _AccountAmountIGST12Sale = 307;
            _AccountAmountSGST18Sale = 308;
            _AccountAmountCGST18Sale = 309;
            _AccountAmountIGST18Sale = 310;
            _AccountAmountSGST28Sale = 311;
            _AccountAmountCGST28Sale = 312;
            _AccountAmountIGST28Sale = 313;

            _AccountSGST5Sale = 331;
            _AccountCGST5Sale = 332;
            _AccountIGST5Sale = 333;
            _AccountSGST12Sale = 334;
            _AccountCGST12Sale = 335;
            _AccountIGST12Sale = 336;
            _AccountSGST18Sale = 337;
            _AccountCGST18Sale = 338;
            _AccountIGST18Sale = 339;
            _AccountSGST28Sale = 340;
            _AccountCGST28Sale = 341;
            _AccountIGST28Sale = 342;


            _AccountAmountGST0Purchase = 401;
            _AccountAmountSGST5Purchase = 402;
            _AccountAmountCGST5Purchase = 403;
            _AccountAmountIGST5Purchase = 404;
            _AccountAmountSGST12Purchase = 405;
            _AccountAmountCGST12Purchase = 406;
            _AccountAmountIGST12Purchase = 407;
            _AccountAmountSGST18Purchase = 408;
            _AccountAmountCGST18Purchase = 409;
            _AccountAmountIGST18Purchase = 410;
            _AccountAmountSGST28Purchase = 411;
            _AccountAmountCGST28Purchase = 412;
            _AccountAmountIGST28Purchase = 413;

            _AccountSGST5Purchase = 431;
            _AccountCGST5Purchase = 432;
            _AccountIGST5Purchase = 433;
            _AccountSGST12Purchase = 434;
            _AccountCGST12Purchase = 435;
            _AccountIGST12Purchase = 436;
            _AccountSGST18Purchase = 437;
            _AccountCGST18Purchase = 438;
            _AccountIGST18Purchase = 439;
            _AccountSGST28Purchase = 440;
            _AccountCGST28Purchase = 441;
            _AccountIGST28Purchase = 442;

            _AccountGSTPurchaseUnderScheme = 501;

            //_NumberOfRowsPerSaleBill = 7;
            _NumberOfRowsPerInstitutionalSaleBill = 50;
            _NumberOfRowsPerReport = 53;
            _ReportPageHeight = 1225;
            _ReportPageWidth = 800;

            _DashLine80Normal = "------------------------------------------------------------------------------------------------------------" +
            "----------------------------------------------------------------------------------------------------";
            _DashLine80Condenced = "----------------------------------------------------------------------------------------------------" +
            "----------------------------------------------------------------------------------------------------" +
            "----------------------------------------------------";

            _DashLine60Normal = "----------------------------------------------------------------------------------------------------" +
           "----------------------------------";
            _VoucherTypeForCashSale = "SCA";  //7
            _VoucherTypeForCreditSale = "SCR"; //6
            _VoucherTypeForCreditStatementSale = "SCT"; //5
            _VoucherTypeForVoucherSale = "SVU"; //8
            _VoucherTypeForBillReprint = "SBR"; //35
            _VoucherTypeForChallanSale = "CHS";

            _VoucherTypeForCashPurchase = "PCA"; //4
            _VoucherTypeForCreditPurchase = "PCR";   //3
            _VoucherTypeForCreditStatementPurchase = "PCT";  //2
            _VoucherTypeForChallanPurchase = "CHP";

            _VoucherTypeForCashPayment = "CSP"; //9
            _VoucherTypeForCashReceipt = "CSR"; //10
            _VoucherTypeForCashExpenses = "CSE"; //11
            _VoucherTypeForBankPayment = "BKP"; //12
            _VoucherTypeForBankReceipt = "BKR";  //13
            _VoucherTypeForBankExpenses = "BKE"; //29
            _VoucherTypeForContraEntry = "CTR"; //30

            _VoucherTypeForOpeningBalance = "OPB";  // 31

            _VoucherTypeForCreditNoteStock = "CNS"; //22
            _VoucherTypeForCreditNoteAmount = "CNA";  //23
            _VoucherTypeForDebitNoteStock = "DNS";   //20
            _VoucherTypeForDebitNoteAmount = "DNA";  // 21
            _VoucherTypeForStockIN = "STI"; // 25
            _VoucherTypeForStockOut = "STO";  // 24
            _VoucherTypeForSalesReturn = "SRT";  // 32
            _VoucherTypeForPurchaseReturn = "PRT"; // 33
            _VoucherTypeForTransferToAccount = "TRN";

            _VoucherTypeForOpeningStock = "OPS"; //01
            _VoucherTypeForPurchaseOrder = "POR";   // 19
            _VoucherTypeForCorrectionInRate = "CIR"; //17
            _VoucherTypeForJournalEntry = "JVE";  // 14
            _VoucherTypeForChequeReturn = "CQR";  // 18
            _VoucherTypeForStatementPurchase = "PST";  // 16
            _VoucherTypeForStatementSale = "SST"; // 15
            _VoucherTypeForStatementHospital = "HST"; // 34

            //_VoucherTypeForDistributorSaleCash = "WCA";   // 26
            //_VoucherTypeForDistributorSaleCredit = "WCR";  // 27
            //_VoucherTypeForDistributorSaleCreditStatement = "WCT"; // 28
            //_VoucherTypeForDistributorCreditNoteStock = "WCN"; // 36
            _VoucherTypeForJournalVoucher = "JV";

            _GroupCodeForBank = 23; 
            _GroupCodeForCreditor = 31;
            _GroupCodeForDebtor = 28;
            _GroupCodeForOtherCreditor = 101;
            _GroupCodeForOtherDebtor = 102;
            _GroupCodeClosingStock = 02000;
            _GroupCodeForGrossProfitCD = 02001;
            _GroupCodeForLOSSCD = 02002;
            _GroupCodeForGrossProfitBD = 02003;
            _GroupCodeForLOSSBD = 02004;
            _GroupCodeForNETProfit = 02005;
            _GroupCodeForNETLOSS = 02006;



            _AccTypeForDebtor = "Debtor";
            _AccTypeForCreditor = "Creditor";
            _AccTypeForBank = "Bank";
            _AccTypeForGeneral = "General";
            _AccTypeForSale = "Sale";
            _AccTypeForPurchase = "Purchase";
            _AccTypeForOtherCreditor = "OtherCreditor";
            _AccTypeForOtherDebtor = "OtherDebtor";
            _AccTypeForPatient = "Patient";


            _AccCodeForDebtor = "D";
            _AccCodeForCreditor = "C";
            _AccCodeForBank = "B";
            _AccCodeForGeneral = "G";
            _AccCodeForSale = "S";
            _AccCodeForPurchase = "H";
            _AccCodeForOtherCreditor = "R";
            _AccCodeForOtherDebtor = "E";
            _AccCodeForPatient = "P";


            _SubTypeForBreakage = "B";
            _SubTypeForExpiry = "E";
            _SubTypeForGoodsReturn = "G";
            _SubTypeForStock = "S";         
            _SubTypeForRegularSale = "T";
            _SubTypeForSpecialSale = "S";
            _SubTypeForPTSSale = "P";
            _SubTypeForRegularSale2 = "R";          
            _SubTypeForSaleWithoutStock = "W";
           

            _TransactionTypeForCash = "Cash";
            _TransactionTypeForCredit = "Credit";
            _TransactionTypeForCreditCard = "CreditCard";
            _TransactionTypeForCreditStatement = "CreditStatement";
            _TransactionTypeForVoucher = "Voucher";

            _PrinterTypeDotMatrix = "Dot Matrix";
            _PrinterTypeLaserJet = "LaserJet";

            // NOTE: Give the same spelling is text for the button.
            _SortByShelf = "Shelf";
            _SortByNone = "None";

        }



        //private void FilldgvCheques()
        //{
        //    ConstructdgvCheques();
        //    DataTable dtable = new DataTable();
        //    string strSql = "Select distinct  a.CBID,a.VoucherType,a.VoucherNumber,a.VoucherDate,a.AmountNet, " +
        //                    "a.AccountID, b.AccountID, b.AccName, b.AccAddress1,b.AccAddress2 from vouchercashbankpayment a, masteraccount b " +
        //                    "where a.AccountId = b.AccountId and a.VoucherType = " + "'" + FixAccounts.VoucherTypeForBankPayment + "'" + "  order by a.vouchernumber ";

        //    //   dtable = DBInterface.SelectDataTable(strSql);

        //    dgvCheques.DataSource = dtable;
        //}

        //private void ConstructdgvCheques()
        //{
        //    dgvCheques.Columns.Clear();
        //    try
        //    {
        //        DataGridViewTextBoxColumn column;
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_ID";
        //        column.DataPropertyName = "CBID";
        //        column.HeaderText = "ID";
        //        column.Visible = false;
        //        dgvCheques.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherType";
        //        column.DataPropertyName = "VoucherType";
        //        column.HeaderText = "Type";
        //        column.Width = 80;
        //        dgvCheques.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherNo";
        //        column.DataPropertyName = "VoucherNumber";
        //        column.HeaderText = "Number";
        //        column.Width = 90;
        //        dgvCheques.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherDate";
        //        column.DataPropertyName = "VoucherDate";
        //        column.HeaderText = "Date";
        //        column.Width = 100;
        //        dgvCheques.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Amount";
        //        column.DataPropertyName = "AmountNet";
        //        column.HeaderText = "Amount";
        //        column.Width = 110;
        //        dgvCheques.Columns.Add(column);

        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_PartyName";
        //        column.DataPropertyName = "AccName";
        //        column.HeaderText = "Party";
        //        column.Width = 200;
        //        dgvCheques.Columns.Add(column);

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}


    }
}
