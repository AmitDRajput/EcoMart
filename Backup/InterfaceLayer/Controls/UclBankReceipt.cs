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
using PharmaSYSRetailPlus.InterfaceLayer.Classes;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclBankReceipt : BaseControl
    {
        #region Declaration
        private BankReceipt _BankReceipt;
        DataTable _saledtable = new DataTable();
        DataTable _statementdtable = new DataTable();
        string DefaultBankID = "";
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
                headerLabel1.Text = "BANK RECEIPT -> NEW";
                FillPartyCombo();

                FillBankAccountCombo();
                GetDefaultBank();
                FillBankCombo();
                FillBranchCombo();

                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();
                btnModify.Visible = false;
                mpMSCSale.Visible = true;               
                mpMSVC.Visible = false;              
                mcbCreditor.Enabled = true;
                txtAmountReceived.Enabled = true;
                pnlVouTypeNo.Enabled = true;
                pnlVou.Enabled = true;
                datePickerBillDate.Value = DateTime.Now;
                datePickerChequeDate.Value = DateTime.Now;
                mcbBankAccount.Focus();
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
            try
            {
                ClearData();
                headerLabel1.Text = "BANK RECEIPT -> EDIT";
                FillmpMSVCGrid("");              
                FillPartyCombo();
                FillBankAccountCombo();
                FillBankCombo();
                FillBranchCombo();

                datePickerBillDate.Value = DateTime.Now;
                btnModify.Visible = true;
                mpMSCSale.Visible = true;              
                mpMSVC.Visible = false;                
                mcbCreditor.Enabled = false;
                txtAmountReceived.Enabled = false;
                pnlVouTypeNo.Enabled = true;
                pnlVou.Enabled = true;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
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
            btnModify.Visible = false;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = true;
            txtAmountReceived.Enabled = true;
            txtVouchernumber.ReadOnly = false;
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
                datePickerBillDate.Value = DateTime.Now;
                FillmpMSVCGrid("");            
                FillPartyCombo();
                FillBankAccountCombo();
                FillBankCombo();
                FillBranchCombo();

                btnModify.Visible = true;
                mpMSCSale.Visible = true;               
                mpMSVC.Visible = false;               
                mcbCreditor.Enabled = false;
                txtAmountReceived.Enabled = false;
                pnlVouTypeNo.Enabled = true;
                pnlVou.Enabled = true;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                txtVouchernumber.Focus();
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
              
                FillmpMSVCGrid("");
                FillPartyCombo();
                FillBankAccountCombo();
                FillBankCombo();
                FillBranchCombo();

                btnModify.Visible = true;
                mpMSCSale.Visible = true;             
                mpMSVC.Visible = false;               
                mcbCreditor.Enabled = false;
                txtAmountReceived.Enabled = false;
                pnlVouTypeNo.Enabled = true;
                pnlVou.Enabled = true;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                txtVouchernumber.Focus();
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
            PrintData();
            ClearData();
            return retValue;
        }

        private void PrintData()
        {
            PrintRow row;
            try
            {
                PrintBill.Rows.Clear();
                Font fnt = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = mpMSCSale.Rows.Count;
                PrintPageNumber = 0;
                int rowcount = 0;
                PrintRowPixel = 0;
                double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                int totalpages = Convert.ToInt32(totpages);
                PrintHeader(totalpages, rowcount, fnt);
                foreach (DataGridViewRow dr in mpMSCSale.Rows)
                {

                    if (dr.Cells["Col_ClearedAmount"].Value != null)
                    {
                        if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                        {
                            PrintRowPixel = 325;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                            PrintBill.Rows.Add(row);
                            //////////_BankReceipt.PrintRowPixel = 418;
                            //////////row = new PrintRow("End--------------", _BankReceipt.PrintRowPixel, 15, fnt);
                            //////////PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintHeader(totalpages, rowcount, fnt);

                            rowcount = 0;
                        }
                        PrintRowPixel += 17;
                        rowcount += 1;
                        row = new PrintRow(dr.Cells["Col_BillSeries"].Value.ToString(), PrintRowPixel, 85, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_BillType"].Value.ToString(), PrintRowPixel, 125, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_BillNumber"].Value.ToString(), PrintRowPixel, 175, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_BillFromDate"].Value.ToString(), PrintRowPixel, 250, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_BalanceAmount"].Value.ToString(), PrintRowPixel, 350, fnt);
                        PrintBill.Rows.Add(row);
                    }
                }
                PrintRowPixel = 325;
                row = new PrintRow(_BankReceipt.CBNarration, PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(_BankReceipt.CBAmount.ToString("#0.00"), PrintRowPixel, 700, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel = 418;
                row = new PrintRow("---", PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);


                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private int PrintHeader(int TotalPages, int Rowcount, Font fnt)
        {
            PrintRow row;
            try
            {
                string billtype = "Bank Receipt";

                PrintRowPixel = PrintRowPixel + 37;
                row = new PrintRow(billtype, PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_BankReceipt.CBVouNo.ToString(), PrintRowPixel, 400, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 36;
                row = new PrintRow(_BankReceipt.CBName, PrintRowPixel, 40, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel = PrintRowPixel + 34;
                string myadd = _BankReceipt.CBAddress1.Trim() + " " + _BankReceipt.CBAddress2.Trim();
                row = new PrintRow(myadd, PrintRowPixel, 30, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 520, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_BankReceipt.CBVouDate, PrintRowPixel, 680, fnt);
                PrintBill.Rows.Add(row);
                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow(page, PrintRowPixel, 750, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;

                string mych = "Chq.No.:" + _BankReceipt.CBChequeNumber.Trim() + " Chq.Date :" + General.GetDateInDateFormat(_BankReceipt.CBChequeDate);
                row = new PrintRow(mych, PrintRowPixel, 80, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;

                mych = "Bank :" + _BankReceipt.CBBankName.Trim() + " Branch :" + _BankReceipt.CBBranchName.Trim();
                row = new PrintRow(mych, PrintRowPixel, 80, fnt);
                PrintBill.Rows.Add(row);


                PrintRowPixel += 34;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            Rowcount = 3;
            return Rowcount;
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
                    LockTable.LockTablesForCashBankReceipts();
                    _BankReceipt.CBAccountID = mcbCreditor.SelectedID;
                    _BankReceipt.CBAmount = Convert.ToDouble(txtAmountReceived.Text.ToString());
                    _BankReceipt.CBNarration = txtNarration.Text.ToString().Trim();

                    if (txtAmtNotAdjusted.Text != null)
                    {
                        _BankReceipt.CBOnAccountAmount = Convert.ToDouble(txtAmtNotAdjusted.Text.ToString());
                    }

                    _BankReceipt.CBBankAccountID = mcbBankAccount.SelectedID;
                    _BankReceipt.CBBankID = mcbBank.SelectedID;
                    _BankReceipt.CBBranchID = mcbBranch.SelectedID;
                    if (txtChequeNumber.Text != null)
                        _BankReceipt.CBChequeNumber = txtChequeNumber.Text.ToString();
                    _BankReceipt.CBChequeDate = datePickerChequeDate.Value.Date.ToString("yyyyMMdd");

                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _BankReceipt.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString().Trim());
                    _BankReceipt.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    if (_Mode == OperationMode.Edit)
                        _BankReceipt.IFEdit = "Y";

                    _BankReceipt.Validate();
                    if (_BankReceipt.IsValid)
                    {
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();
                            _BankReceipt.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _BankReceipt.CreatedBy = General.CurrentUser.Id;
                            _BankReceipt.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _BankReceipt.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            _BankReceipt.CBVouNo = _BankReceipt.GetAndUpdateBKRNumber(General.ShopDetail.ShopVoucherSeries);
                            txtVouchernumber.Text = _BankReceipt.CBVouNo.ToString();
                            retValue = _BankReceipt.AddDetails();
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
                            if (retValue)
                                General.CommitTransaction();
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
                                _BankReceipt.ModifiedBy = General.CurrentUser.Id;
                                _BankReceipt.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _BankReceipt.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                                retValue = _BankReceipt.UpdateDetails();
                                if (retValue)
                                    retValue = DeletePreviousEntry();                               
                                    retValue = RevertPreviousSalesBalance();                               
                                    retValue = saveDetails();
                                if (retValue)
                                    retValue = _BankReceipt.DeleteAccountDetails();
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

                                else
                                {
                                    LockTable.UnLockTables();
                                    MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    retValue = false;
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
                    DateTime mydate = new DateTime(Convert.ToInt32(_BankReceipt.CBVouDate.Substring(0, 4)), Convert.ToInt32(_BankReceipt.CBVouDate.Substring(4, 2)), Convert.ToInt32(_BankReceipt.CBVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    mcbBankAccount.SelectedID = _BankReceipt.CBBankAccountID;
                    mcbBank.SelectedID = _BankReceipt.CBBankID;
                    mcbBranch.SelectedID = _BankReceipt.CBBranchID;                  
                    txtAddress1.Text = _BankReceipt.CBAddress1;
                    txtAddress2.Text = _BankReceipt.CBAddress2;
                    txtNarration.Text = _BankReceipt.CBNarration;
                    txtVouchernumber.Text = _BankReceipt.CBVouNo.ToString();
                    txtVouType.Text = FixAccounts.VoucherTypeForBankReceipt;
                    txtChequeNumber.Text = _BankReceipt.CBChequeNumber.ToString();
                    datePickerChequeDate.Value = General.ConvertStringToDateyyyyMMdd(_BankReceipt.CBChequeDate);
                    txtAmountReceived.Text = _BankReceipt.CBAmount.ToString("#0.00");
                    txtAmtNotAdjusted.Text = _BankReceipt.CBOnAccountAmount.ToString("#0.00");
                    pnlVouTypeNo.Enabled = false;

                    pnlVou.Enabled = false;
                    if (_BankReceipt.IFEdit == "Y")
                    {
                        btnModify.Visible = true;
                        btnModify.Enabled = true;
                        btnModify.Focus();
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
        public override void ReFillData()
        {
            FillBankAccountCombo();
            string preselectedID = "";
            if (mcbCreditor.SelectedID != null)
                preselectedID = mcbCreditor.SelectedID;
            FillPartyCombo();
            mcbCreditor.SelectedID = preselectedID;
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
                _Mode = OperationMode.Edit;
                _BankReceipt.ModifyEdit = "Y";
               
                retValue = FillmpPVC1GridSaleforModify();               

                retValue = RevertPreviousEntry();

                mpMSCSale.Refresh();
                headerLabel1.Text = "BANK RECEIPT -> MODIFY";
                txtAmtNotAdjusted.Text = _BankReceipt.CBAmount.ToString("#0.00");
            //added
                txtAmountReceived.Enabled = true;
            //added
                EnableDisable();
                mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public bool RevertPreviousEntry()
        {
            bool returnVal = true;
            try
            {
                foreach (DataGridViewRow drowMSVC in mpMSVC.Rows)
                {
                    string mSaleID = "";
                    string mcbid = "";
                    double mClearedAmount = 0;
                    if (drowMSVC.Cells["Col_MasterSaleID"].Value != null)
                        mSaleID = drowMSVC.Cells["Col_MasterSaleID"].Value.ToString();
                    if (drowMSVC.Cells["Col_MasterID"].Value != null)
                        mcbid = drowMSVC.Cells["Col_MasterID"].Value.ToString();
                    if (drowMSVC.Cells["Col_ClearedAmount"].Value != null)
                        double.TryParse(drowMSVC.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                    foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
                    {
                        if (drowMSCSale.Cells["Col_MasterID"].Value != null && drowMSCSale.Cells["Col_MasterID"].Value.ToString() == mcbid)
                        {
                            if (drowMSCSale.Cells["Col_ID"].Value != null && drowMSCSale.Cells["Col_ID"].Value.ToString() == mSaleID)
                            {
                                double mbalaceamount = 0;
                                if (drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
                                    double.TryParse(drowMSCSale.Cells["Col_BalanceAmount"].Value.ToString(), out mbalaceamount);

                                drowMSCSale.Cells["Col_BalanceAmount"].ReadOnly = false;
                                drowMSCSale.Cells["Col_BalanceAmount"].Value = mbalaceamount + mClearedAmount;
                                drowMSCSale.Cells["Col_GetClearedAmount"].Value = "";
                                drowMSCSale.Cells["Col_BalanceAmount"].ReadOnly = true;
                                break;
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
                    if (drowPVCTemp.Cells["Col_MasterSaleID"].Value != null)
                        mSaleID = drowPVCTemp.Cells["Col_MasterSaleID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_MasterID"].Value != null)
                        mcbid = drowPVCTemp.Cells["Col_MasterID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_ClearedAmount"].Value != null)
                        double.TryParse(drowPVCTemp.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                    if (mSaleID != null && mClearedAmount != 0)
                    {
                        mvoutype = drowPVCTemp.Cells["Col_BillType"].Value.ToString();
                        if (mvoutype == FixAccounts.VoucherTypeForOpeningBalance)
                        {
                            retValue = _BankReceipt.UpdateOpeningBalanceReducePrevious(_BankReceipt.preAccountID, _BankReceipt.OpeningClearedInVoucher);
                            retValue = _BankReceipt.UpdateOpeningBalanceAddNew();
                        }
                        if (mvoutype == FixAccounts.VoucherTypeForCreditSale)
                            retValue = _BankReceipt.RevertPreviousSalesBalance(mSaleID, mClearedAmount);
                        else if (mvoutype == FixAccounts.VoucherTypeForStatementSale)
                            retValue = _BankReceipt.RevertPreviousStatementBalance(mSaleID, mClearedAmount);
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
                            returnVal = _BankReceipt.AddParticularsDetails();
                            if (returnVal)
                            {
                                if (_BankReceipt.DVoucherType == FixAccounts.VoucherTypeForOpeningBalance)
                                {
                                    if (_Mode == OperationMode.Add)
                                    {
                                        _BankReceipt.OpeningCleared = 0;
                                        if (_BankReceipt.preAccountID != null)
                                           returnVal = _BankReceipt.UpdateOpeningBalanceReducePrevious(_BankReceipt.preAccountID, _BankReceipt.OpeningCleared);
                                        returnVal = _BankReceipt.UpdateOpeningBalanceAddNew();
                                    }
                                }
                                if (_BankReceipt.DVoucherType == FixAccounts.VoucherTypeForStatementSale)
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
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtChequeNumber.Clear();
                txtNoOfRows.Text = "0";
                txtTotalBalance.Text = "0.00";
                txtVouType.Text = FixAccounts.VoucherTypeForBankReceipt;
                datePickerBillDate.ResetText();
                datePickerChequeDate.ResetText();
                txtAmountReceived.Text = "0.00";
                txtAmtNotAdjusted.Text = "0.00";
                mpMSVC.ColumnsMain.Clear();
                mpMSCSale.ColumnsMain.Clear();
                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();
                mcbCreditor.SelectedID = "";
                mcbBank.SelectedID = "";
                mcbBranch.SelectedID = "";
                mcbBankAccount.SelectedID = "";
                this.mcbCreditor.Focus();
                lblMessage.Text = "";
                tsBtnSave.Enabled = true;
                tsBtnSavenPrint.Enabled = true;
                tsBtnDelete.Enabled = true;
                _saledtable = null;
                _statementdtable = null;
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
                mcbCreditor.SourceDataString = new string[7] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccOpeningDebit", "AccClearedAmount" };
                mcbCreditor.ColumnWidth = new string[7] { "0", "20", "200", "200", "150", "0", "0" };
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
                    _BankReceipt.CBAccountID = mcbCreditor.SelectedID;
                    if (_Mode != OperationMode.ReportView)
                     FillmpMSVCGrid("");                   
                    txtAmountReceived.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        
        private bool FillmpPVC1GridSaleforModify()
        {
            bool retValue = false;
            try
            {
                ConstructSaleColumns();
                mpMSCSale.DoubleColumnNames.Add("Col_BillAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_BalanceAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_GetClearedAmount");
                mpMSCSale.DateColumnNames.Add("Col_BillFromDate");
                DataTable dtable = new DataTable();
                dtable = _BankReceipt.ReadBillDetailsByIDforModify();

                IfOpeningAdded = false;
                _statementdtable = _BankReceipt.ReadStatementDetailsByIDforModify();
                mpMSCSale.Rows.Clear();
                BindmpMSCSaleGrid(dtable, _statementdtable);
                IfOpeningAdded = true;
                _saledtable = _BankReceipt.ReadBillDetailsByID();
                _statementdtable = _BankReceipt.ReadStatementDetailsByID();
                BindmpMSCSaleGrid(_saledtable, _statementdtable);
                NoofRows();
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }
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
                    if (_Mode == OperationMode.Add || (_BankReceipt.ActualAccountID != _BankReceipt.CBAccountID && _BankReceipt.ModifyEdit == "Y"))
                    {
                        _saledtable = _BankReceipt.ReadBillDetailsByID();
                        _statementdtable = _BankReceipt.ReadStatementDetailsByID();
                        mpMSCSale.Rows.Clear();
                        BindmpMSVCGrid(_saledtable, _statementdtable);
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
                           // _statementdtable = _BankReceipt.ReadStatementDetailsByBKRID();
                        }
                        BindmpMSVCGrid(_saledtable, _statementdtable);
                        NoofRowsFormpMSVCGrid();
                        foreach (DataRow dr in _saledtable.Rows)
                        {
                            if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForOpeningBalance)
                            {
                               _BankReceipt.OpeningClearedInVoucher = Convert.ToDouble(dr["AmountClear"].ToString());
                                break;
                            }
                        }

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
        private void BindmpMSVCGrid(DataTable saletable, DataTable statementtable)
        {
            mpMSVC.Rows.Clear();
            int _rowIndex = 0;
            if (_Mode == OperationMode.Add)
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
                }
            }
            
            if (saletable != null && saletable.Rows.Count > 0)
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
                    currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                }

            }
            if (statementtable != null && statementtable.Rows.Count > 0)
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
                    currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                    currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();

                }

            }

        }

        private void BindmpMSCSaleGrid(DataTable saletable, DataTable statementtable)
        {

            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            if (_Mode == OperationMode.Add)
            {
                if ((_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared) > 0)
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
                    currentdr.Cells["Col_BalanceAmount"].Value = (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared).ToString();
                    currentdr.Cells["Col_ClearedAmount"].Value = "";
                }
            }
            else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
            {
                if (_BankReceipt.OpeningClearedInVoucher >= 0)
                {
                    if ((_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared + _BankReceipt.OpeningClearedInVoucher) > 0)
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
                        currentdr.Cells["Col_BalanceAmount"].Value = (_BankReceipt.OpeningBalance - _BankReceipt.OpeningCleared + _BankReceipt.OpeningClearedInVoucher).ToString();
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

                mpPVCTemp.DoubleColumnNames.Add("Col_BillAmount");
                mpPVCTemp.DoubleColumnNames.Add("Col_BalanceAmount");
                mpPVCTemp.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSVC.DoubleColumnNames.Add("Col_ClearedAmount");

                DataTable dtable = new DataTable();
                dtable = _BankReceipt.ReadBillDetailsByBKRID();
                _statementdtable = _BankReceipt.ReadStatementDetailsByBKRID();
                mpPVCTemp.DataSourceMain = dtable;
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
                        //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
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
                column.Width = 60;
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
                column.Width = 135;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
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
                column.Width = 135;
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
                column.Width = 135;
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
                column.DataPropertyName = "ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "Series.";
                column.Width = 70;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.ReadOnly = true;
                column.Width = 60;
                mpMSCSale.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 60;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "Sub";
                column.Width = 40;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 135;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpMSCSale.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.Width = 135;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.HeaderText = "ClearedAmount";
                column.Width = 135;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                mpMSCSale.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.Width = 120;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.DataPropertyName = "MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
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
            mpPVCTemp.ColumnsMain.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                mpPVCTemp.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSeries";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "VoucherSeriesT";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "VoucherType";
                column.ReadOnly = true;
                column.Width = 80;
                mpPVCTemp.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "VoucherNumber";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SubType";
                column.Width = 45;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "FromDate";
                column.Width = 80;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                column.DataPropertyName = "MasterSaleID";
                column.HeaderText = "MasterSaleID";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PatientShortName";
                column.DataPropertyName = "PatientShortName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
                column.Width = 110;
                column.Visible = false;
                mpPVCTemp.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region EVENTS

        private void txtAmountReceived_TextChanged(object sender, EventArgs e)
        {
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
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

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
                if (e.KeyCode == Keys.Enter)
                    mpMSCSale.SetFocus(8);
                else if (e.KeyCode == Keys.Up)
                    txtAmountReceived.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtChequeNumber.Focus();
        }

        private void mpMSCSale_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double getclearedamt = 0;
                if (e.ColumnIndex == 9) // old 8
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
                }
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }

        }

        private void mpMSCSale_OnCellValueChangeCommited(int colIndex)
        {
            //  txtAmountReceived.Enabled = false;
            double totalreceived = 0;
            double getclearedamt = 0;
            double mcell7 = 0;
            double mamtnotadj = 0;
            double clearedamt = 0;
            double mbillamt = 0;
            double.TryParse(txtAmountReceived.Text, out mbillamt);
            double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
            if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null)
                double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
            double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mcell7);

            clearedamt = Math.Min(mamtnotadj, mcell7);
            try
            {

                if (colIndex == 9) // old 8
                {

                    if (getclearedamt == 0)
                    {
                        _BankReceipt.CellOldValueAmount = 0;
                    }

                    if (mcell7 < getclearedamt)
                    {
                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
                        _BankReceipt.CellOldValueAmount = clearedamt;
                    }
                    else
                    {
                        if (mamtnotadj == 0)
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankReceipt.CellOldValueAmount;


                        if (clearedamt <= Math.Min(mamtnotadj, mcell7) || (clearedamt <= _BankReceipt.CellOldValueAmount))
                        {

                            _BankReceipt.CellOldValueAmount = getclearedamt;
                            foreach (DataGridViewRow dr in mpMSCSale.Rows)
                            {
                                double mcleared = 0;
                                if (dr.Cells["Col_GetClearedAmount"].Value != null)
                                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                                if (mcleared > 0)
                                    totalreceived += mcleared;
                            }
                            double d = (mbillamt - totalreceived);
                            txtAmtNotAdjusted.Text = d.ToString("#0.00");

                            int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
                            if (mpMSCSale.Rows.Count > rowindex + 1)
                                mpMSCSale.SetFocus(rowindex + 1, 9);
                        }
                    } 
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpMSCSale_OnCellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 9) // old 8
                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankReceipt.CellOldValueAmount.ToString("#0.00");
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
                mcbBank.Focus();
            else if (e.KeyCode == Keys.Up)
                mcbCreditor.Focus();
        }

        private void mcbBank_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbBranch.Focus();
        }

        private void mcbBankAccount_EnterKeyPressed(object sender, EventArgs e)
        {
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
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtVouchernumber.Text != "")
                    {
                        _BankReceipt.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _BankReceipt.ReadDetailsByIDVoucherNumber();
                        FillSearchData(_BankReceipt.Id, "");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                mcbBank.Focus();
        }
     

        #region tooltip

        #endregion
    }
}
