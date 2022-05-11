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
using PrintDataGrid;
using System.IO;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDistributorSaleold : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _PaymentDetailsBindingSource;
        private SSSale _SSSale;
        private BaseControl ViewControl;
        private Form frmView;
        string _preID = "";
        #endregion

        #region Constructor
        public UclDistributorSaleold()
        {
            try
            {
                InitializeComponent();
                _SSSale = new SSSale();
                SearchControl = new UclDistributorSaleSearch();
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
            try
            {
                if (_Mode == OperationMode.Add)
                    txtTokenNumber.Focus();
                else
                    txtVouchernumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool ClearData()
        {
            try
            {
                _SSSale.Initialise();
                ClearControls();
                ConstructMainColumns();
                FormatGrids();
                if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit) && _SSSale.AddNewRowCheck(mpPVC1))
                    mpPVC1.Rows.Add();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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

                if (General.CurrentUser.Level > 1)
                    cbEditRate.Visible = false;

                mpPVC1.ModuleNumber = ModuleNumber.DebtorSale;
                mpPVC1.OperationMode = OperationMode.Add;

                InitialisempPVC1("");


                FillDoctorCombo();
                FillPrescription();
                FillTransactionType();
                mcbDoctor.SelectedID = "";
                FillPartyCombo();
                InitializeCheckBoxes();
                txtTokenNumber.Text = _SSSale.TokenNumber.ToString("#0");
                cbclonevoutype.Items.Add(FixAccounts.VoucherTypeForCashSale);
                cbclonevoutype.Items.Add(FixAccounts.VoucherTypeForCreditSale);
                cbclonevoutype.Items.Add(FixAccounts.VoucherTypeForCreditStatementSale);

                txtTokenNumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                ClearData();
                FillPrescription();
                headerLabel1.Text = "DISTRIBUTOR SALE -> EDIT";
                InitializeCheckBoxes();

                if (General.CurrentUser.Level > 1)
                    cbEditRate.Visible = false;

                FillPartyCombo();
                FillTransactionType();
                //   rbtCashCredit.Checked = true;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                mpPVC1.ModuleNumber = ModuleNumber.DebtorSale;
                mpPVC1.OperationMode = OperationMode.Edit;
                txtVouchernumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            ClearData();
            return retValue;
        }
        public override bool Exit()
        {
            bool retValue = base.Exit();
            System.IO.File.Delete(General.GetDebtorSaleTempFile());
            UpdateClosingStockinCache();
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "DISTRIBUTOR SALE -> DELETE";
                InitializeCheckBoxes();
                FillTransactionType();
                ClearData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_SSSale.CrdbAmountBalance != _SSSale.CrdbAmountNet && _SSSale.CrdbVouType != FixAccounts.VoucherTypeForCashSale)
                MessageBox.Show("Payment Done Can not Delete", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

                if (MessageBox.Show("Are you sure you want to delete Sale Information?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BindTempGrid();
                    LockTable.LockTablesForSale();
                    General.BeginTransaction();
                    retValue = _SSSale.DeleteDetails();
                    if (retValue)
                        retValue = DeletePreviousRows();
                    if (retValue)
                        retValue = AddPreviousStock();
                    if (retValue)
                        clearPreviousdebitcreditnotes();
                    if (retValue)
                        General.CommitTransaction();
                    else
                        General.RollbackTransaction();
                    LockTable.UnLockTables();
                    if (retValue)
                    {
                        UpdateClosingStockinCache();
                        _SSSale.ModifiedBy = General.CurrentUser.Id;
                        _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        _SSSale.AddDetailsInDeleteMaster();
                        AddPreviousRowsInDeleteDetail();
                        retValue = true;
                        MessageBox.Show("Successfully Deleted", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        PSDialogResult result = PSMessageBox.Show("Could not Delete...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                        retValue = false;
                    }
                }
            }
            ClearData();
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                ClearData();
                headerLabel1.Text = "DISTRIBUTOR SALE -> VIEW";
                InitializeCheckBoxes();
                FillTransactionType();
                mpPVC1.ClosePopupGrid();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool Print()
        {
            bool retValue = true;
            if (_SSSale.CrdbAmountNet > 0)
            {
                if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                    PrintSaleBillPrePrintedPaper();
                else
                    PrintSaleBillPlainPaper();
            }
            return retValue;
        }
        private void PrintSaleBillPrePrintedPaper()
        {
            PharmaSYSRetailPlus.Printing.PrePrintedPaperPrinter printer = new PharmaSYSRetailPlus.Printing.PrePrintedPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, _SSSale.PatientShortAddress, _SSSale.Telephone, _SSSale.DoctorName, _SSSale.DoctorAddress, mpPVC1.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount + _SSSale.CrdbAmountNet);

        }

        private void PrintSaleBillPlainPaper()
        {
            PharmaSYSRetailPlus.Printing.PlainPaperPrinter printer = new PharmaSYSRetailPlus.Printing.PlainPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, _SSSale.PatientShortAddress, _SSSale.Telephone, _SSSale.DoctorName, _SSSale.DoctorAddress, mpPVC1.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount + _SSSale.CrdbAmountNet);

        }

        public override bool Save()
        {
            return SaveData(false);
        }

        public override bool SaveAndPrint()
        {
            return SaveData(true);
        }

        private bool SaveData(bool printData)
        {
            bool retValue = false;
            double mdiscper = 0;
            double maddon = 0;
            double mdiscamount = 0;
            double mvat5per = 0;
            double mvat12point5per = 0;
            double mamtforzerovat = 0;
            double mbillamount = 0;
            double mamount = 0;
            double mround = 0;
            double mamountvat5per = 0;
            double mamountvat12point5per = 0;
            double mcreditnoteamount = 0;
            double mdebitnoteamount = 0;
            if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
            {
                if (txtNetAmount.Text != null && Convert.ToDouble(txtNetAmount.Text.ToString()) > 0)
                {

                    try
                    {
                        if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                            txtVouType.Text = FixAccounts.VoucherTypeForDistributorSaleCash;
                        else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                            txtVouType.Text = FixAccounts.VoucherTypeForDistributorSaleCreditStatement;
                        else
                            txtVouType.Text = FixAccounts.VoucherTypeForDistributorSaleCredit;
                        //if (rbtCash.Checked == true)
                        //    txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                        //else if (rbtCreditStatement.Checked == true)
                        //    txtVouType.Text = FixAccounts.VoucherTypeForCreditStatementSale;
                        //else
                        //    txtVouType.Text = FixAccounts.VoucherTypeForCreditSale;
                        _SSSale.SaleSubType = FixAccounts.SubTypeForDistributorSale;
                        System.Text.StringBuilder _errorMessage;
                        if (mcbCreditor.SelectedID != null)
                        {
                            _SSSale.AccountID = mcbCreditor.SelectedID.Trim();
                            _SSSale.CrdbName = mcbCreditor.SeletedItem.ItemData[2];
                        }
                        _SSSale.PatientID = "";
                        if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "")
                        {
                            _SSSale.DocID = mcbDoctor.SelectedID.Trim();
                            _SSSale.DoctorName = mcbDoctor.SeletedItem.ItemData[1];
                            _SSSale.DoctorAddress = mcbDoctor.SeletedItem.ItemData[2];
                        }
                        else
                        {
                            _SSSale.DocID = string.Empty;
                            _SSSale.DoctorName = mcbDoctor.Text.ToString();
                            _SSSale.DoctorAddress = txtDoctorAddress.Text.ToString();
                        }
                        _SSSale.CrdbVouType = txtVouType.Text.Trim();

                        if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                            _SSSale.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                        _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                        double.TryParse(txtVatInput5per.Text, out mvat5per);
                        _SSSale.CrdbVat5 = Math.Round(mvat5per, 2);
                        double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                        _SSSale.CrdbVat12point5 = Math.Round(mvat12point5per, 2);
                        double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamtforzerovat);
                        _SSSale.CrdbAmtForZeroVAT = mamtforzerovat;
                        double.TryParse(txtDiscPercent.Text, out mdiscper);
                        _SSSale.CrdbDiscPer = mdiscper;
                        double.TryParse(txtDiscAmount.Text, out mdiscamount);
                        _SSSale.CrdbDiscAmt = mdiscamount;
                        double.TryParse(txtAmountVAT12Point5Per.Text, out mamountvat12point5per);
                        _SSSale.CrdbAmountVat12point5 = mamountvat12point5per;
                        double.TryParse(txtAmountVAT5Per.Text, out mamountvat5per);
                        _SSSale.CrdbAmountVat5 = mamountvat5per;
                        double.TryParse(txtBillAmount.Text, out mbillamount);
                        _SSSale.CrdbAmountNet = mbillamount;
                        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditSale || _SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditStatementSale)
                        {
                            _SSSale.CrdbAmountBalance = mbillamount;
                            _SSSale.CrdbAmountClear = 0;
                        }
                        else
                        {
                            _SSSale.CrdbAmountBalance = 0;
                            _SSSale.CrdbAmountClear = mbillamount;
                        }
                        double.TryParse(txtAmount.Text, out mamount);
                        _SSSale.CrdbAmount = mamount;
                        double.TryParse(txtRoundAmount.Text, out mround);
                        _SSSale.CrdbRoundAmount = mround;
                        double.TryParse(txtAddOn.Text, out maddon);
                        _SSSale.CrdbAddOn = maddon;
                        double.TryParse(txtCreditNote.Text, out mcreditnoteamount);
                        _SSSale.CrNoteAmount = mcreditnoteamount;
                        double.TryParse(txtDebitNote.Text, out mdebitnoteamount);
                        _SSSale.DbNoteAmount = mdebitnoteamount; ;
                        _SSSale.CrdbNarration = txtNarration.Text.ToString().Trim();
                        _SSSale.CrdbName = txtPatient.Text.ToString().Trim();
                        _SSSale.ShortName = txtPatient.Text.ToString();
                        if (_SSSale.ShortName.Length > 50)
                            _SSSale.ShortName = _SSSale.ShortName.Substring(0, 50);
                        if (txtAddress1.Text != null && txtAddress1.Text != "")
                            _SSSale.PatientAddress1 = txtAddress1.Text;
                        if (txtAddress2.Text != null && txtAddress2.Text != "")
                            _SSSale.PatientAddress2 = txtAddress2.Text;
                        if (txtPatientAddress.Text != null && txtPatientAddress.Text.ToString() != string.Empty)
                            _SSSale.PatientShortAddress = txtPatientAddress.Text.ToString();
                        if (_SSSale.DoctorAddress.Length > 50)
                            _SSSale.DoctorAddress = _SSSale.DoctorAddress.Substring(0, 50);
                        if (_SSSale.PatientShortAddress.Length > 50)
                            _SSSale.PatientShortAddress = _SSSale.PatientShortAddress.Substring(0, 50);
                        if (txtMobileNumber.Text != null && txtMobileNumber.Text != string.Empty)
                            _SSSale.Telephone = txtMobileNumber.Text;
                        _SSSale.OperatorID = "";
                        _SSSale.OperatorPassword = txtOperator.Text.ToString();
                        CalculateProfitPercent();
                        if (_Mode == OperationMode.Edit)
                            _SSSale.IFEdit = "Y";
                        _SSSale.Validate();

                        if (_SSSale.IsValid)
                        {
                            LockTable.LockTablesForSale();
                            bool ifstockavailable = CheckForStockintblStock();
                            if (ifstockavailable)
                            {
                                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                                {
                                    General.BeginTransaction();

                                    _SSSale.CreatedBy = General.CurrentUser.Id;
                                    _SSSale.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                    _SSSale.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                                    _SSSale.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                                    txtVouchernumber.Text = Convert.ToString(_SSSale.CrdbVouNo);
                                    retValue = _SSSale.AddDetails();
                                    _SavedID = _SSSale.Id;
                                    if (retValue)
                                        retValue = SaveparticularsProductwise();
                                    if (retValue)
                                        retValue = ReduceStockIntblStock();
                                    if (retValue)
                                    {
                                        clearPreviousdebitcreditnotes();
                                        SaveAndUpdateDebitCreditNote();
                                    }
                                    if (retValue)
                                        SaveIntblTrnac();
                                    if (retValue)
                                        General.CommitTransaction();
                                    else
                                        General.RollbackTransaction();
                                    LockTable.UnLockTables();
                                    System.IO.File.Delete(General.GetDebtorSaleTempFile());
                                    if (retValue)
                                    {
                                        UpdateClosingStockinCache();
                                        string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                                        PSDialogResult result;
                                        if (printData)
                                        {
                                            result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                            Print();
                                        }
                                        else
                                        {
                                            result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                            if (result == PSDialogResult.Print)
                                                Print();
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
                                    General.BeginTransaction();
                                    _SSSale.ModifiedBy = General.CurrentUser.Id;
                                    _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                    _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                                    retValue = _SSSale.UpdateDetails();
                                    if (retValue)
                                        retValue = DeletePreviousRows();
                                    if (retValue)
                                        retValue = AddPreviousStock();
                                    //if (retValue)
                                    //    General.CommitTransaction();
                                    //else
                                    //{
                                    //    General.RollbackTransaction();
                                    //    PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                    //    retValue = false;
                                    //}
                                    if (retValue)
                                    {
                                    //    General.BeginTransaction();
                                        retValue = SaveparticularsProductwise();

                                        if (retValue)
                                            retValue = ReduceStockIntblStock();
                                        if (retValue)
                                        {
                                            clearPreviousdebitcreditnotes();
                                            SaveAndUpdateDebitCreditNote();
                                        }
                                        if (retValue)
                                            retValue = SaveIntblTrnac();
                                        if (retValue)
                                            General.CommitTransaction();
                                        else
                                        {
                                            General.RollbackTransaction();
                                            //retValue = _SSSale.ReverseUpdateDetails();
                                            //retValue = AddPreviousRows();
                                            //retValue = ReducePreviousStock();
                                            PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                            retValue = false;
                                        }
                                    }
                                    LockTable.UnLockTables();
                                    if (retValue)
                                    {
                                        UpdateClosingStockinCache();
                                        _SSSale.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                        _SSSale.AddDetailsInChangedMaster();
                                        AddPreviousRowsInChangedDetail();
                                        string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                                        PSDialogResult result;
                                        if (printData)
                                        {
                                            result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                            Print();
                                        }
                                        else
                                        {
                                            result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                            if (result == PSDialogResult.Print)
                                                Print();
                                        }
                                        _SavedID = _SSSale.Id;
                                        retValue = true;
                                    }

                                }
                            }
                        }
                        else
                        {
                            LockTable.UnLockTables();
                            _errorMessage = new System.Text.StringBuilder();
                            _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                            foreach (string _message in _SSSale.ValidationMessages)
                            {
                                _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                            }
                            MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    catch (Exception Ex)
                    {
                        Log.WriteException(Ex);
                    }
                }
            }
            LockTable.UnLockTables();
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            if (ID != null && ID != "")
            {
                try
                {
                    _SSSale.Id = ID;
                    if (Vmode == "C")
                        _SSSale.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _SSSale.ReadDetailsByIDForDeleted();
                    else
                        _SSSale.ReadDetailsByID();
                    FillDoctorCombo();
                    mcbDoctor.SelectedID = _SSSale.DocID;
                    if (_SSSale.DocID == string.Empty)
                        mcbDoctor.Text = _SSSale.DoctorName;
                    txtVouType.Text = _SSSale.CrdbVouType;
                    if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForCreditSale)
                        btnPaymentHistory.Visible = false;
                    else
                    {
                        btnPaymentHistory.Visible = true;
                        BindPaymentDetails();
                    }
                    FillPartyCombo();
                    mcbCreditor.SelectedID = _SSSale.AccountID;
                    BindTempGrid();
                    BindPaymentDetails();
                    InitialisempPVC1(Vmode);
                    txtAddress1.Text = _SSSale.PatientAddress1;
                    txtAddress2.Text = _SSSale.PatientAddress2;
                    txtPatient.Text = _SSSale.ShortName;
                    txtPatientAddress.Text = _SSSale.PatientShortAddress;
                    txtMobileNumber.Text = _SSSale.Telephone;
                    txtNarration.Text = _SSSale.CrdbNarration.ToString();
                    txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString().Trim();
                    txtTokenNumber.Text = _SSSale.TokenNumber.ToString();
                    if (_SSSale.CrdbVat5 >= 0)
                        txtVatInput5per.Text = _SSSale.CrdbVat5.ToString("#0.00");
                    if (_SSSale.CrdbVat12point5 >= 0)
                        txtVatInput12point5per.Text = _SSSale.CrdbVat12point5.ToString("#0.00");
                    txtAmount.Text = _SSSale.CrdbAmount.ToString("#0.00");
                    txtDiscPercent.Text = _SSSale.CrdbDiscPer.ToString("#0.00");
                    txtRoundAmount.Text = _SSSale.CrdbRoundAmount.ToString("#0.00");
                    txtBillAmount.Text = _SSSale.CrdbBillAmount.ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text;
                    txtTotalAmount.Text = _SSSale.CrdbTotalAmount.ToString("#0.00");
                    if (_SSSale.CrdbRoundAmount != 0)
                        cbRound.Checked = true;
                    txtDiscAmount.Text = _SSSale.CrdbDiscAmt.ToString("#0.00");
                    DateTime mydate = new DateTime(Convert.ToInt32(_SSSale.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    txtAddOn.Text = _SSSale.CrdbAddOn.ToString("#0.00");
                    txtCreditNote.Text = _SSSale.CrNoteAmount.ToString("#0.00");
                    txtDebitNote.Text = _SSSale.DbNoteAmount.ToString("#0.00");
                    txtAmountforZeroVAT.Text = _SSSale.CrdbAmtForZeroVAT.ToString("#0.00");
                    txtAmountVAT12Point5Per.Text = _SSSale.CrdbAmountVat12point5.ToString("#0.00");
                    txtAmountVAT5Per.Text = _SSSale.CrdbAmountVat5.ToString("#0.00");
                    if (mcbDoctor.SelectedID != string.Empty)
                    {
                        _SSSale.DoctorName = mcbDoctor.SeletedItem.ItemData[1];
                        if (mcbDoctor.SeletedItem.ItemData[2] != null && mcbDoctor.SeletedItem.ItemData[2].ToString() != string.Empty)
                            _SSSale.DoctorAddress = mcbDoctor.SeletedItem.ItemData[2];
                    }

                    txtDoctorAddress.Text = _SSSale.DoctorAddress.ToString();
                    if (txtVouType.Text == FixAccounts.VoucherTypeForCashSale)
                        cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                    else if (txtVouType.Text == FixAccounts.VoucherTypeForCreditStatementSale)
                        cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                    else
                        cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;

                    if (_SSSale.CrdbRoundAmount != 0)
                        cbRound.Checked = true;
                    else
                        cbRound.Checked = false;
                    NoofRows();
                    txtAddress1.Enabled = false;
                    txtAddress2.Enabled = false;

                    if (_Mode == OperationMode.View)
                    {
                        mpPVC1.ColumnsMain["Col_ProductName"].ReadOnly = true;
                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                        mpPVC1.IsAllowDelete = false;
                        mcbCreditor.Enabled = false;
                        mcbDoctor.Enabled = false;
                    }
                    else
                    {
                        mpPVC1.ColumnsMain["Col_ProductName"].ReadOnly = false;
                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = false;
                        mpPVC1.IsAllowDelete = true;
                        mcbCreditor.Enabled = true;
                        mcbDoctor.Enabled = true;
                        mpPVC1.SetFocus(1);
                        mpPVC1.Select();
                        if (_Mode == OperationMode.Edit)
                        {
                            cbTransactionType.Enabled = false;
                        }

                    }

                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            return true;
        }

        #endregion IDetail Control

        #region IDetail Members
        public override void ReFillData()
        {
            try
            {
                this.Focus();
                FillPartyCombo();
                FillTransactionType();
                mcbCreditor.SelectedID = _SSSale.AccountID;
                FillCreditDebitNote();
                RefreshProductGrid();

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {

                if (keyPressed == Keys.B && modifier == Keys.Alt)
                {
                    mcbCreditor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    if (btnClone.Enabled)
                        btnClone.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    mcbDoctor.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.E && modifier == Keys.Alt)
                {
                    cbEditRate.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.I && modifier == Keys.Alt)
                {
                    if (btnIfDebitCredit.Visible)
                    {
                        BtnIfDebitCreditNoteClick();
                        retValue = true;
                    }
                }
                if (keyPressed == Keys.K && modifier == Keys.Alt)
                {
                    txtTokenNumber.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.L && modifier == Keys.Alt)
                {
                    cbFill.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    txtNarration.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {

                    if (pnlDebitCreditNote.Visible)
                    {
                        btnOKCreditDebitNoteClick();
                    }
                    else if (pnlDebtorProduct.Visible)
                    {
                        btnOKFillClick();
                    }
                    else
                        txtAddOn.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.P && modifier == Keys.Alt)
                {
                    txtPatient.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.S && modifier == Keys.Alt)
                {
                    if (txtDiscPercent.Visible)
                    {
                        txtDiscPercent.Focus();
                        retValue = true;
                    }
                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    cbTransferSale.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.U && modifier == Keys.Alt)
                {
                    if (cbRound.Checked == true)
                        cbRound.Checked = false;
                    else
                        cbRound.Checked = true;
                    retValue = true;
                }
                if (keyPressed == Keys.V && modifier == Keys.Alt)
                {
                    datePickerBillDate.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Y && modifier == Keys.Alt)
                {
                    cbTransactionType.Focus();
                    retValue = true;

                }
                if (keyPressed == Keys.Escape)
                {
                    //if (dgBatch.Visible)
                    //{
                    //    dgBatch.Visible = false;
                    //    retValue = true;
                    //}
                    //else
                    if (pnlDebitCreditNote.Visible)
                    {
                        btnOKCreditDebitNoteClick();
                        retValue = true;
                    }
                    else
                        retValue = Exit();
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

        private bool DeletePreviousRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _SSSale.DeletePreviousRecords();
                returnVal = _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }
        private bool CheckForStockintblStock()
        {
            bool retValue = true;
            string mlastsaleid;
            string mproductname;
            string mpack;
            string muom;
            try
            {
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null && prodrow.Cells["Col_Quantity"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        mproductname = prodrow.Cells["Col_ProductName"].Value.ToString();
                        mpack = prodrow.Cells["Col_Pack"].Value.ToString();
                        muom = prodrow.Cells["Col_UOM"].Value.ToString();
                        mlastsaleid = "";
                        _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                        _SSSale.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        if (prodrow.Cells["Col_StockID"].Value != null)
                            mlastsaleid = prodrow.Cells["Col_StockID"].Value.ToString();
                        _SSSale.LastStockID = mlastsaleid;
                        int stockavailable = 0;
                        stockavailable = _SSSale.GetStockByStockID();
                        double  ssstockavailable = Math.Floor(Convert.ToDouble(stockavailable) /Convert.ToDouble(muom));
                        stockavailable = Convert.ToInt32(ssstockavailable);
                        int tempstock = 0;

                        foreach (DataGridViewRow temprow in dgtemp.Rows)
                        {
                            string mtempid = "";
                            if (temprow.Cells["Temp_StockID"].Value != null)
                                mtempid = temprow.Cells["Temp_StockID"].Value.ToString();
                            if (mtempid == mlastsaleid && mtempid != "")
                            {
                                tempstock = Convert.ToInt32(temprow.Cells["Temp_Quantity"].Value.ToString());
                                break;
                            }
                        }

                        if (stockavailable + tempstock < _SSSale.Quantity)
                        {
                            if (stockavailable == 0)
                                MessageBox.Show("Stock Not Available For " + mproductname + " " + muom + " " + mpack);
                            else
                                MessageBox.Show("Stock Available : " + stockavailable + " : For " + mproductname + " " + muom + " " + mpack);
                            retValue = false;
                            LockTable.UnLockTables();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LockTable.UnLockTables();
                Log.WriteException(ex);
                retValue = false;
            }
            return retValue;


        }
        private void CalculateProfitPercent()
        {
            _SSSale.ProfitPercentByPurchaseRate = 0;
            _SSSale.ProfitPercentBySaleRate = 0;
            _SSSale.TotalProfitPercentByPurchaseRate = 0;
            _SSSale.TotalProfitPercentBySaleRate = 0;
            _SSSale.TotalProfitInRupees = 0;

            double mqty = 0;
            double mpurrate = 0;
            double mtraderate = 0;
            double msalerate = 0;
            double mpakn = 0;
            double mvatper = 0;
            double mvatamt = 0;

            try
            {
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    mqty = 0;
                    mpurrate = 0;
                    mtraderate = 0;
                    msalerate = 0;
                    mpakn = 0;
                    mvatper = 0;
                    mvatamt = 0;

                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        if (prodrow.Cells["Col_UOM"].Value != null)
                            double.TryParse(prodrow.Cells["Col_UOM"].Value.ToString(), out mpakn);
                        if (prodrow.Cells["Col_Quantity"].Value != null)
                            double.TryParse(prodrow.Cells["Col_Quantity"].Value.ToString().Trim(), out mqty);
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                        _SSSale.PurchaseRate = mpurrate;
                        if (prodrow.Cells["Col_TradeRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                        _SSSale.TradeRate = mtraderate;
                        if (prodrow.Cells["Col_VATPer"].Value != null)
                            double.TryParse(prodrow.Cells["Col_VATPer"].Value.ToString(), out mvatper);
                        mvatamt = Math.Round((mtraderate * mvatper) / 100, 2);
                        double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                        _SSSale.SaleRate = msalerate;
                        _SSSale.ProfitPercentBySaleRate = Math.Round((msalerate - (mpurrate + mvatamt)) / msalerate, 4);
                        _SSSale.ProfitPercentByPurchaseRate = Math.Round((msalerate - (mpurrate + mvatamt)) / (mpurrate + mvatamt), 4);
                        _SSSale.TotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
                        _SSSale.TotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
                        _SSSale.ProfitInRupees = Math.Round(((msalerate - (mpurrate + mvatamt)) / mpakn) * mqty, 2);
                        _SSSale.TotalProfitInRupees += _SSSale.ProfitInRupees;
                        prodrow.Cells["Col_ProfitPercentBySaleRate"].Value = _SSSale.ProfitPercentBySaleRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _SSSale.ProfitPercentByPurchaseRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitInRupees"].Value = _SSSale.ProfitInRupees.ToString("#0.00");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private bool SaveparticularsProductwise()
        {
            {
                bool returnVal = false;
                _SSSale.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                    {
                        if (prodrow.Cells["Col_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                        {
                            _SSSale.SerialNumber += 1;
                            int mqty = 0;
                            double mpurrate = 0;
                            double mtraderate = 0;
                            double mmrp = 0;
                            double msalerate = 0;
                            double mvatper = 0;
                            double mamt = 0;
                            double mvatamt = 0;
                            string mlastsaleid = "";
                            _SSSale.ProfitPercentBySaleRate = 0;
                            _SSSale.ProfitPercentByPurchaseRate = 0;
                            _SSSale.ProfitInRupees = 0;

                            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                            _SSSale.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            _SSSale.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            _SSSale.ProdPakn = Convert.ToInt32(prodrow.Cells["Col_UOM"].Value.ToString());
                            if (prodrow.Cells["Col_ExpiryDate"].Value != null)
                                _SSSale.ExpiryDate = prodrow.Cells["Col_ExpiryDate"].Value.ToString();
                            int.TryParse(prodrow.Cells["Col_Quantity"].Value.ToString().Trim(), out mqty);
                            _SSSale.Quantity = mqty;
                            if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                                double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            _SSSale.PurchaseRate = mpurrate;
                            if (prodrow.Cells["Col_TradeRate"].Value != null)
                                double.TryParse(prodrow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(prodrow.Cells["Col_MRP"].Value.ToString().Trim(), out mmrp);
                            _SSSale.MRP = mmrp;
                            double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                            _SSSale.SaleRate = msalerate;
                            double.TryParse(prodrow.Cells["Col_VATPer"].Value.ToString().Trim(), out mvatper);
                            _SSSale.VATPer = mvatper;
                            double.TryParse(prodrow.Cells["Col_VATAmount"].Value.ToString().Trim(), out mvatamt);
                            _SSSale.VATAmount = mvatamt;
                            double.TryParse(prodrow.Cells["Col_Amount"].Value.ToString().Trim(), out mamt);
                            _SSSale.Amount = mamt;
                            if (prodrow.Cells["Col_StockID"].Value != null)
                                mlastsaleid = (prodrow.Cells["Col_StockID"].Value.ToString());

                            if (prodrow.Cells["Col_ProfitPercentBySaleRate"].Value != null)
                                _SSSale.ProfitPercentBySaleRate = Convert.ToDouble(prodrow.Cells["Col_ProfitPercentBySaleRate"].Value.ToString());

                            if (prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value != null)
                                _SSSale.ProfitPercentByPurchaseRate = Convert.ToDouble(prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value.ToString());

                            if (prodrow.Cells["Col_ProfitInRupees"].Value != null)
                                _SSSale.ProfitInRupees = Convert.ToDouble(prodrow.Cells["Col_ProfitInRupees"].Value.ToString());
                            _SSSale.LastStockID = mlastsaleid;
                            returnVal = _SSSale.AddProductDetailsSS();
                            if (returnVal == false)
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                    returnVal = false;
                }
                return returnVal;
            }
        }
        private bool AddPreviousRows()
        {
            {
                bool returnVal = false;
                _SSSale.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in dgtemp.Rows)
                    {
                        if (prodrow.Cells["Temp_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0)
                        {
                            _SSSale.SerialNumber += 1;
                            int mqty = 0;
                            double mpurrate = 0;
                            double mtraderate = 0;
                            double mmrp = 0;
                            double msalerate = 0;
                            double mvatper = 0;
                            double mamt = 0;
                            double mvatamt = 0;
                            string mlastsaleid = "";

                            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                            _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                            _SSSale.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
                            if (prodrow.Cells["Temp_ExpiryDate"].Value != null)
                                _SSSale.ExpiryDate = prodrow.Cells["Temp_ExpiryDate"].Value.ToString();
                            int.TryParse(prodrow.Cells["Temp_Quantity"].Value.ToString().Trim(), out mqty);
                            _SSSale.Quantity = mqty;
                            if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            _SSSale.PurchaseRate = mpurrate;
                            if (prodrow.Cells["Temp_TradeRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_TradeRate"].Value.ToString(), out mtraderate);
                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(prodrow.Cells["Temp_MRP"].Value.ToString().Trim(), out mmrp);
                            _SSSale.MRP = mmrp;
                            double.TryParse(prodrow.Cells["Temp_SaleRate"].Value.ToString().Trim(), out msalerate);
                            _SSSale.SaleRate = msalerate;
                            double.TryParse(prodrow.Cells["Temp_VATPer"].Value.ToString().Trim(), out mvatper);
                            _SSSale.VATPer = mvatper;
                            double.TryParse(prodrow.Cells["Temp_VATAmount"].Value.ToString().Trim(), out mvatamt);
                            _SSSale.VATAmount = mvatamt;
                            double.TryParse(prodrow.Cells["Temp_Amount"].Value.ToString().Trim(), out mamt);
                            _SSSale.Amount = mamt;
                            if (prodrow.Cells["Temp_StockID"].Value != null)
                                mlastsaleid = (prodrow.Cells["Temp_StockID"].Value.ToString());
                            _SSSale.LastStockID = mlastsaleid;
                            returnVal = _SSSale.AddProductDetailsSS();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                    returnVal = false;
                }
                return returnVal;
            }
        }
        private bool AddPreviousRowsInDeleteDetail()
        {
            {
                bool returnVal = false;
                _SSSale.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in dgtemp.Rows)
                    {
                        if (prodrow.Cells["Temp_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0)
                        {
                            _SSSale.SerialNumber += 1;
                            int mqty = 0;
                            double mpurrate = 0;
                            double mtraderate = 0;
                            double mmrp = 0;
                            double msalerate = 0;
                            double mvatper = 0;
                            double mamt = 0;
                            double mvatamt = 0;
                            string mlastsaleid = "";

                            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                            _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                            _SSSale.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
                            if (prodrow.Cells["Temp_ExpiryDate"].Value != null)
                                _SSSale.ExpiryDate = prodrow.Cells["Temp_ExpiryDate"].Value.ToString();
                            int.TryParse(prodrow.Cells["Temp_Quantity"].Value.ToString().Trim(), out mqty);
                            _SSSale.Quantity = mqty;
                            if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            _SSSale.PurchaseRate = mpurrate;
                            if (prodrow.Cells["Temp_TradeRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_TradeRate"].Value.ToString(), out mtraderate);
                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(prodrow.Cells["Temp_MRP"].Value.ToString().Trim(), out mmrp);
                            _SSSale.MRP = mmrp;
                            double.TryParse(prodrow.Cells["Temp_SaleRate"].Value.ToString().Trim(), out msalerate);
                            _SSSale.SaleRate = msalerate;
                            double.TryParse(prodrow.Cells["Temp_VATPer"].Value.ToString().Trim(), out mvatper);
                            _SSSale.VATPer = mvatper;
                            double.TryParse(prodrow.Cells["Temp_VATAmount"].Value.ToString().Trim(), out mvatamt);
                            _SSSale.VATAmount = mvatamt;
                            double.TryParse(prodrow.Cells["Temp_Amount"].Value.ToString().Trim(), out mamt);
                            _SSSale.Amount = mamt;
                            if (prodrow.Cells["Temp_StockID"].Value != null)
                                mlastsaleid = (prodrow.Cells["Temp_StockID"].Value.ToString());
                            _SSSale.LastStockID = mlastsaleid;
                            returnVal = _SSSale.AddDeletedProductDetailsSS();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                    returnVal = false;
                }
                return returnVal;
            }
        }
        private bool AddPreviousRowsInChangedDetail()
        {
            {
                bool returnVal = false;
                _SSSale.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in dgtemp.Rows)
                    {
                        if (prodrow.Cells["Temp_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0)
                        {
                            _SSSale.SerialNumber += 1;
                            int mqty = 0;
                            double mpurrate = 0;
                            double mtraderate = 0;
                            double mmrp = 0;
                            double msalerate = 0;
                            double mvatper = 0;
                            double mamt = 0;
                            double mvatamt = 0;
                            string mlastsaleid = "";

                            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                            _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                            _SSSale.Expiry = prodrow.Cells["Temp_Expiry"].Value.ToString();
                            if (prodrow.Cells["Temp_ExpiryDate"].Value != null)
                                _SSSale.ExpiryDate = prodrow.Cells["Temp_ExpiryDate"].Value.ToString();
                            int.TryParse(prodrow.Cells["Temp_Quantity"].Value.ToString().Trim(), out mqty);
                            _SSSale.Quantity = mqty;
                            if (prodrow.Cells["Temp_PurchaseRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            _SSSale.PurchaseRate = mpurrate;
                            if (prodrow.Cells["Temp_TradeRate"].Value != null)
                                double.TryParse(prodrow.Cells["Temp_TradeRate"].Value.ToString(), out mtraderate);
                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(prodrow.Cells["Temp_MRP"].Value.ToString().Trim(), out mmrp);
                            _SSSale.MRP = mmrp;
                            double.TryParse(prodrow.Cells["Temp_SaleRate"].Value.ToString().Trim(), out msalerate);
                            _SSSale.SaleRate = msalerate;
                            double.TryParse(prodrow.Cells["Temp_VATPer"].Value.ToString().Trim(), out mvatper);
                            _SSSale.VATPer = mvatper;
                            double.TryParse(prodrow.Cells["Temp_VATAmount"].Value.ToString().Trim(), out mvatamt);
                            _SSSale.VATAmount = mvatamt;
                            double.TryParse(prodrow.Cells["Temp_Amount"].Value.ToString().Trim(), out mamt);
                            _SSSale.Amount = mamt;
                            if (prodrow.Cells["Temp_StockID"].Value != null)
                                mlastsaleid = (prodrow.Cells["Temp_StockID"].Value.ToString());
                            _SSSale.LastStockID = mlastsaleid;
                            returnVal = _SSSale.AddChangedProductDetailsSS();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                    returnVal = false;
                }
                return returnVal;
            }
        }

        private bool ReducePreviousStock()
        {
            bool returnVal = false;
            string mlastsaleid;
            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0)
                    {
                        mlastsaleid = "";
                        _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        if (prodrow.Cells["Temp_StockID"].Value != null)
                            mlastsaleid = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _SSSale.LastStockID = mlastsaleid;
                        string ifRecordFound = "";
                        ifRecordFound = _SSSale.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                        {
                            returnVal = _SSSale.UpdateIntblStock();
                            if (returnVal)
                                returnVal = _SSSale.UpdateSaleStockInMasterProduct();
                            if (returnVal)
                            {
                                if (_SSSale.IfAddToShortList())
                                {
                                    Filldailyshortlist();
                                }
                            }
                            else
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;

        }
        private bool SaveIntblTrnac()
        {

            bool retValue = false;
            try
            {
                double mdebit = 0;
                double mdiscper = 0;
                double maddon = 0;
                double mdiscamount = 0;
                double mvat5per = 0;
                double mvat12point5per = 0;
                double mamtforzerovat = 0;
                double mbillamount = 0;
                double mround = 0;
                double mcreditnoteamt = 0;
                double mdebitnoteamt = 0;
                if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                    _SSSale.ShortNameForNarration = _SSSale.ShortName;
                else
                    _SSSale.ShortNameForNarration = "";
                double.TryParse(txtCreditNote.Text.ToString(), out mcreditnoteamt);
                _SSSale.CrNoteAmount = mcreditnoteamt;
                double.TryParse(txtDebitNote.Text.ToString(), out mdebitnoteamt);
                _SSSale.DbNoteAmount = mdebitnoteamt;
                double.TryParse(txtVatInput5per.Text, out mvat5per);
                _SSSale.CrdbVat5 = Math.Round(mvat5per, 2);
                double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                _SSSale.CrdbVat12point5 = Math.Round(mvat12point5per, 2);
                double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamtforzerovat);
                _SSSale.CrdbAmtForZeroVAT = mamtforzerovat;
                double.TryParse(txtDiscPercent.Text, out mdiscper);
                _SSSale.CrdbDiscPer = mdiscper;
                double.TryParse(txtDiscAmount.Text, out mdiscamount);
                _SSSale.CrdbDiscAmt = mdiscamount;
                double.TryParse(txtBillAmount.Text, out mbillamount);
                _SSSale.CrdbAmountNet = mbillamount;
                double.TryParse(txtRoundAmount.Text, out mround);
                mround = _SSSale.CrdbRoundAmount;
                double.TryParse(txtAddOn.Text, out maddon);
                _SSSale.CrdbAddOn = maddon;

                mdebit = Math.Round(mbillamount - Math.Round(_SSSale.CrdbVat5, 2) - Math.Round(_SSSale.CrdbVat12point5, 2) + mdiscamount - maddon - mround - mamtforzerovat + mcreditnoteamt - mdebitnoteamt, 2);

                if (mamtforzerovat > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVATZeroSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mamtforzerovat;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;

                }

                if (Math.Round(_SSSale.CrdbVat5, 2) > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(_SSSale.CrdbVat5, 2);
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;
                }
                if (Math.Round(_SSSale.CrdbVat12point5, 2) > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput12point5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(_SSSale.CrdbVat12point5, 2);
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;
                }
                if (maddon > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountAddonSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = maddon;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;
                }
                if (mround < 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = (mround * -1);
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;
                }
                if (mround > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mround;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;
                }

                if (mdiscamount > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountCashDiscountSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mdiscamount;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;
                }

                if (mcreditnoteamt > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountSalesReturn;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mcreditnoteamt;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;
                }
                if (mdebitnoteamt > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountDebitNoteSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mdebitnoteamt;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;
                }
                if (mdebit > 0)
                {

                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.DebitAccount = FixAccounts.AccountCashSale;
                    else
                        _SSSale.DebitAccount = FixAccounts.AccountCashCreditSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mdebit;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;
                }
                if (mbillamount > 0)
                {
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.DebitAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.DebitAccount = _SSSale.AccountID;

                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCashSale;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;

                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mbillamount;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                    if (retValue == false)
                        return retValue;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private void Filldailyshortlist()
        {
            try
            {
                _SSSale.ShortListID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _SSSale.AddToShortList();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool ReduceStockIntblStock()
        {
            bool returnVal = false;
            string mlastsaleid;
            try
            {
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        mlastsaleid = "";
                        _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                        _SSSale.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        _SSSale.ProdPakn = Convert.ToInt32(prodrow.Cells["Col_UOM"].Value);
                        if (prodrow.Cells["Col_StockID"].Value != null)
                            mlastsaleid = prodrow.Cells["Col_StockID"].Value.ToString();
                        _SSSale.LastStockID = mlastsaleid;
                        string ifRecordFound = "";
                        ifRecordFound = _SSSale.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                        {
                            returnVal = _SSSale.UpdateIntblStockForDistributor();
                            if (returnVal)
                                returnVal = _SSSale.UpdateSaleStockInMasterProductForDistributor();
                            if (returnVal)
                            {
                                if (_SSSale.IfAddToShortList())
                                    Filldailyshortlist();
                            }
                            else
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;

        }

        private bool UpdateClosingStockinCache()
        {
            bool returnVal = false;
            try
            {
                General.UpdateProductListCacheTest(mpPVC1.Rows, "Col_ProductID", dgtemp.Rows, "Temp_ProductID");
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }
        private bool AddPreviousStock()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0)
                    {
                        _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _SSSale.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value);
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _SSSale.LastStockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _SSSale.ProdPakn = Convert.ToInt32(prodrow.Cells["Temp_UOM"].Value.ToString());

                        string ifRecordFound = "";
                        ifRecordFound = _SSSale.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                            returnVal = _SSSale.UpdateIntblStockAddForDistributor();
                        returnVal = _SSSale.UpdateDebtorSaleStockInMasterProductAddFromTempForDistributor();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }

       
        #endregion

        # region Internal methods
        private void InitialisempPVC1(string vmode)
        {
            try
            {
                ConstructMainColumns();
                ConstructProductSelectionListGridColumns();
                ConstructBatchGridColumns();
                ConstructmpMSVC1Columns();

                FormatGrids();

                pnlDebtorProduct.Visible = false;

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
                mpPVC1.DataSourceMain = dtable;

                Product prod = new Product();
                //mpPVC1.DataSourceProductList = PharmaSysRetailPlusCache.GetProductData();
                mpPVC1.DataSourceProductList = General.ProductList;
                string tempFileName = General.GetDebtorSaleTempFile();
                if (_Mode == OperationMode.Add && File.Exists(tempFileName))
                {
                    mpPVC1.DataSourceMain = null;
                    mpPVC1.Rows.Clear();

                    DataSet ds = new DataSet();
                    ds.ReadXml(tempFileName);
                    mpPVC1.DataSourceMain = ds.Tables[0];
                    mpPVC1.Bind();

                    mpPVC1.IsAllowNewRow = true;
                    if (_SSSale.AddNewRowCheck(mpPVC1))
                        mpPVC1.Rows.Add(1);

                    mpPVC1.AddRowsInStockTempTable();
                    CalculateAmount(-1);
                    CalculateAllAmounts();
                }
                else
                    mpPVC1.Bind();

                if (_Mode == OperationMode.Edit && _SSSale.AddNewRowCheck(mpPVC1))
                    mpPVC1.Rows.Add();
                mpPVC1.ClearSelection();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FormatGrids()
        {
            mpPVC1.BatchGridShowColumnName = "Col_UOM";
            mpPVC1.NewRowColumnName = "Col_Quantity";
            mpPVC1.DoubleColumnNames.Add("Col_MRP");
            mpPVC1.NumericColumnNames.Add("Col_Quantity");
            mpPVC1.DoubleColumnNames.Add("Col_VATPer");
            mpPVC1.DoubleColumnNames.Add("Col_PurchaseRate");
            mpPVC1.DoubleColumnNames.Add("Col_Amount");
            mpPVC1.DoubleColumnNames.Add("Col_SaleRate");
            dgCreditNote.DoubleColumnNames.Add("Col_AmountNet");
            mpPVC1.ProductGridClosingStockColumnName = "Col_ClosingStock";
            mpPVC1.MainGridSoldQuantityColumnName = "Col_Quantity";

            mpPVC1.ClearSelection();
        }
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
                int _RowIndex = 0;
                if (dgPaymentDetails.Rows.Count > 0)
                {

                    dgPaymentDetails.DataSource = null;
                    ConstructPaymentDetailsColumns();
                    //   dgPaymentDetails.Rows.Clear();
                }
                foreach (DataRow dr in _PaymentDetailsBindingSource.Rows)
                {
                    _RowIndex = dgPaymentDetails.Rows.Add();
                    string voudt = "";
                    DataGridViewRow currentdr = dgPaymentDetails.Rows[_RowIndex];
                    currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_CrdbID"].Value = dr["CBID"].ToString();
                    currentdr.Cells["Col_PurID"].Value = dr["MasterSaleID"].ToString();
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;
                    currentdr.Cells["Col_AmountNet"].Value = dr["ClearAmount"].ToString();
                    currentdr.Cells["Col_IfChequeReturn"].Value = dr["IfChequeReturn"].ToString();
                    if (dr["IfChequeReturn"].ToString() == "Y")
                        currentdr.DefaultCellStyle.BackColor = Color.DeepPink;


                    //  currentdr.Cells["Col_Narr"].Value = dr["Narration"].ToString();
                    //   if (dr["AmountClear"] != DBNull.Value && dr["AmountClear"].ToString() != "")
                    //        double.TryParse(dr["AmountClear"].ToString(), out amtclear);
                    //    currentdr.Cells["Col_AmountClear"].Value = dr["AmountClear"].ToString();
                    //   if (_Mode == OperationMode.Delete)
                    //        currentdr.Cells["Col_Check"].Value = false;
                    //     else if (amtclear != 0)
                    //          currentdr.Cells["Col_Check"].Value = true;
                    //  _RowIndex += 1;
                }
                //  dgPaymentDetails.DataSource = _PaymentDetailsBindingSource;
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
                _SSSale.SaleSubType = FixAccounts.SubTypeForDistributorSale;
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtVouType.Text = "   ";
                datePickerBillDate.ResetText();
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtTotalAmount.Text = "0.00";
                txtAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtAddOn.Text = "0.00";
                txtPendingBalance.Text = "0.00";
                txtVatInput12point5per.Text = "0.00";
                txtVatInput5per.Text = "0.00";
                txtAmountforZeroVAT.Text = "0.00";
                txtTotalAmountForDiscount.Text = "0.00";
                txtRoundAmount.Text = "0.00";
                txtNoOfRows.Text = "";
                txtCreditNote.Text = "0.00";
                txtDebitNote.Text = "0.00";
                btnIfDebitCredit.Visible = false;
                dgCreditNote.Visible = false;
                pnlDebitCreditNote.Visible = false;
                lblCreditNote.Visible = false;
                lblDebitNote.Visible = false;
                txtCreditNote.Visible = false;
                txtDebitNote.Visible = false;
                pnlDebitCreditNote.SendToBack();
                pnlPaymentDetails.SendToBack();
                pnlPaymentDetails.Visible = false;
                mcbCreditor.Text = "";
                mcbCreditor.SelectedID = "";
                mcbDoctor.SelectedID = "";
                mcbDoctor.Text = string.Empty;
                txtMobileNumber.Text = string.Empty;
                mcbPrescription.SelectedID = "";
                mcbCreditor.Focus();
                lblFooterMessage.Text = "";
                cbTransactionType.Enabled = true;
                if (_Mode != OperationMode.Add)
                {
                    mcbCreditor.Enabled = false;
                    txtTokenNumber.Enabled = false;
                    txtVouchernumber.ReadOnly = false;
                    txtVouchernumber.Enabled = true;
                    if (_Mode != OperationMode.Edit)
                        txtPatient.Enabled = false;
                    txtVouchernumber.Focus();
                    btnPaymentHistory.Visible = true;
                }
                else
                {
                    mcbCreditor.Enabled = true;
                    mcbDoctor.Enabled = true;
                    txtPatient.Enabled = true;
                    txtTokenNumber.Enabled = true;
                    txtVouchernumber.ReadOnly = true;
                    txtVouchernumber.Enabled = false;
                    mcbCreditor.Focus();
                    btnPaymentHistory.Visible = false;
                }
                if (General.CurrentSetting.MsetAskOperatorOtherThanVoucherSale == "Y")
                {
                    txtOperator.Visible = true;
                    lblOperator.Visible = true;
                }
                else
                {
                    txtOperator.Visible = false;
                    lblOperator.Visible = false;
                }
                if (General.CurrentSetting.MsetSaleAskRoundinginSale == "Y")
                {
                    cbRound.Visible = true;
                    txtRoundAmount.Visible = true;
                }
                else
                {
                    cbRound.Visible = false;
                    txtRoundAmount.Visible = false;
                }
                if (General.CurrentSetting.MsetSaleRoundOff == "Y")
                    cbRound.Checked = true;
                else
                    cbRound.Checked = false;
                pnlClone.Visible = false;
                txtclonevouno.Text = "";
                cbclonevoutype.Text = "";
                InitializeScreen();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void InitializeScreen()
        {
            try
            {
                btnIfDebitCredit.Visible = false;
                dgPaymentDetails.Visible = false;
                dgCreditNote.Visible = false;
                pnlDebitCreditNote.Visible = false;
                pnlPaymentDetails.Visible = false;
                lblCreditNote.Visible = false;
                lblDebitNote.Visible = false;
                txtCreditNote.Visible = false;
                txtDebitNote.Visible = false;
                pnlCenter.BringToFront();
                pnlDebitCreditNote.Visible = false;
                dgCreditNote.Visible = false;
                pnlDebtorProduct.Visible = false;
                mpMSVCFill.Visible = false;
                pnlCenter.Dock = DockStyle.Fill;
                mpPVC1.Dock = DockStyle.Fill;
                mcbCreditor.Enabled = true;
                if (_Mode == OperationMode.Edit)
                {
                    txtVouchernumber.ReadOnly = false;
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.Focus();
                    mcbCreditor.Enabled = false;
                }
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

        private void FillCreditDebitNote()
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
            {
                try
                {
                    ConstructCreditNoteColumns();
                    dgCreditNote.DoubleColumnNames.Add("Col_AmountNet");
                    CreditNoteStock crdb = new CreditNoteStock();

                    dt = crdb.GetOverviewDataForDebtorSale(mcbCreditor.SelectedID, _SSSale.Id);
                    if (dt != null)
                        retValue = BindCreditNoteDebitNote(dt);

                    if (dt.Rows.Count > 0)
                    {
                        btnIfDebitCredit.Visible = true;
                        lblCreditNote.Visible = true;
                        lblDebitNote.Visible = true;
                        txtCreditNote.Visible = true;
                        txtDebitNote.Visible = true;
                    }

                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

            }

        }

        private void FillDoctorCombo()
        {
            try
            {
                mcbDoctor.SelectedID = null;
                mcbDoctor.SourceDataString = new string[4] { "DocID", "DocName", "DocAddress", "DocShortNameAddress" };
                mcbDoctor.ColumnWidth = new string[4] { "0", "200", "300", "0" };
                mcbDoctor.ValueColumnNo = 0;
                mcbDoctor.UserControlToShow = new UclDoctor();
                Doctor _Doctor = new Doctor();
                DataTable dtabled = _Doctor.GetSSDoctorsList();
                mcbDoctor.FillData(dtabled);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillPrescription()
        {
            try
            {
                mcbPrescription.SelectedID = null;
                mcbPrescription.SourceDataString = new string[2] { "prescriptionID", "PrescriptionName" };
                mcbPrescription.ColumnWidth = new string[2] { "0", "200" };
                mcbPrescription.ValueColumnNo = 0;
                mcbPrescription.UserControlToShow = new UclPrescription();
                Prescription _Prescription = new Prescription();
                DataTable dtabled = _Prescription.GetOverviewData();
                mcbPrescription.FillData(dtabled);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillTransactionType()
        {
            cbTransactionType.Items.Clear();
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            if (General.CurrentSetting.MsetSaleCreditSale == "Y")
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditCard);
            cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
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
        private bool SaveAndUpdateDebitCreditNote()
        {
            {
                bool returnVal = false;
                try
                {
                    foreach (DataGridViewRow prodrow in dgCreditNote.Rows)
                    {
                        if ((prodrow.Cells["Col_CrdbID"].Value) != null && (Convert.ToBoolean(prodrow.Cells["Col_Check"].Value) == true))
                        {
                            _SSSale.CreditDebitNoteID = prodrow.Cells["Col_CrdbID"].Value.ToString();
                            _SSSale.Amount = Convert.ToDouble(prodrow.Cells["Col_AmountNet"].Value.ToString());
                            returnVal = _SSSale.UpdateCreditDebitNoteAdjustedDetails(_SSSale.CreditDebitNoteID, _SSSale.Amount, _SSSale.CrdbVouType, _SSSale.CrdbVouNo, _SSSale.CrdbVouDate, _SSSale.CrdbVouType, _SSSale.Id, "");
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }
        private bool clearPreviousdebitcreditnotes()
        {
            bool retValue = false;
            retValue = _SSSale.clearPreviousdebitcreditnotes(_SSSale.Id);
            return retValue;
        }
        #endregion

        #region Other private methods

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }



        private void CalculateAmountForDiscount()
        {
            double TotalAmount = 0;
            double TotalAmountforDiscount = 0;
            double mvatper = 0;
            double mvatamount5 = 0;
            double mvatamount12point5 = 0;
            double mtotvat5 = 0;
            double mtotvat12point5 = 0;
            double mtotamtvat0 = 0;
            double mtotamtvat5 = 0;
            double mtotamtvat12 = 0;
            double mrate = 0;
            double mamt = 0;
            double mpakn = 0;
            double mqty = 0;
            int itemCount = 0;
            string ifdiscount = "Y";
            double mprate = 0;
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "0" && dr.Cells["Col_Quantity"].Value.ToString() != "")
                    {
                        ifdiscount = "Y";
                        mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                        mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                        mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());

                        mprate = 0;
                        if (dr.Cells["Col_PurchaseRate"].Value != null && (dr.Cells["Col_PurchaseRate"].Value.ToString() != ""))
                            mprate = Convert.ToDouble(dr.Cells["Col_PurchaseRate"].Value.ToString());

                        if (dr.Cells["Col_IfSaleDisc"].Value != null)
                            ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString();
                        if (Math.Truncate(mqty / mpakn) == (mqty / mpakn))
                            mamt = Math.Round((mqty / mpakn) * mrate, 2);
                        else
                        {
                            mamt = Math.Round(Math.Round(mrate / mpakn, 2) * mqty, 2);
                            if (Math.Round(mamt, 1) - mamt < 0.05)
                                mamt = Math.Round(mamt, 1);
                        }
                        dr.Cells["Col_Amount"].Value = (mamt).ToString("#0.00");
                        if (mamt > 0)
                        {
                            mvatamount12point5 = 0;
                            mvatamount5 = 0;
                            mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());
                            if (mvatper == 5)
                            {
                                mvatamount5 = Math.Round((mamt) / (100 / mvatper), 4);
                                mtotamtvat5 += mamt;
                            }
                            else if (mvatper == 12.5)
                            {
                                mvatamount12point5 = Math.Round((mamt) / (100 / mvatper), 4);
                                mtotamtvat12 += mamt;
                            }
                            else
                                mtotamtvat0 += mamt;
                            dr.Cells["Col_VATAmount"].Value = mvatamount12point5 + mvatamount5;
                            TotalAmount += mamt;
                            if (ifdiscount != "N")
                                TotalAmountforDiscount += mamt;
                            mtotvat12point5 += mvatamount12point5;
                            mtotvat5 += mvatamount5;
                            itemCount += 1;
                        }
                    }
                }

                txtTotalAmountForDiscount.Text = Math.Round(TotalAmountforDiscount, 2).ToString("#0.00");

                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateAmount(int deletedrowindex)
        {
            double TotalAmount = 0;
            double TotalAmountforDiscount = 0;
            double mvatper = 0;
            double mvatamount5 = 0;
            double mvatamount12point5 = 0;
            double mtotvat5 = 0;
            double mtotvat12point5 = 0;
            double mtotamtvat0 = 0;
            double mtotamtvat5 = 0;
            double mtotamtvat12 = 0;
            double mrate = 0;
            double mamt = 0;
            double mpakn = 0;
            double mqty = 0;
            int itemCount = 0;
            string ifdiscount = "Y";
            double mprate = 0;
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Index != deletedrowindex)
                    {
                        if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "0" && dr.Cells["Col_Quantity"].Value.ToString() != "")
                        {
                            ifdiscount = "Y";
                            mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                            mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                            mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                            mpakn = 1;
                            mprate = 0;
                            if (dr.Cells["Col_PurchaseRate"].Value != null && (dr.Cells["Col_PurchaseRate"].Value.ToString() != ""))
                                mprate = Convert.ToDouble(dr.Cells["Col_PurchaseRate"].Value.ToString());

                            if (dr.Cells["Col_IfSaleDisc"].Value != null)
                                ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString();
                            if (Math.Truncate(mqty / mpakn) == (mqty / mpakn))
                                mamt = Math.Round((mqty / mpakn) * mrate, 2);
                            else
                            {
                                mamt = Math.Round(Math.Round(mrate / mpakn, 2) * mqty, 2);
                                if (Math.Round(mamt, 1) - mamt < 0.05)
                                    mamt = Math.Round(mamt, 1);
                            }
                            dr.Cells["Col_Amount"].Value = (mamt).ToString("#0.00");
                            if (mamt > 0)
                            {
                                mvatamount12point5 = 0;
                                mvatamount5 = 0;
                                mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());
                                if (mvatper == 5)
                                {
                                    mvatamount5 = Math.Round((mamt) / (100 / mvatper), 4);
                                    mtotamtvat5 += mamt;
                                }
                                else if (mvatper == 12.5)
                                {
                                    mvatamount12point5 = Math.Round((mamt) / (100 / mvatper), 4);
                                    mtotamtvat12 += mamt;
                                }
                                else
                                    mtotamtvat0 += mamt;
                                dr.Cells["Col_VATAmount"].Value = mvatamount12point5 + mvatamount5;
                                TotalAmount += mamt;
                                if (ifdiscount != "N")
                                    TotalAmountforDiscount += mamt;
                                mtotvat12point5 += mvatamount12point5;
                                mtotvat5 += mvatamount5;
                                itemCount += 1;
                            }
                        }
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtVatInput12point5per.Text = Math.Round(mtotvat12point5, 4).ToString("#0.0000");
                txtVatInput5per.Text = Math.Round(mtotvat5, 4).ToString("#0.0000");
                txtAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");
                txtAmountforZeroVAT.Text = mtotamtvat0.ToString("#0.00");
                txtTotalAmountForDiscount.Text = Math.Round(TotalAmountforDiscount, 2).ToString("#0.00");
                txtAmountVAT5Per.Text = mtotamtvat5.ToString();
                txtAmountVAT12Point5Per.Text = mtotamtvat12.ToString();
                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void CalculateAllAmounts()
        {
            double mdblAmount;
            double maddon = 0;
            double mcreditnote = 0;
            double mdebitnote = 0;
            double mtotamt = 0;
            double.TryParse(txtCreditNote.Text.ToString(), out mcreditnote);
            double.TryParse(txtDebitNote.Text.ToString(), out mdebitnote);
            double.TryParse(txtAmount.Text.ToString(), out mdblAmount);
            double mdblAmountforDiscount = 0;
            double.TryParse(txtTotalAmountForDiscount.Text.ToString(), out mdblAmountforDiscount);
            double.TryParse(txtAddOn.Text.ToString(), out maddon);
            double mdblDiscPer = 0;
            double.TryParse(txtDiscPercent.Text.ToString(), out mdblDiscPer);
            double mdblDiscAmount = 0;
            double.TryParse(txtDiscAmount.Text.ToString(), out mdblDiscAmount);
            try
            {
                mdblDiscAmount = Math.Round(((mdblAmountforDiscount) * mdblDiscPer / 100), 2);
                txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                mtotamt = Math.Round(mdblAmount - mdblDiscAmount + maddon + mdebitnote, 2);
                if (mcreditnote < mtotamt)
                    mtotamt = Math.Round(mtotamt - mcreditnote, 2);
                else
                {
                    txtCreditNote.Text = "0.00";
                    ClearDebitCreditNoteWhenAmountIsLess();
                }
                txtTotalAmount.Text = mtotamt.ToString("#0.00");
                txtRoundAmount.Enabled = true;
                if (cbRound.Checked == true)
                {
                    txtRoundAmount.Text = Math.Round(Math.Round(Convert.ToDouble(txtTotalAmount.Text.ToString()), 0) - Math.Round(Convert.ToDouble(txtTotalAmount.Text.ToString()), 2), 2).ToString("#0.00");
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text;
                }
                else
                {
                    txtRoundAmount.Text = "0.00";
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text;
                }
                txtRoundAmount.Enabled = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private void NoofRows()
        {
            int itemCount = 0;
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
                    if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                        crdbdr.Cells["Col_Check"].Value = false;
                }
            }
        }
        private void FillMainGridwithmpMSVC1()
        {
            int mmaingridrowIndex = 0;
            try
            {
                mmaingridrowIndex = mpPVC1.Rows.Count - 1;
                foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                {
                    int mclstk = 0;
                    int mreqstk = 0;
                    int msalestk = 0;
                    if (dr2.Cells["Col_ProductID"].Value != null)
                    {
                        string mprodno = dr2.Cells["Col_ProductID"].Value.ToString().Trim();
                        if (dr2.Cells["Col_ClosingStock"].Value != null)
                            int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                        if (dr2.Cells["Col_Quantity"].Value != null)
                            int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                        if (dr2.Cells["Col_SQuantity"].Value != null)
                            int.TryParse(dr2.Cells["Col_SQuantity"].Value.ToString().Trim(), out msalestk);
                        if (msalestk > 0)
                        {
                            SsStock dbstk = new SsStock();
                            DataTable stkdt = new DataTable();
                            stkdt = dbstk.GetStockByProductIDForFill(mprodno);
                            if (stkdt != null)
                            {
                                foreach (DataRow dtrow in stkdt.Rows)
                                {
                                    int mbatchstock = 0;
                                    int mactualsalestock = 0;
                                    double msalerate = 0;
                                    int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock);
                                    mactualsalestock = Math.Min(mbatchstock, msalestk);
                                    if (mactualsalestock > 0 && msalestk > 0)
                                    {
                                        string mbtno = "";
                                        double mmrp = 0;
                                        string mproddr1 = "";
                                        string mbatnodr1 = "";
                                        double mmrpdr1 = 0;
                                        int msaleQtydr1 = 0;
                                        int mbatchstkdr1 = 0;
                                        string mstockid = "";
                                        string ifbatchfoundindr1 = "";
                                        mbtno = dtrow["BatchNumber"].ToString();
                                        double.TryParse(dtrow["MRP"].ToString(), out mmrp);
                                        int mycolindex = 0;
                                        foreach (DataGridViewRow dr1 in mpPVC1.Rows)
                                        {
                                            if (dr1.Cells["Col_ProductID"].Value != null)
                                            {
                                                mproddr1 = dr1.Cells["Col_ProductID"].Value.ToString();
                                                mbatnodr1 = dr1.Cells["Col_BatchNumber"].Value.ToString();
                                                double.TryParse(dr1.Cells["Col_MRP"].Value.ToString(), out mmrpdr1);
                                                int.TryParse(dr1.Cells["Col_Quantity"].Value.ToString(), out msaleQtydr1);
                                                int.TryParse(dr1.Cells["Col_BatchStock"].Value.ToString(), out mbatchstkdr1);
                                                if (dr1.Cells["Col_StockID"].Value != null)
                                                    mstockid = dr1.Cells["Col_StockID"].Value.ToString();
                                                if (mprodno == mproddr1 && mbtno == mbatnodr1 && mmrp == mmrpdr1)
                                                {
                                                    mycolindex = dr1.Index;
                                                    ifbatchfoundindr1 = "Y";
                                                    break;
                                                }
                                            }

                                        }
                                        if (ifbatchfoundindr1 == "Y")
                                        {
                                            mpPVC1.Rows[mycolindex].Cells["Col_ProductID"].Value = mprodno;
                                            mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].Value = dtrow["ProdName"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_UOM"].Value = dtrow["ProdLoosePack"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_Pack"].Value = dtrow["ProdPack"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_ProdCompShortName"].Value = dtrow["ProdCompShortName"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_IfSaleDisc"].Value = dtrow["ProdIfSaleDisc"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_Shelf"].Value = dtrow["ShelfCode"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_BatchNumber"].Value = dtrow["BatchNumber"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_Expiry"].Value = dtrow["Expiry"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_MRP"].Value = Convert.ToDouble(dtrow["MRP"].ToString()).ToString("#0.00");
                                            mpPVC1.Rows[mycolindex].Cells["Col_SaleRate"].Value = Convert.ToDouble(dtrow["SaleRate"].ToString()).ToString("#0.00");
                                            double.TryParse(dtrow["SaleRate"].ToString(), out msalerate);
                                            mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value = Math.Min((mactualsalestock + msaleQtydr1), mbatchstkdr1);
                                            mpPVC1.Rows[mycolindex].Cells["Col_Amount"].Value = (msalerate * mactualsalestock).ToString("#0.00");
                                            mpPVC1.Rows[mycolindex].Cells["Col_ClosingStock"].Value = dtrow["ProdClosingStock"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_PurchaseRate"].Value = dtrow["PurchaseRate"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_ExpiryDate"].Value = dtrow["ExpiryDate"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = 0;
                                            mpPVC1.Rows[mycolindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dtrow["ProdVATPercent"].ToString()).ToString("#0.00");
                                            mpPVC1.Rows[mycolindex].Cells["Col_TradeRate"].Value = dtrow["TradeRate"].ToString();
                                            int mclstkdr1 = 0;
                                            int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
                                            mpPVC1.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
                                            mpPVC1.Rows[mycolindex].Cells["Col_StockID"].Value = mstockid;
                                            msalestk = msalestk - mactualsalestock;
                                        }
                                        else
                                        {
                                            mycolindex = mmaingridrowIndex;
                                            mpPVC1.Rows.Add();
                                            mmaingridrowIndex = mmaingridrowIndex + 1;
                                            mpPVC1.Rows[mycolindex].Cells["Col_ProductID"].Value = mprodno;
                                            mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].Value = dtrow["ProdName"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_UOM"].Value = dtrow["ProdLoosePack"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_Pack"].Value = dtrow["ProdPack"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_ProdCompShortName"].Value = dtrow["ProdCompShortName"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_IfSaleDisc"].Value = dtrow["ProdIfSaleDisc"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_Shelf"].Value = dtrow["ShelfCode"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_BatchNumber"].Value = dtrow["BatchNumber"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_Expiry"].Value = dtrow["Expiry"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_MRP"].Value = Convert.ToDouble(dtrow["MRP"].ToString()).ToString("#0.00");
                                            mpPVC1.Rows[mycolindex].Cells["Col_SaleRate"].Value = Convert.ToDouble(dtrow["SaleRate"].ToString()).ToString("#0.00");
                                            double.TryParse(dtrow["SaleRate"].ToString(), out msalerate);
                                            mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value = Math.Min(mactualsalestock, mbatchstock);
                                            double mamt = 0;
                                            mamt = Math.Round(msalerate * mactualsalestock, 2);
                                            mpPVC1.Rows[mycolindex].Cells["Col_Amount"].Value = Convert.ToDouble(mamt.ToString()).ToString("#0.00");
                                            mpPVC1.Rows[mycolindex].Cells["Col_ClosingStock"].Value = dtrow["ProdClosingStock"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_PurchaseRate"].Value = dtrow["PurchaseRate"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_ExpiryDate"].Value = dtrow["ExpiryDate"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = 0;
                                            mpPVC1.Rows[mycolindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dtrow["ProdVATPercent"].ToString()).ToString("#0.00");
                                            mpPVC1.Rows[mycolindex].Cells["Col_TradeRate"].Value = dtrow["TradeRate"].ToString();
                                            mpPVC1.Rows[mycolindex].Cells["Col_StockID"].Value = dtrow["StockID"].ToString();
                                            int mclstkdr1 = 0;
                                            int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
                                            mpPVC1.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
                                            msalestk = msalestk - mactualsalestock;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                mpPVC1.AddRowsInStockTempTable();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool FillLastSaleDataFromMasterProduct()
        {
            bool retValue = false;
            DataRow dr = null;
            DataRow invdr = null;
            string mprodno = "";
            int mclosingstock = 0;
            string mexpirydate = "";
            string mexpiry = "";
            double mmrpn = 0;
            double mpurrate = 0;
            double mtraderate = 0;
            double msalerate = 0;
            string mlastsalestockid = "";
            string mbatchno = "";

            try
            {
                Product drprod = new Product();
                dr = drprod.ReadLastSaleByID(mpPVC1.MainDataGridCurrentRow.Cells[0].Value.ToString());
                mprodno = mpPVC1.MainDataGridCurrentRow.Cells[0].Value.ToString();
                if (dr != null)
                {
                    if (dr["ProdLastSaleStockID"] != null && dr["ProdLastSaleStockID"].ToString() != "")
                    {
                        mlastsalestockid = dr["ProdLastSaleStockID"].ToString();
                        if (mlastsalestockid != "")
                        {
                            SsStock invss = new SsStock();
                            invdr = invss.GetStockByStockID(mlastsalestockid);
                        }

                        if (invdr != null)
                        {
                            int.TryParse(invdr["ClosingStock"].ToString().Trim(), out mclosingstock);

                            if (mclosingstock > 0)
                            {
                                mexpiry = invdr["Expiry"].ToString().Trim();
                                mexpirydate = invdr["ExpiryDate"].ToString().Trim();
                                double.TryParse(invdr["MRP"].ToString().Trim(), out mmrpn);
                                double.TryParse(invdr["SaleRate"].ToString().Trim(), out msalerate);
                                double.TryParse(invdr["PurchaseRate"].ToString().Trim(), out mpurrate);
                                double.TryParse(invdr["TradeRate"].ToString().Trim(), out mtraderate);
                                mbatchno = invdr["BatchNumber"].ToString().Trim();
                            }
                        }
                        retValue = true;

                    }
                }
                else
                    retValue = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void ClearSummarySection()
        {
            try
            {
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtDiscAmount.Text = "0.00";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void InitializeCheckBoxes()
        {
            try
            {
                cbFill.Checked = false;
                cbFill.Enabled = false;
                cbEditRate.Checked = false;
                cbEditRate.Enabled = false;
                cbTransferSale.Checked = false;
                cbTransferSale.Enabled = false;
                txtTokenNumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        #region Construct columns

        private void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC1.ColumnsMain.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 220;
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                mpPVC1.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 50;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                mpPVC1.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //8          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
               
                //
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStockPack";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //14         
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "IfSaleDisc";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "LastStockID";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                column.DataPropertyName = "ProfitPercentBySaleRate";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                column.DataPropertyName = "ProfitPercentByPurchaseRate";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                column.DataPropertyName = "ProfitInRupees";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

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
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "ID";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
                dgPaymentDetails.Columns.Add(column);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurID";
                column.DataPropertyName = "MastersaleID";
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
                column.Name = "Col_IfChequeReturn";
                column.DataPropertyName = "IfChequeReturn";
                column.HeaderText = "ChqRtn";
                column.Width = 60;
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
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
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

        private void ConstructCacheGridColumns()
        {
            DataGridViewTextBoxColumn column;
            dgCache.Columns.Clear();
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Cache_ProductID";
                column.Width = 0;
                column.Visible = false;
                dgCache.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructTempGridColumns()
        {
            DataGridViewTextBoxColumn column;
            dgtemp.Columns.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 200;
                column.ReadOnly = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch No.";
                column.Width = 90;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 50;
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
                column.Name = "Temp_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Purchase Rate";
                column.Width = 70;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 95;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                dgtemp.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_StockID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                dgtemp.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructProductSelectionListGridColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC1.ColumnsProductList.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 170;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "ShelfID";
                column.Width = 60;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStockPack";
                column.HeaderText = "Cl.Stk";
                column.Width = 60;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "#0";
                mpPVC1.ColumnsProductList.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "Disc";
                column.Width = 40;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "ProdLastPurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 30;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfShortListed";
                column.DataPropertyName = "ProdIfShortListed";
                column.HeaderText = "Short";
                column.Width = 30;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MaxLevel";
                column.DataPropertyName = "ProdMaxLevel";
                column.HeaderText = "MaxLevel";
                column.Width = 30;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "ProdLastSaleStockID";
                column.Width = 60;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);



                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "ProdLastSaleStockID";
                column.HeaderText = "laststockid";
                column.Width = 30;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsProductList.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructBatchGridColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC1.ColumnsBatchList.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batchno";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batchno";
                column.Width = 130;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 70;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPercent";
                column.DataPropertyName = "ProductVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);

                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate1";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                column.Visible = false;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock1";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Cl.STK";
                column.Width = 65;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStockPack";
                column.HeaderText = "Cl.STK";
                column.Width = 65;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyShortName";
                column.DataPropertyName = "AccShortName";
                column.HeaderText = "";
                column.Width = 45;
                mpPVC1.ColumnsBatchList.Add(column);
                //7              
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructmpMSVC1Columns()
        {
            DataGridViewTextBoxColumn column;
            mpMSVCFill.ColumnsMain.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                mpMSVCFill.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 200;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                mpMSVCFill.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //5              
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl.tock";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Required.Qty";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SQuantity";
                column.HeaderText = "Sale.Qty";
                column.Width = 70;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVCFill.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructCreditNoteColumns()
        {
            dgCreditNote.ColumnsMain.Clear();
            DataGridViewTextBoxColumn column;
            //0
            try
            {
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
                column.HeaderText = "VouSeries";
                column.Width = 50;
                column.ReadOnly = true;
                dgCreditNote.ColumnsMain.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "VouType";
                column.Width = 50;
                column.ReadOnly = true;
                dgCreditNote.ColumnsMain.Add(column);
                //3 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "VouNumber";
                column.ReadOnly = true;
                column.Width = 50;
                dgCreditNote.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "VoucherDate";
                column.Width = 80;
                column.ReadOnly = true;
                dgCreditNote.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountNet";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "AmountNet";
                column.Width = 80;
                column.ReadOnly = true;
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

                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountClear";
                //column.DataPropertyName = "AmountClear";
                column.HeaderText = "AmountBalance";
                column.Visible = false;
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCreditNote.ColumnsMain.Add(column);
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

        #endregion

        #region Events

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
                    txtPatient.Text = mcbCreditor.SeletedItem.ItemData[2];
                    if (mcbCreditor.SeletedItem.ItemData[6] != null && mcbCreditor.SeletedItem.ItemData[6].ToString() != "")
                        _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    _SSSale.DocID = mcbCreditor.SeletedItem.ItemData[7];
                    FillDoctorCombo();
                    mcbDoctor.SelectedID = _SSSale.DocID;
                    txtTokenNumber.Text = _SSSale.TokenNumber.ToString();
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
                txtPatient.Focus();
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
                    txtPatient.Text = "";
                    txtPatientAddress.Text = "";
                    txtDoctorAddress.Text = "";
                    txtTokenNumber.Text = "0";
                }
                else
                {
                    if (General.CurrentUser.Level == 0)
                    {
                        cbEditRate.Enabled = true;
                    }
                    cbFill.Enabled = true;
                    cbTransferSale.Enabled = true;
                    FillCreditDebitNote();
                    _SSSale.CrdbName = mcbCreditor.SeletedItem.ItemData[2];
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    txtPatient.Text = mcbCreditor.SeletedItem.ItemData[2];
                    txtPatientAddress.Text = txtAddress1.Text.ToString();
                    txtDiscPercent.Text = mcbCreditor.SeletedItem.ItemData[9];
                    if (mcbCreditor.SeletedItem.ItemData[6] != "")
                        _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    if (_Mode == OperationMode.Add)
                    {
                        _SSSale.DocID = mcbCreditor.SeletedItem.ItemData[7];
                        mcbDoctor.SelectedID = _SSSale.DocID;
                    }
                    _SSSale.TransactionType = mcbCreditor.SeletedItem.ItemData[8];
                    txtTokenNumber.Text = _SSSale.TokenNumber.ToString();
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
                    txtPatient.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtTokenNumber_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtTokenNumber.Text != null && txtTokenNumber.Text.ToString().Trim() != "" && txtTokenNumber.Text.ToString() != "0")
                    {
                        _SSSale.TokenNumber = Convert.ToInt32(txtTokenNumber.Text.ToString().Trim());
                        if (_SSSale.TokenNumber > 0)
                        {
                            DataRow dr = null;
                            dr = _SSSale.GetCreditorDataByTokenNumber();
                            if (dr != null)
                            {
                                string selectedId = dr["AccountID"].ToString();
                                mcbCreditor.SelectedID = selectedId;
                                //if (dr["AccAddress1"].ToString().Trim() != null)
                                //    txtAddress1.Text = dr["AccAddress1"].ToString().Trim();
                                //if (dr["AccAddress2"].ToString().Trim() != null)
                                //    txtAddress1.Text = dr["AccAddress2"].ToString().Trim();
                                //if (dr["AccNameAddress"].ToString().Trim() != null)
                                //    _SSSale.ShortName = dr["AccNameAddress"].ToString().Trim();
                                //txtPatient.Text = _SSSale.ShortName.ToString();
                                //_SSSale.CrdbAreaId = dr["AccAreaID"].ToString().Trim();
                                //if (_Mode == OperationMode.Add)
                                //{
                                //    _SSSale.TransactionType = dr["AccTransactionType"].ToString();
                                //    if (_SSSale.TransactionType == "CS")
                                //    {
                                //        rbtCash.Checked = true;
                                //        _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
                                //    }
                                //    else if (_SSSale.TransactionType == "CR")
                                //    {
                                //        rbtCreditStatement.Checked = true;
                                //        _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditStatementSale;
                                //    }
                                //    else
                                //    {
                                //        rbtCashCredit.Checked = true;
                                //        _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
                                //    }
                                //}
                                //selectedId = dr["AccDoctorID"].ToString();
                                //mcbDoctor.SelectedID = selectedId;
                                //txtVouType.Text = _SSSale.CrdbVouType;
                                //cbFill.Enabled = true;
                                //cbEditRate.Enabled = true;
                                //cbTransferSale.Enabled = true;
                                if (mcbDoctor.SelectedID != null)
                                    mpPVC1.SetFocus(1);
                                else
                                    mcbDoctor.Focus();
                            }
                            else
                            {
                                mcbCreditor.SelectedID = null;
                                txtAddress1.Text = "";
                                txtAddress2.Text = "";
                                txtPatient.Text = "";
                                mcbDoctor.SelectedID = "";
                                mcbCreditor.Focus();
                            }
                        }
                        else
                        {
                            mcbCreditor.SelectedID = null;
                            txtAddress1.Text = "";
                            txtAddress2.Text = "";
                            txtPatient.Text = "";
                            mcbDoctor.SelectedID = "";
                            mcbCreditor.Focus();
                        }
                    }
                    else
                    {
                        mcbCreditor.Focus();
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpPVC1_OnProductSelected(DataGridViewRow productRow)
        {
            double mprate = 0;
            int mclstk = 0;
            string mifshortlisted = "";
            string mifsalediscount = "Y";
            int mqty = 0;
            string mlastsalestockid = "";
            string mprodno = "";
            double mamt = 0;
            try
            {
                mpPVC1.MainDataGridCurrentRow.Cells[0].Value = productRow.Cells[0].Value;
                _SSSale.ProductID = productRow.Cells[0].Value.ToString();
                mprodno = _SSSale.ProductID;
                double.TryParse(productRow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                _SSSale.PurchaseRate = mprate;
                int.TryParse(productRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                _SSSale.Closingstock = mclstk;
                mifshortlisted = productRow.Cells["Col_IfShortListed"].Value.ToString().Trim();
                if (productRow.Cells["Col_IfSaleDisc"].Value != null && productRow.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                    mifsalediscount = productRow.Cells["Col_IfSaleDisc"].Value.ToString();
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = productRow.Cells["Col_ProductName"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value = productRow.Cells["Col_UOM"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Pack"].Value = productRow.Cells["Col_Pack"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = productRow.Cells["Col_ProdCompShortName"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = productRow.Cells["Col_Shelf"].Value.ToString();
                mpPVC1.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = Convert.ToDouble(productRow.Cells["Col_VATPer"].Value.ToString()).ToString("#0.00");
                mpPVC1.MainDataGridCurrentRow.Cells["Col_LastStockID"].Value = productRow.Cells["Col_LastStockID"].Value;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_IfSaleDisc"].Value = mifsalediscount.ToString();
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value = productRow.Cells["Col_ProdScheduleDrugCode"].Value;
                if (_Mode == OperationMode.Add)
                {
                    if (productRow.Cells["Col_LastStockID"].Value != null)
                        mlastsalestockid = productRow.Cells["Col_LastStockID"].Value.ToString();
                }
                else
                    mlastsalestockid = mprodno;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value = productRow.Cells["Col_ClosingStock"].Value;

                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString() != string.Empty)
                    mamt = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString());

                int currentrow = mpPVC1.MainDataGridCurrentRow.Index;
                int totproductsale = 0;
                int saleqty = 0;

                int tempstock = 0;

                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Index != currentrow && dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                    {
                        if (dr.Cells["Col_Quantity"].Value != null)
                            int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                        totproductsale += saleqty;

                    }
                }
                if (_Mode == OperationMode.Edit)
                {
                    foreach (DataGridViewRow dr in dgtemp.Rows)
                    {
                        if (dr.Cells["Temp_ProductID"].Value != null && dr.Cells["Temp_ProductID"].Value.ToString() == mprodno)
                        {
                            if (dr.Cells["Temp_Quantity"].Value != null)
                                int.TryParse(dr.Cells["Temp_Quantity"].Value.ToString(), out saleqty);
                            tempstock += saleqty;

                        }
                    }
                }

                //if (mamt == 0)
                mclstk = mclstk + tempstock - totproductsale;


                if (mclstk == 0 && mifshortlisted != "N" && mqty == 0)
                {
                    lblFooterMessage.Text = "No Stock";
                    Filldailyshortlist();
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = null;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value = null;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Pack"].Value = null;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = null;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = null;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = null;
                    mpPVC1.MainDataGridCurrentRow.Cells[0].Value = null;
                    mpPVC1.RefreshMe();
                    mpPVC1.SetFocus(1);
                }
                else
                {
                    lblFooterMessage.Text = "Product Stock :" + mclstk.ToString() + " : ";
                    try
                    {

                        if (mprodno != "")
                            FillLastSaleDataFromMasterProduct();
                    }
                    catch (Exception ex) { Log.WriteError(ex.ToString()); }
                    mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                    mpPVC1.SetFocus(11);
                }

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }
        private void mpPVC1_OnBatchSelected(DataGridViewRow batchRow)
        {
            if (_Mode != OperationMode.View)
            {
                int mclosingstock = 0;
                string mexpirydate = "";
                string mexpiry = "";
                double mmrpn = 0;
                double mpurrate = 0;
                double mtraderate = 0;
                double msalerate = 0;
                int mclstk = 0;
                string mprodno = "";
                string mlastsalestockid = "";
                int mqty = 0;
                double mamt = 0;
                try
                {
                    mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                    mexpiry = batchRow.Cells["Col_Expiry"].Value.ToString().Trim();
                    mexpirydate = batchRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                    double.TryParse(batchRow.Cells["Col_MRP"].Value.ToString().Trim(), out mmrpn);                   
                    double.TryParse(batchRow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                    double.TryParse(batchRow.Cells["Col_TradeRate"].Value.ToString().Trim(), out mtraderate);
                    double.TryParse(batchRow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                    int.TryParse(batchRow.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclosingstock);
                    mlastsalestockid = batchRow.Cells["Col_StockID"].Value.ToString();
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                        int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);


                    mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = batchRow.Cells["Col_Batchno"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = mexpiry;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value = mmrpn.ToString("#0.00");
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = msalerate.ToString("#0.00");
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value = mlastsalestockid;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value = mpurrate.ToString("#0.00");
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = mtraderate.ToString("#0.00");

                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value != null)
                        int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString() != string.Empty)
                        mamt = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString());

                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = batchRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value = batchRow.Cells["Col_ClosingStock"].Value.ToString().Trim();
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;
                    string mdt = DateTime.Today.Date.ToString("yyyyMMdd");

                    if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                    {
                        lblFooterMessage.Text = "Expired Product";
                        mpPVC1.Rows.Remove(mpPVC1.MainDataGridCurrentRow);
                        bool ifblank = false;
                        int currentindex = 0;
                        foreach (DataGridViewRow dr in mpPVC1.Rows)
                        {
                            currentindex = dr.Index;
                            if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == "")
                                ifblank = true;

                        }
                        if (ifblank == false)
                        {
                            int mindex = mpPVC1.Rows.Add();
                            mpPVC1.SetFocus(mindex, 1);
                        }
                        else
                            mpPVC1.SetFocus(currentindex, 1);
                    }
                    else
                    {
                        lblFooterMessage.Text = "";

                        int currentrow = mpPVC1.MainDataGridCurrentRow.Index;
                        int totbatchsale = 0;
                        int totproductsale = 0;
                        int saleqty = 0;
                        int tempproductstock = 0;
                        int tempbatchstock = 0;

                        foreach (DataGridViewRow dr in mpPVC1.Rows)
                        {
                            if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                            {
                                if (dr.Index != currentrow)
                                {
                                    if (dr.Cells["Col_Quantity"].Value != null)
                                        int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                                    totproductsale += saleqty;
                                    if (dr.Cells["Col_StockID"].Value.ToString().Trim() == mlastsalestockid)
                                    {
                                        totbatchsale += saleqty;
                                    }
                                }
                            }
                        }
                        if (_Mode == OperationMode.Edit)
                        {

                            foreach (DataGridViewRow dr in dgtemp.Rows)
                            {
                                if (dr.Cells["Temp_ProductID"].Value != null && dr.Cells["Temp_ProductID"].Value.ToString() == mprodno)
                                {
                                    if (dr.Cells["Temp_Quantity"].Value != null)
                                        int.TryParse(dr.Cells["Temp_Quantity"].Value.ToString(), out saleqty);
                                    tempproductstock += saleqty;
                                    if (dr.Cells["Temp_StockID"].Value.ToString().Trim() == mlastsalestockid)
                                    {
                                        if (dr.Cells["Temp_Quantity"].Value != null)
                                            int.TryParse(dr.Cells["Temp_Quantity"].Value.ToString(), out saleqty);
                                        tempbatchstock += saleqty;
                                    }

                                }
                            }
                        }


                        mclstk = mclstk + tempproductstock - totproductsale;

                        mclosingstock = mclosingstock + tempbatchstock - totbatchsale;



                        lblFooterMessage.Text = "Product Stock :" + mclstk.ToString().Trim() + " : Batch Stock :" + mclosingstock.ToString().Trim();
                        _SSSale.CurrentProductStock = mclstk;
                        _SSSale.CurrentBatchStock = mclosingstock;

                        if (_SSSale.CurrentBatchStock <= 0)
                        {
                            lblFooterMessage.Text = "Batch Stock Zero";
                            // mpPVC1.Rows.Remove(mpPVC1.MainDataGridCurrentRow);                   
                            mpPVC1.SetFocus(1);
                        }
                        else
                        {
                            if (cbEditRate.Checked == true)
                            {
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;
                                mpPVC1.SetFocus(10);
                            }
                            else
                                mpPVC1.SetFocus(11);
                        }
                    }
                }

                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());
                }
            }
            else
                mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
        }

        private void mpPVC1_OnRowDeleted(object sender, EventArgs e)
        {
            mpPVC1OnRowDeleted(sender);
        }

        private void mpPVC1_OnRowAdded(object sender, System.EventArgs e)
        {
            try
            {
                RefreshProductGrid();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void RefreshProductGrid()
        {
            try
            {
                //General.RefreshClientProductList();
                mpPVC1.DataSourceProductList = General.ProductList;
                mpPVC1.BindGridProductList();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void mpPVC1OnRowDeleted(object sender)
        {
            try
            {
                DataGridViewRow deletedrow = (DataGridViewRow)sender;
                int deletedrowindex = deletedrow.Index;
                CalculateAmount(deletedrowindex);
                lblFooterMessage.Text = "";
                if (_SSSale.AddNewRowCheck(mpPVC1))
                    mpPVC1.Rows.Add();
                mpPVC1.SetFocus(1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }
        private void mpPVC1_OnSelectedProductClosingStock(int closingStockValue, string productID)
        {
            int mqty = 0;
            if (_Mode == OperationMode.Add)
            {
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);
                if (closingStockValue + mqty == 0)
                {
                    _SSSale.ProductID = productID;

                    if (_SSSale.IfAddToShortList())
                    {
                        Filldailyshortlist();
                    }
                    lblFooterMessage.Text = "No Stock";
                    mpPVC1.SetFocus(mpPVC1.MainDataGridCurrentRow.Index, 1);
                }
                else
                {
                    lblFooterMessage.Text = string.Format("Stock: {0}", closingStockValue + mqty);
                }
            }
        }

        private void mpPVC1_OnCellValueChangeCommited(int colIndex)
        {
            mpPVC1CellValueChanged(colIndex);
        }

        private void mpPVC1CellValueChanged(int colIndex)
        {

            int requiredQty = 0;
            double mmrp = 0;
            double mrate = 0;
            int mqty = 0;
            int mpakn = 1;
            string mbtno = "";
            string mprodno = "";
            int mcurrentindex = 0;
            int oldmqty = 0;
            string prodname = "";
            string mexpirydate = "";
            try
            {
                if (colIndex == 1)
                {
                    _preID = "";
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                        _preID = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value != null)
                        prodname = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
                    if (prodname != "" && _preID != "")
                    {
                        prodname = General.GetProductName(_preID);
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = prodname;
                    }
                }
                if (colIndex == 11) //Quantity
                {

                    lblFooterMessage.Text = "";
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value == null || Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString()) == 0)
                        mpPVC1.IsAllowNewRow = false;
                    else
                    {
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                            mbtno = mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
                        if (mbtno != string.Empty)
                        {
                            string mdt = DateTime.Today.Date.ToString("yyyyMMdd");
                            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value != null)
                                mexpirydate = mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                            if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                            {
                                lblFooterMessage.Text = "Expired Product";
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = 0;
                                mpPVC1.IsAllowNewRow = false;
                                mpPVC1.SetFocus(11);
                            }
                            else
                            {
                                requiredQty = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                                if (requiredQty <= 0 || _SSSale.CurrentBatchStock == 0)
                                {
                                    if (requiredQty <= 0)
                                    {
                                        lblFooterMessage.Text = "Enter Quantity";
                                        mpPVC1.SetFocus(11);
                                        mpPVC1.IsAllowNewRow = false;
                                    }
                                }

                                else
                                {
                                    int mbatchstock = 0;
                                    int oldQuantity = 0;

                                    string mstockid = "";

                                    mprodno = "";
                                    mqty = 0;
                                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                        mprodno = (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString());
                                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value != null)
                                        int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value.ToString().Trim(), out mbatchstock);
                                    if (mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value != null)
                                        int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString().Trim(), out oldQuantity);
                                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                                        mstockid = (mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString());

                                    lblFooterMessage.Text = "";


                                    if (requiredQty <= _SSSale.CurrentBatchStock)
                                    {
                                        FillBatchStock(ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty);
                                        mpPVC1.IsAllowNewRow = true;
                                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;

                                        if (_Mode == OperationMode.Add)
                                        {
                                            WriteToXML();
                                        }
                                    }

                                    else
                                    {

                                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                            mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();

                                        FillMainGridwithMultipleBatch(requiredQty, mprodno);
                                        CalculateAmount(-1);

                                        WriteToXML();

                                    }
                                }
                            }
                        }
                        else
                        {

                            mpPVC1OnRowDeleted(mpPVC1.MainDataGridCurrentRow);
                            mpPVC1.Rows.Remove(mpPVC1.MainDataGridCurrentRow);

                        }
                    }

                }
                if (colIndex == 10)
                {
                    if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value) <= 0)
                    {
                        lblFooterMessage.Text = "Enter SaleRate";
                        mpPVC1.SetFocus(10);
                        mpPVC1.IsAllowNewRow = false;
                    }
                    else if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) > 0)
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value = (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value) * Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value)).ToString();
                        CalculateAmount(-1);
                        mpPVC1.IsAllowNewRow = true;
                    }
                }


                if (colIndex == 7)
                {
                    string newexpiry = "";
                    string newexpirydate = "";
                    int explength = 0;
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                        explength = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim().Length;
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim() != "" && explength > 0)
                    {
                        newexpiry = General.GetValidExpiry(mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString());
                        if (newexpiry != "")
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry.ToString();
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                            lblFooterMessage.Text = "";
                        }
                        else
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                            lblFooterMessage.Text = " No Expiry ";
                        }

                    }
                    else
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                        lblFooterMessage.Text = " No Expiry ";
                    }
                }
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void FillMainGridwithMultipleBatch(int requiredqty, string productID)
        {
            // why here
            int mmaingridrowIndex = 0;
            DataTable stkdt = new DataTable();
            Stock prodstk = new Stock();
            int mycolindex = 0;
            int msalestk = requiredqty;
            int mactualclosingstock = 0;

            if (mpPVC1.Rows.Count > 0)
                mmaingridrowIndex = mpPVC1.MainDataGridCurrentRow.Index;

            stkdt = prodstk.GetStockByProductIDForSale(productID);

            foreach (DataRow dtrow in stkdt.Rows)
            {
                if (dtrow["ClosingStockPack"] != DBNull.Value)
                    mactualclosingstock += Convert.ToInt32(dtrow["ClosingStockPack"].ToString());
            }
            try
            {
                // here
                foreach (DataRow dtrow in stkdt.Rows)
                {
                    int mbatchstock = 0;
                    int mactualsalestock = 0;
                    double msalerate = 0;
                    int.TryParse(dtrow["ClosingStockPack"].ToString(), out mbatchstock);
                    mactualsalestock = Math.Min(mbatchstock, msalestk);
                    if (mactualsalestock > 0 && msalestk > 0 && mactualclosingstock > 0)
                    {
                        string mbtno = "";
                        double mmrp = 0;
                        // string ifbatchfoundindr1 = "";
                        mbtno = dtrow["BatchNumber"].ToString();
                        double.TryParse(dtrow["MRP"].ToString(), out mmrp);
                        mycolindex = 0;


                        mycolindex = mmaingridrowIndex;

                        mpPVC1.Rows[mycolindex].Cells["Col_ProductID"].Value = dtrow["ProductID"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].Value = dtrow["ProdName"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_UOM"].Value = dtrow["ProdLoosePack"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_Pack"].Value = dtrow["ProdPack"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_ProdCompShortName"].Value = dtrow["ProdCompShortName"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_Shelf"].Value = dtrow["ShelfCode"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_BatchNumber"].Value = dtrow["BatchNumber"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_Expiry"].Value = dtrow["Expiry"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_MRP"].Value = Convert.ToDouble(dtrow["MRP"].ToString()).ToString("#0.00");
                        mpPVC1.Rows[mycolindex].Cells["Col_SaleRate"].Value = Convert.ToDouble(dtrow["SaleRate"].ToString()).ToString("#0.00");
                        double.TryParse(dtrow["SaleRate"].ToString(), out msalerate);
                        mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value = Math.Min(mactualsalestock, mbatchstock);
                        double mamt = 0;
                        mamt = Math.Round(msalerate * mactualsalestock, 2);
                        mpPVC1.Rows[mycolindex].Cells["Col_Amount"].Value = Convert.ToDouble(mamt.ToString()).ToString("#0.00");
                        mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value;
                        mpPVC1.Rows[mycolindex].Cells["Col_ClosingStock"].Value = dtrow["ProdClosingStock"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_PurchaseRate"].Value = dtrow["PurchaseRate"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_ExpiryDate"].Value = dtrow["ExpiryDate"].ToString();
                        // mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = 0;
                        mpPVC1.Rows[mycolindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dtrow["ProdVATPercent"].ToString()).ToString("#0.00");
                        mpPVC1.Rows[mycolindex].Cells["Col_TradeRate"].Value = dtrow["TradeRate"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_StockID"].Value = dtrow["StockID"].ToString();
                        int mclstkdr1 = 0;
                        int.TryParse(dtrow["ClosingStockPack"].ToString(), out mclstkdr1);
                        mpPVC1.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
                        msalestk = msalestk - mactualsalestock;
                        mactualclosingstock -= mactualsalestock;
                        CalculateAmount(-1);
                        if (msalestk > 0 && mactualclosingstock > 0)
                        {
                            mpPVC1.Rows.Add();
                            mpPVC1.SetFocus(mpPVC1.Rows.Count - 1, 11);
                            mmaingridrowIndex = mmaingridrowIndex + 1;
                        }
                    }
                }
                mpPVC1.IsAllowNewRow = true;

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void WriteToXML()
        {
            DataTable dt = mpPVC1.GetGridData();
            if (dt.Rows.Count > 0)
                dt.WriteXml(General.GetDebtorSaleTempFile());
        }

        private void FillBatchStock(ref double mmrp, ref double mrate, ref int mpakn, ref string mbtno, ref string mprodno, ref int mcurrentindex, ref int oldmqty, ref int mqty)
        {
            mcurrentindex = mpPVC1.MainDataGridCurrentRow.Index;
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                mbtno = mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                double.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                mqty = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
            foreach (DataGridViewRow drp in mpPVC1.Rows)
            {
                if (drp.Cells["Col_ProductID"].Value != null &&
                      drp.Cells["Col_BatchNumber"].Value != null &&
                         drp.Cells["Col_MRP"].Value != null)
                {
                    if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
                          drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
                             drp.Cells["Col_MRP"].Value.ToString().Trim() == mmrp.ToString("#0.00") && drp.Index != mcurrentindex)
                    {
                        if (drp.Cells["Col_Quantity"].Value != null)
                            oldmqty = Convert.ToInt32(drp.Cells["Col_Quantity"].Value.ToString());
                        oldmqty += mqty;
                        drp.Cells["Col_Quantity"].Value = oldmqty;
                        mpPVC1.Rows.Remove(mpPVC1.MainDataGridCurrentRow);
                        break;
                    }
                }
            }

            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value != null)
                mpakn = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
            mpakn = 1;
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null)
                double.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString(), out mrate);
            mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value = ((mrate / mpakn) * mqty).ToString("#0.00");
            CalculateAmount(-1);
        }

        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtAddOn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbDoctor.SelectedID;
                FillDoctorCombo();
                mcbDoctor.SelectedID = selectedId;
                if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "" && mcbDoctor.SeletedItem.ItemData[2] != null)
                {
                    txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2].ToString();
                    mpPVC1.SetFocus(1);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbDoctor.Focus();
            else if (e.KeyCode == Keys.Up)
                txtMobileNumber.Focus();
        }
        private void txtAddOn_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    cbRound.Focus();
                    CalculateAllAmounts();
                    break;
            }
        }

        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    CalculateAmountForDiscount();
                    txtAddOn.Focus();
                    break;
                case Keys.Down:
                    txtAddOn.Focus();
                    break;
            }
        }

        //private void txtDiscAmount_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Enter:
        //            CalculateAllAmounts();

        //            break;
        //        case Keys.Down:
        //            txtAddOn.Focus();
        //            break;
        //    }

        //}


        private void cbFill_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbFill.Checked == true && mcbCreditor.SelectedID != "")
                {
                    DebtorProduct dbprod = new DebtorProduct();
                    DataTable dt = new DataTable();
                    dt = dbprod.ReadProdDetailsByIdForDebtorSale(mcbCreditor.SelectedID);

                    int cnt = dt.Rows.Count;
                    if (cnt > 0)
                        txtNoOfProducts.Text = cnt.ToString();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cbFill.Enabled = false;
                        pnlCenter.SendToBack();
                        pnlCenter.Enabled = false;
                        pnlDebtorProduct.Location = GetpnlDebtorProductLocation();
                        pnlDebtorProduct.Width = 614;
                        pnlDebtorProduct.Height = 325;
                        pnlDebtorProduct.Visible = true;

                        mpMSVCFill.Visible = true;
                        mpMSVCFill.Dock = DockStyle.Fill;
                        ConstructmpMSVC1Columns();

                        mpMSVCFill.DataSourceMain = dt;
                        Product prod = new Product();
                        mpMSVCFill.DataSource = General.ProductList;

                        mpMSVCFill.NumericColumnNames.Add("Col_ClosingStock");
                        mpMSVCFill.NumericColumnNames.Add("Col_Quantity");
                        mpMSVCFill.NumericColumnNames.Add("Col_SQuantity");
                        mpMSVCFill.DoubleColumnNames.Add("Col_VATPer");

                        mpMSVCFill.Bind();

                        int cntstock = 0;
                        foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                        {
                            int mclstk = 0;
                            int mreqstk = 0;
                            int msalestk = 0;
                            if (dr2.Cells["Col_ClosingStock"].Value != null)
                                int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                            if (dr2.Cells["Col_Quantity"].Value != null)
                                int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                            msalestk = Math.Min(mclstk, mreqstk);
                            if (msalestk > 0)
                                cntstock += 1;
                            if (dr2.Cells["Col_ProductID"].Value != null)
                            {
                                dr2.Cells["Col_SQuantity"].Value = msalestk;
                            }

                        }
                        txtStockInProducts.Text = cntstock.ToString();
                    }
                    else
                        lblFooterMessage.Text = "No Data Available for the Debtor.";
                }
                cbFill.Checked = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            DataRow dr = null;
            int vouno = 0;
            string voutype = "";
            if (e.KeyCode == Keys.Enter)
            {
                if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                    voutype = FixAccounts.VoucherTypeForDistributorSaleCash;
                else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                    voutype = FixAccounts.VoucherTypeForDistributorSaleCreditStatement;
                else
                    voutype = FixAccounts.VoucherTypeForDistributorSaleCredit;


                try
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (txtVouchernumber.Text != "")
                        {
                            int.TryParse(txtVouchernumber.Text.ToString(), out vouno);
                            _SSSale.CrdbVouType = voutype;
                            _SSSale.CrdbVouNo = vouno;
                            dr = _SSSale.ReadDetailsByVouNumber();
                            if (dr == null)
                                Cancel();
                            else
                                FillSearchData(_SSSale.Id, "");
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
        }

        private Point GetpnlDebtorProductLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlCenter.Location.X + 300;
                pt.Y = pnlCenter.Location.Y + 10;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private Point GetpnlDebitCreditNoteLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlCenter.Location.X + 180;
                pt.Y = pnlCenter.Location.Y + 80;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        private Point GetpnlPaymentDetailsLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlCenter.Location.X + 80;
                pt.Y = pnlCenter.Location.Y + 80;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private Point GetmpMSVC1Location()
        {
            Point pt = new Point();
            try
            {
                pt.X = pnlDebtorProduct.Location.X + 4;
                pt.Y = pnlDebtorProduct.Location.Y + 3;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDebtorProduct.Visible = false;
                mpMSVCFill.ColumnsMain.Clear();
                cbFill.Enabled = true;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpMSVC1_OnCellValueChangeCommited(int colIndex)
        {
            try
            {
                if (colIndex == 6)
                {
                    int mclstk = 0;
                    int mreqstk = 0;
                    int msalestk = 0;
                    if (mpMSVCFill.MainDataGridCurrentRow.Cells[4].Value != null)
                        int.TryParse(mpMSVCFill.MainDataGridCurrentRow.Cells[4].Value.ToString().Trim(), out mclstk);
                    if (mpMSVCFill.MainDataGridCurrentRow.Cells[5].Value != null)
                        int.TryParse(mpMSVCFill.MainDataGridCurrentRow.Cells[5].Value.ToString().Trim(), out mreqstk);
                    if (mpMSVCFill.MainDataGridCurrentRow.Cells[6].Value != null)
                        int.TryParse(mpMSVCFill.MainDataGridCurrentRow.Cells[6].Value.ToString().Trim(), out msalestk);
                    if (msalestk > mclstk)
                    {
                        msalestk = Math.Min(mclstk, mreqstk);
                        mpMSVCFill.MainDataGridCurrentRow.Cells[6].Value = msalestk;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void cbTransferSale_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbTransferSale.Enabled = false;
                if (cbTransferSale.Checked == true)
                {
                    mpPVC1.ColumnsMain["Col_SaleRate"].HeaderText = "Purchase Rate";
                    cbEditRate.Enabled = false;
                    ChangeRateinGrid();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ChangeRateinGrid()
        {
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_ProductID"].Value != null)
                    {
                        if (dr.Cells["Col_PurchaseRate"].Value != null)
                            dr.Cells["Col_SaleRate"].Value = dr.Cells["Col_PurchaseRate"].Value.ToString();
                    }
                }
                CalculateAmount(-1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnIfDebitCredit_KeyDown(object sender, KeyEventArgs e)
        {
            BtnIfDebitCreditNoteClick();
        }

        private void btnIfDebitCredit_Click(object sender, EventArgs e)
        {
            try
            {
                BtnIfDebitCreditNoteClick();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void BtnIfDebitCreditNoteClick()
        {
            double amt = 0;
            try
            {
                double.TryParse(txtAmount.Text.ToString(), out amt);
                if (amt > 0)
                {
                    pnlDebitCreditNote.BringToFront();
                    pnlDebitCreditNote.Location = GetpnlDebitCreditNoteLocation();
                    pnlDebitCreditNote.Width = 585;
                    pnlDebitCreditNote.Height = 175;
                    pnlDebitCreditNote.Visible = true;
                    dgCreditNote.Visible = true;
                    lblCreditNote.Visible = true;
                    lblDebitNote.Visible = true;
                    lblFooterMessage.Text = "Press Space Bar To Select Credit/Debit Note";
                    txtCreditNote.Visible = true;
                    txtDebitNote.Visible = true;
                    dgCreditNote.BringToFront();
                    dgCreditNote.Focus();
                }
                else
                    lblFooterMessage.Text = "First Enter Sale";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "" && mcbDoctor.SeletedItem.ItemData[2] != null)
            {
                txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2].ToString();
                mpPVC1.SetFocus(1);
            }
            else
                txtDoctorAddress.Focus();

        }


        //private void btnCRDBCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtCreditNote.Text = "0.00";
        //        txtDebitNote.Text = "0.00";
        //        pnlDebitCreditNote.Visible = false;
        //        foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
        //        {
        //            crdbdr.Cells["Col_Check"].Value = false;

        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}

        //private void btnCanelFill_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        pnlDebtorProduct.Visible = false;
        //        pnlCenter.BringToFront();
        //        pnlCenter.Enabled = true;
        //        CalculateAmount();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}

        //private void btnOKFill_Click(object sender, EventArgs e)
        //{
        //    btnOKFillClick();
        //}
        //private void btnOKFillClick()
        //{
        //    try
        //    {
        //        foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
        //        {
        //            int mclstk = 0;
        //            int mreqstk = 0;
        //            int msalestk = 0;
        //            if (dr2.Cells["Col_ClosingStock"].Value != null)
        //                int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
        //            if (dr2.Cells["Col_Quantity"].Value != null)
        //                int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
        //            if (dr2.Cells["Col_SQuantity"].Value != null)
        //                int.TryParse(dr2.Cells["Col_SQuantity"].Value.ToString().Trim(), out msalestk);
        //            if (msalestk > mclstk)
        //            {
        //                msalestk = mclstk;
        //                dr2.Cells["Col_SQuantity"].Value = msalestk;
        //            }

        //        }

        //        FillMainGridwithmpMSVC1();
        //        pnlDebtorProduct.Visible = false;
        //        pnlCenter.BringToFront();
        //        pnlCenter.Enabled = true;
        //        CalculateAmount();
        //        mpPVC1.SetFocus(1);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}

        private void btnOKFill_Click(object sender, EventArgs e)
        {
            btnOKFillClick();
        }

        private void btnCanelFill_Click(object sender, EventArgs e)
        {
            btnCancelFillClick();
        }

        private void btnOKFill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKFillClick();
        }

        private void btnCanelFill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnCancelFillClick();
        }

        private void btnOKFillClick()
        {

            try
            {
                foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                {
                    int mclstk = 0;
                    int mreqstk = 0;
                    int msalestk = 0;
                    if (dr2.Cells["Col_ClosingStock"].Value != null)
                        int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                    if (dr2.Cells["Col_Quantity"].Value != null)
                        int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                    if (dr2.Cells["Col_SQuantity"].Value != null)
                        int.TryParse(dr2.Cells["Col_SQuantity"].Value.ToString().Trim(), out msalestk);
                    if (msalestk > mclstk)
                    {
                        msalestk = mclstk;
                        dr2.Cells["Col_SQuantity"].Value = msalestk;
                    }

                }

                FillMainGridwithmpMSVC1();
                pnlDebtorProduct.Visible = false;
                pnlCenter.BringToFront();
                pnlCenter.Enabled = true;
                pnlTotals.Enabled = true;
                CalculateAmount(-1);
                mpPVC1.SetFocus(1);
                txtNarration.Focus();
                mpPVC1.SetFocus(mpPVC1.Rows.Count - 1, 1);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnCancelFillClick()
        {
            try
            {
                pnlDebtorProduct.Visible = false;
                pnlCenter.BringToFront();
                pnlCenter.Enabled = true;
                cbFill.Enabled = true;
                cbFill.Checked = false;
                mcbPrescription.SelectedID = "";
                CalculateAmount(-1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private void mcbPrescription_EnterKeyPressed(object sender, EventArgs e)
        {

            mcbPrescriptionEnterKeyPressed();
        }
        private void mcbPrescription_SeletectIndexChanged(object sender, EventArgs e)
        {
            mcbPrescriptionEnterKeyPressed();
        }
        private void mcbPrescriptionEnterKeyPressed()
        {
            try
            {
                if (mcbPrescription.SelectedID != null && mcbPrescription.SelectedID != "")
                {
                    Prescription dbpres = new Prescription();
                    DataTable dt = new DataTable();
                    dt = dbpres.ReadProductDetailsById(mcbPrescription.SelectedID);
                    int cnt = dt.Rows.Count;
                    if (cnt > 0)
                        txtNoOfProducts.Text = cnt.ToString();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        pnlCenter.SendToBack();
                        pnlCenter.Enabled = false;
                        pnlDebtorProduct.Location = GetpnlDebtorProductLocation();
                        pnlDebtorProduct.Visible = true;
                        pnlDebtorProduct.Width = 614;
                        pnlDebtorProduct.Height = 325;

                        mpMSVCFill.Visible = true;
                        mpMSVCFill.Dock = DockStyle.Fill;
                        ConstructmpMSVC1Columns();

                        mpMSVCFill.DataSourceMain = dt;
                        mpMSVCFill.DataSource = General.ProductList;

                        mpMSVCFill.NumericColumnNames.Add("Col_ClosingStock");
                        mpMSVCFill.NumericColumnNames.Add("Col_Quantity");
                        mpMSVCFill.NumericColumnNames.Add("Col_SQuantity");
                        mpMSVCFill.DoubleColumnNames.Add("Col_VATPer");

                        mpMSVCFill.Bind();
                        int cntstock = 0;
                        foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                        {
                            int mclstk = 0;
                            int mreqstk = 0;
                            int msalestk = 0;
                            if (dr2.Cells["Col_ClosingStock"].Value != null)
                                int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                            if (dr2.Cells["Col_Quantity"].Value != null)
                                int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                            msalestk = Math.Min(mclstk, mreqstk);
                            if (msalestk > 0)
                                cntstock += 1;
                            if (dr2.Cells["Col_ProductID"].Value != null)
                            {
                                dr2.Cells["Col_SQuantity"].Value = msalestk;
                            }

                        }
                        txtStockInProducts.Text = cntstock.ToString();
                    }
                    else
                        lblFooterMessage.Text = "No Data Available for the Debtor.";
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {

            try
            {
                //txtPatient.Text = mcbCreditor.Text.ToString().Trim();  
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                    _SSSale.AccountID = mcbCreditor.SelectedID;
                if (General.CurrentUser.Level <= 1)
                {
                    cbEditRate.Enabled = true;
                }

                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    cbFill.Enabled = true;
                    txtAddress1.ReadOnly = true;
                    txtAddress2.ReadOnly = true;
                    txtMobileNumber.Focus();
                    _SSSale.AccountID = mcbCreditor.SelectedID;

                }
                else
                {
                    cbFill.Enabled = false;
                    txtAddress1.ReadOnly = false;
                    txtAddress2.ReadOnly = false;
                    txtAddress1.Focus();

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }


        }
        private void txtMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPatient.Focus();
            else if (e.KeyCode == Keys.Up)
                mcbCreditor.Focus();
        }
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btnIfDebitCredit.Visible)
                {
                    btnIfDebitCredit.Focus();
                }
                else
                {
                    if (txtDiscPercent.Visible)
                    {
                        txtDiscPercent.Focus();
                    }
                    else
                        tsBtnSave.Select();
                }
            }
        }

        private void btnOKCreditDebitNote_Click(object sender, EventArgs e)
        {

        }
        private void btnOKCreditDebitNoteClick()
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
                txtCreditNote.Text = mcrnoteamt.ToString("#0.00");
                txtDebitNote.Text = mdbnoteamt.ToString("#0.00");
                pnlDebitCreditNote.Visible = false;
                CalculateAllAmounts();
                if (txtDiscPercent.Visible)
                    txtDiscPercent.Focus();
                else
                    txtAddOn.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCRDBOK_Click>>" + Ex.Message);
            }
        }
        private void txtclonevouno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbclonevoutype.Focus();
        }

        private void btncloneOK_Click(object sender, EventArgs e)
        {
            btnOKCloneClick();
        }
        private void btncloneOK_KeyDown(object sender, KeyEventArgs e)
        {
            btnOKCloneClick();
        }
        private void btnOKCloneClick()
        {
            pnlClone.Visible = false;
            Product dbprod = new Product();
            DataTable dt = new DataTable();
            DataRow dr;
            int clonevouno = 0;
            string clonevoutype = "";
            string cloneSaleID = "";
            if (txtclonevouno.Text != null && txtclonevouno.Text.ToString() != "")
                int.TryParse(txtclonevouno.Text.ToString(), out clonevouno);
            if (cbclonevoutype.Text != null)
                clonevoutype = cbclonevoutype.Text.ToString();
            try
            {
                dr = _SSSale.GetSaleIDforClone(clonevouno, clonevoutype);
                if (dr != null)
                {
                    if (dr["ID"] != DBNull.Value)
                        cloneSaleID = dr["ID"].ToString();
                    dt = _SSSale.ReadProductDetailsByCloneID(cloneSaleID);

                    int cnt = dt.Rows.Count;
                    if (cnt > 0)
                        txtNoOfProducts.Text = cnt.ToString();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cbFill.Enabled = false;
                        pnlCenter.SendToBack();
                        pnlCenter.Enabled = false;
                        pnlTotals.Enabled = false;
                        pnlDebtorProduct.BringToFront();
                        pnlDebtorProduct.Location = GetpnlDebtorProductLocation();
                        pnlDebtorProduct.Width = 614;
                        pnlDebtorProduct.Height = 325;
                        pnlDebtorProduct.Visible = true;

                        mpMSVCFill.Visible = true;
                        mpMSVCFill.Dock = DockStyle.Fill;
                        ConstructmpMSVC1Columns();

                        mpMSVCFill.DataSourceMain = dt;
                        mpMSVCFill.DataSource = General.ProductList;

                        mpMSVCFill.NumericColumnNames.Add("Col_ClosingStock");
                        mpMSVCFill.NumericColumnNames.Add("Col_Quantity");
                        mpMSVCFill.NumericColumnNames.Add("Col_SQuantity");
                        mpMSVCFill.DoubleColumnNames.Add("Col_VATPer");

                        mpMSVCFill.Bind();

                        int cntstock = 0;
                        foreach (DataGridViewRow dr2 in mpMSVCFill.Rows)
                        {
                            int mclstk = 0;
                            int mreqstk = 0;
                            int msalestk = 0;
                            if (dr2.Cells["Col_ClosingStock"].Value != null)
                                int.TryParse(dr2.Cells["Col_ClosingStock"].Value.ToString().Trim(), out mclstk);
                            if (dr2.Cells["Col_Quantity"].Value != null)
                                int.TryParse(dr2.Cells["Col_Quantity"].Value.ToString().Trim(), out mreqstk);
                            msalestk = Math.Min(mclstk, mreqstk);
                            if (msalestk > 0)
                                cntstock += 1;
                            if (dr2.Cells["Col_ProductID"].Value != null)
                            {
                                dr2.Cells["Col_SQuantity"].Value = msalestk;
                            }

                        }
                        txtStockInProducts.Text = cntstock.ToString();
                    }
                    else
                        lblFooterMessage.Text = "No Data Available for the Debtor.";
                }
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            pnlClone.Visible = true;
            txtclonevouno.Focus();
        }

        private void cbclonevoutype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKCloneClick();
        }

        private void dgCreditNote_OnShowViewForm(DataGridViewRow selectedRow)
        {

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

        private void mpPVC1_OnShiftTABKeyPressed(object sender, EventArgs e)
        {
            mcbDoctor.Focus();
        }
        private void btnPaymentHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlPaymentDetails.Visible == false)
                {
                    pnlPaymentDetails.BringToFront();
                    pnlPaymentDetails.Location = GetpnlDebitCreditNoteLocation();
                    pnlPaymentDetails.Width = 485;
                    pnlPaymentDetails.Height = 175;
                    pnlPaymentDetails.Visible = true;
                    dgPaymentDetails.Visible = true;
                    dgPaymentDetails.Columns["Col_ID"].Visible = false;
                    dgPaymentDetails.BringToFront();
                    dgPaymentDetails.Columns["Col_ID"].Visible = false;
                    dgPaymentDetails.Focus();

                }
                else
                {
                    pnlPaymentDetails.SendToBack();
                    dgPaymentDetails.Visible = false;
                    pnlPaymentDetails.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnPaymentHistory_Click>>" + Ex.Message);
            }
        }

        private void cbTransactionType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mpPVC1.SetFocus(1);
        }

        private void btnSubstitute_Click(object sender, EventArgs e)
        {

        }

        private void txtPatientAddress_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void txtDoctorAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mpPVC1.SetFocus(1);
            else if (e.KeyCode == Keys.Up)
                mcbDoctor.Focus();
        }

        private void mcbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDoctorAddress.Focus();
        }
        private DataRow mpPVC1_OnProductBarCodeScaned(string scanCode)
        {
            return GetProductNameFromScanCode(scanCode);
        }

        private DataRow GetProductNameFromScanCode(string scanCode)
        {
            DataRow dr = null;
            try
            {
                dr = _SSSale.GetProductNameFromScanCode(scanCode);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return dr;
        }

        private void datePickerBillDate_Validating(object sender, CancelEventArgs e)
        {
            bool retValue = false;
            string _MDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
            retValue = General.CheckDates(_MDate, _MDate);
            string _CDate = DateTime.Now.Date.ToString("yyyyMMdd");
            if (_MDate == _CDate)
            {
                retValue = true;
            }
            else
                retValue = false;
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
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            //ttDebtorSale.SetToolTip(cbTransferSale, "Sale By Purchase Rate");
            //ttDebtorSale.SetToolTip(cbFill, "Show Product List From DebtorProduct Link");
            //ttDebtorSale.SetToolTip(cbEditRate, "Edit Sale Rate");
        }
        #endregion

        private void mcbCreditor_UpArrowPressed(object sender, EventArgs e)
        {
            mcbPrescription.Focus();
        }     

    }
}
