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
    public partial class UclStockListCategorySummary : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SsStock _SsStock;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;      

        #endregion

        # region Constructor
        public UclStockListCategorySummary()
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
                _SsStock = new SsStock();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "STOCK-CATEGORYWISE STOCK";
                FillReportGrid();
                dgvReportList.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
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
                PrintReportHead = "Stock [Category] ";
                PrintReportHead2 = "";
                PrintBill.Rows.Clear();
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
                int totalrows = dgvReportList.Rows.Count;
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    double mamt = 0;

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
                    PrintRowPixel += 17;
                    PrintRowCount += 1;
                    if (dr.Cells["Col_ProdName"].Value != null)
                    {
                        row = new PrintRow(dr.Cells["Col_ProdName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
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
                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
                mlen = (_MTotalAmount.ToString("#0.00").Length);
                colpix = Convert.ToInt32(550.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(_MTotalAmount.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 13;
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

                PrintRowPixel += 13;

                row = new PrintRow("Category", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Stock (Qty * MRP)", PrintRowPixel, 550, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

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

        public void ClearControls()
        {
            try
            {
               
                lblFooterMessage.Text = "";
                txtReportTotalAmount.Text = "";           
                if (dgvReportList.Rows.Count > 0)
                    dgvReportList.Rows.Clear();
                txtReportTotalAmount.Text = "";

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
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductCategoryID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdName";
                column.DataPropertyName = "ProductCategoryName";
                column.HeaderText = "Product Category";
                column.Width = 545;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 400;
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
                ConstructReportColumns();
                FillReportData();
                dgvReportList.DataSource = _BindingSource;
                dgvReportList.Bind();
                int noofrecords = dgvReportList.Rows.Count;
                _MTotalAmount = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());                  

                }
                txtReportTotalAmount.Text = _MTotalAmount.ToString("#0.00");
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
        //Fill Overview Details
        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _SsStock.GetProductCategorywise();
                _BindingSource = dtable;
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
                FillReportGrid();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion
    }
}
