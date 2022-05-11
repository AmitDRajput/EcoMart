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
using System.IO;

namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclAcListBankBook : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private MPReports _MPReports;
        private Account _Account;
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
        private string _MAccountName = "";
        string DefaultBankID = "";
        #endregion

        # region Constructor
        public UclAcListBankBook()
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
                _Account = new Account();
                headerLabel1.Text = "ACCOUNT - BANK BOOK";
                ClearControls();
                FillVoucherTypes();
                FillBankCombo();
                GetDefaultBank();
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
                fromDate1.Focus();
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

                    if (dr.Cells["Col_VoucherType"].Value != null || dr.Cells["Col_Narr"].Value != null)
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
                    }



                }
                PrintFooter();
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

                //PrintRowPixel += 200;
                //row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);  // [Temporary]
                //PrintBill.Rows.Add(row);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 0;
            return PrintRowCount;
        }

        private int PrintFooter()
        {
            PrintRow row;
            try
            {
                PrintRowPixel = GeneralReport.PrintFooter(PrintTotalPages, PrintFont, PrintRowPixel, PrintPageNumber);

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

        #region Other Private methods

        public void ClearControls()
        {
            try
            {
                InitializeReportGrid();
                InitializeDates();
                lblFooterMessage.Text = "";
                mcbBank.SelectedID = "";
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
            txtViewtext.Text = "";
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
            txtViewtext.Text = mcbBank.SeletedItem.ItemData[2];
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
                column.Width = 75;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.HeaderText = "Party";
                column.Width = 280;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Narr";
                column.HeaderText = "Narration";
                column.Width = 230;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.HeaderText = "Type";
                column.Width = 40;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 65;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Debit";
                column.HeaderText = "Debit";
                column.Width = 120;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Credit";
                column.HeaderText = "Credit";
                column.Width = 120;
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
                CheckFilter();
                dgvReportList.DataSource = _BindingSource;
                FormatReportGrid();
                BindReportGrid(mcbBank.SelectedID, _MAccountName, "", _MOpeningDebit, _MOpeningCredit);
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
                dtable = _MPReports.GetGeneralLedger(_MFromDate, _MToDate, mcbBank.SelectedID);
                _BindingSource = dtable;

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillVoucherTypes()
        {
            try
            {
                mcbvoutype.SelectedID = null;
                mcbvoutype.SourceDataString = new string[3] { "ID", "VoucherType", "Code" };
                mcbvoutype.ColumnWidth = new string[3] { "0", "50", "0" };
                mcbvoutype.ValueColumnNo = 0;
                //  mcbvoutype.UserControlToShow = new UclAccount();
                _Account = new Account();
                DataTable dtable = _Account.GetVoucherTypes();
                mcbvoutype.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillBankCombo()
        {
            try
            {
                mcbBank.SelectedID = null;
                mcbBank.SourceDataString = new string[4] { "AccountID", "AccCode", "AccName", "AccAddress1" };
                mcbBank.ColumnWidth = new string[4] { "0", "20", "200", "200" };
                mcbBank.DisplayColumnNo = 2;
                mcbBank.ValueColumnNo = 0;
                mcbBank.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetBankAccountList();
                mcbBank.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public void GetDefaultBank()
        {
            DefaultBankID = General.GetDefaultBank();
            if (DefaultBankID != null)
                mcbBank.SelectedID = DefaultBankID;
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
                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                if (_MOpeningDebit >= 0 && _MOpeningCredit == 0)
                {
                    currentdr.Cells["Col_Narr"].Value = "Opening Balance Debit";
                    currentdr.Cells["Col_Debit"].Value = _MOpeningDebit.ToString("#0.00");
                }
                else
                {
                    currentdr.Cells["Col_Narr"].Value = "Opening Balance Credit";
                    currentdr.Cells["Col_Credit"].Value = _MOpeningCredit.ToString("#0.00");
                }
                //   int ifwhite = 0;
                Color mycolor = Color.WhiteSmoke;
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (dr["AccountID"].ToString().Trim() == AccID && Convert.ToInt32(dr["TransactionDate"].ToString()) >= Convert.ToInt32(_MFromDate) && Convert.ToInt32(dr["TransactionDate"].ToString()) <= Convert.ToInt32(_MToDate))
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
                            _MDebit = Convert.ToDouble(dr["Debit"].ToString());
                            _MCredit = Convert.ToDouble(dr["Credit"].ToString());
                            _MTrDebit += _MDebit;
                            _MTrCredit += _MCredit;
                            if (_MDebit > 0)
                                currentdr.Cells["Col_Debit"].Value = _MDebit.ToString("#0.00");
                            else
                                currentdr.Cells["Col_Credit"].Value = _MCredit.ToString("#0.00");
                            if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForBankReceipt || dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForChequeReturn)
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
                            else if (dr["VoucherType"].ToString() == FixAccounts.VoucherTypeForBankPayment)
                            {
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
                currentdr.Cells["Col_ID"].Value = "Final Total";
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_Narr"].Value = "Closing Balance";
                if (_MClosingDebit > 0)
                {
                    currentdr.Cells["Col_Narr"].Value = "Debit Closing Balance ";
                    currentdr.Cells["Col_Debit"].Value = _MClosingDebit.ToString("#0.00");
                }
                else
                {
                    currentdr.Cells["Col_Narr"].Value = "Credit Closing Balance ";
                    currentdr.Cells["Col_Credit"].Value = _MClosingCredit.ToString("#0.00");
                }
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
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lblFooterMessage.Text = "";
                    InitializeReportGrid();                   
                    _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                    _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                    _MAccountName = "";
                    DataRow dr;
                    dr = null;
                    Account acc = new Account();
                    if (mcbBank.SelectedID != null && mcbBank.SelectedID != "")

                        dr = acc.GetSSNameForGivenAccount(mcbBank.SelectedID);
                    if (dr != null)
                    {
                        if (dr["AccName"] != DBNull.Value)
                            _MAccountName = dr["AccName"].ToString();
                        if (dr["AccOpeningDebit"] != DBNull.Value)
                            _MOpeningDebit = Convert.ToDouble(dr["AccOpeningDebit"].ToString());
                        if (dr["AccOpeningCredit"] != DBNull.Value)
                            _MOpeningCredit = Convert.ToDouble(dr["AccOpeningCredit"].ToString());
                        FillReportGrid();
                        ShowpnlGO();
                        PrintReportHead = "Bank Book From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                        if (txtViewtext.Text.Trim() != string.Empty)
                            PrintReportHead2 = "[" + txtViewtext.Text.ToString() + " - "+ txtViewText2.Text.Trim() + "]";
                       
                    }
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

        private void CheckFilter()
        {
            try
            {

                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                if (mcbvoutype.SelectedID == null)
                {
                    _BindingSource.DefaultView.RowFilter = "AccountID = '" + mcbBank.SelectedID +"'" ;
                    txtViewText2.Text = "ALL";
                }
                else
                {
                    _BindingSource.DefaultView.RowFilter = "AccountID = '" + mcbBank.SelectedID + "' and  vouchertype = '" + mcbvoutype.SeletedItem.ItemData[1].ToString() +"'" ;
                    txtViewText2.Text = mcbvoutype.SeletedItem.ItemData[1].ToString();                    
                }


            }
            catch (Exception ex) { Log.WriteException(ex); }

        }

        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }





        #endregion

        # region Events
        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
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
                    if (voutype == FixAccounts.VoucherTypeForStatementPurchase)
                        ViewControl = new UclStatementSale();
                    if (voutype == FixAccounts.VoucherTypeForStockIN)
                        ViewControl = new UclStockIn();
                    if (voutype == FixAccounts.VoucherTypeForStockOut)
                        ViewControl = new UclStockOut();
                    else if (vousubtype == FixAccounts.SubTypeForRegularSale)
                        ViewControl = new UclDistributorSale("R");
                   


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
                mcbBank.Focus();
        }
        private void mcbBank_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbBank.SelectedID != null && mcbBank.SelectedID != string.Empty)
                btnOKMultiSelectionClick();
        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "F10 = Show Report");
                ttToolTip.SetToolTip(pnlMultiSelection1, "F12 = Reopen This Form , F11 = Date ");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion 
     
    }
}
