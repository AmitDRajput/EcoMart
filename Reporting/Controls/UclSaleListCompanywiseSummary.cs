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
    public partial class UclSaleListCompanywiseSummary : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SsStock _SsStock;
        private string _MFromDate;
        private string _MToDate;
        private int _MTotalRows;
        private double _MTotalAmount = 0;
        #endregion

        # region Constructor
        public UclSaleListCompanywiseSummary()
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
                headerLabel1.Text = "SALE - Companywise";
                ClearControls();               
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
            fromDate1.Focus();

        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;
            if (keyPressed == Keys.Home)
            {
                HidepnlGO();
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
                int totalrows = _MTotalRows;
                totalrows = CalculateTotalRows(totalrows);
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead(_MFromDate,_MToDate);
                PrintIfFirstRow = "Y";
                double mamt = 0;
                //int mcolpix = 0;
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
                        PrintHead(_MFromDate, _MToDate);
                        PrintIfFirstRow = "Y";
                    }

                    if (dr.Visible)
                    {
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        mamt = 0;

                        row = new PrintRow(dr.Cells["Col_CompanyName"].Value.ToString().PadRight(30), PrintRowPixel, 10, PrintFont);
                        PrintBill.Rows.Add(row);

                        row = new PrintRow(dr.Cells["Col_Amount"].Value.ToString().PadRight(30), PrintRowPixel, 500, PrintFont);
                        PrintBill.Rows.Add(row);

                        //if (dr.Cells["Col_ProductName"].Value != null)
                        //{
                        //    {
                        //        row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                        //        PrintBill.Rows.Add(row);
                        //    }
                        //    if (dr.Cells["Col_ProductLoosePack"].Value != null)
                        //    {
                        //        row = new PrintRow(dr.Cells["Col_ProductLoosePack"].Value.ToString().PadRight(15), PrintRowPixel, 200, PrintFont);
                        //        PrintBill.Rows.Add(row);
                        //    }
                        //    if (dr.Cells["Col_ProductPack"].Value != null)
                        //    {
                        //        row = new PrintRow(dr.Cells["Col_ProductPack"].Value.ToString().PadLeft(6), PrintRowPixel, 250, PrintFont);
                        //        PrintBill.Rows.Add(row);
                        //    }

                        //    mamt = 0;
                        //    mcolpix = 310;


                        //    if (dr.Cells["Col_salestock"].Value != null)
                        //    {
                        //        mamt = Convert.ToDouble(dr.Cells["Col_salestock"].Value.ToString());
                        //        mlen = (mamt.ToString("#0").Length);
                        //        colpix = Convert.ToInt32(mcolpix + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                        //    }
                        //    row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                        //    PrintBill.Rows.Add(row);
                        //    mcolpix += 200;


                        //    if (dr.Cells["Col_Amount"].Value != null && Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString()) != 0)
                        //    {
                        //        mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                        //        mlen = (mamt.ToString("#0.00").Length);
                        //        colpix = Convert.ToInt32(470.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        //        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        //        PrintBill.Rows.Add(row);
                        //    }

                        //}
                    }
                }
                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
                mamt = _MTotalAmount;
                mlen = (mamt.ToString("#0.00").Length);
                colpix = Convert.ToInt32(470.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
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
            return totrows;
        }

        private int PrintHead(string _MFromDate, string _MToDate)
        {
            PrintRow row;

            try
            {
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(_MFromDate), General.GetDateInDateFormat(_MToDate));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 13;

                row = new PrintRow("Company", PrintRowPixel, 10, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 500, PrintFont);
                PrintBill.Rows.Add(row);

                //row = new PrintRow("Pack", PrintRowPixel, 256, PrintFont);
                //PrintBill.Rows.Add(row);
                //int colpix = 330;

                //row = new PrintRow("Sale Stock", PrintRowPixel, colpix, PrintFont);
                //PrintBill.Rows.Add(row);

                //colpix = colpix + 165;



                //row = new PrintRow("Amount", PrintRowPixel, colpix, PrintFont);
                //PrintBill.Rows.Add(row);



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
        private int PrintFooter(int PrintRowPixel) // [23.02.2017]
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
        #endregion IReportMember

        # region Other Private Methods
        public void ClearControls()
        {
            try
            {
                InitializeReportGrid();
                InitializeDates();
                lblFooterMessage.Text = "";               
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void InitializeDates()
        {
            _MFromDate = General.ShopDetail.Shopsy;
            _MToDate = General.ShopDetail.Shopey;
            fromDate1.Value = General.ConvertStringToDateyyyyMMdd(_MFromDate);
            toDate1.Value = General.ConvertStringToDateyyyyMMdd(_MToDate);

        }
        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            FormatReportGrid();
            dgvReportList.InitializeColumnContextMenu();
        }

        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Debit");
            dgvReportList.DoubleColumnNames.Add("Col_Credit");
            dgvReportList.OptionalColumnNames.Add("Col_Debit");
            dgvReportList.OptionalColumnNames.Add("Col_Credit");
        }
        public void HidepnlGO()
        {
            pnlMultiSelection.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Visible = false;
            ViewToDate.Visible = false;
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);          
            ViewFromDate.Visible = true;
            ViewToDate.Visible = true;
        }

        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyName";
                column.HeaderText = "Company";
                column.Width = 300;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column); 

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 120;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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
                FillReportData();
                txtReportTotalAmount.Text = _MTotalAmount.ToString("#0.00");
                CheckFilter();
                NoofRows();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillReportData()
        {
            DataTable dtable = new DataTable();
            string _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
            string _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");                   
            double mamt = 0;
            _MTotalAmount = 0; 
            int _RowIndex;
             DataGridViewRow currentdr;
            try
            {             


                dtable = _SsStock.GetSaleForCompanywiseSummary(_MFromDate, _MToDate);

                if (dtable != null)
                {
                    foreach (DataRow dr in dtable.Rows)
                    {
                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["CompID"].ToString();
                        currentdr.Cells["Col_CompanyName"].Value = dr["CompName"].ToString();
                        mamt = 0;
                        if (dr["Amount"] != DBNull.Value && dr["Amount"].ToString() != string.Empty)
                            mamt = Convert.ToDouble(dr["Amount"].ToString());
                        _MTotalAmount += mamt;
                        currentdr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                    }

                }

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
                InitializeReportGrid();
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                ShowpnlGO();
                retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FillReportGrid();
                    this.Cursor = Cursors.Default;
                    PrintReportHead = "Sale  CompanyWise Summary"; // From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = " ";
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

        private void CheckFilter()
        {

            //int drsalestk = 0;
            _MTotalRows = 0;
            int i = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                   // drsalestk = 0;
                    //if (dr.Cells["Col_salestock"].Value != null)
                    //    drsalestk = Convert.ToInt32(dr.Cells["Col_salestock"].Value.ToString());

                    _MTotalRows += 1;
                    dr.Visible = true;
                    i += 1;

                    //if (cbZero.Checked == true)           // [By Ansuman] Need To Confirm
                    //{
                    //    _MTotalRows += 1;
                    //    dr.Visible = true;
                    //    i += 1;
                    //}
                    //else
                    //{
                    //    if (drsalestk == 0)
                    //    {

                    //        if (i >= 0)
                    //            dr.Visible = false;
                    //        i += 1;
                    //    }
                    //    else
                    //    {
                    //        _MTotalRows += 1;
                    //        dr.Visible = true;
                    //        i += 1;
                    //    }
                    //}

                }              
            }
            catch (Exception ex)
            {
                Log.WriteError("UclStockListStocknSale:NoofRows>> " + ex.Message);
            }
        }

        private void NoofRows()
        {
            string strmessage = General.NoofRows(_MTotalRows);
            lblFooterMessage.Text = strmessage;
        }

        #endregion Other Private Methods

        # region events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void todate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
            else if (e.KeyCode == Keys.Left)
                fromDate1.Focus();
        }
       

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                ReportControl = new UclSaleListCompanywisesale();
                ShowReportForm(selectedID, "Sale", _MFromDate, _MToDate);
                this.Cursor = Cursors.Default;
            }
        }
        #endregion Events

      
    }
}
