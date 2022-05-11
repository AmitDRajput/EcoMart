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
    public partial class UclPurchaseListPartyProduct : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceMultiSelection;
        private Purchase _Purchase;
        private string _MFromDate;
        private string _MToDate;
        private List<DataGridViewRow> rowCollection;
        private int selectedRowCount;
        #endregion

        # region Constructor
        public UclPurchaseListPartyProduct()
        {
            InitializeComponent();
            ViewControl = new UclPurchase();
        }
        #endregion

        #region IOverview Members


        public override void ShowOverview()
        {
            try
            {
                selectedRowCount = 0;
                _BindingSource = new DataTable();
                _Purchase = new Purchase();
                rowCollection = new List<DataGridViewRow>();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "PURCHASE-PARTY/PRODUCT";
                fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
                toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);
                InitializeReportGrid();
                ConstructSelectedGridColumns();
                FillPartyCombo();
                ClearControls();
                HidepnlGO();
                dgvSelected.Visible = false;
                FillMultiSelectionGrid();
                txtNoofSearches.Enabled = false;
                AddToolTip();
                cbSelectAll.Checked = false;
                cbSelectAll.Visible = false;
                mcbCreditor.Focus();
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
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }

        public override void SetFocus()
        {
            base.SetFocus();
            mcbCreditor.Focus();
        }

        #endregion

        # region IReport Members

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
                PrintReportHead = "Purchase Report [Party/Products]  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "[" + txtViewParty.Text.ToString() + "]";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                totalrows = CalculateTotalRows(totalrows);
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                // totalrows += 7; // for first page heading
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
                        //  row = new PrintRow("", 0, 0, PrintFont);
                        double mamt = 0;

                        if (dr.Cells["Col_ProductName"].Value != null && dr.Cells["Col_Batch"].Value != null)
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 13;
                            PrintRowCount += 1;
                            if (dr.Cells["Col_ProductName"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Batch"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 250, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Quantity"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 350, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Amount"].Value != null)
                            {

                                mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(550.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
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
                            if (dr.Cells["Col_ProductName"].Value != null && dr.Cells["Col_Batch"].Value == null)
                            {
                                row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
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
                                    row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 50, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_VoucherDate"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 150, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }


                                if (dr.Cells["Col_Batch"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 250, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Quantity"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 350, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_PRate"].Value != null)
                                {
                                    mamt = Convert.ToDouble(dr.Cells["Col_PRate"].Value.ToString());
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(450.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Amount"].Value != null)
                                {

                                    mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(550.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                            }
                        }

                    }
                }

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

        private int PrintHead()
        {
            PrintRow row;
            try
            {
                PrintFont = new Font("Arial", 10, FontStyle.Regular);
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2);
                PrintRowPixel += 17;
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 60, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 150, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Batch", PrintRowPixel, 250, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 350, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("S.Rate", PrintRowPixel, 490, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 580, PrintFont);
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
                string startdate = General.GetDateInDateFormat(General.ShopDetail.Shopsy);
                fromDate1.Text = startdate;
                string enddate = General.GetDateInDateFormat(General.ShopDetail.Shopey);
                toDate1.Text = enddate;
                lblFooterMessage.Text = "";
                mcbCreditor.SelectedID = "";
                txtSearch.Text = "";
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
            dgvReportList.Columns.Clear();
            DataGridViewTextBoxColumn column;
            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ID";
            column.DataPropertyName = "PurchaseID";
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
            column.HeaderText = "No";
            column.Width = 60;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_VoucherDate";
            column.DataPropertyName = "VoucherDate";
            column.HeaderText = "Date";
            column.Width = 100;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_BillNumber";
            column.DataPropertyName = "PurchaseBillNumber";
            column.HeaderText = "Bill.Number";
            column.Width = 100;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Name";
            column.DataPropertyName = "ProdName";
            column.HeaderText = "Product";
            column.Width = 190;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Quantity";
            column.DataPropertyName = "Quantity";
            column.HeaderText = "Qty";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 50;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_SCM";
            column.DataPropertyName = "SchemeQuantity";
            column.HeaderText = "SCM";
            column.Width = 50;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Batch";
            column.DataPropertyName = "BatchNumber";
            column.HeaderText = "Batch";
            column.Width = 80;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_PRate";
            column.DataPropertyName = "PurchaseRate";
            column.HeaderText = "Pur.Rate";
            column.Width = 100;
            column.DefaultCellStyle.Format = "N2";
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_Amount";
            column.DataPropertyName = "Amount";
            column.HeaderText = "Amount";
            column.Width = 100;
            column.DefaultCellStyle.Format = "N2";
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
            dgvReportList.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Col_ProductName";
            column.Visible = false;
            column.Width = 120;
            dgvReportList.Columns.Add(column);
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
                //   columnCheck.DataPropertyName = "TAG";
                columnCheck.HeaderText = "Check";
                column.ValueType = typeof(bool);
                columnCheck.Width = 100;
                dgvMultiSelection.Columns.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ConstructSelectedGridColumns()
        {
            try
            {
                dgvSelected.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "ProductName";
                column.HeaderText = "Name";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 200;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ValueType = typeof(string);
                column.ReadOnly = true;
                column.Width = 50;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 80;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 80;
                dgvSelected.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        //private void FillReportGrid()
        //{
        //    FillReportData();
        //    dgvReportList.DataSource = _BindingSource;
        //    dgvReportList.DateColumnNames.Add("Col_VoucherDate");
        //    dgvReportList.DoubleColumnNames.Add("Col_Amount");
        //    dgvReportList.Bind();
        //    double totamt = 0;
        //    double totqty = 0;
        //    double totscm = 0;

        //    foreach (DataGridViewRow dr in dgvReportList.Rows)
        //    {
        //        totamt += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
        //        totqty += Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
        //        totscm += Convert.ToDouble(dr.Cells["Col_Scm"].Value.ToString());
        //    }

        //    txtReportTotalAmount.Text = totamt.ToString("#0.00");
        //    txtTotQuantity.Text = totqty.ToString("#0");
        //    txtTotScheme.Text = totscm.ToString("#0");

        //    int noofrecords = dgvReportList.Rows.Count;
        //    if (noofrecords == 0)
        //        lblFooterMessage.Text = "NO Records ";
        //    else if (noofrecords == 1)
        //        lblFooterMessage.Text = "Record : " + noofrecords.ToString().Trim();
        //    else
        //        lblFooterMessage.Text = "Records : " + noofrecords.ToString().Trim();
        //}

        //private void FillReportData()
        //{
        //    DataTable dtable = new DataTable();
        //    if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
        //        dtable = _Purchase.GetOverviewDataForPartyProductList(mcbCreditor.SelectedID, mcbProduct.SelectedID);
        //    _BindingSource = dtable;
        //}

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
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
            ViewFromDate.Visible = true;
            ViewToDate.Visible = true;
            ViewFromDate.Value = fromDate1.Value;
            ViewToDate.Value = toDate1.Value;
            dgvReportList.Focus();
        }

        private void BindReportGrid(string AccID, string party, string UOM, string Pack, string comp)
        {
            try
            {
                int _RowIndex = 0;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                currentdr.Cells["Col_ID"].Value = AccID;
                currentdr.Cells["Col_Name"].Value = party.Trim() + " " + UOM + " " + Pack + " " + comp;
                currentdr.Cells["Col_ProductName"].Value = party.Trim() + " " + UOM + " " + Pack + " " + comp;

                double mtotamount = 0;
                int mtotquantity = 0;

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (dr["ProductID"].ToString() == AccID && Convert.ToInt32(dr["VoucherDate"].ToString()) >= Convert.ToInt32(_MFromDate) && Convert.ToInt32(dr["VoucherDate"].ToString()) <= Convert.ToInt32(_MToDate))
                    {

                        double mamt = 0;

                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                        currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_VoucherDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillNumber"].Value = dr["PurchaseBillNumber"].ToString();
                        currentdr.Cells["Col_Name"].Value = (party.Trim() + " " + UOM + " " + Pack + " " + comp).ToString();
                        currentdr.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();
                        currentdr.Cells["Col_SCM"].Value = dr["SchemeQuantity"].ToString();
                        currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();
                        mamt = Convert.ToDouble(dr["PurchaseRate"].ToString());
                        currentdr.Cells["Col_PRate"].Value = mamt.ToString("#0.00");
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
                currentdr.Cells["Col_ID"].Value = AccID;
                currentdr.Cells["Col_Name"].Value = (party.Trim() + " " + UOM + " " + Pack + " " + comp);
                currentdr.Cells["Col_ProductName"].Value = party.Trim() + " " + UOM + " " + Pack + " " + comp;
                currentdr.Cells["Col_Batch"].Value = "Total";
                currentdr.Cells["Col_Quantity"].Value = mtotquantity.ToString();
                currentdr.Cells["Col_Amount"].Value = mtotamount.ToString("#0.00");
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }


        }
        private void CheckFiltertxtSearch(string txtString)
        {

            try
            {
                _BindingSourceMultiSelection.DefaultView.RowFilter = "ProdName like '" + txtString + "%'";
            }
            catch (Exception ex) { Log.WriteException(ex); }

        }

        private void FillMultiSelectionGrid()
        {

            try
            {
                selectedRowCount = 0;
                RefreshSelectedRowCounter();
                dgvMultiSelection.DataSource = null;
                ConstructMultiSelectionGridColumns();              
                FillMultiSelectionData();
                dgvMultiSelection.DataSource = _BindingSourceMultiSelection;
               // _BindingSourceMultiSelection.DefaultView.RowFilter = "";

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillMultiSelectionData()
        {
            try
            {
                DataTable dtable = new DataTable();            
                dtable = General.ProductList;
                _BindingSourceMultiSelection = dtable;
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
                    AddOptionalColumns();
                    ShowpnlGO();
                    txtViewParty.Text = mcbCreditor.SeletedItem.ItemData[2].ToString();
                    _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                    _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                    ViewFromDate.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
                    ViewToDate.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);
                    if (dgvReportList.Rows.Count > 0)
                        dgvReportList.Rows.Clear();
                    rowCollection = new List<DataGridViewRow>();
                    foreach (DataGridViewRow selectedrow in dgvSelected.Rows)
                    {
                        rowCollection.Add(selectedrow);
                    }
                    if (rowCollection.Count > 0)
                    {
                        if (dgvReportList.Rows.Count > 0)
                            dgvReportList.Rows.Clear();
                        string mproductID = "";
                        foreach (DataGridViewRow row in rowCollection)
                        {
                            mproductID = "";
                            if (row.Cells["Col_ID"].Value != null)
                                mproductID = row.Cells["Col_ID"].Value.ToString();
                            if (mproductID != "")
                            {
                                GetPurchaseDataProductwise(mproductID);
                                if (_BindingSource.Rows.Count > 0)
                                {
                                    BindReportGrid(row.Cells["Col_ID"].Value.ToString(), row.Cells["Col_Name"].Value.ToString(), row.Cells["Col_UOM"].Value.ToString(),
                                    row.Cells["Col_Pack"].Value.ToString(), row.Cells["Col_Comp"].Value.ToString());
                                }
                            }
                        }
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

        //private void NoofSearchesSelected()
        //{
        //    int i = 0;
        //    bool iftrue = false;
        //    if (dgvSelected.Rows.Count > 0)
        //        dgvSelected.Rows.Clear();
        //    _BindingSourceMultiSelection.DefaultView.RowFilter = "";
        //    try
        //    {
        //        foreach (DataGridViewRow drow in dgvMultiSelection.Rows)
        //        {
        //            if (drow.Cells["Col_Check"].Value != DBNull.Value && drow.Cells["Col_Check"] != null)
        //                iftrue = Convert.ToBoolean(drow.Cells["Col_Check"].Value);
        //            else
        //                iftrue = false;
        //            if (iftrue)
        //            {
        //                int selectedrowindex = dgvSelected.Rows.Add();
        //                dgvSelected.Rows[selectedrowindex].Cells["Col_ID"].Value = drow.Cells["Col_ID"].Value.ToString();
        //                dgvSelected.Rows[selectedrowindex].Cells["Col_Name"].Value = drow.Cells["Col_Name"].Value.ToString();
        //                dgvSelected.Rows[selectedrowindex].Cells["Col_UOM"].Value = drow.Cells["Col_UOM"].Value.ToString();
        //                dgvSelected.Rows[selectedrowindex].Cells["Col_Pack"].Value = drow.Cells["Col_Pack"].Value.ToString();
        //                dgvSelected.Rows[selectedrowindex].Cells["Col_Comp"].Value = drow.Cells["Col_Comp"].Value.ToString();
        //                i += 1;
        //            }
        //        }
        //        txtNoofSearches.Enabled = true;
        //        txtNoofSearches.Text = i.ToString("#0");
        //        txtNoofSearches.Enabled = false;
        //        txtSearch.Text = "";

        //    }
        //    catch (Exception Ex)
        //    {

        //        Log.WriteException(Ex);
        //    }
        //}

        private void btnViewListClick()
        {
            if (dgvSelected.Visible == false)
            {
                dgvSelected.Visible = true;
                dgvMultiSelection.Enabled = false;
                btnViewList.Text = "Close";
            }
            else
            {
                dgvSelected.Visible = false;
                dgvMultiSelection.Enabled = true;
                btnViewList.Text = "View";
            }
        }

        private void GetPurchaseDataProductwise(string productID)
        {
            DataTable dtable = new DataTable();
            string acccode = mcbCreditor.SeletedItem.ItemData[1].ToString();
            dtable = _Purchase.GetOverviewDataForPartyProductList(mcbCreditor.SelectedID, productID);
            _BindingSource = dtable;
        }

        private void RefreshSelectedRowCounter()
        {
            selectedRowCount = dgvSelected.Rows.Count;
            txtNoofSearches.Text = selectedRowCount.ToString("#0");
        }

        private void FillPartyCombo()
        {
            mcbCreditor.SelectedID = null;
            mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccDiscountOffered" };
            mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "150", "50" };
            mcbCreditor.DisplayColumnNo = 2;
            mcbCreditor.ValueColumnNo = 0;
            mcbCreditor.UserControlToShow = new UclAccount();
            Account _Party = new Account();
            DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
            mcbCreditor.FillData(dtable);
        }

        private void AddIndgvSelectedGrid(DataGridViewRow drow)
        {
            bool iffound = false;
            foreach (DataGridViewRow drowselected in dgvSelected.Rows)
            {
                if (drowselected.Cells["Col_ID"].Value.ToString() == drow.Cells["Col_ID"].Value.ToString())
                {
                    iffound = true;
                    break;
                }
            }
            if (!iffound)
            {

                int selectedrowindex = dgvSelected.Rows.Add();
                dgvSelected.Rows[selectedrowindex].Cells["Col_ID"].Value = drow.Cells["Col_ID"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_Name"].Value = drow.Cells["Col_Name"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_UOM"].Value = drow.Cells["Col_UOM"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_Pack"].Value = drow.Cells["Col_Pack"].Value.ToString();
                dgvSelected.Rows[selectedrowindex].Cells["Col_Comp"].Value = drow.Cells["Col_Comp"].Value.ToString();
            }

        }

        private void RemoveFromdgvSelectedGrid(DataGridViewRow drow)
        {
            foreach (DataGridViewRow drowselected in dgvSelected.Rows)
            {
                if (drowselected.Cells["Col_ID"].Value.ToString() == drow.Cells["Col_ID"].Value.ToString())
                {
                    dgvSelected.Rows.Remove(drowselected);
                    break;
                }
            }


        }

        #endregion Other Private Methods

        # region Events
        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void AddOptionalColumns()
        {
            dgvReportList.OptionalColumnNames.Add("Col_Amount");
            dgvReportList.OptionalColumnNames.Add("Col_Rate");
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {               
                    foreach (DataGridViewRow drow in dgvMultiSelection.Rows)
                    {
                        drow.Cells["Col_Check"].Value = cbSelectAll.Checked;
                    }
                
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void ToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelection1.Focus();
        }
        private void dgvMultiSelection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvMultiSelection.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvMultiSelection_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvMultiSelection.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    if (Convert.ToBoolean(dgvMultiSelection.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == true)
                    {
                        selectedRowCount++;
                        rowCollection.Add(dgvMultiSelection.Rows[e.RowIndex]);
                        AddIndgvSelectedGrid(dgvMultiSelection.Rows[e.RowIndex]);
                    }
                    else
                    {
                        selectedRowCount--;
                        RemoveFromdgvSelectedGrid(dgvMultiSelection.Rows[e.RowIndex]);
                    }
                    RefreshSelectedRowCounter();
                }

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSearch.Text != null && txtSearch.Text.ToString() != "")
            {
                CheckFiltertxtSearch(txtSearch.Text.ToString().Trim());
                cbSelectAll.Visible = true;
                cbSelectAll.Checked = false;
                dgvMultiSelection.Focus();
            }
        }

        private void btnViewList_KeyDown(object sender, KeyEventArgs e)
        {
            btnViewListClick();
        }

        private void btnViewList_Click(object sender, EventArgs e)
        {
            btnViewListClick();
        }

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtSearch.Focus();
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



        #endregion Events


        #region ToolTip

        private void AddToolTip()
        {
            try
            {
                ttPurchasePartyProduct.SetToolTip(txtSearch, "Type First Few characters of Product Name and Press Enter");
                ttPurchasePartyProduct.SetToolTip(cbSelectAll, "Check to Select All Products in the List and Click OK");
                ttPurchasePartyProduct.SetToolTip(btnViewList, "Click to View List of Selected Products and Click Again to Close the List");
                ttPurchasePartyProduct.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttPurchasePartyProduct.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion ToolTip

          }
}
