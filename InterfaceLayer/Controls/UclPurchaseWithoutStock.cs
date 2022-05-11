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
using System.IO;
using PharmaSYSDistributorPlus.InterfaceLayer.Classes;

namespace PharmaSYSDistributorPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclPurchaseWithoutStock : BaseControl
    {
        #region Declaration
        private Purchase _Purchase;
    //   private DataTable _BindingSource;
        private DataTable _PaymentDetailsBindingSource;
        //private string purchaseType;
        private BaseControl ViewControl;
        private Form frmView;

   //     private ImportBill _ImportBill;
        #endregion

        #region contructor
        public UclPurchaseWithoutStock()
        {
            InitializeComponent();
            _Purchase = new Purchase();
            SearchControl = new UclPurchaseWithoutStockSearch();
         
           // _ImportBill = null;
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
            _Purchase.Initialise();
            ClearControls();
            return true;
        }
        public override bool Add()
        {

            bool retValue = base.Add();
            ClearData();
            headerLabel1.Text = "PURCHASE WITHOUT STOCK -> NEW";         
            mcbBank.SelectedID = null;
            mcbCreditor.SelectedID = null;
            InitializeMainSubViewControl("");
          
            FillBankCombo();
       
            FillCreditorCombo();
            btnPaymentHistory.Visible = false;
            cbRound.Checked = true;
            mcbCreditor.Enabled = true;
            cbTransactionType.Enabled = true;
            txtNarration.Enabled = true;
            txtBillNumber.Enabled = true;
            txtVouchernumber.Enabled = false;
           
            cbTransactionType.Focus();

            return retValue;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            tsBtnSave.Enabled = true;
            headerLabel1.Text = "PURCHASE WITHOUT STOCK -> EDIT";
            InitializeMainSubViewControl("");
          
            FillCreditorCombo();
            FillBankCombo();
            FillTransactionType();
            btnPaymentHistory.Visible = true;
            mcbCreditor.Enabled = false;
           
            txtNarration.Enabled = false;
            txtBillNumber.Enabled = false;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            cbTransactionType.Enabled = true;
            txtVouType.Text = FixAccounts.VoucherTypeForCreditPurchase;
           

            return retValue;
        }

        public override bool Cancel()
        {
            bool retValue = base.Cancel();
           
            cbTransactionType.Enabled = true;
            return retValue;
        }

        public override bool Exit()
        {
            bool retValue = base.Exit();
            System.IO.File.Delete(General.GetPurchaseTempFile());
          //  _ImportBill = null;
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            headerLabel1.Text = "PURCHASE WITHOUT STOCK -> DELETE";
            ClearData();
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            txtVouchernumber.Focus();
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_Purchase.AmountClearS != 0)
                MessageBox.Show("Payment Done", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

                if (MessageBox.Show("Are you sure you want to delete Purchase information?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                 
                    bool canbedeleted = true;
                    if (canbedeleted)
                    {
                        LockTable.LockTablesForPurchase();
                        retValue = _Purchase.DeleteDetails();
                        retValue = _Purchase.DeletePreviousRecords();
                        retValue = _Purchase.DeleteAccountDetails();                     
                        clearPreviousdebitcreditnotes();
                        LockTable.UnLockTables();                     
                        retValue = true;
                        MessageBox.Show("Successfully Deleted", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _Purchase.AddDeletedDetails();                     
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
            headerLabel1.Text = "PURCHASE WITHOUT STOCK -> VIEW";
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
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
            tsBtnExit.Select();

            return retValue;
        }

        private void GetLastRecord()
        {
            try
            {
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                _Purchase.VoucherSubType = "2";
                _Purchase.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                _Purchase.GetLastRecordForPurchase(_Purchase.VoucherType, _Purchase.VoucherSubType, _Purchase.VoucherSeries);
                FillSearchData(_Purchase.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _Purchase.VoucherType = txtVouType.Text.ToString();
            _Purchase.VoucherSubType = "2";
            dr = _Purchase.GetFirstRecord();
            if (dr != null && dr["PurchaseID"] != DBNull.Value)
            {
                _Purchase.Id = dr["PurchaseID"].ToString();
                FillSearchData(_Purchase.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            //   DataRow dr = null;
            _Purchase.VoucherType = txtVouType.Text.ToString();
            _Purchase.VoucherSubType = "2";
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _Purchase.VoucherType = txtVouType.Text.ToString();
            _Purchase.VoucherSubType = "2";
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _Purchase.VoucherSeries = txtVoucherSeries.Text.ToString();
            else
                _Purchase.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _Purchase.VoucherNumber = i;
                dr = _Purchase.ReadDetailsByVouNumber(i, _Purchase.VoucherType, _Purchase.VoucherSeries , _Purchase.VoucherSubType);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["PurchaseID"] != DBNull.Value)
            {
                _Purchase.Id = dr["PurchaseID"].ToString();
                FillSearchData(_Purchase.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _Purchase.GetLastVoucherNumber(_Purchase.VoucherType, _Purchase.VoucherSubType, _Purchase.VoucherSeries);
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _Purchase.VoucherType = txtVouType.Text.ToString();
            _Purchase.VoucherSubType = "2";
            if (txtVoucherSeries.Text == null || txtVoucherSeries.Text == string.Empty)
                _Purchase.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
            else
                _Purchase.VoucherSeries = txtVoucherSeries.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _Purchase.VoucherNumber = i;
                dr = _Purchase.ReadDetailsByVouNumber(_Purchase.VoucherNumber, _Purchase.VoucherType, _Purchase.VoucherSeries,_Purchase.VoucherSubType);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["PurchaseID"] != DBNull.Value)
            {
                _Purchase.Id = dr["PurchaseID"].ToString();
                FillSearchData(_Purchase.Id, "");
            }
            return retValue;
        }
        public override bool Save()
        {
            bool retValue = false;
            if (txtBillAmount.Text != null && Convert.ToDouble(txtBillAmount.Text.ToString()) > 0)
            {
                FixVoucherType();
                //if (_Mode == OperationMode.Add)
                //{
                IfAdd();
                //}
                if (_Mode == OperationMode.Edit)
                    _Purchase.IFEdit = "Y";
                _Purchase.VoucherSubType = "2";
                _Purchase.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                _Purchase.TransactionText = cbTransactionType.Text.ToString();
                //if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
               
                if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                {
                    LockTable.LocktblVoucherNo();
                    DBGetVouNumbers getno = new DBGetVouNumbers();
                    _Purchase.VoucherNumber = _Purchase.GetAndUpdatePurchaseNumber(_Purchase.VoucherType);
                }

                _Purchase.Validate();


                if (_Purchase.IsValid)
                {
                    try
                    {
                        LockTable.LockTablesForPurchase();
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();

                            _Purchase.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");                            
                            _Purchase.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                            txtVouchernumber.Text = _Purchase.VoucherNumber.ToString();
                            _Purchase.CreatedBy = General.CurrentUser.Id;
                            _Purchase.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _Purchase.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                          //  retValue = _Purchase.AddDetails();
                            _SavedID = _Purchase.Id;
                            if (retValue)
                            {
                                if (_Purchase.AmountCreditNoteS > 0 || _Purchase.AmountDebitNoteS > 0)
                                SaveAndUpdateDebitCreditNote();
                            }
                            if (retValue)
                            {
                                retValue = _Purchase.AddAccountDetails();
                            }
                            if (retValue)
                            {
                                if (_Purchase.IfCashPaid == "Y")
                                {
                                    _Purchase.CBId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    CashPayment _csp = new CashPayment();
                                    _Purchase.CBVouNo = _csp.GetAndUpdateCSPNumber(General.ShopDetail.ShopVoucherSeries);
                                    _Purchase.CBVouType = FixAccounts.VoucherTypeForCashPayment;
                                    retValue = _Purchase.AddCashEntry();
                                }
                                else if (_Purchase.ChequeNumber != "" && _Purchase.BankID != "")
                                {
                                    _Purchase.CBId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    BankPayment _bkp = new BankPayment();
                                    _Purchase.CBVouNo = _bkp.GetAndUpdateBKPNumber(General.ShopDetail.ShopVoucherSeries);
                                    _Purchase.CBVouType = FixAccounts.VoucherTypeForBankPayment;
                                    retValue = _Purchase.AddBankEntry();
                                }
                            }
                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {                               
                                System.IO.File.Delete(General.GetPurchaseTempFile());
                                string msgLine2 = _Purchase.VoucherType + "  " + _Purchase.VoucherNumber.ToString("#0");
                                PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                           //     _ImportBill = null;
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
                            DataTable stocktbl = new DataTable();
                            _Purchase.VoucherNumber = int.Parse(txtVouchernumber.Text);
                            if (cbTransactionType.Text == cbNewTransactionType.Text)
                                _Purchase.IfTypeChange = "N";
                            else
                                _Purchase.IfTypeChange = "Y";
                            if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCash)
                                _Purchase.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                            else if (cbNewTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                                _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                            else
                                _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;

                            General.BeginTransaction();

                            if (_Purchase.IfTypeChange == "Y")
                            {
                                if (_Purchase.OldVoucherType != _Purchase.VoucherType)
                                {
                                    _Purchase.VoucherNumber = _Purchase.GetAndUpdatePurchaseNumber(_Purchase.VoucherType);
                                    txtVouchernumber.Text = _Purchase.VoucherNumber.ToString();
                                    txtVouType.Text = _Purchase.VoucherType;
                                    retValue = _Purchase.UpdateDetailsForTypeChange();
                                    if (retValue)
                                    {
                                        retValue = _Purchase.DeleteAccountDetails();
                                    }
                                    if (retValue)
                                    {
                                        retValue = _Purchase.AddAccountDetails();
                                    }
                                    if (retValue)
                                        _Purchase.UpdateCreditDebitNoteforTypeChange(_Purchase.CreditDebitNoteID, _Purchase.Amount, _Purchase.VoucherType, _Purchase.VoucherNumber, _Purchase.VoucherDate, _Purchase.PurchaseBillNumber, _Purchase.Id);

                                    if (retValue)
                                        General.CommitTransaction();
                                    else
                                        General.RollbackTransaction();
                                    LockTable.UnLockTables();
                                    if (retValue)
                                    {
                                        string msgLine2 = _Purchase.VoucherType + "  " + _Purchase.VoucherNumber.ToString("#0");
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
                        else
                        {
                            DataTable stocktbl = new DataTable();
                            _Purchase.VoucherNumber = int.Parse(txtVouchernumber.Text);
                             General.BeginTransaction();
                            _Purchase.VoucherDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                            _Purchase.ModifiedBy = General.CurrentUser.Id;
                            _Purchase.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _Purchase.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");

                            retValue = true;
                            if (retValue)
                            { 
                                 retValue = _Purchase.UpdateDetails();
                                _SavedID = _Purchase.Id;

                                if (retValue)
                                {
                                    clearPreviousdebitcreditnotes();
                                    if (retValue && (_Purchase.AmountCreditNoteS > 0 || _Purchase.AmountDebitNoteS > 0))
                                        retValue = SaveAndUpdateDebitCreditNote();

                                    if (retValue)
                                    {                                       
                                            retValue = _Purchase.DeleteAccountDetails();
                                            _Purchase.CreatedBy = _Purchase.ModifiedBy;
                                            _Purchase.CreatedDate = _Purchase.ModifiedDate;
                                            _Purchase.CreatedTime = _Purchase.ModifiedTime;
                                            retValue = _Purchase.AddAccountDetails();
                                        
                                    }
                                    if (retValue)
                                        General.CommitTransaction();
                                    else
                                        General.RollbackTransaction();
                                    LockTable.UnLockTables();
                                    if (retValue)
                                    {
                                        _Purchase.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                        _Purchase.AddChangedDetails();                                    
                                        MessageBox.Show("Information has been Updated successfully.'", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                    MessageBox.Show("Can not Update Stock < 0", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    General.RollbackTransaction();
                                    LockTable.UnLockTables();
                                }
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
                    foreach (string _message in _Purchase.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            LockTable.UnLockTables();
            return retValue;
        }
        private void FixVoucherTypeBycbTransactionType()
        {
            _Purchase.VoucherType = "";
            if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
            else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
            else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCreditStatement)
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
            txtVouType.Text = _Purchase.VoucherType;
        }
        public void FixVoucherType()
        {
            _Purchase.EntryDate = Convert.ToString(DateTime.Now);
            FixVoucherTypeBycbTransactionType();
            if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCashPurchase)
                _Purchase.PurchaseAccount = FixAccounts.AccountCashPurchase;
            else if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCreditStatementPurchase)
                _Purchase.PurchaseAccount = FixAccounts.AccountCreditPurchase;
            else if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCreditPurchase)
                _Purchase.PurchaseAccount = FixAccounts.AccountCashCreditPurchase;
            txtVouType.Text = _Purchase.VoucherType;
            if (mcbCreditor.SelectedID != null)
                _Purchase.AccountID = this.mcbCreditor.SelectedID;
            _Purchase.PurchaseBillNumber = txtBillNumber.Text;
        }


        public void IfAdd()
        {
            _Purchase.AmountItemDiscountS = Convert.ToDouble(txtItemDiscountS.Text.ToString());           
            _Purchase.AmountSchemeDiscountS = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
            _Purchase.CashDiscountPercentageS = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
            _Purchase.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
            if (txtAddOnS.Text.ToString() != "")
                _Purchase.AmountAddOnFreightS = Convert.ToDouble(txtAddOnS.Text.ToString());
           
            if (txtCRAmountS.Text.ToString() != "")
                _Purchase.AmountCreditNoteS = Convert.ToDouble(txtCRAmountS.Text.ToString());
            _Purchase.AmountDebitNoteS = Convert.ToDouble(txtDBAmountS.Text.ToString());
            if (txtOCTPerS.Text != "")
                _Purchase.OctroiPercentageS = Convert.ToDouble(txtOCTPerS.Text.ToString());
            if (txtOCTAmountS.Text != "")
                _Purchase.AmountOctroiS = Convert.ToDouble(txtOCTAmountS.Text.ToString());
            _Purchase.Narration = txtNarration.Text.ToString();
            _Purchase.RoundUpAmountS = Convert.ToDouble(txtRoundUPS.Text.ToString());
            _Purchase.AmountVAT5PercentS = Convert.ToDouble(txtVAT5AmountS.Text.ToString());
            _Purchase.AmountVAT12point5PercentS = Convert.ToDouble(txtVAT12Point5AmountS.Text.ToString());
            if (txtPurchaseAmountVATZeroS.Text != null && txtPurchaseAmountVATZeroS.Text != "")
                _Purchase.PurchaseAmountZeroVATS = Convert.ToDouble(txtPurchaseAmountVATZeroS.Text.ToString());
            //if (txtpuramount0.Text.ToString() != "")
            //    _Purchase.PurchaseAmount0PercentVATS = Convert.ToDouble(txtPurchaseAmountVATZeroS.Text.ToString());
            if (txtPurchaseAmountVAT5S.Text.ToString() != "")
                _Purchase.PurchaseAmount5PercentVATS = Convert.ToDouble(txtPurchaseAmountVAT5S.Text.ToString());
            if (txtPurchaseAmountVAT12point5S.Text.ToString() != "")
                _Purchase.PurchaseAmount12point5PercentVATS = Convert.ToDouble(txtPurchaseAmountVAT12point5S.Text.ToString());

            if (_Mode == OperationMode.Add)
            {
                _Purchase.CBVouType = "";
                _Purchase.VoucherType = "";
                _Purchase.IfCashPaid = "N";
                if (txtChequeNumber.Text != null && txtChequeNumber.Text != "")
                {
                    _Purchase.ChequeNumber = txtChequeNumber.Text.ToString();

                    if (mcbBank.SelectedID != null)
                    {
                        _Purchase.BankID = mcbBank.SelectedID;
                    }
                    _Purchase.ChequeDate = datePickerChqDate.Value.Date.ToString("yyyyMMdd");
                }


                if (_Purchase.IfCashPaid == "Y" || (_Purchase.ChequeNumber != "" && _Purchase.BankID != ""))
                {
                    _Purchase.AmountClearS = _Purchase.AmountNetS;
                }
                if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
                    _Purchase.VoucherType = FixAccounts.VoucherTypeForCashPurchase;
                else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                    _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                else
                    _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
                txtVouType.Text = _Purchase.VoucherType;
            }

        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _Purchase.Id = ID;
                    if (Vmode == "C")
                        _Purchase.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _Purchase.ReadDetailsByIDForDeleted();
                    else
                        _Purchase.ReadDetailsByID();
                  
                    BindPaymentDetails();
                    InitializeMainSubViewControl(Vmode);                  
                    _Purchase.OldVoucherType = _Purchase.VoucherType;
                    _Purchase.OldVoucherNumber = _Purchase.VoucherNumber;
                    if (_Purchase.StatementNumber.ToString() != "" && _Purchase.StatementNumber > 0)
                        lblFooterMessage.Text = "Statement Number : " + _Purchase.StatementNumber.ToString();
                    else
                        lblFooterMessage.Text = "";
                    if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCashPurchase)
                    {                       
                        cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
                    }
                    else if (_Purchase.VoucherType == FixAccounts.VoucherTypeForCreditStatementPurchase)
                    {                       
                        cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCreditStatement);
                    }
                    else
                    {                      
                        cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
                        cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
                    }

                    txtVouType.Text = _Purchase.VoucherType.ToString();
                    txtVouchernumber.Text = _Purchase.VoucherNumber.ToString();
                    txtBillNumber.Text = _Purchase.PurchaseBillNumber;
                    txtNarration.Text = _Purchase.Narration;
                
                    txtCashDiscountPerS.Text = _Purchase.CashDiscountPercentageS.ToString("#0.00");
                    txtCashDiscountAmountS.Text = _Purchase.AmountCashDiscountS.ToString("#0.00");
                   
                    txtItemDiscountS.Text = _Purchase.AmountItemDiscountS.ToString("#0.00");
                    txtSchemeDiscountS.Text = _Purchase.AmountSchemeDiscountS.ToString("#0.00");
                    txtAddOnS.Text = _Purchase.AmountAddOnFreightS.ToString("#0.00");
                    txtCRAmountS.Text = _Purchase.AmountCreditNoteS.ToString("#0.00");
                    txtDBAmountS.Text = _Purchase.AmountDebitNoteS.ToString("#0.00");
                    txtOCTPerS.Text = _Purchase.OctroiPercentageS.ToString("#0.00");
                    txtOCTAmountS.Text = _Purchase.AmountOctroiS.ToString("#0.00");

                    txtVAT5AmountS.Text = _Purchase.AmountVAT5PercentS.ToString("#0.00");
                    txtVAT12Point5AmountS.Text = _Purchase.AmountVAT12point5PercentS.ToString("#0.00");                   
                    txtPurchaseAmountVATZeroS.Text = _Purchase.PurchaseAmountZeroVATS.ToString("#0.00");
                    txtPurchaseAmountVAT5S.Text = _Purchase.PurchaseAmount5PercentVATS.ToString("#0.00");
                    txtPurchaseAmountVAT12point5S.Text = _Purchase.PurchaseAmount12point5PercentVATS.ToString("#0.00");
                    DateTime mydate = new DateTime(Convert.ToInt32(_Purchase.VoucherDate.Substring(0, 4)), Convert.ToInt32(_Purchase.VoucherDate.Substring(4, 2)), Convert.ToInt32(_Purchase.VoucherDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    txtRoundUPS.Text = _Purchase.RoundUpAmountS.ToString("#0.00");                 
                    txtBillAmountS.Text = _Purchase.AmountS.ToString("#0.00");
                    txtBillAmount.ReadOnly = false;
                    txtBillAmount.Enabled = true;
                    txtBillAmount.Text = _Purchase.AmountNetS.ToString("#0.00");
                    txtNetAmountS.Text = _Purchase.AmountNetS.ToString("#0.00");
                    txtBillAmount.ReadOnly = true;
                    txtBillAmount.Enabled = false;
                    FillBankCombo();

                    FillCreditorCombo(); 

                    mcbCreditor.SelectedID = _Purchase.AccountID;
                    if (_Mode == OperationMode.Fifth && _Purchase.StatementNumber == 0)
                    {
                        btnTypeChange.Visible = true;
                        cbNewTransactionType.Visible = true;
                        cbNewTransactionType.Enabled = false;
                        btnTypeChange.Enabled = true;
                        btnTypeChange.Focus();
                    }
                    if (_Purchase.StatementNumber > 0 || _Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                    {
                        pnlPaymentDetails.Enabled = false;
                        mcbCreditor.Enabled = false;
                    }
                    else
                    {
                        pnlPaymentDetails.Enabled = true;
                        pnlBillDetails.Enabled = true;
                        mcbCreditor.Enabled = true;
                        txtBillNumber.Enabled = true;
                        if (_Mode != OperationMode.Fifth)
                            mcbCreditor.Focus();

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
        public override void ReFillData(Control closedControl)
        {
            try
            {
              
                string oldtrans = cbTransactionType.Text;
                Int32 oldtransindex = cbTransactionType.SelectedIndex;
                FillTransactionType();
                cbTransactionType.Text = oldtrans;
                cbTransactionType.SelectedIndex = oldtransindex;
                if (closedControl is UclAccount)
                {
                    string creditorID = mcbCreditor.SelectedID;
                    FillCreditorCombo();
                    mcbCreditor.SelectedID = creditorID;
                    mcbCreditor.Focus();
                }
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
                        txtBillNumber.Focus();
                        retValue = true;
                }
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                        this.mcbCreditor.Focus();
                        retValue = true;

                }
                if (keyPressed == Keys.D && modifier == Keys.Alt)
                {
                    datePickerBillDate.Focus();
                    retValue = true;
                }
              
               
                if (keyPressed == Keys.H && modifier == Keys.Alt)
                { 
                        txtCashDiscountPerS.Focus();
                        retValue = true;
                }             
               
             
                if (keyPressed == Keys.O && modifier == Keys.Alt)
                {
                    if (pnlDebitCreditNote.Visible == true)
                    {
                        btnCRDBOKClick();
                        retValue = true;
                    }                  

                }  
               
                if (keyPressed == Keys.Escape)
                {
                    
                    if (pnlDebitCreditNote.Visible)
                    {
                        btnCRDBOKClick();
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

       

        

    
      
       

       
        private bool SaveAndUpdateDebitCreditNote()
        {
            {
                bool returnVal = true;
                try
                {
                    foreach (DataGridViewRow prodrow in dgCreditNote.Rows)
                    {
                        if ((prodrow.Cells["Col_CrdbID"].Value) != null && (Convert.ToBoolean(prodrow.Cells["Col_Check"].Value) == true))
                        {
                            _Purchase.CreditDebitNoteID = prodrow.Cells["Col_CrdbID"].Value.ToString();
                            _Purchase.Amount = Convert.ToDouble(prodrow.Cells["Col_AmountNet"].Value.ToString());
                            returnVal = _Purchase.UpdateCreditDebitNoteAdjustedDetails(_Purchase.CreditDebitNoteID, _Purchase.Amount, _Purchase.VoucherType, _Purchase.VoucherNumber, _Purchase.VoucherDate, _Purchase.PurchaseBillNumber, _Purchase.Id, _Purchase.VoucherSeries);
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }

        private bool clearPreviousdebitcreditnotes()
        {
            bool retValue = true;
            retValue = _Purchase.clearPreviousdebitcreditnotes(_Purchase.Id);
            return retValue;
        }

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
        void mcbCreditor_SeletectIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                Account _account = new Account();
                _account.Id = mcbCreditor.SelectedID;
                _Purchase.AccountID = mcbCreditor.SelectedID;
                _account.ReadDetailsByID();
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[5];
                    if (_Mode == OperationMode.Add)
                        txtCashDiscountPerS.Text = _account.AccDiscountOffered.ToString("0.00");
                    _Purchase.GetPendingAmount(mcbCreditor.SelectedID);
                    _Purchase.GetOpeningBalance(mcbCreditor.SelectedID);
                    _Purchase.PendingAmount = _Purchase.OpeningBalance + (_Purchase.TotalDebit - _Purchase.TotalCredit);
                    txtPendingBalance.Text = Math.Abs(_Purchase.PendingAmount).ToString("#0.00");
                    _Purchase.PendingAmount = 0;
                    _Purchase.PendingAmount = _Purchase.GetDNAmount(mcbCreditor.SelectedID);
                    txtPendingCN.Text = _Purchase.PendingAmount.ToString("#0.00");


                    if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                    {
                        txtDBAmountS.Text = "0.00";
                        txtCRAmountS.Text = "0.00";
                        FillCreditDebitNote();                    
                    }


                    txtBillNumber.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
      
        private bool DeletePreviousRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _Purchase.DeletePreviousRecords();
            }
            catch { returnVal = false; }
            return returnVal;
        }
      
        
       
      

        #endregion

        # region Contruct

       
       
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
    

        private void InitializeMainSubViewControl(string vmode)
        {

            try
            {              

                DataTable dtable = new DataTable();
                if (vmode == "C")
                {
                    dtable = _Purchase.ReadProductDetailsByIDForChanged();
                    headerLabel1.Text = "PURCHASE WITHOUT STOCK => Changed Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else if (vmode == "D")
                {
                    dtable = _Purchase.ReadProductDetailsByIDForDeleted();
                    headerLabel1.Text = "PURCHASE WITHOUT STOCK => Deleted Voucher";
                    tsBtnPrint.Enabled = false;
                }
                else
                    dtable = _Purchase.ReadProductDetailsByID();

                if (dtable != null)
                    _Purchase.NoofRows = dtable.Rows.Count;              

                        
              
            }

            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        # region fill or clean

       

        private void BindPaymentDetails()
        {
            try
            {
                ConstructPaymentDetailsColumns();
                DataTable tmptable = new DataTable();
                tmptable = _Purchase.ReadPaymentDetailsByID();
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
               
                txtVouchernumber.Clear();
                txtVouType.Text = "";
                tsBtnSavenPrint.Enabled = false;
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));

                txtTotalS.Text = "0.00";
                txtNetAmountS.Text = "0.00";
                txtPurchaseAmountVATZeroS.Text = "0.00";
                txtPurchaseAmountVAT5S.Text = "0.00";
                txtPurchaseAmountVAT12point5S.Text = "0.00";

              
                txtBillNumber.Clear();
                datePickerChqDate.ResetText();
                txtChequeNumber.Clear();
                txtNarration.Text = "";
                txtAddress1.Clear();
                txtAddress2.Clear();
              
                txtBillAmountS.Text = "0.00";
                txtSchemeDiscountS.Text = "0.00";
                txtItemDiscountS.Text = "0.00";
                _Purchase.VoucherSubType = "2";
             
            
             
                txtAddOnS.Text = "0.00";
                txtCRAmountS.Text = "0.00";
                txtDBAmountS.Text = "0.00";
                txtCashDiscountPerS.Text = "0.00";
                txtCashDiscountAmountS.Text = "0.00";
            
                txtVAT5AmountS.Text = "0.00";
                txtVAT12Point5AmountS.Text = "0.00";              
                txtTotalVATAmountS.Text = "0.00";
                txtOCTPerS.Text = "0.00";
                txtOCTAmountS.Text = "0.00";
              
                txtRoundUPS.Text = "0.00";
              
                txtBillAmount.Text = "0.00";
                txtpuramount12point5.Text = "0.00";
                txtpuramount5.Text = "0.00";
                txtpuramount0.Text = "0.00";
                txtOCTPerS.Text = "0.00";
                mcbCreditor.SelectedID = "";
                txtChequeNumber.Text = "";
              
                mcbBank.SelectedID = "";
                txtPendingBalance.Text = "0.00";
                txtPendingCN.Text = "0.00";
              
                pnlBillDetails.Enabled = true;
                pnlVou.Enabled = true;
             
                lblFooterMessage.Text = "";
                btnTypeChange.Visible = false;
                cbNewTransactionType.Visible = false;
                cbTransactionType.Focus();
                DataTable dtp = new DataTable();
                if (dgPaymentDetails.Rows.Count > 0)
                {
                    dgPaymentDetails.DataSource = dtp;

                }
                if (dgCreditNote.Rows.Count > 0)
                    BindCreditNoteDebitNote(dtp);
                    


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
            //cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCash);
            //if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
            //    cbTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
            cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
            cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
        }
     

        private void FillCreditorCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[6] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccDiscountOffered", "AccAddress2" };
                mcbCreditor.ColumnWidth = new string[6] { "0", "20", "200", "150", "50", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
                mcbCreditor.FillData(dtable);
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
                else
                {
                    BindCreditNoteDebitNote(dt);
                }
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

       



        #endregion

        #region keydown-Click-DoubleClick  
      
       
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtBillNumber.Enabled = true;
            txtBillNumber.Focus();
        }
        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtBillNumber.Text.ToString().Trim() != "")
                    {
                        bool retValue = true;
                        Purchase purbill = new Purchase();
                        _Purchase.PurchaseBillNumber = txtBillNumber.Text.ToString().Trim();
                        if (_Mode == OperationMode.Add)
                            retValue = purbill.CheckForUniqueBillNumberforNew(_Purchase.PurchaseBillNumber, _Purchase.AccountID);
                        else
                            retValue = purbill.CheckForUniqueBillNumberforEdit(_Purchase.Id, _Purchase.PurchaseBillNumber, _Purchase.AccountID);
                        if (retValue == false)
                        {
                            lblFooterMessage.Text = "Purchase Number Already Entered";
                            txtBillNumber.Focus();
                        }
                        else
                        {
                            lblFooterMessage.Text = "";
                            txtNarration.Enabled = true;
                            txtNarration.Focus();

                        }
                    }
                }
                else if (e.KeyCode == Keys.Up)
                    mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtBillNumber_KeyDown>>" + Ex.Message);
            }
        }
      

        #endregion

        #region Calculate Amounts Rates
       
       
       

       
        private void CalculateRoundup()
        {
            try
            {
                txtTotalS.Text = _Purchase.AmountS.ToString("#0.00");
                if (cbRound.Checked == true)
                    _Purchase.RoundUpAmountS = Math.Round(_Purchase.AmountS, 0) - _Purchase.AmountS;
                else
                    _Purchase.RoundUpAmountS = 0;
                _Purchase.AmountNetS = _Purchase.AmountS + _Purchase.RoundUpAmountS;
                txtNetAmountS.Text = _Purchase.AmountNetS.ToString("#0.00");
                txtBillAmount.Text = _Purchase.AmountNetS.ToString("#0.00");
                txtRoundUPS.Text = _Purchase.RoundUpAmountS.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateRoundup>>" + Ex.Message);
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



        private void CalculateFinalSummary()
        {
            _Purchase.AmountBillS = Convert.ToDouble(txtBillAmountS.Text.ToString());
            try
            {

                if (_Purchase.AmountBillS > 0)
                {
                    if (txtSchemeDiscountS.Text.ToString().Trim() != "")
                        _Purchase.AmountSchemeDiscountS = Convert.ToDouble(txtSchemeDiscountS.Text.ToString());
                    if (txtItemDiscountS.Text.ToString().Trim() != "")
                        _Purchase.AmountItemDiscountS = Convert.ToDouble(txtItemDiscountS.Text.ToString());
                    

                    _Purchase.SpecialDiscountPercentS = Math.Round((100 * _Purchase.AmountSpecialDiscountS) / (_Purchase.AmountBillS - _Purchase.AmountItemDiscountS - _Purchase.AmountSchemeDiscountS - _Purchase.AmountOctroiS), 6);

                    if (txtAddOnS.Text.ToString().Trim() != "")
                        _Purchase.AmountAddOnFreightS = Convert.ToDouble(txtAddOnS.Text.ToString());
                    if (txtCRAmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountCreditNoteS = Convert.ToDouble(txtCRAmountS.Text.ToString());
                    if (txtDBAmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountDebitNoteS = Convert.ToDouble(txtDBAmountS.Text.ToString());
                    if (txtCashDiscountPerS.Text.ToString().Trim() != "")
                        _Purchase.CashDiscountPercentageS = Convert.ToDouble(txtCashDiscountPerS.Text.ToString());
                    if (txtCashDiscountAmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
                    if (_Purchase.AmountBillS - _Purchase.AmountSpecialDiscountS - _Purchase.AmountSchemeDiscountS - _Purchase.AmountItemDiscountS + _Purchase.AmountCreditNoteS <= _Purchase.AmountDebitNoteS)
                    {
                        lblFooterMessage.Text = "Invalid Debit Note Amount";
                        _Purchase.AmountDebitNoteS = 0;
                        txtDBAmountS.Text = "0.00";
                        ClearDebitCreditNoteWhenAmountIsLess();
                    }
                    if (_Purchase.AmountCashDiscountS > (_Purchase.AmountBillS - _Purchase.AmountSpecialDiscountS - _Purchase.AmountSchemeDiscountS - _Purchase.AmountItemDiscountS + _Purchase.AmountCreditNoteS - _Purchase.AmountDebitNoteS))
                    {
                        lblFooterMessage.Text = "Invalid Cash Discount";
                        _Purchase.CashDiscountPercentageS = 0;
                        _Purchase.AmountCashDiscountS = 0;
                        txtCashDiscountAmountS.Text = "0.00";
                      
                    }
                    txtCashDiscountAmountS.Text = _Purchase.AmountCashDiscountS.ToString("#0.00");
                
                    if (txtCashDiscountAmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountCashDiscountS = Convert.ToDouble(txtCashDiscountAmountS.Text.ToString());
                    if (txtVAT5AmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountVAT5PercentS = Convert.ToDouble(txtVAT5AmountS.Text.ToString());
                    if (txtVAT12Point5AmountS.Text.ToString().Trim() != "")
                        _Purchase.AmountVAT12point5PercentS = Convert.ToDouble(txtVAT12Point5AmountS.Text.ToString());

                    if (txtOCTPerS.Text.ToString().Trim() != "")
                        _Purchase.OctroiPercentageS = Convert.ToDouble(txtOCTPerS.Text.ToString());
                    if (_Purchase.OctroiPercentageS > 0)
                        _Purchase.AmountOctroiS = Math.Round(_Purchase.TotalAmountForOctroiS * _Purchase.OctroiPercentageS / 100, 2);
                    _Purchase.AmountS = Math.Round(_Purchase.AmountBillS - _Purchase.AmountSchemeDiscountS - _Purchase.AmountItemDiscountS
                        - _Purchase.AmountSpecialDiscountS + _Purchase.AmountAddOnFreightS + _Purchase.AmountCreditNoteS
                        - _Purchase.AmountDebitNoteS - _Purchase.AmountCashDiscountS + _Purchase.AmountVAT5PercentS
                        + _Purchase.AmountVAT12point5PercentS + _Purchase.AmountOctroiS, 2);
                    CalculateRoundup();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateFinalSummary>>" + Ex.Message);
            }

        }
        public void CalculateTotalVATAmount()
        {
            if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
            {


                double mtotvat5 = 0;
                double mtotvat12point5 = 0;
                double mtotvat = 0;
                double mamountpurchase5 = 0;
                double mamountpurchase12point5 = 0;

                try
                {
                    if (txtVAT5AmountS.Text != null && txtVAT5AmountS.Text.ToString() != "")
                    {
                        // vat 5.5
                        double.TryParse(txtVAT5AmountS.Text.ToString(), out mtotvat5);
                        mamountpurchase5 = Math.Round((mtotvat5 * 100) / 6, 2);
                        
                    }
                    if (txtVAT12Point5AmountS.Text != null && txtVAT12Point5AmountS.Text.ToString() != "")
                    {
                        double.TryParse(txtVAT12Point5AmountS.Text.ToString(), out mtotvat12point5);
                        mamountpurchase12point5 = Math.Round((mtotvat12point5 * 100) / 13.5, 2);
                       
                    }
                    mtotvat = Math.Round(mtotvat5, 2) + Math.Round(mtotvat12point5, 2);
                    txtTotalVATAmountS.Text = (mtotvat).ToString("0.00");
                    txtPurchaseAmountVAT5S.Text = mamountpurchase5.ToString("#0.00");
                    txtPurchaseAmountVAT12point5S.Text= mamountpurchase12point5.ToString("#0.00");                    
                }
                catch (Exception Ex)
                {
                    Log.WriteError("UclPurchase.CalculateTotalVATAmount>>" + Ex.Message);
                }
            }
        }
        # endregion

        # region summary keydown textchange
        private void txtAddOnS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {                   
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
                    datePickerBillDate.Focus();
                else if (e.KeyCode == Keys.Up)
                    txtBillNumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.txtNarration_KeyDown>>" + Ex.Message);
            }
        }
       
       

        private void txtVAT5AmountS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    txtVAT12Point5AmountS.Focus();
                    //txtDBAmountS.Focus();
                    CalculateTotalVATAmount();
                    CalculateFinalSummary();
                }
                else if (e.KeyCode == Keys.Up)
                {
                    txtDBAmountS.Focus();
                    CalculateTotalVATAmount();
                    CalculateFinalSummary();
                }
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
                        pnlBillDetails.Enabled = true;
                        txtCashDiscountPerS.Text = entereddiscper.ToString("#0.00");
                        lblFooterMessage.Text = "Press Enter..";
                        txtCashDiscountPerS.Focus();
                    }
                    CalculateFinalSummary();
                    txtOCTAmountS.Focus();
                }
                else if (e.KeyCode == Keys.Up)
                {
                    CalculateFinalSummary();
                    txtAddOnS.Focus();
                    
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
                _Purchase.IfTypeChange = "Y";
                //if (General.CurrentSetting.MsetPurchaseIfCreditPurchase == "Y")
                //{
                //    rbtCreditSTMT.Visible = true;
                //    rbtCreditSTMT.Enabled = true;
                //}
                //else
                //    rbtCreditSTMT.Visible = false;  

                tsBtnSave.Enabled = true;
                mcbCreditor.Enabled = false;              
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
                    ////if (General.CurrentSetting.MsetPurchaseIfCreditStatementPurchase == "Y")
                    ////    cbNewTransactionType.Items.Add(FixAccounts.TransactionTypeForCreditStatement);
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

                        _Purchase.VoucherNumber = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _Purchase.VoucherSeries = General.ShopDetail.ShopVoucherSeries;
                        _Purchase.ReadDetailsByVouNumber(_Purchase.VoucherNumber, _Purchase.VoucherType, _Purchase.VoucherSeries,_Purchase.VoucherSubType);
                        FillSearchData(_Purchase.Id, "");
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



        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = mcbCreditor.SelectedID;
            FillCreditorCombo();
            mcbCreditor.SelectedID = selectedId;
            txtBillNumber.Focus();
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

        private void txtNarration_Enter(object sender, EventArgs e)
        {
            if (txtBillNumber.Text == null || txtBillNumber.Text.ToString() == "")
                txtBillNumber.Focus();
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
            if (cbTransactionType.Text == FixAccounts.TransactionTypeForCash)
            {
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCashPurchase;

            }
            else if (cbTransactionType.Text == FixAccounts.TransactionTypeForCredit)
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditPurchase;
            else
                _Purchase.VoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
            _Purchase.OldVoucherType = _Purchase.VoucherType;
            txtVouType.Text = _Purchase.VoucherType;

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

        private void UclPurchaseWithoutStock_Load(object sender, EventArgs e)
        {
            FillTransactionType();
        }

        private void txtBillAmountS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSchemeDiscountS.Focus();
            else if (e.KeyCode == Keys.Up)
                datePickerBillDate.Focus();
        }

        private void txtSchemeDiscountS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPurchaseAmountVATZeroS.Focus();
            else if (e.KeyCode == Keys.Up)
                txtBillAmountS.Focus();
        }
        private void txtPurchaseAmountVATZeroS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCRAmountS.Focus();
            else if (e.KeyCode == Keys.Up)
                txtSchemeDiscountS.Focus();
        }
        private void txtItemDiscountS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCRAmountS.Focus();
            else if (e.KeyCode == Keys.Up)
                txtSchemeDiscountS.Focus();
        }

        private void txtChequeNumber_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void datePickerChqDate_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void mcbBank_EnterKeyPressed(object sender, EventArgs e)
        {

        }



        #region Events
        #endregion

        #endregion

        private void datePickerBillDate_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                     txtVoucherSeries .Focus();
                              
            }
               else if (e.KeyCode == Keys.Up)
            {
                txtNarration.Focus();
            }
                
        }

        private void txtVoucherSeries_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBillAmountS.Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                datePickerBillDate.Focus();
            }
        }
    }
}
