using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.Common;
using EcoMart.DataLayer;

namespace EcoMart.BusinessLayer
{
    public class Settings : BaseObject
    {
        #region Declaration

        private string _MsetPurchaseRounding;
        //   private string _MsetPurchaseIfCreditStatementPurchase;
        //  private string _MsetPurchaseAddVATInPurchaseRate;
        //  private string _MsetPurchaseAddVATInSaleRate;
        private string _MsetPurchaseReadPurchaseOrder;
        //    private string _MsetPurchaseGetPendingScheme;
        //     private string _MsetPurchaseIfProductWithOctroi;
        //    private string _MsetPurchaseOctroionZeroVAT;
        private string _MsetPurchaseChangeSaleRate;
        //     private string _MsetPurchaseMarginByPurchaseRate;
        private string _MsetPurchaseAcceptExpriedItems;
        //     private string _MsetPurchaseIncludeCreditPurchaseInStatements;
        private string _MsetPurchaseUpdateVATInMaster;
        private string _MsetPurchaseCopyPurchaseOrder;
        private string _MsetPurchaseHold;
        private string _MsetPurchaseIfPTR;

        //
        private string _MsetSaleCreditSale;
        private string _MsetSaleAskDiscountinCounterSale;
        private string _MsetSaleAskRoundinginSale;
        private string _MsetSaleRoundOff;
        private string _MsetSaleShowProfitInSaleBill;
        private string _MsetSaleIPDOPD;
        private string _MsetSaleDiscountWithoutVAT;
        private string _MsetSaleIncludeCreditSaleInStatements;
        private string _MsetSaleSaveCustomerInMaster;
        private string _MsetSaleShowOnlyMRPInCounterSale;
        private string _MsetSaleAllowDistributorSale;
        private string _MsetSaleRoundingTo10Paise;
        private string _MsetSaleRoundingToPreviousRupee;
        private string _MsetSaleOnlyCashSaleInCounterSale;
        private string _MsetSaleEditRateInCounterSale;
        private string _MsetSaleAllowNegetiveStock;
        private string _MsetSaleAllowBackDate;
        private string _MsetSaleSelectVATPercent;
        private string _MsetSaleProductRefreshButton;
        private double _MsetSaleMaxDiscount;
        private double _MsetOpeningStockGetPercent;
        private string _MsetSaleCounterSaleSingleGrid;

        private string _MsetSaleTenderAmount;


        private string _MsetSaleGetDelivaryBoy;
        private string _MsetSaleGetDoctor;
        private string _MsetSaleGetOrderNumberDate;

        private string _MsetCashBankShowDiscount;   //Amar
        private string _MsetAllowPrintMessage; //Amar

        private string _MsetRemoveCodeFormCreditNote;
        private string _MsetAllowPendingCashMemo;


        //   private string _MsetGeneralPrintPlainPaper;
        private string _MsetGeneralProfitPercentageByPurchaseRate;
        private string _MsetGeneralExpiryLast;
        private string _MsetGeneralBatchNumberRequired;
        private string _MsetGeneralExpiryDateReuired;
        private string _MsetGeneralAskDatesInSearch;
        private string _MsetGeneralAlphabetical;


        private string _MsetPrintSaleBill;
        private string _MsetPrintCRDBNote;
        private string _MsetPrintCashBankVoucher;
        private string _MsetPrintPO;
        private string _MsetPrintFontSize;
        private string _MsetPrintFontName;
        private string _MsetPrintFixNumberOfLines;

        private string _MsetSortOrder;

        // Operators
        private string _MsetAskOperatorVoucherSale;
        private string _MsetAskOperatorOtherThanVoucherSale;
        private string _MsetAskOperatorPurchase;
        private string _MsetAskOperatorCRDB;
        private string _MsetAskOperatorOpeningStock;
        private string _MsetAskOperatorCorrectionRate;
        private string _MsetAskOperatorJV;
        private string _MsetAskOperatorCashBankReceipt;
        private string _MsetAskOperatorCashBankPayment;

        private int _MsetNumberOfLinesSaleBill;
        private int _MsetNumberOfLinesPerPage;

        private double _MsetGSTSPercent;

        private string _SmsSetPatientSale;
        private string _SmsSetDebtorSale;
        private string _SmsSetCreditCardSale;
        private string _SmsSetInstitutionalSale;
        private string _SmsSetBankPaymentSale;
        private string _SmsSetBankReceiptSale;
        private string _SmsSetCashPaymentSale;
        private string _SmsSetCashReceiptSale;


        private int _MsetNumberOfBillsAtaTime;

        private int _MsetPrinterType;

        private string _MsetScanBarCode;

        private string _MsetSaleAllowSpecialDiscount;
        private double _MsetSpecialDiscount1;
        private double _MsetSpecialDiscount2;
        private double _MsetSpecialDiscount3;



        // Email


        private string _MsetEmailID;
        private string _MsetEmailPassword;
        private string _MsetEmailType;

        private string _MsetFixedNarration;

        private string _MsetCreditNoteDefaultTransferToAccount;
        private string _MsetCreditNoteReturnRateDisable;
        private string _MsetCreditNoteDoNotShowPurchaseRate;

        // Report

        private string _MsetReportSaleDailySaleDoNotShowTotal;
        private string _MsetReportSaleDailyProductsDoNotShowTotal;

        private int _MsetProductNameWidthInSaleInvoice;
        #endregion

        #region Constructors, Destructors
        public Settings()
        {
            try
            {
                Initialise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Constructor

        # region properties

        // Report
        public string MsetReportSaleDailySaleDoNotShowTotal
        {
            get { return _MsetReportSaleDailySaleDoNotShowTotal; }
            set { _MsetReportSaleDailySaleDoNotShowTotal = value; }
        }
        public string MsetReportSaleDailyProductsDoNotShowTotal
        {
            get { return _MsetReportSaleDailyProductsDoNotShowTotal; }
            set { _MsetReportSaleDailyProductsDoNotShowTotal = value; }
        }
        public int MsetProductNameWidthInSaleInvoice
        {
            get { return _MsetProductNameWidthInSaleInvoice; }
            set { _MsetProductNameWidthInSaleInvoice = value; }
        }

        // Credit Note

        public string MsetCreditNoteDefaultTransferToAccount
        {
            get { return _MsetCreditNoteDefaultTransferToAccount; }
            set { _MsetCreditNoteDefaultTransferToAccount = value; }
        }
        public string MsetCreditNoteReturnRateDisable
        {
            get { return _MsetCreditNoteReturnRateDisable; }
            set { _MsetCreditNoteReturnRateDisable = value; }
        }
        public string MsetCreditNoteDoNotShowPurchaseRate
        {
            get { return _MsetCreditNoteDoNotShowPurchaseRate; }
            set { _MsetCreditNoteDoNotShowPurchaseRate = value; }
        }
        public string MsetFixedNarration
        {
            get { return _MsetFixedNarration; }
            set { _MsetFixedNarration = value; }
        }

        // Credit Note
        public string MsetRemoveCodeFromCreditNote
        {
            get { return _MsetRemoveCodeFormCreditNote; }
            set { _MsetRemoveCodeFormCreditNote = value; }
        }

        public string MsetAllowPendingCashMemo
        {
            get { return _MsetAllowPendingCashMemo; }
            set { _MsetAllowPendingCashMemo = value; }
        }
        // Print

        public string MsetPrintSaleBill
        {
            get { return _MsetPrintSaleBill; }
            set { _MsetPrintSaleBill = value; }
        }
        public string MsetPrintCRDBNote
        {
            get { return _MsetPrintCRDBNote; }
            set { _MsetPrintCRDBNote = value; }
        }
        public string MsetPrintCashBankVoucher
        {
            get { return _MsetPrintCashBankVoucher; }
            set { _MsetPrintCashBankVoucher = value; }
        }
        public string MsetPrintPO
        {
            get { return _MsetPrintPO; }
            set { _MsetPrintPO = value; }
        }
        public string MsetPrintFontName
        {
            get { return _MsetPrintFontName; }
            set { _MsetPrintFontName = value; }
        }
        public string MsetPrintFontSize
        {
            get { return _MsetPrintFontSize; }
            set { _MsetPrintFontSize = value; }
        }
        public string MsetPrintFixNumberOfLines
        {
            get { return _MsetPrintFixNumberOfLines; }
            set { _MsetPrintFixNumberOfLines = value; }
        }
        // Purchase
        public string MsetPurchaseCopyPurchaseOrder
        {
            get { return _MsetPurchaseCopyPurchaseOrder; }
            set { _MsetPurchaseCopyPurchaseOrder = value; }
        }
        public string MsetPurchaseRounding
        {
            get { return _MsetPurchaseRounding; }
            set { _MsetPurchaseRounding = value; }
        }
        public string MsetPurchaseHold
        {
            get { return _MsetPurchaseHold; }
            set { _MsetPurchaseHold = value; }
        }
        public string MsetPurchaseIfPTR
        {
            get { return _MsetPurchaseIfPTR; }
            set { _MsetPurchaseIfPTR = value; }
        }
        //public string MsetPurchaseIfCreditStatementPurchase
        //{
        //    get { return _MsetPurchaseIfCreditStatementPurchase; }
        //    set { _MsetPurchaseIfCreditStatementPurchase = value; }
        //}
        //public string MsetPurchaseAddVATInPurchaseRate
        //{
        //    get { return _MsetPurchaseAddVATInPurchaseRate; }
        //    set { _MsetPurchaseAddVATInPurchaseRate = value; }
        //}
        //public string MsetPurchaseAddVATInSaleRate
        //{
        //    get { return _MsetPurchaseAddVATInSaleRate; }
        //    set { _MsetPurchaseAddVATInSaleRate = value; }
        //}
        public string MsetPurchaseReadPurchaseOrder
        {
            get { return _MsetPurchaseReadPurchaseOrder; }
            set { _MsetPurchaseReadPurchaseOrder = value; }
        }
        //public string MsetPurchaseGetPendingScheme
        //{
        //    get { return _MsetPurchaseGetPendingScheme; }
        //    set { _MsetPurchaseGetPendingScheme = value; }
        //}
        //public string MsetPurchaseIfProductWithOctroi
        //{
        //    get { return _MsetPurchaseIfProductWithOctroi; }
        //    set { _MsetPurchaseIfProductWithOctroi = value; }
        //}
        //public string MsetPurchaseOctroionZeroVAT
        //{
        //    get { return _MsetPurchaseOctroionZeroVAT; }
        //    set { _MsetPurchaseOctroionZeroVAT = value; }
        //}

        public string MsetPurchaseChangeSaleRate
        {
            get { return _MsetPurchaseChangeSaleRate; }
            set { _MsetPurchaseChangeSaleRate = value; }
        }

        //public string MsetPurchaseMarginByPurchaseRate
        //{
        //    get { return _MsetPurchaseMarginByPurchaseRate; }
        //    set { _MsetPurchaseMarginByPurchaseRate = value; }
        //}
        public string MsetPurchaseAcceptExpriedItems
        {
            get { return _MsetPurchaseAcceptExpriedItems; }
            set { _MsetPurchaseAcceptExpriedItems = value; }
        }
        //public string MsetPurchaseIncludeCreditPurchaseInStatements
        //{
        //    get { return _MsetPurchaseIncludeCreditPurchaseInStatements; }
        //    set { _MsetPurchaseIncludeCreditPurchaseInStatements = value; }
        //}

        public string MsetPurchaseUpdateVATInMaster
        {
            get { return _MsetPurchaseUpdateVATInMaster; }
            set { _MsetPurchaseUpdateVATInMaster = value; }
        }
        // Sale
        public string MsetSaleRoundOff
        {
            get { return _MsetSaleRoundOff; }
            set { _MsetSaleRoundOff = value; }
        }
        public string MsetSaleAllowNegativeStock
        {
            get { return _MsetSaleAllowNegetiveStock; }
            set { _MsetSaleAllowNegetiveStock = value; }

        }
        public string MsetSaleAllowBackDate
        {
            get { return _MsetSaleAllowBackDate; }
            set { _MsetSaleAllowBackDate = value; }

        }
        public string MsetSaleCounterSaleSingleGrid
        {
            get { return _MsetSaleCounterSaleSingleGrid; }
            set { _MsetSaleCounterSaleSingleGrid = value; }
        }
        public string MsetSaleTenderAmount
        {
            get { return _MsetSaleTenderAmount; }
            set { _MsetSaleTenderAmount = value; }
        }


        public string MsetSaleGetDelivaryBoy
        {
            get { return _MsetSaleGetDelivaryBoy; }
            set { _MsetSaleGetDelivaryBoy = value; }
        }

        public string MsetSaleGetDoctor
        {
            get { return _MsetSaleGetDoctor; }
            set { _MsetSaleGetDoctor = value; }
        }

        public string MsetSaleGetOrderNumberDate
        {
            get { return _MsetSaleGetOrderNumberDate; }
            set { _MsetSaleGetOrderNumberDate = value; }
        }
        public string MsetCashBankShowDiscount
        {
            get { return _MsetCashBankShowDiscount; }
            set { _MsetCashBankShowDiscount = value; }
        }

        public string MsetAllowPrintMessage
        {
            get { return _MsetAllowPrintMessage; }
            set { _MsetAllowPrintMessage = value; }
        }
        public string MsetSaleSelectVATPercent
        {
            get { return _MsetSaleSelectVATPercent; }
            set { _MsetSaleSelectVATPercent = value; }
        }
        public string MsetSaleCreditSale
        {
            get { return _MsetSaleCreditSale; }
            set { _MsetSaleCreditSale = value; }
        }
        public string MsetSaleAskDiscountinCounterSale
        {
            get { return _MsetSaleAskDiscountinCounterSale; }
            set { _MsetSaleAskDiscountinCounterSale = value; }
        }

        public string MsetSaleAskRoundinginSale
        {
            get { return _MsetSaleAskRoundinginSale; }
            set { _MsetSaleAskRoundinginSale = value; }
        }

        public string MsetSaleShowProfitInSaleBill
        {
            get { return _MsetSaleShowProfitInSaleBill; }
            set { _MsetSaleShowProfitInSaleBill = value; }
        }
        public string MsetSaleIPDOPD
        {
            get { return _MsetSaleIPDOPD; }
            set { _MsetSaleIPDOPD = value; }
        }
        public string MsetSaleDiscountWithoutVAT
        {
            get { return _MsetSaleDiscountWithoutVAT; }
            set { _MsetSaleDiscountWithoutVAT = value; }
        }

        public string MsetSaleIncludeCreditSaleInStatements
        {
            get { return _MsetSaleIncludeCreditSaleInStatements; }
            set { _MsetSaleIncludeCreditSaleInStatements = value; }
        }
        public string MsetSaleSaveCustomerInMaster
        {
            get { return _MsetSaleSaveCustomerInMaster; }
            set { _MsetSaleSaveCustomerInMaster = value; }
        }


        public string MsetSaleShowOnlyMRPInCounterSale
        {
            get { return _MsetSaleShowOnlyMRPInCounterSale; }
            set { _MsetSaleShowOnlyMRPInCounterSale = value; }
        }
        public string MsetSaleAllowDistributorSale
        {
            get { return _MsetSaleAllowDistributorSale; }
            set { _MsetSaleAllowDistributorSale = value; }

        }

        public string MsetSaleRoundingTo10Paise
        {
            get { return _MsetSaleRoundingTo10Paise; }
            set { _MsetSaleRoundingTo10Paise = value; }

        }
        public string MsetSaleRoundingToPreviousRupee
        {
            get { return _MsetSaleRoundingToPreviousRupee; }
            set { _MsetSaleRoundingToPreviousRupee = value; }
        }
        public string MsetSaleOnlyCashSaleInCounterSale
        {
            get { return _MsetSaleOnlyCashSaleInCounterSale; }
            set { _MsetSaleOnlyCashSaleInCounterSale = value; }

        }


        public string MsetSaleEditRateInCounterSale
        {
            get { return _MsetSaleEditRateInCounterSale; }
            set { _MsetSaleEditRateInCounterSale = value; }

        }


        public string MsetSaleProductRefreshButton
        {
            get { return _MsetSaleProductRefreshButton; }
            set { _MsetSaleProductRefreshButton = value; }
        }

        public double MsetSaleMaxDiscount
        {
            get { return _MsetSaleMaxDiscount; }
            set { _MsetSaleMaxDiscount = value; }
        }
        public double MsetOpeningStockGetPercent
        {
            get { return _MsetOpeningStockGetPercent; }
            set { _MsetOpeningStockGetPercent = value; }
        }
        // General
        public string MsetGeneralProfitPercentageByPurchaseRate
        {
            get { return _MsetGeneralProfitPercentageByPurchaseRate; }
            set { _MsetGeneralProfitPercentageByPurchaseRate = value; }
        }
        public string MsetGeneralExpiryLast
        {
            get { return _MsetGeneralExpiryLast; }
            set { _MsetGeneralExpiryLast = value; }
        }
        public string MsetGeneralBatchNumberRequired
        {
            get { return _MsetGeneralBatchNumberRequired; }
            set { _MsetGeneralBatchNumberRequired = value; }
        }
        public string MsetGeneralExpiryDateReuired
        {
            get { return _MsetGeneralExpiryDateReuired; }
            set { _MsetGeneralExpiryDateReuired = value; }
        }
        public string MsetGeneralAskDatesInSearch
        {
            get { return _MsetGeneralAskDatesInSearch; }
            set { _MsetGeneralAskDatesInSearch = value; }
        }

        public string MsetGeneralAlphabetical
        {
            get { return _MsetGeneralAlphabetical; }
            set { _MsetGeneralAlphabetical = value; }
        }

        // operators
        public string MsetAskOperatorVoucherSale
        {
            get { return _MsetAskOperatorVoucherSale; }
            set { _MsetAskOperatorVoucherSale = value; }
        }
        public string MsetAskOperatorOtherThanVoucherSale
        {
            get { return _MsetAskOperatorOtherThanVoucherSale; }
            set { _MsetAskOperatorOtherThanVoucherSale = value; }
        }
        public string MsetAskOperatorPurchase
        {
            get { return _MsetAskOperatorPurchase; }
            set { _MsetAskOperatorPurchase = value; }
        }
        public string MsetAskOperatorCRDB
        {
            get { return _MsetAskOperatorCRDB; }
            set { _MsetAskOperatorCRDB = value; }
        }
        public string MsetAskOperatorOpeningStock
        {
            get { return _MsetAskOperatorOpeningStock; }
            set { _MsetAskOperatorOpeningStock = value; }
        }
        public string MsetAskOperatorCorrectionRate
        {
            get { return _MsetAskOperatorCorrectionRate; }
            set { _MsetAskOperatorCorrectionRate = value; }
        }
        public string MsetAskOperatorJV
        {
            get { return _MsetAskOperatorJV; }
            set { _MsetAskOperatorJV = value; }
        }
        public string MsetAskOperatorCashBankReceipt
        {
            get { return _MsetAskOperatorCashBankReceipt; }
            set { _MsetAskOperatorCashBankReceipt = value; }
        }
        public string MsetAskOperatorCashBankPayment
        {
            get { return _MsetAskOperatorCashBankPayment; }
            set { _MsetAskOperatorCashBankPayment = value; }
        }
        public int MsetNumberOfLinesSaleBill
        {
            get { return _MsetNumberOfLinesSaleBill; }
            set { _MsetNumberOfLinesSaleBill = value; }
        }

        public string SmsSetPatientSale  //Amar
        {
            get { return _SmsSetPatientSale; }
            set { _SmsSetPatientSale = value; }
        }
        public string SmsSetDebtorSale
        {
            get { return _SmsSetDebtorSale; }
            set { _SmsSetDebtorSale = value; }
        }
        public string SmsSetCreditCardSale
        {
            get { return _SmsSetCreditCardSale; }
            set { _SmsSetCreditCardSale = value; }
        }
        public string SmsSetInstitutionalSale
        {
            get { return _SmsSetInstitutionalSale; }
            set { _SmsSetInstitutionalSale = value; }
        }
        public string SmsSetBankPaymentSale
        {
            get { return _SmsSetBankPaymentSale; }
            set { _SmsSetBankPaymentSale = value; }
        }
        public string SmsSetBankReceiptSale
        {
            get { return _SmsSetBankReceiptSale; }
            set { _SmsSetBankReceiptSale = value; }
        }
        public string SmsSetCashPaymentSale
        {
            get { return _SmsSetCashPaymentSale; }
            set { _SmsSetCashPaymentSale = value; }
        }
        public string SmsSetCashReceiptSale
        {
            get { return _SmsSetCashReceiptSale; }
            set { _SmsSetCashReceiptSale = value; }
        }


        public int MsetNumberOfLinesPerPage   // by [ansuman]
        {
            get { return _MsetNumberOfLinesPerPage; }
            set { _MsetNumberOfLinesPerPage = value; }
        }

        public int MsetNumberOfBillsAtaTime
        {
            get { return _MsetNumberOfBillsAtaTime; }
            set { _MsetNumberOfBillsAtaTime = value; }
        }
        public string MsetScanBarCode
        {
            get { return _MsetScanBarCode; }
            set { _MsetScanBarCode = value; }
        }

        public int MsetPrinterType
        {
            get { return _MsetPrinterType; }
            set { _MsetPrinterType = value; }
        }

        // Email

        public string MsetEmailID
        {
            get { return _MsetEmailID; }
            set { _MsetEmailID = value; }
        }
        public string MsetEmailPassword
        {
            get { return _MsetEmailPassword; }
            set { _MsetEmailPassword = value; }
        }
        public string MsetEmailType
        {
            get { return _MsetEmailType; }
            set { _MsetEmailType = value; }
        }

        public double MsetSpecialDiscount1
        {
            get { return _MsetSpecialDiscount1; }
            set { _MsetSpecialDiscount1 = value; }
        }
        public double MsetSpecialDiscount2
        {
            get { return _MsetSpecialDiscount2; }
            set { _MsetSpecialDiscount2 = value; }
        }
        public double MsetSpecialDiscount3
        {
            get { return _MsetSpecialDiscount3; }
            set { _MsetSpecialDiscount3 = value; }
        }
        public string MsetSaleAllowSpecialDiscount
        {
            get { return _MsetSaleAllowSpecialDiscount; }
            set { _MsetSaleAllowSpecialDiscount = value; }
        }
        public double MsetGSTSPercent
        {
            get { return _MsetGSTSPercent; }
            set { _MsetGSTSPercent = value; }
        }
        // Sort Order
        public string MsetSortOrder
        {
            get { return _MsetSortOrder; }
            set { _MsetSortOrder = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                InitializeSettings();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void InitializeSettings()
        {
            _MsetPurchaseCopyPurchaseOrder = "N";
            _MsetPurchaseRounding = "Y";
            _MsetPurchaseReadPurchaseOrder = "N";
            _MsetPurchaseChangeSaleRate = "N";
            _MsetPurchaseAcceptExpriedItems = "N";
            _MsetPurchaseHold = "N";
            _MsetPurchaseIfPTR = "N";
            //                
            _MsetSaleRoundOff = "N";
            _MsetSaleCreditSale = "N";
            _MsetSaleAllowBackDate = "N";
            _MsetSaleTenderAmount = "N";
            _MsetSaleShowProfitInSaleBill = "N";
            _MsetSaleMaxDiscount = 10;
            _MsetSaleGetOrderNumberDate = "N";
            _MsetSaleGetDoctor = "N";
            _MsetSaleGetDelivaryBoy = "N";

            _MsetOpeningStockGetPercent = 16.00;
            //
            _MsetGeneralExpiryLast = "N";
            _MsetGeneralProfitPercentageByPurchaseRate = "Y";
            _MsetGeneralBatchNumberRequired = "Y";
            _MsetGeneralExpiryDateReuired = "N";
            _MsetGeneralAskDatesInSearch = "N";
            _MsetGeneralAlphabetical = "Y";
            //
            _MsetPrintSaleBill = "N";
            _MsetPrintCRDBNote = "Y";
            _MsetPrintCashBankVoucher = "Y";
            _MsetPrintPO = "Y";
            _MsetPrintFontSize = "8";
            _MsetPrintFontName = "Arial";
            _MsetPrintFixNumberOfLines = "N";
            //
            _MsetAskOperatorVoucherSale = "N";
            _MsetAskOperatorOtherThanVoucherSale = "N";
            _MsetAskOperatorPurchase = "N";
            _MsetAskOperatorCRDB = "N";
            _MsetAskOperatorOpeningStock = "N";
            _MsetAskOperatorCorrectionRate = "N";
            _MsetAskOperatorJV = "N";
            _MsetAskOperatorCashBankReceipt = "N";
            _MsetAskOperatorCashBankPayment = "N";

            _MsetNumberOfLinesSaleBill = 7;
            _MsetNumberOfLinesPerPage = 14; // [ansuman]
            _MsetNumberOfBillsAtaTime = 1;
            _MsetPrinterType = 1;
            _MsetScanBarCode = "N";

            _MsetSaleAllowSpecialDiscount = "N";
            _MsetSpecialDiscount1 = 0;
            _MsetSpecialDiscount2 = 0;
            _MsetSpecialDiscount3 = 0;

            _MsetRemoveCodeFormCreditNote = "N";
            _MsetAllowPendingCashMemo = "N";

            _MsetFixedNarration = "";

            _MsetCreditNoteDefaultTransferToAccount = "N";
            _MsetCreditNoteReturnRateDisable = "N";
            _MsetCreditNoteDoNotShowPurchaseRate = "N";
            //
            _MsetReportSaleDailySaleDoNotShowTotal = "N";
            _MsetReportSaleDailyProductsDoNotShowTotal = "N";
            _MsetCashBankShowDiscount = "Y";

            _SmsSetPatientSale = "N";
            _SmsSetDebtorSale = "N";
            _SmsSetCreditCardSale = "N";
            _SmsSetInstitutionalSale = "N";
            _SmsSetBankPaymentSale = "N";
            _SmsSetBankReceiptSale = "N";
            _SmsSetCashPaymentSale = "N";
            _SmsSetCashReceiptSale = "N";

            _MsetSortOrder = FixAccounts.SortByNone;
            _MsetProductNameWidthInSaleInvoice = 20;
            _MsetGSTSPercent = 50;
        }

        #endregion  Internal Methods

        #region Public Methods

        public void FillSettings()
        {
            try
            {
                DataRow dr;
                InitializeSettings();
                DBSettings set = new DBSettings();
                dr = set.GetOverviewData(General.ShopDetail.ShopVoucherSeries);
                if (dr != null)
                {
                    //purchase
                    if (dr["setPurchaseCopyPurchaseOrder"] != DBNull.Value)
                        MsetPurchaseCopyPurchaseOrder = dr["setPurchaseCopyPurchaseOrder"].ToString();
                    if (dr["setPurchaseRounding"] != DBNull.Value)
                        MsetPurchaseRounding = dr["setPurchaseRounding"].ToString();
                    if (dr["setPurchaseReadPurchaseOrder"] != DBNull.Value)
                        MsetPurchaseReadPurchaseOrder = dr["setPurchaseReadPurchaseOrder"].ToString();
                    if (dr["setPurchaseChangeSaleRate"] != DBNull.Value)
                        MsetPurchaseChangeSaleRate = dr["setPurchaseChangeSaleRate"].ToString();
                    if (dr["setPurchaseAllowExpiredItems"] != DBNull.Value)
                        MsetPurchaseAcceptExpriedItems = dr["setPurchaseAllowExpiredItems"].ToString();
                    if (dr["setPurchaseHold"] != DBNull.Value)
                        MsetPurchaseHold = dr["setPurchaseHold"].ToString();
                    if (dr["setPurchaseIfPTR"] != DBNull.Value)
                        MsetPurchaseIfPTR = dr["setPurchaseIfPTR"].ToString();

                    // sale 
                    if (dr["setSaleRoundOff"] != DBNull.Value)
                        MsetSaleRoundOff = dr["setSaleRoundOff"].ToString();
                    if (dr["setSaleCreditStatement"] != DBNull.Value)
                        MsetSaleCreditSale = dr["setSaleCreditStatement"].ToString();                    
                    if (dr["setSaleAllowBackDate"] != DBNull.Value)
                        MsetSaleAllowBackDate = dr["setSaleAllowBackDate"].ToString();
                    if (dr["setSaleTenderAmount"] != DBNull.Value)
                        MsetSaleTenderAmount = dr["setSaleTenderAmount"].ToString();
                    if (dr["setSaleShowProfitInSaleBill"] != DBNull.Value)
                        MsetSaleShowProfitInSaleBill = dr["setSaleShowProfitInSaleBill"].ToString();
                    if (dr["setSaleMaxDiscount"] != DBNull.Value && dr["setSaleMaxDiscount"].ToString() != string.Empty)
                        MsetSaleMaxDiscount = Convert.ToDouble(dr["setSaleMaxDiscount"].ToString());

                    if (dr["setSaleGetOrderNumberDate"] != DBNull.Value)
                        MsetSaleGetOrderNumberDate = dr["setSaleGetOrderNumberDate"].ToString();
                    if (dr["setSaleGetDoctor"] != DBNull.Value)
                        MsetSaleGetDoctor = dr["setSaleGetDoctor"].ToString();
                    if (dr["setSaleGetDelivaryBoy"] != DBNull.Value)
                        MsetSaleGetDelivaryBoy = dr["setSaleGetDelivaryBoy"].ToString();

                    //cashbank
                    //if (dr["setCashBankShowDiscount"] != DBNull.Value)
                    //    MsetCashBankShowDiscount = dr["setCashBankShowDiscount"].ToString();
                    ////sms
                    //if (dr["SmsDebtorSaleSet"] != DBNull.Value)
                    //    SmsSetDebtorSale = dr["SmsDebtorSaleSet"].ToString();
                    //if (dr["SmsBankPaymentSet"] != DBNull.Value)
                    //    SmsSetBankPaymentSale = dr["SmsBankPaymentSet"].ToString();
                    //if (dr["SmsBankReceiptSet"] != DBNull.Value)
                    //    SmsSetBankReceiptSale = dr["SmsBankReceiptSet"].ToString();
                    //if (dr["SmsCashReceiptSet"] != DBNull.Value)
                    //    SmsSetCashReceiptSale = dr["SmsCashReceiptSet"].ToString();
                    //if (dr["SmsCashPaymentSet"] != DBNull.Value)
                    //    SmsSetCashPaymentSale = dr["SmsCashPaymentSet"].ToString();


                    //
                    //if (dr["setOpeningStockGetPercent"] != DBNull.Value && dr["setOpeningStockGetPercent"].ToString() != string.Empty)
                    //    MsetOpeningStockGetPercent = Convert.ToDouble(dr["setOpeningStockGetPercent"].ToString());
                    ////general

                    //if (dr["setGeneralExpiryLast"] != DBNull.Value)
                    //    MsetGeneralExpiryLast = dr["setGeneralExpiryLast"].ToString();
                    //if (dr["setGeneralBatchNumberRequired"] != DBNull.Value)
                    //    MsetGeneralBatchNumberRequired = dr["setGeneralBatchNumberRequired"].ToString();
                    //if (dr["setGeneralExpiryDateRequired"] != DBNull.Value)
                    //    MsetGeneralExpiryDateReuired = dr["setGeneralExpiryDateRequired"].ToString();
                    //if (dr["setGeneralAskDatesInSearch"] != DBNull.Value)
                    //    MsetGeneralAskDatesInSearch = dr["setGeneralAskDatesInSearch"].ToString();
                    //if (dr["setGeneralAlphabetical"] != DBNull.Value)
                    //    MsetGeneralAlphabetical = dr["setGeneralAlphabetical"].ToString();
                    //if (dr["setAskOperatorOtherThanVoucherSale"] != DBNull.Value)
                    //    MsetAskOperatorOtherThanVoucherSale = dr["setAskOperatorOtherThanVoucherSale"].ToString();
                    //if (dr["setAskOperatorVoucherSale"] != DBNull.Value)
                    //    MsetAskOperatorVoucherSale = dr["setAskOperatorVoucherSale"].ToString();
                    //if (dr["setScanBarCode"] != DBNull.Value)
                    //    MsetScanBarCode = dr["setScanBarCode"].ToString();

                    //if (dr["setPrintSettingYesNo"] != DBNull.Value)
                    //    _MsetAllowPrintMessage = dr["setPrintSettingYesNo"].ToString();
                    //if (dr["setPrintSaleBillPrintedPaper"] != DBNull.Value)
                    //    MsetPrintSaleBill = dr["setPrintSaleBillPrintedPaper"].ToString();
                    //if (dr["setPrintCRDBNotePrintedPaper"] != DBNull.Value)
                    //    MsetPrintCRDBNote = dr["setPrintCRDBNotePrintedPaper"].ToString();
                    //if (dr["setPrintCashBankVoucherPrintedPaper"] != DBNull.Value)
                    //    MsetPrintCashBankVoucher = dr["setPrintCashBankVoucherPrintedPaper"].ToString();
                    //if (dr["setPrintPurchaseOrderPrintedPaper"] != DBNull.Value)
                    //    MsetPrintPO = dr["setPrintPurchaseOrderPrintedPaper"].ToString();
                    //if (dr["setPrintFontName"] != DBNull.Value)
                    //    MsetPrintFontName = dr["setPrintFontName"].ToString();
                    //if (dr["setPrintFontSize"] != DBNull.Value)
                    //    _MsetPrintFontSize = dr["setPrintFontSize"].ToString();
                    //if (dr["setPrintFixNumberOfLines"] != DBNull.Value)
                    //    _MsetPrintFixNumberOfLines = dr["setPrintFixNumberOfLines"].ToString();
                    //if (dr["setNumberOfLinesSaleBill"] != DBNull.Value)
                    //    MsetNumberOfLinesSaleBill = Convert.ToInt32(dr["setNumberOfLinesSaleBill"].ToString());
                    //if (dr["setNumberOfBillsAtaTime"] != DBNull.Value)
                    //    MsetNumberOfBillsAtaTime = Convert.ToInt32(dr["setNumberOfBillsAtaTime"].ToString());
                    //if (dr["setPrinterType"] != DBNull.Value)
                    //    MsetPrinterType = Convert.ToInt32(dr["setPrinterType"].ToString());
                    //if (dr["setPrintFontName"] != DBNull.Value)
                    //    MsetPrintFontName = dr["setPrintFontName"].ToString();
                    //if (dr["setPrintFontSize"] != DBNull.Value)
                    //    MsetPrintFontSize = dr["setPrintFontSize"].ToString();

                    //if (dr["SpecialDiscount1"] != DBNull.Value)
                    //    MsetSpecialDiscount1 = Convert.ToDouble(dr["SpecialDiscount1"].ToString());
                    //if (dr["SpecialDiscount2"] != DBNull.Value)
                    //    MsetSpecialDiscount2 = Convert.ToDouble(dr["SpecialDiscount2"].ToString());
                    //if (dr["SpecialDiscount3"] != DBNull.Value)
                    //    MsetSpecialDiscount3 = Convert.ToDouble(dr["SpecialDiscount3"].ToString());



                    //if (dr["setEmailID"] != DBNull.Value)
                    //    MsetEmailID = dr["setEmailID"].ToString();
                    //if (dr["setEmailPassword"] != DBNull.Value)
                    //    MsetEmailPassword = dr["setEmailPassword"].ToString();
                    //if (dr["SetEmailType"] != DBNull.Value)
                    //    MsetEmailType = dr["SetEmailType"].ToString();

                    //if (dr["setFixedNarration"] != DBNull.Value)
                    //    MsetFixedNarration = dr["setFixedNarration"].ToString();

                    //if (dr["setCreditNoteDefaultTransferToAccount"] != DBNull.Value)
                    //    MsetCreditNoteDefaultTransferToAccount = dr["setCreditNoteDefaultTransferToAccount"].ToString();
                    //if (dr["setCreditNoteReturnRateDisable"] != DBNull.Value)
                    //    MsetCreditNoteReturnRateDisable = dr["setCreditNoteReturnRateDisable"].ToString();
                    //if (dr["setCreditNoteDoNotShowPurchaseRate"] != DBNull.Value)
                    //    MsetCreditNoteDoNotShowPurchaseRate = dr["setCreditNoteDoNotShowPurchaseRate"].ToString();
                    //// Report
                    //if (dr["setReportSaleDailySaleDoNotShowTotal"] != DBNull.Value)
                    //    MsetReportSaleDailySaleDoNotShowTotal = dr["setReportSaleDailySaleDoNotShowTotal"].ToString();
                    //if (dr["setReportSaleDailyProductsDoNotShowTotal"] != DBNull.Value)
                    //    MsetReportSaleDailyProductsDoNotShowTotal = dr["setReportSaleDailyProductsDoNotShowTotal"].ToString();
                    //if (dr["setProductNameWidthInSaleInvoice"] != DBNull.Value)
                    //    MsetProductNameWidthInSaleInvoice = Convert.ToInt32(dr["setProductNameWidthInSaleInvoice"]);

                    //if (dr["setSortOrder"] != DBNull.Value)
                    //    MsetSortOrder = dr["setSortOrder"].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        public bool GetOverviewData(string voucherseries)
        {
            bool retValue = false;
            DataRow dr;
            DBSettings set = new DBSettings();
            try
            {
                dr = set.GetOverviewData(voucherseries);
                if (dr != null)
                {
                    //purchase
                    if (dr["setPurchaseCopyPurchaseOrder"] != DBNull.Value)
                        MsetPurchaseCopyPurchaseOrder = dr["setPurchaseCopyPurchaseOrder"].ToString();
                    if (dr["setPurchaseRounding"] != DBNull.Value)
                        MsetPurchaseRounding = dr["setPurchaseRounding"].ToString();
                    if (dr["setPurchaseReadPurchaseOrder"] != DBNull.Value)
                        MsetPurchaseReadPurchaseOrder = dr["setPurchaseReadPurchaseOrder"].ToString();
                    if (dr["setPurchaseChangeSaleRate"] != DBNull.Value)
                        MsetPurchaseChangeSaleRate = dr["setPurchaseChangeSaleRate"].ToString();
                    if (dr["setPurchaseAllowExpiredItems"] != DBNull.Value)
                        MsetPurchaseAcceptExpriedItems = dr["setPurchaseAllowExpiredItems"].ToString();
                    if (dr["setPurchaseHold"] != DBNull.Value)
                        MsetPurchaseHold = dr["setPurchaseHold"].ToString();
                    if (dr["setPurchaseIfPTR"] != DBNull.Value)
                        MsetPurchaseIfPTR = dr["setPurchaseIfPTR"].ToString();

                    // sale 
                    if (dr["setSaleRoundOff"] != DBNull.Value)
                        MsetSaleRoundOff = dr["setSaleRoundOff"].ToString();
                    if (dr["setSaleCreditStatement"] != DBNull.Value)
                        MsetSaleCreditSale = dr["setSaleCreditStatement"].ToString();
                    if (dr["setSaleAllowBackDate"] != DBNull.Value)
                        MsetSaleAllowBackDate = dr["setSaleAllowBackDate"].ToString();
                    if (dr["setSaleTenderAmount"] != DBNull.Value)
                        MsetSaleTenderAmount = dr["setSaleTenderAmount"].ToString();
                    if (dr["setSaleShowProfitInSaleBill"] != DBNull.Value)
                        MsetSaleShowProfitInSaleBill = dr["setSaleShowProfitInSaleBill"].ToString();
                    if (dr["setSaleMaxDiscount"] != DBNull.Value && dr["setSaleMaxDiscount"].ToString() != string.Empty)
                        MsetSaleMaxDiscount = Convert.ToDouble(dr["setSaleMaxDiscount"].ToString());

                    if (dr["setSaleGetOrderNumberDate"] != DBNull.Value)
                        MsetSaleGetOrderNumberDate = dr["setSaleGetOrderNumberDate"].ToString();
                    if (dr["setSaleGetDoctor"] != DBNull.Value)
                        MsetSaleGetDoctor = dr["setSaleGetDoctor"].ToString();
                    if (dr["setSaleGetDelivaryBoy"] != DBNull.Value)
                        MsetSaleGetDelivaryBoy = dr["setSaleGetDelivaryBoy"].ToString();

                    //cashbank
                    //if (dr["setCashBankShowDiscount"] != DBNull.Value)
                    //    MsetCashBankShowDiscount = dr["setCashBankShowDiscount"].ToString();
                    ////sms
                    //if (dr["SmsDebtorSaleSet"] != DBNull.Value)
                    //    SmsSetDebtorSale = dr["SmsDebtorSaleSet"].ToString();
                    //if (dr["SmsBankPaymentSet"] != DBNull.Value)
                    //    SmsSetBankPaymentSale = dr["SmsBankPaymentSet"].ToString();
                    //if (dr["SmsBankReceiptSet"] != DBNull.Value)
                    //    SmsSetBankReceiptSale = dr["SmsBankReceiptSet"].ToString();
                    //if (dr["SmsCashReceiptSet"] != DBNull.Value)
                    //    SmsSetCashReceiptSale = dr["SmsCashReceiptSet"].ToString();
                    //if (dr["SmsCashPaymentSet"] != DBNull.Value)
                    //    SmsSetCashPaymentSale = dr["SmsCashPaymentSet"].ToString();


                    //
                    //if (dr["setOpeningStockGetPercent"] != DBNull.Value && dr["setOpeningStockGetPercent"].ToString() != string.Empty)
                    //    MsetOpeningStockGetPercent = Convert.ToDouble(dr["setOpeningStockGetPercent"].ToString());
                    ////general

                    //if (dr["setGeneralExpiryLast"] != DBNull.Value)
                    //    MsetGeneralExpiryLast = dr["setGeneralExpiryLast"].ToString();
                    //if (dr["setGeneralBatchNumberRequired"] != DBNull.Value)
                    //    MsetGeneralBatchNumberRequired = dr["setGeneralBatchNumberRequired"].ToString();
                    //if (dr["setGeneralExpiryDateRequired"] != DBNull.Value)
                    //    MsetGeneralExpiryDateReuired = dr["setGeneralExpiryDateRequired"].ToString();
                    //if (dr["setGeneralAskDatesInSearch"] != DBNull.Value)
                    //    MsetGeneralAskDatesInSearch = dr["setGeneralAskDatesInSearch"].ToString();
                    //if (dr["setGeneralAlphabetical"] != DBNull.Value)
                    //    MsetGeneralAlphabetical = dr["setGeneralAlphabetical"].ToString();
                    //if (dr["setAskOperatorOtherThanVoucherSale"] != DBNull.Value)
                    //    MsetAskOperatorOtherThanVoucherSale = dr["setAskOperatorOtherThanVoucherSale"].ToString();
                    //if (dr["setAskOperatorVoucherSale"] != DBNull.Value)
                    //    MsetAskOperatorVoucherSale = dr["setAskOperatorVoucherSale"].ToString();
                    //if (dr["setScanBarCode"] != DBNull.Value)
                    //    MsetScanBarCode = dr["setScanBarCode"].ToString();

                    //if (dr["setPrintSettingYesNo"] != DBNull.Value)
                    //    _MsetAllowPrintMessage = dr["setPrintSettingYesNo"].ToString();
                    //if (dr["setPrintSaleBillPrintedPaper"] != DBNull.Value)
                    //    MsetPrintSaleBill = dr["setPrintSaleBillPrintedPaper"].ToString();
                    //if (dr["setPrintCRDBNotePrintedPaper"] != DBNull.Value)
                    //    MsetPrintCRDBNote = dr["setPrintCRDBNotePrintedPaper"].ToString();
                    //if (dr["setPrintCashBankVoucherPrintedPaper"] != DBNull.Value)
                    //    MsetPrintCashBankVoucher = dr["setPrintCashBankVoucherPrintedPaper"].ToString();
                    //if (dr["setPrintPurchaseOrderPrintedPaper"] != DBNull.Value)
                    //    MsetPrintPO = dr["setPrintPurchaseOrderPrintedPaper"].ToString();
                    //if (dr["setPrintFontName"] != DBNull.Value)
                    //    MsetPrintFontName = dr["setPrintFontName"].ToString();
                    //if (dr["setPrintFontSize"] != DBNull.Value)
                    //    _MsetPrintFontSize = dr["setPrintFontSize"].ToString();
                    //if (dr["setPrintFixNumberOfLines"] != DBNull.Value)
                    //    _MsetPrintFixNumberOfLines = dr["setPrintFixNumberOfLines"].ToString();
                    //if (dr["setNumberOfLinesSaleBill"] != DBNull.Value)
                    //    MsetNumberOfLinesSaleBill = Convert.ToInt32(dr["setNumberOfLinesSaleBill"].ToString());
                    //if (dr["setNumberOfBillsAtaTime"] != DBNull.Value)
                    //    MsetNumberOfBillsAtaTime = Convert.ToInt32(dr["setNumberOfBillsAtaTime"].ToString());
                    //if (dr["setPrinterType"] != DBNull.Value)
                    //    MsetPrinterType = Convert.ToInt32(dr["setPrinterType"].ToString());
                    //if (dr["setPrintFontName"] != DBNull.Value)
                    //    MsetPrintFontName = dr["setPrintFontName"].ToString();
                    //if (dr["setPrintFontSize"] != DBNull.Value)
                    //    MsetPrintFontSize = dr["setPrintFontSize"].ToString();

                    //if (dr["SpecialDiscount1"] != DBNull.Value)
                    //    MsetSpecialDiscount1 = Convert.ToDouble(dr["SpecialDiscount1"].ToString());
                    //if (dr["SpecialDiscount2"] != DBNull.Value)
                    //    MsetSpecialDiscount2 = Convert.ToDouble(dr["SpecialDiscount2"].ToString());
                    //if (dr["SpecialDiscount3"] != DBNull.Value)
                    //    MsetSpecialDiscount3 = Convert.ToDouble(dr["SpecialDiscount3"].ToString());



                    //if (dr["setEmailID"] != DBNull.Value)
                    //    MsetEmailID = dr["setEmailID"].ToString();
                    //if (dr["setEmailPassword"] != DBNull.Value)
                    //    MsetEmailPassword = dr["setEmailPassword"].ToString();
                    //if (dr["SetEmailType"] != DBNull.Value)
                    //    MsetEmailType = dr["SetEmailType"].ToString();

                    //if (dr["setFixedNarration"] != DBNull.Value)
                    //    MsetFixedNarration = dr["setFixedNarration"].ToString();

                    //if (dr["setCreditNoteDefaultTransferToAccount"] != DBNull.Value)
                    //    MsetCreditNoteDefaultTransferToAccount = dr["setCreditNoteDefaultTransferToAccount"].ToString();
                    //if (dr["setCreditNoteReturnRateDisable"] != DBNull.Value)
                    //    MsetCreditNoteReturnRateDisable = dr["setCreditNoteReturnRateDisable"].ToString();
                    //if (dr["setCreditNoteDoNotShowPurchaseRate"] != DBNull.Value)
                    //    MsetCreditNoteDoNotShowPurchaseRate = dr["setCreditNoteDoNotShowPurchaseRate"].ToString();
                    //// Report
                    //if (dr["setReportSaleDailySaleDoNotShowTotal"] != DBNull.Value)
                    //    MsetReportSaleDailySaleDoNotShowTotal = dr["setReportSaleDailySaleDoNotShowTotal"].ToString();
                    //if (dr["setReportSaleDailyProductsDoNotShowTotal"] != DBNull.Value)
                    //    MsetReportSaleDailyProductsDoNotShowTotal = dr["setReportSaleDailyProductsDoNotShowTotal"].ToString();
                    //if (dr["setProductNameWidthInSaleInvoice"] != DBNull.Value)
                    //    MsetProductNameWidthInSaleInvoice = Convert.ToInt32(dr["setProductNameWidthInSaleInvoice"]);

                    //if (dr["setSortOrder"] != DBNull.Value)
                    //    MsetSortOrder = dr["setSortOrder"].ToString();

                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }


        public bool GetOverviewDataReportSettings(string voucherseries)
        {
            bool retValue = false;
            DataRow dr;
            DBSettings set = new DBSettings();
            try
            {
                dr = set.GetOverviewData(voucherseries);
                if (dr != null)
                {


                    if (dr["setReportSaleDailySaleDoNotShowTotal"] != DBNull.Value)
                        MsetReportSaleDailySaleDoNotShowTotal = dr["setReportSaleDailySaleDoNotShowTotal"].ToString();
                    if (dr["setReportSaleDailyProductsDoNotShowTotal"] != DBNull.Value)
                        MsetReportSaleDailyProductsDoNotShowTotal = dr["setReportSaleDailyProductsDoNotShowTotal"].ToString();

                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public bool GetOverviewDataPrint()
        {
            bool retValue = false;
            DataRow dr;
            DBSettings set = new DBSettings();
            try
            {
                dr = set.GetOverviewData(General.ShopDetail.ShopVoucherSeries);
                if (dr != null)
                {
                    if (dr["setSortOrder"] != DBNull.Value)
                        MsetSortOrder = dr["setSortOrder"].ToString();
                    if (dr["setPrintSaleBillPrintedPaper"] != DBNull.Value)
                        MsetPrintSaleBill = dr["setPrintSaleBillPrintedPaper"].ToString();
                    if (dr["setPrintCRDBNotePrintedPaper"] != DBNull.Value)
                        MsetPrintCRDBNote = dr["setPrintCRDBNotePrintedPaper"].ToString();
                    if (dr["setPrintCashBankVoucherPrintedPaper"] != DBNull.Value)
                        MsetPrintCashBankVoucher = dr["setPrintCashBankVoucherPrintedPaper"].ToString();
                    if (dr["setPrintPurchaseOrderPrintedPaper"] != DBNull.Value)
                        MsetPrintPO = dr["setPrintPurchaseOrderPrintedPaper"].ToString();
                    if (dr["setNumberOfLinesSaleBill"] != DBNull.Value)
                        MsetNumberOfLinesSaleBill = Convert.ToInt32(dr["setNumberOfLinesSaleBill"].ToString());
                    if (dr["setNumberOfBillsAtaTime"] != DBNull.Value)
                        MsetNumberOfBillsAtaTime = Convert.ToInt32(dr["setNumberOfBillsAtaTime"].ToString());
                    if (dr["setPrintFontName"] != DBNull.Value)
                        MsetPrintFontName = dr["setPrintFontName"].ToString();
                    if (dr["setPrintFontSize"] != DBNull.Value)
                        MsetPrintFontSize = dr["setPrintFontSize"].ToString();
                    if (dr["setPrintFixNumberOfLines"] != DBNull.Value)
                        MsetPrintFixNumberOfLines = dr["setPrintFixNumberOfLines"].ToString();
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }


        public bool AddDetails()
        {
            DBSettings dbset = new DBSettings();
            // ss 5/11
            return dbset.AddDetails(MsetPurchaseCopyPurchaseOrder, MsetPurchaseRounding, MsetPurchaseHold, MsetPurchaseIfPTR, MsetPurchaseReadPurchaseOrder, MsetSaleRoundOff, MsetPurchaseChangeSaleRate, MsetPurchaseAcceptExpriedItems, MsetSaleRoundOff,
                    MsetSaleCreditSale, MsetSaleAskDiscountinCounterSale, MsetSaleAskRoundinginSale, MsetSaleShowProfitInSaleBill, MsetSaleIPDOPD, MsetSaleDiscountWithoutVAT,
                    MsetGeneralProfitPercentageByPurchaseRate, MsetGeneralExpiryLast, MsetGeneralBatchNumberRequired, MsetGeneralExpiryDateReuired, MsetAskOperatorVoucherSale,
                    MsetAskOperatorOtherThanVoucherSale, MsetAskOperatorPurchase, MsetAskOperatorCRDB, MsetAskOperatorOpeningStock, MsetAskOperatorCorrectionRate, MsetAskOperatorJV,
                    MsetAskOperatorCashBankReceipt, MsetAskOperatorCashBankPayment, MsetScanBarCode, MsetPurchaseUpdateVATInMaster,
                    MsetSaleIncludeCreditSaleInStatements, MsetSaleSaveCustomerInMaster, MsetSaleShowOnlyMRPInCounterSale, MsetSaleAllowDistributorSale, MsetSaleOnlyCashSaleInCounterSale,
                    MsetSaleRoundingTo10Paise, MsetSaleRoundingToPreviousRupee, MsetSaleEditRateInCounterSale, MsetSaleAllowNegativeStock, MsetRemoveCodeFromCreditNote, MsetAllowPendingCashMemo,
                    MsetFixedNarration, MsetCreditNoteDefaultTransferToAccount, MsetCreditNoteReturnRateDisable, MsetSaleAllowBackDate, MsetSaleCounterSaleSingleGrid, MsetCreditNoteDoNotShowPurchaseRate, MsetSaleSelectVATPercent,
                    MsetGeneralAskDatesInSearch, MsetGeneralAlphabetical, MsetSaleProductRefreshButton, MsetSaleMaxDiscount, MsetOpeningStockGetPercent, MsetSaleTenderAmount, MsetCashBankShowDiscount, MsetProductNameWidthInSaleInvoice,
                    SmsSetPatientSale, SmsSetDebtorSale, SmsSetCreditCardSale, SmsSetInstitutionalSale, SmsSetBankPaymentSale, SmsSetBankReceiptSale, SmsSetCashPaymentSale, SmsSetCashReceiptSale, _MsetAllowPrintMessage,
                    MsetSaleGetOrderNumberDate, MsetSaleGetDoctor, MsetSaleGetDelivaryBoy);
            // ss 5/11
        }

        public bool UpdateDetails()
        {
            DBSettings dbset = new DBSettings();
            // ss 5/11
            return dbset.UpdateDetails(MsetPurchaseCopyPurchaseOrder, MsetPurchaseRounding, MsetPurchaseHold, MsetPurchaseIfPTR, MsetPurchaseReadPurchaseOrder, MsetPurchaseChangeSaleRate, MsetPurchaseAcceptExpriedItems, MsetSaleRoundOff,
                    MsetSaleCreditSale, MsetSaleAskDiscountinCounterSale, MsetSaleAskRoundinginSale, MsetSaleShowProfitInSaleBill, MsetSaleIPDOPD,
                    MsetSaleDiscountWithoutVAT, MsetGeneralProfitPercentageByPurchaseRate, MsetGeneralExpiryLast, MsetGeneralBatchNumberRequired, MsetGeneralExpiryDateReuired,
                    MsetAskOperatorVoucherSale, MsetAskOperatorOtherThanVoucherSale, MsetAskOperatorPurchase, MsetAskOperatorCRDB, MsetAskOperatorOpeningStock,
                    MsetAskOperatorCorrectionRate, MsetAskOperatorJV, MsetAskOperatorCashBankReceipt, MsetAskOperatorCashBankPayment, MsetScanBarCode,
                    MsetSaleIncludeCreditSaleInStatements, MsetSaleSaveCustomerInMaster, MsetSaleShowOnlyMRPInCounterSale, MsetSaleAllowDistributorSale,
                    MsetSaleOnlyCashSaleInCounterSale, MsetSaleRoundingTo10Paise, MsetSaleRoundingToPreviousRupee, MsetSaleEditRateInCounterSale, MsetSaleAllowNegativeStock,
                    MsetRemoveCodeFromCreditNote, MsetAllowPendingCashMemo, MsetFixedNarration, MsetCreditNoteDefaultTransferToAccount, MsetCreditNoteReturnRateDisable,
                    MsetSaleAllowBackDate, MsetSaleCounterSaleSingleGrid, MsetCreditNoteDoNotShowPurchaseRate, MsetSaleSelectVATPercent, MsetGeneralAskDatesInSearch,
                    MsetGeneralAlphabetical, MsetSaleProductRefreshButton, MsetSaleMaxDiscount, MsetOpeningStockGetPercent, MsetSaleTenderAmount,
                    MsetCashBankShowDiscount, MsetProductNameWidthInSaleInvoice, SmsSetPatientSale, SmsSetDebtorSale,
                    SmsSetCreditCardSale, SmsSetInstitutionalSale, SmsSetBankPaymentSale, SmsSetBankReceiptSale, SmsSetCashPaymentSale, SmsSetCashReceiptSale, MsetSaleGetOrderNumberDate, MsetSaleGetDoctor, MsetSaleGetDelivaryBoy);
            // ss 5/11
        }

        public bool UpdateDetailsPrint()

        {
            DBSettings dbset = new DBSettings();
            return dbset.UpdateDetailsPrint(MsetPrintSaleBill, MsetPrintCRDBNote, MsetPrintCashBankVoucher, MsetPrintPO, MsetNumberOfLinesSaleBill, MsetNumberOfBillsAtaTime, MsetSortOrder, MsetPrintFixNumberOfLines);
        }
        public bool UpdateDetailsReport()
        {
            DBSettings dbset = new DBSettings();
            return dbset.UpdateDetailsReports(MsetReportSaleDailySaleDoNotShowTotal, MsetReportSaleDailyProductsDoNotShowTotal, MsetPrintFontName, MsetPrintFontSize);
        }
        // Email

        public bool GetOverviewDataEmail()
        {
            bool retValue = false;
            DataRow dr;
            DBSettings set = new DBSettings();
            try
            {
                dr = set.GetOverviewData(General.ShopDetail.ShopVoucherSeries);
                if (dr != null)
                {
                    if (dr["setEmailID"] != DBNull.Value)
                        MsetEmailID = dr["setEmailID"].ToString();
                    if (dr["setEmailPassword"] != DBNull.Value)
                        MsetEmailPassword = dr["setEmailPassword"].ToString();
                    if (dr["setEmailType"] != DBNull.Value)
                        MsetEmailType = dr["setEmailType"].ToString();
                    retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public bool UpdateDetailsEmail()
        {
            DBSettings dbset = new DBSettings();
            return dbset.UpdateDetailsEmail(MsetEmailID, MsetEmailPassword, MsetEmailType);
        }
        #endregion Public Methods
    }
}
