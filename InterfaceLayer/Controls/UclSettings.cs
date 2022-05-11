using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;
using EcoMart.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using EcoMart.InterfaceLayer.CommonControls;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSettingsSale : BaseControl
    {
        # region Declaration
        DataTable _SourceData;
        Settings _Settings;
        bool IfGetOverViewData;
        #endregion Declaration

        #region Constructor
        public UclSettingsSale()
        {
            try
            {
                InitializeComponent();
                _SourceData = new DataTable();
                _Settings = new Settings();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion Constructor

        # region IDetail Control
        public override void SetFocus()
        {
            
        }
        public override bool ClearData()
        {
            return true;
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            headerLabel1.Text = "SETTINGS";
            IfGetOverViewData = GetOverviewData();
            tsBtnCancel.Visible = false;
            tsBtnFifth.Visible = false;
            tsBtnPrint.Visible = false;
            tsBtnSavenPrint.Visible = false;
            //   tsBtnUpLoad.Visible = true;
            return retValue;
        }
        public override bool Edit()
        {
            return true;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();

            return retValue;
        }

        public override bool Delete()
        {
            return true;
        }

        public override bool ProcessDelete()
        {
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            headerLabel1.Text = "SETTINGS SALE -> VIEW";
            return retValue;
        }

        public override bool Save()
        {
            try
            {
                bool retValue = true;
                int ProductNameWidth = 30;
                if (nmProductNameWidth.Text.ToString() != string.Empty)
                    ProductNameWidth = Convert.ToInt32(nmProductNameWidth.Text);
                if (ProductNameWidth > 30)
                {
                    MessageBox.Show("Please enter product name width less than 30.");
                    retValue = false;
                }
                else if (ProductNameWidth < 20)
                {
                    MessageBox.Show("Please enter product name width more than 20.");
                    retValue = false;
                }
                if (retValue && (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild))
                {
                    if (cbCreditNoteDoNotShowPurchaseRate.Checked)
                        _Settings.MsetCreditNoteDoNotShowPurchaseRate = "Y";
                    else
                        _Settings.MsetCreditNoteDoNotShowPurchaseRate = "N";
                    if (cbSaleAllowBackDate.Checked)
                        _Settings.MsetSaleAllowBackDate = "Y";
                    else
                        _Settings.MsetSaleAllowBackDate = "N";   
                   
                        
                    if (cbCashBankShowDiscount.Checked)           
                        _Settings.MsetCashBankShowDiscount = "Y";
                    else
                        _Settings.MsetCashBankShowDiscount = "N";                   
                    if (txtMaxDiscount.Text != null && txtMaxDiscount.Text.ToString() != string.Empty)
                        _Settings.MsetSaleMaxDiscount = Convert.ToDouble(txtMaxDiscount.Text.ToString());
                    if (txtOpeningStockGetPercent.Text != null && txtOpeningStockGetPercent.Text.ToString() != string.Empty)
                        _Settings.MsetOpeningStockGetPercent = Convert.ToDouble(txtOpeningStockGetPercent.Text.ToString());
                   
                    if (cbCreditNoteDefaultTransferToAccount.Checked)
                        _Settings.MsetCreditNoteDefaultTransferToAccount = "Y";
                    else
                        _Settings.MsetCreditNoteDefaultTransferToAccount = "N";

                    if (cbCreditNoteReturnRateDisable.Checked)
                        _Settings.MsetCreditNoteReturnRateDisable = "Y";
                    else
                        _Settings.MsetCreditNoteReturnRateDisable = "N";
                    if (txtFixedNarration.Text != null)
                        _Settings.MsetFixedNarration = txtFixedNarration.Text.ToString();
                    

                   



                    if (cbPurchaseCopyPurchaseOrder.Checked)
                        _Settings.MsetPurchaseCopyPurchaseOrder = "Y";
                    else
                        _Settings.MsetPurchaseCopyPurchaseOrder = "N";

                    if (cbPurchaseRoundOff.Checked)
                        _Settings.MsetPurchaseRounding = "Y";
                    else
                        _Settings.MsetPurchaseRounding = "N";

                    if (cbHoldPurchase.Checked)
                        _Settings.MsetPurchaseHold = "Y";
                    else
                        _Settings.MsetPurchaseHold = "N";

                    if (cbIfPTR.Checked)
                        _Settings.MsetPurchaseIfPTR = "Y";
                    else
                        _Settings.MsetPurchaseIfPTR = "N";

                    //2 Purchase
                    if (cbReadPurchaseOrder.Checked)
                        _Settings.MsetPurchaseReadPurchaseOrder = "Y";
                    else
                        _Settings.MsetPurchaseReadPurchaseOrder = "N";
                   
                    
                    //5  Purchase
                    if (cbAcceptExpiredItems.Checked)
                        _Settings.MsetPurchaseAcceptExpriedItems = "Y";
                    else
                        _Settings.MsetPurchaseAcceptExpriedItems = "N";
                    //6  Purchase
                    if (cbAllowChangeInSaleRate.Checked)
                        _Settings.MsetPurchaseChangeSaleRate = "Y";
                    else
                        _Settings.MsetPurchaseChangeSaleRate = "N";
                  
                    // 8  Purchase
                    //
                    if (cbCreditStatementSale.Checked)
                        _Settings.MsetSaleCreditSale = "Y";
                    else
                        _Settings.MsetSaleCreditSale = "N";
                    //9 Sale
                    if (cbShowProfit.Checked)
                        _Settings.MsetSaleShowProfitInSaleBill = "Y";
                    else
                        _Settings.MsetSaleShowProfitInSaleBill = "N";

                    if (cbDelivaryBoy.Checked)
                        _Settings.MsetSaleGetDelivaryBoy = "Y";
                    else
                        _Settings.MsetSaleGetDelivaryBoy = "N";

                    if (cbDoctor.Checked)
                        _Settings.MsetSaleGetDoctor = "Y";
                    else
                        _Settings.MsetSaleGetDoctor = "N";
                    if (cbOrderNumberDate.Checked)
                        _Settings.MsetSaleGetOrderNumberDate = "Y";
                    else
                        _Settings.MsetSaleGetOrderNumberDate = "N";

                    //16 General
                    if (cbExpiryLastDay.Checked)
                        _Settings.MsetGeneralExpiryLast = "Y";
                    else
                        _Settings.MsetGeneralExpiryLast = "N";
                    //17 General 
                    if (cbExpiryDateRequired.Checked)
                        _Settings.MsetGeneralExpiryDateReuired = "Y";
                    else
                        _Settings.MsetGeneralExpiryDateReuired = "N";
                    // 18 General
                  
                    // 18 General
                    if (cbGeneralAlphabetical.Checked)
                        _Settings.MsetGeneralAlphabetical = "Y";
                    else
                        _Settings.MsetGeneralAlphabetical = "N";
                    //19 Operator
                    
                    
                    if (cbOperatorinPurchase.Checked)
                        _Settings.MsetAskOperatorPurchase = "Y";
                    else
                        _Settings.MsetAskOperatorPurchase = "N";
                    //21 Operator
                    if (cbOperatorinDebitCreditNote.Checked)
                        _Settings.MsetAskOperatorCRDB = "Y";
                    else
                        _Settings.MsetAskOperatorCRDB = "N";
                    //22 Operator
                    if (cbOperatorInOpeningStock.Checked)
                        _Settings.MsetAskOperatorOpeningStock = "Y";
                    else
                        _Settings.MsetAskOperatorOpeningStock = "N";
                    //23 Operator
                    if (cbOperatorinCorrectioninRate.Checked)
                        _Settings.MsetAskOperatorCorrectionRate = "Y";
                    else
                        _Settings.MsetAskOperatorCorrectionRate = "N";
                    //24 Operator
                    if (cbOperatorinJV.Checked)
                        _Settings.MsetAskOperatorJV = "Y";
                    else
                        _Settings.MsetAskOperatorJV = "N";
                    //25 Operator
                    if (cbOperatorinCashBankReceipt.Checked)
                        _Settings.MsetAskOperatorCashBankReceipt = "Y";
                    else
                        _Settings.MsetAskOperatorCashBankReceipt = "N";
                    //26 Operator
                    if (cbOperatorinCashBankPayment.Checked)
                        _Settings.MsetAskOperatorCashBankPayment = "Y";
                    else
                        _Settings.MsetAskOperatorCashBankPayment = "N";
                    //27 Operator
                    if (cbScanBarCode.Checked)
                        _Settings.MsetScanBarCode = "Y";
                    else
                        _Settings.MsetScanBarCode = "N";
                    //28 Sale
                    if (cbSaleRoundOff.Checked)
                        _Settings.MsetSaleRoundOff = "Y";
                    else
                        _Settings.MsetSaleRoundOff = "N";                   

                    if (chkpatientsale.Checked)  
                        _Settings.SmsSetPatientSale = "Y";
                    else
                        _Settings.SmsSetPatientSale = "N";

                    if (chkDebtorSale.Checked)   
                        _Settings.SmsSetDebtorSale = "Y";
                    else
                        _Settings.SmsSetDebtorSale = "N";

                    if (chkCreditCardSale.Checked)   
                        _Settings.SmsSetCreditCardSale  = "Y";
                    else
                        _Settings.SmsSetCreditCardSale = "N";

                    if (chkInstitutionalSale.Checked)   
                        _Settings.SmsSetInstitutionalSale  = "Y";
                    else
                        _Settings.SmsSetInstitutionalSale = "N";

                    if (chkBankPayment.Checked)   
                        _Settings.SmsSetBankPaymentSale  = "Y";
                    else
                        _Settings.SmsSetBankPaymentSale = "N";

                    if (chkBankReceiptSale.Checked)  
                        _Settings.SmsSetBankReceiptSale  = "Y";
                    else
                        _Settings.SmsSetBankReceiptSale = "N";

                    if (chkCashPaymentsale.Checked)   
                        _Settings.SmsSetCashPaymentSale  = "Y";
                    else
                        _Settings.SmsSetCashPaymentSale = "N";

                    if (chkCashReceiptSale.Checked)  
                        _Settings.SmsSetCashReceiptSale  = "Y";
                    else
                        _Settings.SmsSetCashReceiptSale = "N";

                    //   if (string.IsNullOrEmpty(Convert.ToString(_Settings.MsetProductNameWidthInSaleInvoice)) == false)
                    _Settings.MsetProductNameWidthInSaleInvoice = ProductNameWidth;

                    if (IfGetOverViewData)
                        retValue = _Settings.UpdateDetails();
                    else
                        retValue = _Settings.AddDetails();


                    MessageBox.Show("Information has been saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    General.CurrentSetting.FillSettings();

                    retValue = true;
                }
                return retValue;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
                return false;
            }
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            return true;
        }

        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData(Control closedControl)
        {

        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Escape)
            {
                retValue = Exit();
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }
        public bool GetOverviewData()
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            retValue = _Settings.GetOverviewData(General.ShopDetail.ShopVoucherSeries);
            if (retValue)
            {
                try
                {
                    if (_Settings.MsetCreditNoteDoNotShowPurchaseRate == "Y")
                        cbCreditNoteDoNotShowPurchaseRate.Checked = true;
                    else
                        cbCreditNoteDoNotShowPurchaseRate.Checked = false;
                    if (_Settings.MsetSaleAllowBackDate == "Y")
                        cbSaleAllowBackDate.Checked = true;
                    else
                        cbSaleAllowBackDate.Checked = false;
                    // ss 5/11

                    if (_Settings.MsetCashBankShowDiscount == "Y")
                        cbCashBankShowDiscount.Checked = true;
                    else
                        cbCashBankShowDiscount.Checked = false;


                    txtMaxDiscount.Text = _Settings.MsetSaleMaxDiscount.ToString("#0.00");
                    txtOpeningStockGetPercent.Text = _Settings.MsetOpeningStockGetPercent.ToString("#0.00");

                    if (_Settings.MsetCreditNoteDefaultTransferToAccount == "Y")
                        cbCreditNoteDefaultTransferToAccount.Checked = true;
                    else
                        cbCreditNoteDefaultTransferToAccount.Checked = false;
                    if (_Settings.MsetCreditNoteReturnRateDisable == "Y")
                        cbCreditNoteReturnRateDisable.Checked = true;
                    else
                        cbCreditNoteReturnRateDisable.Checked = false;

                    if (_Settings.MsetFixedNarration != null)
                        txtFixedNarration.Text = _Settings.MsetFixedNarration.ToString();
                    if (_Settings.MsetPurchaseCopyPurchaseOrder == "Y")
                        cbPurchaseCopyPurchaseOrder.Checked = true;
                    else
                        cbPurchaseCopyPurchaseOrder.Checked = false;
                    if (_Settings.MsetPurchaseRounding == "Y")
                        cbPurchaseRoundOff.Checked = true;
                    else
                        cbPurchaseRoundOff.Checked = false;
                    if (_Settings.MsetPurchaseHold == "Y")
                        cbHoldPurchase.Checked = true;
                    else
                        cbHoldPurchase.Checked = false;
                    // 1 Purchase
                    if (_Settings.MsetPurchaseIfPTR == "Y")
                        cbIfPTR.Checked = true;
                    else
                        cbIfPTR.Checked = false;
                    // 1 Purchase

                    // 2 Purchase
                    if (_Settings.MsetPurchaseReadPurchaseOrder == "Y")
                        cbReadPurchaseOrder.Checked = true;
                    else
                        cbReadPurchaseOrder.Checked = false;

                    // 5 Purchase
                    if (_Settings.MsetPurchaseAcceptExpriedItems == "Y")
                        cbAcceptExpiredItems.Checked = true;
                    else
                        cbAcceptExpiredItems.Checked = false;
                    // 6 Purchase
                    if (_Settings.MsetPurchaseChangeSaleRate == "Y")
                        cbAllowChangeInSaleRate.Checked = true;
                    else
                        cbAllowChangeInSaleRate.Checked = false;
                    if (_Settings.MsetSaleGetDelivaryBoy == "Y")
                        cbDelivaryBoy.Checked = true;
                    else
                        cbDelivaryBoy.Checked = false;
                    if (_Settings.MsetSaleGetDoctor == "Y")
                        cbDoctor.Checked = true;
                    else
                        cbDoctor.Checked = false;
                    if (_Settings.MsetSaleGetOrderNumberDate == "Y")
                        cbOrderNumberDate.Checked = true;
                    else
                        cbOrderNumberDate.Checked = false;

                    //8 Purchase              
                    //               
                    if (_Settings.MsetSaleCreditSale == "Y")
                        cbCreditStatementSale.Checked = true;
                    else
                        cbCreditStatementSale.Checked = false;
                    // 9 sale
                    if (_Settings.MsetSaleShowProfitInSaleBill == "Y")
                        cbShowProfit.Checked = true;
                    else
                        cbShowProfit.Checked = false;
                    //10 Sale
                    if (_Settings.MsetGeneralExpiryLast == "Y")
                        cbExpiryLastDay.Checked = true;
                    else
                        cbExpiryLastDay.Checked = false;
                    //17 General 
                    if (_Settings.MsetGeneralExpiryDateReuired == "Y")
                        cbExpiryDateRequired.Checked = true;
                    else
                        cbExpiryDateRequired.Checked = false;
                    //18 General

                    //18 General
                    if (_Settings.MsetGeneralAlphabetical == "Y")
                        cbGeneralAlphabetical.Checked = true;
                    else
                        cbGeneralAlphabetical.Checked = false;
                    // 19 Operator

                    if (_Settings.MsetAskOperatorPurchase == "Y")
                        cbOperatorinPurchase.Checked = true;
                    else
                        cbOperatorinPurchase.Checked = false;
                    // 21 Operator
                    if (_Settings.MsetAskOperatorCRDB == "Y")
                        cbOperatorinDebitCreditNote.Checked = true;
                    else
                        cbOperatorinDebitCreditNote.Checked = false;
                    //22 Operator
                    if (_Settings.MsetAskOperatorOpeningStock == "Y")
                        cbOperatorInOpeningStock.Checked = true;
                    else
                        cbOperatorInOpeningStock.Checked = false;
                    //23 Operator
                    if (_Settings.MsetAskOperatorCorrectionRate == "Y")
                        cbOperatorinCorrectioninRate.Checked = true;
                    else
                        cbOperatorinCorrectioninRate.Checked = false;
                    //24 Operator
                    if (_Settings.MsetAskOperatorJV == "Y")
                        cbOperatorinJV.Checked = true;
                    else
                        cbOperatorinJV.Checked = false;
                    //25 Operator
                    if (_Settings.MsetAskOperatorCashBankReceipt == "Y")
                        cbOperatorinCashBankReceipt.Checked = true;
                    else
                        cbOperatorinCashBankReceipt.Checked = false;
                    //26 Operator
                    if (_Settings.MsetAskOperatorCashBankPayment == "Y")
                        cbOperatorinCashBankPayment.Checked = true;
                    else
                        cbOperatorinCashBankPayment.Checked = false;
                    //27 Operator
                    if (_Settings.MsetScanBarCode == "Y")
                        cbScanBarCode.Checked = true;
                    else
                        cbScanBarCode.Checked = false;
                    //28 sale
                    if (_Settings.MsetSaleRoundOff == "Y")
                        cbSaleRoundOff.Checked = true;
                    else
                        cbSaleRoundOff.Checked = false;
                    // 29 Sale
                    if (_Settings.SmsSetPatientSale == "Y")
                        chkpatientsale.Checked = true;
                    else
                        chkpatientsale.Checked = false;

                    if (_Settings.SmsSetPatientSale == "Y")
                        chkpatientsale.Checked = true;
                    else
                        chkpatientsale.Checked = false;

                    if (_Settings.SmsSetDebtorSale == "Y")
                        chkDebtorSale.Checked = true;
                    else
                        chkDebtorSale.Checked = false;

                    if (_Settings.SmsSetCreditCardSale == "Y")
                        chkCreditCardSale.Checked = true;
                    else
                        chkCreditCardSale.Checked = false;

                    if (_Settings.SmsSetInstitutionalSale == "Y")
                        chkInstitutionalSale.Checked = true;
                    else
                        chkInstitutionalSale.Checked = false;

                    if (_Settings.SmsSetBankPaymentSale == "Y")
                        chkBankPayment.Checked = true;
                    else
                        chkBankPayment.Checked = false;

                    if (_Settings.SmsSetBankReceiptSale == "Y")
                        chkBankReceiptSale.Checked = true;
                    else
                        chkBankReceiptSale.Checked = false;

                    if (_Settings.SmsSetCashPaymentSale == "Y")
                        chkCashPaymentsale.Checked = true;
                    else
                        chkCashPaymentsale.Checked = false;

                    if (_Settings.SmsSetCashReceiptSale == "Y")
                        chkCashReceiptSale.Checked = true;
                    else
                        chkCashReceiptSale.Checked = false;

                    nmProductNameWidth.Text = Convert.ToString(_Settings.MsetProductNameWidthInSaleInvoice);
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                    return false;
                }
            }
            return retValue;
        }
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        #endregion IDetail Members

        private void txtMaxDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtFixedNarration.Focus();
        }

      
    }
}
