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
using EcoMart.Printing;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclDebitNoteAmount : BaseControl
    {
        #region  Declaration
        private DebitNoteAmount _DNAmount;
        #endregion

        #region constructor
        public UclDebitNoteAmount()
        {
            InitializeComponent();
            _DNAmount = new DebitNoteAmount();
            SearchControl = new UclDebitNoteAmountSearch();
        }
        #endregion

        # region IDetail Control
        public override void SetFocus()
        {
            if (_Mode == OperationMode.Add)
                mcbCreditor.Focus();
            else
                txtVouchernumber.Focus();
        }
        public override bool ClearData()
        {
            _DNAmount.Initialise();
            ClearControls();
            return true;
        }

        public override bool Add()
        {
            try
            {
                bool retValue = base.Add();
                ClearData();
                InitializeMainSubViewControl();
                AddToolTip();
                headerLabel1.Text = "DEBIT NOTE AMOUNT -> NEW";
                FillPartyCombo();
                mcbCreditor.Focus();
                return retValue;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
                return false;
            }
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();
            InitializeMainSubViewControl();
            AddToolTip();
            if (_Mode == OperationMode.Edit)
                headerLabel1.Text = "DEBIT NOTE AMOUNT -> EDIT";
            else
                headerLabel1.Text = "DEBIT NOTE AMOUNT -> SPLIT";
            FillPartyCombo();
            txtVouchernumber.Focus();
            tsBtnFifth.Visible = false;
            return retValue;
        }
        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            pnlVouTypeNo.Enabled = true;
            ClearData();
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            headerLabel1.Text = "DEBIT NOTE AMOUNT -> DELETE";
            ClearData();
            InitializeMainSubViewControl();
            AddToolTip();
            FillPartyCombo();
            txtVouchernumber.Focus();
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_DNAmount.Id != null && _DNAmount.Id != "")
            {
                if (_DNAmount.CanBeDeleted())
                {
                    LockTable.LockTablesForCreditDebitNoteAmount();
                    General.BeginTransaction();
                    retValue = _DNAmount.DeleteDetails();
                    if (retValue)
                        retValue = _DNAmount.DeleteParticulars();

                    if (retValue)
                        General.CommitTransaction();
                    else
                        General.RollbackTransaction();
                    LockTable.UnLockTables();
                    if (retValue)
                    {
                        retValue = true;
                        MessageBox.Show("Successfully Deleted", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    retValue = false;
                }
            }
            pnlVouTypeNo.Enabled = true;
            ClearData();
            return retValue;
        }

        public override bool View()
        {
            bool retValue = base.View();
            ClearData();
            InitializeMainSubViewControl();
            headerLabel1.Text = "DEBIT NOTE AMOUNT -> VIEW";
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
            txtVouchernumber.Focus();          
          //  GetLastRecord();
            return retValue;
        }
        private void GetLastRecord()
        {
            try
            {
                if (txtVouType.Text == null || txtVouType.Text.ToString().Trim() == string.Empty)
                {
                    _DNAmount.CrdbVouType = FixAccounts.VoucherTypeForDebitNoteAmount;
                }
                _DNAmount.GetLastRecord();
                FillSearchData(_DNAmount.Id, "");
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
                int totalrows = mpProductGrid.Rows.Count;
                PrintPageNumber = 0;
                int rowcount = 0;
                PrintRowPixel = 0;
                double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                int totalpages = Convert.ToInt32(totpages);
                PrintHeader(totalpages, rowcount, fnt);
                foreach (DataGridViewRow dr in mpProductGrid.Rows)
                {

                    if (dr.Cells["Col_Particulars"].Value != null)
                    {
                        if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                        {
                            PrintRowPixel = 325;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                            PrintBill.Rows.Add(row);
                            //////////_DNAmount.PrintRowPixel = 418;
                            //////////row = new PrintRow("End--------------", _DNAmount.PrintRowPixel, 15, fnt);
                            //////////PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintHeader(totalpages, rowcount, fnt);

                            rowcount = 0;
                        }
                        PrintRowPixel += 17;
                        rowcount += 1;
                        //row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString(), _DNAmount.PrintRowPixel, 15, fnt);
                        //PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_Particulars"].Value.ToString(), PrintRowPixel, 85, fnt);
                        PrintBill.Rows.Add(row);
                        double mamt = 0;
                        if (dr.Cells["Col_Amount"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 700, fnt);
                            PrintBill.Rows.Add(row);
                        }

                    }
                }
                PrintRowPixel = 325;
                row = new PrintRow(_DNAmount.CrdbNarration, PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(_DNAmount.CrdbAmountNet.ToString("#0.00"), PrintRowPixel, 700, fnt);
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
                string billtype = "Debit Note Amount";

                PrintRowPixel = PrintRowPixel + 37;
                row = new PrintRow(billtype, PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_DNAmount.CrdbVouNo.ToString(), PrintRowPixel, 400, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 36;

                PrintRowPixel = PrintRowPixel + 34;



                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 520, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_DNAmount.CrdbVouDate, PrintRowPixel, 680, fnt);
                PrintBill.Rows.Add(row);
                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow(page, PrintRowPixel, 750, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 34;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            Rowcount = 1;
            return Rowcount;
        }

        public override bool MoveFirst()
        {
            bool retValue = true;
            retValue = base.MoveFirst();
            DataRow dr = null;
            _DNAmount.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _DNAmount.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _DNAmount.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            dr = _DNAmount.GetFirstRecord();
            if (dr != null && dr["CRDBID"] != DBNull.Value)
            {
                _DNAmount.Id = dr["CRDBID"].ToString();
                FillSearchData(_DNAmount.Id, "");
            }
            return retValue;
        }
        public override bool MoveLast()
        {
            bool retValue = true;
            retValue = base.MoveLast();
            _DNAmount.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _DNAmount.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _DNAmount.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            GetLastRecord();
            return retValue;
        }
        public override bool MovePrevious()
        {
            bool retValue = true;
            retValue = base.MovePrevious();
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _DNAmount.CrdbVouType = txtVouType.Text.ToString();
            if (txtVoucherSeries.Text != null && txtVoucherSeries.Text != string.Empty)
                _DNAmount.CrdbVouSeries = txtVoucherSeries.Text.ToString();
            else
                _DNAmount.CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            for (int i = mvouno - 1; i > 0; i--)
            {
                _DNAmount.CrdbVouNo = i;
                dr = _DNAmount.ReadDetailsByVouNumber(_DNAmount.CrdbVouNo);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CRDBID"] != DBNull.Value)
            {
                _DNAmount.Id = dr["CRDBID"].ToString();
                FillSearchData(_DNAmount.Id, "");
            }
            return retValue;
        }
        public override bool MoveNext()
        {
            bool retValue = true;
            retValue = base.MoveNext();
            int lastvouno = _DNAmount.GetLastVoucherNumber(FixAccounts.VoucherTypeForDebitNoteAmount, txtVoucherSeries.Text.ToString());
            DataRow dr = null;
            int mvouno = Convert.ToInt32(txtVouchernumber.Text.ToString());
            _DNAmount.CrdbVouType = txtVouType.Text.ToString();
            for (int i = mvouno + 1; i <= lastvouno; i++)
            {
                _DNAmount.CrdbVouNo = i;
                dr = _DNAmount.ReadDetailsByVouNumber(i);
                if (dr != null)
                    break;
            }
            if (dr != null && dr["CRDBID"] != DBNull.Value)
            {
                _DNAmount.Id = dr["CRDBID"].ToString();
                FillSearchData(_DNAmount.Id, "");
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
                System.Text.StringBuilder _errorMessage;
                if (mcbCreditor.SelectedID != null)
                    _DNAmount.CrdbId = mcbCreditor.SelectedID.Trim();
                _DNAmount.CrdbNarration = txtNarration.Text.Trim();
                if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                    _DNAmount.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                _DNAmount.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                double.TryParse(txtVatInput5per.Text, out mvat5per);
                _DNAmount.CrdbVat5 = mvat5per;
                double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                _DNAmount.CrdbVat12point5 = mvat12point5per;
                double.TryParse(txtDiscPercent.Text, out mdiscper);
                _DNAmount.CrdbDiscPer = mdiscper;
                double.TryParse(txtDiscAmount.Text, out mdiscamount);
                _DNAmount.CrdbDiscAmt = mdiscamount;
                double.TryParse(txtBillAmount.Text, out mbillamount);
                _DNAmount.CrdbAmountNet = mbillamount;
                double.TryParse(txtAmount.Text, out mamount);
                _DNAmount.CrdbAmount = mamount;
                double.TryParse(txtRoundAmount.Text, out mround);
                _DNAmount.CrdbRoundAmount = mround;
                if (_Mode == OperationMode.Edit)
                    _DNAmount.IFEdit = "Y";
                _DNAmount.Validate();

                if (_DNAmount.IsValid)
                {
                    LockTable.LockTablesForCreditDebitNoteAmount();
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        General.BeginTransaction();
                        //_DNAmount.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _DNAmount.CrdbVouNo = _DNAmount.GetAndUpdateDNNumber(General.ShopDetail.ShopVoucherSeries);
                        txtVouchernumber.Text = Convert.ToString(_DNAmount.CrdbVouNo);
                        _DNAmount.CreatedBy = General.CurrentUser.Id;
                        _DNAmount.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _DNAmount.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        //retValue = _DNAmount.AddDetails();
                        _DNAmount.IntID = 0;
                        _DNAmount.IntID = _DNAmount.AddDetails();
                        //retValue = _CNAmount.AddDetails();
                        if (_DNAmount.IntID > 0)
                            retValue = true;
                        else
                            retValue = false;

                        _SavedID = _DNAmount.Id;

                        if (retValue)
                            retValue = saveparticulars();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            string msgLine2 = _DNAmount.CrdbVouType + "  " + _DNAmount.CrdbVouNo.ToString("#0");
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
                            PSDialogResult result = PSMessageBox.Show("Could not Add...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                            retValue = false;
                        }
                    }
                    else if (_Mode == OperationMode.Edit)
                    {
                        General.BeginTransaction();
                        _DNAmount.ModifiedBy = General.CurrentUser.Id;
                        _DNAmount.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _DNAmount.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _DNAmount.UpdateDetails();
                        _SavedID = _DNAmount.Id;
                        if (retValue)
                            retValue = DeletePreviousRows();
                        if (retValue)
                            retValue = saveparticulars();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            string msgLine2 = _DNAmount.CrdbVouType + "  " + _DNAmount.CrdbVouNo.ToString("#0");
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
                            _SavedID = _DNAmount.Id;
                            retValue = true;
                        }
                        else
                        {
                            PSDialogResult result = PSMessageBox.Show("Could not Add...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                            retValue = false;
                        }
                    }




                    else if (_Mode == OperationMode.Fifth)
                    {
                        General.BeginTransaction();
                        bool returnVal = false;
                        _DNAmount.SerialNumber = 0;
                        try
                        {
                            foreach (DataGridViewRow prodrow in mpProductGrid.Rows)
                            {
                                if (prodrow.Cells["Col_Particulars"].Value != null &&
                                  Convert.ToDouble(prodrow.Cells["Col_Amount"].Value) >= 0)
                                {
                                    _DNAmount.IDForSelected = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _DNAmount.CrdbVouNoForSelected = _DNAmount.GetAndUpdateDNNumber(General.ShopDetail.ShopVoucherSeries);
                                    _DNAmount.CreatedBy = General.CurrentUser.Id;
                                    _DNAmount.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                                    _DNAmount.CreatedTime = DateTime.Now.ToString("HH:mm:ss");

                                    _DNAmount.CrdbBillAmountForSelected = Math.Round(Convert.ToDouble(prodrow.Cells["Col_Amount"].Value.ToString()));

                                    _DNAmount.SerialNumber = 1;
                                    _DNAmount.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                                    _DNAmount.Particulars = prodrow.Cells["Col_Particulars"].Value.ToString();
                                    _DNAmount.IntID = 0;
                                    _DNAmount.IntID = _DNAmount.AddDetailsForSelected();
                                    if (_DNAmount.IntID > 0)
                                        retValue = true;
                                    else
                                        retValue = false;
                                    //retValue = _DNAmount.AddDetailsForSelected();
                                    returnVal = _DNAmount.AddParticularsDetailsForSelected();
                                }
                            }
                        }
                        catch { returnVal = false; }
                        retValue = DeletePreviousRows();
                        retValue = _DNAmount.DeleteDetails();

                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        return returnVal; 
                    }

                }
                else
                {
                    LockTable.UnLockTables();
                    retValue = false;
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _DNAmount.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            LockTable.UnLockTables();
            return retValue;
        }

        public override bool FillSearchData(string ID, string Vmode)
        {
            string ifAdjusted = "N";
            if (ID != null && ID != "")
            {
                _DNAmount.Id = ID;
                _DNAmount.ReadDetailsByID();
                mcbCreditor.SelectedID = _DNAmount.AccountID;

                if (_DNAmount.ClearVouNo > 0)
                {
                    ifAdjusted = "Y";
                    lblFooterMessage.Text = "Debit Note Cleared in Purchase Voucher:" + _DNAmount.ClearVouType + " " + _DNAmount.ClearVouNo.ToString();
                    
                }
                
                    FillData();
                    NumberofRows();
                    AddToolTip();
                    mcbCreditor.Focus();
                    txtNarration.Text = _DNAmount.CrdbNarration.ToString();
                    txtVouchernumber.Text = _DNAmount.CrdbVouNo.ToString().Trim();
                    //  _DNAmount.CrdbVouDate = datePickerBillDate.Text.Trim();
                    if (_DNAmount.CrdbVat5 >= 0)
                        txtVatInput5per.Text = _DNAmount.CrdbVat5.ToString("#0.00");
                    if (_DNAmount.CrdbVat12point5 >= 0)
                        txtVatInput12point5per.Text = _DNAmount.CrdbVat12point5.ToString("#0.00");
                    if (_DNAmount.CrdbDiscPer >= 0)
                        txtDiscPercent.Text = _DNAmount.CrdbDiscPer.ToString("#0.00");
                    if (_DNAmount.CrdbDiscAmt > 0)
                        txtDiscAmount.Text = _DNAmount.CrdbDiscAmt.ToString("#0.00");

                    txtBillAmount.Text = _DNAmount.CrdbAmountNet.ToString("#0.00");
                    txtNetAmount.Text = txtBillAmount.Text.ToString();
                    txtAmount.Text = _DNAmount.CrdbAmount.ToString("#0.00");
                    if (_DNAmount.CrdbRoundAmount != 0)
                        txtRoundAmount.Text = _DNAmount.CrdbRoundAmount.ToString("#0.00");
                    txtTotalAmount.Text = _DNAmount.CrdbTotalAmount.ToString("#0.00");
                    DateTime mydate = new DateTime(Convert.ToInt32(_DNAmount.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_DNAmount.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_DNAmount.CrdbVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    CalculateAllAmounts();
                    if (_Mode == OperationMode.Edit )
                    {
                        if (ifAdjusted != "Y")
                        {
                            pnlVouTypeNo.Enabled = true;
                            pnlAmounts.Enabled = true;
                            mcbCreditor.Enabled = true;
                            txtNarration.Enabled = true;
                            txtVouchernumber.Enabled = false;
                            txtVouchernumber.Enabled = true;
                            mcbCreditor.Select();
                            mcbCreditor.Focus();
                            
                        }
                        else
                        {
                            pnlVouTypeNo.Enabled = false;
                            pnlAmounts.Enabled = false;
                            mcbCreditor.Enabled = false;
                            txtNarration.Enabled = false;
                        }
                    }
                    else
                    {
                        mpProductGrid.Enabled = false;
                    }
                
            }
            return true;
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
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Escape)
            {
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
                returnVal = _DNAmount.DeletePreviousRecords();
            }
            catch { returnVal = false; }
            return returnVal;
        }

        private bool saveparticulars()
        {
            {
                bool returnVal = false;
                _DNAmount.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in mpProductGrid.Rows)
                    {
                        if (prodrow.Cells["Col_Particulars"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Amount"].Value) >= 0)
                        {
                            _DNAmount.SerialNumber += 1;
                            _DNAmount.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _DNAmount.Particulars = prodrow.Cells["Col_Particulars"].Value.ToString();
                            _DNAmount.Amount = Convert.ToDouble(prodrow.Cells["Col_Amount"].Value);
                            returnVal = _DNAmount.AddParticularsDetails();
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }

      

        #endregion

        #region Other Private Methods

        private void NumberofRows()
        {
            int itemCount = 0;

            //loop to calculate purchase amount by given customer id
            foreach (DataGridViewRow dr in mpProductGrid.Rows)
            {
                if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                {
                    itemCount += 1;
                }

            }

            txtNoOfRows.Text = itemCount.ToString().Trim();
        }

        public override bool IsDetailChanged()
        {
            if (_Mode == OperationMode.View || _Mode == OperationMode.ReportView)
                return false;
            else
                return true;
            //bool retValue = false;

            //if (_DNAmount.AccountID != mcbCreditor.SelectedID)
            //    retValue = true;
            //if (_DNAmount.CrdbNarration != txtNarration.Text.Trim())
            //    retValue = true;
            //double mdbl12point5 = 0;
            //double.TryParse(txtVatInput12point5per.Text, out mdbl12point5);
            //if (_DNAmount.CrdbVat12point5 != mdbl12point5)
            //    retValue = true;
            //double dbl5per = 0;
            //double.TryParse(txtVatInput5per.Text, out dbl5per);
            //if (_DNAmount.CrdbVat5 != dbl5per)
            //    retValue = true;
            ////  if (_DNAmount.CrdbVouDate != txtVouDate.Text.Trim()) 
            ////      retValue = true;
            //double dblamount = 0;
            //double.TryParse(txtAmount.Text, out dblamount);
            //if (_DNAmount.CrdbAmount != dblamount)
            //    retValue = true;
            //return retValue;
        }

        private void ClearControls()
        {
            try
            {
                mpProductGrid.Enabled = true;
                mcbCreditor.SelectedID = "";
                txtAddress1.Clear();
                txtAddress2.Clear();
                txtBillAmount.Text = "0.00";
                txtNetAmount.Text = "0.00";
                txtDiscAmount.Text = "0.00";
                txtDiscPercent.Text = "0.00";
                txtNarration.Clear();
                txtAmount.Text = "0.00";
                txtTotalAmount.Text = "0.00";
                txtNoOfRows.Text = "0";
                txtVatInput12point5per.Text = "0.00";
                txtVatInput5per.Text = "0.00";
                txtVouchernumber.Clear();
                txtVouType.Text = _DNAmount.CrdbVouType.ToString().Trim();
                cbRound.Checked = true;
                txtRoundAmount.Text = "0.00";
                datePickerBillDate.Value = General.ConvertStringToDateyyyyMMdd(DateTime.Today.ToString("yyyyMMdd"));
                //mpProductGrid.ColumnsMain.Clear();
                if (_Mode == OperationMode.Add)
                {
                    mcbCreditor.Enabled = true;
                    txtNarration.Enabled = true;
                    pnlAmounts.Enabled = true;
                    //  pnlVouTypeNo.Enabled = false;
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

        private void ConstructMainColumns()
        {
            try
            {
                if(mpProductGrid.Rows.Count > 0 )
                {
                    mpProductGrid.dgMainGrid.EndEdit();
                }
                mpProductGrid.ColumnsMain.Clear();
                DataGridViewTextBoxColumn column;


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Particulars";
                column.DataPropertyName = "Particulars";
                column.HeaderText = "Particulars";
                column.Visible = true;
                column.Width = 300;
                mpProductGrid.ColumnsMain.Add(column);



                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 150;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.MaxInputLength = 14;
                mpProductGrid.ColumnsMain.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void InitializeMainSubViewControl()
        {
            ConstructMainColumns();
            mpProductGrid.DoubleColumnNames.Add("Col_Amount");
            FillData();
        }

        private void FillData()
        {
            DataTable dtable = new DataTable();
            dtable = _DNAmount.ReadParticularsByID();
            mpProductGrid.DataSourceMain = dtable;
            mpProductGrid.Bind();
        }
        #endregion

        # region Other Private Methods

        private void FillPartyCombo()
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
        #endregion

        #region Events

        private void mcbCreditor_ItemAddedEdited(object sender, EventArgs e)
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

        private void mcbCreditor_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (mcbCreditor.SeletedItem != null)
            {
                txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
                txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
            }
        }

        private void mpProductGrid_OnCellValueChangeCommited(int colIndex)
        {

            if (colIndex == 1) //Amount Column
            {
                

                foreach (DataGridViewRow prodrow in mpProductGrid.Rows)
                {

                    if (prodrow.Cells["Col_Amount"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Amount"].Value) < 0)
                    {
                        prodrow.Cells["Col_Amount"].Value = 0;
                    }
                }               
            }

            if (colIndex == 1) //Amount Column
            {
                double totamt = 0;

                foreach (DataGridViewRow prodrow in mpProductGrid.Rows)
                {
                   
                    if (prodrow.Cells["Col_Amount"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Amount"].Value) > 0)
                    {
                        totamt = totamt + Convert.ToDouble(prodrow.Cells["Col_Amount"].Value);
                    }
                }
                txtAmount.Text = totamt.ToString("#0.00");
                txtNoOfRows.Text = mpProductGrid.Rows.Count.ToString();
                CalculateAllAmounts();
                txtNoOfRows.Text = mpProductGrid.Rows.Count.ToString();
            }
        }

        private void CalculateAmount()
        {
            double TotalAmount = 0;
            double VatAmount5 = 0;
            double VatAmount12Point5 = 0;
            int itemCount = 0;

            //loop to calculate purchase amount by given customer id
            foreach (DataGridViewRow dr in mpProductGrid.Rows)
            {
                if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                {

                    TotalAmount += double.Parse(dr.Cells["Col_Amount"].Value.ToString());
                    itemCount += 1;
                }

            }

            txtNoOfRows.Text = itemCount.ToString().Trim();
            txtVatInput12point5per.Text = Math.Round(VatAmount12Point5, 2).ToString("#0.00");
            txtVatInput5per.Text = Math.Round(VatAmount5, 2).ToString("#0.00");
            txtAmount.Text = Math.Round(TotalAmount, 2).ToString("#0.00");

            CalculateAllAmounts();
        }

        public void CalculateDiscount()
        {

            double mdblAmount;
            double.TryParse(txtAmount.Text, out mdblAmount);
            double mdblDiscPer;
            double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
            double mdblDiscAmount;
            double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);


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

        private void CalculateAllAmounts()
        {

            double mdblAmount;
            double.TryParse(txtAmount.Text, out mdblAmount);
            double mdblVatInput12point5per;
            double.TryParse(txtVatInput12point5per.Text, out mdblVatInput12point5per);
            double mdblVatInput5per;
            double.TryParse(txtVatInput5per.Text, out mdblVatInput5per);
            double mdblDiscPer;
            double.TryParse(txtDiscPercent.Text, out mdblDiscPer);
            double mdblDiscAmount;
            double mdblDiscamountCalculated = 0;
            double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);


            if (mdblDiscPer > 0)
            {
                mdblDiscamountCalculated = Math.Round(((mdblAmount) * mdblDiscPer / 100), 2);
                if (mdblDiscamountCalculated != mdblDiscAmount)
                {
                    mdblDiscPer = 0;
                    txtDiscPercent.Text = "0.00";
                }
            }

            if (txtAmount.Text != null)
                txtTotalAmount.Text = Math.Round(mdblAmount
                               - mdblDiscAmount, 2).ToString("#0.00");

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

        private void cbRound_CheckedChanged(object sender, EventArgs e)
        {
            CalculateAllAmounts();
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

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mpProductGrid.SetFocus(0);
            }
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
        private void mpProductGrid_OnTABKeyPressed(object sender, EventArgs e)
        {
            CalculateAmount();
            txtDiscPercent.Focus();
        }

        private void mpProductGrid_OnRowDeleted(object sender, EventArgs e)
        {
            CalculateAmount();
        }
        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtNarration.Focus();
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {

        }

        # endregion

        # region ToolTip
        private void AddToolTip()
        {
           
        }

        # endregion


    }
}
