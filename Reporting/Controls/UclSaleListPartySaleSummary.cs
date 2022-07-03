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
using PrintDataGrid;

namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclSaleListPartySaleSummary : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotAmountCash = 0;
        private double _MTotAmountCredit = 0;
        private double _MTotAmountCreditStatement = 0;

        #endregion

        #region Constructor
        public UclSaleListPartySaleSummary()
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
                headerLabel1.Text = "PARTYWISE SALE SUMMARY";
                ClearControls();
                HidepnlGO();
                AddToolTip();
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

            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }

        #endregion

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
                totalrows = CalculateTotalRows(totalrows);
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead(_MFromDate, _MToDate);
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
                        PrintHead(_MToDate, _MToDate);
                    }
                    PrintRowPixel += 17;
                    PrintRowCount += 1;
                    double mamt = 0;
                    if (dr.Cells["Col_Name"].Value != null)
                    {
                        row = new PrintRow(dr.Cells["Col_Name"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                    }

                    if (dr.Cells["Col_Address"].Value != null)
                    {
                        row = new PrintRow(dr.Cells["Col_Address"].Value.ToString().PadRight(20), PrintRowPixel, 250, PrintFont);
                        PrintBill.Rows.Add(row);
                    }
                    if (dr.Cells["Col_CashAmount"].Value != null)
                    {
                        mamt = Convert.ToDouble(dr.Cells["Col_CashAmount"].Value.ToString());
                        mlen = (mamt.ToString("#0.00").Length);
                        colpix = Convert.ToInt32(480.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        PrintBill.Rows.Add(row);
                    }
                    if (dr.Cells["Col_CreditAmount"].Value != null)
                    {
                        mamt = Convert.ToDouble(dr.Cells["Col_CreditAmount"].Value.ToString());
                        mlen = (mamt.ToString("#0.00").Length);
                        colpix = Convert.ToInt32(580.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        PrintBill.Rows.Add(row);
                    }

                    if (dr.Cells["Col_CreditStatementAmount"].Value != null)
                    {
                        mamt = Convert.ToDouble(dr.Cells["Col_CreditStatementAmount"].Value.ToString());
                        mlen = (mamt.ToString("#0.00").Length);
                        colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        PrintBill.Rows.Add(row);
                    }

                }
                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
                if (_MTotAmountCash > 0)
                {
                    mlen = (_MTotAmountCash.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(480.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                    row = new PrintRow(_MTotAmountCash.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                }
                if (_MTotAmountCredit > 0)
                {
                    mlen = (_MTotAmountCredit.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(580.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                    row = new PrintRow(_MTotAmountCredit.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                }
                if (_MTotAmountCreditStatement > 0)
                {
                    mlen = (_MTotAmountCreditStatement.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                    row = new PrintRow(_MTotAmountCreditStatement.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                }
                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintFooter(PrintRowPixel);
                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public int CalculateTotalRows(int totrows)
        {

            return totrows;
        }

        private int PrintHead(string FromDate, string ToDate)
        {
            PrintRow row;
            try
            {
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(_MFromDate), General.GetDateInDateFormat(_MToDate));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Party", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Address", PrintRowPixel, 250, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Cash", PrintRowPixel, 520, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Credit", PrintRowPixel, 620, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("CreditStatement", PrintRowPixel, 670, PrintFont);
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
        private int PrintFooter(int PrintRowPixel) // [06.02.2017]
        {
            try
            {
                PrintRowPixel = GeneralReport.PrintFooter(PrintTotalPages, PrintFont, PrintRowPixel, PrintPageNumber);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 0;
            return PrintRowCount;
        }
        private void InitializeDates()
        {
            _MFromDate = General.ShopDetail.Shopsy;
            _MToDate = General.ShopDetail.Shopey;
            fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
            toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);
        }
        public void ClearControls()
        {
            try
            {
                //fromDate1.Value = DateTime.Now;
                //toDate1.Value = DateTime.Now;
                InitializeDates();
                lblFooterMessage.Text = "";
                txtCrStatmentTotal.Text = "";
                InitializeReportGrid();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion

        # region Other Private methods
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
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Party";
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "PatientAddress1";
                column.HeaderText = "Address";
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CashAmount";
                column.DataPropertyName = "AmountNetCash";
                column.HeaderText = "Cash Amount";
                column.Width = 120;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreditAmount";
                column.DataPropertyName = "AmountNetCredit";
                column.HeaderText = "Credit Amount";
                column.Width = 120;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreditStatementAmount";
                column.DataPropertyName = "AmountNetCreditStatement";
                column.HeaderText = "Cr/Stmt Amount";
                column.Width = 120;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Format = "n2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);
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
            pnlMultiSelection.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Text = "";
            ViewToDate.Text = "";
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
            pnlMultiSelection.Visible = false;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            dgvReportList.Focus();
        }
        //private void RefreshSelectedRowCounter()
        //{
        //    selectedRowCount = dgvSelected.Rows.Count;
        //    txtNoofSearches.Text = selectedRowCount.ToString("#0");
        //}


        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
        }
        private void FillReportGrid()
        {
            try
            {
                FillReportData();
                FillGridData();
                double totamt = 0;


                txtCrStatmentTotal.Text = totamt.ToString("#0.00");

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

        private void FillGridData()
        {
            try
            {
                int _RowIndex = 0;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];

                _MTotAmountCash = 0;
                _MTotAmountCredit = 0;
                _MTotAmountCreditStatement = 0;

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    string drvouchertype = "";
                    double dramount = 0;
                    string drpatient = "";
                    string draddress = "";
                    string drid = "";
                    if (dr["AccountID"] != DBNull.Value && dr["AccountID"].ToString() != "")
                        drid = dr["AccountID"].ToString();
                    else if (dr["PatientID"] != DBNull.Value)
                        drid = dr["PatientID"].ToString();
                    if (dr["VoucherType"] != DBNull.Value)
                        drvouchertype = dr["VoucherType"].ToString();
                    if (dr["AmountNet"] != DBNull.Value)
                        dramount = Convert.ToDouble(dr["AmountNet"].ToString());
                    if (dr["PatientName"] != DBNull.Value)
                        drpatient = dr["PatientName"].ToString();
                    if (dr["PatientAddress1"] != DBNull.Value)
                        draddress = dr["PatientAddress1"].ToString();
                    if (dramount > 0)
                    {
                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = drid;
                        currentdr.Cells["Col_Name"].Value = drpatient;
                        currentdr.Cells["Col_Address"].Value = draddress;
                        if (drvouchertype == FixAccounts.VoucherTypeForCashSale)
                        {
                            currentdr.Cells["Col_CashAmount"].Value = dramount.ToString("#0.00");
                            _MTotAmountCash += dramount;
                        }
                        else
                        {
                            if (drvouchertype == FixAccounts.VoucherTypeForCreditSale)
                            {
                                currentdr.Cells["Col_CreditAmount"].Value = dramount.ToString("#0.00");
                                _MTotAmountCredit += dramount;
                            }
                            else
                            {
                                if (drvouchertype == FixAccounts.VoucherTypeForCreditStatementSale)
                                {
                                    currentdr.Cells["Col_CreditStatementAmount"].Value = dramount.ToString("#0.00");
                                    _MTotAmountCreditStatement += dramount;
                                }
                            }
                        }
                    }
                }
                txtCashTotal.Text = _MTotAmountCash.ToString("#0.00");
                txtCreditTotal.Text = _MTotAmountCredit.ToString("#0.00");
                txtCrStatmentTotal.Text = _MTotAmountCreditStatement.ToString("#0.00");
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
                dtable = _SaleList.GetOverviewDataForAllPartySummary(_MFromDate, _MToDate);
                _BindingSource = dtable;
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
        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (dgvReportList.SelectedRow.Cells[0].Value != null)
                    {
                        string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                        ReportControl = new UclSaleListPartywiseBills();
                        ShowReportForm(selectedID, "", _MFromDate, _MToDate);
                    }
                    this.Cursor = Cursors.Default;
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
            InitializeReportGrid();
            _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
            _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
            retValue = General.CheckDates(_MFromDate, _MToDate);
            if (retValue)
            {
                this.Cursor = Cursors.WaitCursor;
                ShowpnlGO();
                FillReportGrid();
                this.Cursor = Cursors.Default;

                NoofRows();
                PrintReportHead = "Sales Report [Patient Summay]"; // From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "";
                dgvReportList.Focus();
            }
            else
                lblFooterMessage.Text = "Please Check Date...";
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion

        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {

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
