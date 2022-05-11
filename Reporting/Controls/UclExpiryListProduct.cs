﻿using System;
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

    public partial class UclExpiryListProduct : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private DebitNoteExpiry _DNExpiry;
        public double _TotalAmount = 0;
        private int _FromMonth;
        private int _FromYear;
        private int _ToMonth;
        private int _ToYear;
        # endregion

        #region Constructor
        public UclExpiryListProduct()
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

        # region IOverview
        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _DNExpiry = new DebitNoteExpiry();
                headerLabel1.Text = "EXPIRY-ALL PRODUCTS";
                //txtYear.Text = General.ShopDetail.Shopsy.ToString().Substring(0, 4);
                FillShelfCombo();
                //   FillReportGrid();
                //  dgvReportList.Focus();
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
                pnlMonthYear.Visible = true;
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
            txtFromMonth.Focus();
        }
        # endregion

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
                double mamt = 0;
                double mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_ID"].Value != null || dr.Cells["Col_ID"].Value.ToString() != "")
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
                        PrintColumnPixel = 1;

                        if (dr.Cells["Col_ProductName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(40).Substring(0,35), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        PrintColumnPixel += 250;
                        if (dr.Cells["Col_UOM"].Visible == true && dr.Cells["Col_UOM"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_UOM"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        PrintColumnPixel += 40;
                        if (dr.Cells["Col_Pack"].Visible == true && dr.Cells["Col_Pack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Pack"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        PrintColumnPixel += 70;
                        if (dr.Cells["Col_BatchNumber"].Visible == true && dr.Cells["Col_BatchNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_BatchNumber"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 80;
                        }
                        if (dr.Cells["Col_Expiry"].Visible == true && dr.Cells["Col_Expiry"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Expiry"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        PrintColumnPixel += 40;
                        if (dr.Cells["Col_MRP"].Visible == true && dr.Cells["Col_MRP"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_MRP"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(PrintColumnPixel + ((10.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 75;

                        }
                        if (dr.Cells["Col_Quantity"].Visible == true && dr.Cells["Col_Quantity"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                            mlen = (mamt.ToString("#0").Length);
                            colpix = Convert.ToInt32(PrintColumnPixel + ((7.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 55;
                        }

                        if (dr.Cells["Col_Amount"].Visible == true && dr.Cells["Col_Amount"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(PrintColumnPixel + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 90;
                        }

                        PrintColumnPixel += 40;
                        if (dr.Cells["Col_Shelf"].Visible == true && dr.Cells["Col_Shelf"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Shelf"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
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
                PrintColumnPixel = 0;               
                PrintColumnPixel += 150;               
                PrintColumnPixel += 40;               
                PrintColumnPixel += 70;             
                PrintColumnPixel += 80;
                PrintColumnPixel += 40;
                if (dgvReportList.Columns["Col_MRP"].Visible)
                {                   
                    PrintColumnPixel += 75;
                }
                if (dgvReportList.Columns["Col_Quantity"].Visible)
                {                    
                    PrintColumnPixel += 55;
                }

                if (dgvReportList.Columns["Col_Amount"].Visible == true)
                {                   
                    PrintColumnPixel += 90;
                }  
              

                if (PrintColumnPixel > 800)
                    PrintRowPixel = GeneralReport.PrintHeaderLarge(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2);
                else
                    PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

                PrintRowPixel += 17;



                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
                PrintColumnPixel = 1;

                row = new PrintRow("Product", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 250;
                row = new PrintRow("UOM", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 40;
                row = new PrintRow("Pack", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 70;
                //row = new PrintRow("Company", PrintRowPixel, PrintColumnPixel, PrintFont);
                //PrintBill.Rows.Add(row);
                //PrintColumnPixel += 40;
                row = new PrintRow("Batch", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 80;
                row = new PrintRow("Expiry", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 70;
                if (dgvReportList.Columns["Col_MRP"].Visible == true)
                {
                    row = new PrintRow("MRP", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 55;
                }
                if (dgvReportList.Columns["Col_Quantity"].Visible == true)
                {
                    row = new PrintRow("Quantity", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 70;
                }

                if (dgvReportList.Columns["Col_Amount"].Visible == true)
                {
                    row = new PrintRow("Amount", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 90;
                }             
             
                if (dgvReportList.Columns["Col_Shelf"].Visible == true)
                {
                    row = new PrintRow("Shelf", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);                   
                }
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
      
        #endregion Print Report

        #region Other Private Methods
        private void ConstructReportColumns()
        {
            dgvReportList.Columns.Clear();
            DataGridViewTextBoxColumn column;

            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "AccountID";
                column.HeaderText = "AccountID";
                column.Visible = false;
                column.Width = 75;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 100;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product Name";
                column.Width = 200;
                column.ToolTipText = "Press TAB Key To Exit Product Grid";
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 60;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 90;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);
                //6          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "Expiry";
                column.ToolTipText = "Expiry dd/mm";
                column.Width = 50;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 70;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "ClosingStock";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.HeaderText = "Qty";
                column.Width = 70;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);
                // 10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.HeaderText = "Amount";
                column.Width = 90;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column); 

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 85;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 50;
                column.ReadOnly = true;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
                column.Width = 80;
                column.ReadOnly = true;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.Visible = false;
                dgvReportList.Columns.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void FillReportGrid()
        {
            try
            {
                ConstructReportColumns();
                InitializeReportGrid();
                FormatReportGrid();
                FillReportData();
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
                _TotalAmount = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    _TotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                }
                txtReportTotalAmount.Text = _TotalAmount.ToString("#0.00");
                NoofRows();
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
            dgvReportList.DoubleColumnNames.Add("Col_MRP");
            dgvReportList.OptionalColumnNames.Add("Col_Amount");
            dgvReportList.OptionalColumnNames.Add("Col_AccountName");
            dgvReportList.OptionalColumnNames.Add("Col_Address");
            dgvReportList.OptionalColumnNames.Add("Col_VoucherType");
            dgvReportList.OptionalColumnNames.Add("Col_VoucherNumber");
            dgvReportList.OptionalColumnNames.Add("Col_VoucherDate");
            dgvReportList.OptionalColumnNames.Add("Col_BillNumber");
            dgvReportList.OptionalColumnNames.Add("Col_Shelf");
        }
        public void ShowpnlGO()
        {
            pnlMonthYear.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
            dgvReportList.Focus();
        }
        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }

        private void FillReportData()
        {
            string Frommonth = "";
            string Fromdate = "";
            string Tomonth = "";
            string Todate = "";
            string mshelf = "";
            string mshelfID = string.Empty;
            int.TryParse(txtFromMonth.Text, out _FromMonth);
            int.TryParse(txtFromYear.Text, out _FromYear);
            int.TryParse(txtToMonth.Text, out _ToMonth);
            int.TryParse(txtToYear.Text, out _ToYear);
            try
            {
                if (_FromMonth > 0 && _FromYear > 2000 && _ToMonth > 0 && _ToYear > 2000)
                {

                    Frommonth = "00" + Convert.ToString(_FromMonth).Trim();
                    int len = 0;
                    len = Frommonth.Length;
                    if (len == 3)
                        Frommonth = Frommonth.Substring(1, 2);
                    else
                        Frommonth = Frommonth.Substring(2, 2);
                    Fromdate = Convert.ToString(_FromYear).Trim() + Frommonth + "01";

                    Tomonth = "00" + Convert.ToString(_ToMonth).Trim();
                    int mlen = 0;
                    mlen = Tomonth.Length;
                    if (mlen == 3)
                        Tomonth = Tomonth.Substring(1, 2);
                    else
                        Tomonth = Tomonth.Substring(2, 2);
                    Todate = Convert.ToString(_ToYear).Trim() + Tomonth + "31"; // [ansuman] [01=>31]

                    if (mcbShelfCode.SelectedID != null && mcbShelfCode.SelectedID != string.Empty)
                    {
                        mshelfID = mcbShelfCode.SelectedID.ToString();

                        if (mcbShelfCode.SeletedItem.ItemData[1] != null)

                            mshelf = mcbShelfCode.SeletedItem.ItemData[1].ToString();
                    }

                    //cmonth = "00" + Convert.ToString(mmonth).Trim();
                    //int mlen = 0;
                    //mlen = cmonth.Length;
                    //if (mlen == 3)
                    //    cmonth = cmonth.Substring(1, 2);
                    //else
                    //    cmonth = cmonth.Substring(2, 2);
                    //mdate = Convert.ToString(myear).Trim() + cmonth + "31";

                    if (mshelf != string.Empty)
                        _BindingSource = _DNExpiry.ReadExpiredStockForShelf(Fromdate, Todate, mshelfID);
                    else
                        _BindingSource = _DNExpiry.ReadExpiredProductData(Fromdate, Todate);

                }
                //int.TryParse(txtMonth.Text, out mmonth);
                //int.TryParse(txtYear.Text, out myear);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void FillShelfCombo()
        {
            mcbShelfCode.SelectedID = null;
            mcbShelfCode.SourceDataString = new string[2] { "ShelfId", "ShelfCode" };
            mcbShelfCode.ColumnWidth = new string[2] { "0", "200" };
            mcbShelfCode.ValueColumnNo = 0;
            mcbShelfCode.UserControlToShow = new UclShelf();
            Shelf _Shelf = new Shelf();
            DataTable dtable = _Shelf.GetOverviewData();
            mcbShelfCode.FillData(dtable);

        }
        # endregion Other private methods

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void txtMonth_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    txtYear.Focus();
        }

        private void btnOKMultiSelection1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();

        }

        private void txtYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbShelfCode.Focus();
        }

        private void txtMonth_Validating(object sender, CancelEventArgs e)
        {
            //int m = 0;
            //if (txtMonth.Text != null && txtMonth.Text != string.Empty)
            //{
            //    m = Convert.ToInt32(txtMonth.Text.ToString());

            //}
            //if (m >= 1 && m <= 12)
            //{
            //    txtYear.Focus();
            //    btnOKMultiSelection1.Enabled = true;
            //}

            //else
            //{
            //    txtMonth.Focus();
            //    btnOKMultiSelection1.Enabled = false;
            //}
        }

        private void txtYear_Validating(object sender, CancelEventArgs e)
        {
            int y = 0;
            if (txtToYear.Text != null && txtToYear.Text != string.Empty)
                y = Convert.ToInt32(txtToYear.Text.ToString());
            if (y > 2010)
            {
                mcbShelfCode.Focus();
                btnOKMultiSelection1.Enabled = true;
            }
            else
            {
                txtToYear.Focus();
                btnOKMultiSelection1.Enabled = false;
            }

        }
        private void btnOKMultiSelectionClick()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                InitializeReportGrid();
                FormatReportGrid();
                ShowpnlGO();
                FillReportGrid();
                PrintReportHead = "Product List [All]";
                PrintReportHead2 = "";
                this.Cursor = Cursors.Default;
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion  Events

        #region ToolTip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMonthYear, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }

        #endregion  ToolTip

        private void txtFromMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtFromYear.Focus();
        }

        private void txtFromYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtToMonth.Focus();
        }

        private void txtToMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtToYear.Focus();
        }

        private void txtToYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelection1.Focus();
        }

        private void mcbShelfCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelection1.Focus();
        }
    }
}
