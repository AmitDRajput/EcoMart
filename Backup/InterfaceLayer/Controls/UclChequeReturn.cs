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
    public partial class UclChequeReturn : BaseControl
    {
        #region Declaration
        private ChequeReturn _ChequeReturn;
        DataTable _saledtable = new DataTable();
        DataTable _statementdtable = new DataTable();
        #endregion

        # region Constructor
        public UclChequeReturn()
        {
            try
            {
                InitializeComponent();
                _ChequeReturn = new ChequeReturn();
               
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
                //_ChequeReturn.Initialise();
                ClearControls();
                if (_Mode == OperationMode.Edit)
                    SearchControl = new UclBankReceiptSearch();
                else
                    SearchControl = new UclChequeReturnSearch();
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
                headerLabel1.Text = "CHEQUE RETURN -> EDIT";
                FillmpMSVCGrid("");
                //  FillmpPVC1GridSale();
                FillPartyCombo();
                FillBankAccountCombo();
                FillBankCombo();
                FillBranchCombo();

                datePickerBillDate.Value = DateTime.Now;

                mpMSVC.Visible = true;
                mcbBankAccount.Enabled = false;
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
                mpMSVC.Visible = true;             
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
                if (_ChequeReturn.Id != null && _ChequeReturn.Id != "")
                {
                    if (_ChequeReturn.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _ChequeReturn.DeleteDetails();
                        //   if (retValue)
                        ////       retValue = DeletePreviousEntry();
                        //   if (retValue)
                        // //      retValue = RevertPreviousSalesBalance();

                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            MessageBox.Show("Deleted Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _ChequeReturn.ModifiedBy = General.CurrentUser.Id;
                            _ChequeReturn.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _ChequeReturn.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                         //   _ChequeReturn.AddDeletedDetails();
                            //   AddPreviousRowsInDeletedDetail();
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
                ClearData();
                headerLabel1.Text = "BANK RECEIPT -> VIEW";
                //  FillmpPVC1GridSale();
                FillmpMSVCGrid("");
                FillPartyCombo();
                FillBankAccountCombo();
                FillBankCombo();
                FillBranchCombo();
                mpMSVC.Visible = true;
                //   mpMSCSale.BringToFront();
                mpMSVC.Visible = false;
                //    mpMSVC.SendToBack();
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
                int totalrows = mpMSVC.Rows.Count;
                PrintPageNumber = 0;
                int rowcount = 0;
                PrintRowPixel = 0;
                double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                int totalpages = Convert.ToInt32(totpages);
                PrintHeader(totalpages, rowcount, fnt);
                foreach (DataGridViewRow dr in mpMSVC.Rows)
                {

                    if (dr.Cells["Col_ClearedAmount"].Value != null)
                    {
                        if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                        {
                            PrintRowPixel = 325;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                            PrintBill.Rows.Add(row);
                            //////////_ChequeReturn.PrintRowPixel = 418;
                            //////////row = new PrintRow("End--------------", _ChequeReturn.PrintRowPixel, 15, fnt);
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
                row = new PrintRow(_ChequeReturn.CBNarration, PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(_ChequeReturn.CBAmount.ToString("#0.00"), PrintRowPixel, 700, fnt);
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

                row = new PrintRow(_ChequeReturn.CBVouNo.ToString(), PrintRowPixel, 400, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 36;
                row = new PrintRow(_ChequeReturn.CBName, PrintRowPixel, 40, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel = PrintRowPixel + 34;
                string myadd = _ChequeReturn.CBAddress1.Trim() + " " + _ChequeReturn.CBAddress2.Trim();
                row = new PrintRow(myadd, PrintRowPixel, 30, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 520, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_ChequeReturn.CBVouDate, PrintRowPixel, 680, fnt);
                PrintBill.Rows.Add(row);
                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow(page, PrintRowPixel, 750, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;

                string mych = "Chq.No.:" + _ChequeReturn.CBChequeNumber.Trim() + " Chq.Date :" + General.GetDateInDateFormat(_ChequeReturn.CBChequeDate);
                row = new PrintRow(mych, PrintRowPixel, 80, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;

                mych = "Bank :" + _ChequeReturn.CBBankName.Trim() + " Branch :" + _ChequeReturn.CBBranchName.Trim();
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
            bool retValue = false;
            System.Text.StringBuilder _errorMessage;
            try
            {
                if (txtAmountReceived.Text != null && txtAmountReceived.Text != "")
                {
                    LockTable.LockTablesForChequeReturn();

                    _ChequeReturn.CBAccountID = mcbCreditor.SelectedID;
                    _ChequeReturn.CBAmount = Convert.ToDouble(txtAmountReceived.Text.ToString());
                    _ChequeReturn.CBNarration = txtNarration.Text.ToString().Trim();



                    _ChequeReturn.CBBankAccountID = mcbBankAccount.SelectedID;
                    _ChequeReturn.CBBankID = mcbBank.SelectedID;
                    _ChequeReturn.CBBranchID = mcbBranch.SelectedID;
                    if (txtChequeNumber.Text != null)
                        _ChequeReturn.CBChequeNumber = txtChequeNumber.Text.ToString();
                    _ChequeReturn.CBChequeDate = datePickerChequeDate.Value.Date.ToString("yyyyMMdd");

                 //   if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                 //       _ChequeReturn.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString().Trim());
                 //   _ChequeReturn.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    if (_Mode == OperationMode.Edit)
                        _ChequeReturn.IFEdit = "Y";

                    _ChequeReturn.Validate();
                    if (_ChequeReturn.IsValid)
                    {
                        if (_Mode == OperationMode.Edit)
                        {
                            
                          

                            General.BeginTransaction();
                            _ChequeReturn.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _ChequeReturn.CreatedBy = General.CurrentUser.Id;
                            _ChequeReturn.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _ChequeReturn.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            _ChequeReturn.CBVouDate = dtpReturnDate.Value.Date.ToString("yyyyMMdd");
                            _ChequeReturn.CBVouType = FixAccounts.VoucherTypeForChequeReturn;
                           _ChequeReturn.CBVouNo = _ChequeReturn.GetAndUpdateChequreReturn(General.ShopDetail.ShopVoucherSeries);
                            txtVouchernumber.Text = _ChequeReturn.CBVouNo.ToString();
                            retValue = _ChequeReturn.AddDetails();
                            if (retValue)
                                retValue = saveDetails();
                            if (retValue)
                                RevertPreviousSalesBalance();
                            if (retValue)
                              retValue = _ChequeReturn.UpdateBankReceiptVoucherForChequeReturn();
                            if (retValue)
                            {
                                _ChequeReturn.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _ChequeReturn.AddAccountDetailsIntbltrnacDebitForChequeReturn();
                            }
                            if (retValue)
                            {
                                _ChequeReturn.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                retValue = _ChequeReturn.AddAccountDetailsIntbltrnacCreditForChequeReturn();
                            }
                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                                string msgLine2 = _ChequeReturn.CBVouType + "  " + _ChequeReturn.CBVouNo.ToString("#0");
                                PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                //  MessageBox.Show("Cash Receipt Entry Saved Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _SavedID = _ChequeReturn.Id;

                                retValue = true;
                            }
                            else
                            {
                                MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                retValue = false;
                            }
                        }
                        //else if (_Mode == OperationMode.Edit)
                        //{
                        //    if (_ChequeReturn.ModifyEdit == "Y")
                        //    {
                        //        General.BeginTransaction();
                        //        _ChequeReturn.CBAccountID = mcbCreditor.SelectedID;
                        //        _ChequeReturn.ModifiedBy = General.CurrentUser.Id;
                        //        _ChequeReturn.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        //        _ChequeReturn.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        //        retValue = _ChequeReturn.UpdateDetails();
                        //        //if (retValue)
                        //        //    retValue = DeletePreviousEntry();
                        //        //if (retValue)
                        //        //    retValue = RevertPreviousSalesBalance();
                        //        if (retValue)
                        //            retValue = saveDetails();
                        //        if (retValue)
                        //            retValue = _ChequeReturn.DeleteAccountDetails();
                        //        if (retValue)
                        //        {
                        //            _ChequeReturn.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        //            retValue = _ChequeReturn.AddAccountDetailsIntbltrnacDebit();
                        //        }
                        //        if (retValue)
                        //        {
                        //            _ChequeReturn.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        //            retValue = _ChequeReturn.AddAccountDetailsIntbltrnacCredit();
                        //        }
                        //        if (retValue)
                        //            General.CommitTransaction();
                        //        else
                        //            General.RollbackTransaction();
                        //        LockTable.UnLockTables();
                        //        if (retValue)
                        //        {
                        //            MessageBox.Show("Updated Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //            _ChequeReturn.ChangedID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        //            _ChequeReturn.AddChangedDetails();
                        //       //    AddPreviousRowsInChangedDetail();
                        //            _SavedID = _ChequeReturn.Id;
                        //            retValue = true;
                        //        }

                        //        else
                        //        {
                        //            MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //            retValue = false;
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        LockTable.UnLockTables();
                        _errorMessage = new System.Text.StringBuilder();
                        _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                        foreach (string _message in _ChequeReturn.ValidationMessages)
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
        private bool saveDetails()
        {
            {
                bool returnVal = true;
                _ChequeReturn.SerialNumber = 0;

                try
                {
                    foreach (DataGridViewRow drow in mpMSVC.Rows)
                    {
                        if (drow.Cells["Col_ID"].Value != null && drow.Cells["Col_ID"].Value.ToString() != "")
                           
                        {
                            _ChequeReturn.SerialNumber += 1;
                            _ChequeReturn.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _ChequeReturn.DMasterID = drow.Cells["Col_MasterID"].Value.ToString();
                            _ChequeReturn.DSaleId = drow.Cells["Col_ID"].Value.ToString();
                            _ChequeReturn.DVoucherSeries = drow.Cells["Col_BillSeries"].Value.ToString();
                            _ChequeReturn.DVoucherType = drow.Cells["Col_BillType"].Value.ToString();
                            _ChequeReturn.DVoucherNumber = Convert.ToInt32(drow.Cells["Col_BillNumber"].Value.ToString());
                            _ChequeReturn.DSubType = drow.Cells["Col_BillSubType"].Value.ToString();
                            _ChequeReturn.DVoucherDate = drow.Cells["Col_BillFromDate"].Value.ToString();
                            _ChequeReturn.DBillAmount = Convert.ToDouble(drow.Cells["Col_BillAmount"].Value.ToString());
                            _ChequeReturn.DBalanceAmount = Convert.ToDouble(drow.Cells["Col_BalanceAmount"].Value.ToString());
                            _ChequeReturn.DClearedAmount = Convert.ToDouble(drow.Cells["Col_ClearedAmount"].Value.ToString());
                            returnVal = _ChequeReturn.AddParticularsDetails();
                          
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
        public bool RevertPreviousSalesBalance()
        {
            bool retValue = false;

            try
            {
                foreach (DataGridViewRow drowPVCTemp in mpMSVC.Rows)
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
                        if (mvoutype != FixAccounts.VoucherTypeForStatementSale)
                            retValue = _ChequeReturn.RevertPreviousSalesBalance(mSaleID, mClearedAmount);
                        else
                            retValue = _ChequeReturn.RevertPreviousStatementBalance(mSaleID, mClearedAmount);
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
                dtpReturnDate.Value = DateTime.Now;
                txtVouType.Text = FixAccounts.VoucherTypeForBankReceipt;
                datePickerBillDate.ResetText();
                datePickerChequeDate.ResetText();
                txtAmountReceived.Text = "0.00";
                txtAmtNotAdjusted.Text = "0.00";
                mpMSVC.ColumnsMain.Clear();               
                mpMSVC.Rows.Clear();             
                mcbCreditor.SelectedID = "";
                mcbBank.SelectedID = "";
                mcbBranch.SelectedID = "";
                mcbBankAccount.SelectedID = "";
                lblFooterMessage.Text = "";
                tsBtnSave.Enabled = true;
                tsBtnSavenPrint.Enabled = true;
                _ChequeReturn.Id = "";
                _ChequeReturn.CBAccountID = "";
                this.mcbCreditor.Focus();
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
                foreach (DataGridViewRow dr in mpMSVC.Rows)
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
                mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2" };
                mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "200", "150" };
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
        private bool FillmpMSVCGrid(string vmode)
        {
            bool retValue = false;
            try
            {
                ConstructMainColumns();
                if (mpMSVC.Rows.Count > 0)
                    mpMSVC.Rows.Clear();
                mpMSVC.DoubleColumnNames.Add("Col_BillAmount");
                mpMSVC.DoubleColumnNames.Add("Col_BalanceAmount");
                mpMSVC.DoubleColumnNames.Add("Col_ClearedAmount");
                mpMSVC.DoubleColumnNames.Add("Col_GetClearedAmount");
                mpMSVC.DateColumnNames.Add("Col_VoucherDate");
                mpMSVC.DateColumnNames.Add("Col_BillFromDate");




                if (_ChequeReturn.CBAccountID != null && _ChequeReturn.CBAccountID != "")
                {
                    if (_Mode == OperationMode.Add || (_ChequeReturn.ActualAccountID != _ChequeReturn.CBAccountID && _ChequeReturn.ModifyEdit == "Y"))
                    {
                        _saledtable = _ChequeReturn.ReadBillDetailsByID();
                   //     _statementdtable = _ChequeReturn.ReadStatementDetailsByID();
                        mpMSVC.Rows.Clear();
                        BindmpMSVCGrid(_saledtable, _statementdtable);

                        NoofRows();
                    }
                    else
                    {
                        mpMSVC.Rows.Clear();
                        _statementdtable = null;
                     //   if (vmode == "C")
                     //       _saledtable = _ChequeReturn.ReadBillDetailsByBKRIDForChanged();
                   //     else if (vmode == "D")
                  //          _saledtable = _ChequeReturn.ReadBillDetailsByBKRIDForDeleted();
                      //  else
                        {

                            _saledtable = _ChequeReturn.ReadBillDetailsByBKRID();
                   //         _statementdtable = _ChequeReturn.ReadStatementDetailsByBKRID();
                        }
                        BindmpMSVCGrid(_saledtable, _statementdtable);
                        NoofRowsFormpMSVCGrid();

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
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _ChequeReturn.BKRID = ID;
                    //if (Vmode == "C")
                    //    _ChequeReturn.ReadDetailsByIDForChanged();
                    //else if (Vmode == "D")
                    //    _ChequeReturn.ReadDetailsByIDForDeleted();
                    //else
                        _ChequeReturn.ReadDetailsByID();
                    mcbCreditor.SelectedID = _ChequeReturn.CBAccountID;
                    _ChequeReturn.ActualAccountID = _ChequeReturn.CBAccountID;

                    mpMSVC.Visible = true;


                    FillmpMSVCGrid(Vmode);

                    //     FillmpPVCTempGrid();
                    DateTime mydate = new DateTime(Convert.ToInt32(_ChequeReturn.BKRVoucherDate.Substring(0, 4)), Convert.ToInt32(_ChequeReturn.BKRVoucherDate.Substring(4, 2)), Convert.ToInt32(_ChequeReturn.BKRVoucherDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    mcbBankAccount.SelectedID = _ChequeReturn.CBBankAccountID;
                    mcbBank.SelectedID = _ChequeReturn.CBBankID;
                    mcbBranch.SelectedID = _ChequeReturn.CBBranchID;
                    //      mcbCreditor.SelectedID = _ChequeReturn.CBAccountID;
                    txtAddress1.Text = _ChequeReturn.CBAddress1;
                    txtAddress2.Text = _ChequeReturn.CBAddress2;
                    txtNarration.Text = _ChequeReturn.CBNarration;
                    txtVouchernumber.Text = _ChequeReturn.BKRVoucherNumber.ToString();
                    txtVouType.Text = FixAccounts.VoucherTypeForBankReceipt;
                   
                    txtChequeNumber.Text = _ChequeReturn.CBChequeNumber.ToString();
                    datePickerChequeDate.Value = General.ConvertStringToDateyyyyMMdd(_ChequeReturn.CBChequeDate);
                    txtAmountReceived.Text = _ChequeReturn.CBAmount.ToString("#0.00");
                    txtAmtNotAdjusted.Text = _ChequeReturn.CBOnAccountAmount.ToString("#0.00");
                    pnlVouTypeNo.Enabled = false;
                    pnlVou.Enabled = false;
                    if (_ChequeReturn.CBIfChequeReturn == "Y")
                    {
                        tsBtnSave.Enabled = false;
                        tsBtnSavenPrint.Enabled = false;
                        lblFooterMessage.Text = "Cheque Already Returned..";
                    }
                    else
                        dtpReturnDate.Focus();

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
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
        #endregion
        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtVouchernumber.Text != "")
                    {
                        _ChequeReturn.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _ChequeReturn.ReadDetailsByIDVoucherNumber();
                        FillSearchData(_ChequeReturn.Id, "");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void dtpReturnDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtReturnCharges.Focus();
        }

     
    
    }
}
