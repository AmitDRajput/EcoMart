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
    public partial class UclSchemeListProductPurchase : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceMultiSelection;
        private Product _Product;
        private Purchase _Purchase;
        private string _MFromDate;
        private string _MToDate;
        private List<DataGridViewRow> rowCollection;
        #endregion

        # region Constructor


        public UclSchemeListProductPurchase()
        {
            try
            {

                InitializeComponent();
                ViewControl = new UclPurchase();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        #region IOverView Members
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _Purchase = new Purchase();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "PURCHASE-PRODUCTWISE";
                ConstructReportColumns();
                ConstructSelectedGridColumns();
                ClearControls();
                pnlMultiSelection.BringToFront();
                pnlMultiSelection.Visible = true;
                dgvSelected.Visible = false;
                if (dgvMultiSelection.Rows.Count > 0)
                    dgvMultiSelection.Rows.Clear();
                FillMultiSelectionGrid();
                txtNoofAccounts.Enabled = false;
                btnOKMultiSelection.Visible = false;
                AddToolTip();
                cbSelectAll.Checked = false;
                cbSelectAll.Visible = false;
                tsbtnPrint.Enabled = false;
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
                pnlMultiSelection.Visible = true;
                tsbtnPrint.Enabled = false;
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
            txtSearch.Focus();
        }

        #endregion IOverview Members

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
                PrintReportHead = "Scheme Report [Product/Purchase] From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                totalrows = CalculateTotalRows(totalrows);
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                //int mlen = 0;
                //int colpix = 0;               
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_BillNumber"].Value != null || dr.Cells["Col_AccName"].Value != null)
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
                     //   double mamt = 0;

                        if (dr.Cells["Col_VoucherType"].Value == null && dr.Cells["Col_BillNumber"].Value != null)
                        {


                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 13;
                            PrintRowCount += 1;
                            if (dr.Cells["Col_AccName"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_BillNumber"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_BillNumber"].Value.ToString().PadRight(15), PrintRowPixel, 340, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Quantity"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 500, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_SCM"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_SCM"].Value.ToString().PadLeft(6), PrintRowPixel, 540, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            //if (dr.Cells["Col_Amount"].Value != null)
                            //{

                            //    mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            //    mlen = (mamt.ToString("#0.00").Length);
                            //    colpix = Convert.ToInt32(670.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            //    PrintBill.Rows.Add(row);
                            //}
                            PrintRowPixel += 13;
                            PrintRowCount += 1;

                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);

                        }
                        else
                        {
                            //if (dr.Cells["Col_AccName"].Value != null)
                            //{
                            //    row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            //    PrintBill.Rows.Add(row);
                            //}
                            if (dr.Cells["Col_VoucherType"].Value == null && dr.Cells["Col_AccName"].Value != null && dr.Cells["Col_BillNumber"].Value == null)
                            {
                                row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            else
                            {
                                if (dr.Cells["Col_AccName"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_VoucherType"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 200, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }

                                if (dr.Cells["Col_VoucherNumber"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 240, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_VoucherDate"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 290, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_BillNumber"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_BillNumber"].Value.ToString().PadRight(30), PrintRowPixel, 340, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }

                                if (dr.Cells["Col_Batch"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 420, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Quantity"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 530, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_SCM"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_SCM"].Value.ToString().PadLeft(6), PrintRowPixel, 600, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                //if (dr.Cells["Col_PRate"].Value != null)
                                //{
                                //    mamt = Convert.ToDouble(dr.Cells["Col_PRate"].Value.ToString());
                                //    mlen = (mamt.ToString("#0.00").Length);
                                //    colpix = Convert.ToInt32(580.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                //    PrintBill.Rows.Add(row);
                                //}
                                //if (dr.Cells["Col_Amount"].Value != null)
                                //{

                                //    mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                //    mlen = (mamt.ToString("#0.00").Length);
                                //    colpix = Convert.ToInt32(670.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                //    PrintBill.Rows.Add(row);
                                //}
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2);

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
                row = new PrintRow("Party", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Type", PrintRowPixel, 200, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 240, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 290, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Bill No", PrintRowPixel, 340, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Batch", PrintRowPixel, 420, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 530, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM/Repl", PrintRowPixel, 600, PrintFont);
                PrintBill.Rows.Add(row);
                //row = new PrintRow("T.Rate", PrintRowPixel, 610, PrintFont);
                //PrintBill.Rows.Add(row);
                //row = new PrintRow("Amount", PrintRowPixel, 690, PrintFont);
                //PrintBill.Rows.Add(row);

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
                string startdate = General.GetDateInDateFormat(General.ShopDetail.Shopsy);
                FromDate.Text = startdate;
                string enddate = General.GetDateInDateFormat(General.ShopDetail.Shopey);
                ToDate.Text = enddate;
                FromDate.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                FromDate.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                ToDate.MinDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopsy);
                ToDate.MaxDate = General.ConvertStringToDateyyyyMMdd(General.ShopDetail.Shopey);
                lblFooterMessage.Text = "";
                ClearGrid();
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
                column.DataPropertyName = "PurchaseID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 190;
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
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 70;
                column.DefaultCellStyle.Format = "N";
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SCM";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "SCM";
                column.DefaultCellStyle.Format = "N";
                column.Width = 70;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";               
                column.Width = 120;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
                column.Width = 100;
                column.Visible = false;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.Visible = false;
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
              //  columnCheck.DataPropertyName = "TAG";
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
                column.Width = 200;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 200;
                dgvSelected.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Comp";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 200;
                dgvSelected.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillReportData(string AccID, string party, string UOM, string Pack, string comp)
        {
            try
            {
                int _RowIndex;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                currentdr.Cells["Col_ID"].Value = AccID;
                currentdr.Cells["Col_AccName"].Value = party.Trim() + " " + UOM + " " + Pack + " " + comp;

                double mtotamount = 0;
                int mtotquantity = 0;
                int mtotscheme = 0;

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
                        currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        currentdr.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();
                        currentdr.Cells["Col_SCM"].Value = dr["SchemeQuantity"].ToString();
                        currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();
                        mamt = Convert.ToDouble(dr["PurchaseRate"].ToString());
                        currentdr.Cells["Col_PRate"].Value = mamt.ToString("#0.00");
                        mamt = Convert.ToDouble(dr["Amount"].ToString());
                        currentdr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                        mtotamount += Convert.ToDouble(dr["Amount"].ToString());
                        mtotquantity += Convert.ToInt32(dr["Quantity"].ToString());
                        mtotscheme += Convert.ToInt32(dr["SchemeQuantity"].ToString());
                    }
                }


                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_BillNumber"].Value = "Total";
                currentdr.Cells["Col_Quantity"].Value = mtotquantity.ToString();
                currentdr.Cells["Col_SCM"].Value = mtotscheme.ToString();
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
        private void NoofRowsSelected()
        {
            int noofrecords = dgvSelected.Rows.Count;
            try
            {
                if (noofrecords == 0)
                    txtNoofAccounts.Text = "NO Records ";
                else if (noofrecords == 1)
                    txtNoofAccounts.Text = "Record : " + noofrecords.ToString().Trim();
                else
                    txtNoofAccounts.Text = "Records : " + noofrecords.ToString().Trim();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillMultiSelectionGrid()
        {

            try
            {
                ConstructMultiSelectionGridColumns();
                if (dgvMultiSelection.Rows.Count > 0)
                    dgvMultiSelection.Rows.Clear();
                FillMultiSelectionData();
                dgvMultiSelection.DataSource = _BindingSourceMultiSelection;

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
                _Product = new Product();
                dtable = General.ProductList;
                _BindingSourceMultiSelection = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void NoofSearchesSelected()
        {
            int i = 0;
            bool iftrue = false;
            if (dgvSelected.Rows.Count > 0)
                dgvSelected.Rows.Clear();
            _BindingSourceMultiSelection.DefaultView.RowFilter = "";

            try
            {
                foreach (DataGridViewRow drow in dgvMultiSelection.Rows)
                {
                    if (drow.Cells["Col_Check"].Value != DBNull.Value && drow.Cells["Col_Check"] != null)
                        iftrue = Convert.ToBoolean(drow.Cells["Col_Check"].Value);
                    else
                        iftrue = false;
                    if (iftrue)
                    {
                        int selectedrowindex = dgvSelected.Rows.Add();
                        dgvSelected.Rows[selectedrowindex].Cells["Col_ID"].Value = drow.Cells["Col_ID"].Value.ToString();
                        dgvSelected.Rows[selectedrowindex].Cells["Col_Name"].Value = drow.Cells["Col_Name"].Value.ToString();
                        dgvSelected.Rows[selectedrowindex].Cells["Col_UOM"].Value = drow.Cells["Col_UOM"].Value.ToString();
                        dgvSelected.Rows[selectedrowindex].Cells["Col_Pack"].Value = drow.Cells["Col_Pack"].Value.ToString();
                        dgvSelected.Rows[selectedrowindex].Cells["Col_Comp"].Value = drow.Cells["Col_Comp"].Value.ToString();
                        i += 1;
                    }
                }
                txtNoofAccounts.Enabled = true;
                txtNoofAccounts.Text = i.ToString("#0");
                txtNoofAccounts.Enabled = false;
            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }
        }
        private void GetPurchaseDataProductwise(string productID)
        {
            DataTable dtable = new DataTable();
            dtable = _Purchase.GetPurchaseDataProductWiseWithScheme(productID);
            _BindingSource = dtable;
        }
        #endregion Other Private Methods

        # region Events
        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnOKMultiSelection.Visible != true || (btnOKMultiSelection.Visible == true && cbSelectAll.Visible == false))
                {
                    foreach (DataGridViewRow drow in dgvMultiSelection.Rows)
                    {
                        drow.Cells["Col_Check"].Value = cbSelectAll.Checked;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSearch.Text != null && txtSearch.Text.ToString() != "")
            {
                CheckFiltertxtSearch(txtSearch.Text.ToString().Trim());
                btnOK.Visible = true;
                btnOKMultiSelection.Visible = false;
                cbSelectAll.Visible = true;
                cbSelectAll.Checked = false;
                dgvMultiSelection.Focus();
            }
        }

        private void btnViewList_KeyDown(object sender, KeyEventArgs e)
        {
            btnViewListClick();
        }
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
        private void btnViewList_Click(object sender, EventArgs e)
        {
            btnViewListClick();
        }

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void btnOKMultiSelectionClick()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ConstructReportColumns();
                tsbtnPrint.Enabled = true;
                pnlMultiSelection.Visible = false;
                _MFromDate = FromDate.Value.Date.ToString("yyyyMMdd");
                _MToDate = ToDate.Value.Date.ToString("yyyyMMdd");
                ViewFromDate.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
                ViewToDate.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);
                //  headerLabel1.Text = "PURCHASE - PRODUCTWISE " + "From :" + General.GetDateInShortDateFormat(_MFromDate) + "   To :" + General.GetDateInShortDateFormat(_MToDate);
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
                                FillReportData(row.Cells["Col_ID"].Value.ToString(), row.Cells["Col_Name"].Value.ToString(), row.Cells["Col_UOM"].Value.ToString(),
                               row.Cells["Col_Pack"].Value.ToString(), row.Cells["Col_Comp"].Value.ToString());
                            }
                        }
                    }
                }
                this.Cursor = Cursors.Default;
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            btnOKClick();
        }
        private void btnOKClick()
        {
            try
            {

                NoofSearchesSelected();
                btnOKMultiSelection.Visible = true;
                cbSelectAll.Visible = false;
                txtSearch.Text = "";
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            //    _BindingSourceMultiSelection.DefaultView.RowFilter = "";
        }
        private void ClearGrid()
        {
            if (dgvReportList.Rows.Count > 0)
            {
                DataTable dtable = new DataTable();
                _BindingSource = dtable;
            }
        }
        private void FromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ToDate.Focus();
        }
        private void ToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK.Focus();
        }
        private void dgvReport_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string voutype = "";
            string vousubtype = "";
            try
            {
                if (dgvReportList.CurrentRow != null && dgvReportList.Rows.Count > 0)
                {
                    vousubtype = "";
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.CurrentRow.Cells["Col_ID"].Value.ToString();
                    voutype = dgvReportList.CurrentRow.Cells["Col_VoucherType"].Value.ToString();
                    vousubtype = dgvReportList.CurrentRow.Cells["Col_VoucherSubType"].Value.ToString();
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

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttSchemeProductPurchase.SetToolTip(txtSearch, "Type First Few characters of Product Name and Press Enter");
                ttSchemeProductPurchase.SetToolTip(cbSelectAll, "Check to Select All Products in the List and Click OK");
                ttSchemeProductPurchase.SetToolTip(btnViewList, "Click to View List of Selected Products and Click Again to Close the List");
                ttSchemeProductPurchase.SetToolTip(btnOK, "Click to Save the Seleted Products into the List");
                ttSchemeProductPurchase.SetToolTip(btnOKMultiSelection, "Click to See Report = F10");
                ttSchemeProductPurchase.SetToolTip(pnlMultiSelection, "F12 to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclSchemeListProductPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion


    }
}
