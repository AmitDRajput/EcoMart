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
    public partial class UclGSTPurchaseRegister : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private Purchase _Purchase;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;
        private double _MTotalZero = 0;
        private double _MTotalAmount5S = 0;
        private double _MTotalVAT5S = 0;
        private double _MTotalAmount12S = 0;
        private double _MTotalVAT12S = 0;
        private double _MTotalAmount18S = 0;
        private double _MTotalVAT18S = 0;
        private double _MTotalAmount28S = 0;
        private double _MTotalVAT28S = 0;

        private double _MTotalAmount5C = 0;
        private double _MTotalVAT5C = 0;
        private double _MTotalAmount12C = 0;
        private double _MTotalVAT12C = 0;
        private double _MTotalAmount18C = 0;
        private double _MTotalVAT18C = 0;
        private double _MTotalAmount28C = 0;
        private double _MTotalVAT28C = 0;

        private double _MTotalRoundoff = 0;
        //private double _MTotalCashDiscount = 0;
        //private double _MTotalSpecialDiscount = 0;
        //private double _MTotalSchemeDiscount = 0;
        //private double _MTotalItemDiscount = 0;
        private double _MTotalAddON = 0;
        private double _MTotalCreditNote = 0;
        private double _MTotalDebitNote = 0;
        //private string _MVoucherType = "";

        private int month = 0;
        private string syear = "";
        private string smonth = "";
        #endregion

        #region Constructor
        public UclGSTPurchaseRegister()
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
                btnVoucherNumberWise.Checked = true;
                headerLabel1.Text = "GST - PURCHASE REGISTER DETAIL";
                ClearControls();
                HidepnlGO();
                PrintReportHead = "GST Purchase Register Detail  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                PrintReportHead2 = " ";
                AddToolTip();
                txtMonth.Focus();

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
            txtMonth.Focus();
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
            //if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            //{
                tsbtnPrint.Enabled = true;
            //}
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
                column.Name = "Col_ProductID";
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
                if (btnVoucherNumberWise.Checked == true)
                    column.HeaderText = "Party";
                else
                    column.HeaderText = "Product";
                column.Width = 174;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GSTNumber";
                if (btnVoucherNumberWise.Checked == true)
                    column.HeaderText = "GSTIN";
                else
                    column.HeaderText = "HSNNumber";
                column.Width = 174;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AmountZeroPercent";
                column.HeaderText = "Pur.0 GST";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount5PercentS";
                column.HeaderText = "Pur.S.2.5%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GST5PercentS";
                column.HeaderText = "GST S 2.5%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount5PercentC";
                column.HeaderText = "Pur.C.2.5%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GST5PercentC";
                column.HeaderText = "GST C 2.5%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount12PercentS";
                column.HeaderText = "Pur.S.6.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GST12PercentS";
                column.HeaderText = "GST S 6.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount12PercentC";
                column.HeaderText = "Pur.C.6.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GST12PercentC";
                column.HeaderText = "GST C 6.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount18PercentS";
                column.HeaderText = "Pur.S.9.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GST18PercentS";
                column.HeaderText = "GST S 9.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount18PercentC";
                column.HeaderText = "Pur.C.9.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GST18PercentC";
                column.HeaderText = "GST C 9.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount28PercentS";
                column.HeaderText = "Pur.S.14.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GST28PercentS";
                column.HeaderText = "GST S 14.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount28PercentC";
                column.HeaderText = "Pur.C.14.0%";
                column.Width = 100;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_GST28PercentC";
                column.HeaderText = "GST C 14.0%";
                column.Width = 100;
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

        private void FillReportGrid()
        {
            FillReportData();
            try
            {
                dgvReportList.DataSource = _BindingSource;
                FormatReportGrid();
                // CheckFilter();
                BindReportGrid();
                _MTotalZero = 0;
                _MTotalAmount5S = 0;
                _MTotalVAT5S = 0;
                _MTotalAmount12S = 0;
                _MTotalVAT12S = 0;
                _MTotalAmount18S = 0;
                _MTotalVAT18S = 0;
                _MTotalAmount28S = 0;
                _MTotalVAT28S = 0;

                _MTotalAmount5C = 0;
                _MTotalVAT5C = 0;
                _MTotalAmount12C = 0;
                _MTotalVAT12C = 0;
                _MTotalAmount18C = 0;
                _MTotalVAT18C = 0;
                _MTotalAmount28C = 0;
                _MTotalVAT28C = 0;


                _MTotalRoundoff = 0;
                _MTotalAmount = 0;
              //  _MTotalCashDiscount = 0;
                //_MTotalSpecialDiscount = 0;
                //_MTotalSchemeDiscount = 0;
                //_MTotalItemDiscount = 0;
                _MTotalAddON = 0;
                _MTotalCreditNote = 0;
                _MTotalDebitNote = 0;

                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    if (dr.Cells["Col_Amount"].Value != null && dr.Cells["Col_Amount"].Value.ToString() != string.Empty)
                        _MTotalAmount += Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                    if (dr.Cells["Col_AmountZeroPercent"].Value != null && dr.Cells["Col_AmountZeroPercent"].Value.ToString() != string.Empty)
                        _MTotalZero += Convert.ToDouble(dr.Cells["Col_AmountZeroPercent"].Value.ToString());
                    if (dr.Cells["Col_Amount5PercentS"].Value != null && dr.Cells["Col_Amount5PercentS"].Value.ToString() != string.Empty)
                        _MTotalAmount5S += Convert.ToDouble(dr.Cells["Col_Amount5PercentS"].Value.ToString());
                    if (dr.Cells["Col_GST5PercentS"].Value != null && dr.Cells["Col_GST5PercentS"].Value.ToString() != string.Empty)
                        _MTotalVAT5S += Convert.ToDouble(dr.Cells["Col_GST5PercentS"].Value.ToString());
                    if (dr.Cells["Col_Amount12PercentS"].Value != null && dr.Cells["Col_Amount12PercentS"].Value.ToString() != string.Empty)
                        _MTotalAmount12S += Convert.ToDouble(dr.Cells["Col_Amount12PercentS"].Value.ToString());
                    if (dr.Cells["Col_GST12PercentS"].Value != null && dr.Cells["Col_GST12PercentS"].Value.ToString() != string.Empty)
                        _MTotalVAT12S += Convert.ToDouble(dr.Cells["Col_GST12PercentS"].Value.ToString());
                    if (dr.Cells["Col_Amount18PercentS"].Value != null && dr.Cells["Col_Amount18PercentS"].Value.ToString() != string.Empty)
                        _MTotalAmount18S += Convert.ToDouble(dr.Cells["Col_Amount18PercentS"].Value.ToString());
                    if (dr.Cells["Col_GST18PercentS"].Value != null && dr.Cells["Col_GST18PercentS"].Value.ToString() != string.Empty)
                        _MTotalVAT18S += Convert.ToDouble(dr.Cells["Col_GST18PercentS"].Value.ToString());
                    if (dr.Cells["Col_Amount28PercentS"].Value != null && dr.Cells["Col_Amount28PercentS"].Value.ToString() != string.Empty)
                        _MTotalAmount28S += Convert.ToDouble(dr.Cells["Col_Amount28PercentS"].Value.ToString());
                    if (dr.Cells["Col_GST28PercentS"].Value != null && dr.Cells["Col_GST28PercentS"].Value.ToString() != string.Empty)
                        _MTotalVAT28S += Convert.ToDouble(dr.Cells["Col_GST28PercentS"].Value.ToString());

                    if (dr.Cells["Col_Amount5PercentC"].Value != null && dr.Cells["Col_Amount5PercentC"].Value.ToString() != string.Empty)
                        _MTotalAmount5C += Convert.ToDouble(dr.Cells["Col_Amount5PercentC"].Value.ToString());
                    if (dr.Cells["Col_GST5PercentC"].Value != null && dr.Cells["Col_GST5PercentC"].Value.ToString() != string.Empty)
                        _MTotalVAT5C += Convert.ToDouble(dr.Cells["Col_GST5PercentC"].Value.ToString());
                    if (dr.Cells["Col_Amount12PercentC"].Value != null && dr.Cells["Col_Amount12PercentC"].Value.ToString() != string.Empty)
                        _MTotalAmount12C += Convert.ToDouble(dr.Cells["Col_Amount12PercentC"].Value.ToString());
                    if (dr.Cells["Col_GST12PercentC"].Value != null && dr.Cells["Col_Amount12PercentS"].Value.ToString() != string.Empty)
                        _MTotalVAT12C += Convert.ToDouble(dr.Cells["Col_GST12PercentC"].Value.ToString());
                    if (dr.Cells["Col_Amount18PercentC"].Value != null && dr.Cells["Col_Amount18PercentC"].Value.ToString() != string.Empty)
                        _MTotalAmount18C += Convert.ToDouble(dr.Cells["Col_Amount18PercentC"].Value.ToString());
                    if (dr.Cells["Col_GST18PercentC"].Value != null && dr.Cells["Col_GST18PercentC"].Value.ToString() != string.Empty)
                        _MTotalVAT18C += Convert.ToDouble(dr.Cells["Col_GST18PercentC"].Value.ToString());
                    if (dr.Cells["Col_Amount28PercentC"].Value != null && dr.Cells["Col_Amount28PercentC"].Value.ToString() != string.Empty)
                        _MTotalAmount28C += Convert.ToDouble(dr.Cells["Col_Amount28PercentC"].Value.ToString());
                    if (dr.Cells["Col_GST28PercentC"].Value != null && dr.Cells["Col_GST28PercentC"].Value.ToString() != string.Empty)
                        _MTotalVAT28C += Convert.ToDouble(dr.Cells["Col_GST28PercentC"].Value.ToString());

                    if (btnVoucherNumberWise.Checked == true)
                    {
                        if (dr.Cells["Col_RoundUpAmount"].Value != null)
                            _MTotalRoundoff += Convert.ToDouble(dr.Cells["Col_RoundUpAmount"].Value.ToString());
                       // if (dr.Cells["Col_CashDiscount"].Value != null && dr.Cells["Col_CashDiscount"].Value.ToString() != string.Empty)
                         //   _MTotalCashDiscount += Convert.ToDouble(dr.Cells["Col_CashDiscount"].Value.ToString());
                        //    _MTotalSpecialDiscount += Convert.ToDouble(dr.Cells["Col_SpecialDiscount"].Value.ToString());
                        //     _MTotalSchemeDiscount += Convert.ToDouble(dr.Cells["Col_SchemeDiscount"].Value.ToString());
                        //    _MTotalItemDiscount += Convert.ToDouble(dr.Cells["Col_ItemDiscount"].Value.ToString
                        //    if (dr.Cells["Col_AddOnFreight"].Value != null)
                        //       _MTotalAddON += Convert.ToDouble(dr.Cells["Col_AddOnFreight"].Value.ToString());
                        if (dr.Cells["Col_AmountCreditNote"].Value != null && dr.Cells["Col_AmountCreditNote"].Value.ToString() != string.Empty)
                            _MTotalCreditNote += Convert.ToDouble(dr.Cells["Col_AmountCreditNote"].Value.ToString());
                        if (dr.Cells["Col_AmountDebitNote"].Value != null && dr.Cells["Col_AmountDebitNote"].Value.ToString() != string.Empty)
                            _MTotalDebitNote += Convert.ToDouble(dr.Cells["Col_AmountDebitNote"].Value.ToString());
                    }
                }
                int rowIndex = dgvReportList.Rows.Add();
                DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                //  dgvrow.DefaultCellStyle.BackColor = Color.Honeydew; Very light green
                dgvrow.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                dgvrow.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                dgvrow.Cells["Col_AccName"].Value = "Total";
                dgvrow.Cells["Col_AmountZeroPercent"].Value = _MTotalZero.ToString("#0.00");
                dgvrow.Cells["Col_Amount5PercentS"].Value = _MTotalAmount5S.ToString("#0.00");
                dgvrow.Cells["Col_GST5PercentS"].Value = _MTotalVAT5S.ToString("#0.00");
                dgvrow.Cells["Col_Amount12PercentS"].Value = _MTotalAmount12S.ToString("#0.00");
                dgvrow.Cells["Col_GST12PercentS"].Value = _MTotalVAT12S.ToString("#0.00");
                dgvrow.Cells["Col_Amount18PercentS"].Value = _MTotalAmount18S.ToString("#0.00");
                dgvrow.Cells["Col_GST18PercentS"].Value = _MTotalVAT18S.ToString("#0.00");
                dgvrow.Cells["Col_Amount28PercentS"].Value = _MTotalAmount28S.ToString("#0.00");
                dgvrow.Cells["Col_GST28PercentS"].Value = _MTotalVAT28S.ToString("#0.00");

                dgvrow.Cells["Col_Amount5PercentC"].Value = _MTotalAmount5C.ToString("#0.00");
                dgvrow.Cells["Col_GST5PercentC"].Value = _MTotalVAT5C.ToString("#0.00");
                dgvrow.Cells["Col_Amount12PercentC"].Value = _MTotalAmount12C.ToString("#0.00");
                dgvrow.Cells["Col_GST12PercentC"].Value = _MTotalVAT12C.ToString("#0.00");
                dgvrow.Cells["Col_Amount18PercentC"].Value = _MTotalAmount18C.ToString("#0.00");
                dgvrow.Cells["Col_GST18PercentC"].Value = _MTotalVAT18C.ToString("#0.00");
                dgvrow.Cells["Col_Amount28PercentC"].Value = _MTotalAmount28C.ToString("#0.00");
                dgvrow.Cells["Col_GST28PercentC"].Value = _MTotalVAT28C.ToString("#0.00");

              //  dgvrow.Cells["Col_CashDiscount"].Value = _MTotalCashDiscount.ToString("#0.00");
                //      dgvrow.Cells["Col_SpecialDiscount"].Value = _MTotalSpecialDiscount.ToString("#0.00");
                //     dgvrow.Cells["Col_SchemeDiscount"].Value = _MTotalSchemeDiscount.ToString("#0.00");
                //     dgvrow.Cells["Col_ItemDiscount"].Value = _MTotalItemDiscount.ToString("#0.00");
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
            dgvReportList.DoubleColumnNames.Add("Col_Amount5PercentS");
            dgvReportList.DoubleColumnNames.Add("Col_GST5PercentS");
            dgvReportList.DoubleColumnNames.Add("Col_Amount12PercentS");
            dgvReportList.DoubleColumnNames.Add("Col_GST12PercentS");
            dgvReportList.DoubleColumnNames.Add("Col_Amount18PercentS");
            dgvReportList.DoubleColumnNames.Add("Col_GST18PercentS");
            dgvReportList.DoubleColumnNames.Add("Col_Amount28PercentS");
            dgvReportList.DoubleColumnNames.Add("Col_GST28PercentS");

            dgvReportList.DoubleColumnNames.Add("Col_Amount5PercentC");
            dgvReportList.DoubleColumnNames.Add("Col_GST5PercentC");
            dgvReportList.DoubleColumnNames.Add("Col_Amount12PercentC");
            dgvReportList.DoubleColumnNames.Add("Col_GST12PercentC");
            dgvReportList.DoubleColumnNames.Add("Col_Amount18PercentC");
            dgvReportList.DoubleColumnNames.Add("Col_GST18PercentC");
            dgvReportList.DoubleColumnNames.Add("Col_Amount28PercentC");
            dgvReportList.DoubleColumnNames.Add("Col_GST28PercentC");

            dgvReportList.DoubleColumnNames.Add("Col_AddOnFreight");
            dgvReportList.DoubleColumnNames.Add("Col_AmountCreditNote");
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
                    if (btnVoucherNumberWise.Checked == true)
                    {
                        dgvrow.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                        //  dgvrow.Cells["Col_ACCID"].Value = dr["AccountID"].ToString();
                        dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        if (dr["VoucherNumber"] != null)
                            dgvrow.Cells["Col_VoucherNumber"].Value = Convert.ToInt32(dr["VoucherNumber"]);
                        dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        dgvrow.Cells["Col_AmountZeroPercent"].Value = dr["AmountGST0"].ToString();
                        dgvrow.Cells["Col_Amount5PercentS"].Value = dr["AmountGSTS5"].ToString();
                        dgvrow.Cells["Col_GST5PercentS"].Value = dr["GSTS5"].ToString();
                        dgvrow.Cells["Col_Amount12PercentS"].Value = dr["AmountGSTS12"].ToString();
                        dgvrow.Cells["Col_GST12PercentS"].Value = dr["GSTS12"].ToString();
                        dgvrow.Cells["Col_Amount18PercentS"].Value = dr["AmountGSTS18"].ToString();
                        dgvrow.Cells["Col_GST18PercentS"].Value = dr["GSTS18"].ToString();
                        dgvrow.Cells["Col_Amount28PercentS"].Value = dr["AmountGSTS28"].ToString();
                        dgvrow.Cells["Col_GST28PercentS"].Value = dr["GSTS28"].ToString();

                        dgvrow.Cells["Col_Amount5PercentC"].Value = dr["AmountGSTC5"].ToString();
                        dgvrow.Cells["Col_GST5PercentC"].Value = dr["GSTC5"].ToString();
                        dgvrow.Cells["Col_Amount12PercentC"].Value = dr["AmountGSTC12"].ToString();
                        dgvrow.Cells["Col_GST12PercentC"].Value = dr["GSTC12"].ToString();
                        dgvrow.Cells["Col_Amount18PercentC"].Value = dr["AmountGSTC18"].ToString();
                        dgvrow.Cells["Col_GST18PercentC"].Value = dr["GSTC18"].ToString();
                        dgvrow.Cells["Col_Amount28PercentC"].Value = dr["AmountGSTC28"].ToString();
                        dgvrow.Cells["Col_GST28PercentC"].Value = dr["GSTC28"].ToString();
                    //    dgvrow.Cells["Col_CashDiscount"].Value = dr["AmountCashDiscount"].ToString();
                      //  dgvrow.Cells["Col_CashDiscount"].Value = dr["AmountCashDiscount"].ToString();
                        //    dgvrow.Cells["Col_SpecialDiscount"].Value = dr["AmountSpecialDiscount"].ToString();
                        //    dgvrow.Cells["Col_SchemeDiscount"].Value = dr["AmountSchemeDiscount"].ToString();
                        //    dgvrow.Cells["Col_ItemDiscount"].Value = dr["AmountItemDiscount"].ToString();
                        dgvrow.Cells["Col_AddOnFreight"].Value = dr["AmountFreight"].ToString();
                        dgvrow.Cells["Col_AmountCreditNote"].Value = dr["AmountCreditNote"].ToString();
                        dgvrow.Cells["Col_AmountDebitNote"].Value = dr["AmountDebitNote"].ToString();
                        dgvrow.Cells["Col_RoundUpAmount"].Value = dr["RoundUpAmount"].ToString();
                        dgvrow.Cells["Col_Amount"].Value = dr["AmountNet"].ToString();
                    }
                    else
                    {
                        dgvrow.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                        //  dgvrow.Cells["Col_ACCID"].Value = dr["AccountID"].ToString();
                        dgvrow.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        if (dr["VoucherNumber"] != null)
                            dgvrow.Cells["Col_VoucherNumber"].Value = Convert.ToInt32(dr["VoucherNumber"]);
                        dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        dgvrow.Cells["Col_AccName"].Value = dr["ProdName"].ToString();
                        double gstper = Convert.ToDouble(dr["ProductVATPercent"].ToString());
                        if (gstper == 0)
                        {
                            dgvrow.Cells["Col_AmountZeroPercent"].Value = dr["GSTAmountZero"].ToString();
                        }
                        else if (gstper == 5)
                        {
                            dgvrow.Cells["Col_Amount5PercentS"].Value = dr["GSTAmountS"].ToString();
                            dgvrow.Cells["Col_GST5PercentS"].Value = dr["GSTS5"].ToString();
                            dgvrow.Cells["Col_Amount5PercentC"].Value = dr["GSTAmountC"].ToString();
                            dgvrow.Cells["Col_GST5PercentC"].Value = dr["GSTC"].ToString();
                        }
                        else if (gstper == 12)
                        {
                            dgvrow.Cells["Col_Amount12PercentS"].Value = dr["GSTSAmount"].ToString();
                            dgvrow.Cells["Col_GST12PercentS"].Value = dr["GSTS"].ToString();
                            dgvrow.Cells["Col_Amount12PercentC"].Value = dr["GSTCAmount"].ToString();
                            dgvrow.Cells["Col_GST12PercentC"].Value = dr["GSTC"].ToString();
                        }
                        else if (gstper == 18)
                        {
                            dgvrow.Cells["Col_Amount18PercentS"].Value = dr["GSTAmountS"].ToString();
                            dgvrow.Cells["Col_GST18PercentS"].Value = dr["GSTS5"].ToString();
                            dgvrow.Cells["Col_Amount18PercentC"].Value = dr["GSTAmountC"].ToString();
                            dgvrow.Cells["Col_GST18PercentC"].Value = dr["GSTC"].ToString();
                        }
                        else
                        {
                            dgvrow.Cells["Col_Amount28PercentS"].Value = dr["GSTAmountS"].ToString();
                            dgvrow.Cells["Col_GST28PercentS"].Value = dr["GSTS5"].ToString();
                            dgvrow.Cells["Col_Amount28PercentC"].Value = dr["GSTAmountC"].ToString();
                            dgvrow.Cells["Col_GST28PercentC"].Value = dr["GSTC"].ToString();
                        }
                       
                    }

                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
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
                if (btnVoucherNumberWise.Checked == true)
                    dtable = _Purchase.GetOverviewDataForGSTReport(_MFromDate, _MToDate);
                else
                    dtable = _Purchase.GetOverviewDataForGSTReportHSN(_MFromDate, _MToDate);
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
                   
                       
                    PrintReportHead = "GST Purchase Register Detail  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = " ";
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
                //   ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
                //   ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
            }
            catch (Exception Ex)
            {
                Log.WriteError("UclPatient.AddToolTip>>" + Ex.Message);
            }
        }
        #endregion


        private void txtMonth_Validating(object sender, CancelEventArgs e)
        {
            int month = 0;
            if (txtMonth.Text != null && txtMonth.Text != "")
                month = Convert.ToInt32(txtMonth.Text.ToString());
            if (month < 1 || month > 12)
            {
                //  lblMessage.Text = "1 to 12";
                txtMonth.Focus();
            }
            //else
            ////    lblMessage.Text = "";
        }

        private void txtMonth_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                if (txtMonth.Text != null && txtMonth.Text != "")
                {
                    month = Convert.ToInt32(txtMonth.Text.ToString());
                    if (month <= 3)
                        syear = General.ShopDetail.Shopey.Substring(0, 4);
                    else
                        syear = General.ShopDetail.Shopsy.Substring(0, 4);
                    txtYear.Text = syear;
                    if (month < 10)
                        smonth = "0" + month.ToString("0");
                    else
                        smonth = month.ToString("00");
                   
                    string dfmdate = syear + smonth + "01";
                    DateTime fmdate = General.ConvertStringToDateyyyyMMdd(dfmdate);
                    DateTime ttDate = fmdate.AddMonths(1);
                    TimeSpan tt = new TimeSpan(1, 0, 0, 0);
                    ttDate = ttDate.Subtract(tt);
                    toDate1.Value = ttDate;
                    fromDate1.Value = fmdate;
                    btnVoucherNumberWise.Focus();
                }
                else
                    txtMonth.Focus();
            }

        }

        private void btnVoucherNumberWise_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                btnHSNNumberWise.Focus();
                btnHSNNumberWise.Checked = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnVoucherNumberWise.Focus();
                btnVoucherNumberWise.Checked = true;
                btnOKMultiSelectionClick();
            }
        }

        private void btnHSNNumberWise_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                btnVoucherNumberWise.Focus();
                btnVoucherNumberWise.Checked = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnHSNNumberWise.Focus();
                btnHSNNumberWise.Checked = true;
                btnOKMultiSelectionClick();
            }
        }
    }

}
