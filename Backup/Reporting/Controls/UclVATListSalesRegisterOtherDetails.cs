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
    public partial class UclVATListSalesRegisterOtherDetails : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalCashDiscount = 0;
        private double _MTotalAddON = 0;
        private double _MTotalCreditNote = 0;
        private double _MTotalDebitNote = 0;
        #endregion

        # region Constructor
        public UclVATListSalesRegisterOtherDetails()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclPatientSale();
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
                _SaleList = new SaleList();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "VAT - SALES REGISTER OTHER DETAILS ";
                ClearControls();
                ConstructReportColumns();
                dgvReportList.Columns["Col_ID"].Visible = false;           
                AddToolTip();
                HidepnlGO();
                rbtnAll.Checked = true; 
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
                PrintReportHead = "VAT Sale Register Other Details [" + txtViewtext.Text.ToString() + "] From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "[" + txtViewtext.Text.ToString() + "]";
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

                    if (dr.Cells["Col_VoucherType"].Value != null || dr.Cells["Col_AccName"].Value != null)
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
                        //  row = new PrintRow("", 0, 0, PrintFont);
                        mamt = 0;
                        if (dr.Cells["Col_AccName"].Value.ToString() == "Total")
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                        }
                        if (dr.Cells["Col_VoucherType"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_VoucherNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 40, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherDate"].Value != null)
                        {
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 80, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AccName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30).Substring(0, 15), PrintRowPixel, 140, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AmountDiscount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AmountDiscount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(340.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_AddOnFreight"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AddOnFreight"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(440.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AmountCreditNote"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AmountCreditNote"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(540.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AmountDebitNote"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AmountDebitNote"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(640.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                      
             
                    }



                }
                PrintRowPixel += 17;
                PrintRowCount += 1;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);


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

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 50, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 80, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 140, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Cash Disc", PrintRowPixel, 360, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("ADD-On", PrintRowPixel, 460, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("CR.Note", PrintRowPixel, 560, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("DB.Note", PrintRowPixel, 660, PrintFont);
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
                string startdate = General.GetDateInDateFormat(General.ShopDetail.Shopsy);
                fromDate1.Text = startdate;
                string enddate = General.GetDateInDateFormat(General.ShopDetail.Shopey);
                toDate1.Text = enddate;               
                lblFooterMessage.Text = "";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
            ViewFromDate.Visible = true;
            ViewToDate.Visible = true;
        }

        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";             
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";              
                column.HeaderText = "TYPE";
                column.Width = 40;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";              
                column.HeaderText = "NUMBER";
                column.Width = 65;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";               
                column.HeaderText = "DATE";
                column.Width = 75;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";               
                column.HeaderText = "Party";
                column.Width = 230;
                dgvReportList.Columns.Add(column);               

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountDiscount";             
                column.HeaderText = "Discount";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();

                column.Name = "Col_AddOnFreight";               
                column.HeaderText = "Add On";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountCreditNote";             
                column.HeaderText = "Credit Note";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountDebitNote";             
                column.HeaderText = "Debit Note";
                column.Width = 100;
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
                CheckFilter();
                BindReportGrid();
                CalculateFinalTotals();              
                NoofRows();
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
                dtable = _SaleList.GetDataForVATRegister(_MFromDate, _MToDate);
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
            dgvReportList.DoubleColumnNames.Add("Col_AmountDiscount");
            dgvReportList.DoubleColumnNames.Add("Col_AddOnFreight");
            dgvReportList.DoubleColumnNames.Add("Col_AmountCreditNote");
            dgvReportList.DoubleColumnNames.Add("Col_AmountDebitNote");
        }

        private void BindReportGrid()
        {
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {                  
                    if (txtType.Text.ToString() == "" || (dr["VoucherType"].ToString() == txtType.Text.ToString()))
                    {
                        int rowIndex = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                        dgvrow.Cells["Col_ID"].Value = dr["ID"].ToString();
                        dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        dgvrow.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                        dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        dgvrow.Cells["Col_AmountDiscount"].Value = dr["AmountCashDiscount"].ToString();
                        dgvrow.Cells["Col_AddOnFreight"].Value = dr["AddOnFreight"].ToString();
                        dgvrow.Cells["Col_AmountCreditNote"].Value = dr["AmountCreditNote"].ToString();
                        dgvrow.Cells["Col_AmountDebitNote"].Value = dr["AmountDebitNote"].ToString();
                    }
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
                    ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
                    ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
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

            try
            {

                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");

                if (rbtnCash.Checked == true)
                {
                   _BindingSource.DefaultView.RowFilter = " VoucherType = '"+FixAccounts.VoucherTypeForCashSale +"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "Cash";
                    txtType.Text = FixAccounts.VoucherTypeForCashSale;
                }
                else if (rbtnCreditStatement.Checked == true)
                {
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+FixAccounts.VoucherTypeForCreditStatementSale +"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "CreditStatement";
                    txtType.Text = FixAccounts.VoucherTypeForCreditStatementSale;
                }
                else if (rbtnCredit.Checked == true)
                {
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+FixAccounts.VoucherTypeForCreditSale +"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "Credit";
                    txtType.Text = FixAccounts.VoucherTypeForCreditSale;
                
                }
                else if (rbtnVoucher.Checked == true)
                {
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '" + FixAccounts.VoucherTypeForVoucherSale + "'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "Voucher";
                    txtType.Text = FixAccounts.VoucherTypeForVoucherSale;
                }
                else
                {
                    _BindingSource.DefaultView.RowFilter = "Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "ALL";
                    txtType.Text = "";
                }
            }
            catch (Exception ex) { Log.WriteException(ex); }
        }

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }

        private void CalculateFinalTotals()
        {
            _MTotalCashDiscount = 0;
            _MTotalAddON = 0;
            _MTotalCreditNote = 0;
            _MTotalDebitNote = 0;

            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {
                _MTotalCashDiscount += Convert.ToDouble(dr.Cells["Col_AmountDiscount"].Value.ToString());
                _MTotalAddON += Convert.ToDouble(dr.Cells["Col_AddOnFreight"].Value.ToString());
                _MTotalCreditNote += Convert.ToDouble(dr.Cells["Col_AmountCreditNote"].Value.ToString());
                _MTotalDebitNote += Convert.ToDouble(dr.Cells["Col_AmountDebitNote"].Value.ToString());
            }
            int rowIndex = dgvReportList.Rows.Add();
            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
            dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
            dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
            dgvrow.Cells["Col_AccName"].Value = "Total";
            dgvrow.Cells["Col_AmountDiscount"].Value = _MTotalCashDiscount.ToString("#0.00");
            dgvrow.Cells["Col_AddOnFreight"].Value = _MTotalAddON.ToString("#0.00");
            dgvrow.Cells["Col_AmountCreditNote"].Value = _MTotalCreditNote.ToString("#0.00");
            dgvrow.Cells["Col_AmountDebitNote"].Value = _MTotalDebitNote.ToString("#0.00");

        }
       

        #endregion

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
      
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
        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtnAll.Focus();
        }
        private void rbtnAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }

        private void rbtnCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }

        private void rbtnCredit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }

        private void rbtnCreditStatement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }

        private void rbtnVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }
        #endregion Events


        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion

      

     
    }
}
