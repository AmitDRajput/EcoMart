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
    public partial class UclListProductAll : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private Product _Product;

        # endregion Declaration

        # region Constructor

        public UclListProductAll()
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
        # endregion Constructor

        #region IOverview Members

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _Product = new Product();
                headerLabel1.Text = "LIST-PRODUCT LIST ALL";
                FillReportGrid();
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
                PrintReportHead = "Product List [All]";
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
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

                        if (dr.Cells["Col_Name"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_Name"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);                           
                        }
                        PrintColumnPixel += 150;
                        if (dr.Cells["Col_ProductLoosePack"].Visible == true && dr.Cells["Col_ProductLoosePack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductLoosePack"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);                            
                        }
                        PrintColumnPixel += 50;
                        if (dr.Cells["Col_ProductPack"].Visible == true && dr.Cells["Col_ProductPack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductPack"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        PrintColumnPixel += 70;
                        if (dr.Cells["Col_CompanyName"].Visible == true && dr.Cells["Col_CompanyName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_CompanyName"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
                            PrintBill.Rows.Add(row);
                        }                       
                        PrintColumnPixel += 200;
                        if (dr.Cells["Col_MRP"].Visible == true && dr.Cells["Col_MRP"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_MRP"].Value.ToString());
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(PrintColumnPixel + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                                PrintColumnPixel += 75;
                           
                        }
                        if (dr.Cells["Col_PurchaseRate"].Visible == true && dr.Cells["Col_PurchaseRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_PurchaseRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(PrintColumnPixel + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 75;
                        }
                        if (dr.Cells["Col_SaleRate"].Visible == true && dr.Cells["Col_SaleRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(PrintColumnPixel + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 75;
                        }
                        if (dr.Cells["Col_VATPercent"].Visible == true && dr.Cells["Col_VATPercent"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_VATPercent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(PrintColumnPixel + ((6.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintColumnPixel += 40;
                        }
                        if (dr.Cells["Col_ShelfCode"].Visible == true && dr.Cells["Col_ShelfCode"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ShelfCode"].Value.ToString(), PrintRowPixel, PrintColumnPixel, PrintFont);
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2);

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
                PrintColumnPixel = 1;

                row = new PrintRow("Product", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 150;
                row = new PrintRow("UOM", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 50;
                row = new PrintRow("Pack", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 70;
                row = new PrintRow("Company", PrintRowPixel, PrintColumnPixel, PrintFont);
                PrintBill.Rows.Add(row);
                PrintColumnPixel += 240;

                if (dgvReportList.Columns["Col_MRP"].Visible == true)
                {
                    row = new PrintRow("MRP", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 70;
                }
                if (dgvReportList.Columns["Col_PurchaseRate"].Visible == true)
                {
                    row = new PrintRow("Pur.Rate", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 60;
                }
                if (dgvReportList.Columns["Col_SaleRate"].Visible == true)
                {
                    row = new PrintRow("Sale Rate", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 55;
                }
                if (dgvReportList.Columns["Col_VATPercent"].Visible == true)
                {
                    row = new PrintRow("VAT%", PrintRowPixel, PrintColumnPixel, PrintFont);
                    PrintBill.Rows.Add(row);
                    PrintColumnPixel += 40;
                }
                if (dgvReportList.Columns["Col_ShelfCode"].Visible == true)
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

        #region Other private methods

        private void ConstructReportColumns()
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
                column.Name = "Col_Name";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product";
                column.Width = 200;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductLoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyName";
                column.DataPropertyName = "CompName";
                column.HeaderText = "Company";
                column.Width = 250;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "ProdLastPurchaseMRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "ProdLastPurchaseRate";
                column.HeaderText = "PUR.RATE";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "ProdLastPurchaseSaleRate";
                column.HeaderText = "SALE.RATE";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPercent";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ShelfCode";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
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
                ConstructReportColumns();
                InitializeReportGrid();
                FormatReportGrid();
                FillReportData();
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
                //CheckSort();
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
            dgvReportList.DoubleColumnNames.Add("Col_MRP");
            dgvReportList.DoubleColumnNames.Add("Col_PurchaseRate");
            dgvReportList.DoubleColumnNames.Add("Col_SaleRate");
            dgvReportList.DoubleColumnNames.Add("Col_VATPercent");
            dgvReportList.OptionalColumnNames.Add("Col_MRP");
            dgvReportList.OptionalColumnNames.Add("Col_PurchaseRate");
            dgvReportList.OptionalColumnNames.Add("Col_SaleRate");
            dgvReportList.OptionalColumnNames.Add("Col_VATPercent");
            dgvReportList.OptionalColumnNames.Add("Col_ShelfCode");
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
                dtable = General.ProductList;
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        # endregion Other private methods

    }
}
