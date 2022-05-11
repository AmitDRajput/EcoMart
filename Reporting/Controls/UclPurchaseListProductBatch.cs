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
    public partial class UclPurchaseListProductBatch : ReportBaseControl
    {
        #region Declaration
        private DataTable _BindingSource;
        private DataTable _BindingSourceMultiSelection;      
        private Purchase _Purchase;
        private Stock _Stock;
        private string _MFromDate;
        private string _MToDate;
        private List<DataGridViewRow> rowCollection;
        private string _MProductName;       
        private string _MMrp;
        #endregion

        # region Constructor
        public UclPurchaseListProductBatch()
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
                _Stock = new Stock();                        
                headerLabel1.Text = "PURCHASE PRODUCT/BATCHWISE)";            
                FillProductCombo();
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
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    if (dr.Cells["Col_BillNumber"].Value != null || dr.Cells["Col_AccName"].Value != null)
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
                        double mamt = 0;

                        if (dr.Cells["Col_VoucherType"].Value == null && dr.Cells["Col_BillNumber"].Value != null)
                        {


                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);
                            PrintRowPixel += 13;
                            PrintRowCount += 1;
                            if (dr.Cells["Col_AccName"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_BillNumber"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_BillNumber"].Value.ToString().PadRight(15), PrintRowPixel, 340, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Quantity"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 500, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_SCM"].Value != null)
                            {
                                row = new PrintRow(dr.Cells["Col_SCM"].Value.ToString().PadLeft(6), PrintRowPixel, 540, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            if (dr.Cells["Col_Amount"].Value != null)
                            {

                                mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                mlen = (mamt.ToString("#0.00").Length);
                                colpix = Convert.ToInt32(670.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            PrintRowPixel += 13;
                            PrintRowCount += 1;

                            row = new PrintRow(FixAccounts.DashLine80Normal, PrintRowPixel, 1, PrintFont);
                            PrintBill.Rows.Add(row);

                        }
                        else
                        {
                            //if (dr.Cells["Col_AccName"].Value != null)
                            //{
                            //    row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                            //    PrintBill.Rows.Add(row);
                            //}
                            if (dr.Cells["Col_VoucherType"].Value == null && dr.Cells["Col_AccName"].Value != null && dr.Cells["Col_BillNumber"].Value == null)
                            {
                                row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                PrintBill.Rows.Add(row);
                            }
                            else
                            {
                                if (dr.Cells["Col_AccName"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_AccName"].Value.ToString().PadRight(30), PrintRowPixel, 1, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_VoucherType"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherType"].Value.ToString(), PrintRowPixel, 200, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }

                                if (dr.Cells["Col_VoucherNumber"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherNumber"].Value.ToString().PadLeft(6), PrintRowPixel, 240, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_VoucherDate"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_VoucherDate"].Value.ToString(), PrintRowPixel, 290, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_BillNumber"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_BillNumber"].Value.ToString().PadRight(30), PrintRowPixel, 340, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }

                                if (dr.Cells["Col_Batch"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Batch"].Value.ToString().PadRight(15), PrintRowPixel, 420, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Quantity"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_Quantity"].Value.ToString().PadLeft(6), PrintRowPixel, 500, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_SCM"].Value != null)
                                {
                                    row = new PrintRow(dr.Cells["Col_SCM"].Value.ToString().PadLeft(6), PrintRowPixel, 540, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Rate"].Value != null)
                                {
                                    mamt = Convert.ToDouble(dr.Cells["Col_Rate"].Value.ToString());
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(580.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                                if (dr.Cells["Col_Amount"].Value != null)
                                {

                                    mamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                                    mlen = (mamt.ToString("#0.00").Length);
                                    colpix = Convert.ToInt32(670.00 + ((12.00 - Convert.ToDouble(mlen)) * 5.5));
                                    row = new PrintRow(mamt.ToString("#0.00"), PrintRowPixel, colpix, PrintFont);
                                    PrintBill.Rows.Add(row);
                                }
                            }
                        }

                    }
                }

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
                row = new PrintRow("Party", PrintRowPixel, 1, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Type", PrintRowPixel, 200, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("No", PrintRowPixel, 240, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Date", PrintRowPixel, 290, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Bill No", PrintRowPixel, 340, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Batch", PrintRowPixel, 420, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Qty", PrintRowPixel, 500, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("SCM/Repl", PrintRowPixel, 540, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Pur.Rate", PrintRowPixel, 610, PrintFont);
                PrintBill.Rows.Add(row);
                row = new PrintRow("Amount", PrintRowPixel, 690, PrintFont);
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
                
                ConstructMultiSelectionGridColumns();
                InitializeReportGrid();
                cbSelectAll.Checked = false;
                mcbProduct.SelectedID = "";                    
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
            ConstructMultiSelectionGridColumns();
            dgvMultiSelection.Columns["Col_ID"].Visible = false;           
            cbSelectAll.Checked = false;     
            pnlMultiSelection1.Visible = true;
            tsbtnPrint.Enabled = false;
            ViewFromDate.Text = "";
            ViewToDate.Text = "";
            fromDate1.Focus();
        }

        public void ShowpnlGO()
        {
            pnlMultiSelection1.Visible = false;
            if (General.EcoMartLicense.LicenseType == EcoMartLicenseLib.LicenseTypes.Full)
            {
                tsbtnPrint.Enabled = true;
            }       
            ViewFromDate.Text = General.GetDateInShortDateFormat(_MFromDate);
            ViewToDate.Text = General.GetDateInShortDateFormat(_MToDate);
            dgvReportList.Focus();
        }
        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "PurchaseID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccName";
                column.DataPropertyName = "AccName";
                column.HeaderText = "Party";
                column.Width = 190;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                column.DataPropertyName = "VoucherType";
                column.HeaderText = "Type";
                column.Width = 50;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "No";
                column.Width = 60;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherDate";
                column.DataPropertyName = "VoucherDate";
                column.HeaderText = "Date";               
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BillNumber";
                column.DataPropertyName = "PurchaseBillNumber";
                column.HeaderText = "Bill.Number";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);           

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "Qty";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SCM";
                column.DataPropertyName = "SchemeQuantity";
                column.HeaderText = "SCM";
                column.Width = 70;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 120;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Rate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur.Rate";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
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
        private void ConstructMultiSelectionGridColumns()
        {
            try
            {
                dgvMultiSelection.Columns.Clear();
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ID";
                column.ValueType = typeof(string);
                column.Visible = false;
                dgvMultiSelection.Columns.Add(column);

                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
                //    columnCheck.DataPropertyName = "TAG";
                columnCheck.HeaderText = "Check";
                column.ValueType = typeof(bool);
                columnCheck.Width = 100;
                dgvMultiSelection.Columns.Add(columnCheck);             

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.ReadOnly = true;
                column.ValueType = typeof(string);
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.ReadOnly = true;
             //   column.ValueType = typeof(string);
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.HeaderText = "Pur Rate";
                column.ReadOnly = true;
              //  column.ValueType = typeof(string);
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.HeaderText = "T Rate";
                column.ReadOnly = true;
                //  column.ValueType = typeof(string);
                column.Width = 200;
                dgvMultiSelection.Columns.Add(column);



            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }      
        private void FillAccountData(string ProductID, string batch, double mrp)
        {
            try
            {
                int _RowIndex;
                _RowIndex = dgvReportList.Rows.Add();
                DataGridViewRow currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTitleColor;
                currentdr.Cells["Col_ID"].Value = ProductID;
                _MProductName = mcbProduct.SeletedItem.ItemData[1].ToString();
                //_MBatchNumber = mcbProduct.SeletedItem.ItemData[2].ToString();
                _MMrp = mrp.ToString("#0.00");

                currentdr.Cells["Col_AccName"].Value =  _MProductName + " MRP:" + _MMrp;

                double mtotamount = 0;
                int mtotquantity = 0;
                int mtotscheme = 0;

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (dr["ProductID"].ToString() == ProductID && dr["BatchNumber"].ToString() == batch && Convert.ToDouble(dr["MRP"].ToString()) == mrp && Convert.ToInt32(dr["VoucherDate"].ToString()) >= Convert.ToInt32(_MFromDate) && Convert.ToInt32(dr["VoucherDate"].ToString()) <= Convert.ToInt32(_MToDate))
                    {

                        double mamt = 0;

                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["PurchaseID"].ToString();
                        currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                        currentdr.Cells["Col_VoucherDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_BillNumber"].Value = dr["PurchaseBillNumber"].ToString();
                        currentdr.Cells["Col_AccName"].Value = dr["AccName"].ToString();
                        currentdr.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();
                        currentdr.Cells["Col_SCM"].Value = dr["SchemeQuantity"].ToString();
                        currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();
                        mamt = Convert.ToDouble(dr["PurchaseRate"].ToString());
                        currentdr.Cells["Col_Rate"].Value = mamt.ToString("#0.00");
                        mamt = Convert.ToDouble(dr["Amount"].ToString());
                        currentdr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                        mtotamount += Convert.ToDouble(dr["Amount"].ToString());
                        mtotquantity += Convert.ToInt32(dr["Quantity"].ToString());
                        mtotscheme += Convert.ToInt32(dr["SchemeQuantity"].ToString());
                    }
                }


                _RowIndex = dgvReportList.Rows.Add();
                currentdr = dgvReportList.Rows[_RowIndex];
                currentdr.DefaultCellStyle.BackColor = General.ReportTotalBackColor;
                currentdr.DefaultCellStyle.ForeColor = General.ReportTotalForeColor;
                currentdr.Cells["Col_BillNumber"].Value = "Total";
                currentdr.Cells["Col_Quantity"].Value = mtotquantity.ToString();
                currentdr.Cells["Col_SCM"].Value = mtotscheme.ToString();
                currentdr.Cells["Col_Amount"].Value = mtotamount.ToString("#0.00");
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }


        }         
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
                    bool ifselected = false;                  
                    rowCollection = new List<DataGridViewRow>();

                    foreach (DataGridViewRow selectedrow in dgvMultiSelection.Rows)
                    {
                        if (selectedrow.Cells["Col_Check"].Value != DBNull.Value)
                        {
                            ifselected = Convert.ToBoolean(selectedrow.Cells["Col_Check"].Value);
                        }
                        if (ifselected)
                        {
                            rowCollection.Add(selectedrow);
                        }

                    }
                    if (rowCollection.Count > 0)
                    {  
                        string mproductID = "";
                        string mbatch = "";
                        foreach (DataGridViewRow row in rowCollection)
                        {
                            mproductID = "";
                            mbatch = "";
                            double mmrp = 0;
                            if (row.Cells["Col_ID"].Value != null)
                                mproductID = row.Cells["Col_ID"].Value.ToString();
                            if (row.Cells["Col_Batch"].Value != null)
                                mbatch = row.Cells["Col_Batch"].Value.ToString();
                            if (row.Cells["Col_MRP"].Value != null)
                                double.TryParse(row.Cells["Col_MRP"].Value.ToString(), out mmrp);

                            if (mproductID != "")
                            {
                                GetBindingSource(mbatch, mmrp);

                                if (_BindingSource.Rows.Count > 0)
                                {
                                    FillAccountData(mproductID, mbatch, mmrp);
                                }
                            }
                        }
                    }
                    this.Cursor = Cursors.Default;
                    PrintReportHead = "Purchase Report [Product/Batch]  From: " + General.GetDateInShortDateFormat(_MFromDate) + " To: " + General.GetDateInShortDateFormat(_MToDate);
                    PrintReportHead2 = "[" + txtViewText.Text.ToString() + "]";
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
        private void GetBindingSource(string batch, double mrp)
        {
            DataTable dtable = new DataTable();
            dtable = _Purchase.GetOverviewDataForProductBatchList(mcbProduct.SelectedID, batch, mrp);
            _BindingSource = dtable;
        }
        private void FillMultiSelectionGrid()
        {

            try
            {
                ConstructMultiSelectionGridColumns();
                dgvMultiSelection.Columns["Col_ID"].Visible = false;
                if (dgvMultiSelection.Rows.Count > 0)
                    dgvMultiSelection.Rows.Clear();
                FillMultiSelectionData();
                dgvMultiSelection.DataSource = _BindingSourceMultiSelection;

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillMultiSelectionData()
        {
            try
            {
                if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                {
                    DataTable dtable = _Stock.GetBatchListByProductIDForPurchaseBatchWise(mcbProduct.SelectedID);
                    _BindingSourceMultiSelection = dtable;
                    dgvMultiSelection.DataSource = _BindingSourceMultiSelection;
                }
                dgvMultiSelection.Columns["Col_ID"].Visible = false;    
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
        private void FillProductCombo()
        {
            try
            {
                mcbProduct.SelectedID = null;
                mcbProduct.SourceDataString = new string[5] { "ProductID", "ProdName", "ProdLoosePack", "ProdPack", "ProdCompShortName" };
                mcbProduct.ColumnWidth = new string[5] { "0", "250", "50", "50", "50" };
                mcbProduct.ValueColumnNo = 0;
                mcbProduct.UserControlToShow = new UclProduct();
                DataTable dtable = new DataTable();
                //dtable = General.ProductList;
                Product prod = new Product();
                dtable = prod.GetOverviewData();
                mcbProduct.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        #endregion

        # region Events 
      

        private void mcbProduct_SeletectIndexChanged(object sender, EventArgs e)
        {
            if (mcbProduct.SelectedID != null && mcbProduct.SelectedID != "")
                FillMultiSelectionData();
            else
            {
                ConstructMultiSelectionGridColumns();
                dgvMultiSelection.Columns["Col_ID"].Visible = false;
            }
            dgvMultiSelection.Focus();
        }
        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                foreach (DataGridViewRow drow in dgvMultiSelection.Rows)
                {
                    drow.Cells["Col_Check"].Value = cbSelectAll.Checked;
                }

            }
            catch (Exception Ex)
            {

                Log.WriteException(Ex);
            }

        }
        private void dgvReportList_DoubleClicked(object sender, EventArgs e)
        {
            if (dgvReportList.SelectedRow != null && dgvReportList.Rows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                string selectedID = dgvReportList.SelectedRow.Cells["Col_ID"].Value.ToString();
                ViewControl = new UclPurchase();
                ShowViewForm(selectedID, ViewMode.Current);
                this.Cursor = Cursors.Default;
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
                mcbProduct.Focus();
            else if (e.KeyCode == Keys.Up)
                fromDate1.Focus();
            else if (e.KeyCode == Keys.Left)
                fromDate1.Focus();
        }

        #endregion Events

    
       
    }
}
