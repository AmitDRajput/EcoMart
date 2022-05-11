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
    public partial class UclVouChequeReceivedButNotCleared : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private BankReceipt _BankReceipt;
        private string _VouType;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        private double _MAmount;
        private string _MPartyName;
        private string DefaultBankID = "";
        # endregion

        # region Contructor
        public UclVouChequeReceivedButNotCleared()
        {
            try
            {
            InitializeComponent();
            ViewControl = new UclBankReceipt();
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
                _BankReceipt = new BankReceipt();
                headerLabel1.Text = "VOUCHER LIST-CHEQUE RECEIVED BUT NOT CLEARED LIST";
                ClearControls();
                FillPartyCombo();
                FillBankCombo();
                InitializeReportGrid();
                GetDefaultBank();
                //AddToolTip();
                HidepnlGO();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public override void SetFocus()
        {
            mcbBank.Focus();
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
                PrintReportHead = "Cheques Received But Not Cleared From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;
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
                        row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString(), PrintRowPixel, 50, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 130, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString(), PrintRowPixel, 230, PrintFont);
                        PrintBill.Rows.Add(row);
                        row = new PrintRow(dr.Cells["Col_Address"].Value.ToString(), PrintRowPixel, 480, PrintFont);
                        PrintBill.Rows.Add(row);
                        mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, 700, PrintFont);
                        PrintBill.Rows.Add(row);

                    }
                }
                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;
                row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, 700, PrintFont);
                PrintBill.Rows.Add(row);
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

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Number", PrintRowPixel, 40, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 130, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 230, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Address", PrintRowPixel, 480, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 700, PrintFont);
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
                _VouType = FixAccounts.VoucherTypeForBankReceipt;
                txtAmount.Text = "0.00";
                mcbAccount.SelectedID = "";
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
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Visible = false;
            ViewToDate.Visible = false;
            txtAmount.Text = "0.00";
            mcbAccount.SelectedID = string.Empty;
            txtViewAmount.Text = "0.00";
            txtViewText.Text = string.Empty;
            txtViewText2.Text = string.Empty;
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
            ViewFromDate.Visible = true;
            ViewToDate.Visible = true;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            txtViewAmount.Text = _MAmount.ToString("#0.00");
            txtViewText.Text = mcbBank.SeletedItem.ItemData[1];
            txtViewText2.Text = _MPartyName;

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
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "Date";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.HeaderText = "AccountID";
                column.Visible = false;
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.HeaderText = "Party";
                column.Width = 300;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.HeaderText = "Address";
                column.Width = 100;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChequeNumber";
                column.HeaderText = "CHQ.Number";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChequeDate";
                column.HeaderText = "CHQ.Date";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 140;
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
                FormatReportGrid();
                BindReportGrid();
                CalculateFinalTotals();
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
                _BindingSource = new DataTable();
                _BindingSource = _BankReceipt.GetDataForReportChequesReceivedButNotCleared(mcbBank.SelectedID, _VouType, _MFromDate, _MToDate);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillPartyCombo()
        {
            try
            {
                mcbAccount.SelectedID = null;
                mcbAccount.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAreaID" };
                mcbAccount.ColumnWidth = new string[5] { "0", "20", "200", "200", "0" };
                mcbAccount.DisplayColumnNo = 2;
                mcbAccount.ValueColumnNo = 0;
                mcbAccount.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetOverviewDataForList();
                mcbAccount.FillData(dtable);
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
        private void FillBankCombo()
        {
            try
            {
                mcbBank.SelectedID = null;
                mcbBank.SourceDataString = new string[2] { "AccountID", "AccName" };
                mcbBank.ColumnWidth = new string[2] { "0", "200" };
                mcbBank.ValueColumnNo = 0;
                mcbBank.UserControlToShow = new UclAccount();
                Account _Bank = new Account();
                DataTable dtable = _Bank.GetSSAccountHoldersList(FixAccounts.AccCodeForBank);
                mcbBank.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DateColumnNames.Add("Col_ChequeDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
        }
        private void BindReportGrid()
        {
            string accountID = "";
            double amt = 0;
            double mamount = 0;
            string partycode = "";
            bool ifinsertrow = false;
            _MAmount = 0;
            _MPartyName = "";

            if (txtAmount.Text != null && txtAmount.Text.ToString() != "")
            {
                double.TryParse(txtAmount.Text.ToString(), out mamount);
                _MAmount = mamount;
            }
            if (mcbAccount.SelectedID != null && mcbAccount.SelectedID != "")
            {
                partycode = mcbAccount.SelectedID;
                _MPartyName = mcbAccount.SeletedItem.ItemData[2].ToString();
            }
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    accountID = dr["AccountID"].ToString();
                    amt = Convert.ToDouble(dr["AmountNet"].ToString());

                    ifinsertrow = false;

                    if (mamount > 0)
                    {
                        if (partycode == "")
                        {
                            if (amt == mamount)
                                ifinsertrow = true;
                        }
                        else
                        {
                            if (amt == mamount && accountID == partycode)
                                ifinsertrow = true;
                        }

                    }
                    else
                    {
                        if (partycode != "")
                        {
                            if (accountID == partycode)
                                ifinsertrow = true;
                        }
                        else
                        {
                            ifinsertrow = true;
                        }
                    }

                    if (ifinsertrow)
                    {
                        int rowIndex = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                        dgvrow.Cells["Col_ID"].Value = dr["CBID"].ToString();
                        dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        dgvrow.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                        dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        if (dr["ChequeNumber"] != DBNull.Value)
                            dgvrow.Cells["Col_ChequeNumber"].Value = dr["ChequeNumber"].ToString();
                        if (dr["ChequeDate"] != DBNull.Value)
                            dgvrow.Cells["Col_ChequeDate"].Value = dr["ChequeDate"].ToString();
                        dgvrow.Cells["Col_AccName"].Value = dr["ACCName"].ToString();
                        dgvrow.Cells["Col_Amount"].Value = dr["AmountNet"].ToString();
                    }


                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            //try
            //{
            //    foreach (DataRow dr in _BindingSource.Rows)
            //    {
            //        int rowIndex = dgvReportList.Rows.Add();
            //        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
            //        dgvrow.Cells["Col_ID"].Value = dr["ID"].ToString();
            //        dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
            //        dgvrow.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
            //        dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
            //        dgvrow.Cells["Col_AccName"].Value = dr["PatientName"].ToString();
            //        dgvrow.Cells["Col_Amount"].Value = dr["AmountNet"].ToString();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    Log.WriteException(Ex);
            //}
        }

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                if (mcbBank.SelectedID == null || mcbBank.SelectedID == "")
                {
                    lblFooterMessage.Text = "Select Bank";
                    mcbBank.Focus();
                }
                else
                {
                    _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                    _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                    retValue = General.CheckDates(_MFromDate, _MToDate);
                    if (retValue)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        lblFooterMessage.Text = "";
                        InitializeReportGrid();
                        FillReportGrid();
                        ShowpnlGO();
                        this.Cursor = Cursors.Default;
                        dgvReportList.Focus();
                    }
                    else
                        lblFooterMessage.Text = "Check Date";
                }
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
            try
            {
                _MTotalAmount = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null)
                    {
                        _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                    }
                }
                int rowIndex = dgvReportList.Rows.Add();
                DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                dgvrow.Cells["Col_AccName"].Value = "Total";
                dgvrow.Cells["Col_Amount"].Value = _MTotalAmount.ToString("#0.00");
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
        }
        #endregion Other Private Methods

        # region Events

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


        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAmount.Focus();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbAccount.Focus();
            else if (e.KeyCode == Keys.Up)
                toDate1.Focus();
        }

        private void mcbAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
            else if (e.KeyCode == Keys.Up)
                txtAmount.Focus();
        }
        private void mcbBank_EnterKeyPressed(object sender, EventArgs e)
        {
            fromDate1.Focus();
        }
        # endregion Events

        private void mcbAccount_EnterKeyPressed(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

    }
}
