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
    public partial class UclBankPayment : BaseControl
    {
        #region Declaration
        private BankPayment _BankPayment;
        string DefaultBankID = "";
        DataTable _saledtable = new DataTable();
        DataTable _statementdtable = new DataTable();
        bool IfOpeningAdded = false;
        #endregion

        # region Constructor
        public UclBankPayment()
        {
            try
            {
                InitializeComponent();
                _BankPayment = new BankPayment();
                SearchControl = new UclBankPaymentSearch();
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
                _BankPayment.Initialise();
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
                headerLabel1.Text = "BANK PAYMENT -> NEW";
                FillGrids();
                GetDefaultBank();
                //datePickerBillDate.Value = DateTime.Now;
                //datePickerChequeDate.Value = DateTime.Now;
                EnableDisableAdd();
                mcbBankAccount.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        private void FillGrids()
        {
            FillmpPVC1GridSale();
            FillPartyCombo();
            FillBankAccountCombo();
            if (_Mode != OperationMode.Add)
                FillmpMSVCGrid("");
        }

        private void EnableDisableAdd()
        {
            btnModify.Visible = false;
            mpMSCSale.Visible = true;         
            mpMSVC.Visible = false;           
            mcbCreditor.Enabled = true;
            txtAmountReceived.Enabled = true;
            pnlVouTypeNo.Enabled = true;
            pnlVou.Enabled = true;
        }

        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                ClearData();
                if (_Mode == OperationMode.Edit)
                    headerLabel1.Text = "BANK PAYMENT -> EDIT";
                else
                    headerLabel1.Text = "BANK PAYMENT -> VOUCHER DATE CHANGE";
                FillGrids();
             //   datePickerBillDate.Value = DateTime.Now;
                EnableDisableForEdit();
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

        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {                
                headerLabel1.Text = "BANK PAYMENT -> DELETE";
                ClearData();
                datePickerBillDate.Value = DateTime.Now;
                FillGrids();
                EnableDisable();
               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        private void EnableDisable()
        {
           
            btnModify.Visible = false;
            mpMSCSale.Visible = true;       
            mpMSVC.Visible = false;       
            mcbCreditor.Enabled = false;
            txtAmountReceived.Enabled = false;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            mcbBankAccount.SelectedID = "";
            txtVouchernumber.Focus();
            if (_Mode == OperationMode.Add)
                mcbCreditor.Enabled = true;
            else
                mcbCreditor.Enabled = false;

        }
        private void EnableDisableForEdit()
        {

            btnModify.Visible = true;
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = false;
            pnlAddress.Enabled = true;
            txtAmountReceived.Enabled = false;
            txtVouchernumber.ReadOnly = false;
            txtVouchernumber.Enabled = true;
            mcbBankAccount.SelectedID = "";
            txtVouchernumber.Focus();
            if (_Mode == OperationMode.Add)
                mcbCreditor.Enabled = true;
            else
                mcbCreditor.Enabled = false;

        }
        public override bool ProcessDelete()
        {
            bool retValue = false;
            try
            {
                if (_BankPayment.Id != null && _BankPayment.Id != "")
                {
                    LockTable.LockTablesForCashBankPayment();
                    if (_BankPayment.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _BankPayment.DeleteDetails();
                        if (retValue)
                        {
                            _BankPayment.DeletePreviousRecords();
                            RevertPreviousPurchaseBalance();
                        }
                        retValue = _BankPayment.DeleteAccountDetails();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            MessageBox.Show("Deleted Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _BankPayment.ModifiedBy = General.CurrentUser.Id;
                            _BankPayment.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _BankPayment.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            _BankPayment.AddDeletedDetails();
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
                Log.WriteException(Ex);
            }
            return true;
        }

        public override bool View()
        {
            bool retValue = base.View();
            try
            {          
                headerLabel1.Text = "BANK PAYMENT -> VIEW";
                ClearData();
              //  datePickerBillDate.Value = DateTime.Now;
                FillGrids();
                EnableDisable();
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
                            //////////_BankPayment.PrintRowPixel = 418;
                            //////////row = new PrintRow("End--------------", PrintRowPixel, 15, fnt);
                            //////////PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;                            
                            PrintHeader(totalpages, rowcount, fnt);

                            rowcount = 0;
                        }
                        PrintRowPixel += 17;
                        rowcount += 1;
                        row = new PrintRow(dr.Cells["Col_Series"].Value.ToString(), PrintRowPixel, 85, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_Type"].Value.ToString(), PrintRowPixel, 125, fnt);
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
                row = new PrintRow(_BankPayment.CBNarration, PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(_BankPayment.CBAmount.ToString("#0.00"), PrintRowPixel, 700, fnt);
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

                row = new PrintRow(_BankPayment.CBVouNo.ToString(), PrintRowPixel, 400, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel =PrintRowPixel + 36;
                row = new PrintRow(_BankPayment.CBName, PrintRowPixel, 40, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel = PrintRowPixel + 34;
                string myadd = _BankPayment.CBAddress1.Trim() + " " + _BankPayment.CBAddress2.Trim();
                row = new PrintRow(myadd, PrintRowPixel, 30, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 520, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_BankPayment.CBVouDate, PrintRowPixel, 680, fnt);
                PrintBill.Rows.Add(row);
                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow(page, PrintRowPixel, 750, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;

                string mych = "Chq.No.:" + _BankPayment.CBChequeNumber.Trim() + " Chq.Date :" + General.GetDateInDateFormat(_BankPayment.CBChequeDate);
                row = new PrintRow(mych, PrintRowPixel, 80, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;

                mych = "Bank :" + _BankPayment.CBBankName.Trim() ;
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
            try
            {
                System.Text.StringBuilder _errorMessage;
                if (txtAmountReceived.Text != null && txtAmountReceived.Text != "")
                {
                    _BankPayment.CBAccountID = mcbCreditor.SelectedID;
                    _BankPayment.CBAmount = Convert.ToDouble(txtAmountReceived.Text.ToString());
                    _BankPayment.CBNarration = txtNarration.Text.ToString().Trim();
                    _BankPayment.CBBankAccountID = mcbBankAccount.SelectedID;
                    _BankPayment.CBName = mcbCreditor.SeletedItem.ItemData[2];
                    _BankPayment.CBBankName = mcbBankAccount.SeletedItem.ItemData[1];
                    _BankPayment.CBChequeNumber = txtChequeNumber.Text.ToString();
                    _BankPayment.CBChequeDate = datePickerChequeDate.Value.Date.ToString("yyyyMMdd");

                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _BankPayment.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString().Trim());
                    _BankPayment.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    _BankPayment.Validate();
                    if (_BankPayment.IsValid)
                    {
                        LockTable.LockTablesForCashBankPayment();
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();
                            _BankPayment.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _BankPayment.CreatedBy = General.CurrentUser.Id;
                            _BankPayment.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _BankPayment.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            _BankPayment.CBVouNo = _BankPayment.GetAndUpdateBKPNumber(General.ShopDetail.ShopVoucherSeries);
                            txtVouchernumber.Text = _BankPayment.CBVouNo.ToString();
                            retValue = _BankPayment.AddDetails();
                            if (retValue)
                                retValue = saveDetails();
                            if (retValue)
                                retValue = _BankPayment.SaveIntblTrnac();
                             if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                                string msgLine2 = _BankPayment.CBVouType + "  " + _BankPayment.CBVouNo.ToString("#0");
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
                            _SavedID = _BankPayment.Id;                    
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
                            _BankPayment.CBAccountID = mcbCreditor.SelectedID;
                            _BankPayment.ModifiedBy = General.CurrentUser.Id;
                            _BankPayment.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _BankPayment.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                            retValue = _BankPayment.UpdateDetailsForFifth();
                            retValue = _BankPayment.SaveIntblTrnacForFifth();
                            General.CommitTransaction();                           
                            LockTable.UnLockTables();
                            
                                _BankPayment.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                _BankPayment.AddChangedDetails();
                                AddPreviousRowsInChangedDetail();

                                string msgLine2 = _BankPayment.CBVouType + "  " + _BankPayment.CBVouNo.ToString("#0");
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
                                _SavedID = _BankPayment.Id;


                                retValue = true;
                            
                        }
                            
                        else   if (_Mode == OperationMode.Edit)
                        {
                            if (_BankPayment.ModifyEdit == "Y")
                            {
                                General.BeginTransaction();
                                _BankPayment.CBAccountID = mcbCreditor.SelectedID;
                                _BankPayment.ModifiedBy = General.CurrentUser.Id;
                                _BankPayment.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _BankPayment.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                                retValue = _BankPayment.UpdateDetails();
                                if (retValue)
                                {
                                    _BankPayment.DeletePreviousRecords();
                                    RevertPreviousPurchaseBalance();
                                    saveDetails();
                                }
                                if (retValue)
                                    retValue = _BankPayment.DeleteAccountDetails();
                                if (retValue)
                                    retValue = _BankPayment.SaveIntblTrnac();
                                if (retValue)
                                    General.CommitTransaction();
                                else
                                    General.RollbackTransaction();
                                LockTable.UnLockTables();
                                if (retValue)
                                {
                                  
                                    _BankPayment.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _BankPayment.AddChangedDetails();
                                    AddPreviousRowsInChangedDetail();

                                    string msgLine2 = _BankPayment.CBVouType + "  " + _BankPayment.CBVouNo.ToString("#0");
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
                                    _SavedID = _BankPayment.Id;

                                    retValue = true;
                                }
                                else
                                {
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
                        foreach (string _message in _BankPayment.ValidationMessages)
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
                    _BankPayment.Id = ID;
                    if (Vmode == "C")
                        _BankPayment.ReadDetailsByIDForChanged();
                    else if (Vmode == "D")
                        _BankPayment.ReadDetailsByIDForDeleted();
                    else
                    _BankPayment.ReadDetailsByID();
                        
                    mcbBankAccount.SelectedID = _BankPayment.CBBankAccountID;                  
                    _BankPayment.ActualAccountID = _BankPayment.CBAccountID;
                    _BankPayment.CBBankName = mcbBankAccount.SeletedItem.ItemData[2];                  
                    mpMSCSale.Visible = false;               
                    mpMSVC.Visible = true;
              
                    FillmpMSVCGrid(Vmode);

                    FillmpPVCTempGrid();
                    DateTime mydate = new DateTime(Convert.ToInt32(_BankPayment.CBVouDate.Substring(0, 4)), Convert.ToInt32(_BankPayment.CBVouDate.Substring(4, 2)), Convert.ToInt32(_BankPayment.CBVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    mcbCreditor.SelectedID = _BankPayment.CBAccountID;
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    _BankPayment.CBAddress1 = txtAddress1.Text.ToString();
                    _BankPayment.CBAddress2 = txtAddress2.Text.ToString();
                    txtNarration.Text = _BankPayment.CBNarration;
                    _BankPayment.CBName = mcbCreditor.SeletedItem.ItemData[2];
                    txtVouchernumber.Text = _BankPayment.CBVouNo.ToString();
                    txtChequeNumber.Text = _BankPayment.CBChequeNumber.ToString();
                    datePickerChequeDate.Value = General.ConvertStringToDateyyyyMMdd(_BankPayment.CBChequeDate);
                    txtVouType.Text = FixAccounts.VoucherTypeForBankPayment;
                    txtAmountReceived.Text = _BankPayment.CBAmount.ToString("#0.00");
                    txtAmtNotAdjusted.Text = _BankPayment.CBOnAccountAmount.ToString("#0.00");
              
                    pnlVou.Enabled = false;
                    if (_Mode == OperationMode.Edit)
                    {
                        btnModify.Visible = true;
                        btnModify.Enabled = true;
                        btnModify.Focus();
                    }
                    if (_Mode == OperationMode.Fifth)
                    {
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
                if (keyPressed == Keys.C && modifier == Keys.Alt)
                {
                    txtChequeNumber.Focus();
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
                _BankPayment.ModifyEdit = "Y";
                                      
                retValue = FillmpPVC1GridPurchaseforModify();
              //  retValue = FillmpMSVCGrid("");
                retValue = RevertPreviousEntry();
             //   retValue = RemoveZeroAmountAfterRevertPreviousEntry();
                mpMSCSale.Refresh();
                headerLabel1.Text = "BANK PAYMENT -> MODIFY";
              //  FillPartyCombo();
              //  FillBankAccountCombo();
             //   mcbCreditor.SelectedID = _BankPayment.CBAccountID;
                EnableDisableForModify();
            //    mcbBankAccount.SelectedID = _BankPayment.CBBankAccountID;               
                txtAmountReceived.Focus();
               
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void EnableDisableForModify()
        {

            if (_Mode == OperationMode.Add)
                btnModify.Visible = false;
            else
            {
                btnModify.Enabled = false;
                txtVouchernumber.ReadOnly = false;
                txtVouchernumber.Enabled = true;
                txtVouchernumber.Focus();
            }
            mpMSCSale.Visible = true;
            mpMSVC.Visible = false;
            mcbCreditor.Enabled = true;
            txtAmountReceived.Enabled = true;
            pnlVouTypeNo.Enabled = true;
            pnlVou.Enabled = true;
            txtChequeNumber.Enabled = true;
            datePickerChequeDate.Enabled = true;
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
                    if (drowMSVC.Cells["Col_ID"].Value != null)
                        mSaleID = drowMSVC.Cells["Col_ID"].Value.ToString();
                    if (drowMSVC.Cells["Col_MasterID"].Value != null)
                        mcbid = drowMSVC.Cells["Col_MasterID"].Value.ToString();
                    if (drowMSVC.Cells["Col_ClearedAmount"].Value != null)
                        double.TryParse(drowMSVC.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                    foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
                    {
                        if (drowMSCSale.Cells["Col_ID"].Value != null)
                        {
                            if (drowMSCSale.Cells["Col_ID"].Value.ToString() == mSaleID && drowMSCSale.Cells["Col_ID"].Value.ToString() != FixAccounts.VoucherTypeForOpeningBalance)
                            {
                                double mbalaceamount = 0;
                                //   double mClearedAmount = 0;
                                if (drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
                                    double.TryParse(drowMSCSale.Cells["Col_BalanceAmount"].Value.ToString(), out mbalaceamount);
                                //if (drowMSCSale.Cells["Col_GetClearedAmount"].Value != null)
                                //    double.TryParse(drowMSCSale.Cells["Col_GetClearedAmount"].Value.ToString(), out mClearedAmount);

                                drowMSCSale.Cells["Col_BalanceAmount"].Value = mbalaceamount + mClearedAmount;
                                drowMSCSale.Cells["Col_GetClearedAmount"].Value = "";
                                break;
                            }
                            //if (Convert.ToDouble(drowMSCSale.Cells["Col_BalanceAmount"].Value) == 0)
                            //    mpMSCSale.Rows.Remove(drowMSCSale);
                            
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


        public bool RemoveZeroAmountAfterRevertPreviousEntry()
        {
            bool returnVal = true;
            try
            {
                    foreach (DataGridViewRow drowMSCSale in mpMSCSale.Rows)
                    {
                        if (drowMSCSale.Cells["Col_ID"].Value != null && drowMSCSale.Cells["Col_BalanceAmount"].Value != null)
                        {
                            if (Convert.ToDouble(drowMSCSale.Cells["Col_BalanceAmount"].Value) == 0)
                            {
                                mpMSCSale.Rows.Remove(drowMSCSale);
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


        public bool RevertPreviousPurchaseBalance()
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
                    if (drowPVCTemp.Cells["Col_ID"].Value != null)
                        mSaleID = drowPVCTemp.Cells["Col_ID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_MasterID"].Value != null)
                        mcbid = drowPVCTemp.Cells["Col_MasterID"].Value.ToString();
                    if (drowPVCTemp.Cells["Col_ClearedAmount"].Value != null)
                        double.TryParse(drowPVCTemp.Cells["Col_ClearedAmount"].Value.ToString(), out mClearedAmount);
                    if (drowPVCTemp.Cells["Col_Type"].Value != null)
                        mvoutype = drowPVCTemp.Cells["Col_Type"].Value.ToString();
                    if (mSaleID != null && mClearedAmount != 0 && mvoutype != "")
                    {
                        if (mvoutype == FixAccounts.VoucherTypeForOpeningBalance)
                            _BankPayment.UpdateOpeningBalanceReducePrevious(_BankPayment.CBAccountID, mClearedAmount);
                        else if (mvoutype == FixAccounts.VoucherTypeForCreditPurchase)
                            retValue = _BankPayment.RevertPreviousPurchaseBalanceBill(mSaleID, mClearedAmount);
                        else if (mvoutype == FixAccounts.VoucherTypeForStatementPurchase)
                            retValue = _BankPayment.RevertPreviousPurchaseBalanceStatement(mSaleID, mClearedAmount);
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
                returnVal = _BankPayment.DeletePreviousRecords();
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
                _BankPayment.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow drow in mpMSCSale.Rows)
                    {
                        if (drow.Cells["Col_GetClearedAmount"].Value != null &&
                           Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value) > 0)
                        {
                            _BankPayment.SerialNumber += 1;
                            _BankPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            if (drow.Cells["Col_MasterID"].Value != null)
                                _BankPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _BankPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _BankPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                            _BankPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                            if (drow.Cells["Col_Number"].Value != null && drow.Cells["Col_Number"].Value.ToString() != "")
                                _BankPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString()); ;                      
                            _BankPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _BankPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _BankPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _BankPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_GetClearedAmount"].Value.ToString());                         
                            returnVal = _BankPayment.AddParticularsDetails();
                            if (returnVal)
                            {
                                if (_BankPayment.DVoucherType == FixAccounts.VoucherTypeForOpeningBalance)
                                {
                                    //if (_Mode == OperationMode.Add)
                                    //{
                                        _BankPayment.OpeningCleared = 0;
                                        if (_BankPayment.preAccountID != null && _BankPayment.preAccountID != "")
                                            returnVal = _BankPayment.UpdateOpeningBalanceReducePrevious(_BankPayment.preAccountID, _BankPayment.OpeningCleared);
                                        returnVal = _BankPayment.UpdateOpeningBalanceAddNew();

                                    //}
                                }
                                if (_BankPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                                    returnVal = _BankPayment.UpdatePurchaseStatement();
                                else
                                    returnVal = _BankPayment.UpdatePurchaseBill();
                                if (returnVal == false)
                                    break;
                            }
                            else
                                break;
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }

        private bool AddPreviousRowsInChangedDetail()
        {
            {
                bool returnVal = true;
                _BankPayment.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow drow in mpPVCTemp.Rows)
                    {
                        if (drow.Cells["Col_ID"].Value != null && drow.Cells["Col_ID"].Value.ToString() != "") 
                          
                        {
                            _BankPayment.SerialNumber += 1;
                            _BankPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _BankPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _BankPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _BankPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                            _BankPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                            _BankPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString());
                            _BankPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _BankPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _BankPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _BankPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_ClearedAmount"].Value.ToString());
                            returnVal = _BankPayment.AddParticularsDetailsChanged();
                            //if (returnVal)
                            //{
                            //    if (_BankPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                            //        returnVal = _BankPayment.UpdatePurchaseStatement();
                            //    else
                            //        returnVal = _BankPayment.UpdatePurchaseBill();
                            //    if (returnVal == false)
                            //        break;
                            //}
                            //else
                            //    break;
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }

        private bool AddPreviousRowsInDeletedDetail()
        {
            {
                bool returnVal = true;
                _BankPayment.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow drow in mpMSVC.Rows)
                    {
                        if (drow.Cells["Col_ID"].Value != null && drow.Cells["Col_ID"].Value.ToString() != "")
                        {
                            _BankPayment.SerialNumber += 1;
                            _BankPayment.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _BankPayment.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _BankPayment.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _BankPayment.DVoucherSeries = drow.Cells["Col_Series"].Value.ToString();
                            _BankPayment.DVoucherType = drow.Cells["Col_Type"].Value.ToString();
                            _BankPayment.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_Number"].Value.ToString());
                            _BankPayment.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _BankPayment.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _BankPayment.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _BankPayment.DClearedAmount = Convert.ToDouble(drow.Cells["Col_ClearedAmount"].Value.ToString());
                            returnVal = _BankPayment.AddParticularsDetailsDeleted();
                            //if (returnVal)
                            //{
                            //    if (_BankPayment.DVoucherType == FixAccounts.VoucherTypeForStatementPurchase)
                            //        returnVal = _BankPayment.UpdatePurchaseStatement();
                            //    else
                            //        returnVal = _BankPayment.UpdatePurchaseBill();
                            //    if (returnVal == false)
                            //        break;
                            //}
                            //else
                            //    break;
                        }
                    }
                }
                catch { returnVal = false; }
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
                txtNoOfRows.Text = "0";
                txtTotalBalance.Text = "0.00";
                txtVouType.Text = FixAccounts.VoucherTypeForBankPayment;
                datePickerBillDate.ResetText();
                datePickerChequeDate.ResetText();
                txtAmountReceived.Text = "0.00";
                txtAmtNotAdjusted.Text = "0.00";               
                txtChequeNumber.Text = ""; 
                mpMSVC.Rows.Clear();
                mpMSCSale.Rows.Clear();                           
                mcbCreditor.SelectedID = "";
                mcbBankAccount.SelectedID = "";
                if (_Mode == OperationMode.Add)
                {
                    mcbCreditor.Enabled = true;
                    mcbBankAccount.Enabled = true;   
                    this.mcbCreditor.Focus();
                    txtVouchernumber.Enabled = false;
                }
                else
                {
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.Focus();
                    mcbBankAccount.Enabled = false;
                    mcbCreditor.Enabled = false;
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
                    if (dr.Cells["Col_BillAmount"].Value != null && dr.Cells["Col_BillAmount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                        totamt = totamt + Convert.ToDouble(dr.Cells["Col_BillAmount"].Value.ToString());
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
                mcbCreditor.SourceDataString = new string[7] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccOpeningCredit", "AccClearedAmount" };
                mcbCreditor.ColumnWidth = new string[7] { "0", "20", "200", "200", "150", "0", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetDebtorCreditorList();
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
        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                if (mcbCreditor.SeletedItem != null)
                { 
                        txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                        txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                        _BankPayment.CBAccountID = mcbCreditor.SelectedID;
                    if (_Mode != OperationMode.ReportView)
                        FillmpMSVCGrid("");
                        if (_BankPayment.ModifyEdit != "Y" || (_BankPayment.ModifyEdit == "Y" && _BankPayment.ActualAccountID != _BankPayment.CBAccountID))
                            FillmpPVC1GridSale();
                    
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
                    if (mcbCreditor.SeletedItem.ItemData[4] != null && mcbCreditor.SeletedItem.ItemData[4] != string.Empty)
                        txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    if (mcbCreditor.SeletedItem.ItemData[5] != null && mcbCreditor.SeletedItem.ItemData[5] != string.Empty)
                        _BankPayment.OpeningBalance = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString());
                    if (mcbCreditor.SeletedItem.ItemData[6] != null && mcbCreditor.SeletedItem.ItemData[6] != string.Empty)
                        _BankPayment.OpeningCleared = Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString());
                    _BankPayment.CBAccountID = mcbCreditor.SelectedID;
                    if (_Mode != OperationMode.ReportView)
                    FillmpMSVCGrid("");
                    //if (_BankPayment.ModifyEdit != "Y" || (_BankPayment.ModifyEdit == "Y" && _BankPayment.ActualAccountID != _BankPayment.CBAccountID))
                    //    FillmpPVC1GridSale();
                    txtAmountReceived.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private bool FillmpPVC1GridSale()
        {
            bool retValue = false;
            try
            {
                ConstructSaleColumns();
                FormatSaleGrid();
                DataTable dtable = new DataTable();
                if (_BankPayment.CBAccountID != null && _BankPayment.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_BankPayment.ActualAccountID != _BankPayment.CBAccountID && _BankPayment.ModifyEdit == "Y"))
                    {
                        dtable = _BankPayment.ReadBillDetailsByID();
                        _statementdtable = _BankPayment.ReadStatementDetailsByID();
                    }
                    else
                        dtable = _BankPayment.ReadBillDetailsByBKPID();
                }
                //mpMSCSale.DataSourceMain = dtable;
                //mpMSCSale.Bind();
                BindmpMSVCGrid(dtable, _statementdtable);
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

        private void FormatSaleGrid()
        {
            mpMSCSale.DoubleColumnNames.Add("Col_GetClearedAmount");
            mpMSCSale.DoubleColumnNames.Add("Col_BillAmount");
            mpMSCSale.DoubleColumnNames.Add("Col_BalanceAmount");
            mpMSCSale.DateColumnNames.Add("Col_BillFromDate");
            mpMSVC.DateColumnNames.Add("Col_BillFromDate");
        }
        private void mpMSCSale_OnShowViewForm(DataGridViewRow selectedRow)
        {
            try
            {
                //TODO: Create form as per voucher type
                mpMSCSale.ViewControl = new UclPurchase();
                mpMSCSale.ProcessViewForm(selectedRow.Cells[0].Value.ToString(), this.Size, this.Location);
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        private bool FillmpPVC1GridPurchaseforModify()
        {
            bool retValue = false;
            try
            {
                ConstructSaleColumns();
                FormatSaleGrid();
                DataTable dtable = new DataTable();

                
                IfOpeningAdded = false;
                dtable = _BankPayment.ReadBillDetailsByIDforModify();
                _statementdtable = _BankPayment.ReadStatementDetailsByIDforModify();
                mpMSCSale.Rows.Clear();               
                BindmpMSCSaleGrid(dtable, _statementdtable);
                IfOpeningAdded = true;
                _saledtable = _BankPayment.ReadBillDetailsByID();
                _statementdtable = _BankPayment.ReadStatementDetailsByID();
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
        private void BindmpMSCSaleGrid(DataTable saletable, DataTable statementtable)
        {

            bool ifIDFound = false;
            int _rowIndex = 0;
            string iD = "";
            try
            {
                if (_Mode == OperationMode.Add)
                {
                    if ((_BankPayment.OpeningBalance - _BankPayment.OpeningCleared) > 0)
                    {
                        _rowIndex = mpMSCSale.Rows.Add();
                        DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "";
                        currentdr.Cells["Col_Series"].Value = "";
                        currentdr.Cells["Col_Type"].Value = "OPB";
                        currentdr.Cells["Col_Number"].Value = "1";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = General.ShopDetail.Shopsy.ToString();
                        currentdr.Cells["Col_BillNumber"].Value = "1";
                        currentdr.Cells["Col_BillAmount"].Value = _BankPayment.OpeningBalance;
                        currentdr.Cells["Col_BalanceAmount"].Value = _BankPayment.OpeningBalance - _BankPayment.OpeningCleared;
                        currentdr.Cells["Col_GetClearedAmount"].Value = "";
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        currentdr.Cells["Col_MasterID"].Value = "";


                        mpMSCSale.Sort(mpMSCSale.ColumnsMain["Col_Type"], ListSortDirection.Ascending);


                    }
                }
                else if (_Mode == OperationMode.Edit && IfOpeningAdded == false)
                {
                    if (_BankPayment.OpeningClearedInVoucher >= 0)
                    {
                        if ((_BankPayment.OpeningBalance - _BankPayment.OpeningCleared + _BankPayment.OpeningClearedInVoucher) > 0)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = "OPB";
                            currentdr.Cells["Col_Series"].Value = General.ShopDetail.ShopVoucherSeries;
                            currentdr.Cells["Col_Type"].Value = "OPB";
                            currentdr.Cells["Col_Number"].Value = "";
                            currentdr.Cells["Col_BillSubType"].Value = "";
                            currentdr.Cells["Col_BillFromDate"].Value = "";
                            currentdr.Cells["Col_BillAmount"].Value = _BankPayment.OpeningBalance.ToString();
                           // currentdr.Cells["Col_PatientShortName"].Value = "";
                            currentdr.Cells["Col_BalanceAmount"].Value = (_BankPayment.OpeningBalance - _BankPayment.OpeningCleared + _BankPayment.OpeningClearedInVoucher).ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = "";
                        }
                    }
                }
                if (saletable != null && saletable.Rows.Count > 0)
                {
                    foreach (DataRow dr in saletable.Rows)
                    {
                        if (dr["PurchaseID"] != DBNull.Value)
                            iD = dr["PurchaseID"].ToString();
                        ifIDFound = SearchforIDInSaleGrid(iD);
                        if (ifIDFound == false)
                        {
                            _rowIndex = mpMSCSale.Rows.Add();
                            DataGridViewRow currentdr = mpMSCSale.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                            currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            if (dr["PurchaseBillNumber"] != DBNull.Value)
                                currentdr.Cells["Col_BillNumber"].Value = dr["PurchaseBillNumber"].ToString();
                            //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            //  currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            // currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
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
                            currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            // currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            // currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        }

                    }


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        private void BindmpMSVCGrid(DataTable saletable, DataTable statementtable)
        {
            mpMSVC.Rows.Clear();          
            int _rowIndex = 0;           
            try
            {
                if (_Mode == OperationMode.Add)
                {
                    if ((_BankPayment.OpeningBalance - _BankPayment.OpeningCleared) > 0)
                    {
                        _rowIndex =  mpMSVC.Rows.Add();
                        DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = "";
                        currentdr.Cells["Col_Series"].Value = "";
                        currentdr.Cells["Col_Type"].Value = "OPB";
                        currentdr.Cells["Col_Number"].Value = "1";
                        currentdr.Cells["Col_BillSubType"].Value = "";
                        currentdr.Cells["Col_BillFromDate"].Value = General.ShopDetail.Shopsy.ToString();
                        currentdr.Cells["Col_BillNumber"].Value = "1";
                        currentdr.Cells["Col_BillAmount"].Value = _BankPayment.OpeningBalance;
                        currentdr.Cells["Col_BalanceAmount"].Value = _BankPayment.OpeningBalance - _BankPayment.OpeningCleared;
                        currentdr.Cells["Col_GetClearedAmount"].Value = "";
                        currentdr.Cells["Col_ClearedAmount"].Value = "";
                        currentdr.Cells["Col_MasterID"].Value = "";                    


                    }
                }
                if (saletable != null && saletable.Rows.Count > 0)
                {

                    foreach (DataRow dr in saletable.Rows)
                    {
                        _rowIndex = mpMSVC.Rows.Add();
                        DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                        currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                        currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                        if (dr["PurchaseBillNumber"] != DBNull.Value)
                            currentdr.Cells["Col_BillNumber"].Value = dr["PurchaseBillNumber"].ToString();
                        //   currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                        currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                        //  currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                        currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                        currentdr.Cells["Col_GetClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                        currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                        // currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                    }

                }

                
                if (statementtable != null && statementtable.Rows.Count > 0)
                {

                    foreach (DataRow dr in statementtable.Rows)
                    {
                       
                            _rowIndex = mpMSVC.Rows.Add();
                            DataGridViewRow currentdr = mpMSVC.Rows[_rowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_Series"].Value = dr["VoucherSeries"].ToString();
                            currentdr.Cells["Col_Type"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_Number"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_BillSubType"].Value = dr["VoucherSubType"].ToString();
                            //  currentdr.Cells["Col_BillFromDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                            currentdr.Cells["Col_BillFromDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_BillAmount"].Value = dr["AmountNet"].ToString();
                            // currentdr.Cells["Col_PatientShortName"].Value = dr["PatientShortName"].ToString();
                            currentdr.Cells["Col_BalanceAmount"].Value = dr["AmountBalance"].ToString();
                            currentdr.Cells["Col_ClearedAmount"].Value = dr["AmountClear"].ToString();
                            currentdr.Cells["Col_MasterID"].Value = dr["MasterID"].ToString();
                            // currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        

                    }


                }
             //  mpMSVC.Sort(mpMSVC.ColumnsMain["Col_Type"], ListSortDirection.Ascending);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
        private bool SearchforIDInmpMSVCGrid(string ID)
        {
            bool retValue = false;
            string _GridID = "";
            foreach (DataGridViewRow dr in mpMSVC.Rows)
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

                ConstructSaleColumns();

                mpMSCSale.DoubleColumnNames.Add("Col_BillAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_BalanceAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSCSale.DoubleColumnNames.Add("Col_GetClearedAmount");
                mpMSCSale.DateColumnNames.Add("Col_VoucherDate");

                DataTable dtable = new DataTable();
                if (_BankPayment.CBAccountID != null && _BankPayment.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_BankPayment.ActualAccountID != _BankPayment.CBAccountID && _BankPayment.ModifyEdit == "Y"))
                    {
                        dtable = _BankPayment.ReadBillDetailsByID();
                        _statementdtable = _BankPayment.ReadStatementDetailsByID();

                        mpMSCSale.Rows.Clear();
                        mpMSVC.Rows.Clear();
                        BindmpMSVCGrid(dtable, _statementdtable);
                        BindmpMSCSaleGrid(dtable, _statementdtable);
                        NoofRows();
                        mpMSCSale.Sort(mpMSCSale.ColumnsMain["Col_BillFromDate"], ListSortDirection.Ascending);
                        mpMSVC.Sort(mpMSVC.ColumnsMain["Col_BillFromDate"], ListSortDirection.Ascending);
                        //mpMSVC.Refresh();
                        //mpMSCSale.Refresh();


                    }
                    else
                    {
                        mpMSVC.Rows.Clear();
                        _statementdtable = null;
                        if (vmode == "C")
                            dtable = _BankPayment.ReadBillDetailsByBKPIDForChanged();
                        else if (vmode == "D")
                            dtable = _BankPayment.ReadBillDetailsByBKPIDForDeleted();
                        else
                            dtable = _BankPayment.ReadBillDetailsByBKPID();



                        BindmpMSVCGrid(dtable, _statementdtable);
                        NoofRowsFormpMSVCGrid();
                        foreach (DataRow dr in dtable.Rows)
                        {
                            if (dr["VoucherType"] != DBNull.Value && dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForOpeningBalance)
                            {
                                _BankPayment.OpeningClearedInVoucher = Convert.ToDouble(dr["AmountClear"].ToString());
                                break;
                            }
                        }
                        retValue = true;
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
        private bool FillmpPVCTempGrid()
        {           
            bool retValue = false;
            try
            {
                ConstructTempColumns();

                mpPVCTemp.DoubleColumnNames.Add("Col_BillAmount");
                mpPVCTemp.DoubleColumnNames.Add("Col_BalanceAmount");
                mpPVCTemp.DoubleColumnNames.Add("Col_ClearedAmount");
                //        mpPVCTemp.DoubleColumnNames.Add("Col_AmountDiscount");
                mpMSVC.DoubleColumnNames.Add("Col_GetClearedAmount");


                DataTable dtable = new DataTable();
                dtable = _BankPayment.ReadBillDetailsByBKPID();
                mpPVCTemp.DataSourceMain = dtable;
                mpPVCTemp.Bind();
                retValue = true;
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                retValue = false;

            }
            return retValue;
        }

        #endregion

        #region Construct columns

        private void ConstructMainColumns()
        {
            //DataGridViewTextBoxColumn column;

            DataGridViewTextBoxColumn column;
            mpMSVC.ColumnsMain.Clear();            
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "purchaseID";
                column.HeaderText = "ID";
                column.Width = 0;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Series";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "A.YEAR";
                column.Width = 70;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Type";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.ReadOnly = true;
                column.Width = 70;
                mpMSVC.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Number";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 95;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SUB";
                column.Width = 40;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "PurchaseBillNumber";
                column.HeaderText = "BillNumber";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 140;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                mpMSVC.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.Width = 140;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSVC.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.Width = 140;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                column.Visible = false;
                mpMSVC.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterID";
                column.DataPropertyName = "MasterID";
                column.HeaderText = "MasterID";
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
            //DataGridViewTextBoxColumn column;

            DataGridViewTextBoxColumn column;
            mpMSCSale.ColumnsMain.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "purchaseID";
                column.HeaderText = "ID";
                column.Width = 0;
                column.Visible = false;
                mpMSCSale.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Series";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "YEAR";
                column.Width = 70;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Type";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.ReadOnly = true;
                column.Width = 70;
                mpMSCSale.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Number";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 95;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SUB";
                column.Width = 40;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "PurchaseBillNumber";
                column.HeaderText = "BillNumber";
                column.Width = 120;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 140;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                mpMSCSale.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.Width = 140;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpMSCSale.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GetClearedAmount";
                //   column.DataPropertyName = "AmountClear";
                column.HeaderText = "ClearedAmount";
                column.Width = 140;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                mpMSCSale.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClearedAmount";
                column.DataPropertyName = "AmountClear";
                column.Width = 110;
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
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructTempColumns()
        {
            //DataGridViewTextBoxColumn column;

            DataGridViewTextBoxColumn column;

            mpPVCTemp.ColumnsMain.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PurchaseID";
                column.HeaderText = "MasterID";
                column.Width = 0;
                column.Visible = false;
                mpPVCTemp.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Series";
                column.DataPropertyName = "VoucherSeries";
                column.HeaderText = "VoucherSeriesT";
                column.Width = 70;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Type";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "VoucherType";
                column.ReadOnly = true;
                column.Width = 80;
                mpPVCTemp.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Number";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "VoucherNumber";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //4
                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_BillSubType";
                //column.DataPropertyName = "VoucherSubType";
                //column.HeaderText = "SubType";
                //column.Width = 45;
                //column.ReadOnly = true;
                //mpPVCTemp.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillFromDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "FromDate";
                column.Width = 80;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_MasterSaleID";
                //column.DataPropertyName = "MasterSaleID";
                //column.HeaderText = "MasterSaleID";
                //column.Width = 110;
                //column.Visible = false;
                //mpPVCTemp.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillAmount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 100;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);

                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BalanceAmount";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "BalanceAmount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 110;
                column.ReadOnly = true;
                mpPVCTemp.ColumnsMain.Add(column);
                //8
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

        private void mpMSCSale_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                double getclearedamt = 0;
                if (e.ColumnIndex == 9) // 
                {                    
                    double mbalanceamount = 0;
                    double mamtnotadj = 0;
                    double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
                    //done
                    if (mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value != null && mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString().Trim() != "")
                        double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out getclearedamt);
                    _BankPayment.CellOldValueAmount = getclearedamt;
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
            double mgetclearedamount = 0;
            double mbalamount = 0;   
            double mamtnotadj = 0;
            double mbillamt = 0;
            double clearedamt = 0;
            double.TryParse(txtAmountReceived.Text, out mbillamt);
            double.TryParse(txtAmtNotAdjusted.Text, out mamtnotadj);
            //done
             double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value.ToString(), out mgetclearedamount);
             double.TryParse(mpMSCSale.MainDataGridCurrentRow.Cells["Col_BalanceAmount"].Value.ToString(), out mbalamount);

             clearedamt = Math.Min(mamtnotadj, mbalamount);
            try
            {
                if (colIndex == 9)
                {
                    if (mgetclearedamount == 0)
                    {
                       _BankPayment.CellOldValueAmount = 0;
                       
                    }
                    if (mbalamount < mgetclearedamount)
                    {                                              
                        mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = clearedamt;
                        _BankPayment.CellOldValueAmount = clearedamt;
                    }
                    else
                    {
                       // if (mamtnotadj == 0 && mgetclearedamount > 0)
                            //done
                        if (mamtnotadj == 0)
                            mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankPayment.CellOldValueAmount;

                        if (clearedamt <= Math.Min(mamtnotadj, mbalamount) || (clearedamt <= _BankPayment.CellOldValueAmount))
                        {
                            _BankPayment.CellOldValueAmount = mgetclearedamount;
                            foreach (DataGridViewRow dr in mpMSCSale.Rows)
                            {
                                double mcleared = 0;
                                if (dr.Cells["Col_GetClearedAmount"].Value != null && dr.Cells["Col_GetClearedAmount"].Value.ToString() != "")
                                    double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                                if (mcleared > 0)
                                    totalreceived += mcleared;
                            }


                            double d = (mbillamt - totalreceived);
                            txtAmtNotAdjusted.Text = d.ToString("#0.00");


                        int rowindex = mpMSCSale.MainDataGridCurrentRow.Index;
                            if (mpMSCSale.Rows.Count > rowindex+1)
                                mpMSCSale.SetFocus(rowindex+1, 9);
                            //double amt = 0;
                            //totalreceived = 0;
                            //foreach (DataGridViewRow dr in mpMSCSale.Rows)
                            //{
                            //    if (dr != mpMSCSale.MainDataGridCurrentRow)
                            //    {
                            //        double mcleared = 0;
                            //        if (dr.Cells["Col_GetClearedAmount"].Value != null)
                            //            double.TryParse(dr.Cells["Col_GetClearedAmount"].Value.ToString(), out mcleared);
                            //        if (mcleared > 0)
                            //            totalreceived += mcleared;
                            //    }
                            //}

                            //amt = (mbillamt - totalreceived);


                            //_BankPayment.CellOldValueAmount = mgetclearedamount;


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
                if (e.ColumnIndex == 9) // ss 14/3/2012
                    mpMSCSale.MainDataGridCurrentRow.Cells["Col_GetClearedAmount"].Value = _BankPayment.CellOldValueAmount.ToString("#0.00");
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }           
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mpMSCSale.SetFocus(0, 1);
            else if (e.KeyCode == Keys.Up)
                txtChequeNumber.Focus();

        }
       

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtAmountReceived.Focus();
        }

        private void txtAmountReceived_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClearGrid();
                txtChequeNumber.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                mcbCreditor.Focus();
        }

        private void txtChequeNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                datePickerChequeDate.Focus();
        }

        private void datePickerChequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNarration.Focus();
        }

        private void txtAmountReceived_TextChanged(object sender, EventArgs e)
        {
            ClearGrid();
        }

        public void ClearGrid()
        {
            try
            {
                txtAmtNotAdjusted.Text = txtAmountReceived.Text;
                _BankPayment.CellOldValueAmount = 0;
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

        private void btnModify_Click(object sender, EventArgs e)
        {
            ModifyEdit();
        }

        private void mcbBankAccount_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbCreditor.Focus();
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtVouchernumber.Text != "")
                    {
                        _BankPayment.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _BankPayment.ReadDetailsByVoucherNumber();
                        FillSearchData(_BankPayment.Id,"");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion           

        #region tooltip

        #endregion
    }
}
