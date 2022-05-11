using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.Common;
using PharmaSYSDistributorPlus.BusinessLayer;
using PharmaSYSDistributorPlus.InterfaceLayer;
using PharmaSYSDistributorPlus.Printing;
using PrintDataGrid;
using System.IO;

namespace PharmaSYSDistributorPlus.Reporting.Controls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclVATListPurchaseRegisterOtherDetails : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Purchase _Purchase;
        private string _MFromDate;
        private string _MToDate;      
        private double _MTotalCashDiscount = 0;
        private double _MTotalSpecialDiscount = 0;
        private double _MTotalSchemeDiscount = 0;
        private double _MTotalItemDiscount = 0;
        private double _MTotalAddON = 0;
        private double _MTotalCreditNote = 0;
        private double _MTotalDebitNote = 0;
        private string _MVoucherType = "";
        #endregion

        # region Constructor
        public UclVATListPurchaseRegisterOtherDetails()
        {
            try
            {
                InitializeComponent();
                ViewControl = new UclPurchase();
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
                _Purchase = new Purchase();               
                headerLabel1.Text = "VAT - PURCHASE REGISTER";              
                ClearControls();              
                HidepnlGO();
                AddToolTip();              
                rbtnCash.Focus();
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
            rbtnAll.Checked = true;
            fromDate1.Focus();
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
                //  totalrows = CalculateTotalRows(totalrows);
                PrintPageNumber = 1;
                PrintRowPixel = 0;
                int mlen = 0;
                double mamt = 0;
                int colpix = 0;
                totpg = Convert.ToDouble(Math.Ceiling(Convert.ToDouble(totalrows) / FixAccounts.NumberOfRowsPerReport));
                PrintTotalPages = Convert.ToInt32(Math.Ceiling(totpg));
                PrintHead();
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_VoucherType"].Value != null || dr.Cells["Col_AccName"].Value != null)
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
                        //  row = new PrintRow("", 0, 0, PrintFont);
                        mamt = 0;
                        if (dr.Cells["Col_AccName"].Value.ToString() == "Total")
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 17;
                            PrintRowCount += 1;
                        }
                        if (dr.Cells["Col_VoucherType"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_VoucherNumber"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 40, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_VoucherDate"].Value != null)
                        {
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 80, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AccName"].Value != null)
                        {
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30).Substring(0,15), PrintRowPixel, 140, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                       

                        if (dr.Cells["Col_CashDiscount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_CashDiscount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(260.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_SpecialDiscount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_SpecialDiscount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(330.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_SchemeDiscount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_SchemeDiscount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(400.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ItemDiscount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_ItemDiscount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(470.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AddOnFreight"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AddOnFreight"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(540.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AmountCreditNote"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AmountCreditNote"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(610.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AmountDebitNote"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AmountDebitNote"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(680.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
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

        private int PrintHead()
        {
            PrintRow row;
            try
            {
                PrintRowPixel = GeneralReport.PrintHeader(PrintTotalPages, PrintFont, PrintReportHead, PrintRowPixel, PrintPageNumber, PrintReportHead2, General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")), General.GetDateInDateFormat(DateTime.Now.Date.ToString("yyyyMMdd")));

                PrintRowPixel += 17;

                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);

                PrintRowPixel += 17;

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 50, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 80, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 140, PrintFont);
                PrintBill.Rows.Add(row);           
                row = new PrintRow("Cash Disc", PrintRowPixel, 280, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SPL Disc", PrintRowPixel, 350, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM Disc", PrintRowPixel, 420, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Item Disc", PrintRowPixel, 490, PrintFont);
                PrintBill.Rows.Add(row);

                row = new PrintRow("ADD-On", PrintRowPixel, 560, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("CR.Note", PrintRowPixel, 630, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("DB.Note", PrintRowPixel, 700, PrintFont);
                PrintBill.Rows.Add(row);
                //row = new PrintRow("RoundOFF", PrintRowPixel, 640, PrintFont);
                //PrintBill.Rows.Add(row);
                //row = new PrintRow("Amount", PrintRowPixel, 710, PrintFont);
                //PrintBill.Rows.Add(row);

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
            if (General.PharmaSYSDistributorPlusLicense.LicenseType == LicenseLib.LicenseTypes.Full)
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
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ACCID";            
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";            
                column.HeaderText = "TYPE";
                column.Width = 40;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";              
                column.HeaderText = "NUMBER";
                column.ValueType = typeof(Int32);
                column.SortMode = DataGridViewColumnSortMode.Automatic;
                column.Width = 55;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";             
                column.HeaderText = "DATE";
                column.Width = 75;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";            
                column.HeaderText = "Party";
                column.Width = 155;
                dgvReportList.Columns.Add(column);
               
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CashDiscount";            
                column.HeaderText = "Cash Discount";
                column.Width = 90;             
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SpecialDiscount";             
                column.HeaderText = "SPL Discount";
                column.Width = 90;              
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SchemeDiscount";              
                column.HeaderText = "SCM Discount";
                column.Width = 90;               
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ItemDiscount";             
                column.HeaderText = "ITEM Discount";
                column.Width = 90;               
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AddOnFreight";               
                column.HeaderText = "ADD-ONS";
                column.Width = 90;               
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountCreditNote";              
                column.HeaderText = "Credit Note";
                column.Width = 90;               
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountDebitNote";              
                column.HeaderText = "Debit Note";
                column.Width = 90;               
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RoundUpAmount";             
                column.HeaderText = "Round OFF";
                column.Width = 45;
                column.Visible = false;              
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";             
                column.HeaderText = "Amount";
                column.Width = 90;
                column.Visible = false;
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
            FillReportData();
            dgvReportList.DataSource = _BindingSource;
            FormatReportGrid();         
            BindReportGrid();
            CalculateFinalTotals();     
            NoofRows();           
        }
      

        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                dtable = _Purchase.GetOverviewDataForVATReportOtherDetails(_MFromDate, _MToDate, _MVoucherType);
                _BindingSource = dtable;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FormatReportGrid()
        {
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DoubleColumnNames.Add("Col_CashDiscount");
            dgvReportList.DoubleColumnNames.Add("Col_SpecialDiscount");
            dgvReportList.DoubleColumnNames.Add("Col_SchemeDiscount");
            dgvReportList.DoubleColumnNames.Add("Col_ItemDiscount");
            dgvReportList.DoubleColumnNames.Add("Col_AddOnFreight");
            dgvReportList.DoubleColumnNames.Add("Col_AmountCreditNote");
            dgvReportList.DoubleColumnNames.Add("Col_AmountDebitNote");
        }

        private void BindReportGrid()
        {
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (txtType.Text.ToString() == "" || (dr["VoucherType"].ToString() == txtType.Text.ToString()))
                    {
                        int rowIndex = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                        dgvrow.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                        dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        if (dr["VoucherNumber"] != null)
                            dgvrow.Cells["Col_VoucherNumber"].Value = Convert.ToInt32(dr["VoucherNumber"]);
                        dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        dgvrow.Cells["Col_CashDiscount"].Value = dr["AmountCashDiscount"].ToString();
                        dgvrow.Cells["Col_SpecialDiscount"].Value = dr["AmountSpecialDiscount"].ToString();
                        dgvrow.Cells["Col_SchemeDiscount"].Value = dr["AmountSchemeDiscount"].ToString();
                        dgvrow.Cells["Col_ItemDiscount"].Value = dr["AmountItemDiscount"].ToString();
                        dgvrow.Cells["Col_AddOnFreight"].Value = dr["AmountAddOnFreight"].ToString();
                        dgvrow.Cells["Col_AmountCreditNote"].Value = dr["AmountCreditNote"].ToString();
                        dgvrow.Cells["Col_AmountDebitNote"].Value = dr["AmountDebitNote"].ToString();
                    }
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
                GetVoucherType();  
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
                    PrintReportHead = "VAT Purchase Register OtherDetails  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = "[" + txtViewtext.Text.ToString() + "]";
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

        private void CheckFilter()
        {

            try
            {

                _MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");

                if (rbtnCash.Checked == true)
                {
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+ FixAccounts.VoucherTypeForCashPurchase +"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "CASH";
                    txtType.Text = FixAccounts.VoucherTypeForCashPurchase ;
                }
                else if (rbtnCreditStatement.Checked == true)
                {
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+ FixAccounts.VoucherTypeForCreditStatementPurchase+"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "CREDIT STATEMENT";
                    txtType.Text = FixAccounts.VoucherTypeForCreditStatementPurchase;
                }
                else if (rbtnCredit.Checked == true)
                {
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+ FixAccounts.VoucherTypeForCreditPurchase +"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "CREDIT";
                    txtType.Text = FixAccounts.VoucherTypeForCreditPurchase;
                }
                else
                {
                    _BindingSource.DefaultView.RowFilter = "Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "ALL";
                    txtType.Text = "";
                }

            }
            catch (Exception ex) { Log.WriteException(ex); }
        }
        private void GetVoucherType()
        {
            try
            {


                if (rbtnCash.Checked == true)
                {
                    _MVoucherType = FixAccounts.VoucherTypeForCashPurchase;
                }
                else if (rbtnCreditStatement.Checked == true)
                {
                    _MVoucherType = FixAccounts.VoucherTypeForCreditStatementPurchase;
                }
                else if (rbtnCredit.Checked == true)
                {
                    _MVoucherType = FixAccounts.VoucherTypeForCreditPurchase;
                }
                else
                {
                    _MVoucherType = "";
                }

            }
            catch (Exception ex) { Log.WriteException(ex); }
        }
        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }

        private void CalculateFinalTotals()
        {   
            _MTotalCashDiscount = 0;
            _MTotalSpecialDiscount = 0;
            _MTotalSchemeDiscount = 0;
            _MTotalItemDiscount = 0;
            _MTotalAddON = 0;
            _MTotalCreditNote = 0;
            _MTotalDebitNote = 0;
            try
            {
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    
                    _MTotalCashDiscount += Convert.ToDouble(dr.Cells["Col_CashDiscount"].Value.ToString());
                    _MTotalSpecialDiscount += Convert.ToDouble(dr.Cells["Col_SpecialDiscount"].Value.ToString());
                    _MTotalSchemeDiscount += Convert.ToDouble(dr.Cells["Col_SchemeDiscount"].Value.ToString());
                    _MTotalItemDiscount += Convert.ToDouble(dr.Cells["Col_ItemDiscount"].Value.ToString());
                    _MTotalAddON += Convert.ToDouble(dr.Cells["Col_AddOnFreight"].Value.ToString());
                    _MTotalCreditNote += Convert.ToDouble(dr.Cells["Col_AmountCreditNote"].Value.ToString());
                    _MTotalDebitNote += Convert.ToDouble(dr.Cells["Col_AmountDebitNote"].Value.ToString());                    
                }
                int rowIndex = dgvReportList.Rows.Add();
                DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                dgvrow.Cells["Col_AccName"].Value = "Total";
                dgvrow.Cells["Col_CashDiscount"].Value = _MTotalCashDiscount.ToString();
                dgvrow.Cells["Col_SpecialDiscount"].Value = _MTotalSpecialDiscount.ToString();
                dgvrow.Cells["Col_SchemeDiscount"].Value = _MTotalSchemeDiscount.ToString();
                dgvrow.Cells["Col_ItemDiscount"].Value = _MTotalItemDiscount.ToString();                
                dgvrow.Cells["Col_AddOnFreight"].Value = _MTotalAddON.ToString("#0.00");
                dgvrow.Cells["Col_AmountCreditNote"].Value = _MTotalCreditNote.ToString("#0.00");
                dgvrow.Cells["Col_AmountDebitNote"].Value = _MTotalDebitNote.ToString("#0.00");

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

       

        #endregion

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
      

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            try
            {
                if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string selectedID = dgvReportList.SelectedRow.Cells[0].Value.ToString();
                    ShowViewForm(selectedID, ViewMode.Current);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex.ToString());
            }
        }
        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                toDate1.Focus();
        }

        private void toDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtnAll.Focus();
        }

        private void rbtnAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelection1.Focus();
        }

        private void rbtnCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelection1.Focus();
        }

        private void rbtnCredit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelection1.Focus();
        }

         private void rbtnCreditStatement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOKMultiSelection1.Focus();
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
