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
    public partial class UclMISListDailyBankClosing : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Account _Account;
        private MPReports _MPReports;
        private string _MFromDate;
        private string _MToDate;
        private double _MOpeningDebit = 0;
        private double _MOpeningCredit = 0;
        private double _MTrDebit = 0;
        private double _MTrCredit = 0; 
        private double _MDebit = 0;
        private double _MCredit = 0;
        string DefaultBankID = "";
        #endregion

        # region Constructor
        public UclMISListDailyBankClosing()
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
                headerLabel1.Text = "MIS - DAILY BANK CLOSING";
                ClearControls();
                FillBankCombo();
                GetDefaultBank();
                HidepnlGO();
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }   
        #endregion

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

                    if (dr.Cells["Col_Date"].Value != null)
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


                        if (dr.Cells["Col_Date"].Value != null)
                        {
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_Date"].Value.ToString()), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Day"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Day"].Value.ToString().PadRight(30), PrintRowPixel, 60, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_Debit"].Value != null && Convert.ToDouble(dr.Cells["Col_Debit"].Value.ToString()) != 0)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Debit"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(270.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Credit"].Value != null && Convert.ToDouble(dr.Cells["Col_Credit"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Credit"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(420.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_Balance"].Value != null && Convert.ToDouble(dr.Cells["Col_Balance"].Value.ToString()) != 0)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Balance"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(570.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
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


                row = new PrintRow("Date", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Day", PrintRowPixel, 65, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Debit", PrintRowPixel, 300, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Credit", PrintRowPixel, 440, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Balance Amount", PrintRowPixel, 570, PrintFont);
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

        #endregion

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
            FormatReportGrid();
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            //ViewFromDate.Visible = false;
            //ViewToDate.Visible = false;
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
            txtViewtext.Text = mcbBank.SeletedItem.ItemData[2].ToString();
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
                column.Name = "Col_Date";
                column.HeaderText = "DATE";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Day";
                column.HeaderText = "Day";
                column.Width = 126;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Debit";
                column.HeaderText = "Debit";
                column.Width = 150;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Credit";
                column.HeaderText = "Credit";
                column.Width = 150;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Balance";
                column.HeaderText = "Balance";
                column.Width = 150;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
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
                BindReportGrid(FixAccounts.AccountCash.ToString(), "DAILY CASH CLOSING", "", _MOpeningDebit, _MOpeningCredit);
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
                dtable = _MPReports.GetDayTotalForDailyClosing(_MFromDate, _MToDate, mcbBank.SelectedID);
                _BindingSource = dtable;

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
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
            dgvReportList.DateColumnNames.Add("Col_Date");
            dgvReportList.DoubleColumnNames.Add("Col_Debit");
            dgvReportList.DoubleColumnNames.Add("Col_Credit");
            dgvReportList.DoubleColumnNames.Add("Col_Balance");
        }

        private void BindReportGrid(string AccID, string party, string narr, double opDebit, double opCredit)
        {
            double mbalance = 0;
            string cd = "";
            string td = "";
            DateTime currentDate = fromDate1.Value;
            try
            {
                int _RowIndex;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr = dgvReportList.Rows[_RowIndex];
                _MOpeningDebit = opDebit;
                _MOpeningCredit = opCredit;
                GetActualOpeningBalance(AccID, _MFromDate);
                mbalance = _MOpeningDebit - _MOpeningCredit;
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
                currentdr.Cells["Col_Date"].Value = _MFromDate;
                currentdr.Cells["Col_Day"].Value = fromDate1.Value.DayOfWeek;
                currentdr.Cells["Col_Balance"].Value = mbalance.ToString("#0.00");



                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (Convert.ToInt32(dr["TransactionDate"].ToString()) >= Convert.ToInt32(_MFromDate))
                    {
                       
                        cd = currentDate.Date.ToString("yyyyMMdd");                       
                        td = dr["TransactionDate"].ToString();
                        if (Convert.ToInt32(td) != Convert.ToInt32(cd))
                        {
                            do
                            {
                                _RowIndex = dgvReportList.Rows.Add();
                                currentdr = dgvReportList.Rows[_RowIndex];
                                currentdr.Cells["Col_Date"].Value = cd;
                                currentdr.Cells["Col_Balance"].Value = mbalance.ToString("#0.00");
                                currentdr.Cells["Col_Day"].Value = currentDate.DayOfWeek;
                                currentDate = currentDate.AddDays(1);
                                cd = currentDate.Date.ToString("yyyyMMdd");
                            } while (Convert.ToInt32(td) != Convert.ToInt32(cd));

                            _RowIndex = dgvReportList.Rows.Add();
                            currentdr = dgvReportList.Rows[_RowIndex];
                            _MDebit = Convert.ToDouble(dr["Debit"].ToString());
                            _MCredit = Convert.ToDouble(dr["Credit"].ToString());
                            mbalance = mbalance + (_MDebit - _MCredit);
                            _MTrDebit += _MDebit;
                            _MTrCredit += _MCredit;
                            currentdr.Cells["Col_Date"].Value = cd;
                            currentdr.Cells["Col_Day"].Value = currentDate.DayOfWeek;
                            currentdr.Cells["Col_Debit"].Value = _MDebit.ToString("#0.00");
                            currentdr.Cells["Col_Credit"].Value =_MCredit.ToString("#0.00");
                            currentdr.Cells["Col_Balance"].Value = mbalance.ToString("#0.00");
                            currentDate = currentDate.AddDays(1);
                        }
                        else
                        {
                            _RowIndex = dgvReportList.Rows.Add();
                            currentdr = dgvReportList.Rows[_RowIndex];
                            _MDebit = Convert.ToDouble(dr["Debit"].ToString());
                            _MCredit = Convert.ToDouble(dr["Credit"].ToString());
                            mbalance = mbalance + (_MDebit - _MCredit);
                            _MTrDebit += _MDebit;
                            _MTrCredit += _MCredit;
                            currentdr.Cells["Col_Date"].Value = cd;
                            currentdr.Cells["Col_Day"].Value = currentDate.DayOfWeek;
                            currentdr.Cells["Col_Debit"].Value = _MDebit.ToString("#0.00");
                            currentdr.Cells["Col_Credit"].Value = _MCredit.ToString("#0.00");
                            currentdr.Cells["Col_Balance"].Value = mbalance.ToString("#0.00");
                            currentDate = currentDate.AddDays(1);
                        }
                    }

                }
                        currentDate = currentDate.AddDays(1);
                        cd = currentDate.Date.ToString("yyyyMMdd");
                     
                        if (Convert.ToInt32(cd) <=  Convert.ToInt32(_MToDate))
                        {
                            do
                            {
                                _RowIndex = dgvReportList.Rows.Add();
                                currentdr = dgvReportList.Rows[_RowIndex];
                                currentdr.Cells["Col_Date"].Value = cd;
                                currentdr.Cells["Col_Day"].Value = currentDate.DayOfWeek;
                                currentdr.Cells["Col_Balance"].Value = mbalance.ToString("#0.00");
                                currentDate = currentDate.AddDays(1);
                                cd = currentDate.Date.ToString("yyyyMMdd");
                            } while (Convert.ToInt32(cd) <= Convert.ToInt32(_MToDate));
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
                    ShowpnlGO();
                    DataRow dr;
                    Account acc = new Account();
                    dr = acc.GetSSNameForGivenAccount(FixAccounts.AccountCash.ToString());
                    if (dr != null)
                    {
                        if (dr["AccOpeningDebit"] != DBNull.Value)
                            _MOpeningDebit = Convert.ToDouble(dr["AccOpeningDebit"].ToString());
                        if (dr["AccOpeningCredit"] != DBNull.Value)
                            _MOpeningCredit = Convert.ToDouble(dr["AccOpeningCredit"].ToString());

                    }
                    FillReportGrid();
                    PrintReportHead = "Daily Bank Closing From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = txtViewtext.Text;
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
                if (Convert.ToInt32(dr["TransactionDate"].ToString()) < Convert.ToInt32(fromDate))
                {
                    if (dr["Debit"] != null && dr["Debit"].ToString() != "")
                        totdebit += Convert.ToDouble(dr["Debit"].ToString());
                    if (dr["Credit"] != null && dr["Credit"].ToString() != "")
                        totcredit += Convert.ToDouble(dr["Credit"].ToString());
                }
                else
                    break;

            }
            _MOpeningDebit += totdebit;
            _MOpeningCredit += totcredit;
        }
        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }
        #endregion

        # region Events
       
        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbBank.Focus();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
        }

        #endregion

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

        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void mcbBank_EnterKeyPressed(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
    }
}
