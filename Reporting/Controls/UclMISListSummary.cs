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
using EcoMart.InterfaceLayer;
using EcoMart.Printing;
using EcoMart.Reporting;
using EcoMart.Reporting.Base;
using PrintDataGrid;


namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclMISListSummary : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        #endregion

        # region Constructor
        public UclMISListSummary()
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
                _MFromDate = DateTime.Now.Date.ToString("yyyyMMdd");
                _MToDate = DateTime.Now.Date.ToString("yyyyMMdd");
                headerLabel1.Text = "MIS - SUMMARY";
                ClearControls(); 
                dgvReportList.Focus();
                HidepnlGO();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region IReport Members

        public DataTable GetReportData()
        {
            return _BindingSource.Copy();
        }
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
              //  string reportHeading = General.GetReportHeading();
               // reportHeading += "Summary From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
               //// PrintDataGrid.PrintDGV.Print_DataGridView(dgvReportList.GridView, reportHeading, true, true);

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

                    if (dr.Cells["Col_Description"].Value != null || dr.Cells["Col_Description"].Value != null)
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
                        if (dr.Cells["Col_Description"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Description"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                      

                        if (dr.Cells["Col_Amount"].Value != null && Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString()) != 0)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(360.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
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


                row = new PrintRow("Description", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount (Rs)", PrintRowPixel, 365, PrintFont);
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
        #endregion IReport Members

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
            fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
            toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);

        }
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
          //  dgvReportList.Columns["Col_ID"].Visible = false;
            FormatReportGrid();
           // dgvReportList.InitializeColumnContextMenu();
        }
        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
        }
        private void FormatReportGrid()
        {
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
        }
        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.Visible = true;
                column.HeaderText = "Vou Type";
                column.Width = 90;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Description";
                column.HeaderText = "Description";
                column.Width = 300;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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
                FormatGrid();
                BindGrid();
                int noofrecords = dgvReportList.Rows.Count;
                if (noofrecords == 0)
                    lblFooterMessage.Text = "NO Records ";
                else if (noofrecords == 1)
                    lblFooterMessage.Text = "Record : " + noofrecords.ToString().Trim();
                else
                    lblFooterMessage.Text = "Records : " + noofrecords.ToString().Trim();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void BindGrid()
        {
            double _TotalSale = 0;
            bool _IfTotalSalePrinted = false;
            try
            {

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    int rowindex = dgvReportList.Rows.Add();
                    DataGridViewRow gdr = dgvReportList.Rows[rowindex];                   
                    double amount = 0;
                    amount = Convert.ToDouble(dr["AmountNet"].ToString());
                   
                    if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCashSale)
                    {
                        gdr.Cells["Col_Description"].Value = "Cash Sale";
                        _TotalSale += amount;
                    }
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCreditSale)
                    {
                        gdr.Cells["Col_Description"].Value = "Credit Sale";
                        _TotalSale += amount;
                    }
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCreditStatementSale)
                    {
                        gdr.Cells["Col_Description"].Value = "Credit Statement Sale";
                        _TotalSale += amount;
                    }

                    

                    //else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForDistributorSaleCash)
                    //{
                    //    gdr.Cells["Col_Description"].Value = "WholeSale Cash Sale";
                    //    _TotalSale += amount;
                    //}
                    //else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForDistributorSaleCredit)
                    //{
                    //    gdr.Cells["Col_Description"].Value = "WholeSale Credit Sale";
                    //    _TotalSale += amount;
                    //}
                    //else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForDistributorSaleCreditStatement)
                    //{
                    //    gdr.Cells["Col_Description"].Value = "WholeSale Credit Statement Sale";
                    //    _TotalSale += amount;
                    //}

                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForVoucherSale)
                    {
                        gdr.Cells["Col_Description"].Value = "Voucher Sale";
                        _TotalSale += amount;
                    }
                    else if (!_IfTotalSalePrinted)
                    {
                       
                       // gdr.Cells["Col_ID"].Value = dr["VoucherType"].ToString(); 
                        gdr.Cells["Col_Description"].Value = "Total Sale";
                        gdr.Cells["Col_Amount"].Value = _TotalSale.ToString("#0,00");
                        gdr.DefaultCellStyle.BackColor = Color.MistyRose;
                        rowindex = dgvReportList.Rows.Add();
                        gdr = dgvReportList.Rows[rowindex];
                        _IfTotalSalePrinted = true;
                    }
                    if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCashPurchase)
                        gdr.Cells["Col_Description"].Value = "Cash Purchase";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCreditPurchase)
                        gdr.Cells["Col_Description"].Value = "Credit Purchase";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCreditStatementPurchase)
                        gdr.Cells["Col_Description"].Value = "Credit Statement Purchase";

                    if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCreditNoteAmount)
                        gdr.Cells["Col_Description"].Value = "Credit Note Amount";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCreditNoteStock)
                        gdr.Cells["Col_Description"].Value = "Credit Note Stock";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForDebitNoteAmount)
                        gdr.Cells["Col_Description"].Value = "Debit Note Amount";
                    if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForDebitNoteStock)
                        gdr.Cells["Col_Description"].Value = "Debit Note Stock";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForStockIN)
                        gdr.Cells["Col_Description"].Value = "StockIN";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForStockOut)
                        gdr.Cells["Col_Description"].Value = "StockOut";
                    //else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForDistributorCreditNoteStock)
                    //    gdr.Cells["Col_Description"].Value = "WholeSale Credit Note Stock";

                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCashReceipt)
                        gdr.Cells["Col_Description"].Value = "Cash Receipt";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCashPayment)
                        gdr.Cells["Col_Description"].Value = "Cash Payment";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForCashExpenses)
                        gdr.Cells["Col_Description"].Value = "Cash Expenses";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForBankReceipt)
                        gdr.Cells["Col_Description"].Value = "Bank Receipt";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForBankPayment)
                        gdr.Cells["Col_Description"].Value = "Bank Payment";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForBankExpenses)
                        gdr.Cells["Col_Description"].Value = "Bank Expenses";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForContraEntry)
                        gdr.Cells["Col_Description"].Value = "Contra Entries";
                    else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForChequeReturn)
                        gdr.Cells["Col_Description"].Value = "Cheque Return";
                    gdr.Cells["Col_ID"].Value = dr["VoucherType"].ToString();
                    gdr.Cells["Col_Amount"].Value = amount.ToString("#0,00");

                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FormatGrid()
        {
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
        }

        private void FillReportData()
        {
            try
            {
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                bool  retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    DataTable dtable = new DataTable();
                    dtable = _SaleList.GetDataForSummary(_MFromDate, _MToDate);
                    _BindingSource = dtable;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
            _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
            retValue = General.CheckDates(_MFromDate, _MToDate);
            if (retValue)
            {
                ShowpnlGO();
                PrintReportHead = "Summary  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "";
                FillReportGrid();
            }
            else
            {
                lblFooterMessage.Text = "Check Date";
            }
        }
        #endregion

        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            try
            {
                btnOKMultiSelectionClick();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }
}
