using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EcoMart.DataLayer;
using EcoMart.Common;
using EcoMart.InterfaceLayer;
using EcoMart.InterfaceLayer.Classes;
using EcoMart.InterfaceLayer.CommonControls;


namespace EcoMart.BusinessLayer
{
    public class SSSale : BaseObject
    {

        #region Declaration
        private string _ProductID;
        private string _NewBatchno;
        private double _NewMRP;
        private double _NewSaleRate;
        private string _Batchno;
        private double _Mrp;
        private double _PurchaseRate;
        private int _ProdPakn;
        private double _SaleRate;
        private double _TradeRate;
        private int _Quantity;
        private double _Amount;
        private int _SchemeQuantity;
        private string _Expiry;
        private string _ExpiryDate;
        private string _ReasonCode;
        private double _VATPer;
        private double _VATAmount;
        private double _PMTDiscountPer;
        private double _PMTDiscountAmount;
        private double _PMTTotalDiscount;
        private double _ItemDiscountPer;
        private double _ItemDiscountAmount;
        private double _ItemTotalDiscount;
        private double _SchemeTotalDiscount;
        private double _SchemeDiscountAmount;
        private double _DiscAmountCB;
        private int _ClosingStock;
        private string _TransactionType;
        private string _PatientAddress1;
        private string _PatientAddress2;
        private string _PatientVATTIN;
        private string _SaleSubType;
        private int _TokenNumber;
        private string _Todays;
        private string _ShortListID;
        private int _SalesmanID;
        private int _TransporterID;
        private string _DocID;
        private string _DoctorName;
        private string _DoctorAddress;
        private string _ShortName;
        private string _PatientShortAddress;
        private string _OperatorID;
        private string _OperatorPassword;
        private string _AccCode;
        private string _OrderNumber;
        private string _OrderDate;
        private int _CurrentProductStock;
        private int _CurrentBatchStock;
        private string _IfTypeChange;
        private string _OldVoucherType;
        private int _OldVoucherNumber;
        private string _IfNewPatient;
        private string _IfFullPayment;
        private int _CreditCardBankID;
        private string _NewPatientIDInDebtorSale;
        private string _IfMultipleMRP;
        private string _DebtorsPatientID;

        private string _MobileNumberForSMS;

        private double _MySpecialDiscountPer;
        private double _MySpecialDiscountAmount;
        private double _MyTotalSpecialDiscountPer5;
        private double _MyTotalSpecialDiscountPer12point5;
        private double _TotalDiscount5;
        private double _TotalDiscount12point5;
        private double _PreMySpecialDiscountPer;
        private double _PreMySpecialDiscountAmount;
      //  private double _PreMyTotalSpecialDiscountPer5;
      //  private double _PreMyTotalSpecialDiscountPer12point5;
        private double _PreTotalDiscount5;
        private double _PreTotalDiscount12point5;

        private double _ProfitPercentBySaleRate;
        private double _ProfitPercentByPurchaseRate;
        private double _TotalProfitPercentBySaleRate;
        private double _TotalProfitPercentByPurchaseRate;
        private double _ProfitInRupees;
        private double _TotalProfitInRupees;
        private double _PreTotalProfitPercentBySaleRate;
        private double _PreTotalProfitPercentByPurchaseRate;
        private double _PreTotalProfitInRupees;
        private double _PrePMTTotalDiscount;
        private double _PreItemTotalDiscount;

        private double _PendingAmount;
        private double _TotalDebit;
        private double _TotalCredit;
        private double _OpeningBalance;
        private double _TotalPreviousSale;

        private double _DistributorSaleRate;
        private double _DistributorSaleRatePercent;
        private int _NoofRows;
        private string _PartyLBT;
        private string _PartyDLN;

        private string _PatientId;
        private string _AccountId;
        private string _CrdbName;
        //private string _CrdbDocID;
        private string _CrdbNarration1;
        private string _CrdbNarration2;
        private string _CrdbVouType;
        private int _CrdbVouNo;
        private string _CrdbVouDate;
        private string _CrdbVouSeries;
        private int _CrdbNoOfRows;
        private double _CrdbVat5;
        private double _CrdbVat12point5;
        private double _CrdbAmountVat5;
        private double _CrdbAmountVat12point5;
        private double _CrdbAmtForZeroVAT;
        private double _CrdbAmount;
        private double _CrdbDiscPer;
        private double _CrdbDiscAmt;
        private double _CrdbAmountNet;
        private double _CrdbRoundAmount;
        private double _CrdbTotalAmount;
        private string _Particulars;
        private double _CrdbBillAmount;
        private int _ClearVouNo;
        private string _CrdbDoctorName;
        private string _CrdbAreaId;
        private double _CrdbAddOn;
        private string _CrdbCompanyID;
        private double _CrdbVatAmount;
        private string _CrdbIfProdDisc;
        private int _CrdbCountersaleNumber;
        private double _CrdbAmountClear;
        private double _CrdbAmountBalance;
        private double _CrdbOctroiPer;
        private double _CrdbOctroiAmount;
        private string _CrdbIPDOPD;
        private int _MaxLevel;
        private int _StatementNumber;
        private double _CreditNoteAmount;
        private double _DebitNoteAmount;
        private string _LastStockID;
        private string _StockID;
        private string _ScanCode;
        private string _ShortNameForNarration;
        private string _CreditDebitNoteID;
        private int _CustNumber;
      //  private string _Telephone;
        private string _TempChallanID;

        private double _CrdbTotalAdd;
        private double _CrdbTotalLess;

        private string _PutInBlackList;
        private string _NextVisitDate;

        private string _PreAccountID;
        private string _PreNarration1;
        private string _PreNarration2;
        private string _PreVouType;
        private string _PreVouDate;
        private int    _PreVouNumber;
        private double _PreAmountNet;
        private double _PreDiscPer;
        private double _PreDiscAmt;
        private double _PreAmount;
        private double _PreVat5;
        private double _PreVat12point5;
        private double _PreRoundAmount;
        private string _PreDocID;
        private string _PreDoctorNameAddress;
        private string _PreDoctorAddress;
        private double _PreAddOn;
        private string _PreSaleSubType;
        private double _PreAmountBalance;
        private double _PreAmountClear;
        private double _PreOctoriPer;
        private double _PreOctroiAmount;
        private int _PreCountersaleNumber;
        private int _PreStatementNumber;
        private double _PreCrNoteAmount;
        private double _PreDbNoteAmount;
        private string _PreCrdbName;
        private string _PrePatientAddress1;
        private string _PrePatientAddress2;
        private string _PreShortName;
        private string _PrePatientShortAddress;
        private double _PreAmtForZeroVAT;
        private string _PreOperatorID;
        private double _PreAmountVat12point5;
        private double _PreAmountVat5;
        private string _PreIPDOPD;
        private string _PreOrderNumber;
        private string _PreOrderDate;
        private string _PreTelephone;
        private string _PrePrescriptionFileName;

        private string _MytblStockID;
        private string _setSaleAskDiscountinCounterSale;
        private string _setSaleAskOperatorinCounterSale;
        private string _SetPrintSaleBillPrintedPaper;

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

        private string _IFOMS;

        //  private byte[] _Prescription;
        private string _PrescriptionFileName;
        //   private string _FileExtension;
        #endregion

        #region Constructors, Destructors
        public SSSale()
        {
            Initialise();
        }
        #endregion

        #region properties

        public string IFOMS
        {
            get { return _IFOMS; }
            set { _IFOMS = value; }
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
            set { _GSTAmtS18 = value; }
        }
        public double GSTC18
        {
            get { return _GSTC18; }
            set { _GSTAmtC18 = value; }
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
        public string DebtorsPatientID
        {
            get { return _DebtorsPatientID; }
            set { _DebtorsPatientID = value; }
        }
        public string NextVisitDate
        {
            get { return _NextVisitDate; }
            set { _NextVisitDate = value; }
        }
        public string PutInBlackList
        {
            get { return _PutInBlackList; }
            set { _PutInBlackList = value; }
        }
        public string PMobileNumberForSMS
        {
            get { return _MobileNumberForSMS; }
            set { _MobileNumberForSMS = value; }
        }
        public string IFMultipleMRP
        {
            get { return _IfMultipleMRP; }
            set { _IfMultipleMRP = value; }
        }
        public string NewPatientIDInDebtorSale
        {
            get { return _NewPatientIDInDebtorSale; }
            set { _NewPatientIDInDebtorSale = value; }
        }
        public int CreditCardBankID
        {
            get { return _CreditCardBankID; }
            set { _CreditCardBankID = value; }
        }
        public string PartyLBT
        {
            get { return _PartyLBT; }
            set { _PartyLBT = value; }
        }
        public string PartyDLN
        {
            get { return _PartyDLN; }
            set { _PartyDLN = value; }
        }
        public int PreVouNumber
        {
            get { return _PreVouNumber; }
            set { _PreVouNumber = value; }
        }
        public Int32 NoofRows
        {
            get { return _NoofRows; }
            set { _NoofRows = value; }
        }
        public double DistributorSaleRate
        {
            get { return _DistributorSaleRate; }
            set { _DistributorSaleRate = value; }
        }
        public double DistributorSaleRatePercent
        {
            get { return _DistributorSaleRatePercent; }
            set { _DistributorSaleRatePercent = value; }
        }

        public string TempChallanID
        {
            get { return _TempChallanID; }
            set { _TempChallanID = value; }
        }
        public string IfNewPatient
        {
            get { return _IfNewPatient; }
            set { _IfNewPatient = value; }
        }

        public string IfFullPayment
        {
            get { return _IfFullPayment; }
            set { _IfFullPayment = value; }
        }

        public int CustNumber
        {
            get { return _CustNumber; }
            set { _CustNumber = value; }
        }
        public string CreditDebitNoteID
        {
            get { return _CreditDebitNoteID; }
            set { _CreditDebitNoteID = value; }
        }
        public string ShortNameForNarration
        {
            get { return _ShortNameForNarration; }
            set { _ShortNameForNarration = value; }
        }
        public string CrdbIPDOPD
        {
            get { return _CrdbIPDOPD; }
            set { _CrdbIPDOPD = value; }
        }
        public string MytblStockID
        {
            get { return _MytblStockID; }
            set { _MytblStockID = value; }
        }
        public string SetPrintSaleBillPrintedPaper
        {
            get { return _SetPrintSaleBillPrintedPaper; }
            set { _SetPrintSaleBillPrintedPaper = value; }
        }
        public string setSaleAskDiscountinCounterSale
        {
            get { return _setSaleAskDiscountinCounterSale; }
            set { _setSaleAskDiscountinCounterSale = value; }
        }
        public string setSaleAskOperatorinCounterSale
        {
            get { return _setSaleAskOperatorinCounterSale; }
            set { _setSaleAskOperatorinCounterSale = value; }
        }
        public string AccCode
        {
            get { return _AccCode; }
            set { _AccCode = value; }
        }
        public string OrderNumber
        {
            get { return _OrderNumber; }
            set { _OrderNumber = value; }
        }
        public string OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }
        public double DiscountAmountCB
        {
            get { return _DiscAmountCB; }
            set { _DiscAmountCB = value; }
        }
        public string OperatorID
        {
            get { return _OperatorID; }
            set { _OperatorID = value; }
        }
        public string OperatorPassword
        {
            get { return _OperatorPassword; }
            set { _OperatorPassword = value; }
        }
        public string StockID
        {
            get { return _StockID; }
            set { _StockID = value; }
        }
        public string ScanCode
        {
            get { return _ScanCode; }
            set { _ScanCode = value; }
        }
        public string LastStockID
        {
            get { return _LastStockID; }
            set { _LastStockID = value; }
        }
        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public string NewBatchno
        {
            get { return _NewBatchno; }
            set { _NewBatchno = value; }
        }
        public double NewMRP
        {
            get { return _NewMRP; }
            set { _NewMRP = value; }
        }
        public double NewSaleRate
        {
            get { return _NewSaleRate; }
            set { _NewSaleRate = value; }
        }
        public string Batchno
        {
            get { return _Batchno; }
            set { _Batchno = value; }
        }
        public Int32 ProdPakn
        {
            get { return _ProdPakn; }
            set { _ProdPakn = value; }
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
        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
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
        public string ReasonCode
        {
            get { return _ReasonCode; }
            set { _ReasonCode = value; }
        }
        public double VATPer
        {
            get { return _VATPer; }
            set { _VATPer = value; }
        }
        public double VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }

        public double PMTDiscountPer
        {
            get { return _PMTDiscountPer; }
            set { _PMTDiscountPer = value; }
        }
        public double PMTDiscountAmount
        {
            get { return _PMTDiscountAmount; }
            set { _PMTDiscountAmount = value; }
        }
        public double PMTTotalDiscount
        {
            get { return _PMTTotalDiscount; }
            set { _PMTTotalDiscount = value; }
        }

        public double ItemDiscountPer
        {
            get { return _ItemDiscountPer; }
            set { _ItemDiscountPer = value; }
        }
        public double ItemDiscountAmount
        {
            get { return _ItemDiscountAmount; }
            set { _ItemDiscountAmount = value; }
        }
        public double ItemTotalDiscount
        {
            get { return _ItemTotalDiscount; }
            set { _ItemTotalDiscount = value; }
        }

        public double SchemeDiscountAmount
        {
            get { return _SchemeDiscountAmount; }
            set { _SchemeDiscountAmount = value; }
        }
        public double SchemeTotalDiscount
        {
            get { return _SchemeTotalDiscount; }
            set { _SchemeTotalDiscount = value; }
        }
        public double PreItemTotalDiscount
        {
            get { return _PreItemTotalDiscount; }
            set { _PreItemTotalDiscount = value; }
        }

        public int Closingstock
        {
            get { return _ClosingStock; }
            set { _ClosingStock = value; }
        }
        public int ClearVouNo
        {
            get { return _ClearVouNo; }
            set { _ClearVouNo = value; }
        }
        public string AccountID
        {
            get { return _AccountId; }
            set { _AccountId = value; }
        }
        public string PatientID
        {
            get { return _PatientId; }
            set { _PatientId = value; }
        }
        public int SalesmanID
        {
            get { return _SalesmanID; }
            set { _SalesmanID = value; }
        }
        public int TransporterID
        {
            get { return _TransporterID; }
            set { _TransporterID = value; }
        }
        public string DocID
        {
            get { return _DocID; }
            set { _DocID = value; }
        }
        public string DoctorName
        {
            get { return _DoctorName; }
            set { _DoctorName = value; }
        }
        public string DoctorAddress
        {
            get { return _DoctorAddress; }
            set { _DoctorAddress = value; }
        }
        //public string CrdbDocID
        //{
        //    get { return _CrdbDocID; }
        //    set { _CrdbDocID = value; }
        //}
        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        public string PatientShortAddress
        {
            get { return _PatientShortAddress; }
            set { _PatientShortAddress = value; }
        }

        internal bool AddAccountDetails()
        {   
            bool retValue = false;
            try
            {
                double mdebit = 0;
            //    double mdiscper = 0;
            //    double maddon = 0;
            //    double mdiscamount = 0;
                //  double mvat5per = 0;
                //  double mvat12point5per = 0;
             //   double mamtforzerovat = 0;
          //     double mbillamount = 0;
            //    double mround = 0;
             //   double mcreditnoteamt = 0;
             //   double mdebitnoteamt = 0;
                //if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                //    ShortNameForNarration = ShortName;
                //else
                //    ShortNameForNarration = "";
                //double.TryParse(txtCreditNote.Text.ToString(), out mcreditnoteamt);
                //CrNoteAmount = mcreditnoteamt;
                //double.TryParse(txtDebitNote.Text.ToString(), out mdebitnoteamt);
                //DbNoteAmount = mdebitnoteamt;
                //double.TryParse(txtVatInput5per.Text, out mvat5per);
                // CrdbVat5 = Math.Round(mvat5per, 2);
                //double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                //CrdbVat12point5 = Math.Round(mvat12point5per, 2);
                //double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamtforzerovat);
                //CrdbAmtForZeroVAT = mamtforzerovat;

                //double.TryParse(txtAddOn.Text, out maddon);
                //CrdbAddOn = maddon;


                mdebit = Math.Round(CrdbAmountNet - GSTS5 - GSTS12 - GSTS18 - GSTS28
                                                - GSTC5 - GSTC12 - GSTC18 - GSTC28
                                                - GSTI5 - GSTI12 - GSTI18 - GSTI28
                                           - CrdbAddOn - CrdbRoundAmount - GSTAmt0 + CrNoteAmount - DbNoteAmount, 2);
               
                if (GSTAmt0 > 0)
                {
                    DebitAccount = FixAccounts.AccountAmountGST0Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        CreditAccount = FixAccounts.AccountCash;
                    else
                        CreditAccount = Convert.ToInt32(AccountID);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = GSTAmt0;
                    retValue = AddVoucherIntblTrnac();

                }

                if (GSTS5 > 0)
                {
                    DebitAccount = FixAccounts.AccountSGST5Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;                       
                        else
                            CreditAccount = Convert.ToInt32(AccountID);
                    }
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = Math.Round(GSTS5, 2);
                    retValue = AddVoucherIntblTrnac();

                }

                if (GSTS12 > 0)
                {
                    DebitAccount = FixAccounts.AccountSGST12Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;                       
                        else
                            CreditAccount = Convert.ToInt32(AccountID);
                    }
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = Math.Round(GSTS12, 2);
                    retValue = AddVoucherIntblTrnac();

                }
                if (GSTS18 > 0)
                {
                    DebitAccount = FixAccounts.AccountSGST18Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;                       
                        else
                            CreditAccount = Convert.ToInt32(AccountID);
                    }
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = Math.Round(GSTS18, 2);
                    retValue = AddVoucherIntblTrnac();

                }

                if (GSTS28 > 0)
                {
                    DebitAccount = FixAccounts.AccountSGST28Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;                        
                        else
                            CreditAccount = Convert.ToInt32(AccountID);
                    }
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = Math.Round(GSTS28, 2);
                    retValue = AddVoucherIntblTrnac();

                }


                if (GSTC5 > 0)
                {
                    DebitAccount = FixAccounts.AccountCGST5Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;                        
                        else
                            CreditAccount = Convert.ToInt32(AccountID);
                    }
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = Math.Round(GSTC5, 2);
                    retValue = AddVoucherIntblTrnac();

                }

                if (GSTC12 > 0)
                {
                    DebitAccount = FixAccounts.AccountCGST12Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;                       
                        else
                            CreditAccount = Convert.ToInt32(AccountID);
                    }
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = Math.Round(GSTS12, 2);
                    retValue = AddVoucherIntblTrnac();

                }
                if (GSTC18 > 0)
                {
                    DebitAccount = FixAccounts.AccountCGST18Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;                       
                        else
                            CreditAccount = Convert.ToInt32(AccountID);
                    }
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = Math.Round(GSTC18, 2);
                    retValue = AddVoucherIntblTrnac();

                }

                if (GSTC28 > 0)
                {
                    DebitAccount = FixAccounts.AccountCGST28Sale;
                    
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;
                    
                    else
                            CreditAccount = Convert.ToInt32(AccountID);
                    
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = Math.Round(GSTC28, 2);
                    retValue = AddVoucherIntblTrnac();

                }




                if (CrdbAddOn > 0)
                {
                    DebitAccount = FixAccounts.AccountAddonSale;

                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;
                   
                    else

                            CreditAccount = Convert.ToInt32(AccountID);
                   
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = CrdbAddOn;
                    retValue = AddVoucherIntblTrnac();
                }
                if (CrdbRoundAmount > 0)
                {
                    DebitAccount = FixAccounts.AccountRoundoffSale;
                   
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;
                    
                    else

                            CreditAccount = Convert.ToInt32(AccountID);
                    
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = (CrdbRoundAmount);
                    retValue = AddVoucherIntblTrnac();
                }
                if (CrdbRoundAmount < 0)
                {
                    DebitAccount = FixAccounts.AccountRoundoffSale;

                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                            CreditAccount = FixAccounts.AccountCash;
                    
                    else

                            CreditAccount = Convert.ToInt32(AccountID);
                   
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = CrdbRoundAmount * (-1);
                    CreditAmount = 0;
                    retValue = AddVoucherIntblTrnac();
                }

                //if (CrdbDiscAmt > 0)
                //{
                //    DebitAccount = FixAccounts.AccountCashDiscountSale.ToString();
                   
                //        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                //            CreditAccount = FixAccounts.AccountCash.ToString();
                //    else if (SaleSubType == FixAccounts.SubTypeForCreditCardSale)
                //            CreditAccount = FixAccounts.AccountCreditSale.ToString();
                //    else

                //            CreditAccount = Convert.ToInt32(AccountID);
                   
                //    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                //    DebitAmount = CrdbDiscAmt;
                //    CreditAmount = 0;
                //    retValue = AddVoucherIntblTrnac();
                //}

                if (CrNoteAmount > 0)
                {
                    DebitAccount = FixAccounts.AccountSalesReturn;
                    if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                            CreditAccount = FixAccounts.AccountCash;
                        else
                            CreditAccount = Convert.ToInt32(AccountID);
                    }
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = CrNoteAmount;
                    CreditAmount = 0;
                    retValue = AddVoucherIntblTrnac();
                }
                if (DbNoteAmount > 0)
                {
                    DebitAccount = FixAccounts.AccountDebitNoteSale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        CreditAccount = FixAccounts.AccountVoucherSale;
                    }
                    else
                    {
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                            CreditAccount = FixAccounts.AccountCash;
                        else
                            CreditAccount = Convert.ToInt32(AccountID);
                    }
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = DbNoteAmount;
                    retValue = AddVoucherIntblTrnac();
                }
                if (GSTAmtS5 > 0)
                {

                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        DebitAccount = FixAccounts.AccountAmountSGST5Sale;
                    else if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        DebitAccount = FixAccounts.AccountAmountSGST5Sale;
                    else
                        DebitAccount = FixAccounts.AccountAmountSGST5Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        CreditAccount = FixAccounts.AccountCash;
                   
                    else
                        CreditAccount = Convert.ToInt32(AccountID);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = GSTAmtS5;
                    retValue = AddVoucherIntblTrnac();
                }

                if (GSTAmtS12 > 0)
                {

                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        DebitAccount = FixAccounts.AccountAmountSGST12Sale;
                    else if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        DebitAccount = FixAccounts.AccountAmountSGST12Sale;
                    else
                        DebitAccount = FixAccounts.AccountAmountSGST12Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        CreditAccount = FixAccounts.AccountCash;
                    
                    else
                        CreditAccount = Convert.ToInt32(AccountID);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = GSTAmtS12;
                    retValue = AddVoucherIntblTrnac();
                }
                if (GSTAmtS18 > 0)
                {

                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        DebitAccount = FixAccounts.AccountAmountSGST18Sale;
                    else if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        DebitAccount = FixAccounts.AccountAmountSGST18Sale;
                    else
                        DebitAccount = FixAccounts.AccountAmountSGST18Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        CreditAccount = FixAccounts.AccountCash;
                   
                    else
                        CreditAccount = Convert.ToInt32(AccountID);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = GSTAmtS18;
                    retValue = AddVoucherIntblTrnac();
                }
                if (GSTAmtS28 > 0)
                {

                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        DebitAccount = FixAccounts.AccountAmountSGST28Sale;
                    else if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        DebitAccount = FixAccounts.AccountAmountSGST28Sale;
                    else
                        DebitAccount = FixAccounts.AccountAmountSGST28Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        CreditAccount = FixAccounts.AccountCash;                   
                    else
                        CreditAccount = Convert.ToInt32(AccountID);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = GSTAmtS28;
                    retValue = AddVoucherIntblTrnac();
                }

                if (GSTAmtC5 > 0)
                {

                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        DebitAccount = FixAccounts.AccountAmountCGST5Sale;
                    else if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        DebitAccount = FixAccounts.AccountAmountCGST5Sale;
                    else
                        DebitAccount = FixAccounts.AccountAmountCGST5Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        CreditAccount = FixAccounts.AccountCash;
                    
                    else
                        CreditAccount = Convert.ToInt32(AccountID);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = GSTAmtC5;
                    retValue = AddVoucherIntblTrnac();
                }

                if (GSTAmtC12 > 0)
                {

                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        DebitAccount = FixAccounts.AccountAmountCGST12Sale;
                    else if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        DebitAccount = FixAccounts.AccountAmountCGST12Sale;
                    else
                        DebitAccount = FixAccounts.AccountAmountCGST12Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        CreditAccount = FixAccounts.AccountCash;                    
                    else
                        CreditAccount = Convert.ToInt32(AccountID);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = GSTAmtC12;
                    retValue = AddVoucherIntblTrnac();
                }
                if (GSTAmtC18 > 0)
                {

                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        DebitAccount = FixAccounts.AccountAmountCGST18Sale;
                    else if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        DebitAccount = FixAccounts.AccountAmountCGST18Sale;
                    else
                        DebitAccount = FixAccounts.AccountAmountCGST18Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        CreditAccount = FixAccounts.AccountCash;                   
                    else
                        CreditAccount = Convert.ToInt32(AccountID);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = GSTAmtC18;
                    retValue = AddVoucherIntblTrnac();
                }
                if (GSTAmtC28 > 0)
                {

                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        DebitAccount = FixAccounts.AccountAmountCGST28Sale;
                    else if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        DebitAccount = FixAccounts.AccountAmountCGST28Sale;
                    else
                        DebitAccount = FixAccounts.AccountAmountCGST28Sale;
                    if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                        CreditAccount = FixAccounts.AccountCash;                   
                    else
                        CreditAccount = Convert.ToInt32(AccountID);
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = 0;
                    CreditAmount = GSTAmtC28;
                    retValue = AddVoucherIntblTrnac();
                }
                if (CrdbAmountNet > 0)
                {
                    
                        if (CrdbVouType == FixAccounts.VoucherTypeForCashSale || CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                            DebitAccount = FixAccounts.AccountCash;
                        else
                            DebitAccount = Convert.ToInt32(AccountID);
                        if (CrdbVouType == FixAccounts.VoucherTypeForVoucherSale)
                            CreditAccount = FixAccounts.AccountVoucherSale;
                        else
                        {
                            if (CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                                CreditAccount = FixAccounts.AccountCashSale;
                            else
                                CreditAccount = FixAccounts.AccountCreditSale;
                        }
                   
                    DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    DebitAmount = CrdbAmountNet;
                    CreditAmount = 0;
                    retValue = AddVoucherIntblTrnac();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        
    }

        public string TodayS
        {
            get { return _Todays; }
            set { _Todays = value; }
        }
        public string ShortListID
        {
            get { return _ShortListID; }
            set { _ShortListID = value; }
        }
        public int MaxLevel
        {
            get { return _MaxLevel; }
            set { _MaxLevel = value; }
        }
        public string Particulars
        {
            get { return _Particulars; }
            set { _Particulars = value; }
        }
        public string SaleSubType
        {
            get { return _SaleSubType; }
            set { _SaleSubType = value; }
        }
        public double CrdbAmount
        {
            get { return _CrdbAmount; }
            set { _CrdbAmount = value; }
        }
        public string TransactionType
        {
            get { return _TransactionType; }
            set { _TransactionType = value; }
        }
        public string PatientAddress1
        {
            get { return _PatientAddress1; }
            set { _PatientAddress1 = value; }
        }
        public string PatientAddress2
        {
            get { return _PatientAddress2; }
            set { _PatientAddress2 = value; }
        }
        public string PatientVATTIN
        {
            get { return _PatientVATTIN; }
            set { _PatientVATTIN = value; }
        }
        public string CrdbVouDate
        {
            get { return _CrdbVouDate; }
            set { _CrdbVouDate = value; }
        }
        public string CrdbVouSeries
        {
            get { return _CrdbVouSeries; }
            set { _CrdbVouSeries = value; }
        }
        public string CrdbName
        {
            get { return _CrdbName; }
            set { _CrdbName = value; }
        }
        public string CrdbNarration1
        {
            get { return _CrdbNarration1; }
            set { _CrdbNarration1 = value; }
        }
        public string CrdbNarration2
        {
            get { return _CrdbNarration2; }
            set { _CrdbNarration2 = value; }
        }
        public string CrdbVouType
        {
            get { return _CrdbVouType; }
            set { _CrdbVouType = value; }
        }
        public int CrdbVouNo
        {
            get { return _CrdbVouNo; }
            set { _CrdbVouNo = value; }
        }
        public int CrdbNoOFRows
        {
            get { return _CrdbNoOfRows; }
            set { _CrdbNoOfRows = value; }
        }
        public double CrdbVat5
        {
            get { return _CrdbVat5; }
            set { _CrdbVat5 = value; }
        }
        public double CrdbVat12point5
        {
            get { return _CrdbVat12point5; }
            set { _CrdbVat12point5 = value; }
        }
        public double CrdbAmountVat5
        {
            get { return _CrdbAmountVat5; }
            set { _CrdbAmountVat5 = value; }
        }
        public double CrdbAmountVat12point5
        {
            get { return _CrdbAmountVat12point5; }
            set { _CrdbAmountVat12point5 = value; }
        }
        public double CrdbAmtForZeroVAT
        {
            get { return _CrdbAmtForZeroVAT; }
            set { _CrdbAmtForZeroVAT = value; }
        }
        public double CrdbBillAmount
        {
            get { return _CrdbBillAmount; }
            set { _CrdbBillAmount = value; }
        }
        public double CrdbDiscPer
        {
            get { return _CrdbDiscPer; }
            set { _CrdbDiscPer = value; }
        }
        public double CrdbDiscAmt
        {
            get { return _CrdbDiscAmt; }
            set { _CrdbDiscAmt = value; }
        }
        public double CrdbAmountNet
        {
            get { return _CrdbAmountNet; }
            set { _CrdbAmountNet = value; }
        }
        public double CrdbTotalAmount
        {
            get { return _CrdbTotalAmount; }
            set { _CrdbTotalAmount = value; }
        }
        public double CrdbRoundAmount
        {
            get { return _CrdbRoundAmount; }
            set { _CrdbRoundAmount = value; }
        }
        public string CrdbDoctorName
        {
            get { return _CrdbDoctorName; }
            set { _CrdbDoctorName = value; }
        }
        public string CrdbAreaId
        {
            get { return _CrdbAreaId; }
            set { _CrdbAreaId = value; }
        }
        public double CrdbAddOn
        {
            get { return _CrdbAddOn; }
            set { _CrdbAddOn = value; }
        }

        public string CrdbCompanyID
        {
            get { return _CrdbCompanyID; }
            set { _CrdbCompanyID = value; }
        }
        public double CrdbVatAmount
        {
            get { return _CrdbVatAmount; }
            set { _CrdbVatAmount = value; }
        }
        public string CrdbIfProdDisc
        {
            get { return _CrdbIfProdDisc; }
            set { _CrdbIfProdDisc = value; }
        }
        public int CrdbCountersaleNumber
        {
            get { return _CrdbCountersaleNumber; }
            set { _CrdbCountersaleNumber = value; }
        }
        public double CrdbAmountClear
        {
            get { return _CrdbAmountClear; }
            set { _CrdbAmountClear = value; }
        }
        public double CrdbAmountBalance
        {
            get { return _CrdbAmountBalance; }
            set { _CrdbAmountBalance = value; }
        }
        public double CrdbOctoriPer
        {
            get { return _CrdbOctroiPer; }
            set { _CrdbOctroiPer = value; }
        }
        public double CrdbOctroiAmount
        {
            get { return _CrdbOctroiAmount; }
            set { _CrdbOctroiAmount = value; }
        }
        public int TokenNumber
        {
            get { return _TokenNumber; }
            set { _TokenNumber = value; }
        }
        public int StatementNumber
        {
            get { return _StatementNumber; }
            set { _StatementNumber = value; }
        }
        public double CrdbTotalADD
        {
            get { return _CrdbTotalAdd; }
            set { _CrdbTotalAdd = value; }
        }
        public double CrdbTotalLESS
        {
            get { return _CrdbTotalLess; }
            set { _CrdbTotalLess = value; }
        }
        public double CrNoteAmount
        {
            get { return _CreditNoteAmount; }
            set { _CreditNoteAmount = value; }
        }
        public double DbNoteAmount
        {
            get { return _DebitNoteAmount; }
            set { _DebitNoteAmount = value; }
        }
        public int CurrentProductStock
        {
            get { return _CurrentProductStock; }
            set { _CurrentProductStock = value; }
        }
        public int CurrentBatchStock
        {
            get { return _CurrentBatchStock; }
            set { _CurrentBatchStock = value; }
        }

        public string IfTypeChange
        {
            get { return _IfTypeChange; }
            set { _IfTypeChange = value; }
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

        public double ProfitPercentByPurchaseRate
        {
            get { return _ProfitPercentByPurchaseRate; }
            set { _ProfitPercentByPurchaseRate = value; }
        }

        public double ProfitPercentBySaleRate
        {
            get { return _ProfitPercentBySaleRate; }
            set { _ProfitPercentBySaleRate = value; }
        }
        public double TotalProfitPercentByPurchaseRate
        {
            get { return _TotalProfitPercentByPurchaseRate; }
            set { _TotalProfitPercentByPurchaseRate = value; }
        }
        public double TotalProfitPercentBySaleRate
        {
            get { return _TotalProfitPercentBySaleRate; }
            set { _TotalProfitPercentBySaleRate = value; }
        }

        public double ProfitInRupees
        {
            get { return _ProfitInRupees; }
            set { _ProfitInRupees = value; }
        }

        public double TotalProfitInRupees
        {
            get { return _TotalProfitInRupees; }
            set { _TotalProfitInRupees = value; }
        }

        ////public string Telephone
        ////{
        ////    get { return _Telephone; }
        ////    set { _Telephone = value; }
        ////}

        public string PreTelephone
        {
            get { return _PreTelephone; }
            set { _PreTelephone = value; }
        }

        public double PreTotalProfitPercentByPurchaseRate
        {
            get { return _PreTotalProfitPercentByPurchaseRate; }
            set { _PreTotalProfitPercentByPurchaseRate = value; }
        }
        public double PreTotalProfitPercentBySaleRate
        {
            get { return _PreTotalProfitPercentBySaleRate; }
            set { _PreTotalProfitPercentBySaleRate = value; }
        }

        public double PreTotalProfitInRupees
        {
            get { return _PreTotalProfitInRupees; }
            set { _PreTotalProfitInRupees = value; }
        }

        //public double AmountByPurchaseRate
        //{
        //    get { return _AmountByPurchaseRate; }
        //    set { _AmountByPurchaseRate = value; }
        //}

        public double TotalDiscount5
        {
            get { return _TotalDiscount5; }
            set { _TotalDiscount5 = value; }
        }
        public double TotalDiscount12point5
        {
            get { return _TotalDiscount12point5; }
            set { _TotalDiscount12point5 = value; }
        }
        public double PreTotalDiscount5
        {
            get { return _PreTotalDiscount5; }
            set { _PreTotalDiscount5 = value; }
        }
        public double PreTotalDiscount12point5
        {
            get { return _PreTotalDiscount12point5; }
            set { _PreTotalDiscount12point5 = value; }
        }


        public double MySpecialDiscountPer
        {
            get { return _MySpecialDiscountPer; }
            set { _MySpecialDiscountPer = value; }
        }

        public double MySpecialDiscountAmount
        {
            get { return _MySpecialDiscountAmount; }
            set { _MySpecialDiscountAmount = value; }
        }
        public double MyTotalSpecialDiscountPer5
        {
            get { return _MyTotalSpecialDiscountPer5; }
            set { _MyTotalSpecialDiscountPer5 = value; }
        }
        public double MyTotalSpecialDiscountPer12point5
        {
            get { return _MyTotalSpecialDiscountPer12point5; }
            set { _MyTotalSpecialDiscountPer12point5 = value; }
        }

        public double PreMySpecialDiscountPer
        {
            get { return _PreMySpecialDiscountPer; }
            set { _PreMySpecialDiscountPer = value; }
        }

        public double PreMySpecialDiscountAmount
        {
            get { return _PreMySpecialDiscountAmount; }
            set { _PreMySpecialDiscountAmount = value; }
        }
        private string PreAccountID
        {
            get { return _PreAccountID; }
            set { _PreAccountID = value; }
        }
        private string PreNarration1
        {
            get { return _PreNarration1; }
            set { _PreNarration1 = value; }
        }
        private string PreNarration2
        {
            get { return _PreNarration2; }
            set { _PreNarration2 = value; }
        }
        private string PreVouType
        {
            get { return _PreVouType; }
            set { _PreVouType = value; }
        }
        private string PreVouDate
        {
            get { return _PreVouDate; }
            set { _PreVouDate = value; }
        }
        public double PreAmountNet
        {
            get { return _PreAmountNet; }
            set { _PreAmountNet = value; }
        }
        private double PreDiscPer
        {
            get { return _PreDiscPer; }
            set { _PreDiscPer = value; }
        }
        private double PreDiscAmt
        {
            get { return _PreDiscAmt; }
            set { _PreDiscAmt = value; }
        }
        private double PreAmount
        {
            get { return _PreAmount; }
            set { _PreAmount = value; }
        }
        private double PreVat5
        {
            get { return _PreVat5; }
            set { _PreVat5 = value; }
        }
        private double PreVat12point5
        {
            get { return _PreVat12point5; }
            set { _PreVat12point5 = value; }
        }
        private double PreRoundAmount
        {
            get { return _PreRoundAmount; }
            set { _PreRoundAmount = value; }
        }
        private string PreDocID
        {
            get { return _PreDocID; }
            set { _PreDocID = value; }
        }

        private string PreDoctorNameAddress
        {
            get { return _PreDoctorNameAddress; }
            set { _PreDoctorNameAddress = value; }
        }
        private string PreDoctorAddress
        {
            get { return _PreDoctorAddress; }
            set { _PreDoctorAddress = value; }
        }
        private double PreAddOn
        {
            get { return _PreAddOn; }
            set { _PreAddOn = value; }
        }
        private string PreSaleSubType
        {
            get { return _PreSaleSubType; }
            set { _PreSaleSubType = value; }
        }
        private double PreAmountBalance
        {
            get { return _PreAmountBalance; }
            set { _PreAmountBalance = value; }
        }
        private double PreAmountClear
        {
            get { return _PreAmountClear; }
            set { _PreAmountClear = value; }
        }
        private double PreOctoriPer
        {
            get { return _PreOctoriPer; }
            set { _PreOctoriPer = value; }
        }
        private double PreOctroiAmount
        {
            get { return _PreOctroiAmount; }
            set { _PreOctroiAmount = value; }
        }
        private int PreCountersaleNumber
        {
            get { return _PreCountersaleNumber; }
            set { _PreCountersaleNumber = value; }
        }
        private int PreStatementNumber
        {
            get { return _PreStatementNumber; }
            set { _PreStatementNumber = value; }
        }
        private double PreCrNoteAmount
        {
            get { return _PreCrNoteAmount; }
            set { _PreCrNoteAmount = value; }
        }
        private double PreDbNoteAmount
        {
            get { return _PreDbNoteAmount; }
            set { _PreDbNoteAmount = value; }
        }
        private string PreCrdbName
        {
            get { return _PreCrdbName; }
            set { _PreCrdbName = value; }
        }
        private string PrePatientAddress1
        {
            get { return _PrePatientAddress1; }
            set { _PrePatientAddress1 = value; }
        }
        private string PrePatientAddress2
        {
            get { return _PrePatientAddress2; }
            set { _PrePatientAddress2 = value; }
        }
        private string PreShortName
        {
            get { return _PreShortName; }
            set { _PreShortName = value; }
        }
        private string PrePatientShortAddress
        {
            get { return _PrePatientShortAddress; }
            set { _PrePatientShortAddress = value; }
        }
        private double PreAmtForZeroVAT
        {
            get { return _PreAmtForZeroVAT; }
            set { _PreAmtForZeroVAT = value; }
        }
        private string PreOperatorID
        {
            get { return _PreOperatorID; }
            set { _PreOperatorID = value; }
        }
        private double PreAmountVat12point5
        {
            get { return _PreAmountVat12point5; }
            set { _PreAmountVat12point5 = value; }
        }
        private double PreAmountVat5
        {
            get { return _PreAmountVat5; }
            set { _PreAmountVat5 = value; }
        }
        private string PreIPDOPD
        {
            get { return _PreIPDOPD; }
            set { _PreIPDOPD = value; }
        }
        private string PreOrderNumber
        {
            get { return _PreOrderNumber; }
            set { _PreOrderNumber = value; }
        }
        private string PreOrderDate
        {
            get { return _PreOrderDate; }
            set { _PreOrderDate = value; }
        }

        private double PrePMTTotalDiscount
        {
            get { return _PrePMTTotalDiscount; }
            set { _PrePMTTotalDiscount = value; }
        }

        public double PendingAmount
        {
            get { return _PendingAmount; }
            set { _PendingAmount = value; }
        }
        public double TotalDebit
        {
            get { return _TotalDebit; }
            set { _TotalDebit = value; }
        }
        public double TotalPreviousSale
        {
            get { return _TotalPreviousSale; }
            set { _TotalPreviousSale = value; }
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

        //public byte[] Prescription
        //{
        //    get { return _Prescription; }
        //    set { _Prescription = value; }
        //}

        //public string PrescriptionID
        //{
        //    get { return _PrescriptionID; }
        //    set { _PrescriptionID = value; }
        //}
        public string PrescriptionFileName
        {
            get { return _PrescriptionFileName; }
            set { _PrescriptionFileName = value; }
        }

        public string PrePrescriptionFileName
        {
            get { return _PrePrescriptionFileName; }
            set { _PrePrescriptionFileName = value; }
        }
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _NewBatchno = "";
                _NewMRP = 0;
                _NewSaleRate = 0;
                _Batchno = "";
                _Expiry = "";
                _ExpiryDate = "";
                _Mrp = 0;
                _ProdPakn = 0;
                _TransporterID = 0;
                _SalesmanID = 0;
                _DocID = "0";
                _DoctorName = "";
                _DoctorAddress = "";
                //_CrdbDocID = "0";
                _ShortName = "";
                _PatientShortAddress = "";
                _ProductID = "";
                _Amount = 0;
                _VATPer = 0;
                _VATAmount = 0;
                _PMTDiscountPer = 0;
                _PMTDiscountAmount = 0;
                _PMTTotalDiscount = 0;
                _PurchaseRate = 0;
                _Quantity = 0;
                _ReasonCode = "";
                _SaleRate = 0;
                _SchemeQuantity = 0;
                _TradeRate = 0;
                _PatientAddress1 = "";
                _PatientAddress2 = "";
                _PatientVATTIN = "";
                _OperatorID = "";
                _OperatorPassword = "";
                _SaleSubType = "";
                _TokenNumber = 0;
                _Todays = DateTime.Today.Date.ToString("yyyyMMdd");
                _ShortListID = null;
                _MaxLevel = 0;
                _AccCode = "";
                _OrderDate = "";
                _OrderNumber = "";
                _ShortNameForNarration = "";
                _IfTypeChange = "N";
                _OldVoucherNumber = 0;
                _OldVoucherType = "";
                _IfNewPatient = "N";
                _IfFullPayment = "Y";
                _PartyLBT = "";
                _PartyDLN = "";
                _CreditCardBankID = 0;
                _NewPatientIDInDebtorSale = "";
                _IfMultipleMRP = "N";

                _MobileNumberForSMS = "";
                _PutInBlackList = "N";
                _NextVisitDate = "";
                _DebtorsPatientID = "";

                _ProfitPercentBySaleRate = 0;
                _ProfitPercentByPurchaseRate = 0;
                _TotalProfitPercentBySaleRate = 0;
                _TotalProfitPercentByPurchaseRate = 0;
                _ProfitInRupees = 0;
                _TotalProfitInRupees = 0;
                _PreTotalProfitPercentBySaleRate = 0;
                _PreTotalProfitPercentByPurchaseRate = 0;
                _PreTotalProfitInRupees = 0;
                //_AmountByPurchaseRate = 0;

                _PendingAmount = 0;
                _OpeningBalance = 0;
                _TotalCredit = 0;
                _TotalDebit = 0;
                _TotalPreviousSale = 0;


                _CrdbName = "";
                _AccountId = "";
                _CrdbNarration1 = "";
                _CrdbNarration2 = "";
                _CrdbVouType = "";
                _PatientId = "";

                _CrdbVouNo = 0;
                _CrdbVat5 = 0;
                _CrdbVat12point5 = 0;
                _CrdbAmountVat5 = 0;
                _CrdbAmountVat12point5 = 0;
                _CrdbAmtForZeroVAT = 0;
                _CrdbNoOfRows = 0;
                _CrdbAmount = 0;
                _CrdbDiscPer = 0;
                _CrdbDiscAmt = 0;
                _CrdbAmountNet = 0;
                _CrdbRoundAmount = 0;
                _CrdbVouDate = "";
                _CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
                _Particulars = "";
                _CrdbAmount = 0;
                _ClearVouNo = 0;
                _CrdbDoctorName = "";
                _CrdbAreaId = "";
                _CrdbAddOn = 0;
                _CrdbCompanyID = "";
                _CrdbVatAmount = 0;
                _CrdbIfProdDisc = "";
                _CrdbCountersaleNumber = 0;
                _CrdbAmountClear = 0;
                _CrdbAmountBalance = 0;
                _CrdbOctroiPer = 0;
                _CrdbOctroiAmount = 0;
                _StatementNumber = 0;
                _CreditNoteAmount = 0;
                _DebitNoteAmount = 0;
                _CrdbIPDOPD = "I";
                _StockID = "";
                _ScanCode = "";
                _LastStockID = "";
                _TransactionType = "";
                _CreditDebitNoteID = "";
                _CustNumber = 0;
               // _Telephone = "";
                _TempChallanID = "";

                _CrdbTotalAdd = 0;
                _CrdbTotalLess = 0;

                _PrescriptionFileName = "";
                _PrePrescriptionFileName = "";

                _MySpecialDiscountAmount = 0;
                _MySpecialDiscountPer = 0;
                _MyTotalSpecialDiscountPer12point5 = 0;
                _MyTotalSpecialDiscountPer5 = 0;

                _PreMySpecialDiscountAmount = 0;
                _PreMySpecialDiscountPer = 0;

                _PreAccountID = "";
                _PreNarration1 = "";
                _PreNarration2 = "";
                _PreVouType = "";
                _PreVouDate = "";
                _PreVouNumber = 0;
                _PreAmountNet = 0;
                _PreDiscPer = 0;
                _PreDiscAmt = 0;
                _PreAmount = 0;
                _PreVat5 = 0;
                _PreVat12point5 = 0;
                _PreRoundAmount = 0;
                _PreDocID = "";
                _PreDoctorNameAddress = "";
                _PreDoctorAddress = "";
                _PreAddOn = 0;
                _PreSaleSubType = "";
                _PreAmountBalance = 0;
                _PreAmountClear = 0;
                _PreOctoriPer = 0;
                _PreOctroiAmount = 0;
                _PreCountersaleNumber = 0;
                _PreStatementNumber = 0;
                _PreCrNoteAmount = 0;
                _PreDbNoteAmount = 0;
                _PreCrdbName = "";
                _PrePatientAddress1 = "";
                _PrePatientAddress2 = "";
                _PreShortName = "";
                _PrePatientShortAddress = "";
                _PreAmtForZeroVAT = 0;
                _PreOperatorID = "";
                _PreAmountVat12point5 = 0;
                _PreAmountVat5 = 0;
                _PreIPDOPD = "";
                _PreOrderNumber = "";
                _PreOrderDate = "";
                _PrePMTTotalDiscount = 0;
                _PreTelephone = "";

                _SetPrintSaleBillPrintedPaper = "N";
                _setSaleAskDiscountinCounterSale = "N";
                _setSaleAskOperatorinCounterSale = "N";

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
                _IFOMS = "N";

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override void DoValidate()
        {
            try
            {
                bool retValue = false;
                
                string _MDate = CrdbVouDate;
                string _CDate = DateTime.Now.Date.ToString("yyyyMMdd");
                if (CrdbVouNo == 0)
                {
                    //if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                    //{                   
                    //   ValidationMessages.Add("Error while saving, Please save again...");                   
                    //}
                    //else if (CrdbVouType != FixAccounts.VoucherTypeForVoucherSale)
                    //    ValidationMessages.Add("Error while saving, Please save again...");
                }
                if (General.CurrentSetting.MsetSaleAllowBackDate != "Y")
                {
                    if (Convert.ToInt32(_MDate) < Convert.ToInt32(_CDate))
                    {
                        ValidationMessages.Add("Check Date...");
                    }
                    else
                    {
                        retValue = General.CheckDates(_MDate, _MDate);
                        if (retValue == false)
                        {
                            ValidationMessages.Add("Please Check Date...");
                        }
                    }                   
                  
                }
                else
                {
                    retValue = General.CheckDates(_MDate, _MDate);
                    if (retValue == false)
                    {

                        ValidationMessages.Add("Please Check Date...");
                    }
                }
               if (PatientAddress1.Length > 50)
                   ValidationMessages.Add("Address should be Max 50 Characters");

                if (CrdbVouType.ToString().Trim() == string.Empty)
                    ValidationMessages.Add("Select Voucher Type");
                if (CrdbVouType != FixAccounts.VoucherTypeForVoucherSale)
                {
                    if (CrdbAmountNet == 0)
                        ValidationMessages.Add("Invalid Amount");
                    if (CrdbVouType.Substring(0, 1) != "W")
                    {
                        if (CrdbName == "")
                            ValidationMessages.Add("Please enter Patient Name.");
                       
                        if ((CrdbVouType == FixAccounts.VoucherTypeForCreditStatementSale && AccountID == ""))
                            ValidationMessages.Add("Not a Valid Sale Type");
                    }
                    if (General.CurrentSetting.MsetAskOperatorOtherThanVoucherSale == "Y")
                    {
                        if (OperatorPassword == "")
                            ValidationMessages.Add("Type Operator Code");
                        else
                        {
                            DataRow dr;
                            DBOperator dbop = new DBOperator();
                            dr = dbop.GetOperatorIDByOperatorPassword(OperatorPassword);
                            if (dr != null)
                            {
                                OperatorID = dr["OperatorID"].ToString();
                            }
                            else
                                ValidationMessages.Add("Wrong Operator");
                        }
                    }
                    if (CrdbAmountBalance < 0)
                        ValidationMessages.Add("Please Check Balance Amount");                   

                }
                else
                {
                    if (General.CurrentSetting.MsetAskOperatorVoucherSale == "Y")
                    {
                        if (OperatorPassword == "")
                            ValidationMessages.Add("Type Operator Code");
                        else
                        {
                            DataRow dr;
                            DBOperator dbop = new DBOperator();
                            dr = dbop.GetOperatorIDByOperatorPassword(OperatorPassword);
                            if (dr != null)
                            {
                                OperatorID = dr["OperatorID"].ToString();
                            }
                            else
                                ValidationMessages.Add("Wrong Operator");
                        }
                    }
                    
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }
        #endregion

        #region Public Methods
        public DataTable GetOverviewDataForGSTReportHSN(string fromdate, string todate)
        {
            DBSaleList dbPur = new DBSaleList();
            return dbPur.GetOverviewDataForGSTReportHSN(fromdate, todate);
        }

        public DataTable GetOverviewDataForGSTReport(string fromdate, string todate)
        {

            DBSaleList dbPur = new DBSaleList();
            return dbPur.GetOverviewDataForGSTReport(fromdate, todate);
        }

        public DataTable GetOverviewData(string vouSubType)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetOverviewData(vouSubType);
        }

        public DataTable GetOverviewData(string vouSubType, string fromDate, string toDate)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetOverviewData(vouSubType, fromDate, toDate);
        }
        public DataTable GetOverviewDataSpecialSale(string vouSubType, string fromDate, string toDate)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetOverviewDataSpecialSale(vouSubType, fromDate, toDate);
        }
        public DataTable GetOverviewDataForLastSale(string accID, string ProductID)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetOverviewDataForLastSale(accID, ProductID);
        }

        public DataTable GetOverviewDataForPartywiseBillsForStatements(string PartyID, string fromDate, string toDate)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetOverviewDataForPartywiseBillsForStatements(PartyID, fromDate, toDate);
        }

        public DataTable GetOverviewDataForHospitalStatement(string InwardID)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetOverviewDataForHospitalStatement(InwardID);
        }

        public DataTable GetOverviewDataForPartywiseStatementsView(int statementNumber, string voucherSeries)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetOverviewDataForPartywiseStatementsView(statementNumber, voucherSeries);
        }
        public DataTable GetOverviewDataForPartywiseSaleReportforPatient(string accID)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetOverviewDataForPartywiseSaleReportforPatient(accID);
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
        public DataTable GetPreviousSale(string accountid)
        {
            DataTable dt = null;
            DBSSSale  dbp = new DBSSSale();
            try
            {
                dt = dbp.GetPreviousSale(accountid);
                TotalPreviousSale = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["AmountNet"] != DBNull.Value)
                        TotalPreviousSale += Convert.ToDouble(dr["AmountNet"].ToString());

                }
                
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }
        public DataTable GetOverviewDataForLastPurchase(string productID)
        {
            DBSSSale dbp = new DBSSSale();
            return dbp.GetOverviewDataForLastPurchase(productID);
        }
        public DataTable GetPreviousSaleBillWise(string accountid, Int32 month)
        {
            DataTable dt = null;
            DBSSSale dbp = new DBSSSale();
            try
            {
                dt = dbp.GetPreviousSaleBillWise(accountid, month); 
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
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

        public DataTable GetVoucherSaleDataData(string voutdate, string changeCounterSaleType)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetVoucherSaleDataData(voutdate,changeCounterSaleType);
        }
        public DataTable GetRegularSaleOverviewData()
        {
            DBCounterSale dbCountSale = new DBCounterSale();
            return dbCountSale.GetRegularSaleOverviewData();
        }
        //public DataTable GetHospitalSaleOverviewData()
        //{
        //    DBCounterSale dbHos = new DBCounterSale();
        //    return dbHos.GetHospitalSaleOverviewData();
        //}
        //public DataTable GetDebtorSaleOverviewData()
        //{
        //    DBCounterSale dbdebtor = new DBCounterSale();
        //    return dbdebtor.GetDebtorSaleOverviewData();
        //}
        //public DataTable GetCreditCardSaleOverviewData()
        //{
        //    DBCounterSale dbdebtor = new DBCounterSale();
        //    return dbdebtor.GetCreditCardSaleOverviewData();
        //}
        public DataTable GetDistributorSaleOverviewData()
        {
            DBCounterSale dbdebtor = new DBCounterSale();
            return dbdebtor.GetDistributorSaleOverviewData();
        }
        public DataTable GetDistributorSaleOverviewData(string subType)
        {
            DBCounterSale dbdebtor = new DBCounterSale();
            return dbdebtor.GetDistributorSaleOverviewData(subType);
        }
        //public DataTable GetInstitutionalSaleOverviewData()
        //{
        //    DBCounterSale dbdebtor = new DBCounterSale();
        //    return dbdebtor.GetInstitutionalSaleOverviewData();
        //}
        //public DataTable GetSaleWithProductDiscountOverviewData()
        //{
        //    DBCounterSale dbdebtor = new DBCounterSale();
        //    return dbdebtor.GetSaleWithProductDiscountOverviewData();
        //}
        public DataTable ReadDetailsByID()
        {
            DataTable dt = null;
            try
            {
                DataRow drow = null;
                DBSSSale dbsale = new DBSSSale();
                drow = dbsale.ReadDetailsByID(Id, SaleSubType);

                if (drow != null)
                {
                    Id = drow["ID"].ToString();
                    CrdbVouType = drow["VoucherType"].ToString();
                    OldVoucherType = CrdbVouType;
                    IntID = Convert.ToInt32(drow["ID"].ToString());
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                //    CrdbCountersaleNumber = Convert.ToInt32(drow["CounterSaleNumber"].ToString());
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    SaleSubType = drow["VoucherSubType"].ToString();
                    if (drow["AccountID"] != DBNull.Value && drow["AccountID"].ToString() != string.Empty)
                        AccountID = drow["AccountId"].ToString();                   
            //        if (drow["MySpecialDiscountAmount"] != DBNull.Value)
           //         MySpecialDiscountAmount = Convert.ToDouble(drow["MySpecialDiscountAmount"].ToString());
                    CrdbBillAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CrdbBillAmount = CrdbBillAmount + MySpecialDiscountAmount;
                    CrdbAmountNet = CrdbBillAmount;
                    CrdbAmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    CrdbAmountClear = CrdbAmountClear + MySpecialDiscountAmount;
                    CrdbAmountBalance = Convert.ToDouble(drow["AmountBalance"].ToString());
                    CrdbAmount = Convert.ToDouble(drow["AmountGross"].ToString());
                    CrdbAmount = CrdbAmount + MySpecialDiscountAmount;
                    CrdbDiscPer = Convert.ToDouble(drow["CashDiscountPercent"].ToString());
                    CrdbDiscAmt = Convert.ToDouble(drow["AmountCashDiscount"].ToString()); 
                  //  ItemTotalDiscount = Convert.ToDouble(drow["Amount
                    if (drow["AddOnFreight"] != DBNull.Value)
                        CrdbAddOn = Convert.ToDouble(drow["AddOnFreight"].ToString());
                    if (drow["AmountCreditNoteStock"] != DBNull.Value)
                        CrNoteAmount = Convert.ToDouble(drow["AmountCreditNoteStock"].ToString());
                    if (drow["AmountDebitNote"] != DBNull.Value)
                        DbNoteAmount = Convert.ToDouble(drow["AmountDebitNote"].ToString());
                    CrdbNarration1 = Convert.ToString(drow["Narration1"]);
                    CrdbNarration2 = drow["Narration2"].ToString();
                    if (drow["StatementNumber"] != DBNull.Value)
                        StatementNumber = Convert.ToInt32(drow["StatementNumber"].ToString());
                    if (drow["DoctorID"] != DBNull.Value)
                        DocID = drow["DoctorID"].ToString();
                //    if (drow["DoctorShortName"] != DBNull.Value)
                //        DoctorName = drow["DoctorShortName"].ToString();
              //      if (drow["DoctorAddress"] != DBNull.Value)
              //          DoctorAddress = drow["DoctorAddress"].ToString();
                    if (drow["PatientID"] != DBNull.Value)
                        PatientID = drow["PatientID"].ToString();
                    if (drow["OperatorID"] != DBNull.Value)
                        OperatorID = drow["OperatorID"].ToString();
                    if (drow["SalesmanID"] != DBNull.Value)
                        SalesmanID = Convert.ToInt32(drow["SalesmanID"].ToString());
                    if (drow["TransporterID"] != DBNull.Value)
                       TransporterID = Convert.ToInt32(drow["TransporterID"].ToString());
                    if (drow["PatientName"] != DBNull.Value)
                        CrdbName = drow["PatientName"].ToString();
                    if (drow["PatientAddress1"] != DBNull.Value)
                        PatientAddress1 = drow["PatientAddress1"].ToString();
                    if (drow["PatientAddress2"] != DBNull.Value)
                        PatientAddress2 = drow["PatientAddress2"].ToString();
                 //   if (drow["MobileNumberForSMS"] != DBNull.Value)
                 //       MobileNumberForSMS = drow["MobileNumberForSMS"].ToString();
               //     if (drow["PatientShortName"] != DBNull.Value)
                //        ShortName = drow["PatientShortName"].ToString();
               //     if (drow["PatientShortAddress"] != DBNull.Value)
               //         PatientShortAddress = drow["PatientShortAddress"].ToString();
               //     if (drow["IPDOPDCode"] != DBNull.Value)
              //          CrdbIPDOPD = drow["IPDOPDCode"].ToString();
                    if (drow["OrderNumber"] != DBNull.Value)
                        OrderNumber = drow["OrderNumber"].ToString();
                    if (drow["OrderDate"] != DBNull.Value)
                        OrderDate = drow["OrderDate"].ToString();
               //     if (drow["Telephone"] != DBNull.Value)
                //        Telephone = drow["Telephone"].ToString();
                //    if (drow["ScanPrescriptionFileName"] != DBNull.Value)
               //         PrescriptionFileName = drow["ScanPrescriptionFileName"].ToString();
                    //   if (drow["ScanPrescriptionID"] != DBNull.Value)
                    //     PrescriptionID = drow["ScanPrescriptionID"].ToString();
                    //     if (PrescriptionID != string.Empty)
                    //   {
                    //     FileExtension = drow["FileExtension"].ToString();
                    //      Prescription = (byte[])(drow["PrescriptionData"]);

                 //   //   }
                 //   CrdbVat5 = Convert.ToDouble(drow["VAT5per"].ToString());
                //    CrdbVat12point5 = Convert.ToDouble(drow["VAT12Point5Per"].ToString());
               //     CrdbAmtForZeroVAT = Convert.ToDouble(drow["AmountForZeroVAT"].ToString());
               //     CrdbAmountVat5 = Convert.ToDouble(drow["AmountVAT5Per"].ToString());
               //     CrdbAmountVat12point5 = Convert.ToDouble(drow["AmountVAT12Point5Per"].ToString());
                    SchemeTotalDiscount = Convert.ToDouble(drow["AmountSchemeDiscount"].ToString());
                    IfFullPayment = "Y";                   
                    if (CrdbAmountBalance > 0)
                        IfFullPayment = "N";

                    CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    if (drow["DiscountAmountCB"] != DBNull.Value)
                        DiscountAmountCB = Convert.ToDouble(drow["DiscountAmountCB"].ToString());
                 //   if (drow["AccTokenNumber"] != DBNull.Value)
                  //      TokenNumber = Convert.ToInt32(drow["AccTokenNumber"].ToString());
                    CrdbTotalAmount = CrdbBillAmount + CrdbRoundAmount;

                    if (drow["ProfitInRupees"] != DBNull.Value)
                        TotalProfitInRupees = Convert.ToDouble(drow["ProfitInRupees"].ToString());

                    if (drow["ProfitPercentBySaleRate"] != DBNull.Value)
                        TotalProfitPercentBySaleRate = Convert.ToDouble(drow["ProfitPercentBySaleRate"].ToString());

                    if (drow["ProfitPercentByPurchaseRate"] != DBNull.Value)
                        TotalProfitPercentByPurchaseRate = Convert.ToDouble(drow["ProfitPercentByPurchaseRate"].ToString());

               //     if (drow["AmountPMTDiscount"] != DBNull.Value)
                //        PMTTotalDiscount = Convert.ToDouble(drow["AmountPMTDiscount"].ToString());
                    if (drow["AmountItemDiscount"] != DBNull.Value)
                        ItemTotalDiscount = Convert.ToDouble(drow["AmountItemDiscount"].ToString());
              //      if (drow["Telephone"] != DBNull.Value)
              //          Telephone = drow["Telephone"].ToString();
              //      if (drow["MySpecialDiscountPercent"] != DBNull.Value)
              //          MySpecialDiscountPer = Convert.ToDouble(drow["MySpecialDiscountPercent"].ToString());
              //      if (drow["MySpecialDiscountAmount"] != DBNull.Value)
              //          MySpecialDiscountAmount = Convert.ToDouble(drow["MySpecialDiscountAmount"].ToString());
              //      if (drow["MySpecialDiscountAmount12point5"] != DBNull.Value)
              //          MyTotalSpecialDiscountPer12point5 = Convert.ToDouble(drow["MySpecialDiscountAmount12point5"].ToString());
              //      if (drow["MySpecialDiscountAmount5"] != DBNull.Value)
               //         MyTotalSpecialDiscountPer5 = Convert.ToDouble(drow["MySpecialDiscountAmount5"].ToString());
               //     if (drow["AmountCashDiscount5"] != DBNull.Value)
               //         TotalDiscount5 = Convert.ToDouble(drow["AmountCashDiscount5"].ToString());
               //     if (drow["AmountCashDiscount12point5"] != DBNull.Value)
               //         TotalDiscount12point5 = Convert.ToDouble(drow["AmountCashDiscount12point5"].ToString());
               //     if (drow["NextVisitDate"] != DBNull.Value)
               //         NextVisitDate = drow["NextVisitDate"].ToString();
               //     else
               //         NextVisitDate = ""; 
             //       if (drow["DebtorsPatientID"] != DBNull.Value)
              //          DebtorsPatientID = drow["DebtorsPatientID"].ToString();
              //      else
              //          DebtorsPatientID = "";

             //       if (drow["CreditCardBankID"] != DBNull.Value)
             //           CreditCardBankID = drow["CreditCardBankID"].ToString();
             //       else
             //           CreditCardBankID = "";
                    if (drow["amountgst0"] != DBNull.Value)
                        GSTAmt0 = Convert.ToDouble(drow["amountgst0"].ToString());
                    if (drow["amountgsts5"] != DBNull.Value)
                        GSTAmtS5 = Convert.ToDouble(drow["amountgsts5"].ToString());
                    if (drow["amountgstc5"] != DBNull.Value)
                        GSTAmtC5 = Convert.ToDouble(drow["amountgstc5"].ToString());
                    if (drow["amountgsti5"] != DBNull.Value)
                        GSTAmtI5 = Convert.ToDouble(drow["amountgsti5"].ToString());
                    if (drow["amountgsts12"] != DBNull.Value)
                        GSTAmtS12 = Convert.ToDouble(drow["amountgsts12"].ToString());
                    if (drow["amountgstc12"] != DBNull.Value)
                        GSTAmtC12 = Convert.ToDouble(drow["amountgstc12"].ToString());
                    if (drow["amountgsti12"] != DBNull.Value)
                        GSTAmtI12 = Convert.ToDouble(drow["amountgsti12"].ToString());

                    if (drow["amountgsts18"] != DBNull.Value)
                        GSTAmtS18 = Convert.ToDouble(drow["amountgsts18"].ToString());
                    if (drow["amountgstc18"] != DBNull.Value)
                        GSTAmtC18 = Convert.ToDouble(drow["amountgstc18"].ToString());
                    if (drow["amountgsti18"] != DBNull.Value)
                        GSTAmtI18 = Convert.ToDouble(drow["amountgsti18"].ToString());

                    if (drow["amountgsts28"] != DBNull.Value)
                        GSTAmtS28 = Convert.ToDouble(drow["amountgsts28"].ToString());
                    if (drow["amountgstc28"] != DBNull.Value)
                        GSTAmtC28 = Convert.ToDouble(drow["amountgstc28"].ToString());
                    if (drow["amountgsti28"] != DBNull.Value)
                        GSTAmtI28 = Convert.ToDouble(drow["amountgsti28"].ToString());

                    if (drow["gsts5"] != DBNull.Value)
                        GSTS5 = Convert.ToDouble(drow["gsts5"].ToString());
                    if (drow["gstc5"] != DBNull.Value)
                        GSTC5 = Convert.ToDouble(drow["gstc5"].ToString());
                    if (drow["gsti5"] != DBNull.Value)
                        GSTI5 = Convert.ToDouble(drow["gsti5"].ToString());

                    if (drow["gsts12"] != DBNull.Value)
                        GSTS12 = Convert.ToDouble(drow["gsts12"].ToString());
                    if (drow["gstc12"] != DBNull.Value)
                        GSTC12 = Convert.ToDouble(drow["gstc12"].ToString());
                    if (drow["gsti12"] != DBNull.Value)
                        GSTI12 = Convert.ToDouble(drow["gsti12"].ToString());

                    if (drow["gsts18"] != DBNull.Value)
                        GSTS18 = Convert.ToDouble(drow["gsts18"].ToString());
                    if (drow["gstc18"] != DBNull.Value)
                        GSTC18 = Convert.ToDouble(drow["gstc18"].ToString());
                    if (drow["gsti18"] != DBNull.Value)
                        GSTI18 = Convert.ToDouble(drow["gsti18"].ToString());

                    if (drow["gsts28"] != DBNull.Value)
                        GSTS28 = Convert.ToDouble(drow["gsts28"].ToString());
                    if (drow["gstc28"] != DBNull.Value)
                        GSTC28 = Convert.ToDouble(drow["gstc28"].ToString());
                    if (drow["gsti28"] != DBNull.Value)
                        GSTI28 = Convert.ToDouble(drow["gsti28"].ToString());

                    PreVouType = CrdbVouType;
                    PreCountersaleNumber = CrdbCountersaleNumber;
                    PreVouDate = CrdbVouDate;
                    PreVouNumber = CrdbVouNo;
                    PreSaleSubType = SaleSubType;
                    PreAccountID = AccountID;
                    PreAmountNet = CrdbAmountNet;
                    PreAmountClear = CrdbAmountClear;
                    PreAmountBalance = CrdbAmountBalance;
                    PreAmount = CrdbAmount;
                    PreDiscPer = CrdbDiscPer;
                    PreDiscAmt = CrdbDiscAmt;
                    PreAddOn = CrdbAddOn;
                    PreCrNoteAmount = CrNoteAmount;
                    PreDbNoteAmount = DbNoteAmount;
                    PreNarration1 = CrdbNarration1;
                    PreNarration2 = CrdbNarration2;
                    PreStatementNumber = StatementNumber;
                    PreDocID = DocID;
                    PreDoctorNameAddress = DoctorName;
                    PreDoctorAddress = DoctorAddress;
                    PreTelephone = Telephone;
                    PreOperatorID = OperatorID;
                    PreCrdbName = CrdbName;
                    PrePatientAddress1 = PatientAddress1;
                    PrePatientAddress2 = PatientAddress2;
                    PreShortName = ShortName;
                    PrePatientShortAddress = PatientShortAddress;
                    PreIPDOPD = CrdbIPDOPD;
                    PreOrderNumber = OrderNumber;
                    PreOrderDate = OrderDate;
                    PreVat5 = CrdbVat5;
                    PreVat12point5 = CrdbVat12point5;
                    PreAmtForZeroVAT = CrdbAmtForZeroVAT;
                    PreAmountVat5 = CrdbAmountVat5;
                    PreAmountVat12point5 = CrdbAmountVat12point5;
                    PreRoundAmount = CrdbRoundAmount;
                    PreTotalProfitInRupees = TotalProfitInRupees;
                    PreTotalProfitPercentByPurchaseRate = TotalProfitPercentByPurchaseRate;
                    PreTotalProfitPercentBySaleRate = TotalProfitPercentBySaleRate;
                    PrePMTTotalDiscount = PMTTotalDiscount;
                    PreItemTotalDiscount = ItemTotalDiscount;
                    PrePrescriptionFileName = PrescriptionFileName;
                    PreTotalDiscount12point5 = TotalDiscount12point5;
                    PreTotalDiscount5 = TotalDiscount5;
                    PreMySpecialDiscountAmount = MySpecialDiscountAmount;
                    PreMySpecialDiscountPer = MySpecialDiscountPer;                                       
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public DataTable ReadDetailsByIDSpecialSale()
        {
            DataTable dt = null;
            try
            {
                DataRow drow = null;
                DBSSSale dbsale = new DBSSSale();
                drow = dbsale.ReadDetailsByIDSpecialSale(Id);

                if (drow != null)
                {
                    Id = drow["ID"].ToString();
                    CrdbVouType = drow["VoucherType"].ToString();
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CrdbCountersaleNumber = Convert.ToInt32(drow["CounterSaleNumber"].ToString());
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    SaleSubType = drow["VoucherSubType"].ToString();
                    if (drow["AccountID"] != DBNull.Value && drow["AccountID"].ToString() != string.Empty)
                        AccountID = drow["AccountId"].ToString();
                   
                    CrdbBillAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CrdbBillAmount = CrdbBillAmount + MySpecialDiscountAmount;
                    CrdbAmountNet = CrdbBillAmount;
                    CrdbAmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    CrdbAmountClear = CrdbAmountClear + MySpecialDiscountAmount;
                    CrdbAmountBalance = Convert.ToDouble(drow["AmountBalance"].ToString());
                    CrdbAmount = Convert.ToDouble(drow["AmountGross"].ToString());
                    CrdbAmount = CrdbAmount + MySpecialDiscountAmount;
                    CrdbDiscPer = Convert.ToDouble(drow["CashDiscountPercent"].ToString());
                    CrdbDiscAmt = Convert.ToDouble(drow["AmountCashDiscount"].ToString());
                    //  ItemTotalDiscount = Convert.ToDouble(drow["Amount
                    if (drow["AddOnFreight"] != DBNull.Value)
                        CrdbAddOn = Convert.ToDouble(drow["AddOnFreight"].ToString());
                    if (drow["AmountCreditNote"] != DBNull.Value)
                        CrNoteAmount = Convert.ToDouble(drow["AmountCreditNote"].ToString());
                    if (drow["AmountDebitNote"] != DBNull.Value)
                        DbNoteAmount = Convert.ToDouble(drow["AmountDebitNote"].ToString());
                    CrdbNarration1 = Convert.ToString(drow["Narration1"]);
                    CrdbNarration2 = drow["Narration2"].ToString();
                    if (drow["StatementNumber"] != DBNull.Value)
                        StatementNumber = Convert.ToInt32(drow["StatementNumber"].ToString());
                    if (drow["DoctorID"] != DBNull.Value)
                        DocID = drow["DoctorID"].ToString();
                    if (drow["DoctorShortName"] != DBNull.Value)
                        DoctorName = drow["DoctorShortName"].ToString();
                    if (drow["DoctorAddress"] != DBNull.Value)
                        DoctorAddress = drow["DoctorAddress"].ToString();
                    if (drow["PatientID"] != DBNull.Value)
                        PatientID = drow["PatientID"].ToString();
                    if (drow["OperatorID"] != DBNull.Value)
                        OperatorID = drow["OperatorID"].ToString();
                    if (drow["PatientName"] != DBNull.Value)
                        CrdbName = drow["PatientName"].ToString();
                    if (drow["PatientAddress1"] != DBNull.Value)
                        PatientAddress1 = drow["PatientAddress1"].ToString();
                    if (drow["PatientAddress2"] != DBNull.Value)
                        PatientAddress2 = drow["PatientAddress2"].ToString();
                    if (drow["PatientShortName"] != DBNull.Value)
                        ShortName = drow["PatientShortName"].ToString();
                    if (drow["PatientShortAddress"] != DBNull.Value)
                        PatientShortAddress = drow["PatientShortAddress"].ToString();
                    if (drow["IPDOPDCode"] != DBNull.Value)
                        CrdbIPDOPD = drow["IPDOPDCode"].ToString();
                    if (drow["OrderNumber"] != DBNull.Value)
                        OrderNumber = drow["OrderNumber"].ToString();
                    if (drow["OrderDate"] != DBNull.Value)
                        OrderDate = drow["OrderDate"].ToString();
                    if (drow["Telephone"] != DBNull.Value)
                        Telephone = drow["Telephone"].ToString();
                    
                    //   if (drow["ScanPrescriptionID"] != DBNull.Value)
                    //     PrescriptionID = drow["ScanPrescriptionID"].ToString();
                    //     if (PrescriptionID != string.Empty)
                    //   {
                    //     FileExtension = drow["FileExtension"].ToString();
                    //      Prescription = (byte[])(drow["PrescriptionData"]);

                    //   }
                    CrdbVat5 = Convert.ToDouble(drow["VAT5per"].ToString());
                    CrdbVat12point5 = Convert.ToDouble(drow["VAT12Point5Per"].ToString());
                    CrdbAmtForZeroVAT = Convert.ToDouble(drow["AmountForZeroVAT"].ToString());
                    CrdbAmountVat5 = Convert.ToDouble(drow["AmountVAT5Per"].ToString());
                    CrdbAmountVat12point5 = Convert.ToDouble(drow["AmountVAT12Point5Per"].ToString());
                    //SchemeTotalDiscount = Convert.ToDouble(drow["AmountSchemeDiscount"].ToString());
                    IfFullPayment = "Y";
                    //if (drow["IfFullPayment"] != DBNull.Value)
                    //    IfFullPayment = drow["IfFullPayment"].ToString();

                    CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    if (drow["DiscountAmountCB"] != DBNull.Value)
                        DiscountAmountCB = Convert.ToDouble(drow["DiscountAmountCB"].ToString());
                    if (drow["AccTokenNumber"] != DBNull.Value)
                        TokenNumber = Convert.ToInt32(drow["AccTokenNumber"].ToString());
                    CrdbTotalAmount = CrdbBillAmount - CrdbRoundAmount;

                    if (drow["ProfitInRupees"] != DBNull.Value)
                        TotalProfitInRupees = Convert.ToDouble(drow["ProfitInRupees"].ToString());

                    if (drow["ProfitPercentBySaleRate"] != DBNull.Value)
                        TotalProfitPercentBySaleRate = Convert.ToDouble(drow["ProfitPercentBySaleRate"].ToString());

                    if (drow["ProfitPercentByPurchaseRate"] != DBNull.Value)
                        TotalProfitPercentByPurchaseRate = Convert.ToDouble(drow["ProfitPercentByPurchaseRate"].ToString());

                    if (drow["AmountPMTDiscount"] != DBNull.Value)
                        PMTTotalDiscount = Convert.ToDouble(drow["AmountPMTDiscount"].ToString());
                    if (drow["AmountItemDiscount"] != DBNull.Value)
                        ItemTotalDiscount = Convert.ToDouble(drow["AmountItemDiscount"].ToString());
                    if (drow["Telephone"] != DBNull.Value)
                        Telephone = drow["Telephone"].ToString();
                    //if (drow["MySpecialDiscountPercent"] != DBNull.Value)
                    //    MySpecialDiscountPer = Convert.ToDouble(drow["MySpecialDiscountPercent"].ToString());
                    //if (drow["MySpecialDiscountAmount"] != DBNull.Value)
                    //    MySpecialDiscountAmount = Convert.ToDouble(drow["MySpecialDiscountAmount"].ToString());
                    //if (drow["MySpecialDiscountAmount12point5"] != DBNull.Value)
                    //    MyTotalSpecialDiscountPer12point5 = Convert.ToDouble(drow["MySpecialDiscountAmount12point5"].ToString());
                    //if (drow["MySpecialDiscountAmount5"] != DBNull.Value)
                    //    MyTotalSpecialDiscountPer5 = Convert.ToDouble(drow["MySpecialDiscountAmount5"].ToString());
                    //if (drow["AmountCashDiscount5"] != DBNull.Value)
                    //    TotalDiscount5 = Convert.ToDouble(drow["AmountCashDiscount5"].ToString());
                    //if (drow["AmountCashDiscount12point5"] != DBNull.Value)
                    //    TotalDiscount12point5 = Convert.ToDouble(drow["AmountCashDiscount12point5"].ToString());

                   


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }
        public DataTable ReadDetailsByIDForChanged()
        {
            DataTable dt = null;
            try
            {
                DataRow drow = null;
                DBSSSale dbsale = new DBSSSale();
                drow = dbsale.ReadDetailsByIDForChanged(Id, SaleSubType);

                if (drow != null)
                {
                    Id = drow["ChangedID"].ToString();
                    CrdbVouType = drow["VoucherType"].ToString();
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CrdbCountersaleNumber = Convert.ToInt32(drow["CounterSaleNumber"].ToString());
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    SaleSubType = drow["VoucherSubType"].ToString();
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = drow["AccountId"].ToString();
                    CrdbBillAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CrdbAmountNet = CrdbBillAmount;
                    CrdbAmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    CrdbAmountBalance = Convert.ToDouble(drow["AmountBalance"].ToString());
                    CrdbAmount = Convert.ToDouble(drow["AmountGross"].ToString());
                    CrdbDiscPer = Convert.ToDouble(drow["CashDiscountPercent"].ToString());
                    CrdbDiscAmt = Convert.ToDouble(drow["AmountCashDiscount"].ToString());
                    if (drow["AddOnFreight"] != DBNull.Value)
                        CrdbAddOn = Convert.ToDouble(drow["AddOnFreight"].ToString());
                    if (drow["AmountCreditNote"] != DBNull.Value)
                        CrNoteAmount = Convert.ToDouble(drow["AmountCreditNote"].ToString());
                    if (drow["AmountDebitNote"] != DBNull.Value)
                        DbNoteAmount = Convert.ToDouble(drow["AmountDebitNote"].ToString());
                    CrdbNarration1 = Convert.ToString(drow["Narration1"]);
                    CrdbNarration2 = drow["Narration2"].ToString();
                    if (drow["StatementNumber"] != DBNull.Value)
                        StatementNumber = Convert.ToInt32(drow["StatementNumber"].ToString());
                    if (drow["DoctorID"] != DBNull.Value)
                        DocID = drow["DoctorID"].ToString();
                    if (drow["DoctorShortName"] != DBNull.Value)
                        DoctorName = drow["DoctorShortName"].ToString();
                    if (drow["DoctorAddress"] != DBNull.Value)
                        DoctorAddress = drow["DoctorAddress"].ToString();
                    if (drow["PatientID"] != DBNull.Value)
                        PatientID = drow["PatientID"].ToString();
                    if (drow["OperatorID"] != DBNull.Value)
                        OperatorID = drow["OperatorID"].ToString();
                    if (drow["PatientName"] != DBNull.Value)
                        CrdbName = drow["PatientName"].ToString();
                    if (drow["PatientAddress1"] != DBNull.Value)
                        PatientAddress1 = drow["PatientAddress1"].ToString();
                    if (drow["PatientAddress2"] != DBNull.Value)
                        PatientAddress2 = drow["PatientAddress2"].ToString();
                    if (drow["PatientShortName"] != DBNull.Value)
                        ShortName = drow["PatientShortName"].ToString();
                    if (drow["PatientShortAddress"] != DBNull.Value)
                        PatientShortAddress = drow["PatientShortAddress"].ToString();
                    if (drow["IPDOPDCode"] != DBNull.Value)
                        CrdbIPDOPD = drow["IPDOPDCode"].ToString();
                    if (drow["OrderNumber"] != DBNull.Value)
                        OrderNumber = drow["OrderNumber"].ToString();
                    if (drow["OrderDate"] != DBNull.Value)
                        OrderDate = drow["OrderDate"].ToString();
                    CrdbVat5 = Convert.ToDouble(drow["VAT5per"].ToString());
                    CrdbVat12point5 = Convert.ToDouble(drow["VAT12Point5Per"].ToString());
                    CrdbAmtForZeroVAT = Convert.ToDouble(drow["AmountForZeroVAT"].ToString());
                    CrdbAmountVat5 = Convert.ToDouble(drow["AmountVAT5Per"].ToString());
                    CrdbAmountVat12point5 = Convert.ToDouble(drow["AmountVAT12Point5Per"].ToString());

                    CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    if (drow["DiscountAmountCB"] != DBNull.Value)
                        DiscountAmountCB = Convert.ToDouble(drow["DiscountAmountCB"].ToString());
                    if (drow["AccTokenNumber"] != DBNull.Value)
                        TokenNumber = Convert.ToInt32(drow["AccTokenNumber"].ToString());
                    CrdbTotalAmount = CrdbBillAmount - CrdbRoundAmount;

                    if (drow["ProfitInRupees"] != DBNull.Value)
                        TotalProfitInRupees = Convert.ToDouble(drow["ProfitInRupees"].ToString());

                    if (drow["ProfitPercentBySaleRate"] != DBNull.Value)
                        TotalProfitPercentBySaleRate = Convert.ToDouble(drow["ProfitPercentBySaleRate"].ToString());

                    if (drow["ProfitPercentByPurchaseRate"] != DBNull.Value)
                        TotalProfitPercentByPurchaseRate = Convert.ToDouble(drow["ProfitPercentByPurchaseRate"].ToString());

                    if (drow["AmountPMTDiscount"] != DBNull.Value)
                        PMTTotalDiscount = Convert.ToDouble(drow["AmountPMTDiscount"].ToString());
                    if (drow["AmountItemDiscount"] != DBNull.Value)
                        ItemTotalDiscount = Convert.ToDouble(drow["AmountItemDiscount"].ToString());


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public DataTable ReadDetailsByIDForDeleted()
        {
            DataTable dt = null;
            try
            {
                DataRow drow = null;
                DBSSSale dbsale = new DBSSSale();
                drow = dbsale.ReadDetailsByIDForDeleted(Id, SaleSubType);

                if (drow != null)
                {
                    Id = drow["ID"].ToString();
                    CrdbVouType = drow["VoucherType"].ToString();
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CrdbCountersaleNumber = Convert.ToInt32(drow["CounterSaleNumber"].ToString());
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    SaleSubType = drow["VoucherSubType"].ToString();
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = drow["AccountId"].ToString();
                    CrdbBillAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CrdbAmountNet = CrdbBillAmount;
                    CrdbAmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    CrdbAmountBalance = Convert.ToDouble(drow["AmountBalance"].ToString());
                    CrdbAmount = Convert.ToDouble(drow["AmountGross"].ToString());
                    CrdbDiscPer = Convert.ToDouble(drow["CashDiscountPercent"].ToString());
                    CrdbDiscAmt = Convert.ToDouble(drow["AmountCashDiscount"].ToString());
                    if (drow["AddOnFreight"] != DBNull.Value)
                        CrdbAddOn = Convert.ToDouble(drow["AddOnFreight"].ToString());
                    if (drow["AmountCreditNote"] != DBNull.Value)
                        CrNoteAmount = Convert.ToDouble(drow["AmountCreditNote"].ToString());
                    if (drow["AmountDebitNote"] != DBNull.Value)
                        DbNoteAmount = Convert.ToDouble(drow["AmountDebitNote"].ToString());
                    CrdbNarration1 = Convert.ToString(drow["Narration1"]);
                    CrdbNarration2 = drow["Narration2"].ToString();
                    if (drow["StatementNumber"] != DBNull.Value)
                        StatementNumber = Convert.ToInt32(drow["StatementNumber"].ToString());
                    if (drow["DoctorID"] != DBNull.Value)
                        DocID = drow["DoctorID"].ToString();
                    if (drow["DoctorShortName"] != DBNull.Value)
                        DoctorName = drow["DoctorShortName"].ToString();
                    if (drow["DoctorAddress"] != DBNull.Value)
                        DoctorAddress = drow["DoctorAddress"].ToString();
                    if (drow["PatientID"] != DBNull.Value)
                        PatientID = drow["PatientID"].ToString();
                    if (drow["OperatorID"] != DBNull.Value)
                        OperatorID = drow["OperatorID"].ToString();
                    if (drow["PatientName"] != DBNull.Value)
                        CrdbName = drow["PatientName"].ToString();
                    if (drow["PatientAddress1"] != DBNull.Value)
                        PatientAddress1 = drow["PatientAddress1"].ToString();
                    if (drow["PatientAddress2"] != DBNull.Value)
                        PatientAddress2 = drow["PatientAddress2"].ToString();
                    if (drow["PatientShortName"] != DBNull.Value)
                        ShortName = drow["PatientShortName"].ToString();
                    if (drow["PatientShortAddress"] != DBNull.Value)
                        PatientShortAddress = drow["PatientShortAddress"].ToString();
                    if (drow["IPDOPDCode"] != DBNull.Value)
                        CrdbIPDOPD = drow["IPDOPDCode"].ToString();
                    if (drow["OrderNumber"] != DBNull.Value)
                        OrderNumber = drow["OrderNumber"].ToString();
                    if (drow["OrderDate"] != DBNull.Value)
                        OrderDate = drow["OrderDate"].ToString();
                    CrdbVat5 = Convert.ToDouble(drow["VAT5per"].ToString());
                    CrdbVat12point5 = Convert.ToDouble(drow["VAT12Point5Per"].ToString());
                    CrdbAmtForZeroVAT = Convert.ToDouble(drow["AmountForZeroVAT"].ToString());
                    CrdbAmountVat5 = Convert.ToDouble(drow["AmountVAT5Per"].ToString());
                    CrdbAmountVat12point5 = Convert.ToDouble(drow["AmountVAT12Point5Per"].ToString());

                    CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    if (drow["DiscountAmountCB"] != DBNull.Value)
                        DiscountAmountCB = Convert.ToDouble(drow["DiscountAmountCB"].ToString());
                    if (drow["AccTokenNumber"] != DBNull.Value)
                        TokenNumber = Convert.ToInt32(drow["AccTokenNumber"].ToString());
                    CrdbTotalAmount = CrdbBillAmount - CrdbRoundAmount;

                    if (drow["ProfitInRupees"] != DBNull.Value)
                        TotalProfitInRupees = Convert.ToDouble(drow["ProfitInRupees"].ToString());

                    if (drow["ProfitPercentBySaleRate"] != DBNull.Value)
                        TotalProfitPercentBySaleRate = Convert.ToDouble(drow["ProfitPercentBySaleRate"].ToString());

                    if (drow["ProfitPercentByPurchaseRate"] != DBNull.Value)
                        TotalProfitPercentByPurchaseRate = Convert.ToDouble(drow["ProfitPercentByPurchaseRate"].ToString());

                    if (drow["AmountPMTDiscount"] != DBNull.Value)
                        PMTTotalDiscount = Convert.ToDouble(drow["AmountPMTDiscount"].ToString());
                    if (drow["AmountItemDiscount"] != DBNull.Value)
                        ItemTotalDiscount = Convert.ToDouble(drow["AmountItemDiscount"].ToString());


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public DataRow ReadDetailsByVouNumber()
        {
            DataRow drow = null;
            try
            {
                DBSSSale dbsale = new DBSSSale();
                drow = dbsale.ReadDetailsByVouNumber(CrdbVouType, SaleSubType, CrdbVouNo, CrdbVouSeries);

                if (drow != null)
                {
                    Id = drow["ID"].ToString();
                    CrdbVouType = drow["VoucherType"].ToString();
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CrdbCountersaleNumber = Convert.ToInt32(drow["CounterSaleNumber"].ToString());
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    SaleSubType = drow["VoucherSubType"].ToString();
                    //if (drow["AccountID"] != DBNull.Value)
                    //    AccountID = drow["AccountId"].ToString();
                    //CrdbBillAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    //CrdbAmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    //CrdbAmountBalance = Convert.ToDouble(drow["AmountBalance"].ToString());
                    //CrdbAmount = Convert.ToDouble(drow["AmountGross"].ToString());
                    //CrdbDiscPer = Convert.ToDouble(drow["CashDiscountPercent"].ToString());
                    //CrdbDiscAmt = Convert.ToDouble(drow["AmountCashDiscount"].ToString());
                    //if (drow["AddOnFreight"] != DBNull.Value)
                    //    CrdbAddOn = Convert.ToDouble(drow["AddOnFreight"].ToString());
                    //if (drow["AmountCreditNote"] != DBNull.Value)
                    //    CrNoteAmount = Convert.ToDouble(drow["AmountCreditNote"].ToString());
                    //if (drow["AmountDebitNote"] != DBNull.Value)
                    //    DbNoteAmount = Convert.ToDouble(drow["AmountDebitNote"].ToString());
                    //CrdbNarration = Convert.ToString(drow["Narration"]);
                    //if (drow["StatementNumber"] != DBNull.Value)
                    //    StatementNumber = Convert.ToInt32(drow["StatementNumber"].ToString());
                    //if (drow["DoctorID"] != DBNull.Value)
                    //    DocID = drow["DoctorID"].ToString();
                    //if (drow["PatientID"] != DBNull.Value)
                    //    AccountID = drow["PatientID"].ToString();
                    //if (drow["OperatorID"] != DBNull.Value)
                    //    OperatorID = drow["OperatorID"].ToString();
                    //if (drow["PatientName"] != DBNull.Value)
                    //    CrdbName = drow["PatientName"].ToString();
                    //if (drow["PatientAddress1"] != DBNull.Value)
                    //    PatientAddress1 = drow["PatientAddress1"].ToString();
                    //if (drow["PatientAddress2"] != DBNull.Value)
                    //    PatientAddress2 = drow["PatientAddress2"].ToString();
                    //if (drow["PatientShortName"] != DBNull.Value)
                    //    ShortName = drow["PatientShortName"].ToString();
                    //if (drow["IPDOPDCode"] != DBNull.Value)
                    //    CrdbIPDOPD = drow["IPDOPDCode"].ToString();
                    //if (drow["OrderNumber"] != DBNull.Value)
                    //    OrderNumber = drow["OrderNumber"].ToString();
                    //if (drow["OrderDate"] != DBNull.Value)
                    //    OrderDate = drow["OrderDate"].ToString();
                    //CrdbVat5 = Convert.ToDouble(drow["VAT5per"].ToString());
                    //CrdbVat12point5 = Convert.ToDouble(drow["VAT12Point5Per"].ToString());
                    //CrdbAmtForZeroVAT = Convert.ToDouble(drow["AmountForZeroVAT"].ToString());
                    //CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    //if (drow["DiscountAmountCB"] != DBNull.Value)
                    //    DiscountAmountCB = Convert.ToDouble(drow["DiscountAmountCB"].ToString());
                    //if (drow["AccTokenNumber"] != DBNull.Value)
                    //    TokenNumber = Convert.ToInt32(drow["AccTokenNumber"].ToString());
                    //CrdbTotalAmount = CrdbBillAmount - CrdbRoundAmount;
                    //if (drow["AmountPMTDiscount"] != DBNull.Value)
                    //  PMTTotalDiscount = Convert.ToDouble(drow["AmountPMTDiscount"].ToString());

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return drow;
        }
        public DataRow ReadDetailsByVouNumberSpecialSale()
        {
            DataRow drow = null;
            try
            {
                DBSSSale dbsale = new DBSSSale();
                drow = dbsale.ReadDetailsByVouNumberSpecialSale(CrdbVouType, CrdbVouNo, CrdbVouSeries);

                if (drow != null)
                {
                    Id = drow["ID"].ToString();
                    CrdbVouType = drow["VoucherType"].ToString();
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CrdbCountersaleNumber = Convert.ToInt32(drow["CounterSaleNumber"].ToString());
                    CrdbVouDate = drow["VoucherDate"].ToString();
                  //  SaleSubType = drow["VoucherSubType"].ToString();
                    //if (drow["AccountID"] != DBNull.Value)
                    //    AccountID = drow["AccountId"].ToString();
                    //CrdbBillAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    //CrdbAmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    //CrdbAmountBalance = Convert.ToDouble(drow["AmountBalance"].ToString());
                    //CrdbAmount = Convert.ToDouble(drow["AmountGross"].ToString());
                    //CrdbDiscPer = Convert.ToDouble(drow["CashDiscountPercent"].ToString());
                    //CrdbDiscAmt = Convert.ToDouble(drow["AmountCashDiscount"].ToString());
                    //if (drow["AddOnFreight"] != DBNull.Value)
                    //    CrdbAddOn = Convert.ToDouble(drow["AddOnFreight"].ToString());
                    //if (drow["AmountCreditNote"] != DBNull.Value)
                    //    CrNoteAmount = Convert.ToDouble(drow["AmountCreditNote"].ToString());
                    //if (drow["AmountDebitNote"] != DBNull.Value)
                    //    DbNoteAmount = Convert.ToDouble(drow["AmountDebitNote"].ToString());
                    //CrdbNarration = Convert.ToString(drow["Narration"]);
                    //if (drow["StatementNumber"] != DBNull.Value)
                    //    StatementNumber = Convert.ToInt32(drow["StatementNumber"].ToString());
                    //if (drow["DoctorID"] != DBNull.Value)
                    //    DocID = drow["DoctorID"].ToString();
                    //if (drow["PatientID"] != DBNull.Value)
                    //    AccountID = drow["PatientID"].ToString();
                    //if (drow["OperatorID"] != DBNull.Value)
                    //    OperatorID = drow["OperatorID"].ToString();
                    //if (drow["PatientName"] != DBNull.Value)
                    //    CrdbName = drow["PatientName"].ToString();
                    //if (drow["PatientAddress1"] != DBNull.Value)
                    //    PatientAddress1 = drow["PatientAddress1"].ToString();
                    //if (drow["PatientAddress2"] != DBNull.Value)
                    //    PatientAddress2 = drow["PatientAddress2"].ToString();
                    //if (drow["PatientShortName"] != DBNull.Value)
                    //    ShortName = drow["PatientShortName"].ToString();
                    //if (drow["IPDOPDCode"] != DBNull.Value)
                    //    CrdbIPDOPD = drow["IPDOPDCode"].ToString();
                    //if (drow["OrderNumber"] != DBNull.Value)
                    //    OrderNumber = drow["OrderNumber"].ToString();
                    //if (drow["OrderDate"] != DBNull.Value)
                    //    OrderDate = drow["OrderDate"].ToString();
                    //CrdbVat5 = Convert.ToDouble(drow["VAT5per"].ToString());
                    //CrdbVat12point5 = Convert.ToDouble(drow["VAT12Point5Per"].ToString());
                    //CrdbAmtForZeroVAT = Convert.ToDouble(drow["AmountForZeroVAT"].ToString());
                    //CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    //if (drow["DiscountAmountCB"] != DBNull.Value)
                    //    DiscountAmountCB = Convert.ToDouble(drow["DiscountAmountCB"].ToString());
                    //if (drow["AccTokenNumber"] != DBNull.Value)
                    //    TokenNumber = Convert.ToInt32(drow["AccTokenNumber"].ToString());
                    //CrdbTotalAmount = CrdbBillAmount - CrdbRoundAmount;
                    //if (drow["AmountPMTDiscount"] != DBNull.Value)
                    //  PMTTotalDiscount = Convert.ToDouble(drow["AmountPMTDiscount"].ToString());

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return drow;
        }
        //public DataTable ReadDetailsByVouNumberCounterSale()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        DataRow drow = null;
        //        DBSSSale dbsale = new DBSSSale();
        //        drow = dbsale.ReadDetailsByVouNumberCounterSale(CrdbVouType, SaleSubType, CrdbVouNo);

        //        if (drow != null)
        //        {
        //            Id = drow["ID"].ToString();
        //            CrdbVouType = drow["VoucherType"].ToString();
        //            CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
        //            //CrdbCountersaleNumber = Convert.ToInt32(drow["CounterSaleNumber"].ToString());
        //            CrdbVouDate = drow["VoucherDate"].ToString();
        //            SaleSubType = drow["VoucherSubType"].ToString();
        //            if (drow["AccountID"] != DBNull.Value)
        //                AccountID = drow["AccountId"].ToString();
        //            CrdbBillAmount = Convert.ToDouble(drow["AmountNet"].ToString());
        //            CrdbAmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
        //            CrdbAmountBalance = Convert.ToDouble(drow["AmountBalance"].ToString());
        //            CrdbAmount = Convert.ToDouble(drow["AmountGross"].ToString());
        //            CrdbDiscPer = Convert.ToDouble(drow["CashDiscountPercent"].ToString());
        //            CrdbDiscAmt = Convert.ToDouble(drow["AmountCashDiscount"].ToString());
        //            CrdbAmountNet = CrdbAmount;
        //            CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
        //            if (drow["AddOnFreight"] != DBNull.Value)
        //                CrdbAddOn = Convert.ToDouble(drow["AddOnFreight"].ToString());
        //            if (drow["AmountCreditNote"] != DBNull.Value)
        //                CrNoteAmount = Convert.ToDouble(drow["AmountCreditNote"].ToString());
        //            if (drow["AmountDebitNote"] != DBNull.Value)
        //                DbNoteAmount = Convert.ToDouble(drow["AmountDebitNote"].ToString());
        //            CrdbNarration = Convert.ToString(drow["Narration"]);
        //            if (drow["StatementNumber"] != DBNull.Value)
        //                StatementNumber = Convert.ToInt32(drow["StatementNumber"].ToString());
        //            if (drow["DoctorID"] != DBNull.Value)
        //                DocID = drow["DoctorID"].ToString();
        //            if (drow["DoctorShortName"] != DBNull.Value)
        //                DoctorName = drow["DoctorShortName"].ToString();
        //            if (drow["DoctorAddress"] != DBNull.Value)
        //                DoctorAddress = drow["DoctorAddress"].ToString();
        //            if (drow["PatientID"] != DBNull.Value)
        //                AccountID = drow["PatientID"].ToString();
        //            if (drow["OperatorID"] != DBNull.Value)
        //                OperatorID = drow["OperatorID"].ToString();
        //            if (drow["PatientName"] != DBNull.Value)
        //                CrdbName = drow["PatientName"].ToString();
        //            if (drow["PatientAddress1"] != DBNull.Value)
        //                PatientAddress1 = drow["PatientAddress1"].ToString();
        //            if (drow["PatientAddress2"] != DBNull.Value)
        //                PatientAddress2 = drow["PatientAddress2"].ToString();
        //            if (drow["PatientShortName"] != DBNull.Value)
        //                ShortName = drow["PatientShortName"].ToString();
        //            if (drow["PatientShortAddress"] != DBNull.Value)
        //                PatientShortAddress = drow["PatientShortAddress"].ToString();
        //            if (drow["IPDOPDCode"] != DBNull.Value)
        //                CrdbIPDOPD = drow["IPDOPDCode"].ToString();
        //            if (drow["OrderNumber"] != DBNull.Value)
        //                OrderNumber = drow["OrderNumber"].ToString();
        //            if (drow["OrderDate"] != DBNull.Value)
        //                OrderDate = drow["OrderDate"].ToString();
        //            CrdbVat5 = Convert.ToDouble(drow["VAT5per"].ToString());
        //            CrdbVat12point5 = Convert.ToDouble(drow["VAT12Point5Per"].ToString());
        //            CrdbAmtForZeroVAT = Convert.ToDouble(drow["AmountForZeroVAT"].ToString());
        //            CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
        //            CrdbAmountVat5 = Convert.ToDouble(drow["AmountVAT5Per"].ToString());
        //            CrdbAmountVat12point5 = Convert.ToDouble(drow["AmountVAT12Point5Per"].ToString());


        //            CrdbTotalAmount = CrdbBillAmount - CrdbRoundAmount;

        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //    return dt;
        //}

        public DataTable ReadProductDetailsByID()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = null;
                DBSSSale dbStock = new DBSSSale();
                dt = dbStock.ReadProductDetailsByIDDetailSale(Id);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public DataTable ReadProductDetailsByIDSpecialSale()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = null;
                DBSSSale dbStock = new DBSSSale();
                dt = dbStock.ReadProductDetailsByIDDetailSaleSpecialSale(Id);

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
            try
            {
                dt = null;
                DBSSSale dbStock = new DBSSSale();
                dt = dbStock.ReadProductDetailsByIDDetailSaleForChanged(Id);

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
            try
            {
                dt = null;
                DBSSSale dbStock = new DBSSSale();
                dt = dbStock.ReadProductDetailsByIDDetailSaleForDeleted(Id);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public DataTable ReadPaymentDetailsByID()
        {
            DataTable dt = new DataTable();
            dt = null;
            try
            {
                DBSSSale dbp = new DBSSSale();
                dt = dbp.ReadPaymentDetailsByID(Id);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }
        public DataTable ReadProductDetailsByCloneID(string CloneID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = null;
                DBSSSale dbStock = new DBSSSale();
                dt = dbStock.ReadProductDetailsByIDDetailSale(CloneID);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dt;
        }

        public void ReadBillPrintSettings()
        {
            DataRow drow = null;
            try
            {
                DBSSSale dbsale = new DBSSSale();
                drow = dbsale.ReadBillPrintSettings();

                if (drow != null)
                {
                    if (drow["SetPrintSalebillPrintedPaper"] != DBNull.Value && drow["SetPrintSalebillPrintedPaper"].ToString() != "")
                        SetPrintSaleBillPrintedPaper = drow["SetPrintSalebillPrintedPaper"].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public int GetAndUpdateSaleNumber(string vtype, string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetSale(vtype, voucherseries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return vouno;
        }      

        public int AddDetails()
        {
            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.AddDetails(Id, AccountID, CrdbNarration1,CrdbNarration2, CrdbVouType, CrdbVouNo, CrdbVouDate,
                CrdbAmountNet, CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbRoundAmount,DocID, CrdbAddOn, 
                SaleSubType, CrdbAmountBalance, CrdbAmountClear,StatementNumber, CrNoteAmount,
                DbNoteAmount, CrdbName, PatientAddress1, PatientAddress2, OperatorID,SalesmanID,TransporterID,
                OrderNumber, OrderDate, TotalProfitInRupees,TotalProfitPercentByPurchaseRate, TotalProfitPercentBySaleRate,
                ItemTotalDiscount,SchemeTotalDiscount,
                GSTAmt0, GSTAmtS5, GSTAmtS12, GSTAmtS18, GSTAmtS28, GSTAmtC5, GSTAmtC12, GSTAmtC18, GSTAmtC28,
           GSTS5, GSTS12, GSTS18, GSTS28, GSTC5, GSTC12, GSTC18, GSTC28, GSTAmtI5, GSTAmtI12, GSTAmtI18, GSTAmtI28, GSTI5, GSTI12, GSTI18, GSTI28, CreatedBy, CreatedDate, CreatedTime); 
        }

        public bool AddDetailsSpecialSale()
        {
            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.AddDetailsSpecialSale(Id, AccountID, CrdbNarration1,CrdbNarration2, CrdbVouType, CrdbVouNo, CrdbVouDate,
                CrdbAmountNet, CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,
                DocID, DoctorName, DoctorAddress, CrdbAddOn, SaleSubType, CrdbAmountBalance, CrdbAmountClear,
                CrdbOctoriPer, CrdbOctroiAmount, CrdbCountersaleNumber, StatementNumber, CrNoteAmount,
                DbNoteAmount, CrdbName, PatientAddress1, PatientAddress2, ShortName, PatientShortAddress, CrdbAmtForZeroVAT, OperatorID,
                PatientID, CrdbAmountVat12point5, CrdbAmountVat5, CrdbIPDOPD, OrderNumber, OrderDate, TotalProfitInRupees, TotalProfitPercentByPurchaseRate, TotalProfitPercentBySaleRate, PMTTotalDiscount, ItemTotalDiscount, CreatedBy, CreatedDate, CreatedTime);
        }


        //public void SaveDiscPercentInPatientMaster(string accountID, double discoutPercent)
        //{
        //    DBSSSale dbcrdb = new DBSSSale();
        //    dbcrdb.SaveDiscPercentInPatientMaster(accountID, discoutPercent);
        //}
        public void SaveDiscPercentInAccountMaster(string accountID, double discoutPercent)
        {
            DBSSSale dbcrdb = new DBSSSale();
            dbcrdb.SaveDiscPercentInAccountMaster(accountID, discoutPercent);
        }
        public bool AddProductDetailsSS()
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.AddDetailsProductsSS(IntID, ProductID, Batchno, Quantity, PurchaseRate, MRP, SaleRate, TradeRate,
                Expiry, VATPer, Amount, ExpiryDate, AccountID, CrdbCompanyID, VATAmount, CrdbIfProdDisc, LastStockID,
                DetailId, SerialNumber, ProfitInRupees, ProfitPercentByPurchaseRate, ProfitPercentBySaleRate, PMTDiscountPer,
                PMTDiscountAmount, ItemDiscountPer, ItemDiscountAmount, CrdbDiscAmt, MySpecialDiscountAmount,SchemeDiscountAmount,
                SchemeQuanity,CrdbVouDate,CrdbCountersaleNumber,CrdbVouType, GSTPurchaseAmountZero, GSTSPurchaseAmount, GSTCPurchaseAmount, GSTSAmount, GSTCAmount,
                NewBatchno,NewMRP,NewSaleRate);
        }
        public bool AddProductDetailsSSSpecialSale()
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.AddDetailsProductsSSSpecialSale(Id, ProductID, Batchno, Quantity, PurchaseRate, MRP, SaleRate, TradeRate,
                Expiry, VATPer, Amount, ExpiryDate,
                AccountID, CrdbCompanyID, VATAmount, CrdbIfProdDisc, LastStockID, DetailId, SerialNumber, ProfitInRupees, ProfitPercentByPurchaseRate, ProfitPercentBySaleRate, PMTDiscountPer, PMTDiscountAmount, ItemDiscountPer, ItemDiscountAmount);
        }
        public bool AddDeletedProductDetailsSS()
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.AddDeletedDetailsProductsSS(Id, ProductID, Batchno, Quantity, PurchaseRate, MRP, SaleRate, TradeRate,
                Expiry, VATPer, Amount, ExpiryDate,
                AccountID, CrdbCompanyID, VATAmount, CrdbIfProdDisc, LastStockID, DetailId, SerialNumber, ProfitInRupees, ProfitPercentByPurchaseRate, ProfitPercentBySaleRate, PMTDiscountPer, PMTDiscountAmount, ItemDiscountPer, ItemDiscountAmount);
        }

        public bool AddChangedProductDetailsSS()
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.AddChangedDetailsProductsSS(Id, ChangedID, ProductID, Batchno, Quantity, PurchaseRate, MRP, SaleRate, TradeRate,
                Expiry, VATPer, Amount, ExpiryDate,
                AccountID, CrdbCompanyID, VATAmount, CrdbIfProdDisc, LastStockID, DetailId, SerialNumber, ProfitInRupees, ProfitPercentByPurchaseRate, ProfitPercentBySaleRate, PMTDiscountPer, PMTDiscountAmount, ItemDiscountPer, ItemDiscountAmount);
        }

        public bool UpdateDetails()
        {

            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.UpdateDetails(Id, AccountID, CrdbNarration1,CrdbNarration2, CrdbVouType, CrdbVouNo, CrdbVouDate,
                CrdbAmountNet, CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,
                CrdbAmountNet, DocID, DoctorName, DoctorAddress, CrdbAddOn, SaleSubType, CrdbAmountBalance, CrdbAmountClear,
                CrdbOctoriPer, CrdbOctroiAmount, CrdbCountersaleNumber, StatementNumber, CrNoteAmount, DbNoteAmount, CrdbName,
                PatientAddress1, PatientAddress2, ShortName, PatientShortAddress, CrdbAmtForZeroVAT, OperatorID,PatientID,
                CrdbAmountVat12point5, CrdbAmountVat5, CrdbIPDOPD, OrderNumber, OrderDate, TotalProfitInRupees, 
                TotalProfitPercentByPurchaseRate, TotalProfitPercentBySaleRate, PMTTotalDiscount, ItemTotalDiscount, 
                PrescriptionFileName, Telephone, MySpecialDiscountAmount, MySpecialDiscountPer, MyTotalSpecialDiscountPer12point5,
                MyTotalSpecialDiscountPer5, TotalDiscount12point5, TotalDiscount5, SchemeTotalDiscount, IfFullPayment, NextVisitDate,DebtorsPatientID,
                GSTAmt0, GSTAmtS5, GSTAmtS12, GSTAmtS18, GSTAmtS28, GSTAmtC5, GSTAmtC12, GSTAmtC18, GSTAmtC28,
           GSTS5, GSTS12, GSTS18, GSTS28, GSTC5, GSTC12, GSTC18, GSTC28, GSTAmtI5, GSTAmtI12, GSTAmtI18, GSTAmtI28, GSTI5, GSTI12, GSTI18, GSTI28,ModifiedBy,ModifiedDate,ModifiedTime);
        }
        public bool UpdateDetailsPrescription()
        {

            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.UpdateDetailsPrescription(Id, PrescriptionFileName);
        }

        //public bool ReverseUpdateDetails()
        //{

        //    DBSSSale dbcrdb = new DBSSSale();
        //    return dbcrdb.UpdateDetails(Id, PreAccountID, PreNarration, PreVouType, CrdbVouNo, PreVouDate,
        //        PreAmountNet, PreDiscPer, PreDiscAmt, PreAmount, PreVat5, PreVat12point5, PreRoundAmount,
        //        PreAmountNet, PreDocID, PreDoctorNameAddress, PreDoctorAddress, PreAddOn, PreSaleSubType, PreAmountBalance, PreAmountClear,
        //        PreOctoriPer, PreOctroiAmount, PreCountersaleNumber, PreStatementNumber, PreCrNoteAmount, PreDbNoteAmount, PreCrdbName,
        //        PrePatientAddress1, PrePatientAddress2, PreShortName, PrePatientShortAddress, PreAmtForZeroVAT, PreOperatorID, PreAmountVat12point5, PreAmountVat5, PreIPDOPD, PreOrderNumber, PreOrderDate, PreTotalProfitInRupees, PreTotalProfitPercentByPurchaseRate, PreTotalProfitPercentBySaleRate, PrePMTTotalDiscount, PreItemTotalDiscount, PrescriptionFileName, Telephone, ModifiedBy, ModifiedDate, ModifiedTime);
        //}

        public bool AddDetailsInDeleteMaster()
        {

            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.AddDetailsInDeleteMaster(Id, PreAccountID, PreNarration1,PreNarration2, PreVouType, CrdbVouNo, PreVouDate,
                PreAmountNet, PreDiscPer, PreDiscAmt, PreAmount, PreVat5, PreVat12point5, PreRoundAmount,
                PreAmountNet, PreDocID, PreDoctorNameAddress, PreDoctorAddress, PreAddOn, PreSaleSubType, PreAmountBalance, PreAmountClear,
                PreOctoriPer, PreOctroiAmount, PreCountersaleNumber, PreStatementNumber, PreCrNoteAmount, PreDbNoteAmount, PreCrdbName,
                PrePatientAddress1, PrePatientAddress2, PreShortName, PrePatientShortAddress, PreAmtForZeroVAT, PreOperatorID, PreAmountVat12point5, PreAmountVat5, PreIPDOPD, PreOrderNumber, PreOrderDate, PreTotalProfitInRupees, PreTotalProfitPercentByPurchaseRate, PreTotalProfitPercentBySaleRate, PrePMTTotalDiscount, PreTelephone, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool AddDetailsInChangedMaster()
        {

            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.AddDetailsInChangedMaster(Id, ChangedID, PreAccountID, PreNarration1,PreNarration2, PreVouType, PreVouNumber, PreVouDate,
                PreAmountNet, PreDiscPer, PreDiscAmt, PreAmount, PreVat5, PreVat12point5, PreRoundAmount,
                PreAmountNet, PreDocID, PreDoctorNameAddress, PreDoctorAddress, PreAddOn, PreSaleSubType, PreAmountBalance, PreAmountClear,
                PreOctoriPer, PreOctroiAmount, PreCountersaleNumber, PreStatementNumber, PreCrNoteAmount, PreDbNoteAmount, PreCrdbName,
                PrePatientAddress1, PrePatientAddress2, PreShortName, PrePatientShortAddress, PreAmtForZeroVAT, PreOperatorID, PreAmountVat12point5, PreAmountVat5, PreIPDOPD, PreOrderNumber, PreOrderDate, PreTotalProfitInRupees, PreTotalProfitPercentByPurchaseRate, PreTotalProfitPercentBySaleRate, PrePMTTotalDiscount, PreTelephone, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool DeleteDetails()
        {
            DBSSSale dbData = new DBSSSale();
            return dbData.DeleteDetails(Id);
        }

        public bool UpdateDetailsEditCounterSale()
        {
            //DBSSSale dbsale = new DBSSSale();
            //return dbsale.AddDetails(Id, AccountID, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate,
            //    CrdbAmountNet, CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,
            //    DocID, DoctorName, DoctorAddress, CrdbAddOn, SaleSubType, CrdbAmountBalance, CrdbAmountClear,
            //    CrdbOctoriPer, CrdbOctroiAmount, CrdbCountersaleNumber, StatementNumber, CrNoteAmount,
            //    DbNoteAmount, CrdbName, PatientAddress1, PatientAddress2, ShortName, PatientShortAddress, CrdbAmtForZeroVAT, OperatorID,
            //    PatientID, CrdbAmountVat12point5, CrdbAmountVat5, CrdbIPDOPD, OrderNumber, OrderDate, TotalProfitInRupees,
            //    TotalProfitPercentByPurchaseRate, TotalProfitPercentBySaleRate, PMTTotalDiscount, ItemTotalDiscount, PrescriptionFileName,
            //    Telephone, MySpecialDiscountAmount, MySpecialDiscountPer, MyTotalSpecialDiscountPer12point5, MyTotalSpecialDiscountPer5,
            //    TotalDiscount12point5, TotalDiscount5, SchemeTotalDiscount, IfFullPayment, CreditCardBankID, MobileNumberForSMS, CreatedBy, CreatedDate, CreatedTime);

            DBSSSale dbsale = new DBSSSale();
            return dbsale.UpdateDetailsEditCounterSale(Id, AccountID, CrdbVouType, CrdbVouNo,
                CrdbVouDate, DocID, DoctorName, DoctorAddress, SaleSubType, CrdbCountersaleNumber, CrdbName, PatientID,
                 PatientAddress1, PatientAddress2, ShortName, PatientShortAddress, OperatorID, Telephone,
                 ModifiedBy, ModifiedDate, ModifiedTime);
        }
                
                
                
        //        Id, AccountID, CrdbVouType, CrdbVouNo,
        //        CrdbVouDate, DocID, DoctorName, DoctorAddress, SaleSubType, CrdbCountersaleNumber, CrdbName, PatientID,
        //         PatientAddress1, PatientAddress2, ShortName, PatientShortAddress, OperatorID, Telephone,
        //         ModifiedBy, ModifiedDate, ModifiedTime);
        //}

        public bool UpdateVoucherSaleDeleteMaster(string mmasterSaleID)
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.UpdateVoucherSaleDeleteMaster(mmasterSaleID);
        }

        public bool DeleteDetailsVoucherSale(string mmasterSaleID)
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.DeleteDetailsVoucherSale(mmasterSaleID);
        }
        public bool DeleteDetailsFromtblTrnac(string ID)
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.DeleteDetailsFromtblTrnac(ID);
        }
        public bool UpdateVoucherSaleDeleteDetails(string mdetailSaleID)
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.UpdateVoucherSaleDeleteDetails(mdetailSaleID);
        }
        public bool UpdateVoucherSaleUpdateStock(string mstockID, int mqty)
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.UpdateVoucherSaleUpdateStock(mstockID, mqty);
        }
        public bool UpdateVoucherSaleUpdateMasterProduct(string mproductID, int mqty)
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.UpdateVoucherSaleUpdateMasterProduct(mproductID, mqty);
        }
        public bool UpdateVoucherSaleUpdateMaster(string mmasterSaleID, double mamt, double mvatamt5, double mvatamt12point5, double mvatamtforZero, double mvat5, double mvat12point5, double  mProfitInRupees,double mTotalProfitPercentBySaleRate,double mTotalProfitPercentByPurchaseRate)
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.UpdateVoucherSaleUpdateMaster(mmasterSaleID, mamt, mvatamt5, mvatamt12point5, mvatamtforZero, mvat5, mvat12point5, mProfitInRupees, mTotalProfitPercentBySaleRate, mTotalProfitPercentByPurchaseRate);
        }



        public string CheckForBatchMRPInStockTable()
        {
            DBSsStock sstk = new DBSsStock();
            DataRow drow = null;
            string ifrowfound = "N";
            try
            {
                drow = sstk.GetRecordByStockID(LastStockID);

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

        public int GetStockByStockID()
        {
            DBSsStock sstk = new DBSsStock();
            int batchclosingstock = 0;
            try
            {
                batchclosingstock = sstk.GetStockByStockID(LastStockID);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return batchclosingstock;

        }
        public bool UpdateIntblStock()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateDebtorSaleStock(LastStockID, Quantity);

        }
        public bool AddIntblStockForNegativeStock()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.AddDebtorSaleStockForNegativeStock(ProductID, Batchno, TradeRate, PurchaseRate, MRP, SaleRate,
                Expiry, ExpiryDate, Quantity, SchemeQuanity, VATPer, ProdPakn, StockID, DistributorSaleRate, DistributorSaleRatePercent);

        }
        public bool UpdateIntblStockForDistributor()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateDebtorSaleStock(LastStockID, (Quantity+SchemeQuanity) * ProdPakn);

        }
        public bool UpdateCreditDebitNoteAdjustedDetails(string crdbID, double mamtnet, string VouType, int VouNumber, string VouDate, string BillNumber, string puchaseid, string vouSeries)
        {
            DBCreditDebitNote dbcr = new DBCreditDebitNote();
            return dbcr.UpdateCreditDebitNoteAdjustedDetails(crdbID, mamtnet, VouType, VouNumber, VouDate, BillNumber, puchaseid, vouSeries);
        }
        public bool UpdateCreditDebitNoteforTypeChange(string crdbID, double mamtnet, string VouType, int VouNumber, string VouDate, string BillNumber, string purchaseid)
        {
            DBCreditDebitNote dbcr = new DBCreditDebitNote();
            return dbcr.UpdateCreditDebitNoteforTypeChange(crdbID, mamtnet, VouType, VouNumber, VouDate, BillNumber, purchaseid);
        }

        public bool UpdateDetailsForTypeChange()
        {
            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.UpdateDetailsForTypeChange(Id, CrdbVouType, SaleSubType, CrdbVouNo, CrdbAmountBalance, CrdbAmountClear, AccountID, CreditCardBankID, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool clearPreviousdebitcreditnotes(string purchaseID)
        {
            DBCreditDebitNote dbcr = new DBCreditDebitNote();
            return dbcr.ClearPreviousdebitcreditnotes(purchaseID);
        }
        public bool IfAddToShortList()
        {
            bool retValue = false;
            try
            {
                DataRow dt = null;
                DBSsStock sstk = new DBSsStock();
                if (ProductID != "")
                {
                    dt = sstk.GetDataForAddToStock(ProductID);
                    if (dt != null)
                        retValue = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;


        }

        public bool UpdateSaleStockInMasterProduct()
        {
            int Closingstock = GetClosingStock();
            Closingstock -= Quantity;
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateSaleStockInmasterProduct(ProductID, Closingstock, LastStockID, CrdbVouType, CrdbVouNo, CrdbVouDate, AccountID, ScanCode,MRP );
        }
        public bool UpdateSaleStockInMasterProductForDistributor()
        {
            int Closingstock = GetClosingStock();
            Closingstock -= ((Quantity+SchemeQuanity) * ProdPakn);
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateSaleStockInmasterProduct(ProductID, Closingstock, LastStockID, CrdbVouType, CrdbVouNo, CrdbVouDate, AccountID, ScanCode,MRP);
        }
        public int GetClosingStock()
        {
            DBProduct dbprod = new DBProduct();
            return dbprod.GetClosingStock(ProductID);
        }
        public bool UpdateIntblStockAdd()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateDebtorSaleStockAddFromTemp(LastStockID, Quantity);

        }
        public bool UpdateIntblStockAddForDistributor()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateDebtorSaleStockAddFromTemp(LastStockID, (Quantity+SchemeQuanity) * ProdPakn);

        }
        public bool UpdateDebtorSaleStockInMasterProductAddFromTemp()
        {
            int Closingstock = GetClosingStock();
            Closingstock += Quantity;
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateDebtorSaleStockInmasterProductAddFromTemp(ProductID, Closingstock);
        }
        public bool UpdateDebtorSaleStockInMasterProductAddFromTempForDistributor()
        {
            int Closingstock = GetClosingStock();
            Closingstock += ((Quantity+SchemeQuanity) * ProdPakn);
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateDebtorSaleStockInmasterProductAddFromTemp(ProductID, Closingstock);
        }
        public void GetSettings()
        {
            DBSettings setss = new DBSettings();
            setss.GetOverviewData(General.ShopDetail.ShopVoucherSeries);
        }

        public bool DeletePreviousRecords()
        {
            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.DeleteDebtorSaleByMasterID(Id);
        }

        public DataRow GetCreditorDataByTokenNumber()
        {
            DataRow dRow = null;
            try
            {
                DBAccount actoken = new DBAccount();
                dRow = actoken.ReadCreditorDataByTokenNumber(TokenNumber);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dRow;
        }

        public DataRow GetSaleSettings()
        {
            DataRow dRow = null;
            try
            {
                DBSSSale dbs = new DBSSSale();
                dRow = dbs.GetSaleSettings();

                if (dRow != null)
                {
                    if (dRow["setSaleAskDiscountinCounterSale"] != DBNull.Value)
                        setSaleAskDiscountinCounterSale = dRow["setSaleAskDiscountinCounterSale"].ToString();
                    if (dRow["setSaleAskOperatorinCounterSale"] != DBNull.Value)
                        setSaleAskOperatorinCounterSale = dRow["setSaleAskOperatorinCounterSale"].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dRow;
        }
       

        public void AddToShortList()
        {

            DBSSSale dbsl = new DBSSSale();
            DataRow dr = null;
            string accid = string.Empty;
            try
            {
                dr = dbsl.CheckProductInShortList(ProductID);
                if (dr == null)
                {
                    dr = dbsl.GetAccountIDForShortList(ProductID);
                    if (dr["ProdlastpurchasepartyID"] != DBNull.Value && dr["ProdlastpurchasepartyID"].ToString() != string.Empty)
                        accid = dr["ProdlastpurchasepartyID"].ToString();
                    else if (dr["ProdPartyID_1"] != DBNull.Value && dr["ProdPartyID_1"].ToString() != string.Empty)
                    accid = dr["ProdPartyID_1"].ToString();
                    dbsl.AddToShortList(ProductID, TodayS, ShortListID, PurchaseRate, accid);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public bool AddVoucherIntblTrnac()
        {
            DBSSSale ssale = new DBSSSale();
            return ssale.AddVoucherIntblTrnac(IntID, DebitAccount, CreditAccount, CrdbNarration1, CrdbVouType, CrdbVouNo, CrdbVouDate, DebitAmount, CreditAmount, DetailId, ShortNameForNarration, SaleSubType, CreatedBy, CreatedDate, CreatedTime);
        }
        public DataRow GetSaleIDforClone(int clonevouno, string clonevoutype)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetSaleIDforClone(clonevouno, clonevoutype);
        }
        //public void SaveNewPatient()
        //{
        //    DBSSSale dbSale = new DBSSSale();
        //    dbSale.SaveNewPatient(NewPatientIDInDebtorSale, CrdbName, PatientAddress1, PatientAddress2, ShortName, Telephone, MobileNumberForSMS, DocID, DoctorName, DoctorAddress, CreatedBy, CreatedDate, CreatedTime);
        //}
        public bool SaveNewDoctor()
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.SaveNewDoctor(DocID, DoctorName, DoctorAddress, CreatedBy, CreatedDate, CreatedTime);
        }

        public DataRow GetProductNameFromScanCode(string scanCode)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetProductNameFromScanCode(scanCode);
        }
        #endregion Public Methods



        public bool AddNewRowCheck(PSProductViewControl mpPVC)
        {
            bool retValue = true;

            try
            {
                foreach (DataGridViewRow dr in mpPVC.Rows)
                {
                    if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == string.Empty)
                    {
                        retValue = false;
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public bool AddNewRowCheck(PSMainSubViewControl mpPVC)
        {
            bool retValue = true;

            try
            {
                foreach (DataGridViewRow dr in mpPVC.Rows)
                {
                    if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == string.Empty)
                    {
                        retValue = false;
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public bool AddNewRowCheck(PSSaleViewControl mpPVC)
        {
            bool retValue = true;

            try
            {
                foreach (DataGridViewRow dr in mpPVC.Rows)
                {
                    if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == string.Empty)
                    {
                        retValue = false;
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public DataRow GetSchemeDetails(string mprod)
        {
            DataRow dr = null;
            try
            {
                
                DBSSSale dbScheme = new DBSSSale();
                dr = dbScheme.GetSchemeDetails(mprod);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }

        public void AddDetailsInTempPurchase()
        {
            DBSSSale dbcrdb = new DBSSSale();
            dbcrdb.AddDetailsInTempPurchase(TempChallanID, ProductID, Batchno, Quantity, PurchaseRate, MRP, TradeRate,
                Expiry,LastStockID, CreatedBy, CreatedDate, CreatedTime);
        }
        public void UpdateDetailsInTempPurchase()
        {
            DBSSSale dbcrdb = new DBSSSale();
            dbcrdb.UpdateDetailsInTempPurchase(TempChallanID, Quantity, LastStockID, CreatedBy, CreatedDate, CreatedTime);
        }

        public string CheckForBatchMRPIntblTempPurchase()
        {
            DBSsStock sstk = new DBSsStock();
            DataRow drow = null;
            string stockid = "";
            try
            {
                drow = sstk.GetRowForBatchMRPIntblTempPurchase(ProductID, Batchno, MRP);

                if (drow != null)
                    stockid = drow["StockID"].ToString();              
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return stockid;

        }

        public string CheckForProductBatchMRPInStocktable( )
        {
            DBSsStock sstk = new DBSsStock();
            DataRow drow = null;
            string stockid = "";
            try
            {
                drow = sstk.CheckForProductBatchMRPInStocktable(ProductID, Batchno, MRP);

                if (drow != null)
                    stockid = drow["StockID"].ToString();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return stockid;
        }

       public  void UpdateDetailSaleForNewVoucherTypeAndNumber(string ID, string vouID, string vouType, int vouNumber)
        {
            DBSSSale dbs = new DBSSSale();
            dbs.UpdateDetailSaleForNewVoucherTypeAndNumber(Id, vouID,vouType,vouNumber);
        }

       public void GetPartyOtherDetails(string partyID)
       {
           DataRow dr = null;
           DBSSSale dbs = new DBSSSale();
           dr = dbs.GetPartyOtherDetails(partyID);
           if (dr != null)
           {
               if (dr["AccVatTin"] != DBNull.Value)
                   PatientVATTIN = dr["AccVatTin"].ToString();
               if (dr["AccDLN"] != DBNull.Value)
                   PartyDLN = dr["AccDLN"].ToString();
               if (dr["ACCLBT"] != DBNull.Value)
                  PartyLBT = dr["ACCLBT"].ToString();
               if (dr["AccAddress1"] != DBNull.Value)
                   PatientAddress1 = dr["AccAddress1"].ToString();
               if (dr["AccAddress2"] != DBNull.Value)
                   PatientAddress2 = dr["AccAddress2"].ToString();
               if (dr["AccTelephone"] != DBNull.Value)
                   Telephone = dr["AccTelephone"].ToString();

           }
       }

       public void GetLastRecordForSale()
       {
           DataRow dr;          
           try
           {
               DBSSSale dbs = new DBSSSale();
               dr = dbs.GetLastRecordForSale(CrdbVouType,SaleSubType,CrdbVouSeries);
               if (dr != null && dr["ID"] != null)
               {

                   Id = dr["ID"].ToString();                  
                   
               }
              
           }
           catch (Exception Ex)
           {
               Log.WriteException(Ex);
           }
                   
       }

      public int GetLastVoucherNumber(string vouType, string subType , string vouSeries)
      {
          DataRow dr;
          int lastvouno = 0;
          try
          {
              DBSSSale dbs = new DBSSSale();
              dr = dbs.GetLastVoucherNumber(vouType, subType, vouSeries);
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

      public DataRow GetFirstRecord()
      {
          DataRow dr = null;
          try
          {
              DBSSSale dbs = new DBSSSale();
              dr = dbs.GetFirstRecord(CrdbVouType,SaleSubType,CrdbVouSeries);            
          }
          catch (Exception Ex)
          {
              Log.WriteException(Ex);
          }
          return dr;
      }


      public void GetLastRecordForSaleSpecialSale()
      {
          DataRow dr;
          try
          {
              DBSSSale dbs = new DBSSSale();
              dr = dbs.GetLastRecordForSaleSpecialSale(CrdbVouType, CrdbVouSeries);
              if (dr != null && dr["ID"] != null)
              {

                  Id = dr["ID"].ToString();

              }

          }
          catch (Exception Ex)
          {
              Log.WriteException(Ex);
          }

      }

      public int GetLastVoucherNumberSpecialSale(string vouType, string vouSeries)
      {
          DataRow dr;
          int lastvouno = 0;
          try
          {
              DBSSSale dbs = new DBSSSale();
              dr = dbs.GetLastVoucherNumberSpecialSale(vouType, vouSeries);
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

      public DataRow GetFirstRecordSpecialSale()
      {
          DataRow dr = null;
          try
          {
              DBSSSale dbs = new DBSSSale();
              dr = dbs.GetFirstRecordSpecialSale(CrdbVouType, CrdbVouSeries);
          }
          catch (Exception Ex)
          {
              Log.WriteException(Ex);
          }
          return dr;
      }

      internal void UpdateCreditDebitNoteforTypeChange(string p, string p_2, int p_3, string p_4, string p_5)
      {
          throw new NotImplementedException();
      }

      public string IfmultipleMRP(string mprodno, string mbatchno, double mmrpn)
      {
          string ifm = "N";
          DataRow  dr = null;
          DBSSSale dbs = new DBSSSale();
          dr = dbs.IfMultipleMrp(mprodno, mbatchno, mmrpn);
          if (dr != null)
              ifm = "Y";
          return ifm;
      }



       public  void UpdateDetailSaleForRateInCounterSale(string detailID, double mrate, string voucherType, int voucherNumber)
      {

          DBSSSale dbs = new DBSSSale();
          dbs.UpdateDetailSaleForRateInCounterSale(detailID, mrate, voucherType, voucherNumber);
      }

       //public string GetPutInBlackList(string selectedID)
       //{
       //    string putInBlackList = "N";
       //    DataRow drow = null;
       //    DBSSSale dbsale = new DBSSSale();
       //    drow = dbsale.GetPutInBlackList(selectedID);
       //    if (drow != null)
       //    {
       //        if (drow["PutInBlackList"] != DBNull.Value)
       //            putInBlackList = drow["PutInBlackList"].ToString();
       //    }
       //    return putInBlackList;

       //}

       //public string GetPatientTelephone(string selectedID)
       //{
       //    string telephone = string.Empty;
       //    DataRow drow = null;
       //    DBSSSale dbsale = new DBSSSale();
       //    drow = dbsale.GetPatientTelephone(selectedID);
       //    if (drow != null && drow["TelephoneNumber"] != DBNull.Value)
       //        telephone = drow["TelephoneNumber"].ToString();
       //    return telephone;
       //}



     

       public int GetStockToCheckNegetive()
       {
           int stk = 0;
           DBSsStock sstk = new DBSsStock();
           stk =  sstk.GetStockToCheckNegetive(LastStockID);
           return stk;
       }

       public void UpdateDoctor(string address, string docID)
       {
           DBSSSale dbs = new DBSSSale();
           dbs.UpdateDoctor(address,docID);
       }

       //public void UpdatePatient(string mobilenumber, string telephone, string patientID ,string docID)
       //{
       //    DBSSSale dbs = new DBSSSale();
       //    dbs.UpdatePatient(mobilenumber, telephone, patientID, docID);
       //}

        public DataTable GetNegetiveStockRowsFromtblStock()
        {
            DBSSSale dbs = new DBSSSale();
            DataTable dt = new DataTable();
            dt = dbs.GetNegetiveStockRowsFromtblStock(CrdbVouDate);
            return dt;
        }

        public DataRow GetdebtorDataByMobileNumber(string AccCode)
        {
            DataRow dRow = null;
            try
            {
                DBAccount actoken = new DBAccount();
                dRow = actoken.GetdebtorDataByMobileNumber(AccCode,MobileNumberForSMS);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dRow;
        }
        public DataRow GetDetailsByPatientID(string PatientID) // [ansuman]
        {
            DBSSSale dbs = new DBSSSale();
            return dbs.GetDetailsByPatientID(PatientID);
        }
    }
}
