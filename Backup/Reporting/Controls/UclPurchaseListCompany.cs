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
    public partial class UclPurchaseListCompany : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Purchase _Purchase;
        private string _MFromDate;
        private string _MToDate;
        double _MReportTotalAmount = 0;
        double _MTotalAmount = 0;
        int _MTotalScheme = 0;
        int _MTotalQuantity = 0;
        #endregion

        # region Constructor
        public UclPurchaseListCompany()
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

        #region IOverview Members


        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _Purchase = new Purchase();               
                headerLabel1.Text = "PURCHASE-COMPANY";               
                FillCompanyCombo();
                ClearControls(); 
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
                PrintReportHead = "Purchase Report [Company]  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "["+ txtViewText.Text.ToString() +"]" ;
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
                        double mamt = 0;

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
                            if (dr.Cells["Col_Amount"].Value != null)
                            {

                                mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(670.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
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
                                    row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 500, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_SCM"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_SCM"].Value.ToString().PadLeft(6), PrintRowPixel, 540, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_PRate"].Value != null)
                                {
                                    mamt = Convert.ToDouble(dr.Cells["Col_PRate"].Value.ToString());
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(580.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Amount"].Value != null)
                                {

                                    mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(670.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
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
                row = new PrintRow("Qty", PrintRowPixel, 500, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM/Repl", PrintRowPixel, 540, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("T.Rate", PrintRowPixel, 610, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 690, PrintFont);
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
                fromDate1.Value = DateTime.Now;
                toDate1.Value = DateTime.Now;
                lblFooterMessage.Text = "";
                txtReportTotalAmount.Text = "";
                mcbCompany.SelectedID = "";
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
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.Width = 100;               
                dgvReportList.Columns.Add(column);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillReportHeadLine(string AccID, string party, string UOM, string Pack)
        {
            try
            {
                int _RowIndex;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                currentdr.Cells["Col_ID"].Value = AccID;
                currentdr.Cells["Col_AccName"].Value = party.Trim() + " " + UOM + " " + Pack;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillReportRow(DataRow dr)
        {
            try
            {
                int _RowIndex = 0;
                DataGridViewRow currentdr;
                double mamt = 0;
                int mqty = 0;
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
                _MTotalAmount += mamt;
                _MReportTotalAmount += _MTotalAmount;
                if (dr["Quantity"] != DBNull.Value)
                    mqty = Convert.ToInt32(dr["Quantity"].ToString());
                _MTotalQuantity += mqty;
                if (dr["SchemeQuantity"] != DBNull.Value)
                mqty = Convert.ToInt32(dr["SchemeQuantity"].ToString());
                _MTotalScheme += mqty;

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillReportFooter()
        {
            int _RowIndex = 0;
            if (_MTotalAmount > 0)
            {
                DataGridViewRow currentdr;
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_BillNumber"].Value = "Total";
                currentdr.Cells["Col_Quantity"].Value = _MTotalQuantity.ToString();
                currentdr.Cells["Col_SCM"].Value = _MTotalScheme.ToString();
                currentdr.Cells["Col_Amount"].Value = _MTotalAmount.ToString("#0.00");
                _MTotalAmount = 0;
                _MTotalQuantity = 0;
                _MTotalScheme = 0;
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
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Text = "";
            ViewToDate.Text = "";
            mcbCompany.SelectedID = "";
            txtViewText.Text = "";
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            tsbtnPrint.Enabled = true;
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            dgvReportList.Focus();
        }
        //private void FillReportData(string AccID, string party, string UOM, string Pack, string comp)
        //{
        //    try
        //    {
        //        int _RowIndex;
        //        //_RowIndex = dgvReportList.Rows.Add();
        //        //DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
        //        //currentdr.DefaultCellStyle.BackColor = Color.SkyBlue;
        //        //currentdr.Cells["Col_ID"].Value = AccID;
        //        //currentdr.Cells["Col_AccName"].Value = party.Trim() + " " + UOM + " " + Pack + " " + comp;

        //        double mtotamount = 0;
        //        int mtotquantity = 0;
        //        int mtotscheme = 0;

        //        foreach (DataRow dr in _BindingSource.Rows)
        //        {
        //            if (dr["ProductID"].ToString() == AccID && Convert.ToInt32(dr["VoucherDate"].ToString()) >= Convert.ToInt32(_MFromDate) && Convert.ToInt32(dr["VoucherDate"].ToString()) <= Convert.ToInt32(_MToDate))
        //            {

        //                double mamt = 0;

        //                _RowIndex = dgvReportList.Rows.Add();
        //                currentdr = dgvReportList.Rows[_RowIndex];
        //                currentdr.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
        //                currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
        //                currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
        //                currentdr.Cells["Col_VoucherDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
        //                currentdr.Cells["Col_BillNumber"].Value = dr["PurchaseBillNumber"].ToString();
        //                currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
        //                currentdr.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();
        //                currentdr.Cells["Col_SCM"].Value = dr["SchemeQuantity"].ToString();
        //                currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();
        //                mamt = Convert.ToDouble(dr["PurchaseRate"].ToString());
        //                currentdr.Cells["Col_PRate"].Value = mamt.ToString("#0.00");
        //                mamt = Convert.ToDouble(dr["Amount"].ToString());
        //                currentdr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
        //                mtotamount += Convert.ToDouble(dr["Amount"].ToString());
        //                mtotquantity += Convert.ToInt32(dr["Quantity"].ToString());
        //                mtotscheme += Convert.ToInt32(dr["SchemeQuantity"].ToString());
        //            }
        //        }


        //        _RowIndex = dgvReportList.Rows.Add();
        //        currentdr = dgvReportList.Rows[_RowIndex];
        //        currentdr.DefaultCellStyle.BackColor = Color.LightGreen;
        //        currentdr.Cells["Col_BillNumber"].Value = "Total";
        //        currentdr.Cells["Col_Quantity"].Value = mtotquantity.ToString();
        //        currentdr.Cells["Col_SCM"].Value = mtotscheme.ToString();
        //        currentdr.Cells["Col_Amount"].Value = mtotamount.ToString("#0.00");
        //    }

        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }


        //}

        //private void GetPurchaseDataProductwise(string productID)
        //{
        //    DataTable dtable = new DataTable();
        //    dtable = _Purchase.GetPurchaseDataProductWise(productID);
        //    _BindingSource = dtable;
        //}

        //private void FillReportGrid()
        //{
        //    try
        //    {
        //        FillReportData();
        //        dgvReportList.DataSource = _BindingSource;               
        //        dgvReportList.DateColumnNames.Add("Col_VoucherDate");
        //        dgvReportList.DoubleColumnNames.Add("Col_Amount");
        //        dgvReportList.Bind();
        //        double totamt = 0;

        //        foreach (DataGridViewRow dr in dgvReportList.Rows)
        //        {
        //            totamt += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
        //        }

        //        txtReportTotalAmount.Text = totamt.ToString("#0.00");

        //        int noofrecords = dgvReportList.Rows.Count;
        //        if (noofrecords == 0)
        //            lblFooterMessage.Text = "NO Records ";
        //        else if (noofrecords == 1)
        //            lblFooterMessage.Text = "Record : " + noofrecords.ToString().Trim();
        //        else
        //            lblFooterMessage.Text = "Records : " + noofrecords.ToString().Trim();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        //private void GetReportData()
        //{
        //    try
        //    {
        //        DataTable dtable = new DataTable();
        //        string mfromdate = "";
        //        string mtodate = "";

        //        mfromdate = FromDate.Value.Date.ToString("yyyyMMdd");
        //        mtodate = ToDate.Value.Date.ToString("yyyyMMdd");
        //        if (mcbCompany.SelectedID != null && mcbCompany.SelectedID != "")
        //            dtable = _Purchase.GetOverviewDataCompany(mcbCompany.SelectedID, mfromdate, mtodate);
        //        _BindingSource = dtable;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        //private void FilldgvSelected()
        //{
        //    int i = 0;           
        //    if (dgvSelected.Rows.Count > 0)
        //        dgvSelected.Rows.Clear();
        //    string productid = "";
        //    if (_BindingSource.Rows.Count > 0)
        //        productid = _BindingSource.Rows[0]["Col_ID"].ToString();
        //    try
        //    {
        //        foreach (DataRow dr in _BindingSource.Rows)
        //        {
        //            if (dr["Col_ID"].Value != DBNull.Value && drow.Cells["Col_Check"] != null)
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
        //        txtNoofAccounts.Enabled = true;
        //        txtNoofAccounts.Text = i.ToString("#0");
        //        txtNoofAccounts.Enabled = false;
        //    }
        //    catch (Exception Ex)
        //    {

        //        Log.WriteException(Ex);
        //    }
        //}
        private void FillCompanyCombo()
        {
            try
            {
                mcbCompany.SelectedID = null;
                mcbCompany.SourceDataString = new string[2] { "CompID", "CompName" };
                mcbCompany.ColumnWidth = new string[2] { "0", "250" };
                mcbCompany.ValueColumnNo = 0;
                mcbCompany.UserControlToShow = new UclCompany();
                Company _Company = new Company();
                DataTable dtable = _Company.GetOverviewData();
                mcbCompany.FillData(dtable);
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
        #endregion Other Private Methods

        # region Events
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
                retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    InitializeReportGrid();
                    ShowpnlGO();
                    lblFooterMessage.Text = "";
                    string productid = "";                  
                    txtViewText.Text = mcbCompany.SeletedItem.ItemData[1].ToString();
                    if (mcbCompany.SelectedID != null && mcbCompany.SelectedID != "")
                        _BindingSource = _Purchase.GetOverviewDataCompany(mcbCompany.SelectedID, _MFromDate, _MToDate);                   
                    if (_BindingSource.Rows.Count > 0)
                    {
                        productid = _BindingSource.Rows[0]["ProductID"].ToString();
                        FillReportHeadLine(_BindingSource.Rows[0]["ProductID"].ToString(), _BindingSource.Rows[0]["ProdName"].ToString(), _BindingSource.Rows[0]["ProdLoosePack"].ToString(),
                                       _BindingSource.Rows[0]["ProdPack"].ToString());
                    }
                    if (_BindingSource.Rows.Count > 0)
                    {
                        _MTotalAmount = 0;
                        _MTotalQuantity = 0;
                        _MTotalScheme = 0;
                        string mproductID = "";
                        foreach (DataRow dr in _BindingSource.Rows)
                        {
                            mproductID = "";
                            if (dr["ProductID"] != DBNull.Value)
                                mproductID = dr["ProductID"].ToString();
                            if (mproductID != "" && mproductID == productid)
                            {
                                FillReportRow(dr);
                            }
                            else
                            {
                                FillReportFooter();
                                _MTotalAmount = 0;
                                _MTotalQuantity = 0;
                                _MTotalScheme = 0;
                                productid = dr["ProductID"].ToString();
                                FillReportHeadLine(dr["ProductID"].ToString(), dr["ProdName"].ToString(), dr["ProdLoosePack"].ToString(),
                                   dr["ProdPack"].ToString());
                                FillReportRow(dr);

                            }
                        }
                    }
                    FillReportFooter();
                    this.Cursor = Cursors.Default;
                    NoofRows();
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
        private void mcbCompany_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (mcbCompany.SelectedID != null && mcbCompany.SelectedID != "")
                btnOKMultiSelectionClick();
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
        private void fromdate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void todate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbCompany.Focus();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
            else if (e.KeyCode == Keys.Left)
                fromDate1.Focus();
        }
       
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion     

     
      
    }
}
