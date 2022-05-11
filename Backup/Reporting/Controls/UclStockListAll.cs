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
    public partial class UclStockListAll : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private Product _Product;
        private string _MFromDate;
        private string _MToDate;
        private double _MOPStkMRP = 0;
        private double _MOPStkPUR = 0;
        private double _MCLStkMRP = 0;
        private double _MCLStkPUR = 0;
        private double _MOPStkMRPPageTotal = 0;
        private double _MOPStkPURPageTotal = 0;
        private double _MCLStkMRPPageTotal = 0;
        private double _MCLStkPURPageTotal = 0;
        # endregion

        # region Constructor
        public UclStockListAll()
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
                _Product = new Product();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "STOCK-PRODUCT LIST";
                rbtAlphabetical.Checked = true;
                rbtNone.Checked = true;
                AddToolTip();
                pnlMultiSelection.Visible = true;
                lblOPStockValue.Visible = false;
                txtReportTotalAmountOP.Visible = false;
                lblCLStockValue.Visible = false;
                txtReportTotalAmountCL.Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


        public override void SetFocus()
        {
            base.SetFocus();
            rbtAlphabetical.Focus();
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
        #endregion IOverview Methods

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
        //public override bool PrintReport()
        //{
        //    bool retValue = true;
        //    PrintData();
        //    return retValue;
        //}
        private void PrintData()
        {
            PrintRow row;
            try
            {
                PrintReportHead = "Product List [All] ";
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
                PrintIfFirstRow = "Y";
                double mamt = 0;
                int mcolpix = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    if (PrintRowCount >= FixAccounts.NumberOfRowsPerReport)
                    {
                        if (dgvReportList.Columns["Col_OPValueMRP"].Visible || dgvReportList.Columns["Col_OPValuePUR"].Visible || dgvReportList.Columns["Col_CLValueMRP"].Visible || dgvReportList.Columns["Col_CLValuePUR"].Visible)
                            PrintPageTotal();
                        PrintRowPixel += 34;
                        row = new PrintRow("Continued....", PrintRowPixel, 15, PrintFont);
                        PrintBill.Rows.Add(row);
                        PrintBill.Print_Bill();
                        PrintBill.Rows.Clear();
                        PrintRowPixel = 0;
                        PrintPageNumber += 1;
                        _MCLStkMRPPageTotal = 0;
                        _MCLStkPURPageTotal = 0;
                        _MOPStkMRPPageTotal = 0;
                        _MOPStkPURPageTotal = 0;
                        PrintHead();
                        PrintIfFirstRow = "Y";
                    }
                    PrintRowPixel += 17;
                    PrintRowCount += 1;
                    mamt = 0;

                    if (dr.Cells["Col_ProductName"].Value != null)
                    {                       
                        if (dr.Cells["Col_ProductName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ProductLoosePack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductLoosePack"].Value.ToString().PadRight(15), PrintRowPixel, 200, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ProductPack"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_ProductPack"].Value.ToString().PadLeft(6), PrintRowPixel, 250, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_CompanyShortName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_CompanyShortName"].Value.ToString().PadLeft(6), PrintRowPixel, 300, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        mamt = 0;
                        mcolpix = 350;


                        if (rbtMRP.Checked)
                        {
                            if (dr.Cells["Col_MRP"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_MRP"].Value.ToString());
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                                mcolpix += 80;
                            }
                        }
                        if (rbtPurhcaseRate.Checked)
                        {
                            if (dr.Cells["Col_PurchaseRate"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_PurchaseRate"].Value.ToString());                                
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                                mcolpix += 80;
                            }
                        }
                        if (dr.Cells["Col_OpeningStock"].Visible)
                        {
                            if (dr.Cells["Col_OpeningStock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_OpeningStock"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(mcolpix + ((10.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            mcolpix += 80;
                        }
                        if (rbtMRP.Checked)
                        {
                            if (dr.Cells["Col_OPValueMRP"].Visible)
                            {
                                if (dr.Cells["Col_OPValueMRP"].Value != null)
                                {
                                    mamt = Convert.ToDouble(dr.Cells["Col_OPValueMRP"].Value.ToString());
                                    _MOPStkMRPPageTotal += mamt;
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                                }
                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                                mcolpix += 80;
                            }
                        }
                        if (rbtPurhcaseRate.Checked)
                        {
                            if (dr.Cells["Col_OPValuePUR"].Visible)
                            {
                                if (dr.Cells["Col_OPValuePUR"].Value != null)
                                {
                                    mamt = Convert.ToDouble(dr.Cells["Col_OPValuePUR"].Value.ToString());
                                    _MOPStkPURPageTotal += mamt;
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                                }
                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                                mcolpix += 80;
                            }
                        }
                        if (dr.Cells["Col_ClosingStock"].Visible)
                        {
                            if (dr.Cells["Col_ClosingStock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_ClosingStock"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(mcolpix + ((10.00 - Convert.ToDouble(mlen)) * 5.5));                            
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            mcolpix += 80;
                        }

                        if (rbtMRP.Checked)
                        {
                            if (dr.Cells["Col_CLValueMRP"].Visible)
                            {
                                if (dr.Cells["Col_CLValueMRP"].Value != null)
                                {
                                    mamt = Convert.ToDouble(dr.Cells["Col_CLValueMRP"].Value.ToString());
                                    _MCLStkMRPPageTotal += mamt;
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                                }
                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                                mcolpix += 80;
                            }
                        }
                        if (rbtPurhcaseRate.Checked)
                        {
                            if (dr.Cells["Col_CLValuePUR"].Visible)
                            {
                                if (dr.Cells["Col_CLValuePUR"].Value != null)
                                {
                                    mamt = Convert.ToDouble(dr.Cells["Col_CLValuePUR"].Value.ToString());
                                    _MCLStkPURPageTotal += mamt;
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                                }
                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                                mcolpix += 80;
                            }
                        }
                    }
                }
                if (dgvReportList.Columns["Col_OPValueMRP"].Visible || dgvReportList.Columns["Col_OPValuePUR"].Visible || dgvReportList.Columns["Col_CLValueMRP"].Visible || dgvReportList.Columns["Col_CLValuePUR"].Visible)
                {
                    PrintPageTotal();
                    PrintFinalTotal();
                }
                else
                {
                    PrintRowPixel += 17;
                    row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                    PrintBill.Rows.Add(row);
                }
                PrintBill.Print_Bill();
                //}
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void PrintPageTotal()
        {
            int   mcolpix = 350;
            double mamt = 0;
            int mlen = 0;
            int colpix = 0;
            PrintRow row = new PrintRow("", 0, 0, PrintFont);
            PrintRowPixel += 17;
            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
            PrintBill.Rows.Add(row);

            PrintRowPixel += 13;
            row = new PrintRow("Page Total", PrintRowPixel, 200, PrintFont);
            PrintBill.Rows.Add(row);
            if (rbtMRP.Checked)
            {
                if (dgvReportList.Columns["Col_MRP"].Visible)
                {
                    mcolpix += 80;
                }
            }
            if (rbtPurhcaseRate.Checked)
            {
                if (dgvReportList.Columns["Col_PurchaseRate"].Visible)
                {
                    mcolpix += 80;
                }
            }
            if (dgvReportList.Columns["Col_OpeningStock"].Visible)
            {
                if (dgvReportList.Columns["Col_OpeningStock"].Visible)
                {
                    mcolpix += 80;
                }
            }
            if (rbtMRP.Checked)
            {
                if (dgvReportList.Columns["Col_OPValueMRP"].Visible)
                {
                    mamt = _MOPStkMRPPageTotal;
                    mlen = (mamt.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));

                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    mcolpix += 80;
                }
            }
            if (rbtPurhcaseRate.Checked)
            {
                if (dgvReportList.Columns["Col_OPValuePUR"].Visible)
                {

                    mamt = _MOPStkPURPageTotal;
                    mlen = (mamt.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));

                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    mcolpix += 80;
                }
            }
            if (dgvReportList.Columns["Col_ClosingStock"].Visible)
            {
                mcolpix += 80;
            }

            if (rbtMRP.Checked)
            {
                if (dgvReportList.Columns["Col_CLValueMRP"].Visible)
                {

                    mamt = _MCLStkMRPPageTotal;
                    mlen = (mamt.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    mcolpix += 80;
                }
            }
            if (rbtPurhcaseRate.Checked)
            {
                if (dgvReportList.Columns["Col_CLValuePUR"].Visible)
                {

                    mamt = _MCLStkPURPageTotal;
                    mlen = (mamt.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    mcolpix += 80;
                }
            }
            PrintBill.Rows.Add(row);
            PrintRowPixel += 13;
            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
            PrintBill.Rows.Add(row);

        }

        private void PrintFinalTotal()
        {
            int mcolpix = 350;
            double mamt = 0;
            int mlen = 0;
            int colpix = 0;
            PrintRow row = new PrintRow("", 0, 0, PrintFont);
            PrintRowPixel += 17;
            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
            PrintBill.Rows.Add(row);

            PrintRowPixel += 13;
            row = new PrintRow("Total", PrintRowPixel, 200, PrintFont);
            PrintBill.Rows.Add(row);
            if (rbtMRP.Checked)
            {
                if (dgvReportList.Columns["Col_MRP"].Visible)
                {
                    mcolpix += 80;
                }
            }
            if (rbtPurhcaseRate.Checked)
            {
                if (dgvReportList.Columns["Col_PurchaseRate"].Visible)
                {
                    mcolpix += 80;
                }
            }
            if (dgvReportList.Columns["Col_OpeningStock"].Visible)
            {
                if (dgvReportList.Columns["Col_OpeningStock"].Visible)
                {
                    mcolpix += 80;
                }
            }
            if (rbtMRP.Checked)
            {
                if (dgvReportList.Columns["Col_OPValueMRP"].Visible)
                {
                    mamt = _MOPStkMRP;
                    mlen = (mamt.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));

                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    mcolpix += 80;
                }
            }
            if (rbtPurhcaseRate.Checked)
            {
                if (dgvReportList.Columns["Col_OPValuePUR"].Visible)
                {

                    mamt = _MOPStkPUR;
                    mlen = (mamt.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));

                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    mcolpix += 80;
                }
            }
            if (dgvReportList.Columns["Col_ClosingStock"].Visible)
            {
                mcolpix += 80;
            }

            if (rbtMRP.Checked)
            {
                if (dgvReportList.Columns["Col_CLValueMRP"].Visible)
                {

                    mamt = _MCLStkMRP;
                    mlen = (mamt.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    mcolpix += 80;
                }
            }
            if (rbtPurhcaseRate.Checked)
            {
                if (dgvReportList.Columns["Col_CLValuePUR"].Visible)
                {

                    mamt = _MCLStkPUR;
                    mlen = (mamt.ToString("#0.00").Length);
                    colpix = Convert.ToInt32(mcolpix + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    mcolpix += 80;
                }
            }
            PrintBill.Rows.Add(row);
            PrintRowPixel += 13;
            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
            PrintBill.Rows.Add(row);

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
               PrintRowPixel =  GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber,PrintReportHead2);

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow("Product", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("UOM", PrintRowPixel, 200, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pack", PrintRowPixel, 250, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Comp", PrintRowPixel, 300, PrintFont);
                PrintBill.Rows.Add(row);
                int colpix = 390;
                if (rbtMRP.Checked)
                {
                    row = new PrintRow("MRP", PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    colpix = colpix + 70;
                }
                if (rbtPurhcaseRate.Checked)
                {
                    row = new PrintRow("PRate", PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    colpix = colpix + 70;
                }
                if (dgvReportList.Columns["Col_OpeningStock"].Visible)
                {
                    row = new PrintRow("OP Stock", PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    colpix = colpix + 70;
                }
                if (dgvReportList.Columns["Col_OPValueMRP"].Visible || dgvReportList.Columns["Col_OPValuePUR"].Visible)
                {
                    row = new PrintRow("OPStk Value", PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    colpix = colpix + 70;
                }
                if (dgvReportList.Columns["Col_ClosingStock"].Visible)
                {
                    row = new PrintRow(" Cl Stock", PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    colpix = colpix + 70;
                }
                if (dgvReportList.Columns["Col_CLValueMRP"].Visible || dgvReportList.Columns["Col_CLValuePUR"].Visible)
                {
                    row = new PrintRow("CLStk Value", PrintRowPixel, colpix, PrintFont);
                    PrintBill.Rows.Add(row);
                    colpix = colpix + 70;
                }

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

        #region Other Private Methods
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
                column.Width = 200;
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
                column.HeaderText = "Comp";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "ProdLastPurchaseMRP";
                column.HeaderText = "MRP";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 90;

                if (rbtMRP.Checked == true)
                    column.Visible = true;
                else
                    column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "ProdLastPurchaseRate";
                column.HeaderText = "PUR RATE";
                column.Width = 90;
                if (rbtPurhcaseRate.Checked == true)
                    column.Visible = true;
                else
                    column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OpeningStock";
                column.DataPropertyName = "ProdOpeningStock";
                column.HeaderText = "OPStk";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OPValueMRP";
                column.DataPropertyName = "OPValueByMRP";
                column.HeaderText = "OPSTK Value";
                column.Width = 90;
                if (rbtMRP.Checked == true)
                    column.Visible = true;
                else
                    column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_OPValuePUR";
                column.DataPropertyName = "OPValueByPurchaseRate";
                column.HeaderText = "OPSTK Value";
                column.Width = 90;
                if (rbtPurhcaseRate.Checked == true)
                    column.Visible = true;
                else
                    column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "CL Stock";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 80;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CLValueMRP";
                column.DataPropertyName = "CLValueByMRP";
                column.HeaderText = "CLSTK Value";
                column.Width = 90;
                if (rbtMRP.Checked == true)
                    column.Visible = true;
                else
                    column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CLValuePUR";
                column.DataPropertyName = "CLValueByPurchaseRate";
                column.HeaderText = "CLSTK Value";
                column.Width = 90;
                if (rbtPurhcaseRate.Checked == true)
                    column.Visible = true;
                else
                    column.Visible = false;
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
                ConstructProductColumns();
                dgvReportList.Columns["Col_ID"].Visible = false;

                FillReportData();

                dgvReportList.DataSource = _BindingSource;
                dgvReportList.DoubleColumnNames.Add("Col_MRP");
                dgvReportList.DoubleColumnNames.Add("Col_PurchaseRate");
                dgvReportList.DoubleColumnNames.Add("Col_OPValueMRP");
                dgvReportList.DoubleColumnNames.Add("Col_OPValuePUR");
                dgvReportList.DoubleColumnNames.Add("Col_CLValueMRP");
                dgvReportList.DoubleColumnNames.Add("Col_CLValuePUR");
                dgvReportList.Bind();
                CheckSort();
                if (rbtMRP.Checked || rbtPurhcaseRate.Checked)
                {
                    if (dgvReportList.Columns["Col_OPValueMRP"].Visible || dgvReportList.Columns["Col_OPValuePUR"].Visible)
                    {
                        if (dgvReportList.Columns["Col_OPValueMRP"].Visible)
                        {
                            lblOPStockValue.Visible = true;
                            txtReportTotalAmountOP.Visible = true;
                            txtReportTotalAmountOP.Text = _MOPStkMRP.ToString("#0.00");
                        }
                        if (dgvReportList.Columns["Col_OPValuePUR"].Visible)
                        {
                            lblOPStockValue.Visible = true;
                            txtReportTotalAmountOP.Visible = true;
                            txtReportTotalAmountOP.Text = _MOPStkPUR.ToString("#0.00");
                        }
                    }
                    else
                    {
                        lblOPStockValue.Visible = false;
                        txtReportTotalAmountOP.Visible = false;
                    }
                    if (dgvReportList.Columns["Col_CLValueMRP"].Visible || dgvReportList.Columns["Col_CLValuePUR"].Visible)
                    {
                        if (dgvReportList.Columns["Col_CLValueMRP"].Visible)
                        {
                            lblCLStockValue.Visible = true;
                            txtReportTotalAmountCL.Visible = true;
                            txtReportTotalAmountCL.Text = _MCLStkMRP.ToString("#0.00");
                        }
                        if (dgvReportList.Columns["Col_CLValuePUR"].Visible)
                        {
                            lblCLStockValue.Visible = true;
                            txtReportTotalAmountCL.Visible = true;
                            txtReportTotalAmountCL.Text = _MCLStkPUR.ToString("#0.00");
                        }
                    }
                    else
                    {
                        lblCLStockValue.Visible = false;
                        txtReportTotalAmountCL.Visible = false;
                    }
                }
                else
                {
                    lblOPStockValue.Visible = false;
                    txtReportTotalAmountOP.Visible = false;
                    lblCLStockValue.Visible = false;
                    txtReportTotalAmountCL.Visible = false;
                }

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
        private void CheckSort()
        {
            try
            {
                if (rbtCompanyWise.Checked == true)
                    _BindingSource.DefaultView.Sort = "CompName";
                else
                    _BindingSource.DefaultView.Sort = "ProdName";

                _MOPStkMRP = 0;
                _MOPStkPUR = 0;
                _MCLStkMRP = 0;
                _MCLStkPUR = 0;
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    if (dr.Cells["Col_OPValueMRP"].Value != null)
                        _MOPStkMRP += Convert.ToDouble(dr.Cells["Col_OPValueMRP"].Value.ToString());
                    if (dr.Cells["Col_OPValuePUR"].Value != null)
                        _MOPStkPUR += Convert.ToDouble(dr.Cells["Col_OPValuePUR"].Value.ToString());
                    if (dr.Cells["Col_CLValueMRP"].Value != null)
                        _MCLStkMRP += Convert.ToDouble(dr.Cells["Col_CLValueMRP"].Value.ToString());
                    if (dr.Cells["Col_CLValuePUR"].Value != null)
                        _MCLStkPUR += Convert.ToDouble(dr.Cells["Col_CLValuePUR"].Value.ToString());
                }

                NoofRows();
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
                dtable = _Product.GetOverviewDataWithOutZeroAllProducts();
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void btnOKMultiSelectionClick()
        {
            this.Cursor = Cursors.WaitCursor;
            pnlMultiSelection.Visible = false;
            tsbtnPrint.Enabled = true;
            FillReportGrid();
            this.Cursor = Cursors.Default;
            dgvReportList.Focus();
        }

        #endregion Other Private Methods

        #region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        # endregion Events

        #region AddToolTip
        private void AddToolTip()
        {
            try
            {
                //ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                //ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion AddToolTip
    }
}
