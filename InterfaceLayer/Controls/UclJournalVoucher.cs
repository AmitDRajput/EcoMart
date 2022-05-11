using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.BusinessLayer;
using EcoMart.Common;
using EcoMart.InterfaceLayer.Classes;
using EcoMart.Printing;
using PrintDataGrid;

namespace EcoMart.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclJournalVoucher : BaseControl
    {
        #region Declaration
        private JournalVoucher _JournalVoucher;
        #endregion

        public UclJournalVoucher()
        {
            InitializeComponent();
            _JournalVoucher = new JournalVoucher();
            SearchControl = new UclJournalVoucherSearch();
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
                column.HeaderText = "Particulars";
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
        private void ClearControls()
        {
            try
            {
                txtNarration.Clear();
                txtVouchernumber.Clear();
                txtVouType.Text = FixAccounts.VoucherTypeForJournalVoucher;
                txtVoucherSeries.Text = General.ShopDetail.ShopVoucherSeries;
                txtVouchernumber.Clear();
                if (mpMainSubViewControl1.Rows.Count > 0)
                    mpMainSubViewControl1.Rows.Clear();
                txtTotalCredit.Text = "0.00";
                txtTotalDebit.Text = "0.00";
                txtNoOfRows.Text = "0";
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
                _JournalVoucher.Initialise();
                ClearControls();
                txtNarration.Focus();
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
                headerLabel1.Text = "JOURNAL VOUCHER -> NEW";
                mpMainSubViewControl1.Enabled = true;
                pnlNameAddress.Enabled = true;
                pnlVou.Enabled = true;
                txtNarration.Focus();
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
                headerLabel1.Text = "JOURNAL VOUCHER -> EDIT";
                InitialisempPVC1();
                pnlNameAddress.Enabled = true;
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
                if (_JournalVoucher.Id != null && _JournalVoucher.Id != "")
                {
                    LockTable.LockTablesForCashBankExpenses();

                    if (_JournalVoucher.CanBeDeleted())
                    {
                        General.BeginTransaction();
                        retValue = _JournalVoucher.DeleteDetails();
                        if (retValue)
                            //retValue = _JournalVoucher.DeletePreviousRecords();
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
            PrintData();
            ClearData();
            return retValue;
        }

        private void PrintData()
        {

            try
            {
                PrintFactory.SendReverseLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.ReverseLineFeed);
                //PrintBill.Rows.Clear();
                Font fnt = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = 0;
                PrintPageNumber = 0;
                int rowcount = 0;
                double totpages = 0;
                int totalpages = 0;
                PrintRowPixel = 0;
                DataTable dtable = new DataTable();
                
                PrintRow row;
                PrintBill.Rows.Clear();
                List<DataGridViewRow> rowCollection = new List<DataGridViewRow>();
                foreach (DataGridViewRow prodrow in mpMainSubViewControl1.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null)
                    {
                        rowCollection.Add(prodrow);
                    }
                }
                totalrows = 0;
                PrintPageNumber = 0;
                rowcount = 0;
                totpages = 0;
                totalpages = 0;
                PrintRowPixel = 0;
                totalrows = rowCollection.Count();
                totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / General.CurrentSetting.MsetNumberOfLinesSaleBill));
                totalpages = Convert.ToInt32(totpages);
                PrintHeader(totalpages, rowcount, fnt);
                foreach (DataGridViewRow prodrow in rowCollection)
                {
                    if (rowcount > General.CurrentSetting.MsetNumberOfLinesSaleBill)
                    {
                        PrintRowPixel += 17;
                        row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                        PrintBill.Rows.Add(row);
                        PrintBill.Print_Bill(600, 400);
                        PrintBill.Rows.Clear();
                        PrintRowPixel = 0;
                        PrintHeader(totalpages, rowcount, fnt);
                        rowcount = 0;
                    }
                    else
                    {
                        PrintRowPixel += 17;
                        rowcount += 1;
                        row = new PrintRow(prodrow.Cells["Col_AccountName"].Value.ToString(), PrintRowPixel, 10, fnt);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(prodrow.Cells["Col_Address"].Value.ToString().PadLeft(6), PrintRowPixel, 130, fnt);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(prodrow.Cells["Col_Debit"].Value.ToString(), PrintRowPixel, 360, fnt);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(prodrow.Cells["Col_Credit"].Value.ToString(), PrintRowPixel, 420, fnt);
                        PrintBill.Rows.Add(row);
                    }

                }
                PrintFooter(fnt);
                PrintBill.Print_Bill(600, 400);
                rowCollection = new List<DataGridViewRow>();
                PrintFactory.SendLineFeed(General.PrintSettings.SaleBillPrintSettingsPrePrintedPaper.GeneralSettings.LineFeed);
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

                PrintRowPixel = PrintRowPixel + 37;

                row = new PrintRow(General.ShopDetail.ShopName, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Journal Voucher", PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                row = new PrintRow(General.ShopDetail.ShopAddress1.Trim(), PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Time :" + DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                string voudate = DateTime.Now.ToShortDateString();
                row = new PrintRow("Date :" + voudate, PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;
                
                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow("          Page :" + page, PrintRowPixel, 340, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 14;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 14;
                row = new PrintRow("ACCOUNT                          ADDRESS                                      DEBIT                CREDIT ", PrintRowPixel, 15, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 14;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            Rowcount = 1;
            return PrintRowPixel;

        }

        private int PrintFooter(Font fnt)
        {
            try
            {
                PrintRow row;
                PrintRowPixel = 305;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 21;

                row = new PrintRow("Drug Lic.No.: " + General.ShopDetail.ShopDLN, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("For: " + General.ShopDetail.ShopName, PrintRowPixel, 370, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 14;
                row = new PrintRow(FixAccounts.DashLine60Normal, PrintRowPixel, 10, fnt);
                PrintBill.Rows.Add(row);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

            return PrintRowPixel;
        }

        private void InitialisempPVC1()
        {
            try
            {
                ConstructMainColumns();
                ConstructSubColumns();
                mpMainSubViewControl1.DoubleColumnNames.Add("Col_Debit");
                mpMainSubViewControl1.DoubleColumnNames.Add("Col_Credit");

                DataTable dtable = new DataTable();
                dtable = _JournalVoucher.ReadJVDetailsByID(_JournalVoucher.JVVoucherID);
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

        private void mpMainSubViewControl1_OnCellValueChangeCommited(int colIndex)
        {
            try
            {
                //if(colIndex == 1 || colIndex == 2)
                //{
                //    mpMainSubViewControl1.MainDataGridCurrentRow.Cells[3].Value = 0;
                //    mpMainSubViewControl1.MainDataGridCurrentRow.Cells[4].Value = 0;
                //}
                if (colIndex == 3 || colIndex == 4)
                {
                    //if (Convert.ToDouble(mpMainSubViewControl1.MainDataGridCurrentRow.Cells[3].Value) > 0)
                    //    mpMainSubViewControl1.MainDataGridCurrentRow.Cells[4].Value = 0;
                    CalculateTotals();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void mpMainSubViewControl1_OnDetailsFilled(DataGridViewRow selectedRow)
        {
            string mprodID = "";
            int mrowindex = 0;
            int mcindex = 0;
            try
            {
                _JournalVoucher.DuplicateAccount = false;
                if (mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ID"].Value != null)
                {
                    mprodID = mpMainSubViewControl1.MainDataGridCurrentRow.Cells["Col_ID"].Value.ToString();
                    mrowindex = mpMainSubViewControl1.MainDataGridCurrentRow.Index;
                }
                foreach (DataGridViewRow prodrow in mpMainSubViewControl1.Rows)
                {
                    if (prodrow.Cells["Col_ID"].Value != null)
                    {
                        _JournalVoucher.RAccountID = prodrow.Cells["Col_ID"].Value.ToString();
                        mcindex = prodrow.Index;
                        if (_JournalVoucher.RAccountID == mprodID && mrowindex != mcindex)
                        {
                            _JournalVoucher.DuplicateAccount = true;
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

        private void mpMainSubViewControl1_OnRowDeleted(object sender, EventArgs e)
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

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mpMainSubViewControl1.Focus();
                mpMainSubViewControl1.SetFocus(0, 1);
            }
        }
        public override bool Save()
        {
            return SaveData(false);
        }
        private bool SaveData(bool printData)
        {
            bool retValue = false;
            try
            {
                if (Convert.ToDouble(txtTotalCredit.Text) == Convert.ToDouble(txtTotalDebit.Text))
                {
                    System.Text.StringBuilder _errorMessage;
                    _JournalVoucher.Validate();
                    if (_JournalVoucher.IsValid)
                    {
                        if (_Mode == OperationMode.Add || _Mode == OperationMode.OpenAsChild)
                        {
                            General.BeginTransaction();
                            retValue = SaveDetailsInVoucherJV();
                            if (retValue)
                                SaveDatainTransac();
                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                        }
                        else if (_Mode == OperationMode.Edit)
                        {

                            General.BeginTransaction();
                            retValue = UpdateDetailsInVoucherJV();
                            if (retValue)
                                UpdateDatainTransac();
                            if (retValue)
                                General.CommitTransaction();
                            else
                                General.RollbackTransaction();
                            LockTable.UnLockTables();
                        }
                        else
                        {
                            LockTable.UnLockTables();
                            _errorMessage = new System.Text.StringBuilder();
                            _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                            foreach (string _message in _JournalVoucher.ValidationMessages)
                            {
                                _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                            }
                            retValue = false;
                            MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        if (retValue)
                        {
                            string msgLine2 = _JournalVoucher.JVVouType + "  " + _JournalVoucher.JVVouNo.ToString("#0");
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
                            PSDialogResult result = PSMessageBox.Show("Could not Update...", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                            retValue = false;
                        }
                    }
                }
                else
                {
                    PSDialogResult result = PSMessageBox.Show("Debit and Credit Value Mismatches.", "Error", General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Error, PSMessageBoxButtons.None);
                    retValue = false;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            LockTable.UnLockTables();
            return retValue;
        }

        private bool SaveDatainTransac()
        {
            bool retValue = false;

            try
            {
                foreach (DataGridViewRow drow in mpMainSubViewControl1.Rows)
                {
                    if (drow.Cells["Col_ID"].Value != null && drow.Cells["Col_ID"].Value.ToString() != "")
                    {
                        if (Convert.ToDouble(drow.Cells["Col_Debit"].Value) > 0 && _JournalVoucher.FirstDBAccID == "")
                            _JournalVoucher.FirstDBAccID = drow.Cells["Col_ID"].Value.ToString();

                        if (Convert.ToDouble(drow.Cells["Col_Credit"].Value) > 0 && _JournalVoucher.FirstCRAccID == "")
                            _JournalVoucher.FirstCRAccID = drow.Cells["Col_ID"].Value.ToString();
                    }
                }

                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        _JournalVoucher.TransacID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _JournalVoucher.JVVouSeries = General.ShopDetail.ShopVoucherSeries;
                        _JournalVoucher.JVAccountID = dr.Cells["Col_ID"].Value.ToString();

                        if (dr.Cells["Col_Debit"].Value != null)
                            _JournalVoucher.JVDebit = Convert.ToDouble(dr.Cells["Col_Debit"].Value);
                        if (dr.Cells["Col_Credit"].Value != null)
                            _JournalVoucher.JVCredit = Convert.ToDouble(dr.Cells["Col_Credit"].Value);

                        if ((Convert.ToDouble(dr.Cells["Col_Credit"].Value) > 0))
                            _JournalVoucher.AccAccountID = _JournalVoucher.FirstDBAccID;
                        if ((Convert.ToDouble(dr.Cells["Col_Debit"].Value) > 0))
                            _JournalVoucher.AccAccountID = _JournalVoucher.FirstCRAccID;

                        _JournalVoucher.JVVouType = FixAccounts.VoucherTypeForJournalVoucher;

                        //_JournalVoucher.JVVouNo = _JournalVoucher.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);
                        _JournalVoucher.CreatedBy = General.CurrentUser.Id;
                        _JournalVoucher.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _JournalVoucher.CreatedTime = DateTime.Now.ToString("HH:mm:ss");

                        retValue = _JournalVoucher.AddJVDetailsIntblTrnac();
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

        private bool UpdateDatainTransac()
        {
            bool retValue = false;
            try
            {
                _JournalVoucher.DeleteJVFormtblTrans();
                foreach (DataGridViewRow drow in mpMainSubViewControl1.Rows)
                {
                    if (drow.Cells["Col_ID"].Value != null && drow.Cells["Col_ID"].Value.ToString() != "")
                    {
                        if (Convert.ToDouble(drow.Cells["Col_Debit"].Value) > 0 && _JournalVoucher.FirstDBAccID == "")
                            _JournalVoucher.FirstDBAccID = drow.Cells["Col_ID"].Value.ToString();

                        if (Convert.ToDouble(drow.Cells["Col_Credit"].Value) > 0 && _JournalVoucher.FirstCRAccID == "")
                            _JournalVoucher.FirstCRAccID = drow.Cells["Col_ID"].Value.ToString();
                    }
                }

                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        _JournalVoucher.TransacID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _JournalVoucher.JVVouSeries = General.ShopDetail.ShopVoucherSeries;
                        _JournalVoucher.JVAccountID = dr.Cells["Col_ID"].Value.ToString();

                        if (dr.Cells["Col_Debit"].Value != null)
                            _JournalVoucher.JVDebit = Convert.ToDouble(dr.Cells["Col_Debit"].Value);
                        if (dr.Cells["Col_Credit"].Value != null)
                            _JournalVoucher.JVCredit = Convert.ToDouble(dr.Cells["Col_Credit"].Value);

                        if ((Convert.ToDouble(dr.Cells["Col_Credit"].Value) > 0))
                            _JournalVoucher.AccAccountID = _JournalVoucher.FirstDBAccID;
                        if ((Convert.ToDouble(dr.Cells["Col_Debit"].Value) > 0))
                            _JournalVoucher.AccAccountID = _JournalVoucher.FirstCRAccID;

                        _JournalVoucher.JVVouType = FixAccounts.VoucherTypeForJournalVoucher;

                        _JournalVoucher.CreatedBy = General.CurrentUser.Id;
                        _JournalVoucher.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _JournalVoucher.CreatedTime = DateTime.Now.ToString("HH:mm:ss");

                        _JournalVoucher.ModifiedBy = General.CurrentUser.Id;
                        _JournalVoucher.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _JournalVoucher.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");

                        retValue = _JournalVoucher.AddJVDetailsIntblTrnac();
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

        private bool SaveDetailsInVoucherJV()
        {
            //double mdebit;
            //double mcredit;
            //string macno;
            bool retValue = false;
            try
            {
                _JournalVoucher.SerialNumber = 0;
                _JournalVoucher.JVVoucherID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                _JournalVoucher.JVVouNo = _JournalVoucher.GetAndUpdateJVNumber(General.ShopDetail.ShopVoucherSeries);

                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {

                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        _JournalVoucher.SerialNumber += 1;
                        _JournalVoucher.JVVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                        if (txtNarration.Text != string.Empty)
                            _JournalVoucher.JVNarration = txtNarration.Text.ToString();
                        _JournalVoucher.ID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _JournalVoucher.CreatedBy = General.CurrentUser.Id;
                        _JournalVoucher.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _JournalVoucher.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _JournalVoucher.JVVouType = FixAccounts.VoucherTypeForJournalVoucher;
                        _JournalVoucher.JVVouSeries = General.ShopDetail.ShopVoucherSeries;
                        _JournalVoucher.JVAccountID = dr.Cells["Col_ID"].Value.ToString();

                        if (dr.Cells["Col_Debit"].Value != null)
                            _JournalVoucher.JVDebit = Convert.ToDouble(dr.Cells["Col_Debit"].Value);
                        if (dr.Cells["Col_Credit"].Value != null)
                            _JournalVoucher.JVCredit = Convert.ToDouble(dr.Cells["Col_Credit"].Value);
                        retValue = _JournalVoucher.AddDetails();
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

        private bool UpdateDetailsInVoucherJV()
        {
            try
            {
                bool retValue = false;
                _JournalVoucher.SerialNumber = 0;

                _JournalVoucher.DeleteJVDetails();

                foreach (DataGridViewRow dr in mpMainSubViewControl1.Rows)
                {
                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() != "")
                    {
                        _JournalVoucher.SerialNumber += 1;
                        _JournalVoucher.JVVouDate = datePickerBillDate.Value.Date.ToString("yyyyMMdd");
                        if (txtNarration.Text != string.Empty)
                            _JournalVoucher.JVNarration = txtNarration.Text.ToString();
                        _JournalVoucher.ID = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        _JournalVoucher.CreatedBy = General.CurrentUser.Id;
                        _JournalVoucher.CreatedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _JournalVoucher.CreatedTime = DateTime.Now.ToString("HH:mm:ss");
                        _JournalVoucher.ModifiedBy = General.CurrentUser.Id;
                        _JournalVoucher.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                        _JournalVoucher.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                        _JournalVoucher.JVVouType = FixAccounts.VoucherTypeForJournalVoucher;
                        _JournalVoucher.JVVouSeries = General.ShopDetail.ShopVoucherSeries;
                        _JournalVoucher.JVAccountID = dr.Cells["Col_ID"].Value.ToString();

                        if (dr.Cells["Col_Debit"].Value != null)
                            _JournalVoucher.JVDebit = Convert.ToDouble(dr.Cells["Col_Debit"].Value);
                        if (dr.Cells["Col_Credit"].Value != null)
                            _JournalVoucher.JVCredit = Convert.ToDouble(dr.Cells["Col_Credit"].Value);
                        retValue = _JournalVoucher.AddDetails();
                        if (retValue == false)
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }

        private void mpMainSubViewControl1_OnTABKeyPressed(object sender, EventArgs e)
        {
            MainToolStrip.Select();
            tsBtnSave.Select();
        }

        private void mpMainSubViewControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (mpMainSubViewControl1.Rows.Count > 1 && (mpMainSubViewControl1.MainDataGridCurrentRow.Cells[0].Value == null))
                {
                    mpMainSubViewControl1.ClearSelection();
                    mpMainSubViewControl1_OnTABKeyPressed(null, null);
                }
            }
        }
        public override bool FillSearchData(string ID, string Vmode)
        {
            try
            {
                if (ID != null && ID != "")
                {
                    //pnlNameAddress.Enabled = true;
                    _JournalVoucher.ID = ID;
                    _JournalVoucher.ReadDetailsByJVID();

                    txtNarration.Text = _JournalVoucher.JVNarration;
                    txtVouchernumber.Text = _JournalVoucher.JVVouNo.ToString();
                    DateTime mydate = new DateTime(Convert.ToInt32(_JournalVoucher.JVVouDate.Substring(0, 4)), Convert.ToInt32(_JournalVoucher.JVVouDate.Substring(4, 2)), Convert.ToInt32(_JournalVoucher.JVVouDate.Substring(6, 2)));
                    datePickerBillDate.Value = mydate;
                    InitialisempPVC1();
                    CalculateTotals();
                    if (_Mode == OperationMode.Edit)
                    {
                        txtNarration.Enabled = true;
                        mpMainSubViewControl1.ClearSelection();
                        txtVouchernumber.Enabled = false;
                        pnlNameAddress.Enabled = true;
                        mpMainSubViewControl1.Enabled = true;
                        this.ActiveControl = mpMainSubViewControl1;
                        mpMainSubViewControl1.SetFocus(0, 1);
                        txtNarration.Focus();

                    }


                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return true;
        }

        private void txtVouchernumber_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtVouchernumber.Text != "")
                    {
                        _JournalVoucher.JVVouNo = Convert.ToInt32(txtVouchernumber.Text.ToString());
                        _JournalVoucher.JVVouSeries = txtVoucherSeries.Text.ToString();
                        //_JournalVoucher.ReadDetailsByVouNumber(_JournalVoucher.VoucherNumber, _JournalVoucher.VoucherType, _JournalVoucher.VoucherSeries, _Purchase.VoucherSubType);
                        FillSearchData(_JournalVoucher.JVVoucherID, "");
                    }
                }
                catch (Exception Ex)
                {
                    Log.WriteError("UclPurchase.txtVouchernumber_KeyDown>>" + Ex.Message);
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            _Mode = OperationMode.Edit;
            _JournalVoucher.ModifyEdit = "Y";
            mpMainSubViewControl1.Enabled = true;
            mpMainSubViewControl1.SetFocus(1);
        }
    }
}
