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
    public partial class UclHospitalSale : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SSSale _SSSale;
        private BaseControl ViewControl;
        private Form frmView;
        // string _preID = "";
        public int _PreCurrentQuantity = 0;
        DataTable dtTempHospitalSale;
        Timer timer = new Timer();
       // bool IsSetSaveInvoice = false;
        #endregion

        #region Constructor
        public UclHospitalSale()
        {
            try
            {
                InitializeComponent();
                _SSSale = new SSSale();
                SearchControl = new UclHospitalSaleSearch();
                timer.Tick += new EventHandler(timer_Tick);
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
                    mcbInwardNumber.Focus();
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
                UpdateTempCounterSaleDt();
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
                headerLabel1.Text = "HOSPITAL SALE -> NEW";

                if (General.CurrentUser.Level > 1)
                    cbEditRate.Visible = false;
                _SSSale.ReadBillPrintSettings();
                mpPVC1.ModuleNumber = ModuleNumber.HospitalSale;
                mpPVC1.OperationMode = OperationMode.Add;
                mpPVC1.EditedTempDataList = null;
                InitialisempPVC1("");
                AddToolTip();
                FillDoctorCombo();
                FillPrescription();
                mcbDoctor.SelectedID = "";
                FillInwardNumber();
                FillCloneType();
                InitializeCheckBoxes();

                mcbInwardNumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
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
                //  ClearData();
                FillPrescription();
                headerLabel1.Text = "HOSPITAL SALE -> EDIT";
                InitializeCheckBoxes();

                FillInwardNumber();
                EnableDisable();
                mpPVC1.ClosePopupGrid();
                mpPVC1.ModuleNumber = ModuleNumber.HospitalSale;
                mpPVC1.OperationMode = OperationMode.Edit;
                mpPVC1.EditedTempDataList = null;
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
                txtName.Enabled = false;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                txtVouchernumber.Focus();
            }
            else
            {
                txtName.Enabled = false;
                txtPatient.Enabled = true;
                mcbDoctor.Enabled = true;
                txtVouchernumber.ReadOnly = true;
                txtVouchernumber.Enabled = false;
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

            if (AllBillGridsAreEmpty())
            {

                try
                {
                    retValue = base.Exit();
                    if (retValue)
                    {
                        tsBtnCancel.Visible = true;
                        // UpdateClosingStockinCache();
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
            else if ((Mode == OperationMode.Add || Mode == OperationMode.Edit))
            {
                PSDialogResult result;
                result = PSMessageBox.Show("Save Or Remove All Invoices..", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
            }

            return retValue;
        }
        public bool AllBillGridsAreEmpty()
        {
            bool retValue = false;
            if (Convert.ToDouble(txtBillAmount.Text) == 0)
                retValue = true;
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "HOSPITAL SALE -> DELETE";
                InitializeCheckBoxes();
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
            if (_SSSale.CrdbAmountBalance != _SSSale.CrdbAmountNet)
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
                        //  UpdateClosingStockinCache();
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
                headerLabel1.Text = "HOSPITAL SALE -> VIEW";
                InitializeCheckBoxes();
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
                    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
                }
                _SSSale.SaleSubType = FixAccounts.SubTypeForHospitalSale;
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
                if (General.CurrentSetting.MsetSortOrder == FixAccounts.SortByShelf)
                    mpPVC1.Sort(mpPVC1.ColumnsMain["Col_Shelf"], ListSortDirection.Ascending);

                if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                    PrintSaleBillPrePrintedPaper();
                else
                    PrintSaleBillPlainPaper();
            }
            ClearData();
            return retValue;
        }
        private void PrintSaleBillPlainPaper()
        {
            PharmaSYSDistributorPlus.Printing.PlainPaperPrinter printer = new PharmaSYSDistributorPlus.Printing.PlainPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "", _SSSale.Telephone, _SSSale.DoctorName, _SSSale.DoctorAddress, mpPVC1.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount, "");

        }
        private void PrintSaleBillPrePrintedPaper()
        {
            PharmaSYSDistributorPlus.Printing.PrePrintedPaperPrinter printer = new PharmaSYSDistributorPlus.Printing.PrePrintedPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "", _SSSale.Telephone, _SSSale.DoctorName, _SSSale.DoctorAddress, mpPVC1.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount, "");

        }

        public override bool Save()
        {
            return SaveData(false);
        }

        public override bool SaveAndPrint()
        {
            return SaveData(true);
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            _SSSale.SaleSubType = FixAccounts.SubTypeForHospitalSale;
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
            _SSSale.SaleSubType = FixAccounts.SubTypeForHospitalSale;
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
            _SSSale.SaleSubType = FixAccounts.SubTypeForHospitalSale;
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
            int lastvouno = _SSSale.GetLastVoucherNumber(FixAccounts.VoucherTypeForCashSale, FixAccounts.SubTypeForPatientSale, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            _SSSale.SaleSubType = FixAccounts.SubTypeForHospitalSale;
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _SSSale.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _SSSale.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno + 1; i <= lastvouno; i++)
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
        private bool SaveData(bool printData)
        {
            bool retValue = false;
            if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
            {
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
                if (txtNetAmount.Text != null && Convert.ToDouble(txtNetAmount.Text.ToString()) > 0)
                {
                    try
                    {
                        System.Text.StringBuilder _errorMessage;
                        //if (rbtCash.Checked == true)  //Amar
                        //    txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
                        //else
                            txtVouType.Text = FixAccounts.VoucherTypeForCreditSale;
                        _SSSale.SaleSubType = FixAccounts.SubTypeForHospitalSale;

                        if (mcbInwardNumber.SelectedID != null)
                            _SSSale.AccountID = mcbInwardNumber.SelectedID.Trim();
                        _SSSale.PatientID = "";
                        if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "")
                        {
                            _SSSale.DocID = mcbDoctor.SelectedID.Trim();
                            {
                                _SSSale.DoctorName = mcbDoctor.SeletedItem.ItemData[1];
                                _SSSale.DoctorAddress = mcbDoctor.SeletedItem.ItemData[2];
                            }
                        }

                        _SSSale.CrdbVouType = txtVouType.Text.Trim();

                        if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                            _SSSale.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                        _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                        double.TryParse(txtVatInput5per.Text, out mvat5per);
                        _SSSale.CrdbVat5 = mvat5per;
                        double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                        _SSSale.CrdbVat12point5 = mvat12point5per;
                        double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamtforzerovat);
                        _SSSale.CrdbAmtForZeroVAT = mamtforzerovat;
                        double.TryParse(txtDiscPercent.Text, out mdiscper);
                        _SSSale.CrdbDiscPer = mdiscper;
                        double.TryParse(txtDiscAmount.Text, out mdiscamount);
                        _SSSale.CrdbDiscAmt = mdiscamount;
                        _SSSale.TotalDiscount5 = Convert.ToDouble(txtdiscountAmount5.Text.ToString());
                        _SSSale.TotalDiscount12point5 = Convert.ToDouble(txtDiscountAmount12point5.Text.ToString());
                        _SSSale.TotalDiscount5 = Convert.ToDouble(txtdiscountAmount5.Text.ToString());
                        _SSSale.TotalDiscount12point5 = Convert.ToDouble(txtDiscountAmount12point5.Text.ToString());
                        double.TryParse(txtAmountVAT12Point5Per.Text, out mamountvat12point5per);
                        _SSSale.CrdbAmountVat12point5 = mamountvat12point5per;
                        double.TryParse(txtAmountVAT5Per.Text, out mamountvat5per);
                        _SSSale.CrdbAmountVat5 = mamountvat5per; double.TryParse(txtBillAmount.Text, out mbillamount);
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
                        if (txtRoundAmount.Text != null)
                            _SSSale.CrdbRoundAmount = Convert.ToDouble(txtRoundAmount.Text.ToString());
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
                        if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != string.Empty)
                        {
                            _SSSale.DoctorName = mcbDoctor.SeletedItem.ItemData[1];
                            _SSSale.DoctorAddress = mcbDoctor.SeletedItem.ItemData[2];
                        }
                        _SSSale.OperatorID = "";
                        _SSSale.OperatorPassword = txtOperator.Text.ToString();
                        CalculateProfitPercent();
                        if (_Mode == OperationMode.Edit)
                            _SSSale.IFEdit = "Y";
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            LockTable.LocktblVoucherNo();
                            _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                        }
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
                                    //_SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                                    if (_SSSale.AccountID != null && _SSSale.AccountID != "")
                                        _SSSale.SaleSubType = FixAccounts.SubTypeForHospitalSale;
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
                                        if (_SSSale.CrNoteAmount > 0 || _SSSale.DbNoteAmount > 0)
                                            SaveAndUpdateDebitCreditNote();
                                    }
                                    if (retValue)
                                        retValue = SaveIntblTrnac();

                                    if (retValue)
                                        General.CommitTransaction();
                                    else
                                        General.RollbackTransaction();
                                    LockTable.UnLockTables();
                                    System.IO.File.Delete(General.GetHospitalSaleTempFile());
                                    if (retValue)
                                    {
                                        // UpdateClosingStockinCache();
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
                                        HasShowSavedMessage(printData, Convert.ToDouble(txtBillAmount.Text.ToString()));
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
                            txtPatient.Focus();
                        }
                        LockTable.UnLockTables();
                    }
                    catch (Exception Ex)
                    {
                        LockTable.UnLockTables();
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
            }
            else
            {
                if (General.CurrentSetting.MsetSaleTenderAmount == "Y")
                {
                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print, Convert.ToDouble(BillAmt.ToString()));
                    if (result == PSDialogResult.Print)
                        Print();
                }
                else
                {
                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                    if (result == PSDialogResult.Print)
                        Print();
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
                    FillDoctorCombo();
                    mcbDoctor.SelectedID = _SSSale.DocID;
                    txtVouType.Text = _SSSale.CrdbVouType;
                    FillInwardNumber();
                    mcbInwardNumber.SelectedID = _SSSale.AccountID;
                    BindTempGrid();

                    InitialisempPVC1(Vmode);
                    txtAddress1.Text = _SSSale.PatientAddress1;
                    txtAddress2.Text = _SSSale.PatientAddress2;
                    txtPatient.Text = _SSSale.ShortName;
                    txtNarration.Text = _SSSale.CrdbNarration.ToString();
                    txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString().Trim();
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
                    _SSSale.DoctorName = mcbDoctor.SeletedItem.ItemData[1];
                    _SSSale.DoctorAddress = mcbDoctor.SeletedItem.ItemData[2];
                    //if (txtVouType.Text == FixAccounts.VoucherTypeForCashSale)
                    //    rbtCash.Checked = true;  //Amar
                    //else
                        rbtCashCredit.Checked = true;
                    NoofRows();
                    txtAddress1.Enabled = false;
                    txtAddress2.Enabled = false;
                    txtPatient.Enabled = false;
                    if (_Mode == OperationMode.View)
                    {
                        mpPVC1.ColumnsMain["Col_ProductName"].ReadOnly = true;
                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                        mpPVC1.IsAllowDelete = false;
                        mcbInwardNumber.Enabled = false;
                        mcbDoctor.Enabled = false;
                    }
                    else
                    {
                        mpPVC1.ColumnsMain["Col_ProductName"].ReadOnly = false;
                        mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = false;
                        mpPVC1.IsAllowDelete = true;
                        mcbInwardNumber.Enabled = true;
                        mcbDoctor.Enabled = true;
                        mpPVC1.SetFocus(1);
                        mpPVC1.Select();
                        if (_Mode == OperationMode.Edit)
                        {
                            //rbtCash.Enabled = false;
                            rbtCashCredit.Enabled = false;
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
                if (closedControl is UclHospitalPatient)
                    FillInwardNumber();
                else if (closedControl is UclProduct)
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
                    mcbInwardNumber.Focus();
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
                    if (cbEditRate.Visible == true)
                    {
                        cbEditRate.Focus();
                    }
                    retValue = true;
                }
                //if (keyPressed == Keys.H && modifier == Keys.Alt)
                //{
                //    rbtCash.Focus();
                //    retValue = true;
                //}
                if (keyPressed == Keys.I && modifier == Keys.Alt)
                {
                    rbtCashCredit.Focus();
                    retValue = true;
                }

                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    txtNarration.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {

                    if (pnlClone.Visible)
                    {
                        btnOKCloneClick();
                    }
                    if (pnlDebtorProduct.Visible)
                    {
                        btnOKFill.Focus();
                        //btnOKCreditDebitNoteClick();
                    }
                    else if (pnlDebitCreditNote.Visible)
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
                    }
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
                    if (btnIfDebitCredit.Visible)
                    {
                        BtnIfDebitCreditNoteClick();
                    }
                    retValue = true;
                }
                if (uclSubstituteControl1.Visible)
                {
                    retValue = uclSubstituteControl1.HandleShortcutAction(keyPressed, modifier); // ansuman
                }
                if (keyPressed == Keys.Escape)
                {
                    if (pnlDebitCreditNote.Visible)
                    {
                        btnOKCreditDebitNoteClick();
                        retValue = true;
                    }
                    else if (uclSubstituteControl1.Visible == true) // ansuman
                    {
                        uclSubstituteControl1.Visible = false;
                        mpPVC1.SetFocus(mpPVC1.Rows.Count - 1, 1);
                        retValue = true;
                    }
                    else if (mpPVC1.VisibleProductGrid() == true) //kiran
                    {
                        lblFooterMessage.Text = "";
                        lblRightSideFooterMsg.Text = "";
                        //retValue = true;
                    }
                    else if (pnlTotals.Visible == true)
                    {
                        //pnlTotals.Visible = false;
                        mpPVC1.SetFocus(1);

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
        //private void CalculateProfitPercent()
        //{
        //    _SSSale.ProfitPercentByPurchaseRate = 0;
        //    _SSSale.ProfitPercentBySaleRate = 0;
        //    _SSSale.TotalProfitPercentByPurchaseRate = 0;
        //    _SSSale.TotalProfitPercentBySaleRate = 0;
        //    _SSSale.TotalProfitInRupees = 0;

        //    double mqty = 0;
        //    double mpurrate = 0;
        //    double mtraderate = 0;
        //    double msalerate = 0;
        //    double mpakn = 0;
        //    double mvatper = 0;
        //    double mvatamt = 0;
        //    try
        //    {
        //        foreach (DataGridViewRow prodrow in mpPVC1.Rows)
        //        {
        //            mqty = 0;
        //            mpurrate = 0;
        //            mtraderate = 0;
        //            msalerate = 0;
        //            mpakn = 0;
        //            mvatper = 0;
        //            mvatamt = 0;

        //            if (prodrow.Cells["Col_ProductName"].Value != null &&
        //               Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
        //            {
        //                if (prodrow.Cells["Col_UOM"].Value != null)
        //                    double.TryParse(prodrow.Cells["Col_UOM"].Value.ToString(), out mpakn);
        //                if (prodrow.Cells["Col_Quantity"].Value != null)
        //                    double.TryParse(prodrow.Cells["Col_Quantity"].Value.ToString().Trim(), out mqty);
        //                if (prodrow.Cells["Col_PurchaseRate"].Value != null)
        //                    double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
        //                _SSSale.PurchaseRate = mpurrate;
        //                if (prodrow.Cells["Col_TradeRate"].Value != null)
        //                    double.TryParse(prodrow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
        //                _SSSale.TradeRate = mtraderate;
        //                if (prodrow.Cells["Col_VATPer"].Value != null)
        //                    double.TryParse(prodrow.Cells["Col_VATPer"].Value.ToString(), out mvatper);
        //                mvatamt = Math.Round((mtraderate * mvatper) / 100, 2);
        //                double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
        //                _SSSale.SaleRate = msalerate;
        //                _SSSale.ProfitPercentBySaleRate = Math.Round((msalerate - (mpurrate + mvatamt)) / msalerate, 4);
        //                _SSSale.ProfitPercentByPurchaseRate = Math.Round((msalerate - (mpurrate + mvatamt)) / (mpurrate + mvatamt), 4);
        //                _SSSale.TotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
        //                _SSSale.TotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
        //                _SSSale.ProfitInRupees = Math.Round(((msalerate - (mpurrate + mvatamt)) / mpakn) * mqty, 2);
        //                _SSSale.TotalProfitInRupees += _SSSale.ProfitInRupees;
        //                prodrow.Cells["Col_ProfitPercentBySaleRate"].Value = _SSSale.ProfitPercentBySaleRate.ToString("#0.00");
        //                prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _SSSale.ProfitPercentByPurchaseRate.ToString("#0.00");
        //                prodrow.Cells["Col_ProfitInRupees"].Value = _SSSale.ProfitInRupees.ToString("#0.00");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

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

                        _SSSale.ProfitPercentBySaleRate = Math.Round(((msalerate - PerPackDiscount) - (mpurrate )) / (msalerate - PerPackDiscount), 4);
                        _SSSale.ProfitPercentByPurchaseRate = Math.Round(((msalerate - PerPackDiscount) - (mpurrate)) / (mpurrate), 4);
                        //_SSSale.TotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
                        //_SSSale.TotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
                        _SSSale.ProfitInRupees = Math.Round((((msalerate - PerPackDiscount) - (mpurrate )) / mpakn) * mqty, 2);
                        _SSSale.TotalProfitInRupees += _SSSale.ProfitInRupees;
                        prodrow.Cells["Col_ProfitPercentBySaleRate"].Value = _SSSale.ProfitPercentBySaleRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _SSSale.ProfitPercentByPurchaseRate.ToString("#0.00");
                        prodrow.Cells["Col_ProfitInRupees"].Value = _SSSale.ProfitInRupees.ToString("#0.00");
                    }

                }
                _SSSale.TotalProfitPercentBySaleRate = Math.Round(((totalsale) - (totalpur + totalvat)) / (totalsale), 4);
                _SSSale.TotalProfitPercentByPurchaseRate = Math.Round(((totalsale) - (totalpur + totalvat)) / (totalpur + totalvat), 4);
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
                            _SSSale.LastStockID = mlastsaleid;
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
                _SSSale.CrdbVat5 = mvat5per;
                double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                _SSSale.CrdbVat12point5 = mvat12point5per;
                double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamtforzerovat);
                _SSSale.CrdbAmtForZeroVAT = mamtforzerovat;
                double.TryParse(txtDiscPercent.Text, out mdiscper);
                _SSSale.CrdbDiscPer = mdiscper;
                double.TryParse(txtDiscAmount.Text, out mdiscamount);
                _SSSale.CrdbDiscAmt = mdiscamount;
                double.TryParse(txtBillAmount.Text, out mbillamount);
                _SSSale.CrdbAmountNet = mbillamount;
                double.TryParse(txtRoundAmount.Text, out mround);
                _SSSale.CrdbRoundAmount = mround;
                double.TryParse(txtAddOn.Text, out maddon);
                _SSSale.CrdbAddOn = maddon;

                mdebit = Math.Round(mbillamount - Math.Round(mvat5per, 2) - Math.Round(mvat12point5per, 2) + mdiscamount - maddon + mround - mamtforzerovat + mcreditnoteamt - mdebitnoteamt, 2);

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

                }

                if (mvat5per > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput6Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(mvat5per, 2);
                    retValue = _SSSale.AddVoucherIntblTrnac();

                }
                if (mvat12point5per > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput13point5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(mvat12point5per, 2);
                    retValue = _SSSale.AddVoucherIntblTrnac();
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
                }
                if (mround < 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = (mround * -1);
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mround > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mround;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
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
        //        foreach (DataGridViewRow prodrow in mpPVC1.Rows)
        //        {
        //            if (prodrow.Cells["Col_ProductID"].Value != null && prodrow.Cells["Col_ProductID"].Value.ToString() != string.Empty)
        //            {
        //                _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();                       
        //                PharmaSYSDistributorPlusCache.RefreshProductData(_SSSale.ProductID);
        //            }
        //        }
        //        foreach (DataGridViewRow prodrow in dgtemp.Rows)
        //        {
        //            if (prodrow.Cells["Temp_ProductID"].Value != null && prodrow.Cells["Temp_ProductID"].Value.ToString() != string.Empty)
        //            {
        //                _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();                       
        //                PharmaSYSDistributorPlusCache.RefreshProductData(_SSSale.ProductID);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        returnVal = false;
        //    }
        //    return returnVal;
        //}
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
                    headerLabel1.Text = "Hospital Sale => Changed Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else if (vmode == "D")
                {
                    dtable = _SSSale.ReadProductDetailsByIDForDeleted();
                    headerLabel1.Text = "Hospital Sale => Deleted Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else
                    dtable = _SSSale.ReadProductDetailsByID();
                mpPVC1.DataSourceMain = dtable;

                if (_Mode != OperationMode.View)
                {
                    Product prod = new Product();
                    dtable = prod.GetOverviewData();
                    mpPVC1.DataSourceProductList = dtable;
                }
                // mpPVC1.DataSourceProductList = General.ProductList;
                string tempFileName = General.GetHospitalSaleTempFile();
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
                    {
                        mpPVC1.Rows.Add(1);
                    }
                    mpPVC1.AddRowsInStockTempTable();
                    CalculateAmount(-1);
                    //  CalculateAllAmounts();
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

        private void ClearControls()
        {
            try
            {
                mpPVC1.ProductListGridWidth = 700;
                mpPVC1.BatchListGridWidth = 690;
                _SSSale.SaleSubType = FixAccounts.SubTypeForHospitalSale;
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtNarration.Text = General.CurrentSetting.MsetFixedNarration.ToString();
                txtVouchernumber.Clear();
                txtName.Clear();
                txtPatient.Clear();
                txtVouType.Text = "   ";
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
                mcbInwardNumber.SelectedID = "";
                mcbDoctor.SelectedID = "";
                mcbPrescription.SelectedID = "";
                mcbInwardNumber.Focus();
                lblFooterMessage.Text = "";
                lblRightSideFooterMsg.Text = "";
                if (_Mode != OperationMode.Add)
                {
                    mcbInwardNumber.Enabled = false;
                    txtVouchernumber.ReadOnly = false;
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.Focus();
                }
                else
                {
                    mcbInwardNumber.Enabled = true;
                    txtVouchernumber.ReadOnly = true;
                    txtVouchernumber.Enabled = false;
                    mcbInwardNumber.Focus();
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
                mcbInwardNumber.Focus();
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
                dgCreditNote.Visible = false;
                pnlDebitCreditNote.Visible = false;
                lblCreditNote.Visible = false;
                lblDebitNote.Visible = false;
                txtCreditNote.Visible = false;
                txtDebitNote.Visible = false;
                pnlCenter.BringToFront();
                pnlDebitCreditNote.Visible = false;
                dgCreditNote.Visible = false;
                pnlDebtorProduct.Visible = false;
                mpMSVC1.Visible = false;
                pnlCenter.Dock = DockStyle.Fill;
                mpPVC1.Dock = DockStyle.Fill;
                //rbtCash.Enabled = true;
                rbtCashCredit.Enabled = true;
                if (_Mode == OperationMode.Edit)
                {
                    txtVouchernumber.ReadOnly = false;
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.Focus();
                    mcbInwardNumber.Enabled = false;
                }
                mcbInwardNumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void FillInwardNumber()
        {
            try
            {
                mcbInwardNumber.SelectedID = null;
                mcbInwardNumber.SourceDataString = new string[7] { "ID", "InwardNumber", "PatientName", "Address1", "Address2", "ShortNameAddress", "DoctorID" };
                mcbInwardNumber.ColumnWidth = new string[7] { "0", "200", "200", "200", "0", "0", "0" };
                mcbInwardNumber.ValueColumnNo = 0;
                mcbInwardNumber.UserControlToShow = new UclHospitalPatient();
                HospitalPatient _Party = new HospitalPatient();
                DataTable dtable = _Party.GetOverviewData();
                mcbInwardNumber.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillMainGridwithMultipleBatch(int requiredqty, string productID)
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

            foreach (DataRow dtrow in stkdt.Rows)
            {
                if (dtrow["ClosingStock"] != DBNull.Value)
                    mactualclosingstock += Convert.ToInt32(dtrow["ClosingStock"].ToString());
            }
            try
            {

                foreach (DataRow dtrow in stkdt.Rows)
                {
                    int mbatchstock = 0;
                    int mactualsalestock = 0;
                    double msalerate = 0;
                    string stkdtstockid = dtrow["StockID"].ToString();
                    int.TryParse(dtrow["ClosingStock"].ToString(), out mbatchstock);
                    mactualsalestock = Math.Min(mbatchstock, msalestk);
                    if (mactualsalestock > 0 && msalestk > 0 && mactualclosingstock > 0)
                    {
                        string mbtno = "";
                        double mmrp = 0;




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
                        int.TryParse(dtrow["ClosingStock"].ToString(), out mclstkdr1);
                        mpPVC1.Rows[mycolindex].Cells["Col_BatchStock"].Value = mclstkdr1;

                        if (mactualclosingstock < msalestk)
                            mpPVC1.Rows[mycolindex].DefaultCellStyle.ForeColor = Color.DarkViolet; // kiran
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
                    mpPVC1.IsAllowNewRow = true;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private DataTable FillCreditDebitNote()
        {
            bool retValue = false;
            DataTable dt = new DataTable();

            string patid = "";

            if (_SSSale.AccountID == "")
                patid = "50001";
            else
                patid = _SSSale.AccountID;

            try
            {
                ConstructCreditNoteColumns();
                dgCreditNote.DoubleColumnNames.Add("Col_AmountNet");
                CreditNoteStock crdb = new CreditNoteStock();

                dt = crdb.GetOverviewDataForDebtorSale(patid, _SSSale.Id);


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


            return dt;
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
                    currentdr.Cells["Col_Check"].Value = false;
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
                    //if (_Mode == OperationMode.Delete)
                    //    currentdr.Cells["Col_Check"].Value = false;
                    // else
                    if (dr["ClearedInID"] != DBNull.Value && dr["ClearedInID"].ToString() != "" && dr["ClearedInID"].ToString() == _SSSale.Id)
                    {
                        currentdr.Cells["Col_Check"].Value = true;
                    }
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
            return true;
        }
        private void CalculateAmount(int deletedrowindex)
        {
            lblFooterMessage.Text = "";
            lblRightSideFooterMsg.Text = "";
            double mTotalAmount = 0;
            //  double mTotalAmountforDiscount = 0;
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

                                dr.Cells["Col_VATAmount"].Value = (mvatamount12point5 + mvatamount5).ToString("#0.00");
                                dr.Cells["Col_DiscountAmount"].Value = (mdiscamt5 + mdiscamt12point5 + mdiscamtzero).ToString("#0.00");
                                dr.Cells["Col_MySpecialDiscountAmount"].Value = mmyspecialdiscountamt5 + mmyspecialdiscountamt12point5 + mmyspecialdiscountamtzero;
                                mTotalAmount += mamt;
                                mtotalafterdiscount += mnewamt + mmyspecialdiscountamt12point5 + mmyspecialdiscountamt5;
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
                txtAmount.Text = Math.Round(mTotalAmount, 2).ToString("#0.00");
                //  txtMyDiscountAmount5.Text = mtotalmyspecialdiscamt5.ToString("#0.00");
                //   txtMyDiscountAmount12point5.Text = mtotalmyspecialdiscamt12point5.ToString("#0.00");
                txtVatInput5per.Text = mTvatamount5.ToString("#0.00");
                txtVatInput12point5per.Text = mTvatamount12point5.ToString("#0.00");
                txtAmountVAT12Point5Per.Text = mTotalAmountVAT12.ToString("#0.00");
                txtAmountVAT5Per.Text = mTotalAmountVAT5.ToString("#0.00");

                double mdblDiscAmount = mtotaldiscountamount5 + mtotaldiscountamount12point5 + mtotalmyspecialdiscamtzero;
                txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                mtotalafterdiscountwithoutmydiscount = mTotalAmount - mdblDiscAmount;
                txtTotalafterdiscount.Text = mtotalafterdiscountwithoutmydiscount.ToString("#0.00");

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
                UpdateTempCounterSaleDt();
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
                                        if (dr1.Cells["Col_ProductID"].Value != null && dr1.Cells["Col_ProductID"].Value.ToString() != "")
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
                                        mpPVC1.Rows[mycolindex].Cells["Col_ProdCompShortName"].Value = dtrow["ProdCompShortName"].ToString(); // [06.01.2017]
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

        //private void PrintBill()
        //{
        //    try
        //    {
        //        frmCounterSaleBill saleBill = new frmCounterSaleBill();
        //        saleBill.SaleId = _SavedID;
        //        saleBill.ShowDialog();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        private void InitializeCheckBoxes()
        {
            try
            {
                if (cbEditRate.Visible == true)
                {
                    cbEditRate.Checked = false;
                    cbEditRate.Enabled = true;
                }
                rbtCashCredit.Checked = true;
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
                column.Width = 300;
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
                column.HeaderText = "  Batch No  ";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.Width = 100;
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
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
                column.HeaderText = "Comp";
                column.Width = 50;
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
        //private void ConstructmpMSVC1Columns()
        //{
        //    DataGridViewTextBoxColumn column;
        //    mpMSVC1.ColumnsMain.Clear();
        //    //0
        //    try
        //    {
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_ProductID";
        //        column.DataPropertyName = "ProductID";
        //        column.HeaderText = "ProductID";
        //        column.Width = 0;
        //        column.Visible = false;
        //        mpMSVC1.ColumnsMain.Add(column);
        //        //1
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_ProductName";
        //        column.DataPropertyName = "ProdName";
        //        column.HeaderText = "ProductName";
        //        column.Width = 200;
        //        column.ReadOnly = true;
        //        mpMSVC1.ColumnsMain.Add(column);
        //        //2 
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_UOM";
        //        column.DataPropertyName = "ProdLoosePack";
        //        column.HeaderText = "UOM";
        //        column.ReadOnly = true;
        //        column.Width = 50;
        //        mpMSVC1.ColumnsMain.Add(column);
        //        //3
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Pack";
        //        column.DataPropertyName = "ProdPack";
        //        column.HeaderText = "Pack";
        //        column.Width = 60;
        //        column.ReadOnly = true;
        //        mpMSVC1.ColumnsMain.Add(column);
        //        //4
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Comp";
        //        column.DataPropertyName = "ProdCompShortName";
        //        column.HeaderText = "Pack";
        //        column.Width = 60;
        //        column.ReadOnly = true;
        //        mpMSVC1.ColumnsMain.Add(column);
        //        //5              
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_ClosingStock";
        //        column.DataPropertyName = "ProdClosingStock";
        //        column.HeaderText = "Cl.tock";
        //        column.Width = 60;
        //        column.ReadOnly = true;
        //        mpMSVC1.ColumnsMain.Add(column);
        //        //6
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_Quantity";
        //        column.DataPropertyName = "Quantity";
        //        column.HeaderText = "Required.Qty";
        //        column.Width = 60;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //        column.ReadOnly = true;
        //        mpMSVC1.ColumnsMain.Add(column);
        //        //7
        //        column = new DataGridViewTextBoxColumn();
        //        column.Name = "Col_SQuantity";               
        //        column.HeaderText = "Sale.Qty";
        //        column.Width = 70;
        //        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //        mpMSVC1.ColumnsMain.Add(column);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
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
                column.Width = 180;
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
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "COM";
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
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSVCFill.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SQuantity";
                column.HeaderText = "Sale.Qty";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.DefaultCellStyle.Format = "N2";
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
                else
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = 0;
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
                string mbatchno = "";
                double mpurrate = 0;
                double mtraderate = 0;
                double msalerate = 0;
                int mclstk = 0;
                string mprodno = "";
                string mlastsalestockid = "";
                int mqty = 0;
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

                        mclstk = mclstk + tempproductstock - totproductsale;

                        mclosingstock = mclosingstock - totbatchsale;



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
            int deletedrowindex = 0;
            //   int newindex = 0;
            try
            {
                DataGridViewRow deletedrow = (DataGridViewRow)sender;
                deletedrowindex = deletedrow.Index;
                CalculateAmount(deletedrowindex);
                lblFooterMessage.Text = "";
                lblRightSideFooterMsg.Text = "";
                if (_SSSale.AddNewRowCheck(mpPVC1))
                    mpPVC1.Rows.Add();
                mpPVC1.SetFocus(1);
                UpdateTempCounterSaleDt();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            ////if (newindex > mpPVC1.Rows.Count)
            ////    mpPVC1.SetFocus(deletedrowindex, 1);
            ////else
            ////    mpPVC1.SetFocus(newindex, 1);
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }

        //private void mpPVC1_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    int colIndex = e.ColumnIndex;
        //    mpPVC1CellValueChanged(colIndex);
        //}
        private void mpPVC1_OnCellValueChangeCommited(int colIndex)
        {
            mpPVC1CellValueChanged(colIndex);
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
                {
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
                if (colIndex == 11)
                {
                    lblFooterMessage.Text = "";
                    lblRightSideFooterMsg.Text = "";
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
                                    requiredQty = _SSSale.CurrentBatchStock;
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = requiredQty.ToString("#0");
                                if (requiredQty <= 0 || _SSSale.CurrentBatchStock == 0)
                                {
                                    //here
                                    if (requiredQty <= 0)
                                    {
                                        lblFooterMessage.Text = "Enter Quantity";
                                        mpPVC1.SetFocus(11);
                                        mpPVC1.IsAllowNewRow = false;
                                    }
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

                                        lblFooterMessage.Text = "";
                                        lblRightSideFooterMsg.Text = "";


                                        if (requiredQty <= _SSSale.CurrentBatchStock || (requiredQty > _SSSale.CurrentBatchStock && General.CurrentSetting.MsetSaleAllowNegativeStock == "Y"))
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
                                            mpPVC1.ColumnsMain["Col_Quantity"].ReadOnly = true;
                                            WriteToXML();
                                        }
                                    }
                                    else
                                    {
                                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = _PreCurrentQuantity.ToString("#0");
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
                    UpdateTempCounterSaleDt();
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
                            lblRightSideFooterMsg.Text = "";
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
        private void mpPVC1_OnShiftTABKeyPressed(object sender, EventArgs e)
        {
            mcbDoctor.Focus();
        }
        private void mpPVC1_OnSetlblFotterMultipleMRP()
        {
            lblFooterMessage.Text = "Multiple MRP";
        }
        private void mpPVC1_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            mpPVC1OnCellEntered(e);
        }
        private void mpPVC1OnCellEntered(DataGridViewCellEventArgs e)
        {
            try
            {

                //if (e.ColumnIndex == 11) // Quantity
                //{
                //    if (e.RowIndex >= 0)
                //    {
                //        int mbatchstock = 0;
                //        string mprodno = "";
                //        string mbtno = "";
                //        string mrp = "";
                //        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                //            mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                //        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                //            mbtno = mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString();
                //        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                //            mrp = mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString();
                //        int currentrow = e.RowIndex;
                //        int totbatchsale = 0;
                //        int totproductsale = 0;
                //        int saleqty = 0;

                //        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value != null)
                //            int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value.ToString().Trim(), out mbatchstock);

                //        foreach (DataGridViewRow dr in mpPVC1.Rows)
                //        {
                //            if (dr.Cells["Col_ProductID"].Value != null && dr.Index != currentrow && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                //            {
                //                int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                //                totproductsale += saleqty;
                //                if (dr.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno && dr.Cells["Col_MRP"].Value.ToString().Trim() == mrp)
                //                {
                //                    totbatchsale += saleqty;
                //                }
                //            }
                //        }

                //UpdateTempDebtorSaleDt();
                General.ProdID = "";
                // }
                // }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private DataTable CreateCounterSaleDt()
        {
            try
            {
                dtTempHospitalSale = CacheObject.Get<DataTable>("TempCounterSale");
                List<DataRow> rowsToDelete = new List<DataRow>();
                if (dtTempHospitalSale == null)
                {
                    dtTempHospitalSale = new DataTable();
                    dtTempHospitalSale.Columns.Add("ProductID", typeof(string));
                    dtTempHospitalSale.Columns.Add("BatchID", typeof(string));
                    dtTempHospitalSale.Columns.Add("QTY", typeof(int));
                    dtTempHospitalSale.Columns.Add("SRate", typeof(double));
                    dtTempHospitalSale.Columns.Add("FormName", typeof(string));
                    dtTempHospitalSale.Columns.Add("StockID", typeof(string));
                    //productid, batch,mrp
                    CacheObject.Add(dtTempHospitalSale, "TempCounterSale");
                }

                foreach (DataRow item in dtTempHospitalSale.Rows)
                {
                    if (string.Equals(item["FormName"], this.Name))
                    {
                        rowsToDelete.Add(item);
                    }
                }
                foreach (DataRow row in rowsToDelete)
                {
                    dtTempHospitalSale.Rows.Remove(row);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
            }
            return dtTempHospitalSale;

        }
        private void UpdateTempCounterSaleDt()
        {
            try
            {
                DataTable dtTempCounterSale = CreateCounterSaleDt();
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
        private void WriteToXML()
        {
            DataTable dt = mpPVC1.GetGridData();
            if (dt.Rows.Count > 0)
                dt.WriteXml(General.GetHospitalSaleTempFile());
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
            try
            {
                CalculateAmount(-1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }


        private void rbtCash_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void rbtCashCredit_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void rbtCredit_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void rbtCreditCard_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {

        }
        private void txtPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mcbDoctor.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                mcbInwardNumber.Focus();
            }
        }
        private void txtAddOn_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    cbRound.Focus();
                    CalculateAmount(-1);
                    break;
                case Keys.Up:
                    txtDiscPercent.Focus();
                    break;
            }
        }

        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        //CalculateAmount(-1);
                        //txtAddOn.Focus();
                        double mdiscper = 0;
                        if (txtDiscPercent.Text != null && txtDiscPercent.Text.ToString() != string.Empty)
                            mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());
                        if (mdiscper <= General.CurrentSetting.MsetSaleMaxDiscount)
                        {
                            lblFooterMessage.Text = "";
                            CalculateAmount(-1);
                            txtBillAmount.Text = txtNetAmount.Text;
                            //txtOperator.Focus();
                            if (General.CurrentSetting.MsetSaleAskRoundinginSale == "Y")
                            {
                                cbRound.Focus(); // [ansuman][15.11.2016]
                                cbRound.BackColor = Color.LightGreen;
                            }
                        }
                        else
                        {
                            lblFooterMessage.Text = string.Concat("Max Discount Percent is ", General.CurrentSetting.MsetSaleMaxDiscount.ToString());
                            txtDiscPercent.Text = "0.00";
                        }
                    }
                    break;
                case Keys.Down:
                    txtAddOn.Focus();
                    break;
                case Keys.Up:
                    txtNarration.Focus();
                    break;
            }
        }

        private void cbFill_CheckedChanged(object sender, EventArgs e)
        {

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
                mpMSVC1.ColumnsMain.Clear();
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
                    if (mpMSVC1.MainDataGridCurrentRow.Cells[4].Value != null)
                        int.TryParse(mpMSVC1.MainDataGridCurrentRow.Cells[4].Value.ToString().Trim(), out mclstk);
                    if (mpMSVC1.MainDataGridCurrentRow.Cells[5].Value != null)
                        int.TryParse(mpMSVC1.MainDataGridCurrentRow.Cells[5].Value.ToString().Trim(), out mreqstk);
                    if (mpMSVC1.MainDataGridCurrentRow.Cells[6].Value != null)
                        int.TryParse(mpMSVC1.MainDataGridCurrentRow.Cells[6].Value.ToString().Trim(), out msalestk);
                    if (msalestk > mclstk)
                    {
                        msalestk = Math.Min(mclstk, mreqstk);
                        mpMSVC1.MainDataGridCurrentRow.Cells[6].Value = msalestk;
                    }
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
            mpPVC1.SetFocus(mpPVC1.Rows.Count - 1, 1);
            //mpPVC1.SetFocus(1);
        }

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
                mcbPrescription.SelectedID = "";
                CalculateAmount(-1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }
        private void btnSubstitute_Click(object sender, EventArgs e)
        {
            uclSubstituteControl1.Initialize();
            uclSubstituteControl1.Visible = true;
            uclSubstituteControl1.BringToFront();
        }
        private void uclSubstituteControl1_OnProductSelected(string productID)
        {
            mpPVC1.LoadProduct(productID);  // ansuman
            mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productID;
        }

        private void mcbPrescription_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbPrescriptionEnterKeyPressed();
            pnlDebtorProduct.BringToFront();
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
                        mpMSVCFill.SetFocus(0, 2);
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

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDiscPercent.Focus();
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

        #endregion

        #region tooltip
        private void AddToolTip()
        {
            ttToolTip.SetToolTip(cbEditRate, "Edit Sale Rate");
            ttToolTip.SetToolTip(gbSaleType, "Select Sale Type");
        }
        #endregion

        #region UIEvents

        private void mcbInwardNumber_EnterKeyPressed(object sender, EventArgs e)
        {
            //mpPVC1.SetFocus(1);
            txtPatient.Focus();
        }

        private void mcbInwardNumber_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = ((PSComboBoxNew)sender).SelectedID;
                mcbInwardNumber.SelectedID = selectedId;
                FillInwardNumber();
                mcbInwardNumber.SelectedID = selectedId;
                if (mcbInwardNumber.SeletedItem != null)
                {
                    _SSSale.DocID = "";
                    txtAddress1.Text = mcbInwardNumber.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbInwardNumber.SeletedItem.ItemData[4];
                    txtPatient.Text = mcbInwardNumber.SeletedItem.ItemData[5];
                    if (mcbInwardNumber.SeletedItem.ItemData[6] != null && mcbInwardNumber.SeletedItem.ItemData[6].ToString() != "")
                        _SSSale.DocID = mcbInwardNumber.SeletedItem.ItemData[6].ToString();
                    mcbDoctor.SelectedID = _SSSale.DocID;
                    mpPVC1.SetFocus(1);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbInwardNumber_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mcbInwardNumber.SeletedItem == null)
                {
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                }
                else
                {
                    txtName.Text = mcbInwardNumber.SeletedItem.ItemData[2];
                    txtAddress1.Text = mcbInwardNumber.SeletedItem.ItemData[3].ToString();
                    txtAddress2.Text = mcbInwardNumber.SeletedItem.ItemData[4].ToString(); ;
                    if (mcbInwardNumber.SeletedItem.ItemData[5] != null)
                        txtPatient.Text = mcbInwardNumber.SeletedItem.ItemData[5].ToString();
                    if (mcbInwardNumber.SeletedItem.ItemData[6] != null)
                    {
                        _SSSale.DocID = mcbInwardNumber.SeletedItem.ItemData[6].ToString();
                        mcbDoctor.SelectedID = _SSSale.DocID;

                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

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
        private void UclHospitalSale_Load(object sender, EventArgs e)
        {

        }
        private void mcbDoctor_UpArrowPressed(object sender, EventArgs e)
        {
            txtPatient.Focus();
        }
        private void txtDiscAmount_KeyDown(object sender, KeyEventArgs e) // [ansuman]
        {
            double mdiscper = 0;
            if (e.KeyCode == Keys.Enter)
            {

                if (txtDiscPercent.Text != null && txtDiscPercent.Text.ToString() != string.Empty)
                    mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());
                if (mdiscper <= General.CurrentSetting.MsetSaleMaxDiscount)
                {
                    lblFooterMessage.Text = "";
                    CalculateDiscountAmtPer(-1);
                    CalculateAmount(-1);
                }
                else
                {
                    lblFooterMessage.Text = string.Concat("Max Discount Percent is ", General.CurrentSetting.MsetSaleMaxDiscount.ToString());
                    txtDiscPercent.Text = "0.00";
                    txtOperator.Focus();
                }
            }
        }
        private void CalculateDiscountAmtPer(int deletedrowindex) // [ansuman]
        {
            double mrate = 0;
            double mamt = 0;
            double mpakn = 0;
            double mqty = 0;
            string ifdiscount = "Y";
            double mdiscamt = 0;
            double mdiscper = 0;
            double mvatper = 0;
            double totamtfordiscount = 0;
            double mvatamountfordiscount = 0;
            if (txtDiscAmount.Text != null && txtDiscAmount.Text != string.Empty)
                mdiscamt = Convert.ToDouble(txtDiscAmount.Text.ToString());
            if (mdiscamt > 0)
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
                            mamt = 0;
                            if (dr.Cells["Col_IfSaleDisc"].Value != null && dr.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                                ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString().ToUpper();
                            if (ifdiscount == "Y")
                            {
                                mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                                mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                                mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                                mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());

                                if (Math.Truncate(mqty / mpakn) == (mqty / mpakn))
                                    mamt = Math.Round((mqty / mpakn) * mrate, 2);
                                else
                                {
                                    mamt = Math.Round(Math.Round(mrate / mpakn, 4) * mqty, 2);
                                }
                                mvatamountfordiscount = Math.Round((mamt * mvatper) / (100 + mvatper), 2);
                                totamtfordiscount += (mamt - mvatamountfordiscount);
                            }
                        }
                    }
                }

                if (totamtfordiscount <= mdiscamt)
                {
                    mdiscamt = 0;
                }
                mdiscper = Math.Round((mdiscamt * 100) / totamtfordiscount, 2);
                txtDiscPercent.Text = mdiscper.ToString("#0.00");
            }
            else
            {
                txtDiscPercent.Text = "0.00";
                mdiscper = 0;
            }
        }

        private void mcbDoctor_SeletectIndexChanged(object sender, EventArgs e)
        {
            txtDocAddress.Text = mcbDoctor.SeletedItem.ItemData[2].ToString();
        }

        private void mpPVC1_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            Exit();
        }
        private void SetDateStatus()
        {
            timer.Interval = 1000;

            DateTime _MDate = datePickerBillDate.Value.Date;
            DateTime _CDate = DateTime.Now.Date;
            int result = DateTime.Compare(_MDate, _CDate);
            if (result < 0)
            {
                lblmsg.Text = "You are working in Previous date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                timer.Enabled = true;
                timer.Start();
            }
            else if (result == 0)
            {
                lblmsg.Text = ""; // "You are working in Current date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                timer.Enabled = false;
                timer.Stop();
            }
            else
            {
                lblmsg.Text = "You are working in Next date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                timer.Enabled = true;
                timer.Start();
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (lblmsg.BackColor == Color.Gainsboro)
                lblmsg.BackColor = Color.Red;
            else
                lblmsg.BackColor = Color.Gainsboro;
        }

        private void datePickerBillDate_ValueChanged(object sender, EventArgs e)
        {
            SetDateStatus();
        }

        private void txtDocAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mpPVC1.SetFocus(1);
            }
        }

        private void cbRound_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbRound.BackColor = Color.PapayaWhip; // [ansuman]11.11.2016
                MainToolStrip.Select();
                tsBtnSave.Select();
            }
        }
        #endregion UIEvents

        private void btnClearPatient_Click(object sender, EventArgs e) // [31.01.2017]
        {
            mcbInwardNumber.SelectedID = string.Empty;
            txtName.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtPatient.Text = string.Empty;
            mcbDoctor.SelectedID = string.Empty;
            txtDocAddress.Text = string.Empty;
            mcbInwardNumber.Focus();
        }
    }
}
