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
    public partial class UclChallanProductList : ReportBaseControl
    {
        #region Declaration

        private DataTable _BindingSource;
        private ChallanPurchase _ChallanPurchase;
        private string _MFromDate;
        private string _MToDate;
        private double _MTotalAmount;

        #endregion

        #region Constructor

        public UclChallanProductList()
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

        #region IOverview

        public override void ShowOverview()
        {
            try
            {
                _BindingSource = new DataTable();
                _ChallanPurchase = new ChallanPurchase();
                _MFromDate = General.ShopDetail.Shopsy;
                _MToDate = General.ShopDetail.Shopey;
                headerLabel1.Text = "Challan Purchase LIST";
                InitializeReportGrid();
                //AddToolTip();
                ClearControls();
                FillPartyCombo();
                HidepnlGO();
                mcbParty.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void SetFocus()
        {
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

        #endregion IOverview

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
                if (txtViewtext.Text != null && txtViewtext.Text.ToString() != "")
                    PrintReportHead2 = "Party : " + "[" + txtViewtext.Text.ToString() + "]  ";
                if (txtViewAmount.Text != null && txtViewAmount.Text.ToString() != "")
                    PrintReportHead2 = PrintReportHead2 + "Amount : " + txtViewAmount.Text.ToString();

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

                    if (dr.Cells["Col_ID"].Value != null)
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

                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_ChallanNumber"].Value)) == false)
                        {
                            row = new PrintRow(dr.Cells["Col_ChallanNumber"].Value.ToString(), PrintRowPixel, 5, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_ChallanDate"].Value)) == false)
                        {
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_ChallanDate"].Value.ToString()), PrintRowPixel, 45, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_AccName"].Value)) == false)
                        {
                            int length = Math.Min(dr.Cells["Col_AccName"].Value.ToString().Length, 20);
                            row = new PrintRow((dr.Cells["Col_AccName"].Value.ToString()).Substring(0, length), PrintRowPixel, 105, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_ProductName"].Value)) == false)
                        {
                            int length = Math.Min(dr.Cells["Col_ProductName"].Value.ToString().Length, 20);
                            row = new PrintRow((dr.Cells["Col_ProductName"].Value.ToString()).Substring(0, length), PrintRowPixel, 260, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_BatchNumber"].Value)) == false)
                        {
                            row = new PrintRow((dr.Cells["Col_BatchNumber"].Value.ToString()), PrintRowPixel, 410, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_Expiry"].Value)) == false)
                        {
                            row = new PrintRow(General.GetDateInEXPDateFormat(dr.Cells["Col_Expiry"].Value.ToString()), PrintRowPixel, 470, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_TradeRate"].Value)) == false)
                        {
                            row = new PrintRow((dr.Cells["Col_TradeRate"].Value.ToString()), PrintRowPixel, 520, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_Quantity"].Value)) == false)
                        {
                            row = new PrintRow((dr.Cells["Col_Quantity"].Value.ToString()), PrintRowPixel, 570, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (dr.Cells["Col_Amount"].Value != null)
                        {
                            mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mlen = (mamt.ToString("#0.00").Length);
                            colpix = Convert.ToInt32(580 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                            row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_VoucherNumber"].Value)) == false)
                        {
                            row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString(), PrintRowPixel, 660, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_PurchaseBillNumber"].Value)) == false)
                        {
                            row = new PrintRow(dr.Cells["Col_PurchaseBillNumber"].Value.ToString(), PrintRowPixel, 700, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(dr.Cells["Col_VoucherDate"].Value)) == false)
                        {
                            row = new PrintRow(General.GetDateInShortDateFormat(dr.Cells["Col_VoucherDate"].Value.ToString()), PrintRowPixel, 740, PrintFont);
                            PrintBill.Rows.Add(row);
                        }
                    }
                }
                PrintRowPixel += 17;
                row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                PrintRowPixel += 17;

                mamt = _MTotalAmount;
                mlen = (mamt.ToString("#0.00").Length);
                colpix = Convert.ToInt32(580.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                PrintBill.Rows.Add(row);

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

                row = new PrintRow("ChlnNo", PrintRowPixel, 0, PrintFont);
                PrintBill.Rows.Add(row);

                row = new PrintRow("ChlnDate", PrintRowPixel, 45, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 105, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Product Name", PrintRowPixel, 260, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Batch No", PrintRowPixel, 410, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Exp", PrintRowPixel, 480, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("TRate", PrintRowPixel, 510, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Quantity", PrintRowPixel, 550, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 600, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("PurVNo", PrintRowPixel, 650, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("BillNo", PrintRowPixel, 700, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Bill Date", PrintRowPixel, 740, PrintFont);
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

        #region Other Private Methods

        public void ClearControls()
        {
            try
            {
                txtAmount.Text = "0.00";
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
            txtViewtext.Text = "";
            mcbParty.SelectedID = "";
            txtAmount.Text = "0.0";
            txtViewAmount.Text = "";
            fromDate1.Focus();

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
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChallanNumber";
                column.HeaderText = "ChallanNo";
                column.Width = 70;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ChallanDate";
                column.HeaderText = "ChallanDate";
                column.Width = 80;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccountID";
                column.HeaderText = "AccountID";
                column.Visible = false;
                column.Width = 115;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.HeaderText = "Party";
                column.Width = 150;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_Address";
                //column.HeaderText = "Address";
                //column.Width = 190;
                //column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.HeaderText = "Product Name";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.HeaderText = "Batch No";
                column.Width = 70;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.HeaderText = "ExpiryDate";
                column.Width = 75;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.HeaderText = "TradeRate";
                column.Width = 70;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.HeaderText = "Quantity";
                column.Width = 60;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.HeaderText = "Amount";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                //column = new DataGridViewTextBoxColumn();
                //column.Name = "Col_CVoucherType";
                //column.HeaderText = "BillType";
                //column.Width = 50;
                //column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                //dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.HeaderText = "Pur.VoucherNo";
                column.Width = 100;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseBillNumber";
                column.HeaderText = "Pur.BillNo";
                column.Width = 70;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Bill.Date";
                column.Width = 60;
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
                //  dgvReportList.DataSource = _BindingSource;             
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
            dgvReportList.DateColumnNames.Add("Col_ChallanDate");
            dgvReportList.DoubleColumnNames.Add("Col_Amount");
            dgvReportList.DateColumnNames.Add("Col_VoucherDate");
            dgvReportList.DateColumnNames.Add("Col_Expiry");
        }
        private void BindReportGrid()
        {
            try
            {
                _MTotalAmount = 0;
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (dr["ID"] != DBNull.Value)
                    {
                        string drID = "";
                        double drAmt = 0;
                        if (dr["AccountId"] != DBNull.Value)
                            drID = dr["AccountID"].ToString();
                        if (dr["Amount"] != DBNull.Value)
                            drAmt = Convert.ToDouble(dr["Amount"].ToString());
                        int rowIndex = dgvReportList.Rows.Add();
                        DataGridViewRow dgvrow = dgvReportList.Rows[rowIndex];
                        dgvrow.Cells["Col_ID"].Value = dr["ID"].ToString();
                        if (string.IsNullOrEmpty(dr["ChallanNumber"].ToString()) == false)
                            dgvrow.Cells["Col_ChallanNumber"].Value = dr["ChallanNumber"].ToString();
                        if (string.IsNullOrEmpty(dr["ChallanDate"].ToString()) == false)
                            dgvrow.Cells["Col_ChallanDate"].Value = dr["ChallanDate"].ToString();
                        if (string.IsNullOrEmpty(dr["AccountID"].ToString()) == false)
                            dgvrow.Cells["Col_AccountID"].Value = dr["AccountID"].ToString();

                        if (string.IsNullOrEmpty(dr["AccName"].ToString()) == false)
                            dgvrow.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        if (string.IsNullOrEmpty(dr["ProdName"].ToString()) == false)
                            dgvrow.Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                        if (string.IsNullOrEmpty(dr["BatchNumber"].ToString()) == false)
                            dgvrow.Cells["Col_BatchNumber"].Value = dr["BatchNumber"].ToString();
                        if (string.IsNullOrEmpty(dr["ExpiryDate"].ToString()) == false)
                            dgvrow.Cells["Col_Expiry"].Value = dr["ExpiryDate"].ToString();
                        if (string.IsNullOrEmpty(dr["TradeRate"].ToString()) == false)
                            dgvrow.Cells["Col_TradeRate"].Value = dr["TradeRate"].ToString();
                        if (string.IsNullOrEmpty(dr["Quantity"].ToString()) == false)
                            dgvrow.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();

                        if (string.IsNullOrEmpty(dr["VoucherNumber"].ToString()) == false)
                            dgvrow.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                        if (string.IsNullOrEmpty(dr["VoucherDate"].ToString()) == false)
                            dgvrow.Cells["Col_VoucherDate"].Value = dr["VoucherDate"].ToString();
                        if (string.IsNullOrEmpty(dr["PurchaseBillNumber"].ToString()) == false)
                            dgvrow.Cells["Col_PurchaseBillNumber"].Value = dr["PurchaseBillNumber"].ToString();
                        dgvrow.Cells["Col_Amount"].Value = dr["Amount"].ToString();
                        _MTotalAmount += Convert.ToDouble(dr["Amount"].ToString());
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
            dgvrow.Cells["Col_AccName"].Value = "Total";
            dgvrow.Cells["Col_Amount"].Value = _MTotalAmount.ToString("#0.00");
        }
        private void NoofRows()
        {
            string strmessage = General.NoofRows(dgvReportList.Rows.Count);
            lblFooterMessage.Text = strmessage;
        }


        //private bool CheckFilter(string drID, string drtype, double dramt, int drbillNumber)
        //{
        //    bool retValue = false;
        //    try
        //    {

        //        string mtype = "";
        //        if (mcbType.Text != null && mcbType.Text != "")
        //            mtype = mcbType.Text.ToString();
        //        double mamt = Convert.ToDouble(txtAmount.Text.ToString());

        //        //if (drbillNumber >= 0)
        //        //    retValue = false;
        //        if (mcbParty.SelectedID != null && mcbParty.SelectedID != "")
        //        {
        //            string selectedid = mcbParty.SelectedID;
        //            if (txtAmount.Text != null && txtAmount.Text != "0.00")
        //            {
        //                if (mtype == null || mtype == "")
        //                {
        //                    if (drID == selectedid && dramt == mamt)
        //                        retValue = true;
        //                }
        //                else
        //                {
        //                    if (drID == selectedid && dramt == mamt && drtype == mtype)
        //                        retValue = true;
        //                }
        //            }
        //            else
        //            {
        //                if (mtype == null || mtype == "")
        //                {
        //                    if (drID == selectedid)
        //                        retValue = true;
        //                }
        //                else
        //                {
        //                    if (drID == selectedid && drtype == mtype)
        //                        retValue = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (txtAmount.Text != null && Convert.ToDouble(txtAmount.Text.ToString()) != 0)
        //            {
        //                if (mtype == null || mtype == "")
        //                {
        //                    if (dramt == mamt)
        //                        retValue = true;
        //                }
        //                else
        //                {
        //                    if (dramt == mamt && drtype == mtype)
        //                        retValue = true;
        //                }
        //            }
        //            else
        //            {
        //                if (mtype == null || mtype == "")
        //                    retValue = true;
        //                else
        //                {
        //                    if (drtype == mtype)
        //                        retValue = true;
        //                }

        //            }

        //        }

        //    }

        //    catch (Exception ex) { Log.WriteException(ex); }
        //    return retValue;
        //}

        private void FillReportData()
        {
            try
            {
                DataTable dtable = new DataTable();
                if (mcbParty.SeletedItem == null)
                {
                    if (rbtnPending.Checked == true)
                        dtable = _ChallanPurchase.GetOverviewDataForPendingChallanProductSummary(_MFromDate, _MToDate);
                    else
                        dtable = _ChallanPurchase.GetOverviewDataForAllChallanProductSummary(_MFromDate, _MToDate);
                }
                else
                {
                    string PartyID = mcbParty.SelectedID;
                    if (rbtnPending.Checked == true)
                        dtable = _ChallanPurchase.GetOverviewDataForPartyPendingChallanProductSummary(PartyID, _MFromDate, _MToDate);
                    else
                        dtable = _ChallanPurchase.GetOverviewDataForPartyAllChallanProductSummary(PartyID, _MFromDate, _MToDate);
                }
                _BindingSource = dtable;
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
                mcbParty.SelectedID = null;
                mcbParty.SourceDataString = new string[5] { "AccountID", "AccCode", "AccName", "AccAddress1", "AccAreaID" };
                mcbParty.ColumnWidth = new string[5] { "0", "20", "200", "200", "0" };
                mcbParty.DisplayColumnNo = 2;
                mcbParty.ValueColumnNo = 0;
                mcbParty.UserControlToShow = new UclAccount();
                Account _Party = new Account();
                DataTable dtable = _Party.GetOverviewDataForList();
                mcbParty.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion Other Private Methods

        #region events

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
                    if (mcbParty.SelectedID != null && mcbParty.SelectedID != "")
                        txtViewtext.Text = mcbParty.SeletedItem.ItemData[2];
                    else
                        txtViewtext.Text = "";

                    if (Convert.ToDouble(txtAmount.Text.ToString()) != 0)
                        txtViewAmount.Text = txtAmount.Text.ToString();
                    else
                        txtViewAmount.Text = "";
                    lblFooterMessage.Text = "";
                    ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
                    ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
                    FillReportGrid();
                    PrintReportHead = "Challan Purchase LIST From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
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

        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {

        }

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }
        private void btnOKMultiSelection_KeyDown(object sender, KeyEventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void mcbParty_EnterKeyPressed(object sender, EventArgs e)
        {
            txtAmount.Focus();
        }
        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    mcbType.Focus();
        }
        private void mcbType_KeyDown(object sender, KeyEventArgs e)
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
                mcbParty.Focus();
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
                fromDate1.Focus();
        }

        #endregion Events

        #region AddToolTip
        //private void AddToolTip()
        //{
        //    try
        //    {
        //        ttToolTip.SetToolTip(btnOKMultiSelection1, "Click to See Report = End");
        //        ttToolTip.SetToolTip(pnlMultiSelection1, "Home to Reopen This Form");
        //    }
        //    catch (Exception Ex)
        //    {
        //        Log.WriteException(Ex);
        //    }
        //}
        #endregion AddToolTip
    }
}
