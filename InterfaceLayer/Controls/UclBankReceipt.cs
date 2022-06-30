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
using PrintDataGrid;
using EcoMart.InterfaceLayer.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclBankReceipt : BaseControl
    {
        #region Declaration
        private BankReceipt _BankReceipt;
        DataTable _saledtable = new DataTable();
        DataTable _statementdtable = new DataTable();
        string DefaultBankID = "";
        private Form frmView;
        private DateTime _preDate = DateTime.Now;
        private string _preBankID = string.Empty;
        private string _prepaymentmodeID = string.Empty;
        private BaseControl ViewControl;    
        bool IfOpeningAdded = false;
        #endregion

        # region Constructor
        public UclBankReceipt()
        {
            try
            {
                InitializeComponent();
                _BankReceipt = new BankReceipt();
                SearchControl = new UclBankReceiptSearch();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region IDetail Control
        public override void SetFocus()
        {
            try
            {
                if (_Mode == OperationMode.Add)
                    mcbBankAccount.Focus();
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
                _BankReceipt.Initialise();
                ClearControls();
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
                datePickerBillDate.Value = _preDate;
                mcbBankAccount.SelectedID = _preBankID;
                headerLabel1.Text = "BANK RECEIPT -> NEW";
                pspaymode.SelectedID = _prepaymentmodeID;
                FillCombo();
                EnableDisableAdd();
                mcbBankAccount.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void FillCombo()
        {
            FillPartyCombo();

            string oldBankID = "";
            if (mcbBankAccount.SelectedID != null)
                oldBankID = mcbBankAccount.SelectedID;
            FillBankCombo();
            FillBankAccountCombo();
            FillPaymentModeCombo();
            _BankReceipt.CBBankAccountID = oldBankID;
            mcbBankAccount.SelectedID = oldBankID;
            FillBranchCombo();
        }

        private void EnableDisableAdd()
        {
            btnModify.Visible = false;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            pnlVouTypeNo.Enabled = true;
            pnlVou.Enabled = true;
            panel1.Enabled = true;
            if (_Mode == OperationMode.Add)
            {
                mcbCreditor.Enabled = true;
                txtAmountReceived.Enabled = true;
            }
            else if (_Mode == OperationMode.Edit)
            {
                mcbCreditor.Enabled = false;
                txtAmountReceived.Enabled = false;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
            }
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                ClearData();
                datePickerFromDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                datePickerToDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                headerLabel1.Text = "BANK RECEIPT -> EDIT";
                FillmpMSVCGrid("");
                //FillOpeningBalanceGrid("");//Commnet said By Madam
                FillCombo();
                datePickerBillDate.Value = General.TodayDateTime;
                EnableDisableAdd();
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
            pnlVou.Enabled = true;
            pnlVouTypeNo.Enabled = true;
            panel1.Enabled = true;
            btnModify.Visible = false;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = true;
            txtAmountReceived.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            mpMSCSale.ClearSelection();
            mcbCreditor.Focus();
        }
        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            ClearData();
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "BANK RECEIPT -> DELETE";
                ClearData();
                datePickerBillDate.Value = General.TodayDateTime;
                FillmpMSVCGrid("");
                FillCombo();
                EnableDisableDelete();
                txtVouchernumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }

        private void EnableDisableDelete()
        {
            btnModify.Visible = false;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = false;
            txtAmountReceived.Enabled = false;
            pnlVouTypeNo.Enabled = true;
            pnlVou.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            try
            {
                if (_BankReceipt.Id != null && _BankReceipt.Id != "")
                {
                    if (_BankReceipt.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _BankReceipt.DeleteDetails();
                        if (retValue)
                        {
                            DeletePreviousEntry();
                            RevertPreviousSalesBalance();
                            _BankReceipt.DeleteJV();
                        }
                        retValue = _BankReceipt.DeleteAccountDetails();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            MessageBox.Show("Deleted Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _BankReceipt.ModifiedBy = General.CurrentUser.Id;
                            _BankReceipt.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _BankReceipt.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            _BankReceipt.AddDeletedDetails();
                            AddPreviousRowsInDeletedDetail();
                            retValue = true;
                        }
                        else
                        {
                            MessageBox.Show("Could not Delete...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            retValue = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                LockTable.UnLockTables();
                ClearData();
            }
            catch (Exception Ex)
            {
                LockTable.UnLockTables();
                Log.WriteException(Ex);
            }
            return true;
        }
        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                 ClearData();
                headerLabel1.Text = "BANK RECEIPT -> VIEW";
                FillCombo();
                EnableDisableView();
                txtVouchernumber.Focus();
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

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void EnableDisableView()
        {
            btnModify.Visible = false;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = false;
            txtAmountReceived.Enabled = false;
            pnlVouTypeNo.Enabled = true;
            pnlVou.Enabled = true;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
        }
        private void GetLastRecord()
        {
            try
            {
                if (txtVouType.Text == null || txtVouType.Text.ToString().Trim() == string.Empty)
                {
                    _BankReceipt.CBVouType = FixAccounts.VoucherTypeForBankReceipt;
                }
                _BankReceipt.GetLastRecord();
                FillSearchData(_BankReceipt.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool Print()
        {
            bool retValue = true;
            if (txtAmountReceived.Text != null && Convert.ToDouble(txtAmountReceived.Text.ToString()) > 0)
            {
                if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
                {

                    ConstructPrintGridColumns();
                    PrintGrid.Rows.Clear();
                    FillPrintGrid();
                    if (General.CurrentSetting.MsetPrintCashBankVoucher == "Y")
                        PrintCashBankVoucherPrePrintedPaper();
                    else
                        PrintCashBankVoucherPlainPaper();
                }
                else
                {
                    PSDialogResult result;
                    result = PSMessageBox.Show("Trial License", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                }
            }
            ClearData();
            return retValue;
        }

        private void FillPrintGrid()
        {
            int colcount = mpMSCSale.ColumnsMain.Count;
            PSCashBankcontrol GridForPrint = new PSCashBankcontrol();
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                GridForPrint = mpMSCSale;
            else
                GridForPrint = mpMSVC;

            try
            {

                foreach (DataGridViewRow dr in GridForPrint.Rows)
                {
                    if (dr.Cells[0].Value != null && (dr.Cells["Col_GetClearedAmount"].Value != null || dr.Cells["Col_ClearedAmount"].Value != null) && Convert.ToDouble(dr.Cells["Col_GetClearedAmount"].Value.ToString()) > 0)
                    {
                        int printgridindex = PrintGrid.Rows.Add();
                        if (dr.Cells["Col_BillSeries"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_Quantity"].Value = dr.Cells["Col_BillSeries"].Value.ToString();
                        if (dr.Cells["Col_BillType"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_ProductName"].Value = dr.Cells["Col_BillType"].Value.ToString();
                        if (dr.Cells["Col_BillNumber"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_Shelf"].Value = dr.Cells["Col_BillNumber"].Value.ToString();
                        if (dr.Cells["Col_BillSubType"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_ProdCompShortName"].Value = dr.Cells["Col_BillSubType"].Value.ToString();
                        if (dr.Cells["Col_BillFromDate"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_BatchNumber"].Value = General.GetDateInShortDateFormat(dr.Cells["Col_BillFromDate"].Value.ToString());
                        if (dr.Cells["Col_BillAmount"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_RatePerUnit"].Value = dr.Cells["Col_BillAmount"].Value.ToString();
                        double amt = 0;
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                            amt = Convert.ToDouble(dr.Cells["Col_GetClearedAmount"].Value.ToString());
                        else
                            amt = Convert.ToDouble(dr.Cells["Col_ClearedAmount"].Value.ToString());
                        if (dr.Cells["Col_ClearedAmount"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_Amount"].Value = amt.ToString();


                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void PrintCashBankVoucherPrePrintedPaper()
        {
            EcoMart.Printing.PrePrintedPaperPrinter printer = new EcoMart.Printing.PrePrintedPaperPrinter();
            string atow = General.AmountToWord(_BankReceipt.CBAmount);
            printer.Print(_BankReceipt.CBVouType, _BankReceipt.CBVouNo.ToString(), _BankReceipt.CBVouDate, _BankReceipt.CBName, _BankReceipt.CBAddress1, _BankReceipt.CBAddress2, "Bank:" + " " + _BankReceipt.CBBankName + " Branch:" + _BankReceipt.CBBranchName, "Chq No:" + _BankReceipt.CBChequeNumber + "  Date:" + General.GetDateInShortDateFormat(_BankReceipt.CBChequeDate), PrintGrid.Rows, _BankReceipt.CBNarration, _BankReceipt.CBAmount, "", _BankReceipt.CBTotalDiscount, 0, 0, 0, 0, atow);

        }

        private void PrintCashBankVoucherPlainPaper()
        {
            EcoMart.Printing.PlainPaperPrinter printer = new EcoMart.Printing.PlainPaperPrinter();
            string atow = General.AmountToWord(_BankReceipt.CBAmount);
            printer.Print(_BankReceipt.CBVouType, _BankReceipt.CBVouNo.ToString(), _BankReceipt.CBVouDate, _BankReceipt.CBName, _BankReceipt.CBAddress1, _BankReceipt.CBAddress2, "Bank:" + " " + _BankReceipt.CBBankName + " Branch:" + _BankReceipt.CBBranchName, "Chq No:" + _BankReceipt.CBChequeNumber + "  Date:" + General.GetDateInShortDateFormat(_BankReceipt.CBChequeDate), PrintGrid.Rows, _BankReceipt.CBNarration, _BankReceipt.CBAmount, "", _BankReceipt.CBTotalDiscount, 0, 0, 0, 0, atow);

        }

        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _BankReceipt.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _BankReceipt.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _BankReceipt.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _BankReceipt.GetFirstRecord();
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _BankReceipt.Id = dr["CBID"].ToString();
                FillSearchData(_BankReceipt.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _BankReceipt.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _BankReceipt.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _BankReceipt.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _BankReceipt.CBVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _BankReceipt.CBVouSeries = txtVoucherSeries.Text.ToString();
            else
                _BankReceipt.CBVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _BankReceipt.CBVouNo = i;
                dr = _BankReceipt.ReadDetailsByIDVoucherNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _BankReceipt.Id = dr["CBID"].ToString();
                FillSearchData(_BankReceipt.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _BankReceipt.GetLastVoucherNumber(FixAccounts.VoucherTypeForCashReceipt, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _BankReceipt.CBVouType = txtVouType.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _BankReceipt.CBVouNo = i;
                dr = _BankReceipt.ReadDetailsByIDVoucherNumber();
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CBID"] != DBNull.Value)
            {
                _BankReceipt.Id = dr["CBID"].ToString();
                FillSearchData(_BankReceipt.Id, "");
            }
            return retValue;
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
        private void mpMSCSale_OnShowViewForm(DataGridViewRow selectedRow)
        {
            string voutype = "";
            string vousubtype = "";
            try
            {
                mpMSCSale.ViewControl = new UclDistributorSale("R");              
                string selectedID = selectedRow.Cells[0].Value.ToString();
                voutype = selectedRow.Cells["Col_BillType"].Value.ToString();
                vousubtype = selectedRow.Cells["Col_BillSubType"].Value.ToString();
                if (vousubtype == FixAccounts.SubTypeForRegularSale)
                    ViewControl = new UclDistributorSale("R");
             
                ShowViewForm(selectedID);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
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
            System.Text.StringBuilder _errorMessage;
            try
            {
                if (txtAmountReceived.Text != null && txtAmountReceived.Text != "")
                {
                    _BankReceipt.CBTotalDiscount = GetTotalDiscount();
                    LockTable.LockTablesForCashBankReceipts();
                    _BankReceipt.CBAccountID = mcbCreditor.SelectedID;
                    _BankReceipt.CBAmount = Convert.ToDouble(txtAmountReceived.Text.ToString());
                    _BankReceipt.CBNarration = txtNarration.Text.ToString().Trim();
                    if (txtAmtNotAdjusted.Text != null)
                    {
                        _BankReceipt.CBOnAccountAmount = Convert.ToDouble(txtAmtNotAdjusted.Text.ToString());
                    }

                    _BankReceipt.CBBankAccountID = mcbBankAccount.SelectedID;
                    _BankReceipt.CBPaymodeID = pspaymode.SelectedID;

                    _BankReceipt.CBBankID = mcbBank.SelectedID;
                    _BankReceipt.CBBranchID = mcbBranch.SelectedID;
                    _BankReceipt.CBBankName = mcbBank.SeletedItem.ItemData[1];
                    _BankReceipt.CBBranchName = mcbBranch.SeletedItem.ItemData[1];
                    _BankReceipt.MobileNumberForSMS = mcbCreditor.SeletedItem.ItemData[9]; //Amar
                    if (txtChequeNumber.Text != null)
                        _BankReceipt.CBChequeNumber = txtChequeNumber.Text.ToString();
                    _BankReceipt.CBChequeDate = datePickerChequeDate.Value.Date.ToString("yyyyMMdd");

                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _BankReceipt.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString().Trim());
                    _BankReceipt.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");

                    if (_Mode == OperationMode.Add)
                    {
                        _preDate = datePickerBillDate.Value;
                        _preBankID = mcbBankAccount.SelectedID;
                        _prepaymentmodeID = pspaymode.SelectedID;
                    }
                    
                    if (_Mode == OperationMode.Edit)
                        _BankReceipt.IFEdit = "Y";
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        LockTable.LocktblVoucherNo();
                        _BankReceipt.CBVouNo = _BankReceipt.GetAndUpdateBKRNumber(General.ShopDetail.ShopVoucherSeries);
                    }
                    _BankReceipt.Validate();
                    if (_BankReceipt.IsValid)
                    {
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();
                            //_BankReceipt.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _BankReceipt.CreatedBy = General.CurrentUser.Id;
                            _BankReceipt.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _BankReceipt.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            //_BankReceipt.CBVouNo = _BankReceipt.GetAndUpdateBKRNumber(General.ShopDetail.ShopVoucherSeries);
                            txtVouchernumber.Text = _BankReceipt.CBVouNo.ToString();
                            if (_BankReceipt.CBTotalDiscount > 0)
                            {
                                _BankReceipt.CBJVNo = _BankReceipt.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                            }
                            else
                            {
                                _BankReceipt.CBJVNo = 0;
                                _BankReceipt.CBJVIDpay = 0;
                            }
                            _BankReceipt.IntID = 0;
                            _BankReceipt.IntID = _BankReceipt.AddDetails();
                              
                            if (_BankReceipt.IntID > 0)
                                retValue = true;
                            else
                                retValue = false;
                            _SavedID = _BankReceipt.Id;
                            if (retValue)
                                retValue = saveDetails();
                            if (retValue)
                            {
                                _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _BankReceipt.AddAccountDetailsIntbltrnacDebit();
                            }
                            if (retValue)
                            {
                                _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _BankReceipt.AddAccountDetailsIntbltrnacCredit();
                            }
                            if (retValue && _BankReceipt.CBTotalDiscount > 0)
                            {
                                retValue = _BankReceipt.AddToMasterJV();
                                retValue = _BankReceipt.UpdateJVIDInVoucherBankReceipt();// update jvid with update jv number
                                _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _BankReceipt.AddJVTotblTrnacDebit();
                                _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _BankReceipt.AddJVTotblTrnacCredit();
                            }
                            if (retValue)
                            {
                                //SaveOldDetails();//Commnet said By Madam
                                General.CommitTransaction();
                            }
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                                string msgLine2 = _BankReceipt.CBVouType + "  " + _BankReceipt.CBVouNo.ToString("#0");
                                PSDialogResult result;
                                if (printData)
                                {
                                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                    Print();
                                    //Amar For Sms
                                    if (General.CurrentSetting.SmsSetBankReceiptSale =="Y")
                                    {
                                        SendSMS mSMS = new SendSMS();
                                        string Msg = "Dear Customer Your Bill No.:" + _BankReceipt.CBVouNo + " Of Amount :" + _BankReceipt.CBAmount + " Thank You For Dealing With Us.";
                                        Msg += mSMS.GetShopDetailsFromData();
                                        if (string.IsNullOrEmpty(_BankReceipt.MobileNumberForSMS) == false)
                                        {
                                            mSMS.SendSMSData(_BankReceipt.MobileNumberForSMS, Msg);
                                        }
                                        else
                                            MessageBox.Show("Please Update Mobile Number For Sending SMS", "EcoMart", MessageBoxButtons.OK);
                                    }
                                }
                                else
                                {
                                    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                    if (result == PSDialogResult.Print)
                                        Print();
                                    //Amar For Sms
                                    if (General.CurrentSetting.SmsSetBankReceiptSale == "Y")
                                    {
                                        SendSMS mSMS = new SendSMS();
                                        string Msg = "Dear Customer Your Bill No.:" + _BankReceipt.CBVouNo + " Of Amount :" + _BankReceipt.CBAmount + " Thank You For Dealing With Us.";
                                        Msg += mSMS.GetShopDetailsFromData();
                                        if (string.IsNullOrEmpty(_BankReceipt.MobileNumberForSMS) == false)
                                        {
                                            mSMS.SendSMSData(_BankReceipt.MobileNumberForSMS, Msg);
                                        }
                                        else
                                            MessageBox.Show("Please Update Mobile Number For Sending SMS", "EcoMart", MessageBoxButtons.OK);
                                    }
                                }
                                _SavedID = _BankReceipt.Id;

                                retValue = true;
                            }
                            else
                            {
                                MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                retValue = false;
                            }
                        }
                        else if (_Mode == OperationMode.Fifth)
                        {
                            General.BeginTransaction();
                            _BankReceipt.CBAccountID = mcbCreditor.SelectedID;
                            _BankReceipt.ModifiedBy = General.CurrentUser.Id;
                            _BankReceipt.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _BankReceipt.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            retValue = _BankReceipt.UpdateDetailsForFifth();
                            retValue = _BankReceipt.UpdateAccountDetailsIntbltrnacForFifth();
                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                                _BankReceipt.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                _BankReceipt.AddChangedDetails();
                                AddPreviousRowsInChangedDetail();
                                string msgLine2 = _BankReceipt.CBVouType + "  " + _BankReceipt.CBVouNo.ToString("#0");
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
                                _SavedID = _BankReceipt.Id;
                                retValue = true;
                            }
                        }
                        else if (_Mode == OperationMode.Edit)
                        {
                            if (_BankReceipt.ModifyEdit == "Y")
                            {
                                General.BeginTransaction();
                                _BankReceipt.CBAccountID = mcbCreditor.SelectedID;
                                _BankReceipt.CBPaymodeID = pspaymode.SelectedID;
                                _BankReceipt.ModifiedBy = General.CurrentUser.Id;
                                _BankReceipt.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _BankReceipt.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                                if (_BankReceipt.CBTotalDiscount > 0)
                                {
                                    _BankReceipt.CBJVNo = _BankReceipt.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                                    //_CashPayment.CBJVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                }
                                else
                                {
                                    _BankReceipt.CBJVIDpay = 0;
                                    _BankReceipt.CBJVNo = 0;
                                }
                                retValue = _BankReceipt.UpdateDetails();
                                if (retValue)
                                {
                                    retValue = DeletePreviousEntry();
                                    retValue = RevertPreviousSalesBalance();
                                    retValue = saveDetails();
                                }
                                if (retValue)
                                {
                                    retValue = _BankReceipt.DeleteJV();
                                    retValue = _BankReceipt.DeleteAccountDetails();
                                }
                                if (retValue)
                                {
                                    _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _BankReceipt.AddAccountDetailsIntbltrnacDebit();
                                }
                                if (retValue)
                                {
                                    _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _BankReceipt.AddAccountDetailsIntbltrnacCredit();
                                }
                                if (retValue && _BankReceipt.CBTotalDiscount > 0)
                                {
                                    //_CashReceipt.CBJVNo = _CashReceipt.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                                    //_CashReceipt.CBJVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _BankReceipt.AddToMasterJV();
                                    _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _BankReceipt.AddJVTotblTrnacDebit();
                                    _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    retValue = _BankReceipt.AddJVTotblTrnacCredit();
                                }
                                if (retValue)
                                {
                                    SaveOldDetails();
                                    General.CommitTransaction();
                                }
                                else
                                    General.RollbackTransaction();
                                LockTable.UnLockTables();
                                if (retValue)
                                {
                                    _BankReceipt.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _BankReceipt.AddChangedDetails();
                                    AddPreviousRowsInChangedDetail();
                                    string msgLine2 = _BankReceipt.CBVouType + "  " + _BankReceipt.CBVouNo.ToString("#0");
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
                                    _SavedID = _BankReceipt.Id;
                                    retValue = true;
                                }

                                else
                                {
                                    LockTable.UnLockTables();
                                    MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    retValue = false;
                                    //comment
                                }
                            }
                        }
                    }
                    else
                    {
                        LockTable.UnLockTables();
                        _errorMessage = new System.Text.StringBuilder();
                        _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                        foreach (string _message in _BankReceipt.ValidationMessages)
                        {
                            _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                        }
                        MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

            catch (Exception Ex)
            {
                LockTable.UnLockTables();
                Log.WriteException(Ex);
            }
            LockTable.UnLockTables();
            return retValue;
        }

        private double GetTotalDiscount()
        {
            double totdisc = 0;
            foreach (DataGridViewRow dr in mpMSCSale.Rows)
            {
                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != string.Empty)
                    totdisc += Convert.ToDouble(dr.Cells["Col_DiscountAmount"].Value.ToString());

            }
            return totdisc;
        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _BankReceipt.Id = ID;
                    if (Vmode == "C")
                        _BankReceipt.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _BankReceipt.ReadDetailsByIDForDeleted();
                    else
                        _BankReceipt.ReadDetailsByID();
                    mcbCreditor.SelectedID = _BankReceipt.CBAccountID;
                    _BankReceipt.ActualAccountID = _BankReceipt.CBAccountID;
                    mpMSCSale.Visible = false;
                    mpMSVC.Visible = true;

                    FillmpMSVCGrid(Vmode);

                    FillmpPVCTempGrid();
                    //DateTime mydate = new DateTime(Convert.ToInt32(_BankReceipt.CBVouDate.Substring(0, 4)), Convert.ToInt32(_BankReceipt.CBVouDate.Substring(4, 2)), Convert.ToInt32(_BankReceipt.CBVouDate.Substring(6, 2)));
                    //datePickerBillDate.Value = mydate;
                    if (DateTime.TryParse(_BankReceipt.CBVouDate, out DateTime mydate))
                        datePickerBillDate.Value = mydate;
                    mcbBankAccount.SelectedID = _BankReceipt.CBBankAccountID;
                    mcbBank.SelectedID = _BankReceipt.CBBankID;
                    mcbBranch.SelectedID = _BankReceipt.CBBranchID;
                   // _BankReceipt.CBBankName = mcbBank.SeletedItem.ItemData[1];
                    //_BankReceipt.CBBranchName = mcbBranch.SeletedItem.ItemData[1];

                    pspaymode.SelectedID = _BankReceipt.CBPaymodeID;   
                    _BankReceipt.CBPaymentModeOption=pspaymode.SeletedItem.ItemData[1];

                    txtAddress1.Text = _BankReceipt.CBAddress1;
                    txtAddress2.Text = _BankReceipt.CBAddress2;
                    txtNarration.Text = _BankReceipt.CBNarration;
                    txtVouchernumber.Text = _BankReceipt.CBVouNo.ToString();
                    txtVouType.Text = FixAccounts.VoucherTypeForBankReceipt;
                    txtChequeNumber.Text = _BankReceipt.CBChequeNumber.ToString();
                    datePickerChequeDate.Value = General.ConvertStringToDateyyyyMMdd(_BankReceipt.CBChequeDate);
                    txtAmountReceived.Text = _BankReceipt.CBAmount.ToString("#0.00");
                    txtAmtNotAdjusted.Text = _BankReceipt.CBOnAccountAmount.ToString("#0.00");
                    panel1.Enabled = false;

                    pnlVou.Enabled = false;
                    if (_Mode == OperationMode.Edit)
                        _BankReceipt.IFEdit = "Y";
                    else
                        _BankReceipt.IFEdit = "N";
                    if (_BankReceipt.IFEdit == "Y")
                    {
                        mpMSVC.ClearSelection();
                        mcbCreditor.BackColor = Color.White;
                        btnModify.Visible = true;
                        btnModify.Enabled = true;
                        btnModify.Focus();
                        btnModify.BackColor = General.ControlFocusColor;
                    }
                    else
                    {
                        btnModify.Visible = false;
                        btnModify.Enabled = false;
                    }
                    if (_BankReceipt.IfChequeReturn == "Y")
                    {
                        lblMessage.Text = "Cheque Return";
                        tsBtnSave.Enabled = false;
                        tsBtnSavenPrint.Enabled = false;
                        tsBtnDelete.Enabled = false;
                    }
                    if (_Mode == OperationMode.Fifth)
                    {
                        pnlVouTypeNo.Enabled = true;
                        pnlVou.Enabled = true;
                        datePickerBillDate.Enabled = true;
                        mpMSVC.Enabled = false;
                        datePickerBillDate.Focus();

                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        #endregion IDetail Control

        #region IDetail Members
        public override void ReFillData(Control closedControl)
        {
            if (closedControl is UclAccount)
            {
                FillBankAccountCombo();
                string preselectedID = "";
                if (mcbCreditor.SelectedID != null)
                    preselectedID = mcbCreditor.SelectedID;
                FillPartyCombo();
                mcbCreditor.SelectedID = preselectedID;
            }
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            try
            {

                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    mcbCreditor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    txtAmountReceived.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.T && modifier == Keys.Alt)
                {
                    txtNarration.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    datePickerBillDate.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.B && modifier == Keys.Alt)
                {
                    mcbBankAccount.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.K && modifier == Keys.Alt)
                {
                    mcbBank.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.H && modifier == Keys.Alt)
                {
                    mcbBranch.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    txtChequeNumber.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Q && modifier == Keys.Alt)
                {
                    datePickerChequeDate.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Escape)
                {
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

        public void ModifyEdit()
        {
            bool retValue = false;
            try
            {
                tsBtnSearch.Visible = false;
                btnModify.BackColor = Color.White;
                _Mode = OperationMode.Edit;
                _BankReceipt.ModifyEdit = "Y";

                retValue = FillmpPVC1GridSaleforModify();
                //FillOpeningBalanceGrid("");//Commnet said By Madam
                //  retValue = RevertPreviousEntry();
                //   FillDiscountFromTempGrid();
                mpMSCSale.Refresh();
                headerLabel1.Text = "BANK RECEIPT -> MODIFY";
                txtAmtNotAdjusted.Text = _BankReceipt.CBAmount.ToString("#0.00");
                //added
                txtAmountReceived.Enabled = true;
                //added
                EnableDisable();
                txtAmountReceived.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public bool RevertPreviousEntry()
        {
            bool returnVal = true;
            string mpmsvcID = "";
            double pvcClearedAmount = 0;
            double pvcdiscountamt = 0;
            double pvcbalaceamount = 0;
            double mbalaceamount = 0;
            try
            {
                foreach (DataGridViewRow pvcdr in mpMSVC.Rows)
                {
                    mpmsvcID = "";
                    pvcClearedAmount = 0;
                    pvcdiscountamt = 0;
                    pvcbalaceamount = 0;
                    if (pvcdr.Cells["Col_ID"].Value != null && pvcdr.Cells["Col_ID"].Value.ToString() != string.Empty)
                    {
                        mpmsvcID = pvcdr.Cells["Col_ID"].Value.ToString();
                        if (pvcdr.Cells["Col_BalanceAmount"].Value != null)
                            double.TryParse(pvcdr.Cells["Col_BalanceAmount"].Value.ToString(), out pvcbalaceamount);
                        if (pvcdr.Cells["Col_ClearedAmount"].Value != null)
                            double.TryParse(pvcdr.Cells["Col_ClearedAmount"].Value.ToString(), out pvcClearedAmount);
                        if (pvcdr.Cells["Col_DiscountAmount"].Value != null)
                            double.TryParse(pvcdr.Cells["Col_DiscountAmount"].Value.ToString(), out pvcdiscountamt);
                    }
                    foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
                    {



                        if (drowMSCSale.Cells["Col_ID"].Value != null && drowMSCSale.Cells["Col_BillType"].Value.ToString() != "OPB")
                        {
                            string drowMSCSaleID = drowMSCSale.Cells["Col_ID"].Value.ToString();
                            if (mpmsvcID == drowMSCSaleID)
                            {
                                mbalaceamount = 0;

                                if (drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
                                    double.TryParse(drowMSCSale.Cells["Col_BalanceAmount"].Value.ToString(), out mbalaceamount);
                                //  if (drowMSCSale.Cells["Col_ClearedAmount"].Value != null)
                                //        double.TryParse(drowMSCSale.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                                //   if (drowMSCSale.Cells["Col_DiscountAmount"].Value != null)
                                //       double.TryParse(drowMSCSale.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);

                                drowMSCSale.Cells["Col_BalanceAmount"].Value = mbalaceamount + pvcClearedAmount + pvcdiscountamt;
                                drowMSCSale.Cells["Col_GetClearedAmount"].Value = "";
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
            //////bool returnVal = true;
            //////try
            //////{
            //////    foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
            //////    {

            //////        double mClearedAmount = 0;



            //////        if (drowMSCSale.Cells["Col_ID"].Value != null && drowMSCSale.Cells["Col_BillType"].Value.ToString() != "OPB")
            //////        {
            //////            double mbalaceamount = 0;
            //////            double discountamount = 0;

            //////            if (drowMSCSale.Cells["Col_ClearedAmount"].Value != null)
            //////                double.TryParse(drowMSCSale.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
            //////            if (drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
            //////                double.TryParse(drowMSCSale.Cells["Col_BalanceAmount"].Value.ToString(), out mbalaceamount);
            //////            if (drowMSCSale.Cells["Col_DiscountAmount"].Value != null && drowMSCSale.Cells["Col_DiscountAmount"].Value.ToString() != "")
            //////                double.TryParse(drowMSCSale.Cells["Col_DiscountAmount"].Value.ToString(), out discountamount);


            //////            drowMSCSale.Cells["Col_BalanceAmount"].Value = mbalaceamount + mClearedAmount + discountamount;
            //////            drowMSCSale.Cells["Col_GetClearedAmount"].Value = "";


            //////        }


            //////    }
            //////}
            //////catch (Exception ex)
            //////{
            //////    Log.WriteError(ex.ToString());
            //////    returnVal = false;
            //////}
            //////return returnVal;

        }
        public bool RevertPreviousSalesBalance()
        {
            bool retValue = false;

            try
            {
                foreach (DataGridViewRow drowPVCTemp in mpPVCTemp.Rows)
                {
                    string mSaleID = "";
                    string mcbid = "";
                    double mClearedAmount = 0;
                    string mvoutype = "";
                    double mdiscamount = 0;
                    if (drowPVCTemp.Cells["Col_MasterSaleID"].Value != null)
                        mSaleID = drowPVCTemp.Cells["Col_MasterSaleID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_MasterID"].Value != null)
                        mcbid = drowPVCTemp.Cells["Col_MasterID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_ClearedAmount"].Value != null)
                        double.TryParse(drowPVCTemp.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                    if (drowPVCTemp.Cells["Col_DiscountAmount"].Value != null && drowPVCTemp.Cells["Col_DiscountAmount"].Value.ToString() != "")
                        double.TryParse(drowPVCTemp.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscamount);
                    if (mSaleID != null && mClearedAmount != 0)
                    {
                        if (drowPVCTemp.Cells["Col_BillType"].Value != null)
                            mvoutype = drowPVCTemp.Cells["Col_BillType"].Value.ToString();
                        if (mvoutype == FixAccounts.VoucherTypeForOpeningBalance)
                            retValue = _BankReceipt.UpdateOpeningBalanceReducePrevious(_BankReceipt.preAccountID, _BankReceipt.OpeningClearedInVoucher + _BankReceipt.DiscountInOpeningBalance);
                        else if (mvoutype == FixAccounts.VoucherTypeForCreditSale )
                            retValue = _BankReceipt.RevertPreviousSalesBalance(mSaleID, mClearedAmount + mdiscamount);
                        else if (mvoutype == FixAccounts.VoucherTypeForStatementSale )
                            retValue = _BankReceipt.RevertPreviousStatementBalance(mSaleID, mClearedAmount + mdiscamount);
                    }
                    if (retValue == false)
                        break;
                    retValue = true;
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

        #region Other Private Methods

        private bool DeletePreviousEntry()
        {
            bool returnVal = false;
            try
            {
                returnVal = _BankReceipt.DeletePreviousRecords();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return returnVal;
        }

        private bool saveDetails()
        {
            {
                bool returnVal = true;
                _BankReceipt.SerialNumber = 0;

                try
                {
                    foreach (DataGridViewRow drow in mpMSCSale.Rows)
                    {
                        if (drow.Cells["Col_GetClearedAmount"].Value != null && drow.Cells["Col_GetClearedAmount"].Value.ToString() != "" &&
                           Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                        {
                            _BankReceipt.SerialNumber += 1;
                            _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            if (drow.Cells["Col_MasterID"].Value != null)
                                _BankReceipt.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _BankReceipt.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            if (_BankReceipt.DSaleId == "OPB")
                            {
                                _BankReceipt.DSaleId = "0";
                            }
                            else
                            {
                                _BankReceipt.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            }

                            _BankReceipt.DVoucherSeries = drow.Cells["Col_BillSeries"].Value.ToString();
                            _BankReceipt.DVoucherType = drow.Cells["Col_BillType"].Value.ToString();
                            if (drow.Cells["Col_BillNumber"].Value.ToString() != string.Empty)
                                _BankReceipt.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_BillNumber"].Value.ToString());
                            if (drow.Cells["Col_BillSubType"].Value.ToString() != string.Empty)
                                _BankReceipt.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                            if (drow.Cells["Col_BillFromDate"].Value.ToString() != string.Empty)
                                _BankReceipt.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _BankReceipt.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _BankReceipt.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _BankReceipt.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());
                            _BankReceipt.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                            returnVal = _BankReceipt.AddParticularsDetails();
                            if (returnVal)
                            {
                                if (_BankReceipt.DVoucherType == FixAccounts.VoucherTypeForOpeningBalance)
                                    returnVal = _BankReceipt.UpdateOpeningBalanceAddNew();
                                else if (_BankReceipt.DVoucherType == FixAccounts.VoucherTypeForStatementSale)
                                    returnVal = _BankReceipt.UpdateSaleStatement();
                                else if (_BankReceipt.DVoucherType == FixAccounts.VoucherTypeForCreditSale)
                                    returnVal = _BankReceipt.UpdateSCCBill();
                                if (returnVal == false)
                                    break;
                            }
                            else
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());
                    returnVal = false;
                }
                return returnVal;
            }
        }

        private bool AddPreviousRowsInChangedDetail()
        {
            {
                bool returnVal = true;
                _BankReceipt.SerialNumber = 0;

                try
                {
                    foreach (DataGridViewRow drow in mpPVCTemp.Rows)
                    {
                        if (drow.Cells["Col_BalanceAmount"].Value != null && drow.Cells["Col_BalanceAmount"].Value.ToString() != "" &&
                           Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value) > 0)
                        {
                            _BankReceipt.SerialNumber += 1;
                            _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _BankReceipt.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _BankReceipt.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _BankReceipt.DVoucherSeries = drow.Cells["Col_BillSeries"].Value.ToString();
                            _BankReceipt.DVoucherType = drow.Cells["Col_BillType"].Value.ToString();
                            _BankReceipt.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_BillNumber"].Value.ToString());
                            _BankReceipt.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                            _BankReceipt.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _BankReceipt.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _BankReceipt.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _BankReceipt.DClearedAmount = Convert.ToDouble(drow.Cells["Col_ClearedAmount"].Value.ToString());
                            _BankReceipt.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                            returnVal = _BankReceipt.AddParticularsDetailsChanged();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());
                    returnVal = false;
                }
                return returnVal;
            }
        }
        private bool AddPreviousRowsInDeletedDetail()
        {
            {
                bool returnVal = true;
                _BankReceipt.SerialNumber = 0;

                try
                {
                    foreach (DataGridViewRow drow in mpPVCTemp.Rows)
                    {
                        if (drow.Cells["Col_BalanceAmount"].Value != null && drow.Cells["Col_BalanceAmount"].Value.ToString() != "" &&
                           Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value) > 0)
                        {
                            _BankReceipt.SerialNumber += 1;
                            _BankReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _BankReceipt.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _BankReceipt.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _BankReceipt.DVoucherSeries = drow.Cells["Col_BillSeries"].Value.ToString();
                            _BankReceipt.DVoucherType = drow.Cells["Col_BillType"].Value.ToString();
                            _BankReceipt.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_BillNumber"].Value.ToString());
                            _BankReceipt.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                            _BankReceipt.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _BankReceipt.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _BankReceipt.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _BankReceipt.DClearedAmount = Convert.ToDouble(drow.Cells["Col_ClearedAmount"].Value.ToString());
                            _BankReceipt.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                            returnVal = _BankReceipt.AddParticularsDetailsDeleted();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());
                    returnVal = false;
                }
                return returnVal;
            }
        }


        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }
        private void ClearControls()
        {
            try
            {
                tsBtnFifth.Visible = false;
                dgClearOpeningBalance.Visible = false;
                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtChequeNumber.Clear();
                txtNoOfRows.Text = "0";
                txtTotalBalance.Text = "0.00";
                txtVouType.Text = FixAccounts.VoucherTypeForBankReceipt;
                txtVoucherSeries.Text = _BankReceipt.CBVouSeries;

                datePickerChequeDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));

                txtAmountReceived.Text = "0.00";
                txtAmtNotAdjusted.Text = "0.00";
                mpMSVC.ColumnsMain.Clear();
                mpMSCSale.ColumnsMain.Clear();
                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();
                mcbCreditor.SelectedID = "";
                mcbBank.SelectedID = "";
                mcbBranch.SelectedID = "";
                // mcbBankAccount.SelectedID = "";
                this.mcbCreditor.Focus();
                lblMessage.Text = "";
                tsBtnSave.Enabled = true;
                tsBtnSavenPrint.Enabled = true;
                tsBtnDelete.Enabled = true;
                _saledtable = null;
                _statementdtable = null;
                if (_Mode == OperationMode.Add)
                {
                 //   mcbCreditor.Enabled = true;
               //     mcbBankAccount.Enabled = true;
                 //   this.mcbCreditor.Focus();
                //    txtVouchernumber.Enabled = false;
                    datePickerFromDate.Visible = true;
                    datePickerToDate.Visible = true;
                    lblFromDate.Visible = true;
                    lblToDate.Visible = true;
                }
                else
                {
                 //   txtVouchernumber.Enabled = true;
                //    txtVouchernumber.Focus();
                 //   mcbBankAccount.Enabled = false;
                //    mcbCreditor.Enabled = false;
                    datePickerFromDate.Visible = false;
                    datePickerToDate.Visible = false;
                    lblFromDate.Visible = false;
                    lblToDate.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void NoofRows()
        {
            int itemCount = 0;
            double totamt = 0;

            try
            {
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {
                    if (dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                        totamt = totamt + Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtTotalBalance.Text = totamt.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void NoofRowsFormpMSVCGrid()
        {
            int itemCount = 0;
            double totamt = 0;

            try
            {
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {
                    if (dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                        totamt = totamt + Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtTotalBalance.Text = totamt.ToString("#0.00");
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
                mcbCreditor.SourceDataString = new string[10] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccOpeningDebit", "AccClearedAmount" ,"AccBankID","AccBranchID", "MobileNumberForSMS" };
                mcbCreditor.ColumnWidth = new string[10] { "0", "20", "200", "200", "150", "0", "0","0","0", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetDebtorCreditorListForCashBankReceipt();
                mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
        public void GetDefaultBank()
        {
            DefaultBankID = General.GetDefaultBank();
            if (DefaultBankID != null)
                mcbBankAccount.SelectedID = DefaultBankID;
        }
        private void FillBankCombo()
        {
            try
            {
                mcbBank.SelectedID = null;
                mcbBank.SourceDataString = new string[2] { "BankID", "BankName" };
                mcbBank.ColumnWidth = new string[2] { "0", "200" };
                mcbBank.ValueColumnNo = 0;
                mcbBank.UserControlToShow = new UclBank();
                Bank _Bank = new Bank();
                DataTable dtable = _Bank.GetOverviewData();
                mcbBank.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillPaymentModeCombo()
        {
            try
            {
                pspaymode.SelectedID = null;
                pspaymode.SourceDataString = new string[2] { "PayModeID", "PayModeOption" };
                pspaymode.ColumnWidth = new string[2] { "0", "200" };
                pspaymode.ValueColumnNo = 0;
                pspaymode.UserControlToShow = new UclBank();
                Bank _Bank = new Bank();
                DataTable dtable = _Bank.GetPaymentModeData();
                pspaymode.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillBranchCombo()
        {
            try
            {
                mcbBranch.SelectedID = null;
                mcbBranch.SourceDataString = new string[2] { "BranchID", "BranchName" };
                mcbBranch.ColumnWidth = new string[2] { "0", "200" };
                mcbBranch.ValueColumnNo = 0;
                mcbBranch.UserControlToShow = new UclBranch();
                Branch _Branch = new Branch();
                DataTable dtable = _Branch.GetOverviewData();
                mcbBranch.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                if (mcbCreditor.SeletedItem != null)
                {
                    if (mcbCreditor.SeletedItem.ItemData[3] != null)
                        txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    if (mcbCreditor.SeletedItem.ItemData[4] != null)
                        txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    if (mcbCreditor.SeletedItem.ItemData[5] != null)
                        _BankReceipt.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
                    if (mcbCreditor.SeletedItem.ItemData[6] != null)
                        _BankReceipt.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    _BankReceipt.CBAccountID = mcbCreditor.SelectedID;
                    //  FillmpMSVCGrid("");                   
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
                GetData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void GetData()
        {
            _BankReceipt.CBFromDate = datePickerFromDate.Value.Date.ToString("yyyyMMdd");
            _BankReceipt.CBToDate = datePickerToDate.Value.Date.ToString("yyyyMMdd");
            if (mcbCreditor.SeletedItem != null)
            {
                if (mcbCreditor.SeletedItem.ItemData[3] != null)
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                if (mcbCreditor.SeletedItem.ItemData[4] != null)
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                if (mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5].ToString() != string.Empty)
                    _BankReceipt.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
                if (mcbCreditor.SeletedItem.ItemData[6] != null && mcbCreditor.SeletedItem.ItemData[6].ToString() != string.Empty)
                    _BankReceipt.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString());
                if (mcbCreditor.SeletedItem.ItemData[7] != null && mcbCreditor.SeletedItem.ItemData[7].ToString() != string.Empty)
                {
                    _BankReceipt.CBBankID = mcbCreditor.SeletedItem.ItemData[7].ToString();
                    mcbBank.SelectedID = _BankReceipt.CBBankID;
                }
                if (mcbCreditor.SeletedItem.ItemData[8] != null && mcbCreditor.SeletedItem.ItemData[8].ToString() != string.Empty)
                {
                    _BankReceipt.CBBranchID = mcbCreditor.SeletedItem.ItemData[8].ToString();
                    mcbBranch.SelectedID = _BankReceipt.CBBranchID;
                }
                _BankReceipt.CBAccountID = mcbCreditor.SelectedID;
                // if (_Mode != OperationMode.ReportView)
                FillmpMSVCGrid("");
                //FillOpeningBalanceGrid("");//Commnet said By Madam
                mpMSCSale.ClearSelection();
               // txtAmountReceived.Focus();
            }
        }

        private bool FillmpPVC1GridSaleforModify()
        {
            bool retValue = false;
            try
            {
                ConstructSaleColumns();
                FormatSaleGrid();
                DataTable dtable = new DataTable();

                IfOpeningAdded = false;
                dtable = _BankReceipt.ReadBillDetailsByIDforModify();
                _statementdtable = _BankReceipt.ReadStatementDetailsByIDforModify();
                mpMSCSale.Rows.Clear();
                BindmpMSCSaleGrid(dtable, _statementdtable);
                retValue = RevertPreviousEntry();
                IfOpeningAdded = true;
                _saledtable = _BankReceipt.ReadBillDetailsByID();
                _statementdtable = _BankReceipt.ReadStatementDetailsByID();
                BindmpMSCSaleGrid(_saledtable, _statementdtable);
                NoofRows();
               // FillOpeningBalanceGrid("");//Commnet said By Madam
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        private void FormatSaleGrid()
        {
            mpMSCSale.DoubleColumnNames.Add("Col_BillAmount");
            mpMSCSale.DoubleColumnNames.Add("Col_BalanceAmount");
            mpMSCSale.DoubleColumnNames.Add("Col_GetClearedAmount");
            mpMSCSale.DateColumnNames.Add("Col_BillFromDate");
            mpMSCSale.ClearSelection();
        }
        //private void FillDiscountFromTempGrid()
        //{
        //    string mtempvouid = "";
        //    string msalevouid = "";
        //    double mtempdiscamt = 0;
        //    double balamount = 0;
        //    foreach (DataGridViewRow tempdr in mpPVCTemp.Rows)
        //    {
        //        if (tempdr.Cells["Col_MasterSaleID"].Value != null)
        //            mtempvouid = tempdr.Cells["Col_MasterSaleID"].Value.ToString();
        //        if (tempdr.Cells["Col_DiscountAmount"].Value != null)
        //            mtempdiscamt = Convert.ToDouble(tempdr.Cells["Col_DiscountAmount"].Value.ToString());
        //        if (mtempvouid != FixAccounts.VoucherTypeForOpeningBalance && mtempdiscamt > 0)
        //        {
        //            foreach (DataGridViewRow dr in mpMSCSale.Rows)
        //            {
        //                if (dr.Cells["Col_ID"].Value != null)
        //                    msalevouid = dr.Cells["Col_ID"].Value.ToString();
        //                if (mtempvouid == msalevouid)
        //                {
        //                    if (dr.Cells["Col_BalanceAmount"].Value != null)
        //                        balamount = Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
        //                    balamount += mtempdiscamt;
        //                    dr.Cells["Col_BalanceAmount"].Value = balamount.ToString("#0.00");
        //                }
        //            }
        //        }
        //    }
        //}
        private bool FillmpMSVCGrid(string vmode)
        {
            bool retValue = false;
            try
            {
                ConstructMainColumns();

                mpMSVC.DoubleColumnNames.Add("Col_BillAmount");
                mpMSVC.DoubleColumnNames.Add("Col_BalanceAmount");
                mpMSVC.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSVC.DoubleColumnNames.Add("Col_GetClearedAmount");
                mpMSVC.DateColumnNames.Add("Col_VoucherDate");
                mpMSVC.DateColumnNames.Add("Col_BillFromDate");


                ConstructSaleColumns();

                mpMSCSale.DoubleColumnNames.Add("Col_GetClearedAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_BillAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_BalanceAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSCSale.DateColumnNames.Add("Col_VoucherDate");
                mpMSCSale.DateColumnNames.Add("Col_BillFromDate");


                if (_BankReceipt.CBAccountID != null && _BankReceipt.CBAccountID != "")
                {
                    if ((_Mode == OperationMode.Add) || (_BankReceipt.ActualAccountID != _BankReceipt.CBAccountID && _BankReceipt.ModifyEdit == "Y"))
                    {
                        _saledtable = _BankReceipt.ReadBillDetailsByID();
                        _statementdtable = _BankReceipt.ReadStatementDetailsByID();
                        mpMSCSale.Rows.Clear();
                        BindmpMSVCGrid(_saledtable, _statementdtable);
                        IfOpeningAdded = false;
                        BindmpMSCSaleGrid(_saledtable, _statementdtable);
                        NoofRows();
                    }
                    else
                    {
                        mpMSVC.Rows.Clear();
                        _statementdtable = null;
                        if (vmode == "C")
                            _saledtable = _BankReceipt.ReadBillDetailsByBKRIDForChanged();
                        else if (vmode == "D")
                            _saledtable = _BankReceipt.ReadBillDetailsByBKRIDForDeleted();
                        else
                        {
                            _saledtable = _BankReceipt.ReadBillDetailsByBKRID();
                            _statementdtable = _BankReceipt.ReadStatementDetailsByBKRID();
                        }
                        BindmpMSVCGrid(_saledtable, _statementdtable);
                        NoofRowsFormpMSVCGrid();
                        foreach (DataRow dr in _saledtable.Rows)
                        {
                            if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForOpeningBalance)
                            {
                                _BankReceipt.OpeningClearedInVoucher = Convert.ToDouble(dr["AmountClear"].ToString());
                                _BankReceipt.DiscountInOpeningBalance = Convert.ToDouble(dr["DiscountAmount"].ToString());
                                break;
                            }
                        }

                    }
                }


                retValue = true;
                txtVouchernumber.BackColor = Color.White;
                btnModify.BackColor = General.ControlFocusColor;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }
        private void BindmpMSVCGrid(DataTable saletable, DataTable statementtable)
        {
            mpMSVC.Rows.Clear();
            int _rowIndex = 0;
            IfOpeningAdded = false;
            if ((_Mode == OperationMode.Add || _Mode == OperationMode.ReportView) || (_BankReceipt.CBAccountID != _BankReceipt.preAccountID))
            {
                if ((_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared) > 0)
                {
                    _rowIndex = mpMSVC.Rows.Add();
                    DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = "OPB";
                    currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                    currentdr.Cells["Col_BillType"].Value = "OPB";
                    currentdr.Cells["Col_BillNumber"].Value = "";
                    currentdr.Cells["Col_BillSubType"].Value = "";
                    currentdr.Cells["Col_BillFromDate"].Value = "";
                    currentdr.Cells["Col_BillAmount"].Value = _BankReceipt.OpeningBalance.ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = "";
                    currentdr.Cells["Col_BalanceAmount"].Value = (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared).ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = "";
                    IfOpeningAdded = true;
                }
            }

            if (saletable != null && saletable.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        _rowIndex = mpMSVC.Rows.Add();
                        DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in statementtable.Rows)
                    {
                        _rowIndex = mpMSVC.Rows.Add();
                        DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }

            }

        }

        private void BindmpMSCSaleGrid(DataTable saletable, DataTable statementtable)
        {

            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            if (IfOpeningAdded != true && (_Mode != OperationMode.Edit) && (_Mode == OperationMode.Add || ((_BankReceipt.preAccountID != null && _BankReceipt.preAccountID != "") && _BankReceipt.CBAccountID != _BankReceipt.preAccountID) || _BankReceipt.ModifyEdit == "Y"))
            {
                if ((_BankReceipt.OpeningClearedInVoucher > 0 || (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared + _BankReceipt.OpeningClearedInVoucher + _BankReceipt.DiscountInOpeningBalance) > 0) && ((_BankReceipt.preAccountID == null || _BankReceipt.preAccountID == "") || _BankReceipt.CBAccountID == _BankReceipt.preAccountID))
                {
                    _rowIndex = mpMSCSale.Rows.Add();
                    DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = "OPB";
                    currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                    currentdr.Cells["Col_BillType"].Value = "OPB";
                    currentdr.Cells["Col_BillNumber"].Value = "";
                    currentdr.Cells["Col_BillSubType"].Value = "";
                    currentdr.Cells["Col_BillFromDate"].Value = "";
                    currentdr.Cells["Col_BillAmount"].Value = _BankReceipt.OpeningBalance.ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = "";
                    if ((_BankReceipt.preAccountID == null || _BankReceipt.preAccountID == "") || ((_BankReceipt.preAccountID != null || _BankReceipt.preAccountID != "") && _BankReceipt.CBAccountID == _BankReceipt.preAccountID))
                        currentdr.Cells["Col_BalanceAmount"].Value = (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared + _BankReceipt.OpeningClearedInVoucher + _BankReceipt.DiscountInOpeningBalance).ToString();
                    else
                        currentdr.Cells["Col_BalanceAmount"].Value = _BankReceipt.OpeningBalance;
                    currentdr.Cells["Col_ClearedAmount"].Value = "";
                    IfOpeningAdded = true;
                }
            }
            else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
            {
                if (_BankReceipt.OpeningClearedInVoucher >= 0)
                {
                    if ((_BankReceipt.OpeningBalance > 0) && (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared + _BankReceipt.OpeningClearedInVoucher + _BankReceipt.DiscountInOpeningBalance) > 0)
                    {
                        _rowIndex = mpMSCSale.Rows.Add();
                        DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
                        currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                        currentdr.Cells["Col_BillType"].Value = "OPB";
                        currentdr.Cells["Col_BillNumber"].Value = "";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = "";
                        currentdr.Cells["Col_BillAmount"].Value = _BankReceipt.OpeningBalance.ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = "";
                        currentdr.Cells["Col_BalanceAmount"].Value = (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared + _BankReceipt.OpeningClearedInVoucher + _BankReceipt.DiscountInOpeningBalance).ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                    }
                }
            }
            if (saletable != null && saletable.Rows.Count > 0)
            {
                foreach (DataRow dr in saletable.Rows)
                {
                    if (dr["ID"] != DBNull.Value)
                        iD = dr["ID"].ToString();
                    ifIDFound = SearchforIDInSaleGrid(iD);
                    if (ifIDFound == false)
                    {
                        _rowIndex = mpMSCSale.Rows.Add();
                        DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        currentdr.Cells["Col_BillFromDate"].Value = (dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        //   currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        //currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                        currentdr.Cells["Col_Discountamount"].Value = "0.00";
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                    }

                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {

                foreach (DataRow dr in statementtable.Rows)
                {
                    if (dr["ID"] != DBNull.Value)
                        iD = dr["ID"].ToString();
                    ifIDFound = SearchforIDInSaleGrid(iD);
                    if (ifIDFound == false)
                    {
                        _rowIndex = mpMSCSale.Rows.Add();
                        DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        currentdr.Cells["Col_BillFromDate"].Value = (dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        //   currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                        currentdr.Cells["Col_Discountamount"].Value = "0.00";
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                    }

                }

            }

        }
        private bool SearchforIDInSaleGrid(string ID)
        {
            bool retValue = false;
            string _GridID = "";
            foreach (DataGridViewRow dr in mpMSCSale.Rows)
            {
                if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    _GridID = dr.Cells["Col_ID"].Value.ToString();
                if (_GridID == ID)
                {
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }

        private bool FillmpPVCTempGrid()
        {
            bool retValue = false;
            try
            {
                ConstructTempColumns();
                FormatTempGrid();
                DataTable dtable = new DataTable();
                dtable = _BankReceipt.ReadBillDetailsByBKRID();
                _statementdtable = _BankReceipt.ReadStatementDetailsByBKRID();
                //   mpPVCTemp.DataSourceMain = dtable;
                BindTempGrid(dtable, _statementdtable);
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        private void FormatTempGrid()
        {
            //mpPVCTemp.DoubleColumnNames.Add("Col_BillAmount");
            //mpPVCTemp.DoubleColumnNames.Add("Col_BalanceAmount");
            //mpPVCTemp.DoubleColumnNames.Add("Col_ClearedAmount");
            mpMSVC.DoubleColumnNames.Add("Col_ClearedAmount");
        }
        private void BindTempGrid(DataTable saletable, DataTable statementtable)
        {

            int _rowIndex = 0;
            string iD = "";

            if (saletable != null && saletable.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        //ifIDFound = SearchforIDInSaleGrid(iD);
                        //if (ifIDFound == false)
                        //{
                        _rowIndex = mpPVCTemp.Rows.Add();
                        DataGridViewRow currentdr = mpPVCTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}

                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());


                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {

                foreach (DataRow dr in statementtable.Rows)
                {
                    try
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        _rowIndex = mpPVCTemp.Rows.Add();
                        DataGridViewRow currentdr = mpPVCTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex.ToString());

                    }
                }

            }

        }

        #endregion

        #region Construct columns
        private void ConstructPrintGridColumns()
        {
            DataGridViewTextBoxColumn column;
            PrintGrid.Columns.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 220;
                column.ReadOnly = false;
                PrintGrid.Columns.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                PrintGrid.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 50;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                PrintGrid.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //8          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 70;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                PrintGrid.Columns.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                PrintGrid.Columns.Add(column);
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
                PrintGrid.Columns.Add(column);
                //13            // temp storage columns 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //14         
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "IfSaleDisc";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                ////// added new columns 29/3/2015

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RatePerUnit";
                // column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFMultipleMRP";
                //  column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.Columns.Add(column);



            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
        private void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSVC.ColumnsMain.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Series";
                column.Width = 70;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.ReadOnly = true;
                column.Width = 55;
                mpMSVC.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 80;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "Sub";
                column.Width = 40;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //6             
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                //7 --- 9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                //9 -- 11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.DataPropertyName = "MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.HeaderText = "Discount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.Visible = true;
                column.ReadOnly = false;
                mpMSVC.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructSaleColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMSCSale.ColumnsMain.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.HeaderText = "Series.";
                column.Width = 70;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.HeaderText = "Type";
                column.ReadOnly = true;
                column.Width = 55;
                mpMSCSale.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.HeaderText = "Number";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.HeaderText = "Sub";
                column.Width = 40;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.HeaderText = "Date";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Check";
                column.HeaderText = "Check";
                column.Width = 30;
                if (_Mode == OperationMode.Add)
                {
                    column.Visible = true;
                    //column.ReadOnly = false;
                }
                else
                {
                    column.Visible = false;
                    //column.ReadOnly = true;
                }
                mpMSCSale.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSCSale.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.HeaderText = "Party";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.HeaderText = "BalanceAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);

                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //13
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //14
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.HeaderText = "Discount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructTempColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVCTemp.Columns.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "VoucherSeriesT";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "VoucherType";
                column.ReadOnly = true;
                column.Width = 80;
                mpPVCTemp.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "VoucherNumber";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SubType";
                column.Width = 45;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "FromDate";
                column.Width = 80;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.DataPropertyName = "MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region EVENTS

       

        private void txtBillAmount_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    mcbBank.Focus();
                    break;
                case Keys.Down:
                    mcbBank.Focus();
                    break;
            }
        }
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_Mode == OperationMode.Add)
                {
                    if (_BankReceipt.PaymentSubType == 2)
                        mpMSCSale.SetFocus(0, 6); // old 11
                    else if (_BankReceipt.PaymentSubType == 3 || (_BankReceipt.PaymentSubType == 0))
                        mpMSCSale.SetFocus(0, 13);
                    else
                        txtNarration.Focus();
                }
                else
                    mpMSCSale.SetFocus(0, 13);
            }
            else if (e.KeyCode == Keys.Up)
                txtChequeNumber.Focus();
        }

        private void mpMSCSale_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                double getclearedamt = 0;
                if (e.ColumnIndex == 13 && (_BankReceipt.PaymentSubType == 3 || _BankReceipt.PaymentSubType == 0))// old 11
                {
                    double mbalanceamount = 0;
                    double mamtnotadj = 0;
                    double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
                    _BankReceipt.CellOldValueAmount = getclearedamt;
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString().Trim() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceamount);
                    if (getclearedamt == 0 && mamtnotadj != 0)
                    {
                        double clearedamt = 0;
                        clearedamt = Math.Min(mamtnotadj, mbalanceamount);
                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;

                    }
                    //mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].ReadOnly = false;
                    //mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
                }
                //else
                //{
                //    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].ReadOnly = true;
                //    mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = true;
                //}
                
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void mpMSCSale_OnCellValueChangeCommited(int colIndex)
        {
            //  txtAmountReceived.Enabled = false;
            double totalreceived = 0;
            double getclearedamt = 0;
            double mbalanceAmount = 0;
            double mamtnotadj = 0;
            double clearedamt = 0;
            double mbillamt = 0;
            double mdiscountamt = 0;
            double.TryParse(txtAmountReceived.Text, out mbillamt);
            double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);

            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
            double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceAmount);

            clearedamt = Math.Min(mamtnotadj, mbalanceAmount);
            try
            {
                if (colIndex == 6)
                {
                    ColCheckChecked();
                }
                if (colIndex == 13) // old 8
                {
                    if (_BankReceipt.PaymentSubType == 3 || _BankReceipt.PaymentSubType == 0)
                    {
                        if (getclearedamt == 0)
                    {
                        _BankReceipt.CellOldValueAmount = 0;
                    }

                    if (mbalanceAmount < getclearedamt)
                    {
                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
                        _BankReceipt.CellOldValueAmount = clearedamt;
                    }
                    else
                    {
                        if (mamtnotadj == 0)
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankReceipt.CellOldValueAmount;


                        //if (clearedamt <= Math.Min(mamtnotadj, mbalanceAmount) || (clearedamt <= _CashReceipt.CellOldValueAmount))
                        //{

                        _BankReceipt.CellOldValueAmount = getclearedamt;
                        foreach (DataGridViewRow dr in mpMSCSale.Rows)
                        {
                            double mcleared = 0;
                            double mdiscount = 0;
                            if (dr.Cells["Col_GetClearedAmount"].Value != null)
                                double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                            if (dr.Index == mpMSCSale.MainDataGridCurrentRow.Index)
                                mcleared = getclearedamt;
                            if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
                            if (mcleared > 0)
                                    totalreceived += mcleared;
                                //totalreceived += mcleared + mdiscount;
                            }
                        mamtnotadj = (mbillamt - totalreceived);

                        if (mamtnotadj < 0)
                        {
                            mamtnotadj = 0;
                            _BankReceipt.CellOldValueAmount = 0;
                            _BankReceipt.CellOldValueDiscount = 0;
                            getclearedamt = 0;
                        }
                            totalreceived = 0;
                            foreach (DataGridViewRow dr in mpMSCSale.Rows)
                            {
                                double mcleared = 0;
                                double mdiscount = 0;
                                if (dr.Cells["Col_GetClearedAmount"].Value != null)
                                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                                if (dr.Index == mpMSCSale.MainDataGridCurrentRow.Index)
                                    mcleared = getclearedamt;
                                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                    double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
                                if (mcleared > 0)
                                    totalreceived += mcleared;
                                // totalreceived += mcleared + mdiscount;
                            }

                            mamtnotadj = (mbillamt - totalreceived);
                            txtAmtNotAdjusted.Text = mamtnotadj.ToString("#0.00");
                        if (mamtnotadj >= 0 && getclearedamt > 0)
                        {
                            mdiscountamt = mbalanceAmount - getclearedamt;
                                if (General.CurrentSetting.MsetCashBankShowDiscount != "Y")   //Amar
                                {
                                    if (mdiscountamt > 10)
                                        mdiscountamt = 0;
                                }
                        }
                        else
                            mdiscountamt = 0;
                            if (General.CurrentSetting.MsetCashBankShowDiscount != "Y")   //Amar
                            {
                                if (mdiscountamt > 10)
                                    mdiscountamt = 0;
                            }

                        if (getclearedamt == 0)
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                        else
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");
                            //  mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
                            if (mdiscountamt == 0)
                            {
                                mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = true;
                                mpMSCSale.NextRowColumn = 13;
                            }
                            else
                            {
                                mpMSCSale.NextRowColumn = 14;
                                mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;

                            }
                        }
                            //int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
                            //if (mpMSCSale.Rows.Count > rowindex + 1)
                            //    mpMSCSale.SetFocus(rowindex + 1, 9);
                            //}
                        
                    }
                    //else
                    //{
                    //    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = mbalanceAmount.ToString("#0.00");
                    //}
                }
                else if (colIndex == 14)
                {
                    double mdiscgiven = 0;
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
                    mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
                    if (mdiscgiven != 0)
                    {
                        if (mdiscgiven != mdiscountamt || getclearedamt == 0)
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                    }
                    totalreceived = 0;
                    foreach (DataGridViewRow dr in mpMSCSale.Rows)
                    {
                        double mcleared = 0;
                        double mdiscount = 0;
                        if (dr.Cells["Col_GetClearedAmount"].Value != null)
                            double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);

                        if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                            double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);

                        totalreceived += mcleared;
                        //totalreceived += (mcleared + mdiscount);
                    }
                    if (mbillamt - totalreceived > 0)
                        mamtnotadj = (mbillamt - totalreceived);
                    else
                        mamtnotadj = 0;
                    txtAmtNotAdjusted.Text = mamtnotadj.ToString("#0.00");
                    //double mdiscgiven = 0;
                    //if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                    //    double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
                    //mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
                    //if (mdiscgiven != 0)
                    //{
                    //    if (mdiscgiven != mdiscountamt || getclearedamt == 0)
                    //        mpMSCSale.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
                    //}
                    ////if (mpMSCSale.MainDataGridCurrentRow.Index < mpMSCSale.Rows.Count-1)

                    ////    mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 12);
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void ColCheckChecked()
        {
            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value == null || mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value.ToString().Trim() == string.Empty)
            {
                mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value = ((char)0x221A).ToString();
            }
            else
                mpMSCSale.MainDataGridCurrentRow.Cells["Col_Check"].Value = " ";
            double totamount = 0;
            double amt = 0;
            foreach (DataGridViewRow dr in mpMSCSale.Rows)
            {
                if (dr.Cells["Col_Check"].Value.ToString().Trim() != string.Empty && dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != string.Empty)
                {
                    amt = Convert.ToDouble(dr.Cells["Col_BalanceAmount"].Value.ToString());
                    dr.Cells["Col_GetClearedAmount"].Value = amt.ToString("#0.00");
                    totamount += amt;
                }
                else
                    dr.Cells["Col_GetClearedAmount"].Value = "0.00";
            }
            // mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
            txtAmountReceived.Text = totamount.ToString("#0.00");
            //  mpMSCSale.Refresh();     
            mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 6);
        }
        private void mpMSCSale_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 13 && (txtAmountReceived.Text != txtTotalBalance.Text && mpMSCSale.ColumnsMain["Col_Check"].ReadOnly != false))// old 12
                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankReceipt.CellOldValueAmount.ToString("#0.00");
                //if (e.ColumnIndex == 13) // old 8
                //    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankReceipt.CellOldValueAmount.ToString("#0.00");

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                ModifyEdit();
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
                if (datePickerFromDate.Visible == true)
                    datePickerFromDate.Focus();
                else
                    txtAmountReceived.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbBank_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbBank.SelectedID;
                FillBankCombo();
                mcbBank.SelectedID = selectedId;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbBranch_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbBranch.SelectedID;
                FillBranchCombo();
                mcbBranch.SelectedID = selectedId;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtAmountReceived_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClearGrid();
               mcbBank.Focus();
                mpMSCSale.ClearSelection();
                _BankReceipt.PaymentSubType = 0;

                if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == 0 && _Mode == OperationMode.Add)
                {
                    mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                    mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
                    mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = false;
                }
                else
                {

                    mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                    mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = false;
                    mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;
                }
                if (_Mode == OperationMode.Add)
                {

                    if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == Convert.ToDouble(txtTotalBalance.Text.ToString()))
                    {
                        _BankReceipt.PaymentSubType = 1;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
                        FillGridForDates();
                    }
                    else if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == 0)
                    {
                        _BankReceipt.PaymentSubType = 2;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = false;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = true;
                    }
                    else
                    {
                       _BankReceipt.PaymentSubType = 3;
                        mpMSCSale.ColumnsMain["Col_Check"].ReadOnly = true;
                        mpMSCSale.ColumnsMain["Col_GetClearedAmount"].ReadOnly = false;
                        mpMSCSale.ColumnsMain["Col_DiscountAmount"].ReadOnly = false;
                    }
                }

               // txtChequeNumber.Focus();
                mcbBank.Focus();

            }
            else if (e.KeyCode == Keys.Up)
                mcbCreditor.Focus();
            //if (e.KeyCode == Keys.Enter)
            //{
            //    FillGridForDates();
            //    mcbBank.Focus();
            //}
            //else if (e.KeyCode == Keys.Up)
            //    mcbCreditor.Focus();
        }

        private void FillGridForDates()
        {
            //double clearedAmount = 0;
            //double balanceAmount = 0;
            if (Convert.ToDouble(txtAmountReceived.Text.ToString()) == Convert.ToDouble(txtTotalBalance.Text.ToString()))
            {
                txtAmtNotAdjusted.Text = "0.00";
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {
                    if (dr.Cells["Col_BalanceAmount"].Value != null && dr.Cells["Col_BalanceAmount"].Value.ToString() != string.Empty)
                    {
                        dr.Cells["Col_GetClearedAmount"].Value = dr.Cells["Col_BalanceAmount"].Value;
                    }
                }
            }
            mcbBank.Focus();
        }

        private void mcbBank_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbBranch.Focus();
        }

        private void mcbBankAccount_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbBankAccount.SelectedID != null)
                _BankReceipt.CBBankAccountID = mcbBankAccount.SelectedID;
            mcbCreditor.Focus();
        }

        private void mcbBranch_EnterKeyPressed(object sender, EventArgs e)
        {
            txtChequeNumber.Focus();
        }

        private void txtChequeNumber_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    datePickerChequeDate.Focus();
                    break;
                case Keys.Up:
                    mcbBranch.Focus();
                    break;
            }
        }
        private void txtChequeNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void datePickerChequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNarration.Focus();
            else if (e.KeyCode == Keys.Up)
                txtChequeNumber.Focus();
        }
        #endregion


        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtVouchernumber.Text != "")
                    {
                        _BankReceipt.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _BankReceipt.CBVouSeries = txtVoucherSeries.Text.ToString();

                        _BankReceipt.ReadDetailsByIDVoucherNumber();
                        if (mpMSVC.Rows.Count > 1)
                        {
                            mpMSVC.SetFocus(mpMSVC.Rows.Count - 1, 1);
                            System.Threading.Thread.Sleep(10);
                        }
                        FillSearchData(_BankReceipt.Id, "");
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
        }

        private void mcbBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                mcbBank.Focus();
        }

        private void UclBankReceipt_Load(object sender, EventArgs e)
        {
            datePickerBillDate.Value = General.TodayDateTime;
            dgClearOpeningBalance.Visible = false;
            datePickerFromDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
            datePickerToDate.Value = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);           
            GetDefaultBank();
            _preBankID = mcbBankAccount.SelectedID;
        }

        private void mpMSCSale_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            ClearGrid();

            mcbCreditor.Focus();
        }

        private void btnClearOpeningBalance_Click(object sender, EventArgs e)
        {
            //btnClearOpeningBalanceClick();
        }

        private void btnClearOpeningBalanceClick()
        {
            double opamt = 0;
            try
            {
                if (mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value != null && mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value.ToString() != string.Empty)
                    opamt = Convert.ToDouble(mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value.ToString());
                txtOpeningBalanceAmount.Text = opamt.ToString("#0.00");
                if (opamt > 0 && dgClearOpeningBalance.Rows.Count > 0)
                {
                    dgClearOpeningBalance.Visible = true;
                    dgClearOpeningBalance.Enabled = true;
                    dgClearOpeningBalance.SetFocus(0, 12);
                }
                else if (opamt > 0)

                    lblFooterMessage.Text = "No Previous Records...";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void dgClearOpeningBalance_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    double getclearedamt = 0;
            //    if (e.ColumnIndex == 12) // old 8
            //    {
            //        double mbalanceamount = 0;
            //        double mamtnotadj = 0;
            //        // txtOpeningBalanceAmount.Text = mpMSCSale.MainDataGridCurrentRow.Cells[12].ToString();
            //        double.TryParse(txtOpeningBalanceAmount.Text, out mamtnotadj);
            //        if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
            //            double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
            //        _BankReceipt.CellOpeningBalanceOldValueAmount = getclearedamt;
            //        if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString().Trim() != "")
            //            double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceamount);
            //        if (getclearedamt == 0 && mamtnotadj != 0)
            //        {
            //            double clearedamt = 0;
            //            clearedamt = Math.Min(mamtnotadj, mbalanceamount);
            //            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;

            //        }
            //        dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].ReadOnly = false;
            //    }
            //}
            //catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgClearOpeningBalance_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 12) // old 8
                    dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankReceipt.CellOpeningBalanceOldValueAmount.ToString("#0.00");

            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
        }

        private void dgClearOpeningBalance_OnCellValueChangeCommited(int colIndex)
        {
            //txtAmountReceived.Enabled = false;
            //txtOpeningBalanceAmount.Enabled = false;
            //double totalreceived = 0;
            //double getclearedamt = 0;
            //double mbalanceAmount = 0;
            //double mamtnotadj = 0;
            //double clearedamt = 0;
            //double mbillamt = 0;
            //double mdiscountamt = 0;
            //mbillamt = Convert.ToDouble(mpMSCSale.Rows[0].Cells["Col_GetClearedAmount"].Value.ToString());
            //double.TryParse(txtOpeningBalanceAmount.Text, out mamtnotadj);

            //if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
            //    double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
            //if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
            //    double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscountamt);
            //double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalanceAmount);

            //clearedamt = Math.Min(mamtnotadj, mbalanceAmount);
            //try
            //{

            //    if (colIndex == 12) // old 8
            //    {

            //        if (getclearedamt == 0)
            //        {
            //            _BankReceipt.CellOpeningBalanceOldValueAmount = 0;
            //        }

            //        if (mbalanceAmount < getclearedamt)
            //        {
            //            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
            //            _BankReceipt.CellOpeningBalanceOldValueAmount = clearedamt;
            //        }
            //        else
            //        {
            //            if (mamtnotadj == 0)
            //                dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankReceipt.CellOpeningBalanceOldValueAmount;


            //            //if (clearedamt <= Math.Min(mamtnotadj, mbalanceAmount) || (clearedamt <= _CashReceipt.CellOldValueAmount))
            //            //{

            //            _BankReceipt.CellOpeningBalanceOldValueAmount = getclearedamt;
            //            foreach (DataGridViewRow dr in dgClearOpeningBalance.Rows)
            //            {
            //                double mcleared = 0;
            //                double mdiscount = 0;
            //                if (dr.Cells["Col_GetClearedAmount"].Value != null)
            //                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
            //                if (dr.Index == dgClearOpeningBalance.MainDataGridCurrentRow.Index)
            //                    mcleared = getclearedamt;
            //                if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
            //                    double.TryParse(dr.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscount);
            //                if (mcleared > 0)
            //                    totalreceived += mcleared;
            //            }

            //            mamtnotadj = (mbillamt - totalreceived);

            //            if (mamtnotadj < 0)
            //            {
            //                mamtnotadj = 0;
            //                _BankReceipt.CellOpeningBalanceOldValueAmount = 0;
            //                _BankReceipt.CellOpeningBalanceOldValueDiscount = 0;
            //                getclearedamt = 0;
            //            }
            //            txtOpeningBalanceAmount.Text = mamtnotadj.ToString("#0.00");
            //            if (mamtnotadj >= 0 && getclearedamt > 0)
            //            {
            //                mdiscountamt = mbalanceAmount - getclearedamt;
            //                if (mdiscountamt > 10)
            //                    mdiscountamt = 0;
            //            }
            //            else
            //                mdiscountamt = 0;


            //            if (getclearedamt == 0)
            //                dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
            //            else
            //                dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");
            //            dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].ReadOnly = false;
            //            //int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
            //            //if (mpMSCSale.Rows.Count > rowindex + 1)
            //            //    mpMSCSale.SetFocus(rowindex + 1, 9);
            //            //}
            //        }
            //    }
            //    else if (colIndex == 13)
            //    {
            //        double mdiscgiven = 0;
            //        if (dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value != null && dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString() != "")
            //            double.TryParse(dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value.ToString(), out mdiscgiven);
            //        mdiscountamt = Math.Round(mbalanceAmount - getclearedamt, 2);
            //        if (mdiscgiven != 0)
            //        {
            //            if (mdiscgiven != mdiscountamt || getclearedamt == 0)
            //                dgClearOpeningBalance.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = "0.00";
            //        }
            //        //if (mpMSCSale.MainDataGridCurrentRow.Index < mpMSCSale.Rows.Count-1)

            //        //    mpMSCSale.SetFocus(mpMSCSale.MainDataGridCurrentRow.Index + 1, 12);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
        }

        private void dgClearOpeningBalance_OnEscapeKeyPressed(object sender, EventArgs e)
        {
        }

        private bool FillOpeningBalanceGrid(string vmode)
        {
            bool retValue = false;
            try
            {
                ConstructdgClearOpeningBalanceTempMainColumns();

                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_BillAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_BalanceAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_ClearedAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_GetClearedAmount");
                //dgClearOpeningBalanceTemp.DoubleColumnNames.Add("Col_DiscountAmount");
                //dgClearOpeningBalanceTemp.DateColumnNames.Add("Col_VoucherDate");
                //dgClearOpeningBalanceTemp.DateColumnNames.Add("Col_BillFromDate");



                ConstructdgClearOpeningBalanceColumns();

                dgClearOpeningBalance.DoubleColumnNames.Add("Col_GetClearedAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_BillAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_BalanceAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_ClearedAmount");
                dgClearOpeningBalance.DoubleColumnNames.Add("Col_DiscountAmount");
                dgClearOpeningBalance.DateColumnNames.Add("Col_VoucherDate");
                dgClearOpeningBalance.DateColumnNames.Add("Col_BillFromDate");

                DataTable dtable = new DataTable();
                if (_BankReceipt.CBAccountID != null && _BankReceipt.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_BankReceipt.ActualAccountID != _BankReceipt.CBAccountID && _BankReceipt.ModifyEdit == "Y"))
                    {
                        _saledtable = _BankReceipt.ReadOldBillDetailsByID();
                        _statementdtable = _BankReceipt.ReadOldStatementDetailsByID();
                        dgClearOpeningBalance.Rows.Clear();
                        BinddgClearOpeningBalanceGrid(_saledtable, _statementdtable);
                        //IfOpeningAdded = false;
                        //BinddgClearOpeningBalanceGrid(_saledtable, _statementdtable);
                        NoofRows();
                    }
                    else
                    {
                        dgClearOpeningBalanceTemp.Rows.Clear();
                        _statementdtable = null;
                        if (vmode == "C")
                            _saledtable = _BankReceipt.ReadOldBillDetailsByCSRIDForChanged();
                        else if (vmode == "D")
                            _saledtable = _BankReceipt.ReadOldBillDetailsByCSRIDForDeleted();
                        else
                        {
                            /////////  _saledtable = _CashReceipt.ReadOldBillDetailsByCSRID();
                            //  _statementdtable = _CashReceipt.ReadStatementDetailsByCSRID();

                            _saledtable = _BankReceipt.ReadOldBillDetailsByID();
                            _statementdtable = _BankReceipt.ReadOldStatementDetailsByID();
                            dgClearOpeningBalance.Rows.Clear();
                            BinddgClearOpeningBalanceGrid(_saledtable, _statementdtable);

                        }
                        BinddgClearOpeningBalanceTempGrid(_saledtable, _statementdtable);
                        // NoofRowsFormpMSVCGrid();
                        //foreach (DataRow dr in _saledtable.Rows)
                        //{
                        //    if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForOpeningBalance)
                        //    {
                        //        _CashReceipt.OpeningClearedInVoucher = Convert.ToDouble(dr["AmountClear"].ToString());
                        //        _CashReceipt.DiscountInOpeningBalance = Convert.ToDouble(dr["DiscountAmount"].ToString());
                        //        break;
                        //    }
                        //}
                        retValue = true;
                        //txtVouchernumber.BackColor = Color.White;
                        //btnModify.BackColor = General.ControlFocusColor;
                    }

                }

                retValue = true;

            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        private void BinddgClearOpeningBalanceTempGrid(DataTable saletable, DataTable statementtable)
        {
            // view
            mpMSVC.Rows.Clear();
            int _rowIndex = 0;
            IfOpeningAdded = false;
            if (_Mode == OperationMode.Add)
            {
                if ((_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared) > 0)
                {
                    _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                    DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = "OPB";
                    currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                    currentdr.Cells["Col_BillType"].Value = "OPB";
                    currentdr.Cells["Col_BillNumber"].Value = "";
                    currentdr.Cells["Col_BillSubType"].Value = "";
                    currentdr.Cells["Col_BillFromDate"].Value = "";
                    currentdr.Cells["Col_BillAmount"].Value = _BankReceipt.OpeningBalance.ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = "";
                    currentdr.Cells["Col_BalanceAmount"].Value = (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared).ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = "";
                    IfOpeningAdded = true;
                }
            }
            if (saletable != null && saletable.Rows.Count > 0)
            {
                foreach (DataRow dr in saletable.Rows)
                {
                    _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                    DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                    currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                    currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                    currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                    currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                    currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                    currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                    currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                    currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {

                foreach (DataRow dr in statementtable.Rows)
                {
                    _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                    DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                    currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                    currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                    currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                    currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                    currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                    currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                    currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                    currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                    currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                    currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                }

            }
        }

        private void BinddgClearOpeningBalanceGrid(DataTable saletable, DataTable statementtable)
        {
            //modify
            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            IfOpeningAdded = true;
            try
            {
                if (IfOpeningAdded != true && (_Mode != OperationMode.Edit) && (_Mode == OperationMode.Add || ((_BankReceipt.preAccountID != null && _BankReceipt.preAccountID != "") && _BankReceipt.CBAccountID != _BankReceipt.preAccountID) || _BankReceipt.ModifyEdit == "Y"))
                {
                    if ((_BankReceipt.OpeningClearedInVoucher > 0 || (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared + _BankReceipt.OpeningClearedInVoucher + _BankReceipt.DiscountInOpeningBalance) > 0) && (_BankReceipt.preAccountID == null || _BankReceipt.CBAccountID == _BankReceipt.preAccountID))
                    {
                        _rowIndex = dgClearOpeningBalance.Rows.Add();
                        DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "OPB";
                        currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                        currentdr.Cells["Col_BillType"].Value = "OPB";
                        currentdr.Cells["Col_BillNumber"].Value = "";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = "";
                        currentdr.Cells["Col_BillAmount"].Value = _BankReceipt.OpeningBalance.ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = "";
                        if ((_BankReceipt.preAccountID == null || _BankReceipt.preAccountID == "") || ((_BankReceipt.preAccountID != null || _BankReceipt.preAccountID != "") && _BankReceipt.CBAccountID == _BankReceipt.preAccountID))
                            currentdr.Cells["Col_BalanceAmount"].Value = (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared + _BankReceipt.OpeningClearedInVoucher + _BankReceipt.DiscountInOpeningBalance).ToString();
                        else
                            currentdr.Cells["Col_BalanceAmount"].Value = _BankReceipt.OpeningBalance;
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        IfOpeningAdded = true;
                    }
                }
                else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
                {
                    if (_BankReceipt.OpeningClearedInVoucher >= 0)
                    {
                        if (_BankReceipt.OpeningBalance > 0 && (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared + _BankReceipt.OpeningClearedInVoucher + _BankReceipt.DiscountInOpeningBalance) > 0)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = "OPB";
                            currentdr.Cells["Col_BillSeries"].Value = General.ShopDetail.ShopVoucherSeries;
                            currentdr.Cells["Col_BillType"].Value = "OPB";
                            currentdr.Cells["Col_BillNumber"].Value = "";
                            currentdr.Cells["Col_BillSubType"].Value = "";
                            currentdr.Cells["Col_BillFromDate"].Value = "";
                            currentdr.Cells["Col_BillAmount"].Value = _BankReceipt.OpeningBalance.ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = "";
                            currentdr.Cells["Col_BalanceAmount"].Value = (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared + _BankReceipt.OpeningClearedInVoucher + _BankReceipt.DiscountInOpeningBalance).ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = "";
                        }
                    }
                }
                if (saletable != null && saletable.Rows.Count > 0)
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        ifIDFound = SearchforIDInSaleGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = dgClearOpeningBalance.Rows.Add();
                            DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                            currentdr.Cells["Col_Discountamount"].Value = "0.00";
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }

                }
                if (statementtable != null && statementtable.Rows.Count > 0)
                {

                    foreach (DataRow dr in statementtable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        ifIDFound = SearchforIDInSaleGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = dgClearOpeningBalance.Rows.Add();
                            DataGridViewRow currentdr = dgClearOpeningBalance.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_GetClearedAmount"].Value = "0.00";
                            currentdr.Cells["Col_Discountamount"].Value = "0.00";
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void ConstructdgClearOpeningBalanceColumns()
        {
            DataGridViewTextBoxColumn column;
            dgClearOpeningBalance.ColumnsMain.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.HeaderText = "Series";
                column.Width = 70;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.HeaderText = "Type,";
                column.ReadOnly = true;
                column.Width = 55;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.HeaderText = "Number";
                column.Width = 80;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.HeaderText = "Sub";
                column.Width = 40;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.HeaderText = "Date";
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);

                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.HeaderText = "BalanceAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);



                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);


                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                column.ReadOnly = true;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.HeaderText = "ClearedAmount.";
                column.Width = 110;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);
                //13
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.HeaderText = "Discount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = false;
                dgClearOpeningBalance.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructdgClearOpeningBalanceTempMainColumns()
        {
            DataGridViewTextBoxColumn column;
            dgClearOpeningBalanceTemp.Columns.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "VoucherSeriesT";
                column.Width = 70;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "VoucherType";
                column.ReadOnly = true;
                column.Width = 80;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "VoucherNumber";
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SubType";
                column.Width = 45;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "FromDate";
                column.Width = 80;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.DataPropertyName = "MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);

                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                dgClearOpeningBalanceTemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.Width = 110;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                dgClearOpeningBalanceTemp.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool SaveOldDetails()
        {
            bool returnVal = true;
            _BankReceipt.SerialNumber = 0;
            try
            {
                foreach (DataGridViewRow drow in dgClearOpeningBalance.Rows)
                {
                    if (drow.Cells["Col_GetClearedAmount"].Value != null && drow.Cells["Col_GetClearedAmount"].Value.ToString() != "" &&
                       Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                    {
                        _BankReceipt.SerialNumber += 1;
                        //   _CashReceipt.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        if (drow.Cells["Col_MasterID"].Value != null)
                            _BankReceipt.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                        _BankReceipt.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                        _BankReceipt.DVoucherSeries = drow.Cells["Col_BillSeries"].Value.ToString();
                        _BankReceipt.DVoucherType = drow.Cells["Col_BillType"].Value.ToString();
                        if (drow.Cells["Col_BillNumber"].Value.ToString() != string.Empty)
                            _BankReceipt.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_BillNumber"].Value.ToString());
                        if (drow.Cells["Col_BillSubType"].Value.ToString() != string.Empty)
                            _BankReceipt.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                        if (drow.Cells["Col_BillFromDate"].Value.ToString() != string.Empty)
                            _BankReceipt.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                        _BankReceipt.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                        _BankReceipt.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                        _BankReceipt.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());
                        _BankReceipt.DDiscountAmount = Convert.ToDouble(drow.Cells["Col_DiscountAmount"].Value.ToString());
                        //  returnVal = _CashReceipt.AddParticularsDetailsOldSale();                        

                        if (_BankReceipt.DVoucherType == FixAccounts.VoucherTypeForStatementSale )
                            returnVal = _BankReceipt.UpdateSaleStatementOld();
                        else if (_BankReceipt.DVoucherType == FixAccounts.VoucherTypeForCreditSale )
                            returnVal = _BankReceipt.UpdateSCCBillOld();


                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }

            return returnVal;
        }

        private bool FillOpeningBalanceTempGrid()
        {
            bool retValue = false;
            try
            {
                ConstructdgClearOpeningBalanceTempMainColumns();
                //  FormatOpeningBalanceTempGrid();
                DataTable dtable = new DataTable();
                dtable = _BankReceipt.ReadBillDetailsByCSRIDFromtblOld();
                _statementdtable = _BankReceipt.ReadStatementDetailsByCSRIDFromtblOld();
                //  mpPVCTemp.DataSource .DataSourceMain = dtable;
                BindOpeningBalanceTempGrid(dtable, _statementdtable);
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        private void BindOpeningBalanceTempGrid(DataTable saletable, DataTable statementtable)
        {
            int _rowIndex = 0;
            string iD = "";

            if (saletable != null && saletable.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        //ifIDFound = SearchforIDInSaleGrid(iD);
                        //if (ifIDFound == false)
                        //{
                        _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                        DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}

                    }
                }
                catch (Exception ex)
                {
                    Log.WriteError(ex.ToString());


                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
            {

                foreach (DataRow dr in statementtable.Rows)
                {
                    try
                    {
                        if (dr["ID"] != DBNull.Value)
                            iD = dr["ID"].ToString();
                        _rowIndex = dgClearOpeningBalanceTemp.Rows.Add();
                        DataGridViewRow currentdr = dgClearOpeningBalanceTemp.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        currentdr.Cells["Col_BillSeries"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_BillType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_BillNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_DiscountAmount"].Value = dr["DiscountAmount"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        //}
                    }
                    catch (Exception ex)
                    {
                        Log.WriteError(ex.ToString());

                    }
                }

            }
        }

        private void mcbBank_UpArrowPressed(object sender, EventArgs e)
        {
            txtAmountReceived.Focus();
        }

        private void mcbBranch_UpArrowPressed(object sender, EventArgs e)
        {
            mcbBank.Focus();
        }

        private void mcbCreditor_UpArrowPressed(object sender, EventArgs e)
        {
            mcbBankAccount.Focus();
        }

        #region tooltip

        #endregion

        private void datePickerFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime dd = datePickerFromDate.Value.AddMonths(1).AddDays(-1);
                datePickerToDate.Value = dd;
                datePickerToDate.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                mcbCreditor.Focus();
        }

        private void datePickerToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetData();
                txtAmountReceived.Text = txtTotalBalance.Text;
                txtAmountReceived.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                datePickerFromDate.Focus();
        }
        private void txtAmountReceived_TextChanged(object sender, EventArgs e)
        {
            if (mpMSCSale.ColumnsMain["Col_Check"].ReadOnly == true)
                ClearGrid();
        }

        public void ClearGrid()
        {
            try
            {
                txtAmtNotAdjusted.Text = txtAmountReceived.Text;
               _BankReceipt.CellOldValueAmount = 0;
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {
                    dr.Cells["Col_GetClearedAmount"].Value = 0;
                    dr.Cells["Col_DiscountAmount"].Value = 0;
                    dr.Cells["Col_Check"].Value = " ";
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void pspaymode_SeletectIndexChanged(object sender, EventArgs e)
        {

        }

      

        private void pspaymode_EnterKeyPressed(object sender, EventArgs e)
        {

        }
    }
}
