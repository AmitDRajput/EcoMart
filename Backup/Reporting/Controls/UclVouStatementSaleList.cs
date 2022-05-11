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
    public partial class UclVouStatementSaleList : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private Statements _Statements;
        private string _VouType;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        # endregion

        # region Contructor
        public UclVouStatementSaleList()
        {
            try
            {
            InitializeComponent();
            ViewControl = new UclStatementPartywiseSale();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        # endregion

        # region Ioverview members

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _Statements = new Statements();
                headerLabel1.Text = "VOUCHER LIST-STATEMENT SALE";
                ClearControls();
                InitializeReportGrid();
                AddToolTip();
                HidepnlGO();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override void SetFocus()
        {
            base.SetFocus();
            fromDate1.Focus();
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

                PrintReportHead = "Statement Sale From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                double mamt = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                int mlen = 0;
                int colpix = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    //if (dr.Cells["Col_ID"].Value != null)
                    {
                        if (PrintRowCount >= FixAccounts.NumberOfRowsPerReport)
                        {
                            PrintRowPixel += 34;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintPageNumber += 1;
                            PrintHead();
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;

                        if ((dr.Cells["Col_VoucherNumber"].Value == null || dr.Cells["Col_VoucherNumber"].Value.ToString() == "") && dr.Cells["Col_Amount"].Value != null)
                        {
                            PrintRowPixel += 17;
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;


                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(670.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);

                            PrintRowPixel += 17;
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            break;
                        }

                        mamt = Convert.ToDouble(dr.Cells["Col_VoucherNumber"].Value.ToString());
                        mlen = (mamt.ToString("#0").Length);
                        colpix = Convert.ToInt32(5.00 + ((6.00 - Convert.ToDouble(mlen)) * 5.5));
                        row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                        PrintBill.Rows.Add(row);

                        //row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString(), PrintRowPixel, 40, PrintFont);
                        //PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 60, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString(), PrintRowPixel, 140, PrintFont);
                        PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_Address"].Value != null)
                            row = new PrintRow(dr.Cells["Col_Address"].Value.ToString(), PrintRowPixel, 240, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_FromDate"].Value.ToString()), PrintRowPixel, 470, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_ToDate"].Value.ToString()), PrintRowPixel, 540, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_NoofBills"].Value.ToString(), PrintRowPixel, 650, PrintFont);
                        PrintBill.Rows.Add(row);

                        mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        mlen = (mamt.ToString("#0.00").Length);
                        colpix = Convert.ToInt32(670.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        PrintBill.Rows.Add(row);

                        //row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 660, PrintFont);
                        //PrintBill.Rows.Add(row);

                    }
                }

                PrintBill.Print_Bill();
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

                row = new PrintRow("Number", PrintRowPixel, 10, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 60, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 150, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Address", PrintRowPixel, 350, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("From Date", PrintRowPixel, 460, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("To Date", PrintRowPixel, 550, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Bills", PrintRowPixel, 640, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 700, PrintFont);
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
                InitializeDates();
                lblFooterMessage.Text = "";
                _VouType = FixAccounts.VoucherTypeForStatementSale;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Visible = false;
            ViewToDate.Visible = false;
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
            ViewFromDate.Visible = true;
            ViewToDate.Visible = true;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
        }



        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "Date";
                column.Width = 90;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.HeaderText = "AccountID";
                column.Visible = false;
                column.Width = 115;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.HeaderText = "Party";
                column.Width = 300;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.HeaderText = "Address";
                column.Width = 190;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_FromDate";
                column.HeaderText = "From Date";
                column.Width = 90;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ToDate";
                column.HeaderText = "To Date";
                column.Width = 90;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_NoOfBills";
                column.HeaderText = "Bills";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 120;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillReportGrid()
        {
            try
            {
                FillReportData();
                dgvReportList.DataSource = _BindingSource;
                FormatReportGrid();
                BindReportGrid();
                CalculateFinalTotals();
                NoofRows();
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _Statements.GetOverviewDataBothStatementForView(_VouType, _MFromDate, _MToDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }



        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DateColumnNames.Add("Col_FromDate");
            dgvReportList.DateColumnNames.Add("Col_ToDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");

        }

        private void BindReportGrid()
        {
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {

                    int rowIndex = dgvReportList.Rows.Add();
                    DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                    dgvrow.Cells["Col_ID"].Value = dr["ID"].ToString();
                    dgvrow.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                    dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                    dgvrow.Cells["Col_FromDate"].Value = dr["FromDate"].ToString();
                    dgvrow.Cells["Col_ToDate"].Value = dr["ToDate"].ToString();
                    dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                    dgvrow.Cells["Col_NoOFBills"].Value = dr["NumberofBills"].ToString();
                    dgvrow.Cells["Col_Amount"].Value = dr["AmountNet"].ToString();


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lblFooterMessage.Text = "";
                    InitializeReportGrid();
                    ShowpnlGO();
                    FillReportGrid();
                    this.Cursor = Cursors.Default;
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

        private void CheckFilter()
        {
            double mamount = 0;
            string partycode = "";
            try
            {

                if (mamount > 0)
                {
                    if (partycode == "")
                        _BindingSource.DefaultView.RowFilter = "AmountNet = " + mamount + " and Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    else
                        _BindingSource.DefaultView.RowFilter = "AmountNet = " + mamount + " AND  AccountID = '" + partycode + "' and voucherdate <= '" + _MToDate + "'";
                }
                else
                {
                    if (partycode != "")
                        _BindingSource.DefaultView.RowFilter = "AccountID = '" + partycode + "' and Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    else
                        _BindingSource.DefaultView.RowFilter = "Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                }
                NoofRows();
            }
            catch (Exception ex) { Log.WriteException(ex); }

        }

        private void CalculateFinalTotals()
        {
            try
            {
                _MTotalAmount = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null)
                    {
                        _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                    }
                }
                int rowIndex = dgvReportList.Rows.Add();
                DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                dgvrow.Cells["Col_AccName"].Value = "Total";
                dgvrow.Cells["Col_Amount"].Value = _MTotalAmount.ToString("#0.00");
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
        }

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }

        #endregion Other Private Methods

        # region Events


        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    ShowViewForm(selectedID, ViewMode.Current);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }


        private void btnOKMultiSelection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }
        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelection1.Focus();
        }
        # endregion Events

        #region Tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "End = Show Report");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home = Reopen This Form ");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion  Tooltip

       
    }
}
