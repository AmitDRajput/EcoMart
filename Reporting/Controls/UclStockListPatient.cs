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
    public partial class UclStockListPatient : ReportBaseControl
    {
        # region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceDebtor;
        private SsStock _SsStock;
        private double _MTotalAmount;
        private string _MFromDate;
        private string _MToDate;
        private string _Mdaystring;
        # endregion

        # region Constructor
        public UclStockListPatient()
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
                _BindingSourceDebtor = new DataTable();
                _SsStock = new SsStock();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                _Mdaystring = "";
                headerLabel1.Text = "STOCK-PATIENT SHORT LIST";
                HidepnlGO();
                txtDays.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void SetFocus()
        {
            txtDays.Focus();
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
                        if (dr.Cells["Col_MRP"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_MRP"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(350.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }


                        mamt = 0;
                        if (dr.Cells["Col_ClosingStock"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_ClosingStock"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(430.00 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        mamt = 0;
                        if (dr.Cells["Col_RequStock"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_RequStock"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(480.00 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Difference"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Difference"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(520.00 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AccName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadLeft(6), PrintRowPixel, 580, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                    }
                }
                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                //mamt = _MTotalAmount;
                //mlen = (mamt.ToString("#0.00").Length);
                //colpix = Convert.ToInt32(600.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                //PrintRowPixel += 13;
                //row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                //PrintBill.Rows.Add(row);
                //PrintRowPixel += 13;
                //row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
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
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

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
                row = new PrintRow("MRP", PrintRowPixel, 390, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Cl Stock", PrintRowPixel, 430, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Required", PrintRowPixel, 480, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Diff", PrintRowPixel, 530, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 580, PrintFont);
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

        #endregion IReportMember


        #region Other Private methods
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
          //  FormatReportGrid();
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlMultiSelection.Visible = true;
            txtReportTotalAmount.Text = "";
            tsbtnPrint.Enabled = false;           
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
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
             //   column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
              //  column.DataPropertyName = "ProdName";
                column.HeaderText = "Product";
                column.Width = 200;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
             //   column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
             //   column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyShortName";
            //    column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Company";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
              //column.DataPropertyName = "ProdLastPurchaseMRP";
                column.HeaderText = "MRP";
                column.Width = 100;
                column.Visible = false;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
             //   column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "CL Stock";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RequStock";
             //   column.DataPropertyName = "RequiredQty";
                column.HeaderText = "Required Stk";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Difference";
             //   column.DataPropertyName = "Difference";
                column.HeaderText = "Difference";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
            //    column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 150;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                //    column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.Visible = false;
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
                InitializeReportGrid();
                if (dgvReportList.Rows.Count > 0)
                    dgvReportList.Rows.Clear();
                FillReportData();
              //  CheckFilter();
                BindReportGrid();
            //    dgvReportList.DataSource = _BindingSource;
              //  dgvReportList.Bind();
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
        private void BindReportGrid()
        {
            try
            {
                int _RowIndex;
                DataGridViewRow currentdr;

                //double mtotamount = 0;
                //int mtotquantity = 0; 
                int mdifference = 0;

                if (_BindingSource != null)
                {
                    foreach (DataRow dr in _BindingSource.Rows)
                    {
                        mdifference = 0;
                        if (dr["Difference"] != DBNull.Value && dr["Difference"].ToString() != string.Empty)
                            mdifference = Convert.ToInt32(dr["Difference"].ToString());

                        if (cbOnlyLessStock.Checked == false || (cbOnlyLessStock.Checked == true && mdifference < 0))
                        {
                            _RowIndex = dgvReportList.Rows.Add();
                            currentdr = dgvReportList.Rows[_RowIndex];
                            if (dr["ProductID"] != DBNull.Value && dr["ProductID"].ToString() != string.Empty)
                            {
                                currentdr.Cells["Col_ID"].Value = dr["ProductID"].ToString();
                                if (dr["ProdName"] != DBNull.Value && dr["ProdName"].ToString() != string.Empty)
                                    currentdr.Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                                if (dr["ProdLoosePack"] != DBNull.Value && dr["ProdLoosePack"].ToString() != string.Empty)
                                    currentdr.Cells["Col_UOM"].Value = dr["ProdLoosePack"].ToString();
                                if (dr["ProdPack"] != DBNull.Value && dr["ProdPack"].ToString() != string.Empty)
                                    currentdr.Cells["Col_Pack"].Value = dr["ProdPack"].ToString();
                                if (dr["ProdCompShortName"] != DBNull.Value && dr["ProdCompShortName"].ToString() != string.Empty)
                                    currentdr.Cells["Col_CompanyShortName"].Value = dr["ProdCompShortName"].ToString();
                                //  currentdr.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();
                                if (dr["ProdLastPurchaseMRP"] != DBNull.Value && dr["ProdLastPurchaseMRP"].ToString() != string.Empty)
                                    currentdr.Cells["Col_MRP"].Value = dr["ProdLastPurchaseMRP"].ToString();
                                if (dr["ProdClosingStock"] != DBNull.Value && dr["ProdClosingStock"].ToString() != string.Empty && dr["ProdClosingStock"].ToString() != "0")
                                    currentdr.Cells["Col_ClosingStock"].Value = dr["ProdClosingStock"].ToString();
                                if (dr["RequiredQty"] != DBNull.Value && dr["RequiredQty"].ToString() != string.Empty)
                                    currentdr.Cells["Col_RequStock"].Value = dr["RequiredQty"].ToString();
                                if (dr["Difference"] != DBNull.Value && dr["Difference"].ToString() != string.Empty)
                                    currentdr.Cells["Col_Difference"].Value = dr["Difference"].ToString();
                                if (dr["AccName"] != DBNull.Value && dr["AccName"].ToString() != string.Empty)
                                    currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                                if (dr["Amount"] != DBNull.Value && dr["Amount"].ToString() != string.Empty)
                                    currentdr.Cells["Col_Amount"].Value = dr["Amount"].ToString();
                            }
                        }
                    }
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
       
        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
             //   dtable = _SsStock.GetOverviewPatientShortList(txtDays.Text.ToString().Trim());
                _BindingSource = dtable;
                //dtable = _SsStock.GetOverviewDebtorShortList(txtDays.Text.ToString().Trim());
                //_BindingSourceDebtor = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        //private void CheckFilter()
        //{
        //    try
        //    {
        //        if (cbOnlyLessStock.Checked == true)
        //        {
        //            _BindingSource.DefaultView.RowFilter = " Difference < " + 0;
        //            _BindingSourceDebtor.DefaultView.RowFilter = " Difference < " + 0;
        //        }
        //        else
        //        {
        //            _BindingSource.DefaultView.RowFilter = "";
        //            _BindingSourceDebtor.DefaultView.RowFilter = "";
        //        }
        //        NoofRows();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }           
        //}

        #endregion OtherPrivate Methods       

      
        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelectionClick()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ShowpnlGO();
                FillReportGrid();
                this.Cursor = Cursors.Default;
                int mday = Convert.ToInt32(txtDays.Text.ToString());
                _Mdaystring = "";
                if (mday == 1)
                    _Mdaystring = mday.ToString() + "st";
                else if (mday == 2)
                    _Mdaystring = mday.ToString() + "nd";
                else if (mday == 3)
                    _Mdaystring = mday.ToString() + "rd";
                else
                    _Mdaystring = mday.ToString() + "th";
                PrintReportHead = "Stock [Patient Short List] Visiting on  " + _Mdaystring + "  Day of the Month";
                PrintReportHead2 = "";
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
                if (e.KeyCode == Keys.Enter && txtDays.Text.ToString().Trim() != "0" && txtDays.Text.ToString().Trim() != "")
                    btnOKMultiSelectionClick();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        #endregion

    }
}
