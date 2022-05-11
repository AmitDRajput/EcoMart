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
    public partial class UclSaleListCompanywisesale : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SsStock _SsStock;
        private string _MFromDate;
        private string _MToDate;
        private int _MTotalRows;
        private double _MTotalAmount = 0;
        private string _ReportOption = string.Empty;
        #endregion

        # region Constructor
        public UclSaleListCompanywisesale()
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
                FillCompanyCombo();
                HidepnlGO();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void ShowReport(string ID, string blank, string FromDate, string ToDate)
        {
            base.ShowReport(ID, blank, FromDate, ToDate);
            _ReportOption = blank;
            if (ID != null && ID != "")
            {
                _MFromDate = FromDate;
                _MToDate = ToDate;
                mcbCompany.SelectedID = ID;
                ShowReportGrid();

            }
        }
        private void ShowReportGrid()
        {
            InitializeReportGrid();
            ShowpnlGO();
            FillReportGrid();
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
                int mcolpix = 0;
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
                        PrintHead(_MFromDate,_MToDate);
                        PrintIfFirstRow = "Y";
                    }

                    if (dr.Visible)
                    {
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        mamt = 0;
                        if (dr.Cells["Col_ProductName"].Value != null)
                        {
                            {
                                row = new PrintRow(dr.Cells["Col_ProductName"].Value.ToString().PadRight(30), PrintRowPixel, 10, PrintFont);
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

                            mamt = 0;
                            mcolpix = 310;
                                                         

                            if (dr.Cells["Col_salestock"].Value != null)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_salestock"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(mcolpix + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                            mcolpix += 200;
                           

                            if (dr.Cells["Col_Amount"].Value != null && Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString()) != 0)
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(470.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                            }

                            

                        }
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
               
                row = new PrintRow("Product", PrintRowPixel, 30, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("UOM", PrintRowPixel, 220, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pack", PrintRowPixel, 276, PrintFont);
                PrintBill.Rows.Add(row);
                int colpix = 330;               

                row = new PrintRow("Sale Stock", PrintRowPixel, colpix+20, PrintFont);
                PrintBill.Rows.Add(row);

                colpix = colpix + 165;

               

                row = new PrintRow("Amount", PrintRowPixel, colpix, PrintFont);
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
                mcbCompany.SelectedID = "";
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
            txtViewText.Text = mcbCompany.SeletedItem.ItemData[1].ToString();
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

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_SrNo";
                //column.HeaderText = "SrNo";
                //column.Width = 40;
                //column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.HeaderText = "Product";
                column.Width = 300;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductLoosePack";
                column.HeaderText = "UOM";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductPack";
                column.HeaderText = "Pack";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);              

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_salestock";
                column.HeaderText = "SALE Stock";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
            DataRow dtrow = null;
            string curproduct = "";
            int mqty = 0;
            int mscm = 0;
            double mamt = 0;
            _MTotalAmount = 0;
           
            int drsalestk = 0;
            string mvoudate = "";
            try
            {

                dtable = _SsStock.GetOverViewStocknSale(mcbCompany.SelectedID);
                _BindingSource = dtable;
                BindReportGrid();
                  

                dtable = _SsStock.GetSaleStockForStocknSale(_MToDate, mcbCompany.SelectedID);

                if (dtable != null)
                {
                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        mqty = 0;
                        mscm = 0;
                        dtrow = dtable.Rows[i];
                        curproduct = dtrow["ProductID"].ToString();
                        if (dtrow["Quantity"] != DBNull.Value)
                            mqty = Convert.ToInt16(dtrow["Quantity"].ToString());
                        if (dtrow["SchemeQuantity"] != DBNull.Value)
                            mscm = Convert.ToInt16(dtrow["SchemeQuantity"].ToString());
                        if (dtrow["VoucherDate"] != DBNull.Value)
                            mvoudate = dtrow["VoucherDate"].ToString();
                        if (Convert.ToInt32(mvoudate) >= Convert.ToInt32(_MFromDate) && Convert.ToInt32(mvoudate) <= Convert.ToInt32(_MToDate))
                        {
                            foreach (DataGridViewRow dr in dgvReportList.Rows)
                            {
                                if (dr.Cells["Col_ID"] != null && dr.Cells["Col_ID"].Value.ToString() == curproduct)
                                {

                                    drsalestk = 0;
                                    if (dr.Cells["Col_salestock"].Value != null)
                                        drsalestk = Convert.ToInt16(dr.Cells["Col_salestock"].Value.ToString());

                                    dr.Cells["Col_salestock"].Value = (drsalestk + mqty + mscm).ToString();
                                    mamt = 0;
                                    if (dtrow["Amount"] != DBNull.Value && dtrow["Amount"].ToString() != string.Empty)
                                        mamt = Convert.ToDouble(dtrow["Amount"].ToString());


                                    _MTotalAmount += mamt;

                                    dr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                                    break;
                                }

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

        private void BindReportGrid()
        {
            int _RowIndex;
            DataGridViewRow currentdr;
            foreach (DataRow dr in _BindingSource.Rows)
            {
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.Cells["Col_ID"].Value = dr["ProductID"].ToString();
                currentdr.Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                currentdr.Cells["Col_ProductLoosePack"].Value = dr["ProdLoosePack"].ToString();
                currentdr.Cells["Col_ProductPack"].Value = dr["ProdPack"].ToString();               
            }

        }

        private void btnOKMultiSelectionClick()
        {
            bool retValue = false;
            try
            {
                InitializeReportGrid();
                ShowpnlGO();
                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_MFromDate, _MToDate);
                if (retValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FillReportGrid();                   
                    this.Cursor = Cursors.Default;
                    PrintReportHead = "Sale  CompanyWise"; // From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = "[" + mcbCompany.SeletedItem.ItemData[1].ToString() + "]";
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
           
            int drsalestk = 0;          
            _MTotalRows = 0;
            int i = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {                  
                   
                    drsalestk = 0;                                   
                    if (dr.Cells["Col_salestock"].Value != null)
                        drsalestk = Convert.ToInt32(dr.Cells["Col_salestock"].Value.ToString());
                   
                    if (cbZero.Checked == true)
                    {
                        _MTotalRows += 1;
                        dr.Visible = true;
                        i += 1;
                    }
                    else
                    {
                        if ( drsalestk  == 0)
                        {

                            if (i >= 0)
                                dr.Visible = false;
                            i += 1;
                        }
                        else
                        {
                            _MTotalRows += 1;
                            dr.Visible = true;
                            i += 1;
                        }
                    }

                }
                _BindingSource.DefaultView.RowFilter = "ProdCompID = '" + mcbCompany.SelectedID + "'";

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
                mcbCompany.Focus();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
            else if (e.KeyCode == Keys.Left)
                fromDate1.Focus();
        }
        private void mcbCompany_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbCompany.SelectedID != null && mcbCompany.SelectedID != "")
                btnOKMultiSelectionClick();
        }

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                if (_ReportOption == "Sale")
                    ReportControl = new UclSaleListProduct();
                else
                    ReportControl = new UclStockListProductLedger();
                ShowReportForm(selectedID, "", _MFromDate, _MToDate);
                this.Cursor = Cursors.Default;
            }
        }
        #endregion Events

        #region tooltip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = F10");
                ttToolTip.SetToolTip(pnlMultiSelection, "F12 to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion  
    }
}
