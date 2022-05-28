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
using PrintDataGrid;
using PaperlessPharmaRetail.Common.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclCreditNoteStock : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private CreditNoteStock _CNStock;
        private string deletedproductname = "";
        DataTable dtTempPatientSale;
        int proID;
        //string _lastCustIdSelected = "1";
     //   private SSSale _SSSale;
        Timer DateTimer;
        #endregion

        #region constructor
        public UclCreditNoteStock()
        {
            try
            {
                InitializeComponent();
                _CNStock = new CreditNoteStock();
                SearchControl = new UclCreditNoteStockSearch();
                DateTimer = new Timer();
                DateTimer.Tick += new EventHandler(DateTimer_Tick);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        #region IDetail Control
        private void ConstructLastPurchaseColumns()    //Amar 
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
                column.Width = 60;
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
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";//PurchaseRate
                column.HeaderText = "Sale Rate";
                column.Width = 65;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CashDiscount";
                column.DataPropertyName = "Discount";
                column.HeaderText = "Discount";
                column.Width = 75;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Qty";
                column.DataPropertyName = "SaleQuantity";
                column.HeaderText = "Quantity";
                column.Width = 65;
                column.ReadOnly = true;
                //if (General.ShopDetail.ShopDistributorSale == "Y")
                //    column.Visible = true;
                //else
                //    column.Visible = false;
                column.Visible = true;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SchemeQty";
                column.DataPropertyName = "Scheme";
                column.HeaderText = "SCM Qty";
                column.Width = 65;
                column.ReadOnly = true;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNo";
                column.DataPropertyName = "VoucherNo";
                column.HeaderText = "Bill No";
                column.Width = 65;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLastPurchase.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Name of party";
                column.Width = 140;
                column.ReadOnly = true;
                column.Visible = false;
                dgvLastPurchase.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
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
                _CNStock.Initialise();
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
                pnlVouTypeNo.Enabled = true;
                AddToolTip();
                headerLabel1.Text = "CREDIT NOTE STOCK -> NEW";
                FillPartyCombo();
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
                headerLabel1.Text = "CREDIT NOTE STOCK -> EDIT";
                FillPartyCombo();
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
            System.IO.File.Delete(General.GetDebitNoteStockTempFile());
            return retValue;
        }
        public override bool Exit()
        {
            bool retValue = base.Exit();
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "CREDIT NOTE STOCK -> DELETE";
                ClearData();
                txtVouchernumber.Focus();
                FillPartyCombo();
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
                if (_CNStock.Id != null && _CNStock.Id != "")
                {
                    bool canbe = _CNStock.CanBeDeleted();
                    if (canbe)
                    {
                        LockTable.LockTablesForCreditDebitNoteStock();
                        General.BeginTransaction();
                        retValue = DeletePreviousRows();
                        if (retValue)
                            retValue = _CNStock.DeleteDetails();
                        if (retValue)
                            retValue = ReducePreviousStock();
                        _CNStock.RemoveAccountDetails();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            // UpdateClosingStockinCache();
                            _SavedID = _CNStock.Id;
                            MessageBox.Show("Voucher Deleted Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            retValue = true;
                        }
                        else
                        {
                            PSDialogResult result = PSMessageBox.Show("Could not Delete...", deletedproductname, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                            retValue = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot Delete.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        retValue = false;
                    }
                }
                pnlVouTypeNo.Enabled = true;
                ClearData();
                txtVouchernumber.Focus();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                ClearData();
                headerLabel1.Text = "CREDIT NOTE STOCK -> VIEW";
                FillPartyCombo();
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
                // GetLastRecord();
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
                    _CNStock.CrdbVouType = FixAccounts.VoucherTypeForCreditNoteStock;
                }
                _CNStock.GetLastRecord();
                FillSearchData(_CNStock.Id, "");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public override bool Print()
        {
            bool retValue = true;
            if (txtBillAmount.Text != null && Convert.ToDouble(txtBillAmount.Text.ToString()) > 0)
            {
                if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                    PrintCreditnNotePrePrintedPaper();
                else
                    PrintCreditnNotePlainPaper();
            }
            ClearData();
            return retValue;
        }
        private void PrintCreditnNotePrePrintedPaper()
        {
            EcoMart.Printing.PrePrintedPaperPrinter printer = new EcoMart.Printing.PrePrintedPaperPrinter();
            printer.Print(_CNStock.CrdbVouType, _CNStock.CrdbVouNo.ToString(), _CNStock.CrdbVouDate, "", _CNStock.CrdbAddress, "", "", "", mpPVC1.Rows, _CNStock.CrdbNarration, _CNStock.CrdbAmountNet, "", _CNStock.CrdbDiscAmt, 0, 0, _CNStock.CrdbAmount, 0 + _CNStock.CrdbAmountNet, "");

        }

        private void PrintCreditnNotePlainPaper()
        {
            EcoMart.Printing.PlainPaperPrinter printer = new EcoMart.Printing.PlainPaperPrinter();
            printer.Print(_CNStock.CrdbVouType, _CNStock.CrdbVouNo.ToString(), _CNStock.CrdbVouDate, "", _CNStock.CrdbAddress, "", "", "", mpPVC1.Rows, _CNStock.CrdbNarration, _CNStock.CrdbAmountNet, "", _CNStock.CrdbDiscAmt, 0, 0, _CNStock.CrdbAmount, 0 + _CNStock.CrdbAmountNet, "");

        }

        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _CNStock.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _CNStock.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _CNStock.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _CNStock.GetFirstRecord();
            if (dr != null && dr["CRDBID"] != DBNull.Value)
            {
                _CNStock.Id = dr["CRDBID"].ToString();
                FillSearchData(_CNStock.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _CNStock.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _CNStock.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _CNStock.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _CNStock.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _CNStock.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _CNStock.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _CNStock.CrdbVouNo = i;
                dr = _CNStock.ReadDetailsByVouNumber(_CNStock.CrdbVouType, _CNStock.CrdbVouNo);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CRDBID"] != DBNull.Value)
            {
                _CNStock.Id = dr["CRDBID"].ToString();
                FillSearchData(_CNStock.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _CNStock.GetLastVoucherNumber(FixAccounts.VoucherTypeForCreditNoteStock, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _CNStock.CrdbVouType = txtVouType.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _CNStock.CrdbVouNo = i;
                dr = _CNStock.ReadDetailsByVouNumber(_CNStock.CrdbVouType, i);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CRDBID"] != DBNull.Value)
            {
                _CNStock.Id = dr["CRDBID"].ToString();
                FillSearchData(_CNStock.Id, "");
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
            if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView)
            {
                double mdiscamount = 0;
                double mvat5per = 0;
                double mvat12point5per = 0;
                double mdiscper = 0;
                double mbillamount = 0;
                double mamount = 0;
                double mround = 0;
                double mtotalamount = 0;
                System.Text.StringBuilder _errorMessage;
                try
                {
                    if (mcbCreditor.SelectedID != null)
                        _CNStock.CrdbId = mcbCreditor.SelectedID.Trim();
                    _CNStock.CrdbNarration = txtNarration.Text.Trim();
                    _CNStock.CrdbVouType = txtVouType.Text.Trim();
                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _CNStock.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                    _CNStock.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    double.TryParse(txtVatInput5per.Text, out mvat5per);
                    _CNStock.CrdbVat5 = mvat5per;
                    double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                    _CNStock.CrdbVat12point5 = mvat12point5per;
                    double.TryParse(txtDiscPercent.Text, out mdiscper);
                    _CNStock.CrdbDiscPer = mdiscper;
                    double.TryParse(txtDiscAmount.Text, out mdiscamount);
                    _CNStock.CrdbDiscAmt = mdiscamount;
                    double.TryParse(txtBillAmount.Text, out mbillamount);
                    _CNStock.CrdbAmountNet = mbillamount;
                    double.TryParse(txtAmount.Text, out mamount);
                    _CNStock.CrdbAmount = mamount;
                    double.TryParse(txtTotalAmount.Text, out mtotalamount);
                    _CNStock.CrdbTotalAmount = mtotalamount;
                    double.TryParse(txtRoundAmount.Text, out mround);
                    _CNStock.CrdbRoundAmount = mround;
                    if (cbTransferToAccount.Checked == true)
                    {
                        _CNStock.TrasferToAccount = "Y";
                        _CNStock.ClearVouType = FixAccounts.VoucherTypeForTransferToAccount;
                    }
                    else
                    {
                        _CNStock.TrasferToAccount = "N";
                        _CNStock.ClearVouType = "";
                    }
                    if (_CNStock.TrasferToAccount == "Y")
                        _CNStock.CrdbAmountClear = mbillamount;
                    else
                        _CNStock.CrdbAmountClear = 0;
                    if (_Mode == OperationMode.Edit)
                        _CNStock.IFEdit = "Y";
                    _CNStock.Validate();

                    if (_CNStock.IsValid)
                    {
                        LockTable.LockTablesForCreditDebitNoteStock();
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();
                            _CNStock.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CNStock.CrdbVouNo = _CNStock.GetAndUpdateCNNumber(General.ShopDetail.ShopVoucherSeries);
                            txtVouchernumber.Text = Convert.ToString(_CNStock.CrdbVouNo);
                            _CNStock.CreatedBy = General.CurrentUser.Id;
                            _CNStock.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _CNStock.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            int ii = _CNStock.AddDetails();
                            _SavedID = _CNStock.Id;
                            if (retValue)
                                retValue = SaveParticularsProductwise();

                            if (retValue && _CNStock.TrasferToAccount == "Y")
                            {
                                SaveIntblTrnac();
                            }

                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                            if (retValue)
                            {
                                // UpdateClosingStockinCache();
                                string msgLine2 = _CNStock.CrdbVouType + "  " + _CNStock.CrdbVouNo.ToString("#0");
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
                            }
                            else
                            {
                                PSDialogResult result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                retValue = false;
                            }
                        }
                        else
                        {
                            if (_Mode == OperationMode.Edit)
                            {
                                General.BeginTransaction();
                                _CNStock.ModifiedBy = General.CurrentUser.Id;
                                _CNStock.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _CNStock.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");

                                retValue = CheckStockForDeletedRows();

                                ////////////////////////////////////////////////////////////////////
                                if (retValue)
                                {
                                    retValue = DeletePreviousRows();
                                    _CNStock.DeleteFromtblTrnac();
                                    if (retValue)
                                        retValue = SaveParticularsProductwise();
                                    if (retValue)
                                        retValue = ReducePreviousStock();
                                    if (retValue)
                                        retValue = _CNStock.UpdateDetails();
                                    if (retValue && _CNStock.TrasferToAccount == "Y")
                                    {
                                        SaveIntblTrnac();
                                    }
                                    if (retValue)
                                        General.CommitTransaction();
                                    else
                                        General.RollbackTransaction();
                                    LockTable.UnLockTables();
                                    if (retValue)
                                    {
                                        // UpdateClosingStockinCache();
                                        string msgLine2 = _CNStock.CrdbVouType + "  " + _CNStock.CrdbVouNo.ToString("#0");
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
                                        PSDialogResult result = PSMessageBox.Show("Could not Update...", deletedproductname, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                        retValue = false;
                                    }

                                }

                                else
                                {
                                    MessageBox.Show("Can not Update " + deletedproductname, General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    General.RollbackTransaction();
                                    deletedproductname = "";
                                    LockTable.UnLockTables();
                                }
                            }
                        }
                    }
                    else // Show Validation Messages
                    {
                        LockTable.UnLockTables();
                        _errorMessage = new System.Text.StringBuilder();
                        _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                        foreach (string _message in _CNStock.ValidationMessages)
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
            LockTable.UnLockTables();
            CacheObject.Clear("TempCounterSale");
            return retValue;
        }

        internal void SaveIntblTrnac()
        {
            double mdebit = 0;
            mdebit = Math.Round(_CNStock.CrdbAmountNet - _CNStock.CrdbVat5 - _CNStock.CrdbVat12point5 - _CNStock.CrdbRoundAmount, 2);
            if (_CNStock.CrdbVat5 > 0)
            {
                _CNStock.CreditAccount = FixAccounts.AccountSalesReturn;
                _CNStock.DebitAccount = FixAccounts.AccountVatOutput6Sale;
                _CNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _CNStock.DebitAmount = _CNStock.CrdbVat5;
                _CNStock.CreditAmount = 0;
                _CNStock.AddVoucherIntblTrnac();

            }
            if (_CNStock.CrdbVat12point5 > 0)
            {
                _CNStock.CreditAccount = FixAccounts.AccountSalesReturn;
                _CNStock.DebitAccount = FixAccounts.AccountVatOutput13point5Sale;
                _CNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _CNStock.DebitAmount = _CNStock.CrdbVat12point5;
                _CNStock.CreditAmount = 0;
                _CNStock.AddVoucherIntblTrnac();

            }
            if (_CNStock.CrdbRoundAmount < 0)
            {
                _CNStock.CreditAccount = FixAccounts.AccountRoundoffSale;
                _CNStock.DebitAccount = FixAccounts.AccountSalesReturn;
                _CNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _CNStock.DebitAmount = (_CNStock.CrdbRoundAmount * -1);
                _CNStock.CreditAmount = 0;
                _CNStock.AddVoucherIntblTrnac();
            }
            if (_CNStock.CrdbRoundAmount > 0)
            {
                _CNStock.DebitAccount = FixAccounts.AccountRoundoffSale;
                _CNStock.CreditAccount = FixAccounts.AccountSalesReturn;
                _CNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _CNStock.DebitAmount = 0;
                _CNStock.CreditAmount = _CNStock.CrdbRoundAmount;
                _CNStock.AddVoucherIntblTrnac();
            }
            if (mdebit > 0)
            {
                _CNStock.DebitAccount = FixAccounts.AccountSalesReturn;
                _CNStock.CreditAccount = FixAccounts.AccountCash;
                _CNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _CNStock.DebitAmount = mdebit;
                _CNStock.CreditAmount = 0;
                _CNStock.AddVoucherIntblTrnac();

            }
            if (_CNStock.CrdbAmountNet > 0)
            {

                _CNStock.DebitAccount = FixAccounts.AccountCash;
                _CNStock.CreditAccount = FixAccounts.AccountSalesReturn;

                _CNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _CNStock.DebitAmount = 0;
                _CNStock.CreditAmount = _CNStock.CrdbAmountNet;
                _CNStock.AddVoucherIntblTrnac();

            }
        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _CNStock.Id = ID;
                    _CNStock.ReadDetailsByID();
                    BindTempGrid();
                    InitialisempPVC1();
                    NumberofRows();
                    AddToolTip();
                    //
                    mcbCreditor.SelectedID = _CNStock.CrdbId.ToString();
                    if (_Mode == OperationMode.Edit)
                        mcbCreditor.Focus();
                    //
                    txtNarration.Text = _CNStock.CrdbNarration.ToString();
                    txtVouType.Text = FixAccounts.VoucherTypeForCreditNoteStock;
                    txtVouchernumber.Text = _CNStock.CrdbVouNo.ToString().Trim();
                    if (_CNStock.ClearedIn != "")
                    {
                        lblAdjustedIn.Visible = true;
                        txtAdjustedIn.Visible = true;
                        txtAdjustedIn.Text = _CNStock.ClearedIn;
                    }

                    if (_CNStock.CrdbVat5 >= 0)
                        txtVatInput5per.Text = _CNStock.CrdbVat5.ToString("#0.00");
                    if (_CNStock.CrdbVat12point5 >= 0)
                        txtVatInput12point5per.Text = _CNStock.CrdbVat12point5.ToString("#0.00");
                    if (_CNStock.CrdbDiscPer >= 0)
                        txtDiscPercent.Text = _CNStock.CrdbDiscPer.ToString("#0.00");
                    if (_CNStock.CrdbDiscAmt > 0)
                        txtDiscAmount.Text = _CNStock.CrdbDiscAmt.ToString("#0.00");

                    txtBillAmount.Text = _CNStock.CrdbAmountNet.ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                    txtAmount.Text = _CNStock.CrdbAmount.ToString("#0.00");
                    if (_CNStock.TrasferToAccount == "Y")
                        cbTransferToAccount.Checked = true;
                    else
                        cbTransferToAccount.Checked = false;
                    if (_CNStock.CrdbRoundAmount != 0)
                        txtRoundAmount.Text = _CNStock.CrdbRoundAmount.ToString("#0.00");
                    txtTotalAmount.Text = _CNStock.CrdbTotalAmount.ToString("#0.00");
                    DateTime mydate = new DateTime(Convert.ToInt32(_CNStock.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_CNStock.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_CNStock.CrdbVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    mpPVC1.SetFocus(1);
                    if (_CNStock.ClearVouNo > 0 && _Mode == OperationMode.Edit)
                    {
                        lblFooterMessage.Text = "Credit Note Cleared in Purchase Voucher:" + _CNStock.ClearVouType + " " + _CNStock.ClearVouNo.ToString();
                        Cancel();
                    }

                    if (_Mode == OperationMode.Delete || _Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                    {
                        pnlAmounts.Enabled = false;
                        mpPVC1.ColumnsMain["Col_ProductName"].ReadOnly = true;
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        pnlVouTypeNo.Enabled = true;
                        pnlAmounts.Enabled = true;
                        mcbCreditor.Enabled = true;
                        txtNarration.Enabled = true;
                    }
                }
                else
                {
                    ClearControls();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
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


        private bool CheckStockForDeletedRows()
        {
            bool returnVal = true;
            string gridstockid;
            int CurrentClosingStock = 0;
            deletedproductname = "";
            //bool ifRecordFound = false;
            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_StockID"].Value != null &&
                        (Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0
                         || Convert.ToDouble(prodrow.Cells["Temp_Scheme"].Value) > 0
                         || Convert.ToDouble(prodrow.Cells["Temp_Replacement"].Value) > 0))
                    {
                        _CNStock.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        deletedproductname = prodrow.Cells["Temp_ProductName"].Value.ToString().Trim() + " " + prodrow.Cells["Temp_UOM"].Value.ToString().Trim() + " " + prodrow.Cells["Temp_Pack"].Value.ToString().Trim();
                        //_Purchase.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
                        //_Purchase.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        //_Purchase.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _CNStock.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _CNStock.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UOM"].Value.ToString());
                        string ifmatchfound = "N";
                        foreach (DataGridViewRow gridrow in mpPVC1.Rows)
                        {
                            gridstockid = "";
                            if (gridrow.Cells["Col_StockID"].Value != null && gridrow.Cells["Col_StockID"].Value.ToString() != "")
                                gridstockid = gridrow.Cells["Col_StockID"].Value.ToString();
                            if (_CNStock.StockID == gridstockid)
                            {
                                deletedproductname = "";
                                ifmatchfound = "Y";
                                break;
                            }

                        }
                        if (ifmatchfound == "N")
                        {
                            CurrentClosingStock = _CNStock.GetCurrentClosingStock(_CNStock.StockID);
                            if (CurrentClosingStock < (_CNStock.Quantity + _CNStock.SchemeQuanity))
                            {
                                returnVal = false;
                                break;
                            }
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


        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData(Control closedControl)
        {
            if (closedControl is UclAccount)
            {
                string preselectedID = "";
                if (mcbCreditor.SelectedID != null)
                    preselectedID = mcbCreditor.SelectedID;
                FillPartyCombo();
                mcbCreditor.SelectedID = preselectedID;
            }
        }
        public override bool RefreshProductList()
        {
            //// mpPVC1.DataSourceProductList = EcoMartCache.GetProductData();
            // DataTable dtable = new DataTable();
            // //dtable = General.ProductList;
            // Product prod = new Product();
            // dtable = prod.GetOverviewData();
            // mpPVC1.DataSourceProductList = dtable;
            // mpPVC1.BindGridProductList();          
            return true;
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;


            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                mcbCreditor.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.N && modifier == Keys.Alt)
            {
                txtNarration.Focus();
                retValue = true;
            }

            if (keyPressed == Keys.Escape)
            {
                dgvLastPurchase.Visible = false;  //Amar
                if (mpPVC1.VisibleProductGrid() == true) //kiran
                {
                    retValue = true;
                }
                else
                    retValue = Exit();
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }

        private bool DeletePreviousRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _CNStock.DeletePreviousRecords();
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
                returnVal = false;
            }
            return returnVal;
        }

        private bool SaveParticularsProductwise()
        {
            {
                bool returnVal = false;
                _CNStock.SerialNumber = 0;
                int oldTempStock = 0;
                int CurrentClosingStock = 0;
                int ThisStockID = 0;
                DataRow dr;
                try
                {
                    foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                    {
                        if (prodrow.Cells["Col_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Amount"].Value) > 0)
                        {
                            _CNStock.SerialNumber += 1;
                            string mexpdate = "";
                            _CNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CNStock.StockID = "";
                            _CNStock.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
                            _CNStock.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            _CNStock.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Col_UOM"].Value.ToString());
                            _CNStock.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                            _CNStock.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurRate"].Value);
                            _CNStock.ReturnRate = Convert.ToDouble(prodrow.Cells["Col_ReturnRate"].Value);
                            _CNStock.TradeRate = _CNStock.PurchaseRate;
                            _CNStock.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                            _CNStock.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value);
                            if (_CNStock.SaleRate == 0)
                                _CNStock.SaleRate = _CNStock.MRP;

                            _CNStock.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            //  _CNStock.Expiry = General.GetValidExpiry(prodrow.Cells["Col_Expiry"].Value.ToString()); 
                            if (_CNStock.Expiry != "00/00")
                                mexpdate = General.GetValidExpiryDate(_CNStock.Expiry);
                            if (mexpdate != "")
                                _CNStock.ExpiryDate = General.GetExpiryInyyyymmddForm(mexpdate);

                            _CNStock.VATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value);
                            _CNStock.ReasonCode = prodrow.Cells["Col_Code"].Value.ToString();

                            if (prodrow.Cells["Col_StockID"].Value != null)
                                _CNStock.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                            _CNStock.Amount = Convert.ToDouble(prodrow.Cells["Col_Amount"].Value);

        // check ss 18-10                    ThisStockID = _CNStock.CheckForBatchMRPInStockTable();
                            _CNStock.IntStockID = ThisStockID;
                            if (_CNStock.StockID != null && _CNStock.StockID != "")
                            {
                                dr = _CNStock.IfStockIDFoundInStockTable(_CNStock.StockID);
                                if (dr == null)
                                {
                                 //   _CNStock.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    Purchase pur = new Purchase();
                                    _CNStock.ScanCode = pur.GetScanGodeForCurrentBatch(_CNStock.ProductID);
                                    returnVal = _CNStock.AddProductDetailsInStockTable();
                                }
                                else
                                {

                                 //   if (ThisStockID != 0)
                                 //       CurrentClosingStock = _CNStock.GetCurrentClosingStock(ThisStockID);
                                    if (ThisStockID == _CNStock.IntStockID)
                                    {
                                        oldTempStock = GetOldStockFromTempGrid(_CNStock.IntStockID);
                                        if (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
                                            returnVal = _CNStock.UpdateIntblStock();
                                        else if ((CurrentClosingStock - oldTempStock + (_CNStock.Quantity + _CNStock.SchemeQuanity)) >= 0)
                                        {
                                            returnVal = _CNStock.UpdateIntblStock();
                                        }
                                        else
                                        {
                                            returnVal = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (ThisStockID != 0)
                                        {
                                            oldTempStock = GetOldStockFromTempGrid(ThisStockID);
                                            returnVal = _CNStock.UpdateProductDetailsInStockTable();
                                        }
                                        else
                                        {

                                            if (ThisStockID == 0)
                                            {
                                                oldTempStock = GetOldStockFromTempGrid(_CNStock.IntStockID);
                                                CurrentClosingStock = _CNStock.GetCurrentClosingStock(_CNStock.StockID);
                                                if ((CurrentClosingStock - oldTempStock) > 0)
                                                {
                                                    Purchase pur = new Purchase();
                                                    _CNStock.ScanCode = pur.GetScanGodeForCurrentBatch(_CNStock.ProductID);
                                                    _CNStock.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                                    returnVal = _CNStock.AddProductDetailsInStockTable();
                                                }
                                                else
                                                    returnVal = false;
                                            }
                                            else
                                                returnVal = false;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                _CNStock.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                returnVal = _CNStock.AddProductDetailsInStockTable();
                            }
                            if (returnVal)
                                returnVal = _CNStock.UpdateCreditNoteStockInMasterProduct();


                            if (returnVal)
                                returnVal = _CNStock.AddProductDetails();
                            //////string ifRecordFound = "";
                            //////ifRecordFound = _CNStock.CheckForBatchMRPInStockTable();
                            //////if (ifRecordFound == "Y")
                            //////{
                            //////    returnVal = _CNStock.UpdateIntblStock();
                            //////}
                            //////else
                            //////{
                            //////    _CNStock.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            //////    returnVal = _CNStock.InsertNewBatchIntblStock();
                            //////}
                            //////returnVal = _CNStock.AddProductDetails();
                            //////returnVal = _CNStock.UpdateCreditNoteStockInMasterProduct();                            
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
        private int GetOldStockFromTempGrid(int stockID)
        {
            int closingstock = 0;
            int tempstockID = 0;
            int qty = 0;
            int repl = 0;
            int scm = 0;
            foreach (DataGridViewRow dr in dgtemp.Rows)
            {
                tempstockID = 0;
                if (dr.Cells["Temp_StockID"].Value != null && dr.Cells["Temp_StockID"].Value.ToString() != "")
                    tempstockID = Convert.ToInt32(dr.Cells["Temp_StockID"].Value.ToString());
                if (tempstockID == stockID)
                {
                    if (dr.Cells["Temp_Quantity"].Value != null && dr.Cells["Temp_Quantity"].Value.ToString() != "")
                        qty = Convert.ToInt32(dr.Cells["Temp_Quantity"].Value.ToString());
                    //   if (dr.Cells["Temp_Scheme"].Value != null && dr.Cells["Temp_Scheme"].Value.ToString() != "")
                    //       scm = Convert.ToInt32(dr.Cells["Temp_Scheme"].Value.ToString());
                    //     if (dr.Cells["Temp_Replacement"].Value != null && dr.Cells["Temp_Replacement"].Value.ToString() != "")
                    //         repl = Convert.ToInt32(dr.Cells["Temp_Replacement"].Value.ToString());
                    closingstock = qty + scm + repl;
                    break;
                }
            }
            return closingstock;
        }
        private bool AddStockIntblStock()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in mpPVC1.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Amount"].Value) > 0 && prodrow.Cells["Col_Code"].Value.ToString().Trim() == "S")
                    {
                        _CNStock.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
                        _CNStock.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        _CNStock.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                        _CNStock.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value);
                        _CNStock.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurRate"].Value);
                        _CNStock.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value);
                        _CNStock.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                        _CNStock.SaleRate = _CNStock.MRP;
                        _CNStock.VATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value);
                        _CNStock.ReasonCode = prodrow.Cells["Col_Code"].Value.ToString();
                        if (prodrow.Cells["Col_StockID"].Value != null)
                            _CNStock.StockID = prodrow.Cells["Col_StockID"].Value.ToString();

                        string ifRecordFound = "";
                     //   ifRecordFound = _CNStock.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "Y")
                        {
                            returnVal = _CNStock.UpdateIntblStock();
                            returnVal = _CNStock.UpdateCreditNoteStockInMasterProduct();
                        }
                        else
                        {
                            _CNStock.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            returnVal = _CNStock.InsertNewBatchIntblStock();
                            returnVal = _CNStock.UpdateCreditNoteStockInMasterProduct();
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

        private bool ReducePreviousStock()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0 && prodrow.Cells["Temp_Code"].Value.ToString().Trim() != "")
                    {
                        _CNStock.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _CNStock.ProductID = Convert.ToInt32(prodrow.Cells["Temp_ProductID"].Value.ToString());
                        _CNStock.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _CNStock.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _CNStock.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _CNStock.ReasonCode = prodrow.Cells["Temp_Code"].Value.ToString();

                        string ifRecordFound = "";
                      //  ifRecordFound = _CNStock.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == _CNStock.StockID)
                            returnVal = _CNStock.UpdateIntblStockReduce();
                        if (returnVal)
                        {
                            returnVal = _CNStock.UpdateCreditNoteStockInMasterProductReduce();
                            deletedproductname = "";
                        }
                        else
                        {
                            deletedproductname = prodrow.Cells["Temp_ProductName"].Value.ToString().Trim() + " " + prodrow.Cells["Temp_UOM"].Value.ToString().Trim() + " " + prodrow.Cells["Temp_Pack"].Value.ToString().Trim();
                            break;
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

        private bool CheckStockForDelete()
        {
            bool retValue = true;

            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0 && prodrow.Cells["Temp_Code"].Value.ToString().Trim() == "S")
                    {
                        _CNStock.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _CNStock.ProductID = Convert.ToInt32(prodrow.Cells["Temp_ProductID"].Value.ToString());
                        _CNStock.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _CNStock.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _CNStock.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        //      _CNStock.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurRate"].Value);
                        //    _CNStock.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _CNStock.ReasonCode = prodrow.Cells["Temp_Code"].Value.ToString();

                        string ifRecordFound = "";
                        ifRecordFound = _CNStock.CheckStockForBatchMRPInStockTable();
                        if (ifRecordFound != "Y")
                        {
                            retValue = false;
                            break;
                        }
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
        #endregion

        #region Construct columns

        private void ConstructMainColumns()
        {
            //DataGridViewTextBoxColumn column;

            try
            {
                DataGridViewTextBoxColumn column;
                mpPVC1.ColumnsMain.Clear();
                //0
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
                column.Width = 190;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ToolTipText = "Press TAB Key To Exit Product Grid";
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
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);


                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "VATPer";
                column.HeaderText = "VAT%";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //7          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.ToolTipText = "Expiry dd/mm";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 75;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 75;
                if (General.CurrentSetting.MsetCreditNoteDoNotShowPurchaseRate == "Y")
                    column.Visible = false;
                else
                    column.Visible = true;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ReturnRate";
                column.DataPropertyName = "ReturnRate";
                column.HeaderText = "RetnRate";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 75;
                if (General.CurrentSetting.MsetCreditNoteReturnRateDisable == "Y")
                    column.ReadOnly = true;
                else
                    column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.HeaderText = "Qty";
                column.Width = 65;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Code";
                column.DataPropertyName = "ReasonCode";
                column.HeaderText = "CD";
                column.Width = 35;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.ToolTipText = "N=Non Saleable, S=Add to Stock";
                mpPVC1.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.ToolTipText = "N=Qty*Pur.Rate,S=Qty*MRP";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                //13            
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //14          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Width = 70;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);


                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                column.Width = 60;
                mpPVC1.ColumnsMain.Add(column);
                //16

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.Visible = false;
                column.ReadOnly = true;
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
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "StockID";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 50;
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
            try
            {
                DataGridViewTextBoxColumn column;
                dgtemp.Columns.Clear();

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
                column.ReadOnly = false;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch No.";
                column.Width = 90;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 50;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATPer";
                column.DataPropertyName = "VATPer";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 70;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_PurRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Purchase Rate";
                column.Width = 70;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_ScmQuantity";
                //column.DataPropertyName = "SchemeQuantity";
                //column.HeaderText = "Scm";
                //column.Width = 50;
                //column.Visible = false;
                ////column.ReadOnly = true;
                //mpPVC1.ColumnsMain.Add(column); 

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Code";
                column.DataPropertyName = "ReasonCode";
                column.HeaderText = "CD";
                column.Width = 40;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 95;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                // temp storage columns 
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
            try
            {
                DataGridViewTextBoxColumn column;
                mpPVC1.ColumnsProductList.Clear();

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfID";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "ShelfID";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.DefaultCellStyle.Format = "n2";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "ClStk";
                column.Width = 55;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructBatchGridColumns()
        {
            try
            {
                DataGridViewTextBoxColumn column;
                mpPVC1.ColumnsBatchList.Clear();

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batchno";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batchno";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 55;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPercent";
                column.DataPropertyName = "ProductVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 55;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                column.Visible = true;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 85;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 85;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (General.CurrentSetting.MsetCreditNoteDoNotShowPurchaseRate == "Y")
                    column.Visible = false;
                else
                    column.Visible = true;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 90;
                column.Visible = false;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "ClStock";
                column.Width = 65;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
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

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 85;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);

                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyShortName";
                column.DataPropertyName = "AccShortName";
                column.HeaderText = "";
                column.Width = 45;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                mpPVC1.ColumnsBatchList.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "TradeRate";
                column.Width = 65;
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);

                //Additional columns needed
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpPVC1.ColumnsBatchList.Add(column);

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

        #endregion

        #region Internal methods

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        private void InitialisempPVC1()
        {
            try
            {
                ConstructMainColumns();
                ConstructProductSelectionListGridColumns();
                ConstructBatchGridColumns();
                if (General.CurrentSetting.MsetRemoveCodeFromCreditNote == "Y")
                {
                    mpPVC1.ColumnsMain["Col_Code"].ReadOnly = true;
                }
                DataTable dtable = new DataTable();
                dtable = _CNStock.ReadProductDetailsByID();
                mpPVC1.DataSourceMain = dtable;

                if (_Mode == OperationMode.Delete)
                    mpPVC1.ColumnsMain["Col_ProductName"].ReadOnly = true;

                //mpPVC1.DataSourceProductList = EcoMartCache.GetProductData();
                dtable = new DataTable();
                Product prod = new Product();
                dtable = prod.GetOverviewData();
                mpPVC1.DataSourceProductList = dtable;
                // mpPVC1.DataSourceProductList = General.ProductList;

                FormatMainGrid();

                mpPVC1.Bind();
                sortcolumn();

                bool ifblankrow = General.CheckForBlankRowInTheGrid(mpPVC1);
                if (!ifblankrow)
                {
                    mpPVC1.Rows.Add();
                }
                if (_Mode == OperationMode.View || _Mode == OperationMode.Delete || _Mode == OperationMode.ReportView)
                {
                    mpPVC1.ColumnsMain[1].ReadOnly = true;
                    mpPVC1.ColumnsMain[2].ReadOnly = true;
                    mpPVC1.ColumnsMain[3].ReadOnly = true;
                    mpPVC1.ColumnsMain[4].ReadOnly = true;
                    mpPVC1.ColumnsMain[5].ReadOnly = true;
                    mpPVC1.ColumnsMain[6].ReadOnly = true;
                    mpPVC1.ColumnsMain[7].ReadOnly = true;
                    mpPVC1.ColumnsMain[8].ReadOnly = true;
                    mpPVC1.ColumnsMain[9].ReadOnly = true;
                    mpPVC1.ColumnsMain[10].ReadOnly = true;
                    mpPVC1.ColumnsMain[11].ReadOnly = true;
                    mpPVC1.ColumnsMain[12].ReadOnly = true;
                    mpPVC1.ColumnsMain[13].ReadOnly = true;
                    mpPVC1.ColumnsMain[14].ReadOnly = true;
                    mpPVC1.ColumnsMain[15].ReadOnly = true;

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FormatMainGrid()
        {
            mpPVC1.NewRowColumnName = "Col_Code";

            mpPVC1.BatchGridShowColumnName = "Col_UOM";
            mpPVC1.NumericColumnNames.Add("Col_Quantity");
            mpPVC1.DoubleColumnNames.Add("Col_VATPer");
            mpPVC1.DoubleColumnNames.Add("Col_MRP");
            mpPVC1.DoubleColumnNames.Add("Col_PurRate");
            mpPVC1.DoubleColumnNames.Add("Col_Amount");
            mpPVC1.DoubleColumnNames.Add("Col_ReturnRate");
        }


        private void BindTempGrid()
        {
            try
            {
                ConstructTempGridColumns();
                DataTable tmptable = new DataTable();
                tmptable = _CNStock.ReadProductDetailsByID();
                _BindingSource = tmptable;
                dgtemp.DataSource = _BindingSource;
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
                lblAdjustedIn.Visible = false;
                txtAdjustedIn.Visible = false;
                mpPVC1.ProductListGridWidth = 695;
                mpPVC1.BatchListGridWidth = 695;
                mpPVC1.ShowBatchWithZeroStock = true;
                cbTransferToAccount.Checked = false;
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                txtNoOfRows.Text = "";
                txtVouchernumber.Clear();
                txtVouType.Text = _CNStock.CrdbVouType;
                txtVoucherSeries.Text = General.ShopDetail.ShopVoucherSeries;
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                datePickerBillDate.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy.ToString());
                datePickerBillDate.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey.ToString());
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtVatInput12point5per.Text = "0.00";
                txtVatInput5per.Text = "0.00";
                txtTotalAmount.Text = "0.00";
                txtRoundAmount.Text = "0.00";
                mcbCreditor.SelectedID = "";
                _CNStock.StockID = "";
                cbRound.Checked = true;
                mcbCreditor.Focus();
                lblFooterMessage.Text = "";
                InitialisempPVC1();
                mpPVC1.ProductListGridWidth = 700;
                mpPVC1.BatchListGridWidth = 690;
                dgvLastPurchase.Visible = false;   //Amar
                if (General.CurrentSetting.MsetCreditNoteDefaultTransferToAccount == "Y")
                    cbTransferToAccount.Checked = true;
                else
                    cbTransferToAccount.Checked = false;

                if (_Mode == OperationMode.Add)
                {
                    mcbCreditor.Enabled = true;
                    txtNarration.Enabled = true;
                    pnlAmounts.Enabled = true;
                    pnlVouTypeNo.Enabled = true;
                    txtVouchernumber.Enabled = false;
                    mcbCreditor.Focus();
                }
                else
                {
                    mcbCreditor.Enabled = false;
                    txtNarration.Enabled = false;
                    pnlAmounts.Enabled = false;
                    pnlVouTypeNo.Enabled = true;
                    txtVouchernumber.Enabled = true;
                    txtVouchernumber.ReadOnly = false;
                    txtVouchernumber.Focus();
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
                mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2" };
                mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "200", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetDebtorCreditorPatientList();
                mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void sortcolumn()
        {
            mpPVC1.ColumnsMain["Col_ProductName"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpPVC1.ColumnsMain["Col_MRP"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpPVC1.ColumnsMain["Col_BatchNumber"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpPVC1.ColumnsMain["Col_Pack"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpPVC1.ColumnsMain["Col_Expiry"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpPVC1.ColumnsMain["Col_UOM"].SortMode = DataGridViewColumnSortMode.Automatic;

        }

        #endregion

        #region Events

        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = ((PSComboBoxNew)sender).SelectedID;
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
                    txtNarration.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpPVC1_OnProductSelected(DataGridViewRow productRow)
        {
            try
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                {
                    proID = 0;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productRow.Cells["Col_ProductID"].Value;
                    proID = Convert.ToInt32(productRow.Cells["Col_ProductID"].Value.ToString());
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = productRow.Cells["Col_ProductName"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value = productRow.Cells["Col_UOM"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Pack"].Value = productRow.Cells["Col_ProdPack"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = productRow.Cells["Col_ProdCompShortName"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = productRow.Cells["Col_VATPer"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = true;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = true;
                    mpPVC1.RefreshMe();
                    //  lblMessage.Text = "Enter Loose Quantity";
                    //Amar
                    if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView && mpPVC1.MainDataGridCurrentRow.Cells[0].Value != null)
                    {
                        dgvLastPurchase.Visible = true;
                        dgvLastPurchase.Location = GetdgvLastPurchaseLocation();
                        dgvLastPurchase.BringToFront();
                    }
                    if (mpPVC1.MainDataGridCurrentRow.Cells[0].Value != null)
                        FillLastPurchase();
                    mpPVC1.SetFocus(4);           //focus to Batch

                    FillLastSaleDataFromMasterProduct();  //amar
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                    // double mmargin = 0;
                    int mqty = 0;
                    int mscm = 0;
                    DataGridViewRow currentdr = dgvLastPurchase.Rows[_RowIndex];
                    currentdr.Cells["Col_ID"].Value = dr["DetailSaleID"].ToString();
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
                    if (dr["SaleRate"] != DBNull.Value)
                        double.TryParse(dr["SaleRate"].ToString(), out amtclear);
                    currentdr.Cells["Col_SaleRate"].Value = amtclear.ToString("#0.00");

                    amtclear = 0;
                    if (dr["CashDiscountAmount"] != DBNull.Value)
                        double.TryParse(dr["CashDiscountAmount"].ToString(), out amtclear);
                    currentdr.Cells["Col_CashDiscount"].Value = amtclear.ToString("#0.00");

                    amtclear = 0;
                    if (dr["VoucherNumber"] != DBNull.Value)
                        double.TryParse(dr["VoucherNumber"].ToString(), out amtclear);
                    currentdr.Cells["Col_VoucherNo"].Value = amtclear.ToString();

                    mqty = 0;
                    mscm = 0;
                    if (dr["Quantity"] != DBNull.Value)
                        int.TryParse(dr["Quantity"].ToString(), out mqty);
                    currentdr.Cells["Col_Qty"].Value = mqty.ToString("#0");

                    if (dr["SchemeQuantity"] != DBNull.Value)
                        int.TryParse(dr["SchemeQuantity"].ToString(), out mscm);
                    string scm = mqty.ToString() + "+" + mscm.ToString();
                    currentdr.Cells["Col_SchemeQty"].Value = scm;

                    //if (dr["MarginAfterDiscount"] != DBNull.Value)
                    //    double.TryParse(dr["MarginAfterDiscount"].ToString(), out mmargin);
                    //currentdr.Cells["Col_Margin"].Value = mmargin.ToString("#0.00");
                    //currentdr.Cells["Col_PartyName"].Value = dr["AccName"].ToString();
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

        private DataTable FillLastPurchase()  //Amar
        {
            bool retValue = false;
            DataTable dt = new DataTable();
            try
            {
                ConstructLastPurchaseColumns();
                //dt = _SSSale.GetOverviewDataForLastPurchase(_SSSale.ProductID);
                dt = _CNStock.GetOverviewDataForLastPurchase(proID, mcbCreditor.SelectedID.Trim()); //Add account ID
                if (dt != null && dt.Rows.Count > 0)
                    retValue = BindLastPurchase(dt);

            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.FillCreditDebitNote>>" + Ex.Message);
            }
            return dt;
        }
        private DataTable CreatePatientSaleDt()
        {
            dtTempPatientSale = CacheObject.Get<DataTable>("TempCounterSale");
            List<DataRow> rowsToDelete = new List<DataRow>();

            if (dtTempPatientSale == null)
            {
                dtTempPatientSale = new DataTable();
                dtTempPatientSale.Columns.Add("ProductID", typeof(string));
                dtTempPatientSale.Columns.Add("BatchID", typeof(string));
                dtTempPatientSale.Columns.Add("QTY", typeof(int));
                dtTempPatientSale.Columns.Add("SRate", typeof(double));
                dtTempPatientSale.Columns.Add("FormName", typeof(string));
                dtTempPatientSale.Columns.Add("StockID", typeof(string));
                //ProductID, batch,mrp
                CacheObject.Add(dtTempPatientSale, "TempCounterSale");
                //DataRow[] drFormRows = dtTempPatientSale.Select("FormName");

            }
            foreach (DataRow item in dtTempPatientSale.Rows)
            {
                if (string.Equals(item["FormName"], this.Name))
                {
                    rowsToDelete.Add(item);
                }
            }
            foreach (DataRow row in rowsToDelete)
            {
                dtTempPatientSale.Rows.Remove(row);
            }
            return dtTempPatientSale;

        }

        private Point GetdgvLastPurchaseLocation()
        {
            Point pt = new Point();
            try
            {
                pt.X = Location.X + 400;
                pt.Y = Location.Y + 10;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
        }

        private bool FillLastSaleDataFromMasterProduct()
        {
            DataRow dr = null;
            DataRow invdr = null;
            string mprodno = "";
            string mshelf = "";
            int mprodclosingstock = 0;

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
                if (dr["ProdLastSaleStockID"] != null && dr["ProdLastSaleStockID"].ToString() != "")
                    mlastsalestockid = dr["ProdLastSaleStockID"].ToString();
                if (dr["ProdClosingStock"] != DBNull.Value && dr["ProdClosingStock"].ToString() != string.Empty)
                    mprodclosingstock = Convert.ToInt32(dr["ProdClosingStock"].ToString().Trim());
                mshelf = dr["ShelfCode"].ToString().Trim();

                if (mlastsalestockid != "")
                {
                    SsStock invss = new SsStock();
                    invdr = invss.GetStockByStockID(mlastsalestockid);
                }

                if (invdr != null)
                {
                    int.TryParse(invdr["ClosingStock"].ToString().Trim(), out mclosingstock);

                    if (mclosingstock > 0 || (mclosingstock == 0 && General.CurrentSetting.MsetSaleAllowNegativeStock == "Y"))
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
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }


        private void mpPVC1_OnBatchSelected(DataGridViewRow batchRow)
        {
            string mexpirydate = "";
            string mexpiry = "";
            string mprodno = "";
          //  string mbatchno = "";
         //   double mmrpn = 0;
            try
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                {
                    string newexpirydate = "";
                    string newexpiry = "";
                    double mmrp = 0;
                    double mprate = 0;
                    string mlastsalestockid = "";
                    mprodno = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                    mexpiry = batchRow.Cells["Col_Expiry"].Value.ToString().Trim();
                    mexpirydate = batchRow.Cells["Col_ExpiryDate"].Value.ToString().Trim();

                    mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = batchRow.Cells["Col_Batchno"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = batchRow.Cells["Col_Expiry"].Value;
                    if (batchRow.Cells["Col_MRP"].Value != null)
                        double.TryParse(batchRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value = mmrp;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value = mmrp;
                    if (batchRow.Cells["Col_PurchaseRate"].Value != null)
                        double.TryParse(batchRow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value = batchRow.Cells["Col_PurchaseRate"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value = batchRow.Cells["Col_StockID"].Value;
                    newexpiry = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString();

                    newexpirydate = General.GetValidExpiryDate(newexpiry);
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = false;
                    lblFooterMessage.Text = "Enter Return Rate";
                    string mdt = DateTime.Today.Date.ToString("yyyyMMdd");

                    //Amar start
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

                        // _SSSale.IFMultipleMRP = _SSSale.IfmultipleMRP(mprodno, mbatchno, mmrpn);
                        lblFooterMessage.Text = "";
                        int currentrow = mpPVC1.MainDataGridCurrentRow.Index;
                        int totbatchsale = 0;
                        int totproductsale = 0;
                        int saleqty = 0;
                        int rowmqty = 0;
                        foreach (DataGridViewRow dr in mpPVC1.Rows)
                        {
                            if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                            {
                                if (dr.Index != currentrow)
                                {
                                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                                        int.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out rowmqty);
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
                        //Amar End

                        mpPVC1.SetFocus(9); //focus to Quantity
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1_OnNewBatchClicked()
        {
            try
            {
                mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = false;

                //Enable Columns

                mpPVC1.SetFocus(4); //focus to Batch
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }



        private void mpPVC1_OnCellValueChangeCommited(int colIndex)
        {
            lblFooterMessage.Text = "";
            string mexpirydate = "";
            try
            {
                if (colIndex == 9)
                {
                    if (General.CurrentSetting.MsetRemoveCodeFromCreditNote == "Y")
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value = "S";
                    lblFooterMessage.Text = "Enter Loose Quantity";
                    if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) <= 0)
                    {
                        lblFooterMessage.Text = "Enter Loose Quantity";
                        // mpPVC1.SetFocus(10);
                    }
                    else
                    {

                        // if Old Quantity > 0 
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString() != "")
                        {
                            // if closing stock + current quantity > old Quantity

                            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value != null)
                            {
                                //double clstk = mpPVC1.MainDataGridCurrentRow.Cells[12].Value;
                                //double curstk = mpPVC1.MainDataGridCurrentRow.Cells[9].Value;

                                if ((Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value.ToString()) + Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString())) >= Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString()))
                                {
                                    lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";
                                    mpPVC1.SetFocus(11);
                                }
                                else
                                {
                                    lblFooterMessage.Text = "Not Enough Stock for EDIT...";
                                    mpPVC1.SetFocus(10);
                                }

                            }
                            else
                                if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value == null || mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == "")
                                mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value = "N";
                            lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";
                        }
                        else
                            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value == null || mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == "")
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value = "N";
                        lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";
                    }


                    mpPVC1.IsAllowNewRow = false;

                }
                if (colIndex == 10)
                {
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString().Trim() != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString().Trim() != "" && Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) > 0)
                    {
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() != null)
                        {
                            CalculateRowAmount();
                            mpPVC1.IsAllowNewRow = false;
                        }
                        else
                            lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";
                    }
                    else
                        lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";

                    mpPVC1.IsAllowNewRow = false;
                }
                if (colIndex == 11)
                {

                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == "N" || mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == "S")
                    {
                        if (General.CurrentSetting.MsetRemoveCodeFromCreditNote == "Y")
                            mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value = "S";
                        if (Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) == 0)
                        {
                            lblFooterMessage.Text = "Enter Quantity";
                            mpPVC1.SetFocus(10);

                        }
                        else if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value) == 0.00)
                        {
                            lblFooterMessage.Text = "Enter Purchase Rate";
                            mpPVC1.SetFocus(8);

                        }
                        else if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value) == 0.00)
                        {
                            lblFooterMessage.Text = "Enter ReturnRate Rate";
                            mpPVC1.SetFocus(9);

                        }
                        else if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value) == 0.00)
                        {
                            lblFooterMessage.Text = "Enter MRP";
                            mpPVC1.SetFocus(7);

                        }
                        else if (mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value == null || mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim() == "")
                        {
                            lblFooterMessage.Text = "Enter Batch Number";
                            mpPVC1.SetFocus(5);

                        }
                        else
                        {
                            string ifproductexists = "N";
                            int currowindex = 0;
                            string curProductID = mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                            string curbatch = mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString();
                            double curmrp = 0;
                            double.TryParse(mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString(), out curmrp);
                            string curcode = mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString();

                            try
                            {

                                foreach (DataGridViewRow dr in mpPVC1.Rows)
                                {
                                    if (dr.Index != mpPVC1.MainDataGridCurrentRow.Index)
                                    {
                                        if (dr.Cells["Col_ProductID"].Value != null)
                                        {
                                            string drProductID = dr.Cells["Col_ProductID"].Value.ToString();
                                            string drbatch = dr.Cells["Col_BatchNumber"].Value.ToString();
                                            double drmrp = 0;

                                            //Amar
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
                                            //End Amar

                                            double.TryParse(dr.Cells["Col_MRP"].Value.ToString(), out drmrp);
                                            string drcode = dr.Cells["Col_Code"].Value.ToString();

                                            if (curProductID == drProductID && curbatch == drbatch && curmrp == drmrp && curcode == drcode)
                                            {
                                                currowindex = dr.Index;
                                                ifproductexists = "Y";
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                Log.WriteException(Ex);
                            }

                            if (ifproductexists == "Y")
                            {
                                lblFooterMessage.Text = "Batch Already Exist";
                            }
                            else
                            {
                                CalculateRowAmount();
                                mpPVC1.IsAllowNewRow = true;
                                lblFooterMessage.Text = "";
                            }

                        }
                    }
                    else
                    {
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value = "S";

                        lblFooterMessage.Text = "[N] Non Saleable  [S] Salebable ";
                        mpPVC1.IsAllowNewRow = false;
                        mpPVC1.SetFocus(11);
                    }
                }

                if (colIndex == 6)  // Expiry
                {
                    string newexpiry = "";
                    string newexpirydate = "";
                    int explength = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim().Length;
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

                                mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                                lblFooterMessage.Text = "";
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
                if (colIndex == 9)
                {
                    lblFooterMessage.Text = "Enter Loose Quantity";
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value == null || mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value.ToString() == "")
                    {

                        mpPVC1.SetFocus(9);
                    }
                    else
                    {
                        double mreturnrate = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value.ToString());
                        if (mreturnrate <= 0)
                        {
                            lblFooterMessage.Text = "Enter Return Rate";
                            mpPVC1.SetFocus(9);
                        }
                    }


                }

                if (colIndex == 8)  // Expiry
                {
                    string newexpiry = "";
                    string newexpirydate = "";
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null)
                    {
                        int explength = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim().Length;
                        if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim() != "" && explength > 0)
                        {
                            newexpiry = General.GetValidExpiry(mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString());
                            if (newexpiry != "")
                            {
                                bool ifexp = CheckValidExpiry(newexpiry);

                                newexpirydate = General.GetValidExpiryDate(newexpiry.ToString());
                                newexpirydate = General.GetExpiryInyyyymmddForm(newexpirydate);
                                string mdt = DateTime.Today.Date.ToString("yyyyMMdd");

                                if (newexpirydate != "" && Convert.ToInt32(newexpirydate) < Convert.ToInt32(mdt))
                                {
                                    lblFooterMessage.Text = "Expired Product..";
                                    ifexp = false;
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = true;
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = true;
                                }
                                else
                                {
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_SaleRate"].ReadOnly = false;
                                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                                    if (ifexp)
                                    {
                                        mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry.ToString();
                                        newexpirydate = General.GetValidExpiryDate(newexpiry);
                                        newexpirydate = General.GetExpiryInyyyymmddForm(newexpirydate);
                                        mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                                        lblFooterMessage.Text = "";
                                        lblRightSideFooterMsg.Text = "";
                                        //txtMinlevel.Text = "";
                                        //txtMaxlevel.Text = "";
                                    }
                                    else
                                    {
                                        lblFooterMessage.Text = "Check Expiry ";
                                        mpPVC1.SetFocus(5);
                                    }
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

                CalculateAmount(-1);
                dgvLastPurchase.Visible = false;   //Amar
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateRowAmount()
        {
            double mqty = 0;
            double mamt = 0;
            double mpakn = 0;
            double mprate = 0;
            double mmrp = 0;
            double mreturnrate = 0;
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value != null)
                mpakn = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                mqty = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value != null)
                mprate = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value != null)
                mreturnrate = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                mmrp = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
            //   if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == "N")
            mamt = Math.Round(mqty * (mreturnrate / mpakn), 2);
            //   mamount = Math.Round(Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) * Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[8].Value), 2);
            //  else
            //       mamt = mqty * (mmrp / mpakn);
            //mamount = Math.Round(Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) * Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[7].Value), 2);

            mpPVC1.MainDataGridCurrentRow.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
            CalculateAmount(-1);
        }

        private void NumberofRows()
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
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateAmount(int deletedrowindex)
        {
            double TotalAmount = 0;
            double VatAmount5 = 0;
            double VatAmount12Point5 = 0;
            int itemCount = 0;

            //loop to calculate purchase amount by given customer id
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Index != deletedrowindex)
                    {
                        if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                        {
                            double mamt = double.Parse(dr.Cells["Col_Amount"].Value.ToString());
                            double mvatper = double.Parse(dr.Cells["Col_VATPer"].Value.ToString());
                            // vat 5.5
                            if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 6)

                                VatAmount5 += Math.Round((mamt * mvatper) / (100 + mvatper), 4);
                            else
                            {
                                if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 13.50)
                                    VatAmount12Point5 += Math.Round((mamt * mvatper) / (100 + mvatper), 4);
                            }
                            TotalAmount += double.Parse(dr.Cells["Col_Amount"].Value.ToString());
                            itemCount += 1;
                        }
                    }

                }

                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtVatInput12point5per.Text = Math.Round(VatAmount12Point5, 2).ToString("#0.00");
                txtVatInput5per.Text = Math.Round(VatAmount5, 2).ToString("#0.00");
                txtTotalAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");
                txtAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");

                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateAllAmounts()
        {
            double mdblAmount;
            double.TryParse(txtTotalAmount.Text, out mdblAmount);
            double mdblVatInput12point5per;
            double.TryParse(txtVatInput12point5per.Text, out mdblVatInput12point5per);
            double mdblVatInput5per;
            double.TryParse(txtVatInput5per.Text, out mdblVatInput5per);
            double mdblDiscPer;
            double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
            double mdblDiscAmount;
            double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);


            try
            {
                if (mdblAmount < mdblDiscAmount)
                {
                    txtDiscAmount.Text = "0.00";
                    txtDiscPercent.Text = "0.00";
                    double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);
                    double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
                }

                txtAmount.Text = Math.Round(mdblAmount - mdblDiscAmount, 2).ToString("#0.00");

                if (cbRound.Checked == true)
                {
                    txtRoundAmount.Text = Math.Round(Math.Round(Convert.ToDouble(txtAmount.Text), 0) - Math.Round(Convert.ToDouble(txtAmount.Text), 2), 2).ToString("#0.00");
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(txtRoundAmount.Text)), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                }
                else
                {
                    txtRoundAmount.Text = "0.00";
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(txtRoundAmount.Text)), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        public void CalculateDiscount()
        {

            double mdblAmount;
            double.TryParse(txtTotalAmount.Text, out mdblAmount);
            double mdblDiscPer;
            double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
            double mdblDiscAmount;
            double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);


            try
            {
                if (mdblDiscPer > 0)
                {
                    mdblDiscAmount = Math.Round(((mdblAmount) * mdblDiscPer / 100), 2);
                    txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                }

                if (mdblAmount < mdblDiscAmount)
                {
                    txtDiscAmount.Text = "0.00";
                    txtDiscPercent.Text = "0.00";
                    double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);
                    double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
                }
                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    CalculateDiscount();
                    txtDiscAmount.Focus();
                    break;
                case Keys.Down:
                    txtDiscAmount.Focus();
                    break;
            }
        }
        private void txtDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Calculatediscountpercent();
                    CalculateAllAmounts();
                    txtAmount.Focus();
                    // cbRound.Focus();
                    break;
            }
        }
        private void Calculatediscountpercent()
        {
            double mdblAmount;
            double.TryParse(txtTotalAmount.Text, out mdblAmount);
            double mdblVatInput12point5per;
            double.TryParse(txtVatInput12point5per.Text, out mdblVatInput12point5per);
            double mdblVatInput5per;
            double.TryParse(txtVatInput5per.Text, out mdblVatInput5per);
            double mdblDiscPer;
            double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
            double mdblDiscAmount;
            double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);
            if (mdblDiscAmount > 0)
            {
                mdblDiscPer = Math.Round((mdblDiscAmount * 100) / mdblAmount, 4);
                txtDiscPercent.Text = mdblDiscPer.ToString("#0.00");
            }
        }
        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        txtDiscAmount.Focus();
                        break;
                    case Keys.Enter:
                        CalculateAllAmounts();
                        cbRound.Focus();
                        break;
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
            {
                mpPVC1.SetFocus(1);
            }
        }
        private void mpPVC1_OnRowDeleted(object sender, EventArgs e)
        {
            mpPVC1OnRowDeleted(sender);
        }
        private void mpPVC1OnRowDeleted(object sender)
        {
            try
            {
                DataGridViewRow deletedrow = (DataGridViewRow)sender;
                int deletedrowindex = deletedrow.Index;
                CalculateAmount(deletedrowindex);
                dgvLastPurchase.Visible = false;   //Amar
                lblFooterMessage.Text = "";
                if (!General.CheckForBlankRowInTheGrid(mpPVC1))
                    mpPVC1.Rows.Add();
                mpPVC1.SetFocus(1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            txtDiscPercent.Focus();
        }

        private void mpPVC1_OnShiftTABKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }



        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            int vouno = 0;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (txtVouchernumber.Text != null)
                        {
                            int.TryParse(txtVouchernumber.Text.ToString(), out vouno);
                            _CNStock.ReadDetailsByVouNumber(_CNStock.CrdbVouType, vouno);
                            FillSearchData(_CNStock.Id, "");
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
        }

        #endregion

        #region tooltip
        private void AddToolTip()
        {
            ttcns.SetToolTip(txtNarration, "Enter Narration");
            ttcns.SetToolTip(txtAmount, "Total Amount of All Rows");
            ttcns.SetToolTip(txtTotalAmount, "Amount+Vat Amounts - Discount");
            ttcns.SetToolTip(cbTransferToAccount, "Voucher Transfered to Accounts and not available to adjust in any bill");

        }
        #endregion

        #region TimerDate

        private void SetDateStatus()
        {
            DateTimer.Interval = 1000;

            DateTime _MDate = datePickerBillDate.Value.Date;
            DateTime _CDate = DateTime.Now.Date;
            int result = DateTime.Compare(_MDate, _CDate);
            if (result < 0)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You are working in Previous date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                DateTimer.Enabled = true;
                DateTimer.Start();
            }
            else if (result == 0)
            {
                lblmsg.Visible = false;
                lblmsg.Text = ""; // "You are working in Current date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                DateTimer.Enabled = false;
                DateTimer.Stop();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "You are working in Next date";
                lblmsg.ForeColor = Color.Black;
                lblmsg.BackColor = Color.Gainsboro;
                DateTimer.Enabled = true;
                DateTimer.Start();
            }
        }
        void DateTimer_Tick(object sender, EventArgs e)
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
        private void datePickerBillDate_ValueChanged_1(object sender, EventArgs e)
        {
            SetDateStatus();
        }
        private void datePickerBillDate_ValueChanged_2(object sender, EventArgs e)
        {
            SetDateStatus();
        }
        #endregion TimerDate        
    }
}