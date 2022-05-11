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
    public partial class UclDistributorPartywiseOutStanding : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingOpeningBalance;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        private double _MTotalOutStanding;
        private string _MSaleSubType = FixAccounts.SubTypeForRegularSale;
        #endregion

        # region Constructor
        public UclDistributorPartywiseOutStanding()
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
                _BindingOpeningBalance = new DataTable();
                _SaleList = new SaleList();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "Distributor PARTYWISE Outstanding BILLS";
                ClearControls();
                FillPartyCombo();
                AddToolTip();
                fromDate1.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void ShowReport(string ID, string blank, string FromDate, string ToDate)
        {
            base.ShowReport(ID,blank, FromDate, ToDate);
            try
            {
                if (ID != null && ID != "")
                {
                    _MFromDate = FromDate;
                    _MToDate = ToDate;
                    mcbCreditor.SelectedID = ID;
                    ShowReportGrid();

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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

                    if (dr.Cells["Col_ID"].Value != null && dr.Cells["Col_Amount"].Value != null)
                    {
                        if (PrintRowCount >= FixAccounts.NumberOfRowsPerReport)
                        {
                            PrintRowPixel += 17;
                            row = new PrintRow("Continued....", PrintRowPixel, 15, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintBill.Print_Bill();
                            PrintBill.Rows.Clear();
                            PrintRowPixel = 0;
                            PrintPageNumber += 1;
                            PrintHead();
                        }
                        if (dr.Cells["Col_ID"].Value.ToString() == "Total")
                        {
                            PrintRowPixel += 17;
                              row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                              PrintBill.Rows.Add(row);               
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        if (dr.Cells["Col_VoucherType"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 45, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherDate"].Value != null)
                        {
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 100, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AccName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 160, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        //   row = new PrintRow(dr.Cells["Col_Address"].Value.ToString().PadRight(30), PrintRowPixel, 360, PrintFont);
                        //   PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_Amount"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(480.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AmountBalance"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_AmountBalance"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(600.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ID"].Value.ToString() == "Total")
                        {
                            PrintRowPixel += 17;
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                    }
                }
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;
                mlen = (_MTotalAmount.ToString("#0.00").Length);
                colpix = Convert.ToInt32(510.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                mlen = (_MTotalOutStanding.ToString("#0.00").Length);
                colpix = Convert.ToInt32(630.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Number", PrintRowPixel, 40, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 100, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 160, PrintFont);
                PrintBill.Rows.Add(row);
                //row = new PrintRow("Address", PrintRowPixel, 360, PrintFont);
                //PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 510, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Outstanding Rs.", PrintRowPixel, 630, PrintFont);
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
                InitializeDates();
                lblFooterMessage.Text = "";
                mcbCreditor.SelectedID = "";
                txtReportTotal.Text = "";
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

        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
            dgvReportList.DoubleColumnNames.Add("Col_AmountBalance");
            dgvReportList.DoubleColumnNames.Add("Col_OnAccount");
        }

        public void HidepnlGO()
        {

            tsbtnPrint.Enabled = false;
            ViewFromDate.Visible = false;
            pnlMultiSelection1.Visible = true;
            txtReportTotal.Text = "";

        }

        public void ShowpnlGO()
        {
            try
            {
                if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
                {
                    tsbtnPrint.Enabled = true;
                }
                ViewFromDate.Visible = true;
                ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
                ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                    txtViewText.Text = mcbCreditor.SeletedItem.ItemData[2] + " " + mcbCreditor.SeletedItem.ItemData[3];
                pnlMultiSelection1.Visible = false;
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "Sub";
                column.Width = 40;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "DATE";
                column.Width = 80;
                column.Visible = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 200;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "AccAddress1";
                column.HeaderText = "Address";
                column.Width = 150;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountBalance";
                column.DataPropertyName = "AmountBalance";
                column.HeaderText = "OutStanding Amount";
                column.Width = 110;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OnAccount";
                column.DataPropertyName = "OnAccount";
                column.HeaderText = "On Account";
                column.Width = 110;
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
                FormatReportGrid();
              
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                    dgvReportList.DataSource = _BindingSource;
                    dgvReportList.Bind();
                }
                else
                    BindReportList();
                dgvReportList.Columns["Col_ID"].Visible = false;
                _MTotalAmount = 0;
                _MTotalOutStanding = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != string.Empty)
                        _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                    if (dr.Cells["Col_AmountBalance"].Value != null && dr.Cells["Col_AmountBalance"].Value.ToString() != string.Empty)
                    _MTotalOutStanding += Convert.ToDouble(dr.Cells["Col_AmountBalance"].Value.ToString());
                }
                txtReportTotal.Text = _MTotalAmount.ToString("#0.00");
                txtOutStanding.Text = _MTotalOutStanding.ToString("#0.00");
                NoofRows();
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void BindReportList()
        {
            string preaccountID = "";
            string curraccountID = "";
            double totalbillamount = 0;
            double totaloutstanding = 0;
            double totalnoofbills = 0;
            double totalonaccount = 0;
            double opdebit = 0;
            double opcredit = 0;
            double clearedamt = 0;
            double onaccount = 0;
            int _RowIndex = 0;
            double balanceamt = 0;
            bool iffirst = true;
            DataGridViewRow currentdr;
            dgvReportList.Rows.Clear();
            try
            {
                if (_BindingSource.Rows.Count > 0)
                {

                    foreach (DataRow dr in _BindingSource.Rows)
                    {
                        balanceamt = 0;                       
                        curraccountID = dr["AccountID"].ToString();
                        if (preaccountID != curraccountID)
                        {
                            if (!iffirst)
                            {
                                _RowIndex = dgvReportList.Rows.Add();
                                currentdr = dgvReportList.Rows[_RowIndex];
                                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                                currentdr.Cells["Col_ID"].Value = "Total";
                                currentdr.Cells["Col_VoucherType"].Value = "Total:";
                                currentdr.Cells["Col_AccName"].Value = "Bills : "+ totalnoofbills.ToString();
                                currentdr.Cells["Col_Amount"].Value = totalbillamount.ToString("#0.00");
                                currentdr.Cells["Col_AmountBalance"].Value = totaloutstanding.ToString("#0.00");
                                currentdr.Cells["Col_OnAccount"].Value = totalonaccount.ToString("#0.00");
                                _RowIndex = dgvReportList.Rows.Add();
                            }
                            iffirst = false;
                            totalbillamount = 0;
                            totaloutstanding = 0;
                            totalnoofbills = 0;
                            totalonaccount = 0;
                            opdebit = 0;
                            opcredit = 0;
                            clearedamt = 0;
                            onaccount = 0;
                            if (dr["accopeningdebit"] != DBNull.Value)
                                opdebit = Convert.ToDouble(dr["AccOpeningDebit"].ToString());
                            if (dr["accopeningcredit"] != DBNull.Value)
                                opcredit = Convert.ToDouble(dr["AccOpeningCredit"].ToString());
                            if (dr["accclearedamount"] != DBNull.Value)
                                clearedamt = Convert.ToDouble(dr["AccClearedAmount"].ToString());
                            if (dr["onaccountAmount"] != DBNull.Value)
                                onaccount = Convert.ToDouble(dr["onaccountAmount"].ToString());
                            double balamount = opdebit - opcredit - clearedamt;
                            _RowIndex = dgvReportList.Rows.Add();
                            currentdr = dgvReportList.Rows[_RowIndex];
                            currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                            currentdr.Cells["Col_ID"].Value = dr["AccountID"].ToString();
                            currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                            currentdr.Cells["Col_Address"].Value = "Opening Balance";
                            currentdr.Cells["Col_Amount"].Value =  opdebit.ToString("#0.00");
                            currentdr.Cells["Col_AmountBalance"].Value = balamount.ToString("#0.00");
                            currentdr.Cells["Col_OnAccount"].Value = onaccount.ToString("#0.00");
                            totalbillamount = opdebit;
                            totaloutstanding = balamount;
                            totalonaccount = onaccount;
                        }
                        preaccountID = curraccountID;
                        if (dr["AmountBalance"] != DBNull.Value && dr["AmountBalance"].ToString() != string.Empty)
                            balanceamt = Convert.ToDouble(dr["AmountBalance"].ToString());
                        if (dr["onaccountAmount"] != DBNull.Value && dr["onaccountAmount"].ToString() != string.Empty)
                            balanceamt += Convert.ToDouble(dr["onaccountAmount"].ToString());
                        if (balanceamt > 0)
                        {
                            totalnoofbills += 1;
                            _RowIndex = dgvReportList.Rows.Add();
                            currentdr = dgvReportList.Rows[_RowIndex];
                            currentdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                            currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                            currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                            currentdr.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                            currentdr.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                            currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                            currentdr.Cells["Col_Address"].Value = dr["AccAddress1"].ToString();
                            currentdr.Cells["Col_Amount"].Value = dr["Amount"].ToString();
                            currentdr.Cells["Col_AmountBalance"].Value = dr["AmountBalance"].ToString();
                            string mvdt = dr["VoucherDate"].ToString();
                            if (Convert.ToInt32(mvdt) >= Convert.ToInt32(General.ShopDetail.Shopsy))
                            {
                                totalbillamount += Convert.ToDouble(dr["Amount"].ToString());
                                totaloutstanding += Convert.ToDouble(dr["AmountBalance"].ToString());
                            }
                                
                            else
                                currentdr.DefaultCellStyle.BackColor = Color.LightYellow;
                            if (Convert.ToDouble(dr["onaccountAmount"].ToString()) != 0)
                                currentdr.Cells["Col_OnAccount"].Value = dr["onaccountAmount"].ToString();
                        }
                        
                       
                        
                        //  totalbillamount += Convert.ToDouble(dr["Amount"].ToString());
                        // totaloutstanding += Convert.ToDouble(dr["AmountBalance"].ToString());
                        totalonaccount += Convert.ToDouble(dr["onaccountAmount"].ToString());
                       

                    }
                    _RowIndex = dgvReportList.Rows.Add();
                    currentdr = dgvReportList.Rows[_RowIndex];
                    currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                    currentdr.Cells["Col_ID"].Value = "Total";
                    currentdr.Cells["Col_VoucherType"].Value = "Total:";
                    currentdr.Cells["Col_AccName"].Value = "Bills : " + totalnoofbills.ToString();
                    currentdr.Cells["Col_Amount"].Value = totalbillamount.ToString("#0.00");
                    currentdr.Cells["Col_AmountBalance"].Value = totaloutstanding.ToString("#0.00");
                    currentdr.Cells["Col_OnAccount"].Value = totalonaccount.ToString("#0.00");
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }



        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }
        private void FillPartyCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccDiscountOffered" };
                mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "150", "50" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetDebtorPatientList();
                mcbCreditor.FillData(dtable);
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
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                {
                        dtable = _SaleList.GetOverviewDataForPartywiseOutstandingSaleReport(mcbCreditor.SelectedID, _MFromDate, _MToDate, _MSaleSubType);
                }
                else
                    dtable = _SaleList.GetOverviewDataForPartywiseOutstandingSaleReport(_MFromDate, _MToDate, _MSaleSubType);
                _BindingSource = dtable;

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion

        # region Events

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
                    voutype = dgvReportList.SelectedRow.Cells[1].Value.ToString();
                    vousubtype = dgvReportList.SelectedRow.Cells["Col_VoucherSubType"].Value.ToString();
                    //if (vousubtype == FixAccounts.SubTypeForPatientSale)
                    //    ViewControl = new UclPatientSale();
                    //else if (vousubtype == FixAccounts.SubTypeForHospitalSale)
                    //    ViewControl = new UclHospitalSale();
                    //else if (vousubtype == FixAccounts.SubTypeForInstitutionalSale)
                    //    ViewControl = new UclInstitutionalSale();
                    if (vousubtype == FixAccounts.SubTypeForRegularSale)
                        ViewControl = new UclDistributorSale("R");
                 //   if (vousubtype == FixAccounts.SubTypeForDebtorSale || vousubtype == FixAccounts.SubTypeForInstitutionalSale || vousubtype == FixAccounts.SubTypeForHospitalSale || vousubtype == FixAccounts.SubTypeForPatientSale)
                        ShowViewForm(selectedID, ViewMode.Current);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        private void ShowReportGrid()
        {
            try
            {
                InitializeReportGrid();
                ShowpnlGO();
                FillReportGrid();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_MToDate, _MToDate);
                //if (mcbCreditor.SelectedID == null || mcbCreditor.SelectedID == string.Empty)
                //    lblFooterMessage.Text = "Select Party";
                //else
                //{
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ShowpnlGO();
                    ShowReportGrid();
                    PrintReportHead = "Partywise Outstanding From : " + General.GetDateInDateFormat(_MFromDate) + " To : " + General.GetDateInDateFormat(_MToDate);
                    if (txtViewText.Text != "")
                        PrintReportHead2 = "[" + txtViewText.Text.ToString() + "]";
                    else
                        PrintReportHead2 = "";
                    this.Cursor = Cursors.Default;
                    dgvReportList.Focus();
                }
                else
                    lblFooterMessage.Text = "Check Date";
                //}
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
                mcbCreditor.Focus();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
            else if (e.KeyCode == Keys.Left)
                fromDate1.Focus();
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID != "")
                btnOKMultiSelectionClick();
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                //ttToolTip.SetToolTip(dateVoucherTypeControlOneDateSale1, "Home  To Reopen This Form   End To See Report");
                //ttToolTip.SetToolTip(dgvReportList, "Home key To Select Options");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }



        #endregion
    }
}
