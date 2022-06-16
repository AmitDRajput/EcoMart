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

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDistributorCreditNoteStock : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private CreditNoteStock _CNStock;
        private string deletedproductname = "";
        #endregion

        #region constructor
        public UclDistributorCreditNoteStock()
        {
            try
            {
                InitializeComponent();
                _CNStock = new CreditNoteStock();
                SearchControl = new UclDistributorCreditNoteStockSearch();
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
                headerLabel1.Text = "DISTRIBUTOR CREDIT NOTE STOCK -> NEW";
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
                headerLabel1.Text = "DISTRIBUTOR CREDIT NOTE STOCK -> EDIT";
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
            pnlGST.Visible = false;
            pnlIGST.Visible = false;
            return retValue;
        }
        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "DISTRIBUTOR CREDIT NOTE STOCK -> DELETE";
                FillPartyCombo();
                ClearData();
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
                            //  UpdateClosingStockinCache();
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
                headerLabel1.Text = "DISTRIBUTOR CREDIT NOTE STOCK -> VIEW";
                FillPartyCombo();
                txtVouchernumber.Focus();
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
            printer.Print(_CNStock.CrdbVouTypeDistributor, _CNStock.CrdbVouNo.ToString(), _CNStock.CrdbVouDate, "", _CNStock.CrdbAddress, "", "", "", mpPVC1.Rows, _CNStock.CrdbNarration, _CNStock.CrdbAmountNet, "", _CNStock.CrdbDiscAmt, 0, 0, _CNStock.CrdbAmount, 0 + _CNStock.CrdbAmountNet, "");

        }

        private void PrintCreditnNotePlainPaper()
        {
            EcoMart.Printing.PlainPaperPrinter printer = new EcoMart.Printing.PlainPaperPrinter();
            printer.Print(_CNStock.CrdbVouTypeDistributor, _CNStock.CrdbVouNo.ToString(), _CNStock.CrdbVouDate, _CNStock.CrdbName, _CNStock.CrdbAddress, "", "", "", mpPVC1.Rows, _CNStock.CrdbNarration, _CNStock.CrdbAmountNet, "", _CNStock.CrdbDiscAmt, 0, 0, _CNStock.CrdbAmount, 0 + _CNStock.CrdbAmountNet, "");

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
                //  double mdiscamount = 0;              
                //   double mdiscper = 0;
                double mbillamount = 0;
                double mamount = 0;
                double mround = 0;
                System.Text.StringBuilder _errorMessage;
                try
                {
                    if (mcbCreditor.SelectedID != null)
                    {

                        _CNStock.CrdbId = mcbCreditor.SelectedID.Trim();
                        _CNStock.CrdbName = mcbCreditor.SeletedItem.ItemData[2];
                        if (mcbCreditor.SeletedItem.ItemData[3] != null)
                            _CNStock.CrdbAddress = mcbCreditor.SeletedItem.ItemData[3].ToString();
                        if (mcbCreditor.SeletedItem.ItemData[4] != null)
                            _CNStock.CrdbAddress = _CNStock.CrdbAddress.Trim() + " " + mcbCreditor.SeletedItem.ItemData[4].ToString();
                    }

                    _CNStock.CrdbNarration = txtNarration.Text.Trim();
                    _CNStock.CrdbVouTypeDistributor = FixAccounts.VoucherTypeForCreditNoteStock;
                    if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                        _CNStock.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                    _CNStock.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                    //     double.TryParse(txtDiscPercent.Text, out mdiscper);
                    //    _CNStock.CrdbDiscPer = mdiscper;
                    //     double.TryParse(txtDiscAmount.Text, out mdiscamount);
                    //     _CNStock.CrdbDiscAmt = mdiscamount;
                    double.TryParse(txtBillAmount.Text, out mbillamount);
                    _CNStock.CrdbAmountNet = mbillamount;
                    double.TryParse(txtAmount.Text, out mamount);
                    _CNStock.CrdbAmount = mamount;
                    double.TryParse(txtRoundAmount.Text, out mround);
                    _CNStock.CrdbRoundAmount = mround;
                    if (cbTransferToAccount.Checked == true)
                        _CNStock.TrasferToAccount = "Y";
                    else
                        _CNStock.TrasferToAccount = "N";
                    if (_CNStock.TrasferToAccount == "Y")
                        _CNStock.CrdbAmountClear = mbillamount;
                    else
                        _CNStock.CrdbAmountClear = 0;
                    if (_Mode == OperationMode.Edit)
                        _CNStock.IFEdit = "Y";
                    _CNStock.Validate();

                    if (_CNStock.IsValid)
                    {
                        _CNStock.CrdbVouType = FixAccounts.VoucherTypeForCreditNoteStock;
                        LockTable.LockTablesForCreditDebitNoteStock();
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();
                            //   _CNStock.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CNStock.CrdbVouNo = _CNStock.GetAndUpdateCNNumber(General.ShopDetail.ShopVoucherSeries);
                            txtVouchernumber.Text = Convert.ToString(_CNStock.CrdbVouNo);
                            _CNStock.CreatedBy = General.CurrentUser.Id;
                            _CNStock.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                            _CNStock.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                            _CNStock.IntID = _CNStock.AddDetails();
                            _CNStock.Id = _CNStock.IntID.ToString();
                            //   _SavedID = _CNStock.Id;
                            if (_CNStock.IntID > 0)
                                retValue = true;
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
                                string msgLine2 = _CNStock.CrdbVouTypeDistributor + "  " + _CNStock.CrdbVouNo.ToString("#0");
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
                                        //  UpdateClosingStockinCache();
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

                    if (_CNStock.CrdbDiscPer >= 0)
                        txtDiscPercent.Text = _CNStock.CrdbDiscPer.ToString("#0.00");
                    if (_CNStock.CrdbDiscAmt > 0)
                        txtDiscAmount.Text = _CNStock.CrdbDiscAmt.ToString("#0.00");

                    txtBillAmount.Text = _CNStock.CrdbAmountNet.ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                    txtAmount.Text = _CNStock.CrdbAmount.ToString("#0.00");
                    if (_CNStock.CrdbRoundAmount != 0)
                        txtRoundAmount.Text = _CNStock.CrdbRoundAmount.ToString("#0.00");
                    double mtotgst = _CNStock.GSTS5 + _CNStock.GSTS12 + _CNStock.GSTS18 + _CNStock.GSTS28 +
                              _CNStock.GSTC5 + _CNStock.GSTC12 + _CNStock.GSTC18 + _CNStock.GSTC28 + _CNStock.GSTI5 + _CNStock.GSTI12 + _CNStock.GSTI18 + _CNStock.GSTI28;

                    double mtotgstamt = _CNStock.GSTAmt0 + _CNStock.GSTAmtS5 + _CNStock.GSTAmtS12 + _CNStock.GSTAmtS18 + _CNStock.GSTAmtS28 +
                                                  _CNStock.GSTAmtC5 + _CNStock.GSTAmtC12 + _CNStock.GSTAmtC18 + _CNStock.GSTAmtC28 + _CNStock.GSTAmtI5 +
                                                  _CNStock.GSTAmtI12 + _CNStock.GSTAmtI18 + _CNStock.GSTAmtI18 + _CNStock.GSTI28;
                    txtTotalAmount.Text = Math.Round(mtotgstamt + mtotgst, 2).ToString("#0.00");
                    if (DateTime.TryParse(_CNStock.CrdbVouDate, out DateTime mydate))
                    {
                        datePickerBillDate.Value = mydate;
                    }
                    pnlGST.Visible = false;
                    pnlIGST.Visible = false;
                    FillGSTpnl();
                    mpPVC1.SetFocus(1);
                    if (_CNStock.ClearVouNo > 0 && _Mode == OperationMode.Edit)
                    {
                        lblFooterMessage.Text = "DISTRIBUTOR Credit Note Cleared in Purchase Voucher:" + _CNStock.ClearVouType + " " + _CNStock.ClearVouNo.ToString();
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
                    pnlGST.Visible = false;
                    pnlIGST.Visible = false;
                    mcbCreditor.Focus();
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
                            if (CurrentClosingStock < ((_CNStock.Quantity + _CNStock.SchemeQuanity) * _CNStock.ProdLoosePack))
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
            //DataTable dtable = new DataTable();
            ////dtable = General.ProductList;
            //Product prod = new Product();
            //dtable = prod.GetOverviewData();
            //mpPVC1.DataSourceProductList = dtable;
            //mpPVC1.BindGridProductList();
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
                if (pnlGST.Visible == true)
                {
                    pnlGST.Visible = false;
                    mpPVC1.SetFocus(1);
                    retValue = true;
                }
                else if (mpPVC1.VisibleProductGrid() == true) //kiran
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
                string ThisStockID = "";
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
                            //   _CNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CNStock.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                            _CNStock.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
                            _CNStock.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            if (prodrow.Cells["Col_UOM"].Value != null)
                                _CNStock.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Col_UOM"].Value.ToString());
                            _CNStock.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                            _CNStock.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurRate"].Value.ToString());
                            //  _CNStock.ReturnRate = Convert.ToDouble(prodrow.Cells["Col_ReturnRate"].Value);
                            _CNStock.TradeRate = _CNStock.PurchaseRate;
                            _CNStock.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                            if (prodrow.Cells["Col_SaleRate"].Value != null && prodrow.Cells["Col_SaleRate"].Value.ToString() != string.Empty)
                                _CNStock.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                            _CNStock.DiscountPercent = 0;
                            _CNStock.GSTPurchaseAmountZero = 0;
                            _CNStock.GSTSAmount = 0;
                            _CNStock.GSTCAmount = 0;
                            _CNStock.GSTSPurchaseAmount = 0;
                            _CNStock.GSTCPurchaseAmount = 0;
                            if (prodrow.Cells["Col_DiscountPercent"].Value.ToString() != string.Empty)
                                _CNStock.DiscountPercent = Convert.ToDouble(prodrow.Cells["Col_DiscountPercent"].Value.ToString());
                            _CNStock.DiscountAmount = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                            if (_CNStock.SaleRate == 0)
                                _CNStock.SaleRate = _CNStock.MRP;
                            _CNStock.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            //  _CNStock.Expiry = General.GetValidExpiry(prodrow.Cells["Col_Expiry"].Value.ToString()); 
                            if (_CNStock.Expiry != "00/00")
                                mexpdate = General.GetValidExpiryDate(_CNStock.Expiry);
                            if (mexpdate != "")
                                _CNStock.ExpiryDate = General.GetExpiryInyyyymmddForm(mexpdate);
                            if (prodrow.Cells["Col_GSTAmountZero"].Value != null && prodrow.Cells["Col_GSTAmountZero"].Value.ToString() != string.Empty)
                                _CNStock.GSTPurchaseAmountZero = Convert.ToDouble(prodrow.Cells["Col_GSTAmountZero"].Value.ToString());
                            if (prodrow.Cells["Col_GSTSAmount"].Value != null && prodrow.Cells["Col_GSTSAmount"].Value.ToString() != string.Empty)
                                _CNStock.GSTSPurchaseAmount = Convert.ToDouble(prodrow.Cells["Col_GSTSAmount"].Value.ToString());
                            if (prodrow.Cells["Col_GSTCAmount"].Value != null && prodrow.Cells["Col_GSTCAmount"].Value.ToString() != string.Empty)
                                _CNStock.GSTCPurchaseAmount = Convert.ToDouble(prodrow.Cells["Col_GSTCAmount"].Value.ToString());
                            if (prodrow.Cells["Col_GSTS"].Value != null && prodrow.Cells["Col_GSTS"].Value.ToString() != string.Empty)
                                _CNStock.GSTSAmount = Convert.ToDouble(prodrow.Cells["Col_GSTS"].Value.ToString());
                            if (prodrow.Cells["Col_GSTC"].Value != null && prodrow.Cells["Col_GSTC"].Value.ToString() != string.Empty)
                                _CNStock.GSTCAmount = Convert.ToDouble(prodrow.Cells["Col_GSTC"].Value.ToString());
                            _CNStock.VATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value);
                            _CNStock.ReasonCode = prodrow.Cells["Col_Code"].Value.ToString();

                            if (prodrow.Cells["Col_StockID"].Value != null)
                                _CNStock.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                            _CNStock.Amount = Convert.ToDouble(prodrow.Cells["Col_Amount"].Value);


                            if (_CNStock.StockID != null && _CNStock.StockID != "")
                            {

                                dr = _CNStock.IfStockIDFoundInStockTable(_CNStock.StockID);
                                if (dr == null)
                                {
                                    //  _CNStock.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _CNStock.StockID = _CNStock.AddProductDetailsInStockTableForDistributor();
                                    if (_CNStock.StockID != "")
                                        returnVal = true;
                                }
                                else
                                {
                                    // check ss 18-10
                                    ThisStockID = _CNStock.CheckForBatchMRPInStockTable();
                                    if (ThisStockID != "")
                                        CurrentClosingStock = _CNStock.GetCurrentClosingStock(ThisStockID);
                                    if (ThisStockID == _CNStock.StockID)
                                    {
                                        oldTempStock = GetOldStockFromTempGrid(_CNStock.StockID);
                                        //if (General.CurrentSetting.MsetSaleAllowNegativeStock == "Y")
                                        //   returnVal = _CNStock.UpdateIntblStockForDistributor();
                                        if ((CurrentClosingStock - (oldTempStock * _CNStock.ProdLoosePack) + ((_CNStock.Quantity + _CNStock.SchemeQuanity))) >= 0)
                                        {
                                            returnVal = _CNStock.UpdateIntblStockForDistributor();
                                        }
                                        else
                                        {
                                            returnVal = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        ////check ss 18-10
                                        if (ThisStockID != "")
                                        {
                                            oldTempStock = GetOldStockFromTempGrid(ThisStockID);
                                            returnVal = _CNStock.UpdateProductDetailsInStockTable();
                                        }
                                        else
                                        {

                                            if (ThisStockID == "") // && _CNStock.IntStockID != 0)
                                            {
                                                oldTempStock = GetOldStockFromTempGrid(_CNStock.StockID);
                                                CurrentClosingStock = _CNStock.GetCurrentClosingStock(_CNStock.StockID);
                                                if ((CurrentClosingStock - oldTempStock) > 0)
                                                {

                                                    //  _CNStock.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                                    _CNStock.StockID = _CNStock.AddProductDetailsInStockTableForDistributor();
                                                    if (_CNStock.StockID != "")
                                                        returnVal = true;
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
                                //   _CNStock.StockID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                _CNStock.AccountID = mcbCreditor.SelectedID;
                                _CNStock.StockID = _CNStock.AddProductDetailsInStockTableForDistributor();
                                if (_CNStock.StockID != "")
                                    returnVal = true;
                            }
                            if (returnVal)
                                returnVal = _CNStock.UpdateCreditNoteStockInMasterProductForDistributor();


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
        private Point GetpnlGSTLocation()
        {
            Point pt = new Point();
            //  int difference = pnlSummary.Size.Height - mpPVC1.Size.Height;

            try
            {
                pt.X = mpPVC1.Location.X + 50;
                pt.Y = mpPVC1.Location.Y + 50;
            }
            catch (Exception ex) { Log.WriteError(ex.ToString()); }
            return pt;
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
                        ifRecordFound = _CNStock.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == "")
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
                        _CNStock.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UOM"].Value.ToString());
                        _CNStock.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _CNStock.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _CNStock.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _CNStock.ReasonCode = prodrow.Cells["Temp_Code"].Value.ToString();

                        string ifRecordFound = "";
                        ifRecordFound = _CNStock.CheckForBatchMRPInStockTable();
                        if (ifRecordFound == _CNStock.StockID)
                            returnVal = _CNStock.UpdateIntblStockReduceForDistributor();
                        if (returnVal)
                        {
                            returnVal = _CNStock.UpdateCreditNoteStockInMasterProductReduceForDistributor();
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
                        _CNStock.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UOM"].Value.ToString());
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.ToolTipText = "Press TAB Key To Exit Product Grid";
                column.ReadOnly = false;
                mpPVC1.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Visible = false;
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                mpPVC1.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);


                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "VATPer";
                column.HeaderText = "GST%";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //6         
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.ToolTipText = "Expiry dd/mm";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 120;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur Rate";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 120;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountPercent";
                column.DataPropertyName = "DiscountPercent";
                column.HeaderText = "Less%";
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 90;
                mpPVC1.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.HeaderText = "Qty";
                column.Width = 100;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Code";
                column.DataPropertyName = "ReasonCode";
                column.HeaderText = "CD";
                column.Width = 35;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                column.ToolTipText = "N=Non Saleable, S=Add to Stock";
                mpPVC1.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.ToolTipText = "N=Qty*Pur.Rate,S=Qty*MRP";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 120;
                column.ReadOnly = true;
                mpPVC1.ColumnsMain.Add(column);

                //12           
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //13       
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Width = 70;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //14
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                column.Width = 60;
                mpPVC1.ColumnsMain.Add(column);
                //15

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
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.Visible = false;
                column.Width = 75;
                mpPVC1.ColumnsMain.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 50;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTAmountZero";
                column.DataPropertyName = "GSTAmountZero";
                column.Width = 80;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //21
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTSAmount";
                column.DataPropertyName = "GSTSAmount";
                column.Width = 80;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //22
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTCAmount";
                column.DataPropertyName = "GSTCAmount";
                column.Width = 80;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //23
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTIAmount";
                column.DataPropertyName = "GSTIAmount";
                column.Width = 80;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //24
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTS";
                column.DataPropertyName = "GSTS";
                column.Width = 80;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //25
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTC";
                column.DataPropertyName = "GSTC";
                column.Width = 80;
                column.Visible = false;
                mpPVC1.ColumnsMain.Add(column);
                //26
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTI";
                column.DataPropertyName = "GSTI";
                column.Width = 80;
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
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATPer";
                column.DataPropertyName = "VATPer";
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 60;
                column.Visible = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.HeaderText = "GST%";
                column.DefaultCellStyle.Format = "n2";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC1.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClStock";
                column.DataPropertyName = "ProdClosingStockPack";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.HeaderText = "Cl.Stk";
                column.Width = 55;
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
                column.Name = "Col_ProductId";
                column.DataPropertyName = "ProductId";
                column.HeaderText = "ProductId";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 100;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batchno";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batchno";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 100;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = 50;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPercent";
                column.DataPropertyName = "ProductVATPercent";
                column.HeaderText = "GST%22";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP22";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "PurRate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.DefaultCellStyle.Format = "N2";
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.HeaderText = "DistSaleRate";
                column.Width = 90;
                column.Visible = false;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "ClStock";
                column.Visible = false;
                column.Width = 65;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStockpack";
                column.DataPropertyName = "ClosingStockPack";
                column.HeaderText = "Cl.STK";
                column.Width = 65;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC1.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
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

        # region Internal methods
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

                DataTable dtable = new DataTable();
                dtable = _CNStock.ReadProductDetailsByID();
                mpPVC1.DataSourceMain = dtable;

                if (_Mode == OperationMode.Delete)
                    mpPVC1.ColumnsMain["Col_ProductName"].ReadOnly = true;

                Product prod = new Product();
                dtable = prod.GetOverviewData();
                mpPVC1.DataSourceProductList = dtable;
                // mpPVC1.DataSourceProductList = General.ProductList;              
                //  mpPVC1.DataSourceProductList = General.ProductList;

                FormatMainGrid();

                mpPVC1.Bind();
                string ifblankrow = "N";
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Cells["Col_ProductID"].Value == null || dr.Cells["Col_ProductID"].Value.ToString() == "")
                    {
                        ifblankrow = "Y";
                        break;
                    }
                }
                if (ifblankrow == "N")
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

            try
            {

                mpPVC1.NewRowColumnName = "Col_Code";

                mpPVC1.BatchGridShowColumnName = "Col_UOM";
                mpPVC1.NumericColumnNames.Add("Col_Quantity");

                //mpPVC1.DoubleColumnNames.Add("Col_VATPer");
                //mpPVC1.DoubleColumnNames.Add("Col_MRP");
                //mpPVC1.DoubleColumnNames.Add("Col_PurRate");
                mpPVC1.DoubleColumnNames.Add("Col_Amount");
                //mpPVC1.DoubleColumnNames.Add("Col_ReturnRate");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                pnlGST.Visible = false;
                pnlIGST.Visible = false;
                mpPVC1.ShowBatchWithZeroStock = true;
                mpPVC1.ProductListGridWidth = 700;
                mpPVC1.BatchListGridWidth = 690;
                cbTransferToAccount.Checked = false;
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                //  txtNoOfRows.Text = "";
                txtVouchernumber.Clear();
                txtVouType.Text = _CNStock.CrdbVouTypeDistributor;
                txtVoucherSeries.Text = General.ShopDetail.ShopVoucherSeries;
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                datePickerBillDate.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy.ToString());
                datePickerBillDate.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey.ToString());
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtTotalAmount.Text = "0.00";
                txtRoundAmount.Text = "0.00";
                mcbCreditor.SelectedID = "";
                _CNStock.StockID = "";
                cbRound.Checked = true;
                mcbCreditor.Focus();
                lblFooterMessage.Text = "";
                InitialisempPVC1();
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
                mcbCreditor.SourceDataString = new string[6] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccIFOMS" };
                mcbCreditor.ColumnWidth = new string[6] { "0", "20", "200", "200", "0", "0" };
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
                    _CNStock.IFOMS = mcbCreditor.SeletedItem.ItemData[5].ToString();
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

                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productRow.Cells["Col_ProductID"].Value;
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
                    mpPVC1.SetFocus(4);           //focus to Batch
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpPVC1_OnBatchSelected(DataGridViewRow batchRow)
        {
            try
            {
                if (_Mode == OperationMode.Add || _Mode == OperationMode.Edit)
                {
                    string newexpirydate = "";
                    string newexpiry = "";
                    double mmrp = 0;
                    double mprate = 0;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = batchRow.Cells["Col_Batchno"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = batchRow.Cells["Col_Expiry"].Value;
                    if (batchRow.Cells["Col_MRP"].Value != null)
                        double.TryParse(batchRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value = mmrp;
                    //  mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value = mmrp;
                    if (batchRow.Cells["Col_PurchaseRate"].Value != null)
                        double.TryParse(batchRow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                    //  mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value = batchRow.Cells["Col_PurchaseRate"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value = batchRow.Cells["Col_PurchaseRate"].Value;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_StockID"].Value = batchRow.Cells["Col_StockID"].Value;
                    newexpiry = mpPVC1.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString();
                    //  double mdiscper = 0;
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountPercent"].Value == null || mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountPercent"].Value.ToString() == string.Empty)
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountPercent"].Value = "0.00";
                    newexpirydate = General.GetValidExpiryDate(newexpiry);
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                    mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = false;
                    lblFooterMessage.Text = "Enter Return Rate";

                    mpPVC1.SetFocus(8); //focus to Quantity
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

        private void mpPVC1_OnNewBatchClicked_1()
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
            try
            {
                if (colIndex == 9)
                {
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

                            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStockpack"].Value != null)
                            {
                                //double clstk = mpPVC1.MainDataGridCurrentRow.Cells[12].Value;
                                //double curstk = mpPVC1.MainDataGridCurrentRow.Cells[9].Value;

                                if ((Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStockpack"].Value.ToString()) + Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString())) >= Convert.ToInt32(mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString()))
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
                        //else if (Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value) == 0.00)
                        //{
                        //    lblFooterMessage.Text = "Enter ReturnRate Rate";
                        //    mpPVC1.SetFocus(9);

                        //}
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

                            foreach (DataGridViewRow dr in mpPVC1.Rows)
                            {
                                if (dr.Index != mpPVC1.MainDataGridCurrentRow.Index && dr.Cells["Col_ProductID"].Value != null)
                                {
                                    string drProductID = dr.Cells["Col_ProductID"].Value.ToString();
                                    string drbatch = dr.Cells["Col_BatchNumber"].Value.ToString();
                                    double drmrp = 0;
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
                    if (mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountPercent"].Value == null || mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountPercent"].Value.ToString() == "")
                    {

                        mpPVC1.SetFocus(9);
                    }
                    else
                    {
                        double mdisc = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountPercent"].Value.ToString());
                        mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountPercent"].Value = mdisc.ToString("#0.00");
                    }
                    //else
                    //{
                    //    double mreturnrate = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value.ToString());
                    //    if (mreturnrate <= 0)
                    //    {
                    //        lblFooterMessage.Text = "Enter Return Rate";
                    //        mpPVC1.SetFocus(9);
                    //    }
                    //}


                }
                CalculateAmount(-1);
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
            double mdiscper = 0;
            double mdiscamt = 0;
            string mcode = " ";
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value != null)
                mpakn = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                mqty = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value != null)
                mprate = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_PurRate"].Value.ToString());
            //if (mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value != null)
            //    mreturnrate = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_ReturnRate"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                mmrp = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountPercent"].Value != null && mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountPercent"].Value.ToString() != string.Empty)
                mdiscper = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountPercent"].Value.ToString());
            if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value != null)
                mcode = mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString();
            //   if (mpPVC1.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == "N")
            if (mcode == "N")
                mamt = Math.Round(mqty * (mmrp), 2);
            else
                mamt = Math.Round(mqty * mprate, 2);
            mdiscamt = Math.Round(mamt * mdiscper / 100, 2);
            mamt = mamt - mdiscamt;
            //   mamount = Math.Round(Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) * Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[8].Value), 2);
            //  else
            //       mamt = mqty * (mmrp / mpakn);
            //mamount = Math.Round(Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[9].Value) * Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells[7].Value), 2);
            mpPVC1.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscamt.ToString("#0.00");
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
                //   txtNoOfRows.Text = itemCount.ToString().Trim();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void CalculateAmount(int deletedrowindex)
        {

            double mamt = 0;
            int itemCount = 0;

            _CNStock.GSTAmt0 = 0;
            _CNStock.GSTAmtS5 = 0;
            _CNStock.GSTAmtC5 = 0;
            _CNStock.GSTAmtI5 = 0;
            _CNStock.GSTS5 = 0;
            _CNStock.GSTC5 = 0;
            _CNStock.GSTI5 = 0;
            _CNStock.GSTAmtS12 = 0;
            _CNStock.GSTAmtC12 = 0;
            _CNStock.GSTAmtI12 = 0;
            _CNStock.GSTS12 = 0;
            _CNStock.GSTC12 = 0;
            _CNStock.GSTI12 = 0;
            _CNStock.GSTAmtS18 = 0;
            _CNStock.GSTAmtC18 = 0;
            _CNStock.GSTAmtI18 = 0;
            _CNStock.GSTS18 = 0;
            _CNStock.GSTC18 = 0;
            _CNStock.GSTI18 = 0;
            _CNStock.GSTAmtS28 = 0;
            _CNStock.GSTAmtC28 = 0;
            _CNStock.GSTAmtI28 = 0;
            _CNStock.GSTS28 = 0;
            _CNStock.GSTC28 = 0;
            _CNStock.GSTI28 = 0;
            double mgstamts = 0;
            double mgstamtc = 0;
            double mgsts = 0;
            double mgstc = 0;
            double mgstamt0 = 0;
            double mtta = 0;

            double TotalAmount = 0;
            double mvatamt = 0;
            //  double VatAmount12Point5 = 0;
            double lessm = 0;
            _CNStock.CrdbDiscPer = 0;
            _CNStock.CrdbDiscAmt = 0;
            if (txtDiscPercent.Text != null && txtDiscPercent.Text.ToString() != string.Empty)
            {
                _CNStock.CrdbDiscPer = Convert.ToDouble(txtDiscPercent.Text.ToString());
                _CNStock.CrdbDiscAmt = Convert.ToDouble(txtDiscAmount.Text.ToString());
            }
            //loop to calculate purchase amount by given customer id
            try
            {
                foreach (DataGridViewRow dr in mpPVC1.Rows)
                {
                    if (dr.Index != deletedrowindex)
                    {
                        if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                        {
                            mamt = double.Parse(dr.Cells["Col_Amount"].Value.ToString());
                            double mvatper = double.Parse(dr.Cells["Col_VATPer"].Value.ToString());
                            lessm = Math.Round((mamt * _CNStock.CrdbDiscPer) / 100, 2);

                            // vat 5.5
                            //   if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 6)

                            mvatamt = Math.Round((((mamt - lessm) * mvatper) / 100), 2);
                            //   else
                            //    {
                            //       if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 13.50)
                            //          VatAmount12Point5 += Math.Round((mamt * mvatper) / (100 + mvatper), 4);
                            //   }
                            mtta = mamt - lessm;
                            //     dr.Cells["Col_Amount"].Value = (mamt).ToString("#0.00");
                            //  mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());
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
                                    _CNStock.GSTAmt0 += mtta;
                                }
                                if (mvatper == 5)
                                {
                                    if (_CNStock.IFOMS != "Y")
                                    {
                                        _CNStock.GSTAmtS5 += mgstamts;
                                        _CNStock.GSTAmtC5 += mgstamtc;
                                        _CNStock.GSTS5 += mgsts;
                                        _CNStock.GSTC5 += mgstc;
                                    }
                                    else
                                    {
                                        _CNStock.GSTAmtI5 += (mgstamts + mgstamtc);
                                        _CNStock.GSTI5 += (mgsts + mgstc);
                                    }
                                }
                                else if (mvatper == 12.00)
                                {
                                    if (_CNStock.IFOMS != "Y")
                                    {
                                        _CNStock.GSTAmtS12 += mgstamts;
                                        _CNStock.GSTAmtC12 += mgstamtc;
                                        _CNStock.GSTS12 += mgsts;
                                        _CNStock.GSTC12 += mgstc;
                                    }
                                    else
                                    {
                                        _CNStock.GSTAmtI12 += (mgstamts + mgstamtc);
                                        _CNStock.GSTI12 += (mgsts + mgstc);
                                    }
                                }
                                else if (mvatper == 18.00)
                                {
                                    if (_CNStock.IFOMS != "Y")
                                    {
                                        _CNStock.GSTAmtS18 += mgstamts;
                                        _CNStock.GSTAmtC18 += mgstamtc;
                                        _CNStock.GSTS18 += mgsts;
                                        _CNStock.GSTC18 += mgstc;
                                    }
                                    else
                                    {
                                        _CNStock.GSTAmtI18 += (mgstamts + mgstamtc);
                                        _CNStock.GSTI18 += (mgsts + mgstc);
                                    }
                                }
                                else if (mvatper == 28.00)
                                {
                                    if (_CNStock.IFOMS != "Y")
                                    {
                                        _CNStock.GSTAmtS28 += mgstamts;
                                        _CNStock.GSTAmtC28 += mgstamtc;
                                        _CNStock.GSTS28 += mgsts;
                                        _CNStock.GSTC28 += mgstc;
                                    }
                                    else
                                    {
                                        _CNStock.GSTAmtI28 += (mgstamts + mgstamtc);
                                        _CNStock.GSTI28 += (mgsts + mgstc);
                                    }
                                }
                                dr.Cells["Col_GSTAmountZero"].Value = mgstamt0;
                                if (_CNStock.IFOMS != "Y")
                                {
                                    dr.Cells["Col_GSTSAmount"].Value = mgstamts.ToString();
                                    dr.Cells["Col_GSTCAmount"].Value = mgstamtc.ToString();
                                    dr.Cells["Col_GSTS"].Value = mgsts.ToString();
                                    dr.Cells["Col_GSTC"].Value = mgstc.ToString();
                                }
                                else
                                {
                                    dr.Cells["Col_GSTIAmount"].Value = (mgstamts + mgstamtc).ToString();
                                    dr.Cells["Col_GSTI"].Value = (mgsts + mgstc).ToString();
                                }
                                TotalAmount += double.Parse(dr.Cells["Col_Amount"].Value.ToString());
                                itemCount += 1;
                            }
                        }
                    }

                }

                txtNoOfRows.Text = itemCount.ToString().Trim();
                double gstamt = _CNStock.GSTAmtS5 + _CNStock.GSTAmtS12 + _CNStock.GSTAmtS18 + _CNStock.GSTAmtS28 + _CNStock.GSTAmtC5 + _CNStock.GSTAmtC12 + _CNStock.GSTAmtC18 + _CNStock.GSTAmtC28 + _CNStock.GSTAmtI5 + _CNStock.GSTAmtI12 + _CNStock.GSTAmtI18 + _CNStock.GSTAmtI28;
                double gst = _CNStock.GSTS5 + _CNStock.GSTS12 + _CNStock.GSTS18 + _CNStock.GSTS28 + _CNStock.GSTC5 + _CNStock.GSTC12 + _CNStock.GSTC18 + _CNStock.GSTC28 + _CNStock.GSTI5 + _CNStock.GSTI12 + _CNStock.GSTI18 + _CNStock.GSTI28;
                double mtotamt = Math.Round(_CNStock.GSTAmt0 + gstamt + gst, 2);
                txtTotalAmount.Text = Math.Round(mtotamt - _CNStock.DiscountAmount, 2).ToString("#0.00");
                txtAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");
                //  CalculateAllAmounts();
                if (cbRound.Checked == true)
                {
                    txtRoundAmount.Text = Math.Round(Math.Round(Convert.ToDouble(txtTotalAmount.Text), 0) - Math.Round(Convert.ToDouble(txtTotalAmount.Text), 2), 2).ToString("#0.00");
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text) + Convert.ToDouble(txtRoundAmount.Text)), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                }
                else
                {
                    txtRoundAmount.Text = "0.00";
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text) + Convert.ToDouble(txtRoundAmount.Text)), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                }
                FillGSTpnl();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        //private void CalculateAmount()
        //{
        //    double TotalAmount = 0;
        //    double VatAmount5 = 0;
        //    double VatAmount12Point5 = 0;
        //    int itemCount = 0;

        //    //loop to calculate purchase amount by given customer id
        //    try
        //    {
        //        foreach (DataGridViewRow dr in mpPVC1.Rows)
        //        {
        //            if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
        //            {
        //                double mamt = double.Parse(dr.Cells["Col_Amount"].Value.ToString());
        //                double mvatper = double.Parse(dr.Cells["Col_VATPer"].Value.ToString());
        //                // vat 5.5
        //                if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 6)

        //                    VatAmount5 += Math.Round((mamt * mvatper) / (100 + mvatper), 4);
        //                else
        //                {
        //                    if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 13.50)
        //                        VatAmount12Point5 += Math.Round((mamt * mvatper) / (100 + mvatper), 4);
        //                }


        //                double mtotgstamt = 0;
        //                double mtotgst = 0;

        //                mtotgstamt =_CNStock.GSTAmt0 + _CNStock.GSTAmtS5 + _CNStock.GSTAmtS12 + _CNStock.GSTAmtS18 + _CNStock.GSTAmtS28 +
        //                             _CNStock.GSTAmtC5 + _CNStock.GSTAmtC12 + _CNStock.GSTAmtC18 + _CNStock.GSTAmtC28 + _CNStock.GSTAmtI5 +
        //                             _CNStock.GSTAmtI12 + _CNStock.GSTAmtI18 + _CNStock.GSTAmtI18 + _CNStock.GSTI28;
        //                mtotgst = _CNStock.GSTS5 + _CNStock.GSTS12 + _CNStock.GSTS18 + _CNStock.GSTS28 +
        //                             _CNStock.GSTC5 + _CNStock.GSTC12 + _CNStock.GSTC18 + _CNStock.GSTC28 + _CNStock.GSTI5 + _CNStock.GSTI12 + _CNStock.GSTI18 + _CNStock.GSTI28;

        //                _CNStock.CrdbAmount = Math.Round(mtotgstamt + mtotgst , 2);


        //                TotalAmount += double.Parse(dr.Cells["Col_Amount"].Value.ToString());
        //                itemCount += 1;
        //            }

        //        }

        //        //   txtNoOfRows.Text = itemCount.ToString().Trim();

        //        txtAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");

        //        CalculateAllAmounts();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        private void FillGSTpnl()
        {
            txtSPUR5.Text = _CNStock.GSTAmtS5.ToString("#0.00");
            txtSPUR12.Text = _CNStock.GSTAmtS12.ToString("#0.00");
            txtSPUR18.Text = _CNStock.GSTAmtS18.ToString("0.00");
            txtSPUR28.Text = _CNStock.GSTAmtS28.ToString("0.00");
            txtCPUR5.Text = _CNStock.GSTAmtC5.ToString("#0.00");
            txtCPUR12.Text = _CNStock.GSTAmtC12.ToString("#0.00");
            txtCPUR18.Text = _CNStock.GSTAmtC18.ToString("0.00");
            txtCPUR28.Text = _CNStock.GSTAmtC28.ToString("0.00");

            txtSGST5.Text = _CNStock.GSTS5.ToString("0.00");
            txtSGST12.Text = _CNStock.GSTS12.ToString("0.00");
            txtSGST18.Text = _CNStock.GSTS18.ToString("0.00");
            txtSGST28.Text = _CNStock.GSTS28.ToString("0.00");

            txtCGST5.Text = _CNStock.GSTC5.ToString("0.00");
            txtCGST12.Text = _CNStock.GSTC12.ToString("0.00");
            txtCGST18.Text = _CNStock.GSTC18.ToString("0.00");
            txtCGST28.Text = _CNStock.GSTC28.ToString("0.00");

            txtIGST5.Text = _CNStock.GSTI5.ToString("#0.00");
            txtIGST12.Text = _CNStock.GSTI12.ToString("#0.00");
            txtIGST18.Text = _CNStock.GSTI18.ToString("#0.00");
            txtIGST28.Text = _CNStock.GSTI28.ToString("#0.00");

            txtIPUR0.Text = _CNStock.GSTAmt0.ToString("#0.00");
            txtIPUR5.Text = _CNStock.GSTAmtI5.ToString("#0.00");
            txtIPUR12.Text = _CNStock.GSTAmtI12.ToString("#0.00");
            txtIPUR18.Text = _CNStock.GSTAmtI18.ToString("#0.00");
            txtIPUR28.Text = _CNStock.GSTAmtI28.ToString("#0.00");
        }
        private void CalculateAllAmounts()
        {
            double mdblAmount;
            double.TryParse(txtAmount.Text, out mdblAmount);
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

                txtTotalAmount.Text = Math.Round(mdblAmount - mdblDiscAmount, 2).ToString("#0.00");

                if (cbRound.Checked == true)
                {
                    txtRoundAmount.Text = Math.Round(Math.Round(Convert.ToDouble(txtTotalAmount.Text), 0) - Math.Round(Convert.ToDouble(txtTotalAmount.Text), 2), 2).ToString("#0.00");
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text) + Convert.ToDouble(txtRoundAmount.Text)), 2).ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                }
                else
                {
                    txtRoundAmount.Text = "0.00";
                    txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalAmount.Text) + Convert.ToDouble(txtRoundAmount.Text)), 2).ToString("#0.00");
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
            double.TryParse(txtAmount.Text, out mdblAmount);
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
                CalculateAmount(-1);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void CalculateFinalVAT()
        {

            _CNStock.GSTAmt0 = 0;
            _CNStock.GSTAmtS5 = 0;
            _CNStock.GSTAmtC5 = 0;
            _CNStock.GSTAmtI5 = 0;
            _CNStock.GSTS5 = 0;
            _CNStock.GSTC5 = 0;
            _CNStock.GSTI5 = 0;
            _CNStock.GSTAmtS12 = 0;
            _CNStock.GSTAmtC12 = 0;
            _CNStock.GSTAmtI12 = 0;
            _CNStock.GSTS12 = 0;
            _CNStock.GSTC12 = 0;
            _CNStock.GSTI12 = 0;
            _CNStock.GSTAmtS18 = 0;
            _CNStock.GSTAmtC18 = 0;
            _CNStock.GSTAmtI18 = 0;
            _CNStock.GSTS18 = 0;
            _CNStock.GSTC18 = 0;
            _CNStock.GSTI18 = 0;
            _CNStock.GSTAmtS28 = 0;
            _CNStock.GSTAmtC28 = 0;
            _CNStock.GSTAmtI28 = 0;
            _CNStock.GSTS28 = 0;
            _CNStock.GSTC28 = 0;
            _CNStock.GSTI28 = 0;
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

            //  double mpuramountzero = 0;
            //  double mpuramount0 = 0;
            //   double mpuramount5 = 0;
            //    double mpuramount12point5 = 0;
            double mamt = 0;
            double mtotalvat = 0;
            if (txtDiscPercent.Text != "")
                _CNStock.DiscountPercent = Convert.ToDouble(txtDiscPercent.Text.ToString());
            try
            {

                foreach (DataGridViewRow dr in mpPVC1.Rows)
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
                        msplddx = 0;
                        mcrddx = 0; //4
                        mcrddx = 0; // ss 19-10-2017
                        mddx = Math.Round(Math.Round((mskl - msplddx - mitm) * _CNStock.DiscountPercent, 2) / 100, 2); //4
                        mtta = Math.Round((mamt - mddx - msplddx - mcrddx - mscmdisc - mitm), 2);
                        mtt1 = Math.Round(mtta * (mmstperpur / 100), 2); //4
                        mtt1S = mtt1;
                        mtt1 = Math.Round(mtt1 / mqty, 2); //4
                        mtotalvat += mtt1;
                        //   dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();
                        //    dr.Cells["Col_CreditNoteAmount"].Value = mcrddx.ToString();
                        dr.Cells["Col_CashDiscountAmount"].Value = mddx.ToString();
                        mtotcashdiscount += mddx;
                        dr.Cells["Col_VATAmountPurchase"].Value = mtt1.ToString();
                        //    dr.Cells["Col_SplDiscountPer"].Value = _Purchase.SpecialDiscountPercentS.ToString();
                        //   dr.Cells["Col_SplDiscountAmount"].Value = msplddx.ToString();


                        if (mmstperpur == 0)
                        {
                            mgstamt0 = mtta;
                            _CNStock.GSTAmt0 += mtta;
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
                            _CNStock.GSTAmtS5 += mgstamts;
                            _CNStock.GSTAmtC5 += mgstamtc;

                            _CNStock.GSTS5 += mgsts;
                            _CNStock.GSTC5 += mgstc;
                        }
                        else if (mmstperpur == 12.00)
                        {
                            _CNStock.GSTAmtS12 += mgstamts;
                            _CNStock.GSTAmtC12 += mgstamtc;
                            _CNStock.GSTS12 += mgsts;
                            _CNStock.GSTC12 += mgstc;

                        }
                        else if (mmstperpur == 18.00)
                        {
                            _CNStock.GSTAmtS18 += mgstamts;
                            _CNStock.GSTAmtC18 += mgstamtc;
                            _CNStock.GSTS18 += mgsts;
                            _CNStock.GSTC18 += mgstc;
                        }
                        else if (mmstperpur == 28.00)
                        {
                            _CNStock.GSTAmtS28 += mgstamts;
                            _CNStock.GSTAmtC28 += mgstamtc;
                            _CNStock.GSTS28 += mgsts;
                            _CNStock.GSTC28 += mgstc;
                        }

                    }
                    dr.Cells["Col_GSTAmountZero"].Value = mgstamt0;
                    dr.Cells["Col_GSTSAmount"].Value = mgstamts.ToString();
                    dr.Cells["Col_GSTCAmount"].Value = mgstamtc.ToString();
                    dr.Cells["Col_GSTS"].Value = mgsts.ToString();
                    dr.Cells["Col_GSTC"].Value = mgstc.ToString();




                }
                txtDiscAmount.Text = mtotcashdiscount.ToString("#0.00");
                //     txtPreCashDiscountAmountS.Text = mtotcashdiscount.ToString("#0.00");
                txtNetAmount.Text = mtotamt.ToString("#0.00");
                FillGSTpnl();


            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPurchase.CalculateFinalVAT>>" + Ex.Message);
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

        private void mpPVC1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    CalculateAmount(-1);
                    break;
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
            PnlGSTVisible();
            txtDiscPercent.Focus();
        }
        private void PnlGSTVisible()
        {
            //   GetpnlGSTLocation();
            //    pnlGST.BringToFront();
            if (_CNStock.IFOMS != "Y")
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
        }
        private void mpPVC1_OnShiftTABKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }

        private void txtDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    CalculateAllAmounts();
                    cbRound.Focus();
                    break;
            }
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
                            _CNStock.ReadDetailsByVouNumberForDistributor(vouno);
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
            //ttcns.SetToolTip(txtNarration, "Enter Narration");
            //ttcns.SetToolTip(txtAmount, "Total Amount of All Rows");
            //ttcns.SetToolTip(txtTotalAmount, "Amount+Vat Amounts - Discount");
            //ttcns.SetToolTip(cbTransferToAccount, "Voucher Transfered to Accounts and not available to adjust in any bill");

        }
        #endregion
    }
}
