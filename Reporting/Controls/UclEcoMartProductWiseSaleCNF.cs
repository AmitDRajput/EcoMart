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
    public partial class UclEcoMartProductWiseSaleCNF : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceMultiSelection;
        private SaleList _SaleList;
        private Stock _Stock;
        private string _MFromDate;
        private string _MToDate;
        private string _MProductName;
        private string _MMrp;
        public static string FromDt = ""; // [06.02.2017]
        public static string ToDt = "";   // [06.02.2017]
        private List<DataGridViewRow> rowCollection;

        #endregion

        public UclEcoMartProductWiseSaleCNF()
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

        #region IOverview Members
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _SaleList = new SaleList();
                _Stock = new Stock();
                headerLabel1.Text = "SALE PRODUCT/BATCHWISE";
                ClearControls();
                FillProductCombo();
                FillMultiSelectionGrid();
                AddToolTip();
                HidepnlGO();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void SetFocus()
        {
            base.SetFocus();
            //fromDate1.Focus();
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
                btnOKMultiSelection1Click();
                retValue = true;
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }


        #endregion

        #region IReport Members      
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

        private void PrintData() // Modified On [06.02.2017] FromDt, ToDt Added
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
                // totalrows += 7; // for first page heading
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead(FromDt, ToDt);
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
                            PrintHead(FromDt, ToDt);
                        }
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        //  row = new PrintRow("", 0, 0, PrintFont);
                        double mamt = 0;

                        if (dr.Cells["Col_VoucherType"].Value == null && dr.Cells["Col_Batch"].Value != null)
                        {


                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 13;
                            PrintRowCount += 1;
                            if (dr.Cells["Col_Name"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Name"].Value.ToString().PadRight(30), PrintRowPixel, 180, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Batch"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 430, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Quantity"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 530, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Amount"].Value != null)
                            {

                                mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            PrintRowPixel += 13;
                            PrintRowCount += 1;

                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);

                        }
                        else
                        {
                            if (dr.Cells["Col_VoucherType"].Value == null && dr.Cells["Col_Name"].Value != null && dr.Cells["Col_Batch"].Value == null)
                            {
                                row = new PrintRow(dr.Cells["Col_Name"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            else
                            {
                                if (dr.Cells["Col_VoucherType"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }

                                if (dr.Cells["Col_VoucherNumber"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 40, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_VoucherDate"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 80, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_VoucherType"].Value != null && dr.Cells["Col_Name"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Name"].Value.ToString().PadRight(30), PrintRowPixel, 180, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }

                                if (dr.Cells["Col_Batch"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 430, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Quantity"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 530, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Rate"].Value != null)
                                {
                                    mamt = Convert.ToDouble(dr.Cells["Col_Rate"].Value.ToString());
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(570.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Amount"].Value != null)
                                {

                                    mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(650.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                            }
                        }

                    }
                }
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
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {

                if (dr.Cells["Col_Batch"].Value != null && dr.Cells["Col_Batch"].Value.ToString() == "Total")
                    totrows += 2;
            }
            return totrows;
        }

        private int PrintHead(string FromDate, string ToDate)
        {
            PrintRow row;
            try
            {
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, FromDate, ToDate);

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 50, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 90, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 230, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Batch", PrintRowPixel, 430, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 530, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("S.Rate", PrintRowPixel, 600, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 670, PrintFont);
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

        private int PrintFooter(int PrintRowPixel) // [03.02.2017]
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

        public void ClearControls()
        {
            try
            {
                //cbSelectAll.Checked = false;
                lblFooterMessage.Text = "";
                mcbProduct.SelectedID = "";
                InitializeReportGrid();
                ConstructMultiSelectionGridColumns();
                dgvMultiSelection.Columns["Col_ID"].Visible = false;
                _BindingSourceMultiSelection = new DataTable();
                dgvMultiSelection.DataSource = _BindingSourceMultiSelection;

                //if (month < 10)
                //    smonth = "0" + month.ToString("0");
                //else
                //    smonth = month.ToString("00");
                //_Statements.FromDate = syear + smonth + "01";
                //_Statements.ToDate = syear + smonth + "15";

                //  txtFromDate.Text = General.GetDateInShortDateFormat(_Statements.FromDate);


                //fromDate1.Value = DateTime.Now;
                //toDate1.Value = DateTime.Now;
                DataTable dtable = new DataTable();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                column.DataPropertyName = "VoucherSubType";
                column.HeaderText = "SubTYPE";
                column.Width = 30;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "DATE";
                column.DefaultCellStyle.Format = "d";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "PatientName";
                column.HeaderText = "Party";
                column.Width = 300;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Address";
                column.DataPropertyName = "Address";
                column.HeaderText = "Address";
                column.Width = 300;
                //  column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Rate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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
        private void ConstructMultiSelectionGridColumns()
        {
            try
            {
                dgvMultiSelection.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgvMultiSelection.Columns.Add(column);

                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
                //    columnCheck.DataPropertyName = "TAG";
                columnCheck.HeaderText = "Check";
                column.ValueType = typeof(bool);
                columnCheck.Width = 100;
                dgvMultiSelection.Columns.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 150;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 150;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "P.Rate";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 150;
                dgvMultiSelection.Columns.Add(column);


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

        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
        }
        public void HidepnlGO()
        {
            mcbProduct.SelectedID = "";
            ConstructMultiSelectionGridColumns();
            dgvMultiSelection.Columns["Col_ID"].Visible = false;
            _BindingSourceMultiSelection = new DataTable();
            dgvMultiSelection.DataSource = null;

            //cbSelectAll.Checked = false;
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            //ViewFromDate.Text = "";
            //ViewToDate.Text = "";
            //txtViewText.Text = "";
            //fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
            //ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            //ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            dgvReportList.Focus();
        }
        private void RefreshSelectedRowCounter()
        {
            //selectedRowCount = dgvMultiSelection.Rows.Count;
            //txtNoofSearches.Text = selectedRowCount.ToString("#0");
        }
        private void FillAccountData(int ProductID, string batch, double mrp)
        {
            try
            {
                int _RowIndex;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                currentdr.Cells["Col_ID"].Value = ProductID;
                _MProductName = mcbProduct.SeletedItem.ItemData[1].ToString() + " " + mcbProduct.SeletedItem.ItemData[2].ToString() + " " + mcbProduct.SeletedItem.ItemData[3].ToString() + " " + mcbProduct.SeletedItem.ItemData[4].ToString();
                _MMrp = mrp.ToString("#0.00");
                currentdr.Cells["Col_Name"].Value = _MProductName + " MRP:" + _MMrp;
                double mtotamount = 0;
                int mtotquantity = 0;

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    int drproudtid = 0;
                    string drbatch = "";
                    double drmrp = 0;
                    drproudtid = Convert.ToInt32(dr["ProductID"].ToString());
                    drbatch = dr["BatchNumber"].ToString();
                    drmrp = Convert.ToDouble(dr["MRP"].ToString());
                    if (drproudtid == ProductID && drbatch == batch && drmrp == mrp)
                    {

                        double mamt = 0;

                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["MasterSaleID"].ToString();
                        currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                        currentdr.Cells["Col_VoucherDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_Name"].Value = dr["PatientName"].ToString();
                        currentdr.Cells["Col_Address"].Value = dr["Address"].ToString();
                        currentdr.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();
                        currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();
                        mamt = Convert.ToDouble(dr["SaleRate"].ToString());
                        currentdr.Cells["Col_Rate"].Value = mamt.ToString("#0.00");
                        mamt = Convert.ToDouble(dr["Amount"].ToString());
                        currentdr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                        mtotamount += Convert.ToDouble(dr["Amount"].ToString());
                        mtotquantity += Convert.ToInt32(dr["Quantity"].ToString());
                    }
                }
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_ID"].Value = ProductID;
                currentdr.Cells["Col_Batch"].Value = "Total";
                currentdr.Cells["Col_Quantity"].Value = mtotquantity.ToString();
                currentdr.Cells["Col_Amount"].Value = mtotamount.ToString("#0.00");
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }


        }

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelection1Click();
        }


        private void btnOKMultiSelection1Click()
        {            
            try
            {               
                
                    this.Cursor = Cursors.WaitCursor;
                    lblFooterMessage.Text = "";
                    InitializeReportGrid();
                    if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                    {
                        //FromDt = General.GetDateInDateFormat(FromDt);
                        //ToDt = General.GetDateInDateFormat(ToDt);
                        ShowpnlGO();
                        //txtViewText.Text = mcbProduct.SeletedItem.ItemData[1];
                        headerLabel1.Text = "CNFSale - PRODUCT-BATCHWISE ";

                        bool ifselected = false;
                        rowCollection = new List<DataGridViewRow>();
                     
                        if (rowCollection.Count > 0)
                        {
                            if (dgvReportList.Rows.Count > 0)
                                dgvReportList.Rows.Clear();
                            int mProductID = 0;
                            string mbatch = "";
                            foreach (DataGridViewRow row in rowCollection)
                            {
                                mProductID = 0;
                                mbatch = "";
                                double mmrp = 0;
                                if (row.Cells["Col_ID"].Value != null)
                                    mProductID = Convert.ToInt32(row.Cells["Col_ID"].Value.ToString());
                                if (row.Cells["Col_Batch"].Value != null)
                                    mbatch = row.Cells["Col_Batch"].Value.ToString();
                                if (row.Cells["Col_MRP"].Value != null)
                                    double.TryParse(row.Cells["Col_MRP"].Value.ToString(), out mmrp);

                                //if (mProductID < 0)
                                //{
                                    GetBindingSource();


                                    //if (_BindingSource.Rows.Count > 0)
                                    //{
                                    //    FillAccountData(mProductID, mbatch, mmrp);
                                    //}
                                //}
                            }
                        }
                    }
                    this.Cursor = Cursors.Default;
                    PrintReportHead = "CNFSale Report[Product/Batch]"; // From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate); // Modified On [06.02.2017]
                    PrintReportHead2 = "";
                    NoofRows();
                    dgvReportList.Focus();               

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void GetBindingSource()
        {
            DataTable dtable = new DataTable();
            dtable = _SaleList.GetCNFSaleDataForGivenProductBatchMrp(Convert.ToInt32(mcbProduct.SelectedID));
            _BindingSource = dtable;
        }
        private void FillMultiSelectionGrid()
        {

            try
            {
                RefreshSelectedRowCounter();
                ConstructMultiSelectionGridColumns();
                dgvMultiSelection.Columns["Col_ID"].Visible = false;
                FillMultiSelectionData();
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
        private void FillMultiSelectionData()
        {
            try
            {
                if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                {
                    DataTable dtable = _Stock.GetBatchListByProductIDForPurchaseBatchWise(Convert.ToInt32(mcbProduct.SelectedID));
                    _BindingSourceMultiSelection = dtable;
                    dgvMultiSelection.DataSource = _BindingSourceMultiSelection;
                }
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
                ConstructReportColumns();
                DataTable dtable = new DataTable();
                if (mcbProduct.SelectedID != null)
                {
                    //dtable = _SaleList.GetSaleDataForGivenProduct(Convert.ToInt32(mcbProduct.SelectedID));
                    dtable = _SaleList.GetCNFSaleDataForGivenProductBatchMrp(Convert.ToInt32(mcbProduct.SelectedID));
                }
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillProductCombo()
        {
            try
            {
                mcbProduct.SelectedID = null;
                mcbProduct.SourceDataString = new string[2] { "CustomerID", "CustomerName" };
                mcbProduct.ColumnWidth = new string[2] { "0", "250" };
                mcbProduct.ValueColumnNo = 0;
                mcbProduct.UserControlToShow = new UclProduct();
                DataTable dtable = new DataTable();
                //dtable = General.ProductList;
                Product prod = new Product();
                dtable = prod.GetEcoMartLicensesData();
                mcbProduct.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion

        #region Events

        private void mcbProduct_SeletectIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                    FillMultiSelectionData();
                else
                {
                    ConstructMultiSelectionGridColumns();
                    dgvMultiSelection.DataSource = null;
                    dgvMultiSelection.Columns["Col_ID"].Visible = false;
                }
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
        }


        //private void mcbProduct_EnterKeyPressed(object sender, EventArgs e)
        //{           
        //    cbSelectAll.Focus();
        //}
       

     

        //private void dgvMultiSelection_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        btnOKMultiSelectionClick();
        //}
        private void dgvMultiSelection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvMultiSelection.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
                    if (vousubtype == FixAccounts.SubTypeForRegularSale)
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

        private void mcbProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                btnOKMultiSelection1.Focus();
           
        }
        #endregion

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                //ttSaleProductBatch.SetToolTip(mcbProduct, "Select Product and Press Enter");
                //ttSaleProductBatch.SetToolTip(cbSelectAll, "Check to Select All Products in the List and Click OK");
                //ttSaleProductBatch.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                //ttSaleProductBatch.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion

        private void psLabel4_Click(object sender, EventArgs e)
        {

        }

        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            btnOKMultiSelection1Click();
        }
    }
}
