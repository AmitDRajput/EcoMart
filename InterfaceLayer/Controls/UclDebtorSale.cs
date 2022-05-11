using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.Common;
using PharmaSYSDistributorPlus.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using PharmaSYSDistributorPlus.InterfaceLayer.CommonControls;
using PrintDataGrid;
using System.IO;
using PharmaSYSDistributorPlus.InterfaceLayer.Classes;
using PaperlessPharmaRetail.Common.Classes;

namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDebtorSale : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _PaymentDetailsBindingSource;
        private SSSale _SSSale;
        private BaseControl ViewControl;
        public string _IfNewDoctor = "N";
        private Form frmView;
        // string _preID = "";
        public int _PreCurrentQuantity = 0;
        DataTable dtTempDebtorSale;
        private DataTable _BindingSourcePreviousSale;
        #endregion 

        #region Constructor
        public UclDebtorSale()
        {
            try
            {
                InitializeComponent();
                _SSSale = new SSSale();
                SearchControl = new UclDebtorSaleSearch();
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
                UpdateTempDebtorSaleDt();
                FormatGrids();
                if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit) && _SSSale.AddNewRowCheck(mpPVC1))
                    mpPVC1.Rows.Add();
                mpPVC1.ClearSelection();
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
                headerLabel1.Text = "DEBTOR SALE -> NEW";

                if (General.CurrentUser.Level > 1)
                    cbEditRate.Visible = false;
                _SSSale.ReadBillPrintSettings();

                FillPartyCombo();
                pnlTop.Enabled = true;
                mpPVC1.ModuleNumber = ModuleNumber.DebtorSale;
                mpPVC1.OperationMode = OperationMode.Add;
                mpPVC1.EditedTempDataList = null;
                InitialisempPVC1("");
                //  FillAll();              
                txtTokenNumber.Text = _SSSale.TokenNumber.ToString("#0");
                txtTokenNumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        private void FillAll()
        {
            FillDoctorCombo();
            FillPrescription();
            FillTransactionType();
            FillPartyCombo();
            FillPatientCombo();
            InitializeCheckBoxes();
            FillCloneType();
        }
        private void FillCloneType()
        {
            cbclonevoutype.Items.Add(FixAccounts.VoucherTypeForCashSale);
            cbclonevoutype.Items.Add(FixAccounts.VoucherTypeForCreditSale);
            cbclonevoutype.Items.Add(FixAccounts.VoucherTypeForCreditStatementSale);
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                ClearData();
                FillPrescription();
                if (_Mode == OperationMode.Fifth)
                    headerLabel1.Text = "DEBTOR SALE -> TYPE CHANGE";
                else
                    headerLabel1.Text = "DEBTOR SALE -> EDIT";
                pnlTop.Enabled = true;
                InitializeCheckBoxes();
                FillPatientCombo();
                FillPartyCombo();
                FillTransactionType();
                EnableDisable();

                mpPVC1.ModuleNumber = ModuleNumber.DebtorSale;
                mpPVC1.OperationMode = OperationMode.Edit;
                mpPVC1.EditedTempDataList = null;
                mpPVC1.ClearSelection();
                txtVouchernumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void EnableDisable()
        {
            if (_Mode != OperationMode.Add)
            {
                mcbCreditor.Enabled = false;
                txtTokenNumber.Enabled = false;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                dgPrviousSaleBillWise.Visible = false;
                txtVouchernumber.Focus();
            }
            else
            {
                mcbCreditor.Enabled = true;
                txtAddress1.Enabled = true;
                txtAddress2.Enabled = true;
                txtPatient.Enabled = true;
                mcbDoctor.Enabled = true;
                txtTokenNumber.Enabled = true;
                txtVouchernumber.ReadOnly = true;
                txtVouchernumber.Enabled = false;
                mcbCreditor.Focus();
            }
            if (_Mode == OperationMode.Edit)
            {
                if (General.CurrentUser.Level > 1)
                    cbEditRate.Visible = false;
            }
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            ClearData();
            return retValue;
        }
        public override bool Exit()
        {
            bool retValue = false;
            try
            {
                if (mpPVC1.Rows.Count < 2)
                {
                    retValue = base.Exit();
                    System.IO.File.Delete(General.GetDebtorSaleTempFile());
                    //_ImportBill = null;
                }
                else if (Mode == OperationMode.Add /*|| Mode == OperationMode.Edit*/)
                {
                    PSDialogResult result;
                    result = PSMessageBox.Show("Save Or Remove All Invoices..", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                }
                else
                    retValue = base.Exit();
                // return retValue;

                //if (Mode == OperationMode.Add && Convert.ToDecimal(txtNetAmount.Text.ToString()) > 0)
                //{
                //    PSDialogResult result;
                //    result = PSMessageBox.Show("Save Or Remove All Invoices..", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                //}
                //else
                //{   
                //  if (retValue)
                //    {
                //        System.IO.File.Delete(General.GetDebtorSaleTempFile());
                //        //  UpdateClosingStockinCache();
                //    }
                //}
                //retValue = base.Exit();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "DEBTOR SALE -> DELETE";
                InitializeCheckBoxes();
                FillTransactionType();
                EnableDisable();
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
                    _SSSale.ModifiedBy = General.CurrentUser.Id;
                    _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
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
                        // UpdateClosingStockinCache();
                        _SSSale.AddDetailsInDeleteMaster();
                        AddPreviousRowsInDeleteDetail();
                        retValue = true;
                        //  RefreshProductGrid();
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
                headerLabel1.Text = "DEBTOR SALE -> VIEW";
                InitializeCheckBoxes();
                FillTransactionType();
                mpPVC1.ClosePopupGrid();
                mpPVC1.EditedTempDataList = null;
                EnableDisable();
                if (General.IfYearEndOverGlobal == "Y")
                {
                    if (General.CurrentUser.Level <= 1)
                    {
                        tsBtnAdd.Visible = true;
                        tsBtnDelete.Visible = true;
                        tsBtnFifth.Visible = true;
                        tsBtnEdit.Visible = true;
                    }
                    else
                    {
                        tsBtnAdd.Visible = false;
                        tsBtnDelete.Visible = false;
                        tsBtnFifth.Visible = false;
                        tsBtnEdit.Visible = false;
                    }
                }
                //  GetLastRecord();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private void GetLastRecord()
        {
            try
            {
                if (txtVouType.Text == null || txtVouType.Text.ToString().Trim() == string.Empty)
                {
                    _SSSale.CrdbVouType = txtVouType.Text.ToString();
                }
                _SSSale.SaleSubType = FixAccounts.SubTypeForDebtorSale;
                _SSSale.GetLastRecordForSale();
                FillSearchData(_SSSale.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool Print()
        {
            bool retValue = true;
            if (_SSSale.CrdbAmountNet > 0)
            {
                if (General.PharmaSYSDistributorPlusLicense.LicenseType == LicenseLib.LicenseTypes.Full)
                {
                    if (General.CurrentSetting.MsetSortOrder == FixAccounts.SortByShelf)
                        mpPVC1.Sort(mpPVC1.ColumnsMain["Col_Shelf"], ListSortDirection.Ascending);

                    if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                        PrintSaleBillPrePrintedPaper();
                    else
                        PrintSaleBillPlainPaper();
                }
                else
                {
                    PSDialogResult result;
                    result = PSMessageBox.Show("Trial License", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                }
            }
            return retValue;
        }
        private void PrintSaleBillPrePrintedPaper()
        {
            PharmaSYSDistributorPlus.Printing.PrePrintedPaperPrinter printer = new PharmaSYSDistributorPlus.Printing.PrePrintedPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, _SSSale.PatientShortAddress, _SSSale.MobileNumberForSMS, _SSSale.DoctorName, _SSSale.DoctorAddress, mpPVC1.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount, "");

        }

        private void PrintSaleBillPlainPaper()
        {
            PharmaSYSDistributorPlus.Printing.PlainPaperPrinter printer = new PharmaSYSDistributorPlus.Printing.PlainPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, _SSSale.PatientShortAddress, _SSSale.MobileNumberForSMS, _SSSale.DoctorName, _SSSale.DoctorAddress, mpPVC1.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount, "");

        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            _SSSale.SaleSubType = FixAccounts.SubTypeForDebtorSale;
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _SSSale.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _SSSale.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _SSSale.GetFirstRecord();
            if (dr != null && dr["ID"] != DBNull.Value)
            {
                _SSSale.Id = dr["ID"].ToString();
                FillSearchData(_SSSale.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            _SSSale.SaleSubType = FixAccounts.SubTypeForDebtorSale;
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _SSSale.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _SSSale.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            _SSSale.SaleSubType = FixAccounts.SubTypeForDebtorSale;
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _SSSale.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _SSSale.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _SSSale.CrdbVouNo = i;
                dr = _SSSale.ReadDetailsByVouNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["ID"] != DBNull.Value)
            {
                _SSSale.Id = dr["ID"].ToString();
                FillSearchData(_SSSale.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _SSSale.GetLastVoucherNumber(txtVouType.Text.ToString(), FixAccounts.SubTypeForDebtorSale, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            _SSSale.SaleSubType = FixAccounts.SubTypeForDebtorSale;
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _SSSale.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _SSSale.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno + 1;
                i <= lastvouno; i++)
            {
                _SSSale.CrdbVouNo = i;
                dr = _SSSale.ReadDetailsByVouNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["ID"] != DBNull.Value)
            {
                _SSSale.Id = dr["ID"].ToString();
                FillSearchData(_SSSale.Id, "");
            }
            return retValue;
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
            _SSSale.IfNewPatient = "N";

            if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
            {
                CalculateAmount(-1);
                if (txtNetAmount.Text != null && Convert.ToDouble(txtNetAmount.Text.ToString()) > 0)
                {

                    try
                    {
                        if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                            txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                        else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                            txtVouType.Text = FixAccounts.VoucherTypeForCreditStatementSale;
                        else
                            txtVouType.Text = FixAccounts.VoucherTypeForCreditSale;
                        //if (rbtCash.Checked == true)
                        //    txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                        //else if (rbtCreditStatement.Checked == true)
                        //    txtVouType.Text = FixAccounts.VoucherTypeForCreditStatementSale;
                        //else
                        //    txtVouType.Text = FixAccounts.VoucherTypeForCreditSale;
                        _SSSale.SaleSubType = FixAccounts.SubTypeForDebtorSale;
                        System.Text.StringBuilder _errorMessage;
                        if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != string.Empty)
                        {
                            _SSSale.AccountID = mcbCreditor.SelectedID.Trim();
                            _SSSale.CrdbName = mcbCreditor.SeletedItem.ItemData[2];
                        }
                        _SSSale.DocID = mcbDoctor.SelectedID;
                        if (txtPatient.SelectedID != null && txtPatient.SelectedID != string.Empty)
                            _SSSale.DebtorsPatientID = txtPatient.SelectedID;
                        _SSSale.PatientID = "";
                        _SSSale.NewPatientIDInDebtorSale = "";
                        if (txtPatient.SelectedID == null || txtPatient.SelectedID.ToString().Trim() == string.Empty)
                        {
                            _IfNewDoctor = "N";
                            if (_SSSale.DocID == string.Empty)
                            {
                                _IfNewDoctor = "Y";
                                _SSSale.DocID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            }
                            _SSSale.IfNewPatient = "Y";
                            _SSSale.NewPatientIDInDebtorSale = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _SSSale.DebtorsPatientID = _SSSale.NewPatientIDInDebtorSale;
                        }
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
                        _SSSale.TotalDiscount5 = Convert.ToDouble(txtdiscountAmount5.Text.ToString());
                        _SSSale.TotalDiscount12point5 = Convert.ToDouble(txtDiscountAmount12point5.Text.ToString());
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
                        //_SSSale.PendingAmount = Convert.ToDouble(txtPendingBalance.Text.ToString());  [ansuman]
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
                        if (txtPatientAddress.Text != null && txtPatientAddress.Text != string.Empty)
                        {
                            if (txtPatientAddress.Text != null && txtPatientAddress.Text != "")
                                _SSSale.PatientAddress1 = txtPatientAddress.Text;
                        }
                        else
                        {
                            if (txtAddress1.Text != null && txtAddress1.Text != "")
                                _SSSale.PatientAddress1 = txtAddress1.Text;
                            if (txtAddress2.Text != null && txtAddress2.Text != "")
                                _SSSale.PatientAddress2 = txtAddress2.Text;
                        }
                        if (txtPatientAddress.Text != null && txtPatientAddress.Text.ToString() != string.Empty)
                            _SSSale.PatientShortAddress = txtPatientAddress.Text.ToString();
                        if (_SSSale.DoctorAddress.Length > 50)
                            _SSSale.DoctorAddress = _SSSale.DoctorAddress.Substring(0, 50);
                        if (_SSSale.PatientShortAddress.Length > 50)
                            _SSSale.PatientShortAddress = _SSSale.PatientShortAddress.Substring(0, 50);
                        if (txtMobileNumber.Text != null && txtMobileNumber.Text != string.Empty)
                            _SSSale.MobileNumberForSMS = txtMobileNumber.Text;

                        _SSSale.OperatorID = "";
                        _SSSale.OperatorPassword = txtOperator.Text.ToString();
                        CalculateProfitPercent();
                        if (_Mode == OperationMode.Edit)
                            _SSSale.IFEdit = "Y";
                        //  else
                        //       _SSSale.PendingAmount = _SSSale.PendingAmount + _SSSale.CrdbAmountNet;
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            LockTable.LocktblVoucherNo();
                            _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                        }
                        _SSSale.Validate();

                        if (_SSSale.IsValid)
                        {
                            LockTable.LockTablesForSale();
                            bool ifstockavailable = true;
                            if (_SSSale.IfTypeChange == "N" && General.CurrentSetting.MsetSaleAllowNegativeStock != "Y")
                                ifstockavailable = CheckForStockintblStock();
                            if (ifstockavailable)
                            {
                                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                                {
                                    General.BeginTransaction();

                                    _SSSale.CreatedBy = General.CurrentUser.Id;
                                    _SSSale.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                    _SSSale.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                                    _SSSale.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    //_SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                                    txtVouchernumber.Text = Convert.ToString(_SSSale.CrdbVouNo);
                                    if (_SSSale.IfNewPatient == "Y" && General.CurrentSetting.MsetSaleSaveCustomerInMaster == "Y")
                                    {
                                        if (_IfNewDoctor == "Y")
                                            _SSSale.SaveNewDoctor();
                                        _SSSale.SaveNewPatient();
                                    }
                                    else
                                    {
                                        if (mcbCreditor.SeletedItem != null && string.IsNullOrEmpty(Convert.ToString(mcbCreditor.SelectedID)) == false &&
                                            string.IsNullOrEmpty(Convert.ToString(mcbCreditor.SeletedItem.ItemData[10])) == true && string.IsNullOrEmpty(txtMobileNumber.Text) == false)
                                        {
                                            Account _Account = new Account();
                                            _Account.Id = mcbCreditor.SelectedID;
                                            _Account.AccMobileNumberForSMS = txtMobileNumber.Text;
                                            _Account.UpdateMobilenoInAccountDetail();
                                        }
                                    }
                                    retValue = _SSSale.AddDetails();
                                    _SavedID = _SSSale.Id;
                                    if (retValue)
                                        retValue = SaveparticularsProductwise();
                                    if (retValue)
                                        retValue = ReduceStockIntblStock();
                                    if (retValue)
                                    {
                                        clearPreviousdebitcreditnotes();
                                        if (_SSSale.CrNoteAmount > 0 || _SSSale.DbNoteAmount > 0)
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
                                        // UpdateClosingStockinCache();
                                        HasShowSavedMessage(printData, Convert.ToDouble(txtBillAmount.Text.ToString()));
                                        //string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                                        //PSDialogResult result;
                                        //if (printData)
                                        //{
                                        //    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                        //    Print();
                                        //}
                                        //else
                                        //{
                                        //    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                        //    if (result == PSDialogResult.Print)
                                        //        Print();
                                        //}
                                        retValue = true;
                                        //  RefreshProductGrid();
                                    }
                                    else
                                    {
                                        PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                        retValue = false;
                                    }
                                }
                                else if (_Mode == OperationMode.Fifth && _SSSale.StatementNumber == 0)
                                {
                                    DataTable stocktbl = new DataTable();
                                    _SSSale.OldVoucherType = txtVouType.Text.ToString();
                                    _SSSale.OldVoucherNumber = _SSSale.CrdbVouNo;
                                    _SSSale.CrdbVouNo = int.Parse(txtVouchernumber.Text);
                                    //if (mcbBankAccount.SelectedID != null)
                                    //    _SSSale.CreditCardBankID = mcbBankAccount.SelectedID; //Amar
                                    //_SSSale.AccountID = FixAccounts.AccountCashCreditSale;
                                    if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != string.Empty)
                                    {
                                        _SSSale.AccountID = mcbCreditor.SelectedID.Trim();
                                        _SSSale.CrdbName = mcbCreditor.SeletedItem.ItemData[2];
                                    }
                                    if (mcbBankAccount.SelectedID != null)
                                        _SSSale.CreditCardBankID = mcbBankAccount.SelectedID; //Amar

                                    if (cbTransactionType.Text == cbNewTransactionType.Text)
                                        _SSSale.IfTypeChange = "N";
                                    else
                                        _SSSale.IfTypeChange = "Y";
                                    if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCash)
                                        _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
                                    else if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                                        _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
                                    else if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCreditCard) //Amar
                                    {
                                        _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
                                        _SSSale.SaleSubType = FixAccounts.SubTypeForCreditCardSale;
                                    }
                                    else
                                        _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditStatementSale;

                                    General.BeginTransaction();

                                    if (_SSSale.IfTypeChange == "Y")
                                    {
                                        if (_SSSale.OldVoucherType != _SSSale.CrdbVouType)
                                        {
                                            _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(_SSSale.CrdbVouType, General.ShopDetail.ShopVoucherSeries);
                                            txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString();
                                            txtVouType.Text = _SSSale.CrdbVouType;
                                            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                                            {
                                                _SSSale.CrdbAmountBalance = 0;
                                                _SSSale.CrdbAmountClear = _SSSale.CrdbAmountNet;
                                            }
                                            else
                                            {
                                                _SSSale.CrdbAmountBalance = _SSSale.CrdbAmountNet;
                                                _SSSale.CrdbAmountClear = 0;
                                            }
                                            retValue = _SSSale.UpdateDetailsForTypeChange();
                                            if (retValue)
                                            {
                                                retValue = _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
                                            }
                                            if (retValue)
                                            {
                                                SaveIntblTrnac();
                                            }
                                            if (retValue)
                                                _SSSale.UpdateCreditDebitNoteforTypeChange(_SSSale.CreditDebitNoteID, _SSSale.Amount, _SSSale.CrdbVouType, _SSSale.CrdbVouNo, _SSSale.CrdbVouDate, "", _SSSale.Id);

                                            if (retValue)
                                                General.CommitTransaction();
                                            else
                                                General.RollbackTransaction();
                                            LockTable.UnLockTables();
                                            if (retValue)
                                            {
                                                //  UpdateClosingStockinCache(); 
                                                string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                                                PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                                //     MessageBox.Show("Information has been saved successfully.'", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retValue = true;
                                            }
                                            else
                                            {
                                                PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                                retValue = false;
                                            }
                                        }
                                        else if (_SSSale.SaleSubType !="D")  //Amar
                                        {
                                            _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(_SSSale.CrdbVouType, General.ShopDetail.ShopVoucherSeries);
                                            txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString();
                                            txtVouType.Text = _SSSale.CrdbVouType;
                                            _SSSale.SaleSubType = FixAccounts.SubTypeForCreditCardSale; //Amar
                                            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                                            {
                                                _SSSale.CrdbAmountBalance = 0;
                                                _SSSale.CrdbAmountClear = _SSSale.CrdbAmountNet;
                                            }
                                            else
                                            {
                                                _SSSale.CrdbAmountBalance = _SSSale.CrdbAmountNet;
                                                _SSSale.CrdbAmountClear = 0;
                                            }
                                            retValue = _SSSale.UpdateDetailsForTypeChange();
                                            if (retValue)
                                            {
                                                retValue = _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
                                            }
                                            if (retValue)
                                            {
                                                SaveIntblTrnac();
                                            }
                                            if (retValue)
                                                _SSSale.UpdateCreditDebitNoteforTypeChange(_SSSale.CreditDebitNoteID, _SSSale.Amount, _SSSale.CrdbVouType, _SSSale.CrdbVouNo, _SSSale.CrdbVouDate, "", _SSSale.Id);

                                            if (retValue)
                                                General.CommitTransaction();
                                            else
                                                General.RollbackTransaction();
                                            LockTable.UnLockTables();
                                            if (retValue)
                                            {
                                                //  UpdateClosingStockinCache(); 
                                                string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                                                PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                                //     MessageBox.Show("Information has been saved successfully.'", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                retValue = true;
                                            }
                                            else
                                            {
                                                PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                                retValue = false;
                                            }
                                        }

                                        ClearData();
                                    }
                                }
                                else if (_Mode == OperationMode.Edit)
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
                                        //General.BeginTransaction();
                                        retValue = SaveparticularsProductwise();

                                        if (retValue)
                                            retValue = ReduceStockIntblStock();
                                        if (retValue)
                                        {
                                            clearPreviousdebitcreditnotes();
                                            if (_SSSale.CrNoteAmount > 0 || _SSSale.DbNoteAmount > 0)
                                                SaveAndUpdateDebitCreditNote();
                                        }
                                        if (retValue)
                                            retValue = SaveIntblTrnac();
                                        if (retValue)
                                            General.CommitTransaction();
                                        else
                                        {
                                            General.RollbackTransaction();

                                            PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                            retValue = false;
                                        }
                                    }
                                    LockTable.UnLockTables();
                                    if (retValue)
                                    {
                                        // UpdateClosingStockinCache();
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
                                        //  RefreshProductGrid();
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
            mpPVC1.EditedTempDataList = null;
            return retValue;
        }
        private void HasShowSavedMessage(bool printData, double BillAmt)
        {
            string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
            PSDialogResult result;
            if (printData)
            {
                result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK, Convert.ToDouble(txtNetAmount.Text.ToString()));
                Print();
                //Amar For Sms
                if (General.CurrentSetting.SmsSetDebtorSale  == "Y")
                {
                    SendSMS mSMS = new SendSMS();
                    string Msg = "Dear Customer Your Bill No.:'" + _SSSale.CrdbVouNo + "' Of Amount :'" + _SSSale.CrdbAmountNet + "' Thank You For Dealing With Us.";
                    Msg += mSMS.GetShopDetailsFromData();
                    if (string.IsNullOrEmpty(_SSSale.MobileNumberForSMS) == false)
                    {
                        mSMS.SendSMSData(_SSSale.MobileNumberForSMS, Msg);
                    }
                    else
                        MessageBox.Show("Please Update Mobile Number For Sending SMS", "PharmaSYSDistributorPlus", MessageBoxButtons.OK);
                }
            }
            else
            {
                if (General.CurrentSetting.MsetSaleTenderAmount == "Y")
                {
                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print, Convert.ToDouble(BillAmt.ToString()));
                    if (result == PSDialogResult.Print)
                        Print();
                    //Amar For Sms
                    if (General.CurrentSetting.SmsSetDebtorSale == "Y")
                    {
                        SendSMS mSMS = new SendSMS();
                        string Msg = "Dear Customer Your Bill No.:'" + _SSSale.CrdbVouNo + "' Of Amount :'" + _SSSale.CrdbAmountNet + "' Thank You For Dealing With Us.";
                        Msg += mSMS.GetShopDetailsFromData();
                        if (string.IsNullOrEmpty(_SSSale.MobileNumberForSMS) == false)
                        {
                            mSMS.SendSMSData(_SSSale.MobileNumberForSMS, Msg);
                        }
                        else
                            MessageBox.Show("Please Update Mobile Number For Sending SMS", "PharmaSYSDistributorPlus", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                    if (result == PSDialogResult.Print)
                        Print();
                    //Amar For Sms
                    if (General.CurrentSetting.SmsSetDebtorSale == "Y")
                    {
                        SendSMS mSMS = new SendSMS();
                        string Msg = "Dear Customer Your Bill No.:'" + _SSSale.CrdbVouNo + "' Of Amount :'" + _SSSale.CrdbAmountNet + "' Thank You For Dealing With Us.";
                        Msg += mSMS.GetShopDetailsFromData();
                        if (string.IsNullOrEmpty(_SSSale.MobileNumberForSMS) == false)
                        {
                            mSMS.SendSMSData(_SSSale.MobileNumberForSMS, Msg);
                        }
                        else
                            MessageBox.Show("Please Update Mobile Number For Sending SMS", "PharmaSYSDistributorPlus", MessageBoxButtons.OK);
                    }
                }
            }
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
                    // if (_Mode == OperationMode.ReportView)
                    //if(_SSSale.CrdbAmountBalance != _SSSale.CrdbAmountNet && _SSSale.CrdbVouType != FixAccounts.VoucherTypeForCashSale)
                    //{
                    //    _Mode = OperationMode.View;View();
                    //}
                    FillDoctorCombo();
                    mcbDoctor.SelectedID = _SSSale.DocID;
                    if (_SSSale.DocID == string.Empty)
                        mcbDoctor.Text = _SSSale.DoctorName;
                    FillPatientCombo();
                    txtPatient.SelectedID = _SSSale.DebtorsPatientID;
                    txtVouType.Text = _SSSale.CrdbVouType;
                    if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForCreditSale)
                        btnPaymentHistory.Visible = false;
                    else
                    {
                        btnPaymentHistory.Visible = true;
                        BindPaymentDetails();
                    }

                    //FillPatientCombo();
                    FillPartyCombo();
                    if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
                        BindTempGrid();


                    if (btnPaymentHistory.Visible == true)
                        BindPaymentDetails();
                    mcbCreditor.SelectedID = _SSSale.AccountID;

                    InitialisempPVC1(Vmode);

                    txtAddress1.Text = _SSSale.PatientAddress1;
                    txtAddress2.Text = _SSSale.PatientAddress2;
                    txtPatient.Text = _SSSale.ShortName;
                    txtPatientAddress.Text = _SSSale.PatientShortAddress;
                    txtMobileNumber.Text = _SSSale.MobileNumberForSMS;
                    txtNarration.Text = _SSSale.CrdbNarration.ToString();
                    txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString().Trim();
                    txtTokenNumber.Text = _SSSale.TokenNumber.ToString();
                    txtdiscountAmount5.Text = _SSSale.TotalDiscount5.ToString();
                    txtDiscountAmount12point5.Text = _SSSale.TotalDiscount12point5.ToString();
                    txtTotalafterdiscount.Text = "0.00";
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
                    txtDiscAmount.Text = _SSSale.CrdbDiscAmt.ToString("#0.00");
                    DateTime mydate = new DateTime(Convert.ToInt32(_SSSale.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    txtAddOn.Text = _SSSale.CrdbAddOn.ToString("#0.00");
                    txtCreditNote.Text = _SSSale.CrNoteAmount.ToString("#0.00");
                    txtDebitNote.Text = _SSSale.DbNoteAmount.ToString("#0.00");
                    txtAmountforZeroVAT.Text = _SSSale.CrdbAmtForZeroVAT.ToString("#0.00");
                    txtAmountVAT12Point5Per.Text = _SSSale.CrdbAmountVat12point5.ToString("#0.00");
                    txtAmountVAT5Per.Text = _SSSale.CrdbAmountVat5.ToString("#0.00");
                    txtnextVisitDate.Text = General.GetDateInShortDateFormat(_SSSale.NextVisitDate.ToString());


                    if (txtnextVisitDate.Text != null && txtnextVisitDate.Text != string.Empty) //Amar
                    {
                        DateTime mdate = new DateTime(Convert.ToInt32(_SSSale.NextVisitDate.ToString().Substring(0, 4)), Convert.ToInt32(_SSSale.NextVisitDate.ToString().Substring(4, 2)), Convert.ToInt32(_SSSale.NextVisitDate.ToString().Substring(6, 2)));
                        TimeSpan mdays = mdate - mydate;
                        int days = mdays.Days;
                        txtNextVisitDays.Text = days.ToString();
                        txtDayOFWeek.Text = mdate.DayOfWeek.ToString();
                    }
                    else
                    {
                        txtnextVisitDate.Text = "";
                        txtDayOFWeek.Text = "";
                        txtNextVisitDays.Text = "";
                    } //End

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
                    cbTransactionType.Enabled = false;
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
                        txtVouchernumber.Enabled = false;
                        mpPVC1.SetFocus(1);
                        txtVouchernumber.Enabled = true;
                        if (_Mode == OperationMode.Edit)
                        {
                            cbTransactionType.Enabled = false;
                        }

                    }
                    if (_Mode == OperationMode.Fifth && _SSSale.StatementNumber == 0)
                    {
                        if (_SSSale.CrdbAmountClear > 0 && _SSSale.CrdbVouType != FixAccounts.VoucherTypeForDistributorSaleCash)
                            lblFooterMessage.Text = "Payment Done";
                        else
                        {
                            pnlTop.Enabled = false;
                            mpPVC1.Enabled = false;
                            pnlTypeChange.BringToFront();
                            pnlTypeChange.Visible = true;
                            btnTypeChange.Visible = true;
                            cbNewTransactionType.Visible = true;
                            cbNewTransactionType.Enabled = false;
                            btnTypeChange.Enabled = true;
                            btnTypeChange.Focus();
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
        public override void ReFillData(Control closedControl)
        {
            try
            {
                this.Focus();
                //if (closedControl is UclPatient)
                //    FillPatientCombo();
                if (closedControl is UclAccount)
                {
                    string preselectedID = "";
                    if (mcbCreditor.SelectedID != null)
                        preselectedID = mcbCreditor.SelectedID;
                    FillPartyCombo();
                    mcbCreditor.SelectedID = preselectedID;
                }
                else if (closedControl is UclCreditNoteAmount || closedControl is UclCreditNoteStock || closedControl is UclDebitNoteAmount || closedControl is UclDebitNotestock)
                    FillCreditDebitNote();
                else if (closedControl is UclProduct || closedControl is UclPurchase)
                    RefreshProductGrid();
                FillTransactionType();
                mcbCreditor.SelectedID = _SSSale.AccountID;


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
                    //txtPatient.Focus();
                    //retValue = true;
                    btnPreviousSaleClick();
                    retValue = true;
                }
                if (keyPressed == Keys.R && modifier == Keys.Alt)
                {
                    BtnClearPatientClick();     //mcbDoctor.Focus();
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
                if (uclSubstituteControl1.Visible)
                {
                    retValue = uclSubstituteControl1.HandleShortcutAction(keyPressed, modifier); // ansuman
                }
                if (keyPressed == Keys.Escape)
                {
                    if (dgBatch.Visible)
                    {
                        dgBatch.Visible = false;
                        retValue = true;
                    }
                    else if (pnlDebitCreditNote.Visible)
                    {
                        btnOKCreditDebitNoteClick();
                        retValue = true;
                    }
                    else if (pnlDebtorProduct.Visible == true)
                    {
                        btnCancelFillClick();
                        retValue = true;
                    }
                    else if (mpPVC1.VisibleProductGrid() == true) //kiran
                    {
                        lblFooterMessage.Text = "";
                        lblRightSideFooterMsg.Text = "";
                        // retValue = true;
                    }
                    else if (uclSubstituteControl1.Visible == true) // [ansuman][26.11.2016]
                    {
                        uclSubstituteControl1.Visible = false;
                        retValue = true;
                    }
                    else if (dgvPreviousSale.Visible == true)
                    {
                        dgvPreviousSale.Visible = false;
                        retValue = true;
                    }
                    else if (dgPrviousSaleBillWise.Visible == true)
                    {
                        dgPrviousSaleBillWise.Visible = false;
                        dgvPreviousSale.Visible = true;
                        dgvPreviousSale.Focus();
                        retValue = true;
                    }
                    else if (pnlClone.Visible == true)
                    {
                        pnlClone.Visible = false;
                        pnlClone.SendToBack();
                    }
                    else if (ISSaleSummaryShow == true)
                    {
                        if (base.ctrlUclSaleSummaryControl.Visible)
                        {
                            ctrlUclSaleSummaryControl.SendToBack();
                            ctrlUclSaleSummaryControl.Visible = false;
                        }
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
        public override string[] FillAllShortcutkeys()
        {
            string[] ShortKeys = new string[12];
            ShortKeys[0] = "Clone ---------->  Alt + C";
            ShortKeys[1] = "Edit Rate -------->  Alt + E";
            ShortKeys[2] = "Similar Product -> Alt + M";
            ShortKeys[3] = "Pre. Sale ------->  Alt + P";
            ShortKeys[4] = "Clear Patient --->  Alt + R";
            ShortKeys[5] = "";
            ShortKeys[6] = "";
            ShortKeys[7] = "";
            ShortKeys[8] = "";
            ShortKeys[9] = "";
            ShortKeys[10] = "";
            ShortKeys[11] = "";
            return ShortKeys;
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
            double mprate = 0;
            double mvatper = 0;
            double mvatamt = 0;
            double mamt = 0;
            double mrate = 0;

            double totalsale = 0;
            double totalpur = 0;
            double totalvat = 0;
            //  double totaldisc = 0;

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
                    mprate = 0;
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
                        mrate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());

                        if (prodrow.Cells["Col_TradeRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                        if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                            double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                        _SSSale.TradeRate = mtraderate;
                        double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                        if (prodrow.Cells["Col_VATPer"].Value != null)
                            double.TryParse(prodrow.Cells["Col_VATPer"].Value.ToString(), out mvatper);
                        mvatamt = Math.Round((mtraderate * mvatper) / 100, 2);
                        mamt = Math.Round(Math.Round(mrate / mpakn, 4) * mqty, 2);
                        double mdiscamt = 0;
                        if (prodrow.Cells["Col_DiscountAmount"].Value != null)
                            mdiscamt = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_MySpecialDiscountAmount"].Value != null && prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString() != string.Empty)
                            mdiscamt += Convert.ToDouble(prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString());
                        //mdiscamt = mdiscamt / mpakn;
                        _SSSale.SaleRate = msalerate;
                        double newmdiscper = 0;
                        double newmydiscper = 0;
                        double.TryParse(txtDiscPercent.Text, out newmdiscper);
                        //  double.TryParse(txtMyDiscountPercent.Text, out newmydiscper);
                        double newsalerate = msalerate - Math.Round(((msalerate - Math.Round((msalerate * mvatper / 100), 2)) * (newmdiscper + newmydiscper) / 100), 2);
                        double vatontrrate = Math.Round((mtraderate * mvatper) / 100, 2);

                        double PerPackDiscount = mdiscamt / (mqty / mpakn);
                        totalvat += vatontrrate;
                        totalsale += newsalerate;
                        totalpur += mpurrate;

                        _SSSale.ProfitPercentBySaleRate = Math.Round(((msalerate - PerPackDiscount) - (mpurrate)) / (msalerate - PerPackDiscount), 4);
                        _SSSale.ProfitPercentByPurchaseRate = Math.Round(((msalerate - PerPackDiscount) - (mpurrate)) / (mpurrate), 4);
                        //_SSSale.TotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
                        //_SSSale.TotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
                        _SSSale.ProfitInRupees = Math.Round((((msalerate - PerPackDiscount) - (mpurrate)) / mpakn) * mqty, 2);
                        _SSSale.TotalProfitInRupees += _SSSale.ProfitInRupees;
                        prodrow.Cells["Col_ProfitPercentBySaleRate"].Value = _SSSale.ProfitPercentBySaleRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _SSSale.ProfitPercentByPurchaseRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitInRupees"].Value = _SSSale.ProfitInRupees.ToString("#0.00");
                    }

                }
                _SSSale.TotalProfitPercentBySaleRate = Math.Round(((totalsale) - (totalpur + totalvat)) / (totalsale), 4);
                _SSSale.TotalProfitPercentByPurchaseRate = Math.Round(((totalsale) - (totalpur + totalvat)) / (totalpur + totalvat), 4);
                //if (General.CurrentSetting.MsetSaleShowProfitInSaleBill == "Y")
                //{
                //    txtProfit.Text = _SSSale.TotalProfitInRupees.ToString("#0.00");
                //} 
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
                            if (prodrow.Cells["Col_DiscountAmount"].Value != null)
                                _SSSale.CrdbDiscAmt = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                            if (prodrow.Cells["Col_MySpecialDiscountAmount"].Value != null && prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString() != string.Empty)
                                _SSSale.MySpecialDiscountAmount = Convert.ToDouble(prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString());
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
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput6Sale;
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
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput13point5Sale;
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
                        if (prodrow.Cells["Col_StockID"].Value != null)
                            mlastsaleid = prodrow.Cells["Col_StockID"].Value.ToString();
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

        //private bool UpdateClosingStockinCache()
        //{
        //    bool returnVal = false;
        //    try
        //    {               
        //        General.UpdateProductListCacheTest(mpPVC1.Rows, "Col_ProductID", dgtemp.Rows, "Temp_ProductID");                
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        returnVal = false;
        //    }
        //    return returnVal;
        //}
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

                        string ifRecordFound = "";
                        ifRecordFound = _SSSale.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                            returnVal = _SSSale.UpdateIntblStockAdd();
                        returnVal = _SSSale.UpdateDebtorSaleStockInMasterProductAddFromTemp();
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

                if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
                {
                    Product prod = new Product();
                    dtable = prod.GetOverviewData();
                    mpPVC1.DataSourceProductList = dtable;
                }
                // mpPVC1.DataSourceProductList = General.ProductList;

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
                    //  CalculateAllAmounts();
                }
                else
                    mpPVC1.Bind();

                if (_Mode != OperationMode.Add && _Mode != OperationMode.Delete)
                {
                    FillRatePerunit();
                }

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
            //   dgCreditNote.DoubleColumnNames.Add("Col_AmountNet");
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
                mpPVC1.EditedTempDataList = _BindingSource;
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
                mpPVC1.ProductListGridWidth = 700;
                mpPVC1.BatchListGridWidth = 690;
                txtCRAmountSelected.Text = "0.00";
                txtDNAmountSelected.Text = "0.00";
                pnlTypeChange.Visible = false;
                pnlTypeChange.SendToBack();
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                mcbDoctor.SelectedID = "";
                txtVoucherSeries.Text = General.ShopDetail.ShopVoucherSeries;
                _SSSale.CrdbVouSeries = txtVoucherSeries.Text.ToString();
                mcbDoctor.Text = "";
                txtDoctorAddress.Text = "";
                txtMobileNumber.Text = "";
                txtNarration.Text = General.CurrentSetting.MsetFixedNarration.ToString();
                txtVouchernumber.Clear();
                txtVouType.Text = "   ";
                _SSSale.CrdbVouType = txtVouType.Text.ToString();
                _SSSale.SaleSubType = FixAccounts.SubTypeForDebtorSale;
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtTotalAmount.Text = "0.00";
                txtAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtdiscountAmount5.Text = "0.00";
                txtDiscountAmount12point5.Text = "0.00";
                txtTotalafterdiscount.Text = "0.00";
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
                txtMobileNumber.Text = string.Empty;
                mcbPrescription.SelectedID = "";
                mcbCreditor.Focus();
                lblFooterMessage.Text = "";
                lblRightSideFooterMsg.Text = "";
                cbTransactionType.Enabled = true;
                _SSSale.IfNewPatient = "N";
                btnPreviousSale.Text = "0.00";
                btnPreviousSale.Enabled = false;
                txtDayOFWeek.Text = ""; //Amar
                txtNextVisitDays.Text = "0";
                txtnextVisitDate.Text = "";
                if (_Mode != OperationMode.Add)
                {
                    mcbCreditor.Enabled = false;
                    txtTokenNumber.Enabled = false;
                    txtVouchernumber.ReadOnly = false;
                    txtVouchernumber.Enabled = true;
                    //if (_Mode != OperationMode.Edit)
                    //    txtPatient.Enabled = false;
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
                pnlClone.SendToBack();
                txtclonevouno.Text = "";
                cbclonevoutype.Text = "";
                btnPreviousSale.Text = "0.00";
                InitializeScreen();
                FillBankAccountCombo();
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
                mcbCreditor.SourceDataString = new string[11] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccNameAddress", "AccTokenNumber", "AccDoctorID", "AccTransactionType", "AccDiscountOffered", "MobileNumberForSMS" };
                mcbCreditor.ColumnWidth = new string[10] { "0", "20", "250", "250", "0", "0", "0", "0", "0", "0" };
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

        private void FillPatientCombo()
        {
            try
            {
                txtPatient.SelectedID = null;
                txtPatient.SourceDataString = new string[9] { "PatientID", "PatientName", "PatientAddress1", "PatientAddress2", "ShortNameAddress", "TokenNumber", "DoctorID", "AccCode", "DiscountOffered" };
                txtPatient.ColumnWidth = new string[9] { "0", "200", "200", "200", "0", "40", "0", "0", "0" };
                txtPatient.ValueColumnNo = 0;
                //txtPatient.UserControlToShow = new UclPatient();
                //Patient _Party = new Patient();
                //DataTable dtable = _Party.GetOverviewData();
                //txtPatient.FillData(dtable);
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
                    //  dgCreditNote.DoubleColumnNames.Add("Col_AmountNet");
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
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);  //Amar
            if (General.CurrentSetting.MsetSaleCreditSale == "Y")
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditCard);
            cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);

        }

        //private bool BindCreditNoteDebitNote(DataTable dt)
        //{
        //    bool retValue = true;
        //    try
        //    {

        //        if (dgCreditNote != null)
        //            dgCreditNote.Rows.Clear();
        //        int _RowIndex = 0;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            _RowIndex = dgCreditNote.Rows.Add();
        //            string voudt = "";
        //            double amtclear = 0;
        //            DataGridViewRow currentdr = dgCreditNote.Rows[_RowIndex];
        //            currentdr.Cells["Col_Check"].Value = false;
        //            currentdr.Cells["Col_CrdbID"].Value = dr["CRDBID"].ToString();
        //            currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
        //            currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
        //            if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
        //            {
        //                voudt = dr["VoucherDate"].ToString();
        //                voudt = General.GetDateInShortDateFormat(voudt);
        //            }
        //            currentdr.Cells["Col_VoucherDate"].Value = voudt;
        //            currentdr.Cells["Col_AmountNet"].Value = dr["AmountNet"].ToString();
        //            currentdr.Cells["Col_Narr"].Value = dr["Narration"].ToString();
        //            if (dr["AmountClear"] != DBNull.Value && dr["AmountClear"].ToString() != "")
        //                double.TryParse(dr["AmountClear"].ToString(), out amtclear);
        //            currentdr.Cells["Col_AmountClear"].Value = dr["AmountClear"].ToString();
        //            //if (_Mode == OperationMode.Delete)
        //            //    currentdr.Cells["Col_Check"].Value = false;
        //            // else
        //            if (dr["ClearedInID"] != DBNull.Value && dr["ClearedInID"].ToString() != "" && dr["ClearedInID"].ToString() == _SSSale.Id)
        //            {
        //                currentdr.Cells["Col_Check"].Value = true;
        //            }
        //            _RowIndex += 1;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteError(ex.ToString());
        //        retValue = false;
        //    }
        //    return retValue;
        //}
        private bool SaveAndUpdateDebitCreditNote()
        {
            {
                bool returnVal = false;
                try
                {
                    foreach (DataGridViewRow prodrow in dgCreditNote.Rows)
                    {
                        if ((prodrow.Cells["Col_CrdbID"].Value) != null && (prodrow.Cells["Col_Check"].Value.ToString() != string.Empty))
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

            return true;
        }


        private void CalculateAmount(int deletedrowindex)
        {
            lblFooterMessage.Text = "";
            lblRightSideFooterMsg.Text = "";
            double mTotalAmount = 0;
            double mTotalAmountVAT5 = 0;
            double mTotalAmountVAT12 = 0;

            double mvatper = 0;
            double mvatamount5 = 0;
            double mvatamount12point5 = 0;
            double mtotamtvat0 = 0;

            double mTvatamount5 = 0;
            double mTvatamount12point5 = 0;
            double mTtotamtvat0 = 0;



            double mrate = 0;
            double mamt = 0;
            double mpakn = 0;
            double mqty = 0;
            int itemCount = 0;

            double mcreditnote = 0;
            double mdebitnote = 0;
            double maddon = 0;
            double mtotamt = 0;

            // 9/12/2014   calculate discount after vat and calculate vat after subtracting vat from amt;
            double mmyspecialDiscountper = 0;
            double mmyspecialdiscountamt5 = 0;
            double mmyspecialdiscountamt12point5 = 0;
            double mmyspecialdiscountamtzero = 0;
            double mdiscamt5 = 0;
            double mdiscamt12point5 = 0;
            double mdiscamtzero = 0;
            double mdiscper = 0;
            double mnewamt = 0;
            double mnewamtwithoutmydiscount = 0;
            double mtotalafterdiscountwithoutmydiscount = 0;
            double mtotaldiscountamount5 = 0;
            double mtotaldiscountamount12point5 = 0;
            double mtotaldiscountamountzero = 0;
            double mtotalmyspecialdiscamt5 = 0;
            double mtotalmyspecialdiscamt12point5 = 0;
            double mtotalmyspecialdiscamtzero = 0;
            double mtotalafterdiscount = 0;
            string ifdiscount = "Y";
            double mprate = 0;


            if (txtDiscPercent.Text != null && txtDiscPercent.Text != string.Empty)
                mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());
            if (txtAddOn.Text != null && txtAddOn.Text.ToString() != string.Empty)
                maddon = Convert.ToDouble(txtAddOn.Text.ToString());
            double.TryParse(txtCreditNote.Text.ToString(), out mcreditnote);
            double.TryParse(txtDebitNote.Text.ToString(), out mdebitnote);
            //if (pnlSpecialDiscount.Visible == true)
            //{
            //    if (rbtnSpecialDiscount4.Checked == true)
            //        mmyspecialDiscountper = 0;
            //    else if (rbtnSpecialDiscount1.Checked == true)
            //        mmyspecialDiscountper = Convert.ToDouble(rbtnSpecialDiscount1.Text.ToString());
            //    else if (rbtnSpecialDiscount2.Checked == true)
            //        mmyspecialDiscountper = Convert.ToDouble(rbtnSpecialDiscount2.Text.ToString());
            //    else if (rbtnSpecialDiscount3.Checked == true)
            //        mmyspecialDiscountper = Convert.ToDouble(rbtnSpecialDiscount3.Text.ToString());
            //}

            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    mvatamount5 = 0;
                    mvatamount12point5 = 0;
                    mtotamtvat0 = 0;
                    mdiscamt5 = 0;
                    mdiscamt12point5 = 0;

                    mdiscamtzero = 0;
                    mmyspecialdiscountamt5 = 0;
                    mmyspecialdiscountamt12point5 = 0;
                    mnewamtwithoutmydiscount = 0;
                    mmyspecialdiscountamtzero = 0;
                    mnewamt = 0;

                    if (dr.Index != deletedrowindex)
                    {
                        if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "0" && dr.Cells["Col_Quantity"].Value.ToString() != "")
                        {
                            ifdiscount = "Y";
                            mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                            mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                            mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                            if (dr.Cells["Col_IfSaleDisc"].Value != null && dr.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                                ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString().ToUpper();
                            mprate = 0;
                            if (dr.Cells["Col_PurchaseRate"].Value != null && (dr.Cells["Col_PurchaseRate"].Value.ToString() != ""))
                                mprate = Convert.ToDouble(dr.Cells["Col_PurchaseRate"].Value.ToString());

                            if (Math.Truncate(mqty / mpakn) == (mqty / mpakn))
                                mamt = Math.Round((mqty / mpakn) * mrate, 2);
                            else
                            {
                                mamt = Math.Round(Math.Round(mrate / mpakn, 4) * mqty, 2);
                                //if (Math.Round(mamt, 1) - mamt < 0.05)
                                //    mamt = Math.Round(mamt, 1);
                            }

                            dr.Cells["Col_Amount"].Value = (mamt).ToString("#0.00");
                            if (mamt > 0)
                            {
                                mvatamount12point5 = 0;
                                mvatamount5 = 0;
                                mmyspecialDiscountper = 0;
                                mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());
                                // vat 5.5
                                if (mvatper == 6)
                                {
                                    mvatamount5 = Math.Round((mamt * mvatper) / (100 + mvatper), 2);
                                    mmyspecialdiscountamt5 = Math.Round((mamt - mvatamount5) * mmyspecialDiscountper / 100, 2);
                                    if (ifdiscount != "N")
                                        mdiscamt5 = Math.Round((mamt - mvatamount5) * mdiscper / 100, 2);
                                    else
                                        mdiscamt5 = 0;
                                }
                                else if (mvatper == 13.5)
                                {
                                    mvatamount12point5 = Math.Round((mamt * mvatper) / (100 + mvatper), 4);
                                    mmyspecialdiscountamt12point5 = Math.Round((mamt - mvatamount5) * mmyspecialDiscountper / 100, 4);
                                    if (ifdiscount != "N")
                                        mdiscamt12point5 = Math.Round((mamt - mvatamount12point5) * mdiscper / 100, 4);
                                    else
                                        mdiscamt12point5 = 0;
                                }
                                else
                                {
                                    mmyspecialdiscountamtzero = Math.Round((mamt) * mmyspecialDiscountper / 100, 2);
                                    if (ifdiscount != "N")
                                        mdiscamtzero = Math.Round(mamt * mdiscper / 100, 2);
                                    else
                                        mdiscamtzero = 0;
                                    mtotamtvat0 += mamt - mmyspecialdiscountamtzero - mdiscamtzero;
                                }
                                mtotaldiscountamount5 += mdiscamt5;
                                mtotaldiscountamount12point5 += mdiscamt12point5;
                                mtotaldiscountamountzero += mdiscamtzero;
                                mtotalmyspecialdiscamt5 += mmyspecialdiscountamt5;
                                mtotalmyspecialdiscamt12point5 += mmyspecialdiscountamt12point5;
                                mtotalmyspecialdiscamtzero += mmyspecialdiscountamtzero;
                                mnewamt = (mamt - mdiscamt5 - mdiscamt12point5 - mdiscamtzero - mmyspecialdiscountamt5 - mmyspecialdiscountamt12point5 - mmyspecialdiscountamtzero);
                                mnewamtwithoutmydiscount = (mamt - mdiscamt5 - mdiscamt12point5 - mdiscamtzero);
                                // vat 5.5
                                if (mvatper == 6)
                                {
                                    mvatamount5 = Math.Round((mnewamt * mvatper) / (100 + mvatper), 2);
                                }
                                else if (mvatper == 13.5)
                                {
                                    mvatamount12point5 = Math.Round((mnewamt * mvatper) / (100 + mvatper), 2);
                                }                                
                                dr.Cells["Col_VATAmount"].Value = (mvatamount12point5 + mvatamount5 ).ToString("#0.00");
                                dr.Cells["Col_DiscountAmount"].Value = (mdiscamt5 + mdiscamt12point5 + mdiscamtzero).ToString("#0.00");
                                dr.Cells["Col_MySpecialDiscountAmount"].Value = mmyspecialdiscountamt5 + mmyspecialdiscountamt12point5 + mmyspecialdiscountamtzero;
                                mTotalAmount += mamt;
                                mtotalafterdiscount += mnewamt;
                                // mtotalafterdiscountwithoutmydiscount += 
                                itemCount += 1;
                                //if (ifdiscount != "N")
                                //    mTotalAmountforDiscount += mamt;
                                mTvatamount5 += mvatamount5;
                                mTvatamount12point5 += mvatamount12point5;
                                mTtotamtvat0 += mtotamtvat0;
                                // vat 5.5
                                if (mvatper == 6)
                                    mTotalAmountVAT5 += (mnewamt - mvatamount5);
                                else if (mvatper == 13.5)
                                    mTotalAmountVAT12 += (mnewamt - mvatamount12point5);
                            }
                        }
                    }
                }
                NoofRows();




                txtdiscountAmount5.Text = mtotaldiscountamount5.ToString("#0.00");
                txtDiscountAmount12point5.Text = mtotaldiscountamount12point5.ToString("#0.00");
                
               // txtMyDiscountAmount5.Text = mtotalmyspecialdiscamt5.ToString("#0.00");
               // txtMyDiscountAmount12point5.Text = mtotalmyspecialdiscamt12point5.ToString("#0.00");

                txtVatInput5per.Text = mTvatamount5.ToString("#0.00");
                txtVatInput12point5per.Text = mTvatamount12point5.ToString("#0.00");
                txtAmountVAT12Point5Per.Text = mTotalAmountVAT12.ToString("#0.00");
                txtAmountVAT5Per.Text = mTotalAmountVAT5.ToString("#0.00");
                txtAmountforZeroVAT.Text = mTtotamtvat0.ToString("#0.00");

                double mdblDiscAmount = Math.Round(mtotaldiscountamount5 + mtotaldiscountamount12point5 + mtotaldiscountamountzero, 2);
                //double mdblMyDiscAmount = Math.Round(mtotalmyspecialdiscamt12point5 + mtotalmyspecialdiscamt5 + mtotalmyspecialdiscamtzero, 2);
                txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                mtotalafterdiscountwithoutmydiscount = mTotalAmount - mdblDiscAmount;
                txtTotalafterdiscount.Text = mtotalafterdiscountwithoutmydiscount.ToString("#0.00");

                txtAmount.Text = Math.Round(mTotalAmount, 2).ToString("#0.00");

                mtotamt = Math.Round(mtotalafterdiscountwithoutmydiscount + maddon + mdebitnote, 2);
                if (mcreditnote < mtotamt)
                    mtotamt = Math.Round(mtotamt - mcreditnote, 2);
                else
                {
                    txtCreditNote.Text = "0.00";
                    ClearDebitCreditNoteWhenAmountIsLess();
                }
                txtTotalAmount.Text = mtotamt.ToString("#0.00");



                CalculateRoundAmount();
                CalculateProfitPercent();

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateRoundAmount()
        {
            double mrndamt = 0;
            if (cbRound.Checked == true)
            {
                double mtotalafterdiscount = Convert.ToDouble(txtTotalAmount.Text.ToString());
                if (General.CurrentSetting.MsetSaleRoundingToPreviousRupee == "Y")
                {
                    mrndamt = Math.Floor(Math.Round(mtotalafterdiscount, 2)) - Math.Round(mtotalafterdiscount, 2);
                    txtRoundAmount.Text = mrndamt.ToString("#0.00");
                }
                else
                    txtRoundAmount.Text = Math.Round(Math.Round(mtotalafterdiscount, 0) - Math.Round(mtotalafterdiscount, 2), 2).ToString("#0.00");
                txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                txtNetAmount.Text = txtBillAmount.Text;
            }
            else
            {
                txtRoundAmount.Text = "0.00";
                txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                txtNetAmount.Text = txtBillAmount.Text;
            }

            double newpendingAmount = _SSSale.PendingAmount - _SSSale.PreAmountNet + Convert.ToDouble(txtNetAmount.Text.ToString());  // [ansuman]
            txtPendingBalance.Text = newpendingAmount.ToString("#0.00");
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
                UpdateTempDebtorSaleDt(); //kiran
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ClearDebitCreditNoteWhenAmountIsLess()
        {
            string mvoutype = "";
            try
            {
                foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
                {
                    string ch = string.Empty;
                    double mamt = 0;
                    if (crdbdr.Cells["Col_Check"].Value != null)
                    {
                        ch = crdbdr.Cells["Col_Check"].Value.ToString();
                        if (ch != string.Empty)
                        {
                            mvoutype = crdbdr.Cells["Col_VoucherType"].Value.ToString().Trim();
                            double.TryParse(crdbdr.Cells["Col_AmountNet"].Value.ToString().Trim(), out mamt);
                            if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                                crdbdr.Cells["Col_Check"].Value = string.Empty;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
                                            mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].ReadOnly = true;
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
                                            mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].ReadOnly = true;
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
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                mpPVC1.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                mpPVC1.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 110;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 80;
                //  column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 150;
                //       column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //13
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
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

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MySpecialDiscountAmount";
                column.DataPropertyName = "MySpecialDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFTempSale";
                //  column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RatePerUnit";
                // column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfMultipleMRP";
                //  column.DataPropertyName = "ProdIfSaleDisc";            
                column.Width = 60;
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch No.";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 45;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "COM";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "ShelfID";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl.Stk";
                column.Width = 60;
                column.ReadOnly = true;
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
                column.Name = "Col_MRP";
                column.DataPropertyName = "ProdLastPurchaseMRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
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

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GenericCategoryName";
                column.DataPropertyName = "GenericCategoryName";
                column.HeaderText = "GenericCategoryName";
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
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                column.Visible = false;
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
                column.Name = "Col_PurchaseDate";
                column.DataPropertyName = "LastPurchaseDate";
                column.HeaderText = "LastPurOn";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 85;
                mpPVC1.ColumnsBatchList.Add(column);

                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Cl.STK";
                column.Width = 65;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStockpack";
                column.DataPropertyName = "ClosingStockPack";
                column.HeaderText = "Cl.STK";
                column.Visible = false;
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                mpMSVCFill.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //5              
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl.tock";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Required.Qty";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
            dgCreditNote.Columns.Clear();
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
                dgCreditNote.Columns.Add(column);

                //DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                //columnCheck.Name = "Col_Check";
                //columnCheck.HeaderText = "Check";
                //columnCheck.Width = 50;
                //columnCheck.Visible = true;
                //dgCreditNote.ColumnsMain.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = " ";
                column.Width = 15;
                dgCreditNote.Columns.Add(column);

                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Series";
                column.Width = 50;
                column.ReadOnly = true;
                dgCreditNote.Columns.Add(column);

                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.ReadOnly = true;
                dgCreditNote.Columns.Add(column);
                //3 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.ReadOnly = true;
                column.Width = 50;
                dgCreditNote.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 80;
                column.ReadOnly = true;
                dgCreditNote.Columns.Add(column);
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
                dgCreditNote.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Narr";
                column.DataPropertyName = "Narration";
                column.HeaderText = "Narration";
                column.Width = 160;
                column.ReadOnly = true;
                dgCreditNote.Columns.Add(column);
                //8

                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountClear";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "AmountClear";
                column.Visible = false;
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCreditNote.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Acc";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "a1";
                column.Width = 50;
                column.Visible = false;
                dgCreditNote.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool BindCreditNoteDebitNote(DataTable dt)
        {
            bool retValue = true;
            double amt = 0;
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
                    currentdr.Cells["Col_VoucherSeries"].Value = dr["VoucherSeries"].ToString();
                    currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    if (dr["VoucherDate"] != DBNull.Value && dr["VoucherDate"].ToString() != "")
                    {
                        voudt = dr["VoucherDate"].ToString();
                        voudt = General.GetDateInShortDateFormat(voudt);
                    }
                    currentdr.Cells["Col_VoucherDate"].Value = voudt;
                    amt = Convert.ToDouble(dr["AmountNet"].ToString());
                    currentdr.Cells["Col_AmountNet"].Value = amt.ToString("#0.00");
                    currentdr.Cells["Col_Narr"].Value = dr["Narration"].ToString();
                    if (dr["AmountClear"] != DBNull.Value && dr["AmountClear"].ToString() != "")
                        double.TryParse(dr["AmountClear"].ToString(), out amtclear);
                    currentdr.Cells["Col_AmountClear"].Value = dr["AmountClear"].ToString();
                    if (_Mode == OperationMode.Delete)
                        currentdr.Cells["Col_Check"].Value = string.Empty;
                    else if (amtclear != 0)
                        currentdr.Cells["Col_Check"].Value = ((char)0x221A).ToString();
                    else
                        currentdr.Cells["Col_Check"].Value = string.Empty;

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
        private void dgCreditNote_KeyDown(object sender, KeyEventArgs e)
        {
            string ifchecked = string.Empty;
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgCreditNote.CurrentRow.Cells["Col_Check"].Value != null && dgCreditNote.CurrentRow.Cells["Col_Check"].Value.ToString() != string.Empty)
                        ifchecked = dgCreditNote.CurrentRow.Cells["Col_Check"].Value.ToString();
                    if (ifchecked != string.Empty)
                        dgCreditNote.CurrentRow.Cells["Col_Check"].Value = string.Empty;
                    else
                        dgCreditNote.CurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();

                    CalculateCRDBSelectedAmount();
                }
            }
        }
        private void CalculateCRDBSelectedAmount()
        {
            string mvoutype = "";
            double mcrnoteamt = 0;
            double mdbnoteamt = 0;
            try
            {
                foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
                {
                    string ch = string.Empty;
                    double mamt = 0;
                    if (crdbdr.Cells["Col_Check"].Value != null && crdbdr.Cells["Col_Check"].Value.ToString() != string.Empty)
                        ch = crdbdr.Cells["Col_Check"].Value.ToString();
                    if (ch != string.Empty)
                    {
                        mvoutype = crdbdr.Cells["Col_VoucherType"].Value.ToString().Trim();
                        double.TryParse(crdbdr.Cells["Col_AmountNet"].Value.ToString().Trim(), out mamt);
                        if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                            mcrnoteamt += mamt;
                        else if (mvoutype == FixAccounts.VoucherTypeForDebitNoteStock || mvoutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                            mdbnoteamt += mamt;
                    }
                }
                txtCRAmountSelected.Text = mcrnoteamt.ToString("#0.00");
                txtDNAmountSelected.Text = mdbnoteamt.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
            lblRightSideFooterMsg.Text = "";

            try
            {
                foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
                {
                    string ch = string.Empty;
                    double mamt = 0;
                    if (crdbdr.Cells["Col_Check"].Value != null && crdbdr.Cells["Col_Check"].Value.ToString() != string.Empty)
                        ch = crdbdr.Cells["Col_Check"].Value.ToString();
                    if (ch != string.Empty)
                    {
                        mvoutype = crdbdr.Cells["Col_VoucherType"].Value.ToString().Trim();
                        double.TryParse(crdbdr.Cells["Col_AmountNet"].Value.ToString().Trim(), out mamt);
                        if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                            mcrnoteamt += mamt;
                        else if (mvoutype == FixAccounts.VoucherTypeForDebitNoteStock || mvoutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                            mdbnoteamt += mamt;
                    }
                }
                txtCreditNote.Text = mcrnoteamt.ToString("#0.00");
                txtDebitNote.Text = mdbnoteamt.ToString("#0.00");
                pnlDebitCreditNote.Visible = false;
                CalculateAmount(-1);
                // CalculateFinalSummary();
                tsBtnSave.Enabled = true;
                //pnlSummary.BringToFront();
                //pnlSummary.Visible = true;
                //pnlSummary.Focus();
                //txtCRAmountS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.btnCRDBOK_Click>>" + Ex.Message);
            }
        }
        //private void ConstructCreditNoteColumns()
        //{
        //    dgCreditNote.ColumnsMain.Clear();
        //    DataGridViewTextBoxColumn column;
        //    //0
        //    try
        //    {
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_CrdbID";
        //        column.DataPropertyName = "CRDBID";
        //        column.HeaderText = "VouSeries";
        //        column.Width = 50;
        //        column.Visible = false;
        //        column.ReadOnly = true;
        //        dgCreditNote.ColumnsMain.Add(column);

        //        DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
        //        columnCheck.Name = "Col_Check";
        //        columnCheck.HeaderText = "Check";
        //        columnCheck.Width = 50;
        //        columnCheck.Visible = true;
        //        dgCreditNote.ColumnsMain.Add(columnCheck);

        //        //1
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherSeries";
        //        column.DataPropertyName = "VoucherSeries";
        //        column.HeaderText = "VouSeries";
        //        column.Width = 50;
        //        column.ReadOnly = true;
        //        dgCreditNote.ColumnsMain.Add(column);

        //        //2
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherType";
        //        column.DataPropertyName = "VoucherType";
        //        column.HeaderText = "VouType";
        //        column.Width = 50;
        //        column.ReadOnly = true;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //3 
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherNumber";
        //        column.DataPropertyName = "VoucherNumber";
        //        column.HeaderText = "VouNumber";
        //        column.ReadOnly = true;
        //        column.Width = 50;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //4
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_VoucherDate";
        //        column.DataPropertyName = "VoucherDate";
        //        column.HeaderText = "VoucherDate";
        //        column.Width = 80;
        //        column.ReadOnly = true;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //5
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_AmountNet";
        //        column.DataPropertyName = "AmountNet";
        //        column.HeaderText = "AmountNet";
        //        column.Width = 80;
        //        column.ReadOnly = true;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //10
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Narr";
        //        column.DataPropertyName = "Narration";
        //        column.HeaderText = "Narration";
        //        column.Width = 160;
        //        column.ReadOnly = true;
        //        dgCreditNote.ColumnsMain.Add(column);

        //        //6
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_AmountClear";
        //        //column.DataPropertyName = "AmountClear";
        //        column.HeaderText = "AmountBalance";
        //        column.Visible = false;
        //        column.Width = 80;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //7
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_AmountClear";
        //        column.DataPropertyName = "AmountClear";
        //        column.HeaderText = "AmountClear";
        //        column.Visible = false;
        //        column.Width = 80;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //        dgCreditNote.ColumnsMain.Add(column);
        //        //9
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Acc";
        //        column.DataPropertyName = "AccountID";
        //        column.HeaderText = "a1";
        //        column.Width = 50;
        //        column.Visible = false;
        //        dgCreditNote.ColumnsMain.Add(column);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}

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
                {
                    _SSSale.AccountID = mcbCreditor.SelectedID;
                    GetPreviousSale();
                }
                btnPreviousSale.Enabled = true;
                btnPreviousSale.Text = _SSSale.TotalPreviousSale.ToString("#0.00");
                SetDebtorDeatils();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void SetDebtorDeatils()
        {
            try
            {
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
                    txtPatient.SelectedID = "";
                    cbFill.Enabled = true;
                    cbTransferSale.Enabled = true;
                    FillCreditDebitNote();
                    _SSSale.CrdbName = mcbCreditor.SeletedItem.ItemData[2];
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    txtPatient.Text = mcbCreditor.SeletedItem.ItemData[2];

                    DataRow[] dr = (txtPatient.DataSource).Select("PatientName = '" + txtPatient.Text + "'");
                    if (dr.Length > 0 && dr[0] != null)
                    {
                        txtPatient.SelectedID = Convert.ToString(dr[0]["PatientID"]);
                    }

                    txtPatientAddress.Text = txtAddress1.Text.ToString();
                    string MobileNumber = Convert.ToString(mcbCreditor.SeletedItem.ItemData[10]);
                    if (string.IsNullOrEmpty(MobileNumber) == false)
                        txtMobileNumber.Text = MobileNumber;

                    txtDiscPercent.Text = mcbCreditor.SeletedItem.ItemData[9];
                    if (mcbCreditor.SeletedItem.ItemData[6] != "")
                        _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    if (_Mode == OperationMode.Add)
                    {
                        _SSSale.DocID = mcbCreditor.SeletedItem.ItemData[7];
                        mcbDoctor.SelectedID = _SSSale.DocID;
                    }
                    _SSSale.TransactionType = mcbCreditor.SeletedItem.ItemData[8];

                    if (string.IsNullOrEmpty(Convert.ToString(mcbCreditor.SeletedItem.ItemData[6])) == false)
                        _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[6]);
                    else
                        _SSSale.TokenNumber = 0;

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
            { Log.WriteException(Ex); }
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
                else if (e.KeyCode == Keys.Tab)
                {
                    mcbPrescription.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpPVC1_OnProductSelected(DataGridViewRow productRow)
        {
            _SSSale.IFMultipleMRP = "N";
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
                if (productRow.Cells["Col_ProdScheduleDrugCode"].Value != null)
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value = productRow.Cells["Col_ProdScheduleDrugCode"].Value;
                // mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value = productRow.Cells["Col_ProdScheduleDrugCode"].Value;
                if (_Mode == OperationMode.Add)
                {
                    if (productRow.Cells["Col_LastStockID"].Value != null)
                        mlastsalestockid = productRow.Cells["Col_LastStockID"].Value.ToString();
                }
                else
                    mlastsalestockid = mprodno;
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value.ToString() == "H1")
                    mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = Color.Tomato;

                mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value = productRow.Cells["Col_ClosingStock"].Value;

                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = !cbEditRate.Checked;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;
                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);
                else
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = 0;
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
                    bool iffilllastsale = false;
                    try
                    {
                        if (mprodno != "")
                            iffilllastsale = FillLastSaleDataFromMasterProduct();
                    }
                    catch (Exception ex) { Log.WriteError(ex.ToString()); }
                    if (iffilllastsale)
                    {
                        if (cbEditRate.Checked)
                        {
                            mpPVC1.SetFocus(10);
                            mpPVC1.IsFocusSameCell = true;
                        }
                        else
                            mpPVC1.SetFocus(11);
                    }
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
                string mbatchno = "";
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
                    double.TryParse(batchRow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                    double.TryParse(batchRow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                    double.TryParse(batchRow.Cells["Col_TradeRate"].Value.ToString().Trim(), out mtraderate);
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

                    mbatchno = batchRow.Cells["Col_Batchno"].Value.ToString().Trim();

                    _SSSale.IFMultipleMRP = _SSSale.IfmultipleMRP(mprodno, mbatchno, mmrpn);

                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].ReadOnly = true;
                    string mdt = DateTime.Today.Date.ToString("yyyyMMdd");

                    if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                    {
                        lblFooterMessage.Text = "Expired Product";
                        PSMessageBox.Show("Product Expired", "Invalid expiry date", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
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
                        lblRightSideFooterMsg.Text = "";
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


                        //mclstk = mclstk + tempproductstock - totproductsale;

                        //mclosingstock = mclosingstock + tempbatchstock - totbatchsale;



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
                                mpPVC1.SetFocus(10);
                                mpPVC1.IsFocusSameCell = true;
                            }
                            else
                            {
                                mpPVC1.SetFocus(11);
                            }
                        }
                    }
                    setRightFoooterMessage(batchRow);
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
                //  RefreshProductGrid();
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

                Product prod = new Product();
                DataTable dtable = prod.GetOverviewData();
                mpPVC1.DataSourceProductList = dtable;
                // mpPVC1.DataSourceProductList = General.ProductList;
                mpPVC1.DataSourceProductList = dtable;
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
                {
                    mpPVC1.Rows.Add();
                    mpPVC1.MainDataGridCurrentRow.Cells[1].ReadOnly = false; // sheela
                }
                if (mpPVC1.Rows.Count == 1)
                    mpPVC1.SetFocus(1);
                else
                    mpPVC1.SetFocus(mpPVC1.MainDataGridCurrentRow.Index + 1, 1);
                UpdateTempDebtorSaleDt(); //kiran
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value)) == false)
            {
                mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                mpPVC1.SetForwardCellIndex();
            }
            else
            {
                txtNarration.Focus();
                if (General.CurrentSetting.MsetSaleShowProfitInSaleBill == "Y")
                {
                    lblProfit.Visible = txtProfit.Visible = true;
                    txtProfit.Text = _SSSale.TotalProfitInRupees.ToString("#0.00");
                }
            }
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
            else if (_Mode == OperationMode.Edit)
                lblFooterMessage.Text = "No Stock";
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
            // string prodname = "";
            string mexpirydate = "";
            try
            {
                if (colIndex == 1)
                {     //To check for enter key
                    if (mpPVC1.Rows.Count > 1 && string.IsNullOrEmpty(Convert.ToString(mpPVC1.MainDataGridCurrentRow.Cells[0].Value)) == true)
                    {
                        mpPVC1.ClearSelection();
                        //  mpPVC1_OnTABKeyPressed();
                    }
                    //_PreCurrentQuantity = 0;
                    //if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    //    _PreCurrentQuantity = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                    //_preID = "";
                    //if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                    //    _preID = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                    //if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value != null)
                    //    prodname = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value.ToString();
                    //if (prodname != "" && _preID != "")
                    //{
                    //    prodname = General.GetProductName(_preID);
                    //    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = prodname;
                    //}
                }
                if (colIndex == 11) //Quantity
                {
                    //kiran
                    int i;
                    //bool IsSplitinMultipleBatch = false;
                    if (!int.TryParse(Convert.ToString(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value), out i))
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = null;
                    }

                    SsStock stk = new SsStock();
                    DataTable dtCurrStk = stk.GetStockByStockIDForDBCRNote(mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString());

                    foreach (DataRow drCurrStk in dtCurrStk.Rows)
                    {
                        if (drCurrStk["ClosingStock"] != null)
                            _SSSale.CurrentBatchStock = Convert.ToInt32(drCurrStk["ClosingStock"]);
                    }

                    // 10/1/2017
                    foreach (DataGridViewRow dr in dgtemp.Rows)
                    {
                        if (dr.Cells["Temp_StockID"].Value != null)
                        {
                            if (dr.Cells["Temp_StockID"].Value.ToString() == mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString())
                                _SSSale.CurrentBatchStock += Convert.ToInt32(dr.Cells["Temp_Quantity"].Value.ToString());
                        }
                    }
                    lblFooterMessage.Text = "";
                    lblRightSideFooterMsg.Text = "";

                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value == null || mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString() == string.Empty)//Pravin: COpied from counter sale// || Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString()) == 0)
                        mpPVC1.IsAllowNewRow = false;
                    else
                    {
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString() == "0")
                        {
                            int minq = 0;
                            _SSSale.ProdPakn = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
                            minq = Math.Min(_SSSale.CurrentBatchStock, _SSSale.ProdPakn);
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = minq.ToString("#0");
                        }
                        if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) <= _SSSale.CurrentBatchStock)
                            mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.ForeColor = Color.Black;

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
                                PSMessageBox.Show("Product Expired", "Invalid expiry date", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = 0;
                                mpPVC1.IsAllowNewRow = false;
                                mpPVC1.SetFocus(11);
                            }
                            else
                            {
                                //here
                                int activegridbatchstock = 0;
                                requiredQty = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                                if (_PreCurrentQuantity == 0)
                                    _PreCurrentQuantity = requiredQty;
                                string stkdtstockid = mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString();
                                foreach (DataGridViewRow dr in mpPVC1.Rows)
                                {
                                    if (dr.Cells["Col_StockID"].Value != null)
                                    {
                                        string activegridrowstockid = dr.Cells["Col_StockID"].Value.ToString();
                                        if (stkdtstockid == activegridrowstockid && (dr.Index != mpPVC1.MainDataGridCurrentRow.Index || _Mode == OperationMode.Edit))
                                        {
                                            activegridbatchstock = Convert.ToInt32(dr.Cells["Col_Quantity"].Value.ToString());
                                            break;
                                        }
                                    }
                                }
                                if ((_SSSale.CurrentBatchStock - activegridbatchstock) >= 0 && requiredQty > _SSSale.CurrentProductStock && General.CurrentSetting.MsetSaleAllowNegativeStock == "N" && requiredQty == 0)
                                {
                                    requiredQty = _SSSale.CurrentBatchStock;
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = requiredQty.ToString("#0");
                                }
                                if (requiredQty <= 0 || _SSSale.CurrentBatchStock == 0)
                                {
                                    //here
                                    if (requiredQty <= 0)
                                    {
                                        lblFooterMessage.Text = "Enter Quantity";
                                        mpPVC1.SetFocus(11);
                                        mpPVC1.IsAllowNewRow = false;
                                    }
                                    else if (General.CurrentSetting.MsetSaleAllowNegativeStock == "N")
                                    {
                                        lblFooterMessage.Text = "Batch Stock Zero";
                                        mpPVC1.SetFocus(11);
                                        mpPVC1.IsAllowNewRow = false;
                                    }
                                    else
                                    {
                                        int mbatchstock = 0;
                                        int oldQuantity = 0;
                                        string mstockid = "";
                                        int custno = 0;
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
                                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value != null)
                                            int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_CustID"].Value.ToString(), out custno);

                                        lblFooterMessage.Text = "";
                                        lblRightSideFooterMsg.Text = "";


                                        FillBatchStock(ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty);
                                        mpPVC1.IsAllowNewRow = true;
                                    }
                                }
                                else if (requiredQty > _SSSale.CurrentBatchStock && _Mode == OperationMode.Edit)
                                {

                                    lblFooterMessage.Text = "Enter Correct Quantity";
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;
                                    mpPVC1.SetFocus(11);
                                    mpPVC1.IsAllowNewRow = false;
                                    CalculateAmount(-1);
                                }
                                else
                                {

                                    int currindex = mpPVC1.MainDataGridCurrentRow.Index;
                                    if (requiredQty <= _SSSale.CurrentBatchStock || (mpPVC1.Rows.Count == currindex + 1) || (mpPVC1.Rows.Count > currindex + 1 && mpPVC1.Rows[currindex + 1].Cells["Col_ProductID"].Value == null))
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
                                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null)
                                            mrate = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value);

                                        lblFooterMessage.Text = "";
                                        lblRightSideFooterMsg.Text = "";

                                        if ((requiredQty <= _SSSale.CurrentBatchStock) || (requiredQty > _SSSale.CurrentBatchStock && General.CurrentSetting.MsetSaleAllowNegativeStock == "Y"))
                                        {
                                            FillBatchStock(ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty);
                                            int numberofLines = General.GetNumberofLinesInGrid(mpPVC1);
                                            if (General.CurrentSetting.MsetPrintFixNumberOfLines == "Y")
                                            {
                                                if (numberofLines < General.CurrentSetting.MsetNumberOfLinesSaleBill)
                                                {
                                                    mpPVC1.IsAllowNewRow = true;
                                                }
                                                else
                                                {
                                                    mpPVC1.IsAllowNewRow = false;
                                                }
                                            }
                                            else
                                            {
                                                mpPVC1.IsAllowNewRow = true;
                                            }
                                        }

                                        else
                                        {

                                            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                                mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();

                                            //pravin
                                            bool checkCounterDt = false;
                                            checkCounterDt = RemovePreviousProductInMainGrid(ref requiredQty, mprodno, mbtno, Convert.ToString(mrate), mprodno);



                                            FillMainGridwithMultipleBatch(requiredQty, mprodno, checkCounterDt, mbtno);
                                            CalculateAmount(-1);
                                            if (_Mode == OperationMode.Add)
                                            {
                                                WriteToXML();
                                            }

                                        }
                                    }
                                    else
                                    {
                                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;//.ToString("#0");
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

                    _PreCurrentQuantity = 0;

                    //pravin
                    UpdateTempDebtorSaleDt();
                    if (mpPVC1.Rows.Count > 0)
                    {
                        if (mpPVC1.Rows[0].Cells["Col_ProductID"].Value == null)
                        {
                            mpPVC1.Rows.RemoveAt(0);
                        }
                    }
                }
                if (colIndex == 10)
                {
                    decimal i;
                    if (!Decimal.TryParse(Convert.ToString(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value), out i))
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = null;
                        mpPVC1.IsFocusSameCell = true;
                    }
                    else
                    {

                        mpPVC1.IsFocusSameCell = false;
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = String.Format("{0:0.00}", Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value)).ToString();
                        string newexp = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString();
                        newexp = General.GetValidExpiry(newexp);
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexp.ToString();
                        string edate = General.GetValidExpiryDate(newexp);
                        edate = General.GetExpiryInyyyymmddForm(edate);
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = edate.ToString();



                        if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value) <= 0)
                        {
                            lblFooterMessage.Text = "Enter SaleRate";
                            mpPVC1.SetFocus(10);
                            mpPVC1.IsAllowNewRow = false;
                        }
                        else if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) > 0)
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = String.Format("{0:0.00}", Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value)).ToString();
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value = (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value) * Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value)).ToString();
                            CalculateAmount(-1);
                            mpPVC1.IsAllowNewRow = true;
                        }
                    }
                }

                if (colIndex == 7)
                {
                    string newexpiry = "";
                    string newexpirydate = "";
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                    {
                        int explength = 0;
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                            explength = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim().Length;
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim() != "" && explength > 0)
                        {
                            newexpiry = General.GetValidExpiry(mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString());
                            if (newexpiry != "")
                            {
                                bool ifexp = CheckValidExpiry(newexpiry);
                                //   bool ifexp = true;
                                if (ifexp)
                                {
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry.ToString();
                                    newexpirydate = General.GetValidExpiryDate(newexpiry);
                                    newexpirydate = General.GetExpiryInyyyymmddForm(newexpirydate);
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                                    lblFooterMessage.Text = "";
                                    lblRightSideFooterMsg.Text = "";
                                }
                                else
                                {
                                    lblFooterMessage.Text = "Check Expiry ";
                                    mpPVC1.SetFocus(5);
                                }
                            }
                            else
                            {
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                                lblFooterMessage.Text = " No Expiry ";
                                mpPVC1.SetFocus(5);
                            }

                        }
                        else
                        {
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                            lblFooterMessage.Text = " No Expiry ";
                            mpPVC1.SetFocus(5);
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void FillMainGridwithMultipleBatch(int requiredqty, string productID, bool checkCounterDt, string batchId)
        {

            int mmaingridrowIndex = 0;
            DataTable stkdt = new DataTable();
            Stock prodstk = new Stock();
            int mycolindex = 0;
            int msalestk = requiredqty;
            int mactualclosingstock = 0;

            if (mpPVC1.Rows.Count > 0)
                mmaingridrowIndex = mpPVC1.MainDataGridCurrentRow.Index;

            stkdt = prodstk.GetStockByProductIDForSale(productID);
            stkdt = SortByBatch(stkdt, batchId);


            int counterProductStk = 0;
            DataTable dtTempCounterSale = CacheObject.Get<DataTable>("TempCounterSale");
            if (dtTempCounterSale != null)
            {
                if (dtTempCounterSale.Rows.Count > 0)
                {
                    foreach (DataRow drTempCounter in dtTempCounterSale.Rows)
                    {
                        if (drTempCounter["ProductID"].ToString() == productID)
                        {
                            counterProductStk += Convert.ToInt32(drTempCounter["QTY"]);
                        }
                    }
                }
            }

            foreach (DataRow dtrow in stkdt.Rows)
            {
                if (dtrow["ClosingStock"] != DBNull.Value)
                    if (Convert.ToInt32(dtrow["ClosingStock"].ToString()) != 0)
                        mactualclosingstock += Convert.ToInt32(dtrow["ClosingStock"].ToString());
            }
            mactualclosingstock -= counterProductStk;

            try
            {
                //ss here
                foreach (DataRow dtrow in stkdt.Rows)
                {
                    int mbatchstock = 0;
                    int mactualsalestock = 0;
                    double msalerate = 0;
                    // here
                    string stkdtstockid = dtrow["StockID"].ToString();
                    //here
                    //  int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock);//pravin
                    //pravin
                    if (dtTempCounterSale != null)
                    {
                        if (dtTempCounterSale.Rows.Count > 0 && checkCounterDt)
                        {
                            foreach (DataRow drTempCounter in dtTempCounterSale.Rows)
                            {
                                if (drTempCounter["ProductID"].ToString() == productID && drTempCounter["BatchID"].ToString() == dtrow["BatchNumber"].ToString() && drTempCounter["SRate"].ToString() == dtrow["SaleRate"].ToString())
                                {
                                    //dtrow["ClosingStock"] = Convert.ToInt32(dtrow["ClosingStock"]) - Convert.ToInt32(drTempCounter["QTY"]);
                                    mbatchstock = Convert.ToInt32(dtrow["ClosingStock"]) - Convert.ToInt32(drTempCounter["QTY"]);
                                }
                                else
                                    mbatchstock = Convert.ToInt32(dtrow["ClosingStock"]);
                            }
                        }
                        else
                            int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock);
                    }
                    else
                        int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock);


                    mactualsalestock = Math.Min(mbatchstock, msalestk);

                    if (mactualsalestock > 0 && msalestk > 0 && mactualclosingstock > 0)
                    {
                        string mbtno = "";
                        double mmrp = 0;
                        int muom = 1;
                        double rateperunit = 0;

                        mbtno = dtrow["BatchNumber"].ToString();
                        double.TryParse(dtrow["MRP"].ToString(), out mmrp);
                        mycolindex = 0;


                        mycolindex = mmaingridrowIndex;

                        mpPVC1.Rows[mycolindex].Cells["Col_ProductID"].Value = dtrow["ProductID"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_ProductName"].Value = dtrow["ProdName"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_UOM"].Value = dtrow["ProdLoosePack"].ToString();
                        int.TryParse(dtrow["ProdLoosePack"].ToString(), out muom);

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
                        mamt = Math.Round((msalerate / muom) * mactualsalestock, 2);
                        rateperunit = Math.Round((msalerate / muom), 2);
                        mpPVC1.Rows[mycolindex].Cells["Col_Amount"].Value = Convert.ToDouble(mamt.ToString()).ToString("#0.00");
                        mpPVC1.Rows[mycolindex].Cells["Col_RatePerUnit"].Value = rateperunit.ToString("#0.00");
                        mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = mpPVC1.Rows[mycolindex].Cells["Col_Quantity"].Value;
                        mpPVC1.Rows[mycolindex].Cells["Col_ClosingStock"].Value = dtrow["ProdClosingStock"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_PurchaseRate"].Value = dtrow["PurchaseRate"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_ExpiryDate"].Value = dtrow["ExpiryDate"].ToString();
                        // mpPVC1.Rows[mycolindex].Cells["Old_Quantity"].Value = 0;
                        mpPVC1.Rows[mycolindex].Cells["Col_VATPer"].Value = Convert.ToDouble(dtrow["ProdVATPercent"].ToString()).ToString("#0.00");
                        mpPVC1.Rows[mycolindex].Cells["Col_TradeRate"].Value = dtrow["TradeRate"].ToString();
                        mpPVC1.Rows[mycolindex].Cells["Col_StockID"].Value = dtrow["StockID"].ToString();
                        int mclstkdr1 = 0;
                        int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
                        mpPVC1.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value = dtrow["ProdScheduleDrugCode"].ToString();
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdScheduleDrugCode"].Value.ToString() == "H1")
                            mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = Color.Tomato;
                        else
                            ResetGridColour();
                        if (mactualclosingstock < msalestk)
                            mpPVC1.Rows[mycolindex].DefaultCellStyle.ForeColor = Color.DarkViolet; // kiran


                        msalestk = msalestk - mactualsalestock;
                        mactualclosingstock -= mactualsalestock;
                        CalculateAmount(-1);
                        if (msalestk > 0 && mactualclosingstock > 0)
                        {
                            //Pravin
                            int CurRowIndex = mpPVC1.MainDataGridCurrentRow.Index + 1;
                            mpPVC1.MainDataGridCurrentRow.ReadOnly = true;
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                            if (cbEditRate.Checked == true)
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;

                            mpPVC1.Rows.Add();
                            mpPVC1.SetFocus(mpPVC1.Rows.Count - 1, 11);
                            mmaingridrowIndex = mmaingridrowIndex + 1;
                        }
                    }
                }
                // here
                if (!General.CheckForBlankRowInTheGrid(mpPVC1))
                    mpPVC1.IsAllowNewRow = true;
                else
                {
                    foreach (DataGridViewRow dr in mpPVC1.Rows)
                    {
                        if (dr.Cells["Col_ProductID"].Value == null || dr.Cells["Col_ProductID"].Value.ToString() == string.Empty)
                        {
                            mpPVC1.Rows.Remove(dr);
                            break;
                        }

                    }
                    RemoveBlankRowsInActiveGrid();
                    mpPVC1.IsAllowNewRow = true;
                    UpdateTempDebtorSaleDt();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ResetGridColour()
        {
            //if (mpPVC1.MainDataGridCurrentRow.Cells[0].Value != null)
            //{
            //    mpPVC1.MainDataGridCurrentRow.DefaultCellStyle.BackColor = btn1.BackColor;
            //}
        }
        private DataTable SortByBatch(DataTable dtStk, string batchId)
        {

            List<DataRow> stkLists = dtStk.AsEnumerable().ToList();
            var stkList = stkLists;
            var matchList = stkList.Where(m => m["BatchNumber"].ToString().StartsWith(batchId)).ToList();
            var FinalList = matchList.Concat(stkList.Except(matchList).ToList());
            dtStk = FinalList.CopyToDataTable();
            return dtStk;
        }

        private bool RemovePreviousProductInMainGrid(ref int reqQty, string productId, string batchId, string saleRate, string prodId)
        {
            bool checkCounterDt = false;
            //Check productStocck
            Stock prodstk = new Stock();
            DataTable stkdt = new DataTable();
            stkdt = prodstk.GetStockByProductIDForSale(prodId);



            int totalCounterProduct = 0;
            foreach (DataGridViewRow row in mpPVC1.Rows)
            {
                if (Convert.ToString(row.Cells["Col_ProductID"].Value) == productId)
                {
                    totalCounterProduct += Convert.ToInt32(row.Cells["Col_Quantity"].Value);
                }
            }


            int totalStock = 0;
            foreach (DataRow dtrow in stkdt.Rows)
            {
                if (dtrow["ClosingStock"] != DBNull.Value)
                    totalStock += Convert.ToInt32(dtrow["ClosingStock"].ToString());
            }

            bool addedNewRow = false;
            List<int> rowIdx = new List<int>();

            int currentRowIdx = mpPVC1.Rows.Count;
            //if reqired qty is > total stock remove all productin the batch (ignoring price)
            //if (reqQty > totalStock)
            if (totalCounterProduct >= totalStock)
            {
                reqQty = totalCounterProduct;
                checkCounterDt = false; // we'll NOT check items in the counter sale datatable while merging
                foreach (DataGridViewRow row in mpPVC1.Rows)
                {
                    if ((Convert.ToString(row.Cells["Col_ProductID"].Value) == productId)) // && (Convert.ToString(row.Cells["Col_BatchNumber"].Value) == batchId))
                    {
                        rowIdx.Add(row.Index);
                    }
                }
            }
            else
            {
                checkCounterDt = true; // we'll check items in the counter sale while merging
                foreach (DataGridViewRow row in mpPVC1.Rows)
                {
                    if ((Convert.ToString(row.Cells["Col_ProductID"].Value) == productId) && (Convert.ToString(row.Cells["Col_BatchNumber"].Value) == batchId) && (Convert.ToString(double.Parse(row.Cells["Col_SaleRate"].Value.ToString())) == saleRate))
                    {

                        rowIdx.Add(row.Index);
                    }
                }
            }
            int deletedRows = 0;
            foreach (int rowId in rowIdx)
            {
                if (!addedNewRow)
                    mpPVC1.Rows.RemoveAt(rowId);
                else
                    mpPVC1.Rows.RemoveAt(rowId - deletedRows);

                deletedRows++;

                UpdateTempDebtorSaleDt();

                if (!addedNewRow)
                {

                    // ActiveDataGrid.Rows.Add();


                    mpPVC1.addNewBlankRow();
                    mpPVC1.IsAllowNewRow = true;
                    addedNewRow = true;
                    //for (int i = 0; i < rowIdx.Count; i++)
                    //{
                    //    rowIdx[i] = rowIdx[i] - 1;
                    //}
                    //ActiveDataGrid.MainDataGridCurrentRow.Selected = true;
                    //ActiveDataGrid.MainDataGridCurrentRow[rowId].sele

                    //if (currentRowIdx <= 0)
                    //    ActiveDataGrid.Rows[0].Selected = true;
                    //else
                    //    ActiveDataGrid.Rows[currentRowIdx - 1].Selected = true;
                }
            }

            return checkCounterDt;
        }

        private bool CheckValidExpiry(string newexp)
        {
            bool ifexp = false;
            string exp = "";
            //    string expdate = "";
            try
            {
                if (newexp == "0000")
                    newexp = "00/00";
                if (newexp != "00/00")
                {
                    exp = General.GetValidExpiry(newexp);

                    if (exp == "")
                    {
                        lblFooterMessage.Text = "Please Check Expiry";
                        ifexp = false;
                    }
                    else
                    {
                        ifexp = true;

                        //expdate = General.GetValidExpiryDate(exp);
                        //string mexpdate = General.GetExpiryInyyyymmddForm(expdate);
                        //DateTime dd = General.ConvertStringToDateyyyyMMdd(mexpdate);
                        //TimeSpan tt = dd.Subtract(DateTime.Now.Date);
                        //int days = tt.Days;
                    }

                }
                else
                {
                    if (General.CurrentSetting.MsetGeneralExpiryDateReuired != "Y")
                    {
                        ifexp = true;
                        lblFooterMessage.Text = "";
                        lblRightSideFooterMsg.Text = "";

                    }
                    else
                    {
                        ifexp = false;
                        lblFooterMessage.Text = "Please Check Expiry";

                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return ifexp;
        }
        private void RemoveBlankRowsInActiveGrid()
        {
            if (mpPVC1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in mpPVC1.Rows)
                {
                    if (row.Cells["Col_ProductID"].Value == null)
                    {
                        mpPVC1.Rows.RemoveAt(row.Index);
                    }
                }
            }
        }
        private void WriteToXML()
        {
            DataTable dt = mpPVC1.GetGridData();
            if (dt.Rows.Count > 0)
                dt.WriteXml(General.GetDebtorSaleTempFile());
        }
        private void FillRatePerunit()
        {
            double mrate = 0;
            double mrateperunit = 0;
            double uom = 0;
            foreach (DataGridViewRow dr in mpPVC1.Rows)
            {
                if (dr.Cells["Col_SaleRate"].Value != null && dr.Cells["Col_SaleRate"].Value.ToString() != string.Empty)
                    mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                if (dr.Cells["Col_UOM"].Value != null && dr.Cells["Col_UOM"].Value.ToString() != string.Empty)
                    uom = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                mrateperunit = Math.Round((mrate / uom), 2);
                dr.Cells["Col_RatePerUnit"].Value = mrateperunit.ToString("#0.00");


            }
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
                    if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno)
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_IfMultipleMRP"].Value = _SSSale.IFMultipleMRP;
                    if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
                          drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
                             drp.Cells["Col_MRP"].Value.ToString().Trim() == mmrp.ToString("#0.00") && drp.Index != mcurrentindex)
                    {
                        if (drp.Cells["Col_Quantity"].Value != null)
                            oldmqty = Convert.ToInt32(drp.Cells["Col_Quantity"].Value.ToString());
                        oldmqty += mqty;

                        if (oldmqty > _SSSale.CurrentBatchStock)
                            drp.Cells["Col_Quantity"].Value = _SSSale.CurrentBatchStock;
                        else
                            drp.Cells["Col_Quantity"].Value = oldmqty;

                        mpPVC1.Rows.Remove(mpPVC1.MainDataGridCurrentRow);
                        break;
                    }
                }
            }

            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value != null)
                mpakn = Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null)
                double.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString(), out mrate);
            mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value = ((mrate / mpakn) * mqty).ToString("#0.00");
            mpPVC1.MainDataGridCurrentRow.Cells["Col_RatePerUnit"].Value = Math.Round((mrate / mpakn), 3).ToString("#0.00");
            CalculateAmount(-1);
        }

        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateAmount(-1);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtAddOn_TextChanged(object sender, EventArgs e)
        {

        }

        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {

        }
        private void txtAddOn_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    CalculateAmount(-1);
                    if (General.CurrentSetting.MsetSaleAskRoundinginSale == "Y")
                    {
                        cbRound.Focus();
                        cbRound.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        MainToolStrip.Select();
                        tsBtnSave.Select();
                    }
                    break;
                case Keys.Up:
                    txtDiscPercent.Focus();
                    break;
            }
        }

        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            double mdiscper = 0;

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (txtDiscPercent.Text != null && txtDiscPercent.Text.ToString() != string.Empty)
                            mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());
                        if (mdiscper <= General.CurrentSetting.MsetSaleMaxDiscount)
                        {
                            txtDiscAmount.Text = "0.00";
                            CalculateAmount(-1);
                            txtAddOn.Focus();
                        }
                        else
                        {
                            lblFooterMessage.Text = string.Concat("Max Discount Percent is ", General.CurrentSetting.MsetSaleMaxDiscount.ToString());
                            txtDiscPercent.Text = "0.00";
                        }
                        if (General.CurrentSetting.MsetSaleShowProfitInSaleBill == "Y")
                        {
                            txtProfit.Text = _SSSale.TotalProfitInRupees.ToString("#0.00");
                        }
                        break;
                    case Keys.Down:
                        if (mdiscper <= General.CurrentSetting.MsetSaleMaxDiscount)
                        {
                            txtDiscAmount.Text = "0.00";
                            CalculateAmount(-1);
                            txtAddOn.Focus();
                        }
                        else
                        {
                            lblFooterMessage.Text = string.Concat("Max Discount Percent is ", General.CurrentSetting.MsetSaleMaxDiscount.ToString());
                            txtDiscPercent.Text = "0.00";
                        }
                        break;
                    case Keys.Up:
                        txtNextVisitDays.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
                        DataTable dtable = prod.GetOverviewData();

                        mpMSVCFill.DataSource = dtable;

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
                    voutype = FixAccounts.VoucherTypeForCashSale;
                else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                    voutype = FixAccounts.VoucherTypeForCreditStatementSale;
                else
                    voutype = FixAccounts.VoucherTypeForCreditSale;


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
                    pnlDebitCreditNote.Width = 600;
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
            try
            {
                if (mcbDoctor.SeletedItem != null && string.IsNullOrEmpty(Convert.ToString(mcbDoctor.SelectedID)) == false)
                {
                    if (mcbDoctor.SeletedItem.ItemData[2] != null)
                        txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2].ToString();
                }
                if (mpPVC1.Rows.Count > 0)
                    mpPVC1.SetFocus(mpPVC1.Rows.Count - 1, 1);
                //else
                //   mcbDoctor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclDebtorSale.mcbDoctor_EnterKeyPressed>>" + Ex.Message);
            }
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
            mpPVC1.SetFocus(mpPVC1.Rows.Count - 1, 1);
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
                        pnlDebtorProduct.BringToFront();
                        pnlDebtorProduct.Visible = true;
                        pnlDebtorProduct.Width = 614;
                        pnlDebtorProduct.Height = 325;

                        mpMSVCFill.Visible = true;
                        mpMSVCFill.Dock = DockStyle.Fill;
                        ConstructmpMSVC1Columns();

                        mpMSVCFill.DataSourceMain = dt;

                        Product prod = new Product();
                        DataTable dtable = prod.GetOverviewData();

                        mpMSVCFill.DataSource = dtable;

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

                if (mcbCreditor.SeletedItem != null && mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    cbFill.Enabled = true;
                    txtAddress1.ReadOnly = true;
                    txtAddress2.ReadOnly = true;
                    GetPreviousSale();
                    btnPreviousSale.Enabled = true;
                    btnPreviousSale.Text = _SSSale.TotalPreviousSale.ToString("#0.00");
                    SetDebtorDeatils();
                    string MobileNumber = Convert.ToString(mcbCreditor.SeletedItem.ItemData[10]);
                    if (string.IsNullOrEmpty(MobileNumber) == false)
                        txtMobileNumber.Text = MobileNumber;

                    txtMobileNumber.Focus();
                    _SSSale.AccountID = mcbCreditor.SelectedID;
                }
                else
                {
                    _SSSale.AccountID = "";
                    cbFill.Enabled = false;
                    txtAddress1.ReadOnly = false;
                    txtAddress2.ReadOnly = false;
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtMobileNumber.Text = "";
                    btnPreviousSale.Text = "0.00";
                    txtAddress1.Focus();
                    txtTokenNumber.Text = "0";
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }


        }
        private Point GetPreviousSaleLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = btnPreviousSale.Location.X;
                pt.Y = btnPreviousSale.Location.Y + 30;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private void GetPreviousSale()
        {
            _BindingSourcePreviousSale = null;
            _BindingSourcePreviousSale = _SSSale.GetPreviousSale(mcbCreditor.SelectedID);
        }
        private void txtMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(mcbCreditor.Text) == true)
                {
                    if (txtMobileNumber.Text != null && txtMobileNumber.Text != string.Empty)
                        _SSSale.MobileNumberForSMS = txtMobileNumber.Text.ToString();
                    if (_SSSale.MobileNumberForSMS != string.Empty)
                    {
                        DataRow dr = null;
                        dr = _SSSale.GetdebtorDataByMobileNumber(FixAccounts.AccCodeForDebtor);
                        if (dr != null)
                        {
                            if (string.IsNullOrEmpty(dr["AccountID"].ToString()) == false)
                            {
                                mcbCreditor.SelectedID = dr["AccountID"].ToString();
                                _SSSale.AccountID = mcbCreditor.SelectedID;
                            }
                            SetDebtorDeatils();
                        }
                        else txtPatient.Focus();
                    }
                    else
                        txtPatient.Focus();
                }
                else
                    txtPatient.Focus();
            }
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
                        //txtDiscPercent.Focus();
                        txtNextVisitDays.Focus();
                    }
                    else
                    {
                        MainToolStrip.Select();
                        tsBtnSave.Select();
                    }
                   
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                mpPVC1.SetFocus(1);
            }
        }

        private void btnOKCreditDebitNote_Click(object sender, EventArgs e)
        {
            btnOKCreditDebitNoteClick();
        }
        private void btnOKCreditDebitNoteClick()
        {
            double mcrnoteamt = 0;
            double mdbnoteamt = 0;
            string mvoutype = "";
            lblFooterMessage.Text = "";
            lblRightSideFooterMsg.Text = "";
            try
            {
                foreach (DataGridViewRow crdbdr in dgCreditNote.Rows)
                {
                    string ch = string.Empty;
                    double mamt = 0;
                    if (crdbdr.Cells["Col_Check"].Value == null)
                        ch = string.Empty;
                    else
                        ch = crdbdr.Cells["Col_Check"].Value.ToString();
                    if (ch != string.Empty)
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
                CalculateAmount(-1);
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
            pnlClone.SendToBack();
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
                        Product prod = new Product();
                        DataTable dtable = prod.GetOverviewData();

                        mpMSVCFill.DataSource = dtable;

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
            pnlClone.BringToFront();
            txtclonevouno.Focus();
        }

        private void cbclonevoutype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKCloneClick();
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
                //  frmView.Icon = PharmaSYSDistributorPlus.Properties.Resources.Icon;
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
            if (uclSubstituteControl1.Visible == false)
            {
                uclSubstituteControl1.Initialize();
                uclSubstituteControl1.Visible = true;
                uclSubstituteControl1.BringToFront();
                // [ansuman][29.11.2016]
                uclSubstituteControl1.Select();
                uclSubstituteControl1.Focus();
            }
            else
            {
                uclSubstituteControl1.Visible = false;
                uclSubstituteControl1.SendToBack();
            }
        }
        private void uclSubstituteControl1_OnProductSelected(string productID)
        {
            mpPVC1.LoadProduct(productID); // [ansuman] [29.11.2016]
            mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productID;
            //search for productID in datasourceproduct list and send that row  to mppvc1_onproductselected                      

        }
        private void txtPatientAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbDoctor.Focus();
        }
        private void txtDoctorAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mpPVC1.SetFocus(1);
            else if (e.KeyCode == Keys.Up)
                mcbDoctor.Focus();
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
            bool retValue = true;
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {

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
        }
        private void FillBankAccountCombo()
        {
            try
            {
                mcbBankAccount.SelectedID = null;
                mcbBankAccount.SourceDataString = new string[4] { "AccountID", "AccCode", "AccName", "AccAddress1" };
                mcbBankAccount.ColumnWidth = new string[4] { "0", "20", "200", "200" };
                mcbBankAccount.DisplayColumnNo = 2;
                mcbBankAccount.ValueColumnNo = 0;
                mcbBankAccount.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetBankAccountList();
                mcbBankAccount.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
                mpPVC1.Enabled = false;
                pnlCenter.Enabled = false;
                pnlAmounts.Enabled = false;
                cbTransactionType.Enabled = false;
                cbNewTransactionType.Enabled = true;
                cbNewTransactionType.Items.Clear();
                if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                {

                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditCard); //Amar 21/4/2017
                    if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
                        cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
                    cbNewTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                    cbNewTransactionType.SelectedIndex = cbNewTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                }
                else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                {
                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditCard); //Amar 21/4/2017
                            
                    if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
                        cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
                    cbNewTransactionType.Text = FixAccounts.TransactionTypeForCash;
                    cbNewTransactionType.SelectedIndex = cbNewTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);

                }
                else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                {
                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditCard); //Amar 21/4/2017
                    cbNewTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                    cbNewTransactionType.SelectedIndex = cbNewTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                }
                btnTypeChange.Enabled = false;
                cbNewTransactionType.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclDebtorSale.btnTypeChange_Click>>" + Ex.Message);
            }
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            ttDebtorSale.SetToolTip(cbTransferSale, "Sale By Purchase Rate");
            ttDebtorSale.SetToolTip(cbFill, "Show Product List From DebtorProduct Link");
            ttDebtorSale.SetToolTip(cbEditRate, "Edit Sale Rate");
        }
        #endregion

        #region UIEvents 

        private void txtPatient_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                if (txtPatient.SelectedID != null && txtPatient.SelectedID.Trim() != string.Empty && txtPatient.SeletedItem.ItemData[2] != null)
                    txtPatientAddress.Text = txtPatient.SeletedItem.ItemData[2];
                //  else
                //      txtPatientAddress.Text = "";
                if (txtPatientAddress.Text == null || txtPatientAddress.Text.ToString().Trim() == string.Empty)
                    txtPatientAddress.Focus();
                //else if (txtPatientAddress.Text == null || txtPatientAddress.Text.ToString().Trim() == string.Empty)
                //    txtPatientAddress.Focus();
                else
                    mcbDoctor.Focus();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress2.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                mcbCreditor.Focus();
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMobileNumber.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                mcbCreditor.Focus();
            }
        }

        private void mcbDoctor_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtPatient.Focus();
        }

        private void txtPatient_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtMobileNumber.Focus();
        }

        private void mcbCreditor_UpArrowPressed(object sender, EventArgs e)
        {
            txtTokenNumber.Focus();
        }

        private void UclDebtorSale_Load(object sender, EventArgs e)
        {
            FillAll();
        }

        //kiran 20/12/2016
        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            BtnClearPatientClick();
        }
        private void BtnClearPatientClick() // [31.01.2017]
        {
            txtPatient.Text = string.Empty;
            txtPatientAddress.Text = string.Empty;
            txtPatient.ReadOnly = false;

            mcbDoctor.Text = string.Empty;
            txtDoctorAddress.Text = string.Empty;
            mcbDoctor.ReadOnly = false;
            btnPreviousSale.Text = "0.00";
            btnPreviousSale.Enabled = false;
            this.txtPatient.Focus();
        }

        private void mpPVC1_OnSetlblFotterMultipleMRP()
        {
            lblFooterMessage.Text = "Multiple MRP";
        }

        private void mpPVC1_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            Exit();
        }

        private void mpPVC1_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                mpPVC1OnCellEntered(e);
                if (General.CurrentSetting.MsetSaleShowProfitInSaleBill == "Y" && mpPVC1.Rows.Count > 0 && e.ColumnIndex == 1
                       && string.IsNullOrEmpty(Convert.ToString(mpPVC1.Rows[e.RowIndex].Cells[0].Value)) == false)
                {
                    txtProfit.Visible = lblProfit.Visible = true;
                    txtProfit.Text = Convert.ToDouble(mpPVC1.Rows[e.RowIndex].Cells["Col_ProfitInRupees"].Value).ToString("#0.00");
                }
                else
                    txtProfit.Visible = lblProfit.Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void mpPVC1OnCellEntered(DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 11) // Quantity
                {
                    if (e.RowIndex >= 0)
                    {
                        int mbatchstock = 0;
                        string mprodno = "";
                        string mbtno = "";
                        string mrp = "";
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                            mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                            mbtno = mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString();
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                            mrp = mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString();
                        int currentrow = e.RowIndex;
                        int totbatchsale = 0;
                        int totproductsale = 0;
                        int saleqty = 0;

                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value != null)
                            int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value.ToString().Trim(), out mbatchstock);

                        foreach (DataGridViewRow dr in mpPVC1.Rows)
                        {
                            if (dr.Cells["Col_ProductID"].Value != null && dr.Index != currentrow && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                            {
                                int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                                totproductsale += saleqty;
                                if (dr.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno && dr.Cells["Col_MRP"].Value.ToString().Trim() == mrp)
                                {
                                    totbatchsale += saleqty;
                                }
                            }
                        }

                        UpdateTempDebtorSaleDt();
                        General.ProdID = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private DataTable CreateDebtorSaleDt()
        {
            dtTempDebtorSale = CacheObject.Get<DataTable>("TempCounterSale");
            List<DataRow> rowsToDelete = new List<DataRow>();

            if (dtTempDebtorSale == null)
            {
                dtTempDebtorSale = new DataTable();
                dtTempDebtorSale.Columns.Add("ProductID", typeof(string));
                dtTempDebtorSale.Columns.Add("BatchID", typeof(string));
                dtTempDebtorSale.Columns.Add("QTY", typeof(int));
                dtTempDebtorSale.Columns.Add("SRate", typeof(double));
                dtTempDebtorSale.Columns.Add("FormName", typeof(string));
                dtTempDebtorSale.Columns.Add("StockID", typeof(string));
                //productid, batch,mrp
                CacheObject.Add(dtTempDebtorSale, "TempCounterSale");
                //DataRow[] drFormRows = dtTempPatientSale.Select("FormName");

            }
            foreach (DataRow item in dtTempDebtorSale.Rows)
            {
                if (string.Equals(item["FormName"], this.Name))
                {
                    rowsToDelete.Add(item);
                }
            }
            foreach (DataRow row in rowsToDelete)
            {
                dtTempDebtorSale.Rows.Remove(row);
            }
            return dtTempDebtorSale;

        }
        private void UpdateTempDebtorSaleDt()
        {
            try
            {
                DataTable dtTempCounterSale = CreateDebtorSaleDt();
                //dtTempCounterSale.Clear();

                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_SaleRate"].Value != null)
                    {
                        if (dtTempCounterSale.Rows.Count > 0)
                        {
                            //  DataRow[] TempCounterSale = dtTempCounterSale.Select("ProductID=" + dr.Cells["Col_ProductID"].Value + " And BatchID=" + dr.Cells["Col_BatchNumber"].Value + " And SRate=" + dr.Cells["Col_SaleRate"].Value);
                            DataRow[] TempCounterSale = dtTempCounterSale.Select("ProductID='" + dr.Cells["Col_ProductID"].Value + "' And BatchID='" + dr.Cells["Col_BatchNumber"].Value + "' And SRate='" + dr.Cells["Col_SaleRate"].Value + "' And FormName='" + this.Name + "'");
                            if (TempCounterSale.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_Quantity"].Value)))
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(TempCounterSale[0]["QTY"])))
                                        TempCounterSale[0]["QTY"] = Convert.ToInt32(TempCounterSale[0]["QTY"]) + Convert.ToInt32(dr.Cells["Col_Quantity"].Value);
                                    else
                                        TempCounterSale[0]["QTY"] = dr.Cells["Col_Quantity"].Value;
                                }

                            }
                            else
                            {
                                DataRow drTempCounterSale = dtTempCounterSale.NewRow();

                                drTempCounterSale["StockID"] = dr.Cells["Col_StockID"].Value;
                                drTempCounterSale["ProductID"] = dr.Cells["Col_ProductID"].Value;
                                drTempCounterSale["BatchID"] = dr.Cells["Col_BatchNumber"].Value;
                                drTempCounterSale["QTY"] = dr.Cells["Col_Quantity"].Value;
                                drTempCounterSale["FormName"] = this.Name;

                                if (dr.Cells["Col_SaleRate"].Value != null)
                                    drTempCounterSale["SRate"] = dr.Cells["Col_SaleRate"].Value;
                                else
                                    drTempCounterSale["SRate"] = DBNull.Value;

                                dtTempCounterSale.Rows.Add(drTempCounterSale);

                            }
                        }
                        else
                        {
                            DataRow drTempCounterSale = dtTempCounterSale.NewRow();
                            drTempCounterSale["StockID"] = dr.Cells["Col_StockID"].Value;
                            drTempCounterSale["ProductID"] = dr.Cells["Col_ProductID"].Value;
                            drTempCounterSale["BatchID"] = dr.Cells["Col_BatchNumber"].Value;
                            drTempCounterSale["QTY"] = dr.Cells["Col_Quantity"].Value;
                            drTempCounterSale["FormName"] = this.Name;

                            if (dr.Cells["Col_SaleRate"].Value != null)
                                drTempCounterSale["SRate"] = dr.Cells["Col_SaleRate"].Value;
                            else
                                drTempCounterSale["SRate"] = DBNull.Value;

                            dtTempCounterSale.Rows.Add(drTempCounterSale);
                        }
                    }
                }
                CacheObject.Add(dtTempCounterSale, "TempCounterSale");
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
            }
        }

        private void cbRound_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbRound.BackColor = Color.PapayaWhip;
                MainToolStrip.Select();
                tsBtnSave.Select();
            }
        }
        private void cbEditRate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEditRate.Checked)
                mpPVC1.ColumnsMain["Col_SaleRate"].ReadOnly = false;
            else
                mpPVC1.ColumnsMain["Col_SaleRate"].ReadOnly = true;
            mpPVC1.SetFocus(mpPVC1.Rows.Count - 1, 1);
        }
        private void mcbDoctor_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (mcbDoctor.SeletedItem != null && string.IsNullOrEmpty(Convert.ToString(mcbDoctor.SelectedID)) == false)
            {
                if (mcbDoctor.SeletedItem.ItemData[2] != null)
                    txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2].ToString();
            }
        }

        #endregion UIEvents 

        #region PresaleFunctionality

        private void btnPreviousSale_KeyDown(object sender, KeyEventArgs e)
        {
            btnPreviousSaleClick();
        }
        private void btnPreviousSaleClick()
        {
            try
            {
            if (dgvPreviousSale.Visible == true)
                dgvPreviousSale.Visible = false;
            else
            {
                    if (_BindingSourcePreviousSale != null && _BindingSourcePreviousSale.Rows.Count > 0)
                    {
                dgvPreviousSale.Visible = true;
                dgvPreviousSale.ReadOnly = true;
                dgvPreviousSale.BringToFront();
                dgvPreviousSale.Location = GetPreviousSaleLocation();
                int ht = 0;
                    ht = (_BindingSourcePreviousSale.Rows.Count * 35) + 50;
                dgvPreviousSale.Size = new Size(180, ht);
                FillPreviousSaleGrid();
                dgvPreviousSale.BringToFront();
                dgvPreviousSale.Focus();
                if (dgvPreviousSale.Rows.Count > 0)
                {
                    dgvPreviousSale.Select();
                    dgvPreviousSale.Rows[0].Selected = true;
                }
            }
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillPreviousSaleGrid()
        {
            int month = 0;
            string cmonth = "";
            string voudate = "";
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            ConstructPreviousSaleColumns();
            try
            {
                foreach (DataRow dr in _BindingSourcePreviousSale.Rows)
                {
                    if (dr["VoucherDate"] != DBNull.Value)
                    {
                        voudate = dr["VoucherDate"].ToString();
                        DateTime mydt = Convert.ToDateTime(General.GetyyyyMMddDateInDateType(voudate));
                        month = mydt.Month;
                        cmonth = mfi.GetMonthName(month).ToString().Substring(0, 3).ToUpper();
                        double amtnet = Convert.ToDouble(dr["AmountNet"].ToString());
                        int rowindex = dgvPreviousSale.Rows.Add();
                        dgvPreviousSale.Rows[rowindex].Cells["Col_Month"].Value = cmonth;
                        dgvPreviousSale.Rows[rowindex].Cells["Col_Amount"].Value = amtnet.ToString("0.00");
                        dgvPreviousSale.Rows[rowindex].Cells["Col_MonthNumber"].Value = month.ToString("#0");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructPreviousSaleColumns()
        {
            DataGridViewTextBoxColumn column;
            dgvPreviousSale.Columns.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Month";
                column.HeaderText = "Mon";
                column.Width = 40;
                dgvPreviousSale.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "SaExpiry";
                column.HeaderText = "Sale(Rs)";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; ;
                dgvPreviousSale.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MonthNumber";
                column.HeaderText = "Mon";
                column.Visible = false;
                column.Width = 40;
                dgvPreviousSale.Columns.Add(column);


            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void btnPreviousSale_Click(object sender, EventArgs e)
        {
            try
            {
            btnPreviousSaleClick();
        }

             catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void dgvPreviousSale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgPrviousSaleBillWise.Visible = true;
            dgPrviousSaleBillWise.BringToFront();
        }

        private void dgPrviousSaleBillWise_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgPrviousSaleBillWise_CellContentClick();
        }
        private void dgPrviousSaleBillWise_CellContentClick()
        {
            if (dgPrviousSaleBillWise.Rows.Count > 0 && dgPrviousSaleBillWise.SelectedRows.Count > 0)
            {
                dgPrviousSaleBillWise.Visible = false;
                dgvPreviousSale.Visible = false;
                int vouno = 0;
                string voutype = "";
                if (dgPrviousSaleBillWise.CurrentRow.Cells["Col_VoucherType"].Value != null)
                    voutype = dgPrviousSaleBillWise.CurrentRow.Cells["Col_VoucherType"].Value.ToString();
                if (dgPrviousSaleBillWise.CurrentRow.Cells["Col_VoucherNumber"].Value != null)
                    vouno = Convert.ToInt32(dgPrviousSaleBillWise.CurrentRow.Cells["Col_VoucherNumber"].Value.ToString());
                txtclonevouno.Text = vouno.ToString();
                cbclonevoutype.Text = voutype;
                btnOKCloneClick();
            }
        }

        private void dgPrviousSaleBillWise_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                dgPrviousSaleBillWise.Visible = false;
                dgvPreviousSale.Visible = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                dgPrviousSaleBillWise_CellContentClick();
            }
        }

        private void dgvPreviousSale_Click(object sender, EventArgs e)
        {
            dgvPreviousSale_Click();
        }
        private void dgvPreviousSale_Click()
        {
            if (dgvPreviousSale.Rows.Count > 0)
            {
                int mmonth = 0;
                if (dgvPreviousSale.CurrentRow.Cells["Col_MonthNumber"].Value != null)
                    mmonth = Convert.ToInt32(dgvPreviousSale.CurrentRow.Cells["Col_MonthNumber"].Value.ToString());
                FillBillWiseSaleForGivenMonth(mmonth);
                dgPrviousSaleBillWise.BringToFront();
                dgvPreviousSale.Visible = false;
                dgPrviousSaleBillWise.Visible = true;
                dgPrviousSaleBillWise.Focus();
                if (dgPrviousSaleBillWise.Rows.Count > 0)
                {
                    dgPrviousSaleBillWise.Rows[0].Selected = true;
                }
            }
        }
        private void FillBillWiseSaleForGivenMonth(int mmonth)
        {
            ConstructPrviousSaleBillWiseColumns();
            DataTable dtable = _SSSale.GetPreviousSaleBillWise(mcbCreditor.SelectedID, mmonth);
            BindBillWiseSaleForGivenMonth(dtable);
        }

        private void BindBillWiseSaleForGivenMonth(DataTable dtable)
        {
            try
            {
                foreach (DataRow dr in dtable.Rows)
                {
                    int rowIndex = dgPrviousSaleBillWise.Rows.Add();
                    DataGridViewRow dgvrow = dgPrviousSaleBillWise.Rows[rowIndex];
                    dgvrow.Cells["Col_ID"].Value = dr["ID"].ToString();
                    dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    dgvrow.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    dgvrow.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                    dgvrow.Cells["Col_VoucherDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                    dgvrow.Cells["Col_AccName"].Value = dr["PatientName"].ToString();
                    double mamt = Convert.ToDouble(dr["AmountNet"].ToString());
                    dgvrow.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ConstructPrviousSaleBillWiseColumns()
        {
            try
            {
                dgPrviousSaleBillWise.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgPrviousSaleBillWise.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 60;
                dgPrviousSaleBillWise.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgPrviousSaleBillWise.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "Sub";
                column.Width = 40;
                dgPrviousSaleBillWise.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "DATE";
                column.Width = 70;
                column.DefaultCellStyle.Format = "d";
                dgPrviousSaleBillWise.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 400;
                column.Visible = false;
                dgPrviousSaleBillWise.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_Address";
                //column.DataPropertyName = "AccAddress1";
                //column.HeaderText = "Address";
                //column.Width = 200;
                //dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgPrviousSaleBillWise.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void dgvPreviousSale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvPreviousSale.Rows.Count > 0 && dgvPreviousSale.SelectedRows.Count > 0)
                    dgvPreviousSale_Click();
            }
        }

        private void mpMSVCFill_OnCellEnterKeyPressed(int SelectedCellIndex)
        {
            if (mpMSVCFill.Rows.Count > 0 && mpMSVCFill.ColumnsMain["Col_SQuantity"].Index == SelectedCellIndex)
            {
                this.ActiveControl = btnOKFill;
                btnOKFill.Focus();
            }
        }

        #endregion PresaleFunctionality

        private void cbNewTransactionType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCreditCard)
                {
                    lblBank.Visible = true;
                    mcbBankAccount.Visible = true;
                    mcbBankAccount.Focus();
                }
                else //Amar
                {
                    lblBank.Visible = false;
                    mcbBankAccount.Visible = false;
                    //mcbBankAccount.SeletedItem = null;
                }
            }
        }

        private void cbNewTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCreditCard)
            {
                lblBank.Visible = true;
                mcbBankAccount.Visible = true;
                mcbBankAccount.Focus();
            }
            else //Amar
            {
                lblBank.Visible = false;
                mcbBankAccount.Visible = false;
                //mcbBankAccount.SeletedItem = null;
            }
        }

        private void txtNextVisitDays_KeyDown(object sender, KeyEventArgs e) //Amar
        {
            if (e.KeyCode == Keys.Enter)
            {
                int nextdays = 0;
                DateTime nextvisitday;
                string nextvisitdaystring = "";
                if (txtNextVisitDays.Text != null && txtNextVisitDays.Text != string.Empty)
                {
                    nextdays = Convert.ToInt32(txtNextVisitDays.Text.ToString());
                    if (nextdays > 0)
                    {
                        nextvisitday = datePickerBillDate.Value.AddDays(nextdays);
                        nextvisitdaystring = nextvisitday.Date.ToString("yyyyMMdd");
                        _SSSale.NextVisitDate = nextvisitdaystring;
                        txtnextVisitDate.Text = General.GetDateInShortDateFormat(nextvisitdaystring);
                        txtDayOFWeek.Text = nextvisitday.DayOfWeek.ToString();
                    }
                    else
                    {
                        txtDayOFWeek.Text = "";
                        txtnextVisitDate.Text = "";
                        _SSSale.NextVisitDate = "";
                    }
                    if (txtDiscPercent.Visible)
                    {
                        txtDiscPercent.Focus();
                    }
                    else
                    {
                        tsBtnSave.Select();
                        MainToolStrip.Select();
                    }
                }
                else
                {
                    _SSSale.NextVisitDate = "";
                    txtDiscPercent.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)
                txtNarration.Focus();
        }
    }
}