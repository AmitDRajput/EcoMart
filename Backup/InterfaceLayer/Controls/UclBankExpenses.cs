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
using PharmaSYSRetailPlus.InterfaceLayer.Classes;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;


namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclBankExpenses : BaseControl
    {
        #region Declaration
        private BankExpenses _BankExpenses;
        string DefaultBankID = "";
        #endregion

        # region Constructor
        public UclBankExpenses()
        {
            try
            {
                InitializeComponent();
                _BankExpenses = new BankExpenses();
                SearchControl = new UclBankExpensesSearch();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion Constructor

        #region IDetail Control
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
                _BankExpenses.Initialise();
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
                InitialisempPVC1();
                headerLabel1.Text = "BANK EXPENSES -> NEW";
                AddToolTip();
                FillPartyCombo();
                FillBankAccountCombo();
                GetDefaultBank();
                mcbCreditor.Enabled = true;
                txtAmount.Enabled = true;
                pnlNameAddress.Enabled = true;
                pnlVou.Enabled = true;
                datePickerBillDate.Value = DateTime.Now;
                mcbCreditor.Focus();
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
                headerLabel1.Text = "BANK EXPENSES -> EDIT";
                InitialisempPVC1();
                FillPartyCombo();
                FillBankAccountCombo();
                pnlNameAddress.Enabled = true;
                mcbCreditor.Enabled = false;
                txtAmount.Enabled = false;
                txtChequeNumber.Enabled = false;
                datePickerChequeDate.Enabled = false;
                txtNarration.Enabled = false;               
                pnlVou.Enabled = true;
                AddToolTip();
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
            try
            {
                pnlNameAddress.Enabled = true;
                ClearData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "BANK EXPENSES -> DELETE";
                ClearData();
                InitialisempPVC1();
                FillPartyCombo();
                FillBankAccountCombo();
                pnlNameAddress.Enabled = true;
                mcbCreditor.Enabled = false;
                txtAmount.Enabled = false;
                txtChequeNumber.Enabled = false;
                datePickerChequeDate.Enabled = false;
                mpMainSubViewControl1.Enabled = false;
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
                if (_BankExpenses.Id != null && _BankExpenses.Id != "")
                {
                    LockTable.LockTablesForCashBankExpenses();

                    if (_BankExpenses.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _BankExpenses.DeleteDetails();
                        if (retValue)
                            retValue = _BankExpenses.DeletePreviousRecords();
                        //retValue = dele
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            MessageBox.Show("Deleted successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                pnlNameAddress.Enabled = true;
                ClearData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
                retValue = false;
            }
            return retValue;
        }

        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                headerLabel1.Text = "BANK EXPENSES -> VIEW";
                ClearData();
                InitialisempPVC1();
                FillPartyCombo();
                FillBankAccountCombo();
                pnlNameAddress.Enabled = true;
                mcbCreditor.Enabled = false;
                txtAmount.Enabled = false;
                txtChequeNumber.Enabled = false;
                datePickerChequeDate.Enabled = false;
                mpMainSubViewControl1.Enabled = false;
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

        public override bool Save()
        {
            return SaveData(false);
        }

        public override bool SaveAndPrint()
        {
            return SaveData(true);
        }

        public bool SaveData(bool printData)
        {
            bool retValue = false;
            try
            {
                System.Text.StringBuilder _errorMessage;              
                _BankExpenses.CBAccountID = mcbCreditor.SelectedID;
                _BankExpenses.CBBankAccountID = mcbBankAccount.SelectedID;
                if (txtChequeNumber.Text != null)
                    _BankExpenses.CBChequeNumber = txtChequeNumber.Text.ToString();
                _BankExpenses.CBChequeDate = datePickerChequeDate.Value.Date.ToString("yyyyMMdd");
                if (txtAmount.Text != null && txtAmount.Text != string.Empty)
                    _BankExpenses.CBAmount = Convert.ToDouble(txtAmount.Text.ToString());
                _BankExpenses.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                if (txtNarration.Text != null)
                    _BankExpenses.CBNarration = txtNarration.Text.ToString();
                _BankExpenses.DebitAmount = Convert.ToDouble(txtTotalDebit.Text.ToString());
                _BankExpenses.Validate();
                if (_BankExpenses.IsValid)
                {
                    LockTable.LockTablesForCashBankExpenses();
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        General.BeginTransaction();
                        _BankExpenses.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _BankExpenses.CBVouNo = _BankExpenses.GetAndUpdateBankPaidExpensesNumber(General.ShopDetail.ShopVoucherSeries);
                        _BankExpenses.CreatedBy = General.CurrentUser.Id;
                        _BankExpenses.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _BankExpenses.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _BankExpenses.AddDetails();
                        if (retValue)
                            retValue = SaveDetails();
                       
                            retValue = SaveIntblTrnac();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            string msgLine2 = _BankExpenses.CBVouType + "  " + _BankExpenses.CBVouNo.ToString("#0");
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
                            //  MessageBox.Show("Cash Expenses saved successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _SavedID = _BankExpenses.Id;
                            retValue = true;
                        }
                        else
                        {
                            MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            retValue = false;
                        }
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        General.BeginTransaction();
                        _BankExpenses.ModifiedBy = General.CurrentUser.Id;
                        _BankExpenses.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _BankExpenses.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _BankExpenses.UpdateDetails();
                        if (retValue)
                            retValue = DeletePreviousEntry();
                        if (retValue)
                            retValue = SaveDetails();
                       
                            retValue = SaveIntblTrnac();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            string msgLine2 = _BankExpenses.CBVouType + "  " + _BankExpenses.CBVouNo.ToString("#0");
                            PSDialogResult result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                            retValue = true;
                        }
                        else
                        {
                            PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                            retValue = false;
                        }
                        //MessageBox.Show("Cash Expenses Updated successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _SavedID = _BankExpenses.Id;
                        retValue = true;
                    }
                    else
                    {
                        MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        retValue = false;
                    }

                }
                else // Show Validation Messages
                {
                    LockTable.UnLockTables();
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _BankExpenses.ValidationMessages)
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
            LockTable.UnLockTables();
            return retValue;
        }


        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    FillPartyCombo();
                    _BankExpenses.Id = ID;
                    _BankExpenses.ReadDetailsByID();
                    mcbCreditor.SelectedID = _BankExpenses.CBAccountID;
                    mcbBankAccount.SelectedID = _BankExpenses.CBBankAccountID;
                    txtAmount.Text = _BankExpenses.CBAmount.ToString("0.00");
                    txtNarration.Text = _BankExpenses.CBNarration;
                    txtVouchernumber.Text = _BankExpenses.CBVouNo.ToString();
                    txtChequeNumber.Text = _BankExpenses.CBChequeNumber;
                    datePickerChequeDate.Value = new DateTime(Convert.ToInt32(_BankExpenses.CBChequeDate.Substring(0, 4)), Convert.ToInt32(_BankExpenses.CBChequeDate.Substring(4, 2)), Convert.ToInt32(_BankExpenses.CBChequeDate.Substring(6, 2)));
                    DateTime mydate = new DateTime(Convert.ToInt32(_BankExpenses.CBVouDate.Substring(0, 4)), Convert.ToInt32(_BankExpenses.CBVouDate.Substring(4, 2)), Convert.ToInt32(_BankExpenses.CBVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    InitialisempPVC1();
                    CalculateTotals();
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                    {
                        mcbCreditor.Enabled = true;
                        txtAmount.Enabled = true;
                        txtChequeNumber.Enabled = true;
                        datePickerChequeDate.Enabled = true;
                        txtNarration.Enabled = true;
                        //   AddToolTip();           
                        mcbCreditor.Focus();
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
            try
            {
                FillPartyCombo();

                Account Acc = new Account();
                DataTable dt = Acc.GetOverviewData();
                mpMainSubViewControl1.DataSource = dt;
                mpMainSubViewControl1.ReBindSubGrid();
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
        #endregion

        #region Other Private Methods
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
                txtVouType.Text = FixAccounts.VoucherTypeForBankExpenses;
                datePickerBillDate.ResetText();
                datePickerChequeDate.ResetText();
                mcbCreditor.SelectedID = "";
                txtChequeNumber.Text = "";
                txtAmount.Clear();
                if (mpMainSubViewControl1.Rows.Count > 0)
                    mpMainSubViewControl1.Rows.Clear();
                txtTotalCredit.Text = "0.00";
                txtTotalDebit.Text = "0.00";
                txtNoOfRows.Text = "0";
                this.mcbCreditor.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FormatGrid()
        {
            mpMainSubViewControl1.DoubleColumnNames.Add("Col_Debit");           
        }
        private void FillPartyCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2" };
                mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "200", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetOverviewData();
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
        private bool DeletePreviousEntry()
        {
            bool returnVal = false;
            try
            {
                returnVal = _BankExpenses.DeletePreviousRecords();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return returnVal;
        }

        private void mcbCreditor_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mcbCreditor.SeletedItem == null)
                {
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                }
                else
                {
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillmpPVC1Grid()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _BankExpenses.ReadBillDetailsByID(_BankExpenses.Id);
                mpMainSubViewControl1.DataSourceMain = dtable;
                mpMainSubViewControl1.Bind();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void InitialisempPVC1()
        {
            try
            {
                ConstructMainColumns();
                ConstructSubColumns();               
                mpMainSubViewControl1.NextRowColumn = 3;
                mpMainSubViewControl1.DoubleColumnNames.Add("Col_Debit");           

                DataTable dtable = new DataTable();
                dtable = _BankExpenses.ReadBillDetailsByID(_BankExpenses.Id);
                mpMainSubViewControl1.DataSourceMain = dtable;

                Account Acc = new Account();
                DataTable dt = Acc.GetOverviewData();
                mpMainSubViewControl1.DataSource = dt;

                mpMainSubViewControl1.Bind();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        #region Events

        #region Construct columns

        private void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpMainSubViewControl1.ColumnsMain.Clear();
            try
            {
                //0
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "AccountID";
                column.Width = 0;
                column.Visible = false;
                mpMainSubViewControl1.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Account Name";
                column.Width = 300;
                column.ReadOnly = false;
                mpMainSubViewControl1.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 260;
                column.ReadOnly = true;
                mpMainSubViewControl1.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Debit";
                column.DataPropertyName = "Debit";
                column.HeaderText = "Debit";
                column.ReadOnly = false;
                column.Width = 180;
                column.DefaultCellStyle.Format = "D2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                mpMainSubViewControl1.ColumnsMain.Add(column);
                //4
                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_Credit";
                //column.DataPropertyName = "Credit";
                //column.HeaderText = "Credit";
                //column.Width = 180;
                //column.ReadOnly = false;
                //mpMainSubViewControl1.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructSubColumns()
        {
            mpMainSubViewControl1.ColumnsSub.Clear();
            DataGridViewTextBoxColumn column;

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "ID";
                column.Visible = false;
                mpMainSubViewControl1.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Account";
                column.Width = 200;
                column.ReadOnly = true;
                mpMainSubViewControl1.ColumnsSub.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 150;
                column.ReadOnly = true;
                mpMainSubViewControl1.ColumnsSub.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        private void mcbBankAccount_EnterKeyPressed(object sender, EventArgs e)
        {
            mcbCreditor.Focus();
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtAmount.Focus();
        }
        private void mpMainSubViewControl_OnDetailsFilled(DataGridViewRow selectedRow)
        {
            string mprodID = "";
            int mrowindex = 0;
            int mcindex = 0;
            try
            {
                _BankExpenses.DuplicateAccount = false;
                if (mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ID"].Value != null)
                {
                    mprodID = mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ID"].Value.ToString();
                    mrowindex = mpMainSubViewControl1.MainDataGridCurrentRow.Index;
                }
                foreach (DataGridViewRow prodrow in mpMainSubViewControl1.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null)
                    {
                        _BankExpenses.RAccountID = prodrow.Cells["Col_ID"].Value.ToString();
                        mcindex = prodrow.Index;
                        if (_BankExpenses.RAccountID == mprodID && mrowindex != mcindex)
                        {
                            _BankExpenses.DuplicateAccount = true;
                            mpMainSubViewControl1.Rows.Remove(mpMainSubViewControl1.Rows[mrowindex]);
                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpMainSubViewControl_OnCellValueChangeCommited(int colIndex)
        {
            try
            {
                if (colIndex == 3 || colIndex == 4)
                {
                    if (Convert.ToDouble(mpMainSubViewControl1.MainDataGridCurrentRow.Cells[3].Value) > 0)
                        mpMainSubViewControl1.MainDataGridCurrentRow.Cells[4].Value = 0;
                    CalculateTotals();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void CalculateTotals()
        {
            double totdebit = 0;
            double totcredit = 0;
            int itemCount = 0;
            double mdebit;
            double mcredit;

            try
            {
                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    mdebit = 0;
                    mcredit = 0;
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        itemCount += 1;
                    }
                    if (dr.Cells["Col_Debit"].Value != null)
                        double.TryParse(dr.Cells["Col_Debit"].Value.ToString(), out mdebit);
                    //if (dr.Cells["Col_Credit"].Value != null)
                    //    double.TryParse(dr.Cells["Col_Credit"].Value.ToString(), out mcredit);
                    totdebit += mdebit;
                    totcredit += mcredit;
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtTotalCredit.Text = totcredit.ToString("#0.00");
                txtTotalDebit.Text = totdebit.ToString("#0.00");

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private bool SaveDetails()
        {
            double mdebit;
            double mcredit;
            string macno;
            bool retValue = false;
            try
            {
                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    mdebit = 0;
                    mcredit = 0;
                    macno = "";
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        _BankExpenses.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        macno = dr.Cells["Col_ID"].Value.ToString();
                        if (dr.Cells["Col_Debit"].Value != null)
                            double.TryParse(dr.Cells["Col_Debit"].Value.ToString(), out mdebit);
                        //if (dr.Cells["Col_Credit"].Value != null)
                        //    double.TryParse(dr.Cells["Col_Credit"].Value.ToString(), out mcredit);
                        retValue = _BankExpenses.AddParticularsDetails(_BankExpenses.Id, macno, mdebit, mcredit, _BankExpenses.DetailId);

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

        private bool SaveIntblTrnac()
        {
            double mdebit;
            double mcredit;
            string macno;
            bool retValue = false;

            _BankExpenses.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            retValue = _BankExpenses.AddVoucherIntblTrnac();
            _BankExpenses.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            retValue = _BankExpenses.AddVoucherIntblTrnacReverse();
            try
            {
                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    mdebit = 0;
                    mcredit = 0;
                    macno = "";
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        _BankExpenses.JVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _BankExpenses.JVNo = _BankExpenses.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                        macno = dr.Cells["Col_ID"].Value.ToString();
                        if (dr.Cells["Col_Debit"].Value != null)
                            double.TryParse(dr.Cells["Col_Debit"].Value.ToString(), out mdebit);
                        //if (dr.Cells["Col_Credit"].Value != null)
                        //    double.TryParse(dr.Cells["Col_Credit"].Value.ToString(), out mcredit);

                        //   retValue = _BankExpenses.AddDetailsInmaterJV(_BankExpenses.JVID, macno, FixAccounts.VoucherTypeForJournalEntry, _BankExpenses.JVNo, General.ShopDetail.ShopVoucherSeries, _BankExpenses.CBVouDate, mdebit, mcredit, _BankExpenses.Id, _BankExpenses.CBNarration, _BankExpenses.CreatedDate, _BankExpenses.CreatedTime);
                        _BankExpenses.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        retValue = _BankExpenses.AddJVIntblTrnac(_BankExpenses.JVID, macno, mdebit, mcredit, _BankExpenses.DetailId, _BankExpenses.CBAccountID, _BankExpenses.CBVouDate, FixAccounts.VoucherTypeForJournalEntry, _BankExpenses.Id, _BankExpenses.CBNarration, _BankExpenses.JVNo, _BankExpenses.CBChequeNumber,_BankExpenses.CBChequeDate, _BankExpenses.CreatedBy, _BankExpenses.CreatedDate, _BankExpenses.CreatedTime);
                        _BankExpenses.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        retValue = _BankExpenses.AddJVIntblTrnacReverse(_BankExpenses.JVID, macno, mdebit, mcredit, _BankExpenses.DetailId, _BankExpenses.CBAccountID, _BankExpenses.CBVouDate, FixAccounts.VoucherTypeForJournalEntry, _BankExpenses.Id, _BankExpenses.CBNarration, _BankExpenses.JVNo, _BankExpenses.CBChequeNumber, _BankExpenses.CBChequeDate, _BankExpenses.CreatedBy, _BankExpenses.CreatedDate, _BankExpenses.CreatedTime);
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



        private void mpMainSubViewControl_OnRowDeleted(object sender, EventArgs e)
        {
            try
            {
                CalculateTotals();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            string selectedId = ((PSComboBoxNew)sender).SelectedID;
            try
            {
                FillPartyCombo();
                mcbCreditor.SelectedID = selectedId;
                if (mcbCreditor.SeletedItem != null)
                {
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    mpMainSubViewControl1.SetFocus(1);
                else if (e.KeyCode == Keys.Up)
                    datePickerBillDate.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

      
        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClearGrid();
                txtChequeNumber.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                mcbCreditor.Focus();
        }
       
        public void ClearGrid()
        {
            try
            {
                //txtAmtNotAdjusted.Text = txtAmountReceived.Text;
                //_BankPayment.CellOldValueAmount = 0;
                //foreach (DataGridViewRow dr in mpMSCSale.Rows)
                //{
                //    dr.Cells["Col_GetClearedAmount"].Value = 0;
                //}
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtChequeNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                datePickerChequeDate.Focus();
            else if (e.KeyCode == Keys.Up)
                txtAmount.Focus();
        }
        private void datePickerChequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                datePickerBillDate.Focus();
            else if (e.KeyCode == Keys.Up)
                txtChequeNumber.Focus();
        }
        private void datePickerBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNarration.Focus();
            else if (e.KeyCode == Keys.Up)
                datePickerChequeDate.Focus();
        }     
        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtVouchernumber.Text != "")
                    {
                        _BankExpenses.CBVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _BankExpenses.ReadDetailsByVouNumber();
                        FillSearchData(_BankExpenses.Id, "");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            // ttToolTip.SetToolTip(txtAmount, "Write Full Amount and in the grid select VAT Account and VAT Amount");
        }
        #endregion

        private void mpMainSubViewControl1_OnCellValueChangeCommited(int colIndex)
        {
            if (colIndex == 3)
            {
                CalculateTotals();
            }
        }

      

    }
}
