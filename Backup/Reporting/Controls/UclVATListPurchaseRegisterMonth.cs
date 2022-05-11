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
using System.IO;


namespace PharmaSYSRetailPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclVATListPurchaseRegisterMonth : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceAll;
        private Purchase _Purchase;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;      
        private double _MTotalZero = 0;
        private double _MTotalAmount5 = 0;
        private double _MTotalVAT5 = 0;
        private double _MTotalAmount12point5 = 0;
        private double _MTotalVAT12point5 = 0;
        private double _MTotalLess = 0;
        private double _MTotalAdd = 0;
        private double _MTotalRoundoff = 0;       
        #endregion

        # region Constructor
        public UclVATListPurchaseRegisterMonth()
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
                _BindingSourceAll = new DataTable();
                _Purchase = new Purchase();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "VAT - PURCHASE REGISTER MONTHWISE SUMMARY";
                ConstructReportColumns();
                HidepnlGO();
                ClearControls();
                
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
            btnOKMultiSelection1.Focus();
        }     
      
        #endregion IOverView Members

        # region IReport Members

        public override void Export(string ExportFileName)
        {
            base.Export(ExportFileName);

            string filename = "Report";
            if (ExportFileName != null && ExportFileName.ToString() != string.Empty)
                filename = ExportFileName.ToString().Trim();
            string filePath = "d:\\reports\\" + filename + ".csv";
            if (File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            PrintReportHead = "VAT Purchase Register (Month) From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
            PrintReportHead2 = "[" + txtViewtext.Text.ToString() + "]";
            try
            {
                GeneralReport.ExportPrintHeader(PrintReportHead, PrintReportHead2, filePath);
                GeneralReport.ExportPrintDetails(dgvReportList, filePath);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override void EMail(string ExportFileName, string EmailID)
        {
            base.EMail(ExportFileName, EmailID);
            string filename = "Report";
            string tosendemailID = "sheelabsharma@gmail.com";
            if (ExportFileName != null && ExportFileName.ToString() != string.Empty)
                filename = ExportFileName.ToString().Trim();
            if (EmailID != null && EmailID.ToString() != string.Empty)
                tosendemailID = EmailID.ToString().Trim();
            string filePath = "d:\\reports\\" + filename + ".csv";
            if (File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            try
            {
                GeneralReport.ExportPrintHeader(PrintReportHead, PrintReportHead2, filePath);
                GeneralReport.EmailPrintDetails(dgvReportList, filePath, tosendemailID);
                // Sir here I am sending grid,filepath for all reports

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                        mamt = 0;
                        if (dr.Cells["Col_Month"].Value == null && dr.Cells["Col_Amount"].Value != null)
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                        }
                       
                            if (dr.Cells["Col_Month"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Month"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AmountZeroPercent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AmountZeroPercent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(70.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_Amount5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(150.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_VAT5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_VAT5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(230.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Amount12point5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount12point5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(310.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VAT12Point5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_VAT12Point5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(390.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_TotalLess"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_TotalLess"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(470.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_TotalAdd"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_TotalAdd"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(550.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_RoundUpAmount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_RoundUpAmount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(630.00 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
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
                row = new PrintRow("Month", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pur.0%VAT", PrintRowPixel, 100, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pur. 5%VAT", PrintRowPixel, 170, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("VAT 5%", PrintRowPixel, 260, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pur.12.5%VAT", PrintRowPixel, 330, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("VAT 12.5%", PrintRowPixel, 420, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Less", PrintRowPixel, 500, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Add", PrintRowPixel, 590, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("R.OFF", PrintRowPixel, 640, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Purchase Amount", PrintRowPixel, 680, PrintFont);
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
                column.HeaderText = "ID";
                column.Width = 50;
                column.Visible = false;
                dgvReportList.Columns.Add(column);         

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";              
                column.HeaderText = "TYPE";
                column.Width = 50;
                dgvReportList.Columns.Add(column);              

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Month";               
                column.HeaderText = "Month";
                column.Width = 100;
                dgvReportList.Columns.Add(column);              

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountZeroPercent";              
                column.HeaderText = "Pur.0 VAT";
                column.Width = 100;               
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount5Percent";              
                column.HeaderText = "Pur.5%";
                column.Width = 100;             
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT5Percent";             
                column.HeaderText = "VAT.5%";
                column.Width = 100;              
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount12point5Percent";              
                column.HeaderText = "Pur.12.5%";
                column.Width = 100;              
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT12Point5Percent";              
                column.HeaderText = "VAT.12.5%";
                column.Width = 100;             
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);
               
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TotalLess";              
                column.HeaderText = "Less";
                column.Width = 100;               
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TotalAdd";              
                column.HeaderText = "ADD";
                column.Width = 100;               
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);               

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RoundUpAmount";              
                column.HeaderText = "Round OFF";
                column.Width = 45;
                column.DefaultCellStyle.Format = "N2";
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
                dgvReportList.DataSource = _BindingSource;
                if (rbtnAll.Checked == true)
                {                  
                    dgvReportList.Columns["Col_VoucherType"].Visible = false;                    
                }
                else
                {                   
                    dgvReportList.Columns["Col_VoucherType"].Visible = true;                   
                }

                FormatReportGrid();
                CheckFilter();
                BindReportGrid();
                CalculateFinalTotals();
                NoofRows();
              
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
                if (rbtnAll.Checked)
                    dtable = _Purchase.GetOverviewDataForVATReportMONTHALL(_MFromDate, _MToDate);
                else
                dtable = _Purchase.GetOverviewDataForVATReportMONTH(_MFromDate, _MToDate);
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
            dgvReportList.DoubleColumnNames.Add("Col_TotalLess");
            dgvReportList.DoubleColumnNames.Add("Col_TotalAdd");
            dgvReportList.DoubleColumnNames.Add("Col_RoundUpAmount");
        }

        private void BindReportGrid()
        {
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    int rowIndex = dgvReportList.Rows.Add();
                    DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                    dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    int mm = General.ConvertStringToDateyyyyMMdd(dr["VoucherDate"].ToString()).Month;
                    dgvrow.Cells["Col_Month"].Value = General.GetMonthAsStringShort(mm-1);
                    dgvrow.Cells["Col_AmountZeroPercent"].Value = dr["AmountPurchaseZeroVAT"].ToString();
                    dgvrow.Cells["Col_Amount5Percent"].Value = dr["AmountPurchase5PercentVAT"].ToString();
                    dgvrow.Cells["Col_VAT5Percent"].Value = dr["AmountVAT5Percent"].ToString();
                    dgvrow.Cells["Col_Amount12point5Percent"].Value = dr["AmountPurchase12point5PercentVAT"].ToString();
                    dgvrow.Cells["Col_VAT12Point5Percent"].Value = dr["AmountVAT12Point5Percent"].ToString();
                    dgvrow.Cells["Col_TotalLess"].Value = dr["TotalLess"].ToString();
                    dgvrow.Cells["Col_TotalAdd"].Value = dr["TotalAdd"].ToString();
                    dgvrow.Cells["Col_RoundUpAmount"].Value = dr["RoundUpAmount"].ToString();
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
                    ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
                    ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
                    FillReportGrid();
                    PrintReportHead = "VAT Purchase Report [MONTH] From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = "";
                    this.Cursor = Cursors.Default;
                    dgvReportList.Focus();
                }
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
                if (rbtnCash.Checked == true)
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+ FixAccounts.VoucherTypeForCashPurchase +"' and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" +  _MToDate + "'";
                else if (rbtnCreditStatement.Checked == true)
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+ FixAccounts.VoucherTypeForCreditStatementPurchase +"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" +  _MToDate + "'";
                else if (rbtnCashCredit.Checked == true)
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+ FixAccounts.VoucherTypeForCreditPurchase +"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" +  _MToDate + "'";
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
            _MTotalZero = 0;
            _MTotalAmount5 = 0;
            _MTotalVAT5 = 0;
            _MTotalAmount12point5 = 0;
            _MTotalVAT12point5 = 0;
            _MTotalLess = 0;
            _MTotalAdd = 0;
            _MTotalRoundoff = 0;
            _MTotalAmount = 0;

            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {
                _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                _MTotalZero += Convert.ToDouble(dr.Cells["Col_AmountZeroPercent"].Value.ToString());
                _MTotalAmount5 += Convert.ToDouble(dr.Cells["Col_Amount5Percent"].Value.ToString());
                _MTotalVAT5 += Convert.ToDouble(dr.Cells["Col_VAT5Percent"].Value.ToString());
                _MTotalAmount12point5 += Convert.ToDouble(dr.Cells["Col_Amount12point5Percent"].Value.ToString());
                _MTotalVAT12point5 += Convert.ToDouble(dr.Cells["Col_VAT12Point5Percent"].Value.ToString());
                _MTotalLess += Convert.ToDouble(dr.Cells["Col_TotalLess"].Value.ToString());
                _MTotalAdd += Convert.ToDouble(dr.Cells["Col_TotalAdd"].Value.ToString());
                _MTotalRoundoff += Convert.ToDouble(dr.Cells["Col_RoundUpAmount"].Value.ToString());
            }

            int rowIndex = dgvReportList.Rows.Add();
            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
            dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
            dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;

            dgvrow.Cells["Col_AmountZeroPercent"].Value = _MTotalZero.ToString("#0.00");
            dgvrow.Cells["Col_Amount5Percent"].Value = _MTotalAmount5.ToString("#0.00");
            dgvrow.Cells["Col_VAT5Percent"].Value = _MTotalVAT5.ToString("#0.00");
            dgvrow.Cells["Col_Amount12point5Percent"].Value = _MTotalAmount12point5.ToString("#0.00");
            dgvrow.Cells["Col_VAT12Point5Percent"].Value = _MTotalVAT12point5.ToString("#0.00");
            dgvrow.Cells["Col_TotalLess"].Value = _MTotalLess.ToString("#0.00");
            dgvrow.Cells["Col_TotalAdd"].Value = _MTotalAdd.ToString("#0.00");
            dgvrow.Cells["Col_RoundUpAmount"].Value = _MTotalRoundoff.ToString("#0.00");
            dgvrow.Cells["Col_Amount"].Value = _MTotalAmount.ToString("#0.00");

        }       

        #endregion

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        
        #endregion    

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
