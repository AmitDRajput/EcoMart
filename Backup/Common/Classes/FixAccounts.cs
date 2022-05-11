using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.DataLayer;

namespace PharmaSYSRetailPlus.Common
{
    public class FixAccounts
    {
        #region Declaration
        private static string _AccountStockInOut;
        private static string _AccountCash;
        private static string _AccountCashSale; 
        private static string _AccountCreditSale;
        private static string _AccountCashCreditSale;
        private static string _AccountVoucherSale;
        private static string _AccountPendingCashBills;
        private static string _AccountSalesReturn;
        private static string _AccountCashDiscountSale;
        private static string _AccountPMTDiscountSale;
        private static string _AccountItemDiscountSale;
        private static string _AccountDebitNoteSale;
        private static string _AccountCreditNoteSale;
        private static string _AccountAddonSale;
        private static string _AccountVatOutput5Sale;
        private static string _AccountVatOutput12point5Sale;
        private static string _AccountRoundoffSale;
        private static string _AccountVATZeroSale;
        private static string _AccountStatementPurchase;
        private static string _AccountStatementSale;

        private static string _AccountCashPurchase;
        private static string _AccountCreditPurchase;
        private static string _AccountCashCreditPurchase;
        private static string _AccountPurchaseReturn;
        private static string _AccountCashDiscountPurchase;
        private static string _AccountSplDiscountPurchase;
        private static string _AccountSchemeDiscountPurchcase;
        private static string _AccountItemDiscountPurchase;
        private static string _AccountDebitNotePurchase;
        private static string _AccountCreditNotePurchase;
        private static string _AccountAddonOctroiHamaliPurchase;
        private static string _AccountVatInput5Purchase;
        private static string _AccountVatInput12point5Purchase;
        private static string _AccountRoundoffPurchase;
        private static string _AccountVATZeroPurchase;

        private static string _DashLine80Normal;
        private static string _DashLine80Condenced;
        private static string _DashLine60Normal;

        private static string _VoucherTypeForCashSale;
        private static string _VoucherTypeForCreditSale;
        private static string _VoucherTypeForCreditStatementSale;
        private static string _VoucherTypeForVoucherSale;
        private static string _VoucherTypeForBillReprint;
       

        private static string _VoucherTypeForCashPurchase;
        private static string _VoucherTypeForCreditPurchase;
        private static string _VoucherTypeForCreditStatementPurchase;

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

        private static string _VoucherTypeForCashReceipt;
        private static string _VoucherTypeForCashPayment;
        private static string _VoucherTypeForBankReceipt;
        private static string _VoucherTypeForBankPayment;
        private static string _VoucherTypeForCashExpenses;
        private static string _VoucherTypeForBankExpenses;
        private static string _VoucherTypeForContraEntry;

        private static string _VoucherTypeForOpeningBalance;

        private static string _VoucherTypeForDistributorSaleCash;   // 26
        private static string _VoucherTypeForDistributorSaleCredit;  // 27
        private static string _VoucherTypeForDistributorSaleCreditStatement; // 28


        private static string _GroupCodeForDebtor;
        private static string _GroupCodeForCreditor;
        private static string _GroupCodeForBank;
        private static string _GroupCodeForOtherCreditor;
        private static string _GroupCodeForOtherDebtor;

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

        private static string _SubTypeForDebtorSale;
        private static string _SubTypeForDistributorSale;
        private static string _SubTypeForPatientSale;
        private static string _SubTypeForHospitalSale;
        private static string _SubTypeForCreditCardSale;
        private static string _SubTypeForInstitutionalSale;
        private static string _SubTypeForExpiry;
        private static string _SubTypeForBreakage;
        private static string _SubTypeForStock;
        private static string _SubTypeForGoodsReturn;
        private static string _SubTypeForVoucherSale;
        private static string _SubTypeForSaleWithoutStock;

        private static string _TransactionTypeForCash;
        private static string _TransactionTypeForCredit;
        private static string _TransactionTypeForCreditStatement;
        private static string _TransactionTypeForCreditCard;
        private static string _TransactionTypeForVoucher;


        // Printing
        //  private static double _NumberOfRowsPerSaleBill;
        private static double _NumberOfRowsPerInstitutionalSaleBill;
        private static double _NumberOfRowsPerReport;
        private static int _ReportPageHeight;
        private static int _ReportPageWidth;
        #endregion Declaration

        #region Constructor
        static FixAccounts()
        {
            Initialize();
        }
        #endregion Constructor

        #region Properties
        public static string AccountStockInOut
        {
            get { return _AccountStockInOut; }
            set { _AccountStockInOut = value; }
        }
        public static string AccountCash
        {
            get { return _AccountCash; }
            set { _AccountCash = value; }
        }
        public static string AccountCashSale
        {
            get { return _AccountCashSale; }
            set { _AccountCashSale = value; }
        }
        public static string AccountCreditSale
        {
            get { return _AccountCreditSale; }
            set { _AccountCreditSale = value; }
        }

        public static string AccountCashCreditSale
        {
            get { return _AccountCashCreditSale; }
            set { _AccountCashCreditSale = value; }
        }

        public static string AccountVoucherSale
        {
            get { return _AccountVoucherSale; }
            set { _AccountVoucherSale = value; }
        }

        public static string AccountPendingCashBills
        {
            get { return _AccountPendingCashBills; }
            set { _AccountPendingCashBills = value; }
        }
        public static string AccountSalesReturn
        {
            get { return _AccountSalesReturn; }
            set { _AccountSalesReturn = value; }
        }

        public static string AccountCashDiscountSale
        {
            get { return _AccountCashDiscountSale; }
            set { _AccountCashDiscountSale = value; }
        }

        public static string AccountPMTDiscountSale
        {
            get { return _AccountPMTDiscountSale; }
            set { _AccountPMTDiscountSale = value; }
        }

        public static string AccountItemDiscountSale
        {
            get { return _AccountItemDiscountSale; }
            set { _AccountItemDiscountSale = value; }
        }

        public static string AccountDebitNoteSale
        {
            get { return _AccountDebitNoteSale; }
            set { _AccountDebitNoteSale = value; }
        }

        public static string AccountCreditNoteSale
        {
            get { return _AccountCreditNoteSale; }
            set { _AccountCreditNoteSale = value; }
        }
        public static string AccountAddonSale
        {
            get { return _AccountAddonSale; }
            set { _AccountAddonSale = value; }
        }

        public static string AccountVatOutput5Sale
        {
            get { return _AccountVatOutput5Sale; }
            set { _AccountVatOutput5Sale = value; }
        }

        public static string AccountVatOutput12point5Sale
        {
            get { return _AccountVatOutput12point5Sale; }
            set { _AccountVatOutput12point5Sale = value; }
        }

        public static string AccountRoundoffSale
        {
            get { return _AccountRoundoffSale; }
            set { _AccountRoundoffSale = value; }
        }
        public static string AccountVATZeroSale
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
        public static string AccountCashPurchase
        {
            get { return _AccountCashPurchase; }
            set { _AccountCashPurchase = value; }
        }

        public static string AccountCreditPurchase
        {
            get { return _AccountCreditPurchase; }
            set { _AccountCreditPurchase = value; }
        }

        public static string AccountCashCreditPurchase
        {
            get { return _AccountCashCreditPurchase; }
            set { _AccountCashCreditPurchase = value; }
        }

        public static string AccountPurchaseReturn
        {
            get { return _AccountPurchaseReturn; }
            set { _AccountPurchaseReturn = value; }
        }

        public static string AccountCashDiscountPurchase
        {
            get { return _AccountCashDiscountPurchase; }
            set { _AccountCashDiscountPurchase = value; }
        }
        public static string AccountSplDiscountPurchase
        {
            get { return _AccountSplDiscountPurchase; }
            set { _AccountSplDiscountPurchase = value; }
        }
        public static string AccountSchemeDiscountPurchcase
        {
            get { return _AccountSchemeDiscountPurchcase; }
            set { _AccountSchemeDiscountPurchcase = value; }
        }
        public static string AccountItemDiscountPurchase
        {
            get { return _AccountItemDiscountPurchase; }
            set { _AccountItemDiscountPurchase = value; }
        }
        public static string AccountDebitNotePurchase
        {
            get { return _AccountDebitNotePurchase; }
            set { _AccountDebitNotePurchase = value; }
        }
        public static string AccountCreditNotePurchase
        {
            get { return _AccountCreditNotePurchase; }
            set { _AccountCreditNotePurchase = value; }
        }
        public static string AccountAddonOctroiHamaliPurchase
        {
            get { return _AccountAddonOctroiHamaliPurchase; }
            set { _AccountAddonOctroiHamaliPurchase = value; }
        }
        public static string AccountVatInput5Purchase
        {
            get { return _AccountVatInput5Purchase; }
            set { _AccountVatInput5Purchase = value; }
        }
        public static string AccountVatInput12point5Purchase
        {
            get { return _AccountVatInput12point5Purchase; }
            set { _AccountVatInput12point5Purchase = value; }
        }
        public static string AccountRoundoffPurchase
        {
            get { return _AccountRoundoffPurchase; }
            set { _AccountRoundoffPurchase = value; }
        }
        public static string AccountVATZeroPurchase
        {
            get { return _AccountVATZeroPurchase; }
            set { _AccountVATZeroPurchase = value; }
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

        public static string VoucherTypeForDistributorSaleCash
        {
            get { return _VoucherTypeForDistributorSaleCash; }
            set { _VoucherTypeForDistributorSaleCash = value; }
        }

        public static string VoucherTypeForDistributorSaleCredit
        {
            get { return _VoucherTypeForDistributorSaleCredit; }
            set { _VoucherTypeForDistributorSaleCredit = value; }
        }

        public static string VoucherTypeForDistributorSaleCreditStatement
        {
            get { return _VoucherTypeForDistributorSaleCreditStatement; }
            set { _VoucherTypeForDistributorSaleCreditStatement = value; }
        }

        public static string GroupCodeForDebtor
        {
            get { return _GroupCodeForDebtor; }
            set { _GroupCodeForDebtor = value; }
        }

        public static string GroupCodeForCreditor
        {
            get { return _GroupCodeForCreditor; }
            set { _GroupCodeForCreditor = value; }
        }
        public static string GroupCodeForBank
        {
            get { return _GroupCodeForBank; }
            set { _GroupCodeForBank = value; }
        }

        public static string GroupCodeForOtherCreditor
        {
            get { return _GroupCodeForOtherCreditor; }
            set { _GroupCodeForOtherCreditor = value; }
        }

        public static string GroupCodeForOtherDebtor
        {
            get { return _GroupCodeForOtherDebtor; }
            set { _GroupCodeForOtherDebtor = value; }
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

        public static string SubTypeForDebtorSale
        {
            get { return _SubTypeForDebtorSale; }
            set { _SubTypeForDebtorSale = value; }
        }
        public static string SubTypeForDistributorSale
        {
            get { return _SubTypeForDistributorSale; }
            set { _SubTypeForDistributorSale = value; }
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
        public static string SubTypeForPatientSale
        {
            get { return _SubTypeForPatientSale; }
            set { _SubTypeForPatientSale = value; }
        }

        public static string SubTypeForHospitalSale
        {
            get { return _SubTypeForHospitalSale; }
            set { _SubTypeForHospitalSale = value; }
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
        public static string SubTypeForCreditCardSale
        {
            get { return _SubTypeForCreditCardSale; }
            set { _SubTypeForCreditCardSale = value; }
        }
        public static string SubTypeForInstitutionalSale
        {
            get { return _SubTypeForInstitutionalSale; }
            set { _SubTypeForInstitutionalSale = value; }
        }
        public static string SubTypeForSaleWithoutStock
        {
            get { return _SubTypeForSaleWithoutStock; }
            set { _SubTypeForSaleWithoutStock = value; }
        }

        public static string AccountStatementPurchase
        {
            get { return _AccountStatementPurchase; }
            set { _AccountStatementPurchase = value; }
        }

        public static string AccountStatementSale
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

        #endregion Properties

        private static void Initialize()
        {
            _AccountStockInOut = "50001";
            _AccountStatementPurchase = "50002";
            _AccountStatementSale = "50003";
            _AccountCash = "26001";

            _AccountCashSale = "30001";
            _AccountCreditSale = "30002";
            _AccountCashCreditSale = "30003";
            _AccountVoucherSale = "30004";
            _AccountSalesReturn = "30005";
            _AccountCashDiscountSale = "30006";
            _AccountPMTDiscountSale = "30014";
            _AccountItemDiscountSale = "30015";

            _AccountDebitNoteSale = "30007";
            _AccountAddonSale = "30008";
            _AccountVatOutput5Sale = "30009";
            _AccountVatOutput12point5Sale = "30010";
            _AccountRoundoffSale = "30011";
            _AccountVATZeroSale = "30012";
            _AccountPendingCashBills = "30013";

            _AccountCashPurchase = "40001";
            _AccountCreditPurchase = "40002";
            _AccountCashCreditPurchase = "40003";
            _AccountPurchaseReturn = "40004";
            _AccountCashDiscountPurchase = "40005";
            _AccountSplDiscountPurchase = "40006";
            _AccountSchemeDiscountPurchcase = "40007";
            _AccountItemDiscountPurchase = "40008";
            _AccountDebitNotePurchase = "40009";
            _AccountCreditNotePurchase = "40010";
            _AccountAddonOctroiHamaliPurchase = "40011";
            _AccountVatInput5Purchase = "40012";
            _AccountVatInput12point5Purchase = "40013";
            _AccountRoundoffPurchase = "40014";
            _AccountVATZeroPurchase = "40015";

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

            _VoucherTypeForCashPurchase = "PCA"; //4
            _VoucherTypeForCreditPurchase = "PCR";   //3
            _VoucherTypeForCreditStatementPurchase = "PCT";  //2

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

            _VoucherTypeForOpeningStock = "OPS"; //01
            _VoucherTypeForPurchaseOrder = "POR";   // 19
            _VoucherTypeForCorrectionInRate = "CIR"; //17
            _VoucherTypeForJournalEntry = "JVE";  // 14
            _VoucherTypeForChequeReturn = "CQR";  // 18
            _VoucherTypeForStatementPurchase = "PST";  // 16
            _VoucherTypeForStatementSale = "SST"; // 15
            _VoucherTypeForStatementHospital = "HST"; // 34

            _VoucherTypeForDistributorSaleCash = "WCA";   // 26
            _VoucherTypeForDistributorSaleCredit = "WCR";  // 27
            _VoucherTypeForDistributorSaleCreditStatement = "WCT"; // 28


            _GroupCodeForBank = "23"; 
            _GroupCodeForCreditor = "31";
            _GroupCodeForDebtor = "28";
            _GroupCodeForOtherCreditor = "101";
            _GroupCodeForOtherDebtor = "102";


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
            _SubTypeForCreditCardSale = "C";
            _SubTypeForDebtorSale = "D";
            _SubTypeForDistributorSale = "T";
            _SubTypeForPatientSale = "P";
            _SubTypeForVoucherSale = "V";
            _SubTypeForHospitalSale = "H";
            _SubTypeForInstitutionalSale = "I";
            _SubTypeForSaleWithoutStock = "W";

            _TransactionTypeForCash = "Cash";
            _TransactionTypeForCredit = "Credit";
            _TransactionTypeForCreditCard = "CreditCard";
            _TransactionTypeForCreditStatement = "CreditStatement";
            _TransactionTypeForVoucher = "Voucher";
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
