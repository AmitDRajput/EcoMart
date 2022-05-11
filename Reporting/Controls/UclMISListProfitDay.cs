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
using System.IO;

namespace EcoMart.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclMISListProfitDay : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceCreditNote;
        private SaleList _SaleList;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotal1;
        private double _MTotal2;
        private double _MTotal3;
        private double _MTotal4;
        private double _MTotal5;
        private double _MTotalCRS = 0;
        private double _MTotalCRP = 0;
        private double _MTotalCRProfit = 0;
        private double _MTotalActualProfit = 0;
        //  private double _MTotal6;
        #endregion

        # region Constructor
        public UclMISListProfitDay()
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
                _BindingSourceCreditNote = new DataTable();
                _SaleList = new SaleList();
                //_MFromDate = General.ShopDetail.Shopsy;
                //_MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "MIS - PROFIT % DAYWISE";
                InitializeReportGrid();
                ClearControls();
                AddToolTip();
                fromDate1.Focus();
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
            if (keyPressed == Keys.A && modifier == Keys.Alt)
            {
                if (pnlMultiSelection1.Visible == true)
                {

                    retValue = true;
                }

            }
            if (keyPressed == Keys.G && modifier == Keys.Alt)
            {
                if (pnlMultiSelection1.Visible == true)
                {
                    btnOKMultiSelectionClick();
                    retValue = true;
                }
            }
            if (retValue == false)
            {
                retValue = base.HandleShortcutAction(keyPressed, modifier);
            }
            return retValue;
        }

        #endregion

        #region IReportview
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
                int mlen = 0;
                int colpix = 0;
                double mamt = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_VoucherDate"].Value != null)
                    {
                        if (PrintRowCount > FixAccounts.NumberOfRowsPerReport)
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
                        if (dr.Cells["Col_VoucherDate"].Value.ToString() == "Total")
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        else
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                        if (dr.Cells["Col_SaleAmount"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_SaleAmount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(40.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_SaleByPurchaseRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_SaleByPurchaseRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(130.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_ProfitInRs"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_ProfitInRs"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(220.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_CreditNoteBySaleRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_CreditNoteBySaleRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(280.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_CreditNoteByPurchaseRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_CreditNoteByPurchaseRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(370.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_CreditNoteProfitInRs"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_CreditNoteProfitInRs"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(460.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ActualProfitInRs"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_ActualProfitInRs"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(520.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_ProfitPercentBySaleRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_ProfitPercentBySaleRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(580.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ProfitPercentByPurchaseRate"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_ProfitPercentByPurchaseRate"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(640.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                    }
                }
                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintBill.Print_Bill();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public int PrintHead()
        {
            PrintRow row;
            try
            {
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Date", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Sale By", PrintRowPixel, 55, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Sale By", PrintRowPixel, 140, PrintFont);
                PrintBill.Rows.Add(row);              
                row = new PrintRow("Profit In Rs.", PrintRowPixel, 230, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("CR By", PrintRowPixel, 310, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("CR By", PrintRowPixel, 400, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("CR Diff", PrintRowPixel, 490, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Actual", PrintRowPixel, 540, PrintFont);
                PrintBill.Rows.Add(row);             
                row = new PrintRow("Profit % by", PrintRowPixel, 600, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Profit % By", PrintRowPixel, 680, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 15;

               
                row = new PrintRow("SaleRate", PrintRowPixel, 55, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("PurRate", PrintRowPixel, 140, PrintFont);
                PrintBill.Rows.Add(row);
               
                row = new PrintRow("SaleRate", PrintRowPixel, 310, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("PurRate", PrintRowPixel, 400, PrintFont);
                PrintBill.Rows.Add(row);
               
                row = new PrintRow("Profit In Rs.", PrintRowPixel, 540, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Sale Rate", PrintRowPixel, 600, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("By Pur Rate", PrintRowPixel, 680, PrintFont);
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
        #endregion IReportControl

        # region Other Private methods

        public void ClearControls()
        {
            try
            {

                InitializeDates();
                lblFooterMessage.Text = "";
                HidepnlGO();

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
            dgvReportList.InitializeColumnContextMenu();
        }

        public void HidepnlGO()
        {
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Visible = false;
            ViewToDate.Visible = false;
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
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
                column.DataPropertyName = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.HeaderText = "DATE";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleAmount";
                column.HeaderText = "Sale Amount";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleByPurchaseRate";
                column.HeaderText = "SaleByPurRate";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRs";
                column.HeaderText = "Profit In Rs";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreditNoteBySaleRate";
                column.HeaderText = "CR Sale Rate";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreditNoteByPurchaseRate";
                column.HeaderText = "CR Pur Rate";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CreditNoteProfitInRs";
                column.HeaderText = "CR Profit In Rs";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ActualProfitInRs";
                column.HeaderText = "Profit In Rs";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                column.HeaderText = "Profit % By Sale Rate";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                column.HeaderText = "Profit % By Purchase Rate";
                column.Width = 50;
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
                FormatReportGrid();
                dgvReportList.DataSource = _BindingSource;
                BindReportGrid();
                CalculateFinalTotals();
                NoofRows();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_ProfitPercentByPurchaseRate");
            dgvReportList.DoubleColumnNames.Add("Col_ProfitPercentBySaleRate");
            dgvReportList.DoubleColumnNames.Add("Col_ProfitInRs");
            dgvReportList.DoubleColumnNames.Add("Col_SaleByPurchaseRate");
            dgvReportList.DoubleColumnNames.Add("Col_SaleAmount");
        }

        private void BindReportGrid()
        {
            double msaleamt = 0;
            double mpuramt = 0;
            double mprofit = 0;
            double profitbysale = 0;
            double profitbypur = 0;
            double mcrnoteamt = 0;
            double mcrnotepuramt = 0;
            double mcrnoteprofit = 0;

            _MTotal1 = 0;
            _MTotal2 = 0;
            _MTotal3 = 0;
            _MTotal4 = 0;
            _MTotal5 = 0;
            _MTotalCRS = 0;
            _MTotalCRP = 0;
            _MTotalCRProfit = 0;
            _MTotalActualProfit = 0;

            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    msaleamt = 0;
                    mpuramt = 0;
                    profitbysale = 0;
                    profitbypur = 0;
                    mcrnoteamt = 0;
                    mcrnotepuramt = 0;
                    mprofit = 0;
                    mcrnoteprofit = 0;
                    if (dr["VoucherDate"].ToString() != null)
                    {
                        foreach (DataRow cdr in _BindingSourceCreditNote.Rows)
                        {
                            if (cdr["VoucherDate"] != null && cdr["VoucherDate"].ToString() == dr["VoucherDate"].ToString())
                            {
                                mcrnoteamt = Convert.ToDouble(cdr["AmountNet"].ToString());
                                mcrnotepuramt = Convert.ToDouble(cdr["AmountByPurchaseRate"].ToString());
                                mcrnoteprofit = mcrnoteamt - mcrnotepuramt;
                                break;
                            }
                        }
                        int rowIndex = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                        dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        if (dr["AmountNet"] != DBNull.Value)
                        {
                            dgvrow.Cells["Col_SaleAmount"].Value = dr["AmountNet"].ToString();
                            msaleamt = Convert.ToDouble(dr["AmountNet"].ToString());
                            _MTotal1 += msaleamt;
                        }
                        if (dr["AmountByPurchaseRate"] != DBNull.Value)
                        {
                            dgvrow.Cells["Col_SaleByPurchaseRate"].Value = dr["AmountByPurchaseRate"].ToString();
                            mpuramt = Convert.ToDouble(dr["AmountByPurchaseRate"].ToString());
                            _MTotal2 += mpuramt;
                        }
                        if (dr["ProfitInRupees"] != DBNull.Value)
                        {
                            mprofit = Convert.ToDouble(dr["ProfitInRupees"].ToString());
                            dgvrow.Cells["Col_ProfitInRs"].Value = mprofit.ToString("#0.00");
                            _MTotal3 += mprofit;
                        }
                        dgvrow.Cells["Col_CreditNoteBySaleRate"].Value = mcrnoteamt.ToString("#0.00");
                        _MTotalCRS += mcrnoteamt;
                        dgvrow.Cells["Col_CreditNoteByPurchaseRate"].Value = mcrnotepuramt.ToString("#0.00");
                        _MTotalCRP += mcrnotepuramt;
                        dgvrow.Cells["Col_CreditNoteProfitInRs"].Value = mcrnoteprofit.ToString("#0.00");
                        _MTotalCRProfit = mcrnotepuramt;

                        dgvrow.Cells["Col_ActualProfitInRs"].Value = (mprofit - mcrnoteprofit).ToString("#0.00");
                        _MTotalActualProfit += (mprofit - mcrnoteprofit);


                        profitbysale = Math.Round(((msaleamt - mpuramt - (mcrnoteamt - mcrnotepuramt)) * 100) / (msaleamt - (mcrnoteamt - mcrnotepuramt)), 2);
                        dgvrow.Cells["Col_ProfitPercentBySaleRate"].Value = profitbysale.ToString();
                        _MTotal4 = Math.Round(((_MTotal1 - _MTotal2 - (_MTotalCRS - _MTotalCRP)) * 100) / (_MTotal1 - (_MTotalCRS - _MTotalCRP)), 2);


                        profitbypur = Math.Round(((msaleamt - mpuramt - (mcrnoteamt - mcrnotepuramt)) * 100) / mpuramt, 2);
                        dgvrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = profitbypur.ToString();
                        _MTotal5 = Math.Round(((_MTotal1 - _MTotal2 - (_MTotalCRS - _MTotalCRP)) * 100) / _MTotal2, 2);
                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        private void CalculateFinalTotals()
        {
            int rowIndex = dgvReportList.Rows.Add();
            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
            dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
            dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
            dgvrow.Cells["Col_VoucherDate"].Value = "Total";
            dgvrow.Cells["Col_SaleAmount"].Value = _MTotal1.ToString("#0.00");
            dgvrow.Cells["Col_SaleByPurchaseRate"].Value = _MTotal2.ToString("#0.00");          
            dgvrow.Cells["Col_ProfitInRs"].Value = _MTotal3.ToString("#0.00");

            dgvrow.Cells["Col_CreditNoteBySaleRate"].Value = _MTotalCRS.ToString("#0.00");
            dgvrow.Cells["Col_CreditNoteByPurchaseRate"].Value = _MTotalCRP.ToString("#0.00");
            dgvrow.Cells["Col_CreditNoteProfitInRs"].Value =_MTotalCRProfit.ToString("#0.00");
            dgvrow.Cells["Col_ActualProfitInRs"].Value = _MTotalActualProfit.ToString("#0.00");

            dgvrow.Cells["Col_ProfitPercentBySaleRate"].Value = _MTotal4.ToString("#0.00");
            dgvrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _MTotal5.ToString("#0.00");
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
                dtable = _SaleList.GetOverviewDataProfitPercentDay(_MFromDate, _MToDate);
                _BindingSource = dtable;
                _BindingSourceCreditNote = _SaleList.GetCreditNoteDataProfitPercentDay(_MFromDate, _MToDate);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region Events
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
                    ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
                    ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
                    FillReportGrid();
                    PrintReportHead = "PROFIT DAYWISE From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = "";
                    this.Cursor = Cursors.Default;
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

        #endregion

        #region AddToolTip
        private void AddToolTip()
        {
            try
            {
                ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }
        #endregion AddToolTip

        private void btnOKMultiSelection1_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void btnOKMultiSelection1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }

        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelectionClick();
        }
    }
}
