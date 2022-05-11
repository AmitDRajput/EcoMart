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
using System.IO;


namespace PharmaSYSRetailPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclVATListCombine : ReportBaseControl
    {

        #region Declaration
        private DataTable _BindingSourcePurchase;
        private DataTable _BindingSourceAllPurchase;
        private DataTable _BindingSourceSale;
        private DataTable _BindingSourceAllSale;
        private DataTable _BindingSourceSalesReturn;
        private DataTable _BindingSourceAllSalesReturn;
        private Purchase _Purchase;
        private SaleList _SaleList;
        private CreditDebitNote _CDNote;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        private double _MTotalZero = 0;
        private double _MTotalAmount5 = 0;
        private double _MTotalVAT5 = 0;
        private double _MTotalAmount12point5 = 0;
        private double _MTotalVAT12point5 = 0;
        private double _MTotalLess = 0;
        private double _MTotalAdd = 0;
        private double _MTotalRoundoff = 0;
        #endregion

        # region Constructor
        public UclVATListCombine()
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
                _BindingSourcePurchase = new DataTable();
                _BindingSourceAllPurchase = new DataTable();
                _BindingSourceSale = new DataTable();
                _BindingSourceAllSale = new DataTable();
                _BindingSourceSalesReturn = new DataTable();
                _BindingSourceAllSalesReturn = new DataTable();
                _Purchase = new Purchase();
                _SaleList = new SaleList();
                _CDNote = new CreditDebitNote();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "VAT - COMBINE REGISTER MONTHWISE SUMMARY";
                ConstructReportColumns();
                HidepnlGO();
                ClearControls();

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

        public override void SetFocus()
        {
            base.SetFocus();
            btnOKMultiSelection1.Focus();
        }

        #endregion IOverView Members

        # region IReport Members

        public override void Export(string ExportFileName)
        {
            base.Export(ExportFileName);

            string filename = "Report";
            if (ExportFileName != null && ExportFileName.ToString() != string.Empty)
                filename = ExportFileName.ToString().Trim();
            string filePath = "d:\\reports\\" + filename + ".csv";
            if (File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            PrintReportHead = "VAT COMBINE Register (Month) From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
            PrintReportHead2 = "[" + txtViewtext.Text.ToString() + "]";
            try
            {
                GeneralReport.ExportPrintHeader(PrintReportHead, PrintReportHead2, filePath);
                GeneralReport.ExportPrintDetails(dgvReportList, filePath);

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        public override void EMail(string ExportFileName, string EmailID)
        {
            base.EMail(ExportFileName, EmailID);
            string filename = "Report";
            string tosendemailID = "sheelabsharma@gmail.com";
            if (ExportFileName != null && ExportFileName.ToString() != string.Empty)
                filename = ExportFileName.ToString().Trim();
            if (EmailID != null && EmailID.ToString() != string.Empty)
                tosendemailID = EmailID.ToString().Trim();
            string filePath = "d:\\reports\\" + filename + ".csv";
            if (File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            try
            {
                GeneralReport.ExportPrintHeader(PrintReportHead, PrintReportHead2, filePath);
                GeneralReport.EmailPrintDetails(dgvReportList, filePath, tosendemailID);
                // Sir here I am sending grid,filepath for all reports

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                PrintFont = new Font("Arial", 8, FontStyle.Regular);
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
                    mamt = 0;
                    if (dr.Cells["Col_Month"].Value == null ) //&& dr.Cells["Col_NetVAT"].Value != null)
                    {
                        row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                    }

                    if (dr.Cells["Col_Month"].Value != null)
                    {
                        row = new PrintRow(dr.Cells["Col_Month"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                        PrintBill.Rows.Add(row);
                    }

                    if (dr.Cells["Col_VoucherType"].Value != null)
                    {
                        row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 100, PrintFont);
                        PrintBill.Rows.Add(row);
                    }

                    //if (dr.Cells["Col_AmountZeroPercent"].Value != null)
                    //{

                    //    mamt = Convert.ToDouble(dr.Cells["Col_AmountZeroPercent"].Value.ToString());
                    //    mlen = (mamt.ToString("#0.00").Length);
                    //    colpix = Convert.ToInt32(120.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                    //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    //    PrintBill.Rows.Add(row);
                    //}

                    if (dr.Cells["Col_Amount5Percent"].Value != null)
                    {

                        mamt = Convert.ToDouble(dr.Cells["Col_Amount5Percent"].Value.ToString());
                        mlen = (mamt.ToString("#0.00").Length);
                        colpix = Convert.ToInt32(200.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        PrintBill.Rows.Add(row);
                    }

                    if (dr.Cells["Col_VAT5Percent"].Value != null)
                    {

                        mamt = Convert.ToDouble(dr.Cells["Col_VAT5Percent"].Value.ToString());
                        mlen = (mamt.ToString("#0.00").Length);
                        colpix = Convert.ToInt32(290.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        PrintBill.Rows.Add(row);
                    }
                    if (dr.Cells["Col_Amount12point5Percent"].Value != null)
                    {

                        mamt = Convert.ToDouble(dr.Cells["Col_Amount12point5Percent"].Value.ToString());
                        mlen = (mamt.ToString("#0.00").Length);
                        colpix = Convert.ToInt32(360.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        PrintBill.Rows.Add(row);
                    }
                    if (dr.Cells["Col_VAT12Point5Percent"].Value != null)
                    {

                        mamt = Convert.ToDouble(dr.Cells["Col_VAT12Point5Percent"].Value.ToString());
                        mlen = (mamt.ToString("#0.00").Length);
                        colpix = Convert.ToInt32(450.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        PrintBill.Rows.Add(row);
                    }
                    //if (dr.Cells["Col_TotalLess"].Value != null)
                    //{

                    //    mamt = Convert.ToDouble(dr.Cells["Col_TotalLess"].Value.ToString());
                    //    mlen = (mamt.ToString("#0.00").Length);
                    //    colpix = Convert.ToInt32(470.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                    //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    //    PrintBill.Rows.Add(row);
                    //}
                    //if (dr.Cells["Col_TotalAdd"].Value != null)
                    //{

                    //    mamt = Convert.ToDouble(dr.Cells["Col_TotalAdd"].Value.ToString());
                    //    mlen = (mamt.ToString("#0.00").Length);
                    //    colpix = Convert.ToInt32(550.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                    //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    //    PrintBill.Rows.Add(row);
                    //}
                    //if (dr.Cells["Col_RoundUpAmount"].Value != null)
                    //{

                    //    mamt = Convert.ToDouble(dr.Cells["Col_RoundUpAmount"].Value.ToString());
                    //    mlen = (mamt.ToString("#0.00").Length);
                    //    colpix = Convert.ToInt32(630.00 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                    //    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                    //    PrintBill.Rows.Add(row);
                    //}
                    if (dr.Cells["Col_NetVAT"].Value != null)
                    {

                        mamt = Convert.ToDouble(dr.Cells["Col_NetVAT"].Value.ToString());
                        mlen = (mamt.ToString("#0.00").Length);
                        colpix = Convert.ToInt32(530.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                        row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                        PrintBill.Rows.Add(row);
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
                row = new PrintRow("Month", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Vou.Type", PrintRowPixel, 100, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pur. 5%VAT", PrintRowPixel, 200, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("VAT 5%", PrintRowPixel, 290, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pur.12.5%VAT", PrintRowPixel, 370, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("VAT 12.5%", PrintRowPixel, 460, PrintFont);
                PrintBill.Rows.Add(row);
                //row = new PrintRow("Less", PrintRowPixel, 500, PrintFont);
                //PrintBill.Rows.Add(row);
                //row = new PrintRow("Add", PrintRowPixel, 590, PrintFont);
                //PrintBill.Rows.Add(row);
                //row = new PrintRow("R.OFF", PrintRowPixel, 640, PrintFont);
                //PrintBill.Rows.Add(row);
                row = new PrintRow("NET Amount", PrintRowPixel, 550, PrintFont);
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
                string startdate = General.GetDateInDateFormat(General.ShopDetail.Shopsy);
                fromDate1.Text = startdate;
                string enddate = General.GetDateInDateFormat(General.ShopDetail.Shopey);
                toDate1.Text = enddate;
                lblFooterMessage.Text = "";
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
            tsbtnPrint.Enabled = true;
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
                column.Width = 50;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Month";
                column.HeaderText = "Month";
                column.Width = 100;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 100;
                dgvReportList.Columns.Add(column);

               
                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_AmountZeroPercent";
                //column.HeaderText = "Pur.0 VAT";
                //column.Width = 100;
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount5Percent";
                column.HeaderText = "Pur.5%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT5Percent";
                column.HeaderText = "VAT.5%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount12point5Percent";
                column.HeaderText = "Pur.12.5%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT12Point5Percent";
                column.HeaderText = "VAT.12.5%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_TotalLess";
                //column.HeaderText = "Less";
                //column.Width = 100;
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvReportList.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_TotalAdd";
                //column.HeaderText = "ADD";
                //column.Width = 100;
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvReportList.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_RoundUpAmount";
                //column.HeaderText = "Round OFF";
                //column.Width = 45;
                //column.DefaultCellStyle.Format = "N2";
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_NetVAT";
                column.HeaderText = "Net VAT";
                column.Width = 100;               
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
                FillReportData();               
                FormatReportGrid();              
                BindReportGrid();
               // CalculateFinalTotals();
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
                dtable = _Purchase.GetOverviewDataForVATReportMONTHALL(_MFromDate, _MToDate);
               
                _BindingSourcePurchase = dtable;
               
                    dtable = _SaleList.GetDataForVATRegisterMONTHALL(_MFromDate, _MToDate);
                
                _BindingSourceSale = dtable;
                dtable = _CDNote.GetOverviewDataForVATReportMonth(FixAccounts.VoucherTypeForCreditNoteStock, _MFromDate, _MToDate);
                _BindingSourceAllSalesReturn = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
            dgvReportList.DoubleColumnNames.Add("Col_AmountZeroPercent");
            dgvReportList.DoubleColumnNames.Add("Col_Amount5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_VAT5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_Amount12point5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_VAT12Point5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_TotalLess");
            dgvReportList.DoubleColumnNames.Add("Col_TotalAdd");
            dgvReportList.DoubleColumnNames.Add("Col_RoundUpAmount");
            dgvReportList.DoubleColumnNames.Add("Col_NetVAT");
        }

        private void BindReportGrid()
        {
            int startmonth = Convert.ToInt32(_MFromDate.Substring(4, 2));
            int endmonth = Convert.ToInt32(_MToDate.Substring(4, 2));
            int currentmonth = startmonth;
            double totsaleamt5 = 0;
            double totsalesreturnamt5 = 0;
            double totpurchaseamt5 = 0;
            double totsaleamt125 = 0;
            double totsalesreturnamt125 = 0;
            double totpurchaseamt125 = 0;
            double totsalevat5 = 0;
            double totsalesreturnvat5 = 0;
            double totpurchasevat5 = 0;
            double totsalevat125 = 0;
            double totsalesreturnvat125 = 0;
            double totpurchasevat125 = 0; 
            int mm = 0;
            int rowIndexo = 0;
            string yy = "";
            try
            {              
                while (true)
                {
                   

                    foreach (DataRow dr in _BindingSourceSale.Rows)
                    {
                        
                        mm = General.ConvertStringToDateyyyyMMdd(dr["VoucherDate"].ToString()).Month;
                        yy = dr["VoucherDate"].ToString().Substring(0, 4);
                        if (mm == currentmonth)
                        {
                            int rowIndex = dgvReportList.Rows.Add();
                            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                            dgvrow.Cells["Col_VoucherType"].Value = "Sale";
                            dgvrow.Cells["Col_Month"].Value = General.GetMonthAsStringShort(currentmonth - 1) + " - " + yy;
                          //  dgvrow.Cells["Col_AmountZeroPercent"].Value = dr["AmountForZeroVAT"].ToString();

                            dgvrow.Cells["Col_Amount5Percent"].Value = dr["AmountVAT5Per"].ToString();
                            totsaleamt5 = Convert.ToDouble(dr["AmountVAT5Per"].ToString());

                            dgvrow.Cells["Col_VAT5Percent"].Value = dr["VAT5Per"].ToString();
                            totsalevat5 = Convert.ToDouble(dr["VAT5Per"].ToString());

                            dgvrow.Cells["Col_Amount12point5Percent"].Value = dr["AmountVAT12point5Per"].ToString();
                            totsaleamt125 = Convert.ToDouble(dr["AmountVAT12point5Per"].ToString());

                            dgvrow.Cells["Col_VAT12Point5Percent"].Value = dr["VAT12Point5Per"].ToString();
                            totsalevat125 = Convert.ToDouble(dr["VAT12Point5Per"].ToString());
                            //dgvrow.Cells["Col_TotalLess"].Value = dr["TotalLess"].ToString();
                            //dgvrow.Cells["Col_TotalAdd"].Value = dr["TotalAdd"].ToString();
                            //dgvrow.Cells["Col_RoundUpAmount"].Value = dr["RoundUpAmount"].ToString();
                            dgvrow.Cells["Col_NetVAT"].Value = (totsalevat5 + totsalevat125).ToString();
                            break;
                        }

                    }

                    string ifsalesreturn = "N";
                    foreach (DataRow dr in _BindingSourceAllSalesReturn.Rows)
                    {
                       
                        mm = General.ConvertStringToDateyyyyMMdd(dr["VoucherDate"].ToString()).Month;
                        yy = dr["VoucherDate"].ToString().Substring(0, 4);
                        if (mm == currentmonth)
                        {
                            int rowIndex = dgvReportList.Rows.Add();
                            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                            dgvrow.Cells["Col_VoucherType"].Value = "Sales Return";
                            dgvrow.Cells["Col_Month"].Value = General.GetMonthAsStringShort(currentmonth - 1) + " - " + yy;
                          //  dgvrow.Cells["Col_AmountZeroPercent"].Value = dr["AmountPurchaseZeroVAT"].ToString();
                            dgvrow.Cells["Col_Amount5Percent"].Value = dr["Amount5"].ToString();
                            totsalesreturnamt5 = Convert.ToDouble(dr["Amount5"].ToString());

                            dgvrow.Cells["Col_VAT5Percent"].Value = dr["VAT5"].ToString();
                            totsalesreturnvat5 = Convert.ToDouble(dr["VAT5"].ToString());

                            dgvrow.Cells["Col_Amount12point5Percent"].Value = dr["Amount12point5"].ToString();
                            totsalesreturnamt125 = Convert.ToDouble(dr["Amount12point5"].ToString());

                            dgvrow.Cells["Col_VAT12Point5Percent"].Value = dr["VAT12Point5"].ToString();
                            totsalesreturnvat125 = Convert.ToDouble(dr["VAT12Point5"].ToString());
                            //dgvrow.Cells["Col_TotalLess"].Value = dr["TotalLess"].ToString();
                            //dgvrow.Cells["Col_TotalAdd"].Value = dr["TotalAdd"].ToString();
                            //dgvrow.Cells["Col_RoundUpAmount"].Value = dr["RoundUpAmount"].ToString();
                            dgvrow.Cells["Col_NetVAT"].Value = (totsalesreturnvat5 + totsalesreturnvat125).ToString();
                            ifsalesreturn = "Y";
                            break;

                        }

                    }
                    if (ifsalesreturn == "N")
                    {
                        rowIndexo = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow1 = dgvReportList.Rows[rowIndexo];
                        dgvrow1.Cells["Col_VoucherType"].Value = "SalesReturn";
                        dgvrow1.Cells["Col_Month"].Value = General.GetMonthAsStringShort(currentmonth - 1) + " - " + yy;
                        // dgvrow.Cells["Col_AmountZeroPercent"].Value = dr["AmountPurchaseZeroVAT"].ToString();
                        dgvrow1.Cells["Col_Amount5Percent"].Value = (totsalesreturnamt5).ToString();
                        dgvrow1.Cells["Col_VAT5Percent"].Value = (totsalesreturnvat5).ToString();
                        dgvrow1.Cells["Col_Amount12point5Percent"].Value = (totsalesreturnamt125).ToString();
                        dgvrow1.Cells["Col_VAT12Point5Percent"].Value = (totsalesreturnvat125).ToString();
                        dgvrow1.Cells["Col_NetVAT"].Value = (totsalesreturnvat5 + totsalesreturnvat125).ToString();
                    }

                    rowIndexo =  dgvReportList.Rows.Add();
                    DataGridViewRow dgvrow3 = dgvReportList.Rows[rowIndexo];
                    dgvrow3.Cells["Col_VoucherType"].Value = "Sales - SalesReturn";
                    dgvrow3.Cells["Col_Month"].Value = General.GetMonthAsStringShort(currentmonth - 1) + " - " + yy;
                    // dgvrow.Cells["Col_AmountZeroPercent"].Value = dr["AmountPurchaseZeroVAT"].ToString();
                    dgvrow3.Cells["Col_Amount5Percent"].Value = (totsaleamt5 - totsalesreturnamt5).ToString();
                    dgvrow3.Cells["Col_VAT5Percent"].Value = (totsalevat5 - totsalesreturnvat5).ToString();
                    dgvrow3.Cells["Col_Amount12point5Percent"].Value = (totsaleamt125 - totsalesreturnamt125).ToString();
                    dgvrow3.Cells["Col_VAT12Point5Percent"].Value = (totsalevat125 - totsalesreturnvat125).ToString();
                    //dgvrow.Cells["Col_TotalLess"].Value = dr["TotalLess"].ToString();
                    dgvrow3.Cells["Col_NetVAT"].Value = ((totsalevat5 - totsalesreturnvat5) + (totsalevat125 - totsalesreturnvat125)).ToString();
                    foreach (DataRow dr in _BindingSourcePurchase.Rows)
                    {
                       
                        mm = General.ConvertStringToDateyyyyMMdd(dr["VoucherDate"].ToString()).Month;
                        yy = dr["VoucherDate"].ToString().Substring(0, 4);
                        if (mm == currentmonth)
                        {
                            int rowIndex = dgvReportList.Rows.Add();
                            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                            dgvrow.Cells["Col_VoucherType"].Value = "Purchase";
                            dgvrow.Cells["Col_Month"].Value = General.GetMonthAsStringShort(currentmonth - 1) + " - " + yy;
                           // dgvrow.Cells["Col_AmountZeroPercent"].Value = dr["AmountPurchaseZeroVAT"].ToString();
                            dgvrow.Cells["Col_Amount5Percent"].Value = dr["AmountPurchase5PercentVAT"].ToString();
                            totpurchaseamt5 = Convert.ToDouble(dr["AmountPurchase5PercentVAT"].ToString());

                            dgvrow.Cells["Col_VAT5Percent"].Value = dr["AmountVAT5Percent"].ToString();
                            totpurchasevat5 = Convert.ToDouble(dr["AmountVAT5Percent"].ToString());

                            dgvrow.Cells["Col_Amount12point5Percent"].Value = dr["AmountPurchase12point5PercentVAT"].ToString();
                            totpurchaseamt125 = Convert.ToDouble(dr["AmountPurchase12point5PercentVAT"].ToString());

                            dgvrow.Cells["Col_VAT12Point5Percent"].Value = dr["AmountVAT12Point5Percent"].ToString();
                            totpurchasevat125 = Convert.ToDouble(dr["AmountVAT12Point5Percent"].ToString());
                            //dgvrow.Cells["Col_TotalLess"].Value = dr["TotalLess"].ToString();
                            //dgvrow.Cells["Col_TotalAdd"].Value = dr["TotalAdd"].ToString();
                            //dgvrow.Cells["Col_RoundUpAmount"].Value = dr["RoundUpAmount"].ToString();
                            dgvrow.Cells["Col_NetVAT"].Value = (totpurchasevat5 + totpurchasevat125).ToString();
                            break;
                        }

                    }

                    rowIndexo = dgvReportList.Rows.Add();
                    DataGridViewRow dgvrow2 = dgvReportList.Rows[rowIndexo];
                    dgvrow2.Cells["Col_VoucherType"].Value = "Net ";
                    dgvrow2.Cells["Col_Month"].Value = General.GetMonthAsStringShort(currentmonth - 1) + " - " + yy;
                    // dgvrow.Cells["Col_AmountZeroPercent"].Value = dr["AmountPurchaseZeroVAT"].ToString();
                    dgvrow2.Cells["Col_Amount5Percent"].Value = (totsaleamt5 - totsalesreturnamt5 - totpurchaseamt5).ToString();
                    dgvrow2.Cells["Col_VAT5Percent"].Value = (totsalevat5 - totsalesreturnvat5-totpurchasevat5).ToString();
                    dgvrow2.Cells["Col_Amount12point5Percent"].Value = (totsaleamt125 - totsalesreturnamt125 - totpurchaseamt125).ToString();
                    dgvrow2.Cells["Col_VAT12Point5Percent"].Value = (totsalevat125 - totsalesreturnvat125-totpurchasevat125).ToString();
                    dgvrow2.Cells["Col_NetVAT"].Value =  Math.Round((totsalevat5 - totsalesreturnvat5-totpurchasevat5)+(totsalevat125 - totsalesreturnvat125-totpurchasevat125),2).ToString();
                   
                    rowIndexo = dgvReportList.Rows.Add();
                    totpurchaseamt125 = 0;
                    totpurchaseamt5 = 0;
                    totpurchasevat125 = 0;
                    totpurchasevat5 = 0;
                    totsaleamt125 = 0;
                    totsaleamt5 = 0;
                    totsalesreturnamt125 = 0;
                    totsalesreturnamt5 = 0;
                    totsalesreturnvat125 = 0;
                    totsalesreturnvat5 = 0;
                    totsalevat125 = 0;
                    totsalevat5 = 0;
                    if (currentmonth == endmonth)
                        break;
                    currentmonth += 1;
                    if (currentmonth > 12)
                        currentmonth = 1;
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
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
                    lblFooterMessage.Text = "";
                    InitializeReportGrid();
                    ShowpnlGO();
                    ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
                    ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
                    FillReportGrid();
                    PrintReportHead = "VAT COMBINE Register (Month) From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = "";
                    this.Cursor = Cursors.Default;
                    dgvReportList.Focus();
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

        private void CalculateFinalTotals()
        {
            _MTotalZero = 0;
            _MTotalAmount5 = 0;
            _MTotalVAT5 = 0;
            _MTotalAmount12point5 = 0;
            _MTotalVAT12point5 = 0;
            _MTotalLess = 0;
            _MTotalAdd = 0;
            _MTotalRoundoff = 0;
            _MTotalAmount = 0;

            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {
                _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                _MTotalZero += Convert.ToDouble(dr.Cells["Col_AmountZeroPercent"].Value.ToString());
                _MTotalAmount5 += Convert.ToDouble(dr.Cells["Col_Amount5Percent"].Value.ToString());
                _MTotalVAT5 += Convert.ToDouble(dr.Cells["Col_VAT5Percent"].Value.ToString());
                _MTotalAmount12point5 += Convert.ToDouble(dr.Cells["Col_Amount12point5Percent"].Value.ToString());
                _MTotalVAT12point5 += Convert.ToDouble(dr.Cells["Col_VAT12Point5Percent"].Value.ToString());
                _MTotalLess += Convert.ToDouble(dr.Cells["Col_TotalLess"].Value.ToString());
                _MTotalAdd += Convert.ToDouble(dr.Cells["Col_TotalAdd"].Value.ToString());
                _MTotalRoundoff += Convert.ToDouble(dr.Cells["Col_RoundUpAmount"].Value.ToString());
            }

            int rowIndex = dgvReportList.Rows.Add();
            DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
            dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
            dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;

            dgvrow.Cells["Col_AmountZeroPercent"].Value = _MTotalZero.ToString("#0.00");
            dgvrow.Cells["Col_Amount5Percent"].Value = _MTotalAmount5.ToString("#0.00");
            dgvrow.Cells["Col_VAT5Percent"].Value = _MTotalVAT5.ToString("#0.00");
            dgvrow.Cells["Col_Amount12point5Percent"].Value = _MTotalAmount12point5.ToString("#0.00");
            dgvrow.Cells["Col_VAT12Point5Percent"].Value = _MTotalVAT12point5.ToString("#0.00");
            dgvrow.Cells["Col_TotalLess"].Value = _MTotalLess.ToString("#0.00");
            dgvrow.Cells["Col_TotalAdd"].Value = _MTotalAdd.ToString("#0.00");
            dgvrow.Cells["Col_RoundUpAmount"].Value = _MTotalRoundoff.ToString("#0.00");
            dgvrow.Cells["Col_Amount"].Value = _MTotalAmount.ToString("#0.00");

        }

        #endregion

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void btnOKMultiSelection1_KeyDown(object sender, KeyEventArgs e)
        {
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
        #endregion

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
