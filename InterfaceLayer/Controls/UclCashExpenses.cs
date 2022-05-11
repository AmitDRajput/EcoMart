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
using EcoMart.InterfaceLayer.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclCashExpenses : BaseControl
    {
        #region Declaration
        private CashExpenses _CashExpenses;
        #endregion

        # region Constructor

        public UclCashExpenses()
        {
            try
            {
                InitializeComponent();
                _CashExpenses = new CashExpenses();
                SearchControl = new UclCashExpensesSearch();
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
                    mcbCreditor.Focus();
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
                _CashExpenses.Initialise();
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
                headerLabel1.Text = "CASH EXPENSES -> NEW";
                AddToolTip();
                FillPartyCombo();
                mcbCreditor.Enabled = true;
                txtAmount.Enabled = true;
                mpMainSubViewControl1.Enabled = true;
                pnlNameAddress.Enabled = true;
                pnlVou.Enabled = true;             
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
                headerLabel1.Text = "CASH EXPENSES -> EDIT";
                InitialisempPVC1();
                FillPartyCombo();
                mcbCreditor.Enabled = false;
                txtAmount.Enabled = false;
                pnlNameAddress.Enabled = true;
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
                headerLabel1.Text = "CASH EXPENSES -> DELETE";
                ClearData();
                InitialisempPVC1();
                FillPartyCombo();
                mcbCreditor.Enabled = false;
                txtAmount.Enabled = false;
                pnlNameAddress.Enabled = true;
                pnlVou.Enabled = true;
                mpMainSubViewControl1.Enabled = false;
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
                if (_CashExpenses.Id != null && _CashExpenses.Id != "")
                {
                    LockTable.LockTablesForCashBankExpenses();

                    if (_CashExpenses.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _CashExpenses.DeleteDetails();
                        if (retValue)
                            retValue = _CashExpenses.DeletePreviousRecords();
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
                headerLabel1.Text = "CASH EXPENSES -> VIEW";
                ClearData();
                InitialisempPVC1();
                FillPartyCombo();
                mcbCreditor.Enabled = false;
                txtAmount.Enabled = false;
                pnlNameAddress.Enabled = true;
                mpMainSubViewControl1.Enabled = false;
                pnlVou.Enabled = true;
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
            if (txtAmount.Text != null && Convert.ToDouble(txtAmount.Text.ToString()) > 0)
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
            int colcount = mpMainSubViewControl1.ColumnsMain.Count;
            PSMainSubViewControl GridForPrint = new PSMainSubViewControl();
            GridForPrint = mpMainSubViewControl1;

            try
            {

                foreach (DataGridViewRow dr in GridForPrint.Rows)
                {
                    if (dr.Cells[0].Value != null)
                    {
                        int printgridindex = PrintGrid.Rows.Add();

                        if (dr.Cells["Col_AccountName"].Value != null)
                            PrintGrid.Rows[printgridindex].Cells["Col_ProductName"].Value = dr.Cells["Col_AccountName"].Value.ToString();
                        double amtdb = Convert.ToDouble(dr.Cells["Col_Debit"].Value.ToString());
                        double amtcr = Convert.ToDouble(dr.Cells["Col_Credit"].Value.ToString());
                        if ( amtdb+amtcr > 0)

                            PrintGrid.Rows[printgridindex].Cells["Col_Amount"].Value = (amtdb + amtcr).ToString();


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
            string atow = General.AmountToWord(_CashExpenses.CBAmount);
            printer.Print(_CashExpenses.CBVouType, _CashExpenses.CBVouNo.ToString(), _CashExpenses.CBVouDate, _CashExpenses.CBName, _CashExpenses.CBAddress1, _CashExpenses.CBAddress2, "", "", PrintGrid.Rows, _CashExpenses.CBNarration, _CashExpenses.CBAmount, "", 0, 0, 0, 0, 0, atow);

        }

        private void PrintCashBankVoucherPlainPaper()
        {
            EcoMart.Printing.PlainPaperPrinter printer = new EcoMart.Printing.PlainPaperPrinter();
            string atow = General.AmountToWord(_CashExpenses.CBAmount);
            printer.Print(_CashExpenses.CBVouType, _CashExpenses.CBVouNo.ToString(), _CashExpenses.CBVouDate, _CashExpenses.CBName, _CashExpenses.CBAddress1, _CashExpenses.CBAddress2, "", "", PrintGrid.Rows, _CashExpenses.CBNarration, _CashExpenses.CBAmount, "", 0, 0, 0, 0, 0, atow);

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
                _CashExpenses.CBAccountID = mcbCreditor.SelectedID;
                _CashExpenses.CBName = mcbCreditor.SeletedItem.ItemData[2].ToString();
                _CashExpenses.CBAmount = Convert.ToDouble(txtAmount.Text.ToString());
                _CashExpenses.CBVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                if (txtNarration.Text != null)
                    _CashExpenses.CBNarration = txtNarration.Text.ToString();
                _CashExpenses.Validate();
                if (_CashExpenses.IsValid)
                {
                    LockTable.LockTablesForCashBankExpenses();
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        General.BeginTransaction();
                       // _CashExpenses.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _CashExpenses.CBVouNo = _CashExpenses.GetAndUpdateCPENumber(General.ShopDetail.ShopVoucherSeries);                       
                        _CashExpenses.CreatedBy = General.CurrentUser.Id;
                        _CashExpenses.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _CashExpenses.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _CashExpenses.IntID = 0;
                        _CashExpenses.IntID = _CashExpenses.AddDetails();
                        if (_CashExpenses.IntID > 0)
                            retValue = true;
                        else
                            retValue = false;
                        _SavedID = _CashExpenses.Id;

                        //retValue = _CashExpenses.AddDetails();
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
                            string msgLine2 = _CashExpenses.CBVouType + "  " + _CashExpenses.CBVouNo.ToString("#0");
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
                            _SavedID = _CashExpenses.Id;
                            retValue = true;
                        }
                        else
                        {
                            PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                            retValue = false;
                        }
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        General.BeginTransaction();
                        _CashExpenses.ModifiedBy = General.CurrentUser.Id;
                        _CashExpenses.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _CashExpenses.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _CashExpenses.UpdateDetails();
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
                            string msgLine2 = _CashExpenses.CBVouType + "  " + _CashExpenses.CBVouNo.ToString("#0");
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
                            _SavedID = _CashExpenses.Id;
                            retValue = true;
                        }
                        else
                        {
                            MessageBox.Show("Could not Update...", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            retValue = false;
                        }
                    }
                }
                else // Show Validation Messages
                {
                    LockTable.UnLockTables();
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _CashExpenses.ValidationMessages)
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
                    _CashExpenses.Id = ID;
                    _CashExpenses.ReadDetailsByID();
                    mcbCreditor.SelectedID = _CashExpenses.CBAccountID;
                    txtAmount.Text = _CashExpenses.CBAmount.ToString("0.00");
                    txtNarration.Text = _CashExpenses.CBNarration;
                    txtVouchernumber.Text = _CashExpenses.CBVouNo.ToString();
                    DateTime mydate = new DateTime(Convert.ToInt32(_CashExpenses.CBVouDate.Substring(0, 4)), Convert.ToInt32(_CashExpenses.CBVouDate.Substring(4, 2)), Convert.ToInt32(_CashExpenses.CBVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    InitialisempPVC1();
                    CalculateTotals();
                    //   AddToolTip(); 
                    if (_Mode == OperationMode.Edit)
                    {
                        pnlNameAddress.Enabled = true;
                        mcbCreditor.Enabled = true;
                        txtAmount.Enabled = true;
                        txtNarration.Enabled = true;
                        mpMainSubViewControl1.ClearSelection();
                        txtVouchernumber.Enabled = false;
                        mpMainSubViewControl1.Enabled = true;
                        mcbCreditor.Focus();                        
                    }

                    mcbCreditor.Focus();
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
            try
            {
                if (closedControl is UclAccount)
                {
                    FillPartyCombo();

                    Account Acc = new Account();
                    DataTable dt = Acc.GetOverviewData();
                    mpMainSubViewControl1.DataSource = dt;
                    mpMainSubViewControl1.ReBindSubGrid();
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
                tsBtnFifth.Visible = false;
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                txtVouchernumber.Clear();
                lblFooterMessage.Text = "Enter VAT Amount if Required In The Grid with VAT Account";
                txtVouType.Text = FixAccounts.VoucherTypeForCashExpenses;             
                mcbCreditor.SelectedID = "";
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
        private bool DeletePreviousEntry()
        {
            bool returnVal = false;
            try
            {
                returnVal = _CashExpenses.DeletePreviousRecords();
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
                dtable = _CashExpenses.ReadBillDetailsByID(_CashExpenses.Id);
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
                //ConstructBatchGridColumns();
                mpMainSubViewControl1.NextRowColumn = 4;
                mpMainSubViewControl1.DoubleColumnNames.Add("Col_Debit");
                mpMainSubViewControl1.DoubleColumnNames.Add("Col_Credit");

                DataTable dtable = new DataTable();
                dtable = _CashExpenses.ReadBillDetailsByID(_CashExpenses.Id);
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
                column.HeaderText = "VAT Account Name";
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
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;    
                mpMainSubViewControl1.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Credit";
                column.DataPropertyName = "Credit";
                column.HeaderText = "Credit";
                column.Width = 180;
                column.ReadOnly = false;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;    
                mpMainSubViewControl1.ColumnsMain.Add(column);
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
                _CashExpenses.DuplicateAccount = false;
                if (mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ID"].Value != null)
                {
                    mprodID = mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ID"].Value.ToString();
                    mrowindex = mpMainSubViewControl1.MainDataGridCurrentRow.Index;
                }
                foreach (DataGridViewRow prodrow in mpMainSubViewControl1.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null)
                    {
                        _CashExpenses.RAccountID = prodrow.Cells["Col_ID"].Value.ToString();
                        mcindex = prodrow.Index;
                        if (_CashExpenses.RAccountID == mprodID && mrowindex != mcindex)
                        {
                            _CashExpenses.DuplicateAccount = true;
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
                    if (dr.Cells["Col_Credit"].Value != null)
                        double.TryParse(dr.Cells["Col_Credit"].Value.ToString(), out mcredit);
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
            int macno;
            bool retValue = false;
            try
            {
                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    mdebit = 0;
                    mcredit = 0;
                    macno = 0;
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                      //  _CashExpenses.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        macno = Convert.ToInt32(dr.Cells["Col_ID"].Value);
                        if (dr.Cells["Col_Debit"].Value != null)
                            double.TryParse(dr.Cells["Col_Debit"].Value.ToString(), out mdebit);
                        if (dr.Cells["Col_Credit"].Value != null)
                            double.TryParse(dr.Cells["Col_Credit"].Value.ToString(), out mcredit);
                        retValue = _CashExpenses.AddParticularsDetails(_CashExpenses.IntID ,macno, mdebit, mcredit, _CashExpenses.DetailId);
                        
                        if (retValue == false)
                            break;
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

        private bool SaveIntblTrnac() // chnages here only
        {
            double mdebit;
            double mcredit;
            string macno;
            bool retValue = false;

          //  _CashExpenses.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            retValue = _CashExpenses.AddVoucherIntblTrnac();
          //  _CashExpenses.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            retValue = _CashExpenses.AddVoucherIntblTrnacReverse();
            try
            {
                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    mdebit = 0;
                    mcredit = 0;
                    macno = "";
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                      //  _CashExpenses.JVID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _CashExpenses.JVNo = _CashExpenses.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                        macno = dr.Cells["Col_ID"].Value.ToString();
                        if (dr.Cells["Col_Debit"].Value != null)
                            double.TryParse(dr.Cells["Col_Debit"].Value.ToString(), out mdebit);
                        if (dr.Cells["Col_Credit"].Value != null)
                            double.TryParse(dr.Cells["Col_Credit"].Value.ToString(), out mcredit);
                        _CashExpenses.JVID = _CashExpenses.JVNo.ToString();
                       int mjvid  = _CashExpenses.AddDetailsInmaterJV(_CashExpenses.JVID, macno, FixAccounts.VoucherTypeForJournalEntry, _CashExpenses.JVNo, General.ShopDetail.ShopVoucherSeries, _CashExpenses.CBVouDate, mdebit, mcredit, _CashExpenses.Id, _CashExpenses.CBNarration, _CashExpenses.CreatedBy, _CashExpenses.CreatedDate, _CashExpenses.CreatedTime);
                        _CashExpenses.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        retValue = _CashExpenses.AddJVIntblTrnac(_CashExpenses.JVID, macno, mdebit, mcredit, _CashExpenses.DetailId, _CashExpenses.CBAccountID, _CashExpenses.CBVouDate, FixAccounts.VoucherTypeForJournalEntry, _CashExpenses.IntID.ToString(), _CashExpenses.CBNarration, _CashExpenses.JVNo, _CashExpenses.CreatedBy, _CashExpenses.CreatedDate, _CashExpenses.CreatedTime);
                        _CashExpenses.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        retValue = _CashExpenses.AddJVIntblTrnacReverse(_CashExpenses.JVID, macno, mdebit, mcredit, _CashExpenses.DetailId, _CashExpenses.CBAccountID, _CashExpenses.CBVouDate, FixAccounts.VoucherTypeForJournalEntry, _CashExpenses.IntID.ToString() , _CashExpenses.CBNarration, _CashExpenses.JVNo, _CashExpenses.CreatedBy, _CashExpenses.CreatedDate, _CashExpenses.CreatedTime);
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
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtNarration.Focus();
                    break;
                case Keys.Up:
                    mcbCreditor.Focus();
                    break;
            }
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

        }

        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            ttToolTip.SetToolTip(txtAmount, "Write Full Amount and in the grid select VAT Account and VAT Amount");
        }
        #endregion

        private void UclCashExpenses_Load(object sender, EventArgs e)
        {
            datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
        }


    }
}
