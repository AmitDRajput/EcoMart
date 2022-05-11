using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;

namespace PharmaSYSRetailPlus.InterfaceLayer
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
            cbDiscountInCounterSale.Focus();
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
            bool retValue = false;

            if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
            {

                if (cbPurchaseRoundOff.Checked)
                    _Settings.MsetPurchaseRounding = "Y";
                else
                    _Settings.MsetPurchaseRounding = "N";
                //1 Purchase
                if (cbCreditPurchase.Checked)
                    _Settings.MsetPurchaseIfCreditStatementPurchase = "Y";
                else
                    _Settings.MsetPurchaseIfCreditStatementPurchase = "N";
                //2 Purchase
                if (cbReadPurchaseOrder.Checked)
                    _Settings.MsetPurchaseReadPurchaseOrder = "Y";
                else
                    _Settings.MsetPurchaseReadPurchaseOrder = "N";
                //3 Purchase
                if (cbProductWithOctroi.Checked)
                    _Settings.MsetPurchaseIfProductWithOctroi = "Y";
                else
                    _Settings.MsetPurchaseIfProductWithOctroi = "N";
                //4  Purchase
                if (cbOctroiOnzeroVAT.Checked)
                    _Settings.MsetPurchaseOctroionZeroVAT = "Y";
                else
                    _Settings.MsetPurchaseOctroionZeroVAT = "N";
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
                //7  Purchase
                if (cbIncludeCreditPurchaseInStatement.Checked)
                    _Settings.MsetPurchaseIncludeCreditPurchaseInStatements = "Y";
                else
                    _Settings.MsetPurchaseIncludeCreditPurchaseInStatements = "N";
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
                //10 sale
                if (cbDiscountInCounterSale.Checked)
                    _Settings.MsetSaleAskDiscountinCounterSale = "Y";
                else
                    _Settings.MsetSaleAskDiscountinCounterSale = "N";
                // 11 sale
                if (cbAskRoundingInSale.Checked)
                    _Settings.MsetSaleAskRoundinginSale = "Y";
                else
                    _Settings.MsetSaleAskRoundinginSale = "N";
                // 12 sale
                if (cbIPDOPD.Checked)
                    _Settings.MsetSaleIPDOPD = "Y";
                else
                    _Settings.MsetSaleIPDOPD = "N";
                //13 sale
                if (cbIncludeCreditSaleInStatement.Checked)
                    _Settings.MsetSaleIncludeCreditSaleInStatements = "Y";
                else
                    _Settings.MsetSaleIncludeCreditSaleInStatements = "N";
                //14 sale
                if (cbProfitPerByPurchaseRate.Checked)
                    _Settings.MsetGeneralProfitPercentageByPurchaseRate = "Y";
                else
                    _Settings.MsetGeneralProfitPercentageByPurchaseRate = "N";
                //15 sale
                if (cbSaveCounterSaleCustomerInMaster.Checked)
                    _Settings.MsetSaleSaveCustomerInMaster = "Y";
                else
                    _Settings.MsetSaleSaveCustomerInMaster = "N";
                if (cbShowOnlyMRPInCounterSale.Checked)
                    _Settings.MsetSaleShowOnlyMRPInCounterSale = "Y";
                else
                    _Settings.MsetSaleShowOnlyMRPInCounterSale = "N";
                if (cbAllowDistributorSale.Checked)
                    _Settings.MsetSaleAllowDistributorSale = "Y";
                else
                    _Settings.MsetSaleAllowDistributorSale = "N";
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
                if (cbOperatorInVoucherSale.Checked)
                    _Settings.MsetAskOperatorVoucherSale = "Y";
                else
                    _Settings.MsetAskOperatorVoucherSale = "N";
                //19 Operator
                if (cbOperatorinotherthanVouchersale.Checked)
                    _Settings.MsetAskOperatorOtherThanVoucherSale = "Y";
                else
                    _Settings.MsetAskOperatorOtherThanVoucherSale = "N";
                //20 Operator
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
                //29 Sale
                if (cbDiscountWithoutVAT.Checked)
                    _Settings.MsetSaleDiscountWithoutVAT = "Y";
                else
                    _Settings.MsetSaleDiscountWithoutVAT = "N";

                //29 Sale
                if (cbF3KeyForPatientSaleEdit.Checked)
                    _Settings.MsetSaleF3KeyForPatientSaleEdit = "Y";
                else
                    _Settings.MsetSaleF3KeyForPatientSaleEdit = "N";

                if (cbOnlyCashSaleInCounterSale.Checked)
                    _Settings.MsetSaleOnlyCashSaleInCounterSale = "Y";
                else
                    _Settings.MsetSaleOnlyCashSaleInCounterSale = "N";
                if (cbRoundingTo10Paise.Checked)
                    _Settings.MsetSaleRoundingTo10Paise = "Y";
                else
                    _Settings.MsetSaleRoundingTo10Paise = "N";
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


        public override bool FillSearchData(string ID, string Vmode)
        {
            return true;
        }


        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData()
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
                if (_Settings.MsetPurchaseRounding == "Y")
                    cbPurchaseRoundOff.Checked = true;
                else
                    cbPurchaseRoundOff.Checked = false;
                // 1 Purchase
                if (_Settings.MsetPurchaseIfCreditStatementPurchase == "Y")
                    cbCreditPurchase.Checked = true;
                else
                    cbCreditPurchase.Checked = false;
                // 2 Purchase
                if (_Settings.MsetPurchaseReadPurchaseOrder == "Y")
                    cbReadPurchaseOrder.Checked = true;
                else
                    cbReadPurchaseOrder.Checked = false;
                // 3 Purchase
                if (_Settings.MsetPurchaseIfProductWithOctroi == "Y")
                    cbProductWithOctroi.Checked = true;
                else
                    cbProductWithOctroi.Checked = false;
                //4 Purchase
                if (_Settings.MsetPurchaseOctroionZeroVAT == "Y")
                    cbOctroiOnzeroVAT.Checked = true;
                else
                    cbOctroiOnzeroVAT.Checked = false;
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
                // 7 Purchase
                if (_Settings.MsetPurchaseIncludeCreditPurchaseInStatements == "Y")
                    cbIncludeCreditPurchaseInStatement.Checked = true;
                else
                    cbIncludeCreditPurchaseInStatement.Checked = false;
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
                if (_Settings.MsetSaleAskDiscountinCounterSale == "Y")
                    cbDiscountInCounterSale.Checked = true;
                else
                    cbDiscountInCounterSale.Checked = false;
                //11 Sale
                if (_Settings.MsetSaleAskRoundinginSale == "Y")
                    cbAskRoundingInSale.Checked = true;
                else
                    cbAskRoundingInSale.Checked = false;
                //12 Sale
                if (_Settings.MsetSaleIPDOPD == "Y")
                    cbIPDOPD.Checked = true;
                else
                    cbIPDOPD.Checked = false;
                //13 Sale
                if (_Settings.MsetSaleIncludeCreditSaleInStatements == "Y")
                    cbIncludeCreditSaleInStatement.Checked = true;
                else
                    cbIncludeCreditSaleInStatement.Checked = false;
                //14 sale
                if (_Settings.MsetGeneralProfitPercentageByPurchaseRate == "Y")
                    cbProfitPerByPurchaseRate.Checked = true;
                else
                    cbProfitPerByPurchaseRate.Checked = false;
                //15 Sale
                if (_Settings.MsetSaleSaveCustomerInMaster == "Y")
                    cbSaveCounterSaleCustomerInMaster.Checked = true;
                else
                    cbSaveCounterSaleCustomerInMaster.Checked = false;
                if (_Settings.MsetSaleShowOnlyMRPInCounterSale == "Y")
                    cbShowOnlyMRPInCounterSale.Checked = true;
                else
                    cbShowOnlyMRPInCounterSale.Checked = false;
                if (_Settings.MsetSaleAllowDistributorSale == "Y")
                    cbAllowDistributorSale.Checked = true;
                else
                    cbAllowDistributorSale.Checked = false;
                //16 General
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
                if (_Settings.MsetAskOperatorVoucherSale == "Y")
                    cbOperatorInVoucherSale.Checked = true;
                else
                    cbOperatorInVoucherSale.Checked = false;
                // 19 Operator
                if (_Settings.MsetAskOperatorOtherThanVoucherSale == "Y")
                    cbOperatorinotherthanVouchersale.Checked = true;
                else
                    cbOperatorinotherthanVouchersale.Checked = false;
                // 20 Operator
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
                if (_Settings.MsetSaleDiscountWithoutVAT == "Y")
                    cbDiscountWithoutVAT.Checked = true;
                else
                    cbDiscountWithoutVAT.Checked = false;
                // 29

                if (_Settings.MsetSaleF3KeyForPatientSaleEdit == "Y")
                    cbF3KeyForPatientSaleEdit.Checked = true;
                else
                    cbF3KeyForPatientSaleEdit.Checked = false;

                if (_Settings.MsetSaleOnlyCashSaleInCounterSale == "Y")
                    cbOnlyCashSaleInCounterSale.Checked = true;
                else
                    cbOnlyCashSaleInCounterSale.Checked = false;
                if (_Settings.MsetSaleRoundingTo10Paise == "Y")
                    cbRoundingTo10Paise.Checked = true;
                else
                    cbRoundingTo10Paise.Checked = false;               
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
    }
}
