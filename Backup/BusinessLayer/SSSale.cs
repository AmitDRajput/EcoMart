using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.InterfaceLayer;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;


namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class SSSale : BaseObject
    {

        #region Declaration
        private string _ProductID;
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
        private double _DiscAmountCB;
        private int _ClosingStock;
        private string _TransactionType;
        private string _PatientAddress1;
        private string _PatientAddress2;
        private string _SaleSubType;
        private int _TokenNumber;
        private string _Todays;
        private string _ShortListID;
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

        private double _MySpecialDiscountPer;
        private double _MySpecialDiscountAmount;
        private double _MyTotalSpecialDiscountPer5;
        private double _MyTotalSpecialDiscountPer12point5;
        private double _TotalDiscount5;
        private double _TotalDiscount12point5;
        private double _PreMySpecialDiscountPer;
        private double _PreMySpecialDiscountAmount;
        private double _PreMyTotalSpecialDiscountPer5;
        private double _PreMyTotalSpecialDiscountPer12point5;
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

        private string _PatientId;
        private string _AccountId;
        private string _CrdbName;
        private string _CrdbDocID;
        private string _CrdbNarration;
        private string _CrdbVouType;
        private int _CrdbVouNo;
        private string _CrdbVouDate;
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
        private string _Telephone;

        private string _PreAccountID;
        private string _PreNarration;
        private string _PreVouType;
        private string _PreVouDate;
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

        # region properties

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

        public string IfNewPatient
        {
            get { return _IfNewPatient; }
            set { _IfNewPatient = value; }
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
        public string CrdbDocID
        {
            get { return _CrdbDocID; }
            set { _CrdbDocID = value; }
        }
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

        public string CrdbVouDate
        {
            get { return _CrdbVouDate; }
            set { _CrdbVouDate = value; }
        }
        public string CrdbName
        {
            get { return _CrdbName; }
            set { _CrdbName = value; }
        }
        public string CrdbNarration
        {
            get { return _CrdbNarration; }
            set { _CrdbNarration = value; }
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

        public string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }

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
        private string PreNarration
        {
            get { return _PreNarration; }
            set { _PreNarration = value; }
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
        private double PreAmountNet
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
                _Batchno = "";
                _Expiry = "";
                _ExpiryDate = "";
                _Mrp = 0;
                _ProdPakn = 0;
                _DocID = "";
                _DoctorName = "";
                _DoctorAddress = "";
                _CrdbDocID = "";
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
                _CrdbNarration = "";
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
                _Telephone = "";

                _PrescriptionFileName = "";
                _PrePrescriptionFileName = "";

                _MySpecialDiscountAmount = 0;
                _MySpecialDiscountPer = 0;
                _MyTotalSpecialDiscountPer12point5 = 0;
                _MyTotalSpecialDiscountPer5 = 0;

                _PreMySpecialDiscountAmount = 0;
                _PreMySpecialDiscountPer = 0;

                _PreAccountID = "";
                _PreNarration = "";
                _PreVouType = "";
                _PreVouDate = "";
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
                if (CrdbVouType != FixAccounts.VoucherTypeForVoucherSale)
                {


                    if (CrdbAmountNet == 0)
                        ValidationMessages.Add("Invalid Amount");
                    if (CrdbVouType.Substring(0, 1) != "W")
                    {
                        if (CrdbName == "")
                            ValidationMessages.Add("Please enter Patient Name.");
                        if (DoctorName == string.Empty)
                            ValidationMessages.Add("Please enter Doctor.");
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

        public DataTable GetOverviewData(string vouSubType)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetOverviewData(vouSubType);
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

        public DataTable GetVoucherSaleDataData(string voutdate)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetVoucherSaleDataData(voutdate);
        }
        public DataTable GetRegularSaleOverviewData()
        {
            DBCounterSale dbCountSale = new DBCounterSale();
            return dbCountSale.GetRegularSaleOverviewData();
        }
        public DataTable GetHospitalSaleOverviewData()
        {
            DBCounterSale dbHos = new DBCounterSale();
            return dbHos.GetHospitalSaleOverviewData();
        }
        public DataTable GetDebtorSaleOverviewData()
        {
            DBCounterSale dbdebtor = new DBCounterSale();
            return dbdebtor.GetDebtorSaleOverviewData();
        }
        public DataTable GetDistributorSaleOverviewData()
        {
            DBCounterSale dbdebtor = new DBCounterSale();
            return dbdebtor.GetDistributorSaleOverviewData();
        }
        public DataTable GetInstitutionalSaleOverviewData()
        {
            DBCounterSale dbdebtor = new DBCounterSale();
            return dbdebtor.GetInstitutionalSaleOverviewData();
        }

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
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CrdbCountersaleNumber = Convert.ToInt32(drow["CounterSaleNumber"].ToString());
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    SaleSubType = drow["VoucherSubType"].ToString();
                    if (drow["AccountID"] != DBNull.Value && drow["AccountID"].ToString() != string.Empty)
                        AccountID = drow["AccountId"].ToString();
                    if (drow["MySpecialDiscountAmount"] != DBNull.Value)
                    MySpecialDiscountAmount = Convert.ToDouble(drow["MySpecialDiscountAmount"].ToString());
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
                    if (drow["AddOnFreight"] != DBNull.Value)
                        CrdbAddOn = Convert.ToDouble(drow["AddOnFreight"].ToString());
                    if (drow["AmountCreditNote"] != DBNull.Value)
                        CrNoteAmount = Convert.ToDouble(drow["AmountCreditNote"].ToString());
                    if (drow["AmountDebitNote"] != DBNull.Value)
                        DbNoteAmount = Convert.ToDouble(drow["AmountDebitNote"].ToString());
                    CrdbNarration = Convert.ToString(drow["Narration"]);
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
                    if (drow["ScanPrescriptionFileName"] != DBNull.Value)
                        PrescriptionFileName = drow["ScanPrescriptionFileName"].ToString();
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
                    if (drow["MySpecialDiscountPercent"] != DBNull.Value)
                        MySpecialDiscountPer = Convert.ToDouble(drow["MySpecialDiscountPercent"].ToString());
                    if (drow["MySpecialDiscountAmount"] != DBNull.Value)
                        MySpecialDiscountAmount = Convert.ToDouble(drow["MySpecialDiscountAmount"].ToString());
                    if (drow["MySpecialDiscountAmount12point5"] != DBNull.Value)
                        MyTotalSpecialDiscountPer12point5 = Convert.ToDouble(drow["MySpecialDiscountAmount12point5"].ToString());
                    if (drow["MySpecialDiscountAmount5"] != DBNull.Value)
                        MyTotalSpecialDiscountPer5 = Convert.ToDouble(drow["MySpecialDiscountAmount5"].ToString());
                    if (drow["AmountCashDiscount5"] != DBNull.Value)
                        TotalDiscount5 = Convert.ToDouble(drow["AmountCashDiscount5"].ToString());
                    if (drow["AmountCashDiscount12point5"] != DBNull.Value)
                        TotalDiscount12point5 = Convert.ToDouble(drow["AmountCashDiscount12point5"].ToString());

                    PreVouType = CrdbVouType;
                    PreCountersaleNumber = CrdbCountersaleNumber;
                    PreVouDate = CrdbVouDate;
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
                    PreNarration = CrdbNarration;
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
                    CrdbNarration = Convert.ToString(drow["Narration"]);
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
                    CrdbNarration = Convert.ToString(drow["Narration"]);
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
                drow = dbsale.ReadDetailsByVouNumber(CrdbVouType, SaleSubType, CrdbVouNo);

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

        public DataTable ReadDetailsByVouNumberCounterSale()
        {
            DataTable dt = null;
            try
            {
                DataRow drow = null;
                DBSSSale dbsale = new DBSSSale();
                drow = dbsale.ReadDetailsByVouNumberCounterSale(CrdbVouType, SaleSubType, CrdbVouNo);

                if (drow != null)
                {
                    Id = drow["ID"].ToString();
                    CrdbVouType = drow["VoucherType"].ToString();
                    CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    //CrdbCountersaleNumber = Convert.ToInt32(drow["CounterSaleNumber"].ToString());
                    CrdbVouDate = drow["VoucherDate"].ToString();
                    SaleSubType = drow["VoucherSubType"].ToString();
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = drow["AccountId"].ToString();
                    CrdbBillAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CrdbAmountClear = Convert.ToDouble(drow["AmountClear"].ToString());
                    CrdbAmountBalance = Convert.ToDouble(drow["AmountBalance"].ToString());
                    CrdbAmount = Convert.ToDouble(drow["AmountGross"].ToString());
                    CrdbDiscPer = Convert.ToDouble(drow["CashDiscountPercent"].ToString());
                    CrdbDiscAmt = Convert.ToDouble(drow["AmountCashDiscount"].ToString());
                    CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["AddOnFreight"] != DBNull.Value)
                        CrdbAddOn = Convert.ToDouble(drow["AddOnFreight"].ToString());
                    if (drow["AmountCreditNote"] != DBNull.Value)
                        CrNoteAmount = Convert.ToDouble(drow["AmountCreditNote"].ToString());
                    if (drow["AmountDebitNote"] != DBNull.Value)
                        DbNoteAmount = Convert.ToDouble(drow["AmountDebitNote"].ToString());
                    CrdbNarration = Convert.ToString(drow["Narration"]);
                    if (drow["StatementNumber"] != DBNull.Value)
                        StatementNumber = Convert.ToInt32(drow["StatementNumber"].ToString());
                    if (drow["DoctorID"] != DBNull.Value)
                        DocID = drow["DoctorID"].ToString();
                    if (drow["DoctorShortName"] != DBNull.Value)
                        DoctorName = drow["DoctorShortName"].ToString();
                    if (drow["DoctorAddress"] != DBNull.Value)
                        DoctorAddress = drow["DoctorAddress"].ToString();
                    if (drow["PatientID"] != DBNull.Value)
                        AccountID = drow["PatientID"].ToString();
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
                    CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    CrdbAmountVat5 = Convert.ToDouble(drow["AmountVAT5Per"].ToString());
                    CrdbAmountVat12point5 = Convert.ToDouble(drow["AmountVAT12Point5Per"].ToString());


                    CrdbTotalAmount = CrdbBillAmount - CrdbRoundAmount;

                }
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

        public bool AddDetails()
        {
            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.AddDetails(Id, AccountID, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate,
                CrdbAmountNet, CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,
                DocID, DoctorName, DoctorAddress, CrdbAddOn, SaleSubType, CrdbAmountBalance, CrdbAmountClear,
                CrdbOctoriPer, CrdbOctroiAmount, CrdbCountersaleNumber, StatementNumber, CrNoteAmount,
                DbNoteAmount, CrdbName, PatientAddress1, PatientAddress2, ShortName, PatientShortAddress, CrdbAmtForZeroVAT, OperatorID,
                PatientID, CrdbAmountVat12point5, CrdbAmountVat5, CrdbIPDOPD, OrderNumber, OrderDate, TotalProfitInRupees,
                TotalProfitPercentByPurchaseRate, TotalProfitPercentBySaleRate, PMTTotalDiscount, ItemTotalDiscount, PrescriptionFileName, Telephone, MySpecialDiscountAmount, MySpecialDiscountPer, MyTotalSpecialDiscountPer12point5, MyTotalSpecialDiscountPer5, TotalDiscount12point5, TotalDiscount5, CreatedBy, CreatedDate, CreatedTime);
        }

        public bool AddDetailsSpecialSale()
        {
            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.AddDetailsSpecialSale(Id, AccountID, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate,
                CrdbAmountNet, CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,
                DocID, DoctorName, DoctorAddress, CrdbAddOn, SaleSubType, CrdbAmountBalance, CrdbAmountClear,
                CrdbOctoriPer, CrdbOctroiAmount, CrdbCountersaleNumber, StatementNumber, CrNoteAmount,
                DbNoteAmount, CrdbName, PatientAddress1, PatientAddress2, ShortName, PatientShortAddress, CrdbAmtForZeroVAT, OperatorID,
                PatientID, CrdbAmountVat12point5, CrdbAmountVat5, CrdbIPDOPD, OrderNumber, OrderDate, TotalProfitInRupees, TotalProfitPercentByPurchaseRate, TotalProfitPercentBySaleRate, PMTTotalDiscount, ItemTotalDiscount, CreatedBy, CreatedDate, CreatedTime);
        }


        public void SaveDiscPercentInPatientMaster(string accountID, double discoutPercent)
        {
            DBSSSale dbcrdb = new DBSSSale();
            dbcrdb.SaveDiscPercentInPatientMaster(accountID, discoutPercent);
        }
        public void SaveDiscPercentInAccountMaster(string accountID, double discoutPercent)
        {
            DBSSSale dbcrdb = new DBSSSale();
            dbcrdb.SaveDiscPercentInAccountMaster(accountID, discoutPercent);
        }
        public bool AddProductDetailsSS()
        {
            DBSSSale dbsale = new DBSSSale();
            return dbsale.AddDetailsProductsSS(Id, ProductID, Batchno, Quantity, PurchaseRate, MRP, SaleRate, TradeRate,
                Expiry, VATPer, Amount, ExpiryDate, AccountID, CrdbCompanyID, VATAmount, CrdbIfProdDisc, LastStockID,
                DetailId, SerialNumber, ProfitInRupees, ProfitPercentByPurchaseRate, ProfitPercentBySaleRate, PMTDiscountPer,
                PMTDiscountAmount, ItemDiscountPer, ItemDiscountAmount, CrdbDiscAmt, MySpecialDiscountAmount);
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
            return dbcrdb.UpdateDetails(Id, AccountID, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate,
                CrdbAmountNet, CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,
                CrdbAmountNet, DocID, DoctorName, DoctorAddress, CrdbAddOn, SaleSubType, CrdbAmountBalance, CrdbAmountClear,
                CrdbOctoriPer, CrdbOctroiAmount, CrdbCountersaleNumber, StatementNumber, CrNoteAmount, DbNoteAmount, CrdbName,
                PatientAddress1, PatientAddress2, ShortName, PatientShortAddress, CrdbAmtForZeroVAT, OperatorID, CrdbAmountVat12point5, CrdbAmountVat5, CrdbIPDOPD, OrderNumber, OrderDate, TotalProfitInRupees, TotalProfitPercentByPurchaseRate, TotalProfitPercentBySaleRate, PMTTotalDiscount, ItemTotalDiscount, PrescriptionFileName, Telephone, MySpecialDiscountAmount, MySpecialDiscountPer, MyTotalSpecialDiscountPer12point5, MyTotalSpecialDiscountPer5, TotalDiscount12point5, TotalDiscount5, ModifiedBy, ModifiedDate, ModifiedTime);
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
            return dbcrdb.AddDetailsInDeleteMaster(Id, PreAccountID, PreNarration, PreVouType, CrdbVouNo, PreVouDate,
                PreAmountNet, PreDiscPer, PreDiscAmt, PreAmount, PreVat5, PreVat12point5, PreRoundAmount,
                PreAmountNet, PreDocID, PreDoctorNameAddress, PreDoctorAddress, PreAddOn, PreSaleSubType, PreAmountBalance, PreAmountClear,
                PreOctoriPer, PreOctroiAmount, PreCountersaleNumber, PreStatementNumber, PreCrNoteAmount, PreDbNoteAmount, PreCrdbName,
                PrePatientAddress1, PrePatientAddress2, PreShortName, PrePatientShortAddress, PreAmtForZeroVAT, PreOperatorID, PreAmountVat12point5, PreAmountVat5, PreIPDOPD, PreOrderNumber, PreOrderDate, PreTotalProfitInRupees, PreTotalProfitPercentByPurchaseRate, PreTotalProfitPercentBySaleRate, PrePMTTotalDiscount, PreTelephone, ModifiedBy, ModifiedDate, ModifiedTime);
        }

        public bool AddDetailsInChangedMaster()
        {

            DBSSSale dbcrdb = new DBSSSale();
            return dbcrdb.AddDetailsInChangedMaster(Id, ChangedID, PreAccountID, PreNarration, PreVouType, CrdbVouNo, PreVouDate,
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
            DBSSSale dbsale = new DBSSSale();
            return dbsale.UpdateDetailsEditCounterSale(Id, AccountID, CrdbVouType, CrdbVouNo,
                CrdbVouDate, DocID, DoctorName, DoctorAddress, SaleSubType, CrdbCountersaleNumber, CrdbName, PatientID,
                 PatientAddress1, PatientAddress2, ShortName, PatientShortAddress, OperatorID, Telephone,
                 ModifiedBy, ModifiedDate, ModifiedTime);
        }

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
        public bool UpdateIntblStockForDistributor()
        {
            DBSsStock sstk = new DBSsStock();
            return sstk.UpdateDebtorSaleStock(LastStockID, Quantity * ProdPakn);

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
            return dbcrdb.UpdateDetailsForTypeChange(Id, CrdbVouType, CrdbVouNo, CrdbAmountBalance, ModifiedBy, ModifiedDate, ModifiedTime);
        }
        public bool clearPreviousdebitcreditnotes(string purchaseID)
        {
            DBCreditDebitNote dbcr = new DBCreditDebitNote();
            return dbcr.clearPreviousdebitcreditnotes(purchaseID);
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
            return dbprod.UpdateSaleStockInmasterProduct(ProductID, Closingstock, LastStockID, CrdbVouType, CrdbVouNo, CrdbVouDate, AccountID, ScanCode);
        }
        public bool UpdateSaleStockInMasterProductForDistributor()
        {
            int Closingstock = GetClosingStock();
            Closingstock -= (Quantity * ProdPakn);
            DBProduct dbprod = new DBProduct();
            return dbprod.UpdateSaleStockInmasterProduct(ProductID, Closingstock, LastStockID, CrdbVouType, CrdbVouNo, CrdbVouDate, AccountID, ScanCode);
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
            return sstk.UpdateDebtorSaleStockAddFromTemp(LastStockID, Quantity * ProdPakn);

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
            Closingstock += (Quantity * ProdPakn);
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

        public DataRow GetPatientDataByTokenNumber()
        {
            DataRow dRow = null;
            try
            {
                DBAccount actoken = new DBAccount();
                dRow = actoken.ReadPatientDataByTokenNumber(TokenNumber);
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
            try
            {
                dr = dbsl.CheckProductInShortList(ProductID);
                if (dr == null)
                    dbsl.AddToShortList(ProductID, TodayS, ShortListID, PurchaseRate);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public bool AddVoucherIntblTrnac()
        {
            DBSSSale ssale = new DBSSSale();
            return ssale.AddVoucherIntblTrnac(Id, DebitAccount, CreditAccount, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, DebitAmount, CreditAmount, DetailId, ShortNameForNarration, SaleSubType, CreatedBy, CreatedDate, CreatedTime);
        }
        public DataRow GetSaleIDforClone(int clonevouno, string clonevoutype)
        {
            DBSSSale dbSale = new DBSSSale();
            return dbSale.GetSaleIDforClone(clonevouno, clonevoutype);
        }
        public void SaveNewPatient()
        {
            DBSSSale dbSale = new DBSSSale();
            dbSale.SaveNewPatient(PatientID, CrdbName, PatientAddress1, PatientAddress2, ShortName, Telephone, DocID, DoctorName, DoctorAddress, CreatedBy, CreatedDate, CreatedTime);
        }
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
    }
}
