﻿using System;
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
    public partial class UclCreditNoteAmount : BaseControl
    {
        #region  Declaration
        private CreditNoteAmount _CNAmount;
        #endregion

        #region constructor
        public UclCreditNoteAmount()
        {
            InitializeComponent();
            _CNAmount = new CreditNoteAmount();
            SearchControl = new UclCreditNoteAmountSearch();
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
            _CNAmount.Initialise();
            ClearControls();
            return true;
        }

        public override bool Add()
        {
            bool retValue = base.Add();
            ClearData();
            pnlVouTypeNo.Enabled = true;
            pnlAmounts.Enabled = true;
            InitializeMainSubViewControl();
            AddToolTip();
            headerLabel1.Text = "CREDIT NOTE AMOUNT -> NEW";
            FillPartyCombo();
            mcbCreditor.Focus();

            return retValue;
        }
        public override bool Edit()
        {
            bool retValue = base.Edit();
            ClearData();      
            AddToolTip();
            headerLabel1.Text = "CREDIT NOTE AMOUNT -> EDIT";
            FillPartyCombo();
            txtVouchernumber.Focus();
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
            headerLabel1.Text = "CREDIT NOTE AMOUNT -> DELETE";
            FillPartyCombo();
            ClearData();         
            txtVouchernumber.Focus();
            return true;
        }

        public override bool ProcessDelete()
        {
            bool retValue = false;
            if (_CNAmount.Id != null && _CNAmount.Id != "")
            {
                if (_CNAmount.CanBeDeleted())
                {
                    LockTable.LockTablesForCreditDebitNoteAmount();
                    General.BeginTransaction();
                    retValue = _CNAmount.DeleteDetails();
                    if (retValue)
                        retValue = _CNAmount.DeleteParticulars();

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
            headerLabel1.Text = "CREDIT NOTE AMOUNT -> VIEW";
            FillPartyCombo();        
            txtVouchernumber.Focus();
            return retValue;
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
                            //////////_CNAmount.PrintRowPixel = 418;
                            //////////row = new PrintRow("End--------------", _CNAmount.PrintRowPixel, 15, fnt);
                            //////////PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;                          
                            PrintHeader(totalpages, rowcount, fnt);

                            rowcount = 0;
                        }
                        PrintRowPixel += 17;
                        rowcount += 1;
                        //row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString(), _CNAmount.PrintRowPixel, 15, fnt);
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
                row = new PrintRow(_CNAmount.CrdbNarration, PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(_CNAmount.CrdbAmountNet.ToString("#0.00"), PrintRowPixel, 700, fnt);
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
                string billtype = "Credit Note";               

                PrintRowPixel = PrintRowPixel + 37;
                row = new PrintRow(billtype, PrintRowPixel, 250, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(_CNAmount.CrdbVouNo.ToString(), PrintRowPixel, 400, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel = PrintRowPixel + 36;
                PrintRowPixel = PrintRowPixel + 34;  
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 520, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(_CNAmount.CrdbVouDate, PrintRowPixel, 680, fnt);
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
                    _CNAmount.CrdbId = mcbCreditor.SelectedID.Trim();
                _CNAmount.CrdbNarration = txtNarration.Text.Trim();
                _CNAmount.CrdbVouType = txtVouType.Text.Trim();
                if (txtVouchernumber.Text.Trim() != "" && txtVouchernumber.Text.Trim() != null)
                    _CNAmount.CrdbVouNo = Convert.ToInt32(txtVouchernumber.Text.Trim());
                _CNAmount.CrdbVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                double.TryParse(txtVatInput5per.Text, out mvat5per);
                _CNAmount.CrdbVat5 = mvat5per;
                double.TryParse(txtVatInput12point5per.Text, out mvat12point5per);
                _CNAmount.CrdbVat12point5 = mvat12point5per;
                double.TryParse(txtDiscPercent.Text, out mdiscper);
                _CNAmount.CrdbDiscPer = mdiscper;
                double.TryParse(txtDiscAmount.Text, out mdiscamount);
                _CNAmount.CrdbDiscAmt = mdiscamount;
                double.TryParse(txtBillAmount.Text, out mbillamount);
                _CNAmount.CrdbAmountNet = mbillamount;
                double.TryParse(txtAmount.Text, out mamount);
                _CNAmount.CrdbAmount = mamount;
                double.TryParse(txtRoundAmount.Text, out mround);
                _CNAmount.CrdbRoundAmount = mround;
                if (_Mode == OperationMode.Edit)
                    _CNAmount.IFEdit = "Y";
                _CNAmount.Validate();

                if (_CNAmount.IsValid)
                {
                    LockTable.LockTablesForCreditDebitNoteAmount();
                    if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                    {
                        General.BeginTransaction();
                        _CNAmount.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _CNAmount.CrdbVouNo = _CNAmount.GetAndUpdateCNNumber(General.ShopDetail.ShopVoucherSeries);
                        _CNAmount.CreatedBy = General.CurrentUser.Id;
                        _CNAmount.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _CNAmount.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        txtVouchernumber.Text = Convert.ToString(_CNAmount.CrdbVouNo);
                        retValue = _CNAmount.AddDetails();
                        _SavedID = _CNAmount.Id;
                        if (retValue)
                            retValue = saveparticulars();
                        if (retValue)
                            General.CommitTransaction();
                        else
                            General.RollbackTransaction();
                        LockTable.UnLockTables();
                        if (retValue)
                        {
                            string msgLine2 = _CNAmount.CrdbVouType + "  " + _CNAmount.CrdbVouNo.ToString("#0");
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
                        _CNAmount.ModifiedBy = General.CurrentUser.Id;
                        _CNAmount.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _CNAmount.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        retValue = _CNAmount.UpdateDetails();
                        _SavedID = _CNAmount.Id;
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
                            string msgLine2 = _CNAmount.CrdbVouType + "  " + _CNAmount.CrdbVouNo.ToString("#0");
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
                }
                else // Show Validation Messages
                {
                    LockTable.UnLockTables();
                    retValue = false;
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _CNAmount.ValidationMessages)
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
            if (ID != null && ID != "")
            {
                _CNAmount.Id = ID;
                _CNAmount.ReadDetailsByID();
                mcbCreditor.SelectedID = _CNAmount.AccountID;
                InitializeMainSubViewControl();
                NumberofRows();
                AddToolTip();
                mcbCreditor.Focus();
                txtNarration.Text = _CNAmount.CrdbNarration.ToString();
                txtVouType.Text = FixAccounts.VoucherTypeForCreditNoteAmount;
                txtVouchernumber.Text = _CNAmount.CrdbVouNo.ToString().Trim();
                if (_CNAmount.CrdbVat5 >= 0)
                    txtVatInput5per.Text = _CNAmount.CrdbVat5.ToString("#0.00");
                if (_CNAmount.CrdbVat12point5 >= 0)
                    txtVatInput12point5per.Text = _CNAmount.CrdbVat12point5.ToString("#0.00");
                if (_CNAmount.CrdbDiscPer >= 0)
                    txtDiscPercent.Text = _CNAmount.CrdbDiscPer.ToString("#0.00");
                if (_CNAmount.CrdbDiscAmt > 0)
                    txtDiscAmount.Text = _CNAmount.CrdbDiscAmt.ToString("#0.00");

                txtBillAmount.Text = _CNAmount.CrdbAmountNet.ToString("#0.00");
                txtNetAmount.Text = txtBillAmount.Text.ToString();
                txtAmount.Text = _CNAmount.CrdbAmount.ToString("#0.00");
                if (_CNAmount.CrdbRoundAmount != 0)
                    txtRoundAmount.Text = _CNAmount.CrdbRoundAmount.ToString("#0.00");
                txtTotalAmount.Text = _CNAmount.CrdbTotalAmount.ToString("#0.00");
                if (_CNAmount.CrdbRoundAmount != 0)
                    cbRound.Checked = true;
                DateTime mydate = new DateTime(Convert.ToInt32(_CNAmount.CrdbVouDate.Substring(0, 4)), Convert.ToInt32(_CNAmount.CrdbVouDate.Substring(4, 2)), Convert.ToInt32(_CNAmount.CrdbVouDate.Substring(6, 2)));
                datePickerBillDate.Value = mydate;
                CalculateAllAmounts();
                if (_Mode == OperationMode.Edit)
                {
                    mcbCreditor.Enabled = true;
                    txtNarration.Enabled = true;
                    mcbCreditor.Focus();
                }
            }
            return true;
        }

        #endregion IDetail Control

        #region Idetail members
        public override void ReFillData()
        {
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            if (keyPressed == Keys.N && modifier == Keys.Alt)
            {
                txtNarration.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                mcbCreditor.Focus();
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
            return retValue;
        }

        private bool DeletePreviousRows()
        {
            bool returnVal = false;
            try
            {
                returnVal = _CNAmount.DeletePreviousRecords();
            }
            catch { returnVal = false; }
            return returnVal;
        }

        private bool saveparticulars()
        {
            {
                bool returnVal = false;
                _CNAmount.SerialNumber = 0;
                try
                {
                    foreach (DataGridViewRow prodrow in mpProductGrid.Rows)
                    {
                        if (prodrow.Cells["Col_Particulars"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Amount"].Value) >= 0)
                        {
                            _CNAmount.SerialNumber += 1;
                            _CNAmount.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                            _CNAmount.Particulars = prodrow.Cells["Col_Particulars"].Value.ToString();
                            _CNAmount.Amount = Convert.ToDouble(prodrow.Cells["Col_Amount"].Value);
                            returnVal = _CNAmount.AddParticularsDetails();
                        }
                    }
                }
                catch { returnVal = false; }
                return returnVal;
            }
        }

        public DateTime ConvertStringToDate(string strdate)
        {
            DateTime dt;
            int theyear = System.Convert.ToInt32(strdate.Substring(0, 4));
            int themonth = System.Convert.ToInt32(strdate.Substring(4, 2));
            int theday = System.Convert.ToInt32(strdate.Substring(6, 2));
            dt = new DateTime(theyear, themonth, theday);
            return dt;
        }


        #endregion

        #region Other Private Methods

        private void NumberofRows()
        {
            int itemCount = 0;
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

            //if (_CNAmount.AccountID != mcbCreditor.SelectedID)
            //    retValue = true;
            //if (_CNAmount.CrdbNarration != txtNarration.Text.Trim())
            //    retValue = true;
            //double mdbl12point5 = 0;
            //double.TryParse(txtVatInput12point5per.Text, out mdbl12point5);
            //if (_CNAmount.CrdbVat12point5 != mdbl12point5)
            //    retValue = true;
            //double dbl5per = 0;
            //double.TryParse(txtVatInput5per.Text, out dbl5per);
            //if (_CNAmount.CrdbVat5 != dbl5per)
            //    retValue = true;            
            //double dblamount = 0;
            //double.TryParse(txtAmount.Text, out dblamount);
            //if (_CNAmount.CrdbAmount != dblamount)
            //    retValue = true;



            //return retValue;
        }



        private void ClearControls()
        {
            mcbCreditor.SelectedID = "";
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtBillAmount.Text = "0.00";
            txtNetAmount.Text = "0.00";
            txtDiscAmount.Text = "0.00";
            txtDiscPercent.Text = "0.00";
            txtNarration.Clear();
            txtAmount.Text = "0.00";
            txtNoOfRows.Text = "0";
            txtVatInput12point5per.Text = "0.00";
            txtVatInput5per.Text = "0.00";
            txtVouchernumber.Clear();
            txtVouType.Text = _CNAmount.CrdbVouType.ToString().Trim();
            cbRound.Checked = true;
            txtRoundAmount.Text = "0.00";
            txtTotalAmount.Text = "0.00";
            datePickerBillDate.ResetText();
            if (_Mode == OperationMode.Add)
            {
                mcbCreditor.Enabled = true;
                txtNarration.Enabled = true;
                pnlAmounts.Enabled = true;
                pnlVouTypeNo.Enabled = false;
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
            ConstructMainColumns();
        }

        private void ConstructMainColumns()
        {
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

        private void InitializeMainSubViewControl()
        {
            ConstructMainColumns();
            DataTable dtable = new DataTable();
            dtable = _CNAmount.ReadParticularsByID();

            mpProductGrid.DoubleColumnNames.Add("Col_Amount");
            mpProductGrid.DataSourceMain = dtable;
            mpProductGrid.Bind();

        }
        #endregion
       

        # region Other Private Methods

        private void FillPartyCombo()
        {
            mcbCreditor.SelectedID = null;
            mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress1" };
            mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "200" ,"0"};
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
            if (colIndex == 1) 
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
            double.TryParse(txtDiscAmount.Text, out mdblDiscAmount);
            double mdblDiscamountCalculated = 0;


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
        private void txtDiscAmount_KeyDown(object sender, KeyEventArgs e)
        {
            CalculateAllAmounts();
        }

        private void txtVatInput5per_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtVatInput12point5per.Focus();
                    break;
                case Keys.Down:
                    txtVatInput12point5per.Focus();
                    break;
            }

        }

        private void txtVatInput12point5per_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    txtDiscPercent.Focus();
                    break;
                case Keys.Down:
                    txtDiscPercent.Focus();
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
            int vouno = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (txtVouchernumber.Text != null)
                {
                    int.TryParse(txtVouchernumber.Text.ToString(), out vouno);
                    _CNAmount.ReadDetailsByVouNumber(vouno);
                    FillSearchData(_CNAmount.Id,"");
                }
            }
        }
        # endregion

        # region ToolTip
        private void AddToolTip()
        {
            ttToolTip.SetToolTip(txtVatInput12point5per, "Write VAT Amount for 12.5%");
        }

        # endregion

     
    }
}
