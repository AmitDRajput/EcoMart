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
using System.IO;
using EcoMart.Printing;
using PaperlessPharmaRetail.Common.Classes;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDebitNotestock : BaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DebitNoteStock _DNStock;
        private int _Month;
        private int _Year;
        private string _IfAdjusted = "N";
        DataTable dtTempStockOut;
        Timer DateTimer;
        #endregion

        #region constructor
        public UclDebitNotestock()
        {
            try
            {
                InitializeComponent();
                _DNStock = new DebitNoteStock();
                SearchControl = new UclDebitNoteStockSearch();
                CreateStockOutDt();
                DateTimer = new Timer();
                DateTimer.Tick += new EventHandler(DateTimer_Tick);
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
                _DNStock.Initialise();
                ClearControls();
                UpdateTempStockOutDt();
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
                headerLabel1.Text = "DEBIT NOTE STOCK -> NEW";
                FillPartyCombo();
                pnlMonthYear.Visible = false;
                btnAddExpiry.Visible = true;
                btnAddExpiry.Enabled = false;
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
                if (_Mode == OperationMode.Edit)
                    headerLabel1.Text = "DEBIT NOTE STOCK -> EDIT";
                else
                    headerLabel1.Text = "DEBIT NOTE STOCK -> SPLIT";
                FillPartyCombo();
                btnAddExpiry.Visible = false;
                tsBtnFifth.Visible = false;
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
                pnlVouTypeNo.Enabled = true;
                System.IO.File.Delete(General.GetDebitNoteStockTempFile());
                ClearData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool Exit()
        {
            bool retValue = false;
            if ((Mode == OperationMode.Add /*|| Mode == OperationMode.Edit*/) && Convert.ToDecimal(txtNetAmount.Text.ToString()) > 0)
            {
                PSDialogResult result;
                result = PSMessageBox.Show("Save Or Remove All Invoices..", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    retValue = base.Exit();
                    if (retValue)
                    {
                        System.IO.File.Delete(General.GetDebitNoteStockTempFile());
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                }
            }
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {
                headerLabel1.Text = "DEBIT NOTE STOCK -> DELETE";
                ClearData();
                btnAddExpiry.Visible = false;
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
                if (_DNStock.Id != null && _DNStock.Id != "")
                {
                    bool canbedeleted = true;
                    if (canbedeleted)
                    {
                        BindTempGrid();
                        LockTable.LockTablesForCreditDebitNoteStock();
                        General.BeginTransaction();
                        retValue = AddPreviousStock();
                        if (retValue)
                            retValue = DeletePreviousProductRows();
                        if (retValue)
                            retValue = _DNStock.DeleteDetails();
                        _DNStock.RemoveAccountDetails();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            //  UpdateClosingStockinCache();
                            retValue = true;
                            MessageBox.Show("Voucher Deleted Successfully.", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            PSDialogResult result = PSMessageBox.Show("Could not Delete...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
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
                headerLabel1.Text = "DEBIT NOTE STOCK -> VIEW";
                InitialisempPVC1();
                btnAddExpiry.Visible = false;
                FillPartyCombo();
                tsBtnFifth.Text = "Split";
                tsBtnFifth.Visible = true;
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
                    _DNStock.CrdbVouType = FixAccounts.VoucherTypeForDebitNoteStock;
                }
                _DNStock.GetLastRecord();
                FillSearchData(_DNStock.Id, "");
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
                    PrintDebitNotePrePrintedPaper();
                else
                    PrintDebitNotePlainPaper();
            }
            ClearData();
            return retValue;
        }
        private void PrintDebitNotePrePrintedPaper()
        {
            EcoMart.Printing.PrePrintedPaperPrinter printer = new EcoMart.Printing.PrePrintedPaperPrinter();
            printer.Print(_DNStock.CrdbVouType, _DNStock.CrdbVouNo.ToString(), _DNStock.CrdbVouDate, _DNStock.Name, _DNStock.CrdbAddress, "", "", "", mpPVC2.Rows, _DNStock.CrdbNarration, _DNStock.CrdbAmountNet, "", _DNStock.CrdbDiscAmt, 0, 0, _DNStock.CrdbAmount, 0 + _DNStock.CrdbAmountNet, "");

        }

        private void PrintDebitNotePlainPaper()
        {
            EcoMart.Printing.PlainPaperPrinter printer = new EcoMart.Printing.PlainPaperPrinter();
            printer.Print(_DNStock.CrdbVouType, _DNStock.CrdbVouNo.ToString(), _DNStock.CrdbVouDate, _DNStock.Name, _DNStock.CrdbAddress, "", "", "", mpPVC2.Rows, _DNStock.CrdbNarration, _DNStock.CrdbAmountNet, "", _DNStock.CrdbDiscAmt, 0, 0, _DNStock.CrdbTotalAmount, 0 + _DNStock.CrdbAmountNet, "");

        }
        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _DNStock.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _DNStock.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _DNStock.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _DNStock.GetFirstRecord();
            if (dr != null && dr["CRDBID"] != DBNull.Value)
            {
                _DNStock.Id = dr["CRDBID"].ToString();
                FillSearchData(_DNStock.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _DNStock.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _DNStock.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _DNStock.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _DNStock.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _DNStock.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _DNStock.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _DNStock.CrdbVouNo = i;
                dr = _DNStock.ReadDetailsByVouNumber(_DNStock.CrdbVouNo);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CRDBID"] != DBNull.Value)
            {
                _DNStock.Id = dr["CRDBID"].ToString();
                FillSearchData(_DNStock.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _DNStock.GetLastVoucherNumber(FixAccounts.VoucherTypeForDebitNoteStock, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _DNStock.CrdbVouType = txtVouType.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _DNStock.CrdbVouNo = i;
                dr = _DNStock.ReadDetailsByVouNumber(i);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CRDBID"] != DBNull.Value)
            {
                _DNStock.Id = dr["CRDBID"].ToString();
                FillSearchData(_DNStock.Id, "");
            }
            return retValue;
        }
        public override bool Save()
        {
            return SaveData(false);
        }

        public override bool SaveAndPrint()
        {

            return SaveData(false);
        }

        private bool SaveData(bool printData)
        {

            bool retValue = false;
            if (_Mode != OperationMode.View && _Mode != OperationMode.ReportView && _IfAdjusted != "Y")
            {
                double mdiscamount = 0;
                double mvat5per = 0;
                double mvat12point5per = 0;
                double mdiscper = 0;
                double mbillamount = 0;
                double mamount = 0;
                double mround = 0;
                if (txtBillAmount.Text != null && Convert.ToDouble(txtBillAmount.Text.ToString()) > 0)
                {
                    try
                    {
                        System.Text.StringBuilder _errorMessage;
                        if (mcbCreditor.SelectedID != null)
                            _DNStock.CrdbId = mcbCreditor.SelectedID.Trim();
                        _DNStock.CrdbNarration = txtNarration.Text.Trim();
                        if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                            _DNStock.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                        _DNStock.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                        double.TryParse(txtVatInput5per.Text, out mvat5per);
                        _DNStock.CrdbVat5 = mvat5per;
                        double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                        _DNStock.CrdbVat12point5 = mvat12point5per;
                        double.TryParse(txtDiscPercent.Text, out mdiscper);
                        _DNStock.CrdbDiscPer = mdiscper;
                        double.TryParse(txtDiscAmount.Text, out mdiscamount);
                        _DNStock.CrdbDiscAmt = mdiscamount;
                        double.TryParse(txtBillAmount.Text, out mbillamount);
                        _DNStock.CrdbAmountNet = mbillamount;
                        if (_Mode == OperationMode.Fifth)
                            _DNStock.CrdbPreSelectedAmount = mbillamount;
                        else
                            _DNStock.CrdbPreSelectedAmount = 0;
                        double.TryParse(txtTotalAmount.Text, out mamount);
                        _DNStock.CrdbTotalAmount = mamount;
                        double.TryParse(txtRoundAmount.Text, out mround);
                        _DNStock.CrdbRoundAmount = mround;

                        if (txtNetAmountForSelected.Text != null && txtNetAmountForSelected.Text.ToString() != string.Empty)
                            _DNStock.CrdbBillAmountForSelected = Convert.ToDouble(txtNetAmountForSelected.Text.ToString());
                        if (cbTransferToAccount.Checked == true)
                        {
                            _DNStock.TrasferToAccount = "Y";
                            _DNStock.ClearVouType = FixAccounts.VoucherTypeForTransferToAccount;
                        }
                        else
                        {
                            _DNStock.TrasferToAccount = "N";
                            _DNStock.ClearVouType = "";
                        }
                        if (_DNStock.TrasferToAccount == "Y")
                            _DNStock.CrdbAmountClear = mbillamount;
                        else
                            _DNStock.CrdbAmountClear = 0;
                        if (_Mode == OperationMode.Edit)
                            _DNStock.IFEdit = "Y";

                        _DNStock.Validate();


                        if (_Mode == OperationMode.Fifth || _DNStock.IsValid)
                        {

                            LockTable.LockTablesForCreditDebitNoteStock();
                            if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                            {
                                General.BeginTransaction();
                                //_DNStock.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                _DNStock.CrdbVouNo = _DNStock.GetAndUpdateDNNumber(General.ShopDetail.ShopVoucherSeries);
                                txtVouchernumber.Text = Convert.ToString(_DNStock.CrdbVouNo);
                                _DNStock.CreatedBy = General.CurrentUser.Id;
                                _DNStock.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _DNStock.CreatedTime = DateTime.Now.ToString("HH:mm:ss");

                                _DNStock.Id = _DNStock.AddDetails().ToString();
                                retValue = Convert.ToInt32(_DNStock.Id) > 0;
                                _SavedID = _DNStock.Id;
                                if (retValue)
                                    retValue = SaveParticularsProductwise();
                                if (retValue)
                                    retValue = ReduceStockIntblStock();

                                if (retValue)
                                {
                                    if (_DNStock.TrasferToAccount == "Y")
                                    {
                                        //_DNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                        _DNStock.AddAccountDetailsIntbltrnacDebit();

                                        //_DNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                        _DNStock.AddAccountDetailsIntbltrnacCredit();
                                    }
                                }



                                if (retValue)
                                    General.CommitTransaction();
                                else
                                    General.RollbackTransaction();

                                LockTable.UnLockTables();

                                if (retValue)
                                {
                                    // retValue = UpdateClosingStockinCache();

                                    System.IO.File.Delete(General.GetDebitNoteStockTempFile());
                                    string msgLine2 = _DNStock.CrdbVouType + "  " + _DNStock.CrdbVouNo.ToString("#0");
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
                                    PSDialogResult result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                    retValue = false;
                                }
                            }
                            else if (_Mode == OperationMode.Edit)
                            {
                                General.BeginTransaction();
                                _DNStock.ModifiedBy = General.CurrentUser.Id;
                                _DNStock.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                _DNStock.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                                retValue = _DNStock.UpdateDetails();
                                if (retValue)
                                    retValue = DeletePreviousProductRows();
                                if (retValue)
                                    retValue = SaveParticularsProductwise();
                                if (retValue)
                                    retValue = AddPreviousStock();
                                if (retValue)
                                    retValue = ReduceStockIntblStock();
                                _DNStock.RemoveAccountDetails();

                                if (retValue)
                                {
                                    if (_DNStock.TrasferToAccount == "Y")
                                    {
                                        _DNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                        _DNStock.AddAccountDetailsIntbltrnacDebit();

                                        _DNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                        _DNStock.AddAccountDetailsIntbltrnacCredit();
                                    }
                                }


                                if (retValue)
                                    General.CommitTransaction();
                                else
                                    General.RollbackTransaction();
                                LockTable.UnLockTables();
                                if (retValue)
                                {
                                    // retValue = UpdateClosingStockinCache();

                                    System.IO.File.Delete(General.GetDebitNoteStockTempFile());
                                    string msgLine2 = _DNStock.CrdbVouType + "  " + _DNStock.CrdbVouNo.ToString("#0");
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
                                    mpPVC2.DataSourceMain = null;
                                    //   mpPVC1.DataSourceProductList = EcoMartCache.GetProductData();
                                    //    DataTable dtable = General.ProductList;
                                    Product prod = new Product();
                                    DataTable dtable = prod.GetOverviewData();
                                    mpPVC2.DataSourceProductList = dtable;
                                    mpPVC2.Bind();
                                    sortcolumn();
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
                                if (_DNStock.CrdbBillAmountForSelected != 0)
                                {

                                    General.BeginTransaction();
                                    //_DNStock.IDForSelected = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _DNStock.CrdbVouNoForSelected = _DNStock.GetAndUpdateDNNumber(General.ShopDetail.ShopVoucherSeries);
                                    txtVouchernumber.Text = Convert.ToString(_DNStock.CrdbVouNo);
                                    _DNStock.CreatedBy = General.CurrentUser.Id;
                                    _DNStock.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                    _DNStock.CreatedTime = DateTime.Now.ToString("HH:mm:ss");

                                    _DNStock.CrdbAmountForSelected = Convert.ToDouble(txtTotalAmountForSelected.Text.ToString());
                                    _DNStock.CrdbBillAmountForSelected = Math.Floor(Convert.ToDouble(txtNetAmountForSelected.Text.ToString()));
                                    _DNStock.CrdbVat12point5ForSelected = Convert.ToDouble(txtVat12point5ForSelected.Text.ToString());
                                    _DNStock.CrdbVat5ForSelected = Convert.ToDouble(txtVat5ForSelected.Text.ToString());
                                    _DNStock.crdbDiscAmtForSelected = Convert.ToDouble(txtDiscountAmountForSelected.Text.ToString());
                                    _DNStock.CrdbRoundAmount = (Convert.ToDouble(txtNetAmountForSelected.Text.ToString())) - Math.Floor(Convert.ToDouble(txtNetAmountForSelected.Text.ToString()));





                                    _DNStock.Id = _DNStock.AddDetailsForSelected().ToString();
                                    retValue = Convert.ToInt32(_DNStock.Id) > 0;
                                    _SavedID = _DNStock.Id;
                                    if (retValue)
                                        retValue = SaveParticularsProductwiseForSelected();
                                    //if (retValue)
                                    //    General.CommitTransaction();
                                    //else
                                    //    General.RollbackTransaction();

                                    //LockTable.UnLockTables();

                                    if (retValue)
                                    {
                                        //string msgLine2 = _DNStock.CrdbVouType + "  " + _DNStock.CrdbVouNoForSelected.ToString("#0");
                                        //PSDialogResult result;
                                        //if (printData)
                                        //{
                                        //    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                        //    Print();
                                        //}
                                        //else
                                        //{
                                        //    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                        //    if (result == PSDialogResult.Print)
                                        //        Print();
                                        //}
                                        retValue = true;
                                    }
                                    else
                                    {
                                        PSDialogResult result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                        retValue = false;
                                    }

                                    if (retValue)
                                    {

                                        _DNStock.ModifiedBy = General.CurrentUser.Id;
                                        _DNStock.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                        _DNStock.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");

                                        _DNStock.CrdbTotalAmount -= _DNStock.CrdbAmountForSelected;
                                        _DNStock.CrdbVat12point5 -= _DNStock.CrdbVat12point5ForSelected;
                                        _DNStock.CrdbVat5 -= _DNStock.CrdbVat5ForSelected;
                                        _DNStock.CrdbDiscAmt -= _DNStock.crdbDiscAmtForSelected;
                                        _DNStock.CrdbAmountNet = Math.Floor(_DNStock.CrdbTotalAmount - _DNStock.CrdbDiscAmt);
                                        _DNStock.CrdbRoundAmount = (_DNStock.CrdbTotalAmount - _DNStock.CrdbDiscAmt) - Math.Floor(_DNStock.CrdbTotalAmount - _DNStock.CrdbDiscAmt);


                                        retValue = _DNStock.UpdateDetails();
                                        if (retValue)
                                            retValue = DeletePreviousProductRows();
                                        if (retValue)
                                            retValue = SaveParticularsProductwiseRemaining();

                                        if (retValue)
                                            General.CommitTransaction();
                                        else
                                            General.RollbackTransaction();
                                        LockTable.UnLockTables();
                                        if (retValue)
                                        {
                                            System.IO.File.Delete(General.GetDebitNoteStockTempFile());
                                            string msgLine2 = _DNStock.CrdbVouType + "  " + _DNStock.CrdbVouNoForSelected.ToString("#0");
                                            PSDialogResult result;
                                            //if (printData)
                                            //{
                                            result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                                            //  Print();
                                            //}
                                            //else
                                            //{
                                            //    result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                                            //    //if (result == PSDialogResult.Print)
                                            //    //    Print();
                                            //}
                                            mpPVC2.DataSourceMain = null;
                                            //   mpPVC1.DataSourceProductList = EcoMartCache.GetProductData();
                                            DataTable dtable = new DataTable();
                                            //dtable = General.ProductList;
                                            Product prod = new Product();
                                            dtable = prod.GetOverviewData();
                                            mpPVC2.DataSourceProductList = dtable;
                                            mpPVC2.Bind();
                                            sortcolumn();
                                            retValue = true;
                                        }
                                        else
                                        {
                                            PSDialogResult result = PSMessageBox.Show("Could not Save Voucher...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                                            retValue = false;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            LockTable.UnLockTables();
                            _errorMessage = new System.Text.StringBuilder();
                            _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                            foreach (string _message in _DNStock.ValidationMessages)
                            {
                                _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                            }
                            MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    catch (Exception Ex)
                    {
                        LockTable.UnLockTables();
                        Log.WriteException(Ex);
                    }
                }
            }
            LockTable.UnLockTables();
            CacheObject.Clear("TempCounterSale");
            return retValue;
        }


        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    _DNStock.Id = ID;
                    _DNStock.ReadDetailsByID();
                    mcbCreditor.SelectedID = _DNStock.CrdbId;
                    BindTempGrid();
                    InitialisempPVC1();
                    NumberofRows();
                    txtNarration.Text = _DNStock.CrdbNarration.ToString();
                    txtVouType.Text = _DNStock.CrdbVouType;
                    txtVouchernumber.Text = _DNStock.CrdbVouNo.ToString().Trim();
                    if (_DNStock.CrdbVat5 >= 0)
                        txtVatInput5per.Text = _DNStock.CrdbVat5.ToString("#0.00");
                    if (_DNStock.CrdbVat12point5 >= 0)
                        txtVatInput12point5per.Text = _DNStock.CrdbVat12point5.ToString("#0.00");
                    if (_DNStock.CrdbDiscPer >= 0)
                        txtDiscPercent.Text = _DNStock.CrdbDiscPer.ToString("#0.00");
                    if (_DNStock.CrdbDiscAmt > 0)
                        txtDiscAmount.Text = _DNStock.CrdbDiscAmt.ToString("#0.00");

                    txtBillAmount.Text = _DNStock.CrdbAmountNet.ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                    txtAmount.Text = _DNStock.CrdbAmount.ToString("#0.00");
                    if (_DNStock.TrasferToAccount == "Y")
                        cbTransferToAccount.Checked = true;
                    else
                        cbTransferToAccount.Checked = false;
                    if (_DNStock.CrdbRoundAmount != 0)
                        txtRoundAmount.Text = _DNStock.CrdbRoundAmount.ToString("#0.00");
                    txtTotalAmount.Text = _DNStock.CrdbTotalAmount.ToString("#0.00");
                    if (DateTime.TryParse(_DNStock.CrdbVouDate, out DateTime mydate))
                    {
                        datePickerBillDate.Value = mydate;
                    }
                    _IfAdjusted = "N";
                    if (_DNStock.ClearedIn != "")
                    {
                        lblAdjustedIn.Visible = true;
                        txtAdjustedIn.Visible = true;
                        txtAdjustedIn.Text = _DNStock.ClearedIn;

                        //lblFooterMessage.Text = "Debit Note Cleared in Purchase Voucher:" + _DNStock.ClearVouType + " " + _DNStock.ClearVouNo.ToString();
                        _IfAdjusted = "Y";
                    }

                    if (_Mode == OperationMode.Delete || _Mode == OperationMode.View || _Mode == OperationMode.ReportView || _IfAdjusted == "Y")
                    {
                        pnlAmounts.Enabled = false;
                        mpPVC2.ColumnsMain["Col_ProductName"].ReadOnly = true;
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        pnlAmounts.Enabled = true;
                        mcbCreditor.Enabled = true;
                        txtNarration.Enabled = true;
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

        #region Idetail members
        public override void ReFillData(Control closedControl)
        {
            try
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


                if (keyPressed == Keys.A && modifier == Keys.Alt)
                {
                    mcbCreditor.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.L && modifier == Keys.Alt)
                {
                    txtDiscPercent.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.N && modifier == Keys.Alt)
                {
                    txtNarration.Focus();
                    retValue = true;
                }
                if (keyPressed == Keys.Escape)
                {
                    //if (mpPVC1.VisibleProductGrid() == true) //kiran
                    //{
                    //    retValue = true;
                    //}
                    //else
                    if (mpPVC2.VisibleProductGrid() == true)
                    {
                        lblFooterMessage.Text = "";
                        lblRightSideFooterMsg.Text = "";
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

        private bool DeletePreviousProductRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _DNStock.DeletePreviousRecords();
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
                _DNStock.SerialNumber = 0;
                int mpakn = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in mpPVC2.Rows)
                    {
                        if (prodrow.Cells["Col_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                        {
                            _DNStock.SerialNumber += 1;
                            //_DNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _DNStock.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                            _DNStock.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
                            mpakn = 1;
                            if (prodrow.Cells["Col_UOM"].Value != null && prodrow.Cells["Col_UOM"].Value.ToString() != "")
                                mpakn = Convert.ToInt32(prodrow.Cells["Col_UOM"].Value.ToString());
                            _DNStock.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            _DNStock.Quantity = 0;
                            if (prodrow.Cells["Col_Quantity"].Value != null && prodrow.Cells["Col_Quantity"].Value.ToString() != "")
                                _DNStock.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                            _DNStock.SchemeQuanity = 0;
                            if (prodrow.Cells["Col_ScmQuantity"].Value != null && prodrow.Cells["Col_ScmQuantity"].Value.ToString() != "")
                                _DNStock.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Col_ScmQuantity"].Value.ToString());
                            _DNStock.PurchaseRate = 0;
                            if (prodrow.Cells["Col_PurRate"].Value != null && prodrow.Cells["Col_PurRate"].Value.ToString() != "")
                                _DNStock.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurRate"].Value.ToString());
                            _DNStock.TradeRate = 0;
                            if (prodrow.Cells["Col_TradeRate"].Value != null && prodrow.Cells["Col_TradeRate"].Value.ToString() != "")
                                _DNStock.TradeRate = Convert.ToDouble(prodrow.Cells["Col_TradeRate"].Value.ToString());
                            _DNStock.MRP = 0;
                            if (prodrow.Cells["Col_MRP"].Value != null && prodrow.Cells["Col_MRP"].Value.ToString() != "")
                                _DNStock.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                            _DNStock.SaleRate = _DNStock.MRP;
                            if (prodrow.Cells["Col_SaleRate"].Value != null)
                                _DNStock.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                            _DNStock.Expiry = "";
                            if (prodrow.Cells["Col_Expiry"].Value != null && prodrow.Cells["Col_Expiry"].Value.ToString() != "")
                                _DNStock.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            _DNStock.Expiry = General.GetValidExpiry(_DNStock.Expiry);
                            _DNStock.ExpiryDate = "";
                            _DNStock.ExpiryDate = General.GetValidExpiryDate(_DNStock.Expiry);
                            _DNStock.ExpiryDate = General.GetExpiryInyyyymmddForm(_DNStock.ExpiryDate);
                            _DNStock.VATPer = 0;
                            if (prodrow.Cells["Col_VATPer"].Value != null && prodrow.Cells["Col_VATPer"].Value.ToString() != "")
                                _DNStock.VATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value.ToString());
                            if (prodrow.Cells["Col_VATAmount"].Value != null && prodrow.Cells["Col_VATAmount"].Value.ToString() != "")
                                _DNStock.VATAmount = Convert.ToDouble(prodrow.Cells["Col_VATAmount"].Value.ToString());
                            _DNStock.ReasonCode = "E";
                            if (prodrow.Cells["Col_Code"].Value != null && prodrow.Cells["Col_Code"].Value.ToString() != "")
                                _DNStock.ReasonCode = prodrow.Cells["Col_Code"].Value.ToString();
                            _DNStock.DiscountPercent = 0;
                            if (prodrow.Cells["Col_Discount"].Value != null && prodrow.Cells["Col_Discount"].Value.ToString() != "")
                                _DNStock.DiscountPercent = Convert.ToDouble(prodrow.Cells["Col_Discount"].Value.ToString());
                            _DNStock.DiscountAmount = 0;
                            if (prodrow.Cells["Col_DiscountAmount"].Value != null && prodrow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                _DNStock.DiscountAmount = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                            _DNStock.Amount = 0;
                            if (prodrow.Cells["Col_IFAddVATInTradeRate"].Value != null)
                                _DNStock.IfAddVATInTradeRate = prodrow.Cells["Col_IFAddVATInTradeRate"].Value.ToString();

                            if (prodrow.Cells["Col_Amount"].Value != null && prodrow.Cells["Col_Amount"].Value.ToString() != "")
                                _DNStock.Amount = Convert.ToDouble(prodrow.Cells["Col_Amount"].Value.ToString());
                            returnVal = _DNStock.AddProductDetails();
                            if (returnVal == false)
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

        private bool SaveParticularsProductwiseForSelected()
        {
            {
                bool returnVal = false;
                _DNStock.SerialNumber = 0;
                int mpakn = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in mpPVC2.Rows)
                    {
                        if (prodrow.Cells["Col_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0 && prodrow.Cells["Col_Check"].Value != null && Convert.ToBoolean(prodrow.Cells["Col_Check"].Value.ToString()) == true)
                        {
                            _DNStock.SerialNumber += 1;
                            _DNStock.DetailIDForSelected = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _DNStock.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                            _DNStock.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
                            mpakn = 1;
                            mpakn = Convert.ToInt32(prodrow.Cells["Col_UOM"].Value.ToString());
                            _DNStock.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            _DNStock.Quantity = 0;
                            if (prodrow.Cells["Col_Quantity"].Value != null && prodrow.Cells["Col_Quantity"].Value.ToString() != "")
                                _DNStock.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                            _DNStock.SchemeQuanity = 0;
                            if (prodrow.Cells["Col_ScmQuantity"].Value != null && prodrow.Cells["Col_ScmQuantity"].Value.ToString() != "")
                                _DNStock.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Col_ScmQuantity"].Value.ToString());
                            _DNStock.PurchaseRate = 0;
                            if (prodrow.Cells["Col_PurRate"].Value != null && prodrow.Cells["Col_PurRate"].Value.ToString() != "")
                                _DNStock.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurRate"].Value.ToString());
                            _DNStock.TradeRate = 0;
                            if (prodrow.Cells["Col_TradeRate"].Value != null && prodrow.Cells["Col_TradeRate"].Value.ToString() != "")
                                _DNStock.TradeRate = Convert.ToDouble(prodrow.Cells["Col_TradeRate"].Value.ToString());
                            _DNStock.MRP = 0;
                            if (prodrow.Cells["Col_MRP"].Value != null && prodrow.Cells["Col_MRP"].Value.ToString() != "")
                                _DNStock.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                            _DNStock.SaleRate = _DNStock.MRP;
                            if (prodrow.Cells["Col_SaleRate"].Value != null)
                                _DNStock.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                            _DNStock.Expiry = "";
                            if (prodrow.Cells["Col_Expiry"].Value != null && prodrow.Cells["Col_Expiry"].Value.ToString() != "")
                                _DNStock.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            _DNStock.Expiry = General.GetValidExpiry(_DNStock.Expiry);
                            _DNStock.ExpiryDate = "";
                            _DNStock.ExpiryDate = General.GetValidExpiryDate(_DNStock.Expiry);
                            _DNStock.ExpiryDate = General.GetExpiryInyyyymmddForm(_DNStock.ExpiryDate);
                            _DNStock.VATPer = 0;
                            if (prodrow.Cells["Col_VATPer"].Value != null && prodrow.Cells["Col_VATPer"].Value.ToString() != "")
                                _DNStock.VATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value.ToString());
                            if (prodrow.Cells["Col_VATAmount"].Value != null && prodrow.Cells["Col_VATAmount"].Value.ToString() != "")
                                _DNStock.VATAmount = Convert.ToDouble(prodrow.Cells["Col_VATAmount"].Value.ToString());
                            _DNStock.ReasonCode = "E";
                            if (prodrow.Cells["Col_Code"].Value != null && prodrow.Cells["Col_Code"].Value.ToString() != "")
                                _DNStock.ReasonCode = prodrow.Cells["Col_Code"].Value.ToString();
                            _DNStock.DiscountPercent = 0;
                            if (prodrow.Cells["Col_Discount"].Value != null && prodrow.Cells["Col_Discount"].Value.ToString() != "")
                                _DNStock.DiscountPercent = Convert.ToDouble(prodrow.Cells["Col_Discount"].Value.ToString());
                            _DNStock.DiscountAmount = 0;
                            if (prodrow.Cells["Col_DiscountAmount"].Value != null && prodrow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                _DNStock.DiscountAmount = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                            _DNStock.Amount = 0;
                            if (prodrow.Cells["Col_IFAddVATInTradeRate"].Value != null)
                                _DNStock.IfAddVATInTradeRate = prodrow.Cells["Col_IFAddVATInTradeRate"].Value.ToString();

                            if (prodrow.Cells["Col_Amount"].Value != null && prodrow.Cells["Col_Amount"].Value.ToString() != "")
                                _DNStock.Amount = Convert.ToDouble(prodrow.Cells["Col_Amount"].Value.ToString());
                            returnVal = _DNStock.AddProductDetailsForSelected();
                            if (returnVal == false)
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

        private bool SaveParticularsProductwiseRemaining()
        {
            {
                bool returnVal = false;
                _DNStock.SerialNumber = 0;
                int mpakn = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in mpPVC2.Rows)
                    {
                        if (prodrow.Cells["Col_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0 && (prodrow.Cells["Col_Check"].Value == null || Convert.ToBoolean(prodrow.Cells["Col_Check"].Value.ToString()) == false))
                        {
                            _DNStock.SerialNumber += 1;
                            //_DNStock.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _DNStock.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                            _DNStock.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
                            mpakn = 1;
                            mpakn = Convert.ToInt32(prodrow.Cells["Col_UOM"].Value.ToString());
                            _DNStock.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                            _DNStock.Quantity = 0;
                            if (prodrow.Cells["Col_Quantity"].Value != null && prodrow.Cells["Col_Quantity"].Value.ToString() != "")
                                _DNStock.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                            _DNStock.SchemeQuanity = 0;
                            if (prodrow.Cells["Col_ScmQuantity"].Value != null && prodrow.Cells["Col_ScmQuantity"].Value.ToString() != "")
                                _DNStock.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Col_ScmQuantity"].Value.ToString());
                            _DNStock.PurchaseRate = 0;
                            if (prodrow.Cells["Col_PurRate"].Value != null && prodrow.Cells["Col_PurRate"].Value.ToString() != "")
                                _DNStock.PurchaseRate = Convert.ToDouble(prodrow.Cells["Col_PurRate"].Value.ToString());
                            _DNStock.TradeRate = 0;
                            if (prodrow.Cells["Col_TradeRate"].Value != null && prodrow.Cells["Col_TradeRate"].Value.ToString() != "")
                                _DNStock.TradeRate = Convert.ToDouble(prodrow.Cells["Col_TradeRate"].Value.ToString());
                            _DNStock.MRP = 0;
                            if (prodrow.Cells["Col_MRP"].Value != null && prodrow.Cells["Col_MRP"].Value.ToString() != "")
                                _DNStock.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                            _DNStock.SaleRate = _DNStock.MRP;
                            if (prodrow.Cells["Col_SaleRate"].Value != null)
                                _DNStock.SaleRate = Convert.ToDouble(prodrow.Cells["Col_SaleRate"].Value.ToString());
                            _DNStock.Expiry = "";
                            if (prodrow.Cells["Col_Expiry"].Value != null && prodrow.Cells["Col_Expiry"].Value.ToString() != "")
                                _DNStock.Expiry = prodrow.Cells["Col_Expiry"].Value.ToString();
                            _DNStock.Expiry = General.GetValidExpiry(_DNStock.Expiry);
                            _DNStock.ExpiryDate = "";
                            _DNStock.ExpiryDate = General.GetValidExpiryDate(_DNStock.Expiry);
                            _DNStock.ExpiryDate = General.GetExpiryInyyyymmddForm(_DNStock.ExpiryDate);
                            _DNStock.VATPer = 0;
                            if (prodrow.Cells["Col_VATPer"].Value != null && prodrow.Cells["Col_VATPer"].Value.ToString() != "")
                                _DNStock.VATPer = Convert.ToDouble(prodrow.Cells["Col_VATPer"].Value.ToString());
                            if (prodrow.Cells["Col_VATAmount"].Value != null && prodrow.Cells["Col_VATAmount"].Value.ToString() != "")
                                _DNStock.VATAmount = Convert.ToDouble(prodrow.Cells["Col_VATAmount"].Value.ToString());
                            _DNStock.ReasonCode = "E";
                            if (prodrow.Cells["Col_Code"].Value != null && prodrow.Cells["Col_Code"].Value.ToString() != "")
                                _DNStock.ReasonCode = prodrow.Cells["Col_Code"].Value.ToString();
                            _DNStock.DiscountPercent = 0;
                            if (prodrow.Cells["Col_Discount"].Value != null && prodrow.Cells["Col_Discount"].Value.ToString() != "")
                                _DNStock.DiscountPercent = Convert.ToDouble(prodrow.Cells["Col_Discount"].Value.ToString());
                            _DNStock.DiscountAmount = 0;
                            if (prodrow.Cells["Col_DiscountAmount"].Value != null && prodrow.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                _DNStock.DiscountAmount = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                            _DNStock.Amount = 0;
                            if (prodrow.Cells["Col_IFAddVATInTradeRate"].Value != null)
                                _DNStock.IfAddVATInTradeRate = prodrow.Cells["Col_IFAddVATInTradeRate"].Value.ToString();

                            if (prodrow.Cells["Col_Amount"].Value != null && prodrow.Cells["Col_Amount"].Value.ToString() != "")
                                _DNStock.Amount = Convert.ToDouble(prodrow.Cells["Col_Amount"].Value.ToString());
                            returnVal = _DNStock.AddProductDetails();
                            if (returnVal == false)
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

        private bool ReduceStockIntblStock()
        {
            bool returnVal = false;
            int mpakn = 0;
            try
            {
                foreach (DataGridViewRow prodrow in mpPVC2.Rows)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        mpakn = 1;
                        if (prodrow.Cells["Col_UOM"].Value != null && prodrow.Cells["Col_UOM"].Value.ToString() != "")
                            mpakn = Convert.ToInt32(prodrow.Cells["Col_UOM"].Value.ToString());
                        _DNStock.ProdLoosePack = mpakn;
                        _DNStock.StockID = prodrow.Cells["Col_StockID"].Value.ToString();
                        _DNStock.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
                        _DNStock.Batchno = prodrow.Cells["Col_BatchNumber"].Value.ToString();
                        _DNStock.MRP = Convert.ToDouble(prodrow.Cells["Col_MRP"].Value.ToString());
                        _DNStock.Quantity = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                        if (prodrow.Cells["Col_ScmQuantity"].Value != null && prodrow.Cells["Col_ScmQuantity"].Value.ToString() != "")
                            _DNStock.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Col_ScmQuantity"].Value.ToString());

                        string ifRecordFound = "";
                        ifRecordFound = _DNStock.CheckForStockIDInStockTable();
                        if (ifRecordFound == "Y")
                        {
                            returnVal = _DNStock.UpdateIntblStock();
                            if (returnVal)
                            {
                                returnVal = _DNStock.UpdateDebitNoteStockInMasterProduct();
                                if (returnVal == false)
                                    break;
                            }
                            else
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
        //        foreach (DataGridViewRow prodrow in mpPVC1.Rows)
        //        {
        //            if (prodrow.Cells["Col_ProductID"].Value != null && prodrow.Cells["Col_ProductID"].Value.ToString() != string.Empty)
        //            {
        //                _DNStock.ProductID = Convert.ToInt32(prodrow.Cells["Col_ProductID"].Value.ToString());
        //                EcoMartCache.RefreshProductData(_DNStock.ProductID);
        //                returnVal = true;
        //            }
        //        }

        //        foreach (DataGridViewRow prodrow in dgtemp.Rows)
        //        {
        //            if (prodrow.Cells["Temp_ProductID"].Value != null && prodrow.Cells["Temp_ProductID"].Value.ToString() != string.Empty)
        //            {
        //                _DNStock.ProductID = prodrow.Cells["Temp_ProductID"].Value.ToString();
        //                EcoMartCache.RefreshProductData(_DNStock.ProductID);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //        returnVal = false;
        //    }
        //    return returnVal;
        //}
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
        private bool AddPreviousStock()
        {
            bool returnVal = false;

            try
            {
                foreach (DataGridViewRow prodrow in dgtemp.Rows)
                {
                    if (prodrow.Cells["Temp_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Temp_Quantity"].Value) > 0 && prodrow.Cells["Temp_Code"].Value.ToString().Trim() != "")
                    {
                        _DNStock.StockID = prodrow.Cells["Temp_StockID"].Value.ToString();
                        _DNStock.ProductID = Convert.ToInt32(prodrow.Cells["Temp_ProductID"].Value.ToString());
                        _DNStock.Batchno = prodrow.Cells["Temp_BatchNumber"].Value.ToString();
                        _DNStock.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _DNStock.Quantity = Convert.ToInt32(prodrow.Cells["Temp_Quantity"].Value);
                        _DNStock.SchemeQuanity = Convert.ToInt32(prodrow.Cells["Temp_ScmQuantity"].Value);

                        _DNStock.PurchaseRate = Convert.ToDouble(prodrow.Cells["Temp_PurRate"].Value);
                        _DNStock.MRP = Convert.ToDouble(prodrow.Cells["Temp_MRP"].Value);
                        _DNStock.ReasonCode = prodrow.Cells["Temp_Code"].Value.ToString();
                        _DNStock.ProdLoosePack = Convert.ToInt32(prodrow.Cells["Temp_UOM"].Value.ToString());

                        string ifRecordFound = "";
                        ifRecordFound = _DNStock.CheckForStockIDInStockTable();
                        if (ifRecordFound == "Y")
                            returnVal = _DNStock.UpdateIntblStockAdd();
                        returnVal = _DNStock.UpdateDebitNoteStockInMasterProductAddFromTemp();
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

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
        }

        #endregion

        #region Construct columns

        private void ConstructMainColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC2.ColumnsMain.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                mpPVC2.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 220;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.ToolTipText = "Press TAB Key To Exit Product Grid";
                column.ReadOnly = false;
                mpPVC2.ColumnsMain.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                mpPVC2.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 35;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.Visible = true;
                column.ReadOnly = true;
                mpPVC2.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "VATPer";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsMain.Add(column);
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "  Batch No  ";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsMain.Add(column);
                //6          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.ToolTipText = "Expiry dd/mm";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 75;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpPVC2.ColumnsMain.Add(column);
                //8
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
                column.Width = 75;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC2.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "  Quantity  ";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ScmQuantity";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = true;
                column.ReadOnly = true;
                mpPVC2.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Discount";
                column.DataPropertyName = "DiscountPercent";
                column.HeaderText = "Less%";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = true;
                column.ReadOnly = true;
                mpPVC2.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Code";
                column.DataPropertyName = "ReasonCode";
                column.HeaderText = "CD";
                column.Width = 35;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.ToolTipText = "E=EXPIRY,B=BREAKAGE,U=UNKNOWN,G=GOODS RETURN";
                mpPVC2.ColumnsMain.Add(column);
                //13
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFAddVATInTradeRate";
                column.DataPropertyName = "AddVatInTradeRate";
                column.HeaderText = "Y/N";
                column.Width = 35;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                column.ToolTipText = "Y=Add Vat In Trade Rate";
                mpPVC2.ColumnsMain.Add(column);
                //14
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.ToolTipText = "N=Qty*Pur.Rate,S=Qty*MRP";
                column.Width = 85;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                mpPVC2.ColumnsMain.Add(column);

                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
                columnCheck.HeaderText = "Chk";
                columnCheck.Width = 50;
                columnCheck.Visible = false;
                mpPVC2.ColumnsMain.Add(columnCheck);
                //14            
                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_ClosingStock";
                //column.DataPropertyName = "ProdClosingStock";
                //column.Visible = false;
                //mpPVC1.ColumnsMain.Add(column);
                //15

                //15          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Visible = false;
                mpPVC2.ColumnsMain.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpPVC2.ColumnsMain.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                mpPVC2.ColumnsMain.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "DiscountAmount";
                column.Width = 50;
                column.Visible = false;
                mpPVC2.ColumnsMain.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.Visible = false;
                mpPVC2.ColumnsMain.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "LastStockID";
                column.Visible = false;
                mpPVC2.ColumnsMain.Add(column);
                //21
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                mpPVC2.ColumnsMain.Add(column);
                //22
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "Trade Rate";
                column.Visible = false;
                column.Width = 75;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC2.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VatAmount";
                column.HeaderText = "VAT Amount";
                column.Visible = false;
                column.Width = 75;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC2.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.Visible = false;
                mpPVC2.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructTempGridColumns()
        {
            DataGridViewTextBoxColumn column;
            dgtemp.Columns.Clear();

            try
            {
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_VATPer";
                column.DataPropertyName = "VATPer";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                column.Name = "Temp_PurRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Purchase Rate";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_ScmQuantity";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "Scm";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.Visible = false;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Code";
                column.DataPropertyName = "ReasonCode";
                column.HeaderText = "CD";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Temp_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 95;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                dgtemp.Columns.Add(column);

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
            DataGridViewTextBoxColumn column;
            mpPVC2.ColumnsProductList.Clear();

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                column.ReadOnly = true;
                mpPVC2.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.ReadOnly = true;
                mpPVC2.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfID";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT %";
                column.DefaultCellStyle.Format = "n2";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsProductList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "Cl Stk";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.ReadOnly = true;
                mpPVC2.ColumnsProductList.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void ConstructBatchGridColumns()
        {
            DataGridViewTextBoxColumn column;
            mpPVC2.ColumnsBatchList.Clear();

            try
            {                
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 30;
                mpPVC2.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batchno";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batchno";
                column.Width = 130;
                mpPVC2.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.Width = 70;
                mpPVC2.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPercent";
                column.DataPropertyName = "ProductVATPercent";
                column.HeaderText = "VAT(%)";
                column.Width = 60;
                column.Visible = true;
                mpPVC2.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.DefaultCellStyle.Format = "n2";
                column.Width = 100;
                mpPVC2.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "TradeRate";
                column.Visible = true;
                column.Width = 100;
                mpPVC2.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DistSaleRate";
                column.DataPropertyName = "DistributorSaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                column.Visible = false;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC2.ColumnsBatchList.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Closing Stock";
                column.Width = 65;
                mpPVC2.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStockpack";
                column.DataPropertyName = "ClosingStockPack";
                column.HeaderText = "Cl.STK";
                column.Visible = false;
                column.Width = 65;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                mpPVC2.ColumnsBatchList.Add(column);

                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PartyShortName";
                column.DataPropertyName = "AccShortName";
                column.HeaderText = "";
                column.Width = 45;
                mpPVC2.ColumnsBatchList.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Visible = false;
                column.Width = 65;
                mpPVC2.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                mpPVC2.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur Rate";
                column.DefaultCellStyle.Format = "n2";
                column.Visible = false;
                column.Width = 65;
                mpPVC2.ColumnsBatchList.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                mpPVC2.ColumnsBatchList.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        #endregion

        # region Internal methods

        private void InitialisempPVC1()
        {
            try
            {
                ConstructMainColumns();
                if (_Mode == OperationMode.Fifth)
                {
                    mpPVC2.ColumnsMain["Col_Check"].Visible = true;
                    mpPVC2.ColumnsMain["Col_Discount"].Visible = false;
                    mpPVC2.ColumnsMain["Col_VATPer"].Visible = false;
                }
                else
                {
                    mpPVC2.ColumnsMain["Col_Check"].Visible = false;
                    mpPVC2.ColumnsMain["Col_Discount"].Visible = true;
                    mpPVC2.ColumnsMain["Col_VATPer"].Visible = true;
                }
                ConstructProductSelectionListGridColumns();
                ConstructBatchGridColumns();
                DataTable dtable = new DataTable();
                dtable = _DNStock.ReadProductDetailsByID();
                mpPVC2.DataSourceMain = dtable;
                string tempFileName = General.GetDebitNoteStockTempFile();
                if (_Mode == OperationMode.Add && File.Exists(tempFileName))
                {
                    mpPVC2.DataSourceMain = null;
                    mpPVC2.Rows.Clear();

                    DataSet ds = new DataSet();
                    ds.ReadXml(tempFileName);
                    mpPVC2.DataSourceMain = ds.Tables[0];
                }


                mpPVC2.NewRowColumnName = "Col_IFAddVATInTradeRate";

                mpPVC2.BatchGridShowColumnName = "Col_UOM";
                mpPVC2.NumericColumnNames.Add("Col_Quantity");
                mpPVC2.DoubleColumnNames.Add("Col_VATPer");
                mpPVC2.DoubleColumnNames.Add("Col_MRP");
                mpPVC2.DoubleColumnNames.Add("Col_PurRate");
                mpPVC2.DoubleColumnNames.Add("Col_Amount");
                mpPVC2.DoubleColumnNames.Add("Col_Discount");
                mpPVC2.DoubleColumnNames.Add("Col_TradeRate");


                if (_Mode != OperationMode.View)
                {
                    Product prod = new Product();
                    dtable = prod.GetOverviewData();
                    mpPVC2.DataSourceProductList = dtable;
                }
                // mpPVC1.DataSourceProductList = General.ProductList;
                mpPVC2.Bind();
                sortcolumn();
                CalculateTotalAmount(-1);
                if (_Mode == OperationMode.Edit)
                {
                    if (mpPVC2.Rows.Count == 0 || !(General.CheckForBlankRowInTheGrid(mpPVC2)))
                    {
                        mpPVC2.Rows.Add();
                        txtVouchernumber.Enabled = false;
                        mpPVC2.ClearSelection();
                    }

                    mpPVC2.SetFocus(mpPVC2.Rows.Count, 1);
                    txtVouchernumber.Enabled = true;
                }
                if (_Mode == OperationMode.View || _Mode == OperationMode.Delete || _Mode == OperationMode.ReportView)
                {
                    mpPVC2.ColumnsMain[1].ReadOnly = true;
                    mpPVC2.ColumnsMain[2].ReadOnly = true;
                    mpPVC2.ColumnsMain[3].ReadOnly = true;
                    mpPVC2.ColumnsMain[4].ReadOnly = true;
                    mpPVC2.ColumnsMain[5].ReadOnly = true;
                    mpPVC2.ColumnsMain[6].ReadOnly = true;
                    mpPVC2.ColumnsMain[7].ReadOnly = true;
                    mpPVC2.ColumnsMain[8].ReadOnly = true;
                    mpPVC2.ColumnsMain[9].ReadOnly = true;
                    mpPVC2.ColumnsMain[10].ReadOnly = true;
                    mpPVC2.ColumnsMain[11].ReadOnly = true;
                    mpPVC2.ColumnsMain[12].ReadOnly = true;
                    mpPVC2.ColumnsMain[13].ReadOnly = true;
                    mpPVC2.ColumnsMain[14].ReadOnly = true;
                    mpPVC2.ColumnsMain[15].ReadOnly = true;
                    if (_Mode == OperationMode.ReportView)
                        tsBtnFifth.Visible = false;

                }
                if (_Mode != OperationMode.Edit)
                    mpPVC2.ClearSelection();
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
                tmptable = _DNStock.ReadProductDetailsByID();
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
                mpPVC2.ProductListGridWidth = 600;
                mpPVC2.BatchListGridWidth = 690;
                mpPVC2.ClearSelection();
                _DNStock.Id = "";
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtNarration.Clear();
                txtNoOfRows.Text = "";
                txtVouchernumber.Clear();
                txtVouType.Text = _DNStock.CrdbVouType;
                txtVoucherSeries.Text = General.ShopDetail.ShopVoucherSeries;
                _DNStock.CrdbVouSeries = txtVoucherSeries.Text.ToString();
                txtRoundAmount.Text = "";
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtVatInput12point5per.Text = "0.00";
                txtVatInput5per.Text = "0.00";
                txtTotalAmount.Text = "";
                txtRoundAmount.Text = "0.00";
                txtNetAmountForSelected.Text = "0.00";
                txtAdjustedIn.Text = string.Empty;
                mcbCreditor.SelectedID = "";
                _DNStock.StockID = "";
                cbRound.Checked = true;
                mcbCreditor.Focus();
                InitialisempPVC1();
                lblFooterMessage.Text = "";
                pnlMonthYear.Visible = false;
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
                mpPVC2.ClearSelection();
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
                DataTable dtable = _Party.GetDebtorCreditorList();
                mcbCreditor.FillData(dtable);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void sortcolumn()
        {
            mpPVC2.ColumnsMain["Col_ProductName"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpPVC2.ColumnsMain["Col_MRP"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpPVC2.ColumnsMain["Col_BatchNumber"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpPVC2.ColumnsMain["Col_Pack"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpPVC2.ColumnsMain["Col_Expiry"].SortMode = DataGridViewColumnSortMode.Automatic;
            mpPVC2.ColumnsMain["Col_UOM"].SortMode = DataGridViewColumnSortMode.Automatic;

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
                if (mcbCreditor.SeletedItem == null || mcbCreditor.SelectedID == string.Empty)
                {
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                }
                else
                {
                    txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                    txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
                    _DNStock.Name = mcbCreditor.SeletedItem.ItemData[2];
                    if (txtAddress1.Text != null)
                        _DNStock.CrdbAddress = txtAddress1.Text.ToString().Trim();
                    if (txtAddress2.Text != null)
                        _DNStock.CrdbAddress = txtAddress1.Text.ToString().Trim() + " " + txtAddress2.Text.ToString().Trim();
                    if (_Mode == OperationMode.Add)
                        btnAddExpiry.Enabled = true;

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1_OnProductSelected(DataGridViewRow productRow)
        {

            int mclstk = 0;
            string mifshortlisted = "";
            int mqty = 0;
            string mlastsalestockid = "";
            string mprodno = "";
            int mpakn = 0;


            double mvatper = 0;
            try
            {
                mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductID"].Value = productRow.Cells["Col_ProductID"].Value;
                mprodno = mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = productRow.Cells["Col_ProductName"].Value;

                mpPVC2.MainDataGridCurrentRow.Cells["Col_UOM"].Value = productRow.Cells["Col_UOM"].Value;
                mpakn = 1;
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_UOM"].Value != null && mpPVC2.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString() != "")
                    mpakn = Convert.ToInt32(mpPVC2.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
                mpPVC2.MainDataGridCurrentRow.Cells["Col_Pack"].Value = productRow.Cells["Col_ProdPack"].Value;
                int.TryParse(productRow.Cells["Col_ClosingStock"].Value.ToString(), out mclstk);

                if (productRow.Cells["Col_VATPer"].Value != null)
                    double.TryParse(productRow.Cells["Col_VATPer"].Value.ToString(), out mvatper);
                mpPVC2.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = mvatper.ToString("#0.00");
                mpPVC2.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = productRow.Cells["Col_ProdCompShortName"].Value;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchNumber"].ReadOnly = true;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_Expiry"].ReadOnly = true;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_MRP"].ReadOnly = true;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_PurRate"].ReadOnly = true;
                //  mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = true;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].ReadOnly = true;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = true;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].ReadOnly = true;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_Discount"].ReadOnly = true;


                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_StockID"].Value != null)
                    mlastsalestockid = mpPVC2.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString();
                // mpPVC1.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;

                mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductName"].ReadOnly = true;

                //mpPVC1.MainDataGridCurrentRow.Cells["Col_ClosingStock"].Value = productRow.Cells["Col_ClosingStock"].Value;


                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                    int.TryParse(mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString(), out mqty);

                int currentrow = mpPVC2.MainDataGridCurrentRow.Index;
                int totproductsale = 0;
                int saleqty = 0;
                //int currentoldstock = 0;
                int tempstock = 0;

                foreach (DataGridViewRow dr in mpPVC2.Rows)
                {
                    if (dr.Index != currentrow && dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_ProductID"].Value.ToString() == mprodno)
                    {
                        if (dr.Cells["Col_Quantity"].Value != null)
                            int.TryParse(dr.Cells["Col_Quantity"].Value.ToString(), out saleqty);
                        totproductsale += saleqty;

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
                            tempstock += saleqty;

                        }
                    }
                }

                mclstk = mclstk + tempstock;

                if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                    mclstk = Convert.ToInt32(Math.Floor(Convert.ToDouble(mclstk) / mpakn));
                _DNStock.CurrentProductStock = mclstk;


                _DNStock.Closingstock = mclstk;

                if (mclstk == 0 && mifshortlisted != "N" && mqty == 0)
                {
                    lblFooterMessage.Text = "No Stock";

                    mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductName"].Value = null;
                    mpPVC2.MainDataGridCurrentRow.Cells["Col_UOM"].Value = null;
                    mpPVC2.MainDataGridCurrentRow.Cells["Col_Pack"].Value = null;
                    mpPVC2.MainDataGridCurrentRow.Cells["Col_ProdCompShortName"].Value = null;
                    //   mpPVC1.MainDataGridCurrentRow.Cells["Col_Shelf"].Value = null;
                    mpPVC2.MainDataGridCurrentRow.Cells["Col_VATPer"].Value = null;

                    mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductName"].ReadOnly = false;
                    mpPVC2.MainDataGridCurrentRow.Cells[0].Value = null;
                    mpPVC2.RefreshMe();
                    mpPVC2.SetFocus(1);
                }
                else
                {
                    lblFooterMessage.Text = "Product Stock :" + mclstk.ToString() + " : ";
                }
                mpPVC2.RefreshMe();
                // mpPVC1.SetFocus(5);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpPVC1_OnBatchSelected(DataGridViewRow batchRow)
        {

            int mclosingstock = 0;
            int mclstk = 0;
            string mprodno = "";
            string mlastsalestockid = "";



            string newexpirydate = "";
            string newexpiry = "";
            double mmrp = 0;
            double mprate = 0;
            int mpakn = 0;
            double mtraderate = 0;

            try
            {

                mprodno = mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString();
                mpakn = 1;
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_UOM"].Value != null && mpPVC2.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString() != "")
                    mpakn = Convert.ToInt32(mpPVC2.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
                mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value = batchRow.Cells["Col_Batchno"].Value;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = batchRow.Cells["Col_Expiry"].Value;
                if (batchRow.Cells["Col_MRP"].Value != null)
                    double.TryParse(batchRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
                mpPVC2.MainDataGridCurrentRow.Cells["Col_MRP"].Value = mmrp.ToString("#0.00");
                if (batchRow.Cells["Col_PurchaseRate"].Value != null)
                    double.TryParse(batchRow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                if (batchRow.Cells["Col_TradeRate"].Value != null)
                    double.TryParse(batchRow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                //  trade rate as purchase rate
                mpPVC2.MainDataGridCurrentRow.Cells["Col_PurRate"].Value = mtraderate.ToString("#0.00");
                mpPVC2.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value = mtraderate.ToString("#0.00");
                mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchClosingStock"].Value = batchRow.Cells["Col_ClosingStock"].Value;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_StockID"].Value = batchRow.Cells["Col_StockID"].Value;
                newexpiry = mpPVC2.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString();

                if (batchRow.Cells["Col_SaleRate"].Value != null)
                    double.TryParse(batchRow.Cells["Col_SaleRate"].Value.ToString(), out mtraderate);
                mpPVC2.MainDataGridCurrentRow.Cells["Col_SaleRate"].Value = mtraderate.ToString("#0.00");

                //  mpPVC1.MainDataGridCurrentRow.Cells["Old_Quantity"].Value = "0";
                newexpirydate = General.GetValidExpiryDate(newexpiry);
                mpPVC2.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                //mpPVC1.MainDataGridCurrentRow.Cells["Col_Rate"].ReadOnly = false;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_PurRate"].ReadOnly = true;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].ReadOnly = false;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].ReadOnly = false;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = false;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].ReadOnly = false;
                mpPVC2.MainDataGridCurrentRow.Cells["Col_Discount"].ReadOnly = false;
                string CutStockID = string.Empty;
                if (batchRow.Cells["Col_ClosingStock"].Value != null && batchRow.Cells["Col_ClosingStock"].Value.ToString() != "")
                    mclosingstock = Convert.ToInt32(batchRow.Cells["Col_ClosingStock"].Value.ToString());

                if (batchRow.Cells["Col_StockID"].Value != null && batchRow.Cells["Col_StockID"].Value.ToString() != "")
                    CutStockID = Convert.ToString(batchRow.Cells["Col_StockID"].Value.ToString());

                lblFooterMessage.Text = "";

                int currentrow = mpPVC2.MainDataGridCurrentRow.Index;
                int totbatchsale = 0;
                int totproductsale = 0;
                int saleqty = 0;
                int tempproductstock = 0;
                int tempbatchstock = 0;
                mlastsalestockid = mpPVC2.MainDataGridCurrentRow.Cells["Col_StockID"].Value.ToString();
                foreach (DataGridViewRow dr in mpPVC2.Rows)
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
                        else if (dr.Cells["Col_StockID"].Value.ToString().Trim() == CutStockID && dr.Index == currentrow)
                        {
                            mclosingstock += Convert.ToInt32(dr.Cells["Col_Quantity"].Value);
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
                                tempbatchstock += saleqty;
                            }

                        }
                    }
                }

                mclstk = _DNStock.CurrentProductStock + tempproductstock;
                // not to add tempbatchstock 24/5/2015
                //mclosingstock = mclosingstock - totbatchsale;

                if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                    mclosingstock = Convert.ToInt32(Math.Floor(Convert.ToDouble(mclosingstock) / mpakn));


                lblFooterMessage.Text = "Product Stock :" + _DNStock.CurrentProductStock.ToString().Trim() + " : Batch Stock :" + mclosingstock.ToString().Trim();
                if (General.ShopDetail.ShopDebitNoteWithLooseQuantity == "Y")
                    lblRightSideFooterMsg.Text = "Enter Loose Quantity";
                else
                    lblRightSideFooterMsg.Text = "Enter strip Quantity"; ;

                _DNStock.CurrentProductStock = mclstk;
                _DNStock.CurrentBatchStock = mclosingstock;

                if (_DNStock.CurrentBatchStock <= 0)
                {
                    lblFooterMessage.Text = "Batch Stock Zero";
                    mpPVC2.SetFocus(1);
                }
                mpPVC2.SetFocus(9);
                mpPVC2.IsFocusSameCell = true;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1_OnRowDeleted(object sender, EventArgs e)
        {
            try
            {
                mpPVC1OnRowDeleted(sender);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1OnRowDeleted(object sender)
        {
            try
            {
                DataGridViewRow deletedrow = (DataGridViewRow)sender;
                int deletedrowindex = deletedrow.Index;
                CalculateTotalAmount(deletedrowindex);
                lblFooterMessage.Text = "";
                //if (!General.CheckForBlankRowInTheGrid(mpPVC2))
                //    mpPVC2.Rows.Add();
                //mpPVC2.SetFocus(1);
                UpdateTempStockOutDt();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void mpPVC1_OnTABKeyPressed(object sender, EventArgs e)
        {
            mpPVC2.ClearSelection();
            txtDiscPercent.Focus();
        }
        private void mpPVC2_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            General.ProdID = string.Empty;
        }
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mpPVC2.SetFocus(1);
            }
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }
        private void mpPVC1_OnCellValueChangeCommited(int colIndex)
        {
            int mqty = 0;
            int mscmqty = 0;
            try
            {
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null && mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString() != "")
                    mqty = Convert.ToInt32(mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].Value != null && mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].Value.ToString() != "")
                    mscmqty = Convert.ToInt32(mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].Value.ToString());
                if (colIndex == 10)  // old 9
                {
                    if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                        lblFooterMessage.Text = "Enter Quantity as per Pack";
                    if (Convert.ToInt32(mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) <= 0)
                    {
                        if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                            lblFooterMessage.Text = "Enter Quantity as per Pack";
                        mpPVC2.IsAllowNewRow = false;
                        mpPVC2.SetFocus(11); // old 10
                    }
                    else
                    {
                        int oldqty = 0;
                        int batchqty = 0;

                        if (mpPVC2.MainDataGridCurrentRow.Cells["Old_Quantity"].Value != null && mpPVC2.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString() != "")
                            int.TryParse(mpPVC2.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString(), out oldqty);

                        if (mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchClosingStock"].Value != null)
                            int.TryParse(mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchClosingStock"].Value.ToString(), out batchqty);

                        mqty = Convert.ToInt32(mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
                        //  totalqty = GetTotalQuantityForStockID();

                        if (mqty + mscmqty <= _DNStock.CurrentBatchStock + oldqty)
                        //if (totalqty <= (oldqty + batchqty))
                        {
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].ReadOnly = false;
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_Discount"].ReadOnly = false;
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = false;
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].ReadOnly = false;

                            if (mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].Value == null || mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].Value.ToString() == "")
                                mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].Value = "0";


                        }
                        else
                        {
                            lblFooterMessage.Text = "Not Enough Stock ";
                            mpPVC2.IsAllowNewRow = false;
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].ReadOnly = true;
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_Discount"].ReadOnly = true;
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = true;
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].ReadOnly = true;
                            mpPVC2.SetFocus(10); // old 9
                        }
                    }
                }

                if (colIndex == 10) // 9
                {
                    //int oldqty = 0;
                    //if (mpPVC2.MainDataGridCurrentRow.Cells["Old_Quantity"].Value != null && mpPVC2.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString() != "")
                    //    int.TryParse(mpPVC2.MainDataGridCurrentRow.Cells["Old_Quantity"].Value.ToString(), out oldqty);

                    //lblFooterMessage.Text = "";
                    //if (mqty + mscmqty <= _DNStock.CurrentBatchStock + oldqty)
                    //{
                    //    mpPVC2.MainDataGridCurrentRow.Cells["Col_Discount"].ReadOnly = false;
                    //    mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = false;
                    //    mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].ReadOnly = false;
                    //}
                    //else
                    //{
                    //    lblFooterMessage.Text = "Not Enough Stock ";
                    //    mpPVC2.IsAllowNewRow = false;
                    //    mpPVC2.MainDataGridCurrentRow.Cells["Col_ScmQuantity"].Value = "0";
                    //    mpPVC2.MainDataGridCurrentRow.Cells["Col_Discount"].ReadOnly = true;
                    //    mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].ReadOnly = true;
                    //    mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].ReadOnly = true;
                    //    mpPVC2.SetFocus(10); // old 9
                    //}
                    UpdateTempStockOutDt();
                }

                if (colIndex == 12) // old 11
                {
                    if (mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value == null || mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString() == "")
                        mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value = FixAccounts.SubTypeForExpiry;
                    lblFooterMessage.Text = "[B] Breakage  [E] Expiry [G] Goods Return/Saleable ";

                }

                if (colIndex == 13) // old 12
                {

                    if (mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == FixAccounts.SubTypeForExpiry || mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == FixAccounts.SubTypeForBreakage || mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == FixAccounts.SubTypeForGoodsReturn)
                    {
                        if (Convert.ToInt32(mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].Value) == 0)
                        {
                            lblFooterMessage.Text = "Enter Quantity as per Pack";
                            mpPVC2.SetFocus(10); // old 9

                        }
                        else if (Convert.ToDouble(mpPVC2.MainDataGridCurrentRow.Cells["Col_PurRate"].Value) == 0.00)
                        {
                            lblFooterMessage.Text = "Enter Purchase Rate";
                            mpPVC2.MainDataGridCurrentRow.Cells[7].ReadOnly = false;
                            mpPVC2.MainDataGridCurrentRow.Cells[8].ReadOnly = false;
                            mpPVC2.SetFocus(8); // old 7
                            mpPVC2.MainDataGridCurrentRow.Cells[7].ReadOnly = true;

                        }
                        else if (Convert.ToDouble(mpPVC2.MainDataGridCurrentRow.Cells["Col_MRP"].Value) == 0.00)
                        {
                            lblFooterMessage.Text = "Enter MRP";
                            mpPVC2.SetFocus(8); // old 7

                        }
                        else if (mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value == null || mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim() == "")
                        {
                            lblFooterMessage.Text = "Enter Batch Number";
                            mpPVC2.SetFocus(6); // old 5

                        }
                        else
                        {
                            bool retValue = false;
                            retValue = CheckForDuplicateProduct();
                            if (retValue == true)
                            {
                                lblFooterMessage.Text = "Product Already Entered";
                                mpPVC2.IsAllowNewRow = false;
                                mpPVC2.SetFocus(13); // old 12
                            }
                            else
                            {
                                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString() == FixAccounts.SubTypeForBreakage || mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString() == FixAccounts.SubTypeForGoodsReturn)
                                    mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].Value = "Y";
                                else
                                    mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].Value = "N";
                                lblFooterMessage.Text = "Y = Add VAT in TradeRate   N = Do Not Add VAT in TradeRate";
                            }

                        }
                        if (_Mode == OperationMode.Add)
                        {
                            DataTable dt = mpPVC2.GetGridData();
                            if (dt.Rows.Count > 0)
                                dt.WriteXml(General.GetDebitNoteStockTempFile());
                        }
                    }
                    else
                    {
                        lblFooterMessage.Text = "[B] Breakage  [E] Expiry [G] Goods Return/Saleable ";
                        mpPVC2.IsAllowNewRow = false;
                        mpPVC2.SetFocus(13); // old 12
                    }

                }

                if (colIndex == 14) // old 13
                {
                    if (mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].Value.ToString().Trim() == "Y" || mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].Value.ToString().Trim() == "N")
                    {
                        CalculateRowAmount();
                        mpPVC2.IsAllowNewRow = true;
                    }

                    else
                    {
                        lblFooterMessage.Text = "Y = Add VAT in TradeRate   N = Do Not Add VAT in TradeRate";
                        mpPVC2.IsAllowNewRow = false;
                        mpPVC2.SetFocus(14); // old 13
                    }
                }
                if (colIndex == 16) // old 15
                {
                    CalculateTotalsForSelected();

                }
                if (colIndex == 7) // old 6
                {
                    string newexpiry = "";
                    string newexpirydate = "";

                    int explength = mpPVC2.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim().Length;
                    if (mpPVC2.MainDataGridCurrentRow.Cells["Col_Expiry"].Value != null && mpPVC2.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString().Trim() != "" && explength > 0)
                    {
                        newexpiry = General.GetValidExpiry(mpPVC2.MainDataGridCurrentRow.Cells["Col_Expiry"].Value.ToString());
                        if (newexpiry != "")
                        {
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry.ToString();
                            newexpirydate = General.GetValidExpiryDate(newexpiry);
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_ExpiryDate"].Value = newexpirydate.ToString();
                            lblFooterMessage.Text = "";
                        }
                        else
                        {
                            mpPVC2.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                            lblFooterMessage.Text = " No Expiry ";
                        }

                    }
                    else
                    {
                        mpPVC2.MainDataGridCurrentRow.Cells["Col_Expiry"].Value = newexpiry;
                        lblFooterMessage.Text = " No Expiry ";
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateRowAmount()
        {
            double mamt = 0;
            double mqty = 0;
            double mmrp = 0;
            double mprate = 0;
            double mpakn = 0;
            double mdiscountper = 0;
            double mdiscountamt = 0;
            double mtraterate = 0;
            string mifAddVATinTradeRate = "";
            double mvatper = 0;
            double mvatamt = 0;
            double mvatamtqty = 0;

            if (mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].Value != null)
                mqty = Convert.ToDouble(mpPVC2.MainDataGridCurrentRow.Cells["Col_Quantity"].Value.ToString());
            if (mpPVC2.MainDataGridCurrentRow.Cells["Col_PurRate"].Value != null)
                mprate = Convert.ToDouble(mpPVC2.MainDataGridCurrentRow.Cells["Col_PurRate"].Value.ToString());

            if (mpPVC2.MainDataGridCurrentRow.Cells["Col_VATPer"].Value != null)
                mvatper = Convert.ToDouble(mpPVC2.MainDataGridCurrentRow.Cells["Col_VATPer"].Value.ToString());
            //if (mpPVC1.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value != null)
            //    mtraterate = Convert.ToDouble(mpPVC1.MainDataGridCurrentRow.Cells["Col_TradeRate"].Value.ToString());
            //if (mtraterate == 0)
            mtraterate = mprate;
            if (mpPVC2.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                mmrp = Convert.ToDouble(mpPVC2.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString());
            if (mpPVC2.MainDataGridCurrentRow.Cells["Col_UOM"].Value != null)
                mpakn = Convert.ToDouble(mpPVC2.MainDataGridCurrentRow.Cells["Col_UOM"].Value.ToString());
            if (mpPVC2.MainDataGridCurrentRow.Cells["Col_Discount"].Value != null)
                mdiscountper = Convert.ToDouble(mpPVC2.MainDataGridCurrentRow.Cells["Col_Discount"].Value.ToString());
            if (mpPVC2.MainDataGridCurrentRow.Cells["Col_IFAddVATInTradeRate"].Value != null)
                mifAddVATinTradeRate = mpPVC2.MainDataGridCurrentRow.Cells["Col_IfAddVATInTradeRate"].Value.ToString();
            if (mvatper > 0)
                mvatamt = Math.Round((mtraterate * mvatper) / 100, 4);

            if (mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == FixAccounts.SubTypeForBreakage || mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString().Trim() == FixAccounts.SubTypeForExpiry)
            {
                if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                    mamt = mqty * (mmrp);
                else
                    mamt = Math.Round((mqty * Math.Round(mmrp / mpakn, 4)), 2);
            }
            else
            {
                if (mifAddVATinTradeRate == "Y")
                {
                    mvatamtqty = Math.Round((mqty * mvatamt), 2);
                    if (General.ShopDetail.ShopDebitNoteWithLooseQuantity != "Y")
                        mamt = mqty * (mtraterate + mvatamt);
                    else
                        mamt = Math.Round((mqty * Math.Round((mtraterate + mvatamt) / mpakn, 4)), 2);
                    //  mamt = Math.Round(mqty * ((mtraterate + mvatamt)), 2);
                }
                else
                    mamt = Math.Round(mqty * ((mtraterate)), 2);
            }
            mdiscountamt = Math.Round(mamt * (mdiscountper / 100), 2);
            mamt = mamt - mdiscountamt;
            mpPVC2.MainDataGridCurrentRow.Cells["Col_DiscountAmount"].Value = mdiscountamt.ToString("#0.00");
            mpPVC2.MainDataGridCurrentRow.Cells["Col_Amount"].Value = Math.Round(mamt, 2).ToString("#0.00");
            mpPVC2.MainDataGridCurrentRow.Cells["Col_VatAmount"].Value = mvatamtqty.ToString("#0.00");
            lblFooterMessage.Text = "";
            CalculateTotalAmount(-1);
        }
        public bool CheckForDuplicateProduct()
        {
            bool retValue = false;
            double mmrp = 0;
            string mbtno = "";
            string mprodno = "";
            string mcode = "";
            int mcurrentindex = 0;
            try
            {
                mcurrentindex = mpPVC2.MainDataGridCurrentRow.Index;
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                    mprodno = mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                    mbtno = mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                    double.TryParse(mpPVC2.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value != null)
                    mcode = mpPVC2.MainDataGridCurrentRow.Cells["Col_Code"].Value.ToString();

                foreach (DataGridViewRow drp in mpPVC2.Rows)
                {
                    if (drp.Cells["Col_ProductID"].Value != null && drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
                          drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
                             drp.Cells["Col_MRP"].Value.ToString().Trim() == mmrp.ToString("#0.00") &&
                              drp.Cells["Col_Code"].Value.ToString() == mcode && drp.Index != mcurrentindex)
                    {
                        retValue = true;
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public int GetTotalQuantityForStockID()
        {
            int totalQty = 0;
            double mmrp = 0;
            int mqty = 0;
            string mbtno = "";
            string mprodno = "";
            int mcurrentindex = 0;

            try
            {
                mcurrentindex = mpPVC2.MainDataGridCurrentRow.Index;
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductID"].Value != null)
                    mprodno = mpPVC2.MainDataGridCurrentRow.Cells["Col_ProductID"].Value.ToString().Trim();
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value != null)
                    mbtno = mpPVC2.MainDataGridCurrentRow.Cells["Col_BatchNumber"].Value.ToString().Trim();
                if (mpPVC2.MainDataGridCurrentRow.Cells["Col_MRP"].Value != null)
                    double.TryParse(mpPVC2.MainDataGridCurrentRow.Cells["Col_MRP"].Value.ToString(), out mmrp);

                foreach (DataGridViewRow drp in mpPVC2.Rows)
                {
                    if (drp.Cells["Col_ProductID"].Value != null && drp.Cells["Col_ProductID"].Value.ToString().Trim() == mprodno &&
                          drp.Cells["Col_BatchNumber"].Value.ToString().Trim() == mbtno &&
                             drp.Cells["Col_MRP"].Value.ToString().Trim() == mmrp.ToString("#0.00"))
                    {
                        int.TryParse(drp.Cells["Col_Quantity"].Value.ToString(), out mqty);
                        totalQty += mqty;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return totalQty;
        }

        private void NumberofRows()
        {
            int itemCount = 0;
            try
            {
                foreach (DataGridViewRow dr in mpPVC2.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                    {
                        itemCount += 1;
                    }
                }
                txtNoOfRows.Text = itemCount.ToString().Trim();
                UpdateTempStockOutDt();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void CalculateTotalAmount(int deletedrowindex)
        {
            double TotalAmount = 0;
            double VatAmount5 = 0;
            double VatAmount12Point5 = 0;
            double TotalDiscount = 0;
            int itemCount = 0;

            try
            {
                foreach (DataGridViewRow dr in mpPVC2.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                    {
                        if (dr.Index != deletedrowindex)
                        {
                            // vat 5.5
                            if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 6)
                                VatAmount5 += Convert.ToDouble(dr.Cells["Col_VatAmount"].Value.ToString());
                            else
                            {
                                if (double.Parse(dr.Cells["Col_VATPer"].Value.ToString()) == 13.50)
                                    VatAmount12Point5 += Convert.ToDouble(dr.Cells["Col_VatAmount"].Value.ToString());
                            }
                            TotalAmount += double.Parse(dr.Cells["Col_Amount"].Value.ToString());
                            itemCount += 1;
                            if (dr.Cells["Col_DiscountAmount"].Value != null && dr.Cells["Col_DiscountAmount"].Value.ToString() != "")
                                TotalDiscount += double.Parse(dr.Cells["Col_DiscountAmount"].Value.ToString());
                        }
                    }

                }

                txtNoOfRows.Text = itemCount.ToString().Trim();
                txtVatInput12point5per.Text = Math.Round(VatAmount12Point5, 2).ToString("#0.00");
                txtVatInput5per.Text = Math.Round(VatAmount5, 2).ToString("#0.00");
                txtTotalAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");
                CalculateAllAmounts();
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
                if (mdblDiscPer > 0)
                {
                    mdblDiscAmount = Math.Round(((mdblAmount) * mdblDiscPer / 100), 2);
                    txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                }
                else
                {
                    mdblDiscAmount = 0;
                    txtDiscAmount.Text = "0.00";
                }
                if (mdblAmount < mdblDiscAmount)
                {
                    txtDiscAmount.Text = "0.00";
                    txtDiscPercent.Text = "0.00";
                    double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);
                    double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
                }

                txtAmount.Text = Math.Round(mdblAmount - mdblDiscAmount, 2).ToString("#0.00");


                CalculateRoundup();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void CalculateRoundup()
        {
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


        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateAllAmounts();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            CalculateAllAmounts();
                            MainToolStrip.Select();
                            tsBtnSave.Select();
                            //txtDiscAmount.Focus();
                        }
                        break;
                    case Keys.Down:
                        CalculateAllAmounts();
                        txtDiscAmount.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        CalculateAllAmounts();
                        CalculateRoundup();
                        break;
                    case Keys.Down:
                        CalculateRoundup();
                        break;
                    case Keys.Up:
                        txtDiscAmount.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        Calculatediscountpercent();
                        CalculateAllAmounts();
                        txtAmount.Focus();
                        break;
                    case Keys.Down:
                        Calculatediscountpercent();
                        CalculateAllAmounts();
                        txtAmount.Focus();
                        txtDiscAmount.Focus();
                        break;
                    case Keys.Up:
                        CalculateAllAmounts();
                        txtDiscPercent.Focus();
                        break;
                    case Keys.Left:
                        CalculateAllAmounts();
                        txtDiscPercent.Focus();
                        break;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            int vouno = 0;
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtVouchernumber.Text != null)
                    {
                        int.TryParse(txtVouchernumber.Text.ToString(), out vouno);
                        _DNStock.ReadDetailsByVouNumber(vouno);
                        FillSearchData(_DNStock.Id, "");
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtMonth_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtYear.Focus();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void txtYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtMonth.Text != "")
            {
                try
                {
                    btnOKExpiryClick();
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
            else if (e.KeyCode == Keys.Left)
                txtMonth.Focus();

            lblFooterMessage.Text = "";

        }



        private void btnAddExpiryClick()
        {
            pnlMonthYear.Visible = true;
            txtMonth.Focus();

        }
        private void btnOKExpiryClick()
        {
            pnlMonthYear.Visible = false;
            BindExpiryData();
            CalculateTotalAmount(-1);
            mpPVC2.Focus();
            AddRowinmpPVC1();
            //mpPVC1.Rows.Add();
            mpPVC2.Focus();

        }

        private void AddRowinmpPVC1()
        {
            bool addRow = false;
            foreach (DataGridViewRow dr in mpPVC2.Rows)
            {
                if ((dr.Cells[0].Value == null || dr.Cells[0].Value.ToString() == string.Empty))
                {
                    addRow = true;
                    break;
                }

            }
            if (addRow == false)
                mpPVC2.Rows.Add();

        }
        private void btnAddExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddExpiryClick();
        }
        private void BindExpiryData()
        {

            int noofrows = 0;
            string cmonth = "";
            string mdate = "";
            int.TryParse(txtMonth.Text, out _Month);
            int.TryParse(txtYear.Text, out _Year);
            if (_Month > 0 && _Year > 2000)
            {
                try
                {
                    cmonth = "00" + Convert.ToString(_Month).Trim();
                    int mlen = 0;
                    mlen = cmonth.Length;
                    if (mlen == 3)
                        cmonth = cmonth.Substring(1, 2);
                    else
                        cmonth = cmonth.Substring(2, 2);
                    mdate = Convert.ToString(_Year).Trim() + cmonth + "01";
                    _BindingSource = _DNStock.ReadExpiredStock(mdate, mcbCreditor.SelectedID);

                    mpPVC2.Rows.Clear();
                    int _rowIndex = 0;
                    double mamt = 0;
                    int muom = 0;
                    double mqty = 0;
                    double clstk = 0;
                    double mvatper = 0;
                    double mmrp = 0;
                    double mvatamt = 0;
                    double mvatamtqty = 0;
                    //   string mifAddVATinTradeRate = "N";
                    double mtraterate = 0;
                    if (_BindingSource != null && _BindingSource.Rows.Count > 0)
                    {
                        foreach (DataRow dr in _BindingSource.Rows)
                        {
                            _rowIndex = mpPVC2.Rows.Add();
                            mqty = 1;
                            muom = 1;
                            clstk = 0;
                            mvatper = 0;
                            mmrp = 0;
                            mvatamt = 0;
                            mvatamtqty = 0;
                            mtraterate = 0;
                            if (dr["VATPer"] != DBNull.Value)
                                mvatper = Convert.ToInt32(dr["VATPer"].ToString());
                            if (dr["MRP"] != DBNull.Value)
                                mmrp = Convert.ToDouble(dr["MRP"].ToString());
                            if (dr["TradeRate"] != DBNull.Value)
                                mtraterate = Convert.ToDouble(dr["TradeRate"].ToString());
                            if (dr["ClosingStock"] != DBNull.Value)
                                clstk = Convert.ToDouble(dr["ClosingStock"].ToString());
                            if (dr["ProdLoosePack"] != DBNull.Value)
                                muom = Convert.ToInt32(dr["ProdLoosePack"].ToString());
                            mqty = Convert.ToDouble(Math.Floor(clstk / muom));
                            if (mqty <= 0)
                                mqty = 1;



                            if (mvatper > 0)
                                mvatamt = Math.Round((mtraterate * mvatper) / 100, 4);
                            mamt = mqty * (mmrp);
                            //else
                            //{
                            //    if (mifAddVATinTradeRate == "Y")
                            //    {
                            //        mvatamtqty = Math.Round((mqty * mvatamt), 2);
                            //        mamt = Math.Round(mqty * ((mtraterate + mvatamt)), 2);
                            //    }
                            //    else
                            //        mamt = Math.Round(mqty * ((mtraterate)), 2);
                            //}

                            DataGridViewRow currentdr = mpPVC2.Rows[_rowIndex];
                            currentdr.Cells["Col_ProductID"].Value = dr["ProductID"].ToString();
                            currentdr.Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                            currentdr.Cells["Col_UOM"].Value = dr["ProdLoosePack"].ToString();
                            currentdr.Cells["Col_Pack"].Value = dr["ProdPack"].ToString();
                            currentdr.Cells["Col_VATPer"].Value = mvatper.ToString("#0.00");
                            currentdr.Cells["Col_VatAmount"].Value = mvatamtqty.ToString("#0.00");
                            currentdr.Cells["Col_BatchNumber"].Value = dr["BatchNumber"].ToString();
                            currentdr.Cells["Col_Expiry"].Value = dr["Expiry"].ToString();
                            currentdr.Cells["Col_MRP"].Value = mmrp.ToString("#0.00");
                            currentdr.Cells["Col_PurRate"].Value = dr["PurchaseRate"].ToString();
                            currentdr.Cells["Col_Quantity"].Value = mqty.ToString();
                            currentdr.Cells["Col_ScmQuantity"].Value = "0";
                            currentdr.Cells["Col_Discount"].Value = "0.00";
                            currentdr.Cells["Col_DiscountAmount"].Value = "0.00";
                            currentdr.Cells["Col_IFAddVATInTradeRate"].Value = "N";
                            currentdr.Cells["Col_Code"].Value = "E";
                            currentdr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");

                        }
                    }
                    else
                    {
                        lblFooterMessage.Text = "No Expiry Data";
                    }










                    noofrows = mpPVC2.Rows.Count;
                    txtNoOfRows.Text = noofrows.ToString();

                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
            }
        }



        private void btnAddExpiry_Click(object sender, EventArgs e)
        {
            btnAddExpiryClick();
        }

        private void btnOKExpiry_Click(object sender, EventArgs e)
        {
            btnOKExpiryClick();
        }

        private void btnOKExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKExpiryClick();
        }

        private void mpPVC1_OnCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                mpPVC2.CommitEdit(DataGridViewDataErrorContexts.Commit);
                CalculateTotalsForSelected();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void CalculateTotalsForSelected()
        {
            double mamountforselected = 0;
            double mvat5forselected = 0;
            double mvat12point5forselected = 0;
            double mnetamountforselected = 0;
            double mdiscountamountforselected = 0;
            double mdiscountper = 0;
            foreach (DataGridViewRow dr in mpPVC2.Rows)
            {
                if (dr.Cells["Col_Check"].Value != null && dr.Cells["Col_Amount"].Value != null)
                {
                    if (Convert.ToBoolean(dr.Cells["Col_Check"].Value.ToString()) == true)
                    {

                        mamountforselected += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        if (dr.Cells["Col_VATPer"].Value != null && dr.Cells["Col_VATAmount"].Value != null)
                        {
                            // vat 5.5
                            if (Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString()) == 6)
                            {
                                mvat5forselected += Convert.ToDouble(dr.Cells["Col_VATAmount"].Value.ToString());
                            }
                            else if (Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString()) == 13.50)
                            {
                                mvat12point5forselected += Convert.ToDouble(dr.Cells["Col_VATAmount"].Value.ToString());
                            }
                        }
                    }
                }
            }
            txtTotalAmountForSelected.Text = mamountforselected.ToString("#0.00");
            if (txtDiscPercent.Text != null && txtDiscPercent.Text.ToString() != string.Empty)
                mdiscountper = Convert.ToDouble(txtDiscPercent.Text.ToString());
            mdiscountamountforselected = Math.Round(mamountforselected * mdiscountper / 100, 2);
            txtDiscountAmountForSelected.Text = mdiscountamountforselected.ToString("#0.00");
            mnetamountforselected = mamountforselected - mdiscountamountforselected;
            txtNetAmountForSelected.Text = mnetamountforselected.ToString("#0.00");

        }
        private void mpPVC2_OnEscapeKeyPressed(object sender, EventArgs e)
        {
            Exit();
        }
        #endregion

        #region StockUpdate
        private DataTable CreateStockOutDt()
        {
            dtTempStockOut = CacheObject.Get<DataTable>("TempCounterSale");
            List<DataRow> rowsToDelete = new List<DataRow>();

            if (dtTempStockOut == null)
            {
                dtTempStockOut = new DataTable();
                dtTempStockOut.Columns.Add("ProductID", typeof(int));
                dtTempStockOut.Columns.Add("BatchID", typeof(string));
                dtTempStockOut.Columns.Add("QTY", typeof(int));
                dtTempStockOut.Columns.Add("SRate", typeof(decimal));
                dtTempStockOut.Columns.Add("FormName", typeof(string));
                dtTempStockOut.Columns.Add("StockID", typeof(int));
                //ProductID, batch,mrp
                CacheObject.Add(dtTempStockOut, "TempCounterSale");
                //DataRow[] drFormRows = dtTempPatientSale.Select("FormName");

            }
            foreach (DataRow item in dtTempStockOut.Rows)
            {
                if (string.Equals(item["FormName"], this.Name))
                {
                    rowsToDelete.Add(item);
                }
            }
            foreach (DataRow row in rowsToDelete)
            {
                dtTempStockOut.Rows.Remove(row);
            }
            return dtTempStockOut;

        }
        private void UpdateTempStockOutDt()
        {
            try
            {
                DataTable dtTempCounterSale = CreateStockOutDt();
                bool ShopDebitNoteWithLooseQuantity = false;

                if (General.ShopDetail.ShopDebitNoteWithLooseQuantity == "Y")
                    ShopDebitNoteWithLooseQuantity = true;
                //dtTempCounterSale.Clear();

                foreach (DataGridViewRow dr in mpPVC2.Rows)
                {
                    if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_SaleRate"].Value != null)
                    {
                        int PackQuant = 0;
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_UOM"].Value)) == false)
                        {
                            PackQuant = Convert.ToInt32(dr.Cells["Col_UOM"].Value);
                        }
                        if (dtTempCounterSale.Rows.Count > 0)
                        {
                            //  DataRow[] TempCounterSale = dtTempCounterSale.Select("ProductID=" + dr.Cells["Col_ProductID"].Value + " And BatchID=" + dr.Cells["Col_BatchNumber"].Value + " And SRate=" + dr.Cells["Col_SaleRate"].Value);
                            DataRow[] TempCounterSale = dtTempCounterSale.Select("ProductID='" + dr.Cells["Col_ProductID"].Value + "' And BatchID='" + dr.Cells["Col_BatchNumber"].Value + "' And SRate='" + dr.Cells["Col_SaleRate"].Value + "' And FormName='" + this.Name + "' And StockID='" + dr.Cells["Col_StockID"].Value + "'");
                            if (TempCounterSale.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_Quantity"].Value)))
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(TempCounterSale[0]["QTY"])))
                                    {
                                        if (ShopDebitNoteWithLooseQuantity == false)
                                            TempCounterSale[0]["QTY"] = Convert.ToInt32(TempCounterSale[0]["QTY"]) + Convert.ToInt32((Convert.ToInt32(dr.Cells["Col_Quantity"].Value) * PackQuant));
                                        else
                                            TempCounterSale[0]["QTY"] = Convert.ToInt32(TempCounterSale[0]["QTY"]) + Convert.ToInt32(dr.Cells["Col_Quantity"].Value);
                                    }
                                    else
                                    {
                                        TempCounterSale[0]["QTY"] = dr.Cells["Col_Quantity"].Value;
                                    }
                                }

                            }
                            else
                            {
                                DataRow drTempCounterSale = dtTempCounterSale.NewRow();

                                drTempCounterSale["StockID"] = dr.Cells["Col_StockID"].Value;
                                drTempCounterSale["ProductID"] = dr.Cells["Col_ProductID"].Value;
                                drTempCounterSale["BatchID"] = dr.Cells["Col_BatchNumber"].Value;

                                if (ShopDebitNoteWithLooseQuantity == false)
                                    drTempCounterSale["QTY"] = (Convert.ToInt32(dr.Cells["Col_Quantity"].Value) * PackQuant).ToString();
                                else
                                    drTempCounterSale["QTY"] = dr.Cells["Col_Quantity"].Value.ToString();

                                drTempCounterSale["FormName"] = this.Name;

                                if (dr.Cells["Col_SaleRate"].Value != null)
                                    drTempCounterSale["SRate"] = dr.Cells["Col_SaleRate"].Value;
                                else
                                    drTempCounterSale["SRate"] = DBNull.Value;

                                dtTempCounterSale.Rows.Add(drTempCounterSale);

                            }
                        }
                        else
                        {
                            DataRow drTempCounterSale = dtTempCounterSale.NewRow();
                            drTempCounterSale["StockID"] = dr.Cells["Col_StockID"].Value;
                            drTempCounterSale["ProductID"] = dr.Cells["Col_ProductID"].Value;
                            drTempCounterSale["BatchID"] = dr.Cells["Col_BatchNumber"].Value;

                            if (ShopDebitNoteWithLooseQuantity == false)
                                drTempCounterSale["QTY"] = (Convert.ToInt32(dr.Cells["Col_Quantity"].Value) * PackQuant).ToString();
                            else
                                drTempCounterSale["QTY"] = dr.Cells["Col_Quantity"].Value.ToString();

                            drTempCounterSale["FormName"] = this.Name;

                            if (dr.Cells["Col_SaleRate"].Value != null)
                                drTempCounterSale["SRate"] = dr.Cells["Col_SaleRate"].Value;
                            else
                                drTempCounterSale["SRate"] = DBNull.Value;

                            dtTempCounterSale.Rows.Add(drTempCounterSale);
                        }
                    }
                }
                CacheObject.Add(dtTempCounterSale, "TempCounterSale");
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.Message);
            }
        }

        #endregion StockUpdate        

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

        #endregion TimerDate

    }
}
