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
    public partial class UclAcListSundryCreditor : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private MPReports _MPReports;
        private string _MFromDate;
        private string _MToDate;
        private double _MDebit = 0;
        private double _MCredit = 0;
        private double _MTotalDebit = 0;
        private double _MTotalCredit = 0;
        #endregion

        # region Constructor
        public UclAcListSundryCreditor()
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
                headerLabel1.Text = "ACCOUNT - SUNDRY CREDITOR";              
                ClearControls();
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
            if (keyPressed == Keys.F11)
            {
                toDate1.Focus();
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
            toDate1.Focus();
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
                PrintReportHead = "SUNDRY CREDUTIRS AS ON " + General.GetDateInShortDateFormat(_MToDate);
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

                    if (dr.Cells["Col_ID"].Value != null)
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

                        if (dr.Cells["Col_AccName"].Value != null && dr.Cells["Col_AccName"].Value.ToString() == "Total")
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                        }
                        if (dr.Cells["Col_AccName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }


                        if (dr.Cells["Col_Debit"].Value != null && Convert.ToDouble(dr.Cells["Col_Debit"].Value.ToString()) != 0)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Debit"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(370.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Credit"].Value != null && Convert.ToDouble(dr.Cells["Col_Credit"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Credit"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(470.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                    }



                }

                PrintRowPixel += 17;
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


                row = new PrintRow("Accout Name", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Debit Amount", PrintRowPixel, 370, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Credit Amount", PrintRowPixel, 470, PrintFont);
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
                cbCredit.Checked = true;
                cbDebit.Checked = true;
                cbWithZeroClosing.Checked = true;
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
            FormatReportGrid();
            dgvReportList.InitializeColumnContextMenu();
        }
        public void HidepnlGO()
        {           
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;          
            toDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
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
                column.Name = "Col_AccName";             
                column.HeaderText = "Party";
                column.Width = 660;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Debit";              
                column.HeaderText = "DEBIT";
                column.Width = 135;                
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Credit";               
                column.HeaderText = "Credit";
                column.Width = 135;              
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
                FormatReportGrid();
                BindReportGrid();
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
                string _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                DataTable dtable = new DataTable();
                dtable = _MPReports.GetSundryCreditors(_MToDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FormatReportGrid()
        {
            dgvReportList.DoubleColumnNames.Add("Col_Debit");
            dgvReportList.DoubleColumnNames.Add("Col_Credit");
            dgvReportList.OptionalColumnNames.Add("Col_Debit");
            dgvReportList.OptionalColumnNames.Add("Col_Credit");
        }
        private void BindReportGrid()
        {
            try
            {
                int _RowIndex;
                double opdebit;
                double opcredit;
                double cldebit;
                double clcredit;
                _MTotalCredit = 0;
                _MTotalDebit = 0;
                DataGridViewRow currentdr;
                foreach (DataRow dr in _BindingSource.Rows)
                {

                    opdebit = 0;
                    opcredit = 0;
                    cldebit = 0;
                    clcredit = 0;
                    _MDebit = 0;
                    _MCredit = 0;
                    _RowIndex = dgvReportList.Rows.Add();
                   currentdr = dgvReportList.Rows[_RowIndex];                  

                    if (dr["Accopeningdebit"] != null)
                        double.TryParse(dr["Accopeningdebit"].ToString(), out opdebit);

                    if (dr["accopeningcredit"] != null)
                        double.TryParse(dr["accopeningcredit"].ToString(), out opcredit);


                    if (dr["Debit"] != null)
                        double.TryParse(dr["Debit"].ToString(), out _MDebit);

                    if (dr["Credit"] != null)
                        double.TryParse(dr["Credit"].ToString(), out _MCredit);
                    cldebit = opdebit + _MDebit - opcredit - _MCredit;
                    if (cldebit < 0)
                    {
                        clcredit = cldebit * -1;
                        cldebit = 0;
                    }

                    if ((cbDebit.Checked && cbCredit.Checked) || (!cbDebit.Checked && clcredit > 0) || (!cbCredit.Checked && cldebit > 0))
                    {
                        if (((opcredit + opdebit + _MCredit + _MDebit) > 0) && (cbWithZeroClosing.Checked || (!cbWithZeroClosing.Checked && (cldebit > 0 || clcredit > 0))))
                        {
                            currentdr.Cells["Col_ID"].Value = dr["AccountID"].ToString();
                            currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                            if (cldebit > 0)
                                currentdr.Cells["Col_Debit"].Value = cldebit.ToString("#0.00");
                            if (clcredit >= 0)
                                 currentdr.Cells["Col_Credit"].Value = clcredit.ToString("#0.00");
                            _MTotalDebit += cldebit;
                            _MTotalCredit += clcredit;
                        }
                    }


                }
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_ID"].Value = "T";
                currentdr.Cells["Col_AccName"].Value = "Total";
                currentdr.Cells["Col_Debit"].Value = _MTotalDebit.ToString("#0.00");
                currentdr.Cells["Col_Credit"].Value = _MTotalCredit.ToString("#0.00");
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

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }

        #endregion

        # region Events
        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "F10 = Show Report");
                ttToolTip.SetToolTip(pnlMultiSelection1, "F12 = Reopen This Form , F11 = Date");

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        
       

    }
}
