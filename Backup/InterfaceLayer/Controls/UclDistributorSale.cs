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
using System.IO;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;
namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDistributorSale : BaseControl
    {
        #region Declaration
        private SSSale _SSSale;
        private DataTable _BindingSource;
        private DataRow _SchemeData;
        private DataTable _PaymentDetailsBindingSource;
        private string IfEditPreviousRow = "N";
        private string _LastStockID;
        private string deletedproductname = "";
        private BaseControl ViewControl;       
        private Form frmView;
        private int schemeQuantity1 = 0;
        private int schemeQuantity2 = 0;
        private int schemeQuantity3 = 0;
        private int schemeFree1 = 0;
        private int schemeFree2 = 0;
        private int schemeFree3 = 0;
        #endregion

        #region contructor
        public UclDistributorSale()
        {
            InitializeComponent();
            _SSSale = new SSSale();            
            SearchControl = new UclDistributorSaleSearch();
            _LastStockID = string.Empty;
        }
        # endregion

        # region IDetail Control
        public override void SetFocus()
        {
            if (_Mode == OperationMode.Add)
                cbTransactionType.Focus();
            else
                txtVouchernumber.Focus();
        }
        public override bool ClearData()
        {
            _SSSale.Initialise();
            ClearControls();
            return true;
        }
        public override bool Add()
        {

            bool retValue = base.Add();
            try
            {
                ClearData();
                txtVouType.Text = "";
                InitializeScreen();
                headerLabel1.Text = "DISTRIBUTOR SALE -> NEW";

                cbEditRate.Visible = true;

                InitializeMainSubViewControl("");

                mpMSVC.Enabled = true;
                FillTransactionType();
                FillPartyCombo();
                mpMSVC.BringToFront();
                btnPaymentHistory.Visible = false;
                btnSummary.Enabled = false;
                pnlProductDetail.Enabled = true;
                dgvBatchGrid.Visible = false;
                pnlSummary.Visible = false;
                cbRound.Checked = true;
                mcbCreditor.Enabled = true;
                cbTransactionType.Enabled = true;
                txtNarration.Enabled = true;
                txtVouchernumber.Enabled = false;
               // if (General.CurrentSetting.MsetPurchaseChangeSaleRate == "Y")
                //    txtSaleRate.Enabled = true;
              //  else
              //      txtSaleRate.Enabled = false;

                FixVoucherTypeBycbTransactionType();
                cbTransactionType.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return retValue;
        }


        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            InitializeScreen();
            tsBtnSave.Enabled = false;
            headerLabel1.Text = "PURCHASE -> EDIT";
            InitializeMainSubViewControl("");
            FillPartyCombo();
            btnPaymentHistory.Visible = true;
            mcbCreditor.Enabled = false;
            pnlProductDetail.Enabled = true;
            txtNarration.Enabled = false;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            cbTransactionType.Enabled = true;
            FixVoucherTypeBycbTransactionType();
            //if (General.CurrentSetting.MsetPurchaseChangeSaleRate == "Y")
            //    txtSaleRate.Enabled = true;
            //else
            //    txtSaleRate.Enabled = false;

            return retValue;
        }

        private void FixVoucherTypeBycbTransactionType()
        {
            _SSSale.CrdbVouType = "";
            if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                _SSSale.CrdbVouType = FixAccounts.VoucherTypeForDistributorSaleCredit;
            else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                _SSSale.CrdbVouType = FixAccounts.VoucherTypeForDistributorSaleCash;
            else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                _SSSale.CrdbVouType = FixAccounts.VoucherTypeForDistributorSaleCreditStatement;
            txtVouType.Text = _SSSale.CrdbVouType;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            pnlProductDetail.Visible = false;
            pnlSummary.Visible = false;
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {
                mpMSVC.Rows.Add();
            }
            cbTransactionType.Enabled = true;
            return retValue;
        }

        public override bool Exit()
        {
            bool retValue = base.Exit();
            System.IO.File.Delete(General.GetPurchaseTempFile());
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            InitializeScreen();
            headerLabel1.Text = "PURCHASE -> DELETE";
            ClearData();
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.Focus();
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_SSSale.CrdbAmountClear != 0)
                MessageBox.Show("Payment Done", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

                if (MessageBox.Show("Are you sure you want to delete Purchase information?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BindTempGrid();
                    bool canbedeleted = CheckStockForDelete();
                    if (canbedeleted)
                    {
                        LockTable.LockTablesForPurchase();
                        retValue = _SSSale.DeleteDetails();
                        retValue = _SSSale.DeletePreviousRecords();
                        retValue = _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
                        ReducePreviousStock();
                        // clearPreviousdebitcreditnotes();
                        LockTable.UnLockTables();
                        UpdateClosingStockinCache();
                        retValue = true;
                        MessageBox.Show("Successfully Deleted", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SSSale.AddDeletedProductDetailsSS();
                        AddPreviousRowsInDeleteDetail();
                    }
                    else
                        MessageBox.Show("Can not Update " + deletedproductname, General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            ClearData();
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            InitializeScreen();
            headerLabel1.Text = "PURCHASE -> VIEW";
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.Focus();
            tsBtnExit.Select();
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            string TransactionText = "";
            if (cbTransactionType.Text != null && cbTransactionType.Text.ToString() != string.Empty)
                TransactionText = cbTransactionType.Text.ToString();
            if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
            {
                if (txtBillAmount.Text != null && Convert.ToDouble(txtBillAmount.Text.ToString()) > 0)
                {
                    TransactionText = cbTransactionType.Text;
                    if (TransactionText != string.Empty)
                    {
                        FixVoucherType();
                        IfAdd();

                    }
                    if (_Mode == OperationMode.Edit)
                        _SSSale.IFEdit = "Y";
                    _SSSale.SaleSubType = "W";
                    _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    _SSSale.Validate();


                    if (_SSSale.IsValid)
                    {
                        try
                        {
                            LockTable.LockTablesForPurchase();
                            if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                            {
                                General.BeginTransaction();

                                _SSSale.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(_SSSale.CrdbVouType, General.ShopDetail.ShopVoucherSeries);
                                _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                                txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString();
                                _SSSale.CreatedBy = General.CurrentUser.Id;
                                _SSSale.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _SSSale.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                                retValue = _SSSale.AddDetails();
                                _SavedID = _SSSale.Id;
                                if (retValue)
                                    retValue = SaveParticularsProductwise();

                                //if (retValue)
                                //    SaveAndUpdateDebitCreditNote();
                                if (retValue)
                                {
                                    _SSSale.AddVoucherIntblTrnac();
                                }
                                if (retValue)
                                    General.CommitTransaction();
                                else
                                    General.RollbackTransaction();
                                LockTable.UnLockTables();
                                if (retValue)
                                {
                                    UpdateClosingStockinCache();
                                    System.IO.File.Delete(General.GetPurchaseTempFile());
                                    string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                                    PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                    retValue = true;
                                }
                                else
                                {
                                    PSDialogResult result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                    retValue = false;
                                }
                            }
                            else if (_Mode == OperationMode.Fifth)
                            {
                                //DataTable stocktbl = new DataTable();
                                //_SSSale.CrdbVouNo = int.Parse(txtVouchernumber.Text);
                                //if (cbTransactionType.Text == cbNewTransactionType.Text)
                                //    _Purchase.IfTypeChange = "N";
                                //else
                                //    _Purchase.IfTypeChange = "Y";
                                //if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCash)
                                //    _Purchase.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                                //else if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                                //    _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                                //else
                                //    _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;

                                //General.BeginTransaction();

                                //if (_Purchase.IfTypeChange == "Y")
                                //{
                                //    if (_Purchase.OldVoucherType != _Purchase.VoucherType)
                                //    {
                                //        _Purchase.VoucherNumber = _Purchase.GetAndUpdatePurchaseNumber(_Purchase.VoucherType);
                                //        txtVouchernumber.Text = _Purchase.VoucherNumber.ToString();
                                //        txtVouType.Text = _Purchase.VoucherType;
                                //        retValue = _Purchase.UpdateDetailsForTypeChange();
                                //        if (retValue)
                                //        {
                                //            retValue = _Purchase.DeleteAccountDetails();
                                //        }
                                //        if (retValue)
                                //        {
                                //            retValue = _Purchase.AddAccountDetails();
                                //        }
                                //        if (retValue)
                                //            _Purchase.UpdateCreditDebitNoteforTypeChange(_Purchase.CreditDebitNoteID, _Purchase.Amount, _Purchase.VoucherType, _Purchase.VoucherNumber, _Purchase.VoucherDate, _Purchase.PurchaseBillNumber, _Purchase.Id);

                                //        if (retValue)
                                //            General.CommitTransaction();
                                //        else
                                //            General.RollbackTransaction();
                                //        LockTable.UnLockTables();
                                //        if (retValue)
                                //        {
                                //            UpdateClosingStockinCache(); string msgLine2 = _Purchase.VoucherType + "  " + _Purchase.VoucherNumber.ToString("#0");
                                //            PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                //            //     MessageBox.Show("Information has been saved successfully.'", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //            retValue = true;
                                //        }
                                //        else
                                //        {
                                //            PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                //            retValue = false;
                                //        }
                                //    }
                                //    ClearData();
                                //}
                            }
                            else if (_Mode == OperationMode.Edit)
                            {
                                DataTable stocktbl = new DataTable();
                                _SSSale.CrdbVouNo = int.Parse(txtVouchernumber.Text);
                                General.BeginTransaction();
                                _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                                _SSSale.ModifiedBy = General.CurrentUser.Id;
                                _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");

                                retValue = CheckStockForDeletedRows();
                                if (retValue)
                                {
                                    retValue = DeletePreviousRows();
                                    if (retValue)
                                        retValue = SaveParticularsProductwise();
                                    if (retValue)
                                        retValue = ReducePreviousStock();

                                    if (retValue)
                                        retValue = _SSSale.UpdateDetails();
                                    _SavedID = _SSSale.Id;

                                    if (retValue)
                                    {
                                        //retValue = DeletePreviousRows();
                                        //if (retValue)
                                        //    retValue = SaveParticularsProductwise();

                                        // clearPreviousdebitcreditnotes();
                                        //if (retValue && (_SSSale.CrNoteAmount > 0 || _SSSale.DbNoteAmount > 0))
                                        //    retValue = SaveAndUpdateDebitCreditNote();

                                        if (retValue)
                                        {

                                            retValue = _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
                                            _SSSale.CreatedBy = _SSSale.ModifiedBy;
                                            _SSSale.CreatedDate = _SSSale.ModifiedDate;
                                            _SSSale.CreatedTime = _SSSale.ModifiedTime;
                                            retValue = _SSSale.AddVoucherIntblTrnac();

                                        }
                                        if (retValue)
                                            General.CommitTransaction();
                                        else
                                            General.RollbackTransaction();
                                        LockTable.UnLockTables();
                                        if (retValue)
                                        {
                                            UpdateClosingStockinCache();
                                            try
                                            {
                                                _SSSale.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                                _SSSale.AddDetailsInChangedMaster();
                                                AddPreviousRowsInChangedDetail();
                                                MessageBox.Show("Information has been Updated successfully.'", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            catch (Exception Ex)
                                            {
                                                Log.WriteException(Ex);
                                            }
                                            retValue = true;
                                        }
                                        else
                                        {
                                            PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                            retValue = false;
                                        }
                                    }
                                    else
                                    {
                                        string mm = "";
                                        //  string mm = _SSSale.Name + " " + _SSSale.ProdLoosePack.ToString() + _SSSale.Pack + " - " + _SSSale.Batchno + " - " + _SSSale.MRP.ToString("0.00");
                                        MessageBox.Show(mm + " Can not Update Stock < 0", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        General.RollbackTransaction();
                                        LockTable.UnLockTables();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Can not Update " + deletedproductname, General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    General.RollbackTransaction();
                                    deletedproductname = "";
                                    LockTable.UnLockTables();
                                }

                            }





                            //else
                            //{
                            //    StringBuilder _errorMessage = new System.Text.StringBuilder();
                            //    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                            //    foreach (string _message in _Purchase.ValidationMessages)
                            //    {
                            //        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                            //    }
                            //    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //}
                        }

                        catch (Exception ex)
                        {
                            Log.WriteError(ex.ToString());
                        }
                    }
                    else
                    {
                        StringBuilder _errorMessage = new System.Text.StringBuilder();
                        _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                        foreach (string _message in _SSSale.ValidationMessages)
                        {
                            _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                        }
                        MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            LockTable.UnLockTables();

            return retValue;
        }

        public void FixVoucherType()
        {
            //_SSSale.EntryDate = Convert.ToString(DateTime.Now);

            FixVoucherTypeBycbTransactionType();

            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForDistributorSaleCash)
                _SSSale.AccountID = FixAccounts.AccountCash;
            else if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForDistributorSaleCreditStatement)
                _SSSale.AccountID = FixAccounts.AccountCreditSale;
            else if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForDistributorSaleCredit)
                _SSSale.AccountID = FixAccounts.AccountCashCreditSale;
            txtVouType.Text = _SSSale.CrdbVouType;
            if (mcbCreditor.SelectedID != null)
                _SSSale.AccountID = this.mcbCreditor.SelectedID;
            // _SSSale.PurchaseBillNumber = txtBillNumber.Text;
        }


        public void IfAdd()
        {
            _SSSale.ItemTotalDiscount = Convert.ToDouble(txtItemDiscountS.Text.ToString());
            //if (txtSplDiscountS.Text.ToString() != "")
            //    _SSSale .AmountSpecialDiscountS = Convert.ToDouble(txtSplDiscountS.Text.ToString());
            //_SSSale.AmountSchemeDiscountS = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
            _SSSale.CrdbDiscPer = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
            _SSSale.CrdbDiscAmt = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
            if (txtAddOnS.Text.ToString() != "")
                _SSSale.CrdbAddOn = Convert.ToDouble(txtAddOnS.Text.ToString());
            if (txtCRAmountS.Text.ToString() != "")
                _SSSale.CrNoteAmount = Convert.ToDouble(txtCRAmountS.Text.ToString());
            _SSSale.DbNoteAmount = Convert.ToDouble(txtDBAmountS.Text.ToString());
            if (txtOCTPerS.Text != "")
                _SSSale.CrdbOctoriPer = Convert.ToDouble(txtOCTPerS.Text.ToString());
            if (txtOCTAmountS.Text != "")
                _SSSale.CrdbOctroiAmount = Convert.ToDouble(txtOCTAmountS.Text.ToString());
            _SSSale.CrdbNarration = txtNarration.Text.ToString();
            _SSSale.CrdbRoundAmount = Convert.ToDouble(txtRoundUPS.Text.ToString());
            _SSSale.CrdbVat5 = Convert.ToDouble(txtVAT5AmountS.Text.ToString());
            _SSSale.CrdbVat12point5 = Convert.ToDouble(txtVAT12Point5AmountS.Text.ToString());
            if (txtPurchaseAmountVATZeroS.Text != null && txtPurchaseAmountVATZeroS.Text != "")
                _SSSale.CrdbAmtForZeroVAT = Convert.ToDouble(txtPurchaseAmountVATZeroS.Text.ToString());
            //if (txtpuramount0.Text.ToString() != "")
            //    _Purchase.PurchaseAmount0PercentVATS = Convert.ToDouble(txtPurchaseAmountVATZeroS.Text.ToString());
            if (txtPurchaseAmountVAT5S.Text.ToString() != "")
                _SSSale.CrdbAmountVat5 = Convert.ToDouble(txtPurchaseAmountVAT5S.Text.ToString());
            if (txtPurchaseAmountVAT12point5S.Text.ToString() != "")
                _SSSale.CrdbAmountVat12point5 = Convert.ToDouble(txtPurchaseAmountVAT12point5S.Text.ToString());

            if (_Mode == OperationMode.Add)
            {
                //_Purchase.CBVouType = "";
                //_Purchase.IfCashPaid = "N";
                //if (txtChequeNumber.Text != null && txtChequeNumber.Text != "")
                //{
                //    _Purchase.ChequeNumber = txtChequeNumber.Text.ToString();

                //    if (mcbBank.SelectedID != null)
                //    {
                //        _Purchase.BankID = mcbBank.SelectedID;
                //    }
                //    _Purchase.ChequeDate = datePickerChqDate.Value.Date.ToString("yyyyMMdd");
                //}


                //if (_Purchase.IfCashPaid == "Y" || (_Purchase.ChequeNumber != "" && _Purchase.BankID != ""))
                //{
                //    _Purchase.AmountClearS = _Purchase.AmountNetS;
                //}

            }

        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _SSSale.Id = ID;
                    if (Vmode == "C")
                        _SSSale.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _SSSale.ReadDetailsByIDForDeleted();
                    else
                        _SSSale.ReadDetailsByID();
                    BindTempGrid();
                    BindPaymentDetails();
                    InitializeMainSubViewControl(Vmode);
                    if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
                    {
                        int currentrow = mpMSVC.Rows.Add();
                        mpMSVC.SetFocus(currentrow, 1);
                    }
                    _SSSale.OldVoucherType = _SSSale.CrdbVouType;
                    _SSSale.OldVoucherNumber = _SSSale.CrdbVouNo;
                    if (_SSSale.StatementNumber.ToString() != "" && _SSSale.StatementNumber > 0)
                        lblFooterMessage.Text = "Statement Number : " + _SSSale.StatementNumber.ToString();
                    else
                        lblFooterMessage.Text = "";
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForDistributorSaleCash)
                    {

                        cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                    }
                    else if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditStatementPurchase)
                    {

                        cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCreditStatement);
                    }
                    else
                    {

                        cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                    }

                    txtVouType.Text = _SSSale.CrdbVouType.ToString();
                    txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString();
                    //  txtBillNumber.Text = _SSSale.PurchaseBillNumber;
                    txtNarration.Text = _SSSale.CrdbNarration;
                    //txtSplDiscPerS.Text = _SSSale.SpecialDiscountPercentS.ToString("#0.00");
                    txtCashDiscountPerS.Text = _SSSale.CrdbDiscPer.ToString("#0.00");
                    txtCashDiscountAmountS.Text = _SSSale.CrdbDiscAmt.ToString("#0.00");
                    txtPreCashDiscountAmountS.Text = _SSSale.CrdbDiscAmt.ToString("#0.00");
                    //txtSplDiscountS.Text = _SSSale.AmountSpecialDiscountS.ToString("#0.00");
                    txtItemDiscountS.Text = _SSSale.ItemTotalDiscount.ToString("#0.00");
                    //txtSchemeDiscountS.Text = _SSSale. .AmountSchemeDiscountS.ToString("#0.00");
                    txtAddOnS.Text = _SSSale.CrdbAddOn.ToString("#0.00");
                    txtCRAmountS.Text = _SSSale.CrNoteAmount.ToString("#0.00");
                    txtDBAmountS.Text = _SSSale.DbNoteAmount.ToString("#0.00");
                    txtOCTPerS.Text = _SSSale.CrdbOctoriPer.ToString("#0.00");
                    txtOCTAmountS.Text = _SSSale.CrdbOctroiAmount.ToString("#0.00");

                    txtVAT5AmountS.Text = _SSSale.CrdbVat5.ToString("#0.00");
                    txtVAT12Point5AmountS.Text = _SSSale.CrdbVat12point5.ToString("#0.00");
                    txtViewVat5per.Text = _SSSale.CrdbVat5.ToString("#0.00");
                    txtViewVat12point5per.Text = _SSSale.CrdbVat12point5.ToString("#0.00");
                    txtPurchaseAmountVATZeroS.Text = _SSSale.CrdbAmtForZeroVAT.ToString("#0.00");
                    txtPurchaseAmountVAT5S.Text = _SSSale.CrdbAmountVat5.ToString("#0.00");
                    txtPurchaseAmountVAT12point5S.Text = _SSSale.CrdbAmountVat12point5.ToString("#0.00");
                    DateTime mydate = new DateTime(Convert.ToInt32(_SSSale.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    txtRoundUPS.Text = _SSSale.CrdbRoundAmount.ToString("#0.00");
                    txtGridAmountTot.Text = _SSSale.CrdbAmount.ToString("#0.00");
                    txtBillAmountS.Text = _SSSale.CrdbAmountNet.ToString("#0.00");
                    txtBillAmount.ReadOnly = false;
                    txtBillAmount.Enabled = true;
                    txtBillAmount.Text = _SSSale.CrdbAmountNet.ToString("#0.00");
                    txtNetAmountS.Text = _SSSale.CrdbAmountNet.ToString("#0.00");
                    txtBillAmount.ReadOnly = true;
                    txtBillAmount.Enabled = false;

                    FillPartyCombo();
                    btnSummary.Enabled = true;
                    dgvBatchGrid.Visible = false;
                    pnlSummary.Visible = false;

                    mcbCreditor.SelectedID = _SSSale.AccountID;
                    if (_Mode == OperationMode.Fifth && _SSSale.StatementNumber == 0)
                    {
                        btnTypeChange.Visible = true;
                        cbNewTransactionType.Visible = true;
                        cbNewTransactionType.Enabled = false;
                        btnTypeChange.Enabled = true;
                        btnTypeChange.Focus();
                    }
                    if (_SSSale.StatementNumber > 0 || _Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                    {
                        pnlPaymentDetails.Enabled = false;
                        mpMSVC.IsAllowDelete = false;
                        mcbCreditor.Enabled = false;
                    }
                    else
                    {
                        mpMSVC.IsAllowDelete = true;
                        pnlPaymentDetails.Enabled = true;
                        pnlBillDetails.Enabled = true;
                        mcbCreditor.Enabled = true;
                        //if (_Mode !=OperationMode.Add)
                        //    mcbCreditor.Focus();

                    }

                    cbTransactionType.Enabled = false;

                }
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        # endregion Idetail Control

        #region IDetail Members
        public override void ReFillData()
        {
            try
            {
                General.CurrentSetting.FillSettings();
                mpMSVC.DataSource = General.ProductList;
                mpMSVC.BindGridSub();
                string oldtrans = cbTransactionType.Text;
                Int32 oldtransindex = cbTransactionType.SelectedIndex;
                FillTransactionType();
                cbTransactionType.Text = oldtrans;
                cbTransactionType.SelectedIndex = oldtransindex;

                string creditorID = mcbCreditor.SelectedID;
                FillPartyCombo();
                mcbCreditor.SelectedID = creditorID;
                mcbCreditor.Focus();
                FillCreditDebitNote();
                if (General.CurrentSetting.MsetScanBarCode == "Y")
                    btnPrintBarCode.Visible = true;
                else
                    btnPrintBarCode.Visible = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool RefreshProductList()
        {
            mpMSVC.DataSource = General.ProductList;
            mpMSVC.BindGridSub();
            return true;
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            try
            {

                if (keyPressed == Keys.B && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        pnlProductDetail.Focus();
                        txtBatch.Focus();
                        this.ActiveControl = txtBatch;
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        btnCancel.Focus();
                        btnCancel.BackColor = General.ControlFocusColor;
                        retValue = true;
                    }
                    else
                    {
                        this.mcbCreditor.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    datePickerBillDate.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.E && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtExpiry.Focus();
                        retValue = true;
                    }

                }

                if (keyPressed == Keys.H && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtSchemeAmount.Focus();
                        retValue = true;
                    }
                    else
                    {
                        txtCashDiscountPerS.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.I && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtDiscountPer.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.L && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtSaleRate.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.M && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtMRP.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == false)
                    {
                        txtNarration.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {
                    if (pnlDebitCreditNote.Visible == true)
                    {
                        btnCRDBOKClick();
                        retValue = true;
                    }
                    else
                    {
                        btnOK.Focus();
                        retValue = true;
                    }

                }

                if (keyPressed == Keys.Q && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtQuantity.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.R && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtReplacement.Focus();
                        retValue = true;
                    }

                }

                if (keyPressed == Keys.S && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtSchemeQuantity.Focus();
                        retValue = true;
                    }
                    else
                    {
                        btnSummary.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtTradeRate.Focus();
                        retValue = true;
                    }

                }

                if (keyPressed == Keys.Z && modifier == Keys.Alt)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        txtCSTAmount.Focus();
                        retValue = true;
                    }

                }
                if (keyPressed == Keys.Escape)
                {
                    if (dgvBatchGrid.Visible)
                    {
                        dgvBatchGrid.Visible = false;
                        pnlProductDetail.Enabled = true;
                        txtSchemeQuantity.Focus();
                        retValue = true;
                    }

                    else if (pnlProductDetail.Visible && dgvBatchGrid.Visible == false)
                    {
                        btnCancelClick();
                        retValue = true;
                    }
                    else if (pnlDebitCreditNote.Visible)
                    {
                        btnCRDBOKClick();
                        retValue = true;
                    }
                    else if (pnlSummary.Visible)
                    {
                        btnCancelSClick();
                        retValue = true;
                    }
                    else
                    {
                        retValue = Exit();
                    }
                }

                if (retValue == false)
                {
                    retValue = base.HandleShortcutAction(keyPressed, modifier);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }


        #endregion

        # region Stock update

        private bool CheckStockForUpdate(DataTable stocktable)
        {
            bool retValue = true;
            try
            {
                int mclosingstock = 0;
                int prodqty = 0;
                int prodscm = 0;
                int prodrepl = 0;
                bool ifbreak = false;
                foreach (DataGridViewRow temprow in dgtemp.Rows)
                {

                    if (temprow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(temprow.Cells["Temp_Quantity"].Value) > 0)
                    {
                        _SSSale.ProductID = temprow.Cells["Temp_ProductID"].Value.ToString();
                        _SSSale.Batchno = temprow.Cells["Temp_BatchNumber"].Value.ToString();
                        if (temprow.Cells["Temp_MRP"].Value != null)
                            _SSSale.MRP = Convert.ToDouble(temprow.Cells["Temp_MRP"].Value.ToString());
                        if (temprow.Cells["Temp_Scheme"].Value != null)
                            _SSSale.SchemeQuanity = Convert.ToInt32(temprow.Cells["Temp_Scheme"].Value.ToString());
                        //if (temprow.Cells["Temp_Replacement"].Value != null)
                        //    _SSSale.ReplacementQuantity = Convert.ToInt32(temprow.Cells["Temp_Replacement"].Value.ToString());
                        if (temprow.Cells["Temp_Quantity"].Value != null)
                            _SSSale.Quantity = Convert.ToInt32(temprow.Cells["Temp_Quantity"].Value.ToString());
                        if (temprow.Cells["Temp_StockID"].Value != null)
                            _SSSale.StockID = temprow.Cells["Temp_StockID"].Value.ToString();
                        mclosingstock = 0;
                        foreach (DataRow dr in stocktable.Rows)
                        {
                            if (dr["StockID"].ToString() == _SSSale.StockID)
                            {
                                //  if (dr["ProductID"].ToString() == _Purchase.ProductID && dr["BatchNumber"].ToString() == _Purchase.Batchno && Convert.ToDouble(dr["MRP"].ToString()) == _Purchase.MRP)
                                mclosingstock = Convert.ToInt32(dr["ClosingStock"].ToString());
                                break;
                            }

                        }
                        mclosingstock = mclosingstock - _SSSale.Quantity - _SSSale.SchemeQuanity;// -_SSSale.ReplacementQuantity;
                        prodqty = 0;
                        prodrepl = 0;
                        prodscm = 0;
                        foreach (DataGridViewRow prodrow in mpMSVC.Rows)
                        {
                            if (prodrow.Cells["Col_ProductName"].Value != null)
                            {
                                if (prodrow.Cells["Col_StockID"].Value != null && prodrow.Cells["Col_StockID"].Value.ToString() != "")
                                {
                                    if (prodrow.Cells["Col_StockID"].Value.ToString() == _SSSale.StockID)
                                    {
                                        //if (prodrow.Cells["Col_ProductID"].Value.ToString() == _Purchase.ProductID &&
                                        //    prodrow.Cells["Col_BatchNumber"].Value.ToString() == _Purchase.Batchno &&
                                        //   Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString()) == _Purchase.MRP)
                                        {
                                            if (prodrow.Cells["Col_Scheme"].Value != null)
                                                prodscm = Convert.ToInt32(prodrow.Cells["Col_Scheme"].Value.ToString());
                                            if (prodrow.Cells["Col_Replacement"].Value != null)
                                                prodrepl = Convert.ToInt32(prodrow.Cells["Col_Replacement"].Value.ToString());
                                            if (prodrow.Cells["Col_Quantity"].Value != null)
                                                prodqty = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                                            mclosingstock = mclosingstock + prodqty + prodrepl + prodscm;
                                            if (mclosingstock < 0)
                                            {
                                                ifbreak = true;
                                                retValue = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (ifbreak == true)
                            break;
                    }
                    if (ifbreak == true)
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;

        }

        private bool CheckStockForDelete()
        {
            bool retValue = true;
            int CurrentClosingStock = 0;
            deletedproductname = "";
            //  DataRow dr;
            try
            {
                foreach (DataGridViewRow temprow in dgtemp.Rows)
                {

                    if (temprow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(temprow.Cells["Temp_Quantity"].Value) > 0)
                    {
                        _SSSale.StockID = temprow.Cells["Temp_StockID"].Value.ToString();
                        _SSSale.ProductID = temprow.Cells["Temp_ProductID"].Value.ToString();
                        _SSSale.Batchno = temprow.Cells["Temp_BatchNumber"].Value.ToString();
                        if (temprow.Cells["Temp_MRP"].Value != null)
                            _SSSale.MRP = Convert.ToDouble(temprow.Cells["Temp_MRP"].Value.ToString());
                        if (temprow.Cells["Temp_Scheme"].Value != null)
                            _SSSale.SchemeQuanity = Convert.ToInt32(temprow.Cells["Temp_Scheme"].Value.ToString());
                        //if (temprow.Cells["Temp_Replacement"].Value != null)
                        //    _SSSale.ReplacementQuantity = Convert.ToInt32(temprow.Cells["Temp_Replacement"].Value.ToString());
                        if (temprow.Cells["Temp_Quantity"].Value != null)
                            _SSSale.Quantity = Convert.ToInt32(temprow.Cells["Temp_Quantity"].Value.ToString());
                        //if (temprow.Cells["Temp_UnitOfMeasure"].Value != null)
                        //    _SSSale .ProdLoosePack = Convert.ToInt16(temprow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                        //CurrentClosingStock = _SSSale.GetCurrentClosingStock(_SSSale.StockID);
                        if (CurrentClosingStock < (_SSSale.Quantity + _SSSale.SchemeQuanity)) //+ _SSSale.ReplacementQuantity))
                        {
                            deletedproductname = temprow.Cells["Temp_ProductName"].Value.ToString().Trim() + " " + temprow.Cells["Temp_UnitOfMeasure"].Value.ToString().Trim() + " " + temprow.Cells["Temp_Pack"].Value.ToString().Trim();
                            retValue = false;
                            break;
                        }
                        //dr = _Purchase.IFRecordFoundInStockTable();
                        //if (dr == null)
                        //{
                        //    retValue = false;
                        //    break;
                        //}
                        //else
                        //{

                        //    ReducePreviousStock();
                        //    _Purchase.DeleteAccountDetails();
                        //    _Purchase.DeletePreviousRecords();
                        //    _Purchase.DeleteDetails();

                        //}
                    }
                }
            }

            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;
        }


        private bool SaveParticularsProductwise()
        {

            bool returnVal = false;
            //    bool IfRecordFound = false;
            _SSSale.SerialNumber = 0;
            int mqty = 0;
            int mrepl = 0;
            int mscm = 0;
            int oldTempStock = 0;
            //int CurrentClosingStock = 0;
            string ThisStockID = "";
            //   DataRow dr;
            try
            {
                foreach (DataGridViewRow prodrow in mpMSVC.Rows)
                {
                    mqty = 0;
                    mrepl = 0;
                    mscm = 0;
                    if (prodrow.Cells["Col_Quantity"].Value != null)
                        mqty = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                    if (prodrow.Cells["Col_Replacement"].Value != null)
                        mrepl = Convert.ToInt32(prodrow.Cells["Col_Replacement"].Value.ToString());
                    if (prodrow.Cells["Col_Scheme"].Value != null)
                        mscm = Convert.ToInt32(prodrow.Cells["Col_Scheme"].Value.ToString());
                    if (prodrow.Cells["Col_ProductName"].Value != null && (mqty + mrepl + mscm) > 0)
                    {
                        _SSSale.SerialNumber += 1;
                        _SSSale.ProductID = "";
                        _SSSale.Batchno = "";
                        //  _SSSale.ProdLoosePack = 0;
                        _SSSale.MRP = 0;
                        _SSSale.Expiry = "";
                        _SSSale.ExpiryDate = "";
                        _SSSale.TradeRate = 0;
                        _SSSale.PurchaseRate = 0;
                        _SSSale.SaleRate = 0;
                        _SSSale.SchemeQuanity = 0;
                        // _SSSale.ReplacementQuantity = 0;
                        _SSSale.Quantity = 0;
                        _SSSale.VATPer = 0;
                        //  _SSSale.ProductVATPercent = 0;
                        _SSSale.ItemDiscountPer = 0;
                        _SSSale.ItemDiscountAmount = 0;
                        // _SSSale.AmountSchemeDiscount = 0;
                        //  _SSSale.AmountCST = 0;
                        //  _SSSale.SplDiscountPercent = 0;
                        //   _SSSale.AmountSplDiscountPerUnit = 0;
                        //  _SSSale.crdbam .AmountPurchaseVAT = 0;
                        _SSSale.CrdbVatAmount = 0;
                        _SSSale.CrdbAmtForZeroVAT = 0;
                        //  _SSSale.CrdbDiscPerperunit = 0;
                        _SSSale.StockID = "";
                        //  _SSSale.ShelfID = "";
                        //  _SSSale.ProductMargin = 0;
                        //   _SSSale.ProductMargin2 = 0;
                        //     _SSSale.PurScanCode = string.Empty;

                        _SSSale.DistributorSaleRate = 0;
                        _SSSale.DistributorSaleRatePercent = 0;

                        _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                        _SSSale.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        //if (prodrow.Cells["Col_UnitOfMeasure"].Value != null)
                        //    _SSSale.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Col_UnitOfMeasure"].Value.ToString());
                        if (prodrow.Cells["Col_MRP"].Value != null)
                            _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                        //if (prodrow.Cells["Col_Pack"].Value != null)
                        //    _SSSale.Pack = prodrow.Cells["Col_Pack"].Value.ToString();

                        if (prodrow.Cells["Col_Expiry"].Value != null)
                            _SSSale.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();

                        if (_SSSale.Expiry != "00/00")
                        {
                            _SSSale.ExpiryDate = General.GetValidExpiryDate(prodrow.Cells["Col_Expiry"].Value.ToString());
                        }
                        if (prodrow.Cells["Col_TradeRate"].Value != null)
                            _SSSale.TradeRate = Convert.ToDouble(prodrow.Cells["Col_TradeRate"].Value.ToString());
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                            _SSSale.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurchaseRate"].Value.ToString());
                        if (prodrow.Cells["Col_SaleRate"].Value != null)
                            _SSSale.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                        if (prodrow.Cells["Col_Scheme"].Value != null)
                            _SSSale.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Col_Scheme"].Value.ToString());
                        //if (prodrow.Cells["Col_Replacement"].Value != null)
                        //    _SSSale.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Col_Replacement"].Value.ToString());
                        if (prodrow.Cells["Col_Quantity"].Value != null)
                            _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        //if (prodrow.Cells["Col_VAT"].Value != null && prodrow.Cells["Col_VAT"].Value.ToString() != "")
                        //    _SSSale.PurchaseVATPercent = Convert.ToDouble(prodrow.Cells["Col_VAT"].Value.ToString());
                        if (prodrow.Cells["Col_ProdVATPer"].Value != null)
                            _SSSale.VATPer = Convert.ToDouble(prodrow.Cells["Col_ProdVATPer"].Value.ToString());
                        if (prodrow.Cells["Col_ItemDiscountPer"].Value != null)
                            _SSSale.ItemDiscountPer = Convert.ToDouble(prodrow.Cells["Col_ItemDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Col_ItemDiscountAmount"].Value != null)
                            _SSSale.ItemDiscountAmount = Convert.ToDouble(prodrow.Cells["Col_ItemDiscountAmount"].Value.ToString());
                        //if (prodrow.Cells["Col_ItemSCMDiscountAmount"].Value != null)
                        //    _SSSale.AmountSchemeDiscount = Convert.ToDouble(prodrow.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                        //if (prodrow.Cells["Col_CSTAmount"].Value != null)
                        //    _SSSale.AmountCST = Convert.ToDouble(prodrow.Cells["Col_CSTAmount"].Value.ToString());
                        //if (prodrow.Cells["Col_SplDiscountPer"].Value != null)
                        //    _SSSale.SplDiscountPercent = Convert.ToDouble(prodrow.Cells["Col_SplDiscountPer"].Value.ToString());
                        //if (prodrow.Cells["Col_SplDiscountAmount"].Value != null)
                        //    _SSSale.AmountSplDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Col_SplDiscountAmount"].Value.ToString());
                        //if (prodrow.Cells["Col_ShelfID"].Value != null)
                        //    _SSSale.ShelfID = prodrow.Cells["Col_ShelfID"].Value.ToString();
                        //if (prodrow.Cells["Col_VATAmountPurchase"].Value != null)
                        //    _SSSale AmountPurchaseVAT = Convert.ToDouble(prodrow.Cells["Col_VATAmountPurchase"].Value.ToString());

                        if (prodrow.Cells["Col_VATAmountSale"].Value != null)
                            _SSSale.VATAmount = Convert.ToDouble(prodrow.Cells["Col_VATAmountSale"].Value.ToString());
                        if (prodrow.Cells["Col_CashDiscountAmount"].Value != null)
                            _SSSale.CrdbDiscPer = Convert.ToDouble(prodrow.Cells["Col_CashDiscountAmount"].Value.ToString());
                        //if (prodrow.Cells["Col_Margin"].Value != null)
                        //    _SSSale.ProductMargin = Convert.ToDouble(prodrow.Cells["Col_Margin"].Value.ToString());

                        //if (prodrow.Cells["Col_Margin2"].Value != null)
                        //    _SSSale.ProductMargin2 = Convert.ToDouble(prodrow.Cells["Col_Margin2"].Value.ToString());

                        if (prodrow.Cells["Col_StockID"].Value != null && prodrow.Cells["Col_StockID"].Value.ToString() != "")
                            _SSSale.StockID = prodrow.Cells["Col_StockID"].Value.ToString();

                        //if (prodrow.Cells["Col_ScanCode"].Value != null && prodrow.Cells["Col_ScanCode"].Value.ToString() != "")
                        //    _SSSale.PurScanCode = prodrow.Cells["Col_ScanCode"].Value.ToString();
                        _SSSale.Name = prodrow.Cells["Col_ProductName"].Value.ToString();

                        if (prodrow.Cells["Col_DistributorSaleRate"].Value != null && prodrow.Cells["Col_DistributorSaleRate"].Value.ToString() != string.Empty)
                            _SSSale.DistributorSaleRate = Convert.ToDouble(prodrow.Cells["Col_DistributorSaleRate"].Value.ToString());
                        if (prodrow.Cells["Col_DistributorSaleRatePer"].Value != null && prodrow.Cells["Col_DistributorSaleRatePer"].Value.ToString() != string.Empty)
                            _SSSale.DistributorSaleRatePercent = Convert.ToDouble(prodrow.Cells["Col_DistributorSaleRatePer"].Value.ToString());

                        string expdt = "";
                        expdt = _SSSale.ExpiryDate;
                        if (expdt != "")
                        {
                            _SSSale.ExpiryDate = General.GetExpiryInyyyymmddForm(expdt);
                        }

                        ThisStockID = string.Empty;

                        //ThisStockID = _SSSale.CheckForBatchMRPStockIDInStockTable();

                        if (ThisStockID == string.Empty)
                        {
                            ThisStockID = _SSSale.CheckForBatchMRPInStockTable();
                        }

                        if (ThisStockID == string.Empty)
                        {

                            _SSSale.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            ThisStockID = _SSSale.StockID;
                            //Create new scancode 
                            //returnVal = _SSSale.AddProductDetailsInStockTable();
                        }
                        else
                        {

                            _SSSale.StockID = ThisStockID;
                            //CurrentClosingStock = _SSSale.GetCurrentClosingStock(ThisStockID);
                            oldTempStock = 0;
                            if (_Mode == OperationMode.Edit)
                            {
                                oldTempStock = GetOldStockFromTempGrid(ThisStockID);
                            }
                            //if ((CurrentClosingStock - (oldTempStock * _SSSale.ProdLoosePack) + ((_SSSale.Quantity + _SSSale.SchemeQuanity + _SSSale.ReplacementQuantity) * _SSSale.ProdLoosePack)) >= 0)
                            //    returnVal = _SSSale.UpdatePurchaseIntblStock();
                            //else
                            //{
                            //    returnVal = false;
                            //    break;
                            //}
                        }

                        //if (returnVal)
                        //    //returnVal = _SSSale.UpdatePurchaseStockInMasterProduct();
                        //else
                        //    break;
                        if (returnVal)
                        {
                            //_SSSale.UpdateLastPurhcaseDataInMasterProduct();
                            //_SSSale.RemoveFromShortList(_SSSale.ProductID);
                            //_SSSale.GetFirstAndSecondCreditor(_SSSale.ProductID);
                            //if (_SSSale.FirstCreditor != _SSSale.AccountID && _SSSale.SecondCreditor != _SSSale.AccountID)
                            //{
                            //    if (_SSSale.FirstCreditor == string.Empty)
                            //        _SSSale.FillFirstCreditorInMasterProduct();
                            //    else if (_SSSale.SecondCreditor == string.Empty)
                            //        _SSSale.FillSecondCreditorInMasterProduct();
                            //}

                        }
                        else
                            break;


                        if (returnVal)
                            returnVal = _SSSale.AddProductDetailsSS();
                        else
                            break;
                    }
                }
            }
            catch { returnVal = false; }
            return returnVal;

        }


        private int GetOldStockFromTempGrid(string stockID)
        {
            int closingstock = 0;
            string tempstockID = "";
            int qty = 0;
            int repl = 0;
            int scm = 0;
            foreach (DataGridViewRow dr in dgtemp.Rows)
            {
                tempstockID = "";
                if (dr.Cells["Temp_StockID"].Value != null && dr.Cells["Temp_StockID"].Value.ToString() != "")
                    tempstockID = dr.Cells["Temp_StockID"].Value.ToString();
                if (tempstockID == stockID)
                {
                    if (dr.Cells["Temp_Quantity"].Value != null && dr.Cells["Temp_Quantity"].Value.ToString() != "")
                        qty = Convert.ToInt32(dr.Cells["Temp_Quantity"].Value.ToString());
                    if (dr.Cells["Temp_Scheme"].Value != null && dr.Cells["Temp_Scheme"].Value.ToString() != "")
                        scm = Convert.ToInt32(dr.Cells["Temp_Scheme"].Value.ToString());
                    if (dr.Cells["Temp_Replacement"].Value != null && dr.Cells["Temp_Replacement"].Value.ToString() != "")
                        repl = Convert.ToInt32(dr.Cells["Temp_Replacement"].Value.ToString());
                    closingstock = qty + scm + repl;
                    break;
                }
            }
            return closingstock;
        }

        private bool UpdateClosingStockinCache()
        {
            bool returnVal = false;
            try
            {
                General.UpdateProductListCacheTest(mpMSVC.Rows, "Col_ProductID", dgtemp.Rows, "Temp_ProductID");
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }

        //private bool SaveAndUpdateDebitCreditNote()
        //{
        //    {
        //        bool returnVal = true;
        //        try
        //        {
        //            foreach (DataGridViewRow prodrow in dgCreditNote.Rows)
        //            {
        //                if ((prodrow.Cells["Col_CrdbID"].Value) != null && (Convert.ToBoolean(prodrow.Cells["Col_Check"].Value) == true))
        //                {
        //                    _Purchase.CreditDebitNoteID = prodrow.Cells["Col_CrdbID"].Value.ToString();
        //                    _Purchase.Amount = Convert.ToDouble(prodrow.Cells["Col_AmountNet"].Value.ToString());
        //                    returnVal = _Purchase.UpdateCreditDebitNoteAdjustedDetails(_Purchase.CreditDebitNoteID, _Purchase.Amount, _Purchase.VoucherType, _Purchase.VoucherNumber, _Purchase.VoucherDate, _Purchase.PurchaseBillNumber, _Purchase.Id, _Purchase.VoucherSeries);
        //                }
        //            }
        //        }
        //        catch { returnVal = false; }
        //        return returnVal;
        //    }
        //}

        //private bool clearPreviousdebitcreditnotes()
        //{
        //    bool retValue = true;
        //    retValue = _Purchase.clearPreviousdebitcreditnotes(_Purchase.Id);
        //    return retValue;
        //}

        #endregion

        #region IChildDetail Members

        #endregion

        #region Other Private Methods
        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }
        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = ((PSComboBoxNew)sender).SelectedID;
                mcbCreditor.SelectedID = selectedId;
                FillPartyCombo();
                mcbCreditor.SelectedID = selectedId;
                if (mcbCreditor.SeletedItem != null)
                {
                    _SSSale.TokenNumber = 0;
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    if (mcbCreditor.SeletedItem.ItemData[6] != null && mcbCreditor.SeletedItem.ItemData[6].ToString() != "")
                        _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    _SSSale.DocID = mcbCreditor.SeletedItem.ItemData[7];
                    _SSSale.TransactionType = mcbCreditor.SeletedItem.ItemData[8];
                    if (_Mode == OperationMode.Add)
                    {
                        if (_SSSale.TransactionType == "CS")
                        {
                            cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                            _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
                        }
                        else if (_SSSale.TransactionType == "CR")
                        {
                            cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCreditStatement);
                            _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditStatementSale;
                        }
                        else
                        {
                            cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                            _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
                        }
                        txtVouType.Text = _SSSale.CrdbVouType;
                    }
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mcbCreditor_SeletectIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                    _SSSale.AccountID = mcbCreditor.SelectedID;
                if (mcbCreditor.SeletedItem == null)
                {
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                }
                else
                {
                    if (General.CurrentUser.Level == 0)
                    {
                        cbEditRate.Enabled = true;
                    }

                    FillCreditDebitNote();
                    _SSSale.CrdbName = mcbCreditor.SeletedItem.ItemData[2];
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    //   txtDiscPercent.Text = mcbCreditor.SeletedItem.ItemData[9];
                    if (mcbCreditor.SeletedItem.ItemData[6] != "")
                        _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[6].ToString());

                    _SSSale.TransactionType = mcbCreditor.SeletedItem.ItemData[8];

                    if (_Mode == OperationMode.Add)
                    {
                        if (_SSSale.TransactionType == "CS")
                        {
                            cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                            _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
                        }
                        else if (_SSSale.TransactionType == "CR")
                        {
                            cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCreditStatement);
                            _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditStatementSale;
                        }
                        else
                        {
                            cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                            _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
                        }
                        txtVouType.Text = _SSSale.CrdbVouType;
                    }

                    _SSSale.GetPendingAmount(mcbCreditor.SelectedID);
                    _SSSale.GetOpeningBalance(mcbCreditor.SelectedID);
                    _SSSale.PendingAmount = _SSSale.OpeningBalance + (_SSSale.TotalDebit - _SSSale.TotalCredit);
                    txtPendingBalance.Text = Math.Abs(_SSSale.PendingAmount).ToString("#0.00");
                    txtNarration.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        //void mcbCreditor_SeletectIndexChanged(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        Account _account = new Account();
        //        _account.Id = mcbCreditor.SelectedID;
        //      _SSSale.AccountID = mcbCreditor.SelectedID;
        //        _account.ReadDetailsByID();
        //        if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
        //        {
        //            txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
        //            txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[5];
        //            if (_Mode == OperationMode.Add)
        //                txtCashDiscountPerS.Text = _account.AccDiscountOffered.ToString("0.00");
        //            _SSSale.GetPendingAmount(mcbCreditor.SelectedID);
        //            _SSSale.GetOpeningBalance(mcbCreditor.SelectedID);
        //            _SSSale.PendingAmount = _SSSale.OpeningBalance + (_SSSale.TotalDebit - _SSSale.TotalCredit);
        //            txtPendingBalance.Text = Math.Abs(_SSSale.PendingAmount).ToString("#0.00");
        //            _SSSale.PendingAmount = 0;
        //          //  _SSSale.PendingAmount = _SSSale.GetDNAmount(mcbCreditor.SelectedID);

        //            txtBillNumber.Focus();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}

        private void mpMSVC_OnDetailsFilled(DataGridViewRow selectedRow)
        {
            
            try
            {
                _SchemeData = null;
                //if (mrowcount > 0)
                //    {
                string mprod = "";
                if (selectedRow.Cells[0].Value != null)
                    mprod = selectedRow.Cells[0].Value.ToString();
                if (mprod != "")
                {
                    _SchemeData = _SSSale.GetSchemeDetails(mprod);                   
                }
                _SSSale.CurrentProductStock = Convert.ToInt32(selectedRow.Cells["Col_ClStock"].Value.ToString());
                lblFooterMessage.Text = "Product Stock : " + _SSSale.CurrentProductStock.ToString("#0");
                FillBatchGrid();
                lblFooterMessage.Text = "Esc = Exit   Enter = Select Batch";

                dgvBatchGrid.BringToFront();
                dgvBatchGrid.Location = GetdgvBatchGridLocation();
                dgvBatchGrid.Height = 237;
                dgvBatchGrid.Width = pnlProductDetail.Width;
                dgvBatchGrid.Visible = true;
                dgvBatchGrid.Enabled = true;
                dgvBatchGrid.Enabled = true;
                pnlProductDetail.Enabled = false;
             //   CalculatePurRateSaleRateAndAmount();
                dgvBatchGrid.Focus();

                //_SSSale.MRP = 0;
                //double mmamt = 0;
                //if (mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                //    _SSSale.MRP = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
                //if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
                //    mmamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString());
                //_SSSale.ProductID = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                ////       int mmqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                //mpMSVC.Enabled = false;
                ////if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
                ////{
                ////    dgvLastPurchase.Visible = true;
                ////    dgvLastPurchase.Location = GetdgvLastPurchaseLocation();
                ////    dgvLastPurchase.BringToFront();
                ////}
                ////FillLastPurchase();
                //pnlBillDetails.Enabled = false;
                //pnlProductDetail.BringToFront();
                //pnlProductDetail.Location = GetpnlProductDetailLocation();
                //pnlProductDetail.Visible = true;
                //if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _SSSale.StatementNumber > 0)
                //    pnlProductDetail1.Enabled = false;
                //else
                //    pnlProductDetail1.Enabled = true;

                //_LastStockID = string.Empty;
                //if (mmamt == 0)
                //{
                //    IfEditPreviousRow = "N";
                //    FillLastPurchaseDataFromMasterProduct();
                //}
                //else
                //{
                //    IfEditPreviousRow = "Y";
                //    FillDataFromMPSVRow();
                //}

                //txtQuantity.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private Point GetpnlProductDetailLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = mpMSVC.Location.X + 335;
                pt.Y = mpMSVC.Location.Y + 3;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        private Point GetdgvLastPurchaseLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlBillDetails.Location.X + 400;
                pt.Y = pnlBillDetails.Location.Y + 23;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private Point GetpnlSummaryLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = mpMSVC.Location.X + 335;
                pt.Y = mpMSVC.Location.Y + 3;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        private Point GetdgvBatchGridLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = mpMSVC.Location.X + 335;
                pt.Y = mpMSVC.Location.Y + 125;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private bool DeletePreviousRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _SSSale.DeletePreviousRecords();
            }
            catch { returnVal = false; }
            return returnVal;
        }
        private bool AddPreviousRowsInDeleteDetail()
        {
            bool returnVal = false;
            //_SSSale.SerialNumber = 0;
            //try
            //{
            //    foreach (DataGridViewRow prodrow in dgtemp.Rows)
            //    {
            //        if (prodrow.Cells["Temp_ProductName"].Value != null)
            //        {
            //            _Purchase.SerialNumber += 1;
            //            _Purchase.ProductID = "";
            //            _Purchase.Batchno = "";
            //            _Purchase.ProdLoosePack = 0;
            //            _Purchase.MRP = 0;
            //            _Purchase.Expiry = "";
            //            _Purchase.ExpiryDate = "";
            //            _Purchase.TradeRate = 0;
            //            _Purchase.PurchaseRate = 0;
            //            _Purchase.SaleRate = 0;
            //            _Purchase.SchemeQuanity = 0;
            //            _Purchase.ReplacementQuantity = 0;
            //            _Purchase.Quantity = 0;
            //            _Purchase.PurchaseVATPercent = 0;
            //            _Purchase.ProductVATPercent = 0;
            //            _Purchase.ItemDiscountPercent = 0;
            //            _Purchase.AmountItemDiscount = 0;
            //            _Purchase.AmountSchemeDiscount = 0;
            //            _Purchase.AmountCST = 0;
            //            _Purchase.SplDiscountPercent = 0;
            //            _Purchase.AmountSplDiscountPerUnit = 0;
            //            _Purchase.AmountPurchaseVAT = 0;
            //            _Purchase.AmountProductVAT = 0;
            //            _Purchase.AmountZeroVAT = 0;
            //            _Purchase.AmountCashDiscountPerUnit = 0;
            //            _Purchase.StockID = "";
            //            _Purchase.ProductMargin = 0;
            //            _Purchase.ProductMargin2 = 0;

            //            _Purchase.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            //            _Purchase.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
            //            _Purchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
            //            if (prodrow.Cells["Temp_UnitOfMeasure"].Value != null)
            //                _Purchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
            //            if (prodrow.Cells["Temp_MRP"].Value != null)
            //                _Purchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value.ToString());
            //            if (prodrow.Cells["Temp_Expiry"].Value != null)
            //                _Purchase.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
            //            if (_Purchase.Expiry != "00/00")
            //                _Purchase.ExpiryDate = General.GetValidExpiryDate(prodrow.Cells["Temp_Expiry"].Value.ToString());
            //            if (prodrow.Cells["Temp_TradeRate"].Value != null)
            //                _Purchase.TradeRate = Convert.ToDouble(prodrow.Cells["Temp_TradeRate"].Value.ToString());
            //            if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
            //                _Purchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value.ToString());
            //            if (prodrow.Cells["Temp_SaleRate"].Value != null)
            //                _Purchase.SaleRate = Convert.ToDouble(prodrow.Cells["Temp_SaleRate"].Value.ToString());
            //            if (prodrow.Cells["Temp_Scheme"].Value != null)
            //                _Purchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value.ToString());
            //            if (prodrow.Cells["Temp_Replacement"].Value != null)
            //                _Purchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
            //            if (prodrow.Cells["Temp_Quantity"].Value != null)
            //                _Purchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value.ToString());
            //            if (prodrow.Cells["Temp_VAT"].Value != null && prodrow.Cells["Temp_VAT"].Value.ToString() != "")
            //                _Purchase.PurchaseVATPercent = Convert.ToDouble(prodrow.Cells["Temp_VAT"].Value.ToString());
            //            if (prodrow.Cells["Temp_ProdVATPer"].Value != null)
            //                _Purchase.ProductVATPercent = Convert.ToDouble(prodrow.Cells["Temp_ProdVATPer"].Value.ToString());
            //            if (prodrow.Cells["Temp_ItemDiscountPer"].Value != null)
            //                _Purchase.ItemDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountPer"].Value.ToString());
            //            if (prodrow.Cells["Temp_ItemDiscountAmount"].Value != null)
            //                _Purchase.AmountItemDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountAmount"].Value.ToString());
            //            if (prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value != null)
            //                _Purchase.AmountSchemeDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value.ToString());
            //            if (prodrow.Cells["Temp_CSTAmount"].Value != null)
            //                _Purchase.AmountCST = Convert.ToDouble(prodrow.Cells["Temp_CSTAmount"].Value.ToString());
            //            if (prodrow.Cells["Temp_SplDiscountPer"].Value != null)
            //                _Purchase.SplDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountPer"].Value.ToString());
            //            if (prodrow.Cells["Temp_SplDiscountAmount"].Value != null)
            //                _Purchase.AmountSplDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountAmount"].Value.ToString());

            //            if (prodrow.Cells["Temp_VATAmountPurchase"].Value != null)
            //                _Purchase.AmountPurchaseVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountPurchase"].Value.ToString());

            //            if (prodrow.Cells["Temp_VATAmountSale"].Value != null)
            //                _Purchase.AmountProductVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountSale"].Value.ToString());
            //            if (prodrow.Cells["Temp_CashDiscountAmount"].Value != null)
            //                _Purchase.AmountCashDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_CashDiscountAmount"].Value.ToString());
            //            if (prodrow.Cells["Temp_Margin"].Value != null)
            //                _Purchase.ProductMargin = Convert.ToDouble(prodrow.Cells["Temp_Margin"].Value.ToString());

            //            if (prodrow.Cells["Temp_Margin2"].Value != null)
            //                _Purchase.ProductMargin2 = Convert.ToDouble(prodrow.Cells["Temp_Margin2"].Value.ToString());

            //            if (prodrow.Cells["Temp_StockID"].Value != null)
            //                _Purchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();

            //            string expdt = "";
            //            expdt = _Purchase.ExpiryDate;
            //            if (expdt != "")
            //            {
            //                _Purchase.ExpiryDate = General.GetExpiryInyyyymmddForm(expdt);
            //            }

            //            returnVal = _Purchase.AddDeletedProductDetailsSS();

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteException(ex);
            //    returnVal = false;
            //}
            return returnVal;
        }
        private bool AddPreviousRowsInChangedDetail()
        {
            bool returnVal = false;
            //_Purchase.SerialNumber = 0;
            //try
            //{
            //    foreach (DataGridViewRow prodrow in dgtemp.Rows)
            //    {
            //        if (prodrow.Cells["Temp_ProductName"].Value != null)
            //        {
            //            _Purchase.SerialNumber += 1;
            //            _Purchase.ProductID = "";
            //            _Purchase.Batchno = "";
            //            _Purchase.ProdLoosePack = 0;
            //            _Purchase.MRP = 0;
            //            _Purchase.Expiry = "";
            //            _Purchase.ExpiryDate = "";
            //            _Purchase.TradeRate = 0;
            //            _Purchase.PurchaseRate = 0;
            //            _Purchase.SaleRate = 0;
            //            _Purchase.SchemeQuanity = 0;
            //            _Purchase.ReplacementQuantity = 0;
            //            _Purchase.Quantity = 0;
            //            _Purchase.PurchaseVATPercent = 0;
            //            _Purchase.ProductVATPercent = 0;
            //            _Purchase.ItemDiscountPercent = 0;
            //            _Purchase.AmountItemDiscount = 0;
            //            _Purchase.AmountSchemeDiscount = 0;
            //            _Purchase.AmountCST = 0;
            //            _Purchase.SplDiscountPercent = 0;
            //            _Purchase.AmountSplDiscountPerUnit = 0;
            //            _Purchase.AmountPurchaseVAT = 0;
            //            _Purchase.AmountProductVAT = 0;
            //            _Purchase.AmountZeroVAT = 0;
            //            _Purchase.AmountCashDiscountPerUnit = 0;
            //            _Purchase.StockID = "";
            //            _Purchase.ProductMargin = 0;
            //            _Purchase.ProductMargin2 = 0;

            //            _Purchase.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            //            _Purchase.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
            //            _Purchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
            //            if (prodrow.Cells["Temp_UnitOfMeasure"].Value != null)
            //                _Purchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
            //            if (prodrow.Cells["Temp_MRP"].Value != null)
            //                _Purchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value.ToString());
            //            if (prodrow.Cells["Temp_Expiry"].Value != null)
            //                _Purchase.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
            //            if (_Purchase.Expiry != "00/00")
            //                _Purchase.ExpiryDate = General.GetValidExpiryDate(prodrow.Cells["Temp_Expiry"].Value.ToString());
            //            if (prodrow.Cells["Temp_TradeRate"].Value != null)
            //                _Purchase.TradeRate = Convert.ToDouble(prodrow.Cells["Temp_TradeRate"].Value.ToString());
            //            if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
            //                _Purchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value.ToString());
            //            if (prodrow.Cells["Temp_SaleRate"].Value != null)
            //                _Purchase.SaleRate = Convert.ToDouble(prodrow.Cells["Temp_SaleRate"].Value.ToString());
            //            if (prodrow.Cells["Temp_Scheme"].Value != null)
            //                _Purchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value.ToString());
            //            if (prodrow.Cells["Temp_Replacement"].Value != null)
            //                _Purchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
            //            if (prodrow.Cells["Temp_Quantity"].Value != null)
            //                _Purchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value.ToString());
            //            if (prodrow.Cells["Temp_VAT"].Value != null && prodrow.Cells["Temp_VAT"].Value.ToString() != "")
            //                _Purchase.PurchaseVATPercent = Convert.ToDouble(prodrow.Cells["Temp_VAT"].Value.ToString());
            //            if (prodrow.Cells["Temp_ProdVATPer"].Value != null)
            //                _Purchase.ProductVATPercent = Convert.ToDouble(prodrow.Cells["Temp_ProdVATPer"].Value.ToString());
            //            if (prodrow.Cells["Temp_ItemDiscountPer"].Value != null)
            //                _Purchase.ItemDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountPer"].Value.ToString());
            //            if (prodrow.Cells["Temp_ItemDiscountAmount"].Value != null)
            //                _Purchase.AmountItemDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemDiscountAmount"].Value.ToString());
            //            if (prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value != null)
            //                _Purchase.AmountSchemeDiscount = Convert.ToDouble(prodrow.Cells["Temp_ItemSCMDiscountAmount"].Value.ToString());
            //            if (prodrow.Cells["Temp_CSTAmount"].Value != null)
            //                _Purchase.AmountCST = Convert.ToDouble(prodrow.Cells["Temp_CSTAmount"].Value.ToString());
            //            if (prodrow.Cells["Temp_SplDiscountPer"].Value != null)
            //                _Purchase.SplDiscountPercent = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountPer"].Value.ToString());
            //            if (prodrow.Cells["Temp_SplDiscountAmount"].Value != null)
            //                _Purchase.AmountSplDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_SplDiscountAmount"].Value.ToString());

            //            if (prodrow.Cells["Temp_VATAmountPurchase"].Value != null)
            //                _Purchase.AmountPurchaseVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountPurchase"].Value.ToString());

            //            if (prodrow.Cells["Temp_VATAmountSale"].Value != null)
            //                _Purchase.AmountProductVAT = Convert.ToDouble(prodrow.Cells["Temp_VATAmountSale"].Value.ToString());
            //            if (prodrow.Cells["Temp_CashDiscountAmount"].Value != null)
            //                _Purchase.AmountCashDiscountPerUnit = Convert.ToDouble(prodrow.Cells["Temp_CashDiscountAmount"].Value.ToString());
            //            if (prodrow.Cells["Temp_Margin"].Value != null)
            //                _Purchase.ProductMargin = Convert.ToDouble(prodrow.Cells["Temp_Margin"].Value.ToString());

            //            if (prodrow.Cells["Temp_Margin2"].Value != null)
            //                _Purchase.ProductMargin2 = Convert.ToDouble(prodrow.Cells["Temp_Margin2"].Value.ToString());

            //            if (prodrow.Cells["Temp_StockID"].Value != null)
            //                _Purchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();

            //            string expdt = "";
            //            expdt = _Purchase.ExpiryDate;
            //            if (expdt != "")
            //            {
            //                _Purchase.ExpiryDate = General.GetExpiryInyyyymmddForm(expdt);
            //            }

            //            returnVal = _Purchase.AddChangedProductDetailsSS();

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteException(ex);
            //    returnVal = false;
            //}
            return returnVal;
        }
        private bool ReducePreviousStock()
        {
            bool returnVal = false;
            ////bool ifRecordFound = false;
            //try
            //{
            //    foreach (DataGridViewRow prodrow in dgtemp.Rows)
            //    {
            //        if (prodrow.Cells["Temp_ProductName"].Value != null &&
            //            (Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0
            //             || Convert.ToDouble(prodrow.Cells["Temp_Scheme"].Value) > 0
            //             || Convert.ToDouble(prodrow.Cells["Temp_Replacement"].Value) > 0))
            //        {
            //            _Purchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
            //            _Purchase.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
            //            _Purchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
            //            _Purchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
            //            _Purchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
            //            _Purchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value);
            //            _Purchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
            //            _Purchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
            //            _Purchase.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value.ToString());
            //            DataRow dr = _Purchase.IfStockIDFoundInStockTable(_Purchase.StockID);
            //            if (dr != null)
            //                returnVal = _Purchase.UpdatePurchaseIntblStockReduceFromTemp();
            //            returnVal = _Purchase.UpdatePurchaseStockInmasterProductReduceFromTemp();

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteException(ex);
            //    returnVal = false;
            //}
            return returnVal;

        }


        private bool CheckStockForDeletedRows()
        {
            bool returnVal = true;
            //string gridstockid;
            //int CurrentClosingStock = 0;
            //deletedproductname = "";
            ////bool ifRecordFound = false;
            //try
            //{
            //    foreach (DataGridViewRow prodrow in dgtemp.Rows)
            //    {
            //        if (prodrow.Cells["Temp_StockID"].Value != null &&
            //            (Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0
            //             || Convert.ToDouble(prodrow.Cells["Temp_Scheme"].Value) > 0
            //             || Convert.ToDouble(prodrow.Cells["Temp_Replacement"].Value) > 0))
            //        {
            //            _Purchase.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
            //            deletedproductname = prodrow.Cells["Temp_ProductName"].Value.ToString().Trim() + " " + prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString().Trim() + " " + prodrow.Cells["Temp_Pack"].Value.ToString().Trim();
            //            //_Purchase.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
            //            //_Purchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
            //            //_Purchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
            //            _Purchase.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
            //            _Purchase.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value);
            //            _Purchase.ReplacementQuantity = Convert.ToInt32(prodrow.Cells["Temp_Replacement"].Value.ToString());
            //            _Purchase.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
            //            string ifmatchfound = "N";
            //            foreach (DataGridViewRow gridrow in mpMSVC.Rows)
            //            {
            //                gridstockid = "";
            //                if (gridrow.Cells["Col_StockID"].Value != null && gridrow.Cells["Col_StockID"].Value.ToString() != "")
            //                    gridstockid = gridrow.Cells["Col_StockID"].Value.ToString();
            //                if (_Purchase.StockID == gridstockid)
            //                {
            //                    deletedproductname = "";
            //                    ifmatchfound = "Y";
            //                    break;
            //                }

            //            }
            //            if (ifmatchfound == "N")
            //            {
            //                CurrentClosingStock = _Purchase.GetCurrentClosingStock(_Purchase.StockID);
            //                if (CurrentClosingStock < (_Purchase.Quantity + _Purchase.SchemeQuanity + _Purchase.ReplacementQuantity) * _Purchase.ProdLoosePack)
            //                {
            //                    returnVal = false;
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteException(ex);
            //    returnVal = false;
            //}
            return returnVal;

        }


        #endregion

        # region Contruct

        public void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSVC.ColumnsMain.Clear();
            
            try
            {               

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 192;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UnitOfMeasure";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 45;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Company";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Co.";
                column.Width = 35;
                column.ReadOnly = true;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Box1";
                column.HeaderText = "Box";
                column.Width = 90;
                column.ReadOnly = true;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdVATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT %";
                column.Width = 85;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFBarCodeRequired";
                column.DataPropertyName = "ProdIfBarCodeRequired";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber1";
                column.HeaderText = "Batch";
                column.Width = 105;
                column.ReadOnly = true;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 90;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Exp";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "Trd.Rate";
                column.Width = 74;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 74;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT";
                column.DataPropertyName = "PurchaseVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 55;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 45;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Replacement";
                column.DataPropertyName = "ReplacementQuantity";
                column.HeaderText = "Repl";
                column.Width = 40;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountPer";
                column.DataPropertyName = "ItemDiscountPercent";
                column.HeaderText = "Disc";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 93;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountAmount";
                column.DataPropertyName = "AmountItemDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SplDiscountPer";
                column.DataPropertyName = "SpecialDiscountPercent";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SplDiscountAmount";
                column.DataPropertyName = "AmountSpecialDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountPurchase";
                column.DataPropertyName = "AmountPurchaseVAT";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CSTAmount";
                column.DataPropertyName = "AmountCST";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CSTPer";
                column.DataPropertyName = "CSTPercent";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmount";
                column.DataPropertyName = "AmountSchemeDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmountPerUnit";
                column.DataPropertyName = "SchemeDiscountPercent";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfOctroi";
                column.DataPropertyName = "ProdIfOctroi";
                column.Width = 40;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreditNoteAmount";
                column.DataPropertyName = "AmountCreditNote";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CashDiscountAmount";
                column.DataPropertyName = "AmountCashDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompID";
                column.DataPropertyName = "ProdCompID";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountSale";
                column.DataPropertyName = "AmountProdVAT";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistributorSaleRatePer";
                column.DataPropertyName = "DistributorSaleRatePer";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfCode";
                column.DataPropertyName = "ShelfCode";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfID";
                column.DataPropertyName = "ProdShelfID";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin";
                column.DataPropertyName = "Margin";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin2";
                column.DataPropertyName = "MarginAfterDiscount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScanCode";
                column.DataPropertyName = "ScanCode";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructSubColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSVC.ColumnsSub.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProdName";
                column.Width = 350;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BoxQty";
                column.DataPropertyName = "ProdBoxQuantity";
                column.HeaderText = "BoxQty";
                column.Width = 55;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClStock";
                column.DataPropertyName = "ProdClosingStockPack";
                column.HeaderText = "Cl.Stk";
                column.Width = 55;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdVATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFBarCodeRequired";
                column.DataPropertyName = "ProdIfBarCodeRequired";
                column.HeaderText = "BarCode";
                column.Width = 40;
                mpMSVC.ColumnsSub.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void ConstructBatchGrid()
        {
            DataGridViewTextBoxColumn column;
            try
            {
                dgvBatchGrid.Columns.Clear();
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batchno";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batchno";
                column.Width = 90;
                dgvBatchGrid.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 50;
                dgvBatchGrid.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "TrateRate";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                dgvBatchGrid.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                dgvBatchGrid.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                dgvBatchGrid.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Closing Stock";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 140;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                dgvBatchGrid.Columns.Add(column);
                //7              
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                dgvBatchGrid.Columns.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Width = 70;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseVATPer";
                column.DataPropertyName = "PurchaseVATPercent";
                column.Width = 70;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);
                //
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCSTPer";
                column.DataPropertyName = "ProdCSTPercent";
                column.Width = 70;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScanCode";
                column.DataPropertyName = "ScanCode";
                column.Width = 120;
                column.Visible = false;
                dgvBatchGrid.Columns.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.Width = 70;
                column.Visible = false;
                dgvBatchGrid.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "PartyID";
                column.Width = 140;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvBatchGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 40;
                column.Visible = false;
                dgvBatchGrid.Columns.Add(column);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        //private void ConstructSchemeGrid()
        //{
        //    DataGridViewTextBoxColumn column;
        //    try
        //    {
        //        dgvSchemeGrid.Columns.Clear();
        //        //0
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_ProductID";
        //        column.DataPropertyName = "ProductID";             
        //        column.Width = 90;
        //        column.Visible = false;
        //        dgvSchemeGrid.Columns.Add(column);
        //        //1
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_ProductQuantity";               
        //        column.HeaderText = "Prod.Qty";
        //        column.Width = 50;
        //        dgvSchemeGrid.Columns.Add(column);
        //        //2
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_SchemeQuantity";               
        //        column.HeaderText = "Scm.Qty";
        //        column.Width = 70;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //        dgvSchemeGrid.Columns.Add(column);
        //        //3 
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        private void ConstructTempGridColumns()
        {
            try
            {
                dgtemp.Columns.Clear();
                DataGridViewTextBoxColumn column;

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 180;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_UnitOfMeasure";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Company";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Co.";
                column.Width = 35;
                column.ReadOnly = true;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 40;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 88;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Exp";
                column.Width = 45;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "Trade Rate";
                column.Width = 70;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 70;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VAT";
                column.DataPropertyName = "PurchaseVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 45;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Scheme";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 45;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Replacement";
                column.DataPropertyName = "ReplacementQuantity";
                column.HeaderText = "Repl";
                column.Width = 45;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ItemDiscountPer";
                column.DataPropertyName = "ItemDiscountPercent";
                column.HeaderText = "Disc";
                column.Width = 45;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 80;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ProdVATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT %";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ItemDiscountAmount";
                column.DataPropertyName = "AmountItemDiscount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_SplDiscountPer";
                column.DataPropertyName = "SpecialDiscountPercent";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_SplDiscountAmount";
                column.DataPropertyName = "AmountSpecialDiscount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATAmountPurchase";
                column.DataPropertyName = "ProdVATAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_CSTAmount";
                column.DataPropertyName = "AmountCST";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ItemSCMDiscountAmount";
                column.DataPropertyName = "ItemSCMDiscountAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ItemSCMDiscountAmountPerUnit";
                column.DataPropertyName = "ItemSCMDiscountAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_IfOctroi";
                column.DataPropertyName = "ProdIfOctroi";
                column.Width = 40;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_CreditNoteAmount";
                column.DataPropertyName = "CreditNoteAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_CashDiscountAmount";
                column.DataPropertyName = "CashDiscountAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_CompID";
                column.DataPropertyName = "ProdCompID";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATAmountSale";
                column.DataPropertyName = "ProdVATAmount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Margin";
                column.DataPropertyName = "Margin";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Margin2";
                column.DataPropertyName = "MarginAfterDiscount";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 80;
                column.Visible = false;
                dgtemp.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructCreditNoteColumns()
        {
            dgCreditNote.ColumnsMain.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CrdbID";
                column.DataPropertyName = "CRDBID";
                column.HeaderText = "VouSeries";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgCreditNote.ColumnsMain.Add(column);

                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
                columnCheck.HeaderText = "Check";
                columnCheck.Width = 50;
                columnCheck.Visible = true;
                dgCreditNote.ColumnsMain.Add(columnCheck);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Series";
                column.Width = 50;
                column.ReadOnly = true;
                dgCreditNote.ColumnsMain.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.ReadOnly = true;
                dgCreditNote.ColumnsMain.Add(column);
                //3 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 50;
                dgCreditNote.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 80;
                column.ReadOnly = true;
                dgCreditNote.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 80;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCreditNote.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Narr";
                column.DataPropertyName = "Narration";
                column.HeaderText = "Narration";
                column.Width = 160;
                column.ReadOnly = true;
                dgCreditNote.ColumnsMain.Add(column);
                //8

                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountClear";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "AmountClear";
                column.Visible = false;
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCreditNote.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Acc";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "a1";
                column.Width = 50;
                column.Visible = false;
                dgCreditNote.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void ConstructPaymentDetailsColumns()
        {
            dgPaymentDetails.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CrdbID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "ID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.Columns.Add(column);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurID";
                column.DataPropertyName = "MasterPurchaseID";
                column.HeaderText = "PurID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.Columns.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 80;
                column.ReadOnly = true;
                dgPaymentDetails.Columns.Add(column);
                //3 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 80;
                dgPaymentDetails.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 90;
                column.DefaultCellStyle.Format = "d2";
                column.ReadOnly = true;
                dgPaymentDetails.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                column.DataPropertyName = "ClearAmount";
                column.HeaderText = "Cleared Amount";
                column.Width = 90;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgPaymentDetails.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CrdbID";
                column.DataPropertyName = "CBID";
                column.HeaderText = "cID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CrdbID";
                column.DataPropertyName = "purchaseID";
                column.HeaderText = "pID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructLastPurchaseColumns()
        {
            dgvLastPurchase.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn column;
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PurchaseID";
                column.HeaderText = "ID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 70;
                column.ReadOnly = true;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 80;
                column.ReadOnly = true;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 65;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
                column.Width = 65;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme";
                column.DataPropertyName = "Scheme";
                column.HeaderText = "SCM";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin";
                column.DataPropertyName = "MarginAfterDiscount";
                column.HeaderText = "Margin%";
                column.Width = 50;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Name of party";
                column.Width = 140;
                column.ReadOnly = true;
                dgvLastPurchase.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void InitializeMainSubViewControl(string vmode)
        {

            try
            {
                ConstructMainColumns();
                ConstructSubColumns();
                ConstructBatchGrid();
                ConstructLastPurchaseColumns();
             //   ConstructSchemeGrid();

                if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit) && _SSSale.AddNewRowCheck(mpMSVC))
                    mpMSVC.Rows.Add();
                FormatGrids();

                DataTable dtable = new DataTable();
                if (vmode == "C")
                {
                    dtable = _SSSale.ReadProductDetailsByIDForChanged();
                    headerLabel1.Text = "Debtor Sale => Changed Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else if (vmode == "D")
                {
                    dtable = _SSSale.ReadProductDetailsByIDForDeleted();
                    headerLabel1.Text = "Debtor Sale => Deleted Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else
                    dtable = _SSSale.ReadProductDetailsByID();

                if (dtable != null)
                    _SSSale.NoofRows = dtable.Rows.Count;

                psLableWithBorder1.Text = _SSSale.NoofRows.ToString();
                mpMSVC.DataSourceMain = dtable;

                string tempFileName = General.GetPurchaseTempFile();

                if (_Mode == OperationMode.Add && File.Exists(tempFileName))
                {
                    mpMSVC.DataSourceMain = null;
                    mpMSVC.Rows.Clear();

                    DataSet ds = new DataSet();
                    ds.ReadXml(tempFileName);
                    mpMSVC.DataSourceMain = ds.Tables[0];

                }

               
                //DataTable dt = PharmaSysRetailPlusCache.GetProductData();
                DataTable dt = General.ProductList;
                mpMSVC.DataSource = dt;
                mpMSVC.Bind();
                if (_Mode == OperationMode.Add && File.Exists(tempFileName))
                {
                    CalculateTotals();
                    mpMSVC.Rows.Add();
                    mcbCreditor.Focus();
                }
                mpMSVC.ClearSelection();
                // GotoLastRow();
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void FormatGrids()
        {
            //mpMSVC.NumericColumnNames.Add("Col_Quantity");
          //  mpMSVC.NumericColumnNames.Add("Col_Quantity");
            //mpMSVC.DoubleColumnNames.Add("Col_MRP");
            //mpMSVC.NumericColumnNames.Add("Col_Quantity");
            //mpMSVC.DoubleColumnNames.Add("Col_VATPer");
            //mpMSVC.DoubleColumnNames.Add("Col_VAT");
            //mpMSVC.DoubleColumnNames.Add("Col_PurchaseRate");
            //mpMSVC.DoubleColumnNames.Add("Col_Amount");
            //mpMSVC.DoubleColumnNames.Add("Col_SaleRate");
            //mpMSVC.DoubleColumnNames.Add("Col_TradeRate");
            //mpMSVC.DoubleColumnNames.Add("Col_Amount");
        }

        private void GotoLastRow()
        {
            if (mpMSVC.Rows.Count > 1)
            {
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    if (dr.Cells[1].Value == null || dr.Cells[1].Value.ToString() == string.Empty)
                        mpMSVC.SetFocus(dr.Index, 1);

                }
            }

        }
        #endregion

        # region fill or clean

        private void BindTempGrid()
        {
            try
            {
                ConstructTempGridColumns();
                DataTable tmptable = new DataTable();
                tmptable = _SSSale.ReadProductDetailsByID();
                _BindingSource = tmptable;
                dgtemp.DataSource = _BindingSource;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void BindPaymentDetails()
        {
            try
            {
                ConstructPaymentDetailsColumns();
                DataTable tmptable = new DataTable();
                tmptable = _SSSale.ReadPaymentDetailsByID();
                _PaymentDetailsBindingSource = tmptable;
                dgPaymentDetails.DataSource = _PaymentDetailsBindingSource;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ClearControls()
        {
            try
            {
                ConstructMainColumns();
                if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit) && _SSSale.AddNewRowCheck(mpMSVC))
                    mpMSVC.Rows.Add();
                _SchemeData = null;
                btnSummary.BackColor = Color.Linen;
                txtVouchernumber.Clear();
                tsBtnSavenPrint.Enabled = false;
                datePickerBillDate.ResetText();
                txtExpiry.BackColor = Color.White;
                datePickerChqDate.ResetText();
                txtChequeNumber.Clear();
                txtNarration.Text = "";
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtQuantity.Text = "0";
                txtSchemeQuantity.Text = "0";
                txtReplacement.Text = "0";
                txtBillAmountS.Text = "0.00";
                txtSchemeDiscountS.Text = "0.00";
                txtItemDiscountS.Text = "0.00";
                txtSplDiscountS.Text = "0.00";
                txtSplDiscountPerUnit.Text = "";
                txtSplDiscPerS.Text = "";
                txtAddOnS.Text = "0.00";
                txtCRAmountS.Text = "0.00";
                txtDBAmountS.Text = "0.00";
                txtCashDiscountPerS.Text = "0.00";
                txtCashDiscountAmountS.Text = "0.00";
                txtPreCashDiscountAmountS.Text = "0.00";
                txtVAT5AmountS.Text = "0.00";
                txtVAT12Point5AmountS.Text = "0.00";
                txtViewVat5per.Text = "0.00";
                txtViewVat12point5per.Text = "0.00";
                txtTotalVATAmountS.Text = "0.00";
                txtOCTPerS.Text = "0.00";
                txtOCTAmountS.Text = "0.00";
                txtPurchaseRate.Text = "0.00";
                txtDiscountPer.Text = "0.00";
                txtDiscountAmt.Text = "0.00";
                txtRoundUPS.Text = "0.00";
                txtMasterVATAmt.Text = "0.00";
                txtMasterVATPer.Text = "0.00";
                txtBillAmount.Text = "0.00";
                txtpuramount12point5.Text = "0.00";
                txtpuramount5.Text = "0.00";
                txtpuramount0.Text = "0.00";
                txtOCTPerS.Text = "0.00";
                mcbCreditor.SelectedID = "";
                txtChequeNumber.Text = "";
                txtStockID.Text = "";
                mcbBank.SelectedID = "";
                txtPendingBalance.Text = "0.00";
                txtGridAmountTot.Text = "0.00";
                pnlBillDetails.Enabled = true;
                pnlVou.Enabled = true;
                mpMSVC.Rows.Clear();
                psLableWithBorder1.Text = "";
                mpMSVC.Enabled = true;
                dgvLastPurchase.Visible = false;
                lblFooterMessage.Text = "";
                btnTypeChange.Visible = false;
                cbNewTransactionType.Visible = false;
                FixVoucherTypeBycbTransactionType();
                cbTransactionType.Focus();
                pnlScheme.Visible = false;
                DataTable dtp = new DataTable();
                if (dgPaymentDetails.Rows.Count > 0)
                {
                    dgPaymentDetails.DataSource = dtp;

                }
                if (General.CurrentSetting.MsetScanBarCode == "Y")
                {
                    if (_Mode == OperationMode.View)
                        btnPrintBarCode.Visible = true;
                    else
                        btnPrintBarCode.Visible = false;
                }
                else
                    btnPrintBarCode.Visible = false;
                _SSSale.SaleSubType = "W";



            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillBankCombo()
        {
            mcbBank.SelectedID = null;
            mcbBank.SourceDataString = new string[2] { "AccountID", "AccName" };
            mcbBank.ColumnWidth = new string[2] { "0", "200" };
            mcbBank.ValueColumnNo = 0;
            mcbCreditor.UserControlToShow = new UclAccount();
            Account _Bank = new Account();
            DataTable dbanktable = _Bank.GetSSAccountHoldersList(FixAccounts.AccCodeForBank);
            mcbBank.FillData(dbanktable);
        }
        private void FillTransactionType()
        {
            cbTransactionType.Items.Clear();
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
        }
        private void InitializeScreen()
        {
            try
            {
                mpMSVC.BringToFront();
                mpMSVC.Visible = true;
                mpMSVC.Dock = DockStyle.Fill;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void FillPartyCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[10] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccNameAddress", "AccTokenNumber", "AccDoctorID", "AccTransactionType", "AccDiscountOffered" };
                mcbCreditor.ColumnWidth = new string[10] { "0", "20", "200", "200", "0", "200", "0", "0", "0", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForDebtor);
                mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        //private void FillCreditDebitNote()
        //{
        //    bool retValue = false;
        //    DataTable dt = new DataTable();
        //    if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
        //    {
        //        try
        //        {
        //            ConstructCreditNoteColumns();
        //            dgCreditNote.DoubleColumnNames.Add("Col_AmountNet");
        //            CreditNoteStock crdb = new CreditNoteStock();

        //            dt = crdb.GetOverviewDataForDebtorSale(mcbCreditor.SelectedID, _SSSale.Id);
        //            if (dt != null)
        //                retValue = BindCreditNoteDebitNote(dt);

        //            //if (dt.Rows.Count > 0)
        //            //{
        //            //    btnIfDebitCredit.Visible = true;
        //            //    lblCreditNote.Visible = true;
        //            //    lblDebitNote.Visible = true;
        //            //    txtCreditNote.Visible = true;
        //            //    txtDebitNote.Visible = true;
        //            //}

        //        }
        //        catch (Exception Ex)
        //        {
        //            Log.WriteException(Ex);
        //        }

        //    }

        //}

        private bool FillLastPurchaseDataFromMasterProduct()
        {
            DataRow dr = null;
            DataRow invdr = null;
            string mbatchno = "";
            string mprodno = "";
            string mmrp = "";
            string mshelfcode = "";
            string mshelfID = "";
            int mprodclosingstock = 0;
            string mexpirydate = "";
            string mexpiry = "";
            double mmrpn = 0.00;
            double mpurrate = 0.00;
            double mtraderate = 0.00;
            double msalerate = 0.00;
            double mcstper = 0.00;
            double mcstamt = 0.00;
            double mscmamt = 0.00;
            double mscmper = 0.00;
            double mpurvatper = 0.00;
            double mpurvatamt = 0.00;
            double mprodvatper = 0.00;
            double mprodvatamt = 0.00;
            double mitemdiscper = 0.00;
            double mitemdiscamt = 0.00;
            string mlastpurchasestockid = "";

            try
            {
                Product drprod = new Product();
                dr = drprod.ReadLastPurchaseByID(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString());
                mprodno = mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString();
                if (dr["ProdLastPurchaseStockID"] != DBNull.Value)
                    mlastpurchasestockid = dr["ProdLastPurchaseStockID"].ToString().Trim();
                if (dr["ProdLastPurchaseBatchNumber"] != DBNull.Value)
                    mbatchno = dr["ProdLastPurchaseBatchNumber"].ToString().Trim();
                if (dr["ProdLastPurchaseMRP"] != DBNull.Value)
                {
                    double.TryParse(dr["ProdLastPurchaseMRP"].ToString(), out mmrpn);
                    mmrp = dr["ProdLastPurchaseMRP"].ToString();
                    _SSSale.MRP = mmrpn;
                }
                if (dr["ProdClosingStock"] != DBNull.Value)
                    mprodclosingstock = Convert.ToInt32(dr["ProdClosingStock"].ToString().Trim());
                if (dr["ShelfCode"] != DBNull.Value)
                    mshelfcode = (dr["ShelfCode"].ToString().Trim());
                if (dr["ProdShelfID"] != DBNull.Value)
                    mshelfID = dr["ProdShelfID"].ToString().Trim();
                if (dr["ProdLastPurchaseExpiry"] != DBNull.Value)
                    mexpiry = dr["ProdLastPurchaseExpiry"].ToString().Trim();
                if (dr["ProdLastPurchaseExpiryDate"] != DBNull.Value)
                    mexpirydate = dr["ProdLastPurchaseExpiryDate"].ToString().Trim();
                if (dr["ProdLastPurchaseSaleRate"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseSaleRate"].ToString(), out msalerate);
                if (dr["ProdLastPurchaseTradeRate"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseTradeRate"].ToString(), out mtraderate);
                if (dr["ProdLastPurchaseRate"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseRate"].ToString(), out mpurrate);
                if (dr["ProdLastPurchaseCST"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseCST"].ToString(), out mcstamt);
                if (dr["ProdLastPurchaseCSTPer"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseCSTPer"].ToString(), out mcstper);
                if (dr["ProdLastPurchaseSCMPer"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseSCMPer"].ToString(), out mscmper);
                if (dr["ProdLastPurchaseSCM"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseSCM"].ToString(), out mscmamt);
                if (dr["ProdLastPurchaseVATPer"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseVATPer"].ToString(), out mpurvatper);
                if (dr["ProdVATPercent"] != DBNull.Value)
                    double.TryParse(dr["ProdVATPercent"].ToString(), out mprodvatper);
                mpurvatper = mprodvatper;
                mpurvatamt = Math.Round((mtraderate * mpurvatper) / 100, 2);
                mprodvatamt = Math.Round((mmrpn * mprodvatper) / 100, 2);
                if (dr["ProdLastPurchaseItemDiscPer"] != DBNull.Value)
                    double.TryParse(dr["ProdLastPurchaseItemDiscPer"].ToString(), out mitemdiscper);
                if (mitemdiscper > 0)
                    mitemdiscamt = Math.Round((mtraderate * mitemdiscper) / 100, 4);

                if (dr["ProdLastPurchaseStockID"] != DBNull.Value)
                {
                    _LastStockID = dr["ProdLastPurchaseStockID"].ToString();
                    txtStockID.Text = dr["ProdLastPurchaseStockID"].ToString();
                }

                txtBatch.Text = mbatchno;
                txtExpiry.Text = mexpiry;
                if (mexpiry != string.Empty)
                {
                    mexpiry = General.GetValidExpiryDate(mexpiry);
                    txtExpiryDate.Text = General.GetDateInShortDateFormat(mexpirydate);
                }
                txtTradeRate.Text = mtraderate.ToString("#0.00");
                txtDiscountPer.Text = Convert.ToString(mitemdiscper.ToString("#0.00")).Trim();
                txtDiscountAmt.Text = Convert.ToString(mitemdiscamt.ToString("#0.0000")).Trim();
                txtPurchaseRate.Text = Convert.ToString(mpurrate.ToString("#0.00")).Trim();
                txtMRP.Text = Convert.ToString(mmrpn.ToString("#0.00")).Trim();
                txtSaleRate.Text = Convert.ToString(msalerate.ToString("#0.00")).Trim();
                txtMasterVATPer.Text = mprodvatper.ToString("#0.00");
                txtMasterVATAmt.Text = mprodvatamt.ToString("#0.0000");
                if (mpurvatper == 0)
                {
                    mpurvatper = mprodvatper;
                    mpurvatamt = mprodvatamt;
                }

                SsStock invss = new SsStock();
                invdr = invss.GetStockByStockID(mlastpurchasestockid);
                if (invdr != null)
                {
                    mexpiry = invdr["Expiry"].ToString().Trim();
                    mexpirydate = invdr["ExpiryDate"].ToString().Trim();
                    double.TryParse(invdr["MRP"].ToString().Trim(), out mmrpn);
                    double.TryParse(invdr["SaleRate"].ToString().Trim(), out msalerate);
                    double.TryParse(invdr["PurchaseRate"].ToString().Trim(), out mpurrate);
                    double.TryParse(invdr["TradeRate"].ToString().Trim(), out mtraderate);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        private bool FillDataFromMPSVRow()
        {
            DataRow invdr = null;
            string mbatchno = "";
            string mmrp = "";
            string mshelfcode = "";
            string mshelfID = "";
            int mqty = 0;
            int mrepl = 0;
            int mscm = 0;
            string mexpirydate = "";
            string mexpiry = "";
            double mmrpn = 0;
            double mpurrate = 0;
            double mtraderate = 0;
            double msalerate = 0;
            double mcstper = 0;
            double mcstamt = 0;
            double mscmamt = 0;
            double mscmper = 0;
            double mpurvatper = 0;
            double mpurvatamt = 0;
            double mprodvatper = 0;
            double mprodvatamt = 0;
            double mitemdiscper = 0;
            double mitemdiscamt = 0;
            double mamt = 0;
            string mstockid = "";
            try
            {
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfCode"].Value != null)
                    mshelfcode = mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfCode"].Value.ToString().Trim();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value != null)
                    mshelfID = mpMSVC.MainDataGridCurrentRow.Cells["Col_ShelfID"].Value.ToString().Trim();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    mqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value != null)
                    mscm = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Replacement"].Value != null)
                    mrepl = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Replacement"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                    mbatchno = mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                {
                    mmrpn = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
                    mmrp = mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString();
                }
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                    mexpiry = mpMSVC.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value != null)
                    mexpirydate = mpMSVC.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value.ToString();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value != null)
                    mpurrate = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value != null)
                    mtraderate = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null)
                    msalerate = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value != null)
                    mcstamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value.ToString() != "")
                    mcstper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTPer"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmountPerUnit"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmountPerUnit"].Value.ToString() != "")
                    mscmper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmountPerUnit"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmount"].Value != null)
                    mscmamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_VAT"].Value != null)
                    mpurvatper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_VAT"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value != null)
                    mprodvatper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value != null)
                    mitemdiscper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                {
                    mstockid = mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString();
                    _LastStockID = mstockid;
                }
                mpurvatper = mprodvatper;
                mpurvatamt = Math.Round((mmrpn * mpurvatper) / 100, 2); //4
                mprodvatamt = Math.Round((mmrpn * mprodvatper) / 100, 2); //4
                mitemdiscamt = Math.Round((mtraderate * mitemdiscper) / 100, 4); //4
                mamt = Math.Round((mtraderate * mqty), 2);

                txtQuantity.Text = mqty.ToString("#0");
                txtReplacement.Text = mrepl.ToString("#0");
                txtSchemeQuantity.Text = mscm.ToString("#0");
                txtAmount.Text = mamt.ToString("#0.00");
                txtBatch.Text = mbatchno;
                txtExpiry.Text = mexpiry;
                txtExpiryDate.Text = General.GetDateInShortDateFormat(mexpirydate);
                txtTradeRate.Text = mtraderate.ToString("#0.00");
                txtDiscountPer.Text = mitemdiscper.ToString("#0.00");
                txtDiscountAmt.Text = mitemdiscamt.ToString("#0.0000");
                txtPurchaseRate.Text = mpurrate.ToString("#0.00");
                txtMRP.Text = mmrpn.ToString("#0.00");
                txtSaleRate.Text = msalerate.ToString("#0.00");
                txtMasterVATPer.Text = mprodvatper.ToString("#0.00");
                txtMasterVATAmt.Text = mprodvatamt.ToString("#0.00");
                txtSchemeAmount.Text = mscmamt.ToString("#0.00");
               // txtSchemePer.Text = mscmper.ToString("#0.00");
                txtStockID.Text = mstockid;

                SsStock invss = new SsStock();
                invdr = invss.GetStockByProductIDAndBatchNumberAndMRP(_SSSale.ProductID, mbatchno, mmrp);

                if (invdr != null)
                {
                    mexpiry = invdr["Expiry"].ToString().Trim();
                    mexpirydate = invdr["ExpiryDate"].ToString().Trim();
                    double.TryParse(invdr["MRP"].ToString().Trim(), out mmrpn);
                    double.TryParse(invdr["SaleRate"].ToString().Trim(), out msalerate);
                    double.TryParse(invdr["PurchaseRate"].ToString().Trim(), out mpurrate);
                    double.TryParse(invdr["TradeRate"].ToString().Trim(), out mtraderate);
                }
                IfEditPreviousRow = "N";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        private void FillBatchGrid()
        {
            DataTable dt = new DataTable();
            SsStock invss = new SsStock();
            try
            {
                dt = invss.GetStockByProductIDForDistributorSale(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString());
                dgvBatchGrid.DataSource = dt;
                if (dgvBatchGrid.Rows.Count > 0 )
                {
                    foreach (DataGridViewRow row in dgvBatchGrid.Rows)
                    {
                        if (row.Cells["Col_StockID"].Value.ToString() == _LastStockID)
                        {
                            row.Selected = true;
                            dgvBatchGrid.CurrentCell = dgvBatchGrid.Rows[row.Index].Cells["Col_Batchno"];
                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.FillBatchGrid>>" + Ex.Message);
            }
        }
        //private void CreateNewProduct()
        //{
        //    Product pp = new Product();
        //}
        private void ClearpnlProductDetail()
        {
            try
            {
                txtQuantity.Text = "0";
                txtSchemeQuantity.Text = "0";
                txtReplacement.Text = "0";
                txtBatch.Text = "";
                txtExpiry.Text = "";
                txtMRP.Text = "0.00";
                txtTradeRate.Text = "0.00";
                txtDiscountAmt.Text = "0.00";
                txtDiscountPer.Text = "0.00";
                txtExpiryDate.Text = "";
                txtCSTAmount.Text = "0.00";
                txtCSTPer.Text = "0.00";
                txtPurchaseRate.Text = "0.00";
                txtSaleRate.Text = "0.00";
                txtAmount.Text = "0.00";
                txtSchemeAmount.Text = "0.00";
               // txtSchemePer.Text = "0.00";
                txtMasterVATAmt.Text = "0.00";
                txtMasterVATPer.Text = "0.00";
                txtCashDisountPerUnit.Text = "0.00";
                txtSplDiscountPerUnit.Text = "0.00";
                txtDistRatePercent.Text = "0.00";
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.ClearpnlProductDetail>>" + Ex.Message);
            }
        }

        private DataTable FillLastPurchase()
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            try
            {
                ConstructLastPurchaseColumns();
                Purchase lastpur = new Purchase();
                dt = lastpur.GetOverviewDataForLastPurchase(_SSSale.ProductID);
                if (dt != null && dt.Rows.Count > 0)
                    retValue = BindLastPurchase(dt);

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.FillCreditDebitNote>>" + Ex.Message);
            }
            return dt;
        }
        private bool BindCreditNoteDebitNote(DataTable dt)
        {
            bool retValue = true;
            try
            {

                if (dgCreditNote != null)
                    dgCreditNote.Rows.Clear();
                int _RowIndex = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    _RowIndex = dgCreditNote.Rows.Add();
                    string voudt = "";
                    double amtclear = 0;
                    DataGridViewRow currentdr = dgCreditNote.Rows[_RowIndex];
                    currentdr.Cells["Col_CrdbID"].Value = dr["CRDBID"].ToString();
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;
                    currentdr.Cells["Col_AmountNet"].Value = dr["AmountNet"].ToString();
                    currentdr.Cells["Col_Narr"].Value = dr["Narration"].ToString();
                    if (dr["AmountClear"] != DBNull.Value && dr["AmountClear"].ToString() != "")
                        double.TryParse(dr["AmountClear"].ToString(), out amtclear);
                    currentdr.Cells["Col_AmountClear"].Value = dr["AmountClear"].ToString();
                    if (_Mode == OperationMode.Delete)
                        currentdr.Cells["Col_Check"].Value = false;
                    else if (amtclear != 0)
                        currentdr.Cells["Col_Check"].Value = true;
                    _RowIndex += 1;

                }

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;
        }

        private bool BindLastPurchase(DataTable dt)
        {
            bool retValue = true;
            try
            {

                if (dgvLastPurchase != null)
                    dgvLastPurchase.Rows.Clear();
                int _RowIndex = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    _RowIndex = dgvLastPurchase.Rows.Add();
                    string voudt = "";
                    double amtclear = 0;
                    double mmargin = 0;
                    int mqty = 0;
                    int mscm = 0;
                    DataGridViewRow currentdr = dgvLastPurchase.Rows[_RowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;

                    currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();
                    amtclear = 0;
                    if (dr["MRP"] != DBNull.Value)
                        double.TryParse(dr["MRP"].ToString(), out amtclear);
                    currentdr.Cells["Col_MRP"].Value = amtclear.ToString("#0.00");
                    amtclear = 0;
                    if (dr["PurchaseRate"] != DBNull.Value)
                        double.TryParse(dr["PurchaseRate"].ToString(), out amtclear);
                    currentdr.Cells["Col_PurchaseRate"].Value = amtclear.ToString("#0.00");
                    mqty = 0;
                    mscm = 0;
                    if (dr["Quantity"] != DBNull.Value)
                        int.TryParse(dr["Quantity"].ToString(), out mqty);
                    if (dr["SchemeQuantity"] != DBNull.Value)
                        int.TryParse(dr["SchemeQuantity"].ToString(), out mscm);
                    string scm = mqty.ToString() + "+" + mscm.ToString();
                    currentdr.Cells["Col_Scheme"].Value = scm;
                    if (dr["MarginAfterDiscount"] != DBNull.Value)
                        double.TryParse(dr["MarginAfterDiscount"].ToString(), out mmargin);
                    currentdr.Cells["Col_Margin"].Value = mmargin.ToString("#0.00");
                    currentdr.Cells["Col_PartyName"].Value = dr["AccName"].ToString();
                    _RowIndex += 1;

                }

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;
            }
            return retValue;
        }



        #endregion

        #region keydown-Click-DoubleClick

        //private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            int mqty = 0;
        //            int.TryParse(txtQuantity.Text.ToString(), out mqty);
        //            int mrowcount = 0;
        //            mrowcount = dgvBatchGrid.RowCount;


        //            //}
        //            //else
        //            //{
        //            //    CalculatePurRateSaleRateAndAmount();
        //            //    lblFooterMessage.Text = "No Batch Data ";
        //            //    txtSchemeQuantity.Focus();
        //            //}
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPurchase.txtQuantity_KeyDown>>" + Ex.Message);
        //    }
        //}

        private void dgvBatchGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dgvBatchGridClick();
        }
        private void dgvBatchGrid_DoubleClick(object sender, EventArgs e)
        {
            dgvBatchGridClick();
        }

        private void dgvBatchGridClick()
        {
            double mtraderate = 0;
            double mcstper = 0;
            double mmstper = 0;
            double mqty = 0;
            double mmrp = 0;
            try
            {
                double.TryParse(txtQuantity.Text.ToString(), out mqty);
                dgvBatchGrid.Visible = false;
                dgvBatchGrid.SendToBack();
                pnlProductDetail.BringToFront();
                pnlProductDetail.Visible = true;
                pnlProductDetail1.Visible = true;
                pnlProductDetail2.Visible = true;
                pnlProductDetail.Enabled = true;
                mpMSVC.Enabled = true;
               
                if (dgvBatchGrid.CurrentRow.Cells["Col_StockID"].Value != null)
                    txtStockID.Text = dgvBatchGrid.CurrentRow.Cells["Col_StockID"].Value.ToString();
                if (dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value != null)
                    txtBatch.Text = dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value.ToString();
                if (dgvBatchGrid.CurrentRow.Cells["Col_Expiry"].Value != null)
                    txtExpiry.Text = dgvBatchGrid.CurrentRow.Cells["Col_Expiry"].Value.ToString();
                _SSSale.CurrentBatchStock = 0;
                if (dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value != null && dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value.ToString() != string.Empty)
                _SSSale.CurrentBatchStock = Convert.ToInt32(Math.Truncate(Convert.ToDouble(dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value)));
                lblFooterMessage.Text = "Product Stock : " + _SSSale.CurrentProductStock.ToString("#0") + " Batch Stock : " + _SSSale.CurrentBatchStock.ToString("#0");
                if (dgvBatchGrid.CurrentRow.Cells["Col_MRP"].Value != null)
                {
                    double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
                }
                txtMRP.Text = mmrp.ToString("#0.00");
                if (dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value != null)
                {

                    double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                    txtTradeRate.Text = mtraderate.ToString("#0.00");
                }
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value != null)
                {

                    double.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value.ToString(), out mmstper);
                    txtMasterVATPer.Text = mmstper.ToString("#0.00");
                    txtMasterVATAmt.Text = Math.Round(mtraderate * mmstper / 100, 2).ToString("#0.00");
                }
                else
                {
                    txtMasterVATPer.Text = "0.00";
                    txtMasterVATAmt.Text = Math.Round(mtraderate * mmstper / 100, 2).ToString("#0.00");
                }
                if (dgvBatchGrid.CurrentRow.Cells["Col_PurchaseRate"].Value != null)
                    txtPurchaseRate.Text = dgvBatchGrid.CurrentRow.Cells["Col_PurchaseRate"].Value.ToString();
                if (dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value != null)
                {
                    double sr = Convert.ToDouble(dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value.ToString());
                    txtSaleRate.Text = sr.ToString("#0.00");
                }
                if (dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value != null)
                {
                    txtCSTPer.Text = dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value.ToString();
                    double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value.ToString(), out mcstper);
                    txtCSTAmount.Text = Math.Round(mtraderate * mcstper / 100, 2).ToString("#0.00");
                }
                pnlProductDetail.Enabled = true;
                txtAmount.Text = Math.Round(mtraderate * mqty).ToString("#0.00");
                txtQuantity.Focus();
               // CalculatePurRateSaleRateAndAmount();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.dgvBatchGridClick>>" + Ex.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancelClick();
        }
        private void btnCancel_KeyDown(object sender, KeyEventArgs e)
        {
            btnCancelClick();
        }

        public void btnCancelClick()
        {
            lblExpired.Text = "";
            double mamt = 0;
            btnOK.Enabled = true;
            btnSummary.Enabled = true;
            btnCancel.BackColor = Color.White;
            try
            {
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null)
                    double.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString(), out mamt);
                mpMSVC.Enabled = true;
                pnlProductDetail.Enabled = true;
                pnlProductDetail.SendToBack();
                pnlProductDetail.Visible = false;
                dgvBatchGrid.Visible = false;
                dgvLastPurchase.Visible = false;
                ClearpnlProductDetail();
                pnlBillDetails.Enabled = true;
                if (mamt == 0)
                {
                    mpMSVC.Rows.Remove(mpMSVC.MainDataGridCurrentRow);
                    int curro = mpMSVC.Rows.Add();
                    mpMSVC.SetFocus(curro, 1);

                }
                else
                    mpMSVC.SetFocus(1);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCancel_KeyDown>>" + Ex.Message);
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            ButtonOKClick();
        }

        private bool ValidationForOK()
        {
            bool retValue = false;
            lblFooterMessage.Text = "";
            double mamt = 0;
            int mqty = 0;
            int mscm = 0;
            int mrepl = 0;
            double mmrp = 0;
            double mtrate = 0;
            double msalerate = 0;
            double mvatamt = 0;
            string mbatch = "";
            string mexp = "";
            string mexpdate = "";

            try
            {               
                mamt = Convert.ToDouble(txtAmount.Text.ToString());
                mqty = Convert.ToInt32(txtQuantity.Text.ToString());
                mscm = Convert.ToInt32(txtSchemeQuantity.Text.ToString());
                mrepl = Convert.ToInt32(txtReplacement.Text.ToString());
                mmrp = Convert.ToDouble(txtMRP.Text.ToString());
                mtrate = Convert.ToDouble(txtTradeRate.Text.ToString());
                msalerate = Convert.ToDouble(txtSaleRate.Text.ToString());
                mbatch = txtBatch.Text.ToString();
                mexp = txtExpiry.Text;
                mexpdate = General.GetValidExpiryDate(mexp);

                if (mmrp > 0 && mtrate > 0)
                {
                    if ((mqty > 0 && mamt > 0))
                    {
                        if (mamt > 0 || (mamt == 0 && (mscm > 0 || mrepl > 0)))
                        {
                            if (mmrp > mtrate)
                            {
                                if (msalerate < (mtrate + mvatamt))
                               
                                    lblFooterMessage.Text = "Sale Rate > Trade Rate + Vat Amount";
                            }
                            else
                                lblFooterMessage.Text = "Mrp > Trade Rate";
                        }
                        else
                            lblFooterMessage.Text = "Please Check Quantity,Scheme,Replacement";
                    }
                    else
                        lblFooterMessage.Text = "Please Check Quantity";

                }

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.ValidationForOK>>" + Ex.Message);
            }
            return retValue;

        }

        private void ButtonOKClick()
        {
            bool ifok = ValidationForOK();
            ifok = true;
            lblFooterMessage.Text = "";
            lblExpired.Text = "";
            pnlBillDetails.Enabled = true;
            try
            {
                if (ifok)
                {
                    CalculateRowAmount();
                    pnlProductDetail.SendToBack();
                    pnlProductDetail.Visible = false;
                    dgvLastPurchase.Visible = false;
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = txtQuantity.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = txtBatch.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = txtExpiry.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = txtExpiryDate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = txtTradeRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value = txtMRP.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value = txtPurchaseRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = txtSaleRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value = txtSchemeQuantity.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Replacement"].Value = txtReplacement.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value = txtDiscountPer.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value = txtAmount.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountAmount"].Value = txtDiscountAmt.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmount"].Value = txtSchemeAmount.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value = txtCSTAmount.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProdVATPer"].Value = txtMasterVATPer.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_VATAmountSale"].Value = txtMasterVATAmt.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_SpldiscountPer"].Value = txtSplDiscPerS.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_SplDiscountAmount"].Value = txtSplDiscountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_CashDiscountAmount"].Value = txtCashDisountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Margin"].Value = txtMargin.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Margin2"].Value = txtMargin2.Text.ToString();
                    // mpMSVC.MainDataGridCurrentRow.Cells["Col_SplDiscountAmount"].Value = txtSplDiscountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value = txtStockID.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRatePer"].Value = txtDistRatePercent.Text.ToString();

                    ClearpnlProductDetail();
                    mpMSVC.Enabled = true;
                    int pp = mpMSVC.MainDataGridCurrentRow.Index;
                    if (IfEditPreviousRow == "Y")
                    {
                        if (mpMSVC.Rows.Count > mpMSVC.SelectedRow.Index + 1)
                            mpMSVC.SetFocus(mpMSVC.SelectedRow.Index + 1, 1);
                    }

                    if (IfEditPreviousRow == "N")
                    {
                        int rowcnt = mpMSVC.Rows.Count;
                        int rowID = mpMSVC.MainDataGridCurrentRow.Index + 1;
                        if (rowID >= rowcnt && mpMSVC.Rows[rowcnt - 1].Cells[0].Value != null)
                        {
                            rowID = mpMSVC.Rows.Add();
                        }
                        mpMSVC.SetFocus(rowID, 1);
                    }
                    else
                    {
                        int rowcnt = mpMSVC.Rows.Count;
                        int rowID = mpMSVC.MainDataGridCurrentRow.Index + 1;
                        if (rowID >= rowcnt && mpMSVC.Rows[rowcnt - 1].Cells[0].Value != null)
                        {
                            rowID = mpMSVC.Rows.Add();
                        }
                        mpMSVC.SetFocus(rowID, 1);
                    }
                    if (_Mode == OperationMode.Add)
                    {
                        DataTable dt = mpMSVC.GetGridData();
                        if (dt.Rows.Count > 0)
                            dt.WriteXml(General.GetPurchaseTempFile());
                    }
                    CalculateTotals();
                    btnOK.Enabled = true;
                }
                else
                    btnOK.Enabled = false;

            }

            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.ButtonOKClick>>" + Ex.Message);
            }
        }
        private void btnOK_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ButtonOKClick();

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnOK_KeyDown>>" + Ex.Message);
            }
        }
        private void txtSchemeQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    if (txtReplacement.Visible == true)
                        txtReplacement.Focus();
                    else
                        txtBatch.Focus();
                else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                    txtQuantity.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtSchemeQuantity_KeyDown>>" + Ex.Message);
            }
        }
        private void txtReplacement_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    txtBatch.Focus();
                else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                    txtSchemeQuantity.Focus();
                else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.O)
                {
                    btnOK.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtReplacement_KeyDown>>" + Ex.Message);
            }
        }
        private void txtBatch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                    txtExpiry.Focus();
                else if (e.KeyCode == Keys.Up)
                {
                    if (txtReplacement.Visible == true)
                        txtReplacement.Focus();
                    else
                        txtQuantity.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtBatch_KeyDown>>" + Ex.Message);
            }
        }

       
        //private void txtCSTPer_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
        //        {
        //            CalculatePurRateSaleRateAndAmount();
        //            txtSchemeAmount.Focus();
        //        }
        //        else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
        //            txtMRP.Focus();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPurchase.txtCSTPer_KeyDown>>" + Ex.Message);
        //    }
        //}
        //private void txtCSTAmount_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
        //        {
        //            CalculatePurRateSaleRateAndAmount();
        //            txtTradeRate.Focus();
        //        }
        //        else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
        //            txtCSTPer.Focus();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPurchase.txtCSTAmount_KeyDown>>" + Ex.Message);
        //    }
        //}
        //private void txtTradeRate_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //            txtTradeRateValidating();
        //        if (e.KeyCode == Keys.Right)
        //            txtSchemeAmount.Focus();
        //        else if (e.KeyCode == Keys.Up)
        //            txtMRP.Focus();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
       
        private void txtDiscountPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtDiscountAmt.Text = "0.00";
                CalculateRowAmount(); ;
                btnOK.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtSaleRate.Focus();
            
        }        


        private void mpMSVC_OnTABKeyPressed(object sender, EventArgs e)
        {
            btnSummary.BackColor = General.ControlFocusColor;
            btnSummary.Focus();
        }
        private void txtBatch_Validating(object sender, CancelEventArgs e)
        {
            if ((txtBatch.Text.ToString() == null || txtBatch.Text.ToString() == ""))
                txtBatch.Focus();
            else
                btnOK.Enabled = true;
        }
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }

        private void mpMSVC_OnRowDeleted(object sender, EventArgs e)
        {
            try
            {
                CalculateTotals();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.mpMSVC_OnRowDeleted>>" + Ex.Message);
            }
        }

        #endregion

        #region Calculate Amounts Rates
        private void CalculateRowAmount()
        {

            double mmrp = 0;
            double mgridamttot = 0;
            try
            {
                double.TryParse(txtMRP.Text.ToString(), out mmrp);
                double.TryParse(txtGridAmountTot.Text.ToString(), out mgridamttot);
                if (mmrp > 0 || mgridamttot > 0)
                {
                    double mprate = 0;
                    double mtraderate = 0;
                    double mcstamt = 0;
                    double mmstamtbySale = 0;
                    double mqty = 0;
                    double mscmqty = 0;                   
                    double mscmamt = 0;
                    double mitemdiscper = 0;
                    double mitemdiscamt = 0;
                    double mtraderateafterscm = 0;
                    double mcashdiscper = 0;
                    _SSSale.CrdbDiscAmt = 0;
                    //_SSSale.AmountSplDiscountPerUnit = 0;
                    //_SSSale.SchemeDiscountPercent = 0;
                    //_SSSale.AmountScmDiscountPerUnit = 0;
                    //_SSSale.AmountSchemeDiscount = 0;
                    double mspldiscper = 0;
                    double mspldiscamt = 0;
                    double moctper = 0;
                    double moctamt = 0;
                    double msalerate = 0;
                    double mpurvatper = 0;
                    double msalevatper = 0;
                    double msalevatamt = 0;
                    double mamt = 0;
                    double mamtzerovat = 0;
                    double mskl = 0;

                    double.TryParse(txtQuantity.Text.ToString(), out mqty);
                    double.TryParse(txtTradeRate.Text.ToString(), out mtraderate);
                    double.TryParse(txtDiscountPer.Text.ToString(), out mitemdiscper);
                  //  double.TryParse(txtSchemePer.Text.ToString(), out mscmdiscper);
                    double.TryParse(txtSplDiscPerS.Text.ToString(), out mspldiscper);
                    double.TryParse(txtMasterVATPer.Text.ToString(), out msalevatper);
                    double.TryParse(txtOCTPerS.Text.ToString(), out moctper);
                    double.TryParse(txtCashDiscountPerS.Text.ToString(), out mcashdiscper);
                    double.TryParse(txtSchemeAmount.Text.ToString(), out mscmamt);
                    double.TryParse(txtOCTAmountS.Text.ToString(), out moctamt);
                    double.TryParse(txtSplDiscountS.Text.ToString(), out mspldiscamt);
                    double.TryParse(txtDiscountAmt.Text.ToString(), out mitemdiscamt);
                    mamt = Math.Round(mqty * mtraderate, 2); //4
                    mskl = Math.Round(mamt - mscmamt, 2); //4
                    //_SSSale.AmountSchemeDiscount = mscmamt;
                    //_SSSale.SchemeDiscountPercent = mscmdiscper;
                    if (mqty > 0)
                    {
                        //  if (mitemdiscamt > 0)
                        //     mitemdiscper = Math.Round((mitemdiscamt * 100 * mqty) / mskl, 2);
                        //   txtDiscountPer.Text = mitemdiscper.ToString("#0.00");

                        mitemdiscamt = Math.Round((((mskl) * mitemdiscper / 100) / mqty), 4); //4
                        mspldiscper = Math.Round((100 * mspldiscamt) / (mamt - mitemdiscamt - mscmamt - moctamt), 2); //4
                        //_SSSale.AmountScmDiscountPerUnit = Math.Round(_SSSale.AmountSchemeDiscount / mqty, 2); //4
                        //_SSSale.AmountSplDiscountPerUnit = Math.Round((((mskl - mitemdiscamt) * mspldiscper) / 100) / mqty, 2); //4
                        //_SSSale.AmountCashDiscountPerUnit = Math.Round((((mskl - _SSSale.AmountSplDiscountPerUnit - mitemdiscamt) * mcashdiscper) / 100) / mqty, 2); //4
                    }
                    double.TryParse(txtCSTAmount.Text.ToString(), out mcstamt);
                    double.TryParse(txtSchemeQuantity.Text.ToString(), out mscmqty);
                    //if (mqty > 0)
                    //    mpurvatamt = Math.Round(((mamt - moctamt - _SSSale.AmountCashDiscountPerUnit - mspldiscamt - mscmamt - mitemdiscamt) / mqty) * mpurvatper / 100, 2); //4

                    msalevatamt = Math.Round((mmrp * msalevatper) / 100, 2); //4                    
                    if ((mqty + mscmqty) > 0)
                        mtraderateafterscm = Math.Round((mtraderate * mqty) / (mqty + mscmqty), 2); //4
                    //if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                    //    mprate = mtraderateafterscm + mpurvatamt + mcstamt - _SSSale.AmountScmDiscountPerUnit - mitemdiscamt - _SSSale.AmountCashDiscountPerUnit;
                    //else
                    //    mprate = mtraderateafterscm + mcstamt - _SSSale.AmountScmDiscountPerUnit - mitemdiscamt - _SSSale.AmountCashDiscountPerUnit;
                    if (General.CurrentSetting.MsetPurchaseAddVATInSaleRate == "Y")
                        msalerate = mmrp + mmstamtbySale + mcstamt;
                    else
                        msalerate = mmrp;
                    mamt = Math.Round(mqty * mtraderate, 2);
                    if (mpurvatper == 0)
                        mamtzerovat = mamt - mitemdiscamt - mscmamt;
                    else
                        mamtzerovat = 0;

                    txtDiscountAmt.Text = mitemdiscamt.ToString("#0.0000");
                    txtMasterVATAmt.Text = msalevatamt.ToString("#0.0000");
                    txtAmount.Text = mamt.ToString("#0.00");
                    txtSaleRate.Text = msalerate.ToString("#0.00");
                    //txtSplDiscountPerUnit.Text = _SSSale.AmountSplDiscountPerUnit.ToString("0.00");
                    //txtCashDisountPerUnit.Text = _SSSale.AmountCashDiscountPerUnit.ToString("0.0000");
                    txtSplDiscPerS.Text = mspldiscper.ToString("#0.0000");
                    //_SSSale.SpecialDiscountPercentS = mspldiscper;
                    txtPurchaseRate.Text = mprate.ToString("#0.00");
                    CalculateTotals();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculatePurRateSaleRateAndAmount>>" + Ex.Message);
            }
        }

        private void CalculatePurRateSaleRateAmountforFullGrid()
        {
            try
            {
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {

                    double mprate = 0;
                    double mtraderate = 0;
                    double mpurvatamt = 0;
                    double mcstamt = 0;
                    double mmstamtbySale = 0;
                    double mqty = 0;
                    double mscmqty = 0;
                    double mscmdiscper = 0;
                    double mscmamt = 0;
                    double mitemdiscper = 0;
                    double mitemdiscamt = 0;
                    double mtraderateafterscm = 0;
                    double mcashdiscper = 0;
                    //_SSSale.AmountCashDiscountPerUnit = 0;
                    //_SSSale.AmountSplDiscountPerUnit = 0;
                    //_SSSale.SchemeDiscountPercent = 0;
                    //_SSSale.AmountScmDiscountPerUnit = 0;
                    //_SSSale.AmountSchemeDiscount = 0;
                    double mspldiscper = 0;
                    double mspldiscamt = 0;
                    double moctamt = 0;
                    double msalerate = 0;
                    double mpurvatper = 0;
                    double msalevatper = 0;
                    double msalevatamt = 0;
                    double mamt = 0;
                    double mamtzerovat = 0;
                    double mskl = 0;
                    double mmrp = 0;
                    double mmargin = 0;
                    double mmargin2 = 0;
                    double mpurrate = 0;
                    if (dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "")
                        mqty = Convert.ToInt16(dr.Cells["Col_Quantity"].Value.ToString());
                    if (mqty > 0)
                    {
                        double.TryParse(dr.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                        double.TryParse(dr.Cells["Col_MRP"].Value.ToString(), out mmrp);
                        double.TryParse(dr.Cells["Col_ItemDiscountPer"].Value.ToString(), out mitemdiscper);
                        if (dr.Cells["Col_ItemSCMDiscountAmountPerUnit"].Value != null)
                            double.TryParse(dr.Cells["Col_ItemSCMDiscountAmountPerUnit"].Value.ToString(), out mscmdiscper);
                        if (txtSplDiscPerS.Text != null && txtSplDiscountS.Text.ToString() != string.Empty)
                            double.TryParse(txtSplDiscPerS.Text.ToString(), out mspldiscper);
                        double.TryParse(dr.Cells["Col_VAT"].Value.ToString(), out mpurvatper);
                        double.TryParse(dr.Cells["Col_ProdVATPer"].Value.ToString(), out msalevatper);
                        double.TryParse(txtCashDiscountPerS.Text.ToString(), out mcashdiscper);
                        if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != string.Empty)
                            double.TryParse(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString(), out mscmamt);
                        double.TryParse(txtSplDiscountS.Text.ToString(), out mspldiscamt);
                        double.TryParse(dr.Cells["Col_PurchaseRate"].Value.ToString(), out mpurrate);
                        mamt = Math.Round(mqty * mtraderate, 2); //4
                        mskl = Math.Round(mamt - mscmamt, 2); //4
                        //_SSSale.AmountSchemeDiscount = mscmamt;
                        //_SSSale.SchemeDiscountPercent = mscmdiscper;

                        if (mqty > 0)
                        {
                            mitemdiscamt = Math.Round((((mskl) * mitemdiscper / 100) / mqty), 2); //4
                            mspldiscper = Math.Round((100 * mspldiscamt) / (mamt - mitemdiscamt - mscmamt - moctamt), 2); //4
                            //_SSSale.AmountScmDiscountPerUnit = Math.Round(_SSSale.AmountSchemeDiscount / mqty, 2); //4

                            //_SSSale.AmountSplDiscountPerUnit = Math.Round((((mskl - mitemdiscamt) * mspldiscper) / 100) / mqty, 2); //4
                            //_SSSale.AmountCashDiscountPerUnit = Math.Round((((mskl - _SSSale.AmountSplDiscountPerUnit - mitemdiscamt) * mcashdiscper) / 100) / mqty, 2); //4
                        }
                        double.TryParse(dr.Cells["Col_CSTAmount"].Value.ToString(), out mcstamt);
                        double.TryParse(dr.Cells["Col_Scheme"].Value.ToString(), out mscmqty);
                        //if (mqty > 0)
                        //    mpurvatamt = Math.Round(((mamt - moctamt - _SSSale.AmountCashDiscountPerUnit - mspldiscamt - mscmamt - mitemdiscamt) / mqty) * mpurvatper / 100, 2); //4

                        msalevatamt = Math.Round((mmrp * msalevatper) / 100, 2); //4
                        if ((mqty + mscmqty) > 0)
                            mtraderateafterscm = Math.Round((mtraderate * mqty) / (mqty + mscmqty), 2); //4
                        //if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                        //    mprate = mtraderateafterscm + mpurvatamt + mcstamt - _SSSale.AmountScmDiscountPerUnit - mitemdiscamt - _SSSale.AmountCashDiscountPerUnit;
                        //else
                        //    mprate = mtraderateafterscm + mcstamt - _SSSale.AmountScmDiscountPerUnit - mitemdiscamt - _SSSale.AmountCashDiscountPerUnit;
                        if (General.CurrentSetting.MsetPurchaseAddVATInSaleRate == "Y")
                            msalerate = mmrp + mmstamtbySale + mcstamt;
                        else
                            msalerate = mmrp;
                        mamt = Math.Round(mqty * mtraderate, 2);
                        if (mpurvatper == 0)
                            mamtzerovat = mamt - mitemdiscamt - mscmamt;
                        else
                            mamtzerovat = 0;

                        if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                        {
                            if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                            {
                                mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / (mtraderate + mpurvatamt), 2);
                                mmargin2 = Math.Round((msalerate - mprate) / mprate, 2);
                            }
                            else
                            {
                                mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / msalerate, 2);
                                mmargin2 = Math.Round((msalerate - mprate) / msalerate, 2);
                            }
                        }
                        else
                        {
                            if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                            {
                                mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / (mtraderate + mpurvatamt), 2);
                                mmargin2 = Math.Round((msalerate - mprate) / mprate, 2);
                            }
                            else
                            {
                                mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / msalerate, 2);
                                mmargin2 = Math.Round((msalerate - mprate) / msalerate, 2);
                            }
                        }
                        mmargin = Math.Round(mmargin * 100, 2);
                        mmargin2 = Math.Round(mmargin2 * 100, 2);


                        dr.Cells["Col_ItemDiscountAmount"].Value = mitemdiscamt.ToString("#0.0000");
                        dr.Cells["Col_ItemSCMDiscountAmount"].Value = mscmamt.ToString("#0.00");
                        dr.Cells["Col_VATAmountPurchase"].Value = mpurvatamt.ToString("#0.0000");
                        dr.Cells["Col_VATAmountSale"].Value = msalevatamt.ToString("#0.0000");
                        dr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                        dr.Cells["Col_SaleRate"].Value = msalerate.ToString("#0.00");
                        //dr.Cells["Col_SplDiscountPer"].Value = _SSSale.AmountSplDiscountPerUnit.ToString("0.00");
                        //dr.Cells["Col_CashDiscountAmount"].Value = _SSSale.AmountCashDiscountPerUnit.ToString("0.00");
                        dr.Cells["Col_PurchaseRate"].Value = mprate.ToString("#0.00");
                        dr.Cells["Col_Margin"].Value = mmargin.ToString("#0.00");
                        dr.Cells["Col_Margin2"].Value = mmargin2.ToString("#0.00");
                    }
                }
                CalculateTotals();

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculatePurRateSaleRateAmountforFullGrid>>" + Ex.Message);
            }
        }
        private void CalculateTotals()
        {
            // check for inpurstring not in correct format???

            double mtotamt = 0;
            double mamt = 0;
            int itemCount = 0;
            double mmargin = 0;
            double mmargin2 = 0;
            double mpurrate = 1;
            double msalerate = 1;
            double mvatamt = 0;
            double mtraterate = 0;
            double mmrp = 0;
            if (txtPurchaseRate.Text != null && txtPurchaseRate.Text.ToString() != string.Empty)
                double.TryParse(txtPurchaseRate.Text.ToString(), out mpurrate);
            if (txtSaleRate.Text != null && txtSaleRate.Text.ToString() != string.Empty)
                double.TryParse(txtSaleRate.Text.ToString(), out msalerate);
            if (txtTradeRate.Text != null && txtTradeRate.Text.ToString() != string.Empty)
                double.TryParse(txtTradeRate.Text.ToString(), out mtraterate);
            if (txtMRP.Text != null && txtMRP.Text != string.Empty)
                double.TryParse(txtMRP.Text.ToString(), out mmrp);
            try
            {
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    if (dr.Cells["Col_MRP"].Value != null && dr.Cells["Col_MRP"].Value.ToString().Trim() != "0.00" && dr.Cells["Col_MRP"].Value.ToString() != "")
                    {
                        itemCount += 1;
                        if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != string.Empty)
                        {
                            double.TryParse(dr.Cells["Col_Amount"].Value.ToString(), out mamt);
                            mtotamt += mamt;
                        }
                    }
                    txtGridAmountTot.Text = mtotamt.ToString("#0.00");
                    psLableWithBorder1.Text = itemCount.ToString().Trim();
                }
                if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                {
                    if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                    {
                        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (mtraterate + mvatamt), 2);
                        mmargin2 = Math.Round((msalerate - mpurrate) / mpurrate, 2);
                    }
                    else
                    {
                        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / msalerate, 2);
                        mmargin2 = Math.Round((msalerate - mpurrate) / msalerate, 2);
                    }
                }
                //else
                //{
                //    //if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                //    //{
                //    //    mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (mtraterate + mvatamt), 2);
                //    //    mmargin2 = Math.Round((msalerate - mpurrate) / mpurrate, 2);
                //    //}
                //    //else
                //    //{
                //    //    mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / msalerate, 2);
                //    //    mmargin2 = Math.Round((msalerate - mpurrate) / msalerate, 2);
                //    //}
                //}
                mmargin = Math.Round(mmargin * 100, 2);
                mmargin2 = Math.Round(mmargin2 * 100, 2);
                txtMargin.Text = mmargin.ToString("#0.00");
                txtMargin2.Text = mmargin2.ToString("#0.00");
                if (mtotamt > 0)
                    btnSummary.Enabled = true;
                else
                    btnSummary.Enabled = false;
                CalculateGetSummaryData();
                CalculateFinalVAT();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateTotals>>" + Ex.Message);
            }

        }

        private void CalculateFinalSummary()
        {
            //_SSSale.AmountBillS = Convert.ToDouble(txtBillAmountS.Text.ToString());
            //try
            //{

            //    if (_SSSale.AmountBillS > 0)
            //    {
            //        if (txtSchemeDiscountS.Text.ToString().Trim() != "")
            //            _SSSale.AmountSchemeDiscountS = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
            //        if (txtItemDiscountS.Text.ToString().Trim() != "")
            //            _SSSale.AmountItemDiscountS = Convert.ToDouble(txtItemDiscountS.Text.ToString());
            //        if (txtSplDiscountS.Text.ToString().Trim() != "")
            //            _SSSale.AmountSpecialDiscountS = Convert.ToDouble(txtSplDiscountS.Text.ToString());

            //        _SSSale.SpecialDiscountPercentS = Math.Round((100 * _SSSale.AmountSpecialDiscountS) / (_SSSale.AmountBillS - _SSSale.AmountItemDiscountS - _SSSale.AmountSchemeDiscountS - _SSSale.AmountOctroiS), 6);

            //        if (txtAddOnS.Text.ToString().Trim() != "")
            //            _SSSale.AmountAddOnFreightS = Convert.ToDouble(txtAddOnS.Text.ToString());
            //        if (txtCRAmountS.Text.ToString().Trim() != "")
            //            _SSSale.AmountCreditNoteS = Convert.ToDouble(txtCRAmountS.Text.ToString());
            //        if (txtDBAmountS.Text.ToString().Trim() != "")
            //            _SSSale.AmountDebitNoteS = Convert.ToDouble(txtDBAmountS.Text.ToString());
            //        if (txtCashDiscountPerS.Text.ToString().Trim() != "")
            //            _SSSale.CashDiscountPercentageS = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
            //        if (txtCashDiscountAmountS.Text.ToString().Trim() != "")
            //            _SSSale.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
            //        if (_SSSale.AmountBillS - _SSSale.AmountSpecialDiscountS - _SSSale.AmountSchemeDiscountS - _SSSale.AmountItemDiscountS + _SSSale.AmountCreditNoteS <= _SSSale.AmountDebitNoteS)
            //        {
            //            lblFooterMessage.Text = "Invalid Debit Note Amount";
            //            _SSSale.AmountDebitNoteS = 0;
            //            txtDBAmountS.Text = "0.00";
            //            ClearDebitCreditNoteWhenAmountIsLess();
            //        }
            //        if (_SSSale.AmountCashDiscountS > (_SSSale.AmountBillS - _SSSale.AmountSpecialDiscountS - _SSSale.AmountSchemeDiscountS - _SSSale.AmountItemDiscountS + _SSSale.AmountCreditNoteS - _SSSale.AmountDebitNoteS))
            //        {
            //            lblFooterMessage.Text = "Invalid Cash Discount";
            //            _SSSale.CashDiscountPercentageS = 0;
            //            _SSSale.AmountCashDiscountS = 0;
            //            txtCashDiscountAmountS.Text = "0.00";
            //            txtPreCashDiscountAmountS.Text = "0.00";
            //        }
            //        txtCashDiscountAmountS.Text = _SSSale.AmountCashDiscountS.ToString("#0.00");
            //        txtPreCashDiscountAmountS.Text = _SSSale.AmountCashDiscountS.ToString("#0.00");
            //        if (txtCashDiscountAmountS.Text.ToString().Trim() != "")
            //            _SSSale.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
            //        if (txtVAT5AmountS.Text.ToString().Trim() != "")
            //            _SSSale.AmountVAT5PercentS = Convert.ToDouble(txtVAT5AmountS.Text.ToString());
            //        if (txtVAT12Point5AmountS.Text.ToString().Trim() != "")
            //            _SSSale.AmountVAT12point5PercentS = Convert.ToDouble(txtVAT12Point5AmountS.Text.ToString());

            //        if (txtOCTPerS.Text.ToString().Trim() != "")
            //            _SSSale.OctroiPercentageS = Convert.ToDouble(txtOCTPerS.Text.ToString());
            //        if (_SSSale.OctroiPercentageS > 0)
            //            _SSSale.AmountOctroiS = Math.Round(_SSSale.TotalAmountForOctroiS * _SSSale.OctroiPercentageS / 100, 2);
            //        _SSSale.AmountS = Math.Round(_SSSale.AmountBillS - _SSSale.AmountSchemeDiscountS - _SSSale.AmountItemDiscountS
            //            - _SSSale.AmountSpecialDiscountS + _SSSale.AmountAddOnFreightS + _SSSale.AmountCreditNoteS
            //            - _SSSale.AmountDebitNoteS - _SSSale.AmountCashDiscountS + _SSSale.AmountVAT5PercentS
            //            + _SSSale.AmountVAT12point5PercentS + _SSSale.AmountOctroiS, 2);
            //        CalculateRoundup();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteError("UclPurchase.CalculateFinalSummary>>" + Ex.Message);
            //}

        }
        private void CalculateRoundup()
        {
            try
            {
                //txtTotalS.Text = _SSSale.AmountS.ToString("#0.00");
                //if (cbRound.Checked == true)
                //    _SSSale.RoundUpAmountS = Math.Round(_SSSale.AmountS, 0) - _SSSale.AmountS;
                //else
                //    _SSSale.RoundUpAmountS = 0;
                //_SSSale.AmountNetS = _SSSale.AmountS + _SSSale.RoundUpAmountS;
                //txtNetAmountS.Text = _SSSale.AmountNetS.ToString("#0.00");
                //txtBillAmount.Text = _SSSale.AmountNetS.ToString("#0.00");
                //txtRoundUPS.Text = _SSSale.RoundUpAmountS.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateRoundup>>" + Ex.Message);
            }
        }
        private void CalculateFinalVAT()
        {
            double mtotdisczero = 0;
            double mtotdisc5 = 0;
            double mtotdisc12point5 = 0;
            double mtotdiscother = 0;
            double mtotcashdiscount = 0;
            double mmstamt5 = 0;
            double mmstamt12point5 = 0;
            double mmstamtother = 0;
            double mtotmstzero = 0;
            double mtotmst5 = 0;
            double mtotmst12point5 = 0;
            double mtotmstother = 0;
            int mqty = 0;
            double mskl = 0;
            double mscmdisc = 0;
            double mitm = 0;
            double msplddx = 0;
            double mcrddx = 0;
            double mddx = 0;
            double mtt1 = 0;
            double mtt1S = 0;
            double mmstperpur = 0;

            double mpuramountzero = 0;
            //  double mpuramount0 = 0;
            double mpuramount5 = 0;
            double mpuramount12point5 = 0;
            double mamt = 0;
            double mtotalvat = 0;
            //if (txtCashDiscountPerS.Text != "")
            //    _SSSale.CashDiscountPercentageS = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
            try
            {

                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != string.Empty)
                    {
                        int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out mqty);
                        mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != string.Empty)
                            mscmdisc = Convert.ToDouble(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                        if (dr.Cells["Col_VAT"].Value != null && dr.Cells["Col_VAT"].Value.ToString() != "")
                            mmstperpur = Convert.ToDouble(dr.Cells["Col_VAT"].Value.ToString());
                        mskl = Math.Round(mamt - mscmdisc, 2);
                        mitm = Math.Round((mskl * Convert.ToDouble(dr.Cells["Col_ItemDiscountPer"].Value.ToString())) / 100, 2); //4
                        //msplddx = Math.Round(((mskl - mitm) * _SSSale.SpecialDiscountPercentS) / 100, 2); //4
                        //mcrddx = Math.Round(((mskl - mitm) * _SSSale.CreditNoteDiscountPercentS) / 100, 2); //4
                        //mddx = Math.Round(((mskl - msplddx - mitm) * _SSSale.CashDiscountPercentageS) / 100, 2); //4
                        mtt1 = Math.Round((mamt - mddx - msplddx - mcrddx - mscmdisc - mitm) * (mmstperpur / 100), 2); //4
                        mtt1S = mtt1;
                        mtt1 = Math.Round(mtt1 / mqty, 2); //4
                        mtotalvat += mtt1;
                        dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();
                        dr.Cells["Col_CreditNoteAmount"].Value = mcrddx.ToString();
                        dr.Cells["Col_CashDiscountAmount"].Value = mddx.ToString();
                        mtotcashdiscount += mddx;
                        dr.Cells["Col_VATAmountPurchase"].Value = mtt1.ToString();
                        //dr.Cells["Col_SplDiscountPer"].Value = _SSSale.SpecialDiscountPercentS.ToString();
                        //   dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();
                        if (mmstperpur == 0)
                        {
                            mpuramountzero += mamt;
                            mtotmstzero += mtt1S;
                            mtotdisczero += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }
                        else if (mmstperpur == 5)
                        {
                            mmstamt5 += (mamt - mddx - msplddx - mcrddx - msplddx - mitm);
                            mpuramount5 += mamt;
                            mtotmst5 += mtt1S;
                            mtotdisc5 += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }
                        else if (mmstperpur == 12.5)
                        {
                            mmstamt12point5 += (mamt - mddx - msplddx - mcrddx - msplddx - mitm);
                            mpuramount12point5 += mamt;
                            mtotmst12point5 += mtt1S;
                            mtotdisc12point5 += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }
                        else
                        {
                            mmstamtother += (mamt - mddx - msplddx - mcrddx - msplddx - mitm);
                            mtotmstother += mtt1S;
                            mtotdiscother += mddx + msplddx + mcrddx + mscmdisc + mitm;
                        }

                    }

                    mtotalvat = mtotmst5 + mtotmst12point5 + mtotmstother;

                }
                txtVAT5AmountS.Text = mtotmst5.ToString("0.00");
                txtVAT12Point5AmountS.Text = mtotmst12point5.ToString("#0.00");
                txtViewVat5per.Text = mtotmst5.ToString("0.00");
                txtViewVat12point5per.Text = mtotmst12point5.ToString("#0.00");
                txtTotalVATAmountS.Text = (mtotmst5 + mtotmst12point5).ToString("#0.00");
                txtPurchaseAmountVAT5S.Text = mmstamt5.ToString("0.00");
                txtPurchaseAmountVAT12point5S.Text = mmstamt12point5.ToString("0.00");
                txtPurchaseAmountVATZeroS.Text = mpuramountzero.ToString("#0.00");
                txtCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
                txtPreCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
                //txtpuramount0.Text = mpuramount0.ToString("0.00");
                // txtpuramount5.Text = mpuramount5.ToString("0.00");
                // txtpuramount12point5.Text = mpuramount12point5.ToString("#0.00");


            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateFinalVAT>>" + Ex.Message);
            }
        }
        private void ClearDebitCreditNoteWhenAmountIsLess()
        {
            string mvoutype = "";
            foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
            {
                bool ch = false;
                double mamt = 0;
                ch = Convert.ToBoolean(crdbdr.Cells["Col_Check"].Value);
                if (ch == true)
                {
                    mvoutype = crdbdr.Cells["Col_VoucherType"].Value.ToString().Trim();
                    double.TryParse(crdbdr.Cells["Col_AmountNet"].Value.ToString().Trim(), out mamt);
                    if (mvoutype == FixAccounts.VoucherTypeForDebitNoteStock || mvoutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                        crdbdr.Cells["Col_Check"].Value = false;
                }
            }
        }
        #endregion

        # region Button Click
        private void btnSummary_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                {
                    txtDBAmountS.Text = "0.00";
                    txtCRAmountS.Text = "0.00";
                    dt = FillCreditDebitNote();
                    pnlProductDetail.Visible = false;
                    dgvLastPurchase.Visible = false;
                    pnlBillDetails.Enabled = false;
                    mpMSVC.Enabled = false;
                    GetpnlSummaryLocation();
                    pnlSummary.BringToFront();
                    pnlSummary.Visible = true;
                    pnlDebitCreditNote.BringToFront();
                    dgCreditNote.Visible = true;
                    if (_Mode == OperationMode.Edit || cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                        pnlBank.Visible = false;
                    if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _SSSale.StatementNumber > 0)
                        pnlSummary.Enabled = false;  
                    else
                        pnlSummary.Enabled = true;
                    CalculateGetSummaryData();
                    txtCRAmountS.Focus();
                    btnSummary.Enabled = false;

                    if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                    {

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            pnlDebitCreditNote.BringToFront();
                            pnlDebitCreditNote.Visible = true;
                            dgCreditNote.Visible = true;
                            lblFooterMessage.Text = "Press Space Bar to Select unSelect Credit Debit Note";
                            pnlDebitCreditNote.Select();
                            if (_Mode == OperationMode.View)
                                btnCRDBOK.Focus();
                            else
                                dgCreditNote.Focus();

                        }
                    }

                    if (pnlDebitCreditNote.Visible == false)
                    {
                        tsBtnSave.Enabled = true;

                    }
                    CalculateFinalVAT();
                    CalculateFinalSummary();
                    if (_Purchase.StatementNumber > 0)
                        tsBtnSave.Enabled = false;
                }
                else
                {
                    pnlProductDetail.Visible = false;
                    dgvLastPurchase.Visible = false;
                    pnlBillDetails.Enabled = false;
                    mpMSVC.Enabled = false;
                    GetpnlSummaryLocation();
                    pnlSummary.BringToFront();
                    pnlSummary.Enabled = false;
                    CalculateGetSummaryData();
                    CalculateFinalSummary();
                    txtCRAmountS.Focus();
                    btnSummary.Enabled = false;
                    pnlSummary.Visible = true;
                }
                if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _Mode == OperationMode.Delete)
                    btnCancelS.Visible = false;
                else
                    btnCancelS.Visible = true;
            }

            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnSummary_Click>>" + Ex.Message);
            }
        }
        private DataTable FillCreditDebitNote()
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            try
            {
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    ConstructCreditNoteColumns();
                    dgCreditNote.DoubleColumnNames.Add("Col_AmountNet");
                    Purchase crdb = new Purchase();

                    dt = crdb.GetOverviewDataForPurchase(mcbCreditor.SelectedID, _Purchase.Id);
                    if (dt != null)
                        retValue = BindCreditNoteDebitNote(dt);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.FillCreditDebitNote>>" + Ex.Message);
            }
            return dt;
        }
        private void btnSummary_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void btnCancelS_Click(object sender, EventArgs e)
        {
            btnCancelSClick();

        }
        private void btnCancelSClick()
        {
            try
            {
                pnlSummary.Visible = false;
                pnlSummary.SendToBack();
                btnSummary.Enabled = true;
                mpMSVC.BringToFront();
                mpMSVC.Visible = true;
                if (_SSSale.IfTypeChange == "N")
                {
                    pnlBillDetails.Enabled = true;
                    mpMSVC.Enabled = true;
                    tsBtnSave.Enabled = false;
                }
                if (txtGridAmountTot.Text != null && txtGridAmountTot.Text != "")
                    btnSummary.Enabled = true;
                mpMSVC.SetFocus(1);
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCancelS_Click>>" + Ex.Message);
            }
        }
        private void CalculateGetSummaryData()
        {

            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {
                double mtotamt = 0;
                double mtotscm = 0;
                double mtotitem = 0;
                double mtotvat5 = 0;
                double mtotvat12point5 = 0;
                double mvatamount = 0;
                double mamt = 0;
                double mamts = 0;
                double mvatper = 0;
                double mqty = 0;
                double mtotvatzeroamt = 0;
                double moctroiamt = 0;
                //_SSSale.AmountCashDiscountS = 0;
                double mtotspldisc = 0;
                double mpuramount0 = 0;
                double mpuramount5 = 0;
                double mpuramount12point5 = 0;
                double mtotamtbymrp = 0;
                double mtotamtbypurrate = 0;
                double mmrp = 0;
                double mprate = 0;
                int muom = 1;

                try
                {
                    foreach (DataGridViewRow dr in mpMSVC.Rows)
                    {
                        if (dr.Cells["Col_SaleRate"].Value != null && dr.Cells["Col_SaleRate"].Value.ToString().Trim() != "")
                        {
                            mprate = 0;
                            muom = 1;//Col_UnitOfMeasure

                            double.TryParse(dr.Cells["Col_MRP"].Value.ToString(), out mmrp);
                            if (dr.Cells["Col_PurchaseRate"].Value != null && dr.Cells["Col_PurchaseRate"].Value.ToString().Trim() != "")
                                double.TryParse(dr.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                            if (dr.Cells["Col_UnitOfMeasure"].Value != null && dr.Cells["Col_UnitOfMeasure"].Value.ToString().Trim() != "")
                                int.TryParse(dr.Cells["Col_UnitOfMeasure"].Value.ToString(), out muom);
                            if (dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != string.Empty)
                                mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                            mtotamtbymrp += Math.Round(mqty * (mmrp / muom), 2);
                            mtotamtbypurrate += Math.Round(mqty * (mprate / muom), 2);
                            if (dr.Cells["Col_VAT"].Value != null && dr.Cells["Col_VAT"].Value.ToString() != string.Empty)
                            {
                                double.TryParse(dr.Cells["Col_VAT"].Value.ToString(), out mvatper);
                            }
                            if (dr.Cells["Col_VATAmountPurchase"].Value != null && dr.Cells["Col_VATAmountPurchase"].Value.ToString() != string.Empty)
                            {
                                double.TryParse(dr.Cells["Col_VATAmountPurchase"].Value.ToString(), out mvatamount);
                            }
                            if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != string.Empty)
                            {
                                double.TryParse(dr.Cells["Col_Amount"].Value.ToString(), out mamts);
                                mtotamt += mamts;
                                if (mvatper == 0)
                                {
                                    mtotvatzeroamt += mamts;
                                    mpuramount0 += mamts;
                                }
                                else if (mvatper == 12.5)
                                {
                                    mtotvat12point5 += mvatamount;
                                    mpuramount12point5 += mamts;
                                }
                                else
                                {
                                    mtotvat5 += mvatamount;
                                    mpuramount5 += mamts;
                                }
                            }
                            mamt = 0;
                            if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != "")
                            {
                                double.TryParse(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString(), out mamt);
                                mtotscm += mamt;
                            }
                            mamt = 0;
                            if (dr.Cells["Col_ItemDiscountAmount"].Value != null && dr.Cells["Col_ItemDiscountAmount"].Value.ToString() != "")
                            {
                                double.TryParse(dr.Cells["Col_ItemDiscountAmount"].Value.ToString(), out mamt);
                                if (mamt > 0)
                                    mtotitem += Math.Round(mamt * mqty, 4);
                            }
                            mamt = 0;
                            //if (dr.Cells["Col_CashDiscountAmount"].Value != null && dr.Cells["Col_CashDiscountAmount"].Value.ToString() != "")
                            //{
                            //    //double.TryParse(dr.Cells["Col_CashDiscountAmount"].Value.ToString(), out mamt);
                            //    //_SSSale.AmountCashDiscountS += mamt;
                            //}

                            mamt = 0;
                            if (dr.Cells["Col_SplDiscountAmount"].Value != null && dr.Cells["Col_SplDiscountAmount"].Value.ToString() != "")
                            {
                                double.TryParse(dr.Cells["Col_SplDiscountAmount"].Value.ToString(), out mamt);
                                mtotspldisc += mamt;
                            }
                            mamt = 0;
                            if (General.CurrentSetting.MsetPurchaseIfProductWithOctroi == "Y")
                            {
                                if (dr.Cells["Col_IfOctroi"].Value != null && dr.Cells["Col_IfOctroi"].Value.ToString() == "Y")
                                    moctroiamt += mamt;
                            }
                            else
                            {
                                if (General.CurrentSetting.MsetPurchaseOctroionZeroVAT == "Y")
                                {
                                    if (dr.Cells["Col_VAT"].Value != null && dr.Cells["Col_VAT"].Value.ToString() != "" && Convert.ToDouble(dr.Cells["Col_VAT"].Value.ToString()) == 0)
                                        moctroiamt += mamt;
                                }
                                else
                                    moctroiamt += mamt;
                            }
                        }
                    }
                    //_SSSale.TotalAmountForOctroiS = moctroiamt;
                    txtBillAmountS.Text = mtotamt.ToString("#0.00");
                    txtBillAmount.Text = mtotamt.ToString("#0.00");
                    txtItemDiscountS.Text = mtotitem.ToString("#0.00");
                    //txtSplDiscPerS.Text = _SSSale.SpecialDiscountPercentS.ToString("#0.00");
                    //txtCashDiscountAmountS.Text = _SSSale.AmountCashDiscountS.ToString("0.00");
                    //txtPreCashDiscountAmountS.Text = _SSSale.AmountCashDiscountS.ToString("#0.00");
                    txtSchemeDiscountS.Text = mtotscm.ToString("#0.00");
                    txtVAT5AmountS.Text = mtotvat5.ToString("#0.00");
                    txtVAT12Point5AmountS.Text = mtotvat12point5.ToString("#0.00");
                    txtViewVat5per.Text = mtotvat5.ToString("#0.00");
                    txtViewVat12point5per.Text = mtotvat12point5.ToString("#0.00");
                    txtPurchaseAmountVAT12point5S.Text = mpuramount12point5.ToString("#0.00");
                    txtPurchaseAmountVAT5S.Text = mpuramount5.ToString("#0.00");
                    txtPurchaseAmountVATZeroS.Text = mpuramount0.ToString("#0.00");
                    double mtotprofit = Math.Round(((mtotamtbymrp - mtotamtbypurrate) / mtotamtbypurrate) * 100, 2);
                    txtProfitPerS.Text = mtotprofit.ToString("#0.00");
                    CalculateTotalVATAmount();
                }

                catch (Exception Ex)
                {
                    Log.WriteError("UclPurchase.CalculateGetSummaryData>>" + Ex.Message);
                }
            }
        }

        public void CalculateTotalVATAmount()
        {
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {


                double mtotvat5 = 0;
                double mtotvat12point5 = 0;
                double mtotvat = 0;
                try
                {
                    if (txtVAT5AmountS.Text != null && txtVAT5AmountS.Text.ToString() != "")
                        double.TryParse(txtVAT5AmountS.Text.ToString(), out mtotvat5);
                    if (txtVAT12Point5AmountS.Text != null && txtVAT12Point5AmountS.Text.ToString() != "")
                        double.TryParse(txtVAT12Point5AmountS.Text.ToString(), out mtotvat12point5);
                    mtotvat = Math.Round(mtotvat5, 2) + Math.Round(mtotvat12point5, 2);
                    txtTotalVATAmountS.Text = (mtotvat).ToString("0.00");
                }
                catch (Exception Ex)
                {
                    Log.WriteError("UclPurchase.CalculateTotalVATAmount>>" + Ex.Message);
                }
            }
        }
        # endregion




        #region Events
        private void txtAddOnS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    CalculateFinalSummary();
                    txtCashDiscountAmountS.Focus();
                }
                else if (e.KeyCode == Keys.Up)
                    txtVAT12Point5AmountS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtAddOnS_KeyDown>>" + Ex.Message);
            }
        }

        private void txtCRAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    CalculateFinalSummary();
                    txtDBAmountS.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtCRAmountS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtDBAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtVAT5AmountS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtCRAmountS.Focus();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtDBAmountS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtCashDiscountPerS.Focus();
                else if (e.KeyCode == Keys.Up)
                    mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtNarration_KeyDown>>" + Ex.Message);
            }
        }
        private void txtCashDiscountPerS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {                
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
                {
                    lblFooterMessage.Text = string.Empty;
                    if (pnlProductDetail.Visible == true)
                    {
                        CalculateRowAmount();
                    }
                    else
                    {
                        CalculatePurRateSaleRateAmountforFullGrid();
                    }
                    txtSplDiscountS.Focus();
                }
                else if (e.KeyCode == Keys.Up)
                    txtNarration.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtCashDiscountPerS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtSpecialDiscountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    if (pnlProductDetail.Visible == true)
                    {
                        CalculateRowAmount();
                    }
                    else
                    {
                        CalculatePurRateSaleRateAmountforFullGrid();
                    }
                    mpMSVC.SetFocus(1);
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                    txtCashDiscountPerS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtSpecialDiscountS_KeyDown>>" + Ex.Message);
            }
        }

        private void txtVAT5AmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtVAT12Point5AmountS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtDBAmountS.Focus();
                CalculateTotalVATAmount();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtVAT5AmountS_KeyDown>>" + Ex.Message);
            }
        }
        private void txtVAT12Point5AmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtAddOnS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtVAT5AmountS.Focus();
                CalculateTotalVATAmount();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtVAT12Point5AmountS_KeyDown>>" + Ex.Message);
            }
        }

        private void txtCashDiscountAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    txtOCTPerS.Focus();
                    double billamt = Convert.ToDouble(txtBillAmountS.Text.ToString());
                    double scmamt = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
                    double itemamt = Convert.ToDouble(txtItemDiscountS.Text.ToString());
                    double discamt = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
                    double actualdiscamountper = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
                    double entereddiscper = Math.Round((discamt * 100) / (billamt - scmamt - itemamt), 2);
                    if (((entereddiscper) > (actualdiscamountper + 0.20)) || ((entereddiscper) < (actualdiscamountper - 0.20)))
                    {
                        pnlSummary.Visible = false;
                        pnlBillDetails.Enabled = true;
                        txtCashDiscountPerS.Text = entereddiscper.ToString("#0.00");
                        lblFooterMessage.Text = "Press Enter..";
                        txtCashDiscountPerS.Focus();
                    }
                    CalculateFinalSummary();
                }
                else if (e.KeyCode == Keys.Up)
                {
                    CalculateFinalSummary();
                    txtCashDiscountAmountS.Focus();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtOCTPerS_KeyDown>>" + Ex.Message);
            }
        }

        private void txtOCTPerS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    txtOCTAmountS.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtAddOnS.Focus();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtOCTPerS_KeyDown>>" + Ex.Message);
            }
        }

        private void txtOCTAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                //    btnOKS.Focus();
                //else
                if (e.KeyCode == Keys.Up)
                    txtOCTPerS.Focus();
                CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtOCTAmountS_KeyDown>>" + Ex.Message);
            }
        }



        private void btnCRDBOK_Click(object sender, EventArgs e)
        {
            btnCRDBOKClick();
        }
        private void btnCRDBOKClick()
        {
            double mcrnoteamt = 0;
            double mdbnoteamt = 0;
            string mvoutype = "";
            lblFooterMessage.Text = "";

            try
            {
                foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
                {
                    bool ch = false;
                    double mamt = 0;
                    ch = Convert.ToBoolean(crdbdr.Cells["Col_Check"].Value);
                    if (ch == true)
                    {
                        mvoutype = crdbdr.Cells["Col_VoucherType"].Value.ToString().Trim();
                        double.TryParse(crdbdr.Cells["Col_AmountNet"].Value.ToString().Trim(), out mamt);
                        if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                            mcrnoteamt += mamt;
                        else
                            mdbnoteamt += mamt;
                    }
                }
                txtCRAmountS.Text = mcrnoteamt.ToString("#0.00");
                txtDBAmountS.Text = mdbnoteamt.ToString("#0.00");
                pnlDebitCreditNote.Visible = false;
                CalculateFinalSummary();
                tsBtnSave.Enabled = true;
                pnlSummary.BringToFront();
                pnlSummary.Visible = true;
                pnlSummary.Focus();
                txtCRAmountS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCRDBOK_Click>>" + Ex.Message);
            }
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            try
            {
                btnSummary.BackColor = Color.Bisque;
                btnSummary.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.mpPVC1_OnTABKeyPressed>>" + Ex.Message);
            }
        }

        private void mpPVC1_OnRowDeleted(object sender, EventArgs e)
        {
            try
            {
                CalculateTotals();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.mpPVC1_OnRowDeleted>>" + Ex.Message);
            }
        }

        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateRowAmount();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.cbRound_CheckedChanged>>" + Ex.Message);
            }
        }

        private void btnPaymentHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlPaymentDetails.Visible == false)
                {
                    mpMSVC.Enabled = false;
                    pnlPaymentDetails.BringToFront();
                    pnlPaymentDetails.Visible = true;
                    dgPaymentDetails.Visible = true;
                    btnSummary.Enabled = false;

                }
                else
                {
                    pnlPaymentDetails.SendToBack();
                    dgPaymentDetails.Visible = false;
                    mpMSVC.Enabled = true;
                    pnlPaymentDetails.Visible = false;
                    pnlBillDetails.Enabled = true;
                    btnSummary.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnPaymentHistory_Click>>" + Ex.Message);
            }
        }



        private void btnCRDBNote_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDebitCreditNote.BringToFront();
                pnlDebitCreditNote.Visible = true;
                dgCreditNote.Visible = true;
                dgCreditNote.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCRDBNote_Click>>" + Ex.Message);
            }
        }

        private void dgCreditNote_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    pnlDebitCreditNote.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.dgCreditNote_KeyDown>>" + Ex.Message);
            }
        }


        private void btnTypeChange_Click(object sender, EventArgs e)
        {
            try
            {
                _SSSale.IfTypeChange = "Y";
                //if (General.CurrentSetting.MsetPurchaseIfCreditPurchase == "Y")
                //{
                //    rbtCreditSTMT.Visible = true;
                //    rbtCreditSTMT.Enabled = true;
                //}
                //else
                //    rbtCreditSTMT.Visible = false;  

                tsBtnSave.Enabled = true;
                mcbCreditor.Enabled = false;
                mpMSVC.Enabled = false;
                pnlProductDetail.Enabled = false;
                pnlSummary.Enabled = false;
                cbTransactionType.Enabled = false;
                cbNewTransactionType.Enabled = true;
                cbNewTransactionType.Items.Clear();
                if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                {

                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                    if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
                        cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
                    cbNewTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                    cbNewTransactionType.SelectedIndex = cbNewTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                }
                else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                {
                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
                    if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
                        cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
                    cbNewTransactionType.Text = FixAccounts.TransactionTypeForCash;
                    cbNewTransactionType.SelectedIndex = cbNewTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);

                }
                else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                {
                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                    cbNewTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                    cbNewTransactionType.SelectedIndex = cbNewTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                }
                btnTypeChange.Enabled = false;
                cbNewTransactionType.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnTypeChange_Click>>" + Ex.Message);
            }
        }



        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtVouchernumber.Text != "")
                    {

                        //_SSSale.VoucherNumber = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        //_SSSale.ReadDetailsByVouNumber(_SSSale.VoucherNumber, _SSSale.VoucherType);
                        FillSearchData(_SSSale.Id, "");
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteError("UclPurchase.txtVouchernumber_KeyDown>>" + Ex.Message);
                }
            }
        }


        private void mcbCreditor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Up)
                    cbTransactionType.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.mcbCreditor_KeyDown>>" + Ex.Message);
            }
        }

        private void cbAcceptNrExpired_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMRP.Focus();
        }

        private void btnOK_Enter(object sender, EventArgs e)
        {
            btnOK.BackColor = General.ControlFocusColor;
        }

        private void btnOK_Leave(object sender, EventArgs e)
        {
            btnOK.BackColor = Color.White;
        }

        private void btnCancel_Leave(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.White;
        }

        private void btnCancel_Enter(object sender, EventArgs e)
        {
            btnCancel.BackColor = General.ControlFocusColor;
        }

        private void dgCreditNote_OnCellValueChangeCommited(int colIndex)
        {
            dgCreditNote.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgCreditNote_OnShowViewForm(DataGridViewRow selectedRow)
        {
            string voutype = "";
            try
            {
                if (selectedRow != null && dgCreditNote.Rows.Count > 0 && selectedRow.Index >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = selectedRow.Cells[0].Value.ToString();
                    voutype = selectedRow.Cells["Col_VoucherType"].Value.ToString();
                    if (voutype == FixAccounts.VoucherTypeForCreditNoteStock)
                        ViewControl = new UclCreditNoteStock();
                    else if (voutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                        ViewControl = new UclCreditNoteAmount();
                    else if (voutype == FixAccounts.VoucherTypeForDebitNoteStock)
                        ViewControl = new UclDebitNotestock();
                    else if (voutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                        ViewControl = new UclDebitNoteAmount();
                    ShowViewForm(selectedID);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        private void ViewControl_ExitClicked(object sender, EventArgs e)
        {
            if (frmView != null)
            {
                frmView.Close();
            }
        }
        public void ShowViewForm(string ID)
        {
            if (ViewControl != null)
            {
                frmView = new Form();
                frmView.FormBorderStyle = FormBorderStyle.None;
                frmView.Height = this.Height;
                frmView.Width = this.Width;
                frmView.StartPosition = FormStartPosition.Manual;
                frmView.Location = new Point(this.Location.X + 45, this.Location.Y + 60);
                //  frmView.Icon = PharmaSYSRetailPlus.Properties.Resources.Icon;
                ViewControl.Mode = OperationMode.ReportView;
                ((IDetailControl)ViewControl).View();
                ViewControl.FillSearchData(ID, "C");
                ViewControl.ExitClicked -= new EventHandler(ViewControl_ExitClicked);
                ViewControl.ExitClicked += new EventHandler(ViewControl_ExitClicked);
                ViewControl.Visible = true;
                ViewControl.Height = this.Height - 6;
                ViewControl.Width = this.Width - 6;
                ViewControl.BringToFront();
                ViewControl.Location = new Point(3, 3);
                Panel pnl = new Panel();
                pnl.BackColor = Color.Orange;
                pnl.Dock = DockStyle.Fill;
                pnl.Controls.Add(ViewControl);
                frmView.Controls.Add(pnl);
                frmView.ShowDialog();
            }
        }

        private void txtNarration_Enter(object sender, EventArgs e)
        {
            txtCashDiscountPerS.Focus();
        }

        private void datePickerBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNarration.Focus();
        }


        private void cbTransactionType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbCreditor.Focus();
        }

        private void cbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
            //{
            //    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashPurchase;

            //}
            //else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
            //    _SSSale.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
            //else
            //    _SSSale.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
            //_SSSale.OldVoucherType = _SSSale.VoucherType;
            //txtVouType.Text = _SSSale.VoucherType;

        }

        private void UclPurchase_Load(object sender, EventArgs e)
        {
            FillTransactionType();
        }

        private void mpMSVC_OnCellValueChangeCommited(int colIndex)
        {
            if (colIndex == 1)
            {
                string _preID = "";
                string prodname = "";
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                    _preID = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value != null)
                    prodname = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
                if (prodname != "" && _preID != "")
                {
                    prodname = General.GetProductName(_preID);
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = prodname;
                }
            }
        }

        private void datePickerBillDate_Validating(object sender, CancelEventArgs e)
        {
            bool retValue = false;
            retValue = General.CheckBillDateForAccountingYear(datePickerBillDate.Text.ToString());
            if (retValue)
            {
                lblFooterMessage.Text = "";

            }
            else
            {
                datePickerBillDate.Focus();
                lblFooterMessage.Text = "Check Bill Date";
            }

        }


        private void txtDistRatePercent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDistPercentEnterkeyPressed();
            }

        }

        private void txtDistPercentEnterkeyPressed()
        {
            double mdistper = 0;
            double mdistrate = 0;
            double mtraderate = 0;
            try
            {
                if (txtDistRatePercent.Text != null && txtDistRatePercent.Text.ToString() != string.Empty)
                    mdistper = Convert.ToDouble(txtDistRatePercent.Text.ToString());
                if (txtTradeRate.Text != null && txtTradeRate.Text.ToString() != string.Empty)
                    mtraderate = Convert.ToDouble(txtTradeRate.Text.ToString());
                mdistrate = Math.Round((mtraderate * mdistper) / 100, 2);
                mdistrate = Math.Round(mtraderate + mdistrate, 2);
                txtSaleRate.Text = mdistrate.ToString("#0.00");
                btnOK.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void lblAmount_Click(object sender, EventArgs e)
        {

        }

        private void mpMSVC_OnShiftTABKeyPressed(object sender, EventArgs e)
        {
            mcbCreditor.Focus();
        }
        #endregion

        //private void txtQuantity_Validating(object sender, CancelEventArgs e)
        //{
        //    int mqty = 0;
        //    if (txtQuantity.Text != null && txtQuantity.Text.ToString() != string.Empty)
        //        mqty = Convert.ToInt32(txtQuantity.Text.ToString());
        //    if (mqty >= 0 && mqty <= _SSSale.CurrentBatchStock)
        //    {
        //        pnlScheme.Visible = true;
        //        txtDiscountPer.Focus();
        //    }
        //    else
        //        txtQuantity.Focus();
        //}

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQuantityValidating();
        }

        private void txtQuantityValidating()
        {
            int mqty = 0;
            if (txtQuantity.Text != null && txtQuantity.Text.ToString() != string.Empty)
                mqty = Convert.ToInt32(txtQuantity.Text.ToString());
            if (mqty >= 0 && mqty <= _SSSale.CurrentBatchStock)
            {
                CalculateRowAmount();
                txtSaleRate.Focus();                                     
            }
            else
                txtQuantity.Focus();
        }
        private void txtSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (_SchemeData != null)
                {
                    FillSchemeData();
                    pnlScheme.Visible = true;
                    pnlScheme.Focus();
                    lblProductName.Text = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString() + " " + mpMSVC.MainDataGridCurrentRow.Cells["Col_UnitOfMeasure"].Value.ToString() + "  " + mpMSVC.MainDataGridCurrentRow.Cells["Col_Pack"].Value.ToString();
                    lblQuantity.Text = txtQuantity.Text.ToString();
                    // dgvSchemeGrid.Focus();
                    gbGivenScheme.Enabled = false;
                    btnScheme1.Focus();
                }
        }

        private void FillSchemeData()
        {
            int prodqty = 0;
            int schemeqty = 0;
            gbScheme.Enabled = true;
            try
            {


                if (_SchemeData["ProductQuantity1"] != DBNull.Value && _SchemeData["ProductQuantity1"].ToString() != string.Empty)
                    prodqty = Convert.ToInt32(_SchemeData["ProductQuantity1"].ToString());
                if (_SchemeData["SchemeQuantity1"] != DBNull.Value && _SchemeData["SchemeQuantity1"].ToString() != string.Empty)
                    schemeqty = Convert.ToInt32(_SchemeData["SchemeQuantity1"].ToString());
                //int rowIndex = dgvSchemeGrid.Rows.Add();
                //dgvSchemeGrid.Rows[rowIndex].Cells["Col_ProductQuantity"].Value = prodqty.ToString("#0");
                //dgvSchemeGrid.Rows[rowIndex].Cells["Col_SchemeQuantity"].Value = schemeqty.ToString("#0");
                if (prodqty > 0 && schemeqty > 0)
                {
                    btnScheme1.Text = prodqty.ToString() + " : " + schemeqty.ToString();
                    schemeQuantity1 = prodqty;
                    schemeFree1 = schemeqty;
                    btnScheme1.Checked = true;
                    if (Convert.ToInt32(btnGivenSchemeQuantity.Text.ToString()) > 0)
                        btnGivenSchemeQuantity.Checked = true;
                    else
                        btnGivenSchemeAmount.Checked = true;
                }
                else
                    btnScheme1.Enabled = false;

                prodqty = 0;
                schemeqty = 0;
                if (_SchemeData["ProductQuantity2"] != DBNull.Value && _SchemeData["ProductQuantity2"].ToString() != string.Empty)
                    prodqty = Convert.ToInt32(_SchemeData["ProductQuantity2"].ToString());
                if (_SchemeData["SchemeQuantity2"] != DBNull.Value && _SchemeData["SchemeQuantity2"].ToString() != string.Empty)
                    schemeqty = Convert.ToInt32(_SchemeData["SchemeQuantity2"].ToString());
                //rowIndex = dgvSchemeGrid.Rows.Add();
                //dgvSchemeGrid.Rows[rowIndex].Cells["Col_ProductQuantity"].Value = prodqty.ToString("#0");
                //dgvSchemeGrid.Rows[rowIndex].Cells["Col_SchemeQuantity"].Value = schemeqty.ToString("#0");
                if (prodqty > 0 && schemeqty > 0)
                {
                    btnScheme2.Text = prodqty.ToString() + " : " + schemeqty.ToString();
                    schemeQuantity2 = prodqty;
                    schemeFree2 = schemeqty;
                }
                else
                    btnScheme2.Visible = false;
                prodqty = 0;
                schemeqty = 0;
                if (_SchemeData["ProductQuantity3"] != DBNull.Value && _SchemeData["ProductQuantity3"].ToString() != string.Empty)
                    prodqty = Convert.ToInt32(_SchemeData["ProductQuantity3"].ToString());
                if (_SchemeData["SchemeQuantity3"] != DBNull.Value && _SchemeData["SchemeQuantity3"].ToString() != string.Empty)
                    schemeqty = Convert.ToInt32(_SchemeData["SchemeQuantity3"].ToString());
                //rowIndex = dgvSchemeGrid.Rows.Add();
                //dgvSchemeGrid.Rows[rowIndex].Cells["Col_ProductQuantity"].Value = prodqty.ToString("#0");
                //dgvSchemeGrid.Rows[rowIndex].Cells["Col_SchemeQuantity"].Value = schemeqty.ToString("#0");
                if (prodqty > 0 && schemeqty > 0)
                {
                    btnScheme3.Text = prodqty.ToString() + " : " + schemeqty.ToString();
                    schemeQuantity3 = prodqty;
                    schemeFree3 = schemeqty;
                }
                else
                    btnScheme3.Visible = false;
                lblFromDate.Text = General.GetDateInShortDateFormat(_SchemeData["StartingDate"].ToString());
                lblToDate.Text = General.GetDateInShortDateFormat(_SchemeData["ClosingDate"].ToString());
                
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnSchemeExit_Click(object sender, EventArgs e)
        {
            btnSchemeExitClick();
        }

        private void btnSchemeExitClick()
        {
            pnlScheme.Visible = false;

        }

        private void btnSchemeExit_KeyDown(object sender, KeyEventArgs e)
        {
            btnSchemeExitClick();
        }

        private void dgvSchemeGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CalculateScheme();
            }
        }       

        //private void dgvSchemeGrid_Click(object sender, EventArgs e)
        //{
        //    int mqty = Convert.ToInt32(dgvSchemeGrid.CurrentRow.Cells["Col_Quantity"].Value.ToString());
        //    CalculateScheme();
        //}

        private void CalculateScheme()
        {
            int mqty = 0;
            int mscm = 0;            
            lblQuantity.Text = txtQuantity.Text.ToString();
            if (btnScheme1.Checked)
            {
                mqty = schemeQuantity1;
                mscm = schemeFree1;
            }
            else if (btnScheme2.Checked)
            {
                mqty = schemeQuantity2;
                mscm = schemeFree2;
            }
            else if (btnScheme3.Checked)
            {
                mqty = schemeQuantity3;
                mscm = schemeFree3;
            }           
            int msaleQty = Convert.ToInt32(lblQuantity.Text.ToString());
            int mscmqty = 0;
            double scmx = 0;
            double mscmamt = 0;
            double mrate = Convert.ToDouble(txtSaleRate.Text.ToString());            
            double mmsaleqty = 0;
            mmsaleqty = msaleQty * mscm / mqty;
            mmsaleqty = Math.Floor(mmsaleqty);
            if ((Math.Floor(mmsaleqty) - (msaleQty * mscm / mqty) == 0))
                mscmqty = Convert.ToInt32(mmsaleqty);
            else
                mscmqty = 0;
            double mmqty = mqty / 2;
            mmqty = Math.Floor(mmqty);
            if (mqty + mscm > 0)
            {
                if (msaleQty < mqty)
                    scmx = Math.Round((mrate * mqty) / (mscm + mqty), 2);
                else
                    scmx = Math.Round((mrate * mqty) / (mqty + mscm), 2);
            }
            double scmy = Math.Round(mrate - scmx, 2);
            mscmamt = Math.Round(msaleQty * scmy, 2);
            btnGivenSchemeQuantity.Text = mscmqty.ToString("#0");
            btnGivenSchemeAmount.Text = mscmamt.ToString("#0.00");
            if (mscmqty > 0)
                btnGivenSchemeQuantity.Focus();
            else
                btnGivenSchemeAmount.Focus();
           // ValidateScheme();


        }

        //private void ValidateScheme()
        //{
        //    if (Convert.ToDouble(btnGivenSchemeAmount.Text.ToString()) == 0 && Convert.ToInt32(btnGivenSchemeQuantity.Text.ToString()) == 0)
        //        btnSchemeExit.Enabled = true;
        //    if (Convert.ToInt32(btnGivenSchemeQuantity.Text.ToString()) > 0)
        //    {
        //        if (Convert.ToDouble(btnGivenSchemeAmount.Text.ToString()) == 0)
        //            btnSchemeExit.Enabled = true;
        //    }
        //    else if (Convert.ToDouble(btnGivenSchemeAmount.Text.ToString()) > 0)
        //    {
        //        if (Convert.ToInt32(btnGivenSchemeQuantity.Text.ToString()) == 0)
        //            btnSchemeExit.Enabled = true;
        //    }
        //}

        //private void dgvSchemeGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    CalculateScheme();
        //}

        //private void dgvSchemeGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    CalculateScheme();
        //}

        //private void txtSchemeQty_KeyDown(object sender, KeyEventArgs e)
        //{
            
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if ((Convert.ToInt32(btnGivenSchemeQuantity.Text.ToString())) == 0)
        //            bt txtSchemeDisc.Focus();
        //        else
        //            ValidateScheme();
        //    }
        //}

        private void btnScheme1_KeyDown(object sender, KeyEventArgs e)
        {
            SchemeSelected();
        }

        private void btnScheme2_KeyDown(object sender, KeyEventArgs e)
        {
            SchemeSelected();
        }
       

        private void btnScheme3_KeyDown(object sender, KeyEventArgs e)
        {
            SchemeSelected();
        }

        private void SchemeSelected()
        {
            gbScheme.Enabled = false;
            gbGivenScheme.Enabled = true;
            if (Convert.ToInt32(btnGivenSchemeQuantity.Text.ToString()) > 0)
            {
                btnGivenSchemeQuantity.Focus();
                btnGivenSchemeQuantity.Enabled = true;
            }
            else
            {
                btnGivenSchemeQuantity.Enabled = false;
                btnGivenSchemeAmount.Focus();
            }
        }

        private void btnScheme1_CheckedChanged(object sender, EventArgs e)
        {
            if (btnScheme1.Checked)
                CalculateScheme();
        }

        private void btnScheme2_CheckedChanged(object sender, EventArgs e)
        {
            if(btnScheme2.Checked)
                CalculateScheme();
        }

        private void btnScheme3_CheckedChanged(object sender, EventArgs e)
        {
            if (btnScheme3.Checked)
                CalculateScheme();
        }

        private void btnSchemeSave_Click(object sender, EventArgs e)
        {
            btnSchemeSaveClick();
        }

        private void btnSchemeSaveClick()
        {
            pnlScheme.Visible = false;
            if (btnGivenSchemeQuantity.Checked)
            {
                txtSchemeQuantity.Text = btnGivenSchemeQuantity.Text.ToString();
                txtSchemeAmount.Text = "0.00";
            }
            else
            {
                txtSchemeQuantity.Text = "0";
                txtSchemeAmount.Text = btnGivenSchemeAmount.Text.ToString();
            }
            txtDiscountPer.Focus();
        }

        private void btnSchemeCancel_Click(object sender, EventArgs e)
        {
            btnSchemeCancelClick();
        }

        private void btnSchemeCancelClick()
        {
            pnlScheme.Visible = false;
            txtSchemeAmount.Text = "0.00";
            txtSchemeQuantity.Text = "0";
            txtDiscountPer.Focus();
        }

        private void btnGivenSchemeQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSchemeSaveClick();
        }

        private void btnGivenSchemeAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSchemeSaveClick();
        }

       
      
    }
}
