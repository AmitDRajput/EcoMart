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
using System.IO;
using EcoMart.InterfaceLayer.Classes;
using EcoMart.InterfaceLayer.CommonControls;
namespace EcoMart.InterfaceLayer
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
        private string MainSaleSubType = "";
        private string headoption = "";
        private int _selectedScmSaleQuantity = 0;
        private int _selectedScmQuantity = 0;
        Timer timer;
        #endregion

        #region contructor
        public UclDistributorSale(string  _saleSubType)
        {
            InitializeComponent();
            _SSSale = new SSSale();
            SearchControl = new UclDistributorSaleSearch(_saleSubType);
            _LastStockID = string.Empty;            
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            //CodeForSubType START
            MainSaleSubType = _saleSubType;
            this.Name = "UclDistributorSale" + MainSaleSubType;
            if (MainSaleSubType == FixAccounts.SubTypeForRegularSale2)
                MainSaleSubType = FixAccounts.SubTypeForRegularSale;
            //CodeForSubType END
        }
        # endregion

        # region IDetail Control
        public override void SetFocus()
        {
            if (_Mode == OperationMode.Add)
                mcbCreditor.Focus();
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

                headoption = "NEW";
                ShowHeading(headoption);

                //cbEditRate.Visible = true;

                InitializeMainSubViewControl("");

                mpMSVC.Enabled = true;
                FillTransactionType();
                FillCombos();
                mpMSVC.BringToFront();
                btnPaymentHistory.Visible = false;
                btnSummary.Enabled = false;
                pnlProductDetail.Enabled = true;
                dgvBatchGrid.Visible = false;
                pnlSummary.Visible = false;
                cbRound.Checked = true;
                mcbCreditor.Enabled = true;
                cbTransactionType.Enabled = true;
                //   txtNarration.Enabled = true;
                txtVouchernumber.Enabled = false;
                // if (General.CurrentSetting.MsetPurchaseChangeSaleRate == "Y")
                //    txtSaleRate.Enabled = true;
                //  else
                //      txtSaleRate.Enabled = false;

                FixVoucherTypeBycbTransactionType();
                mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

            return retValue;
        }

        private void ShowHeading(string ho)
        {
            if (MainSaleSubType == FixAccounts.SubTypeForRegularSale || MainSaleSubType == FixAccounts.SubTypeForRegularSale2)
                headerLabel1.Text = "DISTRIBUTOR REGULAR SALE -> " + ho;
            else if (MainSaleSubType == FixAccounts.SubTypeForSpecialSale)
                headerLabel1.Text = "DISTRIBUTOR SPECIAL SALE -> " + ho;
            else if (MainSaleSubType == FixAccounts.SubTypeForPTSSale)
                headerLabel1.Text = "DISTRIBUTOR PTS SALE -> " + ho;
        }

        private void FillCombos()
        {
            FillPartyCombo();
            FillDelivaryBoyCombo();
            FillDoctorCombo();
            FillSalesmanCombo();
            FillTransporterCombo();
            
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            btnSummary.Enabled = true;
            InitializeScreen();
            tsBtnSave.Enabled = false;
            headoption = "EDIT";
            ShowHeading(headoption);
            //headerLabel1.Text = "DISTRIBUTOR SALE -> EDIT";
            InitializeMainSubViewControl("");
            FillCombos();
            btnPaymentHistory.Visible = true;
            mcbCreditor.Enabled = false;
            pnlProductDetail.Enabled = true;
         //   txtNarration.Enabled = false;
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
                _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
            else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
            else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditStatementSale;
            txtVouType.Text = _SSSale.CrdbVouType;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            pnlGST.Visible = false;
            pnlIGST.Visible = false;
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
            headoption = "DELETE";
            ShowHeading(headoption);
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
                        // UpdateClosingStockinCache();
                        _SSSale.ModifiedBy = General.CurrentUser.Id;
                        _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        _SSSale.AddDetailsInDeleteMaster();
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
            ClearData();
            InitializeScreen();
            headoption = "VIEW";
            ShowHeading(headoption);
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
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
            txtVouchernumber.Focus();
            //tsBtnExit.Select();
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
                _SSSale.SaleSubType = FixAccounts.SubTypeForRegularSale;
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
                if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                    PrintSaleBillPlainPaper();
                else
                    PrintSaleBillPlainPaper();
            }
            return retValue;
        }
        private void PrintSaleBillPrePrintedPaper()
        {
            EcoMart.Printing.PlainPaperPrinterForDistributor printer = new EcoMart.Printing.PlainPaperPrinterForDistributor();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.CrdbName, _SSSale.PatientAddress1, _SSSale.PatientAddress2, _SSSale.Telephone, _SSSale.PatientVATTIN, _SSSale.PartyDLN, _SSSale.PartyLBT, _SSSale.DoctorName, _SSSale.DoctorAddress, mpMSVC.Rows, _SSSale.CrdbAmount, _SSSale.CrdbDiscAmt, _SSSale.CrdbVat12point5, _SSSale.CrdbVat5, _SSSale.CrdbTotalADD, _SSSale.CrdbTotalLESS, _SSSale.CrdbNarration1,_SSSale.CrdbNarration2, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.PendingAmount + _SSSale.CrdbAmountNet, General.ShopDetail.ShopDLNDist, General.ShopDetail.ShopLBT, _SSSale.ItemTotalDiscount, _SSSale.SchemeTotalDiscount, _SSSale.CrdbRoundAmount);

        }

        private void PrintSaleBillPlainPaper()
        {
            EcoMart.Printing.PlainPaperPrinterForDistributor printer = new EcoMart.Printing.PlainPaperPrinterForDistributor();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.CrdbName, _SSSale.PatientAddress1, _SSSale.PatientAddress2, _SSSale.Telephone, _SSSale.PatientVATTIN, _SSSale.PartyDLN, _SSSale.PartyLBT, _SSSale.DoctorName, _SSSale.DoctorAddress, mpMSVC.Rows, _SSSale.CrdbAmount, _SSSale.CrdbDiscAmt, _SSSale.CrdbVat12point5, _SSSale.CrdbVat5, _SSSale.CrdbTotalADD, _SSSale.CrdbTotalLESS, _SSSale.CrdbNarration1, _SSSale.CrdbNarration2, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.PendingAmount + _SSSale.CrdbAmountNet, General.ShopDetail.ShopDLNDist, General.ShopDetail.ShopLBT, _SSSale.ItemTotalDiscount, _SSSale.SchemeTotalDiscount, _SSSale.CrdbRoundAmount);

        }

        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            _SSSale.SaleSubType = FixAccounts.SubTypeForRegularSale;
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
            _SSSale.SaleSubType = FixAccounts.SubTypeForRegularSale;
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
            _SSSale.SaleSubType = FixAccounts.SubTypeForRegularSale;
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
            int lastvouno = _SSSale.GetLastVoucherNumber(txtVouType.Text.ToString(), FixAccounts.SubTypeForRegularSale, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _SSSale.CrdbVouType = txtVouType.Text.ToString();
            _SSSale.SaleSubType = FixAccounts.SubTypeForRegularSale;
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
            //double mvat5per = 0;
            //double mvat12point5per = 0;
         //   double mamtforzerovat = 0;
            double mbillamount = 0;
            double mamount = 0;
            double mround = 0;
         //   double mamountvat5per = 0;
        //    double mamountvat12point5per = 0;
            double mcreditnoteamount = 0;
            double mdebitnoteamount = 0;
            string TransactionText = "";
            double mscmdisc = 0;
            double mitemdisc = 0;
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
                        //  IfAdd();

                    }
                    if (mcbCreditor.SelectedID != null)
                    {
                        _SSSale.AccountID = mcbCreditor.SelectedID.Trim();
                        _SSSale.CrdbName = mcbCreditor.SeletedItem.ItemData[2];
                    }                  
                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _SSSale.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                    _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    double.TryParse(txtCashDiscountPerS.Text, out mdiscper);
                    _SSSale.CrdbDiscPer = mdiscper;
                    double.TryParse(txtCashDiscountAmountS.Text, out mdiscamount);
                    _SSSale.CrdbDiscAmt = mdiscamount;                   
                    double.TryParse(txtBillAmount.Text, out mbillamount);
                    _SSSale.CrdbAmountNet = mbillamount;
                    if (mcbTransporter.SelectedID != null && mcbTransporter.SelectedID != string.Empty)
                        _SSSale.TransporterID = Convert.ToInt32(mcbTransporter.SelectedID);
                    else
                        _SSSale.TransporterID = 0;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditSale)
                    {
                        _SSSale.CrdbAmountBalance = mbillamount - _SSSale.CrdbAmountClear;
                    }                   
                    double.TryParse(txtBillAmountS.Text, out mamount);
                    _SSSale.CrdbAmount = mamount;
                    double.TryParse(txtRoundAmount.Text, out mround);
                    _SSSale.CrdbRoundAmount = mround;
                    double.TryParse(txtAddOnS.Text, out maddon);
                    _SSSale.CrdbAddOn = maddon;
                    double.TryParse(txtCRAmountS.Text, out mcreditnoteamount);
                    _SSSale.CrNoteAmount = mcreditnoteamount;
                    double.TryParse(txtDBAmountS.Text, out mdebitnoteamount);
                    _SSSale.DbNoteAmount = mdebitnoteamount;

                    double.TryParse(txtSchemeDiscountS.Text, out mscmdisc);
                    _SSSale.SchemeTotalDiscount = mscmdisc;
                    double.TryParse(txtItemDiscountS.Text, out mitemdisc);
                    _SSSale.ItemTotalDiscount = mitemdisc;
                    double.TryParse(txtCashDiscountPerS.Text.ToString(), out mdiscper);
                    _SSSale.CrdbDiscPer = mdiscper;
                    double.TryParse(txtCashDiscountAmountS.Text, out mdiscamount);
                    _SSSale.CrdbDiscAmt = mdiscamount;
                    double.TryParse(txtBillAmount.Text, out mbillamount);
                    _SSSale.CrdbAmountNet = mbillamount;
                    
                    _SSSale.ShortName = "";
                    if (txtAddress1.Text != null && txtAddress1.Text != "")
                        _SSSale.PatientAddress1 = txtAddress1.Text;
                    if (txtAddress2.Text != null && txtAddress2.Text != "")
                        _SSSale.PatientAddress2 = txtAddress2.Text;
                    _SSSale.OperatorID = "";                
                    if (_Mode == OperationMode.Edit)
                        _SSSale.IFEdit = "Y";
                    _SSSale.SaleSubType = MainSaleSubType;
                    _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        LockTable.LocktblVoucherNo();
                        _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(txtVouType.Text.Trim(), General.ShopDetail.ShopVoucherSeries);
                    }
                    _SSSale.Validate();


                    if (_SSSale.IsValid)
                    {
                        try
                        {
                            LockTable.LockTablesForSale();
                            if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                            {
                                General.BeginTransaction();

                                _SSSale.CreatedBy = General.CurrentUser.Id;
                                _SSSale.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _SSSale.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                             //   _SSSale.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(_SSSale.CrdbVouType, General.ShopDetail.ShopVoucherSeries);
                                _SSSale.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                                txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString();

                                int intid = _SSSale.AddDetails();
                                // _SavedID = _SSSale.Id;
                                _SSSale.IntID = intid;
                                if (intid > 0)
                                    retValue = true;                                
                                if (retValue)
                                    retValue = SaveParticularsProductwise();

                                if (retValue)
                                    retValue = ReduceStockIntblStock();
                                if (retValue)
                                {
                                    clearPreviousdebitcreditnotes();
                                    if (_SSSale.CrNoteAmount > 0 || _SSSale.DbNoteAmount > 0)
                                        SaveAndUpdateDebitCreditNote();
                                }
                                if (retValue)
                                   retValue = _SSSale.AddAccountDetails();
                                if (retValue)
                                    General.CommitTransaction();
                                else
                                    General.RollbackTransaction();
                                LockTable.UnLockTables();
                                if (retValue)
                                {
                                    // UpdateClosingStockinCache();
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
                            else if (_Mode == OperationMode.Fifth && _SSSale.StatementNumber == 0)
                            {
                                DataTable stocktbl = new DataTable();
                                _SSSale.OldVoucherType = txtVouType.Text.ToString();
                                _SSSale.OldVoucherNumber = _SSSale.CrdbVouNo;
                                _SSSale.CrdbVouNo = int.Parse(txtVouchernumber.Text);
                                if (cbTransactionType.Text == cbNewTransactionType.Text)
                                    _SSSale.IfTypeChange = "N";
                                else
                                    _SSSale.IfTypeChange = "Y";
                                if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCash)
                                    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
                                else if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                                    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
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
                                        if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForCashSale)
                                        {
                                            _SSSale.CrdbAmountBalance = _SSSale.CrdbAmountNet;
                                            _SSSale.CrdbAmountClear = 0;
                                        }
                                        else
                                        {
                                            _SSSale.CrdbAmountBalance = 0;
                                            _SSSale.CrdbAmountClear = _SSSale.CrdbAmountNet;
                                        }
                                        retValue = _SSSale.UpdateDetailsForTypeChange();
                                        if (retValue)
                                        {
                                            retValue = _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
                                        }
                                        if (retValue)
                                        {
                                            retValue = _SSSale.AddAccountDetails();
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
                                            string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                                            PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
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
                               
                                if (retValue)
                                {                                   
                                    retValue = SaveParticularsProductwise();

                                    if (retValue)
                                        retValue = ReduceStockIntblStock();
                                    if (retValue)
                                    {
                                        clearPreviousdebitcreditnotes();
                                        if (_SSSale.CrNoteAmount > 0 || _SSSale.DbNoteAmount > 0)
                                            SaveAndUpdateDebitCreditNote();
                                    }
                                    if (retValue)
                                        retValue = _SSSale.AddAccountDetails();
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
                                    _SSSale.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _SSSale.AddDetailsInChangedMaster();
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

            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                _SSSale.AccountID = FixAccounts.AccountCash.ToString();
            else if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditStatementSale)
                _SSSale.AccountID = FixAccounts.AccountCreditStatementSale.ToString();
            else if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCreditSale)
                _SSSale.AccountID = FixAccounts.AccountCreditSale.ToString();
            txtVouType.Text = _SSSale.CrdbVouType;
            if (mcbCreditor.SelectedID != null)
                _SSSale.AccountID = this.mcbCreditor.SelectedID;
            // _SSSale.PurchaseBillNumber = txtBillNumber.Text;
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
                    txtVouType.Text = _SSSale.CrdbVouType;
                    if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForCreditSale)
                        btnPaymentHistory.Visible = false;
                    else
                    {
                        btnPaymentHistory.Visible = true;
                        BindPaymentDetails();
                    }
                  //  FillPartyCombo();
                    mcbCreditor.SelectedID = _SSSale.AccountID;
                    mcbSalesman.SelectedID = _SSSale.SalesmanID.ToString();
                    mcbTransporter.SelectedID = _SSSale.TransporterID.ToString();
                    BindTempGrid();
                    BindPaymentDetails();

                    FillGSTpnl();
                    InitializeMainSubViewControl(Vmode);
                    if (mcbCreditor.SelectedID != null)
                        _SSSale.GetPartyOtherDetails(mcbCreditor.SelectedID);
                    //  InitialisempMSVC(Vmode);
                    txtAddress1.Text = _SSSale.PatientAddress1;
                    txtAddress2.Text = _SSSale.PatientAddress2;
                 //   txtNarration.Text = _SSSale.CrdbNarration.ToString();
                    txtVouchernumber.Text = _SSSale.CrdbVouNo.ToString().Trim();
                    //txtdiscountAmount5.Text = _SSSale.TotalDiscount5.ToString();
                    //txtDiscountAmount12point5.Text = _SSSale.TotalDiscount12point5.ToString();                   
                    txtAmount.Text = _SSSale.CrdbAmount.ToString("#0.00");
                    txtItemDiscPercent.Text = _SSSale.CrdbDiscPer.ToString("#0.00");
                    txtRoundAmount.Text = _SSSale.CrdbRoundAmount.ToString("#0.00");
                    txtBillAmount.Text = _SSSale.CrdbBillAmount.ToString("#0.00");
                    txtNetAmountS.Text = txtBillAmount.Text;
                    txtTotalS.Text = _SSSale.CrdbTotalAmount.ToString("#0.00");
                    txtItemDiscountAmt.Text = _SSSale.CrdbDiscAmt.ToString("#0.00");
                    DateTime mydate = new DateTime(Convert.ToInt32(_SSSale.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_SSSale.CrdbVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    txtAddOnS.Text = _SSSale.CrdbAddOn.ToString("#0.00");
                    txtItemDiscountS.Text = _SSSale.ItemTotalDiscount.ToString("#0.00");
                    txtCRAmountS.Text = _SSSale.CrNoteAmount.ToString("#0.00");
                    txtDBAmountS.Text = _SSSale.DbNoteAmount.ToString("#0.00");
                    //txtAmountforZeroVAT.Text = _SSSale.CrdbAmtForZeroVAT.ToString("#0.00");
                    //txtAmountVAT12Point5Per.Text = _SSSale.CrdbAmountVat12point5.ToString("#0.00");
                    //txtAmountVAT5Per.Text = _SSSale.CrdbAmountVat5.ToString("#0.00");
                    txtCashDiscountPerS.Text = _SSSale.CrdbDiscPer.ToString("#0.00");
                    txtCashDiscountAmountS.Text = _SSSale.CrdbDiscAmt.ToString("#0.00");
                    txtSchemeDiscountS.Text = _SSSale.SchemeTotalDiscount.ToString("#0.00");
                    if (txtVouType.Text == FixAccounts.VoucherTypeForCashSale)
                        cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                    else if (txtVouType.Text == FixAccounts.VoucherTypeForCreditStatementSale)
                        cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                    else
                        cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                    NoofRows();
                    txtAddress1.Enabled = false;
                    txtAddress2.Enabled = false;

                    if (_Mode == OperationMode.View)
                    {
                        mpMSVC.ColumnsMain["Col_ProductName"].ReadOnly = true;
                        mpMSVC.ColumnsMain["Col_Quantity"].ReadOnly = true;
                        mpMSVC.IsAllowDelete = false;
                        mcbCreditor.Enabled = false;
                    }
                    else
                    {
                        mpMSVC.ColumnsMain["Col_ProductName"].ReadOnly = false;
                        mpMSVC.ColumnsMain["Col_Quantity"].ReadOnly = true;
                        mpMSVC.IsAllowDelete = true;
                        mcbCreditor.Enabled = true;
                        mpMSVC.SetFocus(1);
                        mpMSVC.Select();
                        txtVouchernumber.Enabled = false;
                        if (_Mode == OperationMode.Edit)
                        {
                            cbTransactionType.Enabled = false;
                        }

                    }
                    pnlGST.Visible = false;
                    pnlIGST.Visible = false;
                    if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
                    {
                        int currentrow = mpMSVC.Rows.Add();
                        mpMSVC.SetFocus(currentrow, 1);
                    }
                    if (_Mode == OperationMode.Fifth && _SSSale.StatementNumber == 0)
                    {
                        if (_SSSale.CrdbAmountClear > 0 && _SSSale.CrdbVouType != FixAccounts.VoucherTypeForCashSale)
                            lblFooterMessage.Text = "Payment Done";
                        else
                        {
                            mpMSVC.Enabled = false;
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
        # endregion Idetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {
            try
            {
                General.CurrentSetting.FillSettings();
                if (closedControl is UclProduct)
                {
                    Product prod = new Product();
                    DataTable proddt = prod.GetOverviewData();
                    mpMSVC.DataSource = proddt;
                    //   mpMSVC.DataSource = General.ProductList;
                    mpMSVC.BindGridSub();
                }
                else if (closedControl is UclAccount)
                {
                    string creditorID = mcbCreditor.SelectedID;
                    FillPartyCombo();
                    mcbCreditor.SelectedID = creditorID;
                }
                else if (closedControl is UclCreditNoteAmount || closedControl is UclCreditNoteStock || closedControl is UclDebitNoteAmount || closedControl is UclDebitNotestock)
                    FillCreditDebitNote();
                string oldtrans = cbTransactionType.Text;
                Int32 oldtransindex = cbTransactionType.SelectedIndex;
                FillTransactionType();
                cbTransactionType.Text = oldtrans;
                cbTransactionType.SelectedIndex = oldtransindex;


                if (pnlProductDetail.Visible)
                    txtSaleRate.Focus();
                else
                    mcbCreditor.Focus();

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override bool RefreshProductList()
        {
            Product prod = new Product();
            DataTable proddt = prod.GetOverviewData();
            mpMSVC.DataSource = proddt;          
            mpMSVC.BindGridSub();
            return true;
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            try
            {
                if (uclSubstituteControl1.Visible)
                {
                    retValue = uclSubstituteControl1.HandleShortcutAction(keyPressed, modifier);
                }              
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
                if (keyPressed == Keys.M && modifier == Keys.Alt)
                {
                    if (uclSubstituteControl1.Visible)
                        uclSubstituteControl1.Visible = false;
                    else
                        btnSubstitute_Click();
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
               
                if (keyPressed == Keys.Escape)
                {

                    if (pnlScheme.Visible)
                    {
                        //  btnSchemeCancelClick();
                        CloseSchemePanel();
                        retValue = true;
                    }
                    else if (dgvBatchGrid.Visible)
                    {
                        dgvBatchGrid.Visible = false;
                        pnlProductDetail.Enabled = true;
                        mpMSVC.SetFocus(1);
                        retValue = true;
                    }
                    else if (pnlProductDetail.Visible && dgvBatchGrid.Visible == false)
                    {
                        btnCancelClick();
                        retValue = true;
                    }
                    else if (pnlDebitCreditNote.Visible)
                    {
                        btnDbCrCancelClick();
                        retValue = true;
                    }
                    else if (pnlSummary.Visible)
                    {
                        btnCancelSClick();
                        retValue = true;
                    }
                    else if (mpMSVC.VisibleProductGrid() == true) //kiran
                    {
                        retValue = true;
                    }
                    else if (uclSubstituteControl1.Visible == true)
                    {
                        uclSubstituteControl1.Visible = false;
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
                        if (temprow.Cells["Temp_Quantity"].Value != null)
                            _SSSale.Quantity = Convert.ToInt32(temprow.Cells["Temp_Quantity"].Value.ToString());
                        if (temprow.Cells["Temp_StockID"].Value != null)
                            _SSSale.StockID = temprow.Cells["Temp_StockID"].Value.ToString();
                        mclosingstock = 0;
                        foreach (DataRow dr in stocktable.Rows)
                        {
                            if (dr["StockID"].ToString() == _SSSale.StockID)
                            {                               
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
            _SSSale.SerialNumber = 0;
            try
            {
                foreach (DataGridViewRow prodrow in mpMSVC.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                            (Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0 ||
                            Convert.ToDouble(prodrow.Cells["Col_Scheme"].Value) > 0))
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
                        double mscmdiscamt = 0;
                        int mscmdiscqty = 0;
                        string mlastsaleid = "";
                      //  string mactbatch = "";
                      //  double mactmrp = 0;
                     //   double mactrate = 0;
                        _SSSale.ProfitPercentBySaleRate = 0;
                        _SSSale.ProfitPercentByPurchaseRate = 0;
                        _SSSale.ProfitInRupees = 0;

                        _SSSale.GSTPurchaseAmountZero = 0;
                        _SSSale.GSTSAmount = 0;
                        _SSSale.GSTCAmount = 0;
                        _SSSale.GSTSPurchaseAmount = 0;
                        _SSSale.GSTCPurchaseAmount = 0;


                        _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                        //if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                        //{
                            _SSSale.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            double.TryParse(prodrow.Cells["Col_MRP"].Value.ToString().Trim(), out mmrp);
                            _SSSale.MRP = mmrp;
                            double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                            _SSSale.SaleRate = msalerate;
                         //   _SSSale.NewBatchno = _SSSale.Batchno;
                        //    _SSSale.NewMRP = _SSSale.MRP;
                        //    _SSSale.NewSaleRate = _SSSale.SaleRate;
                        //}
                        //else
                        //{
                            _SSSale.NewBatchno = prodrow.Cells["Col_NewBatchNumber"].Value.ToString();
                            double.TryParse(prodrow.Cells["Col_NewMRP"].Value.ToString().Trim(), out mmrp);
                            _SSSale.NewMRP = mmrp;
                            double.TryParse(prodrow.Cells["Col_NewSaleRate"].Value.ToString().Trim(), out msalerate);
                            _SSSale.NewSaleRate = msalerate;
                        //    _SSSale.NewBatchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        //    double.TryParse(prodrow.Cells["Col_MRP"].Value.ToString().Trim(), out mmrp);
                         //   _SSSale.NewMRP = _SSSale.MRP;
                         //   double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                         //   _SSSale.NewSaleRate = _SSSale.SaleRate;
                        //}

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
                     //   double.TryParse(prodrow.Cells["Col_MRP"].Value.ToString().Trim(), out mmrp);
                     //   _SSSale.MRP = mmrp;
                     //   double.TryParse(prodrow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                     //   _SSSale.SaleRate = msalerate;
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
                        //if (prodrow.Cells["Col_DiscountAmount"].Value != null)
                        //    _SSSale.CrdbDiscAmt = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                        //if (prodrow.Cells["Col_MySpecialDiscountAmount"].Value != null && prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString() != string.Empty)
                        //    _SSSale.MySpecialDiscountAmount = Convert.ToDouble(prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_Scheme"].Value != null)
                            mscmdiscqty = Convert.ToInt32(prodrow.Cells["Col_Scheme"].Value.ToString());
                        if (prodrow.Cells["Col_ItemSCMDiscountAmount"].Value != null)
                            mscmdiscamt = Convert.ToDouble(prodrow.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                        if (prodrow.Cells["Col_ItemDiscountPer"].Value != null)
                            _SSSale.ItemDiscountPer = Convert.ToDouble(prodrow.Cells["Col_ItemDiscountPer"].Value.ToString());
                        if (prodrow.Cells["Col_ItemDiscountAmount"].Value != null)
                            _SSSale.ItemDiscountAmount = Convert.ToDouble(prodrow.Cells["Col_ItemDiscountAmount"].Value.ToString());

                        if (prodrow.Cells["Col_GSTAmountZero"].Value != null && prodrow.Cells["Col_GSTAmountZero"].Value.ToString() != string.Empty)
                            _SSSale.GSTPurchaseAmountZero = Convert.ToDouble(prodrow.Cells["Col_GSTAmountZero"].Value.ToString());
                        if (prodrow.Cells["Col_GSTSAmount"].Value != null && prodrow.Cells["Col_GSTSAmount"].Value.ToString() != string.Empty)
                            _SSSale.GSTSPurchaseAmount = Convert.ToDouble(prodrow.Cells["Col_GSTSAmount"].Value.ToString());
                        if (prodrow.Cells["Col_GSTCAmount"].Value != null && prodrow.Cells["Col_GSTCAmount"].Value.ToString() != string.Empty)
                            _SSSale.GSTCPurchaseAmount = Convert.ToDouble(prodrow.Cells["Col_GSTCAmount"].Value.ToString());
                        if (prodrow.Cells["Col_GSTS"].Value != null && prodrow.Cells["Col_GSTS"].Value.ToString() != string.Empty)
                            _SSSale.GSTSAmount = Convert.ToDouble(prodrow.Cells["Col_GSTS"].Value.ToString());
                        if (prodrow.Cells["Col_GSTC"].Value != null && prodrow.Cells["Col_GSTC"].Value.ToString() != string.Empty)
                            _SSSale.GSTCAmount = Convert.ToDouble(prodrow.Cells["Col_GSTC"].Value.ToString());

                        _SSSale.SchemeDiscountAmount = mscmdiscamt;
                        _SSSale.SchemeQuanity = mscmdiscqty;
                        _SSSale.LastStockID = mlastsaleid;
                        returnVal = _SSSale.AddProductDetailsSS();
                        if (returnVal == false)
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

        //private bool SaveIntblTrnac()
        //{

        //    bool retValue = false;
        //    try
        //    {
        //        double mdebit = 0;
        //        double mdiscper = 0;
        //        double maddon = 0;
        //        double mdiscamount = 0;
        //        //  double mvat5per = 0;
        //        //  double mvat12point5per = 0;
        //        double mamtforzerovat = 0;
        //        double mbillamount = 0;
        //        double mround = 0;
        //        double mcreditnoteamt = 0;
        //        double mdebitnoteamt = 0;
        //        if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //            _SSSale.ShortNameForNarration = _SSSale.ShortName;
        //        else
        //            _SSSale.ShortNameForNarration = "";
        //        //double.TryParse(txtCreditNote.Text.ToString(), out mcreditnoteamt);
        //        //_SSSale.CrNoteAmount = mcreditnoteamt;
        //        //double.TryParse(txtDebitNote.Text.ToString(), out mdebitnoteamt);
        //        //_SSSale.DbNoteAmount = mdebitnoteamt;
        //        //double.TryParse(txtVatInput5per.Text, out mvat5per);
        //        //_SSSale.CrdbVat5 = Math.Round(mvat5per, 2);
        //        //double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
        //        //_SSSale.CrdbVat12point5 = Math.Round(mvat12point5per, 2);
        //        //double.TryParse(txtAmountforZeroVAT.Text.ToString(), out mamtforzerovat);
        //        //_SSSale.CrdbAmtForZeroVAT = mamtforzerovat;
        //        double.TryParse(txtCashDiscountPerS.Text.ToString(), out mdiscper);
        //        _SSSale.CrdbDiscPer = mdiscper;
        //        double.TryParse(txtCashDiscountAmountS.Text, out mdiscamount);
        //        _SSSale.CrdbDiscAmt = mdiscamount;
        //        double.TryParse(txtBillAmount.Text, out mbillamount);
        //        _SSSale.CrdbAmountNet = mbillamount;
        //        double.TryParse(txtRoundAmount.Text, out mround);
        //        mround = _SSSale.CrdbRoundAmount;
        //        //double.TryParse(txtAddOn.Text, out maddon);
        //        //_SSSale.CrdbAddOn = maddon;

        //        mdebit = Math.Round(mbillamount - Math.Round(_SSSale.CrdbVat5, 2) - Math.Round(_SSSale.CrdbVat12point5, 2) + mdiscamount - maddon - mround - mamtforzerovat + mcreditnoteamt - mdebitnoteamt, 2);

        //        if (mamtforzerovat > 0)
        //        {
        //            _SSSale.DebitAccount = FixAccounts.AccountVATZeroSale;
        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.CreditAccount = FixAccounts.AccountCash;
        //            else
        //                _SSSale.CreditAccount = _SSSale.AccountID;
        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = 0;
        //            _SSSale.CreditAmount = mamtforzerovat;
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;

        //        }

        //        if (Math.Round(_SSSale.CrdbVat5, 2) > 0)
        //        {
        //            _SSSale.DebitAccount = FixAccounts.AccountVatOutput6Sale;
        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.CreditAccount = FixAccounts.AccountCash;
        //            else
        //                _SSSale.CreditAccount = _SSSale.AccountID;
        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = 0;
        //            _SSSale.CreditAmount = Math.Round(_SSSale.CrdbVat5, 2);
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;
        //        }
        //        if (Math.Round(_SSSale.CrdbVat12point5, 2) > 0)
        //        {
        //            _SSSale.DebitAccount = FixAccounts.AccountVatOutput13point5Sale;
        //            //if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //            //    _SSSale.CreditAccount = FixAccounts.AccountCash;
        //            //else
        //                _SSSale.CreditAccount = _SSSale.AccountID;
        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = 0;
        //            _SSSale.CreditAmount = Math.Round(_SSSale.CrdbVat12point5, 2);
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;
        //        }
        //        if (maddon > 0)
        //        {
        //            _SSSale.DebitAccount = FixAccounts.AccountAddonSale;
        //            //if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForDistributorSaleCash)
        //            //    _SSSale.CreditAccount = FixAccounts.AccountCash;
        //            //else
        //                _SSSale.CreditAccount = _SSSale.AccountID;
        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = 0;
        //            _SSSale.CreditAmount = maddon;
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;
        //        }
        //        if (mround < 0)
        //        {
        //            _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.CreditAccount = FixAccounts.AccountCash;
        //            else
        //                _SSSale.CreditAccount = _SSSale.AccountID;
        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = (mround * -1);
        //            _SSSale.CreditAmount = 0;
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;
        //        }
        //        if (mround > 0)
        //        {
        //            _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.CreditAccount = FixAccounts.AccountCash;
        //            else
        //                _SSSale.CreditAccount = _SSSale.AccountID;
        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = 0;
        //            _SSSale.CreditAmount = mround;
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;
        //        }

        //        if (mdiscamount > 0)
        //        {
        //            _SSSale.DebitAccount = FixAccounts.AccountCashDiscountSale;
        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.CreditAccount = FixAccounts.AccountCash;
        //            else
        //                _SSSale.CreditAccount = _SSSale.AccountID;
        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = mdiscamount;
        //            _SSSale.CreditAmount = 0;
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;
        //        }

        //        if (mcreditnoteamt > 0)
        //        {
        //            _SSSale.DebitAccount = FixAccounts.AccountSalesReturn;
        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.CreditAccount = FixAccounts.AccountCash;
        //            else
        //                _SSSale.CreditAccount = _SSSale.AccountID;
        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = mcreditnoteamt;
        //            _SSSale.CreditAmount = 0;
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;
        //        }
        //        if (mdebitnoteamt > 0)
        //        {
        //            _SSSale.DebitAccount = FixAccounts.AccountDebitNoteSale;
        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.CreditAccount = FixAccounts.AccountCash;
        //            else
        //                _SSSale.CreditAccount = _SSSale.AccountID;
        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = 0;
        //            _SSSale.CreditAmount = mdebitnoteamt;
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;
        //        }
        //        if (mdebit > 0)
        //        {

        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.DebitAccount = FixAccounts.AccountCashSale;
        //            else
        //                _SSSale.DebitAccount = FixAccounts.AccountCreditSale;
        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.CreditAccount = FixAccounts.AccountCash;
        //            else
        //                _SSSale.CreditAccount = _SSSale.AccountID;
        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = 0;
        //            _SSSale.CreditAmount = mdebit;
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;
        //        }
        //        if (mbillamount > 0)
        //        {
        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.DebitAccount = FixAccounts.AccountCash;
        //            else
        //                _SSSale.DebitAccount = _SSSale.AccountID;

        //            if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
        //                _SSSale.CreditAccount = FixAccounts.AccountCashSale;
        //            else
        //                _SSSale.CreditAccount = FixAccounts.AccountCreditSale;

        //            _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
        //            _SSSale.DebitAmount = mbillamount;
        //            _SSSale.CreditAmount = 0;
        //            retValue = _SSSale.AddVoucherIntblTrnac();
        //            if (retValue == false)
        //                return retValue;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //    return retValue;
        //}
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
                foreach (DataGridViewRow prodrow in mpMSVC.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       (Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0 || Convert.ToDouble(prodrow.Cells["Col_Scheme"].Value) > 0))
                    {
                        mlastsaleid = "";
                        _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                     //   if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                     //   {
                            _SSSale.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                     //   }
                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        _SSSale.SchemeQuanity = 0;
                        if (prodrow.Cells["Col_Scheme"].Value != null && prodrow.Cells["Col_Scheme"].Value.ToString() != string.Empty)
                            _SSSale.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Col_Scheme"].Value.ToString());
                        _SSSale.ProdPakn = 1;
                        //  if (prodrow.Cells["Col_UnitOfMeasure"].Value != null && prodrow.Cells["Col_UnitOfMeasure"].Value.ToString() != string.Empty)
                        //     _SSSale.ProdPakn = Convert.ToInt32(prodrow.Cells["Col_UnitOfMeasure"].Value.ToString());
                        _SSSale.ProdPakn = 1;
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
        //private bool UpdateClosingStockinCache()
        //{
        //    bool returnVal = false;
        //    try
        //    {
        //        General.UpdateProductListCacheTest(mpMSVC.Rows, "Col_ProductID", dgtemp.Rows, "Temp_ProductID");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        returnVal = false;
        //    }
        //    return returnVal;
        //}

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
                    _SSSale.GetPartyOtherDetails(mcbCreditor.SelectedID);
                    txtAddress1.Text = _SSSale.PatientAddress1;
                    txtAddress2.Text = _SSSale.PatientAddress2;
                    if (mcbCreditor.SeletedItem.ItemData[6] != null && mcbCreditor.SeletedItem.ItemData[6].ToString() != "")
                        _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    _SSSale.DocID = mcbCreditor.SeletedItem.ItemData[7];
                    _SSSale.TransactionType = mcbCreditor.SeletedItem.ItemData[8];
                    if (_Mode == OperationMode.Add)
                    {
                        if (_SSSale.TransactionType == "1")
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

                    FillCreditDebitNote();
                    _SSSale.CrdbName = mcbCreditor.SeletedItem.ItemData[2];
                    _SSSale.GetPartyOtherDetails(mcbCreditor.SelectedID);
                    txtAddress1.Text = _SSSale.PatientAddress1;
                    txtAddress2.Text = _SSSale.PatientAddress2;
                    //   txtDiscPercent.Text = mcbCreditor.SeletedItem.ItemData[9];
                    //if (mcbCreditor.SeletedItem.ItemData[6] != "")
                    //    _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[6].ToString());

                    _SSSale.TransactionType = mcbCreditor.SeletedItem.ItemData[6];

                    if (_Mode == OperationMode.Add)
                    {
                        FillTransactionType();
                        //if (_SSSale.TransactionType == "CS")
                        //{
                        //    cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                        //    cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                        //    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
                        //}
                        //else if (_SSSale.TransactionType == "CR")
                        //{
                        //    cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                        //    cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCreditStatement);
                        //    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditStatementSale;
                        //}
                        //else
                        //{
                        //    cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                        //    cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                        //    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
                        //}
                        txtVouType.Text = _SSSale.CrdbVouType;
                    }

                    _SSSale.GetPendingAmount(mcbCreditor.SelectedID);
                    _SSSale.GetOpeningBalance(mcbCreditor.SelectedID);
                    _SSSale.PendingAmount = _SSSale.OpeningBalance + (_SSSale.TotalDebit - _SSSale.TotalCredit);
                    txtPendingBalance.Text = Math.Abs(_SSSale.PendingAmount).ToString("#0.00");
                 //   txtNarration.Focus();
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
                _SSSale.CurrentProductStock = 0;
                if (selectedRow.Cells["Col_ClStock"].Value != null && selectedRow.Cells["Col_ClStock"].Value.ToString() != string.Empty)
                    _SSSale.CurrentProductStock = Convert.ToInt32(selectedRow.Cells["Col_ClStock"].Value.ToString());
                lblFooterMessage.Text = "Product Stock : " + _SSSale.CurrentProductStock.ToString("#0");
                FillBatchGrid();
                lblFooterMessage.Text = "Esc = Exit   Enter = Select Batch";

                //if (dgvBatchGrid.Rows.Count > 0)
                //{
                dgvBatchGrid.BringToFront();
                dgvBatchGrid.Location = GetdgvBatchGridLocation();
                dgvBatchGrid.Height = 237;
                dgvBatchGrid.Width = pnlProductDetail.Width;
                dgvBatchGrid.Visible = true;
                dgvBatchGrid.Enabled = true;
                dgvBatchGrid.Enabled = true;
                pnlProductDetail.Enabled = false;
                //   CalculatePurRateSaleRateAndAmount();
                if (dgvBatchGrid.Rows.Count > 0)
                    dgvBatchGrid.Select();
                dgvBatchGrid.Focus();
                //}
                //else
                //{
                //    mpMSVC.MainDataGridCurrentRow.Cells["Col_Batchnumber"].ReadOnly = false;
                //    mpMSVC.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = false;
                //    mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;
                //    mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                //   // mpMSVC.MainDataGridCurrentRow.Cells["Col_Batchnumber"]
                //}

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
            int difference = pnlSummary.Size.Height - mpMSVC.Size.Height;
            try
            {
                pt.X = mpMSVC.Location.X + 500; // 335;
                pt.Y = mpMSVC.Location.Y - 30;//  + 3;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private Point GetpnlGSTLocation()
        {
            Point pt = new Point();
            int difference = pnlSummary.Size.Height - mpMSVC.Size.Height;

            try
            {
                pt.X = mpMSVC.Location.X + 50;
                pt.Y = mpMSVC.Location.Y - 30; // difference + 50 ;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }
        private Point GetdgvBatchGridLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = mpMSVC.Location.X + 300;
                pt.Y = mpMSVC.Location.Y + 100;
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
                returnVal = _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
            }
            catch { returnVal = false; }
            return returnVal;
        }


        #endregion

        #region Contruct

        public void ConstructSchemeColumns()
        {
            DataGridViewTextBoxColumn column;
            dgScheme.ColumnsMain.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Selected";
                column.Visible = false;
                column.HeaderText = "Check";
                dgScheme.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmText1";
                column.Width = 120;                
                column.HeaderText = "Scheme";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgScheme.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmProductQuantity";
                column.Width = 80;
                column.HeaderText = "Quantity";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                dgScheme.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmText2";
                column.Width = 80;
                column.HeaderText = " ";
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgScheme.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmSchemeQuantity";
                column.Width = 80;
                column.HeaderText = "ScmQty";
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgScheme.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSVC.ColumnsMain.Clear();

            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 185;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                mpMSVC.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "GST%";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MaxLevel";
                column.DataPropertyName = "ProdMaxLevel";
                column.HeaderText = "MaxLevel";
                column.Width = 30;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "ProdLastSaleStockID";
                column.Width = 60;
                column.Visible = false;
                mpMSVC.ColumnsSub.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GenericCategoryName";
                column.DataPropertyName = "GenericCategoryName";
                column.HeaderText = "GenericCategoryName";
                column.Width = 40;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "ProdLastPurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 30;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);               
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 120;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                    column.Visible = true;
                else
                    column.Visible = false;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //13
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_NewBatchNumber";
                column.DataPropertyName = "ActualBatchNumber";
                column.HeaderText = "Batch";
                column.Width = 120;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                    column.Visible = false;
                else
                    column.Visible = true;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //14
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                    column.Visible = true;
                else
                    column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_NewMRP";
                column.DataPropertyName = "ActualMRP";
                column.HeaderText = "MRP";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                    column.Visible = false;
                else
                    column.Visible = true;
                mpMSVC.ColumnsMain.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                    column.Visible = true;
                else
                    column.Visible = false;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_NewSaleRate";
                column.DataPropertyName = "ActualSaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                    column.Visible = false;
                else
                    column.Visible = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Scheme";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);
                //21
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmount";
                column.DataPropertyName = "SchemeDiscountAmount";
                column.HeaderText = "ScmDisc";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);
                //22
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountPer";
                column.DataPropertyName = "ItemDiscountPer";
                column.HeaderText = "Disc";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);
                //23
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 150;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSVC.ColumnsMain.Add(column);
                //24         
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //25
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //26
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //27
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //28
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //29
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //30
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //31
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "LastStockID";
                column.Width = 60;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //32
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                column.DataPropertyName = "ProfitPercentBySaleRate";
                column.Width = 60;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //33
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                column.DataPropertyName = "ProfitPercentByPurchaseRate";
                column.Width = 60;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //34
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                column.DataPropertyName = "ProfitInRupees";
                column.Width = 60;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);                
                //35
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);              
                //36
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemSCMDiscountAmountPerUnit";              
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //37
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //38
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CashDiscountAmount";
                column.DataPropertyName = "CashDiscountAmount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //39
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmountSale";
                column.DataPropertyName = "VATAmount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //40
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin";              
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //41
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Margin2";              
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //42
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTAmountZero";
                column.DataPropertyName = "GSTAmountZero";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //43
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTSAmount";
                column.DataPropertyName = "GSTSAmount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //44
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTCAmount";
                column.DataPropertyName = "GSTCAmount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //45
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTIAmount";
                column.DataPropertyName = "GSTIAmount";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //46
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTS";
                column.DataPropertyName = "GSTS";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //47
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTC";
                column.DataPropertyName = "GSTC";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //48
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTI";
                column.DataPropertyName = "GSTI";
                column.Width = 80;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //49
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_HSN";
                column.DataPropertyName = "HSNNumber";
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
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                mpMSVC.ColumnsSub.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProdName";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);
                //2                
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);
                //5            
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "GST%";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClStock";
                column.DataPropertyName = "ProdClosingStockPack";
                column.HeaderText = "Cl.Stk";
                column.Width = 55;
                column.ReadOnly = true;
                mpMSVC.ColumnsSub.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MaxLevel";
                column.DataPropertyName = "ProdMaxLevel";
                column.HeaderText = "MaxLevel";
                column.Width = 30;
                column.Visible = false;
                mpMSVC.ColumnsSub.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "ProdLastSaleStockID";
                column.Width = 60;
                column.Visible = false;
                mpMSVC.ColumnsSub.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                mpMSVC.ColumnsSub.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GenericCategoryName";
                column.DataPropertyName = "GenericCategoryName";
                column.HeaderText = "GenericCategoryName";
                column.Width = 40;
                column.Visible = false;
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
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvBatchGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                column.Visible = false;
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_UnitOfMeasure";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Company";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Co.";
                column.Width = 35;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.Visible = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 88;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Exp";
                column.Width = 45;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VAT";
                column.DataPropertyName = "PurchaseVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 45;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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

        private bool AddPreviousStock()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                      (Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0 || Convert.ToDouble(prodrow.Cells["Temp_Scheme"].Value) > 0))
                    {
                        _SSSale.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        _SSSale.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value.ToString());

                        _SSSale.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value.ToString());
                        _SSSale.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_Scheme"].Value.ToString());
                        _SSSale.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurchaseRate"].Value.ToString());
                        _SSSale.ProdPakn = Convert.ToInt32(prodrow.Cells["Temp_UnitOfMeasure"].Value.ToString());
                        _SSSale.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _SSSale.LastStockID = prodrow.Cells["Temp_StockID"].Value.ToString();

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

                txtNoOfRows.Text = _SSSale.NoofRows.ToString();
                mpMSVC.DataSourceMain = dtable;

                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != string.Empty)
                    _SSSale.GetPartyOtherDetails(mcbCreditor.SelectedID);

                //  string tempFileName = General.GetPurchaseTempFile();

                //   if (_Mode == OperationMode.Add && File.Exists(tempFileName))
                //   {
                //      mpMSVC.DataSourceMain = null;
                //       mpMSVC.Rows.Clear();

                //       DataSet ds = new DataSet();
                //        ds.ReadXml(tempFileName);
                //        mpMSVC.DataSourceMain = ds.Tables[0];

                //    }


                // DataTable dt = EcoMartCache.GetProductData();
                //  DataTable dt = General.ProductList;
                Product prod = new Product();
                DataTable proddt = prod.GetOverviewData();
                mpMSVC.DataSource = proddt;
                mpMSVC.Bind();
                //   if (_Mode == OperationMode.Add && File.Exists(tempFileName))
                //   {
                //        CalculateTotals();
                //         mpMSVC.Rows.Add();
                //         mcbCreditor.Focus();
                //     }
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
            try
            {
                mpMSVC.NumericColumnNames.Add("Col_Quantity");
                mpMSVC.NumericColumnNames.Add("Col_Quantity");
                mpMSVC.DoubleColumnNames.Add("Col_MRP");
                mpMSVC.NumericColumnNames.Add("Col_Quantity");
                mpMSVC.DoubleColumnNames.Add("Col_VATPer");
                mpMSVC.DoubleColumnNames.Add("Col_VAT");
                mpMSVC.DoubleColumnNames.Add("Col_PurchaseRate");
                mpMSVC.DoubleColumnNames.Add("Col_Amount");
                mpMSVC.DoubleColumnNames.Add("Col_SaleRate");
                mpMSVC.DoubleColumnNames.Add("Col_TradeRate");
                mpMSVC.DoubleColumnNames.Add("Col_Amount");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                pnlGST.Visible = false;
                pnlIGST.Visible = false;
                ConstructMainColumns();
                pnlTypeChange.Visible = false;
                if ((_Mode == OperationMode.Add || _Mode == OperationMode.Edit) && _SSSale.AddNewRowCheck(mpMSVC))
                    mpMSVC.Rows.Add();
                _SchemeData = null;
                btnSummary.BackColor = Color.Linen;
                txtVoucherSeries.Text = General.ShopDetail.ShopVoucherSeries;
                txtVouchernumber.Clear();
                tsBtnSavenPrint.Enabled = false;
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                txtExpiry.BackColor = Color.White;
             //   txtNarration.Text = General.CurrentSetting.MsetFixedNarration.ToString();
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtQuantity.Text = "0";
                txtSchemeQuantity.Text = "0";
                txtReplacement.Text = "0";
                txtBillAmountS.Text = "0.00";
                txtSchemeDiscountS.Text = "0.00";
                txtItemDiscountS.Text = "0.00";
                //txtSplDiscountS.Text = "0.00";
                txtSplDiscountPerUnit.Text = "";
                //txtSplDiscPerS.Text = "";
                txtAddOnS.Text = "0.00";
                txtCRAmountS.Text = "0.00";
                txtDBAmountS.Text = "0.00";
                txtCashDiscountPerS.Text = "0.00";
                txtCashDiscountAmountS.Text = "0.00";
                txtPurchaseRate.Text = "0.00";
                txtItemDiscPercent.Text = "0.00";
                txtItemDiscountAmt.Text = "0.00";
                txtMasterVATAmt.Text = "0.00";
                txtMasterVATPer.Text = "0.00";
                txtBillAmount.Text = "0.00";
                txtRoundAmount.Text = "0.00";
                mcbCreditor.SelectedID = "";
                txtStockID.Text = "";
                txtPendingBalance.Text = "0.00";
                txtGridAmountTot.Text = "0.00";
                pnlBillDetails.Enabled = true;
                pnlVou.Enabled = true;
                mpMSVC.Rows.Clear();
                txtNoOfRows.Text = "";
                mpMSVC.Enabled = true;
                dgvLastPurchase.Visible = false;
                lblFooterMessage.Text = "";
                btnTypeChange.Visible = false;
                cbNewTransactionType.Visible = false;
                FixVoucherTypeBycbTransactionType();
                cbTransactionType.Focus();
                if (General.CurrentSetting.MsetSaleGetDelivaryBoy == "Y")
                {
                    mcbDelivaryBoy.Visible = true;
                    lblDelivaryBoy.Visible = true;
                }
                else
                {
                    mcbDelivaryBoy.Visible = false;
                    mcbDelivaryBoy.SelectedID = null;
                    lblDelivaryBoy.Visible = false;
                }

                if (General.CurrentSetting.MsetSaleGetDoctor == "Y")
                {
                    mcbDoctor.Visible = true;
                    lblDoctor.Visible = true;
                }
                else
                {
                    mcbDoctor.Visible = false;
                    mcbDoctor.SelectedID = null;
                    lblDoctor.Visible = false;
                }
                pnlScheme.Visible = false;
                _SSSale.SaleSubType = FixAccounts.SubTypeForRegularSale;
                DataTable dtp = new DataTable();
                if (dgPaymentDetails.Rows.Count > 0)
                {
                    dgPaymentDetails.DataSource = dtp;

                }
                _SSSale.SaleSubType = FixAccounts.SubTypeForRegularSale;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillTransactionType()
        {
            cbTransactionType.Items.Clear();
            if (_SSSale.TransactionType == "" || _SSSale.TransactionType == "2")
            {
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
                cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCreditStatement);
                txtVouType.Text = FixAccounts.VoucherTypeForCreditStatementSale;
            }
            else if (_SSSale.TransactionType == "3")
            {
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCredit);
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);              
                cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                txtVouType.Text = FixAccounts.VoucherTypeForCreditSale;
            }
            else
            {
                cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
                cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                txtVouType.Text = FixAccounts.VoucherTypeForCashSale;
            }

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
                mcbCreditor.SourceDataString = new string[8] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccDoctorID", "AccTransactionType", "AccDiscountOffered" };
                mcbCreditor.ColumnWidth = new string[8] { "0", "20", "200", "200", "0", "200", "0", "0" };
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
        private void FillSalesmanCombo()
        {
            try
            {
                mcbSalesman.SelectedID = null;
                mcbSalesman.SourceDataString = new string[2] { "ID", "Name" };
                mcbSalesman.ColumnWidth = new string[2] { "0", "200" };
                mcbSalesman.DisplayColumnNo = 1;
                mcbSalesman.ValueColumnNo = 0;
                mcbSalesman.UserControlToShow = new UclSalesman();
                Salesman _Salesman = new Salesman();
                DataTable dtable = _Salesman.GetOverviewData();
                mcbSalesman.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillDoctorCombo()
        {
            try
            {
                mcbDoctor.SelectedID = null;
                mcbDoctor.SourceDataString = new string[2] { "DocID", "DocName" };
                mcbDoctor.ColumnWidth = new string[2] { "0", "200" };
                mcbDoctor.DisplayColumnNo = 2;
                mcbDoctor.ValueColumnNo = 0;
                mcbDoctor.UserControlToShow = new UclDoctor();
                Doctor _Doctor = new Doctor();
                DataTable dtable = _Doctor.GetOverviewData();
                mcbDoctor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillDelivaryBoyCombo()
        {
            try
            {
                mcbDelivaryBoy.SelectedID = null;
                mcbDelivaryBoy.SourceDataString = new string[2] { "ID", "Name" };
                mcbDelivaryBoy.ColumnWidth = new string[2] { "0", "200" };
                mcbDelivaryBoy.DisplayColumnNo = 2;
                mcbDelivaryBoy.ValueColumnNo = 0;
                mcbDelivaryBoy.UserControlToShow = new UclDelivaryBoy();
                DelivaryBoy _DelivaryBoy = new DelivaryBoy();
                DataTable dtable = _DelivaryBoy.GetOverviewData();
                mcbDelivaryBoy.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillTransporterCombo()
        {
            try
            {
                mcbTransporter.SelectedID = null;
                mcbTransporter.SourceDataString = new string[2] { "ID", "Name" };
                mcbTransporter.ColumnWidth = new string[2] { "0", "200" };
                mcbTransporter.DisplayColumnNo = 1;
                mcbTransporter.ValueColumnNo = 0;
                mcbTransporter.UserControlToShow = new UclDelivaryBoy();
                Transporter _Transporter = new Transporter();
                DataTable dtable = _Transporter.GetOverviewData();
                mcbTransporter.FillData(dtable);
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
                txtItemDiscPercent.Text = Convert.ToString(mitemdiscper.ToString("#0.00")).Trim();
                txtItemDiscountAmt.Text = Convert.ToString(mitemdiscamt.ToString("#0.0000")).Trim();
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
                //if (mpMSVC.MainDataGridCurrentRow.Cells["Col_VAT"].Value != null)
                //    mpurvatper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_VAT"].Value.ToString());
                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_VATPer"].Value != null)
                    mprodvatper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_VATPer"].Value.ToString());
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
                txtItemDiscPercent.Text = mitemdiscper.ToString("#0.00");
                txtItemDiscountAmt.Text = mitemdiscamt.ToString("#0.0000");
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
            int rowindex = 0;
            int mclstk = 0;
            string mstockid = "";
            string prodstockid = "";
            if (mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                prodstockid = mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString();
            int prodsaleqty = 0;
            int prodscmqty = 0;
            string mexpirydate;
            int expdt = 0;
            if (_Mode == OperationMode.Add)
                expdt = Convert.ToInt32(DateTime.Now.Date.ToString("yyyyMMdd"));
            else
                expdt = Convert.ToInt32(_SSSale.CrdbVouDate);
            if (dgvBatchGrid.Rows.Count > 0)
            {
                dgvBatchGrid.Rows.Clear();
                foreach (DataGridViewRow dr in dgvBatchGrid.Rows)
                {
                    dgvBatchGrid.Rows.Remove(dr);
                }
            }
            if (dgvBatchGrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow dr in dgvBatchGrid.Rows)
                {
                    dgvBatchGrid.Rows.Remove(dr);
                }
            }
            try
            {
                dt = invss.GetStockByProductIDForDistributorSale(mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString());
                //  dgvBatchGrid.DataSource = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        mclstk = 0;
                        mexpirydate = string.Empty;
                        if (dr["StockID"] != DBNull.Value && dr["StockID"].ToString() != string.Empty)
                        {
                            mstockid = dr["StockID"].ToString();
                            if (dr["ClosingStock"] != DBNull.Value && dr["ClosingStock"].ToString() != string.Empty)
                                mclstk = Convert.ToInt32(dr["ClosingStock"].ToString());
                            if (dr["ExpiryDate"] != DBNull.Value)
                            {
                                if (dr["ExpiryDate"].ToString() != string.Empty)
                                    mexpirydate = dr["ExpiryDate"].ToString();
                            }
                            if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString() != string.Empty)
                            {
                                prodsaleqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                            }
                            if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value.ToString() != string.Empty)
                            {
                                prodscmqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value.ToString());
                            }
                            if (mclstk > 0 || ((mexpirydate == string.Empty || Convert.ToInt32(mexpirydate) >= expdt)) && (mstockid == prodstockid && _Mode == OperationMode.Edit))
                            {
                                rowindex = dgvBatchGrid.Rows.Add();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_Batchno"].Value = dr["Batchnumber"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_Expiry"].Value = dr["Expiry"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_TradeRate"].Value = dr["TradeRate"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_MRP"].Value = dr["MRP"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_SaleRate"].Value = dr["SaleRate"].ToString();
                                if (mstockid == prodstockid && _Mode == OperationMode.Edit)
                                    dgvBatchGrid.Rows[rowindex].Cells["Col_ClosingStock"].Value = (mclstk + prodsaleqty + prodscmqty).ToString();
                                else
                                    dgvBatchGrid.Rows[rowindex].Cells["Col_ClosingStock"].Value = mclstk.ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_AccountName"].Value = dr["AccName"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_UOM"].Value = dr["ProdLoosePack"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_ExpiryDate"].Value = dr["ExpiryDate"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_PurchaseRate"].Value = dr["PurchaseRate"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_PurchaseVATPer"].Value = dr["PurchaseVATPercent"].ToString();
                                //   dgvBatchGrid.Rows[rowindex].Cells["Col_ProdCSTPer"].Value = dr["ProdCSTPercent"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_ScanCode"].Value = dr["ScanCode"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_ProductID"].Value = dr["ProductID"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_AccountID"].Value = dr["AccountID"].ToString();
                                dgvBatchGrid.Rows[rowindex].Cells["Col_StockID"].Value = dr["StockID"].ToString();
                            }
                        }

                    }
                }
                if (dgvBatchGrid.Rows.Count > 0 && mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                {
                    foreach (DataGridViewRow row in dgvBatchGrid.Rows)
                    {

                        if (row.Cells["Col_StockID"].Value.ToString() == mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString())// _LastStockID)
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
                txtItemDiscountAmt.Text = "0.00";
                txtItemDiscPercent.Text = "0.00";
                txtExpiryDate.Text = "";               
                txtPurchaseRate.Text = "0.00";
                txtSaleRate.Text = "0.00";
                txtAmount.Text = "0.00";
                txtSchemeAmount.Text = "0.00";
                // txtSchemePer.Text = "0.00";
                txtMasterVATAmt.Text = "0.00";
                txtMasterVATPer.Text = "0.00";
                txtCashDisountPerUnit.Text = "0.00";
                txtSplDiscountPerUnit.Text = "0.00";
                txtLastSaleRate.Text = "0.00";
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
                double mvatper = 0;
                double mvatamt = 0;
                string mlastsalestockid = "";
                //  int mqty = 0;
                double mamt = 0;
                double mitemdisc = 0;
                double mitemdiscamt = 0;
                int mscmqty = 0;
                double mscmdisc = 0;
                //double mdistrate = 0;
                int tempscmqty = 0;
                string mbatchno = "";
                try
                {

                    string prodstockid = string.Empty;
                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                        prodstockid = mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString();
                    string batchstockid = string.Empty;
                    if (dgvBatchGrid.CurrentRow.Cells["Col_StockID"].Value != null)
                        batchstockid = dgvBatchGrid.CurrentRow.Cells["Col_StockID"].Value.ToString();
                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                    {
                        if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value != null)
                           int.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                            if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString() != string.Empty)
                            mamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString());
                        mprodno = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                        if (dgvBatchGrid.Rows.Count > 0 && dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value != null)
                        {
                            //if (mamt == 0 || _Mode == OperationMode.Add)
                            //{


                            mexpiry = dgvBatchGrid.CurrentRow.Cells["Col_Expiry"].Value.ToString().Trim();
                            mexpirydate = dgvBatchGrid.CurrentRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                            double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_MRP"].Value.ToString().Trim(), out mmrpn);
                            double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value.ToString().Trim(), out msalerate);
                            double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value.ToString().Trim(), out mtraderate);
                            double.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_VATPer"].Value.ToString(), out mvatper);
                            if (dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value != null)
                                mclosingstock = Convert.ToInt32(dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value.ToString());
                            if (dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value != null)
                                mbatchno = dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value.ToString();
                            if (prodstockid == batchstockid)
                            {
                                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value.ToString() != string.Empty)
                                    mscmqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value.ToString());

                                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value.ToString() != string.Empty)
                                    mitemdisc = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value.ToString());
                                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountAmount"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountAmount"].Value.ToString() != string.Empty)
                                    mitemdiscamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountAmount"].Value.ToString());
                                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString() != string.Empty)
                                    msalerate = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString());
                               // if (mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRatePer"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRatePer"].Value.ToString() != string.Empty)
                               //     mdistrate = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRatePer"].Value.ToString());
                                //mmrpn = Convert.ToDouble(dgvBatchGrid.CurrentRow.Cells["Col_MRP"].Value.ToString());
                                //msalerate = Convert.ToDouble(dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value.ToString());

                            }

                            mlastsalestockid = dgvBatchGrid.CurrentRow.Cells["Col_StockID"].Value.ToString();
                            //}
                            //else
                            //{
                            //    mexpiry = mpMSVC.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim();
                            //    mexpirydate = mpMSVC.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();
                            //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                            //        mbatchno = mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString();
                            //    msalerate = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString());
                            //    mitemdisc = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value.ToString());
                            //    mitemdiscamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountAmount"].Value.ToString());
                            //    mscmqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value.ToString());
                            //    mscmdisc = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                            //    mmrpn = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
                            //    mvatper = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_VATPer"].Value.ToString());
                            //    mvatamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_VATAmount"].Value.ToString());
                            //    if (dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value != null)
                            //        mclosingstock = Convert.ToInt32(dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value.ToString());

                            //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                            //        int.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);
                            //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_VATPer"].Value != null)
                            //        double.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_VATPer"].Value.ToString(), out mvatper);

                            //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value != null)
                            //        int.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);
                            //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString() != string.Empty)
                            //        mamt = Convert.ToDouble(mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value.ToString());

                            //    mlastsalestockid = dgvBatchGrid.CurrentRow.Cells["Col_StockID"].Value.ToString();
                            //}

                        }


                        string mdt = DateTime.Today.Date.ToString("yyyyMMdd");
                        mexpirydate = General.GetShortDateInyyyymmddForm(General.GetValidExpiryDate(mexpiry));
                        if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                        {
                            lblFooterMessage.Text = "Expired Product";
                            PSMessageBox.Show("Product Expired", "Invalid expiry date", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                            mpMSVC.Rows.Remove(mpMSVC.MainDataGridCurrentRow);
                            bool ifblank = false;
                            int currentindex = 0;
                            foreach (DataGridViewRow dr in mpMSVC.Rows)
                            {
                                currentindex = dr.Index;
                                if (dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == "")
                                    ifblank = true;

                            }
                            if (ifblank == false)
                            {
                                int mindex = mpMSVC.Rows.Add();
                                mpMSVC.SetFocus(mindex, 1);
                            }
                            else
                                mpMSVC.SetFocus(currentindex, 1);
                        }
                        else
                        {
                            lblFooterMessage.Text = "";

                            int currentrow = mpMSVC.MainDataGridCurrentRow.Index;
                            int totbatchsale = 0;
                            int totproductsale = 0;
                            int saleqty = 0;
                            int tempproductstock = 0;
                            int tempbatchstock = 0;

                            foreach (DataGridViewRow dr in mpMSVC.Rows)
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
                                            if (dr.Cells["Temp_Scheme"].Value != null)
                                                int.TryParse(dr.Cells["Temp_Quantity"].Value.ToString(), out tempscmqty);
                                            // tempbatchstock += saleqty;
                                        }

                                    }
                                }
                            }


                            mclstk = mclstk + tempproductstock - totproductsale;

                            mclosingstock = mclosingstock + tempbatchstock - totbatchsale;

                            txtProductStock.Text = mclstk.ToString("#0");
                            txtBatchStock.Text = mclosingstock.ToString("#0");

                            lblFooterMessage.Text = "Product Stock :" + mclstk.ToString().Trim() + " : Batch Stock :" + mclosingstock.ToString().Trim();
                            _SSSale.CurrentProductStock = mclstk;
                            _SSSale.CurrentBatchStock = mclosingstock;

                            //  string mallownegativestock = "Y";
                            if (_SSSale.CurrentBatchStock <= 0)
                            {
                                lblFooterMessage.Text = "Batch Stock Zero";

                                mpMSVC.SetFocus(1);
                            }
                            else
                            {
                                dgvBatchGrid.Visible = false;
                                dgvBatchGrid.SendToBack();
                                pnlProductDetail.BringToFront();
                                pnlProductDetail.Visible = true;
                                pnlProductDetail1.Visible = true;
                                pnlProductDetail2.Visible = true;
                                pnlProductDetail.Enabled = true;
                                mpMSVC.Enabled = true;



                                txtStockID.Text = mlastsalestockid.ToString();
                                txtBatch.Text = mbatchno.ToString();
                                txtExpiry.Text = mexpiry.ToString();
                                txtExpiryDate.Text = mexpirydate.ToString();
                                txtMRP.Text = mmrpn.ToString("#0.00");
                                txtSaleRate.Text = msalerate.ToString("#0.00");
                                txtMasterVATPer.Text = mvatper.ToString("#0.00");
                                txtSchemeQuantity.Text = mscmqty.ToString("#0");
                                txtSchemeAmount.Text = mscmdisc.ToString("#0.00");
                                txtItemDiscPercent.Text = mitemdisc.ToString("#0.00");
                                txtItemDiscountAmt.Text = mitemdiscamt.ToString("#0.00");
                                txtMasterVATPer.Text = mvatper.ToString("#0.00");
                                txtMasterVATAmt.Text = mvatamt.ToString("#0.00");
                                txtNewBatch.Text = mbatchno.ToString();
                                txtNewMRP.Text = mmrpn.ToString("#0.00");
                                txtNewSaleRate.Text = msalerate.ToString("#0.00");
                                if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                                {
                                    txtNewBatch.Visible = false;
                                    txtNewMRP.Visible = false;
                                    txtNewSaleRate.Visible = false;
                                    lblLastSaleRate.Visible = false;
                                    txtLastSaleRate.Visible = false;
                                }
                                else
                                {
                                    txtNewBatch.Visible = true;
                                    txtNewMRP.Visible = true;
                                    txtNewSaleRate.Visible = true;
                                    lblLastSaleRate.Visible = true;
                                    txtLastSaleRate.Visible = true;
                                }
                                if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString() != string.Empty)
                                    txtQuantity.Text = mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString();
                                if (MainSaleSubType == FixAccounts.SubTypeForSpecialSale)
                                {
                                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_NewBatch"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_NewBatch"].Value.ToString() != string.Empty)
                                        txtNewBatch.Text = mpMSVC.MainDataGridCurrentRow.Cells["Col_NewBatch"].Value.ToString();
                                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_NewMRP"].Value != null && mpMSVC.MainDataGridCurrentRow.Cells["Col_NewMRP"].Value.ToString() != string.Empty)
                                        txtNewMRP.Text = mpMSVC.MainDataGridCurrentRow.Cells["Col_NewMRP"].Value.ToString();
                                    txtNewSaleRate.Text = msalerate.ToString("#0.00");
                                }
                                _SSSale.CurrentBatchStock = 0;
                              
                                if (dgvBatchGrid.Rows.Count > 0 && dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value != null && dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value.ToString() != string.Empty)
                                    _SSSale.CurrentBatchStock = Convert.ToInt32(Math.Truncate(Convert.ToDouble(dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value)));

                                lblFooterMessage.Text = "Product Stock : " + _SSSale.CurrentProductStock.ToString("#0") + " Batch Stock : " + _SSSale.CurrentBatchStock.ToString("#0");


                                if (dgvBatchGrid.Rows.Count > 0 && dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value != null)
                                {

                                    double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                                    txtTradeRate.Text = mtraderate.ToString("#0.00");
                                }
                                txtQuantity.Focus();

                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());
                }
            }

            //double mtraderate = 0;
            //double mcstper = 0;
            //double mmstper = 0;
            //double mqty = 0;
            //double mmrp = 0;
            //try
            //{
            //    double.TryParse(txtQuantity.Text.ToString(), out mqty);
            //    dgvBatchGrid.Visible = false;
            //    dgvBatchGrid.SendToBack();
            //    pnlProductDetail.BringToFront();
            //    pnlProductDetail.Visible = true;
            //    pnlProductDetail1.Visible = true;
            //    pnlProductDetail2.Visible = true;
            //    pnlProductDetail.Enabled = true;
            //    mpMSVC.Enabled = true;

            //    if (dgvBatchGrid.CurrentRow.Cells["Col_StockID"].Value != null)
            //        txtStockID.Text = dgvBatchGrid.CurrentRow.Cells["Col_StockID"].Value.ToString();
            //    if (dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value != null)
            //        txtBatch.Text = dgvBatchGrid.CurrentRow.Cells["Col_Batchno"].Value.ToString();
            //    if (dgvBatchGrid.CurrentRow.Cells["Col_Expiry"].Value != null)
            //        txtExpiry.Text = dgvBatchGrid.CurrentRow.Cells["Col_Expiry"].Value.ToString();
            //    _SSSale.CurrentBatchStock = 0;
            //    if (dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value != null && dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value.ToString() != string.Empty)
            //    _SSSale.CurrentBatchStock = Convert.ToInt32(Math.Truncate(Convert.ToDouble(dgvBatchGrid.CurrentRow.Cells["Col_ClosingStock"].Value)));
            //    lblFooterMessage.Text = "Product Stock : " + _SSSale.CurrentProductStock.ToString("#0") + " Batch Stock : " + _SSSale.CurrentBatchStock.ToString("#0");
            //    if (dgvBatchGrid.CurrentRow.Cells["Col_MRP"].Value != null)
            //    {
            //        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
            //    }
            //    txtMRP.Text = mmrp.ToString("#0.00");
            //    if (dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value != null)
            //    {

            //        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
            //        txtTradeRate.Text = mtraderate.ToString("#0.00");
            //    }
            //    //if (mpMSVC.MainDataGridCurrentRow.Cells["Col_VATPer"].Value != null)
            //    //{

            //    //    double.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_VATPer"].Value.ToString(), out mmstper);
            //    //    txtMasterVATPer.Text = mmstper.ToString("#0.00");
            //    //    txtMasterVATAmt.Text = Math.Round(mtraderate * mmstper / 100, 2).ToString("#0.00");
            //    //}
            //    //else
            //    //{
            //    //    txtMasterVATPer.Text = "0.00";
            //    //    txtMasterVATAmt.Text = Math.Round(mtraderate * mmstper / 100, 2).ToString("#0.00");
            //    //}
            //    if (dgvBatchGrid.CurrentRow.Cells["Col_PurchaseRate"].Value != null)
            //        txtPurchaseRate.Text = dgvBatchGrid.CurrentRow.Cells["Col_PurchaseRate"].Value.ToString();
            //    if (dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value != null)
            //    {
            //        double sr = Convert.ToDouble(dgvBatchGrid.CurrentRow.Cells["Col_SaleRate"].Value.ToString());
            //        txtSaleRate.Text = sr.ToString("#0.00");
            //    }
            //    if (dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value != null)
            //    {
            //        txtCSTPer.Text = dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value.ToString();
            //        double.TryParse(dgvBatchGrid.CurrentRow.Cells["Col_ProdCSTPer"].Value.ToString(), out mcstper);
            //        txtCSTAmount.Text = Math.Round(mtraderate * mcstper / 100, 2).ToString("#0.00");
            //    }
            //    pnlProductDetail.Enabled = true;
            //    txtAmount.Text = Math.Round(msaler * mqty).ToString("#0.00");
            //    txtQuantity.Focus();
            //   // CalculatePurRateSaleRateAndAmount();
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteError("UclPurchase.dgvBatchGridClick>>" + Ex.Message);
            //}
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
            CloseSchemePanel();
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
            bool retValue = true;
            lblFooterMessage.Text = "";
            double mamt = 0;
            int mqty = 0;
            int mscm = 0;
            int mrepl = 0;
            double mmrp = 0;
            double mtrate = 0;
            double msalerate = 0;
            //  double mvatamt = 0;
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

                if ((mqty + mscm) > _SSSale.CurrentBatchStock)
                {
                    lblFooterMessage.Text = "Please Check Quantity";
                    txtQuantity.Focus();
                    retValue = false;
                }
                else if (msalerate == 0)
                {
                    txtSaleRate.Focus();
                    lblFooterMessage.Text = "Please Sale Rate Quantity";
                    retValue = false;
                }
                else
                {
                    if (mmrp > 0 && mtrate > 0)
                    {
                        if ((mqty > 0 && mamt > 0))
                        {
                            if (mamt > 0 || (mamt == 0 && (mscm > 0 || mrepl > 0)))
                            {
                                retValue = true;
                                //if (mmrp > mtrate)
                                //{
                                //    if (msalerate < (mtrate + mvatamt))
                                //    {
                                //        retValue = false;
                                //        lblFooterMessage.Text = "Sale Rate > Trade Rate + Vat Amount";
                                //    }
                                //}
                                //else
                                //{
                                //    lblFooterMessage.Text = "Mrp > Trade Rate";
                                //    retValue = false;
                                //}
                            }
                            else
                            {
                                lblFooterMessage.Text = "Please Check Quantity,Scheme,Replacement";
                                retValue = false;
                            }
                        }
                        //else
                        //{
                        //    lblFooterMessage.Text = "Please Check Quantity";
                        //    retValue = false;
                        //}

                    }
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
            //  ifok = true;

            lblExpired.Text = "";
            pnlBillDetails.Enabled = true;
            try
            {
                if (ifok)
                {
                    lblFooterMessage.Text = "";
                    CalculateRowAmount();
                    pnlProductDetail.SendToBack();
                    pnlProductDetail.Visible = false;
                    dgvLastPurchase.Visible = false;
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value = txtQuantity.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = txtBatch.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_NewBatchNumber"].Value = txtNewBatch.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = txtExpiry.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = txtExpiryDate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = txtTradeRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value = txtMRP.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_NewMRP"].Value = txtNewMRP.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_PurchaseRate"].Value = txtPurchaseRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = txtSaleRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_NewSaleRate"].Value = txtNewSaleRate.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Scheme"].Value = txtSchemeQuantity.Text.ToString();
                    //  mpMSVC.MainDataGridCurrentRow.Cells["Col_Replacement"].Value = txtReplacement.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountPer"].Value = txtItemDiscPercent.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value = txtAmount.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemDiscountAmount"].Value = txtItemDiscountAmt.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ItemSCMDiscountAmount"].Value = txtSchemeAmount.Text.ToString();
                //    mpMSVC.MainDataGridCurrentRow.Cells["Col_CSTAmount"].Value = txtCSTAmount.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = txtMasterVATPer.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_VATAmount"].Value = txtMasterVATAmt.Text.ToString();
                    //mpMSVC.MainDataGridCurrentRow.Cells["Col_SpldiscountPer"].Value = txtSplDiscPerS.Text.ToString();
                    //mpMSVC.MainDataGridCurrentRow.Cells["Col_SplDiscountAmount"].Value = txtSplDiscountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_CashDiscountAmount"].Value = txtCashDisountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Margin"].Value = txtMargin.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_Margin2"].Value = txtMargin2.Text.ToString();
                    // mpMSVC.MainDataGridCurrentRow.Cells["Col_SplDiscountAmount"].Value = txtSplDiscountPerUnit.Text.ToString();
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].ReadOnly = false;
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value = txtStockID.Text.ToString();
                    //  mpMSVC.MainDataGridCurrentRow.Cells["Col_DistributorSaleRatePer"].Value = 
                    mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductName"].ReadOnly = true;
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
                    //if (_Mode == OperationMode.Add)
                    //{
                    //    DataTable dt = mpMSVC.GetGridData();
                    //    if (dt.Rows.Count > 0)
                    //        dt.WriteXml(General.GetPurchaseTempFile());
                    //}
                    CalculateTotals();
                    //  CalculateAmount(-1);
                    btnOK.Enabled = true;
                }
                //else
                //    btnOK.Enabled = false;

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
            int mqty = 0;
            int mscm = 0;
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
                {
                    mqty = Convert.ToInt32(txtQuantity.Text.ToString());
                    if (txtSchemeQuantity.Text != null && txtSchemeQuantity.Text.ToString() != string.Empty)
                    {
                        mscm = Convert.ToInt32(txtSchemeQuantity.Text.ToString());
                    }
                    if (mscm > 0)
                        txtSchemeAmount.Text = "0.00";
                    if ((mqty + mscm) > _SSSale.CurrentBatchStock)
                        txtSchemeQuantity.Text = "0";
                    if (txtReplacement.Visible == true)
                        txtReplacement.Focus();
                    else
                        txtSaleRate.Focus();
                }
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



        private void mpMSVC_OnTABKeyPressed(object sender, EventArgs e)
        {
            //btnSummary.BackColor = General.ControlFocusColor;
            //btnSummary.Focus();
            btnSummaryClick();
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
            _SSSale.GetPartyOtherDetails(mcbCreditor.SelectedID);
            txtAddress1.Text = _SSSale.PatientAddress1.ToString();
            txtAddress2.Text = _SSSale.PatientAddress2.ToString();
            _SSSale.TransactionType = "3";
            _SSSale.CrdbDiscPer = 0;
            if (mcbCreditor.SeletedItem.ItemData[6] != null)
                _SSSale.TransactionType = mcbCreditor.SeletedItem.ItemData[6].ToString();
            if (mcbCreditor.SeletedItem.ItemData[7] != null && mcbCreditor.SeletedItem.ItemData[7].ToString() != string.Empty)
                _SSSale.CrdbDiscPer = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[7].ToString());
            FillTransactionType();
            cbTransactionType.Focus();
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

            int requiredQty = 0;
            double mrate = 0;
            double mamt = 0;
            double mitemdiscper = 0;
            double mitemdiscamt = 0;
            double mscmdiscamt = 0;
            double mcashdiscper = 0;
            double mcashdiscamt = 0;
            double mvatper = 0;
            double mvatamt = 0;
            int mqty = 0;
            string mbtno = "";
            string mprodno = "";

            //  string prodname = "";
            string mexpirydate = "";
            try
            {
                lblFooterMessage.Text = "";
                if (txtItemDiscPercent.Text != null)
                    mitemdiscper = Convert.ToDouble(txtItemDiscPercent.Text.ToString());
                if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                {
                    if (txtSaleRate.Text != null)
                        mrate = Convert.ToDouble(txtSaleRate.Text.ToString());
                }
                else
                {
                    if (txtNewSaleRate.Text != null)
                        mrate = Convert.ToDouble(txtNewSaleRate.Text.ToString());
                }
                if (txtMasterVATPer.Text != null)
                    mvatper = Convert.ToDouble(txtMasterVATPer.Text.ToString());
                if (txtCashDiscountPerS.Text != null)
                    mcashdiscper = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
                if (txtSchemeAmount.Text != null && txtSchemeAmount.Text.ToString() != string.Empty)
                    mscmdiscamt = Convert.ToDouble(txtSchemeAmount.Text.ToString());

                if (mrate == 0)
                    txtSaleRate.Focus();
                else
                {
                    if (txtQuantity.Text == null || txtQuantity.Text.ToString() == string.Empty || Convert.ToInt32(txtQuantity.Text.ToString()) == 0)
                        txtQuantity.Focus();
                    else
                    {
                        if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                        {
                            if (txtBatch.Text != null)
                                mbtno = txtBatch.Text.ToString();
                        }
                        else
                        {
                            if (txtNewBatch.Text != null)
                                mbtno = txtNewBatch.Text.ToString();
                        }

                        if (mbtno != string.Empty)
                        {
                            string mdt = DateTime.Today.Date.ToString("yyyyMMdd");
                            if (txtExpiryDate.Text != null)
                                mexpirydate = txtExpiryDate.Text.ToString();
                            if (mexpirydate != "" && Convert.ToInt32(mexpirydate) < Convert.ToInt32(mdt))
                            {
                                lblFooterMessage.Text = "Expired Product";
                                PSMessageBox.Show("Product Expired", "Invalid expiry date", PSMessageBoxButtons.OK, PSMessageBoxIcon.Warning, PSMessageBoxButtons.OK);
                                txtQuantity.Text = "0";
                                btnCancel.Focus();
                            }
                            else
                            {
                                requiredQty = Convert.ToInt32(txtQuantity.Text.ToString());
                                if (requiredQty <= 0 || _SSSale.CurrentBatchStock < requiredQty)
                                {
                                    lblFooterMessage.Text = "Enter Quantity";
                                    txtQuantity.Focus();
                                }
                                else
                                {
                                    int mbatchstock = 0;
                                    int oldQuantity = 0;

                                    string mstockid = "";

                                    mprodno = "";
                                    mqty = 0;
                                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                                        mprodno = (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString());
                                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value != null)
                                        int.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchStock"].Value.ToString().Trim(), out mbatchstock);
                                    if (mpMSVC.MainDataGridCurrentRow.Cells["Old_Quantity"].Value != null)
                                        int.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString().Trim(), out oldQuantity);
                                    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                                        mstockid = (mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString());


                                    lblFooterMessage.Text = "";


                                    if (requiredQty <= _SSSale.CurrentBatchStock)
                                    {
                                        mqty = requiredQty;
                                        mamt = Math.Round(mqty * mrate, 2);
                                        double skl = mamt - mscmdiscamt;
                                        mitemdiscamt = Math.Round(skl * mitemdiscper / 100, 2);
                                        mcashdiscamt = Math.Round((skl - mitemdiscamt) * mcashdiscper / 100, 2);
                                        mvatamt = Math.Round((skl - mitemdiscamt - mcashdiscamt) * mvatper / 100, 2);
                                        txtAmount.Text = mamt.ToString("#0.00");
                                        txtItemDiscountAmt.Text = mitemdiscamt.ToString("#0.00");
                                        txtMasterVATAmt.Text = mvatamt.ToString("#0.00");
                                       
                                        //FillBatchStock(ref mmrp, ref mrate, ref mpakn, ref mbtno, ref mprodno, ref mcurrentindex, ref oldmqty, ref mqty);
                                        //mpMSVC.IsAllowNewRow = true;
                                        //mpMSVC.ColumnsMain["Col_Quantity"].ReadOnly = true;
                                    }
                                    else
                                        txtQuantity.Focus();
                                }
                            }
                        }
                        else
                        {

                            mpMSVCOnRowDeleted(mpMSVC.MainDataGridCurrentRow);
                            mpMSVC.Rows.Remove(mpMSVC.MainDataGridCurrentRow);

                        }
                    }
                }

            }
  
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculatePurRateSaleRateAndAmount>>" + Ex.Message);
            }
        }
        private void mpMSVCOnRowDeleted(object sender)
        {
            try
            {
                DataGridViewRow deletedrow = (DataGridViewRow)sender;
                int deletedrowindex = deletedrow.Index;
                CalculateAmount(deletedrowindex);
                lblFooterMessage.Text = "";
                if (_SSSale.AddNewRowCheck(mpMSVC))
                    mpMSVC.Rows.Add();
                mpMSVC.SetFocus(1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        //private void FillBatchStock(ref double mmrp, ref double mrate, ref int mpakn, ref string mbtno, ref string mprodno, ref int mcurrentindex, ref int oldmqty, ref int mqty)
        //{
        //    mcurrentindex = mpMSVC.MainDataGridCurrentRow.Index;
        //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
        //        mprodno = mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();
        //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
        //        mbtno = mpMSVC.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
        //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
        //        double.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
        //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
        //        mqty = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
        //    foreach (DataGridViewRow drp in mpMSVC.Rows)
        //    {
        //        if (drp.Cells["Col_ProductID"].Value != null &&
        //              drp.Cells["Col_BatchNumber"].Value != null &&
        //                 drp.Cells["Col_MRP"].Value != null)
        //        {
        //            if (drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
        //                  drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
        //                     drp.Cells["Col_MRP"].Value.ToString().Trim() == mmrp.ToString("#0.00") && drp.Index != mcurrentindex)
        //            {
        //                if (drp.Cells["Col_Quantity"].Value != null)
        //                    oldmqty = Convert.ToInt32(drp.Cells["Col_Quantity"].Value.ToString());
        //                oldmqty += mqty;
        //                drp.Cells["Col_Quantity"].Value = oldmqty;
        //                mpMSVC.Rows.Remove(mpMSVC.MainDataGridCurrentRow);
        //                break;
        //            }
        //        }
        //    }

        //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_UOM"].Value != null)
        //        mpakn = Convert.ToInt32(mpMSVC.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
        //    if (mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value != null)
        //        double.TryParse(mpMSVC.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value.ToString(), out mrate);
        //    mpMSVC.MainDataGridCurrentRow.Cells["Col_Amount"].Value = ((mrate / mpakn) * mqty).ToString("#0.00");
        //    CalculateAmount(-1);
        //}

        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            CalculateRoundAmount();
            //try
            //{
            //    CalculateAmount(-1);

            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
        }
        private void CalculateAmount(int deletedrowindex)
        {
            lblFooterMessage.Text = "";

            double mTotalAmount = 0;
            //  double mTotalAmountforDiscount = 0;
            //double mTotalAmountVAT5 = 0;
            //double mTotalAmountVAT12 = 0;

            double mvatper = 0;
            //double mvatamount5 = 0;
            //double mvatamount12point5 = 0;
          //  double mtotamtvat0 = 0;

            //double mTvatamount5 = 0;
            //double mTvatamount12point5 = 0;
         //   double mTtotamtvat0 = 0;



            double mrate = 0;
            double mamt = 0;
            double mqty = 0;
            int itemCount = 0;
            

            double mcreditnote = 0;
            double mdebitnote = 0;
            double maddon = 0;
            double mtotamt = 0;

            // 9/12/2014   calculate discount after vat and calculate vat after subtracting vat from amt;
            //double mmyspecialDiscountper = 0;
            //double mmyspecialdiscountamt5 = 0;
            //double mmyspecialdiscountamt12point5 = 0;
            //double mmyspecialdiscountamtzero = 0;
            //double mdiscamt5 = 0;
            //double mdiscamt12point5 = 0;
            //double mdiscamtzero = 0;
            double mdiscper = 0;
            double mnewamt = 0;
            double mnewamtwithoutmydiscount = 0;
            //   double mtotalafterdiscountwithoutmydiscount = 0;
            //double mtotaldiscountamount5 = 0;
            //double mtotaldiscountamount12point5 = 0;
            //double mtotaldiscountamountzero = 0;
            //double mtotalmyspecialdiscamt5 = 0;
            //double mtotalmyspecialdiscamt12point5 = 0;
            //double mtotalmyspecialdiscamtzero = 0;
            double mtotalafterdiscount = 0;
            //string ifdiscount = "Y";
            double mprate = 0;
            double mtraderate = 0;
            double skl = 0;
            double mscmdiscamt = 0;
            //  double mitemdiscper = 0;
            double mitemdiscamt = 0;
            double mvatamt = 0;
            double mcashdiscamt = 0;
            double mcashdiscper = 0;
            double mtotitemdisc = 0;
            double mtotscmdisc = 0;
            double mtotcashdisc = 0;

            _SSSale.GSTAmt0 = 0;
            _SSSale.GSTAmtS5 = 0;
            _SSSale.GSTAmtC5 = 0;
            _SSSale.GSTAmtI5 = 0;
            _SSSale.GSTS5 = 0;
            _SSSale.GSTC5 = 0;
            _SSSale.GSTI5 = 0;
            _SSSale.GSTAmtS12 = 0;
            _SSSale.GSTAmtC12 = 0;
            _SSSale.GSTAmtI12 = 0;
            _SSSale.GSTS12 = 0;
            _SSSale.GSTC12 = 0;
            _SSSale.GSTI12 = 0;
            _SSSale.GSTAmtS18 = 0;
            _SSSale.GSTAmtC18 = 0;
            _SSSale.GSTAmtI18 = 0;
            _SSSale.GSTS18 = 0;
            _SSSale.GSTC18 = 0;
            _SSSale.GSTI18 = 0;
            _SSSale.GSTAmtS28 = 0;
            _SSSale.GSTAmtC28 = 0;
            _SSSale.GSTAmtI28 = 0;
            _SSSale.GSTS28 = 0;
            _SSSale.GSTC28 = 0;
            _SSSale.GSTI28 = 0;

            double mgstamts = 0;
            double mgstamtc = 0;
            double mgsts = 0;
            double mgstc = 0;
            double mgstamt0 = 0;
            double mtta = 0;

            if (txtItemDiscPercent.Text != null && txtItemDiscPercent.Text != string.Empty)
                mdiscper = Convert.ToDouble(txtItemDiscPercent.Text.ToString());
            if (txtAddOnS.Text != null && txtAddOnS.Text.ToString() != string.Empty)
                maddon = Convert.ToDouble(txtAddOnS.Text.ToString());
            double.TryParse(txtCRAmountS.Text.ToString(), out mcreditnote);
            double.TryParse(txtDBAmountS.Text.ToString(), out mdebitnote);
            if (txtCashDiscountPerS.Text != null)
                mcashdiscper = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());

            try
            {
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    //mvatamount5 = 0;
                    //mvatamount12point5 = 0;
                    //mtotamtvat0 = 0;
                    //mdiscamt5 = 0;
                    //mdiscamt12point5 = 0;
                    //mdiscamtzero = 0;
                  //  mmyspecialdiscountamt5 = 0;
                 //   mmyspecialdiscountamt12point5 = 0;
                    mnewamtwithoutmydiscount = 0;
                 //   mmyspecialdiscountamtzero = 0;
                    mnewamt = 0;
                    mitemdiscamt = 0;
                    mcashdiscamt = 0;
                    mscmdiscamt = 0;
                    mscmdiscamt = 0;

                   
                    mgstamts = 0;
                    mgstamtc = 0;
                    mgsts = 0;
                    mgstc = 0;
                    mgstamt0 = 0;
                    mtta = 0;
                    if (dr.Index != deletedrowindex)
                    {
                        if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "0" && dr.Cells["Col_Quantity"].Value.ToString() != "")
                        {
                            //ifdiscount = "Y";
                            if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                                mrate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                            else
                                mrate = Convert.ToDouble(dr.Cells["Col_NewSaleRate"].Value.ToString());
                            mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                          //  if (dr.Cells["Col_IfSaleDisc"].Value != null && dr.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                          //      ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString().ToUpper();
                            mprate = 0;
                            if (dr.Cells["Col_PurchaseRate"].Value != null && (dr.Cells["Col_PurchaseRate"].Value.ToString() != ""))
                                mprate = Convert.ToDouble(dr.Cells["Col_PurchaseRate"].Value.ToString());
                            if (dr.Cells["Col_TradeRate"].Value != null && (dr.Cells["Col_TradeRate"].Value.ToString() != ""))
                                mtraderate = Convert.ToDouble(dr.Cells["Col_TradeRate"].Value.ToString());
                            
                            if (dr.Cells["Col_PurchaseRate"].Value != null && (dr.Cells["Col_PurchaseRate"].Value.ToString() != ""))
                                mprate = Convert.ToDouble(dr.Cells["Col_PurchaseRate"].Value.ToString());
                            if (mprate == 0)
                                mprate = mtraderate;

                            if (dr.Cells["Col_VATPer"].Value != null && dr.Cells["Col_VATPer"].Value.ToString() != string.Empty)
                            {
                                double.TryParse(dr.Cells["Col_VATPer"].Value.ToString(), out mvatper);
                            }

                            if (dr.Cells["Col_VATAmount"].Value != null && dr.Cells["Col_VATAmount"].Value.ToString() != string.Empty)
                            {
                                double.TryParse(dr.Cells["Col_VATAmount"].Value.ToString(), out mvatamt);
                            }
                            if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != string.Empty)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            }
                            if (dr.Cells["Col_ItemDiscountAmount"].Value != null && dr.Cells["Col_ItemDiscountAmount"].Value.ToString() != string.Empty)
                                mitemdiscamt = Convert.ToDouble(dr.Cells["Col_ItemDiscountAmount"].Value.ToString());
                            if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != string.Empty)
                                mscmdiscamt = Convert.ToDouble(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());


                            skl = mamt - mscmdiscamt;
                            mcashdiscamt = Math.Round((skl - mitemdiscamt) * mcashdiscper / 100, 2);
                            double amtforgst = Math.Round((skl - mitemdiscamt - mcashdiscamt), 2);
                            mvatamt = Math.Round((skl - mitemdiscamt - mcashdiscamt) * mvatper / 100, 2);
                            dr.Cells["Col_CashDiscountAmount"].Value = mcashdiscamt.ToString("#0.00");
                            dr.Cells["Col_VATAmount"].Value = mvatamt.ToString("#0.00");

                            mtotitemdisc += mitemdiscamt;
                            mtotcashdisc += mcashdiscamt;
                            mtotscmdisc += mscmdiscamt;

                            //mitemdiscamt = Math.Round(skl * mitemdiscper / 100, 2);
                            //mcashdiscamt = Math.Round((skl - mitemdiscamt) * mcashdiscper / 100, 2);
                            //mvatamt = Math.Round((skl - mitemdiscamt - mcashdiscamt) * mvatper / 100, 2);
                            //txtAmount.Text = mamt.ToString("#0.00");
                            //txtItemDiscountAmt.Text = mitemdiscamt.ToString("#0.00");
                            //txtMasterVATAmt.Text = mvatamt.ToString("#0.00");
                            mtta = amtforgst;
                            dr.Cells["Col_Amount"].Value = (mamt).ToString("#0.00");
                            mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());
                            //double mtt1S = Math.Round((mamt * mvatper) / (100 + mvatper), 4);                           
                            //mtta = mamt - mtt1S;
                            double mtt1S = mvatamt;
                            mgstamts = Math.Round(mtta * (General.CurrentSetting.MsetGSTSPercent / 100), 2);
                            if (General.CurrentSetting.MsetGSTSPercent == 50)
                                mgstamtc = mgstamts;
                            else
                                mgstamtc = mtta - mgstamts;

                            mgsts = Math.Round(mtt1S * (General.CurrentSetting.MsetGSTSPercent / 100), 2);
                            if (General.CurrentSetting.MsetGSTSPercent == 50)
                                mgstc = mgsts;
                            else
                                mgstc = mtta - mgsts;
                            if (General.CurrentSetting.MsetGSTSPercent == 50)
                                mgstamtc = mgstamts;
                            else
                                mgstamtc = mtta - mgstamts;

                            if (mamt > 0)
                            {
                              
                                if (mvatper == 0)
                                {
                                    _SSSale.GSTAmt0 += mtta;
                                }

                                //vat 5.5
                                if (mvatper == 5)
                                {



                                    _SSSale.GSTAmtS5 += mgstamts;
                                    _SSSale.GSTAmtC5 += mgstamtc;

                                    _SSSale.GSTS5 += mgsts;
                                    _SSSale.GSTC5 += mgstc;

                                }
                                else if (mvatper == 12.00)
                                {

                                    _SSSale.GSTAmtS12 += mgstamts;
                                    _SSSale.GSTAmtC12 += mgstamtc;
                                    _SSSale.GSTS12 += mgsts;
                                    _SSSale.GSTC12 += mgstc;
                                    //}
                                    //else
                                    //{
                                    //    _SSSale.GSTAmtI12 += (mgstamts + mgstamtc);
                                    //    _SSSale.GSTI12 += (mgsts + mgstc);
                                    //}

                                }
                                else if (mvatper == 18.00)
                                {
                                    //if (_SSSale.IFOMS != "Y")
                                    //{
                                    _SSSale.GSTAmtS18 += mgstamts;
                                    _SSSale.GSTAmtC18 += mgstamtc;
                                    _SSSale.GSTS18 += mgsts;
                                    _SSSale.GSTC18 += mgstc;
                                    //}
                                    //else
                                    //{
                                    //    _SSSale.GSTAmtI18 += (mgstamts + mgstamtc);
                                    //    _SSSale.GSTI18 += (mgsts + mgstc);
                                    //}
                                }
                                else if (mvatper == 28.00)
                                {
                                    //if (_SSSale.IFOMS != "Y")
                                    //{
                                    _SSSale.GSTAmtS28 += mgstamts;
                                    _SSSale.GSTAmtC28 += mgstamtc;
                                    _SSSale.GSTS28 += mgsts;
                                    _SSSale.GSTC28 += mgstc;
                                    //    }
                                    //    else
                                    //    {
                                    //        _SSSale.GSTAmtI28 += (mgstamts + mgstamtc);
                                    //        _SSSale.GSTI28 += (mgsts + mgstc);
                                    //}


                                }
                                dr.Cells["Col_GSTAmountZero"].Value = mgstamt0;
                                //if (_SSSale.IFOMS != "Y")
                                //{
                                dr.Cells["Col_GSTSAmount"].Value = mgstamts.ToString();
                                dr.Cells["Col_GSTCAmount"].Value = mgstamtc.ToString();
                                dr.Cells["Col_GSTS"].Value = mgsts.ToString();
                                dr.Cells["Col_GSTC"].Value = mgstc.ToString();
                                //}
                                //mtotaldiscountamount5 += mdiscamt5;
                                //mtotaldiscountamount12point5 += mdiscamt12point5;
                                //mtotaldiscountamountzero += mdiscamtzero;
                                //mtotalmyspecialdiscamt5 += mmyspecialdiscountamt5;
                                //mtotalmyspecialdiscamt12point5 += mmyspecialdiscountamt12point5;
                                //mtotalmyspecialdiscamtzero += mmyspecialdiscountamtzero;
                                mnewamt = (mamt );
                                mnewamtwithoutmydiscount = (mamt );


                             //   dr.Cells["Col_VATAmountSale"].Value = (mvatamount12point5 + mvatamount5).ToString("#0.00");

                                mTotalAmount += mamt;
                                mtotalafterdiscount += mnewamt;

                                itemCount += 1;

                              
                                //mTtotamtvat0 += mtotamtvat0;

                            }
                        }
                    }
                }
                NoofRows();
                txtBillAmountS.Text = mTotalAmount.ToString("#0.00");
                txtSchemeDiscountS.Text = mtotscmdisc.ToString("#0.00");
                txtItemDiscountS.Text = mtotitemdisc.ToString("#0.00");               
                txtCashDiscountAmountS.Text = mtotcashdisc.ToString("#0.00");
               


                if (txtCRAmountS.Text != null)
                    mcreditnote = Convert.ToDouble(txtCRAmountS.Text.ToString());
                if (txtDBAmountS.Text != null)
                    mdebitnote = Convert.ToDouble(txtDBAmountS.Text.ToString());




                //    txtAmountforZeroVAT.Text = mTtotamtvat0.ToString("#0.00");
                double gstamt = _SSSale.GSTAmtS5 + _SSSale.GSTAmtS12 + _SSSale.GSTAmtS18 + _SSSale.GSTAmtS28 + _SSSale.GSTAmtC5 + _SSSale.GSTAmtC12 + _SSSale.GSTAmtC18 + _SSSale.GSTAmtC28 + _SSSale.GSTAmtI5 + _SSSale.GSTAmtI12 + _SSSale.GSTAmtI18 + _SSSale.GSTAmtI28;
                double gst = _SSSale.GSTS5 + _SSSale.GSTS12 + _SSSale.GSTS18 + _SSSale.GSTS28 + _SSSale.GSTC5 + _SSSale.GSTC12 + _SSSale.GSTC18 + _SSSale.GSTC28 + _SSSale.GSTI5 + _SSSale.GSTI12 + _SSSale.GSTI18 + _SSSale.GSTI28;
                mtotamt = Math.Round(_SSSale.GSTAmt0 + gstamt + gst  - mtotscmdisc - mtotitemdisc - mtotcashdisc - mdebitnote  + maddon , 2);
                if (mcreditnote < mtotamt)
                {
                    mtotamt = Math.Round(mtotamt - mcreditnote, 2);
                   
                }
                else
                {
                    txtCRAmountS.Text = "0.00";
                    ClearDebitCreditNoteWhenAmountIsLess();
                }
                txtTotalS.Text = mtotamt.ToString("#0.00");
                CalculateFinalSummary();
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
                foreach (DataGridViewRow dr in mpMSVC.Rows)
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
        private void CalculateRoundAmount()
        {
            if (cbRound.Checked == true)
            {
              //  double mmtot = 
                double mtotalafterdiscount = Convert.ToDouble(txtTotalS.Text.ToString());
                txtRoundAmount.Text = Math.Round(Math.Round(mtotalafterdiscount, 0) - Math.Round(mtotalafterdiscount, 2), 2).ToString("#0.00");
                txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalS.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                txtNetAmountS.Text = txtBillAmount.Text;
            }
            else
            {
                txtRoundAmount.Text = "0.00";
                txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalS.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString())), 2).ToString("#0.00");
                txtNetAmountS.Text = txtBillAmount.Text;
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
               //     double mmstamtbySale = 0;
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
                        //if (txtSplDiscPerS.Text != null && txtSplDiscountS.Text.ToString() != string.Empty)
                        //    double.TryParse(txtSplDiscPerS.Text.ToString(), out mspldiscper);
                        //double.TryParse(dr.Cells["Col_VAT"].Value.ToString(), out mpurvatper);
                        double.TryParse(dr.Cells["Col_VATPer"].Value.ToString(), out msalevatper);
                        double.TryParse(txtCashDiscountPerS.Text.ToString(), out mcashdiscper);
                        if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != string.Empty)
                            double.TryParse(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString(), out mscmamt);
                        //double.TryParse(txtSplDiscountS.Text.ToString(), out mspldiscamt);
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
                        //if (General.CurrentSetting.MsetPurchaseAddVATInSaleRate == "Y")
                        //    msalerate = mmrp + mmstamtbySale + mcstamt;
                        //else
                            msalerate = mmrp;
                        mamt = Math.Round(mqty * mtraderate, 2);
                        if (mpurvatper == 0)
                            mamtzerovat = mamt - mitemdiscamt - mscmamt;
                        else
                            mamtzerovat = 0;

                        //if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                        //{
                        //    if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                        //    {
                        //        mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / (mtraderate + mpurvatamt), 2);
                        //        mmargin2 = Math.Round((msalerate - mprate) / mprate, 2);
                        //    }
                        //    else
                        //    {
                        //        mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / msalerate, 2);
                        //        mmargin2 = Math.Round((msalerate - mprate) / msalerate, 2);
                        //    }
                        //}
                        //else
                        //{
                        //    if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                        //    {
                                mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / (mtraderate + mpurvatamt), 2);
                                mmargin2 = Math.Round((msalerate - mprate) / mprate, 2);
                        //    }
                        //    else
                        //    {
                        //        mmargin = Math.Round((msalerate - (mtraderate + mpurvatamt)) / msalerate, 2);
                        //        mmargin2 = Math.Round((msalerate - mprate) / msalerate, 2);
                        //    }
                        //}
                        mmargin = Math.Round(mmargin * 100, 2);
                        mmargin2 = Math.Round(mmargin2 * 100, 2);


                        dr.Cells["Col_ItemDiscountAmount"].Value = mitemdiscamt.ToString("#0.0000");
                        dr.Cells["Col_ItemSCMDiscountAmount"].Value = mscmamt.ToString("#0.00");
                        dr.Cells["Col_VATAmountPurchase"].Value = mpurvatamt.ToString("#0.0000");
                        dr.Cells["Col_VATAmountSale"].Value = msalevatamt.ToString("#0.0000");
                        dr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                        //if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                        //dr.Cells["Col_SaleRate"].Value = msalerate.ToString("#0.00");
                        //else
                        //   dr.C*/ells["Col_NewSaleRate"].Value = 
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
            //  double mmargin = 0;
            //  double mmargin2 = 0;
            double mpurrate = 1;
            double msalerate = 1;
            //   double mvatamt = 0;
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
                    txtNoOfRows.Text = itemCount.ToString().Trim();
                    txtBillAmountS.Text = mtotamt.ToString("#0.00");
                    txtBillAmount.Text = mtotamt.ToString("#0.00");
                }
                //if (General.CurrentSetting.MsetPurchaseAddVATInPurchaseRate == "Y")
                //{
                //    if (General.CurrentSetting.MsetPurchaseMarginByPurchaseRate == "Y")
                //    {
                //        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / (mtraterate + mvatamt), 2);
                //        mmargin2 = Math.Round((msalerate - mpurrate) / mpurrate, 2);
                //    }
                //    else
                //    {
                //        mmargin = Math.Round((msalerate - (mtraterate + mvatamt)) / msalerate, 2);
                //        mmargin2 = Math.Round((msalerate - mpurrate) / msalerate, 2);
                //    }
                //}
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
                //mmargin = Math.Round(mmargin * 100, 2);
                //mmargin2 = Math.Round(mmargin2 * 100, 2);
                //txtMargin.Text = mmargin.ToString("#0.00");
                //txtMargin2.Text = mmargin2.ToString("#0.00");
                if (mtotamt > 0)
                    btnSummary.Enabled = true;
                else
                    btnSummary.Enabled = false;
                // CalculateGetSummaryData();
                CalculateAmount(-1);
                //    CalculateFinalVAT();
                //  CalculateFinalSummary();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateTotals>>" + Ex.Message);
            }

        }

        private void CalculateFinalVAT()
        {
            _SSSale.GSTAmt0 = 0;
            _SSSale.GSTAmtS5 = 0;
            _SSSale.GSTAmtC5 = 0;
            _SSSale.GSTAmtI5 = 0;
            _SSSale.GSTS5 = 0;
            _SSSale.GSTC5 = 0;
            _SSSale.GSTI5 = 0;
            _SSSale.GSTAmtS12 = 0;
            _SSSale.GSTAmtC12 = 0;
            _SSSale.GSTAmtI12 = 0;
            _SSSale.GSTS12 = 0;
            _SSSale.GSTC12 = 0;
            _SSSale.GSTI12 = 0;
            _SSSale.GSTAmtS18 = 0;
            _SSSale.GSTAmtC18 = 0;
            _SSSale.GSTAmtI18 = 0;
            _SSSale.GSTS18 = 0;
            _SSSale.GSTC18 = 0;
            _SSSale.GSTI18 = 0;
            _SSSale.GSTAmtS28 = 0;
            _SSSale.GSTAmtC28 = 0;
            _SSSale.GSTAmtI28 = 0;
            _SSSale.GSTS28 = 0;
            _SSSale.GSTC28 = 0;
            _SSSale.GSTI28 = 0;
            //   double  mgstamt = 0;
            //   double  mgst = 0;
            //   double mtotdisczero = 0;
            //     double mtotdisc5 = 0;
            //    double mtotdisc12point5 = 0;
            //     double mtotdiscother = 0;
            double mtotcashdiscount = 0;
            //     double mmstamt5 = 0;
            //     double mmstamt12point5 = 0;
            //      double mmstamtother = 0;
            //      double mtotmstzero = 0;
            //      double mtotmst5 = 0;
            //      double mtotmst12point5 = 0;
            //      double mtotmstother = 0;
            int mqty = 0;
            double mskl = 0;
            double mscmdisc = 0;
            double mitm = 0;
            double msplddx = 0;
            double mcrddx = 0;
            double mddx = 0;
            double mtt1 = 0;
            double mtt1S = 0;
            double mtta = 0;
            double mmstperpur = 0;
            double mgstamts = 0;
            double mgstamtc = 0;
            double mgsts = 0;
            double mgstc = 0;
            double mgstamt0 = 0;

            double mtotamt = 0;

           
            double mamt = 0;
            double mtotalvat = 0;
            if (txtCashDiscountPerS.Text != "")
                _SSSale.CrdbDiscPer = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
            try
            {

                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    mgstamts = 0;
                    mgstamtc = 0;
                    mgsts = 0;
                    mgstc = 0;
                    mgstamt0 = 0;
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != string.Empty && Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString()) > 0)
                    {
                        int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out mqty);
                        mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        if (dr.Cells["Col_ItemSCMDiscountAmount"].Value != null && dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString() != string.Empty)
                            mscmdisc = Convert.ToDouble(dr.Cells["Col_ItemSCMDiscountAmount"].Value.ToString());
                        if (dr.Cells["Col_VAT"].Value != null && dr.Cells["Col_VAT"].Value.ToString() != "")
                            mmstperpur = Convert.ToDouble(dr.Cells["Col_VAT"].Value.ToString());
                        mskl = Math.Round(mamt - mscmdisc, 2);
                        mitm = Math.Round((mskl * Convert.ToDouble(dr.Cells["Col_ItemDiscountPer"].Value.ToString())) / 100, 2); //4
                        msplddx = 0; // Math.Round(((mskl - mitm) * _SSSale.SpecialDiscountPercentS) / 100, 2); //4
                        mcrddx = 0; // Math.Round(((mskl - mitm) * _SSSale.CreditNoteDiscountPercentS) / 100, 2); //4
                        mcrddx = 0; // ss 19-10-2017
                        mddx = Math.Round(Math.Round((mskl - msplddx - mitm) * _SSSale.CrdbDiscPer, 2) / 100, 2); //4
                        mtta = Math.Round((mamt - mddx - msplddx - mcrddx - mscmdisc - mitm), 2);
                        mtt1 = Math.Round(mtta * (mmstperpur / 100), 2); //4
                        mtt1S = mtt1;
                        mtt1 = Math.Round(mtt1 / mqty, 2); //4
                        mtotalvat += mtt1;
                        dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();
                        dr.Cells["Col_CreditNoteAmount"].Value = mcrddx.ToString();
                        dr.Cells["Col_CashDiscountAmount"].Value = mddx.ToString();
                        mtotcashdiscount += mddx;
                        dr.Cells["Col_VATAmountPurchase"].Value = mtt1.ToString();
                      //  dr.Cells["Col_SplDiscountPer"].Value = _SSSale.SpecialDiscountPercentS.ToString();
                        //   dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();


                        if (mmstperpur == 0)
                        {
                            mgstamt0 = mtta;
                            _SSSale.GSTAmt0 += mtta;
                        }
                        else
                        {
                            mgstamts = Math.Round(mtta * (General.CurrentSetting.MsetGSTSPercent / 100), 2);
                            if (General.CurrentSetting.MsetGSTSPercent == 50)
                                mgstamtc = mgstamts;
                            else
                                mgstamtc = mtta - mgstamts;
                            mgsts = Math.Round(mtt1S * (General.CurrentSetting.MsetGSTSPercent / 100), 2);
                            if (General.CurrentSetting.MsetGSTSPercent == 50)
                                mgstc = mgsts;
                            else
                                mgstc = mtta - mgsts;
                        }
                        mtotamt += mgstamts + mgstamtc + mgsts + mgstc;
                        //vat 5.5
                        if (mmstperpur == 5)
                        {
                            _SSSale.GSTAmtS5 += mgstamts;
                            _SSSale.GSTAmtC5 += mgstamtc;

                            _SSSale.GSTS5 += mgsts;
                            _SSSale.GSTC5 += mgstc;
                        }
                        else if (mmstperpur == 12.00)
                        {
                            _SSSale.GSTAmtS12 += mgstamts;
                            _SSSale.GSTAmtC12 += mgstamtc;
                            _SSSale.GSTS12 += mgsts;
                            _SSSale.GSTC12 += mgstc;

                        }
                        else if (mmstperpur == 18.00)
                        {
                            _SSSale.GSTAmtS18 += mgstamts;
                            _SSSale.GSTAmtC18 += mgstamtc;
                            _SSSale.GSTS18 += mgsts;
                            _SSSale.GSTC18 += mgstc;
                        }
                        else if (mmstperpur == 28.00)
                        {
                            _SSSale.GSTAmtS28 += mgstamts;
                            _SSSale.GSTAmtC28 += mgstamtc;
                            _SSSale.GSTS28 += mgsts;
                            _SSSale.GSTC28 += mgstc;
                        }

                    }
                    dr.Cells["Col_GSTAmountZero"].Value = mgstamt0;
                    dr.Cells["Col_GSTSAmount"].Value = mgstamts.ToString();
                    dr.Cells["Col_GSTCAmount"].Value = mgstamtc.ToString();
                    dr.Cells["Col_GSTS"].Value = mgsts.ToString();
                    dr.Cells["Col_GSTC"].Value = mgstc.ToString();




                }
                txtCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
              //  txtPreCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
                txtNetAmountS.Text = mtotamt.ToString("#0.00");

                FillGSTpnl();

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
            btnSummaryClick();
        }

        private void btnSummaryClick()
        {
            DataTable dt = new DataTable();
            try
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                {

                    txtDBAmountS.Text = "0.00";
                    txtCRAmountS.Text = "0.00";
                    CalculateAmount(-1);
                    dt = FillCreditDebitNote();
                    pnlProductDetail.Visible = false;
                    dgvLastPurchase.Visible = false;
                    pnlBillDetails.Enabled = false;
                    mpMSVC.Enabled = false;
                  pnlSummary.Location = GetpnlSummaryLocation();
                    pnlSummary.BringToFront();
                    pnlSummary.Visible = true;
                    pnlDebitCreditNote.BringToFront();
                    dgCreditNote.Visible = true;
                    if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView || _SSSale.StatementNumber > 0)
                        pnlSummary.Enabled = false;
                    else
                        pnlSummary.Enabled = true;
                    //    CalculateGetSummaryData();
                    txtCRAmountS.Focus();
                    btnSummary.Enabled = false;
                    if (_SSSale.IFOMS != "Y")
                    {
                        pnlGST.Location = GetpnlGSTLocation();
                        pnlGST.BringToFront();
                        pnlGST.Visible = true;
                        pnlIGST.Visible = false;
                    }
                    else
                    {
                        pnlIGST.Location = GetpnlGSTLocation();
                        pnlIGST.BringToFront();
                        pnlIGST.Visible = true;
                        pnlGST.Visible = false;
                    }
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                    {

                        //////if (dt != null && dt.Rows.Count > 0)
                        //////{
                        //////    pnlDebitCreditNote.BringToFront();
                        //////    pnlDebitCreditNote.Visible = true;
                        //////    dgCreditNote.Visible = true;
                        //////    lblFooterMessage.Text = "Press Space Bar to Select unSelect Credit Debit Note";
                        //////    pnlDebitCreditNote.Select();
                        //////    if (_Mode == OperationMode.View)
                        //////        btnCRDBOK.Focus();
                        //////    else
                        //////        dgCreditNote.Focus();

                        //////}
                    }                   
                    if (pnlDebitCreditNote.Visible == false)
                    {
                        tsBtnSave.Enabled = true;
                    }
                    //  CalculateFinalVAT();
                    //    CalculateFinalSummary();
                    //if (_SSSale.StatementNumber > 0)
                    //    tsBtnSave.Enabled = false;
                }
                else
                {
                    pnlProductDetail.Visible = false;
                    dgvLastPurchase.Visible = false;
                    pnlBillDetails.Enabled = false;
                    mpMSVC.Enabled = false;
                   pnlSummary.Location =   GetpnlSummaryLocation();
                   pnlGST.Location =  GetpnlGSTLocation();
                    pnlSummary.BringToFront();
                    pnlSummary.Enabled = false;
                    //   CalculateGetSummaryData();
                    //   CalculateFinalSummary();
                    txtCRAmountS.Focus();
                    btnSummary.Enabled = false;
                    pnlSummary.Visible = true;
                    pnlGST.BringToFront();
                    if (_SSSale.IFOMS != "Y")
                    {
                        pnlGST.BringToFront();
                        //     pnlGST.Enabled = false;
                        pnlGST.Visible = true;
                        pnlIGST.Visible = false;
                    }
                    else
                    {
                        pnlIGST.BringToFront();
                        pnlIGST.Visible = true;
                        pnlGST.Visible = false;
                    }
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
           // bool retValue = false;
            DataTable dt = new DataTable();
            try
            {
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    ConstructCreditNoteColumns();
                    dgCreditNote.DoubleColumnNames.Add("Col_AmountNet");
                    CreditNoteStock crdb = new CreditNoteStock();

                    dt = crdb.GetOverviewDataForDebtorSale(mcbCreditor.SelectedID, _SSSale.Id);
                    // if (dt != null)
                    //   retValue = BindCreditNoteDebitNote(dt);

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
                pnlGST.Visible = false;
                pnlSummary.Visible = false;
             //   pnlSummary.SendToBack();
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
                Log.WriteException(Ex);
            }
        }

        public void CalculateTotalVATAmount()
        {
            //    if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            //    {


            //        double mtotvat5 = 0;
            //        double mtotvat12point5 = 0;
            //        double mtotvat = 0;
            //        try
            //        {
            //            if (txtVAT5AmountS.Text != null && txtVAT5AmountS.Text.ToString() != "")
            //                double.TryParse(txtVAT5AmountS.Text.ToString(), out mtotvat5);
            //            if (txtVAT12Point5AmountS.Text != null && txtVAT12Point5AmountS.Text.ToString() != "")
            //                double.TryParse(txtVAT12Point5AmountS.Text.ToString(), out mtotvat12point5);
            //            mtotvat = Math.Round(mtotvat5, 2) + Math.Round(mtotvat12point5, 2);
            //            txtTotalVATAmountS.Text = (mtotvat).ToString("0.00");
            //        }
            //        catch (Exception Ex)
            //        {
            //            Log.WriteError("UclPurchase.CalculateTotalVATAmount>>" + Ex.Message);
            //        }
            //    }
        }

        private void uclSubstituteControl1_OnProductSelected(string productID)
        {
            mpMSVC.LoadProduct(productID);
            mpMSVC.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productID;
        }
      
        private void btnSubstitute_Click()
        {
            uclSubstituteControl1.Initialize();
            uclSubstituteControl1.Visible = true;
            uclSubstituteControl1.BringToFront();
            uclSubstituteControl1.Select();
            uclSubstituteControl1.Focus();
        }

        #endregion

        #region Events
        private void txtAddOnS_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    CalculateFinalSummary();
                    mcbTransporter.Focus();
                }
                else if (e.KeyCode == Keys.Up)
                    txtCashDiscountPerS.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtAddOnS_KeyDown>>" + Ex.Message);
            }
        }
        private void FillGSTpnl()
        {
            txtSPUR5.Text = _SSSale.GSTAmtS5.ToString("#0.00");
            txtSPUR12.Text = _SSSale.GSTAmtS12.ToString("#0.00");
            txtSPUR18.Text = _SSSale.GSTAmtS18.ToString("0.00");
            txtSPUR28.Text = _SSSale.GSTAmtS28.ToString("0.00");
            txtCPUR5.Text = _SSSale.GSTAmtC5.ToString("#0.00");
            txtCPUR12.Text = _SSSale.GSTAmtC12.ToString("#0.00");
            txtCPUR18.Text = _SSSale.GSTAmtC18.ToString("0.00");
            txtCPUR28.Text = _SSSale.GSTAmtC28.ToString("0.00");

            txtSGST5.Text = _SSSale.GSTS5.ToString("0.00");
            txtSGST12.Text = _SSSale.GSTS12.ToString("0.00");
            txtSGST18.Text = _SSSale.GSTS18.ToString("0.00");
            txtSGST28.Text = _SSSale.GSTS28.ToString("0.00");

            txtCGST5.Text = _SSSale.GSTC5.ToString("0.00");
            txtCGST12.Text = _SSSale.GSTC12.ToString("0.00");
            txtCGST18.Text = _SSSale.GSTC18.ToString("0.00");
            txtCGST28.Text = _SSSale.GSTC28.ToString("0.00");

            txtIGST5.Text = _SSSale.GSTI5.ToString("#0.00");
            txtIGST12.Text = _SSSale.GSTI12.ToString("#0.00");
            txtIGST18.Text = _SSSale.GSTI18.ToString("#0.00");
            txtIGST28.Text = _SSSale.GSTI28.ToString("#0.00");

            txtIPUR0.Text = _SSSale.GSTAmt0.ToString("#0.00");
            txtIPUR5.Text = _SSSale.GSTAmtI5.ToString("#0.00");
            txtIPUR12.Text = _SSSale.GSTAmtI12.ToString("#0.00");
            txtIPUR18.Text = _SSSale.GSTAmtI18.ToString("#0.00");
            txtIPUR28.Text = _SSSale.GSTAmtI28.ToString("#0.00");
        }
        private void CalculateFinalSummary()
        {
            _SSSale.CrdbAmount = Convert.ToDouble(txtBillAmountS.Text.ToString());
            try
            {

                if (_SSSale.CrdbAmount > 0)
                {
                    if (txtSchemeDiscountS.Text.ToString().Trim() != "")
                        _SSSale.SchemeTotalDiscount = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
                    if (txtItemDiscountS.Text.ToString().Trim() != "")
                        _SSSale.ItemTotalDiscount = Convert.ToDouble(txtItemDiscountS.Text.ToString());
                    //if (txtSplDiscountS.Text.ToString().Trim() != "")
                    //    _SSSale.AmountSpecialDiscountS = Convert.ToDouble(txtSplDiscountS.Text.ToString());

                    //_SSSale.SpecialDiscountPercentS = Math.Round((100 * _SSSale.AmountSpecialDiscountS) / (_SSSale.AmountBillS - _SSSale.AmountItemDiscountS - _SSSale.AmountSchemeDiscountS - _SSSale.AmountOctroiS), 6);

                    if (txtAddOnS.Text.ToString().Trim() != "")
                        _SSSale.CrdbAddOn = Convert.ToDouble(txtAddOnS.Text.ToString());
                    if (txtCRAmountS.Text.ToString().Trim() != "")
                        _SSSale.CrNoteAmount = Convert.ToDouble(txtCRAmountS.Text.ToString());
                    if (txtDBAmountS.Text.ToString().Trim() != "")
                        _SSSale.DbNoteAmount = Convert.ToDouble(txtDBAmountS.Text.ToString());
                    if (txtCashDiscountPerS.Text.ToString().Trim() != "")
                        _SSSale.CrdbDiscPer = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
                    if (txtCashDiscountAmountS.Text.ToString().Trim() != "")
                        _SSSale.CrdbDiscAmt = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
                   
                    // scheme discount ??????????????????
                    if (_SSSale.CrdbAmount - _SSSale.ItemTotalDiscount - _SSSale.CrNoteAmount - _SSSale.SchemeTotalDiscount - _SSSale.CrdbDiscAmt + _SSSale.DbNoteAmount < 0)
                    {
                        lblFooterMessage.Text = "Invalid Debit Note Amount";
                        _SSSale.DbNoteAmount = 0;
                        txtDBAmountS.Text = "0.00";
                        ClearDebitCreditNoteWhenAmountIsLess();
                    }
                    //if (_SSSale.CrdbDiscAmt > (_SSSale.CrdbAmount - _SSSale.CrdbAmountNet - -_SSSale.ItemTotalDiscount + _SSSale.CrNoteAmount - _SSSale.DbNoteAmount - _SSSale.SchemeTotalDiscount))
                    //{
                    //    lblFooterMessage.Text = "Invalid Cash Discount";
                    //    _SSSale.CrdbDiscPer = 0;
                    //    _SSSale.CrdbDiscAmt = 0;
                    //    txtCashDiscountAmountS.Text = "0.00";
                    //    //txtPreCashDiscountAmountS.Text = "0.00";
                    //}
                    //txtCashDiscountAmountS.Text = _SSSale.CrdbDiscAmt.ToString("#0.00");
                    //txtPreCashDiscountAmountS.Text = _SSSale.CrdbDiscAmt.ToString("#0.00");
                    double gst =  _SSSale.GSTS5 +  _SSSale.GSTS12 + _SSSale.GSTS18 + _SSSale.GSTS28 + _SSSale.GSTC5 + _SSSale.GSTC12 + _SSSale.GSTC18 + _SSSale.GSTC28 + _SSSale.GSTI5 + _SSSale.GSTI12 + _SSSale.GSTI18 + _SSSale.GSTI28;
                    if (txtCashDiscountAmountS.Text.ToString().Trim() != "")
                        _SSSale.CrdbDiscAmt = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
                    double gstamt = _SSSale.GSTAmt0 + _SSSale.GSTAmtS5 + _SSSale.GSTAmtS12 + _SSSale.GSTAmtS18 + _SSSale.GSTAmtS28 + _SSSale.GSTAmtC5 + _SSSale.GSTAmtC12 + _SSSale.GSTAmtC18 + _SSSale.GSTAmtC28 + _SSSale.GSTAmtI5 + _SSSale.GSTAmtI12 + _SSSale.GSTAmtI18 + _SSSale.GSTAmtI28;
                    _SSSale.CrdbAmountNet = Math.Round(gstamt + gst
                        + _SSSale.CrdbAddOn - _SSSale.CrNoteAmount
                        + _SSSale.DbNoteAmount - _SSSale.CrdbDiscAmt ,2);
                    txtTotalS.Text = _SSSale.CrdbAmountNet.ToString("#0.00");
                    FillGSTpnl();
                    CalculateRoundAmount();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateFinalSummary>>" + Ex.Message);
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
                {
                    txtCashDiscountPerS.Focus();
                }
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
                // string ss = mpMSVC.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString();
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                    mpMSVC.SetFocus(1);
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
                    CalculateAmount(-1);
                    CalculateFinalSummary();
                    txtAddOnS.Focus();
                }
                else if (e.KeyCode == Keys.Up)
                    txtDBAmountS.Focus();
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
                    //else
                    //{
                    //    CalculatePurRateSaleRateAmountforFullGrid();
                    //}
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

        //private void txtCashDiscountAmountS_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
        //        {

        //            CalculateAmount(-1);
        //            CalculateFinalSummary();
        //        }
        //        else if (e.KeyCode == Keys.Up)
        //        {
        //            CalculateFinalSummary();
        //            txtCashDiscountAmountS.Focus();
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPurchase.txtOCTPerS_KeyDown>>" + Ex.Message);
        //    }
        //}      
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
                        if (mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteStock || mvoutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                            mcrnoteamt += mamt;
                        else if (mvoutype == FixAccounts.VoucherTypeForDebitNoteStock || mvoutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                            mdbnoteamt += mamt;
                    }
                }
                txtCRAmountS.Text = mcrnoteamt.ToString("#0.00");
                txtDBAmountS.Text = mdbnoteamt.ToString("#0.00");
                pnlDebitCreditNote.Visible = false;
                CalculateFinalSummary();
                tsBtnSave.Enabled = true;
                pnlGST.BringToFront();
                pnlGST.Visible = true;
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
        //private void mpMSVC_OnTABKeyPressed(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        btnSummary.BackColor = Color.Bisque;
        //        btnSummary.Focus();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPurchase.mpMSVC_OnTABKeyPressed>>" + Ex.Message);
        //    }
        //}

        //private void mpMSVC_OnRowDeleted(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CalculateTotals();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteError("UclPurchase.mpMSVC_OnRowDeleted>>" + Ex.Message);
        //    }
        //}



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
                    //if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
                    //    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
                    cbNewTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                    cbNewTransactionType.SelectedIndex = cbNewTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                }
                else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                {
                    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
                    //if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
                    //    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
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
                Log.WriteError("UclDistributorSale.btnTypeChange_Click>>" + Ex.Message);
            }
        }



        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

        }


        private void mcbCreditor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
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

        //private void btnOK_Enter(object sender, EventArgs e)
        //{
        //    btnOK.BackColor = General.ControlFocusColor;
        //}

        //private void btnOK_Leave(object sender, EventArgs e)
        //{
        //    btnOK.BackColor = Color.White;
        //}

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
                //  frmView.Icon = EcoMart.Properties.Resources.Icon;
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

     

        private void cbTransactionType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mcbSalesman.Focus();
                FixVoucherTypeBycbTransactionType();
            }
            if (e.KeyCode == Keys.Up)
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
            else if (colIndex == 19) //quantity
                CalculateRowAmount();
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
                if (txtLastSaleRate.Text != null && txtLastSaleRate.Text.ToString() != string.Empty)
                    mdistper = Convert.ToDouble(txtLastSaleRate.Text.ToString());
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

        #region UiEvents

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                pnlScheme.Visible = false;
                bool ifscheme =  FillSchemeData();
                if (ifscheme == true)
                {
                    dgScheme.ClearSelection();
                    btnScmYes.Visible = true;
                    btnScmNO.Visible = true;
                    if ((txtSchemeQuantity.Text == null || txtSchemeQuantity.Text.ToString() == string.Empty || Convert.ToInt32(txtSchemeQuantity.Text.ToString()) == 0) && (txtSchemeAmount.Text == null || txtSchemeAmount.Text == string.Empty || Convert.ToDouble(txtSchemeAmount.Text.ToString()) == 0))
                        btnScmYes.Focus();
                    else
                        btnScmNO.Focus();

                }
                else
                {
                    if (txtNewBatch.Visible == true)
                        txtNewBatch.Focus();
                    else
                        txtItemDiscPercent.Focus();
                }
            }
        }
        private void txtQuantityValidating()
        {
            int mqty = 0;
            if (txtQuantity.Text != null && txtQuantity.Text.ToString() != string.Empty)
                mqty = Convert.ToInt32(txtQuantity.Text.ToString());
            if (mqty >= 0 && mqty <= _SSSale.CurrentBatchStock)
            {
                lblFooterMessage.Text = "";
                CalculateRowAmount();
                txtSaleRate.Focus();
            }
            else
            {
                lblFooterMessage.Text = "Please Check Quantity";
                txtQuantity.Focus();
            }
        }
        private void txtSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                CalculateRowAmount();

              
                txtSchemeQuanityGiven.Text = "0";
                txtSchemeAmountGiven.Text = "0.00";
                string mprod = mpMSVC.MainDataGridCurrentRow.Cells[0].Value.ToString();
                _SchemeData = _SSSale.GetSchemeDetails(mprod);
                try
                {
                    if (_SchemeData != null)
                    {
                        if (txtSchemeQuantity.Text == null || txtSchemeQuantity.Text.ToString() == string.Empty || txtSchemeQuantity.Text == "0")
                        {
                            FillSchemeData();
                            pnlScheme.Visible = true;
                            pnlScheme.Focus();
                            
                        }
                        else
                            txtItemDiscPercent.Focus();
                    }
                    else
                    {
                        txtItemDiscPercent.Focus();
                    }

                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
        }

        private bool FillSchemeData()
        {
            _selectedScmQuantity = 0;
            _selectedScmSaleQuantity = 0;
            int prodqty = 0;
            int schemeqty = 0;
            bool retValue = false;
            try
            {

                if (_SchemeData != null)
                {
                    retValue = true;
                    if (_SchemeData["ProductQuantity1"] != DBNull.Value && _SchemeData["ProductQuantity1"].ToString() != string.Empty)
                        prodqty = Convert.ToInt32(_SchemeData["ProductQuantity1"].ToString());
                    if (_SchemeData["SchemeQuantity1"] != DBNull.Value && _SchemeData["SchemeQuantity1"].ToString() != string.Empty)
                        schemeqty = Convert.ToInt32(_SchemeData["SchemeQuantity1"].ToString());
                    ConstructSchemeColumns();                 
                    dgScheme.ColumnsMain["Col_Selected"].Visible = false;
                    int rowIndex = dgScheme.Rows.Add();

                    dgScheme.Rows[rowIndex].Cells["Col_Selected"].Value = string.Empty;
                    dgScheme.Rows[rowIndex].Cells["Col_ScmText1"].Value = "Scheme - 1  Qty:";
                    dgScheme.Rows[rowIndex].Cells["Col_ScmProductQuantity"].Value = prodqty.ToString("#0");
                    dgScheme.Rows[rowIndex].Cells["Col_ScmText2"].Value = "Free";
                    dgScheme.Rows[rowIndex].Cells["Col_ScmSchemeQuantity"].Value = schemeqty.ToString("#0");
                    prodqty = 0;
                    schemeqty = 0;
                    if (_SchemeData["ProductQuantity2"] != DBNull.Value && _SchemeData["ProductQuantity2"].ToString() != string.Empty)
                        prodqty = Convert.ToInt32(_SchemeData["ProductQuantity2"].ToString());
                    if (_SchemeData["SchemeQuantity2"] != DBNull.Value && _SchemeData["SchemeQuantity2"].ToString() != string.Empty)
                        schemeqty = Convert.ToInt32(_SchemeData["SchemeQuantity2"].ToString());
                    rowIndex = dgScheme.Rows.Add();

                    dgScheme.Rows[rowIndex].Cells["Col_Selected"].Value = string.Empty;
                    dgScheme.Rows[rowIndex].Cells["Col_ScmText1"].Value = "Scheme - 2  Qty:";
                    dgScheme.Rows[rowIndex].Cells["Col_ScmProductQuantity"].Value = prodqty.ToString("#0");
                    dgScheme.Rows[rowIndex].Cells["Col_ScmText2"].Value = "Free";
                    dgScheme.Rows[rowIndex].Cells["Col_ScmSchemeQuantity"].Value = schemeqty.ToString("#0");
                    prodqty = 0;
                    schemeqty = 0;
                    if (_SchemeData["ProductQuantity3"] != DBNull.Value && _SchemeData["ProductQuantity3"].ToString() != string.Empty)
                        prodqty = Convert.ToInt32(_SchemeData["ProductQuantity3"].ToString());
                    if (_SchemeData["SchemeQuantity3"] != DBNull.Value && _SchemeData["SchemeQuantity3"].ToString() != string.Empty)
                        schemeqty = Convert.ToInt32(_SchemeData["SchemeQuantity3"].ToString());
                    rowIndex = dgScheme.Rows.Add();

                    dgScheme.Rows[rowIndex].Cells["Col_Selected"].Value = string.Empty;
                    dgScheme.Rows[rowIndex].Cells["Col_ScmText1"].Value = "Scheme - 3  Qty:";
                    dgScheme.Rows[rowIndex].Cells["Col_ScmProductQuantity"].Value = prodqty.ToString("#0");
                    dgScheme.Rows[rowIndex].Cells["Col_ScmText2"].Value = "Free";
                    dgScheme.Rows[rowIndex].Cells["Col_ScmSchemeQuantity"].Value = schemeqty.ToString("#0");
                    lblFromDate.Text = General.GetDateInShortDateFormat(_SchemeData["StartingDate"].ToString());
                    lblToDate.Text = General.GetDateInShortDateFormat(_SchemeData["ClosingDate"].ToString());

                    pnlScheme.BringToFront();
                    lblscmIsDataOk.Visible = false;
                    txtscmIsDataOK.Visible = false;
                    lblscmDoyouwanttoenterscheme.Visible = true;
                    btnscmisdataokNO.Visible = false;
                    btnscmisdataokYES.Visible = false;
                 //   txtscmDoyouWanttoEnterScheme.Visible = true;
                    pnlScheme.Visible = true;                  
                    btnScmYes.Focus();

                }
                else
                {
                    if (dgScheme.Rows.Count > 0)
                        dgScheme.Rows.Clear();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
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
       

       

        private void CalculateScheme(int saleqty, int scm )
        {
            
            int msaleQty = saleqty;
            int mscm = scm;
            int mqty = Convert.ToInt32(txtQuantity.Text.ToString());
            int mscmqty = 0;
            double mrate = 0;
            double scmx = 0;
            double mscmamt = 0;
            if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                mrate = Convert.ToDouble(txtSaleRate.Text.ToString());
            else
                mrate = Convert.ToDouble(txtNewSaleRate.Text.ToString());
            double mmsaleqty = 0;
            mmsaleqty = (mqty * mscm) / msaleQty;
            mmsaleqty = Math.Floor(mmsaleqty);
            if ((Math.Floor(mmsaleqty) - (mqty * mscm / msaleQty) == 0))
                mscmqty = Convert.ToInt32(mmsaleqty);
            else
                mscmqty = 0;          
            if (msaleQty + mscm > 0)
            {
                scmx = Math.Round((mrate * msaleQty) / (msaleQty + mscm), 2);
            }
            double scmy = Math.Round(mrate - scmx, 2);
            mscmamt = Math.Round(mqty * scmy, 2);
            if ((mqty + mscmqty) > _SSSale.CurrentBatchStock)
                mscmqty = 0;
            txtSchemeAmountGiven.Text = mscmamt.ToString("#0.00");
            txtSchemeQuanityGiven.Text = mscmqty.ToString("#0");        
            txtprescheme.Text = mscmqty.ToString("#0");
            txtSchemeQuanityGiven.Focus();
            lblscmIsDataOk.Visible = true;
            btnScmYes.Visible = false;
            btnScmNO.Visible = false;
            btnscmisdataokYES.Visible = true;
            btnscmisdataokNO.Visible = true;      
            lblscmDoyouwanttoenterscheme.Visible = false;           
        }
        private bool ValidateScheme()
        {
            bool retValue = false;
            if (Convert.ToDouble(txtSchemeAmountGiven.Text.ToString()) == 0 && Convert.ToInt32(txtSchemeQuanityGiven.Text.ToString()) == 0)
                retValue = true;
            if (Convert.ToInt32(txtSchemeQuanityGiven.Text.ToString()) > 0)
            {
                if (Convert.ToDouble(txtSchemeAmountGiven.Text.ToString()) == 0)
                    retValue = true;
            }
            else if (Convert.ToDouble(txtSchemeAmountGiven.Text.ToString()) > 0)
            {
                if (Convert.ToInt32(txtSchemeQuanityGiven.Text.ToString()) == 0)
                    retValue = true;
            }
            return retValue;
        }

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

       

        private void SchemeSelected()
        {
            // CalculateScheme();
            txtSchemeQuanityGiven.Focus();
            //gbScheme.Enabled = false;
            //gbGivenScheme.Enabled = true;
            //if (Convert.ToInt32(btnGivenSchemeQuantity.Text.ToString()) > 0)
            //{
            //    btnGivenSchemeQuantity.Focus();
            //    btnGivenSchemeQuantity.Enabled = true;
            //}
            //else
            //{
            //    btnGivenSchemeQuantity.Enabled = false;
            //    btnGivenSchemeAmount.Focus();
            //}
        }

       
        private void btnSchemeSave_Click(object sender, EventArgs e)
        {
            btnSchemeSaveClick();
        }
        private void btnSchemeSaveClick()
        {
            pnlScheme.Visible = false;
            txtSchemeQuantity.Text = txtSchemeQuanityGiven.Text.ToString();
            txtSchemeAmount.Text = txtSchemeAmountGiven.Text.ToString();
            txtItemDiscPercent.Focus();
            CalculateRowAmount();
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
            txtSchemeQuanityGiven.Text = "0";
            txtSchemeAmountGiven.Text = "0.00";
            txtItemDiscPercent.Focus();
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
        private void txtItemDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                txtItemDiscountAmt.Text = "0.00";
                CalculateRowAmount(); ;
                btnOK.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtSaleRate.Focus();
        }
        private void txtSchemeQuanityGiven_KeyDown(object sender, KeyEventArgs e)
        {
            int mscm = 0;
            int mqty = 0;
            int prescm = 0;
            double mschemeamtgiven = 0;
            if (txtSchemeAmountGiven.Text != null && txtSchemeAmountGiven.Text.ToString() != string.Empty && Convert.ToDouble(txtSchemeAmountGiven.Text.ToString()) > 0)
                mschemeamtgiven = Convert.ToDouble(txtSchemeAmountGiven.Text.ToString());
            if (e.KeyCode == Keys.Enter)
            {
                
                if (txtQuantity.Text != string.Empty)
                    mqty = Convert.ToInt32(txtQuantity.Text.ToString());
                if (txtprescheme.Text != null && txtprescheme.Text.ToString() != string.Empty)
                    prescm = Convert.ToInt32(txtprescheme.Text.ToString());

                if (txtSchemeQuanityGiven.Text == string.Empty)
                    txtSchemeQuanityGiven.Text = "0";
                mscm = Convert.ToInt32(txtSchemeQuanityGiven.Text.ToString());

                if (mscm > 0 && mscm != prescm)
                {
                    mscm = prescm;
                    txtSchemeAmountGiven.Text = mscm.ToString("#0.00");
                }

                if (mqty > 0 && mscm > 0)
                {
                    if ((mqty + mscm) <= _SSSale.CurrentBatchStock)
                        txtSchemeAmountGiven.Text = "0.00";
                    else
                        txtSchemeQuanityGiven.Text = "0";
                }
               btnscmisdataokYES.Focus();
            }
        }
        private void btnDbCrCancel_Click(object sender, EventArgs e)
        {
            btnDbCrCancelClick();
        }
        private void btnDbCrCancelClick()
        {
            foreach (DataGridViewRow dr in dgCreditNote.Rows)
            {
                dr.Cells["Col_Check"].Value = false;
            }
            //txtCRAmountS.Text = mcrnoteamt.ToString("#0.00");
            //txtDBAmountS.Text = mdbnoteamt.ToString("#0.00");
            pnlDebitCreditNote.Visible = false;
            CalculateFinalSummary();
            tsBtnSave.Enabled = true;
            pnlSummary.BringToFront();
            pnlSummary.Visible = true;
            pnlSummary.Focus();
            txtCRAmountS.Focus();
        }

        private void dgCreditNote_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            btnCRDBOKClick();
        }
        private void mpMSVC_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            Exit();
        }
        private void cbRound_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MainToolStrip.Select();
                tsBtnSave.Select();
            }
            else if (e.KeyCode == Keys.Up)
                txtCashDiscountPerS.Focus();
        }
        private void lblViewVat5per_Click(object sender, EventArgs e)
        {

        }
        private void lblViewVAT12point5per_Click(object sender, EventArgs e)
        {

        }
        private void txtViewVat5per_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion UiEvents

        #region DateTimer
        private void datePickerBillDate_ValueChanged(object sender, EventArgs e)
        {
            SetDateStatus();
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


        #endregion DateTimer   

        private void mcbSalesman_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (mcbSalesman.SelectedID != null && mcbSalesman.SelectedID != "")
                _SSSale.SalesmanID = Convert.ToInt32(mcbSalesman.SelectedID);
        }

        private void mcbSalesman_ItemAddedEdited(object sender, EventArgs e)
        {

        }

        private void mcbSalesman_EnterKeyPressed(object sender, EventArgs e)
        {

            mpMSVC.SetFocus(1);
        }

        private void mcbSalesman_UpArrowPressed(object sender, EventArgs e)
        {

        }

        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {

        }

        private void mcbDoctor_SeletectIndexChanged(object sender, EventArgs e)
        {

        }

        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {

        }

        private void mcbDoctor_UpArrowPressed(object sender, EventArgs e)
        {

        }       

        private void dgScheme_OnCellEnterKeyPressed(int SelectedCellIndex)
        {
            int scmsaleqty = 0;
            int scmqty = 0;
            dgScheme.MainDataGridCurrentRow.Cells["Col_Selected"].Value = "Y";
            if (dgScheme.MainDataGridCurrentRow.Cells["Col_ScmProductQuantity"].Value != null && dgScheme.MainDataGridCurrentRow.Cells["Col_ScmProductQuantity"].Value.ToString() != string.Empty)
                scmsaleqty = Convert.ToInt32(dgScheme.MainDataGridCurrentRow.Cells["Col_ScmProductQuantity"].Value.ToString());
            if (dgScheme.MainDataGridCurrentRow.Cells["Col_ScmSchemeQuantity"].Value != null && dgScheme.MainDataGridCurrentRow.Cells["Col_ScmSchemeQuantity"].Value.ToString() != string.Empty)
                scmqty = Convert.ToInt32(dgScheme.MainDataGridCurrentRow.Cells["Col_ScmSchemeQuantity"].Value.ToString());
            if (dgScheme.MainDataGridCurrentRow.Index == 0)
            {
                dgScheme.Rows[0].Cells["Col_ScmText1"].Value = "Scheme - 1  Qty:";
                dgScheme.Rows[1].Cells["Col_Selected"].Value = "N";
                dgScheme.Rows[2].Cells["Col_Selected"].Value = "N";
            }
            else if (dgScheme.MainDataGridCurrentRow.Index == 1)
            {
                dgScheme.Rows[0].Cells["Col_Selected"].Value = "N";
                dgScheme.Rows[1].Cells["Col_ScmText1"].Value = "Scheme - 2  Qty:";
                dgScheme.Rows[2].Cells["Col_Selected"].Value = "N";
            }
            else if (dgScheme.MainDataGridCurrentRow.Index == 2)
            {
                dgScheme.Rows[0].Cells["Col_Selected"].Value = "N";
                dgScheme.Rows[1].Cells["Col_Selected"].Value = "N";
                dgScheme.Rows[2].Cells["Col_ScmText1"].Value = "Scheme - 3  Qty:";
            }
            _selectedScmSaleQuantity = scmsaleqty;
            _selectedScmQuantity = scmqty;
            CalculateScheme(scmsaleqty, scmqty);
        }

        private void txtscmDoyouWanttoEnterScheme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Y)
            {
                //if (txtscmDoyouWanttoEnterScheme.Text != null && txtscmDoyouWanttoEnterScheme.Text == "Y")
                //    dgScheme.SetFocus(1);
            }
            else
            {
                CloseSchemePanel();
            }
        }

        private void CloseSchemePanel()
        {
            pnlScheme.Visible = false;
            if (dgScheme.Rows.Count > 0)
                dgScheme.Rows.Clear();
            txtSchemeQuanityGiven.Text = "0";

            txtSchemeAmountGiven.Text = "0.00";
            if (txtNewBatch.Visible == true)
                txtNewBatch.Focus();
            else
                txtItemDiscPercent.Focus();
        }

        private void txtscmIsDataOK_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtscmIsDataOK.Text.Trim() == "Y")
                {
                    txtSchemeQuantity.Text = txtSchemeQuanityGiven.Text;
                    txtSchemeAmount.Text = txtSchemeAmountGiven.Text;
                }
                CloseSchemePanel();
            }
        }

        private void btnScmYes_Click(object sender, EventArgs e)
        {
            
            dgScheme.Select();
            dgScheme.SetFocus(0);
        }

        private void btnScmNO_Click(object sender, EventArgs e)
        {
            CloseSchemePanel();           
        }

        private void btnScmYes_KeyDown(object sender, KeyEventArgs e)
        {
            dgScheme.SetFocus(0);
        }

        private void btnScmNO_KeyDown(object sender, KeyEventArgs e)
        {
            pnlScheme.Visible = false;
        }

        private void btnScmYes_Enter(object sender, EventArgs e)
        {
            btnScmYes.BackColor = Color.NavajoWhite;
        }

        private void btnScmYes_Leave(object sender, EventArgs e)
        {
            btnScmYes.BackColor = Color.White;
        }

        private void btnScmNO_Enter(object sender, EventArgs e)
        {
            btnScmNO.BackColor = Color.NavajoWhite;
        }

        private void btnScmNO_Leave(object sender, EventArgs e)
        {
            btnScmNO.BackColor = Color.White;
        }

        private void btnscmisdataokNO_Click(object sender, EventArgs e)
        {
            CloseSchemePanel();
            txtscmIsDataOK.Text = "N";
        }

        private void btnscmisdataokNO_KeyDown(object sender, KeyEventArgs e)
        {
            CloseSchemePanel();
            txtscmIsDataOK.Text = "N";
        }

        private void btnscmisdataokYES_Click(object sender, EventArgs e)
        {          
            txtSchemeQuantity.Text = txtSchemeQuanityGiven.Text;
            txtSchemeAmount.Text = txtSchemeAmountGiven.Text;
            CloseSchemePanel();
            txtscmIsDataOK.Text = "Y";
        }

        private void btnscmisdataokYES_KeyDown(object sender, KeyEventArgs e)
        {
            CloseSchemePanel();
            txtscmIsDataOK.Text = "Y";
        }

        private void btnscmisdataokYES_Enter(object sender, EventArgs e)
        {
            btnscmisdataokYES.BackColor = Color.NavajoWhite;
        }

        private void btnscmisdataokYES_Leave(object sender, EventArgs e)
        {
            btnscmisdataokYES.BackColor = Color.White;
        }

        private void btnscmisdataokNO_Enter(object sender, EventArgs e)
        {
            btnscmisdataokNO.BackColor = Color.NavajoWhite;
        }

        private void btnscmisdataokNO_Leave(object sender, EventArgs e)
        {
            btnscmisdataokNO.BackColor = Color.White;
        }

        private void mcbTransporter_EnterKeyPressed(object sender, EventArgs e)
        {
            txtNarration1.Focus();
        }

        private void mcbTransporter_Validated(object sender, EventArgs e)
        {
            txtNarration1.Focus();
        }

        private void txtNarration1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNarration2.Focus();
            else if (e.KeyCode == Keys.Up)
                mcbTransporter.Focus();

        }

        private void txtNarration2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tsBtnSave.Select();
            else if (e.KeyCode == Keys.Up)
                txtNarration1.Focus();
        }

        private void txtNewBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNewMRP.Focus();
            else if (e.KeyCode == Keys.Up)
                txtQuantity.Focus();
        }

        private void txtNewMRP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNewSaleRate.Focus();
            else if (e.KeyCode == Keys.Up)
                txtNewBatch.Focus();
        }

        private void txtNewSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double ss = 0;
                if (txtSchemeAmount.Text != null && txtSchemeAmount.Text.ToString() != string.Empty)
                {
                    ss = Convert.ToDouble(txtSchemeAmount.Text.ToString());
                }
                if (ss > 0)
                    RecalculateSchemeAmount(_selectedScmSaleQuantity, _selectedScmQuantity);

                txtItemDiscPercent.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtNewMRP.Focus();
 
        }

        private void RecalculateSchemeAmount(int saleqty, int scm)
        {
            int msaleQty = saleqty;
            int mscm = scm;
            int mqty = Convert.ToInt32(txtQuantity.Text.ToString());
            int mscmqty = 0;
            double mrate = 0;
            double scmx = 0;
            double mscmamt = 0;
            if (MainSaleSubType != FixAccounts.SubTypeForSpecialSale)
                mrate = Convert.ToDouble(txtSaleRate.Text.ToString());
            else
                mrate = Convert.ToDouble(txtNewSaleRate.Text.ToString());
            double mmsaleqty = 0;
            mmsaleqty = (mqty * mscm) / msaleQty;
            mmsaleqty = Math.Floor(mmsaleqty);
            if ((Math.Floor(mmsaleqty) - (mqty * mscm / msaleQty) == 0))
                mscmqty = Convert.ToInt32(mmsaleqty);
            else
                mscmqty = 0;
            if (msaleQty + mscm > 0)
            {
                scmx = Math.Round((mrate * msaleQty) / (msaleQty + mscm), 2);
            }
            double scmy = Math.Round(mrate - scmx, 2);
            mscmamt = Math.Round(mqty * scmy, 2);
            if ((mqty + mscmqty) > _SSSale.CurrentBatchStock)
                mscmqty = 0;
            txtSchemeAmount.Text = mscmamt.ToString("#0.00");
         //   txtSchemeQuanityGiven.Text = mscmqty.ToString("#0");
         //   txtprescheme.Text = mscmqty.ToString("#0");
         //   txtSchemeQuanityGiven.Focus();
         //   lblscmIsDataOk.Visible = true;
         //   btnScmYes.Visible = false;
         //   btnScmNO.Visible = false;
         //   btnscmisdataokYES.Visible = true;
        //    btnscmisdataokNO.Visible = true;
        //    lblscmDoyouwanttoenterscheme.Visible = false;
        }
    }
}