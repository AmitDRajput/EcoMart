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
    public partial class UclMISListProfitDay : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotal1;
        private double _MTotal2;
        private double _MTotal3;
        private double _MTotal4;
        private double _MTotal5;
      //  private double _MTotal6;
        #endregion

        # region Constructor
        public UclMISListProfitDay()
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
                _SaleList = new SaleList();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "MIS - PROFIT % DAYWISE";
                InitializeReportGrid();
                ClearControls();
                AddToolTip();
                fromDate1.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override void SetFocus()
        {
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
            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                if (pnlMultiSelection1.Visible == true)
                {

                    retValue = true;
                }

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

        #endregion

        #region IReportview
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
            PrintReportHead = "Profit Daywise From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
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
                PrintReportHead = "PROFIT DAYWISE From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
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

                    if (dr.Cells["Col_VoucherDate"].Value != null)
                    {
                        if (PrintRowCount > FixAccounts.NumberOfRowsPerReport)
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
                        if (dr.Cells["Col_VoucherDate"].Value.ToString() == "Total")
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);                           
                        }
                        else
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_SaleAmount"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_SaleAmount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(40.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_SaleByPurchaseRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_SaleByPurchaseRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(140.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ProfitInRs"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_ProfitInRs"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(240.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ProfitPercentBySaleRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_ProfitPercentBySaleRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(340.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ProfitPercentByPurchaseRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_ProfitPercentByPurchaseRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(440.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
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

        public int PrintHead()
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
                row = new PrintRow("Sale in Rs.", PrintRowPixel, 55, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Sale By Pur Rate", PrintRowPixel, 140, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Profit In Rs.", PrintRowPixel, 255, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Profit % by Sale Rate", PrintRowPixel, 340, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Profit % By Pur Rate", PrintRowPixel, 470, PrintFont);
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
        #endregion IReportControl

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
                HidepnlGO();

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
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

             
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";              
                column.HeaderText = "DATE";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleAmount";              
                column.HeaderText = "Sale Amount";
                column.Width = 150;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleByPurchaseRate";             
                column.HeaderText = "SaleByPurRate";
                column.Width = 150;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRs";            
                column.HeaderText = "Profit In Rs";
                column.Width = 150;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";              
                column.HeaderText = "Profit % By Sale Rate";
                column.Width = 150;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";              
                column.HeaderText = "Profit % By Purchase Rate";
                column.Width = 150;
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
                dgvReportList.DataSource = _BindingSource;
                BindReportGrid();
                CalculateFinalTotals();
                NoofRows();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_ProfitPercentByPurchaseRate");
            dgvReportList.DoubleColumnNames.Add("Col_ProfitPercentBySaleRate");
            dgvReportList.DoubleColumnNames.Add("Col_ProfitInRs");
            dgvReportList.DoubleColumnNames.Add("Col_SaleByPurchaseRate");
            dgvReportList.DoubleColumnNames.Add("Col_SaleAmount");
        }
      
        private void BindReportGrid()
        {
            double msaleamt = 0;
            double mpuramt = 0;
            double profitbysale = 0;
            double profitbypur = 0;
            _MTotal1 = 0;
            _MTotal2 = 0;
            _MTotal3 = 0;
            _MTotal4 = 0;
            _MTotal5 = 0;
            try
            {                         
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    msaleamt = 0;
                    mpuramt = 0;
                    profitbysale = 0;
                    profitbypur = 0;
                    if (dr["VoucherDate"].ToString() != null)
                    {
                        int rowIndex = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                        dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        if (dr["AmountNet"] != DBNull.Value)
                        {
                            dgvrow.Cells["Col_SaleAmount"].Value = dr["AmountNet"].ToString();
                            msaleamt = Convert.ToDouble(dr["AmountNet"].ToString());
                            _MTotal1 += msaleamt;
                        }
                        if (dr["AmountByPurchaseRate"] != DBNull.Value)
                        {
                            dgvrow.Cells["Col_SaleByPurchaseRate"].Value = dr["AmountByPurchaseRate"].ToString();
                            mpuramt = Convert.ToDouble(dr["AmountByPurchaseRate"].ToString());
                            _MTotal2 += mpuramt;
                        }
                        if (dr["ProfitInRupees"] != DBNull.Value)
                        {
                            dgvrow.Cells["Col_ProfitInRs"].Value = dr["ProfitInRupees"].ToString();
                            _MTotal3 += Convert.ToDouble(dr["ProfitInRupees"].ToString());
                        }
                        if (dr["ProfitPercentBySaleRate"] != DBNull.Value)
                        {
                            profitbysale = Math.Round(((msaleamt - mpuramt)*100) / msaleamt,2);
                            dgvrow.Cells["Col_ProfitPercentBySaleRate"].Value = profitbysale.ToString();
                            _MTotal4 = Math.Round(((_MTotal1 - _MTotal2) * 100) / _MTotal1, 2);
                        }
                        if (dr["ProfitPercentByPurchaseRate"] != DBNull.Value)
                        {
                            profitbypur = Math.Round(((msaleamt - mpuramt) * 100) / mpuramt , 2);
                            dgvrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = profitbypur.ToString();
                            _MTotal5 = Math.Round(((_MTotal1 - _MTotal2) * 100) / _MTotal2, 2);
                        }                     
                        
                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void CalculateFinalTotals()
        {
            int rowIndex = dgvReportList.Rows.Add();
            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
            dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
            dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
            dgvrow.Cells["Col_VoucherDate"].Value = "Total";
            dgvrow.Cells["Col_SaleAmount"].Value = _MTotal1.ToString("#0.00");
            dgvrow.Cells["Col_SaleByPurchaseRate"].Value = _MTotal2.ToString("#0.00");
            dgvrow.Cells["Col_ProfitInRs"].Value = _MTotal3.ToString("#0.00");
            dgvrow.Cells["Col_ProfitPercentBySaleRate"].Value = _MTotal4.ToString("#0.00");
            dgvrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _MTotal5.ToString("#0.00");
        }
        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }
        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _SaleList.GetOverviewDataProfitPercentDay(_MFromDate,_MToDate);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region Events      
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
                    InitializeReportGrid();
                    ShowpnlGO();                   
                    lblFooterMessage.Text = "";
                    ViewFromDate.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
                    ViewToDate.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);
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
     
        #endregion

        #region AddToolTip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion AddToolTip

        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelection1_KeyDown(object sender, KeyEventArgs e)
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
                btnOKMultiSelectionClick();
        }
    }
}
