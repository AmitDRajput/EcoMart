using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.Common;
using PharmaSYSDistributorPlus.BusinessLayer;
using PharmaSYSDistributorPlus.InterfaceLayer;
using PharmaSYSDistributorPlus.Printing;
using PrintDataGrid;
using System.IO;

namespace PharmaSYSDistributorPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclVATListCreditNote : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private CreditDebitNote _CDNote;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;     
        private double _MTotalAmount5 = 0;
        private double _MTotalVAT5 = 0;
        private double _MTotalAmount12point5 = 0;
        private double _MTotalVAT12point5 = 0;
        private double _MTotalRoundoff = 0;
        #endregion

        # region Constructor
        public UclVATListCreditNote()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclCreditNoteStock();
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
                _CDNote = new CreditDebitNote();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "VAT - SALES RETURN REGISTER";
                ConstructReportColumns();
                dgvReportList.Columns["Col_ID"].Visible = false;
                dgvReportList.DataSource = _BindingSource;
                ClearControls();
              //  FillReportData();
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

        public override void Export(string ExportFileName)
        {
            base.Export(ExportFileName);
            GeneralReport.ExportFile(PrintReportHead, PrintReportHead2, dgvReportList, ExportFileName);
        }

        public override void EMail(string EmailID)
        {
            base.EMail(EmailID);
            GeneralReport.SendEmails(PrintReportHead, PrintReportHead2, dgvReportList, EmailID);
        }
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
                
                PrintBill.Rows.Clear();
                PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));
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
                        if (dr.Cells["Col_BillType"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 150, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_BillNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 190, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AccName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30).Substring(0, 15), PrintRowPixel, 250, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Amount5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(320.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_VAT5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_VAT5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(390.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Amount12point5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount12point5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(460.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_VAT12Point5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_VAT12Point5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(540.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_RoundUpAmount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_RoundUpAmount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(620.00 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Amount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(680.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

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
                row = new PrintRow("Bill Type", PrintRowPixel,140, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Vou No", PrintRowPixel, 190, PrintFont);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Party", PrintRowPixel, 240, PrintFont);
                PrintBill.Rows.Add(row);
                //row = new PrintRow("Pur 0%", PrintRowPixel, 280, PrintFont);
                //PrintBill.Rows.Add(row);
                row = new PrintRow("Amt 6%", PrintRowPixel, 450, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("VAT 6%", PrintRowPixel, 510, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amt 13.5%", PrintRowPixel, 570, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("VAT 13.5%", PrintRowPixel, 630, PrintFont);
                PrintBill.Rows.Add(row);

                row = new PrintRow("RoundOFF", PrintRowPixel, 690, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 720, PrintFont);
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
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            if (General.PharmaSYSDistributorPlusLicense.LicenseType == LicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }       
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
                column.Name = "Col_ACCID";               
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";              
                column.HeaderText = "TYPE";
                column.Width = 40;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";              
                column.HeaderText = "No";
                column.DefaultCellStyle.Format = "N";
                column.ValueType = typeof(Int32);
                column.SortMode = DataGridViewColumnSortMode.Automatic;
                column.Width = 40;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";              
                column.HeaderText = "DATE";
                column.Width = 75;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillType";               
                column.HeaderText = "BillType";
                column.DefaultCellStyle.Format = "N";
                column.Width = 40;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";              
                column.HeaderText = "VouNumber";
                column.DefaultCellStyle.Format = "N";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

               

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";               
                column.HeaderText = "Party";
                column.Width = 160;
                dgvReportList.Columns.Add(column);              

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount5Percent";             
                column.HeaderText = "Amt.6%";
                column.Width = 90;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT5Percent";               
                column.HeaderText = "VAT.6%";
                column.Width = 85;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount12point5Percent";               
                column.HeaderText = "Amt 13.5%";
                column.Width = 90;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT12Point5Percent";              
                column.HeaderText = "VAT.13.5%";
                column.Width = 85;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RoundUpAmount";             
                column.HeaderText = "Round OFF";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";             
                column.HeaderText = "Amount";
                column.Width = 100;
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
              //  dgvReportList.DataSource = _BindingSource;
                FormatReportGrid();
                //CheckFilter();
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
                dtable = _CDNote.GetOverviewDataForVATReport(FixAccounts.VoucherTypeForCreditNoteStock, _MFromDate,_MToDate);               
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
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
            dgvReportList.DoubleColumnNames.Add("Col_AmountZeroPercent");
            dgvReportList.DoubleColumnNames.Add("Col_Amount5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_VAT5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_Amount12point5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_VAT12Point5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_TotalAdd");
            dgvReportList.DoubleColumnNames.Add("Col_TotalLess");
            dgvReportList.DoubleColumnNames.Add("Col_RoundUpAmount");
          //  dgvReportList.NumericColumnNames.Add("Col_VoucherNumber");
        }

        private void BindReportGrid()
        {
            try
            {
                if (dgvReportList.Rows.Count > 0)
                    dgvReportList.Rows.Clear();

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (txtType.Text.ToString() == "" || (dr["VoucherType"].ToString() == txtType.Text.ToString()))
                    {
                        int rowIndex = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                        dgvrow.Cells["Col_ID"].Value = dr["CRDBID"].ToString();
                        dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        if (dr["VoucherNumber"] != null)
                            dgvrow.Cells["Col_VoucherNumber"].Value = Convert.ToInt32(dr["VoucherNumber"]);
                        dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        if (dr["ClearedInVoucherType"] != DBNull.Value)
                            dgvrow.Cells["Col_BillType"].Value = dr["ClearedInVoucherType"].ToString();
                        if (dr["ClearedInVoucherNumber"] != DBNull.Value)
                            dgvrow.Cells["Col_BillNumber"].Value = dr["ClearedInVoucherNumber"].ToString();
                        dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        dgvrow.Cells["Col_Amount5Percent"].Value = dr["Amount5"].ToString();
                        dgvrow.Cells["Col_VAT5Percent"].Value = dr["VAT5"].ToString();
                        dgvrow.Cells["Col_Amount12point5Percent"].Value = dr["Amount12point5"].ToString();
                        dgvrow.Cells["Col_VAT12Point5Percent"].Value = dr["VAT12Point5"].ToString();
                        dgvrow.Cells["Col_RoundUpAmount"].Value = dr["RoundingAmount"].ToString();
                        dgvrow.Cells["Col_Amount"].Value = dr["AmountNet"].ToString();
                    }

                }
              //  dgvReportList.Sort(dgvReportList.Columns["Col_VoucherDate"], ListSortDirection.Ascending);

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
                    PrintReportHead = "VAT Sales Return Register  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = "";
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

        private void CalculateFinalTotals()
        {
           
            _MTotalAmount5 = 0;
            _MTotalVAT5 = 0;
            _MTotalAmount12point5 = 0;
            _MTotalVAT12point5 = 0;
            _MTotalRoundoff = 0;
            _MTotalAmount = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                  //  _MTotalZero += Convert.ToDouble(dr.Cells["Col_AmountZeroPercent"].Value.ToString());
                    _MTotalAmount5 += Convert.ToDouble(dr.Cells["Col_Amount5Percent"].Value.ToString());
                    _MTotalVAT5 += Convert.ToDouble(dr.Cells["Col_VAT5Percent"].Value.ToString());
                    _MTotalAmount12point5 += Convert.ToDouble(dr.Cells["Col_Amount12point5Percent"].Value.ToString());
                    _MTotalVAT12point5 += Convert.ToDouble(dr.Cells["Col_VAT12Point5Percent"].Value.ToString());
                    _MTotalRoundoff += Convert.ToDouble(dr.Cells["Col_RoundUpAmount"].Value.ToString());
                }
                int rowIndex = dgvReportList.Rows.Add();
                DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                dgvrow.Cells["Col_AccName"].Value = "Total";
            //    dgvrow.Cells["Col_AmountZeroPercent"].Value = _MTotalZero.ToString("#0.00");
                dgvrow.Cells["Col_Amount5Percent"].Value = _MTotalAmount5.ToString("#0.00");
                dgvrow.Cells["Col_VAT5Percent"].Value = _MTotalVAT5.ToString("#0.00");
                dgvrow.Cells["Col_Amount12point5Percent"].Value = _MTotalAmount12point5.ToString("#0.00");
                dgvrow.Cells["Col_VAT12Point5Percent"].Value = _MTotalVAT12point5.ToString("#0.00");
                dgvrow.Cells["Col_RoundUpAmount"].Value = _MTotalRoundoff.ToString("#0.00");
                dgvrow.Cells["Col_Amount"].Value = _MTotalAmount.ToString("#0.00");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        #endregion

        # region Events

        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {

        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {
               // ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion

        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }
      
    }
}
