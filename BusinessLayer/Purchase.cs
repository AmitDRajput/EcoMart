using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using System.Globalization;
using EcoMart.Common;
using EcoMart.BusinessLayer;

namespace EcoMart.BusinessLayer
{
    public class Purchase : BaseObject
    {
        #region Declaration
        private string _EntryDate;
        private string _VoucherSeries;
        private string _VoucherType;
        private int _VoucherNumber;
        private string _VoucherDate;
        private string _PurchaseBillNumber;
        private string _AccountID;
        private string _CreditDebitNoteID;
        private string _OldVoucherType;
        private int _OldVoucherNumber;

        private string _DueDate;
        private string _Narration;
        private double _VAT5Percent;
        private double _VATOPercent;
        private double _VAT12point5Percent;
        private int _ProductID;
        private int _ProdLoosePack;
        private string _Batchno;
        private string _Pack;
        private double _Mrp;
        private double _PurchaseRate;
        private double _SaleRate;
        private double _TradeRate;
        private int _Quantity;
        private int _SchemeQuantity;
        private int _ReplacementQuantity;
        private string _Expiry;
        private string _ExpiryDate;
        private double _ItemDiscountPercent;
        private double _AmountItemDiscount;
        private double _SchemeDiscountPercent;
        private double _AmountSchemeDiscount;
        private double _AmountScmDiscountPerUnit;
        private double _SplDiscountPercent;
        private double _AmountSplDiscount;
        private double _PurchaseVATPercent;
        private double _ProductVATPercent;
        private double _AmountPurchaseVAT;
        private double _AmountProductVAT;
        private double _CSTPercent;
        private double _AmountCST;
        private double _Amount;
        private string _IfMRPInclusiveOfVAT;
        private string _IfTradeRateInclusiveOfVAT;
        private string _ShelfCode;
        private string _ShelfID;
        private double _AmountZeroVAT;
        private double _AmountCashDiscountPerUnit;
        private double _AmountSplDiscountPerUnit;
        private double _ProductMargin;
        private double _ProductMargin2;
        private double _ProfitPercent;
        private double _PriceToRetailer;
        //
        //    private double _ExciseInRs;
        //  private double _ExcisePerUnit;

        private string _FirstCreditor;
        private string _SecondCreditor;

        private double _AmountS;
        private double _AmountNetS;
        private double _AmountClearS;
        private double _AmountBillS;
        private double _AmountItemDiscountS;
        private double _AmountSpecialDiscountS;
        private double _SpecialDiscountPercentageS;
        private double _AmountSchemeDiscountS;
        private double _AmountCashDiscountS;
        private double _AmountAddOnS;
        private double _AmountFreightS;
        private double _CashDiscountPercentageS;
        private double _AmountCreditNoteS;
        private double _AmountDebitNoteS;
        private double _RoundUpAmountS;
        private double _OctroiPercentageS;
        private double _AmountOctroiS;
        private double _AmountVAT12point5PercentS;
        private double _AmountVAT5PercentS;
        private double _AmountVAT0PercentS;
        private double _PurchaseAmountZeroVATS;
        private double _PurchaseAmount12point5PercentVATS;
        private double _PurchaseAmount5PercentVATS;
        private double _CreditNoteDiscountPercentS;
        private double _TotalAmountForOctroiS;

        private double _AmountAddOnFreightS;
        private double _AmountLessS;
        private string _oldAccountId;
        //
        //   private double _TotalAmountExciseS;

        private int _NumberofChallans;
        private int _StatementNumber;
        private int _NoofRows;
        private string _TransactionText = "";

        private string _PurchaseAccount;
        private string _IfTypeChange;



        private double _PendingAmount;
        private double _TotalDebit;
        private double _TotalCredit;
        private double _OpeningBalance;

        private string _IfCashPaid;
        private string _ChequeNumber;
        private string _ChequeDate;
        private string _BankID;
        private int _CBVouNo;
        private string _CBVouType;
        private string _CBId;
        private string _VoucherSubType;

        private string _PurchaseAccAccountID;
        private string _StockID;
        //   private int _IntStockID = 0;

        //private string _btnCashCreditChecked;
        //private string _btnCashChecked;
        //private string _btnCreditChecked;

        private string _PurScanCode;
        private string _PrePurScanCode;

        private string _preAccountID;
        private string _preNarration;
        private string _preEntryDate;
        private string _preVoucherSeries;
        private string _preVoucherType;
        private int _preVoucherNumber;
        private string _preVoucherDate;
        private string _prePurchaseBillNumber;
        private double _preAmountNetS;
        private double _preAmountClearS;
        private double _preAmountBillS;
        private double _preAmountItemDiscountS;
        private double _preAmountSpecialDiscountS;
        private double _preSpecialDiscountPercentS;
        private double _preAmountCashDiscountS;
        private double _preCreditNoteDiscountPercentS;
        private double _preAmountSchemeDiscountS;
        private double _preAmountAddOnS;
        private double _preAmountFreightS;
        private double _preCashDiscountPercentageS;
        private double _preAmountCreditNoteS;
        private double _preAmountDebitNoteS;
        private double _preRoundUpAmountS;
        private double _preOctroiPercentageS;
        private double _preAmountOctroiS;
        private double _prePurchaseAmount5PercentVATS;
        private double _preAmountVAT0PercentS;
        private double _preAmountVAT5PercentS;
        private double _prePurchaseAmount12point5PercentVATS;
        private double _preAmountVAT12point5PercentS;


        //  private double _preExcisePerUnit;
        //  private double _preTotalAmountExciseS;

        //    private double _prePurchaseAmount0PercentVATS;
        private string _preDueDate;
        private int _preNumberofChallans;
        private double _prePurchaseAmountZeroVATS;
        private int _preStatementNumber;
        private string _preVoucherSubType;



        private double _GSTAmt0;
        private double _GSTAmtS5 = 0;
        private double _GSTAmtC5 = 0;
        private double _GSTAmtI5 = 0;
        private double _GSTS5 = 0;
        private double _GSTC5 = 0;
        private double _GSTI5 = 0;
        private double _GSTAmtS12 = 0;
        private double _GSTAmtC12 = 0;
        private double _GSTAmtI12 = 0;
        private double _GSTS12 = 0;
        private double _GSTC12 = 0;
        private double _GSTI12 = 0;
        private double _GSTAmtS18 = 0;
        private double _GSTAmtC18 = 0;
        private double _GSTAmtI18 = 0;
        private double _GSTS18 = 0;
        private double _GSTC18 = 0;
        private double _GSTI18 = 0;
        private double _GSTAmtS28 = 0;
        private double _GSTAmtC28 = 0;
        private double _GSTAmtI28 = 0;
        private double _GSTS28 = 0;
        private double _GSTC28 = 0;
        private double _GSTI28 = 0;

        private double _GSTSAmount = 0;
        private double _GSTCAmount = 0;
        private double _GSTSPurchaseAmount = 0;
        private double _GSTCPurchaseAmount = 0;
        private double _GSTPurchaseAmountZero = 0;

        private string _IfOMS = "N";
        private string _IfExemptedScheme = "N";


        private string _PurchaseBillFormat;


        #endregion

        #region Properties

        public double ProfitPercent
        {
            get { return _ProfitPercent; }
            set { _ProfitPercent = value; }
        }
        public double PriceToRetailer
        {
            get { return _PriceToRetailer; }
            set { _PriceToRetailer = value; }
        }
        public string PurchaseBillFormat
        {
            get { return _PurchaseBillFormat; }
            set { _PurchaseBillFormat = value; }
        }


        public string IFOMS
        {
            get { return _IfOMS; }
            set { _IfOMS = value; }
        }

        public string IfExemptedScheme
        {
            get { return _IfExemptedScheme; }
            set { _IfExemptedScheme = value; }
        }

        public double GSTPurchaseAmountZero
        {
            get { return _GSTPurchaseAmountZero; }
            set { _GSTPurchaseAmountZero = value; }
        }
        public double GSTSAmount
        {
            get { return _GSTSAmount; }
            set { _GSTSAmount = value; }
        }
        public double GSTCAmount
        {
            get { return _GSTCAmount; }
            set { _GSTCAmount = value; }
        }

        public double GSTSPurchaseAmount
        {
            get { return _GSTSPurchaseAmount; }
            set { _GSTSPurchaseAmount = value; }
        }

        public double GSTCPurchaseAmount
        {
            get { return _GSTCPurchaseAmount; }
            set { _GSTCPurchaseAmount = value; }
        }
        public double GSTAmt0
        {
            get { return _GSTAmt0; }
            set { _GSTAmt0 = value; }
        }
        public double GSTAmtS5
        {
            get { return _GSTAmtS5; }
            set { _GSTAmtS5 = value; }
        }
        public double GSTAmtC5
        {
            get { return _GSTAmtC5; }
            set { _GSTAmtC5 = value; }
        }
        public double GSTAmtI5
        {
            get { return _GSTAmtI5; }
            set { _GSTAmtI5 = value; }
        }
        public double GSTAmtS12
        {
            get { return _GSTAmtS12; }
            set { _GSTAmtS12 = value; }
        }
        public double GSTAmtC12
        {
            get { return _GSTAmtC12; }
            set { _GSTAmtC12 = value; }
        }
        public double GSTAmtI12
        {
            get { return _GSTAmtI12; }
            set { _GSTAmtI12 = value; }
        }
        public double GSTAmtS18
        {
            get { return _GSTAmtS18; }
            set { _GSTAmtS18 = value; }
        }
        public double GSTAmtC18
        {
            get { return _GSTAmtC18; }
            set { _GSTAmtC18 = value; }
        }
        public double GSTAmtI18
        {
            get { return _GSTAmtI18; }
            set { _GSTAmtI18 = value; }
        }
        public double GSTAmtS28
        {
            get { return _GSTAmtS28; }
            set { _GSTAmtS28 = value; }
        }
        public double GSTAmtC28
        {
            get { return _GSTAmtC28; }
            set { _GSTAmtC28 = value; }
        }
        public double GSTAmtI28
        {
            get { return _GSTAmtI28; }
            set { _GSTAmtI28 = value; }
        }
        public double GSTS5
        {
            get { return _GSTS5; }
            set { _GSTS5 = value; }
        }
        public double GSTC5
        {
            get { return _GSTC5; }
            set { _GSTC5 = value; }
        }
        public double GSTI5
        {
            get { return _GSTI5; }
            set { _GSTI5 = value; }
        }
        public double GSTS12
        {
            get { return _GSTS12; }
            set { _GSTS12 = value; }
        }
        public double GSTC12
        {
            get { return _GSTC12; }
            set { _GSTC12 = value; }
        }
        public double GSTI12
        {
            get { return _GSTI12; }
            set { _GSTI12 = value; }
        }
        public double GSTS18
        {
            get { return _GSTS18; }
            set { _GSTS18 = value; }
        }
        public double GSTC18
        {
            get { return _GSTC18; }
            set { _GSTC18 = value; }
        }
        public double GSTI18
        {
            get { return _GSTI18; }
            set { _GSTI18 = value; }
        }
        public double GSTS28
        {
            get { return _GSTS28; }
            set { _GSTS28 = value; }
        }
        public double GSTC28
        {
            get { return _GSTC28; }
            set { _GSTC28 = value; }
        }
        public double GSTI28
        {
            get { return _GSTI28; }
            set { _GSTI28 = value; }
        }

        public string oldAccountId
        {
            get { return _oldAccountId; }
            set { _oldAccountId = value; }
        }
        public double AmountLessS
        {
            get { return _AmountLessS; }
            set { _AmountLessS = value; }
        }
        public double AmountAddOnFreightS
        {
            get { return _AmountAddOnFreightS; }
            set { _AmountAddOnFreightS = value; }
        }

        public string StockID
        {
            get { return _StockID; }
            set { _StockID = value; }
        }

        //public int IntStockID
        //{
        //    get { return _IntStockID; }
        //    set { _IntStockID = value; }
        //}
        public string PurchaseAccAccountID
        {
            get { return _PurchaseAccAccountID; }
            set { _PurchaseAccAccountID = value; }
        }

        public int CBVouNo
        {
            get { return _CBVouNo; }
            set { _CBVouNo = value; }
        }

        public string OldVoucherType
        {
            get { return _OldVoucherType; }
            set { _OldVoucherType = value; }
        }

        public int OldVoucherNumber
        {
            get { return _OldVoucherNumber; }
            set { _OldVoucherNumber = value; }
        }

        public string CBVouType
        {
            get { return _CBVouType; }
            set { _CBVouType = value; }
        }
        public string TransactionText
        {
            get { return _TransactionText; }
            set { _TransactionText = value; }
        }


        public string CBId
        {
            get { return _CBId; }
            set { _CBId = value; }
        }
        public string IfCashPaid
        {
            get { return _IfCashPaid; }
            set { _IfCashPaid = value; }
        }
        public string ChequeNumber
        {
            get { return _ChequeNumber; }
            set { _ChequeNumber = value; }
        }
        public string ChequeDate
        {
            get { return _ChequeDate; }
            set { _ChequeDate = value; }
        }
        public string BankID
        {
            get { return _BankID; }
            set { _BankID = value; }
        }

        public double TotalDebit
        {
            get { return _TotalDebit; }
            set { _TotalDebit = value; }
        }
        public double TotalCredit
        {
            get { return _TotalCredit; }
            set { _TotalCredit = value; }
        }
        public double OpeningBalance
        {
            get { return _OpeningBalance; }
            set { _OpeningBalance = value; }
        }

        public double ProductMargin
        {
            get { return _ProductMargin; }
            set { _ProductMargin = value; }
        }

        public double ProductMargin2
        {
            get { return _ProductMargin2; }
            set { _ProductMargin2 = value; }
        }

        public double PendingAmount
        {
            get { return _PendingAmount; }
            set { _PendingAmount = value; }
        }

        public string PurchaseAccount
        {
            get { return _PurchaseAccount; }
            set { _PurchaseAccount = value; }
        }


        public int NoofRows
        {
            get { return _NoofRows; }
            set { _NoofRows = value; }
        }

        public string CreditDebitNoteID
        {
            get { return _CreditDebitNoteID; }
            set { _CreditDebitNoteID = value; }
        }

        public int ProdLoosePack
        {
            get { return _ProdLoosePack; }
            set { _ProdLoosePack = value; }
        }
        public string ShelfCode
        {
            get { return _ShelfCode; }
            set { ShelfCode = value; }
        }
        public string ShelfID
        {
            get { return _ShelfID; }
            set { _ShelfID = value; }
        }
        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public string Batchno
        {
            get { return _Batchno; }
            set { _Batchno = value; }
        }
        public string Pack
        {
            get { return _Pack; }
            set { _Pack = value; }
        }
        public double MRP
        {
            get { return _Mrp; }
            set { _Mrp = value; }
        }
        public double PurchaseRate
        {
            get { return _PurchaseRate; }
            set { _PurchaseRate = value; }
        }
        public double SaleRate
        {
            get { return _SaleRate; }
            set { _SaleRate = value; }
        }
        public double TradeRate
        {
            get { return _TradeRate; }
            set { _TradeRate = value; }
        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public int SchemeQuanity
        {
            get { return _SchemeQuantity; }
            set { _SchemeQuantity = value; }
        }
        public int ReplacementQuantity
        {
            get { return _ReplacementQuantity; }
            set { _ReplacementQuantity = value; }
        }
        public string Expiry
        {
            get { return _Expiry; }
            set { _Expiry = value; }
        }
        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }

        public string EntryDate
        {
            get { return _EntryDate; }
            set { _EntryDate = value; }
        }
        public string VoucherSeries
        {
            get { return _VoucherSeries; }
            set { _VoucherSeries = value; }
        }
        public string VoucherType
        {
            get { return _VoucherType; }
            set { _VoucherType = value; }
        }
        public int VoucherNumber
        {
            get { return _VoucherNumber; }
            set { _VoucherNumber = value; }
        }
        public string VoucherDate
        {
            get { return _VoucherDate; }
            set { _VoucherDate = value; }
        }

        public string VoucherSubType
        {
            get { return _VoucherSubType; }
            set { _VoucherSubType = value; }
        }

        public string preVoucherSubType
        {
            get { return _preVoucherSubType; }
            set { _preVoucherSubType = value; }
        }
        public string PurchaseBillNumber
        {
            get { return _PurchaseBillNumber; }
            set { _PurchaseBillNumber = value; }
        }
        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }
        public string DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        public string Narration
        {
            get { return _Narration; }
            set { _Narration = value; }
        }

        public double VATOPercent
        {
            get { return _VATOPercent; }
            set { _VATOPercent = value; }
        }
        public double VAT5Percent
        {
            get { return _VAT5Percent; }
            set { _VAT5Percent = value; }
        }
        public double VAT12point5Percent
        {
            get { return _VAT12point5Percent; }
            set { _VAT12point5Percent = value; }
        }

        public int NumberofChallans
        {
            get { return _NumberofChallans; }
            set { _NumberofChallans = value; }
        }

        public double ItemDiscountPercent
        {
            get { return _ItemDiscountPercent; }
            set { _ItemDiscountPercent = value; }
        }

        public double AmountItemDiscount
        {
            get { return _AmountItemDiscount; }
            set { _AmountItemDiscount = value; }
        }
        public double SplDiscountPercent
        {
            get { return _SplDiscountPercent; }
            set { _SplDiscountPercent = value; }
        }

        public double AmountSplDiscount
        {
            get { return _AmountSplDiscount; }
            set { _AmountSplDiscount = value; }
        }
        public double SchemeDiscountPercent
        {
            get { return _SchemeDiscountPercent; }
            set { _SchemeDiscountPercent = value; }
        }
        public double AmountSchemeDiscount
        {
            get { return _AmountSchemeDiscount; }
            set { _AmountSchemeDiscount = value; }
        }

        public double AmountScmDiscountPerUnit
        {
            get { return _AmountScmDiscountPerUnit; }
            set { _AmountScmDiscountPerUnit = value; }
        }
        public double PurchaseVATPercent
        {
            get { return _PurchaseVATPercent; }
            set { _PurchaseVATPercent = value; }
        }
        public double ProductVATPercent
        {
            get { return _ProductVATPercent; }
            set { _ProductVATPercent = value; }
        }
        public double AmountPurchaseVAT
        {
            get { return _AmountPurchaseVAT; }
            set { _AmountPurchaseVAT = value; }
        }

        public double AmountProductVAT
        {
            get { return _AmountProductVAT; }
            set { _AmountProductVAT = value; }
        }
        public double CSTPercent
        {
            get { return _CSTPercent; }
            set { _CSTPercent = value; }
        }
        public double AmountCST
        {
            get { return _AmountCST; }
            set { _AmountCST = value; }
        }
        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string IfMRPInclusiveOfVAT
        {
            get { return _IfMRPInclusiveOfVAT; }
            set { _IfMRPInclusiveOfVAT = value; }
        }
        public string IfTradeRateInclusiveOfVAT
        {
            get { return _IfTradeRateInclusiveOfVAT; }
            set { _IfTradeRateInclusiveOfVAT = value; }
        }

        public double AmountZeroVAT
        {
            get { return _AmountZeroVAT; }
            set { _AmountZeroVAT = value; }
        }

        public double AmountCashDiscountPerUnit
        {
            get { return _AmountCashDiscountPerUnit; }
            set { _AmountCashDiscountPerUnit = value; }
        }
        public double AmountSplDiscountPerUnit
        {
            get { return _AmountSplDiscountPerUnit; }
            set { _AmountSplDiscountPerUnit = value; }
        }

        public double AmountS
        {
            get { return _AmountS; }
            set { _AmountS = value; }
        }
        public double AmountNetS
        {
            get { return _AmountNetS; }
            set { _AmountNetS = value; }
        }
        public double AmountClearS
        {
            get { return _AmountClearS; }
            set { _AmountClearS = value; }
        }
        public double AmountBillS
        {
            get { return _AmountBillS; }
            set { _AmountBillS = value; }
        }
        public double AmountItemDiscountS
        {
            get { return _AmountItemDiscountS; }
            set { _AmountItemDiscountS = value; }
        }
        public double AmountSpecialDiscountS
        {
            get { return _AmountSpecialDiscountS; }
            set { _AmountSpecialDiscountS = value; }
        }
        public double SpecialDiscountPercentS
        {
            get { return _SpecialDiscountPercentageS; }
            set { _SpecialDiscountPercentageS = value; }
        }
        public double CreditNoteDiscountPercentS
        {
            get { return _CreditNoteDiscountPercentS; }
            set { _CreditNoteDiscountPercentS = value; }
        }
        public double AmountSchemeDiscountS
        {
            get { return _AmountSchemeDiscountS; }
            set { _AmountSchemeDiscountS = value; }
        }
        public double AmountCashDiscountS
        {
            get { return _AmountCashDiscountS; }
            set { _AmountCashDiscountS = value; }
        }
        public double AmountAddOnS
        {
            get { return _AmountAddOnS; }
            set { _AmountAddOnS = value; }
        }

        public double AmountFreightS
        {
            get { return _AmountFreightS; }
            set { _AmountFreightS = value; }
        }

        public double CashDiscountPercentageS
        {
            get { return _CashDiscountPercentageS; }
            set { _CashDiscountPercentageS = value; }
        }
        public double AmountCreditNoteS
        {
            get { return _AmountCreditNoteS; }
            set { _AmountCreditNoteS = value; }
        }
        public double AmountDebitNoteS
        {
            get { return _AmountDebitNoteS; }
            set { _AmountDebitNoteS = value; }
        }
        public double RoundUpAmountS
        {
            get { return _RoundUpAmountS; }
            set { _RoundUpAmountS = value; }
        }
        public double OctroiPercentageS
        {
            get { return _OctroiPercentageS; }
            set { _OctroiPercentageS = value; }
        }
        public double AmountOctroiS
        {
            get { return _AmountOctroiS; }
            set { _AmountOctroiS = value; }
        }

        public double PurchaseAmountZeroVATS
        {
            get { return _PurchaseAmountZeroVATS; }
            set { _PurchaseAmountZeroVATS = value; }
        }
        public double PurchaseAmount5PercentVATS
        {
            get { return _PurchaseAmount5PercentVATS; }
            set { _PurchaseAmount5PercentVATS = value; }
        }
        public double AmountVAT0PercentS
        {
            get { return _AmountVAT0PercentS; }
            set { _AmountVAT0PercentS = value; }
        }
        public double AmountVAT5PercentS
        {
            get { return _AmountVAT5PercentS; }
            set { _AmountVAT5PercentS = value; }
        }
        public double PurchaseAmount12point5PercentVATS
        {
            get { return _PurchaseAmount12point5PercentVATS; }
            set { _PurchaseAmount12point5PercentVATS = value; }
        }
        public double AmountVAT12point5PercentS
        {
            get { return _AmountVAT12point5PercentS; }
            set { _AmountVAT12point5PercentS = value; }
        }
        //public double PurchaseAmount0PercentVATS
        //{
        //    get { return _PurchaseAmount0PercentVATS; }
        //    set { _PurchaseAmount0PercentVATS = value; }
        //}
        public double TotalAmountForOctroiS
        {
            get { return _TotalAmountForOctroiS; }
            set { _TotalAmountForOctroiS = value; }
        }
        public int StatementNumber
        {
            get { return _StatementNumber; }
            set { _StatementNumber = value; }
        }
        public string IfTypeChange
        {
            get { return _IfTypeChange; }
            set { _IfTypeChange = value; }
        }

        public string PurScanCode
        {
            get { return _PurScanCode; }
            set { _PurScanCode = value; }
        }

        public string PrePurScanCode
        {
            get { return _PrePurScanCode; }
            set { _PrePurScanCode = value; }
        }

        public string preAccountID
        {
            get { return _preAccountID; }
            set { _preAccountID = value; }
        }
        public string preNarration
        {
            get { return _preNarration; }
            set { _preNarration = value; }
        }
        public string preEntryDate
        {
            get { return _preEntryDate; }
            set { _preEntryDate = value; }
        }

        public string preVoucherSeries
        {
            get { return _preVoucherSeries; }
            set { _preVoucherSeries = value; }
        }
        public string preVoucherType
        {
            get { return _preVoucherType; }
            set { _preVoucherType = value; }
        }
        public int preVoucherNumber
        {
            get { return _preVoucherNumber; }
            set { _preVoucherNumber = value; }
        }

        public string preVoucherDate
        {
            get { return _preVoucherDate; }
            set { _preVoucherDate = value; }
        }

        public string prePurchaseBillNumber
        {
            get { return _prePurchaseBillNumber; }
            set { _prePurchaseBillNumber = value; }
        }

        public double preAmountNetS
        {
            get { return _preAmountNetS; }
            set { _preAmountNetS = value; }
        }
        public double preAmountClearS
        {
            get { return _preAmountClearS; }
            set { _preAmountClearS = value; }
        }
        public double preAmountBillS
        {
            get { return _preAmountBillS; }
            set { _preAmountBillS = value; }
        }

        public double preAmountItemDiscountS
        {
            get { return _preAmountItemDiscountS; }
            set { _preAmountItemDiscountS = value; }
        }
        public double preAmountSpecialDiscountS
        {
            get { return _preAmountSpecialDiscountS; }
            set { _preAmountSpecialDiscountS = value; }
        }

        public double preSpecialDiscountPercentS
        {
            get { return _preSpecialDiscountPercentS; }
            set { _preSpecialDiscountPercentS = value; }
        }

        public double preAmountCashDiscountS
        {
            get { return _preAmountCashDiscountS; }
            set { _preAmountCashDiscountS = value; }
        }
        public double preCreditNoteDiscountPercentS
        {
            get { return _preCreditNoteDiscountPercentS; }
            set { _preCreditNoteDiscountPercentS = value; }
        }
        public double preAmountSchemeDiscountS
        {
            get { return _preAmountSchemeDiscountS; }
            set { _preAmountSchemeDiscountS = value; }
        }
        public double preAmountAddOnS
        {
            get { return _preAmountAddOnS; }
            set { _preAmountAddOnS = value; }
        }

        public double preAmountFreightS
        {
            get { return _preAmountFreightS; }
            set { _preAmountFreightS = value; }
        }
        public double preCashDiscountPercentageS
        {
            get { return _preCashDiscountPercentageS; }
            set { _preCashDiscountPercentageS = value; }
        }
        public double preAmountCreditNoteS
        {
            get { return _preAmountCreditNoteS; }
            set { _preAmountCreditNoteS = value; }
        }
        public double preAmountDebitNoteS
        {
            get { return _preAmountDebitNoteS; }
            set { _preAmountDebitNoteS = value; }
        }
        public double preRoundUpAmountS
        {
            get { return _preRoundUpAmountS; }
            set { _preRoundUpAmountS = value; }
        }
        public double preOctroiPercentageS
        {
            get { return _preOctroiPercentageS; }
            set { _preOctroiPercentageS = value; }
        }
        public double preAmountOctroiS
        {
            get { return _preAmountOctroiS; }
            set { _preAmountOctroiS = value; }
        }
        public double prePurchaseAmount5PercentVATS
        {
            get { return _prePurchaseAmount5PercentVATS; }
            set { _prePurchaseAmount5PercentVATS = value; }
        }
        public double preAmountVAT0PercentS
        {
            get { return _preAmountVAT0PercentS; }
            set { _preAmountVAT0PercentS = value; }
        }
        public double preAmountVAT5PercentS
        {
            get { return _preAmountVAT5PercentS; }
            set { _preAmountVAT5PercentS = value; }
        }
        public double prePurchaseAmount12point5PercentVATS
        {
            get { return _prePurchaseAmount12point5PercentVATS; }
            set { _prePurchaseAmount12point5PercentVATS = value; }
        }



        public double preAmountVAT12point5PercentS
        {
            get { return _preAmountVAT12point5PercentS; }
            set { _preAmountVAT12point5PercentS = value; }
        }
        //public double prePurchaseAmount0PercentVATS
        //{
        //    get { return _prePurchaseAmount0PercentVATS; }
        //    set { _prePurchaseAmount0PercentVATS = value; }
        //}
        public string preDueDate
        {
            get { return _preDueDate; }
            set { _preDueDate = value; }
        }

        public int preNumberofChallans
        {
            get { return _preNumberofChallans; }
            set { _preNumberofChallans = value; }
        }
        public double prePurchaseAmountZeroVATS
        {
            get { return _prePurchaseAmountZeroVATS; }
            set { _prePurchaseAmountZeroVATS = value; }
        }
        public int preStatementNumber
        {
            get { return _preStatementNumber; }
            set { _preStatementNumber = value; }
        }
        public string FirstCreditor
        {
            get { return _FirstCreditor; }
            set { _FirstCreditor = value; }
        }
        public string SecondCreditor
        {
            get { return _SecondCreditor; }
            set { _SecondCreditor = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();

            _CreditDebitNoteID = "";
            _EntryDate = "";
            _VoucherSeries = "";
            _VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
            _VoucherNumber = 0;
            _VoucherDate = "";
            _PurchaseBillNumber = "";
            _AccountID = "";
            _Batchno = "";
            _Pack = "";
            _ProdLoosePack = 1;
            _Expiry = "";
            _ExpiryDate = "";
            _Mrp = 0;
            _ProductID = 0;
            _PurchaseRate = 0;
            _Quantity = 0;
            _NoofRows = 0;
            _OldVoucherNumber = 0;
            _OldVoucherType = "";
            _ProductMargin = 0;
            _ProductMargin2 = 0;
            _FirstCreditor = "";
            _SecondCreditor = "";
            _TransactionText = "";

            _SaleRate = 0;
            _SchemeQuantity = 0;
            _ReplacementQuantity = 0;
            _TradeRate = 0;
            _AmountZeroVAT = 0;
            _PriceToRetailer = 0;
            _ProfitPercent = 0;

            _ItemDiscountPercent = 0;
            _AmountItemDiscount = 0;
            _SchemeDiscountPercent = 0;
            _AmountSchemeDiscount = 0;
            _PurchaseVATPercent = 0;
            _ProductVATPercent = 0;
            _AmountPurchaseVAT = 0;
            _AmountProductVAT = 0;
            _CSTPercent = 0;
            _AmountCST = 0;
            _Amount = 0;
            _IfMRPInclusiveOfVAT = "";
            _IfTradeRateInclusiveOfVAT = "";
            _ShelfCode = "";
            _ShelfID = "";
            _AmountCashDiscountPerUnit = 0;
            _AmountSplDiscountPerUnit = 0;
            _AmountScmDiscountPerUnit = 0;


            _AmountNetS = 0;
            _AmountClearS = 0;
            _AmountS = 0;
            _AmountBillS = 0;
            _AmountItemDiscountS = 0;
            _AmountSpecialDiscountS = 0;
            _AmountSchemeDiscountS = 0;
            _AmountCashDiscountS = 0;
            _AmountAddOnS = 0;
            _AmountFreightS = 0;
            _CashDiscountPercentageS = 0;
            _AmountCreditNoteS = 0;
            _AmountDebitNoteS = 0;
            _RoundUpAmountS = 0;
            _OctroiPercentageS = 0;
            _AmountOctroiS = 0;

            _AmountVAT0PercentS = 0;
            _AmountVAT5PercentS = 0;
            _AmountVAT12point5PercentS = 0;

            _PurchaseAmount12point5PercentVATS = 0;
            _PurchaseAmount5PercentVATS = 0;
            //   _PurchaseAmount0PercentVATS = 0;
            _PurchaseAmountZeroVATS = 0;


            _TotalAmountForOctroiS = 0;
            _SpecialDiscountPercentageS = 0;
            _CreditNoteDiscountPercentS = 0;
            _StatementNumber = 0;
            _PendingAmount = 0;
            _OpeningBalance = 0;
            _TotalCredit = 0;
            _TotalDebit = 0;

            _IfCashPaid = "N";
            _ChequeDate = "";
            _ChequeNumber = "";
            _BankID = "";
            _PurchaseAccAccountID = "";
            _StockID = "";
            //   _IntStockID = 0;

            //_btnCashCreditChecked = "N";
            //_btnCashChecked = "N";
            //_btnCreditChecked = "N";
            _IfTypeChange = "N";

            _PurScanCode = "";
            _PrePurScanCode = "";

            _preAccountID = "";
            _preNarration = "";
            _preEntryDate = "";
            _preVoucherSeries = "";
            _preVoucherType = "";
            _preVoucherNumber = 0;
            _preVoucherDate = "";
            _prePurchaseBillNumber = "";
            _preAmountNetS = 0;
            _preAmountClearS = 0;
            _preAmountBillS = 0;
            _preAmountItemDiscountS = 0;
            _preAmountSpecialDiscountS = 0;
            _preSpecialDiscountPercentS = 0;
            _preAmountCashDiscountS = 0;
            _preCreditNoteDiscountPercentS = 0;
            _preAmountSchemeDiscountS = 0;
            _preAmountAddOnS = 0;
            _preAmountFreightS = 0;
            _preCashDiscountPercentageS = 0;
            _preAmountCreditNoteS = 0;
            _preAmountDebitNoteS = 0;
            _preRoundUpAmountS = 0;
            _preOctroiPercentageS = 0;
            _preAmountOctroiS = 0;
            _prePurchaseAmount5PercentVATS = 0;
            _preAmountVAT0PercentS = 0;
            _preAmountVAT5PercentS = 0;
            _prePurchaseAmount12point5PercentVATS = 0;
            _preAmountVAT12point5PercentS = 0;
            //   _prePurchaseAmount0PercentVATS = 0;
            _preDueDate = "";
            _preNumberofChallans = 0;
            _prePurchaseAmountZeroVATS = 0;
            _preStatementNumber = 0;
            _preVoucherSubType = "";
            _VoucherSubType = "";



            _GSTAmt0 = 0;
            _GSTAmtS5 = 0;
            _GSTAmtC5 = 0;
            _GSTAmtI5 = 0;
            _GSTS5 = 0;
            _GSTC5 = 0;
            _GSTI5 = 0;
            _GSTAmtS12 = 0;
            _GSTAmtC12 = 0;
            _GSTAmtI12 = 0;
            _GSTS12 = 0;
            _GSTC12 = 0;
            _GSTI12 = 0;
            _GSTAmtS18 = 0;
            _GSTAmtC18 = 0;
            _GSTAmtI18 = 0;
            _GSTS18 = 0;
            _GSTC18 = 0;
            _GSTI18 = 0;
            _GSTAmtS28 = 0;
            _GSTAmtC28 = 0;
            _GSTAmtI28 = 0;
            _GSTS28 = 0;
            _GSTC28 = 0;
            _GSTI28 = 0;

            _GSTSAmount = 0;
            _GSTCAmount = 0;
            _GSTSPurchaseAmount = 0;
            _GSTCPurchaseAmount = 0;
            _GSTPurchaseAmountZero = 0;



            _IfOMS = "N";
            _IfExemptedScheme = "N";

        }

        public override void DoValidate()
        {
            bool retValue = true;
            try
            {
                if (AccountID == "")
                    ValidationMessages.Add("Please Select Creditor");
                if (PurchaseBillNumber == "")
                    ValidationMessages.Add("Please enter the  PurchaseBillNumber");
                if (AccountID == "")
                    ValidationMessages.Add("Please enter the  AccountID");
                if (AmountNetS == 0)
                    ValidationMessages.Add("Can Not Save Zero Amount Purchase");
                if (TransactionText == "")
                    ValidationMessages.Add("Select Transaction Type");

                if (Id == "")
                    retValue = CheckForUniqueBillNumberforNew(PurchaseBillNumber, AccountID);
                else
                    retValue = CheckForUniqueBillNumberforEdit(Id, PurchaseBillNumber, AccountID);
                if (retValue == false)
                {
                    ValidationMessages.Add("Purchase Number Already Entered");

                }



                //if (VoucherType == FixAccounts.VoucherTypeForCreditStatementPurchase)
                //    retValue = CheckForStatementOver();
                //if (retValue == false)
                //{
                //    ValidationMessages.Add("Statement Over ..");
                //}

                if (AmountClearS > AmountNetS)
                {
                    ValidationMessages.Add("Amount Cleared :" + AmountClearS.ToString("#0.00") + ": is More Than Purchase Amount");
                }

                if (_IfCashPaid == "N")
                {
                    if (_ChequeNumber == "" && _BankID != "")
                        ValidationMessages.Add("Enter Cheque Number");
                    else
                        if (_ChequeNumber != "" && _BankID == "")
                        ValidationMessages.Add("Select Bank");
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        #region Public Methods
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBPurchase dbData = new DBPurchase();
                drow = dbData.ReadDetailsByID(Id);
                if (drow != null)
                {
                    if (drow["purchaseID"] != DBNull.Value)
                        Id = Convert.ToString(drow["purchaseID"]);
                    if (drow["EntryDate"] != DBNull.Value)
                        EntryDate = Convert.ToString(drow["EntryDate"]);
                    if (drow["VoucherSeries"] != DBNull.Value)
                        VoucherSeries = Convert.ToString(drow["VoucherSeries"]);
                    if (drow["VoucherType"] != DBNull.Value)
                        VoucherType = Convert.ToString(drow["VoucherType"]);
                    if (drow["VoucherNumber"] != DBNull.Value)
                        VoucherNumber = Convert.ToInt32(drow["VoucherNumber"]);
                    if (drow["VoucherDate"] != DBNull.Value)
                        VoucherDate = Convert.ToString(drow["VoucherDate"]);
                    if (drow["PurchaseBillNumber"] != DBNull.Value)
                        PurchaseBillNumber = Convert.ToString(drow["PurchaseBillNumber"]);
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = Convert.ToString(drow["AccountID"]);
                    if (drow["AmountNet"] != DBNull.Value)
                        AmountNetS = Convert.ToDouble(drow["AmountNet"]);
                    if (drow["AmountClear"] != DBNull.Value)
                        AmountClearS = Convert.ToDouble(drow["AmountClear"]);
                    if (drow["AmountGross"] != DBNull.Value)
                    {
                        AmountS = Convert.ToDouble(drow["AmountGross"]);
                        AmountBillS = AmountS;
                    }
                    if (drow["AmountItemDiscount"] != DBNull.Value)
                        AmountItemDiscountS = Convert.ToDouble(drow["AmountItemDiscount"]);
                    //if (drow["AmountSpecialDiscount"] != DBNull.Value)
                    //    AmountSpecialDiscountS = Convert.ToByte(drow["AmountSpecialDiscount"]);
                    if (drow["AmountSchemeDiscount"] != DBNull.Value)
                        AmountSchemeDiscountS = Convert.ToDouble(drow["AmountSchemeDiscount"]);
                    if (drow["AmountCashDiscount"] != DBNull.Value)
                        AmountCashDiscountS = Convert.ToDouble(drow["AmountCashDiscount"]);
                    if (drow["AmountAddOn"] != DBNull.Value)
                        AmountAddOnS = Convert.ToDouble(drow["AmountAddOn"]);
                    if (drow["AmountFreight"] != DBNull.Value)
                        AmountFreightS = Convert.ToDouble(drow["AmountFreight"]);
                    if (drow["CashDiscountPercentage"] != DBNull.Value)
                        CashDiscountPercentageS = Convert.ToDouble(drow["CashDiscountPercentage"]);
                    //if (drow["SpecialDiscountPercentage"] != DBNull.Value)
                    //    SpecialDiscountPercentS = Convert.ToDouble(drow["SpecialDiscountPercentage"]);
                    if (drow["AmountCreditNote"] != DBNull.Value)
                        AmountCreditNoteS = Convert.ToDouble(drow["AmountCreditNote"]);
                    if (drow["AmountDebitNote"] != DBNull.Value)
                        AmountDebitNoteS = Convert.ToDouble(drow["AmountDebitNote"]);
                    //if (drow["StatementNumber"] != DBNull.Value)
                    //    StatementNumber = Convert.ToInt32(drow["StatementNumber"]);
                    if (drow["RoundUpAmount"] != DBNull.Value)
                        RoundUpAmountS = Convert.ToDouble(drow["RoundUpAmount"]);
                    //if (drow["OctroiPercentage"] != DBNull.Value)
                    //    OctroiPercentageS = Convert.ToDouble(drow["OctroiPercentage"]);
                    //if (drow["AmountOctroi"] != DBNull.Value)
                    //    AmountOctroiS = Convert.ToDouble(drow["AmountOctroi"]);
                    if (drow["DueDate"] != DBNull.Value)
                        DueDate = Convert.ToString(drow["DueDate"]);
                    if (drow["Narration"] != DBNull.Value)
                        Narration = Convert.ToString(drow["Narration"]);
                    //if (drow["AmountVAT5Percent"] != DBNull.Value)
                    //    AmountVAT5PercentS = Convert.ToDouble(drow["AmountVAT5Percent"]);
                    //if (drow["AmountVAT12point5Percent"] != DBNull.Value)
                    //    AmountVAT12point5PercentS = Convert.ToDouble(drow["AmountVAT12point5Percent"]);
                    //if (drow["AmountVATOPercent"] != DBNull.Value)
                    //    VATOPercent = Convert.ToDouble(drow["AmountVATOPercent"]);
                    //if (drow["NumberofChallans"] != DBNull.Value)
                    //    NumberofChallans = Convert.ToInt32(drow["NumberofChallans"]);
                    //if (drow["AmountPurchaseZeroVAT"] != DBNull.Value)
                    //    PurchaseAmountZeroVATS = Convert.ToDouble(drow["AmountPurchaseZeroVAT"]);
                    //if (drow["AmountPurchase12point5PercentVAT"] != DBNull.Value)
                    //    PurchaseAmount12point5PercentVATS = Convert.ToDouble(drow["AmountPurchase12point5PercentVAT"]);
                    //if (drow["AmountPurchase5PercentVAT"] != DBNull.Value)
                    //    PurchaseAmount5PercentVATS = Convert.ToDouble(drow["AmountPurchase5PercentVAT"]);                   
                    if (drow["GSTS5"] != DBNull.Value)
                        GSTS5 = Convert.ToInt32(drow["GSTS5"]);
                    if (drow["GSTC5"] != DBNull.Value)
                        GSTC5 = Convert.ToInt32(drow["GSTC5"]);
                    if (drow["GSTS12"] != DBNull.Value)
                        GSTS12 = Convert.ToInt32(drow["GSTS12"]);
                    if (drow["GSTC12"] != DBNull.Value)
                        GSTC12 = Convert.ToInt32(drow["GSTC12"]);
                    if (drow["GSTS18"] != DBNull.Value)
                        GSTS18 = Convert.ToInt32(drow["GSTS18"]);
                    if (drow["GSTC18"] != DBNull.Value)
                        GSTC18 = Convert.ToInt32(drow["GSTC18"]);
                    if (drow["GSTS28"] != DBNull.Value)
                        GSTS28 = Convert.ToInt32(drow["GSTS28"]);
                    if (drow["GSTC28"] != DBNull.Value)
                        GSTC28 = Convert.ToInt32(drow["GSTC28"]);

                    if (drow["AmountGST0"] != DBNull.Value)
                        GSTAmt0 = Convert.ToInt32(drow["AmountGST0"]);
                    if (drow["AmountGSTS5"] != DBNull.Value)
                        GSTAmtS5 = Convert.ToInt32(drow["AmountGSTS5"]);
                    if (drow["AmountGSTC5"] != DBNull.Value)
                        GSTAmtC5 = Convert.ToInt32(drow["AmountGSTC5"]);
                    if (drow["AmountGSTS12"] != DBNull.Value)
                        GSTAmtS12 = Convert.ToInt32(drow["AmountGSTS12"]);
                    if (drow["AmountGSTC12"] != DBNull.Value)
                        GSTAmtC12 = Convert.ToInt32(drow["AmountGSTC12"]);
                    if (drow["AmountGSTS18"] != DBNull.Value)
                        GSTAmtS18 = Convert.ToInt32(drow["AmountGSTS18"]);
                    if (drow["AmountGSTC18"] != DBNull.Value)
                        GSTAmtC18 = Convert.ToInt32(drow["AmountGSTC18"]);
                    if (drow["AmountGSTS28"] != DBNull.Value)
                        GSTAmtS28 = Convert.ToInt32(drow["AmountGSTS28"]);
                    if (drow["AmountGSTC28"] != DBNull.Value)
                        GSTAmtC28 = Convert.ToInt32(drow["AmountGSTC28"]);


                    preAccountID = AccountID;
                    preNarration = Narration;
                    preEntryDate = EntryDate;
                    preVoucherSeries = VoucherSeries;
                    preVoucherType = VoucherType;
                    preVoucherNumber = VoucherNumber;
                    preVoucherDate = VoucherDate;
                    prePurchaseBillNumber = PurchaseBillNumber;
                    preAmountNetS = AmountNetS;
                    preAmountClearS = AmountClearS;
                    preAmountBillS = AmountBillS;
                    preAmountItemDiscountS = AmountItemDiscountS;
                    //preAmountSpecialDiscountS = AmountSpecialDiscountS;
                    //preSpecialDiscountPercentS = SpecialDiscountPercentS;
                    preAmountCashDiscountS = AmountCashDiscountS;
                    preCreditNoteDiscountPercentS = 0;
                    preAmountSchemeDiscountS = AmountSchemeDiscountS;
                    preAmountAddOnS = AmountAddOnS;
                    preAmountFreightS = AmountFreightS;
                    preCashDiscountPercentageS = CashDiscountPercentageS;
                    preAmountCreditNoteS = AmountCreditNoteS;
                    preAmountDebitNoteS = AmountDebitNoteS;
                    preRoundUpAmountS = RoundUpAmountS;
                    preOctroiPercentageS = OctroiPercentageS;
                    preAmountOctroiS = AmountOctroiS;
                    prePurchaseAmount5PercentVATS = PurchaseAmount5PercentVATS;
                    preAmountVAT0PercentS = 0;
                    preAmountVAT5PercentS = AmountVAT5PercentS;
                    prePurchaseAmount12point5PercentVATS = PurchaseAmount12point5PercentVATS;
                    preAmountVAT12point5PercentS = AmountVAT12point5PercentS;
                    prePurchaseAmountZeroVATS = PurchaseAmountZeroVATS;
                    preDueDate = DueDate;
                    //preNumberofChallans = NumberofChallans;                   
                    //preStatementNumber = StatementNumber;


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return retValue;
        }


        public bool ReadDetailsByIDForChanged()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBPurchase dbData = new DBPurchase();
                drow = dbData.ReadDetailsByIDForChanged(Id);
                if (drow != null)
                {
                    if (drow["changedID"] != DBNull.Value)
                        Id = Convert.ToString(drow["changedID"]);
                    if (drow["EntryDate"] != DBNull.Value)
                        EntryDate = Convert.ToString(drow["EntryDate"]);
                    if (drow["VoucherSeries"] != DBNull.Value)
                        VoucherSeries = Convert.ToString(drow["VoucherSeries"]);
                    if (drow["VoucherType"] != DBNull.Value)
                        VoucherType = Convert.ToString(drow["VoucherType"]);
                    if (drow["VoucherNumber"] != DBNull.Value)
                        VoucherNumber = Convert.ToInt32(drow["VoucherNumber"]);
                    if (drow["VoucherDate"] != DBNull.Value)
                        VoucherDate = Convert.ToString(drow["VoucherDate"]);
                    if (drow["PurchaseBillNumber"] != DBNull.Value)
                        PurchaseBillNumber = Convert.ToString(drow["PurchaseBillNumber"]);
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = Convert.ToString(drow["AccountID"]);
                    if (drow["AmountNet"] != DBNull.Value)
                        AmountNetS = Convert.ToDouble(drow["AmountNet"]);
                    if (drow["AmountClear"] != DBNull.Value)
                        AmountClearS = Convert.ToDouble(drow["AmountClear"]);
                    if (drow["AmountGross"] != DBNull.Value)
                    {
                        AmountS = Convert.ToDouble(drow["AmountGross"]);
                        AmountBillS = AmountS;
                    }
                    if (drow["AmountItemDiscount"] != DBNull.Value)
                        AmountItemDiscountS = Convert.ToDouble(drow["AmountItemDiscount"]);
                    //if (drow["AmountSpecialDiscount"] != DBNull.Value)
                    //    AmountSpecialDiscountS = Convert.ToByte(drow["AmountSpecialDiscount"]);
                    if (drow["AmountSchemeDiscount"] != DBNull.Value)
                        AmountSchemeDiscountS = Convert.ToDouble(drow["AmountSchemeDiscount"]);
                    if (drow["AmountCashDiscount"] != DBNull.Value)
                        AmountCashDiscountS = Convert.ToDouble(drow["AmountCashDiscount"]);
                    if (drow["AmountAddOn"] != DBNull.Value)
                        AmountAddOnS = Convert.ToDouble(drow["AmountAddOn"]);
                    if (drow["AmountFreight"] != DBNull.Value)
                        AmountFreightS = Convert.ToDouble(drow["AmountFreight"]);
                    if (drow["CashDiscountPercentage"] != DBNull.Value)
                        CashDiscountPercentageS = Convert.ToDouble(drow["CashDiscountPercentage"]);
                    //if (drow["SpecialDiscountPercentage"] != DBNull.Value)
                    //    SpecialDiscountPercentS = Convert.ToDouble(drow["SpecialDiscountPercentage"]);
                    if (drow["AmountCreditNote"] != DBNull.Value)
                        AmountCreditNoteS = Convert.ToDouble(drow["AmountCreditNote"]);
                    if (drow["AmountDebitNote"] != DBNull.Value)
                        AmountDebitNoteS = Convert.ToDouble(drow["AmountDebitNote"]);
                    //if (drow["StatementNumber"] != DBNull.Value)
                    //    StatementNumber = Convert.ToInt32(drow["StatementNumber"]);
                    if (drow["RoundUpAmount"] != DBNull.Value)
                        RoundUpAmountS = Convert.ToDouble(drow["RoundUpAmount"]);
                    if (drow["OctroiPercentage"] != DBNull.Value)
                        OctroiPercentageS = Convert.ToDouble(drow["OctroiPercentage"]);
                    if (drow["AmountOctroi"] != DBNull.Value)
                        AmountOctroiS = Convert.ToDouble(drow["AmountOctroi"]);
                    if (drow["DueDate"] != DBNull.Value)
                        DueDate = Convert.ToString(drow["DueDate"]);
                    if (drow["Narration"] != DBNull.Value)
                        Narration = Convert.ToString(drow["Narration"]);
                    if (drow["AmountVAT5Percent"] != DBNull.Value)
                        AmountVAT5PercentS = Convert.ToDouble(drow["AmountVAT5Percent"]);
                    if (drow["AmountVAT12point5Percent"] != DBNull.Value)
                        AmountVAT12point5PercentS = Convert.ToDouble(drow["AmountVAT12point5Percent"]);
                    if (drow["AmountVATOPercent"] != DBNull.Value)
                        VATOPercent = Convert.ToDouble(drow["AmountVATOPercent"]);
                    //if (drow["NumberofChallans"] != DBNull.Value)
                    //    NumberofChallans = Convert.ToInt32(drow["NumberofChallans"]);
                    if (drow["AmountPurchaseZeroVAT"] != DBNull.Value)
                        PurchaseAmountZeroVATS = Convert.ToDouble(drow["AmountPurchaseZeroVAT"]);
                    if (drow["AmountPurchase12point5PercentVAT"] != DBNull.Value)
                        PurchaseAmount12point5PercentVATS = Convert.ToDouble(drow["AmountPurchase12point5PercentVAT"]);
                    if (drow["AmountPurchase5PercentVAT"] != DBNull.Value)
                        PurchaseAmount5PercentVATS = Convert.ToDouble(drow["AmountPurchase5PercentVAT"]);


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return retValue;
        }

        public bool ReadDetailsByIDForDeleted()
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBPurchase dbData = new DBPurchase();
                drow = dbData.ReadDetailsByIDForDeleted(Id);
                if (drow != null)
                {
                    if (drow["purchaseID"] != DBNull.Value)
                        Id = Convert.ToString(drow["purchaseID"]);
                    if (drow["EntryDate"] != DBNull.Value)
                        EntryDate = Convert.ToString(drow["EntryDate"]);
                    if (drow["VoucherSeries"] != DBNull.Value)
                        VoucherSeries = Convert.ToString(drow["VoucherSeries"]);
                    if (drow["VoucherType"] != DBNull.Value)
                        VoucherType = Convert.ToString(drow["VoucherType"]);
                    if (drow["VoucherNumber"] != DBNull.Value)
                        VoucherNumber = Convert.ToInt32(drow["VoucherNumber"]);
                    if (drow["VoucherDate"] != DBNull.Value)
                        VoucherDate = Convert.ToString(drow["VoucherDate"]);
                    if (drow["PurchaseBillNumber"] != DBNull.Value)
                        PurchaseBillNumber = Convert.ToString(drow["PurchaseBillNumber"]);
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = Convert.ToString(drow["AccountID"]);
                    if (drow["AmountNet"] != DBNull.Value)
                        AmountNetS = Convert.ToDouble(drow["AmountNet"]);
                    if (drow["AmountClear"] != DBNull.Value)
                        AmountClearS = Convert.ToDouble(drow["AmountClear"]);
                    if (drow["AmountGross"] != DBNull.Value)
                    {
                        AmountS = Convert.ToDouble(drow["AmountGross"]);
                        AmountBillS = AmountS;
                    }
                    if (drow["AmountItemDiscount"] != DBNull.Value)
                        AmountItemDiscountS = Convert.ToDouble(drow["AmountItemDiscount"]);
                    //if (drow["AmountSpecialDiscount"] != DBNull.Value)
                    //    AmountSpecialDiscountS = Convert.ToByte(drow["AmountSpecialDiscount"]);
                    if (drow["AmountSchemeDiscount"] != DBNull.Value)
                        AmountSchemeDiscountS = Convert.ToDouble(drow["AmountSchemeDiscount"]);
                    if (drow["AmountCashDiscount"] != DBNull.Value)
                        AmountCashDiscountS = Convert.ToDouble(drow["AmountCashDiscount"]);
                    if (drow["AmountAddOn"] != DBNull.Value)
                        AmountAddOnS = Convert.ToDouble(drow["AmountAddOn"]);
                    if (drow["AmountFreight"] != DBNull.Value)
                        AmountFreightS = Convert.ToDouble(drow["AmountFreight"]);
                    if (drow["CashDiscountPercentage"] != DBNull.Value)
                        CashDiscountPercentageS = Convert.ToDouble(drow["CashDiscountPercentage"]);
                    //if (drow["SpecialDiscountPercentage"] != DBNull.Value)
                    //    SpecialDiscountPercentS = Convert.ToDouble(drow["SpecialDiscountPercentage"]);
                    if (drow["AmountCreditNote"] != DBNull.Value)
                        AmountCreditNoteS = Convert.ToDouble(drow["AmountCreditNote"]);
                    if (drow["AmountDebitNote"] != DBNull.Value)
                        AmountDebitNoteS = Convert.ToDouble(drow["AmountDebitNote"]);
                    //if (drow["StatementNumber"] != DBNull.Value)
                    //    StatementNumber = Convert.ToInt32(drow["StatementNumber"]);
                    if (drow["RoundUpAmount"] != DBNull.Value)
                        RoundUpAmountS = Convert.ToDouble(drow["RoundUpAmount"]);
                    if (drow["OctroiPercentage"] != DBNull.Value)
                        OctroiPercentageS = Convert.ToDouble(drow["OctroiPercentage"]);
                    if (drow["AmountOctroi"] != DBNull.Value)
                        AmountOctroiS = Convert.ToDouble(drow["AmountOctroi"]);
                    if (drow["DueDate"] != DBNull.Value)
                        DueDate = Convert.ToString(drow["DueDate"]);
                    if (drow["Narration"] != DBNull.Value)
                        Narration = Convert.ToString(drow["Narration"]);
                    if (drow["AmountVAT5Percent"] != DBNull.Value)
                        AmountVAT5PercentS = Convert.ToDouble(drow["AmountVAT5Percent"]);
                    if (drow["AmountVAT12point5Percent"] != DBNull.Value)
                        AmountVAT12point5PercentS = Convert.ToDouble(drow["AmountVAT12point5Percent"]);
                    if (drow["AmountVATOPercent"] != DBNull.Value)
                        VATOPercent = Convert.ToDouble(drow["AmountVATOPercent"]);
                    //if (drow["NumberofChallans"] != DBNull.Value)
                    //    NumberofChallans = Convert.ToInt32(drow["NumberofChallans"]);
                    if (drow["AmountPurchaseZeroVAT"] != DBNull.Value)
                        PurchaseAmountZeroVATS = Convert.ToDouble(drow["AmountPurchaseZeroVAT"]);
                    if (drow["AmountPurchase12point5PercentVAT"] != DBNull.Value)
                        PurchaseAmount12point5PercentVATS = Convert.ToDouble(drow["AmountPurchase12point5PercentVAT"]);
                    if (drow["AmountPurchase5PercentVAT"] != DBNull.Value)
                        PurchaseAmount5PercentVATS = Convert.ToDouble(drow["AmountPurchase5PercentVAT"]);


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return retValue;
        }





        public bool ReadDetailsByVouNumber(int vouno, string voutype, string vouseries)
        {
            bool retValue = false;
            try
            {
                DataRow drow = null;
                DBPurchase dbData = new DBPurchase();
                string mvousubtype = "";
                drow = dbData.ReadDetailsByVouNumber(vouno, voutype);
                if (drow != null)
                {
                    if (drow["VoucherSubType"] != DBNull.Value)
                        mvousubtype = drow["VoucherSubType"].ToString();
                    if (VoucherSubType == mvousubtype)
                    {
                        if (drow["purchaseID"] != DBNull.Value)
                            Id = Convert.ToString(drow["purchaseID"]);
                        if (drow["EntryDate"] != DBNull.Value)
                            EntryDate = Convert.ToString(drow["EntryDate"]);
                        if (drow["VoucherSeries"] != DBNull.Value)
                            VoucherSeries = Convert.ToString(drow["VoucherSeries"]);
                        if (drow["VoucherType"] != DBNull.Value)
                            VoucherType = Convert.ToString(drow["VoucherType"]);
                        if (drow["VoucherSubType"] != DBNull.Value)
                            VoucherSubType = Convert.ToString(drow["VoucherSubType"]);
                        if (drow["VoucherNumber"] != DBNull.Value)
                            VoucherNumber = Convert.ToInt32(drow["VoucherNumber"]);
                        if (drow["VoucherDate"] != DBNull.Value)
                            VoucherDate = Convert.ToString(drow["VoucherDate"]);
                        if (drow["PurchaseBillNumber"] != DBNull.Value)
                            PurchaseBillNumber = Convert.ToString(drow["PurchaseBillNumber"]);
                        if (drow["AccountID"] != DBNull.Value)
                            AccountID = Convert.ToString(drow["AccountID"]);
                        if (drow["AmountNet"] != DBNull.Value)
                            AmountNetS = Convert.ToDouble(drow["AmountNet"]);
                        if (drow["AmountClear"] != DBNull.Value)
                            AmountClearS = Convert.ToDouble(drow["AmountClear"]);
                        if (drow["AmountGross"] != DBNull.Value)
                        {
                            AmountS = Convert.ToDouble(drow["AmountGross"]);
                            AmountBillS = AmountS;
                        }
                        if (drow["AmountItemDiscount"] != DBNull.Value)
                            AmountItemDiscountS = Convert.ToDouble(drow["AmountItemDiscount"]);
                        //if (drow["AmountSpecialDiscount"] != DBNull.Value)
                        //    AmountSpecialDiscountS = Convert.ToByte(drow["AmountSpecialDiscount"]);
                        if (drow["AmountSchemeDiscount"] != DBNull.Value)
                            AmountSchemeDiscountS = Convert.ToDouble(drow["AmountSchemeDiscount"]);
                        if (drow["AmountCashDiscount"] != DBNull.Value)
                            AmountCashDiscountS = Convert.ToDouble(drow["AmountCashDiscount"]);
                        if (drow["AmountAddOn"] != DBNull.Value)
                            AmountAddOnS = Convert.ToDouble(drow["AmountAddOn"]);
                        if (drow["AmountFreight"] != DBNull.Value)
                            AmountFreightS = Convert.ToDouble(drow["AmountFreight"]);
                        if (drow["CashDiscountPercentage"] != DBNull.Value)
                            CashDiscountPercentageS = Convert.ToDouble(drow["CashDiscountPercentage"]);
                        if (drow["AmountCreditNote"] != DBNull.Value)
                            AmountCreditNoteS = Convert.ToDouble(drow["AmountCreditNote"]);
                        if (drow["AmountDebitNote"] != DBNull.Value)
                            AmountDebitNoteS = Convert.ToDouble(drow["AmountDebitNote"]);
                        //if (drow["StatementNumber"] != DBNull.Value)
                        //    StatementNumber = Convert.ToInt32(drow["StatementNumber"]);
                        if (drow["RoundUpAmount"] != DBNull.Value)
                            RoundUpAmountS = Convert.ToDouble(drow["RoundUpAmount"]);
                        if (drow["OctroiPercentage"] != DBNull.Value)
                            OctroiPercentageS = Convert.ToDouble(drow["OctroiPercentage"]);
                        if (drow["AmountOctroi"] != DBNull.Value)
                            AmountOctroiS = Convert.ToDouble(drow["AmountOctroi"]);
                        if (drow["DueDate"] != DBNull.Value)
                            DueDate = Convert.ToString(drow["DueDate"]);
                        if (drow["Narration"] != DBNull.Value)
                            Narration = Convert.ToString(drow["Narration"]);
                        if (drow["AmountVAT5Percent"] != DBNull.Value)
                            AmountVAT5PercentS = Convert.ToDouble(drow["AmountVAT5Percent"]);
                        if (drow["AmountVAT12point5Percent"] != DBNull.Value)
                            AmountVAT12point5PercentS = Convert.ToDouble(drow["AmountVAT12point5Percent"]);
                        if (drow["AmountVATOPercent"] != DBNull.Value)
                            VATOPercent = Convert.ToDouble(drow["AmountVATOPercent"]);
                        //if (drow["NumberofChallans"] != DBNull.Value)
                        //    NumberofChallans = Convert.ToInt32(drow["NumberofChallans"]);
                        if (drow["AmountPurchaseZeroVAT"] != DBNull.Value)
                            PurchaseAmountZeroVATS = Convert.ToDouble(drow["AmountPurchaseZeroVAT"]);
                        if (drow["AmountPurchase12point5PercentVAT"] != DBNull.Value)
                            PurchaseAmount12point5PercentVATS = Convert.ToDouble(drow["AmountPurchase12point5PercentVAT"]);
                        if (drow["AmountPurchase5PercentVAT"] != DBNull.Value)
                            PurchaseAmount5PercentVATS = Convert.ToDouble(drow["AmountPurchase5PercentVAT"]);
                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }


        public DataTable ReadPaymentDetailsByID()
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBPurchase dbp = new DBPurchase();
                dt = dbp.ReadPaymentDetailsByID(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }
        public DataTable ReadProductDetailsByID()
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBPurchase dbp = new DBPurchase();
                dt = dbp.ReadProductDetailsByIDPurchase(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return dt;
        }

        public DataTable ReadProductDetailsByIDForChanged()
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBPurchase dbp = new DBPurchase();
                dt = dbp.ReadProductDetailsByIDPurchaseForChanged(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return dt;
        }

        public DataTable ReadProductDetailsByIDForDeleted()
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBPurchase dbp = new DBPurchase();
                dt = dbp.ReadProductDetailsByIDPurchaseForDeleted(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return dt;
        }
        public DataRow IfStockIDFoundInStockTable(string stockID)
        {
            DBSsStock dbssstk = new DBSsStock();
            return dbssstk.IfStockIDFoundInStockTable(stockID);

        }
        public int GetAndUpdatePurchaseNumber(string vtype)
        {
            int vouno = 0;
            try
            {

                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetPurchase(vtype, General.ShopDetail.ShopVoucherSeries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return vouno;
        }
        public void GetPendingAmount(string accountid)
        {
            DataRow dr;
            DBPurchase dbp = new DBPurchase();
            try
            {
                dr = dbp.GetPendingAmount(accountid);
                TotalDebit = 0;
                TotalCredit = 0;
                if (dr != null)
                {
                    if (dr["Credit"] != DBNull.Value)
                        TotalCredit = Convert.ToDouble(dr["Credit"].ToString());
                    if (dr["Debit"] != DBNull.Value)
                        TotalDebit = Convert.ToDouble(dr["Debit"].ToString());
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        public double GetDNAmount(string accountid)
        {
            DataRow dr;
            double totalDN = 0;
            DBPurchase dbp = new DBPurchase();
            try
            {
                dr = dbp.GetDNAmount(accountid);

                if (dr != null)
                {
                    if (dr["AmountNet"] != DBNull.Value)
                        totalDN = Convert.ToDouble(dr["AmountNet"].ToString());
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return totalDN;
        }


        public void GetOpeningBalance(string accountid)
        {
            DataRow dr;
            try
            {
                DBAccount dbp = new DBAccount();
                dr = dbp.ReadDetailsByID(accountid);
                double mopdebit = 0;
                double mopcredit = 0;
                if (dr != null)
                {
                    if (dr["AccOpeningDebit"] != DBNull.Value)
                        mopdebit = Convert.ToDouble(dr["AccOpeningDebit"].ToString());
                    if (dr["AccOpeningCredit"] != DBNull.Value)
                        mopcredit = Convert.ToDouble(dr["AccOpeningCredit"].ToString());
                }
                OpeningBalance = mopdebit - mopcredit;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        public int AddDetails()
        {
            DBPurchase dbcrdb = new DBPurchase();
            return dbcrdb.AddDetails(Id, AccountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
           PurchaseBillNumber, AmountNetS, AmountClearS, AmountBillS, AmountItemDiscountS, AmountSpecialDiscountS,
           SpecialDiscountPercentS, AmountCashDiscountS, CreditNoteDiscountPercentS, AmountSchemeDiscountS,
           AmountAddOnS, AmountFreightS, CashDiscountPercentageS, AmountCreditNoteS, AmountDebitNoteS, RoundUpAmountS,
           OctroiPercentageS, AmountOctroiS, PurchaseAmount5PercentVATS, AmountVAT0PercentS, AmountVAT5PercentS,
           PurchaseAmount12point5PercentVATS, AmountVAT12point5PercentS, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, VoucherSubType,
           GSTAmt0, GSTAmtS5, GSTAmtS12, GSTAmtS18, GSTAmtS28, GSTAmtC5, GSTAmtC12, GSTAmtC18, GSTAmtC28,
           GSTS5, GSTS12, GSTS18, GSTS28, GSTC5, GSTC12, GSTC18, GSTC28, GSTAmtI5, GSTAmtI12, GSTAmtI18, GSTAmtI28, GSTI5, GSTI12, GSTI18, GSTI28, CreatedBy, CreatedDate, CreatedTime);
        }

        public Int32 AddChangedDetails()
        {
            DBPurchase dbcrdb = new DBPurchase();
            return dbcrdb.AddChangedDetails(Id, ChangedID, preAccountID, preNarration, preEntryDate, preVoucherSeries, preVoucherType, preVoucherNumber, preVoucherDate,
           prePurchaseBillNumber, preAmountNetS, preAmountClearS, preAmountBillS, preAmountItemDiscountS, preAmountSpecialDiscountS,
           preSpecialDiscountPercentS, preAmountCashDiscountS, preCreditNoteDiscountPercentS, preAmountSchemeDiscountS,
           preAmountAddOnS, preAmountFreightS, preCashDiscountPercentageS, preAmountCreditNoteS, preAmountDebitNoteS, preRoundUpAmountS,
           preOctroiPercentageS, preAmountOctroiS, prePurchaseAmount5PercentVATS, preAmountVAT0PercentS, preAmountVAT5PercentS,
           prePurchaseAmount12point5PercentVATS, preAmountVAT12point5PercentS, prePurchaseAmountZeroVATS, preDueDate, preNumberofChallans, preStatementNumber, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool AddDeletedDetails()
        {
            DBPurchase dbcrdb = new DBPurchase();
            return dbcrdb.AddDeletedDetails(Id, preAccountID, preNarration, preEntryDate, preVoucherSeries, preVoucherType, preVoucherNumber, preVoucherDate,
           prePurchaseBillNumber, preAmountNetS, preAmountClearS, preAmountBillS, preAmountItemDiscountS, preAmountSpecialDiscountS,
           preSpecialDiscountPercentS, preAmountCashDiscountS, preCreditNoteDiscountPercentS, preAmountSchemeDiscountS,
           preAmountAddOnS, preAmountFreightS, preCashDiscountPercentageS, preAmountCreditNoteS, preAmountDebitNoteS, preRoundUpAmountS,
           preOctroiPercentageS, preAmountOctroiS, prePurchaseAmount5PercentVATS, preAmountVAT0PercentS, preAmountVAT5PercentS,
           prePurchaseAmount12point5PercentVATS, preAmountVAT12point5PercentS, prePurchaseAmountZeroVATS, preDueDate, preNumberofChallans, preStatementNumber, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool AddProductDetailsSS()
        {
            DBPurchase dbpur = new DBPurchase();
            Id = String.IsNullOrEmpty(Id) ? "0" : Id;
            return dbpur.AddDetailsProductsSS(Convert.ToInt32(Id), ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercent,
                AmountSchemeDiscount, PurchaseVATPercent, ProductVATPercent, AmountPurchaseVAT, AmountProductVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT,
                IfTradeRateInclusiveOfVAT, Amount, AmountSplDiscountPerUnit, SplDiscountPercent, AmountZeroVAT, AmountCashDiscountPerUnit, IntStockID,
                ProductMargin, ProductMargin2, SerialNumber, PurScanCode, GSTPurchaseAmountZero, GSTSPurchaseAmount, GSTCPurchaseAmount, GSTSAmount, GSTCAmount, PriceToRetailer, ProfitPercent);
        }

        public bool AddChangedProductDetailsSS()
        {
            DBPurchase dbpur = new DBPurchase();
            return dbpur.AddChangedDetailsProductsSS(Id, ChangedID, ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercent,
                AmountSchemeDiscount, PurchaseVATPercent, ProductVATPercent, AmountPurchaseVAT, AmountProductVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT,
                IfTradeRateInclusiveOfVAT, Amount, AmountSplDiscountPerUnit, SplDiscountPercent, AmountZeroVAT, AmountCashDiscountPerUnit, StockID, ProductMargin, ProductMargin2, SerialNumber);
        }
        public bool AddDeletedProductDetailsSS()
        {
            DBPurchase dbpur = new DBPurchase();
            return dbpur.AddDeletedDetailsProductsSS(Id, ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, ReplacementQuantity, ItemDiscountPercent, AmountItemDiscount, SchemeDiscountPercent,
                AmountSchemeDiscount, PurchaseVATPercent, ProductVATPercent, AmountPurchaseVAT, AmountProductVAT, CSTPercent, AmountCST, IfMRPInclusiveOfVAT,
                IfTradeRateInclusiveOfVAT, Amount, AmountSplDiscountPerUnit, SplDiscountPercent, AmountZeroVAT, AmountCashDiscountPerUnit, StockID, ProductMargin, ProductMargin2, SerialNumber);
        }
        public string AddProductDetailsInStockTable()
        {
            DBPurchase dbpur = new DBPurchase();
            int intstockid = 0;
            string stockid = "";
            intstockid = dbpur.AddProductDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, ReplacementQuantity, PurchaseVATPercent,
                ProductVATPercent, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount, AccountID, PurchaseBillNumber,
                VoucherType, VoucherNumber, VoucherDate, ProdLoosePack, StockID, ProductMargin, PurScanCode, PriceToRetailer, ProfitPercent, Convert.ToInt32(Id));
            stockid = Convert.ToString(intstockid);
            return stockid;
        }




        public bool UpdateCreditDebitNoteAdjustedDetails(string crdbID, double mamtnet, string VouType, int VouNumber, string VouDate, string BillNumber, string puchaseid, string vouSeries)
        {
            DBCreditDebitNote dbcr = new DBCreditDebitNote();
            return dbcr.UpdateCreditDebitNoteAdjustedDetails(crdbID, mamtnet, VouType, VouNumber, VouDate, BillNumber, puchaseid, vouSeries);
        }

        public bool clearPreviousdebitcreditnotes(string purchaseID)
        {
            DBCreditDebitNote dbcr = new DBCreditDebitNote();
            return dbcr.clearPreviousdebitcreditnotes(purchaseID);
        }

        public bool UpdateCreditDebitNoteforTypeChange(string crdbID, double mamtnet, string VouType, int VouNumber, string VouDate, string BillNumber, string purchaseid)
        {
            DBCreditDebitNote dbcr = new DBCreditDebitNote();
            return dbcr.UpdateCreditDebitNoteforTypeChange(crdbID, mamtnet, VouType, VouNumber, VouDate, BillNumber, purchaseid);
        }


        public bool AddAccountDetails()
        {
            bool bRetValue = true;
            try
            {
                //DBAccountDetails dbpur = new DBAccountDetails();
                //if (VoucherType == FixAccounts.VoucherTypeForCashPurchase)
                //    CreditAccount = FixAccounts.AccountCash;
                //else
                //    CreditAccount = AccountID;
                //if (bRetValue == true &&  PurchaseAmountZeroVATS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountVATZeroPurchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount, PurchaseAmountZeroVATS, 0, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                ////if (bRetValue == true && PurchaseAmount5PercentVATS > 0)
                ////{
                ////    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                ////    DebitAccount = FixAccounts.AccountVat5Purchase;
                ////    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, PurchaseAccount, PurchaseAmount5PercentVATS, 0, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                ////}
                ////if (bRetValue == true && PurchaseAmount12point5PercentVATS > 0)
                ////{
                ////    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                ////    DebitAccount = FixAccounts.AccountVat12point5Purchase;
                ////    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, PurchaseAccount, PurchaseAmount12point5PercentVATS, 0, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                ////} // ss 17/10
                //if (bRetValue == true && AmountVAT5PercentS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountVatInput5Purchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount, AmountVAT5PercentS, 0, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true && AmountVAT12point5PercentS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountVatInput12point5Purchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount, AmountVAT12point5PercentS, 0, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration,"", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true && AmountItemDiscountS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountItemDiscountPurchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount, 0, AmountItemDiscountS, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true && AmountSpecialDiscountS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountSplDiscountPurchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount, 0, AmountSpecialDiscountS, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true && AmountCashDiscountS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountCashDiscountPurchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount, 0, AmountCashDiscountS, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true && AmountSchemeDiscountS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountSchemeDiscountPurchcase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount, 0, AmountSchemeDiscountS, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true && AmountAddOnS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountAddonPurchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount, AmountAddOnS, 0, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true &&  AmountFreightS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountFreightPurchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount,0,AmountFreightS, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true && RoundUpAmountS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountRoundoffPurchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount, RoundUpAmountS, 0, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //else if (bRetValue == true && RoundUpAmountS < 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountRoundoffPurchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount, 0, (RoundUpAmountS * -1), CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true && AmountCreditNoteS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountCreditNotePurchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount,AmountCreditNoteS,0, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true && AmountDebitNoteS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAccount = FixAccounts.AccountDebitNotePurchase;
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, DebitAccount,0,  AmountDebitNoteS, CreditAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (bRetValue == true && AmountNetS > 0)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    bRetValue = dbpur.AddDetailsForAccountsPurchase(Id, DetailId, CreditAccount, 0, AmountNetS, PurchaseAccount, VoucherType, VoucherNumber, VoucherDate, Narration, "", CreatedBy, CreatedDate, CreatedTime);
                //}
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return bRetValue;
        }


        public bool AddCashEntry()
        {
            bool retValue = false;
            try
            {
                //DBPurchase dbcrdb = new DBPurchase();
                //retValue = dbcrdb.AddCashEntry(CBId, CBVouType, CBVouNo, VoucherDate, AccountID, Narration, AmountNetS, CreatedBy, CreatedDate, CreatedTime);
                //if (retValue)
                //    retValue = dbcrdb.AddCashEntryDetails(CBId, Id, VoucherType, VoucherNumber, VoucherDate, AmountNetS);
                //DBAccountDetails dbab = new DBAccountDetails();
                //if (retValue)
                //{
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    retValue = dbab.AddDetailsForAccountsCashPayment(IntID, DetailId, AccountID, AmountNetS, 0, FixAccounts.AccountCash, CBVouType, CBVouNo, VoucherDate, Narration, CreatedBy, CreatedDate, CreatedTime);
                //}
                //if (retValue)
                //{
                //  //  EmailID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    retValue = dbab.AddDetailsForAccountsCashPayment(IntID, DetailId, FixAccounts.AccountCash, 0, AmountNetS, AccountID, CBVouType, CBVouNo, VoucherDate, Narration, CreatedBy, CreatedDate, CreatedTime);
                //}
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public bool AddBankEntry()
        {
            //SS
            bool retValue = false;
            //try
            //{
            //    DBPurchase dbcrdb = new DBPurchase();
            //    retValue = dbcrdb.AddBankEntry(CBId, CBVouType, CBVouNo, VoucherDate, AccountID, Narration, AmountNetS, ChequeNumber, ChequeDate, BankID, CreatedBy, CreatedDate, CreatedTime);
            //    if (retValue)
            //        retValue = dbcrdb.AddBankEntryDetails(CBId, Id, VoucherType, VoucherNumber, VoucherDate, AmountNetS);
            //EcoMart.DataLayer.DBAccountDetails dbab = new DBAccountDetails();
            //    if (retValue)
            //    {
            //        DetailId = GetIntID(); /* Guid.NewGuid().ToString().ToUpper().Replace("-", "");*/
            //        retValue = dbab.AddDetailsForAccountsCashPayment(IntID, DetailId, AccountID, AmountNetS, 0, BankID, CBVouType, CBVouNo, VoucherDate, Narration, CreatedBy, CreatedDate, CreatedTime);
            //    }
            //    if (retValue)
            //    {
            //        DetailId = _Bank.GetIntID(); /* Guid.NewGuid().ToString().ToUpper().Replace("-", "");*/
            //        retValue = dbab.AddDetailsForAccountsCashPayment(IntID, DetailId, BankID, 0, AmountNetS, AccountID, CBVouType, CBVouNo, VoucherDate, Narration, CreatedBy, CreatedDate, CreatedTime);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
            return retValue;
        }

        public bool DeleteAccountDetails()
        {
            bool bRetValue = false;
            try
            {
                DBAccountDetails dbpur = new DBAccountDetails();
                bRetValue = dbpur.DeleteAccountDetailsPurchase(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return bRetValue;

        }
        public string CheckForBatchMRPInStockTable()
        {

            string stockID = string.Empty;
            try
            {
                DBSsStock sstk = new DBSsStock();
                DataRow drow = null;
                drow = sstk.GetRecordByProductIDAndBatchNumberAndMRP(ProductID, Batchno, MRP);
                if (drow != null)
                {
                    if (drow["StockID"] != DBNull.Value)
                        stockID = drow["StockID"].ToString();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return stockID;
        }

        public string CheckForBatchMRPStockIDInStockTable()
        {

            string stockID = "";
            try
            {
                DBSsStock sstk = new DBSsStock();
                DataRow drow = null;
                drow = sstk.GetRecordByProductIDAndBatchNumberAndMRPAndStockID(ProductID, Batchno, MRP, StockID);
                if (drow != null)
                {
                    if (drow["StockID"] != DBNull.Value)
                        stockID = drow["StockID"].ToString();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return stockID;
        }

        public string CheckForPurchaseIdStockIDInStockTable()
        {
            string stockID = "";
            try
            {
                DBSsStock sstk = new DBSsStock();
                DataRow drow = null;
                drow = sstk.GetRecordByProductIDAndPurchaseIdAndStockID(ProductID, StockID, Id);
                if (drow != null)
                {
                    if (drow["StockID"] != DBNull.Value)
                        stockID = drow["StockID"].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return stockID;
        }

        public DataRow IFRecordFoundInStockTable()
        {
            DataRow dr = null;
            try
            {
                DBSsStock sstk = new DBSsStock();
                dr = sstk.GetRecordByProductIDAndBatchNumberAndMRP(ProductID, Batchno, MRP);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }
        public DataRow IFStockIDFoundInStockTable()
        {
            DataRow dr = null;
            try
            {
                DBSsStock sstk = new DBSsStock();
                dr = sstk.GetRecordByStockID(StockID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }


        public DataRow GetDetailsForProduct(int prodID)
        {
            DataRow dr = null;
            try
            {
                DBProduct sstk = new DBProduct();
                dr = sstk.GetDetailsForProduct(prodID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }


        public int GetCurrentClosingStock(String thisStockID)
        {
            int thisclosingstock = 0;
            try
            {
                DBSsStock sstk = new DBSsStock();
                DataRow drow = null;
                drow = sstk.GetCurrentClosingStockByThisStockID(thisStockID);
                if (drow != null)
                {
                    if (drow["ClosingStock"] != DBNull.Value)
                        thisclosingstock = Convert.ToInt32(drow["ClosingStock"].ToString());
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return thisclosingstock;
        }
        public string CheckStockForBatchMRPInStockTable()
        {
            DBSsStock sstk = new DBSsStock();
            DataRow drow = null;
            string ifrowfound = "N";
            try
            {
                drow = sstk.GetRecordByProductIDAndBatchNumberAndMRP(StockID, (Quantity + SchemeQuanity + ReplacementQuantity) * ProdLoosePack);
                if (drow != null)
                    ifrowfound = "Y";
                else
                    ifrowfound = "N";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return ifrowfound;

        }
        public DataTable GetStockForCheck()
        {
            DBSsStock cstk = new DBSsStock();
            DataTable stktbl = new DataTable();
            try
            {
                stktbl = cstk.GetStockforCheck();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return stktbl;
        }
        public bool CheckForStatementOver()
        {
            bool retVal = false;
            try
            {
                DBPurchase purb = new DBPurchase();
                retVal = purb.CheckForStatementOver(VoucherDate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retVal;
        }

        public bool CheckForUniqueBillNumberforEdit(string Id, string purbillno, string accid)
        {
            bool retVal = false;
            try
            {
                DBPurchase purb = new DBPurchase();
                retVal = purb.CheckforUniqueBillNumberforEdit(Id, purbillno, accid);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retVal;
        }
        public bool CheckForUniqueBillNumberforNew(string purbillno, string accid)
        {
            bool retVal = false;
            try
            {
                DBPurchase purb = new DBPurchase();
                retVal = purb.CheckforUniqueBillNumberforNew(purbillno, accid);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retVal;
        }
        public bool UpdatePurchaseIntblStock()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdatePurchaseStock(StockID, Quantity, SchemeQuanity, ReplacementQuantity, ProdLoosePack, 0, 0);

        }
        public bool UpdateProductDetailsInStockTable()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateProductDetailsInStockTable(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, ReplacementQuantity, PurchaseVATPercent,
                ProductVATPercent, IfMRPInclusiveOfVAT, IfTradeRateInclusiveOfVAT, Amount, AccountID, PurchaseBillNumber,
                VoucherType, VoucherNumber, VoucherDate, ProdLoosePack, StockID, ProductMargin, ProdLoosePack, PriceToRetailer, ProfitPercent);
        }

        public bool UpdatePurchaseIntblStockReduceFromTemp()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdatePurchaseIntblStockReduceFromTemp(StockID, Quantity, SchemeQuanity, ReplacementQuantity, ProdLoosePack);

        }

        public bool UpdateDetails()
        {
            DBPurchase dbcrdb = new DBPurchase();
            return dbcrdb.UpdateDetails(Id, AccountID, Narration, EntryDate, VoucherSeries, VoucherType, VoucherNumber, VoucherDate,
           PurchaseBillNumber, AmountNetS, AmountClearS, AmountBillS, AmountItemDiscountS, AmountSpecialDiscountS,
           SpecialDiscountPercentS, AmountCashDiscountS, CreditNoteDiscountPercentS, AmountSchemeDiscountS,
           AmountAddOnS, AmountFreightS, CashDiscountPercentageS, AmountCreditNoteS, AmountDebitNoteS, RoundUpAmountS,
           OctroiPercentageS, AmountOctroiS, PurchaseAmount5PercentVATS, AmountVAT0PercentS, AmountVAT5PercentS,
           PurchaseAmount12point5PercentVATS, AmountVAT12point5PercentS, PurchaseAmountZeroVATS, DueDate, NumberofChallans, StatementNumber, VoucherSubType,
           GSTAmt0, GSTAmtS5, GSTAmtS12, GSTAmtS18, GSTAmtS28, GSTAmtC5, GSTAmtC12, GSTAmtC18, GSTAmtC28,
           GSTS5, GSTS12, GSTS18, GSTS28, GSTC5, GSTC12, GSTC18, GSTC28, GSTAmtI5, GSTAmtI12, GSTAmtI18, GSTAmtI28, GSTI5, GSTI12, GSTI18, GSTI28, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool UpdateDetailsForTypeChange()
        {
            DBPurchase dbcrdb = new DBPurchase();
            return dbcrdb.UpdateDetailsForTypeChange(Id, VoucherType, VoucherNumber, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool UpdatePurchaseStockInMasterProduct()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdatePurchaseStockInmasterProduct(ProductID, (Quantity + SchemeQuanity + ReplacementQuantity) * ProdLoosePack, ProductMargin, 0);
        }

        public bool UpdateLastPurhcaseDataInMasterProduct()
        {

            DBProduct dbprod = new DBProduct();
            return dbprod.UpdatePurchaseDataInmasterProduct(ProductID, PurchaseBillNumber, VoucherDate, AccountID, VoucherType,
                VoucherNumber, PurchaseRate, TradeRate, SaleRate, MRP, PurchaseVATPercent, CSTPercent, AmountCST, SchemeDiscountPercent, PriceToRetailer,
            AmountSchemeDiscount, ItemDiscountPercent, Expiry, ExpiryDate, Batchno, ShelfID, StockID);
            // return true;

        }

        public bool UpdatePurchaseStockInmasterProductReduceFromTemp()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdatePurchaseStockInmasterProductReduceFromTemp(ProductID, (Quantity + SchemeQuanity + ReplacementQuantity) * ProdLoosePack);
        }

        public void RemoveFromShortList(int ProductID)
        {

            DBPurchase dbpur = new DBPurchase();
            DataRow dr = null;
            try
            {
                dr = dbpur.CheckProductInShortList(ProductID);
                if (dr != null)
                    dbpur.RemoveFromShortList(ProductID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void GetFirstAndSecondCreditor(int ProductID)
        {
            string fcreditor = "";
            string screditor = "";
            DBPurchase dbpur = new DBPurchase();
            DataRow dr = null;
            try
            {
                dr = dbpur.GetFirstAndSecondCreditor(ProductID);
                if (dr != null)
                {
                    if (dr["ProdPartyID_1"] != DBNull.Value)
                        fcreditor = dr["ProdPartyID_1"].ToString();

                    if (dr["ProdPartyID_2"] != DBNull.Value)
                        screditor = dr["ProdPartyID_2"].ToString();

                    FirstCreditor = fcreditor;
                    SecondCreditor = screditor;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public void FillFirstCreditorInMasterProduct()
        {
            DBPurchase dbpur = new DBPurchase();

            try
            {
                dbpur.FillFirstCreditorInMasterProduct(ProductID, AccountID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public void FillSecondCreditorInMasterProduct()
        {
            DBPurchase dbpur = new DBPurchase();

            try
            {
                dbpur.FillSecondCreditorInMasterProduct(ProductID, AccountID);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public bool DeleteDetails()
        {
            DBPurchase dbData = new DBPurchase();
            return dbData.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBPurchase dbpur = new DBPurchase();
            return dbpur.DeletePreviousRecords(Id);
        }

        #endregion

        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }

        #region Public Methods
        public DataTable GetOverviewData()
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewData();
        }
        public DataTable GetOverviewDataForWithoutStockSearch()
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForWithoutStockSearch();
        }
        public DataTable GetOverviewDataForPurchaseRegister(string fromdate, string todate, string voutype)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForPurchaseRegister(fromdate, todate, voutype);
        }
        public DataTable GetOverviewDataForPurchaseRegister(string fromdate, string todate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForPurchaseRegister(fromdate, todate);
        }
        public DataTable GetOverviewDataForVATReport()
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReport();
        }

        public DataTable GetOverviewDataForVATReportOtherDetails()
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReportOtherDetails();
        }
        public DataTable GetOverviewDataForVATReportDATE()
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReportDATE();
        }

        public DataTable GetOverviewDataForVATReportDATEALL()
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReportDATEALL();
        }

        public DataTable GetOverviewDataForVATReportMONTH(string mfromdate, string mtodate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReportMONTH(mfromdate, mtodate);
        }

        public DataTable GetOverviewDataForVATReportMONTHALL(string mfromdate, string mtodate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReportMONTHALL(mfromdate, mtodate);
        }

        public DataTable GetOverviewDataForVATReportTIN(string mfromdate, string mtodate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReportTIN(mfromdate, mtodate);
        }
        public DataTable GetOverviewDataForLastPurchase(int ProductID)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForLastPurchase(ProductID);
        }
        public DataTable GetPurchase()
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetPurchase();
        }

        public DataTable GetProductPurchased(string purchaseID)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetProductPurchased(purchaseID);
        }

        public DataTable GetOverviewDataForPurchase(string AccID, string ClearedInID)
        {
            DBCreditNoteStock dbStock = new DBCreditNoteStock();
            return dbStock.GetOverviewDataForPurchase(AccID, ClearedInID);
        }

        public DataTable GetOverviewDataForProductList()
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForProductList();
        }

        public DataTable GetPurchaseDataProductWise(int ProductID)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetPurchaseDataProductWise(ProductID);
        }

        public DataTable GetPurchaseDataProductWiseWithScheme(int ProductID)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetPurchaseDataProductWiseWithScheme(ProductID);
        }
        public DataTable GetOverviewDataForProductBatchList(int ProductID, string mbatch, double mrp)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForProductBatchList(ProductID, mbatch, mrp);
        }

        public DataTable GetOverviewDataForPartyProductList(string PartyID, int ProductID)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForPartyProductList(PartyID, ProductID);
        }

        public DataTable GetOverviewDataForPartywiseBills(string PartyID, string fromDate, string toDate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForPartywiseBills(PartyID, fromDate, toDate);
        }

        public DataTable GetOverviewDataForPartywiseBillsForStatements(string PartyID, string fromDate, string toDate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForPartywiseBillsForStatements(PartyID, fromDate, toDate);
        }

        public DataTable GetOverviewDataForPartywiseStatementsView(int statementNumber, string voucherSeries)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForPartywiseStatementsView(statementNumber, voucherSeries);
        }

        public DataTable GetOverviewDataForDiscount()
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForDiscount();
        }
        public DataTable GetOverviewDataForGSTReport(string fromdate, string todate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForGSTReport(fromdate, todate);
        }
        public DataTable GetOverviewDataForGSTReportHSN(string fromdate, string todate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForGSTReportHSN(fromdate, todate);
        }
        public DataTable GetOverviewDataForAllPartySummary(string fromdate, string todate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForAllPartySummary(fromdate, todate);
        }
        public DataTable GetOverviewDataCategory(string mfromdate, string mtodate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataCategory(mfromdate, mtodate);
        }
        public DataTable GetOverviewDataCompany(string CompanyID, string mfromdate, string mtodate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataCompany(CompanyID, mfromdate, mtodate);
        }
        public DataTable GetOverviewDataNewProducts(string fromdate, string todate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataNewProducts(fromdate, todate);
        }

        public Int32 GetIntID()
        {
            int maxid;
            maxid = 1;
            DBPurchase dbPur = new DBPurchase();
            DataRow idrow = dbPur.GetMaxID();
            if (idrow != null)
            {
                if (idrow["maxid"] != null && idrow["maxid"].ToString() != string.Empty)
                {
                    maxid = Convert.ToInt32(idrow["maxid"]) + 1;
                }
            }
            return maxid;

        }
        public Int32 GetChangedIntID()
        {
            int maxid;
            maxid = 1;
            DBPurchase dbPur = new DBPurchase();
            DataRow idrow = dbPur.GetChangedMaxID();
            if (idrow != null)
            {
                if (idrow["maxid"] != null && idrow["maxid"].ToString() != string.Empty)
                {
                    maxid = Convert.ToInt32(idrow["maxid"]) + 1;
                }
            }
            return maxid;

        }
        #endregion

        # region PurchaseReports


        # endregion


        internal void UpdateDetailsInDetailCreditDebitNote()
        {
            throw new NotImplementedException();
        }

        internal void UpdateDetailsInDetailSale()
        {
            throw new NotImplementedException();
        }

        public DataTable FillScheduleH1ProductList()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetOverviewDataForScheduleH1();
        }

        public DataTable GetOverviewDataForDiscount(string mfromdate, string mtodate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForDiscount(mfromdate, mtodate);
        }


        public void SavePartyMSCDACodeInMasterAccount(string ID, string Code, string alliedCode)
        {
            DBAccount dbac = new DBAccount();
            dbac.SavePartyMSCDACodeInMasterAccount(ID, Code, alliedCode);
        }

        public bool DeletePreviousRecordsFromtblBarCode()
        {
            DBPurchase dbData = new DBPurchase();
            return dbData.DeletePreviousRecordsFromtblBarCode();
        }

        public DataTable GetOverviewDataForVATReport(string fromdate, string todate, string voutype)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReport(fromdate, todate, voutype);
        }
        public DataTable GetOverviewDataForVATReport(string fromdate, string todate, string voutype, string accountID)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReport(fromdate, todate, voutype, accountID);
        }
        public DataTable GetOverviewDataForVATReportOtherDetails(string fromdate, string todate, string voutype)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReportOtherDetails(fromdate, todate, voutype);
        }
        public DataTable GetOverviewDataForVATReportDATE(string mfromdate, string mtodate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForVATReportDATE(mfromdate, mtodate);
        }



        public string CheckForBatchMRPStockIDInStockTable_stock()
        {
            string stockID = string.Empty;
            try
            {
                DBSsStock sstk = new DBSsStock();
                DataRow drow = null;
                drow = sstk.GetRecordByProductIDAndBatchNumberAndMRPAndStockID_stock(ProductID, Batchno, MRP, AccountID);
                if (drow != null)
                {
                    if (drow["StockID"] != DBNull.Value)
                        stockID = drow["StockID"].ToString();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return stockID;
        }

        public string oldaccountIDFind()
        {
            string stockID = string.Empty;
            try
            {
                DBSsStock sstk = new DBSsStock();
                DataRow drow = null;
                drow = sstk.GetRecord_oldaccountID(StockID);
                if (drow != null)
                {
                    if (drow["LastPurchaseAccountId"] != DBNull.Value)
                        stockID = drow["LastPurchaseAccountId"].ToString();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return stockID;
        }

        public DataRow GetDuplicateBarcode(string Barcode)
        {
            DBPurchase dbprod = new DBPurchase();
            return dbprod.GetDuplicateBarcode(Barcode);
        }


        public DataRow GetFirstRecord()
        {
            DataRow dr = null;
            try
            {
                DBPurchase dbp = new DBPurchase();
                dr = dbp.GetFirstRecord(VoucherType, VoucherSubType, VoucherSeries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }
        public void GetLastRecordForPurchase(string vouType, string vouSubType, string vouSeries)
        {
            DataRow dr;
            try
            {
                DBPurchase dbp = new DBPurchase();
                dr = dbp.GetLastRecordForPurchase(vouType, vouSubType, vouSeries);
                if (dr != null && dr["PurchaseID"] != null)
                {

                    Id = dr["PurchaseID"].ToString();

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        public int GetLastVoucherNumber(string vouType, string subType, string vouSeries)
        {
            DataRow dr;
            int lastvouno = 0;
            try
            {
                DBPurchase dbp = new DBPurchase();
                dr = dbp.GetLastVoucherNumber(vouType, subType, vouSeries);
                if (dr != null)
                {

                    lastvouno = Convert.ToInt32(dr["VoucherNumber"].ToString());

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return lastvouno;
        }



        public string GetScanGodeForCurrentBatch(int ProductID)
        {
            string mproductScancode = "";
            string mscancode = "";
            int iscancode = 0;
            DBPurchase dbp = new DBPurchase();
            DataRow dr = dbp.GetProductScancode(ProductID);
            if (dr != null)
            {
                mproductScancode = dr["ScannedBarcode"].ToString();
            }
            if (mproductScancode != string.Empty)
            {
                DataTable dt = dbp.GetAllBatchNumbersForScanCode(ProductID);
                foreach (DataRow drr in dt.Rows)
                {
                    if (drr["Scancode"] != DBNull.Value && drr["Scancode"].ToString() != string.Empty)
                    {
                        iscancode = Convert.ToInt32(drr["Scancode"].ToString().Substring(6, 4));
                    }
                }
                if (iscancode == 0)
                    iscancode = 1000;
                iscancode += 1;
                mscancode = string.Concat(mproductScancode, iscancode.ToString());
            }
            return mscancode;
        }



        public void SavePartyAIOCDACodeInMasterAccount(string ID, string Code, string alliedCode)
        {
            DBAccount dbac = new DBAccount();
            dbac.SavePartyAIOCDACodeInMasterAccount(ID, Code, alliedCode);
        }
        public void SaveProductsintblbillimportlink(string guid, string DistributorID, string distributorProductID, string retailerProductID)
        {
            bool retValue = false;
            DBPurchase dbp = new DBPurchase();
            retValue = dbp.CheckforProductforDistributorID(DistributorID, distributorProductID);
            if (retValue == false)
            {
                dbp.AddLinkIntblbillimportlink(guid, DistributorID, distributorProductID, retailerProductID);
            }
        }
        public void SavePurchaseBillformat()
        {
            DBPurchase dbp = new DBPurchase();
            dbp.SavePurchaseBillFormat(PurchaseBillFormat, AccountID);
        }
        public bool UpdatePurchaseOrderStockist()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdatePurchaseOrderStockist(ProductID, (Quantity + SchemeQuanity + ReplacementQuantity));
        }

        public bool UpdatePurchaseOrderCNF()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdatePurchaseOrderCNF(ProductID, (Quantity + SchemeQuanity + ReplacementQuantity));
        }

        public bool UpdatePurchaseOrderEcoMart()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdatePurchaseOrderEcoMart(ProductID, (Quantity + SchemeQuanity + ReplacementQuantity));
        }



        public DataRow ReadDetailsByVouNumber(int vouno, string voutype, string vouSeries, string vousubtype)
        {
            DataRow drow = null;
            try
            {

                DBPurchase dbData = new DBPurchase();
                string mvousubtype = "";
                drow = dbData.ReadDetailsByVouNumber(vouno, voutype, vouSeries, vousubtype);
                if (drow != null)
                {
                    if (drow["VoucherSubType"] != DBNull.Value)
                        mvousubtype = drow["VoucherSubType"].ToString();
                    if (VoucherSubType == mvousubtype)
                    {
                        if (drow["purchaseID"] != DBNull.Value)
                            Id = Convert.ToString(drow["purchaseID"]);
                        if (drow["EntryDate"] != DBNull.Value)
                            EntryDate = Convert.ToString(drow["EntryDate"]);
                        if (drow["VoucherSeries"] != DBNull.Value)
                            VoucherSeries = Convert.ToString(drow["VoucherSeries"]);
                        if (drow["VoucherType"] != DBNull.Value)
                            VoucherType = Convert.ToString(drow["VoucherType"]);
                        if (drow["VoucherSubType"] != DBNull.Value)
                            VoucherSubType = Convert.ToString(drow["VoucherSubType"]);
                        if (drow["VoucherNumber"] != DBNull.Value)
                            VoucherNumber = Convert.ToInt32(drow["VoucherNumber"]);
                        if (drow["VoucherDate"] != DBNull.Value)
                            VoucherDate = Convert.ToString(drow["VoucherDate"]);
                        if (drow["PurchaseBillNumber"] != DBNull.Value)
                            PurchaseBillNumber = Convert.ToString(drow["PurchaseBillNumber"]);
                        if (drow["AccountID"] != DBNull.Value)
                            AccountID = Convert.ToString(drow["AccountID"]);
                        if (drow["AmountNet"] != DBNull.Value)
                            AmountNetS = Convert.ToDouble(drow["AmountNet"]);
                        if (drow["AmountClear"] != DBNull.Value)
                            AmountClearS = Convert.ToDouble(drow["AmountClear"]);
                        if (drow["AmountGross"] != DBNull.Value)
                        {
                            AmountS = Convert.ToDouble(drow["AmountGross"]);
                            AmountBillS = AmountS;
                        }
                        if (drow["AmountItemDiscount"] != DBNull.Value)
                            AmountItemDiscountS = Convert.ToDouble(drow["AmountItemDiscount"]);
                        //if (drow["AmountSpecialDiscount"] != DBNull.Value)
                        //    AmountSpecialDiscountS = Convert.ToByte(drow["AmountSpecialDiscount"]);
                        if (drow["AmountSchemeDiscount"] != DBNull.Value)
                            AmountSchemeDiscountS = Convert.ToDouble(drow["AmountSchemeDiscount"]);
                        if (drow["AmountCashDiscount"] != DBNull.Value)
                            AmountCashDiscountS = Convert.ToDouble(drow["AmountCashDiscount"]);
                        if (drow["AmountFreight"] != DBNull.Value)
                            AmountFreightS = Convert.ToDouble(drow["AmountFreight"]);
                        if (drow["AmountLess"] != DBNull.Value)
                            AmountLessS = Convert.ToDouble(drow["AmountLess"]);
                        if (drow["CashDiscountPercentage"] != DBNull.Value)
                            CashDiscountPercentageS = Convert.ToDouble(drow["CashDiscountPercentage"]);
                        if (drow["AmountCreditNote"] != DBNull.Value)
                            AmountCreditNoteS = Convert.ToDouble(drow["AmountCreditNote"]);
                        if (drow["AmountDebitNote"] != DBNull.Value)
                            AmountDebitNoteS = Convert.ToDouble(drow["AmountDebitNote"]);
                        //if (drow["StatementNumber"] != DBNull.Value)
                        //    StatementNumber = Convert.ToInt32(drow["StatementNumber"]);
                        if (drow["RoundUpAmount"] != DBNull.Value)
                            RoundUpAmountS = Convert.ToDouble(drow["RoundUpAmount"]);
                        if (drow["OctroiPercentage"] != DBNull.Value)
                            OctroiPercentageS = Convert.ToDouble(drow["OctroiPercentage"]);
                        if (drow["AmountOctroi"] != DBNull.Value)
                            AmountOctroiS = Convert.ToDouble(drow["AmountOctroi"]);
                        if (drow["DueDate"] != DBNull.Value)
                            DueDate = Convert.ToString(drow["DueDate"]);
                        if (drow["Narration"] != DBNull.Value)
                            Narration = Convert.ToString(drow["Narration"]);
                        if (drow["AmountVAT5Percent"] != DBNull.Value)
                            AmountVAT5PercentS = Convert.ToDouble(drow["AmountVAT5Percent"]);
                        if (drow["AmountVAT12point5Percent"] != DBNull.Value)
                            AmountVAT12point5PercentS = Convert.ToDouble(drow["AmountVAT12point5Percent"]);
                        if (drow["AmountVATOPercent"] != DBNull.Value)
                            VATOPercent = Convert.ToDouble(drow["AmountVATOPercent"]);
                        //if (drow["NumberofChallans"] != DBNull.Value)
                        //    NumberofChallans = Convert.ToInt32(drow["NumberofChallans"]);
                        if (drow["AmountPurchaseZeroVAT"] != DBNull.Value)
                            PurchaseAmountZeroVATS = Convert.ToDouble(drow["AmountPurchaseZeroVAT"]);
                        if (drow["AmountPurchase12point5PercentVAT"] != DBNull.Value)
                            PurchaseAmount12point5PercentVATS = Convert.ToDouble(drow["AmountPurchase12point5PercentVAT"]);
                        if (drow["AmountPurchase5PercentVAT"] != DBNull.Value)
                            PurchaseAmount5PercentVATS = Convert.ToDouble(drow["AmountPurchase5PercentVAT"]);
                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return drow;
        }

        public DataTable GetOverviewDataForPartywiseOutstandingPurchaseReportforParty(string accID, string fromdate, string todate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForPartywiseOutstandingPurchaseReportforParty(accID, fromdate, todate);
        }
        public DataTable GetOverviewDataForPartywiseOutstandingPurchaseReportAll(string fromdate, string todate)
        {
            DBPurchase dbPur = new DBPurchase();
            return dbPur.GetOverviewDataForPartywiseOutstandingPurchaseReportAll(fromdate, todate);
        }
        public DataTable ReadPaymentDetailsStatementByID()
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBPurchase dbp = new DBPurchase();
                dt = dbp.ReadPaymentDetailsStatementsByID(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public void ReadDetailsFromAzure()
        {
            //bool retValue = false;
            try
            {
                DataRow drow = null;
                DBPurchase dbData = new DBPurchase();
                drow = dbData.ReadDetailsFromAzue();
                if (drow != null)
                {
                    if (drow["purchaseID"] != DBNull.Value)
                        Id = Convert.ToString(drow["purchaseID"]);
                    if (drow["EntryDate"] != DBNull.Value)
                        EntryDate = Convert.ToString(drow["EntryDate"]);
                    if (drow["VoucherSeries"] != DBNull.Value)
                        VoucherSeries = Convert.ToString(drow["VoucherSeries"]);
                    if (drow["VoucherType"] != DBNull.Value)
                        VoucherType = Convert.ToString(drow["VoucherType"]);
                    if (drow["VoucherNumber"] != DBNull.Value)
                        VoucherNumber = Convert.ToInt32(drow["VoucherNumber"]);
                    if (drow["VoucherDate"] != DBNull.Value)
                        VoucherDate = Convert.ToString(drow["VoucherDate"]);
                    if (drow["PurchaseBillNumber"] != DBNull.Value)
                        PurchaseBillNumber = Convert.ToString(drow["PurchaseBillNumber"]);
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = Convert.ToString(drow["AccountID"]);
                    if (drow["AmountNet"] != DBNull.Value)
                        AmountNetS = Convert.ToDouble(drow["AmountNet"]);
                    if (drow["AmountClear"] != DBNull.Value)
                        AmountClearS = Convert.ToDouble(drow["AmountClear"]);
                    if (drow["AmountGross"] != DBNull.Value)
                    {
                        AmountS = Convert.ToDouble(drow["AmountGross"]);
                        AmountBillS = AmountS;
                    }
                    if (drow["AmountItemDiscount"] != DBNull.Value)
                        AmountItemDiscountS = Convert.ToDouble(drow["AmountItemDiscount"]);
                    //if (drow["AmountSpecialDiscount"] != DBNull.Value)
                    //    AmountSpecialDiscountS = Convert.ToByte(drow["AmountSpecialDiscount"]);
                    if (drow["AmountSchemeDiscount"] != DBNull.Value)
                        AmountSchemeDiscountS = Convert.ToDouble(drow["AmountSchemeDiscount"]);
                    if (drow["AmountCashDiscount"] != DBNull.Value)
                        AmountCashDiscountS = Convert.ToDouble(drow["AmountCashDiscount"]);
                    if (drow["AmountAddOn"] != DBNull.Value)
                        AmountAddOnS = Convert.ToDouble(drow["AmountAddOn"]);
                    if (drow["AmountFreight"] != DBNull.Value)
                        AmountFreightS = Convert.ToDouble(drow["AmountFreight"]);
                    if (drow["CashDiscountPercentage"] != DBNull.Value)
                        CashDiscountPercentageS = Convert.ToDouble(drow["CashDiscountPercentage"]);
                    //if (drow["SpecialDiscountPercentage"] != DBNull.Value)
                    //    SpecialDiscountPercentS = Convert.ToDouble(drow["SpecialDiscountPercentage"]);
                    if (drow["AmountCreditNote"] != DBNull.Value)
                        AmountCreditNoteS = Convert.ToDouble(drow["AmountCreditNote"]);
                    if (drow["AmountDebitNote"] != DBNull.Value)
                        AmountDebitNoteS = Convert.ToDouble(drow["AmountDebitNote"]);
                    //if (drow["StatementNumber"] != DBNull.Value)
                    //    StatementNumber = Convert.ToInt32(drow["StatementNumber"]);
                    if (drow["RoundUpAmount"] != DBNull.Value)
                        RoundUpAmountS = Convert.ToDouble(drow["RoundUpAmount"]);
                    //if (drow["OctroiPercentage"] != DBNull.Value)
                    //    OctroiPercentageS = Convert.ToDouble(drow["OctroiPercentage"]);
                    //if (drow["AmountOctroi"] != DBNull.Value)
                    //    AmountOctroiS = Convert.ToDouble(drow["AmountOctroi"]);
                    if (drow["DueDate"] != DBNull.Value)
                        DueDate = Convert.ToString(drow["DueDate"]);
                    if (drow["Narration"] != DBNull.Value)
                        Narration = Convert.ToString(drow["Narration"]);
                    //if (drow["AmountVAT5Percent"] != DBNull.Value)
                    //    AmountVAT5PercentS = Convert.ToDouble(drow["AmountVAT5Percent"]);
                    //if (drow["AmountVAT12point5Percent"] != DBNull.Value)
                    //    AmountVAT12point5PercentS = Convert.ToDouble(drow["AmountVAT12point5Percent"]);
                    //if (drow["AmountVATOPercent"] != DBNull.Value)
                    //    VATOPercent = Convert.ToDouble(drow["AmountVATOPercent"]);
                    //if (drow["NumberofChallans"] != DBNull.Value)
                    //    NumberofChallans = Convert.ToInt32(drow["NumberofChallans"]);
                    //if (drow["AmountPurchaseZeroVAT"] != DBNull.Value)
                    //    PurchaseAmountZeroVATS = Convert.ToDouble(drow["AmountPurchaseZeroVAT"]);
                    //if (drow["AmountPurchase12point5PercentVAT"] != DBNull.Value)
                    //    PurchaseAmount12point5PercentVATS = Convert.ToDouble(drow["AmountPurchase12point5PercentVAT"]);
                    //if (drow["AmountPurchase5PercentVAT"] != DBNull.Value)
                    //    PurchaseAmount5PercentVATS = Convert.ToDouble(drow["AmountPurchase5PercentVAT"]);                   
                    if (drow["GSTS5"] != DBNull.Value)
                        GSTS5 = Convert.ToInt32(drow["GSTS5"]);
                    if (drow["GSTC5"] != DBNull.Value)
                        GSTC5 = Convert.ToInt32(drow["GSTC5"]);
                    if (drow["GSTS12"] != DBNull.Value)
                        GSTS12 = Convert.ToInt32(drow["GSTS12"]);
                    if (drow["GSTC12"] != DBNull.Value)
                        GSTC12 = Convert.ToInt32(drow["GSTC12"]);
                    if (drow["GSTS18"] != DBNull.Value)
                        GSTS18 = Convert.ToInt32(drow["GSTS18"]);
                    if (drow["GSTC18"] != DBNull.Value)
                        GSTC18 = Convert.ToInt32(drow["GSTC18"]);
                    if (drow["GSTS28"] != DBNull.Value)
                        GSTS28 = Convert.ToInt32(drow["GSTS28"]);
                    if (drow["GSTC28"] != DBNull.Value)
                        GSTC28 = Convert.ToInt32(drow["GSTC28"]);

                    if (drow["AmountGST0"] != DBNull.Value)
                        GSTAmt0 = Convert.ToInt32(drow["AmountGST0"]);
                    if (drow["AmountGSTS5"] != DBNull.Value)
                        GSTAmtS5 = Convert.ToInt32(drow["AmountGSTS5"]);
                    if (drow["AmountGSTC5"] != DBNull.Value)
                        GSTAmtC5 = Convert.ToInt32(drow["AmountGSTC5"]);
                    if (drow["AmountGSTS12"] != DBNull.Value)
                        GSTAmtS12 = Convert.ToInt32(drow["AmountGSTS12"]);
                    if (drow["AmountGSTC12"] != DBNull.Value)
                        GSTAmtC12 = Convert.ToInt32(drow["AmountGSTC12"]);
                    if (drow["AmountGSTS18"] != DBNull.Value)
                        GSTAmtS18 = Convert.ToInt32(drow["AmountGSTS18"]);
                    if (drow["AmountGSTC18"] != DBNull.Value)
                        GSTAmtC18 = Convert.ToInt32(drow["AmountGSTC18"]);
                    if (drow["AmountGSTS28"] != DBNull.Value)
                        GSTAmtS28 = Convert.ToInt32(drow["AmountGSTS28"]);
                    if (drow["AmountGSTC28"] != DBNull.Value)
                        GSTAmtC28 = Convert.ToInt32(drow["AmountGSTC28"]);


                    preAccountID = AccountID;
                    preNarration = Narration;
                    preEntryDate = EntryDate;
                    preVoucherSeries = VoucherSeries;
                    preVoucherType = VoucherType;
                    preVoucherNumber = VoucherNumber;
                    preVoucherDate = VoucherDate;
                    prePurchaseBillNumber = PurchaseBillNumber;
                    preAmountNetS = AmountNetS;
                    preAmountClearS = AmountClearS;
                    preAmountBillS = AmountBillS;
                    preAmountItemDiscountS = AmountItemDiscountS;
                    //preAmountSpecialDiscountS = AmountSpecialDiscountS;
                    //preSpecialDiscountPercentS = SpecialDiscountPercentS;
                    preAmountCashDiscountS = AmountCashDiscountS;
                    preCreditNoteDiscountPercentS = 0;
                    preAmountSchemeDiscountS = AmountSchemeDiscountS;
                    preAmountAddOnS = AmountAddOnS;
                    preAmountFreightS = AmountFreightS;
                    preCashDiscountPercentageS = CashDiscountPercentageS;
                    preAmountCreditNoteS = AmountCreditNoteS;
                    preAmountDebitNoteS = AmountDebitNoteS;
                    preRoundUpAmountS = RoundUpAmountS;
                    preOctroiPercentageS = OctroiPercentageS;
                    preAmountOctroiS = AmountOctroiS;
                    prePurchaseAmount5PercentVATS = PurchaseAmount5PercentVATS;
                    preAmountVAT0PercentS = 0;
                    preAmountVAT5PercentS = AmountVAT5PercentS;
                    prePurchaseAmount12point5PercentVATS = PurchaseAmount12point5PercentVATS;
                    preAmountVAT12point5PercentS = AmountVAT12point5PercentS;
                    prePurchaseAmountZeroVATS = PurchaseAmountZeroVATS;
                    preDueDate = DueDate;
                    //preNumberofChallans = NumberofChallans;                   
                    //preStatementNumber = StatementNumber;


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            //return retValue;
        }
    }
}
