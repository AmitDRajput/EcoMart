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
using PharmaSYSRetailPlus.InterfaceLayer;
using PharmaSYSRetailPlus.Printing;
using PrintDataGrid;

namespace PharmaSYSRetailPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclAcListCreditorLedger : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceMultiSelection;
        private Account _Account;

        private List<DataGridViewRow> rowCollection;
      
        private MPReports _MPReports;
        private string _MFromDate;
        private string _MToDate;
        private double _MOpeningDebit = 0;
        private double _MOpeningCredit = 0;
        private double _MTrDebit = 0;
        private double _MTrCredit = 0;
        private double _MClosingDebit = 0;
        private double _MClosingCredit = 0;
        private double _MDebit = 0;
        private double _MCredit = 0;
        private string _MAccountID = "";

        private int selectedRowCount;
        #endregion

        # region Constructor
        public UclAcListCreditorLedger()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        #region IOverview Members

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _MPReports = new MPReports();
                rowCollection = new List<DataGridViewRow>();
                headerLabel1.Text = "ACCOUNT - CREDITORS' LEDGER";
                ClearControls();
                FillPartyCombo();
                FillVoucherTypes();
                FillMultiSelectionGrid();
                AddToolTip();
                HidepnlGO();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Home)
            {
                HidepnlGO();
                retValue = true;
            }
            if (keyPressed == Keys.End)
            {
                btnOKMultiSelectionClick();
                retValue = true;
            }
            if (keyPressed == Keys.C && modifier == Keys.Alt)
            {
                btnClearClick();
                retValue = true;
            }
            if (keyPressed == Keys.F && modifier == Keys.Alt)
            {
                fromDate1.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                if (cbSelectAll.Checked)
                    cbSelectAll.Checked = false;
                else
                    cbSelectAll.Checked = true;
                //CbSelectAllCheckedChange();
                retValue = true;
            }
            if (keyPressed == Keys.S && modifier == Keys.Alt)
            {
                txtSearch.Focus();
                retValue = true;
            }
            if (keyPressed == Keys.V && modifier == Keys.Alt)
            {
                btnViewListClick();
                retValue = true;
            }
            if (keyPressed == Keys.G && modifier == Keys.Alt)
            {
                if (pnlMultiSelection1.Visible == true)
                {
                    btnOKMultiSelectionClick();
                    retValue = true;
                }
            }

            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }

        public override void SetFocus()
        {
            base.SetFocus();
            fromDate1.Focus();
        }

        #endregion IOverview Members

        # region IReport Members

        public override void Print()
        {
            try
            {
                PrintData();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void PrintData()
        {
            PrintRow row;
            try
            {
                PrintReportHead = "CREDITOR'S LEDGER From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                double mamt = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_VoucherType"].Value != null || dr.Cells["Col_Narr"].Value != null)
                    {
                        if (PrintRowCount >= FixAccounts.NumberOfRowsPerReport)
                        {
                            PrintRowPixel += 34;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill(FixAccounts.ReportPageWidth, FixAccounts.ReportPageHeight);
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintPageNumber += 1;
                            PrintHead();
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        //  row = new PrintRow("", 0, 0, PrintFont);
                        mamt = 0;
                        if (dr.Cells["Col_ID"].Value != null && (dr.Cells["Col_ID"].Value.ToString() == "Total" || dr.Cells["Col_ID"].Value.ToString() == "Final Total"))
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                        }

                        if (dr.Cells["Col_VoucherDate"].Value != null)
                        {
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AccName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 60, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Narr"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Narr"].Value.ToString().PadRight(30), PrintRowPixel, 260, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_VoucherType"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 460, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_VoucherNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 510, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_Debit"].Value != null && Convert.ToDouble(dr.Cells["Col_Debit"].Value.ToString()) != 0)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Debit"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(570.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Credit"].Value != null && Convert.ToDouble(dr.Cells["Col_Credit"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Credit"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(670.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_ID"].Value.ToString() == "Final Total")
                        {
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                            PrintBill.Rows.Add(row);
                        }
                    }
                }

                PrintBill.Print_Bill(FixAccounts.ReportPageWidth, FixAccounts.ReportPageHeight);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private int PrintHead()
        {
            PrintRow row;
            try
            {
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2);

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;


                row = new PrintRow("Date", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Particulars", PrintRowPixel, 65, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Narration", PrintRowPixel, 270, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Type", PrintRowPixel, 460, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 520, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Debit Amount", PrintRowPixel, 570, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Credit Amount", PrintRowPixel, 670, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 0;
            return PrintRowCount;
        }


        #endregion IReportMember

        # region Other Private methods
        public void ClearControls()
        {
            try
            {
                InitializeReportGrid();
                InitializeMultiSelectionGrid();
                InitializeDates();
                lblFooterMessage.Text = "";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public void InitializeMultiSelectionGrid()
        {
            ConstructMultiSelectionGridColumns();
            ConstructSelectedGridColumns();
            if (dgvSelected.Rows.Count > 0)
                dgvSelected.Rows.Clear();
            dgvMultiSelection.Columns["Col_ID"].Visible = false;
            dgvSelected.Columns["Col_ID"].Visible = false;
            txtNoofSearches.Text = "";
            txtNoofSearches.Enabled = false;
            cbWithZeroTransactions.Checked = false;
            cbSelectAll.Checked = false;
            dgvSelected.Visible = false;
            txtNoofSearches.Enabled = false;
        }

        private void InitializeDates()
        {
            _MFromDate = General.ShopDetail.Shopsy;
            _MToDate = General.ShopDetail.Shopey;
            fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
            toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);
        }

        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            FormatReportGrid();
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {           
            pnlMultiSelection1.Visible = true;
            dgvSelected.Visible = false;
            tsbtnPrint.Enabled = false;
            mcbCreditor.SelectedID = "";
            mcbCreditor.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;           
            tsbtnPrint.Enabled = true;           
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            if (mcbvoutype.SelectedID != null && mcbvoutype.SelectedID != string.Empty)
                txtViewtext.Text = mcbvoutype.SeletedItem.ItemData[1];
            dgvReportList.Focus();
        }

        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.Width = 80;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccID";
                column.Visible = false;
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccAccID";
                column.Visible = false;
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "Date";
                column.Width = 90;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.HeaderText = "Party";
                column.Width = 300;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Narr";
                column.HeaderText = "Particulars";
                column.Width = 210;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.HeaderText = "Type";
                column.Width = 70;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 70;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Debit";
                column.HeaderText = "Debit";
                column.Width = 100;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Credit";
                column.HeaderText = "Credit";
                column.Width = 100;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.HeaderText = "VoucherSubType";
                column.Width = 80;
                column.Visible = false;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructMultiSelectionGridColumns()
        {
            try
            {
                dgvMultiSelection.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgvMultiSelection.Columns.Add(column);

                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
                columnCheck.HeaderText = "Check";
                columnCheck.ValueType = typeof(bool);
                columnCheck.Width = 100;
                dgvMultiSelection.Columns.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 150;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OpeningDebit";
                column.DataPropertyName = "AccOpeningDebit";
                column.HeaderText = "Opening Debit";             
                column.ReadOnly = true;
                column.Visible = false;
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OpeningCredit";
                column.DataPropertyName = "AccOpeningCredit";
                column.HeaderText = "Opening Credit";              
                column.Visible = false;
                column.ReadOnly = true;
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);


            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructSelectedGridColumns()
        {
            try
            {
                dgvSelected.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OpeningDebit";              
                column.HeaderText = "Opening Debit";  
                column.Visible = false;
                column.ReadOnly = true;
                column.Width = 200;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OpeningCredit";             
                column.HeaderText = "Opening Credit";               
                column.Visible = false;
                column.ReadOnly = true;
                column.Width = 200;
                dgvSelected.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Debit");
            dgvReportList.DoubleColumnNames.Add("Col_Credit");
            dgvReportList.OptionalColumnNames.Add("Col_Debit");
            dgvReportList.OptionalColumnNames.Add("Col_Credit");
        }
        private void BindReportGrid(string AccID, string party, string narr, double opDebit, double opCredit)
        {
            try
            {
                int _RowIndex;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                currentdr.Cells["Col_AccID"].Value = AccID;
                currentdr.Cells["Col_AccName"].Value = party;
                currentdr.Cells["Col_Narr"].Value = narr;
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                _MOpeningDebit = opDebit;
                _MOpeningCredit = opCredit;
                GetActualOpeningBalance(AccID, _MFromDate);
                double mbalance = _MOpeningDebit - _MOpeningCredit;
                if (mbalance < 0)
                {
                    _MOpeningCredit = mbalance * (-1);
                    _MOpeningDebit = 0;
                }
                else
                {
                    _MOpeningDebit = mbalance;
                    _MOpeningCredit = 0;
                }
                _MTrDebit = 0;
                _MTrCredit = 0;
                if (_MOpeningDebit >= 0 && _MOpeningCredit == 0)
                {
                    currentdr.Cells["Col_Narr"].Value = "Opening Debit";
                    currentdr.Cells["Col_Debit"].Value = _MOpeningDebit.ToString("#0.00");
                }
                else
                {
                    currentdr.Cells["Col_Narr"].Value = "Opening Credit";
                    currentdr.Cells["Col_Credit"].Value = _MOpeningCredit.ToString("#0.00");
                }

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (dr["AccountID"].ToString() == AccID && Convert.ToInt32(dr["TransactionDate"].ToString()) >= Convert.ToInt32(_MFromDate) && Convert.ToInt32(dr["TransactionDate"].ToString()) <= Convert.ToInt32(_MToDate))
                    {
                        bool ifadd = false;
                        if (mcbvoutype.SelectedID != null && mcbvoutype.SelectedID != "" && mcbvoutype.SeletedItem.ItemData[1].ToString() != null)
                        {
                            if (dr["VoucherType"].ToString() == mcbvoutype.SeletedItem.ItemData[1].ToString())
                            {
                                ifadd = true;
                            }
                            else ifadd = false;
                        }
                        else
                            ifadd = true;
                        if (ifadd)
                        {

                            _RowIndex = dgvReportList.Rows.Add();
                            currentdr = dgvReportList.Rows[_RowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["VoucherID"].ToString();
                            currentdr.Cells["Col_VoucherDate"].Value = dr["TransactionDate"].ToString();
                            currentdr.Cells["Col_AccID"].Value = dr["AccAccountID"].ToString();
                            currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                            currentdr.Cells["Col_Narr"].Value = dr["Narration"].ToString();
                            currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                            if (dr["VoucherSubType"] != DBNull.Value)
                                currentdr.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                            _MDebit = Convert.ToDouble(dr["Debit"].ToString());
                            _MCredit = Convert.ToDouble(dr["Credit"].ToString());
                            _MTrDebit += _MDebit;
                            _MTrCredit += _MCredit;
                            if (_MDebit > 0)
                                currentdr.Cells["Col_Debit"].Value = _MDebit.ToString("#0.00");
                            else
                                currentdr.Cells["Col_Credit"].Value = _MCredit.ToString("#0.00");
                            if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForBankReceipt)
                            {
                                _RowIndex = dgvReportList.Rows.Add();
                                currentdr = dgvReportList.Rows[_RowIndex];
                                if (dr["BankName"] != DBNull.Value)
                                    currentdr.Cells["Col_AccName"].Value = "  " + dr["BankName"].ToString();
                                if (dr["BranchName"] != DBNull.Value)
                                    currentdr.Cells["Col_Narr"].Value = dr["BranchName"].ToString();
                                _RowIndex = dgvReportList.Rows.Add();
                                currentdr = dgvReportList.Rows[_RowIndex];
                                string chq = "";
                                if (dr["ChequeNumber"] != DBNull.Value)
                                    chq = "Chq.No:" + dr["ChequeNumber"].ToString().Trim();
                                if (dr["ChequeDate"] != DBNull.Value)
                                    chq = chq + "  Date:" + General.GetDateInShortDateFormat(dr["ChequeDate"].ToString());
                                currentdr.Cells["Col_Narr"].Value = chq;

                            }
                        }
                    }

                }
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportSubTotalColor;
                currentdr.Cells["Col_ID"].Value = "Total";
                currentdr.Cells["Col_Narr"].Value = "Transactions Total";
                currentdr.Cells["Col_Debit"].Value = _MTrDebit.ToString("#0.00");
                currentdr.Cells["Col_Credit"].Value = _MTrCredit.ToString("#0.00");
                _MClosingCredit = 0;
                _MClosingDebit = 0;
                _MDebit = _MOpeningDebit + _MTrDebit;
                _MCredit = _MOpeningCredit + _MTrCredit;
                _MDebit = _MDebit - _MCredit;
                if (_MDebit < 0)
                    _MClosingCredit = _MDebit * (-1);
                else
                    _MClosingDebit = _MDebit;

                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_ID"].Value = "Final Total";
                currentdr.Cells["Col_AccName"].Value = party;
                currentdr.Cells["Col_Narr"].Value = "Closing Balance";
                if (_MClosingCredit > 0)
                {
                    currentdr.Cells["Col_Narr"].Value = "Credit Closing Balance ";
                    currentdr.Cells["Col_Credit"].Value = _MClosingCredit.ToString("#0.00");
                }
                else
                {
                    currentdr.Cells["Col_Narr"].Value = "Debit Closing Balance ";
                    currentdr.Cells["Col_Debit"].Value = _MClosingDebit.ToString("#0.00");
                }
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }


        }
        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_MFromDate, _MToDate);
                bool foundintrnac = true;
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lblFooterMessage.Text = "";
                    InitializeReportGrid();
                    ShowpnlGO();
                    rowCollection = new List<DataGridViewRow>();
                    if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != string.Empty)
                    {
                        _MAccountID = mcbCreditor.SelectedID;
                        if (_MAccountID != "")
                        {
                            GetAccountData();
                        }
                        foundintrnac = true;
                        if (!cbWithZeroTransactions.Checked && _BindingSource.Rows.Count == 0)
                        {
                            foundintrnac = false;
                        }

                        if (foundintrnac)
                        {
                            BindReportGrid(mcbCreditor.SelectedID, mcbCreditor.SeletedItem.ItemData[2], mcbCreditor.SeletedItem.ItemData[3],
                           Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[5].ToString()), Convert.ToDouble(mcbCreditor.SeletedItem.ItemData[6].ToString()));
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow selectedrow in dgvSelected.Rows)
                        {
                            rowCollection.Add(selectedrow);
                        }
                        if (rowCollection.Count > 0)
                        {
                            if (dgvReportList.Rows.Count > 0)
                                dgvReportList.Rows.Clear();

                            foundintrnac = true;
                            foreach (DataGridViewRow row in rowCollection)
                            {
                                _MAccountID = "";
                                if (row.Cells["Col_ID"].Value != null)
                                    _MAccountID = row.Cells["Col_ID"].Value.ToString();
                                if (_MAccountID != "")
                                {
                                    GetAccountData();
                                }
                                foundintrnac = true;
                                if (!cbWithZeroTransactions.Checked && _BindingSource.Rows.Count == 0)
                                {
                                    foundintrnac = false;
                                }

                                if (foundintrnac)
                                {
                                    BindReportGrid(row.Cells["Col_ID"].Value.ToString(), row.Cells["Col_Name"].Value.ToString(), row.Cells["Col_Address"].Value.ToString(),
                                    Convert.ToDouble(row.Cells["Col_OpeningDebit"].Value.ToString()), Convert.ToDouble(row.Cells["Col_OpeningCredit"].Value.ToString()));
                                }
                            }
                        }
                    }
                    this.Cursor = Cursors.Default;
                    NoofRows();
                    dgvReportList.Focus();
                }
                else
                    lblFooterMessage.Text = "Check Date";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void GetActualOpeningBalance(string accID, string fromDate)
        {
            double totdebit = 0;
            double totcredit = 0;
            foreach (DataRow dr in _BindingSource.Rows)
            {
                if (dr["AccountID"].ToString() == accID && Convert.ToInt32(dr["TransactionDate"].ToString()) < Convert.ToInt32(fromDate))
                {
                    if (dr["Debit"] != null && dr["Debit"].ToString() != "")
                        totdebit += Convert.ToDouble(dr["Debit"].ToString());
                    if (dr["Credit"] != null && dr["Credit"].ToString() != "")
                        totcredit += Convert.ToDouble(dr["Credit"].ToString());
                }

            }
            _MOpeningDebit += totdebit;
            _MOpeningCredit += totcredit;
        }

        private void CheckFiltertxtSearch(string txtString)
        {

            try
            {

                _BindingSourceMultiSelection.DefaultView.RowFilter = "AccName like '" + txtString + "%'";

            }
            catch (Exception ex) { Log.WriteException(ex); }

        }

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        } 

        private void FillVoucherTypes()
        {
            try
            {
                mcbvoutype.SelectedID = null;
                mcbvoutype.SourceDataString = new string[3] { "ID", "VoucherType", "Code" };
                mcbvoutype.ColumnWidth = new string[3] { "0", "50", "0" };
                mcbvoutype.ValueColumnNo = 0;
                mcbvoutype.UserControlToShow = new UclAccount();
                _Account = new Account();
                DataTable dtable = _Account.GetVoucherTypes();
                mcbvoutype.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillPartyCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[7] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAddress2", "AccOpeningDebit", "AccOpeningCredit" };
                mcbCreditor.ColumnWidth = new string[7] { "0", "50", "200", "200", "200", "0", "0" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;              
                Account _Party = new Account();
                DataTable dtable = _Party.GetSSAccountHoldersList("C");
                mcbCreditor.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillMultiSelectionGrid()
        {

            try
            {
                selectedRowCount = 0;
                RefreshSelectedRowCounter();
                dgvMultiSelection.DataSource = null;
                ConstructMultiSelectionGridColumns();
                FillMultiSelectionData();
                dgvMultiSelection.DataSource = _BindingSourceMultiSelection;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillMultiSelectionData()
        {
            try
            {
                DataTable dtable = new DataTable();
                _Account = new Account();
                dtable = _Account.GetSSAccountHoldersListForMultiSelection(FixAccounts.AccCodeForCreditor);
                _BindingSourceMultiSelection = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnViewListClick()
        {
            dgvSelected.Sort(dgvSelected.Columns["Col_Name"], ListSortDirection.Ascending);
            if (dgvSelected.Visible == false)
            {
                dgvSelected.Visible = true;
                dgvMultiSelection.Enabled = false;
                btnViewList.Text = "Close";
            }
            else
            {
                dgvSelected.Visible = false;
                dgvMultiSelection.Enabled = true;
                btnViewList.Text = "View";
            }
        }

        private void GetAccountData()
        {
            DataTable dtable = new DataTable();
            dtable = _MPReports.GetGeneralLedger(_MFromDate, _MToDate, _MAccountID);
            _BindingSource = dtable;
        }

        private void RefreshSelectedRowCounter()
        {
            selectedRowCount = dgvSelected.Rows.Count;
            txtNoofSearches.Text = selectedRowCount.ToString("#0");
        }

        private void AddIndgvSelectedGrid(DataGridViewRow drow)
        {
            bool iffound = false;
            foreach (DataGridViewRow drowselected in dgvSelected.Rows)
            {
                if (drowselected.Cells["Col_ID"].Value != null && drowselected.Cells["Col_ID"].Value.ToString() == drow.Cells["Col_ID"].Value.ToString())
                {
                    iffound = true;
                    break;
                }
            }
            if (!iffound)
            {

                int selectedrowindex = dgvSelected.Rows.Add();
                dgvSelected.Rows[selectedrowindex].Cells["Col_ID"].Value = drow.Cells["Col_ID"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_Name"].Value = drow.Cells["Col_Name"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_Address"].Value = drow.Cells["Col_Address"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_OpeningDebit"].Value = drow.Cells["Col_OpeningDebit"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_OpeningCredit"].Value = drow.Cells["Col_OpeningCredit"].Value.ToString();
            }

        }

        private void RemoveFromdgvSelectedGrid(DataGridViewRow drow)
        {
            foreach (DataGridViewRow drowselected in dgvSelected.Rows)
            {
                if (drowselected.Cells["Col_ID"].Value.ToString() == drow.Cells["Col_ID"].Value.ToString())
                {
                    dgvSelected.Rows.Remove(drowselected);
                    break;
                }
            }


        }

        #endregion OtherPrivate Methods

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CbSelectAllCheckedChange();
        }

        private void CbSelectAllCheckedChange()
        {
            try
            {
                int mlen = 0;
                mlen = txtSearch.Text.ToString().Length;
                if (txtSearch.Text != null && txtSearch.Text != "")
                {
                    //  if (cbSelectAll.Checked == true)
                    {
                        if (dgvMultiSelection.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow drow in dgvMultiSelection.Rows)
                            {

                                if (drow.Cells["Col_Name"].Value != null && txtSearch.Text.ToString() == drow.Cells["Col_Name"].Value.ToString().Substring(0, mlen))
                                {
                                    drow.Cells["Col_Check"].Value = cbSelectAll.Checked;
                                }
                            }
                        }
                        //  txtSearch.Text = "";
                    }

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void dgvMultiSelection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvMultiSelection.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvMultiSelection_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvMultiSelection.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    if (Convert.ToBoolean(dgvMultiSelection.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == true)
                    {
                        selectedRowCount++;
                        rowCollection.Add(dgvMultiSelection.Rows[e.RowIndex]);
                        AddIndgvSelectedGrid(dgvMultiSelection.Rows[e.RowIndex]);
                    }
                    else
                    {
                        selectedRowCount--;
                        RemoveFromdgvSelectedGrid(dgvMultiSelection.Rows[e.RowIndex]);
                    }
                    RefreshSelectedRowCounter();
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSearch.Text != null && txtSearch.Text.ToString() != "")
            {
                CheckFiltertxtSearch(txtSearch.Text.ToString().Trim());
                cbSelectAll.Visible = true;
                cbSelectAll.Checked = false;
                dgvMultiSelection.Focus();
            }
        }

        private void btnViewList_KeyDown(object sender, KeyEventArgs e)
        {
            btnViewListClick();
        }

        private void btnViewList_Click(object sender, EventArgs e)
        {
            btnViewListClick();
        }

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            string voutype = "";
            string vousubtype = "";
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    voutype = dgvReportList.SelectedRow.Cells["Col_VoucherType"].Value.ToString();
                    //if (dgvReportList.SelectedRow.Cells["Col_VoucherSubType"].Value != null)
                    //    vousubtype = dgvReportList.SelectedRow.Cells["Col_VoucherSubType"].Value.ToString();

                    if (voutype == FixAccounts.VoucherTypeForCashPurchase || voutype == FixAccounts.VoucherTypeForCreditPurchase || voutype == FixAccounts.VoucherTypeForCreditStatementPurchase)
                        ViewControl = new UclPurchase();
                    if (voutype == FixAccounts.VoucherTypeForCashReceipt)
                        ViewControl = new UclCashReceipt();
                    if (voutype == FixAccounts.VoucherTypeForCashPayment)
                        ViewControl = new UclCashPayment();
                    if (voutype == FixAccounts.VoucherTypeForBankReceipt)
                        ViewControl = new UclBankReceipt();
                    if (voutype == FixAccounts.VoucherTypeForBankPayment)
                        ViewControl = new UclBankPayment();
                    if (voutype == FixAccounts.VoucherTypeForBankExpenses)
                        ViewControl = new UclBankExpenses();
                    if (voutype == FixAccounts.VoucherTypeForCashExpenses)
                        ViewControl = new UclCashExpenses();
                    if (voutype == FixAccounts.VoucherTypeForChequeReturn)
                        ViewControl = new UclChequeReturn();
                    if (voutype == FixAccounts.VoucherTypeForCorrectionInRate)
                        ViewControl = new UclCorrectioninRate();
                    if (voutype == FixAccounts.VoucherTypeForCreditNoteAmount)
                        ViewControl = new UclCreditNoteAmount();
                    if (voutype == FixAccounts.VoucherTypeForCreditNoteStock)
                        ViewControl = new UclCreditNoteStock();
                    if (voutype == FixAccounts.VoucherTypeForDebitNoteAmount)
                        ViewControl = new UclDebitNoteAmount();
                    if (voutype == FixAccounts.VoucherTypeForDebitNoteStock)
                        ViewControl = new UclDebitNotestock();
                    if (voutype == FixAccounts.VoucherTypeForJournalEntry)
                        ViewControl = new UclJournalVoucher();

                    if (voutype == FixAccounts.VoucherTypeForOpeningStock)
                        ViewControl = new UclOPStock();
                    if (voutype == FixAccounts.VoucherTypeForPurchaseOrder)
                        ViewControl = new UclPurchaseOrder();
                    //if (voutype == FixAccounts.VoucherTypeForPurchaseReturn)
                    //    ViewControl = new UclPurchaseReturn();
                    //if (voutype == FixAccounts.VoucherTypeForSalesReturn)
                    //    ViewControl = new uclSalesReturn();
                    if (voutype == FixAccounts.VoucherTypeForStatementHospital)
                        ViewControl = new UclStatementHospital();
                    if (voutype == FixAccounts.VoucherTypeForStatementPurchase)
                        ViewControl = new UclStatementSale();
                    if (voutype == FixAccounts.VoucherTypeForStockIN)
                        ViewControl = new UclStockIn();
                    if (voutype == FixAccounts.VoucherTypeForStockOut)
                        ViewControl = new UclStockOut();

                    if (vousubtype == FixAccounts.SubTypeForPatientSale)
                        ViewControl = new UclPatientSale();
                    else if (vousubtype == FixAccounts.SubTypeForHospitalSale)
                        ViewControl = new UclHospitalSale();
                    else if (vousubtype == FixAccounts.SubTypeForInstitutionalSale)
                        ViewControl = new UclInstitutionalSale();
                    else if (vousubtype == FixAccounts.SubTypeForDebtorSale)
                        ViewControl = new UclDebtorSale();
                    else if (vousubtype == FixAccounts.SubTypeForVoucherSale)
                        ViewControl = new UclPatientSale();



                    ShowViewForm(selectedID, ViewMode.Current);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }

        }
        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            mcbCreditor.Focus();
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != string.Empty)
                btnOKMultiSelectionClick();
            else
                txtSearch.Focus();
        }

        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnClearClick();
        }

        private void btnClearClick()
        {
            if (dgvSelected.Rows.Count > 0)
                dgvSelected.Rows.Clear();
            txtSearch.Text = "";
            txtNoofSearches.Text = "";
            cbSelectAll.Checked = false;
            foreach (DataGridViewRow dr in dgvMultiSelection.Rows)
            {
                dr.Cells["Col_Check"].Value = false;
            }

        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "F10 = Show Report");
                ttToolTip.SetToolTip(pnlMultiSelection1, "F12 = Reopen This Form , F11 = Date , F9 = Select All , F2 = Search String");
                ttToolTip.SetToolTip(cbSelectAll, "F9");
                ttToolTip.SetToolTip(txtSearch, "F2");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion
      
    }
}
