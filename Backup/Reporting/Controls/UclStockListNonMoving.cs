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
using PharmaSYSRetailPlus.Printing;
using PrintDataGrid;


namespace PharmaSYSRetailPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclStockListNonMoving : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private SsStock _SsStock;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        # endregion

        # region Constructor
        public UclStockListNonMoving()
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
        # endregion

        #region IOverview Members

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _SsStock = new SsStock();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "STOCK-NON MOVING PRODUCTS";
                pnlMultiSelection1.Visible = true;         
                txtDays.Focus();
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
                pnlMultiSelection1.Visible = true;
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
            txtDays.Focus();
        }
        #endregion IOverview

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
                PrintReportHead = "Stock [NonMoving]  Not Sold For Last  " + txtDays.Text.ToString() + "  Days";
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                totalrows = CalculateTotalRows(totalrows);
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                double mamt = 0;

                PrintIfFirstRow = "Y";
                foreach (DataGridViewRow dr in dgvReportList.Rows)
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
                   
                    if (dr.Cells["Col_ProductName"].Value != null)
                    {

                        if (dr.Cells["Col_ProductName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ProductLoosePack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductLoosePack"].Value.ToString().PadRight(15), PrintRowPixel, 250, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ProductPack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductPack"].Value.ToString().PadLeft(6), PrintRowPixel, 300, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_CompanyShortName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_CompanyShortName"].Value.ToString().PadLeft(6), PrintRowPixel, 360, PrintFont);
                            PrintBill.Rows.Add(row);
                        }                      
                        mamt = 0;
                        if (dr.Cells["Col_MRP"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_MRP"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(430.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }


                        mamt = 0;
                        if (dr.Cells["Col_ClosingStock"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_ClosingStock"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(520.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        mamt = 0;
                        if (dr.Cells["Col_Amount"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(600.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                    }
                }
                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                mamt = _MTotalAmount;
                mlen = (mamt.ToString("#0.00").Length);
                colpix = Convert.ToInt32(600.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                PrintRowPixel += 13;
                row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public int CalculateTotalRows(int totrows)
        {
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

                PrintRowPixel += 13;

                row = new PrintRow("Product", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("UOM", PrintRowPixel, 250, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pack", PrintRowPixel, 310, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Comp", PrintRowPixel, 360, PrintFont);
                PrintBill.Rows.Add(row);              
                row = new PrintRow("MRP", PrintRowPixel, 470, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Cl Stock", PrintRowPixel, 540, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Value", PrintRowPixel, 640, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            PrintRowCount = 7;
            return PrintRowCount;
        }

        #endregion IReportMember

        #region Other Private methods
        private void ConstructProductColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductId";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product";
                column.Width = 250;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductLoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Company";
                column.Width = 80;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 100;               
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "CL Stock";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 93;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Name";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 175;
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
                ConstructProductColumns();
                FillReportData();
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.DoubleColumnNames.Add("Col_MRP");
                dgvReportList.DoubleColumnNames.Add("Col_Amount");
                dgvReportList.Bind();               
                double mamt = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    mamt = 0;
                    if (dr.Cells["Col_Amount"].Value != null)
                    {
                        mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        _MTotalAmount += mamt;
                    }
                }
                txtReportTotalAmount.Text = _MTotalAmount.ToString("#0.00");
                NoofRows();
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
        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                double noofday = 0;
                if (txtDays.Text != null && txtDays.Text.ToString().Trim() != "")
                    double.TryParse(txtDays.Text.ToString(), out noofday);
                if (noofday > 0)
                {
                    _SsStock.Today = DateTime.Now.Date.AddDays(noofday * -1);
                    _SsStock.MDate = _SsStock.Today.Date.ToString("yyyyMMdd");
                    dtable = _SsStock.GetOverviewDataNonMoving(_SsStock.MDate);
                }

                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion  OtherPrivate Methods      

        #region Events
        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {

            btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelectionClick()
        {
            try
            {
              
                pnlMultiSelection1.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                FillReportGrid();
                this.Cursor = Cursors.Default;
                dgvReportList.Focus();
            
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }      

        private void txtDays_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    btnOKMultiSelectionClick();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {
             //   ttStockNonMoving.SetToolTip(btnOKMultiSelection, "Click to See Report = F10");
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
