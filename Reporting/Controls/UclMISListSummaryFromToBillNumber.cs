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
    public partial class UclMISListSummaryFromToBillNumber : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _SaleData;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private int _MFromNumber;
        private int _MToNumber;
        private string _MFromTime;
        private string _MToTime;

        #endregion

        # region Constructor
        public UclMISListSummaryFromToBillNumber()
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
                _SaleData = new DataTable();
                _BindingSource = new DataTable();
                _SaleList = new SaleList();
                _MFromDate = DateTime.Now.Date.ToString("yyyyMMdd");
                _MToDate = DateTime.Now.Date.ToString("yyyyMMdd");
                headerLabel1.Text = "MIS - SUMMARY";
                ClearControls();
                txtFromNumber.Focus();
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
                dgvChangedBills.Visible = false;
                btndgvChangedBills.Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void InitializeDates()
        {
            //fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
            //toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);

        }
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            ConstructChangedBillsColumns();
            //  dgvReportList.Columns["Col_ID"].Visible = false;
            FormatReportGrid();
            // dgvReportList.InitializeColumnContextMenu();
        }
        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            txtFromNumber.Focus();
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
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvChangedBills.DoubleColumnNames.Add("Col_Amount");
            dgvChangedBills.DateColumnNames.Add("Col_ModifiedDate");
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
                column.HeaderText = "ID";
                column.Width = 90;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.HeaderText = "Type";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Number";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.HeaderText = "Sub";
                column.Width = 40;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "Date";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.HeaderText = "Name";
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructChangedBillsColumns()
        {
            try
            {
                dgvChangedBills.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.Visible = false;
                column.HeaderText = "ID";
                column.Width = 90;
                dgvChangedBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.HeaderText = "Type";
                column.Visible = false;
                column.Width = 70;
                dgvChangedBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Number";
                column.Visible = false;
                column.Width = 60;
                dgvChangedBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.HeaderText = "Sub";
                column.Visible = false;
                column.Width = 40;
                dgvChangedBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "Date";
                column.Visible = false;
                column.Width = 90;
                dgvChangedBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.HeaderText = "Name";
                column.Width = 250;
                column.Visible = false;
                dgvChangedBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ModifiedDate";
                column.HeaderText = "Date";               
                column.Width = 80;
                dgvChangedBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ModifiedTime";
                column.HeaderText = "Time";               
                column.Width = 80;
                dgvChangedBills.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 90;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvChangedBills.Columns.Add(column);
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
            string mtime = string.Empty;
            string mdate = string.Empty;
            string mmodifiedDate = string.Empty;
            int rowindex = 0;
            try
            {

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    
                    double amount = 0;
                    mmodifiedDate = string.Empty;
                    amount = Convert.ToDouble(dr["AmountNet"].ToString());
                    mtime = dr["CreatedTime"].ToString();
                    mtime = General.GetTimeinHHMMDDFormat(mtime);
                    mdate = dr["CreatedDate"].ToString();
                    if (dr["ModifiedDate"] != DBNull.Value)
                    {
                        mmodifiedDate = dr["ModifiedDate"].ToString();
                    }
                    if ((mdate == _MFromDate &&  Convert.ToInt32(mtime) >= Convert.ToInt32(_MFromTime)) || (mdate == _MToDate && Convert.ToInt32(mtime) <=  Convert.ToInt32(_MToTime)) || (mdate != _MFromDate && mdate != _MToDate))
                    {
                        rowindex = dgvReportList.Rows.Add();
                        DataGridViewRow gdr = dgvReportList.Rows[rowindex];
                        if (mmodifiedDate != string.Empty)
                        {
                            gdr.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                        }
                        gdr.Cells["Col_ID"].Value = dr["ID"].ToString();
                        gdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        gdr.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        gdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                        gdr.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                        gdr.Cells["Col_Name"].Value = dr["AccName"].ToString();
                        gdr.Cells["Col_Amount"].Value = amount.ToString("#0,00");
                    }  
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
                //_MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                //_MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                bool retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    DataTable dtable = new DataTable();
                    dtable = _SaleList.GetDataForSummaryFromToNumber(_MFromDate, _MToDate, _MFromTime,_MToTime);
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
       

      

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            if (txtFromNumber.Text != string.Empty)
                _MFromNumber = Convert.ToInt32(txtFromNumber.Text.ToString());
            if (txtToNumber.Text != string.Empty)
                _MToNumber = Convert.ToInt32(txtToNumber.Text.ToString());

            _SaleData = _SaleList.GetSaleDataFromToNumber(_MFromNumber, _MToNumber);

            if (_SaleData != null && _SaleData.Rows.Count > 0)
            {
                _MFromDate = _SaleData.Rows[0]["CreatedDate"].ToString();
                _MFromTime = General.GetTimeinHHMMDDFormat(_SaleData.Rows[0]["CreatedTime"].ToString());
                foreach (DataRow dr in _SaleData.Rows)
                {
                    _MToDate = dr["CreatedDate"].ToString();
                    _MToTime = General.GetTimeinHHMMDDFormat(dr["CreatedTime"].ToString());
                }
            }



            retValue = true;
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

        private void txtFromNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtToNumber.Focus();
        }

        private void txtToNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }

        private void dgvReportList_TabKeyPressed(object sender, EventArgs e)
        {
            if (dgvChangedBills.Rows.Count >= 0)
            {
                dgvChangedBills.Rows.Clear();
            }
            dgvChangedBills.Visible = true;
            btndgvChangedBills.Visible = true;

            //string voutype = "";
            //string vousubtype = "";
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                   // this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();

                    DataTable dt = _SaleList.GetChangedBillForSelectedID(selectedID);

                    string mtime = string.Empty;
                    string mdate = string.Empty;
                    string mmodifiedDate = string.Empty;
                    int rowindex = 0;
                    try
                    {

                        foreach (DataRow dr in dt.Rows)
                        {

                            double amount = 0;
                            mmodifiedDate = string.Empty;
                            amount = Convert.ToDouble(dr["AmountNet"].ToString());
                            mtime = dr["ModifiedTime"].ToString();
                         //   mtime = General.GetTimeinHHMMDDFormat(mtime);                           
                            if (dr["ModifiedDate"] != DBNull.Value)
                            {
                                mmodifiedDate = dr["ModifiedDate"].ToString();
                            }
                           
                                rowindex = dgvChangedBills.Rows.Add();
                                DataGridViewRow gdrc = dgvChangedBills.Rows[rowindex];
                                if (mmodifiedDate != string.Empty)
                                {
                                    gdrc.DefaultCellStyle.BackColor = Color.LightBlue;
                                }
                                gdrc.Cells["Col_ID"].Value = dr["ChangedID"].ToString();
                                gdrc.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                                gdrc.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                                gdrc.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                                gdrc.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                                gdrc.Cells["Col_ModifiedDate"].Value = dr["ModifiedDate"].ToString();
                                gdrc.Cells["Col_ModifiedTime"].Value = dr["ModifiedTime"].ToString();
                              //  gdr.Cells["Col_Name"].Value = dr["AccName"].ToString();
                                gdrc.Cells["Col_Amount"].Value = amount.ToString("#0,00");
                           
                        }
                        dgvChangedBills.Focus();
                    }
                       
                    catch (Exception ex)
                    {
                        Log.WriteException(ex);
                    }

                    //voutype = dgvReportList.SelectedRow.Cells[1].Value.ToString();
                    //vousubtype = dgvReportList.SelectedRow.Cells["Col_VoucherSubType"].Value.ToString();
                    //if (voutype == FixAccounts.VoucherTypeForCashPurchase || voutype == FixAccounts.VoucherTypeForCreditPurchase || voutype == FixAccounts.VoucherTypeForCreditStatementPurchase)
                    //    ViewControl = new UclPurchase();
                    //else if (vousubtype == FixAccounts.SubTypeForPatientSale)
                    //    ViewControl = new UclPatientSale();
                    //else if (vousubtype == FixAccounts.SubTypeForHospitalSale)
                    //    ViewControl = new UclHospitalSale();
                    //else if (vousubtype == FixAccounts.SubTypeForInstitutionalSale)
                    //    ViewControl = new UclInstitutionalSale();
                    //else if (vousubtype == FixAccounts.SubTypeForDebtorSale)
                    //    ViewControl = new UclDebtorSale();
                    //ShowViewForm(selectedID, ViewMode.Changed);
                    //this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
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
                    voutype = dgvReportList.SelectedRow.Cells[1].Value.ToString();
                    vousubtype = dgvReportList.SelectedRow.Cells["Col_VoucherSubType"].Value.ToString();
                    if (voutype == FixAccounts.VoucherTypeForCashPurchase || voutype == FixAccounts.VoucherTypeForCreditPurchase || voutype == FixAccounts.VoucherTypeForCreditStatementPurchase)
                        ViewControl = new UclPurchase();                   
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

        private void dgvChangedBills_DoubleClicked(object sender, EventArgs e)
        {
            string voutype = "";
            string vousubtype = "";
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvChangedBills.SelectedRow.Cells[0].Value.ToString();
                    voutype = dgvChangedBills.SelectedRow.Cells["Col_VoucherType"].Value.ToString();
                    vousubtype = dgvChangedBills.SelectedRow.Cells["Col_VoucherSubType"].Value.ToString();
                  //  if (voutype == FixAccounts.VoucherTypeForCashPurchase || voutype == FixAccounts.VoucherTypeForCreditPurchase || voutype == FixAccounts.VoucherTypeForCreditStatementPurchase)
                   
                    if (vousubtype == FixAccounts.SubTypeForRegularSale)
                        ViewControl = new UclDistributorSale("R");
                    ShowViewForm(selectedID, ViewMode.Changed);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }

        }      

        private void btndgvChangedBills_Click(object sender, EventArgs e)
        {
            dgvChangedBills.Visible = false;
            btndgvChangedBills.Visible = false;
        }
    }
}
