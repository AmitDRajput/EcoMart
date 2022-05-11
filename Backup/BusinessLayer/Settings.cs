using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.DataLayer;

namespace PharmaSYSRetailPlus.BusinessLayer
{
    public class Settings : BaseObject
    {
        #region Declaration

        private string _MsetPurchaseRounding;
        private string _MsetPurchaseIfCreditStatementPurchase;
        private string _MsetPurchaseAddVATInPurchaseRate;
        private string _MsetPurchaseAddVATInSaleRate;
        private string _MsetPurchaseReadPurchaseOrder;
        private string _MsetPurchaseIfProductWithOctroi;
        private string _MsetPurchaseOctroionZeroVAT;
        private string _MsetPurchaseChangeSaleRate;
        private string _MsetPurchaseMarginByPurchaseRate;
        private string _MsetPurchaseAcceptExpriedItems;
        private string _MsetPurchaseIncludeCreditPurchaseInStatements;

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
        private string _MsetSaleOnlyCashSaleInCounterSale;
        private string _MsetSaleF3KeyForPatientSaleEdit;

        //   private string _MsetGeneralPrintPlainPaper;
        private string _MsetGeneralProfitPercentageByPurchaseRate;
        private string _MsetGeneralExpiryLast;
        private string _MsetGeneralBatchNumberRequired;
        private string _MsetGeneralExpiryDateReuired;


        private string _MsetPrintSaleBill;
        private string _MsetPrintCRDBNote;
        private string _MsetPrintCashBankVoucher;
        private string _MsetPrintPO;

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

        private string _MsetScanBarCode;

        private string _MsetSaleAllowSpecialDiscount;
        private double _MsetSpecialDiscount1;
        private double _MsetSpecialDiscount2;
        private double _MsetSpecialDiscount3;

        // Email

       
        private string _MsetEmailID;
        private string _MsetEmailPassword;
        private string _MsetEmailType;

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

        // Purchase
        public string MsetPurchaseRounding
        {
            get { return _MsetPurchaseRounding; }
            set { _MsetPurchaseRounding = value; }
        }
        public string MsetPurchaseIfCreditStatementPurchase
        {
            get { return _MsetPurchaseIfCreditStatementPurchase; }
            set { _MsetPurchaseIfCreditStatementPurchase = value; }
        }
        public string MsetPurchaseAddVATInPurchaseRate
        {
            get { return _MsetPurchaseAddVATInPurchaseRate; }
            set { _MsetPurchaseAddVATInPurchaseRate = value; }
        }
        public string MsetPurchaseAddVATInSaleRate
        {
            get { return _MsetPurchaseAddVATInSaleRate; }
            set { _MsetPurchaseAddVATInSaleRate = value; }
        }
        public string MsetPurchaseReadPurchaseOrder
        {
            get { return _MsetPurchaseReadPurchaseOrder; }
            set { _MsetPurchaseReadPurchaseOrder = value; }
        }
        public string MsetPurchaseIfProductWithOctroi
        {
            get { return _MsetPurchaseIfProductWithOctroi; }
            set { _MsetPurchaseIfProductWithOctroi = value; }
        }
        public string MsetPurchaseOctroionZeroVAT
        {
            get { return _MsetPurchaseOctroionZeroVAT; }
            set { _MsetPurchaseOctroionZeroVAT = value; }
        }

        public string MsetPurchaseChangeSaleRate
        {
            get { return _MsetPurchaseChangeSaleRate; }
            set { _MsetPurchaseChangeSaleRate = value; }
        }

        public string MsetPurchaseMarginByPurchaseRate
        {
            get { return _MsetPurchaseMarginByPurchaseRate; }
            set { _MsetPurchaseMarginByPurchaseRate = value; }
        }
        public string MsetPurchaseAcceptExpriedItems
        {
            get { return _MsetPurchaseAcceptExpriedItems; }
            set { _MsetPurchaseAcceptExpriedItems = value; }
        }
        public string MsetPurchaseIncludeCreditPurchaseInStatements
        {
            get { return _MsetPurchaseIncludeCreditPurchaseInStatements; }
            set { _MsetPurchaseIncludeCreditPurchaseInStatements = value; }
        }
        // Sale
        public string MsetSaleRoundOff
        {
            get { return _MsetSaleRoundOff; }
            set { _MsetSaleRoundOff = value; }
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
        public string MsetSaleOnlyCashSaleInCounterSale
        {
            get { return _MsetSaleOnlyCashSaleInCounterSale; }
            set { _MsetSaleOnlyCashSaleInCounterSale = value; }

        }
        public string MsetSaleF3KeyForPatientSaleEdit
        {
            get { return _MsetSaleF3KeyForPatientSaleEdit; }
            set { _MsetSaleF3KeyForPatientSaleEdit = value; }

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

        public string MsetScanBarCode
        {
            get { return _MsetScanBarCode; }
            set { _MsetScanBarCode = value; }
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
        #endregion

        #region Internal Methods
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _MsetPurchaseRounding = "Y";
                _MsetPurchaseIfCreditStatementPurchase = "Y";
                _MsetPurchaseAddVATInPurchaseRate = "N";
                _MsetPurchaseAddVATInSaleRate = "N";
                _MsetPurchaseReadPurchaseOrder = "N";
                _MsetPurchaseIfProductWithOctroi = "N";
                _MsetPurchaseOctroionZeroVAT = "N";
                _MsetPurchaseChangeSaleRate = "N";
                _MsetPurchaseMarginByPurchaseRate = "N";
                _MsetPurchaseAcceptExpriedItems = "N";
                _MsetPurchaseIncludeCreditPurchaseInStatements = "N";
                //                
                _MsetSaleRoundOff = "N";
                _MsetSaleCreditSale = "N";
                _MsetSaleAskDiscountinCounterSale = "N";
                _MsetSaleAskRoundinginSale = "N";
                _MsetSaleShowProfitInSaleBill = "N";
                _MsetSaleIPDOPD = "N";
                _MsetSaleDiscountWithoutVAT = "N";
                _MsetSaleIncludeCreditSaleInStatements = "N";
                _MsetSaleSaveCustomerInMaster = "N";
                _MsetSaleShowOnlyMRPInCounterSale = "N";
                _MsetSaleAllowDistributorSale = "N";
                _MsetSaleRoundingTo10Paise = "N";
                _MsetSaleOnlyCashSaleInCounterSale = "N";
                _MsetSaleF3KeyForPatientSaleEdit = "N";
                //
                _MsetGeneralExpiryLast = "N";
                _MsetGeneralProfitPercentageByPurchaseRate = "Y";
                _MsetGeneralBatchNumberRequired = "Y";
                _MsetGeneralExpiryDateReuired = "N";
                //
                _MsetPrintSaleBill = "Y";
                _MsetPrintCRDBNote = "Y";
                _MsetPrintCashBankVoucher = "Y";
                _MsetPrintPO = "Y";
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
                _MsetScanBarCode = "N";

                _MsetSaleAllowSpecialDiscount = "N";
                _MsetSpecialDiscount1 = 0;
                _MsetSpecialDiscount2 = 0;
                _MsetSpecialDiscount3 = 0;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion  Internal Methods

        #region Public Methods

        public void FillSettings()
        {
            try
            {
                DataRow dr;
                DBSettings set = new DBSettings();
                dr = set.GetOverviewData(General.ShopDetail.ShopVoucherSeries);
                if (dr != null)
                {
                    if (dr["setPurchaseRounding"] != DBNull.Value)
                        MsetPurchaseRounding = dr["setPurchaseRounding"].ToString();
                    if (dr["setPurchaseIfCreditPurchase"] != DBNull.Value)
                        MsetPurchaseIfCreditStatementPurchase = dr["setPurchaseIfCreditPurchase"].ToString();
                    if (dr["setPurchaseReadPurchaseOrder"] != DBNull.Value)
                        MsetPurchaseReadPurchaseOrder = dr["setPurchaseReadPurchaseOrder"].ToString();
                    if (dr["setPurchaseIfProductWithOctroi"] != DBNull.Value)
                        MsetPurchaseIfProductWithOctroi = dr["setPurchaseIfProductWithOctroi"].ToString();
                    if (dr["setPurchaseOctroionZeroVAT"] != DBNull.Value)
                        MsetPurchaseOctroionZeroVAT = dr["setPurchaseOctroionZeroVAT"].ToString();
                    if (dr["setPurchaseChangeSaleRate"] != DBNull.Value)
                        MsetPurchaseChangeSaleRate = dr["setPurchaseChangeSaleRate"].ToString();
                    if (dr["setPurchaseMarginbyPurchaseRate"] != DBNull.Value)
                        MsetPurchaseMarginByPurchaseRate = dr["setPurchaseMarginbyPurchaseRate"].ToString();
                    if (dr["setPurchaseAllowExpiredItems"] != DBNull.Value)
                        MsetPurchaseAcceptExpriedItems = dr["setPurchaseAllowExpiredItems"].ToString();
                    if (dr["setPurchaseIncludeCreditPurchaseInStatements"] != DBNull.Value)
                        MsetPurchaseIncludeCreditPurchaseInStatements = dr["setPurchaseIncludeCreditPurchaseInStatements"].ToString();
                    if (dr["setSaleRoundOff"] != DBNull.Value)
                        MsetSaleRoundOff = dr["setSaleRoundOff"].ToString();
                    if (dr["setSaleCreditStatement"] != DBNull.Value)
                        MsetSaleCreditSale = dr["setSaleCreditStatement"].ToString();
                    if (dr["setSaleAskDiscountinCounterSale"] != DBNull.Value)
                        MsetSaleAskDiscountinCounterSale = dr["setSaleAskDiscountinCounterSale"].ToString();
                    if (dr["setSaleAskRoundinginSale"] != DBNull.Value)
                        MsetSaleAskRoundinginSale = dr["setSaleAskRoundinginSale"].ToString();
                    if (dr["setSaleShowProfitInSaleBill"] != DBNull.Value)
                        MsetSaleShowProfitInSaleBill = dr["setSaleShowProfitInSaleBill"].ToString();
                    if (dr["setSaleIPDOPD"] != DBNull.Value)
                        MsetSaleIPDOPD = dr["setSaleIPDOPD"].ToString();
                    if (dr["setSaleDiscountWithoutVAT"] != DBNull.Value)
                        MsetSaleDiscountWithoutVAT = dr["setSaleDiscountWithoutVAT"].ToString();
                    if (dr["setSaleIncludeCreditsaleInStatements"] != DBNull.Value)
                        MsetSaleIncludeCreditSaleInStatements = dr["setSaleIncludeCreditsaleInStatements"].ToString();
                    if (dr["setSaleSaveCustomerInMaster"] != DBNull.Value)
                        MsetSaleSaveCustomerInMaster = dr["setSaleSaveCustomerInMaster"].ToString();
                    if (dr["setSaleShowOnlyMRPInCounterSale"] != DBNull.Value)
                        MsetSaleShowOnlyMRPInCounterSale = dr["setSaleShowOnlyMRPInCounterSale"].ToString();
                    if (dr["setSaleAllowDistributorSale"] != DBNull.Value)
                        MsetSaleAllowDistributorSale = dr["setSaleAllowDistributorSale"].ToString();

                    if (dr["setSaleRoundingTo10Paise"] != DBNull.Value)
                        MsetSaleRoundingTo10Paise = dr["setSaleRoundingTo10Paise"].ToString();
                    if (dr["setSaleOnlyCashSaleInCounterSale"] != DBNull.Value)
                        MsetSaleOnlyCashSaleInCounterSale = dr["setSaleOnlyCashSaleInCounterSale"].ToString();
                    if (dr["setSaleF3KeyForPatientSaleEdit"] != DBNull.Value)
                        MsetSaleF3KeyForPatientSaleEdit = dr["setSaleF3KeyForPatientSaleEdit"].ToString();

                    if (dr["setGeneralProfitPercentageByPurchaseRate"] != DBNull.Value)
                        MsetGeneralProfitPercentageByPurchaseRate = dr["setGeneralProfitPercentageByPurchaseRate"].ToString();
                    if (dr["setGeneralExpiryLast"] != DBNull.Value)
                        MsetGeneralExpiryLast = dr["setGeneralExpiryLast"].ToString();
                    if (dr["setGeneralBatchNumberRequired"] != DBNull.Value)
                        MsetGeneralBatchNumberRequired = dr["setGeneralBatchNumberRequired"].ToString();
                    if (dr["setGeneralExpiryDateRequired"] != DBNull.Value)
                        MsetGeneralExpiryDateReuired = dr["setGeneralExpiryDateRequired"].ToString();
                    if (dr["setAskOperatorOtherThanVoucherSale"] != DBNull.Value)
                        MsetAskOperatorOtherThanVoucherSale = dr["setAskOperatorOtherThanVoucherSale"].ToString();
                    if (dr["setAskOperatorVoucherSale"] != DBNull.Value)
                        MsetAskOperatorVoucherSale = dr["setAskOperatorVoucherSale"].ToString();                   
                    if (dr["setScanBarCode"] != DBNull.Value)
                        MsetScanBarCode = dr["setScanBarCode"].ToString();
                   // if (dr["setPrintSaleBill"] != DBNull.Value)
                     //   MsetPrintSaleBill = dr["setPrintSaleBill"].ToString();
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
                    if (dr["SpecialDiscount1"] != DBNull.Value)
                        MsetSpecialDiscount1 = Convert.ToDouble(dr["SpecialDiscount1"].ToString());
                    if (dr["SpecialDiscount2"] != DBNull.Value)
                        MsetSpecialDiscount2 = Convert.ToDouble(dr["SpecialDiscount2"].ToString());
                    if (dr["SpecialDiscount3"] != DBNull.Value)
                        MsetSpecialDiscount3 = Convert.ToDouble(dr["SpecialDiscount3"].ToString());
                    if (dr["setSaleAllowSpecialDiscount"] != DBNull.Value)
                        MsetSaleAllowSpecialDiscount = dr["setSaleAllowSpecialDiscount"].ToString();
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
                   

                    if (dr["setPurchaseRounding"] != DBNull.Value)
                        MsetPurchaseRounding = dr["setPurchaseRounding"].ToString();
                    if (dr["setPurchaseIfCreditPurchase"] != DBNull.Value)
                        MsetPurchaseIfCreditStatementPurchase = dr["setPurchaseIfCreditPurchase"].ToString();
                    if (dr["setPurchaseReadPurchaseOrder"] != DBNull.Value)
                        MsetPurchaseReadPurchaseOrder = dr["setPurchaseReadPurchaseOrder"].ToString();
                    if (dr["setPurchaseIfProductWithOctroi"] != DBNull.Value)
                        MsetPurchaseIfProductWithOctroi = dr["setPurchaseIfProductWithOctroi"].ToString();
                    if (dr["setPurchaseOctroionZeroVAT"] != DBNull.Value)
                        MsetPurchaseOctroionZeroVAT = dr["setPurchaseOctroionZeroVAT"].ToString();
                    if (dr["setPurchaseChangeSaleRate"] != DBNull.Value)
                        MsetPurchaseChangeSaleRate = dr["setPurchaseChangeSaleRate"].ToString();
                    if (dr["setPurchaseMarginbyPurchaseRate"] != DBNull.Value)
                        MsetPurchaseMarginByPurchaseRate = dr["setPurchaseMarginbyPurchaseRate"].ToString();
                    if (dr["setPurchaseAllowExpiredItems"] != DBNull.Value)
                        MsetPurchaseAcceptExpriedItems = dr["setPurchaseAllowExpiredItems"].ToString();
                    if (dr["setPurchaseIncludeCreditPurchaseInStatements"] != DBNull.Value)
                        MsetPurchaseIncludeCreditPurchaseInStatements = dr["setPurchaseIncludeCreditPurchaseInStatements"].ToString();
                    if (dr["setSaleRoundOff"] != DBNull.Value)
                        MsetSaleRoundOff = dr["setSaleRoundOff"].ToString();
                    if (dr["setSaleCreditStatement"] != DBNull.Value)
                        MsetSaleCreditSale = dr["setSaleCreditStatement"].ToString();
                    if (dr["setSaleAskDiscountinCounterSale"] != DBNull.Value)
                        MsetSaleAskDiscountinCounterSale = dr["setSaleAskDiscountinCounterSale"].ToString();
                    if (dr["setSaleAskRoundinginSale"] != DBNull.Value)
                        MsetSaleAskRoundinginSale = dr["setSaleAskRoundinginSale"].ToString();
                    if (dr["setSaleShowProfitInSaleBill"] != DBNull.Value)
                        MsetSaleShowProfitInSaleBill = dr["setSaleShowProfitInSaleBill"].ToString();
                    if (dr["setSaleIPDOPD"] != DBNull.Value)
                        MsetSaleIPDOPD = dr["setSaleIPDOPD"].ToString();
                    if (dr["setSaleDiscountWithoutVAT"] != DBNull.Value)
                        MsetSaleDiscountWithoutVAT = dr["setSaleDiscountWithoutVAT"].ToString();
                    if (dr["setSaleIncludeCreditsaleInStatements"] != DBNull.Value)
                        MsetSaleIncludeCreditSaleInStatements = dr["setSaleIncludeCreditsaleInStatements"].ToString();
                    if (dr["setSaleSaveCustomerInMaster"] != DBNull.Value)
                        MsetSaleSaveCustomerInMaster = dr["setSaleSaveCustomerInMaster"].ToString();
                    if (dr["setSaleShowOnlyMRPInCounterSale"] != DBNull.Value)
                        MsetSaleShowOnlyMRPInCounterSale = dr["setSaleShowOnlyMRPInCounterSale"].ToString();
                    if (dr["setSaleAllowDistributorSale"] != DBNull.Value)
                        MsetSaleAllowDistributorSale = dr["setSaleAllowDistributorSale"].ToString();

                    if (dr["setSaleRoundingTo10Paise"] != DBNull.Value)
                        MsetSaleRoundingTo10Paise = dr["setSaleRoundingTo10Paise"].ToString();
                    if (dr["setSaleOnlyCashSaleInCounterSale"] != DBNull.Value)
                        MsetSaleOnlyCashSaleInCounterSale = dr["setSaleOnlyCashSaleInCounterSale"].ToString();
                    if (dr["setSaleF3KeyForPatientSaleEdit"] != DBNull.Value)
                        MsetSaleF3KeyForPatientSaleEdit = dr["setSaleF3KeyForPatientSaleEdit"].ToString();

                    if (dr["setGeneralProfitPercentageByPurchaseRate"] != DBNull.Value)
                        MsetGeneralProfitPercentageByPurchaseRate = dr["setGeneralProfitPercentageByPurchaseRate"].ToString();
                    if (dr["setGeneralExpiryLast"] != DBNull.Value)
                        MsetGeneralExpiryLast = dr["setGeneralExpiryLast"].ToString();
                    if (dr["setGeneralBatchNumberRequired"] != DBNull.Value)
                        MsetGeneralBatchNumberRequired = dr["setGeneralBatchNumberRequired"].ToString();
                    if (dr["setGeneralExpiryDateRequired"] != DBNull.Value)
                        MsetGeneralExpiryDateReuired = dr["setGeneralExpiryDateRequired"].ToString();
                    if (dr["setAskOperatorOtherThanVoucherSale"] != DBNull.Value)
                        MsetAskOperatorOtherThanVoucherSale = dr["setAskOperatorOtherThanVoucherSale"].ToString();
                    if (dr["setAskOperatorVoucherSale"] != DBNull.Value)
                        MsetAskOperatorVoucherSale = dr["setAskOperatorVoucherSale"].ToString();
                    if (dr["setNumberOfLinesSaleBill"] != DBNull.Value)
                        MsetNumberOfLinesSaleBill = Convert.ToInt32(dr["setNumberOfLinesSaleBill"].ToString());
                    if (dr["setScanBarCode"] != DBNull.Value)
                        MsetScanBarCode = dr["setScanBarCode"].ToString();



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
            return dbset.AddDetails(MsetPurchaseRounding, MsetPurchaseIfCreditStatementPurchase, MsetPurchaseAddVATInPurchaseRate, MsetPurchaseAddVATInSaleRate, MsetPurchaseReadPurchaseOrder,
                    MsetPurchaseIfProductWithOctroi, MsetPurchaseOctroionZeroVAT, MsetPurchaseChangeSaleRate, MsetPurchaseMarginByPurchaseRate, MsetPurchaseAcceptExpriedItems, MsetSaleRoundOff, MsetSaleCreditSale, MsetSaleAskDiscountinCounterSale, MsetSaleAskRoundinginSale, MsetSaleShowProfitInSaleBill, MsetSaleIPDOPD, MsetSaleDiscountWithoutVAT,
                    MsetGeneralProfitPercentageByPurchaseRate, MsetGeneralExpiryLast, MsetGeneralBatchNumberRequired,MsetGeneralExpiryDateReuired, MsetAskOperatorVoucherSale,
                    MsetAskOperatorOtherThanVoucherSale, MsetAskOperatorPurchase, MsetAskOperatorCRDB, MsetAskOperatorOpeningStock, MsetAskOperatorCorrectionRate, MsetAskOperatorJV,
                    MsetAskOperatorCashBankReceipt, MsetAskOperatorCashBankPayment, MsetScanBarCode,MsetPurchaseIncludeCreditPurchaseInStatements,MsetSaleIncludeCreditSaleInStatements,MsetSaleSaveCustomerInMaster,MsetSaleShowOnlyMRPInCounterSale,MsetSaleAllowDistributorSale,MsetSaleF3KeyForPatientSaleEdit,MsetSaleOnlyCashSaleInCounterSale,MsetSaleRoundingTo10Paise);
        }

        public bool UpdateDetails()
        {
            DBSettings dbset = new DBSettings();
            return dbset.UpdateDetails(MsetPurchaseRounding, MsetPurchaseIfCreditStatementPurchase, MsetPurchaseAddVATInPurchaseRate, MsetPurchaseAddVATInSaleRate, MsetPurchaseReadPurchaseOrder,
                    MsetPurchaseIfProductWithOctroi, MsetPurchaseOctroionZeroVAT, MsetPurchaseChangeSaleRate, MsetPurchaseMarginByPurchaseRate, MsetPurchaseAcceptExpriedItems, MsetSaleRoundOff, MsetSaleCreditSale, MsetSaleAskDiscountinCounterSale, MsetSaleAskRoundinginSale, MsetSaleShowProfitInSaleBill, MsetSaleIPDOPD, MsetSaleDiscountWithoutVAT,
                    MsetGeneralProfitPercentageByPurchaseRate, MsetGeneralExpiryLast, MsetGeneralBatchNumberRequired,MsetGeneralExpiryDateReuired, MsetAskOperatorVoucherSale,
                    MsetAskOperatorOtherThanVoucherSale, MsetAskOperatorPurchase, MsetAskOperatorCRDB, MsetAskOperatorOpeningStock, MsetAskOperatorCorrectionRate, MsetAskOperatorJV,
                    MsetAskOperatorCashBankReceipt, MsetAskOperatorCashBankPayment, MsetScanBarCode,MsetPurchaseIncludeCreditPurchaseInStatements,MsetSaleIncludeCreditSaleInStatements,MsetSaleSaveCustomerInMaster,MsetSaleShowOnlyMRPInCounterSale,MsetSaleAllowDistributorSale,MsetSaleF3KeyForPatientSaleEdit,MsetSaleOnlyCashSaleInCounterSale,MsetSaleRoundingTo10Paise);
        }
       
        public bool UpdateDetailsPrint()
        {
            DBSettings dbset = new DBSettings();
            return dbset.UpdateDetailsPrint(MsetPrintSaleBill, MsetPrintCRDBNote, MsetPrintCashBankVoucher, MsetPrintPO, MsetNumberOfLinesSaleBill);
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
            return dbset.UpdateDetailsEmail(MsetEmailID, MsetEmailPassword,MsetEmailType);
        }
        #endregion Public Methods
    }
}
