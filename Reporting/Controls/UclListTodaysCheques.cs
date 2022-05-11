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
using PrintDataGrid;
using EcoMart.Printing;

namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclListTodaysCheques : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private BankPayment _BankPayment;
        private string _VouType;
        private string _VouType2;
        private string _MFromDate;
        private string _MToDate;
        private double _TotalAmount = 0;

        # endregion

        # region Contructor
        public UclListTodaysCheques()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclBankPayment();
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
                _BankPayment = new BankPayment();
                _VouType = FixAccounts.VoucherTypeForBankPayment;
                _VouType2 = FixAccounts.VoucherTypeForBankExpenses;
                headerLabel1.Text = "VOUCHER LIST-CHEQUE PAYMENT LIST";
                ClearControls();
                HidepnlGO();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void SetFocus()
        {
            FromDate.Focus();
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Home)
            {
                pnlMultiSelection.Visible = true;
                tsbtnPrint.Enabled = false;
                retValue = true;
            }
            if (keyPressed == Keys.End)
            {
                btnOKMultiSelectionClick();
                retValue = true;
            }
            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                if (pnlMultiSelection.Visible == true)
                {

                    retValue = true;
                }

            }
            if (keyPressed == Keys.G && modifier == Keys.Alt)
            {
                if (pnlMultiSelection.Visible == true)
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
                Font fnt = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 0;
                int rowcount = 0;
                PrintRowPixel = 0;
                double totpages = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                int totalpages = Convert.ToInt32(totpages);
                PrintHeader(totalpages, rowcount, fnt);
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_ID"].Value != null)
                    {
                        if (rowcount > FixAccounts.NumberOfRowsPerReport)
                        {
                            PrintRowPixel = 325;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, fnt);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintHeader(totalpages, rowcount, fnt);

                            rowcount = 0;
                        }
                        PrintRowPixel += 17;
                        rowcount += 1;
                        row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 1, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString(), PrintRowPixel, 45, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 100, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString(), PrintRowPixel, 160, fnt);
                        PrintBill.Rows.Add(row);
                        int mlen = Math.Min((dr.Cells["Col_Address"].Value.ToString().Length), 20);

                        string madd = (dr.Cells["Col_Address"].Value.ToString()).Substring(0, mlen);
                        row = new PrintRow(madd, PrintRowPixel, 360, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_ChequeNumber"].Value.ToString(), PrintRowPixel, 500, fnt);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_ChequeDate"].Value.ToString()), PrintRowPixel, 600, fnt);
                        PrintBill.Rows.Add(row);
                        double mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 700, fnt);
                        PrintBill.Rows.Add(row);

                    }
                }
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;
                row = new PrintRow(_TotalAmount.ToString("#0.00"), PrintRowPixel, 700, fnt);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                PrintBill.Print_Bill();
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
                string reportHead = "Cheques Paid From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);

                PrintRowPixel = PrintRowPixel + 36;

                row = new PrintRow(General.ShopDetail.ShopName, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Report Date : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow(General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), PrintRowPixel, 680, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(General.ShopDetail.ShopAddress1, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Report Time : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow(DateTime.Now.TimeOfDay.ToString().Substring(0, 5), PrintRowPixel, 680, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(General.ShopDetail.ShopTelephone, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                row = new PrintRow("Page Number : ", PrintRowPixel, 580, fnt);
                PrintBill.Rows.Add(row);
                PrintPageNumber += 1;
                string page = PrintPageNumber.ToString() + "/" + TotalPages.ToString();
                row = new PrintRow(page, PrintRowPixel, 680, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel = PrintRowPixel + 17;

                row = new PrintRow(reportHead + "     ", PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Type", PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Number", PrintRowPixel, 40, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 100, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 160, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Address", PrintRowPixel, 360, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Cheque No.", PrintRowPixel, 500, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Chqeque Date", PrintRowPixel, 600, fnt);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 700, fnt);

                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, fnt);
                PrintBill.Rows.Add(row);
                // PrintRowPixel += 17;

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            Rowcount = 1;
            return Rowcount;
        }
        public void ClearControls()
        {
            try
            {
                FromDate.Value = DateTime.Now;
                ToDate.Value = DateTime.Now;
                _VouType = FixAccounts.VoucherTypeForBankPayment;
                InitializeReportGrid();
                dgvReportList.Bind();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        # endregion

        # region Construct and Fill
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            _BindingSource = new DataTable();
            dgvReportList.DataSource = _BindingSource;
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlMultiSelection.Visible = true;
            InitializeReportGrid();
            tsbtnPrint.Enabled = false;
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
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
                column.DataPropertyName = "CBID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "AccountID";
                column.Visible = false;
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChequeNumber";
                column.DataPropertyName = "ChequeNumber";
                column.HeaderText = "CHQ.Number";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChequeDate";
                column.DataPropertyName = "ChequeDate";
                column.HeaderText = "CHQ.Date";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "AmountNet";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BankName";
                column.DataPropertyName = "BankName";
                column.HeaderText = "Bank";
                column.Width = 150;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                FormatGrid();
                dgvReportList.Bind();
                GetTotals();
                NoofRows();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void GetTotals()
        {
            _TotalAmount = 0;
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {
                if (dr.Cells["Col_Amount"].Value != null)
                {
                    _TotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                }
            }
            txtReportTotalAmount.Text = _TotalAmount.ToString("#0.00");
        }

        private void FormatGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
            dgvReportList.DateColumnNames.Add("Col_ChequeDate");
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
                _BindingSource = new DataTable();
                _BindingSource = _BankPayment.GetOverviewDataForTodaysCheques(_VouType, _VouType2, _MFromDate, _MToDate);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }



        # endregion

        # region Events
        private void btnOKMultiSelectionClick()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                InitializeReportGrid();
                _MFromDate = FromDate.Value.Date.ToString("yyyyMMdd");
                _MToDate = ToDate.Value.Date.ToString("yyyyMMdd");
                FillReportGrid();
                ShowpnlGO();
                PrintReportHead = "Cheques Paid From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                PrintReportHead2 = "";
                this.Cursor = Cursors.Default;
                dgvReportList.Focus();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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


        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }


        private void btnOKMultiSelection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }

        private void FromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ToDate.Focus();
        }

        private void ToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }


        # endregion Events



    }
}
