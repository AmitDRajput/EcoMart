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
    public partial class UclStockListClosingStockAsOn : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private SsStock _SsStock;
        private string _MFromDate;
        private string _MToDate;
        private int _OpeningStock;
        #endregion

        # region Constructor
        public UclStockListClosingStockAsOn()
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
                HidepnlGO();
                _BindingSource = new DataTable();
                _SsStock = new SsStock();
                headerLabel1.Text = "PRODUCT LEDGER";
                ClearControls();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        public override void ShowReport(string ID, string blank, string FromDate, string ToDate)
        {
            base.ShowReport(ID, blank, FromDate, ToDate);
            if (ID != null && ID != "")
            {
                _MFromDate = FromDate;
                _MToDate = ToDate;
                _OpeningStock = _SsStock.GetOpendingStockByProductID(Convert.ToInt32(ID));
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


        #endregion

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
                PrintIfFirstRow = "Y";
                double mamt = 0;

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
                        PrintIfFirstRow = "Y";
                    }

                    if (dr.Visible)
                    {
                        PrintRowPixel += 17;
                        PrintRowCount += 1;
                        mamt = 0;
                        if (dr.Cells["Col_VoucherType"].Value == null && PrintIfFirstRow == "Y")
                        {
                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 13;
                            PrintRowCount += 1;
                            PrintIfFirstRow = "N";
                        }
                        if (dr.Cells["Col_AccName"].Value != null)
                        {
                            {
                                if (dr.Cells["Col_VoucherType"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                            }
                            if (dr.Cells["Col_VoucherNumber"].Value != null)
                            {
                                if (dr.Cells["Col_VoucherNumber"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadRight(10), PrintRowPixel, 50, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                            }
                            if (dr.Cells["Col_VoucherDate"].Value != null && dr.Cells["Col_VoucherDate"].Value.ToString() != "")
                            {
                                row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 100, PrintFont);
                                PrintBill.Rows.Add(row);
                            }

                            mamt = 0;

                            if (dr.Cells["Col_AccName"].Value != null && dr.Cells["Col_AccName"].Value.ToString() != "")
                            {
                                row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 180, PrintFont);
                                PrintBill.Rows.Add(row);
                            }


                            if (dr.Cells["Col_Batch"].Value != null)
                            {

                                row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(30), PrintRowPixel, 400, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_QtyIN"].Value != null && dr.Cells["Col_QtyIN"].Value.ToString() != "")
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_QtyIN"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(500 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);


                            if (dr.Cells["Col_ScmIN"].Value != null && dr.Cells["Col_ScmIN"].Value.ToString() != "")
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_ScmIN"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(560 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);


                            if (dr.Cells["Col_QtyOUT"].Value != null && dr.Cells["Col_QtyOUT"].Value.ToString() != "")
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_QtyOUT"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(620 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
                            PrintBill.Rows.Add(row);


                            if (dr.Cells["Col_ScmOUT"].Value != null && dr.Cells["Col_ScmOUT"].Value.ToString() != "")
                            {
                                mamt = Convert.ToDouble(dr.Cells["Col_ScmOUT"].Value.ToString());
                                mlen = (mamt.ToString("#0").Length);
                                colpix = Convert.ToInt32(680 + ((8.00 - Convert.ToDouble(mlen)) * 5.5));
                            }
                            row = new PrintRow(mamt.ToString("#0"), PrintRowPixel, colpix, PrintFont);
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

                row = new PrintRow("Vou Type", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Number", PrintRowPixel, 50, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 100, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Party", PrintRowPixel, 180, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Batch", PrintRowPixel, 400, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("QTY IN", PrintRowPixel, 500, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM/REPL", PrintRowPixel, 560, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("QTY OUT", PrintRowPixel, 620, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM/REPL", PrintRowPixel, 680, PrintFont);
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

        public void ClearControls()
        {
            try
            {

                toDate1.Value = DateTime.Now;
                lblFooterMessage.Text = "";
                dgvReportList.Rows.Clear();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
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
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "Product";
                column.Width = 200;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductLoosePack";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductPack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "Comp";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "ProdLastPurchaseMRP";
                column.HeaderText = "MRP";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = false;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Purchase Rate";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = false;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "Trade Rate";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = false;
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VatPercent";
                column.DataPropertyName = "VatPercent";
                column.HeaderText = "VatPercent";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = false;
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRateWithVat";
                column.DataPropertyName = "PurchaseRateWithVat";
                column.HeaderText = "Purchase Amount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = true;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VatAmount";
                column.DataPropertyName = "VatAmount";
                column.HeaderText = "Vat Amount";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Visible = true;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.HeaderText = "CL Stock";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 80;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CLValueMRP";
                column.DataPropertyName = "CLValueByMRP";
                column.HeaderText = "CLSTK Value";
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

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
            ViewFromDate.Text = "";
            ViewToDate.Text = "";
            toDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }
            if (_MFromDate != null)
                ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            if (_MToDate != null)
                ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            dgvReportList.Focus();
        }
        private void FillReportGrid()
        {
            try
            {
                dgvReportList.Rows.Clear();
                FillReportData();
                NoofRows();
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
                dtable = _SsStock.GetDataForClosingStockASOn(_MToDate);

                _BindingSource = dtable;
                BindReportGrid();

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
                //int mopstk = 0;
                int mclstk = 0;
                //string mvoudate = "";
                int mqtyin = 0;
                int mqtyout = 0;
                int mscmqtyin = 0;
                int mscmqtyout = 0;
                //int mcrstk = 0;
                //int mdbstk = 0;
                //int mpurstk = 0;
                //int mslstk = 0;
                int mProductID = 0;
                int premProductID = 0;
                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[0];
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    //  mvoudate = "";
                    mqtyin = 0;
                    mqtyout = 0;
                    mscmqtyin = 0;
                    mscmqtyout = 0;
                    mProductID = Convert.ToInt32(dr["ProductID"].ToString());
                    if (mProductID != premProductID)
                    {
                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        premProductID = mProductID;
                        if (dr["ProdName"] != DBNull.Value)
                            currentdr.Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                        if (dr["ProdLoosePack"] != DBNull.Value)
                            currentdr.Cells["Col_ProductLoosePack"].Value = dr["ProdLoosePack"].ToString();
                        if (dr["ProdPack"] != DBNull.Value)
                            currentdr.Cells["Col_ProductPack"].Value = dr["ProdPack"].ToString();

                        if (dr["ProdLastPurchaseRate"] != DBNull.Value)
                            currentdr.Cells["Col_PurchaseRate"].Value = dr["ProdLastPurchaseRate"].ToString();
                        if (dr["ProdLastPurchaseTradeRate"] != DBNull.Value)
                            currentdr.Cells["Col_TradeRate"].Value = dr["ProdLastPurchaseTradeRate"].ToString();
                        if (dr["ProdVATPercent"] != DBNull.Value)
                            currentdr.Cells["Col_VatPercent"].Value = Convert.ToString(dr["ProdVATPercent"]);

                        if (dr["QuantityIN"] != DBNull.Value && dr["QuantityIN"].ToString() != "")
                            int.TryParse(dr["QuantityIN"].ToString(), out mqtyin);
                        if (dr["QuantityOUT"] != DBNull.Value && dr["QuantityOUT"].ToString() != "")
                            int.TryParse(dr["QuantityOUT"].ToString(), out mqtyout);
                        if (dr["SchemeQuantityIN"] != DBNull.Value && dr["SchemeQuantityIN"].ToString() != "")
                            int.TryParse(dr["SchemeQuantityIN"].ToString(), out mscmqtyin);
                        if (dr["SchemeQuantityOUT"] != DBNull.Value && dr["SchemeQuantityOUT"].ToString() != "")
                            int.TryParse(dr["SchemeQuantityOUT"].ToString(), out mscmqtyout);
                        int mclosingstock = mqtyin + mscmqtyin - mqtyout - mscmqtyout;
                        currentdr.Cells["Col_ClosingStock"].Value = mclosingstock.ToString("#0");

                    }
                    else
                    {
                        mclstk = 0;

                        mclstk = Convert.ToInt32(currentdr.Cells["Col_ClosingStock"].Value.ToString());
                        if (dr["QuantityIN"] != DBNull.Value && dr["QuantityIN"].ToString() != "")
                            int.TryParse(dr["QuantityIN"].ToString(), out mqtyin);
                        if (dr["QuantityOUT"] != DBNull.Value && dr["QuantityOUT"].ToString() != "")
                            int.TryParse(dr["QuantityOUT"].ToString(), out mqtyout);
                        if (dr["SchemeQuantityIN"] != DBNull.Value && dr["SchemeQuantityIN"].ToString() != "")
                            int.TryParse(dr["SchemeQuantityIN"].ToString(), out mscmqtyin);
                        if (dr["SchemeQuantityOUT"] != DBNull.Value && dr["SchemeQuantityOUT"].ToString() != "")
                            int.TryParse(dr["SchemeQuantityOUT"].ToString(), out mscmqtyout);
                        int mclosingstock = mclstk + mqtyin + mscmqtyin - mqtyout - mscmqtyout;
                        currentdr.Cells["Col_ClosingStock"].Value = mclosingstock.ToString("#0");

                    }


                }


                //_RowIndex = dgvReportList.Rows.Add();
                //currentdr = dgvReportList.Rows[_RowIndex];
                //currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                //currentdr.Cells["Col_AccName"].Value = "Opening Stock";
                //currentdr.Cells["Col_QtyIN"].Value = (_OpeningStock + mopstk).ToString();
                //_RowIndex = dgvReportList.Rows.Add();
                //currentdr = dgvReportList.Rows[_RowIndex];
                //currentdr.Cells["Col_AccName"].Value = "Purchase Stock";
                //currentdr.Cells["Col_QtyIN"].Value = (mpurstk).ToString();
                //_RowIndex = dgvReportList.Rows.Add();
                //currentdr = dgvReportList.Rows[_RowIndex];
                //currentdr.Cells["Col_AccName"].Value = "Credit Note Stock";
                //currentdr.Cells["Col_QtyIN"].Value = (mcrstk).ToString();
                //_RowIndex = dgvReportList.Rows.Add();
                //currentdr = dgvReportList.Rows[_RowIndex];
                //currentdr.Cells["Col_AccName"].Value = "Sale Stock";
                //currentdr.Cells["Col_Qtyout"].Value = (mslstk).ToString();
                //_RowIndex = dgvReportList.Rows.Add();
                //currentdr = dgvReportList.Rows[_RowIndex];
                //currentdr.Cells["Col_AccName"].Value = "Debit Note Stock";
                //currentdr.Cells["Col_Qtyout"].Value = (mdbstk).ToString();
                //_RowIndex = dgvReportList.Rows.Add();
                //currentdr = dgvReportList.Rows[_RowIndex];
                //mclstk = _OpeningStock + mopstk + mpurstk + mcrstk - mslstk - mdbstk;
                //currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                //currentdr.Cells["Col_AccName"].Value = "Closing Stock";
                //currentdr.Cells["Col_QtyIN"].Value = (mclstk).ToString();

                BindVatAmountData();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void BindVatAmountData()
        {
            try
            {
                //int ProductID = 0;
                int ClosingStock = 0;
                int UOM = 0;
                double ProdpurchaseRate = 0;
                double ProdTradeRate = 0;
                double Vat = 0;
                double purchaseAmt = 0;
                double VatAmt = 0;

                double TotalpurchaseAmt = 0;
                double TotalVatAmt = 0;

                foreach (DataGridViewRow row in dgvReportList.Rows)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(row.Cells["Col_ProductName"].Value)) == false)
                    {
                        ClosingStock = Convert.ToInt32(row.Cells["Col_ClosingStock"].Value);
                        UOM = Convert.ToInt32(row.Cells["Col_ProductLoosePack"].Value);
                        ProdpurchaseRate = Convert.ToDouble(row.Cells["Col_purchaseRate"].Value);
                        ProdTradeRate = Convert.ToDouble(row.Cells["Col_TradeRate"].Value);
                        Vat = Convert.ToDouble(row.Cells["Col_VatPercent"].Value);

                        purchaseAmt = ClosingStock * (ProdpurchaseRate / UOM) + (ClosingStock * (ProdTradeRate / UOM) * Vat / 100);
                        VatAmt = ClosingStock * (ProdTradeRate / UOM) * Vat / 100;
                        row.Cells["Col_PurchaseRateWithVat"].Value = purchaseAmt.ToString("#0.00");
                        row.Cells["Col_VatAmount"].Value = VatAmt.ToString("#0.00");

                        TotalpurchaseAmt += purchaseAmt;
                        TotalVatAmt += VatAmt;
                    }
                }
                lblAmtTotal.Text = string.Format("Total Purchase Amount: {0} TotalVat Amount: {1}", TotalpurchaseAmt.ToString("#0.00"), TotalVatAmt.ToString("#0.00"));
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
                _MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                retValue = General.CheckDates(_MToDate, _MToDate);
                if (retValue)
                {

                    this.Cursor = Cursors.WaitCursor;
                    _OpeningStock = 0;
                    FillReportGrid();
                    ShowpnlGO();
                    PrintReportHead = "Product Ledger  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
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
        #endregion Other Private Methods 

        # region Events

        private void btnOKMultiSelection_Click(object sender, EventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        private void todate1_KeyDown(object sender, KeyEventArgs e)
        {
            btnOKMultiSelectionClick();
        }


        private void btnOKMultiSelection1_KeyDown(object sender, KeyEventArgs e)
        {
            btnOKMultiSelectionClick();
        }

        #endregion Events
    }
}
