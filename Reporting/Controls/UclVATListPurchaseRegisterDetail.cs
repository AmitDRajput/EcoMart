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
    public partial class UclVATListPurchaseRegisterDetail : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Purchase _Purchase;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        private double _MTotalZero = 0;
        private double _MTotalAmount5 = 0;
        private double _MTotalVAT5 = 0;
        private double _MTotalAmount12point5 = 0;
        private double _MTotalVAT12point5 = 0;
        private double _MTotalRoundoff = 0;
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
        public UclVATListPurchaseRegisterDetail()
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
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "VAT - PURCHASE REGISTER DETAIL";                    
                ClearControls();
                FillPartyCombo();
                HidepnlGO();
                PrintReportHead = "VAT Purchase Register Detail  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = "[" + txtViewtext.Text.ToString() + "]";
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
            base.SetFocus();
            rbtnCash.Focus();
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
                //PrintReportHead = "VAT Purchase Register Detail  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                //PrintReportHead2 = "[" + txtViewtext.Text.ToString() + "]";
                PrintBill.Rows.Clear();
                PrintFont = new Font(General.CurrentSetting.MsetPrintFontName, Convert.ToInt32(General.CurrentSetting.MsetPrintFontSize));
                int totalrows = dgvReportList.Rows.Count;
                //  totalrows = CalculateTotalRows(totalrows);
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
                            row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30).Substring(0, 15), PrintRowPixel, 140, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AmountZeroPercent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AmountZeroPercent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(250.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Amount5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(320.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_VAT5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_VAT5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(390.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Amount12point5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount12point5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(460.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }

                        if (dr.Cells["Col_VAT12Point5Percent"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_VAT12Point5Percent"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(540.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_CashDiscount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_CashDiscount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(610.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_SpecialDiscount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_SpecialDiscount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(680.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_SchemeDiscount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_SchemeDiscount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(750.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_ItemDiscount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_ItemDiscount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(820.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AddOnFreight"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AddOnFreight"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(890.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AmountCreditNote"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AmountCreditNote"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(960.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_AmountDebitNote"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_AmountDebitNote"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(1030.00 + ((11.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_RoundUpAmount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_RoundUpAmount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(1100.00 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Amount"].Value != null)
                        {

                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(1140.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
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

        public int CalculateTotalRows(int totrows)
        {
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {

                if (dr.Cells["Col_Batch"].Value != null && dr.Cells["Col_Batch"].Value.ToString() == "Total")
                    totrows += 2;
            }
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

                PrintRowPixel += 17;

                row = new PrintRow("Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 50, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 80, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 140, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pur 0%", PrintRowPixel, 280, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pur 6%", PrintRowPixel, 350, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("VAT 6%", PrintRowPixel, 420, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pur 13.5%", PrintRowPixel, 490, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("VAT 13.5%", PrintRowPixel, 550, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Cash Disc", PrintRowPixel, 620, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SPL Disc", PrintRowPixel, 690, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM Disc", PrintRowPixel, 760, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Item Disc", PrintRowPixel, 830, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("ADD-On", PrintRowPixel, 900, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("CR.Note", PrintRowPixel, 970, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("DB.Note", PrintRowPixel, 1040, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("RoundOFF", PrintRowPixel, 1110, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 1180, PrintFont);
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
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
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
                column.Width = 40;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";              
                column.HeaderText = "DATE";
                column.Width = 70;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";              
                column.HeaderText = "Party";
                column.Width = 174;
                dgvReportList.Columns.Add(column);               

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountZeroPercent";             
                column.HeaderText = "Pur.0 VAT";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount5Percent";              
                column.HeaderText = "Pur.6%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT5Percent";              
                column.HeaderText = "VAT.6%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount12point5Percent";              
                column.HeaderText = "Pur.13.5%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VAT12Point5Percent";              
                column.HeaderText = "VAT.13.5%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                column.Width = 100;
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
        private void FillPartyCombo()
        {
            try
            {
                mcbCreditor.SelectedID = null;
                mcbCreditor.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccDiscountOffered" };
                mcbCreditor.ColumnWidth = new string[5] { "0", "20", "200", "150", "50" };
                mcbCreditor.DisplayColumnNo = 2;
                mcbCreditor.ValueColumnNo = 0;
                mcbCreditor.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetSSAccountHoldersList(FixAccounts.AccCodeForCreditor);
                mcbCreditor.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillReportGrid()
        {
            FillReportData();
            try
            {
                dgvReportList.DataSource = _BindingSource;
                FormatReportGrid();
                CheckFilter();
                BindReportGrid();               
                _MTotalZero = 0;
                _MTotalAmount5 = 0;
                _MTotalVAT5 = 0;
                _MTotalAmount12point5 = 0;
                _MTotalVAT12point5 = 0;
                _MTotalRoundoff = 0;
                _MTotalAmount = 0;
                _MTotalCashDiscount = 0;
                _MTotalSpecialDiscount = 0;
                _MTotalSchemeDiscount = 0;
                _MTotalItemDiscount = 0;
                _MTotalAddON = 0;
                _MTotalCreditNote = 0;
                _MTotalDebitNote = 0;

                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                    _MTotalZero += Convert.ToDouble(dr.Cells["Col_AmountZeroPercent"].Value.ToString());
                    _MTotalAmount5 += Convert.ToDouble(dr.Cells["Col_Amount5Percent"].Value.ToString());
                    _MTotalVAT5 += Convert.ToDouble(dr.Cells["Col_VAT5Percent"].Value.ToString());
                    _MTotalAmount12point5 += Convert.ToDouble(dr.Cells["Col_Amount12point5Percent"].Value.ToString());
                    _MTotalVAT12point5 += Convert.ToDouble(dr.Cells["Col_VAT12Point5Percent"].Value.ToString());
                    _MTotalRoundoff += Convert.ToDouble(dr.Cells["Col_RoundUpAmount"].Value.ToString());
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
                //  dgvrow.DefaultCellStyle.BackColor = Color.Honeydew; Very light green
                dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                dgvrow.Cells["Col_AccName"].Value = "Total";
                dgvrow.Cells["Col_AmountZeroPercent"].Value = _MTotalZero.ToString("#0.00");
                dgvrow.Cells["Col_Amount5Percent"].Value = _MTotalAmount5.ToString("#0.00");
                dgvrow.Cells["Col_VAT5Percent"].Value = _MTotalVAT5.ToString("#0.00");
                dgvrow.Cells["Col_Amount12point5Percent"].Value = _MTotalAmount12point5.ToString("#0.00");
                dgvrow.Cells["Col_VAT12Point5Percent"].Value = _MTotalVAT12point5.ToString("#0.00");
                dgvrow.Cells["Col_CashDiscount"].Value = _MTotalCashDiscount.ToString("#0.00");
                dgvrow.Cells["Col_SpecialDiscount"].Value = _MTotalSpecialDiscount.ToString("#0.00");
                dgvrow.Cells["Col_SchemeDiscount"].Value = _MTotalSchemeDiscount.ToString("#0.00");
                dgvrow.Cells["Col_ItemDiscount"].Value = _MTotalItemDiscount.ToString("#0.00");
                dgvrow.Cells["Col_AddOnFreight"].Value = _MTotalAddON.ToString("#0.00");
                dgvrow.Cells["Col_AmountCreditNote"].Value = _MTotalCreditNote.ToString("#0.00");
                dgvrow.Cells["Col_AmountDebitNote"].Value = _MTotalDebitNote.ToString("#0.00");
                dgvrow.Cells["Col_RoundUpAmount"].Value = _MTotalRoundoff.ToString("#0.00");
                dgvrow.Cells["Col_Amount"].Value = _MTotalAmount.ToString("#0.00");
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
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
            dgvReportList.DoubleColumnNames.Add("Col_AmountZeroPercent");
            dgvReportList.DoubleColumnNames.Add("Col_Amount5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_VAT5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_Amount12point5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_VAT12Point5Percent");
            dgvReportList.DoubleColumnNames.Add("Col_TotalAdd");
            dgvReportList.DoubleColumnNames.Add("Col_TotalLess");
            dgvReportList.DoubleColumnNames.Add("Col_RoundUpAmount");
        }

        private void BindReportGrid()
        {
            try
            {
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    int rowIndex = dgvReportList.Rows.Add();
                    DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                    dgvrow.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                    dgvrow.Cells["Col_ACCID"].Value = dr["AccountID"].ToString();
                    dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                    if (dr["VoucherNumber"] != null)
                        dgvrow.Cells["Col_VoucherNumber"].Value = Convert.ToInt32(dr["VoucherNumber"]);
                    dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                    dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                    dgvrow.Cells["Col_AmountZeroPercent"].Value = dr["AmountPurchaseZeroVAT"].ToString();
                    dgvrow.Cells["Col_Amount5Percent"].Value = dr["AmountPurchase5PercentVAT"].ToString();
                    dgvrow.Cells["Col_VAT5Percent"].Value = dr["AmountVAT5Percent"].ToString();
                    dgvrow.Cells["Col_Amount12point5Percent"].Value = dr["AmountPurchase12point5PercentVAT"].ToString();
                    dgvrow.Cells["Col_VAT12Point5Percent"].Value = dr["AmountVAT12Point5Percent"].ToString();
                    dgvrow.Cells["Col_CashDiscount"].Value = dr["AmountCashDiscount"].ToString();
                    dgvrow.Cells["Col_SpecialDiscount"].Value = dr["AmountSpecialDiscount"].ToString();
                    dgvrow.Cells["Col_SchemeDiscount"].Value = dr["AmountSchemeDiscount"].ToString();
                    dgvrow.Cells["Col_ItemDiscount"].Value = dr["AmountItemDiscount"].ToString();
                    dgvrow.Cells["Col_AddOnFreight"].Value = dr["AmountAddOnFreight"].ToString();
                    dgvrow.Cells["Col_AmountCreditNote"].Value = dr["AmountCreditNote"].ToString();
                    dgvrow.Cells["Col_AmountDebitNote"].Value = dr["AmountDebitNote"].ToString();
                    dgvrow.Cells["Col_RoundUpAmount"].Value = dr["RoundUpAmount"].ToString();
                    dgvrow.Cells["Col_Amount"].Value = dr["AmountNet"].ToString();

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+ FixAccounts.VoucherTypeForCashPurchase+"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "CASH";
                }
                else if (rbtnCreditStatement.Checked == true)
                {
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+ FixAccounts.VoucherTypeForCreditStatementPurchase +"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "CREDIT STATEMENT";
                }
                else if (rbtnCredit.Checked == true)
                {
                    _BindingSource.DefaultView.RowFilter = " VoucherType = '"+ FixAccounts.VoucherTypeForCreditPurchase +"'  and   Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "CREDIT";
                }
                else
                {
                    _BindingSource.DefaultView.RowFilter = "Voucherdate >= '" + _MFromDate + "' and voucherdate <= '" + _MToDate + "'";
                    txtViewtext.Text = "ALL";
                }

            }
            catch (Exception ex) { Log.WriteException(ex); }
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
                if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID.ToString() != string.Empty)
                    dtable = _Purchase.GetOverviewDataForVATReport(_MFromDate, _MToDate, _MVoucherType, mcbCreditor.SelectedID);
                else
                    dtable = _Purchase.GetOverviewDataForVATReport(_MFromDate, _MToDate, _MVoucherType);
                _BindingSource = dtable;
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
                    PrintReportHead = "VAT Purchase Register Detail  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
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

        private void mcbCreditor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbCreditor.SelectedID != null && mcbCreditor.SelectedID.ToString() != string.Empty)
            {
                rbtnAll.Checked = true;
                btnOKMultiSelectionClick();
            }
        }
    }
}
